using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmContractor
    {
        public frmContractor()
        {
            InitializeComponent();
            _cmdSave.Name = "cmdSave";
            _cmdExit.Name = "cmdExit";
            _cmdDelete.Name = "cmdDelete";
        }

        private frmRecordsLists mListForm;
        private SqlDataAdapter daContractor = new SqlDataAdapter();
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

        private void Form1_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdSave, 6);
            Module1.SetButtonsImage(cmdDelete, 9);
            Module1.SetButtonsImage(cmdExit, 14);
            FormLoad();
        }

        private void frmContractor_FormClosing(object sender, FormClosingEventArgs e)
        {
            dsProductionPlanning.RejectChanges();
            daContractor.Dispose();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("مشخصات پیمانکار را حذف می کنید", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading, false) == DialogResult.Yes)
            {
                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();
                var trnDelete = Module1.cnProductionPlanning.BeginTransaction();
                try
                {
                    daContractor.DeleteCommand.Transaction = trnDelete;
                    CurrentRow.Delete();
                    SaveChanges();
                    trnDelete.Commit();
                    Close();
                }
                catch (InvalidConstraintException ObjCnstEx)
                {
                    dsProductionPlanning.RejectChanges();
                    trnDelete.Rollback();
                    Logger.LogException("cmdDelete_Click", ObjCnstEx);
                    MessageBox.Show("اشکال در حذف رکورد، برای حذف رکورد باید رکورد(های) مرتبط با آن حذف شوند", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                }
                catch (Exception objEx)
                {
                    dsProductionPlanning.RejectChanges();
                    trnDelete.Rollback();
                    Logger.SaveError(Name + ".cmdDelete_Click", objEx.Message);
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
            if (!FormValidation())
            {
                return;
            }

            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                Module1.cnProductionPlanning.Open();
            var trnContractor = Module1.cnProductionPlanning.BeginTransaction();
            switch (ListForm.FormMode)
            {
                case (int)Module1.FormModeEnum.INSERT_MODE:
                    {
                        try
                        {
                            string mCode = Module1.GetNewCode("Tbl_Contractors", "ContractorCode").ToString();
                            daContractor.InsertCommand.Transaction = trnContractor;
                            CurrentRow = dsProductionPlanning.Tables["Tbl_Contractors"].NewRow();
                            CurrentRow["ContractorCode"] = mCode;
                            CurrentRow["ContractorName"] = txtName.Text;
                            CurrentRow["Address"] = txtAddress.Text;
                            CurrentRow["Phone"] = txtPhone.Text;
                            CurrentRow["ActivationContext"] = txtActivityContext.Text;
                            CurrentRow["Description"] = txtDescription.Text;
                            dsProductionPlanning.Tables["Tbl_Contractors"].Rows.Add(CurrentRow);
                            SaveChanges();
                            trnContractor.Commit();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            dsProductionPlanning.RejectChanges();
                            trnContractor.Rollback();
                            Logger.SaveError(Name + ".cmdSave_Click", objEx.Message);
                            MessageBox.Show("اشکال در ثبت رکورد جدید، رکورد جدید ثبت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            trnContractor.Dispose();
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                        }

                        break;
                    }

                case (int)Module1.FormModeEnum.EDIT_MODE:
                    {
                        try
                        {
                            daContractor.UpdateCommand.Transaction = trnContractor;
                            CurrentRow.BeginEdit();
                            CurrentRow["ContractorName"] = txtName.Text;
                            CurrentRow["Address"] = txtAddress.Text;
                            CurrentRow["Phone"] = txtPhone.Text;
                            CurrentRow["ActivationContext"] = txtActivityContext.Text;
                            CurrentRow["Description"] = txtDescription.Text;
                            CurrentRow.EndEdit();
                            SaveChanges();
                            trnContractor.Commit();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            CurrentRow.CancelEdit();
                            trnContractor.Rollback();
                            Logger.SaveError(Name + ".cmdSave_Click", objEx.Message);
                            MessageBox.Show("اشکال در اصلاح رکورد، تغییرات ثبت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            trnContractor.Dispose();
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
                    case (int)Module1.FormModeEnum.INSERT_MODE: // در صورتیکه فرم برای ایجاد رکورد جدید فراخوانی شده باشد این قسمت اجرا خواهد شد
                        {
                            txtName.Focus();
                            break;
                        }

                    case (int)Module1.FormModeEnum.EDIT_MODE:
                    case (int)Module1.FormModeEnum.DELETE_MODE: // در صورتیکه فرم برای ویرایش یا حذف رکورد جاری فراخوانی شده باشد این قسمت اجرا خواهد شد
                        {
                            CurrentRow = ListForm.GetRow();
                            // پر کردن کنترل فرم با مقدار رکورد جاری
                            txtName.Text = Conversions.ToString(CurrentRow["ContractorName"]);
                            txtAddress.Text = Conversions.ToString(CurrentRow["Address"]);
                            txtPhone.Text = Conversions.ToString(CurrentRow["Phone"]);
                            txtActivityContext.Text = Conversions.ToString(CurrentRow["ActivationContext"]);
                            txtDescription.Text = Conversions.ToString(CurrentRow["Description"]);
                            switch (ListForm.FormMode) // در صورتیکه فرم برای ویرایش(اصلاح) رکورد جاری فراخوانی شده باشد این قسمت اجرا خواهد شد
                            {
                                case (int)Module1.FormModeEnum.EDIT_MODE:
                                    {
                                        txtName.Focus();
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
                daContractor.Update(dsChanges, "Tbl_Contractors");
                dsProductionPlanning.AcceptChanges();
            }

            dsChanges = null;
        }

        private void CreateDataAdapterCommands()
        {
            // ایجاد دستور اضافه کردن رکورد جدید در جدول
            daContractor.InsertCommand = new SqlCommand("Insert Into Tbl_Contractors(ContractorCode,ContractorName,Address,Phone,ActivationContext,Description) Values(@ContractorCode,@ContractorName,@Address,@Phone,@ActivationContext,@Description)", Module1.cnProductionPlanning);
            {
                var withBlock = daContractor.InsertCommand;
                withBlock.Parameters.Add("@ContractorCode", SqlDbType.Int, default, "ContractorCode");
                withBlock.Parameters.Add("@ContractorName", SqlDbType.VarChar, 50, "ContractorName");
                withBlock.Parameters.Add("@Address", SqlDbType.VarChar, 255, "Address");
                withBlock.Parameters.Add("@Phone", SqlDbType.VarChar, 50, "Phone");
                withBlock.Parameters.Add("@ActivationContext", SqlDbType.VarChar, 50, "ActivationContext");
                withBlock.Parameters.Add("@Description", SqlDbType.VarChar, 255, "Description");
            }
            // ایجاد دستور اصلاح رکورد جاری در جدول
            daContractor.UpdateCommand = new SqlCommand("Update Tbl_Contractors Set ContractorName=@ContractorName,Address=@Address,Phone=@Phone,ActivationContext=@ActivationContext,Description=@Description Where ContractorCode=@ContractorCode", Module1.cnProductionPlanning);
            {
                var withBlock1 = daContractor.UpdateCommand;
                withBlock1.Parameters.Add("@ContractorName", SqlDbType.VarChar, 50, "ContractorName");
                withBlock1.Parameters.Add("@Address", SqlDbType.VarChar, 255, "Address");
                withBlock1.Parameters.Add("@Phone", SqlDbType.VarChar, 50, "Phone");
                withBlock1.Parameters.Add("@ActivationContext", SqlDbType.VarChar, 50, "ActivationContext");
                withBlock1.Parameters.Add("@Description", SqlDbType.VarChar, 255, "Description");
                withBlock1.Parameters.Add("@ContractorCode", SqlDbType.Int, default, "ContractorCode").SourceVersion = DataRowVersion.Original;
            }
            // ایجاد دستور حذف رکورد جاری در جدول
            daContractor.DeleteCommand = new SqlCommand("Delete From Tbl_Contractors Where ContractorCode=@ContractorCode", Module1.cnProductionPlanning);
            {
                var withBlock2 = daContractor.DeleteCommand;
                withBlock2.Parameters.Add("@ContractorCode", SqlDbType.Int, default, "ContractorCode").SourceVersion = DataRowVersion.Original;
            }
        }

        private bool FormValidation()
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("نام پیمانکار را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtActivityContext.Text))
            {
                MessageBox.Show("زمینه فعالیت پیمانکار را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtActivityContext.Focus();
                return false;
            }

            return true;
        }
    }
}