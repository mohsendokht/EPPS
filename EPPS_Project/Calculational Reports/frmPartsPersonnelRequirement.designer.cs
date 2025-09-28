using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmPartsPersonnelRequirement : Form
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
            Panel1 = new System.Windows.Forms.Panel();
            Label3 = new Label();
            txtDifferenceTime = new DateTimePicker();
            _cmdPrintPreview = new System.Windows.Forms.Button();
            _cmdPrintPreview.Click += new EventHandler(cmdPrint_Click);
            _cmdCancel = new System.Windows.Forms.Button();
            _cmdCancel.Click += new EventHandler(cmdCancel_Click);
            lblCalcWaiting = new Label();
            _cmdCalc = new System.Windows.Forms.Button();
            _cmdCalc.Click += new EventHandler(cmdCalc_Click);
            Label8 = new Label();
            txtFromDate = new PSB_FarsiDateControl.PSB_DateControl();
            txtToDate = new PSB_FarsiDateControl.PSB_DateControl();
            Label6 = new Label();
            dgPartsPersonnelRequirement = new DataGridView();
            TabControl1 = new TabControl();
            TabPage1 = new TabPage();
            Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgPartsPersonnelRequirement).BeginInit();
            TabControl1.SuspendLayout();
            TabPage1.SuspendLayout();
            SuspendLayout();
            // 
            // Panel1
            // 
            Panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Panel1.BackColor = SystemColors.Control;
            Panel1.BorderStyle = BorderStyle.FixedSingle;
            Panel1.Controls.Add(Label3);
            Panel1.Controls.Add(txtDifferenceTime);
            Panel1.Controls.Add(_cmdPrintPreview);
            Panel1.Controls.Add(_cmdCancel);
            Panel1.Controls.Add(lblCalcWaiting);
            Panel1.ForeColor = Color.Black;
            Panel1.Location = new Point(6, 382);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(790, 38);
            Panel1.TabIndex = 5;
            // 
            // Label3
            // 
            Label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Label3.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label3.Location = new Point(608, 8);
            Label3.Name = "Label3";
            Label3.RightToLeft = RightToLeft.Yes;
            Label3.Size = new Size(173, 20);
            Label3.TabIndex = 241;
            Label3.Text = "نمایش رنگ بندی اختلاف زمان از:";
            Label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtDifferenceTime
            // 
            txtDifferenceTime.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            txtDifferenceTime.CustomFormat = "HH:mm";
            txtDifferenceTime.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtDifferenceTime.Format = DateTimePickerFormat.Custom;
            txtDifferenceTime.Location = new Point(547, 8);
            txtDifferenceTime.Name = "txtDifferenceTime";
            txtDifferenceTime.RightToLeft = RightToLeft.No;
            txtDifferenceTime.RightToLeftLayout = true;
            txtDifferenceTime.ShowUpDown = true;
            txtDifferenceTime.Size = new Size(58, 21);
            txtDifferenceTime.TabIndex = 240;
            txtDifferenceTime.Value = new DateTime(2009, 12, 31, 1, 0, 0, 0);
            // 
            // cmdPrintPreview
            // 
            _cmdPrintPreview.BackColor = Color.Transparent;
            _cmdPrintPreview.DialogResult = DialogResult.OK;
            _cmdPrintPreview.Enabled = false;
            _cmdPrintPreview.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdPrintPreview.ForeColor = Color.Blue;
            _cmdPrintPreview.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdPrintPreview.Location = new Point(132, 6);
            _cmdPrintPreview.Name = "_cmdPrintPreview";
            _cmdPrintPreview.RightToLeft = RightToLeft.No;
            _cmdPrintPreview.Size = new Size(107, 24);
            _cmdPrintPreview.TabIndex = 2;
            _cmdPrintPreview.Text = "پیش نمایش چاپ";
            _cmdPrintPreview.UseVisualStyleBackColor = false;
            // 
            // cmdCancel
            // 
            _cmdCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _cmdCancel.BackColor = Color.Transparent;
            _cmdCancel.DialogResult = DialogResult.Cancel;
            _cmdCancel.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdCancel.ForeColor = Color.Red;
            _cmdCancel.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdCancel.Location = new Point(13, 6);
            _cmdCancel.Name = "_cmdCancel";
            _cmdCancel.RightToLeft = RightToLeft.No;
            _cmdCancel.Size = new Size(100, 24);
            _cmdCancel.TabIndex = 1;
            _cmdCancel.Text = "انصراف/خروج";
            _cmdCancel.UseVisualStyleBackColor = false;
            // 
            // lblCalcWaiting
            // 
            lblCalcWaiting.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblCalcWaiting.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            lblCalcWaiting.ForeColor = Color.Fuchsia;
            lblCalcWaiting.Location = new Point(245, 10);
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
            _cmdCalc.BackColor = Color.Transparent;
            _cmdCalc.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdCalc.ForeColor = Color.Black;
            _cmdCalc.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdCalc.Location = new Point(31, 6);
            _cmdCalc.Name = "_cmdCalc";
            _cmdCalc.RightToLeft = RightToLeft.No;
            _cmdCalc.Size = new Size(100, 24);
            _cmdCalc.TabIndex = 0;
            _cmdCalc.Text = "محاسبه";
            _cmdCalc.UseVisualStyleBackColor = false;
            // 
            // Label8
            // 
            Label8.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label8.Location = new Point(414, 7);
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
            txtFromDate.Location = new Point(310, 6);
            txtFromDate.Margin = new Padding(4);
            txtFromDate.MinimumSize = new Size(96, 24);
            txtFromDate.Name = "txtFromDate";
            txtFromDate.Size = new Size(102, 24);
            txtFromDate.TabIndex = 1;
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
            txtToDate.Location = new Point(148, 6);
            txtToDate.Margin = new Padding(4);
            txtToDate.MinimumSize = new Size(96, 24);
            txtToDate.Name = "txtToDate";
            txtToDate.Size = new Size(102, 24);
            txtToDate.TabIndex = 3;
            // 
            // Label6
            // 
            Label6.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label6.Location = new Point(252, 7);
            Label6.Name = "Label6";
            Label6.RightToLeft = RightToLeft.Yes;
            Label6.Size = new Size(45, 20);
            Label6.TabIndex = 227;
            Label6.Text = "تا تاریخ:";
            Label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dgPartsPersonnelRequirement
            // 
            dgPartsPersonnelRequirement.AllowUserToAddRows = false;
            dgPartsPersonnelRequirement.AllowUserToDeleteRows = false;
            dgPartsPersonnelRequirement.AllowUserToResizeColumns = false;
            dgPartsPersonnelRequirement.AllowUserToResizeRows = false;
            dgPartsPersonnelRequirement.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            dgPartsPersonnelRequirement.BackgroundColor = SystemColors.ControlDark;
            DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle1.BackColor = SystemColors.Control;
            DataGridViewCellStyle1.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            DataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dgPartsPersonnelRequirement.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1;
            dgPartsPersonnelRequirement.ColumnHeadersHeight = 25;
            dgPartsPersonnelRequirement.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
            DataGridViewCellStyle2.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            DataGridViewCellStyle2.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgPartsPersonnelRequirement.DefaultCellStyle = DataGridViewCellStyle2;
            dgPartsPersonnelRequirement.Location = new Point(6, 8);
            dgPartsPersonnelRequirement.Name = "dgPartsPersonnelRequirement";
            dgPartsPersonnelRequirement.ReadOnly = true;
            dgPartsPersonnelRequirement.RightToLeft = RightToLeft.Yes;
            dgPartsPersonnelRequirement.RowHeadersWidth = 90;
            dgPartsPersonnelRequirement.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle3.BackColor = SystemColors.Info;
            DataGridViewCellStyle3.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle3.ForeColor = Color.Black;
            DataGridViewCellStyle3.SelectionBackColor = Color.RoyalBlue;
            DataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgPartsPersonnelRequirement.RowsDefaultCellStyle = DataGridViewCellStyle3;
            dgPartsPersonnelRequirement.RowTemplate.Height = 18;
            dgPartsPersonnelRequirement.RowTemplate.Resizable = DataGridViewTriState.False;
            dgPartsPersonnelRequirement.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgPartsPersonnelRequirement.Size = new Size(790, 368);
            dgPartsPersonnelRequirement.TabIndex = 1;
            // 
            // TabControl1
            // 
            TabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            TabControl1.Controls.Add(TabPage1);
            TabControl1.Location = new Point(7, 14);
            TabControl1.Name = "TabControl1";
            TabControl1.RightToLeftLayout = true;
            TabControl1.SelectedIndex = 0;
            TabControl1.Size = new Size(810, 452);
            TabControl1.TabIndex = 225;
            // 
            // TabPage1
            // 
            TabPage1.Controls.Add(dgPartsPersonnelRequirement);
            TabPage1.Controls.Add(Panel1);
            TabPage1.Location = new Point(4, 22);
            TabPage1.Name = "TabPage1";
            TabPage1.Padding = new Padding(3);
            TabPage1.Size = new Size(802, 426);
            TabPage1.TabIndex = 0;
            TabPage1.Text = "محاسبۀ ظرفیت مورد نیاز نیروی انسانی مربوط به هر بخش تولید";
            TabPage1.UseVisualStyleBackColor = true;
            // 
            // frmPartsPersonnelRequirement
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _cmdCancel;
            ClientSize = new Size(824, 473);
            Controls.Add(_cmdCalc);
            Controls.Add(txtFromDate);
            Controls.Add(txtToDate);
            Controls.Add(Label8);
            Controls.Add(Label6);
            Controls.Add(TabControl1);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            MinimumSize = new Size(832, 507);
            Name = "frmPartsPersonnelRequirement";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " محاسبۀ ظرفیت مورد نیاز نیروی انسانی مربوط به هر بخش تولید";
            Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgPartsPersonnelRequirement).EndInit();
            TabControl1.ResumeLayout(false);
            TabPage1.ResumeLayout(false);
            Load += new EventHandler(frmPartsPersonnelRequirement_Load);
            FormClosing += new FormClosingEventHandler(frmPartsPersonnelRequirement_FormClosing);
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
        internal PSB_FarsiDateControl.PSB_DateControl txtToDate;
        internal Label Label6;
        internal DataGridView dgPartsPersonnelRequirement;
        internal TabControl TabControl1;
        internal TabPage TabPage1;
        internal Label lblCalcWaiting;
        internal Label Label3;
        internal DateTimePicker txtDifferenceTime;
    }
}