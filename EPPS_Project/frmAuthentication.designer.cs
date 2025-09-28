using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmAuthentication : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components is object)
                {
                    components.Dispose();
                }
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAuthentication));
            TableLayoutPanel1 = new TableLayoutPanel();
            _Cancel = new System.Windows.Forms.Button();
            _Cancel.Click += new EventHandler(Cancel_Click);
            _OK = new System.Windows.Forms.Button();
            _OK.Click += new EventHandler(OK_Click);
            Panel1 = new System.Windows.Forms.Panel();
            _txtPassword = new TextBox();
            _txtPassword.KeyPress += new KeyPressEventHandler(txtUserCode_KeyPress);
            _txtUserCode = new TextBox();
            _txtUserCode.KeyPress += new KeyPressEventHandler(txtUserCode_KeyPress);
            PasswordLabel = new Label();
            UsernameLabel = new Label();
            _cbUserName = new ComboBox();
            _cbUserName.KeyPress += new KeyPressEventHandler(txtUserCode_KeyPress);
            LogoPictureBox = new PictureBox();
            TableLayoutPanel1.SuspendLayout();
            Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LogoPictureBox).BeginInit();
            SuspendLayout();
            // 
            // TableLayoutPanel1
            // 
            TableLayoutPanel1.AutoSize = true;
            TableLayoutPanel1.BackColor = Color.Gainsboro;
            TableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
            TableLayoutPanel1.ColumnCount = 2;
            TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.0f));
            TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.0f));
            TableLayoutPanel1.Controls.Add(_Cancel, 0, 0);
            TableLayoutPanel1.Controls.Add(_OK, 0, 0);
            TableLayoutPanel1.Location = new Point(34, 132);
            TableLayoutPanel1.Name = "TableLayoutPanel1";
            TableLayoutPanel1.RightToLeft = RightToLeft.Yes;
            TableLayoutPanel1.RowCount = 1;
            TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50.0f));
            TableLayoutPanel1.Size = new Size(189, 35);
            TableLayoutPanel1.TabIndex = 1;
            // 
            // Cancel
            // 
            _Cancel.BackColor = Color.White;
            _Cancel.DialogResult = DialogResult.Cancel;
            _Cancel.Font = new Font("Tahoma", 9.0f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _Cancel.Location = new Point(6, 6);
            _Cancel.Name = "_Cancel";
            _Cancel.Size = new Size(84, 23);
            _Cancel.TabIndex = 1;
            _Cancel.Text = "انصراف";
            _Cancel.UseVisualStyleBackColor = false;
            // 
            // OK
            // 
            _OK.BackColor = Color.White;
            _OK.Font = new Font("Tahoma", 9.0f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _OK.Location = new Point(99, 6);
            _OK.Name = "_OK";
            _OK.Size = new Size(84, 23);
            _OK.TabIndex = 0;
            _OK.Text = "تأیید";
            _OK.UseVisualStyleBackColor = false;
            // 
            // Panel1
            // 
            Panel1.BackColor = Color.Gainsboro;
            Panel1.BorderStyle = BorderStyle.FixedSingle;
            Panel1.Controls.Add(_txtPassword);
            Panel1.Controls.Add(_txtUserCode);
            Panel1.Controls.Add(PasswordLabel);
            Panel1.Controls.Add(UsernameLabel);
            Panel1.Controls.Add(_cbUserName);
            Panel1.Location = new Point(6, 16);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(244, 105);
            Panel1.TabIndex = 0;
            // 
            // txtPassword
            // 
            _txtPassword.AcceptsTab = true;
            _txtPassword.Location = new Point(11, 72);
            _txtPassword.Name = "_txtPassword";
            _txtPassword.PasswordChar = '*';
            _txtPassword.RightToLeft = RightToLeft.Yes;
            _txtPassword.Size = new Size(220, 20);
            _txtPassword.TabIndex = 1;
            // 
            // txtUserCode
            // 
            _txtUserCode.AcceptsTab = true;
            _txtUserCode.Location = new Point(11, 27);
            _txtUserCode.Name = "_txtUserCode";
            _txtUserCode.RightToLeft = RightToLeft.Yes;
            _txtUserCode.Size = new Size(220, 20);
            _txtUserCode.TabIndex = 0;
            // 
            // PasswordLabel
            // 
            PasswordLabel.AutoSize = true;
            PasswordLabel.BackColor = Color.Gainsboro;
            PasswordLabel.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            PasswordLabel.Location = new Point(188, 52);
            PasswordLabel.Name = "PasswordLabel";
            PasswordLabel.RightToLeft = RightToLeft.Yes;
            PasswordLabel.Size = new Size(46, 14);
            PasswordLabel.TabIndex = 6;
            PasswordLabel.Text = "رمز ورود";
            PasswordLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // UsernameLabel
            // 
            UsernameLabel.AutoSize = true;
            UsernameLabel.BackColor = Color.Gainsboro;
            UsernameLabel.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            UsernameLabel.Location = new Point(188, 7);
            UsernameLabel.Name = "UsernameLabel";
            UsernameLabel.RightToLeft = RightToLeft.Yes;
            UsernameLabel.Size = new Size(46, 14);
            UsernameLabel.TabIndex = 4;
            UsernameLabel.Text = "کد کاربر";
            UsernameLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cbUserName
            // 
            _cbUserName.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cbUserName.FormattingEnabled = true;
            _cbUserName.Location = new Point(12, 27);
            _cbUserName.Name = "_cbUserName";
            _cbUserName.RightToLeft = RightToLeft.Yes;
            _cbUserName.Size = new Size(219, 21);
            _cbUserName.TabIndex = 0;
            _cbUserName.Visible = false;
            // 
            // LogoPictureBox
            // 
            LogoPictureBox.Image = (Image)resources.GetObject("LogoPictureBox.Image");
            LogoPictureBox.Location = new Point(251, -2);
            LogoPictureBox.Name = "LogoPictureBox";
            LogoPictureBox.Size = new Size(156, 193);
            LogoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            LogoPictureBox.TabIndex = 2;
            LogoPictureBox.TabStop = false;
            // 
            // frmAuthentication
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Silver;
            CancelButton = _Cancel;
            ClientSize = new Size(408, 190);
            Controls.Add(LogoPictureBox);
            Controls.Add(TableLayoutPanel1);
            Controls.Add(Panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmAuthentication";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " EPPS Login";
            TableLayoutPanel1.ResumeLayout(false);
            Panel1.ResumeLayout(false);
            Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)LogoPictureBox).EndInit();
            Load += new EventHandler(frmAuthentication_Load);
            KeyDown += new KeyEventHandler(frmAuthentication_KeyDown);
            ResumeLayout(false);
            PerformLayout();
        }

        internal TableLayoutPanel TableLayoutPanel1;
        private System.Windows.Forms.Button _Cancel;

        internal System.Windows.Forms.Button Cancel
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _Cancel;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_Cancel != null)
                {
                    _Cancel.Click -= Cancel_Click;
                }

                _Cancel = value;
                if (_Cancel != null)
                {
                    _Cancel.Click += Cancel_Click;
                }
            }
        }

        private System.Windows.Forms.Button _OK;

        internal System.Windows.Forms.Button OK
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _OK;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_OK != null)
                {
                    _OK.Click -= OK_Click;
                }

                _OK = value;
                if (_OK != null)
                {
                    _OK.Click += OK_Click;
                }
            }
        }

        internal System.Windows.Forms.Panel Panel1;
        private TextBox _txtPassword;

        internal TextBox txtPassword
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtPassword;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtPassword != null)
                {
                    _txtPassword.KeyPress -= txtUserCode_KeyPress;
                }

                _txtPassword = value;
                if (_txtPassword != null)
                {
                    _txtPassword.KeyPress += txtUserCode_KeyPress;
                }
            }
        }

        private TextBox _txtUserCode;

        internal TextBox txtUserCode
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtUserCode;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtUserCode != null)
                {
                    _txtUserCode.KeyPress -= txtUserCode_KeyPress;
                }

                _txtUserCode = value;
                if (_txtUserCode != null)
                {
                    _txtUserCode.KeyPress += txtUserCode_KeyPress;
                }
            }
        }

        internal Label PasswordLabel;
        internal Label UsernameLabel;
        private ComboBox _cbUserName;

        internal ComboBox cbUserName
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cbUserName;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cbUserName != null)
                {
                    _cbUserName.KeyPress -= txtUserCode_KeyPress;
                }

                _cbUserName = value;
                if (_cbUserName != null)
                {
                    _cbUserName.KeyPress += txtUserCode_KeyPress;
                }
            }
        }

        internal PictureBox LogoPictureBox;
    }
}