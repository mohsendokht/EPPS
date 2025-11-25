namespace ProductionPlanning
{
    partial class frmToolsLists_v3
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvTools = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToolCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToolName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToolTypeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.TechnicalSpecs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinStockLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToolLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaintenanceCycle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreatedAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModifiedAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTools)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 452);
            this.panel1.Name = "panel1";
            this.panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.panel1.Size = new System.Drawing.Size(1077, 38);
            this.panel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblSearch);
            this.splitContainer1.Panel1.Controls.Add(this.txtSearch);
            this.splitContainer1.Panel1.Controls.Add(this.btnDelete);
            this.splitContainer1.Panel1.Controls.Add(this.btnSearch);
            this.splitContainer1.Panel1.Controls.Add(this.btnExit);
            this.splitContainer1.Panel1.Controls.Add(this.btnSave);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvTools);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.splitContainer1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.splitContainer1.Size = new System.Drawing.Size(1077, 452);
            this.splitContainer1.SplitterDistance = 87;
            this.splitContainer1.TabIndex = 1;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(385, 35);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(42, 16);
            this.lblSearch.TabIndex = 5;
            this.lblSearch.Text = "جستجو";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(254, 33);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(125, 22);
            this.txtSearch.TabIndex = 4;
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(540, 32);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "حذف";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(154, 32);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "جستجو";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(29, 32);
            this.btnExit.Name = "btnExit";
            this.btnExit.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "خروج";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(687, 32);
            this.btnSave.Name = "btnSave";
            this.btnSave.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "ثبت";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dgvTools
            // 
            this.dgvTools.AllowUserToOrderColumns = true;
            this.dgvTools.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTools.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.ToolCode,
            this.ToolName,
            this.ToolTypeID,
            this.TypeName,
            this.TechnicalSpecs,
            this.CurrentQuantity,
            this.MinStockLevel,
            this.ToolLocation,
            this.MaintenanceCycle,
            this.CreatedAt,
            this.ModifiedAt});
            this.dgvTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTools.Location = new System.Drawing.Point(0, 0);
            this.dgvTools.Name = "dgvTools";
            this.dgvTools.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dgvTools.RowHeadersWidth = 51;
            this.dgvTools.RowTemplate.Height = 24;
            this.dgvTools.Size = new System.Drawing.Size(1077, 361);
            this.dgvTools.TabIndex = 0;
            this.dgvTools.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvTools_CellBeginEdit);
            this.dgvTools.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTools_CellEndEdit);
            this.dgvTools.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvTools_DefaultValuesNeeded);
            this.dgvTools.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTools_RowLeave);
            this.dgvTools.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTools_RowValidated);
            this.dgvTools.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvTools_UserDeletingRow);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "";
            this.ID.MinimumWidth = 6;
            this.ID.Name = "ID";
            this.ID.Visible = false;
            this.ID.Width = 125;
            // 
            // ToolCode
            // 
            this.ToolCode.DataPropertyName = "ToolCode";
            this.ToolCode.HeaderText = "کد ابزار";
            this.ToolCode.MinimumWidth = 6;
            this.ToolCode.Name = "ToolCode";
            this.ToolCode.Width = 125;
            // 
            // ToolName
            // 
            this.ToolName.DataPropertyName = "ToolName";
            this.ToolName.HeaderText = "نام ابزار";
            this.ToolName.MinimumWidth = 6;
            this.ToolName.Name = "ToolName";
            this.ToolName.Width = 125;
            // 
            // ToolTypeID
            // 
            this.ToolTypeID.DataPropertyName = "ToolTypeID";
            this.ToolTypeID.HeaderText = "";
            this.ToolTypeID.MinimumWidth = 6;
            this.ToolTypeID.Name = "ToolTypeID";
            this.ToolTypeID.Visible = false;
            this.ToolTypeID.Width = 125;
            // 
            // TypeName
            // 
            this.TypeName.DataPropertyName = "TypeName";
            this.TypeName.HeaderText = "نوع ابزار";
            this.TypeName.MinimumWidth = 6;
            this.TypeName.Name = "TypeName";
            this.TypeName.Width = 125;
            // 
            // TechnicalSpecs
            // 
            this.TechnicalSpecs.DataPropertyName = "TechnicalSpecs";
            this.TechnicalSpecs.HeaderText = "مشخصات فنی";
            this.TechnicalSpecs.MinimumWidth = 6;
            this.TechnicalSpecs.Name = "TechnicalSpecs";
            this.TechnicalSpecs.Width = 125;
            // 
            // CurrentQuantity
            // 
            this.CurrentQuantity.DataPropertyName = "CurrentQuantity";
            this.CurrentQuantity.HeaderText = "موجودی فعلی";
            this.CurrentQuantity.MinimumWidth = 6;
            this.CurrentQuantity.Name = "CurrentQuantity";
            this.CurrentQuantity.Width = 125;
            // 
            // MinStockLevel
            // 
            this.MinStockLevel.DataPropertyName = "MinStockLevel";
            this.MinStockLevel.HeaderText = "حداقل موجودی";
            this.MinStockLevel.MinimumWidth = 6;
            this.MinStockLevel.Name = "MinStockLevel";
            this.MinStockLevel.Width = 125;
            // 
            // ToolLocation
            // 
            this.ToolLocation.DataPropertyName = "ToolLocation";
            this.ToolLocation.HeaderText = "محل ذخیره";
            this.ToolLocation.MinimumWidth = 6;
            this.ToolLocation.Name = "ToolLocation";
            this.ToolLocation.Width = 125;
            // 
            // MaintenanceCycle
            // 
            this.MaintenanceCycle.DataPropertyName = "MaintenanceCycle";
            this.MaintenanceCycle.HeaderText = "دوره تعمیر";
            this.MaintenanceCycle.MinimumWidth = 6;
            this.MaintenanceCycle.Name = "MaintenanceCycle";
            this.MaintenanceCycle.Width = 125;
            // 
            // CreatedAt
            // 
            this.CreatedAt.DataPropertyName = "CreatedAt";
            this.CreatedAt.HeaderText = "";
            this.CreatedAt.MinimumWidth = 6;
            this.CreatedAt.Name = "CreatedAt";
            this.CreatedAt.Visible = false;
            this.CreatedAt.Width = 125;
            // 
            // ModifiedAt
            // 
            this.ModifiedAt.DataPropertyName = "ModifiedAt";
            this.ModifiedAt.HeaderText = "";
            this.ModifiedAt.MinimumWidth = 6;
            this.ModifiedAt.Name = "ModifiedAt";
            this.ModifiedAt.Visible = false;
            this.ModifiedAt.Width = 125;
            // 
            // frmToolsLists_v3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 490);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Name = "frmToolsLists_v3";
            this.Text = "frmToolsLists_v3";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmToolsLists_v3_FormClosing);
            this.Load += new System.EventHandler(this.frmToolsLists_v3_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTools)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvTools;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ToolCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ToolName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ToolTypeID;
        private System.Windows.Forms.DataGridViewComboBoxColumn TypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TechnicalSpecs;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinStockLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ToolLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaintenanceCycle;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreatedAt;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModifiedAt;
    }
}