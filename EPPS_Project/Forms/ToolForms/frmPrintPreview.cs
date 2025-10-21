using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmPrintPreview
    {
        public frmPrintPreview()
        {
            InitializeComponent();
        }

        public ReportDocument report1;
        //private ParameterDiscreteValue crParameterDiscreteValue;
        //private ParameterFieldDefinitions crParameterFieldDefinitions;
        //private ParameterFieldDefinition crParameterFieldLocation;
        //private ParameterValues crParameterValues;
        //private TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        private TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        private ConnectionInfo crConnectionInfo = new ConnectionInfo();
        private Tables CrTables;
        private Table CrTable;
        //private object TableCounter;
        private string Report;
        private int Param_Count;
        private string[] Param_Value = new string[11];
        private int Show_Print; // 1=show  2=print
        private string FormulaTmp, TitleTmp;

        public string ReporetName
        {
            get
            {
                return Report;
            }

            set
            {
                Report = value;
            }
        }

        public int ParameterCont
        {
            get
            {
                return Param_Count;
            }

            set
            {
                Param_Count = value;
            }
        }

        public string get_Param(object n)
        {
            return Param_Value[Conversions.ToInteger(n)];
        }

        public void set_Param(object n, string value)
        {
            Param_Value[Conversions.ToInteger(n)] = value;
        }

        public int ShowType
        {
            get
            {
                return Show_Print;
            }

            set
            {
                Show_Print = value;
            }
        }

        public string Formula
        {
            get
            {
                return FormulaTmp;
            }

            set
            {
                FormulaTmp = value;
            }
        }

        public string ReportTitle
        {
            get
            {
                return TitleTmp;
            }

            set
            {
                TitleTmp = value;
            }
        }

        private void PrintPreview_Load(object sender, EventArgs e)
        {
            if (Show_Print == 2)
            {
                Visible = false;
            }

            MinimizeBox = true;
            string ReportPath1;
            string myjob = My.MyProject.Application.Info.DirectoryPath + @"\Reports\" + Report;
            ReportPath1 = myjob;
            string reportname;
            var mycnn1 = new SqlConnectionStringBuilder();
            bool ISdb_flag;
            string servername;
            string password;
            string username1;
            report1 = new ReportDocument();
            mycnn1.ConnectionString = Module1.PlanningCnnStr;
            servername = mycnn1.DataSource;
            password = mycnn1.Password;
            username1 = mycnn1.UserID;
            ISdb_flag = mycnn1.IntegratedSecurity;
            {
                var withBlock = crConnectionInfo;
                if (ISdb_flag == false)
                {
                    withBlock.ServerName = servername;
                    withBlock.DatabaseName = "PSB_ProductionPlanning";
                    withBlock.UserID = username1;
                    withBlock.Password = password;
                    withBlock.IntegratedSecurity = ISdb_flag;
                }
                else
                {
                    withBlock.ServerName = servername;
                    withBlock.DatabaseName = "PSB_ProductionPlanning";
                    withBlock.IntegratedSecurity = ISdb_flag;
                }
            }

            reportname = Report;
            report1 = new ReportDocument();
            if (string.IsNullOrEmpty(FileSystem.Dir(ReportPath1)))
            {
                MessageBox.Show(" وجود ندارد  '" + ReportPath1 + " 'در مسیر  '" + reportname + "' گزارش");
                return;
            }

            report1.Load(ReportPath1);
            CrTables = report1.Database.Tables;
            foreach (Table currentCrTable in CrTables)
            {
                CrTable = currentCrTable;
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);
            }

            CrystalReportViewer1.ReportSource = report1;
            int i;
            var loopTo = Param_Count - 1;
            for (i = 0; i <= loopTo; i++)
                // report1.SetParameterValue(i, Param_Value(i))
                report1.ParameterFields[i].CurrentValues.AddValue(Param_Value[i]);
            CrystalReportViewer1.ReportSource = report1;
            if (Show_Print == 1)
            {
                CrystalReportViewer1.Show();
            }
            else
            {
                report1.PrintToPrinter(1, false, 0, 0);
                Close();
            }
        }

        private void frmPrintPreview_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}