using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmMPS : Form
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
            var DataGridViewCellStyle35 = new DataGridViewCellStyle();
            var DataGridViewCellStyle36 = new DataGridViewCellStyle();
            var DataGridViewCellStyle37 = new DataGridViewCellStyle();
            var DataGridViewCellStyle38 = new DataGridViewCellStyle();
            var DataGridViewCellStyle39 = new DataGridViewCellStyle();
            var DataGridViewCellStyle40 = new DataGridViewCellStyle();
            var DataGridViewCellStyle41 = new DataGridViewCellStyle();
            var DataGridViewCellStyle42 = new DataGridViewCellStyle();
            var DataGridViewCellStyle43 = new DataGridViewCellStyle();
            var DataGridViewCellStyle44 = new DataGridViewCellStyle();
            var DataGridViewCellStyle45 = new DataGridViewCellStyle();
            var DataGridViewCellStyle46 = new DataGridViewCellStyle();
            var DataGridViewCellStyle47 = new DataGridViewCellStyle();
            var DataGridViewCellStyle48 = new DataGridViewCellStyle();
            var DataGridViewCellStyle49 = new DataGridViewCellStyle();
            var DataGridViewCellStyle50 = new DataGridViewCellStyle();
            var DataGridViewCellStyle51 = new DataGridViewCellStyle();
            _dgMPSList = new DataGridView();
            _dgMPSList.KeyDown += new KeyEventHandler(dgMPSList_KeyDown);
            _dgMPSList.RowsAdded += new DataGridViewRowsAddedEventHandler(dgMPSList_RowsAdded);
            _dgMPSList.RowsRemoved += new DataGridViewRowsRemovedEventHandler(dgMPSList_RowsRemoved);
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            Column8 = new DataGridViewTextBoxColumn();
            Column9 = new DataGridViewTextBoxColumn();
            Column10 = new DataGridViewTextBoxColumn();
            Column11 = new DataGridViewTextBoxColumn();
            Column12 = new DataGridViewTextBoxColumn();
            Column13 = new DataGridViewTextBoxColumn();
            Column14 = new DataGridViewTextBoxColumn();
            Column15 = new DataGridViewTextBoxColumn();
            Panel1 = new System.Windows.Forms.Panel();
            _cmdSave = new System.Windows.Forms.Button();
            _cmdSave.Click += new EventHandler(cmdSave_Click);
            _cmdExit = new System.Windows.Forms.Button();
            _cmdExit.Click += new EventHandler(cmdExit_Click);
            _cmdDelete = new System.Windows.Forms.Button();
            _cmdDelete.Click += new EventHandler(cmdDelete_Click);
            GroupBox1 = new GroupBox();
            lblLastUpdate = new Label();
            Label6 = new Label();
            lblDefinitionDate = new Label();
            Label4 = new Label();
            lblMPSSerial = new Label();
            Label1 = new Label();
            Label3 = new Label();
            txtDescription = new TextBox();
            _txtYear = new NumericUpDown();
            _txtYear.ValueChanged += new EventHandler(txtYear_ValueChanged);
            Label13 = new Label();
            cbCalendar = new ComboBox();
            Label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)_dgMPSList).BeginInit();
            Panel1.SuspendLayout();
            GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_txtYear).BeginInit();
            SuspendLayout();
            // 
            // dgMPSList
            // 
            _dgMPSList.AllowUserToResizeColumns = false;
            _dgMPSList.AllowUserToResizeRows = false;
            DataGridViewCellStyle35.BackColor = Color.White;
            _dgMPSList.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle35;
            _dgMPSList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            _dgMPSList.BackgroundColor = SystemColors.ControlDark;
            DataGridViewCellStyle36.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridViewCellStyle36.BackColor = SystemColors.Control;
            DataGridViewCellStyle36.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle36.ForeColor = SystemColors.WindowText;
            DataGridViewCellStyle36.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle36.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle36.WrapMode = DataGridViewTriState.True;
            _dgMPSList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle36;
            _dgMPSList.ColumnHeadersHeight = 40;
            _dgMPSList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            _dgMPSList.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4, Column5, Column6, Column7, Column8, Column9, Column10, Column11, Column12, Column13, Column14, Column15 });
            _dgMPSList.Location = new Point(5, 101);
            _dgMPSList.Name = "_dgMPSList";
            _dgMPSList.RowHeadersWidth = 23;
            _dgMPSList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            _dgMPSList.RowTemplate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            _dgMPSList.RowTemplate.DefaultCellStyle.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            _dgMPSList.RowTemplate.DefaultCellStyle.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _dgMPSList.RowTemplate.DefaultCellStyle.ForeColor = Color.Black;
            _dgMPSList.ShowCellErrors = false;
            _dgMPSList.ShowCellToolTips = false;
            _dgMPSList.ShowRowErrors = false;
            _dgMPSList.Size = new Size(852, 245);
            _dgMPSList.TabIndex = 2;
            // 
            // Column1
            // 
            DataGridViewCellStyle37.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridViewCellStyle37.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            DataGridViewCellStyle37.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle37.ForeColor = Color.Black;
            DataGridViewCellStyle37.Format = "N0";
            DataGridViewCellStyle37.NullValue = "0";
            Column1.DefaultCellStyle = DataGridViewCellStyle37;
            Column1.HeaderText = "اولویت تولید";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Resizable = DataGridViewTriState.False;
            Column1.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column1.Width = 50;
            // 
            // Column2
            // 
            DataGridViewCellStyle38.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridViewCellStyle38.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            DataGridViewCellStyle38.Font = new Font("Tahoma", 9.0f);
            DataGridViewCellStyle38.ForeColor = Color.Black;
            Column2.DefaultCellStyle = DataGridViewCellStyle38;
            Column2.HeaderText = "کد محصول";
            Column2.Name = "Column2";
            Column2.Resizable = DataGridViewTriState.False;
            Column2.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column2.Width = 75;
            // 
            // Column3
            // 
            DataGridViewCellStyle39.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridViewCellStyle39.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            DataGridViewCellStyle39.Font = new Font("Tahoma", 9.0f);
            DataGridViewCellStyle39.ForeColor = Color.Black;
            Column3.DefaultCellStyle = DataGridViewCellStyle39;
            Column3.HeaderText = "شماره قرارداد";
            Column3.Name = "Column3";
            Column3.Resizable = DataGridViewTriState.False;
            Column3.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column3.Width = 85;
            // 
            // Column4
            // 
            DataGridViewCellStyle40.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridViewCellStyle40.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            DataGridViewCellStyle40.Font = new Font("Tahoma", 9.0f);
            DataGridViewCellStyle40.ForeColor = Color.Black;
            DataGridViewCellStyle40.Format = "N0";
            DataGridViewCellStyle40.NullValue = "0";
            Column4.DefaultCellStyle = DataGridViewCellStyle40;
            Column4.HeaderText = "ماه 1";
            Column4.Name = "Column4";
            Column4.Resizable = DataGridViewTriState.False;
            Column4.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column4.Width = 50;
            // 
            // Column5
            // 
            DataGridViewCellStyle41.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridViewCellStyle41.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            DataGridViewCellStyle41.Font = new Font("Tahoma", 9.0f);
            DataGridViewCellStyle41.ForeColor = Color.Black;
            DataGridViewCellStyle41.Format = "N0";
            DataGridViewCellStyle41.NullValue = "0";
            Column5.DefaultCellStyle = DataGridViewCellStyle41;
            Column5.HeaderText = "ماه 2";
            Column5.Name = "Column5";
            Column5.Resizable = DataGridViewTriState.False;
            Column5.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column5.Width = 50;
            // 
            // Column6
            // 
            DataGridViewCellStyle42.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridViewCellStyle42.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            DataGridViewCellStyle42.Font = new Font("Tahoma", 9.0f);
            DataGridViewCellStyle42.ForeColor = Color.Black;
            DataGridViewCellStyle42.Format = "N0";
            DataGridViewCellStyle42.NullValue = "0";
            Column6.DefaultCellStyle = DataGridViewCellStyle42;
            Column6.HeaderText = "ماه 3";
            Column6.Name = "Column6";
            Column6.Resizable = DataGridViewTriState.False;
            Column6.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column6.Width = 50;
            // 
            // Column7
            // 
            DataGridViewCellStyle43.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridViewCellStyle43.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            DataGridViewCellStyle43.Font = new Font("Tahoma", 9.0f);
            DataGridViewCellStyle43.ForeColor = Color.Black;
            DataGridViewCellStyle43.Format = "N0";
            DataGridViewCellStyle43.NullValue = "0";
            Column7.DefaultCellStyle = DataGridViewCellStyle43;
            Column7.HeaderText = "ماه 4";
            Column7.Name = "Column7";
            Column7.Resizable = DataGridViewTriState.False;
            Column7.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column7.Width = 50;
            // 
            // Column8
            // 
            DataGridViewCellStyle44.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridViewCellStyle44.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            DataGridViewCellStyle44.Font = new Font("Tahoma", 9.0f);
            DataGridViewCellStyle44.ForeColor = Color.Black;
            DataGridViewCellStyle44.Format = "N0";
            DataGridViewCellStyle44.NullValue = "0";
            Column8.DefaultCellStyle = DataGridViewCellStyle44;
            Column8.HeaderText = "ماه 5";
            Column8.Name = "Column8";
            Column8.Resizable = DataGridViewTriState.False;
            Column8.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column8.Width = 50;
            // 
            // Column9
            // 
            DataGridViewCellStyle45.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridViewCellStyle45.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            DataGridViewCellStyle45.Font = new Font("Tahoma", 9.0f);
            DataGridViewCellStyle45.ForeColor = Color.Black;
            DataGridViewCellStyle45.Format = "N0";
            DataGridViewCellStyle45.NullValue = "0";
            Column9.DefaultCellStyle = DataGridViewCellStyle45;
            Column9.HeaderText = "ماه 6";
            Column9.Name = "Column9";
            Column9.Resizable = DataGridViewTriState.False;
            Column9.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column9.Width = 50;
            // 
            // Column10
            // 
            DataGridViewCellStyle46.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridViewCellStyle46.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            DataGridViewCellStyle46.Font = new Font("Tahoma", 9.0f);
            DataGridViewCellStyle46.ForeColor = Color.Black;
            DataGridViewCellStyle46.Format = "N0";
            DataGridViewCellStyle46.NullValue = "0";
            Column10.DefaultCellStyle = DataGridViewCellStyle46;
            Column10.HeaderText = "ماه 7";
            Column10.Name = "Column10";
            Column10.Resizable = DataGridViewTriState.False;
            Column10.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column10.Width = 50;
            // 
            // Column11
            // 
            DataGridViewCellStyle47.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridViewCellStyle47.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            DataGridViewCellStyle47.Font = new Font("Tahoma", 9.0f);
            DataGridViewCellStyle47.ForeColor = Color.Black;
            DataGridViewCellStyle47.Format = "N0";
            DataGridViewCellStyle47.NullValue = "0";
            Column11.DefaultCellStyle = DataGridViewCellStyle47;
            Column11.HeaderText = "ماه 8";
            Column11.Name = "Column11";
            Column11.Resizable = DataGridViewTriState.False;
            Column11.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column11.Width = 50;
            // 
            // Column12
            // 
            DataGridViewCellStyle48.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridViewCellStyle48.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            DataGridViewCellStyle48.Font = new Font("Tahoma", 9.0f);
            DataGridViewCellStyle48.ForeColor = Color.Black;
            DataGridViewCellStyle48.Format = "N0";
            DataGridViewCellStyle48.NullValue = "0";
            Column12.DefaultCellStyle = DataGridViewCellStyle48;
            Column12.HeaderText = "ماه 9";
            Column12.Name = "Column12";
            Column12.Resizable = DataGridViewTriState.False;
            Column12.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column12.Width = 50;
            // 
            // Column13
            // 
            DataGridViewCellStyle49.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridViewCellStyle49.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            DataGridViewCellStyle49.Font = new Font("Tahoma", 9.0f);
            DataGridViewCellStyle49.ForeColor = Color.Black;
            DataGridViewCellStyle49.Format = "N0";
            DataGridViewCellStyle49.NullValue = "0";
            Column13.DefaultCellStyle = DataGridViewCellStyle49;
            Column13.HeaderText = "ماه 10";
            Column13.Name = "Column13";
            Column13.Resizable = DataGridViewTriState.False;
            Column13.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column13.Width = 50;
            // 
            // Column14
            // 
            DataGridViewCellStyle50.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridViewCellStyle50.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            DataGridViewCellStyle50.Font = new Font("Tahoma", 9.0f);
            DataGridViewCellStyle50.ForeColor = Color.Black;
            DataGridViewCellStyle50.Format = "N0";
            DataGridViewCellStyle50.NullValue = "0";
            Column14.DefaultCellStyle = DataGridViewCellStyle50;
            Column14.HeaderText = "ماه 11";
            Column14.Name = "Column14";
            Column14.Resizable = DataGridViewTriState.False;
            Column14.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column14.Width = 50;
            // 
            // Column15
            // 
            DataGridViewCellStyle51.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridViewCellStyle51.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            DataGridViewCellStyle51.Font = new Font("Tahoma", 9.0f);
            DataGridViewCellStyle51.ForeColor = Color.Black;
            DataGridViewCellStyle51.Format = "N0";
            DataGridViewCellStyle51.NullValue = "0";
            Column15.DefaultCellStyle = DataGridViewCellStyle51;
            Column15.HeaderText = "ماه 12";
            Column15.Name = "Column15";
            Column15.Resizable = DataGridViewTriState.False;
            Column15.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column15.Width = 50;
            // 
            // Panel1
            // 
            Panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Panel1.BackColor = SystemColors.Control;
            Panel1.BorderStyle = BorderStyle.FixedSingle;
            Panel1.Controls.Add(_cmdSave);
            Panel1.Controls.Add(_cmdExit);
            Panel1.Controls.Add(_cmdDelete);
            Panel1.Location = new Point(5, 354);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(852, 48);
            Panel1.TabIndex = 331;
            // 
            // cmdSave
            // 
            _cmdSave.BackColor = Color.Transparent;
            _cmdSave.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdSave.ForeColor = Color.Black;
            _cmdSave.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdSave.Location = new Point(488, 10);
            _cmdSave.Name = "_cmdSave";
            _cmdSave.RightToLeft = RightToLeft.No;
            _cmdSave.Size = new Size(90, 28);
            _cmdSave.TabIndex = 3;
            _cmdSave.Text = "ثبت";
            _cmdSave.TextAlign = ContentAlignment.MiddleRight;
            _cmdSave.UseVisualStyleBackColor = false;
            // 
            // cmdExit
            // 
            _cmdExit.BackColor = Color.Transparent;
            _cmdExit.DialogResult = DialogResult.Cancel;
            _cmdExit.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdExit.ForeColor = Color.Black;
            _cmdExit.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdExit.Location = new Point(273, 10);
            _cmdExit.Name = "_cmdExit";
            _cmdExit.RightToLeft = RightToLeft.No;
            _cmdExit.Size = new Size(90, 28);
            _cmdExit.TabIndex = 4;
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
            _cmdDelete.Location = new Point(488, 10);
            _cmdDelete.Name = "_cmdDelete";
            _cmdDelete.RightToLeft = RightToLeft.No;
            _cmdDelete.Size = new Size(90, 28);
            _cmdDelete.TabIndex = 5;
            _cmdDelete.Text = "حذف";
            _cmdDelete.TextAlign = ContentAlignment.MiddleRight;
            _cmdDelete.UseVisualStyleBackColor = false;
            _cmdDelete.Visible = false;
            // 
            // GroupBox1
            // 
            GroupBox1.Controls.Add(cbCalendar);
            GroupBox1.Controls.Add(Label2);
            GroupBox1.Controls.Add(lblLastUpdate);
            GroupBox1.Controls.Add(Label6);
            GroupBox1.Controls.Add(lblDefinitionDate);
            GroupBox1.Controls.Add(Label4);
            GroupBox1.Controls.Add(lblMPSSerial);
            GroupBox1.Controls.Add(Label1);
            GroupBox1.Controls.Add(Label3);
            GroupBox1.Controls.Add(txtDescription);
            GroupBox1.Controls.Add(_txtYear);
            GroupBox1.Controls.Add(Label13);
            GroupBox1.Location = new Point(5, 5);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(852, 90);
            GroupBox1.TabIndex = 332;
            GroupBox1.TabStop = false;
            // 
            // lblLastUpdate
            // 
            lblLastUpdate.BackColor = SystemColors.Control;
            lblLastUpdate.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblLastUpdate.ForeColor = Color.Blue;
            lblLastUpdate.Location = new Point(9, 55);
            lblLastUpdate.Name = "lblLastUpdate";
            lblLastUpdate.RightToLeft = RightToLeft.Yes;
            lblLastUpdate.Size = new Size(80, 17);
            lblLastUpdate.TabIndex = 350;
            lblLastUpdate.Text = "//";
            lblLastUpdate.TextAlign = ContentAlignment.TopCenter;
            // 
            // Label6
            // 
            Label6.BackColor = SystemColors.Control;
            Label6.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label6.Location = new Point(91, 55);
            Label6.Name = "Label6";
            Label6.RightToLeft = RightToLeft.Yes;
            Label6.Size = new Size(100, 17);
            Label6.TabIndex = 349;
            Label6.Text = "آخرین بروز رسانی:";
            // 
            // lblDefinitionDate
            // 
            lblDefinitionDate.BackColor = SystemColors.Control;
            lblDefinitionDate.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblDefinitionDate.ForeColor = Color.Blue;
            lblDefinitionDate.Location = new Point(9, 35);
            lblDefinitionDate.Name = "lblDefinitionDate";
            lblDefinitionDate.RightToLeft = RightToLeft.Yes;
            lblDefinitionDate.Size = new Size(80, 17);
            lblDefinitionDate.TabIndex = 348;
            lblDefinitionDate.Text = "//";
            lblDefinitionDate.TextAlign = ContentAlignment.TopCenter;
            // 
            // Label4
            // 
            Label4.BackColor = SystemColors.Control;
            Label4.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label4.Location = new Point(91, 35);
            Label4.Name = "Label4";
            Label4.RightToLeft = RightToLeft.Yes;
            Label4.Size = new Size(100, 17);
            Label4.TabIndex = 347;
            Label4.Text = "تاریخ تعریف:";
            // 
            // lblMPSSerial
            // 
            lblMPSSerial.BackColor = SystemColors.Control;
            lblMPSSerial.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblMPSSerial.ForeColor = Color.Blue;
            lblMPSSerial.Location = new Point(9, 15);
            lblMPSSerial.Name = "lblMPSSerial";
            lblMPSSerial.RightToLeft = RightToLeft.Yes;
            lblMPSSerial.Size = new Size(80, 17);
            lblMPSSerial.TabIndex = 346;
            lblMPSSerial.Text = "0";
            lblMPSSerial.TextAlign = ContentAlignment.TopCenter;
            // 
            // Label1
            // 
            Label1.BackColor = SystemColors.Control;
            Label1.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label1.Location = new Point(91, 15);
            Label1.Name = "Label1";
            Label1.RightToLeft = RightToLeft.Yes;
            Label1.Size = new Size(100, 17);
            Label1.TabIndex = 345;
            Label1.Text = "سریال MPS:";
            // 
            // Label3
            // 
            Label3.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label3.Location = new Point(655, 18);
            Label3.Name = "Label3";
            Label3.RightToLeft = RightToLeft.Yes;
            Label3.Size = new Size(52, 19);
            Label3.TabIndex = 344;
            Label3.Text = "توضیحات:";
            // 
            // txtDescription
            // 
            txtDescription.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtDescription.Location = new Point(383, 15);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(268, 37);
            txtDescription.TabIndex = 1;
            // 
            // txtYear
            // 
            _txtYear.BorderStyle = BorderStyle.FixedSingle;
            _txtYear.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _txtYear.Location = new Point(737, 16);
            _txtYear.Maximum = new decimal(new int[] { 1500, 0, 0, 0 });
            _txtYear.Minimum = new decimal(new int[] { 1380, 0, 0, 0 });
            _txtYear.Name = "_txtYear";
            _txtYear.Size = new Size(60, 21);
            _txtYear.TabIndex = 0;
            _txtYear.Value = new decimal(new int[] { 1380, 0, 0, 0 });
            // 
            // Label13
            // 
            Label13.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label13.BackColor = SystemColors.Control;
            Label13.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label13.Location = new Point(800, 17);
            Label13.Name = "Label13";
            Label13.RightToLeft = RightToLeft.Yes;
            Label13.Size = new Size(36, 19);
            Label13.TabIndex = 341;
            Label13.Text = "سال:";
            // 
            // cbCalendar
            // 
            cbCalendar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbCalendar.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCalendar.FlatStyle = FlatStyle.Flat;
            cbCalendar.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cbCalendar.FormattingEnabled = true;
            cbCalendar.Location = new Point(383, 60);
            cbCalendar.Name = "cbCalendar";
            cbCalendar.Size = new Size(268, 21);
            cbCalendar.TabIndex = 352;
            // 
            // Label2
            // 
            Label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label2.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label2.ForeColor = Color.Black;
            Label2.Location = new Point(655, 62);
            Label2.Name = "Label2";
            Label2.RightToLeft = RightToLeft.Yes;
            Label2.Size = new Size(181, 19);
            Label2.TabIndex = 351;
            Label2.Text = "تقویم جهت ظرفیت سنجی پرسنل:";
            // 
            // frmMPS
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            CancelButton = _cmdExit;
            ClientSize = new Size(862, 408);
            Controls.Add(GroupBox1);
            Controls.Add(Panel1);
            Controls.Add(_dgMPSList);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmMPS";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " برنامه ریزی سالیانه";
            ((System.ComponentModel.ISupportInitialize)_dgMPSList).EndInit();
            Panel1.ResumeLayout(false);
            GroupBox1.ResumeLayout(false);
            GroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_txtYear).EndInit();
            FormClosing += new FormClosingEventHandler(frmCalender_FormClosing);
            Load += new EventHandler(Form1_Load);
            ResumeLayout(false);
        }

        private DataGridView _dgMPSList;

        internal DataGridView dgMPSList
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _dgMPSList;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_dgMPSList != null)
                {
                    _dgMPSList.KeyDown -= dgMPSList_KeyDown;
                    _dgMPSList.RowsAdded -= dgMPSList_RowsAdded;
                    _dgMPSList.RowsRemoved -= dgMPSList_RowsRemoved;
                }

                _dgMPSList = value;
                if (_dgMPSList != null)
                {
                    _dgMPSList.KeyDown += dgMPSList_KeyDown;
                    _dgMPSList.RowsAdded += dgMPSList_RowsAdded;
                    _dgMPSList.RowsRemoved += dgMPSList_RowsRemoved;
                }
            }
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

        internal GroupBox GroupBox1;
        internal Label lblLastUpdate;
        internal Label Label6;
        internal Label lblDefinitionDate;
        internal Label Label4;
        internal Label lblMPSSerial;
        internal Label Label1;
        internal Label Label3;
        internal TextBox txtDescription;
        private NumericUpDown _txtYear;

        internal NumericUpDown txtYear
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtYear;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtYear != null)
                {
                    _txtYear.ValueChanged -= txtYear_ValueChanged;
                }

                _txtYear = value;
                if (_txtYear != null)
                {
                    _txtYear.ValueChanged += txtYear_ValueChanged;
                }
            }
        }

        internal Label Label13;
        internal DataGridViewTextBoxColumn Column1;
        internal DataGridViewTextBoxColumn Column2;
        internal DataGridViewTextBoxColumn Column3;
        internal DataGridViewTextBoxColumn Column4;
        internal DataGridViewTextBoxColumn Column5;
        internal DataGridViewTextBoxColumn Column6;
        internal DataGridViewTextBoxColumn Column7;
        internal DataGridViewTextBoxColumn Column8;
        internal DataGridViewTextBoxColumn Column9;
        internal DataGridViewTextBoxColumn Column10;
        internal DataGridViewTextBoxColumn Column11;
        internal DataGridViewTextBoxColumn Column12;
        internal DataGridViewTextBoxColumn Column13;
        internal DataGridViewTextBoxColumn Column14;
        internal DataGridViewTextBoxColumn Column15;
        internal ComboBox cbCalendar;
        internal Label Label2;
    }
}