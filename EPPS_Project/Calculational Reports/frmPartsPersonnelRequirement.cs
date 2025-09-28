using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Button = System.Windows.Forms.Button;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmPartsPersonnelRequirement
    {
        public frmPartsPersonnelRequirement()
        {
            InitializeComponent();
            _cmdPrintPreview.Name = "cmdPrintPreview";
            _cmdCancel.Name = "cmdCancel";
            _cmdCalc.Name = "cmdCalc";
        }

        private DataTable dtParts = new DataTable();

        private void frmPartsPersonnelRequirement_Load(object sender, EventArgs e)
        {
            using (var Cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    var daParts = new SqlDataAdapter("Select * From Tbl_Natures Order By NatureTitle", Cn);
                    daParts.Fill(dtParts);
                }
                catch (Exception ex)
                {
                    Logger.SaveError(Name + ".frmPartsPersonnelRequirement_Load", ex.Message);
                    MessageBox.Show("فراخوانی لیست بخش های تولید با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    Close();
                    return;
                }
            }
        }

        private void frmPartsPersonnelRequirement_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    var cmExistsTable = new SqlCommand("If exists (Select * From dbo.sysobjects Where id = object_id(N'dbo.Tbl_TempPartsPersonnelRequirement') and OBJECTPROPERTY(id, N'IsUserTable') = 1) " + " drop table dbo.Tbl_TempPartsPersonnelRequirement ", cn);
                    cmExistsTable.ExecuteNonQuery();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    Logger.LogException("frmPartsPersonnelRequirement_FormClosing", ex);
                }
            }

            dtParts.Dispose();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdCalc_Click(object sender, EventArgs e)
        {
            if (CalcPartsPersonnelRequirement())
            {
                cmdPrintPreview.Enabled = true;
            }
            else
            {
                cmdPrintPreview.Enabled = false;
            }
        }

        private bool CalcPartsPersonnelRequirement()
        {
            if (!FormValidation())
            {
                return false;
            }

            try
            {
                dgPartsPersonnelRequirement.Columns.Clear();
                if (TabControl1.TabPages.Count > 1)
                {
                    TabControl1.TabPages["tpPrintPreview"].Dispose();
                }

                if (!SetGridColumns())
                {
                    return false;
                }

                Cursor = Cursors.WaitCursor;
                lblCalcWaiting.Visible = true;
                Application.DoEvents();
                var dtPartsRequirement = new DataTable();
                string mStartDate = txtFromDate.Text;
                string mEndDate = txtToDate.Text;
                using (var cn = new SqlConnection(Module1.PlanningCnnStr))
                {
                    cn.Open();
                    var daPartsRequirement = new SqlDataAdapter("", cn);
                    daPartsRequirement.SelectCommand.CommandText = "Select * From View_PlanningOperationPartsDuration Where PlanningStartDate >= '" + mStartDate + "' And PlanningStartDate <= '" + mEndDate + "' " + "Union " + "Select * From View_PlanningOperationPartsDuration Where PlanningEndDate >= '" + mStartDate + "' And PlanningEndDate <= '" + mEndDate + "' " + "Union " + "Select * From View_PlanningOperationPartsDuration Where PlanningStartDate < '" + mStartDate + "' And PlanningEndDate > '" + mEndDate + "'";



                    daPartsRequirement.Fill(dtPartsRequirement);
                    cn.Close();
                }

                while (true) // ایجاد ردیف های نمایشی در گرید
                {
                    dgPartsPersonnelRequirement.Rows.Add(1);
                    dgPartsPersonnelRequirement.Rows[dgPartsPersonnelRequirement.Rows.Count - 1].Cells["colValuesTitle"].Value = "تعداد نفرات";
                    for (short I = 1, loopTo = (short)(dgPartsPersonnelRequirement.Rows[dgPartsPersonnelRequirement.Rows.Count - 1].Cells.Count - 1); I <= loopTo; I++)
                    {
                        string mPartCode = dgPartsPersonnelRequirement.Columns[I].Tag.ToString();
                        dgPartsPersonnelRequirement.Rows[dgPartsPersonnelRequirement.Rows.Count - 1].Cells[I].Value = GetPartPersonnelQuantity(mPartCode).ToString();
                    }

                    dgPartsPersonnelRequirement.Rows.Add(1);
                    dgPartsPersonnelRequirement.Rows[dgPartsPersonnelRequirement.Rows.Count - 1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgPartsPersonnelRequirement.Rows[dgPartsPersonnelRequirement.Rows.Count - 1].HeaderCell.Value = Module1.GetFormatedDate(mStartDate);
                    dgPartsPersonnelRequirement.Rows[dgPartsPersonnelRequirement.Rows.Count - 1].Cells["colValuesTitle"].Value = "زمان در دسترس";
                    for (short I = 1, loopTo1 = (short)(dgPartsPersonnelRequirement.Rows[dgPartsPersonnelRequirement.Rows.Count - 1].Cells.Count - 1); I <= loopTo1; I++)
                    {
                        string mPartCode = dgPartsPersonnelRequirement.Columns[I].Tag.ToString();
                        string mCalendarCode = GetPartCalendarCode(mPartCode);
                        short ParticulareDayType = Module1.IsParticularDay(mCalendarCode, mStartDate);
                        string DayAccessibleTime = "00:00";
                        const int DT_HOLIDAY = 1;
                        const int DT_COMMON = 2;
                        switch (ParticulareDayType)
                        {
                            case -1: // روز عادی
                                {
                                    DayAccessibleTime = Module1.GetDayAccessibleTime(mCalendarCode, Module1.GetDayNo(mStartDate).ToString(), 1);
                                    break;
                                }

                            case DT_COMMON:
                            case DT_HOLIDAY: // روز خاص
                                {
                                    DayAccessibleTime = Module1.GetDayAccessibleTime(mCalendarCode, Module1.GetDayNo(mStartDate).ToString(), 2);
                                    break;
                                }
                        }

                        int mPersonnelQuantity = Conversions.ToInteger(dgPartsPersonnelRequirement.Rows[dgPartsPersonnelRequirement.Rows.Count - 2].Cells[I].Value);
                        double mFloatingTime = Conversions.ToDouble(Module1.GetFloatingHour(DayAccessibleTime));
                        mFloatingTime *= mPersonnelQuantity;
                        DayAccessibleTime = Module1.GetRegulareHour(mFloatingTime.ToString());
                        dgPartsPersonnelRequirement.Rows[dgPartsPersonnelRequirement.Rows.Count - 1].Cells[I].Value = DayAccessibleTime;
                    }

                    dgPartsPersonnelRequirement.Rows.Add(1);
                    dgPartsPersonnelRequirement.Rows[dgPartsPersonnelRequirement.Rows.Count - 1].Cells["colValuesTitle"].Value = "زمان مورد نیاز";
                    mStartDate = FarsiDateFunctions.AddToDate(mStartDate, "00000001");
                    if (Operators.CompareString(mStartDate, mEndDate, false) > 0)
                    {
                        break;
                    }
                }

                for (int J = 0, loopTo2 = dgPartsPersonnelRequirement.Rows.Count - 1; J <= loopTo2; J += 3)
                {
                    int RowIndex = J + 1;
                    mStartDate = dgPartsPersonnelRequirement.Rows[RowIndex].HeaderCell.Value.ToString().Replace("/", "");
                    for (int ColIndex = 1, loopTo3 = dgPartsPersonnelRequirement.Rows[RowIndex].Cells.Count - 1; ColIndex <= loopTo3; ColIndex++)
                    {
                        string mCalendarCode = GetPartCalendarCode(dgPartsPersonnelRequirement.Columns[ColIndex].Tag.ToString());
                        string mCalendarStart = Module1.GetCalendarStart(mCalendarCode);
                        string mCalendarEnd = Module1.GetCalendarEnd(mCalendarCode);
                        double mPartRequirementTime = 0.0d;
                        if (!Module1.IsHoliday(mCalendarCode, mStartDate))
                        {
                            dtPartsRequirement.DefaultView.RowFilter = "PartCode = " + dgPartsPersonnelRequirement.Columns[ColIndex].Tag.ToString();
                            dtPartsRequirement.DefaultView.Sort = "PlanningStartDate,PlanningStartHour,PlanningEndDate,PlanningEndHour";
                            for (int I = 0, loopTo4 = dtPartsRequirement.DefaultView.Count - 1; I <= loopTo4; I++)
                            {
                                string mStartTime = "00:00";
                                string mEndTime = "00:00";
                                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectLess(dtPartsRequirement.DefaultView[I]["PlanningStartDate"], mStartDate, false))) // -1در صورتیکه تاریخ جاری بعد از تاریخ شروع برنامه ریزی باشد
                                {
                                    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dtPartsRequirement.DefaultView[I]["PlanningEndDate"], mStartDate, false)))
                                    {
                                        mStartTime = mCalendarStart;
                                        mEndTime = dtPartsRequirement.DefaultView[I]["PlanningEndHour"].ToString();
                                    }
                                    else if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(dtPartsRequirement.DefaultView[I]["PlanningEndDate"], mStartDate, false)))
                                    {
                                        mStartTime = mCalendarStart;
                                        mEndTime = mCalendarEnd;
                                    }
                                }
                                else if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dtPartsRequirement.DefaultView[I]["PlanningStartDate"], mStartDate, false))) // -2در صورتیکه تاریخ جاری بعد از تاریخ شروع برنامه ریزی باشد
                                {
                                    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dtPartsRequirement.DefaultView[I]["PlanningEndDate"], mStartDate, false)))
                                    {
                                        mStartTime = dtPartsRequirement.DefaultView[I]["PlanningStartHour"].ToString();
                                        mEndTime = dtPartsRequirement.DefaultView[I]["PlanningEndHour"].ToString();
                                    }
                                    else if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(dtPartsRequirement.DefaultView[I]["PlanningEndDate"], mStartDate, false)))
                                    {
                                        mStartTime = dtPartsRequirement.DefaultView[I]["PlanningStartHour"].ToString();
                                        mEndTime = mCalendarEnd;
                                    }
                                }

                                mPartRequirementTime += Conversions.ToDouble(Module1.GetFloatingHour((TimeSpan.Parse(mEndTime) - TimeSpan.Parse(mStartTime)).ToString().Substring(0, 5)));
                            }
                        }

                        dgPartsPersonnelRequirement.Rows[RowIndex + 1].Cells[ColIndex].Value = Module1.GetRegulareHour(mPartRequirementTime.ToString());
                        int CellColorCode = GetDifferenceCode(dgPartsPersonnelRequirement.Rows[RowIndex].Cells[ColIndex].Value.ToString(), dgPartsPersonnelRequirement.Rows[RowIndex + 1].Cells[ColIndex].Value.ToString(), txtDifferenceTime.Text);
                        if (CellColorCode == 1)
                        {
                            dgPartsPersonnelRequirement.Rows[RowIndex + 1].Cells[ColIndex].Style.ForeColor = Color.Red;
                        }
                        else if (CellColorCode == 2)
                        {
                            dgPartsPersonnelRequirement.Rows[RowIndex].Cells[ColIndex].Style.ForeColor = Color.Green;
                        }
                    }
                }

                for (int I = 0, loopTo5 = dgPartsPersonnelRequirement.Rows.Count - 1; I <= loopTo5; I += 3)
                {
                    if (I % 2 == 0)
                    {
                        dgPartsPersonnelRequirement.Rows[I].DefaultCellStyle.BackColor = Color.Beige;
                        dgPartsPersonnelRequirement.Rows[I + 1].DefaultCellStyle.BackColor = Color.Beige;
                        dgPartsPersonnelRequirement.Rows[I + 2].DefaultCellStyle.BackColor = Color.Beige;
                    }
                    else
                    {
                        dgPartsPersonnelRequirement.Rows[I].DefaultCellStyle.BackColor = Color.White;
                        dgPartsPersonnelRequirement.Rows[I + 1].DefaultCellStyle.BackColor = Color.White;
                        dgPartsPersonnelRequirement.Rows[I + 2].DefaultCellStyle.BackColor = Color.White;
                    }
                }

                lblCalcWaiting.Visible = false;
                Cursor = Cursors.Default;
                Application.DoEvents();
                return true;
            }
            catch (Exception ex)
            {
                lblCalcWaiting.Visible = false;
                Cursor = Cursors.Default;
                Application.DoEvents();
                Logger.LogException(Name + ".CalcPartsPersonnelRequirement", ex);
                MessageBox.Show("محاسبه با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                dgPartsPersonnelRequirement.Rows.Clear();
                return false;
            }
        }

        private bool FormValidation()
        {
            if (txtFromDate.Text is null || string.IsNullOrEmpty(txtFromDate.Text) || txtFromDate.Text.Equals("") || txtFromDate.Text.Trim().Length < 8)
            {
                MessageBox.Show("(محدودۀ تاریخ محاسبه صحیح نیست(از تاریخ", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtFromDate.Focus();
                return false;
            }

            if (txtToDate.Text is null || string.IsNullOrEmpty(txtToDate.Text) || txtToDate.Text.Equals("") || txtToDate.Text.Trim().Length < 8)
            {
                MessageBox.Show("(محدودۀ تاریخ محاسبه صحیح نیست(تا تاریخ", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtToDate.Focus();
                return false;
            }

            return true;
        }

        private bool SetGridColumns()
        {
            var mcol = new DataGridViewColumn(new DataGridViewTextBoxCell());
            int mColWidth = 100;
            try
            {
                // اضافه کردن ستون عنوان مقادیر نمایشی به گرید
                mcol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                mcol.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                mcol.Width = 100;
                mcol.Name = "colValuesTitle";
                mcol.HeaderText = "";
                dgPartsPersonnelRequirement.Columns.Add(mcol);

                // اضافه کردن ستون مربوط به هر بخش تولید
                for (int I = 0, loopTo = dtParts.Rows.Count - 1; I <= loopTo; I++)
                {
                    mcol = new DataGridViewColumn(new DataGridViewTextBoxCell());
                    mcol.HeaderCell.Style.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point);
                    mcol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    mcol.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    mcol.Name = "colPart" + dtParts.Rows[I]["NatureCode"].ToString();
                    mcol.Tag = dtParts.Rows[I]["NatureCode"].ToString();
                    mcol.HeaderText = dtParts.Rows[I]["NatureTitle"].ToString();
                    mColWidth = dtParts.Rows[I]["NatureTitle"].ToString().Length * 6;
                    mcol.Width = mColWidth;
                    if (mcol.Width < 60)
                    {
                        mcol.Width = 60;
                    }

                    dgPartsPersonnelRequirement.Columns.Add(mcol);
                }
            }
            catch (Exception objEx)
            {
                Logger.SaveError(Name + ".SetGridColumns", objEx.Message);
                MessageBox.Show("ایجاد ساختار گرید با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                return false;
            }

            return true;
        }

        private int GetPartPersonnelQuantity(string mPartCode)
        {
            int PersonnelsQuantity = 0;
            using (var Cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    Cn.Open();
                    var cmPersonnelsQuantity = new SqlCommand("Select IsNull(Count(*),0) From Tbl_Operators Where ProductionPart = " + mPartCode, Cn);
                    PersonnelsQuantity = Conversions.ToInteger(cmPersonnelsQuantity.ExecuteScalar().ToString());
                }
                catch (Exception ex)
                {
                    Logger.LogException("GetPartPersonnelQuantity", ex);
                    PersonnelsQuantity = 0;
                }
                finally
                {
                    if (Cn.State == ConnectionState.Open)
                        Cn.Close();
                }
            }

            return PersonnelsQuantity;
        }

        private string GetPartCalendarCode(string mPartCode)
        {
            string CalendarCode = "0";
            var drPart = dtParts.Select("NatureCode = " + mPartCode);
            if (drPart.Length > 0)
            {
                CalendarCode = drPart[0]["CalendarCode"].ToString();
            }

            return CalendarCode;
        }

        private int GetDifferenceCode(string mAccessibleTime, string mRequirementTime, string mDifferenceTime)
        {
            // 0: ValidTime   1: OverRequirementTime   2: OverAccessibleTmie
            double mAccessibleDouble, mRequirementDouble, mDifferenceTimeDouble, mDifferenceDouble;
            mAccessibleDouble = Conversions.ToDouble(Module1.GetFloatingHour(mAccessibleTime));
            mRequirementDouble = Conversions.ToDouble(Module1.GetFloatingHour(mRequirementTime));
            mDifferenceTimeDouble = Conversions.ToDouble(Module1.GetFloatingHour(mDifferenceTime));
            if (mAccessibleDouble > mRequirementDouble)
            {
                mDifferenceDouble = mAccessibleDouble - mRequirementDouble;
                if (mDifferenceDouble > mDifferenceTimeDouble)
                {
                    return 2;
                }
            }
            else if (mAccessibleDouble < mRequirementDouble)
            {
                mDifferenceDouble = mRequirementDouble - mAccessibleDouble;
                if (mDifferenceDouble > mDifferenceTimeDouble)
                {
                    return 1;
                }
            }

            return 0;
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            if (!InsertDataBaseTableRows(dgPartsPersonnelRequirement))
            {
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                var tpPrintPreview = new TabPage("پیش نمایش چاپ");
                tpPrintPreview.Name = "tpPrintPreview";
                if (TabControl1.TabPages.Count < 2 || !TabControl1.TabPages[1].Name.Equals("tpPrintPreview"))
                {
                    var cmdClosePreview = new Button();
                    cmdClosePreview.Text = "بستن پیش نمایش";
                    cmdClosePreview.TextAlign = ContentAlignment.MiddleCenter;
                    cmdClosePreview.Font = new Font("Tahoma", 9f, FontStyle.Regular);
                    cmdClosePreview.Size = new Size(110, 25);
                    cmdClosePreview.Location = new Point(15, 15);
                    cmdClosePreview.BackColor = Color.SkyBlue;
                    cmdClosePreview.Click += cmdClosePreview_Click;
                    tpPrintPreview.Controls.Add(cmdClosePreview);
                    var ReportViewer = new CrystalReportViewer();
                    ReportViewer.Size = new Size(tpPrintPreview.Width - 5, tpPrintPreview.Height - 55);
                    ReportViewer.Location = new Point(5, 50);
                    ReportViewer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
                    ReportViewer.DisplayGroupTree = false;
                    ReportViewer.RightToLeft = RightToLeft.No;
                    if (!LoadReport(ReportViewer))
                    {
                        return;
                    }

                    tpPrintPreview.Controls.Add(ReportViewer);
                    TabControl1.TabPages.Add(tpPrintPreview);
                    TabControl1.SelectTab(tpPrintPreview);
                }
                else
                {
                    LoadReport((CrystalReportViewer)TabControl1.TabPages[1].Controls[1]);
                }

                Cursor = Cursors.Default;
            }
            catch (Exception objEx)
            {
                Cursor = Cursors.Default;
                Logger.SaveError(Name + ".cmdPrint_Click", objEx.Message);
                MessageBox.Show("نمایش نسخۀ چاپی با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }

        private void cmdClosePreview_Click(object sender, EventArgs e)
        {
            TabControl1.TabPages["tpPrintPreview"].Dispose();
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
            if (File.Exists(mrptPath + "Rep102_PartsPersonnelRequirement.rpt"))
            {
                RptDoc.Load(mrptPath + "Rep102_PartsPersonnelRequirement.rpt", OpenReportMethod.OpenReportByDefault);
                RptDoc.ParameterFields["PrintDate"].CurrentValues.AddValue(Module1.mServerShamsiDate);
                RptDoc.ParameterFields["FromDate"].CurrentValues.AddValue(txtFromDate.Text);
                RptDoc.ParameterFields["ToDate"].CurrentValues.AddValue(txtToDate.Text);
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

        private bool InsertDataBaseTableRows(DataGridView dg)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var trnInsert = cn.BeginTransaction();
                var cmInsertOvers = new SqlCommand("if Not Exists(select * from dbo.sysobjects where id = object_id(N'[dbo].[Tbl_TempPartsPersonnelRequirement]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)" + "Begin " + "Create Table [dbo].[Tbl_TempPartsPersonnelRequirement] (" + "	[RowIndex]          [int] NOT NULL ," + "	[CalcDate]          [varchar] (8) Collate Arabic_CS_AS_KS_WS NOT NULL ," + "	[PartCode]          [int] NOT NULL ," + "	[PartName]          [varchar] (50) Collate Arabic_CS_AS_KS_WS NOT NULL ," + "	[ColorCode]         [tinyint] NOT NULL ," + "	[RowHeaderTitle]    [varchar] (50) Collate Arabic_CS_AS_KS_WS NOT NULL ," + "	[RowHeaderValue]   [varchar] (10) Collate Arabic_CS_AS_KS_WS NOT NULL)" + "End", cn);









                try
                {
                    cmInsertOvers.Transaction = trnInsert;
                    cmInsertOvers.ExecuteNonQuery();
                    cmInsertOvers.CommandText = "Delete From Tbl_TempPartsPersonnelRequirement";
                    cmInsertOvers.ExecuteNonQuery();
                    cmInsertOvers.CommandText = "Insert Into Tbl_TempPartsPersonnelRequirement(RowIndex,CalcDate,PartCode,PartName,ColorCode,RowHeaderTitle,RowHeaderValue) " + "Values(@RowIndex,@CalcDate,@PartCode,@PartName,@ColorCode,@RowHeaderTitle,@RowHeaderValue)";
                    {
                        var withBlock = cmInsertOvers.Parameters;
                        withBlock.Add("@RowIndex", SqlDbType.Int, default, "RowIndex");
                        withBlock.Add("@CalcDate", SqlDbType.VarChar, 8, "CalcDate");
                        withBlock.Add("@PartCode", SqlDbType.Int, default, "PartCode");
                        withBlock.Add("@PartName", SqlDbType.VarChar, 50, "PartName");
                        withBlock.Add("@ColorCode", SqlDbType.TinyInt, default, "ColorCode");
                        withBlock.Add("@RowHeaderTitle", SqlDbType.VarChar, 50, "RowHeaderTitle");
                        withBlock.Add("@RowHeaderValue", SqlDbType.VarChar, 10, "RowHeaderValue");
                    }

                    int RowIndex = 1;
                    for (int I = 0, loopTo = dg.Rows.Count - 1; I <= loopTo; I += 3)
                    {
                        string CalcDate = dg.Rows[I + 1].HeaderCell.Value.ToString().Replace("/", "");
                        for (int K = I, loopTo1 = I + 2; K <= loopTo1; K++)
                        {
                            string RowHeaderTitle = Conversions.ToString(Interaction.IIf(K == I, "تعداد نفرات", Interaction.IIf(K == I + 1, "زمان در دسترس", "زمان مورد نیاز")));
                            for (int J = 1, loopTo2 = dg.Columns.Count - 1; J <= loopTo2; J++)
                            {
                                short RowColorCode = Conversions.ToShort(Interaction.IIf(K == I, 0, Interaction.IIf(dg.Rows[K].Cells[J].Style.ForeColor == Color.Green, 2, Interaction.IIf(dg.Rows[K].Cells[J].Style.ForeColor == Color.Red, 1, 0))));
                                cmInsertOvers.Parameters["@RowIndex"].Value = RowIndex;
                                cmInsertOvers.Parameters["@CalcDate"].Value = CalcDate;
                                cmInsertOvers.Parameters["@PartCode"].Value = dg.Columns[J].Tag.ToString();
                                cmInsertOvers.Parameters["@PartName"].Value = dg.Columns[J].HeaderText;
                                cmInsertOvers.Parameters["@ColorCode"].Value = RowColorCode;
                                cmInsertOvers.Parameters["@RowHeaderTitle"].Value = RowHeaderTitle;
                                cmInsertOvers.Parameters["@RowHeaderValue"].Value = dg.Rows[K].Cells[J].Value;
                                cmInsertOvers.ExecuteNonQuery();
                                RowIndex += 1;
                            }
                        }
                    }

                    trnInsert.Commit();
                }
                catch (Exception objEx)
                {
                    trnInsert.Rollback();
                    Logger.SaveError(Name + ".InsertDataBaseTableRows", objEx.Message);
                    MessageBox.Show("چاپ ظرفیت مورد نیاز پرسنل بخش های تولید با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
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
    }
}