using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static ProductionPlanning.MyEnums;

namespace ProductionPlanning.ToolForms
{
    public partial class frmMessage : ProductionPlanning.ToolForms.MyForm
    {
       
        public frmMessage(string Message, MessageBoxButtons buttons)
        {
            InitializeComponent();
            MsgBox.Text = Message;
            switch (buttons)
            {
                
                //case MessageBoxButtons.YesNoCancel:
                //    break;
                case MessageBoxButtons.YesNo:
                    YesBtn.Visible = true;
                    NoBtn.Visible = true;
                    break;
               
                default:
                    OkBtn.Visible = true;
                    break;
            }
        }

        private void YesBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }

        private void NoBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
