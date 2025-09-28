using Microsoft.VisualBasic;
using ProductionPlanning.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ProductionPlanning.Utils
{
    internal class WorkingTimeCalculater
    {
        public List<CalendarDayWorkingTime> WTList;
        public WorkingTimeCalculater() 
        {
            WTList = new List<CalendarDayWorkingTime>();

        }

        public string CalculateHMDuration(string CalendarCode, string StartDate,string StartHM, string EndDate, string EndHM, string HaltHM )
        {
            bool shDateExists;
            int maxCounter = 1000;
            int counter = 1;
            string curDate;

            Int64 StartDHM = (Int64.Parse(StartDate) * 10000) + (Int64.Parse(StartHM.Substring(0,2)) * 100) + Int64.Parse(StartHM.Substring(3,2));
            Int64 EndDHM = (Int64.Parse(EndDate) * 10000) + (Int64.Parse(EndHM.Substring(0, 2)) * 100) + Int64.Parse(EndHM.Substring(3, 2));

            while (true)
            {
                shDateExists = WTList.Any(i => i.CalendarCode == CalendarCode && i.Shdate == StartDate);

                if (!shDateExists)
                {
                    var ListCDWT = GetCalendarDayWorkingTimes(CalendarCode, StartDate);
                    WTList.AddRange(ListCDWT);
                }

                curDate = FarsiDateFunctions.AddToDate(StartDate, "00000001");
                if (int.Parse(curDate)> int.Parse(EndDate) || counter > maxCounter)
                {
                    break;
                }

                counter++;
                                
            }
            var minutes = WTList.Where(item => item.CalendarCode== CalendarCode && item.DHM >= StartDHM && item.DHM <= EndDHM)
                                .Select(item => item.WorkingTime).Sum();
            TimeSpan ProductionTS = TimeSpan.FromMinutes(minutes);
            TimeSpan HaltTS = TimeSpan.FromMinutes(int.Parse(HaltHM.Substring(0,2))* 60 + int.Parse(HaltHM.Substring(3,2)));
            ProductionTS = ProductionTS.Subtract(HaltTS);

            return Module1.GetFloatingHour(ProductionTS.ToString().Substring(0,5));
            
        }

        private string AddHM(string DHM)
        {
            //TODO: Need to be implemented if needed.
            if (string.IsNullOrEmpty(DHM)) DHM = "140101010000";
            return "";
        }
        private string SubtractHM() 
        {
            //TODO: Need to be implemented if needed.
            return ""; 
        }

        private bool CheckDHM(string DHM)
        {
            var check = true;
            if (string.IsNullOrEmpty(DHM)) check = false;
            if (DHM.Length < 12) check = false;
            var H = DHM.Substring(9, 2);
            if (int.Parse(H) > 23) check = false;
            var M = DHM.Substring(11, 2);
            if (int.Parse(H) > 59) check = false;
                        
            return check;
        }

        private List<CalendarDayWorkingTime> GetCalendarDayWorkingTimes(string CalendarCode, string ShDate) 
        {
            
            var ListCDWT = new List<CalendarDayWorkingTime>();

            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cm = new SqlCommand($"EXEC [dbo].[sp_Get_DayWorkingTime]  @CalendarCode = {CalendarCode}, @ShamsiDate = {ShDate}", cn);
                var drOperationCode = cm.ExecuteReader();
                while (drOperationCode.Read())
                {
                    ListCDWT.Add(new CalendarDayWorkingTime {
                        CalendarCode = CalendarCode,
                        Shdate = ShDate,
                        HourMinute = int.Parse(drOperationCode["HM"].ToString()),
                        WorkingTime = int.Parse(drOperationCode["WorkingTime"].ToString())
                    });
                }

                drOperationCode.Close();
                cn.Close();
            }
            

            return ListCDWT;
        }

    }
}
