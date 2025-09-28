using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmOrdersList
    {
        public frmOrdersList()
        {
            InitializeComponent();
            _chkUnProduction.Name = "chkUnProduction";
            _chkProductionDone.Name = "chkProductionDone";
            _chkNotConfirmed.Name = "chkNotConfirmed";
            _chkConfirmed.Name = "chkConfirmed";
            _chkNotAllowed.Name = "chkNotAllowed";
            _chkAllowed.Name = "chkAllowed";
            _cmdShowFiltered.Name = "cmdShowFiltered";
            
            _chkAllRows.Name = "chkAllRows";
            _cmdExit.Name = "cmdExit";
            _cmdDelete.Name = "cmdDelete";
            _cmdUpdate.Name = "cmdUpdate";
            _cmdInsert.Name = "cmdInsert";
            _dgList.Name = "dgList";
            _cmsiProductionDone.Name = "cmsiProductionDone";
            _cmsiCancelProductionDone.Name = "cmsiCancelProductionDone";
            _cmdConfirm.Name = "cmdConfirm";
            _cmdAllow.Name = "cmdAllow";
        }

        public DataSetConfiguration DataSetConfig = new DataSetConfiguration();
        private DataView dvOrdersList = new DataView();
        private SqlDataAdapter daProgress = new SqlDataAdapter();
        private int mFormMode;
        private int mSearchMode = -1;
        //private DataRow[] FoundRows = null;
        //private IEnumerator FoundRowsEnumerator;
        //private bool IsLoading = false;

        public int FormMode
        {
            get
            {
                return mFormMode;
            }

            set
            {
                mFormMode = value;
            }
        }
        // مشخص کننده نوع جستجو(فیلتر یا جستجو) می باشد
        public int SearchMode
        {
            get
            {
                return mSearchMode;
            }

            set
            {
                mSearchMode = value;
            }
        }

        public DataSet dsProductionPlanning
        {
            get
            {
                return DataSetConfig.dsProductionPlanning;
            }
        }

        public enum SearchModeEnum
        {
            SM_FIND,
            SM_FILTER
        }

        private void frmOrdersList_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdInsert, 0);
            Module1.SetButtonsImage(cmdDelete, 1);
            Module1.SetButtonsImage(cmdUpdate, 2);
            dgList.Sorted += Module1.DataGridViews_Sorted_EventHandler;
            //this.IsLoading = true;
            dgList.Tag = -1;
            string SelectStr = 
                "Select A.OrderIndex, A.RegisterDate, A.OrderNo, A.OrderDate, A.CustomerCode, B.CustomerName, A.ProductCode, C.ProductName," + 
                "       A.ProductTechnicalSpecification, A.OrderQuantity, A.DeliveryDueDate, A.ContractNo," +
                "       A.ConfirmFlag, Case A.ConfirmFlag When 0 Then 'تایید نشده' When 1 Then 'تایید شده' End As ConfirmFlagTitle," +
                "       A.AllowFlag, Case A.AllowFlag When 0 Then 'تصویب نشده' When 1 Then 'تصویب شده' End As AllowFlagTitle, A.ProductionCallDate,A.IsDone " +
                "From   dbo.Tbl_CustomerOrders A INNER JOIN " + "       dbo.Tbl_Customers B ON A.CustomerCode = B.CustomerCode INNER JOIN " +
                "       dbo.Tbl_Products C ON A.ProductCode = C.ProductCode";


            DataSetConfig.FillDataSet("Tbl_CustomerOrders", "Tbl_CustomerOrders", SelectStr, "OrderIndex");
            dvOrdersList = dsProductionPlanning.Tables["Tbl_CustomerOrders"].DefaultView;
            dvOrdersList.RowFilter = "OrderIndex = -1";
            dsProductionPlanning.Tables["Tbl_CustomerOrders"].Columns["ConfirmFlagTitle"].ReadOnly = false;
            dsProductionPlanning.Tables["Tbl_CustomerOrders"].Columns["AllowFlagTitle"].ReadOnly = false;
            DataSetConfig.FillDataSet("Tbl_Products", "Tbl_Products_Combo", "Select ProductCode, ProductName From Tbl_Products", "ProductCode");
            DataSetConfig.FillDataSet("Tbl_Customers", "Tbl_Customers_Combo", "Select CustomerCode, CustomerName From Tbl_Customers", "CustomerCode");
            //{
            //    var withBlock = cbProductCode;
            //    withBlock.DataSource = dsProductionPlanning.Tables["Tbl_Products_Combo"];
            //    withBlock.DisplayMember = "ProductName";
            //    withBlock.ValueMember = "ProductCode";
            //    withBlock.Text = Constants.vbNullString;
            //}
            Product_Lookup.CB_DataSource = dsProductionPlanning.Tables["Tbl_Products_Combo"]; // dataTable1;
            Product_Lookup.CB_AutoComplete = true;
            Product_Lookup.CB_LinkedColumnIndex = 1;
            Product_Lookup.CB_DisplayMember = "ProductCode";
            Product_Lookup.CB_ValueMember = "ProductName";
            Product_Lookup.CB_SerachFromTitle = "محصولات";
            Product_Lookup.CB_SelectedIndex = -1;

            //{
            //    var withBlock1 = cbCustomers;
            //    withBlock1.DataSource = dsProductionPlanning.Tables["Tbl_Customers_Combo"];
            //    withBlock1.DisplayMember = "CustomerName";
            //    withBlock1.ValueMember = "CustomerCode";
            //    withBlock1.Text = Constants.vbNullString;
            //}

            Customer_LookUp.CB_DataSource = dsProductionPlanning.Tables["Tbl_Customers_Combo"]; // dataTable1;
            Customer_LookUp.CB_DisplayMember = "CustomerCode";
            Customer_LookUp.CB_ValueMember = "CustomerName";
            Customer_LookUp.CB_AutoComplete = true;
            Customer_LookUp.CB_LinkedColumnIndex = 1;
            Customer_LookUp.CB_SerachFromTitle = "مشتریان";
            Customer_LookUp.CB_SelectedIndex = -1;

            SetGridColumns(dvOrdersList);

            // '''>>>>  Set User Access Right  <<<<'''''''''''''
            cmdConfirm.Visible = Module_UserAccess.HaveAccessToItem(83);
            cmdAllow.Visible = Module_UserAccess.HaveAccessToItem(84);
            //this.IsLoading = false;

            chkAllRows.Checked = false;
            dvOrdersList.RowFilter = GetFilterValue();
        }

        private void frmOrdersList_FormClosing(object sender, FormClosingEventArgs e)
        {
            Module1.SaveGridColumnsWidth(Name, 0, dgList);
            //FoundRows = null;
            //FoundRowsEnumerator = null;
            dvOrdersList.Dispose();
            DataSetConfig = null;
            daProgress.Dispose();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (ReferenceEquals(sender, cmdInsert))
            {
                mFormMode = (int)Module1.FormModeEnum.INSERT_MODE;
            }
            else if (ReferenceEquals(sender, cmdUpdate))
            {
                mFormMode = (int)Module1.FormModeEnum.EDIT_MODE;
            }
            else if (ReferenceEquals(sender, cmdDelete))
            {
                mFormMode = (int)Module1.FormModeEnum.DELETE_MODE;
            }

            if (dgList.Rows.Count == 0 && mFormMode == (int)Module1.FormModeEnum.EDIT_MODE | mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
            {
                MessageBox.Show("رکوردی وجود ندارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                return;
            }

            switch (mFormMode)
            {
                case (int)Module1.FormModeEnum.EDIT_MODE:
                case (int)Module1.FormModeEnum.DELETE_MODE:
                    {
                        if (Conversions.ToBoolean(dgList.CurrentRow.Cells["ConfirmFlag"].Value))
                        {
                            MessageBox.Show("سفارش انتخاب شده به علت تایید شدن، مجاز به اصلاح یا حذف نمی باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                            return;
                        }
                        else if (Conversions.ToBoolean(dgList.CurrentRow.Cells["AllowFlag"].Value))
                        {
                            MessageBox.Show("سفارش انتخاب شده به علت تصویب شدن، مجاز به اصلاح یا حذف نمی باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                            return;
                        }

                        break;
                    }
            }

            //object objForm = null;
            DataSetConfig.FillDataSet("Tbl_Products", "Tbl_Products", "Select ProductCode, ProductName From Tbl_Products Order By ProductName", "ProductCode");
            DataSetConfig.FillDataSet("Tbl_Customers", "Tbl_Customers", "Select CustomerCode, CustomerName From Tbl_Customers Order By CustomerName", "CustomerCode");
            var objForm = new frmOrder();
            objForm.ListForm = this;
            objForm.ListForm.FormMode = mFormMode;
            if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
            {
                objForm.Controls["Panel1"].Controls["cmdSave"].Visible = false;
                objForm.Controls["Panel1"].Controls["cmdDelete"].Visible = true;
            }

            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(objForm.ShowDialog(), DialogResult.Cancel, false)))
            {
                switch (FormMode)
                {
                    case (int)Module1.FormModeEnum.INSERT_MODE:
                        {
                            BindingContext[dsProductionPlanning, "Tbl_CustomerOrders"].Position = BindingContext[dsProductionPlanning, "Tbl_CustomerOrders"].Count;
                            break;
                        }
                }
            }

            objForm.Dispose();
        }

        private void frmOrdersList_Resize(object sender, EventArgs e)
        {
            tsslSearchMode.Width = (int)Math.Round(0.15d * StatusStrip1.Width);
            ToolStripStatusLabel1.Width = (int)Math.Round(0.14d * StatusStrip1.Width);
            ToolStripStatusLabel2.Width = (int)Math.Round(0.14d * StatusStrip1.Width);
            ToolStripStatusLabel3.Width = (int)Math.Round(0.15d * StatusStrip1.Width);
            ToolStripStatusLabel4.Width = (int)Math.Round(0.15d * StatusStrip1.Width);
            tsslRecNo.Width = (int)Math.Round(0.25d * StatusStrip1.Width);
        }

        private void dgList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            lblOrderNo.Text = "";
            lblProductionCallDate.Text = "";
            lblProductCode.Text = "";
            lblProductName.Text = "";
            lblBatchCode.Text = "";
            lblBatchProgress.Text = "";
            lblBatchProductionQuantity.Text = "";
            lblBatchState.Text = "";
            lblOrderProductionProgress.Text = "";
            lblOrderQuantity.Text = "";
            lblPlanningEnd.Text = "";
            lblRealEnd.Text = "";
            lblBatchQuantity.Text = "";
            cmdConfirm.Enabled = false;
            cmdAllow.Enabled = false;
            int RecordCount = Conversions.ToInteger(Interaction.IIf(dgList.DataSource is DataSet, BindingContext[dsProductionPlanning, "Tbl_CustomerOrders"].Count, dgList.Rows.Count));
            int CurrentRecord = e.RowIndex + 1;
            tsslRecNo.Text = "رکورد  " + CurrentRecord.ToString() + "  از  " + RecordCount.ToString();
            if (dgList.CurrentRow is object && dgList.SelectedRows.Count > 0 && dgList.SelectedRows[0].Cells["OrderIndex"].Value is object)
            {
                using (var cn = new SqlConnection(Module1.PlanningCnnStr))
                {
                    cn.Open();
                    var cmProgress = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject("Select * From View_OrdersProductionProgress Where OrderIndex = ", dgList.SelectedRows[0].Cells["OrderIndex"].Value)), cn);
                    var drProgress = cmProgress.ExecuteReader();
                    if (drProgress.Read())
                    {
                        lblOrderNo.Text = Conversions.ToString(drProgress["OrderNo"]);
                        if (!DBNull.Value.Equals(drProgress["ProductionCallDate"]) && drProgress["ProductionCallDate"] is object && !drProgress["ProductionCallDate"].ToString().Equals(""))
                        {
                            lblProductionCallDate.Text = drProgress["ProductionCallDate"].ToString().Substring(0, 4) + "/" + drProgress["ProductionCallDate"].ToString().Substring(4, 2) + "/" + drProgress["ProductionCallDate"].ToString().Substring(6, 2);
                        }

                        lblBatchCode.Text = Conversions.ToString(drProgress["BatchCode"]);
                        lblOrderQuantity.Text = Conversions.ToString(drProgress["OrderQuantity"]);
                        lblProductCode.Text = Conversions.ToString(drProgress["ProductCode"]);
                        lblProductName.Text = Conversions.ToString(drProgress["ProductName"]);
                        lblBatchQuantity.Text = Conversions.ToString(drProgress["BatchQuantity"]);
                        lblBatchProgress.Text = Conversions.ToString(drProgress["BatchProductionProgress"]);
                        lblBatchProductionQuantity.Text = Conversions.ToString(drProgress["BatchProductionQuantity"]);
                        lblBatchState.Text = Conversions.ToString(drProgress["BatchState"]);
                        lblOrderProductionProgress.Text = Conversions.ToString(drProgress["OrderProgressPercent"]);
                        tmrCalcOrderProgress.Enabled = true;
                        if (!DBNull.Value.Equals(drProgress["PlanningEndDate"]) && drProgress["PlanningEndDate"] is object && !drProgress["PlanningEndDate"].ToString().Equals(""))
                        {
                            lblPlanningEnd.Text = drProgress["PlanningEndDate"].ToString().Substring(0, 4) + "/" + drProgress["PlanningEndDate"].ToString().Substring(4, 2) + "/" + drProgress["PlanningEndDate"].ToString().Substring(6, 2);
                        }

                        if (!DBNull.Value.Equals(drProgress["RealEndDate"]) && drProgress["RealEndDate"] is object && !drProgress["RealEndDate"].ToString().Equals(""))
                        {
                            lblRealEnd.Text = drProgress["RealEndDate"].ToString().Trim().Substring(0, 4) + "/" + drProgress["RealEndDate"].ToString().Trim().Substring(4, 2) + "/" + drProgress["RealEndDate"].ToString().Trim().Substring(6, 2);
                        }
                    }

                    drProgress.Close();
                    cn.Close();
                }

                SetConfirmAllowButtons();
                if (!(lblPlanningEnd.Text is null || lblPlanningEnd.Text.Equals("") || lblPlanningEnd.Text.Equals("#")))
                {
                    cmdAllow.Enabled = false;
                }
            }
            else if (dgList.SelectedRows.Count > 0)
            {
                dgList.SelectedRows[0].Selected = false;
            }
        }

        private void dgList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgList.Columns[e.ColumnIndex].Name.Equals("RegisterDate") || dgList.Columns[e.ColumnIndex].Name.Equals("OrderDate") || dgList.Columns[e.ColumnIndex].Name.Equals("DeliveryDueDate") || dgList.Columns[e.ColumnIndex].Name.Equals("ProductionCallDate"))
            {
                e.Value = Strings.Mid(Conversions.ToString(e.Value), 1, 4) + "/" + Strings.Mid(Conversions.ToString(e.Value), 5, 2) + "/" + Strings.Mid(Conversions.ToString(e.Value), 7, 2);
            }
            else if (e.RowIndex > -1 && dgList.Columns[e.ColumnIndex].Name.Equals("IsDone"))
            {
                if (Conversions.ToBoolean(e.Value))
                {
                    dgList.Rows[e.RowIndex].Cells["OrderNo"].Style.BackColor = Color.Green;
                }
                else
                {
                    dgList.Rows[e.RowIndex].Cells["OrderNo"].Style.BackColor = dgList.Rows[e.RowIndex].Cells["OrderIndex"].Style.BackColor;
                }
            }

            if (e.RowIndex > -1)
            {
                dgList.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
            }
        }

        private void dgList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var objCell = dgList.HitTest(e.X, e.Y);
                if (objCell.RowIndex > -1 && objCell.ColumnIndex > -1)
                {
                    dgList.Rows[objCell.RowIndex].Cells[objCell.ColumnIndex].Selected = true;
                    cmsOrder.Opening += cmsOrder_Opening;
                    cmsOrder.Show(dgList, e.X, e.Y);
                }
            }
        }

        private void cbProductName_SelectedValueChanged(object sender, EventArgs e)
        {
            // If cbProductName.Items.Count > 0 AndAlso Not cbProductName.SelectedValue Is Nothing AndAlso cbProductName.SelectedValue.ToString <> "System.Data.DataRowView" AndAlso Not IsLoading Then
            // txtProductCode.Text = cbProductName.SelectedValue.ToString
            // End If
        }

        private void txtProductCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // If txtProductCode.Text = vbNullString Then
            // cbProductName.Text = vbNullString
            // End If
        }

        private void txtProductCode_TextChanged(object sender, EventArgs e)
        {
            // If txtProductCode.Text = vbNullString Then
            // cbProductName.Text = vbNullString
            // End If

            // cbProductName.SelectedValue = Trim(txtProductCode.Text)
        }

        private void cmsOrder_Opening(object sender, System.ComponentModel.CancelEventArgs e) // Handles cmsOrder.Opening
        {
            cmsOrder.Opening -= cmsOrder_Opening;
            if (!Module_UserAccess.HaveAccessToItem(85))
            {
                e.Cancel = true;
                return;
            }

            if (dgList.SelectedRows.Count > 0)
            {
                if (Conversions.ToBoolean(dgList.SelectedRows[0].Cells["IsDone"].Value))
                {
                    cmsiProductionDone.Visible = false;
                    cmsiCancelProductionDone.Visible = true;
                }
                else
                {
                    cmsiCancelProductionDone.Visible = false;
                    cmsiProductionDone.Visible = true;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void cmsiProductionDone_Click(object sender, EventArgs e)
        {
            SqlTransaction trnProductionDone = null;
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    trnProductionDone = cn.BeginTransaction();
                    var daProductionDone = new SqlDataAdapter();
                    daProductionDone.UpdateCommand = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject("Update Tbl_CustomerOrders Set IsDone = 1 Where OrderIndex = ", dgList.SelectedRows[0].Cells["OrderIndex"].Value)), cn);
                    daProductionDone.UpdateCommand.Transaction = trnProductionDone;
                    var drProductionDone = dsProductionPlanning.Tables["Tbl_CustomerOrders"].Select(Conversions.ToString(Operators.ConcatenateObject("OrderIndex = ", dgList.SelectedRows[0].Cells["OrderIndex"].Value)));
                    if (drProductionDone.Length > 0)
                    {
                        drProductionDone[0].BeginEdit();
                        drProductionDone[0]["IsDone"] = 1;
                        drProductionDone[0].EndEdit();
                        daProductionDone.Update(dsProductionPlanning, "Tbl_CustomerOrders");
                    }

                    trnProductionDone.Commit();
                }
                catch (Exception objEx)
                {
                    dsProductionPlanning.RejectChanges();
                    trnProductionDone.Rollback();
                    Logger.SaveError(Name + ".cmsiProductionDone_Click", objEx.Message);
                    MessageBox.Show("ثبت اتمام تولید سفارش، با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }
        }

        private void cmsiCancelProductionDone_Click(object sender, EventArgs e)
        {
            SqlTransaction trnCancelProductionDone = null;
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    trnCancelProductionDone = cn.BeginTransaction();
                    var daCancelProductionDone = new SqlDataAdapter();
                    daCancelProductionDone.UpdateCommand = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject("Update Tbl_CustomerOrders Set IsDone = 0 Where OrderIndex = ", dgList.SelectedRows[0].Cells["OrderIndex"].Value)), cn);
                    daCancelProductionDone.UpdateCommand.Transaction = trnCancelProductionDone;
                    var drCancelProductionDone = dsProductionPlanning.Tables["Tbl_CustomerOrders"].Select(Conversions.ToString(Operators.ConcatenateObject("OrderIndex = ", dgList.SelectedRows[0].Cells["OrderIndex"].Value)));
                    if (drCancelProductionDone.Length > 0)
                    {
                        drCancelProductionDone[0].BeginEdit();
                        drCancelProductionDone[0]["IsDone"] = 0;
                        drCancelProductionDone[0].EndEdit();
                        daCancelProductionDone.Update(dsProductionPlanning, "Tbl_CustomerOrders");
                    }

                    trnCancelProductionDone.Commit();
                }
                catch (Exception objEx)
                {
                    dsProductionPlanning.RejectChanges();
                    trnCancelProductionDone.Rollback();
                    Logger.SaveError(Name + ".cmsiCancelProductionDone_Click", objEx.Message);
                    MessageBox.Show("لغو اتمام تولید سفارش، با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }
        }

        private void dgList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true & e.KeyCode == Keys.F12)
            {
                Module1.ExportGridToExcel(dgList);
            }
        }

        private void chkConfirmed_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConfirmed.Checked)
            {
                chkNotConfirmed.Checked = false;
            }
        }

        private void chkNotConfirmed_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNotConfirmed.Checked)
            {
                chkConfirmed.Checked = false;
            }
        }

        private void chkAllowed_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllowed.Checked)
            {
                chkNotAllowed.Checked = false;
            }
        }

        private void chkNotAllowed_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNotAllowed.Checked)
            {
                chkAllowed.Checked = false;
            }
        }

        private void chkProductionDone_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProductionDone.Checked)
            {
                chkUnProduction.Checked = false;
            }
        }

        private void chkUnProduction_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUnProduction.Checked)
            {
                chkProductionDone.Checked = false;
            }
        }

        private void cmdShowFiltered_Click(object sender, EventArgs e)
        {
            chkAllRows.Checked = false;
            dvOrdersList.RowFilter = GetFilterValue();
        }

        private void chkAllRows_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllRows.Checked)
            {
                dvOrdersList.RowFilter = Constants.vbNullString;
            }
            else
            {
                string Filter = GetFilterValue();
                if (string.IsNullOrEmpty(Filter))
                {
                    dvOrdersList.RowFilter = "OrderIndex = -1";
                }
                else
                {
                    dvOrdersList.RowFilter = Filter;
                }
            }
        }

        private void cmdConfirm_Click(object sender, EventArgs e)
        {
            if (cmdConfirm.Tag.ToString().Equals("1"))
            {
                var cmConfirm = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject("Update Tbl_CustomerOrders Set ConfirmFlag = 1 Where OrderIndex=", dgList.SelectedRows[0].Cells["OrderIndex"].Value)), Module1.cnProductionPlanning);
                SqlTransaction trnConfirm = null;
                var drConfirm = dsProductionPlanning.Tables["Tbl_CustomerOrders"].Select(Conversions.ToString(Operators.ConcatenateObject("OrderIndex=", dgList.SelectedRows[0].Cells["OrderIndex"].Value)));
                try
                {
                    if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                        Module1.cnProductionPlanning.Open();
                    trnConfirm = Module1.cnProductionPlanning.BeginTransaction();
                    cmConfirm.Transaction = trnConfirm;
                    cmConfirm.ExecuteNonQuery();
                    drConfirm[0].BeginEdit();
                    drConfirm[0]["ConfirmFlag"] = 1;
                    drConfirm[0]["ConfirmFlagTitle"] = "تایید شده";
                    drConfirm[0].EndEdit();
                    trnConfirm.Commit();
                    dsProductionPlanning.Tables["Tbl_CustomerOrders"].AcceptChanges();
                }
                catch (Exception objEx)
                {
                    dsProductionPlanning.RejectChanges();
                    trnConfirm.Rollback();
                    Logger.SaveError(Name + ".cmdConfirm_Click", objEx.Message);
                    MessageBox.Show("تایید سفارش با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                }
                finally
                {
                    trnConfirm.Dispose();
                    if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                        Module1.cnProductionPlanning.Close();
                }
            }
            else if (cmdConfirm.Tag.ToString().Equals("2"))
            {
                var cmNotConfirm = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject("Update Tbl_CustomerOrders Set ConfirmFlag = 0,AllowFlag = 0,ProductionCallDate = 0 Where OrderIndex=", dgList.SelectedRows[0].Cells["OrderIndex"].Value)), Module1.cnProductionPlanning);
                SqlTransaction trnNotConfirm = null;
                var drNotConfirm = dsProductionPlanning.Tables["Tbl_CustomerOrders"].Select(Conversions.ToString(Operators.ConcatenateObject("OrderIndex=", dgList.SelectedRows[0].Cells["OrderIndex"].Value)));
                try
                {
                    if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                        Module1.cnProductionPlanning.Open();
                    trnNotConfirm = Module1.cnProductionPlanning.BeginTransaction();
                    cmNotConfirm.Transaction = trnNotConfirm;
                    cmNotConfirm.ExecuteNonQuery();
                    drNotConfirm[0].BeginEdit();
                    drNotConfirm[0]["ConfirmFlag"] = 0;
                    drNotConfirm[0]["ConfirmFlagTitle"] = "تایید نشده";
                    drNotConfirm[0]["AllowFlag"] = 0;
                    drNotConfirm[0]["AllowFlagTitle"] = "تصویب نشده";
                    drNotConfirm[0].EndEdit();
                    trnNotConfirm.Commit();
                    dsProductionPlanning.Tables["Tbl_CustomerOrders"].AcceptChanges();
                }
                catch (Exception objEx)
                {
                    dsProductionPlanning.RejectChanges();
                    trnNotConfirm.Rollback();
                    Logger.SaveError(Name + ".cmdConfirm_Click", objEx.Message);
                    MessageBox.Show("لغو تایید سفارش با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                }
                finally
                {
                    trnNotConfirm.Dispose();
                    if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                        Module1.cnProductionPlanning.Close();
                }
            }

            SetConfirmAllowButtons();
        }

        private void cmdAllow_Click(object sender, EventArgs e)
        {
            if (cmdAllow.Tag.ToString().Equals("1"))
            {
                var cmAllow = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject("Update Tbl_CustomerOrders Set AllowFlag = 1, ProductionCallDate='" + Module1.mServerShamsiDate + "' Where OrderIndex=", dgList.SelectedRows[0].Cells["OrderIndex"].Value)), Module1.cnProductionPlanning);
                SqlTransaction trnAllow = null;
                var drAllow = dsProductionPlanning.Tables["Tbl_CustomerOrders"].Select(Conversions.ToString(Operators.ConcatenateObject("OrderIndex=", dgList.SelectedRows[0].Cells["OrderIndex"].Value)));
                try
                {
                    if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                        Module1.cnProductionPlanning.Open();
                    trnAllow = Module1.cnProductionPlanning.BeginTransaction();
                    cmAllow.Transaction = trnAllow;
                    cmAllow.ExecuteNonQuery();
                    drAllow[0].BeginEdit();
                    drAllow[0]["AllowFlag"] = 1;
                    drAllow[0]["AllowFlagTitle"] = "تصویب شده";
                    drAllow[0]["ProductionCallDate"] = Module1.mServerShamsiDate;
                    drAllow[0].EndEdit();
                    trnAllow.Commit();
                    dsProductionPlanning.Tables["Tbl_CustomerOrders"].AcceptChanges();
                }
                catch (Exception objEx)
                {
                    dsProductionPlanning.RejectChanges();
                    trnAllow.Rollback();
                    Logger.SaveError(Name + ".cmdAllow_Click", objEx.Message);
                    MessageBox.Show("تصویب سفارش با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                }
                finally
                {
                    trnAllow.Dispose();
                    if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                        Module1.cnProductionPlanning.Close();
                }
            }
            else if (cmdAllow.Tag.ToString().Equals("2"))
            {
                var cmNotAllow = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject("Update Tbl_CustomerOrders Set AllowFlag = 0, ProductionCallDate='0' Where OrderIndex=", dgList.SelectedRows[0].Cells["OrderIndex"].Value)), Module1.cnProductionPlanning);
                SqlTransaction trnNotAllow = null;
                var drNotAllow = dsProductionPlanning.Tables["Tbl_CustomerOrders"].Select(Conversions.ToString(Operators.ConcatenateObject("OrderIndex=", dgList.SelectedRows[0].Cells["OrderIndex"].Value)));
                try
                {
                    if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                        Module1.cnProductionPlanning.Open();
                    trnNotAllow = Module1.cnProductionPlanning.BeginTransaction();
                    cmNotAllow.Transaction = trnNotAllow;
                    cmNotAllow.ExecuteNonQuery();
                    drNotAllow[0].BeginEdit();
                    drNotAllow[0]["AllowFlag"] = 0;
                    drNotAllow[0]["AllowFlagTitle"] = "تصویب نشده";
                    drNotAllow[0]["ProductionCallDate"] = 0;
                    drNotAllow[0].EndEdit();
                    trnNotAllow.Commit();
                    dsProductionPlanning.Tables["Tbl_CustomerOrders"].AcceptChanges();
                }
                catch (Exception objEx)
                {
                    dsProductionPlanning.RejectChanges();
                    trnNotAllow.Rollback();
                    Logger.SaveError(Name + ".cmdAllow_Click", objEx.Message);
                    MessageBox.Show("لغو تصویب سفارش با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                }
                finally
                {
                    trnNotAllow.Dispose();
                    if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                        Module1.cnProductionPlanning.Close();
                }
            }

            SetConfirmAllowButtons();
        }

        private void SetGridColumns(object DataSource, string Table = Constants.vbNullString)
        {
            {
                var withBlock = dgList;
                if (DataSource is DataView)
                {
                    withBlock.DataSource = DataSource;
                }
                else
                {
                    withBlock.DataSource = DataSource;
                    withBlock.DataMember = Table;
                }

                withBlock.Columns[0].Visible = false;
                withBlock.Columns[1].HeaderText = "تاریخ ثبت";
                withBlock.Columns[2].HeaderText = "شمارۀ سفارش";
                withBlock.Columns[3].HeaderText = "تاریخ سفارش";
                withBlock.Columns[4].Visible = false;
                withBlock.Columns[5].HeaderText = "نام مشتری";
                withBlock.Columns[6].Visible = true;
                withBlock.Columns[6].HeaderText = "کد محصول";
                withBlock.Columns[7].HeaderText = "نام محصول";
                withBlock.Columns[8].Visible = false;
                withBlock.Columns[9].HeaderText = "تعداد";
                withBlock.Columns[10].HeaderText = "تاریخ تحویل";
                withBlock.Columns[11].HeaderText = "شمارۀ قرارداد";
                withBlock.Columns[12].HeaderText = "وضعیت تایید";
                withBlock.Columns[13].Visible = false;
                withBlock.Columns[14].HeaderText = "وضعیت تصویب";
                withBlock.Columns[15].Visible = false;
                withBlock.Columns[16].Visible = false;
                withBlock.Columns[17].HeaderText = "اتمام تولید";
            }

            Module1.SetGridColumnsWidth(Name, 0, dgList);
        }

        private void SetColumnsWidth()
        {
            {
                var withBlock = dgList;
                withBlock.Columns[1].Width = 70;
                withBlock.Columns[2].Width = 90;
                withBlock.Columns[3].Width = 70;
                withBlock.Columns[5].Width = 170;
                withBlock.Columns[7].Width = 200;
                withBlock.Columns[9].Width = 60;
                withBlock.Columns[10].Width = 70;
                withBlock.Columns[11].Width = 80;
                withBlock.Columns[12].Width = 65;
                withBlock.Columns[14].Width = 65;
                withBlock.Columns[17].Width = 65;
            }
        }
        // این تابع رکورد جاری از جدول جاری استفاده شده در برنامه را باز می گرداند
        public DataRow GetRow()
        {
            string FindValue = Constants.vbNullString;
            DataRow[] drFind = new DataRow[1];
            DataRow crRow;
            FindValue = Conversions.ToString(Operators.ConcatenateObject("OrderIndex=", dgList.CurrentRow.Cells["OrderIndex"].Value));
            drFind = dsProductionPlanning.Tables["Tbl_CustomerOrders"].Select(FindValue);
            crRow = drFind[0];
            return crRow;
        }

        private string GetFilterValue()
        {
            string FilterValue = Constants.vbNullString;
            if (txtRegisterFrom.Text is object && !txtRegisterFrom.Text.Equals(""))
            {
                FilterValue = "RegisterDate>=" + Strings.Replace(txtRegisterFrom.Text, "/", "");
            }

            if (txtRegisterTo.Text is object && !txtRegisterTo.Text.Equals(""))
            {
                FilterValue = Conversions.ToString(FilterValue + Interaction.IIf(string.IsNullOrEmpty(FilterValue), "RegisterDate<=" + Strings.Replace(txtRegisterTo.Text, "/", ""), " And RegisterDate<=" + Strings.Replace(txtRegisterTo.Text, "/", "")));
            }

            if (txtOrderFrom.Text is object && !txtOrderFrom.Text.Equals(""))
            {
                FilterValue = Conversions.ToString(FilterValue + Interaction.IIf(string.IsNullOrEmpty(FilterValue), "OrderDate>=" + Strings.Replace(txtOrderFrom.Text, "/", ""), " And OrderDate>=" + Strings.Replace(txtOrderFrom.Text, "/", "")));
            }

            if (txtOrderTo.Text is object && !txtOrderTo.Text.Equals(""))
            {
                FilterValue = Conversions.ToString(FilterValue + Interaction.IIf(string.IsNullOrEmpty(FilterValue), "OrderDate<=" + Strings.Replace(txtOrderTo.Text, "/", ""), " And OrderDate<=" + Strings.Replace(txtOrderTo.Text, "/", "")));
            }

            if (txtDeliveryFrom.Text is object && !txtDeliveryFrom.Text.Equals(""))
            {
                FilterValue = Conversions.ToString(FilterValue + Interaction.IIf(string.IsNullOrEmpty(FilterValue), "DeliveryDueDate>=" + Strings.Replace(txtDeliveryFrom.Text, "/", ""), " And DeliveryDueDate>=" + Strings.Replace(txtDeliveryFrom.Text, "/", "")));
            }

            if (txtDeliveryTo.Text is object && !txtDeliveryTo.Text.Equals(""))
            {
                FilterValue = Conversions.ToString(FilterValue + Interaction.IIf(string.IsNullOrEmpty(FilterValue), "DeliveryDueDate<=" + Strings.Replace(txtDeliveryTo.Text, "/", ""), " And DeliveryDueDate<=" + Strings.Replace(txtDeliveryTo.Text, "/", "")));
            }

            if (txtOrderNo.Text is object && !txtOrderNo.Text.Equals(""))
            {
                FilterValue = Conversions.ToString(FilterValue + Interaction.IIf(string.IsNullOrEmpty(FilterValue), "OrderNo = '" + txtOrderNo.Text + "'", " And OrderNo = '" + txtOrderNo.Text + "'"));
            }

            if (!string.IsNullOrEmpty(Product_Lookup.CB_SelectedValue) )
            {
                FilterValue = FilterValue + (string.IsNullOrEmpty(FilterValue) ? "": " AND ") + $" ProductCode='{Product_Lookup.CB_SelectedValue}'";
            }

            if (!string.IsNullOrEmpty(Customer_LookUp.CB_SelectedValue))
            {
                FilterValue = FilterValue + (string.IsNullOrEmpty(FilterValue) ? "": " AND ") + $" CustomerCode='{Customer_LookUp.CB_SelectedValue}'";
            }

            if (chkConfirmed.Checked)
            {
                FilterValue = Conversions.ToString(FilterValue + Interaction.IIf(string.IsNullOrEmpty(FilterValue), "ConfirmFlag = 1", " And ConfirmFlag = 1"));
            }
            else if (chkNotConfirmed.Checked)
            {
                FilterValue = Conversions.ToString(FilterValue + Interaction.IIf(string.IsNullOrEmpty(FilterValue), "ConfirmFlag = 0", " And ConfirmFlag = 0"));
            }

            if (chkAllowed.Checked)
            {
                FilterValue = Conversions.ToString(FilterValue + Interaction.IIf(string.IsNullOrEmpty(FilterValue), "AllowFlag = 1", " And AllowFlag = 1"));
            }
            else if (chkNotAllowed.Checked)
            {
                FilterValue = Conversions.ToString(FilterValue + Interaction.IIf(string.IsNullOrEmpty(FilterValue), "AllowFlag = 0", " And AllowFlag = 0"));
            }

            if (chkProductionDone.Checked)
            {
                FilterValue = Conversions.ToString(FilterValue + Interaction.IIf(string.IsNullOrEmpty(FilterValue), "IsDone = 1", " And IsDone = 1"));
            }
            else if (chkUnProduction.Checked)
            {
                FilterValue = Conversions.ToString(FilterValue + Interaction.IIf(string.IsNullOrEmpty(FilterValue), "IsDone = 0", " And IsDone = 0"));
            }

            if (txtContractNo.Text is object && !txtContractNo.Text.Equals(""))
            {
                FilterValue = Conversions.ToString(FilterValue + Interaction.IIf(string.IsNullOrEmpty(FilterValue), "ContractNo = '" + txtContractNo.Text + "'", " And ContractNo = '" + txtContractNo.Text + "'"));
            }

            return FilterValue;
        }

        private void SetConfirmAllowButtons()
        {
            if (Conversions.ToBoolean(dgList.SelectedRows[0].Cells["ConfirmFlag"].Value))
            {
                cmdConfirm.Tag = 2;
                cmdConfirm.Text = "لغو تایید سفارش";
                if (Conversions.ToBoolean(dgList.SelectedRows[0].Cells["AllowFlag"].Value))
                {
                    cmdConfirm.Enabled = false;
                    cmdAllow.Enabled = true;
                    cmdAllow.Tag = 2;
                    cmdAllow.Text = "لغو تصویب سفارش";
                }
                else
                {
                    cmdConfirm.Enabled = true;
                    cmdAllow.Enabled = true;
                    cmdAllow.Tag = 1;
                    cmdAllow.Text = "تصویب سفارش";
                }
            }
            else
            {
                cmdAllow.Enabled = false;
                cmdConfirm.Enabled = true;
                cmdAllow.Tag = 1;
                cmdAllow.Text = "تصویب سفارش";
                cmdConfirm.Tag = 1;
                cmdConfirm.Text = "تایید سفارش";
            }
        }

        private int GetOrderProgressPercent(string BatchCode, string OrderIndex, int OrderQuantity)
        {
            int ReturnedPercent = 0;
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    var cmProgress = new SqlCommand("Select * From View_OrdersProductionProgress Where BatchCode = '" + BatchCode + "' Order By OrderDate", cn);
                    var drProgress = cmProgress.ExecuteReader();
                    int BatchProductionQuantity = 0;
                    if (drProgress.Read())
                    {
                        BatchProductionQuantity = Conversions.ToInteger(drProgress["BatchProductionQuantity"]);
                        if (BatchProductionQuantity == 0)
                        {
                            drProgress.Close();
                            //break;
                        }
                        else if (BatchProductionQuantity >= OrderQuantity)
                        {
                            if (drProgress["OrderIndex"].ToString().Equals(OrderIndex))
                            {
                                ReturnedPercent = 100;
                            }
                            else
                            {
                                BatchProductionQuantity = Conversions.ToInteger(BatchProductionQuantity - (int) drProgress["OrderQuantity"]);
                                while (drProgress.Read())
                                {
                                    if (BatchProductionQuantity <= 0)
                                    {
                                        ReturnedPercent = 0;
                                        break;
                                    }
                                    else if (BatchProductionQuantity >= OrderQuantity)
                                    {
                                        if (drProgress["OrderIndex"].ToString().Equals(OrderIndex))
                                        {
                                            ReturnedPercent = 100;
                                            break;
                                        }
                                        else
                                        {
                                            BatchProductionQuantity = Conversions.ToInteger(BatchProductionQuantity - (int)drProgress["OrderQuantity"]);
                                        }
                                    }
                                    else if (BatchProductionQuantity < OrderQuantity)
                                    {
                                        if (drProgress["OrderIndex"].ToString().Equals(OrderIndex))
                                        {
                                            ReturnedPercent = (int)Math.Round(BatchProductionQuantity / (double)OrderQuantity * 100d);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                        else if (drProgress["OrderIndex"].ToString().Equals(OrderIndex))
                        {
                            ReturnedPercent = (int)Math.Round(BatchProductionQuantity / (double)OrderQuantity * 100d);
                        }

                        drProgress.Close();
                    }
                }
                catch (Exception objEx)
                {
                    Logger.SaveError(Name + ".GetOrderProgressPercent", objEx.Message);
                    MessageBox.Show("محاسبۀ پیشرفت تولید سفارش با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    ReturnedPercent = 0;
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }

            return ReturnedPercent;
        }

        private void tmrCalcOrderProgress_Tick(object sender, EventArgs e)
        {
            tmrCalcOrderProgress.Enabled = false;
            if (!(lblBatchCode.Text is null || lblBatchCode.Text.Equals("")) && !(lblOrderQuantity.Text is null || lblOrderQuantity.Text.Equals("")))
            {
                lblOrderProductionProgress.Text = GetOrderProgressPercent(lblBatchCode.Text, Conversions.ToString(dgList.SelectedRows[0].Cells["OrderIndex"].Value), Conversions.ToInteger(lblOrderQuantity.Text)).ToString();
            }
        }

        private void _dgList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}