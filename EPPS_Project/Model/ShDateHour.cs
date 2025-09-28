using Microsoft.VisualBasic;

namespace ProductionPlanning
{
    public class ShDateHour
    {
        public long ShDate;
        public double WhHour;

        public string DHString
        {
            get
            {
                return GetDHStr();
            }
        }

        private string GetDHStr()
        {
            string strHour = WhHour.ToString();
            if (Strings.Left(WhHour.ToString(), 1) != "0" && WhHour < 10.0d)
            {
                strHour = "0" + WhHour.ToString();
            }

            return ShDate.ToString() + strHour;
        }
    }
}