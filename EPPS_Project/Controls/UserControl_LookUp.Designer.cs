namespace ProductionPlanning.Controls
{
    partial class UserControl_LookUp
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CB_DescriptionBox = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.panel_DescriptionBox = new System.Windows.Forms.Panel();
            this.panel_ComboBox = new System.Windows.Forms.Panel();
            this.CB_MultiColumn = new MyUserControl.MultiColumnComboBox();
            this.panel_DescriptionBox.SuspendLayout();
            this.panel_ComboBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // CB_DescriptionBox
            // 
            this.CB_DescriptionBox.AcceptsReturn = true;
            this.CB_DescriptionBox.BackColor = System.Drawing.Color.White;
            this.CB_DescriptionBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CB_DescriptionBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CB_DescriptionBox.Location = new System.Drawing.Point(0, 0);
            this.CB_DescriptionBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CB_DescriptionBox.Name = "CB_DescriptionBox";
            this.CB_DescriptionBox.ReadOnly = true;
            this.CB_DescriptionBox.Size = new System.Drawing.Size(270, 26);
            this.CB_DescriptionBox.TabIndex = 10;
            this.CB_DescriptionBox.TabStop = false;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonSearch.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.buttonSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSearch.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonSearch.Location = new System.Drawing.Point(279, 0);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(42, 28);
            this.buttonSearch.TabIndex = 11;
            this.buttonSearch.TabStop = false;
            this.buttonSearch.Text = "?";
            this.buttonSearch.UseVisualStyleBackColor = false;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // panel_DescriptionBox
            // 
            this.panel_DescriptionBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_DescriptionBox.Controls.Add(this.CB_DescriptionBox);
            this.panel_DescriptionBox.Location = new System.Drawing.Point(3, 3);
            this.panel_DescriptionBox.Name = "panel_DescriptionBox";
            this.panel_DescriptionBox.Size = new System.Drawing.Size(270, 27);
            this.panel_DescriptionBox.TabIndex = 13;
            // 
            // panel_ComboBox
            // 
            this.panel_ComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_ComboBox.Controls.Add(this.CB_MultiColumn);
            this.panel_ComboBox.Location = new System.Drawing.Point(327, 2);
            this.panel_ComboBox.Name = "panel_ComboBox";
            this.panel_ComboBox.Size = new System.Drawing.Size(190, 28);
            this.panel_ComboBox.TabIndex = 14;
            // 
            // CB_MultiColumn
            // 
            this.CB_MultiColumn.AutoComplete = true;
            this.CB_MultiColumn.AutoDropdown = false;
            this.CB_MultiColumn.BackColorEven = System.Drawing.Color.White;
            this.CB_MultiColumn.BackColorOdd = System.Drawing.Color.White;
            this.CB_MultiColumn.ColumnNames = "";
            this.CB_MultiColumn.ColumnWidthDefault = 75;
            this.CB_MultiColumn.ColumnWidths = "75,250";
            this.CB_MultiColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CB_MultiColumn.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.CB_MultiColumn.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CB_MultiColumn.FormattingEnabled = true;
            this.CB_MultiColumn.LinkedColumnIndex = 1;
            this.CB_MultiColumn.LinkedTextBox = this.CB_DescriptionBox;
            this.CB_MultiColumn.Location = new System.Drawing.Point(0, 0);
            this.CB_MultiColumn.Name = "CB_MultiColumn";
            this.CB_MultiColumn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.CB_MultiColumn.Size = new System.Drawing.Size(190, 27);
            this.CB_MultiColumn.TabIndex = 0;
            this.CB_MultiColumn.OpenSearchForm += new System.EventHandler(this.CB_MultiColumn_OpenSearchForm);
            this.CB_MultiColumn.SelectedValueChanged += new System.EventHandler(this.CB_MultiColumn_SelectedValueChanged);
            // 
            // UserControl_LookUp
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.panel_ComboBox);
            this.Controls.Add(this.panel_DescriptionBox);
            this.Controls.Add(this.buttonSearch);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UserControl_LookUp";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(531, 38);
            this.Resize += new System.EventHandler(this.UserControl_LookUp_Resize);
            this.panel_DescriptionBox.ResumeLayout(false);
            this.panel_DescriptionBox.PerformLayout();
            this.panel_ComboBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyUserControl.MultiColumnComboBox CB_MultiColumn;
        private System.Windows.Forms.TextBox CB_DescriptionBox;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Panel panel_DescriptionBox;
        private System.Windows.Forms.Panel panel_ComboBox;
    }
}
