using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmProductionHaltList
    {
        public frmProductionHaltList()
        {
            InitializeComponent();
            _cmdExit.Name = "cmdExit";
            _cmdDelete.Name = "cmdDelete";
            _cmdUpdate.Name = "cmdUpdate";
            _cmdInsert.Name = "cmdInsert";
        }

        private DataSet mdsProductionHalt;
        private DataRow mProductionCurrentRow;
        private DataView dvHaltList;

        public DataSet dsProductionHalt
        {
            set
            {
                mdsProductionHalt = value;
            }
        }

        public DataRow ProductionCurrentRow
        {
            set
            {
                mProductionCurrentRow = value;
            }
        }

        private void frmRealProduction_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdInsert, 0);
            Module1.SetButtonsImage(cmdDelete, 1);
            Module1.SetButtonsImage(cmdUpdate, 2);
            dgList.Sorted += Module1.DataGridViews_Sorted_EventHandler;
            FormLoad();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            if (dgList.Rows.Count == 0 && !ReferenceEquals(sender, cmdInsert))
            {
                MessageBox.Show("رکوردی وجود ندارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                return;
            }

            if (dgList.Rows.Count > 0 && !ReferenceEquals(sender, cmdInsert) && dgList.CurrentRow is null)
            {
                MessageBox.Show("رکوردی انتخاب نشده است", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                return;
            }

            {
                var withBlock = My.MyProject.Forms.frmProductionHalt;
                withBlock.ProductionCurrentRow = mProductionCurrentRow;
                withBlock.dsProductionHalt = mdsProductionHalt;
                if (ReferenceEquals(sender, cmdInsert))
                {
                    withBlock.FormMode = (int)Module1.FormModeEnum.INSERT_MODE;
                }
                else if (ReferenceEquals(sender, cmdUpdate))
                {
                    withBlock.FormMode = (int)Module1.FormModeEnum.EDIT_MODE;
                    withBlock.CurrentRow = mdsProductionHalt.Tables["Tbl_ProductionHalts"].Select(Conversions.ToString(Operators.ConcatenateObject("HaltID=", dgList.CurrentRow.Cells["HaltID"].Value)))[0];
                }
                else if (ReferenceEquals(sender, cmdDelete))
                {
                    withBlock.FormMode = (int)Module1.FormModeEnum.DELETE_MODE;
                    withBlock.CurrentRow = mdsProductionHalt.Tables["Tbl_ProductionHalts"].Select(Conversions.ToString(Operators.ConcatenateObject("HaltID=", dgList.CurrentRow.Cells["HaltID"].Value)))[0];
                }

                if (ReferenceEquals(sender, cmdDelete))
                {
                    withBlock.Controls["Panel1"].Controls["cmdSave"].Visible = false;
                    withBlock.Controls["Panel1"].Controls["cmdDelete"].Visible = true;
                }

                withBlock.ShowDialog();
                withBlock.Dispose();
            }
        }

        private void FormLoad()
        {
            try
            {
                lblSubbatch.Text = Conversions.ToString(mProductionCurrentRow["SubbatchCode"]);
                lblOperation.Text = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(mProductionCurrentRow["OperationCode"], " {"), mProductionCurrentRow["OperationTitle"]), "}"));
                lblOperator.Text = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(mProductionCurrentRow["OperatorCode"], " {"), mProductionCurrentRow["OperatorName"]), "}"));
                lblMachine.Text = Conversions.ToString(mProductionCurrentRow["MachineCode"]);
                dvHaltList = mdsProductionHalt.Tables["Tbl_ProductionHalts"].DefaultView;
                dvHaltList.RowFilter = Conversions.ToString(Operators.ConcatenateObject("ProductionCode=", mProductionCurrentRow["ProductionCode"]));
                SetGridColumns();
            }
            catch (Exception objEx)
            {
                Logger.SaveError(Name + ".FormLoad", objEx.Message);
                MessageBox.Show("فراخوانی فرم با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetGridColumns()
        {
            {
                var withBlock = dgList;
                withBlock.DataSource = dvHaltList;
                withBlock.Columns[0].Visible = false;
                withBlock.Columns[1].HeaderText = "تاریخ شروع";
                withBlock.Columns[1].Width = 100;
                withBlock.Columns[1].DefaultCellStyle.Format = "####/##/##";
                withBlock.Columns[2].HeaderText = "ساعت شروع";
                withBlock.Columns[2].Width = 100;
                withBlock.Columns[3].HeaderText = "تاریخ پایان";
                withBlock.Columns[3].Width = 100;
                withBlock.Columns[3].DefaultCellStyle.Format = "####/##/##";
                withBlock.Columns[4].HeaderText = "ساعت پایان";
                withBlock.Columns[4].Width = 100;
                withBlock.Columns[5].HeaderText = "طول توقف";
                withBlock.Columns[5].Width = 100;
                withBlock.Columns[6].Visible = false;
            }
        }
    }
}