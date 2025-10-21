using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmWorkPeriod
    {
        public frmWorkPeriod()
        {
            InitializeComponent();
            _cmdSave.Name = "cmdSave";
            _cmdCancel.Name = "cmdCancel";
        }

        private DataTable mdtWorkPeriod = null;
        private string mOperatorCode = "-1";
        private DataRow mCurrentRow;

        public DataTable dtWorkPeriod
        {
            get
            {
                return mdtWorkPeriod;
            }

            set
            {
                mdtWorkPeriod = value;
            }
        }

        public string OperatorCode
        {
            set
            {
                mOperatorCode = value;
            }
        }

        public DataRow CurrentRow
        {
            set
            {
                mCurrentRow = value;
            }
        }

        private void frmWorkPeriod_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdSave, 6);
            Module1.SetButtonsImage(cmdCancel, 14);
            FormLoad();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!ValidationForm())
            {
                return;
            }

            switch (Conversions.ToInteger(Tag))
            {
                case (int)Module1.FormModeEnum.INSERT_MODE:
                    {
                        try
                        {
                            mCurrentRow = mdtWorkPeriod.NewRow();
                            mCurrentRow["OperatorCode"] = mOperatorCode;
                            mCurrentRow["StartDate"] = txtStartDate.Text;
                            mCurrentRow["EndDate"] = txtEndDate.Text;
                            mCurrentRow["Description"] = txtDescription.Text;
                            mdtWorkPeriod.Rows.Add(mCurrentRow);
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            mCurrentRow = null;
                            mdtWorkPeriod.RejectChanges();
                            Logger.SaveError(Name + ".cmdSave_Click", objEx.Message);
                            MessageBox.Show("ثبت سابقۀ شغلی اپراتور با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        }

                        break;
                    }

                case (int)Module1.FormModeEnum.EDIT_MODE:
                    {
                        try
                        {
                            mCurrentRow.BeginEdit();
                            mCurrentRow["StartDate"] = txtStartDate.Text;
                            mCurrentRow["EndDate"] = txtEndDate.Text;
                            mCurrentRow["Description"] = txtDescription.Text;
                            mCurrentRow.EndEdit();
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            mCurrentRow.CancelEdit();
                            Logger.SaveError(Name + ".cmdSave_Click", objEx.Message);
                            MessageBox.Show("ثبت سابقۀ شغلی اپراتور با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        }

                        break;
                    }
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormLoad()
        {
            try
            {
                if (Conversions.ToInteger(Tag) == (int)Module1.FormModeEnum.EDIT_MODE)
                {
                    txtStartDate.Text = Conversions.ToString(mCurrentRow["StartDate"]);
                    txtEndDate.Text = Conversions.ToString(Interaction.IIf(mCurrentRow["EndDate"].ToString().Equals("0"), "", mCurrentRow["EndDate"]));
                    txtDescription.Text = Conversions.ToString(mCurrentRow["Description"]);
                }

                txtStartDate.Focus();
            }
            catch (Exception objEx)
            {
                Logger.SaveError(Name + ".FormLoad", objEx.Message);
                MessageBox.Show("فراخوانی فرم ثبت دورۀ کارب با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                Close();
            }
        }

        private bool ValidationForm()
        {
            if (txtStartDate.Text is null || txtStartDate.Text.Trim().Equals("") || txtStartDate.Text.Trim().Length < 8)
            {
                MessageBox.Show("تاریخ شروع را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtStartDate.Focus();
                return false;
            }

            if (txtEndDate.Text is null || txtEndDate.Text.Trim().Equals(""))
            {
                txtEndDate.Text = "0";
            }

            if (txtEndDate.Text.Trim().Equals("0"))
            {
                if (Conversions.ToInteger(Tag) == (int)Module1.FormModeEnum.EDIT_MODE)
                {
                    if (mdtWorkPeriod.Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("EndDate = '0' And StartDate <> '", mCurrentRow["StartDate"]), "'"))).Length > 0)
                    {
                        MessageBox.Show("قبلا سابقه ای بدون تاریخ پایان وارد شده است، تاریخ پایان را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        txtEndDate.Focus();
                        return false;
                    }
                }
                else if (mdtWorkPeriod.Select("EndDate = '0'").Length > 0)
                {
                    MessageBox.Show("قبلا سابقه ای بدون تاریخ پایان وارد شده است، تاریخ پایان را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    txtEndDate.Focus();
                    return false;
                }
            }
            else if (txtEndDate.Text.Trim().Length < 8)
            {
                MessageBox.Show("تاریخ پایان صحیح نیست", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtEndDate.Focus();
                return false;
            }
            else if (Operators.CompareString(txtStartDate.Text, txtEndDate.Text, false) > 0)
            {
                MessageBox.Show("تاریخ پایان نباید قبل از تاریخ شروع باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtEndDate.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtDescription.Text) || txtDescription.Text.Equals(""))
            {
                txtDescription.Text = "-";
            }

            if (Conversions.ToInteger(Tag) == (int)Module1.FormModeEnum.EDIT_MODE) // اصلاح سابقۀ شغلی
            {
                if (!mCurrentRow["StartDate"].Equals(txtStartDate.Text) && !CheckExistsStartDate(txtStartDate.Text))
                {
                    return false;
                }
            }
            else if (!CheckExistsStartDate(txtStartDate.Text)) // سابقۀ شغلی جدید
            {
                return false;
            }

            if (!CheckOperatorProduction(mOperatorCode, txtStartDate.Text, txtEndDate.Text))
            {
                return false;
            }

            if (!txtEndDate.Text.Trim().Equals("0"))
            {
                if (!CheckPeriodStartEnd(txtStartDate.Text, txtEndDate.Text))
                {
                    return false;
                }
            }
            else if (!CheckPeriodStartEnd(txtStartDate.Text, Module1.mServerShamsiDate))
            {
                return false;
            }

            string mCondition = "StartDate < '" + txtStartDate.Text + "'";
            if (Conversions.ToInteger(Tag) == (int)Module1.FormModeEnum.EDIT_MODE)
            {
                mCondition = Conversions.ToString(mCondition + Operators.ConcatenateObject(Operators.ConcatenateObject(" And StartDate <> '", mCurrentRow["StartDate"]), "'"));
            }

            var mdr = dtWorkPeriod.Select(mCondition);
            for (int I = 0, loopTo = mdr.Length - 1; I <= loopTo; I++)
            {
                if (Operators.CompareString(mdr[I]["EndDate"].ToString(), txtStartDate.Text, false) > 0)
                {
                    MessageBox.Show("تاریخ شروع وارد شده قبل از تاریخ پایان سوابق ثبت شده می باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    txtStartDate.Focus();
                    return false;
                }
            }

            return true;
        }

        private bool CheckExistsStartDate(string mStartDate)
        {
            if (mdtWorkPeriod.Select("StartDate = '" + mStartDate + "'").Length > 0)
            {
                MessageBox.Show("تاریخ شروع تکراری است", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtStartDate.Focus();
                return false;
            }

            return true;
        }

        private bool CheckPeriodStartEnd(string PeriodStart, string PeriodEnd)
        {
            string mCondition = "EndDate > '" + PeriodStart + "' And EndDate <= '" + PeriodEnd + "'";
            if (Conversions.ToInteger(Tag) == (int)Module1.FormModeEnum.EDIT_MODE)
            {
                mCondition = Conversions.ToString(mCondition + Operators.ConcatenateObject(Operators.ConcatenateObject(" And StartDate <> '", mCurrentRow["StartDate"]), "'"));
            }

            if (dtWorkPeriod.Select(mCondition).Length > 0)
            {
                MessageBox.Show("تاریخ های سابقۀ وارد شده با سوابق ثبت شده مغایرت دارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtStartDate.Focus();
                return false;
            }
            else
            {
                mCondition = "StartDate > '" + PeriodStart + "' And StartDate <= '" + PeriodEnd + "'";
                if (Conversions.ToInteger(Tag) == (int)Module1.FormModeEnum.EDIT_MODE)
                {
                    mCondition = Conversions.ToString(mCondition + Operators.ConcatenateObject(Operators.ConcatenateObject(" And StartDate <> '", mCurrentRow["StartDate"]), "'"));
                }

                if (dtWorkPeriod.Select(mCondition).Length > 0)
                {
                    MessageBox.Show("تاریخ پایان وارد شده با سوابق ثبت شده مغایرت دارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    txtEndDate.Focus();
                    return false;
                }
                else
                {
                    mCondition = "StartDate < '" + PeriodStart + "' And EndDate > '" + PeriodEnd + "'";
                    if (Conversions.ToInteger(Tag) == (int)Module1.FormModeEnum.EDIT_MODE)
                    {
                        mCondition = Conversions.ToString(mCondition + Operators.ConcatenateObject(Operators.ConcatenateObject(" And StartDate <> '", mCurrentRow["StartDate"]), "'"));
                    }

                    if (dtWorkPeriod.Select(mCondition).Length > 0)
                    {
                        MessageBox.Show("تاریخ پایان وارد شده با سوابق ثبت شده مغایرت دارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        txtEndDate.Focus();
                        return false;
                    }
                    else
                    {
                        mCondition = "EndDate = '0' And StartDate <= '" + PeriodEnd + "'";
                        if (Conversions.ToInteger(Tag) == (int)Module1.FormModeEnum.EDIT_MODE)
                        {
                            mCondition = Conversions.ToString(mCondition + Operators.ConcatenateObject(Operators.ConcatenateObject(" And StartDate <> '", mCurrentRow["StartDate"]), "'"));
                        }

                        if (dtWorkPeriod.Select(mCondition).Length > 0)
                        {
                            MessageBox.Show("تاریخ های سابقۀ وارد شده با سوابق ثبت شده مغایرت دارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                            txtStartDate.Focus();
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private bool CheckOperatorProduction(string mOperatorCode, string mStartDate, string mEndDate)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    var cmCheckProduction = new SqlCommand("Select Count(A.ProductionCode) From Tbl_RealProduction A Inner Join Tbl_ProductionOperators B ON A.ProductionCode = B.ProductionCode Where B.OperatorCode = '" + mOperatorCode + "' And A.StartDate < '" + mStartDate + "'", cn);
                    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(cmCheckProduction.ExecuteScalar(), 0, false)))
                    {
                        MessageBox.Show("برای اپراتور قبل از تاریخ شروع تولید ثبت شده است", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        txtStartDate.Focus();
                        return false;
                    }
                    else if (!mEndDate.Trim().Equals("0"))
                    {
                        cmCheckProduction.CommandText = "Select Count(A.ProductionCode) From Tbl_RealProduction A Inner Join Tbl_ProductionOperators B ON A.ProductionCode = B.ProductionCode Where B.OperatorCode = '" + mOperatorCode + "' And A.StartDate > '" + mEndDate + "'";
                        if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(cmCheckProduction.ExecuteScalar(), 0, false)))
                        {
                            MessageBox.Show("برای اپراتور بعد از تاریخ پایان تولید ثبت شده است", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                            txtEndDate.Focus();
                            return false;
                        }
                    }
                }
                catch (Exception objEx)
                {
                    Logger.SaveError(Name + ".CheckOperatorProduction", objEx.Message);
                    MessageBox.Show("ثبت سابقۀ شغلی اپراتور با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
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