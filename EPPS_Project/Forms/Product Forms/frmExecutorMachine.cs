using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmExecutorMachine
    {
        public frmExecutorMachine()
        {
            InitializeComponent();
            _cmdSave.Name = "cmdSave";
            _cmdExit.Name = "cmdExit";
        }

        private DataSet mdsProductionPlanning;
        private SqlDataAdapter daExecutorMachines = new SqlDataAdapter();
        private DataRow mCurrentRow;
        private DataRow mOpCurrentRow;
        private DataTable dtCombo = new DataTable();
        private short mFormMode;
        //private short I;

        public DataSet dsProductionPlanning
        {
            get
            {
                return mdsProductionPlanning;
            }

            set
            {
                mdsProductionPlanning = value;
            }
        }

        public DataRow CurrentRow
        {
            set
            {
                mCurrentRow = value;
            }
        }

        public DataRow OpCurrentRow
        {
            set
            {
                mOpCurrentRow = value;
            }
        }

        public short FormMode
        {
            set
            {
                mFormMode = value;
            }
        }

        private void frmExecutorMachine_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdSave, 6);
            Module1.SetButtonsImage(cmdExit, 14);
            FormLoad();
        }

        private void frmOperationMaterial_FormClosing(object sender, FormClosingEventArgs e)
        {
            mdsProductionPlanning.RejectChanges();
            mCurrentRow = null;
            mdsProductionPlanning = null;
            daExecutorMachines.Dispose();
            dtCombo = null;
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!ValidationForm())
            {
                return;
            }

            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                Module1.cnProductionPlanning.Open();
            if (!CheckMachinePriority())
            {
                return;
            }

            var trnExecuterMachine = Module1.cnProductionPlanning.BeginTransaction();
            string mAlarmDesc = Constants.vbNullString;
            switch (mFormMode)
            {
                case (short)Module1.FormModeEnum.INSERT_MODE:
                    {
                        try
                        {
                            daExecutorMachines.InsertCommand.Transaction = trnExecuterMachine;
                            mAlarmDesc = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("ماشین با کد: {" + cbExecutorMachine.SelectedValue.ToString() + "} به لیست ماشین های انجام دهنده عملیات: {", mOpCurrentRow["OperationCode"]), "} اضافه گردید"));
                            // اضافه کردن رکورد جدید به جدول در دیتاست
                            mCurrentRow = dsProductionPlanning.Tables["Tbl_ProductOPCsExecutorMachines"].NewRow();
                            mCurrentRow["TreeCode"] = mOpCurrentRow["TreeCode"];
                            mCurrentRow["OperationCode"] = mOpCurrentRow["OperationCode"];
                            mCurrentRow["MachineCode"] = cbExecutorMachine.SelectedValue;
                            mCurrentRow["MachinePriority"] = txtPriority.Value;
                            mCurrentRow["Operators"] = txtOperator.Value;
                            mCurrentRow["OneExecutionTime"] = txtOneExecutionTime.Value;
                            mCurrentRow["TimeType"] = cbTimeType.SelectedIndex + 1;
                            mCurrentRow["MinimumProduction"] = txtMinimumProduction.Value;
                            mCurrentRow["MaximumAccumulation"] = txtMaximumAccumulation.Value;
                            mCurrentRow["GarbagePercent"] = txtGarbagePercent.Value;
                            mCurrentRow["PerformancePercent"] = txtPerformancePercent.Value;
                            mCurrentRow["OperatorPerformancePercent"] = txtOperatorPerformancePercent.Value;
                            mCurrentRow["RedoPercent"] = txtRedoPercent.Value;
                            double Performance = (double)(1m - txtGarbagePercent.Value / 100m * (txtPerformancePercent.Value / 100m) * (txtOperatorPerformancePercent.Value / 100m) * (txtRedoPercent.Value / 100m));
                            mCurrentRow["ExecutionPerformancePercent"] = Performance;
                            mCurrentRow["MachineSetupTime"] = txtSetupTime.Value;
                            mCurrentRow["SetupTimeType"] = cbSetupTimeType.SelectedIndex + 1;
                            mCurrentRow["OneExecutionPrice"] = Interaction.IIf(string.IsNullOrEmpty(txtOneExecutionPrice.Text), 0, txtOneExecutionPrice.Text);
                            mCurrentRow["CalendarCode"] = 0;
                            mCurrentRow["IsParallelMachine"] = chkIsParallelMachine.Checked;
                            dsProductionPlanning.Tables["Tbl_ProductOPCsExecutorMachines"].Rows.Add(mCurrentRow);
                            SaveChanges();
                            trnExecuterMachine.Commit();
                            Module1.Check_Subbatch_HasPlanningAlarm(Conversions.ToString(mOpCurrentRow["TreeCode"]), "", "", "1", mAlarmDesc);
                            Close();
                        }
                        catch (ConstraintException ObjCnstEx)
                        {
                            Logger.LogException("", ObjCnstEx);
                            mdsProductionPlanning.AcceptChanges();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            mdsProductionPlanning.RejectChanges();
                            trnExecuterMachine.Rollback();
                            MessageBox.Show("اشکال در ثبت رکورد جدید، رکورد جدید ثبت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            MessageBox.Show(objEx.Message);
                        }
                        finally
                        {
                            trnExecuterMachine.Dispose();
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                        }

                        break;
                    }

                case (short)Module1.FormModeEnum.EDIT_MODE:
                    {
                        try
                        {
                            daExecutorMachines.UpdateCommand.Transaction = trnExecuterMachine;
                            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(mCurrentRow["MachineCode"], cbExecutorMachine.SelectedValue, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(mCurrentRow["MachinePriority"], txtPriority.Value, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(mCurrentRow["Operators"], txtOperator.Value, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(mCurrentRow["OneExecutionTime"], txtOneExecutionTime.Value, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(mCurrentRow["TimeType"], cbTimeType.SelectedIndex + 1, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(mCurrentRow["MinimumProduction"], txtMinimumProduction.Value, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(mCurrentRow["MaximumAccumulation"], txtMaximumAccumulation.Value, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(mCurrentRow["MachineSetupTime"], txtSetupTime.Value, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(mCurrentRow["SetupTimeType"], cbSetupTimeType.SelectedIndex + 1, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(mCurrentRow["IsParallelMachine"], chkIsParallelMachine.Checked, false)))



                            {
                                mAlarmDesc = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("مشخصات ماشین با کد: {" + cbExecutorMachine.SelectedValue.ToString() + "} در لیست ماشین های انجام دهنده عملیات: {", mOpCurrentRow["OperationCode"]), "} تغییر کرد"));
                            }

                            mCurrentRow.BeginEdit();
                            mCurrentRow["MachineCode"] = cbExecutorMachine.SelectedValue;
                            mCurrentRow["MachinePriority"] = txtPriority.Value;
                            mCurrentRow["Operators"] = txtOperator.Value;
                            mCurrentRow["OneExecutionTime"] = txtOneExecutionTime.Value;
                            mCurrentRow["TimeType"] = cbTimeType.SelectedIndex + 1;
                            mCurrentRow["MinimumProduction"] = txtMinimumProduction.Value;
                            mCurrentRow["MaximumAccumulation"] = txtMaximumAccumulation.Value;
                            mCurrentRow["GarbagePercent"] = txtGarbagePercent.Value;
                            mCurrentRow["PerformancePercent"] = txtPerformancePercent.Value;
                            mCurrentRow["OperatorPerformancePercent"] = txtOperatorPerformancePercent.Value;
                            mCurrentRow["RedoPercent"] = txtRedoPercent.Value;
                            decimal Performance = (1m - txtGarbagePercent.Value / 100m) * (txtPerformancePercent.Value / 100m) * (txtOperatorPerformancePercent.Value / 100m) * (txtRedoPercent.Value / 100m);
                            mCurrentRow["ExecutionPerformancePercent"] = Performance;
                            mCurrentRow["MachineSetupTime"] = txtSetupTime.Value;
                            mCurrentRow["SetupTimeType"] = cbSetupTimeType.SelectedIndex + 1;
                            mCurrentRow["OneExecutionPrice"] = Interaction.IIf(string.IsNullOrEmpty(txtOneExecutionPrice.Text), 0, txtOneExecutionPrice.Text);
                            mCurrentRow["CalendarCode"] = 0;
                            mCurrentRow["IsParallelMachine"] = chkIsParallelMachine.Checked;
                            mCurrentRow.EndEdit();
                            SaveChanges();
                            trnExecuterMachine.Commit();
                            if (!string.IsNullOrEmpty(mAlarmDesc))
                            {
                                Module1.Check_Subbatch_HasPlanningAlarm(Conversions.ToString(mOpCurrentRow["TreeCode"]), "", "", "1", mAlarmDesc);
                            }

                            Close();
                        }
                        catch (ConstraintException ObjCnstEx)
                        {
                            Logger.LogException("", ObjCnstEx);
                            mdsProductionPlanning.AcceptChanges();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            mCurrentRow.CancelEdit();
                            trnExecuterMachine.Rollback();
                            Logger.SaveError(Name + ".CmdSave_Click", objEx.Message);
                            MessageBox.Show("اشکال در اصلاح رکورد، تغییرات ثبت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        }
                        finally
                        {
                            trnExecuterMachine.Dispose();
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
            try
            {
                CreateDataAdapterCommands();
                if (mFormMode != (int)Module1.FormModeEnum.INSERT_MODE)
                {
                    daExecutorMachines.SelectCommand = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select Code, (Code + ' -- ' + Name) As MachineName From Tbl_Machines Where Code <> '-1' And Not Code IN (Select MachineCode From Tbl_ProductOPCsExecutorMachines Where TreeCode=", mOpCurrentRow["TreeCode"]), " And OperationCode='"), mCurrentRow["OperationCode"]), "' And MachineCode<>'"), mCurrentRow["MachineCode"]), "'"), ")")), Module1.cnProductionPlanning);
                }
                else
                {
                    daExecutorMachines.SelectCommand = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select Code, (Code + ' -- ' + Name) As MachineName From Tbl_Machines Where Code <> '-1' And Not Code IN (Select MachineCode From Tbl_ProductOPCsExecutorMachines Where TreeCode=", mOpCurrentRow["TreeCode"]), " And OperationCode='"), mOpCurrentRow["OperationCode"]), "')")), Module1.cnProductionPlanning);
                }

                daExecutorMachines.Fill(dtCombo);
                {
                    var withBlock = cbExecutorMachine;
                    withBlock.DataSource = null;
                    withBlock.DataSource = dtCombo;
                    withBlock.DisplayMember = "MachineName";
                    withBlock.ValueMember = "Code";
                }

                switch (mFormMode)
                {
                    case (short)Module1.FormModeEnum.INSERT_MODE:
                        {
                            cbSetupTimeType.SelectedIndex = 1;
                            cbTimeType.SelectedIndex = 0;
                            cbExecutorMachine.Focus();
                            break;
                        }

                    case (short)Module1.FormModeEnum.EDIT_MODE:
                        {
                            cbExecutorMachine.SelectedValue = mCurrentRow["MachineCode"];
                            txtPriority.Value = Conversions.ToDecimal(mCurrentRow["MachinePriority"]);
                            txtOperator.Value = Conversions.ToDecimal(mCurrentRow["Operators"]);
                            txtOneExecutionTime.Value = Conversions.ToDecimal(mCurrentRow["OneExecutionTime"]);
                            cbTimeType.SelectedIndex = Conversions.ToInteger(Operators.SubtractObject(mCurrentRow["TimeType"], 1));
                            txtMinimumProduction.Value = Conversions.ToDecimal(mCurrentRow["MinimumProduction"]);
                            txtMaximumAccumulation.Value = Conversions.ToDecimal(mCurrentRow["MaximumAccumulation"]);
                            txtGarbagePercent.Value = Conversions.ToDecimal(mCurrentRow["GarbagePercent"]);
                            txtPerformancePercent.Value = Conversions.ToDecimal(mCurrentRow["PerformancePercent"]);
                            txtOperatorPerformancePercent.Value = Conversions.ToDecimal(mCurrentRow["OperatorPerformancePercent"]);
                            txtRedoPercent.Value = Conversions.ToDecimal(mCurrentRow["RedoPercent"]);
                            txtSetupTime.Value = Conversions.ToDecimal(mCurrentRow["MachineSetupTime"]);
                            cbSetupTimeType.SelectedIndex = Conversions.ToInteger(Operators.SubtractObject(mCurrentRow["SetupTimeType"], 1));
                            txtOneExecutionPrice.Text = Conversions.ToString(mCurrentRow["OneExecutionPrice"]);
                            chkIsParallelMachine.Checked = Conversions.ToBoolean(mCurrentRow["IsParallelMachine"]);
                            cbExecutorMachine.Focus();
                            break;
                        }
                }

                if (!(bool)mOpCurrentRow["IsParallelOperation"])
                {
                    chkIsParallelMachine.Visible = false;
                }
            }
            catch (Exception objEx)
            {
                MessageBox.Show("فراخوانی فرم با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(objEx.Message);
            }
        }

        private bool CheckMachinePriority()
        {
            switch (mFormMode)
            {
                case (short)Module1.FormModeEnum.INSERT_MODE:
                    {
                        var cmPriorityCheck = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select Count(*) From Tbl_ProductOPCsExecutorMachines Where TreeCode=", mOpCurrentRow["TreeCode"]), " And OperationCode='"), mOpCurrentRow["OperationCode"]), "' And MachinePriority="), txtPriority.Value)), Module1.cnProductionPlanning);
                        if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(cmPriorityCheck.ExecuteScalar(), 0, false)))
                        {
                            MessageBox.Show("اولویت وارد شده موجود می باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                            txtPriority.Focus();
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                            cmPriorityCheck.Dispose();
                            return false;
                        }

                        break;
                    }

                case (short)Module1.FormModeEnum.EDIT_MODE:
                    {
                        string OldFilterValue = dsProductionPlanning.Tables["Tbl_ProductOPCsExecutorMachines"].DefaultView.RowFilter;
                        dsProductionPlanning.Tables["Tbl_ProductOPCsExecutorMachines"].DefaultView.RowFilter = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("TreeCode=", mOpCurrentRow["TreeCode"]), " And OperationCode='"), mOpCurrentRow["OperationCode"]), "'"));
                        if (dsProductionPlanning.Tables["Tbl_ProductOPCsExecutorMachines"].DefaultView.Count > 1)
                        {
                            dsProductionPlanning.Tables["Tbl_ProductOPCsExecutorMachines"].DefaultView.RowFilter += " And MachinePriority=" + txtPriority.Value;
                            if (dsProductionPlanning.Tables["Tbl_ProductOPCsExecutorMachines"].DefaultView.Count > 0)
                            {
                                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(dsProductionPlanning.Tables["Tbl_ProductOPCsExecutorMachines"].DefaultView[0]["MachineCode"], mCurrentRow["MachineCode"], false)))
                                {
                                    MessageBox.Show("اولویت وارد شده موجود می باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                                    txtPriority.Focus();
                                    if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                        Module1.cnProductionPlanning.Close();
                                    dsProductionPlanning.Tables["Tbl_ProductOPCsExecutorMachines"].DefaultView.RowFilter = OldFilterValue;
                                    return false;
                                }
                            }
                        }

                        dsProductionPlanning.Tables["Tbl_ProductOPCsExecutorMachines"].DefaultView.RowFilter = OldFilterValue;
                        break;
                    }
            }

            return true;
        }

        private void CreateDataAdapterCommands()
        {
            // -------------------- ایجاد دستورات جدول ماشین آلات انجام دهنده عملیات ----------------------
            // ایجاد دستور اضافه کردن رکورد جدید در جدول
            daExecutorMachines.InsertCommand = new SqlCommand("Insert Into Tbl_ProductOPCsExecutorMachines(TreeCode,OperationCode,MachineCode,MachinePriority,Operators,OneExecutionTime,TimeType,MinimumProduction,MaximumAccumulation,GarbagePercent,PerformancePercent,OperatorPerformancePercent,RedoPercent,ExecutionPerformancePercent,MachineSetupTime,SetupTimeType,OneExecutionPrice,CalendarCode,IsParallelMachine) " + "Values(@TreeCode,@OperationCode,@MachineCode,@MachinePriority,@Operators,@OneExecutionTime,@TimeType,@MinimumProduction,@MaximumAccumulation,@GarbagePercent,@PerformancePercent,@OperatorPerformancePercent,@RedoPercent,@ExecutionPerformancePercent,@MachineSetupTime,@SetupTimeType,@OneExecutionPrice,@CalendarCode,@IsParallelMachine)", Module1.cnProductionPlanning);
            {
                var withBlock = daExecutorMachines.InsertCommand;
                withBlock.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode");
                withBlock.Parameters.Add("@OperationCode", SqlDbType.VarChar, 50, "OperationCode");
                withBlock.Parameters.Add("@MachineCode", SqlDbType.VarChar, 50, "MachineCode");
                withBlock.Parameters.Add("@MachinePriority", SqlDbType.TinyInt, default, "MachinePriority");
                withBlock.Parameters.Add("@Operators", SqlDbType.TinyInt, default, "Operators");
                withBlock.Parameters.Add("@OneExecutionTime", SqlDbType.Real, default, "OneExecutionTime");
                withBlock.Parameters.Add("@TimeType", SqlDbType.TinyInt, default, "TimeType");
                withBlock.Parameters.Add("@MinimumProduction", SqlDbType.Int, default, "MinimumProduction");
                withBlock.Parameters.Add("@MaximumAccumulation", SqlDbType.Int, default, "MaximumAccumulation");
                withBlock.Parameters.Add("@GarbagePercent", SqlDbType.TinyInt, default, "GarbagePercent");
                withBlock.Parameters.Add("@PerformancePercent", SqlDbType.TinyInt, default, "PerformancePercent");
                withBlock.Parameters.Add("@OperatorPerformancePercent", SqlDbType.TinyInt, default, "OperatorPerformancePercent");
                withBlock.Parameters.Add("@RedoPercent", SqlDbType.TinyInt, default, "RedoPercent");
                withBlock.Parameters.Add("@ExecutionPerformancePercent", SqlDbType.VarChar, 50, "ExecutionPerformancePercent");
                withBlock.Parameters.Add("@MachineSetupTime", SqlDbType.Int, default, "MachineSetupTime");
                withBlock.Parameters.Add("@SetupTimeType", SqlDbType.TinyInt, default, "SetupTimeType");
                withBlock.Parameters.Add("@OneExecutionPrice", SqlDbType.Decimal, default, "OneExecutionPrice");
                withBlock.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode");
                withBlock.Parameters.Add("@IsParallelMachine", SqlDbType.Bit, default, "IsParallelMachine");
            }
            // ایجاد دستور اصلاح رکورد جاری در جدول
            daExecutorMachines.UpdateCommand = new SqlCommand("Update Tbl_ProductOPCsExecutorMachines Set MachineCode=@CurrentMachineCode,MachinePriority=@MachinePriority," + "Operators=@Operators,OneExecutionTime=@OneExecutionTime,TimeType=@TimeType,MinimumProduction=@MinimumProduction," + "MaximumAccumulation=@MaximumAccumulation,GarbagePercent=@GarbagePercent,PerformancePercent=@PerformancePercent," + "OperatorPerformancePercent=@OperatorPerformancePercent,RedoPercent=@RedoPercent,ExecutionPerformancePercent=@ExecutionPerformancePercent,MachineSetupTime=@MachineSetupTime," + "SetupTimeType=@SetupTimeType,OneExecutionPrice=@OneExecutionPrice,CalendarCode=@CalendarCode,IsParallelMachine=@IsParallelMachine Where TreeCode=@TreeCode And OperationCode=@OperationCode" + " And MachineCode=@OldMachineCode", Module1.cnProductionPlanning);




            {
                var withBlock1 = daExecutorMachines.UpdateCommand;
                withBlock1.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode").SourceVersion = DataRowVersion.Original;
                withBlock1.Parameters.Add("@OperationCode", SqlDbType.VarChar, 50, "OperationCode").SourceVersion = DataRowVersion.Original;
                withBlock1.Parameters.Add("@CurrentMachineCode", SqlDbType.VarChar, 50, "MachineCode").SourceVersion = DataRowVersion.Current;
                withBlock1.Parameters.Add("@OldMachineCode", SqlDbType.VarChar, 50, "MachineCode").SourceVersion = DataRowVersion.Original;
                withBlock1.Parameters.Add("@MachinePriority", SqlDbType.TinyInt, default, "MachinePriority");
                withBlock1.Parameters.Add("@Operators", SqlDbType.TinyInt, default, "Operators");
                withBlock1.Parameters.Add("@OneExecutionTime", SqlDbType.Real, default, "OneExecutionTime");
                withBlock1.Parameters.Add("@TimeType", SqlDbType.TinyInt, default, "TimeType");
                withBlock1.Parameters.Add("@MinimumProduction", SqlDbType.Int, default, "MinimumProduction");
                withBlock1.Parameters.Add("@MaximumAccumulation", SqlDbType.Int, default, "MaximumAccumulation");
                withBlock1.Parameters.Add("@GarbagePercent", SqlDbType.TinyInt, default, "GarbagePercent");
                withBlock1.Parameters.Add("@PerformancePercent", SqlDbType.TinyInt, default, "PerformancePercent");
                withBlock1.Parameters.Add("@OperatorPerformancePercent", SqlDbType.TinyInt, default, "OperatorPerformancePercent");
                withBlock1.Parameters.Add("@RedoPercent", SqlDbType.TinyInt, default, "RedoPercent");
                withBlock1.Parameters.Add("@ExecutionPerformancePercent", SqlDbType.VarChar, 50, "ExecutionPerformancePercent");
                withBlock1.Parameters.Add("@MachineSetupTime", SqlDbType.Int, default, "MachineSetupTime");
                withBlock1.Parameters.Add("@SetupTimeType", SqlDbType.TinyInt, default, "SetupTimeType");
                withBlock1.Parameters.Add("@OneExecutionPrice", SqlDbType.Decimal, default, "OneExecutionPrice");
                withBlock1.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode");
                withBlock1.Parameters.Add("@IsParallelMachine", SqlDbType.Bit, default, "IsParallelMachine");
            }
        }

        private void SaveChanges()
        {
            DataSet dsChanges;
            dsChanges = dsProductionPlanning.GetChanges();
            if (dsChanges.HasErrors)
            {
                mdsProductionPlanning.RejectChanges();
            }
            else
            {
                daExecutorMachines.Update(dsChanges, "Tbl_ProductOPCsExecutorMachines");
                mdsProductionPlanning.AcceptChanges();
            }

            dsChanges = null;
        }

        private bool ValidationForm()
        {
            if (cbExecutorMachine.SelectedIndex == -1)
            {
                MessageBox.Show("ماشین را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbExecutorMachine.Focus();
                return false;
            }

            if (txtPriority.Value == 0m || string.IsNullOrEmpty(txtPriority.Text))
            {
                MessageBox.Show("اولویت ماشین را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtPriority.Focus();
                return false;
            }

            if (cbTimeType.SelectedIndex == -1)
            {
                MessageBox.Show("نوع زمان را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbTimeType.Focus();
                return false;
            }

            if (cbSetupTimeType.SelectedIndex == -1)
            {
                MessageBox.Show("نوع زمان را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbSetupTimeType.Focus();
                return false;
            }

            // If txtOneExecutionPrice.Text = vbNullString Then
            // MessageBox.Show("هزینه اجرای یکبار عملیات را وارد کنید", MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            // txtOneExecutionPrice.Focus()
            // Return False
            // End If

            return true;
        }
    }
}