
namespace ProductionPlanning.ToolForms
{
    partial class frmMessage
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
            this.MessagePanel = new System.Windows.Forms.Panel();
            this.MsgBox = new System.Windows.Forms.TextBox();
            this.YesBtn = new System.Windows.Forms.Button();
            this.NoBtn = new System.Windows.Forms.Button();
            this.OkBtn = new System.Windows.Forms.Button();
            this.MessagePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MessagePanel
            // 
            this.MessagePanel.Controls.Add(this.MsgBox);
            this.MessagePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.MessagePanel.Location = new System.Drawing.Point(0, 0);
            this.MessagePanel.Name = "MessagePanel";
            this.MessagePanel.Size = new System.Drawing.Size(451, 105);
            this.MessagePanel.TabIndex = 1;
            // 
            // MsgBox
            // 
            this.MsgBox.AcceptsReturn = true;
            this.MsgBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.MsgBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MsgBox.Location = new System.Drawing.Point(0, 0);
            this.MsgBox.Multiline = true;
            this.MsgBox.Name = "MsgBox";
            this.MsgBox.ReadOnly = true;
            this.MsgBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.MsgBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MsgBox.Size = new System.Drawing.Size(451, 105);
            this.MsgBox.TabIndex = 0;
            // 
            // YesBtn
            // 
            this.YesBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.YesBtn.BackColor = System.Drawing.Color.Khaki;
            this.YesBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.YesBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.YesBtn.Location = new System.Drawing.Point(309, 111);
            this.YesBtn.Name = "YesBtn";
            this.YesBtn.Size = new System.Drawing.Size(106, 32);
            this.YesBtn.TabIndex = 2;
            this.YesBtn.Text = "بله";
            this.YesBtn.UseVisualStyleBackColor = false;
            this.YesBtn.Visible = false;
            this.YesBtn.Click += new System.EventHandler(this.YesBtn_Click);
            // 
            // NoBtn
            // 
            this.NoBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NoBtn.BackColor = System.Drawing.Color.Khaki;
            this.NoBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.NoBtn.Location = new System.Drawing.Point(23, 111);
            this.NoBtn.Name = "NoBtn";
            this.NoBtn.Size = new System.Drawing.Size(106, 32);
            this.NoBtn.TabIndex = 3;
            this.NoBtn.Text = "خیر";
            this.NoBtn.UseVisualStyleBackColor = false;
            this.NoBtn.Visible = false;
            this.NoBtn.Click += new System.EventHandler(this.NoBtn_Click);
            // 
            // OkBtn
            // 
            this.OkBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.OkBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.OkBtn.Location = new System.Drawing.Point(172, 111);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(106, 32);
            this.OkBtn.TabIndex = 1;
            this.OkBtn.Text = "Ok";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Visible = false;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // frmMessage
            // 
            this.AcceptButton = this.OkBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.BackColor = System.Drawing.Color.SlateGray;
            this.CancelButton = this.NoBtn;
            this.ClientSize = new System.Drawing.Size(451, 149);
            this.ControlBox = false;
            this.Controls.Add(this.NoBtn);
            this.Controls.Add(this.YesBtn);
            this.Controls.Add(this.OkBtn);
            this.Controls.Add(this.MessagePanel);
            this.Name = "frmMessage";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.MessagePanel.ResumeLayout(false);
            this.MessagePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MessagePanel;
        private System.Windows.Forms.TextBox MsgBox;
        private System.Windows.Forms.Button YesBtn;
        private System.Windows.Forms.Button NoBtn;
        private System.Windows.Forms.Button OkBtn;
    }
}
