using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmWorkPeriod : Form
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
            _cmdSave = new System.Windows.Forms.Button();
            _cmdSave.Click += new EventHandler(cmdSave_Click);
            _cmdCancel = new System.Windows.Forms.Button();
            _cmdCancel.Click += new EventHandler(cmdExit_Click);
            Label1 = new Label();
            txtDescription = new TextBox();
            Panel1 = new System.Windows.Forms.Panel();
            txtEndDate = new PSB_FarsiDateControl.PSB_DateControl();
            lblOperatorEndDate = new Label();
            txtStartDate = new PSB_FarsiDateControl.PSB_DateControl();
            lblOperatorStartDate = new Label();
            Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // cmdSave
            // 
            _cmdSave.BackColor = Color.Transparent;
            _cmdSave.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdSave.ForeColor = Color.Black;
            _cmdSave.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdSave.Location = new Point(197, 6);
            _cmdSave.Name = "_cmdSave";
            _cmdSave.RightToLeft = RightToLeft.No;
            _cmdSave.Size = new Size(90, 24);
            _cmdSave.TabIndex = 0;
            _cmdSave.Text = "تایید";
            _cmdSave.TextAlign = ContentAlignment.MiddleRight;
            _cmdSave.UseVisualStyleBackColor = false;
            // 
            // cmdCancel
            // 
            _cmdCancel.BackColor = Color.Transparent;
            _cmdCancel.DialogResult = DialogResult.Cancel;
            _cmdCancel.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdCancel.ForeColor = Color.Red;
            _cmdCancel.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdCancel.Location = new Point(77, 6);
            _cmdCancel.Name = "_cmdCancel";
            _cmdCancel.RightToLeft = RightToLeft.No;
            _cmdCancel.Size = new Size(90, 24);
            _cmdCancel.TabIndex = 1;
            _cmdCancel.Text = "انصراف";
            _cmdCancel.TextAlign = ContentAlignment.MiddleRight;
            _cmdCancel.UseVisualStyleBackColor = false;
            // 
            // Label1
            // 
            Label1.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label1.Location = new Point(323, 59);
            Label1.Name = "Label1";
            Label1.RightToLeft = RightToLeft.Yes;
            Label1.Size = new Size(55, 18);
            Label1.TabIndex = 94;
            Label1.Text = "توضیحات:";
            // 
            // txtDescription
            // 
            txtDescription.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtDescription.Location = new Point(9, 59);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(311, 53);
            txtDescription.TabIndex = 2;
            // 
            // Panel1
            // 
            Panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Panel1.BackColor = SystemColors.GradientInactiveCaption;
            Panel1.BorderStyle = BorderStyle.FixedSingle;
            Panel1.Controls.Add(_cmdSave);
            Panel1.Controls.Add(_cmdCancel);
            Panel1.Location = new Point(9, 131);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(366, 38);
            Panel1.TabIndex = 3;
            // 
            // txtEndDate
            // 
            txtEndDate.AutoValidate = AutoValidate.Disable;
            txtEndDate.BackColor = SystemColors.Control;
            txtEndDate.BackColorDateBox = Color.White;
            txtEndDate.DateButtonShow = true;
            txtEndDate.EnableDateText = true;
            txtEndDate.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtEndDate.FontMyDate = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtEndDate.Location = new Point(9, 18);
            txtEndDate.Margin = new Padding(4);
            txtEndDate.MinimumSize = new Size(96, 24);
            txtEndDate.Name = "txtEndDate";
            txtEndDate.Size = new Size(98, 24);
            txtEndDate.TabIndex = 1;
            // 
            // lblOperatorEndDate
            // 
            lblOperatorEndDate.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblOperatorEndDate.Location = new Point(107, 18);
            lblOperatorEndDate.Name = "lblOperatorEndDate";
            lblOperatorEndDate.RightToLeft = RightToLeft.Yes;
            lblOperatorEndDate.Size = new Size(52, 20);
            lblOperatorEndDate.TabIndex = 217;
            lblOperatorEndDate.Text = "پایان کار:";
            lblOperatorEndDate.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtStartDate
            // 
            txtStartDate.AutoValidate = AutoValidate.Disable;
            txtStartDate.BackColor = SystemColors.Control;
            txtStartDate.BackColorDateBox = Color.White;
            txtStartDate.DateButtonShow = true;
            txtStartDate.EnableDateText = true;
            txtStartDate.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtStartDate.FontMyDate = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtStartDate.Location = new Point(222, 18);
            txtStartDate.Margin = new Padding(4);
            txtStartDate.MinimumSize = new Size(96, 24);
            txtStartDate.Name = "txtStartDate";
            txtStartDate.Size = new Size(98, 24);
            txtStartDate.TabIndex = 0;
            // 
            // lblOperatorStartDate
            // 
            lblOperatorStartDate.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblOperatorStartDate.Location = new Point(319, 18);
            lblOperatorStartDate.Name = "lblOperatorStartDate";
            lblOperatorStartDate.RightToLeft = RightToLeft.Yes;
            lblOperatorStartDate.Size = new Size(59, 20);
            lblOperatorStartDate.TabIndex = 215;
            lblOperatorStartDate.Text = "شروع کار:";
            lblOperatorStartDate.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // frmWorkPeriod
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveCaptionText;
            CancelButton = _cmdCancel;
            ClientSize = new Size(384, 177);
            Controls.Add(txtDescription);
            Controls.Add(txtEndDate);
            Controls.Add(lblOperatorEndDate);
            Controls.Add(txtStartDate);
            Controls.Add(lblOperatorStartDate);
            Controls.Add(Panel1);
            Controls.Add(Label1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmWorkPeriod";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Tag = "0";
            Text = " ثبت سابقۀ شغلی اپراتور";
            Panel1.ResumeLayout(false);
            Load += new EventHandler(frmWorkPeriod_Load);
            ResumeLayout(false);
            PerformLayout();
        }

        internal Label Label1;
        internal System.Windows.Forms.Panel Panel1;
        internal Label lblOperatorEndDate;
        internal Label lblOperatorStartDate;
        private System.Windows.Forms.Button _cmdSave;

        protected System.Windows.Forms.Button cmdSave
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

        private System.Windows.Forms.Button _cmdCancel;

        protected System.Windows.Forms.Button cmdCancel
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
                    _cmdCancel.Click -= cmdExit_Click;
                }

                _cmdCancel = value;
                if (_cmdCancel != null)
                {
                    _cmdCancel.Click += cmdExit_Click;
                }
            }
        }

        protected TextBox txtDescription;
        protected PSB_FarsiDateControl.PSB_DateControl txtEndDate;
        protected PSB_FarsiDateControl.PSB_DateControl txtStartDate;
    }
}