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
    public partial class frmMachineEmptyTimes
    {
        public frmMachineEmptyTimes()
        {
            InitializeComponent();
            _cmdPrintPreview.Name = "cmdPrintPreview";
            _cmdCalc.Name = "cmdCalc";
            _cmdCancel.Name = "cmdCancel";
            _chkHasProduction.Name = "chkHasProduction";
            _chkHasPlanning.Name = "chkHasPlanning";
            _chkFullDayBase.Name = "chkFullDayBase";
            _chkCalendarBase.Name = "chkCalendarBase";
            _dgMachineWorkTimes.Name = "dgMachineWorkTimes";
        }

        //private frmRecordsLists mListForm;
        private DataTable dtMachines = new DataTable();
        //private DataRow mCurrentRow = null;

        private void frmMachineEmptyTimes_Load(object sender, EventArgs e)
        {
            var daOEE = new SqlDataAdapter("Select * From Tbl_Machines Where Code <> '-1' Order By Code", Module1.cnProductionPlanning);
            daOEE.Fill(dtMachines);
            {
                var withBlock = cbMachineCode;
                withBlock.MessagesHeader = "برنامه ریزی تولید ";
                withBlock.DataSource = dtMachines.DefaultView;
                withBlock.ValueMember = "Code";
                withBlock.DisplayMember = "Name";
                withBlock.ValueColumnWidth = 80;
                withBlock.ListAlternateColor = Color.CadetBlue;
            }
        }

        private void frmMachineEmptyTimes_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    var cmExistsTable = new SqlCommand("If exists (Select * From dbo.sysobjects Where id = object_id(N'dbo.Tbl_TempMachineWorking') and OBJECTPROPERTY(id, N'IsUserTable') = 1) " + " drop table dbo.Tbl_TempMachineWorking ", cn);
                    cmExistsTable.ExecuteNonQuery();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    Logger.LogException("frmMachineEmptyTimes_FormClosing", ex);
                }
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgMachineWorkTimes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgMachineWorkTimes.Columns[e.ColumnIndex].Name.Equals("colDate"))
            {
                e.Value = Strings.Mid(Conversions.ToString(e.Value), 1, 4) + "/" + Strings.Mid(Conversions.ToString(e.Value), 5, 2) + "/" + Strings.Mid(Conversions.ToString(e.Value), 7, 2);
            }
        }

        private void chkCalendarBase_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCalendarBase.Checked)
            {
                chkFullDayBase.Checked = false;
            }
            else if (!chkFullDayBase.Checked)
            {
                chkCalendarBase.Checked = true;
            }
        }

        private void chkFullDayBase_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFullDayBase.Checked)
            {
                chkCalendarBase.Checked = false;
            }
            else if (!chkCalendarBase.Checked)
            {
                chkFullDayBase.Checked = true;
            }

            // txtDownTime.Enabled = chkFullDayBase.Checked
        }

        private void chkHasPlanning_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkHasPlanning.Checked)
            {
                if (!chkHasProduction.Checked)
                {
                    chkHasPlanning.Checked = true;
                }
            }
        }

        private void chkHasProduction_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkHasProduction.Checked)
            {
                if (!chkHasPlanning.Checked)
                {
                    chkHasProduction.Checked = true;
                }
            }
        }

        private void cmdCalc_Click(object sender, EventArgs e)
        {
            if (CalcMachineEmptyTimes(Conversions.ToString(cbMachineCode.SeletedValue)))
            {
                cmdPrintPreview.Enabled = true;
            }
            else
            {
                cmdPrintPreview.Enabled = false;
            }
        }

        private bool CalcMachineEmptyTimes(string mMachnineCode)
        {
            if (!FormValidation())
            {
                return false;
            }

            try
            {
                dgMachineWorkTimes.Columns.Clear();
                if (TabControl1.TabPages.Count > 1)
                {
                    TabControl1.TabPages["tpPrintPreview"].Dispose();
                }

                if (!SetCalcBaseGridColumns())
                {
                    return false;
                }

                Cursor = Cursors.WaitCursor;
                lblCalcWaiting.Visible = true;
                Application.DoEvents();
                var dtMachineProductions = new DataTable();
                var dtMachinePlanning = new DataTable();
                string mStartDate = txtFromDate.Text;
                string mEndDate = txtToDate.Text;
                using (var cn = new SqlConnection(Module1.PlanningCnnStr))
                {
                    cn.Open();
                    var daMachineProductions = new SqlDataAdapter("", cn);
                    if (chkHasPlanning.Checked)
                    {
                        daMachineProductions.SelectCommand.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select PlanningStartDate,dbo.GetRegularTimeFromFloatingTime(PlanningStartHour) As PlanningStartHour," + "       PlanningEndDate,dbo.GetRegularTimeFromFloatingTime(PlanningEndHour) As PlanningEndHour " + "From Tbl_Planning Where MachineCode = '", cbMachineCode.SeletedValue), "' And PlanningStartDate >= '"), mStartDate), "' And PlanningStartDate <= '"), mEndDate), "' "), "Union "), "Select PlanningStartDate,dbo.GetRegularTimeFromFloatingTime(PlanningStartHour) As PlanningStartHour,"), "       PlanningEndDate,dbo.GetRegularTimeFromFloatingTime(PlanningEndHour) As PlanningEndHour "), "From Tbl_Planning Where MachineCode = '"), cbMachineCode.SeletedValue), "' And PlanningEndDate >= '"), mStartDate), "' And PlanningEndDate <= '"), mEndDate), "' "), "Union "), "Select PlanningStartDate,dbo.GetRegularTimeFromFloatingTime(PlanningStartHour) As PlanningStartHour,"), "       PlanningEndDate,dbo.GetRegularTimeFromFloatingTime(PlanningEndHour) As PlanningEndHour "), "From Tbl_Planning Where MachineCode = '"), cbMachineCode.SeletedValue), "' And PlanningStartDate < '"), mStartDate), "' And PlanningEndDate > '"), mEndDate), "'"));









                        daMachineProductions.Fill(dtMachinePlanning);
                    }

                    if (chkHasProduction.Checked)
                    {
                        daMachineProductions.SelectCommand.CommandText = "Select StartDate, StartHour, EndHour From Tbl_RealProduction Where " + GetMachineProductionFilter() + " Order By StartDate,StartHour";
                        daMachineProductions.Fill(dtMachineProductions);
                    }

                    cn.Close();
                }

                while (true) // ایجاد ردیف های نمایشی در گرید
                {
                    dgMachineWorkTimes.Rows.Add(1);
                    dgMachineWorkTimes.Rows[dgMachineWorkTimes.Rows.Count - 1].Cells["colDate"].Value = mStartDate;
                    mStartDate = FarsiDateFunctions.AddToDate(mStartDate, "00000001");
                    if (Operators.CompareString(mStartDate, mEndDate, false) > 0)
                    {
                        break;
                    }
                }

                string mCalendarCode = GetMachineCalendarCode(Conversions.ToString(cbMachineCode.SeletedValue));
                string mCalendarStart = Module1.GetCalendarStart(mCalendarCode);
                string mCalendarEnd = Module1.GetCalendarEnd(mCalendarCode);
                for (int J = 0, loopTo = dgMachineWorkTimes.Rows.Count - 1; J <= loopTo; J++)
                {
                    mStartDate = Conversions.ToString(dgMachineWorkTimes.Rows[J].Cells["colDate"].Value);
                    if (chkHasPlanning.Checked) // در صورتیکه نمایش برنامه ریزی علامت زده شده باشد
                    {
                        dtMachinePlanning.DefaultView.Sort = "PlanningStartDate,PlanningStartHour,PlanningEndDate,PlanningEndHour";
                        for (int I = 0, loopTo1 = dtMachinePlanning.DefaultView.Count - 1; I <= loopTo1; I++)
                        {
                            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectLess(dtMachinePlanning.DefaultView[I]["PlanningStartDate"], mStartDate, false)))
                            {
                                // -1در صورتیکه تاریخ جاری بعد از تاریخ شروع برنامه ریزی باشد
                                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dtMachinePlanning.DefaultView[I]["PlanningEndDate"], mStartDate, false)))
                                {
                                    CheckHasPlanningScopeInGrid(dgMachineWorkTimes.Rows[J], mCalendarStart, dtMachinePlanning.DefaultView[I]["PlanningEndHour"].ToString());
                                }
                                else if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(dtMachinePlanning.DefaultView[I]["PlanningEndDate"], mStartDate, false)))
                                {
                                    CheckHasPlanningScopeInGrid(dgMachineWorkTimes.Rows[J], mCalendarStart, mCalendarEnd);
                                }
                            }
                            else if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dtMachinePlanning.DefaultView[I]["PlanningStartDate"], mStartDate, false)))
                            {
                                // -2در صورتیکه تاریخ جاری بعد از تاریخ شروع برنامه ریزی باشد
                                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dtMachinePlanning.DefaultView[I]["PlanningEndDate"], mStartDate, false)))
                                {
                                    CheckHasPlanningScopeInGrid(dgMachineWorkTimes.Rows[J], dtMachinePlanning.DefaultView[I]["PlanningStartHour"].ToString(), dtMachinePlanning.DefaultView[I]["PlanningEndHour"].ToString());
                                }
                                else if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(dtMachinePlanning.DefaultView[I]["PlanningEndDate"], mStartDate, false)))
                                {
                                    CheckHasPlanningScopeInGrid(dgMachineWorkTimes.Rows[J], dtMachinePlanning.DefaultView[I]["PlanningStartHour"].ToString(), mCalendarEnd);
                                }
                                // ElseIf dtMachinePlanning.DefaultView(I)("PlanningStartDate") > mStartDate Then
                            }
                        }
                    }

                    if (chkHasProduction.Checked) // در صورتیکه نمایش تولید علامت زده شده باشد
                    {
                        for (int I = 1, loopTo2 = dgMachineWorkTimes.Columns.Count - 1; I <= loopTo2; I++)
                        {
                            string mProductionFilter = Constants.vbNullString;
                            DataRow[] drHasProduction;
                            if (I < dgMachineWorkTimes.Columns.Count - 1)
                            {
                                mProductionFilter = "StartDate = '" + mStartDate + "' And ((StartHour >= '" + dgMachineWorkTimes.Columns[I].HeaderText + "' And StartHour < '" + dgMachineWorkTimes.Columns[I + 1].HeaderText + "') Or (EndHour >= '" + dgMachineWorkTimes.Columns[I].HeaderText + "' And EndHour < '" + dgMachineWorkTimes.Columns[I + 1].HeaderText + "'))";
                            }
                            else
                            {
                                mProductionFilter = "StartDate = '" + mStartDate + "' And ((StartHour = '" + dgMachineWorkTimes.Columns[I].HeaderText + "') Or (EndHour = '" + dgMachineWorkTimes.Columns[I].HeaderText + "'))";
                            }

                            drHasProduction = dtMachineProductions.Select(mProductionFilter);
                            if (drHasProduction.Length > 0)
                            {
                                dtMachineProductions.DefaultView.RowFilter = mProductionFilter;
                                dtMachineProductions.DefaultView.Sort = "EndHour Desc";
                                dgMachineWorkTimes.Rows[J].Cells[I].Style.BackColor = Color.Green;
                                for (int K = I + 1, loopTo3 = dgMachineWorkTimes.Rows[J].Cells.Count - 1; K <= loopTo3; K++)
                                {
                                    if (dgMachineWorkTimes.Columns[K].HeaderText.Equals("24:00"))
                                    {
                                        if (TimeSpan.Parse("23:59:59") <= TimeSpan.Parse(dtMachineProductions.DefaultView[0]["EndHour"].ToString()))
                                        {
                                            dgMachineWorkTimes.Rows[J].Cells[K].Style.BackColor = Color.Green;
                                        }
                                    }
                                    else if (TimeSpan.Parse(dgMachineWorkTimes.Columns[K].HeaderText) <= TimeSpan.Parse(dtMachineProductions.DefaultView[0]["EndHour"].ToString()) || TimeSpan.Parse(dgMachineWorkTimes.Columns[K - 1].HeaderText) < TimeSpan.Parse(dtMachineProductions.DefaultView[0]["EndHour"].ToString()) && TimeSpan.Parse(dgMachineWorkTimes.Columns[K].HeaderText) > TimeSpan.Parse(dtMachineProductions.DefaultView[0]["EndHour"].ToString()))
                                    {
                                        dgMachineWorkTimes.Rows[J].Cells[K].Style.BackColor = Color.Green;
                                    }
                                }
                            }
                        }
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
                Logger.SaveError("ProductionPlanning.CalcMachineEmptyTimes", ex.Message);
                MessageBox.Show("محاسبه با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                dgMachineWorkTimes.Rows.Clear();
                return false;
            }
        }

        private void CheckHasPlanningScopeInGrid(DataGridViewRow dgRow, string mScopeStartLimitation, string mScopeEndLimitation)
        {
            for (int K = 1, loopTo = dgRow.Cells.Count - 1; K <= loopTo; K++)
            {
                if (dgMachineWorkTimes.Columns[K].HeaderText.Equals("24:00"))
                {
                    dgRow.Cells[K].Style.BackColor = Color.Red;
                    break;
                }
                else if (TimeSpan.Parse(dgMachineWorkTimes.Columns[K].HeaderText) >= TimeSpan.Parse(mScopeStartLimitation))
                {
                    if (TimeSpan.Parse(dgMachineWorkTimes.Columns[K].HeaderText) <= TimeSpan.Parse(mScopeEndLimitation))
                    {
                        dgRow.Cells[K].Style.BackColor = Color.Red;
                    }
                    else if (TimeSpan.Parse(dgMachineWorkTimes.Columns[K].HeaderText) > TimeSpan.Parse(mScopeEndLimitation))
                    {
                        if (TimeSpan.Parse(dgMachineWorkTimes.Columns[K - 1].HeaderText) < TimeSpan.Parse(mScopeEndLimitation))
                        {
                            dgRow.Cells[K].Style.BackColor = Color.Red;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else if (K < dgRow.Cells.Count - 1 && TimeSpan.Parse(dgMachineWorkTimes.Columns[K + 1].HeaderText) > TimeSpan.Parse(mScopeStartLimitation))
                {
                    dgRow.Cells[K].Style.BackColor = Color.Red;
                }
            }

            // For K As Integer = 1 To dgMachineWorkTimes.Rows(J).Cells.Count - 1
            // If dgMachineWorkTimes.Columns(K).HeaderText.Equals("24:00") Then
            // dgMachineWorkTimes.Rows(J).Cells(K).Style.BackColor = Color.Red
            // Exit For
            // Else
            // If TimeSpan.Parse(dgMachineWorkTimes.Columns(K).HeaderText) >= TimeSpan.Parse(mCalendarStart) Then
            // If TimeSpan.Parse(dgMachineWorkTimes.Columns(K).HeaderText) <= TimeSpan.Parse(mCalendarEnd) Then
            // dgMachineWorkTimes.Rows(J).Cells(K).Style.BackColor = Color.Red
            // ElseIf TimeSpan.Parse(dgMachineWorkTimes.Columns(K).HeaderText) > TimeSpan.Parse(mCalendarEnd) Then
            // If TimeSpan.Parse(dgMachineWorkTimes.Columns(K - 1).HeaderText) < TimeSpan.Parse(mCalendarEnd) Then
            // dgMachineWorkTimes.Rows(J).Cells(K).Style.BackColor = Color.Red
            // Exit For
            // Else
            // Exit For
            // End If
            // End If
            // End If
            // End If
            // Next K
        }

        private bool FormValidation()
        {
            if (cbMachineCode.SeletedValue is null)
            {
                MessageBox.Show("کد ماشین را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbMachineCode.Focus();
                return false;
            }

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

            if (Operators.CompareString(txttimeSlice.Text, "00:03", false) < 0)
            {
                txttimeSlice.Value = DateTime.Parse("00:03");
            }

            return true;
        }

        private bool SetCalcBaseGridColumns()
        {
            string mCalendarStart = "01:00";
            string mCalendarEnd = "23:59:59";
            string mAddedTimeSlice = Conversions.ToString(Interaction.IIf(txttimeSlice.Text.Equals("00:00"), "01:00", txttimeSlice.Text));
            var mcol = new DataGridViewColumn(new DataGridViewTextBoxCell());
            int mColCounter = 0;
            int mColWidth = 37;
            if (chkCalendarBase.Checked) // محاسبه بر اساس تقویم
            {
                int mCalendarCode = Conversions.ToInteger(GetMachineCalendarCode(Conversions.ToString(cbMachineCode.SeletedValue)));
                if (mCalendarCode == 0)
                {
                    return false;
                }

                mCalendarStart = Module1.GetCalendarStart(mCalendarCode.ToString());
                mCalendarEnd = Module1.GetCalendarEnd(mCalendarCode.ToString());
            }

            mcol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            mcol.Width = 80;
            mcol.Name = "colDate";
            mcol.HeaderText = "تاریخ";
            dgMachineWorkTimes.Columns.Add(mcol);
            mcol = new DataGridViewColumn(new DataGridViewTextBoxCell());
            mcol.HeaderCell.Style.Font = new Font("Arial", 7f, FontStyle.Regular, GraphicsUnit.Point);
            mcol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            mColCounter += 1;
            mcol.Name = "colTime" + mColCounter.ToString();
            mcol.HeaderText = mCalendarStart;
            mcol.Width = mColWidth;
            dgMachineWorkTimes.Columns.Add(mcol);
            while (true)
            {
                if (TimeSpan.Parse(mCalendarStart) + TimeSpan.Parse(mAddedTimeSlice) > TimeSpan.Parse(mCalendarEnd))
                {
                    if (chkCalendarBase.Checked)
                    {
                        mCalendarStart = mCalendarEnd;
                    }
                    else
                    {
                        mCalendarStart = "24:00";
                    }
                }
                else
                {
                    mCalendarStart = (TimeSpan.Parse(mCalendarStart) + TimeSpan.Parse(mAddedTimeSlice)).ToString().Substring(0, 5);
                }

                mcol = new DataGridViewColumn(new DataGridViewTextBoxCell());
                mcol.HeaderCell.Style.Font = new Font("Arial", 7f, FontStyle.Regular, GraphicsUnit.Point);
                mcol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                mColCounter += 1;
                mcol.Name = "colTime" + mColCounter.ToString();
                mcol.HeaderText = mCalendarStart;
                mcol.Width = mColWidth;
                dgMachineWorkTimes.Columns.Add(mcol);
                if (mCalendarStart == "24:00" || TimeSpan.Parse(mCalendarStart) >= TimeSpan.Parse(mCalendarEnd))
                {
                    break;
                }
            }

            return true;
        }

        private string GetMachineProductionFilter()
        {
            string mConditions;
            mConditions = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("MachineCode = '", cbMachineCode.SeletedValue), "'"));
            mConditions += " And StartDate >= '" + txtFromDate.Text + "'";
            mConditions += " And StartDate <= '" + txtToDate.Text + "'";
            return mConditions;
        }

        private string GetMachinePlanningFilter()
        {
            string mConditions;
            mConditions = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("MachineCode = '", cbMachineCode.SeletedValue), "'"));
            mConditions += " And ((PlanningStartDate >= '" + txtFromDate.Text + "' And PlanningStartDate <= '" + txtToDate.Text + "')";
            mConditions += " Or (PlanningEndDate >= '" + txtFromDate.Text + "' And PlanningEndDate <= '" + txtToDate.Text + "'))";
            return mConditions;
        }

        private string GetMachineCalendarCode(string MachineCode)
        {
            string CalendarCode = "0";
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmCalendar = new SqlCommand("Select CalendarCode From Tbl_Machines Where Code = '" + MachineCode + "'", cn);
                var drCalendar = cmCalendar.ExecuteReader();
                if (drCalendar.Read())
                {
                    CalendarCode = Conversions.ToString(drCalendar["CalendarCode"]);
                }

                drCalendar.Close();
                cn.Close();
            }

            return CalendarCode;
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                InsertDataBaseTableRows(dgMachineWorkTimes);
                Cursor = Cursors.WaitCursor;
                var tpPrintPreview = new TabPage("پیش نمایش چاپ");
                tpPrintPreview.Name = "tpPrintPreview";
                if (TabControl1.TabPages.Count < 2 || !TabControl1.TabPages[1].Name.Equals("tpPrintPreview"))
                {
                    var cmdClosePreview = new Button();
                    cmdClosePreview.Text = "بستن";
                    cmdClosePreview.TextAlign = ContentAlignment.MiddleCenter;
                    cmdClosePreview.Font = new Font("Tahoma", 10f, FontStyle.Bold);
                    cmdClosePreview.Size = new Size(100, 30);
                    cmdClosePreview.Location = new Point(20, 10);
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
                Logger.SaveError("ProductionPlanning.cmdPrint_Click", objEx.Message);
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
            if (File.Exists(mrptPath + "Rep102_MachineWorkTimes.rpt"))
            {
                RptDoc.Load(mrptPath + "Rep102_MachineWorkTimes.rpt", OpenReportMethod.OpenReportByDefault);
                RptDoc.ParameterFields["PrintDate"].CurrentValues.AddValue(Module1.mServerShamsiDate);
                RptDoc.ParameterFields["MachineCode"].CurrentValues.AddValue(cbMachineCode.Text);
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

        private void InsertDataBaseTableRows(DataGridView dg)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var trnInsert = cn.BeginTransaction();
                var cmInsertOvers = new SqlCommand("if Not Exists(select * from dbo.sysobjects where id = object_id(N'[dbo].[Tbl_TempMachineWorking]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)" + "Begin " + "  Create Table [dbo].[Tbl_TempMachineWorking](" + "               [RowIndex]   [int] NOT NULL," + "               [CalcDate]   [varchar] (8) COLLATE Arabic_CS_AS_KS_WS NOT NULL," + "               [CalcTime]   [varchar] (5) COLLATE Arabic_CS_AS_KS_WS NOT NULL," + "               [WorkStatus] [tinyint] NOT NULL)" + "End", cn);






                try
                {
                    cmInsertOvers.Transaction = trnInsert;
                    cmInsertOvers.ExecuteNonQuery();
                    cmInsertOvers.CommandText = "Delete From Tbl_TempMachineWorking";
                    cmInsertOvers.ExecuteNonQuery();
                    cmInsertOvers.CommandText = "Insert Into Tbl_TempMachineWorking(RowIndex,CalcDate,CalcTime,WorkStatus) Values(@RowIndex,@CalcDate,@CalcTime,@WorkStatus)";
                    {
                        var withBlock = cmInsertOvers.Parameters;
                        withBlock.Add("@RowIndex", SqlDbType.Int, default, "RowIndex");
                        withBlock.Add("@CalcDate", SqlDbType.VarChar, 8, "CalcDate");
                        withBlock.Add("@CalcTime", SqlDbType.VarChar, 5, "CalcTime");
                        withBlock.Add("@WorkStatus", SqlDbType.TinyInt, default, "WorkStatus");
                    }

                    int RowIndex = 1;
                    for (int I = 0, loopTo = dg.Rows.Count - 1; I <= loopTo; I++)
                    {
                        for (int J = 1, loopTo1 = dg.Columns.Count - 1; J <= loopTo1; J++)
                        {
                            cmInsertOvers.Parameters["@RowIndex"].Value = RowIndex;
                            cmInsertOvers.Parameters["@CalcDate"].Value = dg.Rows[I].Cells["colDate"].Value;
                            cmInsertOvers.Parameters["@CalcTime"].Value = dg.Columns[J].HeaderText;
                            if (dg.Rows[I].Cells[J].Style.BackColor == Color.Green)
                            {
                                cmInsertOvers.Parameters["@WorkStatus"].Value = 1;
                            }
                            else if (dg.Rows[I].Cells[J].Style.BackColor == Color.Red)
                            {
                                cmInsertOvers.Parameters["@WorkStatus"].Value = 2;
                            }
                            else
                            {
                                cmInsertOvers.Parameters["@WorkStatus"].Value = 0;
                            }

                            cmInsertOvers.ExecuteNonQuery();
                            RowIndex += 1;
                        }
                    }

                    trnInsert.Commit();
                }
                catch (Exception objEx)
                {
                    trnInsert.Rollback();
                    Logger.SaveError("ProductionPlanning.InsertDataBaseTableRows", objEx.Message);
                    MessageBox.Show("چاپ لیست کارکرد و زمانهای خالی ماشین با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }
        }
    }
}