using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using ProductionPlanning.Primary_Definitions.Calendar_Forms;
using ProductionPlanning;

namespace ProductionPlanning
{
    public partial class frmCalendar
    {
        public frmCalendar()
        {
            InitializeComponent();
            _cmdExit.Name = "cmdExit";
            _cmdSave.Name = "cmdSave";
            _cmdCalenderDays.Name = "cmdCalenderDays";
            _cmdDelete.Name = "cmdDelete";
            _txtTitle.Name = "txtTitle";
            _txtShiftCount.Name = "txtShiftCount";
            _dgShifts.Name = "dgShifts";
            _cmdShowShiftTimeLine.Name = "cmdShowShiftTimeLine";
            _txtShiftExtraTime.Name = "txtShiftExtraTime";
            _txtShiftDuration.Name = "txtShiftDuration";
            _txtShiftStart.Name = "txtShiftStart";
            _chkSetWorkTime.Name = "chkSetWorkTime";
            _chkSetDownTime.Name = "chkSetDownTime";
            _cmdSaveShift.Name = "cmdSaveShift";
            _dgShiftTimeLine.Name = "dgShiftTimeLine";
        }

        private frmRecordsLists mListForm;
        private SqlDataAdapter mdaCalendar = new SqlDataAdapter();
        private SqlDataAdapter mdaCalendarShifts = new SqlDataAdapter();
        private SqlDataAdapter mdaShiftDownTimes = new SqlDataAdapter();
        private SqlDataAdapter mdaCalendarDays = new SqlDataAdapter();
        private SqlDataAdapter mdaParticulareShifts = new SqlDataAdapter();
        private SqlDataAdapter mdaParticulareDays = new SqlDataAdapter();
        private SqlDataAdapter mdaDaysDownTimes = new SqlDataAdapter();
        private DataRow mCurrentRow; // برای نگهداری رکورد جاری
        private DataView dvShifts;
        private short I, J, ShiftCount;
        private bool LoadMode = false;

        public frmRecordsLists ListForm
        {
            get
            {
                return mListForm;
            }

            set
            {
                mListForm = value;
            }
        }

        public DataSet dsProductionPlanning
        {
            get
            {
                return ListForm.dsProductionPlanning;
            }
        }

        private void Form_Activated(object sender, EventArgs e)
        {
            if (cmdDelete.Visible)
            {
                cmdCalenderDays.Enabled = true;
                My.MyProject.Forms.frmCalendarDays.cmdSave.Enabled = false;
            }
        }

        private void frmCalender_FormClosing(object sender, FormClosingEventArgs e)
        {
            dsProductionPlanning.RejectChanges();
            dsProductionPlanning.Relations.Clear();
            for (I = 5; I >= 1; I += -1)
            {
                dsProductionPlanning.Tables[I].Constraints.Clear();
                dsProductionPlanning.Tables[I].Dispose();
                dsProductionPlanning.Tables.RemoveAt(I);
            }

            mdaCalendarDays.Dispose();
            mdaCalendarShifts.Dispose();
            mdaCalendar.Dispose();
            mCurrentRow = null;
        }

        private void frmCalendar_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdSave, 6);
            Module1.SetButtonsImage(cmdDelete, 9);
            Module1.SetButtonsImage(cmdExit, 5);
            FormLoad();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtShiftCount_ValueChanged(object sender, EventArgs e)
        {
            if (!LoadMode)
            {
                if (ListForm.FormMode != (int)Module1.FormModeEnum.DELETE_MODE)
                {
                    DataRow[] mDR = null;
                    txtShiftStart.Text = "00:00";
                    txtShiftDuration.Text = "00:00";
                    txtShiftExtraTime.Text = "00:00";
                    dgShiftTimeLine.Columns.Clear();
                    gbShift.Enabled = false;
                    cmdCalenderDays.Enabled = false;
                    cmdSave.Enabled = true;
                    if (ShiftCount < txtShiftCount.Value)
                    {
                        dgShifts.Rows.Add(txtShiftCount.Value, "به روزآوری مشخصات شیفت " + txtShiftCount.Value.ToString());
                        ShiftCount = (short)Math.Round(txtShiftCount.Value);
                        mDR = dsProductionPlanning.Tables["Tbl_CalendarShifts"].Select("ShiftNo = " + ShiftCount.ToString());
                        if (mDR.Length == 0)
                        {
                            string mCalendarCode = "-1";
                            if (ListForm.FormMode == (int)Module1.FormModeEnum.EDIT_MODE)
                            {
                                mCalendarCode = mCurrentRow["CalendarCode"].ToString();
                            }

                            mDR = new DataRow[1];
                            mDR[0] = dsProductionPlanning.Tables["Tbl_CalendarShifts"].NewRow();
                            mDR[0]["CalendarCode"] = mCalendarCode;
                            mDR[0]["ShiftNo"] = ShiftCount;
                            mDR[0]["ShiftStart"] = "00:00";
                            mDR[0]["ShiftDuration"] = "00:00";
                            mDR[0]["ShiftExtraTime"] = "00:00";
                            dsProductionPlanning.Tables["Tbl_CalendarShifts"].Rows.Add(mDR[0]);
                            mDR = dsProductionPlanning.Tables["Tbl_CalendarShiftDownTimes"].Select("ShiftNo = " + ShiftCount.ToString());
                            for (int I = mDR.Length - 1; I >= 0; I -= 1)
                                mDR[I].Delete();
                        }
                    }
                    else if (ShiftCount > txtShiftCount.Value)
                    {
                        // mDR = dsProductionPlanning.Tables("Tbl_CalendarShiftDownTimes").Select("ShiftNo = " & ShiftCount.ToString())

                        // For I As Integer = mDR.Length - 1 To 0 Step -1
                        // dsProductionPlanning.Tables("Tbl_CalendarShiftDownTimes").Rows.Remove(mDR(I))
                        // Next I

                        // mDR = dsProductionPlanning.Tables("Tbl_CalendarShifts").Select("ShiftNo = " & ShiftCount.ToString())

                        // If mDR.Length > 0 Then
                        // mDR(0).Delete()
                        // End If

                        dgShifts.Rows.RemoveAt(dgShifts.Rows.Count - 1);
                        ShiftCount = (short)Math.Round(txtShiftCount.Value);
                    }
                }
            }
        }

        private void txtShiftStart_ValueChanged(object sender, EventArgs e)
        {
            cmdShowShiftTimeLine.Enabled = true;
            cmdSaveShift.Enabled = false;
        }

        private void dgShifts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtShiftStart.Text = "00:00";
            txtShiftDuration.Text = "00:00";
            txtShiftExtraTime.Text = "00:00";
            dgShiftTimeLine.Columns.Clear();
           
            string mShiftNo = dgShifts.CurrentRow.Cells[0].Value.ToString();
            var mDR = dsProductionPlanning.Tables["Tbl_CalendarShifts"].Select("ShiftNo = " + mShiftNo);
            if (mDR.Length > 0)
            {
                txtShiftStart.Text = mDR[0]["ShiftStart"].ToString();
                txtShiftDuration.Text = mDR[0]["ShiftDuration"].ToString();
                txtShiftExtraTime.Text = mDR[0]["ShiftExtraTime"].ToString();
                mDR = dsProductionPlanning.Tables["Tbl_CalendarShiftDownTimes"].Select("ShiftNo = " + mShiftNo);
                //if (mDR.Length > 0)
                //{
                cmdShowShiftTimeLine_Click(sender, new EventArgs());
                if (dgShiftTimeLine.Columns.Count > 0)
                {
                    foreach (DataGridViewCell dgCell in dgShiftTimeLine.Rows[0].Cells)
                        dgCell.Style.BackColor = Color.Green;
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
                                    // Else
                                    // dgCell.Style.BackColor = Color.White
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
                                    // Else
                                    // dgCell.Style.BackColor = Color.Green
                                }
                            }
                        }
                    }
                }
                //}
            }

            if (e.ColumnIndex == 1)
            {
                gbShift.Enabled = true;
                gbShift.BackColor = Color.LightSteelBlue;
                gb_CalendarHeader.Enabled = false;
                gb_CalendarHeader.BackColor = Color.LightGray;

                //_dgShifts.Enabled = false;
                cmdSaveShift.Enabled = true;
                cmdSaveShift.BackColor = Color.LightYellow;
                cmdSave.Enabled = false;
                cmdCalenderDays.Enabled = false;
                cmdShowShiftTimeLine.Enabled = false;
            }
            else
            {
                cmdSave.Enabled = true;
                //cmdCalenderDays.Enabled = true;
                //gbShift.Enabled = false;
            }
        }

        private void cmdShowShiftTimeLine_Click(object sender, EventArgs e)
        {
            //if (txtShiftStart.Text.Equals("00:00") && txtShiftDuration.Text.Equals("00:00") && txtShiftExtraTime.Text.Equals("00:00") || !txtShiftStart.Text.Equals("00:00") && txtShiftDuration.Text.Equals("00:00") && txtShiftExtraTime.Text.Equals("00:00"))
            //{
            //    MessageBox.Show("مقادیر زمانی شیفت را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            //    txtShiftStart.Focus();
            //    return;
            //}

            if (dgShifts.SelectedRows.Count > 0)
            {
                TimeSpan mShiftDuration = default;
                if (CommonTool.CTimeSpan(txtShiftDuration.Text) + CommonTool.CTimeSpan(txtShiftExtraTime.Text) > CommonTool.CTimeSpan("1.00:00:00"))
                {
                    MessageBox.Show("زمانهای وارد شده برای شیفت اشتباه است", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return;
                }
                else if (CommonTool.CTimeSpan(txtShiftDuration.Text) + CommonTool.CTimeSpan(txtShiftExtraTime.Text) == CommonTool.CTimeSpan("1.00:00:00"))
                {
                    mShiftDuration = CommonTool.CTimeSpan("1.00:00:00");
                }
                else
                {
                    mShiftDuration = CommonTool.CTimeSpan(txtShiftDuration.Text) + CommonTool.CTimeSpan(txtShiftExtraTime.Text);
                }

                // Dim mColCounter As Integer = 0

                dgShiftTimeLine.Columns.Clear();
                if (SetShiftGridColumns(txtShiftStart.Text, mShiftDuration))
                {
                    dgShiftTimeLine.Rows.Add();
                    for (int I = 0, loopTo = dgShiftTimeLine.Rows[0].Cells.Count - 1; I <= loopTo; I++)
                        dgShiftTimeLine.Rows[0].Cells[I].Style.BackColor = Color.Green;
                    cmdShowShiftTimeLine.Enabled = false;
                    cmdSaveShift.Enabled = true;
                }
            }
        }

        private void cmdSaveShift_Click(object sender, EventArgs e)
        {
            if (ListForm.FormMode != (int)Module1.FormModeEnum.DELETE_MODE)
            {
                string mShiftNo = dgShifts.SelectedRows[0].Cells[0].Value.ToString();
                var mDR = dsProductionPlanning.Tables["Tbl_CalendarShifts"].Select("ShiftNo = " + mShiftNo);
                string mCalendarCode = "-1";
                if (mDR.Length > 0)
                {
                    mCalendarCode = Conversions.ToString(mDR[0]["CalendarCode"]);
                    mDR[0].BeginEdit();
                    mDR[0]["ShiftStart"] = txtShiftStart.Text;
                    mDR[0]["ShiftDuration"] = txtShiftDuration.Text;
                    mDR[0]["ShiftExtraTime"] = txtShiftExtraTime.Text;
                    mDR[0].EndEdit();
                }
                else
                {
                    mDR = new DataRow[1];
                    mDR[0] = dsProductionPlanning.Tables["Tbl_CalendarShifts"].NewRow();
                    if (ListForm.FormMode == (int)Module1.FormModeEnum.EDIT_MODE)
                    {
                        mCalendarCode = Conversions.ToString(mCurrentRow["CalendarCode"]);
                    }

                    mDR[0]["CalendarCode"] = mCalendarCode;
                    mDR[0]["ShiftNo"] = dgShifts.SelectedRows[0].Cells[0].Value;
                    mDR[0]["ShiftStart"] = txtShiftStart.Text;
                    mDR[0]["ShiftDuration"] = txtShiftDuration.Text;
                    mDR[0]["ShiftExtraTime"] = txtShiftExtraTime.Text;
                    dsProductionPlanning.Tables["Tbl_CalendarShifts"].Rows.Add(mDR[0]);
                }

                mDR = dsProductionPlanning.Tables["Tbl_CalendarShiftDownTimes"].Select("ShiftNo = " + mShiftNo);
                for (int I = mDR.Length - 1; I >= 0; I -= 1)
                    // dsProductionPlanning.Tables("Tbl_CalendarShiftDownTimes").Rows.Remove(mDR(I))
                    mDR[I].Delete();
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
                        var mDTNewRow = dsProductionPlanning.Tables["Tbl_CalendarShiftDownTimes"].NewRow();
                        mDTNewRow["CalendarCode"] = mCalendarCode;
                        mDTNewRow["ShiftNo"] = mShiftNo;
                        mDTNewRow["DownTimeStart"] = mDTStart;
                        mDTNewRow["DownTimeEnd"] = mDTEnd;
                        dsProductionPlanning.Tables["Tbl_CalendarShiftDownTimes"].Rows.Add(mDTNewRow);
                        I = mEndCellIndex + 1;
                    }
                }

                
                cmdSave.Enabled = true;

                gbShift.Enabled = false;
                gbShift.BackColor = Color.LightGray;
                gb_CalendarHeader.Enabled = true;
                gb_CalendarHeader.BackColor = Color.LightSteelBlue;
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

        private void dgMachineWorkTimes_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
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

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("تقویم کاری را حذف می کنید", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading, false) == DialogResult.Yes)
            {
                SqlTransaction trnDelete = null;
                try
                {
                    if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                        Module1.cnProductionPlanning.Open();
                    trnDelete = Module1.cnProductionPlanning.BeginTransaction();
                    mdaCalendar.DeleteCommand.Transaction = trnDelete;
                    mdaCalendarShifts.DeleteCommand.Transaction = trnDelete;
                    mdaCalendarDays.DeleteCommand.Transaction = trnDelete;
                    mdaShiftDownTimes.DeleteCommand.Transaction = trnDelete;
                    mdaDaysDownTimes.DeleteCommand.Transaction = trnDelete;
                    mdaParticulareShifts.DeleteCommand.Transaction = trnDelete;
                    mdaParticulareDays.DeleteCommand.Transaction = trnDelete;
                    var dvDelete = dsProductionPlanning.Tables["Tbl_CalendarDaysDownTimes"].DefaultView;
                    for (I = (short)(dvDelete.Count - 1); I >= 0; I += -1)
                        dsProductionPlanning.Tables["Tbl_CalendarDaysDownTimes"].Rows.Remove(dvDelete[I].Row);
                    dvDelete = dsProductionPlanning.Tables["Tbl_CalendarShiftDownTimes"].DefaultView;
                    for (I = (short)(dvDelete.Count - 1); I >= 0; I += -1)
                        dsProductionPlanning.Tables["Tbl_CalendarShiftDownTimes"].Rows.Remove(dvDelete[I].Row);
                    dvDelete = dsProductionPlanning.Tables["Tbl_ParticularShiftDownTimes"].DefaultView;
                    for (I = (short)(dvDelete.Count - 1); I >= 0; I += -1)
                        dsProductionPlanning.Tables["Tbl_ParticularShiftDownTimes"].Rows.Remove(dvDelete[I].Row);
                    dvDelete = dsProductionPlanning.Tables["Tbl_CalendarParticularDays"].DefaultView;
                    for (I = (short)(dvDelete.Count - 1); I >= 0; I += -1)
                        dvDelete[I].Row.Delete();
                    dvDelete = dsProductionPlanning.Tables["Tbl_CalendarParticularShifts"].DefaultView;
                    for (I = (short)(dvDelete.Count - 1); I >= 0; I += -1)
                        dvDelete[I].Row.Delete();
                    dvDelete = dsProductionPlanning.Tables["Tbl_CalendarDays"].DefaultView;
                    for (I = (short)(dvDelete.Count - 1); I >= 0; I += -1)
                        dvDelete[I].Delete();
                    dvDelete = dsProductionPlanning.Tables["Tbl_CalendarShifts"].DefaultView;
                    for (I = (short)(dvShifts.Count - 1); I >= 0; I += -1)
                        dvShifts[I].Delete();
                    mCurrentRow.Delete();
                    SaveChanges();
                    trnDelete.Commit();
                    Close();
                }
                catch (InvalidConstraintException ObjCnstEx)
                {
                    Logger.LogException("cmdDelete_Click", ObjCnstEx);
                    dsProductionPlanning.RejectChanges();
                    trnDelete.Rollback();
                    MessageBox.Show("اشکال در حذف رکورد، برای حذف رکورد باید رکورد(های) مرتبط با آن حذف شوند", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                }
                catch (Exception objEx)
                {
                    dsProductionPlanning.RejectChanges();
                    trnDelete.Rollback();
                    Logger.SaveError(Name + ".cmdDelete_Click", objEx.Message + Constants.vbCrLf + objEx.ToString());
                    string ErrorMsg = "اشکال در حذف رکورد، رکورد حذف نشد";
                    MessageBox.Show(ErrorMsg, Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                }
                finally
                {
                    trnDelete.Dispose();
                    if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                        Module1.cnProductionPlanning.Close();
                }
            }
        }

        private void cmdCalenderDays_Click(object sender, EventArgs e)
        {
            {
                var withBlock = My.MyProject.Forms.frmCalendarDays;
                withBlock.dsProductionPlanning = dsProductionPlanning;
                withBlock.CalendarCode = Conversions.ToString(mCurrentRow["CalendarCode"]);
                withBlock.lblCalendarTitle.Text = txtTitle.Text;
                withBlock.ShowDialog();
                withBlock.Dispose();
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!FormValidation())
            {
                return;
            }

            //if (!CheckAccessibleTimeValidation())
            //{
            //    MessageBox.Show("زمان در دسترس شیفتها غیر مجاز می باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            //    return;
            //}

            //if (txtShiftCount.Value > 1m)
            //{
            //    if (!CheckStartTimesValidation())
            //    {
            //        return;
            //    }
            //}

            if (dsProductionPlanning.Tables["Tbl_CalendarShifts"].Rows.Count > txtShiftCount.Value)
            {
                var mDR = dsProductionPlanning.Tables["Tbl_CalendarShiftDownTimes"].Select("ShiftNo > " + txtShiftCount.Value.ToString());
                for (int I = mDR.Length - 1; I >= 0; I -= 1)
                    mDR[I].Delete();
                mDR = dsProductionPlanning.Tables["Tbl_CalendarShifts"].Select("ShiftNo > " + txtShiftCount.Value.ToString());
                for (int I = mDR.Length - 1; I >= 0; I -= 1)
                    mDR[I].Delete();
            }

            switch (ListForm.FormMode)
            {
                case (int)Module1.FormModeEnum.INSERT_MODE:
                    {
                        DataRow drInsert;
                        SqlTransaction trnInsert = null;
                        string mCode = Module1.GetNewCode("Tbl_Calendar", "CalendarCode").ToString();
                        try
                        {
                            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                                Module1.cnProductionPlanning.Open();
                            trnInsert = Module1.cnProductionPlanning.BeginTransaction();
                            mdaCalendar.InsertCommand.Transaction = trnInsert;
                            mdaCalendarShifts.InsertCommand.Transaction = trnInsert;
                            mdaCalendarShifts.UpdateCommand.Transaction = trnInsert;
                            mdaCalendarShifts.DeleteCommand.Transaction = trnInsert;
                            mdaCalendarDays.InsertCommand.Transaction = trnInsert;
                            mdaCalendarDays.UpdateCommand.Transaction = trnInsert;
                            mdaCalendarDays.DeleteCommand.Transaction = trnInsert;
                            mdaShiftDownTimes.InsertCommand.Transaction = trnInsert;
                            mdaShiftDownTimes.UpdateCommand.Transaction = trnInsert;
                            mdaShiftDownTimes.DeleteCommand.Transaction = trnInsert;
                            mdaDaysDownTimes.InsertCommand.Transaction = trnInsert;
                            mdaDaysDownTimes.UpdateCommand.Transaction = trnInsert;
                            mdaDaysDownTimes.DeleteCommand.Transaction = trnInsert;

                            // اضافه کردن رکورد جدید به جدول در دیتاست
                            drInsert = dsProductionPlanning.Tables["Tbl_Calendar"].NewRow();
                            drInsert["CalendarCode"] = mCode;
                            drInsert["CalendarTitle"] = txtTitle.Text;
                            drInsert["ShiftCount"] = txtShiftCount.Value;
                            dsProductionPlanning.Tables["Tbl_Calendar"].Rows.Add(drInsert);
                            foreach (DataRow dr in dsProductionPlanning.Tables["Tbl_CalendarShifts"].Rows)
                            {
                                dr.BeginEdit();
                                dr["CalendarCode"] = mCode;
                                dr.EndEdit();
                            }

                            foreach (DataRow dr in dsProductionPlanning.Tables["Tbl_CalendarShiftDownTimes"].Rows)
                            {
                                dr.BeginEdit();
                                dr["CalendarCode"] = mCode;
                                dr.EndEdit();
                            }

                            // ثبت اطلاعات روزهای تقویم
                            AddNewDay(mCode);
                            SaveChanges();
                            trnInsert.Commit();
                            cmdCalenderDays.Enabled = true;
                            cmdExit.Text = "خروج";
                            mCurrentRow = dsProductionPlanning.Tables["Tbl_Calendar"].Select("CalendarCode = " + mCode)[0];
                            ListForm.FormMode = (int)Module1.FormModeEnum.EDIT_MODE;
                            MessageBox.Show("تقویم جدید ثبت شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);

                    }
                    catch (Exception objEx)
                        {
                            dsProductionPlanning.Tables["Tbl_Calendar"].RejectChanges();
                            trnInsert.Rollback();
                            Logger.LogException(Name + ".cmdSave_Click", objEx);
                            MessageBox.Show("ثبت تقویم جدید با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        }
                        finally
                        {
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                        }

                        break;
                    }

                case (int)Module1.FormModeEnum.EDIT_MODE:
                    {
                        SqlTransaction trnUpdate = null;
                        try
                        {
                            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                                Module1.cnProductionPlanning.Open();
                            trnUpdate = Module1.cnProductionPlanning.BeginTransaction();
                            mdaCalendar.UpdateCommand.Transaction = trnUpdate;
                            mdaCalendarShifts.InsertCommand.Transaction = trnUpdate;
                            mdaCalendarShifts.UpdateCommand.Transaction = trnUpdate;
                            mdaCalendarShifts.DeleteCommand.Transaction = trnUpdate;
                            mdaCalendarDays.InsertCommand.Transaction = trnUpdate;
                            mdaCalendarDays.UpdateCommand.Transaction = trnUpdate;
                            mdaCalendarDays.DeleteCommand.Transaction = trnUpdate;
                            mdaShiftDownTimes.InsertCommand.Transaction = trnUpdate;
                            mdaShiftDownTimes.UpdateCommand.Transaction = trnUpdate;
                            mdaShiftDownTimes.DeleteCommand.Transaction = trnUpdate;
                            mdaDaysDownTimes.InsertCommand.Transaction = trnUpdate;
                            mdaDaysDownTimes.UpdateCommand.Transaction = trnUpdate;
                            mdaDaysDownTimes.DeleteCommand.Transaction = trnUpdate;
                            mCurrentRow.BeginEdit();
                            mCurrentRow["CalendarTitle"] = txtTitle.Text;
                            mCurrentRow["ShiftCount"] = txtShiftCount.Value;
                            mCurrentRow.EndEdit();

                            // حذف رکوردهای روزهای تقویم قبل از ویرایش
                            var dvDays = dsProductionPlanning.Tables["Tbl_CalendarDays"].DefaultView;
                            for (I = (short)(dvDays.Count - 1); I >= 0; I += -1)
                                dvDays[I].Delete();
                            dvDays = dsProductionPlanning.Tables["Tbl_CalendarDaysDownTimes"].DefaultView;
                            for (I = (short)(dvDays.Count - 1); I >= 0; I += -1)
                                dsProductionPlanning.Tables["Tbl_CalendarDaysDownTimes"].Rows.Remove(dvDays[I].Row);
                            foreach (DataRow dr in dsProductionPlanning.Tables["Tbl_CalendarShifts"].Rows)
                            {
                                if (dr.RowState != DataRowState.Deleted)
                                {
                                    dr.BeginEdit();
                                    dr["CalendarCode"] = mCurrentRow["CalendarCode"];
                                    dr.EndEdit();
                                }
                            }

                            foreach (DataRow dr in dsProductionPlanning.Tables["Tbl_CalendarShiftDownTimes"].Rows)
                            {
                                if (dr.RowState != DataRowState.Deleted)
                                {
                                    dr.BeginEdit();
                                    dr["CalendarCode"] = mCurrentRow["CalendarCode"];
                                    dr.EndEdit();
                                }
                            }

                            // ثبت اطلاعات روزهای تقویم
                            AddNewDay(Conversions.ToString(mCurrentRow["CalendarCode"]));
                            SaveChanges();
                            trnUpdate.Commit();
                            cmdCalenderDays.Enabled = true;
                            MessageBox.Show("تغییرات تقویم ثبت شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);

                        //Close();
                    }
                    catch (Exception objEx)
                        {
                            mCurrentRow.CancelEdit();
                            trnUpdate.Rollback();
                            Logger.LogException(Name + ".cmdSave_Click", objEx);
                            MessageBox.Show("ثبت تغییرات تقویم با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        }
                        finally
                        {
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                        }

                        break;
                    }
            }
        }

        private void Controls_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Strings.Asc(e.KeyChar) == (int)Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void FormLoad()
        {
            LoadMode = true;
            try
            {
                CreateDataAdapterCommands();
                switch (ListForm.FormMode)
                {
                    case (int)Module1.FormModeEnum.INSERT_MODE:
                        {
                            txtTitle.Focus();
                            break;
                        }

                    case (int)Module1.FormModeEnum.EDIT_MODE:
                    case (int)Module1.FormModeEnum.DELETE_MODE:
                        {
                            mCurrentRow = null;
                            mCurrentRow = ListForm.GetRow();
                            txtTitle.Text = Conversions.ToString(mCurrentRow["CalendarTitle"]);
                            txtShiftCount.Text = Conversions.ToString(mCurrentRow["ShiftCount"]);
                            ShiftCount = (short)Conversions.ToInteger(mCurrentRow["ShiftCount"]);
                            dvShifts = dsProductionPlanning.Tables["Tbl_CalendarShifts"].DefaultView;
                            dgShifts.Rows.Clear();
                            var loopTo = (short)(dvShifts.Count - 1);
                            for (I = 0; I <= loopTo; I++)
                                dgShifts.Rows.Add(dvShifts[I][1], "به روزآوری مشخصات شیفت " + dvShifts[I][1].ToString());
                            cmdCalenderDays.Enabled = true;
                            switch (ListForm.FormMode) // در صورتیکه فرم برای ویرایش(اصلاح) رکورد جاری فراخوانی شده باشد این قسمت اجرا خواهد شد
                            {
                                case (int)Module1.FormModeEnum.EDIT_MODE:
                                    {
                                        txtTitle.Focus();
                                        break;
                                    }

                                case (int)Module1.FormModeEnum.DELETE_MODE: // در صورتیکه فرم برای حذف رکورد جاری فراخوانی شده باشد این قسمت اجرا خواهد شد
                                    {
                                        dgShifts.ReadOnly = true;
                                        cmdDelete.Focus();
                                        break;
                                    }
                            }

                            break;
                        }
                }

                LoadMode = false;
            }
            catch (Exception objEx)
            {
                MessageBox.Show("فراخوانی فرم با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(objEx.Message);
            }
        }

        private void CreateDataAdapterCommands()
        {
            // ---------- تنظیم دستورات مربوط به تقویم کاری ----------
            // ایجاد دستور اضافه کردن رکورد جدید در جدول
            mdaCalendar.InsertCommand = new SqlCommand("Insert Into Tbl_Calendar(CalendarCode,CalendarTitle,ShiftCount) Values(@CalendarCode,@CalendarTitle,@ShiftCount)", Module1.cnProductionPlanning);
            {
                var withBlock = mdaCalendar.InsertCommand;
                withBlock.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode");
                withBlock.Parameters.Add("@CalendarTitle", SqlDbType.VarChar, 255, "CalendarTitle");
                withBlock.Parameters.Add("@ShiftCount", SqlDbType.TinyInt, default, "ShiftCount");
            }
            // ایجاد دستور اصلاح رکورد جاری در جدول
            mdaCalendar.UpdateCommand = new SqlCommand("Update Tbl_Calendar Set CalendarTitle=@CalendarTitle,ShiftCount=@ShiftCount Where CalendarCode=@CalendarCode", Module1.cnProductionPlanning);
            {
                var withBlock1 = mdaCalendar.UpdateCommand;
                withBlock1.Parameters.Add("@CalendarTitle", SqlDbType.VarChar, 255, "CalendarTitle");
                withBlock1.Parameters.Add("@ShiftCount", SqlDbType.TinyInt, default, "ShiftCount");
                withBlock1.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode").SourceVersion = DataRowVersion.Original;
            }
            // ایجاد دستور حذف رکورد جاری در جدول
            mdaCalendar.DeleteCommand = new SqlCommand("Delete From Tbl_Calendar Where CalendarCode=@CalendarCode", Module1.cnProductionPlanning);
            {
                var withBlock2 = mdaCalendar.DeleteCommand;
                withBlock2.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode").SourceVersion = DataRowVersion.Original;
            }

            // ---------- تنظیم دستورات مربوط به شیفتهای تقویم کاری ----------
            // ایجاد دستور اضافه کردن رکورد جدید در جدول
            mdaCalendarShifts.InsertCommand = new SqlCommand("Insert Into Tbl_CalendarShifts(CalendarCode,ShiftNo,ShiftStart,ShiftDuration,ShiftExtraTime) Values(@CalendarCode,@ShiftNo,@ShiftStart,@ShiftDuration,@ShiftExtraTime)", Module1.cnProductionPlanning);
            {
                var withBlock3 = mdaCalendarShifts.InsertCommand;
                withBlock3.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode");
                withBlock3.Parameters.Add("@ShiftNo", SqlDbType.TinyInt, default, "ShiftNo");
                withBlock3.Parameters.Add("@ShiftStart", SqlDbType.VarChar, 50, "ShiftStart");
                withBlock3.Parameters.Add("@ShiftDuration", SqlDbType.VarChar, 50, "ShiftDuration");
                withBlock3.Parameters.Add("@ShiftExtraTime", SqlDbType.VarChar, 50, "ShiftExtraTime");
            }
            // ایجاد دستور اصلاح رکورد جاری در جدول
            mdaCalendarShifts.UpdateCommand = new SqlCommand("Update Tbl_CalendarShifts Set ShiftStart=@ShiftStart,ShiftDuration=@ShiftDuration,ShiftExtraTime=@ShiftExtraTime Where CalendarCode=@CalendarCode And ShiftNo=@ShiftNo", Module1.cnProductionPlanning);
            {
                var withBlock4 = mdaCalendarShifts.UpdateCommand;
                withBlock4.Parameters.Add("@ShiftStart", SqlDbType.VarChar, 50, "ShiftStart");
                withBlock4.Parameters.Add("@ShiftDuration", SqlDbType.VarChar, 50, "ShiftDuration");
                withBlock4.Parameters.Add("@ShiftExtraTime", SqlDbType.VarChar, 50, "ShiftExtraTime");
                withBlock4.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode").SourceVersion = DataRowVersion.Original;
                withBlock4.Parameters.Add("@ShiftNo", SqlDbType.TinyInt, default, "ShiftNo").SourceVersion = DataRowVersion.Original;
            }
            // ایجاد دستور حذف رکورد جاری در جدول
            mdaCalendarShifts.DeleteCommand = new SqlCommand("Delete From Tbl_CalendarShifts Where CalendarCode=@CalendarCode And ShiftNo=@ShiftNo", Module1.cnProductionPlanning);
            {
                var withBlock5 = mdaCalendarShifts.DeleteCommand;
                withBlock5.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode").SourceVersion = DataRowVersion.Original;
                withBlock5.Parameters.Add("@ShiftNo", SqlDbType.TinyInt, default, "ShiftNo").SourceVersion = DataRowVersion.Original;
            }

            // ---------- تنظیم دستورات مربوط به زمان های استراحت شیفت های تقویم کاری ----------
            // ایجاد دستور اضافه کردن رکورد جدید در جدول
            mdaShiftDownTimes.InsertCommand = new SqlCommand("Insert Into Tbl_CalendarShiftDownTimes(CalendarCode,ShiftNo,DownTimeStart,DownTimeEnd) Values(@CalendarCode,@ShiftNo,@DownTimeStart,@DownTimeEnd)", Module1.cnProductionPlanning);
            {
                var withBlock6 = mdaShiftDownTimes.InsertCommand;
                withBlock6.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode");
                withBlock6.Parameters.Add("@ShiftNo", SqlDbType.TinyInt, default, "ShiftNo");
                withBlock6.Parameters.Add("@DownTimeStart", SqlDbType.VarChar, 50, "DownTimeStart");
                withBlock6.Parameters.Add("@DownTimeEnd", SqlDbType.VarChar, 50, "DownTimeEnd");
            }

            mdaShiftDownTimes.UpdateCommand = new SqlCommand("Update Tbl_CalendarShiftDownTimes Set CalendarCode=@C_CalendarCode,ShiftNo=@C_ShiftNo,DownTimeStart=@C_DownTimeStart,DownTimeEnd=@DownTimeEnd Where CalendarCode=@CalendarCode And ShiftNo=@ShiftNo And DownTimeStart=@DownTimeStart", Module1.cnProductionPlanning);
            {
                var withBlock7 = mdaShiftDownTimes.UpdateCommand;
                withBlock7.Parameters.Add("@C_CalendarCode", SqlDbType.Int, default, "CalendarCode").SourceVersion = DataRowVersion.Current;
                withBlock7.Parameters.Add("@C_ShiftNo", SqlDbType.TinyInt, default, "ShiftNo").SourceVersion = DataRowVersion.Current;
                withBlock7.Parameters.Add("@C_DownTimeStart", SqlDbType.VarChar, 50, "DownTimeStart").SourceVersion = DataRowVersion.Current;
                withBlock7.Parameters.Add("@DownTimeEnd", SqlDbType.VarChar, 50, "DownTimeEnd");
                withBlock7.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode").SourceVersion = DataRowVersion.Original;
                withBlock7.Parameters.Add("@ShiftNo", SqlDbType.TinyInt, default, "ShiftNo").SourceVersion = DataRowVersion.Original;
                withBlock7.Parameters.Add("@DownTimeStart", SqlDbType.VarChar, 50, "DownTimeStart").SourceVersion = DataRowVersion.Original;
            }

            // ایجاد دستور حذف رکورد جاری در جدول
            mdaShiftDownTimes.DeleteCommand = new SqlCommand("Delete From Tbl_CalendarShiftDownTimes Where CalendarCode=@CalendarCode And ShiftNo=@ShiftNo And DownTimeStart=@DownTimeStart", Module1.cnProductionPlanning);
            {
                var withBlock8 = mdaShiftDownTimes.DeleteCommand;
                withBlock8.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode").SourceVersion = DataRowVersion.Original;
                withBlock8.Parameters.Add("@ShiftNo", SqlDbType.TinyInt, default, "ShiftNo").SourceVersion = DataRowVersion.Original;
                withBlock8.Parameters.Add("@DownTimeStart", SqlDbType.VarChar, 50, "DownTimeStart").SourceVersion = DataRowVersion.Original;
            }

            // ---------- تنظیم دستورات مربوط به روزهای تقویم کاری ----------
            // ایجاد دستور اضافه کردن رکورد جدید در جدول
            mdaCalendarDays.InsertCommand = new SqlCommand("Insert Into Tbl_CalendarDays(CalendarCode,ShiftNo,DayNo,ShiftStart,ShiftDuration,ShiftExtraTime,DayName,DayType,Description) " + "Values(@CalendarCode,@ShiftNo,@DayNo,@ShiftStart,@ShiftDuration,@ShiftExtraTime,@DayName,@DayType,@Description)", Module1.cnProductionPlanning);
            {
                var withBlock9 = mdaCalendarDays.InsertCommand;
                withBlock9.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode");
                withBlock9.Parameters.Add("@ShiftNo", SqlDbType.TinyInt, default, "ShiftNo");
                withBlock9.Parameters.Add("@DayNo", SqlDbType.TinyInt, default, "DayNo");
                withBlock9.Parameters.Add("@ShiftStart", SqlDbType.VarChar, 50, "ShiftStart");
                withBlock9.Parameters.Add("@ShiftDuration", SqlDbType.VarChar, 50, "ShiftDuration");
                withBlock9.Parameters.Add("@ShiftExtraTime", SqlDbType.VarChar, 50, "ShiftExtraTime");
                withBlock9.Parameters.Add("@DayName", SqlDbType.VarChar, 50, "DayName");
                withBlock9.Parameters.Add("@DayType", SqlDbType.TinyInt, default, "DayType");
                withBlock9.Parameters.Add("@Description", SqlDbType.VarChar, 255, "Description");
            }
            // ایجاد دستور اصلاح رکورد جاری در جدول
            mdaCalendarDays.UpdateCommand = new SqlCommand("Update Tbl_CalendarDays Set ShiftStart=@ShiftStart,ShiftDuration=@ShiftDuration,ShiftExtraTime=@ShiftExtraTime,DayName=@DayName,DayType=@DayType,Description=@Description Where CalendarCode=@CalendarCode And ShiftNo=@ShiftNo And DayNo=@DayNo", Module1.cnProductionPlanning);
            {
                var withBlock10 = mdaCalendarDays.UpdateCommand;
                withBlock10.Parameters.Add("@ShiftStart", SqlDbType.VarChar, 50, "ShiftStart");
                withBlock10.Parameters.Add("@ShiftDuration", SqlDbType.VarChar, 50, "ShiftDuration");
                withBlock10.Parameters.Add("@ShiftExtraTime", SqlDbType.VarChar, 50, "ShiftExtraTime");
                withBlock10.Parameters.Add("@DayName", SqlDbType.VarChar, 50, "DayName");
                withBlock10.Parameters.Add("@DayType", SqlDbType.TinyInt, default, "DayType");
                withBlock10.Parameters.Add("@Description", SqlDbType.VarChar, 255, "Description");
                withBlock10.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode").SourceVersion = DataRowVersion.Original;
                withBlock10.Parameters.Add("@ShiftNo", SqlDbType.TinyInt, default, "ShiftNo").SourceVersion = DataRowVersion.Original;
                withBlock10.Parameters.Add("@DayNo", SqlDbType.TinyInt, default, "DayNo").SourceVersion = DataRowVersion.Original;
            }
            // ایجاد دستور حذف رکورد جاری در جدول
            mdaCalendarDays.DeleteCommand = new SqlCommand("Delete From Tbl_CalendarDays Where CalendarCode=@CalendarCode And ShiftNo=@ShiftNo And DayNo=@DayNo", Module1.cnProductionPlanning);
            {
                var withBlock11 = mdaCalendarDays.DeleteCommand;
                withBlock11.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode").SourceVersion = DataRowVersion.Original;
                withBlock11.Parameters.Add("@ShiftNo", SqlDbType.TinyInt, default, "ShiftNo").SourceVersion = DataRowVersion.Original;
                withBlock11.Parameters.Add("@DayNo", SqlDbType.TinyInt, default, "DayNo").SourceVersion = DataRowVersion.Original;
            }

            mdaDaysDownTimes.InsertCommand = new SqlCommand("Insert Into Tbl_CalendarDaysDownTimes(CalendarCode,ShiftNo,DayNo,DownTimeStart,DownTimeEnd) Values(@CalendarCode,@ShiftNo,@DayNo,@DownTimeStart,@DownTimeEnd)", Module1.cnProductionPlanning);
            {
                var withBlock12 = mdaDaysDownTimes.InsertCommand;
                withBlock12.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode");
                withBlock12.Parameters.Add("@ShiftNo", SqlDbType.TinyInt, default, "ShiftNo");
                withBlock12.Parameters.Add("@DayNo", SqlDbType.TinyInt, default, "DayNo");
                withBlock12.Parameters.Add("@DownTimeStart", SqlDbType.VarChar, 50, "DownTimeStart");
                withBlock12.Parameters.Add("@DownTimeEnd", SqlDbType.VarChar, 50, "DownTimeEnd");
            }

            mdaDaysDownTimes.UpdateCommand = new SqlCommand("Update Tbl_CalendarDaysDownTimes Set CalendarCode=@C_CalendarCode,ShiftNo=@C_ShiftNo,DayNo=@C_DayNo,DownTimeStart=@C_DownTimeStart,DownTimeEnd=@DownTimeEnd Where CalendarCode=@CalendarCode And ShiftNo=@ShiftNo And DayNo=@DayNo And DownTimeStart=@DownTimeStart", Module1.cnProductionPlanning);
            {
                var withBlock13 = mdaDaysDownTimes.UpdateCommand;
                withBlock13.Parameters.Add("@C_CalendarCode", SqlDbType.Int, default, "CalendarCode").SourceVersion = DataRowVersion.Current;
                withBlock13.Parameters.Add("@C_ShiftNo", SqlDbType.TinyInt, default, "ShiftNo").SourceVersion = DataRowVersion.Current;
                withBlock13.Parameters.Add("@C_DayNo", SqlDbType.TinyInt, default, "DayNo").SourceVersion = DataRowVersion.Current;
                withBlock13.Parameters.Add("@C_DownTimeStart", SqlDbType.VarChar, 50, "DownTimeStart").SourceVersion = DataRowVersion.Current;
                withBlock13.Parameters.Add("@DownTimeEnd", SqlDbType.VarChar, 50, "DownTimeEnd");
                withBlock13.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode").SourceVersion = DataRowVersion.Original;
                withBlock13.Parameters.Add("@ShiftNo", SqlDbType.TinyInt, default, "ShiftNo").SourceVersion = DataRowVersion.Original;
                withBlock13.Parameters.Add("@DayNo", SqlDbType.TinyInt, default, "DayNo").SourceVersion = DataRowVersion.Original;
                withBlock13.Parameters.Add("@DownTimeStart", SqlDbType.VarChar, 50, "DownTimeStart").SourceVersion = DataRowVersion.Original;
            }

            // ایجاد دستور حذف رکورد جاری در جدول
            mdaDaysDownTimes.DeleteCommand = new SqlCommand("Delete From Tbl_CalendarDaysDownTimes Where CalendarCode=@CalendarCode And ShiftNo=@ShiftNo And DayNo=@DayNo And DownTimeStart=@DownTimeStart", Module1.cnProductionPlanning);
            {
                var withBlock14 = mdaDaysDownTimes.DeleteCommand;
                withBlock14.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode").SourceVersion = DataRowVersion.Original;
                withBlock14.Parameters.Add("@ShiftNo", SqlDbType.TinyInt, default, "ShiftNo").SourceVersion = DataRowVersion.Original;
                withBlock14.Parameters.Add("@DayNo", SqlDbType.TinyInt, default, "DayNo").SourceVersion = DataRowVersion.Original;
                withBlock14.Parameters.Add("@DownTimeStart", SqlDbType.VarChar, 50, "DownTimeStart").SourceVersion = DataRowVersion.Original;
            }

            mdaParticulareShifts.DeleteCommand = new SqlCommand("Delete From Tbl_CalendarParticularShifts Where CalendarCode=@CalendarCode And ShamsiDate=@ShamsiDate And ShiftNo=@ShiftNo", Module1.cnProductionPlanning);
            {
                var withBlock15 = mdaParticulareShifts.DeleteCommand;
                withBlock15.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode").SourceVersion = DataRowVersion.Original;
                withBlock15.Parameters.Add("@ShamsiDate", SqlDbType.Int, default, "ShamsiDate").SourceVersion = DataRowVersion.Original;
                withBlock15.Parameters.Add("@ShiftNo", SqlDbType.TinyInt, default, "ShiftNo").SourceVersion = DataRowVersion.Original;
            }

            mdaParticulareDays.DeleteCommand = new SqlCommand("Delete From Tbl_CalendarParticularDays Where CalendarCode=@CalendarCode And ShamsiDate=@ShamsiDate", Module1.cnProductionPlanning);
            {
                var withBlock16 = mdaParticulareDays.DeleteCommand;
                withBlock16.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode").SourceVersion = DataRowVersion.Original;
                withBlock16.Parameters.Add("@ShamsiDate", SqlDbType.Int, default, "ShamsiDate").SourceVersion = DataRowVersion.Original;
            }
        }

        private bool FormValidation()
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                MessageBox.Show("عنوان تقویم را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtTitle.Focus();
                return false;
            }
            else if (txtShiftCount.Value == 0m)
            {
                MessageBox.Show("تعداد شیفتها را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtShiftCount.Focus();
                return false;
            }

            var shifts = new List<CalendarShift>();

            dsProductionPlanning.Tables["Tbl_CalendarShifts"].DefaultView.Sort = "ShiftNo";
            var mFirstShiftStart = CommonTool.CTimeSpan(dsProductionPlanning.Tables["Tbl_CalendarShifts"].DefaultView[0]["ShiftStart"].ToString());
            foreach (DataRowView drv in dsProductionPlanning.Tables["Tbl_CalendarShifts"].DefaultView)
            {
                if (drv.Row.RowState != DataRowState.Deleted)
                {
                    shifts.Add(new CalendarShift
                    {
                        ShiftNo = int.Parse(drv["ShiftNo"].ToString()),
                        Start = drv["ShiftStart"].ToString(),
                        Duration = drv["ShiftDuration"].ToString(),
                        ExtraTime = drv["ShiftExtraTime"].ToString()
                    });
                }
            }
            var calendarShifts = new CalendarShifts(shifts);
            var check = calendarShifts.CalendarShiftCheck();
            return check;
        }

        //private bool CheckAccessibleTimeValidation()
        //{
        //    var AccessibleTime = CommonTool.CTimeSpan("0.00:00:00");
        //    foreach (DataRow dr in dsProductionPlanning.Tables["Tbl_CalendarShifts"].Rows)
        //    {
        //        if (dr.RowState != DataRowState.Deleted)
        //        {
        //            if (AccessibleTime + CommonTool.CTimeSpan(dr["ShiftDuration"].ToString()) + CommonTool.CTimeSpan(dr["ShiftExtraTime"].ToString()) > CommonTool.CTimeSpan("1.00:00:00"))
        //            {
        //                return false;
        //            }
        //            else
        //            {
        //                AccessibleTime += CommonTool.CTimeSpan(dr["ShiftDuration"].ToString()) + CommonTool.CTimeSpan(dr["ShiftExtraTime"].ToString());
        //            }
        //        }
        //    }

        //    return true;
        //}

        //private bool CheckStartTimesValidation()
        //{
        //    var shifts = new List<CalendarShift>();
            

        //    dsProductionPlanning.Tables["Tbl_CalendarShifts"].DefaultView.Sort = "ShiftNo";
        //    var mFirstShiftStart = CommonTool.CTimeSpan(dsProductionPlanning.Tables["Tbl_CalendarShifts"].DefaultView[0]["ShiftStart"].ToString());
        //    foreach (DataRowView drv in dsProductionPlanning.Tables["Tbl_CalendarShifts"].DefaultView)
        //    {
        //        if (drv.Row.RowState != DataRowState.Deleted)
        //        {
        //            shifts.Add(new CalendarShift {
        //                ShiftNo = int.Parse(drv["ShiftNo"].ToString()),
        //                Start = drv["ShiftStart"].ToString(),
        //                Duration = drv["ShiftDuration"].ToString(),
        //                ExtraTime = drv["ShiftExtraTime"].ToString()
        //            });


        //            var mStart = CommonTool.CTimeSpan(Conversions.ToString(drv["ShiftStart"]));
        //            var mDuration = CommonTool.CTimeSpan(Conversions.ToString(drv["ShiftDuration"])) + CommonTool.CTimeSpan(Conversions.ToString(drv["ShiftExtraTime"]));
        //            var mEnd = mStart + mDuration;
        //            var mDR = dsProductionPlanning.Tables["Tbl_CalendarShifts"].Select(Conversions.ToString(Operators.ConcatenateObject("ShiftNo > ", drv["ShiftNo"])));
        //            foreach (DataRow r in mDR)
        //            {
        //                if (r.RowState != DataRowState.Deleted)
        //                {
        //                    //if (mStart.Equals(CTimeSpan(r["ShiftStart"].ToString())))
        //                    //{
        //                    //    // اگر شروع دو شیفت باهم برابر باشد
        //                    //    return false;
        //                    //}
        //                    //else
        //                    if (CommonTool.CTimeSpan(r["ShiftStart"].ToString()) < mEnd && CommonTool.CTimeSpan(r["ShiftStart"].ToString()) > mFirstShiftStart)
        //                    {
        //                        // اگر شروع شیفت فعلی قبل یا مساوی، اتمام شیفت قبلی باشد
        //                        MessageBox.Show("  تداخل زمان شیفتها : اتمام شیفت " + drv["ShiftNo"].ToString() + " بزرگتر از شروع شیفت بعدی است ", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);

        //                        return false;
        //                    }
                            
        //                }
        //            }
        //        }
        //    }
        //    var calendarShifts = new CalendarShifts(shifts);
        //    var check = calendarShifts.CalendarShiftCheck();
        //    return check;
        //}

        private void AddNewShift(string CalendarCode)
        {
            DataRow drInsert;
            var loopTo = (short)Math.Round(txtShiftCount.Value - 1m);
            for (I = 0; I <= loopTo; I++)
            {
                drInsert = dsProductionPlanning.Tables["Tbl_CalendarShifts"].NewRow();
                drInsert["CalendarCode"] = CalendarCode;
                drInsert["ShiftNo"] = dgShifts.Rows[I].Cells[0].Value;
                drInsert["StartHour"] = dgShifts.Rows[I].Cells[1].Value;
                drInsert["StartMinute"] = dgShifts.Rows[I].Cells[2].Value;
                drInsert["DurationHour"] = dgShifts.Rows[I].Cells[3].Value;
                drInsert["DurationMinute"] = dgShifts.Rows[I].Cells[4].Value;
                drInsert["ExtraTimeHour"] = dgShifts.Rows[I].Cells[5].Value;
                drInsert["ExtraTimeMinute"] = dgShifts.Rows[I].Cells[6].Value;
                drInsert["DownTimeHour"] = dgShifts.Rows[I].Cells[7].Value;
                drInsert["DownTimeMinute"] = dgShifts.Rows[I].Cells[8].Value;
                drInsert["DTStartHour"] = dgShifts.Rows[I].Cells[9].Value;
                drInsert["DTStartMinute"] = dgShifts.Rows[I].Cells[10].Value;
                dsProductionPlanning.Tables["Tbl_CalendarShifts"].Rows.Add(drInsert);
            }
        }

        private void AddNewDay(string CalendarCode)
        {
            DataRow drInsert;
            DataRow drInsertDownTime;
            foreach (DataRow dr in dsProductionPlanning.Tables["Tbl_CalendarShifts"].Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    for (J = 1; J <= 7; J++)
                    {
                        drInsert = dsProductionPlanning.Tables["Tbl_CalendarDays"].NewRow();
                        drInsert["CalendarCode"] = CalendarCode;
                        drInsert["ShiftNo"] = dr["ShiftNo"];
                        drInsert["DayNo"] = J;
                        drInsert["Description"] = Constants.vbNullString;
                        if (J < 7)
                        {
                            drInsert["ShiftStart"] = dr["ShiftStart"];
                            drInsert["ShiftDuration"] = dr["ShiftDuration"];
                            drInsert["ShiftExtraTime"] = dr["ShiftExtraTime"];
                            switch (J)
                            {
                                case 1:
                                    {
                                        drInsert["DayName"] = "شنبه";
                                        break;
                                    }

                                case 2:
                                    {
                                        drInsert["DayName"] = "یکشنبه";
                                        break;
                                    }

                                case 3:
                                    {
                                        drInsert["DayName"] = "دوشنبه";
                                        break;
                                    }

                                case 4:
                                    {
                                        drInsert["DayName"] = "سه شنبه";
                                        break;
                                    }

                                case 5:
                                    {
                                        drInsert["DayName"] = "جهارشنبه";
                                        break;
                                    }

                                case 6:
                                    {
                                        drInsert["DayName"] = "پنجشنبه";
                                        break;
                                    }
                            }

                            drInsert["DayType"] = 2;
                            var mdrDT = dsProductionPlanning.Tables["Tbl_CalendarShiftDownTimes"].Select("ShiftNo = " + dr["ShiftNo"].ToString());
                            foreach (DataRow r in mdrDT)
                            {
                                drInsertDownTime = dsProductionPlanning.Tables["Tbl_CalendarDaysDownTimes"].NewRow();
                                drInsertDownTime["CalendarCode"] = CalendarCode;
                                drInsertDownTime["ShiftNo"] = dr["ShiftNo"];
                                drInsertDownTime["DayNo"] = J;
                                drInsertDownTime["DownTimeStart"] = r["DownTimeStart"];
                                drInsertDownTime["DownTimeEnd"] = r["DownTimeEnd"];
                                dsProductionPlanning.Tables["Tbl_CalendarDaysDownTimes"].Rows.Add(drInsertDownTime);
                            }
                        }
                        else
                        {
                            drInsert["ShiftStart"] = "00:00";
                            drInsert["ShiftDuration"] = "00:00";
                            drInsert["ShiftExtraTime"] = "00:00";
                            drInsert["DayName"] = "جمعه";
                            drInsert["DayType"] = 1;
                        }

                        dsProductionPlanning.Tables["Tbl_CalendarDays"].Rows.Add(drInsert);
                    }
                }
            }
        }

        private void SaveChanges()
        {
            DataSet dsChanges;
            dsChanges = dsProductionPlanning.GetChanges();
            if (dsChanges.HasErrors)
            {
                dsProductionPlanning.RejectChanges();
            }
            else
            {
                if (ListForm.FormMode != (int)Module1.FormModeEnum.DELETE_MODE)
                {
                    mdaCalendar.Update(dsChanges, "Tbl_Calendar");
                    mdaCalendarShifts.Update(dsChanges, "Tbl_CalendarShifts");
                    mdaCalendarDays.Update(dsChanges, "Tbl_CalendarDays");
                    mdaShiftDownTimes.Update(dsChanges, "Tbl_CalendarShiftDownTimes");
                    mdaDaysDownTimes.Update(dsChanges, "Tbl_CalendarDaysDownTimes");
                }
                else
                {
                    mdaDaysDownTimes.Update(dsChanges, "Tbl_CalendarDaysDownTimes");
                    mdaShiftDownTimes.Update(dsChanges, "Tbl_CalendarShiftDownTimes");
                    mdaParticulareDays.Update(dsChanges, "Tbl_CalendarParticularDays");
                    mdaParticulareShifts.Update(dsChanges, "Tbl_CalendarParticularShifts");
                    mdaCalendarDays.Update(dsChanges, "Tbl_CalendarDays");
                    mdaCalendarShifts.Update(dsChanges, "Tbl_CalendarShifts");
                    mdaCalendar.Update(dsChanges, "Tbl_Calendar");
                }

                dsProductionPlanning.AcceptChanges();
            }

            dsChanges = null;
        }

        private bool SetShiftGridColumns(string mShiftStart, TimeSpan mShiftDuration)
        {
            var mDayEnd = CommonTool.CTimeSpan("23:59:59") + CommonTool.CTimeSpan("00:00:01");
            string mTimeSlice = "00:05";
            var mAddedTimeSlice = new TimeSpan();
            DataGridViewColumn mcol = null;
            int mColWidth = 45;
            int mColCounter = 0;
            try
            {
                // mcol = New DataGridViewColumn(New DataGridViewTextBoxCell())
                // mcol.HeaderCell.Style.Font = New Font("Arial", 7, FontStyle.Regular, GraphicsUnit.Point)
                // mcol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

                // 'mColCounter += 1
                // mcol.Name = "colTime" & mColCounter.ToString()
                // mcol.HeaderText = mShiftStart
                // mcol.Width = mColWidth
                // dgShiftTimeLine.Columns.Add(mcol)


                if (mShiftDuration == CommonTool.CTimeSpan("00:00"))
                    return false;

                if (mShiftDuration > CommonTool.CTimeSpan("00:00"))
                {
                    while (true)
                    {
                        mAddedTimeSlice += CommonTool.CTimeSpan(mTimeSlice);
                        if (CommonTool.CTimeSpan(mShiftStart) + CommonTool.CTimeSpan(mTimeSlice) >= mDayEnd)
                        {
                            mShiftStart = (CommonTool.CTimeSpan(mShiftStart) + CommonTool.CTimeSpan(mTimeSlice) - mDayEnd).ToString().Substring(0, 5);
                        }
                        else
                        {
                            mShiftStart = (CommonTool.CTimeSpan(mShiftStart) + CommonTool.CTimeSpan(mTimeSlice)).ToString().Substring(0, 5);
                        }

                        if (mAddedTimeSlice > mShiftDuration)
                        {
                            if (mShiftStart.Equals("00:00"))
                            {
                                mShiftStart = (CommonTool.CTimeSpan("1.00:00:00") - (mAddedTimeSlice - mShiftDuration)).ToString().Substring(0, 5);
                            }
                            else
                            {
                                mShiftStart = (CommonTool.CTimeSpan(mShiftStart) - (mAddedTimeSlice - mShiftDuration)).ToString().Substring(0, 5);
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
                Logger.LogException(Name + ".SetShiftGridColumns", ex);
                MessageBox.Show("نمایش شیفت با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                return false;
            }

            return true;
        }

        
    }
}