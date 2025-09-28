using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmSubbatchPlanningStop
    {
        public frmSubbatchPlanningStop()
        {
            InitializeComponent();
            _TreeView1.Name = "TreeView1";
            _cmdOK.Name = "cmdOK";
            _cmdReloadTree.Name = "cmdReloadTree";
        }

        private string mTreeCode = Constants.vbNullString;
        private string mSubbatchCode = Constants.vbNullString;
        private DataSet dsStoping = new DataSet();
        private string AddedOperations = Constants.vbNullString;
        private string mCheckedOperations = Constants.vbNullString;
        private bool ControlCheckState = false;
        private bool mIsCheckedFixNodes = false;
        private ArrayList mFixNodesArray = new ArrayList();

        public string TreeCode
        {
            set
            {
                mTreeCode = value;
            }
        }

        public string SubbatchCode
        {
            set
            {
                mSubbatchCode = value;
            }
        }

        public string CheckedOperations
        {
            get
            {
                return mCheckedOperations;
            }
        }

        private void frmSubbatchPlanningStop_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdCancel, 14);
            Module1.SetButtonsImage(cmdOK, 19);
            Module1.SetButtonsImage(cmdReloadTree, 18);
            {
                var withBlock = new SqlDataAdapter("Select * From Tbl_RealProduction Where SubbatchCode = '" + mSubbatchCode + "'", Module1.cnProductionPlanning);
                withBlock.Fill(dsStoping, "Tbl_RealProduction");
                withBlock.SelectCommand.CommandText = "Select A.TreeCode, A.DetailCode, A.OperationCode, A.OperationTitle, B.PreOperationCode,(Select DetailCode From dbo.Tbl_ProductOPCs Where TreeCode = " + mTreeCode + " And OperationCode = B.PreOperationCode) As PreDetailCode " + "From   dbo.Tbl_ProductOPCs A Left Outer Join dbo.Tbl_PreOperations B ON " + "       A.TreeCode = B.TreeCode And A.OperationCode = B.CurrentOperationCode Where A.TreeCode = " + mTreeCode;

                withBlock.Fill(dsStoping, "Tbl_PreOperations");
                withBlock.SelectCommand.CommandText = "Select * From Tbl_ProductTreeDetails Where TreeCode = " + mTreeCode;
                withBlock.Fill(dsStoping, "Tbl_ProductTreeDetails");
            }

            LoadOperationTree();
            TreeView1.ExpandAll();
            Text += " {" + mSubbatchCode + "}";
        }

        private void frmSubbatchPlanningStop_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int I = dsStoping.Tables.Count - 1; I <= 0; I++)
            {
                dsStoping.Tables[I].Dispose();
                dsStoping.Tables.RemoveAt(I);
            }

            dsStoping.Dispose();
        }

        private void cmdReloadTree_Click(object sender, EventArgs e)
        {
            LoadOperationTree();
            TreeView1.ExpandAll();
        }

        private void TreeView1_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Tag.ToString().Contains("dtl"))
            {
                var drDetailOperations = dsStoping.Tables["Tbl_PreOperations"].Select("DetailCode = '" + e.Node.Tag.ToString().Split(':')[1] + "'");
                string mOps = Constants.vbNullString;
                foreach (DataRow r in drDetailOperations)
                {
                    if (string.IsNullOrEmpty(mOps))
                    {
                        mOps = "'" + r["Operationcode"].ToString() + "'";
                    }
                    else
                    {
                        mOps += ",'" + r["Operationcode"].ToString() + "'";
                    }
                }

                if (!string.IsNullOrEmpty(mOps))
                {
                    CheckOperationsChckedState(e.Node.Nodes, mOps, "", !e.Node.Checked);
                }
            }
            // e.Cancel = True
            else if (e.Node.Tag.ToString().Contains("(fix)") && e.Node.Checked)
            {
                e.Cancel = true;
            }
        }

        private void TreeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (!ControlCheckState)
            {
                if (e.Node.Tag is object && e.Node.Checked)
                {
                    ControlCheckState = true;
                    mIsCheckedFixNodes = e.Node.Tag.ToString().Contains("(fix)");
                    string mOpCode = e.Node.Tag.ToString().Split(':')[1];
                    string mPreOps = GetPreOperations(mTreeCode, mOpCode);
                    if (!string.IsNullOrEmpty(mPreOps))
                    {
                        CheckOperationsChckedState(TreeView1.Nodes, mPreOps, mOpCode, e.Node.Checked);
                    }

                    ControlCheckState = false;
                }
                else if (!e.Node.Checked)
                {
                }
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            GetCheckedOperationPlanning(TreeView1.Nodes);
            if (!string.IsNullOrEmpty(mCheckedOperations))
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void LoadOperationTree()
        {
            try
            {
                AddedOperations = Constants.vbNullString;
                mFixNodesArray = new ArrayList();
                var mDetailRows = dsStoping.Tables["Tbl_ProductTreeDetails"].Select("ParentDetailCode = '0'");
                TreeView1.Nodes.Clear();
                foreach (DataRow r in mDetailRows)
                {
                    var tn = new TreeNode(r["DetailName"].ToString());
                    tn.Tag = "dtl:" + r["DetailCode"].ToString();
                    tn.ForeColor = Color.Blue;
                    TreeView1.Nodes.Add(tn);
                    AddDetailOperationsToNode(tn, mTreeCode);
                    AddChildDetails(Conversions.ToString(r["DetailCode"]), mTreeCode, tn);
                }

                foreach (object tn in mFixNodesArray)
                    ((TreeNode)tn).Checked = true;
            }
            catch (Exception objEx)
            {
                Logger.SaveError(Name + ".LoadOperationTree", objEx.Message);
                MessageBox.Show("نمایش لیست عملیات ها با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }

        private void AddChildDetails(string mDetailCode, string mTreeCode, TreeNode tnParent)
        {
            var mDetailrows = dsStoping.Tables["Tbl_ProductTreeDetails"].Select("ParentDetailCode = '" + mDetailCode + "'");
            foreach (DataRow r in mDetailrows)
            {
                var tn = new TreeNode(r["DetailName"].ToString());
                tn.Tag = "dtl:" + r["DetailCode"].ToString();
                tn.ForeColor = Color.Blue;
                tnParent.Nodes.Add(tn);
                AddDetailOperationsToNode(tn, mTreeCode);
                AddChildDetails(Conversions.ToString(r["DetailCode"]), mTreeCode, tn);
            }
        }

        private void AddDetailOperationsToNode(TreeNode tnParent, string mTreeCode)
        {
            string mDetailCode = tnParent.Tag.ToString().Split(':')[1];
            // فراخوانی عملیات هایی که پیشنیاز آنها در جزء جاری وجود ندارد و در اجزاء دیگر درخت می باشد
            var mDetailOperation = dsStoping.Tables["Tbl_PreOperations"].Select("DetailCode = '" + mDetailCode + "' And PreDetailCode <> '" + mDetailCode + "'");
            foreach (DataRow r in mDetailOperation)
            {
                if (AddedOperations is object)
                {
                    if (!AddedOperations.Contains(Conversions.ToString(r["OperationCode"])))
                    {
                        AddOperationToNode(mTreeCode, Conversions.ToString(r["OperationCode"]), Conversions.ToString(r["OperationTitle"]), tnParent);
                    }
                }
                else
                {
                    AddOperationToNode(mTreeCode, Conversions.ToString(r["OperationCode"]), Conversions.ToString(r["OperationTitle"]), tnParent);
                }
            }

            // فراخوانی لیست عملیات هایی که دارای پیش نیاز نیستند
            mDetailOperation = dsStoping.Tables["Tbl_PreOperations"].Select("DetailCode = '" + mDetailCode + "' And PreOperationCode Is Null");
            foreach (DataRow r in mDetailOperation)
            {
                if (AddedOperations is object)
                {
                    if (!AddedOperations.Contains(Conversions.ToString(r["OperationCode"])))
                    {
                        AddOperationToNode(mTreeCode, Conversions.ToString(r["OperationCode"]), Conversions.ToString(r["OperationTitle"]), tnParent);
                    }
                }
                else
                {
                    AddOperationToNode(mTreeCode, Conversions.ToString(r["OperationCode"]), Conversions.ToString(r["OperationTitle"]), tnParent);
                }
            }
        }

        private void AddAfterwardOperations(string TreeCode, string DetailCode, string PreOperationCode, TreeNode tnParent)
        {
            var mAfterwardOperation = dsStoping.Tables["Tbl_PreOperations"].Select("PreOperationCode = '" + PreOperationCode + "'");
            if (mAfterwardOperation.Length > 0)
            {
                if (AddedOperations is null || !AddedOperations.Contains(Conversions.ToString(mAfterwardOperation[0]["OperationCode"])))
                {
                    AddOperationToNode(TreeCode, Conversions.ToString(mAfterwardOperation[0]["OperationCode"]), Conversions.ToString(mAfterwardOperation[0]["OperationTitle"]), tnParent);
                }
            }
        }

        private void AddOperationToNode(string mTreeCode, string mOpCode, string mOperationTitle, TreeNode tnParent)
        {
            var tn = new TreeNode("{" + mOpCode + "} " + mOperationTitle);
            tn.Tag = "opr:" + mOpCode;
            tn.ForeColor = Color.Green;
            if (CheckHasOperationProduction(mOpCode))
            {
                tn.Tag = "opr(fix):" + mOpCode;
                tn.ForeColor = Color.Red;
                mFixNodesArray.Add(tn);
            }

            tnParent.Nodes.Add(tn);
            AddedOperations = Conversions.ToString(AddedOperations + Interaction.IIf(string.IsNullOrEmpty(AddedOperations), "'" + mOpCode + "'", ",'" + mOpCode + "'"));
            AddAfterwardOperations(mTreeCode, tnParent.Tag.ToString().Split(':')[1], mOpCode, tnParent);
        }

        private bool CheckHasOperationProduction(string mOperationCode)
        {
            return dsStoping.Tables["Tbl_RealProduction"].Select("OperationCode = '" + mOperationCode + "'").Length > 0;
        }

        private void GetCheckedOperationPlanning(TreeNodeCollection mNodes)
        {
            foreach (TreeNode tn in mNodes)
            {
                if (tn.Tag is object && tn.Tag.ToString().Contains("opr") && tn.Checked)
                {
                    string mCheckedOperationCode = tn.Tag.ToString().Split(':')[1];
                    if (string.IsNullOrEmpty(mCheckedOperations))
                    {
                        mCheckedOperations = "'" + mCheckedOperationCode + "'";
                    }
                    else
                    {
                        mCheckedOperations += ",'" + mCheckedOperationCode + "'";
                    }
                }

                GetCheckedOperationPlanning(tn.Nodes);
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

        private void CheckOperationsChckedState(TreeNodeCollection mNodes, string mOperationCodes, string mBaseOPCode, bool CheckedState)
        {
            foreach (TreeNode tn in mNodes)
            {
                if (tn.Tag is object && tn.Tag.ToString().Contains("opr"))
                {
                    string mOpCode = tn.Tag.ToString().Split(':')[1];
                    if (mOperationCodes.Contains(mOpCode))
                    {
                        if (mIsCheckedFixNodes)
                        {
                            tn.ForeColor = Color.Red;
                            tn.Tag = tn.Tag.ToString().Split(':')[0] + "(fix):" + mOpCode;
                        }

                        tn.Checked = CheckedState;
                        if (!CheckedState && tn.Tag.ToString().Contains("(fix)"))
                        {
                            tn.Checked = true;
                        }
                    }
                }

                CheckOperationsChckedState(tn.Nodes, mOperationCodes, mBaseOPCode, CheckedState);
            }
        }
    }
}