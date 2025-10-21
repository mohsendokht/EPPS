using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmOrder : Form
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
            this.Panel1 = new System.Windows.Forms.Panel();
            this._cmdSave = new System.Windows.Forms.Button();
            this._cmdExit = new System.Windows.Forms.Button();
            this._cmdDelete = new System.Windows.Forms.Button();
            this._chkConfirmed = new System.Windows.Forms.CheckBox();
            this._txtRegisterDate = new PSB_FarsiDateControl.PSB_DateControl();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this._txtOrderNo = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this._txtOrderDate = new PSB_FarsiDateControl.PSB_DateControl();
            this.Label9 = new System.Windows.Forms.Label();
            this._txtProductTechnicalSpecification = new System.Windows.Forms.TextBox();
            this._txtQuantity = new System.Windows.Forms.NumericUpDown();
            this.Label5 = new System.Windows.Forms.Label();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this._txtDeliveryDate = new PSB_FarsiDateControl.PSB_DateControl();
            this.Label6 = new System.Windows.Forms.Label();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.Customer_LookUp = new ProductionPlanning.Controls.UserControl_LookUp();
            this.Product_Lookup = new ProductionPlanning.Controls.UserControl_LookUp();
            this.Label7 = new System.Windows.Forms.Label();
            this._txtContractNo = new System.Windows.Forms.TextBox();
            this._chkAllowed = new System.Windows.Forms.CheckBox();
            this.gbAllowed = new System.Windows.Forms.GroupBox();
            this._txtProductionCallDate = new PSB_FarsiDateControl.PSB_DateControl();
            this.Label10 = new System.Windows.Forms.Label();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._txtQuantity)).BeginInit();
            this.GroupBox1.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.gbAllowed.SuspendLayout();
            this.SuspendLayout();
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
            this.Panel1.ForeColor = System.Drawing.Color.Black;
            this.Panel1.Location = new System.Drawing.Point(10, 448);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(665, 48);
            this.Panel1.TabIndex = 6;
            // 
            // _cmdSave
            // 
            this._cmdSave.BackColor = System.Drawing.Color.Transparent;
            this._cmdSave.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdSave.ForeColor = System.Drawing.Color.Black;
            this._cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdSave.Location = new System.Drawing.Point(325, 10);
            this._cmdSave.Name = "_cmdSave";
            this._cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdSave.Size = new System.Drawing.Size(90, 28);
            this._cmdSave.TabIndex = 0;
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
            this._cmdExit.Location = new System.Drawing.Point(204, 10);
            this._cmdExit.Name = "_cmdExit";
            this._cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdExit.Size = new System.Drawing.Size(90, 28);
            this._cmdExit.TabIndex = 1;
            this._cmdExit.Text = "انصراف";
            this._cmdExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._cmdExit.UseVisualStyleBackColor = false;
            this._cmdExit.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // _cmdDelete
            // 
            this._cmdDelete.BackColor = System.Drawing.Color.Transparent;
            this._cmdDelete.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._cmdDelete.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdDelete.ForeColor = System.Drawing.Color.Black;
            this._cmdDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdDelete.Location = new System.Drawing.Point(325, 10);
            this._cmdDelete.Name = "_cmdDelete";
            this._cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdDelete.Size = new System.Drawing.Size(90, 28);
            this._cmdDelete.TabIndex = 2;
            this._cmdDelete.Text = "حذف";
            this._cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._cmdDelete.UseVisualStyleBackColor = false;
            this._cmdDelete.Visible = false;
            this._cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // _chkConfirmed
            // 
            this._chkConfirmed.AutoSize = true;
            this._chkConfirmed.Location = new System.Drawing.Point(513, 397);
            this._chkConfirmed.Name = "_chkConfirmed";
            this._chkConfirmed.Size = new System.Drawing.Size(106, 21);
            this._chkConfirmed.TabIndex = 4;
            this._chkConfirmed.Text = "تایید سفارش";
            this._chkConfirmed.UseVisualStyleBackColor = true;
            this._chkConfirmed.CheckedChanged += new System.EventHandler(this.chkConfirmed_CheckedChanged);
            this._chkConfirmed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPersonnelCode_KeyPress);
            // 
            // _txtRegisterDate
            // 
            this._txtRegisterDate.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this._txtRegisterDate.BackColor = System.Drawing.SystemColors.Control;
            this._txtRegisterDate.BackColorDateBox = System.Drawing.Color.White;
            this._txtRegisterDate.DateButtonShow = true;
            this._txtRegisterDate.EnableDateText = true;
            this._txtRegisterDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._txtRegisterDate.FontMyDate = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._txtRegisterDate.Location = new System.Drawing.Point(26, 18);
            this._txtRegisterDate.Margin = new System.Windows.Forms.Padding(4);
            this._txtRegisterDate.MinimumSize = new System.Drawing.Size(96, 24);
            this._txtRegisterDate.Name = "_txtRegisterDate";
            this._txtRegisterDate.Size = new System.Drawing.Size(98, 24);
            this._txtRegisterDate.TabIndex = 0;
            this._txtRegisterDate.KeyPress += new PSB_FarsiDateControl.PSB_DateControl.KeyPressEventHandler(this.txtPersonnelCode_KeyPress);
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label1.Location = new System.Drawing.Point(126, 18);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label1.Size = new System.Drawing.Size(114, 24);
            this.Label1.TabIndex = 213;
            this.Label1.Text = "تاریخ ثبت:";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label4
            // 
            this.Label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label4.Location = new System.Drawing.Point(533, 25);
            this.Label4.Name = "Label4";
            this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label4.Size = new System.Drawing.Size(115, 24);
            this.Label4.TabIndex = 214;
            this.Label4.Text = "شمارۀ سفارش:";
            // 
            // _txtOrderNo
            // 
            this._txtOrderNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._txtOrderNo.BackColor = System.Drawing.Color.White;
            this._txtOrderNo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._txtOrderNo.Location = new System.Drawing.Point(381, 22);
            this._txtOrderNo.MaxLength = 25;
            this._txtOrderNo.Name = "_txtOrderNo";
            this._txtOrderNo.Size = new System.Drawing.Size(146, 27);
            this._txtOrderNo.TabIndex = 0;
            this._txtOrderNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._txtOrderNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPersonnelCode_KeyPress);
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label2.Location = new System.Drawing.Point(588, 21);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label2.Size = new System.Drawing.Size(68, 20);
            this.Label2.TabIndex = 217;
            this.Label2.Text = "مشتری:";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label3
            // 
            this.Label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label3.Location = new System.Drawing.Point(590, 56);
            this.Label3.Name = "Label3";
            this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label3.Size = new System.Drawing.Size(66, 20);
            this.Label3.TabIndex = 219;
            this.Label3.Text = "محصول:";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label8
            // 
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label8.Location = new System.Drawing.Point(115, 22);
            this.Label8.Name = "Label8";
            this.Label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label8.Size = new System.Drawing.Size(130, 23);
            this.Label8.TabIndex = 221;
            this.Label8.Text = "تاریخ سفارش:";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _txtOrderDate
            // 
            this._txtOrderDate.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this._txtOrderDate.BackColor = System.Drawing.SystemColors.Control;
            this._txtOrderDate.BackColorDateBox = System.Drawing.Color.White;
            this._txtOrderDate.DateButtonShow = true;
            this._txtOrderDate.EnableDateText = true;
            this._txtOrderDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._txtOrderDate.FontMyDate = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._txtOrderDate.Location = new System.Drawing.Point(15, 21);
            this._txtOrderDate.Margin = new System.Windows.Forms.Padding(4);
            this._txtOrderDate.MinimumSize = new System.Drawing.Size(96, 24);
            this._txtOrderDate.Name = "_txtOrderDate";
            this._txtOrderDate.Size = new System.Drawing.Size(98, 24);
            this._txtOrderDate.TabIndex = 1;
            this._txtOrderDate.KeyPress += new PSB_FarsiDateControl.PSB_DateControl.KeyPressEventHandler(this.txtPersonnelCode_KeyPress);
            // 
            // Label9
            // 
            this.Label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label9.Location = new System.Drawing.Point(571, 131);
            this.Label9.Name = "Label9";
            this.Label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label9.Size = new System.Drawing.Size(85, 61);
            this.Label9.TabIndex = 222;
            this.Label9.Text = "مشخصات فنی محصول:";
            // 
            // _txtProductTechnicalSpecification
            // 
            this._txtProductTechnicalSpecification.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._txtProductTechnicalSpecification.BackColor = System.Drawing.Color.White;
            this._txtProductTechnicalSpecification.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._txtProductTechnicalSpecification.Location = new System.Drawing.Point(41, 131);
            this._txtProductTechnicalSpecification.MaxLength = 200;
            this._txtProductTechnicalSpecification.Multiline = true;
            this._txtProductTechnicalSpecification.Name = "_txtProductTechnicalSpecification";
            this._txtProductTechnicalSpecification.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._txtProductTechnicalSpecification.Size = new System.Drawing.Size(528, 61);
            this._txtProductTechnicalSpecification.TabIndex = 3;
            this._txtProductTechnicalSpecification.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPersonnelCode_KeyPress);
            // 
            // _txtQuantity
            // 
            this._txtQuantity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._txtQuantity.Location = new System.Drawing.Point(381, 61);
            this._txtQuantity.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this._txtQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._txtQuantity.Name = "_txtQuantity";
            this._txtQuantity.Size = new System.Drawing.Size(146, 26);
            this._txtQuantity.TabIndex = 2;
            this._txtQuantity.ThousandsSeparator = true;
            this._txtQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._txtQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPersonnelCode_KeyPress);
            // 
            // Label5
            // 
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label5.Location = new System.Drawing.Point(533, 59);
            this.Label5.Name = "Label5";
            this.Label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label5.Size = new System.Drawing.Size(112, 24);
            this.Label5.TabIndex = 225;
            this.Label5.Text = "تعداد سفارش:";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.Controls.Add(this._txtOrderDate);
            this.GroupBox1.Controls.Add(this._txtDeliveryDate);
            this.GroupBox1.Controls.Add(this.Label4);
            this.GroupBox1.Controls.Add(this.Label5);
            this.GroupBox1.Controls.Add(this._txtOrderNo);
            this.GroupBox1.Controls.Add(this._txtQuantity);
            this.GroupBox1.Controls.Add(this.Label8);
            this.GroupBox1.Controls.Add(this.Label6);
            this.GroupBox1.Location = new System.Drawing.Point(11, 60);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(663, 97);
            this.GroupBox1.TabIndex = 1;
            this.GroupBox1.TabStop = false;
            // 
            // _txtDeliveryDate
            // 
            this._txtDeliveryDate.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this._txtDeliveryDate.BackColor = System.Drawing.SystemColors.Control;
            this._txtDeliveryDate.BackColorDateBox = System.Drawing.Color.White;
            this._txtDeliveryDate.DateButtonShow = true;
            this._txtDeliveryDate.EnableDateText = true;
            this._txtDeliveryDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._txtDeliveryDate.FontMyDate = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._txtDeliveryDate.Location = new System.Drawing.Point(15, 59);
            this._txtDeliveryDate.Margin = new System.Windows.Forms.Padding(4);
            this._txtDeliveryDate.MinimumSize = new System.Drawing.Size(96, 24);
            this._txtDeliveryDate.Name = "_txtDeliveryDate";
            this._txtDeliveryDate.Size = new System.Drawing.Size(98, 24);
            this._txtDeliveryDate.TabIndex = 3;
            this._txtDeliveryDate.KeyPress += new PSB_FarsiDateControl.PSB_DateControl.KeyPressEventHandler(this.txtPersonnelCode_KeyPress);
            // 
            // Label6
            // 
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label6.Location = new System.Drawing.Point(115, 60);
            this.Label6.Name = "Label6";
            this.Label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label6.Size = new System.Drawing.Size(133, 23);
            this.Label6.TabIndex = 227;
            this.Label6.Text = "تاریخ موعد تحویل:";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox3
            // 
            this.GroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox3.Controls.Add(this.Customer_LookUp);
            this.GroupBox3.Controls.Add(this.Product_Lookup);
            this.GroupBox3.Controls.Add(this.Label7);
            this.GroupBox3.Controls.Add(this._txtContractNo);
            this.GroupBox3.Controls.Add(this.Label2);
            this.GroupBox3.Controls.Add(this.Label9);
            this.GroupBox3.Controls.Add(this._txtProductTechnicalSpecification);
            this.GroupBox3.Controls.Add(this.Label3);
            this.GroupBox3.Location = new System.Drawing.Point(11, 165);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(663, 198);
            this.GroupBox3.TabIndex = 2;
            this.GroupBox3.TabStop = false;
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
            this.Customer_LookUp.Location = new System.Drawing.Point(41, 17);
            this.Customer_LookUp.Name = "Customer_LookUp";
            this.Customer_LookUp.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Customer_LookUp.Size = new System.Drawing.Size(528, 33);
            this.Customer_LookUp.TabIndex = 225;
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
            this.Product_Lookup.Location = new System.Drawing.Point(38, 56);
            this.Product_Lookup.Name = "Product_Lookup";
            this.Product_Lookup.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Product_Lookup.Size = new System.Drawing.Size(531, 33);
            this.Product_Lookup.TabIndex = 1;
            // 
            // Label7
            // 
            this.Label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label7.Location = new System.Drawing.Point(553, 99);
            this.Label7.Name = "Label7";
            this.Label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label7.Size = new System.Drawing.Size(103, 23);
            this.Label7.TabIndex = 224;
            this.Label7.Text = "شمارۀ قرارداد:";
            // 
            // _txtContractNo
            // 
            this._txtContractNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._txtContractNo.BackColor = System.Drawing.Color.White;
            this._txtContractNo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._txtContractNo.Location = new System.Drawing.Point(366, 95);
            this._txtContractNo.MaxLength = 25;
            this._txtContractNo.Name = "_txtContractNo";
            this._txtContractNo.Size = new System.Drawing.Size(187, 27);
            this._txtContractNo.TabIndex = 2;
            this._txtContractNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._txtContractNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPersonnelCode_KeyPress);
            // 
            // _chkAllowed
            // 
            this._chkAllowed.AutoSize = true;
            this._chkAllowed.Location = new System.Drawing.Point(262, 28);
            this._chkAllowed.Name = "_chkAllowed";
            this._chkAllowed.Size = new System.Drawing.Size(119, 21);
            this._chkAllowed.TabIndex = 0;
            this._chkAllowed.Text = "تصویب سفارش";
            this._chkAllowed.UseVisualStyleBackColor = true;
            this._chkAllowed.CheckedChanged += new System.EventHandler(this.chkAllowed_CheckedChanged);
            this._chkAllowed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPersonnelCode_KeyPress);
            // 
            // gbAllowed
            // 
            this.gbAllowed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbAllowed.Controls.Add(this._txtProductionCallDate);
            this.gbAllowed.Controls.Add(this.Label10);
            this.gbAllowed.Controls.Add(this._chkAllowed);
            this.gbAllowed.Enabled = false;
            this.gbAllowed.Location = new System.Drawing.Point(26, 369);
            this.gbAllowed.Name = "gbAllowed";
            this.gbAllowed.Size = new System.Drawing.Size(474, 70);
            this.gbAllowed.TabIndex = 5;
            this.gbAllowed.TabStop = false;
            // 
            // _txtProductionCallDate
            // 
            this._txtProductionCallDate.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this._txtProductionCallDate.BackColor = System.Drawing.SystemColors.Control;
            this._txtProductionCallDate.BackColorDateBox = System.Drawing.Color.White;
            this._txtProductionCallDate.DateButtonShow = true;
            this._txtProductionCallDate.EnableDateText = false;
            this._txtProductionCallDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._txtProductionCallDate.FontMyDate = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._txtProductionCallDate.Location = new System.Drawing.Point(10, 29);
            this._txtProductionCallDate.Margin = new System.Windows.Forms.Padding(4);
            this._txtProductionCallDate.MinimumSize = new System.Drawing.Size(96, 24);
            this._txtProductionCallDate.Name = "_txtProductionCallDate";
            this._txtProductionCallDate.Size = new System.Drawing.Size(98, 24);
            this._txtProductionCallDate.TabIndex = 1;
            this._txtProductionCallDate.Visible = false;
            this._txtProductionCallDate.KeyPress += new PSB_FarsiDateControl.PSB_DateControl.KeyPressEventHandler(this.txtPersonnelCode_KeyPress);
            // 
            // Label10
            // 
            this.Label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label10.Location = new System.Drawing.Point(115, 29);
            this.Label10.Name = "Label10";
            this.Label10.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label10.Size = new System.Drawing.Size(141, 20);
            this.Label10.TabIndex = 230;
            this.Label10.Text = "تاریخ دستور شروع ساخت:";
            this.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label10.Visible = false;
            // 
            // frmOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cmdExit;
            this.ClientSize = new System.Drawing.Size(686, 503);
            this.Controls.Add(this._chkConfirmed);
            this.Controls.Add(this._txtRegisterDate);
            this.Controls.Add(this.gbAllowed);
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Label1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOrder";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " مشخصات سفارش مشتری";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRealProduction_FormClosing);
            this.Load += new System.EventHandler(this.frmRealProduction_Load);
            this.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._txtQuantity)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox3.PerformLayout();
            this.gbAllowed.ResumeLayout(false);
            this.gbAllowed.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

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
                    _cmdExit.Click -= cmdCancel_Click;
                }

                _cmdExit = value;
                if (_cmdExit != null)
                {
                    _cmdExit.Click += cmdCancel_Click;
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

        private PSB_FarsiDateControl.PSB_DateControl _txtRegisterDate;

        internal PSB_FarsiDateControl.PSB_DateControl txtRegisterDate
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtRegisterDate;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtRegisterDate != null)
                {
                    _txtRegisterDate.KeyPress -= txtPersonnelCode_KeyPress;
                }

                _txtRegisterDate = value;
                if (_txtRegisterDate != null)
                {
                    _txtRegisterDate.KeyPress += txtPersonnelCode_KeyPress;
                }
            }
        }

        internal Label Label1;
        internal Label Label4;
        private TextBox _txtOrderNo;

        internal TextBox txtOrderNo
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtOrderNo;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtOrderNo != null)
                {
                    _txtOrderNo.KeyPress -= txtPersonnelCode_KeyPress;
                }

                _txtOrderNo = value;
                if (_txtOrderNo != null)
                {
                    _txtOrderNo.KeyPress += txtPersonnelCode_KeyPress;
                }
            }
        }

        //internal ComboBox cbCustomer
        //{
        //    [MethodImpl(MethodImplOptions.Synchronized)]
        //    get
        //    {
        //        return _cbCustomer;
        //    }

        //    [MethodImpl(MethodImplOptions.Synchronized)]
        //    set
        //    {
        //        if (_cbCustomer != null)
        //        {
        //            _cbCustomer.KeyPress -= txtPersonnelCode_KeyPress;
        //        }

        //        _cbCustomer = value;
        //        if (_cbCustomer != null)
        //        {
        //            _cbCustomer.KeyPress += txtPersonnelCode_KeyPress;
        //        }
        //    }
        //}

        internal Label Label2;

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
        //            _cbProduct.KeyPress -= txtPersonnelCode_KeyPress;
        //        }

        //        _cbProduct = value;
        //        if (_cbProduct != null)
        //        {
        //            _cbProduct.KeyPress += txtPersonnelCode_KeyPress;
        //        }
        //    }
        //}

        internal Label Label3;
        internal Label Label8;
        private PSB_FarsiDateControl.PSB_DateControl _txtOrderDate;

        internal PSB_FarsiDateControl.PSB_DateControl txtOrderDate
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtOrderDate;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtOrderDate != null)
                {
                    _txtOrderDate.KeyPress -= txtPersonnelCode_KeyPress;
                }

                _txtOrderDate = value;
                if (_txtOrderDate != null)
                {
                    _txtOrderDate.KeyPress += txtPersonnelCode_KeyPress;
                }
            }
        }

        internal Label Label9;
        private TextBox _txtProductTechnicalSpecification;

        internal TextBox txtProductTechnicalSpecification
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtProductTechnicalSpecification;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtProductTechnicalSpecification != null)
                {
                    _txtProductTechnicalSpecification.KeyPress -= txtPersonnelCode_KeyPress;
                }

                _txtProductTechnicalSpecification = value;
                if (_txtProductTechnicalSpecification != null)
                {
                    _txtProductTechnicalSpecification.KeyPress += txtPersonnelCode_KeyPress;
                }
            }
        }

        private NumericUpDown _txtQuantity;

        internal NumericUpDown txtQuantity
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtQuantity;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtQuantity != null)
                {
                    _txtQuantity.KeyPress -= txtPersonnelCode_KeyPress;
                }

                _txtQuantity = value;
                if (_txtQuantity != null)
                {
                    _txtQuantity.KeyPress += txtPersonnelCode_KeyPress;
                }
            }
        }

        internal Label Label5;
        internal GroupBox GroupBox1;
        internal GroupBox GroupBox3;
        private PSB_FarsiDateControl.PSB_DateControl _txtDeliveryDate;

        internal PSB_FarsiDateControl.PSB_DateControl txtDeliveryDate
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtDeliveryDate;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtDeliveryDate != null)
                {
                    _txtDeliveryDate.KeyPress -= txtPersonnelCode_KeyPress;
                }

                _txtDeliveryDate = value;
                if (_txtDeliveryDate != null)
                {
                    _txtDeliveryDate.KeyPress += txtPersonnelCode_KeyPress;
                }
            }
        }

        internal Label Label6;
        internal Label Label7;
        private TextBox _txtContractNo;

        internal TextBox txtContractNo
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtContractNo;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtContractNo != null)
                {
                    _txtContractNo.KeyPress -= txtPersonnelCode_KeyPress;
                }

                _txtContractNo = value;
                if (_txtContractNo != null)
                {
                    _txtContractNo.KeyPress += txtPersonnelCode_KeyPress;
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
                    _chkConfirmed.KeyPress -= txtPersonnelCode_KeyPress;
                }

                _chkConfirmed = value;
                if (_chkConfirmed != null)
                {
                    _chkConfirmed.CheckedChanged += chkConfirmed_CheckedChanged;
                    _chkConfirmed.KeyPress += txtPersonnelCode_KeyPress;
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
                    _chkAllowed.KeyPress -= txtPersonnelCode_KeyPress;
                }

                _chkAllowed = value;
                if (_chkAllowed != null)
                {
                    _chkAllowed.CheckedChanged += chkAllowed_CheckedChanged;
                    _chkAllowed.KeyPress += txtPersonnelCode_KeyPress;
                }
            }
        }

        internal GroupBox gbAllowed;
        private PSB_FarsiDateControl.PSB_DateControl _txtProductionCallDate;

        internal PSB_FarsiDateControl.PSB_DateControl txtProductionCallDate
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtProductionCallDate;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtProductionCallDate != null)
                {
                    _txtProductionCallDate.KeyPress -= txtPersonnelCode_KeyPress;
                }

                _txtProductionCallDate = value;
                if (_txtProductionCallDate != null)
                {
                    _txtProductionCallDate.KeyPress += txtPersonnelCode_KeyPress;
                }
            }
        }

        internal Label Label10;
        private Controls.UserControl_LookUp Product_Lookup;
        private Controls.UserControl_LookUp Customer_LookUp;
    }
}