using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmOperationBuiltGraphicView : Form
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
            Panel1 = new System.Windows.Forms.Panel();
            _pbDiagram = new PictureBox();
            _pbDiagram.GotFocus += new EventHandler(pbDiagram_GotFocus);
            _pbDiagram.MouseClick += new MouseEventHandler(pbDiagram_MouseClick);
            _pbDiagram.MouseDown += new MouseEventHandler(pbDiagram_MouseDown);
            _pbDiagram.MouseMove += new MouseEventHandler(pbDiagram_MouseMove);
            _pbDiagram.MouseUp += new MouseEventHandler(pbDiagram_MouseUp);
            _pbDiagram.Paint += new PaintEventHandler(pbDiagram_Paint);
            Panel2 = new System.Windows.Forms.Panel();
            _cmdPrintDiagram = new System.Windows.Forms.Button();
            _cmdPrintDiagram.Click += new EventHandler(cmdPrintDiagram_Click);
            _cmdExit = new System.Windows.Forms.Button();
            _cmdExit.Click += new EventHandler(cmdExit_Click);
            ProgressBar1 = new System.Windows.Forms.ProgressBar();
            Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_pbDiagram).BeginInit();
            Panel2.SuspendLayout();
            SuspendLayout();
            // 
            // Panel1
            // 
            Panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            Panel1.AutoScroll = true;
            Panel1.BorderStyle = BorderStyle.FixedSingle;
            Panel1.Controls.Add(_pbDiagram);
            Panel1.Location = new Point(12, 78);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(788, 384);
            Panel1.TabIndex = 14;
            // 
            // pbDiagram
            // 
            _pbDiagram.BackColor = Color.WhiteSmoke;
            _pbDiagram.Location = new Point(3, 3);
            _pbDiagram.Name = "_pbDiagram";
            _pbDiagram.Size = new Size(1700, 2000);
            _pbDiagram.TabIndex = 13;
            _pbDiagram.TabStop = false;
            // 
            // Panel2
            // 
            Panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Panel2.BackColor = SystemColors.Control;
            Panel2.BorderStyle = BorderStyle.FixedSingle;
            Panel2.Controls.Add(_cmdPrintDiagram);
            Panel2.Controls.Add(_cmdExit);
            Panel2.Location = new Point(12, 12);
            Panel2.Name = "Panel2";
            Panel2.Size = new Size(788, 55);
            Panel2.TabIndex = 88;
            // 
            // cmdPrintDiagram
            // 
            _cmdPrintDiagram.BackColor = Color.Transparent;
            _cmdPrintDiagram.DialogResult = DialogResult.Cancel;
            _cmdPrintDiagram.Font = new Font("Tahoma", 9.0f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdPrintDiagram.ForeColor = Color.Black;
            _cmdPrintDiagram.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdPrintDiagram.Location = new Point(128, 13);
            _cmdPrintDiagram.Name = "_cmdPrintDiagram";
            _cmdPrintDiagram.RightToLeft = RightToLeft.No;
            _cmdPrintDiagram.Size = new Size(100, 31);
            _cmdPrintDiagram.TabIndex = 2;
            _cmdPrintDiagram.Text = "چاپ";
            _cmdPrintDiagram.UseVisualStyleBackColor = false;
            // 
            // cmdExit
            // 
            _cmdExit.BackColor = Color.Transparent;
            _cmdExit.DialogResult = DialogResult.Cancel;
            _cmdExit.Font = new Font("Tahoma", 9.0f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdExit.ForeColor = Color.Red;
            _cmdExit.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdExit.Location = new Point(7, 13);
            _cmdExit.Name = "_cmdExit";
            _cmdExit.RightToLeft = RightToLeft.No;
            _cmdExit.Size = new Size(100, 31);
            _cmdExit.TabIndex = 0;
            _cmdExit.Text = "خروج";
            _cmdExit.UseVisualStyleBackColor = false;
            // 
            // ProgressBar1
            // 
            ProgressBar1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ProgressBar1.Location = new Point(600, 466);
            ProgressBar1.Name = "ProgressBar1";
            ProgressBar1.Size = new Size(200, 12);
            ProgressBar1.Step = 5;
            ProgressBar1.TabIndex = 89;
            ProgressBar1.Visible = false;
            // 
            // frmOperationBuiltGraphicView
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _cmdExit;
            ClientSize = new Size(812, 481);
            Controls.Add(ProgressBar1);
            Controls.Add(Panel2);
            Controls.Add(Panel1);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            MinimizeBox = false;
            Name = "frmOperationBuiltGraphicView";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " نمایش گرافیکی عملیات ساخت محصول";
            WindowState = FormWindowState.Maximized;
            Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_pbDiagram).EndInit();
            Panel2.ResumeLayout(false);
            Load += new EventHandler(frmOperationBuiltGraphicView_Load);
            FormClosing += new FormClosingEventHandler(frmOperationBuiltGraphicView_FormClosing);
            ResumeLayout(false);
        }

        internal System.Windows.Forms.Panel Panel1;
        private PictureBox _pbDiagram;

        internal PictureBox pbDiagram
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _pbDiagram;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_pbDiagram != null)
                {
                    _pbDiagram.GotFocus -= pbDiagram_GotFocus;
                    _pbDiagram.MouseClick -= pbDiagram_MouseClick;
                    _pbDiagram.MouseDown -= pbDiagram_MouseDown;
                    _pbDiagram.MouseMove -= pbDiagram_MouseMove;
                    _pbDiagram.MouseUp -= pbDiagram_MouseUp;
                    _pbDiagram.Paint -= pbDiagram_Paint;
                }

                _pbDiagram = value;
                if (_pbDiagram != null)
                {
                    _pbDiagram.GotFocus += pbDiagram_GotFocus;
                    _pbDiagram.MouseClick += pbDiagram_MouseClick;
                    _pbDiagram.MouseDown += pbDiagram_MouseDown;
                    _pbDiagram.MouseMove += pbDiagram_MouseMove;
                    _pbDiagram.MouseUp += pbDiagram_MouseUp;
                    _pbDiagram.Paint += pbDiagram_Paint;
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

        internal System.Windows.Forms.ProgressBar ProgressBar1;
        private System.Windows.Forms.Button _cmdPrintDiagram;

        internal System.Windows.Forms.Button cmdPrintDiagram
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdPrintDiagram;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdPrintDiagram != null)
                {
                    _cmdPrintDiagram.Click -= cmdPrintDiagram_Click;
                }

                _cmdPrintDiagram = value;
                if (_cmdPrintDiagram != null)
                {
                    _cmdPrintDiagram.Click += cmdPrintDiagram_Click;
                }
            }
        }
    }
}