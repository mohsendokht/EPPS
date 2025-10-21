using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmProductionConfilicts : Form
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
            _dgList = new DataGridView();
            _dgList.KeyDown += new KeyEventHandler(dgList_KeyDown);
            _dgList.CellFormatting += new DataGridViewCellFormattingEventHandler(dgList_CellFormatting);
            Panel2 = new System.Windows.Forms.Panel();
            _cmdPrintPreview = new System.Windows.Forms.Button();
            _cmdPrintPreview.Click += new EventHandler(cmdPrintPreview_Click);
            _cmdExit = new System.Windows.Forms.Button();
            _cmdExit.Click += new EventHandler(cmdExit_Click);
            OpCode = new DataGridViewTextBoxColumn();
            OpName = new DataGridViewTextBoxColumn();
            SubbatchCode = new DataGridViewTextBoxColumn();
            Errors = new DataGridViewTextBoxColumn();
            Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgList).BeginInit();
            Panel2.SuspendLayout();
            SuspendLayout();
            // 
            // Panel1
            // 
            Panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            Panel1.BorderStyle = BorderStyle.Fixed3D;
            Panel1.Controls.Add(_dgList);
            Panel1.Location = new Point(6, 64);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(861, 371);
            Panel1.TabIndex = 5;
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
            _dgList.Columns.AddRange(new DataGridViewColumn[] { OpCode, OpName, SubbatchCode, Errors });
            DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
            DataGridViewCellStyle3.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            DataGridViewCellStyle3.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
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
            _dgList.RowHeadersWidth = 70;
            _dgList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle5.BackColor = SystemColors.Info;
            DataGridViewCellStyle5.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle5.ForeColor = Color.Black;
            DataGridViewCellStyle5.SelectionBackColor = Color.RoyalBlue;
            DataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            _dgList.RowsDefaultCellStyle = DataGridViewCellStyle5;
            _dgList.RowTemplate.Height = 30;
            _dgList.RowTemplate.Resizable = DataGridViewTriState.True;
            _dgList.Size = new Size(849, 358);
            _dgList.TabIndex = 1;
            // 
            // Panel2
            // 
            Panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Panel2.BackColor = SystemColors.Control;
            Panel2.BorderStyle = BorderStyle.FixedSingle;
            Panel2.Controls.Add(_cmdPrintPreview);
            Panel2.Controls.Add(_cmdExit);
            Panel2.Location = new Point(6, 5);
            Panel2.Name = "Panel2";
            Panel2.Size = new Size(861, 53);
            Panel2.TabIndex = 11;
            // 
            // cmdPrintPreview
            // 
            _cmdPrintPreview.BackColor = Color.Transparent;
            _cmdPrintPreview.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdPrintPreview.ForeColor = Color.Blue;
            _cmdPrintPreview.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdPrintPreview.Location = new Point(126, 10);
            _cmdPrintPreview.Name = "_cmdPrintPreview";
            _cmdPrintPreview.RightToLeft = RightToLeft.No;
            _cmdPrintPreview.Size = new Size(115, 27);
            _cmdPrintPreview.TabIndex = 11;
            _cmdPrintPreview.Text = "پیش نمایش چاپ";
            _cmdPrintPreview.UseVisualStyleBackColor = false;
            // 
            // cmdExit
            // 
            _cmdExit.BackColor = Color.Transparent;
            _cmdExit.DialogResult = DialogResult.Cancel;
            _cmdExit.Font = new Font("Tahoma", 9.0f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdExit.ForeColor = Color.Red;
            _cmdExit.Location = new Point(20, 10);
            _cmdExit.Name = "_cmdExit";
            _cmdExit.Size = new Size(95, 27);
            _cmdExit.TabIndex = 5;
            _cmdExit.Text = "خروج";
            _cmdExit.UseVisualStyleBackColor = false;
            // 
            // OpCode
            // 
            OpCode.HeaderText = "کد عملیات";
            OpCode.Name = "OpCode";
            OpCode.ReadOnly = true;
            OpCode.Width = 150;
            // 
            // OpName
            // 
            OpName.HeaderText = "شرح عملیات";
            OpName.Name = "OpName";
            OpName.ReadOnly = true;
            OpName.Width = 225;
            // 
            // SubbatchCode
            // 
            SubbatchCode.HeaderText = "کد ساب بچ";
            SubbatchCode.Name = "SubbatchCode";
            SubbatchCode.ReadOnly = true;
            SubbatchCode.Width = 170;
            // 
            // Errors
            // 
            Errors.HeaderText = "مغایرت(ها)";
            Errors.Name = "Errors";
            Errors.ReadOnly = true;
            Errors.Width = 230;
            // 
            // frmProductionConfilicts
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _cmdExit;
            ClientSize = new Size(873, 441);
            Controls.Add(Panel2);
            Controls.Add(Panel1);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Name = "frmProductionConfilicts";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " لیست مغایرتهای تولید";
            Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_dgList).EndInit();
            Panel2.ResumeLayout(false);
            FormClosing += new FormClosingEventHandler(frmProductionConfilicts_FormClosing);
            Load += new EventHandler(frmProductionConfilicts_Load);
            ResumeLayout(false);
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
                    _dgList.CellFormatting -= dgList_CellFormatting;
                }

                _dgList = value;
                if (_dgList != null)
                {
                    _dgList.KeyDown += dgList_KeyDown;
                    _dgList.CellFormatting += dgList_CellFormatting;
                }
            }
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

        private System.Windows.Forms.Button _cmdPrintPreview;

        internal System.Windows.Forms.Button cmdPrintPreview
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdPrintPreview;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdPrintPreview != null)
                {
                    _cmdPrintPreview.Click -= cmdPrintPreview_Click;
                }

                _cmdPrintPreview = value;
                if (_cmdPrintPreview != null)
                {
                    _cmdPrintPreview.Click += cmdPrintPreview_Click;
                }
            }
        }

        internal DataGridViewTextBoxColumn OpCode;
        internal DataGridViewTextBoxColumn OpName;
        internal DataGridViewTextBoxColumn SubbatchCode;
        internal DataGridViewTextBoxColumn Errors;
    }
}