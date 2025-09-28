using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ProductionPlanning
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class frmPrintPreview : Form
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
            CrystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            SuspendLayout();
            // 
            // CrystalReportViewer1
            // 
            CrystalReportViewer1.ActiveViewIndex = -1;
            CrystalReportViewer1.BorderStyle = BorderStyle.FixedSingle;
            CrystalReportViewer1.DisplayGroupTree = false;
            CrystalReportViewer1.Dock = DockStyle.Fill;
            CrystalReportViewer1.Location = new Point(0, 0);
            CrystalReportViewer1.Name = "CrystalReportViewer1";
            CrystalReportViewer1.SelectionFormula = "";
            CrystalReportViewer1.Size = new Size(915, 331);
            CrystalReportViewer1.TabIndex = 0;
            CrystalReportViewer1.ViewTimeSelectionFormula = "";
            // 
            // frmPrintPreview
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(915, 331);
            Controls.Add(CrystalReportViewer1);
            KeyPreview = true;
            Name = "frmPrintPreview";
            RightToLeft = RightToLeft.Yes;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "پرینت";
            Load += new EventHandler(PrintPreview_Load);
            KeyDown += new KeyEventHandler(frmPrintPreview_KeyDown);
            ResumeLayout(false);
        }

        internal CrystalDecisions.Windows.Forms.CrystalReportViewer CrystalReportViewer1;
    }
}