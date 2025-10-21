using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmHoliday
    {
        public frmHoliday()
        {
            InitializeComponent();
            _cmdSave.Name = "cmdSave";
            _cmdExit.Name = "cmdExit";
            _cmdDelete.Name = "cmdDelete";
        }

        private frmRecordsLists mListForm;
        private SqlDataAdapter daHoliday = new SqlDataAdapter();
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

        private void frmOperationTitle_FormClosing(object sender, FormClosingEventArgs e)
        {
            dsProductionPlanning.RejectChanges();
            CurrentRow = null;
            daHoliday.Dispose();
            mListForm = null;
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("روز تعطیل را حذف می کنید", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading, false) == DialogResult.Yes)
            {
                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();
                var trnDelete = Module1.cnProductionPlanning.BeginTransaction();
                try
                {
                    daHoliday.DeleteCommand.Transaction = trnDelete;
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
            var trnHoliday = Module1.cnProductionPlanning.BeginTransaction();
            switch (ListForm.FormMode)
            {
                case (int)Module1.FormModeEnum.INSERT_MODE:
                    {
                        try
                        {
                            daHoliday.InsertCommand.Transaction = trnHoliday;
                            // اضافه کردن رکورد جدید به جدول در دیتاست
                            CurrentRow = dsProductionPlanning.Tables["Tbl_HoliDays"].NewRow();
                            CurrentRow["DayNo"] = txtDay.Value;
                            CurrentRow["MonthNo"] = cbMonth.SelectedIndex + 1;
                            CurrentRow["Month"] = cbMonth.Text;
                            CurrentRow["Description"] = txtDescription.Text;
                            dsProductionPlanning.Tables["Tbl_HoliDays"].Rows.Add(CurrentRow);
                            SaveChanges();
                            trnHoliday.Commit();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            dsProductionPlanning.RejectChanges();
                            trnHoliday.Rollback();
                            MessageBox.Show("اشکال در ثبت رکورد جدید، رکورد جدید ثبت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            MessageBox.Show(objEx.Message);
                        }
                        finally
                        {
                            trnHoliday.Dispose();
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                        }

                        break;
                    }

                case (int)Module1.FormModeEnum.EDIT_MODE:
                    {
                        try
                        {
                            daHoliday.UpdateCommand.Transaction = trnHoliday;
                            CurrentRow.BeginEdit();
                            CurrentRow["DayNo"] = txtDay.Value;
                            CurrentRow["MonthNo"] = cbMonth.SelectedIndex + 1;
                            CurrentRow["Month"] = cbMonth.Text;
                            CurrentRow["Description"] = txtDescription.Text;
                            CurrentRow.EndEdit();
                            SaveChanges();
                            trnHoliday.Commit();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            CurrentRow.CancelEdit();
                            trnHoliday.Rollback();
                            MessageBox.Show("اشکال در اصلاح رکورد، تغییرات ثبت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            MessageBox.Show(objEx.Message);
                        }
                        finally
                        {
                            trnHoliday.Dispose();
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
                            cbMonth.Focus();
                            break;
                        }

                    case (int)Module1.FormModeEnum.EDIT_MODE:
                    case (int)Module1.FormModeEnum.DELETE_MODE: // در صورتیکه فرم برای ویرایش یا حذف رکورد جاری فراخوانی شده باشد این قسمت اجرا خواهد شد
                        {
                            CurrentRow = ListForm.GetRow();
                            // پر کردن کنترل فرم با مقدار رکورد جاری
                            txtDay.Text = Conversions.ToString(CurrentRow["DayNo"]);
                            cbMonth.SelectedIndex = Conversions.ToInteger(Operators.SubtractObject(CurrentRow["MonthNo"], 1));
                            txtDescription.Text = Conversions.ToString(CurrentRow["Description"]);
                            switch (ListForm.FormMode) // در صورتیکه فرم برای ویرایش(اصلاح) رکورد جاری فراخوانی شده باشد این قسمت اجرا خواهد شد
                            {
                                case (int)Module1.FormModeEnum.EDIT_MODE:
                                    {
                                        cbMonth.Focus();
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
                daHoliday.Update(dsChanges, "Tbl_HoliDays");
                dsProductionPlanning.AcceptChanges();
            }

            dsChanges = null;
        }

        private void CreateDataAdapterCommands()
        {
            // ایجاد دستور اضافه کردن رکورد جدید در جدول
            daHoliday.InsertCommand = new SqlCommand("Insert Into Tbl_HoliDays(DayNo,MonthNo,Description) Values(@DayNo,@MonthNo,@Description)", Module1.cnProductionPlanning);
            {
                var withBlock = daHoliday.InsertCommand;
                withBlock.Parameters.Add("@DayNo", SqlDbType.TinyInt, default, "DayNo");
                withBlock.Parameters.Add("@MonthNo", SqlDbType.TinyInt, default, "MonthNo");
                withBlock.Parameters.Add("@Description", SqlDbType.VarChar, 255, "Description");
            }
            // ایجاد دستور اصلاح رکورد جاری در جدول
            daHoliday.UpdateCommand = new SqlCommand("Update Tbl_HoliDays Set DayNo=@CurrentDayNo,MonthNo=@CurrentMonthNo,Description=@Description Where DayNo=@OldDayNo And MonthNo=@OldMonthNo", Module1.cnProductionPlanning);
            {
                var withBlock1 = daHoliday.UpdateCommand;
                withBlock1.Parameters.Add("@CurrentDayNo", SqlDbType.TinyInt, default, "DayNo");
                withBlock1.Parameters[0].Direction = ParameterDirection.Input;
                withBlock1.Parameters[0].SourceVersion = DataRowVersion.Current;
                withBlock1.Parameters.Add("@OldDayNo", SqlDbType.TinyInt, default, "DayNo");
                withBlock1.Parameters[1].Direction = ParameterDirection.Input;
                withBlock1.Parameters[1].SourceVersion = DataRowVersion.Original;
                withBlock1.Parameters.Add("@CurrentMonthNo", SqlDbType.TinyInt, default, "MonthNo");
                withBlock1.Parameters[2].Direction = ParameterDirection.Input;
                withBlock1.Parameters[2].SourceVersion = DataRowVersion.Current;
                withBlock1.Parameters.Add("@OldMonthNo", SqlDbType.TinyInt, default, "MonthNo");
                withBlock1.Parameters[3].Direction = ParameterDirection.Input;
                withBlock1.Parameters[3].SourceVersion = DataRowVersion.Original;
                withBlock1.Parameters.Add("@Description", SqlDbType.VarChar, 255, "Description");
            }
            // ایجاد دستور حذف رکورد جاری در جدول
            daHoliday.DeleteCommand = new SqlCommand("Delete From Tbl_HoliDays Where DayNo=@DayNo And MonthNo=@MonthNo", Module1.cnProductionPlanning);
            {
                var withBlock2 = daHoliday.DeleteCommand;
                withBlock2.Parameters.Add("@DayNo", SqlDbType.TinyInt, default, "DayNo");
                withBlock2.Parameters.Add("@MonthNo", SqlDbType.TinyInt, default, "MonthNo");
            }
        }

        private bool FormValidation()
        {
            if (cbMonth.SelectedIndex == -1)
            {
                MessageBox.Show("نام ماه را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbMonth.Focus();
                return false;
            }

            return true;
        }
    }
}