using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmOperator : Form
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
            var DataGridViewCellStyle2 = new DataGridViewCellStyle();
            _cmdSave = new System.Windows.Forms.Button();
            _cmdSave.Click += new EventHandler(cmdSave_Click);
            _cmdExit = new System.Windows.Forms.Button();
            _cmdExit.Click += new EventHandler(cmdExit_Click);
            Label1 = new Label();
            _txtPersonnelCode = new TextBox();
            _txtPersonnelCode.KeyPress += new KeyPressEventHandler(Controls_KeyPress);
            lblName = new Label();
            _txtName = new TextBox();
            _txtName.KeyPress += new KeyPressEventHandler(Controls_KeyPress);
            Panel1 = new System.Windows.Forms.Panel();
            _cmdDelete = new System.Windows.Forms.Button();
            _cmdDelete.Click += new EventHandler(cmdDelete_Click);
            _cbProductionPart = new ComboBox();
            _cbProductionPart.KeyPress += new KeyPressEventHandler(Controls_KeyPress);
            Label2 = new Label();
            GroupBox2 = new GroupBox();
            _cmdDeleteWorkPeriod = new System.Windows.Forms.Button();
            _cmdDeleteWorkPeriod.Click += new EventHandler(cmdAddWorkPeriod_Click);
            _cmdEditWorkPeriod = new System.Windows.Forms.Button();
            _cmdEditWorkPeriod.Click += new EventHandler(cmdAddWorkPeriod_Click);
            _cmdAddWorkPeriod = new System.Windows.Forms.Button();
            _cmdAddWorkPeriod.Click += new EventHandler(cmdAddWorkPeriod_Click);
            _dgWorkPeriods = new DataGridView();
            _dgWorkPeriods.CellFormatting += new DataGridViewCellFormattingEventHandler(dgWorkPeriods_CellFormatting);
            colStartDate = new DataGridViewTextBoxColumn();
            colEndDate = new DataGridViewTextBoxColumn();
            colDescription = new DataGridViewTextBoxColumn();
            Panel1.SuspendLayout();
            GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgWorkPeriods).BeginInit();
            SuspendLayout();
            // 
            // cmdSave
            // 
            _cmdSave.BackColor = Color.Transparent;
            _cmdSave.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdSave.ForeColor = Color.Black;
            _cmdSave.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdSave.Location = new Point(225, 10);
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
            _cmdExit.Location = new Point(105, 10);
            _cmdExit.Name = "_cmdExit";
            _cmdExit.RightToLeft = RightToLeft.No;
            _cmdExit.Size = new Size(90, 28);
            _cmdExit.TabIndex = 5;
            _cmdExit.Text = "انصراف";
            _cmdExit.TextAlign = ContentAlignment.MiddleRight;
            _cmdExit.UseVisualStyleBackColor = false;
            // 
            // Label1
            // 
            Label1.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label1.Location = new Point(328, 21);
            Label1.Name = "Label1";
            Label1.RightToLeft = RightToLeft.Yes;
            Label1.Size = new Size(69, 17);
            Label1.TabIndex = 94;
            Label1.Text = "کد پرسنلی:";
            Label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtPersonnelCode
            // 
            _txtPersonnelCode.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _txtPersonnelCode.Location = new Point(184, 21);
            _txtPersonnelCode.Name = "_txtPersonnelCode";
            _txtPersonnelCode.Size = new Size(141, 21);
            _txtPersonnelCode.TabIndex = 88;
            // 
            // lblName
            // 
            lblName.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblName.Location = new Point(328, 58);
            lblName.Name = "lblName";
            lblName.RightToLeft = RightToLeft.Yes;
            lblName.Size = new Size(64, 17);
            lblName.TabIndex = 93;
            lblName.Text = "نام:";
            lblName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            _txtName.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _txtName.Location = new Point(17, 58);
            _txtName.Name = "_txtName";
            _txtName.Size = new Size(308, 21);
            _txtName.TabIndex = 89;
            // 
            // Panel1
            // 
            Panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Panel1.BackColor = SystemColors.Control;
            Panel1.BorderStyle = BorderStyle.FixedSingle;
            Panel1.Controls.Add(_cmdSave);
            Panel1.Controls.Add(_cmdExit);
            Panel1.Controls.Add(_cmdDelete);
            Panel1.Location = new Point(9, 312);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(423, 48);
            Panel1.TabIndex = 92;
            // 
            // cmdDelete
            // 
            _cmdDelete.BackColor = Color.Transparent;
            _cmdDelete.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdDelete.ForeColor = Color.Black;
            _cmdDelete.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdDelete.Location = new Point(225, 10);
            _cmdDelete.Name = "_cmdDelete";
            _cmdDelete.RightToLeft = RightToLeft.No;
            _cmdDelete.Size = new Size(90, 28);
            _cmdDelete.TabIndex = 4;
            _cmdDelete.Text = "حذف";
            _cmdDelete.TextAlign = ContentAlignment.MiddleRight;
            _cmdDelete.UseVisualStyleBackColor = false;
            _cmdDelete.Visible = false;
            // 
            // cbProductionPart
            // 
            _cbProductionPart.DropDownStyle = ComboBoxStyle.DropDownList;
            _cbProductionPart.FlatStyle = FlatStyle.Flat;
            _cbProductionPart.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cbProductionPart.FormattingEnabled = true;
            _cbProductionPart.Items.AddRange(new object[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" });
            _cbProductionPart.Location = new Point(17, 95);
            _cbProductionPart.Name = "_cbProductionPart";
            _cbProductionPart.Size = new Size(308, 21);
            _cbProductionPart.TabIndex = 279;
            // 
            // Label2
            // 
            Label2.BackColor = SystemColors.Control;
            Label2.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label2.ForeColor = Color.Black;
            Label2.Location = new Point(328, 96);
            Label2.Name = "Label2";
            Label2.RightToLeft = RightToLeft.Yes;
            Label2.Size = new Size(65, 17);
            Label2.TabIndex = 278;
            Label2.Text = "بخش تولید:";
            Label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // GroupBox2
            // 
            GroupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            GroupBox2.Controls.Add(_cmdDeleteWorkPeriod);
            GroupBox2.Controls.Add(_cmdEditWorkPeriod);
            GroupBox2.Controls.Add(_cmdAddWorkPeriod);
            GroupBox2.Controls.Add(_dgWorkPeriods);
            GroupBox2.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            GroupBox2.Location = new Point(9, 127);
            GroupBox2.Name = "GroupBox2";
            GroupBox2.Size = new Size(423, 179);
            GroupBox2.TabIndex = 280;
            GroupBox2.TabStop = false;
            GroupBox2.Text = "وضعیت شغلی(در حال کار)";
            // 
            // cmdDeleteWorkPeriod
            // 
            _cmdDeleteWorkPeriod.Location = new Point(70, 12);
            _cmdDeleteWorkPeriod.Name = "_cmdDeleteWorkPeriod";
            _cmdDeleteWorkPeriod.Size = new Size(25, 25);
            _cmdDeleteWorkPeriod.TabIndex = 221;
            _cmdDeleteWorkPeriod.UseVisualStyleBackColor = true;
            // 
            // cmdEditWorkPeriod
            // 
            _cmdEditWorkPeriod.Location = new Point(39, 12);
            _cmdEditWorkPeriod.Name = "_cmdEditWorkPeriod";
            _cmdEditWorkPeriod.Size = new Size(25, 25);
            _cmdEditWorkPeriod.TabIndex = 220;
            _cmdEditWorkPeriod.UseVisualStyleBackColor = true;
            // 
            // cmdAddWorkPeriod
            // 
            _cmdAddWorkPeriod.Location = new Point(8, 12);
            _cmdAddWorkPeriod.Name = "_cmdAddWorkPeriod";
            _cmdAddWorkPeriod.Size = new Size(25, 25);
            _cmdAddWorkPeriod.TabIndex = 219;
            _cmdAddWorkPeriod.UseVisualStyleBackColor = true;
            // 
            // dgWorkPeriods
            // 
            _dgWorkPeriods.AllowUserToAddRows = false;
            _dgWorkPeriods.AllowUserToDeleteRows = false;
            DataGridViewCellStyle2.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(224)), Conversions.ToInteger(Conversions.ToByte(192)));
            _dgWorkPeriods.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2;
            _dgWorkPeriods.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            _dgWorkPeriods.BackgroundColor = Color.WhiteSmoke;
            _dgWorkPeriods.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _dgWorkPeriods.Columns.AddRange(new DataGridViewColumn[] { colStartDate, colEndDate, colDescription });
            _dgWorkPeriods.Location = new Point(6, 43);
            _dgWorkPeriods.Name = "_dgWorkPeriods";
            _dgWorkPeriods.ReadOnly = true;
            _dgWorkPeriods.RowHeadersVisible = false;
            _dgWorkPeriods.RowHeadersWidth = 23;
            _dgWorkPeriods.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            _dgWorkPeriods.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgWorkPeriods.Size = new Size(411, 130);
            _dgWorkPeriods.TabIndex = 218;
            // 
            // colStartDate
            // 
            colStartDate.HeaderText = "تاریخ شروع کار";
            colStartDate.Name = "colStartDate";
            colStartDate.ReadOnly = true;
            colStartDate.Resizable = DataGridViewTriState.False;
            colStartDate.SortMode = DataGridViewColumnSortMode.NotSortable;
            colStartDate.Width = 95;
            // 
            // colEndDate
            // 
            colEndDate.HeaderText = "تاریخ پایان کار";
            colEndDate.Name = "colEndDate";
            colEndDate.ReadOnly = true;
            colEndDate.Resizable = DataGridViewTriState.False;
            colEndDate.SortMode = DataGridViewColumnSortMode.NotSortable;
            colEndDate.Width = 90;
            // 
            // colDescription
            // 
            colDescription.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDescription.HeaderText = "توضیحات";
            colDescription.Name = "colDescription";
            colDescription.ReadOnly = true;
            colDescription.Resizable = DataGridViewTriState.False;
            colDescription.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // frmOperator
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _cmdExit;
            ClientSize = new Size(441, 367);
            Controls.Add(GroupBox2);
            Controls.Add(_cbProductionPart);
            Controls.Add(Label2);
            Controls.Add(Label1);
            Controls.Add(_txtPersonnelCode);
            Controls.Add(lblName);
            Controls.Add(_txtName);
            Controls.Add(Panel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmOperator";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " مشخصات اپراتور";
            Panel1.ResumeLayout(false);
            GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_dgWorkPeriods).EndInit();
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

        internal Label Label1;
        private TextBox _txtPersonnelCode;

        internal TextBox txtPersonnelCode
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtPersonnelCode;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtPersonnelCode != null)
                {
                    _txtPersonnelCode.KeyPress -= Controls_KeyPress;
                }

                _txtPersonnelCode = value;
                if (_txtPersonnelCode != null)
                {
                    _txtPersonnelCode.KeyPress += Controls_KeyPress;
                }
            }
        }

        internal Label lblName;
        private TextBox _txtName;

        internal TextBox txtName
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtName;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtName != null)
                {
                    _txtName.KeyPress -= Controls_KeyPress;
                }

                _txtName = value;
                if (_txtName != null)
                {
                    _txtName.KeyPress += Controls_KeyPress;
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

        private ComboBox _cbProductionPart;

        internal ComboBox cbProductionPart
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cbProductionPart;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cbProductionPart != null)
                {
                    _cbProductionPart.KeyPress -= Controls_KeyPress;
                }

                _cbProductionPart = value;
                if (_cbProductionPart != null)
                {
                    _cbProductionPart.KeyPress += Controls_KeyPress;
                }
            }
        }

        internal Label Label2;
        internal GroupBox GroupBox2;
        private DataGridView _dgWorkPeriods;

        internal DataGridView dgWorkPeriods
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _dgWorkPeriods;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_dgWorkPeriods != null)
                {
                    _dgWorkPeriods.CellFormatting -= dgWorkPeriods_CellFormatting;
                }

                _dgWorkPeriods = value;
                if (_dgWorkPeriods != null)
                {
                    _dgWorkPeriods.CellFormatting += dgWorkPeriods_CellFormatting;
                }
            }
        }

        internal DataGridViewTextBoxColumn colStartDate;
        internal DataGridViewTextBoxColumn colEndDate;
        internal DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.Button _cmdDeleteWorkPeriod;

        internal System.Windows.Forms.Button cmdDeleteWorkPeriod
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdDeleteWorkPeriod;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdDeleteWorkPeriod != null)
                {
                    _cmdDeleteWorkPeriod.Click -= cmdAddWorkPeriod_Click;
                }

                _cmdDeleteWorkPeriod = value;
                if (_cmdDeleteWorkPeriod != null)
                {
                    _cmdDeleteWorkPeriod.Click += cmdAddWorkPeriod_Click;
                }
            }
        }

        private System.Windows.Forms.Button _cmdEditWorkPeriod;

        internal System.Windows.Forms.Button cmdEditWorkPeriod
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdEditWorkPeriod;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdEditWorkPeriod != null)
                {
                    _cmdEditWorkPeriod.Click -= cmdAddWorkPeriod_Click;
                }

                _cmdEditWorkPeriod = value;
                if (_cmdEditWorkPeriod != null)
                {
                    _cmdEditWorkPeriod.Click += cmdAddWorkPeriod_Click;
                }
            }
        }

        private System.Windows.Forms.Button _cmdAddWorkPeriod;

        internal System.Windows.Forms.Button cmdAddWorkPeriod
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdAddWorkPeriod;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdAddWorkPeriod != null)
                {
                    _cmdAddWorkPeriod.Click -= cmdAddWorkPeriod_Click;
                }

                _cmdAddWorkPeriod = value;
                if (_cmdAddWorkPeriod != null)
                {
                    _cmdAddWorkPeriod.Click += cmdAddWorkPeriod_Click;
                }
            }
        }
    }
}