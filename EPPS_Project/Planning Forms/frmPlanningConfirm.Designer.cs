using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmPlanningConfirm : Form
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
            this.pnlOperations = new System.Windows.Forms.Panel();
            this.lblSubbatchCode = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this._dgEndConfilicts = new System.Windows.Forms.DataGridView();
            this.DataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Label1 = new System.Windows.Forms.Label();
            this._dgStartConfilicts = new System.Windows.Forms.DataGridView();
            this.CurrentOperation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PreOperation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PreStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Button1 = new System.Windows.Forms.Button();
            this.cmdConfirm = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.pnlSubbatch = new System.Windows.Forms.Panel();
            this.cmdTomorrowPlanning = new System.Windows.Forms.Button();
            this.dgSubbatch = new System.Windows.Forms.DataGridView();
            this.DataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.lblLagTime = new System.Windows.Forms.Label();
            this.lblRelationType = new System.Windows.Forms.Label();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.lblPreOpPET = new System.Windows.Forms.Label();
            this.lblPreOpMachine = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.lblOpOET = new System.Windows.Forms.Label();
            this.lblOpMachine = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.pnlOperations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgEndConfilicts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgStartConfilicts)).BeginInit();
            this.Panel2.SuspendLayout();
            this.pnlSubbatch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSubbatch)).BeginInit();
            this.GroupBox1.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOperations
            // 
            this.pnlOperations.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOperations.Controls.Add(this.lblSubbatchCode);
            this.pnlOperations.Controls.Add(this.Label2);
            this.pnlOperations.Controls.Add(this._dgEndConfilicts);
            this.pnlOperations.Controls.Add(this.Label1);
            this.pnlOperations.Controls.Add(this._dgStartConfilicts);
            this.pnlOperations.Location = new System.Drawing.Point(6, 6);
            this.pnlOperations.Name = "pnlOperations";
            this.pnlOperations.Size = new System.Drawing.Size(528, 331);
            this.pnlOperations.TabIndex = 0;
            // 
            // lblSubbatchCode
            // 
            this.lblSubbatchCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubbatchCode.BackColor = System.Drawing.Color.Gainsboro;
            this.lblSubbatchCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblSubbatchCode.ForeColor = System.Drawing.Color.Blue;
            this.lblSubbatchCode.Location = new System.Drawing.Point(6, 10);
            this.lblSubbatchCode.Name = "lblSubbatchCode";
            this.lblSubbatchCode.Size = new System.Drawing.Size(180, 24);
            this.lblSubbatchCode.TabIndex = 270;
            this.lblSubbatchCode.Text = "کد ساب بچ:";
            this.lblSubbatchCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label2.BackColor = System.Drawing.Color.Beige;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label2.ForeColor = System.Drawing.Color.Olive;
            this.Label2.Location = new System.Drawing.Point(3, 179);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(520, 20);
            this.Label2.TabIndex = 269;
            this.Label2.Text = "لیست مواردی که زمان پایان آنها با پیشنیازشان مغایرت دارد:";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _dgEndConfilicts
            // 
            this._dgEndConfilicts.AllowUserToAddRows = false;
            this._dgEndConfilicts.AllowUserToDeleteRows = false;
            this._dgEndConfilicts.AllowUserToResizeColumns = false;
            this._dgEndConfilicts.AllowUserToResizeRows = false;
            this._dgEndConfilicts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dgEndConfilicts.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._dgEndConfilicts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this._dgEndConfilicts.ColumnHeadersHeight = 25;
            this._dgEndConfilicts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this._dgEndConfilicts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DataGridViewTextBoxColumn1,
            this.DataGridViewTextBoxColumn2,
            this.DataGridViewTextBoxColumn3,
            this.DataGridViewTextBoxColumn4});
            this._dgEndConfilicts.Location = new System.Drawing.Point(3, 200);
            this._dgEndConfilicts.Name = "_dgEndConfilicts";
            this._dgEndConfilicts.RowHeadersWidth = 20;
            this._dgEndConfilicts.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this._dgEndConfilicts.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this._dgEndConfilicts.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._dgEndConfilicts.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this._dgEndConfilicts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgEndConfilicts.Size = new System.Drawing.Size(520, 125);
            this._dgEndConfilicts.TabIndex = 268;
            this._dgEndConfilicts.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgEndConfilicts_RowEnter);
            // 
            // DataGridViewTextBoxColumn1
            // 
            this.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DataGridViewTextBoxColumn1.HeaderText = "فعالیت";
            this.DataGridViewTextBoxColumn1.MinimumWidth = 110;
            this.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1";
            this.DataGridViewTextBoxColumn1.ReadOnly = true;
            this.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewTextBoxColumn1.Width = 110;
            // 
            // DataGridViewTextBoxColumn2
            // 
            this.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DataGridViewTextBoxColumn2.HeaderText = "زمان پایان";
            this.DataGridViewTextBoxColumn2.MinimumWidth = 130;
            this.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2";
            this.DataGridViewTextBoxColumn2.ReadOnly = true;
            this.DataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewTextBoxColumn2.Width = 130;
            // 
            // DataGridViewTextBoxColumn3
            // 
            this.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DataGridViewTextBoxColumn3.HeaderText = "فعالیت پیشنیاز";
            this.DataGridViewTextBoxColumn3.MinimumWidth = 110;
            this.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3";
            this.DataGridViewTextBoxColumn3.ReadOnly = true;
            this.DataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewTextBoxColumn3.Width = 110;
            // 
            // DataGridViewTextBoxColumn4
            // 
            this.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DataGridViewTextBoxColumn4.HeaderText = "زمان پایان";
            this.DataGridViewTextBoxColumn4.MinimumWidth = 130;
            this.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4";
            this.DataGridViewTextBoxColumn4.ReadOnly = true;
            this.DataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewTextBoxColumn4.Width = 130;
            // 
            // Label1
            // 
            this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label1.BackColor = System.Drawing.Color.Gainsboro;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label1.ForeColor = System.Drawing.Color.Red;
            this.Label1.Location = new System.Drawing.Point(202, 10);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(318, 24);
            this.Label1.TabIndex = 267;
            this.Label1.Text = "لیست مواردی که زمان شروع آنها با پیشنیازشان مغایرت دارد:";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _dgStartConfilicts
            // 
            this._dgStartConfilicts.AllowUserToAddRows = false;
            this._dgStartConfilicts.AllowUserToDeleteRows = false;
            this._dgStartConfilicts.AllowUserToResizeColumns = false;
            this._dgStartConfilicts.AllowUserToResizeRows = false;
            this._dgStartConfilicts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dgStartConfilicts.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._dgStartConfilicts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this._dgStartConfilicts.ColumnHeadersHeight = 25;
            this._dgStartConfilicts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this._dgStartConfilicts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CurrentOperation,
            this.CurrentStartTime,
            this.PreOperation,
            this.PreStartTime});
            this._dgStartConfilicts.Location = new System.Drawing.Point(3, 36);
            this._dgStartConfilicts.Name = "_dgStartConfilicts";
            this._dgStartConfilicts.RowHeadersWidth = 20;
            this._dgStartConfilicts.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this._dgStartConfilicts.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this._dgStartConfilicts.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this._dgStartConfilicts.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this._dgStartConfilicts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgStartConfilicts.Size = new System.Drawing.Size(520, 125);
            this._dgStartConfilicts.TabIndex = 266;
            this._dgStartConfilicts.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgStartConfilicts_RowEnter);
            // 
            // CurrentOperation
            // 
            this.CurrentOperation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CurrentOperation.HeaderText = "فعالیت";
            this.CurrentOperation.MinimumWidth = 110;
            this.CurrentOperation.Name = "CurrentOperation";
            this.CurrentOperation.ReadOnly = true;
            this.CurrentOperation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CurrentOperation.Width = 110;
            // 
            // CurrentStartTime
            // 
            this.CurrentStartTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CurrentStartTime.HeaderText = "زمان شروع";
            this.CurrentStartTime.MinimumWidth = 130;
            this.CurrentStartTime.Name = "CurrentStartTime";
            this.CurrentStartTime.ReadOnly = true;
            this.CurrentStartTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CurrentStartTime.Width = 130;
            // 
            // PreOperation
            // 
            this.PreOperation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.PreOperation.HeaderText = "فعالیت پیشنیاز";
            this.PreOperation.MinimumWidth = 110;
            this.PreOperation.Name = "PreOperation";
            this.PreOperation.ReadOnly = true;
            this.PreOperation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PreOperation.Width = 110;
            // 
            // PreStartTime
            // 
            this.PreStartTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.PreStartTime.HeaderText = "زمان شروع";
            this.PreStartTime.MinimumWidth = 130;
            this.PreStartTime.Name = "PreStartTime";
            this.PreStartTime.ReadOnly = true;
            this.PreStartTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PreStartTime.Width = 130;
            // 
            // Panel2
            // 
            this.Panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel2.Controls.Add(this.Button1);
            this.Panel2.Controls.Add(this.cmdConfirm);
            this.Panel2.Controls.Add(this.cmdCancel);
            this.Panel2.Location = new System.Drawing.Point(6, 343);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(528, 48);
            this.Panel2.TabIndex = 78;
            // 
            // Button1
            // 
            this.Button1.BackColor = System.Drawing.Color.Transparent;
            this.Button1.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.Button1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Button1.ForeColor = System.Drawing.Color.Black;
            this.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button1.Location = new System.Drawing.Point(12, 11);
            this.Button1.Name = "Button1";
            this.Button1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Button1.Size = new System.Drawing.Size(137, 26);
            this.Button1.TabIndex = 7;
            this.Button1.Text = "انصراف از برنامه ریزی";
            this.Button1.UseVisualStyleBackColor = false;
            // 
            // cmdConfirm
            // 
            this.cmdConfirm.BackColor = System.Drawing.Color.Transparent;
            this.cmdConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdConfirm.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cmdConfirm.ForeColor = System.Drawing.Color.Black;
            this.cmdConfirm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdConfirm.Location = new System.Drawing.Point(378, 11);
            this.cmdConfirm.Name = "cmdConfirm";
            this.cmdConfirm.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdConfirm.Size = new System.Drawing.Size(137, 26);
            this.cmdConfirm.TabIndex = 6;
            this.cmdConfirm.Text = "تأیید برنامه ریزی";
            this.cmdConfirm.UseVisualStyleBackColor = false;
            // 
            // cmdCancel
            // 
            this.cmdCancel.BackColor = System.Drawing.Color.Transparent;
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this.cmdCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cmdCancel.ForeColor = System.Drawing.Color.Black;
            this.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdCancel.Location = new System.Drawing.Point(226, 11);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdCancel.Size = new System.Drawing.Size(137, 26);
            this.cmdCancel.TabIndex = 5;
            this.cmdCancel.Text = "حذف برنامه ریزی";
            this.cmdCancel.UseVisualStyleBackColor = false;
            // 
            // pnlSubbatch
            // 
            this.pnlSubbatch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSubbatch.Controls.Add(this.cmdTomorrowPlanning);
            this.pnlSubbatch.Controls.Add(this.dgSubbatch);
            this.pnlSubbatch.Location = new System.Drawing.Point(6, 6);
            this.pnlSubbatch.Name = "pnlSubbatch";
            this.pnlSubbatch.Size = new System.Drawing.Size(528, 178);
            this.pnlSubbatch.TabIndex = 79;
            // 
            // cmdTomorrowPlanning
            // 
            this.cmdTomorrowPlanning.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.cmdTomorrowPlanning.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.cmdTomorrowPlanning.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cmdTomorrowPlanning.ForeColor = System.Drawing.Color.Black;
            this.cmdTomorrowPlanning.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdTomorrowPlanning.Location = new System.Drawing.Point(27, 133);
            this.cmdTomorrowPlanning.Name = "cmdTomorrowPlanning";
            this.cmdTomorrowPlanning.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdTomorrowPlanning.Size = new System.Drawing.Size(239, 28);
            this.cmdTomorrowPlanning.TabIndex = 267;
            this.cmdTomorrowPlanning.Text = "برنامه ریزی ساب بچ از فردای روز جاری";
            this.cmdTomorrowPlanning.UseVisualStyleBackColor = false;
            // 
            // dgSubbatch
            // 
            this.dgSubbatch.AllowUserToAddRows = false;
            this.dgSubbatch.AllowUserToDeleteRows = false;
            this.dgSubbatch.AllowUserToResizeColumns = false;
            this.dgSubbatch.AllowUserToResizeRows = false;
            this.dgSubbatch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgSubbatch.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSubbatch.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgSubbatch.ColumnHeadersHeight = 25;
            this.dgSubbatch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgSubbatch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DataGridViewTextBoxColumn9,
            this.DataGridViewTextBoxColumn10});
            this.dgSubbatch.Location = new System.Drawing.Point(5, 9);
            this.dgSubbatch.Name = "dgSubbatch";
            this.dgSubbatch.RowHeadersWidth = 20;
            this.dgSubbatch.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgSubbatch.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgSubbatch.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.dgSubbatch.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgSubbatch.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSubbatch.RowTemplate.Height = 40;
            this.dgSubbatch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSubbatch.Size = new System.Drawing.Size(516, 164);
            this.dgSubbatch.TabIndex = 266;
            // 
            // DataGridViewTextBoxColumn9
            // 
            this.DataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DataGridViewTextBoxColumn9.HeaderText = "کد ساب بچ";
            this.DataGridViewTextBoxColumn9.MinimumWidth = 130;
            this.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9";
            this.DataGridViewTextBoxColumn9.ReadOnly = true;
            this.DataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewTextBoxColumn9.Width = 130;
            // 
            // DataGridViewTextBoxColumn10
            // 
            this.DataGridViewTextBoxColumn10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DataGridViewTextBoxColumn10.HeaderText = "شرح";
            this.DataGridViewTextBoxColumn10.MinimumWidth = 340;
            this.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10";
            this.DataGridViewTextBoxColumn10.ReadOnly = true;
            this.DataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewTextBoxColumn10.Width = 340;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.lblLagTime);
            this.GroupBox1.Controls.Add(this.lblRelationType);
            this.GroupBox1.Controls.Add(this.GroupBox3);
            this.GroupBox1.Controls.Add(this.GroupBox2);
            this.GroupBox1.Controls.Add(this.Label5);
            this.GroupBox1.Controls.Add(this.Label4);
            this.GroupBox1.Location = new System.Drawing.Point(543, 0);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(203, 391);
            this.GroupBox1.TabIndex = 80;
            this.GroupBox1.TabStop = false;
            // 
            // lblLagTime
            // 
            this.lblLagTime.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblLagTime.ForeColor = System.Drawing.Color.Green;
            this.lblLagTime.Location = new System.Drawing.Point(12, 268);
            this.lblLagTime.Name = "lblLagTime";
            this.lblLagTime.Size = new System.Drawing.Size(94, 19);
            this.lblLagTime.TabIndex = 7;
            this.lblLagTime.Text = "#";
            this.lblLagTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRelationType
            // 
            this.lblRelationType.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblRelationType.ForeColor = System.Drawing.Color.Green;
            this.lblRelationType.Location = new System.Drawing.Point(12, 239);
            this.lblRelationType.Name = "lblRelationType";
            this.lblRelationType.Size = new System.Drawing.Size(94, 19);
            this.lblRelationType.TabIndex = 6;
            this.lblRelationType.Text = "#";
            this.lblRelationType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.lblPreOpPET);
            this.GroupBox3.Controls.Add(this.lblPreOpMachine);
            this.GroupBox3.Controls.Add(this.Label9);
            this.GroupBox3.Controls.Add(this.Label10);
            this.GroupBox3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.GroupBox3.Location = new System.Drawing.Point(6, 123);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(191, 100);
            this.GroupBox3.TabIndex = 5;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "عملیات پیشنیاز";
            // 
            // lblPreOpPET
            // 
            this.lblPreOpPET.ForeColor = System.Drawing.Color.Blue;
            this.lblPreOpPET.Location = new System.Drawing.Point(6, 64);
            this.lblPreOpPET.Name = "lblPreOpPET";
            this.lblPreOpPET.Size = new System.Drawing.Size(97, 19);
            this.lblPreOpPET.TabIndex = 5;
            this.lblPreOpPET.Text = "#";
            this.lblPreOpPET.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPreOpMachine
            // 
            this.lblPreOpMachine.ForeColor = System.Drawing.Color.Blue;
            this.lblPreOpMachine.Location = new System.Drawing.Point(6, 23);
            this.lblPreOpMachine.Name = "lblPreOpMachine";
            this.lblPreOpMachine.Size = new System.Drawing.Size(97, 19);
            this.lblPreOpMachine.TabIndex = 6;
            this.lblPreOpMachine.Text = "#";
            this.lblPreOpMachine.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(107, 26);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(57, 13);
            this.Label9.TabIndex = 0;
            this.Label9.Text = "کد ماشین:";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(107, 67);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(80, 13);
            this.Label10.TabIndex = 3;
            this.Label10.Text = "زمان یکبار انجام:";
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.lblOpOET);
            this.GroupBox2.Controls.Add(this.lblOpMachine);
            this.GroupBox2.Controls.Add(this.Label3);
            this.GroupBox2.Controls.Add(this.Label6);
            this.GroupBox2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.GroupBox2.Location = new System.Drawing.Point(6, 12);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(191, 100);
            this.GroupBox2.TabIndex = 4;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "عملیات جاری";
            // 
            // lblOpOET
            // 
            this.lblOpOET.ForeColor = System.Drawing.Color.Blue;
            this.lblOpOET.Location = new System.Drawing.Point(6, 64);
            this.lblOpOET.Name = "lblOpOET";
            this.lblOpOET.Size = new System.Drawing.Size(97, 19);
            this.lblOpOET.TabIndex = 5;
            this.lblOpOET.Text = "#";
            this.lblOpOET.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOpMachine
            // 
            this.lblOpMachine.ForeColor = System.Drawing.Color.Blue;
            this.lblOpMachine.Location = new System.Drawing.Point(6, 23);
            this.lblOpMachine.Name = "lblOpMachine";
            this.lblOpMachine.Size = new System.Drawing.Size(97, 19);
            this.lblOpMachine.TabIndex = 6;
            this.lblOpMachine.Text = "#";
            this.lblOpMachine.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(107, 26);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(57, 13);
            this.Label3.TabIndex = 0;
            this.Label3.Text = "کد ماشین:";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(107, 67);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(80, 13);
            this.Label6.TabIndex = 3;
            this.Label6.Text = "زمان یکبار انجام:";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label5.Location = new System.Drawing.Point(113, 271);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(59, 13);
            this.Label5.TabIndex = 2;
            this.Label5.Text = "تاخیر/تقدم:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Label4.Location = new System.Drawing.Point(113, 242);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(53, 13);
            this.Label4.TabIndex = 1;
            this.Label4.Text = "نوع رابطه:";
            // 
            // frmPlanningConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 396);
            this.ControlBox = false;
            this.Controls.Add(this.pnlSubbatch);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.pnlOperations);
            this.Controls.Add(this.GroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPlanningConfirm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " موارد مغایرت در برنامه ریزی ساب بچ";
            this.pnlOperations.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dgEndConfilicts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgStartConfilicts)).EndInit();
            this.Panel2.ResumeLayout(false);
            this.pnlSubbatch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgSubbatch)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox3.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        internal System.Windows.Forms.Panel pnlOperations;
        private DataGridView _dgStartConfilicts;

        internal DataGridView dgStartConfilicts
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _dgStartConfilicts;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_dgStartConfilicts != null)
                {
                    _dgStartConfilicts.RowEnter -= dgStartConfilicts_RowEnter;
                }

                _dgStartConfilicts = value;
                if (_dgStartConfilicts != null)
                {
                    _dgStartConfilicts.RowEnter += dgStartConfilicts_RowEnter;
                }
            }
        }

        internal Label Label1;
        internal Label Label2;
        private DataGridView _dgEndConfilicts;

        internal DataGridView dgEndConfilicts
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _dgEndConfilicts;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_dgEndConfilicts != null)
                {
                    _dgEndConfilicts.RowEnter -= dgEndConfilicts_RowEnter;
                }

                _dgEndConfilicts = value;
                if (_dgEndConfilicts != null)
                {
                    _dgEndConfilicts.RowEnter += dgEndConfilicts_RowEnter;
                }
            }
        }

        internal DataGridViewTextBoxColumn CurrentOperation;
        internal DataGridViewTextBoxColumn CurrentStartTime;
        internal DataGridViewTextBoxColumn PreOperation;
        internal DataGridViewTextBoxColumn PreStartTime;
        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.Button cmdConfirm;
        internal System.Windows.Forms.Button cmdCancel;
        internal DataGridViewTextBoxColumn DataGridViewTextBoxColumn1;
        internal DataGridViewTextBoxColumn DataGridViewTextBoxColumn2;
        internal DataGridViewTextBoxColumn DataGridViewTextBoxColumn3;
        internal DataGridViewTextBoxColumn DataGridViewTextBoxColumn4;
        internal Label lblSubbatchCode;
        internal System.Windows.Forms.Panel pnlSubbatch;
        internal DataGridView dgSubbatch;
        internal DataGridViewTextBoxColumn DataGridViewTextBoxColumn9;
        internal DataGridViewTextBoxColumn DataGridViewTextBoxColumn10;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.Button cmdTomorrowPlanning;
        internal GroupBox GroupBox1;
        internal Label Label6;
        internal Label Label5;
        internal Label Label4;
        internal Label Label3;
        internal GroupBox GroupBox2;
        internal Label lblOpOET;
        internal Label lblOpMachine;
        internal GroupBox GroupBox3;
        internal Label lblPreOpPET;
        internal Label lblPreOpMachine;
        internal Label Label9;
        internal Label Label10;
        internal Label lblRelationType;
        internal Label lblLagTime;
    }
}