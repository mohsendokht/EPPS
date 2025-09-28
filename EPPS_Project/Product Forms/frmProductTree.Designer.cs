using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmProductTree : Form
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
            _cmdExit = new System.Windows.Forms.Button();
            _cmdExit.Click += new EventHandler(cmdExit_Click);
            Panel1 = new System.Windows.Forms.Panel();
            _cmdDelete = new System.Windows.Forms.Button();
            _cmdDelete.Click += new EventHandler(cmdDelete_Click);
            Label4 = new Label();
            _chkDefualt = new CheckBox();
            _chkDefualt.KeyPress += new KeyPressEventHandler(Controls_KeyPress);
            _chkDefualt.Click += new EventHandler(chkDefualt_Click);
            Label2 = new Label();
            lblName = new Label();
            _txtTitle = new TextBox();
            _txtTitle.KeyPress += new KeyPressEventHandler(Controls_KeyPress);
            txtCode = new NumericUpDown();
            cbProductName = new ComboBox();
            Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtCode).BeginInit();
            SuspendLayout();
            // 
            // cmdSave
            // 
            _cmdSave.BackColor = Color.Transparent;
            _cmdSave.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdSave.ForeColor = Color.Black;
            _cmdSave.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdSave.Location = new Point(269, 10);
            _cmdSave.Name = "_cmdSave";
            _cmdSave.RightToLeft = RightToLeft.No;
            _cmdSave.Size = new Size(90, 28);
            _cmdSave.TabIndex = 3;
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
            _cmdExit.Location = new Point(140, 10);
            _cmdExit.Name = "_cmdExit";
            _cmdExit.RightToLeft = RightToLeft.No;
            _cmdExit.Size = new Size(90, 28);
            _cmdExit.TabIndex = 5;
            _cmdExit.Text = "انصراف";
            _cmdExit.TextAlign = ContentAlignment.MiddleRight;
            _cmdExit.UseVisualStyleBackColor = false;
            // 
            // Panel1
            // 
            Panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Panel1.BackColor = SystemColors.Control;
            Panel1.BorderStyle = BorderStyle.FixedSingle;
            Panel1.Controls.Add(_cmdSave);
            Panel1.Controls.Add(_cmdExit);
            Panel1.Controls.Add(_cmdDelete);
            Panel1.Location = new Point(9, 153);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(503, 48);
            Panel1.TabIndex = 87;
            // 
            // cmdDelete
            // 
            _cmdDelete.BackColor = Color.Transparent;
            _cmdDelete.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdDelete.ForeColor = Color.Black;
            _cmdDelete.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdDelete.Location = new Point(270, 10);
            _cmdDelete.Name = "_cmdDelete";
            _cmdDelete.RightToLeft = RightToLeft.No;
            _cmdDelete.Size = new Size(90, 28);
            _cmdDelete.TabIndex = 4;
            _cmdDelete.Text = "حذف";
            _cmdDelete.TextAlign = ContentAlignment.MiddleRight;
            _cmdDelete.UseVisualStyleBackColor = false;
            _cmdDelete.Visible = false;
            // 
            // Label4
            // 
            Label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label4.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label4.Location = new Point(379, 70);
            Label4.Name = "Label4";
            Label4.RightToLeft = RightToLeft.Yes;
            Label4.Size = new Size(98, 19);
            Label4.TabIndex = 160;
            Label4.Text = "کد درخت محصول:";
            // 
            // chkDefualt
            // 
            _chkDefualt.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _chkDefualt.AutoCheck = false;
            _chkDefualt.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _chkDefualt.Location = new Point(34, 70);
            _chkDefualt.Name = "_chkDefualt";
            _chkDefualt.RightToLeft = RightToLeft.Yes;
            _chkDefualt.Size = new Size(203, 19);
            _chkDefualt.TabIndex = 2;
            _chkDefualt.Text = "درخت پیش فرض محصول می باشد";
            _chkDefualt.TextAlign = ContentAlignment.MiddleRight;
            _chkDefualt.UseVisualStyleBackColor = true;
            // 
            // Label2
            // 
            Label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label2.BackColor = Color.Transparent;
            Label2.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label2.ForeColor = Color.Black;
            Label2.Location = new Point(379, 29);
            Label2.Name = "Label2";
            Label2.RightToLeft = RightToLeft.Yes;
            Label2.Size = new Size(65, 19);
            Label2.TabIndex = 158;
            Label2.Text = "نام محصول:";
            // 
            // lblName
            // 
            lblName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblName.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblName.Location = new Point(379, 112);
            lblName.Name = "lblName";
            lblName.RightToLeft = RightToLeft.Yes;
            lblName.Size = new Size(113, 19);
            lblName.TabIndex = 156;
            lblName.Text = "عنوان درخت محصول:";
            // 
            // txtTitle
            // 
            _txtTitle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _txtTitle.BackColor = Color.White;
            _txtTitle.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _txtTitle.Location = new Point(34, 111);
            _txtTitle.Name = "_txtTitle";
            _txtTitle.Size = new Size(342, 21);
            _txtTitle.TabIndex = 1;
            // 
            // txtCode
            // 
            txtCode.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtCode.Location = new Point(259, 69);
            txtCode.Maximum = new decimal(new int[] { 2147483647, 0, 0, 0 });
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(117, 20);
            txtCode.TabIndex = 163;
            // 
            // cbProductName
            // 
            cbProductName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbProductName.DropDownStyle = ComboBoxStyle.DropDownList;
            cbProductName.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cbProductName.FormattingEnabled = true;
            cbProductName.Location = new Point(34, 28);
            cbProductName.Name = "cbProductName";
            cbProductName.Size = new Size(342, 21);
            cbProductName.TabIndex = 167;
            // 
            // frmProductTree
            // 
            AcceptButton = _cmdSave;
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _cmdExit;
            ClientSize = new Size(521, 208);
            Controls.Add(cbProductName);
            Controls.Add(txtCode);
            Controls.Add(Label4);
            Controls.Add(_chkDefualt);
            Controls.Add(Label2);
            Controls.Add(lblName);
            Controls.Add(_txtTitle);
            Controls.Add(Panel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Location = new Point(50, 50);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmProductTree";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " مشخصات کلی درخت محصول";
            Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)txtCode).EndInit();
            Load += new EventHandler(Form1_Load);
            FormClosing += new FormClosingEventHandler(frmProduct_FormClosing);
            ResumeLayout(false);
            PerformLayout();
        }

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
                    _cmdExit.Click -= cmdExit_Click;
                }

                _cmdExit = value;
                if (_cmdExit != null)
                {
                    _cmdExit.Click += cmdExit_Click;
                }
            }
        }

        internal System.Windows.Forms.Panel Panel1;
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

        internal Label Label4;
        private CheckBox _chkDefualt;

        internal CheckBox chkDefualt
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _chkDefualt;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_chkDefualt != null)
                {
                    _chkDefualt.KeyPress -= Controls_KeyPress;
                    _chkDefualt.Click -= chkDefualt_Click;
                }

                _chkDefualt = value;
                if (_chkDefualt != null)
                {
                    _chkDefualt.KeyPress += Controls_KeyPress;
                    _chkDefualt.Click += chkDefualt_Click;
                }
            }
        }

        internal Label Label2;
        internal Label lblName;
        private TextBox _txtTitle;

        internal TextBox txtTitle
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtTitle;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtTitle != null)
                {
                    _txtTitle.KeyPress -= Controls_KeyPress;
                }

                _txtTitle = value;
                if (_txtTitle != null)
                {
                    _txtTitle.KeyPress += Controls_KeyPress;
                }
            }
        }

        internal NumericUpDown txtCode;
        internal ComboBox cbProductName;
    }
}