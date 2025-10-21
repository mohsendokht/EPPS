using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmAlert : Form
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
            this.Label1 = new System.Windows.Forms.Label();
            this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._cmdViewList = new System.Windows.Forms.Button();
            this._cmdClose = new System.Windows.Forms.Button();
            this.dgList = new System.Windows.Forms.DataGridView();
            this.chkNotReshow = new System.Windows.Forms.CheckBox();
            this.TableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgList)).BeginInit();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label1.ForeColor = System.Drawing.Color.Black;
            this.Label1.Location = new System.Drawing.Point(0, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(546, 54);
            this.Label1.TabIndex = 5;
            this.Label1.Text = "مشخصات موثر در برنامه ریزی، مربوط به برخی از ساب بچ هایی که برنامه ریزی شده اند، " +
    "تغییر یافته است";
            // 
            // TableLayoutPanel1
            // 
            this.TableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TableLayoutPanel1.BackColor = System.Drawing.Color.AntiqueWhite;
            this.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.TableLayoutPanel1.ColumnCount = 2;
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.Controls.Add(this._cmdViewList, 0, 0);
            this.TableLayoutPanel1.Controls.Add(this._cmdClose, 1, 0);
            this.TableLayoutPanel1.Location = new System.Drawing.Point(12, 57);
            this.TableLayoutPanel1.Name = "TableLayoutPanel1";
            this.TableLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TableLayoutPanel1.RowCount = 1;
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.Size = new System.Drawing.Size(523, 36);
            this.TableLayoutPanel1.TabIndex = 0;
            // 
            // _cmdViewList
            // 
            this._cmdViewList.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._cmdViewList.BackColor = System.Drawing.Color.Transparent;
            this._cmdViewList.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._cmdViewList.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdViewList.Location = new System.Drawing.Point(334, 6);
            this._cmdViewList.Name = "_cmdViewList";
            this._cmdViewList.Size = new System.Drawing.Size(115, 24);
            this._cmdViewList.TabIndex = 0;
            this._cmdViewList.Tag = "1";
            this._cmdViewList.Text = "<< نمایش لیست";
            this._cmdViewList.UseVisualStyleBackColor = false;
            this._cmdViewList.Click += new System.EventHandler(this.cmdViewList_Click);
            // 
            // _cmdClose
            // 
            this._cmdClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._cmdClose.BackColor = System.Drawing.Color.Transparent;
            this._cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cmdClose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._cmdClose.Location = new System.Drawing.Point(74, 6);
            this._cmdClose.Name = "_cmdClose";
            this._cmdClose.Size = new System.Drawing.Size(115, 24);
            this._cmdClose.TabIndex = 1;
            this._cmdClose.Text = "بستن 1";
            this._cmdClose.UseVisualStyleBackColor = false;
            this._cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // dgList
            // 
            this.dgList.AllowUserToAddRows = false;
            this.dgList.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgList.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgList.ColumnHeadersHeight = 25;
            this.dgList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgList.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgList.Location = new System.Drawing.Point(2, 111);
            this.dgList.MultiSelect = false;
            this.dgList.Name = "dgList";
            this.dgList.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgList.RowHeadersWidth = 15;
            this.dgList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgList.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgList.Size = new System.Drawing.Size(543, 202);
            this.dgList.TabIndex = 6;
            // 
            // chkNotReshow
            // 
            this.chkNotReshow.AutoSize = true;
            this.chkNotReshow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.chkNotReshow.ForeColor = System.Drawing.Color.Red;
            this.chkNotReshow.Location = new System.Drawing.Point(10, 33);
            this.chkNotReshow.Name = "chkNotReshow";
            this.chkNotReshow.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkNotReshow.Size = new System.Drawing.Size(115, 18);
            this.chkNotReshow.TabIndex = 7;
            this.chkNotReshow.Text = "عدم نمایش مجدد";
            this.chkNotReshow.UseVisualStyleBackColor = true;
            // 
            // frmAlert
            // 
            this.AcceptButton = this._cmdViewList;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Yellow;
            this.CancelButton = this._cmdClose;
            this.ClientSize = new System.Drawing.Size(547, 111);
            this.ControlBox = false;
            this.Controls.Add(this.TableLayoutPanel1);
            this.Controls.Add(this.chkNotReshow);
            this.Controls.Add(this.dgList);
            this.Controls.Add(this.Label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAlert";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " ";
            this.Load += new System.EventHandler(this.frmAlert_Load);
            this.Shown += new System.EventHandler(this.frmAlert_Shown);
            this.TableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal Label Label1;
        internal TableLayoutPanel TableLayoutPanel1;
        private System.Windows.Forms.Button _cmdViewList;

        internal System.Windows.Forms.Button cmdViewList
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdViewList;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdViewList != null)
                {
                    _cmdViewList.Click -= cmdViewList_Click;
                }

                _cmdViewList = value;
                if (_cmdViewList != null)
                {
                    _cmdViewList.Click += cmdViewList_Click;
                }
            }
        }

        private System.Windows.Forms.Button _cmdClose;

        internal System.Windows.Forms.Button cmdClose
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdClose;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdClose != null)
                {
                    _cmdClose.Click -= cmdClose_Click;
                }

                _cmdClose = value;
                if (_cmdClose != null)
                {
                    _cmdClose.Click += cmdClose_Click;
                }
            }
        }

        internal DataGridView dgList;
        internal CheckBox chkNotReshow;
    }
}