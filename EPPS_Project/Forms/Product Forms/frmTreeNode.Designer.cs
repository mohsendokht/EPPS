using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmTreeNode : Form
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
            Panel2 = new System.Windows.Forms.Panel();
            _ParentdetailLabel = new Label();
            _ParentdetailLabel.Click += new EventHandler(ParentdetailLabel_Click);
            _ParentdetailLabel.DoubleClick += new EventHandler(ParentdetailLabel_DoubleClick);
            ParentCodeBox = new TextBox();
            rbBT_Buy = new RadioButton();
            rbBT_Built = new RadioButton();
            Label6 = new Label();
            txtParentQuantity = new NumericUpDown();
            lblName = new Label();
            txtCode = new TextBox();
            Label1 = new Label();
            Label3 = new Label();
            txtDescription = new TextBox();
            cbStoreTestUnit = new ComboBox();
            Label8 = new Label();
            cbStorePlace = new ComboBox();
            Label7 = new Label();
            cbProductionTestUnit = new ComboBox();
            Label2 = new Label();
            txtPhysical3 = new TextBox();
            Label15 = new Label();
            txtPhysical2 = new TextBox();
            Label14 = new Label();
            txtPhysical1 = new TextBox();
            Label13 = new Label();
            txtVolume = new NumericUpDown();
            Label12 = new Label();
            txtTemperature = new NumericUpDown();
            Label11 = new Label();
            txtHeight = new NumericUpDown();
            Label10 = new Label();
            txtWeight = new NumericUpDown();
            Label9 = new Label();
            txtMapsSpecifications = new TextBox();
            Label5 = new Label();
            Panel1 = new System.Windows.Forms.Panel();
            _cmdSave = new System.Windows.Forms.Button();
            _cmdSave.Click += new EventHandler(cmdSave_Click);
            _cmdExit = new System.Windows.Forms.Button();
            _cmdExit.Click += new EventHandler(cmdExit_Click);
            _cmdDelete = new System.Windows.Forms.Button();
            _cmdDelete.Click += new EventHandler(cmdDelete_Click);
            txtName = new TextBox();
            Label4 = new Label();
            Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtParentQuantity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtVolume).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtTemperature).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtWeight).BeginInit();
            Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // Panel2
            // 
            Panel2.Controls.Add(_ParentdetailLabel);
            Panel2.Controls.Add(ParentCodeBox);
            Panel2.Controls.Add(rbBT_Buy);
            Panel2.Controls.Add(rbBT_Built);
            Panel2.Controls.Add(Label6);
            Panel2.Controls.Add(txtParentQuantity);
            Panel2.Controls.Add(lblName);
            Panel2.Controls.Add(txtCode);
            Panel2.Controls.Add(Label1);
            Panel2.Location = new Point(16, 12);
            Panel2.Name = "Panel2";
            Panel2.Size = new Size(557, 76);
            Panel2.TabIndex = 0;
            // 
            // ParentdetailLabel
            // 
            _ParentdetailLabel.BackColor = SystemColors.Control;
            _ParentdetailLabel.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _ParentdetailLabel.ForeColor = Color.Black;
            _ParentdetailLabel.Location = new Point(161, 10);
            _ParentdetailLabel.Name = "_ParentdetailLabel";
            _ParentdetailLabel.RightToLeft = RightToLeft.Yes;
            _ParentdetailLabel.Size = new Size(57, 17);
            _ParentdetailLabel.TabIndex = 233;
            _ParentdetailLabel.Text = "کد جزء پدر ";
            _ParentdetailLabel.TextAlign = ContentAlignment.MiddleLeft;
            _ParentdetailLabel.Visible = false;
            // 
            // ParentCodeBox
            // 
            ParentCodeBox.Enabled = false;
            ParentCodeBox.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            ParentCodeBox.Location = new Point(28, 9);
            ParentCodeBox.Name = "ParentCodeBox";
            ParentCodeBox.Size = new Size(127, 21);
            ParentCodeBox.TabIndex = 232;
            ParentCodeBox.Visible = false;
            // 
            // rbBT_Buy
            // 
            rbBT_Buy.AutoSize = true;
            rbBT_Buy.BackColor = Color.Transparent;
            rbBT_Buy.FlatStyle = FlatStyle.Flat;
            rbBT_Buy.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            rbBT_Buy.Location = new Point(19, 46);
            rbBT_Buy.Name = "rbBT_Buy";
            rbBT_Buy.Size = new Size(59, 17);
            rbBT_Buy.TabIndex = 4;
            rbBT_Buy.Text = "خریدنی";
            rbBT_Buy.UseVisualStyleBackColor = false;
            // 
            // rbBT_Built
            // 
            rbBT_Built.AutoSize = true;
            rbBT_Built.BackColor = Color.Transparent;
            rbBT_Built.Checked = true;
            rbBT_Built.FlatStyle = FlatStyle.Flat;
            rbBT_Built.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            rbBT_Built.Location = new Point(91, 45);
            rbBT_Built.Name = "rbBT_Built";
            rbBT_Built.Size = new Size(64, 17);
            rbBT_Built.TabIndex = 3;
            rbBT_Built.TabStop = true;
            rbBT_Built.Text = "ساختنی";
            rbBT_Built.UseVisualStyleBackColor = false;
            // 
            // Label6
            // 
            Label6.BackColor = SystemColors.Control;
            Label6.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label6.ForeColor = Color.Black;
            Label6.Location = new Point(153, 46);
            Label6.Name = "Label6";
            Label6.RightToLeft = RightToLeft.Yes;
            Label6.Size = new Size(61, 17);
            Label6.TabIndex = 231;
            Label6.Text = "نوع تهیه:";
            // 
            // txtParentQuantity
            // 
            txtParentQuantity.BorderStyle = BorderStyle.None;
            txtParentQuantity.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtParentQuantity.Location = new Point(255, 46);
            txtParentQuantity.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            txtParentQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            txtParentQuantity.Name = "txtParentQuantity";
            txtParentQuantity.Size = new Size(69, 17);
            txtParentQuantity.TabIndex = 2;
            txtParentQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblName
            // 
            lblName.BackColor = SystemColors.Control;
            lblName.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblName.ForeColor = Color.Black;
            lblName.Location = new Point(327, 45);
            lblName.Name = "lblName";
            lblName.RightToLeft = RightToLeft.Yes;
            lblName.Size = new Size(222, 17);
            lblName.TabIndex = 230;
            lblName.Text = "تعداد/مقدار مورد نیاز در محصول نیمه ساخته پدر" + '\r' + '\n';
            // 
            // txtCode
            // 
            txtCode.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtCode.Location = new Point(255, 9);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(221, 21);
            txtCode.TabIndex = 0;
            // 
            // Label1
            // 
            Label1.BackColor = SystemColors.Control;
            Label1.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label1.ForeColor = Color.Black;
            Label1.Location = new Point(511, 9);
            Label1.Name = "Label1";
            Label1.RightToLeft = RightToLeft.Yes;
            Label1.Size = new Size(38, 17);
            Label1.TabIndex = 228;
            Label1.Text = "کد جزء";
            Label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label3
            // 
            Label3.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label3.Location = new Point(233, 251);
            Label3.Name = "Label3";
            Label3.RightToLeft = RightToLeft.Yes;
            Label3.Size = new Size(48, 17);
            Label3.TabIndex = 297;
            Label3.Text = "توضیحات";
            // 
            // txtDescription
            // 
            txtDescription.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtDescription.Location = new Point(25, 249);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(202, 95);
            txtDescription.TabIndex = 17;
            // 
            // cbStoreTestUnit
            // 
            cbStoreTestUnit.DropDownStyle = ComboBoxStyle.DropDownList;
            cbStoreTestUnit.FlatStyle = FlatStyle.Flat;
            cbStoreTestUnit.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cbStoreTestUnit.FormattingEnabled = true;
            cbStoreTestUnit.Location = new Point(288, 323);
            cbStoreTestUnit.Name = "cbStoreTestUnit";
            cbStoreTestUnit.Size = new Size(181, 21);
            cbStoreTestUnit.TabIndex = 16;
            // 
            // Label8
            // 
            Label8.BackColor = SystemColors.Control;
            Label8.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label8.ForeColor = Color.Black;
            Label8.Location = new Point(471, 325);
            Label8.Name = "Label8";
            Label8.RightToLeft = RightToLeft.Yes;
            Label8.Size = new Size(93, 17);
            Label8.TabIndex = 296;
            Label8.Text = "واحد سنجش انبار";
            // 
            // cbStorePlace
            // 
            cbStorePlace.DropDownStyle = ComboBoxStyle.DropDownList;
            cbStorePlace.FlatStyle = FlatStyle.Flat;
            cbStorePlace.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cbStorePlace.FormattingEnabled = true;
            cbStorePlace.Location = new Point(288, 286);
            cbStorePlace.Name = "cbStorePlace";
            cbStorePlace.Size = new Size(181, 21);
            cbStorePlace.TabIndex = 15;
            // 
            // Label7
            // 
            Label7.BackColor = SystemColors.Control;
            Label7.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label7.ForeColor = Color.Black;
            Label7.Location = new Point(470, 288);
            Label7.Name = "Label7";
            Label7.RightToLeft = RightToLeft.Yes;
            Label7.Size = new Size(95, 17);
            Label7.TabIndex = 295;
            Label7.Text = "محل نگهداری(انبار)";
            // 
            // cbProductionTestUnit
            // 
            cbProductionTestUnit.DropDownStyle = ComboBoxStyle.DropDownList;
            cbProductionTestUnit.FlatStyle = FlatStyle.Flat;
            cbProductionTestUnit.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cbProductionTestUnit.FormattingEnabled = true;
            cbProductionTestUnit.Location = new Point(288, 249);
            cbProductionTestUnit.Name = "cbProductionTestUnit";
            cbProductionTestUnit.Size = new Size(181, 21);
            cbProductionTestUnit.TabIndex = 14;
            // 
            // Label2
            // 
            Label2.BackColor = SystemColors.Control;
            Label2.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label2.ForeColor = Color.Black;
            Label2.Location = new Point(470, 251);
            Label2.Name = "Label2";
            Label2.RightToLeft = RightToLeft.Yes;
            Label2.Size = new Size(95, 17);
            Label2.TabIndex = 294;
            Label2.Text = "واحد سنجش تولید";
            // 
            // txtPhysical3
            // 
            txtPhysical3.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtPhysical3.Location = new Point(25, 210);
            txtPhysical3.Name = "txtPhysical3";
            txtPhysical3.Size = new Size(76, 21);
            txtPhysical3.TabIndex = 13;
            // 
            // Label15
            // 
            Label15.BackColor = SystemColors.Control;
            Label15.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label15.ForeColor = Color.Black;
            Label15.Location = new Point(107, 210);
            Label15.Name = "Label15";
            Label15.RightToLeft = RightToLeft.Yes;
            Label15.Size = new Size(91, 17);
            Label15.TabIndex = 293;
            Label15.Text = "مشخصه فیزیکی3";
            // 
            // txtPhysical2
            // 
            txtPhysical2.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtPhysical2.Location = new Point(208, 210);
            txtPhysical2.Name = "txtPhysical2";
            txtPhysical2.Size = new Size(76, 21);
            txtPhysical2.TabIndex = 12;
            // 
            // Label14
            // 
            Label14.BackColor = SystemColors.Control;
            Label14.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label14.ForeColor = Color.Black;
            Label14.Location = new Point(290, 210);
            Label14.Name = "Label14";
            Label14.RightToLeft = RightToLeft.Yes;
            Label14.Size = new Size(91, 17);
            Label14.TabIndex = 292;
            Label14.Text = "مشخصه فیزیکی2";
            // 
            // txtPhysical1
            // 
            txtPhysical1.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtPhysical1.Location = new Point(392, 210);
            txtPhysical1.Name = "txtPhysical1";
            txtPhysical1.Size = new Size(76, 21);
            txtPhysical1.TabIndex = 11;
            // 
            // Label13
            // 
            Label13.BackColor = SystemColors.Control;
            Label13.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label13.ForeColor = Color.Black;
            Label13.Location = new Point(474, 210);
            Label13.Name = "Label13";
            Label13.RightToLeft = RightToLeft.Yes;
            Label13.Size = new Size(91, 17);
            Label13.TabIndex = 291;
            Label13.Text = "مشخصه فیزیکی1";
            // 
            // txtVolume
            // 
            txtVolume.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtVolume.Location = new Point(25, 167);
            txtVolume.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            txtVolume.Name = "txtVolume";
            txtVolume.Size = new Size(51, 21);
            txtVolume.TabIndex = 10;
            // 
            // Label12
            // 
            Label12.BackColor = SystemColors.Control;
            Label12.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label12.ForeColor = Color.Black;
            Label12.Location = new Point(82, 171);
            Label12.Name = "Label12";
            Label12.RightToLeft = RightToLeft.Yes;
            Label12.Size = new Size(73, 17);
            Label12.TabIndex = 290;
            Label12.Text = "مشخصه حجم";
            // 
            // txtTemperature
            // 
            txtTemperature.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtTemperature.Location = new Point(164, 171);
            txtTemperature.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            txtTemperature.Name = "txtTemperature";
            txtTemperature.Size = new Size(51, 21);
            txtTemperature.TabIndex = 9;
            // 
            // Label11
            // 
            Label11.BackColor = SystemColors.Control;
            Label11.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label11.ForeColor = Color.Black;
            Label11.Location = new Point(221, 171);
            Label11.Name = "Label11";
            Label11.RightToLeft = RightToLeft.Yes;
            Label11.Size = new Size(65, 17);
            Label11.TabIndex = 289;
            Label11.Text = "مشخصه دما";
            // 
            // txtHeight
            // 
            txtHeight.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtHeight.Location = new Point(294, 171);
            txtHeight.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            txtHeight.Name = "txtHeight";
            txtHeight.Size = new Size(51, 21);
            txtHeight.TabIndex = 8;
            // 
            // Label10
            // 
            Label10.BackColor = SystemColors.Control;
            Label10.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label10.ForeColor = Color.Black;
            Label10.Location = new Point(352, 171);
            Label10.Name = "Label10";
            Label10.RightToLeft = RightToLeft.Yes;
            Label10.Size = new Size(77, 17);
            Label10.TabIndex = 288;
            Label10.Text = "مشخصه ارتفاع";
            // 
            // txtWeight
            // 
            txtWeight.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtWeight.Location = new Point(441, 171);
            txtWeight.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            txtWeight.Name = "txtWeight";
            txtWeight.Size = new Size(51, 21);
            txtWeight.TabIndex = 7;
            // 
            // Label9
            // 
            Label9.BackColor = SystemColors.Control;
            Label9.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label9.ForeColor = Color.Black;
            Label9.Location = new Point(498, 171);
            Label9.Name = "Label9";
            Label9.RightToLeft = RightToLeft.Yes;
            Label9.Size = new Size(67, 17);
            Label9.TabIndex = 287;
            Label9.Text = "مشخصه وزن";
            // 
            // txtMapsSpecifications
            // 
            txtMapsSpecifications.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtMapsSpecifications.Location = new Point(25, 130);
            txtMapsSpecifications.Name = "txtMapsSpecifications";
            txtMapsSpecifications.Size = new Size(369, 21);
            txtMapsSpecifications.TabIndex = 6;
            // 
            // Label5
            // 
            Label5.BackColor = SystemColors.Control;
            Label5.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label5.ForeColor = Color.Black;
            Label5.Location = new Point(399, 130);
            Label5.Name = "Label5";
            Label5.RightToLeft = RightToLeft.Yes;
            Label5.Size = new Size(166, 17);
            Label5.TabIndex = 286;
            Label5.Text = "مشخصات نقشه های فنی مربوطه";
            // 
            // Panel1
            // 
            Panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Panel1.BackColor = SystemColors.Control;
            Panel1.BorderStyle = BorderStyle.FixedSingle;
            Panel1.Controls.Add(_cmdSave);
            Panel1.Controls.Add(_cmdExit);
            Panel1.Controls.Add(_cmdDelete);
            Panel1.Location = new Point(9, 370);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(568, 48);
            Panel1.TabIndex = 299;
            // 
            // cmdSave
            // 
            _cmdSave.BackColor = Color.Transparent;
            _cmdSave.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdSave.ForeColor = Color.Black;
            _cmdSave.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdSave.Location = new Point(298, 10);
            _cmdSave.Name = "_cmdSave";
            _cmdSave.RightToLeft = RightToLeft.No;
            _cmdSave.Size = new Size(90, 28);
            _cmdSave.TabIndex = 18;
            _cmdSave.Text = "ثبت";
            _cmdSave.TextAlign = ContentAlignment.MiddleRight;
            _cmdSave.UseVisualStyleBackColor = false;
            // 
            // cmdExit
            // 
            _cmdExit.BackColor = Color.Transparent;
            _cmdExit.DialogResult = DialogResult.Cancel;
            _cmdExit.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdExit.ForeColor = Color.Red;
            _cmdExit.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdExit.Location = new Point(179, 10);
            _cmdExit.Name = "_cmdExit";
            _cmdExit.RightToLeft = RightToLeft.No;
            _cmdExit.Size = new Size(90, 28);
            _cmdExit.TabIndex = 19;
            _cmdExit.Text = "خروج";
            _cmdExit.TextAlign = ContentAlignment.MiddleRight;
            _cmdExit.UseVisualStyleBackColor = false;
            // 
            // cmdDelete
            // 
            _cmdDelete.BackColor = Color.Transparent;
            _cmdDelete.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdDelete.ForeColor = Color.Black;
            _cmdDelete.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdDelete.Location = new Point(298, 10);
            _cmdDelete.Name = "_cmdDelete";
            _cmdDelete.RightToLeft = RightToLeft.No;
            _cmdDelete.Size = new Size(90, 28);
            _cmdDelete.TabIndex = 19;
            _cmdDelete.Text = "حذف";
            _cmdDelete.TextAlign = ContentAlignment.MiddleRight;
            _cmdDelete.UseVisualStyleBackColor = false;
            _cmdDelete.Visible = false;
            // 
            // txtName
            // 
            txtName.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtName.Location = new Point(25, 99);
            txtName.Name = "txtName";
            txtName.Size = new Size(369, 21);
            txtName.TabIndex = 5;
            // 
            // Label4
            // 
            Label4.BackColor = SystemColors.Control;
            Label4.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label4.ForeColor = Color.Black;
            Label4.Location = new Point(400, 100);
            Label4.Name = "Label4";
            Label4.RightToLeft = RightToLeft.Yes;
            Label4.Size = new Size(165, 17);
            Label4.TabIndex = 303;
            Label4.Text = "نام جزء (قطعه اصلی یا فرعی)";
            Label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // frmTreeNode
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(586, 425);
            Controls.Add(txtName);
            Controls.Add(Label4);
            Controls.Add(Panel1);
            Controls.Add(Panel2);
            Controls.Add(Label3);
            Controls.Add(txtDescription);
            Controls.Add(cbStoreTestUnit);
            Controls.Add(Label8);
            Controls.Add(cbStorePlace);
            Controls.Add(Label7);
            Controls.Add(cbProductionTestUnit);
            Controls.Add(Label2);
            Controls.Add(txtPhysical3);
            Controls.Add(Label15);
            Controls.Add(txtPhysical2);
            Controls.Add(Label14);
            Controls.Add(txtPhysical1);
            Controls.Add(Label13);
            Controls.Add(txtVolume);
            Controls.Add(Label12);
            Controls.Add(txtTemperature);
            Controls.Add(Label11);
            Controls.Add(txtHeight);
            Controls.Add(Label10);
            Controls.Add(txtWeight);
            Controls.Add(Label9);
            Controls.Add(txtMapsSpecifications);
            Controls.Add(Label5);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmTreeNode";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " مشخصات جزء درخت محصول";
            Panel2.ResumeLayout(false);
            Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtParentQuantity).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtVolume).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtTemperature).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtWeight).EndInit();
            Panel1.ResumeLayout(false);
            Load += new EventHandler(frmTreeNode_Load);
            FormClosing += new FormClosingEventHandler(frmTreeNode_FormClosing);
            ResumeLayout(false);
            PerformLayout();
        }

        internal System.Windows.Forms.Panel Panel2;
        internal RadioButton rbBT_Buy;
        internal RadioButton rbBT_Built;
        internal Label Label6;
        internal NumericUpDown txtParentQuantity;
        internal Label lblName;
        internal TextBox txtCode;
        internal Label Label1;
        internal Label Label3;
        internal TextBox txtDescription;
        internal ComboBox cbStoreTestUnit;
        internal Label Label8;
        internal ComboBox cbStorePlace;
        internal Label Label7;
        internal ComboBox cbProductionTestUnit;
        internal Label Label2;
        internal TextBox txtPhysical3;
        internal Label Label15;
        internal TextBox txtPhysical2;
        internal Label Label14;
        internal TextBox txtPhysical1;
        internal Label Label13;
        internal NumericUpDown txtVolume;
        internal Label Label12;
        internal NumericUpDown txtTemperature;
        internal Label Label11;
        internal NumericUpDown txtHeight;
        internal Label Label10;
        internal NumericUpDown txtWeight;
        internal Label Label9;
        internal TextBox txtMapsSpecifications;
        internal Label Label5;
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

        private Label _ParentdetailLabel;

        internal Label ParentdetailLabel
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _ParentdetailLabel;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_ParentdetailLabel != null)
                {
                    _ParentdetailLabel.Click -= ParentdetailLabel_Click;
                    _ParentdetailLabel.DoubleClick -= ParentdetailLabel_DoubleClick;
                }

                _ParentdetailLabel = value;
                if (_ParentdetailLabel != null)
                {
                    _ParentdetailLabel.Click += ParentdetailLabel_Click;
                    _ParentdetailLabel.DoubleClick += ParentdetailLabel_DoubleClick;
                }
            }
        }

        internal TextBox ParentCodeBox;
        internal TextBox txtName;
        internal Label Label4;
    }
}