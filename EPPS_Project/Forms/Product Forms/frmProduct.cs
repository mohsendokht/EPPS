using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmProduct
    {
        public frmProduct()
        {
            InitializeComponent();
            _txtName.Name = "txtName";
            _cmdSave.Name = "cmdSave";
            _cmdExit.Name = "cmdExit";
            _cmdDelete.Name = "cmdDelete";
            _txtStock.Name = "txtStock";
            _txtCode.Name = "txtCode";
        }

        private frmRecordsLists mListForm;
        private SqlDataAdapter daProduct = new SqlDataAdapter();
        private DataRow CurrentRow; // برای نگهداری رکورد جاری
        private SqlParameter[] prmUpdate = new SqlParameter[2];
        private SqlParameter prmDelete;
        //private short I;
        private bool IsValidLock = true;

        public frmRecordsLists ListForm
        {
            get
            {
                return mListForm;
            }

            set
            {
                mListForm = value;
            }
        }

        public DataSet dsProductionPlanning
        {
            get
            {
                return ListForm.dsProductionPlanning;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                IsValidLock = Module_UserAccess.Check_Lock();
            }
            catch (Exception ex)
            {
                Logger.LogException("", ex);
            }

            Module1.SetButtonsImage(cmdSave, 6);
            Module1.SetButtonsImage(cmdDelete, 9);
            Module1.SetButtonsImage(cmdExit, 14);
            FormLoad();
        }

        private void frmProduct_FormClosing(object sender, FormClosingEventArgs e)
        {
            dsProductionPlanning.RejectChanges();
            daProduct.Dispose();
            prmUpdate = null;
            prmDelete = null;
            mListForm = null;
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("محصول را حذف می کنید", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading, false) == DialogResult.Yes)
            {
                SqlTransaction trnDelete = null;
                try
                {
                    if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                        Module1.cnProductionPlanning.Open();
                    trnDelete = Module1.cnProductionPlanning.BeginTransaction();
                    daProduct.DeleteCommand.Transaction = trnDelete;
                    CurrentRow.Delete();
                    SaveChanges();
                    trnDelete.Commit();
                    Close();
                }
                catch (InvalidConstraintException ObjCnstEx)
                {
                    Logger.LogException("", ObjCnstEx);
                    dsProductionPlanning.RejectChanges();
                    trnDelete.Rollback();
                    MessageBox.Show("اشکال در حذف رکورد، برای حذف رکورد باید رکورد(های) مرتبط با آن حذف شوند", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                }
                catch (Exception objEx)
                {
                    dsProductionPlanning.RejectChanges();
                    trnDelete.Rollback();
                    Logger.SaveError(Name + ".CmdDelete_Click", objEx.Message);
                    string ErrorMsg = "اشکال در حذف رکورد، رکورد حذف نشد";
                    MessageBox.Show(ErrorMsg, Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                }
                finally
                {
                    trnDelete.Dispose();
                    if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                        Module1.cnProductionPlanning.Close();
                }
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!IsValidLock)
            {
                MessageBox.Show("قفل سخت افزاری شناسایی نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                Close();
                return;
            }

            if (!ValidationForm())
            {
                return;
            }

            switch (ListForm.FormMode)
            {
                case (int)Module1.FormModeEnum.INSERT_MODE:
                    {
                        DataRow drInsert;
                        SqlTransaction trnInsert = null;
                        try
                        {
                            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                                Module1.cnProductionPlanning.Open();
                            trnInsert = Module1.cnProductionPlanning.BeginTransaction();
                            daProduct.InsertCommand.Transaction = trnInsert;

                            // اضافه کردن رکورد جدید به جدول در دیتاست
                            drInsert = dsProductionPlanning.Tables["Tbl_Products"].NewRow();
                            drInsert["ProductCode"] = txtCode.Text;
                            drInsert["ProductName"] = txtName.Text;
                            drInsert["SafetyStock"] = txtStock.Value;
                            drInsert["TransferMinimumQuantity"] = txtTransferMinQty.Value;
                            dsProductionPlanning.Tables["Tbl_Products"].Rows.Add(drInsert);
                            SaveChanges();
                            trnInsert.Commit();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            dsProductionPlanning.RejectChanges();
                            trnInsert.Rollback();
                            MessageBox.Show("اشکال در ثبت رکورد جدید، رکورد جدید ثبت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            MessageBox.Show(objEx.Message);
                        }
                        finally
                        {
                            drInsert = null;
                            trnInsert.Dispose();
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                        }

                        break;
                    }

                case (int)Module1.FormModeEnum.EDIT_MODE:
                    {
                        SqlTransaction trnUpdate = null;
                        try
                        {
                            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                                Module1.cnProductionPlanning.Open();
                            trnUpdate = Module1.cnProductionPlanning.BeginTransaction();
                            daProduct.UpdateCommand.Transaction = trnUpdate;
                            CurrentRow.BeginEdit();
                            CurrentRow["ProductCode"] = txtCode.Text;
                            CurrentRow["ProductName"] = txtName.Text;
                            CurrentRow["SafetyStock"] = txtStock.Value;
                            CurrentRow["TransferMinimumQuantity"] = txtTransferMinQty.Value;
                            CurrentRow.EndEdit();
                            SaveChanges();
                            trnUpdate.Commit();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            CurrentRow.CancelEdit();
                            trnUpdate.Rollback();
                            MessageBox.Show("اشکال در اصلاح رکورد، تغییرات ثبت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            MessageBox.Show(objEx.Message);
                        }
                        finally
                        {
                            trnUpdate.Dispose();
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                        }

                        break;
                    }
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Controls_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Strings.Asc(e.KeyChar) == (int)Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void FormLoad()
        {
            try
            {
                CreateDataAdapterCommands();
                switch (ListForm.FormMode)
                {
                    case (int)Module1.FormModeEnum.INSERT_MODE: // در صورتیکه فرم برای ایجاد رکورد جدید فراخوانی شده باشد این قسمت اجرا خواهد شد
                        {
                            txtCode.Focus();
                            break;
                        }

                    case (int)Module1.FormModeEnum.EDIT_MODE:
                    case (int)Module1.FormModeEnum.DELETE_MODE: // در صورتیکه فرم برای ویرایش یا حذف رکورد جاری فراخوانی شده باشد این قسمت اجرا خواهد شد
                        {
                            CurrentRow = ListForm.GetRow();
                            // پر کردن کنترل فرم با مقدار رکورد جاری
                            txtCode.Text = Conversions.ToString(CurrentRow["ProductCode"]);
                            txtName.Text = Conversions.ToString(CurrentRow["ProductName"]);
                            txtStock.Value = Conversions.ToDecimal(CurrentRow["SafetyStock"]);
                            txtTransferMinQty.Value = Conversions.ToDecimal(CurrentRow["TransferMinimumQuantity"]);
                            switch (ListForm.FormMode) // در صورتیکه فرم برای ویرایش(اصلاح) رکورد جاری فراخوانی شده باشد این قسمت اجرا خواهد شد
                            {
                                case (int)Module1.FormModeEnum.EDIT_MODE:
                                    {
                                        txtCode.Focus();
                                        break;
                                    }

                                case (int)Module1.FormModeEnum.DELETE_MODE: // در صورتیکه فرم برای حذف رکورد جاری فراخوانی شده باشد این قسمت اجرا خواهد شد
                                    {
                                        cmdDelete.Focus();
                                        break;
                                    }
                            }

                            break;
                        }
                }
            }
            catch (Exception objEx)
            {
                MessageBox.Show("فراخوانی فرم با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(objEx.Message);
            }
        }

        private void SaveChanges()
        {
            DataSet dsChanges;
            dsChanges = dsProductionPlanning.GetChanges();
            if (dsChanges.HasErrors)
            {
                dsProductionPlanning.RejectChanges();
            }
            else
            {
                daProduct.Update(dsChanges, "Tbl_Products");
                dsProductionPlanning.AcceptChanges();
            }

            dsChanges = null;
        }

        private void CreateDataAdapterCommands()
        {
            // ایجاد دستور اضافه کردن رکورد جدید در جدول
            daProduct.InsertCommand = new SqlCommand("Insert Into Tbl_Products(ProductCode,ProductName,SafetyStock,TransferMinimumQuantity) Values(@ProductCode,@ProductName,@SafetyStock,@TransferMinimumQuantity)", Module1.cnProductionPlanning);
            {
                var withBlock = daProduct.InsertCommand;
                withBlock.Parameters.Add("@ProductCode", SqlDbType.VarChar, 50, "ProductCode");
                withBlock.Parameters.Add("@ProductName", SqlDbType.VarChar, 50, "ProductName");
                withBlock.Parameters.Add("@SafetyStock", SqlDbType.BigInt, default, "SafetyStock");
                withBlock.Parameters.Add("@TransferMinimumQuantity", SqlDbType.Int, default, "TransferMinimumQuantity");
            }
            // ایجاد دستور اصلاح رکورد جاری در جدول
            daProduct.UpdateCommand = new SqlCommand("Update Tbl_Products Set ProductCode=@CurrentProductCode,ProductName=@ProductName,SafetyStock=@SafetyStock,TransferMinimumQuantity=@TransferMinimumQuantity Where ProductCode=@OldProductCode", Module1.cnProductionPlanning);
            {
                var withBlock1 = daProduct.UpdateCommand;
                prmUpdate[0] = withBlock1.Parameters.Add("@CurrentProductCode", SqlDbType.VarChar, 50, "ProductCode");
                withBlock1.Parameters.Add("@ProductName", SqlDbType.VarChar, 50, "ProductName");
                withBlock1.Parameters.Add("@SafetyStock", SqlDbType.BigInt, default, "SafetyStock");
                withBlock1.Parameters.Add("@TransferMinimumQuantity", SqlDbType.Int, default, "TransferMinimumQuantity");
                prmUpdate[1] = withBlock1.Parameters.Add("@OldProductCode", SqlDbType.VarChar, 50, "ProductCode");
            }

            prmUpdate[0].Direction = ParameterDirection.Input;
            prmUpdate[0].SourceVersion = DataRowVersion.Current;
            prmUpdate[1].Direction = ParameterDirection.Input;
            prmUpdate[1].SourceVersion = DataRowVersion.Original;
            // ایجاد دستور حذف رکورد جاری در جدول
            daProduct.DeleteCommand = new SqlCommand("Delete From Tbl_Products Where ProductCode=@ProductCode", Module1.cnProductionPlanning);
            {
                var withBlock2 = daProduct.DeleteCommand;
                prmDelete = withBlock2.Parameters.Add("@ProductCode", SqlDbType.VarChar, 50, "ProductCode");
            }

            prmDelete.Direction = ParameterDirection.Input;
            prmDelete.SourceVersion = DataRowVersion.Original;
        }

        private bool ValidationForm()
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                MessageBox.Show("کد محصول را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtCode.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("نام محصول را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtName.Focus();
                return false;
            }

            return true;
        }
    }
}