namespace ProductionPlanning.Planning_Forms.OperatorTasks
{
    partial class frmOperatorTaskList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbConditions = new System.Windows.Forms.GroupBox();
            this.cbSubbatch_LookUp = new ProductionPlanning.Controls.UserControl_LookUp();
            this.cbBatch_Lookup = new ProductionPlanning.Controls.UserControl_LookUp();
            this.cbProduct_Lookup = new ProductionPlanning.Controls.UserControl_LookUp();
            this.Machine_Lookup = new ProductionPlanning.Controls.UserControl_LookUp();
            this.txtToDate = new PSB_FarsiDateControl.PSB_DateControl();
            this.txtFromDate = new PSB_FarsiDateControl.PSB_DateControl();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this._cmdShow = new System.Windows.Forms.Button();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.PlanningCodeTBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.OpMachine = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.OpCode = new System.Windows.Forms.TextBox();
            this.DeleteOperatorTask = new System.Windows.Forms.Button();
            this.EditOperatorTask = new System.Windows.Forms.Button();
            this.NewOperatorTask = new System.Windows.Forms.Button();
            this.dgOperators = new System.Windows.Forms.DataGridView();
            this.dgList = new System.Windows.Forms.DataGridView();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this._cmdExit = new System.Windows.Forms.Button();
            this.gbConditions.SuspendLayout();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOperators)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgList)).BeginInit();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbConditions
            // 
            this.gbConditions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbConditions.Controls.Add(this.cbSubbatch_LookUp);
            this.gbConditions.Controls.Add(this.cbBatch_Lookup);
            this.gbConditions.Controls.Add(this.cbProduct_Lookup);
            this.gbConditions.Controls.Add(this.Machine_Lookup);
            this.gbConditions.Controls.Add(this.txtToDate);
            this.gbConditions.Controls.Add(this.txtFromDate);
            this.gbConditions.Controls.Add(this.Label7);
            this.gbConditions.Controls.Add(this.Label6);
            this.gbConditions.Controls.Add(this.Label4);
            this.gbConditions.Controls.Add(this.Label5);
            this.gbConditions.Controls.Add(this.Label3);
            this.gbConditions.Controls.Add(this.Label1);
            this.gbConditions.Controls.Add(this.chkAll);
            this.gbConditions.Controls.Add(this._cmdShow);
            this.gbConditions.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.gbConditions.Location = new System.Drawing.Point(12, 14);
            this.gbConditions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbConditions.Name = "gbConditions";
            this.gbConditions.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbConditions.Size = new System.Drawing.Size(1347, 197);
            this.gbConditions.TabIndex = 4;
            this.gbConditions.TabStop = false;
            // 
            // cbSubbatch_LookUp
            // 
            this.cbSubbatch_LookUp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cbSubbatch_LookUp.CB_AutoComplete = true;
            this.cbSubbatch_LookUp.CB_AutoDropdown = false;
            this.cbSubbatch_LookUp.CB_ColumnNames = "";
            this.cbSubbatch_LookUp.CB_ColumnWidthDefault = 75;
            this.cbSubbatch_LookUp.CB_ColumnWidths = "75,250";
            this.cbSubbatch_LookUp.CB_DataSource = null;
            this.cbSubbatch_LookUp.CB_DisplayMember = "";
            this.cbSubbatch_LookUp.CB_LinkedColumnIndex = 1;
            this.cbSubbatch_LookUp.CB_SelectedIndex = -1;
            this.cbSubbatch_LookUp.CB_SelectedValue = "";
            this.cbSubbatch_LookUp.CB_SerachFromTitle = null;
            this.cbSubbatch_LookUp.CB_ValueMember = "";
            this.cbSubbatch_LookUp.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSubbatch_LookUp.Location = new System.Drawing.Point(684, 105);
            this.cbSubbatch_LookUp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbSubbatch_LookUp.Name = "cbSubbatch_LookUp";
            this.cbSubbatch_LookUp.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbSubbatch_LookUp.Size = new System.Drawing.Size(531, 38);
            this.cbSubbatch_LookUp.TabIndex = 194;
            // 
            // cbBatch_Lookup
            // 
            this.cbBatch_Lookup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cbBatch_Lookup.CB_AutoComplete = true;
            this.cbBatch_Lookup.CB_AutoDropdown = false;
            this.cbBatch_Lookup.CB_ColumnNames = "";
            this.cbBatch_Lookup.CB_ColumnWidthDefault = 75;
            this.cbBatch_Lookup.CB_ColumnWidths = "75,250";
            this.cbBatch_Lookup.CB_DataSource = null;
            this.cbBatch_Lookup.CB_DisplayMember = "";
            this.cbBatch_Lookup.CB_LinkedColumnIndex = 1;
            this.cbBatch_Lookup.CB_SelectedIndex = -1;
            this.cbBatch_Lookup.CB_SelectedValue = "";
            this.cbBatch_Lookup.CB_SerachFromTitle = null;
            this.cbBatch_Lookup.CB_ValueMember = "";
            this.cbBatch_Lookup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBatch_Lookup.Location = new System.Drawing.Point(683, 62);
            this.cbBatch_Lookup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbBatch_Lookup.Name = "cbBatch_Lookup";
            this.cbBatch_Lookup.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbBatch_Lookup.Size = new System.Drawing.Size(531, 38);
            this.cbBatch_Lookup.TabIndex = 193;
            // 
            // cbProduct_Lookup
            // 
            this.cbProduct_Lookup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cbProduct_Lookup.CB_AutoComplete = true;
            this.cbProduct_Lookup.CB_AutoDropdown = false;
            this.cbProduct_Lookup.CB_ColumnNames = "";
            this.cbProduct_Lookup.CB_ColumnWidthDefault = 75;
            this.cbProduct_Lookup.CB_ColumnWidths = "75,250";
            this.cbProduct_Lookup.CB_DataSource = null;
            this.cbProduct_Lookup.CB_DisplayMember = "";
            this.cbProduct_Lookup.CB_LinkedColumnIndex = 1;
            this.cbProduct_Lookup.CB_SelectedIndex = -1;
            this.cbProduct_Lookup.CB_SelectedValue = "";
            this.cbProduct_Lookup.CB_SerachFromTitle = null;
            this.cbProduct_Lookup.CB_ValueMember = "";
            this.cbProduct_Lookup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProduct_Lookup.Location = new System.Drawing.Point(684, 18);
            this.cbProduct_Lookup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbProduct_Lookup.Name = "cbProduct_Lookup";
            this.cbProduct_Lookup.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbProduct_Lookup.Size = new System.Drawing.Size(525, 38);
            this.cbProduct_Lookup.TabIndex = 192;
            // 
            // Machine_Lookup
            // 
            this.Machine_Lookup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Machine_Lookup.CB_AutoComplete = true;
            this.Machine_Lookup.CB_AutoDropdown = false;
            this.Machine_Lookup.CB_ColumnNames = "";
            this.Machine_Lookup.CB_ColumnWidthDefault = 75;
            this.Machine_Lookup.CB_ColumnWidths = "75,250";
            this.Machine_Lookup.CB_DataSource = null;
            this.Machine_Lookup.CB_DisplayMember = "";
            this.Machine_Lookup.CB_LinkedColumnIndex = 1;
            this.Machine_Lookup.CB_SelectedIndex = -1;
            this.Machine_Lookup.CB_SelectedValue = "";
            this.Machine_Lookup.CB_SerachFromTitle = null;
            this.Machine_Lookup.CB_ValueMember = "";
            this.Machine_Lookup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Machine_Lookup.Location = new System.Drawing.Point(684, 148);
            this.Machine_Lookup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Machine_Lookup.Name = "Machine_Lookup";
            this.Machine_Lookup.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Machine_Lookup.Size = new System.Drawing.Size(531, 38);
            this.Machine_Lookup.TabIndex = 191;
            // 
            // txtToDate
            // 
            this.txtToDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtToDate.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.txtToDate.BackColor = System.Drawing.Color.White;
            this.txtToDate.BackColorDateBox = System.Drawing.Color.White;
            this.txtToDate.DateButtonShow = true;
            this.txtToDate.EnableDateText = true;
            this.txtToDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtToDate.FontMyDate = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtToDate.Location = new System.Drawing.Point(264, 18);
            this.txtToDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtToDate.MinimumSize = new System.Drawing.Size(108, 30);
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.Size = new System.Drawing.Size(123, 30);
            this.txtToDate.TabIndex = 1;
            // 
            // txtFromDate
            // 
            this.txtFromDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFromDate.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.txtFromDate.BackColor = System.Drawing.Color.White;
            this.txtFromDate.BackColorDateBox = System.Drawing.Color.White;
            this.txtFromDate.DateButtonShow = true;
            this.txtFromDate.EnableDateText = true;
            this.txtFromDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtFromDate.FontMyDate = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtFromDate.Location = new System.Drawing.Point(465, 18);
            this.txtFromDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFromDate.MinimumSize = new System.Drawing.Size(108, 30);
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.Size = new System.Drawing.Size(123, 30);
            this.txtFromDate.TabIndex = 0;
            // 
            // Label7
            // 
            this.Label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label7.Location = new System.Drawing.Point(393, 23);
            this.Label7.Name = "Label7";
            this.Label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label7.Size = new System.Drawing.Size(51, 22);
            this.Label7.TabIndex = 188;
            this.Label7.Text = "تا تاریخ:";
            // 
            // Label6
            // 
            this.Label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label6.Location = new System.Drawing.Point(580, 23);
            this.Label6.Name = "Label6";
            this.Label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label6.Size = new System.Drawing.Size(51, 22);
            this.Label6.TabIndex = 186;
            this.Label6.Text = "از تاریخ:";
            // 
            // Label4
            // 
            this.Label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label4.Location = new System.Drawing.Point(1220, 105);
            this.Label4.Name = "Label4";
            this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label4.Size = new System.Drawing.Size(111, 22);
            this.Label4.TabIndex = 174;
            this.Label4.Text = "ساب بچ تولید:";
            // 
            // Label5
            // 
            this.Label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label5.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label5.Location = new System.Drawing.Point(1244, 63);
            this.Label5.Name = "Label5";
            this.Label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label5.Size = new System.Drawing.Size(87, 22);
            this.Label5.TabIndex = 172;
            this.Label5.Text = "بچ تولید:";
            // 
            // Label3
            // 
            this.Label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label3.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label3.Location = new System.Drawing.Point(1244, 148);
            this.Label3.Name = "Label3";
            this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label3.Size = new System.Drawing.Size(77, 28);
            this.Label3.TabIndex = 170;
            this.Label3.Text = "ماشین:";
            // 
            // Label1
            // 
            this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label1.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label1.Location = new System.Drawing.Point(1249, 26);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label1.Size = new System.Drawing.Size(77, 22);
            this.Label1.TabIndex = 166;
            this.Label1.Text = "محصول:";
            // 
            // chkAll
            // 
            this.chkAll.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.chkAll.Location = new System.Drawing.Point(32, 23);
            this.chkAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(136, 30);
            this.chkAll.TabIndex = 8;
            this.chkAll.Text = "نمایش همه";
            this.chkAll.UseVisualStyleBackColor = true;
            // 
            // _cmdShow
            // 
            this._cmdShow.BackColor = System.Drawing.Color.Transparent;
            this._cmdShow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdShow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdShow.Location = new System.Drawing.Point(32, 66);
            this._cmdShow.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._cmdShow.Name = "_cmdShow";
            this._cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdShow.Size = new System.Drawing.Size(136, 33);
            this._cmdShow.TabIndex = 7;
            this._cmdShow.Text = "نمایش";
            this._cmdShow.UseVisualStyleBackColor = false;
            this._cmdShow.Click += new System.EventHandler(this._cmdShow_Click);
            // 
            // Panel1
            // 
            this.Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Panel1.Controls.Add(this.PlanningCodeTBox);
            this.Panel1.Controls.Add(this.label9);
            this.Panel1.Controls.Add(this.OpMachine);
            this.Panel1.Controls.Add(this.label10);
            this.Panel1.Controls.Add(this.OpCode);
            this.Panel1.Controls.Add(this.DeleteOperatorTask);
            this.Panel1.Controls.Add(this.EditOperatorTask);
            this.Panel1.Controls.Add(this.NewOperatorTask);
            this.Panel1.Controls.Add(this.dgOperators);
            this.Panel1.Controls.Add(this.dgList);
            this.Panel1.Controls.Add(this.GroupBox1);
            this.Panel1.Location = new System.Drawing.Point(12, 218);
            this.Panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(1349, 467);
            this.Panel1.TabIndex = 5;
            // 
            // PlanningCodeTBox
            // 
            this.PlanningCodeTBox.BackColor = System.Drawing.SystemColors.Info;
            this.PlanningCodeTBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.PlanningCodeTBox.Location = new System.Drawing.Point(897, 374);
            this.PlanningCodeTBox.Margin = new System.Windows.Forms.Padding(4);
            this.PlanningCodeTBox.Name = "PlanningCodeTBox";
            this.PlanningCodeTBox.Size = new System.Drawing.Size(122, 24);
            this.PlanningCodeTBox.TabIndex = 325;
            this.PlanningCodeTBox.Visible = false;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label9.Location = new System.Drawing.Point(1241, 277);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label9.Size = new System.Drawing.Size(87, 27);
            this.label9.TabIndex = 324;
            this.label9.Text = "ماشین:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OpMachine
            // 
            this.OpMachine.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.OpMachine.Location = new System.Drawing.Point(897, 278);
            this.OpMachine.Margin = new System.Windows.Forms.Padding(4);
            this.OpMachine.Name = "OpMachine";
            this.OpMachine.Size = new System.Drawing.Size(340, 24);
            this.OpMachine.TabIndex = 323;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label10.Location = new System.Drawing.Point(1241, 245);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label10.Size = new System.Drawing.Size(87, 27);
            this.label10.TabIndex = 322;
            this.label10.Text = "کد عملیات:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OpCode
            // 
            this.OpCode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.OpCode.Location = new System.Drawing.Point(897, 246);
            this.OpCode.Margin = new System.Windows.Forms.Padding(4);
            this.OpCode.Name = "OpCode";
            this.OpCode.Size = new System.Drawing.Size(340, 24);
            this.OpCode.TabIndex = 321;
            // 
            // DeleteOperatorTask
            // 
            this.DeleteOperatorTask.BackColor = System.Drawing.Color.Transparent;
            this.DeleteOperatorTask.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.DeleteOperatorTask.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DeleteOperatorTask.Location = new System.Drawing.Point(785, 369);
            this.DeleteOperatorTask.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DeleteOperatorTask.Name = "DeleteOperatorTask";
            this.DeleteOperatorTask.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.DeleteOperatorTask.Size = new System.Drawing.Size(84, 33);
            this.DeleteOperatorTask.TabIndex = 17;
            this.DeleteOperatorTask.Text = "حذف";
            this.DeleteOperatorTask.UseVisualStyleBackColor = false;
            this.DeleteOperatorTask.Click += new System.EventHandler(this.DeleteOperatorTask_Click);
            // 
            // EditOperatorTask
            // 
            this.EditOperatorTask.BackColor = System.Drawing.Color.Transparent;
            this.EditOperatorTask.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.EditOperatorTask.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.EditOperatorTask.Location = new System.Drawing.Point(785, 308);
            this.EditOperatorTask.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.EditOperatorTask.Name = "EditOperatorTask";
            this.EditOperatorTask.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.EditOperatorTask.Size = new System.Drawing.Size(84, 33);
            this.EditOperatorTask.TabIndex = 16;
            this.EditOperatorTask.Text = "اصلاح";
            this.EditOperatorTask.UseVisualStyleBackColor = false;
            this.EditOperatorTask.Click += new System.EventHandler(this.EditOperatorTask_Click);
            // 
            // NewOperatorTask
            // 
            this.NewOperatorTask.BackColor = System.Drawing.Color.Transparent;
            this.NewOperatorTask.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.NewOperatorTask.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.NewOperatorTask.Location = new System.Drawing.Point(785, 246);
            this.NewOperatorTask.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NewOperatorTask.Name = "NewOperatorTask";
            this.NewOperatorTask.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.NewOperatorTask.Size = new System.Drawing.Size(84, 33);
            this.NewOperatorTask.TabIndex = 15;
            this.NewOperatorTask.Text = "جدید";
            this.NewOperatorTask.UseVisualStyleBackColor = false;
            this.NewOperatorTask.Click += new System.EventHandler(this.NewOperatorTask_Click);
            // 
            // dgOperators
            // 
            this.dgOperators.AllowUserToAddRows = false;
            this.dgOperators.AllowUserToDeleteRows = false;
            this.dgOperators.AllowUserToResizeColumns = false;
            this.dgOperators.AllowUserToResizeRows = false;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.PowderBlue;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgOperators.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgOperators.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgOperators.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgOperators.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgOperators.ColumnHeadersHeight = 30;
            this.dgOperators.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgOperators.Location = new System.Drawing.Point(7, 246);
            this.dgOperators.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgOperators.Name = "dgOperators";
            this.dgOperators.ReadOnly = true;
            this.dgOperators.RowHeadersVisible = false;
            this.dgOperators.RowHeadersWidth = 30;
            this.dgOperators.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgOperators.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgOperators.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.dgOperators.RowTemplate.Height = 18;
            this.dgOperators.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgOperators.Size = new System.Drawing.Size(748, 156);
            this.dgOperators.TabIndex = 14;
            this.dgOperators.TabStop = false;
            // 
            // dgList
            // 
            this.dgList.AllowUserToAddRows = false;
            this.dgList.AllowUserToDeleteRows = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            this.dgList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgList.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgList.ColumnHeadersHeight = 50;
            this.dgList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgList.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgList.Location = new System.Drawing.Point(7, 4);
            this.dgList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgList.MultiSelect = false;
            this.dgList.Name = "dgList";
            this.dgList.ReadOnly = true;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgList.RowHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dgList.RowHeadersWidth = 60;
            this.dgList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this.dgList.RowsDefaultCellStyle = dataGridViewCellStyle14;
            this.dgList.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgList.Size = new System.Drawing.Size(1336, 226);
            this.dgList.TabIndex = 1;
            this.dgList.TabStop = false;
            this.dgList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgList_CellClick);
            this.dgList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgList_CellContentClick);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.Controls.Add(this._cmdExit);
            this.GroupBox1.Location = new System.Drawing.Point(7, 411);
            this.GroupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GroupBox1.Size = new System.Drawing.Size(1335, 48);
            this.GroupBox1.TabIndex = 2;
            this.GroupBox1.TabStop = false;
            // 
            // _cmdExit
            // 
            this._cmdExit.BackColor = System.Drawing.Color.Transparent;
            this._cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cmdExit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdExit.Location = new System.Drawing.Point(8, 14);
            this._cmdExit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._cmdExit.Name = "_cmdExit";
            this._cmdExit.Size = new System.Drawing.Size(120, 30);
            this._cmdExit.TabIndex = 10;
            this._cmdExit.Text = "خروج";
            this._cmdExit.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._cmdExit.UseVisualStyleBackColor = false;
            this._cmdExit.Click += new System.EventHandler(this._cmdExit_Click);
            // 
            // frmOperatorTaskList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1375, 690);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.gbConditions);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmOperatorTaskList";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "اختصاص فعالیت به اپراتور ";
            this.Load += new System.EventHandler(this.frmOperatorTaskList_Load);
            this.gbConditions.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOperators)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgList)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox gbConditions;
        private Controls.UserControl_LookUp cbSubbatch_LookUp;
        private Controls.UserControl_LookUp cbBatch_Lookup;
        private Controls.UserControl_LookUp cbProduct_Lookup;
        private Controls.UserControl_LookUp Machine_Lookup;
        internal PSB_FarsiDateControl.PSB_DateControl txtToDate;
        internal PSB_FarsiDateControl.PSB_DateControl txtFromDate;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.Button _cmdShow;
        internal System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.DataGridView dgList;
        internal System.Windows.Forms.GroupBox GroupBox1;
        private System.Windows.Forms.Button _cmdExit;
        internal System.Windows.Forms.DataGridView dgOperators;
        private System.Windows.Forms.Button DeleteOperatorTask;
        private System.Windows.Forms.Button EditOperatorTask;
        private System.Windows.Forms.Button NewOperatorTask;
        internal System.Windows.Forms.Label label9;
        internal System.Windows.Forms.TextBox OpMachine;
        internal System.Windows.Forms.Label label10;
        internal System.Windows.Forms.TextBox OpCode;
        internal System.Windows.Forms.TextBox PlanningCodeTBox;
    }
}