using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmBatchDetour : Form
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
            Panel1 = new System.Windows.Forms.Panel();
            _cmdPrintPreview = new System.Windows.Forms.Button();
            _cmdPrintPreview.Click += new EventHandler(cmdPrint_Click);
            _cmdCancel = new System.Windows.Forms.Button();
            _cmdCancel.Click += new EventHandler(cmdCancel_Click);
            Label8 = new Label();
            txtCalcDate = new PSB_FarsiDateControl.PSB_DateControl();
            CrystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // Panel1
            // 
            Panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Panel1.BackColor = SystemColors.Control;
            Panel1.BorderStyle = BorderStyle.FixedSingle;
            Panel1.Controls.Add(_cmdCancel);
            Panel1.Controls.Add(_cmdPrintPreview);
            Panel1.Controls.Add(Label8);
            Panel1.Controls.Add(txtCalcDate);
            Panel1.ForeColor = Color.Black;
            Panel1.Location = new Point(6, 9);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(812, 38);
            Panel1.TabIndex = 5;
            // 
            // cmdPrintPreview
            // 
            _cmdPrintPreview.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _cmdPrintPreview.BackColor = Color.Transparent;
            _cmdPrintPreview.DialogResult = DialogResult.OK;
            _cmdPrintPreview.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdPrintPreview.ForeColor = Color.Blue;
            _cmdPrintPreview.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdPrintPreview.Location = new Point(492, 6);
            _cmdPrintPreview.Name = "_cmdPrintPreview";
            _cmdPrintPreview.RightToLeft = RightToLeft.No;
            _cmdPrintPreview.Size = new Size(107, 24);
            _cmdPrintPreview.TabIndex = 2;
            _cmdPrintPreview.Text = "نمایش";
            _cmdPrintPreview.UseVisualStyleBackColor = false;
            // 
            // cmdCancel
            // 
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
            // Label8
            // 
            Label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label8.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label8.Location = new Point(715, 7);
            Label8.Name = "Label8";
            Label8.RightToLeft = RightToLeft.Yes;
            Label8.Size = new Size(81, 20);
            Label8.TabIndex = 221;
            Label8.Text = "انحراف تا تاریخ:";
            Label8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtCalcDate
            // 
            txtCalcDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtCalcDate.AutoValidate = AutoValidate.Disable;
            txtCalcDate.BackColor = SystemColors.Control;
            txtCalcDate.BackColorDateBox = Color.White;
            txtCalcDate.DateButtonShow = true;
            txtCalcDate.EnableDateText = true;
            txtCalcDate.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtCalcDate.FontMyDate = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtCalcDate.Location = new Point(611, 6);
            txtCalcDate.Margin = new Padding(4);
            txtCalcDate.MinimumSize = new Size(96, 24);
            txtCalcDate.Name = "txtCalcDate";
            txtCalcDate.Size = new Size(102, 24);
            txtCalcDate.TabIndex = 1;
            // 
            // CrystalReportViewer1
            // 
            CrystalReportViewer1.ActiveViewIndex = -1;
            CrystalReportViewer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            CrystalReportViewer1.DisplayGroupTree = false;
            CrystalReportViewer1.Location = new Point(6, 53);
            CrystalReportViewer1.Name = "CrystalReportViewer1";
            CrystalReportViewer1.SelectionFormula = "";
            CrystalReportViewer1.Size = new Size(812, 429);
            CrystalReportViewer1.TabIndex = 227;
            CrystalReportViewer1.ViewTimeSelectionFormula = "";
            // 
            // frmBatchDetour
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _cmdCancel;
            ClientSize = new Size(824, 487);
            Controls.Add(CrystalReportViewer1);
            Controls.Add(Panel1);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            MinimumSize = new Size(832, 507);
            Name = "frmBatchDetour";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " گزارش لیست عقب افتادگی بچ های تولید";
            Panel1.ResumeLayout(false);
            Load += new EventHandler(frmBatchDetour_Load);
            FormClosing += new FormClosingEventHandler(frmBatchDetour_FormClosing);
            ResumeLayout(false);
        }

        internal System.Windows.Forms.Panel Panel1;
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
        internal PSB_FarsiDateControl.PSB_DateControl txtCalcDate;
        internal CrystalDecisions.Windows.Forms.CrystalReportViewer CrystalReportViewer1;
    }
}