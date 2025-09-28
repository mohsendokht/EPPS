using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmOEE : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOEE));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Panel1 = new System.Windows.Forms.Panel();
            this._cmdPrint = new System.Windows.Forms.Button();
            this._cmdSave = new System.Windows.Forms.Button();
            this._cmdCalc = new System.Windows.Forms.Button();
            this._cmdCancel = new System.Windows.Forms.Button();
            this.prgOEE = new System.Windows.Forms.ProgressBar();
            this.lblOEECalcAction = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.txtFromDate = new PSB_FarsiDateControl.PSB_DateControl();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this._cmdDeSelectAll = new System.Windows.Forms.Button();
            this._cmdSelectAll = new System.Windows.Forms.Button();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtDownTime = new System.Windows.Forms.DateTimePicker();
            this._chkFullDayBase = new System.Windows.Forms.CheckBox();
            this._chkCalendarBase = new System.Windows.Forms.CheckBox();
            this.lstMachines = new System.Windows.Forms.CheckedListBox();
            this.txtToHour = new System.Windows.Forms.DateTimePicker();
            this.txtFromHour = new System.Windows.Forms.DateTimePicker();
            this.txtToDate = new PSB_FarsiDateControl.PSB_DateControl();
            this.Label6 = new System.Windows.Forms.Label();
            this.dgOEEList = new System.Windows.Forms.DataGridView();
            this.colMachineCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAvailability = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPerformance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuality = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOEE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbTotal = new System.Windows.Forms.GroupBox();
            this.lblAllOEE = new System.Windows.Forms.Label();
            this.lblAllQuality = new System.Windows.Forms.Label();
            this.lblAllPerformance = new System.Windows.Forms.Label();
            this.lblAllAvailability = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            //this.acsOEE = new AxMicrosoft.Office.Interop.Owc11.AxChartSpace();
            this.Panel1.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOEEList)).BeginInit();
            this.gbTotal.SuspendLayout();
            this.TabControl1.SuspendLayout();
            this.TabPage1.SuspendLayout();
            //((System.ComponentModel.ISupportInitialize)(this.acsOEE)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this._cmdPrint);
            this.Panel1.Controls.Add(this._cmdSave);
            this.Panel1.Controls.Add(this._cmdCalc);
            this.Panel1.Controls.Add(this._cmdCancel);
            this.Panel1.Controls.Add(this.prgOEE);
            this.Panel1.Controls.Add(this.lblOEECalcAction);
            this.Panel1.ForeColor = System.Drawing.Color.Black;
            this.Panel1.Location = new System.Drawing.Point(10, 418);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(803, 48);
            this.Panel1.TabIndex = 5;
            // 
            // _cmdPrint
            // 
            this._cmdPrint.BackColor = System.Drawing.Color.Transparent;
            this._cmdPrint.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._cmdPrint.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdPrint.ForeColor = System.Drawing.Color.Blue;
            this._cmdPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdPrint.Location = new System.Drawing.Point(135, 10);
            this._cmdPrint.Name = "_cmdPrint";
            this._cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdPrint.Size = new System.Drawing.Size(107, 28);
            this._cmdPrint.TabIndex = 2;
            this._cmdPrint.Text = "پیش نمایش چاپ";
            this._cmdPrint.UseVisualStyleBackColor = false;
            this._cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // _cmdSave
            // 
            this._cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cmdSave.BackColor = System.Drawing.Color.Transparent;
            this._cmdSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._cmdSave.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdSave.ForeColor = System.Drawing.Color.Black;
            this._cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdSave.Location = new System.Drawing.Point(569, 10);
            this._cmdSave.Name = "_cmdSave";
            this._cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdSave.Size = new System.Drawing.Size(100, 28);
            this._cmdSave.TabIndex = 3;
            this._cmdSave.Text = "ثبت";
            this._cmdSave.UseVisualStyleBackColor = false;
            this._cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // _cmdCalc
            // 
            this._cmdCalc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cmdCalc.BackColor = System.Drawing.Color.Transparent;
            this._cmdCalc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdCalc.ForeColor = System.Drawing.Color.Black;
            this._cmdCalc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdCalc.Location = new System.Drawing.Point(684, 10);
            this._cmdCalc.Name = "_cmdCalc";
            this._cmdCalc.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdCalc.Size = new System.Drawing.Size(100, 28);
            this._cmdCalc.TabIndex = 0;
            this._cmdCalc.Text = "محاسبه";
            this._cmdCalc.UseVisualStyleBackColor = false;
            this._cmdCalc.Click += new System.EventHandler(this.cmdCalc_Click);
            // 
            // _cmdCancel
            // 
            this._cmdCancel.BackColor = System.Drawing.Color.Transparent;
            this._cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cmdCancel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdCancel.ForeColor = System.Drawing.Color.Red;
            this._cmdCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdCancel.Location = new System.Drawing.Point(21, 10);
            this._cmdCancel.Name = "_cmdCancel";
            this._cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdCancel.Size = new System.Drawing.Size(100, 28);
            this._cmdCancel.TabIndex = 1;
            this._cmdCancel.Text = "انصراف/خروج";
            this._cmdCancel.UseVisualStyleBackColor = false;
            this._cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // prgOEE
            // 
            this.prgOEE.Location = new System.Drawing.Point(245, 17);
            this.prgOEE.Name = "prgOEE";
            this.prgOEE.Size = new System.Drawing.Size(118, 14);
            this.prgOEE.TabIndex = 4;
            this.prgOEE.Visible = false;
            // 
            // lblOEECalcAction
            // 
            this.lblOEECalcAction.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblOEECalcAction.ForeColor = System.Drawing.Color.Fuchsia;
            this.lblOEECalcAction.Location = new System.Drawing.Point(365, 13);
            this.lblOEECalcAction.Name = "lblOEECalcAction";
            this.lblOEECalcAction.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblOEECalcAction.Size = new System.Drawing.Size(203, 20);
            this.lblOEECalcAction.TabIndex = 228;
            this.lblOEECalcAction.Text = "#";
            this.lblOEECalcAction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblOEECalcAction.Visible = false;
            // 
            // Label8
            // 
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label8.Location = new System.Drawing.Point(187, 31);
            this.Label8.Name = "Label8";
            this.Label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label8.Size = new System.Drawing.Size(45, 20);
            this.Label8.TabIndex = 221;
            this.Label8.Text = "از تاریخ:";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFromDate
            // 
            this.txtFromDate.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.txtFromDate.BackColor = System.Drawing.SystemColors.Control;
            this.txtFromDate.BackColorDateBox = System.Drawing.Color.White;
            this.txtFromDate.DateButtonShow = true;
            this.txtFromDate.EnableDateText = true;
            this.txtFromDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtFromDate.FontMyDate = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtFromDate.Location = new System.Drawing.Point(87, 30);
            this.txtFromDate.Margin = new System.Windows.Forms.Padding(4);
            this.txtFromDate.MinimumSize = new System.Drawing.Size(96, 24);
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.Size = new System.Drawing.Size(98, 24);
            this.txtFromDate.TabIndex = 1;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.Controls.Add(this._cmdDeSelectAll);
            this.GroupBox1.Controls.Add(this._cmdSelectAll);
            this.GroupBox1.Controls.Add(this.GroupBox3);
            this.GroupBox1.Controls.Add(this.lstMachines);
            this.GroupBox1.Controls.Add(this.txtToHour);
            this.GroupBox1.Controls.Add(this.txtFromHour);
            this.GroupBox1.Controls.Add(this.txtFromDate);
            this.GroupBox1.Controls.Add(this.txtToDate);
            this.GroupBox1.Controls.Add(this.Label8);
            this.GroupBox1.Controls.Add(this.Label6);
            this.GroupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.GroupBox1.Location = new System.Drawing.Point(562, 9);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(250, 400);
            this.GroupBox1.TabIndex = 1;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "شرایط محاسبه";
            // 
            // _cmdDeSelectAll
            // 
            this._cmdDeSelectAll.BackColor = System.Drawing.Color.Transparent;
            this._cmdDeSelectAll.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._cmdDeSelectAll.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdDeSelectAll.ForeColor = System.Drawing.Color.Black;
            this._cmdDeSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("_cmdDeSelectAll.Image")));
            this._cmdDeSelectAll.Location = new System.Drawing.Point(10, 180);
            this._cmdDeSelectAll.Name = "_cmdDeSelectAll";
            this._cmdDeSelectAll.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdDeSelectAll.Size = new System.Drawing.Size(30, 25);
            this._cmdDeSelectAll.TabIndex = 237;
            this._cmdDeSelectAll.UseVisualStyleBackColor = false;
            this._cmdDeSelectAll.Click += new System.EventHandler(this.cmdDeSelectAll_Click);
            // 
            // _cmdSelectAll
            // 
            this._cmdSelectAll.BackColor = System.Drawing.Color.Transparent;
            this._cmdSelectAll.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._cmdSelectAll.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdSelectAll.ForeColor = System.Drawing.Color.Black;
            this._cmdSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("_cmdSelectAll.Image")));
            this._cmdSelectAll.Location = new System.Drawing.Point(44, 180);
            this._cmdSelectAll.Name = "_cmdSelectAll";
            this._cmdSelectAll.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdSelectAll.Size = new System.Drawing.Size(30, 25);
            this._cmdSelectAll.TabIndex = 236;
            this._cmdSelectAll.UseVisualStyleBackColor = false;
            this._cmdSelectAll.Click += new System.EventHandler(this.cmdSelectAll_Click);
            // 
            // GroupBox3
            // 
            this.GroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox3.Controls.Add(this.Label1);
            this.GroupBox3.Controls.Add(this.txtDownTime);
            this.GroupBox3.Controls.Add(this._chkFullDayBase);
            this.GroupBox3.Controls.Add(this._chkCalendarBase);
            this.GroupBox3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.GroupBox3.Location = new System.Drawing.Point(6, 95);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(238, 79);
            this.GroupBox3.TabIndex = 235;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "مبنای محاسبه";
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label1.Location = new System.Drawing.Point(69, 49);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label1.Size = new System.Drawing.Size(109, 20);
            this.Label1.TabIndex = 235;
            this.Label1.Text = "طول زمان استراحت:";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDownTime
            // 
            this.txtDownTime.CustomFormat = "HH:mm";
            this.txtDownTime.Enabled = false;
            this.txtDownTime.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtDownTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtDownTime.Location = new System.Drawing.Point(8, 49);
            this.txtDownTime.Name = "txtDownTime";
            this.txtDownTime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDownTime.RightToLeftLayout = true;
            this.txtDownTime.ShowUpDown = true;
            this.txtDownTime.Size = new System.Drawing.Size(58, 21);
            this.txtDownTime.TabIndex = 234;
            this.txtDownTime.Value = new System.DateTime(2008, 6, 5, 0, 0, 0, 0);
            // 
            // _chkFullDayBase
            // 
            this._chkFullDayBase.AutoSize = true;
            this._chkFullDayBase.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._chkFullDayBase.ForeColor = System.Drawing.Color.Blue;
            this._chkFullDayBase.Location = new System.Drawing.Point(187, 49);
            this._chkFullDayBase.Name = "_chkFullDayBase";
            this._chkFullDayBase.Size = new System.Drawing.Size(40, 20);
            this._chkFullDayBase.TabIndex = 233;
            this._chkFullDayBase.Text = "24";
            this._chkFullDayBase.UseVisualStyleBackColor = true;
            this._chkFullDayBase.CheckedChanged += new System.EventHandler(this.chkFullDayBase_CheckedChanged);
            // 
            // _chkCalendarBase
            // 
            this._chkCalendarBase.AutoSize = true;
            this._chkCalendarBase.Checked = true;
            this._chkCalendarBase.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkCalendarBase.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._chkCalendarBase.ForeColor = System.Drawing.Color.Blue;
            this._chkCalendarBase.Location = new System.Drawing.Point(171, 19);
            this._chkCalendarBase.Name = "_chkCalendarBase";
            this._chkCalendarBase.Size = new System.Drawing.Size(56, 20);
            this._chkCalendarBase.TabIndex = 232;
            this._chkCalendarBase.Text = "تقویم";
            this._chkCalendarBase.UseVisualStyleBackColor = true;
            this._chkCalendarBase.CheckedChanged += new System.EventHandler(this.chkCalendarBase_CheckedChanged);
            // 
            // lstMachines
            // 
            this.lstMachines.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstMachines.BackColor = System.Drawing.Color.Beige;
            this.lstMachines.FormattingEnabled = true;
            this.lstMachines.Location = new System.Drawing.Point(10, 207);
            this.lstMachines.Name = "lstMachines";
            this.lstMachines.Size = new System.Drawing.Size(230, 184);
            this.lstMachines.TabIndex = 8;
            // 
            // txtToHour
            // 
            this.txtToHour.CustomFormat = "HH:mm";
            this.txtToHour.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtToHour.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtToHour.Location = new System.Drawing.Point(14, 69);
            this.txtToHour.Name = "txtToHour";
            this.txtToHour.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtToHour.RightToLeftLayout = true;
            this.txtToHour.ShowUpDown = true;
            this.txtToHour.Size = new System.Drawing.Size(58, 21);
            this.txtToHour.TabIndex = 229;
            this.txtToHour.Value = new System.DateTime(2008, 6, 5, 0, 0, 0, 0);
            // 
            // txtFromHour
            // 
            this.txtFromHour.CustomFormat = "HH:mm";
            this.txtFromHour.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtFromHour.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtFromHour.Location = new System.Drawing.Point(14, 31);
            this.txtFromHour.Name = "txtFromHour";
            this.txtFromHour.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtFromHour.RightToLeftLayout = true;
            this.txtFromHour.ShowUpDown = true;
            this.txtFromHour.Size = new System.Drawing.Size(58, 21);
            this.txtFromHour.TabIndex = 228;
            this.txtFromHour.Value = new System.DateTime(2008, 6, 5, 0, 0, 0, 0);
            // 
            // txtToDate
            // 
            this.txtToDate.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.txtToDate.BackColor = System.Drawing.SystemColors.Control;
            this.txtToDate.BackColorDateBox = System.Drawing.Color.White;
            this.txtToDate.DateButtonShow = true;
            this.txtToDate.EnableDateText = true;
            this.txtToDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtToDate.FontMyDate = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtToDate.Location = new System.Drawing.Point(87, 68);
            this.txtToDate.Margin = new System.Windows.Forms.Padding(4);
            this.txtToDate.MinimumSize = new System.Drawing.Size(96, 24);
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.Size = new System.Drawing.Size(98, 24);
            this.txtToDate.TabIndex = 3;
            // 
            // Label6
            // 
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label6.Location = new System.Drawing.Point(187, 69);
            this.Label6.Name = "Label6";
            this.Label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label6.Size = new System.Drawing.Size(45, 20);
            this.Label6.TabIndex = 227;
            this.Label6.Text = "تا تاریخ:";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgOEEList
            // 
            this.dgOEEList.AllowUserToAddRows = false;
            this.dgOEEList.AllowUserToDeleteRows = false;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.Beige;
            this.dgOEEList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgOEEList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgOEEList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgOEEList.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgOEEList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgOEEList.ColumnHeadersHeight = 25;
            this.dgOEEList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgOEEList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMachineCode,
            this.colAvailability,
            this.colPerformance,
            this.colQuality,
            this.colOEE});
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgOEEList.DefaultCellStyle = dataGridViewCellStyle15;
            this.dgOEEList.Location = new System.Drawing.Point(6, 9);
            this.dgOEEList.MultiSelect = false;
            this.dgOEEList.Name = "dgOEEList";
            this.dgOEEList.ReadOnly = true;
            this.dgOEEList.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dgOEEList.RowHeadersVisible = false;
            this.dgOEEList.RowHeadersWidth = 15;
            this.dgOEEList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgOEEList.RowsDefaultCellStyle = dataGridViewCellStyle16;
            this.dgOEEList.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgOEEList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgOEEList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgOEEList.Size = new System.Drawing.Size(526, 263);
            this.dgOEEList.TabIndex = 1;
            // 
            // colMachineCode
            // 
            this.colMachineCode.HeaderText = "کد ماشین";
            this.colMachineCode.Name = "colMachineCode";
            this.colMachineCode.ReadOnly = true;
            // 
            // colAvailability
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colAvailability.DefaultCellStyle = dataGridViewCellStyle11;
            this.colAvailability.HeaderText = "(Availability(A";
            this.colAvailability.Name = "colAvailability";
            this.colAvailability.ReadOnly = true;
            // 
            // colPerformance
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colPerformance.DefaultCellStyle = dataGridViewCellStyle12;
            this.colPerformance.HeaderText = "(Performance(P";
            this.colPerformance.Name = "colPerformance";
            this.colPerformance.ReadOnly = true;
            // 
            // colQuality
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colQuality.DefaultCellStyle = dataGridViewCellStyle13;
            this.colQuality.HeaderText = "(Quality(Q";
            this.colQuality.Name = "colQuality";
            this.colQuality.ReadOnly = true;
            // 
            // colOEE
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colOEE.DefaultCellStyle = dataGridViewCellStyle14;
            this.colOEE.HeaderText = "OEE";
            this.colOEE.Name = "colOEE";
            this.colOEE.ReadOnly = true;
            // 
            // gbTotal
            // 
            this.gbTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTotal.Controls.Add(this.lblAllOEE);
            this.gbTotal.Controls.Add(this.lblAllQuality);
            this.gbTotal.Controls.Add(this.lblAllPerformance);
            this.gbTotal.Controls.Add(this.lblAllAvailability);
            this.gbTotal.Controls.Add(this.Label2);
            this.gbTotal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.gbTotal.Location = new System.Drawing.Point(6, 271);
            this.gbTotal.Name = "gbTotal";
            this.gbTotal.Size = new System.Drawing.Size(526, 50);
            this.gbTotal.TabIndex = 7;
            this.gbTotal.TabStop = false;
            // 
            // lblAllOEE
            // 
            this.lblAllOEE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAllOEE.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblAllOEE.ForeColor = System.Drawing.Color.Blue;
            this.lblAllOEE.Location = new System.Drawing.Point(11, 19);
            this.lblAllOEE.Name = "lblAllOEE";
            this.lblAllOEE.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblAllOEE.Size = new System.Drawing.Size(90, 19);
            this.lblAllOEE.TabIndex = 229;
            this.lblAllOEE.Text = "#";
            this.lblAllOEE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAllQuality
            // 
            this.lblAllQuality.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAllQuality.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblAllQuality.ForeColor = System.Drawing.Color.Blue;
            this.lblAllQuality.Location = new System.Drawing.Point(115, 19);
            this.lblAllQuality.Name = "lblAllQuality";
            this.lblAllQuality.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblAllQuality.Size = new System.Drawing.Size(90, 19);
            this.lblAllQuality.TabIndex = 228;
            this.lblAllQuality.Text = "#";
            this.lblAllQuality.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAllPerformance
            // 
            this.lblAllPerformance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAllPerformance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblAllPerformance.ForeColor = System.Drawing.Color.Blue;
            this.lblAllPerformance.Location = new System.Drawing.Point(218, 19);
            this.lblAllPerformance.Name = "lblAllPerformance";
            this.lblAllPerformance.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblAllPerformance.Size = new System.Drawing.Size(90, 19);
            this.lblAllPerformance.TabIndex = 227;
            this.lblAllPerformance.Text = "#";
            this.lblAllPerformance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAllAvailability
            // 
            this.lblAllAvailability.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAllAvailability.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblAllAvailability.ForeColor = System.Drawing.Color.Blue;
            this.lblAllAvailability.Location = new System.Drawing.Point(324, 19);
            this.lblAllAvailability.Name = "lblAllAvailability";
            this.lblAllAvailability.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblAllAvailability.Size = new System.Drawing.Size(90, 19);
            this.lblAllAvailability.TabIndex = 226;
            this.lblAllAvailability.Text = "#";
            this.lblAllAvailability.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label2.ForeColor = System.Drawing.Color.Fuchsia;
            this.Label2.Location = new System.Drawing.Point(430, 13);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label2.Size = new System.Drawing.Size(91, 32);
            this.Label2.TabIndex = 225;
            this.Label2.Text = "کل ماشین های انتخاب شده:";
            // 
            // Label9
            // 
            this.Label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label9.Location = new System.Drawing.Point(474, 328);
            this.Label9.Name = "Label9";
            this.Label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label9.Size = new System.Drawing.Size(58, 19);
            this.Label9.TabIndex = 224;
            this.Label9.Text = "شرح ثبت:";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.BackColor = System.Drawing.Color.White;
            this.txtDescription.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtDescription.Location = new System.Drawing.Point(6, 328);
            this.txtDescription.MaxLength = 200;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(465, 41);
            this.txtDescription.TabIndex = 223;
            // 
            // TabControl1
            // 
            this.TabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl1.Controls.Add(this.TabPage1);
            this.TabControl1.Location = new System.Drawing.Point(10, 9);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.RightToLeftLayout = true;
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(546, 403);
            this.TabControl1.TabIndex = 225;
            // 
            // TabPage1
            // 
            this.TabPage1.Controls.Add(this.dgOEEList);
            this.TabPage1.Controls.Add(this.Label9);
            this.TabPage1.Controls.Add(this.gbTotal);
            this.TabPage1.Controls.Add(this.txtDescription);
            this.TabPage1.Location = new System.Drawing.Point(4, 22);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(538, 377);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "محاسبۀ OEE";
            this.TabPage1.UseVisualStyleBackColor = true;
            // 
            // acsOEE
            // 
            //this.acsOEE.DataSource = null;
            //this.acsOEE.Enabled = true;
            //this.acsOEE.Location = new System.Drawing.Point(0, 0);
            //this.acsOEE.Name = "acsOEE";
            //this.acsOEE.TabIndex = 0;
            // 
            // frmOEE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cmdCancel;
            this.ClientSize = new System.Drawing.Size(824, 473);
            this.Controls.Add(this.TabControl1);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.GroupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.MinimumSize = new System.Drawing.Size(832, 507);
            this.Name = "frmOEE";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " محاسبۀ OEE";
            this.Load += new System.EventHandler(this.frmOEE_Load);
            this.Resize += new System.EventHandler(this.frmOEE_Resize);
            this.Panel1.ResumeLayout(false);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOEEList)).EndInit();
            this.gbTotal.ResumeLayout(false);
            this.TabControl1.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            //((System.ComponentModel.ISupportInitialize)(this.acsOEE)).EndInit();
            this.ResumeLayout(false);

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

        private System.Windows.Forms.Button _cmdPrint;

        internal System.Windows.Forms.Button cmdPrint
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdPrint;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdPrint != null)
                {
                    _cmdPrint.Click -= cmdPrint_Click;
                }

                _cmdPrint = value;
                if (_cmdPrint != null)
                {
                    _cmdPrint.Click += cmdPrint_Click;
                }
            }
        }

        internal Label Label8;
        internal PSB_FarsiDateControl.PSB_DateControl txtFromDate;
        internal GroupBox GroupBox1;
        internal PSB_FarsiDateControl.PSB_DateControl txtToDate;
        internal Label Label6;
        internal DateTimePicker txtToHour;
        internal DateTimePicker txtFromHour;
        private CheckBox _chkCalendarBase;

        internal CheckBox chkCalendarBase
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _chkCalendarBase;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_chkCalendarBase != null)
                {
                    _chkCalendarBase.CheckedChanged -= chkCalendarBase_CheckedChanged;
                }

                _chkCalendarBase = value;
                if (_chkCalendarBase != null)
                {
                    _chkCalendarBase.CheckedChanged += chkCalendarBase_CheckedChanged;
                }
            }
        }

        private CheckBox _chkFullDayBase;

        internal CheckBox chkFullDayBase
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _chkFullDayBase;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_chkFullDayBase != null)
                {
                    _chkFullDayBase.CheckedChanged -= chkFullDayBase_CheckedChanged;
                }

                _chkFullDayBase = value;
                if (_chkFullDayBase != null)
                {
                    _chkFullDayBase.CheckedChanged += chkFullDayBase_CheckedChanged;
                }
            }
        }

        internal DataGridView dgOEEList;
        internal GroupBox gbTotal;
        internal DataGridViewTextBoxColumn colMachineCode;
        internal DataGridViewTextBoxColumn colAvailability;
        internal DataGridViewTextBoxColumn colPerformance;
        internal DataGridViewTextBoxColumn colQuality;
        internal DataGridViewTextBoxColumn colOEE;
        internal CheckedListBox lstMachines;
        internal GroupBox GroupBox3;
        private System.Windows.Forms.Button _cmdSelectAll;

        internal System.Windows.Forms.Button cmdSelectAll
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdSelectAll;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdSelectAll != null)
                {
                    _cmdSelectAll.Click -= cmdSelectAll_Click;
                }

                _cmdSelectAll = value;
                if (_cmdSelectAll != null)
                {
                    _cmdSelectAll.Click += cmdSelectAll_Click;
                }
            }
        }

        private System.Windows.Forms.Button _cmdDeSelectAll;

        internal System.Windows.Forms.Button cmdDeSelectAll
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdDeSelectAll;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdDeSelectAll != null)
                {
                    _cmdDeSelectAll.Click -= cmdDeSelectAll_Click;
                }

                _cmdDeSelectAll = value;
                if (_cmdDeSelectAll != null)
                {
                    _cmdDeSelectAll.Click += cmdDeSelectAll_Click;
                }
            }
        }

        internal Label Label1;
        internal DateTimePicker txtDownTime;
        private System.Windows.Forms.Button _cmdSave;

        internal System.Windows.Forms.Button cmdSave
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdSave;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdSave != null)
                {
                    _cmdSave.Click -= cmdSave_Click;
                }

                _cmdSave = value;
                if (_cmdSave != null)
                {
                    _cmdSave.Click += cmdSave_Click;
                }
            }
        }

        internal TextBox txtDescription;
        internal Label Label9;
        internal TabControl TabControl1;
        internal TabPage TabPage1;
        internal Label Label2;
        internal Label lblAllQuality;
        internal Label lblAllPerformance;
        internal Label lblAllAvailability;
        internal Label lblAllOEE;

        //internal System.Windows.Forms.Button cmdShowChart
        //{
        //    [MethodImpl(MethodImplOptions.Synchronized)]
        //    get
        //    {
        //     //   return _cmdShowChart;
        //    }

        //    [MethodImpl(MethodImplOptions.Synchronized)]
        //    set
        //    {
        //        //if (_cmdShowChart != null)
        //        //{
        //        //    _cmdShowChart.Click -= cmdShowChart_Click;
        //        //}

        //        //_cmdShowChart = value;
        //        //if (_cmdShowChart != null)
        //        //{
        //        //    _cmdShowChart.Click += cmdShowChart_Click;
        //        //}
        //    }
        //}
        internal System.Windows.Forms.ProgressBar prgOEE;
        internal Label lblOEECalcAction;
        //private AxMicrosoft.Office.Interop.Owc11.AxChartSpace acsOEE;
    }
}