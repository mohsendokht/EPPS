//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.Windows.Forms;
//using Microsoft.VisualBasic.CompilerServices;


//namespace ProductionPlanning
//{
//    public partial class frmTool : Form
//    {

//        public frmTool()
//        {
//            InitializeComponent();
//            //_cmdSave.Name = "cmdSave";
//            //_cmdExit.Name = "cmdExit";
//            //_cmdDelete.Name = "cmdDelete";
//        }

//        private frmRecordsLists mListForm;
//        private SqlDataAdapter daTool = new SqlDataAdapter();
//        private DataRow CurrentRow;

//        public frmRecordsLists ListForm
//        {
//            get { return mListForm; }
//            set { mListForm = value; }
//        }

//        public DataSet dsProductionPlanning
//        {
//            get { return ListForm.dsProductionPlanning; }

//        }
//        private void frmTool_Load(object sender, EventArgs e)
//        {
//            Module1.SetButtonsImage(cmdSave, 6);
//            Module1.SetButtonsImage(cmdDelete, 9);
//            Module1.SetButtonsImage(cmdExit, 14);
//            FormLoad();
//        }

//        private void frmTool_FormClosing(object sender, FormClosingEventArgs e)
//        {
//            dsProductionPlanning.RejectChanges();
//            daTool.Dispose();
//        }

//        private void cmdDelete_Click(object sender, EventArgs e)
//        {
//            if (MessageBox.Show("آیا از حذف این ابزار اطمینان دارید؟", Module1.MessagesTitle,
//                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2,
//                MessageBoxOptions.RtlReading, false) == DialogResult.Yes)
//            {
//                var trnDelete = Module1.cnProductionPlanning.BeginTransaction();
//                try
//                {
//                    daTool.DeleteCommand.Transaction = trnDelete;
//                    // Soft Delete - فقط علامت IsDeleted را true می‌کنیم
//                    CurrentRow.BeginEdit();
//                    CurrentRow["IsDeleted"] = true;
//                    CurrentRow["ModifiedAt"] = DateTime.Now;
//                    CurrentRow.EndEdit();

//                    SaveChanges();
//                    trnDelete.Commit();
//                    Close();
//                }
//                catch (Exception objEx)
//                {
//                    CurrentRow.CancelEdit();
//                    trnDelete.Rollback();
//                    Logger.SaveError(Name + ".cmdDelete_Click", objEx.Message);
//                    MessageBox.Show("حذف ابزار با مشکل مواجه شد", Module1.MessagesTitle,
//                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
//                        MessageBoxOptions.RtlReading, false);
//                }
//                finally
//                {
//                    trnDelete.Dispose();
//                    if (Module1.cnProductionPlanning.State == ConnectionState.Open)
//                        Module1.cnProductionPlanning.Close();
//                }
//            }
//        }

//        private void cmdSave_Click(object sender, EventArgs e)
//        {
//            if (!FormValidation())
//            {
//                return;
//            }

//            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
//                Module1.cnProductionPlanning.Open();

//            var trnSave = Module1.cnProductionPlanning.BeginTransaction();
//            switch (ListForm.FormMode)
//            {
//                case (int)Module1.FormModeEnum.INSERT_MODE:
//                    {
//                        try
//                        {
//                            daTool.InsertCommand.Transaction = trnSave;
//                            CurrentRow = dsProductionPlanning.Tables["Tbl_Tools"].NewRow();

//                            CurrentRow["ToolCode"] = txtToolCode.Text;
//                            CurrentRow["ToolName"] = txtToolName.Text;
//                            CurrentRow["ToolTypeID"] = cmbToolTypeID.SelectedValue;
//                            CurrentRow["TechnicalSpecs"] = txtTechnicalSpecs.Text;
//                            CurrentRow["CurrentQuantity"] = numCurrentQuantity.Value;
//                            CurrentRow["MinStockLevel"] = numMinStockLevel.Value;
//                            CurrentRow["ToolLocation"] = txtToolLocation.Text;
//                            CurrentRow["MaintenanceCycle"] = txtMaintenanceCycle.Text;
//                            CurrentRow["CreatedAt"] = DateTime.Now;
//                            CurrentRow["ModifiedAt"] = DateTime.Now;
//                            CurrentRow["IsDeleted"] = false;

//                            dsProductionPlanning.Tables["Tbl_Tools"].Rows.Add(CurrentRow);
//                            SaveChanges();
//                            trnSave.Commit();
//                            Close();
//                        }
//                        catch (Exception objEx)
//                        {
//                            dsProductionPlanning.RejectChanges();
//                            trnSave.Rollback();
//                            Logger.SaveError(Name + ".cmdSave_Click", objEx.Message);
//                            MessageBox.Show("ثبت ابزار جدید با مشکل مواجه شد",
//                                Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        }
//                        finally
//                        {
//                            trnSave.Dispose();
//                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
//                                Module1.cnProductionPlanning.Close();
//                        }
//                        break;
//                    }

//                case (int)Module1.FormModeEnum.EDIT_MODE:
//                    {
//                        try
//                        {
//                            daTool.UpdateCommand.Transaction = trnSave;
//                            CurrentRow.BeginEdit();

//                            CurrentRow["ToolCode"] = txtToolCode.Text;
//                            CurrentRow["ToolName"] = txtToolName.Text;
//                            CurrentRow["ToolTypeID"] = cmbToolTypeID.SelectedValue;
//                            CurrentRow["TechnicalSpecs"] = txtTechnicalSpecs.Text;
//                            CurrentRow["CurrentQuantity"] = numCurrentQuantity.Value;
//                            CurrentRow["MinStockLevel"] = numMinStockLevel.Value;
//                            CurrentRow["ToolLocation"] = txtToolLocation.Text;
//                            CurrentRow["MaintenanceCycle"] = txtMaintenanceCycle.Text;
//                            CurrentRow["ModifiedAt"] = DateTime.Now;

//                            CurrentRow.EndEdit();
//                            SaveChanges();
//                            trnSave.Commit();
//                            Close();
//                        }
//                        catch (Exception objEx)
//                        {
//                            CurrentRow.CancelEdit();
//                            trnSave.Rollback();
//                            Logger.SaveError(Name + ".cmdSave_Click", objEx.Message);
//                            MessageBox.Show("ثبت تغییرات با مشکل مواجه شد",
//                                Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        }
//                        finally
//                        {
//                            trnSave.Dispose();
//                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
//                                Module1.cnProductionPlanning.Close();
//                        }
//                        break;
//                    }
//            }
//        }

//        private void cmdExit_Click(object sender, EventArgs e)
//        {
//            Close();
//        }

//        private void FormLoad()
//        {
//            try
//            {
//                CreateDataAdapterCommands();

//                // پر کردن ComboBox انواع ابزار
//                cmbToolTypeID.DataSource = dsProductionPlanning.Tables["Tbl_ToolType"];
//                cmbToolTypeID.DisplayMember = "TypeName";
//                cmbToolTypeID.ValueMember = "ID";

//                switch (ListForm.FormMode)
//                {
//                    case (int)Module1.FormModeEnum.INSERT_MODE:
//                        {
//                            txtToolCode.Focus();
//                            break;
//                        }

//                    case (int)Module1.FormModeEnum.EDIT_MODE:
//                    case (int)Module1.FormModeEnum.DELETE_MODE:
//                        {
//                            CurrentRow = ListForm.GetRow();
//                            // پر کردن کنترل فرم با مقدار رکورد جاری
//                            txtToolCode.Text = Conversions.ToString(CurrentRow["ToolCode"]);
//                            txtToolName.Text = Conversions.ToString(CurrentRow["ToolName"]);
//                            cmbToolTypeID.SelectedValue = CurrentRow["ToolTypeID"];
//                            txtTechnicalSpecs.Text = Conversions.ToString(CurrentRow["TechnicalSpecs"]);
//                            numCurrentQuantity.Value = Convert.ToDecimal(CurrentRow["CurrentQuantity"]);
//                            numMinStockLevel.Value = Convert.ToDecimal(CurrentRow["MinStockLevel"]);
//                            txtToolLocation.Text = Conversions.ToString(CurrentRow["ToolLocation"]);
//                            txtMaintenanceCycle.Text = Conversions.ToString(CurrentRow["MaintenanceCycle"]);

//                            switch (ListForm.FormMode)
//                            {
//                                case (int)Module1.FormModeEnum.EDIT_MODE:
//                                    {
//                                        txtToolCode.Focus();
//                                        break;
//                                    }

//                                case (int)Module1.FormModeEnum.DELETE_MODE:
//                                    {
//                                        cmdDelete.Focus();
//                                        break;
//                                    }
//                            }
//                            break;
//                        }
//                }
//            }
//            catch (Exception objEx)
//            {
//                Logger.SaveError(Name + ".FormLoad", objEx.Message);
//                MessageBox.Show("فراخوانی فرم قالب ها و ابزار آلات با مشکل مواجه شد",
//                    Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void SaveChanges()
//        {
//            DataSet dsChanges;
//            dsChanges = dsProductionPlanning.GetChanges();
//            if (dsChanges.HasErrors)
//            {
//                dsProductionPlanning.RejectChanges();
//            }
//            else
//            {
//                daTool.Update(dsChanges, "Tbl_Tools");
//                dsProductionPlanning.AcceptChanges();
//            }
//            dsChanges = null;
//        }

//        private void CreateDataAdapterCommands()
//        {
//            // ایجاد دستور اضافه کردن رکورد جدید
//            daTool.InsertCommand = new SqlCommand(
//                @"INSERT INTO Tbl_Tools (ToolCode, ToolName, ToolTypeID, TechnicalSpecs, 
//                  CurrentQuantity, MinStockLevel, ToolLocation, MaintenanceCycle, 
//                  CreatedAt, ModifiedAt, IsDeleted) 
//                  VALUES (@ToolCode, @ToolName, @ToolTypeID, @TechnicalSpecs, 
//                  @CurrentQuantity, @MinStockLevel, @ToolLocation, @MaintenanceCycle, 
//                  @CreatedAt, @ModifiedAt, @IsDeleted)",
//                Module1.cnProductionPlanning);

//            daTool.InsertCommand.Parameters.Add("@ToolCode", SqlDbType.NVarChar, 50, "ToolCode");
//            daTool.InsertCommand.Parameters.Add("@ToolName", SqlDbType.NVarChar, 100, "ToolName");
//            daTool.InsertCommand.Parameters.Add("@ToolTypeID", SqlDbType.Int, 0, "ToolTypeID");
//            daTool.InsertCommand.Parameters.Add("@TechnicalSpecs", SqlDbType.NText, 0, "TechnicalSpecs");
//            daTool.InsertCommand.Parameters.Add("@CurrentQuantity", SqlDbType.Int, 0, "CurrentQuantity");
//            daTool.InsertCommand.Parameters.Add("@MinStockLevel", SqlDbType.Int, 0, "MinStockLevel");
//            daTool.InsertCommand.Parameters.Add("@ToolLocation", SqlDbType.NVarChar, 200, "ToolLocation");
//            daTool.InsertCommand.Parameters.Add("@MaintenanceCycle", SqlDbType.NVarChar, 100, "MaintenanceCycle");
//            daTool.InsertCommand.Parameters.Add("@CreatedAt", SqlDbType.DateTime, 0, "CreatedAt");
//            daTool.InsertCommand.Parameters.Add("@ModifiedAt", SqlDbType.DateTime, 0, "ModifiedAt");
//            daTool.InsertCommand.Parameters.Add("@IsDeleted", SqlDbType.Bit, 0, "IsDeleted");

//            // ایجاد دستور اصلاح رکورد جاری
//            daTool.UpdateCommand = new SqlCommand(
//                @"UPDATE Tbl_Tools SET 
//                  ToolCode = @ToolCode, 
//                  ToolName = @ToolName, 
//                  ToolTypeID = @ToolTypeID, 
//                  TechnicalSpecs = @TechnicalSpecs, 
//                  CurrentQuantity = @CurrentQuantity, 
//                  MinStockLevel = @MinStockLevel, 
//                  ToolLocation = @ToolLocation, 
//                  MaintenanceCycle = @MaintenanceCycle, 
//                  ModifiedAt = @ModifiedAt 
//                  WHERE ID = @ID",
//                Module1.cnProductionPlanning);

//            daTool.UpdateCommand.Parameters.Add("@ToolCode", SqlDbType.NVarChar, 50, "ToolCode");
//            daTool.UpdateCommand.Parameters.Add("@ToolName", SqlDbType.NVarChar, 100, "ToolName");
//            daTool.UpdateCommand.Parameters.Add("@ToolTypeID", SqlDbType.Int, 0, "ToolTypeID");
//            daTool.UpdateCommand.Parameters.Add("@TechnicalSpecs", SqlDbType.NText, 0, "TechnicalSpecs");
//            daTool.UpdateCommand.Parameters.Add("@CurrentQuantity", SqlDbType.Int, 0, "CurrentQuantity");
//            daTool.UpdateCommand.Parameters.Add("@MinStockLevel", SqlDbType.Int, 0, "MinStockLevel");
//            daTool.UpdateCommand.Parameters.Add("@ToolLocation", SqlDbType.NVarChar, 200, "ToolLocation");
//            daTool.UpdateCommand.Parameters.Add("@MaintenanceCycle", SqlDbType.NVarChar, 100, "MaintenanceCycle");
//            daTool.UpdateCommand.Parameters.Add("@ModifiedAt", SqlDbType.DateTime, 0, "ModifiedAt");
//            daTool.UpdateCommand.Parameters.Add("@ID", SqlDbType.Int, 0, "ID").SourceVersion = DataRowVersion.Original;

//            // ایجاد دستور حذف رکورد جاری (Soft Delete)
//            daTool.DeleteCommand = new SqlCommand(
//                @"UPDATE Tbl_Tools SET 
//                  IsDeleted = 1, 
//                  ModifiedAt = GETDATE() 
//                  WHERE ID = @ID",
//                Module1.cnProductionPlanning);

//            daTool.DeleteCommand.Parameters.Add("@ID", SqlDbType.Int, 0, "ID").SourceVersion = DataRowVersion.Original;
//        }

//        private bool FormValidation()
//        {
//            if (string.IsNullOrEmpty(txtToolCode.Text) || txtToolCode.Text.Trim().Equals(""))
//            {
//                MessageBox.Show("کد ابزار را وارد کنید", Module1.MessagesTitle,
//                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
//                    MessageBoxOptions.RtlReading);
//                txtToolCode.Focus();
//                return false;
//            }

//            if (string.IsNullOrEmpty(txtToolName.Text) || txtToolName.Text.Trim().Equals(""))
//            {
//                MessageBox.Show("نام ابزار را وارد کنید", Module1.MessagesTitle,
//                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
//                    MessageBoxOptions.RtlReading);
//                txtToolName.Focus();
//                return false;
//            }

//            if (cmbToolTypeID.SelectedValue == null)
//            {
//                MessageBox.Show("نوع ابزار را انتخاب کنید", Module1.MessagesTitle,
//                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
//                    MessageBoxOptions.RtlReading);
//                cmbToolTypeID.Focus();
//                return false;
//            }

//            if (string.IsNullOrEmpty(txtToolLocation.Text))
//            {
//                txtToolLocation.Text = "-";
//            }

//            if (string.IsNullOrEmpty(txtMaintenanceCycle.Text))
//            {
//                txtMaintenanceCycle.Text = "-";
//            }

//            return true;
//        }
//    }
//}
