using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class UserChangeAccess
    {
        // Define a local variable to store the property value.
        //private int OPTypeValue;
        //private string UserCodeValue;
        // Define the property.
        // Public Property OPType() As Integer
        // Get
        // ' The Get property procedure is called when the value
        // ' of a property is retrieved.
        // Return OPTypeValue
        // End Get
        // Set(ByVal value As Integer)
        // ' The Set property procedure is called when the value 
        // ' of a property is modified.  The value to be assigned
        // ' is passed in the argument to Set.
        // OPTypeValue = value
        // End Set
        // End Property

        // Public Property UserCode() As String
        // Get
        // Return UserCodeValue
        // End Get
        // Set(ByVal value As String)
        // UserCodeValue = value
        // End Set
        // End Property

        private SqlConnection mycnn = new SqlConnection(Module1.UMCnnStr);
        private SqlDataAdapter objdataadapter = new SqlDataAdapter();
        private SqlCommand objcommand;
        private DataSet objdataset;
        private DataView objdataview;
        public int OldUserCode;

        private void tree(int softwarecode)
        {
            var dt_RootNodes = new DataTable();
            var cmd1 = new SqlCommand();
            var cmd2 = new SqlCommand();
            string ItemCode;
            string FarsiTitle;
            // Dim CurNode As New TreeNode
            TreeView1.Nodes.Clear();
            cmd1.CommandText = "SELECT ItemCode, FarsiTitle FROM  SoftwareItems WHERE (ParentCode='0') and (SoftwareCode=" + Module_UserAccess.MySoftwareCode + ")";
            cmd1.Connection = mycnn;
            objdataadapter.SelectCommand = cmd1;
            mycnn.Open();
            objdataadapter.Fill(dt_RootNodes);
            DataRow DataRowTmp;
            int i;
            TreeNode Cur_Node;
            var loopTo = dt_RootNodes.Rows.Count - 1;
            for (i = 0; i <= loopTo; i++)
            {
                DataRowTmp = dt_RootNodes.Rows[i];
                ItemCode = Conversions.ToString(DataRowTmp[0]);
                FarsiTitle = DataRowTmp[1].ToString();
                Cur_Node = TreeView1.Nodes.Add(ItemCode, FarsiTitle);
                TreeView1.Nodes[ItemCode].Tag = ItemCode.ToString();
                AddChiled_CurrentNode(Cur_Node, softwarecode);
            }

            if (TreeView1.Nodes.Count > 0)
            {
                TreeView1.SelectedNode = TreeView1.Nodes[0];
            }

            mycnn.Close();
        }

        private void AddChiled_CurrentNode(TreeNode MyParentNode, int SoftwareCode)
        {
            TreeNode Cur_NodeTmp;
            var cmd2 = new SqlCommand();
            string ItemCode;
            string FarsiTitle;
            cmd2.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("SELECT ItemCode, FarsiTitle FROM SoftwareItems WHERE(ParentCode =", MyParentNode.Tag), ") and (SoftwareCode="), SoftwareCode), ")"));
            cmd2.Connection = mycnn;
            var objdataadapter1 = new SqlDataAdapter(cmd2);
            var dt_RootNodes = new DataTable();
            objdataadapter1.Fill(dt_RootNodes);
            DataRow DataRowTmp;
            int i;
            var loopTo = dt_RootNodes.Rows.Count - 1;
            for (i = 0; i <= loopTo; i++)
            {
                DataRowTmp = dt_RootNodes.Rows[i];
                ItemCode = Conversions.ToString(DataRowTmp[0]);
                FarsiTitle = Conversions.ToString(DataRowTmp[1]);
                Cur_NodeTmp = MyParentNode.Nodes.Add(ItemCode, FarsiTitle);
                Cur_NodeTmp.Tag = ItemCode.ToString();
                AddChiled_CurrentNode(Cur_NodeTmp, SoftwareCode);
            }

            MyParentNode.ExpandAll();
        }

        private void LoadAccessLimit_ChildNodes(TreeNode Tmpnode)
        {
            int i;
            var loopTo = Tmpnode.Nodes.Count - 1;
            for (i = 0; i <= loopTo; i++)
            {
                Tmpnode.Nodes[i].Checked = Module_UserAccess.HaveAccessToItem(Conversions.ToInteger(Tmpnode.Nodes[i].Tag));
                LoadAccessLimit_ChildNodes(Tmpnode.Nodes[i]);
            }
        }

        private void UserChange_Disposed(object sender, EventArgs e)
        {
            Close();
        }

        private void UserChange_Load(object sender, EventArgs e)
        {
            tree(Conversions.ToInteger(Module_UserAccess.MySoftwareCode));
            if (OPType == 1) // new mode
            {
                txtUID.Text = "";
                txtUName.Text = "";
                txtUPass.Text = "";
                txtConfirm.Text = "";
            }

            if (OPType == 2) // edit mode
            {
                txtUPass.Text = "";
                txtConfirm.Text = "";
                objdataadapter = new SqlDataAdapter("SELECT SoftwareUsers.AccessLimit, UserProperty.UserName FROM SoftwareUsers INNER JOIN UserProperty ON SoftwareUsers.UserCode = UserProperty.UserCode WHERE SoftwareUsers.UserCode = " + txtUID.Text + " AND SoftwareUsers.SoftwareCode =" + Module_UserAccess.MySoftwareCode, mycnn);
                objdataset = new DataSet();
                objdataadapter.Fill(objdataset, "SoftwareUsers");
                objdataview = objdataset.Tables["SoftwareUsers"].DefaultView;
                Module_UserAccess.UserLimitStr = objdataset.Tables["SoftwareUsers"].Rows[0]["AccessLimit"].ToString();
                int i;
                var loopTo = TreeView1.Nodes.Count - 1;
                for (i = 0; i <= loopTo; i++)
                {
                    TreeView1.Nodes[i].Checked = Module_UserAccess.HaveAccessToItem(Conversions.ToInteger(TreeView1.Nodes[i].Tag));
                    LoadAccessLimit_ChildNodes(TreeView1.Nodes[i]);
                }
            }
        }

        private void TreeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // The code only executes if the user caused the checked state to change.
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    // Calls the CheckAllChildNodes method, passing in the current 
                    // Checked value of the TreeNode whose checked state changed. 
                    CheckAllChildNodes(e.Node, e.Node.Checked);
                }
            }
        }

        private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0)
                {
                    // If the current node has child nodes, call the CheckAllChildsNodes method recursively.
                    CheckAllChildNodes(node, nodeChecked);
                }
            }
        }

        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode tn;
            int i;
            tn = e.Node;
            if (tn.Checked == true)
            {
                int level;
                level = e.Node.Level;
                var loopTo = level - 1;
                for (i = 0; i <= loopTo; i++)
                {
                    if (tn.Parent.Checked == false)
                    {
                        tn.Parent.Checked = true;
                    }

                    tn = tn.Parent;
                }
            }
        }

        private void GetAccessLimit(SqlTransaction mTrn)
        {
            string mUserAccess;
            mUserAccess = Conversions.ToString(Module_UserAccess.CreateAccessString_To_Items(TreeView1));
            {
                var withBlock = new SqlCommand("update SoftwareUsers set AccessLimit=@AccessLimit where UserCode=@UserCode And SoftwareCode=" + Module_UserAccess.MySoftwareCode, mTrn.Connection);
                withBlock.Transaction = mTrn;
                withBlock.Parameters.AddWithValue("@AccessLimit", mUserAccess);
                withBlock.Parameters.AddWithValue("@UserCode", txtUID.Text);
                withBlock.ExecuteNonQuery();
            }
        }

        private void butRegister_Click(object sender, EventArgs e)
        {
            if (!txtUPass.Text.Equals(txtConfirm.Text))
            {
                MessageBox.Show("رمز عبور بدرستی وارد نشده است", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                return;
            }

            string MyPassTmp;
            SqlTransaction mTrn = null;
            if (OPType == 2) // edit mode
            {
                using (var cn = new SqlConnection(Module1.UMCnnStr))
                {
                    try
                    {
                        cn.Open();
                        mTrn = cn.BeginTransaction();
                        if (string.IsNullOrEmpty(txtUPass.Text))
                        {
                            {
                                var withBlock = new SqlCommand("update UserProperty Set UserName=@UserName where UserCode=@UserCode", cn);
                                withBlock.Transaction = mTrn;
                                withBlock.Parameters.AddWithValue("@UserName", txtUName.Text);
                                withBlock.Parameters.AddWithValue("@UserCode", txtUID.Text);
                                withBlock.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            MyPassTmp = Module_UserAccess.Encrypt_MyPass(txtUPass.Text);
                            {
                                var withBlock1 = new SqlCommand("update UserProperty set UserName=@UserName,UserPass=@UserPass where UserCode=@UserCode", cn);
                                withBlock1.Transaction = mTrn;
                                withBlock1.Parameters.AddWithValue("@UserName", txtUName.Text);
                                withBlock1.Parameters.AddWithValue("@UserPass", MyPassTmp);
                                withBlock1.Parameters.AddWithValue("@UserCode", txtUID.Text);
                                withBlock1.ExecuteNonQuery();
                            }
                        }

                        GetAccessLimit(mTrn);
                        mTrn.Commit();
                        Close();
                    }
                    catch (Exception ex)
                    {
                        if (mTrn is object)
                        {
                            mTrn.Rollback();
                        }

                        Logger.SaveError(Name + ".butRegister_Click", ex.Message);
                        MessageBox.Show("اصلاح مشخصات و دسترسی های کاربر با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    }
                    finally
                    {
                        if (cn.State == ConnectionState.Open)
                            cn.Close();
                    }
                }
                // Else 'add mode
                // MyPassTmp = Encrypt_MyPass(txtUPass.Text)

                // If txtUPass.Text = "" Then
                // MessageBox.Show("error")
                // End If

                // objcommand = New SqlCommand
                // objcommand.Connection = mycnn
                // objcommand.CommandText = "insert into UserProperty(UserCode,UserName,UserPass) values(@UserCode,@UserName,@UserPass);insert into SoftwareUsers(SoftwareCode,UserCode) values(" & MySoftwareCode & ",@UserCode)"
                // objcommand.Parameters.AddWithValue("@UserCode", txtUID.Text)
                // objcommand.Parameters.AddWithValue("@UserName", txtUName.Text)
                // objcommand.Parameters.AddWithValue("@UserPass", MyPassTmp)

                // mycnn.Open()
                // objcommand.ExecuteNonQuery()
                // mycnn.Close()

                // GetAccessLimit(mTrn)
                // Me.Close()
            }
        }

        private void txtcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Strings.Asc(e.KeyChar) == (int)Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txtname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Strings.Asc(e.KeyChar) == (int)Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txtpass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Strings.Asc(e.KeyChar) == (int)Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txtcode_Leave(object sender, EventArgs e)
        {
            if (OPType == 1)
            {
                if (CheckUIDCode() == false)
                {
                    var tt = new ToolTip();
                    tt.IsBalloon = true;
                    tt.Show("شماره کاربری وارد شده تکراری می باشد", txtUID, 40, -45, 1000);
                    // txtUID.Text = ""
                    txtUID.Focus();
                }
            }
        }

        private bool CheckUIDCode()
        {
            objcommand = new SqlCommand();
            objcommand.Connection = mycnn;
            objcommand.CommandText = "select UserCode from UserProperty where UserCode=@UserCode";
            objcommand.Parameters.AddWithValue("@UserCode", txtUID.Text);
            objdataadapter.SelectCommand = objcommand;
            objdataset = new DataSet();
            objdataadapter.Fill(objdataset, "UserProperty");
            int x;
            x = objdataset.Tables["UserProperty"].Rows.Count;
            if (x != 0)
            {
                return false;
                //return default;
            }

            return true;
        }

        private void TreeView1_AfterCheck1(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    CheckAllChildNodes(e.Node, e.Node.Checked);
                }
            }
        }

        private void TreeView1_NodeMouseClick1(object sender, TreeNodeMouseClickEventArgs e)
        {
            int i;
            TreeNode tn;
            tn = e.Node;
            if (tn.Checked == true)
            {
                int level;
                level = e.Node.Level;
                var loopTo = level - 1;
                for (i = 0; i <= loopTo; i++)
                {
                    if (tn.Parent.Checked == false)
                    {
                        tn.Parent.Checked = true;
                    }

                    tn = tn.Parent;
                }
            }
        }
    }
}