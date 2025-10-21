using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmPlanning : Form
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
            this.ControlPlanItems = new System.Windows.Forms.CheckBox();
            this.lblWaiting = new System.Windows.Forms.Label();
            this.FunctionLabel = new System.Windows.Forms.Label();
            this._cmdPlanning = new System.Windows.Forms.Button();
            this._cmdExit = new System.Windows.Forms.Button();
            this.lblCurrentOperationCode = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.lblCurrentSubbatchCode = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Panel2 = new System.Windows.Forms.Panel();
            this._dgList = new System.Windows.Forms.DataGridView();
            this.Panel1.SuspendLayout();
            this.Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgList)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this.ControlPlanItems);
            this.Panel1.Controls.Add(this.lblWaiting);
            this.Panel1.Controls.Add(this.FunctionLabel);
            this.Panel1.Controls.Add(this._cmdPlanning);
            this.Panel1.Controls.Add(this._cmdExit);
            this.Panel1.Controls.Add(this.lblCurrentOperationCode);
            this.Panel1.Controls.Add(this.Label2);
            this.Panel1.Controls.Add(this.lblCurrentSubbatchCode);
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Location = new System.Drawing.Point(8, 475);
            this.Panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(989, 123);
            this.Panel1.TabIndex = 177;
            // 
            // ControlPlanItems
            // 
            this.ControlPlanItems.AutoSize = true;
            this.ControlPlanItems.Checked = true;
            this.ControlPlanItems.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ControlPlanItems.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ControlPlanItems.Location = new System.Drawing.Point(347, 10);
            this.ControlPlanItems.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ControlPlanItems.Name = "ControlPlanItems";
            this.ControlPlanItems.Size = new System.Drawing.Size(243, 25);
            this.ControlPlanItems.TabIndex = 280;
            this.ControlPlanItems.Text = "کنترل صحت زمانبندی عملیات ";
            this.ControlPlanItems.UseVisualStyleBackColor = true;
            // 
            // lblWaiting
            // 
            this.lblWaiting.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblWaiting.ForeColor = System.Drawing.Color.Blue;
            this.lblWaiting.Location = new System.Drawing.Point(393, 43);
            this.lblWaiting.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWaiting.Name = "lblWaiting";
            this.lblWaiting.Size = new System.Drawing.Size(588, 20);
            this.lblWaiting.TabIndex = 276;
            this.lblWaiting.Text = "سیستم در حال برنامه ریزی ساب بچ ها می باشد، لطفا تا پایان عملیات صبر نمایید ...";
            this.lblWaiting.Visible = false;
            // 
            // FunctionLabel
            // 
            this.FunctionLabel.Location = new System.Drawing.Point(9, 91);
            this.FunctionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.FunctionLabel.Name = "FunctionLabel";
            this.FunctionLabel.Size = new System.Drawing.Size(481, 26);
            this.FunctionLabel.TabIndex = 279;
            this.FunctionLabel.Text = "function Label";
            this.FunctionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _cmdPlanning
            // 
            this._cmdPlanning.BackColor = System.Drawing.Color.Transparent;
            this._cmdPlanning.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._cmdPlanning.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdPlanning.ForeColor = System.Drawing.Color.Black;
            this._cmdPlanning.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdPlanning.Location = new System.Drawing.Point(761, 4);
            this._cmdPlanning.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._cmdPlanning.Name = "_cmdPlanning";
            this._cmdPlanning.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdPlanning.Size = new System.Drawing.Size(161, 34);
            this._cmdPlanning.TabIndex = 6;
            this._cmdPlanning.Text = "برنامه ریزی";
            this._cmdPlanning.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._cmdPlanning.UseVisualStyleBackColor = false;
            this._cmdPlanning.Click += new System.EventHandler(this.cmdPlanning_Click);
            // 
            // _cmdExit
            // 
            this._cmdExit.BackColor = System.Drawing.Color.Transparent;
            this._cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cmdExit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdExit.ForeColor = System.Drawing.Color.Red;
            this._cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cmdExit.Location = new System.Drawing.Point(79, 4);
            this._cmdExit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._cmdExit.Name = "_cmdExit";
            this._cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmdExit.Size = new System.Drawing.Size(156, 34);
            this._cmdExit.TabIndex = 5;
            this._cmdExit.Text = "انصراف/خروج";
            this._cmdExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._cmdExit.UseVisualStyleBackColor = false;
            this._cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // lblCurrentOperationCode
            // 
            this.lblCurrentOperationCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblCurrentOperationCode.ForeColor = System.Drawing.Color.Blue;
            this.lblCurrentOperationCode.Location = new System.Drawing.Point(549, 94);
            this.lblCurrentOperationCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentOperationCode.Name = "lblCurrentOperationCode";
            this.lblCurrentOperationCode.Size = new System.Drawing.Size(292, 22);
            this.lblCurrentOperationCode.TabIndex = 278;
            this.lblCurrentOperationCode.Text = "کد عملیات";
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label2.Location = new System.Drawing.Point(857, 96);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(124, 20);
            this.Label2.TabIndex = 277;
            this.Label2.Text = "کد عملیات جاری:";
            // 
            // lblCurrentSubbatchCode
            // 
            this.lblCurrentSubbatchCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblCurrentSubbatchCode.ForeColor = System.Drawing.Color.Green;
            this.lblCurrentSubbatchCode.Location = new System.Drawing.Point(592, 74);
            this.lblCurrentSubbatchCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentSubbatchCode.Name = "lblCurrentSubbatchCode";
            this.lblCurrentSubbatchCode.Size = new System.Drawing.Size(249, 22);
            this.lblCurrentSubbatchCode.TabIndex = 276;
            this.lblCurrentSubbatchCode.Text = "کد ساب بچ";
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label1.Location = new System.Drawing.Point(849, 76);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(132, 20);
            this.Label1.TabIndex = 275;
            this.Label1.Text = "کد ساب بچ جاری:";
            // 
            // Panel2
            // 
            this.Panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Panel2.Controls.Add(this._dgList);
            this.Panel2.Location = new System.Drawing.Point(8, 15);
            this.Panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(988, 452);
            this.Panel2.TabIndex = 274;
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
            this._dgList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
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
            this._dgList.Location = new System.Drawing.Point(8, 10);
            this._dgList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this._dgList.Size = new System.Drawing.Size(973, 434);
            this._dgList.TabIndex = 1;
            this._dgList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgList_CellFormatting);
            this._dgList.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgList_RowHeaderMouseClick);
            // 
            // frmPlanning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cmdExit;
            this.ClientSize = new System.Drawing.Size(1007, 606);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPlanning";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " برنامه ریزی ساب بچ های تولید";
            this.Load += new System.EventHandler(this.frmPlanning_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dgList)).EndInit();
            this.ResumeLayout(false);

        }

        internal System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.Button _cmdPlanning;

        internal System.Windows.Forms.Button cmdPlanning
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdPlanning;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdPlanning != null)
                {
                    _cmdPlanning.Click -= cmdPlanning_Click;
                }

                _cmdPlanning = value;
                if (_cmdPlanning != null)
                {
                    _cmdPlanning.Click += cmdPlanning_Click;
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

        internal System.Windows.Forms.Panel Panel2;
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
                    _dgList.RowHeaderMouseClick -= dgList_RowHeaderMouseClick;
                    _dgList.CellFormatting -= dgList_CellFormatting;
                }

                _dgList = value;
                if (_dgList != null)
                {
                    _dgList.RowHeaderMouseClick += dgList_RowHeaderMouseClick;
                    _dgList.CellFormatting += dgList_CellFormatting;
                }
            }
        }

        internal Label Label1;
        internal Label lblCurrentSubbatchCode;
        internal Label lblCurrentOperationCode;
        internal Label Label2;
        internal Label lblWaiting;
        internal Label FunctionLabel;
        internal CheckBox ControlPlanItems;
    }
}