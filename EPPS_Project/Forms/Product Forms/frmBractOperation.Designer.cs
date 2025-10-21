using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmBractOperation : Form
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
            this.Panel11 = new System.Windows.Forms.Panel();
            this._cmdSelectMatchedOperations = new System.Windows.Forms.Button();
            this._chkIsHasMatchedOperations = new System.Windows.Forms.CheckBox();
            this.chkIsParallelOperation = new System.Windows.Forms.CheckBox();
            this.chkIsNotCalcWorking = new System.Windows.Forms.CheckBox();
            this.lblPriority = new System.Windows.Forms.Label();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.txtGruopCode = new System.Windows.Forms.TextBox();
            this.cbNature = new System.Windows.Forms.ComboBox();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.cbTitle = new System.Windows.Forms.ComboBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.Panel8 = new System.Windows.Forms.Panel();
            this.cbContractorCalendar = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtContractorProductionCapacity = new System.Windows.Forms.NumericUpDown();
            this.Label23 = new System.Windows.Forms.Label();
            this.txtActivityTime = new System.Windows.Forms.NumericUpDown();
            this.Label22 = new System.Windows.Forms.Label();
            this.Label20 = new System.Windows.Forms.Label();
            this.Panel10 = new System.Windows.Forms.Panel();
            this._rbCSenond = new System.Windows.Forms.RadioButton();
            this._rbCMinute = new System.Windows.Forms.RadioButton();
            this._rbCDay = new System.Windows.Forms.RadioButton();
            this._rbCHour = new System.Windows.Forms.RadioButton();
            this._rbCMonth = new System.Windows.Forms.RadioButton();
            this._rbCWeek = new System.Windows.Forms.RadioButton();
            this.txtMimimumBatch = new System.Windows.Forms.NumericUpDown();
            this.Label16 = new System.Windows.Forms.Label();
            this.cbContractor = new System.Windows.Forms.ComboBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Panel7 = new System.Windows.Forms.Panel();
            this.cbCalendar = new System.Windows.Forms.ComboBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Panel12 = new System.Windows.Forms.Panel();
            this._rbSecond = new System.Windows.Forms.RadioButton();
            this._rbMinute = new System.Windows.Forms.RadioButton();
            this._rbDay = new System.Windows.Forms.RadioButton();
            this._rbHour = new System.Windows.Forms.RadioButton();
            this._rbMonth = new System.Windows.Forms.RadioButton();
            this._rbWeek = new System.Windows.Forms.RadioButton();
            this.txtOneExecutionPrice = new System.Windows.Forms.TextBox();
            this.Label27 = new System.Windows.Forms.Label();
            this.txtRedoPercent = new System.Windows.Forms.NumericUpDown();
            this.Label14 = new System.Windows.Forms.Label();
            this.txtGarbagePercent = new System.Windows.Forms.NumericUpDown();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label19 = new System.Windows.Forms.Label();
            this.Label18 = new System.Windows.Forms.Label();
            this.txtMaximumAccumulation = new System.Windows.Forms.NumericUpDown();
            this.Label7 = new System.Windows.Forms.Label();
            this.txtMinimumProduction = new System.Windows.Forms.NumericUpDown();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtOneExecutionTime = new System.Windows.Forms.NumericUpDown();
            this.Label21 = new System.Windows.Forms.Label();
            this.txtOperator = new System.Windows.Forms.NumericUpDown();
            this.Label25 = new System.Windows.Forms.Label();
            this.pnlExecMethod = new System.Windows.Forms.Panel();
            this._rbMachine = new System.Windows.Forms.RadioButton();
            this._rbNonMachine = new System.Windows.Forms.RadioButton();
            this._rbContractor = new System.Windows.Forms.RadioButton();
            this.Panel1 = new System.Windows.Forms.Panel();
            this._cmdSave = new System.Windows.Forms.Button();
            this._cmdExit = new System.Windows.Forms.Button();
            this.Panel11.SuspendLayout();
            this.Panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtContractorProductionCapacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtActivityTime)).BeginInit();
            this.Panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMimimumBatch)).BeginInit();
            this.Panel7.SuspendLayout();
            this.Panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRedoPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGarbagePercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaximumAccumulation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinimumProduction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOneExecutionTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperator)).BeginInit();
            this.pnlExecMethod.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel11
            // 
            this.Panel11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel11.Controls.Add(this._cmdSelectMatchedOperations);
            this.Panel11.Controls.Add(this._chkIsHasMatchedOperations);
            this.Panel11.Controls.Add(this.chkIsParallelOperation);
            this.Panel11.Controls.Add(this.chkIsNotCalcWorking);
            this.Panel11.Controls.Add(this.lblPriority);
            this.Panel11.Controls.Add(this.Label13);
            this.Panel11.Controls.Add(this.Label11);
            this.Panel11.Controls.Add(this.txtGruopCode);
            this.Panel11.Controls.Add(this.cbNature);
            this.Panel11.Controls.Add(this.Label10);
            this.Panel11.Controls.Add(this.Label9);
            this.Panel11.Controls.Add(this.cbTitle);
            this.Panel11.Controls.Add(this.Label8);
            this.Panel11.Controls.Add(this.Label4);
            this.Panel11.Controls.Add(this.txtCode);
            this.Panel11.Controls.Add(this.Panel8);
            this.Panel11.Controls.Add(this.Panel7);
            this.Panel11.Controls.Add(this.pnlExecMethod);
            this.Panel11.Location = new System.Drawing.Point(6, 11);
            this.Panel11.Name = "Panel11";
            this.Panel11.Size = new System.Drawing.Size(700, 167);
            this.Panel11.TabIndex = 0;
            // 
            // _cmdSelectMatchedOperations
            // 
            this._cmdSelectMatchedOperations.BackColor = System.Drawing.Color.Transparent;
            this._cmdSelectMatchedOperations.Enabled = false;
            this._cmdSelectMatchedOperations.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdSelectMatchedOperations.ForeColor = System.Drawing.Color.Black;
            this._cmdSelectMatchedOperations.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdSelectMatchedOperations.Location = new System.Drawing.Point(10, 79);
            this._cmdSelectMatchedOperations.Name = "_cmdSelectMatchedOperations";
            this._cmdSelectMatchedOperations.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdSelectMatchedOperations.Size = new System.Drawing.Size(129, 26);
            this._cmdSelectMatchedOperations.TabIndex = 8;
            this._cmdSelectMatchedOperations.Text = "انتخاب عملیات های همزمان";
            this._cmdSelectMatchedOperations.UseVisualStyleBackColor = false;
            this._cmdSelectMatchedOperations.Visible = false;
            this._cmdSelectMatchedOperations.Click += new System.EventHandler(this.cmdSelectMatchedOperations_Click);
            // 
            // _chkIsHasMatchedOperations
            // 
            this._chkIsHasMatchedOperations.AutoSize = true;
            this._chkIsHasMatchedOperations.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._chkIsHasMatchedOperations.ForeColor = System.Drawing.Color.Blue;
            this._chkIsHasMatchedOperations.Location = new System.Drawing.Point(141, 83);
            this._chkIsHasMatchedOperations.Name = "_chkIsHasMatchedOperations";
            this._chkIsHasMatchedOperations.Size = new System.Drawing.Size(233, 17);
            this._chkIsHasMatchedOperations.TabIndex = 7;
            this._chkIsHasMatchedOperations.Text = "این عملیات امکان اجرای همزمان داشته باشد";
            this._chkIsHasMatchedOperations.UseVisualStyleBackColor = true;
            this._chkIsHasMatchedOperations.Visible = false;
            this._chkIsHasMatchedOperations.CheckedChanged += new System.EventHandler(this.chkIsHasMatchedOperations_CheckedChanged);
            // 
            // chkIsParallelOperation
            // 
            this.chkIsParallelOperation.AutoSize = true;
            this.chkIsParallelOperation.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.chkIsParallelOperation.ForeColor = System.Drawing.Color.Blue;
            this.chkIsParallelOperation.Location = new System.Drawing.Point(149, 141);
            this.chkIsParallelOperation.Name = "chkIsParallelOperation";
            this.chkIsParallelOperation.Size = new System.Drawing.Size(225, 17);
            this.chkIsParallelOperation.TabIndex = 10;
            this.chkIsParallelOperation.Text = "این عملیات امکان اجرای موازی داشته باشد";
            this.chkIsParallelOperation.UseVisualStyleBackColor = true;
            this.chkIsParallelOperation.Visible = false;
            // 
            // chkIsNotCalcWorking
            // 
            this.chkIsNotCalcWorking.AutoSize = true;
            this.chkIsNotCalcWorking.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.chkIsNotCalcWorking.ForeColor = System.Drawing.Color.Blue;
            this.chkIsNotCalcWorking.Location = new System.Drawing.Point(129, 111);
            this.chkIsNotCalcWorking.Name = "chkIsNotCalcWorking";
            this.chkIsNotCalcWorking.Size = new System.Drawing.Size(245, 17);
            this.chkIsNotCalcWorking.TabIndex = 9;
            this.chkIsNotCalcWorking.Text = "این عملیات در محاسبات کارکرد اپراتور لحاظ نشود";
            this.chkIsNotCalcWorking.UseVisualStyleBackColor = true;
            this.chkIsNotCalcWorking.Visible = false;
            // 
            // lblPriority
            // 
            this.lblPriority.BackColor = System.Drawing.SystemColors.Control;
            this.lblPriority.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblPriority.ForeColor = System.Drawing.Color.Blue;
            this.lblPriority.Location = new System.Drawing.Point(10, 48);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblPriority.Size = new System.Drawing.Size(24, 19);
            this.lblPriority.TabIndex = 325;
            this.lblPriority.Text = "#";
            this.lblPriority.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPriority.Visible = false;
            // 
            // Label13
            // 
            this.Label13.Font = new System.Drawing.Font("Tahoma", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label13.Location = new System.Drawing.Point(36, 48);
            this.Label13.Name = "Label13";
            this.Label13.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label13.Size = new System.Drawing.Size(43, 19);
            this.Label13.TabIndex = 324;
            this.Label13.Text = "الویت اجرا:";
            this.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label13.Visible = false;
            // 
            // Label11
            // 
            this.Label11.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label11.Location = new System.Drawing.Point(572, 115);
            this.Label11.Name = "Label11";
            this.Label11.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label11.Size = new System.Drawing.Size(119, 19);
            this.Label11.TabIndex = 322;
            this.Label11.Text = "کد گروه بندی عملیات:";
            this.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtGruopCode
            // 
            this.txtGruopCode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtGruopCode.Location = new System.Drawing.Point(389, 114);
            this.txtGruopCode.Name = "txtGruopCode";
            this.txtGruopCode.Size = new System.Drawing.Size(181, 21);
            this.txtGruopCode.TabIndex = 6;
            // 
            // cbNature
            // 
            this.cbNature.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNature.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbNature.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cbNature.FormattingEnabled = true;
            this.cbNature.Location = new System.Drawing.Point(389, 81);
            this.cbNature.Name = "cbNature";
            this.cbNature.Size = new System.Drawing.Size(234, 21);
            this.cbNature.TabIndex = 5;
            // 
            // Label10
            // 
            this.Label10.BackColor = System.Drawing.SystemColors.Control;
            this.Label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label10.ForeColor = System.Drawing.Color.Black;
            this.Label10.Location = new System.Drawing.Point(626, 80);
            this.Label10.Name = "Label10";
            this.Label10.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label10.Size = new System.Drawing.Size(65, 22);
            this.Label10.TabIndex = 321;
            this.Label10.Text = "بخش تولید:";
            this.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label9
            // 
            this.Label9.BackColor = System.Drawing.SystemColors.Control;
            this.Label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label9.ForeColor = System.Drawing.Color.Black;
            this.Label9.Location = new System.Drawing.Point(626, 47);
            this.Label9.Name = "Label9";
            this.Label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label9.Size = new System.Drawing.Size(57, 21);
            this.Label9.TabIndex = 316;
            this.Label9.Text = "نحوۀ اجرا:";
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbTitle
            // 
            this.cbTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cbTitle.FormattingEnabled = true;
            this.cbTitle.Location = new System.Drawing.Point(10, 13);
            this.cbTitle.Name = "cbTitle";
            this.cbTitle.Size = new System.Drawing.Size(286, 21);
            this.cbTitle.TabIndex = 1;
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.SystemColors.Control;
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label8.ForeColor = System.Drawing.Color.Black;
            this.Label8.Location = new System.Drawing.Point(299, 13);
            this.Label8.Name = "Label8";
            this.Label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label8.Size = new System.Drawing.Size(79, 20);
            this.Label8.TabIndex = 315;
            this.Label8.Text = "عنوان عملیات:";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label4
            // 
            this.Label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label4.Location = new System.Drawing.Point(626, 12);
            this.Label4.Name = "Label4";
            this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label4.Size = new System.Drawing.Size(65, 22);
            this.Label4.TabIndex = 312;
            this.Label4.Text = "کد عملیات:";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtCode.Location = new System.Drawing.Point(389, 13);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(234, 21);
            this.txtCode.TabIndex = 0;
            // 
            // Panel8
            // 
            this.Panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel8.Controls.Add(this.cbContractorCalendar);
            this.Panel8.Controls.Add(this.Label2);
            this.Panel8.Controls.Add(this.txtContractorProductionCapacity);
            this.Panel8.Controls.Add(this.Label23);
            this.Panel8.Controls.Add(this.txtActivityTime);
            this.Panel8.Controls.Add(this.Label22);
            this.Panel8.Controls.Add(this.Label20);
            this.Panel8.Controls.Add(this.Panel10);
            this.Panel8.Controls.Add(this.txtMimimumBatch);
            this.Panel8.Controls.Add(this.Label16);
            this.Panel8.Controls.Add(this.cbContractor);
            this.Panel8.Controls.Add(this.Label6);
            this.Panel8.Location = new System.Drawing.Point(10, 166);
            this.Panel8.Name = "Panel8";
            this.Panel8.Size = new System.Drawing.Size(678, 131);
            this.Panel8.TabIndex = 2;
            this.Panel8.Visible = false;
            // 
            // cbContractorCalendar
            // 
            this.cbContractorCalendar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbContractorCalendar.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbContractorCalendar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cbContractorCalendar.FormattingEnabled = true;
            this.cbContractorCalendar.ItemHeight = 13;
            this.cbContractorCalendar.Location = new System.Drawing.Point(9, 15);
            this.cbContractorCalendar.Name = "cbContractorCalendar";
            this.cbContractorCalendar.Size = new System.Drawing.Size(225, 21);
            this.cbContractorCalendar.TabIndex = 321;
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.SystemColors.Control;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label2.ForeColor = System.Drawing.Color.Black;
            this.Label2.Location = new System.Drawing.Point(237, 17);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label2.Size = new System.Drawing.Size(61, 17);
            this.Label2.TabIndex = 322;
            this.Label2.Text = "تقویم کاری";
            // 
            // txtContractorProductionCapacity
            // 
            this.txtContractorProductionCapacity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContractorProductionCapacity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtContractorProductionCapacity.Location = new System.Drawing.Point(94, 54);
            this.txtContractorProductionCapacity.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtContractorProductionCapacity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtContractorProductionCapacity.Name = "txtContractorProductionCapacity";
            this.txtContractorProductionCapacity.Size = new System.Drawing.Size(77, 21);
            this.txtContractorProductionCapacity.TabIndex = 33;
            this.txtContractorProductionCapacity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Label23
            // 
            this.Label23.BackColor = System.Drawing.SystemColors.Control;
            this.Label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label23.ForeColor = System.Drawing.Color.Black;
            this.Label23.Location = new System.Drawing.Point(175, 54);
            this.Label23.Name = "Label23";
            this.Label23.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label23.Size = new System.Drawing.Size(124, 21);
            this.Label23.TabIndex = 311;
            this.Label23.Text = "تعداد محصول در دسته:";
            this.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtActivityTime
            // 
            this.txtActivityTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtActivityTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtActivityTime.Location = new System.Drawing.Point(403, 90);
            this.txtActivityTime.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtActivityTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtActivityTime.Name = "txtActivityTime";
            this.txtActivityTime.Size = new System.Drawing.Size(82, 21);
            this.txtActivityTime.TabIndex = 32;
            this.txtActivityTime.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Label22
            // 
            this.Label22.BackColor = System.Drawing.SystemColors.Control;
            this.Label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label22.ForeColor = System.Drawing.Color.Black;
            this.Label22.Location = new System.Drawing.Point(491, 90);
            this.Label22.Name = "Label22";
            this.Label22.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label22.Size = new System.Drawing.Size(179, 21);
            this.Label22.TabIndex = 309;
            this.Label22.Text = "زمان انجام فعالیت بروی یک دسته:";
            this.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label20
            // 
            this.Label20.BackColor = System.Drawing.SystemColors.Control;
            this.Label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label20.ForeColor = System.Drawing.Color.Black;
            this.Label20.Location = new System.Drawing.Point(308, 90);
            this.Label20.Name = "Label20";
            this.Label20.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label20.Size = new System.Drawing.Size(87, 21);
            this.Label20.TabIndex = 308;
            this.Label20.Text = "نوع واحد زمانی:";
            this.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Panel10
            // 
            this.Panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel10.Controls.Add(this._rbCSenond);
            this.Panel10.Controls.Add(this._rbCMinute);
            this.Panel10.Controls.Add(this._rbCDay);
            this.Panel10.Controls.Add(this._rbCHour);
            this.Panel10.Controls.Add(this._rbCMonth);
            this.Panel10.Controls.Add(this._rbCWeek);
            this.Panel10.Location = new System.Drawing.Point(9, 87);
            this.Panel10.Name = "Panel10";
            this.Panel10.Size = new System.Drawing.Size(296, 28);
            this.Panel10.TabIndex = 307;
            // 
            // _rbCSenond
            // 
            this._rbCSenond.AutoSize = true;
            this._rbCSenond.Checked = true;
            this._rbCSenond.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._rbCSenond.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._rbCSenond.Location = new System.Drawing.Point(249, 4);
            this._rbCSenond.Name = "_rbCSenond";
            this._rbCSenond.Size = new System.Drawing.Size(43, 17);
            this._rbCSenond.TabIndex = 39;
            this._rbCSenond.TabStop = true;
            this._rbCSenond.Text = "ثانیه";
            this._rbCSenond.UseVisualStyleBackColor = true;
            this._rbCSenond.CheckedChanged += new System.EventHandler(this.rbCSecond_CheckedChanged);
            // 
            // _rbCMinute
            // 
            this._rbCMinute.AutoSize = true;
            this._rbCMinute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._rbCMinute.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._rbCMinute.Location = new System.Drawing.Point(195, 4);
            this._rbCMinute.Name = "_rbCMinute";
            this._rbCMinute.Size = new System.Drawing.Size(50, 17);
            this._rbCMinute.TabIndex = 34;
            this._rbCMinute.Text = "دقیقه";
            this._rbCMinute.UseVisualStyleBackColor = true;
            this._rbCMinute.CheckedChanged += new System.EventHandler(this.rbCMinute_CheckedChanged);
            // 
            // _rbCDay
            // 
            this._rbCDay.AutoSize = true;
            this._rbCDay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._rbCDay.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._rbCDay.Location = new System.Drawing.Point(96, 4);
            this._rbCDay.Name = "_rbCDay";
            this._rbCDay.Size = new System.Drawing.Size(37, 17);
            this._rbCDay.TabIndex = 36;
            this._rbCDay.Text = "روز";
            this._rbCDay.UseVisualStyleBackColor = true;
            this._rbCDay.CheckedChanged += new System.EventHandler(this.rbCDay_CheckedChanged);
            // 
            // _rbCHour
            // 
            this._rbCHour.AutoSize = true;
            this._rbCHour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._rbCHour.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._rbCHour.Location = new System.Drawing.Point(137, 4);
            this._rbCHour.Name = "_rbCHour";
            this._rbCHour.Size = new System.Drawing.Size(55, 17);
            this._rbCHour.TabIndex = 35;
            this._rbCHour.Text = "ساعت";
            this._rbCHour.UseVisualStyleBackColor = true;
            this._rbCHour.CheckedChanged += new System.EventHandler(this.rbCHour_CheckedChanged);
            // 
            // _rbCMonth
            // 
            this._rbCMonth.AutoSize = true;
            this._rbCMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._rbCMonth.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._rbCMonth.Location = new System.Drawing.Point(3, 4);
            this._rbCMonth.Name = "_rbCMonth";
            this._rbCMonth.Size = new System.Drawing.Size(38, 17);
            this._rbCMonth.TabIndex = 38;
            this._rbCMonth.Text = "ماه";
            this._rbCMonth.UseVisualStyleBackColor = true;
            this._rbCMonth.CheckedChanged += new System.EventHandler(this.rbCMonth_CheckedChanged);
            // 
            // _rbCWeek
            // 
            this._rbCWeek.AutoSize = true;
            this._rbCWeek.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._rbCWeek.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._rbCWeek.Location = new System.Drawing.Point(44, 4);
            this._rbCWeek.Name = "_rbCWeek";
            this._rbCWeek.Size = new System.Drawing.Size(48, 17);
            this._rbCWeek.TabIndex = 37;
            this._rbCWeek.Text = "هفته";
            this._rbCWeek.UseVisualStyleBackColor = true;
            this._rbCWeek.CheckedChanged += new System.EventHandler(this.rbCWeek_CheckedChanged);
            // 
            // txtMimimumBatch
            // 
            this.txtMimimumBatch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMimimumBatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtMimimumBatch.Location = new System.Drawing.Point(350, 54);
            this.txtMimimumBatch.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtMimimumBatch.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtMimimumBatch.Name = "txtMimimumBatch";
            this.txtMimimumBatch.Size = new System.Drawing.Size(68, 21);
            this.txtMimimumBatch.TabIndex = 31;
            this.txtMimimumBatch.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Label16
            // 
            this.Label16.BackColor = System.Drawing.SystemColors.Control;
            this.Label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label16.ForeColor = System.Drawing.Color.Black;
            this.Label16.Location = new System.Drawing.Point(424, 54);
            this.Label16.Name = "Label16";
            this.Label16.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label16.Size = new System.Drawing.Size(246, 21);
            this.Label16.TabIndex = 305;
            this.Label16.Text = "حداقل تعداد دسته لازم جهت ارسال به پیمانکار:";
            this.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbContractor
            // 
            this.cbContractor.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbContractor.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cbContractor.FormattingEnabled = true;
            this.cbContractor.Location = new System.Drawing.Point(306, 15);
            this.cbContractor.Name = "cbContractor";
            this.cbContractor.Size = new System.Drawing.Size(296, 21);
            this.cbContractor.TabIndex = 30;
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.SystemColors.Control;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label6.ForeColor = System.Drawing.Color.Black;
            this.Label6.Location = new System.Drawing.Point(605, 15);
            this.Label6.Name = "Label6";
            this.Label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label6.Size = new System.Drawing.Size(65, 20);
            this.Label6.TabIndex = 304;
            this.Label6.Text = "نام پیمانکار:";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Panel7
            // 
            this.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel7.Controls.Add(this.cbCalendar);
            this.Panel7.Controls.Add(this.Label5);
            this.Panel7.Controls.Add(this.Panel12);
            this.Panel7.Controls.Add(this.txtOneExecutionPrice);
            this.Panel7.Controls.Add(this.Label27);
            this.Panel7.Controls.Add(this.txtRedoPercent);
            this.Panel7.Controls.Add(this.Label14);
            this.Panel7.Controls.Add(this.txtGarbagePercent);
            this.Panel7.Controls.Add(this.Label12);
            this.Panel7.Controls.Add(this.Label19);
            this.Panel7.Controls.Add(this.Label18);
            this.Panel7.Controls.Add(this.txtMaximumAccumulation);
            this.Panel7.Controls.Add(this.Label7);
            this.Panel7.Controls.Add(this.txtMinimumProduction);
            this.Panel7.Controls.Add(this.Label3);
            this.Panel7.Controls.Add(this.txtOneExecutionTime);
            this.Panel7.Controls.Add(this.Label21);
            this.Panel7.Controls.Add(this.txtOperator);
            this.Panel7.Controls.Add(this.Label25);
            this.Panel7.Location = new System.Drawing.Point(10, 166);
            this.Panel7.Name = "Panel7";
            this.Panel7.Size = new System.Drawing.Size(678, 156);
            this.Panel7.TabIndex = 1;
            this.Panel7.Visible = false;
            // 
            // cbCalendar
            // 
            this.cbCalendar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCalendar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCalendar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cbCalendar.FormattingEnabled = true;
            this.cbCalendar.ItemHeight = 13;
            this.cbCalendar.Location = new System.Drawing.Point(13, 79);
            this.cbCalendar.Name = "cbCalendar";
            this.cbCalendar.Size = new System.Drawing.Size(232, 21);
            this.cbCalendar.TabIndex = 319;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.SystemColors.Control;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label5.ForeColor = System.Drawing.Color.Black;
            this.Label5.Location = new System.Drawing.Point(262, 81);
            this.Label5.Name = "Label5";
            this.Label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label5.Size = new System.Drawing.Size(115, 17);
            this.Label5.TabIndex = 320;
            this.Label5.Text = "تقویم کاری";
            // 
            // Panel12
            // 
            this.Panel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel12.Controls.Add(this._rbSecond);
            this.Panel12.Controls.Add(this._rbMinute);
            this.Panel12.Controls.Add(this._rbDay);
            this.Panel12.Controls.Add(this._rbHour);
            this.Panel12.Controls.Add(this._rbMonth);
            this.Panel12.Controls.Add(this._rbWeek);
            this.Panel12.Location = new System.Drawing.Point(13, 12);
            this.Panel12.Name = "Panel12";
            this.Panel12.Size = new System.Drawing.Size(287, 25);
            this.Panel12.TabIndex = 318;
            // 
            // _rbSecond
            // 
            this._rbSecond.AutoSize = true;
            this._rbSecond.Checked = true;
            this._rbSecond.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._rbSecond.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._rbSecond.Location = new System.Drawing.Point(239, 3);
            this._rbSecond.Name = "_rbSecond";
            this._rbSecond.Size = new System.Drawing.Size(43, 17);
            this._rbSecond.TabIndex = 12;
            this._rbSecond.TabStop = true;
            this._rbSecond.Text = "ثانیه";
            this._rbSecond.UseVisualStyleBackColor = true;
            this._rbSecond.CheckedChanged += new System.EventHandler(this.rbSecond_CheckedChanged);
            // 
            // _rbMinute
            // 
            this._rbMinute.AutoSize = true;
            this._rbMinute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._rbMinute.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._rbMinute.Location = new System.Drawing.Point(186, 3);
            this._rbMinute.Name = "_rbMinute";
            this._rbMinute.Size = new System.Drawing.Size(50, 17);
            this._rbMinute.TabIndex = 7;
            this._rbMinute.Text = "دقیقه";
            this._rbMinute.UseVisualStyleBackColor = true;
            this._rbMinute.CheckedChanged += new System.EventHandler(this.rbMinute_CheckedChanged);
            // 
            // _rbDay
            // 
            this._rbDay.AutoSize = true;
            this._rbDay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._rbDay.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._rbDay.Location = new System.Drawing.Point(91, 3);
            this._rbDay.Name = "_rbDay";
            this._rbDay.Size = new System.Drawing.Size(37, 17);
            this._rbDay.TabIndex = 9;
            this._rbDay.Text = "روز";
            this._rbDay.UseVisualStyleBackColor = true;
            this._rbDay.CheckedChanged += new System.EventHandler(this.rbDay_CheckedChanged);
            // 
            // _rbHour
            // 
            this._rbHour.AutoSize = true;
            this._rbHour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._rbHour.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._rbHour.Location = new System.Drawing.Point(129, 3);
            this._rbHour.Name = "_rbHour";
            this._rbHour.Size = new System.Drawing.Size(55, 17);
            this._rbHour.TabIndex = 8;
            this._rbHour.Text = "ساعت";
            this._rbHour.UseVisualStyleBackColor = true;
            this._rbHour.CheckedChanged += new System.EventHandler(this.rbHour_CheckedChanged);
            // 
            // _rbMonth
            // 
            this._rbMonth.AutoSize = true;
            this._rbMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._rbMonth.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._rbMonth.Location = new System.Drawing.Point(1, 3);
            this._rbMonth.Name = "_rbMonth";
            this._rbMonth.Size = new System.Drawing.Size(38, 17);
            this._rbMonth.TabIndex = 11;
            this._rbMonth.Text = "ماه";
            this._rbMonth.UseVisualStyleBackColor = true;
            this._rbMonth.CheckedChanged += new System.EventHandler(this.rbMonth_CheckedChanged);
            // 
            // _rbWeek
            // 
            this._rbWeek.AutoSize = true;
            this._rbWeek.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._rbWeek.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._rbWeek.Location = new System.Drawing.Point(41, 3);
            this._rbWeek.Name = "_rbWeek";
            this._rbWeek.Size = new System.Drawing.Size(48, 17);
            this._rbWeek.TabIndex = 10;
            this._rbWeek.Text = "هفته";
            this._rbWeek.UseVisualStyleBackColor = true;
            this._rbWeek.CheckedChanged += new System.EventHandler(this.rbWeek_CheckedChanged);
            // 
            // txtOneExecutionPrice
            // 
            this.txtOneExecutionPrice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOneExecutionPrice.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtOneExecutionPrice.Location = new System.Drawing.Point(86, 115);
            this.txtOneExecutionPrice.Multiline = true;
            this.txtOneExecutionPrice.Name = "txtOneExecutionPrice";
            this.txtOneExecutionPrice.Size = new System.Drawing.Size(158, 18);
            this.txtOneExecutionPrice.TabIndex = 29;
            // 
            // Label27
            // 
            this.Label27.BackColor = System.Drawing.SystemColors.Control;
            this.Label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label27.ForeColor = System.Drawing.Color.Black;
            this.Label27.Location = new System.Drawing.Point(244, 115);
            this.Label27.Name = "Label27";
            this.Label27.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label27.Size = new System.Drawing.Size(133, 17);
            this.Label27.TabIndex = 317;
            this.Label27.Text = "هزینه یکبار اجرای عملیات";
            // 
            // txtRedoPercent
            // 
            this.txtRedoPercent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRedoPercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtRedoPercent.Location = new System.Drawing.Point(471, 115);
            this.txtRedoPercent.Name = "txtRedoPercent";
            this.txtRedoPercent.Size = new System.Drawing.Size(50, 17);
            this.txtRedoPercent.TabIndex = 28;
            // 
            // Label14
            // 
            this.Label14.BackColor = System.Drawing.SystemColors.Control;
            this.Label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label14.ForeColor = System.Drawing.Color.Black;
            this.Label14.Location = new System.Drawing.Point(570, 115);
            this.Label14.Name = "Label14";
            this.Label14.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label14.Size = new System.Drawing.Size(100, 17);
            this.Label14.TabIndex = 312;
            this.Label14.Text = "درصد دوباره کاری";
            // 
            // txtGarbagePercent
            // 
            this.txtGarbagePercent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtGarbagePercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtGarbagePercent.Location = new System.Drawing.Point(471, 81);
            this.txtGarbagePercent.Name = "txtGarbagePercent";
            this.txtGarbagePercent.Size = new System.Drawing.Size(50, 17);
            this.txtGarbagePercent.TabIndex = 27;
            // 
            // Label12
            // 
            this.Label12.BackColor = System.Drawing.SystemColors.Control;
            this.Label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label12.ForeColor = System.Drawing.Color.Black;
            this.Label12.Location = new System.Drawing.Point(552, 81);
            this.Label12.Name = "Label12";
            this.Label12.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label12.Size = new System.Drawing.Size(118, 17);
            this.Label12.TabIndex = 308;
            this.Label12.Text = "درصد ضایعات عملیات";
            // 
            // Label19
            // 
            this.Label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label19.Location = new System.Drawing.Point(455, 115);
            this.Label19.Name = "Label19";
            this.Label19.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label19.Size = new System.Drawing.Size(18, 17);
            this.Label19.TabIndex = 316;
            this.Label19.Text = "%";
            // 
            // Label18
            // 
            this.Label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label18.Location = new System.Drawing.Point(455, 81);
            this.Label18.Name = "Label18";
            this.Label18.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label18.Size = new System.Drawing.Size(18, 17);
            this.Label18.TabIndex = 315;
            this.Label18.Text = "%";
            // 
            // txtMaximumAccumulation
            // 
            this.txtMaximumAccumulation.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMaximumAccumulation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtMaximumAccumulation.Location = new System.Drawing.Point(150, 50);
            this.txtMaximumAccumulation.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtMaximumAccumulation.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtMaximumAccumulation.Name = "txtMaximumAccumulation";
            this.txtMaximumAccumulation.Size = new System.Drawing.Size(94, 17);
            this.txtMaximumAccumulation.TabIndex = 26;
            this.txtMaximumAccumulation.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.SystemColors.Control;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label7.ForeColor = System.Drawing.Color.Black;
            this.Label7.Location = new System.Drawing.Point(255, 50);
            this.Label7.Name = "Label7";
            this.Label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label7.Size = new System.Drawing.Size(122, 17);
            this.Label7.TabIndex = 304;
            this.Label7.Text = "حداکثر تعداد انباشتگی";
            // 
            // txtMinimumProduction
            // 
            this.txtMinimumProduction.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMinimumProduction.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtMinimumProduction.Location = new System.Drawing.Point(430, 50);
            this.txtMinimumProduction.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtMinimumProduction.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtMinimumProduction.Name = "txtMinimumProduction";
            this.txtMinimumProduction.Size = new System.Drawing.Size(94, 17);
            this.txtMinimumProduction.TabIndex = 25;
            this.txtMinimumProduction.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.SystemColors.Control;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label3.ForeColor = System.Drawing.Color.Black;
            this.Label3.Location = new System.Drawing.Point(530, 50);
            this.Label3.Name = "Label3";
            this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label3.Size = new System.Drawing.Size(140, 17);
            this.Label3.TabIndex = 302;
            this.Label3.Text = "حداقل تعداد تولید اقتصادی";
            // 
            // txtOneExecutionTime
            // 
            this.txtOneExecutionTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOneExecutionTime.DecimalPlaces = 2;
            this.txtOneExecutionTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtOneExecutionTime.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.txtOneExecutionTime.Location = new System.Drawing.Point(304, 16);
            this.txtOneExecutionTime.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtOneExecutionTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.txtOneExecutionTime.Name = "txtOneExecutionTime";
            this.txtOneExecutionTime.Size = new System.Drawing.Size(66, 17);
            this.txtOneExecutionTime.TabIndex = 24;
            this.txtOneExecutionTime.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Label21
            // 
            this.Label21.BackColor = System.Drawing.SystemColors.Control;
            this.Label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label21.ForeColor = System.Drawing.Color.Black;
            this.Label21.Location = new System.Drawing.Point(376, 16);
            this.Label21.Name = "Label21";
            this.Label21.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label21.Size = new System.Drawing.Size(123, 17);
            this.Label21.TabIndex = 297;
            this.Label21.Text = "زمان انجام یکبار عملیات";
            // 
            // txtOperator
            // 
            this.txtOperator.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOperator.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtOperator.Location = new System.Drawing.Point(530, 16);
            this.txtOperator.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtOperator.Name = "txtOperator";
            this.txtOperator.Size = new System.Drawing.Size(71, 17);
            this.txtOperator.TabIndex = 23;
            this.txtOperator.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Label25
            // 
            this.Label25.BackColor = System.Drawing.SystemColors.Control;
            this.Label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label25.ForeColor = System.Drawing.Color.Black;
            this.Label25.Location = new System.Drawing.Point(607, 16);
            this.Label25.Name = "Label25";
            this.Label25.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label25.Size = new System.Drawing.Size(63, 17);
            this.Label25.TabIndex = 265;
            this.Label25.Text = "تعداد اپراتور";
            // 
            // pnlExecMethod
            // 
            this.pnlExecMethod.BackColor = System.Drawing.Color.White;
            this.pnlExecMethod.Controls.Add(this._rbMachine);
            this.pnlExecMethod.Controls.Add(this._rbNonMachine);
            this.pnlExecMethod.Controls.Add(this._rbContractor);
            this.pnlExecMethod.Location = new System.Drawing.Point(10, 46);
            this.pnlExecMethod.Name = "pnlExecMethod";
            this.pnlExecMethod.Size = new System.Drawing.Size(613, 23);
            this.pnlExecMethod.TabIndex = 334;
            // 
            // _rbMachine
            // 
            this._rbMachine.AutoSize = true;
            this._rbMachine.BackColor = System.Drawing.Color.White;
            this._rbMachine.Checked = true;
            this._rbMachine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._rbMachine.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._rbMachine.Location = new System.Drawing.Point(456, 3);
            this._rbMachine.Name = "_rbMachine";
            this._rbMachine.Size = new System.Drawing.Size(154, 17);
            this._rbMachine.TabIndex = 2;
            this._rbMachine.TabStop = true;
            this._rbMachine.Text = "اجرای عملیات توسط ماشین";
            this._rbMachine.UseVisualStyleBackColor = false;
            this._rbMachine.CheckedChanged += new System.EventHandler(this.rbMachine_CheckedChanged);
            // 
            // _rbNonMachine
            // 
            this._rbNonMachine.AutoSize = true;
            this._rbNonMachine.BackColor = System.Drawing.Color.White;
            this._rbNonMachine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._rbNonMachine.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._rbNonMachine.Location = new System.Drawing.Point(218, 3);
            this._rbNonMachine.Name = "_rbNonMachine";
            this._rbNonMachine.Size = new System.Drawing.Size(180, 17);
            this._rbNonMachine.TabIndex = 3;
            this._rbNonMachine.Text = "اجرای عملیات  نیاز به ماشین ندارد";
            this._rbNonMachine.UseVisualStyleBackColor = false;
            this._rbNonMachine.CheckedChanged += new System.EventHandler(this.rbNonMachine_CheckedChanged);
            // 
            // _rbContractor
            // 
            this._rbContractor.AutoSize = true;
            this._rbContractor.BackColor = System.Drawing.Color.White;
            this._rbContractor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._rbContractor.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._rbContractor.Location = new System.Drawing.Point(93, 3);
            this._rbContractor.Name = "_rbContractor";
            this._rbContractor.Size = new System.Drawing.Size(67, 17);
            this._rbContractor.TabIndex = 4;
            this._rbContractor.Text = "پیمانکاری";
            this._rbContractor.UseVisualStyleBackColor = false;
            this._rbContractor.CheckedChanged += new System.EventHandler(this.rbContractor_CheckedChanged);
            // 
            // Panel1
            // 
            this.Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this._cmdSave);
            this.Panel1.Controls.Add(this._cmdExit);
            this.Panel1.Location = new System.Drawing.Point(6, 189);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(700, 48);
            this.Panel1.TabIndex = 3;
            // 
            // _cmdSave
            // 
            this._cmdSave.BackColor = System.Drawing.Color.Transparent;
            this._cmdSave.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdSave.ForeColor = System.Drawing.Color.Black;
            this._cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdSave.Location = new System.Drawing.Point(366, 10);
            this._cmdSave.Name = "_cmdSave";
            this._cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdSave.Size = new System.Drawing.Size(90, 28);
            this._cmdSave.TabIndex = 0;
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
            this._cmdExit.Location = new System.Drawing.Point(243, 10);
            this._cmdExit.Name = "_cmdExit";
            this._cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdExit.Size = new System.Drawing.Size(90, 28);
            this._cmdExit.TabIndex = 1;
            this._cmdExit.Text = "خروج";
            this._cmdExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._cmdExit.UseVisualStyleBackColor = false;
            this._cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // frmBractOperation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 243);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Panel11);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Location = new System.Drawing.Point(50, 50);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBractOperation";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " مشخصات عملیات ساخت  محصول";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBractOperations_FormClosing);
            this.Load += new System.EventHandler(this.frmBractOperations_Load);
            this.Panel11.ResumeLayout(false);
            this.Panel11.PerformLayout();
            this.Panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtContractorProductionCapacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtActivityTime)).EndInit();
            this.Panel10.ResumeLayout(false);
            this.Panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMimimumBatch)).EndInit();
            this.Panel7.ResumeLayout(false);
            this.Panel7.PerformLayout();
            this.Panel12.ResumeLayout(false);
            this.Panel12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRedoPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGarbagePercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaximumAccumulation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinimumProduction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOneExecutionTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperator)).EndInit();
            this.pnlExecMethod.ResumeLayout(false);
            this.pnlExecMethod.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        internal System.Windows.Forms.Panel Panel11;
        internal Label lblPriority;
        internal Label Label13;
        internal Label Label11;
        internal TextBox txtGruopCode;
        internal ComboBox cbNature;
        internal Label Label10;
        private RadioButton _rbContractor;

        internal RadioButton rbContractor
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _rbContractor;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_rbContractor != null)
                {
                    _rbContractor.CheckedChanged -= rbContractor_CheckedChanged;
                }

                _rbContractor = value;
                if (_rbContractor != null)
                {
                    _rbContractor.CheckedChanged += rbContractor_CheckedChanged;
                }
            }
        }

        private RadioButton _rbNonMachine;

        internal RadioButton rbNonMachine
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _rbNonMachine;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_rbNonMachine != null)
                {
                    _rbNonMachine.CheckedChanged -= rbNonMachine_CheckedChanged;
                }

                _rbNonMachine = value;
                if (_rbNonMachine != null)
                {
                    _rbNonMachine.CheckedChanged += rbNonMachine_CheckedChanged;
                }
            }
        }

        private RadioButton _rbMachine;

        internal RadioButton rbMachine
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _rbMachine;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_rbMachine != null)
                {
                    _rbMachine.CheckedChanged -= rbMachine_CheckedChanged;
                }

                _rbMachine = value;
                if (_rbMachine != null)
                {
                    _rbMachine.CheckedChanged += rbMachine_CheckedChanged;
                }
            }
        }

        internal Label Label9;
        internal ComboBox cbTitle;
        internal Label Label8;
        internal Label Label4;
        internal TextBox txtCode;
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

        internal System.Windows.Forms.Panel Panel7;
        internal ComboBox cbCalendar;
        internal Label Label5;
        internal System.Windows.Forms.Panel Panel12;
        private RadioButton _rbSecond;

        internal RadioButton rbSecond
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _rbSecond;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_rbSecond != null)
                {
                    _rbSecond.CheckedChanged -= rbSecond_CheckedChanged;
                }

                _rbSecond = value;
                if (_rbSecond != null)
                {
                    _rbSecond.CheckedChanged += rbSecond_CheckedChanged;
                }
            }
        }

        private RadioButton _rbMinute;

        internal RadioButton rbMinute
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _rbMinute;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_rbMinute != null)
                {
                    _rbMinute.CheckedChanged -= rbMinute_CheckedChanged;
                }

                _rbMinute = value;
                if (_rbMinute != null)
                {
                    _rbMinute.CheckedChanged += rbMinute_CheckedChanged;
                }
            }
        }

        private RadioButton _rbDay;

        internal RadioButton rbDay
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _rbDay;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_rbDay != null)
                {
                    _rbDay.CheckedChanged -= rbDay_CheckedChanged;
                }

                _rbDay = value;
                if (_rbDay != null)
                {
                    _rbDay.CheckedChanged += rbDay_CheckedChanged;
                }
            }
        }

        private RadioButton _rbHour;

        internal RadioButton rbHour
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _rbHour;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_rbHour != null)
                {
                    _rbHour.CheckedChanged -= rbHour_CheckedChanged;
                }

                _rbHour = value;
                if (_rbHour != null)
                {
                    _rbHour.CheckedChanged += rbHour_CheckedChanged;
                }
            }
        }

        private RadioButton _rbMonth;

        internal RadioButton rbMonth
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _rbMonth;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_rbMonth != null)
                {
                    _rbMonth.CheckedChanged -= rbMonth_CheckedChanged;
                }

                _rbMonth = value;
                if (_rbMonth != null)
                {
                    _rbMonth.CheckedChanged += rbMonth_CheckedChanged;
                }
            }
        }

        private RadioButton _rbWeek;

        internal RadioButton rbWeek
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _rbWeek;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_rbWeek != null)
                {
                    _rbWeek.CheckedChanged -= rbWeek_CheckedChanged;
                }

                _rbWeek = value;
                if (_rbWeek != null)
                {
                    _rbWeek.CheckedChanged += rbWeek_CheckedChanged;
                }
            }
        }

        internal TextBox txtOneExecutionPrice;
        internal Label Label27;
        internal NumericUpDown txtRedoPercent;
        internal Label Label14;
        internal NumericUpDown txtGarbagePercent;
        internal Label Label12;
        internal Label Label19;
        internal Label Label18;
        internal NumericUpDown txtMaximumAccumulation;
        internal Label Label7;
        internal NumericUpDown txtMinimumProduction;
        internal Label Label3;
        internal NumericUpDown txtOneExecutionTime;
        internal Label Label21;
        internal NumericUpDown txtOperator;
        internal Label Label25;
        internal System.Windows.Forms.Panel Panel8;
        internal NumericUpDown txtContractorProductionCapacity;
        internal Label Label23;
        internal NumericUpDown txtActivityTime;
        internal Label Label22;
        internal Label Label20;
        internal System.Windows.Forms.Panel Panel10;
        private RadioButton _rbCSenond;

        internal RadioButton rbCSenond
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _rbCSenond;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_rbCSenond != null)
                {
                    _rbCSenond.CheckedChanged -= rbCSecond_CheckedChanged;
                }

                _rbCSenond = value;
                if (_rbCSenond != null)
                {
                    _rbCSenond.CheckedChanged += rbCSecond_CheckedChanged;
                }
            }
        }

        private RadioButton _rbCMinute;

        internal RadioButton rbCMinute
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _rbCMinute;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_rbCMinute != null)
                {
                    _rbCMinute.CheckedChanged -= rbCMinute_CheckedChanged;
                }

                _rbCMinute = value;
                if (_rbCMinute != null)
                {
                    _rbCMinute.CheckedChanged += rbCMinute_CheckedChanged;
                }
            }
        }

        private RadioButton _rbCDay;

        internal RadioButton rbCDay
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _rbCDay;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_rbCDay != null)
                {
                    _rbCDay.CheckedChanged -= rbCDay_CheckedChanged;
                }

                _rbCDay = value;
                if (_rbCDay != null)
                {
                    _rbCDay.CheckedChanged += rbCDay_CheckedChanged;
                }
            }
        }

        private RadioButton _rbCHour;

        internal RadioButton rbCHour
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _rbCHour;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_rbCHour != null)
                {
                    _rbCHour.CheckedChanged -= rbCHour_CheckedChanged;
                }

                _rbCHour = value;
                if (_rbCHour != null)
                {
                    _rbCHour.CheckedChanged += rbCHour_CheckedChanged;
                }
            }
        }

        private RadioButton _rbCMonth;

        internal RadioButton rbCMonth
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _rbCMonth;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_rbCMonth != null)
                {
                    _rbCMonth.CheckedChanged -= rbCMonth_CheckedChanged;
                }

                _rbCMonth = value;
                if (_rbCMonth != null)
                {
                    _rbCMonth.CheckedChanged += rbCMonth_CheckedChanged;
                }
            }
        }

        private RadioButton _rbCWeek;

        internal RadioButton rbCWeek
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _rbCWeek;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_rbCWeek != null)
                {
                    _rbCWeek.CheckedChanged -= rbCWeek_CheckedChanged;
                }

                _rbCWeek = value;
                if (_rbCWeek != null)
                {
                    _rbCWeek.CheckedChanged += rbCWeek_CheckedChanged;
                }
            }
        }

        internal NumericUpDown txtMimimumBatch;
        internal Label Label16;
        internal ComboBox cbContractor;
        internal Label Label6;
        internal ComboBox cbContractorCalendar;
        internal Label Label2;
        internal CheckBox chkIsNotCalcWorking;
        internal CheckBox chkIsParallelOperation;
        private CheckBox _chkIsHasMatchedOperations;

        internal CheckBox chkIsHasMatchedOperations
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _chkIsHasMatchedOperations;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_chkIsHasMatchedOperations != null)
                {
                    _chkIsHasMatchedOperations.CheckedChanged -= chkIsHasMatchedOperations_CheckedChanged;
                }

                _chkIsHasMatchedOperations = value;
                if (_chkIsHasMatchedOperations != null)
                {
                    _chkIsHasMatchedOperations.CheckedChanged += chkIsHasMatchedOperations_CheckedChanged;
                }
            }
        }

        private System.Windows.Forms.Button _cmdSelectMatchedOperations;

        internal System.Windows.Forms.Button cmdSelectMatchedOperations
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdSelectMatchedOperations;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdSelectMatchedOperations != null)
                {
                    _cmdSelectMatchedOperations.Click -= cmdSelectMatchedOperations_Click;
                }

                _cmdSelectMatchedOperations = value;
                if (_cmdSelectMatchedOperations != null)
                {
                    _cmdSelectMatchedOperations.Click += cmdSelectMatchedOperations_Click;
                }
            }
        }

        internal System.Windows.Forms.Panel pnlExecMethod;
    }
}