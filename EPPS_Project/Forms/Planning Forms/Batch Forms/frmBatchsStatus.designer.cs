using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmBatchsStatus : Form
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
            var DataGridViewCellStyle1 = new DataGridViewCellStyle();
            var DataGridViewCellStyle2 = new DataGridViewCellStyle();
            var DataGridViewCellStyle3 = new DataGridViewCellStyle();
            var DataGridViewCellStyle4 = new DataGridViewCellStyle();
            var DataGridViewCellStyle5 = new DataGridViewCellStyle();
            var DataGridViewCellStyle6 = new DataGridViewCellStyle();
            var DataGridViewCellStyle7 = new DataGridViewCellStyle();
            var DataGridViewCellStyle8 = new DataGridViewCellStyle();
            var DataGridViewCellStyle9 = new DataGridViewCellStyle();
            var DataGridViewCellStyle10 = new DataGridViewCellStyle();
            var DataGridViewCellStyle11 = new DataGridViewCellStyle();
            var DataGridViewCellStyle12 = new DataGridViewCellStyle();
            var DataGridViewCellStyle13 = new DataGridViewCellStyle();
            var DataGridViewCellStyle14 = new DataGridViewCellStyle();
            var DataGridViewCellStyle15 = new DataGridViewCellStyle();
            var DataGridViewCellStyle16 = new DataGridViewCellStyle();
            var DataGridViewCellStyle17 = new DataGridViewCellStyle();
            var TreeNode1 = new TreeNode("جزء 1");
            var TreeNode2 = new TreeNode("جزء 2");
            var TreeNode3 = new TreeNode("اجزای درخت محصول", new TreeNode[] { TreeNode1, TreeNode2 });
            Panel1 = new System.Windows.Forms.Panel();
            TabControl1 = new TabControl();
            TabPage1 = new TabPage();
            dgBatchProductionOperations = new DataGridView();
            colOperationCode1 = new DataGridViewTextBoxColumn();
            colOperationName1 = new DataGridViewTextBoxColumn();
            colPlanningQuantity1 = new DataGridViewTextBoxColumn();
            colProductionQuantity1 = new DataGridViewTextBoxColumn();
            colUnProductionQuantity1 = new DataGridViewTextBoxColumn();
            TabPage2 = new TabPage();
            dgBatchUnProductionOperations = new DataGridView();
            DataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
            TabPage3 = new TabPage();
            dgProductionQuantityList = new DataGridView();
            DataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            colTotalProduction = new DataGridViewTextBoxColumn();
            GroupBox2 = new GroupBox();
            _cmdCalcProductionQuantity = new System.Windows.Forms.Button();
            _cmdCalcProductionQuantity.Click += new EventHandler(cmdCalcProductionQuantity_Click);
            txtProductionFrom = new PSB_FarsiDateControl.PSB_DateControl();
            txtProductionDate = new PSB_FarsiDateControl.PSB_DateControl();
            _rbProductionQuantity = new RadioButton();
            _rbProductionQuantity.CheckedChanged += new EventHandler(rbProductionQuantity_CheckedChanged);
            txtProductionQuantity = new NumericUpDown();
            rbProductionDate = new RadioButton();
            Label18 = new Label();
            _dgOperations = new DataGridView();
            _dgOperations.KeyDown += new KeyEventHandler(dgList_KeyDown);
            colOperationCode = new DataGridViewTextBoxColumn();
            colOperationName = new DataGridViewTextBoxColumn();
            colPlanningQuantity = new DataGridViewTextBoxColumn();
            ColProductionQuantity = new DataGridViewTextBoxColumn();
            colUnProductionQuantity = new DataGridViewTextBoxColumn();
            Panel2 = new System.Windows.Forms.Panel();
            _cmdBatchProgressCalc = new System.Windows.Forms.Button();
            _cmdBatchProgressCalc.Click += new EventHandler(cmdBatchProgressCalc_Click);
            txtCalcDate = new PSB_FarsiDateControl.PSB_DateControl();
            cbBatchs = new ComboBox();
            Label6 = new Label();
            _cmdExit = new System.Windows.Forms.Button();
            _cmdExit.Click += new EventHandler(cmdExit_Click);
            _TreeView1 = new System.Windows.Forms.TreeView();
            _TreeView1.AfterSelect += new TreeViewEventHandler(TreeView1_AfterSelect);
            Label1 = new Label();
            lblBatchQuantity = new Label();
            lblBatchProduction = new Label();
            Label4 = new Label();
            lblBatchUnProduction = new Label();
            Label7 = new Label();
            lblBatchDetourPercent = new Label();
            Label9 = new Label();
            lblBatchState = new Label();
            Label11 = new Label();
            lblPlanningQuantity = new Label();
            Label3 = new Label();
            lblBatchDetourHour = new Label();
            Label8 = new Label();
            Label5 = new Label();
            Label10 = new Label();
            lblPlaningStartDate = new Label();
            lblProductionStartDate = new Label();
            Label2 = new Label();
            lblBatchProductionProgress = new Label();
            lblPlaningEndDate = new Label();
            lblProductionEndDate = new Label();
            Label12 = new Label();
            Label13 = new Label();
            _tmrLoadOvers = new Timer(components);
            _tmrLoadOvers.Tick += new EventHandler(tmrLoadOvers_Tick);
            _tmrBatchProductionOperations = new Timer(components);
            _tmrBatchProductionOperations.Tick += new EventHandler(tmrBatchProductionOperations_Tick);
            Label14 = new Label();
            lblDoneQuantity = new Label();
            GroupBox1 = new GroupBox();
            lblDetourDescription = new Label();
            Label17 = new Label();
            lblPlanningDuration = new Label();
            Label15 = new Label();
            lblProductionDuration = new Label();
            Panel1.SuspendLayout();
            TabControl1.SuspendLayout();
            TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgBatchProductionOperations).BeginInit();
            TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgBatchUnProductionOperations).BeginInit();
            TabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgProductionQuantityList).BeginInit();
            GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtProductionQuantity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_dgOperations).BeginInit();
            Panel2.SuspendLayout();
            GroupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // Panel1
            // 
            Panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            Panel1.BorderStyle = BorderStyle.Fixed3D;
            Panel1.Controls.Add(TabControl1);
            Panel1.Controls.Add(_dgOperations);
            Panel1.Location = new Point(6, 58);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(619, 475);
            Panel1.TabIndex = 1;
            // 
            // TabControl1
            // 
            TabControl1.Controls.Add(TabPage1);
            TabControl1.Controls.Add(TabPage2);
            TabControl1.Controls.Add(TabPage3);
            TabControl1.Dock = DockStyle.Fill;
            TabControl1.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            TabControl1.Location = new Point(0, 217);
            TabControl1.Name = "TabControl1";
            TabControl1.RightToLeftLayout = true;
            TabControl1.SelectedIndex = 0;
            TabControl1.Size = new Size(615, 254);
            TabControl1.TabIndex = 39;
            // 
            // TabPage1
            // 
            TabPage1.Controls.Add(dgBatchProductionOperations);
            TabPage1.Location = new Point(4, 22);
            TabPage1.Name = "TabPage1";
            TabPage1.Padding = new Padding(3);
            TabPage1.Size = new Size(607, 228);
            TabPage1.TabIndex = 0;
            TabPage1.Text = "لیست عملیات های تولید شدۀ بچ";
            TabPage1.UseVisualStyleBackColor = true;
            // 
            // dgBatchProductionOperations
            // 
            dgBatchProductionOperations.AllowUserToAddRows = false;
            dgBatchProductionOperations.AllowUserToDeleteRows = false;
            DataGridViewCellStyle1.BackColor = Color.MistyRose;
            dgBatchProductionOperations.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1;
            dgBatchProductionOperations.BackgroundColor = SystemColors.ControlDark;
            DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle2.BackColor = SystemColors.Control;
            DataGridViewCellStyle2.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgBatchProductionOperations.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2;
            dgBatchProductionOperations.ColumnHeadersHeight = 25;
            dgBatchProductionOperations.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgBatchProductionOperations.Columns.AddRange(new DataGridViewColumn[] { colOperationCode1, colOperationName1, colPlanningQuantity1, colProductionQuantity1, colUnProductionQuantity1 });
            DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
            DataGridViewCellStyle3.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            DataGridViewCellStyle3.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            DataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgBatchProductionOperations.DefaultCellStyle = DataGridViewCellStyle3;
            dgBatchProductionOperations.Dock = DockStyle.Fill;
            dgBatchProductionOperations.Location = new Point(3, 3);
            dgBatchProductionOperations.MultiSelect = false;
            dgBatchProductionOperations.Name = "dgBatchProductionOperations";
            dgBatchProductionOperations.ReadOnly = true;
            dgBatchProductionOperations.RowHeadersWidth = 15;
            dgBatchProductionOperations.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle4.BackColor = SystemColors.Info;
            DataGridViewCellStyle4.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle4.ForeColor = Color.Black;
            DataGridViewCellStyle4.SelectionBackColor = Color.RoyalBlue;
            dgBatchProductionOperations.RowsDefaultCellStyle = DataGridViewCellStyle4;
            dgBatchProductionOperations.RowTemplate.Resizable = DataGridViewTriState.True;
            dgBatchProductionOperations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgBatchProductionOperations.Size = new Size(601, 222);
            dgBatchProductionOperations.TabIndex = 2;
            // 
            // colOperationCode1
            // 
            colOperationCode1.FillWeight = 21.20426f;
            colOperationCode1.HeaderText = "کد عملیات";
            colOperationCode1.Name = "colOperationCode1";
            colOperationCode1.ReadOnly = true;
            colOperationCode1.Width = 110;
            // 
            // colOperationName1
            // 
            colOperationName1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colOperationName1.FillWeight = 75.17654f;
            colOperationName1.HeaderText = "عنوان عملیات";
            colOperationName1.Name = "colOperationName1";
            colOperationName1.ReadOnly = true;
            // 
            // colPlanningQuantity1
            // 
            colPlanningQuantity1.HeaderText = "تعداد برنامه ریزی شده";
            colPlanningQuantity1.Name = "colPlanningQuantity1";
            colPlanningQuantity1.ReadOnly = true;
            colPlanningQuantity1.Width = 115;
            // 
            // colProductionQuantity1
            // 
            colProductionQuantity1.FillWeight = 100.5735f;
            colProductionQuantity1.HeaderText = "تعداد تولید شده";
            colProductionQuantity1.Name = "colProductionQuantity1";
            colProductionQuantity1.ReadOnly = true;
            // 
            // colUnProductionQuantity1
            // 
            colUnProductionQuantity1.FillWeight = 203.0457f;
            colUnProductionQuantity1.HeaderText = "تعداد تولید نشده";
            colUnProductionQuantity1.Name = "colUnProductionQuantity1";
            colUnProductionQuantity1.ReadOnly = true;
            // 
            // TabPage2
            // 
            TabPage2.Controls.Add(dgBatchUnProductionOperations);
            TabPage2.Location = new Point(4, 22);
            TabPage2.Name = "TabPage2";
            TabPage2.Padding = new Padding(3);
            TabPage2.Size = new Size(607, 228);
            TabPage2.TabIndex = 1;
            TabPage2.Text = "لیست عملیات های تولید نشدۀ بچ";
            TabPage2.UseVisualStyleBackColor = true;
            // 
            // dgBatchUnProductionOperations
            // 
            dgBatchUnProductionOperations.AllowUserToAddRows = false;
            dgBatchUnProductionOperations.AllowUserToDeleteRows = false;
            DataGridViewCellStyle5.BackColor = Color.Linen;
            dgBatchUnProductionOperations.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5;
            dgBatchUnProductionOperations.BackgroundColor = SystemColors.ControlDark;
            DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle6.BackColor = SystemColors.Control;
            DataGridViewCellStyle6.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
            DataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgBatchUnProductionOperations.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6;
            dgBatchUnProductionOperations.ColumnHeadersHeight = 25;
            dgBatchUnProductionOperations.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgBatchUnProductionOperations.Columns.AddRange(new DataGridViewColumn[] { DataGridViewTextBoxColumn6, DataGridViewTextBoxColumn7, DataGridViewTextBoxColumn8 });
            DataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleRight;
            DataGridViewCellStyle7.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            DataGridViewCellStyle7.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle7.ForeColor = SystemColors.ControlText;
            DataGridViewCellStyle7.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle7.WrapMode = DataGridViewTriState.False;
            dgBatchUnProductionOperations.DefaultCellStyle = DataGridViewCellStyle7;
            dgBatchUnProductionOperations.Dock = DockStyle.Fill;
            dgBatchUnProductionOperations.Location = new Point(3, 3);
            dgBatchUnProductionOperations.MultiSelect = false;
            dgBatchUnProductionOperations.Name = "dgBatchUnProductionOperations";
            dgBatchUnProductionOperations.ReadOnly = true;
            dgBatchUnProductionOperations.RowHeadersWidth = 15;
            dgBatchUnProductionOperations.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            DataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle8.BackColor = SystemColors.Info;
            DataGridViewCellStyle8.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle8.ForeColor = Color.Black;
            DataGridViewCellStyle8.SelectionBackColor = Color.RoyalBlue;
            dgBatchUnProductionOperations.RowsDefaultCellStyle = DataGridViewCellStyle8;
            dgBatchUnProductionOperations.RowTemplate.Resizable = DataGridViewTriState.True;
            dgBatchUnProductionOperations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgBatchUnProductionOperations.Size = new Size(601, 222);
            dgBatchUnProductionOperations.TabIndex = 3;
            // 
            // DataGridViewTextBoxColumn6
            // 
            DataGridViewTextBoxColumn6.FillWeight = 21.20426f;
            DataGridViewTextBoxColumn6.HeaderText = "کد عملیات";
            DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6";
            DataGridViewTextBoxColumn6.ReadOnly = true;
            DataGridViewTextBoxColumn6.Width = 110;
            // 
            // DataGridViewTextBoxColumn7
            // 
            DataGridViewTextBoxColumn7.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataGridViewTextBoxColumn7.FillWeight = 75.17654f;
            DataGridViewTextBoxColumn7.HeaderText = "عنوان عملیات";
            DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7";
            DataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // DataGridViewTextBoxColumn8
            // 
            DataGridViewTextBoxColumn8.HeaderText = "تعداد برنامه ریزی شده";
            DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8";
            DataGridViewTextBoxColumn8.ReadOnly = true;
            DataGridViewTextBoxColumn8.Width = 115;
            // 
            // TabPage3
            // 
            TabPage3.Controls.Add(dgProductionQuantityList);
            TabPage3.Controls.Add(GroupBox2);
            TabPage3.Location = new Point(4, 22);
            TabPage3.Name = "TabPage3";
            TabPage3.Padding = new Padding(3);
            TabPage3.Size = new Size(607, 228);
            TabPage3.TabIndex = 2;
            TabPage3.Text = "پیش بینی تعداد تولید محصول";
            TabPage3.UseVisualStyleBackColor = true;
            // 
            // dgProductionQuantityList
            // 
            dgProductionQuantityList.AllowUserToAddRows = false;
            dgProductionQuantityList.AllowUserToDeleteRows = false;
            DataGridViewCellStyle9.BackColor = Color.Khaki;
            dgProductionQuantityList.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle9;
            dgProductionQuantityList.BackgroundColor = SystemColors.ControlDark;
            DataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle10.BackColor = SystemColors.Control;
            DataGridViewCellStyle10.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle10.ForeColor = SystemColors.WindowText;
            DataGridViewCellStyle10.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle10.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle10.WrapMode = DataGridViewTriState.False;
            dgProductionQuantityList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle10;
            dgProductionQuantityList.ColumnHeadersHeight = 25;
            dgProductionQuantityList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgProductionQuantityList.Columns.AddRange(new DataGridViewColumn[] { DataGridViewTextBoxColumn1, DataGridViewTextBoxColumn2, colTotalProduction });
            DataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleRight;
            DataGridViewCellStyle11.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            DataGridViewCellStyle11.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle11.ForeColor = SystemColors.ControlText;
            DataGridViewCellStyle11.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle11.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle11.WrapMode = DataGridViewTriState.False;
            dgProductionQuantityList.DefaultCellStyle = DataGridViewCellStyle11;
            dgProductionQuantityList.Dock = DockStyle.Fill;
            dgProductionQuantityList.Location = new Point(3, 78);
            dgProductionQuantityList.MultiSelect = false;
            dgProductionQuantityList.Name = "dgProductionQuantityList";
            dgProductionQuantityList.ReadOnly = true;
            dgProductionQuantityList.RowHeadersWidth = 15;
            dgProductionQuantityList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            DataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle12.BackColor = SystemColors.Info;
            DataGridViewCellStyle12.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle12.ForeColor = Color.Black;
            DataGridViewCellStyle12.SelectionBackColor = Color.RoyalBlue;
            dgProductionQuantityList.RowsDefaultCellStyle = DataGridViewCellStyle12;
            dgProductionQuantityList.RowTemplate.Resizable = DataGridViewTriState.True;
            dgProductionQuantityList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgProductionQuantityList.Size = new Size(601, 147);
            dgProductionQuantityList.TabIndex = 189;
            // 
            // DataGridViewTextBoxColumn1
            // 
            DataGridViewTextBoxColumn1.FillWeight = 21.20426f;
            DataGridViewTextBoxColumn1.HeaderText = "تعداد محصول تولیدی";
            DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1";
            DataGridViewTextBoxColumn1.ReadOnly = true;
            DataGridViewTextBoxColumn1.Width = 120;
            // 
            // DataGridViewTextBoxColumn2
            // 
            DataGridViewTextBoxColumn2.HeaderText = "تاریخ تولید";
            DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2";
            DataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // colTotalProduction
            // 
            colTotalProduction.HeaderText = "کل محصول تولید شده";
            colTotalProduction.Name = "colTotalProduction";
            colTotalProduction.ReadOnly = true;
            colTotalProduction.Width = 150;
            // 
            // GroupBox2
            // 
            GroupBox2.Controls.Add(_cmdCalcProductionQuantity);
            GroupBox2.Controls.Add(txtProductionFrom);
            GroupBox2.Controls.Add(txtProductionDate);
            GroupBox2.Controls.Add(_rbProductionQuantity);
            GroupBox2.Controls.Add(txtProductionQuantity);
            GroupBox2.Controls.Add(rbProductionDate);
            GroupBox2.Controls.Add(Label18);
            GroupBox2.Dock = DockStyle.Top;
            GroupBox2.Location = new Point(3, 3);
            GroupBox2.Name = "GroupBox2";
            GroupBox2.Size = new Size(601, 75);
            GroupBox2.TabIndex = 188;
            GroupBox2.TabStop = false;
            GroupBox2.Text = "شرایط محاسبه";
            // 
            // cmdCalcProductionQuantity
            // 
            _cmdCalcProductionQuantity.BackColor = Color.Transparent;
            _cmdCalcProductionQuantity.Font = new Font("Tahoma", 9.0f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdCalcProductionQuantity.ForeColor = Color.Blue;
            _cmdCalcProductionQuantity.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdCalcProductionQuantity.Location = new Point(6, 30);
            _cmdCalcProductionQuantity.Name = "_cmdCalcProductionQuantity";
            _cmdCalcProductionQuantity.RightToLeft = RightToLeft.No;
            _cmdCalcProductionQuantity.Size = new Size(77, 25);
            _cmdCalcProductionQuantity.TabIndex = 187;
            _cmdCalcProductionQuantity.Text = "محاسبه";
            _cmdCalcProductionQuantity.UseVisualStyleBackColor = false;
            // 
            // txtProductionFrom
            // 
            txtProductionFrom.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtProductionFrom.AutoValidate = AutoValidate.Disable;
            txtProductionFrom.BackColor = Color.White;
            txtProductionFrom.BackColorDateBox = Color.White;
            txtProductionFrom.DateButtonShow = true;
            txtProductionFrom.EnableDateText = true;
            txtProductionFrom.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtProductionFrom.FontMyDate = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtProductionFrom.Location = new Point(107, 31);
            txtProductionFrom.Margin = new Padding(4);
            txtProductionFrom.MinimumSize = new Size(96, 24);
            txtProductionFrom.Name = "txtProductionFrom";
            txtProductionFrom.Size = new Size(104, 24);
            txtProductionFrom.TabIndex = 184;
            // 
            // txtProductionDate
            // 
            txtProductionDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtProductionDate.AutoValidate = AutoValidate.Disable;
            txtProductionDate.BackColor = Color.White;
            txtProductionDate.BackColorDateBox = Color.White;
            txtProductionDate.DateButtonShow = true;
            txtProductionDate.Enabled = false;
            txtProductionDate.EnableDateText = true;
            txtProductionDate.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtProductionDate.FontMyDate = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtProductionDate.Location = new Point(356, 46);
            txtProductionDate.Margin = new Padding(4);
            txtProductionDate.MinimumSize = new Size(96, 24);
            txtProductionDate.Name = "txtProductionDate";
            txtProductionDate.Size = new Size(104, 24);
            txtProductionDate.TabIndex = 183;
            // 
            // rbProductionQuantity
            // 
            _rbProductionQuantity.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _rbProductionQuantity.Checked = true;
            _rbProductionQuantity.Location = new Point(462, 18);
            _rbProductionQuantity.Name = "_rbProductionQuantity";
            _rbProductionQuantity.Size = new Size(131, 17);
            _rbProductionQuantity.TabIndex = 184;
            _rbProductionQuantity.TabStop = true;
            _rbProductionQuantity.Text = "تعداد محصول مورد نظر:";
            _rbProductionQuantity.UseVisualStyleBackColor = true;
            // 
            // txtProductionQuantity
            // 
            txtProductionQuantity.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtProductionQuantity.Location = new Point(356, 17);
            txtProductionQuantity.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            txtProductionQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            txtProductionQuantity.Name = "txtProductionQuantity";
            txtProductionQuantity.Size = new Size(104, 21);
            txtProductionQuantity.TabIndex = 186;
            txtProductionQuantity.ThousandsSeparator = true;
            txtProductionQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // rbProductionDate
            // 
            rbProductionDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            rbProductionDate.Location = new Point(460, 49);
            rbProductionDate.Name = "rbProductionDate";
            rbProductionDate.Size = new Size(133, 17);
            rbProductionDate.TabIndex = 185;
            rbProductionDate.Text = "محصول تولیدی در تاریخ:";
            rbProductionDate.UseVisualStyleBackColor = true;
            // 
            // Label18
            // 
            Label18.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label18.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label18.ForeColor = Color.Black;
            Label18.Location = new Point(211, 35);
            Label18.Name = "Label18";
            Label18.RightToLeft = RightToLeft.Yes;
            Label18.Size = new Size(83, 16);
            Label18.TabIndex = 191;
            Label18.Text = "محاسبه از تاریخ:";
            // 
            // dgOperations
            // 
            _dgOperations.AllowUserToAddRows = false;
            _dgOperations.AllowUserToDeleteRows = false;
            DataGridViewCellStyle13.BackColor = Color.White;
            _dgOperations.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle13;
            _dgOperations.BackgroundColor = SystemColors.ControlDark;
            DataGridViewCellStyle14.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle14.BackColor = SystemColors.Control;
            DataGridViewCellStyle14.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle14.ForeColor = SystemColors.WindowText;
            DataGridViewCellStyle14.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle14.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle14.WrapMode = DataGridViewTriState.False;
            _dgOperations.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle14;
            _dgOperations.ColumnHeadersHeight = 25;
            _dgOperations.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            _dgOperations.Columns.AddRange(new DataGridViewColumn[] { colOperationCode, colOperationName, colPlanningQuantity, ColProductionQuantity, colUnProductionQuantity });
            DataGridViewCellStyle15.Alignment = DataGridViewContentAlignment.MiddleRight;
            DataGridViewCellStyle15.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            DataGridViewCellStyle15.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle15.ForeColor = SystemColors.ControlText;
            DataGridViewCellStyle15.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle15.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle15.WrapMode = DataGridViewTriState.False;
            _dgOperations.DefaultCellStyle = DataGridViewCellStyle15;
            _dgOperations.Dock = DockStyle.Top;
            _dgOperations.Location = new Point(0, 0);
            _dgOperations.MultiSelect = false;
            _dgOperations.Name = "_dgOperations";
            _dgOperations.ReadOnly = true;
            DataGridViewCellStyle16.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle16.BackColor = SystemColors.Control;
            DataGridViewCellStyle16.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle16.ForeColor = SystemColors.WindowText;
            DataGridViewCellStyle16.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle16.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle16.WrapMode = DataGridViewTriState.True;
            _dgOperations.RowHeadersDefaultCellStyle = DataGridViewCellStyle16;
            _dgOperations.RowHeadersWidth = 15;
            _dgOperations.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            DataGridViewCellStyle17.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle17.BackColor = SystemColors.Info;
            DataGridViewCellStyle17.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle17.ForeColor = Color.Black;
            DataGridViewCellStyle17.SelectionBackColor = Color.RoyalBlue;
            _dgOperations.RowsDefaultCellStyle = DataGridViewCellStyle17;
            _dgOperations.RowTemplate.Resizable = DataGridViewTriState.True;
            _dgOperations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgOperations.Size = new Size(615, 217);
            _dgOperations.TabIndex = 1;
            // 
            // colOperationCode
            // 
            colOperationCode.FillWeight = 21.20426f;
            colOperationCode.HeaderText = "کد عملیات";
            colOperationCode.Name = "colOperationCode";
            colOperationCode.ReadOnly = true;
            colOperationCode.Width = 110;
            // 
            // colOperationName
            // 
            colOperationName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colOperationName.FillWeight = 75.17654f;
            colOperationName.HeaderText = "عنوان عملیات";
            colOperationName.Name = "colOperationName";
            colOperationName.ReadOnly = true;
            // 
            // colPlanningQuantity
            // 
            colPlanningQuantity.HeaderText = "تعداد برنامه ریزی شده";
            colPlanningQuantity.Name = "colPlanningQuantity";
            colPlanningQuantity.ReadOnly = true;
            colPlanningQuantity.Width = 115;
            // 
            // ColProductionQuantity
            // 
            ColProductionQuantity.FillWeight = 100.5735f;
            ColProductionQuantity.HeaderText = "تعداد تولید شده";
            ColProductionQuantity.Name = "ColProductionQuantity";
            ColProductionQuantity.ReadOnly = true;
            // 
            // colUnProductionQuantity
            // 
            colUnProductionQuantity.FillWeight = 203.0457f;
            colUnProductionQuantity.HeaderText = "تعداد تولید نشده";
            colUnProductionQuantity.Name = "colUnProductionQuantity";
            colUnProductionQuantity.ReadOnly = true;
            // 
            // Panel2
            // 
            Panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Panel2.BackColor = SystemColors.Control;
            Panel2.BorderStyle = BorderStyle.FixedSingle;
            Panel2.Controls.Add(_cmdBatchProgressCalc);
            Panel2.Controls.Add(txtCalcDate);
            Panel2.Controls.Add(cbBatchs);
            Panel2.Controls.Add(Label6);
            Panel2.Controls.Add(_cmdExit);
            Panel2.Location = new Point(6, 5);
            Panel2.Name = "Panel2";
            Panel2.Size = new Size(932, 47);
            Panel2.TabIndex = 2;
            // 
            // cmdBatchProgressCalc
            // 
            _cmdBatchProgressCalc.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _cmdBatchProgressCalc.BackColor = Color.Transparent;
            _cmdBatchProgressCalc.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdBatchProgressCalc.ForeColor = Color.Blue;
            _cmdBatchProgressCalc.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdBatchProgressCalc.Location = new Point(433, 9);
            _cmdBatchProgressCalc.Name = "_cmdBatchProgressCalc";
            _cmdBatchProgressCalc.RightToLeft = RightToLeft.No;
            _cmdBatchProgressCalc.Size = new Size(123, 25);
            _cmdBatchProgressCalc.TabIndex = 183;
            _cmdBatchProgressCalc.Text = "محاسبۀ پیشرفت بچ";
            _cmdBatchProgressCalc.UseVisualStyleBackColor = false;
            // 
            // txtCalcDate
            // 
            txtCalcDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtCalcDate.AutoValidate = AutoValidate.Disable;
            txtCalcDate.BackColor = Color.White;
            txtCalcDate.BackColorDateBox = Color.White;
            txtCalcDate.DateButtonShow = true;
            txtCalcDate.EnableDateText = true;
            txtCalcDate.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtCalcDate.FontMyDate = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtCalcDate.Location = new Point(562, 10);
            txtCalcDate.Margin = new Padding(4);
            txtCalcDate.MinimumSize = new Size(96, 24);
            txtCalcDate.Name = "txtCalcDate";
            txtCalcDate.Size = new Size(104, 24);
            txtCalcDate.TabIndex = 182;
            // 
            // cbBatchs
            // 
            cbBatchs.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbBatchs.FlatStyle = FlatStyle.Flat;
            cbBatchs.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cbBatchs.FormattingEnabled = true;
            cbBatchs.Location = new Point(674, 12);
            cbBatchs.Name = "cbBatchs";
            cbBatchs.Size = new Size(208, 21);
            cbBatchs.TabIndex = 181;
            // 
            // Label6
            // 
            Label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label6.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label6.Location = new Point(882, 14);
            Label6.Name = "Label6";
            Label6.RightToLeft = RightToLeft.Yes;
            Label6.Size = new Size(41, 18);
            Label6.TabIndex = 180;
            Label6.Text = "کد بچ:";
            // 
            // cmdExit
            // 
            _cmdExit.BackColor = Color.Transparent;
            _cmdExit.DialogResult = DialogResult.Cancel;
            _cmdExit.Font = new Font("Tahoma", 9.0f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdExit.ForeColor = Color.Red;
            _cmdExit.Location = new Point(12, 9);
            _cmdExit.Name = "_cmdExit";
            _cmdExit.Size = new Size(91, 27);
            _cmdExit.TabIndex = 5;
            _cmdExit.Text = "خروج";
            _cmdExit.UseVisualStyleBackColor = false;
            // 
            // TreeView1
            // 
            _TreeView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            _TreeView1.BorderStyle = BorderStyle.FixedSingle;
            _TreeView1.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _TreeView1.HideSelection = false;
            _TreeView1.Location = new Point(629, 277);
            _TreeView1.Name = "_TreeView1";
            TreeNode1.Name = "Node1";
            TreeNode1.Text = "جزء 1";
            TreeNode2.Name = "Node2";
            TreeNode2.Text = "جزء 2";
            TreeNode3.Name = "Node0";
            TreeNode3.Text = "اجزای درخت محصول";
            _TreeView1.Nodes.AddRange(new TreeNode[] { TreeNode3 });
            _TreeView1.RightToLeftLayout = true;
            _TreeView1.Size = new Size(309, 256);
            _TreeView1.TabIndex = 4;
            _TreeView1.TabStop = false;
            // 
            // Label1
            // 
            Label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label1.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label1.ForeColor = Color.Black;
            Label1.Location = new Point(892, 77);
            Label1.Name = "Label1";
            Label1.RightToLeft = RightToLeft.Yes;
            Label1.Size = new Size(49, 16);
            Label1.TabIndex = 181;
            Label1.Text = "تعداد بچ:";
            // 
            // lblBatchQuantity
            // 
            lblBatchQuantity.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblBatchQuantity.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblBatchQuantity.ForeColor = Color.Blue;
            lblBatchQuantity.Location = new Point(823, 77);
            lblBatchQuantity.Name = "lblBatchQuantity";
            lblBatchQuantity.RightToLeft = RightToLeft.No;
            lblBatchQuantity.Size = new Size(57, 16);
            lblBatchQuantity.TabIndex = 182;
            lblBatchQuantity.Text = "#";
            lblBatchQuantity.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblBatchProduction
            // 
            lblBatchProduction.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblBatchProduction.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblBatchProduction.ForeColor = Color.Blue;
            lblBatchProduction.Location = new Point(790, 146);
            lblBatchProduction.Name = "lblBatchProduction";
            lblBatchProduction.RightToLeft = RightToLeft.No;
            lblBatchProduction.Size = new Size(69, 17);
            lblBatchProduction.TabIndex = 184;
            lblBatchProduction.Text = "#";
            lblBatchProduction.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Label4
            // 
            Label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label4.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label4.ForeColor = Color.Black;
            Label4.Location = new Point(857, 146);
            Label4.Name = "Label4";
            Label4.RightToLeft = RightToLeft.Yes;
            Label4.Size = new Size(84, 16);
            Label4.TabIndex = 183;
            Label4.Text = "تعداد تولید شده:";
            // 
            // lblBatchUnProduction
            // 
            lblBatchUnProduction.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblBatchUnProduction.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblBatchUnProduction.ForeColor = Color.Blue;
            lblBatchUnProduction.Location = new Point(691, 169);
            lblBatchUnProduction.Name = "lblBatchUnProduction";
            lblBatchUnProduction.RightToLeft = RightToLeft.No;
            lblBatchUnProduction.Size = new Size(128, 17);
            lblBatchUnProduction.TabIndex = 186;
            lblBatchUnProduction.Text = "#";
            lblBatchUnProduction.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Label7
            // 
            Label7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label7.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label7.ForeColor = Color.Black;
            Label7.Location = new Point(816, 169);
            Label7.Name = "Label7";
            Label7.RightToLeft = RightToLeft.Yes;
            Label7.Size = new Size(125, 16);
            Label7.TabIndex = 185;
            Label7.Text = "تعداد محصول تولید نشده:";
            // 
            // lblBatchDetourPercent
            // 
            lblBatchDetourPercent.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblBatchDetourPercent.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblBatchDetourPercent.ForeColor = Color.Blue;
            lblBatchDetourPercent.Location = new Point(218, 17);
            lblBatchDetourPercent.Name = "lblBatchDetourPercent";
            lblBatchDetourPercent.RightToLeft = RightToLeft.No;
            lblBatchDetourPercent.Size = new Size(57, 17);
            lblBatchDetourPercent.TabIndex = 188;
            lblBatchDetourPercent.Text = "#";
            lblBatchDetourPercent.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Label9
            // 
            Label9.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label9.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label9.ForeColor = Color.Black;
            Label9.Location = new Point(271, 17);
            Label9.Name = "Label9";
            Label9.RightToLeft = RightToLeft.Yes;
            Label9.Size = new Size(36, 16);
            Label9.TabIndex = 187;
            Label9.Text = "درصد:";
            // 
            // lblBatchState
            // 
            lblBatchState.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblBatchState.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblBatchState.ForeColor = Color.Blue;
            lblBatchState.Location = new Point(778, 54);
            lblBatchState.Name = "lblBatchState";
            lblBatchState.RightToLeft = RightToLeft.Yes;
            lblBatchState.Size = new Size(105, 16);
            lblBatchState.TabIndex = 190;
            lblBatchState.Text = "#";
            // 
            // Label11
            // 
            Label11.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label11.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label11.ForeColor = Color.Black;
            Label11.Location = new Point(881, 54);
            Label11.Name = "Label11";
            Label11.RightToLeft = RightToLeft.Yes;
            Label11.Size = new Size(60, 16);
            Label11.TabIndex = 189;
            Label11.Text = "وضعیت بچ:";
            // 
            // lblPlanningQuantity
            // 
            lblPlanningQuantity.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblPlanningQuantity.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblPlanningQuantity.ForeColor = Color.Blue;
            lblPlanningQuantity.Location = new Point(633, 77);
            lblPlanningQuantity.Name = "lblPlanningQuantity";
            lblPlanningQuantity.RightToLeft = RightToLeft.No;
            lblPlanningQuantity.Size = new Size(59, 16);
            lblPlanningQuantity.TabIndex = 192;
            lblPlanningQuantity.Text = "#";
            lblPlanningQuantity.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Label3
            // 
            Label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label3.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label3.ForeColor = Color.Black;
            Label3.Location = new Point(690, 77);
            Label3.Name = "Label3";
            Label3.RightToLeft = RightToLeft.Yes;
            Label3.Size = new Size(89, 16);
            Label3.TabIndex = 191;
            Label3.Text = "تعداد برنامه ریزی:";
            // 
            // lblBatchDetourHour
            // 
            lblBatchDetourHour.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblBatchDetourHour.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblBatchDetourHour.ForeColor = Color.Blue;
            lblBatchDetourHour.Location = new Point(97, 17);
            lblBatchDetourHour.Name = "lblBatchDetourHour";
            lblBatchDetourHour.RightToLeft = RightToLeft.No;
            lblBatchDetourHour.Size = new Size(75, 17);
            lblBatchDetourHour.TabIndex = 194;
            lblBatchDetourHour.Text = "#";
            lblBatchDetourHour.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Label8
            // 
            Label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label8.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label8.ForeColor = Color.Black;
            Label8.Location = new Point(174, 17);
            Label8.Name = "Label8";
            Label8.RightToLeft = RightToLeft.Yes;
            Label8.Size = new Size(37, 16);
            Label8.TabIndex = 193;
            Label8.Text = "میزان:";
            // 
            // Label5
            // 
            Label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label5.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label5.ForeColor = Color.Black;
            Label5.Location = new Point(818, 123);
            Label5.Name = "Label5";
            Label5.RightToLeft = RightToLeft.Yes;
            Label5.Size = new Size(123, 16);
            Label5.TabIndex = 197;
            Label5.Text = "شروع / پایان برنامه ریزی:";
            // 
            // Label10
            // 
            Label10.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label10.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label10.ForeColor = Color.Black;
            Label10.Location = new Point(848, 100);
            Label10.Name = "Label10";
            Label10.RightToLeft = RightToLeft.Yes;
            Label10.Size = new Size(93, 16);
            Label10.TabIndex = 195;
            Label10.Text = "شروع / پایان تولید:";
            // 
            // lblPlaningStartDate
            // 
            lblPlaningStartDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblPlaningStartDate.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblPlaningStartDate.ForeColor = Color.Blue;
            lblPlaningStartDate.Location = new Point(757, 123);
            lblPlaningStartDate.Name = "lblPlaningStartDate";
            lblPlaningStartDate.RightToLeft = RightToLeft.No;
            lblPlaningStartDate.Size = new Size(64, 16);
            lblPlaningStartDate.TabIndex = 198;
            lblPlaningStartDate.Text = "#";
            lblPlaningStartDate.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblProductionStartDate
            // 
            lblProductionStartDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblProductionStartDate.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblProductionStartDate.ForeColor = Color.Blue;
            lblProductionStartDate.Location = new Point(757, 100);
            lblProductionStartDate.Name = "lblProductionStartDate";
            lblProductionStartDate.RightToLeft = RightToLeft.No;
            lblProductionStartDate.Size = new Size(64, 16);
            lblProductionStartDate.TabIndex = 196;
            lblProductionStartDate.Text = "#";
            lblProductionStartDate.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Label2
            // 
            Label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label2.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label2.ForeColor = Color.Black;
            Label2.Location = new Point(714, 54);
            Label2.Name = "Label2";
            Label2.RightToLeft = RightToLeft.Yes;
            Label2.Size = new Size(65, 16);
            Label2.TabIndex = 199;
            Label2.Text = "پیشرفت بچ:";
            // 
            // lblBatchProductionProgress
            // 
            lblBatchProductionProgress.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblBatchProductionProgress.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblBatchProductionProgress.ForeColor = Color.Blue;
            lblBatchProductionProgress.Location = new Point(648, 54);
            lblBatchProductionProgress.Name = "lblBatchProductionProgress";
            lblBatchProductionProgress.RightToLeft = RightToLeft.No;
            lblBatchProductionProgress.Size = new Size(44, 16);
            lblBatchProductionProgress.TabIndex = 200;
            lblBatchProductionProgress.Text = "#";
            lblBatchProductionProgress.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblPlaningEndDate
            // 
            lblPlaningEndDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblPlaningEndDate.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblPlaningEndDate.ForeColor = Color.Blue;
            lblPlaningEndDate.Location = new Point(681, 123);
            lblPlaningEndDate.Name = "lblPlaningEndDate";
            lblPlaningEndDate.RightToLeft = RightToLeft.No;
            lblPlaningEndDate.Size = new Size(64, 16);
            lblPlaningEndDate.TabIndex = 204;
            lblPlaningEndDate.Text = "#";
            lblPlaningEndDate.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblProductionEndDate
            // 
            lblProductionEndDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblProductionEndDate.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblProductionEndDate.ForeColor = Color.Blue;
            lblProductionEndDate.Location = new Point(681, 100);
            lblProductionEndDate.Name = "lblProductionEndDate";
            lblProductionEndDate.RightToLeft = RightToLeft.No;
            lblProductionEndDate.Size = new Size(64, 16);
            lblProductionEndDate.TabIndex = 202;
            lblProductionEndDate.Text = "#";
            lblProductionEndDate.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Label12
            // 
            Label12.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label12.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label12.ForeColor = Color.Blue;
            Label12.Location = new Point(746, 100);
            Label12.Name = "Label12";
            Label12.RightToLeft = RightToLeft.No;
            Label12.Size = new Size(13, 16);
            Label12.TabIndex = 205;
            Label12.Text = "/";
            Label12.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Label13
            // 
            Label13.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label13.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label13.ForeColor = Color.Blue;
            Label13.Location = new Point(746, 123);
            Label13.Name = "Label13";
            Label13.RightToLeft = RightToLeft.No;
            Label13.Size = new Size(13, 16);
            Label13.TabIndex = 206;
            Label13.Text = "/";
            Label13.TextAlign = ContentAlignment.MiddleRight;
            // 
            // tmrLoadOvers
            // 
            _tmrLoadOvers.Interval = 50;
            // 
            // tmrBatchProductionOperations
            // 
            _tmrBatchProductionOperations.Interval = 50;
            // 
            // Label14
            // 
            Label14.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label14.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label14.ForeColor = Color.Black;
            Label14.Location = new Point(717, 146);
            Label14.Name = "Label14";
            Label14.RightToLeft = RightToLeft.Yes;
            Label14.Size = new Size(62, 16);
            Label14.TabIndex = 207;
            Label14.Text = "بسته شده:";
            // 
            // lblDoneQuantity
            // 
            lblDoneQuantity.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblDoneQuantity.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblDoneQuantity.ForeColor = Color.Blue;
            lblDoneQuantity.Location = new Point(650, 146);
            lblDoneQuantity.Name = "lblDoneQuantity";
            lblDoneQuantity.RightToLeft = RightToLeft.No;
            lblDoneQuantity.Size = new Size(69, 17);
            lblDoneQuantity.TabIndex = 208;
            lblDoneQuantity.Text = "#";
            lblDoneQuantity.TextAlign = ContentAlignment.MiddleRight;
            // 
            // GroupBox1
            // 
            GroupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            GroupBox1.Controls.Add(lblDetourDescription);
            GroupBox1.Controls.Add(Label17);
            GroupBox1.Controls.Add(lblPlanningDuration);
            GroupBox1.Controls.Add(Label15);
            GroupBox1.Controls.Add(lblProductionDuration);
            GroupBox1.Controls.Add(Label9);
            GroupBox1.Controls.Add(lblBatchDetourPercent);
            GroupBox1.Controls.Add(lblBatchDetourHour);
            GroupBox1.Controls.Add(Label8);
            GroupBox1.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            GroupBox1.Location = new Point(628, 189);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(313, 84);
            GroupBox1.TabIndex = 209;
            GroupBox1.TabStop = false;
            GroupBox1.Text = "انحراف از برنامۀ تولید بچ تا:";
            // 
            // lblDetourDescription
            // 
            lblDetourDescription.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblDetourDescription.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblDetourDescription.ForeColor = Color.Blue;
            lblDetourDescription.Location = new Point(3, 17);
            lblDetourDescription.Name = "lblDetourDescription";
            lblDetourDescription.RightToLeft = RightToLeft.No;
            lblDetourDescription.Size = new Size(86, 17);
            lblDetourDescription.TabIndex = 199;
            lblDetourDescription.Text = "#";
            lblDetourDescription.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label17
            // 
            Label17.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label17.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label17.ForeColor = Color.Black;
            Label17.Location = new Point(154, 64);
            Label17.Name = "Label17";
            Label17.RightToLeft = RightToLeft.Yes;
            Label17.Size = new Size(153, 16);
            Label17.TabIndex = 197;
            Label17.Text = "طول زمان استاندارد برنامه ریزی:";
            // 
            // lblPlanningDuration
            // 
            lblPlanningDuration.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblPlanningDuration.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblPlanningDuration.ForeColor = Color.Blue;
            lblPlanningDuration.Location = new Point(66, 64);
            lblPlanningDuration.Name = "lblPlanningDuration";
            lblPlanningDuration.RightToLeft = RightToLeft.No;
            lblPlanningDuration.Size = new Size(86, 17);
            lblPlanningDuration.TabIndex = 198;
            lblPlanningDuration.Text = "#";
            lblPlanningDuration.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Label15
            // 
            Label15.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label15.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label15.ForeColor = Color.Black;
            Label15.Location = new Point(151, 41);
            Label15.Name = "Label15";
            Label15.RightToLeft = RightToLeft.Yes;
            Label15.Size = new Size(156, 16);
            Label15.TabIndex = 195;
            Label15.Text = "طول زمان استاندارد تولید واقعی:";
            // 
            // lblProductionDuration
            // 
            lblProductionDuration.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblProductionDuration.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblProductionDuration.ForeColor = Color.Blue;
            lblProductionDuration.Location = new Point(66, 41);
            lblProductionDuration.Name = "lblProductionDuration";
            lblProductionDuration.RightToLeft = RightToLeft.No;
            lblProductionDuration.Size = new Size(86, 17);
            lblProductionDuration.TabIndex = 196;
            lblProductionDuration.Text = "#";
            lblProductionDuration.TextAlign = ContentAlignment.MiddleRight;
            // 
            // frmBatchsStatus
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _cmdExit;
            ClientSize = new Size(944, 538);
            Controls.Add(Label14);
            Controls.Add(lblDoneQuantity);
            Controls.Add(Label4);
            Controls.Add(lblProductionEndDate);
            Controls.Add(lblProductionStartDate);
            Controls.Add(lblPlaningEndDate);
            Controls.Add(lblPlaningStartDate);
            Controls.Add(Label2);
            Controls.Add(lblBatchProductionProgress);
            Controls.Add(Label5);
            Controls.Add(Label10);
            Controls.Add(Label3);
            Controls.Add(Label1);
            Controls.Add(Label11);
            Controls.Add(Label7);
            Controls.Add(_TreeView1);
            Controls.Add(Panel2);
            Controls.Add(Panel1);
            Controls.Add(lblBatchState);
            Controls.Add(lblBatchUnProduction);
            Controls.Add(lblPlanningQuantity);
            Controls.Add(lblBatchQuantity);
            Controls.Add(Label13);
            Controls.Add(Label12);
            Controls.Add(lblBatchProduction);
            Controls.Add(GroupBox1);
            MinimumSize = new Size(650, 250);
            Name = "frmBatchsStatus";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = " نمایش پیشرفت تولید بچ";
            Panel1.ResumeLayout(false);
            TabControl1.ResumeLayout(false);
            TabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgBatchProductionOperations).EndInit();
            TabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgBatchUnProductionOperations).EndInit();
            TabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgProductionQuantityList).EndInit();
            GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)txtProductionQuantity).EndInit();
            ((System.ComponentModel.ISupportInitialize)_dgOperations).EndInit();
            Panel2.ResumeLayout(false);
            GroupBox1.ResumeLayout(false);
            Load += new EventHandler(frmBatchsStatus_Load);
            FormClosing += new FormClosingEventHandler(frmBatchsStatus_FormClosing);
            ResumeLayout(false);
        }

        internal System.Windows.Forms.Panel Panel1;
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
                    _dgOperations.KeyDown -= dgList_KeyDown;
                }

                _dgOperations = value;
                if (_dgOperations != null)
                {
                    _dgOperations.KeyDown += dgList_KeyDown;
                }
            }
        }

        internal System.Windows.Forms.Panel Panel2;
        private System.Windows.Forms.Button _cmdExit;

        internal System.Windows.Forms.Button cmdExit
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdExit;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdExit != null)
                {
                    _cmdExit.Click -= cmdExit_Click;
                }

                _cmdExit = value;
                if (_cmdExit != null)
                {
                    _cmdExit.Click += cmdExit_Click;
                }
            }
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
                }

                _TreeView1 = value;
                if (_TreeView1 != null)
                {
                    _TreeView1.AfterSelect += TreeView1_AfterSelect;
                }
            }
        }

        internal ComboBox cbBatchs;
        internal Label Label6;
        internal Label Label1;
        internal Label lblBatchQuantity;
        internal Label lblBatchProduction;
        internal Label Label4;
        internal Label lblBatchUnProduction;
        internal Label Label7;
        internal Label lblBatchDetourPercent;
        internal Label Label9;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal Label lblBatchState;
        internal Label Label11;
        internal DataGridViewTextBoxColumn colOperationCode;
        internal DataGridViewTextBoxColumn colOperationName;
        internal DataGridViewTextBoxColumn colPlanningQuantity;
        internal DataGridViewTextBoxColumn ColProductionQuantity;
        internal DataGridViewTextBoxColumn colUnProductionQuantity;
        internal DataGridView dgBatchProductionOperations;
        internal DataGridView dgBatchUnProductionOperations;
        internal DataGridViewTextBoxColumn DataGridViewTextBoxColumn6;
        internal DataGridViewTextBoxColumn DataGridViewTextBoxColumn7;
        internal DataGridViewTextBoxColumn DataGridViewTextBoxColumn8;
        internal DataGridViewTextBoxColumn colOperationCode1;
        internal DataGridViewTextBoxColumn colOperationName1;
        internal DataGridViewTextBoxColumn colPlanningQuantity1;
        internal DataGridViewTextBoxColumn colProductionQuantity1;
        internal DataGridViewTextBoxColumn colUnProductionQuantity1;
        internal Label lblPlanningQuantity;
        internal Label Label3;
        internal Label lblBatchDetourHour;
        internal Label Label8;
        internal Label Label5;
        internal Label Label10;
        internal Label lblPlaningStartDate;
        internal Label lblProductionStartDate;
        internal PSB_FarsiDateControl.PSB_DateControl txtCalcDate;
        internal Label Label2;
        internal Label lblBatchProductionProgress;
        internal Label lblPlaningEndDate;
        internal Label lblProductionEndDate;
        internal Label Label12;
        internal Label Label13;
        private Timer _tmrLoadOvers;

        internal Timer tmrLoadOvers
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _tmrLoadOvers;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_tmrLoadOvers != null)
                {
                    _tmrLoadOvers.Tick -= tmrLoadOvers_Tick;
                }

                _tmrLoadOvers = value;
                if (_tmrLoadOvers != null)
                {
                    _tmrLoadOvers.Tick += tmrLoadOvers_Tick;
                }
            }
        }

        private Timer _tmrBatchProductionOperations;

        internal Timer tmrBatchProductionOperations
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _tmrBatchProductionOperations;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_tmrBatchProductionOperations != null)
                {
                    _tmrBatchProductionOperations.Tick -= tmrBatchProductionOperations_Tick;
                }

                _tmrBatchProductionOperations = value;
                if (_tmrBatchProductionOperations != null)
                {
                    _tmrBatchProductionOperations.Tick += tmrBatchProductionOperations_Tick;
                }
            }
        }

        internal Label Label14;
        internal Label lblDoneQuantity;
        internal TabControl TabControl1;
        private System.Windows.Forms.Button _cmdBatchProgressCalc;

        internal System.Windows.Forms.Button cmdBatchProgressCalc
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdBatchProgressCalc;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdBatchProgressCalc != null)
                {
                    _cmdBatchProgressCalc.Click -= cmdBatchProgressCalc_Click;
                }

                _cmdBatchProgressCalc = value;
                if (_cmdBatchProgressCalc != null)
                {
                    _cmdBatchProgressCalc.Click += cmdBatchProgressCalc_Click;
                }
            }
        }

        internal GroupBox GroupBox1;
        internal Label Label17;
        internal Label lblPlanningDuration;
        internal Label Label15;
        internal Label lblProductionDuration;
        internal Label lblDetourDescription;
        internal TabPage TabPage3;
        private RadioButton _rbProductionQuantity;

        internal RadioButton rbProductionQuantity
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _rbProductionQuantity;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_rbProductionQuantity != null)
                {
                    _rbProductionQuantity.CheckedChanged -= rbProductionQuantity_CheckedChanged;
                }

                _rbProductionQuantity = value;
                if (_rbProductionQuantity != null)
                {
                    _rbProductionQuantity.CheckedChanged += rbProductionQuantity_CheckedChanged;
                }
            }
        }

        internal PSB_FarsiDateControl.PSB_DateControl txtProductionDate;
        internal RadioButton rbProductionDate;
        internal NumericUpDown txtProductionQuantity;
        private System.Windows.Forms.Button _cmdCalcProductionQuantity;

        internal System.Windows.Forms.Button cmdCalcProductionQuantity
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdCalcProductionQuantity;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdCalcProductionQuantity != null)
                {
                    _cmdCalcProductionQuantity.Click -= cmdCalcProductionQuantity_Click;
                }

                _cmdCalcProductionQuantity = value;
                if (_cmdCalcProductionQuantity != null)
                {
                    _cmdCalcProductionQuantity.Click += cmdCalcProductionQuantity_Click;
                }
            }
        }

        internal Label Label18;
        internal PSB_FarsiDateControl.PSB_DateControl txtProductionFrom;
        internal GroupBox GroupBox2;
        internal DataGridView dgProductionQuantityList;
        internal DataGridViewTextBoxColumn DataGridViewTextBoxColumn1;
        internal DataGridViewTextBoxColumn DataGridViewTextBoxColumn2;
        internal DataGridViewTextBoxColumn colTotalProduction;
    }
}