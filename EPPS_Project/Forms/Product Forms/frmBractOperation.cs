using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using static ProductionPlanning.MyEnums;


namespace ProductionPlanning
{
    public partial class frmBractOperation
    {
        public frmBractOperation()
        {
            InitializeComponent();
            _cmdSelectMatchedOperations.Name = "cmdSelectMatchedOperations";
            _chkIsHasMatchedOperations.Name = "chkIsHasMatchedOperations";
            _rbCSenond.Name = "rbCSenond";
            _rbCMinute.Name = "rbCMinute";
            _rbCDay.Name = "rbCDay";
            _rbCHour.Name = "rbCHour";
            _rbCMonth.Name = "rbCMonth";
            _rbCWeek.Name = "rbCWeek";
            _rbSecond.Name = "rbSecond";
            _rbMinute.Name = "rbMinute";
            _rbDay.Name = "rbDay";
            _rbHour.Name = "rbHour";
            _rbMonth.Name = "rbMonth";
            _rbWeek.Name = "rbWeek";
            _rbMachine.Name = "rbMachine";
            _rbNonMachine.Name = "rbNonMachine";
            _rbContractor.Name = "rbContractor";
            _cmdSave.Name = "cmdSave";
            _cmdExit.Name = "cmdExit";
        }

        private DataSet mdsProductionPlanning;
        private SqlDataAdapter daProductOPCs = new SqlDataAdapter();
        private SqlDataAdapter daNonExecutorMachines = new SqlDataAdapter();
        private SqlDataAdapter daContractorOperation = new SqlDataAdapter();
        private SqlDataAdapter daMatchedOperation = new SqlDataAdapter();
        private DataRow mCurrentRow;
        private DataRow NonMachineCurrentRow;
        private DataRow ContractorCurrentRow;
        private string mTreeCode;
        private string mDetailCode;
        private int mOperationPriority;
        private short I;
        private short mFormMode;
        private short ContractorTimeType = 1;
        private short NonMachineTimeType = 1;
        private string oldOperationCode = Constants.vbNullString;

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

        public short FormMode
        {
            set
            {
                mFormMode = value;
            }
        }

        public DataRow CurrentRow
        {
            get
            {
                return mCurrentRow;
            }

            set
            {
                mCurrentRow = value;
            }
        }

        public string TreeCode
        {
            get
            {
                return mTreeCode;
            }

            set
            {
                mTreeCode = value;
            }
        }

        public string DetailCode
        {
            get
            {
                return mDetailCode;
            }

            set
            {
                mDetailCode = value;
            }
        }

        public int OperationPriority
        {
            get
            {
                return mOperationPriority;
            }

            set
            {
                mOperationPriority = value;
            }
        }

        private void frmBractOperations_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdExit, 5);
            Module1.SetButtonsImage(cmdSave, 6);
            FormLoad();
        }

        private void frmBractOperations_FormClosing(object sender, FormClosingEventArgs e)
        {
            {
                var withBlock = mdsProductionPlanning;
                withBlock.RejectChanges();
                for (I = (short)(withBlock.Tables.Count - 1); I >= 2; I += -1)
                {
                    withBlock.Tables[I].Constraints.Clear();
                    withBlock.Tables[I].Dispose();
                    withBlock.Tables.RemoveAt(I);
                }
            }

            mCurrentRow = null;
            NonMachineCurrentRow = null;
            ContractorCurrentRow = null;
            daProductOPCs.Dispose();
            daNonExecutorMachines.Dispose();
            daContractorOperation.Dispose();
            mdsProductionPlanning = null;
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdSelectMatchedOperations_Click(object sender, EventArgs e)
        {
            if (!ValidationForm(sender))
            {
                return;
            }

            {
                var withBlock = new frmSelectMatchedOperations();
                withBlock.TreeCode = mTreeCode;
                if (mFormMode == (int)Module1.FormModeEnum.EDIT_MODE)
                {
                    withBlock.OperationCode = Conversions.ToString(mCurrentRow["OperationCode"]);
                }
                else if (mFormMode == (int)Module1.FormModeEnum.INSERT_MODE)
                {
                    withBlock.OperationCode = txtCode.Text;
                }

                withBlock.MatchedOperationsTable = mdsProductionPlanning.Tables["Tbl_MatchedOperations"];
                withBlock.ShowDialog();
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!ValidationForm(sender))
            {
                return;
            }

            bool ChangeToMachineMethod = false;
            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                Module1.cnProductionPlanning.Open();
            var trnProductOpc = Module1.cnProductionPlanning.BeginTransaction();
            string mAlarmDesc = Constants.vbNullString;
            switch (mFormMode)
            {
                case (short)Module1.FormModeEnum.INSERT_MODE:
                    {
                        try
                        {
                            daProductOPCs.InsertCommand.Transaction = trnProductOpc;
                            daMatchedOperation.InsertCommand.Transaction = trnProductOpc;
                            daMatchedOperation.UpdateCommand.Transaction = trnProductOpc;
                            daMatchedOperation.DeleteCommand.Transaction = trnProductOpc;
                            mAlarmDesc = "عملیات: {" + txtCode.Text + "} اضافه شد";
                            if (rbNonMachine.Checked)
                            {
                                daNonExecutorMachines.InsertCommand.Transaction = trnProductOpc;
                            }
                            else if (rbContractor.Checked)
                            {
                                daContractorOperation.InsertCommand.Transaction = trnProductOpc;
                            }

                            // اضافه کردن رکورد جدید به جدول در دیتاست
                            mCurrentRow = dsProductionPlanning.Tables["Tbl_ProductOpcs"].NewRow();
                            mCurrentRow["TreeCode"] = mTreeCode;
                            mCurrentRow["DetailCode"] = mDetailCode;
                            mCurrentRow["OperationCode"] = txtCode.Text;
                            mCurrentRow["OperationTitle"] = cbTitle.Text;
                            mCurrentRow["HavePreOperation"] = 0;
                            mCurrentRow["HavePreItem"] = 0;
                            mCurrentRow["ExecutionMethod"] = Interaction.IIf(rbMachine.Checked, 1, Interaction.IIf(rbNonMachine.Checked, 2, 3));
                            mCurrentRow["OperationPriority"] = 0;
                            mCurrentRow["ActivityGroup"] = txtGruopCode.Text;
                            mCurrentRow["NatureCode"] = cbNature.SelectedValue;
                            mCurrentRow["IsNotCalcWorking"] = chkIsNotCalcWorking.Checked;
                            mCurrentRow["IsParallelOperation"] = Interaction.IIf(rbMachine.Checked, chkIsParallelOperation.Checked, false);
                            dsProductionPlanning.Tables["Tbl_ProductOpcs"].Rows.Add(mCurrentRow);
                            if (chkIsHasMatchedOperations.Checked)
                            {
                                foreach (DataRow r in mdsProductionPlanning.Tables["Tbl_MatchedOperations"].Rows)
                                {
                                    if (r.RowState != DataRowState.Deleted)
                                    {
                                        r.BeginEdit();
                                        r["OperationCode"] = txtCode.Text;
                                        r.EndEdit();
                                    }
                                }
                            }
                            else
                            {
                                mdsProductionPlanning.Tables["Tbl_MatchedOperations"].RejectChanges();
                                // Dim mDR() As DataRow = mdsProductionPlanning.Tables("Tbl_MatchedOperations").Select("OperationCode = '" & txtCode.Text & "' Or MatchedOperationCode = '" & txtCode.Text & "'")

                                // For Each r As DataRow In mDR
                                // If r.RowState <> DataRowState.Deleted Then
                                // r.Delete()
                                // End If
                                // Next
                            }

                            if (rbNonMachine.Checked) // درصورتيكه عمليات اپراتوري باشد
                            {
                                NonMachineCurrentRow = dsProductionPlanning.Tables["Tbl_ProductOPCsExecutorMachines"].NewRow();
                                NonMachineCurrentRow["TreeCode"] = mTreeCode;
                                NonMachineCurrentRow["OperationCode"] = txtCode.Text;
                                NonMachineCurrentRow["MachineCode"] = -1;
                                NonMachineCurrentRow["MachinePriority"] = 0;
                                NonMachineCurrentRow["Operators"] = txtOperator.Value;
                                NonMachineCurrentRow["OneExecutionTime"] = txtOneExecutionTime.Value;
                                NonMachineCurrentRow["TimeType"] = NonMachineTimeType;
                                NonMachineCurrentRow["MinimumProduction"] = txtMinimumProduction.Value;
                                NonMachineCurrentRow["MaximumAccumulation"] = txtMaximumAccumulation.Value;
                                NonMachineCurrentRow["GarbagePercent"] = txtGarbagePercent.Value;
                                NonMachineCurrentRow["PerformancePercent"] = 0;
                                NonMachineCurrentRow["OperatorPerformancePercent"] = 0;
                                NonMachineCurrentRow["RedoPercent"] = txtRedoPercent.Value;
                                NonMachineCurrentRow["ExecutionPerformancePercent"] = 1m - txtGarbagePercent.Value / 100m * 1m * 1m * (txtRedoPercent.Value / 100m);
                                NonMachineCurrentRow["MachineSetupTime"] = 0;
                                NonMachineCurrentRow["SetupTimeType"] = 0;
                                NonMachineCurrentRow["OneExecutionPrice"] = Interaction.IIf(string.IsNullOrEmpty(txtOneExecutionPrice.Text), 0, txtOneExecutionPrice.Text);
                                NonMachineCurrentRow["CalendarCode"] = cbCalendar.SelectedValue;
                                NonMachineCurrentRow["IsParallelMachine"] = 0;
                                dsProductionPlanning.Tables["Tbl_ProductOPCsExecutorMachines"].Rows.Add(NonMachineCurrentRow);
                            }
                            else if (rbContractor.Checked) // در صورتيكه عمليات پيمانكاري باشد
                            {
                                ContractorCurrentRow = dsProductionPlanning.Tables["Tbl_ContractorOperations"].NewRow();
                                ContractorCurrentRow["TreeCode"] = mTreeCode;
                                ContractorCurrentRow["OperationCode"] = txtCode.Text;
                                ContractorCurrentRow["ContractorCode"] = cbContractor.SelectedValue;
                                ContractorCurrentRow["ContractorName"] = cbContractor.Text;
                                ContractorCurrentRow["MinimumTransferBatch"] = txtMimimumBatch.Value;
                                ContractorCurrentRow["TimeType"] = ContractorTimeType;
                                ContractorCurrentRow["TransferBatchExecutionTime"] = txtActivityTime.Value;
                                ContractorCurrentRow["BatchCapacity"] = txtContractorProductionCapacity.Value;
                                ContractorCurrentRow["CalendarCode"] = cbContractorCalendar.SelectedValue;
                                dsProductionPlanning.Tables["Tbl_ContractorOperations"].Rows.Add(ContractorCurrentRow);
                            }

                            if (!chkIsParallelOperation.Checked || !rbMachine.Checked)
                            {
                                {
                                    var withBlock = new SqlCommand("Update Tbl_ProductOPCsExecutorMachines Set IsParallelMachine = 0 Where TreeCode = " + mTreeCode + " And OperationCode = '" + txtCode.Text + "'", Module1.cnProductionPlanning);
                                    withBlock.Transaction = trnProductOpc;
                                    withBlock.ExecuteNonQuery();
                                }
                            }

                            SaveChanges();
                            trnProductOpc.Commit();
                            mdsProductionPlanning.AcceptChanges();
                            Module1.Check_Subbatch_HasPlanningAlarm(mTreeCode, "", "", "1", mAlarmDesc);
                        }
                        catch (Exception objEx)
                        {
                            dsProductionPlanning.Tables["Tbl_ProductOpcs"].RejectChanges();
                            dsProductionPlanning.Tables["Tbl_ProductOPCsExecutorMachines"].RejectChanges();
                            dsProductionPlanning.Tables["Tbl_ContractorOperations"].RejectChanges();
                            trnProductOpc.Rollback();
                            Logger.LogException(Name + ".cmdSave_Click", objEx);
                            MessageBox.Show("ثبت مشخصات عملیات جدید با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        finally
                        {
                            trnProductOpc.Dispose();
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                        }

                        if (rbMachine.Checked)
                        {
                            {
                                var withBlock1 = My.MyProject.Forms.frmExecutorMachine;
                                withBlock1.dsProductionPlanning = mdsProductionPlanning;
                                withBlock1.OpCurrentRow = mCurrentRow;
                                withBlock1.FormMode = (short)Module1.FormModeEnum.INSERT_MODE;
                                withBlock1.cmdExit.Enabled = false;
                                withBlock1.ControlBox = false;
                                withBlock1.ShowDialog();
                                withBlock1.Dispose();
                            }
                        }

                        DialogResult = DialogResult.OK;
                        Close();
                        break;
                    }

                case (short)Module1.FormModeEnum.EDIT_MODE:
                    {
                        try
                        {
                            daProductOPCs.UpdateCommand.Transaction = trnProductOpc;
                            daNonExecutorMachines.InsertCommand.Transaction = trnProductOpc;
                            daNonExecutorMachines.DeleteCommand.Transaction = trnProductOpc;
                            daNonExecutorMachines.UpdateCommand.Transaction = trnProductOpc;
                            daContractorOperation.InsertCommand.Transaction = trnProductOpc;
                            daContractorOperation.UpdateCommand.Transaction = trnProductOpc;
                            daContractorOperation.DeleteCommand.Transaction = trnProductOpc;
                            daMatchedOperation.InsertCommand.Transaction = trnProductOpc;
                            daMatchedOperation.UpdateCommand.Transaction = trnProductOpc;
                            daMatchedOperation.DeleteCommand.Transaction = trnProductOpc;
                            int method = int.Parse(mCurrentRow["ExecutionMethod"].ToString());
                            switch ((EnumExecutionMethod)method)
                            {
                                case EnumExecutionMethod.EM_MACHINE:
                                    {
                                        if (!rbMachine.Checked)
                                        {
                                            var dvExecutorMachines = mdsProductionPlanning.Tables["Tbl_ProductOPCsExecutorMachines"].DefaultView;
                                            dvExecutorMachines.RowFilter = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("OperationCode='", mCurrentRow["Operationcode"]), "' And MachineCode <> '-1'"));
                                            for (I = (short)(dvExecutorMachines.Count - 1); I >= 0; I += -1)
                                                dvExecutorMachines[I].Delete();
                                            dvExecutorMachines.RowFilter = "";
                                            NonMachineCurrentRow = null;
                                            ContractorCurrentRow = null;
                                        }

                                        break;
                                    }

                                case EnumExecutionMethod.EM_NONMACHINE:
                                    {
                                        if (!rbNonMachine.Checked)
                                        {
                                            mdsProductionPlanning.Tables["Tbl_ProductOPCsExecutorMachines"].DefaultView.RowFilter = "";
                                            NonMachineCurrentRow.Delete();
                                            if (rbMachine.Checked)
                                            {
                                                ChangeToMachineMethod = true;
                                            }
                                        }

                                        break;
                                    }

                                case EnumExecutionMethod.EM_CONTRACTOR:
                                    {
                                        if (!rbContractor.Checked)
                                        {
                                            ContractorCurrentRow.Delete();
                                            if (rbMachine.Checked)
                                            {
                                                ChangeToMachineMethod = true;
                                            }
                                        }

                                        break;
                                    }
                            }

                            oldOperationCode = Conversions.ToString(mCurrentRow["OperationCode"]);
                            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(mCurrentRow["OperationCode"], txtCode.Text, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(mCurrentRow["ExecutionMethod"], Interaction.IIf(rbMachine.Checked, 1, Interaction.IIf(rbNonMachine.Checked, 2, 3)), false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(mCurrentRow["IsParallelOperation"], Interaction.IIf(rbMachine.Checked, chkIsParallelOperation.Checked, false), false)))
                            {
                                mAlarmDesc = "مشخصات عملیات: {" + txtCode.Text + "} تغییر کرد";
                            }

                            // تغییر در اطلاعات رکورد جاری
                            mCurrentRow.BeginEdit();
                            mCurrentRow["OperationCode"] = txtCode.Text;
                            mCurrentRow["OperationTitle"] = cbTitle.Text;
                            mCurrentRow["ExecutionMethod"] = Interaction.IIf(rbMachine.Checked, 1, Interaction.IIf(rbNonMachine.Checked, 2, 3));
                            mCurrentRow["ActivityGroup"] = txtGruopCode.Text;
                            mCurrentRow["NatureCode"] = cbNature.SelectedValue;
                            mCurrentRow["IsNotCalcWorking"] = chkIsNotCalcWorking.Checked;
                            mCurrentRow["IsParallelOperation"] = Interaction.IIf(rbMachine.Checked, chkIsParallelOperation.Checked, false);
                            mCurrentRow.EndEdit();
                            if ((oldOperationCode ?? "") != (txtCode.Text ?? ""))
                            {
                                var cmPreOpChange = new SqlCommand("Update Tbl_PreOperations Set PreOperationCode='" + txtCode.Text + "' Where TreeCode=" + mTreeCode + " And PreOperationCode='" + oldOperationCode + "'", Module1.cnProductionPlanning);
                                cmPreOpChange.Transaction = trnProductOpc;
                                cmPreOpChange.ExecuteNonQuery();
                            }

                            if (chkIsHasMatchedOperations.Checked)
                            {
                                foreach (DataRow r in mdsProductionPlanning.Tables["Tbl_MatchedOperations"].Rows)
                                {
                                    if (r.RowState != DataRowState.Deleted)
                                    {
                                        r.BeginEdit();
                                        r["OperationCode"] = txtCode.Text;
                                        r.EndEdit();
                                    }
                                }
                            }
                            else
                            {
                                var mDR = mdsProductionPlanning.Tables["Tbl_MatchedOperations"].Select("OperationCode = '" + oldOperationCode + "' Or MatchedOperationCode = '" + oldOperationCode + "'");
                                foreach (DataRow r in mDR)
                                {
                                    if (r.RowState != DataRowState.Deleted)
                                    {
                                        r.Delete();
                                    }
                                }
                            }

                            if (rbNonMachine.Checked)
                            {
                                if (NonMachineCurrentRow is null)
                                {
                                    dsProductionPlanning.Tables["Tbl_ProductOPCsExecutorMachines"].DefaultView.RowFilter = "OperationCode='" + txtCode.Text + "' And MachineCode='-1'";
                                    NonMachineCurrentRow = dsProductionPlanning.Tables["Tbl_ProductOPCsExecutorMachines"].NewRow();
                                    NonMachineCurrentRow["TreeCode"] = mTreeCode;
                                    NonMachineCurrentRow["OperationCode"] = txtCode.Text;
                                    NonMachineCurrentRow["MachineCode"] = -1;
                                    NonMachineCurrentRow["MachinePriority"] = 0;
                                    NonMachineCurrentRow["Operators"] = txtOperator.Value;
                                    NonMachineCurrentRow["OneExecutionTime"] = txtOneExecutionTime.Value;
                                    NonMachineCurrentRow["TimeType"] = NonMachineTimeType;
                                    NonMachineCurrentRow["MinimumProduction"] = txtMinimumProduction.Value;
                                    NonMachineCurrentRow["MaximumAccumulation"] = txtMaximumAccumulation.Value;
                                    NonMachineCurrentRow["GarbagePercent"] = txtGarbagePercent.Value;
                                    NonMachineCurrentRow["PerformancePercent"] = 0;
                                    NonMachineCurrentRow["OperatorPerformancePercent"] = 0;
                                    NonMachineCurrentRow["RedoPercent"] = txtRedoPercent.Value;
                                    NonMachineCurrentRow["ExecutionPerformancePercent"] = 1m - txtGarbagePercent.Value / 100m * 1m * 1m * (txtRedoPercent.Value / 100m);
                                    NonMachineCurrentRow["MachineSetupTime"] = 0;
                                    NonMachineCurrentRow["SetupTimeType"] = 0;
                                    NonMachineCurrentRow["OneExecutionPrice"] = Interaction.IIf(string.IsNullOrEmpty(txtOneExecutionPrice.Text), 0, txtOneExecutionPrice.Text);
                                    NonMachineCurrentRow["CalendarCode"] = cbCalendar.SelectedValue;
                                    NonMachineCurrentRow["IsParallelMachine"] = 0;
                                    dsProductionPlanning.Tables["Tbl_ProductOPCsExecutorMachines"].Rows.Add(NonMachineCurrentRow);
                                }
                                else
                                {
                                    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(NonMachineCurrentRow["OneExecutionTime"], txtOneExecutionTime.Value, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(NonMachineCurrentRow["MinimumProduction"], txtMinimumProduction.Value, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(NonMachineCurrentRow["MaximumAccumulation"], txtMaximumAccumulation.Value, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(NonMachineCurrentRow["TimeType"], NonMachineTimeType, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(NonMachineCurrentRow["CalendarCode"], cbCalendar.SelectedValue, false)))

                                    {
                                        mAlarmDesc = "مشخصات عملیات: {" + txtCode.Text + "} تغییر کرد";
                                    }

                                    NonMachineCurrentRow.BeginEdit();
                                    NonMachineCurrentRow["MachineCode"] = -1;
                                    NonMachineCurrentRow["MachinePriority"] = 0;
                                    NonMachineCurrentRow["Operators"] = txtOperator.Value;
                                    NonMachineCurrentRow["OneExecutionTime"] = txtOneExecutionTime.Value;
                                    NonMachineCurrentRow["MinimumProduction"] = txtMinimumProduction.Value;
                                    NonMachineCurrentRow["MaximumAccumulation"] = txtMaximumAccumulation.Value;
                                    NonMachineCurrentRow["GarbagePercent"] = txtGarbagePercent.Value;
                                    NonMachineCurrentRow["ExecutionPerformancePercent"] = 1m - txtGarbagePercent.Value / 100m * 1m * 1m * (txtRedoPercent.Value / 100m);
                                    NonMachineCurrentRow["RedoPercent"] = txtRedoPercent.Value;
                                    NonMachineCurrentRow["OneExecutionPrice"] = Interaction.IIf(string.IsNullOrEmpty(txtOneExecutionPrice.Text), 0, txtOneExecutionPrice.Text);
                                    NonMachineCurrentRow["TimeType"] = NonMachineTimeType;
                                    NonMachineCurrentRow["OperatorPerformancePercent"] = 0;
                                    NonMachineCurrentRow["PerformancePercent"] = 0;
                                    NonMachineCurrentRow["MachineSetupTime"] = 0;
                                    NonMachineCurrentRow["SetupTimeType"] = 0;
                                    NonMachineCurrentRow["CalendarCode"] = cbCalendar.SelectedValue;
                                    NonMachineCurrentRow["IsParallelMachine"] = 0;
                                    NonMachineCurrentRow.EndEdit();
                                }
                            }
                            else if (rbContractor.Checked)
                            {
                                if (ContractorCurrentRow is null)
                                {
                                    ContractorCurrentRow = dsProductionPlanning.Tables["Tbl_ContractorOperations"].NewRow();
                                    ContractorCurrentRow["TreeCode"] = mTreeCode;
                                    ContractorCurrentRow["OperationCode"] = txtCode.Text;
                                    ContractorCurrentRow["ContractorCode"] = cbContractor.SelectedValue;
                                    ContractorCurrentRow["ContractorName"] = cbContractor.Text;
                                    ContractorCurrentRow["MinimumTransferBatch"] = txtMimimumBatch.Value;
                                    ContractorCurrentRow["TimeType"] = ContractorTimeType;
                                    ContractorCurrentRow["TransferBatchExecutionTime"] = txtActivityTime.Value;
                                    ContractorCurrentRow["BatchCapacity"] = txtContractorProductionCapacity.Value;
                                    ContractorCurrentRow["CalendarCode"] = cbContractorCalendar.SelectedValue;
                                    dsProductionPlanning.Tables["Tbl_ContractorOperations"].Rows.Add(ContractorCurrentRow);
                                }
                                else
                                {
                                    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(ContractorCurrentRow["ContractorCode"], cbContractor.SelectedValue, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(ContractorCurrentRow["MinimumTransferBatch"], txtMimimumBatch.Value, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(ContractorCurrentRow["TimeType"], ContractorTimeType, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(ContractorCurrentRow["TransferBatchExecutionTime"], txtActivityTime.Value, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(ContractorCurrentRow["BatchCapacity"], txtContractorProductionCapacity.Value, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(ContractorCurrentRow["CalendarCode"], cbContractorCalendar.SelectedValue, false)))

                                    {
                                        mAlarmDesc = "مشخصات عملیات: {" + txtCode.Text + "} تغییر کرد";
                                    }

                                    ContractorCurrentRow.BeginEdit();
                                    ContractorCurrentRow["ContractorCode"] = cbContractor.SelectedValue;
                                    ContractorCurrentRow["MinimumTransferBatch"] = txtMimimumBatch.Value;
                                    ContractorCurrentRow["TimeType"] = ContractorTimeType;
                                    ContractorCurrentRow["TransferBatchExecutionTime"] = txtActivityTime.Value;
                                    ContractorCurrentRow["BatchCapacity"] = txtContractorProductionCapacity.Value;
                                    ContractorCurrentRow["CalendarCode"] = cbContractorCalendar.SelectedValue;
                                    ContractorCurrentRow.EndEdit();
                                }
                            }

                            if (!chkIsParallelOperation.Checked || !rbMachine.Checked)
                            {
                                {
                                    var withBlock2 = new SqlCommand("Update Tbl_ProductOPCsExecutorMachines Set IsParallelMachine = 0 Where TreeCode = " + mTreeCode + " And OperationCode = '" + txtCode.Text + "'", Module1.cnProductionPlanning);
                                    withBlock2.Transaction = trnProductOpc;
                                    withBlock2.ExecuteNonQuery();
                                }
                            }

                            SaveChanges();
                            trnProductOpc.Commit();
                            mdsProductionPlanning.AcceptChanges();
                            if (!string.IsNullOrEmpty(mAlarmDesc))
                            {
                                Module1.Check_Subbatch_HasPlanningAlarm(mTreeCode, "", "", "1", mAlarmDesc);
                            }
                        }
                        catch (Exception objEx)
                        {
                            if (rbNonMachine.Checked && NonMachineCurrentRow is object)
                            {
                                NonMachineCurrentRow.CancelEdit();
                            }
                            else if (rbContractor.Checked && ContractorCurrentRow is object)
                            {
                                ContractorCurrentRow.CancelEdit();
                            }

                            // dsProductionPlanning.Tables("Tbl_ProductOpcs").RejectChanges()
                            // dsProductionPlanning.Tables("Tbl_ProductOPCsExecutorMachines").RejectChanges()
                            // dsProductionPlanning.Tables("Tbl_ContractorOperations").RejectChanges()

                            mCurrentRow.CancelEdit();
                            trnProductOpc.Rollback();
                            Logger.LogException(Name + ".cmdSave_Click", objEx);
                            MessageBox.Show("ثبت تغییرات با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                            trnProductOpc.Dispose();
                        }

                        if (ChangeToMachineMethod)
                        {
                            {
                                var withBlock3 = My.MyProject.Forms.frmExecutorMachine;
                                withBlock3.dsProductionPlanning = mdsProductionPlanning;
                                withBlock3.OpCurrentRow = mCurrentRow;
                                withBlock3.FormMode = (short)Module1.FormModeEnum.INSERT_MODE;
                                withBlock3.cmdExit.Enabled = false;
                                withBlock3.ControlBox = false;
                                withBlock3.ShowDialog();
                                withBlock3.Dispose();
                            }
                        }

                        DialogResult = DialogResult.OK;
                        Close();
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

        private void rbMachine_CheckedChanged(object sender, EventArgs e)
        {
            Size = new Size(new Point(719, 275));
            // chkIsParallelOperation.Visible = True
        }

        private void rbNonMachine_CheckedChanged(object sender, EventArgs e)
        {
            Panel7.Visible = rbNonMachine.Checked;
            Size = new Size(new Point(719, 435));
            if (rbNonMachine.Checked)
            {
                if (mFormMode != (int)Module1.FormModeEnum.INSERT_MODE)
                {
                    var NonMachineRows = dsProductionPlanning.Tables["Tbl_ProductOPCsExecutorMachines"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("OperationCode='", mCurrentRow["OperationCode"]), "' And MachineCode='-1'")));
                    if (NonMachineRows.Length > 0)
                    {
                        NonMachineCurrentRow = NonMachineRows[0];
                        txtOperator.Value = Conversions.ToDecimal(NonMachineCurrentRow["Operators"]);
                        Module1.TimeTypes_enum timeType =  Conversion.CTypeDynamic<Module1.TimeTypes_enum>(NonMachineCurrentRow["TimeType"]);

                        switch (timeType)
                        {
                            case Module1.TimeTypes_enum.TT_SECOND:
                                {
                                    rbSecond.Checked = true;
                                    break;
                                }

                            case Module1.TimeTypes_enum.TT_MINUTE:
                                {
                                    rbMinute.Checked = true;
                                    break;
                                }

                            case Module1.TimeTypes_enum.TT_HOUR:
                                {
                                    rbHour.Checked = true;
                                    break;
                                }

                            case Module1.TimeTypes_enum.TT_DAY:
                                {
                                    rbDay.Checked = true;
                                    break;
                                }

                            case Module1.TimeTypes_enum.TT_WEEK:
                                {
                                    rbWeek.Checked = true;
                                    break;
                                }

                            case Module1.TimeTypes_enum.TT_MONTH:
                                {
                                    rbMonth.Checked = true;
                                    break;
                                }
                        }

                        txtOneExecutionTime.Value = Conversions.ToDecimal(NonMachineCurrentRow["OneExecutionTime"]);
                        txtMinimumProduction.Value = Conversions.ToDecimal(NonMachineCurrentRow["MinimumProduction"]);
                        txtMaximumAccumulation.Value = Conversions.ToDecimal(NonMachineCurrentRow["MaximumAccumulation"]);
                        txtGarbagePercent.Value = Conversions.ToDecimal(NonMachineCurrentRow["GarbagePercent"]);
                        txtRedoPercent.Value = Conversions.ToDecimal(NonMachineCurrentRow["RedoPercent"]);
                        txtOneExecutionPrice.Text = Conversions.ToString(NonMachineCurrentRow["OneExecutionPrice"]);
                        cbCalendar.SelectedValue = NonMachineCurrentRow["CalendarCode"];
                    }
                    else
                    {
                        NonMachineCurrentRow = null;
                    }
                }
            }

            chkIsParallelOperation.Visible = false;
        }

        private void rbContractor_CheckedChanged(object sender, EventArgs e)
        {
            Panel8.Visible = rbContractor.Checked;
            chkIsNotCalcWorking.Visible = !rbContractor.Checked;
            chkIsNotCalcWorking.Checked = false;
            Size = new Size(new Point(719, 407));
            if (rbContractor.Checked)
            {
                if (mFormMode != (int)Module1.FormModeEnum.INSERT_MODE)
                {
                    var ContractorRows = dsProductionPlanning.Tables["Tbl_ContractorOperations"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("OperationCode='", mCurrentRow["OperationCode"]), "'")));
                    if (ContractorRows.Length > 0)
                    {
                        ContractorCurrentRow = ContractorRows[0];
                        cbContractor.SelectedValue = ContractorCurrentRow["ContractorCode"];
                        cbContractorCalendar.SelectedValue = Interaction.IIf(Information.IsDBNull(ContractorCurrentRow["CalendarCode"]), -1, ContractorCurrentRow["CalendarCode"]);
                        txtMimimumBatch.Value = Conversions.ToDecimal(ContractorCurrentRow["MinimumTransferBatch"]);
                        txtActivityTime.Value = Conversions.ToDecimal(ContractorCurrentRow["TransferBatchExecutionTime"]);
                        txtContractorProductionCapacity.Value = Conversions.ToDecimal(ContractorCurrentRow["BatchCapacity"]);
                        switch (ContractorCurrentRow["TimeType"])
                        {
                            case 1:
                                {
                                    rbCSenond.Checked = true;
                                    break;
                                }

                            case 2:
                                {
                                    rbCMinute.Checked = true;
                                    break;
                                }

                            case 3:
                                {
                                    rbCHour.Checked = true;
                                    break;
                                }

                            case 4:
                                {
                                    rbCDay.Checked = true;
                                    break;
                                }

                            case 5:
                                {
                                    rbCWeek.Checked = true;
                                    break;
                                }

                            case 6:
                                {
                                    rbCMonth.Checked = true;
                                    break;
                                }
                        }
                    }
                }
            }

            chkIsParallelOperation.Visible = false;
        }

        private void rbSecond_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSecond.Checked)
            {
                NonMachineTimeType = 1;
            }
        }

        private void rbMinute_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMinute.Checked)
            {
                NonMachineTimeType = 2;
            }
        }

        private void rbHour_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHour.Checked)
            {
                NonMachineTimeType = 3;
            }
        }

        private void rbDay_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDay.Checked)
            {
                NonMachineTimeType = 4;
            }
        }

        private void rbWeek_CheckedChanged(object sender, EventArgs e)
        {
            if (rbWeek.Checked)
            {
                NonMachineTimeType = 5;
            }
        }

        private void rbMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMonth.Checked)
            {
                NonMachineTimeType = 6;
            }
        }

        private void rbCSecond_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCSenond.Checked)
            {
                ContractorTimeType = 1;
            }
        }

        private void rbCMinute_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCMinute.Checked)
            {
                ContractorTimeType = 2;
            }
        }

        private void rbCHour_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCHour.Checked)
            {
                ContractorTimeType = 3;
            }
        }

        private void rbCDay_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCDay.Checked)
            {
                ContractorTimeType = 4;
            }
        }

        private void rbCWeek_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCWeek.Checked)
            {
                ContractorTimeType = 5;
            }
        }

        private void rbCMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCMonth.Checked)
            {
                ContractorTimeType = 6;
            }
        }

        private void FormLoad()
        {
            try
            {
                CreateDataAdapterCommands();
                {
                    var withBlock = cbTitle;
                    withBlock.DataSource = null;
                    withBlock.DataSource = dsProductionPlanning.Tables["Tbl_OperationsDefaultTitles"];
                    withBlock.DisplayMember = "Title";
                    withBlock.ValueMember = "Title";
                }

                {
                    var withBlock1 = cbNature;
                    withBlock1.DataSource = null;
                    withBlock1.DataSource = dsProductionPlanning.Tables["Tbl_Natures"];
                    withBlock1.DisplayMember = "NatureTitle";
                    withBlock1.ValueMember = "NatureCode";
                }

                {
                    var withBlock2 = cbCalendar;
                    withBlock2.DataSource = null;
                    withBlock2.DataSource = dsProductionPlanning.Tables["Tbl_Calendar"];
                    withBlock2.DisplayMember = "CalendarTitle";
                    withBlock2.ValueMember = "CalendarCode";
                }

                {
                    var withBlock3 = cbContractor;
                    withBlock3.DataSource = null;
                    withBlock3.DataSource = dsProductionPlanning.Tables["Tbl_Contractors"];
                    withBlock3.DisplayMember = "ContractorName";
                    withBlock3.ValueMember = "ContractorCode";
                }

                {
                    var withBlock4 = cbContractorCalendar;
                    withBlock4.DataSource = null;
                    withBlock4.DataSource = dsProductionPlanning.Tables["Tbl_Calendar"];
                    withBlock4.DisplayMember = "CalendarTitle";
                    withBlock4.ValueMember = "CalendarCode";
                }

                switch (mFormMode)
                {
                    case (short)Module1.FormModeEnum.INSERT_MODE:
                        {
                            chkIsHasMatchedOperations.Checked = false;
                            chkIsHasMatchedOperations.Enabled = false;
                            txtCode.Focus();
                            break;
                        }

                    case (short)Module1.FormModeEnum.EDIT_MODE:
                        {
                            FillControls();
                            txtCode.Focus();
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

        private void FillControls()
        {
            txtCode.Text = Conversions.ToString(mCurrentRow["OperationCode"]);
            cbTitle.Text = Conversions.ToString(mCurrentRow["OperationTitle"]);
            switch ((EnumExecutionMethod)short.Parse(mCurrentRow["ExecutionMethod"].ToString()))
            {
                case EnumExecutionMethod.EM_MACHINE:
                    {
                        rbMachine.Checked = true;
                        break;
                    }

                case EnumExecutionMethod.EM_NONMACHINE:
                    {
                        rbNonMachine.Checked = true;
                        break;
                    }

                case EnumExecutionMethod.EM_CONTRACTOR:
                    {
                        rbContractor.Checked = true;
                        break;
                    }
            }

            lblPriority.Text = Conversions.ToString(mCurrentRow["OperationPriority"]);
            OperationPriority = Conversions.ToInteger(lblPriority.Text);
            cbNature.SelectedValue = mCurrentRow["NatureCode"];
            txtGruopCode.Text = Conversions.ToString(mCurrentRow["ActivityGroup"]);
            if (Information.IsDBNull(mCurrentRow["IsNotCalcWorking"]))
            {
                chkIsNotCalcWorking.Checked = false;
                mCurrentRow["IsNotCalcWorking"] = false;
            }
            else
            {
                chkIsNotCalcWorking.Checked = Conversions.ToBoolean(mCurrentRow["IsNotCalcWorking"]);
            }

            if (Information.IsDBNull(mCurrentRow["IsParallelOperation"]))
            {
                chkIsNotCalcWorking.Checked = false;
                mCurrentRow["IsParallelOperation"] = false;
            }
            else
            {
                chkIsNotCalcWorking.Checked = Conversions.ToBoolean(mCurrentRow["IsNotCalcWorking"]);
            }

            if (!string.IsNullOrEmpty(Module1.GetMatchedOperations(mCurrentRow["OperationCode"].ToString(), mTreeCode)))
            {
                chkIsHasMatchedOperations.Checked = true;
                txtCode.Enabled = false;
                pnlExecMethod.Enabled = false;
            }
            else
            {
                chkIsHasMatchedOperations.Checked = false;
                txtCode.Enabled = true;
                pnlExecMethod.Enabled = true;
            }
        }

        private void SaveChanges()
        {
            var dsChanges = mdsProductionPlanning.GetChanges();
            if (dsChanges.HasErrors)
            {
                mdsProductionPlanning.RejectChanges();
            }
            else if (mFormMode == (int)Module1.FormModeEnum.EDIT_MODE)
            {
                if ((oldOperationCode ?? "") != (txtCode.Text ?? ""))
                {
                    daNonExecutorMachines.Update(dsChanges, "Tbl_ProductOPCsExecutorMachines");
                    daContractorOperation.Update(dsChanges, "Tbl_ContractorOperations");
                    daProductOPCs.Update(dsChanges, "Tbl_ProductOpcs");
                }
                else
                {
                    daProductOPCs.Update(dsChanges, "Tbl_ProductOpcs");
                    daNonExecutorMachines.Update(dsChanges, "Tbl_ProductOPCsExecutorMachines");
                    daContractorOperation.Update(dsChanges, "Tbl_ContractorOperations");
                }

                daMatchedOperation.Update(dsChanges, "Tbl_MatchedOperations");
            }
            else if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
            {
                daMatchedOperation.Update(dsChanges, "Tbl_MatchedOperations");
                daProductOPCs.Update(dsChanges, "Tbl_ProductOpcs");
                daNonExecutorMachines.Update(dsChanges, "Tbl_ProductOPCsExecutorMachines");
                daContractorOperation.Update(dsChanges, "Tbl_ContractorOperations");
            }
            else
            {
                daProductOPCs.Update(dsChanges, "Tbl_ProductOpcs");
                daNonExecutorMachines.Update(dsChanges, "Tbl_ProductOPCsExecutorMachines");
                daContractorOperation.Update(dsChanges, "Tbl_ContractorOperations");
                daMatchedOperation.Update(dsChanges, "Tbl_MatchedOperations");
            }

            dsChanges = null;
        }

        private void CreateDataAdapterCommands()
        {
            // -------------------- ایجاد دستورات جدول عملیات ساخت محصول ----------------------
            // ایجاد دستور اضافه کردن رکورد جدید در جدول
            daProductOPCs.InsertCommand = new SqlCommand("Insert Into Tbl_ProductOPCs(TreeCode,DetailCode,OperationCode,OperationTitle,HavePreOperation,HavePreItem,ExecutionMethod,OperationPriority,ActivityGroup,NatureCode,IsNotCalcWorking,IsParallelOperation) " + "Values(@TreeCode,@DetailCode,@OperationCode,@OperationTitle,@HavePreOperation,@HavePreItem,@ExecutionMethod,@OperationPriority,@ActivityGroup,@NatureCode,@IsNotCalcWorking,@IsParallelOperation)", Module1.cnProductionPlanning);
            {
                var withBlock = daProductOPCs.InsertCommand;
                withBlock.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode");
                withBlock.Parameters.Add("@DetailCode", SqlDbType.VarChar, 50, "DetailCode");
                withBlock.Parameters.Add("@OperationCode", SqlDbType.VarChar, 50, "OperationCode");
                withBlock.Parameters.Add("@OperationTitle", SqlDbType.VarChar, 100, "OperationTitle");
                withBlock.Parameters.Add("@HavePreOperation", SqlDbType.TinyInt, default, "HavePreOperation");
                withBlock.Parameters.Add("@HavePreItem", SqlDbType.TinyInt, default, "HavePreItem");
                withBlock.Parameters.Add("@ExecutionMethod", SqlDbType.TinyInt, default, "ExecutionMethod");
                withBlock.Parameters.Add("@OperationPriority", SqlDbType.SmallInt, default, "OperationPriority");
                withBlock.Parameters.Add("@ActivityGroup", SqlDbType.VarChar, 50, "ActivityGroup");
                withBlock.Parameters.Add("@NatureCode", SqlDbType.SmallInt, default, "NatureCode");
                withBlock.Parameters.Add("@IsNotCalcWorking", SqlDbType.Bit, default, "IsNotCalcWorking");
                withBlock.Parameters.Add("@IsParallelOperation", SqlDbType.Bit, default, "IsParallelOperation");
            }
            // ایجاد دستور اصلاح رکورد جاری در جدول
            daProductOPCs.UpdateCommand = new SqlCommand("Update Tbl_ProductOPCs Set OperationCode=@CurrentOperationCode,OperationTitle=@OperationTitle,HavePreOperation=@HavePreOperation,HavePreItem=@HavePreItem,ExecutionMethod=@ExecutionMethod,ActivityGroup=@ActivityGroup,NatureCode=@NatureCode,IsNotCalcWorking=@IsNotCalcWorking,IsParallelOperation=@IsParallelOperation Where TreeCode=@TreeCode And OperationCode=@OldOperationCode", Module1.cnProductionPlanning);
            {
                var withBlock1 = daProductOPCs.UpdateCommand;
                withBlock1.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode").SourceVersion = DataRowVersion.Original;
                withBlock1.Parameters.Add("@CurrentOperationCode", SqlDbType.VarChar, 50, "OperationCode").SourceVersion = DataRowVersion.Current;
                withBlock1.Parameters.Add("@OldOperationCode", SqlDbType.VarChar, 50, "OperationCode").SourceVersion = DataRowVersion.Original;
                withBlock1.Parameters.Add("@OperationTitle", SqlDbType.VarChar, 100, "OperationTitle");
                withBlock1.Parameters.Add("@HavePreOperation", SqlDbType.TinyInt, default, "HavePreOperation");
                withBlock1.Parameters.Add("@HavePreItem", SqlDbType.TinyInt, default, "HavePreItem");
                withBlock1.Parameters.Add("@ExecutionMethod", SqlDbType.TinyInt, default, "ExecutionMethod");
                withBlock1.Parameters.Add("@ActivityGroup", SqlDbType.VarChar, 50, "ActivityGroup");
                withBlock1.Parameters.Add("@NatureCode", SqlDbType.SmallInt, default, "NatureCode");
                withBlock1.Parameters.Add("@IsNotCalcWorking", SqlDbType.Bit, default, "IsNotCalcWorking");
                withBlock1.Parameters.Add("@IsParallelOperation", SqlDbType.Bit, default, "IsParallelOperation");
            }
            // ایجاد دستور حذف رکورد جاری در جدول
            daProductOPCs.DeleteCommand = new SqlCommand("Delete From Tbl_ProductOPCs Where TreeCode=@TreeCode And OperationCode=@OperationCode", Module1.cnProductionPlanning);
            {
                var withBlock2 = daProductOPCs.DeleteCommand;
                withBlock2.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode").SourceVersion = DataRowVersion.Original;
                withBlock2.Parameters.Add("@OperationCode", SqlDbType.VarChar, 50, "OperationCode").SourceVersion = DataRowVersion.Original;
            }

            // -------------------- ایجاد دستورات جدول ماشین آلات انجام دهنده عملیات ----------------------
            // ایجاد دستور اضافه کردن رکورد جدید در جدول
            daNonExecutorMachines.InsertCommand = new SqlCommand("Insert Into Tbl_ProductOPCsExecutorMachines(TreeCode,OperationCode,MachineCode,MachinePriority,Operators,OneExecutionTime,TimeType,MinimumProduction,MaximumAccumulation,GarbagePercent,PerformancePercent,OperatorPerformancePercent,RedoPercent,ExecutionPerformancePercent,MachineSetupTime,SetupTimeType,OneExecutionPrice,CalendarCode,IsParallelMachine) " + "Values(@TreeCode,@OperationCode,@MachineCode,@MachinePriority,@Operators,@OneExecutionTime,@TimeType,@MinimumProduction,@MaximumAccumulation,@GarbagePercent,@PerformancePercent,@OperatorPerformancePercent,@RedoPercent,@ExecutionPerformancePercent,@MachineSetupTime,@SetupTimeType,@OneExecutionPrice,@CalendarCode,@IsParallelMachine)", Module1.cnProductionPlanning);
            {
                var withBlock3 = daNonExecutorMachines.InsertCommand;
                withBlock3.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode");
                withBlock3.Parameters.Add("@OperationCode", SqlDbType.VarChar, 50, "OperationCode");
                withBlock3.Parameters.Add("@MachineCode", SqlDbType.VarChar, 50, "MachineCode");
                withBlock3.Parameters.Add("@MachinePriority", SqlDbType.TinyInt, default, "MachinePriority");
                withBlock3.Parameters.Add("@Operators", SqlDbType.TinyInt, default, "Operators");
                withBlock3.Parameters.Add("@OneExecutionTime", SqlDbType.Real, default, "OneExecutionTime");
                withBlock3.Parameters.Add("@TimeType", SqlDbType.TinyInt, default, "TimeType");
                withBlock3.Parameters.Add("@MinimumProduction", SqlDbType.Int, default, "MinimumProduction");
                withBlock3.Parameters.Add("@MaximumAccumulation", SqlDbType.Int, default, "MaximumAccumulation");
                withBlock3.Parameters.Add("@GarbagePercent", SqlDbType.TinyInt, default, "GarbagePercent");
                withBlock3.Parameters.Add("@PerformancePercent", SqlDbType.TinyInt, default, "PerformancePercent");
                withBlock3.Parameters.Add("@OperatorPerformancePercent", SqlDbType.TinyInt, default, "OperatorPerformancePercent");
                withBlock3.Parameters.Add("@RedoPercent", SqlDbType.TinyInt, default, "RedoPercent");
                withBlock3.Parameters.Add("@ExecutionPerformancePercent", SqlDbType.VarChar, 50, "ExecutionPerformancePercent");
                withBlock3.Parameters.Add("@MachineSetupTime", SqlDbType.Int, default, "MachineSetupTime");
                withBlock3.Parameters.Add("@SetupTimeType", SqlDbType.TinyInt, default, "SetupTimeType");
                withBlock3.Parameters.Add("@OneExecutionPrice", SqlDbType.Decimal, default, "OneExecutionPrice");
                withBlock3.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode");
                withBlock3.Parameters.Add("@IsParallelMachine", SqlDbType.Bit, default, "IsParallelMachine");
            }
            // ایجاد دستور اصلاح رکورد جاری در جدول
            daNonExecutorMachines.UpdateCommand = new SqlCommand("Update Tbl_ProductOPCsExecutorMachines Set MachineCode=@CurrentMachineCode,MachinePriority=@CurrentMachinePriority," + "Operators=@Operators,OneExecutionTime=@OneExecutionTime,TimeType=@TimeType,MinimumProduction=@MinimumProduction," + "MaximumAccumulation=@MaximumAccumulation,GarbagePercent=@GarbagePercent,PerformancePercent=@PerformancePercent," + "OperatorPerformancePercent=@OperatorPerformancePercent,RedoPercent=@RedoPercent,ExecutionPerformancePercent=@ExecutionPerformancePercent,MachineSetupTime=@MachineSetupTime," + "SetupTimeType=@SetupTimeType,OneExecutionPrice=@OneExecutionPrice,CalendarCode=@CalendarCode,IsParallelMachine=@IsParallelMachine Where TreeCode=@TreeCode And OperationCode=@OperationCode" + " And MachineCode=@OldMachineCode And MachinePriority=@OldMachinePriority", Module1.cnProductionPlanning);




            {
                var withBlock4 = daNonExecutorMachines.UpdateCommand;
                withBlock4.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode").SourceVersion = DataRowVersion.Original;
                withBlock4.Parameters.Add("@OperationCode", SqlDbType.VarChar, 50, "OperationCode").SourceVersion = DataRowVersion.Original;
                withBlock4.Parameters.Add("@CurrentMachineCode", SqlDbType.VarChar, 50, "MachineCode").SourceVersion = DataRowVersion.Current;
                withBlock4.Parameters.Add("@OldMachineCode", SqlDbType.VarChar, 50, "MachineCode").SourceVersion = DataRowVersion.Original;
                withBlock4.Parameters.Add("@CurrentMachinePriority", SqlDbType.TinyInt, default, "MachinePriority").SourceVersion = DataRowVersion.Current;
                withBlock4.Parameters.Add("@OldMachinePriority", SqlDbType.TinyInt, default, "MachinePriority").SourceVersion = DataRowVersion.Original;
                withBlock4.Parameters.Add("@Operators", SqlDbType.TinyInt, default, "Operators");
                withBlock4.Parameters.Add("@OneExecutionTime", SqlDbType.Real, default, "OneExecutionTime");
                withBlock4.Parameters.Add("@TimeType", SqlDbType.TinyInt, default, "TimeType");
                withBlock4.Parameters.Add("@MinimumProduction", SqlDbType.Int, default, "MinimumProduction");
                withBlock4.Parameters.Add("@MaximumAccumulation", SqlDbType.Int, default, "MaximumAccumulation");
                withBlock4.Parameters.Add("@GarbagePercent", SqlDbType.TinyInt, default, "GarbagePercent");
                withBlock4.Parameters.Add("@PerformancePercent", SqlDbType.TinyInt, default, "PerformancePercent");
                withBlock4.Parameters.Add("@OperatorPerformancePercent", SqlDbType.TinyInt, default, "OperatorPerformancePercent");
                withBlock4.Parameters.Add("@RedoPercent", SqlDbType.TinyInt, default, "RedoPercent");
                withBlock4.Parameters.Add("@ExecutionPerformancePercent", SqlDbType.VarChar, 50, "ExecutionPerformancePercent");
                withBlock4.Parameters.Add("@MachineSetupTime", SqlDbType.Int, default, "MachineSetupTime");
                withBlock4.Parameters.Add("@SetupTimeType", SqlDbType.TinyInt, default, "SetupTimeType");
                withBlock4.Parameters.Add("@OneExecutionPrice", SqlDbType.Decimal, default, "OneExecutionPrice");
                withBlock4.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode");
                withBlock4.Parameters.Add("@IsParallelMachine", SqlDbType.Bit, default, "IsParallelMachine");
            }
            // ایجاد دستور حذف رکورد جاری در جدول
            daNonExecutorMachines.DeleteCommand = new SqlCommand("Delete From Tbl_ProductOPCsExecutorMachines Where TreeCode=@TreeCode And OperationCode=@OperationCode And MachineCode=@MachineCode", Module1.cnProductionPlanning);
            {
                var withBlock5 = daNonExecutorMachines.DeleteCommand;
                withBlock5.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode").SourceVersion = DataRowVersion.Original;
                withBlock5.Parameters.Add("@OperationCode", SqlDbType.VarChar, 50, "OperationCode").SourceVersion = DataRowVersion.Original;
                withBlock5.Parameters.Add("@MachineCode", SqlDbType.VarChar, 50, "MachineCode").SourceVersion = DataRowVersion.Original;
            }

            // -------------------- ایجاد دستورات جدول عملیات پیمانکاری ----------------------
            // ایجاد دستور اضافه کردن رکورد جدید در جدول
            daContractorOperation.InsertCommand = new SqlCommand("Insert Into Tbl_ContractorOperations(TreeCode,OperationCode,ContractorCode,MinimumTransferBatch,TimeType,TransferBatchExecutionTime,BatchCapacity,CalendarCode) " + "Values(@TreeCode,@OperationCode,@ContractorCode,@MinimumTransferBatch,@TimeType,@TransferBatchExecutionTime,@BatchCapacity,@CalendarCode)", Module1.cnProductionPlanning);
            {
                var withBlock6 = daContractorOperation.InsertCommand;
                withBlock6.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode");
                withBlock6.Parameters.Add("@OperationCode", SqlDbType.VarChar, 50, "OperationCode");
                withBlock6.Parameters.Add("@ContractorCode", SqlDbType.VarChar, 50, "ContractorCode");
                withBlock6.Parameters.Add("@MinimumTransferBatch", SqlDbType.Int, default, "MinimumTransferBatch");
                withBlock6.Parameters.Add("@TimeType", SqlDbType.TinyInt, default, "TimeType");
                withBlock6.Parameters.Add("@TransferBatchExecutionTime", SqlDbType.Int, default, "TransferBatchExecutionTime");
                withBlock6.Parameters.Add("@BatchCapacity", SqlDbType.Int, default, "BatchCapacity");
                withBlock6.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode");
            }
            // ایجاد دستور اصلاح رکورد جاری در جدول
            daContractorOperation.UpdateCommand = new SqlCommand("Update Tbl_ContractorOperations Set ContractorCode=@CurrentContractorCode,MinimumTransferBatch=@MinimumTransferBatch,TimeType=@TimeType," + "TransferBatchExecutionTime=@TransferBatchExecutionTime,BatchCapacity=@BatchCapacity,CalendarCode=@CalendarCode " + "Where TreeCode=@TreeCode And OperationCode=@OperationCode And ContractorCode=@OldContractorCode", Module1.cnProductionPlanning);

            {
                var withBlock7 = daContractorOperation.UpdateCommand;
                withBlock7.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode").SourceVersion = DataRowVersion.Original;
                withBlock7.Parameters.Add("@OperationCode", SqlDbType.VarChar, 50, "OperationCode").SourceVersion = DataRowVersion.Original;
                withBlock7.Parameters.Add("@CurrentContractorCode", SqlDbType.VarChar, 50, "ContractorCode").SourceVersion = DataRowVersion.Current;
                withBlock7.Parameters.Add("@OldContractorCode", SqlDbType.VarChar, 50, "ContractorCode").SourceVersion = DataRowVersion.Original;
                withBlock7.Parameters.Add("@MinimumTransferBatch", SqlDbType.Int, default, "MinimumTransferBatch");
                withBlock7.Parameters.Add("@TimeType", SqlDbType.TinyInt, default, "TimeType");
                withBlock7.Parameters.Add("@TransferBatchExecutionTime", SqlDbType.Int, default, "TransferBatchExecutionTime");
                withBlock7.Parameters.Add("@BatchCapacity", SqlDbType.Int, default, "BatchCapacity");
                withBlock7.Parameters.Add("@CalendarCode", SqlDbType.Int, default, "CalendarCode");
            }
            // ایجاد دستور حذف رکورد جاری در جدول
            daContractorOperation.DeleteCommand = new SqlCommand("Delete From Tbl_ContractorOperations Where TreeCode=@TreeCode And OperationCode=@OperationCode And ContractorCode=@ContractorCode", Module1.cnProductionPlanning);
            {
                var withBlock8 = daContractorOperation.DeleteCommand;
                withBlock8.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode").SourceVersion = DataRowVersion.Original;
                withBlock8.Parameters.Add("@OperationCode", SqlDbType.VarChar, 50, "OperationCode").SourceVersion = DataRowVersion.Original;
                withBlock8.Parameters.Add("@ContractorCode", SqlDbType.VarChar, 50, "ContractorCode").SourceVersion = DataRowVersion.Original;
            }

            // -------------------- ایجاد دستورات جدول عملیات های جفت ----------------------
            // ایجاد دستور اضافه کردن رکورد جدید در جدول
            daMatchedOperation.InsertCommand = new SqlCommand("Insert Into Tbl_MatchedOperations(TreeCode,OperationCode,MatchedOperationCode) " + "Values(@TreeCode,@OperationCode,@MatchedOperationCode)", Module1.cnProductionPlanning);
            {
                var withBlock9 = daMatchedOperation.InsertCommand;
                withBlock9.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode");
                withBlock9.Parameters.Add("@OperationCode", SqlDbType.VarChar, 50, "OperationCode");
                withBlock9.Parameters.Add("@MatchedOperationCode", SqlDbType.VarChar, 50, "MatchedOperationCode");
            }
            // ایجاد دستور اصلاح رکورد جاری در جدول
            daMatchedOperation.UpdateCommand = new SqlCommand("Update Tbl_MatchedOperations Set OperationCode=@C_OperationCode,MatchedOperationCode=@C_MatchedOperationCode " + "Where TreeCode=@TreeCode And OperationCode=@OperationCode And MatchedOperationCode=@MatchedOperationCode", Module1.cnProductionPlanning);
            {
                var withBlock10 = daMatchedOperation.UpdateCommand;
                withBlock10.Parameters.Add("@C_OperationCode", SqlDbType.VarChar, 50, "OperationCode").SourceVersion = DataRowVersion.Current;
                withBlock10.Parameters.Add("@C_MatchedOperationCode", SqlDbType.VarChar, 50, "MatchedOperationCode").SourceVersion = DataRowVersion.Current;
                withBlock10.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode").SourceVersion = DataRowVersion.Original;
                withBlock10.Parameters.Add("@OperationCode", SqlDbType.VarChar, 50, "OperationCode").SourceVersion = DataRowVersion.Original;
                withBlock10.Parameters.Add("@MatchedOperationCode", SqlDbType.VarChar, 50, "MatchedOperationCode").SourceVersion = DataRowVersion.Original;
            }
            // ایجاد دستور حذف رکورد جاری در جدول
            daMatchedOperation.DeleteCommand = new SqlCommand("Delete From Tbl_MatchedOperations Where TreeCode=@TreeCode And OperationCode=@OperationCode And MatchedOperationCode=@MatchedOperationCode", Module1.cnProductionPlanning);
            {
                var withBlock11 = daMatchedOperation.DeleteCommand;
                withBlock11.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode").SourceVersion = DataRowVersion.Original;
                withBlock11.Parameters.Add("@OperationCode", SqlDbType.VarChar, 50, "OperationCode").SourceVersion = DataRowVersion.Original;
                withBlock11.Parameters.Add("@MatchedOperationCode", SqlDbType.VarChar, 50, "MatchedOperationCode").SourceVersion = DataRowVersion.Original;
            }
        }

        private bool ValidationForm(object sender)
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                MessageBox.Show("کد عملیات را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtCode.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(cbTitle.Text))
            {
                MessageBox.Show("عنوان عملیات را انتخاب یا وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbTitle.Focus();
                return false;
            }

            if (cbNature.SelectedIndex == -1)
            {
                MessageBox.Show("بخش تولید را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbNature.Focus();
                return false;
            }

            if (!ReferenceEquals(sender, cmdSelectMatchedOperations) && chkIsHasMatchedOperations.Checked && dsProductionPlanning.Tables["Tbl_MatchedOperations"].Rows.Count > 0)
            {
                int SeletedExecMethod = Conversions.ToInteger(Interaction.IIf(rbMachine.Checked, EnumExecutionMethod.EM_MACHINE, Interaction.IIf(rbNonMachine.Checked, EnumExecutionMethod.EM_NONMACHINE, EnumExecutionMethod.EM_CONTRACTOR)));
                if (Conversions.ToInteger(mCurrentRow["ExecutionMethod"]) != SeletedExecMethod)
                {
                    MessageBox.Show("در صورت انتخاب عملیات همزمان امکان تغییر نحوۀ اجرای عملیات وجود ندارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return false;
                }

                foreach (DataRow r in dsProductionPlanning.Tables["Tbl_MatchedOperations"].Rows)
                {
                    if (r.RowState != DataRowState.Deleted)
                    {
                        if (!ValidateMatches(r))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private bool ValidateMatches(DataRow MatchRow)
        {
            var drBase = dsProductionPlanning.Tables["Tbl_ProductOPCs"].Select("OperationCode = '" + MatchRow["OperationCode"].ToString() + "'");
            var drMatch = dsProductionPlanning.Tables["Tbl_ProductOPCs"].Select("OperationCode = '" + MatchRow["MatchedOperationCode"].ToString() + "'");
            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(drBase[0]["ExecutionMethod"], drMatch[0]["ExecutionMethod"], false)))
            {
                MessageBox.Show("نحوۀ اجرای عملیات های همزمان باید یکسان باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                return false;
            }

            if (Conversions.ToInteger(mCurrentRow["ExecutionMethod"]) == (int)EnumExecutionMethod.EM_MACHINE)
            {
                // کنترل اینکه ماشین(های) انجام دهندۀ عملیات های همزمان با عملیات جاری یکسان باشد
                var mOpExecMachines = dsProductionPlanning.Tables["Tbl_ProductOPCsExecutorMachines"].Select("OperationCode = '" + mCurrentRow["OperationCode"].ToString() + "'");
                foreach (DataRow ExecMachineRow in mOpExecMachines)
                {
                    var mdrMatchMachine = dsProductionPlanning.Tables["Tbl_ProductOPCsExecutorMachines"].Select("OperationCode = '" + MatchRow["OperationCode"].ToString() + "' And MachineCode = '" + ExecMachineRow["MachineCode"].ToString() + "'");
                    if (mdrMatchMachine.Length == 0)
                    {
                        MessageBox.Show("تمامی ماشین های انجام دهندۀ عملیات های همزمان باید یکسان باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        return false;
                    }
                    else
                    {
                        mdrMatchMachine = dsProductionPlanning.Tables["Tbl_ProductOPCsExecutorMachines"].Select("OperationCode = '" + MatchRow["MatchedOperationCode"].ToString() + "' And MachineCode = '" + ExecMachineRow["MachineCode"].ToString() + "'");
                        if (mdrMatchMachine.Length == 0)
                        {
                            MessageBox.Show("تمامی ماشین های انجام دهندۀ عملیات های همزمان باید یکسان باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private void chkIsHasMatchedOperations_CheckedChanged(object sender, EventArgs e)
        {
            cmdSelectMatchedOperations.Enabled = chkIsHasMatchedOperations.Checked;
        }
    }
}