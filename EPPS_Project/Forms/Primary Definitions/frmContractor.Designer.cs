using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmContractor : Form
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
            _cmdSave = new System.Windows.Forms.Button();
            _cmdSave.Click += new EventHandler(cmdSave_Click);
            Panel1 = new System.Windows.Forms.Panel();
            _cmdExit = new System.Windows.Forms.Button();
            _cmdExit.Click += new EventHandler(cmdCancel_Click);
            _cmdDelete = new System.Windows.Forms.Button();
            _cmdDelete.Click += new EventHandler(cmdDelete_Click);
            txtName = new TextBox();
            Label10 = new Label();
            txtAddress = new TextBox();
            Label2 = new Label();
            txtPhone = new TextBox();
            Label3 = new Label();
            txtActivityContext = new TextBox();
            Label4 = new Label();
            txtDescription = new TextBox();
            Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblName.Location = new Point(474, 22);
            lblName.Name = "lblName";
            lblName.RightToLeft = RightToLeft.Yes;
            lblName.Size = new Size(61, 17);
            lblName.TabIndex = 12;
            lblName.Text = "نام پیمانکار";
            // 
            // cmdSave
            // 
            _cmdSave.BackColor = Color.Transparent;
            _cmdSave.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdSave.ForeColor = Color.Black;
            _cmdSave.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdSave.Location = new Point(277, 10);
            _cmdSave.Name = "_cmdSave";
            _cmdSave.RightToLeft = RightToLeft.No;
            _cmdSave.Size = new Size(90, 28);
            _cmdSave.TabIndex = 5;
            _cmdSave.Text = "ثبت";
            _cmdSave.TextAlign = ContentAlignment.MiddleRight;
            _cmdSave.UseVisualStyleBackColor = false;
            // 
            // Panel1
            // 
            Panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Panel1.BackColor = SystemColors.Control;
            Panel1.BorderStyle = BorderStyle.FixedSingle;
            Panel1.Controls.Add(_cmdSave);
            Panel1.Controls.Add(_cmdExit);
            Panel1.Controls.Add(_cmdDelete);
            Panel1.ForeColor = Color.Black;
            Panel1.Location = new Point(9, 205);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(526, 48);
            Panel1.TabIndex = 14;
            // 
            // cmdExit
            // 
            _cmdExit.BackColor = Color.Transparent;
            _cmdExit.DialogResult = DialogResult.Cancel;
            _cmdExit.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdExit.ForeColor = Color.Red;
            _cmdExit.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdExit.Location = new Point(158, 10);
            _cmdExit.Name = "_cmdExit";
            _cmdExit.RightToLeft = RightToLeft.No;
            _cmdExit.Size = new Size(90, 28);
            _cmdExit.TabIndex = 7;
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
            _cmdDelete.Location = new Point(277, 10);
            _cmdDelete.Name = "_cmdDelete";
            _cmdDelete.RightToLeft = RightToLeft.No;
            _cmdDelete.Size = new Size(90, 28);
            _cmdDelete.TabIndex = 6;
            _cmdDelete.Text = "حذف";
            _cmdDelete.TextAlign = ContentAlignment.MiddleRight;
            _cmdDelete.UseVisualStyleBackColor = false;
            _cmdDelete.Visible = false;
            // 
            // txtName
            // 
            txtName.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtName.Location = new Point(171, 20);
            txtName.Name = "txtName";
            txtName.Size = new Size(297, 21);
            txtName.TabIndex = 0;
            // 
            // Label10
            // 
            Label10.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label10.Location = new Point(499, 55);
            Label10.Name = "Label10";
            Label10.RightToLeft = RightToLeft.Yes;
            Label10.Size = new Size(36, 17);
            Label10.TabIndex = 15;
            Label10.Text = "آدرس";
            // 
            // txtAddress
            // 
            txtAddress.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtAddress.Location = new Point(9, 53);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(459, 21);
            txtAddress.TabIndex = 1;
            // 
            // Label2
            // 
            Label2.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label2.Location = new Point(505, 88);
            Label2.Name = "Label2";
            Label2.RightToLeft = RightToLeft.Yes;
            Label2.Size = new Size(30, 17);
            Label2.TabIndex = 17;
            Label2.Text = "تلفن";
            // 
            // txtPhone
            // 
            txtPhone.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtPhone.Location = new Point(316, 86);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(152, 21);
            txtPhone.TabIndex = 2;
            // 
            // Label3
            // 
            Label3.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label3.Location = new Point(227, 88);
            Label3.Name = "Label3";
            Label3.RightToLeft = RightToLeft.Yes;
            Label3.Size = new Size(70, 17);
            Label3.TabIndex = 19;
            Label3.Text = "زمینه فعالیت";
            // 
            // txtActivityContext
            // 
            txtActivityContext.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtActivityContext.Location = new Point(9, 86);
            txtActivityContext.Name = "txtActivityContext";
            txtActivityContext.Size = new Size(212, 21);
            txtActivityContext.TabIndex = 3;
            // 
            // Label4
            // 
            Label4.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label4.Location = new Point(484, 121);
            Label4.Name = "Label4";
            Label4.RightToLeft = RightToLeft.Yes;
            Label4.Size = new Size(51, 17);
            Label4.TabIndex = 21;
            Label4.Text = "توضیحات";
            // 
            // txtDescription
            // 
            txtDescription.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtDescription.Location = new Point(9, 119);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(459, 71);
            txtDescription.TabIndex = 4;
            // 
            // frmContractor
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _cmdExit;
            ClientSize = new Size(544, 259);
            Controls.Add(Label4);
            Controls.Add(txtDescription);
            Controls.Add(Label3);
            Controls.Add(txtActivityContext);
            Controls.Add(Label2);
            Controls.Add(txtPhone);
            Controls.Add(Label10);
            Controls.Add(txtAddress);
            Controls.Add(lblName);
            Controls.Add(Panel1);
            Controls.Add(txtName);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmContractor";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " مشخصات پیمانکار";
            Panel1.ResumeLayout(false);
            Load += new EventHandler(Form1_Load);
            FormClosing += new FormClosingEventHandler(frmContractor_FormClosing);
            ResumeLayout(false);
            PerformLayout();
        }

        internal Label lblName;
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

        internal System.Windows.Forms.Panel Panel1;
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
                    _cmdExit.Click -= cmdCancel_Click;
                }

                _cmdExit = value;
                if (_cmdExit != null)
                {
                    _cmdExit.Click += cmdCancel_Click;
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

        internal TextBox txtName;
        internal Label Label10;
        internal TextBox txtAddress;
        internal Label Label2;
        internal TextBox txtPhone;
        internal Label Label3;
        internal TextBox txtActivityContext;
        internal Label Label4;
        internal TextBox txtDescription;
    }
}