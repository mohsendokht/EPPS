using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using static ProductionPlanning.MyEnums;

namespace ProductionPlanning
{
    public partial class frmProductionHalt
    {
        public frmProductionHalt()
        {
            InitializeComponent();
            _cmdSave.Name = "cmdSave";
            _cmdExit.Name = "cmdExit";
            _cmdDelete.Name = "cmdDelete";
        }

        private DataSet mdsProductionHalt;
        private int mFormMode;
        private SqlDataAdapter mdaHalt = new SqlDataAdapter();
        private DataRow mCurrentRow;
        private DataRow mProductionCurrentRow;
        private short I;

        public DataSet dsProductionHalt
        {
            set
            {
                mdsProductionHalt = value;
            }
        }

        public int FormMode
        {
            set
            {
                mFormMode = value;
            }
        }

        public DataRow ProductionCurrentRow
        {
            set
            {
                mProductionCurrentRow = value;
            }
        }

        public DataRow CurrentRow
        {
            set
            {
                mCurrentRow = value;
            }
        }

        private void frmProductionHalt_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdSave, 6);
            Module1.SetButtonsImage(cmdDelete, 9);
            Module1.SetButtonsImage(cmdExit, 14);
            FormLoad();
        }

        private void frmProductionHalt_FormClosing(object sender, FormClosingEventArgs e)
        {
            mdaHalt.Dispose();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("مشخصات توقف عملیات را حذف می کنید", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading, false) == DialogResult.Yes)
            {
                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();
                var trnDelete = Module1.cnProductionPlanning.BeginTransaction();
                try
                {
                    mdaHalt.DeleteCommand.Transaction = trnDelete;
                    mCurrentRow.Delete();
                    SaveChanges();
                    trnDelete.Commit();
                    Close();
                }
                catch (InvalidConstraintException ObjCnstEx)
                {
                    Logger.LogException("", ObjCnstEx);
                    mdsProductionHalt.RejectChanges();
                    trnDelete.Rollback();
                    MessageBox.Show("برای حذف رکورد باید رکورد(های) مرتبط با آن حذف شوند", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                }
                catch (Exception objEx)
                {
                    mdsProductionHalt.RejectChanges();
                    trnDelete.Rollback();
                    Logger.SaveError(Name + ".cmdDelete_Click", objEx.Message);
                    MessageBox.Show("حذف رکورد با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            var trnProduction = Module1.cnProductionPlanning.BeginTransaction();
            switch (mFormMode)
            {
                case (int)Module1.FormModeEnum.INSERT_MODE:
                    {
                        try
                        {
                            mdaHalt.InsertCommand.Transaction = trnProduction;
                            mCurrentRow = mdsProductionHalt.Tables["Tbl_ProductionHalts"].NewRow();
                            mCurrentRow["StartDate"] = Strings.Replace(txtStartDate.Text, "/", "");
                            switch (Strings.Left(txtStartHour.Text, 3) ?? "")
                            {
                                case "ق.ظ":
                                    {
                                        mCurrentRow["StartHour"] = Strings.Right(txtStartHour.Text, 2) + ":" + Strings.Mid(txtStartHour.Text, 8, 2);
                                        break;
                                    }

                                case "ب.ظ":
                                    {
                                        mCurrentRow["StartHour"] = Conversions.ToString(Interaction.IIf(Conversions.ToInteger(Strings.Right(txtStartHour.Text, 2)) + 12 < 24, Conversions.ToInteger(Strings.Right(txtStartHour.Text, 2)) + 12, "00")) + ":" + Strings.Mid(txtStartHour.Text, 8, 2);
                                        break;
                                    }
                            }

                            mCurrentRow["EndDate"] = Strings.Replace(txtEndDate.Text, "/", "");
                            switch (Strings.Left(txtEndHour.Text, 3) ?? "")
                            {
                                case "ق.ظ":
                                    {
                                        mCurrentRow["EndHour"] = Strings.Right(txtEndHour.Text, 2) + ":" + Strings.Mid(txtEndHour.Text, 8, 2);
                                        break;
                                    }

                                case "ب.ظ":
                                    {
                                        mCurrentRow["EndHour"] = Conversions.ToString(Interaction.IIf(Conversions.ToInteger(Strings.Right(txtEndHour.Text, 2)) + 12 < 24, Conversions.ToInteger(Strings.Right(txtEndHour.Text, 2)) + 12, "00")) + ":" + Strings.Mid(txtEndHour.Text, 8, 2);
                                        break;
                                    }
                            }

                            mCurrentRow["Duration"] = GetHaltDuration();
                            mCurrentRow["ProductionCode"] = mProductionCurrentRow["ProductionCode"];
                            mdsProductionHalt.Tables["Tbl_ProductionHalts"].Rows.Add(mCurrentRow);
                            SaveChanges();
                            trnProduction.Commit();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            mdsProductionHalt.RejectChanges();
                            trnProduction.Rollback();
                            Logger.SaveError(Name + ".cmdSave_Click", objEx.Message);
                            MessageBox.Show("اشکال در ثبت رکورد جدید، رکورد جدید ثبت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            trnProduction.Dispose();
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                        }

                        break;
                    }

                case (int)Module1.FormModeEnum.EDIT_MODE:
                    {
                        try
                        {
                            mdaHalt.UpdateCommand.Transaction = trnProduction;
                            mCurrentRow.BeginEdit();
                            mCurrentRow["StartDate"] = Strings.Replace(txtStartDate.Text, "/", "");
                            switch (Strings.Left(txtStartHour.Text, 3) ?? "")
                            {
                                case "ق.ظ":
                                    {
                                        mCurrentRow["StartHour"] = Strings.Right(txtStartHour.Text, 2) + ":" + Strings.Mid(txtStartHour.Text, 8, 2);
                                        break;
                                    }

                                case "ب.ظ":
                                    {
                                        mCurrentRow["StartHour"] = Conversions.ToString(Interaction.IIf(Conversions.ToInteger(Strings.Right(txtStartHour.Text, 2)) + 12 < 24, Conversions.ToInteger(Strings.Right(txtStartHour.Text, 2)) + 12, "00")) + ":" + Strings.Mid(txtStartHour.Text, 8, 2);
                                        break;
                                    }
                            }

                            mCurrentRow["EndDate"] = Strings.Replace(txtEndDate.Text, "/", "");
                            switch (Strings.Left(txtEndHour.Text, 3) ?? "")
                            {
                                case "ق.ظ":
                                    {
                                        mCurrentRow["EndHour"] = Strings.Right(txtEndHour.Text, 2) + ":" + Strings.Mid(txtEndHour.Text, 8, 2);
                                        break;
                                    }

                                case "ب.ظ":
                                    {
                                        mCurrentRow["EndHour"] = Conversions.ToString(Interaction.IIf(Conversions.ToInteger(Strings.Right(txtEndHour.Text, 2)) + 12 < 24, Conversions.ToInteger(Strings.Right(txtEndHour.Text, 2)) + 12, "00")) + ":" + Strings.Mid(txtEndHour.Text, 8, 2);
                                        break;
                                    }
                            }

                            mCurrentRow["Duration"] = GetHaltDuration();
                            mCurrentRow["ProductionCode"] = mProductionCurrentRow["ProductionCode"];
                            mCurrentRow.EndEdit();
                            SaveChanges();
                            trnProduction.Commit();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            mCurrentRow.CancelEdit();
                            trnProduction.Rollback();
                            Logger.SaveError(Name + ".cmdSave_Click", objEx.Message);
                            MessageBox.Show("تغییرات ثبت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                        }
                        finally
                        {
                            trnProduction.Dispose();
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                        }

                        break;
                    }
            }
        }

        private void FormLoad()
        {
            try
            {
                CreateDataAdapterCommands();
                switch (mFormMode)
                {
                    case (int)Module1.FormModeEnum.INSERT_MODE: // در صورتیکه فرم برای ایجاد رکورد جدید فراخوانی شده باشد این قسمت اجرا خواهد شد
                        {
                            txtStartDate.Text = Module1.mServerShamsiDate;
                            txtStartHour.Text = DateAndTime.Now.TimeOfDay.ToString();
                            txtEndDate.Text = Module1.mServerShamsiDate;
                            txtEndHour.Text = DateAndTime.Now.TimeOfDay.ToString();
                            txtStartDate.Focus();
                            break;
                        }

                    case (int)Module1.FormModeEnum.EDIT_MODE:
                    case (int)Module1.FormModeEnum.DELETE_MODE: // در صورتیکه فرم برای ویرایش یا حذف رکورد جاری فراخوانی شده باشد این قسمت اجرا خواهد شد
                        {
                            FillControls();
                            switch (mFormMode) // در صورتیکه فرم برای ویرایش(اصلاح) رکورد جاری فراخوانی شده باشد این قسمت اجرا خواهد شد
                            {
                                case (int)Module1.FormModeEnum.EDIT_MODE:
                                    {
                                        txtStartDate.Focus();
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
                Logger.SaveError(Name + ".FormLoad", objEx.Message);
                MessageBox.Show("فراخوانی فرم با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillControls()
        {
            txtStartDate.Text = Conversions.ToString(mCurrentRow["StartDate"]);
            txtStartHour.Text = Conversions.ToString(mCurrentRow["StartHour"]);
            txtEndDate.Text = Conversions.ToString(mCurrentRow["EndDate"]);
            txtEndHour.Text = Conversions.ToString(mCurrentRow["EndHour"]);
        }

        private string GetHaltDuration()
        {
            string StartHour = 0.ToString();
            string EndHour = 0.ToString();
            string HaltDuration = 0.ToString();
            if ((txtStartDate.Text ?? "") == (txtEndDate.Text ?? ""))
            {
                switch (Strings.Left(txtStartHour.Text, 3) ?? "")
                {
                    case "ق.ظ":
                        {
                            StartHour = Strings.Right(txtStartHour.Text, 2) + ":" + Strings.Mid(txtStartHour.Text, 8, 2);
                            break;
                        }

                    case "ب.ظ":
                        {
                            StartHour = Conversions.ToString(Interaction.IIf(Conversions.ToInteger(Strings.Right(txtStartHour.Text, 2)) + 12 < 24, Conversions.ToInteger(Strings.Right(txtStartHour.Text, 2)) + 12, "00")) + ":" + Strings.Mid(txtStartHour.Text, 8, 2);
                            break;
                        }
                }

                switch (Strings.Left(txtEndHour.Text, 3) ?? "")
                {
                    case "ق.ظ":
                        {
                            EndHour = Strings.Right(txtEndHour.Text, 2) + ":" + Strings.Mid(txtEndHour.Text, 8, 2);
                            break;
                        }

                    case "ب.ظ":
                        {
                            EndHour = Conversions.ToString(Interaction.IIf(Conversions.ToInteger(Strings.Right(txtEndHour.Text, 2)) + 12 < 24, Conversions.ToInteger(Strings.Right(txtEndHour.Text, 2)) + 12, "00")) + ":" + Strings.Mid(txtEndHour.Text, 8, 2);
                            break;
                        }
                }

                HaltDuration = (TimeSpan.Parse(EndHour) - TimeSpan.Parse(StartHour)).ToString();
            }
            else
            {
                string CalendarStart = 0.ToString();
                string CalendarEnd = 0.ToString();
                short DurationDays = 0;
                string AddedDate = Strings.Replace(txtStartDate.Text, "/", "");

                // بدست آوردن تعداد روزهای احتلاف بین تاریخ شروع و تاریخ پایان
                while (true)
                {
                    AddedDate = FarsiDateFunctions.AddToDate(AddedDate, "00000001");
                    DurationDays = (short)(DurationDays + 1);
                    if ((AddedDate ?? "") == (Strings.Replace(txtEndDate.Text, "/", "") ?? ""))
                    {
                        break;
                    }
                }

                AddedDate = Strings.Replace(txtStartDate.Text, "/", "");
                var loopTo = DurationDays;
                for (I = 0; I <= loopTo; I++)
                {
                    if (I == 0)
                    {
                        GetDayStartEnd(Strings.Replace(txtStartDate.Text, "/", ""), ref CalendarStart, ref CalendarEnd);
                        switch (Strings.Left(txtStartHour.Text, 3) ?? "")
                        {
                            case "ق.ظ":
                                {
                                    StartHour = Strings.Right(txtStartHour.Text, 2) + ":" + Strings.Mid(txtStartHour.Text, 8, 2);
                                    break;
                                }

                            case "ب.ظ":
                                {
                                    StartHour = Conversions.ToString(Interaction.IIf(Conversions.ToInteger(Strings.Right(txtStartHour.Text, 2)) + 12 < 24, Conversions.ToInteger(Strings.Right(txtStartHour.Text, 2)) + 12, "00")) + ":" + Strings.Mid(txtStartHour.Text, 8, 2);
                                    break;
                                }
                        }

                        HaltDuration = (TimeSpan.Parse(CalendarEnd) - TimeSpan.Parse(StartHour)).ToString();
                    }
                    else if (I == DurationDays)
                    {
                        GetDayStartEnd(AddedDate, ref CalendarStart, ref CalendarEnd);
                        switch (Strings.Left(txtEndHour.Text, 3) ?? "")
                        {
                            case "ق.ظ":
                                {
                                    EndHour = Strings.Right(txtEndHour.Text, 2) + ":" + Strings.Mid(txtEndHour.Text, 8, 2);
                                    break;
                                }

                            case "ب.ظ":
                                {
                                    EndHour = Conversions.ToString(Interaction.IIf(Conversions.ToInteger(Strings.Right(txtEndHour.Text, 2)) + 12 < 24, Conversions.ToInteger(Strings.Right(txtEndHour.Text, 2)) + 12, "00")) + ":" + Strings.Mid(txtEndHour.Text, 8, 2);
                                    break;
                                }
                        }

                        HaltDuration = (TimeSpan.Parse(HaltDuration) + (TimeSpan.Parse(EndHour) - TimeSpan.Parse(CalendarStart))).ToString();
                    }
                    else
                    {
                        GetDayStartEnd(AddedDate, ref CalendarStart, ref CalendarEnd);
                        HaltDuration = (TimeSpan.Parse(HaltDuration) + (TimeSpan.Parse(CalendarEnd) - TimeSpan.Parse(CalendarStart))).ToString();
                    }

                    AddedDate = FarsiDateFunctions.AddToDate(AddedDate, "00000001");
                }
            }

            HaltDuration = Strings.Mid(HaltDuration, 1, 5);
            return HaltDuration;
        }

        private void GetDayStartEnd(string ShamsiDate, ref string CalendarStart, ref string CalendarEnd)
        {
            string CalendarCode;
            mdsProductionHalt.Tables["Tbl_ProductOPCs"].DefaultView.RowFilter = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("TreeCode=", mProductionCurrentRow["TreeCode"]), " And OperationCode='"), mProductionCurrentRow["OperationCode"]), "'"));
            switch ((EnumExecutionMethod)mdsProductionHalt.Tables["Tbl_ProductOPCs"].DefaultView[0]["ExecutionMethod"])
            {
                case EnumExecutionMethod.EM_MACHINE:
                    {
                        CalendarCode = GetCalendarCode((short)EnumExecutionMethod.EM_MACHINE, Conversions.ToString(mProductionCurrentRow["OperationCode"]), Conversions.ToString(mProductionCurrentRow["MachineCode"]));
                        CalendarStart = GetDayStart(CalendarCode, ref ShamsiDate);
                        CalendarEnd = GetDayEnd(CalendarCode, ref ShamsiDate);
                        break;
                    }

                case EnumExecutionMethod.EM_NONMACHINE:
                    {
                        CalendarCode = GetCalendarCode((short)EnumExecutionMethod.EM_MACHINE, Conversions.ToString(mProductionCurrentRow["OperationCode"]));
                        CalendarStart = GetDayStart(CalendarCode, ref ShamsiDate);
                        CalendarEnd = GetDayEnd(CalendarCode, ref ShamsiDate);
                        break;
                    }

                case EnumExecutionMethod.EM_CONTRACTOR:
                    {
                        CalendarStart = 0.ToString();
                        CalendarEnd = 24.ToString();
                        break;
                    }
            }
        }

        /// <summary>
    /// این تابع کد تقویم را در عملیاتهایی که با ماشین یا
    /// بوسیله اپراتور و بدون ماشین انجام می شود را باز می گرداند
    /// </summary>
        private string GetCalendarCode(short ExecutionMethod, string OperationCode, string MachineCode = "-1")
        {
            if (ExecutionMethod == (short)EnumExecutionMethod.EM_MACHINE)
            {
                mdsProductionHalt.Tables["Tbl_Machines"].DefaultView.RowFilter = "Code='" + MachineCode + "'";
                return Conversions.ToString(mdsProductionHalt.Tables["Tbl_Machines"].DefaultView[0]["CalendarCode"]);
            }
            else
            {
                mdsProductionHalt.Tables["Tbl_ProductOPCsExecutorMachines"].DefaultView.RowFilter = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("TreeCode=", mProductionCurrentRow["TreeCode"]), " And OperationCode='"), OperationCode), "'"));
                return Conversions.ToString(mdsProductionHalt.Tables["Tbl_ProductOPCsExecutorMachines"].DefaultView[0]["CalendarCode"]);
            }
        }

        /// <summary>
    /// این تابع ساعت شروع یک روز را باز می گرداند
    /// </summary>
        private string GetDayStart(string CalendarCode, ref string ShamsiDate)
        {
            short DayType;
            string StartDay = "0:0";

            // کنترل اینکه تاریخ داده شده روز خاص می باشد یا نه
            DayType = IsParticularDay(CalendarCode, ShamsiDate);
            switch (DayType)
            {
                case -1: // تاریخ داده یک روز پیش فرض می باشد
                    {
                        short DayNo = Module1.GetDayNo(ShamsiDate);
                        mdsProductionHalt.Tables["Tbl_CalendarDays"].DefaultView.RowFilter = "CalendarCode=" + CalendarCode + " And DayNo=" + DayNo + " And ShiftNo=1";
                        StartDay = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(mdsProductionHalt.Tables["Tbl_CalendarDays"].DefaultView[0]["StartHour"], ":"), mdsProductionHalt.Tables["Tbl_CalendarDays"].DefaultView[0]["StartMinute"]));
                        break;
                    }

                case 2: // تاریخ داده شده یک روز خاص عادی می باشد
                    {
                        mdsProductionHalt.Tables["Tbl_CalendarParticularShifts"].DefaultView.RowFilter = "CalendarCode=" + CalendarCode + " And ShamsiDate=" + ShamsiDate + " And ShiftNo=1";
                        StartDay = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(mdsProductionHalt.Tables["Tbl_CalendarParticularShifts"].DefaultView[0]["StartHour"], ":"), mdsProductionHalt.Tables["Tbl_CalendarParticularShifts"].DefaultView[0]["StartMinute"]));
                        break;
                    }
            }

            return StartDay;
        }

        /// <summary>
    /// این تابع ساعت پایان یک روز را باز می گرداند
    /// </summary>
        private string GetDayEnd(string CalendarCode, ref string ShamsiDate)
        {
            short DayType;
            string DayEnd = "0:0";

            // کنترل اینکه تاریخ داده شده روز خاص می باشد یا نه
            DayType = IsParticularDay(CalendarCode, ShamsiDate);
            switch (DayType)
            {
                case -1: // تاریخ داده یک روز پیش فرض می باشد
                    {
                        short DayNo = Module1.GetDayNo(ShamsiDate);
                        mdsProductionHalt.Tables["Tbl_CalendarDays"].DefaultView.RowFilter = "CalendarCode=" + CalendarCode + " And DayNo=" + DayNo;
                        mdsProductionHalt.Tables["Tbl_CalendarDays"].DefaultView.Sort = "ShiftNo Desc";
                        DayEnd = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.AddObject(Operators.AddObject(mdsProductionHalt.Tables["Tbl_CalendarDays"].DefaultView[0]["StartHour"], mdsProductionHalt.Tables["Tbl_CalendarDays"].DefaultView[0]["DurationHour"]), mdsProductionHalt.Tables["Tbl_CalendarDays"].DefaultView[0]["ExtraTimeHour"]), ":"), Operators.AddObject(Operators.AddObject(mdsProductionHalt.Tables["Tbl_CalendarDays"].DefaultView[0]["StartMinute"], mdsProductionHalt.Tables["Tbl_CalendarDays"].DefaultView[0]["DurationMinute"]), mdsProductionHalt.Tables["Tbl_CalendarDays"].DefaultView[0]["ExtraTimeMinute"])));
                        break;
                    }

                case 2: // تاریخ داده شده یک روز خاص عادی می باشد
                    {
                        mdsProductionHalt.Tables["Tbl_CalendarParticularShifts"].DefaultView.RowFilter = "CalendarCode=" + CalendarCode + " And ShamsiDate=" + ShamsiDate;
                        mdsProductionHalt.Tables["Tbl_CalendarParticularShifts"].DefaultView.Sort = "ShiftNo Desc";
                        DayEnd = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.AddObject(Operators.AddObject(mdsProductionHalt.Tables["Tbl_CalendarParticularShifts"].DefaultView[0]["StartHour"], mdsProductionHalt.Tables["Tbl_CalendarParticularShifts"].DefaultView[0]["DurationHour"]), mdsProductionHalt.Tables["Tbl_CalendarParticularShifts"].DefaultView[0]["ExtraTimeHour"]), ":"), Operators.AddObject(Operators.AddObject(mdsProductionHalt.Tables["Tbl_CalendarParticularShifts"].DefaultView[0]["StartMinute"], mdsProductionHalt.Tables["Tbl_CalendarParticularShifts"].DefaultView[0]["DurationMinute"]), mdsProductionHalt.Tables["Tbl_CalendarParticularShifts"].DefaultView[0]["ExtraTimeMinute"])));
                        break;
                    }
            }

            return DayEnd;
        }

        /// <summary>
    /// این تابع مشخص می کند یک تاریخ مشخص در تقویم روز خاص می باشد یا نه
    /// </summary>
        private short IsParticularDay(string CalendarCode, string ShamsiDate)
        {
            mdsProductionHalt.Tables["Tbl_CalendarParticularDays"].DefaultView.RowFilter = "CalendarCode=" + CalendarCode + " And ShamsiDate=" + ShamsiDate;
            if (mdsProductionHalt.Tables["Tbl_CalendarParticularDays"].DefaultView.Count == 0)
            {
                return -1;
            }

            return Conversions.ToShort(mdsProductionHalt.Tables["Tbl_CalendarParticularDays"].DefaultView[0]["DayType"]);
        }

        private void SaveChanges()
        {
            DataSet dsChanges;
            dsChanges = mdsProductionHalt.GetChanges();
            if (dsChanges.HasErrors)
            {
                mdsProductionHalt.RejectChanges();
            }
            else
            {
                mdaHalt.Update(dsChanges, "Tbl_ProductionHalts");
                mdsProductionHalt.AcceptChanges();
            }

            dsChanges = null;
        }

        private void CreateDataAdapterCommands()
        {
            // ایجاد دستور اصلاح رکورد جاری در جدول
            mdaHalt.InsertCommand = new SqlCommand("Insert Into Tbl_ProductionHalts(StartDate,StartHour,EndDate,EndHour,Duration,ProductionCode)" + "Values(@StartDate,@StartHour,@EndDate,@EndHour,@Duration,@ProductionCode)", Module1.cnProductionPlanning);
            {
                var withBlock = mdaHalt.InsertCommand;
                withBlock.Parameters.Add("@StartDate", SqlDbType.VarChar, 8, "StartDate");
                withBlock.Parameters.Add("@StartHour", SqlDbType.VarChar, 50, "StartHour");
                withBlock.Parameters.Add("@EndDate", SqlDbType.VarChar, 8, "EndDate");
                withBlock.Parameters.Add("@EndHour", SqlDbType.VarChar, 50, "EndHour");
                withBlock.Parameters.Add("@Duration", SqlDbType.VarChar, 50, "Duration");
                withBlock.Parameters.Add("@ProductionCode", SqlDbType.Int, default, "ProductionCode");
            }
            // ایجاد دستور ویرایش رکورد در جدول
            mdaHalt.UpdateCommand = new SqlCommand("Update Tbl_ProductionHalts Set StartDate=@StartDate,StartHour=@StartHour,EndDate=@EndDate," + "EndHour=@EndHour,Duration=@Duration,ProductionCode=@ProductionCode Where HaltID=@HaltID", Module1.cnProductionPlanning);
            {
                var withBlock1 = mdaHalt.UpdateCommand;
                withBlock1.Parameters.Add("@StartDate", SqlDbType.VarChar, 8, "StartDate");
                withBlock1.Parameters.Add("@StartHour", SqlDbType.VarChar, 50, "StartHour");
                withBlock1.Parameters.Add("@EndDate", SqlDbType.VarChar, 8, "EndDate");
                withBlock1.Parameters.Add("@EndHour", SqlDbType.VarChar, 50, "EndHour");
                withBlock1.Parameters.Add("@Duration", SqlDbType.VarChar, 50, "Duration");
                withBlock1.Parameters.Add("@ProductionCode", SqlDbType.Int, default, "ProductionCode");
                withBlock1.Parameters.Add("@HaltID", SqlDbType.Int, default, "HaltID");
                withBlock1.Parameters[6].Direction = ParameterDirection.Input;
                withBlock1.Parameters[6].SourceVersion = DataRowVersion.Original;
            }
            // ایجاد دستور حذف رکورد جاری در جدول
            mdaHalt.DeleteCommand = new SqlCommand("Delete From Tbl_ProductionHalts Where HaltID=@HaltID", Module1.cnProductionPlanning);
            {
                var withBlock2 = mdaHalt.DeleteCommand;
                withBlock2.Parameters.Add("@HaltID", SqlDbType.Int, default, "HaltID");
                withBlock2.Parameters[0].Direction = ParameterDirection.Input;
                withBlock2.Parameters[0].SourceVersion = DataRowVersion.Original;
            }
        }

        private bool FormValidation()
        {
            if (string.IsNullOrEmpty(Strings.Trim(Strings.Replace(txtStartDate.Text, "/", ""))))
            {
                MessageBox.Show("تاریخ شروع توقف را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtStartDate.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(Strings.Trim(Strings.Replace(txtEndDate.Text, "/", ""))))
            {
                MessageBox.Show("تاریخ پایان توقف را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtEndDate.Focus();
                return false;
            }

            return true;
        }
    }
}