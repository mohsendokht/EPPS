using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmSelectMatchedOperations
    {
        public frmSelectMatchedOperations()
        {
            InitializeComponent();
            _TreeView1.Name = "TreeView1";
            _cmdOK.Name = "cmdOK";
            _cmdReloadTree.Name = "cmdReloadTree";
        }

        private string mTreeCode = Constants.vbNullString;
        private string mOperationCode = Constants.vbNullString;
        private DataTable dtMatchedOperations;
        private string AddedOperations = Constants.vbNullString;

        public string TreeCode
        {
            set
            {
                mTreeCode = value;
            }
        }

        public string OperationCode
        {
            set
            {
                mOperationCode = value;
            }
        }

        public object MatchedOperationsTable
        {
            set
            {
                dtMatchedOperations = (DataTable)value;
            }
        }

        private void frmSelectMatchedOperations_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdCancel, 14);
            Module1.SetButtonsImage(cmdOK, 19);
            Module1.SetButtonsImage(cmdReloadTree, 18);
            LoadMatchedOperationTree();
            TreeView1.ExpandAll();
            Text += " {" + mOperationCode + "}";
        }

        private void cmdReloadTree_Click(object sender, EventArgs e)
        {
            LoadMatchedOperationTree();
            TreeView1.ExpandAll();
        }

        private void TreeView1_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Tag is object && e.Node.Tag.ToString().Contains("dtl"))
            {
                e.Cancel = true;
            }
        }

        private void TreeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is object && e.Node.Checked)
            {
                string mOpCode = e.Node.Tag.ToString().Split(':')[1];
                string mPreOps = GetPreOperations(mTreeCode, mOpCode);
                string mAfterOps = GetAfterOperations(mTreeCode, mOpCode);
                if (!string.IsNullOrEmpty(mPreOps))
                {
                    CheckOperationsChckedState(TreeView1.Nodes, mPreOps, mOpCode);
                }

                if (!string.IsNullOrEmpty(mAfterOps))
                {
                    CheckOperationsChckedState(TreeView1.Nodes, mAfterOps, mOpCode);
                }
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            var mDR = dtMatchedOperations.Select("OperationCode = '" + mOperationCode + "' Or MatchedOperationCode = '" + mOperationCode + "'");
            for (int I = mDR.Length - 1; I >= 0; I -= 1)
                mDR[I].Delete();
            AddMatchedOperations(TreeView1.Nodes);
        }

        private void LoadMatchedOperationTree()
        {
            try
            {
                AddedOperations = Constants.vbNullString;
                using (var cn = new SqlConnection(Module1.PlanningCnnStr))
                {
                    cn.Open();
                    var cmMatchedOperation = new SqlCommand("Select DetailCode, DetailName From Tbl_ProductTreeDetails Where TreeCode = " + mTreeCode + " And ParentDetailCode = '0'", cn);
                    var drMatchedOperation = cmMatchedOperation.ExecuteReader();
                    TreeView1.Nodes.Clear();
                    while (drMatchedOperation.Read())
                    {
                        var tn = new TreeNode(Conversions.ToString(drMatchedOperation["DetailName"]));
                        tn.Tag = Operators.ConcatenateObject("dtl:", drMatchedOperation["DetailCode"]);
                        tn.ForeColor = Color.Blue;
                        TreeView1.Nodes.Add(tn);
                        AddDetailOperationsToNode(tn, mTreeCode);
                        AddChildDetails(Conversions.ToString(drMatchedOperation["DetailCode"]), mTreeCode, TreeView1.Nodes[TreeView1.Nodes.Count - 1]);
                    }

                    drMatchedOperation.Close();
                    cn.Close();
                }
            }
            catch (Exception objEx)
            {
                Logger.SaveError(Name + ".LoadMatchedOperationTree", objEx.Message);
                MessageBox.Show("نمایش لیست عملیات ها با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }

        private void AddChildDetails(string mDetailCode, string mTreeCode, TreeNode tnParent)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmOverProduction = new SqlCommand("Select DetailCode,DetailName From Tbl_ProductTreeDetails Where TreeCode = " + mTreeCode + " And ParentDetailCode = '" + mDetailCode + "'", cn);
                var drOverProduction = cmOverProduction.ExecuteReader();
                while (drOverProduction.Read())
                {
                    var tn = new TreeNode(Conversions.ToString(drOverProduction["DetailName"]));
                    tn.Tag = Operators.ConcatenateObject("dtl:", drOverProduction["DetailCode"]);
                    tn.ForeColor = Color.Blue;
                    tnParent.Nodes.Add(tn);
                    AddDetailOperationsToNode(tn, mTreeCode);
                    AddChildDetails(Conversions.ToString(drOverProduction["DetailCode"]), mTreeCode, tn);
                }

                drOverProduction.Close();
                cn.Close();
            }
        }

        private void AddDetailOperationsToNode(TreeNode tnParent, string mTreeCode)
        {
            AddedOperations = Constants.vbNullString;
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
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
                            AddOperationToNode(mTreeCode, Conversions.ToString(drDetailOperations["OperationCode"]), Conversions.ToString(drDetailOperations["OperationTitle"]), tnParent);
                        }
                    }
                    else
                    {
                        AddOperationToNode(mTreeCode, Conversions.ToString(drDetailOperations["OperationCode"]), Conversions.ToString(drDetailOperations["OperationTitle"]), tnParent);
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
                            AddOperationToNode(mTreeCode, Conversions.ToString(drDetailOperations["OperationCode"]), Conversions.ToString(drDetailOperations["OperationTitle"]), tnParent);
                        }
                    }
                    else
                    {
                        AddOperationToNode(mTreeCode, Conversions.ToString(drDetailOperations["OperationCode"]), Conversions.ToString(drDetailOperations["OperationTitle"]), tnParent);
                    }
                }

                drDetailOperations.Close();
                cn.Close();
            }
        }

        private void AddAfterwardOperations(string TreeCode, string DetailCode, string PreOperationCode, TreeNode tnParent)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmHasAfterward = new SqlCommand("Select A.CurrentOperationCode As OperationCode,B.OperationTitle From Tbl_PreOperations A Inner Join Tbl_ProductOPCs B ON A.TreeCode = B.TreeCode And A.CurrentOperationCode = B.OperationCode Where A.TreeCode = " + TreeCode + " And A.PreOperationCode = '" + PreOperationCode + "' And A.CurrentOperationCode IN (Select OperationCode From Tbl_ProductOPCs Where TreeCode = " + TreeCode + " And DetailCode = '" + DetailCode + "')", cn);
                var drHasAfterward = cmHasAfterward.ExecuteReader();
                if (drHasAfterward.Read())
                {
                    if (AddedOperations is null || !AddedOperations.Contains(Conversions.ToString(drHasAfterward["OperationCode"])))
                    {
                        AddOperationToNode(TreeCode, Conversions.ToString(drHasAfterward["OperationCode"]), Conversions.ToString(drHasAfterward["OperationTitle"]), tnParent);
                    }
                }

                cn.Close();
            }
        }

        private void AddOperationToNode(string mTreeCode, string mOpCode, string mOperationTitle, TreeNode tnParent)
        {
            if (!mOpCode.Equals(mOperationCode))
            {
                var tn = new TreeNode("{" + mOpCode + "} " + mOperationTitle);
                tn.Tag = "opr:" + mOpCode;
                tn.ForeColor = Color.Green;
                if (CheckHasMatchedOperation(mOpCode))
                {
                    tn.Checked = true;
                }

                tnParent.Nodes.Add(tn);
                AddedOperations = Conversions.ToString(AddedOperations + Interaction.IIf(string.IsNullOrEmpty(AddedOperations), "'" + mOpCode + "'", ",'" + mOpCode + "'"));
            }

            AddAfterwardOperations(mTreeCode, tnParent.Tag.ToString().Split(':')[1], mOpCode, tnParent);
        }

        private bool CheckHasMatchedOperation(string mMatchOperationCode)
        {
            var mDR = dtMatchedOperations.Select("OperationCode = '" + mOperationCode + "' And MatchedOperationCode = '" + mMatchOperationCode + "'");
            if (mDR.Length == 0 || mDR[0].RowState == DataRowState.Deleted)
            {
                mDR = dtMatchedOperations.Select("OperationCode = '" + mMatchOperationCode + "' And MatchedOperationCode = '" + mOperationCode + "'");
                if (mDR.Length == 0 || mDR[0].RowState == DataRowState.Deleted)
                {
                    return false;
                }
            }

            return true;
        }

        private void AddMatchedOperations(TreeNodeCollection mNodes)
        {
            foreach (TreeNode tn in mNodes)
            {
                if (tn.Tag is object && tn.Tag.ToString().Contains("opr") && tn.Checked)
                {
                    string mMatchedCode = tn.Tag.ToString().Split(':')[1];
                    var mNewMatchedRow = dtMatchedOperations.NewRow();
                    mNewMatchedRow["TreeCode"] = mTreeCode;
                    mNewMatchedRow["OperationCode"] = mOperationCode;
                    mNewMatchedRow["MatchedOperationCode"] = mMatchedCode;
                    dtMatchedOperations.Rows.Add(mNewMatchedRow);
                }

                AddMatchedOperations(tn.Nodes);
            }
        }

        private string GetPreOperations(string TreeCode, string OperationCode)
        {
            string mPreOperations = Constants.vbNullString;
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmPreOps = new SqlCommand("Select PreOperationCode From Tbl_PreOperations Where TreeCode = " + TreeCode + " And CurrentOperationCode = '" + OperationCode + "'", cn);
                var drPreOps = cmPreOps.ExecuteReader();
                while (drPreOps.Read())
                {
                    if (!string.IsNullOrEmpty(mPreOperations))
                    {
                        if (!drPreOps["PreOperationCode"].ToString().Equals(OperationCode) && !mPreOperations.Contains(drPreOps["PreOperationCode"].ToString()))
                        {
                            mPreOperations += ",'" + drPreOps["PreOperationCode"].ToString() + "'";
                        }
                    }
                    else
                    {
                        mPreOperations += "'" + drPreOps["PreOperationCode"].ToString() + "'";
                    }

                    string mPreOps = GetPreOperations(TreeCode, drPreOps["PreOperationCode"].ToString());
                    if (!string.IsNullOrEmpty(mPreOps))
                    {
                        mPreOperations = Conversions.ToString(mPreOperations + Interaction.IIf(string.IsNullOrEmpty(mPreOperations), mPreOps, "," + mPreOps));
                    }
                }

                cn.Close();
            }

            return mPreOperations;
        }

        private string GetAfterOperations(string TreeCode, string OperationCode)
        {
            string mAfterOperations = Constants.vbNullString;
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmAfterOps = new SqlCommand("Select CurrentOperationCode From Tbl_PreOperations Where TreeCode = " + TreeCode + " And PreOperationCode = '" + OperationCode + "'", cn);
                var drAfterOps = cmAfterOps.ExecuteReader();
                while (drAfterOps.Read())
                {
                    if (!string.IsNullOrEmpty(mAfterOperations))
                    {
                        if (!drAfterOps["CurrentOperationCode"].ToString().Equals(OperationCode) && !mAfterOperations.Contains(drAfterOps["CurrentOperationCode"].ToString()))
                        {
                            mAfterOperations += ",'" + drAfterOps["CurrentOperationCode"].ToString() + "'";
                        }
                    }
                    else
                    {
                        mAfterOperations = "'" + drAfterOps["CurrentOperationCode"].ToString() + "'";
                    }

                    string mAfterOps = GetPreOperations(TreeCode, drAfterOps["CurrentOperationCode"].ToString());
                    if (!string.IsNullOrEmpty(mAfterOps))
                    {
                        mAfterOperations = Conversions.ToString(mAfterOperations + Interaction.IIf(string.IsNullOrEmpty(mAfterOperations), mAfterOps, "," + mAfterOps));
                    }
                }

                cn.Close();
            }

            return mAfterOperations;
        }

        private void CheckOperationsChckedState(TreeNodeCollection mNodes, string mOperationCodes, string mBaseOPCode)
        {
            foreach (TreeNode tn in mNodes)
            {
                if (tn.Tag is object && tn.Tag.ToString().Contains("opr"))
                {
                    string mOpCode = tn.Tag.ToString().Split(':')[1];
                    if (mOperationCodes.Contains(mOpCode) && tn.Checked && !mOpCode.Equals(mBaseOPCode))
                    {
                        tn.Checked = false;
                    }
                }

                CheckOperationsChckedState(tn.Nodes, mOperationCodes, mBaseOPCode);
            }
        }
    }
}