using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmUnitsRelation : Form
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
            _cmdSave = new System.Windows.Forms.Button();
            _cmdSave.Click += new EventHandler(cmdSave_Click);
            Panel1 = new System.Windows.Forms.Panel();
            _cmdExit = new System.Windows.Forms.Button();
            _cmdExit.Click += new EventHandler(cmdCancel_Click);
            _cmdDelete = new System.Windows.Forms.Button();
            _cmdDelete.Click += new EventHandler(cmdDelete_Click);
            _txtCommunicationFactor = new TextBox();
            _txtCommunicationFactor.KeyPress += new KeyPressEventHandler(Controls_KeyPress);
            lblName = new Label();
            _cbRelatedUnit = new ComboBox();
            _cbRelatedUnit.KeyPress += new KeyPressEventHandler(Controls_KeyPress);
            _cbBaseUnit = new ComboBox();
            _cbBaseUnit.KeyPress += new KeyPressEventHandler(Controls_KeyPress);
            Label1 = new Label();
            Label2 = new Label();
            Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // cmdSave
            // 
            _cmdSave.BackColor = Color.Transparent;
            _cmdSave.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdSave.ForeColor = Color.Black;
            _cmdSave.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdSave.Location = new Point(221, 10);
            _cmdSave.Name = "_cmdSave";
            _cmdSave.RightToLeft = RightToLeft.No;
            _cmdSave.Size = new Size(90, 28);
            _cmdSave.TabIndex = 3;
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
            Panel1.Location = new Point(9, 144);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(406, 48);
            Panel1.TabIndex = 8;
            // 
            // cmdExit
            // 
            _cmdExit.BackColor = Color.Transparent;
            _cmdExit.DialogResult = DialogResult.Cancel;
            _cmdExit.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdExit.ForeColor = Color.Red;
            _cmdExit.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdExit.Location = new Point(93, 10);
            _cmdExit.Name = "_cmdExit";
            _cmdExit.RightToLeft = RightToLeft.No;
            _cmdExit.Size = new Size(90, 28);
            _cmdExit.TabIndex = 4;
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
            _cmdDelete.Location = new Point(221, 10);
            _cmdDelete.Name = "_cmdDelete";
            _cmdDelete.RightToLeft = RightToLeft.No;
            _cmdDelete.Size = new Size(90, 28);
            _cmdDelete.TabIndex = 8;
            _cmdDelete.Text = "حذف";
            _cmdDelete.TextAlign = ContentAlignment.MiddleRight;
            _cmdDelete.UseVisualStyleBackColor = false;
            _cmdDelete.Visible = false;
            // 
            // txtCommunicationFactor
            // 
            _txtCommunicationFactor.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _txtCommunicationFactor.Location = new Point(53, 101);
            _txtCommunicationFactor.Name = "_txtCommunicationFactor";
            _txtCommunicationFactor.Size = new Size(190, 21);
            _txtCommunicationFactor.TabIndex = 2;
            // 
            // lblName
            // 
            lblName.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            lblName.Location = new Point(258, 103);
            lblName.Name = "lblName";
            lblName.RightToLeft = RightToLeft.Yes;
            lblName.Size = new Size(114, 17);
            lblName.TabIndex = 6;
            lblName.Text = "ضریب ارتباطی واحد";
            // 
            // cbRelatedUnit
            // 
            _cbRelatedUnit.DropDownStyle = ComboBoxStyle.DropDownList;
            _cbRelatedUnit.FlatStyle = FlatStyle.Flat;
            _cbRelatedUnit.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cbRelatedUnit.FormattingEnabled = true;
            _cbRelatedUnit.ItemHeight = 13;
            _cbRelatedUnit.Location = new Point(53, 64);
            _cbRelatedUnit.Name = "_cbRelatedUnit";
            _cbRelatedUnit.Size = new Size(190, 21);
            _cbRelatedUnit.TabIndex = 1;
            // 
            // cbBaseUnit
            // 
            _cbBaseUnit.DropDownStyle = ComboBoxStyle.DropDownList;
            _cbBaseUnit.FlatStyle = FlatStyle.Flat;
            _cbBaseUnit.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cbBaseUnit.FormattingEnabled = true;
            _cbBaseUnit.Location = new Point(53, 27);
            _cbBaseUnit.Name = "_cbBaseUnit";
            _cbBaseUnit.Size = new Size(190, 21);
            _cbBaseUnit.TabIndex = 0;
            // 
            // Label1
            // 
            Label1.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label1.Location = new Point(248, 66);
            Label1.Name = "Label1";
            Label1.RightToLeft = RightToLeft.Yes;
            Label1.Size = new Size(124, 17);
            Label1.TabIndex = 11;
            Label1.Text = "واحد سنجش مورد نظر";
            // 
            // Label2
            // 
            Label2.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label2.Location = new Point(275, 29);
            Label2.Name = "Label2";
            Label2.RightToLeft = RightToLeft.Yes;
            Label2.Size = new Size(97, 17);
            Label2.TabIndex = 12;
            Label2.Text = "واحد سنجش پایه";
            // 
            // frmUnitsRelation
            // 
            AcceptButton = _cmdSave;
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _cmdExit;
            ClientSize = new Size(424, 198);
            Controls.Add(_txtCommunicationFactor);
            Controls.Add(_cbBaseUnit);
            Controls.Add(_cbRelatedUnit);
            Controls.Add(Label2);
            Controls.Add(Label1);
            Controls.Add(Panel1);
            Controls.Add(lblName);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Location = new Point(50, 50);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmUnitsRelation";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " مشخصات ارتباط بین واحد های سنجش";
            Panel1.ResumeLayout(false);
            Load += new EventHandler(Form_Load);
            FormClosing += new FormClosingEventHandler(frmOperationTitle_FormClosing);
            ResumeLayout(false);
            PerformLayout();
        }

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

        private TextBox _txtCommunicationFactor;

        internal TextBox txtCommunicationFactor
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtCommunicationFactor;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtCommunicationFactor != null)
                {
                    _txtCommunicationFactor.KeyPress -= Controls_KeyPress;
                }

                _txtCommunicationFactor = value;
                if (_txtCommunicationFactor != null)
                {
                    _txtCommunicationFactor.KeyPress += Controls_KeyPress;
                }
            }
        }

        internal Label lblName;
        private ComboBox _cbRelatedUnit;

        internal ComboBox cbRelatedUnit
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cbRelatedUnit;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cbRelatedUnit != null)
                {
                    _cbRelatedUnit.KeyPress -= Controls_KeyPress;
                }

                _cbRelatedUnit = value;
                if (_cbRelatedUnit != null)
                {
                    _cbRelatedUnit.KeyPress += Controls_KeyPress;
                }
            }
        }

        private ComboBox _cbBaseUnit;

        internal ComboBox cbBaseUnit
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cbBaseUnit;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cbBaseUnit != null)
                {
                    _cbBaseUnit.KeyPress -= Controls_KeyPress;
                }

                _cbBaseUnit = value;
                if (_cbBaseUnit != null)
                {
                    _cbBaseUnit.KeyPress += Controls_KeyPress;
                }
            }
        }

        internal Label Label1;
        internal Label Label2;
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
    }
}