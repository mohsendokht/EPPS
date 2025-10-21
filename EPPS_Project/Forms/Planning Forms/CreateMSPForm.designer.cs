using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class CreateMSPForm : Form
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
            this._Button1 = new System.Windows.Forms.Button();
            this.msgLabel = new System.Windows.Forms.Label();
            this.SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.OperationCodeLabel = new System.Windows.Forms.Label();
            this.ProgressCountLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _Button1
            // 
            this._Button1.Location = new System.Drawing.Point(94, 24);
            this._Button1.Name = "_Button1";
            this._Button1.Size = new System.Drawing.Size(197, 32);
            this._Button1.TabIndex = 0;
            this._Button1.Text = "تعیین مسیر و نام فایل خروجی";
            this._Button1.UseVisualStyleBackColor = true;
            this._Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // msgLabel
            // 
            this.msgLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.msgLabel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.msgLabel.Location = new System.Drawing.Point(94, 74);
            this.msgLabel.Name = "msgLabel";
            this.msgLabel.Size = new System.Drawing.Size(197, 31);
            this.msgLabel.TabIndex = 1;
            this.msgLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(16, 138);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(275, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // OperationCodeLabel
            // 
            this.OperationCodeLabel.AutoSize = true;
            this.OperationCodeLabel.Location = new System.Drawing.Point(13, 119);
            this.OperationCodeLabel.Name = "OperationCodeLabel";
            this.OperationCodeLabel.Size = new System.Drawing.Size(81, 13);
            this.OperationCodeLabel.TabIndex = 3;
            this.OperationCodeLabel.Text = "Operation Code";
            // 
            // ProgressCountLabel
            // 
            this.ProgressCountLabel.Location = new System.Drawing.Point(194, 119);
            this.ProgressCountLabel.Name = "ProgressCountLabel";
            this.ProgressCountLabel.Size = new System.Drawing.Size(97, 13);
            this.ProgressCountLabel.TabIndex = 4;
            this.ProgressCountLabel.Text = "Progress";
            this.ProgressCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CreateMSPForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 175);
            this.Controls.Add(this.ProgressCountLabel);
            this.Controls.Add(this.OperationCodeLabel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.msgLabel);
            this.Controls.Add(this._Button1);
            this.Name = "CreateMSPForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create MSP File";
            this.Load += new System.EventHandler(this.CreateMSPForm_Load);
            this.Disposed += new System.EventHandler(this.Form1_Disposed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button _Button1;

        internal System.Windows.Forms.Button Button1
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _Button1;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_Button1 != null)
                {
                    _Button1.Click -= Button1_Click;
                }

                _Button1 = value;
                if (_Button1 != null)
                {
                    _Button1.Click += Button1_Click;
                }
            }
        }

        internal Label msgLabel;
        internal SaveFileDialog SaveFileDialog1;
        private ProgressBar progressBar1;
        private Label OperationCodeLabel;
        private Label ProgressCountLabel;
    }
}