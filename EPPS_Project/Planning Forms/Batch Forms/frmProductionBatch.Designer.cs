using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmProductionBatch : Form
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Label4 = new System.Windows.Forms.Label();
            this.Panel1 = new System.Windows.Forms.Panel();
            this._cmdSave = new System.Windows.Forms.Button();
            this._cmdExit = new System.Windows.Forms.Button();
            this._cmdDelete = new System.Windows.Forms.Button();
            this._cbProduct = new System.Windows.Forms.ComboBox();
            this.txtProductionQuantity = new System.Windows.Forms.NumericUpDown();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this._cbProductTree = new System.Windows.Forms.ComboBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtContractNo = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.txtSubbatchQuantity = new System.Windows.Forms.NumericUpDown();
            this.Label6 = new System.Windows.Forms.Label();
            this.dgDetails = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtTransferMinQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblMinDelivary = new System.Windows.Forms.Label();
            this.txtDefineDate = new PSB_FarsiDateControl.PSB_DateControl();
            this.txtDeliveryDate = new PSB_FarsiDateControl.PSB_DateControl();
            this._dgOrders = new System.Windows.Forms.DataGridView();
            this.colOrderIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colorderSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrderQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCoutomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDeliveryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtProductionCallDate = new PSB_FarsiDateControl.PSB_DateControl();
            this.label8 = new System.Windows.Forms.Label();
            this.cbProduct_LookUp = new ProductionPlanning.Controls.UserControl_LookUp();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductionQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubbatchQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTransferMinQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // Label4
            // 
            this.Label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label4.Location = new System.Drawing.Point(529, 57);
            this.Label4.Name = "Label4";
            this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label4.Size = new System.Drawing.Size(89, 32);
            this.Label4.TabIndex = 144;
            this.Label4.Text = "نام محصول:";
            // 
            // Panel1
            // 
            this.Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this._cmdSave);
            this.Panel1.Controls.Add(this._cmdExit);
            this.Panel1.Controls.Add(this._cmdDelete);
            this.Panel1.Location = new System.Drawing.Point(10, 532);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(608, 48);
            this.Panel1.TabIndex = 10;
            // 
            // _cmdSave
            // 
            this._cmdSave.BackColor = System.Drawing.Color.Transparent;
            this._cmdSave.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdSave.ForeColor = System.Drawing.Color.Black;
            this._cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdSave.Location = new System.Drawing.Point(408, 10);
            this._cmdSave.Name = "_cmdSave";
            this._cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdSave.Size = new System.Drawing.Size(90, 28);
            this._cmdSave.TabIndex = 10;
            this._cmdSave.Text = "ثبت";
            this._cmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._cmdSave.UseVisualStyleBackColor = false;
            this._cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // _cmdExit
            // 
            this._cmdExit.BackColor = System.Drawing.Color.Transparent;
            this._cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cmdExit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdExit.ForeColor = System.Drawing.Color.Red;
            this._cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdExit.Location = new System.Drawing.Point(205, 10);
            this._cmdExit.Name = "_cmdExit";
            this._cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdExit.Size = new System.Drawing.Size(90, 28);
            this._cmdExit.TabIndex = 11;
            this._cmdExit.Text = "انصراف";
            this._cmdExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._cmdExit.UseVisualStyleBackColor = false;
            this._cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // _cmdDelete
            // 
            this._cmdDelete.BackColor = System.Drawing.Color.Transparent;
            this._cmdDelete.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdDelete.ForeColor = System.Drawing.Color.Black;
            this._cmdDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdDelete.Location = new System.Drawing.Point(312, 10);
            this._cmdDelete.Name = "_cmdDelete";
            this._cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdDelete.Size = new System.Drawing.Size(90, 28);
            this._cmdDelete.TabIndex = 12;
            this._cmdDelete.Text = "حذف";
            this._cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._cmdDelete.UseVisualStyleBackColor = false;
            this._cmdDelete.Visible = false;
            this._cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // _cbProduct
            // 
            this._cbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cbProduct.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cbProduct.FormattingEnabled = true;
            this._cbProduct.Location = new System.Drawing.Point(63, 88);
            this._cbProduct.Name = "_cbProduct";
            this._cbProduct.Size = new System.Drawing.Size(218, 25);
            this._cbProduct.TabIndex = 3;
            this._cbProduct.Visible = false;
            // 
            // txtProductionQuantity
            // 
            this.txtProductionQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProductionQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtProductionQuantity.Location = new System.Drawing.Point(186, 19);
            this.txtProductionQuantity.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.txtProductionQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtProductionQuantity.Name = "txtProductionQuantity";
            this.txtProductionQuantity.Size = new System.Drawing.Size(77, 26);
            this.txtProductionQuantity.TabIndex = 1;
            this.txtProductionQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.SystemColors.Control;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label7.ForeColor = System.Drawing.Color.Black;
            this.Label7.Location = new System.Drawing.Point(261, 22);
            this.Label7.Name = "Label7";
            this.Label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label7.Size = new System.Drawing.Size(52, 17);
            this.Label7.TabIndex = 157;
            this.Label7.Text = "تعداد بچ:";
            // 
            // Label9
            // 
            this.Label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label9.Location = new System.Drawing.Point(558, 94);
            this.Label9.Name = "Label9";
            this.Label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label9.Size = new System.Drawing.Size(56, 23);
            this.Label9.TabIndex = 161;
            this.Label9.Text = "درخت محصول:";
            // 
            // _cbProductTree
            // 
            this._cbProductTree.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cbProductTree.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cbProductTree.FormattingEnabled = true;
            this._cbProductTree.Location = new System.Drawing.Point(322, 92);
            this._cbProductTree.Name = "_cbProductTree";
            this._cbProductTree.Size = new System.Drawing.Size(231, 25);
            this._cbProductTree.TabIndex = 4;
            this._cbProductTree.SelectedValueChanged += new System.EventHandler(this.cbProductTree_SelectedValueChanged);
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtCode.Location = new System.Drawing.Point(336, 20);
            this.txtCode.MaxLength = 18;
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(218, 21);
            this.txtCode.TabIndex = 0;
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.SystemColors.Control;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label3.ForeColor = System.Drawing.Color.Black;
            this.Label3.Location = new System.Drawing.Point(576, 19);
            this.Label3.Name = "Label3";
            this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label3.Size = new System.Drawing.Size(40, 17);
            this.Label3.TabIndex = 165;
            this.Label3.Text = "کد بچ:";
            // 
            // txtContractNo
            // 
            this.txtContractNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtContractNo.Location = new System.Drawing.Point(10, 300);
            this.txtContractNo.Multiline = true;
            this.txtContractNo.Name = "txtContractNo";
            this.txtContractNo.Size = new System.Drawing.Size(150, 21);
            this.txtContractNo.TabIndex = 5;
            this.txtContractNo.Visible = false;
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.SystemColors.Control;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label1.ForeColor = System.Drawing.Color.Black;
            this.Label1.Location = new System.Drawing.Point(158, 302);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label1.Size = new System.Drawing.Size(81, 19);
            this.Label1.TabIndex = 167;
            this.Label1.Text = "شماره قرارداد:";
            this.Label1.Visible = false;
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label2.Location = new System.Drawing.Point(535, 335);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label2.Size = new System.Drawing.Size(81, 19);
            this.Label2.TabIndex = 170;
            this.Label2.Text = "تاریخ تعریف بچ:";
            // 
            // Label5
            // 
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label5.Location = new System.Drawing.Point(125, 335);
            this.Label5.Name = "Label5";
            this.Label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label5.Size = new System.Drawing.Size(134, 19);
            this.Label5.TabIndex = 172;
            this.Label5.Text = "تاریخ تحویل اولین محموله:";
            this.Label5.Visible = false;
            // 
            // txtSubbatchQuantity
            // 
            this.txtSubbatchQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSubbatchQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtSubbatchQuantity.Location = new System.Drawing.Point(33, 19);
            this.txtSubbatchQuantity.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.txtSubbatchQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtSubbatchQuantity.Name = "txtSubbatchQuantity";
            this.txtSubbatchQuantity.Size = new System.Drawing.Size(58, 26);
            this.txtSubbatchQuantity.TabIndex = 2;
            this.txtSubbatchQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.SystemColors.Control;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label6.ForeColor = System.Drawing.Color.Black;
            this.Label6.Location = new System.Drawing.Point(94, 22);
            this.Label6.Name = "Label6";
            this.Label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label6.Size = new System.Drawing.Size(82, 17);
            this.Label6.TabIndex = 173;
            this.Label6.Text = "تعداد ساب بچ:";
            // 
            // dgDetails
            // 
            this.dgDetails.AllowUserToAddRows = false;
            this.dgDetails.AllowUserToResizeColumns = false;
            this.dgDetails.AllowUserToResizeRows = false;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            this.dgDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgDetails.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgDetails.ColumnHeadersHeight = 30;
            this.dgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column1,
            this.Column4,
            this.Column2,
            this.Column5,
            this.Column6});
            this.dgDetails.Location = new System.Drawing.Point(10, 128);
            this.dgDetails.Name = "dgDetails";
            this.dgDetails.RowHeadersWidth = 10;
            this.dgDetails.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dgDetails.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgDetails.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dgDetails.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.dgDetails.Size = new System.Drawing.Size(608, 165);
            this.dgDetails.TabIndex = 177;
            this.dgDetails.TabStop = false;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "کدجزء";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 110;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "نام جزء";
            this.Column1.MinimumWidth = 120;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 215;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "ضریب مصرف";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 80;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "تعداد موجودی";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 80;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "تعداد مورد نیاز";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column5.Width = 85;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "کد جزء پدر";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.Visible = false;
            this.Column6.Width = 125;
            // 
            // txtTransferMinQuantity
            // 
            this.txtTransferMinQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTransferMinQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtTransferMinQuantity.Location = new System.Drawing.Point(15, 328);
            this.txtTransferMinQuantity.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.txtTransferMinQuantity.Name = "txtTransferMinQuantity";
            this.txtTransferMinQuantity.Size = new System.Drawing.Size(105, 26);
            this.txtTransferMinQuantity.TabIndex = 6;
            this.txtTransferMinQuantity.Visible = false;
            // 
            // lblMinDelivary
            // 
            this.lblMinDelivary.BackColor = System.Drawing.SystemColors.Control;
            this.lblMinDelivary.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblMinDelivary.ForeColor = System.Drawing.Color.Black;
            this.lblMinDelivary.Location = new System.Drawing.Point(119, 335);
            this.lblMinDelivary.Name = "lblMinDelivary";
            this.lblMinDelivary.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblMinDelivary.Size = new System.Drawing.Size(147, 19);
            this.lblMinDelivary.TabIndex = 178;
            this.lblMinDelivary.Text = "تعداد اولین محموله ارسالی:";
            this.lblMinDelivary.Visible = false;
            // 
            // txtDefineDate
            // 
            this.txtDefineDate.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.txtDefineDate.BackColor = System.Drawing.Color.White;
            this.txtDefineDate.BackColorDateBox = System.Drawing.Color.White;
            this.txtDefineDate.DateButtonShow = true;
            this.txtDefineDate.EnableDateText = true;
            this.txtDefineDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtDefineDate.FontMyDate = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtDefineDate.Location = new System.Drawing.Point(425, 330);
            this.txtDefineDate.Margin = new System.Windows.Forms.Padding(4);
            this.txtDefineDate.MinimumSize = new System.Drawing.Size(96, 24);
            this.txtDefineDate.Name = "txtDefineDate";
            this.txtDefineDate.Size = new System.Drawing.Size(105, 24);
            this.txtDefineDate.TabIndex = 7;
            // 
            // txtDeliveryDate
            // 
            this.txtDeliveryDate.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.txtDeliveryDate.BackColor = System.Drawing.Color.White;
            this.txtDeliveryDate.BackColorDateBox = System.Drawing.Color.White;
            this.txtDeliveryDate.DateButtonShow = true;
            this.txtDeliveryDate.EnableDateText = true;
            this.txtDeliveryDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtDeliveryDate.FontMyDate = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtDeliveryDate.Location = new System.Drawing.Point(15, 330);
            this.txtDeliveryDate.Margin = new System.Windows.Forms.Padding(4);
            this.txtDeliveryDate.MinimumSize = new System.Drawing.Size(96, 24);
            this.txtDeliveryDate.Name = "txtDeliveryDate";
            this.txtDeliveryDate.Size = new System.Drawing.Size(108, 24);
            this.txtDeliveryDate.TabIndex = 8;
            this.txtDeliveryDate.Visible = false;
            // 
            // _dgOrders
            // 
            this._dgOrders.AllowUserToAddRows = false;
            this._dgOrders.AllowUserToResizeColumns = false;
            this._dgOrders.AllowUserToResizeRows = false;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
            this._dgOrders.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            this._dgOrders.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this._dgOrders.ColumnHeadersHeight = 30;
            this._dgOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this._dgOrders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colOrderIndex,
            this.colorderSelect,
            this.DataGridViewTextBoxColumn1,
            this.colOrderQuantity,
            this.colCoutomerName,
            this.colDeliveryDate});
            this._dgOrders.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this._dgOrders.Location = new System.Drawing.Point(10, 363);
            this._dgOrders.Name = "_dgOrders";
            this._dgOrders.RowHeadersWidth = 10;
            this._dgOrders.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this._dgOrders.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this._dgOrders.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Info;
            this._dgOrders.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._dgOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgOrders.Size = new System.Drawing.Size(608, 161);
            this._dgOrders.TabIndex = 9;
            this._dgOrders.TabStop = false;
            this._dgOrders.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgOrders_CellMouseDown);
            this._dgOrders.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgOrders_KeyDown);
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
            this.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewTextBoxColumn1.Width = 130;
            // 
            // colOrderQuantity
            // 
            this.colOrderQuantity.HeaderText = "تعداد سفارش";
            this.colOrderQuantity.MinimumWidth = 100;
            this.colOrderQuantity.Name = "colOrderQuantity";
            this.colOrderQuantity.ReadOnly = true;
            this.colOrderQuantity.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colOrderQuantity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colOrderQuantity.Width = 125;
            // 
            // colCoutomerName
            // 
            this.colCoutomerName.HeaderText = "نام مشتری";
            this.colCoutomerName.MinimumWidth = 6;
            this.colCoutomerName.Name = "colCoutomerName";
            this.colCoutomerName.ReadOnly = true;
            this.colCoutomerName.Width = 200;
            // 
            // colDeliveryDate
            // 
            this.colDeliveryDate.HeaderText = "تاریخ تحویل";
            this.colDeliveryDate.MinimumWidth = 6;
            this.colDeliveryDate.Name = "colDeliveryDate";
            this.colDeliveryDate.ReadOnly = true;
            this.colDeliveryDate.Width = 125;
            // 
            // txtProductionCallDate
            // 
            this.txtProductionCallDate.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.txtProductionCallDate.BackColor = System.Drawing.Color.White;
            this.txtProductionCallDate.BackColorDateBox = System.Drawing.Color.White;
            this.txtProductionCallDate.DateButtonShow = true;
            this.txtProductionCallDate.EnableDateText = true;
            this.txtProductionCallDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtProductionCallDate.FontMyDate = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtProductionCallDate.Location = new System.Drawing.Point(322, 297);
            this.txtProductionCallDate.Margin = new System.Windows.Forms.Padding(4);
            this.txtProductionCallDate.MinimumSize = new System.Drawing.Size(96, 24);
            this.txtProductionCallDate.Name = "txtProductionCallDate";
            this.txtProductionCallDate.Size = new System.Drawing.Size(108, 24);
            this.txtProductionCallDate.TabIndex = 180;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.Control;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(437, 297);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label8.Size = new System.Drawing.Size(179, 24);
            this.label8.TabIndex = 181;
            this.label8.Text = "تاریخ دستور شروع ساخت";
            // 
            // cbProduct_LookUp
            // 
            this.cbProduct_LookUp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cbProduct_LookUp.CB_AutoComplete = true;
            this.cbProduct_LookUp.CB_AutoDropdown = false;
            this.cbProduct_LookUp.CB_ColumnNames = "";
            this.cbProduct_LookUp.CB_ColumnWidthDefault = 75;
            this.cbProduct_LookUp.CB_ColumnWidths = "75,250";
            this.cbProduct_LookUp.CB_DataSource = null;
            this.cbProduct_LookUp.CB_DisplayMember = "";
            this.cbProduct_LookUp.CB_LinkedColumnIndex = 1;
            this.cbProduct_LookUp.CB_SelectedIndex = -1;
            this.cbProduct_LookUp.CB_SelectedValue = "";
            this.cbProduct_LookUp.CB_SerachFromTitle = null;
            this.cbProduct_LookUp.CB_ValueMember = "";
            this.cbProduct_LookUp.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProduct_LookUp.Location = new System.Drawing.Point(37, 48);
            this.cbProduct_LookUp.Name = "cbProduct_LookUp";
            this.cbProduct_LookUp.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbProduct_LookUp.Size = new System.Drawing.Size(486, 38);
            this.cbProduct_LookUp.TabIndex = 179;
            this.cbProduct_LookUp.CB_SelectedValueChanged += new System.EventHandler(this._cbProduct_LookUp_CB_SelectedValueChanged);
            // 
            // frmProductionBatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cmdExit;
            this.ClientSize = new System.Drawing.Size(630, 585);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtProductionCallDate);
            this.Controls.Add(this.cbProduct_LookUp);
            this.Controls.Add(this._dgOrders);
            this.Controls.Add(this.txtDeliveryDate);
            this.Controls.Add(this.txtDefineDate);
            this.Controls.Add(this.lblMinDelivary);
            this.Controls.Add(this.dgDetails);
            this.Controls.Add(this.txtSubbatchQuantity);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txtContractNo);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label9);
            this.Controls.Add(this._cbProductTree);
            this.Controls.Add(this.txtProductionQuantity);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this._cbProduct);
            this.Controls.Add(this.txtTransferMinQuantity);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Location = new System.Drawing.Point(50, 50);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProductionBatch";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " مشخصات بچ  تولید";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmProductionBatch_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtProductionQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubbatchQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTransferMinQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgOrders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal Label Label4;
        internal System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.Button _cmdSave;

        internal System.Windows.Forms.Button cmdSave
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdSave;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdSave != null)
                {
                    _cmdSave.Click -= cmdSave_Click;
                }

                _cmdSave = value;
                if (_cmdSave != null)
                {
                    _cmdSave.Click += cmdSave_Click;
                }
            }
        }

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

        private ComboBox _cbProduct;

        //internal ComboBox cbProduct
        //{
        //    [MethodImpl(MethodImplOptions.Synchronized)]
        //    get
        //    {
        //        return _cbProduct;
        //    }

        //    [MethodImpl(MethodImplOptions.Synchronized)]
        //    set
        //    {
        //        if (_cbProduct != null)
        //        {
        //            _cbProduct.SelectedValueChanged -= cbProduct_SelectedValueChanged;
        //        }

        //        _cbProduct = value;
        //        if (_cbProduct != null)
        //        {
        //            _cbProduct.SelectedValueChanged += cbProduct_SelectedValueChanged;
        //        }
        //    }
        //}


       


        internal NumericUpDown txtProductionQuantity;
        internal Label Label7;
        internal Label Label9;
        private ComboBox _cbProductTree;

        internal ComboBox cbProductTree
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cbProductTree;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cbProductTree != null)
                {
                    _cbProductTree.SelectedValueChanged -= cbProductTree_SelectedValueChanged;
                }

                _cbProductTree = value;
                if (_cbProductTree != null)
                {
                    _cbProductTree.SelectedValueChanged += cbProductTree_SelectedValueChanged;
                }
            }
        }

        internal TextBox txtCode;
        internal Label Label3;
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

        internal TextBox txtContractNo;
        internal Label Label1;
        internal Label Label2;
        internal Label Label5;
        internal NumericUpDown txtSubbatchQuantity;
        internal Label Label6;
        internal DataGridView dgDetails;
        internal NumericUpDown txtTransferMinQuantity;
        internal Label lblMinDelivary;
        internal PSB_FarsiDateControl.PSB_DateControl txtDefineDate;
        internal PSB_FarsiDateControl.PSB_DateControl txtDeliveryDate;
        internal DataGridViewTextBoxColumn Column3;
        internal DataGridViewTextBoxColumn Column1;
        internal DataGridViewTextBoxColumn Column4;
        internal DataGridViewTextBoxColumn Column2;
        internal DataGridViewTextBoxColumn Column5;
        internal DataGridViewTextBoxColumn Column6;
        private DataGridView _dgOrders;

        internal DataGridView dgOrders
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _dgOrders;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_dgOrders != null)
                {
                    _dgOrders.CellMouseDown -= dgOrders_CellMouseDown;
                    _dgOrders.KeyDown -= dgOrders_KeyDown;
                }

                _dgOrders = value;
                if (_dgOrders != null)
                {
                    _dgOrders.CellMouseDown += dgOrders_CellMouseDown;
                    _dgOrders.KeyDown += dgOrders_KeyDown;
                }
            }
        }

        internal DataGridViewTextBoxColumn colOrderIndex;
        internal DataGridViewCheckBoxColumn colorderSelect;
        internal DataGridViewTextBoxColumn DataGridViewTextBoxColumn1;
        internal DataGridViewTextBoxColumn colOrderQuantity;
        internal DataGridViewTextBoxColumn colCoutomerName;
        internal DataGridViewTextBoxColumn colDeliveryDate;
        private Controls.UserControl_LookUp cbProduct_LookUp;
        internal PSB_FarsiDateControl.PSB_DateControl txtProductionCallDate;
        internal Label label8;
    }
}