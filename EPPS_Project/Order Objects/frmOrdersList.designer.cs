using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmOrdersList : Form
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
            this.StatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslSearchMode = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslRecNo = new System.Windows.Forms.ToolStripStatusLabel();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.Customer_LookUp = new ProductionPlanning.Controls.UserControl_LookUp();
            this.Product_Lookup = new ProductionPlanning.Controls.UserControl_LookUp();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.label23 = new System.Windows.Forms.Label();
            this._chkUnProduction = new System.Windows.Forms.CheckBox();
            this._chkProductionDone = new System.Windows.Forms.CheckBox();
            this.txtDeliveryTo = new PSB_FarsiDateControl.PSB_DateControl();
            this.Label8 = new System.Windows.Forms.Label();
            this.txtOrderTo = new PSB_FarsiDateControl.PSB_DateControl();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.txtDeliveryFrom = new PSB_FarsiDateControl.PSB_DateControl();
            this.txtRegisterTo = new PSB_FarsiDateControl.PSB_DateControl();
            this.txtOrderFrom = new PSB_FarsiDateControl.PSB_DateControl();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.txtContractNo = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Panel5 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this._chkNotConfirmed = new System.Windows.Forms.CheckBox();
            this._chkConfirmed = new System.Windows.Forms.CheckBox();
            this.txtRegisterFrom = new PSB_FarsiDateControl.PSB_DateControl();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this._chkNotAllowed = new System.Windows.Forms.CheckBox();
            this._chkAllowed = new System.Windows.Forms.CheckBox();
            this._cmdShowFiltered = new System.Windows.Forms.Button();
            this.Label4 = new System.Windows.Forms.Label();
            this.txtOrderNo = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this._chkAllRows = new System.Windows.Forms.CheckBox();
            this.Label10 = new System.Windows.Forms.Label();
            this._cmdExit = new System.Windows.Forms.Button();
            this._cmdDelete = new System.Windows.Forms.Button();
            this._cmdUpdate = new System.Windows.Forms.Button();
            this._cmdInsert = new System.Windows.Forms.Button();
            this.Panel1 = new System.Windows.Forms.Panel();
            this._dgList = new System.Windows.Forms.DataGridView();
            this.cmsOrder = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._cmsiProductionDone = new System.Windows.Forms.ToolStripMenuItem();
            this._cmsiCancelProductionDone = new System.Windows.Forms.ToolStripMenuItem();
            this.GroupBox4 = new System.Windows.Forms.GroupBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.lblBatchQuantity = new System.Windows.Forms.Label();
            this.lblRealEnd = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.lblBatchState = new System.Windows.Forms.Label();
            this.Label22 = new System.Windows.Forms.Label();
            this.lblPlanningStart1 = new System.Windows.Forms.Label();
            this.lblOrderQuantity = new System.Windows.Forms.Label();
            this.lblOrderProductionProgress = new System.Windows.Forms.Label();
            this.Label24 = new System.Windows.Forms.Label();
            this.Label25 = new System.Windows.Forms.Label();
            this.lblBatchProductionQuantity = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.Label20 = new System.Windows.Forms.Label();
            this.Label21 = new System.Windows.Forms.Label();
            this.lblProductCode = new System.Windows.Forms.Label();
            this.lblBatchProgress = new System.Windows.Forms.Label();
            this.Label16 = new System.Windows.Forms.Label();
            this.Label17 = new System.Windows.Forms.Label();
            this.lblBatchCode = new System.Windows.Forms.Label();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.lblOrderNo = new System.Windows.Forms.Label();
            this.lblProductionCallDate = new System.Windows.Forms.Label();
            this.lblPlanningEnd = new System.Windows.Forms.Label();
            this._cmdConfirm = new System.Windows.Forms.Button();
            this._cmdAllow = new System.Windows.Forms.Button();
            this._tmrCalcOrderProgress = new System.Windows.Forms.Timer(this.components);
            this.StatusStrip1.SuspendLayout();
            this.Panel3.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.Panel5.SuspendLayout();
            this.Panel4.SuspendLayout();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgList)).BeginInit();
            this.cmsOrder.SuspendLayout();
            this.GroupBox4.SuspendLayout();
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
            this.StatusStrip1.Location = new System.Drawing.Point(0, 631);
            this.StatusStrip1.Name = "StatusStrip1";
            this.StatusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.StatusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.StatusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StatusStrip1.Size = new System.Drawing.Size(1271, 22);
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
            // Panel3
            // 
            this.Panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel3.BackColor = System.Drawing.Color.Beige;
            this.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel3.Controls.Add(this.Customer_LookUp);
            this.Panel3.Controls.Add(this.Product_Lookup);
            this.Panel3.Controls.Add(this.Panel2);
            this.Panel3.Controls.Add(this.txtDeliveryTo);
            this.Panel3.Controls.Add(this.Label8);
            this.Panel3.Controls.Add(this.txtOrderTo);
            this.Panel3.Controls.Add(this.Label9);
            this.Panel3.Controls.Add(this.Label5);
            this.Panel3.Controls.Add(this.txtDeliveryFrom);
            this.Panel3.Controls.Add(this.txtRegisterTo);
            this.Panel3.Controls.Add(this.txtOrderFrom);
            this.Panel3.Controls.Add(this.Label3);
            this.Panel3.Controls.Add(this.Label7);
            this.Panel3.Controls.Add(this.txtContractNo);
            this.Panel3.Controls.Add(this.Label2);
            this.Panel3.Controls.Add(this.Panel5);
            this.Panel3.Controls.Add(this.txtRegisterFrom);
            this.Panel3.Controls.Add(this.Panel4);
            this.Panel3.Controls.Add(this._cmdShowFiltered);
            this.Panel3.Controls.Add(this.Label4);
            this.Panel3.Controls.Add(this.txtOrderNo);
            this.Panel3.Controls.Add(this.Label6);
            this.Panel3.Controls.Add(this.Label1);
            this.Panel3.Controls.Add(this._chkAllRows);
            this.Panel3.Controls.Add(this.Label10);
            this.Panel3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Panel3.Location = new System.Drawing.Point(7, 9);
            this.Panel3.Margin = new System.Windows.Forms.Padding(4);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(1254, 170);
            this.Panel3.TabIndex = 4;
            // 
            // Customer_LookUp
            // 
            this.Customer_LookUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Customer_LookUp.AutoSize = true;
            this.Customer_LookUp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Customer_LookUp.CB_AutoComplete = false;
            this.Customer_LookUp.CB_AutoDropdown = false;
            this.Customer_LookUp.CB_ColumnNames = "";
            this.Customer_LookUp.CB_ColumnWidthDefault = 75;
            this.Customer_LookUp.CB_ColumnWidths = "75,150";
            this.Customer_LookUp.CB_DataSource = null;
            this.Customer_LookUp.CB_DisplayMember = "";
            this.Customer_LookUp.CB_LinkedColumnIndex = 0;
            this.Customer_LookUp.CB_SelectedIndex = -1;
            this.Customer_LookUp.CB_SelectedValue = "";
            this.Customer_LookUp.CB_SerachFromTitle = null;
            this.Customer_LookUp.CB_ValueMember = "";
            this.Customer_LookUp.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Customer_LookUp.Location = new System.Drawing.Point(437, 116);
            this.Customer_LookUp.Name = "Customer_LookUp";
            this.Customer_LookUp.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Customer_LookUp.Size = new System.Drawing.Size(706, 33);
            this.Customer_LookUp.TabIndex = 10;
            // 
            // Product_Lookup
            // 
            this.Product_Lookup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.Product_Lookup.Location = new System.Drawing.Point(437, 74);
            this.Product_Lookup.Name = "Product_Lookup";
            this.Product_Lookup.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Product_Lookup.Size = new System.Drawing.Size(706, 33);
            this.Product_Lookup.TabIndex = 9;
            // 
            // Panel2
            // 
            this.Panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel2.Controls.Add(this.label23);
            this.Panel2.Controls.Add(this._chkUnProduction);
            this.Panel2.Controls.Add(this._chkProductionDone);
            this.Panel2.Location = new System.Drawing.Point(200, 126);
            this.Panel2.Margin = new System.Windows.Forms.Padding(4);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(213, 38);
            this.Panel2.TabIndex = 13;
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label23.Location = new System.Drawing.Point(166, 8);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label23.Size = new System.Drawing.Size(41, 18);
            this.label23.TabIndex = 191;
            this.label23.Text = "تولید:";
            // 
            // _chkUnProduction
            // 
            this._chkUnProduction.AutoSize = true;
            this._chkUnProduction.Location = new System.Drawing.Point(5, 7);
            this._chkUnProduction.Margin = new System.Windows.Forms.Padding(4);
            this._chkUnProduction.Name = "_chkUnProduction";
            this._chkUnProduction.Size = new System.Drawing.Size(70, 22);
            this._chkUnProduction.TabIndex = 190;
            this._chkUnProduction.Text = " نشده";
            this._chkUnProduction.UseVisualStyleBackColor = true;
            this._chkUnProduction.CheckedChanged += new System.EventHandler(this.chkUnProduction_CheckedChanged);
            // 
            // _chkProductionDone
            // 
            this._chkProductionDone.AutoSize = true;
            this._chkProductionDone.Location = new System.Drawing.Point(86, 4);
            this._chkProductionDone.Margin = new System.Windows.Forms.Padding(4);
            this._chkProductionDone.Name = "_chkProductionDone";
            this._chkProductionDone.Size = new System.Drawing.Size(60, 22);
            this._chkProductionDone.TabIndex = 189;
            this._chkProductionDone.Text = "شده";
            this._chkProductionDone.UseVisualStyleBackColor = true;
            this._chkProductionDone.CheckedChanged += new System.EventHandler(this.chkProductionDone_CheckedChanged);
            // 
            // txtDeliveryTo
            // 
            this.txtDeliveryTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDeliveryTo.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.txtDeliveryTo.BackColor = System.Drawing.Color.White;
            this.txtDeliveryTo.BackColorDateBox = System.Drawing.Color.White;
            this.txtDeliveryTo.DateButtonShow = true;
            this.txtDeliveryTo.EnableDateText = true;
            this.txtDeliveryTo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtDeliveryTo.FontMyDate = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtDeliveryTo.Location = new System.Drawing.Point(35, 9);
            this.txtDeliveryTo.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtDeliveryTo.MinimumSize = new System.Drawing.Size(128, 30);
            this.txtDeliveryTo.Name = "txtDeliveryTo";
            this.txtDeliveryTo.Size = new System.Drawing.Size(132, 30);
            this.txtDeliveryTo.TabIndex = 6;
            // 
            // Label8
            // 
            this.Label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label8.Location = new System.Drawing.Point(176, 15);
            this.Label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label8.Name = "Label8";
            this.Label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label8.Size = new System.Drawing.Size(25, 21);
            this.Label8.TabIndex = 167;
            this.Label8.Text = "تا:";
            // 
            // txtOrderTo
            // 
            this.txtOrderTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOrderTo.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.txtOrderTo.BackColor = System.Drawing.Color.White;
            this.txtOrderTo.BackColorDateBox = System.Drawing.Color.White;
            this.txtOrderTo.DateButtonShow = true;
            this.txtOrderTo.EnableDateText = true;
            this.txtOrderTo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtOrderTo.FontMyDate = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtOrderTo.Location = new System.Drawing.Point(437, 6);
            this.txtOrderTo.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtOrderTo.MinimumSize = new System.Drawing.Size(128, 30);
            this.txtOrderTo.Name = "txtOrderTo";
            this.txtOrderTo.Size = new System.Drawing.Size(132, 30);
            this.txtOrderTo.TabIndex = 4;
            // 
            // Label9
            // 
            this.Label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label9.Location = new System.Drawing.Point(323, 12);
            this.Label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label9.Name = "Label9";
            this.Label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label9.Size = new System.Drawing.Size(93, 23);
            this.Label9.TabIndex = 166;
            this.Label9.Text = "تاریخ تحویل :";
            // 
            // Label5
            // 
            this.Label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label5.Location = new System.Drawing.Point(569, 12);
            this.Label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label5.Name = "Label5";
            this.Label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label5.Size = new System.Drawing.Size(25, 21);
            this.Label5.TabIndex = 167;
            this.Label5.Text = "تا:";
            // 
            // txtDeliveryFrom
            // 
            this.txtDeliveryFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDeliveryFrom.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.txtDeliveryFrom.BackColor = System.Drawing.Color.White;
            this.txtDeliveryFrom.BackColorDateBox = System.Drawing.Color.White;
            this.txtDeliveryFrom.DateButtonShow = true;
            this.txtDeliveryFrom.EnableDateText = true;
            this.txtDeliveryFrom.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtDeliveryFrom.FontMyDate = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtDeliveryFrom.Location = new System.Drawing.Point(200, 9);
            this.txtDeliveryFrom.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtDeliveryFrom.MinimumSize = new System.Drawing.Size(128, 30);
            this.txtDeliveryFrom.Name = "txtDeliveryFrom";
            this.txtDeliveryFrom.Size = new System.Drawing.Size(132, 30);
            this.txtDeliveryFrom.TabIndex = 5;
            // 
            // txtRegisterTo
            // 
            this.txtRegisterTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRegisterTo.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.txtRegisterTo.BackColor = System.Drawing.Color.White;
            this.txtRegisterTo.BackColorDateBox = System.Drawing.Color.White;
            this.txtRegisterTo.DateButtonShow = true;
            this.txtRegisterTo.EnableDateText = true;
            this.txtRegisterTo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtRegisterTo.FontMyDate = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtRegisterTo.Location = new System.Drawing.Point(861, 6);
            this.txtRegisterTo.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtRegisterTo.MinimumSize = new System.Drawing.Size(128, 30);
            this.txtRegisterTo.Name = "txtRegisterTo";
            this.txtRegisterTo.Size = new System.Drawing.Size(132, 30);
            this.txtRegisterTo.TabIndex = 2;
            // 
            // txtOrderFrom
            // 
            this.txtOrderFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOrderFrom.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.txtOrderFrom.BackColor = System.Drawing.Color.White;
            this.txtOrderFrom.BackColorDateBox = System.Drawing.Color.White;
            this.txtOrderFrom.DateButtonShow = true;
            this.txtOrderFrom.EnableDateText = true;
            this.txtOrderFrom.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtOrderFrom.FontMyDate = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtOrderFrom.Location = new System.Drawing.Point(603, 6);
            this.txtOrderFrom.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtOrderFrom.MinimumSize = new System.Drawing.Size(128, 30);
            this.txtOrderFrom.Name = "txtOrderFrom";
            this.txtOrderFrom.Size = new System.Drawing.Size(132, 30);
            this.txtOrderFrom.TabIndex = 3;
            // 
            // Label3
            // 
            this.Label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label3.Location = new System.Drawing.Point(993, 12);
            this.Label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label3.Name = "Label3";
            this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label3.Size = new System.Drawing.Size(25, 21);
            this.Label3.TabIndex = 167;
            this.Label3.Text = "تا:";
            // 
            // Label7
            // 
            this.Label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label7.Location = new System.Drawing.Point(732, 12);
            this.Label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label7.Name = "Label7";
            this.Label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label7.Size = new System.Drawing.Size(105, 24);
            this.Label7.TabIndex = 166;
            this.Label7.Text = "تاریخ سفارش:";
            // 
            // txtContractNo
            // 
            this.txtContractNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContractNo.BackColor = System.Drawing.Color.White;
            this.txtContractNo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtContractNo.Location = new System.Drawing.Point(437, 39);
            this.txtContractNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtContractNo.MaxLength = 25;
            this.txtContractNo.Name = "txtContractNo";
            this.txtContractNo.Size = new System.Drawing.Size(298, 27);
            this.txtContractNo.TabIndex = 8;
            this.txtContractNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label2.Location = new System.Drawing.Point(1157, 12);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label2.Size = new System.Drawing.Size(89, 21);
            this.Label2.TabIndex = 166;
            this.Label2.Text = "تاریخ ثبت :";
            // 
            // Panel5
            // 
            this.Panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel5.Controls.Add(this.label18);
            this.Panel5.Controls.Add(this._chkNotConfirmed);
            this.Panel5.Controls.Add(this._chkConfirmed);
            this.Panel5.Location = new System.Drawing.Point(200, 40);
            this.Panel5.Margin = new System.Windows.Forms.Padding(4);
            this.Panel5.Name = "Panel5";
            this.Panel5.Size = new System.Drawing.Size(213, 38);
            this.Panel5.TabIndex = 11;
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label18.Location = new System.Drawing.Point(169, 5);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label18.Size = new System.Drawing.Size(38, 18);
            this.label18.TabIndex = 189;
            this.label18.Text = "تایید:";
            // 
            // _chkNotConfirmed
            // 
            this._chkNotConfirmed.AutoSize = true;
            this._chkNotConfirmed.Location = new System.Drawing.Point(5, 7);
            this._chkNotConfirmed.Margin = new System.Windows.Forms.Padding(4);
            this._chkNotConfirmed.Name = "_chkNotConfirmed";
            this._chkNotConfirmed.Size = new System.Drawing.Size(65, 22);
            this._chkNotConfirmed.TabIndex = 187;
            this._chkNotConfirmed.Text = "نشده";
            this._chkNotConfirmed.UseVisualStyleBackColor = true;
            this._chkNotConfirmed.CheckedChanged += new System.EventHandler(this.chkNotConfirmed_CheckedChanged);
            // 
            // _chkConfirmed
            // 
            this._chkConfirmed.AutoSize = true;
            this._chkConfirmed.Location = new System.Drawing.Point(83, 6);
            this._chkConfirmed.Margin = new System.Windows.Forms.Padding(4);
            this._chkConfirmed.Name = "_chkConfirmed";
            this._chkConfirmed.Size = new System.Drawing.Size(60, 22);
            this._chkConfirmed.TabIndex = 186;
            this._chkConfirmed.Text = "شده";
            this._chkConfirmed.UseVisualStyleBackColor = true;
            this._chkConfirmed.CheckedChanged += new System.EventHandler(this.chkConfirmed_CheckedChanged);
            // 
            // txtRegisterFrom
            // 
            this.txtRegisterFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRegisterFrom.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.txtRegisterFrom.BackColor = System.Drawing.Color.White;
            this.txtRegisterFrom.BackColorDateBox = System.Drawing.Color.White;
            this.txtRegisterFrom.DateButtonShow = true;
            this.txtRegisterFrom.EnableDateText = true;
            this.txtRegisterFrom.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtRegisterFrom.FontMyDate = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtRegisterFrom.Location = new System.Drawing.Point(1015, 3);
            this.txtRegisterFrom.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtRegisterFrom.MinimumSize = new System.Drawing.Size(128, 30);
            this.txtRegisterFrom.Name = "txtRegisterFrom";
            this.txtRegisterFrom.Size = new System.Drawing.Size(128, 30);
            this.txtRegisterFrom.TabIndex = 1;
            // 
            // Panel4
            // 
            this.Panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel4.Controls.Add(this.label19);
            this.Panel4.Controls.Add(this._chkNotAllowed);
            this.Panel4.Controls.Add(this._chkAllowed);
            this.Panel4.Location = new System.Drawing.Point(200, 86);
            this.Panel4.Margin = new System.Windows.Forms.Padding(4);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(213, 38);
            this.Panel4.TabIndex = 12;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label19.Location = new System.Drawing.Point(152, 7);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label19.Size = new System.Drawing.Size(55, 18);
            this.label19.TabIndex = 190;
            this.label19.Text = "تصویب:";
            // 
            // _chkNotAllowed
            // 
            this._chkNotAllowed.AutoSize = true;
            this._chkNotAllowed.Location = new System.Drawing.Point(10, 4);
            this._chkNotAllowed.Margin = new System.Windows.Forms.Padding(4);
            this._chkNotAllowed.Name = "_chkNotAllowed";
            this._chkNotAllowed.Size = new System.Drawing.Size(65, 22);
            this._chkNotAllowed.TabIndex = 188;
            this._chkNotAllowed.Text = "نشده";
            this._chkNotAllowed.UseVisualStyleBackColor = true;
            this._chkNotAllowed.CheckedChanged += new System.EventHandler(this.chkNotAllowed_CheckedChanged);
            // 
            // _chkAllowed
            // 
            this._chkAllowed.AutoSize = true;
            this._chkAllowed.Location = new System.Drawing.Point(83, 4);
            this._chkAllowed.Margin = new System.Windows.Forms.Padding(4);
            this._chkAllowed.Name = "_chkAllowed";
            this._chkAllowed.Size = new System.Drawing.Size(60, 22);
            this._chkAllowed.TabIndex = 187;
            this._chkAllowed.Text = "شده";
            this._chkAllowed.UseVisualStyleBackColor = true;
            this._chkAllowed.CheckedChanged += new System.EventHandler(this.chkAllowed_CheckedChanged);
            // 
            // _cmdShowFiltered
            // 
            this._cmdShowFiltered.BackColor = System.Drawing.Color.Transparent;
            this._cmdShowFiltered.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdShowFiltered.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdShowFiltered.Location = new System.Drawing.Point(9, 105);
            this._cmdShowFiltered.Margin = new System.Windows.Forms.Padding(4);
            this._cmdShowFiltered.Name = "_cmdShowFiltered";
            this._cmdShowFiltered.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdShowFiltered.Size = new System.Drawing.Size(105, 31);
            this._cmdShowFiltered.TabIndex = 0;
            this._cmdShowFiltered.Text = "نمایش";
            this._cmdShowFiltered.UseVisualStyleBackColor = false;
            this._cmdShowFiltered.Click += new System.EventHandler(this.cmdShowFiltered_Click);
            // 
            // Label4
            // 
            this.Label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label4.Location = new System.Drawing.Point(1124, 43);
            this.Label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label4.Name = "Label4";
            this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label4.Size = new System.Drawing.Size(120, 22);
            this.Label4.TabIndex = 162;
            this.Label4.Text = "شمارۀ سفارش:";
            // 
            // txtOrderNo
            // 
            this.txtOrderNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOrderNo.BackColor = System.Drawing.Color.White;
            this.txtOrderNo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtOrderNo.Location = new System.Drawing.Point(861, 43);
            this.txtOrderNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtOrderNo.MaxLength = 25;
            this.txtOrderNo.Name = "txtOrderNo";
            this.txtOrderNo.Size = new System.Drawing.Size(255, 27);
            this.txtOrderNo.TabIndex = 7;
            this.txtOrderNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Label6
            // 
            this.Label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label6.Location = new System.Drawing.Point(1153, 124);
            this.Label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label6.Name = "Label6";
            this.Label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label6.Size = new System.Drawing.Size(91, 21);
            this.Label6.TabIndex = 178;
            this.Label6.Text = "مشتری:";
            // 
            // Label1
            // 
            this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label1.Location = new System.Drawing.Point(1170, 85);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label1.Size = new System.Drawing.Size(74, 22);
            this.Label1.TabIndex = 164;
            this.Label1.Text = "محصول:";
            // 
            // _chkAllRows
            // 
            this._chkAllRows.AutoSize = true;
            this._chkAllRows.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._chkAllRows.ForeColor = System.Drawing.Color.Blue;
            this._chkAllRows.Location = new System.Drawing.Point(6, 75);
            this._chkAllRows.Margin = new System.Windows.Forms.Padding(4);
            this._chkAllRows.Name = "_chkAllRows";
            this._chkAllRows.Size = new System.Drawing.Size(113, 22);
            this._chkAllRows.TabIndex = 14;
            this._chkAllRows.Text = "نمایش همه";
            this._chkAllRows.UseVisualStyleBackColor = true;
            this._chkAllRows.CheckedChanged += new System.EventHandler(this.chkAllRows_CheckedChanged);
            // 
            // Label10
            // 
            this.Label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label10.Location = new System.Drawing.Point(726, 43);
            this.Label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label10.Name = "Label10";
            this.Label10.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label10.Size = new System.Drawing.Size(108, 22);
            this.Label10.TabIndex = 191;
            this.Label10.Text = "شمارۀ قرارداد:";
            // 
            // _cmdExit
            // 
            this._cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._cmdExit.BackColor = System.Drawing.Color.Transparent;
            this._cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cmdExit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdExit.ForeColor = System.Drawing.Color.Red;
            this._cmdExit.Location = new System.Drawing.Point(17, 584);
            this._cmdExit.Margin = new System.Windows.Forms.Padding(4);
            this._cmdExit.Name = "_cmdExit";
            this._cmdExit.Size = new System.Drawing.Size(121, 33);
            this._cmdExit.TabIndex = 21;
            this._cmdExit.Text = "خروج";
            this._cmdExit.UseVisualStyleBackColor = false;
            this._cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // _cmdDelete
            // 
            this._cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cmdDelete.BackColor = System.Drawing.Color.Transparent;
            this._cmdDelete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdDelete.Location = new System.Drawing.Point(1128, 584);
            this._cmdDelete.Margin = new System.Windows.Forms.Padding(4);
            this._cmdDelete.Name = "_cmdDelete";
            this._cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdDelete.Size = new System.Drawing.Size(121, 33);
            this._cmdDelete.TabIndex = 16;
            this._cmdDelete.Text = "حذف";
            this._cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._cmdDelete.UseVisualStyleBackColor = false;
            this._cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // _cmdUpdate
            // 
            this._cmdUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cmdUpdate.BackColor = System.Drawing.Color.Transparent;
            this._cmdUpdate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdUpdate.Location = new System.Drawing.Point(999, 584);
            this._cmdUpdate.Margin = new System.Windows.Forms.Padding(4);
            this._cmdUpdate.Name = "_cmdUpdate";
            this._cmdUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdUpdate.Size = new System.Drawing.Size(121, 33);
            this._cmdUpdate.TabIndex = 17;
            this._cmdUpdate.Text = "اصلاح";
            this._cmdUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._cmdUpdate.UseVisualStyleBackColor = false;
            this._cmdUpdate.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // _cmdInsert
            // 
            this._cmdInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cmdInsert.BackColor = System.Drawing.Color.Transparent;
            this._cmdInsert.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdInsert.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdInsert.Location = new System.Drawing.Point(869, 584);
            this._cmdInsert.Margin = new System.Windows.Forms.Padding(4);
            this._cmdInsert.Name = "_cmdInsert";
            this._cmdInsert.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdInsert.Size = new System.Drawing.Size(121, 33);
            this._cmdInsert.TabIndex = 18;
            this._cmdInsert.Text = "جدید";
            this._cmdInsert.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._cmdInsert.UseVisualStyleBackColor = false;
            this._cmdInsert.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // Panel1
            // 
            this.Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Panel1.Controls.Add(this._dgList);
            this.Panel1.Location = new System.Drawing.Point(7, 187);
            this.Panel1.Margin = new System.Windows.Forms.Padding(4);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(1253, 288);
            this.Panel1.TabIndex = 1;
            // 
            // _dgList
            // 
            this._dgList.AllowUserToAddRows = false;
            this._dgList.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this._dgList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this._dgList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dgList.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._dgList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this._dgList.ColumnHeadersHeight = 40;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._dgList.DefaultCellStyle = dataGridViewCellStyle3;
            this._dgList.Location = new System.Drawing.Point(7, 4);
            this._dgList.Margin = new System.Windows.Forms.Padding(4);
            this._dgList.MultiSelect = false;
            this._dgList.Name = "_dgList";
            this._dgList.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._dgList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this._dgList.RowHeadersWidth = 50;
            this._dgList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this._dgList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this._dgList.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this._dgList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgList.Size = new System.Drawing.Size(1239, 279);
            this._dgList.TabIndex = 15;
            this._dgList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._dgList_CellContentClick);
            this._dgList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgList_CellFormatting);
            this._dgList.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgList_RowEnter);
            this._dgList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgList_KeyDown);
            this._dgList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgList_MouseClick);
            // 
            // cmsOrder
            // 
            this.cmsOrder.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsOrder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._cmsiProductionDone,
            this._cmsiCancelProductionDone});
            this.cmsOrder.Name = "cmsOrder";
            this.cmsOrder.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmsOrder.Size = new System.Drawing.Size(225, 52);
            // 
            // _cmsiProductionDone
            // 
            this._cmsiProductionDone.Name = "_cmsiProductionDone";
            this._cmsiProductionDone.Size = new System.Drawing.Size(224, 24);
            this._cmsiProductionDone.Text = "ثبت اتمام تولید سفارش";
            this._cmsiProductionDone.Click += new System.EventHandler(this.cmsiProductionDone_Click);
            // 
            // _cmsiCancelProductionDone
            // 
            this._cmsiCancelProductionDone.Name = "_cmsiCancelProductionDone";
            this._cmsiCancelProductionDone.Size = new System.Drawing.Size(224, 24);
            this._cmsiCancelProductionDone.Text = "لغو اتمام تولید سفارش";
            this._cmsiCancelProductionDone.Click += new System.EventHandler(this.cmsiCancelProductionDone_Click);
            // 
            // GroupBox4
            // 
            this.GroupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox4.Controls.Add(this.Label11);
            this.GroupBox4.Controls.Add(this.lblBatchQuantity);
            this.GroupBox4.Controls.Add(this.lblRealEnd);
            this.GroupBox4.Controls.Add(this.Label14);
            this.GroupBox4.Controls.Add(this.Label15);
            this.GroupBox4.Controls.Add(this.lblBatchState);
            this.GroupBox4.Controls.Add(this.Label22);
            this.GroupBox4.Controls.Add(this.lblPlanningStart1);
            this.GroupBox4.Controls.Add(this.lblOrderQuantity);
            this.GroupBox4.Controls.Add(this.lblOrderProductionProgress);
            this.GroupBox4.Controls.Add(this.Label24);
            this.GroupBox4.Controls.Add(this.Label25);
            this.GroupBox4.Controls.Add(this.lblBatchProductionQuantity);
            this.GroupBox4.Controls.Add(this.lblProductName);
            this.GroupBox4.Controls.Add(this.Label20);
            this.GroupBox4.Controls.Add(this.Label21);
            this.GroupBox4.Controls.Add(this.lblProductCode);
            this.GroupBox4.Controls.Add(this.lblBatchProgress);
            this.GroupBox4.Controls.Add(this.Label16);
            this.GroupBox4.Controls.Add(this.Label17);
            this.GroupBox4.Controls.Add(this.lblBatchCode);
            this.GroupBox4.Controls.Add(this.Label13);
            this.GroupBox4.Controls.Add(this.Label12);
            this.GroupBox4.Controls.Add(this.lblOrderNo);
            this.GroupBox4.Controls.Add(this.lblProductionCallDate);
            this.GroupBox4.Controls.Add(this.lblPlanningEnd);
            this.GroupBox4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.GroupBox4.Location = new System.Drawing.Point(7, 475);
            this.GroupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.GroupBox4.Name = "GroupBox4";
            this.GroupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.GroupBox4.Size = new System.Drawing.Size(1255, 101);
            this.GroupBox4.TabIndex = 9;
            this.GroupBox4.TabStop = false;
            this.GroupBox4.Text = "وضعیت پیشرفت سفارش";
            // 
            // Label11
            // 
            this.Label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label11.Location = new System.Drawing.Point(645, 49);
            this.Label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label11.Name = "Label11";
            this.Label11.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label11.Size = new System.Drawing.Size(69, 22);
            this.Label11.TabIndex = 188;
            this.Label11.Text = "تعداد بچ:";
            // 
            // lblBatchQuantity
            // 
            this.lblBatchQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBatchQuantity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblBatchQuantity.ForeColor = System.Drawing.Color.Blue;
            this.lblBatchQuantity.Location = new System.Drawing.Point(537, 49);
            this.lblBatchQuantity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBatchQuantity.Name = "lblBatchQuantity";
            this.lblBatchQuantity.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblBatchQuantity.Size = new System.Drawing.Size(108, 22);
            this.lblBatchQuantity.TabIndex = 187;
            this.lblBatchQuantity.Text = "#";
            // 
            // lblRealEnd
            // 
            this.lblRealEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRealEnd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblRealEnd.ForeColor = System.Drawing.Color.Blue;
            this.lblRealEnd.Location = new System.Drawing.Point(220, 78);
            this.lblRealEnd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRealEnd.Name = "lblRealEnd";
            this.lblRealEnd.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblRealEnd.Size = new System.Drawing.Size(104, 22);
            this.lblRealEnd.TabIndex = 186;
            this.lblRealEnd.Text = "#";
            // 
            // Label14
            // 
            this.Label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label14.Location = new System.Drawing.Point(147, 49);
            this.Label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label14.Name = "Label14";
            this.Label14.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label14.Size = new System.Drawing.Size(83, 22);
            this.Label14.TabIndex = 185;
            this.Label14.Text = "وضعیت بچ:";
            // 
            // Label15
            // 
            this.Label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label15.Location = new System.Drawing.Point(324, 78);
            this.Label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label15.Name = "Label15";
            this.Label15.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label15.Size = new System.Drawing.Size(127, 22);
            this.Label15.TabIndex = 184;
            this.Label15.Text = "تاریخ پایان واقعی:";
            // 
            // lblBatchState
            // 
            this.lblBatchState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBatchState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblBatchState.ForeColor = System.Drawing.Color.Blue;
            this.lblBatchState.Location = new System.Drawing.Point(5, 49);
            this.lblBatchState.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBatchState.Name = "lblBatchState";
            this.lblBatchState.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblBatchState.Size = new System.Drawing.Size(141, 22);
            this.lblBatchState.TabIndex = 183;
            this.lblBatchState.Text = "#";
            // 
            // Label22
            // 
            this.Label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label22.Location = new System.Drawing.Point(1137, 78);
            this.Label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label22.Name = "Label22";
            this.Label22.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label22.Size = new System.Drawing.Size(109, 22);
            this.Label22.TabIndex = 181;
            this.Label22.Text = "تعداد سفارش:";
            // 
            // lblPlanningStart1
            // 
            this.lblPlanningStart1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPlanningStart1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblPlanningStart1.Location = new System.Drawing.Point(557, 78);
            this.lblPlanningStart1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPlanningStart1.Name = "lblPlanningStart1";
            this.lblPlanningStart1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblPlanningStart1.Size = new System.Drawing.Size(157, 22);
            this.lblPlanningStart1.TabIndex = 180;
            this.lblPlanningStart1.Text = "تاریخ پایان برنامه ریزی:";
            // 
            // lblOrderQuantity
            // 
            this.lblOrderQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOrderQuantity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblOrderQuantity.ForeColor = System.Drawing.Color.Blue;
            this.lblOrderQuantity.Location = new System.Drawing.Point(1016, 78);
            this.lblOrderQuantity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOrderQuantity.Name = "lblOrderQuantity";
            this.lblOrderQuantity.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblOrderQuantity.Size = new System.Drawing.Size(121, 22);
            this.lblOrderQuantity.TabIndex = 179;
            this.lblOrderQuantity.Text = "#";
            // 
            // lblOrderProductionProgress
            // 
            this.lblOrderProductionProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOrderProductionProgress.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblOrderProductionProgress.ForeColor = System.Drawing.Color.Blue;
            this.lblOrderProductionProgress.Location = new System.Drawing.Point(739, 78);
            this.lblOrderProductionProgress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOrderProductionProgress.Name = "lblOrderProductionProgress";
            this.lblOrderProductionProgress.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblOrderProductionProgress.Size = new System.Drawing.Size(103, 22);
            this.lblOrderProductionProgress.TabIndex = 178;
            this.lblOrderProductionProgress.Text = "#";
            // 
            // Label24
            // 
            this.Label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label24.Location = new System.Drawing.Point(344, 49);
            this.Label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label24.Name = "Label24";
            this.Label24.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label24.Size = new System.Drawing.Size(107, 22);
            this.Label24.TabIndex = 177;
            this.Label24.Text = "تعداد تولید بچ:";
            // 
            // Label25
            // 
            this.Label25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label25.Location = new System.Drawing.Point(841, 78);
            this.Label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label25.Name = "Label25";
            this.Label25.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label25.Size = new System.Drawing.Size(165, 22);
            this.Label25.TabIndex = 176;
            this.Label25.Text = "پیشرفت تولید سفارش:";
            // 
            // lblBatchProductionQuantity
            // 
            this.lblBatchProductionQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBatchProductionQuantity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblBatchProductionQuantity.ForeColor = System.Drawing.Color.Blue;
            this.lblBatchProductionQuantity.Location = new System.Drawing.Point(236, 49);
            this.lblBatchProductionQuantity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBatchProductionQuantity.Name = "lblBatchProductionQuantity";
            this.lblBatchProductionQuantity.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblBatchProductionQuantity.Size = new System.Drawing.Size(108, 22);
            this.lblBatchProductionQuantity.TabIndex = 175;
            this.lblBatchProductionQuantity.Text = "#";
            // 
            // lblProductName
            // 
            this.lblProductName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProductName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblProductName.ForeColor = System.Drawing.Color.Blue;
            this.lblProductName.Location = new System.Drawing.Point(7, 23);
            this.lblProductName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblProductName.Size = new System.Drawing.Size(356, 22);
            this.lblProductName.TabIndex = 174;
            this.lblProductName.Text = "#";
            // 
            // Label20
            // 
            this.Label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label20.Location = new System.Drawing.Point(628, 23);
            this.Label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label20.Name = "Label20";
            this.Label20.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label20.Size = new System.Drawing.Size(87, 22);
            this.Label20.TabIndex = 173;
            this.Label20.Text = "کد محصول:";
            // 
            // Label21
            // 
            this.Label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label21.Location = new System.Drawing.Point(363, 23);
            this.Label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label21.Name = "Label21";
            this.Label21.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label21.Size = new System.Drawing.Size(88, 22);
            this.Label21.TabIndex = 172;
            this.Label21.Text = "نام محصول:";
            // 
            // lblProductCode
            // 
            this.lblProductCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProductCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblProductCode.ForeColor = System.Drawing.Color.Blue;
            this.lblProductCode.Location = new System.Drawing.Point(508, 23);
            this.lblProductCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProductCode.Name = "lblProductCode";
            this.lblProductCode.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblProductCode.Size = new System.Drawing.Size(120, 22);
            this.lblProductCode.TabIndex = 171;
            this.lblProductCode.Text = "#";
            // 
            // lblBatchProgress
            // 
            this.lblBatchProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBatchProgress.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblBatchProgress.ForeColor = System.Drawing.Color.Blue;
            this.lblBatchProgress.Location = new System.Drawing.Point(739, 49);
            this.lblBatchProgress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBatchProgress.Name = "lblBatchProgress";
            this.lblBatchProgress.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblBatchProgress.Size = new System.Drawing.Size(139, 22);
            this.lblBatchProgress.TabIndex = 170;
            this.lblBatchProgress.Text = "#";
            // 
            // Label16
            // 
            this.Label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label16.Location = new System.Drawing.Point(1193, 49);
            this.Label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label16.Name = "Label16";
            this.Label16.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label16.Size = new System.Drawing.Size(53, 22);
            this.Label16.TabIndex = 169;
            this.Label16.Text = "کد بچ:";
            // 
            // Label17
            // 
            this.Label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label17.Location = new System.Drawing.Point(881, 49);
            this.Label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label17.Name = "Label17";
            this.Label17.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label17.Size = new System.Drawing.Size(125, 22);
            this.Label17.TabIndex = 168;
            this.Label17.Text = "پیشرفت تولید بچ:";
            // 
            // lblBatchCode
            // 
            this.lblBatchCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBatchCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblBatchCode.ForeColor = System.Drawing.Color.Blue;
            this.lblBatchCode.Location = new System.Drawing.Point(1016, 49);
            this.lblBatchCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBatchCode.Name = "lblBatchCode";
            this.lblBatchCode.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblBatchCode.Size = new System.Drawing.Size(179, 22);
            this.lblBatchCode.TabIndex = 167;
            this.lblBatchCode.Text = "#";
            // 
            // Label13
            // 
            this.Label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label13.Location = new System.Drawing.Point(1127, 23);
            this.Label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label13.Name = "Label13";
            this.Label13.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label13.Size = new System.Drawing.Size(120, 22);
            this.Label13.TabIndex = 165;
            this.Label13.Text = "شمارۀ سفارش:";
            // 
            // Label12
            // 
            this.Label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label12.Location = new System.Drawing.Point(819, 23);
            this.Label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label12.Name = "Label12";
            this.Label12.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label12.Size = new System.Drawing.Size(188, 22);
            this.Label12.TabIndex = 164;
            this.Label12.Text = "تاریخ دستور شروع ساخت:";
            // 
            // lblOrderNo
            // 
            this.lblOrderNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOrderNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblOrderNo.ForeColor = System.Drawing.Color.Blue;
            this.lblOrderNo.Location = new System.Drawing.Point(1016, 23);
            this.lblOrderNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOrderNo.Name = "lblOrderNo";
            this.lblOrderNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblOrderNo.Size = new System.Drawing.Size(111, 22);
            this.lblOrderNo.TabIndex = 163;
            this.lblOrderNo.Text = "#";
            // 
            // lblProductionCallDate
            // 
            this.lblProductionCallDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProductionCallDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblProductionCallDate.ForeColor = System.Drawing.Color.Blue;
            this.lblProductionCallDate.Location = new System.Drawing.Point(719, 23);
            this.lblProductionCallDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProductionCallDate.Name = "lblProductionCallDate";
            this.lblProductionCallDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblProductionCallDate.Size = new System.Drawing.Size(103, 22);
            this.lblProductionCallDate.TabIndex = 166;
            this.lblProductionCallDate.Text = "#";
            // 
            // lblPlanningEnd
            // 
            this.lblPlanningEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPlanningEnd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblPlanningEnd.ForeColor = System.Drawing.Color.Blue;
            this.lblPlanningEnd.Location = new System.Drawing.Point(452, 78);
            this.lblPlanningEnd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPlanningEnd.Name = "lblPlanningEnd";
            this.lblPlanningEnd.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblPlanningEnd.Size = new System.Drawing.Size(105, 22);
            this.lblPlanningEnd.TabIndex = 182;
            this.lblPlanningEnd.Text = "#";
            // 
            // _cmdConfirm
            // 
            this._cmdConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._cmdConfirm.BackColor = System.Drawing.Color.Transparent;
            this._cmdConfirm.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdConfirm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdConfirm.Location = new System.Drawing.Point(147, 584);
            this._cmdConfirm.Margin = new System.Windows.Forms.Padding(4);
            this._cmdConfirm.Name = "_cmdConfirm";
            this._cmdConfirm.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdConfirm.Size = new System.Drawing.Size(148, 33);
            this._cmdConfirm.TabIndex = 20;
            this._cmdConfirm.Tag = "1";
            this._cmdConfirm.Text = "تایید سفارش";
            this._cmdConfirm.UseVisualStyleBackColor = false;
            this._cmdConfirm.Click += new System.EventHandler(this.cmdConfirm_Click);
            // 
            // _cmdAllow
            // 
            this._cmdAllow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._cmdAllow.BackColor = System.Drawing.Color.Transparent;
            this._cmdAllow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdAllow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdAllow.Location = new System.Drawing.Point(303, 584);
            this._cmdAllow.Margin = new System.Windows.Forms.Padding(4);
            this._cmdAllow.Name = "_cmdAllow";
            this._cmdAllow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdAllow.Size = new System.Drawing.Size(148, 33);
            this._cmdAllow.TabIndex = 19;
            this._cmdAllow.Tag = "1";
            this._cmdAllow.Text = "تصویب سفارش";
            this._cmdAllow.UseVisualStyleBackColor = false;
            this._cmdAllow.Click += new System.EventHandler(this.cmdAllow_Click);
            // 
            // _tmrCalcOrderProgress
            // 
            this._tmrCalcOrderProgress.Interval = 50;
            this._tmrCalcOrderProgress.Tick += new System.EventHandler(this.tmrCalcOrderProgress_Tick);
            // 
            // frmOrdersList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cmdExit;
            this.ClientSize = new System.Drawing.Size(1271, 653);
            this.Controls.Add(this._cmdConfirm);
            this.Controls.Add(this._cmdAllow);
            this.Controls.Add(this._cmdExit);
            this.Controls.Add(this.Panel3);
            this.Controls.Add(this.GroupBox4);
            this.Controls.Add(this._cmdDelete);
            this.Controls.Add(this._cmdInsert);
            this.Controls.Add(this._cmdUpdate);
            this.Controls.Add(this.StatusStrip1);
            this.Controls.Add(this.Panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1195, 675);
            this.Name = "frmOrdersList";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " لیست سفارشات مشتریان";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOrdersList_FormClosing);
            this.Load += new System.EventHandler(this.frmOrdersList_Load);
            this.Resize += new System.EventHandler(this.frmOrdersList_Resize);
            this.StatusStrip1.ResumeLayout(false);
            this.StatusStrip1.PerformLayout();
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.Panel5.ResumeLayout(false);
            this.Panel5.PerformLayout();
            this.Panel4.ResumeLayout(false);
            this.Panel4.PerformLayout();
            this.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dgList)).EndInit();
            this.cmsOrder.ResumeLayout(false);
            this.GroupBox4.ResumeLayout(false);
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

        internal System.Windows.Forms.Panel Panel1;
        private DataGridView _dgList;

        internal DataGridView dgList
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _dgList;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_dgList != null)
                {
                    _dgList.RowEnter -= dgList_RowEnter;
                    _dgList.CellFormatting -= dgList_CellFormatting;
                    _dgList.MouseClick -= dgList_MouseClick;
                    _dgList.KeyDown -= dgList_KeyDown;
                }

                _dgList = value;
                if (_dgList != null)
                {
                    _dgList.RowEnter += dgList_RowEnter;
                    _dgList.CellFormatting += dgList_CellFormatting;
                    _dgList.MouseClick += dgList_MouseClick;
                    _dgList.KeyDown += dgList_KeyDown;
                }
            }
        }

        internal System.Windows.Forms.Panel Panel3;

       
        internal Label Label1;
        internal Label Label4;
        internal TextBox txtOrderNo;
        internal Label Label3;
        internal Label Label2;
        internal PSB_FarsiDateControl.PSB_DateControl txtRegisterFrom;
        internal PSB_FarsiDateControl.PSB_DateControl txtRegisterTo;
        internal Label Label6;
        internal PSB_FarsiDateControl.PSB_DateControl txtOrderTo;
        internal Label Label5;
        internal Label Label7;
        internal PSB_FarsiDateControl.PSB_DateControl txtOrderFrom;
        internal PSB_FarsiDateControl.PSB_DateControl txtDeliveryTo;
        internal Label Label8;
        internal Label Label9;
        internal PSB_FarsiDateControl.PSB_DateControl txtDeliveryFrom;
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

        private CheckBox _chkConfirmed;

        internal CheckBox chkConfirmed
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _chkConfirmed;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_chkConfirmed != null)
                {
                    _chkConfirmed.CheckedChanged -= chkConfirmed_CheckedChanged;
                }

                _chkConfirmed = value;
                if (_chkConfirmed != null)
                {
                    _chkConfirmed.CheckedChanged += chkConfirmed_CheckedChanged;
                }
            }
        }

        private CheckBox _chkNotConfirmed;

        internal CheckBox chkNotConfirmed
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _chkNotConfirmed;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_chkNotConfirmed != null)
                {
                    _chkNotConfirmed.CheckedChanged -= chkNotConfirmed_CheckedChanged;
                }

                _chkNotConfirmed = value;
                if (_chkNotConfirmed != null)
                {
                    _chkNotConfirmed.CheckedChanged += chkNotConfirmed_CheckedChanged;
                }
            }
        }

        private CheckBox _chkAllowed;

        internal CheckBox chkAllowed
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _chkAllowed;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_chkAllowed != null)
                {
                    _chkAllowed.CheckedChanged -= chkAllowed_CheckedChanged;
                }

                _chkAllowed = value;
                if (_chkAllowed != null)
                {
                    _chkAllowed.CheckedChanged += chkAllowed_CheckedChanged;
                }
            }
        }

        private CheckBox _chkNotAllowed;

        internal CheckBox chkNotAllowed
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _chkNotAllowed;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_chkNotAllowed != null)
                {
                    _chkNotAllowed.CheckedChanged -= chkNotAllowed_CheckedChanged;
                }

                _chkNotAllowed = value;
                if (_chkNotAllowed != null)
                {
                    _chkNotAllowed.CheckedChanged += chkNotAllowed_CheckedChanged;
                }
            }
        }

        private CheckBox _chkAllRows;

        internal CheckBox chkAllRows
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _chkAllRows;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_chkAllRows != null)
                {
                    _chkAllRows.CheckedChanged -= chkAllRows_CheckedChanged;
                }

                _chkAllRows = value;
                if (_chkAllRows != null)
                {
                    _chkAllRows.CheckedChanged += chkAllRows_CheckedChanged;
                }
            }
        }

        internal ContextMenuStrip cmsOrder;
        internal System.Windows.Forms.Panel Panel5;
        internal System.Windows.Forms.Panel Panel4;
        internal Label Label10;
        internal TextBox txtContractNo;
        internal GroupBox GroupBox4;
        internal Label lblOrderProductionProgress;
        internal Label Label24;
        internal Label Label25;
        internal Label lblBatchProductionQuantity;
        internal Label lblProductName;
        internal Label Label20;
        internal Label Label21;
        internal Label lblProductCode;
        internal Label lblBatchProgress;
        internal Label Label16;
        internal Label Label17;
        internal Label lblBatchCode;
        internal Label lblProductionCallDate;
        internal Label Label13;
        internal Label Label12;
        internal Label lblOrderNo;
        internal Label lblRealEnd;
        internal Label Label14;
        internal Label Label15;
        internal Label lblBatchState;
        internal Label lblPlanningEnd;
        internal Label Label22;
        internal Label lblPlanningStart1;
        internal Label lblOrderQuantity;
        private ToolStripMenuItem _cmsiProductionDone;

        internal ToolStripMenuItem cmsiProductionDone
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmsiProductionDone;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmsiProductionDone != null)
                {
                    _cmsiProductionDone.Click -= cmsiProductionDone_Click;
                }

                _cmsiProductionDone = value;
                if (_cmsiProductionDone != null)
                {
                    _cmsiProductionDone.Click += cmsiProductionDone_Click;
                }
            }
        }

        private ToolStripMenuItem _cmsiCancelProductionDone;

        internal ToolStripMenuItem cmsiCancelProductionDone
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmsiCancelProductionDone;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmsiCancelProductionDone != null)
                {
                    _cmsiCancelProductionDone.Click -= cmsiCancelProductionDone_Click;
                }

                _cmsiCancelProductionDone = value;
                if (_cmsiCancelProductionDone != null)
                {
                    _cmsiCancelProductionDone.Click += cmsiCancelProductionDone_Click;
                }
            }
        }

        internal System.Windows.Forms.Panel Panel2;
        private CheckBox _chkUnProduction;

        internal CheckBox chkUnProduction
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _chkUnProduction;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_chkUnProduction != null)
                {
                    _chkUnProduction.CheckedChanged -= chkUnProduction_CheckedChanged;
                }

                _chkUnProduction = value;
                if (_chkUnProduction != null)
                {
                    _chkUnProduction.CheckedChanged += chkUnProduction_CheckedChanged;
                }
            }
        }

        private CheckBox _chkProductionDone;

        internal CheckBox chkProductionDone
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _chkProductionDone;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_chkProductionDone != null)
                {
                    _chkProductionDone.CheckedChanged -= chkProductionDone_CheckedChanged;
                }

                _chkProductionDone = value;
                if (_chkProductionDone != null)
                {
                    _chkProductionDone.CheckedChanged += chkProductionDone_CheckedChanged;
                }
            }
        }

        private System.Windows.Forms.Button _cmdConfirm;

        internal System.Windows.Forms.Button cmdConfirm
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdConfirm;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdConfirm != null)
                {
                    _cmdConfirm.Click -= cmdConfirm_Click;
                }

                _cmdConfirm = value;
                if (_cmdConfirm != null)
                {
                    _cmdConfirm.Click += cmdConfirm_Click;
                }
            }
        }

        private System.Windows.Forms.Button _cmdAllow;

        internal System.Windows.Forms.Button cmdAllow
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdAllow;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdAllow != null)
                {
                    _cmdAllow.Click -= cmdAllow_Click;
                }

                _cmdAllow = value;
                if (_cmdAllow != null)
                {
                    _cmdAllow.Click += cmdAllow_Click;
                }
            }
        }

        internal Label Label11;
        internal Label lblBatchQuantity;
        private Timer _tmrCalcOrderProgress;
        internal Label label23;
        internal Label label18;
        internal Label label19;
        private Controls.UserControl_LookUp Product_Lookup;
        private Controls.UserControl_LookUp Customer_LookUp;

        internal Timer tmrCalcOrderProgress
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _tmrCalcOrderProgress;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_tmrCalcOrderProgress != null)
                {
                    _tmrCalcOrderProgress.Tick -= tmrCalcOrderProgress_Tick;
                }

                _tmrCalcOrderProgress = value;
                if (_tmrCalcOrderProgress != null)
                {
                    _tmrCalcOrderProgress.Tick += tmrCalcOrderProgress_Tick;
                }
            }
        }
    }
}