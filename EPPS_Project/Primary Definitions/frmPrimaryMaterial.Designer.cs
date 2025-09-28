using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmPrimaryMaterial : Form
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
            cbProductionTestUnit = new ComboBox();
            _cmdSave = new System.Windows.Forms.Button();
            _cmdSave.Click += new EventHandler(cmdSave_Click);
            cbStoreTestUnit = new ComboBox();
            Label2 = new Label();
            Label1 = new Label();
            Panel1 = new System.Windows.Forms.Panel();
            _cmdExit = new System.Windows.Forms.Button();
            _cmdExit.Click += new EventHandler(cmdCancel_Click);
            _cmdDelete = new System.Windows.Forms.Button();
            _cmdDelete.Click += new EventHandler(cmdDelete_Click);
            lblName = new Label();
            txtName = new TextBox();
            Label3 = new Label();
            txtSpecification = new TextBox();
            Label4 = new Label();
            Label5 = new Label();
            cbStorePlace = new ComboBox();
            Label6 = new Label();
            Label7 = new Label();
            Label8 = new Label();
            txtFillMeasureAppointMethod = new TextBox();
            Label9 = new Label();
            Label10 = new Label();
            Label11 = new Label();
            Label12 = new Label();
            Label13 = new Label();
            txtLastHolding = new NumericUpDown();
            txtFirstHolding = new NumericUpDown();
            txtMinimumCountPerOrder = new NumericUpDown();
            txtOrderPoint = new TextBox();
            txtBuyTimeBatch = new NumericUpDown();
            txtBatchQuantity = new NumericUpDown();
            Panel7 = new System.Windows.Forms.Panel();
            _RadioButton1 = new RadioButton();
            _RadioButton1.CheckedChanged += new EventHandler(RadioButton1_CheckedChanged);
            _rbMinute = new RadioButton();
            _rbMinute.CheckedChanged += new EventHandler(rbMinute_CheckedChanged);
            _rbDay = new RadioButton();
            _rbDay.CheckedChanged += new EventHandler(rbDay_CheckedChanged);
            _rbHour = new RadioButton();
            _rbHour.CheckedChanged += new EventHandler(rbHour_CheckedChanged);
            _rbMonth = new RadioButton();
            _rbMonth.CheckedChanged += new EventHandler(rbMonth_CheckedChanged);
            _rbWeek = new RadioButton();
            _rbWeek.CheckedChanged += new EventHandler(rbWeek_CheckedChanged);
            txtCode = new TextBox();
            Label14 = new Label();
            txtGarbagePercent = new NumericUpDown();
            Label18 = new Label();
            txtApplication = new TextBox();
            Label15 = new Label();
            Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtLastHolding).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtFirstHolding).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtMinimumCountPerOrder).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtBuyTimeBatch).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtBatchQuantity).BeginInit();
            Panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtGarbagePercent).BeginInit();
            SuspendLayout();
            // 
            // cbProductionTestUnit
            // 
            cbProductionTestUnit.DropDownStyle = ComboBoxStyle.DropDownList;
            cbProductionTestUnit.FlatStyle = FlatStyle.Flat;
            cbProductionTestUnit.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cbProductionTestUnit.FormattingEnabled = true;
            cbProductionTestUnit.ItemHeight = 13;
            cbProductionTestUnit.Location = new Point(16, 53);
            cbProductionTestUnit.Name = "cbProductionTestUnit";
            cbProductionTestUnit.Size = new Size(252, 21);
            cbProductionTestUnit.TabIndex = 4;
            cbProductionTestUnit.Visible = false;
            // 
            // cmdSave
            // 
            _cmdSave.BackColor = Color.Transparent;
            _cmdSave.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdSave.ForeColor = Color.Black;
            _cmdSave.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdSave.Location = new Point(387, 10);
            _cmdSave.Name = "_cmdSave";
            _cmdSave.RightToLeft = RightToLeft.No;
            _cmdSave.Size = new Size(90, 28);
            _cmdSave.TabIndex = 45;
            _cmdSave.Text = "ثبت";
            _cmdSave.TextAlign = ContentAlignment.MiddleRight;
            _cmdSave.UseVisualStyleBackColor = false;
            // 
            // cbStoreTestUnit
            // 
            cbStoreTestUnit.DropDownStyle = ComboBoxStyle.DropDownList;
            cbStoreTestUnit.FlatStyle = FlatStyle.Flat;
            cbStoreTestUnit.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cbStoreTestUnit.FormattingEnabled = true;
            cbStoreTestUnit.Location = new Point(16, 85);
            cbStoreTestUnit.Name = "cbStoreTestUnit";
            cbStoreTestUnit.Size = new Size(252, 21);
            cbStoreTestUnit.TabIndex = 3;
            // 
            // Label2
            // 
            Label2.BackColor = SystemColors.Control;
            Label2.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label2.ForeColor = Color.Black;
            Label2.Location = new Point(270, 87);
            Label2.Name = "Label2";
            Label2.RightToLeft = RightToLeft.Yes;
            Label2.Size = new Size(97, 17);
            Label2.TabIndex = 19;
            Label2.Text = "واحد سنجش انبار";
            // 
            // Label1
            // 
            Label1.BackColor = SystemColors.Control;
            Label1.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label1.ForeColor = Color.Black;
            Label1.Location = new Point(270, 55);
            Label1.Name = "Label1";
            Label1.RightToLeft = RightToLeft.Yes;
            Label1.Size = new Size(101, 17);
            Label1.TabIndex = 18;
            Label1.Text = "واحد سنجش تولید";
            Label1.Visible = false;
            // 
            // Panel1
            // 
            Panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Panel1.BackColor = SystemColors.Control;
            Panel1.BorderStyle = BorderStyle.FixedSingle;
            Panel1.Controls.Add(_cmdSave);
            Panel1.Controls.Add(_cmdExit);
            Panel1.Controls.Add(_cmdDelete);
            Panel1.Location = new Point(9, 255);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(738, 48);
            Panel1.TabIndex = 15;
            // 
            // cmdExit
            // 
            _cmdExit.BackColor = Color.Transparent;
            _cmdExit.DialogResult = DialogResult.Cancel;
            _cmdExit.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdExit.ForeColor = Color.Red;
            _cmdExit.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdExit.Location = new Point(260, 10);
            _cmdExit.Name = "_cmdExit";
            _cmdExit.RightToLeft = RightToLeft.No;
            _cmdExit.Size = new Size(90, 28);
            _cmdExit.TabIndex = 46;
            _cmdExit.Text = "انصراف";
            _cmdExit.TextAlign = ContentAlignment.MiddleRight;
            _cmdExit.UseVisualStyleBackColor = false;
            // 
            // cmdDelete
            // 
            _cmdDelete.BackColor = Color.Transparent;
            _cmdDelete.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdDelete.ForeColor = Color.Black;
            _cmdDelete.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdDelete.Location = new Point(387, 10);
            _cmdDelete.Name = "_cmdDelete";
            _cmdDelete.RightToLeft = RightToLeft.No;
            _cmdDelete.Size = new Size(90, 28);
            _cmdDelete.TabIndex = 47;
            _cmdDelete.Text = "حذف";
            _cmdDelete.TextAlign = ContentAlignment.MiddleRight;
            _cmdDelete.UseVisualStyleBackColor = false;
            _cmdDelete.Visible = false;
            // 
            // lblName
            // 
            lblName.BackColor = SystemColors.Control;
            lblName.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblName.ForeColor = Color.Black;
            lblName.Location = new Point(583, 316);
            lblName.Name = "lblName";
            lblName.RightToLeft = RightToLeft.Yes;
            lblName.Size = new Size(160, 17);
            lblName.TabIndex = 13;
            lblName.Text = "حداقل میزان اقتصادی سفارش";
            lblName.Visible = false;
            // 
            // txtName
            // 
            txtName.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtName.Location = new Point(16, 20);
            txtName.Name = "txtName";
            txtName.Size = new Size(252, 21);
            txtName.TabIndex = 1;
            // 
            // Label3
            // 
            Label3.BackColor = SystemColors.Control;
            Label3.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label3.ForeColor = Color.Black;
            Label3.Location = new Point(270, 22);
            Label3.Name = "Label3";
            Label3.RightToLeft = RightToLeft.Yes;
            Label3.Size = new Size(72, 17);
            Label3.TabIndex = 20;
            Label3.Text = "نام مواد اولیه";
            // 
            // txtSpecification
            // 
            txtSpecification.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtSpecification.Location = new Point(394, 53);
            txtSpecification.Name = "txtSpecification";
            txtSpecification.Size = new Size(248, 21);
            txtSpecification.TabIndex = 2;
            // 
            // Label4
            // 
            Label4.BackColor = SystemColors.Control;
            Label4.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label4.ForeColor = Color.Black;
            Label4.Location = new Point(644, 55);
            Label4.Name = "Label4";
            Label4.RightToLeft = RightToLeft.Yes;
            Label4.Size = new Size(83, 17);
            Label4.TabIndex = 22;
            Label4.Text = "مشخصات فنی";
            // 
            // Label5
            // 
            Label5.BackColor = SystemColors.Control;
            Label5.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label5.ForeColor = Color.Black;
            Label5.Location = new Point(644, 119);
            Label5.Name = "Label5";
            Label5.RightToLeft = RightToLeft.Yes;
            Label5.Size = new Size(78, 17);
            Label5.TabIndex = 25;
            Label5.Text = "نقطه سفارش";
            // 
            // cbStorePlace
            // 
            cbStorePlace.DropDownStyle = ComboBoxStyle.DropDownList;
            cbStorePlace.FlatStyle = FlatStyle.Flat;
            cbStorePlace.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cbStorePlace.FormattingEnabled = true;
            cbStorePlace.Location = new Point(394, 85);
            cbStorePlace.Name = "cbStorePlace";
            cbStorePlace.Size = new Size(248, 21);
            cbStorePlace.TabIndex = 6;
            // 
            // Label6
            // 
            Label6.BackColor = SystemColors.Control;
            Label6.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label6.ForeColor = Color.Black;
            Label6.Location = new Point(644, 87);
            Label6.Name = "Label6";
            Label6.RightToLeft = RightToLeft.Yes;
            Label6.Size = new Size(103, 17);
            Label6.TabIndex = 27;
            Label6.Text = "محل نگهداری(انبار)";
            // 
            // Label7
            // 
            Label7.BackColor = SystemColors.Control;
            Label7.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label7.ForeColor = Color.Black;
            Label7.Location = new Point(644, 153);
            Label7.Name = "Label7";
            Label7.RightToLeft = RightToLeft.Yes;
            Label7.Size = new Size(80, 17);
            Label7.TabIndex = 28;
            Label7.Text = "آخرین موجودی";
            // 
            // Label8
            // 
            Label8.BackColor = SystemColors.Control;
            Label8.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label8.ForeColor = Color.Black;
            Label8.Location = new Point(269, 155);
            Label8.Name = "Label8";
            Label8.RightToLeft = RightToLeft.Yes;
            Label8.Size = new Size(93, 17);
            Label8.TabIndex = 30;
            Label8.Text = "موجودی اول دوره";
            // 
            // txtFillMeasureAppointMethod
            // 
            txtFillMeasureAppointMethod.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtFillMeasureAppointMethod.Location = new Point(394, 288);
            txtFillMeasureAppointMethod.Name = "txtFillMeasureAppointMethod";
            txtFillMeasureAppointMethod.Size = new Size(214, 21);
            txtFillMeasureAppointMethod.TabIndex = 7;
            txtFillMeasureAppointMethod.Visible = false;
            // 
            // Label9
            // 
            Label9.BackColor = SystemColors.Control;
            Label9.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label9.ForeColor = Color.Black;
            Label9.Location = new Point(614, 290);
            Label9.Name = "Label9";
            Label9.RightToLeft = RightToLeft.Yes;
            Label9.Size = new Size(133, 17);
            Label9.TabIndex = 32;
            Label9.Text = "روش تعیین اندازه انباشته ";
            Label9.Visible = false;
            // 
            // Label10
            // 
            Label10.BackColor = SystemColors.Control;
            Label10.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label10.ForeColor = Color.Black;
            Label10.Location = new Point(464, 317);
            Label10.Name = "Label10";
            Label10.RightToLeft = RightToLeft.Yes;
            Label10.Size = new Size(78, 17);
            Label10.TabIndex = 34;
            Label10.Text = "درصد ضایعات";
            Label10.Visible = false;
            // 
            // Label11
            // 
            Label11.BackColor = SystemColors.Control;
            Label11.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label11.ForeColor = Color.Black;
            Label11.Location = new Point(49, 315);
            Label11.Name = "Label11";
            Label11.RightToLeft = RightToLeft.Yes;
            Label11.Size = new Size(156, 17);
            Label11.TabIndex = 36;
            Label11.Text = "زمان لازم برای خرید یک دسته";
            Label11.Visible = false;
            // 
            // Label12
            // 
            Label12.BackColor = SystemColors.Control;
            Label12.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label12.ForeColor = Color.Black;
            Label12.Location = new Point(257, 315);
            Label12.Name = "Label12";
            Label12.RightToLeft = RightToLeft.Yes;
            Label12.Size = new Size(144, 17);
            Label12.TabIndex = 38;
            Label12.Text = "تعداد/مقدار قلم تولید دسته";
            Label12.Visible = false;
            // 
            // Label13
            // 
            Label13.BackColor = SystemColors.Control;
            Label13.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label13.ForeColor = Color.Black;
            Label13.Location = new Point(324, 290);
            Label13.Name = "Label13";
            Label13.RightToLeft = RightToLeft.Yes;
            Label13.Size = new Size(49, 17);
            Label13.TabIndex = 40;
            Label13.Text = "نوع زمان";
            Label13.Visible = false;
            // 
            // txtLastHolding
            // 
            txtLastHolding.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtLastHolding.Location = new Point(543, 151);
            txtLastHolding.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            txtLastHolding.Name = "txtLastHolding";
            txtLastHolding.Size = new Size(99, 21);
            txtLastHolding.TabIndex = 41;
            // 
            // txtFirstHolding
            // 
            txtFirstHolding.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtFirstHolding.Location = new Point(169, 153);
            txtFirstHolding.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            txtFirstHolding.Name = "txtFirstHolding";
            txtFirstHolding.Size = new Size(99, 21);
            txtFirstHolding.TabIndex = 43;
            // 
            // txtMinimumCountPerOrder
            // 
            txtMinimumCountPerOrder.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtMinimumCountPerOrder.Location = new Point(543, 314);
            txtMinimumCountPerOrder.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            txtMinimumCountPerOrder.Name = "txtMinimumCountPerOrder";
            txtMinimumCountPerOrder.Size = new Size(38, 21);
            txtMinimumCountPerOrder.TabIndex = 38;
            txtMinimumCountPerOrder.Visible = false;
            // 
            // txtOrderPoint
            // 
            txtOrderPoint.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtOrderPoint.Location = new Point(394, 117);
            txtOrderPoint.Name = "txtOrderPoint";
            txtOrderPoint.Size = new Size(248, 21);
            txtOrderPoint.TabIndex = 5;
            // 
            // txtBuyTimeBatch
            // 
            txtBuyTimeBatch.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtBuyTimeBatch.Location = new Point(11, 313);
            txtBuyTimeBatch.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            txtBuyTimeBatch.Name = "txtBuyTimeBatch";
            txtBuyTimeBatch.Size = new Size(36, 21);
            txtBuyTimeBatch.TabIndex = 39;
            txtBuyTimeBatch.Visible = false;
            // 
            // txtBatchQuantity
            // 
            txtBatchQuantity.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtBatchQuantity.Location = new Point(212, 313);
            txtBatchQuantity.Name = "txtBatchQuantity";
            txtBatchQuantity.Size = new Size(43, 21);
            txtBatchQuantity.TabIndex = 42;
            txtBatchQuantity.Visible = false;
            // 
            // Panel7
            // 
            Panel7.BorderStyle = BorderStyle.FixedSingle;
            Panel7.Controls.Add(_RadioButton1);
            Panel7.Controls.Add(_rbMinute);
            Panel7.Controls.Add(_rbDay);
            Panel7.Controls.Add(_rbHour);
            Panel7.Controls.Add(_rbMonth);
            Panel7.Controls.Add(_rbWeek);
            Panel7.Location = new Point(11, 287);
            Panel7.Name = "Panel7";
            Panel7.Size = new Size(303, 25);
            Panel7.TabIndex = 163;
            Panel7.Visible = false;
            // 
            // RadioButton1
            // 
            _RadioButton1.AutoSize = true;
            _RadioButton1.Checked = true;
            _RadioButton1.FlatStyle = FlatStyle.Flat;
            _RadioButton1.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _RadioButton1.Location = new Point(255, 3);
            _RadioButton1.Name = "_RadioButton1";
            _RadioButton1.Size = new Size(43, 17);
            _RadioButton1.TabIndex = 8;
            _RadioButton1.TabStop = true;
            _RadioButton1.Text = "ثانیه";
            _RadioButton1.UseVisualStyleBackColor = true;
            // 
            // rbMinute
            // 
            _rbMinute.AutoSize = true;
            _rbMinute.FlatStyle = FlatStyle.Flat;
            _rbMinute.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _rbMinute.Location = new Point(199, 3);
            _rbMinute.Name = "_rbMinute";
            _rbMinute.Size = new Size(50, 17);
            _rbMinute.TabIndex = 9;
            _rbMinute.Text = "دقیقه";
            _rbMinute.UseVisualStyleBackColor = true;
            // 
            // rbDay
            // 
            _rbDay.AutoSize = true;
            _rbDay.FlatStyle = FlatStyle.Flat;
            _rbDay.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _rbDay.Location = new Point(97, 3);
            _rbDay.Name = "_rbDay";
            _rbDay.Size = new Size(37, 17);
            _rbDay.TabIndex = 11;
            _rbDay.Text = "روز";
            _rbDay.UseVisualStyleBackColor = true;
            // 
            // rbHour
            // 
            _rbHour.AutoSize = true;
            _rbHour.FlatStyle = FlatStyle.Flat;
            _rbHour.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _rbHour.Location = new Point(139, 3);
            _rbHour.Name = "_rbHour";
            _rbHour.Size = new Size(55, 17);
            _rbHour.TabIndex = 10;
            _rbHour.Text = "ساعت";
            _rbHour.UseVisualStyleBackColor = true;
            // 
            // rbMonth
            // 
            _rbMonth.AutoSize = true;
            _rbMonth.FlatStyle = FlatStyle.Flat;
            _rbMonth.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _rbMonth.Location = new Point(3, 3);
            _rbMonth.Name = "_rbMonth";
            _rbMonth.Size = new Size(38, 17);
            _rbMonth.TabIndex = 13;
            _rbMonth.Text = "ماه";
            _rbMonth.UseVisualStyleBackColor = true;
            // 
            // rbWeek
            // 
            _rbWeek.AutoSize = true;
            _rbWeek.FlatStyle = FlatStyle.Flat;
            _rbWeek.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _rbWeek.Location = new Point(45, 3);
            _rbWeek.Name = "_rbWeek";
            _rbWeek.Size = new Size(48, 17);
            _rbWeek.TabIndex = 12;
            _rbWeek.Text = "هفته";
            _rbWeek.UseVisualStyleBackColor = true;
            // 
            // txtCode
            // 
            txtCode.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtCode.Location = new Point(394, 20);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(248, 21);
            txtCode.TabIndex = 0;
            // 
            // Label14
            // 
            Label14.BackColor = SystemColors.Control;
            Label14.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label14.ForeColor = Color.Black;
            Label14.Location = new Point(644, 22);
            Label14.Name = "Label14";
            Label14.RightToLeft = RightToLeft.Yes;
            Label14.Size = new Size(78, 17);
            Label14.TabIndex = 164;
            Label14.Text = "کد مواد اولیه";
            // 
            // txtGarbagePercent
            // 
            txtGarbagePercent.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtGarbagePercent.Location = new Point(420, 315);
            txtGarbagePercent.Name = "txtGarbagePercent";
            txtGarbagePercent.Size = new Size(41, 21);
            txtGarbagePercent.TabIndex = 40;
            txtGarbagePercent.Visible = false;
            // 
            // Label18
            // 
            Label18.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label18.Location = new Point(404, 315);
            Label18.Name = "Label18";
            Label18.RightToLeft = RightToLeft.Yes;
            Label18.Size = new Size(18, 17);
            Label18.TabIndex = 293;
            Label18.Text = "%";
            Label18.Visible = false;
            // 
            // txtApplication
            // 
            txtApplication.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtApplication.Location = new Point(16, 188);
            txtApplication.Multiline = true;
            txtApplication.Name = "txtApplication";
            txtApplication.ScrollBars = ScrollBars.Vertical;
            txtApplication.Size = new Size(626, 55);
            txtApplication.TabIndex = 44;
            // 
            // Label15
            // 
            Label15.BackColor = SystemColors.Control;
            Label15.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label15.ForeColor = Color.Black;
            Label15.Location = new Point(644, 190);
            Label15.Name = "Label15";
            Label15.RightToLeft = RightToLeft.Yes;
            Label15.Size = new Size(64, 17);
            Label15.TabIndex = 294;
            Label15.Text = "موارد كاربرد";
            // 
            // frmPrimaryMaterial
            // 
            AcceptButton = _cmdSave;
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _cmdExit;
            ClientSize = new Size(756, 310);
            Controls.Add(txtApplication);
            Controls.Add(Label15);
            Controls.Add(txtCode);
            Controls.Add(Label14);
            Controls.Add(txtOrderPoint);
            Controls.Add(txtFirstHolding);
            Controls.Add(txtLastHolding);
            Controls.Add(Label8);
            Controls.Add(Label7);
            Controls.Add(cbStorePlace);
            Controls.Add(Label6);
            Controls.Add(Label5);
            Controls.Add(txtSpecification);
            Controls.Add(Label4);
            Controls.Add(txtName);
            Controls.Add(Label3);
            Controls.Add(cbProductionTestUnit);
            Controls.Add(cbStoreTestUnit);
            Controls.Add(Label2);
            Controls.Add(Label1);
            Controls.Add(Panel1);
            Controls.Add(txtGarbagePercent);
            Controls.Add(Label18);
            Controls.Add(Panel7);
            Controls.Add(txtBatchQuantity);
            Controls.Add(txtBuyTimeBatch);
            Controls.Add(txtMinimumCountPerOrder);
            Controls.Add(Label13);
            Controls.Add(Label12);
            Controls.Add(Label11);
            Controls.Add(Label10);
            Controls.Add(txtFillMeasureAppointMethod);
            Controls.Add(Label9);
            Controls.Add(lblName);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Location = new Point(50, 50);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmPrimaryMaterial";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " مشخصات مواد اولیه";
            Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)txtLastHolding).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtFirstHolding).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtMinimumCountPerOrder).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtBuyTimeBatch).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtBatchQuantity).EndInit();
            Panel7.ResumeLayout(false);
            Panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtGarbagePercent).EndInit();
            Load += new EventHandler(Form1_Load);
            FormClosing += new FormClosingEventHandler(frmPrimaryMaterial_FormClosing);
            ResumeLayout(false);
            PerformLayout();
        }

        internal ComboBox cbProductionTestUnit;
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

        internal ComboBox cbStoreTestUnit;
        internal Label Label2;
        internal Label Label1;
        internal System.Windows.Forms.Panel Panel1;
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

        internal Label lblName;
        internal TextBox txtName;
        internal Label Label3;
        internal TextBox txtSpecification;
        internal Label Label4;
        internal Label Label5;
        internal ComboBox cbStorePlace;
        internal Label Label6;
        internal Label Label7;
        internal Label Label8;
        internal TextBox txtFillMeasureAppointMethod;
        internal Label Label9;
        internal Label Label10;
        internal Label Label11;
        internal Label Label12;
        internal Label Label13;
        internal NumericUpDown txtLastHolding;
        internal NumericUpDown txtFirstHolding;
        internal NumericUpDown txtMinimumCountPerOrder;
        internal TextBox txtOrderPoint;
        internal NumericUpDown txtBuyTimeBatch;
        internal NumericUpDown txtBatchQuantity;
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

        internal System.Windows.Forms.Panel Panel7;
        private RadioButton _rbMinute;

        internal RadioButton rbMinute
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _rbMinute;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_rbMinute != null)
                {
                    _rbMinute.CheckedChanged -= rbMinute_CheckedChanged;
                }

                _rbMinute = value;
                if (_rbMinute != null)
                {
                    _rbMinute.CheckedChanged += rbMinute_CheckedChanged;
                }
            }
        }

        private RadioButton _rbDay;

        internal RadioButton rbDay
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _rbDay;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_rbDay != null)
                {
                    _rbDay.CheckedChanged -= rbDay_CheckedChanged;
                }

                _rbDay = value;
                if (_rbDay != null)
                {
                    _rbDay.CheckedChanged += rbDay_CheckedChanged;
                }
            }
        }

        private RadioButton _rbHour;

        internal RadioButton rbHour
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _rbHour;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_rbHour != null)
                {
                    _rbHour.CheckedChanged -= rbHour_CheckedChanged;
                }

                _rbHour = value;
                if (_rbHour != null)
                {
                    _rbHour.CheckedChanged += rbHour_CheckedChanged;
                }
            }
        }

        private RadioButton _rbMonth;

        internal RadioButton rbMonth
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _rbMonth;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_rbMonth != null)
                {
                    _rbMonth.CheckedChanged -= rbMonth_CheckedChanged;
                }

                _rbMonth = value;
                if (_rbMonth != null)
                {
                    _rbMonth.CheckedChanged += rbMonth_CheckedChanged;
                }
            }
        }

        private RadioButton _rbWeek;

        internal RadioButton rbWeek
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _rbWeek;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_rbWeek != null)
                {
                    _rbWeek.CheckedChanged -= rbWeek_CheckedChanged;
                }

                _rbWeek = value;
                if (_rbWeek != null)
                {
                    _rbWeek.CheckedChanged += rbWeek_CheckedChanged;
                }
            }
        }

        internal TextBox txtCode;
        internal Label Label14;
        internal NumericUpDown txtGarbagePercent;
        internal Label Label18;
        private RadioButton _RadioButton1;

        internal RadioButton RadioButton1
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _RadioButton1;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_RadioButton1 != null)
                {
                    _RadioButton1.CheckedChanged -= RadioButton1_CheckedChanged;
                }

                _RadioButton1 = value;
                if (_RadioButton1 != null)
                {
                    _RadioButton1.CheckedChanged += RadioButton1_CheckedChanged;
                }
            }
        }

        internal TextBox txtApplication;
        internal Label Label15;
    }
}