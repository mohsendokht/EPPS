namespace ProductionPlanning.ToolForms
{
    partial class frmDeleteConfirm
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
            this.ConfirmBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.DeleteMessagelabel = new System.Windows.Forms.Label();
            this.TB_DeleteCode = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ConfirmBtn
            // 
            this.ConfirmBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ConfirmBtn.BackColor = System.Drawing.Color.Gainsboro;
            this.ConfirmBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ConfirmBtn.Enabled = false;
            this.ConfirmBtn.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfirmBtn.Location = new System.Drawing.Point(46, 207);
            this.ConfirmBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ConfirmBtn.Name = "ConfirmBtn";
            this.ConfirmBtn.Size = new System.Drawing.Size(153, 55);
            this.ConfirmBtn.TabIndex = 2;
            this.ConfirmBtn.Text = "تایید حذف";
            this.ConfirmBtn.UseVisualStyleBackColor = false;
            this.ConfirmBtn.Click += new System.EventHandler(this.ConfirmBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CancelBtn.BackColor = System.Drawing.Color.YellowGreen;
            this.CancelBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelBtn.Location = new System.Drawing.Point(440, 207);
            this.CancelBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(153, 55);
            this.CancelBtn.TabIndex = 1;
            this.CancelBtn.Text = "انصراف";
            this.CancelBtn.UseVisualStyleBackColor = false;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // DeleteMessagelabel
            // 
            this.DeleteMessagelabel.Location = new System.Drawing.Point(21, 23);
            this.DeleteMessagelabel.Name = "DeleteMessagelabel";
            this.DeleteMessagelabel.Size = new System.Drawing.Size(606, 120);
            this.DeleteMessagelabel.TabIndex = 5;
            this.DeleteMessagelabel.Text = "Delete Message";
            // 
            // TB_DeleteCode
            // 
            this.TB_DeleteCode.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_DeleteCode.Location = new System.Drawing.Point(46, 146);
            this.TB_DeleteCode.Name = "TB_DeleteCode";
            this.TB_DeleteCode.Size = new System.Drawing.Size(547, 32);
            this.TB_DeleteCode.TabIndex = 0;
            this.TB_DeleteCode.TextChanged += new System.EventHandler(this.TB_DeleteCode_TextChanged);
            // 
            // frmDeleteConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(644, 294);
            this.ControlBox = false;
            this.Controls.Add(this.TB_DeleteCode);
            this.Controls.Add(this.DeleteMessagelabel);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.ConfirmBtn);
            this.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmDeleteConfirm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تایید حذف";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.DeleteConfirm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConfirmBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Label DeleteMessagelabel;
        private System.Windows.Forms.TextBox TB_DeleteCode;
    }
}