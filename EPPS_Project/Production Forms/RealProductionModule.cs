using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    static class RealProductionModule
    {
        private static string FindOperationCode(string MyRecordNo)
        {
            string OperationCode = "'-1'";
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmOperationCode = new SqlCommand("Select OperationCode From Tbl_Planning Where PlanningCode = " + MyRecordNo, cn);
                var drOperationCode = cmOperationCode.ExecuteReader();
                if (drOperationCode.Read())
                {
                    OperationCode = Conversions.ToString(drOperationCode["OperationCode"]);
                }

                drOperationCode.Close();
                cn.Close();
            }

            return OperationCode;
        }

        public static string FindCalendarCode(string MachineCode, string MyTreeCode, string MyOperationCode)
        {
            string CalendarCode = "0";
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmCalendar = new SqlCommand("", cn);
                SqlDataReader drCalendar = null;
                switch (MachineCode ?? "")
                {
                    case "-1":
                        {
                            cmCalendar.CommandText = "Select CalendarCode From dbo.Tbl_ProductOPCsExecutorMachines Where TreeCode = " + MyTreeCode + " And MachineCode = '" + MachineCode + "' And OperationCode = '" + MyOperationCode + "'";
                            drCalendar = cmCalendar.ExecuteReader();
                            if (drCalendar.Read())
                            {
                                CalendarCode = Conversions.ToString(drCalendar["CalendarCode"]);
                            }

                            drCalendar.Close();
                            break;
                        }

                    default:
                        {
                            cmCalendar.CommandText = "Select CalendarCode From Tbl_Machines Where Code = '" + MachineCode + "'";
                            drCalendar = cmCalendar.ExecuteReader();
                            if (drCalendar.Read())
                            {
                                CalendarCode = Conversions.ToString(drCalendar["CalendarCode"]);
                            }

                            drCalendar.Close();
                            break;
                        }
                }

                cn.Close();
            }

            return CalendarCode;
        }

        public static double Calculate_ProductionDuration(string CalendarCode, string MyStartDate, string MyStartHour, string MyEndHour)
        {
            double ProductionDuration = 0d;
            string Query = Constants.vbNullString;
            string ShamsiDate = Strings.Replace(MyStartDate, "/", "");
            var DataSetConfig = new DataSetConfiguration();
            MyEnums.EnumParticularDayType ParticularDayType = (MyEnums.EnumParticularDayType) Module1.IsParticularDay(CalendarCode, ShamsiDate); 
            switch (ParticularDayType)
            {
                case MyEnums.EnumParticularDayType.PDT_UNKNOWN:
                case MyEnums.EnumParticularDayType.PDT_IS_NOT:  // 'روز عادي مي باشد
                    {
                        var cmIsHoliday = new SqlCommand("Select Distinct DayType From Tbl_CalendarDays Where CalendarCode=" + CalendarCode + " And DayNo=" + Module1.GetDayNo(ShamsiDate), Module1.cnProductionPlanning);
                        if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                            Module1.cnProductionPlanning.Open();
                        short.TryParse((cmIsHoliday.ExecuteScalar() == DBNull.Value ? 1 : cmIsHoliday.ExecuteScalar()).ToString(), out short dayType);
                        switch ((MyEnums.EnumDayType)dayType)
                        {
                            case MyEnums.EnumDayType.DT_OFF_DAY: // روز عادی تعطیل می باشد - جمعه 
                                {
                                    cmIsHoliday.Dispose();
                                    if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                        Module1.cnProductionPlanning.Close();
                                    // MessageBox.Show("تاریخ تولید وارد شده یک روز تعطیل می باشد", MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)

                                    ProductionDuration = 0d;
                                    goto Exit_Function;
                                    //break;
                                }

                            case MyEnums.EnumDayType.DT_WORK_DAY: // روز های غیر تعطیل هفته - روز عادی کاری  می باشد
                                {
                                    short DayNo = Conversions.ToShort(Strings.Right(ShamsiDate, 2));
                                    short MonthNo = Conversions.ToShort(Strings.Mid(ShamsiDate, 5, 2));
                                    cmIsHoliday.CommandText = "Select Count(*) From Tbl_HoliDays Where DayNo=" + DayNo + " And MonthNo=" + MonthNo;
                                    int IsHoliday = (int) cmIsHoliday.ExecuteScalar();
                                    if (IsHoliday > 0) // روز عادی تعطیل می باشد - جزئ تعطیلات ثابت تقویم 
                                    {
                                        cmIsHoliday.Dispose();
                                        if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                            Module1.cnProductionPlanning.Close();
                                        // MessageBox.Show("تاریخ تولید وارد شده یک روز تعطیل می باشد", MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)

                                        ProductionDuration = 0d;
                                        goto Exit_Function;
                                    }
                                    else // روز عادی کاری  می باشد
                                    {
                                        Query = "Select * From Tbl_CalendarShifts Where CalendarCode=" + CalendarCode + " Order By ShiftNo";
                                        DataSetConfig.FillDataSet("Tbl_CalendarShifts", "Tbl_Shifts", Query, "CalendarCode", "ShiftNo");
                                        Query = "Select * From Tbl_CalendarShiftDownTimes Where CalendarCode=" + CalendarCode + " Order By DownTimeStart";
                                        DataSetConfig.FillDataSet("Tbl_CalendarShiftDownTimes", "Tbl_CalendarShiftDownTimes", Query, "CalendarCode", "ShiftNo", "DownTimeStart");
                                    }

                                    break;
                                }
                        }

                        cmIsHoliday.Dispose();
                        if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                            Module1.cnProductionPlanning.Close();
                        break;
                    }

                case MyEnums.EnumParticularDayType.PDT_OFF_DAY: // روز خاص تعطيل مي باشد
                    {
                        // MessageBox.Show("تاریخ تولید وارد شده یک روز تعطیل می باشد", MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)

                        ProductionDuration = 0d;
                        goto Exit_Function;
                        //break;
                    }

                case MyEnums.EnumParticularDayType.PDT_WORK_DAY: // روز خاص کاری مي باشد
                    {
                        Query = "Select * From Tbl_CalendarParticularShifts Where CalendarCode=" + CalendarCode + " And ShamsiDate=" + ShamsiDate + " Order By ShamsiDate,ShiftNo";
                        DataSetConfig.FillDataSet("Tbl_CalendarParticularShifts", "Tbl_Shifts", Query, "CalendarCode", "ShamsiDate", "ShiftNo");
                        Query = "Select * From Tbl_ParticularShiftDownTimes Where CalendarCode=" + CalendarCode + " And ShamsiDate=" + ShamsiDate + " Order By DownTimeStart";
                        DataSetConfig.FillDataSet("Tbl_ParticularShiftDownTimes", "Tbl_CalendarShiftDownTimes", Query, "CalendarCode", "ShamsiDate", "ShiftNo", "DownTimeStart");
                        break;
                    }
            }

            var dvShifts = DataSetConfig.dsProductionPlanning.Tables["Tbl_Shifts"].DefaultView;
            string StartTime = MyStartHour;   // ساعت شروع تولید
            string EndTime = MyEndHour;
            string ShiftStartTime = "00:00";
            string ShiftEndTime = "00:00";
            string WorkTimes = "00:00";
            string DownTimes = "00:00";
            string ShiftDownTimeStart = "00:00";
            string ShiftDownTimeEnd = "00:00";
            int I;
            var loopTo = dvShifts.Count - 1;
            for (I = 0; I <= loopTo; I++)
            {
                ShiftStartTime = dvShifts[I]["ShiftStart"].ToString();
                var mShiftEndTime = TimeSpan.Parse(dvShifts[I]["ShiftStart"].ToString()) + TimeSpan.Parse(dvShifts[I]["ShiftDuration"].ToString()) + TimeSpan.Parse(dvShifts[I]["ShiftExtraTime"].ToString());
                if (mShiftEndTime >= TimeSpan.Parse("1.00:00"))
                {
                    ShiftEndTime = (mShiftEndTime - TimeSpan.Parse("1.00:00")).ToString().Substring(0, 5);
                }
                else
                {
                    ShiftEndTime = mShiftEndTime.ToString().Substring(0, 5);
                }

                if (I > 0)
                {
                    StartTime = ShiftStartTime;
                }

                WorkTimes = (TimeSpan.Parse(WorkTimes) + (TimeSpan.Parse(EndTime) - TimeSpan.Parse(StartTime))).ToString();
                string mDTFilter = "ShiftNo=" + dvShifts[I]["ShiftNo"].ToString();
                var drDowntimes = DataSetConfig.dsProductionPlanning.Tables["Tbl_CalendarShiftDownTimes"].Select(mDTFilter);
                foreach (DataRow r in drDowntimes)
                {
                    ShiftDownTimeStart = r["DownTimeStart"].ToString();
                    ShiftDownTimeEnd = r["DownTimeEnd"].ToString();
                    if (TimeSpan.Parse(StartTime) <= TimeSpan.Parse(ShiftDownTimeStart))
                    {
                        if (TimeSpan.Parse(EndTime) >= TimeSpan.Parse(ShiftDownTimeEnd)) // اگر زمان استراحت بین زمان تولید باشد. زمان استراحت از طول تولید کم میشود
                        {
                            DownTimes = (TimeSpan.Parse(DownTimes) + (TimeSpan.Parse(ShiftDownTimeEnd) - TimeSpan.Parse(ShiftDownTimeStart))).ToString();
                        }
                        else if (TimeSpan.Parse(EndTime) >= TimeSpan.Parse(ShiftDownTimeStart)) // زمان پایان تولید کوچکتر از زمان پایان استراحت باشد. زمان استراحت از زمان تولید کم میشود
                        {
                            DownTimes = (TimeSpan.Parse(DownTimes) + (TimeSpan.Parse(EndTime) - TimeSpan.Parse(ShiftDownTimeStart))).ToString();
                        }
                    }
                    else if (TimeSpan.Parse(StartTime) <= TimeSpan.Parse(ShiftDownTimeEnd)) // زمان شروع تولید بزرگتر از زنان شروع استراحت  میباشد
                                                                                            // و زمان پایان استراحت بعد از شروع تولید باشد . 
                    {
                        if (TimeSpan.Parse(EndTime) >= TimeSpan.Parse(ShiftDownTimeEnd)) // و زمان پایان تولید بزرگتر از زمان پایان استراحت باشد
                        {
                            DownTimes = (TimeSpan.Parse(DownTimes) + (TimeSpan.Parse(ShiftDownTimeEnd) - TimeSpan.Parse(StartTime))).ToString();
                        }
                        else  // زمان پایان تولید کوچکتر از زمان پایان استراحت باشد
                        {
                            DownTimes = (TimeSpan.Parse(DownTimes) + (TimeSpan.Parse(EndTime) - TimeSpan.Parse(StartTime))).ToString();
                        }
                    }
                }
            }

            for (I = DataSetConfig.dsProductionPlanning.Tables.Count - 1; I >= 0; I -= 1)
            {
                DataSetConfig.dsProductionPlanning.Tables[I].Dispose();
                DataSetConfig.dsProductionPlanning.Tables.RemoveAt(I);
            }

            WorkTimes = (TimeSpan.Parse(WorkTimes) - TimeSpan.Parse(DownTimes)).ToString();
            ProductionDuration = Conversions.ToDouble(Module1.GetFloatingHour(Strings.Mid(WorkTimes, 1, 5)));
        Exit_Function:
            ;
            return ProductionDuration;
        }
    }
}