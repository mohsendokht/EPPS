using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmRecordsLists : Form
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
            Panel1 = new System.Windows.Forms.Panel();
            _cbFilter8 = new ComboBox();
            _cbFilter8.DropDown += new EventHandler(cbFilters_DropDown);
            _cbFilter8.SelectedIndexChanged += new EventHandler(cbFilters_SelectedIndexChanged);
            _cbFilter7 = new ComboBox();
            _cbFilter7.DropDown += new EventHandler(cbFilters_DropDown);
            _cbFilter7.SelectedIndexChanged += new EventHandler(cbFilters_SelectedIndexChanged);
            _cbFilter6 = new ComboBox();
            _cbFilter6.DropDown += new EventHandler(cbFilters_DropDown);
            _cbFilter6.SelectedIndexChanged += new EventHandler(cbFilters_SelectedIndexChanged);
            _cbFilter5 = new ComboBox();
            _cbFilter5.DropDown += new EventHandler(cbFilters_DropDown);
            _cbFilter5.SelectedIndexChanged += new EventHandler(cbFilters_SelectedIndexChanged);
            _cbFilter4 = new ComboBox();
            _cbFilter4.DropDown += new EventHandler(cbFilters_DropDown);
            _cbFilter4.SelectedIndexChanged += new EventHandler(cbFilters_SelectedIndexChanged);
            _cbFilter3 = new ComboBox();
            _cbFilter3.DropDown += new EventHandler(cbFilters_DropDown);
            _cbFilter3.SelectedIndexChanged += new EventHandler(cbFilters_SelectedIndexChanged);
            _cbFilter2 = new ComboBox();
            _cbFilter2.DropDown += new EventHandler(cbFilters_DropDown);
            _cbFilter2.SelectedIndexChanged += new EventHandler(cbFilters_SelectedIndexChanged);
            _cbFilter1 = new ComboBox();
            _cbFilter1.DropDown += new EventHandler(cbFilters_DropDown);
            _cbFilter1.SelectedIndexChanged += new EventHandler(cbFilters_SelectedIndexChanged);
            _txtSearch8 = new TextBox();
            _txtSearch8.KeyPress += new KeyPressEventHandler(txtSearchs_KeyPress);
            _txtSearch7 = new TextBox();
            _txtSearch7.KeyPress += new KeyPressEventHandler(txtSearchs_KeyPress);
            _txtSearch6 = new TextBox();
            _txtSearch6.KeyPress += new KeyPressEventHandler(txtSearchs_KeyPress);
            _txtSearch2 = new TextBox();
            _txtSearch2.KeyPress += new KeyPressEventHandler(txtSearchs_KeyPress);
            _txtSearch3 = new TextBox();
            _txtSearch3.KeyPress += new KeyPressEventHandler(txtSearchs_KeyPress);
            _txtSearch4 = new TextBox();
            _txtSearch4.KeyPress += new KeyPressEventHandler(txtSearchs_KeyPress);
            _txtSearch1 = new TextBox();
            _txtSearch1.KeyPress += new KeyPressEventHandler(txtSearchs_KeyPress);
            _txtSearch5 = new TextBox();
            _txtSearch5.KeyPress += new KeyPressEventHandler(txtSearchs_KeyPress);
            _dgList = new DataGridView();
            _dgList.ColumnWidthChanged += new DataGridViewColumnEventHandler(dgList_ColumnWidthChanged);
            _dgList.RowEnter += new DataGridViewCellEventHandler(dgList_RowEnter);
            _dgList.CellFormatting += new DataGridViewCellFormattingEventHandler(dgList_CellFormatting);
            _dgList.KeyDown += new KeyEventHandler(dgList_KeyDown);
            Panel2 = new System.Windows.Forms.Panel();
            _cmdFilter = new System.Windows.Forms.Button();
            _cmdFilter.Click += new EventHandler(cmdFilter_Click);
            _cmdExit = new System.Windows.Forms.Button();
            _cmdExit.Click += new EventHandler(cmdExit_Click);
            _cmdFind = new System.Windows.Forms.Button();
            _cmdFind.Click += new EventHandler(cmdFilter_Click);
            _cmdDelete = new System.Windows.Forms.Button();
            _cmdDelete.Click += new EventHandler(cmdDelete_Click);
            _cmdUpdate = new System.Windows.Forms.Button();
            _cmdUpdate.Click += new EventHandler(cmdDelete_Click);
            _cmdInsert = new System.Windows.Forms.Button();
            _cmdInsert.Click += new EventHandler(cmdDelete_Click);
            StatusStrip1 = new StatusStrip();
            tsslSearchMode = new ToolStripStatusLabel();
            ToolStripStatusLabel1 = new ToolStripStatusLabel();
            ToolStripStatusLabel2 = new ToolStripStatusLabel();
            ToolStripStatusLabel3 = new ToolStripStatusLabel();
            ToolStripStatusLabel4 = new ToolStripStatusLabel();
            tsslRecNo = new ToolStripStatusLabel();
            Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgList).BeginInit();
            Panel2.SuspendLayout();
            StatusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // Panel1
            // 
            Panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            Panel1.BorderStyle = BorderStyle.Fixed3D;
            Panel1.Controls.Add(_cbFilter8);
            Panel1.Controls.Add(_cbFilter7);
            Panel1.Controls.Add(_cbFilter6);
            Panel1.Controls.Add(_cbFilter5);
            Panel1.Controls.Add(_cbFilter4);
            Panel1.Controls.Add(_cbFilter3);
            Panel1.Controls.Add(_cbFilter2);
            Panel1.Controls.Add(_cbFilter1);
            Panel1.Controls.Add(_txtSearch8);
            Panel1.Controls.Add(_txtSearch7);
            Panel1.Controls.Add(_txtSearch6);
            Panel1.Controls.Add(_txtSearch2);
            Panel1.Controls.Add(_txtSearch3);
            Panel1.Controls.Add(_txtSearch4);
            Panel1.Controls.Add(_txtSearch1);
            Panel1.Controls.Add(_txtSearch5);
            Panel1.Controls.Add(_dgList);
            Panel1.Location = new Point(6, 65);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(709, 307);
            Panel1.TabIndex = 1;
            // 
            // cbFilter8
            // 
            _cbFilter8.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cbFilter8.FormattingEnabled = true;
            _cbFilter8.Location = new Point(146, 219);
            _cbFilter8.Name = "_cbFilter8";
            _cbFilter8.Size = new Size(100, 21);
            _cbFilter8.TabIndex = 41;
            _cbFilter8.Visible = false;
            // 
            // cbFilter7
            // 
            _cbFilter7.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cbFilter7.FormattingEnabled = true;
            _cbFilter7.Location = new Point(146, 191);
            _cbFilter7.Name = "_cbFilter7";
            _cbFilter7.Size = new Size(100, 21);
            _cbFilter7.TabIndex = 40;
            _cbFilter7.Visible = false;
            // 
            // cbFilter6
            // 
            _cbFilter6.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cbFilter6.FormattingEnabled = true;
            _cbFilter6.Location = new Point(146, 164);
            _cbFilter6.Name = "_cbFilter6";
            _cbFilter6.Size = new Size(100, 21);
            _cbFilter6.TabIndex = 39;
            _cbFilter6.Visible = false;
            // 
            // cbFilter5
            // 
            _cbFilter5.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cbFilter5.FormattingEnabled = true;
            _cbFilter5.Location = new Point(146, 136);
            _cbFilter5.Name = "_cbFilter5";
            _cbFilter5.Size = new Size(100, 21);
            _cbFilter5.TabIndex = 38;
            _cbFilter5.Visible = false;
            // 
            // cbFilter4
            // 
            _cbFilter4.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cbFilter4.FormattingEnabled = true;
            _cbFilter4.Location = new Point(146, 107);
            _cbFilter4.Name = "_cbFilter4";
            _cbFilter4.Size = new Size(100, 21);
            _cbFilter4.TabIndex = 37;
            _cbFilter4.Visible = false;
            // 
            // cbFilter3
            // 
            _cbFilter3.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cbFilter3.FormattingEnabled = true;
            _cbFilter3.Location = new Point(146, 80);
            _cbFilter3.Name = "_cbFilter3";
            _cbFilter3.Size = new Size(100, 21);
            _cbFilter3.TabIndex = 36;
            _cbFilter3.Visible = false;
            // 
            // cbFilter2
            // 
            _cbFilter2.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cbFilter2.FormattingEnabled = true;
            _cbFilter2.Location = new Point(146, 52);
            _cbFilter2.Name = "_cbFilter2";
            _cbFilter2.Size = new Size(100, 21);
            _cbFilter2.TabIndex = 35;
            _cbFilter2.Visible = false;
            // 
            // cbFilter1
            // 
            _cbFilter1.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cbFilter1.FormattingEnabled = true;
            _cbFilter1.Location = new Point(146, 23);
            _cbFilter1.Name = "_cbFilter1";
            _cbFilter1.Size = new Size(100, 21);
            _cbFilter1.TabIndex = 34;
            _cbFilter1.Visible = false;
            // 
            // txtSearch8
            // 
            _txtSearch8.BorderStyle = BorderStyle.FixedSingle;
            _txtSearch8.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _txtSearch8.Location = new Point(29, 218);
            _txtSearch8.Name = "_txtSearch8";
            _txtSearch8.RightToLeft = RightToLeft.Yes;
            _txtSearch8.Size = new Size(100, 22);
            _txtSearch8.TabIndex = 12;
            _txtSearch8.TextAlign = HorizontalAlignment.Center;
            _txtSearch8.Visible = false;
            // 
            // txtSearch7
            // 
            _txtSearch7.BorderStyle = BorderStyle.FixedSingle;
            _txtSearch7.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _txtSearch7.Location = new Point(29, 190);
            _txtSearch7.Name = "_txtSearch7";
            _txtSearch7.RightToLeft = RightToLeft.Yes;
            _txtSearch7.Size = new Size(100, 22);
            _txtSearch7.TabIndex = 11;
            _txtSearch7.TextAlign = HorizontalAlignment.Center;
            _txtSearch7.Visible = false;
            // 
            // txtSearch6
            // 
            _txtSearch6.BorderStyle = BorderStyle.FixedSingle;
            _txtSearch6.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _txtSearch6.Location = new Point(29, 162);
            _txtSearch6.Name = "_txtSearch6";
            _txtSearch6.RightToLeft = RightToLeft.Yes;
            _txtSearch6.Size = new Size(100, 22);
            _txtSearch6.TabIndex = 10;
            _txtSearch6.TextAlign = HorizontalAlignment.Center;
            _txtSearch6.Visible = false;
            // 
            // txtSearch2
            // 
            _txtSearch2.BorderStyle = BorderStyle.FixedSingle;
            _txtSearch2.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _txtSearch2.Location = new Point(29, 50);
            _txtSearch2.Name = "_txtSearch2";
            _txtSearch2.RightToLeft = RightToLeft.Yes;
            _txtSearch2.Size = new Size(100, 22);
            _txtSearch2.TabIndex = 6;
            _txtSearch2.TextAlign = HorizontalAlignment.Center;
            _txtSearch2.Visible = false;
            // 
            // txtSearch3
            // 
            _txtSearch3.BorderStyle = BorderStyle.FixedSingle;
            _txtSearch3.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _txtSearch3.Location = new Point(29, 78);
            _txtSearch3.Name = "_txtSearch3";
            _txtSearch3.RightToLeft = RightToLeft.Yes;
            _txtSearch3.Size = new Size(100, 22);
            _txtSearch3.TabIndex = 7;
            _txtSearch3.TextAlign = HorizontalAlignment.Center;
            _txtSearch3.Visible = false;
            // 
            // txtSearch4
            // 
            _txtSearch4.BorderStyle = BorderStyle.FixedSingle;
            _txtSearch4.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _txtSearch4.Location = new Point(29, 106);
            _txtSearch4.Name = "_txtSearch4";
            _txtSearch4.RightToLeft = RightToLeft.Yes;
            _txtSearch4.Size = new Size(100, 22);
            _txtSearch4.TabIndex = 8;
            _txtSearch4.TextAlign = HorizontalAlignment.Center;
            _txtSearch4.Visible = false;
            // 
            // txtSearch1
            // 
            _txtSearch1.BorderStyle = BorderStyle.FixedSingle;
            _txtSearch1.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _txtSearch1.Location = new Point(29, 22);
            _txtSearch1.MaxLength = 10;
            _txtSearch1.Name = "_txtSearch1";
            _txtSearch1.RightToLeft = RightToLeft.Yes;
            _txtSearch1.Size = new Size(100, 22);
            _txtSearch1.TabIndex = 5;
            _txtSearch1.TextAlign = HorizontalAlignment.Center;
            _txtSearch1.Visible = false;
            // 
            // txtSearch5
            // 
            _txtSearch5.BorderStyle = BorderStyle.FixedSingle;
            _txtSearch5.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _txtSearch5.Location = new Point(29, 134);
            _txtSearch5.Name = "_txtSearch5";
            _txtSearch5.RightToLeft = RightToLeft.Yes;
            _txtSearch5.Size = new Size(100, 22);
            _txtSearch5.TabIndex = 9;
            _txtSearch5.TextAlign = HorizontalAlignment.Center;
            _txtSearch5.Visible = false;
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
            DataGridViewCellStyle3.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            DataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            _dgList.DefaultCellStyle = DataGridViewCellStyle3;
            _dgList.Location = new Point(6, 7);
            _dgList.MultiSelect = false;
            _dgList.Name = "_dgList";
            _dgList.ReadOnly = true;
            DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridViewCellStyle4.BackColor = SystemColors.Control;
            DataGridViewCellStyle4.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            DataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            _dgList.RowHeadersDefaultCellStyle = DataGridViewCellStyle4;
            _dgList.RowHeadersWidth = 50;
            _dgList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle5.BackColor = SystemColors.Info;
            DataGridViewCellStyle5.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle5.ForeColor = Color.Black;
            DataGridViewCellStyle5.SelectionBackColor = Color.RoyalBlue;
            _dgList.RowsDefaultCellStyle = DataGridViewCellStyle5;
            _dgList.RowTemplate.Resizable = DataGridViewTriState.True;
            _dgList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgList.Size = new Size(697, 294);
            _dgList.TabIndex = 1;
            // 
            // Panel2
            // 
            Panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Panel2.BackColor = SystemColors.Control;
            Panel2.BorderStyle = BorderStyle.FixedSingle;
            Panel2.Controls.Add(_cmdFilter);
            Panel2.Controls.Add(_cmdExit);
            Panel2.Controls.Add(_cmdFind);
            Panel2.Controls.Add(_cmdDelete);
            Panel2.Controls.Add(_cmdUpdate);
            Panel2.Controls.Add(_cmdInsert);
            Panel2.Location = new Point(6, 5);
            Panel2.Name = "Panel2";
            Panel2.Size = new Size(709, 54);
            Panel2.TabIndex = 2;
            // 
            // cmdFilter
            // 
            _cmdFilter.BackColor = Color.Transparent;
            _cmdFilter.Font = new Font("Tahoma", 9.0f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdFilter.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdFilter.Location = new Point(206, 12);
            _cmdFilter.Name = "_cmdFilter";
            _cmdFilter.RightToLeft = RightToLeft.No;
            _cmdFilter.Size = new Size(91, 27);
            _cmdFilter.TabIndex = 4;
            _cmdFilter.Text = "فیلتر";
            _cmdFilter.TextAlign = ContentAlignment.MiddleRight;
            _cmdFilter.UseVisualStyleBackColor = false;
            // 
            // cmdExit
            // 
            _cmdExit.BackColor = Color.Transparent;
            _cmdExit.DialogResult = DialogResult.Cancel;
            _cmdExit.Font = new Font("Tahoma", 9.0f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdExit.ForeColor = Color.Red;
            _cmdExit.Location = new Point(12, 12);
            _cmdExit.Name = "_cmdExit";
            _cmdExit.Size = new Size(91, 27);
            _cmdExit.TabIndex = 5;
            _cmdExit.Text = "خروج";
            _cmdExit.UseVisualStyleBackColor = false;
            // 
            // cmdFind
            // 
            _cmdFind.BackColor = Color.Transparent;
            _cmdFind.Font = new Font("Tahoma", 9.0f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdFind.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdFind.Location = new Point(109, 12);
            _cmdFind.Name = "_cmdFind";
            _cmdFind.RightToLeft = RightToLeft.No;
            _cmdFind.Size = new Size(91, 27);
            _cmdFind.TabIndex = 3;
            _cmdFind.Text = "جستجو";
            _cmdFind.TextAlign = ContentAlignment.MiddleRight;
            _cmdFind.UseVisualStyleBackColor = false;
            // 
            // cmdDelete
            // 
            _cmdDelete.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            _cmdDelete.BackColor = Color.Transparent;
            _cmdDelete.Font = new Font("Tahoma", 9.0f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdDelete.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdDelete.Location = new Point(604, 12);
            _cmdDelete.Name = "_cmdDelete";
            _cmdDelete.RightToLeft = RightToLeft.No;
            _cmdDelete.Size = new Size(91, 27);
            _cmdDelete.TabIndex = 2;
            _cmdDelete.Text = "حذف";
            _cmdDelete.TextAlign = ContentAlignment.MiddleRight;
            _cmdDelete.UseVisualStyleBackColor = false;
            // 
            // cmdUpdate
            // 
            _cmdUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            _cmdUpdate.BackColor = Color.Transparent;
            _cmdUpdate.Font = new Font("Tahoma", 9.0f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdUpdate.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdUpdate.Location = new Point(507, 12);
            _cmdUpdate.Name = "_cmdUpdate";
            _cmdUpdate.RightToLeft = RightToLeft.No;
            _cmdUpdate.Size = new Size(91, 27);
            _cmdUpdate.TabIndex = 1;
            _cmdUpdate.Text = "اصلاح";
            _cmdUpdate.TextAlign = ContentAlignment.MiddleRight;
            _cmdUpdate.UseVisualStyleBackColor = false;
            // 
            // cmdInsert
            // 
            _cmdInsert.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            _cmdInsert.BackColor = Color.Transparent;
            _cmdInsert.Font = new Font("Tahoma", 9.0f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdInsert.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdInsert.Location = new Point(410, 12);
            _cmdInsert.Name = "_cmdInsert";
            _cmdInsert.RightToLeft = RightToLeft.No;
            _cmdInsert.Size = new Size(91, 27);
            _cmdInsert.TabIndex = 0;
            _cmdInsert.Text = "جدید";
            _cmdInsert.TextAlign = ContentAlignment.MiddleRight;
            _cmdInsert.UseVisualStyleBackColor = false;
            // 
            // StatusStrip1
            // 
            StatusStrip1.Items.AddRange(new ToolStripItem[] { tsslSearchMode, ToolStripStatusLabel1, ToolStripStatusLabel2, ToolStripStatusLabel3, ToolStripStatusLabel4, tsslRecNo });
            StatusStrip1.Location = new Point(0, 375);
            StatusStrip1.Name = "StatusStrip1";
            StatusStrip1.RenderMode = ToolStripRenderMode.Professional;
            StatusStrip1.RightToLeft = RightToLeft.No;
            StatusStrip1.Size = new Size(721, 22);
            StatusStrip1.TabIndex = 3;
            StatusStrip1.Text = "StatusStrip1";
            // 
            // tsslSearchMode
            // 
            tsslSearchMode.AutoSize = false;
            tsslSearchMode.BorderStyle = Border3DStyle.Sunken;
            tsslSearchMode.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsslSearchMode.Name = "tsslSearchMode";
            tsslSearchMode.Size = new Size(120, 17);
            tsslSearchMode.Text = "نمایش کلی";
            tsslSearchMode.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ToolStripStatusLabel1
            // 
            ToolStripStatusLabel1.AutoSize = false;
            ToolStripStatusLabel1.BorderStyle = Border3DStyle.Sunken;
            ToolStripStatusLabel1.Name = "ToolStripStatusLabel1";
            ToolStripStatusLabel1.Size = new Size(112, 17);
            // 
            // ToolStripStatusLabel2
            // 
            ToolStripStatusLabel2.AutoSize = false;
            ToolStripStatusLabel2.BorderStyle = Border3DStyle.Sunken;
            ToolStripStatusLabel2.Name = "ToolStripStatusLabel2";
            ToolStripStatusLabel2.Size = new Size(112, 17);
            // 
            // ToolStripStatusLabel3
            // 
            ToolStripStatusLabel3.AutoSize = false;
            ToolStripStatusLabel3.BorderStyle = Border3DStyle.Sunken;
            ToolStripStatusLabel3.Name = "ToolStripStatusLabel3";
            ToolStripStatusLabel3.Size = new Size(120, 17);
            // 
            // ToolStripStatusLabel4
            // 
            ToolStripStatusLabel4.AutoSize = false;
            ToolStripStatusLabel4.BorderStyle = Border3DStyle.Sunken;
            ToolStripStatusLabel4.Name = "ToolStripStatusLabel4";
            ToolStripStatusLabel4.Size = new Size(120, 17);
            // 
            // tsslRecNo
            // 
            tsslRecNo.AutoSize = false;
            tsslRecNo.BorderStyle = Border3DStyle.Sunken;
            tsslRecNo.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(0));
            tsslRecNo.Name = "tsslRecNo";
            tsslRecNo.Size = new Size(200, 17);
            tsslRecNo.Text = "رکورد جاری و تعداد رکوردها";
            tsslRecNo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // frmRecordsLists
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _cmdExit;
            ClientSize = new Size(721, 397);
            Controls.Add(StatusStrip1);
            Controls.Add(Panel2);
            Controls.Add(Panel1);
            MinimumSize = new Size(650, 250);
            Name = "frmRecordsLists";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = " لیست رکوردهای جداول بانک اطلاعاتی";
            Panel1.ResumeLayout(false);
            Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_dgList).EndInit();
            Panel2.ResumeLayout(false);
            StatusStrip1.ResumeLayout(false);
            StatusStrip1.PerformLayout();
            Load += new EventHandler(frmRecordsLists_Load);
            FormClosing += new FormClosingEventHandler(frmRecordsLists_FormClosing);
            Resize += new EventHandler(frmRecordsLists_Resize);
            ResumeLayout(false);
            PerformLayout();
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
                    _dgList.ColumnWidthChanged -= dgList_ColumnWidthChanged;
                    _dgList.RowEnter -= dgList_RowEnter;
                    _dgList.CellFormatting -= dgList_CellFormatting;
                    _dgList.KeyDown -= dgList_KeyDown;
                }

                _dgList = value;
                if (_dgList != null)
                {
                    _dgList.ColumnWidthChanged += dgList_ColumnWidthChanged;
                    _dgList.RowEnter += dgList_RowEnter;
                    _dgList.CellFormatting += dgList_CellFormatting;
                    _dgList.KeyDown += dgList_KeyDown;
                }
            }
        }

        internal System.Windows.Forms.Panel Panel2;
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
                    _cmdInsert.Click -= cmdDelete_Click;
                }

                _cmdInsert = value;
                if (_cmdInsert != null)
                {
                    _cmdInsert.Click += cmdDelete_Click;
                }
            }
        }

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
                    _cmdUpdate.Click -= cmdDelete_Click;
                }

                _cmdUpdate = value;
                if (_cmdUpdate != null)
                {
                    _cmdUpdate.Click += cmdDelete_Click;
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