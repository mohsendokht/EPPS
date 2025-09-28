using ProductionPlanning.ToolForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ProductionPlanning.MyEnums;

namespace ProductionPlanning
{
    static class  CommonTool
    {
        public static DialogResult ShowMessage(string msg, MessageBoxButtons buttons)
        {
            frmMessage frm = new frmMessage(msg, buttons);
            frm.StartPosition = FormStartPosition.CenterScreen;
            var result =  frm.ShowDialog();
            frm.Dispose();
            return result;
        }

        public static ShDateHour GetDateHour(int myDate, double myHour = 0)
        {
            ShDateHour dh = new ShDateHour();
            dh.ShDate = myDate;
            dh.WhHour = myHour;
            return dh;
        }

        public static bool IsDH1BeforeDH2(string d1, double h1, string d2, double h2)
        {
            string strH1 = h1.ToString();
            if ( h1.ToString().Substring(0, 1) != "0" && h1 < 10.0d)
                strH1 = "0" + h1.ToString();

            string strH2 = h2.ToString();
            if (h2.ToString().Substring(0, 1) != "0" && h2 < 10.0d)
                strH2 = "0" + h2.ToString();

            double dh1 = double.Parse(d1.ToString() + strH1);
            double dh2 = double.Parse(d2.ToString() + strH2);

            return (dh1 < dh2);
        }

        public static EnumExecutionMethod GetExecutionMethod(string exMethod)
        {
            try
            {
                EnumExecutionMethod ExecMethod = EnumExecutionMethod.EM_MACHINE;
                short method = short.Parse(exMethod);
                if (Enum.IsDefined(typeof(EnumExecutionMethod), method))
                {
                    ExecMethod = (EnumExecutionMethod)method;
                }

                return ExecMethod;
            }
            catch (Exception)
            {

                return EnumExecutionMethod.EM_MACHINE;
            }
            
        }

        public static EnumRelationType GetRelationType(string RelationType)
        {
            try
            {
                EnumRelationType RType = EnumRelationType.RT_FS;
                short type = short.Parse(RelationType);
                if (Enum.IsDefined(typeof(EnumExecutionMethod), type))
                {
                    RType = (EnumRelationType)type;
                }

                return RType;
            }
            catch (Exception)
            {

                return EnumRelationType.RT_FS;
            }

        }

        public static TimeSpan CTimeSpan(string ts)
        {
            TimeSpan myTimeSpan = TimeSpan.Parse("00:00");

            TimeSpan.TryParse(ts, out myTimeSpan);

            return myTimeSpan;
        }

    }
}
