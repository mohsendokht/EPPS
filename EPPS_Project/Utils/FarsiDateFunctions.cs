using System;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public class FarsiDateFunctions
    {
        private static int Fyear, Lyear;
        private static byte Lmon, Lday, Fmon, Fday, X, Y;
        private static byte[] Mon = new byte[13];

        // این تابع تشخیص می دهد سال ارسالی به آن کبیسه است یا نه
        public static bool IsKabiseh(int Year)
        {
            bool IsKabisehRet = default;
            if ((Year - 1375) % 4 == 0)
            {
                IsKabisehRet = true;
            }
            else
            {
                IsKabisehRet = false;
            }

            return IsKabisehRet;
        }
        // این تابع عملیات تبدیل تاریخ میلادی به شمسی را انجام می دهد
        public static string LFDate(DateTime Ldate)
        {
            string LFDateRet = default;
            X = 0;
            Y = 0;
            //Z = 0;
            if (DateAndTime.Year(Ldate) % 4 == 0)
                X = 1;
            if (DateAndTime.Month(Ldate) == 1 | DateAndTime.Month(Ldate) == 2 | DateAndTime.Month(Ldate) == 3 & DateAndTime.Day(Ldate) < 21 - X)
            {
                Fyear = DateAndTime.Year(Ldate) - 622;
            }
            else
            {
                Fyear = DateAndTime.Year(Ldate) - 621;
            }

            if (Fyear % 4 == 3)
            {
                Y = 1;
                //Z = 2;
            }

            Lmon = (byte)DateAndTime.Month(Ldate);
            Lday = (byte)DateAndTime.Day(Ldate);
            switch (Lmon)
            {
                case 1:
                    {
                        if (Lday < 21)
                        {
                            Fday = (byte)(Lday + 10 + Y);
                            Fmon = 10;
                        }
                        else
                        {
                            Fday = (byte)(Lday - 20 + Y);
                            Fmon = 11;
                        }

                        break;
                    }

                case 2:
                    {
                        if (Lday < 20)
                        {
                            Fday = (byte)(Lday + 11 + Y);
                            Fmon = 11;
                        }
                        else
                        {
                            Fday = (byte)(Lday - 19 + Y);
                            Fmon = 12;
                        }

                        break;
                    }

                case 3:
                    {
                        if (Lday < 21 - X)
                        {
                            Fday = (byte)(Lday + 9 + X + Y);
                            Fmon = 12;
                        }
                        else
                        {
                            Fday = (byte)(Lday - 20 + X);
                            Fmon = 1;
                        }

                        break;
                    }

                case 4:
                    {
                        if (Lday < 21 - X)
                        {
                            Fday = (byte)(Lday + 11 + X);
                            Fmon = 1;
                        }
                        else
                        {
                            Fday = (byte)(Lday - 20 + X);
                            Fmon = 2;
                        }

                        break;
                    }

                case 5:
                    {
                        if (Lday < 22 - X)
                        {
                            Fday = (byte)(Lday + 10 + X);
                            Fmon = 2;
                        }
                        else
                        {
                            Fday = (byte)(Lday - 21 + X);
                            Fmon = 3;
                        }

                        break;
                    }

                case 6:
                    {
                        if (Lday < 22 - X)
                        {
                            Fday = (byte)(Lday + 10 + X);
                            Fmon = 3;
                        }
                        else
                        {
                            Fday = (byte)(Lday - 21 + X);
                            Fmon = 4;
                        }

                        break;
                    }

                case 7:
                    {
                        if (Lday < 23 - X)
                        {
                            Fday = (byte)(Lday + 9 + X);
                            Fmon = 4;
                        }
                        else
                        {
                            Fday = (byte)(Lday - 22 + X);
                            Fmon = 5;
                        }

                        break;
                    }

                case 8:
                    {
                        if (Lday < 23 - X)
                        {
                            Fday = (byte)(Lday + 9 + X);
                            Fmon = 5;
                        }
                        else
                        {
                            Fday = (byte)(Lday - 22 + X);
                            Fmon = 6;
                        }

                        break;
                    }

                case 9:
                    {
                        if (Lday < 23 - X)
                        {
                            Fday = (byte)(Lday + 9 + X);
                            Fmon = 6;
                        }
                        else
                        {
                            Fday = (byte)(Lday - 22 + X);
                            Fmon = 7;
                        }

                        break;
                    }

                case 10:
                    {
                        if (Lday < 23 - X)
                        {
                            Fday = (byte)(Lday + 8 + X);
                            Fmon = 7;
                        }
                        else
                        {
                            Fday = (byte)(Lday - 22 + X);
                            Fmon = 8;
                        }

                        break;
                    }

                case 11:
                    {
                        if (Lday < 22 - X)
                        {
                            Fday = (byte)(Lday + 9 + X);
                            Fmon = 8;
                        }
                        else
                        {
                            Fday = (byte)(Lday - 21 + X);
                            Fmon = 9;
                        }

                        break;
                    }

                case 12:
                    {
                        if (Lday < 22 - X)
                        {
                            Fday = (byte)(Lday + 9 + X);
                            Fmon = 9;
                        }
                        else
                        {
                            Fday = (byte)(Lday - 21 + X);
                            Fmon = 10;
                        }

                        break;
                    }
            }

            if (Fmon < 10)
            {
                LFDateRet = Fyear.ToString() + "/" + "0" + Fmon.ToString() + "/";
            }
            else
            {
                LFDateRet = Fyear.ToString() + "/" + Fmon.ToString() + "/";
            }

            if (Fday < 10)
            {
                LFDateRet = LFDateRet + "0" + Fday.ToString();
            }
            else
            {
                LFDateRet = LFDateRet + Fday.ToString();
            }

            return LFDateRet;
        }
        // این تابع عملیات تبدیل تاریخ شمسی به میلادی را انجام می دهد
        public static string FLDate(string Fdate)
        {
            string FLDateRet = default;
            X = 0;
            Y = 0;
            Fdate = Strings.Replace(Fdate, "/", "");
            Lyear = Conversions.ToInteger(Strings.Mid(Fdate, 1, 4));
            Lmon = (byte)Conversions.ToInteger(Strings.Mid(Fdate, 5, 2));
            Lday = (byte)Conversions.ToInteger(Strings.Mid(Fdate, 7, 2));
            if (IsKabiseh(Lyear))
                X = 1;
            // If Lyear Mod 4 = 3 Then X = 1

            if (Lmon == 11 | Lmon == 12 | Lmon == 10 && Lday > 10 + X)
            {
                Lyear = Lyear + 622;
            }
            else
            {
                Lyear = Lyear + 621;
            }

            if (Lyear % 4 == 0)
            {
                Y = 1;
                //Z = 2;
            }

            Fmon = Lmon;
            Fday = Lday;
            switch (Fmon)
            {
                case 1:
                    {
                        if (Fday < 12 + Y)
                        {
                            Lday = (byte)(Fday + 20 - Y);
                            Lmon = 3;
                        }
                        else
                        {
                            Lday = (byte)(Fday - 11 - Y);
                            Lmon = 4;
                        }

                        break;
                    }

                case 2:
                    {
                        if (Fday < 11 + Y)
                        {
                            Lday = (byte)(Fday + 20 - Y);
                            Lmon = 4;
                        }
                        else
                        {
                            Lday = (byte)(Fday - 10 - Y);
                            Lmon = 5;
                        }

                        break;
                    }

                case 3:
                    {
                        if (Fday < 11 + X)
                        {
                            Lday = (byte)(Fday + 21 - X);
                            Lmon = 5;
                        }
                        else
                        {
                            Lday = (byte)(Fday - 10 - X);
                            Lmon = 6;
                        }

                        break;
                    }

                case 4:
                    {
                        if (Fday < 10 + X)
                        {
                            Lday = (byte)(Fday + 21 - X);
                            Lmon = 6;
                        }
                        else
                        {
                            Lday = (byte)(Fday - 9 - X);
                            Lmon = 7;
                        }

                        break;
                    }

                case 5:
                    {
                        if (Fday <= 9 + X)
                        {
                            Lday = (byte)(Fday + 22 - X);
                            Lmon = 7;
                        }
                        else
                        {
                            Lday = (byte)(Fday - 9 - X);
                            Lmon = 8;
                        }

                        break;
                    }

                case 6:
                    {
                        if (Fday < 10 + X)
                        {
                            Lday = (byte)(Fday + 22 - X);
                            Lmon = 8;
                        }
                        else
                        {
                            Lday = (byte)(Fday - 9 - X);
                            Lmon = 9;
                        }

                        break;
                    }

                case 7:
                    {
                        if (Fday < 9 + X)
                        {
                            Lday = (byte)(Fday + 22 - X);
                            Lmon = 9;
                        }
                        else
                        {
                            Lday = (byte)(Fday - 8 - X);
                            Lmon = 10;
                        }

                        break;
                    }

                case 8:
                    {
                        if (Fday < 10 + X)
                        {
                            Lday = (byte)(Fday + 22 - X);
                            Lmon = 10;
                        }
                        else
                        {
                            Lday = (byte)(Fday - 9 - X);
                            Lmon = 11;
                        }

                        break;
                    }

                case 9:
                    {
                        if (Fday < 10 + X)
                        {
                            Lday = (byte)(Fday + 21 - X);
                            Lmon = 11;
                        }
                        else
                        {
                            Lday = (byte)(Fday - 9 - X);
                            Lmon = 12;
                        }

                        break;
                    }

                case 10:
                    {
                        if (Fday < 11 + X)
                        {
                            Lday = (byte)(Fday + 21 - X);
                            Lmon = 12;
                        }
                        else
                        {
                            Lday = (byte)(Fday - 10 - X);
                            Lmon = 1;
                        }

                        break;
                    }

                case 11:
                    {
                        if (Fday < 12 + X)
                        {
                            Lday = (byte)(Fday + 20 - X);
                            Lmon = 1;
                        }
                        else
                        {
                            Lday = (byte)(Fday - 11 - X);
                            Lmon = 2;
                        }

                        break;
                    }

                case 12:
                    {
                        if (Fday < 10 + X)
                        {
                            Lday = (byte)(Fday + 19 - X);
                            Lmon = 2;
                        }
                        else
                        {
                            Lday = (byte)(Fday - 9 - X);
                            Lmon = 3;
                        }

                        break;
                    }
            }

            if (Lmon < 10)
            {
                FLDateRet = Lyear.ToString() + "/" + "0" + Lmon.ToString() + "/";
            }
            else
            {
                FLDateRet = Lyear.ToString() + "/" + Lmon.ToString() + "/";
            }

            if (Lday < 10)
            {
                FLDateRet = FLDateRet + "0" + Lday.ToString();
            }
            else
            {
                FLDateRet = FLDateRet + Lday.ToString();
            }

            return FLDateRet;
        }

        public static string AddToDate(string beginDate, string Duration)
        {
            int BeginYear, BeginMon, BeginDay;
            int EndYear, EndMon, EndDay;
            int DurYear, DurMon, DurDay;
            var ArrayOfMonDur = new int[13];
            string ResultYear, ResultMon, ResultDay;
            ArrayOfMonDur[1] = 31;
            ArrayOfMonDur[2] = 31;
            ArrayOfMonDur[3] = 31;
            ArrayOfMonDur[4] = 31;
            ArrayOfMonDur[5] = 31;
            ArrayOfMonDur[6] = 31;
            ArrayOfMonDur[7] = 30;
            ArrayOfMonDur[8] = 30;
            ArrayOfMonDur[9] = 30;
            ArrayOfMonDur[10] = 30;
            ArrayOfMonDur[11] = 30;
            ArrayOfMonDur[12] = 29;
            DurYear = Conversions.ToInteger(Strings.Left(Duration, 4));
            DurMon = Conversions.ToInteger(Strings.Left(Strings.Right(Duration, 4), 2));
            DurDay = Conversions.ToInteger(Strings.Right(Duration, 2));
            if (DurDay > 31)
            {
                int mDMOD = DurDay % 31;
                DurMon = (int)Math.Round((DurDay - mDMOD) / 31d);
                DurDay = mDMOD;
            }

            if (DurMon > 12)
            {
                int mMMOD = DurMon % 12;
                DurYear = (int)Math.Round((DurMon - mMMOD) / 12d);
                DurMon = mMMOD;
            }

            BeginYear = Conversions.ToInteger(Strings.Left(beginDate, 4));
            BeginMon = Conversions.ToInteger(Strings.Left(Strings.Right(beginDate, 4), 2));
            BeginDay = Conversions.ToInteger(Strings.Right(beginDate, 2));
            EndYear = BeginYear + DurYear;
            EndMon = BeginMon + DurMon;
            if (EndMon > 12)
            {
                EndYear = (int)Math.Round(EndYear + Conversion.Int(EndMon / 12d));
                EndMon = EndMon % 12;
            }

            if (IsKabiseh(EndYear))
            {
                ArrayOfMonDur[12] = 30;
            }

            EndDay = BeginDay + DurDay;
            if (EndDay > ArrayOfMonDur[EndMon])
            {
                EndDay = EndDay - ArrayOfMonDur[EndMon];
                EndMon = EndMon + 1;
                if (EndMon > 12)
                {
                    EndYear = EndYear + 1;
                    EndMon = EndMon - 12;
                }
            }

            ResultYear = EndYear.ToString();
            if (Strings.Len(EndYear) < 4)
            {
                ResultYear = Strings.StrDup(4 - Strings.Len(ResultYear), "0") + ResultYear;
            }

            ResultMon = EndMon.ToString();
            if (Strings.Len(ResultMon) < 2)
            {
                ResultMon = Strings.StrDup(2 - Strings.Len(ResultMon), "0") + ResultMon;
            }

            ResultDay = EndDay.ToString();
            if (Strings.Len(ResultDay) < 2)
            {
                ResultDay = Strings.StrDup(2 - Strings.Len(ResultDay), "0") + ResultDay;
            }

            return ResultYear + ResultMon + ResultDay;
        }

        public static string SubDayFromDate(string strBeginDay, string strDuration)
        {
            int BeginYear, BeginMon, BeginDay;
            int DurYear, DurMon, DurDay;
            int EndYear, EndMon, EndDay;
            var BeginMonLen = default(int);
            if (!string.IsNullOrEmpty(strBeginDay) && !string.IsNullOrEmpty(strDuration) && strBeginDay.Length == 8 && strDuration.Length == 8)
            {
                BeginYear = Conversions.ToInteger(Strings.Mid(strBeginDay, 1, 4));
                BeginMon = Conversions.ToInteger(Strings.Mid(strBeginDay, 5, 2));
                BeginDay = Conversions.ToInteger(Strings.Mid(strBeginDay, 7, 2));
                DurYear = Conversions.ToInteger(Strings.Mid(strDuration, 1, 4));
                DurMon = Conversions.ToInteger(Strings.Mid(strDuration, 5, 2));
                DurDay = Conversions.ToInteger(Strings.Mid(strDuration, 7, 2));
                if (DurDay > 31)
                {
                    int mDMOD = DurDay % 31;
                    DurMon = (int)Math.Round((DurDay - mDMOD) / 31d);
                    DurDay = mDMOD;
                }

                if (DurMon > 12)
                {
                    int mMMOD = DurMon % 12;
                    DurYear = (int)Math.Round((DurMon - mMMOD) / 12d);
                    DurMon = mMMOD;
                }

                if (BeginMon <= 6)
                {
                    BeginMonLen = 31;
                }

                if (BeginMon <= 11 & BeginMon > 6)
                {
                    BeginMonLen = 30;
                }

                if (BeginMon == 12)
                {
                    if (IsKabiseh(BeginYear))
                    {
                        BeginMonLen = 30;
                    }
                    else
                    {
                        BeginMonLen = 29;
                    }
                }

                EndDay = BeginDay - DurDay;
                while (EndDay < 1)
                {
                    BeginMon = BeginMon - 1;
                    if (BeginMon < 1)
                    {
                        BeginMon = BeginMon + 12;
                        BeginYear = BeginYear - 1;
                    }

                    EndDay = ValidateDay(EndDay + BeginMonLen, BeginMon, BeginYear);
                }

                EndDay = ValidateDay(EndDay, BeginMon, BeginYear);
                EndMon = BeginMon - DurMon;
                if (EndMon < 1)
                {
                    BeginYear = BeginYear - 1;
                    EndMon = EndMon + 12;
                    EndDay = ValidateDay(EndDay, EndMon, BeginYear);
                }

                EndYear = BeginYear - DurYear;
                return Strings.StrDup(4 - Strings.Len(EndYear.ToString()), "0") + EndYear.ToString() + Strings.StrDup(2 - Strings.Len(EndMon.ToString()), "0") + EndMon.ToString() + Strings.StrDup(2 - Strings.Len(EndDay.ToString()), "0") + EndDay.ToString();
            }
            else
            {
                return strBeginDay;
            }
        }

        private static int ValidateDay(int mDay, int mMonth, int mYear)
        {
            if (mMonth > 6 && mDay > 30)
            {
                mDay = 30;
            }

            if (mMonth == 12 && !IsKabiseh(mYear) && mDay >= 30)
            {
                mDay = 29;
            }

            return mDay;
        }

        private static string Add(string startDHM, int days,int hours, int minutes)
        {
            
            string CurShDate = startDHM.Substring(1,8);
            string NewShDate;

            int.TryParse(startDHM.Substring(9,2), out int currentHour);
            int.TryParse(startDHM.Substring(11, 2), out int currentMinute);

            TimeSpan CurTS = new TimeSpan(0, currentHour, currentMinute, 0);
            TimeSpan newTS = new TimeSpan(days,hours,minutes,0);
            newTS = CurTS.Add(newTS);
            var durationDays = newTS.Days.ToString("00000000");

            if (newTS.TotalMinutes >= 0)
            {
                NewShDate = AddToDate(CurShDate, durationDays);
            }
            else
            {
                NewShDate = SubDayFromDate(CurShDate, durationDays);
            }

            Int64.TryParse(NewShDate, out Int64 newDate);
            newDate = ((newDate * 100) + newTS.Hours) * 100 + newTS.Minutes; 
            
            return newDate.ToString();
        }
    }
}