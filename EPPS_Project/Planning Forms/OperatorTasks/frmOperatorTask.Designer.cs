namespace ProductionPlanning.Planning_Forms.OperatorTasks
{
    partial class frmOperatorTask
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
            this.Panel1 = new System.Windows.Forms.Panel();
            this.cmdSave = new System.Windows.Forms.Button();
            this._cmdExit = new System.Windows.Forms.Button();
            this._cmdDelete = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.EndDateTBox = new PSB_FarsiDateControl.PSB_DateControl();
            this.label7 = new System.Windows.Forms.Label();
            this.Operator_Lookup = new ProductionPlanning.Controls.UserControl_LookUp();
            this.label3 = new System.Windows.Forms.Label();
            this.MachineTBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BatchTBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ProductTBox = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.OperationTBox = new System.Windows.Forms.TextBox();
            this.StartDateTBox = new PSB_FarsiDateControl.PSB_DateControl();
            this.Date_Label = new System.Windows.Forms.Label();
            this.EndHourTBox = new System.Windows.Forms.DateTimePicker();
            this.StartHourTBox = new System.Windows.Forms.DateTimePicker();
            this.Panel1.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this.cmdSave);
            this.Panel1.Controls.Add(this._cmdExit);
            this.Panel1.Controls.Add(this._cmdDelete);
            this.Panel1.ForeColor = System.Drawing.Color.Black;
            this.Panel1.Location = new System.Drawing.Point(12, 330);
            this.Panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(767, 62);
            this.Panel1.TabIndex = 17;
            // 
            // cmdSave
            // 
            this.cmdSave.BackColor = System.Drawing.Color.Transparent;
            this.cmdSave.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cmdSave.ForeColor = System.Drawing.Color.Black;
            this.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSave.Location = new System.Drawing.Point(569, 13);
            this.cmdSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdSave.Size = new System.Drawing.Size(101, 37);
            this.cmdSave.TabIndex = 16;
            this.cmdSave.Text = "ثبت";
            this.cmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdSave.UseVisualStyleBackColor = false;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // _cmdExit
            // 
            this._cmdExit.BackColor = System.Drawing.Color.Transparent;
            this._cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cmdExit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdExit.ForeColor = System.Drawing.Color.Red;
            this._cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdExit.Location = new System.Drawing.Point(96, 13);
            this._cmdExit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._cmdExit.Name = "_cmdExit";
            this._cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdExit.Size = new System.Drawing.Size(101, 37);
            this._cmdExit.TabIndex = 17;
            this._cmdExit.Text = "انصراف";
            this._cmdExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._cmdExit.UseVisualStyleBackColor = false;
            // 
            // _cmdDelete
            // 
            this._cmdDelete.BackColor = System.Drawing.Color.Transparent;
            this._cmdDelete.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._cmdDelete.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdDelete.ForeColor = System.Drawing.Color.Black;
            this._cmdDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdDelete.Location = new System.Drawing.Point(476, 13);
            this._cmdDelete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._cmdDelete.Name = "_cmdDelete";
            this._cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdDelete.Size = new System.Drawing.Size(101, 37);
            this._cmdDelete.TabIndex = 18;
            this._cmdDelete.Text = "حذف";
            this._cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._cmdDelete.UseVisualStyleBackColor = false;
            this._cmdDelete.Visible = false;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.label8);
            this.GroupBox1.Controls.Add(this.EndDateTBox);
            this.GroupBox1.Controls.Add(this.label7);
            this.GroupBox1.Controls.Add(this.Operator_Lookup);
            this.GroupBox1.Controls.Add(this.label3);
            this.GroupBox1.Controls.Add(this.MachineTBox);
            this.GroupBox1.Controls.Add(this.label2);
            this.GroupBox1.Controls.Add(this.BatchTBox);
            this.GroupBox1.Controls.Add(this.label1);
            this.GroupBox1.Controls.Add(this.ProductTBox);
            this.GroupBox1.Controls.Add(this.Label4);
            this.GroupBox1.Controls.Add(this.OperationTBox);
            this.GroupBox1.Controls.Add(this.StartDateTBox);
            this.GroupBox1.Controls.Add(this.Date_Label);
            this.GroupBox1.Controls.Add(this.EndHourTBox);
            this.GroupBox1.Controls.Add(this.StartHourTBox);
            this.GroupBox1.Location = new System.Drawing.Point(12, 22);
            this.GroupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.GroupBox1.Size = new System.Drawing.Size(770, 273);
            this.GroupBox1.TabIndex = 18;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "اختصاص عملیات برنامه ریزی شده";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label8.Location = new System.Drawing.Point(293, 220);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label8.Size = new System.Drawing.Size(74, 26);
            this.label8.TabIndex = 324;
            this.label8.Text = " پایان:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label8.Visible = false;
            // 
            // EndDateTBox
            // 
            this.EndDateTBox.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.EndDateTBox.BackColor = System.Drawing.Color.White;
            this.EndDateTBox.BackColorDateBox = System.Drawing.Color.White;
            this.EndDateTBox.DateButtonShow = true;
            this.EndDateTBox.EnableDateText = true;
            this.EndDateTBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.EndDateTBox.FontMyDate = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.EndDateTBox.Location = new System.Drawing.Point(178, 215);
            this.EndDateTBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.EndDateTBox.MinimumSize = new System.Drawing.Size(108, 32);
            this.EndDateTBox.Name = "EndDateTBox";
            this.EndDateTBox.Size = new System.Drawing.Size(108, 32);
            this.EndDateTBox.TabIndex = 323;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label7.Location = new System.Drawing.Point(676, 177);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label7.Size = new System.Drawing.Size(87, 27);
            this.label7.TabIndex = 322;
            this.label7.Text = "اپراتور";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Operator_Lookup
            // 
            this.Operator_Lookup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Operator_Lookup.AutoSize = true;
            this.Operator_Lookup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Operator_Lookup.CB_AutoComplete = false;
            this.Operator_Lookup.CB_AutoDropdown = false;
            this.Operator_Lookup.CB_ColumnNames = "";
            this.Operator_Lookup.CB_ColumnWidthDefault = 75;
            this.Operator_Lookup.CB_ColumnWidths = "75,150";
            this.Operator_Lookup.CB_DataSource = null;
            this.Operator_Lookup.CB_DisplayMember = "";
            this.Operator_Lookup.CB_LinkedColumnIndex = 1;
            this.Operator_Lookup.CB_SelectedIndex = -1;
            this.Operator_Lookup.CB_SelectedValue = "";
            this.Operator_Lookup.CB_SerachFromTitle = null;
            this.Operator_Lookup.CB_ValueMember = "";
            this.Operator_Lookup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Operator_Lookup.Location = new System.Drawing.Point(160, 177);
            this.Operator_Lookup.Name = "Operator_Lookup";
            this.Operator_Lookup.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Operator_Lookup.Size = new System.Drawing.Size(511, 33);
            this.Operator_Lookup.TabIndex = 321;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label3.Location = new System.Drawing.Point(676, 135);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(87, 27);
            this.label3.TabIndex = 320;
            this.label3.Text = "ماشین:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MachineTBox
            // 
            this.MachineTBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.MachineTBox.Location = new System.Drawing.Point(160, 136);
            this.MachineTBox.Margin = new System.Windows.Forms.Padding(4);
            this.MachineTBox.Name = "MachineTBox";
            this.MachineTBox.ReadOnly = true;
            this.MachineTBox.Size = new System.Drawing.Size(511, 24);
            this.MachineTBox.TabIndex = 319;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label2.Location = new System.Drawing.Point(676, 40);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(87, 27);
            this.label2.TabIndex = 318;
            this.label2.Text = "محصول:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BatchTBox
            // 
            this.BatchTBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.BatchTBox.Location = new System.Drawing.Point(160, 72);
            this.BatchTBox.Margin = new System.Windows.Forms.Padding(4);
            this.BatchTBox.Name = "BatchTBox";
            this.BatchTBox.ReadOnly = true;
            this.BatchTBox.Size = new System.Drawing.Size(511, 24);
            this.BatchTBox.TabIndex = 317;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label1.Location = new System.Drawing.Point(676, 70);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(87, 27);
            this.label1.TabIndex = 316;
            this.label1.Text = "بچ تولید:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ProductTBox
            // 
            this.ProductTBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.ProductTBox.Location = new System.Drawing.Point(160, 40);
            this.ProductTBox.Margin = new System.Windows.Forms.Padding(4);
            this.ProductTBox.Name = "ProductTBox";
            this.ProductTBox.ReadOnly = true;
            this.ProductTBox.Size = new System.Drawing.Size(511, 24);
            this.ProductTBox.TabIndex = 315;
            // 
            // Label4
            // 
            this.Label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label4.Location = new System.Drawing.Point(676, 103);
            this.Label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label4.Name = "Label4";
            this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label4.Size = new System.Drawing.Size(87, 27);
            this.Label4.TabIndex = 314;
            this.Label4.Text = " عملیات:";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OperationTBox
            // 
            this.OperationTBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.OperationTBox.Location = new System.Drawing.Point(160, 104);
            this.OperationTBox.Margin = new System.Windows.Forms.Padding(4);
            this.OperationTBox.Name = "OperationTBox";
            this.OperationTBox.ReadOnly = true;
            this.OperationTBox.Size = new System.Drawing.Size(511, 24);
            this.OperationTBox.TabIndex = 313;
            // 
            // StartDateTBox
            // 
            this.StartDateTBox.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.StartDateTBox.BackColor = System.Drawing.Color.White;
            this.StartDateTBox.BackColorDateBox = System.Drawing.Color.White;
            this.StartDateTBox.DateButtonShow = true;
            this.StartDateTBox.EnableDateText = true;
            this.StartDateTBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.StartDateTBox.FontMyDate = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.StartDateTBox.Location = new System.Drawing.Point(563, 220);
            this.StartDateTBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.StartDateTBox.MinimumSize = new System.Drawing.Size(108, 32);
            this.StartDateTBox.Name = "StartDateTBox";
            this.StartDateTBox.Size = new System.Drawing.Size(108, 32);
            this.StartDateTBox.TabIndex = 211;
            // 
            // Date_Label
            // 
            this.Date_Label.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Date_Label.Location = new System.Drawing.Point(689, 220);
            this.Date_Label.Name = "Date_Label";
            this.Date_Label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Date_Label.Size = new System.Drawing.Size(74, 26);
            this.Date_Label.TabIndex = 209;
            this.Date_Label.Text = " شروع:";
            this.Date_Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Date_Label.Visible = false;
            // 
            // EndHourTBox
            // 
            this.EndHourTBox.CustomFormat = "HH:mm";
            this.EndHourTBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.EndHourTBox.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EndHourTBox.Location = new System.Drawing.Point(82, 219);
            this.EndHourTBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.EndHourTBox.Name = "EndHourTBox";
            this.EndHourTBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.EndHourTBox.RightToLeftLayout = true;
            this.EndHourTBox.ShowUpDown = true;
            this.EndHourTBox.Size = new System.Drawing.Size(67, 24);
            this.EndHourTBox.TabIndex = 4;
            this.EndHourTBox.Value = new System.DateTime(2008, 6, 5, 0, 0, 0, 0);
            // 
            // StartHourTBox
            // 
            this.StartHourTBox.CustomFormat = "HH:mm";
            this.StartHourTBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.StartHourTBox.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.StartHourTBox.Location = new System.Drawing.Point(477, 223);
            this.StartHourTBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.StartHourTBox.Name = "StartHourTBox";
            this.StartHourTBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartHourTBox.RightToLeftLayout = true;
            this.StartHourTBox.ShowUpDown = true;
            this.StartHourTBox.Size = new System.Drawing.Size(70, 24);
            this.StartHourTBox.TabIndex = 3;
            this.StartHourTBox.Value = new System.DateTime(2008, 6, 5, 0, 0, 0, 0);
            // 
            // frmOperatorTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 405);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.Panel1);
            this.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmOperatorTask";
            this.Text = " اختصاص دستگاه به اپراتور";
            this.Load += new System.EventHandler(this.frmTaskAssign_Load);
            this.Panel1.ResumeLayout(false);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button _cmdExit;
        private System.Windows.Forms.Button _cmdDelete;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal PSB_FarsiDateControl.PSB_DateControl StartDateTBox;
        internal System.Windows.Forms.Label Date_Label;
        internal System.Windows.Forms.DateTimePicker EndHourTBox;
        internal System.Windows.Forms.DateTimePicker StartHourTBox;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox MachineTBox;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox BatchTBox;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox ProductTBox;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.TextBox OperationTBox;
        internal System.Windows.Forms.Label label7;
        private Controls.UserControl_LookUp Operator_Lookup;
        internal System.Windows.Forms.Label label8;
        internal PSB_FarsiDateControl.PSB_DateControl EndDateTBox;
    }
}