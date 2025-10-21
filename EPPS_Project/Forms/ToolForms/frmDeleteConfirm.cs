using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductionPlanning.ToolForms
{
    public partial class frmDeleteConfirm : Form
    {
        public frmDeleteConfirm()
        {
            InitializeComponent();
        }

        public string DeleteCode { get; set; }
        public string DeleteMessage { get; set; }
        public bool Confirmed { get; set; }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Confirmed = false;
            Close();
        }

        private void DeleteConfirm_Load(object sender, EventArgs e)
        {
            DeleteMessagelabel.Text = DeleteMessage;
            ConfirmBtn.Enabled = false;

            if (DeleteMessage == "")
            {
                TB_DeleteCode.Visible = false;
                DeleteMessagelabel.Text = "آیا حذف اطلاعات را تایید میکنید؟";
            }
        }

        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            if (TB_DeleteCode.Text == "")
            {

            }
            if (DeleteCode == TB_DeleteCode.Text)
            {
                Confirmed = true;
                Close();
            }
          
        }

        private void TB_DeleteCode_TextChanged(object sender, EventArgs e)
        {
            if (DeleteCode == TB_DeleteCode.Text)
            {
                ConfirmBtn.Enabled = true;
                ConfirmBtn.BackColor = Color.Salmon;
            }
        }
    }
}
