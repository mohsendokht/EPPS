//using System;
//using System.Diagnostics;
//using System.Drawing;
//using System.Runtime.CompilerServices;
//using System.Windows.Forms;
//using Microsoft.VisualBasic.CompilerServices;

//namespace ProductionPlanning
//{
//    partial class frmTool
//    {
//        /// <summary>
//        /// Required designer variable.
//        /// </summary>
//        private System.ComponentModel.IContainer components = null;

//        /// <summary>
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Windows Form Designer generated code

//        /// <summary>
//        /// Required method for Designer support - do not modify
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            this.txtToolCode = new System.Windows.Forms.TextBox();
//            this.txtToolName = new System.Windows.Forms.TextBox();
//            this.txtTechnicalSpecs = new System.Windows.Forms.TextBox();
//            this.txtToolLocation = new System.Windows.Forms.TextBox();
//            this.txtMaintenanceCycle = new System.Windows.Forms.TextBox();
//            this.cmbToolTypeID = new System.Windows.Forms.ComboBox();
//            this.numCurrentQuantity = new System.Windows.Forms.NumericUpDown();
//            this.numMinStockLevel = new System.Windows.Forms.NumericUpDown();
//            this.cmdExit = new System.Windows.Forms.Button();
//            this.cmdDelete = new System.Windows.Forms.Button();
//            this.cmdSave = new System.Windows.Forms.Button();
//            this.label1 = new System.Windows.Forms.Label();
//            this.label2 = new System.Windows.Forms.Label();
//            this.label3 = new System.Windows.Forms.Label();
//            this.label4 = new System.Windows.Forms.Label();
//            this.label5 = new System.Windows.Forms.Label();
//            this.label6 = new System.Windows.Forms.Label();
//            this.label7 = new System.Windows.Forms.Label();
//            this.label8 = new System.Windows.Forms.Label();
//            ((System.ComponentModel.ISupportInitialize)(this.numCurrentQuantity)).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)(this.numMinStockLevel)).BeginInit();
//            this.SuspendLayout();
//            // 
//            // txtToolCode
//            // 
//            this.txtToolCode.Location = new System.Drawing.Point(513, 23);
//            this.txtToolCode.Name = "txtToolCode";
//            this.txtToolCode.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.txtToolCode.Size = new System.Drawing.Size(180, 22);
//            this.txtToolCode.TabIndex = 0;
//            // 
//            // txtToolName
//            // 
//            this.txtToolName.Location = new System.Drawing.Point(513, 61);
//            this.txtToolName.Name = "txtToolName";
//            this.txtToolName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.txtToolName.Size = new System.Drawing.Size(180, 22);
//            this.txtToolName.TabIndex = 1;
//            // 
//            // txtTechnicalSpecs
//            // 
//            this.txtTechnicalSpecs.Location = new System.Drawing.Point(513, 139);
//            this.txtTechnicalSpecs.Multiline = true;
//            this.txtTechnicalSpecs.Name = "txtTechnicalSpecs";
//            this.txtTechnicalSpecs.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.txtTechnicalSpecs.Size = new System.Drawing.Size(180, 60);
//            this.txtTechnicalSpecs.TabIndex = 2;
//            // 
//            // txtToolLocation
//            // 
//            this.txtToolLocation.Location = new System.Drawing.Point(83, 112);
//            this.txtToolLocation.Name = "txtToolLocation";
//            this.txtToolLocation.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.txtToolLocation.Size = new System.Drawing.Size(180, 22);
//            this.txtToolLocation.TabIndex = 3;
//            // 
//            // txtMaintenanceCycle
//            // 
//            this.txtMaintenanceCycle.Location = new System.Drawing.Point(83, 150);
//            this.txtMaintenanceCycle.Name = "txtMaintenanceCycle";
//            this.txtMaintenanceCycle.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.txtMaintenanceCycle.Size = new System.Drawing.Size(180, 22);
//            this.txtMaintenanceCycle.TabIndex = 4;
//            // 
//            // cmbToolTypeID
//            // 
//            this.cmbToolTypeID.FormattingEnabled = true;
//            this.cmbToolTypeID.Location = new System.Drawing.Point(513, 99);
//            this.cmbToolTypeID.Name = "cmbToolTypeID";
//            this.cmbToolTypeID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.cmbToolTypeID.Size = new System.Drawing.Size(180, 24);
//            this.cmbToolTypeID.TabIndex = 5;
//            // 
//            // numCurrentQuantity
//            // 
//            this.numCurrentQuantity.Location = new System.Drawing.Point(83, 36);
//            this.numCurrentQuantity.Name = "numCurrentQuantity";
//            this.numCurrentQuantity.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.numCurrentQuantity.Size = new System.Drawing.Size(180, 22);
//            this.numCurrentQuantity.TabIndex = 6;
//            // 
//            // numMinStockLevel
//            // 
//            this.numMinStockLevel.Location = new System.Drawing.Point(83, 74);
//            this.numMinStockLevel.Name = "numMinStockLevel";
//            this.numMinStockLevel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.numMinStockLevel.Size = new System.Drawing.Size(180, 22);
//            this.numMinStockLevel.TabIndex = 7;
//            // 
//            // cmdExit
//            // 
//            this.cmdExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
//            this.cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
//            this.cmdExit.Location = new System.Drawing.Point(170, 251);
//            this.cmdExit.Name = "cmdExit";
//            this.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.cmdExit.Size = new System.Drawing.Size(100, 23);
//            this.cmdExit.TabIndex = 8;
//            this.cmdExit.Text = "خروج";
//            this.cmdExit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
//            this.cmdExit.UseVisualStyleBackColor = true;
//            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
//            // 
//            // cmdDelete
//            // 
//            this.cmdDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
//            this.cmdDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
//            this.cmdDelete.Location = new System.Drawing.Point(365, 251);
//            this.cmdDelete.Name = "cmdDelete";
//            this.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.cmdDelete.Size = new System.Drawing.Size(100, 23);
//            this.cmdDelete.TabIndex = 9;
//            this.cmdDelete.Text = "حذف";
//            this.cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
//            this.cmdDelete.UseVisualStyleBackColor = true;
//            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
//            // 
//            // cmdSave
//            // 
//            this.cmdSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
//            this.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
//            this.cmdSave.Location = new System.Drawing.Point(560, 251);
//            this.cmdSave.Name = "cmdSave";
//            this.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.cmdSave.Size = new System.Drawing.Size(100, 23);
//            this.cmdSave.TabIndex = 10;
//            this.cmdSave.Text = "ذخیره";
//            this.cmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
//            this.cmdSave.UseVisualStyleBackColor = true;
//            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
//            // 
//            // label1
//            // 
//            this.label1.AutoSize = true;
//            this.label1.Location = new System.Drawing.Point(727, 26);
//            this.label1.Name = "label1";
//            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.label1.Size = new System.Drawing.Size(48, 16);
//            this.label1.TabIndex = 11;
//            this.label1.Text = "کد ابزار:";
//            // 
//            // label2
//            // 
//            this.label2.AutoSize = true;
//            this.label2.Location = new System.Drawing.Point(724, 64);
//            this.label2.Name = "label2";
//            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.label2.Size = new System.Drawing.Size(49, 16);
//            this.label2.TabIndex = 12;
//            this.label2.Text = "نام ابزار:";
//            // 
//            // label3
//            // 
//            this.label3.AutoSize = true;
//            this.label3.Location = new System.Drawing.Point(724, 102);
//            this.label3.Name = "label3";
//            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.label3.Size = new System.Drawing.Size(53, 16);
//            this.label3.TabIndex = 13;
//            this.label3.Text = "نوع ابزار:";
//            // 
//            // label4
//            // 
//            this.label4.AutoSize = true;
//            this.label4.Location = new System.Drawing.Point(724, 140);
//            this.label4.Name = "label4";
//            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.label4.Size = new System.Drawing.Size(80, 16);
//            this.label4.TabIndex = 14;
//            this.label4.Text = "مشخصات فنی:";
//            // 
//            // label5
//            // 
//            this.label5.AutoSize = true;
//            this.label5.Location = new System.Drawing.Point(293, 37);
//            this.label5.Name = "label5";
//            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.label5.Size = new System.Drawing.Size(89, 16);
//            this.label5.TabIndex = 15;
//            this.label5.Text = "تعداد فعلی مقدار:";
//            // 
//            // label6
//            // 
//            this.label6.AutoSize = true;
//            this.label6.Location = new System.Drawing.Point(293, 75);
//            this.label6.Name = "label6";
//            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.label6.Size = new System.Drawing.Size(137, 16);
//            this.label6.TabIndex = 16;
//            this.label6.Text = "تعداد حداقل موجودی سطح:";
//            // 
//            // label7
//            // 
//            this.label7.AutoSize = true;
//            this.label7.Location = new System.Drawing.Point(293, 113);
//            this.label7.Name = "label7";
//            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.label7.Size = new System.Drawing.Size(61, 16);
//            this.label7.TabIndex = 17;
//            this.label7.Text = "مکان ابزار:";
//            // 
//            // label8
//            // 
//            this.label8.AutoSize = true;
//            this.label8.Location = new System.Drawing.Point(293, 151);
//            this.label8.Name = "label8";
//            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.label8.Size = new System.Drawing.Size(82, 16);
//            this.label8.TabIndex = 18;
//            this.label8.Text = "چرخه نگهداری:";
//            // 
//            // frmTool
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(834, 319);
//            this.Controls.Add(this.label8);
//            this.Controls.Add(this.label7);
//            this.Controls.Add(this.label6);
//            this.Controls.Add(this.label5);
//            this.Controls.Add(this.label4);
//            this.Controls.Add(this.label3);
//            this.Controls.Add(this.label2);
//            this.Controls.Add(this.label1);
//            this.Controls.Add(this.cmdSave);
//            this.Controls.Add(this.cmdDelete);
//            this.Controls.Add(this.cmdExit);
//            this.Controls.Add(this.numMinStockLevel);
//            this.Controls.Add(this.numCurrentQuantity);
//            this.Controls.Add(this.cmbToolTypeID);
//            this.Controls.Add(this.txtMaintenanceCycle);
//            this.Controls.Add(this.txtToolLocation);
//            this.Controls.Add(this.txtTechnicalSpecs);
//            this.Controls.Add(this.txtToolName);
//            this.Controls.Add(this.txtToolCode);
//            this.Name = "frmTool";
//            this.Text = "frmTool";
//            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTool_FormClosing);
//            this.Load += new System.EventHandler(this.frmTool_Load);
//            ((System.ComponentModel.ISupportInitialize)(this.numCurrentQuantity)).EndInit();
//            ((System.ComponentModel.ISupportInitialize)(this.numMinStockLevel)).EndInit();
//            this.ResumeLayout(false);
//            this.PerformLayout();

//        }

//        #endregion

//        private System.Windows.Forms.TextBox txtToolCode;
//        private System.Windows.Forms.TextBox txtToolName;
//        private System.Windows.Forms.TextBox txtTechnicalSpecs;
//        private System.Windows.Forms.TextBox txtToolLocation;
//        private System.Windows.Forms.TextBox txtMaintenanceCycle;
//        private System.Windows.Forms.ComboBox cmbToolTypeID;
//        private System.Windows.Forms.NumericUpDown numCurrentQuantity;
//        private System.Windows.Forms.NumericUpDown numMinStockLevel;
//        private System.Windows.Forms.Button cmdExit;
//        private System.Windows.Forms.Button cmdDelete;
//        private System.Windows.Forms.Button cmdSave;
//        private System.Windows.Forms.Label label1;
//        private System.Windows.Forms.Label label2;
//        private System.Windows.Forms.Label label3;
//        private System.Windows.Forms.Label label4;
//        private System.Windows.Forms.Label label5;
//        private System.Windows.Forms.Label label6;
//        private System.Windows.Forms.Label label7;
//        private System.Windows.Forms.Label label8;
//    }
//}//using System;
//using System.Diagnostics;
//using System.Drawing;
//using System.Runtime.CompilerServices;
//using System.Windows.Forms;
//using Microsoft.VisualBasic.CompilerServices;

//namespace ProductionPlanning
//{
//    partial class frmTool
//    {
//        /// <summary>
//        /// Required designer variable.
//        /// </summary>
//        private System.ComponentModel.IContainer components = null;

//        /// <summary>
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Windows Form Designer generated code

//        /// <summary>
//        /// Required method for Designer support - do not modify
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            this.txtToolCode = new System.Windows.Forms.TextBox();
//            this.txtToolName = new System.Windows.Forms.TextBox();
//            this.txtTechnicalSpecs = new System.Windows.Forms.TextBox();
//            this.txtToolLocation = new System.Windows.Forms.TextBox();
//            this.txtMaintenanceCycle = new System.Windows.Forms.TextBox();
//            this.cmbToolTypeID = new System.Windows.Forms.ComboBox();
//            this.numCurrentQuantity = new System.Windows.Forms.NumericUpDown();
//            this.numMinStockLevel = new System.Windows.Forms.NumericUpDown();
//            this.cmdExit = new System.Windows.Forms.Button();
//            this.cmdDelete = new System.Windows.Forms.Button();
//            this.cmdSave = new System.Windows.Forms.Button();
//            this.label1 = new System.Windows.Forms.Label();
//            this.label2 = new System.Windows.Forms.Label();
//            this.label3 = new System.Windows.Forms.Label();
//            this.label4 = new System.Windows.Forms.Label();
//            this.label5 = new System.Windows.Forms.Label();
//            this.label6 = new System.Windows.Forms.Label();
//            this.label7 = new System.Windows.Forms.Label();
//            this.label8 = new System.Windows.Forms.Label();
//            ((System.ComponentModel.ISupportInitialize)(this.numCurrentQuantity)).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)(this.numMinStockLevel)).BeginInit();
//            this.SuspendLayout();
//            // 
//            // txtToolCode
//            // 
//            this.txtToolCode.Location = new System.Drawing.Point(513, 23);
//            this.txtToolCode.Name = "txtToolCode";
//            this.txtToolCode.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.txtToolCode.Size = new System.Drawing.Size(180, 22);
//            this.txtToolCode.TabIndex = 0;
//            // 
//            // txtToolName
//            // 
//            this.txtToolName.Location = new System.Drawing.Point(513, 61);
//            this.txtToolName.Name = "txtToolName";
//            this.txtToolName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.txtToolName.Size = new System.Drawing.Size(180, 22);
//            this.txtToolName.TabIndex = 1;
//            // 
//            // txtTechnicalSpecs
//            // 
//            this.txtTechnicalSpecs.Location = new System.Drawing.Point(513, 139);
//            this.txtTechnicalSpecs.Multiline = true;
//            this.txtTechnicalSpecs.Name = "txtTechnicalSpecs";
//            this.txtTechnicalSpecs.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.txtTechnicalSpecs.Size = new System.Drawing.Size(180, 60);
//            this.txtTechnicalSpecs.TabIndex = 2;
//            // 
//            // txtToolLocation
//            // 
//            this.txtToolLocation.Location = new System.Drawing.Point(83, 112);
//            this.txtToolLocation.Name = "txtToolLocation";
//            this.txtToolLocation.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.txtToolLocation.Size = new System.Drawing.Size(180, 22);
//            this.txtToolLocation.TabIndex = 3;
//            // 
//            // txtMaintenanceCycle
//            // 
//            this.txtMaintenanceCycle.Location = new System.Drawing.Point(83, 150);
//            this.txtMaintenanceCycle.Name = "txtMaintenanceCycle";
//            this.txtMaintenanceCycle.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.txtMaintenanceCycle.Size = new System.Drawing.Size(180, 22);
//            this.txtMaintenanceCycle.TabIndex = 4;
//            // 
//            // cmbToolTypeID
//            // 
//            this.cmbToolTypeID.FormattingEnabled = true;
//            this.cmbToolTypeID.Location = new System.Drawing.Point(513, 99);
//            this.cmbToolTypeID.Name = "cmbToolTypeID";
//            this.cmbToolTypeID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.cmbToolTypeID.Size = new System.Drawing.Size(180, 24);
//            this.cmbToolTypeID.TabIndex = 5;
//            // 
//            // numCurrentQuantity
//            // 
//            this.numCurrentQuantity.Location = new System.Drawing.Point(83, 36);
//            this.numCurrentQuantity.Name = "numCurrentQuantity";
//            this.numCurrentQuantity.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.numCurrentQuantity.Size = new System.Drawing.Size(180, 22);
//            this.numCurrentQuantity.TabIndex = 6;
//            // 
//            // numMinStockLevel
//            // 
//            this.numMinStockLevel.Location = new System.Drawing.Point(83, 74);
//            this.numMinStockLevel.Name = "numMinStockLevel";
//            this.numMinStockLevel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.numMinStockLevel.Size = new System.Drawing.Size(180, 22);
//            this.numMinStockLevel.TabIndex = 7;
//            // 
//            // cmdExit
//            // 
//            this.cmdExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
//            this.cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
//            this.cmdExit.Location = new System.Drawing.Point(170, 251);
//            this.cmdExit.Name = "cmdExit";
//            this.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.cmdExit.Size = new System.Drawing.Size(100, 23);
//            this.cmdExit.TabIndex = 8;
//            this.cmdExit.Text = "خروج";
//            this.cmdExit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
//            this.cmdExit.UseVisualStyleBackColor = true;
//            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
//            // 
//            // cmdDelete
//            // 
//            this.cmdDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
//            this.cmdDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
//            this.cmdDelete.Location = new System.Drawing.Point(365, 251);
//            this.cmdDelete.Name = "cmdDelete";
//            this.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.cmdDelete.Size = new System.Drawing.Size(100, 23);
//            this.cmdDelete.TabIndex = 9;
//            this.cmdDelete.Text = "حذف";
//            this.cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
//            this.cmdDelete.UseVisualStyleBackColor = true;
//            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
//            // 
//            // cmdSave
//            // 
//            this.cmdSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
//            this.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
//            this.cmdSave.Location = new System.Drawing.Point(560, 251);
//            this.cmdSave.Name = "cmdSave";
//            this.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.cmdSave.Size = new System.Drawing.Size(100, 23);
//            this.cmdSave.TabIndex = 10;
//            this.cmdSave.Text = "ذخیره";
//            this.cmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
//            this.cmdSave.UseVisualStyleBackColor = true;
//            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
//            // 
//            // label1
//            // 
//            this.label1.AutoSize = true;
//            this.label1.Location = new System.Drawing.Point(727, 26);
//            this.label1.Name = "label1";
//            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.label1.Size = new System.Drawing.Size(48, 16);
//            this.label1.TabIndex = 11;
//            this.label1.Text = "کد ابزار:";
//            // 
//            // label2
//            // 
//            this.label2.AutoSize = true;
//            this.label2.Location = new System.Drawing.Point(724, 64);
//            this.label2.Name = "label2";
//            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.label2.Size = new System.Drawing.Size(49, 16);
//            this.label2.TabIndex = 12;
//            this.label2.Text = "نام ابزار:";
//            // 
//            // label3
//            // 
//            this.label3.AutoSize = true;
//            this.label3.Location = new System.Drawing.Point(724, 102);
//            this.label3.Name = "label3";
//            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.label3.Size = new System.Drawing.Size(53, 16);
//            this.label3.TabIndex = 13;
//            this.label3.Text = "نوع ابزار:";
//            // 
//            // label4
//            // 
//            this.label4.AutoSize = true;
//            this.label4.Location = new System.Drawing.Point(724, 140);
//            this.label4.Name = "label4";
//            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.label4.Size = new System.Drawing.Size(80, 16);
//            this.label4.TabIndex = 14;
//            this.label4.Text = "مشخصات فنی:";
//            // 
//            // label5
//            // 
//            this.label5.AutoSize = true;
//            this.label5.Location = new System.Drawing.Point(293, 37);
//            this.label5.Name = "label5";
//            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.label5.Size = new System.Drawing.Size(89, 16);
//            this.label5.TabIndex = 15;
//            this.label5.Text = "تعداد فعلی مقدار:";
//            // 
//            // label6
//            // 
//            this.label6.AutoSize = true;
//            this.label6.Location = new System.Drawing.Point(293, 75);
//            this.label6.Name = "label6";
//            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.label6.Size = new System.Drawing.Size(137, 16);
//            this.label6.TabIndex = 16;
//            this.label6.Text = "تعداد حداقل موجودی سطح:";
//            // 
//            // label7
//            // 
//            this.label7.AutoSize = true;
//            this.label7.Location = new System.Drawing.Point(293, 113);
//            this.label7.Name = "label7";
//            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.label7.Size = new System.Drawing.Size(61, 16);
//            this.label7.TabIndex = 17;
//            this.label7.Text = "مکان ابزار:";
//            // 
//            // label8
//            // 
//            this.label8.AutoSize = true;
//            this.label8.Location = new System.Drawing.Point(293, 151);
//            this.label8.Name = "label8";
//            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.label8.Size = new System.Drawing.Size(82, 16);
//            this.label8.TabIndex = 18;
//            this.label8.Text = "چرخه نگهداری:";
//            // 
//            // frmTool
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(834, 319);
//            this.Controls.Add(this.label8);
//            this.Controls.Add(this.label7);
//            this.Controls.Add(this.label6);
//            this.Controls.Add(this.label5);
//            this.Controls.Add(this.label4);
//            this.Controls.Add(this.label3);
//            this.Controls.Add(this.label2);
//            this.Controls.Add(this.label1);
//            this.Controls.Add(this.cmdSave);
//            this.Controls.Add(this.cmdDelete);
//            this.Controls.Add(this.cmdExit);
//            this.Controls.Add(this.numMinStockLevel);
//            this.Controls.Add(this.numCurrentQuantity);
//            this.Controls.Add(this.cmbToolTypeID);
//            this.Controls.Add(this.txtMaintenanceCycle);
//            this.Controls.Add(this.txtToolLocation);
//            this.Controls.Add(this.txtTechnicalSpecs);
//            this.Controls.Add(this.txtToolName);
//            this.Controls.Add(this.txtToolCode);
//            this.Name = "frmTool";
//            this.Text = "frmTool";
//            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTool_FormClosing);
//            this.Load += new System.EventHandler(this.frmTool_Load);
//            ((System.ComponentModel.ISupportInitialize)(this.numCurrentQuantity)).EndInit();
//            ((System.ComponentModel.ISupportInitialize)(this.numMinStockLevel)).EndInit();
//            this.ResumeLayout(false);
//            this.PerformLayout();

//        }

//        #endregion

//        private System.Windows.Forms.TextBox txtToolCode;
//        private System.Windows.Forms.TextBox txtToolName;
//        private System.Windows.Forms.TextBox txtTechnicalSpecs;
//        private System.Windows.Forms.TextBox txtToolLocation;
//        private System.Windows.Forms.TextBox txtMaintenanceCycle;
//        private System.Windows.Forms.ComboBox cmbToolTypeID;
//        private System.Windows.Forms.NumericUpDown numCurrentQuantity;
//        private System.Windows.Forms.NumericUpDown numMinStockLevel;
//        private System.Windows.Forms.Button cmdExit;
//        private System.Windows.Forms.Button cmdDelete;
//        private System.Windows.Forms.Button cmdSave;
//        private System.Windows.Forms.Label label1;
//        private System.Windows.Forms.Label label2;
//        private System.Windows.Forms.Label label3;
//        private System.Windows.Forms.Label label4;
//        private System.Windows.Forms.Label label5;
//        private System.Windows.Forms.Label label6;
//        private System.Windows.Forms.Label label7;
//        private System.Windows.Forms.Label label8;
//    }
//}