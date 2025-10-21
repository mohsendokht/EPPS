using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmCopyProductTree : Form
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
            GroupBox1 = new GroupBox();
            _cbSourceTree = new ComboBox();
            _cbSourceTree.SelectedValueChanged += new EventHandler(cbSourceTree_SelectedValueChanged);
            Label2 = new Label();
            _cbSourceProduct = new ComboBox();
            _cbSourceProduct.SelectedValueChanged += new EventHandler(cbSourceProduct_SelectedValueChanged);
            Label1 = new Label();
            GroupBox2 = new GroupBox();
            txtTreeCode = new NumericUpDown();
            Label4 = new Label();
            chkDefualt = new CheckBox();
            txtTreeTitle = new TextBox();
            Label5 = new Label();
            _cbDestinationProduct = new ComboBox();
            _cbDestinationProduct.SelectedValueChanged += new EventHandler(cbDestinationProduct_SelectedValueChanged);
            Label6 = new Label();
            Panel1 = new System.Windows.Forms.Panel();
            _cmdSave = new System.Windows.Forms.Button();
            _cmdSave.Click += new EventHandler(cmdSave_Click);
            cmdExit = new System.Windows.Forms.Button();
            GroupBox1.SuspendLayout();
            GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtTreeCode).BeginInit();
            Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // GroupBox1
            // 
            GroupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox1.Controls.Add(_cbSourceTree);
            GroupBox1.Controls.Add(Label2);
            GroupBox1.Controls.Add(_cbSourceProduct);
            GroupBox1.Controls.Add(Label1);
            GroupBox1.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            GroupBox1.Location = new Point(12, 12);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(440, 117);
            GroupBox1.TabIndex = 0;
            GroupBox1.TabStop = false;
            GroupBox1.Text = "مشخصات درخت مبداء";
            // 
            // cbSourceTree
            // 
            _cbSourceTree.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _cbSourceTree.DropDownStyle = ComboBoxStyle.DropDownList;
            _cbSourceTree.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cbSourceTree.FormattingEnabled = true;
            _cbSourceTree.Location = new Point(15, 71);
            _cbSourceTree.Name = "_cbSourceTree";
            _cbSourceTree.Size = new Size(327, 21);
            _cbSourceTree.TabIndex = 177;
            // 
            // Label2
            // 
            Label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label2.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label2.Location = new Point(348, 71);
            Label2.Name = "Label2";
            Label2.RightToLeft = RightToLeft.Yes;
            Label2.Size = new Size(80, 19);
            Label2.TabIndex = 176;
            Label2.Text = "درخت مبداء:";
            // 
            // cbSourceProduct
            // 
            _cbSourceProduct.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _cbSourceProduct.DropDownStyle = ComboBoxStyle.DropDownList;
            _cbSourceProduct.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cbSourceProduct.FormattingEnabled = true;
            _cbSourceProduct.Location = new Point(15, 35);
            _cbSourceProduct.Name = "_cbSourceProduct";
            _cbSourceProduct.Size = new Size(327, 21);
            _cbSourceProduct.TabIndex = 175;
            // 
            // Label1
            // 
            Label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label1.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label1.Location = new Point(348, 35);
            Label1.Name = "Label1";
            Label1.RightToLeft = RightToLeft.Yes;
            Label1.Size = new Size(80, 19);
            Label1.TabIndex = 174;
            Label1.Text = "محصول مبداء:";
            // 
            // GroupBox2
            // 
            GroupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox2.Controls.Add(txtTreeCode);
            GroupBox2.Controls.Add(Label4);
            GroupBox2.Controls.Add(chkDefualt);
            GroupBox2.Controls.Add(txtTreeTitle);
            GroupBox2.Controls.Add(Label5);
            GroupBox2.Controls.Add(_cbDestinationProduct);
            GroupBox2.Controls.Add(Label6);
            GroupBox2.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            GroupBox2.Location = new Point(12, 135);
            GroupBox2.Name = "GroupBox2";
            GroupBox2.Size = new Size(440, 185);
            GroupBox2.TabIndex = 178;
            GroupBox2.TabStop = false;
            GroupBox2.Text = "مشخصات درخت مقصد(جدید)";
            // 
            // txtTreeCode
            // 
            txtTreeCode.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtTreeCode.Location = new Point(212, 71);
            txtTreeCode.Maximum = new decimal(new int[] { 2147483647, 0, 0, 0 });
            txtTreeCode.Name = "txtTreeCode";
            txtTreeCode.Size = new Size(130, 22);
            txtTreeCode.TabIndex = 180;
            // 
            // Label4
            // 
            Label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label4.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label4.Location = new Point(348, 71);
            Label4.Name = "Label4";
            Label4.RightToLeft = RightToLeft.Yes;
            Label4.Size = new Size(80, 19);
            Label4.TabIndex = 179;
            Label4.Text = "کد درخت:";
            // 
            // chkDefualt
            // 
            chkDefualt.FlatStyle = FlatStyle.System;
            chkDefualt.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            chkDefualt.Location = new Point(15, 144);
            chkDefualt.Name = "chkDefualt";
            chkDefualt.RightToLeft = RightToLeft.No;
            chkDefualt.Size = new Size(148, 19);
            chkDefualt.TabIndex = 178;
            chkDefualt.Text = "درخت پیش فرض محصول";
            chkDefualt.TextAlign = ContentAlignment.MiddleRight;
            chkDefualt.UseVisualStyleBackColor = true;
            // 
            // txtTreeTitle
            // 
            txtTreeTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtTreeTitle.BackColor = Color.White;
            txtTreeTitle.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            txtTreeTitle.Location = new Point(15, 107);
            txtTreeTitle.Name = "txtTreeTitle";
            txtTreeTitle.Size = new Size(327, 21);
            txtTreeTitle.TabIndex = 177;
            // 
            // Label5
            // 
            Label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label5.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label5.Location = new Point(348, 107);
            Label5.Name = "Label5";
            Label5.RightToLeft = RightToLeft.Yes;
            Label5.Size = new Size(80, 19);
            Label5.TabIndex = 176;
            Label5.Text = "عنوان درخت:";
            // 
            // cbDestinationProduct
            // 
            _cbDestinationProduct.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _cbDestinationProduct.DropDownStyle = ComboBoxStyle.DropDownList;
            _cbDestinationProduct.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cbDestinationProduct.FormattingEnabled = true;
            _cbDestinationProduct.Location = new Point(15, 35);
            _cbDestinationProduct.Name = "_cbDestinationProduct";
            _cbDestinationProduct.Size = new Size(327, 21);
            _cbDestinationProduct.TabIndex = 175;
            // 
            // Label6
            // 
            Label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label6.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            Label6.Location = new Point(348, 35);
            Label6.Name = "Label6";
            Label6.RightToLeft = RightToLeft.Yes;
            Label6.Size = new Size(80, 19);
            Label6.TabIndex = 174;
            Label6.Text = "محصول مقصد:";
            // 
            // Panel1
            // 
            Panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Panel1.BackColor = SystemColors.Control;
            Panel1.BorderStyle = BorderStyle.FixedSingle;
            Panel1.Controls.Add(_cmdSave);
            Panel1.Controls.Add(cmdExit);
            Panel1.ForeColor = Color.Black;
            Panel1.Location = new Point(12, 330);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(440, 48);
            Panel1.TabIndex = 179;
            // 
            // cmdSave
            // 
            _cmdSave.BackColor = Color.Transparent;
            _cmdSave.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdSave.ForeColor = Color.Black;
            _cmdSave.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdSave.Location = new Point(238, 10);
            _cmdSave.Name = "_cmdSave";
            _cmdSave.RightToLeft = RightToLeft.No;
            _cmdSave.Size = new Size(90, 28);
            _cmdSave.TabIndex = 6;
            _cmdSave.Text = "تأیید";
            _cmdSave.TextAlign = ContentAlignment.MiddleRight;
            _cmdSave.UseVisualStyleBackColor = false;
            // 
            // cmdExit
            // 
            cmdExit.BackColor = Color.Transparent;
            cmdExit.DialogResult = DialogResult.Cancel;
            cmdExit.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            cmdExit.ForeColor = Color.Red;
            cmdExit.ImageAlign = ContentAlignment.MiddleLeft;
            cmdExit.Location = new Point(111, 10);
            cmdExit.Name = "cmdExit";
            cmdExit.RightToLeft = RightToLeft.No;
            cmdExit.Size = new Size(90, 28);
            cmdExit.TabIndex = 5;
            cmdExit.Text = "انصراف";
            cmdExit.TextAlign = ContentAlignment.MiddleRight;
            cmdExit.UseVisualStyleBackColor = false;
            // 
            // frmCopyProductTree
            // 
            AcceptButton = _cmdSave;
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cmdExit;
            ClientSize = new Size(464, 385);
            Controls.Add(Panel1);
            Controls.Add(GroupBox2);
            Controls.Add(GroupBox1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmCopyProductTree";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " ایجاد کپی از درخت محصول";
            GroupBox1.ResumeLayout(false);
            GroupBox2.ResumeLayout(false);
            GroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtTreeCode).EndInit();
            Panel1.ResumeLayout(false);
            Load += new EventHandler(frmCopyProductTree_Load);
            FormClosing += new FormClosingEventHandler(frmCopyProductTree_FormClosing);
            ResumeLayout(false);
        }

        internal GroupBox GroupBox1;
        internal GroupBox GroupBox2;
        private ComboBox _cbSourceTree;

        internal ComboBox cbSourceTree
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cbSourceTree;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cbSourceTree != null)
                {
                    _cbSourceTree.SelectedValueChanged -= cbSourceTree_SelectedValueChanged;
                }

                _cbSourceTree = value;
                if (_cbSourceTree != null)
                {
                    _cbSourceTree.SelectedValueChanged += cbSourceTree_SelectedValueChanged;
                }
            }
        }

        internal Label Label2;
        private ComboBox _cbSourceProduct;

        internal ComboBox cbSourceProduct
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cbSourceProduct;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cbSourceProduct != null)
                {
                    _cbSourceProduct.SelectedValueChanged -= cbSourceProduct_SelectedValueChanged;
                }

                _cbSourceProduct = value;
                if (_cbSourceProduct != null)
                {
                    _cbSourceProduct.SelectedValueChanged += cbSourceProduct_SelectedValueChanged;
                }
            }
        }

        internal Label Label1;
        internal Label Label5;
        private ComboBox _cbDestinationProduct;

        internal ComboBox cbDestinationProduct
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cbDestinationProduct;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cbDestinationProduct != null)
                {
                    _cbDestinationProduct.SelectedValueChanged -= cbDestinationProduct_SelectedValueChanged;
                }

                _cbDestinationProduct = value;
                if (_cbDestinationProduct != null)
                {
                    _cbDestinationProduct.SelectedValueChanged += cbDestinationProduct_SelectedValueChanged;
                }
            }
        }

        internal Label Label6;
        internal TextBox txtTreeTitle;
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

        internal System.Windows.Forms.Button cmdExit;
        internal CheckBox chkDefualt;
        internal NumericUpDown txtTreeCode;
        internal Label Label4;
    }
}