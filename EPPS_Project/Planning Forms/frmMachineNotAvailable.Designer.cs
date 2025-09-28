using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmMachineNotAvailable : Form
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
            GroupBox1 = new GroupBox();
            txtEndDate = new PSB_FarsiDateControl.PSB_DateControl();
            txtStartDate = new PSB_FarsiDateControl.PSB_DateControl();
            Label10 = new Label();
            Label11 = new Label();
            Label6 = new Label();
            Label5 = new Label();
            txtEndHour = new DateTimePicker();
            txtStartHour = new DateTimePicker();
            cbMachine = new ComboBox();
            Label2 = new Label();
            Panel1 = new System.Windows.Forms.Panel();
            _cmdSave = new System.Windows.Forms.Button();
            _cmdSave.Click += new EventHandler(cmdSave_Click);
            _cmdExit = new System.Windows.Forms.Button();
            _cmdExit.Click += new EventHandler(cmdCancel_Click);
            _cmdDelete = new System.Windows.Forms.Button();
            _cmdDelete.Click += new EventHandler(cmdDelete_Click);
            cbReason = new ComboBox();
            Label1 = new Label();
            GroupBox1.SuspendLayout();
            Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // GroupBox1
            // 
            GroupBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox1.Controls.Add(Label10);
            GroupBox1.Controls.Add(Label11);
            GroupBox1.Controls.Add(Label6);
            GroupBox1.Controls.Add(Label5);
            GroupBox1.Controls.Add(txtEndDate);
            GroupBox1.Controls.Add(txtStartDate);
            GroupBox1.Controls.Add(txtEndHour);
            GroupBox1.Controls.Add(txtStartHour);
            GroupBox1.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            GroupBox1.Location = new Point(12, 100);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(399, 90);
            GroupBox1.TabIndex = 212;
            GroupBox1.TabStop = false;
            GroupBox1.Text = "زمان در دسترس نبودن ماشین";
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
            txtEndDate.Location = new Point(201, 53);
            txtEndDate.Margin = new Padding(4);
            txtEndDate.MinimumSize = new Size(96, 24);
            txtEndDate.Name = "txtEndDate";
            txtEndDate.Size = new Size(111, 24);
            txtEndDate.TabIndex = 3;
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
            txtStartDate.Location = new Point(201, 23);
            txtStartDate.Margin = new Padding(4);
            txtStartDate.MinimumSize = new Size(96, 24);
            txtStartDate.Name = "txtStartDate";
            txtStartDate.Size = new Size(111, 24);
            txtStartDate.TabIndex = 1;
            // 
            // Label10
            // 
            Label10.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label10.Location = new Point(315, 57);
            Label10.Name = "Label10";
            Label10.RightToLeft = RightToLeft.Yes;
            Label10.Size = new Size(67, 20);
            Label10.TabIndex = 209;
            Label10.Text = "تاریخ پایان:";
            Label10.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label11
            // 
            Label11.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label11.Location = new Point(315, 24);
            Label11.Name = "Label11";
            Label11.RightToLeft = RightToLeft.Yes;
            Label11.Size = new Size(67, 20);
            Label11.TabIndex = 208;
            Label11.Text = "تاریخ شروع:";
            Label11.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label6
            // 
            Label6.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label6.Location = new Point(79, 57);
            Label6.Name = "Label6";
            Label6.RightToLeft = RightToLeft.Yes;
            Label6.Size = new Size(80, 20);
            Label6.TabIndex = 205;
            Label6.Text = "ساعت پایان:";
            Label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label5
            // 
            Label5.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label5.Location = new Point(79, 24);
            Label5.Name = "Label5";
            Label5.RightToLeft = RightToLeft.Yes;
            Label5.Size = new Size(80, 20);
            Label5.TabIndex = 204;
            Label5.Text = "ساعت شروع:";
            Label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtEndHour
            // 
            txtEndHour.CustomFormat = "HH:mm";
            txtEndHour.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtEndHour.Format = DateTimePickerFormat.Custom;
            txtEndHour.Location = new Point(17, 57);
            txtEndHour.Name = "txtEndHour";
            txtEndHour.RightToLeft = RightToLeft.No;
            txtEndHour.RightToLeftLayout = true;
            txtEndHour.ShowUpDown = true;
            txtEndHour.Size = new Size(59, 21);
            txtEndHour.TabIndex = 4;
            txtEndHour.Value = new DateTime(2008, 6, 5, 0, 0, 0, 0);
            // 
            // txtStartHour
            // 
            txtStartHour.CustomFormat = "HH:mm";
            txtStartHour.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtStartHour.Format = DateTimePickerFormat.Custom;
            txtStartHour.Location = new Point(17, 24);
            txtStartHour.Name = "txtStartHour";
            txtStartHour.RightToLeft = RightToLeft.No;
            txtStartHour.RightToLeftLayout = true;
            txtStartHour.ShowUpDown = true;
            txtStartHour.Size = new Size(59, 21);
            txtStartHour.TabIndex = 2;
            txtStartHour.Value = new DateTime(2008, 6, 5, 0, 0, 0, 0);
            // 
            // cbMachine
            // 
            cbMachine.DropDownStyle = ComboBoxStyle.DropDownList;
            cbMachine.FlatStyle = FlatStyle.Flat;
            cbMachine.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cbMachine.FormattingEnabled = true;
            cbMachine.Location = new Point(12, 22);
            cbMachine.Name = "cbMachine";
            cbMachine.Size = new Size(346, 21);
            cbMachine.TabIndex = 0;
            // 
            // Label2
            // 
            Label2.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label2.Location = new Point(364, 21);
            Label2.Name = "Label2";
            Label2.RightToLeft = RightToLeft.Yes;
            Label2.Size = new Size(48, 20);
            Label2.TabIndex = 211;
            Label2.Text = "ماشین:";
            Label2.TextAlign = ContentAlignment.MiddleLeft;
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
            Panel1.Location = new Point(12, 199);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(399, 48);
            Panel1.TabIndex = 210;
            // 
            // cmdSave
            // 
            _cmdSave.BackColor = Color.Transparent;
            _cmdSave.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdSave.ForeColor = Color.Black;
            _cmdSave.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdSave.Location = new Point(212, 10);
            _cmdSave.Name = "_cmdSave";
            _cmdSave.RightToLeft = RightToLeft.No;
            _cmdSave.Size = new Size(90, 28);
            _cmdSave.TabIndex = 5;
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
            _cmdExit.Location = new Point(94, 10);
            _cmdExit.Name = "_cmdExit";
            _cmdExit.RightToLeft = RightToLeft.No;
            _cmdExit.Size = new Size(90, 28);
            _cmdExit.TabIndex = 6;
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
            _cmdDelete.Location = new Point(212, 10);
            _cmdDelete.Name = "_cmdDelete";
            _cmdDelete.RightToLeft = RightToLeft.No;
            _cmdDelete.Size = new Size(90, 28);
            _cmdDelete.TabIndex = 14;
            _cmdDelete.Text = "حذف";
            _cmdDelete.TextAlign = ContentAlignment.MiddleRight;
            _cmdDelete.UseVisualStyleBackColor = false;
            _cmdDelete.Visible = false;
            // 
            // cbReason
            // 
            cbReason.DropDownStyle = ComboBoxStyle.DropDownList;
            cbReason.FlatStyle = FlatStyle.Flat;
            cbReason.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cbReason.FormattingEnabled = true;
            cbReason.Location = new Point(12, 60);
            cbReason.Name = "cbReason";
            cbReason.Size = new Size(346, 21);
            cbReason.TabIndex = 213;
            // 
            // Label1
            // 
            Label1.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label1.Location = new Point(364, 61);
            Label1.Name = "Label1";
            Label1.RightToLeft = RightToLeft.Yes;
            Label1.Size = new Size(48, 19);
            Label1.TabIndex = 214;
            Label1.Text = "علت:";
            Label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // frmMachineNotAvailable
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _cmdExit;
            ClientSize = new Size(423, 253);
            Controls.Add(cbReason);
            Controls.Add(Label1);
            Controls.Add(GroupBox1);
            Controls.Add(cbMachine);
            Controls.Add(Label2);
            Controls.Add(Panel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmMachineNotAvailable";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " مشخصات زمان در دسترس نبودن ماشین";
            GroupBox1.ResumeLayout(false);
            Panel1.ResumeLayout(false);
            Load += new EventHandler(frmRealProduction_Load);
            FormClosing += new FormClosingEventHandler(frmRealProduction_FormClosing);
            ResumeLayout(false);
        }

        internal GroupBox GroupBox1;
        internal Label Label10;
        internal Label Label11;
        internal Label Label6;
        internal Label Label5;
        internal DateTimePicker txtEndHour;
        internal DateTimePicker txtStartHour;
        internal ComboBox cbMachine;
        internal Label Label2;
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

        internal PSB_FarsiDateControl.PSB_DateControl txtEndDate;
        internal PSB_FarsiDateControl.PSB_DateControl txtStartDate;
        internal ComboBox cbReason;
        internal Label Label1;
    }
}