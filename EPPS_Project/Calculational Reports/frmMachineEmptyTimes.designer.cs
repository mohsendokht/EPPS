using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmMachineEmptyTimes : Form
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
            _cmdPrintPreview = new System.Windows.Forms.Button();
            _cmdPrintPreview.Click += new EventHandler(cmdPrint_Click);
            lblCalcWaiting = new Label();
            _cmdCalc = new System.Windows.Forms.Button();
            _cmdCalc.Click += new EventHandler(cmdCalc_Click);
            _cmdCancel = new System.Windows.Forms.Button();
            _cmdCancel.Click += new EventHandler(cmdCancel_Click);
            Label8 = new Label();
            txtFromDate = new PSB_FarsiDateControl.PSB_DateControl();
            GroupBox1 = new GroupBox();
            _chkHasProduction = new CheckBox();
            _chkHasProduction.CheckedChanged += new EventHandler(chkHasProduction_CheckedChanged);
            _chkHasPlanning = new CheckBox();
            _chkHasPlanning.CheckedChanged += new EventHandler(chkHasPlanning_CheckedChanged);
            cbMachineCode = new PSBMultiColumnComboBox.PSBMultiColumnComboBox();
            Label2 = new Label();
            GroupBox3 = new GroupBox();
            Label3 = new Label();
            Label1 = new Label();
            txttimeSlice = new DateTimePicker();
            txtDownTime = new DateTimePicker();
            _chkFullDayBase = new CheckBox();
            _chkFullDayBase.CheckedChanged += new EventHandler(chkFullDayBase_CheckedChanged);
            _chkCalendarBase = new CheckBox();
            _chkCalendarBase.CheckedChanged += new EventHandler(chkCalendarBase_CheckedChanged);
            txtToDate = new PSB_FarsiDateControl.PSB_DateControl();
            Label6 = new Label();
            _dgMachineWorkTimes = new DataGridView();
            _dgMachineWorkTimes.CellFormatting += new DataGridViewCellFormattingEventHandler(dgMachineWorkTimes_CellFormatting);
            TabControl1 = new TabControl();
            TabPage1 = new TabPage();
            Panel1.SuspendLayout();
            GroupBox1.SuspendLayout();
            GroupBox3.SuspendLayout();
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
            Panel1.Controls.Add(_cmdPrintPreview);
            Panel1.Controls.Add(lblCalcWaiting);
            Panel1.ForeColor = Color.Black;
            Panel1.Location = new Point(6, 387);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(529, 38);
            Panel1.TabIndex = 5;
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
            lblCalcWaiting.Location = new Point(354, 8);
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
            _cmdCalc.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _cmdCalc.BackColor = Color.Transparent;
            _cmdCalc.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdCalc.ForeColor = Color.Black;
            _cmdCalc.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdCalc.Location = new Point(11, 417);
            _cmdCalc.Name = "_cmdCalc";
            _cmdCalc.RightToLeft = RightToLeft.No;
            _cmdCalc.Size = new Size(100, 28);
            _cmdCalc.TabIndex = 0;
            _cmdCalc.Text = "محاسبه";
            _cmdCalc.UseVisualStyleBackColor = false;
            // 
            // cmdCancel
            // 
            _cmdCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _cmdCancel.BackColor = Color.Transparent;
            _cmdCancel.DialogResult = DialogResult.Cancel;
            _cmdCancel.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdCancel.ForeColor = Color.Red;
            _cmdCancel.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdCancel.Location = new Point(140, 417);
            _cmdCancel.Name = "_cmdCancel";
            _cmdCancel.RightToLeft = RightToLeft.No;
            _cmdCancel.Size = new Size(100, 28);
            _cmdCancel.TabIndex = 1;
            _cmdCancel.Text = "انصراف/خروج";
            _cmdCancel.UseVisualStyleBackColor = false;
            // 
            // Label8
            // 
            Label8.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label8.Location = new Point(192, 80);
            Label8.Name = "Label8";
            Label8.RightToLeft = RightToLeft.Yes;
            Label8.Size = new Size(45, 20);
            Label8.TabIndex = 221;
            Label8.Text = "از تاریخ:";
            Label8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtFromDate
            // 
            txtFromDate.AutoValidate = AutoValidate.Disable;
            txtFromDate.BackColor = SystemColors.Control;
            txtFromDate.BackColorDateBox = Color.White;
            txtFromDate.DateButtonShow = true;
            txtFromDate.EnableDateText = true;
            txtFromDate.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtFromDate.FontMyDate = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtFromDate.Location = new Point(65, 79);
            txtFromDate.Margin = new Padding(4);
            txtFromDate.MinimumSize = new Size(96, 24);
            txtFromDate.Name = "txtFromDate";
            txtFromDate.Size = new Size(102, 24);
            txtFromDate.TabIndex = 1;
            // 
            // GroupBox1
            // 
            GroupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            GroupBox1.Controls.Add(_chkHasProduction);
            GroupBox1.Controls.Add(_cmdCalc);
            GroupBox1.Controls.Add(_chkHasPlanning);
            GroupBox1.Controls.Add(cbMachineCode);
            GroupBox1.Controls.Add(Label2);
            GroupBox1.Controls.Add(_cmdCancel);
            GroupBox1.Controls.Add(GroupBox3);
            GroupBox1.Controls.Add(txtFromDate);
            GroupBox1.Controls.Add(txtToDate);
            GroupBox1.Controls.Add(Label8);
            GroupBox1.Controls.Add(Label6);
            GroupBox1.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            GroupBox1.Location = new Point(562, 9);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(250, 454);
            GroupBox1.TabIndex = 1;
            GroupBox1.TabStop = false;
            GroupBox1.Text = "شرایط محاسبه";
            // 
            // chkHasProduction
            // 
            _chkHasProduction.AutoSize = true;
            _chkHasProduction.Checked = true;
            _chkHasProduction.CheckState = CheckState.Checked;
            _chkHasProduction.Font = new Font("Tahoma", 9.75f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _chkHasProduction.ForeColor = Color.Green;
            _chkHasProduction.Location = new Point(136, 304);
            _chkHasProduction.Name = "_chkHasProduction";
            _chkHasProduction.Size = new Size(98, 20);
            _chkHasProduction.TabIndex = 239;
            _chkHasProduction.Text = "نمایش تولید";
            _chkHasProduction.UseVisualStyleBackColor = true;
            // 
            // chkHasPlanning
            // 
            _chkHasPlanning.AutoSize = true;
            _chkHasPlanning.Checked = true;
            _chkHasPlanning.CheckState = CheckState.Checked;
            _chkHasPlanning.Font = new Font("Tahoma", 9.75f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _chkHasPlanning.ForeColor = Color.Red;
            _chkHasPlanning.Location = new Point(93, 272);
            _chkHasPlanning.Name = "_chkHasPlanning";
            _chkHasPlanning.Size = new Size(141, 20);
            _chkHasPlanning.TabIndex = 238;
            _chkHasPlanning.Text = "نمایش برنامه ریزی";
            _chkHasPlanning.UseVisualStyleBackColor = true;
            // 
            // cbMachineCode
            // 
            cbMachineCode.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cbMachineCode.ListAlternateColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(192)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            cbMachineCode.Location = new Point(14, 33);
            cbMachineCode.MinimumSize = new Size(82, 23);
            cbMachineCode.Name = "cbMachineCode";
            cbMachineCode.RightToLeft = RightToLeft.Yes;
            cbMachineCode.SeletedValue = null;
            cbMachineCode.Size = new Size(153, 23);
            cbMachineCode.TabIndex = 229;
            // 
            // Label2
            // 
            Label2.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label2.Location = new Point(173, 33);
            Label2.Name = "Label2";
            Label2.RightToLeft = RightToLeft.Yes;
            Label2.Size = new Size(64, 22);
            Label2.TabIndex = 237;
            Label2.Text = "کد ماشین:";
            Label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox3
            // 
            GroupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            GroupBox3.Controls.Add(Label3);
            GroupBox3.Controls.Add(Label1);
            GroupBox3.Controls.Add(txttimeSlice);
            GroupBox3.Controls.Add(txtDownTime);
            GroupBox3.Controls.Add(_chkFullDayBase);
            GroupBox3.Controls.Add(_chkCalendarBase);
            GroupBox3.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            GroupBox3.Location = new Point(6, 159);
            GroupBox3.Name = "GroupBox3";
            GroupBox3.Size = new Size(238, 90);
            GroupBox3.TabIndex = 235;
            GroupBox3.TabStop = false;
            GroupBox3.Text = "مبنای محاسبه";
            // 
            // Label3
            // 
            Label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Label3.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label3.Location = new Point(145, 61);
            Label3.Name = "Label3";
            Label3.RightToLeft = RightToLeft.Yes;
            Label3.Size = new Size(86, 20);
            Label3.TabIndex = 239;
            Label3.Text = "طول برش زمان:";
            Label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label1
            // 
            Label1.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label1.Location = new Point(69, 90);
            Label1.Name = "Label1";
            Label1.RightToLeft = RightToLeft.Yes;
            Label1.Size = new Size(109, 20);
            Label1.TabIndex = 235;
            Label1.Text = "طول زمان استراحت:";
            Label1.TextAlign = ContentAlignment.MiddleCenter;
            Label1.Visible = false;
            // 
            // txttimeSlice
            // 
            txttimeSlice.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            txttimeSlice.CustomFormat = "HH:mm";
            txttimeSlice.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txttimeSlice.Format = DateTimePickerFormat.Custom;
            txttimeSlice.Location = new Point(84, 61);
            txttimeSlice.Name = "txttimeSlice";
            txttimeSlice.RightToLeft = RightToLeft.No;
            txttimeSlice.RightToLeftLayout = true;
            txttimeSlice.ShowUpDown = true;
            txttimeSlice.Size = new Size(58, 21);
            txttimeSlice.TabIndex = 238;
            txttimeSlice.Value = new DateTime(2009, 12, 31, 1, 0, 0, 0);
            // 
            // txtDownTime
            // 
            txtDownTime.CustomFormat = "HH:mm";
            txtDownTime.Enabled = false;
            txtDownTime.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtDownTime.Format = DateTimePickerFormat.Custom;
            txtDownTime.Location = new Point(8, 90);
            txtDownTime.Name = "txtDownTime";
            txtDownTime.RightToLeft = RightToLeft.No;
            txtDownTime.RightToLeftLayout = true;
            txtDownTime.ShowUpDown = true;
            txtDownTime.Size = new Size(58, 21);
            txtDownTime.TabIndex = 234;
            txtDownTime.Value = new DateTime(2008, 6, 5, 0, 0, 0, 0);
            txtDownTime.Visible = false;
            // 
            // chkFullDayBase
            // 
            _chkFullDayBase.AutoSize = true;
            _chkFullDayBase.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _chkFullDayBase.ForeColor = Color.Blue;
            _chkFullDayBase.Location = new Point(25, 24);
            _chkFullDayBase.Name = "_chkFullDayBase";
            _chkFullDayBase.Size = new Size(41, 20);
            _chkFullDayBase.TabIndex = 233;
            _chkFullDayBase.Text = "24";
            _chkFullDayBase.UseVisualStyleBackColor = true;
            // 
            // chkCalendarBase
            // 
            _chkCalendarBase.AutoSize = true;
            _chkCalendarBase.Checked = true;
            _chkCalendarBase.CheckState = CheckState.Checked;
            _chkCalendarBase.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _chkCalendarBase.ForeColor = Color.Blue;
            _chkCalendarBase.Location = new Point(171, 24);
            _chkCalendarBase.Name = "_chkCalendarBase";
            _chkCalendarBase.Size = new Size(57, 20);
            _chkCalendarBase.TabIndex = 232;
            _chkCalendarBase.Text = "تقویم";
            _chkCalendarBase.UseVisualStyleBackColor = true;
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
            txtToDate.Location = new Point(65, 117);
            txtToDate.Margin = new Padding(4);
            txtToDate.MinimumSize = new Size(96, 24);
            txtToDate.Name = "txtToDate";
            txtToDate.Size = new Size(102, 24);
            txtToDate.TabIndex = 3;
            // 
            // Label6
            // 
            Label6.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label6.Location = new Point(192, 118);
            Label6.Name = "Label6";
            Label6.RightToLeft = RightToLeft.Yes;
            Label6.Size = new Size(45, 20);
            Label6.TabIndex = 227;
            Label6.Text = "تا تاریخ:";
            Label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dgMachineWorkTimes
            // 
            _dgMachineWorkTimes.AllowUserToAddRows = false;
            _dgMachineWorkTimes.AllowUserToDeleteRows = false;
            _dgMachineWorkTimes.AllowUserToResizeColumns = false;
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
            DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
            DataGridViewCellStyle3.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            DataGridViewCellStyle3.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            DataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            _dgMachineWorkTimes.DefaultCellStyle = DataGridViewCellStyle3;
            _dgMachineWorkTimes.Location = new Point(6, 8);
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
            _dgMachineWorkTimes.Size = new Size(529, 373);
            _dgMachineWorkTimes.TabIndex = 1;
            // 
            // TabControl1
            // 
            TabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            TabControl1.Controls.Add(TabPage1);
            TabControl1.Location = new Point(7, 9);
            TabControl1.Name = "TabControl1";
            TabControl1.RightToLeftLayout = true;
            TabControl1.SelectedIndex = 0;
            TabControl1.Size = new Size(549, 457);
            TabControl1.TabIndex = 225;
            // 
            // TabPage1
            // 
            TabPage1.Controls.Add(_dgMachineWorkTimes);
            TabPage1.Controls.Add(Panel1);
            TabPage1.Location = new Point(4, 22);
            TabPage1.Name = "TabPage1";
            TabPage1.Padding = new Padding(3);
            TabPage1.Size = new Size(541, 431);
            TabPage1.TabIndex = 0;
            TabPage1.Text = "محاسبۀ زمان های خالی ماشین";
            TabPage1.UseVisualStyleBackColor = true;
            // 
            // frmMachineEmptyTimes
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _cmdCancel;
            ClientSize = new Size(824, 473);
            Controls.Add(TabControl1);
            Controls.Add(GroupBox1);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            MinimumSize = new Size(832, 507);
            Name = "frmMachineEmptyTimes";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " محاسبۀ زمانهای کارکرد و خالی ماشین";
            Panel1.ResumeLayout(false);
            GroupBox1.ResumeLayout(false);
            GroupBox1.PerformLayout();
            GroupBox3.ResumeLayout(false);
            GroupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_dgMachineWorkTimes).EndInit();
            TabControl1.ResumeLayout(false);
            TabPage1.ResumeLayout(false);
            Load += new EventHandler(frmMachineEmptyTimes_Load);
            FormClosing += new FormClosingEventHandler(frmMachineEmptyTimes_FormClosing);
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
        internal GroupBox GroupBox1;
        internal PSB_FarsiDateControl.PSB_DateControl txtToDate;
        internal Label Label6;
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

        internal GroupBox GroupBox3;
        internal Label Label1;
        internal DateTimePicker txtDownTime;
        internal TabControl TabControl1;
        internal TabPage TabPage1;
        internal Label lblCalcWaiting;
        internal Label Label2;
        internal PSBMultiColumnComboBox.PSBMultiColumnComboBox cbMachineCode;
        internal Label Label3;
        internal DateTimePicker txttimeSlice;
        private CheckBox _chkHasProduction;

        internal CheckBox chkHasProduction
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _chkHasProduction;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_chkHasProduction != null)
                {
                    _chkHasProduction.CheckedChanged -= chkHasProduction_CheckedChanged;
                }

                _chkHasProduction = value;
                if (_chkHasProduction != null)
                {
                    _chkHasProduction.CheckedChanged += chkHasProduction_CheckedChanged;
                }
            }
        }

        private CheckBox _chkHasPlanning;

        internal CheckBox chkHasPlanning
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _chkHasPlanning;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_chkHasPlanning != null)
                {
                    _chkHasPlanning.CheckedChanged -= chkHasPlanning_CheckedChanged;
                }

                _chkHasPlanning = value;
                if (_chkHasPlanning != null)
                {
                    _chkHasPlanning.CheckedChanged += chkHasPlanning_CheckedChanged;
                }
            }
        }
    }
}