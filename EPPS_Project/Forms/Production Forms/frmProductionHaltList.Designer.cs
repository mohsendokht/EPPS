using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmProductionHaltList : Form
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
            _cmdExit = new System.Windows.Forms.Button();
            _cmdExit.Click += new EventHandler(cmdExit_Click);
            Panel2 = new System.Windows.Forms.Panel();
            _cmdDelete = new System.Windows.Forms.Button();
            _cmdDelete.Click += new EventHandler(cmdInsert_Click);
            _cmdUpdate = new System.Windows.Forms.Button();
            _cmdUpdate.Click += new EventHandler(cmdInsert_Click);
            _cmdInsert = new System.Windows.Forms.Button();
            _cmdInsert.Click += new EventHandler(cmdInsert_Click);
            dgList = new DataGridView();
            gbConditions = new GroupBox();
            lblMachine = new Label();
            lblOperation = new Label();
            lblSubbatch = new Label();
            lblOperator = new Label();
            Label2 = new Label();
            Label4 = new Label();
            Label3 = new Label();
            Label1 = new Label();
            Panel1 = new System.Windows.Forms.Panel();
            Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgList).BeginInit();
            gbConditions.SuspendLayout();
            Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // cmdExit
            // 
            _cmdExit.BackColor = Color.Transparent;
            _cmdExit.DialogResult = DialogResult.Cancel;
            _cmdExit.Font = new Font("Tahoma", 9.0f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdExit.ForeColor = Color.Red;
            _cmdExit.Location = new Point(13, 10);
            _cmdExit.Name = "_cmdExit";
            _cmdExit.Size = new Size(95, 27);
            _cmdExit.TabIndex = 5;
            _cmdExit.Text = "خروج";
            _cmdExit.UseVisualStyleBackColor = false;
            // 
            // Panel2
            // 
            Panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Panel2.BackColor = SystemColors.Control;
            Panel2.BorderStyle = BorderStyle.FixedSingle;
            Panel2.Controls.Add(_cmdExit);
            Panel2.Controls.Add(_cmdDelete);
            Panel2.Controls.Add(_cmdUpdate);
            Panel2.Controls.Add(_cmdInsert);
            Panel2.Location = new Point(6, 121);
            Panel2.Name = "Panel2";
            Panel2.Size = new Size(605, 48);
            Panel2.TabIndex = 13;
            // 
            // cmdDelete
            // 
            _cmdDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _cmdDelete.BackColor = Color.Transparent;
            _cmdDelete.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdDelete.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdDelete.Location = new Point(506, 10);
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
            _cmdUpdate.Location = new Point(415, 10);
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
            _cmdInsert.Location = new Point(324, 10);
            _cmdInsert.Name = "_cmdInsert";
            _cmdInsert.RightToLeft = RightToLeft.No;
            _cmdInsert.Size = new Size(85, 27);
            _cmdInsert.TabIndex = 0;
            _cmdInsert.Text = "جدید";
            _cmdInsert.TextAlign = ContentAlignment.MiddleRight;
            _cmdInsert.UseVisualStyleBackColor = false;
            // 
            // dgList
            // 
            dgList.AllowUserToAddRows = false;
            dgList.AllowUserToDeleteRows = false;
            DataGridViewCellStyle1.BackColor = Color.White;
            dgList.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1;
            dgList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            dgList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgList.BackgroundColor = SystemColors.ControlDark;
            DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle2.BackColor = SystemColors.Control;
            DataGridViewCellStyle2.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2;
            dgList.ColumnHeadersHeight = 30;
            dgList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
            DataGridViewCellStyle3.BackColor = SystemColors.Info;
            DataGridViewCellStyle3.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            DataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgList.DefaultCellStyle = DataGridViewCellStyle3;
            dgList.Location = new Point(6, 7);
            dgList.MultiSelect = false;
            dgList.Name = "dgList";
            dgList.ReadOnly = true;
            dgList.RowHeadersWidth = 30;
            dgList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle4.BackColor = SystemColors.Info;
            DataGridViewCellStyle4.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            DataGridViewCellStyle4.ForeColor = Color.Black;
            DataGridViewCellStyle4.SelectionBackColor = Color.RoyalBlue;
            dgList.RowsDefaultCellStyle = DataGridViewCellStyle4;
            dgList.RowTemplate.Resizable = DataGridViewTriState.True;
            dgList.Size = new Size(593, 201);
            dgList.TabIndex = 1;
            // 
            // gbConditions
            // 
            gbConditions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbConditions.Controls.Add(lblMachine);
            gbConditions.Controls.Add(lblOperation);
            gbConditions.Controls.Add(lblSubbatch);
            gbConditions.Controls.Add(lblOperator);
            gbConditions.Controls.Add(Label2);
            gbConditions.Controls.Add(Label4);
            gbConditions.Controls.Add(Label3);
            gbConditions.Controls.Add(Label1);
            gbConditions.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            gbConditions.Location = new Point(6, 1);
            gbConditions.Name = "gbConditions";
            gbConditions.Size = new Size(605, 115);
            gbConditions.TabIndex = 12;
            gbConditions.TabStop = false;
            // 
            // lblMachine
            // 
            lblMachine.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblMachine.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblMachine.ForeColor = Color.Blue;
            lblMachine.Location = new Point(63, 88);
            lblMachine.Name = "lblMachine";
            lblMachine.RightToLeft = RightToLeft.Yes;
            lblMachine.Size = new Size(411, 20);
            lblMachine.TabIndex = 237;
            lblMachine.Text = "#";
            lblMachine.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblOperation
            // 
            lblOperation.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblOperation.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblOperation.ForeColor = Color.Blue;
            lblOperation.Location = new Point(63, 63);
            lblOperation.Name = "lblOperation";
            lblOperation.RightToLeft = RightToLeft.Yes;
            lblOperation.Size = new Size(411, 20);
            lblOperation.TabIndex = 236;
            lblOperation.Text = "#";
            lblOperation.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblSubbatch
            // 
            lblSubbatch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSubbatch.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblSubbatch.ForeColor = Color.Blue;
            lblSubbatch.Location = new Point(63, 38);
            lblSubbatch.Name = "lblSubbatch";
            lblSubbatch.RightToLeft = RightToLeft.Yes;
            lblSubbatch.Size = new Size(411, 20);
            lblSubbatch.TabIndex = 235;
            lblSubbatch.Text = "#";
            lblSubbatch.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblOperator
            // 
            lblOperator.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblOperator.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblOperator.ForeColor = Color.Blue;
            lblOperator.Location = new Point(63, 13);
            lblOperator.Name = "lblOperator";
            lblOperator.RightToLeft = RightToLeft.Yes;
            lblOperator.Size = new Size(411, 20);
            lblOperator.TabIndex = 234;
            lblOperator.Text = "#";
            lblOperator.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label2
            // 
            Label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label2.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label2.Location = new Point(480, 88);
            Label2.Name = "Label2";
            Label2.RightToLeft = RightToLeft.Yes;
            Label2.Size = new Size(108, 20);
            Label2.TabIndex = 233;
            Label2.Text = "ماشین انجام دهنده";
            Label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label4
            // 
            Label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label4.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label4.Location = new Point(480, 38);
            Label4.Name = "Label4";
            Label4.RightToLeft = RightToLeft.Yes;
            Label4.Size = new Size(108, 20);
            Label4.TabIndex = 232;
            Label4.Text = " کد ساب بچ";
            Label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label3
            // 
            Label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label3.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label3.Location = new Point(480, 63);
            Label3.Name = "Label3";
            Label3.RightToLeft = RightToLeft.Yes;
            Label3.Size = new Size(108, 20);
            Label3.TabIndex = 231;
            Label3.Text = "عملیات متوقف شده";
            Label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label1
            // 
            Label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label1.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label1.Location = new Point(480, 13);
            Label1.Name = "Label1";
            Label1.RightToLeft = RightToLeft.Yes;
            Label1.Size = new Size(108, 20);
            Label1.TabIndex = 230;
            Label1.Text = "اپراتور انجام دهنده";
            Label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Panel1
            // 
            Panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            Panel1.BorderStyle = BorderStyle.Fixed3D;
            Panel1.Controls.Add(dgList);
            Panel1.Location = new Point(6, 173);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(605, 214);
            Panel1.TabIndex = 11;
            // 
            // frmProductionHaltList
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(617, 393);
            Controls.Add(Panel2);
            Controls.Add(gbConditions);
            Controls.Add(Panel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmProductionHaltList";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " لیست توقفات عملیات";
            Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgList).EndInit();
            gbConditions.ResumeLayout(false);
            Panel1.ResumeLayout(false);
            Load += new EventHandler(frmRealProduction_Load);
            ResumeLayout(false);
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

        internal System.Windows.Forms.Panel Panel2;
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
                    _cmdDelete.Click -= cmdInsert_Click;
                }

                _cmdDelete = value;
                if (_cmdDelete != null)
                {
                    _cmdDelete.Click += cmdInsert_Click;
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
                    _cmdUpdate.Click -= cmdInsert_Click;
                }

                _cmdUpdate = value;
                if (_cmdUpdate != null)
                {
                    _cmdUpdate.Click += cmdInsert_Click;
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
                    _cmdInsert.Click -= cmdInsert_Click;
                }

                _cmdInsert = value;
                if (_cmdInsert != null)
                {
                    _cmdInsert.Click += cmdInsert_Click;
                }
            }
        }

        internal DataGridView dgList;
        internal GroupBox gbConditions;
        internal System.Windows.Forms.Panel Panel1;
        internal Label lblMachine;
        internal Label lblOperation;
        internal Label lblSubbatch;
        internal Label lblOperator;
        internal Label Label2;
        internal Label Label4;
        internal Label Label3;
        internal Label Label1;
    }
}