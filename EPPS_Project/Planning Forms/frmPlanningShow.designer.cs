using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmPlanningShow : Form
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Panel1 = new System.Windows.Forms.Panel();
            this._dgList = new System.Windows.Forms.DataGridView();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this._cmdExportToMSP = new System.Windows.Forms.Button();
            this._cmdExit = new System.Windows.Forms.Button();
            this.gbConditions = new System.Windows.Forms.GroupBox();
            this.cbSubbatch_LookUp = new ProductionPlanning.Controls.UserControl_LookUp();
            this.cbBatch_Lookup = new ProductionPlanning.Controls.UserControl_LookUp();
            this.cbProduct_Lookup = new ProductionPlanning.Controls.UserControl_LookUp();
            this.Machine_Lookup = new ProductionPlanning.Controls.UserControl_LookUp();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.chkHasProduction = new System.Windows.Forms.CheckBox();
            this.chkDoneBatchs = new System.Windows.Forms.CheckBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.chkNoProduction = new System.Windows.Forms.CheckBox();
            this.txtToDate = new PSB_FarsiDateControl.PSB_DateControl();
            this.txtFromDate = new PSB_FarsiDateControl.PSB_DateControl();
            this.cbTreeDetails = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this._cmdShow = new System.Windows.Forms.Button();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgList)).BeginInit();
            this.GroupBox1.SuspendLayout();
            this.gbConditions.SuspendLayout();
            this.Panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Panel1.Controls.Add(this._dgList);
            this.Panel1.Controls.Add(this.GroupBox1);
            this.Panel1.Location = new System.Drawing.Point(7, 239);
            this.Panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(1307, 379);
            this.Panel1.TabIndex = 2;
            // 
            // _dgList
            // 
            this._dgList.AllowUserToAddRows = false;
            this._dgList.AllowUserToDeleteRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            this._dgList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this._dgList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dgList.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._dgList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this._dgList.ColumnHeadersHeight = 50;
            this._dgList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._dgList.DefaultCellStyle = dataGridViewCellStyle8;
            this._dgList.Location = new System.Drawing.Point(7, 9);
            this._dgList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._dgList.MultiSelect = false;
            this._dgList.Name = "_dgList";
            this._dgList.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._dgList.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this._dgList.RowHeadersWidth = 60;
            this._dgList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this._dgList.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this._dgList.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this._dgList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgList.Size = new System.Drawing.Size(1294, 318);
            this._dgList.TabIndex = 1;
            this._dgList.TabStop = false;
            this._dgList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgList_KeyDown);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.Controls.Add(this._cmdExportToMSP);
            this.GroupBox1.Controls.Add(this._cmdExit);
            this.GroupBox1.Location = new System.Drawing.Point(7, 324);
            this.GroupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GroupBox1.Size = new System.Drawing.Size(1292, 48);
            this.GroupBox1.TabIndex = 2;
            this.GroupBox1.TabStop = false;
            // 
            // _cmdExportToMSP
            // 
            this._cmdExportToMSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cmdExportToMSP.BackColor = System.Drawing.Color.Transparent;
            this._cmdExportToMSP.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cmdExportToMSP.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdExportToMSP.Location = new System.Drawing.Point(1142, 12);
            this._cmdExportToMSP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._cmdExportToMSP.Name = "_cmdExportToMSP";
            this._cmdExportToMSP.Size = new System.Drawing.Size(142, 32);
            this._cmdExportToMSP.TabIndex = 11;
            this._cmdExportToMSP.Text = "خروجی به MSP";
            this._cmdExportToMSP.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._cmdExportToMSP.UseVisualStyleBackColor = false;
            this._cmdExportToMSP.Click += new System.EventHandler(this.cmdExportToMSP_Click);
            // 
            // _cmdExit
            // 
            this._cmdExit.BackColor = System.Drawing.Color.Transparent;
            this._cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cmdExit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdExit.Location = new System.Drawing.Point(8, 14);
            this._cmdExit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._cmdExit.Name = "_cmdExit";
            this._cmdExit.Size = new System.Drawing.Size(120, 30);
            this._cmdExit.TabIndex = 10;
            this._cmdExit.Text = "خروج";
            this._cmdExit.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._cmdExit.UseVisualStyleBackColor = false;
            this._cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // gbConditions
            // 
            this.gbConditions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbConditions.Controls.Add(this.cbSubbatch_LookUp);
            this.gbConditions.Controls.Add(this.cbBatch_Lookup);
            this.gbConditions.Controls.Add(this.cbProduct_Lookup);
            this.gbConditions.Controls.Add(this.Machine_Lookup);
            this.gbConditions.Controls.Add(this.Panel4);
            this.gbConditions.Controls.Add(this.txtToDate);
            this.gbConditions.Controls.Add(this.txtFromDate);
            this.gbConditions.Controls.Add(this.cbTreeDetails);
            this.gbConditions.Controls.Add(this.Label2);
            this.gbConditions.Controls.Add(this.Label7);
            this.gbConditions.Controls.Add(this.Label6);
            this.gbConditions.Controls.Add(this.Label4);
            this.gbConditions.Controls.Add(this.Label5);
            this.gbConditions.Controls.Add(this.Label3);
            this.gbConditions.Controls.Add(this.Label1);
            this.gbConditions.Controls.Add(this.chkAll);
            this.gbConditions.Controls.Add(this._cmdShow);
            this.gbConditions.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.gbConditions.Location = new System.Drawing.Point(7, 9);
            this.gbConditions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbConditions.Name = "gbConditions";
            this.gbConditions.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbConditions.Size = new System.Drawing.Size(1307, 222);
            this.gbConditions.TabIndex = 3;
            this.gbConditions.TabStop = false;
            // 
            // cbSubbatch_LookUp
            // 
            this.cbSubbatch_LookUp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cbSubbatch_LookUp.CB_AutoComplete = true;
            this.cbSubbatch_LookUp.CB_AutoDropdown = false;
            this.cbSubbatch_LookUp.CB_ColumnNames = "";
            this.cbSubbatch_LookUp.CB_ColumnWidthDefault = 75;
            this.cbSubbatch_LookUp.CB_ColumnWidths = "75,250";
            this.cbSubbatch_LookUp.CB_DataSource = null;
            this.cbSubbatch_LookUp.CB_DisplayMember = "";
            this.cbSubbatch_LookUp.CB_LinkedColumnIndex = 1;
            this.cbSubbatch_LookUp.CB_SelectedIndex = -1;
            this.cbSubbatch_LookUp.CB_SelectedValue = "";
            this.cbSubbatch_LookUp.CB_SerachFromTitle = null;
            this.cbSubbatch_LookUp.CB_ValueMember = "";
            this.cbSubbatch_LookUp.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSubbatch_LookUp.Location = new System.Drawing.Point(679, 135);
            this.cbSubbatch_LookUp.Name = "cbSubbatch_LookUp";
            this.cbSubbatch_LookUp.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbSubbatch_LookUp.Size = new System.Drawing.Size(531, 38);
            this.cbSubbatch_LookUp.TabIndex = 194;
            // 
            // cbBatch_Lookup
            // 
            this.cbBatch_Lookup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cbBatch_Lookup.CB_AutoComplete = true;
            this.cbBatch_Lookup.CB_AutoDropdown = false;
            this.cbBatch_Lookup.CB_ColumnNames = "";
            this.cbBatch_Lookup.CB_ColumnWidthDefault = 75;
            this.cbBatch_Lookup.CB_ColumnWidths = "75,250";
            this.cbBatch_Lookup.CB_DataSource = null;
            this.cbBatch_Lookup.CB_DisplayMember = "";
            this.cbBatch_Lookup.CB_LinkedColumnIndex = 1;
            this.cbBatch_Lookup.CB_SelectedIndex = -1;
            this.cbBatch_Lookup.CB_SelectedValue = "";
            this.cbBatch_Lookup.CB_SerachFromTitle = null;
            this.cbBatch_Lookup.CB_ValueMember = "";
            this.cbBatch_Lookup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBatch_Lookup.Location = new System.Drawing.Point(678, 94);
            this.cbBatch_Lookup.Name = "cbBatch_Lookup";
            this.cbBatch_Lookup.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbBatch_Lookup.Size = new System.Drawing.Size(531, 38);
            this.cbBatch_Lookup.TabIndex = 193;
           // this.cbBatch_Lookup.CB_SelectedValueChanged += new System.EventHandler(this.cbBatch_Lookup_SelectedValueChanged);
            // 
            // cbProduct_Lookup
            // 
            this.cbProduct_Lookup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cbProduct_Lookup.CB_AutoComplete = true;
            this.cbProduct_Lookup.CB_AutoDropdown = false;
            this.cbProduct_Lookup.CB_ColumnNames = "";
            this.cbProduct_Lookup.CB_ColumnWidthDefault = 75;
            this.cbProduct_Lookup.CB_ColumnWidths = "75,250";
            this.cbProduct_Lookup.CB_DataSource = null;
            this.cbProduct_Lookup.CB_DisplayMember = "";
            this.cbProduct_Lookup.CB_LinkedColumnIndex = 1;
            this.cbProduct_Lookup.CB_SelectedIndex = -1;
            this.cbProduct_Lookup.CB_SelectedValue = "";
            this.cbProduct_Lookup.CB_SerachFromTitle = null;
            this.cbProduct_Lookup.CB_ValueMember = "";
            this.cbProduct_Lookup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProduct_Lookup.Location = new System.Drawing.Point(679, 57);
            this.cbProduct_Lookup.Name = "cbProduct_Lookup";
            this.cbProduct_Lookup.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbProduct_Lookup.Size = new System.Drawing.Size(531, 38);
            this.cbProduct_Lookup.TabIndex = 192;
            this.cbProduct_Lookup.CB_SelectedValueChanged += new System.EventHandler(this.cbProduct_Lookup_SelectedValueChanged);
            this.cbProduct_Lookup.Load += new System.EventHandler(this.cbProduct_Lookup_Load);
            // 
            // Machine_Lookup
            // 
            this.Machine_Lookup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Machine_Lookup.CB_AutoComplete = true;
            this.Machine_Lookup.CB_AutoDropdown = false;
            this.Machine_Lookup.CB_ColumnNames = "";
            this.Machine_Lookup.CB_ColumnWidthDefault = 75;
            this.Machine_Lookup.CB_ColumnWidths = "75,250";
            this.Machine_Lookup.CB_DataSource = null;
            this.Machine_Lookup.CB_DisplayMember = "";
            this.Machine_Lookup.CB_LinkedColumnIndex = 1;
            this.Machine_Lookup.CB_SelectedIndex = -1;
            this.Machine_Lookup.CB_SelectedValue = "";
            this.Machine_Lookup.CB_SerachFromTitle = null;
            this.Machine_Lookup.CB_ValueMember = "";
            this.Machine_Lookup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Machine_Lookup.Location = new System.Drawing.Point(682, 179);
            this.Machine_Lookup.Name = "Machine_Lookup";
            this.Machine_Lookup.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Machine_Lookup.Size = new System.Drawing.Size(531, 38);
            this.Machine_Lookup.TabIndex = 191;
            // 
            // Panel4
            // 
            this.Panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel4.Controls.Add(this.chkHasProduction);
            this.Panel4.Controls.Add(this.chkDoneBatchs);
            this.Panel4.Controls.Add(this.Label8);
            this.Panel4.Controls.Add(this.chkNoProduction);
            this.Panel4.Location = new System.Drawing.Point(149, 179);
            this.Panel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(516, 32);
            this.Panel4.TabIndex = 190;
            // 
            // chkHasProduction
            // 
            this.chkHasProduction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkHasProduction.AutoSize = true;
            this.chkHasProduction.Location = new System.Drawing.Point(108, 6);
            this.chkHasProduction.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkHasProduction.Name = "chkHasProduction";
            this.chkHasProduction.Size = new System.Drawing.Size(99, 21);
            this.chkHasProduction.TabIndex = 192;
            this.chkHasProduction.Text = "در حال تولید";
            this.chkHasProduction.UseVisualStyleBackColor = true;
            // 
            // chkDoneBatchs
            // 
            this.chkDoneBatchs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDoneBatchs.AutoSize = true;
            this.chkDoneBatchs.Location = new System.Drawing.Point(5, 5);
            this.chkDoneBatchs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkDoneBatchs.Name = "chkDoneBatchs";
            this.chkDoneBatchs.Size = new System.Drawing.Size(95, 21);
            this.chkDoneBatchs.TabIndex = 193;
            this.chkDoneBatchs.Text = "بسته شده";
            this.chkDoneBatchs.UseVisualStyleBackColor = true;
            // 
            // Label8
            // 
            this.Label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label8.Location = new System.Drawing.Point(431, 6);
            this.Label8.Name = "Label8";
            this.Label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label8.Size = new System.Drawing.Size(73, 21);
            this.Label8.TabIndex = 177;
            this.Label8.Text = "وضعیت بچ:";
            // 
            // chkNoProduction
            // 
            this.chkNoProduction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkNoProduction.AutoSize = true;
            this.chkNoProduction.Location = new System.Drawing.Point(200, 6);
            this.chkNoProduction.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkNoProduction.Name = "chkNoProduction";
            this.chkNoProduction.Size = new System.Drawing.Size(227, 21);
            this.chkNoProduction.TabIndex = 191;
            this.chkNoProduction.Text = "وارد تولید نشده(برنامه ریزی شده)";
            this.chkNoProduction.UseVisualStyleBackColor = true;
            // 
            // txtToDate
            // 
            this.txtToDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtToDate.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.txtToDate.BackColor = System.Drawing.Color.White;
            this.txtToDate.BackColorDateBox = System.Drawing.Color.White;
            this.txtToDate.DateButtonShow = true;
            this.txtToDate.EnableDateText = true;
            this.txtToDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtToDate.FontMyDate = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtToDate.Location = new System.Drawing.Point(846, 19);
            this.txtToDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtToDate.MinimumSize = new System.Drawing.Size(108, 30);
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.Size = new System.Drawing.Size(122, 30);
            this.txtToDate.TabIndex = 1;
            // 
            // txtFromDate
            // 
            this.txtFromDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFromDate.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.txtFromDate.BackColor = System.Drawing.Color.White;
            this.txtFromDate.BackColorDateBox = System.Drawing.Color.White;
            this.txtFromDate.DateButtonShow = true;
            this.txtFromDate.EnableDateText = true;
            this.txtFromDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtFromDate.FontMyDate = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtFromDate.Location = new System.Drawing.Point(1073, 19);
            this.txtFromDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFromDate.MinimumSize = new System.Drawing.Size(108, 30);
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.Size = new System.Drawing.Size(122, 30);
            this.txtFromDate.TabIndex = 0;
            this.txtFromDate.Load += new System.EventHandler(this.txtFromDate_Load);
            // 
            // cbTreeDetails
            // 
            this.cbTreeDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTreeDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbTreeDetails.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cbTreeDetails.FormattingEnabled = true;
            this.cbTreeDetails.Location = new System.Drawing.Point(365, 61);
            this.cbTreeDetails.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbTreeDetails.Name = "cbTreeDetails";
            this.cbTreeDetails.Size = new System.Drawing.Size(220, 25);
            this.cbTreeDetails.TabIndex = 3;
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label2.Location = new System.Drawing.Point(582, 61);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label2.Size = new System.Drawing.Size(71, 22);
            this.Label2.TabIndex = 189;
            this.Label2.Text = "جزء درخت:";
            // 
            // Label7
            // 
            this.Label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label7.Location = new System.Drawing.Point(962, 23);
            this.Label7.Name = "Label7";
            this.Label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label7.Size = new System.Drawing.Size(51, 22);
            this.Label7.TabIndex = 188;
            this.Label7.Text = "تا تاریخ:";
            // 
            // Label6
            // 
            this.Label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label6.Location = new System.Drawing.Point(1234, 23);
            this.Label6.Name = "Label6";
            this.Label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label6.Size = new System.Drawing.Size(51, 22);
            this.Label6.TabIndex = 186;
            this.Label6.Text = "از تاریخ:";
            // 
            // Label4
            // 
            this.Label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label4.Location = new System.Drawing.Point(1201, 135);
            this.Label4.Name = "Label4";
            this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label4.Size = new System.Drawing.Size(98, 22);
            this.Label4.TabIndex = 174;
            this.Label4.Text = "ساب بچ تولید:";
            // 
            // Label5
            // 
            this.Label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label5.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label5.Location = new System.Drawing.Point(1234, 94);
            this.Label5.Name = "Label5";
            this.Label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label5.Size = new System.Drawing.Size(63, 22);
            this.Label5.TabIndex = 172;
            this.Label5.Text = "بچ تولید:";
            // 
            // Label3
            // 
            this.Label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label3.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label3.Location = new System.Drawing.Point(1237, 185);
            this.Label3.Name = "Label3";
            this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label3.Size = new System.Drawing.Size(53, 22);
            this.Label3.TabIndex = 170;
            this.Label3.Text = "ماشین:";
            // 
            // Label1
            // 
            this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label1.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label1.Location = new System.Drawing.Point(1240, 61);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label1.Size = new System.Drawing.Size(54, 22);
            this.Label1.TabIndex = 166;
            this.Label1.Text = "محصول:";
            // 
            // chkAll
            // 
            this.chkAll.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.chkAll.Location = new System.Drawing.Point(17, 19);
            this.chkAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(107, 30);
            this.chkAll.TabIndex = 8;
            this.chkAll.Text = "نمایش همه";
            this.chkAll.UseVisualStyleBackColor = true;
            // 
            // _cmdShow
            // 
            this._cmdShow.BackColor = System.Drawing.Color.Transparent;
            this._cmdShow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdShow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdShow.Location = new System.Drawing.Point(17, 52);
            this._cmdShow.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._cmdShow.Name = "_cmdShow";
            this._cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdShow.Size = new System.Drawing.Size(107, 33);
            this._cmdShow.TabIndex = 7;
            this._cmdShow.Text = "نمایش";
            this._cmdShow.UseVisualStyleBackColor = false;
            this._cmdShow.Click += new System.EventHandler(this.cmdShow_Click);
            // 
            // frmPlanningShow
            // 
            this.AcceptButton = this._cmdShow;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cmdExit;
            this.ClientSize = new System.Drawing.Size(1321, 631);
            this.Controls.Add(this.gbConditions);
            this.Controls.Add(this.Panel1);
            this.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(876, 583);
            this.Name = "frmPlanningShow";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " نمایش برنامه ریزی انجام شده";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPlanningShow_FormClosing);
            this.Load += new System.EventHandler(this.frmPlanningShow_Load);
            this.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dgList)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            this.gbConditions.ResumeLayout(false);
            this.Panel4.ResumeLayout(false);
            this.Panel4.PerformLayout();
            this.ResumeLayout(false);

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
                    _dgList.KeyDown -= dgList_KeyDown;
                }

                _dgList = value;
                if (_dgList != null)
                {
                    _dgList.KeyDown += dgList_KeyDown;
                }
            }
        }

        internal GroupBox gbConditions;
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

        internal CheckBox chkAll;

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

        internal Label Label1;
        internal Label Label3;
        internal Label Label4;

        //internal ComboBox cbBatch
        //{
        //    [MethodImpl(MethodImplOptions.Synchronized)]
        //    get
        //    {
        //        return _cbBatch;
        //    }

        //    [MethodImpl(MethodImplOptions.Synchronized)]
        //    set
        //    {
        //        if (_cbBatch != null)
        //        {
        //            _cbBatch.SelectedValueChanged -= cbBatch_SelectedValueChanged;
        //            _cbBatch.TextChanged -= cbBatch_TextChanged;
        //        }

        //        _cbBatch = value;
        //        if (_cbBatch != null)
        //        {
        //            _cbBatch.SelectedValueChanged += cbBatch_SelectedValueChanged;
        //            _cbBatch.TextChanged += cbBatch_TextChanged;
        //        }
        //    }
        //}

        internal Label Label5;
        internal Label Label7;
        internal Label Label6;
        internal ComboBox cbTreeDetails;
        internal Label Label2;
        internal GroupBox GroupBox1;
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

        private System.Windows.Forms.Button _cmdExportToMSP;

        internal System.Windows.Forms.Button cmdExportToMSP
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdExportToMSP;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdExportToMSP != null)
                {
                    _cmdExportToMSP.Click -= cmdExportToMSP_Click;
                }

                _cmdExportToMSP = value;
                if (_cmdExportToMSP != null)
                {
                    _cmdExportToMSP.Click += cmdExportToMSP_Click;
                }
            }
        }

        internal PSB_FarsiDateControl.PSB_DateControl txtToDate;
        internal PSB_FarsiDateControl.PSB_DateControl txtFromDate;
        internal System.Windows.Forms.Panel Panel4;
        internal Label Label8;
        internal CheckBox chkHasProduction;
        internal CheckBox chkNoProduction;
        internal CheckBox chkDoneBatchs;
        private Controls.UserControl_LookUp Machine_Lookup;
        private Controls.UserControl_LookUp cbProduct_Lookup;
        private Controls.UserControl_LookUp cbBatch_Lookup;
        private Controls.UserControl_LookUp cbSubbatch_LookUp;
    }
}