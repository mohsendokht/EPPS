using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class AboutBox1 : Form
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

        internal TableLayoutPanel TableLayoutPanel;
        internal PictureBox LogoPictureBox;
        internal Label LabelProductName;
        internal Label LabelVersion;
        internal Label LabelCompanyName;
        internal TextBox TextBoxDescription;
        private System.Windows.Forms.Button _OKButton;

        internal System.Windows.Forms.Button OKButton
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _OKButton;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_OKButton != null)
                {
                    _OKButton.Click -= OKButton_Click;
                }

                _OKButton = value;
                if (_OKButton != null)
                {
                    _OKButton.Click += OKButton_Click;
                }
            }
        }

        internal Label LabelCopyright;

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox1));
            TableLayoutPanel = new TableLayoutPanel();
            LogoPictureBox = new PictureBox();
            LabelProductName = new Label();
            LabelVersion = new Label();
            LabelCopyright = new Label();
            LabelCompanyName = new Label();
            TextBoxDescription = new TextBox();
            _OKButton = new System.Windows.Forms.Button();
            _OKButton.Click += new EventHandler(OKButton_Click);
            TableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LogoPictureBox).BeginInit();
            SuspendLayout();
            // 
            // TableLayoutPanel
            // 
            TableLayoutPanel.ColumnCount = 2;
            TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.0f));
            TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67.0f));
            TableLayoutPanel.Controls.Add(LogoPictureBox, 0, 0);
            TableLayoutPanel.Controls.Add(LabelProductName, 1, 0);
            TableLayoutPanel.Controls.Add(LabelVersion, 1, 1);
            TableLayoutPanel.Controls.Add(LabelCopyright, 1, 2);
            TableLayoutPanel.Controls.Add(LabelCompanyName, 1, 3);
            TableLayoutPanel.Controls.Add(TextBoxDescription, 1, 4);
            TableLayoutPanel.Controls.Add(_OKButton, 1, 5);
            TableLayoutPanel.Dock = DockStyle.Fill;
            TableLayoutPanel.Location = new Point(9, 9);
            TableLayoutPanel.Name = "TableLayoutPanel";
            TableLayoutPanel.RowCount = 6;
            TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10.0f));
            TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10.0f));
            TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10.0f));
            TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10.0f));
            TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50.0f));
            TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10.0f));
            TableLayoutPanel.Size = new Size(396, 258);
            TableLayoutPanel.TabIndex = 0;
            // 
            // LogoPictureBox
            // 
            LogoPictureBox.Dock = DockStyle.Fill;
            LogoPictureBox.Image = (Image)resources.GetObject("LogoPictureBox.Image");
            LogoPictureBox.Location = new Point(3, 3);
            LogoPictureBox.Name = "LogoPictureBox";
            TableLayoutPanel.SetRowSpan(LogoPictureBox, 6);
            LogoPictureBox.Size = new Size(124, 252);
            LogoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            LogoPictureBox.TabIndex = 0;
            LogoPictureBox.TabStop = false;
            // 
            // LabelProductName
            // 
            LabelProductName.Dock = DockStyle.Fill;
            LabelProductName.Location = new Point(136, 0);
            LabelProductName.Margin = new Padding(6, 0, 3, 0);
            LabelProductName.MaximumSize = new Size(0, 17);
            LabelProductName.Name = "LabelProductName";
            LabelProductName.Size = new Size(257, 17);
            LabelProductName.TabIndex = 0;
            LabelProductName.Text = "Product Name : Production Planning  and Control";
            LabelProductName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // LabelVersion
            // 
            LabelVersion.Dock = DockStyle.Fill;
            LabelVersion.Location = new Point(136, 25);
            LabelVersion.Margin = new Padding(6, 0, 3, 0);
            LabelVersion.MaximumSize = new Size(0, 17);
            LabelVersion.Name = "LabelVersion";
            LabelVersion.Size = new Size(257, 17);
            LabelVersion.TabIndex = 0;
            LabelVersion.Text = "Version";
            LabelVersion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // LabelCopyright
            // 
            LabelCopyright.Dock = DockStyle.Fill;
            LabelCopyright.Location = new Point(136, 50);
            LabelCopyright.Margin = new Padding(6, 0, 3, 0);
            LabelCopyright.MaximumSize = new Size(0, 17);
            LabelCopyright.Name = "LabelCopyright";
            LabelCopyright.Size = new Size(257, 17);
            LabelCopyright.TabIndex = 0;
            LabelCopyright.Text = "Copyright";
            LabelCopyright.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // LabelCompanyName
            // 
            LabelCompanyName.Dock = DockStyle.Fill;
            LabelCompanyName.Location = new Point(136, 75);
            LabelCompanyName.Margin = new Padding(6, 0, 3, 0);
            LabelCompanyName.MaximumSize = new Size(0, 17);
            LabelCompanyName.Name = "LabelCompanyName";
            LabelCompanyName.Size = new Size(257, 17);
            LabelCompanyName.TabIndex = 0;
            LabelCompanyName.Text = "Company Name";
            LabelCompanyName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TextBoxDescription
            // 
            TextBoxDescription.Dock = DockStyle.Fill;
            TextBoxDescription.Location = new Point(136, 103);
            TextBoxDescription.Margin = new Padding(6, 3, 3, 3);
            TextBoxDescription.Multiline = true;
            TextBoxDescription.Name = "TextBoxDescription";
            TextBoxDescription.ReadOnly = true;
            TextBoxDescription.ScrollBars = ScrollBars.Both;
            TextBoxDescription.Size = new Size(257, 123);
            TextBoxDescription.TabIndex = 0;
            TextBoxDescription.TabStop = false;
            TextBoxDescription.Text = resources.GetString("TextBoxDescription.Text");
            // 
            // OKButton
            // 
            _OKButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _OKButton.DialogResult = DialogResult.Cancel;
            _OKButton.Location = new Point(318, 232);
            _OKButton.Name = "_OKButton";
            _OKButton.Size = new Size(75, 23);
            _OKButton.TabIndex = 0;
            _OKButton.Text = "&OK";
            // 
            // AboutBox1
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _OKButton;
            ClientSize = new Size(414, 276);
            Controls.Add(TableLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutBox1";
            Padding = new Padding(9);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "About epps";
            TableLayoutPanel.ResumeLayout(false);
            TableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)LogoPictureBox).EndInit();
            Load += new EventHandler(AboutBox1_Load);
            ResumeLayout(false);
        }
    }
}