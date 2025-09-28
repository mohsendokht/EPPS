using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmProductionHalt : Form
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
            _cmdSave = new System.Windows.Forms.Button();
            _cmdSave.Click += new EventHandler(cmdSave_Click);
            _cmdExit = new System.Windows.Forms.Button();
            _cmdExit.Click += new EventHandler(cmdCancel_Click);
            _cmdDelete = new System.Windows.Forms.Button();
            _cmdDelete.Click += new EventHandler(cmdDelete_Click);
            GroupBox1 = new GroupBox();
            txtEndDate = new PSB_FarsiDateControl.PSB_DateControl();
            txtStartDate = new PSB_FarsiDateControl.PSB_DateControl();
            Label10 = new Label();
            Label11 = new Label();
            Label6 = new Label();
            Label5 = new Label();
            txtEndHour = new DateTimePicker();
            txtStartHour = new DateTimePicker();
            Panel1.SuspendLayout();
            GroupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // Panel1
            // 
            Panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Panel1.BackColor = SystemColors.Control;
            Panel1.BorderStyle = BorderStyle.FixedSingle;
            Panel1.Controls.Add(_cmdSave);
            Panel1.Controls.Add(_cmdExit);
            Panel1.Controls.Add(_cmdDelete);
            Panel1.ForeColor = Color.Black;
            Panel1.Location = new Point(9, 110);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(408, 48);
            Panel1.TabIndex = 209;
            // 
            // cmdSave
            // 
            _cmdSave.BackColor = Color.Transparent;
            _cmdSave.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdSave.ForeColor = Color.Black;
            _cmdSave.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdSave.Location = new Point(219, 10);
            _cmdSave.Name = "_cmdSave";
            _cmdSave.RightToLeft = RightToLeft.No;
            _cmdSave.Size = new Size(90, 28);
            _cmdSave.TabIndex = 4;
            _cmdSave.Text = "ثبت";
            _cmdSave.TextAlign = ContentAlignment.MiddleRight;
            _cmdSave.UseVisualStyleBackColor = false;
            // 
            // cmdExit
            // 
            _cmdExit.BackColor = Color.Transparent;
            _cmdExit.DialogResult = DialogResult.Cancel;
            _cmdExit.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdExit.ForeColor = Color.Red;
            _cmdExit.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdExit.Location = new Point(98, 10);
            _cmdExit.Name = "_cmdExit";
            _cmdExit.RightToLeft = RightToLeft.No;
            _cmdExit.Size = new Size(90, 28);
            _cmdExit.TabIndex = 5;
            _cmdExit.Text = "انصراف";
            _cmdExit.TextAlign = ContentAlignment.MiddleRight;
            _cmdExit.UseVisualStyleBackColor = false;
            // 
            // cmdDelete
            // 
            _cmdDelete.BackColor = Color.Transparent;
            _cmdDelete.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdDelete.ForeColor = Color.Black;
            _cmdDelete.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdDelete.Location = new Point(219, 10);
            _cmdDelete.Name = "_cmdDelete";
            _cmdDelete.RightToLeft = RightToLeft.No;
            _cmdDelete.Size = new Size(90, 28);
            _cmdDelete.TabIndex = 14;
            _cmdDelete.Text = "حذف";
            _cmdDelete.TextAlign = ContentAlignment.MiddleRight;
            _cmdDelete.UseVisualStyleBackColor = false;
            _cmdDelete.Visible = false;
            // 
            // GroupBox1
            // 
            GroupBox1.Controls.Add(txtEndDate);
            GroupBox1.Controls.Add(txtStartDate);
            GroupBox1.Controls.Add(Label10);
            GroupBox1.Controls.Add(Label11);
            GroupBox1.Controls.Add(Label6);
            GroupBox1.Controls.Add(Label5);
            GroupBox1.Controls.Add(txtEndHour);
            GroupBox1.Controls.Add(txtStartHour);
            GroupBox1.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            GroupBox1.Location = new Point(9, 12);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(408, 90);
            GroupBox1.TabIndex = 215;
            GroupBox1.TabStop = false;
            GroupBox1.Text = "زمان توقف";
            // 
            // txtEndDate
            // 
            txtEndDate.AutoValidate = AutoValidate.Disable;
            txtEndDate.BackColor = Color.White;
            txtEndDate.BackColorDateBox = Color.White;
            txtEndDate.DateButtonShow = true;
            txtEndDate.EnableDateText = true;
            txtEndDate.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtEndDate.FontMyDate = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtEndDate.Location = new Point(215, 57);
            txtEndDate.Margin = new Padding(4);
            txtEndDate.MinimumSize = new Size(96, 24);
            txtEndDate.Name = "txtEndDate";
            txtEndDate.Size = new Size(105, 24);
            txtEndDate.TabIndex = 2;
            // 
            // txtStartDate
            // 
            txtStartDate.AutoValidate = AutoValidate.Disable;
            txtStartDate.BackColor = Color.White;
            txtStartDate.BackColorDateBox = Color.White;
            txtStartDate.DateButtonShow = true;
            txtStartDate.EnableDateText = true;
            txtStartDate.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtStartDate.FontMyDate = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtStartDate.Location = new Point(215, 24);
            txtStartDate.Margin = new Padding(4);
            txtStartDate.MinimumSize = new Size(96, 24);
            txtStartDate.Name = "txtStartDate";
            txtStartDate.Size = new Size(105, 24);
            txtStartDate.TabIndex = 0;
            // 
            // Label10
            // 
            Label10.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label10.Location = new Point(320, 57);
            Label10.Name = "Label10";
            Label10.RightToLeft = RightToLeft.Yes;
            Label10.Size = new Size(66, 20);
            Label10.TabIndex = 209;
            Label10.Text = "تاریخ پایان";
            Label10.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label11
            // 
            Label11.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label11.Location = new Point(320, 24);
            Label11.Name = "Label11";
            Label11.RightToLeft = RightToLeft.Yes;
            Label11.Size = new Size(66, 20);
            Label11.TabIndex = 208;
            Label11.Text = "تاریخ شروع";
            Label11.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label6
            // 
            Label6.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label6.Location = new Point(119, 57);
            Label6.Name = "Label6";
            Label6.RightToLeft = RightToLeft.Yes;
            Label6.Size = new Size(75, 20);
            Label6.TabIndex = 205;
            Label6.Text = "ساعت پایان";
            Label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label5
            // 
            Label5.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label5.Location = new Point(119, 24);
            Label5.Name = "Label5";
            Label5.RightToLeft = RightToLeft.Yes;
            Label5.Size = new Size(75, 20);
            Label5.TabIndex = 204;
            Label5.Text = "ساعت شروع";
            Label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtEndHour
            // 
            txtEndHour.CustomFormat = "";
            txtEndHour.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtEndHour.Format = DateTimePickerFormat.Time;
            txtEndHour.Location = new Point(17, 57);
            txtEndHour.Name = "txtEndHour";
            txtEndHour.RightToLeft = RightToLeft.Yes;
            txtEndHour.RightToLeftLayout = true;
            txtEndHour.ShowUpDown = true;
            txtEndHour.Size = new Size(96, 21);
            txtEndHour.TabIndex = 3;
            txtEndHour.Value = new DateTime(2008, 6, 5, 0, 0, 0, 0);
            // 
            // txtStartHour
            // 
            txtStartHour.CustomFormat = "";
            txtStartHour.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtStartHour.Format = DateTimePickerFormat.Time;
            txtStartHour.Location = new Point(17, 24);
            txtStartHour.Name = "txtStartHour";
            txtStartHour.RightToLeftLayout = true;
            txtStartHour.ShowUpDown = true;
            txtStartHour.Size = new Size(96, 21);
            txtStartHour.TabIndex = 1;
            txtStartHour.Value = new DateTime(2008, 6, 5, 0, 0, 0, 0);
            // 
            // frmProductionHalt
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _cmdExit;
            ClientSize = new Size(426, 165);
            Controls.Add(GroupBox1);
            Controls.Add(Panel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmProductionHalt";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " مشخصات توقف عملیات";
            Panel1.ResumeLayout(false);
            GroupBox1.ResumeLayout(false);
            Load += new EventHandler(frmProductionHalt_Load);
            FormClosing += new FormClosingEventHandler(frmProductionHalt_FormClosing);
            ResumeLayout(false);
        }

        internal System.Windows.Forms.Panel Panel1;
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
                    _cmdExit.Click -= cmdCancel_Click;
                }

                _cmdExit = value;
                if (_cmdExit != null)
                {
                    _cmdExit.Click += cmdCancel_Click;
                }
            }
        }

        private System.Windows.Forms.Button _cmdDelete;

        internal System.Windows.Forms.Button cmdDelete
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdDelete;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdDelete != null)
                {
                    _cmdDelete.Click -= cmdDelete_Click;
                }

                _cmdDelete = value;
                if (_cmdDelete != null)
                {
                    _cmdDelete.Click += cmdDelete_Click;
                }
            }
        }

        internal GroupBox GroupBox1;
        internal Label Label10;
        internal Label Label11;
        internal Label Label6;
        internal Label Label5;
        internal DateTimePicker txtEndHour;
        internal DateTimePicker txtStartHour;
        internal PSB_FarsiDateControl.PSB_DateControl txtEndDate;
        internal PSB_FarsiDateControl.PSB_DateControl txtStartDate;
    }
}