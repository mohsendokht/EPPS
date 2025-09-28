
namespace ProductionPlanning.ToolForms
{
    partial class ErrorLogForm
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
            this.FilelistBox = new System.Windows.Forms.ListBox();
            this.ErrorTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // FilelistBox
            // 
            this.FilelistBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.FilelistBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilelistBox.FormattingEnabled = true;
            this.FilelistBox.ItemHeight = 16;
            this.FilelistBox.Location = new System.Drawing.Point(1, 0);
            this.FilelistBox.Name = "FilelistBox";
            this.FilelistBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.FilelistBox.Size = new System.Drawing.Size(211, 420);
            this.FilelistBox.TabIndex = 0;
            this.FilelistBox.DoubleClick += new System.EventHandler(this.FilelistBox_DoubleClick);
            // 
            // ErrorTextBox
            // 
            this.ErrorTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ErrorTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorTextBox.Location = new System.Drawing.Point(233, 0);
            this.ErrorTextBox.Multiline = true;
            this.ErrorTextBox.Name = "ErrorTextBox";
            this.ErrorTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ErrorTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ErrorTextBox.Size = new System.Drawing.Size(538, 428);
            this.ErrorTextBox.TabIndex = 1;
            this.ErrorTextBox.WordWrap = false;
            // 
            // ErrorLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 428);
            this.Controls.Add(this.ErrorTextBox);
            this.Controls.Add(this.FilelistBox);
            this.Name = "ErrorLogForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "ErrorLogForm";
            this.Load += new System.EventHandler(this.ErrorLogForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox FilelistBox;
        private System.Windows.Forms.TextBox ErrorTextBox;
    }
}