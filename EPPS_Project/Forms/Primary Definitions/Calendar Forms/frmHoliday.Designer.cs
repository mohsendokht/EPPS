using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmHoliday : Form
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
            txtDay = new NumericUpDown();
            Label8 = new Label();
            Panel1 = new System.Windows.Forms.Panel();
            _cmdSave = new System.Windows.Forms.Button();
            _cmdSave.Click += new EventHandler(cmdSave_Click);
            _cmdExit = new System.Windows.Forms.Button();
            _cmdExit.Click += new EventHandler(cmdCancel_Click);
            _cmdDelete = new System.Windows.Forms.Button();
            _cmdDelete.Click += new EventHandler(cmdDelete_Click);
            lblName = new Label();
            txtDescription = new TextBox();
            cbMonth = new ComboBox();
            Label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)txtDay).BeginInit();
            Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // txtDay
            // 
            txtDay.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtDay.Location = new Point(11, 28);
            txtDay.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            txtDay.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            txtDay.Name = "txtDay";
            txtDay.Size = new Size(52, 21);
            txtDay.TabIndex = 76;
            txtDay.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // Label8
            // 
            Label8.BackColor = SystemColors.Control;
            Label8.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label8.ForeColor = Color.Black;
            Label8.Location = new Point(66, 29);
            Label8.Name = "Label8";
            Label8.RightToLeft = RightToLeft.Yes;
            Label8.Size = new Size(92, 17);
            Label8.TabIndex = 74;
            Label8.Text = "شماره روز در ماه";
            // 
            // Panel1
            // 
            Panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Panel1.BackColor = SystemColors.Control;
            Panel1.BorderStyle = BorderStyle.FixedSingle;
            Panel1.Controls.Add(_cmdSave);
            Panel1.Controls.Add(_cmdExit);
            Panel1.Controls.Add(_cmdDelete);
            Panel1.Location = new Point(9, 149);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(350, 48);
            Panel1.TabIndex = 77;
            // 
            // cmdSave
            // 
            _cmdSave.BackColor = Color.Transparent;
            _cmdSave.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdSave.ForeColor = Color.Black;
            _cmdSave.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdSave.Location = new Point(189, 10);
            _cmdSave.Name = "_cmdSave";
            _cmdSave.RightToLeft = RightToLeft.No;
            _cmdSave.Size = new Size(90, 28);
            _cmdSave.TabIndex = 6;
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
            _cmdExit.Location = new Point(70, 10);
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
            _cmdDelete.Location = new Point(189, 10);
            _cmdDelete.Name = "_cmdDelete";
            _cmdDelete.RightToLeft = RightToLeft.No;
            _cmdDelete.Size = new Size(90, 28);
            _cmdDelete.TabIndex = 8;
            _cmdDelete.Text = "حذف";
            _cmdDelete.TextAlign = ContentAlignment.MiddleRight;
            _cmdDelete.UseVisualStyleBackColor = false;
            _cmdDelete.Visible = false;
            // 
            // lblName
            // 
            lblName.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblName.Location = new Point(287, 65);
            lblName.Name = "lblName";
            lblName.RightToLeft = RightToLeft.Yes;
            lblName.Size = new Size(71, 17);
            lblName.TabIndex = 78;
            lblName.Text = "علت تعطیلی";
            // 
            // txtDescription
            // 
            txtDescription.BorderStyle = BorderStyle.None;
            txtDescription.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtDescription.Location = new Point(11, 65);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(273, 69);
            txtDescription.TabIndex = 79;
            // 
            // cbMonth
            // 
            cbMonth.DropDownStyle = ComboBoxStyle.DropDownList;
            cbMonth.FlatStyle = FlatStyle.Flat;
            cbMonth.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cbMonth.FormattingEnabled = true;
            cbMonth.Items.AddRange(new object[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" });
            cbMonth.Location = new Point(181, 28);
            cbMonth.Name = "cbMonth";
            cbMonth.Size = new Size(103, 21);
            cbMonth.TabIndex = 277;
            // 
            // Label1
            // 
            Label1.BackColor = SystemColors.Control;
            Label1.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label1.ForeColor = Color.Black;
            Label1.Location = new Point(287, 29);
            Label1.Name = "Label1";
            Label1.RightToLeft = RightToLeft.Yes;
            Label1.Size = new Size(71, 17);
            Label1.TabIndex = 276;
            Label1.Text = "نام ماه";
            // 
            // frmHoliday
            // 
            AcceptButton = _cmdSave;
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _cmdExit;
            ClientSize = new Size(368, 203);
            Controls.Add(cbMonth);
            Controls.Add(Label1);
            Controls.Add(lblName);
            Controls.Add(txtDescription);
            Controls.Add(Panel1);
            Controls.Add(txtDay);
            Controls.Add(Label8);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Location = new Point(50, 50);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmHoliday";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " مشخصات روزهای تعطیل رسمی در تقویم سال";
            ((System.ComponentModel.ISupportInitialize)txtDay).EndInit();
            Panel1.ResumeLayout(false);
            Load += new EventHandler(Form1_Load);
            FormClosing += new FormClosingEventHandler(frmOperationTitle_FormClosing);
            ResumeLayout(false);
            PerformLayout();
        }

        internal NumericUpDown txtDay;
        internal Label Label8;
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

        internal Label lblName;
        internal TextBox txtDescription;
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

        internal ComboBox cbMonth;
        internal Label Label1;
    }
}