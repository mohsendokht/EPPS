using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmMPSList : Form
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
            StatusStrip1 = new StatusStrip();
            tsslSearchMode = new ToolStripStatusLabel();
            ToolStripStatusLabel1 = new ToolStripStatusLabel();
            ToolStripStatusLabel2 = new ToolStripStatusLabel();
            ToolStripStatusLabel3 = new ToolStripStatusLabel();
            ToolStripStatusLabel4 = new ToolStripStatusLabel();
            tsslRecNo = new ToolStripStatusLabel();
            _txtSearch2 = new TextBox();
            _txtSearch2.KeyPress += new KeyPressEventHandler(txtSearchs_KeyPress);
            Panel1 = new System.Windows.Forms.Panel();
            _cbFilter3 = new ComboBox();
            _cbFilter3.DropDown += new EventHandler(cbFilters_DropDown);
            _cbFilter3.SelectedIndexChanged += new EventHandler(cbFilters_SelectedIndexChanged);
            _cbFilter2 = new ComboBox();
            _cbFilter2.DropDown += new EventHandler(cbFilters_DropDown);
            _cbFilter2.SelectedIndexChanged += new EventHandler(cbFilters_SelectedIndexChanged);
            _cbFilter1 = new ComboBox();
            _cbFilter1.DropDown += new EventHandler(cbFilters_DropDown);
            _cbFilter1.SelectedIndexChanged += new EventHandler(cbFilters_SelectedIndexChanged);
            _txtSearch3 = new TextBox();
            _txtSearch3.KeyPress += new KeyPressEventHandler(txtSearchs_KeyPress);
            _txtSearch1 = new TextBox();
            _txtSearch1.KeyPress += new KeyPressEventHandler(txtSearchs_KeyPress);
            _dgList = new DataGridView();
            _dgList.ColumnWidthChanged += new DataGridViewColumnEventHandler(dgList_ColumnWidthChanged);
            _dgList.RowEnter += new DataGridViewCellEventHandler(dgList_RowEnter);
            _dgList.CellFormatting += new DataGridViewCellFormattingEventHandler(dgList_CellFormatting);
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
            _cmdCapametryShow = new System.Windows.Forms.Button();
            _cmdCapametryShow.Click += new EventHandler(cmdCapametryShow_Click);
            _cmdReCapametry = new System.Windows.Forms.Button();
            _cmdReCapametry.Click += new EventHandler(cmdCapametry_Click);
            StatusStrip1.SuspendLayout();
            Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgList).BeginInit();
            Panel2.SuspendLayout();
            SuspendLayout();
            // 
            // StatusStrip1
            // 
            StatusStrip1.Items.AddRange(new ToolStripItem[] { tsslSearchMode, ToolStripStatusLabel1, ToolStripStatusLabel2, ToolStripStatusLabel3, ToolStripStatusLabel4, tsslRecNo });
            StatusStrip1.Location = new Point(0, 402);
            StatusStrip1.Name = "StatusStrip1";
            StatusStrip1.RenderMode = ToolStripRenderMode.Professional;
            StatusStrip1.RightToLeft = RightToLeft.No;
            StatusStrip1.Size = new Size(841, 22);
            StatusStrip1.TabIndex = 6;
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
            tsslRecNo.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(0));
            tsslRecNo.Name = "tsslRecNo";
            tsslRecNo.Size = new Size(200, 17);
            tsslRecNo.Text = "رکورد جاری و تعداد رکوردها";
            tsslRecNo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtSearch2
            // 
            _txtSearch2.BorderStyle = BorderStyle.FixedSingle;
            _txtSearch2.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _txtSearch2.Location = new Point(29, 91);
            _txtSearch2.Name = "_txtSearch2";
            _txtSearch2.RightToLeft = RightToLeft.Yes;
            _txtSearch2.Size = new Size(100, 22);
            _txtSearch2.TabIndex = 6;
            _txtSearch2.TextAlign = HorizontalAlignment.Center;
            _txtSearch2.Visible = false;
            // 
            // Panel1
            // 
            Panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            Panel1.BorderStyle = BorderStyle.Fixed3D;
            Panel1.Controls.Add(_cbFilter3);
            Panel1.Controls.Add(_cbFilter2);
            Panel1.Controls.Add(_cbFilter1);
            Panel1.Controls.Add(_txtSearch2);
            Panel1.Controls.Add(_txtSearch3);
            Panel1.Controls.Add(_txtSearch1);
            Panel1.Controls.Add(_dgList);
            Panel1.Location = new Point(5, 58);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(829, 306);
            Panel1.TabIndex = 4;
            // 
            // cbFilter3
            // 
            _cbFilter3.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cbFilter3.FormattingEnabled = true;
            _cbFilter3.Location = new Point(135, 120);
            _cbFilter3.Name = "_cbFilter3";
            _cbFilter3.Size = new Size(100, 21);
            _cbFilter3.TabIndex = 57;
            _cbFilter3.Visible = false;
            // 
            // cbFilter2
            // 
            _cbFilter2.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cbFilter2.FormattingEnabled = true;
            _cbFilter2.Location = new Point(135, 92);
            _cbFilter2.Name = "_cbFilter2";
            _cbFilter2.Size = new Size(100, 21);
            _cbFilter2.TabIndex = 56;
            _cbFilter2.Visible = false;
            // 
            // cbFilter1
            // 
            _cbFilter1.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cbFilter1.FormattingEnabled = true;
            _cbFilter1.Location = new Point(135, 63);
            _cbFilter1.Name = "_cbFilter1";
            _cbFilter1.Size = new Size(100, 21);
            _cbFilter1.TabIndex = 55;
            _cbFilter1.Visible = false;
            // 
            // txtSearch3
            // 
            _txtSearch3.BorderStyle = BorderStyle.FixedSingle;
            _txtSearch3.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _txtSearch3.Location = new Point(29, 119);
            _txtSearch3.Name = "_txtSearch3";
            _txtSearch3.RightToLeft = RightToLeft.Yes;
            _txtSearch3.Size = new Size(100, 22);
            _txtSearch3.TabIndex = 7;
            _txtSearch3.TextAlign = HorizontalAlignment.Center;
            _txtSearch3.Visible = false;
            // 
            // txtSearch1
            // 
            _txtSearch1.BorderStyle = BorderStyle.FixedSingle;
            _txtSearch1.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _txtSearch1.Location = new Point(29, 63);
            _txtSearch1.MaxLength = 10;
            _txtSearch1.Name = "_txtSearch1";
            _txtSearch1.RightToLeft = RightToLeft.Yes;
            _txtSearch1.Size = new Size(100, 22);
            _txtSearch1.TabIndex = 5;
            _txtSearch1.TextAlign = HorizontalAlignment.Center;
            _txtSearch1.Visible = false;
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
            _dgList.Size = new Size(817, 293);
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
            Panel2.Location = new Point(5, 4);
            Panel2.Name = "Panel2";
            Panel2.Size = new Size(829, 48);
            Panel2.TabIndex = 8;
            // 
            // cmdFilter
            // 
            _cmdFilter.BackColor = Color.Transparent;
            _cmdFilter.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdFilter.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdFilter.Location = new Point(195, 11);
            _cmdFilter.Name = "_cmdFilter";
            _cmdFilter.RightToLeft = RightToLeft.No;
            _cmdFilter.Size = new Size(85, 27);
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
            _cmdExit.Location = new Point(7, 11);
            _cmdExit.Name = "_cmdExit";
            _cmdExit.Size = new Size(91, 27);
            _cmdExit.TabIndex = 5;
            _cmdExit.Text = "خروج";
            _cmdExit.UseVisualStyleBackColor = false;
            // 
            // cmdFind
            // 
            _cmdFind.BackColor = Color.Transparent;
            _cmdFind.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdFind.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdFind.Location = new Point(104, 11);
            _cmdFind.Name = "_cmdFind";
            _cmdFind.RightToLeft = RightToLeft.No;
            _cmdFind.Size = new Size(85, 27);
            _cmdFind.TabIndex = 3;
            _cmdFind.Text = "جستجو";
            _cmdFind.TextAlign = ContentAlignment.MiddleRight;
            _cmdFind.UseVisualStyleBackColor = false;
            // 
            // cmdDelete
            // 
            _cmdDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _cmdDelete.BackColor = Color.Transparent;
            _cmdDelete.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdDelete.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdDelete.Location = new Point(735, 11);
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
            _cmdUpdate.Location = new Point(644, 11);
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
            _cmdInsert.Location = new Point(553, 11);
            _cmdInsert.Name = "_cmdInsert";
            _cmdInsert.RightToLeft = RightToLeft.No;
            _cmdInsert.Size = new Size(85, 27);
            _cmdInsert.TabIndex = 0;
            _cmdInsert.Text = "جدید";
            _cmdInsert.TextAlign = ContentAlignment.MiddleRight;
            _cmdInsert.UseVisualStyleBackColor = false;
            // 
            // cmdCapametryShow
            // 
            _cmdCapametryShow.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _cmdCapametryShow.BackColor = Color.Transparent;
            _cmdCapametryShow.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdCapametryShow.ForeColor = Color.Blue;
            _cmdCapametryShow.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdCapametryShow.Location = new Point(12, 370);
            _cmdCapametryShow.Name = "_cmdCapametryShow";
            _cmdCapametryShow.RightToLeft = RightToLeft.No;
            _cmdCapametryShow.Size = new Size(186, 27);
            _cmdCapametryShow.TabIndex = 11;
            _cmdCapametryShow.Text = "مشاهده ظرفیت سنجی";
            _cmdCapametryShow.UseVisualStyleBackColor = false;
            // 
            // cmdReCapametry
            // 
            _cmdReCapametry.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _cmdReCapametry.BackColor = Color.Transparent;
            _cmdReCapametry.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdReCapametry.ForeColor = Color.Blue;
            _cmdReCapametry.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdReCapametry.Location = new Point(640, 370);
            _cmdReCapametry.Name = "_cmdReCapametry";
            _cmdReCapametry.RightToLeft = RightToLeft.No;
            _cmdReCapametry.Size = new Size(186, 27);
            _cmdReCapametry.TabIndex = 12;
            _cmdReCapametry.Text = "ظرفیت سنجی مجدد";
            _cmdReCapametry.UseVisualStyleBackColor = false;
            // 
            // frmMPSList
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _cmdExit;
            ClientSize = new Size(841, 424);
            Controls.Add(_cmdReCapametry);
            Controls.Add(_cmdCapametryShow);
            Controls.Add(Panel2);
            Controls.Add(StatusStrip1);
            Controls.Add(Panel1);
            Name = "frmMPSList";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "  برنامه ریزی سالیانه";
            StatusStrip1.ResumeLayout(false);
            StatusStrip1.PerformLayout();
            Panel1.ResumeLayout(false);
            Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_dgList).EndInit();
            Panel2.ResumeLayout(false);
            Load += new EventHandler(frmRecordsLists_Load);
            FormClosing += new FormClosingEventHandler(frmRecordsLists_FormClosing);
            Resize += new EventHandler(frmRecordsLists_Resize);
            ResumeLayout(false);
            PerformLayout();
        }

        internal StatusStrip StatusStrip1;
        internal ToolStripStatusLabel tsslSearchMode;
        internal ToolStripStatusLabel ToolStripStatusLabel1;
        internal ToolStripStatusLabel ToolStripStatusLabel2;
        internal ToolStripStatusLabel ToolStripStatusLabel3;
        internal ToolStripStatusLabel ToolStripStatusLabel4;
        internal ToolStripStatusLabel tsslRecNo;
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

        internal System.Windows.Forms.Panel Panel1;
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
                }

                _dgList = value;
                if (_dgList != null)
                {
                    _dgList.ColumnWidthChanged += dgList_ColumnWidthChanged;
                    _dgList.RowEnter += dgList_RowEnter;
                    _dgList.CellFormatting += dgList_CellFormatting;
                }
            }
        }

        internal System.Windows.Forms.Panel Panel2;
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

        private System.Windows.Forms.Button _cmdCapametryShow;

        internal System.Windows.Forms.Button cmdCapametryShow
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdCapametryShow;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdCapametryShow != null)
                {
                    _cmdCapametryShow.Click -= cmdCapametryShow_Click;
                }

                _cmdCapametryShow = value;
                if (_cmdCapametryShow != null)
                {
                    _cmdCapametryShow.Click += cmdCapametryShow_Click;
                }
            }
        }

        private System.Windows.Forms.Button _cmdReCapametry;

        internal System.Windows.Forms.Button cmdReCapametry
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdReCapametry;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdReCapametry != null)
                {
                    _cmdReCapametry.Click -= cmdCapametry_Click;
                }

                _cmdReCapametry = value;
                if (_cmdReCapametry != null)
                {
                    _cmdReCapametry.Click += cmdCapametry_Click;
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