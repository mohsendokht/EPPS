using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmMachineProductionProgram : Form
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
            var DataGridViewCellStyle1 = new DataGridViewCellStyle();
            var DataGridViewCellStyle2 = new DataGridViewCellStyle();
            var DataGridViewCellStyle3 = new DataGridViewCellStyle();
            var DataGridViewCellStyle4 = new DataGridViewCellStyle();
            Panel1 = new System.Windows.Forms.Panel();
            rbReportPart = new RadioButton();
            rbReportMachine = new RadioButton();
            _cmdPrintPreview = new System.Windows.Forms.Button();
            _cmdPrintPreview.Click += new EventHandler(cmdPrint_Click);
            lblCalcWaiting = new Label();
            _cmdCalc = new System.Windows.Forms.Button();
            _cmdCalc.Click += new EventHandler(cmdCalc_Click);
            _cmdCancel = new System.Windows.Forms.Button();
            _cmdCancel.Click += new EventHandler(cmdCancel_Click);
            Label8 = new Label();
            txtFromDate = new PSB_FarsiDateControl.PSB_DateControl();
            GbConditions = new GroupBox();
            Label3 = new Label();
            _chkAllSubbatchs = new CheckBox();
            _chkAllSubbatchs.CheckedChanged += new EventHandler(chkAllSubbatchs_CheckedChanged);
            _chkAllParts = new CheckBox();
            _chkAllParts.CheckedChanged += new EventHandler(chkAllParts_CheckedChanged);
            Label1 = new Label();
            _chkAllMachines = new CheckBox();
            _chkAllMachines.CheckedChanged += new EventHandler(chkAllMachines_CheckedChanged);
            Label2 = new Label();
            GroupBox2 = new GroupBox();
            Label6 = new Label();
            txtToDate = new PSB_FarsiDateControl.PSB_DateControl();
            Panel2 = new System.Windows.Forms.Panel();
            cbMachineCode = new PSBMultiColumnComboBox.PSBMultiColumnComboBox();
            Panel3 = new System.Windows.Forms.Panel();
            cbPartCode = new PSBMultiColumnComboBox.PSBMultiColumnComboBox();
            Panel4 = new System.Windows.Forms.Panel();
            cbSubbatchCode = new ComboBox();
            _dgMachineWorkTimes = new DataGridView();
            _dgMachineWorkTimes.CellFormatting += new DataGridViewCellFormattingEventHandler(dgMachineWorkTimes_CellFormatting);
            colSubbatchCode = new DataGridViewTextBoxColumn();
            colMachineCode = new DataGridViewTextBoxColumn();
            colPartCode = new DataGridViewTextBoxColumn();
            colDetailCode = new DataGridViewTextBoxColumn();
            colOperation = new DataGridViewTextBoxColumn();
            colPlanningStart = new DataGridViewTextBoxColumn();
            colPlanningEnd = new DataGridViewTextBoxColumn();
            colQuantity = new DataGridViewTextBoxColumn();
            TabControl1 = new TabControl();
            TabPage1 = new TabPage();
            Panel1.SuspendLayout();
            GbConditions.SuspendLayout();
            GroupBox2.SuspendLayout();
            Panel2.SuspendLayout();
            Panel3.SuspendLayout();
            Panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgMachineWorkTimes).BeginInit();
            TabControl1.SuspendLayout();
            TabPage1.SuspendLayout();
            SuspendLayout();
            // 
            // Panel1
            // 
            Panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Panel1.BackColor = SystemColors.Control;
            Panel1.BorderStyle = BorderStyle.FixedSingle;
            Panel1.Controls.Add(rbReportPart);
            Panel1.Controls.Add(rbReportMachine);
            Panel1.Controls.Add(_cmdPrintPreview);
            Panel1.Controls.Add(lblCalcWaiting);
            Panel1.ForeColor = Color.Black;
            Panel1.Location = new Point(4, 403);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(583, 38);
            Panel1.TabIndex = 5;
            // 
            // rbReportPart
            // 
            rbReportPart.AutoSize = true;
            rbReportPart.Location = new Point(247, 10);
            rbReportPart.Name = "rbReportPart";
            rbReportPart.Size = new Size(122, 17);
            rbReportPart.TabIndex = 230;
            rbReportPart.TabStop = true;
            rbReportPart.Text = "گروه بندی بخش تولید";
            rbReportPart.UseVisualStyleBackColor = true;
            // 
            // rbReportMachine
            // 
            rbReportMachine.AutoSize = true;
            rbReportMachine.Checked = true;
            rbReportMachine.Location = new Point(128, 10);
            rbReportMachine.Name = "rbReportMachine";
            rbReportMachine.Size = new Size(106, 17);
            rbReportMachine.TabIndex = 229;
            rbReportMachine.TabStop = true;
            rbReportMachine.Text = "گروه بندی ماشین";
            rbReportMachine.UseVisualStyleBackColor = true;
            // 
            // cmdPrintPreview
            // 
            _cmdPrintPreview.BackColor = Color.Transparent;
            _cmdPrintPreview.DialogResult = DialogResult.OK;
            _cmdPrintPreview.Enabled = false;
            _cmdPrintPreview.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdPrintPreview.ForeColor = Color.Blue;
            _cmdPrintPreview.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdPrintPreview.Location = new Point(6, 6);
            _cmdPrintPreview.Name = "_cmdPrintPreview";
            _cmdPrintPreview.RightToLeft = RightToLeft.No;
            _cmdPrintPreview.Size = new Size(107, 24);
            _cmdPrintPreview.TabIndex = 2;
            _cmdPrintPreview.Text = "پیش نمایش چاپ";
            _cmdPrintPreview.UseVisualStyleBackColor = false;
            // 
            // lblCalcWaiting
            // 
            lblCalcWaiting.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblCalcWaiting.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            lblCalcWaiting.ForeColor = Color.Fuchsia;
            lblCalcWaiting.Location = new Point(408, 8);
            lblCalcWaiting.Name = "lblCalcWaiting";
            lblCalcWaiting.RightToLeft = RightToLeft.No;
            lblCalcWaiting.Size = new Size(167, 17);
            lblCalcWaiting.TabIndex = 228;
            lblCalcWaiting.Text = "... لطفاً تا پایان عملیات صبر کنید";
            lblCalcWaiting.TextAlign = ContentAlignment.MiddleCenter;
            lblCalcWaiting.Visible = false;
            // 
            // cmdCalc
            // 
            _cmdCalc.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _cmdCalc.BackColor = Color.Transparent;
            _cmdCalc.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdCalc.ForeColor = Color.Black;
            _cmdCalc.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdCalc.Location = new Point(17, 438);
            _cmdCalc.Name = "_cmdCalc";
            _cmdCalc.RightToLeft = RightToLeft.No;
            _cmdCalc.Size = new Size(90, 26);
            _cmdCalc.TabIndex = 0;
            _cmdCalc.Text = "نمایش";
            _cmdCalc.UseVisualStyleBackColor = false;
            // 
            // cmdCancel
            // 
            _cmdCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _cmdCancel.BackColor = Color.Transparent;
            _cmdCancel.DialogResult = DialogResult.Cancel;
            _cmdCancel.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdCancel.ForeColor = Color.Red;
            _cmdCancel.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdCancel.Location = new Point(125, 438);
            _cmdCancel.Name = "_cmdCancel";
            _cmdCancel.RightToLeft = RightToLeft.No;
            _cmdCancel.Size = new Size(90, 26);
            _cmdCancel.TabIndex = 1;
            _cmdCancel.Text = "انصراف/خروج";
            _cmdCancel.UseVisualStyleBackColor = false;
            // 
            // Label8
            // 
            Label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label8.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label8.Location = new Point(140, 27);
            Label8.Name = "Label8";
            Label8.RightToLeft = RightToLeft.Yes;
            Label8.Size = new Size(46, 20);
            Label8.TabIndex = 221;
            Label8.Text = "از تاریخ:";
            Label8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtFromDate
            // 
            txtFromDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtFromDate.AutoValidate = AutoValidate.Disable;
            txtFromDate.BackColor = SystemColors.Control;
            txtFromDate.BackColorDateBox = Color.White;
            txtFromDate.DateButtonShow = true;
            txtFromDate.EnableDateText = true;
            txtFromDate.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtFromDate.FontMyDate = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtFromDate.Location = new Point(30, 27);
            txtFromDate.Margin = new Padding(4);
            txtFromDate.MinimumSize = new Size(96, 24);
            txtFromDate.Name = "txtFromDate";
            txtFromDate.Size = new Size(105, 24);
            txtFromDate.TabIndex = 1;
            // 
            // GbConditions
            // 
            GbConditions.BackColor = Color.LightYellow;
            GbConditions.Controls.Add(Label3);
            GbConditions.Controls.Add(_chkAllSubbatchs);
            GbConditions.Controls.Add(_chkAllParts);
            GbConditions.Controls.Add(Label1);
            GbConditions.Controls.Add(_chkAllMachines);
            GbConditions.Controls.Add(Label2);
            GbConditions.Controls.Add(GroupBox2);
            GbConditions.Controls.Add(_cmdCalc);
            GbConditions.Controls.Add(_cmdCancel);
            GbConditions.Controls.Add(Panel2);
            GbConditions.Controls.Add(Panel3);
            GbConditions.Controls.Add(Panel4);
            GbConditions.Dock = DockStyle.Right;
            GbConditions.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            GbConditions.Location = new Point(599, 0);
            GbConditions.Name = "GbConditions";
            GbConditions.Size = new Size(225, 473);
            GbConditions.TabIndex = 1;
            GbConditions.TabStop = false;
            GbConditions.Text = "شرایط نمایش";
            // 
            // Label3
            // 
            Label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label3.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label3.Location = new Point(136, 204);
            Label3.Name = "Label3";
            Label3.RightToLeft = RightToLeft.Yes;
            Label3.Size = new Size(82, 22);
            Label3.TabIndex = 247;
            Label3.Text = "کد ساب بچ";
            Label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // chkAllSubbatchs
            // 
            _chkAllSubbatchs.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _chkAllSubbatchs.Checked = true;
            _chkAllSubbatchs.CheckState = CheckState.Checked;
            _chkAllSubbatchs.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _chkAllSubbatchs.ForeColor = Color.Blue;
            _chkAllSubbatchs.Location = new Point(85, 206);
            _chkAllSubbatchs.Name = "_chkAllSubbatchs";
            _chkAllSubbatchs.Size = new Size(52, 20);
            _chkAllSubbatchs.TabIndex = 248;
            _chkAllSubbatchs.Text = "همه";
            _chkAllSubbatchs.UseVisualStyleBackColor = true;
            // 
            // chkAllParts
            // 
            _chkAllParts.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _chkAllParts.Checked = true;
            _chkAllParts.CheckState = CheckState.Checked;
            _chkAllParts.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _chkAllParts.ForeColor = Color.Blue;
            _chkAllParts.Location = new Point(85, 126);
            _chkAllParts.Name = "_chkAllParts";
            _chkAllParts.Size = new Size(52, 20);
            _chkAllParts.TabIndex = 243;
            _chkAllParts.Text = "همه";
            _chkAllParts.UseVisualStyleBackColor = true;
            // 
            // Label1
            // 
            Label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label1.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label1.Location = new Point(136, 124);
            Label1.Name = "Label1";
            Label1.RightToLeft = RightToLeft.Yes;
            Label1.Size = new Size(82, 22);
            Label1.TabIndex = 242;
            Label1.Text = "کد بخش تولید";
            Label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // chkAllMachines
            // 
            _chkAllMachines.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _chkAllMachines.Checked = true;
            _chkAllMachines.CheckState = CheckState.Checked;
            _chkAllMachines.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _chkAllMachines.ForeColor = Color.Blue;
            _chkAllMachines.Location = new Point(85, 47);
            _chkAllMachines.Name = "_chkAllMachines";
            _chkAllMachines.Size = new Size(52, 20);
            _chkAllMachines.TabIndex = 240;
            _chkAllMachines.Text = "همه";
            _chkAllMachines.UseVisualStyleBackColor = true;
            // 
            // Label2
            // 
            Label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label2.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label2.Location = new Point(136, 45);
            Label2.Name = "Label2";
            Label2.RightToLeft = RightToLeft.Yes;
            Label2.Size = new Size(82, 22);
            Label2.TabIndex = 237;
            Label2.Text = "کد ماشین";
            Label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox2
            // 
            GroupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox2.Controls.Add(txtFromDate);
            GroupBox2.Controls.Add(Label6);
            GroupBox2.Controls.Add(Label8);
            GroupBox2.Controls.Add(txtToDate);
            GroupBox2.Location = new Point(6, 291);
            GroupBox2.Name = "GroupBox2";
            GroupBox2.Size = new Size(213, 101);
            GroupBox2.TabIndex = 244;
            GroupBox2.TabStop = false;
            GroupBox2.Text = "بازۀ زمانی";
            // 
            // Label6
            // 
            Label6.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label6.Location = new Point(140, 66);
            Label6.Name = "Label6";
            Label6.RightToLeft = RightToLeft.Yes;
            Label6.Size = new Size(46, 20);
            Label6.TabIndex = 227;
            Label6.Text = "تا تاریخ:";
            Label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtToDate
            // 
            txtToDate.AutoValidate = AutoValidate.Disable;
            txtToDate.BackColor = SystemColors.Control;
            txtToDate.BackColorDateBox = Color.White;
            txtToDate.DateButtonShow = true;
            txtToDate.EnableDateText = true;
            txtToDate.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtToDate.FontMyDate = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtToDate.Location = new Point(30, 66);
            txtToDate.Margin = new Padding(4);
            txtToDate.MinimumSize = new Size(96, 24);
            txtToDate.Name = "txtToDate";
            txtToDate.Size = new Size(105, 24);
            txtToDate.TabIndex = 3;
            // 
            // Panel2
            // 
            Panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Panel2.BorderStyle = BorderStyle.FixedSingle;
            Panel2.Controls.Add(cbMachineCode);
            Panel2.Location = new Point(6, 59);
            Panel2.Name = "Panel2";
            Panel2.Size = new Size(213, 49);
            Panel2.TabIndex = 245;
            // 
            // cbMachineCode
            // 
            cbMachineCode.Enabled = false;
            cbMachineCode.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cbMachineCode.ListAlternateColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(192)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            cbMachineCode.Location = new Point(29, 17);
            cbMachineCode.MinimumSize = new Size(82, 23);
            cbMachineCode.Name = "cbMachineCode";
            cbMachineCode.RightToLeft = RightToLeft.Yes;
            cbMachineCode.SeletedValue = null;
            cbMachineCode.Size = new Size(153, 23);
            cbMachineCode.TabIndex = 229;
            // 
            // Panel3
            // 
            Panel3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Panel3.BorderStyle = BorderStyle.FixedSingle;
            Panel3.Controls.Add(cbPartCode);
            Panel3.Location = new Point(6, 139);
            Panel3.Name = "Panel3";
            Panel3.Size = new Size(213, 49);
            Panel3.TabIndex = 246;
            // 
            // cbPartCode
            // 
            cbPartCode.Enabled = false;
            cbPartCode.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cbPartCode.ListAlternateColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(192)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            cbPartCode.Location = new Point(29, 17);
            cbPartCode.MinimumSize = new Size(82, 23);
            cbPartCode.Name = "cbPartCode";
            cbPartCode.RightToLeft = RightToLeft.Yes;
            cbPartCode.SeletedValue = null;
            cbPartCode.Size = new Size(153, 23);
            cbPartCode.TabIndex = 241;
            // 
            // Panel4
            // 
            Panel4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Panel4.BorderStyle = BorderStyle.FixedSingle;
            Panel4.Controls.Add(cbSubbatchCode);
            Panel4.Location = new Point(6, 219);
            Panel4.Name = "Panel4";
            Panel4.Size = new Size(213, 49);
            Panel4.TabIndex = 249;
            // 
            // cbSubbatchCode
            // 
            cbSubbatchCode.FormattingEnabled = true;
            cbSubbatchCode.Location = new Point(29, 16);
            cbSubbatchCode.Name = "cbSubbatchCode";
            cbSubbatchCode.Size = new Size(153, 24);
            cbSubbatchCode.TabIndex = 1;
            // 
            // dgMachineWorkTimes
            // 
            _dgMachineWorkTimes.AllowUserToAddRows = false;
            _dgMachineWorkTimes.AllowUserToDeleteRows = false;
            _dgMachineWorkTimes.AllowUserToResizeRows = false;
            DataGridViewCellStyle1.BackColor = Color.Beige;
            _dgMachineWorkTimes.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1;
            _dgMachineWorkTimes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            _dgMachineWorkTimes.BackgroundColor = SystemColors.ControlDark;
            DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle2.BackColor = SystemColors.Control;
            DataGridViewCellStyle2.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            _dgMachineWorkTimes.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2;
            _dgMachineWorkTimes.ColumnHeadersHeight = 25;
            _dgMachineWorkTimes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            _dgMachineWorkTimes.Columns.AddRange(new DataGridViewColumn[] { colSubbatchCode, colMachineCode, colPartCode, colDetailCode, colOperation, colPlanningStart, colPlanningEnd, colQuantity });
            DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
            DataGridViewCellStyle3.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            DataGridViewCellStyle3.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            DataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            _dgMachineWorkTimes.DefaultCellStyle = DataGridViewCellStyle3;
            _dgMachineWorkTimes.Location = new Point(4, 6);
            _dgMachineWorkTimes.Name = "_dgMachineWorkTimes";
            _dgMachineWorkTimes.ReadOnly = true;
            _dgMachineWorkTimes.RightToLeft = RightToLeft.Yes;
            _dgMachineWorkTimes.RowHeadersVisible = false;
            _dgMachineWorkTimes.RowHeadersWidth = 15;
            _dgMachineWorkTimes.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle4.BackColor = SystemColors.Info;
            DataGridViewCellStyle4.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle4.ForeColor = Color.Black;
            DataGridViewCellStyle4.SelectionBackColor = Color.RoyalBlue;
            DataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            _dgMachineWorkTimes.RowsDefaultCellStyle = DataGridViewCellStyle4;
            _dgMachineWorkTimes.RowTemplate.Height = 18;
            _dgMachineWorkTimes.RowTemplate.Resizable = DataGridViewTriState.False;
            _dgMachineWorkTimes.SelectionMode = DataGridViewSelectionMode.CellSelect;
            _dgMachineWorkTimes.Size = new Size(583, 393);
            _dgMachineWorkTimes.TabIndex = 1;
            // 
            // colSubbatchCode
            // 
            colSubbatchCode.HeaderText = "کد ساب بچ";
            colSubbatchCode.Name = "colSubbatchCode";
            colSubbatchCode.ReadOnly = true;
            colSubbatchCode.Width = 110;
            // 
            // colMachineCode
            // 
            colMachineCode.HeaderText = "کد ماشین";
            colMachineCode.Name = "colMachineCode";
            colMachineCode.ReadOnly = true;
            colMachineCode.Width = 80;
            // 
            // colPartCode
            // 
            colPartCode.HeaderText = "کد بخش تولید";
            colPartCode.Name = "colPartCode";
            colPartCode.ReadOnly = true;
            colPartCode.Visible = false;
            // 
            // colDetailCode
            // 
            colDetailCode.HeaderText = "کد و نام قطعه";
            colDetailCode.Name = "colDetailCode";
            colDetailCode.ReadOnly = true;
            colDetailCode.Width = 180;
            // 
            // colOperation
            // 
            colOperation.HeaderText = "کد و نام عملیات";
            colOperation.Name = "colOperation";
            colOperation.ReadOnly = true;
            colOperation.Width = 200;
            // 
            // colPlanningStart
            // 
            colPlanningStart.HeaderText = "شروع برنامه ریزی";
            colPlanningStart.Name = "colPlanningStart";
            colPlanningStart.ReadOnly = true;
            colPlanningStart.Width = 130;
            // 
            // colPlanningEnd
            // 
            colPlanningEnd.HeaderText = "پایان برنامه ریزی";
            colPlanningEnd.Name = "colPlanningEnd";
            colPlanningEnd.ReadOnly = true;
            colPlanningEnd.Width = 130;
            // 
            // colQuantity
            // 
            colQuantity.HeaderText = "تعداد تولید";
            colQuantity.Name = "colQuantity";
            colQuantity.ReadOnly = true;
            colQuantity.Width = 70;
            // 
            // TabControl1
            // 
            TabControl1.Controls.Add(TabPage1);
            TabControl1.Dock = DockStyle.Fill;
            TabControl1.Location = new Point(0, 0);
            TabControl1.Name = "TabControl1";
            TabControl1.RightToLeftLayout = true;
            TabControl1.SelectedIndex = 0;
            TabControl1.Size = new Size(599, 473);
            TabControl1.TabIndex = 225;
            // 
            // TabPage1
            // 
            TabPage1.Controls.Add(_dgMachineWorkTimes);
            TabPage1.Controls.Add(Panel1);
            TabPage1.Location = new Point(4, 22);
            TabPage1.Name = "TabPage1";
            TabPage1.Padding = new Padding(3);
            TabPage1.Size = new Size(591, 447);
            TabPage1.TabIndex = 0;
            TabPage1.Text = "لیست عملیات تولیدی ماشین آلات";
            TabPage1.UseVisualStyleBackColor = true;
            // 
            // frmMachineProductionProgram
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _cmdCancel;
            ClientSize = new Size(824, 473);
            Controls.Add(TabControl1);
            Controls.Add(GbConditions);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            MinimumSize = new Size(832, 507);
            Name = "frmMachineProductionProgram";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " نمایش عملیات تولیدی ماشین در بازۀ زمانی";
            Panel1.ResumeLayout(false);
            Panel1.PerformLayout();
            GbConditions.ResumeLayout(false);
            GroupBox2.ResumeLayout(false);
            Panel2.ResumeLayout(false);
            Panel3.ResumeLayout(false);
            Panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_dgMachineWorkTimes).EndInit();
            TabControl1.ResumeLayout(false);
            TabPage1.ResumeLayout(false);
            Load += new EventHandler(frmMachineProductionProgram_Load);
            FormClosing += new FormClosingEventHandler(frmMachineProductionProgram_FormClosing);
            ResumeLayout(false);
        }

        internal System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.Button _cmdCalc;

        internal System.Windows.Forms.Button cmdCalc
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdCalc;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdCalc != null)
                {
                    _cmdCalc.Click -= cmdCalc_Click;
                }

                _cmdCalc = value;
                if (_cmdCalc != null)
                {
                    _cmdCalc.Click += cmdCalc_Click;
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

        private System.Windows.Forms.Button _cmdPrintPreview;

        internal System.Windows.Forms.Button cmdPrintPreview
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdPrintPreview;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdPrintPreview != null)
                {
                    _cmdPrintPreview.Click -= cmdPrint_Click;
                }

                _cmdPrintPreview = value;
                if (_cmdPrintPreview != null)
                {
                    _cmdPrintPreview.Click += cmdPrint_Click;
                }
            }
        }

        internal Label Label8;
        internal PSB_FarsiDateControl.PSB_DateControl txtFromDate;
        internal GroupBox GbConditions;
        internal PSB_FarsiDateControl.PSB_DateControl txtToDate;
        internal Label Label6;
        private DataGridView _dgMachineWorkTimes;

        internal DataGridView dgMachineWorkTimes
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _dgMachineWorkTimes;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_dgMachineWorkTimes != null)
                {
                    _dgMachineWorkTimes.CellFormatting -= dgMachineWorkTimes_CellFormatting;
                }

                _dgMachineWorkTimes = value;
                if (_dgMachineWorkTimes != null)
                {
                    _dgMachineWorkTimes.CellFormatting += dgMachineWorkTimes_CellFormatting;
                }
            }
        }

        internal TabControl TabControl1;
        internal TabPage TabPage1;
        internal Label lblCalcWaiting;
        internal Label Label2;
        internal PSBMultiColumnComboBox.PSBMultiColumnComboBox cbMachineCode;
        private CheckBox _chkAllMachines;

        internal CheckBox chkAllMachines
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _chkAllMachines;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_chkAllMachines != null)
                {
                    _chkAllMachines.CheckedChanged -= chkAllMachines_CheckedChanged;
                }

                _chkAllMachines = value;
                if (_chkAllMachines != null)
                {
                    _chkAllMachines.CheckedChanged += chkAllMachines_CheckedChanged;
                }
            }
        }

        private CheckBox _chkAllParts;

        internal CheckBox chkAllParts
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _chkAllParts;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_chkAllParts != null)
                {
                    _chkAllParts.CheckedChanged -= chkAllParts_CheckedChanged;
                }

                _chkAllParts = value;
                if (_chkAllParts != null)
                {
                    _chkAllParts.CheckedChanged += chkAllParts_CheckedChanged;
                }
            }
        }

        internal PSBMultiColumnComboBox.PSBMultiColumnComboBox cbPartCode;
        internal Label Label1;
        internal GroupBox GroupBox2;
        internal System.Windows.Forms.Panel Panel3;
        internal System.Windows.Forms.Panel Panel2;
        internal Label Label3;
        private CheckBox _chkAllSubbatchs;

        internal CheckBox chkAllSubbatchs
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _chkAllSubbatchs;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_chkAllSubbatchs != null)
                {
                    _chkAllSubbatchs.CheckedChanged -= chkAllSubbatchs_CheckedChanged;
                }

                _chkAllSubbatchs = value;
                if (_chkAllSubbatchs != null)
                {
                    _chkAllSubbatchs.CheckedChanged += chkAllSubbatchs_CheckedChanged;
                }
            }
        }

        internal System.Windows.Forms.Panel Panel4;
        internal ComboBox cbSubbatchCode;
        internal DataGridViewTextBoxColumn colSubbatchCode;
        internal DataGridViewTextBoxColumn colMachineCode;
        internal DataGridViewTextBoxColumn colPartCode;
        internal DataGridViewTextBoxColumn colDetailCode;
        internal DataGridViewTextBoxColumn colOperation;
        internal DataGridViewTextBoxColumn colPlanningStart;
        internal DataGridViewTextBoxColumn colPlanningEnd;
        internal DataGridViewTextBoxColumn colQuantity;
        internal RadioButton rbReportPart;
        internal RadioButton rbReportMachine;
    }
}