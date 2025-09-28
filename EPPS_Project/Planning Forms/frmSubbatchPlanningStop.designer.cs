using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    [DesignerGenerated()]
    public partial class frmSubbatchPlanningStop : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components is object)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            _TreeView1 = new System.Windows.Forms.TreeView();
            _TreeView1.BeforeCheck += new TreeViewCancelEventHandler(TreeView1_BeforeCheck);
            _TreeView1.AfterCheck += new TreeViewEventHandler(TreeView1_AfterCheck);
            _cmdOK = new System.Windows.Forms.Button();
            _cmdOK.Click += new EventHandler(cmdOK_Click);
            cmdCancel = new System.Windows.Forms.Button();
            _cmdReloadTree = new System.Windows.Forms.Button();
            _cmdReloadTree.Click += new EventHandler(cmdReloadTree_Click);
            SuspendLayout();
            // 
            // TreeView1
            // 
            _TreeView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            _TreeView1.CheckBoxes = true;
            _TreeView1.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _TreeView1.FullRowSelect = true;
            _TreeView1.Location = new Point(5, 6);
            _TreeView1.Margin = new Padding(3, 4, 3, 4);
            _TreeView1.Name = "_TreeView1";
            _TreeView1.RightToLeftLayout = true;
            _TreeView1.Size = new Size(473, 409);
            _TreeView1.TabIndex = 0;
            // 
            // cmdOK
            // 
            _cmdOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _cmdOK.BackColor = Color.Transparent;
            _cmdOK.DialogResult = DialogResult.OK;
            _cmdOK.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdOK.ForeColor = Color.Black;
            _cmdOK.ImageAlign = ContentAlignment.MiddleLeft;
            _cmdOK.Location = new Point(142, 428);
            _cmdOK.Margin = new Padding(3, 4, 3, 4);
            _cmdOK.Name = "_cmdOK";
            _cmdOK.RightToLeft = RightToLeft.No;
            _cmdOK.Size = new Size(105, 27);
            _cmdOK.TabIndex = 157;
            _cmdOK.Text = "توقف";
            _cmdOK.TextAlign = ContentAlignment.MiddleRight;
            _cmdOK.UseVisualStyleBackColor = false;
            // 
            // cmdCancel
            // 
            cmdCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cmdCancel.BackColor = Color.Transparent;
            cmdCancel.DialogResult = DialogResult.Cancel;
            cmdCancel.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            cmdCancel.ForeColor = Color.Black;
            cmdCancel.ImageAlign = ContentAlignment.MiddleLeft;
            cmdCancel.Location = new Point(14, 428);
            cmdCancel.Margin = new Padding(3, 4, 3, 4);
            cmdCancel.Name = "cmdCancel";
            cmdCancel.RightToLeft = RightToLeft.No;
            cmdCancel.Size = new Size(105, 27);
            cmdCancel.TabIndex = 156;
            cmdCancel.Text = "انصراف";
            cmdCancel.TextAlign = ContentAlignment.MiddleRight;
            cmdCancel.UseVisualStyleBackColor = false;
            // 
            // cmdReloadTree
            // 
            _cmdReloadTree.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _cmdReloadTree.BackColor = Color.Transparent;
            _cmdReloadTree.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            _cmdReloadTree.ForeColor = Color.Black;
            _cmdReloadTree.Location = new Point(441, 428);
            _cmdReloadTree.Margin = new Padding(3, 4, 3, 4);
            _cmdReloadTree.Name = "_cmdReloadTree";
            _cmdReloadTree.RightToLeft = RightToLeft.No;
            _cmdReloadTree.Size = new Size(27, 27);
            _cmdReloadTree.TabIndex = 158;
            _cmdReloadTree.TextAlign = ContentAlignment.MiddleRight;
            _cmdReloadTree.UseVisualStyleBackColor = false;
            // 
            // frmSubbatchPlanningStop
            // 
            AutoScaleDimensions = new SizeF(7.0f, 16.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cmdCancel;
            ClientSize = new Size(483, 465);
            Controls.Add(_cmdReloadTree);
            Controls.Add(_cmdOK);
            Controls.Add(cmdCancel);
            Controls.Add(_TreeView1);
            Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(178));
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmSubbatchPlanningStop";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "  انتخاب نقاط توقف ساب بچ";
            Load += new EventHandler(frmSubbatchPlanningStop_Load);
            FormClosing += new FormClosingEventHandler(frmSubbatchPlanningStop_FormClosing);
            ResumeLayout(false);
        }

        private System.Windows.Forms.TreeView _TreeView1;

        internal System.Windows.Forms.TreeView TreeView1
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _TreeView1;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_TreeView1 != null)
                {
                    _TreeView1.BeforeCheck -= TreeView1_BeforeCheck;
                    _TreeView1.AfterCheck -= TreeView1_AfterCheck;
                }

                _TreeView1 = value;
                if (_TreeView1 != null)
                {
                    _TreeView1.BeforeCheck += TreeView1_BeforeCheck;
                    _TreeView1.AfterCheck += TreeView1_AfterCheck;
                }
            }
        }

        private System.Windows.Forms.Button _cmdOK;

        internal System.Windows.Forms.Button cmdOK
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdOK;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdOK != null)
                {
                    _cmdOK.Click -= cmdOK_Click;
                }

                _cmdOK = value;
                if (_cmdOK != null)
                {
                    _cmdOK.Click += cmdOK_Click;
                }
            }
        }

        internal System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button _cmdReloadTree;

        internal System.Windows.Forms.Button cmdReloadTree
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _cmdReloadTree;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_cmdReloadTree != null)
                {
                    _cmdReloadTree.Click -= cmdReloadTree_Click;
                }

                _cmdReloadTree = value;
                if (_cmdReloadTree != null)
                {
                    _cmdReloadTree.Click += cmdReloadTree_Click;
                }
            }
        }
    }
}