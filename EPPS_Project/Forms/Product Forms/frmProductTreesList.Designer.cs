using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmProductTreesList : Form
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
            txtSearch2 = new TextBox();
            Panel1 = new System.Windows.Forms.Panel();
            cbFilter3 = new ComboBox();
            cbFilter2 = new ComboBox();
            cbFilter1 = new ComboBox();
            txtSearch3 = new TextBox();
            txtSearch1 = new TextBox();
            _dgList = new DataGridView();
            _dgList.ColumnWidthChanged += new DataGridViewColumnEventHandler(dgList_ColumnWidthChanged);
            _dgList.RowEnter += new DataGridViewCellEventHandler(dgList_RowEnter);
            _dgList.CellFormatting += new DataGridViewCellFormattingEventHandler(dgList_CellFormatting);
            Panel3 = new System.Windows.Forms.Panel();
            _cbProductName = new ComboBox();
            _cbProductName.SelectionChangeCommitted += new EventHandler(cbProductName_SelectionChangeCommitted);
            Label1 = new Label();
            Label4 = new Label();
            _txtProductCode = new TextBox();
            _txtProductCode.KeyPress += new KeyPressEventHandler(txtProductCode_KeyPress);
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
            cmdShow = new System.Windows.Forms.Button();
            _cmdCopy = new System.Windows.Forms.Button();
            _cmdCopy.Click += new EventHandler(cmdCopy_Click);
            _cmdTreeDetails = new System.Windows.Forms.Button();
            _cmdTreeDetails.Click += new EventHandler(cmdTreeDetails_Click);
            StatusStrip1.SuspendLayout();
            Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgList).BeginInit();
            Panel3.SuspendLayout();
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
            StatusStrip1.Size = new Size(744, 22);
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
            txtSearch2.BorderStyle = BorderStyle.FixedSingle;
            txtSearch2.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtSearch2.Location = new Point(29, 91);
            txtSearch2.Name = "txtSearch2";
            txtSearch2.RightToLeft = RightToLeft.Yes;
            txtSearch2.Size = new Size(100, 22);
            txtSearch2.TabIndex = 6;
            txtSearch2.TextAlign = HorizontalAlignment.Center;
            txtSearch2.Visible = false;
            // 
            // Panel1
            // 
            Panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            Panel1.BorderStyle = BorderStyle.Fixed3D;
            Panel1.Controls.Add(cbFilter3);
            Panel1.Controls.Add(cbFilter2);
            Panel1.Controls.Add(cbFilter1);
            Panel1.Controls.Add(txtSearch2);
            Panel1.Controls.Add(txtSearch3);
            Panel1.Controls.Add(txtSearch1);
            Panel1.Controls.Add(_dgList);
            Panel1.Location = new Point(5, 104);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(732, 258);
            Panel1.TabIndex = 4;
            // 
            // cbFilter3
            // 
            cbFilter3.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cbFilter3.FormattingEnabled = true;
            cbFilter3.Location = new Point(135, 120);
            cbFilter3.Name = "cbFilter3";
            cbFilter3.Size = new Size(100, 21);
            cbFilter3.TabIndex = 57;
            cbFilter3.Visible = false;
            // 
            // cbFilter2
            // 
            cbFilter2.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cbFilter2.FormattingEnabled = true;
            cbFilter2.Location = new Point(135, 92);
            cbFilter2.Name = "cbFilter2";
            cbFilter2.Size = new Size(100, 21);
            cbFilter2.TabIndex = 56;
            cbFilter2.Visible = false;
            // 
            // cbFilter1
            // 
            cbFilter1.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cbFilter1.FormattingEnabled = true;
            cbFilter1.Location = new Point(135, 63);
            cbFilter1.Name = "cbFilter1";
            cbFilter1.Size = new Size(100, 21);
            cbFilter1.TabIndex = 55;
            cbFilter1.Visible = false;
            // 
            // txtSearch3
            // 
            txtSearch3.BorderStyle = BorderStyle.FixedSingle;
            txtSearch3.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtSearch3.Location = new Point(29, 119);
            txtSearch3.Name = "txtSearch3";
            txtSearch3.RightToLeft = RightToLeft.Yes;
            txtSearch3.Size = new Size(100, 22);
            txtSearch3.TabIndex = 7;
            txtSearch3.TextAlign = HorizontalAlignment.Center;
            txtSearch3.Visible = false;
            // 
            // txtSearch1
            // 
            txtSearch1.BorderStyle = BorderStyle.FixedSingle;
            txtSearch1.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtSearch1.Location = new Point(29, 63);
            txtSearch1.MaxLength = 10;
            txtSearch1.Name = "txtSearch1";
            txtSearch1.RightToLeft = RightToLeft.Yes;
            txtSearch1.Size = new Size(100, 22);
            txtSearch1.TabIndex = 5;
            txtSearch1.TextAlign = HorizontalAlignment.Center;
            txtSearch1.Visible = false;
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
            _dgList.Size = new Size(720, 245);
            _dgList.TabIndex = 1;
            // 
            // Panel3
            // 
            Panel3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Panel3.BackColor = Color.Beige;
            Panel3.BorderStyle = BorderStyle.FixedSingle;
            Panel3.Controls.Add(_cbProductName);
            Panel3.Controls.Add(Label1);
            Panel3.Controls.Add(Label4);
            Panel3.Controls.Add(_txtProductCode);
            Panel3.Location = new Point(7, 6);
            Panel3.Name = "Panel3";
            Panel3.Size = new Size(716, 39);
            Panel3.TabIndex = 7;
            // 
            // cbProductName
            // 
            _cbProductName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _cbProductName.DropDownStyle = ComboBoxStyle.DropDownList;
            _cbProductName.FlatStyle = FlatStyle.Flat;
            _cbProductName.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cbProductName.FormattingEnabled = true;
            _cbProductName.Location = new Point(56, 9);
            _cbProductName.Name = "_cbProductName";
            _cbProductName.Size = new Size(278, 21);
            _cbProductName.TabIndex = 165;
            // 
            // Label1
            // 
            Label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label1.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label1.Location = new Point(339, 10);
            Label1.Name = "Label1";
            Label1.RightToLeft = RightToLeft.Yes;
            Label1.Size = new Size(66, 18);
            Label1.TabIndex = 164;
            Label1.Text = "نام محصول:";
            // 
            // Label4
            // 
            Label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label4.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label4.Location = new Point(640, 9);
            Label4.Name = "Label4";
            Label4.RightToLeft = RightToLeft.Yes;
            Label4.Size = new Size(65, 18);
            Label4.TabIndex = 162;
            Label4.Text = "کد محصول:";
            // 
            // txtProductCode
            // 
            _txtProductCode.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _txtProductCode.BackColor = Color.White;
            _txtProductCode.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _txtProductCode.Location = new Point(429, 7);
            _txtProductCode.MaxLength = 25;
            _txtProductCode.Name = "_txtProductCode";
            _txtProductCode.Size = new Size(208, 23);
            _txtProductCode.TabIndex = 163;
            _txtProductCode.TextAlign = HorizontalAlignment.Center;
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
            Panel2.Controls.Add(Panel3);
            Panel2.Location = new Point(5, 5);
            Panel2.Name = "Panel2";
            Panel2.Size = new Size(732, 92);
            Panel2.TabIndex = 8;
            // 
            // cmdFilter
            // 
            _cmdFilter.BackColor = Color.Transparent;
            _cmdFilter.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdFilter.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdFilter.Location = new Point(195, 57);
            _cmdFilter.Name = "_cmdFilter";
            _cmdFilter.RightToLeft = RightToLeft.No;
            _cmdFilter.Size = new Size(85, 27);
            _cmdFilter.TabIndex = 4;
            _cmdFilter.Text = "فیلتر";
            _cmdFilter.TextAlign = ContentAlignment.MiddleRight;
            _cmdFilter.UseVisualStyleBackColor = false;
            _cmdFilter.Visible = false;
            // 
            // cmdExit
            // 
            _cmdExit.BackColor = Color.Transparent;
            _cmdExit.DialogResult = DialogResult.Cancel;
            _cmdExit.Font = new Font("Tahoma", 9.0f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdExit.ForeColor = Color.Red;
            _cmdExit.Location = new Point(7, 57);
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
            _cmdFind.Location = new Point(104, 57);
            _cmdFind.Name = "_cmdFind";
            _cmdFind.RightToLeft = RightToLeft.No;
            _cmdFind.Size = new Size(85, 27);
            _cmdFind.TabIndex = 3;
            _cmdFind.Text = "جستجو";
            _cmdFind.TextAlign = ContentAlignment.MiddleRight;
            _cmdFind.UseVisualStyleBackColor = false;
            _cmdFind.Visible = false;
            // 
            // cmdDelete
            // 
            _cmdDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _cmdDelete.BackColor = Color.Transparent;
            _cmdDelete.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdDelete.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdDelete.Location = new Point(638, 57);
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
            _cmdUpdate.Location = new Point(547, 57);
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
            _cmdInsert.Location = new Point(456, 57);
            _cmdInsert.Name = "_cmdInsert";
            _cmdInsert.RightToLeft = RightToLeft.No;
            _cmdInsert.Size = new Size(85, 27);
            _cmdInsert.TabIndex = 0;
            _cmdInsert.Text = "جدید";
            _cmdInsert.TextAlign = ContentAlignment.MiddleRight;
            _cmdInsert.UseVisualStyleBackColor = false;
            // 
            // cmdShow
            // 
            cmdShow.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cmdShow.BackColor = Color.Transparent;
            cmdShow.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cmdShow.ForeColor = Color.Blue;
            cmdShow.ImageAlign = ContentAlignment.MiddleLeft;
            cmdShow.Location = new Point(648, 370);
            cmdShow.Name = "cmdShow";
            cmdShow.RightToLeft = RightToLeft.No;
            cmdShow.Size = new Size(85, 27);
            cmdShow.TabIndex = 10;
            cmdShow.Text = "نمایش";
            cmdShow.TextAlign = ContentAlignment.MiddleRight;
            cmdShow.UseVisualStyleBackColor = false;
            cmdShow.Visible = false;
            // 
            // cmdCopy
            // 
            _cmdCopy.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _cmdCopy.BackColor = Color.Transparent;
            _cmdCopy.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdCopy.ForeColor = Color.Blue;
            _cmdCopy.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdCopy.Location = new Point(212, 370);
            _cmdCopy.Name = "_cmdCopy";
            _cmdCopy.RightToLeft = RightToLeft.No;
            _cmdCopy.Size = new Size(85, 27);
            _cmdCopy.TabIndex = 9;
            _cmdCopy.Text = "کپی";
            _cmdCopy.TextAlign = ContentAlignment.MiddleRight;
            _cmdCopy.UseVisualStyleBackColor = false;
            // 
            // cmdTreeDetails
            // 
            _cmdTreeDetails.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _cmdTreeDetails.BackColor = Color.Transparent;
            _cmdTreeDetails.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdTreeDetails.ForeColor = Color.Blue;
            _cmdTreeDetails.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdTreeDetails.Location = new Point(13, 370);
            _cmdTreeDetails.Name = "_cmdTreeDetails";
            _cmdTreeDetails.RightToLeft = RightToLeft.No;
            _cmdTreeDetails.Size = new Size(186, 27);
            _cmdTreeDetails.TabIndex = 11;
            _cmdTreeDetails.Text = "مشخصات اجزای درخت محصول";
            _cmdTreeDetails.UseVisualStyleBackColor = false;
            // 
            // frmProductTreesList
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _cmdExit;
            ClientSize = new Size(744, 424);
            Controls.Add(_cmdTreeDetails);
            Controls.Add(cmdShow);
            Controls.Add(_cmdCopy);
            Controls.Add(StatusStrip1);
            Controls.Add(Panel1);
            Controls.Add(Panel2);
            Name = "frmProductTreesList";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " مشخصات درخت محصول";
            StatusStrip1.ResumeLayout(false);
            StatusStrip1.PerformLayout();
            Panel1.ResumeLayout(false);
            Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_dgList).EndInit();
            Panel3.ResumeLayout(false);
            Panel3.PerformLayout();
            Panel2.ResumeLayout(false);
            Load += new EventHandler(frmProductTreesList_Load);
            FormClosing += new FormClosingEventHandler(frmProductTreesList_FormClosing);
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
        internal TextBox txtSearch2;
        internal System.Windows.Forms.Panel Panel1;
        internal TextBox txtSearch3;
        internal TextBox txtSearch1;
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

        internal System.Windows.Forms.Panel Panel3;
        internal Label Label4;
        private TextBox _txtProductCode;

        internal TextBox txtProductCode
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtProductCode;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtProductCode != null)
                {
                    _txtProductCode.KeyPress -= txtProductCode_KeyPress;
                }

                _txtProductCode = value;
                if (_txtProductCode != null)
                {
                    _txtProductCode.KeyPress += txtProductCode_KeyPress;
                }
            }
        }

        internal Label Label1;
        private ComboBox _cbProductName;

        internal ComboBox cbProductName
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cbProductName;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cbProductName != null)
                {
                    _cbProductName.SelectionChangeCommitted -= cbProductName_SelectionChangeCommitted;
                }

                _cbProductName = value;
                if (_cbProductName != null)
                {
                    _cbProductName.SelectionChangeCommitted += cbProductName_SelectionChangeCommitted;
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

        internal System.Windows.Forms.Button cmdShow;
        private System.Windows.Forms.Button _cmdCopy;

        internal System.Windows.Forms.Button cmdCopy
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdCopy;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdCopy != null)
                {
                    _cmdCopy.Click -= cmdCopy_Click;
                }

                _cmdCopy = value;
                if (_cmdCopy != null)
                {
                    _cmdCopy.Click += cmdCopy_Click;
                }
            }
        }

        private System.Windows.Forms.Button _cmdTreeDetails;

        internal System.Windows.Forms.Button cmdTreeDetails
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdTreeDetails;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdTreeDetails != null)
                {
                    _cmdTreeDetails.Click -= cmdTreeDetails_Click;
                }

                _cmdTreeDetails = value;
                if (_cmdTreeDetails != null)
                {
                    _cmdTreeDetails.Click += cmdTreeDetails_Click;
                }
            }
        }

        internal ComboBox cbFilter3;
        internal ComboBox cbFilter2;
        internal ComboBox cbFilter1;
    }
}