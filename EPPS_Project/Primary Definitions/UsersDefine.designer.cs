using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class UsersDefine : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(UsersDefine));
            var DataGridViewCellStyle1 = new DataGridViewCellStyle();
            _butExit = new System.Windows.Forms.Button();
            _butExit.Click += new EventHandler(butExit_Click);
            _butDel = new System.Windows.Forms.Button();
            _butDel.Click += new EventHandler(butDel_Click);
            _butEdit = new System.Windows.Forms.Button();
            _butEdit.Click += new EventHandler(butEdit_Click);
            _butNew = new System.Windows.Forms.Button();
            _butNew.Click += new EventHandler(butNew_Click);
            Label4 = new Label();
            _DataGridView1 = new DataGridView();
            _DataGridView1.CellClick += new DataGridViewCellEventHandler(DataGridView1_CellClick);
            GroupBox1 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)_DataGridView1).BeginInit();
            GroupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // butExit
            // 
            _butExit.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _butExit.Image = (Image)resources.GetObject("butExit.Image");
            _butExit.ImageAlign = ContentAlignment.MiddleRight;
            _butExit.Location = new Point(14, 465);
            _butExit.Margin = new Padding(3, 4, 3, 4);
            _butExit.Name = "_butExit";
            _butExit.Padding = new Padding(3, 0, 0, 0);
            _butExit.Size = new Size(86, 29);
            _butExit.TabIndex = 4;
            _butExit.Text = "خروج";
            _butExit.TextAlign = ContentAlignment.MiddleLeft;
            _butExit.UseVisualStyleBackColor = true;
            // 
            // butDel
            // 
            _butDel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _butDel.Image = (Image)resources.GetObject("butDel.Image");
            _butDel.ImageAlign = ContentAlignment.MiddleRight;
            _butDel.Location = new Point(153, 465);
            _butDel.Margin = new Padding(3, 4, 3, 4);
            _butDel.Name = "_butDel";
            _butDel.Padding = new Padding(6, 0, 6, 0);
            _butDel.Size = new Size(86, 29);
            _butDel.TabIndex = 3;
            _butDel.Text = "حـذف";
            _butDel.TextAlign = ContentAlignment.MiddleLeft;
            _butDel.UseVisualStyleBackColor = true;
            _butDel.Visible = false;
            // 
            // butEdit
            // 
            _butEdit.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _butEdit.Image = (Image)resources.GetObject("butEdit.Image");
            _butEdit.ImageAlign = ContentAlignment.MiddleRight;
            _butEdit.Location = new Point(256, 465);
            _butEdit.Margin = new Padding(3, 4, 3, 4);
            _butEdit.Name = "_butEdit";
            _butEdit.Padding = new Padding(6, 0, 6, 0);
            _butEdit.Size = new Size(86, 29);
            _butEdit.TabIndex = 2;
            _butEdit.Text = "اصلاح";
            _butEdit.TextAlign = ContentAlignment.MiddleLeft;
            _butEdit.UseVisualStyleBackColor = true;
            // 
            // butNew
            // 
            _butNew.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _butNew.Image = (Image)resources.GetObject("butNew.Image");
            _butNew.ImageAlign = ContentAlignment.MiddleRight;
            _butNew.Location = new Point(361, 465);
            _butNew.Margin = new Padding(3, 4, 3, 4);
            _butNew.Name = "_butNew";
            _butNew.Padding = new Padding(6, 0, 6, 0);
            _butNew.Size = new Size(86, 29);
            _butNew.TabIndex = 1;
            _butNew.Text = "جـديـد";
            _butNew.TextAlign = ContentAlignment.MiddleLeft;
            _butNew.UseVisualStyleBackColor = true;
            _butNew.Visible = false;
            // 
            // Label4
            // 
            Label4.AutoSize = true;
            Label4.Location = new Point(220, 25);
            Label4.Name = "Label4";
            Label4.Size = new Size(0, 16);
            Label4.TabIndex = 28;
            // 
            // DataGridView1
            // 
            _DataGridView1.AllowUserToAddRows = false;
            _DataGridView1.AllowUserToDeleteRows = false;
            _DataGridView1.AllowUserToResizeRows = false;
            DataGridViewCellStyle1.BackColor = Color.White;
            _DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1;
            _DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _DataGridView1.BorderStyle = BorderStyle.Fixed3D;
            _DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _DataGridView1.Dock = DockStyle.Fill;
            _DataGridView1.Location = new Point(3, 19);
            _DataGridView1.Margin = new Padding(3, 4, 3, 4);
            _DataGridView1.MultiSelect = false;
            _DataGridView1.Name = "_DataGridView1";
            _DataGridView1.ReadOnly = true;
            _DataGridView1.RowHeadersVisible = false;
            _DataGridView1.RowHeadersWidth = 20;
            _DataGridView1.RowTemplate.Resizable = DataGridViewTriState.False;
            _DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _DataGridView1.Size = new Size(431, 414);
            _DataGridView1.TabIndex = 30;
            // 
            // GroupBox1
            // 
            GroupBox1.Controls.Add(_DataGridView1);
            GroupBox1.Location = new Point(12, 12);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(437, 436);
            GroupBox1.TabIndex = 31;
            GroupBox1.TabStop = false;
            GroupBox1.Text = " ليست کاربران نرم افزار";
            // 
            // UsersDefine
            // 
            AutoScaleDimensions = new SizeF(7.0f, 16.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(461, 507);
            Controls.Add(GroupBox1);
            Controls.Add(Label4);
            Controls.Add(_butExit);
            Controls.Add(_butDel);
            Controls.Add(_butEdit);
            Controls.Add(_butNew);
            Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(0));
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UsersDefine";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " لیست کاربران نرم افزار";
            ((System.ComponentModel.ISupportInitialize)_DataGridView1).EndInit();
            GroupBox1.ResumeLayout(false);
            Load += new EventHandler(Users_Load);
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Button _butExit;

        internal System.Windows.Forms.Button butExit
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _butExit;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_butExit != null)
                {
                    _butExit.Click -= butExit_Click;
                }

                _butExit = value;
                if (_butExit != null)
                {
                    _butExit.Click += butExit_Click;
                }
            }
        }

        private System.Windows.Forms.Button _butDel;

        internal System.Windows.Forms.Button butDel
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _butDel;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_butDel != null)
                {
                    _butDel.Click -= butDel_Click;
                }

                _butDel = value;
                if (_butDel != null)
                {
                    _butDel.Click += butDel_Click;
                }
            }
        }

        private System.Windows.Forms.Button _butEdit;

        internal System.Windows.Forms.Button butEdit
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _butEdit;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_butEdit != null)
                {
                    _butEdit.Click -= butEdit_Click;
                }

                _butEdit = value;
                if (_butEdit != null)
                {
                    _butEdit.Click += butEdit_Click;
                }
            }
        }

        private System.Windows.Forms.Button _butNew;

        internal System.Windows.Forms.Button butNew
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _butNew;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_butNew != null)
                {
                    _butNew.Click -= butNew_Click;
                }

                _butNew = value;
                if (_butNew != null)
                {
                    _butNew.Click += butNew_Click;
                }
            }
        }

        internal Label Label4;
        private DataGridView _DataGridView1;

        internal DataGridView DataGridView1
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _DataGridView1;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_DataGridView1 != null)
                {
                    _DataGridView1.CellClick -= DataGridView1_CellClick;
                }

                _DataGridView1 = value;
                if (_DataGridView1 != null)
                {
                    _DataGridView1.CellClick += DataGridView1_CellClick;
                }
            }
        }

        internal GroupBox GroupBox1;
    }
}