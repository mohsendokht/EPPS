using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmMachineNotAvailableReason
    {
        public frmMachineNotAvailableReason()
        {
            InitializeComponent();
            _cmdExit.Name = "cmdExit";
            _cmdSave.Name = "cmdSave";
            _cmdDelete.Name = "cmdDelete";
            _txtTitle.Name = "txtTitle";
        }

        private frmRecordsLists mListForm;
        private SqlDataAdapter daReason = new SqlDataAdapter();
        private DataRow mCurrentRow;
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

        private void frmOperationTitle_FormClosing(object sender, FormClosingEventArgs e)
        {
            dsProductionPlanning.RejectChanges();
            daReason.Dispose();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("علت در دسترس نبودن را حذف می کنید", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading, false) == DialogResult.Yes)
            {
                SqlTransaction trnDelete = null;
                try
                {
                    if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                        Module1.cnProductionPlanning.Open();
                    {
                        var withBlock = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject("Select Count(*) From Tbl_MachineNotAvailableTimes Where ReasonCode = ", mCurrentRow["ReasonCode"])), Module1.cnProductionPlanning);
                        if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(withBlock.ExecuteScalar(), 0, false)))
                        {
                            MessageBox.Show("به دلیل وجود سابقه(هایی) مرتبط بااین علت در جدول: در دسترس نبودن ماشین ها، قابلیت حذف وجود ندارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                            return;
                        }
                    }

                    trnDelete = Module1.cnProductionPlanning.BeginTransaction();
                    daReason.DeleteCommand.Transaction = trnDelete;
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
                    MessageBox.Show("برای حذف رکورد باید رکورد(های) مرتبط با آن در جدول توقفات تولید حذف شوند", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
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
                    if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                        Module1.cnProductionPlanning.Close();
                }
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                MessageBox.Show("علت توقف را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtTitle.Focus();
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
                            daReason.InsertCommand.Transaction = trnInsert;
                            // اضافه کردن رکورد جدید به جدول در دیتاست

                            drInsert = dsProductionPlanning.Tables["Tbl_MachineNotAvailableReasons"].NewRow();
                            drInsert["ReasonCode"] = Module1.GetNewCode("Tbl_MachineNotAvailableReasons", "ReasonCode");
                            drInsert["ReasonTitle"] = txtTitle.Text;
                            dsProductionPlanning.Tables["Tbl_MachineNotAvailableReasons"].Rows.Add(drInsert);
                            SaveChanges();
                            trnInsert.Commit();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            dsProductionPlanning.RejectChanges();
                            trnInsert.Rollback();
                            Logger.SaveError(Name + ".cmdSave_Click", objEx.Message);
                            MessageBox.Show("اشکال در ثبت رکورد جدید، رکورد جدید ثبت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        }
                        finally
                        {
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
                            daReason.UpdateCommand.Transaction = trnUpdate;
                            mCurrentRow.BeginEdit();
                            mCurrentRow["ReasonTitle"] = txtTitle.Text;
                            mCurrentRow.EndEdit();
                            SaveChanges();
                            trnUpdate.Commit();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            mCurrentRow.CancelEdit();
                            trnUpdate.Rollback();
                            Logger.SaveError(Name + ".cmdSave_Click", objEx.Message);
                            MessageBox.Show("اشکال در اصلاح رکورد، تغییرات ثبت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
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
                            txtTitle.Focus();
                            break;
                        }

                    case (int)Module1.FormModeEnum.EDIT_MODE:
                    case (int)Module1.FormModeEnum.DELETE_MODE: // در صورتیکه فرم برای ویرایش یا حذف رکورد جاری فراخوانی شده باشد این قسمت اجرا خواهد شد
                        {
                            mCurrentRow = ListForm.GetRow();
                            // پر کردن کنترل فرم با مقدار رکورد جاری
                            txtTitle.Text = Conversions.ToString(mCurrentRow["ReasonTitle"]);
                            switch (ListForm.FormMode) // در صورتیکه فرم برای ویرایش(اصلاح) رکورد جاری فراخوانی شده باشد این قسمت اجرا خواهد شد
                            {
                                case (int)Module1.FormModeEnum.EDIT_MODE:
                                    {
                                        txtTitle.Focus();
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
                daReason.Update(dsChanges, "Tbl_MachineNotAvailableReasons");
                dsProductionPlanning.AcceptChanges();
            }

            dsChanges = null;
        }

        private void CreateDataAdapterCommands()
        {
            // ایجاد دستور اضافه کردن رکورد جدید در جدول
            daReason.InsertCommand = new SqlCommand("Insert Into Tbl_MachineNotAvailableReasons(ReasonCode,ReasonTitle) Values(@Code,@Title)", Module1.cnProductionPlanning);
            {
                var withBlock = daReason.InsertCommand;
                withBlock.Parameters.Add("@Code", SqlDbType.Int, default, "ReasonCode");
                withBlock.Parameters.Add("@Title", SqlDbType.VarChar, 50, "ReasonTitle");
            }
            // ایجاد دستور اصلاح رکورد جاری در جدول
            daReason.UpdateCommand = new SqlCommand("Update Tbl_MachineNotAvailableReasons Set ReasonTitle=@Title Where ReasonCode=@Code", Module1.cnProductionPlanning);
            {
                var withBlock1 = daReason.UpdateCommand;
                withBlock1.Parameters.Add("@Title", SqlDbType.VarChar, 50, "ReasonTitle");
                withBlock1.Parameters.Add("@Code", SqlDbType.Int, default, "ReasonCode").SourceVersion = DataRowVersion.Original;
            }
            // ایجاد دستور حذف رکورد جاری در جدول
            daReason.DeleteCommand = new SqlCommand("Delete From Tbl_MachineNotAvailableReasons Where ReasonCode=@Code", Module1.cnProductionPlanning);
            {
                var withBlock2 = daReason.DeleteCommand;
                withBlock2.Parameters.Add("@Code", SqlDbType.Int, default, "ReasonCode").SourceVersion = DataRowVersion.Original;
            }
        }
    }
}