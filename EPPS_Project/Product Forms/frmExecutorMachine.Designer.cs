using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmExecutorMachine : Form
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
            gbMachine = new GroupBox();
            GroupBox2 = new GroupBox();
            txtSetupTime = new NumericUpDown();
            Label16 = new Label();
            cbSetupTimeType = new ComboBox();
            Label2 = new Label();
            GroupBox1 = new GroupBox();
            txtOneExecutionTime = new NumericUpDown();
            Label21 = new Label();
            cbTimeType = new ComboBox();
            Label1 = new Label();
            txtPriority = new NumericUpDown();
            txtRedoPercent = new NumericUpDown();
            Label15 = new Label();
            txtOperatorPerformancePercent = new NumericUpDown();
            Label14 = new Label();
            txtPerformancePercent = new NumericUpDown();
            Label13 = new Label();
            txtGarbagePercent = new NumericUpDown();
            Label12 = new Label();
            txtMaximumAccumulation = new NumericUpDown();
            Label7 = new Label();
            txtMinimumProduction = new NumericUpDown();
            Label10 = new Label();
            Label24 = new Label();
            txtOperator = new NumericUpDown();
            Label25 = new Label();
            Label26 = new Label();
            txtOneExecutionPrice = new TextBox();
            cbExecutorMachine = new ComboBox();
            Label27 = new Label();
            Label20 = new Label();
            Label19 = new Label();
            Label18 = new Label();
            Label17 = new Label();
            Panel1 = new System.Windows.Forms.Panel();
            _cmdSave = new System.Windows.Forms.Button();
            _cmdSave.Click += new EventHandler(cmdSave_Click);
            _cmdExit = new System.Windows.Forms.Button();
            _cmdExit.Click += new EventHandler(cmdExit_Click);
            chkIsParallelMachine = new CheckBox();
            gbMachine.SuspendLayout();
            GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtSetupTime).BeginInit();
            GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtOneExecutionTime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtPriority).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtRedoPercent).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtOperatorPerformancePercent).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtPerformancePercent).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtGarbagePercent).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtMaximumAccumulation).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtMinimumProduction).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtOperator).BeginInit();
            Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // gbMachine
            // 
            gbMachine.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            gbMachine.Controls.Add(chkIsParallelMachine);
            gbMachine.Controls.Add(GroupBox2);
            gbMachine.Controls.Add(GroupBox1);
            gbMachine.Controls.Add(txtPriority);
            gbMachine.Controls.Add(txtRedoPercent);
            gbMachine.Controls.Add(Label15);
            gbMachine.Controls.Add(txtOperatorPerformancePercent);
            gbMachine.Controls.Add(Label14);
            gbMachine.Controls.Add(txtPerformancePercent);
            gbMachine.Controls.Add(Label13);
            gbMachine.Controls.Add(txtGarbagePercent);
            gbMachine.Controls.Add(Label12);
            gbMachine.Controls.Add(txtMaximumAccumulation);
            gbMachine.Controls.Add(Label7);
            gbMachine.Controls.Add(txtMinimumProduction);
            gbMachine.Controls.Add(Label10);
            gbMachine.Controls.Add(Label24);
            gbMachine.Controls.Add(txtOperator);
            gbMachine.Controls.Add(Label25);
            gbMachine.Controls.Add(Label26);
            gbMachine.Controls.Add(txtOneExecutionPrice);
            gbMachine.Controls.Add(cbExecutorMachine);
            gbMachine.Controls.Add(Label27);
            gbMachine.Controls.Add(Label20);
            gbMachine.Controls.Add(Label19);
            gbMachine.Controls.Add(Label18);
            gbMachine.Controls.Add(Label17);
            gbMachine.Location = new Point(12, 12);
            gbMachine.Name = "gbMachine";
            gbMachine.Size = new Size(704, 279);
            gbMachine.TabIndex = 300;
            gbMachine.TabStop = false;
            // 
            // GroupBox2
            // 
            GroupBox2.Controls.Add(txtSetupTime);
            GroupBox2.Controls.Add(Label16);
            GroupBox2.Controls.Add(cbSetupTimeType);
            GroupBox2.Controls.Add(Label2);
            GroupBox2.Location = new Point(12, 63);
            GroupBox2.Name = "GroupBox2";
            GroupBox2.Size = new Size(300, 64);
            GroupBox2.TabIndex = 3;
            GroupBox2.TabStop = false;
            GroupBox2.Text = "زمان Setup ماشین";
            // 
            // txtSetupTime
            // 
            txtSetupTime.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtSetupTime.Location = new Point(172, 27);
            txtSetupTime.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            txtSetupTime.Name = "txtSetupTime";
            txtSetupTime.Size = new Size(83, 21);
            txtSetupTime.TabIndex = 0;
            // 
            // Label16
            // 
            Label16.BackColor = SystemColors.Control;
            Label16.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label16.ForeColor = Color.Black;
            Label16.Location = new Point(257, 29);
            Label16.Name = "Label16";
            Label16.RightToLeft = RightToLeft.Yes;
            Label16.Size = new Size(35, 17);
            Label16.TabIndex = 325;
            Label16.Text = "مقدار";
            // 
            // cbSetupTimeType
            // 
            cbSetupTimeType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSetupTimeType.FlatStyle = FlatStyle.Flat;
            cbSetupTimeType.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cbSetupTimeType.FormattingEnabled = true;
            cbSetupTimeType.Items.AddRange(new object[] { "ثانیه", "دقیقه", "ساعت", "روز", "هفته", "ماه" });
            cbSetupTimeType.Location = new Point(8, 27);
            cbSetupTimeType.Name = "cbSetupTimeType";
            cbSetupTimeType.Size = new Size(84, 21);
            cbSetupTimeType.TabIndex = 1;
            // 
            // Label2
            // 
            Label2.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label2.Location = new Point(96, 29);
            Label2.Name = "Label2";
            Label2.RightToLeft = RightToLeft.Yes;
            Label2.Size = new Size(50, 17);
            Label2.TabIndex = 331;
            Label2.Text = "نوع زمان";
            // 
            // GroupBox1
            // 
            GroupBox1.Controls.Add(txtOneExecutionTime);
            GroupBox1.Controls.Add(Label21);
            GroupBox1.Controls.Add(cbTimeType);
            GroupBox1.Controls.Add(Label1);
            GroupBox1.Location = new Point(381, 63);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(300, 64);
            GroupBox1.TabIndex = 2;
            GroupBox1.TabStop = false;
            GroupBox1.Text = "زمان انجام یکبار عملیات";
            // 
            // txtOneExecutionTime
            // 
            txtOneExecutionTime.DecimalPlaces = 2;
            txtOneExecutionTime.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtOneExecutionTime.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            txtOneExecutionTime.Location = new Point(171, 27);
            txtOneExecutionTime.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            txtOneExecutionTime.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            txtOneExecutionTime.Name = "txtOneExecutionTime";
            txtOneExecutionTime.Size = new Size(84, 21);
            txtOneExecutionTime.TabIndex = 0;
            txtOneExecutionTime.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // Label21
            // 
            Label21.BackColor = SystemColors.Control;
            Label21.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label21.ForeColor = Color.Black;
            Label21.Location = new Point(258, 29);
            Label21.Name = "Label21";
            Label21.RightToLeft = RightToLeft.Yes;
            Label21.Size = new Size(35, 17);
            Label21.TabIndex = 318;
            Label21.Text = "مقدار";
            // 
            // cbTimeType
            // 
            cbTimeType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTimeType.FlatStyle = FlatStyle.Flat;
            cbTimeType.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cbTimeType.FormattingEnabled = true;
            cbTimeType.Items.AddRange(new object[] { "ثانیه", "دقیقه", "ساعت", "روز", "هفته", "ماه" });
            cbTimeType.Location = new Point(8, 28);
            cbTimeType.Name = "cbTimeType";
            cbTimeType.Size = new Size(84, 21);
            cbTimeType.TabIndex = 1;
            // 
            // Label1
            // 
            Label1.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label1.Location = new Point(96, 30);
            Label1.Name = "Label1";
            Label1.RightToLeft = RightToLeft.Yes;
            Label1.Size = new Size(50, 17);
            Label1.TabIndex = 330;
            Label1.Text = "نوع زمان";
            // 
            // txtPriority
            // 
            txtPriority.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtPriority.Location = new Point(12, 22);
            txtPriority.Name = "txtPriority";
            txtPriority.Size = new Size(65, 21);
            txtPriority.TabIndex = 1;
            // 
            // txtRedoPercent
            // 
            txtRedoPercent.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtRedoPercent.Location = new Point(36, 194);
            txtRedoPercent.Name = "txtRedoPercent";
            txtRedoPercent.Size = new Size(60, 21);
            txtRedoPercent.TabIndex = 10;
            // 
            // Label15
            // 
            Label15.BackColor = SystemColors.Control;
            Label15.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label15.ForeColor = Color.Black;
            Label15.Location = new Point(99, 196);
            Label15.Name = "Label15";
            Label15.RightToLeft = RightToLeft.Yes;
            Label15.Size = new Size(65, 17);
            Label15.TabIndex = 324;
            Label15.Text = "دوباره کاری";
            // 
            // txtOperatorPerformancePercent
            // 
            txtOperatorPerformancePercent.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtOperatorPerformancePercent.Location = new Point(391, 194);
            txtOperatorPerformancePercent.Name = "txtOperatorPerformancePercent";
            txtOperatorPerformancePercent.Size = new Size(60, 21);
            txtOperatorPerformancePercent.TabIndex = 8;
            // 
            // Label14
            // 
            Label14.BackColor = SystemColors.Control;
            Label14.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label14.ForeColor = Color.Black;
            Label14.Location = new Point(454, 196);
            Label14.Name = "Label14";
            Label14.RightToLeft = RightToLeft.Yes;
            Label14.Size = new Size(70, 17);
            Label14.TabIndex = 323;
            Label14.Text = "کارایی اپراتور";
            // 
            // txtPerformancePercent
            // 
            txtPerformancePercent.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtPerformancePercent.Location = new Point(206, 194);
            txtPerformancePercent.Name = "txtPerformancePercent";
            txtPerformancePercent.Size = new Size(60, 21);
            txtPerformancePercent.TabIndex = 9;
            // 
            // Label13
            // 
            Label13.BackColor = SystemColors.Control;
            Label13.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label13.ForeColor = Color.Black;
            Label13.Location = new Point(269, 196);
            Label13.Name = "Label13";
            Label13.RightToLeft = RightToLeft.Yes;
            Label13.Size = new Size(78, 17);
            Label13.TabIndex = 322;
            Label13.Text = "کارایی ماشین";
            // 
            // txtGarbagePercent
            // 
            txtGarbagePercent.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtGarbagePercent.Location = new Point(576, 194);
            txtGarbagePercent.Name = "txtGarbagePercent";
            txtGarbagePercent.Size = new Size(60, 21);
            txtGarbagePercent.TabIndex = 7;
            // 
            // Label12
            // 
            Label12.BackColor = SystemColors.Control;
            Label12.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label12.ForeColor = Color.Black;
            Label12.Location = new Point(639, 196);
            Label12.Name = "Label12";
            Label12.RightToLeft = RightToLeft.Yes;
            Label12.Size = new Size(45, 17);
            Label12.TabIndex = 321;
            Label12.Text = "ضایعات";
            // 
            // txtMaximumAccumulation
            // 
            txtMaximumAccumulation.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtMaximumAccumulation.Location = new Point(12, 149);
            txtMaximumAccumulation.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            txtMaximumAccumulation.Name = "txtMaximumAccumulation";
            txtMaximumAccumulation.Size = new Size(106, 21);
            txtMaximumAccumulation.TabIndex = 6;
            // 
            // Label7
            // 
            Label7.BackColor = SystemColors.Control;
            Label7.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label7.ForeColor = Color.Black;
            Label7.Location = new Point(124, 151);
            Label7.Name = "Label7";
            Label7.RightToLeft = RightToLeft.Yes;
            Label7.Size = new Size(122, 17);
            Label7.TabIndex = 320;
            Label7.Text = "حداکثر تعداد انباشتگی";
            // 
            // txtMinimumProduction
            // 
            txtMinimumProduction.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtMinimumProduction.Location = new Point(275, 147);
            txtMinimumProduction.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            txtMinimumProduction.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            txtMinimumProduction.Name = "txtMinimumProduction";
            txtMinimumProduction.Size = new Size(106, 21);
            txtMinimumProduction.TabIndex = 5;
            txtMinimumProduction.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // Label10
            // 
            Label10.BackColor = SystemColors.Control;
            Label10.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label10.ForeColor = Color.Black;
            Label10.Location = new Point(387, 149);
            Label10.Name = "Label10";
            Label10.RightToLeft = RightToLeft.Yes;
            Label10.Size = new Size(140, 17);
            Label10.TabIndex = 319;
            Label10.Text = "حداقل تعداد تولید اقتصادی";
            // 
            // Label24
            // 
            Label24.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label24.Location = new Point(83, 24);
            Label24.Name = "Label24";
            Label24.RightToLeft = RightToLeft.Yes;
            Label24.Size = new Size(77, 17);
            Label24.TabIndex = 317;
            Label24.Text = "اولویت ماشین";
            // 
            // txtOperator
            // 
            txtOperator.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtOperator.Location = new Point(552, 147);
            txtOperator.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            txtOperator.Name = "txtOperator";
            txtOperator.Size = new Size(84, 21);
            txtOperator.TabIndex = 4;
            txtOperator.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // Label25
            // 
            Label25.BackColor = SystemColors.Control;
            Label25.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label25.ForeColor = Color.Black;
            Label25.Location = new Point(639, 149);
            Label25.Name = "Label25";
            Label25.RightToLeft = RightToLeft.Yes;
            Label25.Size = new Size(63, 17);
            Label25.TabIndex = 316;
            Label25.Text = "تعداد اپراتور";
            // 
            // Label26
            // 
            Label26.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label26.Location = new Point(639, 24);
            Label26.Name = "Label26";
            Label26.RightToLeft = RightToLeft.Yes;
            Label26.Size = new Size(44, 17);
            Label26.TabIndex = 315;
            Label26.Text = "ماشین";
            // 
            // txtOneExecutionPrice
            // 
            txtOneExecutionPrice.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtOneExecutionPrice.Location = new Point(12, 245);
            txtOneExecutionPrice.Name = "txtOneExecutionPrice";
            txtOneExecutionPrice.Size = new Size(255, 21);
            txtOneExecutionPrice.TabIndex = 11;
            txtOneExecutionPrice.TextAlign = HorizontalAlignment.Center;
            // 
            // cbExecutorMachine
            // 
            cbExecutorMachine.DropDownStyle = ComboBoxStyle.DropDownList;
            cbExecutorMachine.FlatStyle = FlatStyle.Flat;
            cbExecutorMachine.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cbExecutorMachine.FormattingEnabled = true;
            cbExecutorMachine.Location = new Point(170, 22);
            cbExecutorMachine.Name = "cbExecutorMachine";
            cbExecutorMachine.Size = new Size(466, 21);
            cbExecutorMachine.TabIndex = 0;
            // 
            // Label27
            // 
            Label27.BackColor = SystemColors.Control;
            Label27.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label27.ForeColor = Color.Black;
            Label27.Location = new Point(269, 247);
            Label27.Name = "Label27";
            Label27.RightToLeft = RightToLeft.Yes;
            Label27.Size = new Size(133, 17);
            Label27.TabIndex = 314;
            Label27.Text = "هزینه اجرای یکبار عملیات";
            // 
            // Label20
            // 
            Label20.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label20.Location = new Point(12, 196);
            Label20.Name = "Label20";
            Label20.RightToLeft = RightToLeft.Yes;
            Label20.Size = new Size(18, 17);
            Label20.TabIndex = 329;
            Label20.Text = "%";
            // 
            // Label19
            // 
            Label19.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label19.Location = new Point(367, 196);
            Label19.Name = "Label19";
            Label19.RightToLeft = RightToLeft.Yes;
            Label19.Size = new Size(18, 17);
            Label19.TabIndex = 328;
            Label19.Text = "%";
            // 
            // Label18
            // 
            Label18.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label18.Location = new Point(552, 196);
            Label18.Name = "Label18";
            Label18.RightToLeft = RightToLeft.Yes;
            Label18.Size = new Size(18, 17);
            Label18.TabIndex = 327;
            Label18.Text = "%";
            // 
            // Label17
            // 
            Label17.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label17.Location = new Point(182, 196);
            Label17.Name = "Label17";
            Label17.RightToLeft = RightToLeft.Yes;
            Label17.Size = new Size(18, 17);
            Label17.TabIndex = 326;
            Label17.Text = "%";
            // 
            // Panel1
            // 
            Panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Panel1.BackColor = SystemColors.Control;
            Panel1.BorderStyle = BorderStyle.FixedSingle;
            Panel1.Controls.Add(_cmdSave);
            Panel1.Controls.Add(_cmdExit);
            Panel1.Location = new Point(12, 300);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(704, 48);
            Panel1.TabIndex = 334;
            // 
            // cmdSave
            // 
            _cmdSave.BackColor = Color.Transparent;
            _cmdSave.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdSave.ForeColor = Color.Black;
            _cmdSave.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdSave.Location = new Point(363, 10);
            _cmdSave.Name = "_cmdSave";
            _cmdSave.RightToLeft = RightToLeft.No;
            _cmdSave.Size = new Size(90, 28);
            _cmdSave.TabIndex = 12;
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
            _cmdExit.Location = new Point(241, 10);
            _cmdExit.Name = "_cmdExit";
            _cmdExit.RightToLeft = RightToLeft.No;
            _cmdExit.Size = new Size(90, 28);
            _cmdExit.TabIndex = 13;
            _cmdExit.Text = "انصراف";
            _cmdExit.TextAlign = ContentAlignment.MiddleRight;
            _cmdExit.UseVisualStyleBackColor = false;
            // 
            // chkIsParallelMachine
            // 
            chkIsParallelMachine.AutoSize = true;
            chkIsParallelMachine.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            chkIsParallelMachine.ForeColor = Color.Red;
            chkIsParallelMachine.Location = new Point(421, 247);
            chkIsParallelMachine.Name = "chkIsParallelMachine";
            chkIsParallelMachine.Size = new Size(278, 17);
            chkIsParallelMachine.TabIndex = 332;
            chkIsParallelMachine.Text = "این ماشین در اجرای موازی عملیات شرکت داشته باشد";
            chkIsParallelMachine.UseVisualStyleBackColor = true;
            // 
            // frmExecutorMachine
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(728, 354);
            Controls.Add(gbMachine);
            Controls.Add(Panel1);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmExecutorMachine";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "  مشخصات ماشین انجام دهنده عملیات";
            gbMachine.ResumeLayout(false);
            gbMachine.PerformLayout();
            GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)txtSetupTime).EndInit();
            GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)txtOneExecutionTime).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtPriority).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtRedoPercent).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtOperatorPerformancePercent).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtPerformancePercent).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtGarbagePercent).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtMaximumAccumulation).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtMinimumProduction).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtOperator).EndInit();
            Panel1.ResumeLayout(false);
            Load += new EventHandler(frmExecutorMachine_Load);
            FormClosing += new FormClosingEventHandler(frmOperationMaterial_FormClosing);
            ResumeLayout(false);
        }

        internal GroupBox gbMachine;
        internal Label Label2;
        internal ComboBox cbSetupTimeType;
        internal Label Label1;
        internal ComboBox cbTimeType;
        internal NumericUpDown txtSetupTime;
        internal Label Label16;
        internal NumericUpDown txtRedoPercent;
        internal Label Label15;
        internal NumericUpDown txtOperatorPerformancePercent;
        internal Label Label14;
        internal NumericUpDown txtPerformancePercent;
        internal Label Label13;
        internal NumericUpDown txtGarbagePercent;
        internal Label Label12;
        internal NumericUpDown txtMaximumAccumulation;
        internal Label Label7;
        internal NumericUpDown txtMinimumProduction;
        internal Label Label10;
        internal NumericUpDown txtOneExecutionTime;
        internal Label Label21;
        internal Label Label24;
        internal NumericUpDown txtOperator;
        internal Label Label25;
        internal Label Label26;
        internal TextBox txtOneExecutionPrice;
        internal ComboBox cbExecutorMachine;
        internal Label Label27;
        internal Label Label20;
        internal Label Label19;
        internal Label Label18;
        internal Label Label17;
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

        internal NumericUpDown txtPriority;
        internal GroupBox GroupBox1;
        internal GroupBox GroupBox2;
        internal CheckBox chkIsParallelMachine;
    }
}