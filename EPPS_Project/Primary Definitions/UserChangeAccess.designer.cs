using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class UserChangeAccess : Form
    {
        public UserChangeAccess(DataRow row, byte OP) : base()
        {
            InitializeComponent();
            // selection = "edit"
            txtUID.Text = Conversions.ToString(row[0]);
            txtUName.Text = Conversions.ToString(row[2]);
            txtUPass.Text = Conversions.ToString(row[3]);
            txtConfirm.Text = Conversions.ToString(row[3]);
            // txtUOccup.Text = row(3)
            // txtaccess.Text = row("accessctrl")
            OldUserCode = Conversions.ToInteger(row[0]);
            OPType = OP;
            _butRegister.Name = "butRegister";
            _txtUPass.Name = "txtUPass";
            _txtUName.Name = "txtUName";
            _txtUID.Name = "txtUID";
            _TreeView1.Name = "TreeView1";
        }

        public UserChangeAccess(byte OP) : base()
        {
            InitializeComponent();
            // selection = "new"
            OPType = OP;
            _butRegister.Name = "butRegister";
            _txtUPass.Name = "txtUPass";
            _txtUName.Name = "txtUName";
            _txtUID.Name = "txtUID";
            _TreeView1.Name = "TreeView1";
        }

        public byte OPType;

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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(UserChangeAccess));
            GroupBox1 = new GroupBox();
            butCancel = new System.Windows.Forms.Button();
            txtConfirm = new TextBox();
            _butRegister = new System.Windows.Forms.Button();
            _butRegister.Click += new EventHandler(butRegister_Click);
            _txtUPass = new TextBox();
            _txtUPass.KeyPress += new KeyPressEventHandler(txtpass_KeyPress);
            Label5 = new Label();
            _txtUName = new TextBox();
            _txtUName.KeyPress += new KeyPressEventHandler(txtname_KeyPress);
            Label4 = new Label();
            Label2 = new Label();
            _txtUID = new TextBox();
            _txtUID.KeyPress += new KeyPressEventHandler(txtcode_KeyPress);
            _txtUID.Leave += new EventHandler(txtcode_Leave);
            Label1 = new Label();
            GroupBox2 = new GroupBox();
            _TreeView1 = new System.Windows.Forms.TreeView();
            _TreeView1.AfterCheck += new TreeViewEventHandler(TreeView1_AfterCheck1);
            _TreeView1.NodeMouseClick += new TreeNodeMouseClickEventHandler(TreeView1_NodeMouseClick1);
            GroupBox1.SuspendLayout();
            GroupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // GroupBox1
            // 
            GroupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            GroupBox1.Controls.Add(butCancel);
            GroupBox1.Controls.Add(txtConfirm);
            GroupBox1.Controls.Add(_butRegister);
            GroupBox1.Controls.Add(_txtUPass);
            GroupBox1.Controls.Add(Label5);
            GroupBox1.Controls.Add(_txtUName);
            GroupBox1.Controls.Add(Label4);
            GroupBox1.Controls.Add(Label2);
            GroupBox1.Controls.Add(_txtUID);
            GroupBox1.Controls.Add(Label1);
            GroupBox1.Location = new Point(613, 13);
            GroupBox1.Margin = new Padding(3, 4, 3, 4);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Padding = new Padding(3, 4, 3, 4);
            GroupBox1.Size = new Size(271, 411);
            GroupBox1.TabIndex = 0;
            GroupBox1.TabStop = false;
            GroupBox1.Text = "مشخصات کاربر";
            // 
            // butCancel
            // 
            butCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            butCancel.DialogResult = DialogResult.Cancel;
            butCancel.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            butCancel.ForeColor = Color.Red;
            butCancel.Image = (Image)resources.GetObject("butCancel.Image");
            butCancel.ImageAlign = ContentAlignment.MiddleRight;
            butCancel.Location = new Point(19, 352);
            butCancel.Margin = new Padding(3, 4, 3, 4);
            butCancel.Name = "butCancel";
            butCancel.Size = new Size(112, 31);
            butCancel.TabIndex = 6;
            butCancel.Text = "انصراف";
            butCancel.TextAlign = ContentAlignment.MiddleLeft;
            butCancel.UseVisualStyleBackColor = true;
            // 
            // txtConfirm
            // 
            txtConfirm.BackColor = Color.PapayaWhip;
            txtConfirm.Location = new Point(6, 161);
            txtConfirm.Margin = new Padding(3, 4, 3, 4);
            txtConfirm.Name = "txtConfirm";
            txtConfirm.Size = new Size(167, 23);
            txtConfirm.TabIndex = 3;
            txtConfirm.TextAlign = HorizontalAlignment.Center;
            txtConfirm.UseSystemPasswordChar = true;
            // 
            // butRegister
            // 
            _butRegister.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _butRegister.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _butRegister.ForeColor = Color.Black;
            _butRegister.Image = (Image)resources.GetObject("butRegister.Image");
            _butRegister.ImageAlign = ContentAlignment.MiddleRight;
            _butRegister.Location = new Point(150, 352);
            _butRegister.Margin = new Padding(3, 4, 3, 4);
            _butRegister.Name = "_butRegister";
            _butRegister.Size = new Size(112, 31);
            _butRegister.TabIndex = 5;
            _butRegister.Text = "ثبت";
            _butRegister.TextAlign = ContentAlignment.MiddleLeft;
            _butRegister.UseVisualStyleBackColor = true;
            // 
            // txtUPass
            // 
            _txtUPass.BackColor = Color.PapayaWhip;
            _txtUPass.Location = new Point(6, 124);
            _txtUPass.Margin = new Padding(3, 4, 3, 4);
            _txtUPass.Name = "_txtUPass";
            _txtUPass.Size = new Size(167, 23);
            _txtUPass.TabIndex = 2;
            _txtUPass.TextAlign = HorizontalAlignment.Center;
            _txtUPass.UseSystemPasswordChar = true;
            // 
            // Label5
            // 
            Label5.AutoSize = true;
            Label5.Location = new Point(179, 164);
            Label5.Name = "Label5";
            Label5.Size = new Size(86, 16);
            Label5.TabIndex = 18;
            Label5.Text = "تکرار رمز عبور:";
            // 
            // txtUName
            // 
            _txtUName.BackColor = Color.PapayaWhip;
            _txtUName.Location = new Point(6, 85);
            _txtUName.Margin = new Padding(3, 4, 3, 4);
            _txtUName.Name = "_txtUName";
            _txtUName.Size = new Size(167, 23);
            _txtUName.TabIndex = 1;
            _txtUName.TextAlign = HorizontalAlignment.Center;
            // 
            // Label4
            // 
            Label4.AutoSize = true;
            Label4.Location = new Point(207, 127);
            Label4.Name = "Label4";
            Label4.Size = new Size(58, 16);
            Label4.TabIndex = 15;
            Label4.Text = "رمز عبور:";
            // 
            // Label2
            // 
            Label2.AutoSize = true;
            Label2.Location = new Point(208, 88);
            Label2.Name = "Label2";
            Label2.Size = new Size(57, 16);
            Label2.TabIndex = 14;
            Label2.Text = "نام کاربر:";
            // 
            // txtUID
            // 
            _txtUID.BackColor = Color.PapayaWhip;
            _txtUID.Location = new Point(72, 48);
            _txtUID.Margin = new Padding(3, 4, 3, 4);
            _txtUID.Name = "_txtUID";
            _txtUID.ReadOnly = true;
            _txtUID.Size = new Size(100, 23);
            _txtUID.TabIndex = 0;
            _txtUID.TabStop = false;
            _txtUID.TextAlign = HorizontalAlignment.Center;
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.Location = new Point(211, 51);
            Label1.Name = "Label1";
            Label1.Size = new Size(54, 16);
            Label1.TabIndex = 16;
            Label1.Text = "کد کاربر:";
            // 
            // GroupBox2
            // 
            GroupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            GroupBox2.Controls.Add(_TreeView1);
            GroupBox2.Location = new Point(12, 13);
            GroupBox2.Margin = new Padding(3, 4, 3, 4);
            GroupBox2.Name = "GroupBox2";
            GroupBox2.Padding = new Padding(3, 4, 3, 4);
            GroupBox2.Size = new Size(595, 411);
            GroupBox2.TabIndex = 1;
            GroupBox2.TabStop = false;
            GroupBox2.Text = "دسترسی های کاربر";
            // 
            // TreeView1
            // 
            _TreeView1.CheckBoxes = true;
            _TreeView1.Dock = DockStyle.Fill;
            _TreeView1.Location = new Point(3, 20);
            _TreeView1.Margin = new Padding(3, 4, 3, 4);
            _TreeView1.Name = "_TreeView1";
            _TreeView1.RightToLeftLayout = true;
            _TreeView1.Size = new Size(589, 387);
            _TreeView1.TabIndex = 4;
            // 
            // UserChangeAccess
            // 
            AutoScaleDimensions = new SizeF(7.0f, 16.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = butCancel;
            ClientSize = new Size(896, 437);
            Controls.Add(GroupBox2);
            Controls.Add(GroupBox1);
            Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(0));
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            Name = "UserChangeAccess";
            RightToLeft = RightToLeft.Yes;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " مشخصات کاربر";
            GroupBox1.ResumeLayout(false);
            GroupBox1.PerformLayout();
            GroupBox2.ResumeLayout(false);
            Disposed += new EventHandler(UserChange_Disposed);
            Load += new EventHandler(UserChange_Load);
            ResumeLayout(false);
        }

        internal GroupBox GroupBox1;
        internal TextBox txtConfirm;
        private TextBox _txtUPass;

        internal TextBox txtUPass
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtUPass;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtUPass != null)
                {
                    _txtUPass.KeyPress -= txtpass_KeyPress;
                }

                _txtUPass = value;
                if (_txtUPass != null)
                {
                    _txtUPass.KeyPress += txtpass_KeyPress;
                }
            }
        }

        internal Label Label5;
        private TextBox _txtUName;

        internal TextBox txtUName
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtUName;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtUName != null)
                {
                    _txtUName.KeyPress -= txtname_KeyPress;
                }

                _txtUName = value;
                if (_txtUName != null)
                {
                    _txtUName.KeyPress += txtname_KeyPress;
                }
            }
        }

        internal Label Label4;
        internal Label Label2;
        private TextBox _txtUID;

        internal TextBox txtUID
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtUID;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtUID != null)
                {
                    _txtUID.KeyPress -= txtcode_KeyPress;
                    _txtUID.Leave -= txtcode_Leave;
                }

                _txtUID = value;
                if (_txtUID != null)
                {
                    _txtUID.KeyPress += txtcode_KeyPress;
                    _txtUID.Leave += txtcode_Leave;
                }
            }
        }

        internal Label Label1;
        internal GroupBox GroupBox2;
        private System.Windows.Forms.Button _butRegister;

        internal System.Windows.Forms.Button butRegister
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _butRegister;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_butRegister != null)
                {
                    _butRegister.Click -= butRegister_Click;
                }

                _butRegister = value;
                if (_butRegister != null)
                {
                    _butRegister.Click += butRegister_Click;
                }
            }
        }

        internal System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.TreeView _TreeView1;

        internal System.Windows.Forms.TreeView TreeView1
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _TreeView1;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_TreeView1 != null)
                {
                    _TreeView1.AfterCheck -= TreeView1_AfterCheck1;
                    _TreeView1.NodeMouseClick -= TreeView1_NodeMouseClick1;
                }

                _TreeView1 = value;
                if (_TreeView1 != null)
                {
                    _TreeView1.AfterCheck += TreeView1_AfterCheck1;
                    _TreeView1.NodeMouseClick += TreeView1_NodeMouseClick1;
                }
            }
        }
    }
}