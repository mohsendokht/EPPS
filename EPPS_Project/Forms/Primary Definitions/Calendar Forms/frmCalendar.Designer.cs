using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmCalendar : Form
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this._cmdExit = new System.Windows.Forms.Button();
            this._cmdSave = new System.Windows.Forms.Button();
            this.Panel1 = new System.Windows.Forms.Panel();
            this._cmdCalenderDays = new System.Windows.Forms.Button();
            this._cmdDelete = new System.Windows.Forms.Button();
            this.gbShift = new System.Windows.Forms.GroupBox();
            this.Label1 = new System.Windows.Forms.Label();
            this._cmdShowShiftTimeLine = new System.Windows.Forms.Button();
            this._txtShiftExtraTime = new System.Windows.Forms.DateTimePicker();
            this.Label6 = new System.Windows.Forms.Label();
            this._txtShiftDuration = new System.Windows.Forms.DateTimePicker();
            this.Label5 = new System.Windows.Forms.Label();
            this._txtShiftStart = new System.Windows.Forms.DateTimePicker();
            this.Label4 = new System.Windows.Forms.Label();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this._chkSetWorkTime = new System.Windows.Forms.CheckBox();
            this._chkSetDownTime = new System.Windows.Forms.CheckBox();
            this._cmdSaveShift = new System.Windows.Forms.Button();
            this._dgShiftTimeLine = new System.Windows.Forms.DataGridView();
            this.gb_CalendarHeader = new System.Windows.Forms.GroupBox();
            this.Label2 = new System.Windows.Forms.Label();
            this._txtTitle = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this._txtShiftCount = new System.Windows.Forms.NumericUpDown();
            this._dgShifts = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Panel1.SuspendLayout();
            this.gbShift.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgShiftTimeLine)).BeginInit();
            this.gb_CalendarHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._txtShiftCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgShifts)).BeginInit();
            this.SuspendLayout();
            // 
            // _cmdExit
            // 
            this._cmdExit.BackColor = System.Drawing.Color.Transparent;
            this._cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cmdExit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdExit.ForeColor = System.Drawing.Color.Red;
            this._cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdExit.Location = new System.Drawing.Point(181, 12);
            this._cmdExit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._cmdExit.Name = "_cmdExit";
            this._cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdExit.Size = new System.Drawing.Size(120, 34);
            this._cmdExit.TabIndex = 5;
            this._cmdExit.Text = "خروج";
            this._cmdExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._cmdExit.UseVisualStyleBackColor = false;
            this._cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // _cmdSave
            // 
            this._cmdSave.BackColor = System.Drawing.Color.Transparent;
            this._cmdSave.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdSave.ForeColor = System.Drawing.Color.Black;
            this._cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdSave.Location = new System.Drawing.Point(849, 12);
            this._cmdSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._cmdSave.Name = "_cmdSave";
            this._cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdSave.Size = new System.Drawing.Size(120, 34);
            this._cmdSave.TabIndex = 6;
            this._cmdSave.Text = "ثبت";
            this._cmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._cmdSave.UseVisualStyleBackColor = false;
            this._cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // Panel1
            // 
            this.Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this._cmdCalenderDays);
            this.Panel1.Controls.Add(this._cmdSave);
            this.Panel1.Controls.Add(this._cmdExit);
            this.Panel1.Controls.Add(this._cmdDelete);
            this.Panel1.Location = new System.Drawing.Point(9, 512);
            this.Panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(1254, 59);
            this.Panel1.TabIndex = 22;
            // 
            // _cmdCalenderDays
            // 
            this._cmdCalenderDays.BackColor = System.Drawing.Color.Transparent;
            this._cmdCalenderDays.Enabled = false;
            this._cmdCalenderDays.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdCalenderDays.Location = new System.Drawing.Point(471, 12);
            this._cmdCalenderDays.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._cmdCalenderDays.Name = "_cmdCalenderDays";
            this._cmdCalenderDays.Size = new System.Drawing.Size(252, 34);
            this._cmdCalenderDays.TabIndex = 267;
            this._cmdCalenderDays.Text = "مشخصات روزهای تقویم کاری ...";
            this._cmdCalenderDays.UseVisualStyleBackColor = false;
            this._cmdCalenderDays.Click += new System.EventHandler(this.cmdCalenderDays_Click);
            // 
            // _cmdDelete
            // 
            this._cmdDelete.BackColor = System.Drawing.Color.Transparent;
            this._cmdDelete.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdDelete.ForeColor = System.Drawing.Color.Black;
            this._cmdDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdDelete.Location = new System.Drawing.Point(849, 12);
            this._cmdDelete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._cmdDelete.Name = "_cmdDelete";
            this._cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdDelete.Size = new System.Drawing.Size(120, 34);
            this._cmdDelete.TabIndex = 8;
            this._cmdDelete.Text = "حذف";
            this._cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._cmdDelete.UseVisualStyleBackColor = false;
            this._cmdDelete.Visible = false;
            this._cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // gbShift
            // 
            this.gbShift.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbShift.BackColor = System.Drawing.Color.LightGray;
            this.gbShift.Controls.Add(this.Label1);
            this.gbShift.Controls.Add(this._cmdShowShiftTimeLine);
            this.gbShift.Controls.Add(this._txtShiftExtraTime);
            this.gbShift.Controls.Add(this.Label6);
            this.gbShift.Controls.Add(this._txtShiftDuration);
            this.gbShift.Controls.Add(this.Label5);
            this.gbShift.Controls.Add(this._txtShiftStart);
            this.gbShift.Controls.Add(this.Label4);
            this.gbShift.Controls.Add(this.GroupBox2);
            this.gbShift.Controls.Add(this._dgShiftTimeLine);
            this.gbShift.Enabled = false;
            this.gbShift.Location = new System.Drawing.Point(9, 255);
            this.gbShift.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbShift.Name = "gbShift";
            this.gbShift.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbShift.Size = new System.Drawing.Size(1255, 250);
            this.gbShift.TabIndex = 266;
            this.gbShift.TabStop = false;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(521, 57);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(692, 21);
            this.Label1.TabIndex = 242;
            this.Label1.Text = "طول هر قسمت 5 دقیقه میباشد. عنوان هر قسمت نشان دهنده ساعت پایانی آن قسمت میباشد.";
            // 
            // _cmdShowShiftTimeLine
            // 
            this._cmdShowShiftTimeLine.BackColor = System.Drawing.Color.Transparent;
            this._cmdShowShiftTimeLine.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdShowShiftTimeLine.ForeColor = System.Drawing.Color.Blue;
            this._cmdShowShiftTimeLine.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdShowShiftTimeLine.Location = new System.Drawing.Point(9, 14);
            this._cmdShowShiftTimeLine.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._cmdShowShiftTimeLine.Name = "_cmdShowShiftTimeLine";
            this._cmdShowShiftTimeLine.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdShowShiftTimeLine.Size = new System.Drawing.Size(197, 42);
            this._cmdShowShiftTimeLine.TabIndex = 242;
            this._cmdShowShiftTimeLine.Text = "به روزآوری جدول شیفت";
            this._cmdShowShiftTimeLine.UseVisualStyleBackColor = false;
            this._cmdShowShiftTimeLine.Click += new System.EventHandler(this.cmdShowShiftTimeLine_Click);
            // 
            // _txtShiftExtraTime
            // 
            this._txtShiftExtraTime.CustomFormat = "HH:mm";
            this._txtShiftExtraTime.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._txtShiftExtraTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._txtShiftExtraTime.Location = new System.Drawing.Point(515, 12);
            this._txtShiftExtraTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._txtShiftExtraTime.Name = "_txtShiftExtraTime";
            this._txtShiftExtraTime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._txtShiftExtraTime.RightToLeftLayout = true;
            this._txtShiftExtraTime.ShowUpDown = true;
            this._txtShiftExtraTime.Size = new System.Drawing.Size(76, 27);
            this._txtShiftExtraTime.TabIndex = 246;
            this._txtShiftExtraTime.Value = new System.DateTime(2009, 12, 31, 0, 0, 0, 0);
            this._txtShiftExtraTime.ValueChanged += new System.EventHandler(this.txtShiftStart_ValueChanged);
            // 
            // Label6
            // 
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label6.Location = new System.Drawing.Point(600, 15);
            this.Label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label6.Name = "Label6";
            this.Label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label6.Size = new System.Drawing.Size(104, 25);
            this.Label6.TabIndex = 247;
            this.Label6.Text = "اضافه کاری:";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _txtShiftDuration
            // 
            this._txtShiftDuration.CustomFormat = "HH:mm";
            this._txtShiftDuration.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._txtShiftDuration.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._txtShiftDuration.Location = new System.Drawing.Point(732, 14);
            this._txtShiftDuration.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._txtShiftDuration.Name = "_txtShiftDuration";
            this._txtShiftDuration.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._txtShiftDuration.RightToLeftLayout = true;
            this._txtShiftDuration.ShowUpDown = true;
            this._txtShiftDuration.Size = new System.Drawing.Size(123, 27);
            this._txtShiftDuration.TabIndex = 244;
            this._txtShiftDuration.Value = new System.DateTime(2009, 12, 31, 0, 0, 0, 0);
            this._txtShiftDuration.ValueChanged += new System.EventHandler(this.txtShiftStart_ValueChanged);
            // 
            // Label5
            // 
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label5.Location = new System.Drawing.Point(851, 14);
            this.Label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label5.Name = "Label5";
            this.Label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label5.Size = new System.Drawing.Size(109, 25);
            this.Label5.TabIndex = 245;
            this.Label5.Text = "طول شیفت:";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _txtShiftStart
            // 
            this._txtShiftStart.CustomFormat = "HH:mm";
            this._txtShiftStart.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._txtShiftStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._txtShiftStart.Location = new System.Drawing.Point(1019, 15);
            this._txtShiftStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._txtShiftStart.Name = "_txtShiftStart";
            this._txtShiftStart.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._txtShiftStart.RightToLeftLayout = true;
            this._txtShiftStart.ShowUpDown = true;
            this._txtShiftStart.Size = new System.Drawing.Size(95, 27);
            this._txtShiftStart.TabIndex = 242;
            this._txtShiftStart.Value = new System.DateTime(2009, 12, 31, 0, 0, 0, 0);
            this._txtShiftStart.ValueChanged += new System.EventHandler(this.txtShiftStart_ValueChanged);
            // 
            // Label4
            // 
            this.Label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label4.Location = new System.Drawing.Point(1109, 14);
            this.Label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label4.Name = "Label4";
            this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label4.Size = new System.Drawing.Size(132, 25);
            this.Label4.TabIndex = 243;
            this.Label4.Text = "شروع شیفت:";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox2
            // 
            this.GroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox2.BackColor = System.Drawing.Color.LightGray;
            this.GroupBox2.Controls.Add(this._chkSetWorkTime);
            this.GroupBox2.Controls.Add(this._chkSetDownTime);
            this.GroupBox2.Controls.Add(this._cmdSaveShift);
            this.GroupBox2.Location = new System.Drawing.Point(9, 191);
            this.GroupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GroupBox2.Size = new System.Drawing.Size(1236, 52);
            this.GroupBox2.TabIndex = 241;
            this.GroupBox2.TabStop = false;
            // 
            // _chkSetWorkTime
            // 
            this._chkSetWorkTime.BackColor = System.Drawing.Color.Green;
            this._chkSetWorkTime.Checked = true;
            this._chkSetWorkTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkSetWorkTime.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._chkSetWorkTime.ForeColor = System.Drawing.Color.Black;
            this._chkSetWorkTime.Location = new System.Drawing.Point(1080, 20);
            this._chkSetWorkTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._chkSetWorkTime.Name = "_chkSetWorkTime";
            this._chkSetWorkTime.Size = new System.Drawing.Size(148, 32);
            this._chkSetWorkTime.TabIndex = 241;
            this._chkSetWorkTime.Text = "زمان کار";
            this._chkSetWorkTime.UseVisualStyleBackColor = false;
            this._chkSetWorkTime.CheckedChanged += new System.EventHandler(this.chkSetWorkTime_CheckedChanged);
            // 
            // _chkSetDownTime
            // 
            this._chkSetDownTime.BackColor = System.Drawing.Color.White;
            this._chkSetDownTime.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._chkSetDownTime.ForeColor = System.Drawing.Color.Black;
            this._chkSetDownTime.Location = new System.Drawing.Point(841, 17);
            this._chkSetDownTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._chkSetDownTime.Name = "_chkSetDownTime";
            this._chkSetDownTime.Size = new System.Drawing.Size(172, 32);
            this._chkSetDownTime.TabIndex = 240;
            this._chkSetDownTime.Text = "زمان استراحت";
            this._chkSetDownTime.UseVisualStyleBackColor = false;
            this._chkSetDownTime.CheckedChanged += new System.EventHandler(this.chkSetDownTime_CheckedChanged);
            // 
            // _cmdSaveShift
            // 
            this._cmdSaveShift.BackColor = System.Drawing.Color.Transparent;
            this._cmdSaveShift.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdSaveShift.ForeColor = System.Drawing.Color.Black;
            this._cmdSaveShift.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdSaveShift.Location = new System.Drawing.Point(21, 15);
            this._cmdSaveShift.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._cmdSaveShift.Name = "_cmdSaveShift";
            this._cmdSaveShift.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdSaveShift.Size = new System.Drawing.Size(168, 30);
            this._cmdSaveShift.TabIndex = 0;
            this._cmdSaveShift.Text = "ثبت مشخصات شیفت";
            this._cmdSaveShift.UseVisualStyleBackColor = false;
            this._cmdSaveShift.Click += new System.EventHandler(this.cmdSaveShift_Click);
            // 
            // _dgShiftTimeLine
            // 
            this._dgShiftTimeLine.AllowUserToAddRows = false;
            this._dgShiftTimeLine.AllowUserToDeleteRows = false;
            this._dgShiftTimeLine.AllowUserToResizeColumns = false;
            this._dgShiftTimeLine.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Beige;
            this._dgShiftTimeLine.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this._dgShiftTimeLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dgShiftTimeLine.BackgroundColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._dgShiftTimeLine.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this._dgShiftTimeLine.ColumnHeadersHeight = 30;
            this._dgShiftTimeLine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._dgShiftTimeLine.DefaultCellStyle = dataGridViewCellStyle3;
            this._dgShiftTimeLine.Location = new System.Drawing.Point(9, 80);
            this._dgShiftTimeLine.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._dgShiftTimeLine.Name = "_dgShiftTimeLine";
            this._dgShiftTimeLine.ReadOnly = true;
            this._dgShiftTimeLine.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._dgShiftTimeLine.RowHeadersVisible = false;
            this._dgShiftTimeLine.RowHeadersWidth = 35;
            this._dgShiftTimeLine.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._dgShiftTimeLine.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this._dgShiftTimeLine.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this._dgShiftTimeLine.RowTemplate.Height = 40;
            this._dgShiftTimeLine.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._dgShiftTimeLine.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this._dgShiftTimeLine.Size = new System.Drawing.Size(1236, 103);
            this._dgShiftTimeLine.TabIndex = 2;
            this._dgShiftTimeLine.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgMachineWorkTimes_CellMouseUp);
            // 
            // gb_CalendarHeader
            // 
            this.gb_CalendarHeader.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gb_CalendarHeader.Controls.Add(this._dgShifts);
            this.gb_CalendarHeader.Controls.Add(this._txtShiftCount);
            this.gb_CalendarHeader.Controls.Add(this.Label3);
            this.gb_CalendarHeader.Controls.Add(this.Label2);
            this.gb_CalendarHeader.Controls.Add(this._txtTitle);
            this.gb_CalendarHeader.Location = new System.Drawing.Point(9, 20);
            this.gb_CalendarHeader.Name = "gb_CalendarHeader";
            this.gb_CalendarHeader.Size = new System.Drawing.Size(1252, 227);
            this.gb_CalendarHeader.TabIndex = 267;
            this.gb_CalendarHeader.TabStop = false;
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label2.Location = new System.Drawing.Point(341, 21);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label2.Size = new System.Drawing.Size(136, 21);
            this.Label2.TabIndex = 26;
            this.Label2.Text = "تعداد شیفت کاری:";
            // 
            // _txtTitle
            // 
            this._txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._txtTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._txtTitle.Location = new System.Drawing.Point(543, 22);
            this._txtTitle.Margin = new System.Windows.Forms.Padding(4);
            this._txtTitle.Name = "_txtTitle";
            this._txtTitle.Size = new System.Drawing.Size(598, 24);
            this._txtTitle.TabIndex = 28;
            this._txtTitle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Controls_KeyPress);
            // 
            // Label3
            // 
            this.Label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label3.Location = new System.Drawing.Point(1149, 22);
            this.Label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label3.Name = "Label3";
            this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label3.Size = new System.Drawing.Size(91, 21);
            this.Label3.TabIndex = 27;
            this.Label3.Text = "عنوان تقویم:";
            // 
            // _txtShiftCount
            // 
            this._txtShiftCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._txtShiftCount.BackColor = System.Drawing.Color.White;
            this._txtShiftCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._txtShiftCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._txtShiftCount.Location = new System.Drawing.Point(242, 22);
            this._txtShiftCount.Margin = new System.Windows.Forms.Padding(4);
            this._txtShiftCount.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this._txtShiftCount.Name = "_txtShiftCount";
            this._txtShiftCount.ReadOnly = true;
            this._txtShiftCount.Size = new System.Drawing.Size(75, 20);
            this._txtShiftCount.TabIndex = 45;
            this._txtShiftCount.ValueChanged += new System.EventHandler(this.txtShiftCount_ValueChanged);
            this._txtShiftCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Controls_KeyPress);
            // 
            // _dgShifts
            // 
            this._dgShifts.AllowUserToAddRows = false;
            this._dgShifts.AllowUserToDeleteRows = false;
            this._dgShifts.AllowUserToResizeColumns = false;
            this._dgShifts.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            this._dgShifts.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this._dgShifts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dgShifts.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._dgShifts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this._dgShifts.ColumnHeadersHeight = 30;
            this._dgShifts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this._dgShifts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this._dgShifts.Location = new System.Drawing.Point(242, 67);
            this._dgShifts.Margin = new System.Windows.Forms.Padding(4);
            this._dgShifts.Name = "_dgShifts";
            this._dgShifts.RowHeadersVisible = false;
            this._dgShifts.RowHeadersWidth = 20;
            this._dgShifts.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this._dgShifts.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this._dgShifts.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._dgShifts.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this._dgShifts.RowTemplate.Height = 27;
            this._dgShifts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgShifts.Size = new System.Drawing.Size(1003, 153);
            this._dgShifts.TabIndex = 265;
            this._dgShifts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgShifts_CellClick);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.FillWeight = 360F;
            this.Column1.HeaderText = "شیفت";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 360F;
            this.Column2.HeaderText = "اصلاح شیفت";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.Width = 360;
            // 
            // frmCalendar
            // 
            this.AcceptButton = this._cmdSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cmdExit;
            this.ClientSize = new System.Drawing.Size(1273, 580);
            this.Controls.Add(this.gb_CalendarHeader);
            this.Controls.Add(this.gbShift);
            this.Controls.Add(this.Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Location = new System.Drawing.Point(50, 50);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCalendar";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " مشخصات کلی تقویم کاری";
            this.Activated += new System.EventHandler(this.Form_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCalender_FormClosing);
            this.Load += new System.EventHandler(this.frmCalendar_Load);
            this.Panel1.ResumeLayout(false);
            this.gbShift.ResumeLayout(false);
            this.gbShift.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dgShiftTimeLine)).EndInit();
            this.gb_CalendarHeader.ResumeLayout(false);
            this.gb_CalendarHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._txtShiftCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgShifts)).EndInit();
            this.ResumeLayout(false);

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

        internal System.Windows.Forms.Panel Panel1;

        internal TextBox txtTitle
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtTitle;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtTitle != null)
                {
                    _txtTitle.KeyPress -= Controls_KeyPress;
                }

                _txtTitle = value;
                if (_txtTitle != null)
                {
                    _txtTitle.KeyPress += Controls_KeyPress;
                }
            }
        }

        internal NumericUpDown txtShiftCount
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtShiftCount;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtShiftCount != null)
                {
                    _txtShiftCount.ValueChanged -= txtShiftCount_ValueChanged;
                    _txtShiftCount.KeyPress -= Controls_KeyPress;
                }

                _txtShiftCount = value;
                if (_txtShiftCount != null)
                {
                    _txtShiftCount.ValueChanged += txtShiftCount_ValueChanged;
                    _txtShiftCount.KeyPress += Controls_KeyPress;
                }
            }
        }

        internal DataGridView dgShifts
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _dgShifts;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_dgShifts != null)
                {
                    _dgShifts.CellClick -= dgShifts_CellClick;
                }

                _dgShifts = value;
                if (_dgShifts != null)
                {
                    _dgShifts.CellClick += dgShifts_CellClick;
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

        private System.Windows.Forms.Button _cmdCalenderDays;

        internal System.Windows.Forms.Button cmdCalenderDays
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdCalenderDays;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdCalenderDays != null)
                {
                    _cmdCalenderDays.Click -= cmdCalenderDays_Click;
                }

                _cmdCalenderDays = value;
                if (_cmdCalenderDays != null)
                {
                    _cmdCalenderDays.Click += cmdCalenderDays_Click;
                }
            }
        }

        internal GroupBox gbShift;
        private DataGridView _dgShiftTimeLine;

        internal DataGridView dgShiftTimeLine
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _dgShiftTimeLine;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_dgShiftTimeLine != null)
                {
                    _dgShiftTimeLine.CellMouseUp -= dgMachineWorkTimes_CellMouseUp;
                }

                _dgShiftTimeLine = value;
                if (_dgShiftTimeLine != null)
                {
                    _dgShiftTimeLine.CellMouseUp += dgMachineWorkTimes_CellMouseUp;
                }
            }
        }

        internal GroupBox GroupBox2;
        private System.Windows.Forms.Button _cmdShowShiftTimeLine;

        internal System.Windows.Forms.Button cmdShowShiftTimeLine
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdShowShiftTimeLine;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdShowShiftTimeLine != null)
                {
                    _cmdShowShiftTimeLine.Click -= cmdShowShiftTimeLine_Click;
                }

                _cmdShowShiftTimeLine = value;
                if (_cmdShowShiftTimeLine != null)
                {
                    _cmdShowShiftTimeLine.Click += cmdShowShiftTimeLine_Click;
                }
            }
        }

        private CheckBox _chkSetWorkTime;

        internal CheckBox chkSetWorkTime
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _chkSetWorkTime;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_chkSetWorkTime != null)
                {
                    _chkSetWorkTime.CheckedChanged -= chkSetWorkTime_CheckedChanged;
                }

                _chkSetWorkTime = value;
                if (_chkSetWorkTime != null)
                {
                    _chkSetWorkTime.CheckedChanged += chkSetWorkTime_CheckedChanged;
                }
            }
        }

        private CheckBox _chkSetDownTime;

        internal CheckBox chkSetDownTime
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _chkSetDownTime;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_chkSetDownTime != null)
                {
                    _chkSetDownTime.CheckedChanged -= chkSetDownTime_CheckedChanged;
                }

                _chkSetDownTime = value;
                if (_chkSetDownTime != null)
                {
                    _chkSetDownTime.CheckedChanged += chkSetDownTime_CheckedChanged;
                }
            }
        }

        private System.Windows.Forms.Button _cmdSaveShift;

        internal System.Windows.Forms.Button cmdSaveShift
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdSaveShift;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdSaveShift != null)
                {
                    _cmdSaveShift.Click -= cmdSaveShift_Click;
                }

                _cmdSaveShift = value;
                if (_cmdSaveShift != null)
                {
                    _cmdSaveShift.Click += cmdSaveShift_Click;
                }
            }
        }

        private DateTimePicker _txtShiftStart;

        internal DateTimePicker txtShiftStart
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtShiftStart;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtShiftStart != null)
                {
                    _txtShiftStart.ValueChanged -= txtShiftStart_ValueChanged;
                }

                _txtShiftStart = value;
                if (_txtShiftStart != null)
                {
                    _txtShiftStart.ValueChanged += txtShiftStart_ValueChanged;
                }
            }
        }

        internal Label Label4;
        private DateTimePicker _txtShiftDuration;

        internal DateTimePicker txtShiftDuration
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtShiftDuration;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtShiftDuration != null)
                {
                    _txtShiftDuration.ValueChanged -= txtShiftStart_ValueChanged;
                }

                _txtShiftDuration = value;
                if (_txtShiftDuration != null)
                {
                    _txtShiftDuration.ValueChanged += txtShiftStart_ValueChanged;
                }
            }
        }

        internal Label Label5;
        private DateTimePicker _txtShiftExtraTime;

        internal DateTimePicker txtShiftExtraTime
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtShiftExtraTime;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtShiftExtraTime != null)
                {
                    _txtShiftExtraTime.ValueChanged -= txtShiftStart_ValueChanged;
                }

                _txtShiftExtraTime = value;
                if (_txtShiftExtraTime != null)
                {
                    _txtShiftExtraTime.ValueChanged += txtShiftStart_ValueChanged;
                }
            }
        }

        internal Label Label6;
        internal Label Label1;
        private GroupBox gb_CalendarHeader;
        internal Label Label2;
        private TextBox _txtTitle;
        internal Label Label3;
        private NumericUpDown _txtShiftCount;
        private DataGridView _dgShifts;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewButtonColumn Column2;
    }
}