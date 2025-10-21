using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmMachine
    {
        public frmMachine()
        {
            InitializeComponent();
            _cmdSave.Name = "cmdSave";
            _cmdExit.Name = "cmdExit";
            _cmdDelete.Name = "cmdDelete";
            _cmdEditReplacement.Name = "cmdEditReplacement";
            _cmdRemoveReplacement.Name = "cmdRemoveReplacement";
            _cmdAddReplacement.Name = "cmdAddReplacement";
        }

        private frmRecordsLists mListForm;
        private SqlDataAdapter daMachine = new SqlDataAdapter();
        private DataRow CurrentRow; // برای نگهداری رکورد جاری
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
                return ListForm.dsProductionPlanning;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdSave, 6);
            Module1.SetButtonsImage(cmdDelete, 9);
            Module1.SetButtonsImage(cmdExit, 5);
            Module1.SetButtonsImage(cmdAddReplacement, 0);
            Module1.SetButtonsImage(cmdEditReplacement, 2);
            Module1.SetButtonsImage(cmdRemoveReplacement, 1);
            FormLoad();
        }

        private void frmMachine_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Make sure Machine with code -1 exists otherwise add it.
            Module1.CheckMachineCodeForOperatorWork();

            dsProductionPlanning.RejectChanges();
            dsProductionPlanning.Relations.Clear();
            dsProductionPlanning.Tables["Tbl_Machines"].Constraints.RemoveAt(1);
            for (I = 1; I >= 1; I += -1)
            {
                dsProductionPlanning.Tables[I].Constraints.Clear();
                dsProductionPlanning.Tables[I].Dispose();
                dsProductionPlanning.Tables.RemoveAt(I);
            }

            CurrentRow = null;
            mListForm = null;
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("مشخصات ماشین را حذف می کنید", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading, false) == DialogResult.Yes)
            {
                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();
                var trnDelete = Module1.cnProductionPlanning.BeginTransaction();
                try
                {
                    daMachine.DeleteCommand.Transaction = trnDelete;
                    {
                        var withBlock = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Delete From Tbl_ReplacementMachines Where MachineCode = '", CurrentRow["Code"]), "'")), Module1.cnProductionPlanning);
                        withBlock.Transaction = trnDelete;
                        withBlock.ExecuteNonQuery();
                    }

                    CurrentRow.Delete();
                    SaveChanges();
                    trnDelete.Commit();
                    Close();
                }
                catch (SqlException ObjEx)
                {
                    dsProductionPlanning.RejectChanges();
                    trnDelete.Rollback();
                    if (ObjEx.Number == 547)
                    {
                        MessageBox.Show("این ماشین برای اجرای عملیاتی تعیین شده است لذا قابل حذف نمی باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                    }
                }
                catch (InvalidConstraintException ObjCnstEx)
                {
                    dsProductionPlanning.RejectChanges();
                    trnDelete.Rollback();

                    // MessageBox.Show("این ماشین برای اجرای عملیاتی تعیین شده است لذا قابل حذف نمی باشد", MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, False)
                    MessageBox.Show(ObjCnstEx.Message, "Production Planning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception objEx)
                {
                    dsProductionPlanning.RejectChanges();
                    trnDelete.Rollback();
                    Logger.SaveError(Name + ".cmdDelete_Click", objEx.Message);
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

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (cmdSave.Text == "ثبت")
            {
                if (!FormValidation())
                {
                    return;
                }

                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();
                var trnMachine = Module1.cnProductionPlanning.BeginTransaction();
                switch (ListForm.FormMode)
                {
                    case (int)Module1.FormModeEnum.INSERT_MODE:
                        {
                            try
                            {
                                daMachine.InsertCommand.Transaction = trnMachine;
                                CurrentRow = dsProductionPlanning.Tables["Tbl_Machines"].NewRow();
                                CurrentRow["Code"] = txtCode.Text;
                                CurrentRow["Name"] = txtName.Text;
                                CurrentRow["Producer"] = txtProducer.Text;
                                CurrentRow["ProducerCountry"] = txtProducerCountry.Text;
                                CurrentRow["Application"] = txtApplication.Text;
                                CurrentRow["CalendarCode"] = cbCalendar.SelectedValue;
                                CurrentRow["CalendarTitle"] = cbCalendar.Text;
                                CurrentRow["Description"] = txtDescription.Text;
                                dsProductionPlanning.Tables["Tbl_Machines"].Rows.Add(CurrentRow);
                                CurrentRow = dsProductionPlanning.Tables["Tbl_Machines"].Rows[dsProductionPlanning.Tables["Tbl_Machines"].Rows.Count - 1];
                                SaveChanges();
                                trnMachine.Commit();
                                SetReplacementGridSource();
                                cmdSave.Text = "تغییر";
                                cmdExit.Text = "خروج";
                                Module1.SetButtonsImage(cmdSave, 2);
                                Panel2.Enabled = false;
                                GroupBox1.Enabled = true;
                            }
                            catch (ConstraintException objEx)
                            {
                                dsProductionPlanning.RejectChanges();
                                trnMachine.Rollback();
                                if (objEx.Message.Contains("already present"))
                                {
                                    MessageBox.Show("کد ماشین وارد شده وجود دارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    MessageBox.Show(objEx.Message);
                                }
                            }
                            catch (Exception objEx)
                            {
                                dsProductionPlanning.RejectChanges();
                                trnMachine.Rollback();
                                MessageBox.Show("اشکال در ثبت رکورد جدید، رکورد جدید ثبت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                MessageBox.Show(objEx.Message);
                            }
                            finally
                            {
                                trnMachine.Dispose();
                                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                    Module1.cnProductionPlanning.Close();
                            }

                            break;
                        }

                    case (int)Module1.FormModeEnum.EDIT_MODE:
                        {
                            try
                            {
                                string mAlarmDesc = Constants.vbNullString;
                                daMachine.UpdateCommand.Transaction = trnMachine;
                                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(CurrentRow["CalendarCode"], cbCalendar.SelectedValue, false)))
                                {
                                    mAlarmDesc = "تقویم ماشین با کد: {" + txtCode.Text + "} تغییر کرد";
                                }

                                CurrentRow.BeginEdit();
                                CurrentRow["Code"] = txtCode.Text;
                                CurrentRow["Name"] = txtName.Text;
                                CurrentRow["Producer"] = txtProducer.Text;
                                CurrentRow["ProducerCountry"] = txtProducerCountry.Text;
                                CurrentRow["Application"] = txtApplication.Text;
                                CurrentRow["CalendarCode"] = cbCalendar.SelectedValue;
                                CurrentRow["CalendarTitle"] = cbCalendar.Text;
                                CurrentRow["Description"] = txtDescription.Text;
                                CurrentRow.EndEdit();
                                SaveChanges();
                                trnMachine.Commit();
                                if (!string.IsNullOrEmpty(mAlarmDesc))
                                {
                                    Check_Machine_HasPlanningAlarm(txtCode.Text, "3", mAlarmDesc);
                                }

                                SetReplacementGridSource();
                                cmdSave.Text = "تغییر";
                                cmdExit.Text = "خروج";
                                Module1.SetButtonsImage(cmdSave, 2);
                                Panel2.Enabled = false;
                                GroupBox1.Enabled = true;
                            }
                            catch (Exception objEx)
                            {
                                CurrentRow.CancelEdit();
                                trnMachine.Rollback();
                                MessageBox.Show("اشکال در اصلاح رکورد، تغییرات ثبت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                MessageBox.Show(objEx.Message);
                            }
                            finally
                            {
                                trnMachine.Dispose();
                                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                    Module1.cnProductionPlanning.Close();
                            }

                            break;
                        }
                }
            }
            else
            {
                cmdSave.Text = "ثبت";
                cmdExit.Text = "انصراف";
                Module1.SetButtonsImage(cmdSave, 6);
                GroupBox1.Enabled = false;
                Panel2.Enabled = true;
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            if (cmdExit.Text == "خروج")
            {
                Close();
                return;
            }
            else
            {
                if (mListForm.FormMode != (int)Module1.FormModeEnum.EDIT_MODE)
                {
                    Close();
                    return;
                }

                FillControls();
                SetReplacementGridSource();
                cmdSave.Text = "تغییر";
                cmdExit.Text = "خروج";
                Module1.SetButtonsImage(cmdSave, 2);
                Panel2.Enabled = false;
                GroupBox1.Enabled = true;
            }
        }

        private void cmdAddReplacement_Click(object sender, EventArgs e)
        {
            if (dgReplacements.Rows.Count == 0 && !ReferenceEquals(sender, cmdAddReplacement))
            {
                MessageBox.Show("رکوردی وجود ندارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                return;
            }

            {
                var withBlock = My.MyProject.Forms.frmReplacementMachines;
                withBlock.ParentMachineCode = txtCode.Text;
                if (ReferenceEquals(sender, cmdAddReplacement))
                {
                    withBlock.FormMode = (short)Module1.FormModeEnum.INSERT_MODE;
                }
                else if (ReferenceEquals(sender, cmdEditReplacement))
                {
                    withBlock.FormMode = (short)Module1.FormModeEnum.EDIT_MODE;
                }
                else if (ReferenceEquals(sender, cmdRemoveReplacement))
                {
                    withBlock.FormMode = (short)Module1.FormModeEnum.DELETE_MODE;
                    withBlock.cmdSave.Visible = false;
                    withBlock.cmdDelete.Visible = true;
                }

                if (!ReferenceEquals(sender, cmdAddReplacement))
                {
                    if (dgReplacements.CurrentRow is null)
                    {
                        MessageBox.Show("رکوردی انتخاب نشده است", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                        return;
                    }

                    withBlock.ReplacementMachineCode = Conversions.ToString(dgReplacements.SelectedRows[0].Cells[1].Value);
                    withBlock.Description = Conversions.ToString(dgReplacements.SelectedRows[0].Cells[3].Value);
                }

                string Conditionstr = " Where Not Code IN ('-1','" + txtCode.Text + "'";
                if (ReferenceEquals(sender, cmdAddReplacement) && dgReplacements.Rows.Count > 0)
                {
                    var loopTo = (short)(dgReplacements.Rows.Count - 1);
                    for (I = 0; I <= loopTo; I++)
                        Conditionstr = Conversions.ToString(Conditionstr + Operators.ConcatenateObject(Operators.ConcatenateObject(",'", dgReplacements.Rows[I].Cells[1].Value), "'"));
                }

                Conditionstr += ")";
                withBlock.Conditions = Conditionstr;
                withBlock.ShowDialog();
                withBlock.Dispose();
                SetReplacementGridSource();
            }
        }

        private void FormLoad()
        {
            try
            {
                CreateDataAdapterCommands();

                // Set calendar combo box:
                cbCalendar.DataSource = null;
                cbCalendar.DataSource = dsProductionPlanning.Tables["Tbl_Calendar"];
                cbCalendar.DisplayMember = "CalendarTitle";
                cbCalendar.ValueMember = "CalendarCode";

                switch (ListForm.FormMode)
                {
                    case (int)Module1.FormModeEnum.INSERT_MODE:
                        {
                            GroupBox1.Enabled = false;
                            txtCode.Focus();
                            break;
                        }

                    case (int)Module1.FormModeEnum.EDIT_MODE:
                    case (int)Module1.FormModeEnum.DELETE_MODE:
                        {
                            CurrentRow = ListForm.GetRow();
                            FillControls();
                            SetReplacementGridSource();
                            switch (ListForm.FormMode)
                            {
                                case (int)Module1.FormModeEnum.EDIT_MODE:
                                    {
                                        GroupBox1.Enabled = false;
                                        txtCode.Focus();
                                        break;
                                    }

                                case (int)Module1.FormModeEnum.DELETE_MODE:
                                    {
                                        cmdExit.Text = "خروج";
                                        cmdAddReplacement.Enabled = false;
                                        cmdEditReplacement.Enabled = false;
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
                MessageBox.Show("فراخوانی فرم با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(objEx.Message);
            }
        }

        private void CreateDataAdapterCommands()
        {
            // ----------------- دستورات مربوط به ماشین آلات -------------------
            // ایجاد دستور اضافه کردن رکورد جدید در جدول
            daMachine.InsertCommand = new SqlCommand("Insert Into Tbl_Machines(Code,Name,Producer,ProducerCountry,Application,CalendarCode,Description) Values(@Code,@Name,@Producer,@ProducerCountry,@Application,@CalendarCode,@Description)", Module1.cnProductionPlanning);
            {
                var withBlock = daMachine.InsertCommand;
                withBlock.Parameters.Add("@Code", SqlDbType.VarChar, 50, "Code");
                withBlock.Parameters.Add("@Name", SqlDbType.VarChar, 50, "Name");
                withBlock.Parameters.Add("@Producer", SqlDbType.VarChar, 50, "Producer");
                withBlock.Parameters.Add("@ProducerCountry", SqlDbType.VarChar, 50, "ProducerCountry");
                withBlock.Parameters.Add("@Application", SqlDbType.VarChar, 50, "Application");
                withBlock.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode");
                withBlock.Parameters.Add("@Description", SqlDbType.VarChar, 255, "Description");
            }
            // ایجاد دستور اصلاح رکورد جاری در جدول
            daMachine.UpdateCommand = new SqlCommand("Update Tbl_Machines Set Code=@CurrentCode,Name=@Name,Producer=@Producer,ProducerCountry=@ProducerCountry,Application=@Application,CalendarCode=@CalendarCode,Description=@Description Where Code=@OldCode", Module1.cnProductionPlanning);
            {
                var withBlock1 = daMachine.UpdateCommand;
                withBlock1.Parameters.Add("@CurrentCode", SqlDbType.VarChar, 50, "Code");
                withBlock1.Parameters[0].Direction = ParameterDirection.Input;
                withBlock1.Parameters[0].SourceVersion = DataRowVersion.Current;
                withBlock1.Parameters.Add("@OldCode", SqlDbType.VarChar, 50, "Code");
                withBlock1.Parameters[1].Direction = ParameterDirection.Input;
                withBlock1.Parameters[1].SourceVersion = DataRowVersion.Original;
                withBlock1.Parameters.Add("@Name", SqlDbType.VarChar, 50, "Name");
                withBlock1.Parameters.Add("@Producer", SqlDbType.VarChar, 50, "Producer");
                withBlock1.Parameters.Add("@ProducerCountry", SqlDbType.VarChar, 50, "ProducerCountry");
                withBlock1.Parameters.Add("@Application", SqlDbType.VarChar, 50, "Application");
                withBlock1.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode");
                withBlock1.Parameters.Add("@Description", SqlDbType.VarChar, 255, "Description");
            }
            // ایجاد دستور حذف رکورد جاری در جدول
            daMachine.DeleteCommand = new SqlCommand("Delete From Tbl_Machines Where Code=@Code AND Code <> '-1'", Module1.cnProductionPlanning);
            {
                var withBlock2 = daMachine.DeleteCommand;
                withBlock2.Parameters.Add("@Code", SqlDbType.VarChar, 50, "Code");
                withBlock2.Parameters[0].Direction = ParameterDirection.Input;
                withBlock2.Parameters[0].SourceVersion = DataRowVersion.Original;
            }
        }

        private void FillControls()
        {
            txtCode.Text = Conversions.ToString(CurrentRow["Code"]);
            txtName.Text = Conversions.ToString(CurrentRow["Name"]);
            txtProducer.Text = Conversions.ToString(CurrentRow["Producer"]);
            txtProducerCountry.Text = Conversions.ToString(CurrentRow["ProducerCountry"]);
            txtApplication.Text = Conversions.ToString(CurrentRow["Application"]);
            cbCalendar.SelectedValue = CurrentRow["CalendarCode"];
            txtDescription.Text = Conversions.ToString(CurrentRow["Description"]);

            // configuration for machine code '-1'
            if (txtCode.Text == "-1")
            {
                txtCode.Enabled = false;
                txtName.Enabled = false;
                txtProducer.Enabled = false;
                txtName.Enabled = false;
                txtProducerCountry.Enabled = false;
                txtApplication.Enabled = false;
                txtDescription.Enabled = false;
                cmdDelete.Enabled = false;
                GroupBox1.Visible = false;
               
               // txtDescription.Text = "این ماشین جهت عملیات اپراتوری استفاد شده است و جزیی از تنظیمات سیستم میباشد. تقویم کاری مشخص شده در برنامه ریزی سالیانه استفاده خواهد شد. ";
            }
        }

        private void SetReplacementGridSource()
        {
            dgReplacements.Rows.Clear();
            {
                var withBlock = new SqlDataAdapter("Select A.MachineCode,A.ReplacementMachineCode,B.[Name] AS ReplacementName, A.[Description] " + "From   dbo.Tbl_ReplacementMachines A Inner Join dbo.Tbl_Machines B ON A.ReplacementMachineCode = B.Code Where A.MachineCode='" + txtCode.Text + "'", Module1.cnProductionPlanning);
                var dt = new DataTable();
                withBlock.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                    dgReplacements.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
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
                daMachine.Update(dsChanges, "Tbl_Machines");
                dsProductionPlanning.AcceptChanges();
            }

            dsChanges = null;
        }

        private bool FormValidation()
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                MessageBox.Show("کد ماشین را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtCode.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("نام ماشین را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtName.Focus();
                return false;
            }

            if (cbCalendar.SelectedIndex == -1)
            {
                MessageBox.Show("تقویم را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbCalendar.Focus();
                return false;
            }

            // If txtApplication.Text = vbNullString Then
            // MessageBox.Show("موارد کاربرد را وارد کنید", MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            // txtApplication.Focus()
            // Return False
            // End If

            return true;
        }

        public void Check_Machine_HasPlanningAlarm(string mMachineCode, string mAlarmCode, string mAlarmDesc)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    var dt = new DataTable();
                    {
                        var withBlock = new SqlDataAdapter("Select Distinct SubbatchCode From Tbl_Planning Where MachineCode = '" + mMachineCode + "' And SubbatchCode IN (Select SubbatchCode From Tbl_ProductionSubbatchs Where StopPlanning = 0 And BatchCode IN (Select BatchCode From Tbl_ProductionBatchs Where IsNull(FinishedDate,'0') = '0' Or IsNull(FinishedDate,'0') = ''))", cn);
                        withBlock.Fill(dt);
                    }

                    foreach (DataRow dr in dt.Rows)
                    {
                        {
                            var withBlock1 = new SqlCommand("Insert Into Tbl_SubbatchPlanningAlerts(SubbatchCode,AlertCode,AlertDescription) Values('" + dr["SubbatchCode"].ToString() + "'," + mAlarmCode + ",'" + mAlarmDesc + "')", cn);
                            withBlock1.ExecuteNonQuery();
                        }
                    }
                }
                catch
                {
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }
        }

        //private void CheckMachineCodeForOperatorWork()
        //{
             
        //    try
        //    {
        //        if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
        //            Module1.cnProductionPlanning.Open();

        //        var selCmd = new SqlCommand("SELECT Code FROM Tbl_Machines WHERE Code = '-1'", Module1.cnProductionPlanning);
        //        var exists = selCmd.ExecuteScalar();
        //        if (exists == null)
        //        {
        //             var InsertSQL = @"INSERT INTO dbo.Tbl_Machines
        //                                        (Code
        //                                        ,Name
        //                                        ,Producer
        //                                        ,ProducerCountry
        //                                        ,Application
        //                                        ,CalendarCode
        //                                        ,Description)
        //                               SELECT TOP 1 
        //                                   Code = '-1'
        //                                  ,Name = 'عملیات اپراتوری'
        //                                  ,Producer = ''
        //                                  ,ProducerCountry = ''
        //                                  ,Application = ''
        //                                  ,CalendarCode = Tbl_Calendar.CalendarCode
        //                                  ,Description = 'این ماشین جهت عملیات اپراتوری استفاد شده است و جزیی از تنظیمات سیستم میباشد. تقویم کاری مشخص شده در برنامه ریزی سالیانه استفاده خواهد شد'
        //                               FROM Tbl_Calendar
        //                               ORDER bY Tbl_Calendar.CalendarCode";
                    


        //            var insCmd = new SqlCommand(InsertSQL, Module1.cnProductionPlanning);
        //            insCmd.ExecuteNonQuery();
        //        }

        //        Module1.cnProductionPlanning.Close();

        //    }

        //    catch (SqlException ObjEx)
        //    {
        //        Logger.LogException("CheckMachineCodeForOperatorWork", ObjEx);
        //        MessageBox.Show("خطا در ثبت ماشین برای عملیاتهای اپراتوری", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);

        //    }



        //}
    }
}