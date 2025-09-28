using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmPreOperation : Form
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
            _dgPreOperations = new DataGridView();
            _dgPreOperations.CellClick += new DataGridViewCellEventHandler(dgPreOperations_CellClick);
            _dgPreOperations.DataError += new DataGridViewDataErrorEventHandler(dgPreOperations_DataError);
            DataGridViewTextBoxColumn1 = new DataGridViewComboBoxColumn();
            DataGridViewTextBoxColumn2 = new DataGridViewComboBoxColumn();
            DataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn4 = new DataGridViewComboBoxColumn();
            Panel1 = new System.Windows.Forms.Panel();
            _cmdSave = new System.Windows.Forms.Button();
            _cmdSave.Click += new EventHandler(cmdSave_Click);
            _cmdExit = new System.Windows.Forms.Button();
            _cmdExit.Click += new EventHandler(cmdExit_Click);
            GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgPreOperations).BeginInit();
            Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // GroupBox1
            // 
            GroupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            GroupBox1.Controls.Add(_dgPreOperations);
            GroupBox1.Location = new Point(12, 12);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(644, 315);
            GroupBox1.TabIndex = 305;
            GroupBox1.TabStop = false;
            // 
            // dgPreOperations
            // 
            DataGridViewCellStyle1.BackColor = Color.White;
            _dgPreOperations.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1;
            _dgPreOperations.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            _dgPreOperations.BackgroundColor = Color.WhiteSmoke;
            _dgPreOperations.ColumnHeadersHeight = 30;
            _dgPreOperations.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            _dgPreOperations.Columns.AddRange(new DataGridViewColumn[] { DataGridViewTextBoxColumn1, DataGridViewTextBoxColumn2, DataGridViewTextBoxColumn3, DataGridViewTextBoxColumn4 });
            _dgPreOperations.Location = new Point(6, 20);
            _dgPreOperations.Name = "_dgPreOperations";
            _dgPreOperations.RowHeadersWidth = 30;
            _dgPreOperations.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            DataGridViewCellStyle2.BackColor = SystemColors.Info;
            DataGridViewCellStyle2.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _dgPreOperations.RowsDefaultCellStyle = DataGridViewCellStyle2;
            _dgPreOperations.RowTemplate.DefaultCellStyle.BackColor = SystemColors.Info;
            _dgPreOperations.RowTemplate.DefaultCellStyle.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _dgPreOperations.ShowRowErrors = false;
            _dgPreOperations.Size = new Size(632, 288);
            _dgPreOperations.TabIndex = 314;
            // 
            // DataGridViewTextBoxColumn1
            // 
            DataGridViewTextBoxColumn1.FillWeight = 200.0f;
            DataGridViewTextBoxColumn1.HeaderText = "عملیات پیشنیاز";
            DataGridViewTextBoxColumn1.MinimumWidth = 150;
            DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1";
            DataGridViewTextBoxColumn1.Resizable = DataGridViewTriState.False;
            DataGridViewTextBoxColumn1.Width = 300;
            // 
            // DataGridViewTextBoxColumn2
            // 
            DataGridViewTextBoxColumn2.FillWeight = 43.09283f;
            DataGridViewTextBoxColumn2.HeaderText = "نوع ارتباط";
            DataGridViewTextBoxColumn2.MinimumWidth = 80;
            DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2";
            DataGridViewTextBoxColumn2.Resizable = DataGridViewTriState.False;
            DataGridViewTextBoxColumn2.Width = 80;
            // 
            // DataGridViewTextBoxColumn3
            // 
            DataGridViewTextBoxColumn3.FillWeight = 73.93827f;
            DataGridViewTextBoxColumn3.HeaderText = "میزان تاخیر/ تقدم";
            DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3";
            DataGridViewTextBoxColumn3.Resizable = DataGridViewTriState.False;
            DataGridViewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.NotSortable;
            DataGridViewTextBoxColumn3.Width = 104;
            // 
            // DataGridViewTextBoxColumn4
            // 
            DataGridViewTextBoxColumn4.FillWeight = 82.96889f;
            DataGridViewTextBoxColumn4.HeaderText = "نوع واحد زمانی";
            DataGridViewTextBoxColumn4.MinimumWidth = 110;
            DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4";
            DataGridViewTextBoxColumn4.Resizable = DataGridViewTriState.False;
            DataGridViewTextBoxColumn4.Width = 116;
            // 
            // Panel1
            // 
            Panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Panel1.BackColor = SystemColors.Control;
            Panel1.BorderStyle = BorderStyle.FixedSingle;
            Panel1.Controls.Add(_cmdSave);
            Panel1.Controls.Add(_cmdExit);
            Panel1.Location = new Point(12, 339);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(644, 48);
            Panel1.TabIndex = 313;
            // 
            // cmdSave
            // 
            _cmdSave.BackColor = Color.Transparent;
            _cmdSave.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdSave.ForeColor = Color.Black;
            _cmdSave.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdSave.Location = new Point(341, 10);
            _cmdSave.Name = "_cmdSave";
            _cmdSave.RightToLeft = RightToLeft.No;
            _cmdSave.Size = new Size(90, 28);
            _cmdSave.TabIndex = 12;
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
            _cmdExit.Location = new Point(212, 10);
            _cmdExit.Name = "_cmdExit";
            _cmdExit.RightToLeft = RightToLeft.No;
            _cmdExit.Size = new Size(90, 28);
            _cmdExit.TabIndex = 14;
            _cmdExit.Text = "انصراف";
            _cmdExit.TextAlign = ContentAlignment.MiddleRight;
            _cmdExit.UseVisualStyleBackColor = false;
            // 
            // frmPreOperation
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
            Name = "frmPreOperation";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "  مشخصات عملیات پیشنیاز";
            GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_dgPreOperations).EndInit();
            Panel1.ResumeLayout(false);
            Load += new EventHandler(frmPreOperation_Load);
            FormClosing += new FormClosingEventHandler(frmPreOperation_FormClosing);
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

        private DataGridView _dgPreOperations;

        internal DataGridView dgPreOperations
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _dgPreOperations;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_dgPreOperations != null)
                {
                    _dgPreOperations.CellClick -= dgPreOperations_CellClick;
                    _dgPreOperations.DataError -= dgPreOperations_DataError;
                }

                _dgPreOperations = value;
                if (_dgPreOperations != null)
                {
                    _dgPreOperations.CellClick += dgPreOperations_CellClick;
                    _dgPreOperations.DataError += dgPreOperations_DataError;
                }
            }
        }

        internal DataGridViewComboBoxColumn DataGridViewTextBoxColumn1;
        internal DataGridViewComboBoxColumn DataGridViewTextBoxColumn2;
        internal DataGridViewTextBoxColumn DataGridViewTextBoxColumn3;
        internal DataGridViewComboBoxColumn DataGridViewTextBoxColumn4;
    }
}