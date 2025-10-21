using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using TreeView = System.Windows.Forms.TreeView;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using static ProductionPlanning.MyEnums;

namespace ProductionPlanning
{
    public partial class frmProductTreeDetail
    {
        public frmProductTreeDetail()
        {
            InitializeComponent();
            _TreeView1.Name = "TreeView1";
            _mnuItemNewNode.Name = "mnuItemNewNode";
            _mnuItemEditNode.Name = "mnuItemEditNode";
            _mnuItemDeleteNode.Name = "mnuItemDeleteNode";
            _cmdCancel.Name = "cmdCancel";
            _dgPreItems.Name = "dgPreItems";
            _mnuPreItems.Name = "mnuPreItems";
            _mnuPI_Spcification.Name = "mnuPI_Spcification";
            _dgPreOperations.Name = "dgPreOperations";
            _mnuPreOperation.Name = "mnuPreOperation";
            _mnuPO_Spcification.Name = "mnuPO_Spcification";
            _dgExecutorMachines.Name = "dgExecutorMachines";
            _mnuExecMachines.Name = "mnuExecMachines";
            _mnuEM_New.Name = "mnuEM_New";
            _mnuEM_Edit.Name = "mnuEM_Edit";
            _mnuEM_Delete.Name = "mnuEM_Delete";
            _cmdOperationBuiltGraphicView.Name = "cmdOperationBuiltGraphicView";
            _cmdEditOperation.Name = "cmdEditOperation";
            _cmdRemoveOperation.Name = "cmdRemoveOperation";
            _cmdAddOperation.Name = "cmdAddOperation";
            _dgOperations.Name = "dgOperations";
        }

        private DataSetConfiguration DataSetConfig = new DataSetConfiguration();
        private SqlDataAdapter daProductOPCs = new SqlDataAdapter();
        private SqlDataAdapter daPreOperations = new SqlDataAdapter();
        private SqlDataAdapter daNonExecutorMachines = new SqlDataAdapter();
        private SqlDataAdapter daContractorOperation = new SqlDataAdapter();
        private SqlDataAdapter daOperationMaterial = new SqlDataAdapter();
        private DataRow CurrentRow;
        private TreeNode CurrentNode;
        private DataRow OpCurrentRow = null;

        // Private dvGridSource As DataView
        private string AddedOperations = Constants.vbNullString;
        public short FormMode;
        private short I;

        private DataSet mdsTreeDetail
        {
            get
            {
                return DataSetConfig.dsProductionPlanning;
            }
        }

        private void frmProductTreeDetail_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdOperationBuiltGraphicView, 13);
            mnuTreeView.ImageList = Module1.MainFormObject.ImageList1;
            mnuTreeView.Items["mnuItemNewNode"].ImageIndex = 0;
            mnuTreeView.Items["mnuItemEditNode"].ImageIndex = 2;
            mnuTreeView.Items["mnuItemDeleteNode"].ImageIndex = 1;
            {
                var withBlock = mnuExecMachines;
                withBlock.ImageList = Module1.MainFormObject.ImageList1;
                withBlock.Items["mnuEM_New"].ImageIndex = 0;
                withBlock.Items["mnuEM_Edit"].ImageIndex = 2;
                withBlock.Items["mnuEM_Delete"].ImageIndex = 1;
            }

            Module1.SetButtonsImage(cmdAddOperation, 0);
            Module1.SetButtonsImage(cmdEditOperation, 2);
            Module1.SetButtonsImage(cmdRemoveOperation, 1);
            FormLoad();

            // Set User Access Right  '''''''''''''''
            mnuTreeView.Items["mnuItemNewNode"].Enabled = Module_UserAccess.HaveAccessToItem(29);
            mnuTreeView.Items["mnuItemEditNode"].Enabled = Module_UserAccess.HaveAccessToItem(30);
            mnuTreeView.Items["mnuItemDeleteNode"].Enabled = Module_UserAccess.HaveAccessToItem(31);
            cmdAddOperation.Enabled = Module_UserAccess.HaveAccessToItem(33);
            cmdEditOperation.Enabled = Module_UserAccess.HaveAccessToItem(34);
            cmdRemoveOperation.Enabled = Module_UserAccess.HaveAccessToItem(35);
            mnuEM_New.Enabled = Module_UserAccess.HaveAccessToItem(37);
            mnuEM_Edit.Enabled = Module_UserAccess.HaveAccessToItem(38);
            mnuEM_Delete.Enabled = Module_UserAccess.HaveAccessToItem(39);
            mnuPO_Spcification.Enabled = Module_UserAccess.HaveAccessToItem(40);
            mnuPI_Spcification.Enabled = Module_UserAccess.HaveAccessToItem(41);
            // '''''''''''''''''''''''''''''''''''''''' 
        }

        private void frmProductTreeDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            string Query;
            Query = "Select A.TreeCode,A.OperationCode,A.MachineCode,B.Name AS MachineName,A.MachinePriority," + "       A.Operators,A.OneExecutionTime,A.TimeType,A.MinimumProduction,A.MaximumAccumulation,A.GarbagePercent," + "       A.PerformancePercent,A.OperatorPerformancePercent,A.RedoPercent,A.ExecutionPerformancePercent," + "       A.MachineSetupTime,A.SetupTimeType,A.OneExecutionPrice,A.CalendarCode,A.IsParallelMachine " + "From   dbo.Tbl_ProductOPCsExecutorMachines A INNER JOIN dbo.Tbl_Machines B ON A.MachineCode = B.Code";



            DataSetConfig.FillDataSet("Tbl_ProductOPCsExecutorMachines", "Tbl_ProductOPCsExecutorMachines", Conversions.ToString(Operators.ConcatenateObject(Query + " Where A.TreeCode=", TreeView1.Tag)), "TreeCode", "OperationCode", "MachineCode");
            DataSetConfig.FillDataSet("Tbl_Machines", "Tbl_Machines", "Select * From Tbl_Machines Order By Code", "Code");
            DataSetConfig.FillDataSet("Tbl_Calendar", "Tbl_Calendar", "Select * From Tbl_Calendar Order By CalendarTitle", "CalendarCode");
            if (!OperationNetwork_Paths_Build())
            {
                e.Cancel = true;
                {
                    var withBlock = mdsTreeDetail;
                    withBlock.RejectChanges();
                    for (I = (short)(withBlock.Tables.Count - 1); I >= 2; I += -1)
                    {
                        withBlock.Tables[I].Constraints.Clear();
                        withBlock.Tables[I].Dispose();
                        withBlock.Tables.RemoveAt(I);
                    }
                }

                return;
            }

            {
                var withBlock1 = mdsTreeDetail;
                withBlock1.RejectChanges();
                withBlock1.Relations.Clear();
                for (I = (short)(withBlock1.Tables.Count - 1); I >= 0; I += -1)
                {
                    withBlock1.Tables[I].Constraints.Clear();
                    withBlock1.Tables[I].Dispose();
                    withBlock1.Tables.RemoveAt(I);
                }
            }

            daProductOPCs.Dispose();
            daPreOperations.Dispose();
            daNonExecutorMachines.Dispose();
            daContractorOperation.Dispose();
            daOperationMaterial.Dispose();
            CurrentRow = null;
            CurrentNode = null;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            dgExecutorMachines.DataSource = null;
            dgPreItems.DataSource = null;
            dgPreOperations.DataSource = null;
            var drRows = mdsTreeDetail.Tables["Tbl_ProductTreeDetails"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("DetailCode='", e.Node.Tag), "'")));
            CurrentRow = drRows[0];
            CurrentNode = e.Node;
            mnuTreeView.Items["mnuItemNewNode"].Enabled = Module_UserAccess.HaveAccessToItem(29);
            mnuTreeView.Items["mnuItemEditNode"].Enabled = Module_UserAccess.HaveAccessToItem(30);
            cmdAddOperation.Enabled = Module_UserAccess.HaveAccessToItem(33);
            cmdEditOperation.Enabled = Module_UserAccess.HaveAccessToItem(34);
            cmdRemoveOperation.Enabled = Module_UserAccess.HaveAccessToItem(35);
            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(CurrentRow["ParentDetailCode"], "0", false)))
            {
                mnuTreeView.Items["mnuItemDeleteNode"].Enabled = Module_UserAccess.HaveAccessToItem(31);
            }
            else
            {
                mnuTreeView.Items["mnuItemDeleteNode"].Enabled = false;
            }

            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(CurrentRow["BuiltType"], 1, false)))
            {
                cmdOperationBuiltGraphicView.Enabled = true;
                LoadDetailOperationsToGrid();
            }
            else
            {
                LoadDetailOperationsToGrid();
                dgExecutorMachines.DataSource = null;
                dgPreItems.DataSource = null;
                dgPreOperations.DataSource = null;
                cmdAddOperation.Enabled = false;
                cmdEditOperation.Enabled = false;
                cmdRemoveOperation.Enabled = false;
            }
        }

        private void cmdBuildOperation_Click(object sender, EventArgs e)
        {
            // With frmOperationBract
            // .lblTree.Text = frmProductTreesList.dgList.CurrentRow.Cells(2).Value
            // .lblTree.Tag = TreeView1.Tag
            // .lblProduct.Text = frmProductTreesList.txtProductCode.Text & " -- " & frmProductTreesList.cbProductName.Text

            // If CurrentNode.Level > 0 Then
            // .lblParent.Text = CurrentNode.Parent.Tag & " -- " & CurrentNode.Parent.Text
            // Else
            // .lblParent.Text = "عنصر ریشه"
            // End If

            // .lblCurrent.Text = CurrentNode.Tag & " -- " & CurrentNode.Text
            // .lblCurrent.Tag = CurrentNode.Tag

            // .dsProductionPlanning = dsProductionPlanning

            // .ShowDialog()
            // .Dispose()

            // dsProductionPlanning.Tables("Tbl_ProductOPCs").DefaultView.RowFilter = vbNullString
            // End With
        }

        private void dgOperations_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(dgOperations.Rows[e.RowIndex].Cells["OperationCode"].Value, Constants.vbNullString, false)))
            {
                string mOpCode = dgOperations.Rows[e.RowIndex].Cells["OperationCode"].Value.ToString();
                var CurrentRows = mdsTreeDetail.Tables["Tbl_ProductOPCs"].Select("OperationCode='" + mOpCode + "'");
                OpCurrentRow = CurrentRows[0];
                SetMiniGridsSource();
                if (!string.IsNullOrEmpty(Module1.GetMatchedOperations(mOpCode, TreeView1.Tag.ToString())))
                {
                    dgPreOperations.Enabled = false;
                    dgExecutorMachines.Enabled = false;
                }
                else
                {
                    dgPreOperations.Enabled = true;
                    dgExecutorMachines.Enabled = true;
                }
            }
            else
            {
                OpCurrentRow = null;
                dgPreItems.DataSource = null;
                dgPreOperations.DataSource = null;
                dgExecutorMachines.DataSource = null;
            }
        }

        private void cmdAddOperation_Click(object sender, EventArgs e)
        {
            string Query;
            string QueryCondition = Conversions.ToString(Operators.ConcatenateObject(" Where TreeCode=", TreeView1.Tag));
            DataSetConfig.FillDataSet("Tbl_Calendar", "Tbl_Calendar", "Select * From Tbl_Calendar", "CalendarCode");
            Query = Conversions.ToString(Operators.ConcatenateObject("Select A.TreeCode,A.OperationCode,A.MachineCode,A.MachinePriority," + "       A.Operators,A.OneExecutionTime,A.TimeType,A.MinimumProduction,A.MaximumAccumulation,A.GarbagePercent," + "       A.PerformancePercent,A.OperatorPerformancePercent,A.RedoPercent,A.ExecutionPerformancePercent," + "       A.MachineSetupTime,A.SetupTimeType,A.OneExecutionPrice,A.CalendarCode,A.IsParallelMachine " + "From   dbo.Tbl_ProductOPCsExecutorMachines A INNER JOIN dbo.Tbl_Machines B ON A.MachineCode = B.Code " + "Where A.TreeCode=", TreeView1.Tag));




            DataSetConfig.FillDataSet("Tbl_ProductOPCsExecutorMachines", "Tbl_ProductOPCsExecutorMachines", Query, "TreeCode", "OperationCode", "MachineCode");
            Query = Conversions.ToString(Operators.ConcatenateObject("Select A.TreeCode,A.OperationCode,A.ContractorCode,B.ContractorName,A.MinimumTransferBatch," + "       A.TimeType,A.TransferBatchExecutionTime,A.BatchCapacity,A.CalendarCode " + "FROM   dbo.Tbl_ContractorOperations A INNER JOIN dbo.Tbl_Contractors B ON A.ContractorCode = B.ContractorCode " + "Where A.TreeCode=", TreeView1.Tag));


            DataSetConfig.FillDataSet("Tbl_ContractorOperations", "Tbl_ContractorOperations", Query, "TreeCode", "OperationCode", "ContractorCode");
            DataSetConfig.FillDataSet("Tbl_OperationsDefaultTitles", "Tbl_OperationsDefaultTitles", "Select * From Tbl_OperationsDefaultTitles Order By Title", "Code");
            DataSetConfig.FillDataSet("Tbl_Natures", "Tbl_Natures", "Select * From Tbl_Natures Order By NatureTitle", "NatureCode");
            DataSetConfig.FillDataSet("Tbl_Contractors", "Tbl_Contractors", "Select * From Tbl_Contractors Order By ContractorName", "ContractorCode");
            if (OpCurrentRow is object)
            {
                DataSetConfig.FillDataSet("Tbl_MatchedOperations", "Tbl_MatchedOperations", "Select * From Tbl_MatchedOperations" + QueryCondition + " And (OperationCode = '" + OpCurrentRow["OperationCode"].ToString() + "' Or MatchedOperationCode = '" + OpCurrentRow["OperationCode"].ToString() + "')", "TreeCode", "OperationCode", "MatchedOperationCode");
            }
            else
            {
                DataSetConfig.FillDataSet("Tbl_MatchedOperations", "Tbl_MatchedOperations", "Select * From Tbl_MatchedOperations" + QueryCondition + " And OperationCode = ''", "TreeCode", "OperationCode", "MatchedOperationCode");
            }

            if ((ReferenceEquals(sender, cmdEditOperation) || ReferenceEquals(sender, cmdRemoveOperation)) && dgOperations.Rows.Count == 0)
            {
                MessageBox.Show("رکوردی وجود ندارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                return;
            }
            else if ((ReferenceEquals(sender, cmdEditOperation) || ReferenceEquals(sender, cmdRemoveOperation)) && OpCurrentRow is null)
            {
                MessageBox.Show("رکوردی انتخاب نشده است", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                return;
            }

            {
                var withBlock = new frmBractOperation();
                if (ReferenceEquals(sender, cmdAddOperation))
                {
                    withBlock.FormMode = (short)Module1.FormModeEnum.INSERT_MODE;
                }
                else if (ReferenceEquals(sender, cmdEditOperation))
                {
                    withBlock.FormMode = (short)Module1.FormModeEnum.EDIT_MODE;
                    withBlock.CurrentRow = OpCurrentRow;
                }
                else if (ReferenceEquals(sender, cmdRemoveOperation))
                {
                    if (MessageBox.Show("عمليات انتخاب شده را حذف مي كنيد", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading, false) == DialogResult.Yes)
                    {
                        string mAlarmDesc = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("عملیات: {", OpCurrentRow["OperationCode"]), "} حذف گردید"));
                        SqlTransaction trnDelete = null;
                        try
                        {
                            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                                Module1.cnProductionPlanning.Open();
                            {
                                var withBlock1 = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select Count(*) From Tbl_Planning Where TreeCode=", TreeView1.Tag), " And OperationCode='"), OpCurrentRow["OperationCode"]), "'")), Module1.cnProductionPlanning);
                                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(withBlock1.ExecuteScalar(), 0, false)))
                                {
                                    MessageBox.Show("عملیات انتخاب شده دارای رکورد برنامه ریزی بوده و مجاز به حذف نمی باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                                    if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                        Module1.cnProductionPlanning.Close();
                                    return;
                                }

                                Query = Conversions.ToString(Operators.ConcatenateObject("Select A.TreeCode,A.CurrentOperationCode,A.PreOperationCode,A.RelationType,A.LagTime,A.TimeType " + "From   dbo.Tbl_PreOperations A INNER JOIN dbo.Tbl_ProductOPCs B ON A.TreeCode = B.TreeCode And " + "       A.CurrentOperationCode = B.OperationCode " + "Where  A.TreeCode=", TreeView1.Tag));
                                DataSetConfig.FillDataSet("Tbl_PreOperations", "Tbl_PreOperations", Query, "TreeCode", "CurrentOperationCode", "PreOperationCode");
                                trnDelete = Module1.cnProductionPlanning.BeginTransaction();
                                withBlock1.Transaction = trnDelete;
                                withBlock1.CommandText = "Delete From Tbl_OperationMaterials Where TreeCode = " + TreeView1.Tag.ToString() + " And CurrentOperationCode = '" + OpCurrentRow["OperationCode"].ToString() + "'";
                                withBlock1.ExecuteNonQuery();
                            }

                            daPreOperations.UpdateCommand.Transaction = trnDelete;
                            daPreOperations.DeleteCommand.Transaction = trnDelete;
                            daNonExecutorMachines.DeleteCommand.Transaction = trnDelete;
                            daContractorOperation.DeleteCommand.Transaction = trnDelete;
                            // daOperationMaterial.DeleteCommand.Transaction = trnDelete
                            daProductOPCs.DeleteCommand.Transaction = trnDelete;
                            var AfterwardRow = mdsTreeDetail.Tables["Tbl_PreOperations"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("TreeCode=", OpCurrentRow["TreeCode"]), " And PreOperationCode='"), OpCurrentRow["OperationCode"]), "'")));
                            var PrerequisiteRows = mdsTreeDetail.Tables["Tbl_PreOperations"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("TreeCode=", OpCurrentRow["TreeCode"]), " And CurrentOperationCode='"), OpCurrentRow["OperationCode"]), "'")));
                            if (AfterwardRow.Length > 0) // در صورتيكه پسنياز داشته باشد
                            {
                                if (PrerequisiteRows.Length > 0) // در صورتيكه پيشنياز نيز داشته باشد
                                {
                                    for (short I = 0, loopTo = (short)(PrerequisiteRows.Length - 1); I <= loopTo; I++)
                                        PrerequisiteRows[I]["CurrentOperationCode"] = AfterwardRow[0]["CurrentOperationCode"];
                                }

                                AfterwardRow[0].Delete();
                            }
                            else if (PrerequisiteRows.Length > 0)
                            {
                                for (I = (short)(PrerequisiteRows.Length - 1); I >= 0; I += -1)
                                    PrerequisiteRows[I].Delete();
                            }

                            // حذف ركوردهاي جدول مواد وارده به عمليات
                            // Dim DeleteRows() As DataRow = mdsTreeDetail.Tables("Tbl_OperationMaterials").Select("TreeCode=" & OpCurrentRow("TreeCode") & " And CurrentOperationCode='" & OpCurrentRow("OperationCode") & "'")

                            // If DeleteRows.Length > 0 Then
                            // For I = DeleteRows.Length - 1 To 0 Step -1
                            // DeleteRows(I).Delete()
                            // Next I
                            // End If
                            // With New SqlCommand("Tbl_PrimaryMaterials", cnProductionPlanning)

                            // Query = "Select A.TreeCode,A.CurrentOperationCode,A.MaterialCode," & _
                            // "       A.OneUnitBuildAmount,A.ProductionTestUnit " & _
                            // "From   dbo.Tbl_OperationMaterials A INNER JOIN dbo.Tbl_PrimaryMaterials B " & _
                            // "       ON A.MaterialCode = B.MaterialCode INNER JOIN " & _
                            // "       dbo.Tbl_TestUnits C ON B.ProductionUnit = C.Code " & _
                            // "Where  A.TreeCode=" & TreeView1.Tag
                            // DataSetConfig.FillDataSet("Tbl_OperationMaterials", "Tbl_OperationMaterials", Query, "TreeCode", "CurrentOperationCode", "MaterialCode")
                            // End With

                            // حذف ركوردهاي جدول ماشينهاي انجام دهنده عمليات
                            var DeleteRows = mdsTreeDetail.Tables["Tbl_ProductOPCsExecutorMachines"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("TreeCode=", OpCurrentRow["TreeCode"]), " And OperationCode='"), OpCurrentRow["OperationCode"]), "'")));
                            if (DeleteRows.Length > 0)
                            {
                                for (I = (short)(DeleteRows.Length - 1); I >= 0; I += -1)
                                    DeleteRows[I].Delete();
                            }

                            // حذف ركوردهاي جدول پيمانكاران عمليات
                            DeleteRows = mdsTreeDetail.Tables["Tbl_ContractorOperations"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("TreeCode=", OpCurrentRow["TreeCode"]), " And OperationCode='"), OpCurrentRow["OperationCode"]), "'")));
                            if (DeleteRows.Length > 0)
                            {
                                for (I = (short)(DeleteRows.Length - 1); I >= 0; I += -1)
                                    DeleteRows[I].Delete();
                            }

                            // حذف عملیات های جفت
                            DeleteRows = mdsTreeDetail.Tables["Tbl_MatchedOperations"].Select("OperationCode = '" + OpCurrentRow["OperationCode"].ToString() + "' Or MatchedOperationCode = '" + OpCurrentRow["OperationCode"].ToString() + "'");
                            if (DeleteRows.Length > 0)
                            {
                                for (I = (short)(DeleteRows.Length - 1); I >= 0; I += -1)
                                    DeleteRows[I].Delete();
                            }

                            var cm = new SqlCommand("Delete From Tbl_MatchedOperations Where OperationCode = '" + OpCurrentRow["OperationCode"].ToString() + "' Or MatchedOperationCode = '" + OpCurrentRow["OperationCode"].ToString() + "'", trnDelete.Connection);
                            cm.Transaction = trnDelete;
                            cm.ExecuteNonQuery();

                            // حذف عمليات
                            OpCurrentRow.Delete();
                            daPreOperations.Update(mdsTreeDetail, "Tbl_PreOperations");
                            daNonExecutorMachines.Update(mdsTreeDetail, "Tbl_ProductOPCsExecutorMachines");
                            daContractorOperation.Update(mdsTreeDetail, "Tbl_ContractorOperations");
                            // daOperationMaterial.Update(mdsTreeDetail, "Tbl_OperationMaterials")
                            daProductOPCs.Update(mdsTreeDetail, "Tbl_ProductOPCs");
                            mdsTreeDetail.AcceptChanges();
                            trnDelete.Commit();
                            OpCurrentRow = null;
                            LoadDetailOperationsToGrid();
                            Module1.Check_Subbatch_HasPlanningAlarm(TreeView1.Tag.ToString(), "", "", "1", mAlarmDesc);
                        }
                        catch (Exception objEx)
                        {
                            mdsTreeDetail.Tables["Tbl_MatchedOperations"].RejectChanges();
                            trnDelete.Rollback();
                            Logger.SaveError("FrmProductTreeDetail.cmdAddOperation_Click", objEx.Message);
                            MessageBox.Show("حذف عملیات با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        }
                        finally
                        {
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                            trnDelete.Dispose();
                        }
                    }

                    goto UnloadTables;
                }

                withBlock.TreeCode = Conversions.ToString(TreeView1.Tag);
                withBlock.DetailCode = Conversions.ToString(CurrentNode.Tag);
                withBlock.dsProductionPlanning = mdsTreeDetail;
                if (withBlock.ShowDialog() == DialogResult.OK)
                {
                    LoadDetailOperationsToGrid();
                }

                withBlock.Dispose();
            }

        UnloadTables:
            ;
            {
                var withBlock2 = mdsTreeDetail;
                withBlock2.RejectChanges();
                for (I = (short)(withBlock2.Tables.Count - 1); I >= 2; I += -1)
                {
                    withBlock2.Tables[I].Constraints.Clear();
                    withBlock2.Tables[I].Dispose();
                    withBlock2.Tables.RemoveAt(I);
                }
            }

            SetMiniGridsSource();
        }

        private void TreeView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var curNode = TreeView1.GetNodeAt(new Point(e.X, e.Y));
                TreeView1.SelectedNode = curNode;
            }
        }

        private void TreeView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var curNode = TreeView1.GetNodeAt(new Point(e.X, e.Y));
                if (curNode is object)
                {
                    TreeView1.SelectedNode = curNode;
                }
                else
                {
                    TreeView1.SelectedNode = TreeView1.Nodes[0];
                }
            }
        }

        private void dgPreItems_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgPreItems.DataSource is null)
            {
                mnuPreItems.Hide();
            }
        }

        private void dgPreOperations_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgPreOperations.DataSource is null)
            {
                mnuPreOperation.Hide();
            }
        }

        private void dgExecutorMachines_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (dgExecutorMachines.DataSource is null)
                {
                    mnuExecMachines.Hide();
                }

                if (OpCurrentRow is object && Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(OpCurrentRow["ExecutionMethod"], 1, false)))
                {
                    mnuExecMachines.Hide();
                }
                else
                {
                    var objCell = dgExecutorMachines.HitTest(e.X, e.Y);
                    if (objCell.RowIndex > -1 && objCell.ColumnIndex > -1)
                    {
                        dgExecutorMachines.ClearSelection();
                        dgExecutorMachines.Rows[objCell.RowIndex].Cells[objCell.ColumnIndex].Selected = true;
                    }
                }
            }
        }

        private void mnuNodeItems_Click(object sender, EventArgs e)
        {
            DataSetConfig.FillDataSet("Tbl_TestUnits", "Tbl_ProductionTestUnits", "Select * From Tbl_TestUnits Order By Title", "Code");
            DataSetConfig.FillDataSet("Tbl_TestUnits", "Tbl_StoreTestUnits", "Select * From Tbl_TestUnits Order By Title", "Code");
            DataSetConfig.FillDataSet("Tbl_Stores", "Tbl_Stores", "Select * From Tbl_Stores Order By StoreName", "StoreCode");
            string Query;
            if (ReferenceEquals(sender, mnuItemEditNode) || ReferenceEquals(sender, mnuItemDeleteNode))
            {
                Query = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select B.BatchCode,B.ProductTreeCode, A.DetailCode " + "From   dbo.Tbl_ProductionBatchsDetail A INNER JOIN " + "       dbo.Tbl_ProductionBatchs B ON A.BatchCode = B.BatchCode " + "Where  B.ProductTreeCode=", TreeView1.Tag), " And A.DetailCode='"), CurrentRow["DetailCode"]), "'"));
                DataSetConfig.FillDataSet("Tbl_ProductionBatchsDetail", "Tbl_ProductionBatchsDetail", Query, "BatchCode", "DetailCode");
                Query = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select B.SubbatchCode,C.ProductTreeCode, A.DetailCode " + "From   dbo.Tbl_ProductionSubbatchsDetail A INNER JOIN " + "       dbo.Tbl_ProductionSubbatchs B ON A.SubbatchCode = B.SubbatchCode INNER JOIN " + "       dbo.Tbl_ProductionBatchs C ON B.BatchCode = C.BatchCode " + "Where  C.ProductTreeCode=", TreeView1.Tag), " And A.DetailCode='"), CurrentRow["DetailCode"]), "'"));
                DataSetConfig.FillDataSet("Tbl_ProductionSubbatchsDetail", "Tbl_ProductionSubbatchsDetail", Query, "SubbatchCode", "DetailCode");
            }

            {
                var withBlock = My.MyProject.Forms.frmTreeNode;
                withBlock.dsProductionPlanning = mdsTreeDetail;
                withBlock.TreeCode = Conversions.ToString(TreeView1.Tag);
                withBlock.CurrentNode = CurrentNode;
                if (ReferenceEquals(sender, mnuItemNewNode))
                {
                    withBlock.FormMode = (short)Module1.FormModeEnum.INSERT_MODE;
                    withBlock.ParentDetailCode = Conversions.ToString(CurrentRow["DetailCode"]);
                }
                else if (ReferenceEquals(sender, mnuItemEditNode))
                {
                    withBlock.FormMode = (short)Module1.FormModeEnum.EDIT_MODE;
                    withBlock.ParentDetailCode = Conversions.ToString(CurrentRow["ParentDetailCode"]);
                    withBlock.CurrentRow = CurrentRow;
                    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(CurrentRow["ParentDetailCode"], "0", false)))
                    {
                        withBlock.Panel2.Enabled = false;
                    }
                }
                else if (ReferenceEquals(sender, mnuItemDeleteNode))
                {
                    withBlock.FormMode = (short)Module1.FormModeEnum.DELETE_MODE;
                    withBlock.cmdSave.Visible = false;
                    withBlock.cmdDelete.Visible = true;
                    withBlock.ParentDetailCode = Conversions.ToString(CurrentRow["ParentDetailCode"]);
                    withBlock.CurrentRow = CurrentRow;
                }

                var FormResult = withBlock.ShowDialog();
                withBlock.Dispose();
                if (FormResult != DialogResult.Cancel)
                {
                    BuildTree();
                }
            }

            {
                var withBlock1 = mdsTreeDetail;
                for (I = (short)(withBlock1.Tables.Count - 1); I >= 2; I += -1)
                {
                    withBlock1.Tables[I].Constraints.Clear();
                    withBlock1.Tables[I].Dispose();
                    withBlock1.Tables.RemoveAt(I);
                }
            }
        }

        private void cmdPreItems_Click(object sender, EventArgs e)
        {
            if (OpCurrentRow is object)
            {
                string Query;
                DataSetConfig.FillDataSet("Tbl_PrimaryMaterials", "Tbl_PrimaryMaterials", "Select * From Tbl_PrimaryMaterials Order By MaterialTitle", "MaterialCode");
                Query = Conversions.ToString(Operators.ConcatenateObject("Select TreeCode, CurrentOperationCode, MaterialCode, OneUnitBuildAmount, ProductionTestUnit " + "From   dbo.Tbl_OperationMaterials Where TreeCode=", TreeView1.Tag));
                DataSetConfig.FillDataSet("Tbl_OperationMaterials", "Tbl_OperationMaterials", Query, "TreeCode", "CurrentOperationCode", "MaterialCode");
                DataSetConfig.FillDataSet("Tbl_TestUnits", "Tbl_ProductionTestUnits", "Select * From Tbl_TestUnits Order By Title", "Code");
                {
                    var withBlock = My.MyProject.Forms.frmOperationMaterial;
                    withBlock.OpCurrentRow = OpCurrentRow;
                    withBlock.dsProductionPlanning = mdsTreeDetail;
                    withBlock.ShowDialog();
                    withBlock.Dispose();
                }

                SetMiniGridsSource();
            }
        }

        private void cmdPreOperations_Click(object sender, EventArgs e)
        {
            string Query = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select A.TreeCode,A.CurrentOperationCode,A.PreOperationCode,A.RelationType,A.LagTime,A.TimeType " + "FROM   dbo.Tbl_PreOperations A INNER JOIN dbo.Tbl_ProductOPCs B ON A.TreeCode = B.TreeCode And " + "       A.CurrentOperationCode = B.OperationCode " + "Where  A.TreeCode=", TreeView1.Tag), " And A.CurrentOperationCode='"), OpCurrentRow["OperationCode"]), "'"));
            DataSetConfig.FillDataSet("Tbl_PreOperations", "Tbl_PreOperations", Query, "TreeCode", "CurrentOperationCode", "PreOperationCode");
            DataSetConfig.FillDataSet("Tbl_TimeTypes", "Tbl_TimeTypes", "Select * From Tbl_TimeTypes", "TypeCode");
            DataSetConfig.FillDataSet("Tbl_RelationTypes", "Tbl_RelationTypes", "Select * From Tbl_RelationTypes", "TypeCode");
            {
                var withBlock = My.MyProject.Forms.frmPreOperation;
                withBlock.OpCurrentRow = OpCurrentRow;
                withBlock.dsProductionPlanning = mdsTreeDetail;
                withBlock.ShowDialog();
                withBlock.Dispose();
            }

            SetMiniGridsSource();
        }

        private void mnuExecMachinesOperations_Click(object sender, EventArgs e)
        {
            if (ReferenceEquals(sender, mnuEM_Edit) || ReferenceEquals(sender, mnuEM_Delete))
            {
                if (dgExecutorMachines.Rows.Count == 0)
                {
                    MessageBox.Show("ماشینی وجود ندارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return;
                }
                else if (dgExecutorMachines.CurrentRow is null)
                {
                    MessageBox.Show("ماشینی انتخاب نشده است", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return;
                }
            }

            string Query = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select A.TreeCode,A.OperationCode,A.MachineCode,A.MachinePriority," + "       A.Operators,A.OneExecutionTime,A.TimeType,A.MinimumProduction,A.MaximumAccumulation,A.GarbagePercent," + "       A.PerformancePercent,A.OperatorPerformancePercent,A.RedoPercent,A.ExecutionPerformancePercent," + "       A.MachineSetupTime,A.SetupTimeType,A.OneExecutionPrice,A.CalendarCode,A.IsParallelMachine " + "From   dbo.Tbl_ProductOPCsExecutorMachines A INNER JOIN dbo.Tbl_Machines B ON A.MachineCode=B.Code " + "Where  TreeCode=", OpCurrentRow["TreeCode"]), " And OperationCode='"), OpCurrentRow["OperationCode"]), "' And MachineCode<>'-1'"));
            DataSetConfig.FillDataSet("Tbl_ProductOPCsExecutorMachines", "Tbl_ProductOPCsExecutorMachines", Query, "TreeCode", "OperationCode", "MachineCode");
            {
                var withBlock = My.MyProject.Forms.frmExecutorMachine;
                if (ReferenceEquals(sender, mnuEM_New))
                {
                    withBlock.FormMode = (short)Module1.FormModeEnum.INSERT_MODE;
                    withBlock.dsProductionPlanning = mdsTreeDetail;
                    withBlock.OpCurrentRow = OpCurrentRow;
                }
                else if (ReferenceEquals(sender, mnuEM_Edit))
                {
                    withBlock.FormMode = (short)Module1.FormModeEnum.EDIT_MODE;
                    withBlock.dsProductionPlanning = mdsTreeDetail;
                    withBlock.OpCurrentRow = OpCurrentRow;
                    var drMachine = mdsTreeDetail.Tables["Tbl_ProductOPCsExecutorMachines"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("MachineCode='", dgExecutorMachines.SelectedCells[0].Value), "'")));
                    withBlock.CurrentRow = drMachine[0];
                }
                else if (ReferenceEquals(sender, mnuEM_Delete))
                {
                    if (dgExecutorMachines.Rows.Count == 1)
                    {
                        MessageBox.Show("عملیاتی که با ماشین انجام می شود باید دارای حداقل یک ماشین باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading);
                        goto UnloadTables;
                    }

                    if (MessageBox.Show("ماشین انتخاب شده را حذف می کنید", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading) == DialogResult.Yes)
                    {
                        string mAlarmDesc = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("ماشین با کد: {", dgExecutorMachines.SelectedCells[0].Value), "} از لیست ماشین های انجام دهنده عملیات: {"), dgOperations.SelectedCells[0].Value), "} حذف گردید"));
                        var drMachine = mdsTreeDetail.Tables["Tbl_ProductOPCsExecutorMachines"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("MachineCode='", dgExecutorMachines.SelectedCells[0].Value), "'")));

                        // ایجاد دستور حذف رکورد جاری در جدول
                        var daExecutorMachines = new SqlDataAdapter();
                        daExecutorMachines.DeleteCommand = new SqlCommand("Delete From Tbl_ProductOPCsExecutorMachines Where TreeCode=@TreeCode And OperationCode=@OperationCode And MachineCode=@MachineCode", Module1.cnProductionPlanning);
                        daExecutorMachines.UpdateCommand = new SqlCommand("Update Tbl_ProductOPCsExecutorMachines Set MachinePriority=@MachinePriority Where TreeCode=@TreeCode And OperationCode=@OperationCode And MachineCode=@MachineCode", Module1.cnProductionPlanning);
                        {
                            var withBlock1 = daExecutorMachines.DeleteCommand;
                            withBlock1.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode").SourceVersion = DataRowVersion.Original;
                            withBlock1.Parameters.Add("@OperationCode", SqlDbType.VarChar, 50, "OperationCode").SourceVersion = DataRowVersion.Original;
                            withBlock1.Parameters.Add("@MachineCode", SqlDbType.VarChar, 50, "MachineCode").SourceVersion = DataRowVersion.Original;
                        }

                        {
                            var withBlock2 = daExecutorMachines.UpdateCommand;
                            withBlock2.Parameters.Add("@MachinePriority", SqlDbType.TinyInt, default, "MachinePriority");
                            withBlock2.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode").SourceVersion = DataRowVersion.Original;
                            withBlock2.Parameters.Add("@OperationCode", SqlDbType.VarChar, 50, "OperationCode").SourceVersion = DataRowVersion.Original;
                            withBlock2.Parameters.Add("@MachineCode", SqlDbType.VarChar, 50, "MachineCode").SourceVersion = DataRowVersion.Original;
                        }

                        drMachine[0].Delete();
                        var dvMachinePriority = mdsTreeDetail.Tables["Tbl_ProductOPCsExecutorMachines"].DefaultView;
                        dvMachinePriority.Sort = "MachinePriority";
                        var loopTo = (short)(dvMachinePriority.Count - 1);
                        for (I = 0; I <= loopTo; I++)
                            dvMachinePriority[I]["MachinePriority"] = I + 1;
                        daExecutorMachines.Update(mdsTreeDetail, "Tbl_ProductOPCsExecutorMachines");
                        mdsTreeDetail.AcceptChanges();
                        daExecutorMachines.Dispose();
                        Module1.Check_Subbatch_HasPlanningAlarm(TreeView1.Tag.ToString(), "", "", "1", mAlarmDesc);
                    }

                    goto UnloadTables;
                }

                withBlock.ShowDialog();
                withBlock.Dispose();
            }

        UnloadTables:
            ;
            {
                var withBlock3 = mdsTreeDetail;
                withBlock3.RejectChanges();
                for (I = (short)(withBlock3.Tables.Count - 1); I >= 2; I += -1)
                {
                    withBlock3.Tables[I].Constraints.Clear();
                    withBlock3.Tables[I].Dispose();
                    withBlock3.Tables.RemoveAt(I);
                }
            }

            SetMiniGridsSource();
        }

        private void Controls_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Strings.Asc(e.KeyChar) == (int)Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void cmdOperationBuiltGraphicView_Click(object sender, EventArgs e)
        {
            bool HasNetPaths = true;
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    var cmGraphicView = new SqlCommand("Select Count(*) From Tbl_OperationNetworkPaths Where TreeCode = " + TreeView1.Tag.ToString(), cn);
                    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(cmGraphicView.ExecuteScalar(), 0, false)))
                    {
                        string Query = "Select A.TreeCode,A.OperationCode,A.MachineCode,B.Name AS MachineName,A.MachinePriority," + "       A.Operators,A.OneExecutionTime,A.TimeType,A.MinimumProduction,A.MaximumAccumulation,A.GarbagePercent," + "       A.PerformancePercent,A.OperatorPerformancePercent,A.RedoPercent,A.ExecutionPerformancePercent," + "       A.MachineSetupTime,A.SetupTimeType,A.OneExecutionPrice,A.CalendarCode,A.IsParallelMachine " + "From   dbo.Tbl_ProductOPCsExecutorMachines A INNER JOIN dbo.Tbl_Machines B ON A.MachineCode = B.Code";
                        DataSetConfig.FillDataSet("Tbl_ProductOPCsExecutorMachines", "Tbl_ProductOPCsExecutorMachines", Conversions.ToString(Operators.ConcatenateObject(Query + " Where A.TreeCode=", TreeView1.Tag)), "TreeCode", "OperationCode", "MachineCode");
                        DataSetConfig.FillDataSet("Tbl_Machines", "Tbl_Machines", "Select * From Tbl_Machines Order By Code", "Code");
                        DataSetConfig.FillDataSet("Tbl_Calendar", "Tbl_Calendar", "Select * From Tbl_Calendar Order By CalendarTitle", "CalendarCode");
                        HasNetPaths = OperationNetwork_Paths_Build();
                    }

                    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(cmGraphicView.ExecuteScalar(), 0, false)))
                    {
                        HasNetPaths = false;
                        MessageBox.Show("عملیاتی یافت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    }
                }
                catch (Exception objEx)
                {
                    HasNetPaths = false;
                    Logger.SaveError("frmProducttreeDetail.cmdOperationBuiltGraphicView_Click", objEx.Message);
                    MessageBox.Show("نمایش گرافیکی اجزاء درخت با مل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                    if (mdsTreeDetail.Tables.Count > 2)
                    {
                        {
                            var withBlock = mdsTreeDetail;
                            withBlock.RejectChanges();
                            for (I = (short)(withBlock.Tables.Count - 1); I >= 2; I += -1)
                            {
                                withBlock.Tables[I].Constraints.Clear();
                                withBlock.Tables[I].Dispose();
                                withBlock.Tables.RemoveAt(I);
                            }
                        }
                    }
                }
            }

            if (HasNetPaths)
            {
                {
                    var withBlock = new frmOperationBuiltGraphicView();
                    withBlock.MdiParent = MdiParent;
                    withBlock.TreeCode = Conversions.ToString(TreeView1.Tag);
                    withBlock.Show();
                }
            }
        }

        private void FormLoad()
        {
            try
            {
                LoadDataSetTables();
                CreateOperationDeleteCommands();
                BuildTree();
            }
            catch (Exception objEx)
            {
                MessageBox.Show("فراخوانی فرم با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.SaveError("frmProductTreeDetail.FormLoad", objEx.Message);
            }
        }

        private void LoadDataSetTables()
        {
            string QueryCondition = Conversions.ToString(Operators.ConcatenateObject(" Where TreeCode=", TreeView1.Tag));
            DataSetConfig.FillDataSet("Tbl_ProductTreeDetails", "Tbl_ProductTreeDetails", "Select * From Tbl_ProductTreeDetails" + QueryCondition, "TreeCode", "DetailCode");
            DataSetConfig.FillDataSet("Tbl_ProductOPCs", "Tbl_ProductOPCs", "Select * From Tbl_ProductOPCs" + QueryCondition, "TreeCode", "OperationCode");
        }

        private void SetOperationForeignKeys_And_Relations()
        {
            ForeignKeyConstraint fkColumn;
            var prnColumns = new DataColumn[2];
            var cldColumns = new DataColumn[2];
            prnColumns[0] = mdsTreeDetail.Tables["Tbl_ProductOPCs"].Columns["TreeCode"];
            prnColumns[1] = mdsTreeDetail.Tables["Tbl_ProductOPCs"].Columns["OperationCode"];
            cldColumns[0] = mdsTreeDetail.Tables["Tbl_ContractorOperations"].Columns["TreeCode"];
            cldColumns[1] = mdsTreeDetail.Tables["Tbl_ContractorOperations"].Columns["OperationCode"];
            // تنظیم محدودیت کلید خارجی برای جدول پیمانکاران عملیات و جدول برگه های عملیات
            fkColumn = new ForeignKeyConstraint("fkColumn__ProductOPCs_v_ContractorOperations", prnColumns, cldColumns);
            fkColumn.AcceptRejectRule = AcceptRejectRule.None;
            fkColumn.DeleteRule = Rule.None;
            fkColumn.UpdateRule = Rule.Cascade;
            mdsTreeDetail.Tables["Tbl_ContractorOperations"].Constraints.Add(fkColumn);
            // تنظیم رابطه بین جدول پیمانکاران عملیات و جدول برگه های عملیات بوسیلۀ ستون کد عملیات
            mdsTreeDetail.Relations.Add("ProductOPCs_ContractorOperations", prnColumns, cldColumns);
            prnColumns[0] = mdsTreeDetail.Tables["Tbl_ProductOPCs"].Columns["TreeCode"];
            prnColumns[1] = mdsTreeDetail.Tables["Tbl_ProductOPCs"].Columns["OperationCode"];
            cldColumns[0] = mdsTreeDetail.Tables["Tbl_ProductOPCsExecutorMachines"].Columns["TreeCode"];
            cldColumns[1] = mdsTreeDetail.Tables["Tbl_ProductOPCsExecutorMachines"].Columns["OperationCode"];
            // تنظیم محدودیت کلید خارجی برای جدول ماشین آلات انجام دهنده عملیات و جدول برگه های عملیات
            fkColumn = new ForeignKeyConstraint("fkColumn__ProductOPCs_v_ProductOPCsExecutorMachines", prnColumns, cldColumns);
            fkColumn.AcceptRejectRule = AcceptRejectRule.None;
            fkColumn.DeleteRule = Rule.None;
            fkColumn.UpdateRule = Rule.Cascade;
            mdsTreeDetail.Tables["Tbl_ProductOPCsExecutorMachines"].Constraints.Add(fkColumn);
            // تنظیم رابطه بین جدول جدول ماشین آلات انجام دهنده عملیات و جدول برگه های عملیات بوسیلۀ ستون کد عملیات
            mdsTreeDetail.Relations.Add("ProductOPCs_ProductOPCsExecutorMachines", prnColumns, cldColumns);
            prnColumns[0] = mdsTreeDetail.Tables["Tbl_Machines"].Columns["Code"];
            prnColumns[1] = null;
            cldColumns[0] = mdsTreeDetail.Tables["Tbl_ProductOPCsExecutorMachines"].Columns["MachineCode"];
            cldColumns[1] = null;
            // تنظیم محدودیت کلید خارجی برای جدول ماشین آلات و جدول ماشین آلات انجام دهنده عملیات
            fkColumn = new ForeignKeyConstraint("fkColumn__Machines_v_ProductOPCsExecutorMachines", prnColumns[0], cldColumns[0]);
            fkColumn.AcceptRejectRule = AcceptRejectRule.None;
            fkColumn.DeleteRule = Rule.None;
            fkColumn.UpdateRule = Rule.Cascade;
            mdsTreeDetail.Tables["Tbl_ProductOPCsExecutorMachines"].Constraints.Add(fkColumn);
            // تنظیم رابطه بین جدول جدول ماشین آلات و جدول ماشینهای انجام دهندۀ عملیات بوسیلۀ ستون کد ماشین
            mdsTreeDetail.Relations.Add("Machines_ProductOPCsExecutorMachines", prnColumns[0], cldColumns[0]);
        }

        private void LoadDetailOperationsToGrid()
        {
            AddedOperations = Constants.vbNullString;
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    // فراخوانی عملیات هایی که پیشنیاز آنها در جزء جاری وجود ندارد و در اجزاء دیگر درخت می باشد
                    var cmDetailOperations = new SqlCommand("Select OperationCode,OperationTitle,ActivityGroup " + "From   Tbl_ProductOPCs " + "Where  TreeCode = " + TreeView1.Tag.ToString() + " And DetailCode = '" + CurrentNode.Tag.ToString() + "' And " + "       OperationCode IN (Select CurrentOperationCode From Tbl_PreOperations Where TreeCode = " + TreeView1.Tag.ToString() + " And " + "       Not PreOperationCode IN (Select OperationCode From Tbl_ProductOPCs Where  TreeCode = " + TreeView1.Tag.ToString() + " And DetailCode = '" + CurrentNode.Tag.ToString() + "'))", cn);
                    var drDetailOperations = cmDetailOperations.ExecuteReader();
                    {
                        var withBlock = dgOperations;
                        withBlock.Rows.Clear();
                        while (drDetailOperations.Read())
                        {
                            if (AddedOperations is object)
                            {
                                if (!AddedOperations.Contains(Conversions.ToString(drDetailOperations["OperationCode"])))
                                {
                                    withBlock.Rows.Add(drDetailOperations["OperationCode"], drDetailOperations["OperationTitle"], drDetailOperations["ActivityGroup"]);
                                    AddedOperations = Conversions.ToString(AddedOperations + Interaction.IIf(string.IsNullOrEmpty(AddedOperations), Operators.ConcatenateObject(Operators.ConcatenateObject("'", drDetailOperations["OperationCode"]), "'"), Operators.ConcatenateObject(Operators.ConcatenateObject(",'", drDetailOperations["OperationCode"]), "'")));
                                    AddAfterardOperations(TreeView1.Tag.ToString(), CurrentNode.Tag.ToString(), Conversions.ToString(drDetailOperations["OperationCode"]));
                                }
                            }
                            else
                            {
                                withBlock.Rows.Add(drDetailOperations["OperationCode"], drDetailOperations["OperationTitle"], drDetailOperations["ActivityGroup"]);
                                AddedOperations = Conversions.ToString(AddedOperations + Interaction.IIf(string.IsNullOrEmpty(AddedOperations), Operators.ConcatenateObject(Operators.ConcatenateObject("'", drDetailOperations["OperationCode"]), "'"), Operators.ConcatenateObject(Operators.ConcatenateObject(",'", drDetailOperations["OperationCode"]), "'")));
                                AddAfterardOperations(TreeView1.Tag.ToString(), CurrentNode.Tag.ToString(), Conversions.ToString(drDetailOperations["OperationCode"]));
                            }
                        }
                    }

                    drDetailOperations.Close();
                    // فراخوانی لیست عملیات هایی که دارای پیش نیاز نیستند
                    cmDetailOperations.CommandText = "Select OperationCode,OperationTitle,ActivityGroup " + "From   Tbl_ProductOPCs " + "Where  TreeCode = " + TreeView1.Tag.ToString() + " And DetailCode = '" + CurrentNode.Tag.ToString() + "' And " + "       Not OperationCode IN (Select CurrentOperationCode From Tbl_PreOperations Where TreeCode = " + TreeView1.Tag.ToString() + ")";
                    // & IIf(AddedOperations = vbNullString, "", " Not OperationCode IN (" & AddedOperations & ") And ")

                    drDetailOperations = cmDetailOperations.ExecuteReader();
                    while (drDetailOperations.Read())
                    {
                        if (AddedOperations is object)
                        {
                            if (!AddedOperations.Contains(Conversions.ToString(drDetailOperations["OperationCode"])))
                            {
                                dgOperations.Rows.Add(drDetailOperations["OperationCode"], drDetailOperations["OperationTitle"], drDetailOperations["ActivityGroup"]);
                                AddedOperations = Conversions.ToString(AddedOperations + Interaction.IIf(string.IsNullOrEmpty(AddedOperations), Operators.ConcatenateObject(Operators.ConcatenateObject("'", drDetailOperations["OperationCode"]), "'"), Operators.ConcatenateObject(Operators.ConcatenateObject(",'", drDetailOperations["OperationCode"]), "'")));
                                AddAfterardOperations(TreeView1.Tag.ToString(), CurrentNode.Tag.ToString(), Conversions.ToString(drDetailOperations["OperationCode"]));
                            }
                        }
                        else
                        {
                            dgOperations.Rows.Add(drDetailOperations["OperationCode"], drDetailOperations["OperationTitle"], drDetailOperations["ActivityGroup"]);
                            AddedOperations = Conversions.ToString(AddedOperations + Interaction.IIf(string.IsNullOrEmpty(AddedOperations), Operators.ConcatenateObject(Operators.ConcatenateObject("'", drDetailOperations["OperationCode"]), "'"), Operators.ConcatenateObject(Operators.ConcatenateObject(",'", drDetailOperations["OperationCode"]), "'")));
                            AddAfterardOperations(TreeView1.Tag.ToString(), CurrentNode.Tag.ToString(), Conversions.ToString(drDetailOperations["OperationCode"]));
                        }
                    }

                    drDetailOperations.Close();
                    if (AddedOperations is object && !AddedOperations.Trim().Equals(""))
                    {
                        // فراخوانی لیست عملیات هایی که در فیلترهای بالا نیامده است
                        cmDetailOperations.CommandText = "Select OperationCode,OperationTitle,ActivityGroup " + "From   Tbl_ProductOPCs " + "Where  TreeCode = " + TreeView1.Tag.ToString() + " And DetailCode = '" + CurrentNode.Tag.ToString() + "' And " + "       Not OperationCode IN (" + AddedOperations + ")";
                        drDetailOperations = cmDetailOperations.ExecuteReader();
                        while (drDetailOperations.Read())
                        {
                            if (AddedOperations is object)
                            {
                                if (!AddedOperations.Contains(Conversions.ToString(drDetailOperations["OperationCode"])))
                                {
                                    dgOperations.Rows.Add(drDetailOperations["OperationCode"], drDetailOperations["OperationTitle"], drDetailOperations["ActivityGroup"]);
                                    AddedOperations = Conversions.ToString(AddedOperations + Interaction.IIf(string.IsNullOrEmpty(AddedOperations), Operators.ConcatenateObject(Operators.ConcatenateObject("'", drDetailOperations["OperationCode"]), "'"), Operators.ConcatenateObject(Operators.ConcatenateObject(",'", drDetailOperations["OperationCode"]), "'")));
                                    AddAfterardOperations(TreeView1.Tag.ToString(), CurrentNode.Tag.ToString(), Conversions.ToString(drDetailOperations["OperationCode"]));
                                }
                            }
                            else
                            {
                                dgOperations.Rows.Add(drDetailOperations["OperationCode"], drDetailOperations["OperationTitle"], drDetailOperations["ActivityGroup"]);
                                AddedOperations = Conversions.ToString(AddedOperations + Interaction.IIf(string.IsNullOrEmpty(AddedOperations), Operators.ConcatenateObject(Operators.ConcatenateObject("'", drDetailOperations["OperationCode"]), "'"), Operators.ConcatenateObject(Operators.ConcatenateObject(",'", drDetailOperations["OperationCode"]), "'")));
                                AddAfterardOperations(TreeView1.Tag.ToString(), CurrentNode.Tag.ToString(), Conversions.ToString(drDetailOperations["OperationCode"]));
                            }
                        }

                        drDetailOperations.Close();
                    }
                }
                catch (Exception objEx)
                {
                    Logger.SaveError("frmProductTreeDetail.LoadDetailOperationsToGrid", objEx.Message);
                    MessageBox.Show("فراخوانی لیست عملیاتها با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }

            TabControl1.SelectTab(TabPage1);
        }

        private void AddAfterardOperations(string TreeCode, string DetailCode, string PreOperationCode)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmHasAfterward = new SqlCommand("Select A.CurrentOperationCode As OperationCode,B.OperationTitle,B.ActivityGroup From Tbl_PreOperations A Inner Join Tbl_ProductOPCs B ON A.TreeCode = B.TreeCode And A.CurrentOperationCode = B.OperationCode Where A.TreeCode = " + TreeCode + " And A.PreOperationCode = '" + PreOperationCode + "' And A.CurrentOperationCode IN (Select OperationCode From Tbl_ProductOPCs Where TreeCode = " + TreeCode + " And DetailCode = '" + DetailCode + "')", cn);
                var drHasAfterward = cmHasAfterward.ExecuteReader();
                if (drHasAfterward.Read())
                {
                    if (!AddedOperations.Contains(Conversions.ToString(drHasAfterward["OperationCode"])))
                    {
                        dgOperations.Rows.Add(drHasAfterward["OperationCode"], drHasAfterward["OperationTitle"], drHasAfterward["ActivityGroup"]);
                        AddedOperations = Conversions.ToString(AddedOperations + Interaction.IIf(string.IsNullOrEmpty(AddedOperations), Operators.ConcatenateObject(Operators.ConcatenateObject("'", drHasAfterward["OperationCode"]), "'"), Operators.ConcatenateObject(Operators.ConcatenateObject(",'", drHasAfterward["OperationCode"]), "'")));
                        AddAfterardOperations(TreeCode, DetailCode, Conversions.ToString(drHasAfterward["OperationCode"]));
                    }
                }

                cn.Close();
            }
        }

        private void SetMiniGridsSource()
        {
            dgPreItems.Columns.Clear();
            dgPreOperations.Columns.Clear();
            dgExecutorMachines.Columns.Clear();
            if (OpCurrentRow is object)
            {
                // If cnProductionPlanning.State = ConnectionState.Closed Then
                var daDetails = new SqlDataAdapter(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select MaterialCode From Tbl_OperationMaterials Where TreeCode=", OpCurrentRow["TreeCode"]), " And CurrentOperationCode='"), OpCurrentRow["OperationCode"]), "'")), Module1.cnProductionPlanning);
                var dtGridSource = new DataTable();
                daDetails.Fill(dtGridSource);
                {
                    var withBlock = dgPreItems;
                    withBlock.Columns.Clear();
                    withBlock.DataSource = null;
                    withBlock.DataSource = dtGridSource;
                    withBlock.Columns[0].HeaderText = "مواد/قطعه وارده";
                    withBlock.Columns[0].Width = 192;
                }

                daDetails = new SqlDataAdapter(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select A.PreOperationCode, B.TypeTitle AS RelationType, A.LagTime, C.TypeTitle AS TimeType " + "From   dbo.Tbl_PreOperations A INNER JOIN " + "       dbo.Tbl_RelationTypes B ON A.RelationType = B.TypeCode INNER JOIN " + "       dbo.Tbl_TimeTypes C ON A.TimeType = C.TypeCode " + "Where  A.TreeCode=", OpCurrentRow["TreeCode"]), " And A.CurrentOperationCode='"), OpCurrentRow["OperationCode"]), "'")), Module1.cnProductionPlanning);
                dtGridSource = new DataTable();
                daDetails.Fill(dtGridSource);
                {
                    var withBlock1 = dgPreOperations;
                    withBlock1.Columns.Clear();
                    withBlock1.DataSource = null;
                    withBlock1.DataSource = dtGridSource;
                    withBlock1.Columns[0].HeaderText = "عملیات پیشنیاز";
                    withBlock1.Columns[0].Width = 140;
                    withBlock1.Columns[1].HeaderText = "رابطه";
                    withBlock1.Columns[1].Width = 42;
                    withBlock1.Columns[2].HeaderText = "زمان";
                    withBlock1.Columns[2].Width = 42;
                    withBlock1.Columns[3].HeaderText = "نوع زمان";
                    withBlock1.Columns[3].Width = 65;
                }

                daDetails = new SqlDataAdapter(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select MachineCode,MachinePriority From Tbl_ProductOPCsExecutorMachines Where TreeCode=", OpCurrentRow["TreeCode"]), " And OperationCode='"), OpCurrentRow["OperationCode"]), "' And MachineCode<>'-1' Order By MachinePriority")), Module1.cnProductionPlanning);
                dtGridSource = new DataTable();
                daDetails.Fill(dtGridSource);
                {
                    var withBlock2 = dgExecutorMachines;
                    withBlock2.Columns.Clear();
                    withBlock2.DataSource = null;
                    withBlock2.DataSource = dtGridSource;
                    withBlock2.Columns[0].HeaderText = "ماشین انجام دهنده";
                    withBlock2.Columns[0].Width = 175;
                    withBlock2.Columns[1].HeaderText = "اولویت";
                    withBlock2.Columns[1].Width = 57;
                }
                // End If
            }
        }

        private void BuildTree()
        {
            var dvRoot = mdsTreeDetail.Tables["Tbl_ProductTreeDetails"].DefaultView;
            dvRoot.RowFilter = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("TreeCode=", TreeView1.Tag), " And LevelNo=0"));
            try
            {
                TreeView1.Nodes.Clear();
                ADDNewTreeNode(null, new TreeNode(Conversions.ToString(dvRoot[0]["DetailName"])), Conversions.ToString(dvRoot[0]["DetailCode"]));
                TreeView1.ExpandAll();
                if (TreeView1.Nodes.Count > 0)
                {
                    TreeView1.SelectedNode = TreeView1.Nodes[0];
                }
            }
            catch (Exception objEx)
            {
                Logger.SaveError("frmProductTreeDetail.BuildTree", objEx.Message);
                MessageBox.Show("رسم درخت محصول با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }

        private DataView GetCurrentRowChilds(string DetailCode)
        {
            var dvChilds = mdsTreeDetail.Tables["Tbl_ProductTreeDetails"].DefaultView;
            dvChilds.RowFilter = "ParentDetailCode='" + DetailCode + "'";
            return dvChilds;
        }

        private void ADDNewTreeNode(TreeNode ParentNode, TreeNode NewNode, string NewDetailCode)
        {
            short I;
            string RowFilter;
           
            NewNode.Tag = NewDetailCode;
            if (ParentNode is null)
            {
                TreeView1.Nodes.Add(NewNode);
                ParentNode = NewNode;
            }
            else
            {
                ParentNode.Nodes.Add(NewNode);
            }
            //ParentNode.Nodes.Add(NewNode);
            var dvChilds = GetCurrentRowChilds(NewDetailCode);
            if (dvChilds.Count == 0)
            {
                return;
            }

            var loopTo = (short)(dvChilds.Count - 1);
            for (I = 0; I <= loopTo; I++)
            {
                RowFilter = dvChilds.RowFilter;
                ADDNewTreeNode(NewNode, new TreeNode(Conversions.ToString(dvChilds[I]["DetailName"])), Conversions.ToString(dvChilds[I]["DetailCode"]));
                dvChilds.RowFilter = RowFilter;
            }
        }

        private bool OperationNetwork_Paths_Build()
        {
            SqlTransaction trnOperationNetworkPaths;
            var daProductTreeDetails = new SqlDataAdapter();
            var cmOperationNetworkPaths = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject("Delete From Tbl_OperationNetworkPaths Where TreeCode=", TreeView1.Tag)), Module1.cnProductionPlanning);
            var dtOperationNetworkPaths = new DataTable();
            int I;
            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                Module1.cnProductionPlanning.Open();
            trnOperationNetworkPaths = Module1.cnProductionPlanning.BeginTransaction();
            cmOperationNetworkPaths.Transaction = trnOperationNetworkPaths;
            try
            {
                // حذف مسیرهای ثبت شدۀ قبلی
                cmOperationNetworkPaths.ExecuteNonQuery();
                // بدست آوردن عملیاتهایی که پیشنیازی ندارند
                daProductTreeDetails.SelectCommand = new SqlCommand("Select * From Tbl_ProductOPCs Where TreeCode=" + TreeView1.Tag.ToString() + " And Not OperationCode IN (Select CurrentOperationCode From Tbl_PreOperations Where TreeCode=" + TreeView1.Tag.ToString() + ")", Module1.cnProductionPlanning);
                daProductTreeDetails.SelectCommand.Transaction = trnOperationNetworkPaths;
                daProductTreeDetails.Fill(dtOperationNetworkPaths);
                var loopTo = dtOperationNetworkPaths.Rows.Count - 1;
                for (I = 0; I <= loopTo; I++)
                {
                    if (!AddPathElement((I + 1).ToString(), 1, Conversions.ToString(dtOperationNetworkPaths.Rows[I]["OperationCode"]), GetExecutionTime(Conversions.ToString(dtOperationNetworkPaths.Rows[I]["OperationCode"]), Conversions.ToShort(dtOperationNetworkPaths.Rows[I]["ExecutionMethod"])), ref trnOperationNetworkPaths))
                    {
                        trnOperationNetworkPaths.Rollback();
                        return false;
                    }
                }

                trnOperationNetworkPaths.Commit();
            }
            catch (Exception objEx)
            {
                trnOperationNetworkPaths.Rollback();
                Logger.SaveError("OperationNetwork_Paths_Build", objEx.Message);
                MessageBox.Show("در ثبت مسیرهای شبکه عملیات اشکال وجود دارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                cmOperationNetworkPaths.Dispose();
                dtOperationNetworkPaths.Dispose();
                trnOperationNetworkPaths.Dispose();
                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                    Module1.cnProductionPlanning.Close();
            }

            return true;
        }

        private bool AddPathElement(string PathNo, int Priority, string OperationCode, string ExecutionTime, ref SqlTransaction trnOperationNetworkPaths)
        {
            // Dim cmOperationNetworkPaths As New SqlCommand("Insert Into Tbl_OperationNetworkPaths(TreeCode,PathCode,ItemPriority,OperationCode,ExecutionTime) " & _
            // "Values(" & TreeView1.Tag & "," & PathNo & "," & Priority & ",'" & OperationCode & "'," & ExecutionTime & ")", cnProductionPlanning)
            // Dim dtOperationNetworkPaths As New DataTable
            // Dim I As Integer

            // cmOperationNetworkPaths.Transaction = trnOperationNetworkPaths

            // 'ثبت عنصر جدید مسیر
            // cmOperationNetworkPaths.ExecuteNonQuery()
            // 'بدست آوردن عملیاتهایی که عملیات ثبت شده پیشنیاز آنها می باشد
            // dtOperationNetworkPaths = GetNextOperations(OperationCode, trnOperationNetworkPaths)

            // For I = 0 To dtOperationNetworkPaths.Rows.Count - 1
            // Priority += 1
            // AddPathElement(PathNo, Priority, dtOperationNetworkPaths.Rows(I)("OperationCode"), GetExecutionTime(dtOperationNetworkPaths.Rows(I)("OperationCode"), dtOperationNetworkPaths.Rows(I)("ExecutionMethod")), trnOperationNetworkPaths)
            // Next I

            // dtOperationNetworkPaths.Dispose()
            var cmOperationNetworkPaths = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select Count(*) From Tbl_OperationNetworkPaths Where TreeCode=", TreeView1.Tag), " And OperationCode='"), OperationCode), "' And PathCode="), PathNo)), Module1.cnProductionPlanning);
            cmOperationNetworkPaths.Transaction = trnOperationNetworkPaths;
            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(cmOperationNetworkPaths.ExecuteScalar(), 0, false)))
            {
                MessageBox.Show(" پیشنیاز اشتباه انتخاب شده و در یک مسیر ایجاد حلقه کرده است " + OperationCode + " :برای عملیات ", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                return false;
            }
            else
            {
                cmOperationNetworkPaths.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Insert Into Tbl_OperationNetworkPaths(TreeCode,PathCode,ItemPriority,OperationCode,ExecutionTime) " + "Values(", TreeView1.Tag), ","), PathNo), ","), Priority), ",'"), OperationCode), "',"), ExecutionTime), ")"));

                // ثبت عنصر جدید مسیر
                cmOperationNetworkPaths.ExecuteNonQuery();
            }

            var dtOperationNetworkPaths = new DataTable();
            int I;

            // بدست آوردن عملیاتهایی که عملیات ثبت شده پیشنیاز آنها می باشد
            dtOperationNetworkPaths = GetNextOperations(OperationCode, ref trnOperationNetworkPaths);
            var loopTo = dtOperationNetworkPaths.Rows.Count - 1;
            for (I = 0; I <= loopTo; I++)
            {
                Priority += 1;
                if (!AddPathElement(PathNo, Priority, Conversions.ToString(dtOperationNetworkPaths.Rows[I]["OperationCode"]), GetExecutionTime(Conversions.ToString(dtOperationNetworkPaths.Rows[I]["OperationCode"]), Conversions.ToShort(dtOperationNetworkPaths.Rows[I]["ExecutionMethod"])), ref trnOperationNetworkPaths))
                {
                    return false;
                }
            }

            dtOperationNetworkPaths.Dispose();
            return true;
        }

        private string GetExecutionTime(string OperationCode, short ExecutionMethod)
        {
            DataRow[] drExecutionTime = null;
            switch ((EnumExecutionMethod)ExecutionMethod)
            {
                case EnumExecutionMethod.EM_MACHINE:
                    {
                        drExecutionTime = mdsTreeDetail.Tables["Tbl_ProductOPCsExecutorMachines"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("MachineCode<>'-1' And TreeCode=", TreeView1.Tag), " And OperationCode='"), OperationCode), "'")));
                        if (drExecutionTime.Length > 0)
                        {
                            var drMachine = mdsTreeDetail.Tables["Tbl_Machines"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Code='", drExecutionTime[0]["MachineCode"]), "'")));
                            return Module1.GetAnyTime_TO_Hour(Conversions.ToShort(drExecutionTime[0]["TimeType"]), Conversions.ToDouble(drExecutionTime[0]["OneExecutionTime"]), Module1.GetCalendarAccessibleTime(Conversions.ToString(drMachine[0]["CalendarCode"]))).ToString();
                        }
                        else
                        {
                            return (-1).ToString();
                        }

                        //break;
                    }

                case EnumExecutionMethod.EM_NONMACHINE:
                    {
                        drExecutionTime = mdsTreeDetail.Tables["Tbl_ProductOPCsExecutorMachines"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("MachineCode='-1' And TreeCode=", TreeView1.Tag), " And OperationCode='"), OperationCode), "'")));
                        if (drExecutionTime.Length > 0)
                        {
                            return Module1.GetAnyTime_TO_Hour(Conversions.ToShort(drExecutionTime[0]["TimeType"]), Conversions.ToDouble(drExecutionTime[0]["OneExecutionTime"]), Module1.GetCalendarAccessibleTime(Conversions.ToString(drExecutionTime[0]["CalendarCode"]))).ToString();
                        }
                        else
                        {
                            return (-1).ToString();
                        }

                        //break;
                    }

                case EnumExecutionMethod.EM_CONTRACTOR:
                    {
                        return 0.ToString();
                    }
            }

            return (-1).ToString();
        }

        private DataTable GetNextOperations(string OperationCode, ref SqlTransaction trnOperationNetworkPaths)
        {
            var daProductTreeDetails = new SqlDataAdapter();
            var dtOperationNetworkPaths = new DataTable();
            daProductTreeDetails.SelectCommand = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select * From Tbl_ProductOPCs Where TreeCode=", TreeView1.Tag), " And OperationCode IN (Select CurrentOperationCode From Tbl_PreOperations Where PreOperationCode='"), OperationCode), "' And TreeCode="), TreeView1.Tag), ")")), Module1.cnProductionPlanning);
            daProductTreeDetails.SelectCommand.Transaction = trnOperationNetworkPaths;
            daProductTreeDetails.Fill(dtOperationNetworkPaths);
            return dtOperationNetworkPaths;
        }

        private void CreateOperationDeleteCommands()
        {
            // ایجاد دستور حذف رکورد جاری در جدول عملياتها
            daProductOPCs.DeleteCommand = new SqlCommand("Delete From Tbl_ProductOPCs Where TreeCode=@TreeCode And OperationCode=@OperationCode", Module1.cnProductionPlanning);
            {
                var withBlock = daProductOPCs.DeleteCommand;
                withBlock.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode").SourceVersion = DataRowVersion.Original;
                withBlock.Parameters.Add("@OperationCode", SqlDbType.VarChar, 50, "OperationCode").SourceVersion = DataRowVersion.Original;
            }

            // ایجاد دستور ويرايش رکورد جاری در جدول عملياتهاي پيشنياز
            daPreOperations.UpdateCommand = new SqlCommand("Update Tbl_PreOperations Set CurrentOperationCode=@CurrentOperationCode Where TreeCode=@TreeCode And CurrentOperationCode=@OldCurrentOperationCode And PreOperationCode=@PreOperationCode", Module1.cnProductionPlanning);
            {
                var withBlock1 = daPreOperations.UpdateCommand;
                withBlock1.Parameters.Add("@CurrentOperationCode", SqlDbType.VarChar, 50, "CurrentOperationCode").SourceVersion = DataRowVersion.Current;
                withBlock1.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode").SourceVersion = DataRowVersion.Original;
                withBlock1.Parameters.Add("@OldCurrentOperationCode", SqlDbType.VarChar, 50, "CurrentOperationCode").SourceVersion = DataRowVersion.Original;
                withBlock1.Parameters.Add("@PreOperationCode", SqlDbType.VarChar, 50, "PreOperationCode").SourceVersion = DataRowVersion.Original;
            }
            // ایجاد دستور حذف رکورد جاری در جدول عملياتهاي پيشنياز
            daPreOperations.DeleteCommand = new SqlCommand("Delete From Tbl_PreOperations Where TreeCode=@TreeCode And CurrentOperationCode=@CurrentOperationCode And PreOperationCode=@PreOperationCode", Module1.cnProductionPlanning);
            {
                var withBlock2 = daPreOperations.DeleteCommand;
                withBlock2.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode");
                withBlock2.Parameters[0].Direction = ParameterDirection.Input;
                withBlock2.Parameters[0].SourceVersion = DataRowVersion.Original;
                withBlock2.Parameters.Add("@CurrentOperationCode", SqlDbType.VarChar, 50, "CurrentOperationCode");
                withBlock2.Parameters[1].Direction = ParameterDirection.Input;
                withBlock2.Parameters[1].SourceVersion = DataRowVersion.Original;
                withBlock2.Parameters.Add("@PreOperationCode", SqlDbType.VarChar, 50, "PreOperationCode");
                withBlock2.Parameters[2].Direction = ParameterDirection.Input;
                withBlock2.Parameters[2].SourceVersion = DataRowVersion.Original;
            }

            // ایجاد دستور حذف رکورد جاری در جدول ماشينهاي انجام دهنده عمليات
            daNonExecutorMachines.DeleteCommand = new SqlCommand("Delete From Tbl_ProductOPCsExecutorMachines Where TreeCode=@TreeCode And OperationCode=@OperationCode And MachineCode=@MachineCode And MachinePriority=@MachinePriority", Module1.cnProductionPlanning);
            {
                var withBlock3 = daNonExecutorMachines.DeleteCommand;
                withBlock3.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode").SourceVersion = DataRowVersion.Original;
                withBlock3.Parameters.Add("@OperationCode", SqlDbType.VarChar, 50, "OperationCode").SourceVersion = DataRowVersion.Original;
                withBlock3.Parameters.Add("@MachineCode", SqlDbType.VarChar, 50, "MachineCode").SourceVersion = DataRowVersion.Original;
                withBlock3.Parameters.Add("@MachinePriority", SqlDbType.TinyInt, default, "MachinePriority").SourceVersion = DataRowVersion.Original;
            }

            // ایجاد دستور حذف رکورد جاری در جدول پيمانكاران عمليات
            daContractorOperation.DeleteCommand = new SqlCommand("Delete From Tbl_ContractorOperations Where TreeCode=@TreeCode And OperationCode=@OperationCode And ContractorCode=@ContractorCode", Module1.cnProductionPlanning);
            {
                var withBlock4 = daContractorOperation.DeleteCommand;
                withBlock4.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode").SourceVersion = DataRowVersion.Original;
                withBlock4.Parameters.Add("@OperationCode", SqlDbType.VarChar, 50, "OperationCode").SourceVersion = DataRowVersion.Original;
                withBlock4.Parameters.Add("@ContractorCode", SqlDbType.VarChar, 50, "ContractorCode").SourceVersion = DataRowVersion.Original;
            }

            // ایجاد دستور حذف رکورد جاری در جدول پيمانكاران عمليات
            daOperationMaterial.DeleteCommand = new SqlCommand("Delete From Tbl_OperationMaterials Where TreeCode=@TreeCode And CurrentOperationCode=@OperationCode And MaterialCode=@MaterialCode", Module1.cnProductionPlanning);
            {
                var withBlock5 = daOperationMaterial.DeleteCommand;
                withBlock5.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode").SourceVersion = DataRowVersion.Original;
                withBlock5.Parameters.Add("@OperationCode", SqlDbType.VarChar, 50, "CurrentOperationCode").SourceVersion = DataRowVersion.Original;
                withBlock5.Parameters.Add("@MaterialCode", SqlDbType.VarChar, 50, "MaterialCode").SourceVersion = DataRowVersion.Original;
            }
        }

        private void mnuPreItems_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (dgOperations.SelectedRows.Count == 0)
            {
                e.Cancel = true;
            }
        }
    }
}