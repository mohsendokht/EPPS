using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductionPlanning.Primary_Definitions.Calendar_Forms
{
    internal class CalendarShifts
    {
        
        public List<CalendarShift> shifts;
        public List<FiveMinuteSlice> fiveMinuteSlices = new List<FiveMinuteSlice>();

        public CalendarShifts(List<CalendarShift> calendarShifts)
        {
            shifts = calendarShifts;
        }

        public bool CalendarShiftCheck()
        {
            var result = false;
            TimeSpan Time24 = CommonTool.CTimeSpan("1.00:00:00");
            TimeSpan TotalDuration = CommonTool.CTimeSpan("00:00");

            foreach (var shift in shifts)
            {
                TotalDuration = TotalDuration + CommonTool.CTimeSpan(shift.Duration);
                if (TotalDuration > Time24 )
                {
                    MessageBox.Show("طول شیفتها بیشتر از یکروز میباشد.", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return false;
                }
                result = SetShiftSlices(shift.ShiftNo, shift.Start, shift.Duration);
                if (!result) break;
            }
            return result;
        }
        public string UpdateFiveMinuteSlice(FiveMinuteSlice slice)
        {
            var ErrorMessage = "";
            var usedSlice = fiveMinuteSlices.Find(i => i.Start == slice.Start && i.End == slice.End);
            if (usedSlice != null)
            {
                ErrorMessage = "تداخل در زمانبندی شیفت " + slice.ShiftNo + "  و شیفت " + usedSlice.ShiftNo + " " + " در ساعت " + slice.Start.Substring(0,5);
            }
            else
            {
                fiveMinuteSlices.Add(slice);
            }
            return ErrorMessage;
        }

        private List<FiveMinuteSlice> GenerateShiftSliceFor24Houres()
        {
            var Slices = new List<FiveMinuteSlice>();
            for (int i = 0; i < 288; i++)
            {
                var fiveMinuteSlice = new FiveMinuteSlice { ShiftNo= 0, Start="", End = "", Type = 1 };
                Slices.Add(fiveMinuteSlice);

            }
            return Slices;
        }

        private bool SetShiftSlices(int shiftNo,string start, string duration)
        {
            TimeSpan mTimeSlice = CommonTool.CTimeSpan("00:05");
            TimeSpan addedTimeSlice = CommonTool.CTimeSpan("00:00");
            TimeSpan shiftDuration = CommonTool.CTimeSpan(duration);
            TimeSpan shiftSatrt = CommonTool.CTimeSpan(start) ;
            TimeSpan Time24 = CommonTool.CTimeSpan("1.00:00:00");

            try
            {

                if (shiftDuration == CommonTool.CTimeSpan("00:00"))
                {
                    MessageBox.Show("طول شیفت صحیح نیست.", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return false;
                }

                while (addedTimeSlice < shiftDuration)
                {
                    
                    if ((shiftSatrt + addedTimeSlice) > Time24)
                    {
                        start = (shiftSatrt + addedTimeSlice - Time24).ToString();
                    }
                    else
                    {
                        start = (shiftSatrt + addedTimeSlice).ToString();
                    }

                    var ErrorMsg = UpdateFiveMinuteSlice(new FiveMinuteSlice { ShiftNo = shiftNo, Start = start, End = "", Type = 1 });

                    if (ErrorMsg.Length > 0)
                    {
                        MessageBox.Show(ErrorMsg, Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);

                        return false;
                    }

                    addedTimeSlice += mTimeSlice;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);

                return false;
            }

            return true;
        }
    }
    internal class CalendarShift
    {
        public int ShiftNo;
        public string Start;
        public string End;
        public string Duration;
        public string ExtraTime;


    }
    internal class FiveMinuteSlice
    {
        public int ShiftNo;
        public string Start;
        public string End;
        public int Type;
        

    }
}
