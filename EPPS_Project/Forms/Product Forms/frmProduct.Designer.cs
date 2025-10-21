using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmProduct : Form
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
            lblName = new Label();
            _txtName = new TextBox();
            _txtName.KeyPress += new KeyPressEventHandler(Controls_KeyPress);
            Panel1 = new System.Windows.Forms.Panel();
            _cmdSave = new System.Windows.Forms.Button();
            _cmdSave.Click += new EventHandler(cmdSave_Click);
            _cmdExit = new System.Windows.Forms.Button();
            _cmdExit.Click += new EventHandler(cmdExit_Click);
            _cmdDelete = new System.Windows.Forms.Button();
            _cmdDelete.Click += new EventHandler(cmdDelete_Click);
            _txtStock = new NumericUpDown();
            _txtStock.KeyPress += new KeyPressEventHandler(Controls_KeyPress);
            Label8 = new Label();
            Label1 = new Label();
            _txtCode = new TextBox();
            _txtCode.KeyPress += new KeyPressEventHandler(Controls_KeyPress);
            txtTransferMinQty = new NumericUpDown();
            Label2 = new Label();
            Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_txtStock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtTransferMinQty).BeginInit();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblName.Location = new Point(289, 67);
            lblName.Name = "lblName";
            lblName.RightToLeft = RightToLeft.Yes;
            lblName.Size = new Size(62, 17);
            lblName.TabIndex = 83;
            lblName.Text = "نام محصول";
            // 
            // txtName
            // 
            _txtName.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _txtName.Location = new Point(13, 67);
            _txtName.Name = "_txtName";
            _txtName.Size = new Size(273, 21);
            _txtName.TabIndex = 1;
            // 
            // Panel1
            // 
            Panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Panel1.BackColor = SystemColors.Control;
            Panel1.BorderStyle = BorderStyle.FixedSingle;
            Panel1.Controls.Add(_cmdSave);
            Panel1.Controls.Add(_cmdExit);
            Panel1.Controls.Add(_cmdDelete);
            Panel1.Location = new Point(9, 171);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(347, 48);
            Panel1.TabIndex = 82;
            // 
            // cmdSave
            // 
            _cmdSave.BackColor = Color.Transparent;
            _cmdSave.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdSave.ForeColor = Color.Black;
            _cmdSave.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdSave.Location = new Point(187, 10);
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
            _cmdExit.Location = new Point(68, 10);
            _cmdExit.Name = "_cmdExit";
            _cmdExit.RightToLeft = RightToLeft.No;
            _cmdExit.Size = new Size(90, 28);
            _cmdExit.TabIndex = 5;
            _cmdExit.Text = "انصراف";
            _cmdExit.TextAlign = ContentAlignment.MiddleRight;
            _cmdExit.UseVisualStyleBackColor = false;
            // 
            // cmdDelete
            // 
            _cmdDelete.BackColor = Color.Transparent;
            _cmdDelete.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdDelete.ForeColor = Color.Black;
            _cmdDelete.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdDelete.Location = new Point(187, 10);
            _cmdDelete.Name = "_cmdDelete";
            _cmdDelete.RightToLeft = RightToLeft.No;
            _cmdDelete.Size = new Size(90, 28);
            _cmdDelete.TabIndex = 4;
            _cmdDelete.Text = "حذف";
            _cmdDelete.TextAlign = ContentAlignment.MiddleRight;
            _cmdDelete.UseVisualStyleBackColor = false;
            _cmdDelete.Visible = false;
            // 
            // txtStock
            // 
            _txtStock.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _txtStock.Location = new Point(100, 104);
            _txtStock.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            _txtStock.Name = "_txtStock";
            _txtStock.Size = new Size(95, 21);
            _txtStock.TabIndex = 2;
            // 
            // Label8
            // 
            Label8.BackColor = SystemColors.Control;
            Label8.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label8.ForeColor = Color.Black;
            Label8.Location = new Point(225, 105);
            Label8.Name = "Label8";
            Label8.RightToLeft = RightToLeft.Yes;
            Label8.Size = new Size(126, 17);
            Label8.TabIndex = 80;
            Label8.Text = "حداقل موجودی اطمینان";
            // 
            // Label1
            // 
            Label1.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label1.Location = new Point(289, 30);
            Label1.Name = "Label1";
            Label1.RightToLeft = RightToLeft.Yes;
            Label1.Size = new Size(62, 17);
            Label1.TabIndex = 85;
            Label1.Text = "کد محصول";
            // 
            // txtCode
            // 
            _txtCode.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _txtCode.Location = new Point(13, 30);
            _txtCode.Name = "_txtCode";
            _txtCode.Size = new Size(273, 21);
            _txtCode.TabIndex = 0;
            // 
            // txtTransferMinQty
            // 
            txtTransferMinQty.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtTransferMinQty.Location = new Point(100, 134);
            txtTransferMinQty.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            txtTransferMinQty.Name = "txtTransferMinQty";
            txtTransferMinQty.Size = new Size(95, 21);
            txtTransferMinQty.TabIndex = 86;
            // 
            // Label2
            // 
            Label2.BackColor = SystemColors.Control;
            Label2.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label2.ForeColor = Color.Black;
            Label2.Location = new Point(201, 135);
            Label2.Name = "Label2";
            Label2.RightToLeft = RightToLeft.Yes;
            Label2.Size = new Size(150, 17);
            Label2.TabIndex = 87;
            Label2.Text = "حداقل تعداد محمولۀ ارسالی";
            // 
            // frmProduct
            // 
            AcceptButton = _cmdSave;
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _cmdExit;
            ClientSize = new Size(365, 226);
            Controls.Add(txtTransferMinQty);
            Controls.Add(Label2);
            Controls.Add(Label1);
            Controls.Add(_txtCode);
            Controls.Add(lblName);
            Controls.Add(_txtName);
            Controls.Add(Panel1);
            Controls.Add(_txtStock);
            Controls.Add(Label8);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Location = new Point(50, 50);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmProduct";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " مشخصات محصول";
            Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_txtStock).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtTransferMinQty).EndInit();
            Load += new EventHandler(Form1_Load);
            FormClosing += new FormClosingEventHandler(frmProduct_FormClosing);
            ResumeLayout(false);
            PerformLayout();
        }

        internal Label lblName;
        private TextBox _txtName;

        internal TextBox txtName
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtName;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtName != null)
                {
                    _txtName.KeyPress -= Controls_KeyPress;
                }

                _txtName = value;
                if (_txtName != null)
                {
                    _txtName.KeyPress += Controls_KeyPress;
                }
            }
        }

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

        private NumericUpDown _txtStock;

        internal NumericUpDown txtStock
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtStock;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtStock != null)
                {
                    _txtStock.KeyPress -= Controls_KeyPress;
                }

                _txtStock = value;
                if (_txtStock != null)
                {
                    _txtStock.KeyPress += Controls_KeyPress;
                }
            }
        }

        internal Label Label8;
        internal Label Label1;
        private TextBox _txtCode;

        internal TextBox txtCode
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtCode;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtCode != null)
                {
                    _txtCode.KeyPress -= Controls_KeyPress;
                }

                _txtCode = value;
                if (_txtCode != null)
                {
                    _txtCode.KeyPress += Controls_KeyPress;
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

        internal NumericUpDown txtTransferMinQty;
        internal Label Label2;
    }
}