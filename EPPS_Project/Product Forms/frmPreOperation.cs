using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmPreOperation
    {
        public frmPreOperation()
        {
            InitializeComponent();
            _dgPreOperations.Name = "dgPreOperations";
            _cmdSave.Name = "cmdSave";
            _cmdExit.Name = "cmdExit";
        }

        private DataSet mdsProductionPlanning;
        private SqlDataAdapter daPreOperations = new SqlDataAdapter();
        private SqlDataAdapter daProductOPCs = new SqlDataAdapter();
        private DataTable dtCombo = new DataTable();
        private DataView dvPreOperations;
        private DataRow mOpCurrentRow;
        public short FormMode;

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

        public object OpCurrentRow
        {
            set
            {
                mOpCurrentRow = (DataRow)value;
            }
        }

        private void frmPreOperation_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdSave, 6);
            Module1.SetButtonsImage(cmdExit, 14);
            FormLoad();
        }

        private void frmPreOperation_FormClosing(object sender, FormClosingEventArgs e)
        {
            {
                var withBlock = mdsProductionPlanning;
                withBlock.RejectChanges();
                withBlock.Relations.Clear();
                for (int I = withBlock.Tables.Count - 1; I >= 2; I -= 1)
                {
                    withBlock.Tables[I].Constraints.Clear();
                    withBlock.Tables[I].Dispose();
                    withBlock.Tables.RemoveAt(I);
                }
            }

            dtCombo = null;
            daPreOperations.Dispose();
            daProductOPCs.Dispose();
            dvPreOperations = null;
            mdsProductionPlanning = null;
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgPreOperations_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                return;
            }

            if (e.ColumnIndex == 0)
            {
                string PreOPCodes = Constants.vbNullString;
                for (int I = 0, loopTo = dgPreOperations.Rows.Count - 2; I <= loopTo; I++)
                {
                    if (I != e.RowIndex && Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(dgPreOperations.Rows[I].Cells[0].Value, Constants.vbNullString, false)) && dgPreOperations.Rows[I].Cells[0].Value is object)
                    {
                        if (string.IsNullOrEmpty(PreOPCodes))
                        {
                            PreOPCodes = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("'", dgPreOperations.Rows[I].Cells[0].Value), "'"));
                        }
                        else
                        {
                            PreOPCodes = Conversions.ToString(PreOPCodes + Operators.ConcatenateObject(Operators.ConcatenateObject(",'", dgPreOperations.Rows[I].Cells[0].Value), "'"));
                        }
                    }
                }

                if (!string.IsNullOrEmpty(PreOPCodes))
                {
                    dvPreOperations.RowFilter = "Not OperationCode IN(" + PreOPCodes + ")";
                }
                else
                {
                    dvPreOperations.RowFilter = "";
                }

                dgPreOperations.Rows[e.RowIndex].Cells[0].Dispose();
                dgPreOperations.Rows[e.RowIndex].Cells[0] = Module1.CreateComboBoxCell(dvPreOperations, null, "", "OperationCode", "OperationCode", 300);
            }
        }

        private void dgPreOperations_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!ValidationForm())
            {
                return;
            }

            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                Module1.cnProductionPlanning.Open();
            var trnPreOperation = Module1.cnProductionPlanning.BeginTransaction();
            var dvPreOPs = dsProductionPlanning.Tables["Tbl_PreOperations"].DefaultView;
            string mAlarmDesc = Constants.vbNullString;
            try
            {
                daPreOperations.DeleteCommand.Transaction = trnPreOperation;
                daPreOperations.InsertCommand.Transaction = trnPreOperation;
                daProductOPCs.UpdateCommand.Transaction = trnPreOperation;
                for (int I = 0, loopTo = dvPreOPs.Count - 1; I <= loopTo; I++)
                {
                    if (I < dgPreOperations.Rows.Count - 1)
                    {
                        if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(dvPreOPs[I]["PreOperationCode"], dgPreOperations.Rows[I].Cells[0].Value, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(dvPreOPs[I]["RelationType"], dgPreOperations.Rows[I].Cells[1].Value, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(dvPreOPs[I]["LagTime"], dgPreOperations.Rows[I].Cells[2].Value, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(dvPreOPs[I]["TimeType"], dgPreOperations.Rows[I].Cells[3].Value, false)))


                        {
                            mAlarmDesc = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("مشخصات عملیات: {", mOpCurrentRow["OperationCode"]), "} تغییر کرد"));
                            break;
                        }
                    }
                }

                if (string.IsNullOrEmpty(mAlarmDesc))
                {
                    if (dvPreOPs.Count != dgPreOperations.Rows.Count - 1)
                    {
                        mAlarmDesc = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("مشخصات عملیات: {", mOpCurrentRow["OperationCode"]), "} تغییر کرد"));
                    }
                }

                for (int I = dvPreOPs.Count - 1; I >= 0; I -= 1)
                    dvPreOPs[I].Delete();

                // اضافه کردن رکورد جدید به جدول در دیتاست
                for (int I = 0, loopTo1 = dgPreOperations.Rows.Count - 2; I <= loopTo1; I++)
                    AddNewPreOperations((short)I);
                if (dgPreOperations.Rows.Count > 1)
                {
                    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(mOpCurrentRow["HavePreOperation"], 1, false)))
                    {
                        mOpCurrentRow["HavePreOperation"] = 1;
                    }
                }
                else if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(mOpCurrentRow["HavePreOperation"], 0, false)))
                {
                    mOpCurrentRow["HavePreOperation"] = 0;
                }

                SaveChanges();
                trnPreOperation.Commit();
                if (!string.IsNullOrEmpty(mAlarmDesc))
                {
                    Module1.Check_Subbatch_HasPlanningAlarm(Conversions.ToString(mOpCurrentRow["TreeCode"]), "", "", "1", mAlarmDesc);
                }

                Close();
            }
            catch (ConstraintException ObjCnstEx)
            {
                Logger.LogException("", ObjCnstEx);
                ObjCnstEx = null;
                SaveChanges();
                trnPreOperation.Commit();
                Close();
            }
            catch (Exception objEx)
            {
                mdsProductionPlanning.RejectChanges();
                trnPreOperation.Rollback();
                Logger.SaveError(Name + ".CmdSave_Click", objEx.Message);
                MessageBox.Show("اشکال در ثبت رکورد(های) جدید، رکورد(های) جدید ثبت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
            finally
            {
                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                    Module1.cnProductionPlanning.Close();
                trnPreOperation.Dispose();
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
                string PreOperationCodes = Constants.vbNullString;
                for (int I = 0, loopTo = mdsProductionPlanning.Tables["Tbl_PreOperations"].Rows.Count - 1; I <= loopTo; I++)
                {
                    if (string.IsNullOrEmpty(PreOperationCodes))
                    {
                        PreOperationCodes = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("'", mdsProductionPlanning.Tables["Tbl_PreOperations"].Rows[I]["PreOperationCode"]), "'"));
                    }
                    else
                    {
                        PreOperationCodes = Conversions.ToString(PreOperationCodes + Operators.ConcatenateObject(Operators.ConcatenateObject(",'", mdsProductionPlanning.Tables["Tbl_PreOperations"].Rows[I]["PreOperationCode"]), "'"));
                    }
                }

                daPreOperations.SelectCommand = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select OperationCode,OperationPriority From Tbl_ProductOPCs Where TreeCode=", mOpCurrentRow["TreeCode"]), " And OperationCode<>'"), mOpCurrentRow["OperationCode"]), "' And Not OperationCode IN (Select PreOperationCode From Tbl_PreOperations Where TreeCode="), mOpCurrentRow["TreeCode"]), " And Not PreOperationCode IN("), Interaction.IIf(!string.IsNullOrEmpty(PreOperationCodes), PreOperationCodes, "''")), "))")), Module1.cnProductionPlanning);
                daPreOperations.Fill(dtCombo);
                dvPreOperations = dtCombo.DefaultView;
                FillComboBoxses();
                FillForm();
            }
            catch (Exception objEx)
            {
                Logger.SaveError(Name + ".FormLoad", objEx.Message);
                MessageBox.Show("فراخوانی فرم با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }

        private void FillComboBoxses()
        {
            dgPreOperations.Columns.RemoveAt(1);
            dgPreOperations.Columns.Insert(1, Module1.CreateComboBoxColumn(mdsProductionPlanning, "Tbl_RelationTypes", "TypeTitle", "TypeCode", "نوع رابطه", 80, 80));
            dgPreOperations.Columns.RemoveAt(3);
            dgPreOperations.Columns.Insert(3, Module1.CreateComboBoxColumn(mdsProductionPlanning, "Tbl_TimeTypes", "TypeTitle", "TypeCode", "نوع واحد زمانی", 110, 110));
        }

        private void FillForm()
        {
            if (mdsProductionPlanning.Tables["Tbl_PreOperations"].DefaultView.Count > 0)
            {
                DataGridViewRow dgRowInsert;
                for (int I = 0, loopTo = mdsProductionPlanning.Tables["Tbl_PreOperations"].DefaultView.Count - 1; I <= loopTo; I++)
                {
                    dgRowInsert = (DataGridViewRow)dgPreOperations.Rows[dgPreOperations.Rows.Count - 1].Clone();
                    dgRowInsert.Cells[0] = Module1.CreateComboBoxCell(dvPreOperations, null, "", "OperationCode", "OperationCode", 300);
                    dgRowInsert.SetValues(mdsProductionPlanning.Tables["Tbl_PreOperations"].DefaultView[I]["PreOperationCode"], mdsProductionPlanning.Tables["Tbl_PreOperations"].DefaultView[I]["RelationType"], mdsProductionPlanning.Tables["Tbl_PreOperations"].DefaultView[I]["LagTime"], mdsProductionPlanning.Tables["Tbl_PreOperations"].DefaultView[I]["TimeType"]);


                    dgPreOperations.Rows.Add(dgRowInsert);
                }
            }
        }

        private void CreateDataAdapterCommands()
        {
            // -------------------- ایجاد دستورات جدول عملیات پیشنیاز----------------------
            // ایجاد دستور اضافه کردن رکورد جدید در جدول
            daPreOperations.InsertCommand = new SqlCommand("Insert Into Tbl_PreOperations(TreeCode,CurrentOperationCode,PreOperationCode,RelationType,LagTime,TimeType) " + "Values(@TreeCode,@CurrentOperationCode,@PreOperationCode,@RelationType,@LagTime,@TimeType)", Module1.cnProductionPlanning);
            {
                var withBlock = daPreOperations.InsertCommand;
                withBlock.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode");
                withBlock.Parameters.Add("@CurrentOperationCode", SqlDbType.VarChar, 50, "CurrentOperationCode");
                withBlock.Parameters.Add("@PreOperationCode", SqlDbType.VarChar, 50, "PreOperationCode");
                withBlock.Parameters.Add("@RelationType", SqlDbType.TinyInt, default, "RelationType");
                withBlock.Parameters.Add("@LagTime", SqlDbType.Int, default, "LagTime");
                withBlock.Parameters.Add("@TimeType", SqlDbType.TinyInt, default, "TimeType");
            }
            // ایجاد دستور حذف رکورد جاری در جدول
            daPreOperations.DeleteCommand = new SqlCommand("Delete From Tbl_PreOperations Where TreeCode=@TreeCode And CurrentOperationCode=@CurrentOperationCode And PreOperationCode=@PreOperationCode", Module1.cnProductionPlanning);
            {
                var withBlock1 = daPreOperations.DeleteCommand;
                withBlock1.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode").SourceVersion = DataRowVersion.Original;
                withBlock1.Parameters.Add("@CurrentOperationCode", SqlDbType.VarChar, 50, "CurrentOperationCode").SourceVersion = DataRowVersion.Original;
                withBlock1.Parameters.Add("@PreOperationCode", SqlDbType.VarChar, 50, "PreOperationCode").SourceVersion = DataRowVersion.Original;
            }
            // -------------------- ایجاد دستور حذف جدول عملیاتها----------------------
            daProductOPCs.UpdateCommand = new SqlCommand("Update Tbl_ProductOPCs Set HavePreOperation=@HavePreOperation Where TreeCode=@TreeCode And OperationCode=@OperationCode", Module1.cnProductionPlanning);
            {
                var withBlock2 = daProductOPCs.UpdateCommand;
                withBlock2.Parameters.Add("@HavePreOperation", SqlDbType.TinyInt, default, "HavePreOperation");
                withBlock2.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode").SourceVersion = DataRowVersion.Original;
                withBlock2.Parameters.Add("@OperationCode", SqlDbType.VarChar, 50, "OperationCode").SourceVersion = DataRowVersion.Original;
            }
        }

        private void AddNewPreOperations(short RowNo)
        {
            DataRow drInsert;
            drInsert = dsProductionPlanning.Tables["Tbl_PreOperations"].NewRow();
            drInsert["TreeCode"] = mOpCurrentRow["TreeCode"];
            drInsert["CurrentOperationCode"] = mOpCurrentRow["OperationCode"];
            drInsert["PreOperationCode"] = dgPreOperations.Rows[RowNo].Cells[0].Value;
            drInsert["RelationType"] = dgPreOperations.Rows[RowNo].Cells[1].Value;
            drInsert["LagTime"] = dgPreOperations.Rows[RowNo].Cells[2].Value;
            drInsert["TimeType"] = dgPreOperations.Rows[RowNo].Cells[3].Value;
            dsProductionPlanning.Tables["Tbl_PreOperations"].Rows.Add(drInsert);
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
                daPreOperations.Update(dsChanges, "Tbl_PreOperations");
                daProductOPCs.Update(dsChanges, "Tbl_ProductOPCs");
                mdsProductionPlanning.AcceptChanges();
            }

            dsChanges = null;
        }

        private bool ValidationForm()
        {
            for (int I = 0, loopTo = dgPreOperations.Rows.Count - 2; I <= loopTo; I++)
            {
                if (dgPreOperations.Rows[I].Cells[0].Value is null || Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgPreOperations.Rows[I].Cells[0].Value, Constants.vbNullString, false)))
                {
                    MessageBox.Show("عملیات پیشنیاز را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return false;
                }

                if (dgPreOperations.Rows[I].Cells[1].Value is null || Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgPreOperations.Rows[I].Cells[1].Value, Constants.vbNullString, false)))
                {
                    MessageBox.Show("نوع ارتباط را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return false;
                }

                if (dgPreOperations.Rows[I].Cells[2].Value is null || string.IsNullOrEmpty(dgPreOperations.Rows[I].Cells[2].Value.ToString()))
                {
                    MessageBox.Show("میران تاخیر/تقدم را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return false;
                }

                if (dgPreOperations.Rows[I].Cells[3].Value is null || Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgPreOperations.Rows[I].Cells[3].Value, Constants.vbNullString, false)))
                {
                    MessageBox.Show("نوع زمان را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return false;
                }
            }

            return true;
        }
    }
}