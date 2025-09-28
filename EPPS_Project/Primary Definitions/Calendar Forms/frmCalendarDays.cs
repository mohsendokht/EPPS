using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmCalendarDays
    {
        public frmCalendarDays()
        {
            InitializeComponent();
            _chkSetWorkTime.Name = "chkSetWorkTime";
            _chkSetDownTime.Name = "chkSetDownTime";
            _cmdShowShiftTimeLine.Name = "cmdShowShiftTimeLine";
            _cmdSaveShift.Name = "cmdSaveShift";
            _txtShiftExtraTime.Name = "txtShiftExtraTime";
            _txtShiftDuration.Name = "txtShiftDuration";
            _txtShiftStart.Name = "txtShiftStart";
            _dgShiftTimeLine.Name = "dgShiftTimeLine";
            _rbActive.Name = "rbActive";
            _rbDefualt.Name = "rbDefualt";
            _dgShifts.Name = "dgShifts";
            _cmdSave.Name = "cmdSave";
            _cmdExit.Name = "cmdExit";
            _cmdEdit.Name = "cmdEdit";
            _cmdCalendarShow.Name = "cmdCalendarShow";
            _dgCalenderDays.Name = "dgCalenderDays";
        }

        private DataGridViewCellStyle HolidayCellStyle = new DataGridViewCellStyle();
        public DataSet dsProductionPlanning;
        private SqlDataAdapter daCalendarParticularShifts = new SqlDataAdapter();
        private SqlDataAdapter daCalendarParticularDays = new SqlDataAdapter();
        private SqlDataAdapter daCalendarDays = new SqlDataAdapter();
        private SqlDataAdapter mdaParticulareShiftDownTimes = new SqlDataAdapter();
        private SqlDataAdapter mdaDaysDownTimes = new SqlDataAdapter();
        private string mCalendarCode;
        private int CurrentRow;
        private int CurrentColumn;
        private const string mParticularDayDelimiter = "   ";
        private short I, J;

        public string CalendarCode
        {
            get
            {
                return mCalendarCode;
            }

            set
            {
                mCalendarCode = value;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdSave, 6);
            Module1.SetButtonsImage(cmdExit, 5);
            Module1.SetButtonsImage(cmdEdit, 17);
            FormLoad();
        }

        private void frmCalenderDays_FormClosing(object sender, FormClosingEventArgs e)
        {
            dsProductionPlanning.RejectChanges();
            HolidayCellStyle = null;
            dsProductionPlanning = null;
            daCalendarParticularShifts.Dispose();
            daCalendarParticularDays.Dispose();
            daCalendarDays.Dispose();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            if (cmdExit.Text == "بازگشت")
            {
                Close();
            }
            else
            {
                dsProductionPlanning.RejectChanges();
                tcDays.Enabled = true;
                Panel2.Enabled = false;
                cmdSave.Enabled = false;
                dgCalenderDays_CellMouseClick(sender, new DataGridViewCellMouseEventArgs(CurrentColumn, CurrentRow, 1, 1, new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0)));
                cmdExit.Text = "بازگشت";
                cmdExit.DialogResult = DialogResult.Cancel;
            }
        }

        private void dgCalenderDays_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            CurrentRow = e.RowIndex;
            CurrentColumn = e.ColumnIndex;
            bool mIsParticulare = true;
            string mDayAccessibleTime = "-1";
            DataView dvDays = null;
            short DayType = -1;
            for (int I = 0, loopTo = dgCalenderDays.ColumnCount - 1; I <= loopTo; I++)
            {
                if (dgCalenderDays.Rows[CurrentRow].Cells[I].Value is object && !dgCalenderDays.Rows[CurrentRow].Cells[I].Value.ToString().Equals(""))
                {
                    if (!dgCalenderDays.Rows[CurrentRow].Cells[I].Value.ToString().Contains("*"))
                    {
                        mIsParticulare = false;
                        break;
                    }
                    else
                    {
                        string ShamsiDate = GetDayShamsiDate(dgCalenderDays.Rows[CurrentRow].Cells[I].Value.ToString().Split(Conversions.ToChar(mParticularDayDelimiter))[0].Trim(), dgCalenderDays.Tag.ToString());
                        var mDay = dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].Select("ShamsiDate = " + ShamsiDate);
                        if (mDay.Length == 0)
                        {
                            mIsParticulare = false;
                            break;
                        }
                        else if (!mDayAccessibleTime.Equals("-1"))
                        {
                            if (!mDayAccessibleTime.Equals(mDay[0]["AccessibleWorkTime"].ToString()))
                            {
                                mIsParticulare = false;
                                break;
                            }
                        }
                        else
                        {
                            mDayAccessibleTime = mDay[0]["AccessibleWorkTime"].ToString();
                        }
                    }
                }
            }

            txtShiftStart.Text = "00:00";
            txtShiftDuration.Text = "00:00";
            txtShiftExtraTime.Text = "00:00";
            dgShiftTimeLine.Columns.Clear();
            dgShifts.Rows.Clear();
            if (!mIsParticulare) // روز هفتۀ پیش فرض می باشد
            {
                dsProductionPlanning.Tables["Tbl_CalendarDays"].DefaultView.RowFilter = "DayNo=" + (CurrentRow + 1).ToString();
                dvDays = dsProductionPlanning.Tables["Tbl_CalendarDays"].DefaultView;
                DayType = Conversions.ToShort(dvDays[0]["DayType"]);
                dgShifts.Tag = "2";
                var loopTo1 = (short)(dvDays.Count - 1);
                for (I = 0; I <= loopTo1; I++)
                    dgShifts.Rows.Add(dvDays[I]["ShiftNo"], "نمایش مشخصات شیفت " + dvDays[I]["ShiftNo"].ToString());
                if (DayType == 2)
                {
                    rbActive.Checked = true;
                }
                else
                {
                    rbHoliday.Checked = true;
                }

                dgShifts.ReadOnly = false;
                rbDefualt.Checked = true;
            }
            else // روز هفتۀ خاص می باشد
            {
                string ShamsiDate = Constants.vbNullString;
                for (int I = 0, loopTo2 = dgCalenderDays.ColumnCount - 1; I <= loopTo2; I++)
                {
                    if (dgCalenderDays.Rows[CurrentRow].Cells[I].Value is object && !dgCalenderDays.Rows[CurrentRow].Cells[I].Value.ToString().Equals(""))
                    {
                        ShamsiDate = GetDayShamsiDate(dgCalenderDays.Rows[CurrentRow].Cells[I].Value.ToString().Split(Conversions.ToChar(mParticularDayDelimiter))[0].Trim(), dgCalenderDays.Tag.ToString());
                        break;
                    }
                }

                DayType = Conversions.ToShort(dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].Select("ShamsiDate = " + ShamsiDate)[0]["DayType"]);
                dvDays = dsProductionPlanning.Tables["Tbl_CalendarParticularShifts"].DefaultView;
                dvDays.RowFilter = "ShamsiDate = " + ShamsiDate;
                dgShifts.Tag = "4";
                var loopTo3 = (short)(dvDays.Count - 1);
                for (I = 0; I <= loopTo3; I++)
                    dgShifts.Rows.Add(dvDays[I]["ShiftNo"], "نمایش مشخصات شیفت " + dvDays[I]["ShiftNo"].ToString());
                if (DayType == 2)
                {
                    rbActive.Checked = true;
                }
                else
                {
                    rbHoliday.Checked = true;
                }

                dgShifts.ReadOnly = true;
                rbParticular.Checked = true;
                dvDays.RowFilter = "";
            }

            rbParticular.Enabled = true;
            pnlDayType.Enabled = true;
            cmdEdit.Enabled = true;
        }

        private void cmdCalendarShow_Click(object sender, EventArgs e)
        {
            // پاک کردن جدول روزهای ماه
            ReArangeCalender();
            // تنظیم جدول روزهای ماه با مقادیر ماه انتخاب شده
            SetNewMonthDays();
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            Panel2.Enabled = true;
            cmdSave.Enabled = true;
            tcDays.Enabled = false;
            cmdExit.Text = "انصراف";
            cmdExit.DialogResult = DialogResult.None;
            if (dgShifts.Tag.ToString().Equals("2") || rbDefualt.Checked)
            {
                SetShiftItemEnabled(false);
                if (dgShifts.Tag.ToString().Equals("2"))
                {
                    pnlDayType.Enabled = true;
                }
                else
                {
                    pnlDayType.Enabled = false;
                }
            }
            else
            {
                pnlDayType.Enabled = true;
                if (rbHoliday.Checked)
                {
                    SetShiftItemEnabled(false);
                }
                else
                {
                    SetShiftItemEnabled(true);
                    cmdSave.Enabled = false;
                    cmdSaveShift.Enabled = false;
                }
            }
        }

        private void cmdShowShiftTimeLine_Click(object sender, EventArgs e)
        {
            if (txtShiftStart.Text.Equals("00:00") && txtShiftDuration.Text.Equals("00:00") && txtShiftExtraTime.Text.Equals("00:00") || !txtShiftStart.Text.Equals("00:00") && txtShiftDuration.Text.Equals("00:00") && txtShiftExtraTime.Text.Equals("00:00"))
            {
                MessageBox.Show("مقادیر زمانی شیفت را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtShiftStart.Focus();
                cmdShowShiftTimeLine.Enabled = true;
                cmdSaveShift.Enabled = false;
                return;
            }

            if (dgShifts.SelectedRows.Count > 0)
            {
                TimeSpan mShiftDuration = default;
                if (TimeSpan.Parse(txtShiftDuration.Text) + TimeSpan.Parse(txtShiftExtraTime.Text) > TimeSpan.Parse("1.00:00:00"))
                {
                    MessageBox.Show("زمانهای وارد شده برای شیفت اشتباه است", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return;
                }
                else if (TimeSpan.Parse(txtShiftDuration.Text) + TimeSpan.Parse(txtShiftExtraTime.Text) == TimeSpan.Parse("1.00:00:00"))
                {
                    mShiftDuration = TimeSpan.Parse("1.00:00:00");
                }
                else
                {
                    mShiftDuration = TimeSpan.Parse(txtShiftDuration.Text) + TimeSpan.Parse(txtShiftExtraTime.Text);
                }

                int mColCounter = 0;
                dgShiftTimeLine.Columns.Clear();
                if (SetShiftGridColumns(txtShiftStart.Text, mShiftDuration, ref mColCounter))
                {
                    dgShiftTimeLine.Rows.Add();
                    for (int I = 0, loopTo = dgShiftTimeLine.Rows[0].Cells.Count - 1; I <= loopTo; I++)
                        dgShiftTimeLine.Rows[0].Cells[I].Style.BackColor = Color.White;
                }

                cmdShowShiftTimeLine.Enabled = false;
                cmdSaveShift.Enabled = true;
            }
        }

        private void cmdSaveShift_Click(object sender, EventArgs e)
        {
            string mShiftNo = dgShifts.SelectedRows[0].Cells[0].Value.ToString();
            DataRow[] mDR;
            if (dgShifts.Tag.ToString().Equals("0") || dgShifts.Tag.ToString().Equals("1")) // شیفت پیش فرض
            {
                if (rbDefualt.Checked) // نوع روز تغییر نکرده است
                {
                    mDR = dsProductionPlanning.Tables["Tbl_CalendarShifts"].Select("ShiftNo = " + mShiftNo);
                    if (mDR.Length > 0)
                    {
                        EditShiftRow(mDR[0], txtShiftStart.Text, txtShiftDuration.Text, txtShiftExtraTime.Text);
                    }
                    else
                    {
                        MessageBox.Show("مشخصات شیفت(های) روز یافت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        return;
                    }

                    mDR = dsProductionPlanning.Tables["Tbl_CalendarShiftDownTimes"].Select("ShiftNo = " + mShiftNo);
                    for (int I = mDR.Length - 1; I >= 0; I -= 1)
                        mDR[I].Delete();
                    AddShiftDownTimes("Tbl_CalendarShiftDownTimes", mShiftNo);
                }
                else
                {
                    string ShamsiDate = Constants.vbNullString;
                    if (dgCalenderDays.Rows[CurrentRow].Cells[CurrentColumn].Value.ToString().Contains("*"))
                    {
                        ShamsiDate = GetDayShamsiDate(dgCalenderDays.Rows[CurrentRow].Cells[CurrentColumn].Value.ToString().Split(Conversions.ToChar(mParticularDayDelimiter))[0].Trim(), dgCalenderDays.Tag.ToString());
                    }
                    else
                    {
                        ShamsiDate = GetDayShamsiDate(dgCalenderDays.Rows[CurrentRow].Cells[CurrentColumn].Value.ToString(), dgCalenderDays.Tag.ToString());
                    }

                    mDR = new DataRow[1];
                    mDR[0] = dsProductionPlanning.Tables["Tbl_CalendarParticularShifts"].NewRow();
                    if (rbActive.Checked) // در صورتیکه روز کاری باشد
                    {
                        mDR[0]["CalendarCode"] = mCalendarCode;
                        mDR[0]["ShamsiDate"] = ShamsiDate;
                        mDR[0]["ShiftNo"] = mShiftNo;
                        mDR[0]["ShiftStart"] = txtShiftStart.Text;
                        mDR[0]["ShiftDuration"] = txtShiftDuration.Text;
                        mDR[0]["ShiftExtraTime"] = txtShiftExtraTime.Text;
                        dsProductionPlanning.Tables["Tbl_CalendarParticularShifts"].Rows.Add(mDR[0]);
                        mDR = new DataRow[1];
                        mDR[0] = dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].NewRow();
                        mDR[0]["CalendarCode"] = mCalendarCode;
                        mDR[0]["ShamsiDate"] = ShamsiDate;
                        mDR[0]["DayType"] = 2;
                        mDR[0]["AccessibleWorkTime"] = GetAccessibleTime();
                        mDR[0]["Description"] = txtDescription.Text;
                        dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].Rows.Add(mDR[0]);
                        AddShiftDownTimes("Tbl_ParticularShiftDownTimes", mShiftNo, "ShamsiDate", ShamsiDate);
                    }
                    else // در صورتیکه روز تعطیل باشد
                    {
                        mDR[0]["CalendarCode"] = mCalendarCode;
                        mDR[0]["ShamsiDate"] = ShamsiDate;
                        mDR[0]["ShiftNo"] = mShiftNo;
                        mDR[0]["ShiftStart"] = "00:00";
                        mDR[0]["ShiftDuration"] = "00:00";
                        mDR[0]["ShiftExtraTime"] = "00:00";
                        dsProductionPlanning.Tables["Tbl_CalendarParticularShifts"].Rows.Add(mDR[0]);
                        mDR = new DataRow[1];
                        mDR[0] = dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].NewRow();
                        mDR[0]["CalendarCode"] = mCalendarCode;
                        mDR[0]["ShamsiDate"] = ShamsiDate;
                        mDR[0]["DayType"] = 1;
                        mDR[0]["AccessibleWorkTime"] = "00:00";
                        mDR[0]["Description"] = txtDescription.Text;
                        dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].Rows.Add(mDR[0]);
                    }

                    dgShifts.Tag = "3";
                }
            }
            else if (dgShifts.Tag.ToString().Equals("3")) // شیفت های یک روز خاص
            {
                string ShamsiDate = GetDayShamsiDate(dgCalenderDays.Rows[CurrentRow].Cells[CurrentColumn].Value.ToString().Split(Conversions.ToChar(mParticularDayDelimiter))[0].Trim(), dgCalenderDays.Tag.ToString());
                if (rbParticular.Checked) // نوع روز تغییر نکرده است
                {
                    mDR = dsProductionPlanning.Tables["Tbl_CalendarParticularShifts"].Select("ShamsiDate=" + ShamsiDate + " And ShiftNo = " + mShiftNo);
                    if (mDR.Length > 0)
                    {
                        if (rbActive.Checked) // در صورتیکه روز کاری باشد
                        {
                            EditShiftRow(mDR[0], txtShiftStart.Text, txtShiftDuration.Text, txtShiftExtraTime.Text);
                            mDR = dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].Select("ShamsiDate=" + ShamsiDate);
                            mDR[0].BeginEdit();
                            mDR[0]["DayType"] = 2;
                            mDR[0]["AccessibleWorkTime"] = GetAccessibleTime();
                            mDR[0]["Description"] = txtDescription.Text;
                            mDR[0].EndEdit();
                        }
                        else // در صورتیکه روز تعطیل باشد
                        {
                            EditShiftRow(mDR[0], "00:00", "00:00", "00:00");
                            mDR = dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].Select("ShamsiDate=" + ShamsiDate);
                            mDR[0].BeginEdit();
                            mDR[0]["DayType"] = 1;
                            mDR[0]["AccessibleWorkTime"] = "00:00";
                            mDR[0]["Description"] = txtDescription.Text;
                            mDR[0].EndEdit();
                        }
                    }
                    else
                    {
                        MessageBox.Show("مشخصات زمانبندی روز یافت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        return;
                    }

                    mDR = dsProductionPlanning.Tables["Tbl_ParticularShiftDownTimes"].Select("ShamsiDate=" + ShamsiDate + " And ShiftNo = " + mShiftNo);
                    for (int I = mDR.Length - 1; I >= 0; I -= 1)
                        mDR[I].Delete();
                    if (rbActive.Checked)
                    {
                        AddShiftDownTimes("Tbl_ParticularShiftDownTimes", mShiftNo, "ShamsiDate", ShamsiDate);
                    }
                }

                SetShiftItemEnabled(false);
                cmdSave.Enabled = true;
            }
            else if (dgShifts.Tag.ToString().Equals("4"))
            {
                if (rbParticular.Checked) // نوع روز تغییر نکرده است
                {
                    Create_Or_Edit_ParticulareWeekDayShift(mShiftNo);
                }

                SetShiftItemEnabled(false);
                cmdSave.Enabled = true;
            }
        }

        private void chkSetWorkTime_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSetWorkTime.Checked)
            {
                chkSetDownTime.Checked = false;
            }
            else if (!chkSetDownTime.Checked)
            {
                chkSetWorkTime.Checked = true;
            }
        }

        private void chkSetDownTime_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSetDownTime.Checked)
            {
                chkSetWorkTime.Checked = false;
            }
            else if (!chkSetWorkTime.Checked)
            {
                chkSetDownTime.Checked = true;
            }
        }

        private void rbActive_CheckedChanged(object sender, EventArgs e)
        {
            if (!cmdEdit.Enabled)
            {
                if (rbActive.Checked)
                {
                    if (rbParticular.Checked)
                    {
                        SetShiftItemEnabled(rbActive.Checked);
                        cmdSave.Enabled = false;
                    }
                    else
                    {
                        SetShiftItemEnabled(false);
                        if (dgShifts.Tag.ToString().Equals("2") || dgShifts.Tag.ToString().Equals("4"))
                        {
                            cmdSave.Enabled = true;
                        }
                        else
                        {
                            cmdSave.Enabled = false;
                        }
                    }
                }
                else
                {
                    SetShiftItemEnabled(false);
                    cmdSave.Enabled = true;
                }
            }
        }

        private void txtShiftStart_ValueChanged(object sender, EventArgs e)
        {
            cmdShowShiftTimeLine.Enabled = true;
            cmdSaveShift.Enabled = false;
        }

        private void dgShiftTimeLine_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (chkSetWorkTime.Enabled && chkSetDownTime.Enabled)
            {
                var mColor = Color.White;
                if (chkSetWorkTime.Checked)
                {
                    mColor = Color.Green;
                }

                foreach (DataGridViewCell cell in dgShiftTimeLine.SelectedCells)
                {
                    cell.Style.BackColor = mColor;
                    cell.Selected = false;
                }
            }
        }

        private void dgShifts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                string mShiftNo = dgShifts.CurrentRow.Cells[0].Value.ToString();
                var mDR = dsProductionPlanning.Tables["Tbl_CalendarShifts"].Select("ShiftNo = " + mShiftNo);
                dgShiftTimeLine.Columns.Clear();
                if (dgShifts.Tag.ToString().Equals("2"))
                {
                    mDR = dsProductionPlanning.Tables["Tbl_CalendarDays"].Select("ShiftNo = " + mShiftNo + " And DayNo = " + (CurrentRow + 1).ToString());
                    if (mDR.Length > 0)
                    {
                        txtShiftStart.Text = mDR[0]["ShiftStart"].ToString();
                        txtShiftDuration.Text = mDR[0]["ShiftDuration"].ToString();
                        txtShiftExtraTime.Text = mDR[0]["ShiftExtraTime"].ToString();
                        txtDescription.Text = mDR[0]["Description"].ToString();
                        mDR = dsProductionPlanning.Tables["Tbl_CalendarDaysDownTimes"].Select("ShiftNo = " + mShiftNo + " And DayNo = " + (CurrentRow + 1).ToString());
                        if (mDR.Length > 0)
                        {
                            cmdShowShiftTimeLine_Click(sender, new EventArgs());
                            if (dgShiftTimeLine.Columns.Count > 0)
                            {
                                foreach (DataGridViewCell dgCell in dgShiftTimeLine.Rows[0].Cells)
                                    dgCell.Style.BackColor = Color.Green;
                                FillShiftDownTimes(mDR);
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        txtShiftStart.Text = "00:00";
                        txtShiftDuration.Text = "00:00";
                        txtShiftExtraTime.Text = "00:00";
                        txtDescription.Text = "";
                    }

                    SetShiftItemEnabled(false);
                    cmdSave.Enabled = true;
                }
                else if (dgShifts.Tag.ToString().Equals("0") || dgShifts.Tag.ToString().Equals("1")) // روز عادی
                {
                    if (mDR.Length > 0)
                    {
                        txtShiftStart.Text = mDR[0]["ShiftStart"].ToString();
                        txtShiftDuration.Text = mDR[0]["ShiftDuration"].ToString();
                        txtShiftExtraTime.Text = mDR[0]["ShiftExtraTime"].ToString();
                        mDR = dsProductionPlanning.Tables["Tbl_CalendarShiftDownTimes"].Select("ShiftNo = " + mShiftNo);
                        if (mDR.Length > 0)
                        {
                            cmdShowShiftTimeLine_Click(sender, new EventArgs());
                            if (dgShiftTimeLine.Columns.Count > 0)
                            {
                                foreach (DataGridViewCell dgCell in dgShiftTimeLine.Rows[0].Cells)
                                    dgCell.Style.BackColor = Color.Green;
                                FillShiftDownTimes(mDR);
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        txtShiftStart.Text = "00:00";
                        txtShiftDuration.Text = "00:00";
                        txtShiftExtraTime.Text = "00:00";
                    }

                    if (rbDefualt.Checked)
                    {
                        SetShiftItemEnabled(false);
                        cmdSave.Enabled = true;
                    }
                    else if (!rbHoliday.Checked)
                    {
                        SetShiftItemEnabled(true);
                        cmdShowShiftTimeLine.Enabled = false;
                        cmdSave.Enabled = true;
                    }
                    else
                    {
                        SetShiftItemEnabled(false);
                        cmdSaveShift.Enabled = true;
                        cmdSave.Enabled = false;
                    }
                }
                else if (dgShifts.Tag.ToString().Equals("3") || dgShifts.Tag.ToString().Equals("4")) // روز انتخابی خاص می باشد
                {
                    string ShamsiDate = Constants.vbNullString;
                    if (dgShifts.Tag.ToString().Equals("3"))
                    {
                        ShamsiDate = GetDayShamsiDate(dgCalenderDays.Rows[CurrentRow].Cells[CurrentColumn].Value.ToString().Split(Conversions.ToChar(mParticularDayDelimiter))[0].Trim(), dgCalenderDays.Tag.ToString());
                    }
                    else if (dgShifts.Tag.ToString().Equals("4"))
                    {
                        for (int I = 0, loopTo = dgCalenderDays.ColumnCount - 1; I <= loopTo; I++)
                        {
                            if (dgCalenderDays.Rows[CurrentRow].Cells[I].Value is object && !dgCalenderDays.Rows[CurrentRow].Cells[I].Value.ToString().Equals(""))
                            {
                                ShamsiDate = GetDayShamsiDate(dgCalenderDays.Rows[CurrentRow].Cells[I].Value.ToString().Split(Conversions.ToChar(mParticularDayDelimiter))[0].Trim(), dgCalenderDays.Tag.ToString());
                                break;
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(ShamsiDate))
                    {
                        return;
                    }

                    mDR = dsProductionPlanning.Tables["Tbl_CalendarParticularShifts"].Select("ShamsiDate = " + ShamsiDate + " And ShiftNo = " + mShiftNo);
                    if (mDR.Length > 0)
                    {
                        txtShiftStart.Text = mDR[0]["ShiftStart"].ToString();
                        txtShiftDuration.Text = mDR[0]["ShiftDuration"].ToString();
                        txtShiftExtraTime.Text = mDR[0]["ShiftExtraTime"].ToString();
                        cmdShowShiftTimeLine_Click(sender, new EventArgs());
                        if (dgShiftTimeLine.Columns.Count > 0)
                        {
                            foreach (DataGridViewCell dgCell in dgShiftTimeLine.Rows[0].Cells)
                                dgCell.Style.BackColor = Color.Green;
                        }
                        else
                        {
                            return;
                        }

                        mDR = dsProductionPlanning.Tables["Tbl_ParticularShiftDownTimes"].Select("ShamsiDate = " + ShamsiDate + " And ShiftNo = " + mShiftNo);
                        if (mDR.Length > 0)
                        {
                            if (dgShiftTimeLine.Columns.Count > 0)
                            {
                                FillShiftDownTimes(mDR);
                            }
                        }
                    }
                    else
                    {
                        txtShiftStart.Text = "00:00";
                        txtShiftDuration.Text = "00:00";
                        txtShiftExtraTime.Text = "00:00";
                        mDR = new DataRow[1];
                        mDR[0] = dsProductionPlanning.Tables["Tbl_CalendarParticularShifts"].NewRow();
                        if (rbActive.Checked) // در صورتیکه روز کاری باشد
                        {
                            mDR[0]["CalendarCode"] = mCalendarCode;
                            mDR[0]["ShamsiDate"] = ShamsiDate;
                            mDR[0]["ShiftNo"] = mShiftNo;
                            mDR[0]["ShiftStart"] = txtShiftStart.Text;
                            mDR[0]["ShiftDuration"] = txtShiftDuration.Text;
                            mDR[0]["ShiftExtraTime"] = txtShiftExtraTime.Text;
                            dsProductionPlanning.Tables["Tbl_CalendarParticularShifts"].Rows.Add(mDR[0]);
                            mDR = dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].Select("ShamsiDate = " + ShamsiDate);
                            if (mDR.Length == 0)
                            {
                                mDR = new DataRow[1];
                                mDR[0] = dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].NewRow();
                                mDR[0]["CalendarCode"] = mCalendarCode;
                                mDR[0]["ShamsiDate"] = ShamsiDate;
                                mDR[0]["DayType"] = 2;
                                mDR[0]["AccessibleWorkTime"] = GetAccessibleTime();
                                mDR[0]["Description"] = txtDescription.Text;
                                dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].Rows.Add(mDR[0]);
                            }
                            else
                            {
                                mDR[0].BeginEdit();
                                mDR[0]["AccessibleWorkTime"] = GetAccessibleTime();
                                mDR[0]["Description"] = txtDescription.Text;
                                mDR[0].EndEdit();
                            }

                            AddShiftDownTimes("Tbl_ParticularShiftDownTimes", mShiftNo, "ShamsiDate", ShamsiDate);
                        }
                        else // در صورتیکه روز تعطیل باشد
                        {
                            mDR[0]["CalendarCode"] = mCalendarCode;
                            mDR[0]["ShamsiDate"] = ShamsiDate;
                            mDR[0]["ShiftNo"] = mShiftNo;
                            mDR[0]["ShiftStart"] = "00:00";
                            mDR[0]["ShiftDuration"] = "00:00";
                            mDR[0]["ShiftExtraTime"] = "00:00";
                            dsProductionPlanning.Tables["Tbl_CalendarParticularShifts"].Rows.Add(mDR[0]);
                            mDR = dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].Select("ShamsiDate = " + ShamsiDate);
                            if (mDR.Length == 0)
                            {
                                mDR = new DataRow[1];
                                mDR[0] = dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].NewRow();
                                mDR[0]["CalendarCode"] = mCalendarCode;
                                mDR[0]["ShamsiDate"] = ShamsiDate;
                                mDR[0]["DayType"] = 1;
                                mDR[0]["AccessibleWorkTime"] = "00:00";
                                mDR[0]["Description"] = txtDescription.Text;
                                dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].Rows.Add(mDR[0]);
                            }
                            else
                            {
                                mDR[0].BeginEdit();
                                mDR[0]["AccessibleWorkTime"] = GetAccessibleTime();
                                mDR[0]["Description"] = txtDescription.Text;
                                mDR[0].EndEdit();
                            }
                        }
                    }

                    if (!rbHoliday.Checked)
                    {
                        SetShiftItemEnabled(true);
                        cmdSave.Enabled = false;
                        cmdShowShiftTimeLine.Enabled = false;
                    }
                    else
                    {
                        SetShiftItemEnabled(false);
                        cmdSave.Enabled = false;
                        cmdSaveShift.Enabled = true;
                    }
                }
            }
            else
            {
                SetShiftItemEnabled(false);
                cmdSave.Enabled = true;
            }
        }

        private void dgCalenderDays_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                return;
            }

            short DayType;
            DataView dvShifts;
            CurrentRow = e.RowIndex;
            CurrentColumn = e.ColumnIndex;
            txtShiftStart.Text = "00:00";
            txtShiftDuration.Text = "00:00";
            txtShiftExtraTime.Text = "00:00";
            dgShiftTimeLine.Columns.Clear();
            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(dgCalenderDays.Rows[CurrentRow].Cells[CurrentColumn].Value, Constants.vbNullString, false)))
            {
                rbParticular.Enabled = true;
                if (dgCalenderDays.Rows[CurrentRow].Cells[CurrentColumn].Value.ToString().Contains("*")) // اگر روز خاص انتخاب شده باشد
                {
                    string ShamsiDate = GetDayShamsiDate(dgCalenderDays.Rows[CurrentRow].Cells[CurrentColumn].Value.ToString().Split(Conversions.ToChar(mParticularDayDelimiter))[0].Trim(), dgCalenderDays.Tag.ToString());
                    dgShifts.Tag = "3";
                    dsProductionPlanning.Tables["Tbl_CalendarParticularShifts"].DefaultView.RowFilter = "ShamsiDate=" + ShamsiDate;
                    dvShifts = dsProductionPlanning.Tables["Tbl_CalendarParticularShifts"].DefaultView;
                    dgShifts.Rows.Clear();
                    dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].DefaultView.RowFilter = "ShamsiDate=" + ShamsiDate;
                    var dvDays = dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].DefaultView;
                    DayType = Conversions.ToShort(dvDays[0]["DayType"]);
                    txtDescription.Text = Conversions.ToString(dvDays[0]["Description"]);
                    if (DayType == 2) // روز خاص کاری
                    {
                        rbActive.Checked = true;
                    }
                    else
                    {
                        rbHoliday.Checked = true;
                    }

                    var loopTo = (short)(dvShifts.Count - 1);
                    for (I = 0; I <= loopTo; I++)
                        dgShifts.Rows.Add(dvShifts[I][2], "به روزآوری مشخصات شیفت " + dvShifts[I][2].ToString());
                    rbParticular.Checked = true;
                }
                else // اگر روز عادی انتخاب شده باشد
                {
                    bool IsFixedHoliday = IsHoliday(Conversions.ToString(dgCalenderDays.Rows[CurrentRow].Cells[CurrentColumn].Value), dgCalenderDays.Tag.ToString());
                    dgShifts.Tag = "1";
                    dsProductionPlanning.Tables["Tbl_CalendarDays"].DefaultView.RowFilter = "DayNo=" + (CurrentRow + 1).ToString();
                    dvShifts = dsProductionPlanning.Tables["Tbl_CalendarDays"].DefaultView;
                    dvShifts.Sort = "ShiftNo";
                    DayType = Conversions.ToShort(dvShifts[0]["DayType"]);
                    txtDescription.Text = Conversions.ToString(Interaction.IIf(DBNull.Value.Equals(dvShifts[0]["Description"]), "", dvShifts[0]["Description"]));
                    dgShifts.Rows.Clear();
                    if (IsFixedHoliday)
                    {
                        rbHoliday.Checked = true;
                        var loopTo1 = (short)(dvShifts.Count - 1);
                        for (I = 0; I <= loopTo1; I++)
                            dgShifts.Rows.Add(dvShifts[I][1], "به روزآوری مشخصات شیفت " + dvShifts[I][1].ToString());
                    }
                    else if (DayType == 2)
                    {
                        rbActive.Checked = true;
                        var loopTo2 = (short)(dvShifts.Count - 1);
                        for (I = 0; I <= loopTo2; I++)
                            dgShifts.Rows.Add(dvShifts[I][1], "به روزآوری مشخصات شیفت " + dvShifts[I][1].ToString());
                    }
                    else
                    {
                        rbHoliday.Checked = true;
                        var loopTo3 = (short)(dvShifts.Count - 1);
                        for (I = 0; I <= loopTo3; I++)
                            dgShifts.Rows.Add(dvShifts[I][1], "به روزآوری مشخصات شیفت " + dvShifts[I][1].ToString());
                    }

                    rbDefualt.Checked = true;
                }

                cmdEdit.Enabled = true;
            }
            else
            {
                dgShifts.Tag = "0";
                dgShifts.Rows.Clear();
                cmdEdit.Enabled = false;
            }
        }

        private void rbDefualt_CheckedChanged(object sender, EventArgs e)
        {
            if (!cmdEdit.Enabled)
            {
                if (rbDefualt.Checked)
                {
                    SetShiftItemEnabled(false);
                    dgShifts.Enabled = false;
                    if (dgShifts.Tag.ToString().Equals("2") || dgShifts.Tag.ToString().Equals("4"))
                    {
                        pnlDayType.Enabled = true;
                    }
                    else
                    {
                        pnlDayType.Enabled = false;
                    }

                    lblDescription.Visible = false;
                    txtDescription.Visible = false;
                    cmdSave.Enabled = true;
                }
                else
                {
                    if (rbHoliday.Checked)
                    {
                        SetShiftItemEnabled(false);
                    }
                    else
                    {
                        SetShiftItemEnabled(true);
                        cmdShowShiftTimeLine.Enabled = false;
                    }

                    dgShifts.Enabled = true;
                    pnlDayType.Enabled = true;
                    lblDescription.Visible = true;
                    txtDescription.Visible = true;
                    cmdSave.Enabled = false;
                }
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            var dvShifts = dsProductionPlanning.Tables["Tbl_CalendarParticularShifts"].DefaultView;
            string ShamsiDate = Constants.vbNullString;
            for (int I = 0, loopTo = dgCalenderDays.ColumnCount - 1; I <= loopTo; I++)
            {
                if (dgCalenderDays.Rows[CurrentRow].Cells[I].Value is object && !dgCalenderDays.Rows[CurrentRow].Cells[I].Value.ToString().Equals(""))
                {
                    ShamsiDate = GetDayShamsiDate(dgCalenderDays.Rows[CurrentRow].Cells[I].Value.ToString().Split(Conversions.ToChar(mParticularDayDelimiter))[0].Trim(), dgCalenderDays.Tag.ToString());
                    break;
                }
            }

            short mDayNo = Module1.GetDayNo(ShamsiDate);
            string mFirstShamsiDate = txtYear.Text + "0101";
            int mShiftCount = dgShifts.RowCount;
            while (true) // یافتن اولین روز از {نوع روز هفتۀ} انتخاب شده
            {
                if (Module1.GetDayNo(mFirstShamsiDate).Equals(mDayNo))
                {
                    break;
                }

                mFirstShamsiDate = FarsiDateFunctions.AddToDate(mFirstShamsiDate, "00000001");
            }

            if ((dgShifts.Tag.ToString().Equals("3") || dgShifts.Tag.ToString().Equals("4")) && rbParticular.Checked)
            {
                if (dgShifts.Tag.ToString().Equals("3"))
                {
                    if (dgCalenderDays.Rows[CurrentRow].Cells[CurrentColumn].Value.ToString().Contains("*"))
                    {
                        ShamsiDate = GetDayShamsiDate(dgCalenderDays.Rows[CurrentRow].Cells[CurrentColumn].Value.ToString().Split(Conversions.ToChar(mParticularDayDelimiter))[0].Trim(), dgCalenderDays.Tag.ToString());
                    }
                    else
                    {
                        ShamsiDate = GetDayShamsiDate(dgCalenderDays.Rows[CurrentRow].Cells[CurrentColumn].Value.ToString(), dgCalenderDays.Tag.ToString());
                    }

                    if (!CheckParticulareDayValidation(dvShifts, ShamsiDate))
                    {
                        return;
                    }
                }
                else if (dgShifts.Tag.ToString().Equals("4"))
                {
                    ShamsiDate = mFirstShamsiDate;
                    while (true)
                    {
                        dvShifts.RowFilter = "";
                        if (!CheckParticulareDayValidation(dvShifts, ShamsiDate))
                        {
                            return;
                        }

                        ShamsiDate = FarsiDateFunctions.AddToDate(ShamsiDate, "00000007");
                        if ((ShamsiDate.Substring(0, 4) ?? "") != (txtYear.Text ?? ""))
                        {
                            break;
                        }
                    }
                }
            }

            bool mIsFoundError = false;
            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                Module1.cnProductionPlanning.Open();
            var trnDayShifts = Module1.cnProductionPlanning.BeginTransaction();
            try
            {
                if (dgShifts.Tag.ToString().Equals("2")) // شیفت های یک روز از هفته
                {
                    if (rbDefualt.Checked)
                    {
                        var mDR = dsProductionPlanning.Tables["Tbl_CalendarDays"].Select("DayNo = " + (CurrentRow + 1));
                        foreach (DataRow r in mDR)
                        {
                            if (rbActive.Checked)
                            {
                                var mdrShift = dsProductionPlanning.Tables["Tbl_CalendarShifts"].Select("ShiftNo = " + r["ShiftNo"].ToString());
                                if (mdrShift.Length > 0)
                                {
                                    EditShiftRow(r, Conversions.ToString(mdrShift[0]["ShiftStart"]), Conversions.ToString(mdrShift[0]["ShiftDuration"]), Conversions.ToString(mdrShift[0]["ShiftExtraTime"]), "DayType", 2.ToString(), "Description", txtDescription.Text);
                                }
                            }
                            else
                            {
                                EditShiftRow(r, "00:00", "00:00", "00:00", "DayType", 1.ToString(), "Description", txtDescription.Text);
                            }

                            mDR = dsProductionPlanning.Tables["Tbl_CalendarDaysDownTimes"].Select("ShiftNo = " + r["ShiftNo"].ToString() + " And DayNo = " + (CurrentRow + 1).ToString());
                            for (int I = mDR.Length - 1; I >= 0; I -= 1)
                                mDR[I].Delete();
                            if (rbActive.Checked)
                            {
                                var mDTR = dsProductionPlanning.Tables["Tbl_CalendarShiftDownTimes"].Select("ShiftNo = " + r["ShiftNo"].ToString());
                                foreach (DataRow rDT in mDTR)
                                {
                                    var mNewRow = dsProductionPlanning.Tables["Tbl_CalendarDaysDownTimes"].NewRow();
                                    mNewRow["CalendarCode"] = mCalendarCode;
                                    mNewRow["ShiftNo"] = r["ShiftNo"];
                                    mNewRow["DayNo"] = (CurrentRow + 1).ToString();
                                    mNewRow["DownTimeStart"] = rDT["DownTimeStart"];
                                    mNewRow["DownTimeEnd"] = rDT["DownTimeEnd"];
                                    dsProductionPlanning.Tables["Tbl_CalendarDaysDownTimes"].Rows.Add(mNewRow);
                                }
                            }
                        }
                    }
                    else if (rbParticular.Checked)
                    {
                        for (int I = 0, loopTo1 = mShiftCount - 1; I <= loopTo1; I++)
                            Create_Or_Edit_ParticulareWeekDayShift((I + 1).ToString());
                    }
                }
                else if (dgShifts.Tag.ToString().Equals("4") && rbDefualt.Checked)
                {
                    var mDR = Array.Empty<DataRow>();
                    ShamsiDate = mFirstShamsiDate;
                    while (true)
                    {
                        mDR = dsProductionPlanning.Tables["Tbl_ParticularShiftDownTimes"].Select("ShamsiDate=" + ShamsiDate);
                        for (int I = mDR.Length - 1; I >= 0; I -= 1)
                            dsProductionPlanning.Tables["Tbl_ParticularShiftDownTimes"].Rows.Remove(mDR[I]);
                        mDR = dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].Select("ShamsiDate=" + ShamsiDate);
                        for (int I = mDR.Length - 1; I >= 0; I -= 1)
                            mDR[I].Delete();
                        mDR = dsProductionPlanning.Tables["Tbl_CalendarParticularShifts"].Select("ShamsiDate=" + ShamsiDate);
                        for (int I = mDR.Length - 1; I >= 0; I -= 1)
                            mDR[I].Delete();
                        ShamsiDate = FarsiDateFunctions.AddToDate(ShamsiDate, "00000007");
                        if ((ShamsiDate.Substring(0, 4) ?? "") != (txtYear.Text ?? ""))
                        {
                            break;
                        }
                    }

                    mDR = dsProductionPlanning.Tables["Tbl_CalendarDays"].Select("DayNo = " + (CurrentRow + 1));
                    foreach (DataRow r in mDR)
                    {
                        if (rbActive.Checked)
                        {
                            var mdrShift = dsProductionPlanning.Tables["Tbl_CalendarShifts"].Select("ShiftNo = " + r["ShiftNo"].ToString());
                            if (mdrShift.Length > 0)
                            {
                                EditShiftRow(r, Conversions.ToString(mdrShift[0]["ShiftStart"]), Conversions.ToString(mdrShift[0]["ShiftDuration"]), Conversions.ToString(mdrShift[0]["ShiftExtraTime"]), "DayType", 2.ToString(), "Description", txtDescription.Text);
                            }
                        }
                        else
                        {
                            EditShiftRow(r, "00:00", "00:00", "00:00", "DayType", 1.ToString(), "Description", txtDescription.Text);
                        }

                        mDR = dsProductionPlanning.Tables["Tbl_CalendarDaysDownTimes"].Select("ShiftNo = " + r["ShiftNo"].ToString() + " And DayNo = " + (CurrentRow + 1).ToString());
                        for (int I = mDR.Length - 1; I >= 0; I -= 1)
                            mDR[I].Delete();
                        if (rbActive.Checked)
                        {
                            var mDTR = dsProductionPlanning.Tables["Tbl_CalendarShiftDownTimes"].Select("ShiftNo = " + r["ShiftNo"].ToString());
                            foreach (DataRow rDT in mDTR)
                            {
                                var mNewRow = dsProductionPlanning.Tables["Tbl_CalendarDaysDownTimes"].NewRow();
                                mNewRow["CalendarCode"] = mCalendarCode;
                                mNewRow["ShiftNo"] = r["ShiftNo"];
                                mNewRow["DayNo"] = (CurrentRow + 1).ToString();
                                mNewRow["DownTimeStart"] = rDT["DownTimeStart"];
                                mNewRow["DownTimeEnd"] = rDT["DownTimeEnd"];
                                dsProductionPlanning.Tables["Tbl_CalendarDaysDownTimes"].Rows.Add(mNewRow);
                            }
                        }
                    }
                }
                else if (dgShifts.Tag.ToString().Equals("3") && rbDefualt.Checked)
                {
                    ShamsiDate = GetDayShamsiDate(dgCalenderDays.Rows[CurrentRow].Cells[CurrentColumn].Value.ToString().Split(Conversions.ToChar(mParticularDayDelimiter))[0].Trim(), dgCalenderDays.Tag.ToString());
                    var mDR = dsProductionPlanning.Tables["Tbl_ParticularShiftDownTimes"].Select("ShamsiDate=" + ShamsiDate);
                    for (int I = mDR.Length - 1; I >= 0; I -= 1)
                        dsProductionPlanning.Tables["Tbl_ParticularShiftDownTimes"].Rows.Remove(mDR[I]);
                    mDR = dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].Select("ShamsiDate=" + ShamsiDate);
                    for (int I = mDR.Length - 1; I >= 0; I -= 1)
                        mDR[I].Delete();
                    mDR = dsProductionPlanning.Tables["Tbl_CalendarParticularShifts"].Select("ShamsiDate=" + ShamsiDate);
                    for (int I = mDR.Length - 1; I >= 0; I -= 1)
                        mDR[I].Delete();
                    dgShifts.Tag = "1";
                }
                else if (dgShifts.Tag.ToString().Equals("1") && rbParticular.Checked)
                {
                    if (dgCalenderDays.Rows[CurrentRow].Cells[CurrentColumn].Value.ToString().Contains("*"))
                    {
                        ShamsiDate = GetDayShamsiDate(dgCalenderDays.Rows[CurrentRow].Cells[CurrentColumn].Value.ToString().Split(Conversions.ToChar(mParticularDayDelimiter))[0].Trim(), dgCalenderDays.Tag.ToString());
                    }
                    else
                    {
                        ShamsiDate = GetDayShamsiDate(dgCalenderDays.Rows[CurrentRow].Cells[CurrentColumn].Value.ToString(), dgCalenderDays.Tag.ToString());
                    }

                    for (int I = 0, loopTo2 = mShiftCount - 1; I <= loopTo2; I++)
                    {
                        string mShiftNo = dgShifts.Rows[I].Cells[0].Value.ToString();
                        var mDR = dsProductionPlanning.Tables["Tbl_CalendarParticularShifts"].Select("ShamsiDate = " + ShamsiDate + " And ShiftNo = " + mShiftNo);
                        if (mDR.Length == 0)
                        {
                            if (rbActive.Checked) // در صورتیکه روز کاری باشد
                            {
                                MessageBox.Show("مشخصات شیفت {" + mShiftNo + "} را به روزآوری نمایید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                                return;
                            }
                            else // در صورتیکه روز تعطیل باشد
                            {
                                mDR = new DataRow[1];
                                mDR[0] = dsProductionPlanning.Tables["Tbl_CalendarParticularShifts"].NewRow();
                                mDR[0]["CalendarCode"] = mCalendarCode;
                                mDR[0]["ShamsiDate"] = ShamsiDate;
                                mDR[0]["ShiftNo"] = mShiftNo;
                                mDR[0]["ShiftStart"] = "00:00";
                                mDR[0]["ShiftDuration"] = "00:00";
                                mDR[0]["ShiftExtraTime"] = "00:00";
                                dsProductionPlanning.Tables["Tbl_CalendarParticularShifts"].Rows.Add(mDR[0]);
                                mDR = dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].Select("ShamsiDate = " + ShamsiDate);
                                if (mDR.Length == 0)
                                {
                                    mDR = new DataRow[1];
                                    mDR[0] = dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].NewRow();
                                    mDR[0]["CalendarCode"] = mCalendarCode;
                                    mDR[0]["ShamsiDate"] = ShamsiDate;
                                    mDR[0]["DayType"] = 1;
                                    mDR[0]["AccessibleWorkTime"] = "00:00";
                                    mDR[0]["Description"] = txtDescription.Text;
                                    dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].Rows.Add(mDR[0]);
                                }
                                else
                                {
                                    mDR[0].BeginEdit();
                                    mDR[0]["DayType"] = 1;
                                    mDR[0]["AccessibleWorkTime"] = "00:00";
                                    mDR[0]["Description"] = txtDescription.Text;
                                    mDR[0].BeginEdit();
                                }
                            }
                        }
                        else
                        {
                            if (rbHoliday.Checked)
                            {
                                mDR[0].BeginEdit();
                                mDR[0]["ShiftStart"] = "00:00";
                                mDR[0]["ShiftDuration"] = "00:00";
                                mDR[0]["ShiftExtraTime"] = "00:00";
                                mDR[0].EndEdit();
                            }

                            mDR = dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].Select("ShamsiDate = " + ShamsiDate);
                            if (mDR.Length == 0)
                            {
                                mDR = new DataRow[1];
                                mDR[0] = dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].NewRow();
                                mDR[0]["CalendarCode"] = mCalendarCode;
                                mDR[0]["ShamsiDate"] = ShamsiDate;
                                mDR[0]["DayType"] = 1;
                                mDR[0]["AccessibleWorkTime"] = "00:00";
                                mDR[0]["Description"] = txtDescription.Text;
                                dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].Rows.Add(mDR[0]);
                            }
                            else
                            {
                                mDR[0].BeginEdit();
                                mDR[0]["DayType"] = 1;
                                mDR[0]["AccessibleWorkTime"] = "00:00";
                                mDR[0]["Description"] = txtDescription.Text;
                                mDR[0].BeginEdit();
                            }
                        }

                        dgShifts.Tag = "3";
                    }
                }

                daCalendarDays.InsertCommand.Transaction = trnDayShifts;
                daCalendarDays.UpdateCommand.Transaction = trnDayShifts;
                daCalendarDays.DeleteCommand.Transaction = trnDayShifts;
                daCalendarParticularDays.InsertCommand.Transaction = trnDayShifts;
                daCalendarParticularDays.UpdateCommand.Transaction = trnDayShifts;
                daCalendarParticularDays.DeleteCommand.Transaction = trnDayShifts;
                daCalendarParticularShifts.InsertCommand.Transaction = trnDayShifts;
                daCalendarParticularShifts.UpdateCommand.Transaction = trnDayShifts;
                daCalendarParticularShifts.DeleteCommand.Transaction = trnDayShifts;
                mdaParticulareShiftDownTimes.InsertCommand.Transaction = trnDayShifts;
                mdaParticulareShiftDownTimes.UpdateCommand.Transaction = trnDayShifts;
                mdaParticulareShiftDownTimes.DeleteCommand.Transaction = trnDayShifts;
                mdaDaysDownTimes.InsertCommand.Transaction = trnDayShifts;
                mdaDaysDownTimes.UpdateCommand.Transaction = trnDayShifts;
                mdaDaysDownTimes.DeleteCommand.Transaction = trnDayShifts;
                SaveChanges();
                trnDayShifts.Commit();
            }
            catch (Exception objEx)
            {
                mIsFoundError = true;
                trnDayShifts.Rollback();
                Logger.SaveError(Name + ".cmdSave_Click", objEx.Message);
                MessageBox.Show("ثبت مشخصات زمانبندی روز با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
            finally
            {
                if (!mIsFoundError)
                {
                    if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                        Module1.cnProductionPlanning.Close();
                    tcDays.Enabled = true;
                    Panel2.Enabled = false;
                    cmdSave.Enabled = false;
                    cmdExit.Text = "بازگشت";

                    // ترسیم مجدد تقویم با مقادیر جدید
                    ReArangeCalender();
                    SetNewMonthDays();
                }
            }
        }

        private void FormLoad()
        {
            CreateDataAdapterCommands();
            HolidayCellStyle.BackColor = Color.Red;
            HolidayCellStyle.ForeColor = Color.Black;
            string dtDay = Module1.mServerShamsiDate;
            cbMonth.SelectedIndex = Conversions.ToInteger(Strings.Mid(dtDay, 5, 2)) - 1;
            txtYear.Value = Conversions.ToInteger(Strings.Mid(dtDay, 1, 4));

            // پاک کردن جدول روزهای ماه
            ReArangeCalender();
            // تنظیم جدول روزهای ماه با مقادیر ماه انتخاب شده
            SetNewMonthDays();
        }

        private void ReArangeCalender()
        {
            dgCalenderDays.Columns.Clear();
            dgCalenderDays.Rows.Clear();
            dgCalenderDays.Columns.Add("Column1", "");
            dgCalenderDays.Columns[0].Width = 75;
            dgCalenderDays.Columns.Add("Column2", "");
            dgCalenderDays.Columns[1].Width = 75;
            dgCalenderDays.Columns.Add("Column3", "");
            dgCalenderDays.Columns[2].Width = 75;
            dgCalenderDays.Columns.Add("Column4", "");
            dgCalenderDays.Columns[3].Width = 75;
            dgCalenderDays.Columns.Add("Column5", "");
            dgCalenderDays.Columns[4].Width = 75;
            dgCalenderDays.Columns.Add("Column6", "");
            dgCalenderDays.Columns[5].Width = 75;
            short I;
            var loopTo = (short)(dgCalenderDays.Columns.Count - 1);
            for (I = 0; I <= loopTo; I++)
                dgCalenderDays.Columns[I].SortMode = DataGridViewColumnSortMode.NotSortable;
            for (I = 0; I <= 6; I++)
                dgCalenderDays.Rows.Add();
            dgCalenderDays.Rows[0].HeaderCell.Value = "شنبه";
            dgCalenderDays.Rows[1].HeaderCell.Value = "یکشنبه";
            dgCalenderDays.Rows[2].HeaderCell.Value = "دوشنبه";
            dgCalenderDays.Rows[3].HeaderCell.Value = "سه شنبه";
            dgCalenderDays.Rows[4].HeaderCell.Value = "چهارشنبه";
            dgCalenderDays.Rows[5].HeaderCell.Value = "پنجشنبه";
            dgCalenderDays.Rows[6].HeaderCell.Value = "جمعه";
        }

        private void SetNewMonthDays()
        {
            string FarsiDate;
            DataView dvDays;
            dgCalenderDays.Tag = (cbMonth.SelectedIndex + 1).ToString();
            switch (cbMonth.SelectedIndex)
            {
                case var @case when @case < 9:
                    {
                        FarsiDate = txtYear.Value.ToString() + "/0" + dgCalenderDays.Tag.ToString() + "/01";
                        break;
                    }

                default:
                    {
                        FarsiDate = txtYear.Value.ToString() + "/" + dgCalenderDays.Tag.ToString() + "/01";
                        break;
                    }
            }

            DateTime dtCaleder = Conversions.ToDate(Module1.Get_LatinDate_FromPersianDate(FarsiDate));
            var FirstDayLocation = default(short);
            switch (dtCaleder.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    {
                        dgCalenderDays.Rows[0].Cells[0].Value = "1";
                        FirstDayLocation = 0;
                        break;
                    }

                case DayOfWeek.Sunday:
                    {
                        dgCalenderDays.Rows[1].Cells[0].Value = "1";
                        FirstDayLocation = 1;
                        break;
                    }

                case DayOfWeek.Monday:
                    {
                        dgCalenderDays.Rows[2].Cells[0].Value = "1";
                        FirstDayLocation = 2;
                        break;
                    }

                case DayOfWeek.Tuesday:
                    {
                        dgCalenderDays.Rows[3].Cells[0].Value = "1";
                        FirstDayLocation = 3;
                        break;
                    }

                case DayOfWeek.Wednesday:
                    {
                        dgCalenderDays.Rows[4].Cells[0].Value = "1";
                        FirstDayLocation = 4;
                        break;
                    }

                case DayOfWeek.Thursday:
                    {
                        dgCalenderDays.Rows[5].Cells[0].Value = "1";
                        FirstDayLocation = 5;
                        break;
                    }

                case DayOfWeek.Friday:
                    {
                        dgCalenderDays.Rows[6].Cells[0].Value = "1";
                        FirstDayLocation = 6;
                        break;
                    }
            }

            // کنترل اینکه آیا برای روز اول ماه تعریف خاصی وجود دارد یا نه
            int PDay = ParticularDay(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(txtYear.Value.ToString(), Interaction.IIf(cbMonth.SelectedIndex < 9, "0" + dgCalenderDays.Tag.ToString(), dgCalenderDays.Tag.ToString())), "01")));
            if (PDay != -1) // اگر روز خاص بود
            {
                switch (PDay)
                {
                    case 1:
                        {
                            dgCalenderDays.Rows[FirstDayLocation].Cells[0].Style.ApplyStyle(HolidayCellStyle);
                            break;
                        }
                }

                dgCalenderDays.Rows[FirstDayLocation].Cells[0].Value = Operators.ConcatenateObject(Operators.ConcatenateObject(dgCalenderDays.Rows[FirstDayLocation].Cells[0].Value, mParticularDayDelimiter), "*");
            }
            else if (FirstDayLocation == 6 || IsHoliday("1", dgCalenderDays.Tag.ToString())) // اگر روز خاص نبود و جمعه یا روز تعطیل رسمی بود
            {
                dgCalenderDays.Rows[FirstDayLocation].Cells[0].Style.ApplyStyle(HolidayCellStyle);
            }
            else // اگر روز طبق تقویم جاری تعطیل بود
            {
                dsProductionPlanning.Tables["Tbl_CalendarDays"].DefaultView.RowFilter = "DayNo=" + (FirstDayLocation + 1);
                dvDays = dsProductionPlanning.Tables["Tbl_CalendarDays"].DefaultView;
                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dvDays[0]["DayType"], 1, false)))
                {
                    dgCalenderDays.Rows[FirstDayLocation].Cells[0].Style.ApplyStyle(HolidayCellStyle);
                }
            }

            short DayCounter, RowCounter, ColumnCounter;
            short LastDayofMonth = Conversions.ToShort(Interaction.IIf(cbMonth.SelectedIndex < 6, 31, Interaction.IIf(cbMonth.SelectedIndex < 11, 30, Interaction.IIf(FarsiDateFunctions.IsKabiseh((int)Math.Round(txtYear.Value)), 30, 29))));
            ColumnCounter = 0;
            RowCounter = FirstDayLocation;
            var loopTo = LastDayofMonth;
            for (DayCounter = 2; DayCounter <= loopTo; DayCounter++)
            {
                RowCounter = (short)(RowCounter + 1);
                if (RowCounter > 6)
                {
                    RowCounter = 0;
                    ColumnCounter = (short)(ColumnCounter + 1);
                }

                // کنترل اینکه آیا برای روز جاری ماه تعریف خاصی وجود دارد یا نه
                PDay = ParticularDay(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(txtYear.Value.ToString(), Interaction.IIf(cbMonth.SelectedIndex < 9, "0" + dgCalenderDays.Tag.ToString(), dgCalenderDays.Tag.ToString())), Interaction.IIf(DayCounter < 10, "0" + DayCounter.ToString(), DayCounter.ToString()))));
                if (PDay != -1) // اگر روز خاص بود
                {
                    switch (PDay)
                    {
                        case 1:
                            {
                                dgCalenderDays.Rows[RowCounter].Cells[ColumnCounter].Style.ApplyStyle(HolidayCellStyle);
                                break;
                            }
                    }

                    dgCalenderDays.Rows[RowCounter].Cells[ColumnCounter].Value = DayCounter.ToString() + mParticularDayDelimiter + "*";
                }
                else
                {
                    dgCalenderDays.Rows[RowCounter].Cells[ColumnCounter].Value = DayCounter;
                    if (RowCounter == 6 || IsHoliday(DayCounter.ToString(), dgCalenderDays.Tag.ToString())) // اگر روز خاص نبود و جمعه یا روز تعطیل رسمی بود
                    {
                        dgCalenderDays.Rows[RowCounter].Cells[ColumnCounter].Style.ApplyStyle(HolidayCellStyle);
                    }
                    else // اگر روز طبق تعقویم جاری تعطیل بود
                    {
                        dsProductionPlanning.Tables["Tbl_CalendarDays"].DefaultView.RowFilter = "DayNo=" + (RowCounter + 1);
                        dvDays = dsProductionPlanning.Tables["Tbl_CalendarDays"].DefaultView;
                        if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dvDays[0]["DayType"], 1, false)))
                        {
                            dgCalenderDays.Rows[RowCounter].Cells[ColumnCounter].Style.ApplyStyle(HolidayCellStyle);
                        }
                    }
                }
            }
        }

        private void CreateDataAdapterCommands()
        {
            // ---------- تنظیم دستورات مربوط به روزهای هفته در تقویم کاری ----------
            // ایجاد دستور اصلاح رکورد جاری در جدول
            daCalendarDays.InsertCommand = new SqlCommand("Insert Into Tbl_CalendarDays(CalendarCode,ShiftNo,DayNo,ShiftStart,ShiftDuration,ShiftExtraTime,DayName,DayType,Description) " + "Values(@CalendarCode,@ShiftNo,@DayNo,@ShiftStart,@ShiftDuration,@ShiftExtraTime,@DayName,@DayType,@Description)", Module1.cnProductionPlanning);
            {
                var withBlock = daCalendarDays.InsertCommand;
                withBlock.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode");
                withBlock.Parameters.Add("@ShiftNo", SqlDbType.TinyInt, default, "ShiftNo");
                withBlock.Parameters.Add("@DayNo", SqlDbType.TinyInt, default, "DayNo");
                withBlock.Parameters.Add("@ShiftStart", SqlDbType.VarChar, 50, "ShiftStart");
                withBlock.Parameters.Add("@ShiftDuration", SqlDbType.VarChar, 50, "ShiftDuration");
                withBlock.Parameters.Add("@ShiftExtraTime", SqlDbType.VarChar, 50, "ShiftExtraTime");
                withBlock.Parameters.Add("@DayName", SqlDbType.VarChar, 50, "DayName");
                withBlock.Parameters.Add("@DayType", SqlDbType.TinyInt, default, "DayType");
                withBlock.Parameters.Add("@Description", SqlDbType.VarChar, 255, "Description");
            }
            // ایجاد دستور اصلاح رکورد جاری در جدول
            daCalendarDays.UpdateCommand = new SqlCommand("Update Tbl_CalendarDays Set ShiftStart=@ShiftStart,ShiftDuration=@ShiftDuration,ShiftExtraTime=@ShiftExtraTime,DayName=@DayName,DayType=@DayType,Description=@Description Where CalendarCode=@CalendarCode And ShiftNo=@ShiftNo And DayNo=@DayNo", Module1.cnProductionPlanning);
            {
                var withBlock1 = daCalendarDays.UpdateCommand;
                withBlock1.Parameters.Add("@ShiftStart", SqlDbType.VarChar, 50, "ShiftStart");
                withBlock1.Parameters.Add("@ShiftDuration", SqlDbType.VarChar, 50, "ShiftDuration");
                withBlock1.Parameters.Add("@ShiftExtraTime", SqlDbType.VarChar, 50, "ShiftExtraTime");
                withBlock1.Parameters.Add("@DayName", SqlDbType.VarChar, 50, "DayName");
                withBlock1.Parameters.Add("@DayType", SqlDbType.TinyInt, default, "DayType");
                withBlock1.Parameters.Add("@Description", SqlDbType.VarChar, 255, "Description");
                withBlock1.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode").SourceVersion = DataRowVersion.Original;
                withBlock1.Parameters.Add("@ShiftNo", SqlDbType.TinyInt, default, "ShiftNo").SourceVersion = DataRowVersion.Original;
                withBlock1.Parameters.Add("@DayNo", SqlDbType.TinyInt, default, "DayNo").SourceVersion = DataRowVersion.Original;
            }
            // ایجاد دستور حذف رکورد جاری در جدول
            daCalendarDays.DeleteCommand = new SqlCommand("Delete From Tbl_CalendarDays Where CalendarCode=@CalendarCode And ShiftNo=@ShiftNo And DayNo=@DayNo", Module1.cnProductionPlanning);
            {
                var withBlock2 = daCalendarDays.DeleteCommand;
                withBlock2.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode").SourceVersion = DataRowVersion.Original;
                withBlock2.Parameters.Add("@ShiftNo", SqlDbType.TinyInt, default, "ShiftNo").SourceVersion = DataRowVersion.Original;
                withBlock2.Parameters.Add("@DayNo", SqlDbType.TinyInt, default, "DayNo").SourceVersion = DataRowVersion.Original;
            }

            mdaDaysDownTimes.InsertCommand = new SqlCommand("Insert Into Tbl_CalendarDaysDownTimes(CalendarCode,ShiftNo,DayNo,DownTimeStart,DownTimeEnd) Values(@CalendarCode,@ShiftNo,@DayNo,@DownTimeStart,@DownTimeEnd)", Module1.cnProductionPlanning);
            {
                var withBlock3 = mdaDaysDownTimes.InsertCommand;
                withBlock3.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode");
                withBlock3.Parameters.Add("@ShiftNo", SqlDbType.TinyInt, default, "ShiftNo");
                withBlock3.Parameters.Add("@DayNo", SqlDbType.TinyInt, default, "DayNo");
                withBlock3.Parameters.Add("@DownTimeStart", SqlDbType.VarChar, 50, "DownTimeStart");
                withBlock3.Parameters.Add("@DownTimeEnd", SqlDbType.VarChar, 50, "DownTimeEnd");
            }

            mdaDaysDownTimes.UpdateCommand = new SqlCommand("Update Tbl_CalendarDaysDownTimes Set CalendarCode=@C_CalendarCode,ShiftNo=@C_ShiftNo,DayNo=@C_DayNo,DownTimeStart=@C_DownTimeStart,DownTimeEnd=@DownTimeEnd Where CalendarCode=@CalendarCode And ShiftNo=@ShiftNo And DayNo=@DayNo And DownTimeStart=@DownTimeStart", Module1.cnProductionPlanning);
            {
                var withBlock4 = mdaDaysDownTimes.UpdateCommand;
                withBlock4.Parameters.Add("@C_CalendarCode", SqlDbType.Int, default, "CalendarCode").SourceVersion = DataRowVersion.Current;
                withBlock4.Parameters.Add("@C_ShiftNo", SqlDbType.TinyInt, default, "ShiftNo").SourceVersion = DataRowVersion.Current;
                withBlock4.Parameters.Add("@C_DayNo", SqlDbType.TinyInt, default, "DayNo").SourceVersion = DataRowVersion.Current;
                withBlock4.Parameters.Add("@C_DownTimeStart", SqlDbType.VarChar, 50, "DownTimeStart").SourceVersion = DataRowVersion.Current;
                withBlock4.Parameters.Add("@DownTimeEnd", SqlDbType.VarChar, 50, "DownTimeEnd");
                withBlock4.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode").SourceVersion = DataRowVersion.Original;
                withBlock4.Parameters.Add("@ShiftNo", SqlDbType.TinyInt, default, "ShiftNo").SourceVersion = DataRowVersion.Original;
                withBlock4.Parameters.Add("@DayNo", SqlDbType.TinyInt, default, "DayNo").SourceVersion = DataRowVersion.Original;
                withBlock4.Parameters.Add("@DownTimeStart", SqlDbType.VarChar, 50, "DownTimeStart").SourceVersion = DataRowVersion.Original;
            }

            // ایجاد دستور حذف رکورد جاری در جدول
            mdaDaysDownTimes.DeleteCommand = new SqlCommand("Delete From Tbl_CalendarDaysDownTimes Where CalendarCode=@CalendarCode And ShiftNo=@ShiftNo And DayNo=@DayNo And DownTimeStart=@DownTimeStart", Module1.cnProductionPlanning);
            {
                var withBlock5 = mdaDaysDownTimes.DeleteCommand;
                withBlock5.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode").SourceVersion = DataRowVersion.Original;
                withBlock5.Parameters.Add("@ShiftNo", SqlDbType.TinyInt, default, "ShiftNo").SourceVersion = DataRowVersion.Original;
                withBlock5.Parameters.Add("@DayNo", SqlDbType.TinyInt, default, "DayNo").SourceVersion = DataRowVersion.Original;
                withBlock5.Parameters.Add("@DownTimeStart", SqlDbType.VarChar, 50, "DownTimeStart").SourceVersion = DataRowVersion.Original;
            }

            mdaParticulareShiftDownTimes.InsertCommand = new SqlCommand("Insert Into Tbl_ParticularShiftDownTimes(CalendarCode,ShamsiDate,ShiftNo,DownTimeStart,DownTimeEnd) Values(@CalendarCode,@ShamsiDate,@ShiftNo,@DownTimeStart,@DownTimeEnd)", Module1.cnProductionPlanning);
            {
                var withBlock6 = mdaParticulareShiftDownTimes.InsertCommand;
                withBlock6.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode");
                withBlock6.Parameters.Add("@ShamsiDate", SqlDbType.Int, default, "ShamsiDate");
                withBlock6.Parameters.Add("@ShiftNo", SqlDbType.TinyInt, default, "ShiftNo");
                withBlock6.Parameters.Add("@DownTimeStart", SqlDbType.VarChar, 50, "DownTimeStart");
                withBlock6.Parameters.Add("@DownTimeEnd", SqlDbType.VarChar, 50, "DownTimeEnd");
            }

            mdaParticulareShiftDownTimes.UpdateCommand = new SqlCommand("Update Tbl_ParticularShiftDownTimes Set CalendarCode=@C_CalendarCode,ShamsiDate=@C_ShamsiDate,ShiftNo=@C_ShiftNo,DayNo=@C_DayNo,DownTimeStart=@C_DownTimeStart,DownTimeEnd=@DownTimeEnd Where CalendarCode=@CalendarCode And ShamsiDate=@ShamsiDate And ShiftNo=@ShiftNo And DownTimeStart=@DownTimeStart", Module1.cnProductionPlanning);
            {
                var withBlock7 = mdaParticulareShiftDownTimes.UpdateCommand;
                withBlock7.Parameters.Add("@C_CalendarCode", SqlDbType.Int, default, "CalendarCode").SourceVersion = DataRowVersion.Current;
                withBlock7.Parameters.Add("@C_ShamsiDate", SqlDbType.Int, default, "ShamsiDate").SourceVersion = DataRowVersion.Current;
                withBlock7.Parameters.Add("@C_ShiftNo", SqlDbType.TinyInt, default, "ShiftNo").SourceVersion = DataRowVersion.Current;
                withBlock7.Parameters.Add("@C_DownTimeStart", SqlDbType.VarChar, 50, "DownTimeStart").SourceVersion = DataRowVersion.Current;
                withBlock7.Parameters.Add("@DownTimeEnd", SqlDbType.VarChar, 50, "DownTimeEnd");
                withBlock7.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode").SourceVersion = DataRowVersion.Original;
                withBlock7.Parameters.Add("@ShamsiDate", SqlDbType.Int, default, "ShamsiDate").SourceVersion = DataRowVersion.Original;
                withBlock7.Parameters.Add("@ShiftNo", SqlDbType.TinyInt, default, "ShiftNo").SourceVersion = DataRowVersion.Original;
                withBlock7.Parameters.Add("@DownTimeStart", SqlDbType.VarChar, 50, "DownTimeStart").SourceVersion = DataRowVersion.Original;
            }

            // ایجاد دستور حذف رکورد جاری در جدول
            mdaParticulareShiftDownTimes.DeleteCommand = new SqlCommand("Delete From Tbl_ParticularShiftDownTimes Where CalendarCode=@CalendarCode And ShamsiDate=@ShamsiDate And ShiftNo=@ShiftNo And DownTimeStart=@DownTimeStart", Module1.cnProductionPlanning);
            {
                var withBlock8 = mdaParticulareShiftDownTimes.DeleteCommand;
                withBlock8.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode").SourceVersion = DataRowVersion.Original;
                withBlock8.Parameters.Add("@ShamsiDate", SqlDbType.Int, default, "ShamsiDate").SourceVersion = DataRowVersion.Original;
                withBlock8.Parameters.Add("@ShiftNo", SqlDbType.TinyInt, default, "ShiftNo").SourceVersion = DataRowVersion.Original;
                withBlock8.Parameters.Add("@DownTimeStart", SqlDbType.VarChar, 50, "DownTimeStart").SourceVersion = DataRowVersion.Original;
            }

            // ---------- تنظیم دستورات مربوط به شیفتهای خاص در تقویم کاری ----------
            // ایجاد دستور اضافه کردن رکورد جدید در جدول
            daCalendarParticularShifts.InsertCommand = new SqlCommand("Insert Into Tbl_CalendarParticularShifts(CalendarCode,ShamsiDate,ShiftNo,ShiftStart,ShiftDuration,ShiftExtraTime) Values(@CalendarCode,@ShamsiDate,@ShiftNo,@ShiftStart,@ShiftDuration,@ShiftExtraTime)", Module1.cnProductionPlanning);
            {
                var withBlock9 = daCalendarParticularShifts.InsertCommand;
                withBlock9.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode");
                withBlock9.Parameters.Add("@ShamsiDate", SqlDbType.Int, default, "ShamsiDate");
                withBlock9.Parameters.Add("@ShiftNo", SqlDbType.TinyInt, default, "ShiftNo");
                withBlock9.Parameters.Add("@ShiftStart", SqlDbType.VarChar, 50, "ShiftStart");
                withBlock9.Parameters.Add("@ShiftDuration", SqlDbType.VarChar, 50, "ShiftDuration");
                withBlock9.Parameters.Add("@ShiftExtraTime", SqlDbType.VarChar, 50, "ShiftExtraTime");
            }
            // ایجاد دستور اصلاح رکورد جاری در جدول
            daCalendarParticularShifts.UpdateCommand = new SqlCommand("Update Tbl_CalendarParticularShifts Set ShiftNo=@CurrentShiftNo,ShiftStart=@ShiftStart,ShiftDuration=@ShiftDuration,ShiftExtraTime=@ShiftExtraTime Where CalendarCode=@CalendarCode And ShamsiDate=@ShamsiDate And ShiftNo=@OldShiftNo", Module1.cnProductionPlanning);
            {
                var withBlock10 = daCalendarParticularShifts.UpdateCommand;
                withBlock10.Parameters.Add("@CurrentShiftNo", SqlDbType.TinyInt, default, "ShiftNo").SourceVersion = DataRowVersion.Current;
                withBlock10.Parameters.Add("@ShiftStart", SqlDbType.VarChar, 50, "ShiftStart");
                withBlock10.Parameters.Add("@ShiftDuration", SqlDbType.VarChar, 50, "ShiftDuration");
                withBlock10.Parameters.Add("@ShiftExtraTime", SqlDbType.VarChar, 50, "ShiftExtraTime");
                withBlock10.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode").SourceVersion = DataRowVersion.Original;
                withBlock10.Parameters.Add("@ShamsiDate", SqlDbType.Int, default, "ShamsiDate").SourceVersion = DataRowVersion.Original;
                withBlock10.Parameters.Add("@OldShiftNo", SqlDbType.TinyInt, default, "ShiftNo").SourceVersion = DataRowVersion.Original;
            }
            // ایجاد دستور حذف رکورد جاری در جدول
            daCalendarParticularShifts.DeleteCommand = new SqlCommand("Delete From Tbl_CalendarParticularShifts Where CalendarCode=@CalendarCode And ShamsiDate=@ShamsiDate And ShiftNo=@ShiftNo", Module1.cnProductionPlanning);
            {
                var withBlock11 = daCalendarParticularShifts.DeleteCommand;
                withBlock11.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode").SourceVersion = DataRowVersion.Original;
                withBlock11.Parameters.Add("@ShamsiDate", SqlDbType.Int, default, "ShamsiDate").SourceVersion = DataRowVersion.Original;
                withBlock11.Parameters.Add("@ShiftNo", SqlDbType.TinyInt, default, "ShiftNo").SourceVersion = DataRowVersion.Original;
            }

            // ---------- تنظیم دستورات مربوط به روزهای خاص در تقویم کاری ----------
            // ایجاد دستور اضافه کردن رکورد جدید در جدول
            daCalendarParticularDays.InsertCommand = new SqlCommand("Insert Into Tbl_CalendarParticularDays(CalendarCode,ShamsiDate,DayType,AccessibleWorkTime,Description) " + "Values(@CalendarCode,@ShamsiDate,@DayType,@AccessibleWorkTime,@Description)", Module1.cnProductionPlanning);
            {
                var withBlock12 = daCalendarParticularDays.InsertCommand;
                withBlock12.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode");
                withBlock12.Parameters.Add("@ShamsiDate", SqlDbType.Int, default, "ShamsiDate");
                withBlock12.Parameters.Add("@DayType", SqlDbType.TinyInt, default, "DayType");
                withBlock12.Parameters.Add("@AccessibleWorkTime", SqlDbType.Char, 5, "AccessibleWorkTime");
                withBlock12.Parameters.Add("@Description", SqlDbType.VarChar, 255, "Description");
            }
            // ایجاد دستور اصلاح رکورد جاری در جدول
            daCalendarParticularDays.UpdateCommand = new SqlCommand("Update Tbl_CalendarParticularDays Set DayType=@DayType,AccessibleWorkTime=@AccessibleWorkTime,Description=@Description Where CalendarCode=@CalendarCode And ShamsiDate=@ShamsiDate", Module1.cnProductionPlanning);
            {
                var withBlock13 = daCalendarParticularDays.UpdateCommand;
                withBlock13.Parameters.Add("@DayType", SqlDbType.TinyInt, default, "DayType");
                withBlock13.Parameters.Add("@AccessibleWorkTime", SqlDbType.Char, 5, "AccessibleWorkTime");
                withBlock13.Parameters.Add("@Description", SqlDbType.VarChar, 255, "Description");
                withBlock13.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode").SourceVersion = DataRowVersion.Original;
                withBlock13.Parameters.Add("@ShamsiDate", SqlDbType.Int, default, "ShamsiDate").SourceVersion = DataRowVersion.Original;
            }
            // ایجاد دستور حذف رکورد جاری در جدول
            daCalendarParticularDays.DeleteCommand = new SqlCommand("Delete From Tbl_CalendarParticularDays Where CalendarCode=@CalendarCode And ShamsiDate=@ShamsiDate", Module1.cnProductionPlanning);
            {
                var withBlock14 = daCalendarParticularDays.DeleteCommand;
                withBlock14.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode").SourceVersion = DataRowVersion.Original;
                withBlock14.Parameters.Add("@ShamsiDate", SqlDbType.Int, default, "ShamsiDate").SourceVersion = DataRowVersion.Original;
            }
        }
        // این تابع مشخص می کند یک تاریخ مشخص در تقویم روز خاص می باشد یا نه
        private int ParticularDay(string ShamsiDate)
        {
            dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].DefaultView.RowFilter = "ShamsiDate=" + ShamsiDate;
            if (dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].DefaultView.Count == 0)
            {
                return -1;
            }

            return Conversions.ToInteger(dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].DefaultView[0]["DayType"]);
        }
        // این تابع مشخص می کند یک روز مشخص در تقویم روز تعطیل رسمی می باشد یا نه
        private bool IsHoliday(string DayNo, string MonthNo)
        {
            dsProductionPlanning.Tables["Tbl_HoliDays"].DefaultView.RowFilter = "DayNo=" + DayNo + " And MonthNo=" + MonthNo;
            if (dsProductionPlanning.Tables["Tbl_HoliDays"].DefaultView.Count == 0)
            {
                return false;
            }

            return true;
        }

        private string GetAccessibleTime()
        {
            var mAccessibleTimes = new TimeSpan(0, 0, 0, 0, 0);
            var loopTo = (short)(dgShifts.Rows.Count - 1);
            for (J = 0; J <= loopTo; J++)
            {
                for (int I = 0, loopTo1 = dgShiftTimeLine.Columns.Count - 1; I <= loopTo1; I++)
                {
                    if (!dgShiftTimeLine.Rows[0].Cells[I].Style.BackColor.Equals(Color.White))
                    {
                        mAccessibleTimes += TimeSpan.Parse("00:05");
                    }
                }
            }

            if (mAccessibleTimes > TimeSpan.Parse("1.00:00"))
            {
                return "-1";
            }
            else if (mAccessibleTimes == TimeSpan.Parse("1.00:00"))
            {
                return "23:59";
            }
            else
            {
                string mHour = Conversions.ToString(Interaction.IIf(mAccessibleTimes.Hours < 10, "0" + mAccessibleTimes.Hours.ToString(), mAccessibleTimes.Hours.ToString()));
                string mMinute = Conversions.ToString(Interaction.IIf(mAccessibleTimes.Minutes < 10, "0" + mAccessibleTimes.Minutes.ToString(), mAccessibleTimes.Minutes.ToString()));
                return mHour + ":" + mMinute;
            }
        }

        private bool CheckStartTimesValidation(string ShamsiDate)
        {
            var dvShifts = dsProductionPlanning.Tables["Tbl_CalendarParticularShifts"].DefaultView;
            var mFirstShiftStart = TimeSpan.Parse(dvShifts[0]["ShiftStart"].ToString());
            foreach (DataRowView drv in dvShifts)
            {
                if (drv.Row.RowState != DataRowState.Deleted)
                {
                    var mStart = TimeSpan.Parse(Conversions.ToString(drv["ShiftStart"]));
                    var mDuration = TimeSpan.Parse(Conversions.ToString(drv["ShiftDuration"])) + TimeSpan.Parse(Conversions.ToString(drv["ShiftExtraTime"]));
                    var mEnd = mStart + mDuration;
                    var mDR = dsProductionPlanning.Tables["Tbl_CalendarParticularShifts"].Select("ShamsiDate = " + ShamsiDate + " And ShiftNo > " + drv["ShiftNo"].ToString());
                    foreach (DataRow r in mDR)
                    {
                        if (r.RowState != DataRowState.Deleted)
                        {
                            if (mStart.Equals(TimeSpan.Parse(r["ShiftStart"].ToString())))
                            {
                                // اگر شروع دو شیفت باهم برابر باشد
                                return false;
                            }
                            else if (TimeSpan.Parse(r["ShiftStart"].ToString()) <= mEnd && TimeSpan.Parse(r["ShiftStart"].ToString()) > mFirstShiftStart)
                            {
                                // اگر شروع شیفت فعلی قبل یا مساوی، اتمام شیفت قبلی باشد
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        private void Create_Or_Edit_ParticulareWeekDayShift(string mShiftNo)
        {
            string ShamsiDate = Constants.vbNullString;
            for (int I = 0, loopTo = dgCalenderDays.ColumnCount - 1; I <= loopTo; I++)
            {
                if (dgCalenderDays.Rows[CurrentRow].Cells[I].Value is object && !dgCalenderDays.Rows[CurrentRow].Cells[I].Value.ToString().Equals(""))
                {
                    ShamsiDate = GetDayShamsiDate(dgCalenderDays.Rows[CurrentRow].Cells[I].Value.ToString().Split(Conversions.ToChar(mParticularDayDelimiter))[0].Trim(), dgCalenderDays.Tag.ToString());
                    break;
                }
            }

            short mDayNo = Module1.GetDayNo(ShamsiDate);
            string mFirstShamsiDate = txtYear.Text + "0101";
            while (true) // یافتن اولین روز از {نوع روز هفتۀ} انتخاب شده
            {
                if (Module1.GetDayNo(mFirstShamsiDate).Equals(mDayNo))
                {
                    break;
                }

                mFirstShamsiDate = FarsiDateFunctions.AddToDate(mFirstShamsiDate, "00000001");
            }

            ShamsiDate = mFirstShamsiDate;
            while (true)
            {
                // ثبت مشخصات شیفت روز خاص
                var mDR = dsProductionPlanning.Tables["Tbl_CalendarParticularShifts"].Select("ShamsiDate=" + ShamsiDate + " And ShiftNo = " + mShiftNo);
                if (mDR.Length > 0 && mDR[0].RowState != DataRowState.Deleted)
                {
                    if (rbActive.Checked) // در صورتیکه روز کاری باشد
                    {
                        EditShiftRow(mDR[0], txtShiftStart.Text, txtShiftDuration.Text, txtShiftExtraTime.Text);
                    }
                    else // در صورتیکه روز تعطیل باشد
                    {
                        EditShiftRow(mDR[0], "00:00", "00:00", "00:00");
                    }
                }
                else
                {
                    mDR = new DataRow[1];
                    mDR[0] = dsProductionPlanning.Tables["Tbl_CalendarParticularShifts"].NewRow();
                    mDR[0]["CalendarCode"] = mCalendarCode;
                    mDR[0]["ShamsiDate"] = ShamsiDate;
                    mDR[0]["ShiftNo"] = mShiftNo;
                    if (rbActive.Checked)
                    {
                        mDR[0]["ShiftStart"] = txtShiftStart.Text;
                        mDR[0]["ShiftDuration"] = txtShiftDuration.Text;
                        mDR[0]["ShiftExtraTime"] = txtShiftExtraTime.Text;
                    }
                    else
                    {
                        mDR[0]["ShiftStart"] = "00:00";
                        mDR[0]["ShiftDuration"] = "00:00";
                        mDR[0]["ShiftExtraTime"] = "00:00";
                    }

                    dsProductionPlanning.Tables["Tbl_CalendarParticularShifts"].Rows.Add(mDR[0]);
                }

                // ثبت مشخصات روز هفتۀ خاص
                mDR = dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].Select("ShamsiDate=" + ShamsiDate);
                if (mDR.Length > 0 && mDR[0].RowState != DataRowState.Deleted)
                {
                    if (rbActive.Checked) // در صورتیکه روز کاری باشد
                    {
                        mDR[0].BeginEdit();
                        mDR[0]["DayType"] = 2;
                        mDR[0]["AccessibleWorkTime"] = GetAccessibleTime();
                        mDR[0]["Description"] = txtDescription.Text;
                        mDR[0].EndEdit();
                    }
                    else // در صورتیکه روز تعطیل باشد
                    {
                        mDR[0].BeginEdit();
                        mDR[0]["DayType"] = 1;
                        mDR[0]["AccessibleWorkTime"] = "00:00";
                        mDR[0]["Description"] = txtDescription.Text;
                        mDR[0].EndEdit();
                    }
                }
                else
                {
                    mDR = new DataRow[1];
                    mDR[0] = dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].NewRow();
                    mDR[0]["CalendarCode"] = mCalendarCode;
                    mDR[0]["ShamsiDate"] = ShamsiDate;
                    if (rbActive.Checked) // در صورتیکه روز کاری باشد
                    {
                        mDR[0]["DayType"] = 2;
                        mDR[0]["AccessibleWorkTime"] = GetAccessibleTime();
                        mDR[0]["Description"] = txtDescription.Text;
                    }
                    else
                    {
                        mDR[0]["DayType"] = 1;
                        mDR[0]["AccessibleWorkTime"] = "00:00";
                        mDR[0]["Description"] = txtDescription.Text;
                    }

                    dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].Rows.Add(mDR[0]);
                }

                mDR = dsProductionPlanning.Tables["Tbl_ParticularShiftDownTimes"].Select("ShamsiDate=" + ShamsiDate + " And ShiftNo = " + mShiftNo);
                for (int I = mDR.Length - 1; I >= 0; I -= 1)
                    mDR[I].Delete();
                if (rbActive.Checked)
                {
                    AddShiftDownTimes("Tbl_ParticularShiftDownTimes", mShiftNo, "ShamsiDate", ShamsiDate);
                }

                ShamsiDate = FarsiDateFunctions.AddToDate(ShamsiDate, "00000007");
                if ((ShamsiDate.Substring(0, 4) ?? "") != (txtYear.Text ?? ""))
                {
                    break;
                }
            }
        }

        private bool CheckParticulareDayValidation(DataView dvShifts, string ShamsiDate)
        {
            int mIsParticulare = ParticularDay(ShamsiDate);
            int mShiftCount = dgShifts.RowCount;
            for (int I = 0, loopTo = mShiftCount - 1; I <= loopTo; I++)
            {
                string mShiftNo = dgShifts.Rows[I].Cells[0].Value.ToString();
                var mDR = dsProductionPlanning.Tables["Tbl_CalendarParticularShifts"].Select("ShamsiDate = " + ShamsiDate + " And ShiftNo = " + mShiftNo);
                if (mDR.Length == 0)
                {
                    if (rbActive.Checked) // در صورتیکه روز کاری باشد
                    {
                        MessageBox.Show("مشخصات شیفت {" + mShiftNo + "} را به روزآوری نمایید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        return false;
                    }
                    else // در صورتیکه روز تعطیل باشد
                    {
                        mDR = new DataRow[1];
                        mDR[0] = dsProductionPlanning.Tables["Tbl_CalendarParticularShifts"].NewRow();
                        mDR[0]["CalendarCode"] = mCalendarCode;
                        mDR[0]["ShamsiDate"] = ShamsiDate;
                        mDR[0]["ShiftNo"] = mShiftNo;
                        mDR[0]["ShiftStart"] = "00:00";
                        mDR[0]["ShiftDuration"] = "00:00";
                        mDR[0]["ShiftExtraTime"] = "00:00";
                        dsProductionPlanning.Tables["Tbl_CalendarParticularShifts"].Rows.Add(mDR[0]);
                        mDR = dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].Select("ShamsiDate = " + ShamsiDate);
                        if (mDR.Length == 0)
                        {
                            mDR = new DataRow[1];
                            mDR[0] = dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].NewRow();
                            mDR[0]["CalendarCode"] = mCalendarCode;
                            mDR[0]["ShamsiDate"] = ShamsiDate;
                            mDR[0]["DayType"] = 1;
                            mDR[0]["AccessibleWorkTime"] = "00:00";
                            mDR[0]["Description"] = txtDescription.Text;
                            dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].Rows.Add(mDR[0]);
                        }
                        else
                        {
                            mDR[0].BeginEdit();
                            mDR[0]["DayType"] = 1;
                            mDR[0]["AccessibleWorkTime"] = "00:00";
                            mDR[0]["Description"] = txtDescription.Text;
                            mDR[0].BeginEdit();
                        }
                    }
                }
                else
                {
                    if (rbHoliday.Checked)
                    {
                        mDR[0].BeginEdit();
                        mDR[0]["ShiftStart"] = "00:00";
                        mDR[0]["ShiftDuration"] = "00:00";
                        mDR[0]["ShiftExtraTime"] = "00:00";
                        mDR[0].EndEdit();
                    }

                    mDR = dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].Select("ShamsiDate = " + ShamsiDate);
                    if (mDR.Length == 0)
                    {
                        mDR = new DataRow[1];
                        mDR[0] = dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].NewRow();
                        mDR[0]["CalendarCode"] = mCalendarCode;
                        mDR[0]["ShamsiDate"] = ShamsiDate;
                        if (rbActive.Checked)
                        {
                            mDR[0]["DayType"] = 2;
                            mDR[0]["AccessibleWorkTime"] = GetAccessibleTime();
                        }
                        else
                        {
                            mDR[0]["DayType"] = 1;
                            mDR[0]["AccessibleWorkTime"] = "00:00";
                        }

                        mDR[0]["Description"] = txtDescription.Text;
                        dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].Rows.Add(mDR[0]);
                    }
                    else
                    {
                        mDR[0].BeginEdit();
                        if (rbActive.Checked)
                        {
                            mDR[0]["DayType"] = 2;
                            mDR[0]["AccessibleWorkTime"] = GetAccessibleTime();
                        }
                        else
                        {
                            mDR[0]["DayType"] = 1;
                            mDR[0]["AccessibleWorkTime"] = "00:00";
                        }

                        mDR[0]["Description"] = txtDescription.Text;
                        mDR[0].BeginEdit();
                    }
                }
            }

            dvShifts.RowFilter = "ShamsiDate = " + ShamsiDate;
            dvShifts.Sort = "ShiftNo";
            if (dgShifts.Rows.Count > 1 && mIsParticulare == 2)
            {
                if (!CheckStartTimesValidation(ShamsiDate))
                {
                    MessageBox.Show("در زمان شروع شیفتها تداخل وجود دارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return false;
                }
            }

            if (mIsParticulare == 2)
            {
                var mAccessibleTimes = new TimeSpan(0, 0, 0, 0, 0);
                foreach (DataRowView drv in dvShifts)
                {
                    if (drv.Row.RowState != DataRowState.Deleted)
                    {
                        mAccessibleTimes += TimeSpan.Parse(drv["ShiftDuration"].ToString()) + TimeSpan.Parse(drv["ShiftExtraTime"].ToString());
                    }
                }

                if (mAccessibleTimes > TimeSpan.Parse("1.00:00"))
                {
                    MessageBox.Show("زمان در دسترس شیفت(ها) غیر مجاز می باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return false;
                }
            }

            return true;
        }

        private void SaveChanges()
        {
            DataSet dsChanges;
            dsChanges = dsProductionPlanning.GetChanges();
            if (dsChanges is null || dsChanges.HasErrors)
            {
                dsProductionPlanning.RejectChanges();
            }
            else
            {
                if (dgShifts.Tag.ToString().Equals("2"))
                {
                    daCalendarDays.Update(dsChanges, "Tbl_CalendarDays");
                    mdaDaysDownTimes.Update(dsChanges, "Tbl_CalendarDaysDownTimes");
                    try
                    {
                        daCalendarParticularShifts.Update(dsChanges, "Tbl_CalendarParticularShifts");
                        daCalendarParticularDays.Update(dsChanges, "Tbl_CalendarParticularDays");
                        mdaParticulareShiftDownTimes.Update(dsChanges, "Tbl_ParticularShiftDownTimes");
                    }
                    catch
                    {
                    }
                }
                else
                {
                    try
                    {
                        daCalendarParticularShifts.Update(dsChanges, "Tbl_CalendarParticularShifts");
                        daCalendarParticularDays.Update(dsChanges, "Tbl_CalendarParticularDays");
                        mdaParticulareShiftDownTimes.Update(dsChanges, "Tbl_ParticularShiftDownTimes");
                    }
                    catch
                    {
                    }
                }

                dsProductionPlanning.AcceptChanges();
            }
        }

        private bool SetShiftGridColumns(string mShiftStart, TimeSpan mShiftDuration, ref int mColCounter)
        {
            var mDayEnd = TimeSpan.Parse("23:59:59") + TimeSpan.Parse("00:00:01");
            string mTimeSlice = "00:05";
            var mAddedTimeSlice = new TimeSpan();
            DataGridViewColumn mcol = null;
            int mColWidth = 40;
            try
            {
                mcol = new DataGridViewColumn(new DataGridViewTextBoxCell());
                mcol.HeaderCell.Style.Font = new Font("Arial", 7f, FontStyle.Regular, GraphicsUnit.Point);
                mcol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                mColCounter += 1;
                mcol.Name = "colTime" + mColCounter.ToString();
                mcol.HeaderText = mShiftStart;
                mcol.Width = mColWidth;
                dgShiftTimeLine.Columns.Add(mcol);
                if (mShiftDuration > TimeSpan.Parse("00:00"))
                {
                    while (true)
                    {
                        mAddedTimeSlice += TimeSpan.Parse(mTimeSlice);
                        if (TimeSpan.Parse(mShiftStart) + TimeSpan.Parse(mTimeSlice) >= mDayEnd)
                        {
                            mShiftStart = (TimeSpan.Parse(mShiftStart) + TimeSpan.Parse(mTimeSlice) - mDayEnd).ToString().Substring(0, 5);
                        }
                        else
                        {
                            mShiftStart = (TimeSpan.Parse(mShiftStart) + TimeSpan.Parse(mTimeSlice)).ToString().Substring(0, 5);
                        }

                        if (mAddedTimeSlice > mShiftDuration)
                        {
                            if (mShiftStart.Equals("00:00"))
                            {
                                mShiftStart = (TimeSpan.Parse("1.00:00:00") - (mAddedTimeSlice - mShiftDuration)).ToString().Substring(0, 5);
                            }
                            else
                            {
                                mShiftStart = (TimeSpan.Parse(mShiftStart) - (mAddedTimeSlice - mShiftDuration)).ToString().Substring(0, 5);
                            }
                        }

                        mcol = new DataGridViewColumn(new DataGridViewTextBoxCell());
                        mcol.HeaderCell.Style.Font = new Font("Arial", 7f, FontStyle.Regular, GraphicsUnit.Point);
                        mcol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        mColCounter += 1;
                        mcol.Name = "colTime" + mColCounter.ToString();
                        if (mShiftStart.Equals("00:00"))
                        {
                            mcol.HeaderText = "24:00";
                        }
                        else
                        {
                            mcol.HeaderText = mShiftStart;
                        }

                        mcol.Width = mColWidth;
                        dgShiftTimeLine.Columns.Add(mcol);
                        if (mAddedTimeSlice >= mShiftDuration)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveError(Name + ".SetShiftGridColumns", ex.Message);
                MessageBox.Show("نمایش شیفت با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                return false;
            }

            return true;
        }

        private void EditShiftRow(DataRow mRow, string mStart, string mDuration, string mExtra, params string[] OverFeildName)
        {
            mRow.BeginEdit();
            mRow["ShiftStart"] = mStart;
            mRow["ShiftDuration"] = mDuration;
            mRow["ShiftExtraTime"] = mExtra;
            if (OverFeildName.Length > 1)
            {
                for (int K = 0, loopTo = OverFeildName.Length - 1; K <= loopTo; K += 2)
                    mRow[OverFeildName[K]] = OverFeildName[K + 1];
            }

            mRow.EndEdit();
        }

        private void AddShiftDownTimes(string mTableName, string mShiftNo, params string[] OverFeildName)
        {
            for (int I = 0, loopTo = dgShiftTimeLine.Columns.Count - 1; I <= loopTo; I++)
            {
                if (dgShiftTimeLine.Rows[0].Cells[I].Style.BackColor.Equals(Color.White))
                {
                    string mDTStart = dgShiftTimeLine.Columns[I].HeaderText;
                    int mEndCellIndex = I;
                    if (I > 0 && dgShiftTimeLine.Columns[I - 1] is object)
                    {
                        mDTStart = dgShiftTimeLine.Columns[I - 1].HeaderText;
                        mEndCellIndex = I - 1;
                    }

                    for (int J = I, loopTo1 = dgShiftTimeLine.Columns.Count - 1; J <= loopTo1; J++)
                    {
                        if (dgShiftTimeLine.Rows[0].Cells[J].Style.BackColor == Color.White)
                        {
                            mEndCellIndex = J;
                        }
                        else
                        {
                            break;
                        }
                    }

                    string mDTEnd = dgShiftTimeLine.Columns[mEndCellIndex].HeaderText;
                    var mDTNewRow = dsProductionPlanning.Tables[mTableName].NewRow();
                    mDTNewRow["CalendarCode"] = mCalendarCode;
                    mDTNewRow["ShiftNo"] = mShiftNo;
                    mDTNewRow["DownTimeStart"] = mDTStart;
                    mDTNewRow["DownTimeEnd"] = mDTEnd;
                    if (OverFeildName.Length > 1)
                    {
                        for (int K = 0, loopTo2 = OverFeildName.Length - 1; K <= loopTo2; K += 2)
                            mDTNewRow[OverFeildName[K]] = OverFeildName[K + 1];
                    }

                    dsProductionPlanning.Tables[mTableName].Rows.Add(mDTNewRow);
                    I = mEndCellIndex + 1;
                }
            }
        }

        private void FillShiftDownTimes(DataRow[] mDR)
        {
            foreach (DataRow dr in mDR)
            {
                foreach (DataGridViewCell dgCell in dgShiftTimeLine.Rows[0].Cells)
                {
                    if (dr["DownTimeStart"].ToString().Equals(dgShiftTimeLine.Columns[dgCell.ColumnIndex].HeaderText))
                    {
                        if (dgCell.ColumnIndex + 1 >= 0 && dgShiftTimeLine.Columns[dgCell.ColumnIndex + 1] is object)
                        {
                            dgShiftTimeLine.Rows[0].Cells[dgCell.ColumnIndex + 1].Style.BackColor = Color.White;
                            for (int I = dgCell.ColumnIndex + 1, loopTo = dgShiftTimeLine.Columns.Count - 1; I <= loopTo; I++)
                            {
                                if (Operators.CompareString(dr["DownTimeEnd"].ToString(), dgShiftTimeLine.Columns[I].HeaderText, false) < 0)
                                {
                                    if (I > 0 && dgShiftTimeLine.Columns[I - 1] is object)
                                    {
                                        if (Operators.CompareString(dr["DownTimeEnd"].ToString(), dgShiftTimeLine.Columns[I - 1].HeaderText, false) > 0)
                                        {
                                            dgShiftTimeLine.Rows[0].Cells[I].Style.BackColor = Color.White;
                                        }
                                    }

                                    break;
                                }

                                dgShiftTimeLine.Rows[0].Cells[I].Style.BackColor = Color.White;
                            }

                            break;
                        }
                    }
                    else if (Operators.CompareString(dr["DownTimeStart"].ToString(), dgShiftTimeLine.Columns[dgCell.ColumnIndex].HeaderText, false) < 0)
                    {
                        if (dgCell.ColumnIndex - 1 >= 0 && dgShiftTimeLine.Columns[dgCell.ColumnIndex - 1] is object)
                        {
                            if (Operators.CompareString(dr["DownTimeStart"].ToString(), dgShiftTimeLine.Columns[dgCell.ColumnIndex - 1].HeaderText, false) > 0)
                            {
                                dgCell.Style.BackColor = Color.White;
                                for (int I = dgCell.ColumnIndex + 1, loopTo1 = dgShiftTimeLine.Columns.Count - 1; I <= loopTo1; I++)
                                {
                                    if (Operators.CompareString(dr["DownTimeEnd"].ToString(), dgShiftTimeLine.Columns[I].HeaderText, false) < 0)
                                    {
                                        if (I > 0 && dgShiftTimeLine.Columns[I - 1] is object)
                                        {
                                            if (Operators.CompareString(dr["DownTimeEnd"].ToString(), dgShiftTimeLine.Columns[I - 1].HeaderText, false) > 0)
                                            {
                                                dgShiftTimeLine.Rows[0].Cells[I].Style.BackColor = Color.White;
                                            }
                                        }

                                        break;
                                    }

                                    dgShiftTimeLine.Rows[0].Cells[I].Style.BackColor = Color.White;
                                }

                                break;
                            }
                        }
                    }
                }
            }
        }

        private void SetShiftItemEnabled(bool mValue)
        {
            txtShiftStart.Enabled = mValue;
            txtShiftDuration.Enabled = mValue;
            txtShiftExtraTime.Enabled = mValue;
            chkSetWorkTime.Enabled = mValue;
            chkSetDownTime.Enabled = mValue;
            cmdSaveShift.Enabled = mValue;
            cmdShowShiftTimeLine.Enabled = mValue;
            dgShiftTimeLine.Enabled = mValue;
        }

        private string GetDayShamsiDate(string mDayNo, string mMonthNo)
        {
            string mShamsiDate = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(txtYear.Value.ToString(), Interaction.IIf(Conversions.ToInteger(mMonthNo) < 9, "0" + mMonthNo, mMonthNo)), Interaction.IIf(Conversions.ToInteger(mDayNo) < 10, "0" + mDayNo, mDayNo)));

            return mShamsiDate;
        }
    }
}