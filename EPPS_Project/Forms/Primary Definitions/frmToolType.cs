//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.Windows.Forms;
//using Microsoft.VisualBasic.CompilerServices;


//namespace ProductionPlanning
//{
//    public partial class frmToolType : Form
//    {

//        public frmToolType()
//        {
//            InitializeComponent();
//            //_cmdSave.Name = "cmdSave";
//            //_cmdExit.Name = "cmdExit";
//            //_cmdDelete.Name = "cmdDelete";
//        }

//        private frmRecordsLists mListForm;
//        private SqlDataAdapter daToolType = new SqlDataAdapter();
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

//        private void frmToolType_Load(object sender, EventArgs e)
//        {

//            Module1.SetButtonsImage(cmdSave, 6);
//            Module1.SetButtonsImage(cmdDelete, 9);
//            Module1.SetButtonsImage(cmdExit, 14);
//            FormLoad();
//        }

//        private void cmdDelete_Click(object sender, EventArgs e)
//        {
//            if (MessageBox.Show("آیا از حذف این نوع ابزار اطمینان دارید؟", Module1.MessagesTitle,
//                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2,
//                MessageBoxOptions.RtlReading, false) == DialogResult.Yes)
//            {
//                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
//                    Module1.cnProductionPlanning.Open();

//                // بررسی اینکه آیا این نوع ابزار در حال استفاده است
//                var cmCheckUsage = new SqlCommand(
//                    "SELECT COUNT(*) FROM Tbl_Tools WHERE ToolTypeID = @ToolTypeID AND IsDeleted = 0",
//                    Module1.cnProductionPlanning);
//                cmCheckUsage.Parameters.AddWithValue("@ToolTypeID", CurrentRow["ID"]);

//                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(
//                    cmCheckUsage.ExecuteScalar(), 0, false)))
//                {
//                    if (Module1.cnProductionPlanning.State == ConnectionState.Open)
//                        Module1.cnProductionPlanning.Close();
//                    MessageBox.Show("این نوع ابزار در حال استفاده بوده و قابلیت حذف ندارد",
//                        Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
//                        MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
//                    return;
//                }

//                var trnDelete = Module1.cnProductionPlanning.BeginTransaction();
//                try
//                {
//                    daToolType.DeleteCommand.Transaction = trnDelete;
//                    CurrentRow.Delete();
//                    SaveChanges();
//                    trnDelete.Commit();
//                    Close();
//                }
//                catch (InvalidConstraintException ObjCnstEx)
//                {
//                    Logger.LogException("", ObjCnstEx);
//                    dsProductionPlanning.RejectChanges();
//                    trnDelete.Rollback();
//                    MessageBox.Show("اشکال در حذف رکورد، برای حذف رکورد باید رکورد(های) مرتبط با آن حذف شوند",
//                        Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error,
//                        MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
//                }
//                catch (Exception objEx)
//                {
//                    dsProductionPlanning.RejectChanges();
//                    trnDelete.Rollback();
//                    Logger.SaveError(Name + ".cmdDelete_Click", objEx.Message);
//                    MessageBox.Show("حذف نوع ابزار با مشکل مواجه شد", Module1.MessagesTitle,
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
//                            daToolType.InsertCommand.Transaction = trnSave;
//                            CurrentRow = dsProductionPlanning.Tables["Tbl_ToolType"].NewRow();
//                            CurrentRow["TypeName"] = txtTypeName.Text;
//                            dsProductionPlanning.Tables["Tbl_ToolType"].Rows.Add(CurrentRow);
//                            SaveChanges();
//                            trnSave.Commit();
//                            Close();
//                        }
//                        catch (Exception objEx)
//                        {
//                            dsProductionPlanning.RejectChanges();
//                            trnSave.Rollback();
//                            Logger.SaveError(Name + ".cmdSave_Click", objEx.Message);
//                            MessageBox.Show("ثبت نوع ابزار جدید با مشکل مواجه شد",
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
//                            daToolType.UpdateCommand.Transaction = trnSave;
//                            CurrentRow.BeginEdit();
//                            CurrentRow["TypeName"] = txtTypeName.Text;
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
//                switch (ListForm.FormMode)
//                {
//                    case (int)Module1.FormModeEnum.INSERT_MODE:
//                        {
//                            txtTypeName.Focus();
//                            break;
//                        }

//                    case (int)Module1.FormModeEnum.EDIT_MODE:
//                    case (int)Module1.FormModeEnum.DELETE_MODE:
//                        {
//                            CurrentRow = ListForm.GetRow();
//                            // پر کردن کنترل فرم با مقدار رکورد جاری
//                            txtTypeName.Text = Conversions.ToString(CurrentRow["TypeName"]);

//                            switch (ListForm.FormMode)
//                            {
//                                case (int)Module1.FormModeEnum.EDIT_MODE:
//                                    {
//                                        txtTypeName.Focus();
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
//                MessageBox.Show("فراخوانی فرم انواع قالب و ابزارالات با مشکل مواجه شد",
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
//                daToolType.Update(dsChanges, "Tbl_ToolType");
//                dsProductionPlanning.AcceptChanges();
//            }
//            dsChanges = null;
//        }

//        private void CreateDataAdapterCommands()
//        {
//            // ایجاد دستور اضافه کردن رکورد جدید
//            daToolType.InsertCommand = new SqlCommand(
//                "INSERT INTO Tbl_ToolType (TypeName) VALUES (@TypeName)",
//                Module1.cnProductionPlanning);

//            daToolType.InsertCommand.Parameters.Add("@TypeName", SqlDbType.NVarChar, 100, "TypeName");

//            // ایجاد دستور اصلاح رکورد جاری
//            daToolType.UpdateCommand = new SqlCommand(
//                "UPDATE Tbl_ToolType SET TypeName = @TypeName WHERE ID = @ID",
//                Module1.cnProductionPlanning);

//            daToolType.UpdateCommand.Parameters.Add("@TypeName", SqlDbType.NVarChar, 100, "TypeName");
//            daToolType.UpdateCommand.Parameters.Add("@ID", SqlDbType.Int, 0, "ID").SourceVersion = DataRowVersion.Original;

//            // ایجاد دستور حذف رکورد جاری
//            daToolType.DeleteCommand = new SqlCommand(
//                "DELETE FROM Tbl_ToolType WHERE ID = @ID",
//                Module1.cnProductionPlanning);

//            daToolType.DeleteCommand.Parameters.Add("@ID", SqlDbType.Int, 0, "ID").SourceVersion = DataRowVersion.Original;
//        }

//        private bool FormValidation()
//        {
//            if (string.IsNullOrEmpty(txtTypeName.Text) || txtTypeName.Text.Trim().Equals(""))
//            {
//                MessageBox.Show("نام نوع ابزار را وارد کنید", Module1.MessagesTitle,
//                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
//                    MessageBoxOptions.RtlReading);
//                txtTypeName.Focus();
//                return false;
//            }
//            return true;
//        }

//        private void frmToolType_FormClosing(object sender, FormClosingEventArgs e)
//        {

//            dsProductionPlanning.RejectChanges();
//            daToolType.Dispose();

//        }
//    }

//}
