using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmOperationMaterial : Form
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
            GroupBox1 = new GroupBox();
            _dgPreItems = new DataGridView();
            _dgPreItems.CellClick += new DataGridViewCellEventHandler(dgPreItems_CellClick);
            _dgPreItems.DataError += new DataGridViewDataErrorEventHandler(dgPreItems_DataError);
            Column1 = new DataGridViewComboBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewComboBoxColumn();
            Panel1 = new System.Windows.Forms.Panel();
            _cmdSave = new System.Windows.Forms.Button();
            _cmdSave.Click += new EventHandler(cmdSave_Click);
            _cmdExit = new System.Windows.Forms.Button();
            _cmdExit.Click += new EventHandler(cmdExit_Click);
            GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgPreItems).BeginInit();
            Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // GroupBox1
            // 
            GroupBox1.Controls.Add(_dgPreItems);
            GroupBox1.Location = new Point(12, 12);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(646, 313);
            GroupBox1.TabIndex = 170;
            GroupBox1.TabStop = false;
            // 
            // dgPreItems
            // 
            _dgPreItems.AllowUserToResizeColumns = false;
            _dgPreItems.AllowUserToResizeRows = false;
            DataGridViewCellStyle1.BackColor = Color.White;
            _dgPreItems.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1;
            _dgPreItems.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            _dgPreItems.BackgroundColor = Color.WhiteSmoke;
            _dgPreItems.ColumnHeadersHeight = 30;
            _dgPreItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            _dgPreItems.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3 });
            _dgPreItems.Location = new Point(7, 20);
            _dgPreItems.Name = "_dgPreItems";
            _dgPreItems.RowHeadersWidth = 30;
            _dgPreItems.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            DataGridViewCellStyle2.BackColor = SystemColors.Info;
            _dgPreItems.RowsDefaultCellStyle = DataGridViewCellStyle2;
            _dgPreItems.RowTemplate.DefaultCellStyle.BackColor = SystemColors.Info;
            _dgPreItems.RowTemplate.DefaultCellStyle.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _dgPreItems.Size = new Size(633, 285);
            _dgPreItems.TabIndex = 0;
            _dgPreItems.TabStop = false;
            // 
            // Column1
            // 
            Column1.HeaderText = "نام مواد/قطعه";
            Column1.Name = "Column1";
            Column1.Resizable = DataGridViewTriState.True;
            Column1.SortMode = DataGridViewColumnSortMode.Automatic;
            Column1.Width = 300;
            // 
            // Column2
            // 
            Column2.HeaderText = "مقدار/ تعداد";
            Column2.Name = "Column2";
            Column2.Width = 120;
            // 
            // Column3
            // 
            Column3.HeaderText = "واحد سنجش تولید";
            Column3.Name = "Column3";
            Column3.Resizable = DataGridViewTriState.True;
            Column3.SortMode = DataGridViewColumnSortMode.Automatic;
            Column3.Width = 180;
            // 
            // Panel1
            // 
            Panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Panel1.BackColor = SystemColors.Control;
            Panel1.BorderStyle = BorderStyle.FixedSingle;
            Panel1.Controls.Add(_cmdSave);
            Panel1.Controls.Add(_cmdExit);
            Panel1.Location = new Point(12, 338);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(646, 48);
            Panel1.TabIndex = 175;
            // 
            // cmdSave
            // 
            _cmdSave.BackColor = Color.Transparent;
            _cmdSave.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdSave.ForeColor = Color.Black;
            _cmdSave.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdSave.Location = new Point(338, 10);
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
            _cmdExit.ForeColor = Color.Red;
            _cmdExit.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdExit.Location = new Point(217, 10);
            _cmdExit.Name = "_cmdExit";
            _cmdExit.RightToLeft = RightToLeft.No;
            _cmdExit.Size = new Size(90, 28);
            _cmdExit.TabIndex = 5;
            _cmdExit.Text = "انصراف";
            _cmdExit.TextAlign = ContentAlignment.MiddleRight;
            _cmdExit.UseVisualStyleBackColor = false;
            // 
            // frmOperationMaterial
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(668, 393);
            Controls.Add(GroupBox1);
            Controls.Add(Panel1);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmOperationMaterial";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "  مشخصات مواد/قطعه وارده به عملیات";
            GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_dgPreItems).EndInit();
            Panel1.ResumeLayout(false);
            Load += new EventHandler(frmOperationMaterial_Load);
            FormClosing += new FormClosingEventHandler(frmOperationMaterial_FormClosing);
            ResumeLayout(false);
        }

        internal GroupBox GroupBox1;
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

        private DataGridView _dgPreItems;

        internal DataGridView dgPreItems
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _dgPreItems;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_dgPreItems != null)
                {
                    _dgPreItems.CellClick -= dgPreItems_CellClick;
                    _dgPreItems.DataError -= dgPreItems_DataError;
                }

                _dgPreItems = value;
                if (_dgPreItems != null)
                {
                    _dgPreItems.CellClick += dgPreItems_CellClick;
                    _dgPreItems.DataError += dgPreItems_DataError;
                }
            }
        }

        internal DataGridViewComboBoxColumn Column1;
        internal DataGridViewTextBoxColumn Column2;
        internal DataGridViewComboBoxColumn Column3;
    }
}