using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmDoneBatch : Form
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
            var DataGridViewCellStyle2 = new DataGridViewCellStyle();
            var DataGridViewCellStyle5 = new DataGridViewCellStyle();
            var DataGridViewCellStyle3 = new DataGridViewCellStyle();
            var DataGridViewCellStyle4 = new DataGridViewCellStyle();
            dgBatch = new DataGridView();
            DataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            Label5 = new Label();
            lblMinDelivary = new Label();
            txtDoneQuantity = new NumericUpDown();
            Panel1 = new System.Windows.Forms.Panel();
            _cmdSave = new System.Windows.Forms.Button();
            _cmdSave.Click += new EventHandler(cmdSave_Click);
            _cmdExit = new System.Windows.Forms.Button();
            _cmdExit.Click += new EventHandler(cmdExit_Click);
            GroupBox1 = new GroupBox();
            _dgDetails = new DataGridView();
            _dgDetails.CellFormatting += new DataGridViewCellFormattingEventHandler(dgDetails_CellFormatting);
            txtDoneDate = new PSB_FarsiDateControl.PSB_DateControl();
            _cmdCalcGarbageQuantity = new System.Windows.Forms.Button();
            _cmdCalcGarbageQuantity.Click += new EventHandler(cmdCalcGarbageQuantity_Click);
            colDetailCode = new DataGridViewTextBoxColumn();
            colDetailName = new DataGridViewTextBoxColumn();
            colPrimaryStock = new DataGridViewTextBoxColumn();
            colRequirementQuntity = new DataGridViewTextBoxColumn();
            colProductionQuantity = new DataGridViewTextBoxColumn();
            colProductionStock = new DataGridViewTextBoxColumn();
            colCalcedOverStock = new DataGridViewTextBoxColumn();
            colRealOverQuantity = new DataGridViewTextBoxColumn();
            colGarbageQuantity = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgBatch).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtDoneQuantity).BeginInit();
            Panel1.SuspendLayout();
            GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgDetails).BeginInit();
            SuspendLayout();
            // 
            // dgBatch
            // 
            dgBatch.AllowUserToAddRows = false;
            dgBatch.AllowUserToResizeColumns = false;
            dgBatch.AllowUserToResizeRows = false;
            dgBatch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgBatch.BackgroundColor = Color.WhiteSmoke;
            dgBatch.ColumnHeadersHeight = 30;
            dgBatch.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgBatch.Columns.AddRange(new DataGridViewColumn[] { DataGridViewTextBoxColumn1, DataGridViewTextBoxColumn2, DataGridViewTextBoxColumn3, DataGridViewTextBoxColumn4, DataGridViewTextBoxColumn5, DataGridViewTextBoxColumn6 });
            dgBatch.Location = new Point(12, 21);
            dgBatch.Name = "dgBatch";
            dgBatch.RowHeadersVisible = false;
            dgBatch.RowHeadersWidth = 20;
            dgBatch.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            DataGridViewCellStyle1.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(224)), Conversions.ToInteger(Conversions.ToByte(192)));
            dgBatch.RowsDefaultCellStyle = DataGridViewCellStyle1;
            dgBatch.RowTemplate.DefaultCellStyle.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(224)), Conversions.ToInteger(Conversions.ToByte(192)));
            dgBatch.RowTemplate.DefaultCellStyle.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            dgBatch.RowTemplate.Height = 30;
            dgBatch.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgBatch.Size = new Size(835, 83);
            dgBatch.TabIndex = 179;
            dgBatch.TabStop = false;
            // 
            // DataGridViewTextBoxColumn1
            // 
            DataGridViewTextBoxColumn1.HeaderText = "کدبچ";
            DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1";
            DataGridViewTextBoxColumn1.ReadOnly = true;
            DataGridViewTextBoxColumn1.Resizable = DataGridViewTriState.False;
            DataGridViewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // DataGridViewTextBoxColumn2
            // 
            DataGridViewTextBoxColumn2.HeaderText = "تاریخ تعریف بچ";
            DataGridViewTextBoxColumn2.MinimumWidth = 100;
            DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2";
            DataGridViewTextBoxColumn2.ReadOnly = true;
            DataGridViewTextBoxColumn2.Resizable = DataGridViewTriState.False;
            DataGridViewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // DataGridViewTextBoxColumn3
            // 
            DataGridViewTextBoxColumn3.HeaderText = "کد و نام محصول";
            DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3";
            DataGridViewTextBoxColumn3.ReadOnly = true;
            DataGridViewTextBoxColumn3.Resizable = DataGridViewTriState.False;
            DataGridViewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.NotSortable;
            DataGridViewTextBoxColumn3.Width = 339;
            // 
            // DataGridViewTextBoxColumn4
            // 
            DataGridViewTextBoxColumn4.HeaderText = "تعداد تولید";
            DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4";
            DataGridViewTextBoxColumn4.ReadOnly = true;
            DataGridViewTextBoxColumn4.Resizable = DataGridViewTriState.False;
            DataGridViewTextBoxColumn4.SortMode = DataGridViewColumnSortMode.NotSortable;
            DataGridViewTextBoxColumn4.Width = 93;
            // 
            // DataGridViewTextBoxColumn5
            // 
            DataGridViewTextBoxColumn5.HeaderText = "تاریخ شروع تولید";
            DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5";
            DataGridViewTextBoxColumn5.ReadOnly = true;
            DataGridViewTextBoxColumn5.Resizable = DataGridViewTriState.False;
            DataGridViewTextBoxColumn5.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // DataGridViewTextBoxColumn6
            // 
            DataGridViewTextBoxColumn6.HeaderText = "درصد پیشرفت";
            DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6";
            DataGridViewTextBoxColumn6.ReadOnly = true;
            DataGridViewTextBoxColumn6.Resizable = DataGridViewTriState.False;
            DataGridViewTextBoxColumn6.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // Label5
            // 
            Label5.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label5.Location = new Point(464, 122);
            Label5.Name = "Label5";
            Label5.RightToLeft = RightToLeft.Yes;
            Label5.Size = new Size(63, 17);
            Label5.TabIndex = 181;
            Label5.Text = "تاریخ بستن";
            // 
            // lblMinDelivary
            // 
            lblMinDelivary.BackColor = SystemColors.Control;
            lblMinDelivary.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblMinDelivary.ForeColor = Color.Black;
            lblMinDelivary.Location = new Point(719, 120);
            lblMinDelivary.Name = "lblMinDelivary";
            lblMinDelivary.RightToLeft = RightToLeft.Yes;
            lblMinDelivary.Size = new Size(128, 17);
            lblMinDelivary.TabIndex = 183;
            lblMinDelivary.Text = "تعداد محصول تولید شده";
            // 
            // txtDoneQuantity
            // 
            txtDoneQuantity.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtDoneQuantity.Location = new Point(599, 119);
            txtDoneQuantity.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            txtDoneQuantity.Name = "txtDoneQuantity";
            txtDoneQuantity.Size = new Size(114, 21);
            txtDoneQuantity.TabIndex = 0;
            // 
            // Panel1
            // 
            Panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Panel1.BackColor = SystemColors.Control;
            Panel1.BorderStyle = BorderStyle.FixedSingle;
            Panel1.Controls.Add(_cmdSave);
            Panel1.Controls.Add(_cmdExit);
            Panel1.Location = new Point(12, 362);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(835, 48);
            Panel1.TabIndex = 184;
            // 
            // cmdSave
            // 
            _cmdSave.BackColor = Color.Transparent;
            _cmdSave.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdSave.ForeColor = Color.Black;
            _cmdSave.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdSave.Location = new Point(433, 10);
            _cmdSave.Name = "_cmdSave";
            _cmdSave.RightToLeft = RightToLeft.No;
            _cmdSave.Size = new Size(116, 28);
            _cmdSave.TabIndex = 3;
            _cmdSave.Text = "تایید";
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
            _cmdExit.Location = new Point(283, 10);
            _cmdExit.Name = "_cmdExit";
            _cmdExit.RightToLeft = RightToLeft.No;
            _cmdExit.Size = new Size(116, 28);
            _cmdExit.TabIndex = 4;
            _cmdExit.Text = "انصراف";
            _cmdExit.TextAlign = ContentAlignment.MiddleRight;
            _cmdExit.UseVisualStyleBackColor = false;
            // 
            // GroupBox1
            // 
            GroupBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox1.Controls.Add(_dgDetails);
            GroupBox1.Location = new Point(12, 152);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(835, 205);
            GroupBox1.TabIndex = 185;
            GroupBox1.TabStop = false;
            GroupBox1.Text = "اطلاعات درخت محصول";
            // 
            // dgDetails
            // 
            _dgDetails.AllowUserToAddRows = false;
            _dgDetails.AllowUserToResizeColumns = false;
            _dgDetails.AllowUserToResizeRows = false;
            DataGridViewCellStyle2.BackColor = Color.White;
            _dgDetails.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2;
            _dgDetails.BackgroundColor = Color.WhiteSmoke;
            _dgDetails.ColumnHeadersHeight = 40;
            _dgDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            _dgDetails.Columns.AddRange(new DataGridViewColumn[] { colDetailCode, colDetailName, colPrimaryStock, colRequirementQuntity, colProductionQuantity, colProductionStock, colCalcedOverStock, colRealOverQuantity, colGarbageQuantity });
            _dgDetails.Dock = DockStyle.Fill;
            _dgDetails.Location = new Point(3, 17);
            _dgDetails.Name = "_dgDetails";
            _dgDetails.RowHeadersVisible = false;
            _dgDetails.RowHeadersWidth = 10;
            _dgDetails.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            DataGridViewCellStyle5.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(224)), Conversions.ToInteger(Conversions.ToByte(192)));
            _dgDetails.RowsDefaultCellStyle = DataGridViewCellStyle5;
            _dgDetails.RowTemplate.DefaultCellStyle.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(224)), Conversions.ToInteger(Conversions.ToByte(192)));
            _dgDetails.RowTemplate.DefaultCellStyle.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _dgDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgDetails.Size = new Size(829, 185);
            _dgDetails.TabIndex = 2;
            _dgDetails.TabStop = false;
            // 
            // txtDoneDate
            // 
            txtDoneDate.AutoValidate = AutoValidate.Disable;
            txtDoneDate.BackColor = Color.White;
            txtDoneDate.BackColorDateBox = Color.White;
            txtDoneDate.DateButtonShow = true;
            txtDoneDate.EnableDateText = true;
            txtDoneDate.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtDoneDate.FontMyDate = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtDoneDate.Location = new Point(355, 118);
            txtDoneDate.Margin = new Padding(4);
            txtDoneDate.MinimumSize = new Size(96, 24);
            txtDoneDate.Name = "txtDoneDate";
            txtDoneDate.Size = new Size(104, 24);
            txtDoneDate.TabIndex = 1;
            // 
            // cmdCalcGarbageQuantity
            // 
            _cmdCalcGarbageQuantity.BackColor = Color.Transparent;
            _cmdCalcGarbageQuantity.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdCalcGarbageQuantity.ForeColor = Color.Blue;
            _cmdCalcGarbageQuantity.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdCalcGarbageQuantity.Location = new Point(15, 132);
            _cmdCalcGarbageQuantity.Name = "_cmdCalcGarbageQuantity";
            _cmdCalcGarbageQuantity.RightToLeft = RightToLeft.No;
            _cmdCalcGarbageQuantity.Size = new Size(114, 24);
            _cmdCalcGarbageQuantity.TabIndex = 186;
            _cmdCalcGarbageQuantity.Text = "محاسبۀ تعداد ضایعات";
            _cmdCalcGarbageQuantity.UseVisualStyleBackColor = false;
            // 
            // colDetailCode
            // 
            colDetailCode.HeaderText = "کدجزء";
            colDetailCode.Name = "colDetailCode";
            colDetailCode.ReadOnly = true;
            colDetailCode.Resizable = DataGridViewTriState.False;
            colDetailCode.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // colDetailName
            // 
            colDetailName.HeaderText = "نام جزء";
            colDetailName.MinimumWidth = 120;
            colDetailName.Name = "colDetailName";
            colDetailName.ReadOnly = true;
            colDetailName.Resizable = DataGridViewTriState.False;
            colDetailName.SortMode = DataGridViewColumnSortMode.NotSortable;
            colDetailName.Width = 187;
            // 
            // colPrimaryStock
            // 
            colPrimaryStock.HeaderText = "موجودی اولیه";
            colPrimaryStock.Name = "colPrimaryStock";
            colPrimaryStock.ReadOnly = true;
            colPrimaryStock.Resizable = DataGridViewTriState.False;
            colPrimaryStock.SortMode = DataGridViewColumnSortMode.NotSortable;
            colPrimaryStock.Width = 65;
            // 
            // colRequirementQuntity
            // 
            colRequirementQuntity.HeaderText = "تعداد مورد نیاز";
            colRequirementQuntity.Name = "colRequirementQuntity";
            colRequirementQuntity.ReadOnly = true;
            colRequirementQuntity.Resizable = DataGridViewTriState.False;
            colRequirementQuntity.SortMode = DataGridViewColumnSortMode.NotSortable;
            colRequirementQuntity.Width = 68;
            // 
            // colProductionQuantity
            // 
            colProductionQuantity.HeaderText = "تعداد تولید شده";
            colProductionQuantity.Name = "colProductionQuantity";
            colProductionQuantity.ReadOnly = true;
            colProductionQuantity.Resizable = DataGridViewTriState.False;
            colProductionQuantity.SortMode = DataGridViewColumnSortMode.NotSortable;
            colProductionQuantity.Width = 80;
            // 
            // colProductionStock
            // 
            colProductionStock.HeaderText = "تعداد موجودی تولید";
            colProductionStock.Name = "colProductionStock";
            colProductionStock.ReadOnly = true;
            colProductionStock.Resizable = DataGridViewTriState.False;
            colProductionStock.SortMode = DataGridViewColumnSortMode.NotSortable;
            colProductionStock.Width = 80;
            // 
            // colCalcedOverStock
            // 
            colCalcedOverStock.HeaderText = "تعداد اضافی محاسبه شده";
            colCalcedOverStock.Name = "colCalcedOverStock";
            colCalcedOverStock.ReadOnly = true;
            colCalcedOverStock.Resizable = DataGridViewTriState.False;
            colCalcedOverStock.SortMode = DataGridViewColumnSortMode.NotSortable;
            colCalcedOverStock.Width = 80;
            // 
            // colRealOverQuantity
            // 
            DataGridViewCellStyle3.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(128)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(128)));
            colRealOverQuantity.DefaultCellStyle = DataGridViewCellStyle3;
            colRealOverQuantity.HeaderText = "تعداد اضافی شمارش شده";
            colRealOverQuantity.Name = "colRealOverQuantity";
            colRealOverQuantity.Resizable = DataGridViewTriState.False;
            colRealOverQuantity.SortMode = DataGridViewColumnSortMode.NotSortable;
            colRealOverQuantity.Width = 85;
            // 
            // colGarbageQuantity
            // 
            DataGridViewCellStyle4.BackColor = Color.Red;
            colGarbageQuantity.DefaultCellStyle = DataGridViewCellStyle4;
            colGarbageQuantity.HeaderText = "تعداد ضایعات";
            colGarbageQuantity.Name = "colGarbageQuantity";
            colGarbageQuantity.ReadOnly = true;
            colGarbageQuantity.Resizable = DataGridViewTriState.False;
            colGarbageQuantity.SortMode = DataGridViewColumnSortMode.NotSortable;
            colGarbageQuantity.Width = 62;
            // 
            // frmDoneBatch
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.EnableAllowFocusChange;
            CancelButton = _cmdExit;
            ClientSize = new Size(859, 416);
            Controls.Add(_cmdCalcGarbageQuantity);
            Controls.Add(txtDoneDate);
            Controls.Add(GroupBox1);
            Controls.Add(Panel1);
            Controls.Add(lblMinDelivary);
            Controls.Add(txtDoneQuantity);
            Controls.Add(Label5);
            Controls.Add(dgBatch);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmDoneBatch";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " مشخصات بستن بچ";
            ((System.ComponentModel.ISupportInitialize)dgBatch).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtDoneQuantity).EndInit();
            Panel1.ResumeLayout(false);
            GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_dgDetails).EndInit();
            Load += new EventHandler(frmDoneBatch_Load);
            FormClosing += new FormClosingEventHandler(frmDoneBatch_FormClosing);
            ResumeLayout(false);
        }

        internal DataGridView dgBatch;
        internal Label Label5;
        internal Label lblMinDelivary;
        internal NumericUpDown txtDoneQuantity;
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
                    _cmdExit.Click -= cmdExit_Click;
                }

                _cmdExit = value;
                if (_cmdExit != null)
                {
                    _cmdExit.Click += cmdExit_Click;
                }
            }
        }

        internal GroupBox GroupBox1;
        private DataGridView _dgDetails;

        internal DataGridView dgDetails
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _dgDetails;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_dgDetails != null)
                {
                    _dgDetails.CellFormatting -= dgDetails_CellFormatting;
                }

                _dgDetails = value;
                if (_dgDetails != null)
                {
                    _dgDetails.CellFormatting += dgDetails_CellFormatting;
                }
            }
        }

        internal PSB_FarsiDateControl.PSB_DateControl txtDoneDate;
        internal DataGridViewTextBoxColumn DataGridViewTextBoxColumn1;
        internal DataGridViewTextBoxColumn DataGridViewTextBoxColumn2;
        internal DataGridViewTextBoxColumn DataGridViewTextBoxColumn3;
        internal DataGridViewTextBoxColumn DataGridViewTextBoxColumn4;
        internal DataGridViewTextBoxColumn DataGridViewTextBoxColumn5;
        internal DataGridViewTextBoxColumn DataGridViewTextBoxColumn6;
        private System.Windows.Forms.Button _cmdCalcGarbageQuantity;

        internal System.Windows.Forms.Button cmdCalcGarbageQuantity
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdCalcGarbageQuantity;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdCalcGarbageQuantity != null)
                {
                    _cmdCalcGarbageQuantity.Click -= cmdCalcGarbageQuantity_Click;
                }

                _cmdCalcGarbageQuantity = value;
                if (_cmdCalcGarbageQuantity != null)
                {
                    _cmdCalcGarbageQuantity.Click += cmdCalcGarbageQuantity_Click;
                }
            }
        }

        internal DataGridViewTextBoxColumn colDetailCode;
        internal DataGridViewTextBoxColumn colDetailName;
        internal DataGridViewTextBoxColumn colPrimaryStock;
        internal DataGridViewTextBoxColumn colRequirementQuntity;
        internal DataGridViewTextBoxColumn colProductionQuantity;
        internal DataGridViewTextBoxColumn colProductionStock;
        internal DataGridViewTextBoxColumn colCalcedOverStock;
        internal DataGridViewTextBoxColumn colRealOverQuantity;
        internal DataGridViewTextBoxColumn colGarbageQuantity;
    }
}