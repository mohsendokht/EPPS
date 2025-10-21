using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmRealProductionList : Form
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
            var DataGridViewCellStyle1 = new DataGridViewCellStyle();
            var DataGridViewCellStyle2 = new DataGridViewCellStyle();
            var DataGridViewCellStyle3 = new DataGridViewCellStyle();
            var DataGridViewCellStyle4 = new DataGridViewCellStyle();
            var DataGridViewCellStyle5 = new DataGridViewCellStyle();
            Label1 = new Label();
            gbConditions = new GroupBox();
            _cbOperators = new PSBMultiColumnComboBox.PSBMultiColumnComboBox();
            _cbOperators.SelectedChange += new PSBMultiColumnComboBox.PSBMultiColumnComboBox.SelectedChangeEventHandler(cbOperators_SelectedChange);
            _cbOperators.InputKeyDown += new PSBMultiColumnComboBox.PSBMultiColumnComboBox.InputKeyDownEventHandler(cbOperators_InputKeyDown);
            Panel3 = new System.Windows.Forms.Panel();
            _chkNotConfirmed = new CheckBox();
            _chkNotConfirmed.CheckedChanged += new EventHandler(chkNotConfirmed_CheckedChanged);
            _chkConfirmed = new CheckBox();
            _chkConfirmed.CheckedChanged += new EventHandler(chkConfirmed_CheckedChanged);
            cbSubbatch = new ComboBox();
            Label4 = new Label();
            cbProduct = new ComboBox();
            Label3 = new Label();
            cbOperation = new ComboBox();
            Label2 = new Label();
            txtToDate = new PSB_FarsiDateControl.PSB_DateControl();
            txtFromDate = new PSB_FarsiDateControl.PSB_DateControl();
            chkAll = new CheckBox();
            _cmdShow = new System.Windows.Forms.Button();
            _cmdShow.Click += new EventHandler(cmdShow_Click);
            lblName = new Label();
            Label7 = new Label();
            Label6 = new Label();
            Panel1 = new System.Windows.Forms.Panel();
            _dgList = new DataGridView();
            _dgList.CellFormatting += new DataGridViewCellFormattingEventHandler(dgList_CellFormatting);
            _dgList.KeyDown += new KeyEventHandler(dgList_KeyDown);
            Panel2 = new System.Windows.Forms.Panel();
            _CalDuringTimeButton = new System.Windows.Forms.Button();
            _CalDuringTimeButton.Click += new EventHandler(CalDuringTimeButton_Click);
            _cmdConfirm = new System.Windows.Forms.Button();
            _cmdConfirm.Click += new EventHandler(cmdConfirm_Click);
            _cmdConfilicts = new System.Windows.Forms.Button();
            _cmdConfilicts.Click += new EventHandler(cmdConfilicts_Click);
            _cmdHalts = new System.Windows.Forms.Button();
            _cmdHalts.Click += new EventHandler(cmdHalts_Click);
            _cmdExit = new System.Windows.Forms.Button();
            _cmdExit.Click += new EventHandler(cmdExit_Click);
            _cmdDelete = new System.Windows.Forms.Button();
            _cmdDelete.Click += new EventHandler(IUD_ButtonClick);
            _cmdUpdate = new System.Windows.Forms.Button();
            _cmdUpdate.Click += new EventHandler(IUD_ButtonClick);
            _cmdInsert = new System.Windows.Forms.Button();
            _cmdInsert.Click += new EventHandler(IUD_ButtonClick);
            gbConditions.SuspendLayout();
            Panel3.SuspendLayout();
            Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgList).BeginInit();
            Panel2.SuspendLayout();
            SuspendLayout();
            // 
            // Label1
            // 
            Label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label1.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label1.Location = new Point(966, 16);
            Label1.Name = "Label1";
            Label1.RightToLeft = RightToLeft.Yes;
            Label1.Size = new Size(99, 17);
            Label1.TabIndex = 166;
            Label1.Text = "کد پرسنلی اپراتور:";
            // 
            // gbConditions
            // 
            gbConditions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbConditions.Controls.Add(_cbOperators);
            gbConditions.Controls.Add(Panel3);
            gbConditions.Controls.Add(cbSubbatch);
            gbConditions.Controls.Add(Label4);
            gbConditions.Controls.Add(cbProduct);
            gbConditions.Controls.Add(Label3);
            gbConditions.Controls.Add(cbOperation);
            gbConditions.Controls.Add(Label2);
            gbConditions.Controls.Add(txtToDate);
            gbConditions.Controls.Add(txtFromDate);
            gbConditions.Controls.Add(chkAll);
            gbConditions.Controls.Add(_cmdShow);
            gbConditions.Controls.Add(lblName);
            gbConditions.Controls.Add(Label7);
            gbConditions.Controls.Add(Label6);
            gbConditions.Controls.Add(Label1);
            gbConditions.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            gbConditions.Location = new Point(6, 4);
            gbConditions.Name = "gbConditions";
            gbConditions.RightToLeft = RightToLeft.No;
            gbConditions.Size = new Size(1069, 111);
            gbConditions.TabIndex = 5;
            gbConditions.TabStop = false;
            // 
            // cbOperators
            // 
            _cbOperators.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _cbOperators.ListAlternateColor = Color.Beige;
            _cbOperators.Location = new Point(851, 13);
            _cbOperators.MinimumSize = new Size(82, 23);
            _cbOperators.Name = "_cbOperators";
            _cbOperators.RightToLeft = RightToLeft.Yes;
            _cbOperators.SeletedValue = null;
            _cbOperators.Size = new Size(114, 23);
            _cbOperators.TabIndex = 222;
            // 
            // Panel3
            // 
            Panel3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Panel3.BorderStyle = BorderStyle.FixedSingle;
            Panel3.Controls.Add(_chkNotConfirmed);
            Panel3.Controls.Add(_chkConfirmed);
            Panel3.Location = new Point(364, 44);
            Panel3.Name = "Panel3";
            Panel3.Size = new Size(262, 30);
            Panel3.TabIndex = 221;
            // 
            // chkNotConfirmed
            // 
            _chkNotConfirmed.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _chkNotConfirmed.AutoSize = true;
            _chkNotConfirmed.Location = new Point(21, 9);
            _chkNotConfirmed.Name = "_chkNotConfirmed";
            _chkNotConfirmed.RightToLeft = RightToLeft.Yes;
            _chkNotConfirmed.Size = new Size(74, 17);
            _chkNotConfirmed.TabIndex = 216;
            _chkNotConfirmed.Text = "تأیید نشده";
            _chkNotConfirmed.UseVisualStyleBackColor = true;
            // 
            // chkConfirmed
            // 
            _chkConfirmed.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _chkConfirmed.AutoSize = true;
            _chkConfirmed.Location = new Point(168, 8);
            _chkConfirmed.Name = "_chkConfirmed";
            _chkConfirmed.RightToLeft = RightToLeft.Yes;
            _chkConfirmed.Size = new Size(70, 17);
            _chkConfirmed.TabIndex = 215;
            _chkConfirmed.Text = "تأیید شده";
            _chkConfirmed.UseVisualStyleBackColor = true;
            // 
            // cbSubbatch
            // 
            cbSubbatch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbSubbatch.FlatStyle = FlatStyle.Flat;
            cbSubbatch.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cbSubbatch.FormattingEnabled = true;
            cbSubbatch.Location = new Point(364, 82);
            cbSubbatch.Name = "cbSubbatch";
            cbSubbatch.RightToLeft = RightToLeft.Yes;
            cbSubbatch.Size = new Size(262, 21);
            cbSubbatch.TabIndex = 219;
            // 
            // Label4
            // 
            Label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label4.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label4.Location = new Point(629, 83);
            Label4.Name = "Label4";
            Label4.RightToLeft = RightToLeft.Yes;
            Label4.Size = new Size(70, 17);
            Label4.TabIndex = 220;
            Label4.Text = "کد ساب بچ:";
            // 
            // cbProduct
            // 
            cbProduct.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbProduct.FlatStyle = FlatStyle.Flat;
            cbProduct.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cbProduct.FormattingEnabled = true;
            cbProduct.Location = new Point(705, 81);
            cbProduct.Name = "cbProduct";
            cbProduct.RightToLeft = RightToLeft.Yes;
            cbProduct.Size = new Size(282, 21);
            cbProduct.TabIndex = 217;
            // 
            // Label3
            // 
            Label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label3.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label3.Location = new Point(989, 82);
            Label3.Name = "Label3";
            Label3.RightToLeft = RightToLeft.Yes;
            Label3.Size = new Size(66, 17);
            Label3.TabIndex = 218;
            Label3.Text = "نام محصول:";
            // 
            // cbOperation
            // 
            cbOperation.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbOperation.FlatStyle = FlatStyle.Flat;
            cbOperation.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cbOperation.FormattingEnabled = true;
            cbOperation.Location = new Point(705, 48);
            cbOperation.Name = "cbOperation";
            cbOperation.RightToLeft = RightToLeft.Yes;
            cbOperation.Size = new Size(282, 21);
            cbOperation.TabIndex = 213;
            // 
            // Label2
            // 
            Label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label2.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label2.Location = new Point(989, 49);
            Label2.Name = "Label2";
            Label2.RightToLeft = RightToLeft.Yes;
            Label2.Size = new Size(76, 17);
            Label2.TabIndex = 214;
            Label2.Text = "عنوان فعالیت:";
            // 
            // txtToDate
            // 
            txtToDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtToDate.AutoValidate = AutoValidate.Disable;
            txtToDate.BackColor = Color.White;
            txtToDate.BackColorDateBox = Color.White;
            txtToDate.DateButtonShow = true;
            txtToDate.EnableDateText = true;
            txtToDate.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtToDate.FontMyDate = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtToDate.Location = new Point(364, 15);
            txtToDate.Margin = new Padding(4);
            txtToDate.MinimumSize = new Size(96, 24);
            txtToDate.Name = "txtToDate";
            txtToDate.Size = new Size(105, 24);
            txtToDate.TabIndex = 8;
            // 
            // txtFromDate
            // 
            txtFromDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtFromDate.AutoValidate = AutoValidate.Disable;
            txtFromDate.BackColor = Color.White;
            txtFromDate.BackColorDateBox = Color.White;
            txtFromDate.DateButtonShow = true;
            txtFromDate.EnableDateText = true;
            txtFromDate.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtFromDate.FontMyDate = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtFromDate.Location = new Point(521, 16);
            txtFromDate.Margin = new Padding(4);
            txtFromDate.MinimumSize = new Size(96, 24);
            txtFromDate.Name = "txtFromDate";
            txtFromDate.Size = new Size(105, 24);
            txtFromDate.TabIndex = 7;
            // 
            // chkAll
            // 
            chkAll.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            chkAll.ForeColor = Color.Blue;
            chkAll.Location = new Point(12, 43);
            chkAll.Name = "chkAll";
            chkAll.Size = new Size(88, 24);
            chkAll.TabIndex = 12;
            chkAll.Text = "نمایش همه";
            chkAll.UseVisualStyleBackColor = true;
            // 
            // cmdShow
            // 
            _cmdShow.BackColor = Color.Transparent;
            _cmdShow.Font = new Font("Tahoma", 9.0f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdShow.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdShow.Location = new Point(9, 70);
            _cmdShow.Name = "_cmdShow";
            _cmdShow.RightToLeft = RightToLeft.No;
            _cmdShow.Size = new Size(95, 27);
            _cmdShow.TabIndex = 10;
            _cmdShow.Text = "نمایش";
            _cmdShow.UseVisualStyleBackColor = false;
            // 
            // lblName
            // 
            lblName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblName.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblName.ForeColor = Color.Blue;
            lblName.Location = new Point(678, 14);
            lblName.Name = "lblName";
            lblName.RightToLeft = RightToLeft.Yes;
            lblName.Size = new Size(173, 20);
            lblName.TabIndex = 212;
            lblName.Text = "نام اپراتور";
            lblName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label7
            // 
            Label7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label7.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label7.Location = new Point(470, 19);
            Label7.Name = "Label7";
            Label7.RightToLeft = RightToLeft.Yes;
            Label7.Size = new Size(45, 18);
            Label7.TabIndex = 188;
            Label7.Text = "تا تاریخ:";
            // 
            // Label6
            // 
            Label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label6.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label6.Location = new Point(627, 18);
            Label6.Name = "Label6";
            Label6.RightToLeft = RightToLeft.Yes;
            Label6.Size = new Size(45, 18);
            Label6.TabIndex = 186;
            Label6.Text = "از تاریخ:";
            // 
            // Panel1
            // 
            Panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            Panel1.BorderStyle = BorderStyle.Fixed3D;
            Panel1.Controls.Add(_dgList);
            Panel1.Location = new Point(6, 172);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(1069, 284);
            Panel1.TabIndex = 4;
            // 
            // dgList
            // 
            _dgList.AllowUserToAddRows = false;
            _dgList.AllowUserToDeleteRows = false;
            DataGridViewCellStyle1.BackColor = Color.White;
            _dgList.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1;
            _dgList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            _dgList.BackgroundColor = SystemColors.ControlDark;
            DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle2.BackColor = SystemColors.Control;
            DataGridViewCellStyle2.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            _dgList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2;
            _dgList.ColumnHeadersHeight = 30;
            _dgList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
            DataGridViewCellStyle3.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            DataGridViewCellStyle3.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            DataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            _dgList.DefaultCellStyle = DataGridViewCellStyle3;
            _dgList.Location = new Point(6, 7);
            _dgList.Name = "_dgList";
            _dgList.ReadOnly = true;
            DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridViewCellStyle4.BackColor = SystemColors.Control;
            DataGridViewCellStyle4.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            DataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            _dgList.RowHeadersDefaultCellStyle = DataGridViewCellStyle4;
            _dgList.RowHeadersWidth = 70;
            _dgList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle5.BackColor = SystemColors.Info;
            DataGridViewCellStyle5.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle5.ForeColor = Color.Black;
            DataGridViewCellStyle5.SelectionBackColor = Color.RoyalBlue;
            _dgList.RowsDefaultCellStyle = DataGridViewCellStyle5;
            _dgList.RowTemplate.Resizable = DataGridViewTriState.True;
            _dgList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgList.Size = new Size(1057, 271);
            _dgList.TabIndex = 1;
            // 
            // Panel2
            // 
            Panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Panel2.BackColor = SystemColors.Control;
            Panel2.BorderStyle = BorderStyle.FixedSingle;
            Panel2.Controls.Add(_CalDuringTimeButton);
            Panel2.Controls.Add(_cmdConfirm);
            Panel2.Controls.Add(_cmdConfilicts);
            Panel2.Controls.Add(_cmdHalts);
            Panel2.Controls.Add(_cmdExit);
            Panel2.Controls.Add(_cmdDelete);
            Panel2.Controls.Add(_cmdUpdate);
            Panel2.Controls.Add(_cmdInsert);
            Panel2.Location = new Point(6, 120);
            Panel2.Name = "Panel2";
            Panel2.Size = new Size(1069, 48);
            Panel2.TabIndex = 10;
            // 
            // CalDuringTimeButton
            // 
            _CalDuringTimeButton.BackColor = Color.Transparent;
            _CalDuringTimeButton.DialogResult = DialogResult.Cancel;
            _CalDuringTimeButton.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _CalDuringTimeButton.ImageAlign = ContentAlignment.MiddleRight;
            _CalDuringTimeButton.Location = new Point(366, 10);
            _CalDuringTimeButton.Name = "_CalDuringTimeButton";
            _CalDuringTimeButton.Size = new Size(117, 27);
            _CalDuringTimeButton.TabIndex = 9;
            _CalDuringTimeButton.Text = "محاسبه مدت تولید";
            _CalDuringTimeButton.UseVisualStyleBackColor = false;
            // 
            // cmdConfirm
            // 
            _cmdConfirm.BackColor = Color.Transparent;
            _cmdConfirm.DialogResult = DialogResult.Cancel;
            _cmdConfirm.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdConfirm.ImageAlign = ContentAlignment.MiddleRight;
            _cmdConfirm.Location = new Point(236, 10);
            _cmdConfirm.Name = "_cmdConfirm";
            _cmdConfirm.Size = new Size(117, 27);
            _cmdConfirm.TabIndex = 8;
            _cmdConfirm.Text = @"تایید\لغو تایید ثبت";
            _cmdConfirm.UseVisualStyleBackColor = false;
            // 
            // cmdConfilicts
            // 
            _cmdConfilicts.BackColor = Color.Transparent;
            _cmdConfilicts.DialogResult = DialogResult.Cancel;
            _cmdConfilicts.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdConfilicts.ImageAlign = ContentAlignment.MiddleRight;
            _cmdConfilicts.Location = new Point(115, 10);
            _cmdConfilicts.Name = "_cmdConfilicts";
            _cmdConfilicts.Size = new Size(115, 27);
            _cmdConfilicts.TabIndex = 7;
            _cmdConfilicts.Text = "لیست مغایرتها";
            _cmdConfilicts.UseVisualStyleBackColor = false;
            // 
            // cmdHalts
            // 
            _cmdHalts.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _cmdHalts.BackColor = Color.DarkGray;
            _cmdHalts.DialogResult = DialogResult.Cancel;
            _cmdHalts.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdHalts.ImageAlign = ContentAlignment.MiddleRight;
            _cmdHalts.Location = new Point(651, 10);
            _cmdHalts.Name = "_cmdHalts";
            _cmdHalts.Size = new Size(115, 27);
            _cmdHalts.TabIndex = 6;
            _cmdHalts.Text = "توقفات عملیات";
            _cmdHalts.TextAlign = ContentAlignment.MiddleLeft;
            _cmdHalts.UseVisualStyleBackColor = false;
            _cmdHalts.Visible = false;
            // 
            // cmdExit
            // 
            _cmdExit.BackColor = Color.Transparent;
            _cmdExit.DialogResult = DialogResult.Cancel;
            _cmdExit.Font = new Font("Tahoma", 9.0f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdExit.ForeColor = Color.Red;
            _cmdExit.Location = new Point(14, 10);
            _cmdExit.Name = "_cmdExit";
            _cmdExit.Size = new Size(95, 27);
            _cmdExit.TabIndex = 5;
            _cmdExit.Text = "خروج";
            _cmdExit.UseVisualStyleBackColor = false;
            // 
            // cmdDelete
            // 
            _cmdDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _cmdDelete.BackColor = Color.Transparent;
            _cmdDelete.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdDelete.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdDelete.Location = new Point(974, 10);
            _cmdDelete.Name = "_cmdDelete";
            _cmdDelete.RightToLeft = RightToLeft.No;
            _cmdDelete.Size = new Size(85, 27);
            _cmdDelete.TabIndex = 2;
            _cmdDelete.Text = "حذف";
            _cmdDelete.TextAlign = ContentAlignment.MiddleRight;
            _cmdDelete.UseVisualStyleBackColor = false;
            // 
            // cmdUpdate
            // 
            _cmdUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _cmdUpdate.BackColor = Color.Transparent;
            _cmdUpdate.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdUpdate.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdUpdate.Location = new Point(883, 10);
            _cmdUpdate.Name = "_cmdUpdate";
            _cmdUpdate.RightToLeft = RightToLeft.No;
            _cmdUpdate.Size = new Size(85, 27);
            _cmdUpdate.TabIndex = 1;
            _cmdUpdate.Text = "اصلاح";
            _cmdUpdate.TextAlign = ContentAlignment.MiddleRight;
            _cmdUpdate.UseVisualStyleBackColor = false;
            // 
            // cmdInsert
            // 
            _cmdInsert.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _cmdInsert.BackColor = Color.Transparent;
            _cmdInsert.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdInsert.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdInsert.Location = new Point(792, 10);
            _cmdInsert.Name = "_cmdInsert";
            _cmdInsert.RightToLeft = RightToLeft.No;
            _cmdInsert.Size = new Size(85, 27);
            _cmdInsert.TabIndex = 0;
            _cmdInsert.Text = "جدید";
            _cmdInsert.TextAlign = ContentAlignment.MiddleRight;
            _cmdInsert.UseVisualStyleBackColor = false;
            // 
            // frmRealProductionList
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1081, 470);
            Controls.Add(Panel2);
            Controls.Add(gbConditions);
            Controls.Add(Panel1);
            MinimumSize = new Size(860, 497);
            Name = "frmRealProductionList";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = " لیست اطلاعات تولید";
            gbConditions.ResumeLayout(false);
            Panel3.ResumeLayout(false);
            Panel3.PerformLayout();
            Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_dgList).EndInit();
            Panel2.ResumeLayout(false);
            Load += new EventHandler(frmRealProduction_Load);
            FormClosing += new FormClosingEventHandler(frmRealProductionList_FormClosing);
            ResumeLayout(false);
        }

        internal Label Label1;
        internal GroupBox gbConditions;
        internal Label Label7;
        internal Label Label6;
        internal CheckBox chkAll;
        private System.Windows.Forms.Button _cmdShow;

        internal System.Windows.Forms.Button cmdShow
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdShow;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdShow != null)
                {
                    _cmdShow.Click -= cmdShow_Click;
                }

                _cmdShow = value;
                if (_cmdShow != null)
                {
                    _cmdShow.Click += cmdShow_Click;
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
                    _dgList.CellFormatting -= dgList_CellFormatting;
                    _dgList.KeyDown -= dgList_KeyDown;
                }

                _dgList = value;
                if (_dgList != null)
                {
                    _dgList.CellFormatting += dgList_CellFormatting;
                    _dgList.KeyDown += dgList_KeyDown;
                }
            }
        }

        internal System.Windows.Forms.Panel Panel2;
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
                    _cmdDelete.Click -= IUD_ButtonClick;
                }

                _cmdDelete = value;
                if (_cmdDelete != null)
                {
                    _cmdDelete.Click += IUD_ButtonClick;
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
                    _cmdUpdate.Click -= IUD_ButtonClick;
                }

                _cmdUpdate = value;
                if (_cmdUpdate != null)
                {
                    _cmdUpdate.Click += IUD_ButtonClick;
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
                    _cmdInsert.Click -= IUD_ButtonClick;
                }

                _cmdInsert = value;
                if (_cmdInsert != null)
                {
                    _cmdInsert.Click += IUD_ButtonClick;
                }
            }
        }

        private System.Windows.Forms.Button _cmdHalts;

        internal System.Windows.Forms.Button cmdHalts
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdHalts;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdHalts != null)
                {
                    _cmdHalts.Click -= cmdHalts_Click;
                }

                _cmdHalts = value;
                if (_cmdHalts != null)
                {
                    _cmdHalts.Click += cmdHalts_Click;
                }
            }
        }

        private System.Windows.Forms.Button _cmdConfilicts;

        internal System.Windows.Forms.Button cmdConfilicts
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdConfilicts;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdConfilicts != null)
                {
                    _cmdConfilicts.Click -= cmdConfilicts_Click;
                }

                _cmdConfilicts = value;
                if (_cmdConfilicts != null)
                {
                    _cmdConfilicts.Click += cmdConfilicts_Click;
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

        internal Label lblName;
        internal PSB_FarsiDateControl.PSB_DateControl txtToDate;
        internal PSB_FarsiDateControl.PSB_DateControl txtFromDate;
        internal ComboBox cbOperation;
        internal Label Label2;
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

        internal ComboBox cbSubbatch;
        internal Label Label4;
        internal ComboBox cbProduct;
        internal Label Label3;
        internal System.Windows.Forms.Panel Panel3;
        private PSBMultiColumnComboBox.PSBMultiColumnComboBox _cbOperators;

        internal PSBMultiColumnComboBox.PSBMultiColumnComboBox cbOperators
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cbOperators;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cbOperators != null)
                {
                    _cbOperators.SelectedChange -= cbOperators_SelectedChange;
                    _cbOperators.InputKeyDown -= cbOperators_InputKeyDown;
                }

                _cbOperators = value;
                if (_cbOperators != null)
                {
                    _cbOperators.SelectedChange += cbOperators_SelectedChange;
                    _cbOperators.InputKeyDown += cbOperators_InputKeyDown;
                }
            }
        }

        private System.Windows.Forms.Button _CalDuringTimeButton;

        internal System.Windows.Forms.Button CalDuringTimeButton
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _CalDuringTimeButton;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CalDuringTimeButton != null)
                {
                    _CalDuringTimeButton.Click -= CalDuringTimeButton_Click;
                }

                _CalDuringTimeButton = value;
                if (_CalDuringTimeButton != null)
                {
                    _CalDuringTimeButton.Click += CalDuringTimeButton_Click;
                }
            }
        }
    }
}