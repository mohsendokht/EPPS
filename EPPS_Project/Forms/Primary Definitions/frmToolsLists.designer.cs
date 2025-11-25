using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmToolsLists : Form
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
            this.Panel1 = new System.Windows.Forms.Panel();
            this._cbFilter8 = new System.Windows.Forms.ComboBox();
            this._cbFilter7 = new System.Windows.Forms.ComboBox();
            this._cbFilter6 = new System.Windows.Forms.ComboBox();
            this._cbFilter5 = new System.Windows.Forms.ComboBox();
            this._cbFilter4 = new System.Windows.Forms.ComboBox();
            this._cbFilter3 = new System.Windows.Forms.ComboBox();
            this._cbFilter2 = new System.Windows.Forms.ComboBox();
            this._cbFilter1 = new System.Windows.Forms.ComboBox();
            this._txtSearch8 = new System.Windows.Forms.TextBox();
            this._txtSearch7 = new System.Windows.Forms.TextBox();
            this._txtSearch6 = new System.Windows.Forms.TextBox();
            this._txtSearch2 = new System.Windows.Forms.TextBox();
            this._txtSearch3 = new System.Windows.Forms.TextBox();
            this._txtSearch4 = new System.Windows.Forms.TextBox();
            this._txtSearch1 = new System.Windows.Forms.TextBox();
            this._txtSearch5 = new System.Windows.Forms.TextBox();
            this._dgList = new System.Windows.Forms.DataGridView();
            this.Panel2 = new System.Windows.Forms.Panel();
            this._cmdFilter = new System.Windows.Forms.Button();
            this._cmdExit = new System.Windows.Forms.Button();
            this._cmdFind = new System.Windows.Forms.Button();
            this.StatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslSearchMode = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslRecNo = new System.Windows.Forms.ToolStripStatusLabel();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgList)).BeginInit();
            this.Panel2.SuspendLayout();
            this.StatusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Panel1.Controls.Add(this._cbFilter8);
            this.Panel1.Controls.Add(this._cbFilter7);
            this.Panel1.Controls.Add(this._cbFilter6);
            this.Panel1.Controls.Add(this._cbFilter5);
            this.Panel1.Controls.Add(this._cbFilter4);
            this.Panel1.Controls.Add(this._cbFilter3);
            this.Panel1.Controls.Add(this._cbFilter2);
            this.Panel1.Controls.Add(this._cbFilter1);
            this.Panel1.Controls.Add(this._txtSearch8);
            this.Panel1.Controls.Add(this._txtSearch7);
            this.Panel1.Controls.Add(this._txtSearch6);
            this.Panel1.Controls.Add(this._txtSearch2);
            this.Panel1.Controls.Add(this._txtSearch3);
            this.Panel1.Controls.Add(this._txtSearch4);
            this.Panel1.Controls.Add(this._txtSearch1);
            this.Panel1.Controls.Add(this._txtSearch5);
            this.Panel1.Controls.Add(this._dgList);
            this.Panel1.Location = new System.Drawing.Point(8, 80);
            this.Panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(944, 377);
            this.Panel1.TabIndex = 1;
            // 
            // _cbFilter8
            // 
            this._cbFilter8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cbFilter8.FormattingEnabled = true;
            this._cbFilter8.Location = new System.Drawing.Point(195, 270);
            this._cbFilter8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._cbFilter8.Name = "_cbFilter8";
            this._cbFilter8.Size = new System.Drawing.Size(132, 25);
            this._cbFilter8.TabIndex = 41;
            this._cbFilter8.Visible = false;
            this._cbFilter8.DropDown += new System.EventHandler(this.cbFilters_DropDown);
            this._cbFilter8.SelectedIndexChanged += new System.EventHandler(this.cbFilters_SelectedIndexChanged);
            // 
            // _cbFilter7
            // 
            this._cbFilter7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cbFilter7.FormattingEnabled = true;
            this._cbFilter7.Location = new System.Drawing.Point(195, 235);
            this._cbFilter7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._cbFilter7.Name = "_cbFilter7";
            this._cbFilter7.Size = new System.Drawing.Size(132, 25);
            this._cbFilter7.TabIndex = 40;
            this._cbFilter7.Visible = false;
            this._cbFilter7.DropDown += new System.EventHandler(this.cbFilters_DropDown);
            this._cbFilter7.SelectedIndexChanged += new System.EventHandler(this.cbFilters_SelectedIndexChanged);
            // 
            // _cbFilter6
            // 
            this._cbFilter6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cbFilter6.FormattingEnabled = true;
            this._cbFilter6.Location = new System.Drawing.Point(195, 202);
            this._cbFilter6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._cbFilter6.Name = "_cbFilter6";
            this._cbFilter6.Size = new System.Drawing.Size(132, 25);
            this._cbFilter6.TabIndex = 39;
            this._cbFilter6.Visible = false;
            this._cbFilter6.DropDown += new System.EventHandler(this.cbFilters_DropDown);
            this._cbFilter6.SelectedIndexChanged += new System.EventHandler(this.cbFilters_SelectedIndexChanged);
            // 
            // _cbFilter5
            // 
            this._cbFilter5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cbFilter5.FormattingEnabled = true;
            this._cbFilter5.Location = new System.Drawing.Point(195, 167);
            this._cbFilter5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._cbFilter5.Name = "_cbFilter5";
            this._cbFilter5.Size = new System.Drawing.Size(132, 25);
            this._cbFilter5.TabIndex = 38;
            this._cbFilter5.Visible = false;
            this._cbFilter5.DropDown += new System.EventHandler(this.cbFilters_DropDown);
            this._cbFilter5.SelectedIndexChanged += new System.EventHandler(this.cbFilters_SelectedIndexChanged);
            // 
            // _cbFilter4
            // 
            this._cbFilter4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cbFilter4.FormattingEnabled = true;
            this._cbFilter4.Location = new System.Drawing.Point(195, 132);
            this._cbFilter4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._cbFilter4.Name = "_cbFilter4";
            this._cbFilter4.Size = new System.Drawing.Size(132, 25);
            this._cbFilter4.TabIndex = 37;
            this._cbFilter4.Visible = false;
            this._cbFilter4.DropDown += new System.EventHandler(this.cbFilters_DropDown);
            this._cbFilter4.SelectedIndexChanged += new System.EventHandler(this.cbFilters_SelectedIndexChanged);
            // 
            // _cbFilter3
            // 
            this._cbFilter3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cbFilter3.FormattingEnabled = true;
            this._cbFilter3.Location = new System.Drawing.Point(195, 98);
            this._cbFilter3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._cbFilter3.Name = "_cbFilter3";
            this._cbFilter3.Size = new System.Drawing.Size(132, 25);
            this._cbFilter3.TabIndex = 36;
            this._cbFilter3.Visible = false;
            this._cbFilter3.DropDown += new System.EventHandler(this.cbFilters_DropDown);
            this._cbFilter3.SelectedIndexChanged += new System.EventHandler(this.cbFilters_SelectedIndexChanged);
            // 
            // _cbFilter2
            // 
            this._cbFilter2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cbFilter2.FormattingEnabled = true;
            this._cbFilter2.Location = new System.Drawing.Point(195, 64);
            this._cbFilter2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._cbFilter2.Name = "_cbFilter2";
            this._cbFilter2.Size = new System.Drawing.Size(132, 25);
            this._cbFilter2.TabIndex = 35;
            this._cbFilter2.Visible = false;
            this._cbFilter2.DropDown += new System.EventHandler(this.cbFilters_DropDown);
            this._cbFilter2.SelectedIndexChanged += new System.EventHandler(this.cbFilters_SelectedIndexChanged);
            // 
            // _cbFilter1
            // 
            this._cbFilter1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cbFilter1.FormattingEnabled = true;
            this._cbFilter1.Location = new System.Drawing.Point(195, 28);
            this._cbFilter1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._cbFilter1.Name = "_cbFilter1";
            this._cbFilter1.Size = new System.Drawing.Size(132, 25);
            this._cbFilter1.TabIndex = 34;
            this._cbFilter1.Visible = false;
            this._cbFilter1.DropDown += new System.EventHandler(this.cbFilters_DropDown);
            this._cbFilter1.SelectedIndexChanged += new System.EventHandler(this.cbFilters_SelectedIndexChanged);
            // 
            // _txtSearch8
            // 
            this._txtSearch8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtSearch8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._txtSearch8.Location = new System.Drawing.Point(39, 268);
            this._txtSearch8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._txtSearch8.Name = "_txtSearch8";
            this._txtSearch8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._txtSearch8.Size = new System.Drawing.Size(133, 26);
            this._txtSearch8.TabIndex = 12;
            this._txtSearch8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._txtSearch8.Visible = false;
            this._txtSearch8.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchs_KeyPress);
            // 
            // _txtSearch7
            // 
            this._txtSearch7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtSearch7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._txtSearch7.Location = new System.Drawing.Point(39, 234);
            this._txtSearch7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._txtSearch7.Name = "_txtSearch7";
            this._txtSearch7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._txtSearch7.Size = new System.Drawing.Size(133, 26);
            this._txtSearch7.TabIndex = 11;
            this._txtSearch7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._txtSearch7.Visible = false;
            this._txtSearch7.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchs_KeyPress);
            // 
            // _txtSearch6
            // 
            this._txtSearch6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtSearch6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._txtSearch6.Location = new System.Drawing.Point(39, 199);
            this._txtSearch6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._txtSearch6.Name = "_txtSearch6";
            this._txtSearch6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._txtSearch6.Size = new System.Drawing.Size(133, 26);
            this._txtSearch6.TabIndex = 10;
            this._txtSearch6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._txtSearch6.Visible = false;
            this._txtSearch6.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchs_KeyPress);
            // 
            // _txtSearch2
            // 
            this._txtSearch2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtSearch2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._txtSearch2.Location = new System.Drawing.Point(39, 62);
            this._txtSearch2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._txtSearch2.Name = "_txtSearch2";
            this._txtSearch2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._txtSearch2.Size = new System.Drawing.Size(133, 26);
            this._txtSearch2.TabIndex = 6;
            this._txtSearch2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._txtSearch2.Visible = false;
            this._txtSearch2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchs_KeyPress);
            // 
            // _txtSearch3
            // 
            this._txtSearch3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtSearch3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._txtSearch3.Location = new System.Drawing.Point(39, 96);
            this._txtSearch3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._txtSearch3.Name = "_txtSearch3";
            this._txtSearch3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._txtSearch3.Size = new System.Drawing.Size(133, 26);
            this._txtSearch3.TabIndex = 7;
            this._txtSearch3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._txtSearch3.Visible = false;
            this._txtSearch3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchs_KeyPress);
            // 
            // _txtSearch4
            // 
            this._txtSearch4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtSearch4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._txtSearch4.Location = new System.Drawing.Point(39, 130);
            this._txtSearch4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._txtSearch4.Name = "_txtSearch4";
            this._txtSearch4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._txtSearch4.Size = new System.Drawing.Size(133, 26);
            this._txtSearch4.TabIndex = 8;
            this._txtSearch4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._txtSearch4.Visible = false;
            this._txtSearch4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchs_KeyPress);
            // 
            // _txtSearch1
            // 
            this._txtSearch1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtSearch1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._txtSearch1.Location = new System.Drawing.Point(39, 27);
            this._txtSearch1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._txtSearch1.MaxLength = 10;
            this._txtSearch1.Name = "_txtSearch1";
            this._txtSearch1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._txtSearch1.Size = new System.Drawing.Size(133, 26);
            this._txtSearch1.TabIndex = 5;
            this._txtSearch1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._txtSearch1.Visible = false;
            this._txtSearch1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchs_KeyPress);
            // 
            // _txtSearch5
            // 
            this._txtSearch5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtSearch5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._txtSearch5.Location = new System.Drawing.Point(39, 165);
            this._txtSearch5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._txtSearch5.Name = "_txtSearch5";
            this._txtSearch5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._txtSearch5.Size = new System.Drawing.Size(133, 26);
            this._txtSearch5.TabIndex = 9;
            this._txtSearch5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._txtSearch5.Visible = false;
            this._txtSearch5.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchs_KeyPress);
            // 
            // _dgList
            // 
            this._dgList.AllowUserToAddRows = false;
            this._dgList.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this._dgList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this._dgList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dgList.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._dgList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this._dgList.ColumnHeadersHeight = 30;
            this._dgList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._dgList.DefaultCellStyle = dataGridViewCellStyle3;
            this._dgList.Location = new System.Drawing.Point(8, 9);
            this._dgList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._dgList.MultiSelect = false;
            this._dgList.Name = "_dgList";
            this._dgList.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._dgList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this._dgList.RowHeadersWidth = 50;
            this._dgList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this._dgList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this._dgList.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this._dgList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgList.Size = new System.Drawing.Size(929, 362);
            this._dgList.TabIndex = 1;
            this._dgList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgList_CellFormatting);
//            this._dgList.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgList_ColumnWidthChanged);
            this._dgList.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgList_RowEnter);
            this._dgList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgList_KeyDown);
            // 
            // Panel2
            // 
            this.Panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel2.Controls.Add(this._cmdFilter);
            this.Panel2.Controls.Add(this._cmdExit);
            this.Panel2.Controls.Add(this._cmdFind);
            this.Panel2.Location = new System.Drawing.Point(8, 6);
            this.Panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(945, 66);
            this.Panel2.TabIndex = 2;
            // 
            // _cmdFilter
            // 
            this._cmdFilter.BackColor = System.Drawing.Color.Transparent;
            this._cmdFilter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdFilter.Location = new System.Drawing.Point(275, 15);
            this._cmdFilter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._cmdFilter.Name = "_cmdFilter";
            this._cmdFilter.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdFilter.Size = new System.Drawing.Size(121, 33);
            this._cmdFilter.TabIndex = 4;
            this._cmdFilter.Text = "فیلتر";
            this._cmdFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._cmdFilter.UseVisualStyleBackColor = false;
            this._cmdFilter.Click += new System.EventHandler(this.cmdFilter_Click);
            // 
            // _cmdExit
            // 
            this._cmdExit.BackColor = System.Drawing.Color.Transparent;
            this._cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cmdExit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdExit.ForeColor = System.Drawing.Color.Red;
            this._cmdExit.Location = new System.Drawing.Point(16, 15);
            this._cmdExit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._cmdExit.Name = "_cmdExit";
            this._cmdExit.Size = new System.Drawing.Size(121, 33);
            this._cmdExit.TabIndex = 5;
            this._cmdExit.Text = "خروج";
            this._cmdExit.UseVisualStyleBackColor = false;
            this._cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // _cmdFind
            // 
            this._cmdFind.BackColor = System.Drawing.Color.Transparent;
            this._cmdFind.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdFind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdFind.Location = new System.Drawing.Point(145, 15);
            this._cmdFind.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._cmdFind.Name = "_cmdFind";
            this._cmdFind.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdFind.Size = new System.Drawing.Size(121, 33);
            this._cmdFind.TabIndex = 3;
            this._cmdFind.Text = "جستجو";
            this._cmdFind.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._cmdFind.UseVisualStyleBackColor = false;
            this._cmdFind.Click += new System.EventHandler(this.cmdFilter_Click);
            // 
            // StatusStrip1
            // 
            this.StatusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.StatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslSearchMode,
            this.ToolStripStatusLabel1,
            this.ToolStripStatusLabel2,
            this.ToolStripStatusLabel3,
            this.ToolStripStatusLabel4,
            this.tsslRecNo});
            this.StatusStrip1.Location = new System.Drawing.Point(0, 467);
            this.StatusStrip1.Name = "StatusStrip1";
            this.StatusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.StatusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.StatusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StatusStrip1.Size = new System.Drawing.Size(961, 22);
            this.StatusStrip1.TabIndex = 3;
            this.StatusStrip1.Text = "StatusStrip1";
            // 
            // tsslSearchMode
            // 
            this.tsslSearchMode.AutoSize = false;
            this.tsslSearchMode.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.tsslSearchMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslSearchMode.Name = "tsslSearchMode";
            this.tsslSearchMode.Size = new System.Drawing.Size(120, 16);
            this.tsslSearchMode.Text = "نمایش کلی";
            this.tsslSearchMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ToolStripStatusLabel1
            // 
            this.ToolStripStatusLabel1.AutoSize = false;
            this.ToolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1";
            this.ToolStripStatusLabel1.Size = new System.Drawing.Size(112, 16);
            // 
            // ToolStripStatusLabel2
            // 
            this.ToolStripStatusLabel2.AutoSize = false;
            this.ToolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2";
            this.ToolStripStatusLabel2.Size = new System.Drawing.Size(112, 16);
            // 
            // ToolStripStatusLabel3
            // 
            this.ToolStripStatusLabel3.AutoSize = false;
            this.ToolStripStatusLabel3.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3";
            this.ToolStripStatusLabel3.Size = new System.Drawing.Size(120, 16);
            // 
            // ToolStripStatusLabel4
            // 
            this.ToolStripStatusLabel4.AutoSize = false;
            this.ToolStripStatusLabel4.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.ToolStripStatusLabel4.Name = "ToolStripStatusLabel4";
            this.ToolStripStatusLabel4.Size = new System.Drawing.Size(120, 16);
            // 
            // tsslRecNo
            // 
            this.tsslRecNo.AutoSize = false;
            this.tsslRecNo.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.tsslRecNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslRecNo.Name = "tsslRecNo";
            this.tsslRecNo.Size = new System.Drawing.Size(200, 16);
            this.tsslRecNo.Text = "رکورد جاری و تعداد رکوردها";
            this.tsslRecNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmToolsLists
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cmdExit;
            this.ClientSize = new System.Drawing.Size(961, 489);
            this.Controls.Add(this.StatusStrip1);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.Panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(861, 297);
            this.Name = "frmToolsLists";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = " لیست رکوردهای جداول بانک اطلاعاتی";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRecordsLists_FormClosing);
            this.Load += new System.EventHandler(this.frmRecordsLists_Load);
            this.Resize += new System.EventHandler(this.frmRecordsLists_Resize);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgList)).EndInit();
            this.Panel2.ResumeLayout(false);
            this.StatusStrip1.ResumeLayout(false);
            this.StatusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
      //              _dgList.ColumnWidthChanged -= dgList_ColumnWidthChanged;
                    _dgList.RowEnter -= dgList_RowEnter;
                    _dgList.CellFormatting -= dgList_CellFormatting;
                    _dgList.KeyDown -= dgList_KeyDown;
                }

                _dgList = value;
                if (_dgList != null)
                {
         //           _dgList.ColumnWidthChanged += dgList_ColumnWidthChanged;
                    _dgList.RowEnter += dgList_RowEnter;
                    _dgList.CellFormatting += dgList_CellFormatting;
                    _dgList.KeyDown += dgList_KeyDown;
                }
            }
        }

        internal System.Windows.Forms.Panel Panel2;



        private System.Windows.Forms.Button _cmdFind;

        internal System.Windows.Forms.Button cmdFind
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdFind;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdFind != null)
                {
                    _cmdFind.Click -= cmdFilter_Click;
                }

                _cmdFind = value;
                if (_cmdFind != null)
                {
                    _cmdFind.Click += cmdFilter_Click;
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

        private TextBox _txtSearch3;

        internal TextBox txtSearch3
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtSearch3;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtSearch3 != null)
                {
                    _txtSearch3.KeyPress -= txtSearchs_KeyPress;
                }

                _txtSearch3 = value;
                if (_txtSearch3 != null)
                {
                    _txtSearch3.KeyPress += txtSearchs_KeyPress;
                }
            }
        }

        private TextBox _txtSearch4;

        internal TextBox txtSearch4
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtSearch4;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtSearch4 != null)
                {
                    _txtSearch4.KeyPress -= txtSearchs_KeyPress;
                }

                _txtSearch4 = value;
                if (_txtSearch4 != null)
                {
                    _txtSearch4.KeyPress += txtSearchs_KeyPress;
                }
            }
        }

        private TextBox _txtSearch1;

        internal TextBox txtSearch1
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtSearch1;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtSearch1 != null)
                {
                    _txtSearch1.KeyPress -= txtSearchs_KeyPress;
                }

                _txtSearch1 = value;
                if (_txtSearch1 != null)
                {
                    _txtSearch1.KeyPress += txtSearchs_KeyPress;
                }
            }
        }

        private TextBox _txtSearch5;

        internal TextBox txtSearch5
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtSearch5;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtSearch5 != null)
                {
                    _txtSearch5.KeyPress -= txtSearchs_KeyPress;
                }

                _txtSearch5 = value;
                if (_txtSearch5 != null)
                {
                    _txtSearch5.KeyPress += txtSearchs_KeyPress;
                }
            }
        }

        private TextBox _txtSearch2;

        internal TextBox txtSearch2
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtSearch2;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtSearch2 != null)
                {
                    _txtSearch2.KeyPress -= txtSearchs_KeyPress;
                }

                _txtSearch2 = value;
                if (_txtSearch2 != null)
                {
                    _txtSearch2.KeyPress += txtSearchs_KeyPress;
                }
            }
        }

        private System.Windows.Forms.Button _cmdFilter;

        internal System.Windows.Forms.Button cmdFilter
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdFilter;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdFilter != null)
                {
                    _cmdFilter.Click -= cmdFilter_Click;
                }

                _cmdFilter = value;
                if (_cmdFilter != null)
                {
                    _cmdFilter.Click += cmdFilter_Click;
                }
            }
        }

        private TextBox _txtSearch6;

        internal TextBox txtSearch6
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtSearch6;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtSearch6 != null)
                {
                    _txtSearch6.KeyPress -= txtSearchs_KeyPress;
                }

                _txtSearch6 = value;
                if (_txtSearch6 != null)
                {
                    _txtSearch6.KeyPress += txtSearchs_KeyPress;
                }
            }
        }

        internal StatusStrip StatusStrip1;
        internal ToolStripStatusLabel tsslRecNo;
        internal ToolStripStatusLabel tsslSearchMode;
        internal ToolStripStatusLabel ToolStripStatusLabel1;
        internal ToolStripStatusLabel ToolStripStatusLabel2;
        internal ToolStripStatusLabel ToolStripStatusLabel3;
        internal ToolStripStatusLabel ToolStripStatusLabel4;
        private TextBox _txtSearch8;

        internal TextBox txtSearch8
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtSearch8;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtSearch8 != null)
                {
                    _txtSearch8.KeyPress -= txtSearchs_KeyPress;
                }

                _txtSearch8 = value;
                if (_txtSearch8 != null)
                {
                    _txtSearch8.KeyPress += txtSearchs_KeyPress;
                }
            }
        }

        private TextBox _txtSearch7;

        internal TextBox txtSearch7
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtSearch7;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtSearch7 != null)
                {
                    _txtSearch7.KeyPress -= txtSearchs_KeyPress;
                }

                _txtSearch7 = value;
                if (_txtSearch7 != null)
                {
                    _txtSearch7.KeyPress += txtSearchs_KeyPress;
                }
            }
        }

        private ComboBox _cbFilter8;

        internal ComboBox cbFilter8
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cbFilter8;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cbFilter8 != null)
                {
                    _cbFilter8.DropDown -= cbFilters_DropDown;
                    _cbFilter8.SelectedIndexChanged -= cbFilters_SelectedIndexChanged;
                }

                _cbFilter8 = value;
                if (_cbFilter8 != null)
                {
                    _cbFilter8.DropDown += cbFilters_DropDown;
                    _cbFilter8.SelectedIndexChanged += cbFilters_SelectedIndexChanged;
                }
            }
        }

        private ComboBox _cbFilter7;

        internal ComboBox cbFilter7
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cbFilter7;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cbFilter7 != null)
                {
                    _cbFilter7.DropDown -= cbFilters_DropDown;
                    _cbFilter7.SelectedIndexChanged -= cbFilters_SelectedIndexChanged;
                }

                _cbFilter7 = value;
                if (_cbFilter7 != null)
                {
                    _cbFilter7.DropDown += cbFilters_DropDown;
                    _cbFilter7.SelectedIndexChanged += cbFilters_SelectedIndexChanged;
                }
            }
        }

        private ComboBox _cbFilter6;

        internal ComboBox cbFilter6
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cbFilter6;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cbFilter6 != null)
                {
                    _cbFilter6.DropDown -= cbFilters_DropDown;
                    _cbFilter6.SelectedIndexChanged -= cbFilters_SelectedIndexChanged;
                }

                _cbFilter6 = value;
                if (_cbFilter6 != null)
                {
                    _cbFilter6.DropDown += cbFilters_DropDown;
                    _cbFilter6.SelectedIndexChanged += cbFilters_SelectedIndexChanged;
                }
            }
        }

        private ComboBox _cbFilter5;

        internal ComboBox cbFilter5
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cbFilter5;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cbFilter5 != null)
                {
                    _cbFilter5.DropDown -= cbFilters_DropDown;
                    _cbFilter5.SelectedIndexChanged -= cbFilters_SelectedIndexChanged;
                }

                _cbFilter5 = value;
                if (_cbFilter5 != null)
                {
                    _cbFilter5.DropDown += cbFilters_DropDown;
                    _cbFilter5.SelectedIndexChanged += cbFilters_SelectedIndexChanged;
                }
            }
        }

        private ComboBox _cbFilter4;

        internal ComboBox cbFilter4
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cbFilter4;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cbFilter4 != null)
                {
                    _cbFilter4.DropDown -= cbFilters_DropDown;
                    _cbFilter4.SelectedIndexChanged -= cbFilters_SelectedIndexChanged;
                }

                _cbFilter4 = value;
                if (_cbFilter4 != null)
                {
                    _cbFilter4.DropDown += cbFilters_DropDown;
                    _cbFilter4.SelectedIndexChanged += cbFilters_SelectedIndexChanged;
                }
            }
        }

        private ComboBox _cbFilter3;

        internal ComboBox cbFilter3
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cbFilter3;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cbFilter3 != null)
                {
                    _cbFilter3.DropDown -= cbFilters_DropDown;
                    _cbFilter3.SelectedIndexChanged -= cbFilters_SelectedIndexChanged;
                }

                _cbFilter3 = value;
                if (_cbFilter3 != null)
                {
                    _cbFilter3.DropDown += cbFilters_DropDown;
                    _cbFilter3.SelectedIndexChanged += cbFilters_SelectedIndexChanged;
                }
            }
        }

        private ComboBox _cbFilter2;

        internal ComboBox cbFilter2
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cbFilter2;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cbFilter2 != null)
                {
                    _cbFilter2.DropDown -= cbFilters_DropDown;
                    _cbFilter2.SelectedIndexChanged -= cbFilters_SelectedIndexChanged;
                }

                _cbFilter2 = value;
                if (_cbFilter2 != null)
                {
                    _cbFilter2.DropDown += cbFilters_DropDown;
                    _cbFilter2.SelectedIndexChanged += cbFilters_SelectedIndexChanged;
                }
            }
        }

        private ComboBox _cbFilter1;

        internal ComboBox cbFilter1
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cbFilter1;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cbFilter1 != null)
                {
                    _cbFilter1.DropDown -= cbFilters_DropDown;
                    _cbFilter1.SelectedIndexChanged -= cbFilters_SelectedIndexChanged;
                }

                _cbFilter1 = value;
                if (_cbFilter1 != null)
                {
                    _cbFilter1.DropDown += cbFilters_DropDown;
                    _cbFilter1.SelectedIndexChanged += cbFilters_SelectedIndexChanged;
                }
            }
        }
    }
}