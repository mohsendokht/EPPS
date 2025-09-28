using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TreeView = System.Windows.Forms.TreeView;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using static ProductionPlanning.MyEnums;

namespace ProductionPlanning
{
    public partial class frmBatchsStatus
    {
        public frmBatchsStatus()
        {
            InitializeComponent();
            _cmdCalcProductionQuantity.Name = "cmdCalcProductionQuantity";
            _rbProductionQuantity.Name = "rbProductionQuantity";
            _dgOperations.Name = "dgOperations";
            _cmdBatchProgressCalc.Name = "cmdBatchProgressCalc";
            _cmdExit.Name = "cmdExit";
            _TreeView1.Name = "TreeView1";
        }

        //private int mSearchMode = -1;
        private DataSetConfiguration DataSetConfig = new DataSetConfiguration();
        private DataRow[] FoundRows = null;
        private IEnumerator FoundRowsEnumerator;
        private string AddedOperations = Constants.vbNullString;

        public DataSet dsProductionPlanning
        {
            get
            {
                return DataSetConfig.dsProductionPlanning;
            }
        }

        private void frmBatchsStatus_Load(object sender, EventArgs e)
        {
            string SelectStr = Constants.vbNullString;
            SelectStr = "Select A.BatchCode, B.ProductCode, C.ProductName, A.Productionquantity, A.ProductTreeCode As TreeCode," + "       dbo.GetBatchStatus(A.BatchCode) AS BatchState, A.RealProductionQuantity " + "From   dbo.Tbl_ProductionBatchs A INNER JOIN " + "       dbo.Tbl_ProductTree B ON A.ProductTreeCode=B.TreeCode INNER JOIN " + "       dbo.Tbl_Products C ON B.ProductCode = C.ProductCode";
            DataSetConfig.FillDataSet("Tbl_ProductionBatchs", "Tbl_ProductionBatchs", SelectStr, "BatchCode");
            {
                var withBlock = cbBatchs;
                withBlock.DataSource = dsProductionPlanning.Tables["Tbl_ProductionBatchs"];
                withBlock.DisplayMember = "BatchCode";
                withBlock.ValueMember = "BatchCode";
                withBlock.SelectedIndex = -1;
            }

            dgOperations.Tag = -1;
            txtCalcDate.Text = Module1.mServerShamsiDate;
        }

        private void frmBatchsStatus_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    var cmExistsTable = new SqlCommand("If exists (Select * From dbo.sysobjects Where id = object_id(N'dbo.Tbl_TempBatchOverStocks') and OBJECTPROPERTY(id, N'IsUserTable') = 1) " + " drop table dbo.Tbl_TempBatchOverStocks ", cn);
                    cmExistsTable.ExecuteNonQuery();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    Logger.LogException("frmBatchsStatus_FormClosing", ex);
                }
            }

            dgOperations.DataSource = null;
            DataSetConfig = null;
            FoundRows = null;
            FoundRowsEnumerator = null;
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtSearchs_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox CurrentTextBox = (TextBox)sender;
            if (Strings.Asc(e.KeyChar) == (int)Keys.Back)
            {
                if (string.IsNullOrEmpty(CurrentTextBox.Text))
                {
                    string PreControlName = "txtSearch";
                    PreControlName = Conversions.ToString(Interaction.IIf(Conversions.ToInteger(Strings.Right(CurrentTextBox.Name, 1)) < 8, PreControlName + (Conversions.ToInteger(Strings.Right(CurrentTextBox.Name, 1)) + 1).ToString(), Constants.vbNullString));
                    if (!string.IsNullOrEmpty(PreControlName))
                    {
                        if (Controls["Panel1"].Controls[PreControlName].Visible)
                        {
                            Controls["Panel1"].Controls[PreControlName].Focus();
                        }
                    }
                }
            }
            else if (Strings.Asc(e.KeyChar) == (int)Keys.Enter)
            {
                string Control;
                TextBox CurrentVisibleTextBox;
                string FilterValue = Constants.vbNullString;
                for (short TextBoxCounter = 1; TextBoxCounter <= 8; TextBoxCounter++)
                {
                    Control = "txtSearch" + TextBoxCounter;
                    if (Controls["Panel1"].Controls[Control].Visible)
                    {
                        CurrentVisibleTextBox = (TextBox)Controls["Panel1"].Controls[Control];
                        if (!string.IsNullOrEmpty(CurrentVisibleTextBox.Text))
                        {
                            FilterValue = Conversions.ToString(Interaction.IIf(string.IsNullOrEmpty(FilterValue), Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Convert(", Controls["Panel1"].Controls[Control].Tag), ",System.String) LIKE '%"), Controls["Panel1"].Controls[Control].Text), "%'"), Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(FilterValue + " And Convert(", Controls["Panel1"].Controls[Control].Tag), ",System.String) LIKE '%"), Controls["Panel1"].Controls[Control].Text), "%'")));
                        }
                    }
                }

                CurrentVisibleTextBox = null;
                string TableName;
                if (string.IsNullOrEmpty(FilterValue))
                {
                    FoundRows = null;
                }
                else if (FoundRows is null)
                {
                    if (dgOperations.DataSource is DataView)
                    {
                        DataView GridDataView = (DataView)dgOperations.DataSource;
                        TableName = GridDataView.Table.TableName;
                        SetGridColumns(dsProductionPlanning, TableName);
                    }
                    else
                    {
                        TableName = dgOperations.DataMember;
                    }

                    FoundRows = dsProductionPlanning.Tables[TableName].Select(FilterValue);
                    FoundRowsEnumerator = FoundRows.GetEnumerator();
                    if (FoundRowsEnumerator.MoveNext())
                    {
                        int I = dsProductionPlanning.Tables[TableName].Rows.IndexOf((DataRow)FoundRowsEnumerator.Current);
                        BindingContext[dsProductionPlanning, TableName].Position = I;
                    }
                    else
                    {
                        MessageBox.Show("موردی یافت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                        FoundRows = null;
                    }
                }
                else
                {
                    TableName = dgOperations.DataMember;
                    if (FoundRowsEnumerator.MoveNext())
                    {
                        int I = dsProductionPlanning.Tables[TableName].Rows.IndexOf((DataRow)FoundRowsEnumerator.Current);
                        BindingContext[dsProductionPlanning, TableName].Position = I;
                    }
                    else
                    {
                        MessageBox.Show("مورد دیگری وجود ندارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                        FoundRows = null;
                    }
                }
            }

            CurrentTextBox = null;
        }

        private void tmrBatchProductionOperations_Tick(object sender, EventArgs e)
        {
            tmrBatchProductionOperations.Enabled = false;
            LoadBatchProductionOperations(Conversions.ToString(cbBatchs.SelectedValue), Conversions.ToString(TreeView1.Tag));
        }

        private void cmdBatchProgressCalc_Click(object sender, EventArgs e)
        {
            if (cbBatchs.SelectedValue is object && cbBatchs.SelectedIndex > -1)
            {
                dgOperations.Rows.Clear();
                dgProductionQuantityList.Rows.Clear();
                var drBatchInfo = dsProductionPlanning.Tables["Tbl_ProductionBatchs"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("BatchCode='", cbBatchs.SelectedValue), "'")));
                if (drBatchInfo[0]["BatchState"].ToString().Equals("بسته شده"))
                {
                    tmrLoadOvers.Enabled = true;
                }
                else
                {
                    tmrLoadOvers.Enabled = false;
                    if (TabControl1.TabPages.Count > 3 && TabControl1.TabPages[3].Name.Equals("tpOverProduction"))
                    {
                        TabControl1.TabPages["tpOverProduction"].Dispose();
                    }
                }

                BuildTree(Conversions.ToString(cbBatchs.SelectedValue));
                tmrBatchProductionOperations.Enabled = true;
                lblBatchQuantity.Text = Conversions.ToString(drBatchInfo[0]["Productionquantity"]);
                lblDoneQuantity.Text = Conversions.ToString(drBatchInfo[0]["RealProductionQuantity"]);
                lblPlanningQuantity.Text = GetBatchPlanningQuantity(Conversions.ToString(drBatchInfo[0]["BatchCode"]), Conversions.ToString(drBatchInfo[0]["TreeCode"])).ToString();
                string RealEnd = Constants.vbNullString;
                string PlanningEnd = Constants.vbNullString;
                Get_Batch_Production_Planning_EndDate(Conversions.ToString(drBatchInfo[0]["BatchCode"]), ref RealEnd, ref PlanningEnd);
                lblProductionStartDate.Text = GetBatchProductionStartDate(Conversions.ToString(drBatchInfo[0]["BatchCode"]), Conversions.ToString(drBatchInfo[0]["TreeCode"]));
                lblProductionEndDate.Text = RealEnd;
                lblPlaningStartDate.Text = GetBatchPlaningStartDate(Conversions.ToString(drBatchInfo[0]["BatchCode"]), Conversions.ToString(drBatchInfo[0]["TreeCode"]));
                lblPlaningEndDate.Text = PlanningEnd;
                lblBatchProduction.Text = GetBatchProduction(Conversions.ToString(drBatchInfo[0]["BatchCode"]));
                if (Information.IsNumeric(lblPlanningQuantity.Text) && Information.IsNumeric(lblBatchProduction.Text))
                {
                    if (Conversions.ToLong(lblPlanningQuantity.Text) - Conversions.ToLong(lblBatchProduction.Text) > 0L)
                    {
                        lblBatchUnProduction.Text = (Conversions.ToLong(lblBatchQuantity.Text) - Conversions.ToLong(lblBatchProduction.Text)).ToString();
                    }
                    else
                    {
                        lblBatchUnProduction.Text = "0";
                    }
                }
                else
                {
                    lblBatchUnProduction.Text = "0";
                }

                int DetourPercent = 0;
                string argDetourHour = lblBatchDetourHour.Text;
                GetBatchDetour(Conversions.ToString(drBatchInfo[0]["BatchCode"]), Conversions.ToString(drBatchInfo[0]["TreeCode"]), txtCalcDate.Text, ref DetourPercent, ref argDetourHour);
                lblBatchDetourHour.Text = argDetourHour;
                lblBatchDetourPercent.Text = DetourPercent.ToString();
                lblBatchState.Text = Conversions.ToString(drBatchInfo[0]["BatchState"]);
                // lblBatchProductionProgress.Text = drBatchInfo(0)("ProductionProgressMeasure")

                if (txtCalcDate.Text.Length == 8)
                {
                    GroupBox1.Text = "انحراف از برنامۀ تولید بچ تا: " + txtCalcDate.Text.Substring(0, 4) + "/" + txtCalcDate.Text.Substring(4, 2) + "/" + txtCalcDate.Text.Substring(6, 2);
                }
                else
                {
                    GroupBox1.Text = "انحراف از برنامۀ تولید بچ تا: ";
                }

                LoadBatchUnProductionOperations(Conversions.ToString(cbBatchs.SelectedValue), Conversions.ToString(TreeView1.Tag));
                txtProductionQuantity.Value = GetMinTransferProductionQtyOnFirstSubbatch(Conversions.ToString(drBatchInfo[0]["BatchCode"]));
            }
        }

        private void SetGridColumns(object DataSource, string Table = Constants.vbNullString)
        {
            {
                var withBlock = dgOperations;
                if (DataSource is DataView)
                {
                    withBlock.DataSource = DataSource;
                }
                else
                {
                    withBlock.DataSource = DataSource;
                    withBlock.DataMember = Table;
                }

                withBlock.Columns[0].HeaderText = "سریال بچ تولید";
                withBlock.Columns[0].Width = 150;
                withBlock.Columns[1].HeaderText = "کد محصول";
                withBlock.Columns[1].Width = 150;
                withBlock.Columns[2].HeaderText = "نام محصول";
                withBlock.Columns[2].Width = 250;
                withBlock.Columns[3].HeaderText = "تعداد";
                withBlock.Columns[3].Width = 100;
                withBlock.Columns[4].HeaderText = "میزان پیشرفت تولید";
                withBlock.Columns[4].Width = 170;
            }
        }

        private void dgList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true & e.KeyCode == Keys.F12)
            {
                Module1.ExportGridToExcel((DataGridView)sender);
            }
        }

        private string GetBatchProduction(string BatchCode)
        {
            long BatchProduction = 0L;
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmBatchProduction = new SqlCommand("Select Sum(dbo.GetSubbatchProductionQuantity(A.SubbatchCode,B.ProductTreeCode)) From Tbl_ProductionSubbatchs A Inner Join Tbl_ProductionBatchs B ON A.BatchCode = B.BatchCode Where B.BatchCode = '" + BatchCode + "'", cn);
                BatchProduction = Conversions.ToLong(cmBatchProduction.ExecuteScalar());
                cn.Close();
            }

            return BatchProduction.ToString();
        }

        private void BuildTree(string BatchCode)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    var cmBuiltTree = new SqlCommand("Select IsNull(ProductTreeCode,0) From Tbl_ProductionBatchs Where BatchCode = '" + BatchCode + "'", cn);
                    int TreeCode = Conversions.ToInteger(cmBuiltTree.ExecuteScalar());
                    var daBuiltTree = new SqlDataAdapter("Select * From Tbl_ProductTreeDetails Where TreeCode=" + TreeCode, cn);
                    var dtBuiltTree = new DataTable();
                    daBuiltTree.Fill(dtBuiltTree);
                    daBuiltTree.Dispose();
                    var dvRoot = dtBuiltTree.DefaultView;
                    TreeView1.Tag = TreeCode;
                    dvRoot.RowFilter = "LevelNo=0";
                    TreeView1.Nodes.Clear();
                    ADDNewTreeNode(null, new TreeNode(Conversions.ToString(dvRoot[0]["DetailName"])), Conversions.ToString(dvRoot[0]["DetailCode"]), dvRoot);
                    TreeView1.ExpandAll();
                }
                catch (Exception objEx)
                {
                    Logger.SaveError(Name + ".BuildTree", objEx.Message);
                    MessageBox.Show("فراخوانی درخت محصول با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    TreeView1.Nodes.Clear();
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }
        }

        private void ADDNewTreeNode(TreeNode ParentNode, TreeNode NewNode, string NewDetailCode, DataView dvChilds)
        {
            NewNode.Tag = NewDetailCode;
            if (ParentNode == null)
            {
              TreeView1.Nodes.Add(NewNode);
            }
            else
            {
                ParentNode.Nodes.Add(NewNode);
            }
                       
            GetCurrentRowChilds(NewDetailCode, ref dvChilds);
            if (dvChilds.Count == 0)
            {
                return;
            }

            for (short I = 0, loopTo = (short)(dvChilds.Count - 1); I <= loopTo; I++)
            {
                string RowFilter = dvChilds.RowFilter;
                ADDNewTreeNode(NewNode, new TreeNode(Conversions.ToString(dvChilds[I]["DetailName"])), Conversions.ToString(dvChilds[I]["DetailCode"]), dvChilds);
                dvChilds.RowFilter = RowFilter;
            }
        }

        private void AddNewRootNode(TreeNode NewNode, string NewDetailCode, DataView dvChilds)
        {
            NewNode.Tag = NewDetailCode;
            TreeView1.Nodes.Add(NewNode);
            GetCurrentRowChilds(NewDetailCode, ref dvChilds);
            if (dvChilds.Count == 0)
            {
                return;
            }

            for (short I = 0, loopTo = (short)(dvChilds.Count - 1); I <= loopTo; I++)
            {
                string RowFilter = dvChilds.RowFilter;
                ADDNewTreeNode(NewNode, new TreeNode(Conversions.ToString(dvChilds[I]["DetailName"])), Conversions.ToString(dvChilds[I]["DetailCode"]), dvChilds);
                dvChilds.RowFilter = RowFilter;
            }
        }
        private void GetCurrentRowChilds(string DetailCode, ref DataView dvChilds)
        {
            dvChilds.RowFilter = "ParentDetailCode='" + DetailCode + "'";
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (TreeView1.Tag is object && e.Node.Tag is object)
            {
                var daLoadOperations = new SqlDataAdapter(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select * From Tbl_ProductOPCs Where TreeCode = ", TreeView1.Tag), " And DetailCode='"), e.Node.Tag), "'")), Module1.cnProductionPlanning);
                var dtLoadOperations = new DataTable();
                daLoadOperations.Fill(dtLoadOperations);
                daLoadOperations.Dispose();
                SetOperationGridRows(dtLoadOperations.DefaultView);
            }
        }

        private void SetOperationGridRows(DataView dvGridSource)
        {
            dgOperations.Rows.Clear();
            for (int I = 0, loopTo = dvGridSource.Count - 1; I <= loopTo; I++)
            {
                dgOperations.Rows.Add(dvGridSource[I]["OperationCode"], dvGridSource[I]["OperationTitle"], GetOperationPlanningQuantity(Conversions.ToString(dvGridSource[I]["OperationCode"]), Conversions.ToString(dvGridSource[I]["TreeCode"])), GetOperationProductionQuantity(Conversions.ToString(dvGridSource[I]["OperationCode"]), Conversions.ToString(dvGridSource[I]["TreeCode"])), "0");
                long PlanningQty = Conversions.ToLong(dgOperations.Rows[dgOperations.Rows.Count - 1].Cells["colPlanningQuantity"].Value);
                long ProductionQty = Conversions.ToLong(dgOperations.Rows[dgOperations.Rows.Count - 1].Cells["ColProductionQuantity"].Value);
                long UnProductionQty = PlanningQty - ProductionQty;
                if (UnProductionQty > 0L)
                {
                    dgOperations.Rows[dgOperations.Rows.Count - 1].Cells["colUnProductionQuantity"].Value = UnProductionQty.ToString();
                }
                else
                {
                    dgOperations.Rows[dgOperations.Rows.Count - 1].Cells["colUnProductionQuantity"].Value = "0";
                }
            }
        }

        private long GetOperationPlanningQuantity(string OPerationCode, string TreeCode)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                var cmPlanningQuantity = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Select IsNull(Sum(DetailProductionQuantity),0) From Tbl_Planning Where TreeCode = " + TreeCode + " And OperationCode = '" + OPerationCode + "' And SubbatchCode IN (Select SubbatchCode From Tbl_ProductionSubbatchs Where BatchCode = '", cbBatchs.SelectedValue), "')")), cn);
                cn.Open();
                long PlanningQuantity = Conversions.ToLong(cmPlanningQuantity.ExecuteScalar());
                cn.Close();
                return PlanningQuantity;
            }
        }

        private long GetOperationProductionQuantity(string OPerationCode, string TreeCode)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                var cmProductionQuantity = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Select IsNull(Sum(IntactQuantity),0) From Tbl_RealProduction Where TreeCode = " + TreeCode + " And OperationCode = '" + OPerationCode + "' And SubbatchCode IN (Select SubbatchCode From Tbl_ProductionSubbatchs Where BatchCode = '", cbBatchs.SelectedValue), "')")), cn);
                cn.Open();
                long ProductionQuantity = Conversions.ToLong(cmProductionQuantity.ExecuteScalar());
                cn.Close();
                return ProductionQuantity;
            }
        }

        private void LoadBatchProductionOperations(string BatchCode, string TreeCode)
        {
            dgBatchProductionOperations.Rows.Clear();
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmProductionOperations = new SqlCommand("Select * From Tbl_ProductOPCs Where TreeCode = " + TreeCode + " And OperationCode IN (Select OperationCode From Tbl_RealProduction Where TreeCode = " + TreeCode + " And SubbatchCode IN (Select SubbatchCode From Tbl_ProductionSubbatchs Where BatchCode = '" + BatchCode + "'))", cn);
                var drProductionOperations = cmProductionOperations.ExecuteReader();
                while (true)
                {
                    if (drProductionOperations.Read())
                    {
                        dgBatchProductionOperations.Rows.Add(drProductionOperations["OperationCode"], drProductionOperations["OperationTitle"], GetOperationPlanningQuantity(Conversions.ToString(drProductionOperations["OperationCode"]), Conversions.ToString(drProductionOperations["TreeCode"])), GetOperationProductionQuantity(Conversions.ToString(drProductionOperations["OperationCode"]), Conversions.ToString(drProductionOperations["TreeCode"])), "0");
                        long PlanningQty = Conversions.ToLong(dgBatchProductionOperations.Rows[dgBatchProductionOperations.Rows.Count - 1].Cells["colPlanningQuantity1"].Value);
                        long ProductionQty = Conversions.ToLong(dgBatchProductionOperations.Rows[dgBatchProductionOperations.Rows.Count - 1].Cells["ColProductionQuantity1"].Value);
                        long UnProductionQty = PlanningQty - ProductionQty;
                        if (UnProductionQty > 0L)
                        {
                            dgBatchProductionOperations.Rows[dgBatchProductionOperations.Rows.Count - 1].Cells["colUnProductionQuantity1"].Value = UnProductionQty.ToString();
                        }
                        else
                        {
                            dgBatchProductionOperations.Rows[dgBatchProductionOperations.Rows.Count - 1].Cells["colUnProductionQuantity1"].Value = "0";
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                drProductionOperations.Close();
                // محاسبۀ مقدار پیشرفت تولید بچ
                cmProductionOperations.CommandText = "Select Substring(Convert(VarChar,Round(dbo.GetBatchProductionProgress('" + BatchCode + "'),2)),1,6) As ProductionProgressMeasure";
                drProductionOperations = cmProductionOperations.ExecuteReader();
                if (drProductionOperations.Read())
                {
                    lblBatchProductionProgress.Text = drProductionOperations["ProductionProgressMeasure"].ToString();
                }
                else
                {
                    lblBatchProductionProgress.Text = "";
                }

                drProductionOperations.Close();
                cn.Close();
            }
        }

        private void LoadBatchUnProductionOperations(string BatchCode, string TreeCode)
        {
            dgBatchUnProductionOperations.Rows.Clear();
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmUnProductionOperations = new SqlCommand("Select * From Tbl_ProductOPCs Where TreeCode = " + TreeCode + " And Not OperationCode IN (Select OperationCode From Tbl_RealProduction Where TreeCode = " + TreeCode + " And SubbatchCode IN (Select SubbatchCode From Tbl_ProductionSubbatchs Where BatchCode = '" + BatchCode + "'))", cn);
                var drUnProductionOperations = cmUnProductionOperations.ExecuteReader();
                while (true)
                {
                    if (drUnProductionOperations.Read())
                    {
                        dgBatchUnProductionOperations.Rows.Add(drUnProductionOperations["OperationCode"], drUnProductionOperations["OperationTitle"], GetOperationPlanningQuantity(Conversions.ToString(drUnProductionOperations["OperationCode"]), Conversions.ToString(drUnProductionOperations["TreeCode"])));
                    }
                    else
                    {
                        break;
                    }
                }

                drUnProductionOperations.Close();
                cn.Close();
            }
        }

        private long GetBatchPlanningQuantity(string BatchCode, string TreeCode)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                var cmPlanningQuantity = new SqlCommand("Select IsNull(Sum(DetailProductionQuantity),0) From Tbl_Planning " + "Where  SubbatchCode IN (Select SubbatchCode From Tbl_ProductionSubbatchs Where BatchCode = '" + BatchCode + "') And " + "       OperationCode IN(Select OperationCode From Tbl_OperationNetworkPaths " + "                        Where  TreeCode=" + TreeCode + " And PathCode=1 And ItemPriority=" + "                              (Select Max(ItemPriority) From Tbl_OperationNetworkPaths Where TreeCode=" + TreeCode + " And PathCode=1))", cn);
                cn.Open();
                long PlanningQuantity = Conversions.ToLong(cmPlanningQuantity.ExecuteScalar());
                cn.Close();
                return PlanningQuantity;
            }
        }

        private string GetBatchProductionStartDate(string BatchCode, string TreeCode)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                var cmProductionStartDate = new SqlCommand("Select IsNull(Min(StartDate),0) From Tbl_RealProduction " + "Where  SubbatchCode IN (Select SubbatchCode From Tbl_ProductionSubbatchs Where BatchCode = '" + BatchCode + "') And TreeCode = " + TreeCode, cn);
                cn.Open();
                string ProductionStartDate = Conversions.ToLong(cmProductionStartDate.ExecuteScalar()).ToString();
                cn.Close();
                if (!DBNull.Value.Equals(ProductionStartDate) && ProductionStartDate is object && !ProductionStartDate.Equals("") && !ProductionStartDate.Equals("0"))
                {
                    ProductionStartDate = ProductionStartDate.Substring(0, 4) + "/" + ProductionStartDate.Substring(4, 2) + "/" + ProductionStartDate.Substring(6, 2);
                }
                else
                {
                    ProductionStartDate = "//";
                }

                return ProductionStartDate;
            }
        }

        private string GetBatchPlaningStartDate(string BatchCode, string TreeCode)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                var cmPlaningStartDate = new SqlCommand("Select IsNull(Min(PlanningStartDate),0) From Tbl_Planning " + "Where  SubbatchCode IN (Select SubbatchCode From Tbl_ProductionSubbatchs Where BatchCode = '" + BatchCode + "') And TreeCode = " + TreeCode, cn);
                cn.Open();
                string PlaningStartDate = Conversions.ToLong(cmPlaningStartDate.ExecuteScalar()).ToString();
                cn.Close();
                if (!DBNull.Value.Equals(PlaningStartDate) && PlaningStartDate is object && !PlaningStartDate.Equals("") && !PlaningStartDate.Equals("0"))
                {
                    PlaningStartDate = PlaningStartDate.Substring(0, 4) + "/" + PlaningStartDate.Substring(4, 2) + "/" + PlaningStartDate.Substring(6, 2);
                }
                else
                {
                    PlaningStartDate = "//";
                }

                return PlaningStartDate;
            }
        }

        private void Get_Batch_Production_Planning_EndDate(string BatchCode, ref string ProductionEnd, ref string PlanningEnd)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                var cmProductionStartDate = new SqlCommand("Select(Select MAX(PlanningEndDate) " + "       From   dbo.Tbl_Planning " + "       Where  SubbatchCode IN (Select SubbatchCode " + "                               From   dbo.Tbl_ProductionSubbatchs " + "                               Where  BatchCode = A.BatchCode)) AS PlanningEndDate, " + "       CASE A.FinishedDate " + "       When 0 Then '' " + "       Else (Select Max(EndDate) " + "             From   dbo.Tbl_RealProduction " + "             Where  SubbatchCode IN (Select SubbatchCode " + "                                     From   dbo.Tbl_ProductionSubbatchs " + "                                     Where  BatchCode = A.BatchCode)) End AS RealEndDate " + "From   Tbl_ProductionBatchs A " + "Where  A.BatchCode = '" + BatchCode + "'", cn);
                cn.Open();
                var drEndDates = cmProductionStartDate.ExecuteReader();
                if (drEndDates.Read())
                {
                    if (!DBNull.Value.Equals(drEndDates["RealEndDate"]) && !drEndDates["RealEndDate"].ToString().Equals("") && !drEndDates["RealEndDate"].ToString().Equals("0"))
                    {
                        ProductionEnd = drEndDates["RealEndDate"].ToString().Substring(0, 4) + "/" + drEndDates["RealEndDate"].ToString().Substring(4, 2) + "/" + drEndDates["RealEndDate"].ToString().Substring(6, 2);
                    }
                    else
                    {
                        ProductionEnd = "//";
                    }

                    if (!DBNull.Value.Equals(drEndDates["PlanningEndDate"]) && !drEndDates["PlanningEndDate"].ToString().Equals("") && !drEndDates["PlanningEndDate"].ToString().Equals("0"))
                    {
                        PlanningEnd = drEndDates["PlanningEndDate"].ToString().Substring(0, 4) + "/" + drEndDates["PlanningEndDate"].ToString().Substring(4, 2) + "/" + drEndDates["PlanningEndDate"].ToString().Substring(6, 2);
                    }
                    else
                    {
                        PlanningEnd = "//";
                    }
                }
                else
                {
                    ProductionEnd = "//";
                    PlanningEnd = "//";
                }

                drEndDates.Close();
                cn.Close();
            }
        }

        private void GetBatchDetour(string BatchCode, string TreeCode, string CalcDate, ref int DetourPercent, ref string DetourHour)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmDetour = new SqlCommand("Select PlanningCode,PlanningStartDate,PlanningEndDate,PlanningDuration," + "       Case (Select ExecutionMethod " + "             From   Tbl_ProductOPCs " + "             Where  TreeCode = Tbl_Planning.TreeCode And OperationCode = Tbl_Planning.OperationCode) " + "       When 3 Then -1 " + "       When 2 Then(Select CalendarCode " + "                   From   Tbl_ProductOPCsExecutorMachines " + "                   Where  TreeCode = Tbl_Planning.TreeCode And OperationCode = Tbl_Planning.OperationCode And MachineCode = '-1') " + "       When 1 Then(Select CalendarCode From Tbl_Machines Where Code = Tbl_Planning.MachineCode) " + "       End As CalendarCode " + "From   Tbl_Planning Where TreeCode = " + TreeCode + " And PlanningStartDate <= '" + CalcDate + "' And PlanningEndDate <= '" + CalcDate + "' And SubbatchCode IN " + "(Select SubbatchCode From Tbl_ProductionSubbatchs Where BatchCode = '" + BatchCode + "')", cn);
                var drDetour = cmDetour.ExecuteReader();
                double PlanningDuration = 0.0d;
                string mPlanningCodes = Constants.vbNullString;
                while (drDetour.Read())
                {
                    if (string.IsNullOrEmpty(mPlanningCodes))
                    {
                        mPlanningCodes = drDetour["PlanningCode"].ToString();
                    }
                    else if (!mPlanningCodes.Contains(drDetour["PlanningCode"].ToString()))
                    {
                        mPlanningCodes += "," + drDetour["PlanningCode"].ToString();
                    }

                    PlanningDuration += Conversions.ToDouble(drDetour["PlanningDuration"]);
                }

                drDetour.Close();
                cmDetour.CommandText = "Select A.PlanningCode,A.PlanningStartDate,A.PlanningEndDate, " + "       Case (Select ExecutionMethod " + "             From   Tbl_ProductOPCs " + "             Where  TreeCode = A.TreeCode And OperationCode = A.OperationCode) " + "       When 3 Then -1 " + "       When 2 Then(Select CalendarCode " + "                   From   Tbl_ProductOPCsExecutorMachines " + "                   Where  TreeCode = A.TreeCode And OperationCode = A.OperationCode And MachineCode = '-1') " + "       When 1 Then(Select CalendarCode From Tbl_Machines Where Code = A.MachineCode) " + "       End As CalendarCode " + "From   Tbl_Planning A Where A.TreeCode = " + TreeCode + " And A.PlanningStartDate <= '" + CalcDate + "' And A.PlanningEndDate > '" + CalcDate + "' And A.SubbatchCode IN " + "(Select SubbatchCode From Tbl_ProductionSubbatchs Where BatchCode = '" + BatchCode + "')";
                drDetour = cmDetour.ExecuteReader();
                while (drDetour.Read())
                {
                    if (string.IsNullOrEmpty(mPlanningCodes))
                    {
                        mPlanningCodes = drDetour["PlanningCode"].ToString();
                    }
                    else if (!mPlanningCodes.Contains(drDetour["PlanningCode"].ToString()))
                    {
                        mPlanningCodes += "," + drDetour["PlanningCode"].ToString();
                    }

                    PlanningDuration += GetDaysDurationTime(Conversions.ToString(drDetour["PlanningStartDate"]), CalcDate, Conversions.ToInteger(drDetour["CalendarCode"]));
                }

                drDetour.Close();
                if (string.IsNullOrEmpty(mPlanningCodes))
                {
                    mPlanningCodes = "0";
                }

                cmDetour.CommandText = "Select   IsNull((Sum(IntactQuantity) * OneExecTime),0.0) As ProductionDuration " + "From     (Select IntactQuantity,PlanningCode,dbo.GetOperationExecutionTime(MachineCode, TreeCode, OperationCode) As OneExecTime " + "          From Tbl_RealProduction Where PlanningCode IN (" + mPlanningCodes + ") And StartDate <= '" + CalcDate + "') Tbl_RealProduction " + "Group By PlanningCode,OneExecTime ";
                double ProductionDuration = 0.0d;
                drDetour = cmDetour.ExecuteReader();
                while (drDetour.Read())
                    ProductionDuration += Conversions.ToDouble(drDetour["ProductionDuration"]);
                drDetour.Close();
                cn.Close();
                lblPlanningDuration.Text = Module1.GetRegulareHour(PlanningDuration.ToString());
                lblProductionDuration.Text = Module1.GetRegulareHour(ProductionDuration.ToString());
                if (PlanningDuration <= 0.0d)
                {
                    DetourPercent = 0;
                }
                else
                {
                    DetourPercent = (int)Math.Round(100d - ProductionDuration * 100d / PlanningDuration);
                } // CInt((ProductionDuration / PlanningDuration) * 100)

                if (DetourPercent < 0)
                {
                    DetourPercent *= -1;
                }

                DetourHour = (PlanningDuration - ProductionDuration).ToString();
                if (Conversions.ToDouble(DetourHour) == 0.0d)
                {
                    DetourHour = "00:00";
                }
                else
                {
                    DetourHour = Module1.GetRegulareHour(Conversions.ToDouble(DetourHour).ToString());
                }

                if (DetourHour.Contains("-"))
                {
                    lblDetourDescription.Text = "جلوتر از برنامه";
                    DetourHour = DetourHour.Replace("-", "");
                }
                else if (DetourHour.Equals("00:00"))
                {
                    lblDetourDescription.Text = "بدون انحراف";
                }
                else
                {
                    lblDetourDescription.Text = "عقبتر از برنامه";
                }
            }
        }

        private double GetDaysDurationTime(string StartDate, string EndDate, int CalendarCode)
        {
            double DayTime = 0d;
            while (true)
            {
                if (Module1.IsHoliday(CalendarCode.ToString(), StartDate))
                {
                    DayTime += 0.0d;
                }
                else
                {
                    short ParticulareDayType = Module1.IsParticularDay(CalendarCode.ToString(), StartDate);
                    short DayNo = Convert.ToInt16(Module1.GetDayNo(StartDate));
                    string DayAccessibleTime = "00:00";
                    // Const DT_HOLIDAY = 1
                    const int DT_COMMON = 2;
                    switch (ParticulareDayType)
                    {
                        case -1: // روز عادی
                            {
                                DayAccessibleTime = Module1.GetDayAccessibleTime(CalendarCode.ToString(), DayNo.ToString(), 1);
                                break;
                            }

                        case DT_COMMON: // روز خاص غیر تعطیل
                            {
                                DayAccessibleTime = Module1.GetDayAccessibleTime(CalendarCode.ToString(), DayNo.ToString(), 2);
                                break;
                            }
                    }

                    DayTime += Conversions.ToDouble(Module1.GetFloatingHour(DayAccessibleTime));
                }

                StartDate = FarsiDateFunctions.AddToDate(StartDate, "00000001");
                if (Operators.CompareString(StartDate, EndDate, false) > 0)
                {
                    break;
                }
            }

            return DayTime;
        }


        /* TODO ERROR: Skipped RegionDirectiveTrivia */
        private void tmrLoadOvers_Tick(object sender, EventArgs e)
        {
            tmrLoadOvers.Enabled = false;
            CalcBatchOverProduction(Conversions.ToString(cbBatchs.SelectedValue), TreeView1.Tag.ToString());
        }

        private void CalcBatchOverProduction(string BatchCode, string mTreeCode)
        {
            try
            {
                var tpOverProduction = GetOverTabPage();
                using (var cn = new SqlConnection(Module1.PlanningCnnStr))
                {
                    cn.Open();
                    TreeView tvOverItems = null;
                    if (tpOverProduction.Controls[0] is TabControl)
                    {
                        TabControl tbc = (TabControl)tpOverProduction.Controls["tbcPrintPreview"];
                        var tpTreeView = tbc.TabPages["tpTreeView"];
                        var tpPrintPreview = tbc.TabPages["tpPrintPreview"];
                        ((CrystalReportViewer)tpPrintPreview.Controls["ReportViewer"]).ReportSource = null;
                        tbc.SelectTab(tpTreeView);
                        tvOverItems = (TreeView)tpTreeView.Controls["tvOverItems"];
                    }
                    else
                    {
                        tvOverItems = (TreeView)tpOverProduction.Controls["tvOverItems"];
                    }

                    var cmOverProduction = new SqlCommand("Select DetailCode, DetailName From Tbl_ProductTreeDetails Where TreeCode = " + mTreeCode + " And ParentDetailCode = '0'", cn);
                    var drOverProduction = cmOverProduction.ExecuteReader();
                    tvOverItems.Nodes.Clear();
                    while (drOverProduction.Read())
                    {
                        var tn = new TreeNode(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(drOverProduction["DetailName"], " ["), GetDetailOverProduction(mTreeCode, Conversions.ToString(drOverProduction["DetailCode"]), BatchCode).ToString()), "]")));
                        tn.Tag = Operators.ConcatenateObject("dtl:", drOverProduction["DetailCode"]);
                        tn.ForeColor = Color.Blue;
                        tvOverItems.Nodes.Add(tn);
                        AddDetailOperationsToNode(tn, mTreeCode, BatchCode);
                        AddChildDetails(Conversions.ToString(drOverProduction["DetailCode"]), mTreeCode, tvOverItems.Nodes[tvOverItems.Nodes.Count - 1], BatchCode);
                    }

                    drOverProduction.Close();
                    cn.Close();
                }
            }
            catch (Exception objEx)
            {
                Cursor = Cursors.Default;
                Logger.SaveError(Name + ".CalcBatchOverProduction", objEx.Message);
                MessageBox.Show("نمایش مازاد تولید بچ با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }

        private int GetDetailOverProduction(string mTreeCode, string mDetailCode, string mBatchCode)
        {
            int mOverQuantity = 0;
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmDetailOverQty = new SqlCommand("Select IsNull(CalcedOverStock,0) From Tbl_ProductionBatchsDetail Where BatchCode = '" + mBatchCode + "' And DetailCode = '" + mDetailCode + "'", cn);
                mOverQuantity = Conversions.ToInteger(cmDetailOverQty.ExecuteScalar());
                cn.Close();
            }

            return mOverQuantity;
        }

        private void AddChildDetails(string mDetailCode, string mTreeCode, TreeNode tnParent, string mBatchCode)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmOverProduction = new SqlCommand("Select DetailCode,DetailName From Tbl_ProductTreeDetails Where TreeCode = " + mTreeCode + " And ParentDetailCode = '" + mDetailCode + "'", cn);
                var drOverProduction = cmOverProduction.ExecuteReader();
                while (drOverProduction.Read())
                {
                    var tn = new TreeNode(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(drOverProduction["DetailName"], " ["), GetDetailOverProduction(mTreeCode, Conversions.ToString(drOverProduction["DetailCode"]), mBatchCode).ToString()), "]")));
                    tn.Tag = Operators.ConcatenateObject("dtl:", drOverProduction["DetailCode"]);
                    tn.ForeColor = Color.Blue;
                    tnParent.Nodes.Add(tn);
                    AddDetailOperationsToNode(tn, mTreeCode, mBatchCode);
                    AddChildDetails(Conversions.ToString(drOverProduction["DetailCode"]), mTreeCode, tn, mBatchCode);
                }

                drOverProduction.Close();
                cn.Close();
            }
        }

        private TabPage GetOverTabPage()
        {
            TabPage tpOverProduction = null;
            if (TabControl1.TabPages.Count < 4 || !TabControl1.TabPages[3].Name.Equals("tpOverProduction"))
            {
                tpOverProduction = new TabPage("نمایش مازاد تولید اقلام بچ");
                tpOverProduction.Name = "tpOverProduction";
                CreateOverProductionTreeView(tpOverProduction);
                var cmdPrintReport = new System.Windows.Forms.Button();
                cmdPrintReport.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                cmdPrintReport.Text = "پیش نمایش چاپ";
                cmdPrintReport.TextAlign = ContentAlignment.MiddleCenter;
                cmdPrintReport.Font = new Font("Tahoma", 10f, FontStyle.Regular);
                cmdPrintReport.Size = new Size(130, 26);
                cmdPrintReport.Location = new Point(20, tpOverProduction.Height - 27);
                cmdPrintReport.Click += cmdPrintReport_Click;
                tpOverProduction.Controls.Add(cmdPrintReport);
                TabControl1.TabPages.Add(tpOverProduction);
                TabControl1.SelectTab(tpOverProduction);
            }
            else
            {
                tpOverProduction = TabControl1.TabPages["tpOverProduction"];
            }

            return tpOverProduction;
        }

        private void CreateOverProductionTreeView(TabPage tp)
        {
            var tvOverItems = new TreeView();
            tvOverItems.Name = "tvOverItems";
            tvOverItems.Font = new Font("Tahoma", 10f);
            tvOverItems.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            tvOverItems.Dock = DockStyle.None;
            tvOverItems.Location = new Point(0, 0);
            tvOverItems.RightToLeft = RightToLeft.Yes;
            tvOverItems.RightToLeftLayout = true;
            tvOverItems.Size = new Size(tp.Width - 1, tp.Height - 30);
            tvOverItems.TabIndex = 1;
            tp.Controls.Add(tvOverItems);
        }

        private void cmdPrintReport_Click(object sender, EventArgs e)
        {
            var drBatchInfo = dsProductionPlanning.Tables["Tbl_ProductionBatchs"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("BatchCode='", cbBatchs.SelectedValue), "'")));
            if (drBatchInfo.Length > 0)
            {
                var tbcPrintPreview = new TabControl();
                var tpTreeView = new TabPage("درختواره");
                tbcPrintPreview.Dock = DockStyle.Fill;
                tbcPrintPreview.Name = "tbcPrintPreview";
                tbcPrintPreview.Appearance = TabAppearance.FlatButtons;
                tbcPrintPreview.RightToLeft = RightToLeft.Yes;
                tbcPrintPreview.RightToLeftLayout = true;
                tpTreeView.Name = "tpTreeView";
                for (int I = TabControl1.TabPages["tpOverProduction"].Controls.Count - 1; I >= 0; I -= 1)
                {
                    var c = TabControl1.TabPages["tpOverProduction"].Controls[I];
                    tpTreeView.Controls.Add(c);
                    if (c is TreeView)
                    {
                        c.Width = tpTreeView.Width - 1;
                        c.Height = tpTreeView.Height - 30;
                    }
                    else if (c is System.Windows.Forms.Button)
                    {
                        c.Location = new Point(20, tpTreeView.Height - 27);
                        c.Click -= cmdPrintReport_Click;
                        c.Click += cmdPrintReport_Click1;
                    }
                }

                tbcPrintPreview.TabPages.Add(tpTreeView);
                var tpPreview = GetPrintPreviewTabPage(tbcPrintPreview);
                tbcPrintPreview.TabPages.Add(tpPreview);
                TabControl1.TabPages["tpOverProduction"].Controls.Add(tbcPrintPreview);

                // پر کردن جدول موقت مورد استفادۀ گزارش
                PrintBatchOverStocks((TreeView)tpTreeView.Controls["tvOverItems"], Conversions.ToString(drBatchInfo[0]["BatchCode"]), Conversions.ToString(drBatchInfo[0]["TreeCode"]));
                // فراخوانی گزارش با اطلاعات جدید
                LoadReport((CrystalReportViewer)tpPreview.Controls["ReportViewer"], "Rpt102_BatchOverStocks.rpt");
                tbcPrintPreview.SelectTab(tpPreview);
            }
        }

        private void cmdPrintReport_Click1(object sender, EventArgs e)
        {
            var drBatchInfo = dsProductionPlanning.Tables["Tbl_ProductionBatchs"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("BatchCode='", cbBatchs.SelectedValue), "'")));
            TabControl tbc = (TabControl)TabControl1.TabPages["tpOverProduction"].Controls["tbcPrintPreview"];
            var tpTreeView = tbc.TabPages["tpTreeView"];
            var tpPrintPreview = tbc.TabPages["tpPrintPreview"];

            // پر کردن جدول موقت مورد استفادۀ گزارش
            PrintBatchOverStocks((TreeView)tpTreeView.Controls["tvOverItems"], Conversions.ToString(drBatchInfo[0]["BatchCode"]), Conversions.ToString(drBatchInfo[0]["TreeCode"]));
            // فراخوانی گزارش با اطلاعات جدید
            LoadReport((CrystalReportViewer)tpPrintPreview.Controls["ReportViewer"], "Rpt102_BatchOverStocks.rpt");
            tbc.SelectTab(tpPrintPreview);
        }

        private TabPage GetPrintPreviewTabPage(TabControl tbc)
        {
            var tpPrintPreview = new TabPage("پیش نمایش چاپ");
            try
            {
                tpPrintPreview.Name = "tpPrintPreview";

                // If tbc.TabPages.Count < 2 OrElse Not tbc.TabPages(2).Name.Equals("tpPrintPreview") Then
                // Dim cmdClosePreview As New Button()
                // cmdClosePreview.Text = "بستن"
                // cmdClosePreview.TextAlign = ContentAlignment.MiddleCenter
                // cmdClosePreview.Font = New Drawing.Font("Tahoma", 10, FontStyle.Bold)
                // cmdClosePreview.Size = New Size(100, 30)
                // cmdClosePreview.Location = New Point(20, 10)
                // AddHandler cmdClosePreview.Click, AddressOf cmdClosePreview_Click
                // tpPrintPreview.Controls.Add(cmdClosePreview)

                var ReportViewer = new CrystalReportViewer();
                ReportViewer.Name = "ReportViewer";
                ReportViewer.Dock = DockStyle.Fill;
                ReportViewer.Size = new Size(tpPrintPreview.Width - 1, tpPrintPreview.Height - 1);
                ReportViewer.Location = new Point(0, 0);
                // .Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Bottom Or AnchorStyles.Right
                ReportViewer.DisplayGroupTree = false;
                ReportViewer.RightToLeft = RightToLeft.No;
                tpPrintPreview.Controls.Add(ReportViewer);
                // End If

                Cursor = Cursors.Default;
            }
            catch (Exception objEx)
            {
                Cursor = Cursors.Default;
                Logger.SaveError(Name + ".GetPrintPreviewTabPage", objEx.Message);
                MessageBox.Show("نمایش نسخۀ چاپی با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }

            return tpPrintPreview;
        }

        private bool LoadReport(CrystalReportViewer ReportViewer, string mReportName)
        {
            var CnnBuilder = new SqlConnectionStringBuilder(Module1.PlanningCnnStr);
            var CnnInfo = new ConnectionInfo();
            var RptDoc = new ReportDocument();
            CnnInfo.ServerName = CnnBuilder.DataSource;
            CnnInfo.DatabaseName = "PSB_ProductionPlanning";
            CnnInfo.IntegratedSecurity = CnnBuilder.IntegratedSecurity;
            if (!CnnBuilder.IntegratedSecurity)
            {
                CnnInfo.UserID = CnnBuilder.UserID;
                CnnInfo.Password = CnnBuilder.Password;
            }

            if (File.Exists(Module1.GetReportFolderPath() + mReportName))
            {
                RptDoc.Load(Module1.GetReportFolderPath() + mReportName, OpenReportMethod.OpenReportByDefault);
                RptDoc.ParameterFields["PrintDate"].CurrentValues.AddValue(Module1.mServerShamsiDate);
                foreach (Table tb in RptDoc.Database.Tables)
                {
                    tb.LogOnInfo.ConnectionInfo = CnnInfo;
                    tb.ApplyLogOnInfo(tb.LogOnInfo);
                }

                ReportViewer.ReportSource = null;
                ReportViewer.ReportSource = RptDoc;
            }
            else
            {
                Cursor = Cursors.Default;
                MessageBox.Show("فایل گزارش در مسیر اجرایی نرم افزار یافت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                return false;
            }

            return true;
        }

        private void AddDetailOperationsToNode(TreeNode tnParent, string mTreeCode, string mBatchCode)
        {
            AddedOperations = Constants.vbNullString;
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    // فراخوانی عملیات هایی که پیشنیاز آنها در جزء جاری وجود ندارد و در اجزاء دیگر درخت می باشد
                    var cmDetailOperations = new SqlCommand("Select OperationCode,OperationTitle " + "From   Tbl_ProductOPCs " + "Where  TreeCode = " + mTreeCode + " And DetailCode = '" + tnParent.Tag.ToString().Split(':')[1] + "' And " + "       OperationCode IN (Select CurrentOperationCode From Tbl_PreOperations Where TreeCode = " + mTreeCode + " And " + "       Not PreOperationCode IN (Select OperationCode From Tbl_ProductOPCs Where  TreeCode = " + mTreeCode + " And DetailCode = '" + tnParent.Tag.ToString().Split(':')[1] + "'))", cn);
                    var drDetailOperations = cmDetailOperations.ExecuteReader();
                    while (drDetailOperations.Read())
                    {
                        if (AddedOperations is object)
                        {
                            if (!AddedOperations.Contains(Conversions.ToString(drDetailOperations["OperationCode"])))
                            {
                                AddOperationToNode(mTreeCode, Conversions.ToString(drDetailOperations["OperationCode"]), Conversions.ToString(drDetailOperations["OperationTitle"]), tnParent, mBatchCode);
                            }
                        }
                        else
                        {
                            AddOperationToNode(mTreeCode, Conversions.ToString(drDetailOperations["OperationCode"]), Conversions.ToString(drDetailOperations["OperationTitle"]), tnParent, mBatchCode);
                        }
                    }

                    drDetailOperations.Close();
                    // فراخوانی لیست عملیات هایی که دارای پیش نیاز نیستند
                    cmDetailOperations.CommandText = "Select OperationCode,OperationTitle " + "From   Tbl_ProductOPCs " + "Where  TreeCode = " + mTreeCode + " And DetailCode = '" + tnParent.Tag.ToString().Split(':')[1] + "' And " + "       Not OperationCode IN (Select CurrentOperationCode From Tbl_PreOperations Where TreeCode = " + mTreeCode + ")";
                    drDetailOperations = cmDetailOperations.ExecuteReader();
                    while (drDetailOperations.Read())
                    {
                        if (AddedOperations is object)
                        {
                            if (!AddedOperations.Contains(Conversions.ToString(drDetailOperations["OperationCode"])))
                            {
                                AddOperationToNode(mTreeCode, Conversions.ToString(drDetailOperations["OperationCode"]), Conversions.ToString(drDetailOperations["OperationTitle"]), tnParent, mBatchCode);
                            }
                        }
                        else
                        {
                            AddOperationToNode(mTreeCode, Conversions.ToString(drDetailOperations["OperationCode"]), Conversions.ToString(drDetailOperations["OperationTitle"]), tnParent, mBatchCode);
                        }
                    }

                    drDetailOperations.Close();
                }

                // If tnParent.Nodes(tnParent.Nodes.Count - 1).Tag.ToString().Contains("opr:") Then
                // Dim s As String = tnParent.Text
                // tnParent.Text = tnParent.Text.Substring(0, tnParent.Text.IndexOf("["))
                // tnParent.Text = tnParent.Text.Trim() & " " & tnParent.Nodes(tnParent.Nodes.Count - 1).Text.Substring(tnParent.Nodes(tnParent.Nodes.Count - 1).Text.IndexOf("["))
                // End If
                catch (Exception objEx)
                {
                    Logger.SaveError(Name + ".AddDetailOperationsToNode", objEx.Message);
                    MessageBox.Show("فراخوانی لیست عملیاتهای مازاد تولید با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }
        }

        private void AddAfterardOperations(string TreeCode, string DetailCode, string PreOperationCode, TreeNode tnParent, string mBatchCode)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmHasAfterward = new SqlCommand("Select A.CurrentOperationCode As OperationCode,B.OperationTitle From Tbl_PreOperations A Inner Join Tbl_ProductOPCs B ON A.TreeCode = B.TreeCode And A.CurrentOperationCode = B.OperationCode Where A.TreeCode = " + TreeCode + " And A.PreOperationCode = '" + PreOperationCode + "' And A.CurrentOperationCode IN (Select OperationCode From Tbl_ProductOPCs Where TreeCode = " + TreeCode + " And DetailCode = '" + DetailCode + "')", cn);
                var drHasAfterward = cmHasAfterward.ExecuteReader();
                if (drHasAfterward.Read())
                {
                    if (!AddedOperations.Contains(Conversions.ToString(drHasAfterward["OperationCode"])))
                    {
                        AddOperationToNode(TreeCode, Conversions.ToString(drHasAfterward["OperationCode"]), Conversions.ToString(drHasAfterward["OperationTitle"]), tnParent, mBatchCode);
                    }
                }

                cn.Close();
            }
        }

        private void AddOperationToNode(string mTreeCode, string mOperationCode, string mOperationTitle, TreeNode tnParent, string mBatchCode)
        {
            var tn = new TreeNode("(" + mOperationCode + ") " + mOperationTitle + " [" + GetOperationOverProductionQuantity(mTreeCode, mOperationCode, Module1.GetDetailParentQuantity(mTreeCode, tnParent.Tag.ToString().Split(':')[1]), mBatchCode).ToString() + "]");
            tn.Tag = "opr:" + mOperationCode;
            tn.ForeColor = Color.Green;
            tnParent.Nodes.Add(tn);
            AddedOperations = Conversions.ToString(AddedOperations + Interaction.IIf(string.IsNullOrEmpty(AddedOperations), "'" + mOperationCode + "'", ",'" + mOperationCode + "'"));
            AddAfterardOperations(mTreeCode, tnParent.Tag.ToString().Split(':')[1], mOperationCode, tnParent, mBatchCode);
        }

        private int GetOperationOverProductionQuantity(string mTreeCode, string mOperationCode, int mDetailParentQuantity, string mBatchCode)
        {
            int OPOverProduction = 0;
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmOverProduction = new SqlCommand("Select IsNull(Sum(IntactQuantity),0) From Tbl_RealProduction Where TreeCode = " + mTreeCode + " And OperationCode = '" + mOperationCode + "' And SubbatchCode IN (Select SubbatchCode From Tbl_ProductionSubbatchs Where BatchCode = '" + mBatchCode + "')", cn);
                OPOverProduction = Conversions.ToInteger(cmOverProduction.ExecuteScalar());
                OPOverProduction = (int)Math.Round(OPOverProduction / (double)mDetailParentQuantity) - Conversions.ToInteger(lblDoneQuantity.Text);
                cn.Close();
            }

            return OPOverProduction;
        }

        private void PrintBatchOverStocks(TreeView tvOvers, string mBatchCode, string mTreeCode)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var trnInsert = cn.BeginTransaction();
                var cmInsertOvers = new SqlCommand("if Not Exists(select * from dbo.sysobjects where id = object_id(N'[dbo].[Tbl_TempBatchOverStocks]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)" + "Begin " + "  Create Table [dbo].[Tbl_TempBatchOverStocks](" + "               [BatchCode] [varchar] (20) COLLATE Arabic_CS_AS_KS_WS NOT NULL," + "               [TreeCode] [int] NOT NULL," + "               [DetailCode] [varchar] (50) COLLATE Arabic_CS_AS_KS_WS NOT NULL," + "               [DetailName] [varchar] (100) COLLATE Arabic_CS_AS_KS_WS NOT NULL," + "               [ParentDetailCode] [varchar] (50) COLLATE Arabic_CS_AS_KS_WS NOT NULL," + "               [OperationCode] [varchar] (50) COLLATE Arabic_CS_AS_KS_WS NOT NULL," + "               [OperationTitle] [varchar] (100) COLLATE Arabic_CS_AS_KS_WS NOT NULL," + "               [DetailOverStock] [int] NOT NULL," + "               [OperationOverStock] [int] NOT NULL) " + "End", cn);
                try
                {
                    cmInsertOvers.Transaction = trnInsert;
                    cmInsertOvers.ExecuteNonQuery();
                    cmInsertOvers.CommandText = "Delete From Tbl_TempBatchOverStocks";
                    cmInsertOvers.ExecuteNonQuery();
                    cmInsertOvers.CommandText = "Insert Into Tbl_TempBatchOverStocks(BatchCode,TreeCode,DetailCode,DetailName,ParentDetailCode,OperationCode,OperationTitle,DetailOverStock,OperationOverStock) " + "Values(@BatchCode,@TreeCode,@DetailCode,@DetailName,@ParentDetailCode,@OperationCode,@OperationTitle,@DetailOverStock,@OperationOverStock)";
                    {
                        var withBlock = cmInsertOvers.Parameters;
                        withBlock.Add("@BatchCode", SqlDbType.VarChar, 20, "BatchCode");
                        withBlock.Add("@TreeCode", SqlDbType.Int, default, "TreeCode");
                        withBlock.Add("@DetailCode", SqlDbType.VarChar, 50, "DetailCode");
                        withBlock.Add("@DetailName", SqlDbType.VarChar, 100, "DetailName");
                        withBlock.Add("@ParentDetailCode", SqlDbType.VarChar, 50, "ParentDetailCode");
                        withBlock.Add("@OperationCode", SqlDbType.VarChar, 50, "OperationCode");
                        withBlock.Add("@OperationTitle", SqlDbType.VarChar, 100, "OperationTitle");
                        withBlock.Add("@DetailOverStock", SqlDbType.Int, default, "DetailOverStock");
                        withBlock.Add("@OperationOverStock", SqlDbType.Int, default, "OperationOverStock");
                    }

                    foreach (TreeNode tn in tvOvers.Nodes)
                    {
                        cmInsertOvers.Parameters["@BatchCode"].Value = mBatchCode;
                        cmInsertOvers.Parameters["@TreeCode"].Value = mTreeCode;
                        cmInsertOvers.Parameters["@DetailCode"].Value = tn.Tag.ToString().Split(':')[1];
                        cmInsertOvers.Parameters["@DetailName"].Value = tn.Text.Substring(0, tn.Text.IndexOf("[")).Trim();
                        cmInsertOvers.Parameters["@ParentDetailCode"].Value = "0";
                        cmInsertOvers.Parameters["@OperationCode"].Value = "-1";
                        cmInsertOvers.Parameters["@OperationTitle"].Value = "-1";
                        cmInsertOvers.Parameters["@DetailOverStock"].Value = tn.Text.Substring(tn.Text.IndexOf("[") + 1).Trim().Replace("]", "");
                        cmInsertOvers.Parameters["@OperationOverStock"].Value = "-1";
                        cmInsertOvers.ExecuteNonQuery();
                        AddChildsOverStockInDBTable(cmInsertOvers, tn, mBatchCode, mTreeCode);
                    }

                    trnInsert.Commit();
                }
                catch (Exception objEx)
                {
                    trnInsert.Rollback();
                    Logger.SaveError(Name + ".PrintBatchOverStocks", objEx.Message);
                    MessageBox.Show("چاپ لیست مازاد تولید اقلام بچ با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }
        }

        private void AddChildsOverStockInDBTable(SqlCommand cmInsert, TreeNode ParentNode, string mBatchCode, string mTreeCode)
        {
            foreach (TreeNode tn in ParentNode.Nodes)
            {
                if (tn.Tag.ToString().Split(':')[0].Equals("dtl"))
                {
                    cmInsert.Parameters["@BatchCode"].Value = mBatchCode;
                    cmInsert.Parameters["@TreeCode"].Value = mTreeCode;
                    cmInsert.Parameters["@DetailCode"].Value = tn.Tag.ToString().Split(':')[1];
                    cmInsert.Parameters["@DetailName"].Value = tn.Text.Substring(0, tn.Text.IndexOf("[")).Trim();
                    cmInsert.Parameters["@ParentDetailCode"].Value = ParentNode.Tag.ToString().Split(':')[1];
                    cmInsert.Parameters["@OperationCode"].Value = "-1";
                    cmInsert.Parameters["@OperationTitle"].Value = "-1";
                    cmInsert.Parameters["@DetailOverStock"].Value = tn.Text.Substring(tn.Text.IndexOf("[") + 1).Trim().Replace("]", "");
                    cmInsert.Parameters["@OperationOverStock"].Value = "-1";
                    cmInsert.ExecuteNonQuery();
                    AddChildsOverStockInDBTable(cmInsert, tn, mBatchCode, mTreeCode);
                }
                else if (tn.Tag.ToString().Split(':')[0].Equals("opr"))
                {
                    cmInsert.Parameters["@BatchCode"].Value = mBatchCode;
                    cmInsert.Parameters["@TreeCode"].Value = mTreeCode;
                    cmInsert.Parameters["@DetailCode"].Value = ParentNode.Tag.ToString().Split(':')[1];
                    cmInsert.Parameters["@DetailName"].Value = ParentNode.Text.Substring(0, ParentNode.Text.IndexOf("[")).Trim();
                    cmInsert.Parameters["@ParentDetailCode"].Value = "-1";
                    cmInsert.Parameters["@OperationCode"].Value = tn.Tag.ToString().Split(':')[1];
                    string OPTitle = tn.Text.Substring(0, tn.Text.IndexOf("[")).Trim();
                    cmInsert.Parameters["@OperationTitle"].Value = OPTitle.Substring(OPTitle.IndexOf(")") + 1).Trim();
                    cmInsert.Parameters["@DetailOverStock"].Value = ParentNode.Text.Substring(ParentNode.Text.IndexOf("[") + 1).Trim().Replace("]", "");
                    cmInsert.Parameters["@OperationOverStock"].Value = tn.Text.Substring(tn.Text.IndexOf("[") + 1).Trim().Replace("]", "");
                    cmInsert.ExecuteNonQuery();
                }
            }
        }

        /* TODO ERROR: Skipped EndRegionDirectiveTrivia */
        /* TODO ERROR: Skipped RegionDirectiveTrivia */
        private void cmdCalcProductionQuantity_Click(object sender, EventArgs e)
        {
            if (cbBatchs.SelectedValue is object && cbBatchs.SelectedIndex > -1)
            {
                if (rbProductionDate.Checked)
                {
                    if (txtProductionDate.Text is null || txtProductionDate.Text.Trim().Equals(""))
                    {
                        MessageBox.Show("تاریخ محصول تولیدی را وارد نمایید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        txtProductionDate.Focus();
                        return;
                    }
                    else if (txtProductionFrom.Text is object && !txtProductionFrom.Text.Trim().Equals("") && txtProductionFrom.Text.Trim().Length < 8)
                    {
                        MessageBox.Show("تاریخ شروع محاسبه را وارد نمایید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        txtProductionDate.Focus();
                        return;
                    }
                    else if (txtProductionFrom.Text is object && !txtProductionFrom.Text.Trim().Equals("") && Operators.CompareString(txtProductionFrom.Text, txtProductionDate.Text, false) >= 0)
                    {
                        MessageBox.Show("تاریخ شروع محاسبه نباید بزرگتر یا مساوی تاریخ محصول تولیدی باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        txtProductionFrom.Focus();
                        return;
                    }
                }

                dgProductionQuantityList.Rows.Clear();
                Application.DoEvents();
                using (var cn = new SqlConnection(Module1.PlanningCnnStr))
                {
                    var cmProductionQty = new SqlCommand("", cn);
                    SqlDataReader sdrProductionQty = null;
                    string mLastOperationCode = Constants.vbNullString;
                    var drCurrentBatch = dsProductionPlanning.Tables["Tbl_ProductionBatchs"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("BatchCode='", cbBatchs.SelectedValue), "'")));
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        Application.DoEvents();
                        cn.Open();
                        cmProductionQty.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select OperationCode From Tbl_OperationNetworkPaths Where TreeCode = ", drCurrentBatch[0]["TreeCode"]), " And PathCode = 1 And ItemPriority = (Select Max(ItemPriority) From Tbl_OperationNetworkPaths Where TreeCode = "), drCurrentBatch[0]["TreeCode"]), " And PathCode = 1)"));
                        sdrProductionQty = cmProductionQty.ExecuteReader();
                        if (sdrProductionQty.Read())
                        {
                            mLastOperationCode = Conversions.ToString(sdrProductionQty["OperationCode"]);
                        }

                        sdrProductionQty.Close();
                        sdrProductionQty = null;
                        if (string.IsNullOrEmpty(mLastOperationCode))
                        {
                            MessageBox.Show("کد آخرین عملیات مربوط به بچ انتخاب شده یافت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                            return;
                        }

                        int mTempQty = 0;
                        string mProductionDate = Constants.vbNullString;
                        double mProductionHour;
                        if (rbProductionQuantity.Checked)
                        {
                            cmProductionQty.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select IsNull(DetailProductionQuantity,0) As DetailProductionQuantity," + "PlanningStartDate As ProductionStartDate,PlanningStartHour As ProductionStartHour," + "PlanningEndDate As ProductionEndDate,PlanningEndHour As ProductionEndHour," + "SubbatchCode From Tbl_Planning Where TreeCode = ", drCurrentBatch[0]["TreeCode"]), " And SubbatchCode IN (Select SubbatchCode From Tbl_ProductionSubbatchs Where BatchCode = '"), drCurrentBatch[0]["BatchCode"]), "') And OperationCode = '"), mLastOperationCode), "'"));




                            sdrProductionQty = cmProductionQty.ExecuteReader();
                            while (sdrProductionQty.Read())
                            {
                                mProductionDate = Conversions.ToString(sdrProductionQty["ProductionStartDate"]);
                                mProductionHour = Conversions.ToDouble(sdrProductionQty["ProductionStartHour"]);
                                mTempQty = Conversions.ToInteger(mTempQty + (int)sdrProductionQty["DetailProductionQuantity"]);
                                while (mTempQty >= txtProductionQuantity.Value)
                                {
                                    if (Operators.CompareString(mProductionDate, sdrProductionQty["ProductionEndDate"].ToString(), false) <= 0)
                                    {
                                        string argmCurrentHour = mProductionHour.ToString();
                                        GetProductionDate(Conversions.ToString(drCurrentBatch[0]["TreeCode"]), mLastOperationCode, Conversions.ToString(sdrProductionQty["SubbatchCode"]), ref mProductionDate, ref argmCurrentHour, (int)Math.Round(txtProductionQuantity.Value));
                                        if (Operators.CompareString(mProductionDate, sdrProductionQty["ProductionEndDate"].ToString(), false) > 0)
                                        {
                                            mProductionDate = sdrProductionQty["ProductionEndDate"].ToString();
                                        }
                                    }
                                    else if (Operators.CompareString(mProductionDate, sdrProductionQty["ProductionEndDate"].ToString(), false) > 0)
                                    {
                                        mProductionDate = sdrProductionQty["ProductionEndDate"].ToString();
                                    }

                                    if (txtProductionFrom.Text is object && !txtProductionFrom.Text.Trim().Equals(""))
                                    {
                                        if (Operators.CompareString(mProductionDate, txtProductionFrom.Text, false) >= 0)
                                        {
                                            string mSlashedProductionDate = mProductionDate.Substring(0, 4) + "/" + mProductionDate.Substring(4, 2) + "/" + mProductionDate.Substring(6, 2);
                                            dgProductionQuantityList.Rows.Add(txtProductionQuantity.Value, mSlashedProductionDate, (dgProductionQuantityList.Rows.Count + 1) * txtProductionQuantity.Value);
                                        }
                                    }
                                    else
                                    {
                                        string mSlashedProductionDate = mProductionDate.Substring(0, 4) + "/" + mProductionDate.Substring(4, 2) + "/" + mProductionDate.Substring(6, 2);
                                        dgProductionQuantityList.Rows.Add(txtProductionQuantity.Value, mSlashedProductionDate, (dgProductionQuantityList.Rows.Count + 1) * txtProductionQuantity.Value);
                                    }

                                    mTempQty = (int)Math.Round(mTempQty - txtProductionQuantity.Value);
                                }
                            }

                            sdrProductionQty.Close();
                            sdrProductionQty = null;
                            if (mTempQty > 0)
                            {
                                if (txtProductionFrom.Text is object && !txtProductionFrom.Text.Trim().Equals(""))
                                {
                                    if (Operators.CompareString(mProductionDate, txtProductionFrom.Text, false) >= 0)
                                    {
                                        string mSlashedProductionDate = mProductionDate.Substring(0, 4) + "/" + mProductionDate.Substring(4, 2) + "/" + mProductionDate.Substring(6, 2);
                                        if (mTempQty >= txtProductionQuantity.Value)
                                        {
                                            while (mTempQty >= txtProductionQuantity.Value)
                                            {
                                                dgProductionQuantityList.Rows.Add(txtProductionQuantity.Value, mSlashedProductionDate, (dgProductionQuantityList.Rows.Count + 1) * txtProductionQuantity.Value);
                                                mTempQty = (int)Math.Round(mTempQty - txtProductionQuantity.Value);
                                            }

                                            if (mTempQty > 0)
                                            {
                                                dgProductionQuantityList.Rows.Add(mTempQty, mSlashedProductionDate, dgProductionQuantityList.Rows.Count * txtProductionQuantity.Value + mTempQty);
                                            }
                                        }
                                        else
                                        {
                                            dgProductionQuantityList.Rows.Add(mTempQty, mSlashedProductionDate, dgProductionQuantityList.Rows.Count * txtProductionQuantity.Value + mTempQty);
                                        }
                                    }
                                }
                                else
                                {
                                    string mSlashedProductionDate = mProductionDate.Substring(0, 4) + "/" + mProductionDate.Substring(4, 2) + "/" + mProductionDate.Substring(6, 2);
                                    if (mTempQty >= txtProductionQuantity.Value)
                                    {
                                        while (mTempQty >= txtProductionQuantity.Value)
                                        {
                                            dgProductionQuantityList.Rows.Add(txtProductionQuantity.Value, mSlashedProductionDate, (dgProductionQuantityList.Rows.Count + 1) * txtProductionQuantity.Value);
                                            mTempQty = (int)Math.Round(mTempQty - txtProductionQuantity.Value);
                                        }

                                        if (mTempQty > 0)
                                        {
                                            dgProductionQuantityList.Rows.Add(mTempQty, mSlashedProductionDate, dgProductionQuantityList.Rows.Count * txtProductionQuantity.Value + mTempQty);
                                        }
                                    }
                                    else
                                    {
                                        dgProductionQuantityList.Rows.Add(mTempQty, mSlashedProductionDate, dgProductionQuantityList.Rows.Count * txtProductionQuantity.Value + mTempQty);
                                    }
                                }
                            }
                        }
                        else if (rbProductionDate.Checked)
                        {
                            short mExecMethod = GetOpExecMethod(Conversions.ToString(drCurrentBatch[0]["TreeCode"]), mLastOperationCode);
                            cmProductionQty.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select IsNull(DetailProductionQuantity,0) As DetailProductionQuantity," + "PlanningStartDate As ProductionStartDate,PlanningStartHour As ProductionStartHour," + "PlanningEndDate As ProductionEndDate,PlanningEndHour As ProductionEndHour," + "SubbatchCode From Tbl_Planning Where TreeCode = ", drCurrentBatch[0]["TreeCode"]), " And SubbatchCode IN (Select SubbatchCode From Tbl_ProductionSubbatchs Where BatchCode = '"), drCurrentBatch[0]["BatchCode"]), "') And OperationCode = '"), mLastOperationCode), "' And PlanningEndDate >= "), txtProductionDate.Text));




                            sdrProductionQty = cmProductionQty.ExecuteReader();
                            while (sdrProductionQty.Read())
                            {
                                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectLessEqual(sdrProductionQty["ProductionStartDate"], txtProductionDate.Text, false)))
                                {
                                    string mStartDate = Constants.vbNullString;
                                    string mEndDate = txtProductionDate.Text;
                                    if (!string.IsNullOrEmpty(txtProductionFrom.Text))
                                    {
                                        mStartDate = txtProductionFrom.Text;
                                    }
                                    else
                                    {
                                        mStartDate = sdrProductionQty["ProductionStartDate"].ToString();
                                    }

                                    string mMachineCode = GetMachineCode(mExecMethod, Conversions.ToString(drCurrentBatch[0]["TreeCode"]), mLastOperationCode, Conversions.ToString(sdrProductionQty["SubbatchCode"]));
                                    string mCalendarCode = GetCalendarCode(mExecMethod, Conversions.ToString(drCurrentBatch[0]["TreeCode"]), mLastOperationCode, mMachineCode);
                                    double mDaysDuration = GetDaysDurationTime(mStartDate, mEndDate, Conversions.ToInteger(mCalendarCode));
                                    double mOneExecTime = GetOpOneExecTime(mMachineCode, Conversions.ToString(drCurrentBatch[0]["TreeCode"]), mLastOperationCode);
                                    double mCalendarStart = Conversions.ToDouble(Module1.GetFloatingHour(Module1.GetCalendarStart(mCalendarCode)));
                                    if (Conversions.ToDouble(sdrProductionQty["ProductionStartHour"]) > mCalendarStart && (txtProductionFrom.Text is null || txtProductionFrom.Text.Trim().Equals("")))
                                    {
                                        mDaysDuration -= Conversions.ToDouble(sdrProductionQty["ProductionStartHour"]) - mCalendarStart;
                                    }

                                    mTempQty += (int)Math.Round(Math.Round(mDaysDuration / mOneExecTime, MidpointRounding.ToEven));
                                }
                            }

                            sdrProductionQty.Close();
                            sdrProductionQty = null;
                            string mSlashedProductionDate = txtProductionDate.Text.Substring(0, 4) + "/" + txtProductionDate.Text.Substring(4, 2) + "/" + txtProductionDate.Text.Substring(6, 2);
                            dgProductionQuantityList.Rows.Add(mTempQty, mSlashedProductionDate, mTempQty);
                        }

                        Cursor = Cursors.Default;
                    }
                    catch (Exception objEx)
                    {
                        Logger.LogException("", objEx);
                        Cursor = Cursors.Default;
                        if (sdrProductionQty is object)
                        {
                            sdrProductionQty.Close();
                        }
                    }
                    finally
                    {
                        if (cn.State == ConnectionState.Open)
                            cn.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("بچی انتخاب نشده است", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }

        private double GetOpOneExecTime(string mMachineCode, string mTreeCode, string mOperationCode)
        {
            double mOneExecTime = 0.0d;
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmOneExecTime = new SqlCommand("Select IsNull(dbo.GetOperationExecutionTime('" + mMachineCode + "'," + mTreeCode + ",'" + mOperationCode + "'),0.0)", cn);
                mOneExecTime = Conversions.ToDouble(cmOneExecTime.ExecuteScalar());
                cn.Close();
            }

            return mOneExecTime;
        }

        private int GetMinTransferProductionQtyOnFirstSubbatch(string mBatchCode)
        {
            int MinTransferQty = 1;
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    var cmQTY = new SqlCommand("Select IsNull(TransferMinimumQuantity,1) From Tbl_ProductionSubbatchs Where SubbatchCode = '" + mBatchCode + "01'", cn);
                    MinTransferQty = Conversions.ToInteger(cmQTY.ExecuteScalar());
                }
                catch (Exception objEx)
                {
                    Logger.LogException("", objEx);
                    MinTransferQty = 1;
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }

            return Conversions.ToInteger(Interaction.IIf(MinTransferQty == 0, 1, MinTransferQty));
        }

        private void rbProductionQuantity_CheckedChanged(object sender, EventArgs e)
        {
            if (rbProductionQuantity.Checked)
            {
                txtProductionDate.Enabled = false;
                txtProductionQuantity.Enabled = true;
            }
            else
            {
                txtProductionQuantity.Enabled = false;
                txtProductionDate.Enabled = true;
            }
        }

        private void GetProductionDate(string mTreeCode, string mOperationCode, string mSubbatchCode, ref string mCurrentDate, ref string mCurrentHour, int mQuantity)
        {
            short mExecMethod = GetOpExecMethod(mTreeCode, mOperationCode);
            string mMachineCode = GetMachineCode(mExecMethod, mTreeCode, mOperationCode, mSubbatchCode);
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmProductionDate = new SqlCommand("Select dbo.GetOperationExecutionTime('" + mMachineCode + "'," + mTreeCode + ",'" + mOperationCode + "')", cn);
                double mOPOneExecTime = Conversions.ToDouble(cmProductionDate.ExecuteScalar());
                if (DBNull.Value.Equals(mOPOneExecTime))
                {
                    mOPOneExecTime = 0.0d;
                }

                double mExecDuration = mQuantity * mOPOneExecTime;
                string mCalendarCode = GetCalendarCode(mExecMethod, mTreeCode, mOperationCode, mMachineCode);
                if (mExecDuration > 0.0d)
                {
                    double argCurrentHour = Conversions.ToDouble(mCurrentHour);
                    AddDurationToDate(ref mCurrentDate, ref argCurrentHour, mCalendarCode, mExecDuration);
                }

                cn.Close();
            }
        }

        private short GetOpExecMethod(string mTreeCode, string mOperationCode)
        {
            short mExecMethod = 0;
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmProductionDate = new SqlCommand("Select IsNull(ExecutionMethod,0) From Tbl_ProductOPCs Where TreeCode = " + mTreeCode + " And OperationCode = '" + mOperationCode + "'", cn);
                mExecMethod = Conversions.ToShort(cmProductionDate.ExecuteScalar());
                cn.Close();
            }

            return mExecMethod;
        }

        private int GetProductionQuantityInDate(string mTreeCode, string mOperationCode, string mSubbatchCode, string mCurrentDate, string mCurrentHour, string mProductionDate)
        {
            return default;
            // Dim mProductionQuantity As Integer = 0

            // Using cn As New SqlConnection(PlanningCnnStr)
            // cn.Open()

            // Dim cmProductionDate As New SqlCommand("Select IsNull(ExecutionMethod,0) From Tbl_ProductOPCs Where TreeCode = " & mTreeCode & " And OperationCode = '" & mOperationCode & "'", cn)
            // Dim mExecMethod As Int16 = cmProductionDate.ExecuteScalar()
            // Dim mMachineCode As String = GetMachineCode(mExecMethod, mTreeCode, mOperationCode, mSubbatchCode)

            // cmProductionDate.CommandText = "Select dbo.GetOperationExecutionTime('" & mMachineCode & "'," & mTreeCode & ",'" & mOperationCode & "')"
            // Dim mOPOneExecTime As Double = cmProductionDate.ExecuteScalar()

            // If DBNull.Value.Equals(mOPOneExecTime) Then
            // mOPOneExecTime = 0.0
            // End If

            // Dim mExecDuration As Double = CDbl(mQuantity) * mOPOneExecTime
            // Dim mCalendarCode As String = GetCalendarCode(mExecMethod, mTreeCode, mOperationCode, mMachineCode)

            // If mExecDuration > 0.0 Then
            // AddDurationToDate(mCurrentDate, mCurrentHour, mCalendarCode, mExecDuration)
            // End If

            // cn.Close()
            // End Using

            // Return mProductionQuantity
        }

        private string GetMachineCode(short ExecMethod, string mTreeCode, string mOperationCode, string mSubbatchCode)
        {
            string MachineCode = "-1";
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmGetMachineCode = new SqlCommand("Select MachineCode From Tbl_Planning Where TreeCode=" + mTreeCode + " And SubbatchCode='" + mSubbatchCode + "' And OperationCode='" + mOperationCode + "'", cn);
                var drGetMachineCode = cmGetMachineCode.ExecuteReader();
                if (drGetMachineCode.Read())
                {
                    MachineCode = Conversions.ToString(drGetMachineCode["MachineCode"]);
                    drGetMachineCode.Close();
                }
                else
                {
                    drGetMachineCode.Close();
                    cmGetMachineCode.CommandText = "Select MachineCode From Tbl_ProductOPCsExecutorMachines Where TreeCode=" + mTreeCode + " And OperationCode='" + mOperationCode + "' Order By MachinePriority";
                    drGetMachineCode = cmGetMachineCode.ExecuteReader();
                    if (drGetMachineCode.Read())
                    {
                        MachineCode = Conversions.ToString(drGetMachineCode["MachineCode"]);
                    }

                    drGetMachineCode.Close();
                }

                cn.Close();
            }

            return MachineCode;
        }

        private void AddDurationToDate(ref string CurrentDate, ref double CurrentHour, string CalendarCode, double Duration)
        {
            switch (Module1.IsHoliday(CalendarCode, CurrentDate))
            {
                case true: // روز تعطیل می باشد
                    {
                        CurrentDate = FarsiDateFunctions.AddToDate(CurrentDate, "00000001");
                        AddDurationToDate(ref CurrentDate, ref CurrentHour, CalendarCode, Duration);
                        break;
                    }

                case false: // روز عادی می باشد
                    {
                        // بدست آوردن تعداد شیفتهای تقویم
                        short ShiftCount = GetCalendarShiftsCount(CalendarCode);
                        int HourCount;
                        double ShiftEndTime;
                        short ShiftCounter;
                        short HourCounter;
                        double AddedHour = 0d;
                        var loopTo = ShiftCount;
                        for (ShiftCounter = 1; ShiftCounter <= loopTo; ShiftCounter++)
                        {
                            var dtDownTimes = new DataTable();
                            GetShiftDownTimes(CalendarCode, ShiftCounter.ToString(), CurrentDate, ref dtDownTimes);
                            ShiftEndTime = GetShiftEnd(CalendarCode, ShiftCounter, CurrentDate);
                            // بدست آوردن تعداد ساعات، در مدت زمان درخواستی برای افزایش زمان
                            if (Duration.ToString().Contains("."))
                            {
                                HourCount = Conversions.ToShort(Strings.Mid(Duration.ToString(), 1, Duration.ToString().IndexOf(".")));
                            }
                            else
                            {
                                HourCount = (int)Math.Round(Duration);
                            }

                            HourCount = HourCount + (Duration - (double)HourCount == 0d ? 0: 1);
                            var loopTo1 = HourCount;
                            for (HourCounter = 1; HourCounter <= loopTo1; HourCounter++)
                            {
                                if (HourCounter == HourCount)
                                {
                                    AddedHour = Duration;
                                }
                                else
                                {
                                    AddedHour = 1d;
                                }

                                // کنترل اینکه ساعت محاسبه شده در وقت های استراحت نباشد
                                foreach (DataRow r in dtDownTimes.Rows)
                                {
                                    double DT_Start = Conversions.ToDouble(Module1.GetFloatingHour(r["DownTimeStart"].ToString()));
                                    double DT_End = Conversions.ToDouble(Module1.GetFloatingHour(r["DownTimeEnd"].ToString()));
                                    CheckFloating(ref DT_Start);
                                    CheckFloating(ref DT_End);
                                    if (CurrentHour + AddedHour >= DT_Start)
                                    {
                                        if (CurrentHour + AddedHour <= DT_End)
                                        {
                                            // Duration -= (DT_End - (CurrentHour + AddedHour))
                                            // CurrentHour = DT_End + GetAnyTime_TO_Hour(TimeTypes_enum.TT_MINUTE, 1, "0:0")

                                            Duration -= DT_Start - CurrentHour;
                                            AddedHour -= DT_Start - CurrentHour;
                                            CurrentHour = DT_End + Module1.GetAnyTime_TO_Hour((short)Module1.TimeTypes_enum.TT_MINUTE, 1d, "00:00");
                                            if (AddedHour == 0.0d)
                                            {
                                                goto NextFunctionCall;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }

                                if (CurrentHour + AddedHour < ShiftEndTime)
                                {
                                    CurrentHour = CurrentHour + AddedHour;
                                    Duration -= AddedHour;
                                }
                                else if (CurrentHour + AddedHour == ShiftEndTime)
                                {
                                    Duration -= AddedHour;
                                    if (ShiftCounter == ShiftCount)
                                    {
                                        if (Duration == 0.0d)
                                        {
                                            CurrentHour = ShiftEndTime;
                                        }
                                        else
                                        {
                                            CurrentDate = FarsiDateFunctions.AddToDate(CurrentDate, "00000001");
                                            while (Module1.IsHoliday(CalendarCode, CurrentDate))
                                                CurrentDate = FarsiDateFunctions.AddToDate(CurrentDate, "00000001");
                                            CurrentHour = GetShiftStart(CalendarCode, 1, CurrentDate);
                                        }
                                    }
                                    else
                                    {
                                        CurrentHour = GetShiftStart(CalendarCode, (short)(ShiftCounter + 1), CurrentDate);
                                    }

                                    break;
                                }
                                else if (CurrentHour + AddedHour > ShiftEndTime) // در صورتیکه از طول روز بیشتر باشد
                                {
                                    Duration -= ShiftEndTime - CurrentHour;
                                    if (ShiftCounter == ShiftCount)
                                    {
                                        CurrentDate = FarsiDateFunctions.AddToDate(CurrentDate, "00000001");
                                        while (Module1.IsHoliday(CalendarCode, CurrentDate))
                                            CurrentDate = FarsiDateFunctions.AddToDate(CurrentDate, "00000001");
                                        CurrentHour = GetShiftStart(CalendarCode, 1, CurrentDate);
                                    }
                                    else
                                    {
                                        CurrentHour = GetShiftStart(CalendarCode, (short)(ShiftCounter + 1), CurrentDate);
                                    }

                                    break;
                                }
                            }

                            if (Duration == 0.0d || Duration < 0.0d)
                            {
                                return;
                            }
                        }

                    NextFunctionCall:
                        ;
                        if (Duration == 0.0d || Duration < 0.0d)
                        {
                            return;
                        }
                        else
                        {
                            AddDurationToDate(ref CurrentDate, ref CurrentHour, CalendarCode, Duration);
                        }

                        break;
                    }
            }
        }

        private short GetCalendarShiftsCount(string mCalendarCode)
        {
            short mShiftCount = 0;
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmShiftCount = new SqlCommand("Select ShiftCount From Tbl_Calendar Where CalendarCode = " + mCalendarCode, cn);
                mShiftCount = Conversions.ToShort(cmShiftCount.ExecuteScalar());
                cn.Close();
            }

            return mShiftCount;
        }

        private void GetShiftDownTimes(string CalendarCode, string ShiftNo, string ShamsiDate, ref DataTable dtDownTimes)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                var da = new SqlDataAdapter("", cn);
                switch (Module1.IsParticularDay(CalendarCode, ShamsiDate))
                {
                    case -1: // تاریخ داده یک روز پیش فرض می باشد
                        {
                            da.SelectCommand.CommandText = "Select DownTimeStart,DownTimeEnd From Tbl_CalendarShiftDownTimes Where CalendarCode=" + CalendarCode + " And ShiftNo=" + ShiftNo;
                            break;
                        }

                    case 2: // تاریخ داده شده یک روز خاص عادی می باشد
                        {
                            da.SelectCommand.CommandText = "Select DownTimeStart,DownTimeEnd From Tbl_ParticularShiftDownTimes Where CalendarCode=" + CalendarCode + " And ShamsiDate=" + ShamsiDate + " And ShiftNo=" + ShiftNo;
                            break;
                        }
                }

                da.Fill(dtDownTimes);
            }
        }

        private double GetShiftStart(string CalendarCode, short ShiftNo, string ShamsiDate)
        {
            var ShiftStart = default(double);
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmShiftStart = new SqlCommand("", cn);
                SqlDataReader drShiftStart = null;
                switch (Module1.IsParticularDay(CalendarCode, ShamsiDate))
                {
                    case -1: // تاریخ داده یک روز پیش فرض می باشد
                        {
                            cmShiftStart.CommandText = "Select ShiftStart From Tbl_CalendarShifts Where CalendarCode=" + CalendarCode + " And ShiftNo=" + ShiftNo;
                            break;
                        }

                    case 2: // تاریخ داده شده یک روز خاص عادی می باشد
                        {
                            cmShiftStart.CommandText = "Select ShiftStart From Tbl_CalendarParticularShifts Where CalendarCode=" + CalendarCode + " And ShamsiDate=" + ShamsiDate + " And ShiftNo=" + ShiftNo;
                            break;
                        }
                }

                drShiftStart = cmShiftStart.ExecuteReader();
                if (drShiftStart.Read())
                {
                    var mShiftStartTime = TimeSpan.Parse(drShiftStart["ShiftStart"].ToString());
                    string mHour = Conversions.ToString(Interaction.IIf(mShiftStartTime.Hours < 10, "0" + mShiftStartTime.Hours.ToString(), mShiftStartTime.Hours.ToString()));
                    string mMinute = Conversions.ToString(Interaction.IIf(mShiftStartTime.Minutes < 10, "0" + mShiftStartTime.Minutes.ToString(), mShiftStartTime.Minutes.ToString()));
                    ShiftStart = Conversions.ToDouble(Module1.GetFloatingHour(mHour + ":" + mMinute));
                }

                drShiftStart.Close();
                cn.Close();
            }

            return ShiftStart;
        }

        private double GetShiftEnd(string CalendarCode, short ShiftNo, string ShamsiDate)
        {
            var ShiftEnd = default(double);
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmShiftEnd = new SqlCommand("", cn);
                SqlDataReader drShiftEnd = null;
                switch (Module1.IsParticularDay(CalendarCode, ShamsiDate))
                {
                    case -1: // تاریخ داده یک روز پیش فرض می باشد
                        {
                            cmShiftEnd.CommandText = "Select ShiftStart,ShiftDuration,ShiftExtraTime From Tbl_CalendarShifts Where CalendarCode=" + CalendarCode + " And ShiftNo=" + ShiftNo;
                            break;
                        }

                    case 2: // تاریخ داده شده یک روز خاص عادی می باشد
                        {
                            cmShiftEnd.CommandText = "Select ShiftStart,ShiftDuration,ShiftExtraTime From Tbl_CalendarParticularShifts Where CalendarCode=" + CalendarCode + " And ShamsiDate=" + ShamsiDate + " And ShiftNo=" + ShiftNo;
                            break;
                        }
                }

                drShiftEnd = cmShiftEnd.ExecuteReader();
                if (drShiftEnd.Read())
                {
                    var mShiftEndTime = TimeSpan.Parse(drShiftEnd["ShiftStart"].ToString()) + TimeSpan.Parse(drShiftEnd["ShiftDuration"].ToString()) + TimeSpan.Parse(drShiftEnd["ShiftExtraTime"].ToString());
                    if (mShiftEndTime > TimeSpan.Parse("1.00:00"))
                    {
                        mShiftEndTime -= TimeSpan.Parse("1.00:00");
                    }

                    if (mShiftEndTime == TimeSpan.Parse("1.00:00"))
                    {
                        mShiftEndTime = TimeSpan.Parse("23:59");
                    }

                    string mHour = Conversions.ToString(Interaction.IIf(mShiftEndTime.Hours < 10, "0" + mShiftEndTime.Hours.ToString(), mShiftEndTime.Hours.ToString()));
                    string mMinute = Conversions.ToString(Interaction.IIf(mShiftEndTime.Minutes < 10, "0" + mShiftEndTime.Minutes.ToString(), mShiftEndTime.Minutes.ToString()));
                    ShiftEnd = Conversions.ToDouble(Module1.GetFloatingHour(mHour + ":" + mMinute));
                }

                drShiftEnd.Close();
                cn.Close();
            }

            return ShiftEnd;
        }

        private string GetCalendarCode(short ExecutionMethod, string mTreecode, string mOperationCode, string MachineCode = "-1")
        {
            string mCalendarCode = 0.ToString();
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmCalendarCode = new SqlCommand("", cn);
                SqlDataReader drCalendarCode = null;
                switch ((EnumExecutionMethod)ExecutionMethod)
                {
                    case EnumExecutionMethod.EM_MACHINE:
                        {
                            cmCalendarCode.CommandText = "Select CalendarCode From Tbl_Machines Where Code = '" + MachineCode + "'";
                            break;
                        }

                    case EnumExecutionMethod.EM_NONMACHINE:
                        {
                            cmCalendarCode.CommandText = "Select CalendarCode From Tbl_ProductOPCsExecutorMachines Where TreeCode=" + mTreecode + " And OperationCode='" + mOperationCode + "'";
                            break;
                        }

                    case EnumExecutionMethod.EM_CONTRACTOR:
                        {
                            cmCalendarCode.CommandText = "Select CalendarCode From Tbl_ContractorOperations Where TreeCode=" + mTreecode + " And OperationCode='" + mOperationCode + "'";
                            break;
                        }
                }

                drCalendarCode = cmCalendarCode.ExecuteReader();
                if (drCalendarCode.Read())
                {
                    mCalendarCode = Conversions.ToString(drCalendarCode["CalendarCode"]);
                }

                drCalendarCode.Close();
                cn.Close();
            }

            return mCalendarCode;
        }

        private void CheckFloating(ref double dblHour)
        {
            if (!dblHour.ToString().Contains("."))
            {
                dblHour = Conversions.ToDouble(dblHour.ToString() + ".0");
            }
        }
        /* TODO ERROR: Skipped EndRegionDirectiveTrivia */
    }
}