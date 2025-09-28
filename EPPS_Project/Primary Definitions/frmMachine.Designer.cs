using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmMachine : Form
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
            var DataGridViewCellStyle1 = new DataGridViewCellStyle();
            _cmdSave = new System.Windows.Forms.Button();
            _cmdSave.Click += new EventHandler(cmdSave_Click);
            _cmdExit = new System.Windows.Forms.Button();
            _cmdExit.Click += new EventHandler(cmdExit_Click);
            Panel1 = new System.Windows.Forms.Panel();
            _cmdDelete = new System.Windows.Forms.Button();
            _cmdDelete.Click += new EventHandler(cmdDelete_Click);
            GroupBox1 = new GroupBox();
            _cmdEditReplacement = new System.Windows.Forms.Button();
            _cmdEditReplacement.Click += new EventHandler(cmdAddReplacement_Click);
            _cmdRemoveReplacement = new System.Windows.Forms.Button();
            _cmdRemoveReplacement.Click += new EventHandler(cmdAddReplacement_Click);
            _cmdAddReplacement = new System.Windows.Forms.Button();
            _cmdAddReplacement.Click += new EventHandler(cmdAddReplacement_Click);
            dgReplacements = new DataGridView();
            Panel2 = new System.Windows.Forms.Panel();
            cbCalendar = new ComboBox();
            Label5 = new Label();
            txtProducerCountry = new TextBox();
            txtCode = new TextBox();
            Label2 = new Label();
            txtDescription = new TextBox();
            Label10 = new Label();
            txtApplication = new TextBox();
            Label9 = new Label();
            txtProducer = new TextBox();
            Label4 = new Label();
            txtName = new TextBox();
            Label3 = new Label();
            Label1 = new Label();
            colParentMachineCode = new DataGridViewTextBoxColumn();
            colReplacementMachineCode = new DataGridViewTextBoxColumn();
            colReplacementMachineName = new DataGridViewTextBoxColumn();
            colDescription = new DataGridViewTextBoxColumn();
            Panel1.SuspendLayout();
            GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgReplacements).BeginInit();
            Panel2.SuspendLayout();
            SuspendLayout();
            // 
            // cmdSave
            // 
            _cmdSave.BackColor = Color.Transparent;
            _cmdSave.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdSave.ForeColor = Color.Black;
            _cmdSave.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdSave.Location = new Point(300, 10);
            _cmdSave.Name = "_cmdSave";
            _cmdSave.RightToLeft = RightToLeft.No;
            _cmdSave.Size = new Size(90, 28);
            _cmdSave.TabIndex = 7;
            _cmdSave.Text = "ثبت";
            _cmdSave.TextAlign = ContentAlignment.MiddleRight;
            _cmdSave.UseVisualStyleBackColor = false;
            // 
            // cmdExit
            // 
            _cmdExit.BackColor = Color.Transparent;
            _cmdExit.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdExit.ForeColor = Color.Red;
            _cmdExit.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdExit.Location = new Point(181, 10);
            _cmdExit.Name = "_cmdExit";
            _cmdExit.RightToLeft = RightToLeft.No;
            _cmdExit.Size = new Size(90, 28);
            _cmdExit.TabIndex = 9;
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
            Panel1.Location = new Point(5, 382);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(572, 48);
            Panel1.TabIndex = 44;
            // 
            // cmdDelete
            // 
            _cmdDelete.BackColor = Color.Transparent;
            _cmdDelete.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdDelete.ForeColor = Color.Black;
            _cmdDelete.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdDelete.Location = new Point(300, 10);
            _cmdDelete.Name = "_cmdDelete";
            _cmdDelete.RightToLeft = RightToLeft.No;
            _cmdDelete.Size = new Size(90, 28);
            _cmdDelete.TabIndex = 8;
            _cmdDelete.Text = "حذف";
            _cmdDelete.TextAlign = ContentAlignment.MiddleRight;
            _cmdDelete.UseVisualStyleBackColor = false;
            _cmdDelete.Visible = false;
            // 
            // GroupBox1
            // 
            GroupBox1.Controls.Add(_cmdEditReplacement);
            GroupBox1.Controls.Add(_cmdRemoveReplacement);
            GroupBox1.Controls.Add(_cmdAddReplacement);
            GroupBox1.Controls.Add(dgReplacements);
            GroupBox1.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            GroupBox1.Location = new Point(6, 176);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(571, 198);
            GroupBox1.TabIndex = 71;
            GroupBox1.TabStop = false;
            GroupBox1.Text = "ماشین آلات جایگزین";
            // 
            // cmdEditReplacement
            // 
            _cmdEditReplacement.BackColor = Color.Transparent;
            _cmdEditReplacement.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdEditReplacement.ForeColor = Color.Black;
            _cmdEditReplacement.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdEditReplacement.Location = new Point(83, 19);
            _cmdEditReplacement.Name = "_cmdEditReplacement";
            _cmdEditReplacement.RightToLeft = RightToLeft.No;
            _cmdEditReplacement.Size = new Size(63, 24);
            _cmdEditReplacement.TabIndex = 11;
            _cmdEditReplacement.Text = "اصلاح";
            _cmdEditReplacement.TextAlign = ContentAlignment.MiddleRight;
            _cmdEditReplacement.UseVisualStyleBackColor = false;
            // 
            // cmdRemoveReplacement
            // 
            _cmdRemoveReplacement.BackColor = Color.Transparent;
            _cmdRemoveReplacement.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdRemoveReplacement.ForeColor = Color.Black;
            _cmdRemoveReplacement.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdRemoveReplacement.Location = new Point(14, 19);
            _cmdRemoveReplacement.Name = "_cmdRemoveReplacement";
            _cmdRemoveReplacement.RightToLeft = RightToLeft.No;
            _cmdRemoveReplacement.Size = new Size(63, 24);
            _cmdRemoveReplacement.TabIndex = 12;
            _cmdRemoveReplacement.Text = "حذف";
            _cmdRemoveReplacement.TextAlign = ContentAlignment.MiddleRight;
            _cmdRemoveReplacement.UseVisualStyleBackColor = false;
            // 
            // cmdAddReplacement
            // 
            _cmdAddReplacement.BackColor = Color.Transparent;
            _cmdAddReplacement.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdAddReplacement.ForeColor = Color.Black;
            _cmdAddReplacement.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdAddReplacement.Location = new Point(152, 19);
            _cmdAddReplacement.Name = "_cmdAddReplacement";
            _cmdAddReplacement.RightToLeft = RightToLeft.No;
            _cmdAddReplacement.Size = new Size(63, 24);
            _cmdAddReplacement.TabIndex = 10;
            _cmdAddReplacement.Text = "اضافه";
            _cmdAddReplacement.TextAlign = ContentAlignment.MiddleRight;
            _cmdAddReplacement.UseVisualStyleBackColor = false;
            // 
            // dgReplacements
            // 
            dgReplacements.AllowUserToAddRows = false;
            dgReplacements.AllowUserToDeleteRows = false;
            DataGridViewCellStyle1.BackColor = Color.White;
            dgReplacements.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1;
            dgReplacements.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgReplacements.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgReplacements.BackgroundColor = SystemColors.ControlDark;
            dgReplacements.ColumnHeadersHeight = 25;
            dgReplacements.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgReplacements.Columns.AddRange(new DataGridViewColumn[] { colParentMachineCode, colReplacementMachineCode, colReplacementMachineName, colDescription });
            dgReplacements.Location = new Point(9, 47);
            dgReplacements.Name = "dgReplacements";
            dgReplacements.ReadOnly = true;
            dgReplacements.RowHeadersWidth = 30;
            dgReplacements.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgReplacements.RowTemplate.DefaultCellStyle.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            dgReplacements.RowTemplate.DefaultCellStyle.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            dgReplacements.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgReplacements.Size = new Size(553, 145);
            dgReplacements.TabIndex = 13;
            dgReplacements.TabStop = false;
            // 
            // Panel2
            // 
            Panel2.Controls.Add(cbCalendar);
            Panel2.Controls.Add(Label5);
            Panel2.Controls.Add(txtProducerCountry);
            Panel2.Controls.Add(txtCode);
            Panel2.Controls.Add(Label2);
            Panel2.Controls.Add(txtDescription);
            Panel2.Controls.Add(Label10);
            Panel2.Controls.Add(txtApplication);
            Panel2.Controls.Add(Label9);
            Panel2.Controls.Add(txtProducer);
            Panel2.Controls.Add(Label4);
            Panel2.Controls.Add(txtName);
            Panel2.Controls.Add(Label3);
            Panel2.Controls.Add(Label1);
            Panel2.Location = new Point(6, 5);
            Panel2.Name = "Panel2";
            Panel2.Size = new Size(571, 168);
            Panel2.TabIndex = 72;
            // 
            // cbCalendar
            // 
            cbCalendar.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCalendar.FlatStyle = FlatStyle.Flat;
            cbCalendar.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cbCalendar.FormattingEnabled = true;
            cbCalendar.ItemHeight = 13;
            cbCalendar.Location = new Point(3, 74);
            cbCalendar.Name = "cbCalendar";
            cbCalendar.Size = new Size(226, 21);
            cbCalendar.TabIndex = 5;
            // 
            // Label5
            // 
            Label5.BackColor = SystemColors.Control;
            Label5.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label5.ForeColor = Color.Black;
            Label5.Location = new Point(274, 76);
            Label5.Name = "Label5";
            Label5.RightToLeft = RightToLeft.Yes;
            Label5.Size = new Size(33, 17);
            Label5.TabIndex = 86;
            Label5.Text = "تقویم";
            // 
            // txtProducerCountry
            // 
            txtProducerCountry.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtProducerCountry.Location = new Point(3, 42);
            txtProducerCountry.Name = "txtProducerCountry";
            txtProducerCountry.Size = new Size(226, 21);
            txtProducerCountry.TabIndex = 3;
            // 
            // txtCode
            // 
            txtCode.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtCode.Location = new Point(314, 10);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(190, 21);
            txtCode.TabIndex = 0;
            // 
            // Label2
            // 
            Label2.BackColor = SystemColors.Control;
            Label2.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label2.ForeColor = Color.Black;
            Label2.Location = new Point(504, 12);
            Label2.Name = "Label2";
            Label2.RightToLeft = RightToLeft.Yes;
            Label2.Size = new Size(64, 17);
            Label2.TabIndex = 82;
            Label2.Text = "کد ماشین";
            // 
            // txtDescription
            // 
            txtDescription.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtDescription.Location = new Point(2, 106);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(502, 52);
            txtDescription.TabIndex = 6;
            // 
            // Label10
            // 
            Label10.BackColor = SystemColors.Control;
            Label10.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label10.ForeColor = Color.Black;
            Label10.Location = new Point(504, 108);
            Label10.Name = "Label10";
            Label10.RightToLeft = RightToLeft.Yes;
            Label10.Size = new Size(64, 17);
            Label10.TabIndex = 80;
            Label10.Text = "توضیحات";
            // 
            // txtApplication
            // 
            txtApplication.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtApplication.Location = new Point(314, 74);
            txtApplication.Name = "txtApplication";
            txtApplication.Size = new Size(190, 21);
            txtApplication.TabIndex = 4;
            // 
            // Label9
            // 
            Label9.BackColor = SystemColors.Control;
            Label9.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label9.ForeColor = Color.Black;
            Label9.Location = new Point(504, 76);
            Label9.Name = "Label9";
            Label9.RightToLeft = RightToLeft.Yes;
            Label9.Size = new Size(64, 17);
            Label9.TabIndex = 78;
            Label9.Text = "موارد کاربرد";
            // 
            // txtProducer
            // 
            txtProducer.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtProducer.Location = new Point(314, 42);
            txtProducer.Name = "txtProducer";
            txtProducer.Size = new Size(190, 21);
            txtProducer.TabIndex = 2;
            // 
            // Label4
            // 
            Label4.BackColor = SystemColors.Control;
            Label4.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label4.ForeColor = Color.Black;
            Label4.Location = new Point(504, 44);
            Label4.Name = "Label4";
            Label4.RightToLeft = RightToLeft.Yes;
            Label4.Size = new Size(64, 17);
            Label4.TabIndex = 76;
            Label4.Text = "سازنده";
            // 
            // txtName
            // 
            txtName.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtName.Location = new Point(3, 10);
            txtName.Name = "txtName";
            txtName.Size = new Size(226, 21);
            txtName.TabIndex = 1;
            // 
            // Label3
            // 
            Label3.BackColor = SystemColors.Control;
            Label3.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label3.ForeColor = Color.Black;
            Label3.Location = new Point(246, 12);
            Label3.Name = "Label3";
            Label3.RightToLeft = RightToLeft.Yes;
            Label3.Size = new Size(61, 17);
            Label3.TabIndex = 74;
            Label3.Text = "نام ماشین";
            // 
            // Label1
            // 
            Label1.BackColor = SystemColors.Control;
            Label1.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label1.ForeColor = Color.Black;
            Label1.Location = new Point(232, 44);
            Label1.Name = "Label1";
            Label1.RightToLeft = RightToLeft.Yes;
            Label1.Size = new Size(75, 17);
            Label1.TabIndex = 73;
            Label1.Text = "کشور سازنده";
            // 
            // colParentMachineCode
            // 
            colParentMachineCode.HeaderText = "کد ماشین";
            colParentMachineCode.Name = "colParentMachineCode";
            colParentMachineCode.ReadOnly = true;
            colParentMachineCode.Visible = false;
            // 
            // colReplacementMachineCode
            // 
            colReplacementMachineCode.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colReplacementMachineCode.FillWeight = 101.5228f;
            colReplacementMachineCode.HeaderText = "کد ماشین جایگزین";
            colReplacementMachineCode.Name = "colReplacementMachineCode";
            colReplacementMachineCode.ReadOnly = true;
            colReplacementMachineCode.Resizable = DataGridViewTriState.False;
            colReplacementMachineCode.Width = 121;
            // 
            // colReplacementMachineName
            // 
            colReplacementMachineName.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colReplacementMachineName.FillWeight = 98.47716f;
            colReplacementMachineName.HeaderText = "نام ماشین جایگزین";
            colReplacementMachineName.Name = "colReplacementMachineName";
            colReplacementMachineName.ReadOnly = true;
            colReplacementMachineName.Resizable = DataGridViewTriState.False;
            colReplacementMachineName.Width = 400;
            // 
            // colDescription
            // 
            colDescription.HeaderText = "توضیحات";
            colDescription.MinimumWidth = 310;
            colDescription.Name = "colDescription";
            colDescription.ReadOnly = true;
            colDescription.Visible = false;
            // 
            // frmMachine
            // 
            AcceptButton = _cmdSave;
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(583, 436);
            Controls.Add(Panel2);
            Controls.Add(GroupBox1);
            Controls.Add(Panel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Location = new Point(50, 50);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmMachine";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " مشخصات ماشین آلات";
            Panel1.ResumeLayout(false);
            GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgReplacements).EndInit();
            Panel2.ResumeLayout(false);
            Panel2.PerformLayout();
            Load += new EventHandler(Form1_Load);
            FormClosing += new FormClosingEventHandler(frmMachine_FormClosing);
            ResumeLayout(false);
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

        internal GroupBox GroupBox1;
        private System.Windows.Forms.Button _cmdRemoveReplacement;

        internal System.Windows.Forms.Button cmdRemoveReplacement
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdRemoveReplacement;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdRemoveReplacement != null)
                {
                    _cmdRemoveReplacement.Click -= cmdAddReplacement_Click;
                }

                _cmdRemoveReplacement = value;
                if (_cmdRemoveReplacement != null)
                {
                    _cmdRemoveReplacement.Click += cmdAddReplacement_Click;
                }
            }
        }

        private System.Windows.Forms.Button _cmdAddReplacement;

        internal System.Windows.Forms.Button cmdAddReplacement
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdAddReplacement;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdAddReplacement != null)
                {
                    _cmdAddReplacement.Click -= cmdAddReplacement_Click;
                }

                _cmdAddReplacement = value;
                if (_cmdAddReplacement != null)
                {
                    _cmdAddReplacement.Click += cmdAddReplacement_Click;
                }
            }
        }

        internal DataGridView dgReplacements;
        private System.Windows.Forms.Button _cmdEditReplacement;

        internal System.Windows.Forms.Button cmdEditReplacement
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdEditReplacement;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdEditReplacement != null)
                {
                    _cmdEditReplacement.Click -= cmdAddReplacement_Click;
                }

                _cmdEditReplacement = value;
                if (_cmdEditReplacement != null)
                {
                    _cmdEditReplacement.Click += cmdAddReplacement_Click;
                }
            }
        }

        internal System.Windows.Forms.Panel Panel2;
        internal TextBox txtProducerCountry;
        internal TextBox txtCode;
        internal Label Label2;
        internal TextBox txtDescription;
        internal Label Label10;
        internal TextBox txtApplication;
        internal Label Label9;
        internal TextBox txtProducer;
        internal Label Label4;
        internal TextBox txtName;
        internal Label Label3;
        internal Label Label1;
        internal ComboBox cbCalendar;
        internal Label Label5;
        internal DataGridViewTextBoxColumn colParentMachineCode;
        internal DataGridViewTextBoxColumn colReplacementMachineCode;
        internal DataGridViewTextBoxColumn colReplacementMachineName;
        internal DataGridViewTextBoxColumn colDescription;
    }
}