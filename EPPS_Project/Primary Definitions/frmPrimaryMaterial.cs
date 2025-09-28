using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmPrimaryMaterial
    {
        public frmPrimaryMaterial()
        {
            InitializeComponent();
            _cmdSave.Name = "cmdSave";
            _cmdExit.Name = "cmdExit";
            _cmdDelete.Name = "cmdDelete";
            _RadioButton1.Name = "RadioButton1";
            _rbMinute.Name = "rbMinute";
            _rbDay.Name = "rbDay";
            _rbHour.Name = "rbHour";
            _rbMonth.Name = "rbMonth";
            _rbWeek.Name = "rbWeek";
        }

        private frmRecordsLists mListForm;
        private SqlDataAdapter daMaterial = new SqlDataAdapter();
        private DataRow CurrentRow; // برای نگهداری رکورد جاری
        private SqlParameter[] prmUpdate = new SqlParameter[2];
        private SqlParameter prmDelete;
        private short I;
        private short TimeType = 1;

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

        private void frmPrimaryMaterial_FormClosing(object sender, FormClosingEventArgs e)
        {
            dsProductionPlanning.RejectChanges();
            dsProductionPlanning.Relations.Clear();
            dsProductionPlanning.Tables["Tbl_PrimaryMaterials"].Constraints.Clear();
            for (I = 2; I >= 1; I += -1)
            {
                dsProductionPlanning.Tables[I].Constraints.Clear();
                dsProductionPlanning.Tables[I].Dispose();
                dsProductionPlanning.Tables.RemoveAt(I);
            }

            daMaterial.Dispose();
            prmUpdate = null;
            prmDelete = null;
            mListForm = null;
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("مواد اولیه را حذف می کنید", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading, false) == DialogResult.Yes)
            {
                SqlTransaction trnDelete = null;
                try
                {
                    if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                        Module1.cnProductionPlanning.Open();
                    trnDelete = Module1.cnProductionPlanning.BeginTransaction();
                    daMaterial.DeleteCommand.Transaction = trnDelete;
                    CurrentRow.Delete();
                    SaveChanges();
                    trnDelete.Commit();
                    Close();
                }
                catch (InvalidConstraintException ObjCnstEx)
                {
                    Logger.LogException("cmdDelete_Click", ObjCnstEx);
                    dsProductionPlanning.RejectChanges();
                    trnDelete.Rollback();
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
                    trnDelete = null;
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
                            daMaterial.InsertCommand.Transaction = trnInsert;
                            // اضافه کردن رکورد جدید به جدول در دیتاست
                            drInsert = dsProductionPlanning.Tables["Tbl_PrimaryMaterials"].NewRow();
                            drInsert["MaterialCode"] = txtCode.Text;
                            drInsert["MaterialTitle"] = txtName.Text;
                            drInsert["TechniqueSpecification"] = txtSpecification.Text;
                            drInsert["StoreUnit"] = cbStoreTestUnit.SelectedValue;
                            drInsert["StoreUnitTitle"] = cbStoreTestUnit.Text;
                            drInsert["ProductionUnit"] = -1; // cbProductionTestUnit.SelectedValue
                            drInsert["OrderPoint"] = txtOrderPoint.Text;
                            drInsert["OrderMinimumAmount"] = txtMinimumCountPerOrder.Value;
                            drInsert["StorePlace"] = cbStorePlace.SelectedValue;
                            drInsert["LastHolding"] = txtLastHolding.Value;
                            drInsert["FirstHolding"] = txtFirstHolding.Value;
                            drInsert["StoredAdmeasurementMethod"] = txtFillMeasureAppointMethod.Text;
                            drInsert["GarbagePercent"] = txtGarbagePercent.Value;
                            drInsert["BatchBuyTime"] = txtBuyTimeBatch.Value;
                            drInsert["BatchQuantity"] = txtBatchQuantity.Value;
                            drInsert["TimeType"] = TimeType;
                            drInsert["Application"] = txtApplication.Text;
                            dsProductionPlanning.Tables["Tbl_PrimaryMaterials"].Rows.Add(drInsert);
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
                            daMaterial.UpdateCommand.Transaction = trnUpdate;
                            CurrentRow.BeginEdit();
                            CurrentRow["MaterialCode"] = txtCode.Text;
                            CurrentRow["MaterialTitle"] = txtName.Text;
                            CurrentRow["TechniqueSpecification"] = txtSpecification.Text;
                            CurrentRow["StoreUnit"] = cbStoreTestUnit.SelectedValue;
                            CurrentRow["StoreUnitTitle"] = cbStoreTestUnit.Text;
                            // CurrentRow("ProductionUnit") = cbProductionTestUnit.SelectedValue
                            CurrentRow["OrderPoint"] = txtOrderPoint.Text;
                            CurrentRow["OrderMinimumAmount"] = txtMinimumCountPerOrder.Value;
                            CurrentRow["StorePlace"] = cbStorePlace.SelectedValue;
                            CurrentRow["LastHolding"] = txtLastHolding.Value;
                            CurrentRow["FirstHolding"] = txtFirstHolding.Value;
                            CurrentRow["StoredAdmeasurementMethod"] = txtFillMeasureAppointMethod.Text;
                            CurrentRow["GarbagePercent"] = txtGarbagePercent.Value;
                            CurrentRow["BatchBuyTime"] = txtBuyTimeBatch.Value;
                            CurrentRow["BatchQuantity"] = txtBatchQuantity.Value;
                            CurrentRow["TimeType"] = TimeType;
                            CurrentRow["Application"] = txtApplication.Text;
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

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton1.Checked)
            {
                TimeType = 1;
            }
        }

        private void rbMinute_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMinute.Checked)
            {
                TimeType = 2;
            }
        }

        private void rbHour_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHour.Checked)
            {
                TimeType = 3;
            }
        }

        private void rbDay_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDay.Checked)
            {
                TimeType = 4;
            }
        }

        private void rbWeek_CheckedChanged(object sender, EventArgs e)
        {
            if (rbWeek.Checked)
            {
                TimeType = 5;
            }
        }

        private void rbMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMonth.Checked)
            {
                TimeType = 6;
            }
        }

        private void FormLoad()
        {
            try
            {
                CreateDataAdapterCommands();

                // With cbProductionTestUnit
                // .DataSource = Nothing
                // .DataSource = dsProductionPlanning.Tables("Tbl_ProductionTestUnits")
                // .DisplayMember = "Title"
                // .ValueMember = "Code"
                // .DataBindings.Add("SelectedValue", dsProductionPlanning, "Tbl_PrimaryMaterials.ProductionUnit")
                // End With

                {
                    var withBlock = cbStoreTestUnit;
                    withBlock.DataSource = null;
                    withBlock.DataSource = dsProductionPlanning.Tables["Tbl_StoreTestUnits"];
                    withBlock.DisplayMember = "Title";
                    withBlock.ValueMember = "Code";
                    withBlock.DataBindings.Add("SelectedValue", dsProductionPlanning, "Tbl_PrimaryMaterials.StoreUnit");
                }

                {
                    var withBlock1 = cbStorePlace;
                    withBlock1.DataSource = null;
                    withBlock1.DataSource = dsProductionPlanning.Tables["Tbl_Stores"];
                    withBlock1.DisplayMember = "StoreName";
                    withBlock1.ValueMember = "StoreCode";
                    withBlock1.DataBindings.Add("SelectedValue", dsProductionPlanning, "Tbl_PrimaryMaterials.StorePlace");
                }

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
                            txtCode.Text = Conversions.ToString(CurrentRow["MaterialCode"]);
                            txtName.Text = Conversions.ToString(CurrentRow["MaterialTitle"]);
                            txtSpecification.Text = Conversions.ToString(CurrentRow["TechniqueSpecification"]);
                            cbStoreTestUnit.SelectedValue = CurrentRow["StoreUnit"];
                            // cbProductionTestUnit.SelectedValue = CurrentRow("ProductionUnit")
                            txtOrderPoint.Text = Conversions.ToString(CurrentRow["OrderPoint"]);
                            txtMinimumCountPerOrder.Value = Conversions.ToDecimal(CurrentRow["OrderMinimumAmount"]);
                            cbStorePlace.SelectedValue = CurrentRow["StorePlace"];
                            txtLastHolding.Value = Conversions.ToDecimal(CurrentRow["LastHolding"]);
                            txtFirstHolding.Value = Conversions.ToDecimal(CurrentRow["FirstHolding"]);
                            txtFillMeasureAppointMethod.Text = Conversions.ToString(CurrentRow["StoredAdmeasurementMethod"]);
                            txtGarbagePercent.Value = Conversions.ToDecimal(CurrentRow["GarbagePercent"]);
                            txtBuyTimeBatch.Value = Conversions.ToDecimal(CurrentRow["BatchBuyTime"]);
                            txtBatchQuantity.Value = Conversions.ToDecimal(CurrentRow["BatchQuantity"]);
                            if (!Information.IsDBNull(CurrentRow["Application"]))
                            {
                                txtApplication.Text = Conversions.ToString(CurrentRow["Application"]);
                            }

                            switch (CurrentRow["TimeType"])
                            {
                                case 1:
                                    {
                                        RadioButton1.Checked = true;
                                        break;
                                    }

                                case 2:
                                    {
                                        rbMinute.Checked = true;
                                        break;
                                    }

                                case 3:
                                    {
                                        rbHour.Checked = true;
                                        break;
                                    }

                                case 4:
                                    {
                                        rbDay.Checked = true;
                                        break;
                                    }

                                case 5:
                                    {
                                        rbWeek.Checked = true;
                                        break;
                                    }

                                case 6:
                                    {
                                        rbMonth.Checked = true;
                                        break;
                                    }
                            }

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
                daMaterial.Update(dsChanges, "Tbl_PrimaryMaterials");
                dsProductionPlanning.AcceptChanges();
            }

            dsChanges = null;
        }

        private void CreateDataAdapterCommands()
        {
            // ایجاد دستور اضافه کردن رکورد جدید در جدول
            daMaterial.InsertCommand = new SqlCommand("Insert Into Tbl_PrimaryMaterials(MaterialCode,MaterialTitle,TechniqueSpecification,StoreUnit,ProductionUnit,OrderPoint,OrderMinimumAmount,StorePlace,LastHolding,FirstHolding,StoredAdmeasurementMethod,GarbagePercent,BatchBuyTime,BatchQuantity,TimeType,Application) " + "Values(@MaterialCode,@MaterialTitle,@TechniqueSpecification,@StoreUnit,@ProductionUnit,@OrderPoint,@OrderMinimumAmount,@StorePlace,@LastHolding,@FirstHolding,@StoredAdmeasurementMethod,@GarbagePercent,@BatchBuyTime,@BatchQuantity,@TimeType,@Application)", Module1.cnProductionPlanning);
            {
                var withBlock = daMaterial.InsertCommand;
                withBlock.Parameters.Add("@MaterialCode", SqlDbType.VarChar, 50, "MaterialCode");
                withBlock.Parameters.Add("@MaterialTitle", SqlDbType.VarChar, 50, "MaterialTitle");
                withBlock.Parameters.Add("@TechniqueSpecification", SqlDbType.VarChar, 50, "TechniqueSpecification");
                withBlock.Parameters.Add("@StoreUnit", SqlDbType.Int, default, "StoreUnit");
                withBlock.Parameters.Add("@ProductionUnit", SqlDbType.Int, default, "ProductionUnit");
                withBlock.Parameters.Add("@OrderPoint", SqlDbType.VarChar, 50, "OrderPoint");
                withBlock.Parameters.Add("@OrderMinimumAmount", SqlDbType.Int, default, "OrderMinimumAmount");
                withBlock.Parameters.Add("@StorePlace", SqlDbType.VarChar, 50, "StorePlace");
                withBlock.Parameters.Add("@LastHolding", SqlDbType.Int, default, "LastHolding");
                withBlock.Parameters.Add("@FirstHolding", SqlDbType.Int, default, "FirstHolding");
                withBlock.Parameters.Add("@StoredAdmeasurementMethod", SqlDbType.VarChar, 50, "StoredAdmeasurementMethod");
                withBlock.Parameters.Add("@GarbagePercent", SqlDbType.Int, default, "GarbagePercent");
                withBlock.Parameters.Add("@BatchBuyTime", SqlDbType.Int, default, "BatchBuyTime");
                withBlock.Parameters.Add("@BatchQuantity", SqlDbType.Int, default, "BatchQuantity");
                withBlock.Parameters.Add("@TimeType", SqlDbType.Int, default, "TimeType");
                withBlock.Parameters.Add("@Application", SqlDbType.VarChar, 500, "Application");
            }
            // ایجاد دستور اصلاح رکورد جاری در جدول
            daMaterial.UpdateCommand = new SqlCommand("Update Tbl_PrimaryMaterials Set MaterialCode=@CurrentMaterialCode,MaterialTitle=@MaterialTitle," + "TechniqueSpecification=@TechniqueSpecification,StoreUnit=@StoreUnit," + "OrderPoint=@OrderPoint,OrderMinimumAmount=@OrderMinimumAmount," + "StorePlace=@StorePlace,LastHolding=@LastHolding,FirstHolding=@FirstHolding," + "StoredAdmeasurementMethod=@StoredAdmeasurementMethod,GarbagePercent=@GarbagePercent," + "BatchBuyTime=@BatchBuyTime,BatchQuantity=@BatchQuantity,TimeType=@TimeType," + "Application=@Application Where MaterialCode=@OldMaterialCode", Module1.cnProductionPlanning);





            {
                var withBlock1 = daMaterial.UpdateCommand;
                prmUpdate[0] = withBlock1.Parameters.Add("@CurrentMaterialCode", SqlDbType.VarChar, 50, "MaterialCode");
                prmUpdate[1] = withBlock1.Parameters.Add("@OldMaterialCode", SqlDbType.VarChar, 50, "MaterialCode");
                withBlock1.Parameters.Add("@MaterialTitle", SqlDbType.VarChar, 50, "MaterialTitle");
                withBlock1.Parameters.Add("@TechniqueSpecification", SqlDbType.VarChar, 50, "TechniqueSpecification");
                withBlock1.Parameters.Add("@StoreUnit", SqlDbType.Int, default, "StoreUnit");
                // .Parameters.Add("@ProductionUnit", SqlDbType.Int, Nothing, "ProductionUnit")
                withBlock1.Parameters.Add("@OrderPoint", SqlDbType.VarChar, 50, "OrderPoint");
                withBlock1.Parameters.Add("@OrderMinimumAmount", SqlDbType.Int, default, "OrderMinimumAmount");
                withBlock1.Parameters.Add("@StorePlace", SqlDbType.VarChar, 50, "StorePlace");
                withBlock1.Parameters.Add("@LastHolding", SqlDbType.Int, default, "LastHolding");
                withBlock1.Parameters.Add("@FirstHolding", SqlDbType.Int, default, "FirstHolding");
                withBlock1.Parameters.Add("@StoredAdmeasurementMethod", SqlDbType.VarChar, 50, "StoredAdmeasurementMethod");
                withBlock1.Parameters.Add("@GarbagePercent", SqlDbType.Int, default, "GarbagePercent");
                withBlock1.Parameters.Add("@BatchBuyTime", SqlDbType.Int, default, "BatchBuyTime");
                withBlock1.Parameters.Add("@BatchQuantity", SqlDbType.Int, default, "BatchQuantity");
                withBlock1.Parameters.Add("@TimeType", SqlDbType.Int, default, "TimeType");
                withBlock1.Parameters.Add("@Application", SqlDbType.VarChar, 500, "Application");
            }

            prmUpdate[0].Direction = ParameterDirection.Input;
            prmUpdate[0].SourceVersion = DataRowVersion.Current;
            prmUpdate[1].Direction = ParameterDirection.Input;
            prmUpdate[1].SourceVersion = DataRowVersion.Original;
            // ایجاد دستور حذف رکورد جاری در جدول
            daMaterial.DeleteCommand = new SqlCommand("Delete From Tbl_PrimaryMaterials Where MaterialCode=@MaterialCode", Module1.cnProductionPlanning);
            {
                var withBlock2 = daMaterial.DeleteCommand;
                prmDelete = withBlock2.Parameters.Add("@MaterialCode", SqlDbType.VarChar, 50, "MaterialCode");
            }

            prmDelete.Direction = ParameterDirection.Input;
            prmDelete.SourceVersion = DataRowVersion.Original;
        }

        private bool FormValidation()
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                MessageBox.Show("کد مواد اولیه را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtCode.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("نام مواد اولیه را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtName.Focus();
                return false;
            }

            // If cbProductionTestUnit.SelectedIndex = -1 Then
            // MessageBox.Show("واحد سنجش تولید را انتخاب کنید", MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            // cbProductionTestUnit.Focus()
            // Return False
            // End If

            if (cbStoreTestUnit.SelectedIndex == -1)
            {
                MessageBox.Show("واحد سنجش انبار را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbStoreTestUnit.Focus();
                return false;
            }

            // If cbStorePlace.SelectedIndex = -1 Then
            // MessageBox.Show("محل نگهداری را انتخاب کنید", MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            // cbStorePlace.Focus()
            // Return False
            // End If

            return true;
        }
    }
}