using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmOperationMaterial
    {
        public frmOperationMaterial()
        {
            InitializeComponent();
            _dgPreItems.Name = "dgPreItems";
            _cmdSave.Name = "cmdSave";
            _cmdExit.Name = "cmdExit";
        }

        private DataSet mdsProductionPlanning;
        private SqlDataAdapter daPreItems = new SqlDataAdapter();
        private SqlDataAdapter daProductOPCs = new SqlDataAdapter();
        private DataTable dtMaterials = new DataTable();
        private DataView dvPreItems = null;
        private DataView dvTestUnits = null;
        private DataView dvMaterials = null;
        private DataRow mOpCurrentRow;
        public short FormMode;
        private short I, J;

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

        private void frmOperationMaterial_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdSave, 6);
            Module1.SetButtonsImage(cmdExit, 5);
            FormLoad();
        }

        private void frmOperationMaterial_FormClosing(object sender, FormClosingEventArgs e)
        {
            {
                var withBlock = mdsProductionPlanning;
                withBlock.RejectChanges();
                withBlock.Relations.Clear();
                for (I = (short)(withBlock.Tables.Count - 1); I >= 2; I += -1)
                {
                    withBlock.Tables[I].Constraints.Clear();
                    withBlock.Tables[I].Dispose();
                    withBlock.Tables.RemoveAt(I);
                }
            }

            dtMaterials.Dispose();
            dvMaterials = null;
            daPreItems.Dispose();
            daProductOPCs.Dispose();
            mdsProductionPlanning = null;
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!ValidationForm())
                return;
            SqlTransaction trnPreMaterial = null;
            var dvPreItems = dsProductionPlanning.Tables["Tbl_OperationMaterials"].DefaultView;
            string mAlarmDesc = Constants.vbNullString;
            try
            {
                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();
                CreateDataAdapterCommands();
                trnPreMaterial = Module1.cnProductionPlanning.BeginTransaction();
                daPreItems.DeleteCommand.Transaction = trnPreMaterial;
                daPreItems.InsertCommand.Transaction = trnPreMaterial;
                daProductOPCs.UpdateCommand.Transaction = trnPreMaterial;
                dvPreItems.RowFilter = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("TreeCode=", mOpCurrentRow["TreeCode"]), " And CurrentOperationCode='"), mOpCurrentRow["OperationCode"]), "'"));
                for (int I = 0, loopTo = dvPreItems.Count - 1; I <= loopTo; I++)
                {
                    if (I < dgPreItems.Rows.Count - 1)
                    {
                        if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(dvPreItems[I]["MaterialCode"], dgPreItems.Rows[I].Cells[0].Value, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(dvPreItems[I]["OneUnitBuildAmount"], dgPreItems.Rows[I].Cells[1].Value, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(dvPreItems[I]["ProductionTestUnit"], dgPreItems.Rows[I].Cells[2].Value, false)))

                        {
                            mAlarmDesc = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("مشخصات عملیات: {", mOpCurrentRow["OperationCode"]), "} تغییر کرد"));
                            break;
                        }
                    }
                }

                if (string.IsNullOrEmpty(mAlarmDesc))
                {
                    if (dvPreItems.Count != dgPreItems.Rows.Count - 1)
                    {
                        mAlarmDesc = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("مشخصات عملیات: {", mOpCurrentRow["OperationCode"]), "} تغییر کرد"));
                    }
                }

                for (I = (short)(dsProductionPlanning.Tables["Tbl_OperationMaterials"].DefaultView.Count - 1); I >= 0; I += -1)
                    dsProductionPlanning.Tables["Tbl_OperationMaterials"].DefaultView[I].Delete();

                // اضافه کردن رکورد جدید به جدول در دیتاست
                var loopTo1 = (short)(dgPreItems.Rows.Count - 2);
                for (I = 0; I <= loopTo1; I++)
                    AddNewPreItem(I);
                if (dgPreItems.Rows.Count > 1)
                {
                    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(mOpCurrentRow["HavePreItem"], 1, false)))
                    {
                        mOpCurrentRow["HavePreItem"] = 1;
                    }
                }
                else if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(mOpCurrentRow["HavePreItem"], 0, false)))
                {
                    mOpCurrentRow["HavePreItem"] = 0;
                }

                SaveChanges();
                trnPreMaterial.Commit();
                if (!string.IsNullOrEmpty(mAlarmDesc))
                {
                    Module1.Check_Subbatch_HasPlanningAlarm(Conversions.ToString(mOpCurrentRow["TreeCode"]), "", "", "1", mAlarmDesc);
                }

                Close();
            }
            catch (Exception objEx)
            {
                mdsProductionPlanning.RejectChanges();
                if (trnPreMaterial is object)
                {
                    trnPreMaterial.Rollback();
                }

                Logger.SaveError(Name + ".CmdSave_Click", objEx.Message);
                MessageBox.Show("ثبت تغییرات با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
            finally
            {
                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                    Module1.cnProductionPlanning.Close();
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Controls_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Strings.Asc(e.KeyChar) == (int)Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void dgPreItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                string PreItemCodes = Constants.vbNullString;
                var loopTo = (short)(dgPreItems.Rows.Count - 2);
                for (I = 0; I <= loopTo; I++)
                {
                    if (I != e.RowIndex && Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(dgPreItems.Rows[I].Cells[0].Value, Constants.vbNullString, false)) && dgPreItems.Rows[I].Cells[0].Value is object)
                    {
                        if (string.IsNullOrEmpty(PreItemCodes))
                        {
                            PreItemCodes = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("'", dgPreItems.Rows[I].Cells[0].Value), "'"));
                        }
                        else
                        {
                            PreItemCodes = Conversions.ToString(PreItemCodes + Operators.ConcatenateObject(Operators.ConcatenateObject(",'", dgPreItems.Rows[I].Cells[0].Value), "'"));
                        }
                    }
                }

                if (!string.IsNullOrEmpty(PreItemCodes))
                {
                    dvMaterials.RowFilter = "Not MaterialCode IN(" + PreItemCodes + ")";
                }
                else
                {
                    dvMaterials.RowFilter = "";
                }

                dgPreItems.Rows[e.RowIndex].Cells[0].Dispose();
                dgPreItems.Rows[e.RowIndex].Cells[0] = Module1.CreateComboBoxCell(dvMaterials, null, "", "MaterialTitle", "MaterialCode", 300);
            }
        }

        private void dgPreItems_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void FormLoad()
        {
            try
            {
                daPreItems.SelectCommand = new SqlCommand("Select A.MaterialCode,A.MaterialCode +' '+ A.MaterialTitle As MaterialTitle,B.Code " + "From Tbl_PrimaryMaterials A INNER JOIN Tbl_TestUnits B ON A.StoreUnit=B.Code", Module1.cnProductionPlanning);
                daPreItems.Fill(dtMaterials);
                dvMaterials = dtMaterials.DefaultView;
                dvPreItems = dsProductionPlanning.Tables["Tbl_OperationMaterials"].DefaultView;
                dvTestUnits = dsProductionPlanning.Tables["Tbl_ProductionTestUnits"].DefaultView;
                dvPreItems.RowFilter = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("CurrentOperationCode='", mOpCurrentRow["OperationCode"]), "'"));
                FillForm();
            }
            catch (Exception objEx)
            {
                Logger.SaveError(Name + ".FormLoad", objEx.Message);
                MessageBox.Show("فراخوانی فرم با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }

        private void FillForm()
        {
            dgPreItems.Columns.RemoveAt(2);
            dgPreItems.Columns.Insert(2, Module1.CreateComboBoxColumn(dvTestUnits, "", "Title", "Code", "واحد سنجش تولید", 180, 180));
            if (dvPreItems.Count > 0)
            {
                DataGridViewRow dgRowInsert;
                var loopTo = (short)(dvPreItems.Count - 1);
                for (I = 0; I <= loopTo; I++)
                {
                    dgRowInsert = (DataGridViewRow)dgPreItems.Rows[dgPreItems.Rows.Count - 1].Clone();
                    dgRowInsert.Cells[0] = Module1.CreateComboBoxCell(dvMaterials, null, "", "MaterialTitle", "MaterialCode", 300);
                    dgRowInsert.SetValues(dvPreItems[I]["MaterialCode"], dvPreItems[I]["OneUnitBuildAmount"], dvPreItems[I]["ProductionTestUnit"]);
                    dgPreItems.Rows.Add(dgRowInsert);
                }
            }
        }

        private void AddNewPreItem(short RowIndex)
        {
            DataRow drInsert;
            drInsert = dsProductionPlanning.Tables["Tbl_OperationMaterials"].NewRow();
            drInsert["TreeCode"] = mOpCurrentRow["TreeCode"];
            drInsert["CurrentOperationCode"] = mOpCurrentRow["OperationCode"];
            drInsert["MaterialCode"] = dgPreItems.Rows[RowIndex].Cells[0].Value;
            drInsert["OneUnitBuildAmount"] = dgPreItems.Rows[RowIndex].Cells[1].Value;
            drInsert["ProductionTestUnit"] = dgPreItems.Rows[RowIndex].Cells[2].Value;
            dsProductionPlanning.Tables["Tbl_OperationMaterials"].Rows.Add(drInsert);

            // Dim CurrentCell As System.Windows.Forms.DataGridViewComboBoxCell = CType(dgPreItems.Rows(RowIndex).Cells(2), System.Windows.Forms.DataGridViewComboBoxCell)
            // Dim dtSource As System.Windows.Forms.BindingSource = CType(CurrentCell.DataSource, System.Windows.Forms.BindingSource)
            // Dim dgrw As System.Data.DataRowView = CType(dtSource.Current, System.Data.DataRowView)
            // drInsert("ProductionTestUnit") = dgrw.Row.ItemArray(0)
            // drInsert("ProductionTestUnitTitle") = dgrw.Row.ItemArray(1)
        }

        private void CreateDataAdapterCommands()
        {
            // -------------------- ایجاد دستورات جدول مواد/ قطعه وارده به عملیات ----------------------
            // ایجاد دستور اضافه کردن رکورد جدید در جدول
            daPreItems.InsertCommand = new SqlCommand("Insert Into Tbl_OperationMaterials(TreeCode,CurrentOperationCode,MaterialCode,OneUnitBuildAmount,ProductionTestUnit) " + "Values(@TreeCode,@CurrentOperationCode,@MaterialCode,@OneUnitBuildAmount,@ProductionTestUnit)", Module1.cnProductionPlanning);
            {
                var withBlock = daPreItems.InsertCommand;
                withBlock.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode");
                withBlock.Parameters.Add("@CurrentOperationCode", SqlDbType.VarChar, 50, "CurrentOperationCode");
                withBlock.Parameters.Add("@MaterialCode", SqlDbType.VarChar, 50, "MaterialCode");
                withBlock.Parameters.Add("@OneUnitBuildAmount", SqlDbType.Float, default, "OneUnitBuildAmount");
                withBlock.Parameters.Add("@ProductionTestUnit", SqlDbType.Int, default, "ProductionTestUnit");
            }
            // ایجاد دستور حذف رکورد جاری در جدول
            daPreItems.DeleteCommand = new SqlCommand("Delete From Tbl_OperationMaterials Where TreeCode=@TreeCode And CurrentOperationCode=@CurrentOperationCode And MaterialCode=@MaterialCode", Module1.cnProductionPlanning);
            {
                var withBlock1 = daPreItems.DeleteCommand;
                withBlock1.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode").SourceVersion = DataRowVersion.Original;
                withBlock1.Parameters.Add("@CurrentOperationCode", SqlDbType.VarChar, 50, "CurrentOperationCode").SourceVersion = DataRowVersion.Original;
                withBlock1.Parameters.Add("@MaterialCode", SqlDbType.VarChar, 50, "MaterialCode").SourceVersion = DataRowVersion.Original;
            }

            // -------------------- ایجاد دستور ویرایش جدول عملیاتها----------------------
            daProductOPCs.UpdateCommand = new SqlCommand("Update Tbl_ProductOPCs Set HavePreItem=@HavePreItem Where TreeCode=@TreeCode And OperationCode=@OperationCode", Module1.cnProductionPlanning);
            {
                var withBlock2 = daProductOPCs.UpdateCommand;
                withBlock2.Parameters.Add("@HavePreItem", SqlDbType.TinyInt, default, "HavePreItem");
                withBlock2.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode").SourceVersion = DataRowVersion.Original;
                withBlock2.Parameters.Add("@OperationCode", SqlDbType.VarChar, 50, "OperationCode").SourceVersion = DataRowVersion.Original;
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
                daPreItems.Update(dsChanges, "Tbl_OperationMaterials");
                daProductOPCs.Update(dsChanges, "Tbl_ProductOPCs");
                mdsProductionPlanning.AcceptChanges();
            }

            dsChanges = null;
        }

        private bool ValidationForm()
        {
            var loopTo = (short)(dgPreItems.Rows.Count - 2);
            for (I = 0; I <= loopTo; I++)
            {
                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgPreItems.Rows[I].Cells[0].Value, Constants.vbNullString, false)) || dgPreItems.Rows[I].Cells[0].Value is null)
                {
                    MessageBox.Show("نام مواد/ قطعه را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return false;
                }

                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgPreItems.Rows[I].Cells[1].Value, Constants.vbNullString, false)) || dgPreItems.Rows[I].Cells[1].Value is null)
                {
                    MessageBox.Show("مقدار/تعداد را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return false;
                }

                if (dgPreItems.Rows[I].Cells[2].Value is null || dgPreItems.Rows[I].Cells[2].Value.ToString().Equals(""))
                {
                    MessageBox.Show("واحد سنجش تولید را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return false;
                }

                var loopTo1 = (short)(dgPreItems.Rows.Count - 2);
                for (J = (short)(I + 1); J <= loopTo1; J++)
                {
                    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgPreItems.Rows[I].Cells[0].Value, dgPreItems.Rows[J].Cells[0].Value, false)))
                    {
                        MessageBox.Show("مواد/قطعه بیش از یکبار انتخاب شده است", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        return false;
                    }
                }
            }

            return true;
        }
    }
}