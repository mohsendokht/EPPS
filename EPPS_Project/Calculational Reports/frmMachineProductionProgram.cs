using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmMachineProductionProgram
    {
        public frmMachineProductionProgram()
        {
            InitializeComponent();
            _cmdPrintPreview.Name = "cmdPrintPreview";
            _cmdCalc.Name = "cmdCalc";
            _cmdCancel.Name = "cmdCancel";
            _chkAllSubbatchs.Name = "chkAllSubbatchs";
            _chkAllParts.Name = "chkAllParts";
            _chkAllMachines.Name = "chkAllMachines";
            _dgMachineWorkTimes.Name = "dgMachineWorkTimes";
        }

        private DataTable dtMachines = new DataTable();
        private DataTable dtParts = new DataTable();
        private DataTable dtSubbatchs = new DataTable();
        //private DataRow mCurrentRow = null;
        private DataTable dtMachineProductionProgram = new DataTable("dtMachineProductionProgram");

        private void frmMachineProductionProgram_Load(object sender, EventArgs e)
        {
            var daOEE = new SqlDataAdapter("Select * From Tbl_Machines Where Code <> '-1' Order By Code", Module1.cnProductionPlanning);
            daOEE.Fill(dtMachines);
            daOEE.SelectCommand.CommandText = "Select * From Tbl_Natures";
            daOEE.Fill(dtParts);
            daOEE.SelectCommand.CommandText = "Select SubbatchCode From Tbl_ProductionSubbatchs Where BatchCode IN (Select BatchCode From Tbl_ProductionBatchs Where FinishedDate = '0' Or FinishedDate Is Null)";
            daOEE.Fill(dtSubbatchs);
            {
                var withBlock = cbMachineCode;
                withBlock.MessagesHeader = "برنامه ریزی تولید ";
                withBlock.DataSource = dtMachines.DefaultView;
                withBlock.ValueMember = "Code";
                withBlock.DisplayMember = "Name";
                withBlock.ValueColumnWidth = 80;
                withBlock.ListAlternateColor = Color.CadetBlue;
                withBlock.Enabled = false;
            }

            {
                var withBlock1 = cbPartCode;
                withBlock1.MessagesHeader = "برنامه ریزی تولید ";
                withBlock1.DataSource = dtParts.DefaultView;
                withBlock1.ValueMember = "NatureCode";
                withBlock1.DisplayMember = "NatureTitle";
                withBlock1.ValueColumnWidth = 50;
                withBlock1.ListAlternateColor = Color.Pink;
                withBlock1.Enabled = false;
            }

            {
                var withBlock2 = cbSubbatchCode;
                withBlock2.DataSource = dtSubbatchs.DefaultView;
                withBlock2.ValueMember = "SubbatchCode";
                withBlock2.DisplayMember = "SubbatchCode";
                withBlock2.SelectedIndex = -1;
                withBlock2.Enabled = false;
            }
        }

        private void frmMachineProductionProgram_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    var cmExistsTable = new SqlCommand("If exists (Select * From dbo.sysobjects Where id = object_id(N'dbo.Tbl_TempMachineProductionProgram') and OBJECTPROPERTY(id, N'IsUserTable') = 1) " + " drop table dbo.Tbl_TempMachineProductionProgram ", cn);
                    cmExistsTable.ExecuteNonQuery();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    Logger.LogException("frmMachineProductionProgram_FormClosing", ex);
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

        private void chkAllMachines_CheckedChanged(object sender, EventArgs e)
        {
            cbMachineCode.Enabled = !chkAllMachines.Checked;
        }

        private void chkAllParts_CheckedChanged(object sender, EventArgs e)
        {
            cbPartCode.Enabled = !chkAllParts.Checked;
        }

        private void chkAllSubbatchs_CheckedChanged(object sender, EventArgs e)
        {
            cbSubbatchCode.Enabled = !chkAllSubbatchs.Checked;
        }

        private void cmdCalc_Click(object sender, EventArgs e)
        {
            if (!FormValidation())
            {
                return;
            }

            string mMachineCode = "-1";
            string mPartCode = "-1";
            string mSubbatchCode = "-1";
            if (!chkAllMachines.Checked)
            {
                mMachineCode = Conversions.ToString(cbMachineCode.SeletedValue);
            }

            if (!chkAllParts.Checked)
            {
                mPartCode = Conversions.ToString(cbPartCode.SeletedValue);
            }

            if (!chkAllSubbatchs.Checked)
            {
                mSubbatchCode = Conversions.ToString(cbSubbatchCode.SelectedValue);
            }

            if (CalcMachineProductionProgram(mSubbatchCode, mMachineCode, mPartCode))
            {
                cmdPrintPreview.Enabled = true;
            }
            else
            {
                cmdPrintPreview.Enabled = false;
            }
        }

        private bool CalcMachineProductionProgram(string mSubbatchCode, string mMachnineCode, string mPartCode)
        {
            try
            {
                dtMachineProductionProgram = new DataTable();
                dgMachineWorkTimes.Rows.Clear();
                if (TabControl1.TabPages.Count > 1)
                {
                    TabControl1.TabPages["tpPrintPreview"].Dispose();
                }

                Cursor = Cursors.WaitCursor;
                lblCalcWaiting.Visible = true;
                Application.DoEvents();
                var dtMachineProductions = new DataTable();
                var dtMachinePlanning = new DataTable();
                string mStartDate = txtFromDate.Text;
                string mEndDate = txtToDate.Text;
                string Conditions = Constants.vbNullString;
                if (!mSubbatchCode.Equals("-1"))
                {
                    Conditions = "A.SubbatchCode = '" + mSubbatchCode + "' ";
                }

                if (!mMachnineCode.Equals("-1"))
                {
                    Conditions = Conversions.ToString(Conditions + Interaction.IIf(string.IsNullOrEmpty(Conditions), "A.MachineCode = '" + mMachnineCode + "' ", " And A.MachineCode = '" + mMachnineCode + "' "));
                }
                else
                {
                    Conditions = Conversions.ToString(Conditions + Interaction.IIf(string.IsNullOrEmpty(Conditions), "A.MachineCode <> '-1'", " And A.MachineCode <> '-1'"));
                }

                if (!mPartCode.Equals("-1"))
                {
                    Conditions = Conversions.ToString(Conditions + Interaction.IIf(string.IsNullOrEmpty(Conditions), "B.NatureCode = " + mPartCode, " And B.NatureCode = " + mPartCode));
                }

                using (var cn = new SqlConnection(Module1.PlanningCnnStr))
                {
                    cn.Open();
                    var daMachineProductions = new SqlDataAdapter("", cn);
                    daMachineProductions.SelectCommand.CommandText = "Select Distinct A.SubbatchCode,A.MachineCode,B.DetailCode,C.DetailName,B.OperationCode,B.OperationTitle,B.NatureCode As PartCode,D.NatureTitle As PartName," + "       A.PlanningStartDate,dbo.GetRegularTimeFromFloatingTime(A.PlanningStartHour) As PlanningStartHour," + "       A.PlanningEndDate,dbo.GetRegularTimeFromFloatingTime(A.PlanningEndHour) As PlanningEndHour,A.DetailProductionQuantity,A.TreeCode,A.SetupDuration " + "From   Tbl_Planning A Inner Join Tbl_ProductOPCs B On A.TreeCode = B.TreeCode And A.OperationCode = B.OperationCode Inner Join " + "       Tbl_ProductTreeDetails C On B.TreeCode = C.TreeCode And B.DetailCode = C.DetailCode Inner Join Tbl_Natures D On B.NatureCode = D.NatureCode " + "Where  " + Conditions + " And PlanningStartDate >= '" + mStartDate + "' And PlanningStartDate <= '" + mEndDate + "' " + "Union " + "Select Distinct A.SubbatchCode,A.MachineCode,B.DetailCode,C.DetailName,B.OperationCode,B.OperationTitle,B.NatureCode As PartCode,D.NatureTitle As PartName," + "       A.PlanningStartDate,dbo.GetRegularTimeFromFloatingTime(A.PlanningStartHour) As PlanningStartHour," + "       A.PlanningEndDate,dbo.GetRegularTimeFromFloatingTime(A.PlanningEndHour) As PlanningEndHour,A.DetailProductionQuantity,A.TreeCode,A.SetupDuration " + "From   Tbl_Planning A Inner Join Tbl_ProductOPCs B On A.TreeCode = B.TreeCode And A.OperationCode = B.OperationCode Inner Join " + "       Tbl_ProductTreeDetails C On B.TreeCode = C.TreeCode And B.DetailCode = C.DetailCode Inner Join Tbl_Natures D On B.NatureCode = D.NatureCode " + "Where  " + Conditions + " And PlanningEndDate >= '" + mStartDate + "' And PlanningEndDate <= '" + mEndDate + "' " + "Union " + "Select Distinct A.SubbatchCode,A.MachineCode,B.DetailCode,C.DetailName,B.OperationCode,B.OperationTitle,B.NatureCode As PartCode,D.NatureTitle As PartName," + "       A.PlanningStartDate,dbo.GetRegularTimeFromFloatingTime(A.PlanningStartHour) As PlanningStartHour," + "       A.PlanningEndDate,dbo.GetRegularTimeFromFloatingTime(A.PlanningEndHour) As PlanningEndHour,A.DetailProductionQuantity,A.TreeCode,A.SetupDuration " + "From   Tbl_Planning A Inner Join Tbl_ProductOPCs B On A.TreeCode = B.TreeCode And A.OperationCode = B.OperationCode Inner Join " + "       Tbl_ProductTreeDetails C On B.TreeCode = C.TreeCode And B.DetailCode = C.DetailCode Inner Join Tbl_Natures D On B.NatureCode = D.NatureCode " + "Where  " + Conditions + " And PlanningStartDate < '" + mStartDate + "' And PlanningEndDate > '" + mEndDate + "'";


















                    daMachineProductions.Fill(dtMachinePlanning);
                    cn.Close();
                }

                dtMachineProductionProgram.Columns.Add("SubbatchCode", Type.GetType("System.String"));
                dtMachineProductionProgram.Columns.Add("MachineCode", Type.GetType("System.String"));
                dtMachineProductionProgram.Columns.Add("DetailCode", Type.GetType("System.String"));
                dtMachineProductionProgram.Columns.Add("DetailName", Type.GetType("System.String"));
                dtMachineProductionProgram.Columns.Add("OperationCode", Type.GetType("System.String"));
                dtMachineProductionProgram.Columns.Add("OperationTitle", Type.GetType("System.String"));
                dtMachineProductionProgram.Columns.Add("PlanningStartDate", Type.GetType("System.String"));
                dtMachineProductionProgram.Columns.Add("PlanningStartHour", Type.GetType("System.String"));
                dtMachineProductionProgram.Columns.Add("PlanningEndDate", Type.GetType("System.String"));
                dtMachineProductionProgram.Columns.Add("PlanningEndHour", Type.GetType("System.String"));
                dtMachineProductionProgram.Columns.Add("Quantity", Type.GetType("System.String"));
                dtMachineProductionProgram.Columns.Add("PartCode", Type.GetType("System.String"));
                dtMachineProductionProgram.Columns.Add("PartName", Type.GetType("System.String"));
                dtMachinePlanning.DefaultView.Sort = "PlanningStartDate,PlanningStartHour,PlanningEndDate,PlanningEndHour";
                for (int I = 0, loopTo = dtMachinePlanning.DefaultView.Count - 1; I <= loopTo; I++)
                {
                    while (Module1.IsHoliday(GetMachineCalendarCode(Conversions.ToString(dtMachinePlanning.DefaultView[I]["MachineCode"])), mStartDate))
                    {
                        mStartDate = FarsiDateFunctions.AddToDate(mStartDate, "00000001");
                        if (Operators.CompareString(mStartDate, mEndDate, false) > 0)
                        {
                            lblCalcWaiting.Visible = false;
                            Cursor = Cursors.Default;
                            Application.DoEvents();
                            MessageBox.Show("محدوده تاريخ وارد شده صحيح نيست", "برنامه ريزي توليد ", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                            return false;
                            //break;
                        }
                    }

                    while (Module1.IsHoliday(GetMachineCalendarCode(Conversions.ToString(dtMachinePlanning.DefaultView[I]["MachineCode"])), mEndDate))
                    {
                        mEndDate = FarsiDateFunctions.SubDayFromDate(mEndDate, "00000001");
                        if (Operators.CompareString(mStartDate, mEndDate, false) > 0)
                        {
                            lblCalcWaiting.Visible = false;
                            Cursor = Cursors.Default;
                            Application.DoEvents();
                            MessageBox.Show("محدوده تاريخ وارد شده صحيح نيست", "برنامه ريزي توليد ", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                            return false;
                            //break;
                        }
                    }

                    CheckPlanningScope(dtMachinePlanning.DefaultView[I], mStartDate, mEndDate);
                }

                {
                    var withBlock = dtMachineProductionProgram;
                    for (int I = 0, loopTo1 = withBlock.Rows.Count - 1; I <= loopTo1; I++)
                        dgMachineWorkTimes.Rows.Add(withBlock.Rows[I]["SubbatchCode"], withBlock.Rows[I]["MachineCode"], Operators.ConcatenateObject(Operators.ConcatenateObject(withBlock.Rows[I]["PartCode"], " <> "), withBlock.Rows[I]["PartName"]), Operators.ConcatenateObject(Operators.ConcatenateObject(withBlock.Rows[I]["DetailCode"], " <> "), withBlock.Rows[I]["DetailName"]), Operators.ConcatenateObject(Operators.ConcatenateObject(withBlock.Rows[I]["OperationCode"], " <> "), withBlock.Rows[I]["OperationTitle"]), Operators.ConcatenateObject(Module1.GetFormatedDate(withBlock.Rows[I]["PlanningStartDate"].ToString()) + " <> ", withBlock.Rows[I]["PlanningStartHour"]), Operators.ConcatenateObject(Module1.GetFormatedDate(withBlock.Rows[I]["PlanningEndDate"].ToString()) + " <> ", withBlock.Rows[I]["PlanningEndHour"]), withBlock.Rows[I]["Quantity"]);





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
                Logger.SaveError(Name + ".CalcMachineProductionProgram", ex.Message);
                MessageBox.Show("محاسبه با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                dgMachineWorkTimes.Rows.Clear();
                return false;
            }
        }

        private void CheckPlanningScope(DataRowView drView, string mStartDate, string mEndDate)
        {
            string mCalendarCode = GetMachineCalendarCode(Conversions.ToString(drView["MachineCode"]));
            string mCalendarStart = Module1.GetCalendarStart(mCalendarCode);
            string mCalendarEnd = Module1.GetCalendarEnd(mCalendarCode);
            double DaysDuration, OPOneExecTime;
            int Quantity;
            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(mStartDate, drView["PlanningStartDate"], false)))
            {
                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreaterEqual(mEndDate, drView["PlanningEndDate"], false)))
                {
                    AddNewRow(dtMachineProductionProgram.NewRow(), drView, mStartDate, Conversions.ToString(drView["PlanningStartHour"]), Conversions.ToString(drView["PlanningEndDate"]), Conversions.ToString(drView["PlanningEndHour"]), Conversions.ToInteger(drView["DetailProductionQuantity"]));

                }
                else if (Conversions.ToBoolean(Operators.ConditionalCompareObjectLess(mEndDate, drView["PlanningEndDate"], false)))
                {
                    DaysDuration = GetDaysDurationTime(mStartDate, Conversions.ToString(drView["PlanningStartHour"]), mEndDate, mCalendarEnd, Conversions.ToInteger(mCalendarCode));
                    DaysDuration -= Conversions.ToDouble(drView["SetupDuration"]);
                    OPOneExecTime = GetOperationOneExecTime(Conversions.ToString(drView["MachineCode"]), Conversions.ToString(drView["TreeCode"]), Conversions.ToString(drView["OperationCode"]));
                    Quantity = (int)Math.Round(DaysDuration / OPOneExecTime);
                    string mSD = mStartDate;
                    string mED = mEndDate;

                    // While IsHoliday(mCalendarCode, mSD)
                    // mSD = FarsiDateFunctions.AddToDate(mSD, "00000001")

                    // If mSD > mED Then
                    // mSD = mStartDate
                    // Exit While
                    // End If
                    // End While

                    // While IsHoliday(mCalendarCode, mED)
                    // mED = FarsiDateFunctions.SubDayFromDate(mED, "00000001")

                    // If mSD > mED Then
                    // mED = mEndDate
                    // Exit While
                    // End If
                    // End While

                    AddNewRow(dtMachineProductionProgram.NewRow(), drView, mSD, Conversions.ToString(drView["PlanningStartHour"]), mED, mCalendarEnd, Quantity);
                }
            }
            else if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(mStartDate, drView["PlanningStartDate"], false)))
            {
                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreaterEqual(mEndDate, drView["PlanningEndDate"], false)))
                {
                    DaysDuration = GetDaysDurationTime(mStartDate, mCalendarStart, Conversions.ToString(drView["PlanningEndDate"]), Conversions.ToString(drView["PlanningEndHour"]), Conversions.ToInteger(mCalendarCode));
                    OPOneExecTime = GetOperationOneExecTime(Conversions.ToString(drView["MachineCode"]), Conversions.ToString(drView["TreeCode"]), Conversions.ToString(drView["OperationCode"]));
                    Quantity = (int)Math.Round(DaysDuration / OPOneExecTime);
                    string mSD = mStartDate;

                    // While IsHoliday(mCalendarCode, mSD)
                    // mSD = FarsiDateFunctions.AddToDate(mSD, "00000001")

                    // If mSD > mEndDate Then
                    // mSD = mStartDate
                    // Exit While
                    // End If
                    // End While

                    AddNewRow(dtMachineProductionProgram.NewRow(), drView, mSD, mCalendarStart, Conversions.ToString(drView["PlanningEndDate"]), Conversions.ToString(drView["PlanningEndHour"]), Quantity);
                }
                else if (Conversions.ToBoolean(Operators.ConditionalCompareObjectLess(mEndDate, drView["PlanningEndDate"], false)))
                {
                    DaysDuration = GetDaysDurationTime(mStartDate, mCalendarStart, mEndDate, mCalendarEnd, Conversions.ToInteger(mCalendarCode));
                    OPOneExecTime = GetOperationOneExecTime(Conversions.ToString(drView["MachineCode"]), Conversions.ToString(drView["TreeCode"]), Conversions.ToString(drView["OperationCode"]));
                    Quantity = (int)Math.Round(DaysDuration / OPOneExecTime);
                    string mSD = mStartDate;
                    string mED = mEndDate;

                    // While IsHoliday(mCalendarCode, mSD)
                    // mSD = FarsiDateFunctions.AddToDate(mSD, "00000001")

                    // If mSD > mED Then
                    // mSD = mStartDate
                    // Exit While
                    // End If
                    // End While

                    // While IsHoliday(mCalendarCode, mED)
                    // mED = FarsiDateFunctions.SubDayFromDate(mED, "00000001")

                    // If mSD > mED Then
                    // mED = mEndDate
                    // Exit While
                    // End If
                    // End While

                    AddNewRow(dtMachineProductionProgram.NewRow(), drView, mSD, mCalendarStart, mED, mCalendarEnd, Quantity);
                }
            }
            else if (Conversions.ToBoolean(Operators.ConditionalCompareObjectLess(mStartDate, drView["PlanningStartDate"], false)))
            {
                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreaterEqual(mEndDate, drView["PlanningEndDate"], false)))
                {
                    AddNewRow(dtMachineProductionProgram.NewRow(), drView, Conversions.ToString(drView["PlanningStartDate"]), Conversions.ToString(drView["PlanningStartHour"]), Conversions.ToString(drView["PlanningEndDate"]), Conversions.ToString(drView["PlanningEndHour"]), Conversions.ToInteger(drView["DetailProductionQuantity"]));

                }
                else if (Conversions.ToBoolean(Operators.ConditionalCompareObjectLess(mEndDate, drView["PlanningEndDate"], false)))
                {
                    DaysDuration = GetDaysDurationTime(Conversions.ToString(drView["PlanningStartDate"]), Conversions.ToString(drView["PlanningStartHour"]), mEndDate, mCalendarEnd, Conversions.ToInteger(mCalendarCode));
                    DaysDuration -= Conversions.ToDouble(drView["SetupDuration"]);
                    OPOneExecTime = GetOperationOneExecTime(Conversions.ToString(drView["MachineCode"]), Conversions.ToString(drView["TreeCode"]), Conversions.ToString(drView["OperationCode"]));
                    Quantity = (int)Math.Round(DaysDuration / OPOneExecTime);
                    string mED = mEndDate;

                    // While IsHoliday(mCalendarCode, mED)
                    // mED = FarsiDateFunctions.SubDayFromDate(mED, "00000001")

                    // If mStartDate > mED Then
                    // mED = mEndDate
                    // Exit While
                    // End If
                    // End While

                    AddNewRow(dtMachineProductionProgram.NewRow(), drView, Conversions.ToString(drView["PlanningStartDate"]), Conversions.ToString(drView["PlanningStartHour"]), mED, mCalendarEnd, Quantity);
                }
            }
        }

        private double GetOperationOneExecTime(string MachineCode, string TreeCode, string OperationCode)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    var cm = new SqlCommand("Select IsNull(dbo.GetOperationExecutionTime('" + MachineCode + "'," + TreeCode + ",'" + OperationCode + "'),0)", cn);
                    return Conversions.ToDouble(cm.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }
        }

        private void AddNewRow(DataRow drInsert, DataRowView drView, string mStartDate, string mStartHour, string mEndDate, string mEndHour, int mQuantity)
        {
            drInsert["SubbatchCode"] = drView["SubbatchCode"];
            drInsert["MachineCode"] = drView["MachineCode"];
            drInsert["DetailCode"] = drView["DetailCode"];
            drInsert["DetailName"] = drView["DetailName"];
            drInsert["OperationCode"] = drView["OperationCode"];
            drInsert["OperationTitle"] = drView["OperationTitle"];
            drInsert["PlanningStartDate"] = mStartDate;
            drInsert["PlanningStartHour"] = mStartHour;
            drInsert["PlanningEndDate"] = mEndDate;
            drInsert["PlanningEndHour"] = mEndHour;
            drInsert["Quantity"] = mQuantity.ToString();
            drInsert["PartCode"] = drView["PartCode"];
            drInsert["PartName"] = drView["PartName"];
            dtMachineProductionProgram.Rows.Add(drInsert);
        }

        private double GetDaysDurationTime(string StartDate, string StartHour, string EndDate, string EndHour, int CalendarCode)
        {
            double DayTime = 0d;
            string CalendarStart = Module1.GetCalendarStart(CalendarCode.ToString());
            string CalendarEnd = Module1.GetCalendarEnd(CalendarCode.ToString());
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
                    if (TimeSpan.Parse(CalendarStart) > TimeSpan.Parse(StartHour))
                    {
                        StartHour = CalendarStart;
                    }
                    else if (TimeSpan.Parse(CalendarStart) < TimeSpan.Parse(StartHour))
                    {
                        DayTime -= Conversions.ToDouble(Module1.GetFloatingHour(StartHour)) - Conversions.ToDouble(Module1.GetFloatingHour(CalendarStart));
                        StartHour = CalendarStart;
                    }
                }

                StartDate = FarsiDateFunctions.AddToDate(StartDate, "00000001");
                if (Operators.CompareString(StartDate, EndDate, false) > 0)
                {
                    break;
                }
            }

            if (TimeSpan.Parse(CalendarEnd) > TimeSpan.Parse(EndHour))
            {
                EndHour = (TimeSpan.Parse(CalendarEnd) - TimeSpan.Parse(EndHour)).ToString();
                DayTime -= Conversions.ToDouble(Module1.GetFloatingHour(EndHour.Substring(0, 5)));
            }

            return DayTime;
        }

        private bool FormValidation()
        {
            if (!chkAllMachines.Checked && cbMachineCode.SeletedValue is null)
            {
                MessageBox.Show("کد ماشین را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbMachineCode.Focus();
                return false;
            }

            if (!chkAllParts.Checked && cbPartCode.SeletedValue is null)
            {
                MessageBox.Show("کد بخش تولید را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbPartCode.Focus();
                return false;
            }

            if (!chkAllSubbatchs.Checked && cbSubbatchCode.SelectedValue is null)
            {
                MessageBox.Show("کد ساب بچ را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbSubbatchCode.Focus();
                return false;
            }

            if (txtFromDate.Text is null || string.IsNullOrEmpty(txtFromDate.Text) || txtFromDate.Text.Equals("") || txtFromDate.Text.Trim().Length < 8)
            {
                MessageBox.Show("(محدودۀ بازۀ زمانی صحیح نیست(از تاریخ", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtFromDate.Focus();
                return false;
            }

            if (txtToDate.Text is null || string.IsNullOrEmpty(txtToDate.Text) || txtToDate.Text.Equals("") || txtToDate.Text.Trim().Length < 8)
            {
                MessageBox.Show("(محدودۀ بازۀ زمانی صحیح نیست(تا تاریخ", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtToDate.Focus();
                return false;
            }

            return true;
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
                    var cmdClosePreview = new System.Windows.Forms.Button();
                    cmdClosePreview.Text = "بستن";
                    cmdClosePreview.TextAlign = ContentAlignment.MiddleCenter;
                    cmdClosePreview.Font = new Font("Tahoma", 10f, FontStyle.Regular);
                    cmdClosePreview.Size = new Size(100, 25);
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
                    TabControl1.SelectTab(1);
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
            string RptName = Constants.vbNullString;
            if (rbReportMachine.Checked)
            {
                RptName = "Rep102_MachineProductionProgram_MachineGroup.rpt";
            }
            else if (rbReportPart.Checked)
            {
                RptName = "Rep102_MachineProductionProgram_PartGroup.rpt";
            }

            CnnInfo.ServerName = CnnBuilder.DataSource;
            CnnInfo.DatabaseName = "PSB_ProductionPlanning";
            CnnInfo.IntegratedSecurity = CnnBuilder.IntegratedSecurity;
            if (!CnnBuilder.IntegratedSecurity)
            {
                CnnInfo.UserID = CnnBuilder.UserID;
                CnnInfo.Password = CnnBuilder.Password;
            }

            string mrptPath = Module1.GetReportFolderPath();
            if (File.Exists(mrptPath + RptName))
            {
                RptDoc.Load(mrptPath + RptName, OpenReportMethod.OpenReportByDefault);
                RptDoc.ParameterFields["p_PrintDate"].CurrentValues.AddValue(Module1.mServerShamsiDate);
                RptDoc.ParameterFields["p_FromDate"].CurrentValues.AddValue(txtFromDate.Text);
                RptDoc.ParameterFields["p_ToDate"].CurrentValues.AddValue(txtToDate.Text);
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
                var cmInsertOvers = new SqlCommand("if Not Exists(select * from dbo.sysobjects where id = object_id(N'[dbo].[Tbl_TempMachineProductionProgram]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)" + "Begin " + "  Create Table [dbo].[Tbl_TempMachineProductionProgram](" + "                     [RowIndex] [int] NOT NULL ," + "                     [SubbatchCode] [varchar] (50) COLLATE Arabic_CS_AS_KS_WS NOT NULL ," + "                     [MachineCode] [varchar] (50) COLLATE Arabic_CS_AS_KS_WS NOT NULL ," + "                     [DetailCode] [varchar] (50) COLLATE Arabic_CS_AS_KS_WS NOT NULL ," + "                     [DetailName] [varchar] (100) COLLATE Arabic_CS_AS_KS_WS NOT NULL ," + "                     [OperationCode] [varchar] (50) COLLATE Arabic_CS_AS_KS_WS NOT NULL ," + "                     [OperationTitle] [varchar] (100) COLLATE Arabic_CS_AS_KS_WS NOT NULL ," + "                     [PlanningStartDate] [varchar] (8) COLLATE Arabic_CS_AS_KS_WS NOT NULL ," + "                     [PlanningStartHour] [varchar] (50) COLLATE Arabic_CS_AS_KS_WS NOT NULL ," + "                     [PlanningEndDate] [varchar] (8) COLLATE Arabic_CS_AS_KS_WS NOT NULL ," + "                     [PlanningEndHour] [varchar] (50) COLLATE Arabic_CS_AS_KS_WS NOT NULL ," + "                     [Quantity] [int] NOT NULL ," + "                     [PartCode] [int] NULL ," + "                     [PartName] [varchar] (100) COLLATE Arabic_CS_AS_KS_WS NULL)" + "End", cn);
















                try
                {
                    cmInsertOvers.Transaction = trnInsert;
                    cmInsertOvers.ExecuteNonQuery();
                    cmInsertOvers.CommandText = "Delete From Tbl_TempMachineProductionProgram";
                    cmInsertOvers.ExecuteNonQuery();
                    cmInsertOvers.CommandText = "Insert Into Tbl_TempMachineProductionProgram(RowIndex,SubbatchCode,MachineCode,DetailCode,DetailName," + "OperationCode,OperationTitle,PlanningStartDate,PlanningStartHour," + "PlanningEndDate,PlanningEndHour,Quantity,PartCode,PartName)" + "Values(@RowIndex,@SubbatchCode,@MachineCode,@DetailCode,@DetailName," + "@OperationCode,@OperationTitle,@PlanningStartDate,@PlanningStartHour," + "@PlanningEndDate,@PlanningEndHour,@Quantity,@PartCode,@PartName)";




                    {
                        var withBlock = cmInsertOvers.Parameters;
                        withBlock.Add("@RowIndex", SqlDbType.Int, default, "RowIndex");
                        withBlock.Add("@SubbatchCode", SqlDbType.VarChar, 50, "SubbatchCode");
                        withBlock.Add("@MachineCode", SqlDbType.VarChar, 50, "MachineCode");
                        withBlock.Add("@DetailCode", SqlDbType.VarChar, 50, "DetailCode");
                        withBlock.Add("@DetailName", SqlDbType.VarChar, 100, "DetailName");
                        withBlock.Add("@OperationCode", SqlDbType.VarChar, 50, "OperationCode");
                        withBlock.Add("@OperationTitle", SqlDbType.VarChar, 100, "OperationTitle");
                        withBlock.Add("@PlanningStartDate", SqlDbType.VarChar, 8, "PlanningStartDate");
                        withBlock.Add("@PlanningStartHour", SqlDbType.VarChar, 50, "PlanningStartHour");
                        withBlock.Add("@PlanningEndDate", SqlDbType.VarChar, 8, "PlanningEndDate");
                        withBlock.Add("@PlanningEndHour", SqlDbType.VarChar, 50, "PlanningEndHour");
                        withBlock.Add("@Quantity", SqlDbType.Int, default, "Quantity");
                        withBlock.Add("@PartCode", SqlDbType.Int, default, "PartCode");
                        withBlock.Add("@PartName", SqlDbType.VarChar, 100, "PartName");
                    }

                    int RowIndex = 1;
                    for (int I = 0, loopTo = dtMachineProductionProgram.Rows.Count - 1; I <= loopTo; I++)
                    {
                        cmInsertOvers.Parameters["@RowIndex"].Value = RowIndex;
                        cmInsertOvers.Parameters["@SubbatchCode"].Value = dtMachineProductionProgram.Rows[I]["SubbatchCode"];
                        cmInsertOvers.Parameters["@MachineCode"].Value = dtMachineProductionProgram.Rows[I]["MachineCode"];
                        cmInsertOvers.Parameters["@DetailCode"].Value = dtMachineProductionProgram.Rows[I]["DetailCode"];
                        cmInsertOvers.Parameters["@DetailName"].Value = dtMachineProductionProgram.Rows[I]["DetailName"];
                        cmInsertOvers.Parameters["@OperationCode"].Value = dtMachineProductionProgram.Rows[I]["OperationCode"];
                        cmInsertOvers.Parameters["@OperationTitle"].Value = dtMachineProductionProgram.Rows[I]["OperationTitle"];
                        cmInsertOvers.Parameters["@PlanningStartDate"].Value = dtMachineProductionProgram.Rows[I]["PlanningStartDate"];
                        cmInsertOvers.Parameters["@PlanningStartHour"].Value = dtMachineProductionProgram.Rows[I]["PlanningStartHour"];
                        cmInsertOvers.Parameters["@PlanningEndDate"].Value = dtMachineProductionProgram.Rows[I]["PlanningEndDate"];
                        cmInsertOvers.Parameters["@PlanningEndHour"].Value = dtMachineProductionProgram.Rows[I]["PlanningEndHour"];
                        cmInsertOvers.Parameters["@Quantity"].Value = dtMachineProductionProgram.Rows[I]["Quantity"];
                        cmInsertOvers.Parameters["@PartCode"].Value = dtMachineProductionProgram.Rows[I]["PartCode"];
                        cmInsertOvers.Parameters["@PartName"].Value = dtMachineProductionProgram.Rows[I]["PartName"];
                        cmInsertOvers.ExecuteNonQuery();
                        RowIndex += 1;
                    }

                    trnInsert.Commit();
                }
                catch (Exception objEx)
                {
                    trnInsert.Rollback();
                    Logger.SaveError(Name + ".InsertDataBaseTableRows", objEx.Message);
                    MessageBox.Show("چاپ لیست عملیات تولیدی ماشین در بازۀ زمانی، با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
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