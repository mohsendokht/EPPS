using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmRealProduction : Form
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Panel1 = new System.Windows.Forms.Panel();
            this._cmdSave = new System.Windows.Forms.Button();
            this._cmdExit = new System.Windows.Forms.Button();
            this._cmdDelete = new System.Windows.Forms.Button();
            this._cbSubbatch = new System.Windows.Forms.ComboBox();
            this.Label4 = new System.Windows.Forms.Label();
            this._cbOperation = new System.Windows.Forms.ComboBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.txtEndDate = new PSB_FarsiDateControl.PSB_DateControl();
            this.txtStartDate = new PSB_FarsiDateControl.PSB_DateControl();
            this.EndDate_Label = new System.Windows.Forms.Label();
            this.StartDate_Label = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.txtEndHour = new System.Windows.Forms.DateTimePicker();
            this.txtStartHour = new System.Windows.Forms.DateTimePicker();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.rbOpType3 = new System.Windows.Forms.RadioButton();
            this.rbOpType2 = new System.Windows.Forms.RadioButton();
            this.rbOpType1 = new System.Windows.Forms.RadioButton();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.cbHaltReason = new System.Windows.Forms.ComboBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.txtHaltTime = new System.Windows.Forms.DateTimePicker();
            this.GroupBox4 = new System.Windows.Forms.GroupBox();
            this.QtyLbl = new System.Windows.Forms.Label();
            this.cbGarbageOpUnits = new System.Windows.Forms.ComboBox();
            this.txtGarbageFunStatistics = new System.Windows.Forms.NumericUpDown();
            this._cmdCalcProductionQty = new System.Windows.Forms.Button();
            this._cmdNewOperationUnit = new System.Windows.Forms.Button();
            this.cbOperationUnits = new System.Windows.Forms.ComboBox();
            this.StatUnitLbl = new System.Windows.Forms.Label();
            this.txtFunctionStatistics = new System.Windows.Forms.NumericUpDown();
            this.FunctionStatisticsLbl = new System.Windows.Forms.Label();
            this.txtGarbageQty = new System.Windows.Forms.NumericUpDown();
            this.Label9 = new System.Windows.Forms.Label();
            this.txtIntactQty = new System.Windows.Forms.NumericUpDown();
            this.Label8 = new System.Windows.Forms.Label();
            this._cbOperators = new PSBMultiColumnComboBox.PSBMultiColumnComboBox();
            this._cbBatch = new System.Windows.Forms.ComboBox();
            this.Label16 = new System.Windows.Forms.Label();
            this._cmdAddOperator = new System.Windows.Forms.Button();
            this._cmdRemoveOperator = new System.Windows.Forms.Button();
            this.dgOperators = new System.Windows.Forms.DataGridView();
            this.colOperatorCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOperatorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Label1 = new System.Windows.Forms.Label();
            this.Machin_Lookup = new ProductionPlanning.Controls.UserControl_LookUp();
            this.Panel1.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.GroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtGarbageFunStatistics)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFunctionStatistics)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGarbageQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIntactQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgOperators)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this._cmdSave);
            this.Panel1.Controls.Add(this._cmdExit);
            this.Panel1.Controls.Add(this._cmdDelete);
            this.Panel1.ForeColor = System.Drawing.Color.Black;
            this.Panel1.Location = new System.Drawing.Point(10, 493);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(558, 48);
            this.Panel1.TabIndex = 16;
            // 
            // _cmdSave
            // 
            this._cmdSave.BackColor = System.Drawing.Color.Transparent;
            this._cmdSave.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdSave.ForeColor = System.Drawing.Color.Black;
            this._cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdSave.Location = new System.Drawing.Point(294, 10);
            this._cmdSave.Name = "_cmdSave";
            this._cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdSave.Size = new System.Drawing.Size(90, 28);
            this._cmdSave.TabIndex = 16;
            this._cmdSave.Text = "ثبت";
            this._cmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._cmdSave.UseVisualStyleBackColor = false;
            this._cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // _cmdExit
            // 
            this._cmdExit.BackColor = System.Drawing.Color.Transparent;
            this._cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cmdExit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdExit.ForeColor = System.Drawing.Color.Red;
            this._cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdExit.Location = new System.Drawing.Point(173, 10);
            this._cmdExit.Name = "_cmdExit";
            this._cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdExit.Size = new System.Drawing.Size(90, 28);
            this._cmdExit.TabIndex = 17;
            this._cmdExit.Text = "انصراف";
            this._cmdExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._cmdExit.UseVisualStyleBackColor = false;
            this._cmdExit.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // _cmdDelete
            // 
            this._cmdDelete.BackColor = System.Drawing.Color.Transparent;
            this._cmdDelete.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._cmdDelete.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdDelete.ForeColor = System.Drawing.Color.Black;
            this._cmdDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdDelete.Location = new System.Drawing.Point(294, 10);
            this._cmdDelete.Name = "_cmdDelete";
            this._cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdDelete.Size = new System.Drawing.Size(90, 28);
            this._cmdDelete.TabIndex = 18;
            this._cmdDelete.Text = "حذف";
            this._cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._cmdDelete.UseVisualStyleBackColor = false;
            this._cmdDelete.Visible = false;
            this._cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // _cbSubbatch
            // 
            this._cbSubbatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cbSubbatch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cbSubbatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._cbSubbatch.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cbSubbatch.FormattingEnabled = true;
            this._cbSubbatch.Location = new System.Drawing.Point(20, 99);
            this._cbSubbatch.Name = "_cbSubbatch";
            this._cbSubbatch.Size = new System.Drawing.Size(188, 25);
            this._cbSubbatch.TabIndex = 5;
            this._cbSubbatch.SelectionChangeCommitted += new System.EventHandler(this.cbSubbatch_SelectionChangeCommitted);
            // 
            // Label4
            // 
            this.Label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label4.Location = new System.Drawing.Point(214, 98);
            this.Label4.Name = "Label4";
            this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label4.Size = new System.Drawing.Size(67, 20);
            this.Label4.TabIndex = 196;
            this.Label4.Text = "کد ساب بچ";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _cbOperation
            // 
            this._cbOperation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cbOperation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._cbOperation.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cbOperation.FormattingEnabled = true;
            this._cbOperation.Location = new System.Drawing.Point(20, 134);
            this._cbOperation.Name = "_cbOperation";
            this._cbOperation.Size = new System.Drawing.Size(476, 25);
            this._cbOperation.TabIndex = 6;
            this._cbOperation.SelectionChangeCommitted += new System.EventHandler(this.cbOperation_SelectionChangeCommitted);
            // 
            // Label3
            // 
            this.Label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label3.Location = new System.Drawing.Point(502, 133);
            this.Label3.Name = "Label3";
            this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label3.Size = new System.Drawing.Size(63, 20);
            this.Label3.TabIndex = 194;
            this.Label3.Text = "کد فعالیت";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label2.Location = new System.Drawing.Point(502, 168);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label2.Size = new System.Drawing.Size(63, 20);
            this.Label2.TabIndex = 198;
            this.Label2.Text = "ماشین";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.txtEndDate);
            this.GroupBox1.Controls.Add(this.EndDate_Label);
            this.GroupBox1.Controls.Add(this.StartDate_Label);
            this.GroupBox1.Controls.Add(this.Label6);
            this.GroupBox1.Controls.Add(this.Label5);
            this.GroupBox1.Controls.Add(this.txtEndHour);
            this.GroupBox1.Controls.Add(this.txtStartHour);
            this.GroupBox1.Location = new System.Drawing.Point(10, 6);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(364, 78);
            this.GroupBox1.TabIndex = 1;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "زمان تولید";
            // 
            // txtEndDate
            // 
            this.txtEndDate.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.txtEndDate.BackColor = System.Drawing.Color.White;
            this.txtEndDate.BackColorDateBox = System.Drawing.Color.White;
            this.txtEndDate.DateButtonShow = true;
            this.txtEndDate.EnableDateText = true;
            this.txtEndDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtEndDate.FontMyDate = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtEndDate.Location = new System.Drawing.Point(185, 47);
            this.txtEndDate.Margin = new System.Windows.Forms.Padding(4);
            this.txtEndDate.MinimumSize = new System.Drawing.Size(96, 24);
            this.txtEndDate.Name = "txtEndDate";
            this.txtEndDate.Size = new System.Drawing.Size(105, 24);
            this.txtEndDate.TabIndex = 211;
            this.txtEndDate.Visible = false;
            // 
            // txtStartDate
            // 
            this.txtStartDate.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.txtStartDate.BackColor = System.Drawing.Color.White;
            this.txtStartDate.BackColorDateBox = System.Drawing.Color.White;
            this.txtStartDate.DateButtonShow = true;
            this.txtStartDate.EnableDateText = true;
            this.txtStartDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtStartDate.FontMyDate = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtStartDate.Location = new System.Drawing.Point(195, 21);
            this.txtStartDate.Margin = new System.Windows.Forms.Padding(4);
            this.txtStartDate.MinimumSize = new System.Drawing.Size(96, 24);
            this.txtStartDate.Name = "txtStartDate";
            this.txtStartDate.Size = new System.Drawing.Size(105, 24);
            this.txtStartDate.TabIndex = 2;
            // 
            // EndDate_Label
            // 
            this.EndDate_Label.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.EndDate_Label.Location = new System.Drawing.Point(290, 47);
            this.EndDate_Label.Name = "EndDate_Label";
            this.EndDate_Label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.EndDate_Label.Size = new System.Drawing.Size(66, 20);
            this.EndDate_Label.TabIndex = 209;
            this.EndDate_Label.Text = "تاریخ پایان:";
            this.EndDate_Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.EndDate_Label.Visible = false;
            // 
            // StartDate_Label
            // 
            this.StartDate_Label.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.StartDate_Label.Location = new System.Drawing.Point(290, 21);
            this.StartDate_Label.Name = "StartDate_Label";
            this.StartDate_Label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartDate_Label.Size = new System.Drawing.Size(66, 20);
            this.StartDate_Label.TabIndex = 208;
            this.StartDate_Label.Text = "تاریخ تولید:";
            this.StartDate_Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label6
            // 
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label6.Location = new System.Drawing.Point(90, 47);
            this.Label6.Name = "Label6";
            this.Label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label6.Size = new System.Drawing.Size(72, 20);
            this.Label6.TabIndex = 205;
            this.Label6.Text = "ساعت پایان:";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label5
            // 
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label5.Location = new System.Drawing.Point(90, 19);
            this.Label5.Name = "Label5";
            this.Label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label5.Size = new System.Drawing.Size(79, 20);
            this.Label5.TabIndex = 204;
            this.Label5.Text = "ساعت شروع:";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtEndHour
            // 
            this.txtEndHour.CustomFormat = "HH:mm";
            this.txtEndHour.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtEndHour.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtEndHour.Location = new System.Drawing.Point(10, 47);
            this.txtEndHour.Name = "txtEndHour";
            this.txtEndHour.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtEndHour.RightToLeftLayout = true;
            this.txtEndHour.ShowUpDown = true;
            this.txtEndHour.Size = new System.Drawing.Size(76, 24);
            this.txtEndHour.TabIndex = 4;
            this.txtEndHour.Value = new System.DateTime(2008, 6, 5, 0, 0, 0, 0);
            // 
            // txtStartHour
            // 
            this.txtStartHour.CustomFormat = "HH:mm";
            this.txtStartHour.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtStartHour.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtStartHour.Location = new System.Drawing.Point(10, 19);
            this.txtStartHour.Name = "txtStartHour";
            this.txtStartHour.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtStartHour.RightToLeftLayout = true;
            this.txtStartHour.ShowUpDown = true;
            this.txtStartHour.Size = new System.Drawing.Size(76, 24);
            this.txtStartHour.TabIndex = 3;
            this.txtStartHour.Value = new System.DateTime(2008, 6, 5, 0, 0, 0, 0);
            // 
            // GroupBox2
            // 
            this.GroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox2.Controls.Add(this.rbOpType3);
            this.GroupBox2.Controls.Add(this.rbOpType2);
            this.GroupBox2.Controls.Add(this.rbOpType1);
            this.GroupBox2.Location = new System.Drawing.Point(380, 6);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(188, 78);
            this.GroupBox2.TabIndex = 1;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "نوع عملیات";
            // 
            // rbOpType3
            // 
            this.rbOpType3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.rbOpType3.Location = new System.Drawing.Point(7, 33);
            this.rbOpType3.Name = "rbOpType3";
            this.rbOpType3.Size = new System.Drawing.Size(83, 20);
            this.rbOpType3.TabIndex = 1;
            this.rbOpType3.Text = "دوباره کاری";
            this.rbOpType3.UseVisualStyleBackColor = true;
            // 
            // rbOpType2
            // 
            this.rbOpType2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.rbOpType2.Location = new System.Drawing.Point(99, 52);
            this.rbOpType2.Name = "rbOpType2";
            this.rbOpType2.Size = new System.Drawing.Size(83, 20);
            this.rbOpType2.TabIndex = 1;
            this.rbOpType2.Text = "خارج برنامه";
            this.rbOpType2.UseVisualStyleBackColor = true;
            this.rbOpType2.Visible = false;
            // 
            // rbOpType1
            // 
            this.rbOpType1.Checked = true;
            this.rbOpType1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.rbOpType1.Location = new System.Drawing.Point(99, 33);
            this.rbOpType1.Name = "rbOpType1";
            this.rbOpType1.Size = new System.Drawing.Size(83, 20);
            this.rbOpType1.TabIndex = 1;
            this.rbOpType1.TabStop = true;
            this.rbOpType1.Text = "طیق برنامه";
            this.rbOpType1.UseVisualStyleBackColor = true;
            // 
            // GroupBox3
            // 
            this.GroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox3.Controls.Add(this.cbHaltReason);
            this.GroupBox3.Controls.Add(this.Label12);
            this.GroupBox3.Controls.Add(this.Label7);
            this.GroupBox3.Controls.Add(this.txtHaltTime);
            this.GroupBox3.Location = new System.Drawing.Point(11, 433);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(557, 51);
            this.GroupBox3.TabIndex = 14;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "توقف عملیات";
            // 
            // cbHaltReason
            // 
            this.cbHaltReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHaltReason.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbHaltReason.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cbHaltReason.FormattingEnabled = true;
            this.cbHaltReason.Location = new System.Drawing.Point(9, 22);
            this.cbHaltReason.Name = "cbHaltReason";
            this.cbHaltReason.Size = new System.Drawing.Size(261, 25);
            this.cbHaltReason.TabIndex = 15;
            // 
            // Label12
            // 
            this.Label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label12.Location = new System.Drawing.Point(275, 22);
            this.Label12.Name = "Label12";
            this.Label12.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label12.Size = new System.Drawing.Size(34, 20);
            this.Label12.TabIndex = 208;
            this.Label12.Text = "علت:";
            this.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label7
            // 
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label7.Location = new System.Drawing.Point(495, 22);
            this.Label7.Name = "Label7";
            this.Label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label7.Size = new System.Drawing.Size(59, 20);
            this.Label7.TabIndex = 206;
            this.Label7.Text = "طول زمان:";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtHaltTime
            // 
            this.txtHaltTime.CustomFormat = "HH:mm";
            this.txtHaltTime.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtHaltTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtHaltTime.Location = new System.Drawing.Point(435, 22);
            this.txtHaltTime.Name = "txtHaltTime";
            this.txtHaltTime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtHaltTime.RightToLeftLayout = true;
            this.txtHaltTime.ShowUpDown = true;
            this.txtHaltTime.Size = new System.Drawing.Size(56, 24);
            this.txtHaltTime.TabIndex = 14;
            this.txtHaltTime.Value = new System.DateTime(2008, 6, 5, 0, 0, 0, 0);
            // 
            // GroupBox4
            // 
            this.GroupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox4.Controls.Add(this.QtyLbl);
            this.GroupBox4.Controls.Add(this.cbGarbageOpUnits);
            this.GroupBox4.Controls.Add(this.txtGarbageFunStatistics);
            this.GroupBox4.Controls.Add(this._cmdCalcProductionQty);
            this.GroupBox4.Controls.Add(this._cmdNewOperationUnit);
            this.GroupBox4.Controls.Add(this.cbOperationUnits);
            this.GroupBox4.Controls.Add(this.StatUnitLbl);
            this.GroupBox4.Controls.Add(this.txtFunctionStatistics);
            this.GroupBox4.Controls.Add(this.FunctionStatisticsLbl);
            this.GroupBox4.Controls.Add(this.txtGarbageQty);
            this.GroupBox4.Controls.Add(this.Label9);
            this.GroupBox4.Controls.Add(this.txtIntactQty);
            this.GroupBox4.Controls.Add(this.Label8);
            this.GroupBox4.Location = new System.Drawing.Point(11, 197);
            this.GroupBox4.Name = "GroupBox4";
            this.GroupBox4.Size = new System.Drawing.Size(557, 108);
            this.GroupBox4.TabIndex = 8;
            this.GroupBox4.TabStop = false;
            this.GroupBox4.Text = "تعداد تولید";
            // 
            // QtyLbl
            // 
            this.QtyLbl.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.QtyLbl.ForeColor = System.Drawing.Color.Fuchsia;
            this.QtyLbl.Location = new System.Drawing.Point(39, 19);
            this.QtyLbl.Name = "QtyLbl";
            this.QtyLbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.QtyLbl.Size = new System.Drawing.Size(46, 20);
            this.QtyLbl.TabIndex = 219;
            this.QtyLbl.Text = "تعداد";
            this.QtyLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.QtyLbl.Visible = false;
            // 
            // cbGarbageOpUnits
            // 
            this.cbGarbageOpUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGarbageOpUnits.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbGarbageOpUnits.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cbGarbageOpUnits.FormattingEnabled = true;
            this.cbGarbageOpUnits.Location = new System.Drawing.Point(92, 79);
            this.cbGarbageOpUnits.Name = "cbGarbageOpUnits";
            this.cbGarbageOpUnits.Size = new System.Drawing.Size(149, 25);
            this.cbGarbageOpUnits.TabIndex = 12;
            this.cbGarbageOpUnits.Visible = false;
            // 
            // txtGarbageFunStatistics
            // 
            this.txtGarbageFunStatistics.DecimalPlaces = 2;
            this.txtGarbageFunStatistics.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtGarbageFunStatistics.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.txtGarbageFunStatistics.Location = new System.Drawing.Point(247, 79);
            this.txtGarbageFunStatistics.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtGarbageFunStatistics.Name = "txtGarbageFunStatistics";
            this.txtGarbageFunStatistics.Size = new System.Drawing.Size(75, 23);
            this.txtGarbageFunStatistics.TabIndex = 11;
            this.txtGarbageFunStatistics.ThousandsSeparator = true;
            this.txtGarbageFunStatistics.Visible = false;
            // 
            // _cmdCalcProductionQty
            // 
            this._cmdCalcProductionQty.BackColor = System.Drawing.Color.Transparent;
            this._cmdCalcProductionQty.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdCalcProductionQty.ForeColor = System.Drawing.Color.Black;
            this._cmdCalcProductionQty.Location = new System.Drawing.Point(9, 18);
            this._cmdCalcProductionQty.Name = "_cmdCalcProductionQty";
            this._cmdCalcProductionQty.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdCalcProductionQty.Size = new System.Drawing.Size(28, 24);
            this._cmdCalcProductionQty.TabIndex = 215;
            this._cmdCalcProductionQty.TabStop = false;
            this._cmdCalcProductionQty.UseVisualStyleBackColor = false;
            this._cmdCalcProductionQty.Visible = false;
            this._cmdCalcProductionQty.Click += new System.EventHandler(this.cmdCalcProductionQty_Click);
            // 
            // _cmdNewOperationUnit
            // 
            this._cmdNewOperationUnit.BackColor = System.Drawing.Color.Transparent;
            this._cmdNewOperationUnit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdNewOperationUnit.ForeColor = System.Drawing.Color.Black;
            this._cmdNewOperationUnit.Location = new System.Drawing.Point(91, 18);
            this._cmdNewOperationUnit.Name = "_cmdNewOperationUnit";
            this._cmdNewOperationUnit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdNewOperationUnit.Size = new System.Drawing.Size(28, 24);
            this._cmdNewOperationUnit.TabIndex = 216;
            this._cmdNewOperationUnit.TabStop = false;
            this._cmdNewOperationUnit.UseVisualStyleBackColor = false;
            this._cmdNewOperationUnit.Visible = false;
            this._cmdNewOperationUnit.Click += new System.EventHandler(this.cmdNewOperationUnit_Click);
            // 
            // cbOperationUnits
            // 
            this.cbOperationUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOperationUnits.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbOperationUnits.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cbOperationUnits.FormattingEnabled = true;
            this.cbOperationUnits.Location = new System.Drawing.Point(92, 45);
            this.cbOperationUnits.Name = "cbOperationUnits";
            this.cbOperationUnits.Size = new System.Drawing.Size(149, 25);
            this.cbOperationUnits.TabIndex = 9;
            this.cbOperationUnits.Visible = false;
            // 
            // StatUnitLbl
            // 
            this.StatUnitLbl.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.StatUnitLbl.ForeColor = System.Drawing.Color.Fuchsia;
            this.StatUnitLbl.Location = new System.Drawing.Point(126, 19);
            this.StatUnitLbl.Name = "StatUnitLbl";
            this.StatUnitLbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StatUnitLbl.Size = new System.Drawing.Size(115, 20);
            this.StatUnitLbl.TabIndex = 214;
            this.StatUnitLbl.Text = "واحد سنجش";
            this.StatUnitLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.StatUnitLbl.Visible = false;
            // 
            // txtFunctionStatistics
            // 
            this.txtFunctionStatistics.DecimalPlaces = 2;
            this.txtFunctionStatistics.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtFunctionStatistics.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.txtFunctionStatistics.Location = new System.Drawing.Point(247, 45);
            this.txtFunctionStatistics.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtFunctionStatistics.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtFunctionStatistics.Name = "txtFunctionStatistics";
            this.txtFunctionStatistics.Size = new System.Drawing.Size(75, 23);
            this.txtFunctionStatistics.TabIndex = 8;
            this.txtFunctionStatistics.ThousandsSeparator = true;
            this.txtFunctionStatistics.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtFunctionStatistics.Visible = false;
            // 
            // FunctionStatisticsLbl
            // 
            this.FunctionStatisticsLbl.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FunctionStatisticsLbl.ForeColor = System.Drawing.Color.Fuchsia;
            this.FunctionStatisticsLbl.Location = new System.Drawing.Point(249, 19);
            this.FunctionStatisticsLbl.Name = "FunctionStatisticsLbl";
            this.FunctionStatisticsLbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.FunctionStatisticsLbl.Size = new System.Drawing.Size(73, 20);
            this.FunctionStatisticsLbl.TabIndex = 212;
            this.FunctionStatisticsLbl.Text = "آمار کارکرد";
            this.FunctionStatisticsLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.FunctionStatisticsLbl.Visible = false;
            // 
            // txtGarbageQty
            // 
            this.txtGarbageQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtGarbageQty.Location = new System.Drawing.Point(9, 79);
            this.txtGarbageQty.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtGarbageQty.Name = "txtGarbageQty";
            this.txtGarbageQty.Size = new System.Drawing.Size(77, 23);
            this.txtGarbageQty.TabIndex = 13;
            this.txtGarbageQty.ThousandsSeparator = true;
            // 
            // Label9
            // 
            this.Label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label9.Location = new System.Drawing.Point(327, 79);
            this.Label9.Name = "Label9";
            this.Label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label9.Size = new System.Drawing.Size(227, 20);
            this.Label9.TabIndex = 210;
            this.Label9.Text = "تعداد تولید معیوب (غیر قابل انتقال به مرحله بعد)";
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIntactQty
            // 
            this.txtIntactQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtIntactQty.Location = new System.Drawing.Point(9, 45);
            this.txtIntactQty.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtIntactQty.Name = "txtIntactQty";
            this.txtIntactQty.Size = new System.Drawing.Size(77, 23);
            this.txtIntactQty.TabIndex = 10;
            this.txtIntactQty.ThousandsSeparator = true;
            // 
            // Label8
            // 
            this.Label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label8.Location = new System.Drawing.Point(327, 45);
            this.Label8.Name = "Label8";
            this.Label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label8.Size = new System.Drawing.Size(227, 20);
            this.Label8.TabIndex = 209;
            this.Label8.Text = "تعداد تولید سالم(قابل انتقال به مرحله بعد تولید)";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _cbOperators
            // 
            this._cbOperators.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cbOperators.ListAlternateColor = System.Drawing.Color.Beige;
            this._cbOperators.Location = new System.Drawing.Point(20, 312);
            this._cbOperators.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._cbOperators.MinimumSize = new System.Drawing.Size(82, 23);
            this._cbOperators.Name = "_cbOperators";
            this._cbOperators.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._cbOperators.SeletedValue = null;
            this._cbOperators.Size = new System.Drawing.Size(152, 23);
            this._cbOperators.TabIndex = 223;
            this._cbOperators.InputTextChanged += new PSBMultiColumnComboBox.PSBMultiColumnComboBox.InputTextChangedEventHandler(this.cbOperators_InputTextChanged);
            this._cbOperators.InputKeyDown += new PSBMultiColumnComboBox.PSBMultiColumnComboBox.InputKeyDownEventHandler(this.cbOperators_InputKeyDown);
            // 
            // _cbBatch
            // 
            this._cbBatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cbBatch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cbBatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._cbBatch.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cbBatch.FormattingEnabled = true;
            this._cbBatch.Location = new System.Drawing.Point(308, 99);
            this._cbBatch.Name = "_cbBatch";
            this._cbBatch.Size = new System.Drawing.Size(188, 25);
            this._cbBatch.TabIndex = 224;
            this._cbBatch.SelectedIndexChanged += new System.EventHandler(this.cbBatch_SelectedIndexChanged);
            // 
            // Label16
            // 
            this.Label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label16.Location = new System.Drawing.Point(502, 98);
            this.Label16.Name = "Label16";
            this.Label16.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label16.Size = new System.Drawing.Size(63, 20);
            this.Label16.TabIndex = 225;
            this.Label16.Text = "کد بچ تولید";
            this.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _cmdAddOperator
            // 
            this._cmdAddOperator.BackColor = System.Drawing.Color.Transparent;
            this._cmdAddOperator.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdAddOperator.ForeColor = System.Drawing.Color.Black;
            this._cmdAddOperator.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdAddOperator.Location = new System.Drawing.Point(178, 310);
            this._cmdAddOperator.Name = "_cmdAddOperator";
            this._cmdAddOperator.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdAddOperator.Size = new System.Drawing.Size(27, 27);
            this._cmdAddOperator.TabIndex = 12;
            this._cmdAddOperator.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._cmdAddOperator.UseVisualStyleBackColor = false;
            this._cmdAddOperator.Click += new System.EventHandler(this.cmdAddOperator_Click);
            // 
            // _cmdRemoveOperator
            // 
            this._cmdRemoveOperator.BackColor = System.Drawing.Color.Transparent;
            this._cmdRemoveOperator.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdRemoveOperator.ForeColor = System.Drawing.Color.Black;
            this._cmdRemoveOperator.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdRemoveOperator.Location = new System.Drawing.Point(217, 310);
            this._cmdRemoveOperator.Name = "_cmdRemoveOperator";
            this._cmdRemoveOperator.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdRemoveOperator.Size = new System.Drawing.Size(27, 27);
            this._cmdRemoveOperator.TabIndex = 10;
            this._cmdRemoveOperator.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._cmdRemoveOperator.UseVisualStyleBackColor = false;
            this._cmdRemoveOperator.Click += new System.EventHandler(this.cmdRemoveOperator_Click);
            // 
            // dgOperators
            // 
            this.dgOperators.AllowUserToAddRows = false;
            this.dgOperators.AllowUserToDeleteRows = false;
            this.dgOperators.AllowUserToResizeColumns = false;
            this.dgOperators.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.PowderBlue;
            this.dgOperators.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgOperators.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgOperators.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.dgOperators.ColumnHeadersHeight = 22;
            this.dgOperators.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgOperators.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colOperatorCode,
            this.colOperatorName});
            this.dgOperators.Location = new System.Drawing.Point(20, 339);
            this.dgOperators.Name = "dgOperators";
            this.dgOperators.ReadOnly = true;
            this.dgOperators.RowHeadersVisible = false;
            this.dgOperators.RowHeadersWidth = 30;
            this.dgOperators.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgOperators.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgOperators.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.dgOperators.RowTemplate.Height = 18;
            this.dgOperators.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgOperators.Size = new System.Drawing.Size(542, 90);
            this.dgOperators.TabIndex = 13;
            this.dgOperators.TabStop = false;
            // 
            // colOperatorCode
            // 
            this.colOperatorCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colOperatorCode.HeaderText = "کد اپراتور";
            this.colOperatorCode.MinimumWidth = 6;
            this.colOperatorCode.Name = "colOperatorCode";
            this.colOperatorCode.ReadOnly = true;
            this.colOperatorCode.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colOperatorCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colOperatorCode.Width = 64;
            // 
            // colOperatorName
            // 
            this.colOperatorName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colOperatorName.HeaderText = "نام اپراتور";
            this.colOperatorName.MinimumWidth = 310;
            this.colOperatorName.Name = "colOperatorName";
            this.colOperatorName.ReadOnly = true;
            this.colOperatorName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colOperatorName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Label1
            // 
            this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label1.Location = new System.Drawing.Point(472, 313);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label1.Size = new System.Drawing.Size(93, 20);
            this.Label1.TabIndex = 226;
            this.Label1.Text = "اپراتور(های) تولید";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Machin_Lookup
            // 
            this.Machin_Lookup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Machin_Lookup.AutoSize = true;
            this.Machin_Lookup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Machin_Lookup.CB_AutoComplete = false;
            this.Machin_Lookup.CB_AutoDropdown = false;
            this.Machin_Lookup.CB_ColumnNames = "";
            this.Machin_Lookup.CB_ColumnWidthDefault = 75;
            this.Machin_Lookup.CB_ColumnWidths = "75,150";
            this.Machin_Lookup.CB_DataSource = null;
            this.Machin_Lookup.CB_DisplayMember = "";
            this.Machin_Lookup.CB_LinkedColumnIndex = 0;
            this.Machin_Lookup.CB_SelectedIndex = -1;
            this.Machin_Lookup.CB_SelectedValue = "";
            this.Machin_Lookup.CB_SerachFromTitle = null;
            this.Machin_Lookup.CB_ValueMember = "";
            this.Machin_Lookup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Machin_Lookup.Location = new System.Drawing.Point(40, 165);
            this.Machin_Lookup.Name = "Machin_Lookup";
            this.Machin_Lookup.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Machin_Lookup.Size = new System.Drawing.Size(472, 33);
            this.Machin_Lookup.TabIndex = 227;
            // 
            // frmRealProduction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 548);
            this.Controls.Add(this.Machin_Lookup);
            this.Controls.Add(this.txtStartDate);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this._cmdRemoveOperator);
            this.Controls.Add(this._cmdAddOperator);
            this.Controls.Add(this._cbBatch);
            this.Controls.Add(this.Label16);
            this.Controls.Add(this._cbOperators);
            this.Controls.Add(this.GroupBox4);
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this._cbSubbatch);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this._cbOperation);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.dgOperators);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRealProduction";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " مشخصات تولید انجام شده";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRealProduction_FormClosing);
            this.Load += new System.EventHandler(this.frmRealProduction_Load);
            this.Panel1.ResumeLayout(false);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtGarbageFunStatistics)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFunctionStatistics)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGarbageQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIntactQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgOperators)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

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

        private ComboBox _cbSubbatch;

        internal ComboBox cbSubbatch
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cbSubbatch;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cbSubbatch != null)
                {
                    _cbSubbatch.SelectionChangeCommitted -= cbSubbatch_SelectionChangeCommitted;
                }

                _cbSubbatch = value;
                if (_cbSubbatch != null)
                {
                    _cbSubbatch.SelectionChangeCommitted += cbSubbatch_SelectionChangeCommitted;
                }
            }
        }

        internal Label Label4;
        private ComboBox _cbOperation;

        internal ComboBox cbOperation
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cbOperation;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cbOperation != null)
                {
                    _cbOperation.SelectionChangeCommitted -= cbOperation_SelectionChangeCommitted;
                }

                _cbOperation = value;
                if (_cbOperation != null)
                {
                    _cbOperation.SelectionChangeCommitted += cbOperation_SelectionChangeCommitted;
                }
            }
        }

        internal Label Label3;
        internal Label Label2;
        internal GroupBox GroupBox1;
        internal Label EndDate_Label;
        internal Label StartDate_Label;
        internal Label Label6;
        internal Label Label5;
        internal DateTimePicker txtEndHour;
        internal DateTimePicker txtStartHour;
        internal GroupBox GroupBox2;
        internal RadioButton rbOpType3;
        internal RadioButton rbOpType2;
        internal RadioButton rbOpType1;
        internal GroupBox GroupBox3;
        internal Label Label7;
        internal DateTimePicker txtHaltTime;
        internal ComboBox cbHaltReason;
        internal Label Label12;
        internal GroupBox GroupBox4;
        internal NumericUpDown txtGarbageQty;
        internal Label Label9;
        internal NumericUpDown txtIntactQty;
        internal Label Label8;
        internal NumericUpDown txtFunctionStatistics;
        internal Label FunctionStatisticsLbl;
        internal ComboBox cbOperationUnits;
        internal Label StatUnitLbl;
        private System.Windows.Forms.Button _cmdCalcProductionQty;

        internal System.Windows.Forms.Button cmdCalcProductionQty
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdCalcProductionQty;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdCalcProductionQty != null)
                {
                    _cmdCalcProductionQty.Click -= cmdCalcProductionQty_Click;
                }

                _cmdCalcProductionQty = value;
                if (_cmdCalcProductionQty != null)
                {
                    _cmdCalcProductionQty.Click += cmdCalcProductionQty_Click;
                }
            }
        }

        private System.Windows.Forms.Button _cmdNewOperationUnit;

        internal System.Windows.Forms.Button cmdNewOperationUnit
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdNewOperationUnit;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdNewOperationUnit != null)
                {
                    _cmdNewOperationUnit.Click -= cmdNewOperationUnit_Click;
                }

                _cmdNewOperationUnit = value;
                if (_cmdNewOperationUnit != null)
                {
                    _cmdNewOperationUnit.Click += cmdNewOperationUnit_Click;
                }
            }
        }

        internal ComboBox cbGarbageOpUnits;
        internal NumericUpDown txtGarbageFunStatistics;
        internal Label QtyLbl;
        internal PSB_FarsiDateControl.PSB_DateControl txtStartDate;
        internal PSB_FarsiDateControl.PSB_DateControl txtEndDate;
        private PSBMultiColumnComboBox.PSBMultiColumnComboBox _cbOperators;

        internal PSBMultiColumnComboBox.PSBMultiColumnComboBox cbOperators
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cbOperators;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cbOperators != null)
                {
                    _cbOperators.InputKeyDown -= cbOperators_InputKeyDown;
                    _cbOperators.InputTextChanged -= cbOperators_InputTextChanged;
                }

                _cbOperators = value;
                if (_cbOperators != null)
                {
                    _cbOperators.InputKeyDown += cbOperators_InputKeyDown;
                    _cbOperators.InputTextChanged += cbOperators_InputTextChanged;
                }
            }
        }

        private ComboBox _cbBatch;

        internal ComboBox cbBatch
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cbBatch;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cbBatch != null)
                {
                    _cbBatch.SelectedIndexChanged -= cbBatch_SelectedIndexChanged;
                }

                _cbBatch = value;
                if (_cbBatch != null)
                {
                    _cbBatch.SelectedIndexChanged += cbBatch_SelectedIndexChanged;
                }
            }
        }

        internal Label Label16;
        private System.Windows.Forms.Button _cmdAddOperator;

        internal System.Windows.Forms.Button cmdAddOperator
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdAddOperator;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdAddOperator != null)
                {
                    _cmdAddOperator.Click -= cmdAddOperator_Click;
                }

                _cmdAddOperator = value;
                if (_cmdAddOperator != null)
                {
                    _cmdAddOperator.Click += cmdAddOperator_Click;
                }
            }
        }

        private System.Windows.Forms.Button _cmdRemoveOperator;

        internal System.Windows.Forms.Button cmdRemoveOperator
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdRemoveOperator;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdRemoveOperator != null)
                {
                    _cmdRemoveOperator.Click -= cmdRemoveOperator_Click;
                }

                _cmdRemoveOperator = value;
                if (_cmdRemoveOperator != null)
                {
                    _cmdRemoveOperator.Click += cmdRemoveOperator_Click;
                }
            }
        }

        internal DataGridView dgOperators;
        internal DataGridViewTextBoxColumn colOperatorCode;
        internal DataGridViewTextBoxColumn colOperatorName;
        internal Label Label1;
        private Controls.UserControl_LookUp Machin_Lookup;
    }
}