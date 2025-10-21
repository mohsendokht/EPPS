using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmProductTreeDetail : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            if (disposing && components is object)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            var TreeNode1 = new TreeNode("جزء 1");
            var TreeNode2 = new TreeNode("جزء 2");
            var TreeNode3 = new TreeNode("اجزای درخت محصول", new TreeNode[] { TreeNode1, TreeNode2 });
            var DataGridViewCellStyle1 = new DataGridViewCellStyle();
            var DataGridViewCellStyle2 = new DataGridViewCellStyle();
            var DataGridViewCellStyle3 = new DataGridViewCellStyle();
            var DataGridViewCellStyle4 = new DataGridViewCellStyle();
            _TreeView1 = new System.Windows.Forms.TreeView();
            _TreeView1.AfterSelect += new TreeViewEventHandler(TreeView1_AfterSelect);
            _TreeView1.MouseClick += new MouseEventHandler(TreeView1_MouseClick);
            _TreeView1.MouseDown += new MouseEventHandler(TreeView1_MouseDown);
            mnuTreeView = new ContextMenuStrip(components);
            _mnuItemNewNode = new ToolStripMenuItem();
            _mnuItemNewNode.Click += new EventHandler(mnuNodeItems_Click);
            _mnuItemEditNode = new ToolStripMenuItem();
            _mnuItemEditNode.Click += new EventHandler(mnuNodeItems_Click);
            _mnuItemDeleteNode = new ToolStripMenuItem();
            _mnuItemDeleteNode.Click += new EventHandler(mnuNodeItems_Click);
            TabControl1 = new TabControl();
            TabPage1 = new TabPage();
            _cmdCancel = new System.Windows.Forms.Button();
            _cmdCancel.Click += new EventHandler(cmdCancel_Click);
            _dgPreItems = new DataGridView();
            _dgPreItems.MouseClick += new MouseEventHandler(dgPreItems_MouseClick);
            _dgPreItems.MouseDoubleClick += new MouseEventHandler(dgPreItems_MouseClick);
            _dgPreItems.MouseClick += new MouseEventHandler(dgPreItems_MouseClick);
            _dgPreItems.MouseDoubleClick += new MouseEventHandler(dgPreItems_MouseClick);
            DataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            _mnuPreItems = new ContextMenuStrip(components);
            _mnuPreItems.Opening += new System.ComponentModel.CancelEventHandler(mnuPreItems_Opening);
            _mnuPI_Spcification = new ToolStripMenuItem();
            _mnuPI_Spcification.Click += new EventHandler(cmdPreItems_Click);
            _dgPreOperations = new DataGridView();
            _dgPreOperations.MouseClick += new MouseEventHandler(dgPreOperations_MouseClick);
            _dgPreOperations.MouseDoubleClick += new MouseEventHandler(dgPreOperations_MouseClick);
            _dgPreOperations.MouseClick += new MouseEventHandler(dgPreOperations_MouseClick);
            _dgPreOperations.MouseDoubleClick += new MouseEventHandler(dgPreOperations_MouseClick);
            DataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            _mnuPreOperation = new ContextMenuStrip(components);
            _mnuPreOperation.Opening += new System.ComponentModel.CancelEventHandler(mnuPreItems_Opening);
            _mnuPO_Spcification = new ToolStripMenuItem();
            _mnuPO_Spcification.Click += new EventHandler(cmdPreOperations_Click);
            _dgExecutorMachines = new DataGridView();
            _dgExecutorMachines.MouseClick += new MouseEventHandler(dgExecutorMachines_MouseClick);
            _dgExecutorMachines.MouseDoubleClick += new MouseEventHandler(dgExecutorMachines_MouseClick);
            _dgExecutorMachines.MouseClick += new MouseEventHandler(dgExecutorMachines_MouseClick);
            _dgExecutorMachines.MouseDoubleClick += new MouseEventHandler(dgExecutorMachines_MouseClick);
            DataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            _mnuExecMachines = new ContextMenuStrip(components);
            _mnuExecMachines.Opening += new System.ComponentModel.CancelEventHandler(mnuPreItems_Opening);
            _mnuEM_New = new ToolStripMenuItem();
            _mnuEM_New.Click += new EventHandler(mnuExecMachinesOperations_Click);
            _mnuEM_Edit = new ToolStripMenuItem();
            _mnuEM_Edit.Click += new EventHandler(mnuExecMachinesOperations_Click);
            _mnuEM_Delete = new ToolStripMenuItem();
            _mnuEM_Delete.Click += new EventHandler(mnuExecMachinesOperations_Click);
            _cmdOperationBuiltGraphicView = new System.Windows.Forms.Button();
            _cmdOperationBuiltGraphicView.Click += new EventHandler(cmdOperationBuiltGraphicView_Click);
            _cmdEditOperation = new System.Windows.Forms.Button();
            _cmdEditOperation.Click += new EventHandler(cmdAddOperation_Click);
            _cmdRemoveOperation = new System.Windows.Forms.Button();
            _cmdRemoveOperation.Click += new EventHandler(cmdAddOperation_Click);
            _cmdAddOperation = new System.Windows.Forms.Button();
            _cmdAddOperation.Click += new EventHandler(cmdAddOperation_Click);
            _dgOperations = new DataGridView();
            _dgOperations.RowEnter += new DataGridViewCellEventHandler(dgOperations_RowEnter);
            OperationCode = new DataGridViewTextBoxColumn();
            OperationTitle = new DataGridViewTextBoxColumn();
            ActivityGroup = new DataGridViewTextBoxColumn();
            mnuTreeView.SuspendLayout();
            TabControl1.SuspendLayout();
            TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgPreItems).BeginInit();
            _mnuPreItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgPreOperations).BeginInit();
            _mnuPreOperation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgExecutorMachines).BeginInit();
            _mnuExecMachines.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgOperations).BeginInit();
            SuspendLayout();
            // 
            // TreeView1
            // 
            _TreeView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            _TreeView1.BorderStyle = BorderStyle.FixedSingle;
            _TreeView1.ContextMenuStrip = mnuTreeView;
            _TreeView1.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _TreeView1.HideSelection = false;
            _TreeView1.Location = new Point(640, 17);
            _TreeView1.Name = "_TreeView1";
            TreeNode1.Name = "Node1";
            TreeNode1.Text = "جزء 1";
            TreeNode2.Name = "Node2";
            TreeNode2.Text = "جزء 2";
            TreeNode3.Name = "Node0";
            TreeNode3.Text = "اجزای درخت محصول";
            _TreeView1.Nodes.AddRange(new TreeNode[] { TreeNode3 });
            _TreeView1.RightToLeftLayout = true;
            _TreeView1.Size = new Size(240, 442);
            _TreeView1.TabIndex = 0;
            _TreeView1.TabStop = false;
            // 
            // mnuTreeView
            // 
            mnuTreeView.Items.AddRange(new ToolStripItem[] { _mnuItemNewNode, _mnuItemEditNode, _mnuItemDeleteNode });
            mnuTreeView.Name = "mnuTreeView";
            mnuTreeView.RightToLeft = RightToLeft.Yes;
            mnuTreeView.Size = new Size(102, 70);
            // 
            // mnuItemNewNode
            // 
            _mnuItemNewNode.Name = "_mnuItemNewNode";
            _mnuItemNewNode.Size = new Size(101, 22);
            _mnuItemNewNode.Text = "جدید";
            // 
            // mnuItemEditNode
            // 
            _mnuItemEditNode.Name = "_mnuItemEditNode";
            _mnuItemEditNode.Size = new Size(101, 22);
            _mnuItemEditNode.Text = "اصلاح";
            // 
            // mnuItemDeleteNode
            // 
            _mnuItemDeleteNode.Name = "_mnuItemDeleteNode";
            _mnuItemDeleteNode.Size = new Size(101, 22);
            _mnuItemDeleteNode.Text = "حذف";
            // 
            // TabControl1
            // 
            TabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            TabControl1.Controls.Add(TabPage1);
            TabControl1.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            TabControl1.Location = new Point(3, 10);
            TabControl1.Name = "TabControl1";
            TabControl1.RightToLeftLayout = true;
            TabControl1.SelectedIndex = 0;
            TabControl1.Size = new Size(631, 451);
            TabControl1.TabIndex = 154;
            // 
            // TabPage1
            // 
            TabPage1.BackColor = SystemColors.Control;
            TabPage1.Controls.Add(_cmdCancel);
            TabPage1.Controls.Add(_dgPreItems);
            TabPage1.Controls.Add(_dgPreOperations);
            TabPage1.Controls.Add(_dgExecutorMachines);
            TabPage1.Controls.Add(_cmdOperationBuiltGraphicView);
            TabPage1.Controls.Add(_cmdEditOperation);
            TabPage1.Controls.Add(_cmdRemoveOperation);
            TabPage1.Controls.Add(_cmdAddOperation);
            TabPage1.Controls.Add(_dgOperations);
            TabPage1.Location = new Point(4, 23);
            TabPage1.Name = "TabPage1";
            TabPage1.Padding = new Padding(3);
            TabPage1.Size = new Size(623, 424);
            TabPage1.TabIndex = 0;
            TabPage1.Text = "لیست عملیات ساخت";
            // 
            // cmdCancel
            // 
            _cmdCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _cmdCancel.BackColor = Color.Transparent;
            _cmdCancel.DialogResult = DialogResult.Cancel;
            _cmdCancel.Font = new Font("Tahoma", 9.0f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdCancel.ForeColor = Color.Red;
            _cmdCancel.Location = new Point(16, 385);
            _cmdCancel.Name = "_cmdCancel";
            _cmdCancel.Size = new Size(84, 30);
            _cmdCancel.TabIndex = 291;
            _cmdCancel.Text = "خروج";
            _cmdCancel.UseVisualStyleBackColor = false;
            // 
            // dgPreItems
            // 
            _dgPreItems.AllowUserToAddRows = false;
            _dgPreItems.AllowUserToDeleteRows = false;
            DataGridViewCellStyle1.BackColor = Color.White;
            _dgPreItems.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1;
            _dgPreItems.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _dgPreItems.BackgroundColor = SystemColors.ControlDark;
            _dgPreItems.ColumnHeadersHeight = 25;
            _dgPreItems.Columns.AddRange(new DataGridViewColumn[] { DataGridViewTextBoxColumn2 });
            _dgPreItems.ContextMenuStrip = _mnuPreItems;
            _dgPreItems.Location = new Point(7, 195);
            _dgPreItems.Name = "_dgPreItems";
            _dgPreItems.ReadOnly = true;
            _dgPreItems.RowHeadersWidth = 5;
            _dgPreItems.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            _dgPreItems.RowTemplate.DefaultCellStyle.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(224)), Conversions.ToInteger(Conversions.ToByte(192)));
            _dgPreItems.RowTemplate.DefaultCellStyle.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _dgPreItems.Size = new Size(200, 180);
            _dgPreItems.TabIndex = 290;
            // 
            // DataGridViewTextBoxColumn2
            // 
            DataGridViewTextBoxColumn2.HeaderText = "مواد/قطعه وارده";
            DataGridViewTextBoxColumn2.MinimumWidth = 192;
            DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2";
            DataGridViewTextBoxColumn2.ReadOnly = true;
            DataGridViewTextBoxColumn2.Resizable = DataGridViewTriState.False;
            DataGridViewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
            DataGridViewTextBoxColumn2.Width = 192;
            // 
            // mnuPreItems
            // 
            _mnuPreItems.Items.AddRange(new ToolStripItem[] { _mnuPI_Spcification });
            _mnuPreItems.Name = "_mnuTreeView";
            _mnuPreItems.RightToLeft = RightToLeft.Yes;
            _mnuPreItems.Size = new Size(180, 26);
            // 
            // mnuPI_Spcification
            // 
            _mnuPI_Spcification.Name = "_mnuPI_Spcification";
            _mnuPI_Spcification.Size = new Size(179, 22);
            _mnuPI_Spcification.Text = "لیست مواد/قطعه وارده";
            // 
            // dgPreOperations
            // 
            _dgPreOperations.AllowUserToAddRows = false;
            _dgPreOperations.AllowUserToDeleteRows = false;
            DataGridViewCellStyle2.BackColor = Color.White;
            _dgPreOperations.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2;
            _dgPreOperations.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _dgPreOperations.BackgroundColor = SystemColors.ControlDark;
            _dgPreOperations.ColumnHeadersHeight = 25;
            _dgPreOperations.Columns.AddRange(new DataGridViewColumn[] { DataGridViewTextBoxColumn1, Column2, Column3, Column4 });
            _dgPreOperations.ContextMenuStrip = _mnuPreOperation;
            _dgPreOperations.Location = new Point(212, 195);
            _dgPreOperations.Name = "_dgPreOperations";
            _dgPreOperations.ReadOnly = true;
            _dgPreOperations.RowHeadersWidth = 5;
            _dgPreOperations.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            _dgPreOperations.RowTemplate.DefaultCellStyle.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(224)), Conversions.ToInteger(Conversions.ToByte(192)));
            _dgPreOperations.RowTemplate.DefaultCellStyle.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _dgPreOperations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgPreOperations.Size = new Size(159, 180);
            _dgPreOperations.TabIndex = 289;
            // 
            // DataGridViewTextBoxColumn1
            // 
            DataGridViewTextBoxColumn1.HeaderText = "عملیات پیشنیاز";
            DataGridViewTextBoxColumn1.MinimumWidth = 140;
            DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1";
            DataGridViewTextBoxColumn1.ReadOnly = true;
            DataGridViewTextBoxColumn1.Resizable = DataGridViewTriState.False;
            DataGridViewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
            DataGridViewTextBoxColumn1.Width = 140;
            // 
            // Column2
            // 
            Column2.HeaderText = "رابطه";
            Column2.MinimumWidth = 42;
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.Resizable = DataGridViewTriState.False;
            Column2.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column2.Width = 42;
            // 
            // Column3
            // 
            Column3.HeaderText = "زمان";
            Column3.MinimumWidth = 42;
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 42;
            // 
            // Column4
            // 
            Column4.HeaderText = "نوع زمان";
            Column4.MinimumWidth = 65;
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            Column4.Width = 65;
            // 
            // mnuPreOperation
            // 
            _mnuPreOperation.Items.AddRange(new ToolStripItem[] { _mnuPO_Spcification });
            _mnuPreOperation.Name = "_mnuTreeView";
            _mnuPreOperation.RightToLeft = RightToLeft.Yes;
            _mnuPreOperation.Size = new Size(178, 26);
            // 
            // mnuPO_Spcification
            // 
            _mnuPO_Spcification.Name = "_mnuPO_Spcification";
            _mnuPO_Spcification.Size = new Size(177, 22);
            _mnuPO_Spcification.Text = "لیست عملیات پیشنیاز";
            // 
            // dgExecutorMachines
            // 
            _dgExecutorMachines.AllowUserToAddRows = false;
            _dgExecutorMachines.AllowUserToDeleteRows = false;
            DataGridViewCellStyle3.BackColor = Color.White;
            _dgExecutorMachines.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3;
            _dgExecutorMachines.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _dgExecutorMachines.BackgroundColor = SystemColors.ControlDark;
            _dgExecutorMachines.ColumnHeadersHeight = 25;
            _dgExecutorMachines.Columns.AddRange(new DataGridViewColumn[] { DataGridViewTextBoxColumn4, Column1 });
            _dgExecutorMachines.ContextMenuStrip = _mnuExecMachines;
            _dgExecutorMachines.Location = new Point(376, 195);
            _dgExecutorMachines.Name = "_dgExecutorMachines";
            _dgExecutorMachines.ReadOnly = true;
            _dgExecutorMachines.RowHeadersWidth = 5;
            _dgExecutorMachines.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            _dgExecutorMachines.RowTemplate.DefaultCellStyle.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(224)), Conversions.ToInteger(Conversions.ToByte(192)));
            _dgExecutorMachines.RowTemplate.DefaultCellStyle.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _dgExecutorMachines.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgExecutorMachines.Size = new Size(240, 180);
            _dgExecutorMachines.TabIndex = 288;
            // 
            // DataGridViewTextBoxColumn4
            // 
            DataGridViewTextBoxColumn4.HeaderText = "ماشین انجام دهنده";
            DataGridViewTextBoxColumn4.MinimumWidth = 175;
            DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4";
            DataGridViewTextBoxColumn4.ReadOnly = true;
            DataGridViewTextBoxColumn4.Resizable = DataGridViewTriState.False;
            DataGridViewTextBoxColumn4.SortMode = DataGridViewColumnSortMode.NotSortable;
            DataGridViewTextBoxColumn4.Width = 175;
            // 
            // Column1
            // 
            Column1.HeaderText = "اولویت";
            Column1.MinimumWidth = 57;
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Resizable = DataGridViewTriState.False;
            Column1.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column1.Width = 57;
            // 
            // mnuExecMachines
            // 
            _mnuExecMachines.Items.AddRange(new ToolStripItem[] { _mnuEM_New, _mnuEM_Edit, _mnuEM_Delete });
            _mnuExecMachines.Name = "_mnuTreeView";
            _mnuExecMachines.RightToLeft = RightToLeft.Yes;
            _mnuExecMachines.Size = new Size(102, 70);
            // 
            // mnuEM_New
            // 
            _mnuEM_New.Name = "_mnuEM_New";
            _mnuEM_New.Size = new Size(101, 22);
            _mnuEM_New.Text = "جدید";
            // 
            // mnuEM_Edit
            // 
            _mnuEM_Edit.Name = "_mnuEM_Edit";
            _mnuEM_Edit.Size = new Size(101, 22);
            _mnuEM_Edit.Text = "اصلاح";
            // 
            // mnuEM_Delete
            // 
            _mnuEM_Delete.Name = "_mnuEM_Delete";
            _mnuEM_Delete.Size = new Size(101, 22);
            _mnuEM_Delete.Text = "حذف";
            // 
            // cmdOperationBuiltGraphicView
            // 
            _cmdOperationBuiltGraphicView.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _cmdOperationBuiltGraphicView.BackColor = Color.Transparent;
            _cmdOperationBuiltGraphicView.Enabled = false;
            _cmdOperationBuiltGraphicView.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdOperationBuiltGraphicView.ForeColor = Color.Blue;
            _cmdOperationBuiltGraphicView.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdOperationBuiltGraphicView.Location = new Point(432, 385);
            _cmdOperationBuiltGraphicView.Name = "_cmdOperationBuiltGraphicView";
            _cmdOperationBuiltGraphicView.RightToLeft = RightToLeft.No;
            _cmdOperationBuiltGraphicView.Size = new Size(184, 30);
            _cmdOperationBuiltGraphicView.TabIndex = 4;
            _cmdOperationBuiltGraphicView.Text = "نمایش گرافیکی عملیات ساخت";
            _cmdOperationBuiltGraphicView.TextAlign = ContentAlignment.MiddleRight;
            _cmdOperationBuiltGraphicView.UseVisualStyleBackColor = false;
            // 
            // cmdEditOperation
            // 
            _cmdEditOperation.BackColor = Color.Transparent;
            _cmdEditOperation.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdEditOperation.ForeColor = Color.Black;
            _cmdEditOperation.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdEditOperation.Location = new Point(85, 14);
            _cmdEditOperation.Name = "_cmdEditOperation";
            _cmdEditOperation.RightToLeft = RightToLeft.No;
            _cmdEditOperation.Size = new Size(63, 24);
            _cmdEditOperation.TabIndex = 1;
            _cmdEditOperation.Text = "اصلاح";
            _cmdEditOperation.TextAlign = ContentAlignment.MiddleRight;
            _cmdEditOperation.UseVisualStyleBackColor = false;
            // 
            // cmdRemoveOperation
            // 
            _cmdRemoveOperation.BackColor = Color.Transparent;
            _cmdRemoveOperation.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdRemoveOperation.ForeColor = Color.Black;
            _cmdRemoveOperation.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdRemoveOperation.Location = new Point(16, 14);
            _cmdRemoveOperation.Name = "_cmdRemoveOperation";
            _cmdRemoveOperation.RightToLeft = RightToLeft.No;
            _cmdRemoveOperation.Size = new Size(63, 24);
            _cmdRemoveOperation.TabIndex = 2;
            _cmdRemoveOperation.Text = "حذف";
            _cmdRemoveOperation.TextAlign = ContentAlignment.MiddleRight;
            _cmdRemoveOperation.UseVisualStyleBackColor = false;
            // 
            // cmdAddOperation
            // 
            _cmdAddOperation.BackColor = Color.Transparent;
            _cmdAddOperation.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdAddOperation.ForeColor = Color.Black;
            _cmdAddOperation.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdAddOperation.Location = new Point(154, 14);
            _cmdAddOperation.Name = "_cmdAddOperation";
            _cmdAddOperation.RightToLeft = RightToLeft.No;
            _cmdAddOperation.Size = new Size(63, 24);
            _cmdAddOperation.TabIndex = 0;
            _cmdAddOperation.Text = "جدید";
            _cmdAddOperation.TextAlign = ContentAlignment.MiddleRight;
            _cmdAddOperation.UseVisualStyleBackColor = false;
            // 
            // dgOperations
            // 
            _dgOperations.AllowUserToAddRows = false;
            _dgOperations.AllowUserToDeleteRows = false;
            _dgOperations.AllowUserToResizeColumns = false;
            _dgOperations.AllowUserToResizeRows = false;
            DataGridViewCellStyle4.BackColor = Color.White;
            _dgOperations.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4;
            _dgOperations.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            _dgOperations.BackgroundColor = SystemColors.ControlDark;
            _dgOperations.ColumnHeadersHeight = 25;
            _dgOperations.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            _dgOperations.Columns.AddRange(new DataGridViewColumn[] { OperationCode, OperationTitle, ActivityGroup });
            _dgOperations.Location = new Point(7, 42);
            _dgOperations.Name = "_dgOperations";
            _dgOperations.ReadOnly = true;
            _dgOperations.RowHeadersWidth = 10;
            _dgOperations.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            _dgOperations.RowTemplate.DefaultCellStyle.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            _dgOperations.RowTemplate.DefaultCellStyle.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _dgOperations.RowTemplate.DefaultCellStyle.ForeColor = Color.Black;
            _dgOperations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgOperations.Size = new Size(609, 147);
            _dgOperations.TabIndex = 284;
            _dgOperations.TabStop = false;
            // 
            // OperationCode
            // 
            OperationCode.HeaderText = "کد عملیات";
            OperationCode.Name = "OperationCode";
            OperationCode.ReadOnly = true;
            OperationCode.Width = 140;
            // 
            // OperationTitle
            // 
            OperationTitle.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            OperationTitle.HeaderText = "عنوان عملیات";
            OperationTitle.Name = "OperationTitle";
            OperationTitle.ReadOnly = true;
            // 
            // ActivityGroup
            // 
            ActivityGroup.HeaderText = "گروه عملیات";
            ActivityGroup.Name = "ActivityGroup";
            ActivityGroup.ReadOnly = true;
            ActivityGroup.Width = 110;
            // 
            // frmProductTreeDetail
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _cmdCancel;
            ClientSize = new Size(883, 473);
            Controls.Add(_TreeView1);
            Controls.Add(TabControl1);
            Name = "frmProductTreeDetail";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Show;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " مشخصات اجزای درخت محصول";
            mnuTreeView.ResumeLayout(false);
            TabControl1.ResumeLayout(false);
            TabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_dgPreItems).EndInit();
            _mnuPreItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_dgPreOperations).EndInit();
            _mnuPreOperation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_dgExecutorMachines).EndInit();
            _mnuExecMachines.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_dgOperations).EndInit();
            Load += new EventHandler(frmProductTreeDetail_Load);
            FormClosing += new FormClosingEventHandler(frmProductTreeDetail_FormClosing);
            ResumeLayout(false);
        }

        private System.Windows.Forms.TreeView _TreeView1;

        internal System.Windows.Forms.TreeView TreeView1
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _TreeView1;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_TreeView1 != null)
                {
                    _TreeView1.AfterSelect -= TreeView1_AfterSelect;
                    _TreeView1.MouseClick -= TreeView1_MouseClick;
                    _TreeView1.MouseDown -= TreeView1_MouseDown;
                }

                _TreeView1 = value;
                if (_TreeView1 != null)
                {
                    _TreeView1.AfterSelect += TreeView1_AfterSelect;
                    _TreeView1.MouseClick += TreeView1_MouseClick;
                    _TreeView1.MouseDown += TreeView1_MouseDown;
                }
            }
        }

        internal TabControl TabControl1;
        internal TabPage TabPage1;
        private System.Windows.Forms.Button _cmdEditOperation;

        internal System.Windows.Forms.Button cmdEditOperation
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdEditOperation;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdEditOperation != null)
                {
                    _cmdEditOperation.Click -= cmdAddOperation_Click;
                }

                _cmdEditOperation = value;
                if (_cmdEditOperation != null)
                {
                    _cmdEditOperation.Click += cmdAddOperation_Click;
                }
            }
        }

        private System.Windows.Forms.Button _cmdRemoveOperation;

        internal System.Windows.Forms.Button cmdRemoveOperation
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdRemoveOperation;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdRemoveOperation != null)
                {
                    _cmdRemoveOperation.Click -= cmdAddOperation_Click;
                }

                _cmdRemoveOperation = value;
                if (_cmdRemoveOperation != null)
                {
                    _cmdRemoveOperation.Click += cmdAddOperation_Click;
                }
            }
        }

        private System.Windows.Forms.Button _cmdAddOperation;

        internal System.Windows.Forms.Button cmdAddOperation
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdAddOperation;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdAddOperation != null)
                {
                    _cmdAddOperation.Click -= cmdAddOperation_Click;
                }

                _cmdAddOperation = value;
                if (_cmdAddOperation != null)
                {
                    _cmdAddOperation.Click += cmdAddOperation_Click;
                }
            }
        }

        private DataGridView _dgOperations;

        internal DataGridView dgOperations
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _dgOperations;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_dgOperations != null)
                {
                    _dgOperations.RowEnter -= dgOperations_RowEnter;
                }

                _dgOperations = value;
                if (_dgOperations != null)
                {
                    _dgOperations.RowEnter += dgOperations_RowEnter;
                }
            }
        }

        internal ContextMenuStrip mnuTreeView;
        private ToolStripMenuItem _mnuItemNewNode;

        internal ToolStripMenuItem mnuItemNewNode
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _mnuItemNewNode;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_mnuItemNewNode != null)
                {
                    _mnuItemNewNode.Click -= mnuNodeItems_Click;
                }

                _mnuItemNewNode = value;
                if (_mnuItemNewNode != null)
                {
                    _mnuItemNewNode.Click += mnuNodeItems_Click;
                }
            }
        }

        private ToolStripMenuItem _mnuItemEditNode;

        internal ToolStripMenuItem mnuItemEditNode
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _mnuItemEditNode;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_mnuItemEditNode != null)
                {
                    _mnuItemEditNode.Click -= mnuNodeItems_Click;
                }

                _mnuItemEditNode = value;
                if (_mnuItemEditNode != null)
                {
                    _mnuItemEditNode.Click += mnuNodeItems_Click;
                }
            }
        }

        private ToolStripMenuItem _mnuItemDeleteNode;

        internal ToolStripMenuItem mnuItemDeleteNode
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _mnuItemDeleteNode;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_mnuItemDeleteNode != null)
                {
                    _mnuItemDeleteNode.Click -= mnuNodeItems_Click;
                }

                _mnuItemDeleteNode = value;
                if (_mnuItemDeleteNode != null)
                {
                    _mnuItemDeleteNode.Click += mnuNodeItems_Click;
                }
            }
        }

        private System.Windows.Forms.Button _cmdOperationBuiltGraphicView;

        internal System.Windows.Forms.Button cmdOperationBuiltGraphicView
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdOperationBuiltGraphicView;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdOperationBuiltGraphicView != null)
                {
                    _cmdOperationBuiltGraphicView.Click -= cmdOperationBuiltGraphicView_Click;
                }

                _cmdOperationBuiltGraphicView = value;
                if (_cmdOperationBuiltGraphicView != null)
                {
                    _cmdOperationBuiltGraphicView.Click += cmdOperationBuiltGraphicView_Click;
                }
            }
        }

        private DataGridView _dgPreItems;

        internal DataGridView dgPreItems
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _dgPreItems;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_dgPreItems != null)
                {
                    _dgPreItems.MouseClick -= dgPreItems_MouseClick;
                    _dgPreItems.MouseDoubleClick -= dgPreItems_MouseClick;
                }

                _dgPreItems = value;
                if (_dgPreItems != null)
                {
                    _dgPreItems.MouseClick += dgPreItems_MouseClick;
                    _dgPreItems.MouseDoubleClick += dgPreItems_MouseClick;
                }
            }
        }

        private DataGridView _dgPreOperations;

        internal DataGridView dgPreOperations
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _dgPreOperations;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_dgPreOperations != null)
                {
                    _dgPreOperations.MouseClick -= dgPreOperations_MouseClick;
                    _dgPreOperations.MouseDoubleClick -= dgPreOperations_MouseClick;
                }

                _dgPreOperations = value;
                if (_dgPreOperations != null)
                {
                    _dgPreOperations.MouseClick += dgPreOperations_MouseClick;
                    _dgPreOperations.MouseDoubleClick += dgPreOperations_MouseClick;
                }
            }
        }

        private DataGridView _dgExecutorMachines;

        internal DataGridView dgExecutorMachines
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _dgExecutorMachines;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_dgExecutorMachines != null)
                {
                    _dgExecutorMachines.MouseClick -= dgExecutorMachines_MouseClick;
                    _dgExecutorMachines.MouseDoubleClick -= dgExecutorMachines_MouseClick;
                }

                _dgExecutorMachines = value;
                if (_dgExecutorMachines != null)
                {
                    _dgExecutorMachines.MouseClick += dgExecutorMachines_MouseClick;
                    _dgExecutorMachines.MouseDoubleClick += dgExecutorMachines_MouseClick;
                }
            }
        }

        private ContextMenuStrip _mnuExecMachines;

        internal ContextMenuStrip mnuExecMachines
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _mnuExecMachines;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_mnuExecMachines != null)
                {
                    _mnuExecMachines.Opening -= mnuPreItems_Opening;
                }

                _mnuExecMachines = value;
                if (_mnuExecMachines != null)
                {
                    _mnuExecMachines.Opening += mnuPreItems_Opening;
                }
            }
        }

        private ToolStripMenuItem _mnuEM_New;

        internal ToolStripMenuItem mnuEM_New
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _mnuEM_New;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_mnuEM_New != null)
                {
                    _mnuEM_New.Click -= mnuExecMachinesOperations_Click;
                }

                _mnuEM_New = value;
                if (_mnuEM_New != null)
                {
                    _mnuEM_New.Click += mnuExecMachinesOperations_Click;
                }
            }
        }

        private ToolStripMenuItem _mnuEM_Edit;

        internal ToolStripMenuItem mnuEM_Edit
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _mnuEM_Edit;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_mnuEM_Edit != null)
                {
                    _mnuEM_Edit.Click -= mnuExecMachinesOperations_Click;
                }

                _mnuEM_Edit = value;
                if (_mnuEM_Edit != null)
                {
                    _mnuEM_Edit.Click += mnuExecMachinesOperations_Click;
                }
            }
        }

        private ToolStripMenuItem _mnuEM_Delete;

        internal ToolStripMenuItem mnuEM_Delete
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _mnuEM_Delete;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_mnuEM_Delete != null)
                {
                    _mnuEM_Delete.Click -= mnuExecMachinesOperations_Click;
                }

                _mnuEM_Delete = value;
                if (_mnuEM_Delete != null)
                {
                    _mnuEM_Delete.Click += mnuExecMachinesOperations_Click;
                }
            }
        }

        private ContextMenuStrip _mnuPreItems;

        internal ContextMenuStrip mnuPreItems
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _mnuPreItems;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_mnuPreItems != null)
                {
                    _mnuPreItems.Opening -= mnuPreItems_Opening;
                }

                _mnuPreItems = value;
                if (_mnuPreItems != null)
                {
                    _mnuPreItems.Opening += mnuPreItems_Opening;
                }
            }
        }

        private ToolStripMenuItem _mnuPI_Spcification;

        internal ToolStripMenuItem mnuPI_Spcification
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _mnuPI_Spcification;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_mnuPI_Spcification != null)
                {
                    _mnuPI_Spcification.Click -= cmdPreItems_Click;
                }

                _mnuPI_Spcification = value;
                if (_mnuPI_Spcification != null)
                {
                    _mnuPI_Spcification.Click += cmdPreItems_Click;
                }
            }
        }

        private ContextMenuStrip _mnuPreOperation;

        internal ContextMenuStrip mnuPreOperation
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _mnuPreOperation;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_mnuPreOperation != null)
                {
                    _mnuPreOperation.Opening -= mnuPreItems_Opening;
                }

                _mnuPreOperation = value;
                if (_mnuPreOperation != null)
                {
                    _mnuPreOperation.Opening += mnuPreItems_Opening;
                }
            }
        }

        private ToolStripMenuItem _mnuPO_Spcification;

        internal ToolStripMenuItem mnuPO_Spcification
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _mnuPO_Spcification;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_mnuPO_Spcification != null)
                {
                    _mnuPO_Spcification.Click -= cmdPreOperations_Click;
                }

                _mnuPO_Spcification = value;
                if (_mnuPO_Spcification != null)
                {
                    _mnuPO_Spcification.Click += cmdPreOperations_Click;
                }
            }
        }

        private System.Windows.Forms.Button _cmdCancel;

        internal System.Windows.Forms.Button cmdCancel
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdCancel;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdCancel != null)
                {
                    _cmdCancel.Click -= cmdCancel_Click;
                }

                _cmdCancel = value;
                if (_cmdCancel != null)
                {
                    _cmdCancel.Click += cmdCancel_Click;
                }
            }
        }

        internal DataGridViewTextBoxColumn DataGridViewTextBoxColumn2;
        internal DataGridViewTextBoxColumn DataGridViewTextBoxColumn4;
        internal DataGridViewTextBoxColumn Column1;
        internal DataGridViewTextBoxColumn DataGridViewTextBoxColumn1;
        internal DataGridViewTextBoxColumn Column2;
        internal DataGridViewTextBoxColumn Column3;
        internal DataGridViewTextBoxColumn Column4;
        internal DataGridViewTextBoxColumn OperationCode;
        internal DataGridViewTextBoxColumn OperationTitle;
        internal DataGridViewTextBoxColumn ActivityGroup;
    }
}