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
    public partial class frmPlaningSubbatchsList
    {
        public frmPlaningSubbatchsList()
        {
            InitializeComponent();
            _cmdDeletePriority.Name = "cmdDeletePriority";
            _cmdStopPlanning.Name = "cmdStopPlanning";
            _cmdPriority.Name = "cmdPriority";
            _cmdFilter.Name = "cmdFilter";
            _cmdFind.Name = "cmdFind";
            _cmdEditSubbatch.Name = "cmdEditSubbatch";
            _cmdPlaning.Name = "cmdPlaning";
            _cmdExit.Name = "cmdExit";
            _txtSearch10.Name = "txtSearch10";
            _cbFilter10.Name = "cbFilter10";
            _cbFilter9.Name = "cbFilter9";
            _cbFilter8.Name = "cbFilter8";
            _cbFilter7.Name = "cbFilter7";
            _cbFilter6.Name = "cbFilter6";
            _cbFilter5.Name = "cbFilter5";
            _cbFilter4.Name = "cbFilter4";
            _cbFilter3.Name = "cbFilter3";
            _cbFilter2.Name = "cbFilter2";
            _cbFilter1.Name = "cbFilter1";
            _txtSearch9.Name = "txtSearch9";
            _txtSearch8.Name = "txtSearch8";
            _txtSearch7.Name = "txtSearch7";
            _txtSearch6.Name = "txtSearch6";
            _txtSearch2.Name = "txtSearch2";
            _txtSearch3.Name = "txtSearch3";
            _txtSearch4.Name = "txtSearch4";
            _txtSearch1.Name = "txtSearch1";
            _txtSearch5.Name = "txtSearch5";
            _dgList.Name = "dgList";
            _txtSearch11.Name = "txtSearch11";
            _cbFilter11.Name = "cbFilter11";
        }

        private int mFormMode;
        private int mSearchMode = -1;
        private string mCurrentTableName;
        private DataSetConfiguration DataSetConfig = new DataSetConfiguration();
        private DataRow[] FoundRows = null;
        private IEnumerator FoundRowsEnumerator;
        private int I;

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

        public string CurrentTableName
        {
            get
            {
                return mCurrentTableName;
            }

            set
            {
                mCurrentTableName = value;
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

        private void frmPlaningSubbatchsList_Load(object sender, EventArgs e)
        {
            dgList.Tag = -1;
            Module1.SetButtonsImage(cmdExit, 5);
            Module1.SetButtonsImage(cmdFilter, 3);
            Module1.SetButtonsImage(cmdFind, 10);
            Module1.SetButtonsImage(cmdEditSubbatch, 2);
            Module1.SetButtonsImage(cmdPlaning, 7);
            dgList.Sorted += Module1.DataGridViews_Sorted_EventHandler;

            // Dim SelectStr As String = "Select A.StopPlanning,A.ProductionPriority,D.ProductCode,B.SequenceNo AS BatchPriority,A.SubbatchCode," &
            // "       A.ProductionQuantityinSubbatch,A.SubbatchFirstDeliveryDate,A.PlanningStartDate," &
            // "       (Select Min(StartDate) From dbo.Tbl_RealProduction Where SubbatchCode = A.SubbatchCode)As RealStartDate," &
            // "       IsNull(dbo.GetSubbatchProductionProgress(A.SubbatchCode, B.ProductTreeCode),0) As ProductionProgress," &
            // "       A.BatchCode,A.PlanningStartHour,A.PlanningEndDate,A.PlanningEndHour,A.SubbatchNo,B.ProductTreeCode,A.TransferMinimumQuantity," &
            // "      (Select IsNull(Sum(IntactQuantity),0) From Tbl_RealProduction Where SubbatchCode=A.SubbatchCode And OperationCode IN (Select OperationCode From dbo.Tbl_OperationNetworkPaths Where TreeCode=B.ProductTreeCode And PathCode=1 And ItemPriority=(Select Count(ItemPriority) From dbo.Tbl_OperationNetworkPaths Where TreeCode=B.ProductTreeCode And PathCode=1))) As ProductionQty," &
            // "       B.FirstDelivaryDate,B.FinishedDate " &
            // "From   dbo.Tbl_ProductionSubbatchs A INNER JOIN " &
            // "       dbo.Tbl_ProductionBatchs B ON A.BatchCode = B.BatchCode INNER JOIN " &
            // "       dbo.Tbl_ProductTree D ON B.ProductTreeCode = D.TreeCode Where B.FinishedDate Is Null Or B.FinishedDate = '0' Or B.FinishedDate = '' " &
            // " ORDER BY A.ProductionPriority, A.PlanningStartDate"

            // DataSetConfig.FillDataSet("Tbl_ProductionSubbatchs", "Tbl_PlanningSubbatchs", SelectStr, "SubbatchCode")
            // Prepare_To_Show_TablesRecordList("Tbl_PlanningSubbatchs", MessagesTitle)
            LoadGridData();
            // ''''''''''''''''''''''''''''''''''''''''
            // '''  Set User Access Right  '''''''''''''
            cmdEditSubbatch.Enabled = Module_UserAccess.HaveAccessToItem(60);
            cmdPlaning.Enabled = Module_UserAccess.HaveAccessToItem(61);
            cmdPriority.Enabled = Module_UserAccess.HaveAccessToItem(62);
            cmdStopPlanning.Enabled = Module_UserAccess.HaveAccessToItem(63);
            // ''''''''''''''''''''''''''''''''''''''' 
            // ''''''''''''''''''''''''''''''''''''''''
        }

        private void LoadGridData()
        {
            string SelectStr = 
                "Select A.StopPlanning,A.ProductionPriority,D.ProductCode, D.TreeTitle,B.SequenceNo AS BatchPriority,A.SubbatchCode," +
                "       A.ProductionQuantityinSubbatch,A.SubbatchFirstDeliveryDate,A.PlanningStartDate," +
                "       (Select Min(StartDate) From dbo.Tbl_RealProduction Where SubbatchCode = A.SubbatchCode)As RealStartDate," +
                "       IsNull(dbo.GetSubbatchProductionProgress(A.SubbatchCode, B.ProductTreeCode),0) As ProductionProgress," +
                "       A.BatchCode,A.PlanningStartHour,A.PlanningEndDate,A.PlanningEndHour,A.SubbatchNo,B.ProductTreeCode,A.TransferMinimumQuantity," +
                "      (Select IsNull(Sum(IntactQuantity),0) From Tbl_RealProduction Where SubbatchCode=A.SubbatchCode And OperationCode IN (Select OperationCode From dbo.Tbl_OperationNetworkPaths Where TreeCode=B.ProductTreeCode And PathCode=1 And ItemPriority=(Select Count(ItemPriority) From dbo.Tbl_OperationNetworkPaths Where TreeCode=B.ProductTreeCode And PathCode=1))) As ProductionQty," +
                "       B.FirstDelivaryDate,B.FinishedDate " +
                "From   dbo.Tbl_ProductionSubbatchs A INNER JOIN " +
                "       dbo.Tbl_ProductionBatchs B ON A.BatchCode = B.BatchCode INNER JOIN " +
                "       dbo.Tbl_ProductTree D ON B.ProductTreeCode = D.TreeCode Where B.FinishedDate Is Null Or B.FinishedDate = '0' Or B.FinishedDate = '' " +
                " ORDER BY A.ProductionPriority, A.PlanningStartDate";
            DataSetConfig.FillDataSet("Tbl_ProductionSubbatchs", "Tbl_PlanningSubbatchs", SelectStr, "SubbatchCode");
            Prepare_To_Show_TablesRecordList("Tbl_PlanningSubbatchs", Module1.MessagesTitle);
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmPlaningSubbatchsList_FormClosing(object sender, FormClosingEventArgs e)
        {
            Module1.SaveGridColumnsWidth(Name, 0, dgList);
            dgList.DataSource = null;
            DataSetConfig = null;
            SearchMode = -1;
            FoundRows = null;
            FoundRowsEnumerator = null;
        }

        private void frmPlaningSubbatchsList_Resize(object sender, EventArgs e)
        {
            tsslSearchMode.Width = (int)Math.Round(0.15d * StatusStrip1.Width);
            ToolStripStatusLabel1.Width = (int)Math.Round(0.14d * StatusStrip1.Width);
            ToolStripStatusLabel2.Width = (int)Math.Round(0.14d * StatusStrip1.Width);
            ToolStripStatusLabel3.Width = (int)Math.Round(0.15d * StatusStrip1.Width);
            ToolStripStatusLabel4.Width = (int)Math.Round(0.15d * StatusStrip1.Width);
            tsslRecNo.Width = (int)Math.Round(0.25d * StatusStrip1.Width);
        }

        private void dgList_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            // If tsslSearchMode.Text <> "نمایش کلی" Then
            // SetSearchControlsLocation()
            // End If
        }

        private void dgList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int RecordCount = Conversions.ToInteger(Interaction.IIf(dgList.DataSource is DataSet, BindingContext[dsProductionPlanning, CurrentTableName].Count, dgList.Rows.Count));
            int CurrentRecord = e.RowIndex + 1;
            tsslRecNo.Text = "رکورد  " + CurrentRecord.ToString() + "  از  " + RecordCount.ToString();
        }

        private void dgList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgList.Columns[e.ColumnIndex].Name.Equals("SubbatchFirstDeliveryDate") || dgList.Columns[e.ColumnIndex].Name.Equals("PlanningStartDate") || dgList.Columns[e.ColumnIndex].Name.Equals("RealStartDate"))
            {
                if (!Information.IsDBNull(e.Value))
                {
                    e.Value = Strings.Mid(Conversions.ToString(e.Value), 1, 4) + "/" + Strings.Mid(Conversions.ToString(e.Value), 5, 2) + "/" + Strings.Mid(Conversions.ToString(e.Value), 7, 2);
                }
            }

            if (dgList.Columns[e.ColumnIndex].Name.Equals("StopPlanning"))
            {
                if (Conversions.ToBoolean(e.Value))
                {
                    dgList.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                }
                else
                {
                    dgList.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = dgList.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Style.BackColor;
                }
            }

            if (e.RowIndex > -1)
            {
                dgList.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
            }
        }

        private void cmdEditSubbatch_Click(object sender, EventArgs e)
        {
            if (dgList.Rows.Count == 0)
            {
                MessageBox.Show("رکوردی وجود ندارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                return;
            }

            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(dgList.CurrentRow.Cells["FinishedDate"].Value, 0, false)))
            {
                MessageBox.Show("ساب بچ انتخاب شده به علت بسته شدن بچ، مجاز به تغییر اطلاعات نمی باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                return;
            }

            string Query = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Select A.SubbatchCode,A.DetailCode,D.DetailName,A.Stock,A.RequirementQuantity,D.ParentQuantity,D.ParentDetailCode,D.LevelNo " + "From   dbo.Tbl_ProductionSubbatchsDetail A INNER JOIN " + "       dbo.Tbl_ProductionSubbatchs B ON A.SubbatchCode=B.SubbatchCode INNER JOIN " + "       dbo.Tbl_ProductionBatchs C ON B.BatchCode=C.BatchCode INNER JOIN " + "       dbo.Tbl_ProductTreeDetails D ON C.ProductTreeCode=D.TreeCode And A.DetailCode=D.DetailCode " + "Where  A.SubbatchCode='", dgList.CurrentRow.Cells["SubbatchCode"].Value), "'"));
            DataSetConfig.FillDataSet("Tbl_ProductionSubbatchsDetail", "Tbl_ProductionSubbatchsDetail", Query, "SubbatchCode", "DetailCode");
            Query = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select C.* " + "From   dbo.Tbl_ProductionSubbatchs A INNER JOIN " + "       dbo.Tbl_ProductionBatchs B ON A.BatchCode=B.BatchCode INNER JOIN " + "       dbo.Tbl_ProductTreeDetails C ON B.ProductTreeCode=C.TreeCode " + "Where  A.SubbatchCode='", dgList.CurrentRow.Cells["SubbatchCode"].Value), "' "), "Order By C.LevelNo,C.DetailCode"));
            DataSetConfig.FillDataSet("Tbl_ProductTreeDetails", "Tbl_ProductTreeDetails", Query, "TreeCode", "DetailCode");
            var objForm = new frmProductionSubbatch();
            objForm.ListForm = this;
            if (Information.IsDBNull(dgList.CurrentRow.Cells["RealStartDate"].Value) || Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgList.CurrentRow.Cells["RealStartDate"].Value, Constants.vbNullString, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgList.CurrentRow.Cells["RealStartDate"].Value, "0", false)))
            {
                objForm.EditMode = 1;
            }
            else
            {
                objForm.EditMode = 2;
                // MessageBox.Show("ساب بچ انتخاب شده به علت وارد تولید شدن، مجاز به تغییر اطلاعات نمی باشد", MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, False)
                // Exit Sub
            }

            objForm.ShowDialog();
            objForm.Dispose();
        }

        private void cmdPriority_Click(object sender, EventArgs e)
        {
            var cmPriority = new SqlCommand("Select IsNull(Max(ProductionPriority),0) From dbo.Tbl_ProductionSubbatchs " + "Where  SubbatchCode IN(Select Distinct SubbatchCode From dbo.Tbl_RealProduction)", Module1.cnProductionPlanning);
            SqlTransaction trnPriority = null;
            long LastPriorityNo;
            var daPriority = new SqlDataAdapter("Select SubbatchCode From   dbo.Tbl_ProductionSubbatchs " + "Where  SubbatchCode NOT IN(Select Distinct SubbatchCode From dbo.Tbl_RealProduction)", Module1.cnProductionPlanning);
            var dtPriority = new DataTable("Tbl_PriorityList");
            DataRow[] drCurrent = null;
            CreateDataAdapterCommands(daPriority);
            try
            {
                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();
                trnPriority = Module1.cnProductionPlanning.BeginTransaction();
                cmPriority.Transaction = trnPriority;
                daPriority.SelectCommand.Transaction = trnPriority;
                daPriority.UpdateCommand.Transaction = trnPriority;

                // بدست آوردن شماره آخرین اولویت، از ساب بچ هایی که وارد تولید شده اند
                LastPriorityNo = Conversions.ToLong(cmPriority.ExecuteScalar().ToString());

                // صفر کردن اولویت ساب بچ هایی که وارد تولید نشده اند
                daPriority.Fill(dtPriority);
                for (int ListCounter = 0, loopTo = dtPriority.Rows.Count - 1; ListCounter <= loopTo; ListCounter++)
                {
                    drCurrent = dsProductionPlanning.Tables["Tbl_PlanningSubbatchs"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("SubbatchCode='", dtPriority.Rows[ListCounter]["SubbatchCode"]), "'")));
                    if (drCurrent.Length > 0)
                    {
                        if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(drCurrent[0]["ProductionPriority"], 0, false)))
                        {
                            drCurrent[0]["ProductionPriority"] = 0;
                        }
                    }
                }

                SaveChanges(daPriority);

                // تعیین اولویت ساب بچ هایی که دارای تاریخ اولین محموله ارسالی می باشند
                daPriority.SelectCommand.CommandText = "Select SubbatchCode From dbo.Tbl_ProductionSubbatchs " + "Where  StopPlanning = 0 And SubbatchFirstDeliveryDate > '0' AND SubbatchCode NOT IN(Select DISTINCT SubbatchCode From dbo.Tbl_RealProduction)" + "Order By SubbatchFirstDeliveryDate";
                dtPriority = new DataTable();
                daPriority.Fill(dtPriority);
                for (int ListCounter = 0, loopTo1 = dtPriority.Rows.Count - 1; ListCounter <= loopTo1; ListCounter++)
                {
                    drCurrent = dsProductionPlanning.Tables["Tbl_PlanningSubbatchs"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("SubbatchCode='", dtPriority.Rows[ListCounter]["SubbatchCode"]), "'")));
                    if (drCurrent.Length > 0)
                    {
                        LastPriorityNo += 1L;
                        drCurrent[0]["ProductionPriority"] = LastPriorityNo;
                    }
                }

                SaveChanges(daPriority);

                // تعیین اولویت ساب بچ هایی که حداقل ساب بچ اول آنها اولویت بندی شده اند
                daPriority.SelectCommand.CommandText = "Select BatchCode From dbo.Tbl_ProductionSubbatchs " + "Where  StopPlanning = 0 And SubbatchNo = 1 AND ProductionPriority > 0 " + "Order By ProductionPriority";
                dtPriority = new DataTable();
                daPriority.Fill(dtPriority);
                for (int ListCounter = 0, loopTo2 = dtPriority.Rows.Count - 1; ListCounter <= loopTo2; ListCounter++)
                {
                    drCurrent = dsProductionPlanning.Tables["Tbl_PlanningSubbatchs"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("BatchCode='", dtPriority.Rows[ListCounter]["BatchCode"]), "'")));
                    if (drCurrent.Length > 0)
                    {
                        for (short I = 0, loopTo3 = (short)(drCurrent.Length - 1); I <= loopTo3; I++)
                        {
                            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(drCurrent[I]["ProductionPriority"], 0, false)))
                            {
                                LastPriorityNo += 1L;
                                drCurrent[I]["ProductionPriority"] = LastPriorityNo;
                            }
                        }
                    }
                }

                SaveChanges(daPriority);

                // اولویت بندی ساب بچ هایی که ساب بچ اول آنها اولویت بندی نشده است
                daPriority.SelectCommand.CommandText = "Select B.SubbatchCode " + "From   dbo.Tbl_ProductionBatchs A INNER JOIN dbo.Tbl_ProductionSubbatchs B ON A.BatchCode=B.BatchCode " + "Where  B.StopPlanning = 0 And B.ProductionPriority=0 Order By A.FirstDelivaryDate,B.SubbatchNo";
                dtPriority = new DataTable();
                daPriority.Fill(dtPriority);
                for (int ListCounter = 0, loopTo4 = dtPriority.Rows.Count - 1; ListCounter <= loopTo4; ListCounter++)
                {
                    drCurrent = dsProductionPlanning.Tables["Tbl_PlanningSubbatchs"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("SubbatchCode='", dtPriority.Rows[ListCounter]["SubbatchCode"]), "'")));
                    if (drCurrent.Length > 0)
                    {
                        LastPriorityNo += 1L;
                        drCurrent[0]["ProductionPriority"] = LastPriorityNo;
                    }
                }

                SaveChanges(daPriority);
                trnPriority.Commit();
                MessageBox.Show("عملیات اولویت بندی ساب بچ های تولید با موفقیت انجام شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
            catch (Exception objEx)
            {
                dsProductionPlanning.RejectChanges();
                trnPriority.Rollback();
                MessageBox.Show(objEx.Message, " Production Planning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                    Module1.cnProductionPlanning.Close();
                trnPriority.Dispose();
                daPriority.Dispose();
                dtPriority.Dispose();
            }
        }

        private void cmdStopPlanning_Click(object sender, EventArgs e)
        {
            if (dgList.Rows.Count == 0)
            {
                MessageBox.Show("رکوردی وجود ندارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                return;
            }
            else if (dgList.SelectedRows.Count == 1 && !Conversions.ToBoolean(dgList.SelectedRows[0].Cells["StopPlanning"].Value))
            {
                string mSCode = Conversions.ToString(dgList.SelectedRows[0].Cells["SubbatchCode"].Value);
                string mTreeCode = Conversions.ToString(dgList.SelectedRows[0].Cells["ProductTreeCode"].Value);
                {
                    var withBlock = new frmSubbatchPlanningStop();
                    withBlock.TreeCode = mTreeCode;
                    withBlock.SubbatchCode = mSCode;
                    if (withBlock.ShowDialog() == DialogResult.OK)
                    {
                        SqlTransaction trnStop = null;
                        using (var cn = new SqlConnection(Module1.PlanningCnnStr))
                        {
                            try
                            {
                                cn.Open();
                                trnStop = cn.BeginTransaction();
                                var cm = new SqlCommand("Update Tbl_ProductionSubbatchs Set PlanningStartDate = 0,PlanningStartHour = 0,PlanningEndHour = 0,PlanningEndDate = 0,ProductionPriority = 0, StopPlanning = 1 Where SubbatchCode = '" + mSCode + "'", cn);
                                cm.Transaction = trnStop;
                                cm.ExecuteNonQuery();
                                var mRow = dsProductionPlanning.Tables["Tbl_PlanningSubbatchs"].Select("SubbatchCode = '" + mSCode + "'");
                                mRow[0].BeginEdit();
                                mRow[0]["PlanningStartDate"] = 0;
                                mRow[0]["PlanningStartHour"] = 0;
                                mRow[0]["PlanningEndDate"] = 0;
                                mRow[0]["PlanningEndHour"] = 0;
                                mRow[0]["ProductionPriority"] = 0;
                                mRow[0]["StopPlanning"] = 1;
                                mRow[0].EndEdit();
                                cm.CommandText = "Delete From Tbl_Planning Where TreeCode =" + mTreeCode + " And SubbatchCode = '" + mSCode + "' And Not OperationCode IN (" + withBlock.CheckedOperations + ")";
                                cm.ExecuteNonQuery();
                                dsProductionPlanning.Tables["Tbl_PlanningSubbatchs"].AcceptChanges();
                                trnStop.Commit();
                                MessageBox.Show("انجام شد " + mSCode + " :ثبت توقف برنامه ریزی ساب بچ", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                            }
                            catch (Exception ex)
                            {
                                Logger.LogException("cmdStopPlanning_Click", ex);
                                trnStop.Rollback();
                                dsProductionPlanning.Tables["Tbl_PlanningSubbatchs"].RejectChanges();
                                MessageBox.Show("با اشکال مواجه شد " + mSCode + " :ثبت توقف برنامه ریزی ساب بچ", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                            }
                            finally
                            {
                                if (cn.State == ConnectionState.Open)
                                    cn.Close();
                            }
                        }
                    }
                }

                return;
            }

            string StopMessages = "";
            bool mViewConfirm = true;
            var mdrGridViewRowsIndex = new ArrayList[dgList.SelectedRows.Count];
            for (int I = 0, loopTo = dgList.SelectedRows.Count - 1; I <= loopTo; I++)
            {
                if (Conversions.ToBoolean(!(bool)dgList.SelectedRows[I].Cells["StopPlanning"].Value))
                {
                    mViewConfirm = false;
                }

                mdrGridViewRowsIndex[I] = new ArrayList();
                mdrGridViewRowsIndex[I].Add(dgList.SelectedRows[I].Cells["BatchCode"].Value);
                mdrGridViewRowsIndex[I].Add(dgList.SelectedRows[I].Cells["SubbatchCode"].Value);
                mdrGridViewRowsIndex[I].Add(dgList.SelectedRows[I].Cells["SubbatchNo"].Value);
                mdrGridViewRowsIndex[I].Add(dgList.SelectedRows[I].Cells["ProductTreeCode"].Value);
                mdrGridViewRowsIndex[I].Add(dgList.SelectedRows[I].Cells["StopPlanning"].Value);
            }

            if (mViewConfirm || MessageBox.Show("با ثبت توقف برنامه ریزی ساب بچ(ها)، مشخصات برنامه ریزی انجام شده و اولویت آن(ها) نیز حذف می شود. آیا ادامه می دهید", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading, false) == DialogResult.Yes)
            {
                if (mdrGridViewRowsIndex.Length > 0)
                {
                    DeleteSubbatchsPlanning(mdrGridViewRowsIndex, ref StopMessages, true);
                    StopMessages += Constants.vbCrLf;
                    for (int I = 0, loopTo1 = mdrGridViewRowsIndex.Length - 1; I <= loopTo1; I++)
                    {
                        string mSCode = Conversions.ToString(mdrGridViewRowsIndex[I][1]);
                        using (var cn = new SqlConnection(Module1.PlanningCnnStr))
                        {
                            try
                            {
                                cn.Open();
                                var cm = new SqlCommand("Update Tbl_ProductionSubbatchs Set ProductionPriority = 0, StopPlanning = 1 Where SubbatchCode = '" + mSCode + "'", cn);
                                var mRow = dsProductionPlanning.Tables["Tbl_PlanningSubbatchs"].Select("SubbatchCode = '" + mSCode + "'");
                                if (Conversions.ToBoolean(mdrGridViewRowsIndex[I][4]))
                                {
                                    cm.CommandText = "Update Tbl_ProductionSubbatchs Set StopPlanning = 0 Where SubbatchCode = '" + mSCode + "'";
                                }

                                cm.ExecuteNonQuery();
                                if (Conversions.ToBoolean(mdrGridViewRowsIndex[I][4]))
                                {
                                    mRow[0]["StopPlanning"] = 0;
                                    dsProductionPlanning.Tables["Tbl_PlanningSubbatchs"].AcceptChanges();
                                    StopMessages += Constants.vbCrLf + "انجام شد " + mSCode + " :ثبت لغو توقف برنامه ریزی ساب بچ";
                                }
                                else
                                {
                                    mRow[0]["ProductionPriority"] = 0;
                                    mRow[0]["StopPlanning"] = 1;
                                    dsProductionPlanning.Tables["Tbl_PlanningSubbatchs"].AcceptChanges();
                                    StopMessages += Constants.vbCrLf + "انجام شد " + mSCode + " :ثبت توقف برنامه ریزی ساب بچ";
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.SaveError(Name + ".cmdStopPlanning_Click", ex.Message);
                                if (Conversions.ToBoolean(mdrGridViewRowsIndex[I][4]))
                                {
                                    StopMessages += Constants.vbCrLf + "با اشکال مواجه شد " + mSCode + " :ثبت لغو توقف برنامه ریزی ساب بچ" + Constants.vbCrLf;
                                }
                                else
                                {
                                    StopMessages += Constants.vbCrLf + "با اشکال مواجه شد " + mSCode + " :ثبت توقف برنامه ریزی ساب بچ" + Constants.vbCrLf;
                                }
                            }
                        }
                    }
                }
                else
                {
                    StopMessages = "ثبت توقف / لغو توقف ساب بچ(ها) انجام شد";
                }

                if (!string.IsNullOrEmpty(StopMessages))
                {
                    MessageBox.Show(StopMessages, Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                }
            }
        }

        private void cmdDeletePriority_Click(object sender, EventArgs e)
        {
            if (dgList.Rows.Count == 0)
            {
                MessageBox.Show("رکوردی وجود ندارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                return;
            }

            string DeleteMessages = Constants.vbNullString;
            if (MessageBox.Show("با حذف اولويت بندي ساب بچ، مشخصات برنامه ريزي آن نيز حذف مي گردد. آيا جهت حذف مطمئن هستيد", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading, false) == DialogResult.Yes)
            {
                var cmDeletePriority = new SqlCommand("", Module1.cnProductionPlanning);
                var mdrGridViewRowsIndex = new ArrayList[dgList.SelectedRows.Count];
                for (int I = 0, loopTo = dgList.SelectedRows.Count - 1; I <= loopTo; I++)
                {
                    mdrGridViewRowsIndex[I] = new ArrayList();
                    mdrGridViewRowsIndex[I].Add(dgList.SelectedRows[I].Cells["BatchCode"].Value);
                    mdrGridViewRowsIndex[I].Add(dgList.SelectedRows[I].Cells["SubbatchCode"].Value);
                    mdrGridViewRowsIndex[I].Add(dgList.SelectedRows[I].Cells["SubbatchNo"].Value);
                    mdrGridViewRowsIndex[I].Add(dgList.SelectedRows[I].Cells["ProductTreeCode"].Value);
                    mdrGridViewRowsIndex[I].Add(dgList.SelectedRows[I].Cells["StopPlanning"].Value);
                }

                for (int I = 0, loopTo1 = mdrGridViewRowsIndex.Length - 1; I <= loopTo1; I++)
                {
                    string mSBCode = mdrGridViewRowsIndex[I][1].ToString();
                    SqlTransaction trnDeletePlanning = null;
                    try
                    {
                        if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                            Module1.cnProductionPlanning.Open();
                        trnDeletePlanning = Module1.cnProductionPlanning.BeginTransaction();
                        cmDeletePriority.Transaction = trnDeletePlanning;
                        var drDeletePriority = dsProductionPlanning.Tables["Tbl_PlanningSubbatchs"].Select("BatchCode='" + mdrGridViewRowsIndex[I][0].ToString() + "' And SubbatchNo>" + mdrGridViewRowsIndex[I][2].ToString());
                        for (short J = 0, loopTo2 = (short)(drDeletePriority.Length - 1); J <= loopTo2; J++)
                        {
                            cmDeletePriority.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Update Tbl_ProductionSubbatchs Set ProductionPriority = 0,PlanningStartDate = 0 Where SubbatchCode='", drDeletePriority[J]["SubbatchCode"]), "'"));
                            cmDeletePriority.ExecuteNonQuery();
                            DeleteSubbatchPlanningRows(Conversions.ToString(drDeletePriority[J]["SubbatchCode"]), Conversions.ToString(drDeletePriority[J]["ProductTreeCode"]), trnDeletePlanning);
                            drDeletePriority[J].BeginEdit();
                            drDeletePriority[J]["ProductionPriority"] = 0;
                            drDeletePriority[J]["PlanningStartDate"] = 0;
                            drDeletePriority[J]["PlanningStartHour"] = 0;
                            drDeletePriority[J]["PlanningEndDate"] = 0;
                            drDeletePriority[J]["PlanningEndHour"] = 0;
                            drDeletePriority[J].EndEdit();
                        }

                        cmDeletePriority.CommandText = "Update Tbl_ProductionSubbatchs Set ProductionPriority = 0,PlanningStartDate = 0 Where SubbatchCode='" + mSBCode + "'";
                        cmDeletePriority.ExecuteNonQuery();
                        DeleteSubbatchPlanningRows(mSBCode, mdrGridViewRowsIndex[I][3].ToString(), trnDeletePlanning);
                        // cmDeletePriority.CommandText = "Delete From Tbl_Planning Where SubbatchCode='" & mSBCode & "' And PlanningCode Not IN (Select PlanningCode From Tbl_RealProduction)"
                        // cmDeletePriority.ExecuteNonQuery()

                        var drPLanningSubbatch = dsProductionPlanning.Tables["Tbl_PlanningSubbatchs"].Select("SubbatchCode='" + mSBCode + "'");
                        drPLanningSubbatch[0].BeginEdit();
                        drPLanningSubbatch[0]["ProductionPriority"] = 0;
                        drPLanningSubbatch[0]["PlanningStartDate"] = 0;
                        drPLanningSubbatch[0]["PlanningStartHour"] = 0;
                        drPLanningSubbatch[0]["PlanningEndDate"] = 0;
                        drPLanningSubbatch[0]["PlanningEndHour"] = 0;
                        drPLanningSubbatch[0].EndEdit();
                        if (drPLanningSubbatch[0]["SubbatchNo"].ToString().Equals("1"))
                        {
                            cmDeletePriority.CommandText = "Update Tbl_ProductionBatchs Set PlaningStartDate = '0' Where BatchCode = '" + drPLanningSubbatch[0]["BatchCode"].ToString() + "'";
                            cmDeletePriority.ExecuteNonQuery();
                        }

                        dsProductionPlanning.AcceptChanges();
                        trnDeletePlanning.Commit();
                    }

                    // DeleteMessages &= vbCrLf & "حذف شد " & mSBCode & " :اولويت بندي ساب بچ"
                    catch (Exception objEx)
                    {
                        dsProductionPlanning.Tables["Tbl_PlanningSubbatchs"].RejectChanges();
                        trnDeletePlanning.Rollback();
                        Logger.SaveError(Name + ".cmdDeletePriority_Click", objEx.Message);
                        DeleteMessages += Constants.vbCrLf + "با مشکل مواجه شد " + mSBCode + " :حذف اولويت بندي ساب بچ" + Constants.vbCrLf;
                    }
                    finally
                    {
                        if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                            Module1.cnProductionPlanning.Close();
                    }
                }

                if (!string.IsNullOrEmpty(DeleteMessages))
                {
                    MessageBox.Show(DeleteMessages, "برنامه ريزي توليد ", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                }
            }
        }

        private void cmdPlaning_Click(object sender, EventArgs e)
        {
            if (dsProductionPlanning.Tables["Tbl_PlanningSubbatchs"].Select("ProductionPriority > 0").Length == 0)
            {
                MessageBox.Show("اولویت بندی انجام نشده است", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                return;
            }

            {
                var withBlock = new frmPlanning();
                withBlock.ShowDialog();
            }

            LoadGridData();
        }

        private void cmdFilter_Click(object sender, EventArgs e)
        {
            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgList.Tag, -1, false)))
            {
                if (ReferenceEquals(sender, cmdFilter))
                {
                    dgList.Tag = SearchModeEnum.SM_FILTER;
                    tsslSearchMode.Text = "فیلتر اطلاعات";
                    SetFilterCombosLocation();
                }
                else
                {
                    dgList.Tag = SearchModeEnum.SM_FIND;
                    tsslSearchMode.Text = "جستجوی اطلاعات";
                    SetSearchControlsLocation();
                }

                dgList.ColumnHeadersHeight = 50;
                dgList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft;
            }
            else
            {
                if (ReferenceEquals(sender, cmdFilter))
                {
                    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgList.Tag, SearchModeEnum.SM_FILTER, false)))
                    {
                        for (I = 1; I <= 11; I++)
                            Controls["Panel1"].Controls["cbFilter" + I].Visible = false;
                        dgList.ColumnHeadersHeight = 40;
                        dgList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dgList.Tag = -1;
                        tsslSearchMode.Text = "نمایش کلی";
                    }
                    else if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgList.Tag, SearchModeEnum.SM_FIND, false)))
                    {
                        dgList.Tag = SearchModeEnum.SM_FILTER;
                        tsslSearchMode.Text = "فیلتر اطلاعات";
                        SetFilterCombosLocation();
                    }
                }
                else if (ReferenceEquals(sender, cmdFind))
                {
                    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgList.Tag, SearchModeEnum.SM_FIND, false)))
                    {
                        for (I = 1; I <= 11; I++)
                            Controls["Panel1"].Controls["txtSearch" + I].Visible = false;
                        dgList.ColumnHeadersHeight = 40;
                        dgList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dgList.Tag = -1;
                        tsslSearchMode.Text = "نمایش کلی";
                    }
                    else if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgList.Tag, SearchModeEnum.SM_FILTER, false)))
                    {
                        dgList.Tag = SearchModeEnum.SM_FIND;
                        tsslSearchMode.Text = "جستجوی اطلاعات";
                        SetSearchControlsLocation();
                    }
                }

                if (dgList.DataSource is DataView)
                {
                    ((DataView)dgList.DataSource).RowFilter = Constants.vbNullString;
                }
            }
        }

        private void cbFilters_DropDown(object sender, EventArgs e)
        {
            ComboBox CurrentCombo;
            CurrentCombo = (ComboBox)sender;
            CurrentCombo.Items.Clear();
            CurrentCombo.Items.Add("---< همه >---");
            for (short ItemCounter = 0, loopTo = (short)(dgList.Rows.Count - 1); ItemCounter <= loopTo; ItemCounter++)
            {
                if (!CurrentCombo.Items.Contains(dgList.Rows[(int)ItemCounter].Cells[CurrentCombo.Tag.ToString()].Value))
                {
                    CurrentCombo.Items.Add(dgList.Rows[(int)ItemCounter].Cells[CurrentCombo.Tag.ToString()].Value);
                }
            }
        }

        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterValue = Constants.vbNullString;
            ComboBox CurrentCombo = (ComboBox)sender;
            ComboBox CurrentVisibleCombo;
            string Control;
            if (CurrentCombo.Text == "---< همه >---")
            {
                CurrentCombo.SelectedIndex = -1;
            }

            for (short ComboCounter = 1; ComboCounter <= 11; ComboCounter++)
            {
                Control = "cbFilter" + ComboCounter;
                if (Controls["Panel1"].Controls[Control].Visible)
                {
                    CurrentVisibleCombo = (ComboBox)Controls["Panel1"].Controls[Control];
                    if (CurrentVisibleCombo.SelectedIndex > -1)
                    {
                        FilterValue = Conversions.ToString(Interaction.IIf(string.IsNullOrEmpty(FilterValue), Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Convert(", Controls["Panel1"].Controls[Control].Tag), ",System.String) ='"), Controls["Panel1"].Controls[Control].Text), "'"), Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(FilterValue + " And Convert(", Controls["Panel1"].Controls[Control].Tag), ",System.String) ='"), Controls["Panel1"].Controls[Control].Text), "'")));
                    }
                }
            }

            CurrentCombo = null;
            CurrentVisibleCombo = null;
            DataView GridDataView;
            if (dgList.DataSource is DataView)
            {
                GridDataView = (DataView)dgList.DataSource;
                GridDataView.RowFilter = FilterValue;
            }
            else
            {
                GridDataView = dsProductionPlanning.Tables[dgList.DataMember].DefaultView;
                GridDataView.RowFilter = FilterValue;
                SetGridColumns(GridDataView, "");
            }
        }

        private void dgList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control & e.KeyCode == Keys.F12)
            {
                Module1.ExportGridToExcel(dgList);
            }
        }

        private void txtSearchs_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox CurrentTextBox = (TextBox)sender;
            if (Strings.Asc(e.KeyChar) == (int)Keys.Back)
            {
                if (string.IsNullOrEmpty(CurrentTextBox.Text))
                {
                    string PreControlName = "txtSearch";
                    PreControlName = Conversions.ToString(Interaction.IIf(Conversions.ToInteger(Strings.Right(CurrentTextBox.Name, 1)) < 11, PreControlName + (Conversions.ToInteger(Strings.Right(CurrentTextBox.Name, 1)) + 1).ToString(), Constants.vbNullString));
                    if (!string.IsNullOrEmpty(PreControlName))
                    {
                        if (Controls["Panel1"].Controls[PreControlName].Visible)
                        {
                            Controls["Panel1"].Controls[PreControlName].Focus();
                        }
                    }
                }
            }
            else if (Strings.Asc(e.KeyChar) == (int)Keys.Enter)
            {
                string Control;
                TextBox CurrentVisibleTextBox;
                string FilterValue = Constants.vbNullString;
                for (short TextBoxCounter = 1; TextBoxCounter <= 11; TextBoxCounter++)
                {
                    Control = "txtSearch" + TextBoxCounter;
                    if (Controls["Panel1"].Controls[Control].Visible)
                    {
                        CurrentVisibleTextBox = (TextBox)Controls["Panel1"].Controls[Control];
                        if (!string.IsNullOrEmpty(CurrentVisibleTextBox.Text))
                        {
                            FilterValue = Conversions.ToString(Interaction.IIf(string.IsNullOrEmpty(FilterValue), Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Convert(", Controls["Panel1"].Controls[Control].Tag), ",System.String) LIKE '%"), Controls["Panel1"].Controls[Control].Text), "%'"), Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(FilterValue + " And Convert(", Controls["Panel1"].Controls[Control].Tag), ",System.String) LIKE '%"), Controls["Panel1"].Controls[Control].Text), "%'")));
                        }
                    }
                }

                CurrentVisibleTextBox = null;
                string TableName;
                if (string.IsNullOrEmpty(FilterValue))
                {
                    FoundRows = null;
                }
                else if (FoundRows is null)
                {
                    if (dgList.DataSource is DataView)
                    {
                        DataView GridDataView = (DataView)dgList.DataSource;
                        TableName = GridDataView.Table.TableName;
                        SetGridColumns(dsProductionPlanning, TableName);
                    }
                    else
                    {
                        TableName = dgList.DataMember;
                    }

                    FoundRows = dsProductionPlanning.Tables[TableName].Select(FilterValue);
                    FoundRowsEnumerator = FoundRows.GetEnumerator();
                    if (FoundRowsEnumerator.MoveNext())
                    {
                        I = dsProductionPlanning.Tables[TableName].Rows.IndexOf((DataRow)FoundRowsEnumerator.Current);
                        BindingContext[dsProductionPlanning, TableName].Position = I;
                    }
                    else
                    {
                        MessageBox.Show("موردی یافت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                        FoundRows = null;
                    }
                }
                else
                {
                    TableName = dgList.DataMember;
                    if (FoundRowsEnumerator.MoveNext())
                    {
                        I = dsProductionPlanning.Tables[TableName].Rows.IndexOf((DataRow)FoundRowsEnumerator.Current);
                        BindingContext[dsProductionPlanning, TableName].Position = I;
                    }
                    else
                    {
                        MessageBox.Show("مورد دیگری وجود ندارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                        FoundRows = null;
                    }
                }
            }

            CurrentTextBox = null;
        }

        private void SetSearchControlsLocation()
        {
            short I, J, ControlIndex;
            int TempWidth;
            string Control;
            for (I = 1; I <= 11; I++)
            {
                Controls["Panel1"].Controls["cbFilter" + I].Visible = false;
                Controls["Panel1"].Controls["cbFilter" + I].Tag = Constants.vbNullString;
            }

            ControlIndex = 1;
            for (I = (short)dgList.Columns.Count; I >= 1; I += -1)
            {
                if (dgList.Columns[I - 1].Visible)
                {
                    Control = "txtSearch" + ControlIndex;
                    TempWidth = dgList.Width;
                    var loopTo = (short)(I - 1);
                    for (J = 0; J <= loopTo; J++)
                    {
                        if (dgList.Columns[J].Visible)
                        {
                            TempWidth = TempWidth - dgList.Columns[J].Width;
                        }
                    }

                    {
                        var withBlock = Controls["Panel1"];
                        withBlock.Controls[Control].Text = Constants.vbNullString;
                        withBlock.Controls[Control].Top = dgList.Location.Y;
                        withBlock.Controls[Control].Left = TempWidth - dgList.RowHeadersWidth + 4;
                        withBlock.Controls[Control].Width = dgList.Columns[I - 1].HeaderCell.OwningColumn.Width - 2;
                        withBlock.Controls[Control].Visible = true;
                        withBlock.Controls[Control].Tag = dgList.Columns[I - 1].Name;
                    }

                    ControlIndex = (short)(ControlIndex + 1);
                }
            }

            if (dgList.DataSource is DataView)
            {
                DataView GridDataView = (DataView)dgList.DataSource;
                SetGridColumns(dsProductionPlanning, GridDataView.Table.TableName);
            }
        }

        private void SetFilterCombosLocation()
        {
            short I, J, ControlIndex;
            int TempWidth;
            string Control;
            for (I = 1; I <= 11; I++)
            {
                Controls["Panel1"].Controls["txtSearch" + I].Visible = false;
                Controls["Panel1"].Controls["txtSearch" + I].Tag = Constants.vbNullString;
            }

            ControlIndex = 1;
            for (I = (short)dgList.Columns.Count; I >= 1; I += -1)
            {
                if (dgList.Columns[I - 1].Visible)
                {
                    Control = "cbFilter" + ControlIndex;
                    TempWidth = dgList.Width;
                    var loopTo = (short)(I - 1);
                    for (J = 0; J <= loopTo; J++)
                    {
                        if (dgList.Columns[J].Visible)
                        {
                            TempWidth = TempWidth - dgList.Columns[J].Width;
                        }
                    }

                    {
                        var withBlock = Controls["Panel1"];
                        withBlock.Controls[Control].Text = Constants.vbNullString;
                        withBlock.Controls[Control].Top = dgList.Location.Y;
                        withBlock.Controls[Control].Left = TempWidth - dgList.RowHeadersWidth + 6;
                        withBlock.Controls[Control].Width = dgList.Columns[I - 1].HeaderCell.OwningColumn.Width - 2;
                        withBlock.Controls[Control].Visible = true;
                        withBlock.Controls[Control].Tag = dgList.Columns[I - 1].Name;
                    }

                    ControlIndex = (short)(ControlIndex + 1);
                }
            }

            if (!(dgList.DataSource is DataView))
            {
                SetGridColumns(dsProductionPlanning.Tables[dgList.DataMember].DefaultView, "");
            }
        }

        private void SetGridColumns(object DataSource, string Table = Constants.vbNullString)
        {
            {
                var withBlock = dgList;
                withBlock.DataSource = DataSource;
                if (!(DataSource is DataView))
                {
                    withBlock.DataMember = Table;
                }

                withBlock.Columns[0].HeaderText = "توقف";
                withBlock.Columns[1].HeaderText = "اولویت تولید";
                withBlock.Columns[2].HeaderText = "کد محصول";
                withBlock.Columns[3].HeaderText = " محصول";
                withBlock.Columns[4].HeaderText = "سریال بچ تولید";
                withBlock.Columns[5].HeaderText = "کد ساب بچ";
                withBlock.Columns[6].HeaderText = "تعداد";
                withBlock.Columns[7].HeaderText = "موعد تحویل اولین محموله";
                withBlock.Columns[7].DefaultCellStyle.Format = "####/##/##";
                withBlock.Columns[8].HeaderText = "تاریخ شروع برنامه ریزی";
                withBlock.Columns[8].DefaultCellStyle.Format = "####/##/##";
                withBlock.Columns[9].HeaderText = "تاریخ شروع واقعی";
                withBlock.Columns[9].DefaultCellStyle.Format = "####/##/##";
                withBlock.Columns[10].HeaderText = "پیشرفت تولید";
                withBlock.Columns[11].Visible = false;
                withBlock.Columns[12].Visible = false;
                withBlock.Columns[13].Visible = false;
                withBlock.Columns[14].Visible = false;
                withBlock.Columns[15].Visible = false;
                withBlock.Columns[16].Visible = false;
                withBlock.Columns[17].Visible = false;
                withBlock.Columns[18].HeaderText = "تعداد محصول تولید شده";
                withBlock.Columns[19].Visible = false;
                withBlock.Columns[20].Visible = false;
            }

            Module1.SetGridColumnsWidth(Name, 0, dgList);
        }

        public DataRow GetRow()
        {
            object crRow;
            string FindValue = Constants.vbNullString;
            var drFind = new DataRow[1];
            FindValue = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("SubbatchCode='", dgList.CurrentRow.Cells["SubbatchCode"].Value), "'"));
            drFind = dsProductionPlanning.Tables[mCurrentTableName].Select(FindValue);
            crRow = drFind[0];
            return (DataRow)crRow;
        }

        private void Prepare_To_Show_TablesRecordList(string TN, string FC)
        {
            SetGridColumns(dsProductionPlanning, TN);
            CurrentTableName = TN;
            Text = FC;
        }

        private void CreateDataAdapterCommands(SqlDataAdapter daSubbatch)
        {
            daSubbatch.UpdateCommand = new SqlCommand("Update Tbl_ProductionSubbatchs Set ProductionPriority=@ProductionPriority Where SubbatchCode=@SubbatchCode", Module1.cnProductionPlanning);
            {
                var withBlock = daSubbatch.UpdateCommand;
                withBlock.Parameters.Add("@ProductionPriority", SqlDbType.BigInt, default, "ProductionPriority");
                withBlock.Parameters.Add("@SubbatchCode", SqlDbType.VarChar, 50, "SubbatchCode").SourceVersion = DataRowVersion.Original;
            }
        }

        private void CreateDeletePlanningCommand(ref SqlDataAdapter daSubbatch, SqlTransaction mTrn)
        {
            daSubbatch.UpdateCommand = new SqlCommand("Update Tbl_ProductionSubbatchs Set PlanningStartDate=@PlanningStartDate,PlanningStartHour=@PlanningStartHour," + "PlanningEndDate=@PlanningEndDate,PlanningEndHour=@PlanningEndHour " + "Where SubbatchCode=@SubbatchCode", mTrn.Connection);

            {
                var withBlock = daSubbatch.UpdateCommand;
                withBlock.Parameters.Add("@PlanningStartDate", SqlDbType.VarChar, 8, "PlanningStartDate");
                withBlock.Parameters.Add("@PlanningStartHour", SqlDbType.VarChar, 50, "PlanningStartHour");
                withBlock.Parameters.Add("@PlanningEndDate", SqlDbType.VarChar, 8, "PlanningEndDate");
                withBlock.Parameters.Add("@PlanningEndHour", SqlDbType.VarChar, 50, "PlanningEndHour");
                withBlock.Parameters.Add("@SubbatchCode", SqlDbType.VarChar, 50, "SubbatchCode").SourceVersion = DataRowVersion.Original;
                withBlock.Transaction = mTrn;
            }
        }

        private void SaveChanges(SqlDataAdapter daSubbatch)
        {
            var dsChanges = dsProductionPlanning.GetChanges();
            if (dsChanges is null)
            {
                return;
            }

            if (dsChanges.HasErrors)
            {
                dsProductionPlanning.RejectChanges();
            }
            else
            {
                daSubbatch.Update(dsChanges, "Tbl_PlanningSubbatchs");
                dsProductionPlanning.AcceptChanges();
            }

            dsChanges = null;
        }

        private void DeleteSubbatchsPlanning(ArrayList[] mdrGridViewRowsIndex, ref string DeleteMessages, bool CheckStoping)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                for (int I = 0, loopTo = mdrGridViewRowsIndex.Length - 1; I <= loopTo; I++)
                {
                    if (Conversions.ToBoolean(CheckStoping && !(bool)mdrGridViewRowsIndex[I][4]))
                    {
                        string mBCode = Conversions.ToString(mdrGridViewRowsIndex[I][0]);
                        string mSBCode = Conversions.ToString(mdrGridViewRowsIndex[I][1]);
                        string mSubbatchNo = Conversions.ToString(mdrGridViewRowsIndex[I][2]);
                        string mTCode = Conversions.ToString(mdrGridViewRowsIndex[I][3]);
                        var drNextSubbatch = dsProductionPlanning.Tables["Tbl_PlanningSubbatchs"].Select("BatchCode='" + mBCode + "' And SubbatchNo=" + (Conversions.ToInteger(mSubbatchNo) + 1));
                        if (drNextSubbatch.Length > 0 && Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(drNextSubbatch[0]["PlanningStartDate"], 0, false)))
                        {
                            DeleteMessages += Constants.vbCrLf + "دارای برنامه ریزی می باشد، حذف برنامه ریزی مجاز نیست " + mSBCode + " :ساب بچ بعدی مربوط به ساب بچ";
                            goto NextSubbatch;
                        }

                        var daEditSubbatchPlanningInfo = new SqlDataAdapter("", cn);
                        var dtPaths = new DataTable();
                        var cmDeletePLanning = new SqlCommand("", cn);
                        var drPLanningSubbatch = dsProductionPlanning.Tables["Tbl_PlanningSubbatchs"].Select("SubbatchCode='" + mSBCode + "'");
                        SqlTransaction trnDeletePlanning = null;
                        try
                        {
                            cn.Open();
                            trnDeletePlanning = cn.BeginTransaction();
                            cmDeletePLanning.Transaction = trnDeletePlanning;
                            daEditSubbatchPlanningInfo.SelectCommand.Transaction = trnDeletePlanning;
                            cmDeletePLanning.CommandText = "Select IsNull(Max(PathCode),0) From Tbl_OperationNetworkPaths Where TreeCode = " + mTCode;
                            int mPathCount = Conversions.ToInteger(cmDeletePLanning.ExecuteScalar().ToString());
                            if (mPathCount > 0)
                            {
                                daEditSubbatchPlanningInfo.SelectCommand.CommandText = "Select * From Tbl_OperationNetworkPaths Where TreeCode = " + mTCode;
                                daEditSubbatchPlanningInfo.Fill(dtPaths);
                                for (int P = 1, loopTo1 = mPathCount; P <= loopTo1; P++)
                                {
                                    dtPaths.DefaultView.RowFilter = "PathCode = " + P.ToString();
                                    dtPaths.DefaultView.Sort = "ItemPriority Desc";
                                    foreach (DataRowView drv in dtPaths.DefaultView)
                                    {
                                        cmDeletePLanning.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select Count(*) From Tbl_RealProduction Where TreeCode = " + mTCode + " And OperationCode = '", drv["OperationCode"]), "' And SubbatchCode = '"), mSBCode), "'"));
                                        if (Conversions.ToInteger(cmDeletePLanning.ExecuteScalar().ToString()) == 0)
                                        {
                                            cmDeletePLanning.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Delete From Tbl_Planning Where TreeCode = " + mTCode + " And OperationCode = '", drv["OperationCode"]), "' And SubbatchCode = '"), mSBCode), "'"));
                                            cmDeletePLanning.ExecuteNonQuery();
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }

                                CreateDeletePlanningCommand(ref daEditSubbatchPlanningInfo, trnDeletePlanning);
                                drPLanningSubbatch[0].BeginEdit();
                                drPLanningSubbatch[0]["PlanningStartDate"] = 0;
                                drPLanningSubbatch[0]["PlanningStartHour"] = 0;
                                drPLanningSubbatch[0]["PlanningEndDate"] = 0;
                                drPLanningSubbatch[0]["PlanningEndHour"] = 0;
                                drPLanningSubbatch[0].EndEdit();
                                daEditSubbatchPlanningInfo.Update(dsProductionPlanning, "Tbl_PlanningSubbatchs");
                                if (drPLanningSubbatch[0]["SubbatchNo"].ToString().Equals("1"))
                                {
                                    cmDeletePLanning.CommandText = "Update Tbl_ProductionBatchs Set PlaningStartDate = '0' Where BatchCode = '" + drPLanningSubbatch[0]["BatchCode"].ToString() + "'";
                                    cmDeletePLanning.ExecuteNonQuery();
                                }

                                dsProductionPlanning.AcceptChanges();
                                DeleteMessages += Constants.vbCrLf + "حذف شد " + mSBCode + " :مشخصات برنامه ریزی ساب بچ";
                            }
                            else
                            {
                                DeleteMessages += Constants.vbCrLf + "یافت نشد " + mSBCode + " :مسیرهای شبکه عملیات برای ساب بچ";
                            }

                            trnDeletePlanning.Commit();
                        }
                        catch (Exception objEx)
                        {
                            drPLanningSubbatch[0].CancelEdit();
                            dsProductionPlanning.RejectChanges();
                            trnDeletePlanning.Rollback();
                            Logger.SaveError(Name + ".DeleteSubbatchsPlanning", objEx.Message);
                            DeleteMessages += Constants.vbCrLf + "با مشکل مواجه شد " + mSBCode + " :حذف مشخصات برنامه ریزی ساب بچ" + Constants.vbCrLf;
                        }
                        finally
                        {
                            if (cn.State == ConnectionState.Open)
                                cn.Close();
                        }

                    NextSubbatch:
                        ;
                    }
                }
            }
        }

        private bool DeleteSubbatchPlanningRows(string mSBCode, string mTC, SqlTransaction mTrn)
        {
            var cmDeletePLanning = new SqlCommand("Select IsNull(Max(PathCode),0) From Tbl_OperationNetworkPaths Where TreeCode = " + mTC, mTrn.Connection);
            var dtPaths = new DataTable();
            cmDeletePLanning.Transaction = mTrn;
            int mPathCount = Conversions.ToInteger(cmDeletePLanning.ExecuteScalar().ToString());
            if (mPathCount > 0)
            {
                {
                    var withBlock = new SqlDataAdapter("Select * From Tbl_OperationNetworkPaths Where TreeCode = " + mTC, mTrn.Connection);
                    withBlock.SelectCommand.Transaction = mTrn;
                    withBlock.Fill(dtPaths);
                }

                for (int P = 1, loopTo = mPathCount; P <= loopTo; P++)
                {
                    dtPaths.DefaultView.RowFilter = "PathCode = " + P.ToString();
                    dtPaths.DefaultView.Sort = "ItemPriority Desc";
                    foreach (DataRowView drv in dtPaths.DefaultView)
                    {
                        cmDeletePLanning.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select Count(*) From Tbl_RealProduction Where TreeCode = " + mTC + " And OperationCode = '", drv["OperationCode"]), "' And SubbatchCode = '"), mSBCode), "'"));
                        if (Conversions.ToInteger(cmDeletePLanning.ExecuteScalar().ToString()) == 0)
                        {
                            cmDeletePLanning.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Delete From Tbl_Planning Where TreeCode = " + mTC + " And OperationCode = '", drv["OperationCode"]), "' And SubbatchCode = '"), mSBCode), "'"));
                            cmDeletePLanning.ExecuteNonQuery();
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}