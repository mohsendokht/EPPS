//using System.Drawing;

//namespace ProductionPlanning
//{
//    partial class frmToolType
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
//            this.txtTypeName = new System.Windows.Forms.TextBox();
//            this.cmdExit = new System.Windows.Forms.Button();
//            this.label1 = new System.Windows.Forms.Label();
//            this.cmdDelete = new System.Windows.Forms.Button();
//            this.cmdSave = new System.Windows.Forms.Button();
//            this.SuspendLayout();
//            // 
//            // txtTypeName
//            // 
//            this.txtTypeName.Location = new System.Drawing.Point(88, 40);
//            this.txtTypeName.Name = "txtTypeName";
//            this.txtTypeName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.txtTypeName.Size = new System.Drawing.Size(220, 22);
//            this.txtTypeName.TabIndex = 0;
//            // 
//            // cmdExit
//            // 
//            this.cmdExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
//            this.cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
//            this.cmdExit.Location = new System.Drawing.Point(57, 102);
//            this.cmdExit.Name = "cmdExit";
//            this.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.cmdExit.Size = new System.Drawing.Size(100, 23);
//            this.cmdExit.TabIndex = 1;
//            this.cmdExit.Text = "خروج";
//            this.cmdExit.UseVisualStyleBackColor = true;
//            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
//            // 
//            // label1
//            // 
//            this.label1.AutoSize = true;
//            this.label1.Location = new System.Drawing.Point(314, 43);
//            this.label1.Name = "label1";
//            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.label1.Size = new System.Drawing.Size(45, 16);
//            this.label1.TabIndex = 2;
//            this.label1.Text = "نام نوع:";
//            // 
//            // cmdDelete
//            // 
//            this.cmdDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
//            this.cmdDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
//            this.cmdDelete.Location = new System.Drawing.Point(176, 102);
//            this.cmdDelete.Name = "cmdDelete";
//            this.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.cmdDelete.Size = new System.Drawing.Size(100, 23);
//            this.cmdDelete.TabIndex = 3;
//            this.cmdDelete.Text = "حذف";
//            this.cmdDelete.UseVisualStyleBackColor = true;
//            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
//            // 
//            // cmdSave
//            // 
//            this.cmdSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
//            this.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
//            this.cmdSave.Location = new System.Drawing.Point(295, 102);
//            this.cmdSave.Name = "cmdSave";
//            this.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
//            this.cmdSave.Size = new System.Drawing.Size(100, 23);
//            this.cmdSave.TabIndex = 4;
//            this.cmdSave.Text = "ذخیره";
//            this.cmdSave.UseVisualStyleBackColor = true;
//            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
//            // 
//            // frmToolType
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(424, 161);
//            this.Controls.Add(this.cmdSave);
//            this.Controls.Add(this.cmdDelete);
//            this.Controls.Add(this.label1);
//            this.Controls.Add(this.cmdExit);
//            this.Controls.Add(this.txtTypeName);
//            this.Name = "frmToolType";
//            this.Text = "frmToolType";
//            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmToolType_FormClosing);
//            this.Load += new System.EventHandler(this.frmToolType_Load);
//            this.ResumeLayout(false);
//            this.PerformLayout();

//        }

//        #endregion

//        private System.Windows.Forms.TextBox txtTypeName;
//        private System.Windows.Forms.Button cmdExit;
//        private System.Windows.Forms.Label label1;
//        private System.Windows.Forms.Button cmdDelete;
//        private System.Windows.Forms.Button cmdSave;
//    }
//}