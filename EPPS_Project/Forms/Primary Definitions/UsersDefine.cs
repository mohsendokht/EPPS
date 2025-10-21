using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class UsersDefine
    {
        public UsersDefine()
        {
            InitializeComponent();
            _butExit.Name = "butExit";
            _butDel.Name = "butDel";
            _butEdit.Name = "butEdit";
            _butNew.Name = "butNew";
            _DataGridView1.Name = "DataGridView1";
        }

        // Dim DS As New DataSet
        private string s;
        private int p, rs;
        //private string olduid;
        private SqlConnection mycnn = new SqlConnection(Module1.UMCnnStr);
        private SqlDataAdapter objdataadapter = new SqlDataAdapter();
        private DataSet objdataset;
        private DataView objdataview;
        private SqlCommand objcommand;
        public int flag;

        private void Users_Load(object sender, EventArgs e)
        {
            Module_UserAccess.MyIPLock = Module1.GetDBConfigParamValue(0, 101, Module1.UMCnnStr);
            // mycnn.ConnectionString = connectionstr1

            // 'Login.Visible = False

            userlist();
            // tree(SoftwareCode)

            // Dim t As New DataTable("users")
            // DS.Tables.Add(t)
            // Dim cmd1 As New SqlCommand("select uid,upass,uname from users", CN)
            // SQLDA.SelectCommand = cmd1
            // SQLDA.Fill(DS.Tables("users"))

            // DataGridView1.DataSource = t
            // DataGridView1.AllowUserToAddRows = False

            // DataGridView1.Columns("uid").HeaderText = "کد کاربر"
            // DataGridView1.Columns("uid").Width = 80
            // DataGridView1.Columns("uid").Resizable = DataGridViewTriState.False
            // DataGridView1.Columns("uid").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            // DataGridView1.Columns("uid").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

            // DataGridView1.Columns("upass").HeaderText = "رمز عبور"
            // DataGridView1.Columns("upass").Width = 100
            // DataGridView1.Columns("upass").Resizable = DataGridViewTriState.False
            // DataGridView1.Columns("upass").Visible = False
            // DataGridView1.Columns("upass").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            // DataGridView1.Columns("upass").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

            // DataGridView1.Columns("uname").HeaderText = "نام کاربر"
            // DataGridView1.Columns("uname").Width = 150
            // DataGridView1.Columns("uname").Resizable = DataGridViewTriState.False
            // DataGridView1.Columns("uname").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            // DataGridView1.Columns("uname").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

            // 'DataGridView1.Columns("uoccupation").HeaderText = "شغل"
            // 'DataGridView1.Columns("uoccupation").Width = 176
            // 'DataGridView1.Columns("uoccupation").Resizable = DataGridViewTriState.False
            // 'DataGridView1.Columns("uoccupation").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            // 'DataGridView1.Columns("uoccupation").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

            // CM = Me.BindingContext(DS.Tables("users"))

            // butEdit.Enabled = False
            // butDel.Enabled = False
        }

        private void userlist()
        {
            DataGridView1.Visible = true;
            objdataadapter = new SqlDataAdapter("SELECT SoftwareUsers.UserCode,SoftwareUsers.SoftwareCode,UserProperty.UserName,UserProperty.UserPass FROM SoftwareUsers INNER JOIN UserProperty ON SoftwareUsers.UserCode = UserProperty.UserCode WHERE(SoftwareUsers.SoftwareCode =" + Module_UserAccess.MySoftwareCode + ")", mycnn);
            objdataset = new DataSet();
            objdataadapter.Fill(objdataset, "SoftwareUsers");
            objdataview = new DataView(objdataset.Tables["SoftwareUsers"]);
            DataGridView1.AutoGenerateColumns = true;
            DataGridView1.DataSource = objdataview;
            DataGridView1.Columns[0].HeaderText = "کد کاربری";
            DataGridView1.Columns[1].Visible = false;
            DataGridView1.Columns[2].HeaderText = "نام کاربر";
            DataGridView1.Columns[3].Visible = false;
            var objalternatingcellstyle = new DataGridViewCellStyle();
            objalternatingcellstyle.BackColor = Color.Bisque;
            DataGridView1.AlternatingRowsDefaultCellStyle = objalternatingcellstyle;
            DataGridView1.Columns[0].Width = 100;
            DataGridView1.Columns[2].Width = 250;
            // CM = Me.BindingContext(objdataset.Tables("SoftwareUsers"))
        }

        // Private Sub tree(ByVal softwarecode As Integer)
        // Dim dt_RootNodes As New DataTable
        // Dim cmd1 As New SqlCommand
        // Dim cmd2 As New SqlCommand
        // Dim ItemCode As String
        // Dim FarsiTitle As String
        // 'Dim CurNode As New TreeNode
        // TreeView1.Nodes.Clear()
        // cmd1.CommandText = "SELECT ItemCode, FarsiTitle FROM  SoftwareItems WHERE (ParentCode='0') and (SoftwareCode=" & softwarecode & ")"
        // cmd1.Connection = MyCnn
        // objdataadapter.SelectCommand = cmd1
        // MyCnn.Open()
        // objdataadapter.Fill(dt_RootNodes)
        // Dim DataRowTmp As DataRow
        // Dim i As Integer
        // Dim Cur_Node As TreeNode
        // For i = 0 To dt_RootNodes.Rows.Count - 1
        // DataRowTmp = dt_RootNodes.Rows(i)
        // ItemCode = DataRowTmp.Item(0)
        // FarsiTitle = DataRowTmp.Item(1).ToString
        // Cur_Node = TreeView1.Nodes.Add(ItemCode, FarsiTitle)
        // TreeView1.Nodes(ItemCode).Tag = ItemCode.ToString
        // AddChiled_CurrentNode(Cur_Node, softwarecode)
        // Next
        // MyCnn.Close()
        // End Sub

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

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            s = DataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            p = Conversions.ToInteger(DataGridView1.Rows[e.RowIndex].Cells[0].Value);
            rs = e.RowIndex;
            butEdit.Enabled = true;
            butDel.Enabled = true;
        }

        private void butNew_Click(object sender, EventArgs e)
        {
            var frm = new UserChangeAccess(1);
            frm.ShowDialog();
            userlist();

            // DS.Tables("users").Clear()
            // SQLDA.Fill(DS.Tables("users"))
        }

        private void deletesoftwareAccess()
        {
            objcommand = new SqlCommand();
            objcommand.Connection = mycnn;
            objcommand.CommandText = Conversions.ToString(Operators.ConcatenateObject("SELECT UserCode FROM SoftwareUsers where UserCode=", DataGridView1.SelectedRows[0].Cells[0].Value));
            objdataadapter.SelectCommand = objcommand;
            objdataset = new DataSet();
            objdataadapter.Fill(objdataset, "SoftwareUsers");
            int x;
            x = objdataset.Tables["SoftwareUsers"].Rows.Count;
            if (x != 0)
            {
                if (MessageBox.Show("برای این کاربر سطوح دسترسی تعریف شده است در صورت حذف کاربر سطوح دسترسی تعریف شده حذف می گردد.آیا کاربر حذف گردد؟", "سوال", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    softwareAccess();
                }
            }
            else if (MessageBox.Show("آیا کاربر حذف گردد؟", "سوال", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                deleteuser();
            }
        }

        private void deleteuser()
        {
            objcommand.Connection = mycnn;
            objcommand.CommandText = Conversions.ToString(Operators.ConcatenateObject("delete from UserProperty where UserCode=", DataGridView1.SelectedRows[0].Cells[0].Value));
            objcommand.Connection = mycnn;
            mycnn.Open();
            objcommand.ExecuteNonQuery();
            mycnn.Close();
            userlist();
        }

        private void softwareAccess()
        {
            objcommand.Connection = mycnn;
            objcommand.CommandText = Conversions.ToString(Operators.ConcatenateObject("delete from SoftwareUsers where UserCode=", DataGridView1.SelectedRows[0].Cells[0].Value));
            objcommand.Connection = mycnn;
            mycnn.Open();
            objcommand.ExecuteNonQuery();
            mycnn.Close();
            deleteuser();
        }

        private void butEdit_Click(object sender, EventArgs e)
        {
            if (DataGridView1.SelectedRows.Count == 1)
            {
                DataRow row;
                row = objdataset.Tables["SoftwareUsers"].Rows[rs];
                var frm = new UserChangeAccess(row, 2);
                frm.ShowDialog();
                // UserChange.UserCode = DataGridView1.SelectedRows(0).Cells(0).Value.ToString
                // UserChange.ShowDialog()
                userlist();
            }

            // Dim row(4) As String
            // row(0) = DataGridView1.Rows(rs).Cells(0).Value.ToString
            // row(1) = DataGridView1.Rows(rs).Cells(1).Value.ToString
            // row(2) = DataGridView1.Rows(rs).Cells(2).Value.ToString
            // row(3) = DataGridView1.Rows(rs).Cells(3).Value.ToString
            // row(4) = DataGridView1.Rows(rs).Cells(4).Value.ToString
            // Dim frm As New UserChange(row)
            // frm.ShowDialog()
            // DS.Tables("users").Clear()
            // SQLDA.Fill(DS.Tables("users"))
        }

        private void butDel_Click(object sender, EventArgs e)
        {
            deletesoftwareAccess();

            // Dim re As DialogResult = MessageBox.Show("آیا مطمئن هستید که می خواهید کاربر «" & s & "» را حذف کنید؟","برنامه ریزی تولید ", MessageBoxButtons.YesNo, _
            // MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, _
            // MessageBoxOptions.RtlReading)
            // If re = Windows.Forms.DialogResult.No Then
            // Exit Sub
            // ElseIf re = Windows.Forms.DialogResult.Yes Then
            // Dim cmd2 As New SqlCommand
            // cmd2.Connection = cn
            // cmd2.CommandType = CommandType.StoredProcedure
            // cmd2.CommandText = "spmDelUsers"
            // Dim puid As New SqlParameter("@uid", SqlDbType.Int)
            // cmd2.Parameters.Add(puid)
            // cmd2.Parameters("@uid").Value = p
            // If CN.State = ConnectionState.Closed Then CN.Open()
            // cmd2.ExecuteNonQuery()
            // CN.Close()
            // CM.RemoveAt(CM.Position)
            // MessageBox.Show(".کاربر «" & s & "» حذف شد","برنامه ریزی تولید ", MessageBoxButtons.OK, _
            // MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, _
            // MessageBoxOptions.RtlReading)
            // If CM.Position = CM.Count - 1 Then
            // DataGridView1.Rows(CM.Position).Selected = True
            // s = DataGridView1.Rows(CM.Position).Cells(2).Value.ToString
            // p = DataGridView1.Rows(CM.Position).Cells(0).Value
            // ElseIf CM.Position <> CM.Count - 1 And CM.Position <> 0 Then
            // DataGridView1.Rows(CM.Position).Selected = True
            // s = DataGridView1.Rows(CM.Position).Cells(2).Value.ToString
            // p = DataGridView1.Rows(CM.Position).Cells(0).Value
            // End If
            // End If
        }

        private void DataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (DataGridView1.SelectedRows.Count == 0)
                return;
            objcommand = new SqlCommand();
            objcommand.Connection = mycnn;
            // objcommand.CommandText = "select AccessLimit from SoftwareUsers where SoftwareCode=" & DataGridView1.SelectedRows(0).Cells(1).Value & " and UserCode=" & DataGridView1.SelectedRows(0).Cells(0).Value & ""
            objcommand.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("  SELECT SoftwareUsers.AccessLimit, UserProperty.UserName FROM SoftwareUsers INNER JOIN UserProperty ON SoftwareUsers.UserCode = UserProperty.UserCode WHERE     (SoftwareUsers.UserCode = ", DataGridView1.SelectedRows[0].Cells[0].Value), ") AND (SoftwareUsers.SoftwareCode =101)"));
            objdataadapter = new SqlDataAdapter();
            objdataadapter.SelectCommand = objcommand;
            objdataset = new DataSet();
            objdataadapter.Fill(objdataset, "SoftwareUsers");
            objdataview = new DataView(objdataset.Tables["SoftwareUsers"]);
            Module_UserAccess.UserLimitStr = objdataset.Tables["SoftwareUsers"].Rows[0]["AccessLimit"].ToString();
            Label4.Text = "حدود دسترسی کاربر  " + DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            // Dim i As Integer
            // For i = 0 To TreeView1.Nodes.Count - 1
            // TreeView1.Nodes(i).Checked = HaveAccessToItem(TreeView1.Nodes(i).Tag)
            // LoadAccessLimit_ChildNodes(TreeView1.Nodes(i))
            // Next
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

        private void butExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}