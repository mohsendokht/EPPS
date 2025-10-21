using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmCustomer
    {
        public frmCustomer()
        {
            InitializeComponent();
            _cmdSave.Name = "cmdSave";
            _cmdExit.Name = "cmdExit";
            _cmdDelete.Name = "cmdDelete";
        }

        private frmRecordsLists mListForm;
        private SqlDataAdapter daCustomer = new SqlDataAdapter();
        private DataRow CurrentRow; // برای نگهداری رکورد جاری

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

        private void frmCustomer_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdSave, 6);
            Module1.SetButtonsImage(cmdDelete, 9);
            Module1.SetButtonsImage(cmdExit, 14);
            FormLoad();
        }

        private void frmCustomer_FormClosing(object sender, FormClosingEventArgs e)
        {
            dsProductionPlanning.RejectChanges();
            daCustomer.Dispose();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("مشخصات مشتری را حذف می کنید", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading, false) == DialogResult.Yes)
            {
                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();
                var cmDelete = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Select Count(*) From Tbl_CustomerOrders Where CustomerCode = '", CurrentRow["CustomerCode"]), "'")), Module1.cnProductionPlanning);
                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(cmDelete.ExecuteScalar(), 0, false)))
                {
                    if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                        Module1.cnProductionPlanning.Close();
                    MessageBox.Show("مشتری مورد نظر دارای سفارش بوده و قابلیت حذف ندارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return;
                }

                var trnDelete = Module1.cnProductionPlanning.BeginTransaction();
                try
                {
                    daCustomer.DeleteCommand.Transaction = trnDelete;
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
                    Logger.SaveError(Name + ".cmdDelete_Click", objEx.Message);
                    MessageBox.Show("حذف مشخصات مشتری با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
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
            var trnSave = Module1.cnProductionPlanning.BeginTransaction();
            switch (ListForm.FormMode)
            {
                case (int)Module1.FormModeEnum.INSERT_MODE:
                    {
                        try
                        {
                            daCustomer.InsertCommand.Transaction = trnSave;
                            CurrentRow = dsProductionPlanning.Tables["Tbl_Customers"].NewRow();
                            CurrentRow["CustomerCode"] = txtCode.Text;
                            CurrentRow["CustomerName"] = txtName.Text;
                            CurrentRow["Address"] = txtAddress.Text;
                            CurrentRow["Phone"] = txtPhone.Text;
                            CurrentRow["Fax"] = txtFax.Text;
                            CurrentRow["WebSite"] = txtWebSite.Text;
                            CurrentRow["EMail"] = txtEMail.Text;
                            CurrentRow["Description"] = txtDescription.Text;
                            dsProductionPlanning.Tables["Tbl_Customers"].Rows.Add(CurrentRow);
                            SaveChanges();
                            trnSave.Commit();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            dsProductionPlanning.RejectChanges();
                            trnSave.Rollback();
                            Logger.SaveError(Name + ".cmdSave_Click", objEx.Message);
                            MessageBox.Show("ثبت مشتری جدید با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            trnSave.Dispose();
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                        }

                        break;
                    }

                case (int)Module1.FormModeEnum.EDIT_MODE:
                    {
                        try
                        {
                            daCustomer.UpdateCommand.Transaction = trnSave;
                            CurrentRow.BeginEdit();
                            CurrentRow["CustomerCode"] = txtCode.Text;
                            CurrentRow["CustomerName"] = txtName.Text;
                            CurrentRow["Address"] = txtAddress.Text;
                            CurrentRow["Phone"] = txtPhone.Text;
                            CurrentRow["Fax"] = txtFax.Text;
                            CurrentRow["WebSite"] = txtWebSite.Text;
                            CurrentRow["EMail"] = txtEMail.Text;
                            CurrentRow["Description"] = txtDescription.Text;
                            CurrentRow.EndEdit();
                            SaveChanges();
                            trnSave.Commit();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            CurrentRow.CancelEdit();
                            trnSave.Rollback();
                            Logger.SaveError(Name + ".cmdSave_Click", objEx.Message);
                            MessageBox.Show("ثبت تغییرات با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            MessageBox.Show(objEx.Message);
                        }
                        finally
                        {
                            trnSave.Dispose();
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
                            txtName.Focus();
                            break;
                        }

                    case (int)Module1.FormModeEnum.EDIT_MODE:
                    case (int)Module1.FormModeEnum.DELETE_MODE:
                        {
                            CurrentRow = ListForm.GetRow();
                            // پر کردن کنترل فرم با مقدار رکورد جاری
                            txtCode.Text = Conversions.ToString(CurrentRow["CustomerCode"]);
                            txtName.Text = Conversions.ToString(CurrentRow["CustomerName"]);
                            txtAddress.Text = Conversions.ToString(CurrentRow["Address"]);
                            txtPhone.Text = Conversions.ToString(CurrentRow["Phone"]);
                            txtFax.Text = Conversions.ToString(CurrentRow["Fax"]);
                            txtWebSite.Text = Conversions.ToString(CurrentRow["WebSite"]);
                            txtEMail.Text = Conversions.ToString(CurrentRow["EMail"]);
                            txtDescription.Text = Conversions.ToString(CurrentRow["Description"]);
                            switch (ListForm.FormMode)
                            {
                                case (int)Module1.FormModeEnum.EDIT_MODE:
                                    {
                                        txtName.Focus();
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
                MessageBox.Show("فراخوانی فرم مشخصات مشتری با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                daCustomer.Update(dsChanges, "Tbl_Customers");
                dsProductionPlanning.AcceptChanges();
            }

            dsChanges = null;
        }

        private void CreateDataAdapterCommands()
        {
            // ایجاد دستور اضافه کردن رکورد جدید در جدول
            daCustomer.InsertCommand = new SqlCommand("Insert Into Tbl_Customers(CustomerCode,CustomerName,Address,Phone,Fax,WebSite,EMail,Description)" + "Values(@CustomerCode,@CustomerName,@Address,@Phone,@Fax,@WebSite,@EMail,@Description)", Module1.cnProductionPlanning);
            {
                var withBlock = daCustomer.InsertCommand;
                withBlock.Parameters.Add("@CustomerCode", SqlDbType.VarChar, 50, "CustomerCode");
                withBlock.Parameters.Add("@CustomerName", SqlDbType.VarChar, 100, "CustomerName");
                withBlock.Parameters.Add("@Address", SqlDbType.VarChar, 300, "Address");
                withBlock.Parameters.Add("@Phone", SqlDbType.VarChar, 50, "Phone");
                withBlock.Parameters.Add("@Fax", SqlDbType.VarChar, 50, "Fax");
                withBlock.Parameters.Add("@WebSite", SqlDbType.VarChar, 50, "WebSite");
                withBlock.Parameters.Add("@EMail", SqlDbType.VarChar, 50, "EMail");
                withBlock.Parameters.Add("@Description", SqlDbType.VarChar, 300, "Description");
            }
            // ایجاد دستور اصلاح رکورد جاری در جدول
            daCustomer.UpdateCommand = new SqlCommand("Update Tbl_Customers Set CustomerCode=@NewCustomerCode,CustomerName=@CustomerName,Address=@Address,Phone=@Phone," + "Fax=@Fax,WebSite=@WebSite, EMail=@EMail, Description=@Description Where CustomerCode=@OldCustomerCode", Module1.cnProductionPlanning);
            {
                var withBlock1 = daCustomer.UpdateCommand;
                withBlock1.Parameters.Add("@NewCustomerCode", SqlDbType.VarChar, 50, "CustomerCode").SourceVersion = DataRowVersion.Current;
                withBlock1.Parameters.Add("@CustomerName", SqlDbType.VarChar, 100, "CustomerName");
                withBlock1.Parameters.Add("@Address", SqlDbType.VarChar, 300, "Address");
                withBlock1.Parameters.Add("@Phone", SqlDbType.VarChar, 50, "Phone");
                withBlock1.Parameters.Add("@Fax", SqlDbType.VarChar, 50, "Fax");
                withBlock1.Parameters.Add("@WebSite", SqlDbType.VarChar, 50, "WebSite");
                withBlock1.Parameters.Add("@EMail", SqlDbType.VarChar, 50, "EMail");
                withBlock1.Parameters.Add("@Description", SqlDbType.VarChar, 300, "Description");
                withBlock1.Parameters.Add("@OldCustomerCode", SqlDbType.VarChar, 50, "CustomerCode").SourceVersion = DataRowVersion.Original;
            }
            // ایجاد دستور حذف رکورد جاری در جدول
            daCustomer.DeleteCommand = new SqlCommand("Delete From Tbl_Customers Where CustomerCode=@CustomerCode", Module1.cnProductionPlanning);
            {
                var withBlock2 = daCustomer.DeleteCommand;
                withBlock2.Parameters.Add("@CustomerCode", SqlDbType.VarChar, 50, "CustomerCode").SourceVersion = DataRowVersion.Original;
            }
        }

        private bool FormValidation()
        {
            if (string.IsNullOrEmpty(txtCode.Text) || txtCode.Text.Trim().Equals(""))
            {
                MessageBox.Show("کد مشتری را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtCode.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtName.Text) || txtName.Text.Trim().Equals(""))
            {
                MessageBox.Show("نام مشتری را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtName.Focus();
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

            if (string.IsNullOrEmpty(txtFax.Text) || txtFax.Text.Trim().Equals(""))
            {
                txtFax.Text = "-";
            }

            if (string.IsNullOrEmpty(txtWebSite.Text) || txtWebSite.Text.Trim().Equals(""))
            {
                txtWebSite.Text = "-";
            }

            if (string.IsNullOrEmpty(txtEMail.Text) || txtEMail.Text.Trim().Equals(""))
            {
                txtEMail.Text = "-";
            }

            if (string.IsNullOrEmpty(txtDescription.Text) || txtDescription.Text.Trim().Equals(""))
            {
                txtDescription.Text = "-";
            }

            return true;
        }
    }
}