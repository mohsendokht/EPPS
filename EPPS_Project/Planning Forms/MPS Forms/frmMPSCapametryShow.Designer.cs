using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmMPSCapametryShow : Form
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
            components = new System.ComponentModel.Container();
            var DataGridViewCellStyle13 = new DataGridViewCellStyle();
            var DataGridViewCellStyle14 = new DataGridViewCellStyle();
            var DataGridViewCellStyle15 = new DataGridViewCellStyle();
            var DataGridViewCellStyle16 = new DataGridViewCellStyle();
            var DataGridViewCellStyle17 = new DataGridViewCellStyle();
            var DataGridViewCellStyle18 = new DataGridViewCellStyle();
            var DataGridViewCellStyle19 = new DataGridViewCellStyle();
            var DataGridViewCellStyle20 = new DataGridViewCellStyle();
            var DataGridViewCellStyle21 = new DataGridViewCellStyle();
            var DataGridViewCellStyle22 = new DataGridViewCellStyle();
            var DataGridViewCellStyle23 = new DataGridViewCellStyle();
            var DataGridViewCellStyle24 = new DataGridViewCellStyle();
            Panel2 = new System.Windows.Forms.Panel();
            _cmdShow = new System.Windows.Forms.Button();
            _cmdShow.Click += new EventHandler(cmdShow_Click);
            cbProductName = new ComboBox();
            Label1 = new Label();
            _cmdExit = new System.Windows.Forms.Button();
            _cmdExit.Click += new EventHandler(cmdExit_Click);
            TabControl1 = new TabControl();
            TabPage1 = new TabPage();
            _dgMachines = new DataGridView();
            _dgMachines.KeyDown += new KeyEventHandler(dgMachines_KeyDown);
            TabPage2 = new TabPage();
            _dgPersonnels = new DataGridView();
            _dgPersonnels.KeyDown += new KeyEventHandler(dgPersonnels_KeyDown);
            TabPage3 = new TabPage();
            _dgMaterials = new DataGridView();
            _dgMaterials.KeyDown += new KeyEventHandler(dgMaterials_KeyDown);
            _tmrMachines = new Timer(components);
            _tmrMachines.Tick += new EventHandler(tmrMachines_Tick);
            _tmrPersonnels = new Timer(components);
            _tmrPersonnels.Tick += new EventHandler(tmrPersonnels_Tick);
            tmrMaterials = new Timer(components);
            Panel2.SuspendLayout();
            TabControl1.SuspendLayout();
            TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgMachines).BeginInit();
            TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgPersonnels).BeginInit();
            TabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgMaterials).BeginInit();
            SuspendLayout();
            // 
            // Panel2
            // 
            Panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Panel2.BackColor = SystemColors.Control;
            Panel2.BorderStyle = BorderStyle.FixedSingle;
            Panel2.Controls.Add(_cmdShow);
            Panel2.Controls.Add(cbProductName);
            Panel2.Controls.Add(Label1);
            Panel2.Controls.Add(_cmdExit);
            Panel2.Location = new Point(7, 5);
            Panel2.Name = "Panel2";
            Panel2.Size = new Size(856, 54);
            Panel2.TabIndex = 3;
            // 
            // cmdShow
            // 
            _cmdShow.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _cmdShow.BackColor = Color.Transparent;
            _cmdShow.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdShow.ForeColor = Color.Blue;
            _cmdShow.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdShow.Location = new Point(398, 10);
            _cmdShow.Name = "_cmdShow";
            _cmdShow.RightToLeft = RightToLeft.No;
            _cmdShow.Size = new Size(91, 27);
            _cmdShow.TabIndex = 168;
            _cmdShow.Text = "نمایش";
            _cmdShow.UseVisualStyleBackColor = false;
            // 
            // cbProductName
            // 
            cbProductName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbProductName.DropDownStyle = ComboBoxStyle.DropDownList;
            cbProductName.FlatStyle = FlatStyle.Flat;
            cbProductName.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cbProductName.FormattingEnabled = true;
            cbProductName.Location = new Point(497, 13);
            cbProductName.Name = "cbProductName";
            cbProductName.Size = new Size(278, 21);
            cbProductName.TabIndex = 167;
            // 
            // Label1
            // 
            Label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label1.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label1.ForeColor = Color.Black;
            Label1.Location = new Point(779, 13);
            Label1.Name = "Label1";
            Label1.RightToLeft = RightToLeft.Yes;
            Label1.Size = new Size(66, 17);
            Label1.TabIndex = 166;
            Label1.Text = "نام محصول:";
            // 
            // cmdExit
            // 
            _cmdExit.BackColor = Color.Transparent;
            _cmdExit.DialogResult = DialogResult.Cancel;
            _cmdExit.Font = new Font("Tahoma", 9.0f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdExit.ForeColor = Color.Red;
            _cmdExit.Location = new Point(12, 10);
            _cmdExit.Name = "_cmdExit";
            _cmdExit.Size = new Size(91, 27);
            _cmdExit.TabIndex = 5;
            _cmdExit.Text = "خروج";
            _cmdExit.UseVisualStyleBackColor = false;
            // 
            // TabControl1
            // 
            TabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            TabControl1.Controls.Add(TabPage1);
            TabControl1.Controls.Add(TabPage2);
            TabControl1.Controls.Add(TabPage3);
            TabControl1.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            TabControl1.Location = new Point(7, 67);
            TabControl1.Name = "TabControl1";
            TabControl1.RightToLeftLayout = true;
            TabControl1.SelectedIndex = 0;
            TabControl1.Size = new Size(856, 374);
            TabControl1.TabIndex = 4;
            // 
            // TabPage1
            // 
            TabPage1.BackColor = SystemColors.Control;
            TabPage1.Controls.Add(_dgMachines);
            TabPage1.Location = new Point(4, 22);
            TabPage1.Name = "TabPage1";
            TabPage1.Padding = new Padding(3);
            TabPage1.Size = new Size(848, 348);
            TabPage1.TabIndex = 0;
            TabPage1.Text = "ظرفیت سنجی ماشین آلات";
            // 
            // dgMachines
            // 
            _dgMachines.AllowUserToAddRows = false;
            _dgMachines.AllowUserToDeleteRows = false;
            _dgMachines.AllowUserToResizeRows = false;
            DataGridViewCellStyle13.BackColor = Color.White;
            _dgMachines.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle13;
            _dgMachines.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            _dgMachines.BackgroundColor = SystemColors.ControlDark;
            DataGridViewCellStyle14.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridViewCellStyle14.BackColor = SystemColors.Control;
            DataGridViewCellStyle14.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle14.ForeColor = SystemColors.WindowText;
            DataGridViewCellStyle14.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle14.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle14.WrapMode = DataGridViewTriState.False;
            _dgMachines.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle14;
            _dgMachines.ColumnHeadersHeight = 30;
            _dgMachines.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DataGridViewCellStyle15.Alignment = DataGridViewContentAlignment.MiddleRight;
            DataGridViewCellStyle15.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            DataGridViewCellStyle15.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle15.ForeColor = SystemColors.ControlText;
            DataGridViewCellStyle15.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle15.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle15.WrapMode = DataGridViewTriState.False;
            _dgMachines.DefaultCellStyle = DataGridViewCellStyle15;
            _dgMachines.Location = new Point(3, 5);
            _dgMachines.Name = "_dgMachines";
            _dgMachines.ReadOnly = true;
            _dgMachines.RowHeadersVisible = false;
            _dgMachines.RowHeadersWidth = 30;
            _dgMachines.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            DataGridViewCellStyle16.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridViewCellStyle16.BackColor = SystemColors.Info;
            DataGridViewCellStyle16.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle16.ForeColor = Color.Black;
            DataGridViewCellStyle16.SelectionBackColor = Color.RoyalBlue;
            _dgMachines.RowsDefaultCellStyle = DataGridViewCellStyle16;
            _dgMachines.RowTemplate.Resizable = DataGridViewTriState.True;
            _dgMachines.SelectionMode = DataGridViewSelectionMode.CellSelect;
            _dgMachines.Size = new Size(843, 340);
            _dgMachines.TabIndex = 2;
            // 
            // TabPage2
            // 
            TabPage2.BackColor = SystemColors.Control;
            TabPage2.Controls.Add(_dgPersonnels);
            TabPage2.Location = new Point(4, 22);
            TabPage2.Name = "TabPage2";
            TabPage2.Padding = new Padding(3);
            TabPage2.Size = new Size(848, 348);
            TabPage2.TabIndex = 1;
            TabPage2.Text = "ظرفیت سنجی پرسنل";
            // 
            // dgPersonnels
            // 
            _dgPersonnels.AllowUserToAddRows = false;
            _dgPersonnels.AllowUserToDeleteRows = false;
            _dgPersonnels.AllowUserToResizeRows = false;
            DataGridViewCellStyle17.BackColor = Color.White;
            _dgPersonnels.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle17;
            _dgPersonnels.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            _dgPersonnels.BackgroundColor = SystemColors.ControlDark;
            DataGridViewCellStyle18.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridViewCellStyle18.BackColor = SystemColors.Control;
            DataGridViewCellStyle18.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle18.ForeColor = SystemColors.WindowText;
            DataGridViewCellStyle18.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle18.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle18.WrapMode = DataGridViewTriState.False;
            _dgPersonnels.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle18;
            _dgPersonnels.ColumnHeadersHeight = 30;
            _dgPersonnels.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DataGridViewCellStyle19.Alignment = DataGridViewContentAlignment.MiddleRight;
            DataGridViewCellStyle19.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            DataGridViewCellStyle19.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle19.ForeColor = SystemColors.ControlText;
            DataGridViewCellStyle19.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle19.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle19.WrapMode = DataGridViewTriState.False;
            _dgPersonnels.DefaultCellStyle = DataGridViewCellStyle19;
            _dgPersonnels.Location = new Point(3, 5);
            _dgPersonnels.Name = "_dgPersonnels";
            _dgPersonnels.ReadOnly = true;
            _dgPersonnels.RowHeadersVisible = false;
            _dgPersonnels.RowHeadersWidth = 30;
            _dgPersonnels.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            DataGridViewCellStyle20.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle20.BackColor = SystemColors.Info;
            DataGridViewCellStyle20.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle20.ForeColor = Color.Black;
            DataGridViewCellStyle20.SelectionBackColor = Color.RoyalBlue;
            _dgPersonnels.RowsDefaultCellStyle = DataGridViewCellStyle20;
            _dgPersonnels.RowTemplate.Resizable = DataGridViewTriState.True;
            _dgPersonnels.SelectionMode = DataGridViewSelectionMode.CellSelect;
            _dgPersonnels.Size = new Size(843, 340);
            _dgPersonnels.TabIndex = 2;
            // 
            // TabPage3
            // 
            TabPage3.BackColor = SystemColors.Control;
            TabPage3.Controls.Add(_dgMaterials);
            TabPage3.Location = new Point(4, 22);
            TabPage3.Name = "TabPage3";
            TabPage3.Padding = new Padding(3);
            TabPage3.Size = new Size(848, 348);
            TabPage3.TabIndex = 2;
            TabPage3.Text = "ظرفیت سنجی مواد اولیه";
            // 
            // dgMaterials
            // 
            _dgMaterials.AllowUserToAddRows = false;
            _dgMaterials.AllowUserToDeleteRows = false;
            _dgMaterials.AllowUserToResizeRows = false;
            DataGridViewCellStyle21.BackColor = Color.White;
            _dgMaterials.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle21;
            _dgMaterials.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            _dgMaterials.BackgroundColor = SystemColors.ControlDark;
            DataGridViewCellStyle22.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle22.BackColor = SystemColors.Control;
            DataGridViewCellStyle22.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle22.ForeColor = SystemColors.WindowText;
            DataGridViewCellStyle22.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle22.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle22.WrapMode = DataGridViewTriState.False;
            _dgMaterials.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle22;
            _dgMaterials.ColumnHeadersHeight = 30;
            _dgMaterials.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DataGridViewCellStyle23.Alignment = DataGridViewContentAlignment.MiddleRight;
            DataGridViewCellStyle23.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            DataGridViewCellStyle23.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle23.ForeColor = SystemColors.ControlText;
            DataGridViewCellStyle23.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle23.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle23.WrapMode = DataGridViewTriState.False;
            _dgMaterials.DefaultCellStyle = DataGridViewCellStyle23;
            _dgMaterials.Location = new Point(3, 5);
            _dgMaterials.Name = "_dgMaterials";
            _dgMaterials.ReadOnly = true;
            _dgMaterials.RowHeadersVisible = false;
            _dgMaterials.RowHeadersWidth = 30;
            _dgMaterials.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            DataGridViewCellStyle24.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle24.BackColor = SystemColors.Info;
            DataGridViewCellStyle24.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle24.ForeColor = Color.Black;
            DataGridViewCellStyle24.SelectionBackColor = Color.RoyalBlue;
            _dgMaterials.RowsDefaultCellStyle = DataGridViewCellStyle24;
            _dgMaterials.RowTemplate.Resizable = DataGridViewTriState.True;
            _dgMaterials.SelectionMode = DataGridViewSelectionMode.CellSelect;
            _dgMaterials.Size = new Size(843, 340);
            _dgMaterials.TabIndex = 2;
            // 
            // tmrMachines
            // 
            // 
            // tmrPersonnels
            // 
            // 
            // frmMPSCapametryShow
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _cmdExit;
            ClientSize = new Size(870, 448);
            Controls.Add(TabControl1);
            Controls.Add(Panel2);
            Name = "frmMPSCapametryShow";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " نمایش ظرفیت سنجی MPS";
            Panel2.ResumeLayout(false);
            TabControl1.ResumeLayout(false);
            TabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_dgMachines).EndInit();
            TabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_dgPersonnels).EndInit();
            TabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_dgMaterials).EndInit();
            Load += new EventHandler(frmMPSCapametryShow_Load);
            FormClosing += new FormClosingEventHandler(frmMPSCapametryShow_FormClosing);
            ResumeLayout(false);
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

        internal ComboBox cbProductName;
        internal Label Label1;
        internal TabControl TabControl1;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal TabPage TabPage3;
        private DataGridView _dgMachines;

        internal DataGridView dgMachines
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _dgMachines;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_dgMachines != null)
                {
                    _dgMachines.KeyDown -= dgMachines_KeyDown;
                }

                _dgMachines = value;
                if (_dgMachines != null)
                {
                    _dgMachines.KeyDown += dgMachines_KeyDown;
                }
            }
        }

        private DataGridView _dgPersonnels;

        internal DataGridView dgPersonnels
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _dgPersonnels;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_dgPersonnels != null)
                {
                    _dgPersonnels.KeyDown -= dgPersonnels_KeyDown;
                }

                _dgPersonnels = value;
                if (_dgPersonnels != null)
                {
                    _dgPersonnels.KeyDown += dgPersonnels_KeyDown;
                }
            }
        }

        private DataGridView _dgMaterials;

        internal DataGridView dgMaterials
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _dgMaterials;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_dgMaterials != null)
                {
                    _dgMaterials.KeyDown -= dgMaterials_KeyDown;
                }

                _dgMaterials = value;
                if (_dgMaterials != null)
                {
                    _dgMaterials.KeyDown += dgMaterials_KeyDown;
                }
            }
        }

        private Timer _tmrMachines;

        internal Timer tmrMachines
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _tmrMachines;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_tmrMachines != null)
                {
                    _tmrMachines.Tick -= tmrMachines_Tick;
                }

                _tmrMachines = value;
                if (_tmrMachines != null)
                {
                    _tmrMachines.Tick += tmrMachines_Tick;
                }
            }
        }

        private Timer _tmrPersonnels;

        internal Timer tmrPersonnels
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _tmrPersonnels;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_tmrPersonnels != null)
                {
                    _tmrPersonnels.Tick -= tmrPersonnels_Tick;
                }

                _tmrPersonnels = value;
                if (_tmrPersonnels != null)
                {
                    _tmrPersonnels.Tick += tmrPersonnels_Tick;
                }
            }
        }

        internal Timer tmrMaterials;
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
    }
}