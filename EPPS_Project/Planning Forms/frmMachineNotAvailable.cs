using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmMachineNotAvailable
    {
        public frmMachineNotAvailable()
        {
            InitializeComponent();
            _cmdSave.Name = "cmdSave";
            _cmdExit.Name = "cmdExit";
            _cmdDelete.Name = "cmdDelete";
        }

        private frmRecordsLists mListForm;
        private SqlDataAdapter mdaNotAvailable = new SqlDataAdapter();
        private DataRow mCurrentRow;
        private DataView dvMachines;
        private short I;

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
                return mListForm.dsProductionPlanning;
            }
        }

        private void frmRealProduction_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdSave, 6);
            Module1.SetButtonsImage(cmdDelete, 9);
            Module1.SetButtonsImage(cmdExit, 14);
            FormLoad();
        }

        private void frmRealProduction_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (I = (short)(dsProductionPlanning.Tables.Count - 1); I >= 1; I += -1)
            {
                dsProductionPlanning.Tables[I].Dispose();
                dsProductionPlanning.Tables.RemoveAt(I);
            }

            mdaNotAvailable.Dispose();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("مشخصات در دسترس نبودن را حذف می کنید", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading, false) == DialogResult.Yes)
            {
                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();
                var trnDelete = Module1.cnProductionPlanning.BeginTransaction();
                try
                {
                    mdaNotAvailable.DeleteCommand.Transaction = trnDelete;
                    mCurrentRow.Delete();
                    SaveChanges();
                    trnDelete.Commit();
                    Close();
                }
                catch (InvalidConstraintException ObjCnstEx)
                {
                    Logger.LogException("", ObjCnstEx);
                    dsProductionPlanning.RejectChanges();
                    trnDelete.Rollback();
                    MessageBox.Show("برای حذف رکورد باید رکورد(های) مرتبط با آن حذف شوند", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                }
                catch (Exception objEx)
                {
                    dsProductionPlanning.RejectChanges();
                    trnDelete.Rollback();
                    Logger.SaveError(Name + ".cmdDelete_Click", objEx.Message);
                    MessageBox.Show("حذف رکورد با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    trnDelete.Dispose();
                    if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                        Module1.cnProductionPlanning.Close();
                }
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!FormValidation())
            {
                return;
            }

            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                Module1.cnProductionPlanning.Open();
            var trnProduction = Module1.cnProductionPlanning.BeginTransaction();
            string StartHour = 0.ToString();
            string EndHour = 0.ToString();
            switch (mListForm.FormMode)
            {
                case (int)Module1.FormModeEnum.INSERT_MODE:
                    {
                        try
                        {
                            string mID = Module1.GetNewCode("Tbl_MachineNotAvailableTimes", "ID").ToString();
                            mdaNotAvailable.InsertCommand.Transaction = trnProduction;
                            mCurrentRow = dsProductionPlanning.Tables["Tbl_MachineNotAvailableTimes"].NewRow();
                            mCurrentRow["ID"] = mID;
                            mCurrentRow["MachineCode"] = cbMachine.SelectedValue;
                            mCurrentRow["MachineName"] = cbMachine.Text;
                            mCurrentRow["StartDate"] = Strings.Replace(txtStartDate.Text, "/", "");

                            // mohsendokht remark
                            // mCurrentRow("MinBaseStartHour") = txtStartHour.Text
                            // StartHour = GetFloatingHour(txtStartHour.Text)

                            // If StartHour.ToString().Contains(".") Then
                            // If StartHour < 10.0 Then
                            // StartHour = "0" & StartHour.ToString()
                            // End If
                            // Else
                            // If CInt(StartHour) < 10 Then
                            // StartHour = "0" & StartHour & ".0"
                            // Else
                            // StartHour = StartHour & ".0"
                            // End If
                            // End If

                            // mCurrentRow("StartHour") = StartHour
                            mCurrentRow["StartHour"] = txtStartHour.Text;
                            mCurrentRow["EndDate"] = Strings.Replace(txtEndDate.Text, "/", "");
                            mCurrentRow["EndHour"] = txtEndHour.Text;
                            // mohsendokht remark
                            // mCurrentRow("MinBaseEndHour") = txtEndHour.Text
                            // EndHour = GetFloatingHour(txtEndHour.Text)

                            // If EndHour.ToString().Contains(".") Then
                            // If EndHour < 10.0 Then
                            // EndHour = "0" & EndHour.ToString()
                            // End If
                            // Else
                            // If CInt(EndHour) < 10 Then
                            // EndHour = "0" & EndHour & ".0"
                            // Else
                            // EndHour = EndHour & ".0"
                            // End If
                            // End If

                            // mCurrentRow("EndHour") = EndHour
                            mCurrentRow["ReasonCode"] = cbReason.SelectedValue;
                            mCurrentRow["ReasonTitle"] = cbReason.Text;
                            dsProductionPlanning.Tables["Tbl_MachineNotAvailableTimes"].Rows.Add(mCurrentRow);
                            SaveChanges();
                            trnProduction.Commit();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            dsProductionPlanning.RejectChanges();
                            trnProduction.Rollback();
                            Logger.SaveError(Name + ".cmdSave_Click", objEx.Message);
                            MessageBox.Show("اشکال در ثبت رکورد جدید، رکورد جدید ثبت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            trnProduction.Dispose();
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                        }

                        break;
                    }

                case (int)Module1.FormModeEnum.EDIT_MODE:
                    {
                        try
                        {
                            mdaNotAvailable.UpdateCommand.Transaction = trnProduction;
                            mCurrentRow.BeginEdit();
                            mCurrentRow["MachineCode"] = cbMachine.SelectedValue;
                            mCurrentRow["MachineName"] = cbMachine.Text;
                            mCurrentRow["StartDate"] = Strings.Replace(txtStartDate.Text, "/", "");
                            mCurrentRow["StartHour"] = txtStartHour.Text;
                            mCurrentRow["EndDate"] = Strings.Replace(txtEndDate.Text, "/", "");
                            mCurrentRow["EndHour"] = txtEndHour.Text;
                            // mohsendokht remark
                            // mCurrentRow("MinBaseStartHour") = txtStartHour.Text
                            // StartHour = GetFloatingHour(txtStartHour.Text)

                            // If StartHour.ToString().Contains(".") Then
                            // If StartHour < 10.0 Then
                            // StartHour = "0" & StartHour.ToString()
                            // End If
                            // Else
                            // If CInt(StartHour) < 10 Then
                            // StartHour = "0" & StartHour & ".0"
                            // Else
                            // StartHour = StartHour & ".0"
                            // End If
                            // End If

                            // mCurrentRow("StartHour") = StartHour
                            // mCurrentRow("EndDate") = Replace(txtEndDate.Text, "/", "")
                            // mCurrentRow("MinBaseEndHour") = txtEndHour.Text
                            // EndHour = GetFloatingHour(txtEndHour.Text)

                            // If EndHour.ToString().Contains(".") Then
                            // If EndHour < 10.0 Then
                            // EndHour = "0" & EndHour.ToString()
                            // End If
                            // Else
                            // If CInt(EndHour) < 10 Then
                            // EndHour = "0" & EndHour & ".0"
                            // Else
                            // EndHour = EndHour & ".0"
                            // End If
                            // End If

                            // mCurrentRow("EndHour") = EndHour
                            mCurrentRow["ReasonCode"] = cbReason.SelectedValue;
                            mCurrentRow["ReasonTitle"] = cbReason.Text;
                            mCurrentRow.EndEdit();
                            SaveChanges();
                            trnProduction.Commit();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            mCurrentRow.CancelEdit();
                            trnProduction.Rollback();
                            Logger.SaveError(Name + ".cmdSave_Click", objEx.Message);
                            MessageBox.Show("تغییرات ثبت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                        }
                        finally
                        {
                            trnProduction.Dispose();
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                        }

                        break;
                    }
            }
        }

        private void FormLoad()
        {
            try
            {
                CreateDataAdapterCommands();
                dvMachines = dsProductionPlanning.Tables["Tbl_Machines"].DefaultView;
                {
                    var withBlock = cbMachine;
                    withBlock.DataSource = dvMachines;
                    withBlock.DisplayMember = "Name";
                    withBlock.ValueMember = "Code";
                }

                {
                    var withBlock1 = cbReason;
                    withBlock1.DataSource = dsProductionPlanning.Tables["Tbl_MachineNotAvailableReasons"].DefaultView;
                    withBlock1.DisplayMember = "ReasonTitle";
                    withBlock1.ValueMember = "ReasonCode";
                }

                switch (mListForm.FormMode)
                {
                    case (int)Module1.FormModeEnum.INSERT_MODE: // در صورتیکه فرم برای ایجاد رکورد جدید فراخوانی شده باشد این قسمت اجرا خواهد شد
                        {
                            txtStartDate.Text = Module1.mServerShamsiDate;
                            txtStartHour.Text = DateAndTime.Now.TimeOfDay.ToString();
                            txtEndDate.Text = Module1.mServerShamsiDate;
                            txtEndHour.Text = DateAndTime.Now.TimeOfDay.ToString();
                            cbMachine.Focus();
                            break;
                        }

                    case (int)Module1.FormModeEnum.EDIT_MODE:
                    case (int)Module1.FormModeEnum.DELETE_MODE: // در صورتیکه فرم برای ویرایش یا حذف رکورد جاری فراخوانی شده باشد این قسمت اجرا خواهد شد
                        {
                            mCurrentRow = mListForm.GetRow();
                            // پر کردن کنترل فرم با مقادیر رکورد جاری
                            FillControls();
                            switch (mListForm.FormMode) // در صورتیکه فرم برای ویرایش(اصلاح) رکورد جاری فراخوانی شده باشد این قسمت اجرا خواهد شد
                            {
                                case (int)Module1.FormModeEnum.EDIT_MODE:
                                    {
                                        cbMachine.Focus();
                                        break;
                                    }

                                case (int)Module1.FormModeEnum.DELETE_MODE: // در صورتیکه فرم برای حذف رکورد جاری فراخوانی شده باشد این قسمت اجرا خواهد شد
                                    {
                                        cmdDelete.Focus();
                                        break;
                                    }
                            }

                            break;
                        }
                }
            }
            catch (Exception objEx)
            {
                Logger.SaveError(Name + ".FormLoad", objEx.Message);
                MessageBox.Show("فراخوانی فرم با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillControls()
        {
            ;
//#error Cannot convert OnErrorResumeNextStatementSyntax - see comment for details
            /* Cannot convert OnErrorResumeNextStatementSyntax, CONVERSION ERROR: Conversion for OnErrorResumeNextStatement not implemented, please report this issue in 'On Error Resume Next' at character 14994


            Input:
                    On Error Resume Next

             */
            txtStartDate.Text = Conversions.ToString(mCurrentRow["StartDate"]);
            txtStartHour.Text = TimeSpan.Parse(Conversions.ToString(mCurrentRow["StartHour"])).ToString();
            txtEndDate.Text = Conversions.ToString(mCurrentRow["EndDate"]);
            txtEndHour.Text = TimeSpan.Parse(Conversions.ToString(mCurrentRow["EndHour"])).ToString();
            cbMachine.SelectedValue = mCurrentRow["MachineCode"];
            if (!DBNull.Value.Equals(mCurrentRow["ReasonCode"]))
            {
                cbReason.SelectedValue = mCurrentRow["ReasonCode"];
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
                mdaNotAvailable.Update(dsChanges, "Tbl_MachineNotAvailableTimes");
                dsProductionPlanning.AcceptChanges();
            }

            dsChanges = null;
        }

        private void CreateDataAdapterCommands()
        {
            // ایجاد دستور اصلاح رکورد جاری در جدول
            mdaNotAvailable.InsertCommand = new SqlCommand("Insert Into Tbl_MachineNotAvailableTimes(ID,MachineCode,StartDate,StartHour,EndDate,EndHour,ReasonCode)" + "Values(@ID,@MachineCode,@StartDate,@StartHour,@EndDate,@EndHour,@ReasonCode)", Module1.cnProductionPlanning);
            {
                var withBlock = mdaNotAvailable.InsertCommand;
                withBlock.Parameters.Add("@ID", SqlDbType.Int, default, "ID");
                withBlock.Parameters.Add("@MachineCode", SqlDbType.VarChar, 50, "MachineCode");
                withBlock.Parameters.Add("@StartDate", SqlDbType.VarChar, 8, "StartDate");
                withBlock.Parameters.Add("@StartHour", SqlDbType.VarChar, 50, "StartHour");
                withBlock.Parameters.Add("@EndDate", SqlDbType.VarChar, 8, "EndDate");
                withBlock.Parameters.Add("@EndHour", SqlDbType.VarChar, 50, "EndHour");
                withBlock.Parameters.Add("@ReasonCode", SqlDbType.Int, default, "ReasonCode");
            }
            // ایجاد دستور ویرایش رکورد در جدول
            mdaNotAvailable.UpdateCommand = new SqlCommand("Update Tbl_MachineNotAvailableTimes Set MachineCode=@MachineCode,StartDate=@StartDate," + "StartHour=@StartHour,EndDate=@EndDate,EndHour=@EndHour,ReasonCode=@ReasonCode Where ID=@ID", Module1.cnProductionPlanning);
            {
                var withBlock1 = mdaNotAvailable.UpdateCommand;
                withBlock1.Parameters.Add("@MachineCode", SqlDbType.VarChar, 50, "MachineCode");
                withBlock1.Parameters.Add("@StartDate", SqlDbType.VarChar, 8, "StartDate");
                withBlock1.Parameters.Add("@StartHour", SqlDbType.VarChar, 50, "StartHour");
                withBlock1.Parameters.Add("@EndDate", SqlDbType.VarChar, 8, "EndDate");
                withBlock1.Parameters.Add("@EndHour", SqlDbType.VarChar, 50, "EndHour");
                withBlock1.Parameters.Add("@ReasonCode", SqlDbType.Int, default, "ReasonCode");
                withBlock1.Parameters.Add("@ID", SqlDbType.Int, default, "ID").SourceVersion = DataRowVersion.Original;
            }
            // ایجاد دستور حذف رکورد جاری در جدول
            mdaNotAvailable.DeleteCommand = new SqlCommand("Delete From Tbl_MachineNotAvailableTimes Where ID=@ID", Module1.cnProductionPlanning);
            {
                var withBlock2 = mdaNotAvailable.DeleteCommand;
                withBlock2.Parameters.Add("@ID", SqlDbType.Int, default, "ID").SourceVersion = DataRowVersion.Original;
            }
        }

        private bool FormValidation()
        {
            if (cbMachine.SelectedIndex == -1)
            {
                MessageBox.Show("ماشین را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbMachine.Focus();
                return false;
            }

            if (cbReason.SelectedIndex == -1)
            {
                MessageBox.Show("علت در دسترس نبودن ماشین را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbReason.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(Strings.Trim(Strings.Replace(txtStartDate.Text, "/", ""))))
            {
                MessageBox.Show("تاریخ شروع را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtStartDate.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(Strings.Trim(Strings.Replace(txtEndDate.Text, "/", ""))))
            {
                MessageBox.Show("تاریخ پایان را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtEndDate.Focus();
                return false;
            }

            return true;
        }
    }
}