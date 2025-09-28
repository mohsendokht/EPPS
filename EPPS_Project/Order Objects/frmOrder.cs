using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmOrder
    {
        public frmOrder()
        {
            InitializeComponent();
            _cmdSave.Name = "cmdSave";
            _cmdExit.Name = "cmdExit";
            _cmdDelete.Name = "cmdDelete";
            _chkConfirmed.Name = "chkConfirmed";
            _txtRegisterDate.Name = "txtRegisterDate";
            _txtOrderNo.Name = "txtOrderNo";
            //_cbCustomer.Name = "cbCustomer";
            //_cbProduct.Name = "cbProduct";
            _txtOrderDate.Name = "txtOrderDate";
            _txtProductTechnicalSpecification.Name = "txtProductTechnicalSpecification";
            _txtQuantity.Name = "txtQuantity";
            _txtDeliveryDate.Name = "txtDeliveryDate";
            _txtContractNo.Name = "txtContractNo";
            _chkAllowed.Name = "chkAllowed";
            _txtProductionCallDate.Name = "txtProductionCallDate";
        }

        private frmOrdersList mListForm;
        private DataSetConfiguration DataSetConfig = new DataSetConfiguration();
        private SqlDataAdapter mdaOrder = new SqlDataAdapter();
        private DataRow mCurrentRow; // برای نگهداری رکورد جاری
        private DataView dvCustomers;
        private DataView dvProducts;

        public frmOrdersList ListForm
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

        private void frmRealProduction_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdSave, 6);
            Module1.SetButtonsImage(cmdDelete, 9);
            Module1.SetButtonsImage(cmdExit, 14);
            FormLoad();
            chkConfirmed.Visible = Module_UserAccess.HaveAccessToItem(83);
            gbAllowed.Visible = Module_UserAccess.HaveAccessToItem(84);
        }

        private void frmRealProduction_FormClosing(object sender, FormClosingEventArgs e)
        {
            dsProductionPlanning.RejectChanges();
            for (int I = dsProductionPlanning.Tables.Count - 1; I >= 3; I -= 1)
            {
                dsProductionPlanning.Tables[I].Dispose();
                dsProductionPlanning.Tables.RemoveAt(I);
            }

            DataSetConfig = null;
            mdaOrder.Dispose();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("مشخصات سفارش مشتری را حذف می کنید", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading, false) == DialogResult.Yes)
            {
                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();
                var trnDelete = Module1.cnProductionPlanning.BeginTransaction();
                try
                {
                    mdaOrder.DeleteCommand.Transaction = trnDelete;
                    var cmCheckBatchExist = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject("Select Count(*) From Tbl_Batch_Order Where OrderIndex = ", mCurrentRow["OrderIndex"])), Module1.cnProductionPlanning);
                    cmCheckBatchExist.Transaction = trnDelete;
                    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(cmCheckBatchExist.ExecuteScalar().ToString(), 0, false)))
                    {
                        if (MessageBox.Show("سفارش مورد نظر در ارتباط با بچ(های) تعریف شده می باشد، آیا برای حذف سفارش مطمئن هستید", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading) == DialogResult.No)
                        {
                            trnDelete.Commit();
                            //break;
                        }
                        else
                        {
                            cmCheckBatchExist.CommandText = Conversions.ToString(Operators.ConcatenateObject("Delete From Tbl_Batch_Order Where OrderIndex = ", mCurrentRow["OrderIndex"]));
                            cmCheckBatchExist.ExecuteNonQuery();
                        }
                    }

                    mCurrentRow.Delete();
                    SaveChanges();
                    trnDelete.Commit();
                    Close();
                }
                catch (InvalidConstraintException ObjCnstEx)
                {
                    Logger.LogException("cmdDelete_Click", ObjCnstEx);
                    dsProductionPlanning.RejectChanges();
                    trnDelete.Rollback();
                    MessageBox.Show("برای حذف رکورد باید رکورد(های) مرتبط با آن حذف شوند", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                }
                catch (Exception objEx)
                {
                    dsProductionPlanning.RejectChanges();
                    trnDelete.Rollback();
                    Logger.SaveError(Name + ".cmdDelete_Click", objEx.Message);
                    MessageBox.Show("حذف سفارش با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            var trnOrder = Module1.cnProductionPlanning.BeginTransaction();
            switch (ListForm.FormMode)
            {
                case (int)Module1.FormModeEnum.INSERT_MODE:
                    {
                        try
                        {
                            mdaOrder.InsertCommand.Transaction = trnOrder;
                            mCurrentRow = dsProductionPlanning.Tables["Tbl_CustomerOrders"].NewRow();
                            mCurrentRow["OrderIndex"] = Module1.GetNewCode("Tbl_CustomerOrders", "OrderIndex");
                            mCurrentRow["RegisterDate"] = txtRegisterDate.Text;
                            mCurrentRow["OrderNo"] = txtOrderNo.Text;
                            mCurrentRow["OrderDate"] = txtOrderDate.Text;
                            mCurrentRow["CustomerCode"] = Customer_LookUp.CB_SelectedValue;
                            mCurrentRow["CustomerName"] = Customer_LookUp.CB_SelectedDescription;
                            mCurrentRow["ProductCode"] = Product_Lookup.CB_SelectedValue;
                            mCurrentRow["ProductName"] = Product_Lookup.CB_SelectedDescription;
                            mCurrentRow["ProductTechnicalSpecification"] = txtProductTechnicalSpecification.Text;
                            mCurrentRow["OrderQuantity"] = txtQuantity.Value;
                            mCurrentRow["DeliveryDueDate"] = txtDeliveryDate.Text;
                            mCurrentRow["ContractNo"] = txtContractNo.Text;
                            mCurrentRow["ConfirmFlag"] = Interaction.IIf(chkConfirmed.Checked, 1, 0);
                            mCurrentRow["ConfirmFlagTitle"] = Interaction.IIf(chkConfirmed.Checked, "تایید شده", "تایید نشده");
                            mCurrentRow["AllowFlag"] = Interaction.IIf(chkAllowed.Checked, 1, 0);
                            mCurrentRow["AllowFlagTitle"] = Interaction.IIf(chkAllowed.Checked, "تصویب شده", "تصویب نشده");
                            mCurrentRow["ProductionCallDate"] = Interaction.IIf(chkConfirmed.Checked && chkAllowed.Checked, txtProductionCallDate.Text, "0");
                            mCurrentRow["IsDone"] = 0;
                            dsProductionPlanning.Tables["Tbl_CustomerOrders"].Rows.Add(mCurrentRow);
                            SaveChanges();
                            trnOrder.Commit();
                            dsProductionPlanning.AcceptChanges();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            dsProductionPlanning.RejectChanges();
                            trnOrder.Rollback();
                            Logger.SaveError(Name + ".cmdSave_Click", objEx.Message);
                            MessageBox.Show("ثبت سفارش جدید با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                            trnOrder.Dispose();
                        }

                        break;
                    }

                case (int)Module1.FormModeEnum.EDIT_MODE:
                    {
                        try
                        {
                            mdaOrder.UpdateCommand.Transaction = trnOrder;
                            mCurrentRow.BeginEdit();
                            mCurrentRow["RegisterDate"] = txtRegisterDate.Text;
                            mCurrentRow["OrderNo"] = txtOrderNo.Text;
                            mCurrentRow["OrderDate"] = txtOrderDate.Text;
                            mCurrentRow["CustomerCode"] = Customer_LookUp.CB_SelectedValue;
                            mCurrentRow["CustomerName"] = Customer_LookUp.CB_SelectedDescription;
                            mCurrentRow["ProductCode"] = Product_Lookup.CB_SelectedValue;
                            mCurrentRow["ProductName"] = Product_Lookup.CB_SelectedDescription;
                            mCurrentRow["ProductTechnicalSpecification"] = txtProductTechnicalSpecification.Text;
                            mCurrentRow["OrderQuantity"] = txtQuantity.Value;
                            mCurrentRow["DeliveryDueDate"] = txtDeliveryDate.Text;
                            mCurrentRow["ContractNo"] = txtContractNo.Text;
                            mCurrentRow["ConfirmFlag"] = Interaction.IIf(chkConfirmed.Checked, 1, 0);
                            mCurrentRow["ConfirmFlagTitle"] = Interaction.IIf(chkConfirmed.Checked, "تایید شده", "تایید نشده");
                            mCurrentRow["AllowFlag"] = Interaction.IIf(chkAllowed.Checked, 1, 0);
                            mCurrentRow["AllowFlagTitle"] = Interaction.IIf(chkAllowed.Checked, "تصویب شده", "تصویب نشده");
                            mCurrentRow["ProductionCallDate"] = Interaction.IIf(chkConfirmed.Checked && chkAllowed.Checked, txtProductionCallDate.Text, "0");
                            // mCurrentRow("IsDone") = 0
                            mCurrentRow.EndEdit();
                            SaveChanges();
                            trnOrder.Commit();
                            dsProductionPlanning.AcceptChanges();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            mCurrentRow.CancelEdit();
                            trnOrder.Rollback();
                            Logger.SaveError(Name + ".cmdSave_Click", objEx.Message);
                            MessageBox.Show("ثبت تغییرات سفارش با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                        }
                        finally
                        {
                            trnOrder.Dispose();
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                        }

                        break;
                    }
            }
        }

        private void chkConfirmed_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConfirmed.Checked)
            {
                gbAllowed.Enabled = true;
            }
            else
            {
                gbAllowed.Enabled = false;
                chkAllowed.Checked = false;
            }
        }

        private void chkAllowed_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllowed.Checked)
            {
                txtProductionCallDate.EnableDateText = true;
                txtProductionCallDate.Focus();
            }
            else
            {
                txtProductionCallDate.EnableDateText = false;
            }
        }

        private void FormLoad()
        {
            try
            {
                CreateDataAdapterCommands();
                dvCustomers = dsProductionPlanning.Tables["Tbl_Customers"].DefaultView;
                dvProducts = dsProductionPlanning.Tables["Tbl_Products"].DefaultView;
                SetCombosSource();
                switch (ListForm.FormMode)
                {
                    case (int)Module1.FormModeEnum.INSERT_MODE:
                        {
                            txtRegisterDate.Text = Module1.mServerShamsiDate;
                            txtOrderNo.Focus();
                            Customer_LookUp.CB_SelectedIndex = -1;
                            Product_Lookup.CB_SelectedIndex = -1;
                            txtQuantity.Value = 1m;
                            break;
                        }

                    case (int)Module1.FormModeEnum.EDIT_MODE:
                    case (int)Module1.FormModeEnum.DELETE_MODE:
                        {
                            mCurrentRow = ListForm.GetRow();
                            // پر کردن کنترل فرم با مقادیر رکورد جاری
                            FillControls();
                            switch (ListForm.FormMode)
                            {
                                case (int)Module1.FormModeEnum.EDIT_MODE:
                                    {
                                        txtOrderNo.Focus();
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
                MessageBox.Show("فراخوانی فرم سفارش با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetCombosSource()
        {
            //{
            //    var withBlock = cbCustomer;
            //    withBlock.DataSource = dvCustomers;
            //    withBlock.DisplayMember = "CustomerName";
            //    withBlock.ValueMember = "CustomerCode";
            //}
            
            Customer_LookUp.CB_DataSource = dvCustomers; // dataTable1;
            Customer_LookUp.CB_DisplayMember = "CustomerCode";
            Customer_LookUp.CB_ValueMember = "CustomerName";
            Customer_LookUp.CB_AutoComplete = true;
            Customer_LookUp.CB_LinkedColumnIndex = 1;
            Customer_LookUp.CB_SerachFromTitle = "مشتریان";

            //{
            //    var withBlock1 = cbProduct;
            //    withBlock1.DataSource = dvProducts;
            //    withBlock1.DisplayMember = "ProductName";
            //    withBlock1.ValueMember = "ProductCode";
            //}
            Product_Lookup.CB_DataSource = dvProducts; // dataTable1;
            Product_Lookup.CB_AutoComplete = true;
            Product_Lookup.CB_LinkedColumnIndex = 1;
            Product_Lookup.CB_DisplayMember = "ProductCode";
            Product_Lookup.CB_ValueMember = "ProductName";
            Product_Lookup.CB_SerachFromTitle = "محصولات";
        }

        private void FillControls()
        {
            txtRegisterDate.Text = Conversions.ToString(mCurrentRow["RegisterDate"]);
            txtOrderNo.Text = Conversions.ToString(mCurrentRow["OrderNo"]);
            txtOrderDate.Text = Conversions.ToString(mCurrentRow["OrderDate"]);
            Customer_LookUp.CB_SelectedValue = mCurrentRow["CustomerCode"].ToString();
            Product_Lookup.CB_SelectedValue = mCurrentRow["ProductCode"].ToString();
            txtProductTechnicalSpecification.Text = Conversions.ToString(mCurrentRow["ProductTechnicalSpecification"]);
            txtQuantity.Value = Conversions.ToDecimal(mCurrentRow["OrderQuantity"]);
            txtDeliveryDate.Text = mCurrentRow["DeliveryDueDate"].ToString();
            

            txtContractNo.Text = Conversions.ToString(mCurrentRow["ContractNo"]);
            if (Conversions.ToBoolean(mCurrentRow["ConfirmFlag"]))
            {
                chkConfirmed.Checked = true;
            }
            else
            {
                chkConfirmed.Checked = false;
            }

            if (Conversions.ToBoolean(mCurrentRow["AllowFlag"]))
            {
                chkAllowed.Checked = true;
                txtProductionCallDate.Text = Conversions.ToString(mCurrentRow["ProductionCallDate"]);
            }
            else
            {
                chkAllowed.Checked = false;
                txtProductionCallDate.Text = "";
            }

            if (txtRegisterDate.Text == "0") txtRegisterDate.Text = "";
            if (txtDeliveryDate.Text == "0") txtDeliveryDate.Text = "";
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
                mdaOrder.Update(dsChanges, "Tbl_CustomerOrders");
            }

            dsChanges = null;
        }

        private void CreateDataAdapterCommands()
        {
            // ایجاد دستور اصلاح رکورد جاری در جدول
            mdaOrder.InsertCommand = new SqlCommand("Insert Into Tbl_CustomerOrders(OrderIndex,RegisterDate,OrderNo,OrderDate,CustomerCode,ProductCode,ProductTechnicalSpecification,OrderQuantity,DeliveryDueDate,ContractNo,ConfirmFlag,AllowFlag,ProductionCallDate,IsDone)" + "Values(@OrderIndex,@RegisterDate,@OrderNo,@OrderDate,@CustomerCode,@ProductCode,@ProductTechnicalSpecification,@OrderQuantity,@DeliveryDueDate,@ContractNo,@ConfirmFlag,@AllowFlag,@ProductionCallDate,@IsDone)", Module1.cnProductionPlanning);
            {
                var withBlock = mdaOrder.InsertCommand;
                withBlock.Parameters.Add("@OrderIndex", SqlDbType.BigInt, default, "OrderIndex");
                withBlock.Parameters.Add("@RegisterDate", SqlDbType.VarChar, 8, "RegisterDate");
                withBlock.Parameters.Add("@OrderNo", SqlDbType.VarChar, 50, "OrderNo");
                withBlock.Parameters.Add("@OrderDate", SqlDbType.VarChar, 8, "OrderDate");
                withBlock.Parameters.Add("@CustomerCode", SqlDbType.VarChar, 50, "CustomerCode");
                withBlock.Parameters.Add("@ProductCode", SqlDbType.VarChar, 50, "ProductCode");
                withBlock.Parameters.Add("@ProductTechnicalSpecification", SqlDbType.VarChar, 200, "ProductTechnicalSpecification");
                withBlock.Parameters.Add("@OrderQuantity", SqlDbType.Int, default, "OrderQuantity");
                withBlock.Parameters.Add("@DeliveryDueDate", SqlDbType.VarChar, 8, "DeliveryDueDate");
                withBlock.Parameters.Add("@ContractNo", SqlDbType.VarChar, 50, "ContractNo");
                withBlock.Parameters.Add("@ConfirmFlag", SqlDbType.Bit, default, "ConfirmFlag");
                withBlock.Parameters.Add("@AllowFlag", SqlDbType.Bit, default, "AllowFlag");
                withBlock.Parameters.Add("@ProductionCallDate", SqlDbType.VarChar, 8, "ProductionCallDate");
                withBlock.Parameters.Add("@IsDone", SqlDbType.Int, default, "IsDone");
            }

            // ایجاد دستور ویرایش رکورد در جدول
            mdaOrder.UpdateCommand = new SqlCommand("Update Tbl_CustomerOrders Set RegisterDate=@RegisterDate,OrderNo=@OrderNo,OrderDate=@OrderDate,CustomerCode=@CustomerCode," + "ProductCode=@ProductCode,ProductTechnicalSpecification=@ProductTechnicalSpecification," + "OrderQuantity=@OrderQuantity,DeliveryDueDate=@DeliveryDueDate,ContractNo=@ContractNo," + "ConfirmFlag=@ConfirmFlag,AllowFlag=@AllowFlag,ProductionCallDate=@ProductionCallDate Where OrderIndex = @OrderIndex", Module1.cnProductionPlanning);


            {
                var withBlock1 = mdaOrder.UpdateCommand;
                withBlock1.Parameters.Add("@RegisterDate", SqlDbType.VarChar, 8, "RegisterDate");
                withBlock1.Parameters.Add("@OrderNo", SqlDbType.VarChar, 50, "OrderNo");
                withBlock1.Parameters.Add("@OrderDate", SqlDbType.VarChar, 8, "OrderDate");
                withBlock1.Parameters.Add("@CustomerCode", SqlDbType.VarChar, 50, "CustomerCode");
                withBlock1.Parameters.Add("@ProductCode", SqlDbType.VarChar, 50, "ProductCode");
                withBlock1.Parameters.Add("@ProductTechnicalSpecification", SqlDbType.VarChar, 200, "ProductTechnicalSpecification");
                withBlock1.Parameters.Add("@OrderQuantity", SqlDbType.Int, default, "OrderQuantity");
                withBlock1.Parameters.Add("@DeliveryDueDate", SqlDbType.VarChar, 8, "DeliveryDueDate");
                withBlock1.Parameters.Add("@ContractNo", SqlDbType.VarChar, 50, "ContractNo");
                withBlock1.Parameters.Add("@ConfirmFlag", SqlDbType.Bit, default, "ConfirmFlag");
                withBlock1.Parameters.Add("@AllowFlag", SqlDbType.Bit, default, "AllowFlag");
                withBlock1.Parameters.Add("@ProductionCallDate", SqlDbType.VarChar, 8, "ProductionCallDate");
                withBlock1.Parameters.Add("@OrderIndex", SqlDbType.BigInt, default, "OrderIndex").SourceVersion = DataRowVersion.Original;
            }

            // ایجاد دستور حذف رکورد جاری در جدول
            mdaOrder.DeleteCommand = new SqlCommand("Delete From Tbl_CustomerOrders Where OrderIndex = @OrderIndex", Module1.cnProductionPlanning);
            {
                var withBlock2 = mdaOrder.DeleteCommand;
                withBlock2.Parameters.Add("@OrderIndex", SqlDbType.BigInt, default, "OrderIndex").SourceVersion = DataRowVersion.Original;
            }
        }

        private bool FormValidation()
        {
            if (txtRegisterDate.Text is null || txtRegisterDate.Text.Equals("") || txtRegisterDate.Text.Equals("0") || txtRegisterDate.Text.Length < 8)
            {
                MessageBox.Show("تاریخ ثبت سفارش را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtRegisterDate.Focus();
                return false;
            }

            if (txtOrderNo.Text is null || txtOrderNo.Text.Equals(""))
            {
                MessageBox.Show("شمارۀ سفارش را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtOrderNo.Focus();
                return false;
            }

            if (txtOrderDate.Text is null || txtOrderDate.Text.Equals("") || txtOrderDate.Text.Equals("0") || txtOrderDate.Text.Length < 8)
            {
                MessageBox.Show("تاریخ سفارش را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtOrderDate.Focus();
                return false;
            }

            if (txtDeliveryDate.Text is null || txtDeliveryDate.Text.Equals(""))
            {
                txtDeliveryDate.Text = 0.ToString();
            }

            if (Customer_LookUp.CB_SelectedIndex == -1)
            {
                MessageBox.Show("نام مشتری را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                //cbCustomer.Focus();
                return false;
            }

            if (Product_Lookup.CB_SelectedIndex == -1)
            {
                MessageBox.Show("نام محصول را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                //cbProduct.Focus();
                return false;
            }

            if (txtContractNo.Text is null || txtContractNo.Text.Equals(""))
            {
                txtContractNo.Text = "-";
            }

            if (txtProductTechnicalSpecification.Text is null || txtProductTechnicalSpecification.Text.Equals(""))
            {
                txtProductTechnicalSpecification.Text = "-";
            }

            //if (chkAllowed.Checked && (txtProductionCallDate.Text is null || txtProductionCallDate.Text.Equals("") || txtProductionCallDate.Text.Equals("0") || txtProductionCallDate.Text.Length < 8))
            //{
            //    MessageBox.Show("تاریخ دستور شروع ساخت را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            //    txtProductionCallDate.Focus();
            //    return false;
            //}

            if (txtProductionCallDate.Text is null || txtProductionCallDate.Text.Equals(""))
            {
                txtProductionCallDate.Text = 0.ToString();
            }

            return true;
        }

        private void txtPersonnelCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Strings.Asc(e.KeyChar) == (int)Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }
    }
}