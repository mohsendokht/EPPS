using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmOperationUnit : Form
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
            _cmdExit = new System.Windows.Forms.Button();
            _cmdExit.Click += new EventHandler(cmdCancel_Click);
            _cmdDelete = new System.Windows.Forms.Button();
            _cmdDelete.Click += new EventHandler(cmdDelete_Click);
            Label1 = new Label();
            lblName = new Label();
            _cmdSave = new System.Windows.Forms.Button();
            _cmdSave.Click += new EventHandler(cmdSave_Click);
            Panel1 = new System.Windows.Forms.Panel();
            cbOperation = new ComboBox();
            cbUnit = new ComboBox();
            txtConversionFactor = new NumericUpDown();
            Label8 = new Label();
            Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtConversionFactor).BeginInit();
            SuspendLayout();
            // 
            // cmdExit
            // 
            _cmdExit.BackColor = Color.Transparent;
            _cmdExit.DialogResult = DialogResult.Cancel;
            _cmdExit.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdExit.ForeColor = Color.Red;
            _cmdExit.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdExit.Location = new Point(74, 10);
            _cmdExit.Name = "_cmdExit";
            _cmdExit.RightToLeft = RightToLeft.No;
            _cmdExit.Size = new Size(90, 28);
            _cmdExit.TabIndex = 3;
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
            _cmdDelete.Location = new Point(194, 10);
            _cmdDelete.Name = "_cmdDelete";
            _cmdDelete.RightToLeft = RightToLeft.No;
            _cmdDelete.Size = new Size(90, 28);
            _cmdDelete.TabIndex = 8;
            _cmdDelete.Text = "حذف";
            _cmdDelete.TextAlign = ContentAlignment.MiddleRight;
            _cmdDelete.UseVisualStyleBackColor = false;
            _cmdDelete.Visible = false;
            // 
            // Label1
            // 
            Label1.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label1.Location = new Point(284, 32);
            Label1.Name = "Label1";
            Label1.RightToLeft = RightToLeft.Yes;
            Label1.Size = new Size(66, 17);
            Label1.TabIndex = 174;
            Label1.Text = "کد عملیات";
            // 
            // lblName
            // 
            lblName.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblName.Location = new Point(284, 69);
            lblName.Name = "lblName";
            lblName.RightToLeft = RightToLeft.Yes;
            lblName.Size = new Size(66, 17);
            lblName.TabIndex = 173;
            lblName.Text = "نام واحد";
            // 
            // cmdSave
            // 
            _cmdSave.BackColor = Color.Transparent;
            _cmdSave.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdSave.ForeColor = Color.Black;
            _cmdSave.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdSave.Location = new Point(194, 10);
            _cmdSave.Name = "_cmdSave";
            _cmdSave.RightToLeft = RightToLeft.No;
            _cmdSave.Size = new Size(90, 28);
            _cmdSave.TabIndex = 2;
            _cmdSave.Text = "ثبت";
            _cmdSave.TextAlign = ContentAlignment.MiddleRight;
            _cmdSave.UseVisualStyleBackColor = false;
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
            Panel1.Location = new Point(9, 140);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(360, 48);
            Panel1.TabIndex = 172;
            // 
            // cbOperation
            // 
            cbOperation.DropDownStyle = ComboBoxStyle.DropDownList;
            cbOperation.FlatStyle = FlatStyle.Flat;
            cbOperation.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cbOperation.FormattingEnabled = true;
            cbOperation.Location = new Point(29, 30);
            cbOperation.Name = "cbOperation";
            cbOperation.Size = new Size(252, 21);
            cbOperation.TabIndex = 0;
            // 
            // cbUnit
            // 
            cbUnit.DropDownStyle = ComboBoxStyle.DropDownList;
            cbUnit.FlatStyle = FlatStyle.Flat;
            cbUnit.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cbUnit.FormattingEnabled = true;
            cbUnit.Location = new Point(29, 67);
            cbUnit.Name = "cbUnit";
            cbUnit.Size = new Size(252, 21);
            cbUnit.TabIndex = 1;
            // 
            // txtConversionFactor
            // 
            txtConversionFactor.DecimalPlaces = 2;
            txtConversionFactor.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtConversionFactor.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            txtConversionFactor.Location = new Point(182, 104);
            txtConversionFactor.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            txtConversionFactor.Name = "txtConversionFactor";
            txtConversionFactor.Size = new Size(99, 21);
            txtConversionFactor.TabIndex = 2;
            // 
            // Label8
            // 
            Label8.BackColor = SystemColors.Control;
            Label8.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label8.ForeColor = Color.Black;
            Label8.Location = new Point(284, 106);
            Label8.Name = "Label8";
            Label8.RightToLeft = RightToLeft.Yes;
            Label8.Size = new Size(66, 17);
            Label8.TabIndex = 177;
            Label8.Text = "ضریب تبدیل";
            // 
            // frmOperationUnit
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(378, 195);
            Controls.Add(txtConversionFactor);
            Controls.Add(Label8);
            Controls.Add(cbUnit);
            Controls.Add(cbOperation);
            Controls.Add(Label1);
            Controls.Add(lblName);
            Controls.Add(Panel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmOperationUnit";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " مشخصات واحد عملیات";
            Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)txtConversionFactor).EndInit();
            Load += new EventHandler(frmOperationUnit_Load);
            FormClosing += new FormClosingEventHandler(frmOperationUnit_FormClosing);
            ResumeLayout(false);
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

        internal Label Label1;
        internal Label lblName;
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

        internal System.Windows.Forms.Panel Panel1;
        internal ComboBox cbOperation;
        internal ComboBox cbUnit;
        internal NumericUpDown txtConversionFactor;
        internal Label Label8;
    }
}