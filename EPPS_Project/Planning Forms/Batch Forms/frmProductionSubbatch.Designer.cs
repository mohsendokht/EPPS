using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmProductionSubbatch : Form
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
            var DataGridViewCellStyle5 = new DataGridViewCellStyle();
            var DataGridViewCellStyle6 = new DataGridViewCellStyle();
            _cmdSave = new System.Windows.Forms.Button();
            _cmdSave.Click += new EventHandler(cmdSave_Click);
            _cmdExit = new System.Windows.Forms.Button();
            _cmdExit.Click += new EventHandler(cmdExit_Click);
            Panel1 = new System.Windows.Forms.Panel();
            Label5 = new Label();
            txtProductionQuantity = new NumericUpDown();
            Label7 = new Label();
            lblBatchCode = new Label();
            Label13 = new Label();
            lblSubbatchSerial = new Label();
            Label2 = new Label();
            lblSubbatchCode = new Label();
            Label4 = new Label();
            Label3 = new Label();
            txtTransferMinQty = new NumericUpDown();
            Label1 = new Label();
            dgDetails = new DataGridView();
            Column3 = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            txtExecutionPriority = new NumericUpDown();
            txtDeliveryDate = new PSB_FarsiDateControl.PSB_DateControl();
            Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtProductionQuantity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtTransferMinQty).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgDetails).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtExecutionPriority).BeginInit();
            SuspendLayout();
            // 
            // cmdSave
            // 
            _cmdSave.BackColor = Color.Transparent;
            _cmdSave.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdSave.ForeColor = Color.Black;
            _cmdSave.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdSave.Location = new Point(333, 10);
            _cmdSave.Name = "_cmdSave";
            _cmdSave.RightToLeft = RightToLeft.No;
            _cmdSave.Size = new Size(90, 28);
            _cmdSave.TabIndex = 5;
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
            _cmdExit.Location = new Point(180, 10);
            _cmdExit.Name = "_cmdExit";
            _cmdExit.RightToLeft = RightToLeft.No;
            _cmdExit.Size = new Size(90, 28);
            _cmdExit.TabIndex = 6;
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
            Panel1.Location = new Point(13, 326);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(604, 48);
            Panel1.TabIndex = 176;
            // 
            // Label5
            // 
            Label5.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label5.Location = new Point(484, 294);
            Label5.Name = "Label5";
            Label5.RightToLeft = RightToLeft.Yes;
            Label5.Size = new Size(134, 17);
            Label5.TabIndex = 180;
            Label5.Text = "تاریخ تحویل اولین محموله:";
            // 
            // txtProductionQuantity
            // 
            txtProductionQuantity.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtProductionQuantity.Location = new Point(418, 75);
            txtProductionQuantity.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            txtProductionQuantity.Name = "txtProductionQuantity";
            txtProductionQuantity.Size = new Size(87, 21);
            txtProductionQuantity.TabIndex = 0;
            // 
            // Label7
            // 
            Label7.BackColor = SystemColors.Control;
            Label7.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label7.ForeColor = Color.Black;
            Label7.Location = new Point(510, 77);
            Label7.Name = "Label7";
            Label7.RightToLeft = RightToLeft.Yes;
            Label7.Size = new Size(108, 18);
            Label7.TabIndex = 177;
            Label7.Text = "تعداد تولید ساب بچ:";
            // 
            // lblBatchCode
            // 
            lblBatchCode.BackColor = SystemColors.Control;
            lblBatchCode.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblBatchCode.ForeColor = Color.Blue;
            lblBatchCode.Location = new Point(335, 15);
            lblBatchCode.Name = "lblBatchCode";
            lblBatchCode.RightToLeft = RightToLeft.Yes;
            lblBatchCode.Size = new Size(213, 17);
            lblBatchCode.TabIndex = 327;
            lblBatchCode.Text = "#";
            // 
            // Label13
            // 
            Label13.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label13.Location = new Point(578, 15);
            Label13.Name = "Label13";
            Label13.RightToLeft = RightToLeft.Yes;
            Label13.Size = new Size(40, 17);
            Label13.TabIndex = 326;
            Label13.Text = "کد بچ:";
            // 
            // lblSubbatchSerial
            // 
            lblSubbatchSerial.BackColor = SystemColors.Control;
            lblSubbatchSerial.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblSubbatchSerial.ForeColor = Color.Blue;
            lblSubbatchSerial.Location = new Point(152, 42);
            lblSubbatchSerial.Name = "lblSubbatchSerial";
            lblSubbatchSerial.RightToLeft = RightToLeft.Yes;
            lblSubbatchSerial.Size = new Size(74, 17);
            lblSubbatchSerial.TabIndex = 329;
            lblSubbatchSerial.Text = "#";
            // 
            // Label2
            // 
            Label2.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label2.Location = new Point(225, 42);
            Label2.Name = "Label2";
            Label2.RightToLeft = RightToLeft.Yes;
            Label2.Size = new Size(126, 17);
            Label2.TabIndex = 328;
            Label2.Text = "شماره سریال ساب بچ:";
            // 
            // lblSubbatchCode
            // 
            lblSubbatchCode.BackColor = SystemColors.Control;
            lblSubbatchCode.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblSubbatchCode.ForeColor = Color.Blue;
            lblSubbatchCode.Location = new Point(367, 42);
            lblSubbatchCode.Name = "lblSubbatchCode";
            lblSubbatchCode.RightToLeft = RightToLeft.Yes;
            lblSubbatchCode.Size = new Size(181, 17);
            lblSubbatchCode.TabIndex = 331;
            lblSubbatchCode.Text = "#";
            // 
            // Label4
            // 
            Label4.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label4.Location = new Point(546, 42);
            Label4.Name = "Label4";
            Label4.RightToLeft = RightToLeft.Yes;
            Label4.Size = new Size(72, 17);
            Label4.TabIndex = 330;
            Label4.Text = "کد ساب بچ:";
            // 
            // Label3
            // 
            Label3.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label3.Location = new Point(231, 77);
            Label3.Name = "Label3";
            Label3.RightToLeft = RightToLeft.Yes;
            Label3.Size = new Size(120, 18);
            Label3.TabIndex = 332;
            Label3.Text = "اولویت اجرای ساب بچ:";
            // 
            // txtTransferMinQty
            // 
            txtTransferMinQty.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtTransferMinQty.Location = new Point(147, 293);
            txtTransferMinQty.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            txtTransferMinQty.Name = "txtTransferMinQty";
            txtTransferMinQty.Size = new Size(87, 21);
            txtTransferMinQty.TabIndex = 4;
            // 
            // Label1
            // 
            Label1.BackColor = SystemColors.Control;
            Label1.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label1.ForeColor = Color.Black;
            Label1.Location = new Point(237, 294);
            Label1.Name = "Label1";
            Label1.RightToLeft = RightToLeft.Yes;
            Label1.Size = new Size(115, 17);
            Label1.TabIndex = 334;
            Label1.Text = "حداقل تعداد ارسالی:";
            // 
            // dgDetails
            // 
            dgDetails.AllowUserToAddRows = false;
            dgDetails.AllowUserToResizeColumns = false;
            dgDetails.AllowUserToResizeRows = false;
            DataGridViewCellStyle5.BackColor = Color.White;
            dgDetails.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5;
            dgDetails.BackgroundColor = Color.WhiteSmoke;
            dgDetails.ColumnHeadersHeight = 30;
            dgDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgDetails.Columns.AddRange(new DataGridViewColumn[] { Column3, Column1, Column4, Column2, Column5, Column6 });
            dgDetails.Location = new Point(13, 113);
            dgDetails.Name = "dgDetails";
            dgDetails.RowHeadersWidth = 10;
            dgDetails.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            DataGridViewCellStyle6.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(224)), Conversions.ToInteger(Conversions.ToByte(192)));
            dgDetails.RowsDefaultCellStyle = DataGridViewCellStyle6;
            dgDetails.RowTemplate.DefaultCellStyle.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(224)), Conversions.ToInteger(Conversions.ToByte(192)));
            dgDetails.RowTemplate.DefaultCellStyle.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            dgDetails.Size = new Size(604, 165);
            dgDetails.TabIndex = 2;
            dgDetails.TabStop = false;
            // 
            // Column3
            // 
            Column3.HeaderText = "کدجزء";
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Resizable = DataGridViewTriState.False;
            Column3.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column3.Width = 110;
            // 
            // Column1
            // 
            Column1.HeaderText = "نام جزء";
            Column1.MinimumWidth = 120;
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Resizable = DataGridViewTriState.False;
            Column1.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column1.Width = 215;
            // 
            // Column4
            // 
            Column4.HeaderText = "ضریب مصرف";
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            Column4.Resizable = DataGridViewTriState.False;
            Column4.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column4.Width = 80;
            // 
            // Column2
            // 
            Column2.HeaderText = "تعداد موجودی";
            Column2.Name = "Column2";
            Column2.Resizable = DataGridViewTriState.False;
            Column2.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column2.Width = 80;
            // 
            // Column5
            // 
            Column5.HeaderText = "تعداد مورد نیاز";
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            Column5.Resizable = DataGridViewTriState.False;
            Column5.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column5.Width = 85;
            // 
            // Column6
            // 
            Column6.HeaderText = "كد جزء پدر";
            Column6.Name = "Column6";
            Column6.Visible = false;
            // 
            // txtExecutionPriority
            // 
            txtExecutionPriority.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtExecutionPriority.Location = new Point(147, 75);
            txtExecutionPriority.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            txtExecutionPriority.Name = "txtExecutionPriority";
            txtExecutionPriority.Size = new Size(76, 21);
            txtExecutionPriority.TabIndex = 1;
            // 
            // txtDeliveryDate
            // 
            txtDeliveryDate.AutoValidate = AutoValidate.Disable;
            txtDeliveryDate.BackColor = Color.White;
            txtDeliveryDate.BackColorDateBox = Color.White;
            txtDeliveryDate.DateButtonShow = true;
            txtDeliveryDate.EnableDateText = true;
            txtDeliveryDate.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtDeliveryDate.FontMyDate = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtDeliveryDate.Location = new Point(378, 292);
            txtDeliveryDate.Margin = new Padding(4);
            txtDeliveryDate.MinimumSize = new Size(96, 24);
            txtDeliveryDate.Name = "txtDeliveryDate";
            txtDeliveryDate.Size = new Size(105, 24);
            txtDeliveryDate.TabIndex = 3;
            // 
            // frmProductionSubbatch
            // 
            AcceptButton = _cmdSave;
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _cmdExit;
            ClientSize = new Size(630, 381);
            Controls.Add(txtDeliveryDate);
            Controls.Add(txtExecutionPriority);
            Controls.Add(dgDetails);
            Controls.Add(txtTransferMinQty);
            Controls.Add(Label1);
            Controls.Add(Label3);
            Controls.Add(lblSubbatchCode);
            Controls.Add(Label4);
            Controls.Add(lblSubbatchSerial);
            Controls.Add(Label2);
            Controls.Add(lblBatchCode);
            Controls.Add(Label13);
            Controls.Add(Label5);
            Controls.Add(txtProductionQuantity);
            Controls.Add(Label7);
            Controls.Add(Panel1);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Location = new Point(50, 50);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmProductionSubbatch";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " مشخصات ساب بچ تولید";
            Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)txtProductionQuantity).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtTransferMinQty).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgDetails).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtExecutionPriority).EndInit();
            Load += new EventHandler(Form1_Load);
            FormClosing += new FormClosingEventHandler(frmContractor_FormClosing);
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
        internal Label Label5;
        internal NumericUpDown txtProductionQuantity;
        internal Label Label7;
        internal Label lblBatchCode;
        internal Label Label13;
        internal Label lblSubbatchSerial;
        internal Label Label2;
        internal Label lblSubbatchCode;
        internal Label Label4;
        internal Label Label3;
        internal NumericUpDown txtTransferMinQty;
        internal Label Label1;
        internal DataGridView dgDetails;
        internal NumericUpDown txtExecutionPriority;
        internal PSB_FarsiDateControl.PSB_DateControl txtDeliveryDate;
        internal DataGridViewTextBoxColumn Column3;
        internal DataGridViewTextBoxColumn Column1;
        internal DataGridViewTextBoxColumn Column4;
        internal DataGridViewTextBoxColumn Column2;
        internal DataGridViewTextBoxColumn Column5;
        internal DataGridViewTextBoxColumn Column6;
    }
}