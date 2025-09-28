using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmBatchRecordsList : Form
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.StatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslSearchMode = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslRecNo = new System.Windows.Forms.ToolStripStatusLabel();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.cmdFilter = new System.Windows.Forms.Button();
            this._cmdExit = new System.Windows.Forms.Button();
            this.cmdFind = new System.Windows.Forms.Button();
            this._cmdDelete = new System.Windows.Forms.Button();
            this._cmdUpdate = new System.Windows.Forms.Button();
            this._cmdInsert = new System.Windows.Forms.Button();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.Product_Lookup = new ProductionPlanning.Controls.UserControl_LookUp();
            this.chkCalcProductionProgress = new System.Windows.Forms.CheckBox();
            this._cmdShowFiltered = new System.Windows.Forms.Button();
            this.chkAllRows = new System.Windows.Forms.CheckBox();
            this.txtToDate = new PSB_FarsiDateControl.PSB_DateControl();
            this.txtFromDate = new PSB_FarsiDateControl.PSB_DateControl();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.rbHasProduction = new System.Windows.Forms.RadioButton();
            this.rbNoProduction = new System.Windows.Forms.RadioButton();
            this.rbNoPlanning = new System.Windows.Forms.RadioButton();
            this.rbDoneBatchs = new System.Windows.Forms.RadioButton();
            this.rbAllBatchs = new System.Windows.Forms.RadioButton();
            this.Label5 = new System.Windows.Forms.Label();
            this._txtYear = new System.Windows.Forms.NumericUpDown();
            this.rbDateScope = new System.Windows.Forms.RadioButton();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.rbYearScope = new System.Windows.Forms.RadioButton();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtSearch2 = new System.Windows.Forms.TextBox();
            this.cbFilter5 = new System.Windows.Forms.ComboBox();
            this.cbFilter4 = new System.Windows.Forms.ComboBox();
            this.cbFilter3 = new System.Windows.Forms.ComboBox();
            this.cbFilter2 = new System.Windows.Forms.ComboBox();
            this.cbFilter1 = new System.Windows.Forms.ComboBox();
            this.txtSearch3 = new System.Windows.Forms.TextBox();
            this.txtSearch4 = new System.Windows.Forms.TextBox();
            this.txtSearch1 = new System.Windows.Forms.TextBox();
            this.txtSearch5 = new System.Windows.Forms.TextBox();
            this._dgBatchList = new System.Windows.Forms.DataGridView();
            this.cmnuBatch = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._mnuItemDoneBatch = new System.Windows.Forms.ToolStripMenuItem();
            this._mnuItemCancelDoneBatch = new System.Windows.Forms.ToolStripMenuItem();
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this._dgOrderList = new System.Windows.Forms.DataGridView();
            this.colOrderIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colorderSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrderQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCoutomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDeliveryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._tmrLoadOrders = new System.Windows.Forms.Timer(this.components);
            this.StatusStrip1.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.Panel3.SuspendLayout();
            this.Panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._txtYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgBatchList)).BeginInit();
            this.cmnuBatch.SuspendLayout();
            this.TabControl1.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgOrderList)).BeginInit();
            this.SuspendLayout();
            // 
            // StatusStrip1
            // 
            this.StatusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.StatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslSearchMode,
            this.ToolStripStatusLabel1,
            this.ToolStripStatusLabel2,
            this.ToolStripStatusLabel3,
            this.ToolStripStatusLabel4,
            this.tsslRecNo});
            this.StatusStrip1.Location = new System.Drawing.Point(0, 579);
            this.StatusStrip1.Name = "StatusStrip1";
            this.StatusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.StatusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.StatusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StatusStrip1.Size = new System.Drawing.Size(1156, 22);
            this.StatusStrip1.TabIndex = 6;
            this.StatusStrip1.Text = "StatusStrip1";
            // 
            // tsslSearchMode
            // 
            this.tsslSearchMode.AutoSize = false;
            this.tsslSearchMode.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.tsslSearchMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslSearchMode.Name = "tsslSearchMode";
            this.tsslSearchMode.Size = new System.Drawing.Size(120, 16);
            this.tsslSearchMode.Text = "نمایش کلی";
            this.tsslSearchMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ToolStripStatusLabel1
            // 
            this.ToolStripStatusLabel1.AutoSize = false;
            this.ToolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1";
            this.ToolStripStatusLabel1.Size = new System.Drawing.Size(112, 16);
            // 
            // ToolStripStatusLabel2
            // 
            this.ToolStripStatusLabel2.AutoSize = false;
            this.ToolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2";
            this.ToolStripStatusLabel2.Size = new System.Drawing.Size(112, 16);
            // 
            // ToolStripStatusLabel3
            // 
            this.ToolStripStatusLabel3.AutoSize = false;
            this.ToolStripStatusLabel3.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3";
            this.ToolStripStatusLabel3.Size = new System.Drawing.Size(120, 16);
            // 
            // ToolStripStatusLabel4
            // 
            this.ToolStripStatusLabel4.AutoSize = false;
            this.ToolStripStatusLabel4.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.ToolStripStatusLabel4.Name = "ToolStripStatusLabel4";
            this.ToolStripStatusLabel4.Size = new System.Drawing.Size(120, 16);
            // 
            // tsslRecNo
            // 
            this.tsslRecNo.AutoSize = false;
            this.tsslRecNo.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.tsslRecNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslRecNo.Name = "tsslRecNo";
            this.tsslRecNo.Size = new System.Drawing.Size(200, 16);
            this.tsslRecNo.Text = "رکورد جاری و تعداد رکوردها";
            this.tsslRecNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Panel2
            // 
            this.Panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel2.Controls.Add(this.cmdFilter);
            this.Panel2.Controls.Add(this._cmdExit);
            this.Panel2.Controls.Add(this.cmdFind);
            this.Panel2.Controls.Add(this._cmdDelete);
            this.Panel2.Controls.Add(this._cmdUpdate);
            this.Panel2.Controls.Add(this._cmdInsert);
            this.Panel2.Controls.Add(this.Panel3);
            this.Panel2.Location = new System.Drawing.Point(7, 7);
            this.Panel2.Margin = new System.Windows.Forms.Padding(4);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(1139, 195);
            this.Panel2.TabIndex = 5;
            // 
            // cmdFilter
            // 
            this.cmdFilter.BackColor = System.Drawing.Color.Transparent;
            this.cmdFilter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cmdFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdFilter.Location = new System.Drawing.Point(275, 153);
            this.cmdFilter.Margin = new System.Windows.Forms.Padding(4);
            this.cmdFilter.Name = "cmdFilter";
            this.cmdFilter.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdFilter.Size = new System.Drawing.Size(121, 33);
            this.cmdFilter.TabIndex = 4;
            this.cmdFilter.Text = "فیلتر";
            this.cmdFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdFilter.UseVisualStyleBackColor = false;
            this.cmdFilter.Visible = false;
            // 
            // _cmdExit
            // 
            this._cmdExit.BackColor = System.Drawing.Color.Transparent;
            this._cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cmdExit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdExit.ForeColor = System.Drawing.Color.Red;
            this._cmdExit.Location = new System.Drawing.Point(16, 153);
            this._cmdExit.Margin = new System.Windows.Forms.Padding(4);
            this._cmdExit.Name = "_cmdExit";
            this._cmdExit.Size = new System.Drawing.Size(121, 33);
            this._cmdExit.TabIndex = 5;
            this._cmdExit.Text = "خروج";
            this._cmdExit.UseVisualStyleBackColor = false;
            this._cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdFind
            // 
            this.cmdFind.BackColor = System.Drawing.Color.Transparent;
            this.cmdFind.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cmdFind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdFind.Location = new System.Drawing.Point(145, 153);
            this.cmdFind.Margin = new System.Windows.Forms.Padding(4);
            this.cmdFind.Name = "cmdFind";
            this.cmdFind.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdFind.Size = new System.Drawing.Size(121, 33);
            this.cmdFind.TabIndex = 3;
            this.cmdFind.Text = "جستجو";
            this.cmdFind.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdFind.UseVisualStyleBackColor = false;
            this.cmdFind.Visible = false;
            // 
            // _cmdDelete
            // 
            this._cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cmdDelete.BackColor = System.Drawing.Color.Transparent;
            this._cmdDelete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdDelete.Location = new System.Drawing.Point(1001, 153);
            this._cmdDelete.Margin = new System.Windows.Forms.Padding(4);
            this._cmdDelete.Name = "_cmdDelete";
            this._cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdDelete.Size = new System.Drawing.Size(121, 33);
            this._cmdDelete.TabIndex = 2;
            this._cmdDelete.Text = "حذف";
            this._cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._cmdDelete.UseVisualStyleBackColor = false;
            this._cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // _cmdUpdate
            // 
            this._cmdUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cmdUpdate.BackColor = System.Drawing.Color.Transparent;
            this._cmdUpdate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdUpdate.Location = new System.Drawing.Point(872, 153);
            this._cmdUpdate.Margin = new System.Windows.Forms.Padding(4);
            this._cmdUpdate.Name = "_cmdUpdate";
            this._cmdUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdUpdate.Size = new System.Drawing.Size(121, 33);
            this._cmdUpdate.TabIndex = 1;
            this._cmdUpdate.Text = "اصلاح";
            this._cmdUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._cmdUpdate.UseVisualStyleBackColor = false;
            this._cmdUpdate.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // _cmdInsert
            // 
            this._cmdInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cmdInsert.BackColor = System.Drawing.Color.Transparent;
            this._cmdInsert.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdInsert.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdInsert.Location = new System.Drawing.Point(743, 153);
            this._cmdInsert.Margin = new System.Windows.Forms.Padding(4);
            this._cmdInsert.Name = "_cmdInsert";
            this._cmdInsert.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdInsert.Size = new System.Drawing.Size(121, 33);
            this._cmdInsert.TabIndex = 0;
            this._cmdInsert.Text = "جدید";
            this._cmdInsert.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._cmdInsert.UseVisualStyleBackColor = false;
            this._cmdInsert.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // Panel3
            // 
            this.Panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel3.BackColor = System.Drawing.Color.Beige;
            this.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel3.Controls.Add(this.Product_Lookup);
            this.Panel3.Controls.Add(this.chkCalcProductionProgress);
            this.Panel3.Controls.Add(this._cmdShowFiltered);
            this.Panel3.Controls.Add(this.chkAllRows);
            this.Panel3.Controls.Add(this.txtToDate);
            this.Panel3.Controls.Add(this.txtFromDate);
            this.Panel3.Controls.Add(this.Panel4);
            this.Panel3.Controls.Add(this._txtYear);
            this.Panel3.Controls.Add(this.rbDateScope);
            this.Panel3.Controls.Add(this.Label2);
            this.Panel3.Controls.Add(this.Label4);
            this.Panel3.Controls.Add(this.rbYearScope);
            this.Panel3.Controls.Add(this.Label3);
            this.Panel3.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel3.Location = new System.Drawing.Point(8, 6);
            this.Panel3.Margin = new System.Windows.Forms.Padding(4);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(1121, 137);
            this.Panel3.TabIndex = 8;
            // 
            // Product_Lookup
            // 
            this.Product_Lookup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Product_Lookup.AutoSize = true;
            this.Product_Lookup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Product_Lookup.CB_AutoComplete = false;
            this.Product_Lookup.CB_AutoDropdown = false;
            this.Product_Lookup.CB_ColumnNames = "";
            this.Product_Lookup.CB_ColumnWidthDefault = 75;
            this.Product_Lookup.CB_ColumnWidths = "75,150";
            this.Product_Lookup.CB_DataSource = null;
            this.Product_Lookup.CB_DisplayMember = "";
            this.Product_Lookup.CB_LinkedColumnIndex = 0;
            this.Product_Lookup.CB_SelectedIndex = -1;
            this.Product_Lookup.CB_SelectedValue = "";
            this.Product_Lookup.CB_SerachFromTitle = null;
            this.Product_Lookup.CB_ValueMember = "";
            this.Product_Lookup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Product_Lookup.Location = new System.Drawing.Point(22, 10);
            this.Product_Lookup.Name = "Product_Lookup";
            this.Product_Lookup.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Product_Lookup.Size = new System.Drawing.Size(598, 33);
            this.Product_Lookup.TabIndex = 181;
            // 
            // chkCalcProductionProgress
            // 
            this.chkCalcProductionProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCalcProductionProgress.AutoSize = true;
            this.chkCalcProductionProgress.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.chkCalcProductionProgress.ForeColor = System.Drawing.Color.Fuchsia;
            this.chkCalcProductionProgress.Location = new System.Drawing.Point(427, 62);
            this.chkCalcProductionProgress.Margin = new System.Windows.Forms.Padding(4);
            this.chkCalcProductionProgress.Name = "chkCalcProductionProgress";
            this.chkCalcProductionProgress.Size = new System.Drawing.Size(193, 25);
            this.chkCalcProductionProgress.TabIndex = 180;
            this.chkCalcProductionProgress.Text = "محاسبۀ پیشرفت تولید";
            this.chkCalcProductionProgress.UseVisualStyleBackColor = true;
            // 
            // _cmdShowFiltered
            // 
            this._cmdShowFiltered.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cmdShowFiltered.BackColor = System.Drawing.Color.Transparent;
            this._cmdShowFiltered.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdShowFiltered.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdShowFiltered.Location = new System.Drawing.Point(9, 62);
            this._cmdShowFiltered.Margin = new System.Windows.Forms.Padding(4);
            this._cmdShowFiltered.Name = "_cmdShowFiltered";
            this._cmdShowFiltered.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdShowFiltered.Size = new System.Drawing.Size(200, 31);
            this._cmdShowFiltered.TabIndex = 179;
            this._cmdShowFiltered.Text = "نمایش لیست";
            this._cmdShowFiltered.UseVisualStyleBackColor = false;
            this._cmdShowFiltered.Click += new System.EventHandler(this.cmdShowFiltered_Click);
            // 
            // chkAllRows
            // 
            this.chkAllRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAllRows.AutoSize = true;
            this.chkAllRows.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.chkAllRows.ForeColor = System.Drawing.Color.Blue;
            this.chkAllRows.Location = new System.Drawing.Point(228, 67);
            this.chkAllRows.Margin = new System.Windows.Forms.Padding(4);
            this.chkAllRows.Name = "chkAllRows";
            this.chkAllRows.Size = new System.Drawing.Size(113, 22);
            this.chkAllRows.TabIndex = 178;
            this.chkAllRows.Text = "نمایش همه";
            this.chkAllRows.UseVisualStyleBackColor = true;
            // 
            // txtToDate
            // 
            this.txtToDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtToDate.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.txtToDate.BackColor = System.Drawing.Color.White;
            this.txtToDate.BackColorDateBox = System.Drawing.Color.White;
            this.txtToDate.DateButtonShow = true;
            this.txtToDate.EnableDateText = true;
            this.txtToDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtToDate.FontMyDate = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtToDate.Location = new System.Drawing.Point(895, 71);
            this.txtToDate.Margin = new System.Windows.Forms.Padding(5);
            this.txtToDate.MinimumSize = new System.Drawing.Size(128, 30);
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.Size = new System.Drawing.Size(139, 30);
            this.txtToDate.TabIndex = 177;
            // 
            // txtFromDate
            // 
            this.txtFromDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFromDate.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.txtFromDate.BackColor = System.Drawing.Color.White;
            this.txtFromDate.BackColorDateBox = System.Drawing.Color.White;
            this.txtFromDate.DateButtonShow = true;
            this.txtFromDate.EnableDateText = true;
            this.txtFromDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtFromDate.FontMyDate = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtFromDate.Location = new System.Drawing.Point(895, 31);
            this.txtFromDate.Margin = new System.Windows.Forms.Padding(5);
            this.txtFromDate.MinimumSize = new System.Drawing.Size(128, 30);
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.Size = new System.Drawing.Size(139, 30);
            this.txtFromDate.TabIndex = 176;
            // 
            // Panel4
            // 
            this.Panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel4.Controls.Add(this.rbHasProduction);
            this.Panel4.Controls.Add(this.rbNoProduction);
            this.Panel4.Controls.Add(this.rbNoPlanning);
            this.Panel4.Controls.Add(this.rbDoneBatchs);
            this.Panel4.Controls.Add(this.rbAllBatchs);
            this.Panel4.Controls.Add(this.Label5);
            this.Panel4.Location = new System.Drawing.Point(4, 102);
            this.Panel4.Margin = new System.Windows.Forms.Padding(4);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(1113, 32);
            this.Panel4.TabIndex = 173;
            // 
            // rbHasProduction
            // 
            this.rbHasProduction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbHasProduction.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.rbHasProduction.Location = new System.Drawing.Point(280, 4);
            this.rbHasProduction.Margin = new System.Windows.Forms.Padding(4);
            this.rbHasProduction.Name = "rbHasProduction";
            this.rbHasProduction.Size = new System.Drawing.Size(115, 23);
            this.rbHasProduction.TabIndex = 183;
            this.rbHasProduction.Text = "در حال تولید";
            this.rbHasProduction.UseVisualStyleBackColor = true;
            // 
            // rbNoProduction
            // 
            this.rbNoProduction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbNoProduction.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.rbNoProduction.Location = new System.Drawing.Point(448, 4);
            this.rbNoProduction.Margin = new System.Windows.Forms.Padding(4);
            this.rbNoProduction.Name = "rbNoProduction";
            this.rbNoProduction.Size = new System.Drawing.Size(261, 23);
            this.rbNoProduction.TabIndex = 182;
            this.rbNoProduction.Text = "وارد تولید نشده(برنامه ریزی شده)";
            this.rbNoProduction.UseVisualStyleBackColor = true;
            // 
            // rbNoPlanning
            // 
            this.rbNoPlanning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbNoPlanning.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.rbNoPlanning.Location = new System.Drawing.Point(760, 4);
            this.rbNoPlanning.Margin = new System.Windows.Forms.Padding(4);
            this.rbNoPlanning.Name = "rbNoPlanning";
            this.rbNoPlanning.Size = new System.Drawing.Size(151, 23);
            this.rbNoPlanning.TabIndex = 181;
            this.rbNoPlanning.Text = "برنامه ریزی نشده";
            this.rbNoPlanning.UseVisualStyleBackColor = true;
            // 
            // rbDoneBatchs
            // 
            this.rbDoneBatchs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbDoneBatchs.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.rbDoneBatchs.Location = new System.Drawing.Point(132, 4);
            this.rbDoneBatchs.Margin = new System.Windows.Forms.Padding(4);
            this.rbDoneBatchs.Name = "rbDoneBatchs";
            this.rbDoneBatchs.Size = new System.Drawing.Size(111, 23);
            this.rbDoneBatchs.TabIndex = 179;
            this.rbDoneBatchs.Text = "بسته شده";
            this.rbDoneBatchs.UseVisualStyleBackColor = true;
            // 
            // rbAllBatchs
            // 
            this.rbAllBatchs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbAllBatchs.Checked = true;
            this.rbAllBatchs.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.rbAllBatchs.Location = new System.Drawing.Point(948, 4);
            this.rbAllBatchs.Margin = new System.Windows.Forms.Padding(4);
            this.rbAllBatchs.Name = "rbAllBatchs";
            this.rbAllBatchs.Size = new System.Drawing.Size(64, 23);
            this.rbAllBatchs.TabIndex = 178;
            this.rbAllBatchs.TabStop = true;
            this.rbAllBatchs.Text = "همه";
            this.rbAllBatchs.UseVisualStyleBackColor = true;
            // 
            // Label5
            // 
            this.Label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label5.Location = new System.Drawing.Point(1013, 6);
            this.Label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label5.Name = "Label5";
            this.Label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label5.Size = new System.Drawing.Size(87, 21);
            this.Label5.TabIndex = 177;
            this.Label5.Text = "وضعیت بچ:";
            // 
            // _txtYear
            // 
            this._txtYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._txtYear.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._txtYear.Location = new System.Drawing.Point(754, 45);
            this._txtYear.Margin = new System.Windows.Forms.Padding(4);
            this._txtYear.Maximum = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            this._txtYear.Minimum = new decimal(new int[] {
            1380,
            0,
            0,
            0});
            this._txtYear.Name = "_txtYear";
            this._txtYear.Size = new System.Drawing.Size(101, 24);
            this._txtYear.TabIndex = 172;
            this._txtYear.Value = new decimal(new int[] {
            1380,
            0,
            0,
            0});
            this._txtYear.Visible = false;
            // 
            // rbDateScope
            // 
            this.rbDateScope.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbDateScope.Checked = true;
            this.rbDateScope.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDateScope.Location = new System.Drawing.Point(974, 10);
            this.rbDateScope.Margin = new System.Windows.Forms.Padding(4);
            this.rbDateScope.Name = "rbDateScope";
            this.rbDateScope.Size = new System.Drawing.Size(127, 23);
            this.rbDateScope.TabIndex = 170;
            this.rbDateScope.TabStop = true;
            this.rbDateScope.Text = "تاریخ تعریف بچ:";
            this.rbDateScope.UseVisualStyleBackColor = true;
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label2.Location = new System.Drawing.Point(1055, 39);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label2.Size = new System.Drawing.Size(25, 21);
            this.Label2.TabIndex = 166;
            this.Label2.Text = "از:";
            // 
            // Label4
            // 
            this.Label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label4.Location = new System.Drawing.Point(615, 13);
            this.Label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label4.Name = "Label4";
            this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label4.Size = new System.Drawing.Size(86, 21);
            this.Label4.TabIndex = 162;
            this.Label4.Text = "محصول:";
            // 
            // rbYearScope
            // 
            this.rbYearScope.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbYearScope.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.rbYearScope.Location = new System.Drawing.Point(734, 13);
            this.rbYearScope.Margin = new System.Windows.Forms.Padding(4);
            this.rbYearScope.Name = "rbYearScope";
            this.rbYearScope.Size = new System.Drawing.Size(135, 23);
            this.rbYearScope.TabIndex = 171;
            this.rbYearScope.Text = "سال تعریف بچ:";
            this.rbYearScope.UseVisualStyleBackColor = true;
            this.rbYearScope.Visible = false;
            // 
            // Label3
            // 
            this.Label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label3.Location = new System.Drawing.Point(1055, 72);
            this.Label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label3.Name = "Label3";
            this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label3.Size = new System.Drawing.Size(25, 21);
            this.Label3.TabIndex = 167;
            this.Label3.Text = "تا:";
            // 
            // txtSearch2
            // 
            this.txtSearch2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtSearch2.Location = new System.Drawing.Point(156, 64);
            this.txtSearch2.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch2.Name = "txtSearch2";
            this.txtSearch2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtSearch2.Size = new System.Drawing.Size(133, 26);
            this.txtSearch2.TabIndex = 6;
            this.txtSearch2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSearch2.Visible = false;
            // 
            // cbFilter5
            // 
            this.cbFilter5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cbFilter5.FormattingEnabled = true;
            this.cbFilter5.Location = new System.Drawing.Point(297, 169);
            this.cbFilter5.Margin = new System.Windows.Forms.Padding(4);
            this.cbFilter5.Name = "cbFilter5";
            this.cbFilter5.Size = new System.Drawing.Size(132, 25);
            this.cbFilter5.TabIndex = 56;
            this.cbFilter5.Visible = false;
            // 
            // cbFilter4
            // 
            this.cbFilter4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cbFilter4.FormattingEnabled = true;
            this.cbFilter4.Location = new System.Drawing.Point(297, 133);
            this.cbFilter4.Margin = new System.Windows.Forms.Padding(4);
            this.cbFilter4.Name = "cbFilter4";
            this.cbFilter4.Size = new System.Drawing.Size(132, 25);
            this.cbFilter4.TabIndex = 55;
            this.cbFilter4.Visible = false;
            // 
            // cbFilter3
            // 
            this.cbFilter3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cbFilter3.FormattingEnabled = true;
            this.cbFilter3.Location = new System.Drawing.Point(297, 100);
            this.cbFilter3.Margin = new System.Windows.Forms.Padding(4);
            this.cbFilter3.Name = "cbFilter3";
            this.cbFilter3.Size = new System.Drawing.Size(132, 25);
            this.cbFilter3.TabIndex = 54;
            this.cbFilter3.Visible = false;
            // 
            // cbFilter2
            // 
            this.cbFilter2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cbFilter2.FormattingEnabled = true;
            this.cbFilter2.Location = new System.Drawing.Point(297, 65);
            this.cbFilter2.Margin = new System.Windows.Forms.Padding(4);
            this.cbFilter2.Name = "cbFilter2";
            this.cbFilter2.Size = new System.Drawing.Size(132, 25);
            this.cbFilter2.TabIndex = 53;
            this.cbFilter2.Visible = false;
            // 
            // cbFilter1
            // 
            this.cbFilter1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cbFilter1.FormattingEnabled = true;
            this.cbFilter1.Location = new System.Drawing.Point(297, 30);
            this.cbFilter1.Margin = new System.Windows.Forms.Padding(4);
            this.cbFilter1.Name = "cbFilter1";
            this.cbFilter1.Size = new System.Drawing.Size(132, 25);
            this.cbFilter1.TabIndex = 52;
            this.cbFilter1.Visible = false;
            // 
            // txtSearch3
            // 
            this.txtSearch3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtSearch3.Location = new System.Drawing.Point(156, 98);
            this.txtSearch3.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch3.Name = "txtSearch3";
            this.txtSearch3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtSearch3.Size = new System.Drawing.Size(133, 26);
            this.txtSearch3.TabIndex = 7;
            this.txtSearch3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSearch3.Visible = false;
            // 
            // txtSearch4
            // 
            this.txtSearch4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtSearch4.Location = new System.Drawing.Point(156, 133);
            this.txtSearch4.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch4.Name = "txtSearch4";
            this.txtSearch4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtSearch4.Size = new System.Drawing.Size(133, 26);
            this.txtSearch4.TabIndex = 8;
            this.txtSearch4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSearch4.Visible = false;
            // 
            // txtSearch1
            // 
            this.txtSearch1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtSearch1.Location = new System.Drawing.Point(156, 30);
            this.txtSearch1.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch1.MaxLength = 10;
            this.txtSearch1.Name = "txtSearch1";
            this.txtSearch1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtSearch1.Size = new System.Drawing.Size(133, 26);
            this.txtSearch1.TabIndex = 5;
            this.txtSearch1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSearch1.Visible = false;
            // 
            // txtSearch5
            // 
            this.txtSearch5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtSearch5.Location = new System.Drawing.Point(156, 167);
            this.txtSearch5.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch5.Name = "txtSearch5";
            this.txtSearch5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtSearch5.Size = new System.Drawing.Size(133, 26);
            this.txtSearch5.TabIndex = 9;
            this.txtSearch5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSearch5.Visible = false;
            // 
            // _dgBatchList
            // 
            this._dgBatchList.AllowUserToAddRows = false;
            this._dgBatchList.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this._dgBatchList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this._dgBatchList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dgBatchList.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this._dgBatchList.ColumnHeadersHeight = 40;
            this._dgBatchList.ContextMenuStrip = this.cmnuBatch;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._dgBatchList.DefaultCellStyle = dataGridViewCellStyle2;
            this._dgBatchList.Location = new System.Drawing.Point(8, 7);
            this._dgBatchList.Margin = new System.Windows.Forms.Padding(4);
            this._dgBatchList.MultiSelect = false;
            this._dgBatchList.Name = "_dgBatchList";
            this._dgBatchList.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._dgBatchList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this._dgBatchList.RowHeadersWidth = 50;
            this._dgBatchList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this._dgBatchList.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this._dgBatchList.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this._dgBatchList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgBatchList.Size = new System.Drawing.Size(1113, 318);
            this._dgBatchList.TabIndex = 1;
            this._dgBatchList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgList_CellFormatting);
            this._dgBatchList.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgList_RowEnter);
            this._dgBatchList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgList_KeyDown);
            this._dgBatchList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgList_MouseClick);
            // 
            // cmnuBatch
            // 
            this.cmnuBatch.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmnuBatch.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._mnuItemDoneBatch,
            this._mnuItemCancelDoneBatch});
            this.cmnuBatch.Name = "cmnuBatch";
            this.cmnuBatch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmnuBatch.Size = new System.Drawing.Size(200, 56);
            // 
            // _mnuItemDoneBatch
            // 
            this._mnuItemDoneBatch.Image = global::ProductionPlanning.My.Resources.Resources.DoneBatch;
            this._mnuItemDoneBatch.Name = "_mnuItemDoneBatch";
            this._mnuItemDoneBatch.Size = new System.Drawing.Size(199, 26);
            this._mnuItemDoneBatch.Text = "بستن بچ تولید";
            this._mnuItemDoneBatch.Click += new System.EventHandler(this.mnuItemDoneBatch_Click);
            // 
            // _mnuItemCancelDoneBatch
            // 
            this._mnuItemCancelDoneBatch.Image = global::ProductionPlanning.My.Resources.Resources.CalcelDoneBatch;
            this._mnuItemCancelDoneBatch.Name = "_mnuItemCancelDoneBatch";
            this._mnuItemCancelDoneBatch.Size = new System.Drawing.Size(199, 26);
            this._mnuItemCancelDoneBatch.Text = "انصراف از بستن بچ";
            this._mnuItemCancelDoneBatch.Click += new System.EventHandler(this.mnuItemDoneBatch_Click);
            // 
            // TabControl1
            // 
            this.TabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl1.Controls.Add(this.TabPage1);
            this.TabControl1.Controls.Add(this.TabPage2);
            this.TabControl1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.TabControl1.Location = new System.Drawing.Point(7, 206);
            this.TabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.RightToLeftLayout = true;
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(1140, 364);
            this.TabControl1.TabIndex = 7;
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.TabPage1.Controls.Add(this.cbFilter5);
            this.TabPage1.Controls.Add(this.cbFilter4);
            this.TabPage1.Controls.Add(this.cbFilter3);
            this.TabPage1.Controls.Add(this.txtSearch5);
            this.TabPage1.Controls.Add(this.cbFilter2);
            this.TabPage1.Controls.Add(this.txtSearch1);
            this.TabPage1.Controls.Add(this.cbFilter1);
            this.TabPage1.Controls.Add(this.txtSearch4);
            this.TabPage1.Controls.Add(this.txtSearch2);
            this.TabPage1.Controls.Add(this.txtSearch3);
            this.TabPage1.Controls.Add(this._dgBatchList);
            this.TabPage1.Location = new System.Drawing.Point(4, 26);
            this.TabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.TabPage1.Size = new System.Drawing.Size(1132, 334);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "لیست بچ های تولید";
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.TabPage2.Controls.Add(this._dgOrderList);
            this.TabPage2.Location = new System.Drawing.Point(4, 26);
            this.TabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.TabPage2.Size = new System.Drawing.Size(1132, 334);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "لیست سفارشات";
            // 
            // _dgOrderList
            // 
            this._dgOrderList.AllowUserToAddRows = false;
            this._dgOrderList.AllowUserToResizeColumns = false;
            this._dgOrderList.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            this._dgOrderList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this._dgOrderList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dgOrderList.BackgroundColor = System.Drawing.Color.Gainsboro;
            this._dgOrderList.ColumnHeadersHeight = 30;
            this._dgOrderList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this._dgOrderList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colOrderIndex,
            this.colorderSelect,
            this.DataGridViewTextBoxColumn1,
            this.colOrderQuantity,
            this.colProductCode,
            this.colCoutomerName,
            this.colDeliveryDate});
            this._dgOrderList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this._dgOrderList.Location = new System.Drawing.Point(8, 7);
            this._dgOrderList.Margin = new System.Windows.Forms.Padding(4);
            this._dgOrderList.MultiSelect = false;
            this._dgOrderList.Name = "_dgOrderList";
            this._dgOrderList.RowHeadersWidth = 10;
            this._dgOrderList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this._dgOrderList.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this._dgOrderList.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Info;
            this._dgOrderList.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._dgOrderList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgOrderList.Size = new System.Drawing.Size(1113, 318);
            this._dgOrderList.TabIndex = 10;
            this._dgOrderList.TabStop = false;
            this._dgOrderList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgOrderList_CellMouseDown);
            this._dgOrderList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgOrderList_KeyDown);
            // 
            // colOrderIndex
            // 
            this.colOrderIndex.HeaderText = "سریال سفارش";
            this.colOrderIndex.MinimumWidth = 6;
            this.colOrderIndex.Name = "colOrderIndex";
            this.colOrderIndex.ReadOnly = true;
            this.colOrderIndex.Visible = false;
            this.colOrderIndex.Width = 125;
            // 
            // colorderSelect
            // 
            this.colorderSelect.HeaderText = "انتخاب";
            this.colorderSelect.MinimumWidth = 6;
            this.colorderSelect.Name = "colorderSelect";
            this.colorderSelect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colorderSelect.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colorderSelect.Width = 40;
            // 
            // DataGridViewTextBoxColumn1
            // 
            this.DataGridViewTextBoxColumn1.HeaderText = "شمارۀ سفارش";
            this.DataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1";
            this.DataGridViewTextBoxColumn1.ReadOnly = true;
            this.DataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridViewTextBoxColumn1.Width = 130;
            // 
            // colOrderQuantity
            // 
            this.colOrderQuantity.HeaderText = "تعداد سفارش";
            this.colOrderQuantity.MinimumWidth = 100;
            this.colOrderQuantity.Name = "colOrderQuantity";
            this.colOrderQuantity.ReadOnly = true;
            this.colOrderQuantity.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colOrderQuantity.Width = 125;
            // 
            // colProductCode
            // 
            this.colProductCode.HeaderText = "کد محصول";
            this.colProductCode.MinimumWidth = 6;
            this.colProductCode.Name = "colProductCode";
            this.colProductCode.ReadOnly = true;
            this.colProductCode.Width = 120;
            // 
            // colCoutomerName
            // 
            this.colCoutomerName.HeaderText = "نام مشتری";
            this.colCoutomerName.MinimumWidth = 6;
            this.colCoutomerName.Name = "colCoutomerName";
            this.colCoutomerName.ReadOnly = true;
            this.colCoutomerName.Width = 300;
            // 
            // colDeliveryDate
            // 
            this.colDeliveryDate.HeaderText = "تاریخ تحویل";
            this.colDeliveryDate.MinimumWidth = 6;
            this.colDeliveryDate.Name = "colDeliveryDate";
            this.colDeliveryDate.ReadOnly = true;
            this.colDeliveryDate.Width = 125;
            // 
            // _tmrLoadOrders
            // 
            this._tmrLoadOrders.Tick += new System.EventHandler(this.tmrLoadOrders_Tick);
            // 
            // frmBatchRecordsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cmdExit;
            this.ClientSize = new System.Drawing.Size(1156, 601);
            this.Controls.Add(this.TabControl1);
            this.Controls.Add(this.StatusStrip1);
            this.Controls.Add(this.Panel2);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmBatchRecordsList";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " لیست بج های تولید";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRecordsLists_FormClosing);
            this.Load += new System.EventHandler(this.frmRecordsLists_Load);
            this.Resize += new System.EventHandler(this.frmRecordsLists_Resize);
            this.StatusStrip1.ResumeLayout(false);
            this.StatusStrip1.PerformLayout();
            this.Panel2.ResumeLayout(false);
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            this.Panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._txtYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgBatchList)).EndInit();
            this.cmnuBatch.ResumeLayout(false);
            this.TabControl1.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            this.TabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dgOrderList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal StatusStrip StatusStrip1;
        internal ToolStripStatusLabel tsslSearchMode;
        internal ToolStripStatusLabel ToolStripStatusLabel1;
        internal ToolStripStatusLabel ToolStripStatusLabel2;
        internal ToolStripStatusLabel ToolStripStatusLabel3;
        internal ToolStripStatusLabel ToolStripStatusLabel4;
        internal ToolStripStatusLabel tsslRecNo;
        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.Button cmdFilter;
        private System.Windows.Forms.Button _cmdExit;

        internal System.Windows.Forms.Button cmdExit
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdExit;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdExit != null)
                {
                    _cmdExit.Click -= cmdExit_Click;
                }

                _cmdExit = value;
                if (_cmdExit != null)
                {
                    _cmdExit.Click += cmdExit_Click;
                }
            }
        }

        internal System.Windows.Forms.Button cmdFind;
        private System.Windows.Forms.Button _cmdDelete;

        internal System.Windows.Forms.Button cmdDelete
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdDelete;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdDelete != null)
                {
                    _cmdDelete.Click -= cmdDelete_Click;
                }

                _cmdDelete = value;
                if (_cmdDelete != null)
                {
                    _cmdDelete.Click += cmdDelete_Click;
                }
            }
        }

        private System.Windows.Forms.Button _cmdUpdate;

        internal System.Windows.Forms.Button cmdUpdate
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdUpdate;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdUpdate != null)
                {
                    _cmdUpdate.Click -= cmdDelete_Click;
                }

                _cmdUpdate = value;
                if (_cmdUpdate != null)
                {
                    _cmdUpdate.Click += cmdDelete_Click;
                }
            }
        }

        private System.Windows.Forms.Button _cmdInsert;

        internal System.Windows.Forms.Button cmdInsert
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdInsert;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdInsert != null)
                {
                    _cmdInsert.Click -= cmdDelete_Click;
                }

                _cmdInsert = value;
                if (_cmdInsert != null)
                {
                    _cmdInsert.Click += cmdDelete_Click;
                }
            }
        }

        internal TextBox txtSearch2;
        internal TextBox txtSearch3;
        internal TextBox txtSearch4;
        internal TextBox txtSearch1;
        internal TextBox txtSearch5;
        private DataGridView _dgBatchList;

        internal DataGridView dgBatchList
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _dgBatchList;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_dgBatchList != null)
                {
                    _dgBatchList.RowEnter -= dgList_RowEnter;
                    _dgBatchList.CellFormatting -= dgList_CellFormatting;
                    _dgBatchList.MouseClick -= dgList_MouseClick;
                    _dgBatchList.KeyDown -= dgList_KeyDown;
                }

                _dgBatchList = value;
                if (_dgBatchList != null)
                {
                    _dgBatchList.RowEnter += dgList_RowEnter;
                    _dgBatchList.CellFormatting += dgList_CellFormatting;
                    _dgBatchList.MouseClick += dgList_MouseClick;
                    _dgBatchList.KeyDown += dgList_KeyDown;
                }
            }
        }

        internal System.Windows.Forms.Panel Panel3;

        //internal ComboBox cbProductName
        //{
        //    [MethodImpl(MethodImplOptions.Synchronized)]
        //    get
        //    {
        //        return _cbProductName;
        //    }

        //    [MethodImpl(MethodImplOptions.Synchronized)]
        //    set
        //    {
        //        if (_cbProductName != null)
        //        {
        //            _cbProductName.TextChanged -= cbProductName_TextChanged;
        //            _cbProductName.SelectionChangeCommitted -= cbProductName_SelectionChangeCommitted;
        //        }

        //        _cbProductName = value;
        //        if (_cbProductName != null)
        //        {
        //            _cbProductName.TextChanged += cbProductName_TextChanged;
        //            _cbProductName.SelectionChangeCommitted += cbProductName_SelectionChangeCommitted;
        //        }
        //    }
        //}
        internal Label Label4;

        //internal TextBox txtProductCode
        //{
        //    [MethodImpl(MethodImplOptions.Synchronized)]
        //    get
        //    {
        //        return _txtProductCode;
        //    }

        //    [MethodImpl(MethodImplOptions.Synchronized)]
        //    set
        //    {
        //        if (_txtProductCode != null)
        //        {
        //            _txtProductCode.KeyPress -= txtProductCode_KeyPress;
        //            _txtProductCode.TextChanged -= txtProductCode_TextChanged;
        //        }

        //        _txtProductCode = value;
        //        if (_txtProductCode != null)
        //        {
        //            _txtProductCode.KeyPress += txtProductCode_KeyPress;
        //            _txtProductCode.TextChanged += txtProductCode_TextChanged;
        //        }
        //    }
        //}

        internal Label Label3;
        internal Label Label2;
        private NumericUpDown _txtYear;

        internal NumericUpDown txtYear
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtYear;
            }

            //[MethodImpl(MethodImplOptions.Synchronized)]
            //set
            //{
            //    if (_txtYear != null)
            //    {
            //        _txtYear.KeyPress -= txtProductCode_KeyPress;
            //    }

            //    _txtYear = value;
            //    if (_txtYear != null)
            //    {
            //        _txtYear.KeyPress += txtProductCode_KeyPress;
            //    }
            //}
        }

        internal RadioButton rbYearScope;
        internal RadioButton rbDateScope;
        internal System.Windows.Forms.Panel Panel4;
        internal RadioButton rbDoneBatchs;
        internal RadioButton rbAllBatchs;
        internal Label Label5;
        internal ContextMenuStrip cmnuBatch;
        private ToolStripMenuItem _mnuItemDoneBatch;

        internal ToolStripMenuItem mnuItemDoneBatch
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _mnuItemDoneBatch;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_mnuItemDoneBatch != null)
                {
                    _mnuItemDoneBatch.Click -= mnuItemDoneBatch_Click;
                }

                _mnuItemDoneBatch = value;
                if (_mnuItemDoneBatch != null)
                {
                    _mnuItemDoneBatch.Click += mnuItemDoneBatch_Click;
                }
            }
        }

        private ToolStripMenuItem _mnuItemCancelDoneBatch;

        internal ToolStripMenuItem mnuItemCancelDoneBatch
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _mnuItemCancelDoneBatch;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_mnuItemCancelDoneBatch != null)
                {
                    _mnuItemCancelDoneBatch.Click -= mnuItemDoneBatch_Click;
                }

                _mnuItemCancelDoneBatch = value;
                if (_mnuItemCancelDoneBatch != null)
                {
                    _mnuItemCancelDoneBatch.Click += mnuItemDoneBatch_Click;
                }
            }
        }

        internal ComboBox cbFilter5;
        internal ComboBox cbFilter4;
        internal ComboBox cbFilter3;
        internal ComboBox cbFilter2;
        internal ComboBox cbFilter1;
        internal PSB_FarsiDateControl.PSB_DateControl txtFromDate;
        internal PSB_FarsiDateControl.PSB_DateControl txtToDate;
        internal TabControl TabControl1;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        private DataGridView _dgOrderList;

        internal DataGridView dgOrderList
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _dgOrderList;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_dgOrderList != null)
                {
                    _dgOrderList.CellMouseDown -= dgOrderList_CellMouseDown;
                    _dgOrderList.KeyDown -= dgOrderList_KeyDown;
                }

                _dgOrderList = value;
                if (_dgOrderList != null)
                {
                    _dgOrderList.CellMouseDown += dgOrderList_CellMouseDown;
                    _dgOrderList.KeyDown += dgOrderList_KeyDown;
                }
            }
        }

        private Timer _tmrLoadOrders;

        internal Timer tmrLoadOrders
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _tmrLoadOrders;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_tmrLoadOrders != null)
                {
                    _tmrLoadOrders.Tick -= tmrLoadOrders_Tick;
                }

                _tmrLoadOrders = value;
                if (_tmrLoadOrders != null)
                {
                    _tmrLoadOrders.Tick += tmrLoadOrders_Tick;
                }
            }
        }

        internal DataGridViewTextBoxColumn colOrderIndex;
        internal DataGridViewCheckBoxColumn colorderSelect;
        internal DataGridViewTextBoxColumn DataGridViewTextBoxColumn1;
        internal DataGridViewTextBoxColumn colOrderQuantity;
        internal DataGridViewTextBoxColumn colProductCode;
        internal DataGridViewTextBoxColumn colCoutomerName;
        internal DataGridViewTextBoxColumn colDeliveryDate;
        internal RadioButton rbNoPlanning;
        internal RadioButton rbNoProduction;
        internal RadioButton rbHasProduction;
        private System.Windows.Forms.Button _cmdShowFiltered;

        internal System.Windows.Forms.Button cmdShowFiltered
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdShowFiltered;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdShowFiltered != null)
                {
                    _cmdShowFiltered.Click -= cmdShowFiltered_Click;
                }

                _cmdShowFiltered = value;
                if (_cmdShowFiltered != null)
                {
                    _cmdShowFiltered.Click += cmdShowFiltered_Click;
                }
            }
        }

        internal CheckBox chkAllRows;
        internal CheckBox chkCalcProductionProgress;
        private Controls.UserControl_LookUp Product_Lookup;
    }
}