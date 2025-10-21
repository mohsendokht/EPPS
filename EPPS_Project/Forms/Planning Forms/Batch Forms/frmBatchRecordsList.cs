using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using MyUserControl;
using static ProductionPlanning.MyEnums;

namespace ProductionPlanning
{
    public partial class frmBatchRecordsList
    {
        public frmBatchRecordsList()
        {
            InitializeComponent();
            _cmdExit.Name = "cmdExit";
            _cmdDelete.Name = "cmdDelete";
            _cmdUpdate.Name = "cmdUpdate";
            _cmdInsert.Name = "cmdInsert";
            _cmdShowFiltered.Name = "cmdShowFiltered";
            _txtYear.Name = "txtYear";
            _dgBatchList.Name = "dgBatchList";
            _mnuItemDoneBatch.Name = "mnuItemDoneBatch";
            _mnuItemCancelDoneBatch.Name = "mnuItemCancelDoneBatch";
            _dgOrderList.Name = "dgOrderList";
        }

        private DataSetConfiguration DataSetConfig = new DataSetConfiguration();
        private DataSetConfiguration DataSetCombos = new DataSetConfiguration();
        private DataView dvBatchList = new DataView();
        private SqlDataAdapter daProgress = new SqlDataAdapter();
        private int mFormMode;
        private int mSearchMode = -1;
       
        public ListFormCaller CallerForm { get; set; }
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

        private void frmRecordsLists_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdInsert, 0);
            Module1.SetButtonsImage(cmdDelete, 1);
            Module1.SetButtonsImage(cmdUpdate, 2);
            Module1.SetButtonsImage(cmdFilter, 3);
            Module1.SetButtonsImage(cmdFind, 10);
            dgBatchList.Sorted += Module1.DataGridViews_Sorted_EventHandler;
            dgBatchList.Tag = -1;
            tmrLoadOrders.Enabled = true;
            string SelectStr =
                "Select A.BatchCode,A.DefineYear,A.SequenceNo,B.ProductCode," +
                "       A.SaleContractNo,A.DefineDate,A.Productionquantity," +
                "       A.FirstDelivaryDate,A.ProductTreeCode,A.PlaningStartDate," +
                "       A.RealStartDate, A.ProductionProgressMeasure, A.SubbatchQuantity," +
                "       A.FinishedDate,A.RealProductionQuantity,C.ProductName, " +
                "       dbo.GetBatchProductionQuantity(A.BatchCode) As ProductProductionQuantity, " +
                "       dbo.GetBatchStatus(A.BatchCode) As BatchStatus " +
                "From   dbo.Tbl_ProductionBatchs A INNER JOIN " +
                "       dbo.Tbl_ProductTree B ON A.ProductTreeCode=B.TreeCode INNER JOIN " +
                "       dbo.Tbl_Products C ON B.ProductCode = C.ProductCode";


            DataSetConfig.FillDataSet("Tbl_ProductionBatchs", "Tbl_ProductionBatchs", SelectStr, "BatchCode");
            dsProductionPlanning.Tables["Tbl_ProductionBatchs"].Columns["BatchStatus"].ReadOnly = false;
            dvBatchList = dsProductionPlanning.Tables["Tbl_ProductionBatchs"].DefaultView;
            DataSetConfig.FillDataSet("Tbl_Products", "Tbl_Products_Lookup", "SELECT ProductCode, ProductName From Tbl_Products", "ProductCode");
          
            SetGridColumns(dvBatchList);
            CreateBatchUpdateCommand();

            // '''  Set User Access Right  '''''''''''''
            cmdInsert.Enabled = Module_UserAccess.HaveAccessToItem(51);
            cmdUpdate.Enabled = Module_UserAccess.HaveAccessToItem(52);
            cmdDelete.Enabled = Module_UserAccess.HaveAccessToItem(53);
            // cmdProductionProgressCalc.Enabled = HaveAccessToItem(54)

            LoadProductLookup();
        }

        private void LoadProductLookup()
        {
            DataSetCombos.FillDataSet("Tbl_Products", "Tbl_Products", "SELECT ProductCode, ProductName From Tbl_Products", "ProductCode");

            Product_Lookup.CB_DataSource = DataSetCombos.dsProductionPlanning.Tables["Tbl_Products"].DefaultView;
            Product_Lookup.CB_DisplayMember = "ProductCode";
            Product_Lookup.CB_ValueMember = "ProductName";
            Product_Lookup.CB_LinkedColumnIndex = 1;
            Product_Lookup.CB_AutoComplete = true;
            Product_Lookup.CB_SelectedIndex = -1;
            Product_Lookup.CB_SelectedValue = "";
            Product_Lookup.CB_SerachFromTitle = "محصولات";


        }

        private void frmRecordsLists_FormClosing(object sender, FormClosingEventArgs e)
        {
            Module1.SaveGridColumnsWidth(Name, 0, dgBatchList);
            Module1.SaveGridColumnsWidth(Name, 0, dgOrderList);
        
            dvBatchList.Dispose();
            DataSetConfig = null;
            DataSetCombos = null;
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

            if (dgBatchList.Rows.Count == 0 && mFormMode == (int)Module1.FormModeEnum.EDIT_MODE | mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
            {
                MessageBox.Show("رکوردی وجود ندارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                return;
            }

            switch (mFormMode)
            {
                case (int)Module1.FormModeEnum.EDIT_MODE:
                case (int)Module1.FormModeEnum.DELETE_MODE:
                    {
                        if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(dgBatchList.CurrentRow.Cells["FinishedDate"].Value, 0, false)))
                        {
                            MessageBox.Show("بچ انتخاب شده به علت بسته شدن، مجاز به اصلاح یا حذف نمی باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                            return;
                        }

                        break;
                    }
            }

            short EditMode = -1;
            short NotEntered_InProduction_SubbatchQuantity = -1;
            if (mFormMode == (int)Module1.FormModeEnum.EDIT_MODE)
            {
                var cmEditRight = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Select Count(*) From Tbl_Planning Where SubbatchCode IN (Select SubbatchCode From Tbl_ProductionSubbatchs Where BatchCode='", dgBatchList.CurrentRow.Cells["BatchCode"].Value), "')")), Module1.cnProductionPlanning);
                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();
                if (Conversions.ToInteger(cmEditRight.ExecuteScalar()) == 0)
                {
                    if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                        Module1.cnProductionPlanning.Close();
                    cmEditRight.Dispose();
                    EditMode = 1; // هیچ ساب بچی برنامه ریزی نشده است
                    goto FillDataSet;
                }

                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                    Module1.cnProductionPlanning.Close();

                // کنترل اینکه آیا تمامی ساب بچها وارد مرحله تولید شده اند یا نه
                cmEditRight.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Select Count(*) From Tbl_ProductionSubbatchs Where BatchCode='", dgBatchList.CurrentRow.Cells["BatchCode"].Value), "' And (NOT (SubbatchCode IN(SELECT Distinct SubbatchCode FROM Tbl_RealProduction)))"));
                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();
                NotEntered_InProduction_SubbatchQuantity = (short)Conversions.ToInteger(cmEditRight.ExecuteScalar());
                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                    Module1.cnProductionPlanning.Close();
                cmEditRight.Dispose();
                EditMode = 2; // برخی از ساب بچ ها وارد مرحله تولید شده اند
                goto FillDataSet;
            }
            else if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
            {
                var cmDeleteRight = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Select Count(*) From Tbl_ProductionSubbatchs Where BatchCode='", dgBatchList.CurrentRow.Cells["BatchCode"].Value), "' And (SubbatchCode IN(SELECT SubbatchCode FROM Tbl_RealProduction))")), Module1.cnProductionPlanning);
                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();
                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(cmDeleteRight.ExecuteScalar(), 0, false)))
                {
                    if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                        Module1.cnProductionPlanning.Close();
                    cmDeleteRight.Dispose();
                    MessageBox.Show("بچ انتخاب شده مجاز به حذف نمی باشد" + Constants.vbCrLf + "بعضی از ساب بچ ها وارد مرحله تولید شده اند", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return;
                }

                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                    Module1.cnProductionPlanning.Close();
                cmDeleteRight.Dispose();
            }

        FillDataSet:
            ;
            DataSetConfig.FillDataSet("Tbl_Products", "Tbl_Products", "Select * From Tbl_Products Order By ProductName", "ProductCode");
            DataSetConfig.FillDataSet("Tbl_ProductTree", "Tbl_ProductTree", "Select * From Tbl_ProductTree Order By TreeTitle", "TreeCode");
            {
                var withBlock = new frmProductionBatch();
                if (mFormMode == (int)Module1.FormModeEnum.INSERT_MODE)
                {
                    withBlock.SelectedOrderIndex = GetSelectedOrderIndex();
                    withBlock.SelectedOrdersProductCode = GetSelectedOrdersProductCode();
                }

                if (mFormMode == (int)Module1.FormModeEnum.EDIT_MODE)
                {
                    withBlock.EditMode = EditMode;
                    withBlock.NotEntered_InProduction_SubbatchQuantity = NotEntered_InProduction_SubbatchQuantity;
                }

                withBlock.ListForm = this;
                withBlock.ListForm.FormMode = mFormMode;
                if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
                {
                    withBlock.Controls["Panel1"].Controls["cmdSave"].Visible = false;
                    withBlock.Controls["Panel1"].Controls["cmdDelete"].Visible = true;
                }

                if (withBlock.ShowDialog() == DialogResult.OK)
                {
                    tmrLoadOrders.Enabled = true;
                    switch (FormMode)
                    {
                        case (int)Module1.FormModeEnum.INSERT_MODE:
                            {
                                BindingContext[dsProductionPlanning, "Tbl_ProductionBatchs"].Position = BindingContext[dsProductionPlanning, "Tbl_ProductionBatchs"].Count;
                                break;
                            }

                        case (int)Module1.FormModeEnum.DELETE_MODE:
                            {
                                if (BindingContext[dsProductionPlanning, "Tbl_ProductionBatchs"].Count > 0)
                                {
                                    BindingContext[dsProductionPlanning, "Tbl_ProductionBatchs"].Position = 0;
                                }

                                break;
                            }
                    }
                }

                withBlock.Dispose();
            }
        }

        private void frmRecordsLists_Resize(object sender, EventArgs e)
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
            int RecordCount = Conversions.ToInteger(Interaction.IIf(dgBatchList.DataSource is DataSet, BindingContext[dsProductionPlanning, "Tbl_ProductionBatchs"].Count, dgBatchList.Rows.Count));
            int CurrentRecord = e.RowIndex + 1;
            tsslRecNo.Text = "رکورد  " + CurrentRecord.ToString() + "  از  " + RecordCount.ToString();
        }

        private void dgList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgBatchList.Columns[e.ColumnIndex].Name.Equals("DefineDate") || dgBatchList.Columns[e.ColumnIndex].Name.Equals("FirstDelivaryDate") || dgBatchList.Columns[e.ColumnIndex].Name.Equals("PlaningStartDate") || dgBatchList.Columns[e.ColumnIndex].Name.Equals("RealStartDate") || dgBatchList.Columns[e.ColumnIndex].Name.Equals("FinishedDate"))
            {
                e.Value = Strings.Mid(Conversions.ToString(e.Value), 1, 4) + "/" + Strings.Mid(Conversions.ToString(e.Value), 5, 2) + "/" + Strings.Mid(Conversions.ToString(e.Value), 7, 2);
            }

            if (e.RowIndex > -1)
            {
                dgBatchList.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
            }
        }

        private void dgList_MouseClick(object sender, MouseEventArgs e)
        {
            var objCell = dgBatchList.HitTest(e.X, e.Y);
            if (objCell.RowIndex > -1 && objCell.ColumnIndex > -1)
            {
                dgBatchList.Rows[objCell.RowIndex].Cells[objCell.ColumnIndex].Selected = true;
            }
        }

        private void mnuItemDoneBatch_Click(object sender, EventArgs e)
        {
            string Query = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Select C.ProductTreeCode AS TreeCode,A.BatchCode,A.DetailCode,B.LevelNo," + "       B.DetailName,A.Stock,A.RequirementQuantity,A.OverStock,B.ParentDetailCode,A.ProductionQuantity,A.ProductionStock,A.CalcedOverStock,A.GarbageQuantity " + "From   dbo.Tbl_ProductionBatchsDetail A INNER JOIN " + "       dbo.Tbl_ProductTreeDetails B ON A.DetailCode = B.DetailCode INNER JOIN " + "       dbo.Tbl_ProductionBatchs C ON A.BatchCode = C.BatchCode AND B.TreeCode=C.ProductTreeCode Where A.BatchCode = '", dgBatchList.CurrentRow.Cells["BatchCode"].Value), "'"));



            DataSetConfig.FillDataSet("Tbl_ProductionBatchsDetail", "Tbl_ProductionBatchsDetail", Query, "BatchCode", "DetailCode");
            DataSetConfig.FillDataSet("Tbl_ProductTreeDetails", "Tbl_ProductTreeDetails", Conversions.ToString(Operators.ConcatenateObject("Select * From Tbl_ProductTreeDetails Where TreeCode = ", dgBatchList.CurrentRow.Cells["ProductTreeCode"].Value)), "TreeCode", "DetailCode");
            {
                var withBlock = new frmDoneBatch();
                withBlock.dsDoneBatch = dsProductionPlanning;
                withBlock.CurrentRow = GetRow();
                if (ReferenceEquals(sender, mnuItemDoneBatch))
                {
                    withBlock.FormMode = frmDoneBatch.DoneFormModeEnum.DFM_DoneBatch;
                }
                else if (ReferenceEquals(sender, mnuItemCancelDoneBatch))
                {
                    withBlock.FormMode = frmDoneBatch.DoneFormModeEnum.DFM_CancelDoneBatch;
                }

                if (withBlock.ShowDialog() == DialogResult.OK)
                {
                    var drStatus = dsProductionPlanning.Tables["Tbl_ProductionBatchs"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("BatchCode = '", dgBatchList.CurrentRow.Cells["BatchCode"].Value), "'")));
                    if (drStatus.Length > 0)
                    {
                        if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                            Module1.cnProductionPlanning.Open();
                        var cmStatus = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Select dbo.GetBatchStatus(BatchCode) As BatchStatus From dbo.Tbl_ProductionBatchs Where BatchCode = '", drStatus[0]["BatchCode"]), "'")), Module1.cnProductionPlanning);
                        var sdrStatus = cmStatus.ExecuteReader();
                        if (sdrStatus.Read())
                        {
                            drStatus[0].BeginEdit();
                            drStatus[0]["BatchStatus"] = sdrStatus["BatchStatus"];
                            drStatus[0].EndEdit();
                            dsProductionPlanning.Tables["Tbl_ProductionBatchs"].AcceptChanges();
                        }

                        sdrStatus.Close();
                        if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                            Module1.cnProductionPlanning.Close();
                    }
                }

                withBlock.Dispose();
            }
        }

        private void dgOrderList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.RowIndex != -1 && e.ColumnIndex == 1)
            {
                if (Conversions.ToBoolean(dgOrderList.Rows[e.RowIndex].Cells["colorderSelect"].Value))
                {
                    dgOrderList.Rows[e.RowIndex].Cells["colorderSelect"].Value = false;
                }
                else
                {
                    dgOrderList.Rows[e.RowIndex].Cells["colorderSelect"].Value = true;
                }
            }
        }

        private void dgOrderList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && dgOrderList.CurrentCell.ColumnIndex == 1)
            {
                if (Conversions.ToBoolean(dgOrderList.CurrentRow.Cells["colorderSelect"].Value))
                {
                    dgOrderList.CurrentRow.Cells["colorderSelect"].Value = false;
                }
                else
                {
                    dgOrderList.CurrentRow.Cells["colorderSelect"].Value = true;
                }
            }
        }

        private void tmrLoadOrders_Tick(object sender, EventArgs e)
        {
            tmrLoadOrders.Enabled = false;
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    dgOrderList.Rows.Clear();
                    Application.DoEvents();
                    var cmOrders = new SqlCommand("Select A.OrderIndex,A.OrderNo,A.OrderQuantity,A.ProductCode,B.CustomerName,A.DeliveryDueDate From Tbl_CustomerOrders A Inner Join Tbl_Customers B ON A.CustomerCode = B.CustomerCode " + "Where A.AllowFlag = 1 And Not A.OrderIndex IN (Select OrderIndex From Tbl_Batch_Order) Order By OrderNo", cn);
                    var drOrders = cmOrders.ExecuteReader();
                    while (drOrders.Read())
                    {
                        string DeliveryDate = "0";
                        if (!DBNull.Value.Equals(drOrders["DeliveryDueDate"]) && !Conversions.ToString(drOrders["DeliveryDueDate"]).Equals("0") && Conversions.ToString(drOrders["DeliveryDueDate"]).Length == 8)
                        {
                            DeliveryDate = Conversions.ToString(drOrders["DeliveryDueDate"]).Substring(0, 4) + "/" + Conversions.ToString(drOrders["DeliveryDueDate"]).Substring(4, 2) + "/" + Conversions.ToString(drOrders["DeliveryDueDate"]).Substring(6, 2);
                        }

                        dgOrderList.Rows.Add(drOrders["OrderIndex"], 0, drOrders["OrderNo"], drOrders["OrderQuantity"], drOrders["ProductCode"], drOrders["CustomerName"], DeliveryDate);
                    }

                    drOrders.Close();
                }
                catch (Exception objEx)
                {
                    tmrLoadOrders.Enabled = false;
                    Logger.SaveError(Name + ".tmrLoadOrders_Tick", objEx.Message);
                    MessageBox.Show("فراخوانی لیست سفارشات با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }
        }

        private void cmdShowFiltered_Click(object sender, EventArgs e)
        {
            var mSortedCol = dgBatchList.SortedColumn;
            var mSortDirection = dgBatchList.SortOrder;
            if (chkAllRows.Checked)
            {
                dvBatchList.RowFilter = "";
            }
            else
            {
                dvBatchList.RowFilter = GetFilterValue();
            }

            if (chkCalcProductionProgress.Checked)
            {
               
                for (int I = 0, loopTo = dvBatchList.Count - 1; I <= loopTo; I++)
                    CalcProductionProgress(dvBatchList[I]["BatchCode"].ToString());
                           
            }

            if (mSortedCol is object)
            {
                dgBatchList.Sort(mSortedCol, (ListSortDirection)Interaction.IIf(mSortDirection == System.Windows.Forms.SortOrder.Ascending, ListSortDirection.Ascending, ListSortDirection.Descending));
            }
        }

        private void CreateBatchUpdateCommand()
        {
            daProgress.UpdateCommand = new SqlCommand("Update Tbl_ProductionBatchs Set PlaningStartDate=@PlaningStartDate,RealStartDate=@RealStartDate,ProductionProgressMeasure=@ProductionProgressMeasure Where BatchCode=@BatchCode", Module1.cnProductionPlanning);
            {
                var withBlock = daProgress.UpdateCommand;
                withBlock.Parameters.Add("@PlaningStartDate", SqlDbType.VarChar, 8, "PlaningStartDate");
                withBlock.Parameters.Add("@RealStartDate", SqlDbType.VarChar, 8, "RealStartDate");
                withBlock.Parameters.Add("@ProductionProgressMeasure", SqlDbType.VarChar, 5, "ProductionProgressMeasure");
                withBlock.Parameters.Add("@BatchCode", SqlDbType.VarChar, 20, "BatchCode").SourceVersion = DataRowVersion.Original;
            }
        }

        private void SetGridColumns(object DataSource, string Table = Constants.vbNullString)
        {
            {
                var withBlock = dgBatchList;
                if (DataSource is DataView)
                {
                    withBlock.DataSource = DataSource;
                }
                else
                {
                    withBlock.DataSource = DataSource;
                    withBlock.DataMember = Table;
                }

                withBlock.Columns[0].HeaderText = "سریال بچ تولید";
                // .Columns(0).Width = 120
                withBlock.Columns[1].Visible = false;
                withBlock.Columns[2].Visible = false;
                withBlock.Columns[3].HeaderText = "کد محصول";
                // .Columns(3).Width = 130
                withBlock.Columns[4].Visible = false;
                withBlock.Columns[5].HeaderText = "تاریخ تعریف";
                // .Columns(5).Width = 80
                withBlock.Columns[6].HeaderText = "تعداد";
                // .Columns(6).Width = 60
                withBlock.Columns[7].HeaderText = "موعد تحویل اولین محموله";
                // .Columns(7).Width = 100
                withBlock.Columns[8].Visible = false;
                withBlock.Columns[9].HeaderText = "تاریخ شروع طبق برنامه ریزی";
                // .Columns(9).Width = 100
                withBlock.Columns[10].HeaderText = "تاریخ شروع تولید واقعی";
                // .Columns(10).Width = 100
                withBlock.Columns[11].HeaderText = "میزان پیشرفت تولید";
                // .Columns(11).Width = 80
                withBlock.Columns[12].Visible = false;
                withBlock.Columns[13].HeaderText = "تاریخ بستن";
                // .Columns(13).Width = 100
                withBlock.Columns[14].Visible = false;
                withBlock.Columns[15].Visible = false;
                withBlock.Columns[16].HeaderText = "تعداد محصول تولید شده";
                // .Columns(16).Width = 100
                withBlock.Columns[17].HeaderText = "وضعیت بچ";
                // .Columns(17).Width = 110
            }

            Module1.SetGridColumnsWidth(Name, 0, dgBatchList);
            Module1.SetGridColumnsWidth(Name, 0, dgOrderList);
        }
        // این تابع رکورد جاری از جدول جاری استفاده شده در برنامه را باز می گرداند
        public DataRow GetRow()
        {
            string FindValue = Constants.vbNullString;
            DataRow[] drFind = new DataRow[1];
            DataRow crRow;
            FindValue = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("BatchCode='", dgBatchList.CurrentRow.Cells["BatchCode"].Value), "'"));
            drFind = dsProductionPlanning.Tables["Tbl_ProductionBatchs"].Select(FindValue);
            crRow = drFind[0];
            return crRow;
        }

        private string GetFilterValue()
        {
            string FilterValue = Constants.vbNullString;
            if (rbDateScope.Checked)
            {
                if (!string.IsNullOrEmpty(Strings.Trim(Strings.Replace(txtFromDate.Text, "/", ""))))
                {
                    FilterValue = "DefineDate>=" + Strings.Trim(Strings.Replace(txtFromDate.Text, "/", ""));
                }

                if (!string.IsNullOrEmpty(Strings.Trim(Strings.Replace(txtToDate.Text, "/", ""))))
                {
                    FilterValue = Conversions.ToString(FilterValue + Interaction.IIf(string.IsNullOrEmpty(FilterValue), "DefineDate<=" + Strings.Trim(Strings.Replace(txtToDate.Text, "/", "")), " And DefineDate<=" + Strings.Trim(Strings.Replace(txtToDate.Text, "/", ""))));
                }
            }
            else if (rbYearScope.Checked)
            {
                if (!string.IsNullOrEmpty(txtYear.Text))
                {
                    FilterValue = "DefineYear=" + txtYear.Value;
                }
            }

         
            if ((Product_Lookup.CB_SelectedIndex != -1) && Product_Lookup.CB_SelectedValue.Trim() != "")
            {
                FilterValue = FilterValue + (string.IsNullOrEmpty(FilterValue) ? "" : " AND ") + $" ProductCode = '{Product_Lookup.CB_SelectedValue.Trim()}'";
            }

            if (rbNoPlanning.Checked)
            {
                FilterValue = Conversions.ToString(FilterValue + Interaction.IIf(string.IsNullOrEmpty(FilterValue), "(BatchStatus Like '%برنامه ريزي نشده%')", " And (BatchStatus Like '%برنامه ريزي نشده%')"));
            }
            else if (rbNoProduction.Checked)
            {
                FilterValue = Conversions.ToString(FilterValue + Interaction.IIf(string.IsNullOrEmpty(FilterValue), "(BatchStatus Like '%وارد توليد نشده%')", " And (BatchStatus Like '%وارد توليد نشده%')"));
            }
            else if (rbHasProduction.Checked)
            {
                FilterValue = Conversions.ToString(FilterValue + Interaction.IIf(string.IsNullOrEmpty(FilterValue), "(BatchStatus Like '%در حال توليد%')", " And (BatchStatus Like '%در حال توليد%')"));
            }
            else if (rbDoneBatchs.Checked)
            {
                FilterValue = Conversions.ToString(FilterValue + Interaction.IIf(string.IsNullOrEmpty(FilterValue), "(BatchStatus Like '%بسته شده%')", " And (BatchStatus Like '%بسته شده%')"));
            }

            return FilterValue;
        }

        private void dgList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true & e.KeyCode == Keys.F12)
            {
                Module1.ExportGridToExcel(dgBatchList);
            }
        }
                
        private string GetSelectedOrderIndex()
        {
            string SelectedOrders = Constants.vbNullString;
            for (int I = 0, loopTo = dgOrderList.Rows.Count - 1; I <= loopTo; I++)
            {
                if (Conversions.ToBoolean(dgOrderList.Rows[I].Cells["colorderSelect"].Value))
                {
                    string OrderIndex = Conversions.ToString(dgOrderList.Rows[I].Cells["colOrderIndex"].Value);
                    SelectedOrders = Conversions.ToString(SelectedOrders + Interaction.IIf(string.IsNullOrEmpty(SelectedOrders), OrderIndex, "," + OrderIndex));
                }
            }

            return SelectedOrders;
        }

        private string GetSelectedOrdersProductCode()
        {
            string ProductCode = "-1";
            for (int I = 0, loopTo = dgOrderList.Rows.Count - 1; I <= loopTo; I++)
            {
                if (Conversions.ToBoolean(dgOrderList.Rows[I].Cells["colorderSelect"].Value))
                {
                    if (ProductCode == "-1")
                    {
                        ProductCode = Conversions.ToString(dgOrderList.Rows[I].Cells["colProductCode"].Value);
                    }
                    else if (!dgOrderList.Rows[I].Cells["colProductCode"].Value.Equals(ProductCode))
                    {
                        ProductCode = "-1";
                        break;
                    }
                }
            }

            return ProductCode;
        }

        private void CalcProductionProgress(string mBatchCode)
        {
            var cmProgress = new SqlCommand("Select dbo.GetBatchProductionProgress('" + mBatchCode + "')", Module1.cnProductionPlanning);
            SqlTransaction trnProgress = null;
            try
            {
                Cursor = Cursors.WaitCursor;
                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();
                trnProgress = Module1.cnProductionPlanning.BeginTransaction();
                daProgress.UpdateCommand.Transaction = trnProgress;
                cmProgress.Transaction = trnProgress;
                double mProgressPercent = Conversions.ToDouble(cmProgress.ExecuteScalar());
                cmProgress.CommandText = "Select IsNull(Min(StartDate),0) From Tbl_RealProduction Where SubbatchCode IN (Select SubbatchCode From Tbl_ProductionSubbatchs Where BatchCode='" + mBatchCode + "')";
                var drProgress = dsProductionPlanning.Tables["Tbl_ProductionBatchs"].Select("BatchCode='" + mBatchCode + "'");
                drProgress[0]["RealStartDate"] = cmProgress.ExecuteScalar();
                cmProgress.CommandText = "Select IsNull(Min(PlanningStartDate),0) From Tbl_Planning Where SubbatchCode IN (Select SubbatchCode From Tbl_ProductionSubbatchs Where BatchCode='" + mBatchCode + "')";
                drProgress[0]["PlaningStartDate"] = cmProgress.ExecuteScalar();
                drProgress[0]["ProductionProgressMeasure"] = Strings.Mid(mProgressPercent.ToString(), 1, 5);
                daProgress.Update(dsProductionPlanning, "Tbl_ProductionBatchs");
                dsProductionPlanning.AcceptChanges();
                trnProgress.Commit();
                Cursor = Cursors.Default;
            }
            catch (Exception objEx)
            {
                Cursor = Cursors.Default;
                trnProgress.Rollback();
                Logger.SaveError(Name + ".CalcProductionProgress", objEx.Message);
                MessageBox.Show("با مشکل مواجه شد " + mBatchCode + " :محاسبۀ پیشرفت تولید بچ", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
            finally
            {
                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                    Module1.cnProductionPlanning.Close();

                // For I = dsProductionPlanning.Tables.Count - 1 To 2 Step -1
                // dsProductionPlanning.Tables(I).Dispose()
                // dsProductionPlanning.Tables.RemoveAt(I)
                // Next I

                trnProgress.Dispose();
            }
        }
    }
}