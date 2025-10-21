using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmSupplier
    {
        public frmSupplier()
        {
            InitializeComponent();
            _cmdSave.Name = "cmdSave";
            _cmdExit.Name = "cmdExit";
            _cmdDelete.Name = "cmdDelete";
        }

        private frmRecordsLists mListForm;
        private SqlDataAdapter daSupplier = new SqlDataAdapter();
        private DataRow CurrentRow; // برای نگهداری رکورد جاری
        //private short I;

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

        private void frmSupplier_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdSave, 6);
            Module1.SetButtonsImage(cmdDelete, 9);
            Module1.SetButtonsImage(cmdExit, 14);
            FormLoad();
        }

        private void frmSupplier_FormClosing(object sender, FormClosingEventArgs e)
        {
            dsProductionPlanning.RejectChanges();
            daSupplier.Dispose();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("مشخصات تامین کننده را حذف می کنید", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading, false) == DialogResult.Yes)
            {
                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();
                var trnDelete = Module1.cnProductionPlanning.BeginTransaction();
                try
                {
                    daSupplier.DeleteCommand.Transaction = trnDelete;
                    CurrentRow.Delete();
                    SaveChanges();
                    trnDelete.Commit();
                    Close();
                }
                catch (Exception objEx)
                {
                    dsProductionPlanning.RejectChanges();
                    trnDelete.Rollback();
                    Logger.SaveError(Name + ".cmdDelete_Click", objEx.Message);
                    MessageBox.Show("حذف تامین کننده با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
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
            if (!FormValidation())
            {
                return;
            }

            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                Module1.cnProductionPlanning.Open();
            var trnSupplier = Module1.cnProductionPlanning.BeginTransaction();
            switch (ListForm.FormMode)
            {
                case (int)Module1.FormModeEnum.INSERT_MODE:
                    {
                        try
                        {
                            daSupplier.InsertCommand.Transaction = trnSupplier;
                            int NewCode = (int)Module1.GetNewCode("Tbl_Suppliers", "SupplierCode");
                            CurrentRow = dsProductionPlanning.Tables["Tbl_Suppliers"].NewRow();
                            CurrentRow["SupplierCode"] = NewCode;
                            CurrentRow["SupplierName"] = txtSupplierName.Text;
                            CurrentRow["Address"] = txtAddress.Text;
                            CurrentRow["Phone"] = txtPhone.Text;
                            CurrentRow["ActivationContext"] = txtActivityContext.Text;
                            CurrentRow["Description"] = txtDescription.Text;
                            dsProductionPlanning.Tables["Tbl_Suppliers"].Rows.Add(CurrentRow);
                            SaveChanges();
                            trnSupplier.Commit();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            dsProductionPlanning.RejectChanges();
                            trnSupplier.Rollback();
                            Logger.SaveError(Name + ".cmdSave_Click", objEx.Message);
                            MessageBox.Show("ثبت تامین کنندۀ جدید با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        }
                        finally
                        {
                            trnSupplier.Dispose();
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                        }

                        break;
                    }

                case (int)Module1.FormModeEnum.EDIT_MODE:
                    {
                        try
                        {
                            daSupplier.UpdateCommand.Transaction = trnSupplier;
                            CurrentRow.BeginEdit();
                            CurrentRow["SupplierName"] = txtSupplierName.Text;
                            CurrentRow["Address"] = txtAddress.Text;
                            CurrentRow["Phone"] = txtPhone.Text;
                            CurrentRow["ActivationContext"] = txtActivityContext.Text;
                            CurrentRow["Description"] = txtDescription.Text;
                            CurrentRow.EndEdit();
                            SaveChanges();
                            trnSupplier.Commit();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            CurrentRow.CancelEdit();
                            trnSupplier.Rollback();
                            Logger.SaveError(Name + ".cmdSave_Click", objEx.Message);
                            MessageBox.Show("ثبت تغییرات با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        }
                        finally
                        {
                            trnSupplier.Dispose();
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                        }

                        break;
                    }
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormLoad()
        {
            try
            {
                CreateDataAdapterCommands();
                switch (ListForm.FormMode)
                {
                    case (int)Module1.FormModeEnum.INSERT_MODE:
                        {
                            txtSupplierName.Focus();
                            break;
                        }

                    case (int)Module1.FormModeEnum.EDIT_MODE:
                    case (int)Module1.FormModeEnum.DELETE_MODE:
                        {
                            CurrentRow = ListForm.GetRow();
                            // پر کردن کنترل فرم با مقدار رکورد جاری
                            txtSupplierName.Text = Conversions.ToString(CurrentRow["SupplierName"]);
                            txtAddress.Text = Conversions.ToString(CurrentRow["Address"]);
                            txtPhone.Text = Conversions.ToString(CurrentRow["Phone"]);
                            txtActivityContext.Text = Conversions.ToString(CurrentRow["ActivationContext"]);
                            txtDescription.Text = Conversions.ToString(CurrentRow["Description"]);
                            switch (ListForm.FormMode)
                            {
                                case (int)Module1.FormModeEnum.EDIT_MODE:
                                    {
                                        txtSupplierName.Focus();
                                        break;
                                    }

                                case (int)Module1.FormModeEnum.DELETE_MODE:
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
                Logger.SaveError(Name + ".FormLoad", objEx.Message);
                MessageBox.Show("فراخوانی فرم با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
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
                daSupplier.Update(dsChanges, "Tbl_Suppliers");
                dsProductionPlanning.AcceptChanges();
            }

            dsChanges = null;
        }

        private void CreateDataAdapterCommands()
        {
            // ایجاد دستور اضافه کردن رکورد جدید در جدول
            daSupplier.InsertCommand = new SqlCommand("Insert Into Tbl_Suppliers(SupplierCode,SupplierName,Address,Phone,ActivationContext,Description) Values(@SupplierCode,@SupplierName,@Address,@Phone,@ActivationContext,@Description)", Module1.cnProductionPlanning);
            {
                var withBlock = daSupplier.InsertCommand;
                withBlock.Parameters.Add("@SupplierCode", SqlDbType.Int, default, "SupplierCode");
                withBlock.Parameters.Add("@SupplierName", SqlDbType.VarChar, 50, "SupplierName");
                withBlock.Parameters.Add("@Address", SqlDbType.VarChar, 255, "Address");
                withBlock.Parameters.Add("@Phone", SqlDbType.VarChar, 50, "Phone");
                withBlock.Parameters.Add("@ActivationContext", SqlDbType.VarChar, 50, "ActivationContext");
                withBlock.Parameters.Add("@Description", SqlDbType.VarChar, 255, "Description");
            }

            // ایجاد دستور اصلاح رکورد جاری در جدول
            daSupplier.UpdateCommand = new SqlCommand("Update Tbl_Suppliers Set SupplierName=@SupplierName,Address=@Address,Phone=@Phone,ActivationContext=@ActivationContext,Description=@Description Where SupplierCode=@SupplierCode", Module1.cnProductionPlanning);
            {
                var withBlock1 = daSupplier.UpdateCommand;
                withBlock1.Parameters.Add("@SupplierName", SqlDbType.VarChar, 50, "SupplierName");
                withBlock1.Parameters.Add("@Address", SqlDbType.VarChar, 255, "Address");
                withBlock1.Parameters.Add("@Phone", SqlDbType.VarChar, 50, "Phone");
                withBlock1.Parameters.Add("@ActivationContext", SqlDbType.VarChar, 50, "ActivationContext");
                withBlock1.Parameters.Add("@Description", SqlDbType.VarChar, 255, "Description");
                withBlock1.Parameters.Add("@SupplierCode", SqlDbType.Int, default, "SupplierCode").SourceVersion = DataRowVersion.Original;
            }

            // ایجاد دستور حذف رکورد جاری در جدول
            daSupplier.DeleteCommand = new SqlCommand("Delete From Tbl_Suppliers Where SupplierCode=@SupplierCode", Module1.cnProductionPlanning);
            {
                var withBlock2 = daSupplier.DeleteCommand;
                withBlock2.Parameters.Add("@SupplierCode", SqlDbType.Int, default, "SupplierCode").SourceVersion = DataRowVersion.Original;
            }
        }

        private bool FormValidation()
        {
            if (string.IsNullOrEmpty(txtSupplierName.Text))
            {
                MessageBox.Show("نام تامین کننده را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtSupplierName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtAddress.Text) || txtAddress.Text.Trim().Equals(""))
            {
                txtAddress.Text = "-";
            }

            if (string.IsNullOrEmpty(txtPhone.Text) || txtPhone.Text.Trim().Equals(""))
            {
                txtPhone.Text = "-";
            }

            if (string.IsNullOrEmpty(txtActivityContext.Text) || txtActivityContext.Text.Trim().Equals(""))
            {
                txtActivityContext.Text = "-";
            }

            if (string.IsNullOrEmpty(txtDescription.Text) || txtDescription.Text.Trim().Equals(""))
            {
                txtDescription.Text = "-";
            }

            return true;
        }
    }
}