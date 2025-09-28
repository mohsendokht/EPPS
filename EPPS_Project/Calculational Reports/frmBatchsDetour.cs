using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmBatchDetour
    {
        public frmBatchDetour()
        {
            InitializeComponent();
            _cmdPrintPreview.Name = "cmdPrintPreview";
            _cmdCancel.Name = "cmdCancel";
        }

        private DataTable dtBatchs = new DataTable();

        private void frmBatchDetour_Load(object sender, EventArgs e)
        {
            {
                var withBlock = new Thread(new ThreadStart(LoadBatchsList));
                withBlock.Start();
            }
        }

        private void frmBatchDetour_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    var cmExistsTable = new SqlCommand("If exists (Select * From dbo.sysobjects Where id = object_id(N'dbo.Tbl_TempBatchsDetour') and OBJECTPROPERTY(id, N'IsUserTable') = 1) " + " drop table dbo.Tbl_TempBatchsDetour ", cn);
                    cmExistsTable.ExecuteNonQuery();
                    cn.Close();
                }
                catch (Exception exp)
                {
                    Logger.LogException("frmBatchDetour_FormClosing", exp);
                }
            }

            dtBatchs.Dispose();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool FormValidation()
        {
            if (txtCalcDate.Text is null || string.IsNullOrEmpty(txtCalcDate.Text) || txtCalcDate.Text.Equals("") || txtCalcDate.Text.Trim().Length < 8)
            {
                MessageBox.Show("تاریخ محاسبه را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtCalcDate.Focus();
                return false;
            }

            return true;
        }

        private string GetPartCalendarCode(string mPartCode)
        {
            string CalendarCode = "0";
            var drPart = dtBatchs.Select("NatureCode = " + mPartCode);
            if (drPart.Length > 0)
            {
                CalendarCode = drPart[0]["CalendarCode"].ToString();
            }

            return CalendarCode;
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (!FormValidation())
                {
                    return;
                }

                Cursor = Cursors.WaitCursor;
                Application.DoEvents();
                if (!InsertDataBaseTableRows())
                {
                    return;
                }

                if (!LoadReport(CrystalReportViewer1))
                {
                    return;
                }

                Cursor = Cursors.Default;
                Application.DoEvents();
            }
            catch (Exception objEx)
            {
                Cursor = Cursors.Default;
                Application.DoEvents();
                Logger.SaveError("frmBatchsDetour.cmdPrint_Click", objEx.Message);
                MessageBox.Show("نمایش لیست عقب افتادگی بچ های تولید با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }

        private bool LoadReport(CrystalReportViewer ReportViewer)
        {
            var CnnBuilder = new SqlConnectionStringBuilder(Module1.PlanningCnnStr);
            var CnnInfo = new ConnectionInfo();
            var RptDoc = new ReportDocument();
            CnnInfo.ServerName = CnnBuilder.DataSource;
            CnnInfo.DatabaseName = "PSB_ProductionPlanning";
            CnnInfo.IntegratedSecurity = CnnBuilder.IntegratedSecurity;
            if (!CnnBuilder.IntegratedSecurity)
            {
                CnnInfo.UserID = CnnBuilder.UserID;
                CnnInfo.Password = CnnBuilder.Password;
            }

            string mrptPath = Module1.GetReportFolderPath();
            if (File.Exists(mrptPath + "Rpt102_BatchsDetourList.rpt"))
            {
                RptDoc.Load(mrptPath + "Rpt102_BatchsDetourList.rpt", OpenReportMethod.OpenReportByDefault);
                RptDoc.ParameterFields["PrintDate"].CurrentValues.AddValue(Module1.mServerShamsiDate);
                foreach (Table tb in RptDoc.Database.Tables)
                {
                    tb.LogOnInfo.ConnectionInfo = CnnInfo;
                    tb.ApplyLogOnInfo(tb.LogOnInfo);
                }

                ReportViewer.ReportSource = null;
                ReportViewer.ReportSource = RptDoc;
            }
            else
            {
                Cursor = Cursors.Default;
                MessageBox.Show("فایل گزارش در مسیر اجرایی نرم افزار یافت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                return false;
            }

            return true;
        }

        private void GetBatchDetour(ref DataRow mBatchRow, string CalcDate)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmDetour = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select PlanningCode,PlanningStartDate,PlanningEndDate,PlanningDuration," + "       Case (Select ExecutionMethod " + "             From   Tbl_ProductOPCs " + "             Where  TreeCode = Tbl_Planning.TreeCode And OperationCode = Tbl_Planning.OperationCode) " + "       When 3 Then -1 " + "       When 2 Then(Select CalendarCode " + "                   From   Tbl_ProductOPCsExecutorMachines " + "                   Where  TreeCode = Tbl_Planning.TreeCode And OperationCode = Tbl_Planning.OperationCode And MachineCode = '-1') " + "       When 1 Then(Select CalendarCode From Tbl_Machines Where Code = Tbl_Planning.MachineCode) " + "       End As CalendarCode " + "From   Tbl_Planning Where TreeCode = ", mBatchRow["TreeCode"]), " And PlanningStartDate <= '"), CalcDate), "' And PlanningEndDate <= '"), CalcDate), "' And SubbatchCode IN "), "(Select SubbatchCode From Tbl_ProductionSubbatchs Where BatchCode = '"), mBatchRow["BatchCode"]), "')")), cn);



                var drDetour = cmDetour.ExecuteReader();
                double PlanningDuration = 0.0d;
                string mPlanningCodes = Constants.vbNullString;
                while (drDetour.Read())
                {
                    if (string.IsNullOrEmpty(mPlanningCodes))
                    {
                        mPlanningCodes = drDetour["PlanningCode"].ToString();
                    }
                    else if (!mPlanningCodes.Contains(drDetour["PlanningCode"].ToString()))
                    {
                        mPlanningCodes += "," + drDetour["PlanningCode"].ToString();
                    }

                    PlanningDuration += Conversions.ToDouble(drDetour["PlanningDuration"]);
                }

                drDetour.Close();
                cmDetour.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select A.PlanningCode,A.PlanningStartDate,A.PlanningEndDate, " + "       Case (Select ExecutionMethod " + "             From   Tbl_ProductOPCs " + "             Where  TreeCode = A.TreeCode And OperationCode = A.OperationCode) " + "       When 3 Then -1 " + "       When 2 Then(Select CalendarCode " + "                   From   Tbl_ProductOPCsExecutorMachines " + "                   Where  TreeCode = A.TreeCode And OperationCode = A.OperationCode And MachineCode = '-1') " + "       When 1 Then(Select CalendarCode From Tbl_Machines Where Code = A.MachineCode) " + "       End As CalendarCode " + "From   Tbl_Planning A Where A.TreeCode = ", mBatchRow["TreeCode"]), " And A.PlanningStartDate <= '"), CalcDate), "' And A.PlanningEndDate > '"), CalcDate), "' And A.SubbatchCode IN "), "(Select SubbatchCode From Tbl_ProductionSubbatchs Where BatchCode = '"), mBatchRow["BatchCode"]), "')"));










                drDetour = cmDetour.ExecuteReader();
                while (drDetour.Read())
                {
                    if (string.IsNullOrEmpty(mPlanningCodes))
                    {
                        mPlanningCodes = drDetour["PlanningCode"].ToString();
                    }
                    else if (!mPlanningCodes.Contains(drDetour["PlanningCode"].ToString()))
                    {
                        mPlanningCodes += "," + drDetour["PlanningCode"].ToString();
                    }

                    PlanningDuration += GetDaysDurationTime(Conversions.ToString(drDetour["PlanningStartDate"]), CalcDate, Conversions.ToInteger(drDetour["CalendarCode"]));
                }

                drDetour.Close();
                if (string.IsNullOrEmpty(mPlanningCodes))
                {
                    mPlanningCodes = "0";
                }

                // محاسبۀ طول زمان تولید واقعی
                cmDetour.CommandText = "Select   IsNull((Sum(IntactQuantity) * OneExecTime),0.0) As ProductionDuration " + "From     (Select IntactQuantity,PlanningCode,dbo.GetOperationExecutionTime(MachineCode, TreeCode, OperationCode) As OneExecTime " + "          From Tbl_RealProduction Where PlanningCode IN (" + mPlanningCodes + ") And StartDate <= '" + CalcDate + "') Tbl_RealProduction " + "Group By PlanningCode,OneExecTime ";


                double ProductionDuration = 0.0d;
                drDetour = cmDetour.ExecuteReader();
                while (drDetour.Read())
                    ProductionDuration += Conversions.ToDouble(drDetour["ProductionDuration"]);
                drDetour.Close();

                // محاسبۀ طول زمان باقیماندۀ تولید
                cmDetour.CommandText = "Select   IsNull((((Sum(IntactQuantity) - (Select DetailProductionQuantity From Tbl_Planning Where PlanningCode = Tbl_RealProduction.PlanningCode)) * -1) * OneExecTime),0.0) As ProductionDuration " + "From     (Select IntactQuantity,PlanningCode,dbo.GetOperationExecutionTime(MachineCode, TreeCode, OperationCode) As OneExecTime " + "          From Tbl_RealProduction Where PlanningCode IN (" + mPlanningCodes + ") And StartDate <= '" + CalcDate + "') Tbl_RealProduction " + "Group By PlanningCode,OneExecTime " + "Having   ((Sum(IntactQuantity)-(Select DetailProductionQuantity From Tbl_Planning Where PlanningCode = Tbl_RealProduction.PlanningCode))) < 0";



                double Estimate_ProductionDuration = 0.0d;
                drDetour = cmDetour.ExecuteReader();
                while (drDetour.Read())
                    Estimate_ProductionDuration += Conversions.ToDouble(drDetour["ProductionDuration"]);
                drDetour.Close();
                cn.Close();
                mBatchRow["PlanningDuration"] = Module1.GetRegulareHour(PlanningDuration.ToString());
                mBatchRow["ProductionDuration"] = Module1.GetRegulareHour(ProductionDuration.ToString());
                if (PlanningDuration <= 0.0d || ProductionDuration <= 0.0d)
                {
                    mBatchRow["ProductionProgress"] = "0.00";
                }
                else
                {
                    mBatchRow["ProductionProgress"] = Math.Round(ProductionDuration * 100d / PlanningDuration, 2).ToString();
                }

                if (PlanningDuration <= 0.0d || PlanningDuration < ProductionDuration)
                {
                    mBatchRow["DetourPercent"] = 0;
                }
                else
                {
                    mBatchRow["DetourPercent"] = 100d - ProductionDuration * 100d / PlanningDuration;
                } // CInt((Estimate_ProductionDuration / PlanningDuration) * 100)

                double DetourHour = PlanningDuration - ProductionDuration;
                if (DetourHour <= 0.0d)
                {
                    mBatchRow["DetourTime"] = "00:00";
                }
                else
                {
                    mBatchRow["DetourTime"] = Module1.GetRegulareHour(DetourHour.ToString());
                }
            }
        }

        private double GetDaysDurationTime(string StartDate, string EndDate, int CalendarCode)
        {
            double DayTime = 0d;
            while (true)
            {
                if (Module1.IsHoliday(CalendarCode.ToString(), StartDate))
                {
                    DayTime += 0.0d;
                }
                else
                {
                    short ParticulareDayType = Module1.IsParticularDay(CalendarCode.ToString(), StartDate);
                    short DayNo = Convert.ToInt16(Module1.GetDayNo(StartDate));
                    string DayAccessibleTime = "0:0";
                    // Const DT_HOLIDAY = 1
                    const int DT_COMMON = 2;
                    switch (ParticulareDayType)
                    {
                        case -1: // روز عادی
                            {
                                DayAccessibleTime = Module1.GetDayAccessibleTime(CalendarCode.ToString(), DayNo.ToString(), 1);
                                break;
                            }

                        case DT_COMMON: // روز خاص غیر تعطیل
                            {
                                DayAccessibleTime = Module1.GetDayAccessibleTime(CalendarCode.ToString(), DayNo.ToString(), 2);
                                break;
                            }
                    }

                    DayTime += Conversions.ToDouble(Module1.GetFloatingHour(DayAccessibleTime));
                }

                StartDate = FarsiDateFunctions.AddToDate(StartDate, "00000001");
                if (Operators.CompareString(StartDate, EndDate, false) > 0)
                {
                    break;
                }
            }

            return DayTime;
        }

        private bool InsertDataBaseTableRows()
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var trnInsert = cn.BeginTransaction();
                var cmInsertOvers = new SqlCommand("if Not Exists(select * from dbo.sysobjects where id = object_id(N'[dbo].[Tbl_TempBatchsDetour]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)" + "Begin " + "Create Table [dbo].[Tbl_TempBatchsDetour] (" + "[RowIndex]           [int] NOT NULL ," + "[BatchCode]          [varchar] (20) Collate Arabic_CS_AS_KS_WS NOT NULL ," + "[TreeCode]           [int] NOT NULL ," + "[ProductCode]        [varchar] (50) Collate Arabic_CS_AS_KS_WS NOT NULL ," + "[ProductName]        [varchar] (100) Collate Arabic_CS_AS_KS_WS NOT NULL ," + "[CalcDate]           [varchar] (8) Collate Arabic_CS_AS_KS_WS NOT NULL ," + "[BatchQuantity]      [int] NOT NULL ," + "[ProductionQuantity] [int] NOT NULL ," + "[ProductionProgress] [float] NOT NULL ," + "[PlanningDuration]   [varchar] (8) Collate Arabic_CS_AS_KS_WS NOT NULL ," + "[ProductionDuration] [varchar] (8) Collate Arabic_CS_AS_KS_WS NOT NULL ," + "[DetourTime]         [varchar] (8) Collate Arabic_CS_AS_KS_WS NOT NULL ," + "[DetourPercent]      [float] NOT NULL)" + "End", cn);

                try
                {
                    cmInsertOvers.Transaction = trnInsert;
                    cmInsertOvers.ExecuteNonQuery();
                    cmInsertOvers.CommandText = "Delete From Tbl_TempBatchsDetour";
                    cmInsertOvers.ExecuteNonQuery();
                    cmInsertOvers.CommandText = "Insert Into Tbl_TempBatchsDetour(RowIndex,BatchCode,TreeCode,ProductCode,ProductName,CalcDate,BatchQuantity,ProductionQuantity,ProductionProgress,PlanningDuration,ProductionDuration,DetourTime,DetourPercent) " + "Values(@RowIndex,@BatchCode,@TreeCode,@ProductCode,@ProductName,@CalcDate,@BatchQuantity,@ProductionQuantity,@ProductionProgress,@PlanningDuration,@ProductionDuration,@DetourTime,@DetourPercent)";
                    {
                        var withBlock = cmInsertOvers.Parameters;
                        withBlock.Add("@RowIndex", SqlDbType.Int, default, "RowIndex");
                        withBlock.Add("@BatchCode", SqlDbType.VarChar, 20, "BatchCode");
                        withBlock.Add("@TreeCode", SqlDbType.Int, default, "TreeCode");
                        withBlock.Add("@ProductCode", SqlDbType.VarChar, 50, "ProductCode");
                        withBlock.Add("@ProductName", SqlDbType.VarChar, 100, "ProductName");
                        withBlock.Add("@CalcDate", SqlDbType.VarChar, 8, "CalcDate");
                        withBlock.Add("@BatchQuantity", SqlDbType.Int, default, "BatchQuantity");
                        withBlock.Add("@ProductionQuantity", SqlDbType.Int, default, "ProductionQuantity");
                        withBlock.Add("@ProductionProgress", SqlDbType.Float, default, "ProductionProgress");
                        withBlock.Add("@PlanningDuration", SqlDbType.VarChar, 8, "PlanningDuration");
                        withBlock.Add("@ProductionDuration", SqlDbType.VarChar, 8, "ProductionDuration");
                        withBlock.Add("@DetourTime", SqlDbType.VarChar, 8, "DetourTime");
                        withBlock.Add("@DetourPercent", SqlDbType.Float, default, "DetourPercent");
                    }

                    for (int I = 0, loopTo = dtBatchs.Rows.Count - 1; I <= loopTo; I++)
                    {
                        dtBatchs.Rows[I].BeginEdit();
                        var tmp = dtBatchs.Rows;
                        var argmBatchRow = dtBatchs.Rows[I];
                        GetBatchDetour(ref argmBatchRow, txtCalcDate.Text);
                        //tmp[I] = argmBatchRow;
                        dtBatchs.Rows[I].EndEdit();
                        cmInsertOvers.Parameters["@RowIndex"].Value = I + 1;
                        cmInsertOvers.Parameters["@BatchCode"].Value = dtBatchs.Rows[I]["BatchCode"];
                        cmInsertOvers.Parameters["@TreeCode"].Value = dtBatchs.Rows[I]["TreeCode"];
                        cmInsertOvers.Parameters["@ProductCode"].Value = dtBatchs.Rows[I]["ProductCode"];
                        cmInsertOvers.Parameters["@ProductName"].Value = dtBatchs.Rows[I]["ProductName"];
                        cmInsertOvers.Parameters["@CalcDate"].Value = txtCalcDate.Text;
                        cmInsertOvers.Parameters["@BatchQuantity"].Value = dtBatchs.Rows[I]["BatchQuantity"];
                        cmInsertOvers.Parameters["@ProductionQuantity"].Value = dtBatchs.Rows[I]["ProductionQuantity"];
                        cmInsertOvers.Parameters["@ProductionProgress"].Value = dtBatchs.Rows[I]["ProductionProgress"];
                        cmInsertOvers.Parameters["@PlanningDuration"].Value = dtBatchs.Rows[I]["PlanningDuration"];
                        cmInsertOvers.Parameters["@ProductionDuration"].Value = dtBatchs.Rows[I]["ProductionDuration"];
                        cmInsertOvers.Parameters["@DetourTime"].Value = dtBatchs.Rows[I]["DetourTime"];
                        cmInsertOvers.Parameters["@DetourPercent"].Value = dtBatchs.Rows[I]["DetourPercent"];
                        cmInsertOvers.ExecuteNonQuery();
                    }

                    trnInsert.Commit();
                }
                catch (Exception objEx)
                {
                    trnInsert.Rollback();
                    Cursor = Cursors.Default;
                    Application.DoEvents();
                    Logger.SaveError("frmBatchsDetour.InsertDataBaseTableRows", objEx.Message);
                    MessageBox.Show("نمایش لیست عقب افتادگی بچ های تولید با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return false;
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }

            return true;
        }

        private void LoadBatchsList()
        {
            using (var Cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    var daBatchs = new SqlDataAdapter("Select A.BatchCode, A.ProductTreeCode As TreeCode, B.ProductCode, C.ProductName," + "        A.Productionquantity As BatchQuantity,dbo.GetBatchProductionQuantity(A.BatchCode) As ProductionQuantity," + "        '      ' As ProductionProgress," + "        '        ' As PlanningDuration,'        ' As ProductionDuration,'        ' As DetourTime,0 As DetourPercent,'                ' As DetourState " + "From    dbo.Tbl_ProductionBatchs A INNER JOIN dbo.Tbl_ProductTree B ON A.ProductTreeCode = B.TreeCode INNER JOIN " + "        dbo.Tbl_Products C ON B.ProductCode = C.ProductCode " + "Where   Not dbo.GetBatchStatus(A.BatchCode) IN ('بسته شده','برنامه ريزي نشده')", Cn);
                    dtBatchs = new DataTable();
                    daBatchs.Fill(dtBatchs);
                }
                catch (Exception ex)
                {
                    Logger.SaveError("ProductionPlanning.LoadBatchsList", ex.Message);
                    MessageBox.Show("فراخوانی لیست بچ های تولید با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return;
                }
            }
        }
    }
}