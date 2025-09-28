using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmCalendarDays : Form
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
            this.Panel2 = new System.Windows.Forms.Panel();
            this._chkSetWorkTime = new System.Windows.Forms.CheckBox();
            this._chkSetDownTime = new System.Windows.Forms.CheckBox();
            this._cmdShowShiftTimeLine = new System.Windows.Forms.Button();
            this._cmdSaveShift = new System.Windows.Forms.Button();
            this._txtShiftExtraTime = new System.Windows.Forms.DateTimePicker();
            this._txtShiftDuration = new System.Windows.Forms.DateTimePicker();
            this._txtShiftStart = new System.Windows.Forms.DateTimePicker();
            this._dgShiftTimeLine = new System.Windows.Forms.DataGridView();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.pnlDayType = new System.Windows.Forms.Panel();
            this.rbHoliday = new System.Windows.Forms.RadioButton();
            this._rbActive = new System.Windows.Forms.RadioButton();
            this.Label4 = new System.Windows.Forms.Label();
            this.rbParticular = new System.Windows.Forms.RadioButton();
            this._rbDefualt = new System.Windows.Forms.RadioButton();
            this.Label3 = new System.Windows.Forms.Label();
            this._dgShifts = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this._cmdSave = new System.Windows.Forms.Button();
            this._cmdExit = new System.Windows.Forms.Button();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.tcDays = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.Panel3 = new System.Windows.Forms.Panel();
            this._cmdEdit = new System.Windows.Forms.Button();
            this._cmdCalendarShow = new System.Windows.Forms.Button();
            this.txtYear = new System.Windows.Forms.NumericUpDown();
            this.cbMonth = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.lblCalendarTitle = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this._dgCalenderDays = new System.Windows.Forms.DataGridView();
            this.Label12 = new System.Windows.Forms.Label();
            this.Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgShiftTimeLine)).BeginInit();
            this.pnlDayType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgShifts)).BeginInit();
            this.Panel1.SuspendLayout();
            this.tcDays.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.Panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgCalenderDays)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel2
            // 
            this.Panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel2.Controls.Add(this._chkSetWorkTime);
            this.Panel2.Controls.Add(this._chkSetDownTime);
            this.Panel2.Controls.Add(this._cmdShowShiftTimeLine);
            this.Panel2.Controls.Add(this._cmdSaveShift);
            this.Panel2.Controls.Add(this._txtShiftExtraTime);
            this.Panel2.Controls.Add(this._txtShiftDuration);
            this.Panel2.Controls.Add(this._txtShiftStart);
            this.Panel2.Controls.Add(this._dgShiftTimeLine);
            this.Panel2.Controls.Add(this.lblDescription);
            this.Panel2.Controls.Add(this.txtDescription);
            this.Panel2.Controls.Add(this.pnlDayType);
            this.Panel2.Controls.Add(this.Label4);
            this.Panel2.Controls.Add(this.rbParticular);
            this.Panel2.Controls.Add(this._rbDefualt);
            this.Panel2.Controls.Add(this.Label3);
            this.Panel2.Controls.Add(this._dgShifts);
            this.Panel2.Controls.Add(this.Label7);
            this.Panel2.Controls.Add(this.Label6);
            this.Panel2.Controls.Add(this.Label5);
            this.Panel2.Controls.Add(this.Label8);
            this.Panel2.Enabled = false;
            this.Panel2.Location = new System.Drawing.Point(0, 245);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(835, 273);
            this.Panel2.TabIndex = 146;
            // 
            // _chkSetWorkTime
            // 
            this._chkSetWorkTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._chkSetWorkTime.BackColor = System.Drawing.Color.Green;
            this._chkSetWorkTime.Checked = true;
            this._chkSetWorkTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkSetWorkTime.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._chkSetWorkTime.ForeColor = System.Drawing.Color.Black;
            this._chkSetWorkTime.Location = new System.Drawing.Point(234, 178);
            this._chkSetWorkTime.Name = "_chkSetWorkTime";
            this._chkSetWorkTime.Size = new System.Drawing.Size(75, 18);
            this._chkSetWorkTime.TabIndex = 241;
            this._chkSetWorkTime.Text = "زمان کار";
            this._chkSetWorkTime.UseVisualStyleBackColor = false;
            this._chkSetWorkTime.CheckedChanged += new System.EventHandler(this.chkSetWorkTime_CheckedChanged);
            // 
            // _chkSetDownTime
            // 
            this._chkSetDownTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._chkSetDownTime.BackColor = System.Drawing.Color.White;
            this._chkSetDownTime.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._chkSetDownTime.ForeColor = System.Drawing.Color.Black;
            this._chkSetDownTime.Location = new System.Drawing.Point(138, 178);
            this._chkSetDownTime.Name = "_chkSetDownTime";
            this._chkSetDownTime.Size = new System.Drawing.Size(92, 18);
            this._chkSetDownTime.TabIndex = 240;
            this._chkSetDownTime.Text = "زمان استراحت";
            this._chkSetDownTime.UseVisualStyleBackColor = false;
            this._chkSetDownTime.CheckedChanged += new System.EventHandler(this.chkSetDownTime_CheckedChanged);
            // 
            // _cmdShowShiftTimeLine
            // 
            this._cmdShowShiftTimeLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._cmdShowShiftTimeLine.BackColor = System.Drawing.Color.Transparent;
            this._cmdShowShiftTimeLine.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdShowShiftTimeLine.ForeColor = System.Drawing.Color.Blue;
            this._cmdShowShiftTimeLine.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdShowShiftTimeLine.Location = new System.Drawing.Point(315, 176);
            this._cmdShowShiftTimeLine.Name = "_cmdShowShiftTimeLine";
            this._cmdShowShiftTimeLine.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdShowShiftTimeLine.Size = new System.Drawing.Size(126, 22);
            this._cmdShowShiftTimeLine.TabIndex = 242;
            this._cmdShowShiftTimeLine.Text = "به روزآوری جدول شیفت";
            this._cmdShowShiftTimeLine.UseVisualStyleBackColor = false;
            this._cmdShowShiftTimeLine.Click += new System.EventHandler(this.cmdShowShiftTimeLine_Click);
            // 
            // _cmdSaveShift
            // 
            this._cmdSaveShift.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._cmdSaveShift.BackColor = System.Drawing.Color.Transparent;
            this._cmdSaveShift.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdSaveShift.ForeColor = System.Drawing.Color.Black;
            this._cmdSaveShift.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdSaveShift.Location = new System.Drawing.Point(6, 176);
            this._cmdSaveShift.Name = "_cmdSaveShift";
            this._cmdSaveShift.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdSaveShift.Size = new System.Drawing.Size(126, 22);
            this._cmdSaveShift.TabIndex = 0;
            this._cmdSaveShift.Text = "ثبت مشخصات شیفت";
            this._cmdSaveShift.UseVisualStyleBackColor = false;
            this._cmdSaveShift.Click += new System.EventHandler(this.cmdSaveShift_Click);
            // 
            // _txtShiftExtraTime
            // 
            this._txtShiftExtraTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._txtShiftExtraTime.CustomFormat = "HH:mm";
            this._txtShiftExtraTime.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._txtShiftExtraTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._txtShiftExtraTime.Location = new System.Drawing.Point(447, 177);
            this._txtShiftExtraTime.Name = "_txtShiftExtraTime";
            this._txtShiftExtraTime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._txtShiftExtraTime.RightToLeftLayout = true;
            this._txtShiftExtraTime.ShowUpDown = true;
            this._txtShiftExtraTime.Size = new System.Drawing.Size(58, 21);
            this._txtShiftExtraTime.TabIndex = 246;
            this._txtShiftExtraTime.Value = new System.DateTime(2009, 12, 31, 0, 0, 0, 0);
            this._txtShiftExtraTime.ValueChanged += new System.EventHandler(this.txtShiftStart_ValueChanged);
            // 
            // _txtShiftDuration
            // 
            this._txtShiftDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._txtShiftDuration.CustomFormat = "HH:mm";
            this._txtShiftDuration.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._txtShiftDuration.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._txtShiftDuration.Location = new System.Drawing.Point(572, 177);
            this._txtShiftDuration.Name = "_txtShiftDuration";
            this._txtShiftDuration.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._txtShiftDuration.RightToLeftLayout = true;
            this._txtShiftDuration.ShowUpDown = true;
            this._txtShiftDuration.Size = new System.Drawing.Size(58, 21);
            this._txtShiftDuration.TabIndex = 244;
            this._txtShiftDuration.Value = new System.DateTime(2009, 12, 31, 0, 0, 0, 0);
            this._txtShiftDuration.ValueChanged += new System.EventHandler(this.txtShiftStart_ValueChanged);
            // 
            // _txtShiftStart
            // 
            this._txtShiftStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._txtShiftStart.CustomFormat = "HH:mm";
            this._txtShiftStart.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._txtShiftStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._txtShiftStart.Location = new System.Drawing.Point(699, 177);
            this._txtShiftStart.Name = "_txtShiftStart";
            this._txtShiftStart.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._txtShiftStart.RightToLeftLayout = true;
            this._txtShiftStart.ShowUpDown = true;
            this._txtShiftStart.Size = new System.Drawing.Size(58, 21);
            this._txtShiftStart.TabIndex = 242;
            this._txtShiftStart.Value = new System.DateTime(2009, 12, 31, 0, 0, 0, 0);
            this._txtShiftStart.ValueChanged += new System.EventHandler(this.txtShiftStart_ValueChanged);
            // 
            // _dgShiftTimeLine
            // 
            this._dgShiftTimeLine.AllowUserToAddRows = false;
            this._dgShiftTimeLine.AllowUserToDeleteRows = false;
            this._dgShiftTimeLine.AllowUserToResizeColumns = false;
            this._dgShiftTimeLine.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Beige;
            this._dgShiftTimeLine.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this._dgShiftTimeLine.BackgroundColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._dgShiftTimeLine.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this._dgShiftTimeLine.ColumnHeadersHeight = 25;
            this._dgShiftTimeLine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._dgShiftTimeLine.DefaultCellStyle = dataGridViewCellStyle3;
            this._dgShiftTimeLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._dgShiftTimeLine.Location = new System.Drawing.Point(0, 202);
            this._dgShiftTimeLine.Name = "_dgShiftTimeLine";
            this._dgShiftTimeLine.ReadOnly = true;
            this._dgShiftTimeLine.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._dgShiftTimeLine.RowHeadersVisible = false;
            this._dgShiftTimeLine.RowHeadersWidth = 15;
            this._dgShiftTimeLine.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._dgShiftTimeLine.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this._dgShiftTimeLine.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this._dgShiftTimeLine.RowTemplate.Height = 25;
            this._dgShiftTimeLine.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._dgShiftTimeLine.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this._dgShiftTimeLine.Size = new System.Drawing.Size(833, 69);
            this._dgShiftTimeLine.TabIndex = 2;
            this._dgShiftTimeLine.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgShiftTimeLine_CellMouseUp);
            // 
            // lblDescription
            // 
            this.lblDescription.BackColor = System.Drawing.Color.Beige;
            this.lblDescription.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblDescription.ForeColor = System.Drawing.Color.Black;
            this.lblDescription.Location = new System.Drawing.Point(282, 6);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblDescription.Size = new System.Drawing.Size(51, 19);
            this.lblDescription.TabIndex = 278;
            this.lblDescription.Text = "توضیحات";
            this.lblDescription.Visible = false;
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtDescription.Location = new System.Drawing.Point(7, 5);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(271, 41);
            this.txtDescription.TabIndex = 277;
            this.txtDescription.Visible = false;
            // 
            // pnlDayType
            // 
            this.pnlDayType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDayType.BackColor = System.Drawing.Color.Beige;
            this.pnlDayType.Controls.Add(this.rbHoliday);
            this.pnlDayType.Controls.Add(this._rbActive);
            this.pnlDayType.Enabled = false;
            this.pnlDayType.Location = new System.Drawing.Point(552, 23);
            this.pnlDayType.Name = "pnlDayType";
            this.pnlDayType.Size = new System.Drawing.Size(179, 23);
            this.pnlDayType.TabIndex = 276;
            // 
            // rbHoliday
            // 
            this.rbHoliday.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.rbHoliday.AutoSize = true;
            this.rbHoliday.BackColor = System.Drawing.Color.Beige;
            this.rbHoliday.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.rbHoliday.Location = new System.Drawing.Point(9, 3);
            this.rbHoliday.Name = "rbHoliday";
            this.rbHoliday.Size = new System.Drawing.Size(54, 17);
            this.rbHoliday.TabIndex = 277;
            this.rbHoliday.TabStop = true;
            this.rbHoliday.Text = "تعطیل";
            this.rbHoliday.UseVisualStyleBackColor = false;
            // 
            // _rbActive
            // 
            this._rbActive.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this._rbActive.AutoSize = true;
            this._rbActive.BackColor = System.Drawing.Color.Beige;
            this._rbActive.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._rbActive.Location = new System.Drawing.Point(127, 3);
            this._rbActive.Name = "_rbActive";
            this._rbActive.Size = new System.Drawing.Size(47, 17);
            this._rbActive.TabIndex = 276;
            this._rbActive.TabStop = true;
            this._rbActive.Text = "کاری";
            this._rbActive.UseVisualStyleBackColor = false;
            this._rbActive.CheckedChanged += new System.EventHandler(this.rbActive_CheckedChanged);
            // 
            // Label4
            // 
            this.Label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label4.BackColor = System.Drawing.Color.Beige;
            this.Label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label4.ForeColor = System.Drawing.Color.Black;
            this.Label4.Location = new System.Drawing.Point(730, 26);
            this.Label4.Name = "Label4";
            this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label4.Size = new System.Drawing.Size(44, 17);
            this.Label4.TabIndex = 273;
            this.Label4.Text = "نوع روز:";
            // 
            // rbParticular
            // 
            this.rbParticular.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbParticular.AutoSize = true;
            this.rbParticular.BackColor = System.Drawing.Color.Beige;
            this.rbParticular.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.rbParticular.Location = new System.Drawing.Point(541, 4);
            this.rbParticular.Name = "rbParticular";
            this.rbParticular.Size = new System.Drawing.Size(74, 17);
            this.rbParticular.TabIndex = 272;
            this.rbParticular.TabStop = true;
            this.rbParticular.Text = "حالت خاص";
            this.rbParticular.UseVisualStyleBackColor = false;
            // 
            // _rbDefualt
            // 
            this._rbDefualt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._rbDefualt.AutoSize = true;
            this._rbDefualt.BackColor = System.Drawing.Color.Beige;
            this._rbDefualt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._rbDefualt.Location = new System.Drawing.Point(628, 4);
            this._rbDefualt.Name = "_rbDefualt";
            this._rbDefualt.Size = new System.Drawing.Size(98, 17);
            this._rbDefualt.TabIndex = 271;
            this._rbDefualt.TabStop = true;
            this._rbDefualt.Text = "حالت پیش فرض";
            this._rbDefualt.UseVisualStyleBackColor = false;
            this._rbDefualt.CheckedChanged += new System.EventHandler(this.rbDefualt_CheckedChanged);
            // 
            // Label3
            // 
            this.Label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label3.BackColor = System.Drawing.Color.Beige;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label3.ForeColor = System.Drawing.Color.Black;
            this.Label3.Location = new System.Drawing.Point(730, 4);
            this.Label3.Name = "Label3";
            this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label3.Size = new System.Drawing.Size(99, 17);
            this.Label3.TabIndex = 270;
            this.Label3.Text = "زمانبندی روز کاری:";
            // 
            // _dgShifts
            // 
            this._dgShifts.AllowUserToAddRows = false;
            this._dgShifts.AllowUserToDeleteRows = false;
            this._dgShifts.AllowUserToResizeColumns = false;
            this._dgShifts.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            this._dgShifts.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this._dgShifts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dgShifts.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this._dgShifts.ColumnHeadersHeight = 24;
            this._dgShifts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this._dgShifts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this._dgShifts.Location = new System.Drawing.Point(0, 50);
            this._dgShifts.Name = "_dgShifts";
            this._dgShifts.RowHeadersVisible = false;
            this._dgShifts.RowHeadersWidth = 10;
            this._dgShifts.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this._dgShifts.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this._dgShifts.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._dgShifts.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this._dgShifts.RowTemplate.Height = 24;
            this._dgShifts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgShifts.Size = new System.Drawing.Size(833, 119);
            this._dgShifts.TabIndex = 266;
            this._dgShifts.Tag = "0";
            this._dgShifts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgShifts_CellClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "شیفت";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column1.Width = 60;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "مشخصات شیفت";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.Beige;
            this.Label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label7.ForeColor = System.Drawing.Color.Black;
            this.Label7.Location = new System.Drawing.Point(0, 0);
            this.Label7.Name = "Label7";
            this.Label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label7.Size = new System.Drawing.Size(833, 49);
            this.Label7.TabIndex = 141;
            // 
            // Label6
            // 
            this.Label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label6.Location = new System.Drawing.Point(504, 177);
            this.Label6.Name = "Label6";
            this.Label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label6.Size = new System.Drawing.Size(63, 20);
            this.Label6.TabIndex = 247;
            this.Label6.Text = "اضافه کاری:";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label5
            // 
            this.Label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label5.Location = new System.Drawing.Point(629, 177);
            this.Label5.Name = "Label5";
            this.Label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label5.Size = new System.Drawing.Size(65, 20);
            this.Label5.TabIndex = 245;
            this.Label5.Text = "طول شیفت:";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label8
            // 
            this.Label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label8.Location = new System.Drawing.Point(756, 177);
            this.Label8.Name = "Label8";
            this.Label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label8.Size = new System.Drawing.Size(74, 20);
            this.Label8.TabIndex = 243;
            this.Label8.Text = "شروع شیفت:";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _cmdSave
            // 
            this._cmdSave.BackColor = System.Drawing.Color.Transparent;
            this._cmdSave.Enabled = false;
            this._cmdSave.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdSave.ForeColor = System.Drawing.Color.Black;
            this._cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdSave.Location = new System.Drawing.Point(431, 6);
            this._cmdSave.Name = "_cmdSave";
            this._cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdSave.Size = new System.Drawing.Size(90, 28);
            this._cmdSave.TabIndex = 6;
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
            this._cmdExit.Location = new System.Drawing.Point(312, 6);
            this._cmdExit.Name = "_cmdExit";
            this._cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdExit.Size = new System.Drawing.Size(90, 28);
            this._cmdExit.TabIndex = 5;
            this._cmdExit.Text = "بازگشت";
            this._cmdExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._cmdExit.UseVisualStyleBackColor = false;
            this._cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this._cmdSave);
            this.Panel1.Controls.Add(this._cmdExit);
            this.Panel1.Location = new System.Drawing.Point(4, 523);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(827, 43);
            this.Panel1.TabIndex = 46;
            // 
            // tcDays
            // 
            this.tcDays.Controls.Add(this.TabPage1);
            this.tcDays.Dock = System.Windows.Forms.DockStyle.Top;
            this.tcDays.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.tcDays.HotTrack = true;
            this.tcDays.ItemSize = new System.Drawing.Size(147, 18);
            this.tcDays.Location = new System.Drawing.Point(0, 0);
            this.tcDays.Name = "tcDays";
            this.tcDays.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tcDays.RightToLeftLayout = true;
            this.tcDays.SelectedIndex = 0;
            this.tcDays.Size = new System.Drawing.Size(835, 245);
            this.tcDays.TabIndex = 152;
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.TabPage1.Controls.Add(this.Panel3);
            this.TabPage1.Location = new System.Drawing.Point(4, 22);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TabPage1.Size = new System.Drawing.Size(827, 219);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "مشخصات روزهای تقویم";
            // 
            // Panel3
            // 
            this.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel3.Controls.Add(this._cmdEdit);
            this.Panel3.Controls.Add(this._cmdCalendarShow);
            this.Panel3.Controls.Add(this.txtYear);
            this.Panel3.Controls.Add(this.cbMonth);
            this.Panel3.Controls.Add(this.Label1);
            this.Panel3.Controls.Add(this.lblCalendarTitle);
            this.Panel3.Controls.Add(this.Label9);
            this.Panel3.Controls.Add(this.Label2);
            this.Panel3.Controls.Add(this._dgCalenderDays);
            this.Panel3.Controls.Add(this.Label12);
            this.Panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel3.Location = new System.Drawing.Point(3, 3);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(821, 213);
            this.Panel3.TabIndex = 146;
            // 
            // _cmdEdit
            // 
            this._cmdEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._cmdEdit.BackColor = System.Drawing.Color.Transparent;
            this._cmdEdit.Enabled = false;
            this._cmdEdit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdEdit.ForeColor = System.Drawing.Color.Black;
            this._cmdEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdEdit.Location = new System.Drawing.Point(17, 173);
            this._cmdEdit.Name = "_cmdEdit";
            this._cmdEdit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdEdit.Size = new System.Drawing.Size(136, 27);
            this._cmdEdit.TabIndex = 276;
            this._cmdEdit.Text = "اصلاح مشخصات روز";
            this._cmdEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._cmdEdit.UseVisualStyleBackColor = false;
            this._cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // _cmdCalendarShow
            // 
            this._cmdCalendarShow.BackColor = System.Drawing.Color.Transparent;
            this._cmdCalendarShow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdCalendarShow.ForeColor = System.Drawing.Color.Black;
            this._cmdCalendarShow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdCalendarShow.Location = new System.Drawing.Point(17, 5);
            this._cmdCalendarShow.Name = "_cmdCalendarShow";
            this._cmdCalendarShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdCalendarShow.Size = new System.Drawing.Size(95, 28);
            this._cmdCalendarShow.TabIndex = 275;
            this._cmdCalendarShow.Text = "نمایش تقویم";
            this._cmdCalendarShow.UseVisualStyleBackColor = false;
            this._cmdCalendarShow.Click += new System.EventHandler(this.cmdCalendarShow_Click);
            // 
            // txtYear
            // 
            this.txtYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtYear.Location = new System.Drawing.Point(663, 2);
            this.txtYear.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.txtYear.Minimum = new decimal(new int[] {
            1300,
            0,
            0,
            0});
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(58, 21);
            this.txtYear.TabIndex = 274;
            this.txtYear.Value = new decimal(new int[] {
            1380,
            0,
            0,
            0});
            // 
            // cbMonth
            // 
            this.cbMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMonth.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cbMonth.FormattingEnabled = true;
            this.cbMonth.Items.AddRange(new object[] {
            "فروردین",
            "اردیبهشت",
            "خرداد",
            "تیر",
            "مرداد",
            "شهریور",
            "مهر",
            "آبان",
            "آذر",
            "دی",
            "بهمن",
            "اسفند"});
            this.cbMonth.Location = new System.Drawing.Point(499, 2);
            this.cbMonth.Name = "cbMonth";
            this.cbMonth.Size = new System.Drawing.Size(103, 21);
            this.cbMonth.TabIndex = 273;
            // 
            // Label1
            // 
            this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label1.BackColor = System.Drawing.Color.Beige;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label1.ForeColor = System.Drawing.Color.Black;
            this.Label1.Location = new System.Drawing.Point(604, 4);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label1.Size = new System.Drawing.Size(46, 17);
            this.Label1.TabIndex = 272;
            this.Label1.Text = "نام ماه:";
            // 
            // lblCalendarTitle
            // 
            this.lblCalendarTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCalendarTitle.BackColor = System.Drawing.Color.Beige;
            this.lblCalendarTitle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblCalendarTitle.ForeColor = System.Drawing.Color.Blue;
            this.lblCalendarTitle.Location = new System.Drawing.Point(274, 28);
            this.lblCalendarTitle.Name = "lblCalendarTitle";
            this.lblCalendarTitle.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCalendarTitle.Size = new System.Drawing.Size(450, 17);
            this.lblCalendarTitle.TabIndex = 271;
            this.lblCalendarTitle.Text = "#";
            // 
            // Label9
            // 
            this.Label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label9.BackColor = System.Drawing.Color.Beige;
            this.Label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label9.ForeColor = System.Drawing.Color.Black;
            this.Label9.Location = new System.Drawing.Point(723, 4);
            this.Label9.Name = "Label9";
            this.Label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label9.Size = new System.Drawing.Size(65, 17);
            this.Label9.TabIndex = 269;
            this.Label9.Text = "سال تقویم:";
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label2.BackColor = System.Drawing.Color.Beige;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label2.ForeColor = System.Drawing.Color.Black;
            this.Label2.Location = new System.Drawing.Point(723, 28);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label2.Size = new System.Drawing.Size(68, 17);
            this.Label2.TabIndex = 268;
            this.Label2.Text = "عنوان تقویم:";
            // 
            // _dgCalenderDays
            // 
            this._dgCalenderDays.AllowUserToAddRows = false;
            this._dgCalenderDays.AllowUserToDeleteRows = false;
            this._dgCalenderDays.AllowUserToResizeColumns = false;
            this._dgCalenderDays.AllowUserToResizeRows = false;
            this._dgCalenderDays.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this._dgCalenderDays.ColumnHeadersHeight = 30;
            this._dgCalenderDays.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this._dgCalenderDays.ColumnHeadersVisible = false;
            this._dgCalenderDays.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._dgCalenderDays.EnableHeadersVisualStyles = false;
            this._dgCalenderDays.Location = new System.Drawing.Point(0, 51);
            this._dgCalenderDays.MultiSelect = false;
            this._dgCalenderDays.Name = "_dgCalenderDays";
            this._dgCalenderDays.ReadOnly = true;
            this._dgCalenderDays.RowHeadersWidth = 110;
            this._dgCalenderDays.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this._dgCalenderDays.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this._dgCalenderDays.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this._dgCalenderDays.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._dgCalenderDays.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this._dgCalenderDays.ShowCellErrors = false;
            this._dgCalenderDays.ShowCellToolTips = false;
            this._dgCalenderDays.ShowEditingIcon = false;
            this._dgCalenderDays.ShowRowErrors = false;
            this._dgCalenderDays.Size = new System.Drawing.Size(819, 160);
            this._dgCalenderDays.TabIndex = 267;
            this._dgCalenderDays.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgCalenderDays_CellMouseClick);
            this._dgCalenderDays.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgCalenderDays_RowHeaderMouseClick);
            // 
            // Label12
            // 
            this.Label12.BackColor = System.Drawing.Color.Beige;
            this.Label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label12.ForeColor = System.Drawing.Color.Black;
            this.Label12.Location = new System.Drawing.Point(0, 0);
            this.Label12.Name = "Label12";
            this.Label12.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label12.Size = new System.Drawing.Size(819, 49);
            this.Label12.TabIndex = 141;
            // 
            // frmCalendarDays
            // 
            this.AcceptButton = this._cmdSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cmdExit;
            this.ClientSize = new System.Drawing.Size(835, 570);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.tcDays);
            this.Controls.Add(this.Panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Location = new System.Drawing.Point(50, 50);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCalendarDays";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " مشخصات روزهای تقویم کاری";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCalenderDays_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgShiftTimeLine)).EndInit();
            this.pnlDayType.ResumeLayout(false);
            this.pnlDayType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgShifts)).EndInit();
            this.Panel1.ResumeLayout(false);
            this.tcDays.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.Panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgCalenderDays)).EndInit();
            this.ResumeLayout(false);

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

        internal System.Windows.Forms.Panel Panel1;
        internal TabControl tcDays;
        internal TabPage TabPage1;
        internal System.Windows.Forms.Panel Panel3;
        internal Label Label12;
        private DataGridView _dgCalenderDays;

        internal DataGridView dgCalenderDays
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _dgCalenderDays;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_dgCalenderDays != null)
                {
                    _dgCalenderDays.RowHeaderMouseClick -= dgCalenderDays_RowHeaderMouseClick;
                    _dgCalenderDays.CellMouseClick -= dgCalenderDays_CellMouseClick;
                }

                _dgCalenderDays = value;
                if (_dgCalenderDays != null)
                {
                    _dgCalenderDays.RowHeaderMouseClick += dgCalenderDays_RowHeaderMouseClick;
                    _dgCalenderDays.CellMouseClick += dgCalenderDays_CellMouseClick;
                }
            }
        }

        internal Label lblCalendarTitle;
        internal Label Label9;
        internal Label Label2;
        internal Label Label1;
        internal ComboBox cbMonth;
        internal NumericUpDown txtYear;
        private System.Windows.Forms.Button _cmdCalendarShow;

        internal System.Windows.Forms.Button cmdCalendarShow
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdCalendarShow;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdCalendarShow != null)
                {
                    _cmdCalendarShow.Click -= cmdCalendarShow_Click;
                }

                _cmdCalendarShow = value;
                if (_cmdCalendarShow != null)
                {
                    _cmdCalendarShow.Click += cmdCalendarShow_Click;
                }
            }
        }

        internal System.Windows.Forms.Panel Panel2;
        internal Label Label7;
        private DataGridView _dgShifts;

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

        private RadioButton _rbDefualt;

        internal RadioButton rbDefualt
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _rbDefualt;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_rbDefualt != null)
                {
                    _rbDefualt.CheckedChanged -= rbDefualt_CheckedChanged;
                }

                _rbDefualt = value;
                if (_rbDefualt != null)
                {
                    _rbDefualt.CheckedChanged += rbDefualt_CheckedChanged;
                }
            }
        }

        internal Label Label3;
        internal RadioButton rbParticular;
        internal Label Label4;
        internal System.Windows.Forms.Panel pnlDayType;
        internal RadioButton rbHoliday;
        private RadioButton _rbActive;

        internal RadioButton rbActive
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _rbActive;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_rbActive != null)
                {
                    _rbActive.CheckedChanged -= rbActive_CheckedChanged;
                }

                _rbActive = value;
                if (_rbActive != null)
                {
                    _rbActive.CheckedChanged += rbActive_CheckedChanged;
                }
            }
        }

        private System.Windows.Forms.Button _cmdEdit;

        internal System.Windows.Forms.Button cmdEdit
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdEdit;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdEdit != null)
                {
                    _cmdEdit.Click -= cmdEdit_Click;
                }

                _cmdEdit = value;
                if (_cmdEdit != null)
                {
                    _cmdEdit.Click += cmdEdit_Click;
                }
            }
        }

        internal TextBox txtDescription;
        internal Label lblDescription;
        internal DataGridViewTextBoxColumn Column1;
        internal DataGridViewButtonColumn Column2;
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

        internal Label Label8;
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
                    _dgShiftTimeLine.CellMouseUp -= dgShiftTimeLine_CellMouseUp;
                }

                _dgShiftTimeLine = value;
                if (_dgShiftTimeLine != null)
                {
                    _dgShiftTimeLine.CellMouseUp += dgShiftTimeLine_CellMouseUp;
                }
            }
        }
    }
}