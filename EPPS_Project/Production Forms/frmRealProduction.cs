using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using static ProductionPlanning.MyEnums;

namespace ProductionPlanning
{
    public partial class frmRealProduction
    {
        public frmRealProduction()
        {
            InitializeComponent();
            _cmdSave.Name = "cmdSave";
            _cmdExit.Name = "cmdExit";
            _cmdDelete.Name = "cmdDelete";
            _cbSubbatch.Name = "cbSubbatch";
            _cbOperation.Name = "cbOperation";
            _cmdCalcProductionQty.Name = "cmdCalcProductionQty";
            _cmdNewOperationUnit.Name = "cmdNewOperationUnit";
            _cbOperators.Name = "cbOperators";
            _cbBatch.Name = "cbBatch";
            _cmdAddOperator.Name = "cmdAddOperator";
            _cmdRemoveOperator.Name = "cmdRemoveOperator";
        }

        private frmRealProductionList mListForm;
        private DataSetConfiguration DataSetConfig = new DataSetConfiguration();
        private SqlDataAdapter mdaProduction = new SqlDataAdapter();
        private SqlDataAdapter mdaHalt = new SqlDataAdapter();
        //private string oldFilter;
        private long mPlanningCode = 0L;
        private short I;
        private int ExecutionMethod;

        // -------------------------------------
        private DataSet dsRealProductionNew;
        private MyDB RefData;
        private DataRow CurrentDataRow; // برای نگهداری رکورد جاری
        private bool LoadingComboData = true;

        public frmRealProductionList 
            
            ListForm
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

        // Public ReadOnly Property dsRealProduction() As DataSet
        // Get
        // Return ListForm.dsRealProduction
        // End Get
        // End Property

        public DataSet RealProduction_DataSet
        {
            set
            {
                dsRealProductionNew = value;
            }

            get
            {
                return dsRealProductionNew;
            }
        }

        //private string newPropertyValue;

        public DataRow Current_DataRow
        {
            get
            {
                return CurrentDataRow;
            }

            set
            {
                CurrentDataRow = value;
            }
        }

        private string GetCalendarCode(string MachineCode)
        {
            string CalendarCode = "0";
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmCalendar = new SqlCommand("", cn);
                SqlDataReader drCalendar = null;
                switch (MachineCode ?? "")
                {
                    case "-1":
                        {
                            cmCalendar.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select CalendarCode From dbo.Tbl_ProductOPCsExecutorMachines Where TreeCode = ", cbSubbatch.SelectedValue), " And MachineCode = '"), MachineCode), "' And OperationCode = '"), GetOperationCode()), "'"));
                            drCalendar = cmCalendar.ExecuteReader();
                            if (drCalendar.Read())
                            {
                                CalendarCode = Conversions.ToString(drCalendar["CalendarCode"]);
                            }

                            drCalendar.Close();
                            break;
                        }

                    default:
                        {
                            cmCalendar.CommandText = "Select CalendarCode From Tbl_Machines Where Code = '" + MachineCode + "'";
                            drCalendar = cmCalendar.ExecuteReader();
                            if (drCalendar.Read())
                            {
                                CalendarCode = Conversions.ToString(drCalendar["CalendarCode"]);
                            }

                            drCalendar.Close();
                            break;
                        }
                }

                cn.Close();
            }

            return CalendarCode;
        }

        private void frmRealProduction_Load(object sender, EventArgs e)
        {
            LoadingComboData = true;
            zzSetFormControls();
            zzLoaData();
            LoadingComboData = false;
        }

        private void frmRealProduction_FormClosing(object sender, FormClosingEventArgs e)
        {
            // TODO:
            if (dsRealProductionNew is object)
                dsRealProductionNew.RejectChanges();

            // For I = dsRealProduction.Tables.Count - 1 To 3 Step -1
            // dsRealProduction.Tables(I).Dispose()
            // dsRealProduction.Tables.RemoveAt(I)
            // Next I

            DataSetConfig = null;
            mdaProduction.Dispose();
            mdaHalt.Dispose();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdCalcProductionQty_Click(object sender, EventArgs e)
        {
            if (cbOperationUnits.Items.Count > 0 && cbOperationUnits.SelectedValue is object && cbOperationUnits.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                txtIntactQty.Value = (decimal)(Conversions.ToDouble(cbOperationUnits.SelectedValue.ToString()) * (double)txtFunctionStatistics.Value);
            }

            if (cbGarbageOpUnits.Items.Count > 0 && cbGarbageOpUnits.SelectedValue is object && cbGarbageOpUnits.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                txtGarbageQty.Value = (decimal)(Conversions.ToDouble(cbGarbageOpUnits.SelectedValue.ToString()) * (double)txtGarbageFunStatistics.Value);
            }
        }

        private void cmdNewOperationUnit_Click(object sender, EventArgs e)
        {
            string Query = "Select   Distinct A.OperationCode,A.OperationCode+' '+A.OperationTitle As OperationTitle " + "From     dbo.Tbl_ProductOPCs A INNER JOIN dbo.Tbl_Planning B ON A.TreeCode=B.TreeCode " + "         AND A.OperationCode=B.OperationCode " + "Order By A.OperationCode";
            My.MyProject.Forms.frmRealProductionList.MyDB.FillDataSet("TblOperations", "TblOperations", Query, "OperationCode");
            My.MyProject.Forms.frmRealProductionList.MyDB.FillDataSet("Tbl_TestUnits", "Tbl_TestUnits", "Select * From Tbl_TestUnits", "Code");
            {
                var withBlock = My.MyProject.Forms.frmOperationUnit;
                withBlock.dsOperationUnit = My.MyProject.Forms.frmRealProductionList.MyDB.dsProductionPlanning;
                withBlock.SetFormMode((short)Module1.FormModeEnum.INSERT_MODE);
                withBlock.OperationCode = GetOperationCode();
                withBlock.ShowDialog();
            }

            RefData.DataSet.Tables["Tbl_MeasurementUnit2"].Dispose();
            RefData.DataSet.Tables.Remove("Tbl_MeasurementUnit2");
            Query = "Select A.OperationCode,A.UnitCode,B.Title AS UnitTitle,A.UnitIndex " + "From   dbo.Tbl_Operation_Measurement_UnitIndex A INNER JOIN " + "       dbo.Tbl_TestUnits B ON A.UnitCode = B.Code";
            My.MyProject.Forms.frmRealProductionList.MyDB.FillDataSet("Tbl_Operation_Measurement_UnitIndex", "Tbl_MeasurementUnit2", Query, "OperationCode", "UnitCode");
            RefData.DataSet.Tables["Tbl_MeasurementUnit2"].DefaultView.RowFilter = RefData.DataSet.Tables["Tbl_MeasurementUnit1"].DefaultView.RowFilter;
            {
                var withBlock1 = cbGarbageOpUnits;
                withBlock1.DataSource = null;
                withBlock1.DataSource = RefData.DataSet.Tables["Tbl_MeasurementUnit2"].DefaultView;
                withBlock1.DisplayMember = "UnitTitle";
                withBlock1.ValueMember = "UnitIndex";
            }
            // frmRealProductionList.DataSetConfig.RefreshDataSet("Tbl_MeasurementUnit2")
        }

        private void cmdAddOperator_Click(object sender, EventArgs e)
        {
            bool IsHasOperator = false;
            if (cbOperators.SeletedValue is object)
            {
                foreach (DataGridViewRow r in dgOperators.Rows)
                {
                    if (r.Cells[0].Value.ToString().Equals(cbOperators.SeletedValue.ToString()))
                    {
                        IsHasOperator = true;
                        break;
                    }
                }

                if (IsHasOperator)
                {
                    MessageBox.Show("این اپراتور قبلا اضافه شده است", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                }
                else
                {
                    dgOperators.Rows.Add(cbOperators.SeletedValue, cbOperators.SeletedDisplay);
                }
            }
        }

        private void cmdRemoveOperator_Click(object sender, EventArgs e)
        {
            if (dgOperators.SelectedRows.Count > 0)
            {
                dgOperators.Rows.RemoveAt(dgOperators.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("اپراتوری انتخاب نشده است", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("اطلاعات تولید را حذف می کنید", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading, false) == DialogResult.Yes)
            {
                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();
                var trnDelete = Module1.cnProductionPlanning.BeginTransaction();
                try
                {
                    mdaProduction.DeleteCommand.Transaction = trnDelete;
                    mdaHalt.DeleteCommand.Transaction = trnDelete;
                    if (RefData.DataSet.Tables["Tbl_ProductionHalts"].DefaultView.Count > 0)
                    {
                        RefData.DataSet.Tables["Tbl_ProductionHalts"].DefaultView[0].Delete();
                    }

                    CurrentDataRow.Delete();
                    SaveChanges(Module1.FormModeEnum.DELETE_MODE);
                    trnDelete.Commit();
                    Close();
                }
                catch (InvalidConstraintException ObjCnstEx)
                {
                    Logger.LogException("", ObjCnstEx);
                    RefData.DataSet.RejectChanges();
                    trnDelete.Rollback();
                    MessageBox.Show("برای حذف رکورد باید رکورد(های) مرتبط با آن حذف شوند", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                }
                catch (Exception objEx)
                {
                    RefData.DataSet.RejectChanges();
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
            bool hasProblem = false;
            string SaveConfilicts = ":در ثبت اطلاعات تولید مغایرتهای زیر وجود دارد";

            if (!FormValidation())
            {
                return;
            }

            foreach (DataGridViewRow r in dgOperators.Rows)
            {
                if (CheckOPeratorBusy(r.Cells[0].Value.ToString(), Strings.Replace(txtStartDate.Text, "/", ""), txtStartHour.Text, Strings.Replace(txtStartDate.Text, "/", ""), txtEndHour.Text))
                {
                    MessageBox.Show("اپراتور " + r.Cells[1].Value.ToString() + " در طول ساعات وارد شده، مشغول انجام کار می باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                    return;
                }
            }
                      
            // کنترل اینکه طول زمان توقف عملیات از طول انجام عملیات بیشتر نباشد
            if (txtHaltTime.Text != "00:00")
            {
                if (TimeSpan.Parse(txtHaltTime.Text) > TimeSpan.Parse(txtEndHour.Text) - TimeSpan.Parse(txtStartHour.Text))
                {
                    MessageBox.Show("طول زمان توقف عملیات، از طول زمان انجام عملیات، بیشتر می باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                    txtHaltTime.Focus();
                    return;
                }
            }

            // Get calendar code:-
            string CalCode;
            if (ExecutionMethod == (short)EnumExecutionMethod.EM_CONTRACTOR)
            {
                CalCode = GetContractorCalendar();
            }
            else
            {
                CalCode = GetCalendarCode(Conversions.ToString(Machin_Lookup.CB_SelectedValue));
            }

            double ProductionDuration = GetProductionDuration(CalCode);
            if (txtHaltTime.Text != "00:00")
                ProductionDuration = ProductionDuration - Conversions.ToDouble(Module1.GetFloatingHour(txtHaltTime.Text));
            if (ProductionDuration == -100)
            {
                return;
            }
            
             
            string query = "SELECT COUNT(*) FROM Tbl_PreOperations WHERE TreeCode = " + cbSubbatch.SelectedValue + " AND CurrentOperationCode='" + Strings.Mid(cbOperation.Text, 1, cbOperation.Text.IndexOf(" ")) + "'";
            int PreOpNo = Module1.CmdExecuteScalarInt(query);

            if (PreOpNo > 0)
            {
                // کنترل اینکه عملیات های پیشنیاز عملیات فعلی وارد تولید شده باشند
                string CurrentOperationCode = Strings.Mid(cbOperation.Text, 1, cbOperation.Text.IndexOf(" "));
                query = "Select Count(*) From Tbl_RealProduction Where SubbatchCode='" + cbSubbatch.Text + "' And OperationCode IN(Select PreOperationCode From Tbl_PreOperations Where TreeCode = " + cbSubbatch.SelectedValue.ToString() + " And CurrentOperationCode='" + CurrentOperationCode + "')";
                int PreOpInProduction = Module1.CmdExecuteScalarInt(query);
                if (PreOpInProduction == 0)
                {
                    hasProblem = true;
                    SaveConfilicts += Constants.vbCrLf + "عملیات(های) پیشنیاز عملیات انتخاب شده وارد تولید نشده اند -    ";
                }
            }
            // کنترل اینکه تعداد تولید شده بیشتر از تعداد برنامه ریزی شده نباشد
            int PlanningQty = Module1.CmdExecuteScalarInt("Select Sum(DetailProductionQuantity) From Tbl_Planning Where SubbatchCode='" + cbSubbatch.Text + "' And OperationCode='" + Strings.Mid(cbOperation.Text, 1, cbOperation.Text.IndexOf(" ")) + "'");
            int ProductionQty;
            if (mListForm.FormMode == (int)Module1.FormModeEnum.INSERT_MODE)
            {
                ProductionQty = Module1.CmdExecuteScalarInt("Select IsNull(Sum(IntactQuantity),0) From Tbl_RealProduction Where SubbatchCode='" + cbSubbatch.Text + "' And OperationCode='" + Strings.Mid(cbOperation.Text, 1, cbOperation.Text.IndexOf(" ")) + "'");
            }
            else
            {
                ProductionQty = Module1.CmdExecuteScalarInt("Select IsNull(Sum(IntactQuantity),0) From Tbl_RealProduction Where SubbatchCode='" + cbSubbatch.Text + "' And OperationCode='" + Strings.Mid(cbOperation.Text, 1, cbOperation.Text.IndexOf(" ")) + "' And ProductionCode<>" + CurrentDataRow["ProductionCode"]);
            }

            //ProductionQty = Conversions.ToLong(cmValidation.ExecuteScalar());
            if (ProductionQty + txtIntactQty.Value > PlanningQty)
            {
                hasProblem = true;
                SaveConfilicts += Constants.vbCrLf + "تعداد محصول تولید شده بیشتر از تعداد برنامه ریزی شده می باشد -    ";
                SaveConfilicts += Constants.vbCrLf + "تعداد برنامه ریزی شده: " + PlanningQty + "                                ";
            }
                          
            if (hasProblem)
            {
                SaveConfilicts += Constants.vbCrLf + Constants.vbCrLf + "اطلاعات را ثبت می کنید؟";
                if (MessageBox.Show(SaveConfilicts, Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false) == DialogResult.No)
                {
                    Module1.Closeconnection();
                    return;
                }
            }

            Module1.Openconnection();
            var trnProduction = Module1.cnProductionPlanning.BeginTransaction();
            switch (ListForm.FormMode)
            {
                case (int)Module1.FormModeEnum.INSERT_MODE:
                    {
                        try
                        {
                            mdaProduction.InsertCommand.Transaction = trnProduction;
                            mdaHalt.InsertCommand.Transaction = trnProduction;
                            CurrentDataRow = dsRealProductionNew.Tables["Tbl_RealProduction"].NewRow();
                            CurrentDataRow["ProductionCode"] = Module1.GetNewCode("Tbl_RealProduction", "ProductionCode");
                            CurrentDataRow["BatchCode"] = cbBatch.Text;
                            CurrentDataRow["SubbatchCode"] = cbSubbatch.Text;
                            CurrentDataRow["TreeCode"] = cbSubbatch.SelectedValue;
                            CurrentDataRow["OperationCode"] = GetOperationCode();
                            CurrentDataRow["OperationTitle"] = Strings.Mid(cbOperation.Text, cbOperation.Text.IndexOf(" ") + 2, Strings.Len(cbOperation.Text));
                            CurrentDataRow["StartDate"] = Strings.Replace(txtStartDate.Text, "/", "");
                            CurrentDataRow["StartHour"] = txtStartHour.Text;
                            CurrentDataRow["EndDate"] = Strings.Replace(txtStartDate.Text, "/", ""); // Replace(txtEndDate.Fdate, "/", "")
                            CurrentDataRow["EndHour"] = txtEndHour.Text;
                            if (txtHaltTime.Text != "00:00")
                            {
                                CurrentDataRow["HaltDuration"] = txtHaltTime.Text;
                                CurrentDataRow["HaltReason"] = cbHaltReason.Text;
                            }

                            CurrentDataRow["MachineCode"] = Machin_Lookup.CB_SelectedValue;
                            CurrentDataRow["IntactQuantity"] = txtIntactQty.Value;
                            CurrentDataRow["GarbageQuantity"] = txtGarbageQty.Value;
                            CurrentDataRow["PlanningCode"] = mPlanningCode;
                            CurrentDataRow["OperationType"] = Interaction.IIf(rbOpType1.Checked, 1, Interaction.IIf(rbOpType2.Checked, 2, 3));
                            CurrentDataRow["RecordConfirm"] = 0;
                            // mCurrentRow("FunctionStatistics") = txtFunctionStatistics.Value
                            // mCurrentRow("OperationUnitTitle") = cbOperationUnits.Text
                            CurrentDataRow["ProductionDuration"] = ProductionDuration;
                            CurrentDataRow["ProductCode"] = GetProductCode(Conversions.ToString(cbSubbatch.SelectedValue));
                            dsRealProductionNew.Tables["Tbl_RealProduction"].Rows.Add(CurrentDataRow);
                            if (txtHaltTime.Text != "00:00")
                            {
                                string mCode = Module1.GetNewCode("Tbl_ProductionHalts", "HaltID").ToString();
                                var mHaltRow = RefData.DataSet.Tables["Tbl_ProductionHalts"].NewRow();
                                mHaltRow["HaltID"] = mCode;
                                mHaltRow["StartDate"] = Strings.Replace(txtStartDate.Text, "/", "");
                                mHaltRow["StartHour"] = Strings.Mid((TimeSpan.Parse(txtEndHour.Text) - TimeSpan.Parse(txtHaltTime.Text)).ToString(), 1, 5);
                                mHaltRow["EndDate"] = Strings.Replace(txtStartDate.Text, "/", ""); // Replace(txtEndDate.Fdate, "/", "")
                                mHaltRow["EndHour"] = txtEndHour.Text;
                                mHaltRow["Duration"] = txtHaltTime.Text;
                                mHaltRow["ProductionCode"] = CurrentDataRow["ProductionCode"];
                                mHaltRow["HaltReason"] = cbHaltReason.SelectedValue;
                                RefData.DataSet.Tables["Tbl_ProductionHalts"].Rows.Add(mHaltRow);
                            }

                            SaveChanges(Module1.FormModeEnum.INSERT_MODE);
                            AddOperators(trnProduction, Module1.FormModeEnum.INSERT_MODE);
                            trnProduction.Commit();
                            string mCurrentSD = txtStartDate.Text;
                            ClearForm();
                            txtStartDate.Text = mCurrentSD;
                        }
                        catch (Exception objEx)
                        {
                            dsRealProductionNew.RejectChanges();
                            RefData.DataSet.RejectChanges();
                            trnProduction.Rollback();
                            Logger.SaveError(Name + ".cmdSave_Click", objEx.Message);
                            MessageBox.Show("اشکال در ثبت رکورد جدید، رکورد جدید ثبت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                            trnProduction.Dispose();
                        }

                        break;
                    }

                case (int)Module1.FormModeEnum.EDIT_MODE:
                    {
                        try
                        {
                            mdaProduction.UpdateCommand.Transaction = trnProduction;
                            mdaHalt.InsertCommand.Transaction = trnProduction;
                            mdaHalt.UpdateCommand.Transaction = trnProduction;
                            mdaHalt.DeleteCommand.Transaction = trnProduction;
                            CurrentDataRow.BeginEdit();
                            CurrentDataRow["SubbatchCode"] = cbSubbatch.Text;
                            CurrentDataRow["TreeCode"] = cbSubbatch.SelectedValue;
                            CurrentDataRow["OperationCode"] = GetOperationCode();
                            CurrentDataRow["OperationTitle"] = Strings.Mid(cbOperation.Text, cbOperation.Text.IndexOf(" ") + 2, Strings.Len(cbOperation.Text));
                            CurrentDataRow["StartDate"] = Strings.Replace(txtStartDate.Text, "/", "");
                            CurrentDataRow["StartHour"] = txtStartHour.Text;
                            CurrentDataRow["EndDate"] = Strings.Replace(txtStartDate.Text, "/", ""); // Replace(txtEndDate.Fdate, "/", "")
                            CurrentDataRow["EndHour"] = txtEndHour.Text;
                            if (txtHaltTime.Text != "00:00")
                            {
                                CurrentDataRow["HaltDuration"] = txtHaltTime.Text;
                                CurrentDataRow["HaltReason"] = cbHaltReason.Text;
                            }

                            CurrentDataRow["MachineCode"] = Machin_Lookup.CB_SelectedValue;
                            CurrentDataRow["IntactQuantity"] = txtIntactQty.Value;
                            CurrentDataRow["GarbageQuantity"] = txtGarbageQty.Value;
                            CurrentDataRow["PlanningCode"] = mPlanningCode;
                            CurrentDataRow["OperationType"] = Interaction.IIf(rbOpType1.Checked, 1, Interaction.IIf(rbOpType2.Checked, 2, 3));
                            CurrentDataRow["RecordConfirm"] = 0;
                            // mCurrentRow("FunctionStatistics") = txtFunctionStatistics.Value
                            // mCurrentRow("OperationUnitTitle") = cbOperationUnits.Text
                            CurrentDataRow["ProductionDuration"] = ProductionDuration;
                            CurrentDataRow["ProductCode"] = GetProductCode(Conversions.ToString(cbSubbatch.SelectedValue));
                            CurrentDataRow.EndEdit();
                            if (RefData.DataSet.Tables["Tbl_ProductionHalts"].DefaultView.Count > 0 && txtHaltTime.Text != "00:00")
                            {
                                RefData.DataSet.Tables["Tbl_ProductionHalts"].DefaultView[0].BeginEdit();
                                RefData.DataSet.Tables["Tbl_ProductionHalts"].DefaultView[0]["StartDate"] = Strings.Replace(txtStartDate.Text, "/", "");
                                RefData.DataSet.Tables["Tbl_ProductionHalts"].DefaultView[0]["StartHour"] = Strings.Mid((TimeSpan.Parse(txtEndHour.Text) - TimeSpan.Parse(txtHaltTime.Text)).ToString(), 1, 5);
                                RefData.DataSet.Tables["Tbl_ProductionHalts"].DefaultView[0]["EndDate"] = Strings.Replace(txtStartDate.Text, "/", ""); // Replace(txtEndDate.Fdate, "/", "")
                                RefData.DataSet.Tables["Tbl_ProductionHalts"].DefaultView[0]["EndHour"] = txtEndHour.Text;
                                RefData.DataSet.Tables["Tbl_ProductionHalts"].DefaultView[0]["Duration"] = txtHaltTime.Text;
                                RefData.DataSet.Tables["Tbl_ProductionHalts"].DefaultView[0]["HaltReason"] = cbHaltReason.SelectedValue;
                                RefData.DataSet.Tables["Tbl_ProductionHalts"].DefaultView[0].EndEdit();
                            }
                            else if (txtHaltTime.Text != "00:00" && RefData.DataSet.Tables["Tbl_ProductionHalts"].DefaultView.Count == 0)
                            {
                                string mCode = Module1.GetNewCode("Tbl_ProductionHalts", "HaltID").ToString();
                                var mHaltRow = RefData.DataSet.Tables["Tbl_ProductionHalts"].NewRow();
                                mHaltRow["HaltID"] = mCode;
                                mHaltRow["StartDate"] = Strings.Replace(txtStartDate.Text, "/", "");
                                mHaltRow["StartHour"] = Strings.Mid((TimeSpan.Parse(txtEndHour.Text) - TimeSpan.Parse(txtHaltTime.Text)).ToString(), 1, 5);
                                mHaltRow["EndDate"] = Strings.Replace(txtStartDate.Text, "/", ""); // Replace(txtEndDate.Fdate, "/", "")
                                mHaltRow["EndHour"] = txtEndHour.Text;
                                mHaltRow["Duration"] = txtHaltTime.Text;
                                mHaltRow["ProductionCode"] = CurrentDataRow["ProductionCode"];
                                mHaltRow["HaltReason"] = cbHaltReason.SelectedValue;
                                RefData.DataSet.Tables["Tbl_ProductionHalts"].Rows.Add(mHaltRow);
                            }
                            else if (txtHaltTime.Text == "00:00" && RefData.DataSet.Tables["Tbl_ProductionHalts"].DefaultView.Count > 0)
                            {
                                RefData.DataSet.Tables["Tbl_ProductionHalts"].DefaultView[0].Delete();
                            }

                            SaveChanges(Module1.FormModeEnum.EDIT_MODE);
                            AddOperators(trnProduction, Module1.FormModeEnum.EDIT_MODE);
                            trnProduction.Commit();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            CurrentDataRow.CancelEdit();
                            trnProduction.Rollback();
                            Logger.SaveError("frmRealProduction.cmdSave", objEx.Message);
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

        private void ClearForm()
        {
            cbOperators.SeletedValue = null;
            txtStartDate.Text = FarsiDateFunctions.SubDayFromDate(Module1.mServerShamsiDate, "00000001");
            txtStartHour.Text = "07:30";
            txtEndDate.Text = FarsiDateFunctions.SubDayFromDate(Module1.mServerShamsiDate, "00000001");
            txtEndHour.Text = "16:30";
            txtIntactQty.Value = 1m;
            txtGarbageQty.Value = 0m;
            rbOpType1.Checked = true;
            txtHaltTime.Text = "00:00";
            cbHaltReason.SelectedIndex = -1;
            dgOperators.Rows.Clear();
            rbOpType1.Focus();
        }

        private bool CheckOPeratorBusy(string OperatorCode, string StartDate, string StartHour, string EndDate, string EndHour)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    var cm = new SqlCommand("Select Count(*) From Tbl_RealProduction A Inner Join Tbl_ProductionOperators B ON A.ProductionCode = B.ProductionCode ", cn);
                    string FilterValue = "B.OperatorCode='" + OperatorCode + "' And (A.StartDate+A.StartHour>='" + StartDate + StartHour + "' And A.StartDate+A.StartHour<='" + EndDate + EndHour + "')";
                    if (ListForm.FormMode == (int)Module1.FormModeEnum.EDIT_MODE)
                    {
                        FilterValue = Conversions.ToString(FilterValue + Operators.ConcatenateObject(" And A.ProductionCode <> ", CurrentDataRow["ProductionCode"]));
                    }

                    cm.CommandText += " Where " + FilterValue;
                    if (Conversions.ToInteger(cm.ExecuteScalar()) == 0)
                    {
                        FilterValue = "B.OperatorCode='" + OperatorCode + "' And (A.EndDate+A.EndHour>='" + StartDate + StartHour + "' And A.EndDate+A.EndHour<='" + EndDate + EndHour + "')";
                        if (ListForm.FormMode == (int)Module1.FormModeEnum.EDIT_MODE)
                        {
                            FilterValue = Conversions.ToString(FilterValue + Operators.ConcatenateObject(" And A.ProductionCode <> ", CurrentDataRow["ProductionCode"]));
                        }

                        cm.CommandText = cm.CommandText.Substring(0, cm.CommandText.IndexOf("Where ")).Trim();
                        cm.CommandText += " Where " + FilterValue;
                        if (Conversions.ToInteger(cm.ExecuteScalar()) == 0)
                        {
                            FilterValue = "B.OperatorCode='" + OperatorCode + "' And (A.StartDate+A.StartHour<'" + StartDate + StartHour + "' And A.EndDate+A.EndHour>'" + EndDate + EndHour + "')";
                            if (ListForm.FormMode == (int)Module1.FormModeEnum.EDIT_MODE)
                            {
                                FilterValue = Conversions.ToString(FilterValue + Operators.ConcatenateObject(" And A.ProductionCode <> ", CurrentDataRow["ProductionCode"]));
                            }

                            cm.CommandText = cm.CommandText.Substring(0, cm.CommandText.IndexOf("Where ")).Trim();
                            cm.CommandText += " Where " + FilterValue;
                            if (Conversions.ToInteger(cm.ExecuteScalar()) == 0)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Logger.SaveError("frmRealProduction.CheckOPeratorBusy", ex.Message);
                    MessageBox.Show("کنترل زمان تولید اپراتور با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return false;
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }
        }

        private string GetOperationCode()
        {
            string OperationCode = "'-1'";
            if (cbOperation.SelectedValue is object)
            {
                using (var cn = new SqlConnection(Module1.PlanningCnnStr))
                {
                    cn.Open();
                    var cmOperationCode = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject("Select OperationCode From Tbl_Planning Where PlanningCode = ", cbOperation.SelectedValue)), cn);
                    var drOperationCode = cmOperationCode.ExecuteReader();
                    if (drOperationCode.Read())
                    {
                        OperationCode = Conversions.ToString(drOperationCode["OperationCode"]);
                    }

                    drOperationCode.Close();
                    cn.Close();
                }
            }

            return OperationCode;
        }

        //private double GetProductionDuration_old(string CalendarCode)
        //{
        //    double ProductionDuration = 0d;
        //    string Query = Constants.vbNullString;
        //    string ShamsiDate = Strings.Replace(txtStartDate.Text, "/", "");
        //    switch (IsParticularDay(CalendarCode, ShamsiDate))
        //    {
        //        case 0: // روز عادي مي باشد
        //            {
        //                var cmIsHoliday = new SqlCommand("Select Distinct DayType From Tbl_CalendarDays Where CalendarCode=" + CalendarCode + " And DayNo=" + Module1.GetDayNo(ShamsiDate), Module1.cnProductionPlanning);
        //                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
        //                    Module1.cnProductionPlanning.Open();
        //                switch (cmIsHoliday.ExecuteScalar())
        //                {
        //                    case 1: // روز عادی تعطیل می باشد
        //                        {
        //                            cmIsHoliday.Dispose();
        //                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
        //                                Module1.cnProductionPlanning.Close();
        //                            MessageBox.Show("تاریخ تولید وارد شده یک روز تعطیل می باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
        //                            ProductionDuration = -100;
        //                            goto Exit_Function;
        //                            //break;
        //                        }

        //                    case 2:
        //                        {
        //                            short DayNo = Conversions.ToShort(Strings.Right(ShamsiDate, 2));
        //                            short MonthNo = Conversions.ToShort(Strings.Mid(ShamsiDate, 5, 2));
        //                            cmIsHoliday.CommandText = "Select Count(*) From Tbl_HoliDays Where DayNo=" + DayNo + " And MonthNo=" + MonthNo;
        //                            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(cmIsHoliday.ExecuteScalar(), 0, false))) // روز عادی تعطیل می باشد
        //                            {
        //                                cmIsHoliday.Dispose();
        //                                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
        //                                    Module1.cnProductionPlanning.Close();
        //                                MessageBox.Show("تاریخ تولید وارد شده یک روز تعطیل می باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
        //                                ProductionDuration = -100;
        //                                goto Exit_Function;
        //                            }
        //                            else // روز عادی کاری  می باشد
        //                            {
        //                                Query = "Select * From Tbl_CalendarShifts Where CalendarCode=" + CalendarCode + " Order By ShiftNo";
        //                                DataSetConfig.FillDataSet("Tbl_CalendarShifts", "Tbl_Shifts", Query, "CalendarCode", "ShiftNo");

        //                                // mohsendokht
        //                                Query = "Select * From Tbl_CalendarShiftDownTimes Where CalendarCode=" + CalendarCode + " Order By ShiftNo";
        //                                DataSetConfig.FillDataSet("Tbl_CalendarShiftDownTimes", "Tbl_CalendarShiftDownTimes", Query, "CalendarCode", "ShiftNo", "DownTimeStart");
        //                            }

        //                            break;
        //                        }
        //                }

        //                cmIsHoliday.Dispose();
        //                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
        //                    Module1.cnProductionPlanning.Close();
        //                break;
        //            }

        //        case 1: // روز خاص تعطيل مي باشد
        //            {
        //                MessageBox.Show("تاریخ تولید وارد شده یک روز تعطیل می باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
        //                ProductionDuration = -100;
        //                goto Exit_Function;
        //                //break;
        //            }

        //        case 2: // روز خاص کاری مي باشد
        //            {
        //                Query = "Select * From Tbl_CalendarParticularShifts Where CalendarCode=" + CalendarCode + " And ShamsiDate=" + ShamsiDate + " Order By ShiftNo";
        //                DataSetConfig.FillDataSet("Tbl_CalendarParticularShifts", "Tbl_Shifts", Query, "CalendarCode", "ShamsiDate", "ShiftNo");
        //                Query = "Select * From Tbl_CalendarParticularShifts Where CalendarCode=" + CalendarCode + " And ShamsiDate=" + ShamsiDate + " Order By ShiftNo";
        //                DataSetConfig.FillDataSet("Tbl_CalendarParticularShifts", "Tbl_Shifts", Query, "CalendarCode", "ShamsiDate", "ShiftNo");
        //                break;
        //            }
        //    }

        //    var dvShifts = DataSetConfig.dsProductionPlanning.Tables["Tbl_Shifts"].DefaultView;
        //    var dvDownTimesShifts = DataSetConfig.dsProductionPlanning.Tables["Tbl_CalendarShiftDownTimes"].DefaultView;
        //    string StartTime = txtStartHour.Text;
        //    string EndTime = txtEndHour.Text;
        //    string ShiftStartTime = "00:00";
        //    string ShiftEndTime = "00:00";
        //    string WorkTimes = "00:00";
        //    string DownTimes = "00:00";
        //    string ShiftDownTimeStart = "00:00";
        //    string ShiftDownTimeEnd = "00:00";
        //    for (short I = 0, loopTo = (short)(dvShifts.Count - 1); I <= loopTo; I++)
        //    {
        //        ShiftStartTime = Conversions.ToString(dvShifts[I]["ShiftStart"]);
        //        ShiftEndTime = (TimeSpan.Parse(Conversions.ToString(dvShifts[I]["ShiftStart"])) + TimeSpan.Parse(Conversions.ToString(dvShifts[I]["ShiftDuration"]))).ToString();

        //        // mohsendokht
        //        // ShiftEndTime = (TimeSpan.Parse(ShiftStartTime) + TimeSpan.Parse(ShiftEndTime)).ToString

        //        if (I > 0)
        //        {
        //            StartTime = ShiftStartTime;
        //        }

        //        if (TimeSpan.Parse(EndTime) <= TimeSpan.Parse(ShiftEndTime)) // شیفت پایانی می باشد
        //        {
        //            WorkTimes = (TimeSpan.Parse(WorkTimes) + (TimeSpan.Parse(EndTime) - TimeSpan.Parse(StartTime))).ToString();
        //            ShiftDownTimeStart = Conversions.ToString(dvDownTimesShifts[I]["DownTimeStart"]);
        //            ShiftDownTimeEnd = Conversions.ToString(dvDownTimesShifts[I]["DownTimeEnd"]);
        //            // ShiftDownTimeStart = IIf(dvShifts(I)("DTStartHour") < 10, "0" & dvShifts(I)("DTStartHour"), dvShifts(I)("DTStartHour")) & ":" & _
        //            // IIf(dvShifts(I)("DTStartMinute") < 10, "0" & dvShifts(I)("DTStartMinute"), dvShifts(I)("DTStartMinute"))
        //            // ShiftDownTimeEnd = IIf(dvShifts(I)("DownTimeHour") < 10, "0" & dvShifts(I)("DownTimeHour"), dvShifts(I)("DownTimeHour")) & ":" & _
        //            // IIf(dvShifts(I)("DownTimeMinute") < 10, "0" & dvShifts(I)("DownTimeMinute"), dvShifts(I)("DownTimeMinute"))

        //            ShiftDownTimeEnd = (TimeSpan.Parse(ShiftDownTimeStart) + TimeSpan.Parse(ShiftDownTimeEnd)).ToString();
        //            if (TimeSpan.Parse(StartTime) <= TimeSpan.Parse(ShiftDownTimeStart))
        //            {
        //                if (TimeSpan.Parse(EndTime) >= TimeSpan.Parse(ShiftDownTimeEnd))
        //                {
        //                    DownTimes = (TimeSpan.Parse(DownTimes) + (TimeSpan.Parse(ShiftDownTimeEnd) - TimeSpan.Parse(ShiftDownTimeStart))).ToString();
        //                }
        //                else if (TimeSpan.Parse(EndTime) >= TimeSpan.Parse(ShiftDownTimeStart)) // زمان پایان تولید کوچکتر از زمان پایان استراحت باشد
        //                {
        //                    DownTimes = (TimeSpan.Parse(DownTimes) + (TimeSpan.Parse(EndTime) - TimeSpan.Parse(ShiftDownTimeStart))).ToString();
        //                }
        //            }
        //            else if (TimeSpan.Parse(StartTime) <= TimeSpan.Parse(ShiftDownTimeEnd)) // زمان شروع تولید بعد از زمان شروع استراحت باشد
        //            {
        //                if (TimeSpan.Parse(EndTime) >= TimeSpan.Parse(ShiftDownTimeEnd))
        //                {
        //                    DownTimes = (TimeSpan.Parse(DownTimes) + (TimeSpan.Parse(ShiftDownTimeEnd) - TimeSpan.Parse(StartTime))).ToString();
        //                }
        //                else
        //                {
        //                    DownTimes = (TimeSpan.Parse(DownTimes) + (TimeSpan.Parse(EndTime) - TimeSpan.Parse(StartTime))).ToString();
        //                }
        //            }

        //            break;
        //        }
        //        else // شیفت پایانی نمی باشد
        //        {
        //            WorkTimes = (TimeSpan.Parse(WorkTimes) + (TimeSpan.Parse(EndTime) - TimeSpan.Parse(StartTime))).ToString();
        //            ShiftDownTimeStart = Conversions.ToString(dvShifts[I]["DownTimeStart"]);
        //            ShiftDownTimeEnd = Conversions.ToString(dvShifts[I]["DownTimeEnd"]);
        //            // mohsendokht
        //            // ShiftDownTimeStart = IIf(dvShifts(I)("DTStartHour") < 10, "0" & dvShifts(I)("DTStartHour"), dvShifts(I)("DTStartHour")) & ":" & _
        //            // IIf(dvShifts(I)("DTStartMinute") < 10, "0" & dvShifts(I)("DTStartMinute"), dvShifts(I)("DTStartMinute"))
        //            // ShiftDownTimeEnd = IIf(dvShifts(I)("DownTimeHour") < 10, "0" & dvShifts(I)("DownTimeHour"), dvShifts(I)("DownTimeHour")) & ":" & _
        //            // IIf(dvShifts(I)("DownTimeMinute") < 10, "0" & dvShifts(I)("DownTimeMinute"), dvShifts(I)("DownTimeMinute"))

        //            ShiftDownTimeEnd = (TimeSpan.Parse(ShiftDownTimeStart) + TimeSpan.Parse(ShiftDownTimeEnd)).ToString();
        //            if (TimeSpan.Parse(StartTime) <= TimeSpan.Parse(ShiftDownTimeStart))
        //            {
        //                if (TimeSpan.Parse(EndTime) >= TimeSpan.Parse(ShiftDownTimeEnd))
        //                {
        //                    DownTimes = (TimeSpan.Parse(DownTimes) + (TimeSpan.Parse(ShiftDownTimeEnd) - TimeSpan.Parse(ShiftDownTimeStart))).ToString();
        //                }
        //                else if (TimeSpan.Parse(EndTime) >= TimeSpan.Parse(ShiftDownTimeStart)) // زمان پایان تولید کوچکتر از زمان پایان استراحت باشد
        //                {
        //                    DownTimes = (TimeSpan.Parse(DownTimes) + (TimeSpan.Parse(EndTime) - TimeSpan.Parse(ShiftDownTimeStart))).ToString();
        //                }
        //            }
        //            else if (TimeSpan.Parse(StartTime) <= TimeSpan.Parse(ShiftDownTimeEnd)) // زمان شروع تولید بعد از زمان شروع استراحت باشد
        //            {
        //                if (TimeSpan.Parse(EndTime) >= TimeSpan.Parse(ShiftDownTimeEnd))
        //                {
        //                    DownTimes = (TimeSpan.Parse(DownTimes) + (TimeSpan.Parse(ShiftDownTimeEnd) - TimeSpan.Parse(StartTime))).ToString();
        //                }
        //                else // زمان شروع و پایان تولید در بین زمان استراحت می باشد
        //                {
        //                    DownTimes = (TimeSpan.Parse(DownTimes) + (TimeSpan.Parse(EndTime) - TimeSpan.Parse(StartTime))).ToString();
        //                }
        //            }
        //        }
        //    }

        //    for (I = (short)(DataSetConfig.dsProductionPlanning.Tables.Count - 1); I >= 0; I += -1)
        //    {
        //        DataSetConfig.dsProductionPlanning.Tables[I].Dispose();
        //        DataSetConfig.dsProductionPlanning.Tables.RemoveAt(I);
        //    }

        //    WorkTimes = (TimeSpan.Parse(WorkTimes) - TimeSpan.Parse(DownTimes)).ToString();
        //    ProductionDuration = Conversions.ToDouble(Module1.GetFloatingHour(Strings.Mid(WorkTimes, 1, 5)));
        //Exit_Function:
        //    ;
        //    return ProductionDuration;
        //}

        private short IsParticularDay(string CalendarCode, string ShamsiDate)
        {
            var cmParticularDay = new SqlCommand("Select IsNull(DayType,0) From Tbl_CalendarParticularDays Where CalendarCode=" + CalendarCode + " And ShamsiDate=" + ShamsiDate, Module1.cnProductionPlanning);
            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                Module1.cnProductionPlanning.Open();
            short DayType = Conversions.ToShort(cmParticularDay.ExecuteScalar());
            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                Module1.cnProductionPlanning.Close();
            return DayType;
        }

        private void AddOperators(SqlTransaction mTrn, Module1.FormModeEnum mMode)
        {
            var cm = new SqlCommand("", mTrn.Connection);
            string mProductionCode = Constants.vbNullString;
            switch (mMode)
            {
                case Module1.FormModeEnum.INSERT_MODE:
                    {
                        mProductionCode = Conversions.ToString(CurrentDataRow["ProductionCode"]);
                        break;
                    }

                case Module1.FormModeEnum.EDIT_MODE:
                    {
                        mProductionCode = Conversions.ToString(CurrentDataRow["ProductionCode"]);
                        break;
                    }
            }

            cm.Transaction = mTrn;
            cm.CommandText = "Delete From Tbl_ProductionOperators Where ProductionCode = " + mProductionCode;
            cm.ExecuteNonQuery();
            foreach (DataGridViewRow r in dgOperators.Rows)
            {
                cm.CommandText = "Insert Into Tbl_ProductionOperators Values(" + mProductionCode + ",'" + r.Cells[0].Value.ToString() + "')";
                cm.ExecuteNonQuery();
            }
        }

        private void SaveChanges(Module1.FormModeEnum mode)
        {
            DataSet ProductionHaltChanges;
            DataSet RealProductionChanges;
            bool HasHalt = true;
            bool HasProduction = true;
            RealProductionChanges = dsRealProductionNew.GetChanges();
            ProductionHaltChanges = RefData.DataSet.GetChanges();
            if (RealProductionChanges is null)
                HasProduction = false;
            if (ProductionHaltChanges is null)
                HasHalt = false;
            if (HasHalt && ProductionHaltChanges.HasErrors)
            {
                RefData.DataSet.RejectChanges();
                return;
            }

            if (HasProduction && RealProductionChanges.HasErrors)
            {
                RealProductionChanges.RejectChanges();
                return;
            }

            switch (mode)
            {
                case Module1.FormModeEnum.DELETE_MODE:
                    {
                        if (HasHalt)
                        {
                            mdaHalt.Update(ProductionHaltChanges, "Tbl_ProductionHalts");
                            RefData.DataSet.AcceptChanges();
                        }

                        if (HasProduction)
                        {
                            mdaProduction.Update(RealProductionChanges, "Tbl_RealProduction");
                            dsRealProductionNew.AcceptChanges();
                        }

                        break;
                    }

                default:
                    {
                        if (HasProduction)
                        {
                            mdaProduction.Update(RealProductionChanges, "Tbl_RealProduction");
                            dsRealProductionNew.AcceptChanges();
                        }

                        if (HasHalt)
                        {
                            mdaHalt.Update(ProductionHaltChanges, "Tbl_ProductionHalts");
                            RefData.DataSet.AcceptChanges();
                        }

                        break;
                    }
            }

            // If ProductionHaltChanges IsNot Nothing Then
            // If ProductionHaltChanges.HasErrors Then
            // RefData.DataSet.RejectChanges()
            // Exit Sub
            // Else
            // If ListForm.FormMode = FormModeEnum.DELETE_MODE Then
            // mdaHalt.Update(ProductionHaltChanges, "Tbl_ProductionHalts")

            // Else
            // mdaHalt.Update(ProductionHaltChanges, "Tbl_ProductionHalts")
            // End If

            // RefData.DataSet.AcceptChanges()
            // End If
            // End If

            // 'Save RealProduction dataset changes:-
            // If ListForm.FormMode = FormModeEnum.DELETE_MODE Then
            // RealProductionChanges = dsRealProductionNew.GetChanges()
            // If RealProductionChanges Is Nothing Then Return

            // If RealProductionChanges.HasErrors Then
            // dsRealProductionNew.RejectChanges()
            // Else
            // mdaProduction.Update(RealProductionChanges, "Tbl_RealProduction")
            // End If

            // dsRealProductionNew.AcceptChanges()

            // Exit Sub
            // End If



            // ProductionHaltChanges = Nothing
        }

        private void txtPersonnelCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Strings.Asc(e.KeyChar) == (int)Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
            else if (Strings.Asc(e.KeyChar) != (int)Keys.Back)
            {
                string ValidStr = "0123456789";
                if (Strings.InStr(ValidStr, Conversions.ToString(e.KeyChar)) == 0)
                {
                    e.KeyChar = Conversions.ToChar("");
                }
            }
        }

        private string GetProductCode(string Treecode)
        {
            string ReturnedCode = Constants.vbNullString;
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmProductCode = new SqlCommand("Select ProductCode From Tbl_ProductTree Where TreeCode = " + Treecode, cn);
                var drProductCode = cmProductCode.ExecuteReader();
                if (drProductCode.Read())
                {
                    ReturnedCode = Conversions.ToString(drProductCode["ProductCode"]);
                }

                drProductCode.Close();
                cn.Close();
            }

            return ReturnedCode;
        }

        private void cbOperation_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Machin_Lookup.CB_DataSource = null;
            if (cbOperation.Items.Count > 0 && cbOperation.SelectedValue is object && cbOperation.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                using (var cn = new SqlConnection(Module1.PlanningCnnStr))
                {
                    var daMachines = new SqlDataAdapter(Conversions.ToString(Operators.ConcatenateObject("Select Distinct " + "  B.MachineCode, Case B.MachineCode When '-1' Then 'عملیات اپراتوری' Else B.MachineCode +' '+ C.[Name] End As MachineName " + "From   dbo.Tbl_Planning A INNER JOIN dbo.Tbl_ProductOPCsExecutorMachines B ON A.TreeCode = B.TreeCode And A.OperationCode = B.OperationCode INNER JOIN " + "       dbo.Tbl_Machines C ON B.MachineCode = C.Code " + "Where  A.OperationCode = '" + GetOperationCode() + "' And A.SubbatchCode = '" + cbSubbatch.Text + "' And A.TreeCode = ", cbSubbatch.SelectedValue)), cn);
                    var daNoMachine = new SqlDataAdapter("Select  " + "  M.Code AS MachineCode, M.Name AS MachineName " + "From dbo.Tbl_Machines AS M " + "Where M.Code = '-1' ", cn);
                    var dtMachines = new DataTable();
                    daMachines.Fill(dtMachines);

                    // This will be used for operation with contractor which actually has not a machine. We set a default machin -1
                    if (dtMachines.Rows.Count == 0)
                    {
                        daNoMachine.Fill(dtMachines);
                    }

                    //{
                    //    var withBlock = cbMachine;
                    //    withBlock.DataSource = dtMachines;
                    //    withBlock.DisplayMember = "MachineName";
                    //    withBlock.ValueMember = "MachineCode";
                    //    withBlock.SelectedIndex = -1;
                    //}
                    Machin_Lookup.CB_DataSource = dtMachines; // dataTable1;
                    Machin_Lookup.CB_AutoComplete = true;
                    Machin_Lookup.CB_LinkedColumnIndex = 1;
                    Machin_Lookup.CB_DisplayMember = "MachineCode";
                    Machin_Lookup.CB_ValueMember = "MachineName";
                    Machin_Lookup.CB_SerachFromTitle = "ماشین";

                    // Set the machine if ther is only one item:
                    if (dtMachines.Rows.Count == 1)
                    {
                        Machin_Lookup.CB_SelectedIndex = 0;
                    }
                }

                mPlanningCode = Conversions.ToLong(cbOperation.SelectedValue);
                RefData.DataSet.Tables["Tbl_MeasurementUnit1"].DefaultView.RowFilter = "OperationCode='" + GetOperationCode() + "'";
                RefData.DataSet.Tables["Tbl_MeasurementUnit2"].DefaultView.RowFilter = "OperationCode='" + GetOperationCode() + "'";
            }
            else
            {
                mPlanningCode = 0L;
                RefData.DataSet.Tables["Tbl_MeasurementUnit1"].DefaultView.RowFilter = "OperationCode=''";
                RefData.DataSet.Tables["Tbl_MeasurementUnit2"].DefaultView.RowFilter = "OperationCode=''";
            }
        }

        private void cbBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!LoadingComboData)
            {
                if (cbBatch.Items.Count > 0 && cbBatch.SelectedValue is object && cbBatch.SelectedValue.ToString() != "System.Data.DataRowView")
                {
                    RefData.DataSet.Tables["Tbl_ProductionSubbatchs"].DefaultView.RowFilter = "BatchCode = '" + cbBatch.SelectedValue.ToString() + "'";
                }
                else
                {
                    RefData.DataSet.Tables["Tbl_ProductionSubbatchs"].DefaultView.RowFilter = "";
                }

                cbSubbatch.SelectedIndex = -1;
                cbOperation.SelectedIndex = -1;
                Machin_Lookup.CB_SelectedIndex = -1;
            }
        }

        private void cbSubbatch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            zzSet_cbOperation_Source();

            // If Not LoadingComboData Then
            // cbOperation.DataSource = Nothing
            // cbMachine.DataSource = Nothing

            // RefData.DataSet.Tables("Tbl_MeasurementUnit1").DefaultView.RowFilter = "OperationCode=''"
            // RefData.DataSet.Tables("Tbl_MeasurementUnit2").DefaultView.RowFilter = "OperationCode=''"

            // If cbSubbatch.Items.Count > 0 AndAlso Not cbSubbatch.SelectedValue Is Nothing AndAlso cbSubbatch.SelectedValue.ToString <> "System.Data.DataRowView" Then
            // Dim drProduct() As DataRow = RefData.DataSet.Tables("Tbl_ProductionSubbatchs").Select("SubbatchCode = '" & cbSubbatch.Text & "'")

            // Using cn As New SqlConnection(PlanningCnnStr)
            // Dim daOperations As New SqlDataAdapter("Select    A.OperationCode + ' ' + A.OperationTitle As OperationTitle, B.PlanningCode " &
            // "From     dbo.Tbl_ProductOPCs A INNER JOIN " &
            // "         dbo.Tbl_Planning B ON A.TreeCode = B.TreeCode AND A.OperationCode = B.OperationCode " &
            // "Where    A.TreeCode = " & cbSubbatch.SelectedValue & " And B.SubbatchCode='" & cbSubbatch.Text & "'" &
            // "Order By A.OperationCode", cn)
            // Dim dtOperations As New DataTable

            // daOperations.Fill(dtOperations)

            // With cbOperation
            // .DataSource = dtOperations
            // .DisplayMember = "OperationTitle"
            // .ValueMember = "PlanningCode"
            // .SelectedIndex = -1
            // End With
            // End Using
            // End If
            // End If

        }

        private void cbOperators_InputKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cbOperators.SeletedValue = null;
                var mdr = RefData.DataSet.Tables["ProductionOperators"].Select("OperatorCode = '" + cbOperators.Text + "'");
                if (mdr.Length > 0)
                {
                    cbOperators.SeletedValue = mdr[0]["OperatorCode"];
                }
            }
        }

        private void cbOperators_InputTextChanged(object sender, EventArgs e)
        {
            if (cbOperators.Text is object && !cbOperators.Text.Equals(""))
            {
                cbOperators.SeletedValue = null;
                var mdr = RefData.DataSet.Tables["ProductionOperators"].Select("OperatorCode = '" + cbOperators.Text + "'");
                if (mdr.Length > 0)
                {
                    cbOperators.SeletedValue = mdr[0]["OperatorCode"];
                }
            }
        }

        /* TODO ERROR: Skipped RegionDirectiveTrivia */
        private bool FormValidation()
        {
            if (cbSubbatch.SelectedIndex == -1)
            {
                MessageBox.Show("کد ساب بچ را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbSubbatch.Focus();
                return false;
            }

            if (cbOperation.SelectedIndex == -1)
            {
                MessageBox.Show("کد فعالیت را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbOperation.Focus();
                return false;
            }

            if (Machin_Lookup.CB_SelectedIndex == -1)
            {
                MessageBox.Show("ماشین فعالیت را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                //cbMachine.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(Strings.Trim(Strings.Replace(txtStartDate.Text, "/", ""))))
            {
                MessageBox.Show("تاریخ شروع تولید را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtStartDate.Focus();
                return false;
            }

            if (txtHaltTime.Text != "00:00")
            {
                if (cbHaltReason.SelectedIndex == -1)
                {
                    MessageBox.Show("علت توقف عملیات را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    cbHaltReason.Focus();
                    return false;
                }
            }

            // No operator needed when ExecutionMethod is contractor:-
            ExecutionMethod = Conversions.ToInteger(GetExecutionMethod());
            if (ExecutionMethod != (short)EnumExecutionMethod.EM_CONTRACTOR)
            {
                if (dgOperators.Rows.Count == 0)
                {
                    MessageBox.Show("اپراتور(های) تولید را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    cbOperators.Focus();
                    return false;
                }

                string EndWorkingOperators = Constants.vbNullString;
                foreach (DataGridViewRow r in dgOperators.Rows)
                {
                    var mdr = RefData.DataSet.Tables["ProductionOperators"].Select("OperatorCode = '" + r.Cells[0].Value.ToString() + "'");
                    if (mdr.Length > 0 && Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(mdr[0]["WorkingStatus"], 2, false)) && Conversions.ToBoolean(Operators.ConditionalCompareObjectLessEqual(mdr[0]["WorkingEndDate"], txtStartDate.Text, false)))
                    {
                        if (string.IsNullOrEmpty(EndWorkingOperators))
                        {
                            EndWorkingOperators = r.Cells[1].Value.ToString();
                        }
                        else
                        {
                            EndWorkingOperators += "," + r.Cells[1].Value.ToString();
                        }
                    }
                }

                if (!string.IsNullOrEmpty(EndWorkingOperators))
                {
                    if (MessageBox.Show("برای اپراتور(های) {" + EndWorkingOperators + "} پایان کار ثبت شده است، آیا جهت ثبت مطمئن هستید", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading) == DialogResult.No)
                    {
                        cbOperators.Focus();
                        return false;
                    }
                }
            }

            return true;
        }

        private object GetExecutionMethod()
        {
            int ExeMethod = (int)EnumExecutionMethod.EM_MACHINE;
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cm = new SqlCommand("", cn);
                SqlDataReader dr = null;
                cm.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select ExecutionMethod From Tbl_ProductOPCs Where TreeCode = '", cbSubbatch.SelectedValue), "' AND  OperationCode = '"), GetOperationCode()), "'"));
                dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    ExeMethod = Conversions.ToInteger(dr["ExecutionMethod"]);
                }

                dr.Close();
                cn.Close();
            }

            return ExeMethod;
        }

        private string GetContractorCalendar()
        {
            string CalendarCode = "";
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cm = new SqlCommand("", cn);
                SqlDataReader dr = null;
                cm.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select CalendarCode From Tbl_ContractorOperations Where TreeCode = '", cbSubbatch.SelectedValue), "' AND  OperationCode = '"), GetOperationCode()), "'"));
                dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    CalendarCode = Conversions.ToString(dr["CalendarCode"]);
                }

                dr.Close();
                cn.Close();
            }

            return CalendarCode;
        }

        private double GetProductionDuration(string CalendarCode)
        {
            double ProductionDuration = 0d;
            string Query = Constants.vbNullString;
           
            try
            {

                string ShamsiDate = Strings.Replace(txtStartDate.Text, "/", "");
                switch (IsParticularDay(CalendarCode, ShamsiDate))
                {
                    case 0: // 'روز عادي مي باشد
                        {
                            var cmIsHoliday = new SqlCommand("Select Distinct DayType From Tbl_CalendarDays Where CalendarCode=" + CalendarCode + " And DayNo=" + Module1.GetDayNo(ShamsiDate), Module1.cnProductionPlanning);
                            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                                Module1.cnProductionPlanning.Open();
                            var result = cmIsHoliday.ExecuteScalar();
                            int DayType = result == DBNull.Value ? 0:Conversions.ToInteger(result);
                            switch (DayType)
                            {
                                case 1: // روز عادی تعطیل می باشد - جمعه 
                                    {
                                        cmIsHoliday.Dispose();
                                        if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                            Module1.cnProductionPlanning.Close();
                                        MessageBox.Show("تاریخ تولید وارد شده یک روز تعطیل می باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                                        ProductionDuration = -100;
                                        goto Exit_Function;
                                        //break;
                                    }

                                case 2: // روز های غیر تعطیل هفته - روز عادی کاری  می باشد
                                    {
                                        short DayNo = Conversions.ToShort(Strings.Right(ShamsiDate, 2));
                                        short MonthNo = Conversions.ToShort(Strings.Mid(ShamsiDate, 5, 2));
                                        cmIsHoliday.CommandText = "Select Count(*) From Tbl_HoliDays Where DayNo=" + DayNo + " And MonthNo=" + MonthNo;
                                        var isHoliday = Conversions.ToInteger(cmIsHoliday.ExecuteScalar());
                                        cmIsHoliday.Dispose();
                                        if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                            Module1.cnProductionPlanning.Close();

                                        if (isHoliday > 0) // روز عادی تعطیل می باشد - جزئ تعطیلات ثابت تقویم 
                                        {
                                            
                                            MessageBox.Show("تاریخ تولید وارد شده یک روز تعطیل می باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                                            ProductionDuration = -100;
                                            goto Exit_Function;
                                        }
                                        else // روز عادی کاری  می باشد
                                        {
                                            Query = "Select * From Tbl_CalendarShifts Where CalendarCode=" + CalendarCode + " Order By ShiftNo";
                                            DataSetConfig.FillDataSet("Tbl_CalendarShifts", "Tbl_Shifts", Query, "CalendarCode", "ShiftNo");
                                            Query = "Select * From Tbl_CalendarShiftDownTimes Where CalendarCode=" + CalendarCode + " Order By DownTimeStart";
                                            DataSetConfig.FillDataSet("Tbl_CalendarShiftDownTimes", "Tbl_CalendarShiftDownTimes", Query, "CalendarCode", "ShiftNo", "DownTimeStart");
                                        }

                                        break;
                                    }
                            }

                            cmIsHoliday.Dispose();
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                            break;
                        }

                    case 1: // روز خاص تعطيل مي باشد
                        {
                            MessageBox.Show("تاریخ تولید وارد شده یک روز تعطیل می باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                            ProductionDuration = -100;
                            goto Exit_Function;
                            //break;
                        }

                    case 2: // روز خاص کاری مي باشد
                        {
                            Query = "Select * From Tbl_CalendarParticularShifts Where CalendarCode=" + CalendarCode + " And ShamsiDate=" + ShamsiDate + " Order By ShamsiDate,ShiftNo";
                            DataSetConfig.FillDataSet("Tbl_CalendarParticularShifts", "Tbl_Shifts", Query, "CalendarCode", "ShamsiDate", "ShiftNo");
                            Query = "Select * From Tbl_ParticularShiftDownTimes Where CalendarCode=" + CalendarCode + " And ShamsiDate=" + ShamsiDate + " Order By DownTimeStart";
                            DataSetConfig.FillDataSet("Tbl_ParticularShiftDownTimes", "Tbl_CalendarShiftDownTimes", Query, "CalendarCode", "ShamsiDate", "ShiftNo", "DownTimeStart");
                            break;
                        }
                }

                var dvShifts = DataSetConfig.dsProductionPlanning.Tables["Tbl_Shifts"].DefaultView;
                string StartTime = txtStartHour.Text;  // ساعت شروع تولید
                string EndTime = txtEndHour.Text;
                string ShiftStartTime = "00:00";
                string ShiftEndTime = "00:00";
                string WorkTimes = "00:00";
                string DownTimes = "00:00";
                string ShiftDownTimeStart = "00:00";
                string ShiftDownTimeEnd = "00:00";
                for (short I = 0, loopTo = (short)(dvShifts.Count - 1); I <= loopTo; I++)
                {
                    ShiftStartTime = dvShifts[I]["ShiftStart"].ToString();
                    var mShiftEndTime = TimeSpan.Parse(dvShifts[I]["ShiftStart"].ToString()) + TimeSpan.Parse(dvShifts[I]["ShiftDuration"].ToString()) + TimeSpan.Parse(dvShifts[I]["ShiftExtraTime"].ToString());
                    if (mShiftEndTime >= TimeSpan.Parse("1.00:00"))
                    {
                        ShiftEndTime = (mShiftEndTime - TimeSpan.Parse("1.00:00")).ToString().Substring(0, 5);
                    }
                    else
                    {
                        ShiftEndTime = mShiftEndTime.ToString().Substring(0, 5);
                    }

                    if (I > 0)
                    {
                        StartTime = ShiftStartTime;
                    }

                    WorkTimes = (TimeSpan.Parse(WorkTimes) + (TimeSpan.Parse(EndTime) - TimeSpan.Parse(StartTime))).ToString();
                    string mDTFilter = "ShiftNo=" + dvShifts[I]["ShiftNo"].ToString();
                    var drDowntimes = DataSetConfig.dsProductionPlanning.Tables["Tbl_CalendarShiftDownTimes"].Select(mDTFilter);
                    foreach (DataRow r in drDowntimes)
                    {
                        ShiftDownTimeStart = r["DownTimeStart"].ToString();
                        ShiftDownTimeEnd = r["DownTimeEnd"].ToString();
                        if (TimeSpan.Parse(StartTime) <= TimeSpan.Parse(ShiftDownTimeStart))
                        {
                            if (TimeSpan.Parse(EndTime) >= TimeSpan.Parse(ShiftDownTimeEnd)) // اگر زمان استراحت بین زمان تولید باشد. زمان استراحت از طول تولید کم میشود
                            {
                                DownTimes = (TimeSpan.Parse(DownTimes) + (TimeSpan.Parse(ShiftDownTimeEnd) - TimeSpan.Parse(ShiftDownTimeStart))).ToString();
                            }
                            else if (TimeSpan.Parse(EndTime) >= TimeSpan.Parse(ShiftDownTimeStart)) // زمان پایان تولید کوچکتر از زمان پایان استراحت باشد. زمان استراحت از زمان تولید کم میشود
                            {
                                DownTimes = (TimeSpan.Parse(DownTimes) + (TimeSpan.Parse(EndTime) - TimeSpan.Parse(ShiftDownTimeStart))).ToString();
                            }
                        }
                        else if (TimeSpan.Parse(StartTime) <= TimeSpan.Parse(ShiftDownTimeEnd)) // زمان شروع تولید بزرگتر از زنان شروع استراحت  میباشد
                                                                                                // و زمان پایان استراحت بعد از شروع تولید باشد . 
                        {
                            if (TimeSpan.Parse(EndTime) >= TimeSpan.Parse(ShiftDownTimeEnd)) // و زمان پایان تولید بزرگتر از زمان پایان استراحت باشد
                            {
                                DownTimes = (TimeSpan.Parse(DownTimes) + (TimeSpan.Parse(ShiftDownTimeEnd) - TimeSpan.Parse(StartTime))).ToString();
                            }
                            else  // زمان پایان تولید کوچکتر از زمان پایان استراحت باشد
                            {
                                DownTimes = (TimeSpan.Parse(DownTimes) + (TimeSpan.Parse(EndTime) - TimeSpan.Parse(StartTime))).ToString();
                            }
                        }
                    }
                }

                for (I = (short)(DataSetConfig.dsProductionPlanning.Tables.Count - 1); I >= 0; I += -1)
                {
                    DataSetConfig.dsProductionPlanning.Tables[I].Dispose();
                    DataSetConfig.dsProductionPlanning.Tables.RemoveAt(I);
                }

                WorkTimes = (TimeSpan.Parse(WorkTimes) - TimeSpan.Parse(DownTimes)).ToString();
                ProductionDuration = Conversions.ToDouble(Module1.GetFloatingHour(Strings.Mid(WorkTimes, 1, 5)));
            Exit_Function:
                ;
                return ProductionDuration;

            }
            catch (Exception exc)
            {
                Logger.LogException("GetProductionDuration", exc);
                return 0;
            }
        }

        
        private void zzSetFormControls()
        {
            Module1.SetButtonsImage(cmdSave, 6);
            Module1.SetButtonsImage(cmdDelete, 9);
            Module1.SetButtonsImage(cmdExit, 14);
            Module1.SetButtonsImage(cmdNewOperationUnit, 0);
            Module1.SetButtonsImage(cmdCalcProductionQty, 16);
            Module1.SetButtonsImage(cmdAddOperator, 0);
            Module1.SetButtonsImage(cmdRemoveOperator, 1);

            // Hide statistic controls if no needs:-
            var appSettings = ConfigurationManager.AppSettings;
            bool ShowStatisticItem = Conversions.ToBoolean( appSettings["RealProduction_ShowStatisticItem"] ?? "false");
            if (ShowStatisticItem)
            {
                txtFunctionStatistics.Visible = true;
                FunctionStatisticsLbl.Visible = true;
                txtGarbageFunStatistics.Visible = true;
                cbOperationUnits.Visible = true;
                cbGarbageOpUnits.Visible = true;
                StatUnitLbl.Visible = true;
                cmdNewOperationUnit.Visible = true;
                cmdCalcProductionQty.Visible = true;
                QtyLbl.Visible = true;
            }
            // Read Settings to show/hide Production End Date:-
            bool ShowEndDate = Conversions.ToBoolean (appSettings["RealProduction_ShowEndDate"] ?? "true");
            if (ShowEndDate)
            {
                txtEndDate.Visible = true;
                EndDate_Label.Visible = true;
                StartDate_Label.Text = "تاریخ شروع:";
            }
        }

        private void zzLoaData()
        {
            try
            {
                zzCreateDataAdapterCommands();
                zzFillDataSet();
                zzSetCombosSource();
                switch (ListForm.FormMode)
                {
                    case (int)Module1.FormModeEnum.INSERT_MODE: // در صورتیکه فرم برای ایجاد رکورد جدید فراخوانی شده باشد این قسمت اجرا خواهد شد
                        {
                            txtStartDate.Text = FarsiDateFunctions.SubDayFromDate(Module1.mServerShamsiDate, "00000001");
                            txtStartHour.Text = "07:30";
                            txtEndDate.Text = FarsiDateFunctions.SubDayFromDate(Module1.mServerShamsiDate, "00000001");
                            txtEndHour.Text = "16:30";
                            cmdExit.Text = "خروج";
                            break;
                        }

                    case (int)Module1.FormModeEnum.EDIT_MODE:
                    case (int)Module1.FormModeEnum.DELETE_MODE:
                        {

                            // پر کردن کنترل فرم با مقادیر رکورد جاری
                            zzFillControls();
                            switch (ListForm.FormMode)
                            {
                                case (int)Module1.FormModeEnum.EDIT_MODE:
                                    {
                                        rbOpType1.Focus();
                                        break;
                                    }

                                case (int)Module1.FormModeEnum.DELETE_MODE:
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
                Logger.SaveError("frmRealProduction.zzLoadData", objEx.Message);
                MessageBox.Show("فراخوانی فرم با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void zzFillDataSet()
        {
            string SQL;
            string BatchCode = " '' ";
            string SubBatchCode = " '' ";
            string ProductionCode = " '' ";
            RefData = new MyDB();
            if (CurrentDataRow is object)
            {
                BatchCode = CurrentDataRow["BatchCode"].ToString();
                SubBatchCode = CurrentDataRow["SubBatchCode"].ToString();
                ProductionCode = CurrentDataRow["ProductionCode"].ToString();
            }

            // Get Tbl_ProductionBatchs.BatchCode
            SQL = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(" Select Distinct Tbl_ProductionBatchs.BatchCode " + " From Tbl_ProductionBatchs  " + " INNER JOIN Tbl_ProductionSubbatchs ON Tbl_ProductionBatchs.BatchCode = Tbl_ProductionSubbatchs.BatchCode " + " INNER JOIN Tbl_Planning  ON Tbl_Planning.SubbatchCode = Tbl_ProductionSubbatchs.SubbatchCode " + " WHERE (   ISNULL(Tbl_ProductionBatchs.FinishedDate, '') = '' " + "        OR Tbl_ProductionBatchs.FinishedDate = '0' ) ", Interaction.IIf(!string.IsNullOrEmpty(BatchCode), "   OR Tbl_ProductionBatchs.BatchCode = '" + BatchCode + "'", "")), " ORDER BY  Tbl_ProductionBatchs.BatchCode"));
            RefData.ReadData("Tbl_ProductionBatchs", SQL);
            SQL = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Select DISTINCT A.SubbatchCode, B.ProductTreeCode As TreeCode, D.ProductName,D.ProductCode,B.FinishedDate,B.BatchCode " + "From   dbo.Tbl_ProductionSubbatchs A INNER JOIN " + "       dbo.Tbl_ProductionBatchs B ON A.BatchCode = B.BatchCode INNER JOIN " + "       dbo.Tbl_ProductTree C ON B.ProductTreeCode = C.TreeCode INNER JOIN " + "       dbo.Tbl_Products D ON C.ProductCode = D.ProductCode " + "Where  A.SubbatchCode IN (Select SubbatchCode From Tbl_Planning) And (B.FinishedDate Is Null Or B.FinishedDate = '' Or B.FinishedDate = '0') ", Interaction.IIf(!string.IsNullOrEmpty(SubBatchCode), "   OR A.SubbatchCode = '" + SubBatchCode + "'", "")), " ORDER BY A.SubbatchCode "));
            RefData.ReadData("Tbl_ProductionSubbatchs", SQL);

            // فراخوانی اطلاعات جدول اپراتورها
            RefData.ReadData("ProductionOperators", "Select A.OperatorCode,A.OperatorName,A.WorkingStatus,IsNull((Select EndDate From Tbl_OperatorWorkPeriods Where OperatorCode = A.OperatorCode And StartDate IN (Select Max(StartDate) From Tbl_OperatorWorkPeriods Where OperatorCode = A.OperatorCode)),'0') As WorkingEndDate From Tbl_Operators A Order By A.OperatorName");

            // فراخوانی اطلاعات جدول توقفات عملیات
            RefData.ReadData("Tbl_ProductionHalts", "Select * From Tbl_ProductionHalts WHERE ProductionCode = " + ProductionCode);

            // فراخوانی اطلاعات جدول علت توقفات
            RefData.ReadData("Tbl_HaltReasons", "Select * From Tbl_HaltReasons");

            // فرخوانی جدول ضرایب تبدیل
            SQL = "Select A.OperationCode,A.UnitCode,B.Title AS UnitTitle,A.UnitIndex " + "From   dbo.Tbl_Operation_Measurement_UnitIndex A INNER JOIN " + "       dbo.Tbl_TestUnits B ON A.UnitCode = B.Code";
            RefData.ReadData("Tbl_MeasurementUnit1", SQL);
            RefData.ReadData("Tbl_MeasurementUnit2", SQL);
        }

        private void zzCreateDataAdapterCommands()
        {
            // ایجاد دستور اصلاح رکورد جاری در جدول
            mdaProduction.InsertCommand = new SqlCommand("Insert Into Tbl_RealProduction(ProductionCode,SubbatchCode,TreeCode,OperationCode,MachineCode,StartDate,StartHour,EndDate,EndHour,IntactQuantity,GarbageQuantity,PlanningCode,OperationType,RecordConfirm,ProductionDuration)" + "Values(@ProductionCode,@SubbatchCode,@TreeCode,@OperationCode,@MachineCode,@StartDate,@StartHour,@EndDate,@EndHour,@IntactQuantity,@GarbageQuantity,@PlanningCode,@OperationType,@RecordConfirm,@ProductionDuration)", Module1.cnProductionPlanning);
            {
                var withBlock = mdaProduction.InsertCommand;
                withBlock.Parameters.Add("@ProductionCode", SqlDbType.Int, default, "ProductionCode");
                withBlock.Parameters.Add("@SubbatchCode", SqlDbType.VarChar, 50, "SubbatchCode");
                withBlock.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode");
                withBlock.Parameters.Add("@OperationCode", SqlDbType.VarChar, 50, "OperationCode");
                withBlock.Parameters.Add("@MachineCode", SqlDbType.VarChar, 50, "MachineCode");
                withBlock.Parameters.Add("@StartDate", SqlDbType.VarChar, 8, "StartDate");
                withBlock.Parameters.Add("@StartHour", SqlDbType.VarChar, 50, "StartHour");
                withBlock.Parameters.Add("@EndDate", SqlDbType.VarChar, 8, "EndDate");
                withBlock.Parameters.Add("@EndHour", SqlDbType.VarChar, 50, "EndHour");
                withBlock.Parameters.Add("@IntactQuantity", SqlDbType.Int, default, "IntactQuantity");
                withBlock.Parameters.Add("@GarbageQuantity", SqlDbType.Int, default, "GarbageQuantity");
                withBlock.Parameters.Add("@PlanningCode", SqlDbType.Int, default, "PlanningCode");
                withBlock.Parameters.Add("@OperationType", SqlDbType.TinyInt, default, "OperationType");
                withBlock.Parameters.Add("@RecordConfirm", SqlDbType.Bit, default, "RecordConfirm");
                withBlock.Parameters.Add("@ProductionDuration", SqlDbType.VarChar, 50, "ProductionDuration");
            }
            // ایجاد دستور ویرایش رکورد در جدول
            mdaProduction.UpdateCommand = new SqlCommand("Update Tbl_RealProduction Set SubbatchCode=@SubbatchCode,TreeCode=@TreeCode,OperationCode=@OperationCode," + "MachineCode=@MachineCode,StartDate=@StartDate,StartHour=@StartHour," + "EndDate=@EndDate,EndHour=@EndHour,IntactQuantity=@IntactQuantity,GarbageQuantity=@GarbageQuantity," + "PlanningCode=@PlanningCode,OperationType=@OperationType,RecordConfirm=@RecordConfirm, " + "ProductionDuration=@ProductionDuration Where ProductionCode=@ProductionCode", Module1.cnProductionPlanning);
            {
                var withBlock1 = mdaProduction.UpdateCommand;
                withBlock1.Parameters.Add("@SubbatchCode", SqlDbType.VarChar, 50, "SubbatchCode");
                withBlock1.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode");
                withBlock1.Parameters.Add("@OperationCode", SqlDbType.VarChar, 50, "OperationCode");
                withBlock1.Parameters.Add("@MachineCode", SqlDbType.VarChar, 50, "MachineCode");
                withBlock1.Parameters.Add("@StartDate", SqlDbType.VarChar, 8, "StartDate");
                withBlock1.Parameters.Add("@StartHour", SqlDbType.VarChar, 50, "StartHour");
                withBlock1.Parameters.Add("@EndDate", SqlDbType.VarChar, 8, "EndDate");
                withBlock1.Parameters.Add("@EndHour", SqlDbType.VarChar, 50, "EndHour");
                withBlock1.Parameters.Add("@IntactQuantity", SqlDbType.Int, default, "IntactQuantity");
                withBlock1.Parameters.Add("@GarbageQuantity", SqlDbType.Int, default, "GarbageQuantity");
                withBlock1.Parameters.Add("@PlanningCode", SqlDbType.Int, default, "PlanningCode");
                withBlock1.Parameters.Add("@OperationType", SqlDbType.TinyInt, default, "OperationType");
                withBlock1.Parameters.Add("@RecordConfirm", SqlDbType.Bit, default, "RecordConfirm");
                withBlock1.Parameters.Add("@ProductionDuration", SqlDbType.VarChar, 50, "ProductionDuration");
                withBlock1.Parameters.Add("@ProductionCode", SqlDbType.Int, default, "ProductionCode").SourceVersion = DataRowVersion.Original;
            }
            // ایجاد دستور حذف رکورد جاری در جدول
            mdaProduction.DeleteCommand = new SqlCommand("Delete From Tbl_RealProduction Where ProductionCode=@ProductionCode", Module1.cnProductionPlanning);
            {
                var withBlock2 = mdaProduction.DeleteCommand;
                withBlock2.Parameters.Add("@ProductionCode", SqlDbType.Int, default, "ProductionCode").SourceVersion = DataRowVersion.Original;
            }

            // دستورات توقفات عملیات ---------------------------------------
            // ایجاد دستور اصلاح رکورد جاری در جدول
            mdaHalt.InsertCommand = new SqlCommand("Insert Into Tbl_ProductionHalts(HaltID,StartDate,StartHour,EndDate,EndHour,Duration,ProductionCode,HaltReason)" + "Values(@HaltID,@StartDate,@StartHour,@EndDate,@EndHour,@Duration,@ProductionCode,@HaltReason)", Module1.cnProductionPlanning);
            {
                var withBlock3 = mdaHalt.InsertCommand;
                withBlock3.Parameters.Add("@HaltID", SqlDbType.Int, default, "HaltID");
                withBlock3.Parameters.Add("@StartDate", SqlDbType.VarChar, 8, "StartDate");
                withBlock3.Parameters.Add("@StartHour", SqlDbType.VarChar, 50, "StartHour");
                withBlock3.Parameters.Add("@EndDate", SqlDbType.VarChar, 8, "EndDate");
                withBlock3.Parameters.Add("@EndHour", SqlDbType.VarChar, 50, "EndHour");
                withBlock3.Parameters.Add("@Duration", SqlDbType.VarChar, 50, "Duration");
                withBlock3.Parameters.Add("@ProductionCode", SqlDbType.Int, default, "ProductionCode");
                withBlock3.Parameters.Add("@HaltReason", SqlDbType.Int, default, "HaltReason");
            }
            // ایجاد دستور ویرایش رکورد در جدول
            mdaHalt.UpdateCommand = new SqlCommand("Update Tbl_ProductionHalts Set StartDate=@StartDate,StartHour=@StartHour,EndDate=@EndDate," + "EndHour=@EndHour,Duration=@Duration,HaltReason=@HaltReason Where HaltID=@HaltID", Module1.cnProductionPlanning);
            {
                var withBlock4 = mdaHalt.UpdateCommand;
                withBlock4.Parameters.Add("@StartDate", SqlDbType.VarChar, 8, "StartDate");
                withBlock4.Parameters.Add("@StartHour", SqlDbType.VarChar, 50, "StartHour");
                withBlock4.Parameters.Add("@EndDate", SqlDbType.VarChar, 8, "EndDate");
                withBlock4.Parameters.Add("@EndHour", SqlDbType.VarChar, 50, "EndHour");
                withBlock4.Parameters.Add("@Duration", SqlDbType.VarChar, 50, "Duration");
                withBlock4.Parameters.Add("@HaltReason", SqlDbType.Int, default, "HaltReason");
                withBlock4.Parameters.Add("@HaltID", SqlDbType.Int, default, "HaltID").SourceVersion = DataRowVersion.Original;
            }
            // ایجاد دستور حذف رکورد جاری در جدول
            mdaHalt.DeleteCommand = new SqlCommand("Delete From Tbl_ProductionHalts Where HaltID=@HaltID", Module1.cnProductionPlanning);
            {
                var withBlock5 = mdaHalt.DeleteCommand;
                withBlock5.Parameters.Add("@HaltID", SqlDbType.Int, default, "HaltID").SourceVersion = DataRowVersion.Original;
            }
        }

        private void zzSetCombosSource()
        {
            {
                var withBlock = cbOperators;
                // dsRefData.DataSet.Tables("ProductionOperators").DefaultView.RowFilter = "WorkingStatus = 1"
                withBlock.DataSource = RefData.DefaultView("ProductionOperators", "WorkingStatus = 1");
                withBlock.ValueMember = "OperatorCode";
                withBlock.DisplayMember = "OperatorName";
                withBlock.ValueColumnWidth = 100;
                withBlock.MessagesHeader = "برنامه ریزی تولید ";
            }

            {
                var withBlock1 = cbBatch;
                withBlock1.DataSource = RefData.DefaultView("Tbl_ProductionBatchs");
                withBlock1.DisplayMember = "BatchCode";
                withBlock1.ValueMember = "BatchCode";
                withBlock1.SelectedIndex = -1;
            }

            {
                var withBlock2 = cbSubbatch;
                withBlock2.DataSource = RefData.DefaultView("Tbl_ProductionSubbatchs");
                withBlock2.DisplayMember = "SubbatchCode";
                withBlock2.ValueMember = "TreeCode";
                withBlock2.SelectedIndex = -1;
            }

            {
                var withBlock3 = cbHaltReason;
                withBlock3.DataSource = RefData.DefaultView("Tbl_HaltReasons");
                withBlock3.DisplayMember = "ReasonTitle";
                withBlock3.ValueMember = "ReasonCode";
                withBlock3.SelectedIndex = -1;
            }

            {
                var withBlock4 = cbOperationUnits;
                withBlock4.DataSource = RefData.DefaultView("Tbl_MeasurementUnit1");
                withBlock4.DisplayMember = "UnitTitle";
                withBlock4.ValueMember = "UnitIndex";
                withBlock4.SelectedIndex = -1;
            }

            {
                var withBlock5 = cbGarbageOpUnits;
                withBlock5.DataSource = RefData.DefaultView("Tbl_MeasurementUnit2");
                withBlock5.DisplayMember = "UnitTitle";
                withBlock5.ValueMember = "UnitIndex";
                withBlock5.SelectedIndex = -1;
            }
        }

        private void zzSet_cbOperation_Source()
        {
            cbOperation.DataSource = null;
            RefData.DataSet.Tables["Tbl_MeasurementUnit1"].DefaultView.RowFilter = " OperationCode='' ";
            RefData.DataSet.Tables["Tbl_MeasurementUnit2"].DefaultView.RowFilter = " OperationCode='' ";
            if (cbSubbatch.Items.Count > 0 && cbSubbatch.SelectedValue is object && cbSubbatch.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                var drProduct = RefData.DataSet.Tables["Tbl_ProductionSubbatchs"].Select("SubbatchCode = '" + cbSubbatch.Text + "'");
                using (var cn = new SqlConnection(Module1.PlanningCnnStr))
                {
                    var daOperations = new SqlDataAdapter(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select    A.OperationCode + ' ' + A.OperationTitle As OperationTitle, B.PlanningCode " + "From     dbo.Tbl_ProductOPCs A INNER JOIN " + "         dbo.Tbl_Planning B ON A.TreeCode = B.TreeCode AND A.OperationCode = B.OperationCode " + "Where    A.TreeCode = ", cbSubbatch.SelectedValue), " And B.SubbatchCode='"), cbSubbatch.Text), "'"), "Order By A.OperationCode")), cn);
                    var dtOperations = new DataTable();
                    daOperations.Fill(dtOperations);
                    {
                        var withBlock = cbOperation;
                        withBlock.DataSource = dtOperations;
                        withBlock.DisplayMember = "OperationTitle";
                        withBlock.ValueMember = "PlanningCode";
                        withBlock.SelectedIndex = -1;
                    }
                }
            }
        }

        private void zzFillControls()
        {
            txtStartDate.Text = Conversions.ToString(CurrentDataRow["StartDate"]);
            txtStartHour.Text = Conversions.ToString(CurrentDataRow["StartHour"]);
            txtEndDate.Text = Conversions.ToString(CurrentDataRow["EndDate"]);
            txtEndHour.Text = Conversions.ToString(CurrentDataRow["EndHour"]);

            // Dim mRows() As DataRow = dsRealProduction.Tables("Tbl_ProductionSubbatchs").Select("SubbatchCode = '" & CurrentDataRow("SubbatchCode").ToString() & "'")

            // If mRows.Length > 0 Then
            // cbBatch.SelectedValue = mRows(0)("BatchCode")
            // End If
            cbBatch.SelectedValue = CurrentDataRow["BatchCode"];
            cbSubbatch.SelectedValue = CurrentDataRow["TreeCode"];

            // If Not cbSubbatch.Text.Equals(CurrentDataRow("SubbatchCode")) Then
            cbSubbatch.Text = Conversions.ToString(CurrentDataRow["SubbatchCode"]);
            // End If

            // cbSubbatch_SelectionChangeCommitted(Me, New EventArgs())
            zzSet_cbOperation_Source();
            cbOperation.SelectedValue = CurrentDataRow["PlanningCode"];
            cbOperation_SelectionChangeCommitted(this, new EventArgs());
            Machin_Lookup.CB_SelectedValue = CurrentDataRow["MachineCode"].ToString();
            txtIntactQty.Value = Conversions.ToDecimal(CurrentDataRow["IntactQuantity"]);
            txtGarbageQty.Value = Conversions.ToDecimal(CurrentDataRow["GarbageQuantity"]);
            var drSubbatch = RefData.DataSet.Tables["Tbl_ProductionSubbatchs"].Select("SubbatchCode = '" + CurrentDataRow["SubbatchCode"].ToString() + "'");
            if (drSubbatch.Length > 0)
            {
                if (!(DBNull.Value.Equals(drSubbatch[0]["FinishedDate"]) || drSubbatch[0]["FinishedDate"].ToString().Equals("") || drSubbatch[0]["FinishedDate"].ToString().Equals("0")))
                {
                    cmdSave.Enabled = false;
                }
            }

            switch (CurrentDataRow["OperationType"])
            {
                case 1:
                    {
                        rbOpType1.Checked = true;
                        break;
                    }

                case 2:
                    {
                        rbOpType2.Checked = true;
                        break;
                    }

                case 3:
                    {
                        rbOpType3.Checked = true;
                        break;
                    }
            }

            if (RefData.DataSet.Tables["Tbl_ProductionHalts"].DefaultView.Count > 0)
            {
                txtHaltTime.Text = Conversions.ToString(RefData.DataSet.Tables["Tbl_ProductionHalts"].DefaultView[0]["Duration"]);
                cbHaltReason.SelectedValue = RefData.DataSet.Tables["Tbl_ProductionHalts"].DefaultView[0]["HaltReason"];
            }

            // If Not IsDBNull(mCurrentRow("FunctionStatistics")) Then
            // txtFunctionStatistics.Value = mCurrentRow("FunctionStatistics")
            // End If

            // If Not IsDBNull(mCurrentRow("OperationUnitTitle")) Then
            // cbOperationUnits.Text = mCurrentRow("OperationUnitTitle")
            // End If

            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    {
                        var withBlock = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject("Select A.OperatorCode,B.OperatorName From Tbl_ProductionOperators A Inner Join Tbl_Operators B ON A.OperatorCode = B.OperatorCode Where A.ProductionCode = ", CurrentDataRow["ProductionCode"])), cn);
                        var mdr = withBlock.ExecuteReader();
                        while (mdr.Read())
                            dgOperators.Rows.Add(mdr["OperatorCode"], mdr["OperatorName"]);
                        mdr.Close();
                    }
                }
                catch (Exception ex)
                {
                    Logger.SaveError("frmRealProduction.zzFillControls", ex.Message);
                    MessageBox.Show("فراخوانی لیست اپراتورهای تولید با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }
        }


        /* TODO ERROR: Skipped EndRegionDirectiveTrivia */

    }
}