using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmBackup : Form
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
            components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBackup));
            GroupBox1 = new GroupBox();
            _chkBackup = new CheckBox();
            _chkBackup.CheckedChanged += new EventHandler(chkBackup_CheckedChanged);
            _cmdUMBrowser = new System.Windows.Forms.Button();
            _cmdUMBrowser.Click += new EventHandler(cmdUMBrowser_Click);
            _cmdBrowser = new System.Windows.Forms.Button();
            _cmdBrowser.Click += new EventHandler(cmdBrowser_Click);
            _chkUMBackup = new CheckBox();
            _chkUMBackup.CheckedChanged += new EventHandler(chkUMBackup_CheckedChanged);
            _txtUMBackupFileName = new TextBox();
            _txtUMBackupFileName.KeyPress += new KeyPressEventHandler(txtBackupFileName_KeyPress);
            _txtBackupFileName = new TextBox();
            _txtBackupFileName.KeyPress += new KeyPressEventHandler(txtBackupFileName_KeyPress);
            lblUMBackupFileName = new Label();
            lblBackupFileName = new Label();
            GroupBox2 = new GroupBox();
            _cmdCancel = new System.Windows.Forms.Button();
            _cmdCancel.Click += new EventHandler(cmdCancel_Click);
            ImageList1 = new System.Windows.Forms.ImageList(components);
            _cmdRestore = new System.Windows.Forms.Button();
            _cmdRestore.Click += new EventHandler(cmdRestore_Click);
            _cmdBackup = new System.Windows.Forms.Button();
            _cmdBackup.Click += new EventHandler(cmdBackup_Click);
            Label2 = new Label();
            Label3 = new Label();
            Label4 = new Label();
            GroupBox1.SuspendLayout();
            GroupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // GroupBox1
            // 
            GroupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            GroupBox1.Controls.Add(_chkBackup);
            GroupBox1.Controls.Add(_cmdUMBrowser);
            GroupBox1.Controls.Add(_cmdBrowser);
            GroupBox1.Controls.Add(_chkUMBackup);
            GroupBox1.Controls.Add(_txtUMBackupFileName);
            GroupBox1.Controls.Add(_txtBackupFileName);
            GroupBox1.Controls.Add(lblUMBackupFileName);
            GroupBox1.Controls.Add(lblBackupFileName);
            GroupBox1.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            GroupBox1.Location = new Point(12, 90);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(529, 132);
            GroupBox1.TabIndex = 19;
            GroupBox1.TabStop = false;
            GroupBox1.Text = "آدرس پشتیبان";
            // 
            // chkBackup
            // 
            _chkBackup.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _chkBackup.Checked = true;
            _chkBackup.CheckState = CheckState.Checked;
            _chkBackup.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _chkBackup.ForeColor = Color.Blue;
            _chkBackup.Location = new Point(458, 26);
            _chkBackup.Name = "_chkBackup";
            _chkBackup.Size = new Size(65, 20);
            _chkBackup.TabIndex = 19;
            _chkBackup.Text = "نرم افزار";
            _chkBackup.UseVisualStyleBackColor = true;
            // 
            // cmdUMBrowser
            // 
            _cmdUMBrowser.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _cmdUMBrowser.DialogResult = DialogResult.Cancel;
            _cmdUMBrowser.Enabled = false;
            _cmdUMBrowser.FlatStyle = FlatStyle.System;
            _cmdUMBrowser.Font = new Font("Microsoft Sans Serif", 8.0f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(1), true);
            _cmdUMBrowser.Location = new Point(8, 101);
            _cmdUMBrowser.Name = "_cmdUMBrowser";
            _cmdUMBrowser.Size = new Size(41, 24);
            _cmdUMBrowser.TabIndex = 18;
            _cmdUMBrowser.Text = "...";
            _cmdUMBrowser.TextAlign = ContentAlignment.TopCenter;
            _cmdUMBrowser.UseVisualStyleBackColor = true;
            // 
            // cmdBrowser
            // 
            _cmdBrowser.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _cmdBrowser.DialogResult = DialogResult.Cancel;
            _cmdBrowser.FlatStyle = FlatStyle.System;
            _cmdBrowser.Font = new Font("Microsoft Sans Serif", 8.0f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(1), true);
            _cmdBrowser.Location = new Point(8, 49);
            _cmdBrowser.Name = "_cmdBrowser";
            _cmdBrowser.Size = new Size(41, 24);
            _cmdBrowser.TabIndex = 17;
            _cmdBrowser.Text = "...";
            _cmdBrowser.TextAlign = ContentAlignment.TopCenter;
            _cmdBrowser.UseVisualStyleBackColor = true;
            // 
            // chkUMBackup
            // 
            _chkUMBackup.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _chkUMBackup.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _chkUMBackup.ForeColor = Color.Blue;
            _chkUMBackup.Location = new Point(430, 79);
            _chkUMBackup.Name = "_chkUMBackup";
            _chkUMBackup.Size = new Size(93, 20);
            _chkUMBackup.TabIndex = 16;
            _chkUMBackup.Text = "مدیریت کاربران";
            _chkUMBackup.UseVisualStyleBackColor = true;
            // 
            // txtUMBackupFileName
            // 
            _txtUMBackupFileName.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _txtUMBackupFileName.Enabled = false;
            _txtUMBackupFileName.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(0));
            _txtUMBackupFileName.Location = new Point(54, 103);
            _txtUMBackupFileName.Name = "_txtUMBackupFileName";
            _txtUMBackupFileName.RightToLeft = RightToLeft.No;
            _txtUMBackupFileName.Size = new Size(346, 21);
            _txtUMBackupFileName.TabIndex = 14;
            // 
            // txtBackupFileName
            // 
            _txtBackupFileName.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _txtBackupFileName.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(0));
            _txtBackupFileName.Location = new Point(54, 51);
            _txtBackupFileName.Name = "_txtBackupFileName";
            _txtBackupFileName.RightToLeft = RightToLeft.No;
            _txtBackupFileName.Size = new Size(346, 21);
            _txtBackupFileName.TabIndex = 12;
            // 
            // lblUMBackupFileName
            // 
            lblUMBackupFileName.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblUMBackupFileName.BackColor = SystemColors.Control;
            lblUMBackupFileName.Enabled = false;
            lblUMBackupFileName.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(0));
            lblUMBackupFileName.ForeColor = Color.Black;
            lblUMBackupFileName.Location = new Point(401, 102);
            lblUMBackupFileName.Name = "lblUMBackupFileName";
            lblUMBackupFileName.Size = new Size(125, 22);
            lblUMBackupFileName.TabIndex = 15;
            lblUMBackupFileName.Text = "نام و مسیر فایل پشتیبان:";
            lblUMBackupFileName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblBackupFileName
            // 
            lblBackupFileName.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblBackupFileName.BackColor = SystemColors.Control;
            lblBackupFileName.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(0));
            lblBackupFileName.ForeColor = Color.Black;
            lblBackupFileName.Location = new Point(401, 49);
            lblBackupFileName.Name = "lblBackupFileName";
            lblBackupFileName.Size = new Size(125, 22);
            lblBackupFileName.TabIndex = 13;
            lblBackupFileName.Text = "نام و مسیر فایل پشتیبان:";
            lblBackupFileName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox2
            // 
            GroupBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox2.Controls.Add(_cmdCancel);
            GroupBox2.Controls.Add(_cmdRestore);
            GroupBox2.Controls.Add(_cmdBackup);
            GroupBox2.Location = new Point(12, 225);
            GroupBox2.Name = "GroupBox2";
            GroupBox2.Size = new Size(529, 61);
            GroupBox2.TabIndex = 20;
            GroupBox2.TabStop = false;
            // 
            // cmdCancel
            // 
            _cmdCancel.DialogResult = DialogResult.Cancel;
            _cmdCancel.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdCancel.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdCancel.ImageIndex = 0;
            _cmdCancel.ImageList = ImageList1;
            _cmdCancel.Location = new Point(142, 22);
            _cmdCancel.Name = "_cmdCancel";
            _cmdCancel.RightToLeft = RightToLeft.No;
            _cmdCancel.Size = new Size(112, 26);
            _cmdCancel.TabIndex = 10;
            _cmdCancel.Text = "انصراف";
            _cmdCancel.TextAlign = ContentAlignment.MiddleRight;
            _cmdCancel.UseVisualStyleBackColor = true;
            // 
            // ImageList1
            // 
            ImageList1.ImageStream = (ImageListStreamer)resources.GetObject("ImageList1.ImageStream");
            ImageList1.TransparentColor = Color.Transparent;
            ImageList1.Images.SetKeyName(0, "Cancel.ico");
            ImageList1.Images.SetKeyName(1, "Backup(16_13).PNG");
            ImageList1.Images.SetKeyName(2, "Restore(16_16).PNG");
            // 
            // cmdRestore
            // 
            _cmdRestore.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdRestore.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdRestore.ImageIndex = 2;
            _cmdRestore.ImageList = ImageList1;
            _cmdRestore.Location = new Point(274, 22);
            _cmdRestore.Name = "_cmdRestore";
            _cmdRestore.RightToLeft = RightToLeft.No;
            _cmdRestore.Size = new Size(112, 26);
            _cmdRestore.TabIndex = 11;
            _cmdRestore.Text = "جايگذاري";
            _cmdRestore.TextAlign = ContentAlignment.MiddleRight;
            _cmdRestore.UseVisualStyleBackColor = true;
            _cmdRestore.Visible = false;
            // 
            // cmdBackup
            // 
            _cmdBackup.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdBackup.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdBackup.ImageIndex = 1;
            _cmdBackup.ImageList = ImageList1;
            _cmdBackup.Location = new Point(274, 22);
            _cmdBackup.Name = "_cmdBackup";
            _cmdBackup.RightToLeft = RightToLeft.No;
            _cmdBackup.Size = new Size(112, 26);
            _cmdBackup.TabIndex = 9;
            _cmdBackup.Text = "پشتيبان";
            _cmdBackup.TextAlign = ContentAlignment.MiddleRight;
            _cmdBackup.UseVisualStyleBackColor = true;
            _cmdBackup.Visible = false;
            // 
            // Label2
            // 
            Label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label2.Font = new Font("Tahoma", 11.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(0));
            Label2.ForeColor = Color.Red;
            Label2.Location = new Point(487, -1);
            Label2.Name = "Label2";
            Label2.Size = new Size(57, 23);
            Label2.TabIndex = 21;
            Label2.Text = "توجه !!";
            Label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label3
            // 
            Label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label3.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(0));
            Label3.ForeColor = Color.DarkRed;
            Label3.Location = new Point(12, 23);
            Label3.Name = "Label3";
            Label3.Size = new Size(519, 20);
            Label3.TabIndex = 22;
            Label3.Text = "1- عملیات ایجاد یا جایگذاری پشتیبان، باید بر روی سرور انجام گیرد.";
            Label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label4
            // 
            Label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label4.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(0));
            Label4.ForeColor = Color.DarkRed;
            Label4.Location = new Point(12, 47);
            Label4.Name = "Label4";
            Label4.Size = new Size(519, 34);
            Label4.TabIndex = 23;
            Label4.Text = "2- قبل از انجام عملیات ایجاد یا جایگذاری پشتیبان، مطمئن شوید هیچ یک از کاربران از" + " برنامه استفاده نمی کنند.";
            Label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // frmBackup
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _cmdCancel;
            ClientSize = new Size(553, 298);
            Controls.Add(Label4);
            Controls.Add(Label3);
            Controls.Add(Label2);
            Controls.Add(GroupBox2);
            Controls.Add(GroupBox1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(559, 330);
            Name = "frmBackup";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            GroupBox1.ResumeLayout(false);
            GroupBox1.PerformLayout();
            GroupBox2.ResumeLayout(false);
            Load += new EventHandler(frmBackup_Load);
            FormClosing += new FormClosingEventHandler(frmBackup_FormClosing);
            ResumeLayout(false);
        }

        internal GroupBox GroupBox1;
        internal GroupBox GroupBox2;
        private System.Windows.Forms.Button _cmdBackup;

        internal System.Windows.Forms.Button cmdBackup
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdBackup;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdBackup != null)
                {
                    _cmdBackup.Click -= cmdBackup_Click;
                }

                _cmdBackup = value;
                if (_cmdBackup != null)
                {
                    _cmdBackup.Click += cmdBackup_Click;
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

        private System.Windows.Forms.Button _cmdRestore;

        internal System.Windows.Forms.Button cmdRestore
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdRestore;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdRestore != null)
                {
                    _cmdRestore.Click -= cmdRestore_Click;
                }

                _cmdRestore = value;
                if (_cmdRestore != null)
                {
                    _cmdRestore.Click += cmdRestore_Click;
                }
            }
        }

        internal Label Label2;
        internal Label Label3;
        internal Label Label4;
        internal System.Windows.Forms.ImageList ImageList1;
        internal Label lblBackupFileName;
        private TextBox _txtBackupFileName;

        internal TextBox txtBackupFileName
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtBackupFileName;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtBackupFileName != null)
                {
                    _txtBackupFileName.KeyPress -= txtBackupFileName_KeyPress;
                }

                _txtBackupFileName = value;
                if (_txtBackupFileName != null)
                {
                    _txtBackupFileName.KeyPress += txtBackupFileName_KeyPress;
                }
            }
        }

        internal Label lblUMBackupFileName;
        private TextBox _txtUMBackupFileName;

        internal TextBox txtUMBackupFileName
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtUMBackupFileName;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtUMBackupFileName != null)
                {
                    _txtUMBackupFileName.KeyPress -= txtBackupFileName_KeyPress;
                }

                _txtUMBackupFileName = value;
                if (_txtUMBackupFileName != null)
                {
                    _txtUMBackupFileName.KeyPress += txtBackupFileName_KeyPress;
                }
            }
        }

        private CheckBox _chkUMBackup;

        internal CheckBox chkUMBackup
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _chkUMBackup;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_chkUMBackup != null)
                {
                    _chkUMBackup.CheckedChanged -= chkUMBackup_CheckedChanged;
                }

                _chkUMBackup = value;
                if (_chkUMBackup != null)
                {
                    _chkUMBackup.CheckedChanged += chkUMBackup_CheckedChanged;
                }
            }
        }

        private System.Windows.Forms.Button _cmdBrowser;

        internal System.Windows.Forms.Button cmdBrowser
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdBrowser;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdBrowser != null)
                {
                    _cmdBrowser.Click -= cmdBrowser_Click;
                }

                _cmdBrowser = value;
                if (_cmdBrowser != null)
                {
                    _cmdBrowser.Click += cmdBrowser_Click;
                }
            }
        }

        private System.Windows.Forms.Button _cmdUMBrowser;

        internal System.Windows.Forms.Button cmdUMBrowser
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdUMBrowser;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdUMBrowser != null)
                {
                    _cmdUMBrowser.Click -= cmdUMBrowser_Click;
                }

                _cmdUMBrowser = value;
                if (_cmdUMBrowser != null)
                {
                    _cmdUMBrowser.Click += cmdUMBrowser_Click;
                }
            }
        }

        private CheckBox _chkBackup;

        internal CheckBox chkBackup
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _chkBackup;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_chkBackup != null)
                {
                    _chkBackup.CheckedChanged -= chkBackup_CheckedChanged;
                }

                _chkBackup = value;
                if (_chkBackup != null)
                {
                    _chkBackup.CheckedChanged += chkBackup_CheckedChanged;
                }
            }
        }
    }
}