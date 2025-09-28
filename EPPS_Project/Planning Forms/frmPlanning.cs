using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using ProductionPlanning.Model;

using static ProductionPlanning.MyEnums;
using static ProductionPlanning.Module1;
using ProductionPlanning.Utils;
using System.Configuration;

namespace ProductionPlanning
{
    public partial class frmPlanning
    {
        public frmPlanning()
        {
            InitializeComponent();
            _cmdPlanning.Name = "cmdPlanning";
            _cmdExit.Name = "cmdExit";
            _dgList.Name = "dgList";
        }

        public enum EnumStdResult
        {
            GoTo_PlanningDone = 101,
            Exit_For = 102,
            Continue_Function = 103,
            GoTo_RetryPlanning = 104,
            GoTo_EndOfPlanning = 105
        }



        private enum DateCalcType_enum
        {
            DCT_ADD = 1,
            DCT_SUB = 2
        }

        private enum ExecutionStatus_enum
        {
            ES_SINGLE,
            ES_PARALLEL
        }


        private SqlDataAdapter daPlanning = new SqlDataAdapter();
        private SqlDataAdapter daBatch = new SqlDataAdapter();
        private SqlDataAdapter daSubbatchs = new SqlDataAdapter();
        private long PreviousPlanningCode = -1;
        private int LastPlanningCode = 0;
        private DataView dvPlanningList;
        private DataView dvNetworkPath = null;
        private string mTreeCode = Constants.vbNullString;
        private string mSubbatchCode = Constants.vbNullString;
        //private const int ContractorStartTime = 0;
        //private const int ContractorEndTime = 24;
        private int I;

        private MyDB Db;
        private SqlTransaction trnPlanning;
        private DataSet Myds_Calender;
        private PlanTool planTool;
        private bool LogPlanningProcedure = false;
        private string IsParticularDay_CalendarCode = "";
        private string IsParticularDay_ShamsiDate = "";
        private int IsParticularDay_Status = 0;
        private void frmPlanning_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdPlanning, 7);
            Module1.SetButtonsImage(cmdExit, 5);
            dgList.Sorted += Module1.DataGridViews_Sorted_EventHandler;
            FormLoad();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int PreviousSubbatchNo;
            string BatchCode;
            if (dgList.CurrentRow is object)
            {
                if (Conversions.ToBoolean(!Operators.ConditionalCompareObjectEqual(dgList.CurrentRow.Cells["SubbatchNo"].Value, 1, false)))
                {
                    PreviousSubbatchNo = Conversions.ToInteger(Operators.SubtractObject(dgList.CurrentRow.Cells["SubbatchNo"].Value, 1));
                    BatchCode = Conversions.ToString(dgList.CurrentRow.Cells["BatchCode"].Value);
                    if (dgList.SelectedRows.Count > 1)
                    {
                        // چک کردن اینکه آیا در بین رکوردهای انتخابی وجود دارد یا نه
                        var loopTo = dgList.SelectedRows.Count - 1;
                        for (I = 0; I <= loopTo; I++)
                        {
                            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgList.SelectedRows[I].Cells["BatchCode"].Value, BatchCode, false)) && Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgList.SelectedRows[I].Cells["SubbatchNo"].Value, PreviousSubbatchNo, false)))
                            {
                                return;
                            }
                        }
                        // چک کردن اینکه آیا در برنامه ریزی یا تولید شده است یا نه
                        var loopTo1 = dgList.Rows.Count - 1;
                        for (I = 0; I <= loopTo1; I++)
                        {
                            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgList.Rows[I].Cells["BatchCode"].Value, BatchCode, false)) && Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgList.Rows[I].Cells["SubbatchNo"].Value, PreviousSubbatchNo, false)))
                            {
                                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgList.Rows[I].Cells["SubbatchFirstDeliveryDate"].Value, 0, false)))
                                {
                                    MessageBox.Show("ساب بچ قبلی انتخاب نشده است", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                                    dgList.Rows[I + 1].Selected = false;
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        // چک کردن اینکه آیا در برنامه ریزی یا تولید شده است یا نه
                        var loopTo2 = dgList.Rows.Count - 1;
                        for (I = 0; I <= loopTo2; I++)
                        {
                            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgList.Rows[I].Cells["BatchCode"].Value, BatchCode, false)) && Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgList.Rows[I].Cells["SubbatchNo"].Value, PreviousSubbatchNo, false)))
                            {
                                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgList.Rows[I].Cells["SubbatchFirstDeliveryDate"].Value, 0, false)))
                                {
                                    MessageBox.Show("ساب بچ قبلی انتخاب نشده است", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                                    dgList.Rows[I + 1].Selected = false;
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void dgList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dgList.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
            }
        }

        private void cmdPlanning_Click(object sender, EventArgs e)
        {
            string mDayDate = Module1.mServerShamsiDate;

            int SubbatchStartDate;
            string NavigatedPaths;
            DataView dvPlanning;
            long PathCounter;
            int SubbatchNo;
            bool IsEnforceDeleteAlarms = false;
            string PathCode;
            string DoneMsg = "";
            string PSD;
            int I;
            string mProductionCallDate;
            EnumStdResult Result;

            Update_FormControls();
            Delete_OldPlanningItems();
            Fill_My_ds_Calendar();

            // Prepare Stuff For Planning:-
            dvPlanning = Db.DataSet.Tables["Tbl_Planning"].DefaultView;
            if (cnProductionPlanning.State == ConnectionState.Closed)
                cnProductionPlanning.Open();

            dvNetworkPath = Db.DataSet.Tables["Tbl_OperationNetworkPaths"].DefaultView;
            trnPlanning = null;
            LastPlanningCode = zzGetLastPlanningCode();
            var loopTo = dvPlanningList.Count - 1;
            for (I = 0; I <= loopTo; I++)
            {
                RetryPlanning:
                NavigatedPaths = "";
                SubbatchStartDate = 0;
                PSD = "";
                mProductionCallDate = GetBatchProductionCallDate(dvPlanningList[I]["BatchCode"]);
                trnPlanning = cnProductionPlanning.BeginTransaction();
                try
                {
                    SetCommandsTransaction(trnPlanning);
                    mTreeCode = dvPlanningList[I]["ProductTreeCode"].ToString();
                    mSubbatchCode = dvPlanningList[I]["SubbatchCode"].ToString();
                    UpdatePlannigProgressControls(mSubbatchCode);

                    // فیلتر کردن جدول مسیرهای شبکۀ فعالیت، با مسیرهای درخت جاری
                    dvNetworkPath.RowFilter = "TreeCode=" + mTreeCode;
                    // مرتب کردن نزولی رکوردهای، مسیرهای درخت جاری بر اساس زمان اجرای مسیرها
                    dvNetworkPath.Sort = "PathTime Desc";
                    if (dvNetworkPath.Count <= 0)
                    {
                        DoneMsg += (DoneMsg.Length > 0 ? Constants.vbCrLf : $"{mSubbatchCode} - فعالیتی در درخت محصول مربوط به ساب بچ یافت نشد");
                        continue;
                        //goto PlanningDone;
                    }
                    // تاریخ اولین محموله تعیین نشده است
                    if (dvPlanningList[I]["SubbatchFirstDeliveryDate"].ToString() == "" || dvPlanningList[I]["SubbatchFirstDeliveryDate"].ToString() =="0")
                    {
                        SubbatchNo = Conversions.ToInteger(Db.DataSet.Tables["Tbl_ProductionSubbatchs"].Select("SubbatchCode='" + mSubbatchCode + "'")[0]["SubbatchNo"]);
                        if (SubbatchNo > 1)
                        {
                            var cm = new SqlCommand($"Select PlanningStartDate From Tbl_ProductionSubbatchs Where BatchCode ='{dvPlanningList[I]["BatchCode"]}' And SubbatchNo={SubbatchNo - 1}", Module1.cnProductionPlanning, trnPlanning);
                            var result = cm.ExecuteScalar();
                            string PreSubBatchStartDate = result == DBNull.Value ? "" : result.ToString();
                            if (string.IsNullOrEmpty(PreSubBatchStartDate))
                            {
                                Db.DataSet.RejectChanges();
                                trnPlanning.Rollback();
                                trnPlanning = null;
                                DoneMsg = Conversions.ToString(DoneMsg + Operators.ConcatenateObject(Interaction.IIf(string.IsNullOrEmpty(DoneMsg), "", Constants.vbCrLf), string.Format("برنامه ریزی ساب بچ {0} ثبت نشد -- ساب بچ قبلی برنامه ریزی نشده است", mSubbatchCode)));
                                goto PlanningDone;
                            }
                        }
                        // تعیین تاریخ شروع اولین عملیات از ساب بچ
                        if (Operators.CompareString(mProductionCallDate, mDayDate, false) >= 0)
                        {
                            SubbatchStartDate = int.Parse(mProductionCallDate);
                        }
                        else
                        {
                            SubbatchStartDate = int.Parse(FarsiDateFunctions.AddToDate(mDayDate, "00000001"));
                        }

                        var loopTo1 = (long)(dvNetworkPath.Count - 1);
                        for (PathCounter = 0L; PathCounter <= loopTo1; PathCounter++)
                        {
                            if (PathCounter > dvNetworkPath.Count - 1)
                            {
                                break;
                            }

                            // فیلتر کردن جدول مسیرهای شبکۀ فعالیت با کد مسیر جاری
                            PathCode = Conversions.ToString(dvNetworkPath[(int)PathCounter]["PathCode"]);
                            dvNetworkPath.RowFilter = "TreeCode=" + mTreeCode + " And PathCode=" + PathCode;
                            dvNetworkPath.Sort = "ItemPriority";
                            NavigatedPaths = NavigatedPaths + (NavigatedPaths.Length > 0 ? "," : "") + PathCode;

                            for (int j = 0; j <= (dvNetworkPath.Count - 1); j++)
                            {
                                // برنامه ریزی عملیات جاری
                                //var PlanOp = new PlanOperation(mSubbatchCode, mTreeCode, dvNetworkPath[j]["OperationCode"].ToString());
                                var planItem = planTool.CreatePlanOpertion(mSubbatchCode, mTreeCode, dvNetworkPath[j]["OperationCode"].ToString());
                                planItem.SubbatchStartDate = SubbatchStartDate;
                                Forward_OperationPlan(planItem);
                            }

                            // فیلتر کردن جدول مسیرهای شبکۀ فعالیت با درخت محصول جاری و مسیرهای طی نشده
                            dvNetworkPath.RowFilter = "TreeCode=" + mTreeCode + " And Not PathCode IN(" + NavigatedPaths + ")";
                            dvNetworkPath.Sort = "PathTime Desc";
                            PathCounter = -1;
                        }
                    }
                    else // تاریخ اولین محموله تعیین شده است
                    {
                        var loopTo3 = (long)(dvNetworkPath.Count - 1);
                        for (PathCounter = 0L; PathCounter <= loopTo3; PathCounter++)
                        {
                            if (PathCounter > dvNetworkPath.Count - 1)
                            {
                                break;
                            }

                            // فیلتر کردن جدول مسیرهای شبکۀ فعالیت با کد مسیر جاری
                            PathCode = Conversions.ToString(dvNetworkPath[(int)PathCounter]["PathCode"]);
                            dvNetworkPath.RowFilter = "TreeCode=" + mTreeCode + " And PathCode=" + PathCode;
                            dvNetworkPath.Sort = "ItemPriority Desc";
                            if (string.IsNullOrEmpty(NavigatedPaths))
                            {
                                NavigatedPaths = PathCode;
                            }
                            else
                            {
                                NavigatedPaths += "," + PathCode;
                            }

                            for (int PathItemCounter = 0, loopTo4 = dvNetworkPath.Count - 1; PathItemCounter <= loopTo4; PathItemCounter++)
                                // برنامه ریزی عملیات جاری
                                OpPlan_BackwardMethod(PathItemCounter, Conversions.ToString(dvNetworkPath[PathItemCounter]["OperationCode"]), ref LastPlanningCode, ref trnPlanning, Conversions.ToString(dvPlanningList[I]["SubbatchFirstDeliveryDate"]), Conversions.ToInteger(dvPlanningList[I]["TransferMinimumQuantity"]));

                            // فیلتر کردن جدول مسیرهای شبکۀ فعالیت با درخت محصول جاری و مسیرهای طی نشده
                            dvNetworkPath.RowFilter = "TreeCode=" + mTreeCode + " And Not PathCode IN(" + NavigatedPaths + ")";
                            dvNetworkPath.Sort = "PathTime Desc";
                            PathCounter = -1;
                        }
                    }

                    // کنترل صحت برنامه ریزی ساب بچ
                    Result = CheckPlaningItemsAndThenSave(ref trnPlanning, ref DoneMsg);
                    switch (Result)
                    {
                        case EnumStdResult.GoTo_PlanningDone:
                        {
                            goto PlanningDone;
                        }

                        case EnumStdResult.GoTo_EndOfPlanning:
                        {
                            goto EndOfPlanning;
                        }
                    }

                    dvPlanning.RowFilter = "SubbatchCode='" + mSubbatchCode + "' And ProductionCode = 0";
                    dvPlanning.Sort = "PlanningStartDate,PlanningStartHour";
                    if (dvPlanning.Count > 0)
                    {
                        dvPlanningList[I].BeginEdit();
                        dvPlanningList[I]["PlanningStartDate"] = dvPlanning[0]["PlanningStartDate"];
                        dvPlanningList[I]["PlanningStartHour"] = dvPlanning[0]["PlanningStartHour"];
                        PSD = Conversions.ToString(dvPlanning[0]["PlanningStartDate"]);
                        dvPlanning.Sort = "PlanningEndDate Desc,PlanningEndHour Desc";
                        dvPlanningList[I]["PlanningEndDate"] = dvPlanning[0]["PlanningEndDate"];
                        dvPlanningList[I]["PlanningEndHour"] = dvPlanning[0]["PlanningEndHour"];
                        dvPlanningList[I].EndEdit();
                    }
                    else
                    {
                        PSD = "0";
                    }

                    // کنترل اینکه تاریخ شروع برنامه ریزی، قبل از آخرین تاریخ دستور شروع ساخت سفارشات بچ نباشد
                    Result = Check_PlanningStartDate_With_ProductionCallDate(trnPlanning, ref DoneMsg, PSD, mProductionCallDate, mDayDate);
                    switch (Result)
                    {
                        case EnumStdResult.GoTo_RetryPlanning:
                        {
                            goto RetryPlanning;
                            // break;
                        }

                        case EnumStdResult.GoTo_PlanningDone:
                        {
                            goto PlanningDone;
                            //break;
                        }

                        case EnumStdResult.Exit_For:
                        {
                            break;
                        }
                    }

                    // ثبت شروع برنامه ریزی بچ
                    Update_Batch_PlanningStartDate(trnPlanning, Conversions.ToString(dvPlanningList[I]["BatchCode"]));
                    Update_SubBatch_PlanningStartDate(trnPlanning, mSubbatchCode);
                    SaveChanges();
                    trnPlanning.Commit();
                    trnPlanning = null;
                    DoneMsg = Conversions.ToString(DoneMsg + Operators.ConcatenateObject(Interaction.IIf(string.IsNullOrEmpty(DoneMsg), "", Constants.vbCrLf), string.Format("برنامه ریزی ساب بچ: {0} با موفقیت انجام شد", mSubbatchCode)));
                    PlanningDone:
                    ;
                    if (trnPlanning != null)
                    {
                        trnPlanning.Commit();
                    }

                    //_frmPlanningConfirm.Dispose();
                    if (!IsEnforceDeleteAlarms)
                    {
                        IsEnforceDeleteAlarms = Module1.Delete_Subbatchs_PlanningAlarms();
                    }
                    // Close();
                }
                catch (Exception objEx)
                {
                    Logger.LogException("cmdPlanning_Click:" + FunctionLabel.Text, objEx);
                    dvPlanningList[I].CancelEdit();
                    if (trnPlanning != null)
                    {

                        trnPlanning.Rollback();
                        trnPlanning = null;
                    }

                    DoneMsg = Conversions.ToString(DoneMsg + Operators.ConcatenateObject(Interaction.IIf(string.IsNullOrEmpty(DoneMsg), "", Constants.vbCrLf), string.Format("برنامه ریزی ساب بچ {0} انجام نشد -- اشکال در سیستم", mSubbatchCode)));
                    MessageBox.Show(string.Format("برنامه ریزی ساب بچ {0} انجام نشد -- اشکال در سیستم", mSubbatchCode), Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                }

                NavigatedPaths = Constants.vbNullString;
            }

            EndOfPlanning:
            lblWaiting.Text = "برنامه ریزی ساب بچ ها انجام شد";
            Application.DoEvents();
            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                Module1.cnProductionPlanning.Close();
            dvPlanning = null;
            dvPlanningList.RowFilter = "RealStartDate Is Null  Or RealStartDate=0";
            dvPlanningList.Sort = "BatchPriority,SubbatchNo";
            if (!string.IsNullOrEmpty(DoneMsg))
            {
                MessageBox.Show(DoneMsg, Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
            }

            Cursor = Cursors.Default;
            Close();
        }

        private void SetGridColumns(object DataSource, string Table = Constants.vbNullString)
        {
            {
                var withBlock = dgList;
                withBlock.DataSource = DataSource;
                withBlock.Columns[0].Visible = false;
                withBlock.Columns[1].HeaderText = "اولویت تولید";
                withBlock.Columns[2].Visible = false;
                withBlock.Columns[3].Visible = false;
                withBlock.Columns[4].HeaderText = "کد ساب بچ";
                withBlock.Columns[5].HeaderText = "تعداد";
                withBlock.Columns[6].HeaderText = "موعد تحویل اولین محموله";
                withBlock.Columns[7].HeaderText = "تاریخ شروع(برنامه ریزی)";
                withBlock.Columns[8].Visible = false;
                withBlock.Columns[9].Visible = false;
                withBlock.Columns[10].Visible = false;
                withBlock.Columns[11].Visible = false;
                withBlock.Columns[12].Visible = false;
                withBlock.Columns[13].Visible = false;
                withBlock.Columns[14].Visible = false;
                withBlock.Columns[15].Visible = false;
                withBlock.Columns[16].Visible = false;
                withBlock.Columns[17].Visible = false;
                withBlock.Columns[18].Visible = false;
                withBlock.Columns[19].Visible = false;
                var loopTo = dgList.Columns.Count - 1;
                for (I = 0; I <= loopTo; I++)
                    withBlock.Columns[I].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
     
        private string GetSelectedSubbatchsCode()
        {
            string SelectedSubbatchsCode = Constants.vbNullString;
            var loopTo = dgList.SelectedRows.Count - 1;
            for (I = 0; I <= loopTo; I++)
            {
                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgList.SelectedRows[I].Cells["SubbatchFirstDeliveryDate"].Value, 0, false)))
                {
                    MessageBox.Show(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("مشخص نمی باشد ", dgList.SelectedRows[I].Cells["SubbatchCode"].Value), " تاریخ اولین محمولۀ ارسالی برای ساب بچ")), Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                    return Constants.vbNullString;
                }

                if (string.IsNullOrEmpty(SelectedSubbatchsCode))
                {
                    SelectedSubbatchsCode = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("'", dgList.SelectedRows[I].Cells["SubbatchCode"].Value), "'"));
                }
                else
                {
                    SelectedSubbatchsCode = Conversions.ToString(SelectedSubbatchsCode + Operators.ConcatenateObject(Operators.ConcatenateObject(",'", dgList.SelectedRows[I].Cells["SubbatchCode"].Value), "'"));
                }
            }

            return SelectedSubbatchsCode;
        }

        private void DeleteOldPlanningRows(string SubbatchCode)
        {
            if (this.LogPlanningProcedure) Logger.LogInfo($"DeleteOldPlanningRows(SubbatchCode:{SubbatchCode})");

            int I;
            Db.DataSet.Tables["Tbl_Planning"].DefaultView.RowFilter = "SubbatchCode='" + SubbatchCode + "' And ProductionCode = 0";
            for (I = Db.DataSet.Tables["Tbl_Planning"].DefaultView.Count - 1; I >= 0; I -= 1)
                Db.DataSet.Tables["Tbl_Planning"].DefaultView[I].Delete();
        }

        private void OpPlan_BackwardMethod(int OpLoc, string OperationCode, ref int mCode, ref SqlTransaction mTrn, string FirstDeliveryDate = Constants.vbNullString, int MinQty = 0)
        {
            FunctionLabel.Tag = $"Sub Batch = {lblCurrentSubbatchCode.Text} , Operation = {OperationCode} : ";
            FunctionLabel.Text = FunctionLabel.Tag.ToString() + " Planning backward based on Delivery Time";
            lblCurrentOperationCode.Text = OperationCode;
            Application.DoEvents();

            if (this.LogPlanningProcedure) Logger.LogInfo($"OpPlan_BackwardMethod(OpLoc:{OpLoc},OperationCode:{OperationCode})");

            // در صورتیکه برای فعالیت جاری، قبلا برنامه ریزی نشده باشد
            if (Db.DataSet.Tables["Tbl_Planning"].Select("TreeCode=" + mTreeCode + " And SubbatchCode='" + mSubbatchCode + "' And OperationCode='" + OperationCode + "'").Length == 0)
            {
                var mProductionQtys = new long[] { GetOperationPlanningQuantity(OperationCode) };
                if (mProductionQtys[0] == -1)
                {
                    throw new Exception("تعداد مورد نیاز برای برنامه ریزی عملیات {" + OperationCode + "} در ساب بچ {" + mSubbatchCode + "{ یافت نشد");
                }

                string mMatchedOperations = Module1.GetMatchedOperations(OperationCode, mTreeCode);
                var MachineCodes = new string[] { "-1" };
                var PlanningStartDates = new string[] { "0" };
                var PlanningStartHours = new double[] { 0d };
                var PlanningEndDates = new string[] { "0" };
                var PlanningEndHours = new double[] { 0d };
                var SetupStartDates = new string[] { "0" };
                var SetupStartHours = new double[] { 0d };
                var SetupEndDates = new string[] { "0" };
                var SetupEndHours = new double[] { 0d };
                var SetupDurations = new double[] { 0d };
                var OperationStartDates = new string[] { "0" };
                var OperationStartHours = new double[] { 0d };
                var OperationEndDates = new string[] { "0" };
                var OperationEndHours = new double[] { 0d };
                var OperationDurations = new double[] { 0d };
                var drOPCs = Db.DataSet.Tables["Tbl_ProductOPCs"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + OperationCode + "'");
                var mCopyPlanningCodes = new long[] { 0L };
                var drAfterOperations = Array.Empty<DataRow>();

                // بدست آوردن کد ماشین اولویت اول(در صورتیکه عملیات ماشینی باشد) برای عملیات
                MachineCodes[0] = GetMachineCode((EnumExecutionMethod)short.Parse(drOPCs[0]["ExecutionMethod"].ToString()), OperationCode);
                if (!string.IsNullOrEmpty(mMatchedOperations))
                {
                    foreach (string s in mMatchedOperations.Split(',')) // کنترل اینکه حداقل یکی از عملیات های جفت دارای برنامه ریزی باشد
                    {
                        var mdrExistPlanning = Db.DataSet.Tables["Tbl_Planning"].Select("TreeCode = " + mTreeCode + " And SubbatchCode = '" + mSubbatchCode + "' And OperationCode = " + s);
                        if (mdrExistPlanning.Length > 0)
                        {
                            MachineCodes = new string[mdrExistPlanning.Length];
                            SetupStartDates = new string[mdrExistPlanning.Length];
                            SetupStartHours = new double[mdrExistPlanning.Length];
                            OperationStartDates = new string[mdrExistPlanning.Length];
                            OperationStartHours = new double[mdrExistPlanning.Length];
                            PlanningStartDates = new string[mdrExistPlanning.Length];
                            PlanningStartHours = new double[mdrExistPlanning.Length];
                            for (int I = 0, loopTo = mdrExistPlanning.Length - 1; I <= loopTo; I++)
                            {
                                MachineCodes[I] = Conversions.ToString(mdrExistPlanning[I]["MachineCode"]);
                                SetupStartDates[I] = mdrExistPlanning[I]["SetupStartDate"].ToString();
                                SetupStartHours[I] = Conversions.ToDouble(mdrExistPlanning[I]["SetupStartHour"]);
                                SetupEndDates[I] = mdrExistPlanning[I]["SetupEndDate"].ToString();
                                SetupEndHours[I] = Conversions.ToDouble(mdrExistPlanning[I]["SetupEndHour"]);
                                SetupDurations[I] = Conversions.ToDouble(mdrExistPlanning[I]["SetupDuration"]);
                                OperationStartDates[I] = mdrExistPlanning[I]["OperationStartDate"].ToString();
                                OperationStartHours[I] = Conversions.ToDouble(mdrExistPlanning[I]["OperationStartHour"]);
                                OperationEndDates[I] = mdrExistPlanning[I]["OperationEndDate"].ToString();
                                OperationEndHours[I] = Conversions.ToDouble(mdrExistPlanning[I]["OperationEndHour"]);
                                OperationDurations[I] = Conversions.ToDouble(mdrExistPlanning[I]["OperationDuration"]);
                                PlanningStartDates[I] = mdrExistPlanning[I]["PlanningStartDate"].ToString();
                                PlanningStartHours[I] = Conversions.ToDouble(mdrExistPlanning[I]["PlanningStartHour"]);
                                PlanningEndDates[I] = mdrExistPlanning[I]["PlanningEndDate"].ToString();
                                PlanningEndHours[I] = Conversions.ToDouble(mdrExistPlanning[I]["PlanningEndHour"]);
                            }

                            goto InsertNewRowsPoint2;
                        }
                    }

                    drAfterOperations = Db.DataSet.Tables["Tbl_PreOperations"].Select("TreeCode=" + mTreeCode + " And PreOperationCode='" + OperationCode + "'");
                    if (drAfterOperations.Length > 0) // در صورتیکه پسنیاز داشته باشد
                    {
                        // در صورتیکه عملیات پسنیاز برنامه ریزی نشده باشد
                        if (Db.DataSet.Tables["Tbl_Planning"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("TreeCode=" + mTreeCode + " And SubbatchCode='" + mSubbatchCode + "' And OperationCode='", drAfterOperations[0]["CurrentOperationCode"]), "'"))).Length == 0)
                        {
                            // برنامه ریزی عملیات پیشنیاز
                            OpPlan_BackwardMethod(1, Conversions.ToString(drAfterOperations[0]["CurrentOperationCode"]), ref mCode, ref mTrn, FirstDeliveryDate, MinQty);
                        }
                    }

                    drAfterOperations = Db.DataSet.Tables["Tbl_PreOperations"].Select("TreeCode=" + mTreeCode + " And PreOperationCode IN (" + mMatchedOperations + ")");
                }
                else
                {
                    drAfterOperations = Db.DataSet.Tables["Tbl_PreOperations"].Select("TreeCode=" + mTreeCode + " And PreOperationCode='" + OperationCode + "'");
                }

                if (drAfterOperations.Length > 0) // در صورتیکه پسنیاز داشته باشد
                {
                    foreach (DataRow r in drAfterOperations)
                    {
                        // در صورتیکه عملیات پسنیاز برنامه ریزی نشده باشد
                        if (Db.DataSet.Tables["Tbl_Planning"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("TreeCode=" + mTreeCode + " And SubbatchCode='" + mSubbatchCode + "' And OperationCode='", r["CurrentOperationCode"]), "'"))).Length == 0)
                        {
                            // برنامه ریزی عملیات پیشنیاز
                            OpPlan_BackwardMethod(1, Conversions.ToString(r["CurrentOperationCode"]), ref mCode, ref mTrn, FirstDeliveryDate, MinQty);
                        }
                    }
                }

                if (mProductionQtys[0] > 0L)
                {
                    Backward_GetFirstExecutionTime(OperationCode, OpLoc == 0, drOPCs[0], FirstDeliveryDate, MinQty,
                                                   ref MachineCodes,
                                                   ref PlanningStartDates, ref PlanningStartHours, ref SetupStartDates, ref SetupStartHours, ref OperationStartDates, ref OperationStartHours, ref PlanningEndDates, ref PlanningEndHours, ref SetupEndDates, ref SetupEndHours, ref OperationEndDates, ref OperationEndHours, ref mProductionQtys, ref SetupDurations, ref OperationDurations);
                }
                else // تعداد تولید صفر می باشد
                {
                    EnumExecutionMethod execMethod = CommonTool.GetExecutionMethod(drOPCs[0]["ExecutionMethod"].ToString());
                    string CalendarCode = GetCalendarCode(execMethod, OperationCode, MachineCodes[0]);
                    OperationEndDates[0] = FirstDeliveryDate.ToString();
                    OperationEndHours[0] = Conversions.ToDouble(GetCalendarEnd(CalendarCode));
                    OperationStartDates[0] = OperationEndDates[0];
                    OperationStartHours[0] = OperationEndHours[0];
                    if (MachineCodes[0] != "-1")
                    {
                        SetupStartDates[0] = OperationStartDates[0];
                        SetupStartHours[0] = OperationStartHours[0];
                        PlanningStartDates[0] = SetupStartDates[0];
                        PlanningStartHours[0] = SetupStartHours[0];
                    }
                    else
                    {
                        SetupStartDates[0] = "0";
                        SetupStartHours[0] = 0d;
                        PlanningStartDates[0] = OperationStartDates[0];
                        PlanningStartHours[0] = OperationStartHours[0];
                    }

                    PlanningEndDates[0] = OperationEndDates[0];
                    PlanningEndHours[0] = OperationEndHours[0];
                    SetupDurations[0] = 0d;
                    OperationDurations[0] = 0d;
                }

                InsertNewRowsPoint2:
                ;
                lblCurrentOperationCode.Text = OperationCode;
                Application.DoEvents();


                // ثبت مشخصات برنامه ریزی انجام شده برای عملیات در بانک اطلاعاتی
                InsertPlanningNewRows(ref mCode, OperationCode, MachineCodes, mProductionQtys, PlanningStartDates, PlanningStartHours, SetupStartDates, SetupStartHours, OperationStartDates, OperationStartHours, PlanningEndDates, PlanningEndHours, SetupEndDates, SetupEndHours, OperationEndDates, OperationEndHours, SetupDurations, OperationDurations, mCopyPlanningCodes, ref mTrn);
            }
        }

        private void Backward_GetFirstExecutionTime(string OperationCode,
                                                    bool IsLastOperation,
                                                    DataRow drOPC,
                                                    string FirstDeliveryDate, long FirstDeliveryQty,
                                                    ref string[] MachineCodes,
                                                    ref string[] PlanningStartDates, ref double[] PlanningStartHours,
                                                    ref string[] SetupStartDates, ref double[] SetupStartHours,
                                                    ref string[] OperationStartDates, ref double[] OperationStartHours,
                                                    ref string[] PlanningEndDates, ref double[] PlanningEndHours,
                                                    ref string[] SetupEndDates, ref double[] SetupEndHours,
                                                    ref string[] OperationEndDates, ref double[] OperationEndHours,
                                                    ref long[] mQtys,
                                                    ref double[] SetupDurations,
                                                    ref double[] OperationDurations)
        {
            FunctionLabel.Text = Conversions.ToString(Operators.ConcatenateObject(FunctionLabel.Tag, " GetBackwardFirstExecutionTime"));
            Application.DoEvents();

            if (this.LogPlanningProcedure) Logger.LogInfo($"Backward_GetFirstExecutionTime(OperationCode:{OperationCode},IsLastOperation:{IsLastOperation},FirstDeliveryDate:{FirstDeliveryDate},FirstDeliveryQty:{FirstDeliveryQty})");

            string mMatchedOperations = Module1.GetMatchedOperations(OperationCode, mTreeCode);
            double Tx = 0d;
            //double SetupTime = 0d;
            double OneExecTime = 0d;
            //DataRow[] drExecMachine = null;
            //DataRow[] OtherMachines = null;
            EnumExecutionMethod execMethod = CommonTool.GetExecutionMethod(drOPC["ExecutionMethod"].ToString());
            string mDeliveryDate = FirstDeliveryDate;
            string CalendarCode = GetCalendarCode(execMethod, OperationCode, MachineCodes[0]);
            var mParallelMachines = Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + OperationCode + "' And IsNull(IsParallelMachine,0) = 1", "MachinePriority");


            // Try


            if (IsLastOperation) // در صورتیکه آخرین عملیات مسیر باشد
            {
                if (Conversions.ToBoolean(DBNull.Value.Equals(drOPC["IsParallelOperation"]) || !(bool)drOPC["IsParallelOperation"] || (mParallelMachines.Length == 1)))
                {
                    // در صورتیکه عملیات موازی کاری نداشته باشد یا
                    // تعداد ماشین هایی که در عملیات موازی کاری عملیات شرکت دارند یک ماشین می باشد
                    int Counter = 0;
                    while (Counter < 100 && CheckIsHoliday(CalendarCode, mDeliveryDate))
                    {
                        Counter += 1;
                        mDeliveryDate = FarsiDateFunctions.AddToDate(FirstDeliveryDate, "00000001");
                    }
                    EnumExecutionMethod ExecutionMethod = CommonTool.GetExecutionMethod(drOPC["ExecutionMethod"].ToString());
                    OneExecTime = GetOneOperationExecTime(OperationCode, ExecutionMethod, MachineCodes[0]);
                    // بدست آوردن زمان تولید تعداد اولین محموله ارسالی
                    Tx = FirstDeliveryQty * OneExecTime;
                    OperationEndDates[0] = mDeliveryDate.ToString();
                    OperationEndHours[0] = Conversions.ToDouble(GetCalendarEnd(CalendarCode));
                    OperationStartDates[0] = OperationEndDates[0];
                    OperationStartHours[0] = OperationEndHours[0];

                    // از تاریخ اولین محموله به اندازه زمان تولید تعداد اولین محموله به عقب برمیگردیم
                    // و زمان شروع تولید برای تعداد اولین محموله را بدست می آوریم
                    var tmp = OperationStartDates;
                    string argCurrentDate = tmp[0].ToString();
                    GetNewDate(CalendarCode, ref argCurrentDate, ref OperationStartHours[0], Tx * -1);
                    //ExecutionMethod = CommonTool.GetExecutionMethod(drOPC["ExecutionMethod"].ToString());
                    Backward_GetFirstAccessibleTime(ExecutionMethod, OperationCode, ref MachineCodes[0], ref PlanningStartDates[0], ref PlanningStartHours[0], ref SetupStartDates[0], ref SetupStartHours[0], ref OperationStartDates[0], ref OperationStartHours[0], ref PlanningEndDates[0], ref PlanningEndHours[0], ref SetupEndDates[0], ref SetupEndHours[0], ref OperationEndDates[0], ref OperationEndHours[0], ref SetupDurations[0], ref OperationDurations[0], ref mQtys[0], ExecutionStatus_enum.ES_SINGLE);
                    OperationEndDates[0] = PlanningStartDates[0];
                    OperationEndHours[0] = PlanningStartHours[0];
                    var tmp1 = OperationEndDates;
                    string argCurrentDate1 = tmp1[0].ToString();
                    GetNewDate(CalendarCode, ref argCurrentDate1, ref OperationEndHours[0], OperationDurations[0] + SetupDurations[0]);
                    PlanningEndDates[0] = OperationEndDates[0];
                    PlanningEndHours[0] = OperationEndHours[0];
                    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(drOPC["ExecutionMethod"], EnumExecutionMethod.EM_MACHINE, false)))
                    {
                        SetupEndDates[0] = SetupStartDates[0];
                        SetupEndHours[0] = SetupStartHours[0];
                        var tmp2 = SetupEndDates;
                        string argCurrentDate2 = tmp2[0].ToString();
                        GetNewDate(CalendarCode, ref argCurrentDate2, ref SetupEndHours[0], SetupDurations[0]);
                    }
                }
                else // در صورتیکه عملیات موازی کاری داشته باشد و تعداد ماشین هایی که در موازی کاری عملیات شرکت دارند حداقل دو ماشین باشد
                {
                    var mSingleMachines = Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].Select("TreeCode = " + mTreeCode + " And OperationCode = '" + OperationCode + "'", "MachinePriority");
                    var mParallelMachinesValues = new ArrayList[mParallelMachines.Length];
                    var mSingleMachinesValues = new ArrayList[mSingleMachines.Length];
                    int mEarlSingleIndex = -1;

                    // بدست آوردن زودترین زمان انجام عملیات برای حالتی که امکان موازی کاری نیز وجود داشته باشد
                    GetBackwardParallelExecutionTimes(OperationCode, drOPC, FirstDeliveryDate, Conversions.ToDouble("0.0"), FirstDeliveryQty, false, mParallelMachines, mSingleMachines, mQtys[0], ref mSingleMachinesValues, ref mParallelMachinesValues, ref mEarlSingleIndex);
                    if (mEarlSingleIndex > -1)
                    {
                        MachineCodes[0] = Conversions.ToString(mSingleMachinesValues[mEarlSingleIndex][0]);
                        PlanningStartDates[0] = mSingleMachinesValues[mEarlSingleIndex][1].ToString();
                        PlanningStartHours[0] = Conversions.ToDouble(mSingleMachinesValues[mEarlSingleIndex][2]);
                        PlanningEndDates[0] = mSingleMachinesValues[mEarlSingleIndex][3].ToString();
                        PlanningEndHours[0] = Conversions.ToDouble(mSingleMachinesValues[mEarlSingleIndex][4]);
                        SetupStartDates[0] = mSingleMachinesValues[mEarlSingleIndex][5].ToString();
                        SetupStartHours[0] = Conversions.ToDouble(mSingleMachinesValues[mEarlSingleIndex][6]);
                        SetupEndDates[0] = mSingleMachinesValues[mEarlSingleIndex][12].ToString();
                        SetupEndHours[0] = Conversions.ToDouble(mSingleMachinesValues[mEarlSingleIndex][13]);
                        OperationStartDates[0] = mSingleMachinesValues[mEarlSingleIndex][7].ToString();
                        OperationStartHours[0] = Conversions.ToDouble(mSingleMachinesValues[mEarlSingleIndex][8]);
                        OperationEndDates[0] = mSingleMachinesValues[mEarlSingleIndex][14].ToString();
                        OperationEndHours[0] = Conversions.ToDouble(mSingleMachinesValues[mEarlSingleIndex][15]);
                        mQtys[0] = Conversions.ToLong(mSingleMachinesValues[mEarlSingleIndex][9]);
                        SetupDurations[0] = Conversions.ToDouble(mSingleMachinesValues[mEarlSingleIndex][10]);
                        OperationDurations[0] = Conversions.ToDouble(mSingleMachinesValues[mEarlSingleIndex][11]);
                    }
                    else
                    {
                        MachineCodes = new string[mParallelMachinesValues.Length];
                        mQtys = new long[mParallelMachinesValues.Length];
                        PlanningStartDates = new string[mParallelMachinesValues.Length];
                        PlanningStartHours = new double[mParallelMachinesValues.Length];
                        PlanningEndDates = new string[mParallelMachinesValues.Length];
                        PlanningEndHours = new double[mParallelMachinesValues.Length];
                        SetupStartDates = new string[mParallelMachinesValues.Length];
                        SetupStartHours = new double[mParallelMachinesValues.Length];
                        SetupEndDates = new string[mParallelMachinesValues.Length];
                        SetupEndHours = new double[mParallelMachinesValues.Length];
                        OperationStartDates = new string[mParallelMachinesValues.Length];
                        OperationStartHours = new double[mParallelMachinesValues.Length];
                        OperationEndDates = new string[mParallelMachinesValues.Length];
                        OperationEndHours = new double[mParallelMachinesValues.Length];
                        SetupDurations = new double[mParallelMachinesValues.Length];
                        OperationDurations = new double[mParallelMachinesValues.Length];
                        for (int I = 0, loopTo = mParallelMachinesValues.Length - 1; I <= loopTo; I++)
                        {
                            MachineCodes[I] = Conversions.ToString(mParallelMachinesValues[I][0]);
                            PlanningStartDates[I] = mParallelMachinesValues[I][1].ToString();
                            PlanningStartHours[I] = Conversions.ToDouble(mParallelMachinesValues[I][2]);
                            PlanningEndDates[I] = mParallelMachinesValues[I][3].ToString();
                            PlanningEndHours[I] = Conversions.ToDouble(mParallelMachinesValues[I][4]);
                            SetupStartDates[I] = mParallelMachinesValues[I][5].ToString();
                            SetupStartHours[I] = Conversions.ToDouble(mParallelMachinesValues[I][6]);
                            SetupEndDates[I] = mParallelMachinesValues[I][12].ToString();
                            SetupEndHours[I] = Conversions.ToDouble(mParallelMachinesValues[I][13]);
                            OperationStartDates[I] = mParallelMachinesValues[I][7].ToString();
                            OperationStartHours[I] = Conversions.ToDouble(mParallelMachinesValues[I][8]);
                            OperationEndDates[I] = mParallelMachinesValues[I][14].ToString();
                            OperationEndHours[I] = Conversions.ToDouble(mParallelMachinesValues[I][15]);
                            mQtys[I] = Conversions.ToLong(mParallelMachinesValues[I][9]);
                            SetupDurations[I] = Conversions.ToDouble(mParallelMachinesValues[I][10]);
                            OperationDurations[I] = Conversions.ToDouble(mParallelMachinesValues[I][11]);
                        }
                    }
                }
            }
            else // در صورتیکه آخرین عملیات مسیر نباشد
            {
                var drAfterOperations = Db.DataSet.Tables["Tbl_PreOperations"].Select("TreeCode=" + mTreeCode + " And PreOperationCode='" + OperationCode + "'");
                var mMaxMatchedQuantity = mQtys;
                if (!string.IsNullOrEmpty(mMatchedOperations)) // در صورتیکه اجرای همزمان با عملیات های دیگر داشته باشد
                {
                    // بدست آوردن بیشترین تعداد تولید(مورد نیاز) در بین عملیات هایی که باید همزمان انجام گیرند
                    mMaxMatchedQuantity[0] = GetMaxMatchedQuantity("'" + OperationCode + "'," + mMatchedOperations);
                    // بدست آوردن عملیات های پسنیاز مربوط به تمامی عملیات هایی که در همزمانی شرکت دارند
                    drAfterOperations = Db.DataSet.Tables["Tbl_PreOperations"].Select("TreeCode=" + mTreeCode + " And PreOperationCode IN ('" + OperationCode + "'," + mMatchedOperations + ")");
                    foreach (DataRow drAfter in drAfterOperations)
                        GetMatchedBackwardPlanningTimeWithAfterwardOps(OperationCode, drAfter, ref MachineCodes, ref PlanningStartDates, ref PlanningStartHours, ref SetupStartDates, ref SetupStartHours, ref OperationStartDates, ref OperationStartHours, ref mMaxMatchedQuantity);
                }
                else
                {
                    Backward_GetPlanTime_BaseOn_NextOps(OperationCode, drAfterOperations[0], ref MachineCodes, ref PlanningStartDates, ref PlanningStartHours, ref SetupStartDates, ref SetupStartHours, ref OperationStartDates, ref OperationStartHours, ref mQtys);
                }

                if (DBNull.Value.Equals(drOPC["IsParallelOperation"]) || !Convert.ToBoolean(drOPC["IsParallelOperation"]) || Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + OperationCode + "' And IsNull(IsParallelMachine,0) = 1").Length == 1)
                {
                    // در صورتیکه عملیات موازی کاری نداشته باشد یا
                    // تعداد ماشین هایی که در عملیات موازی کاری عملیات شرکت دارند یک ماشین می باشد
                    EnumExecutionMethod ExecutionMethod = CommonTool.GetExecutionMethod(drOPC["ExecutionMethod"].ToString());
                    Backward_GetFirstAccessibleTime(ExecutionMethod, OperationCode, ref MachineCodes[0], ref PlanningStartDates[0], ref PlanningStartHours[0], ref SetupStartDates[0], ref SetupStartHours[0], ref OperationStartDates[0], ref OperationStartHours[0], ref PlanningEndDates[0], ref PlanningEndHours[0], ref SetupEndDates[0], ref SetupEndHours[0], ref OperationEndDates[0], ref OperationEndHours[0], ref SetupDurations[0], ref OperationDurations[0], ref mMaxMatchedQuantity[0], ExecutionStatus_enum.ES_SINGLE);
                    OperationEndDates[0] = PlanningStartDates[0];
                    OperationEndHours[0] = PlanningStartHours[0];
                    var tmp3 = OperationEndDates;
                    string argCurrentDate3 = tmp3[0].ToString();
                    GetNewDate(CalendarCode, ref argCurrentDate3, ref OperationEndHours[0], OperationDurations[0] + SetupDurations[0]);
                    PlanningEndDates[0] = OperationEndDates[0];
                    PlanningEndHours[0] = OperationEndHours[0];
                    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(drOPC["ExecutionMethod"], EnumExecutionMethod.EM_MACHINE, false)))
                    {
                        SetupEndDates[0] = SetupStartDates[0];
                        SetupEndHours[0] = SetupStartHours[0];
                        var tmp4 = SetupEndDates;
                        string argCurrentDate4 = tmp4[0].ToString();
                        GetNewDate(CalendarCode, ref argCurrentDate4, ref SetupEndHours[0], SetupDurations[0]);
                    }
                }
                else // در صورتیکه عملیات موازی کاری داشته باشد تعداد ماشین هایی که در موازی کاری عملیات شرکت دارند حداقل دو ماشین باشد
                {
                    var mSingleMachines = Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].Select("TreeCode = " + mTreeCode + " And OperationCode = '" + OperationCode + "'", "MachinePriority");
                    var mParallelMachinesValues = new ArrayList[mParallelMachines.Length];
                    var mSingleMachinesValues = new ArrayList[mSingleMachines.Length];
                    int mEarlSingleIndex = -1;
                    long mBaseQty = mQtys[0];
                    if (!string.IsNullOrEmpty(mMatchedOperations)) // عملیات دارای عملیات(های) جفت می باشد
                    {
                        mQtys[0] = GetMaxMatchedQuantity("'" + OperationCode + "'," + mMatchedOperations);
                    }

                    // بدست آوردن زودترین زمان انجام عملیات برای حالتی که امکان موازی کاری نیز وجود داشته باشد
                    GetBackwardParallelExecutionTimes(OperationCode, drOPC, OperationStartDates[0], OperationStartHours[0], 0L, true, mParallelMachines, mSingleMachines, mQtys[0], ref mSingleMachinesValues, ref mParallelMachinesValues, ref mEarlSingleIndex);
                    if (mEarlSingleIndex > -1)
                    {
                        MachineCodes[0] = Conversions.ToString(mSingleMachinesValues[mEarlSingleIndex][0]);
                        PlanningStartDates[0] = mSingleMachinesValues[mEarlSingleIndex][1].ToString();
                        PlanningStartHours[0] = Conversions.ToDouble(mSingleMachinesValues[mEarlSingleIndex][2]);
                        PlanningEndDates[0] = mSingleMachinesValues[mEarlSingleIndex][3].ToString();
                        PlanningEndHours[0] = Conversions.ToDouble(mSingleMachinesValues[mEarlSingleIndex][4]);
                        SetupStartDates[0] = mSingleMachinesValues[mEarlSingleIndex][5].ToString();
                        SetupStartHours[0] = Conversions.ToDouble(mSingleMachinesValues[mEarlSingleIndex][6]);
                        SetupEndDates[0] = mSingleMachinesValues[mEarlSingleIndex][12].ToString();
                        SetupEndHours[0] = Conversions.ToDouble(mSingleMachinesValues[mEarlSingleIndex][13]);
                        OperationStartDates[0] = mSingleMachinesValues[mEarlSingleIndex][7].ToString();
                        OperationStartHours[0] = Conversions.ToDouble(mSingleMachinesValues[mEarlSingleIndex][8]);
                        OperationEndDates[0] = mSingleMachinesValues[mEarlSingleIndex][14].ToString();
                        OperationEndHours[0] = Conversions.ToDouble(mSingleMachinesValues[mEarlSingleIndex][15]);
                        mQtys[0] = mBaseQty; // mSingleMachinesValues(mEarlSingleIndex)(9)
                        SetupDurations[0] = Conversions.ToDouble(mSingleMachinesValues[mEarlSingleIndex][10]);
                        OperationDurations[0] = Conversions.ToDouble(mSingleMachinesValues[mEarlSingleIndex][11]);
                    }
                    else
                    {
                        long mParallelQty = 0L;
                        MachineCodes = new string[mParallelMachinesValues.Length];
                        mQtys = new long[mParallelMachinesValues.Length];
                        PlanningStartDates = new string[mParallelMachinesValues.Length];
                        PlanningStartHours = new double[mParallelMachinesValues.Length];
                        PlanningEndDates = new string[mParallelMachinesValues.Length];
                        PlanningEndHours = new double[mParallelMachinesValues.Length];
                        SetupStartDates = new string[mParallelMachinesValues.Length];
                        SetupStartHours = new double[mParallelMachinesValues.Length];
                        SetupEndDates = new string[mParallelMachinesValues.Length];
                        SetupEndHours = new double[mParallelMachinesValues.Length];
                        OperationStartDates = new string[mParallelMachinesValues.Length];
                        OperationStartHours = new double[mParallelMachinesValues.Length];
                        OperationEndDates = new string[mParallelMachinesValues.Length];
                        OperationEndHours = new double[mParallelMachinesValues.Length];
                        SetupDurations = new double[mParallelMachinesValues.Length];
                        OperationDurations = new double[mParallelMachinesValues.Length];
                        for (int I = 0, loopTo1 = mParallelMachinesValues.Length - 1; I <= loopTo1; I++)
                        {
                            MachineCodes[I] = Conversions.ToString(mParallelMachinesValues[I][0]);
                            PlanningStartDates[I] = mParallelMachinesValues[I][1].ToString();
                            PlanningStartHours[I] = Conversions.ToDouble(mParallelMachinesValues[I][2]);
                            PlanningEndDates[I] = mParallelMachinesValues[I][3].ToString();
                            PlanningEndHours[I] = Conversions.ToDouble(mParallelMachinesValues[I][4]);
                            SetupStartDates[I] = mParallelMachinesValues[I][5].ToString();
                            SetupStartHours[I] = Conversions.ToDouble(mParallelMachinesValues[I][6]);
                            SetupEndDates[I] = mParallelMachinesValues[I][12].ToString();
                            SetupEndHours[I] = Conversions.ToDouble(mParallelMachinesValues[I][13]);
                            OperationStartDates[I] = mParallelMachinesValues[I][7].ToString();
                            OperationStartHours[I] = Conversions.ToDouble(mParallelMachinesValues[I][8]);
                            OperationEndDates[I] = mParallelMachinesValues[I][14].ToString();
                            OperationEndHours[I] = Conversions.ToDouble(mParallelMachinesValues[I][15]);
                            mQtys[I] = Conversions.ToLong(mParallelMachinesValues[I][9]);
                            mParallelQty += mQtys[I];
                            SetupDurations[I] = Conversions.ToDouble(mParallelMachinesValues[I][10]);
                            OperationDurations[I] = Conversions.ToDouble(mParallelMachinesValues[I][11]);
                        }

                        if (!string.IsNullOrEmpty(mMatchedOperations) && mBaseQty != mParallelQty)
                        {
                            long mAddedQty = 0L;
                            for (int I = 0, loopTo2 = mQtys.Length - 1; I <= loopTo2; I++)
                            {
                                long mPartQtyPercent = (long)Math.Round(mQtys[I] / (double)mBaseQty);
                                if (I == mQtys.Length - 1)
                                {
                                    mQtys[I] = mBaseQty - mAddedQty;
                                }
                                else
                                {
                                    mAddedQty += mBaseQty * mPartQtyPercent;
                                    mQtys[I] = mAddedQty;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Backward_GetFirstAccessibleTime(EnumExecutionMethod ExecMethod,
                                                     string OperationCode,
                                                     ref string MachineCode,
                                                     ref string PlanningStartDate, ref double PlanningStartHour,
                                                     ref string SetupStartDate, ref double SetupStartHour,
                                                     ref string OperationStartDate, ref double OperationStartHour,
                                                     ref string PlanningEndDate, ref double PlanningEndHour,
                                                     ref string SetupEndDate, ref double SetupEndHour,
                                                     ref string OperationEndDate, ref double OperationEndHour,
                                                     ref double SetupDuration,
                                                     ref double OperationDuration,
                                                     ref long mQty,
                                                     ExecutionStatus_enum mExecutionStatus)
        {
            FunctionLabel.Text = Conversions.ToString(Operators.AddObject(FunctionLabel.Tag, " GetBackwardFirstAccessibleTime"));
            Application.DoEvents();
            double Tx = 0d;
            double SetupTime = 0d;
            DataRow[] drExecMachine = null;
            DataRow[] OtherMachines = null;
            string CalendarCode = GetCalendarCode(ExecMethod, OperationCode, MachineCode);

            // Try

            // بدست آوردن طول زمان اجرای عملیات به تعداد مورد نیاز ساب بچ
            Tx = GetOperationLTTime(OperationCode, ExecMethod, mQty, MachineCode);
            OperationDuration = Tx;
            if (ExecMethod == EnumExecutionMethod.EM_MACHINE)
            {
                drExecMachine = Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + OperationCode + "' And MachineCode='" + MachineCode + "'");
                SetupTime = Module1.GetAnyTime_TO_Hour(Conversions.ToShort(drExecMachine[0]["SetupTimeType"]), Conversions.ToDouble(drExecMachine[0]["MachineSetupTime"]), Module1.GetCalendarAccessibleTime(CalendarCode));
                SetupDuration = SetupTime;
                SetupStartDate = OperationStartDate;
                SetupStartHour = OperationStartHour;
                // ماشين Setup بدست آوردن زمان شروع
                string argCurrentDate = SetupStartDate.ToString();
                GetNewDate(CalendarCode, ref argCurrentDate, ref SetupStartHour, SetupTime * -1);
                string SuggestSD = SetupStartDate;
                double SuggestSH = SetupStartHour;

                // کنترل در دسترس بودن زمان بدست آمده برای ماشین
                string argStartDate = SuggestSD;
                string argStartHour = SuggestSH.ToString();
                GetBackwardMachineFirstAccessibleTime(ref argStartDate, ref argStartHour, Tx + SetupTime, MachineCode, CalendarCode);
                SuggestSH = CheckFloating(SuggestSH);
                SetupStartHour = CheckFloating(SetupStartHour);
                if ((SuggestSD + GetCompeleteHour(SuggestSH) ?? "") != (SetupStartDate + GetCompeleteHour(SetupStartHour) ?? ""))
                {
                    SetupStartDate = SuggestSD;
                    SetupStartHour = SuggestSH;
                    OperationStartDate = SetupStartDate;
                    OperationStartHour = SetupStartHour;
                    // بدست آوردن زمان شروع عملیات
                    string argCurrentDate1 = OperationStartDate.ToString();
                    GetNewDate(CalendarCode, ref argCurrentDate1, ref OperationStartHour, SetupTime);
                }

                if (mExecutionStatus == ExecutionStatus_enum.ES_SINGLE)
                {
                    OtherMachines = Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + OperationCode + "' And MachineCode <> '" + MachineCode + "'");
                    for (int I = 0, loopTo = OtherMachines.Length - 1; I <= loopTo; I++)
                    {
                        string TempStartDate = OperationStartDate;
                        double TempStartHour = OperationStartHour;
                        CalendarCode = GetCalendarCode(ExecMethod, OperationCode, Conversions.ToString(OtherMachines[I]["MachineCode"]));
                        // ماشين Setup بدست آوردن طول زمان
                        SetupTime = Module1.GetAnyTime_TO_Hour(Conversions.ToShort(OtherMachines[I]["SetupTimeType"]), Conversions.ToDouble(OtherMachines[I]["MachineSetupTime"]), Module1.GetCalendarAccessibleTime(CalendarCode));
                        // ماشين Setup بدست آوردن زمان شروع
                        string argCurrentDate2 = TempStartDate;
                        GetNewDate(CalendarCode, ref argCurrentDate2, ref TempStartHour, SetupTime * -1);
                        // بدست آوردن طول زمان اجرای عملیات به تعداد مورد نیاز ساب بچ
                        Tx = GetOperationLTTime(OperationCode, ExecMethod, mQty, Conversions.ToString(OtherMachines[I]["MachineCode"]));
                        // کنترل در دسترس بودن زمان بدست آمده برای ماشین
                        string argStartDate1 = TempStartDate;
                        string argStartHour1 = TempStartHour.ToString();
                        GetBackwardMachineFirstAccessibleTime(ref argStartDate1, ref argStartHour1, Tx + SetupTime, Conversions.ToString(OtherMachines[I]["MachineCode"]), CalendarCode);

                        // پیدا کردن زودترین زمان شروع عملیات در بین ماشینها
                        if (String.Compare(SuggestSD, TempStartDate) > 0)
                        {
                            MachineCode = Conversions.ToString(OtherMachines[I]["MachineCode"]);
                            SuggestSD = TempStartDate;
                            SuggestSH = TempStartHour;
                            SetupStartDate = SuggestSD;
                            SetupStartHour = SuggestSH;
                            OperationStartDate = SetupStartDate;
                            OperationStartHour = SetupStartHour;
                            // بدست آوردن زمان شروع عملیات
                            string argCurrentDate3 = OperationStartDate.ToString();
                            GetNewDate(CalendarCode, ref argCurrentDate3, ref OperationStartHour, SetupTime);
                            SetupDuration = SetupTime;
                            OperationDuration = Tx;
                        }
                        else if (SuggestSD == TempStartDate && SuggestSH < TempStartHour)
                        {
                            MachineCode = Conversions.ToString(OtherMachines[I]["MachineCode"]);
                            SuggestSH = TempStartHour;
                            SetupStartHour = SuggestSH;
                            OperationStartDate = SetupStartDate;
                            OperationStartHour = SetupStartHour;
                            // بدست آوردن زمان شروع عملیات
                            string argCurrentDate4 = OperationStartDate.ToString();
                            GetNewDate(CalendarCode, ref argCurrentDate4, ref OperationStartHour, SetupTime);
                            SetupDuration = SetupTime;
                            OperationDuration = Tx;
                        }
                    }
                }

                PlanningStartDate = SetupStartDate;
                PlanningStartHour = SetupStartHour;
            }
            else // عملیات بدون ماشین انجام می شود
            {
                SetupStartDate = "0";
                SetupStartHour = 0d;
                PlanningStartDate = OperationStartDate;
                PlanningStartHour = OperationStartHour;
            }
        }


        /// <summary>
        /// این تابع اولین زمان در دسترس ماشین را محاسبه و باز می گردForward_OperationPlanاند
        /// </summary>
        private void GetBackwardMachineFirstAccessibleTime(ref string StartDate, ref string StartHour, double LT, string MachineCode, string CalendarCode)
        {
            FunctionLabel.Text = Conversions.ToString(Operators.ConcatenateObject(FunctionLabel.Tag, "GetBackwardMachineFirstAccessibleTime"));
            Application.DoEvents();
            string EndDate, EndHour;

            // Try


            EndDate = StartDate;
            EndHour = StartHour;
            // زمان پایان درخواست ماشین را بدست می آوریم
            double argCurrentHour = Conversions.ToDouble(EndHour);
            GetNewDate(CalendarCode, ref EndDate, ref argCurrentHour, LT);
            //double argdblHour = Conversions.ToDouble(StartHour);
            StartHour = CheckFloating(Conversions.ToDouble(StartHour)).ToString();
            //double argdblHour1 = Conversions.ToDouble(EndHour);
            EndHour = CheckFloating(Conversions.ToDouble(EndHour)).ToString();
            StartHour = GetCompeleteHour(Conversions.ToDouble(StartHour));
            EndHour = GetCompeleteHour(Conversions.ToDouble(EndHour));
            string FilterValue = "MachineCode='" + MachineCode + "' And (StartDate+StartHour>='" + StartDate + StartHour.ToString() + "' And StartDate+StartHour<='" + EndDate + EndHour.ToString() + "')";
            Db.DataSet.Tables["Tbl_MachineNotAvailableTimes"].DefaultView.RowFilter = FilterValue;
            if (Db.DataSet.Tables["Tbl_MachineNotAvailableTimes"].DefaultView.Count == 0)
            {
                FilterValue = "MachineCode='" + MachineCode + "' And (EndDate+EndHour>='" + StartDate + StartHour.ToString() + "' And EndDate+EndHour<='" + EndDate + EndHour.ToString() + "')";
                Db.DataSet.Tables["Tbl_MachineNotAvailableTimes"].DefaultView.RowFilter = FilterValue;
                if (Db.DataSet.Tables["Tbl_MachineNotAvailableTimes"].DefaultView.Count > 0)
                {
                    Db.DataSet.Tables["Tbl_MachineNotAvailableTimes"].DefaultView.Sort = "StartDate,StartHour";
                    StartDate = Conversions.ToString(Db.DataSet.Tables["Tbl_MachineNotAvailableTimes"].DefaultView[0]["StartDate"]);
                    StartHour = Conversions.ToString(Db.DataSet.Tables["Tbl_MachineNotAvailableTimes"].DefaultView[0]["StartHour"]);

                    // از زمان کوچکترین پایان اشغال ماشین به اندازه طول زمان اجرای عملیات کم می شود
                    double argCurrentHour1 = Conversions.ToDouble(StartHour);
                    GetNewDate(CalendarCode, ref StartDate, ref argCurrentHour1, (LT + Module1.GetAnyTime_TO_Hour((short)Module1.TimeTypes_enum.TT_MINUTE, 1d, "0:0")) * -1);
                    EndDate = StartDate;
                    EndHour = StartHour;
                    // زمان پایان برای درخواست ماشین را بدست می آوریم
                    double argCurrentHour2 = Conversions.ToDouble(EndHour);
                    GetNewDate(CalendarCode, ref EndDate, ref argCurrentHour2, LT);
                }
            }
            else
            {
                Db.DataSet.Tables["Tbl_MachineNotAvailableTimes"].DefaultView.Sort = "StartDate,StartHour";
                StartDate = Conversions.ToString(Db.DataSet.Tables["Tbl_MachineNotAvailableTimes"].DefaultView[0]["StartDate"]);
                StartHour = Conversions.ToString(Db.DataSet.Tables["Tbl_MachineNotAvailableTimes"].DefaultView[0]["StartHour"]);

                // از زمان کوچکترین شروع اشغال ماشین به اندازه طول زمان اجرای عملیات کم می شود
                double argCurrentHour3 = Conversions.ToDouble(StartHour);
                GetNewDate(CalendarCode, ref StartDate, ref argCurrentHour3, (LT + Module1.GetAnyTime_TO_Hour((short)Module1.TimeTypes_enum.TT_MINUTE, 1d, "0:0")) * -1);
                EndDate = StartDate;
                EndHour = StartHour;
                // زمان پایان برای درخواست ماشین را بدست می آوریم
                double argCurrentHour4 = Conversions.ToDouble(EndHour);
                GetNewDate(CalendarCode, ref EndDate, ref argCurrentHour4, LT);
            }

            while (true)
            {
                //double argdblHour2 = Conversions.ToDouble(StartHour);
                StartHour = CheckFloating(Conversions.ToDouble(StartHour)).ToString();
                //double argdblHour3 = Conversions.ToDouble(EndHour);
                EndHour = CheckFloating(Conversions.ToDouble(EndHour)).ToString();
                StartHour = GetCompeleteHour(Conversions.ToDouble(StartHour));
                EndHour = GetCompeleteHour(Conversions.ToDouble(EndHour));
                FilterValue = "MachineCode='" + MachineCode + "' And (PlanningStartDate+PlanningStartHour>='" + StartDate + StartHour.ToString() + "' And  PlanningStartDate+PlanningStartHour<='" + EndDate + EndHour.ToString() + "')";
                Db.DataSet.Tables["Tbl_Planning"].DefaultView.RowFilter = FilterValue;
                if (Db.DataSet.Tables["Tbl_Planning"].DefaultView.Count == 0)
                {
                    FilterValue = "MachineCode='" + MachineCode + "' And (PlanningEndDate+PlanningEndHour>='" + StartDate + StartHour.ToString() + "' And  PlanningEndDate+PlanningEndHour<='" + EndDate + EndHour.ToString() + "')";
                    Db.DataSet.Tables["Tbl_Planning"].DefaultView.RowFilter = FilterValue;
                    if (Db.DataSet.Tables["Tbl_Planning"].DefaultView.Count == 0) // در صورتیکه عملیاتی که پایان آن در بازۀ مورد نظر وجود نداشته باشد
                    {
                        FilterValue = "MachineCode='" + MachineCode + "' And (PlanningEndDate+PlanningEndHour>='" + StartDate + StartHour.ToString() + "' And  PlanningStartDate+PlanningStartHour<'" + StartDate + StartHour.ToString() + "')";
                        Db.DataSet.Tables["Tbl_Planning"].DefaultView.RowFilter = FilterValue;
                        if (Db.DataSet.Tables["Tbl_Planning"].DefaultView.Count == 0)
                        {
                            break;
                        }
                        else
                        {
                            Db.DataSet.Tables["Tbl_Planning"].DefaultView.Sort = "PlanningStartDate";
                            EndDate = Conversions.ToString(Db.DataSet.Tables["Tbl_Planning"].DefaultView[0]["PlanningStartDate"]);
                            EndHour = Conversions.ToString(Db.DataSet.Tables["Tbl_Planning"].DefaultView[0]["PlanningStartHour"]);
                            double argCurrentHour5 = Conversions.ToDouble(EndHour);
                            GetNewDate(CalendarCode, ref EndDate, ref argCurrentHour5, Module1.GetAnyTime_TO_Hour((short)Module1.TimeTypes_enum.TT_MINUTE, 1d, "0:0") * -1);
                            string EDate = Conversions.ToString(Db.DataSet.Tables["Tbl_Planning"].DefaultView[0]["PlanningEndDate"]);
                            string EHour = Conversions.ToString(Db.DataSet.Tables["Tbl_Planning"].DefaultView[0]["PlanningEndHour"]);
                            StartDate = EndDate;
                            StartHour = EndHour;
                            // زمان شروع برای درخواست ماشین را بدست می آوریم
                            double argCurrentHour6 = Conversions.ToDouble(StartHour);
                            GetNewDate(CalendarCode, ref StartDate, ref argCurrentHour6, LT * -1);
                        }
                    }
                    else
                    {
                        Db.DataSet.Tables["Tbl_Planning"].DefaultView.Sort = "PlanningStartDate";
                        EndDate = Conversions.ToString(Db.DataSet.Tables["Tbl_Planning"].DefaultView[0]["PlanningStartDate"]);
                        EndHour = Conversions.ToString(Db.DataSet.Tables["Tbl_Planning"].DefaultView[0]["PlanningStartHour"]);
                        double argCurrentHour7 = Conversions.ToDouble(EndHour);
                        GetNewDate(CalendarCode, ref EndDate, ref argCurrentHour7, Module1.GetAnyTime_TO_Hour((short)Module1.TimeTypes_enum.TT_MINUTE, 1d, "0:0") * -1);
                        StartDate = EndDate;
                        StartHour = EndHour;
                        // زمان شروع برای درخواست ماشین را بدست می آوریم
                        double argCurrentHour8 = Conversions.ToDouble(StartHour);
                        GetNewDate(CalendarCode, ref StartDate, ref argCurrentHour8, LT * -1);
                    }
                }
                else
                {
                    Db.DataSet.Tables["Tbl_Planning"].DefaultView.Sort = "PlanningStartDate";
                    EndDate = Conversions.ToString(Db.DataSet.Tables["Tbl_Planning"].DefaultView[0]["PlanningStartDate"]);
                    EndHour = Conversions.ToString(Db.DataSet.Tables["Tbl_Planning"].DefaultView[0]["PlanningStartHour"]);
                    double argCurrentHour9 = Conversions.ToDouble(EndHour);
                    GetNewDate(CalendarCode, ref EndDate, ref argCurrentHour9, Module1.GetAnyTime_TO_Hour((short)Module1.TimeTypes_enum.TT_MINUTE, 1d, "0:0") * -1);
                    StartDate = EndDate;
                    StartHour = EndHour;
                    // از زمان کوچکترین شروع اشغال ماشین به اندازه طول زمان اجرای عملیات کم می شود
                    // و زمان شروع برای درخواست ماشین را بدست می آوریم
                    double argCurrentHour10 = Conversions.ToDouble(StartHour);
                    GetNewDate(CalendarCode, ref StartDate, ref argCurrentHour10, LT * -1);
                }
            }
        }

        /// <summary>
        /// این تابع زمان پایان فعالیت را باز می گرداند
        /// </summary>
        private void GetOperationEnd(ref string EndDate, ref double EndHour,
                                     ref string SetupEndDate, ref double SetupEndHour,
                                     ref double SetupDuration,
                                     ref string OperationEndDate, ref double OperationEndHour,
                                     ref double OperationDuration,
                                     string CalendarCode,
                                     string ExecMethod,
                                     string TreeCode,
                                     string OperationCode,
                                     string MachineCode,
                                     long ProductQuantity)
        {
            FunctionLabel.Text = Conversions.ToString(Operators.ConcatenateObject(FunctionLabel.Tag, " GetOperationEnd"));
            Application.DoEvents();
            double MachineSetupTime = 0d;

            // Try
            EnumExecutionMethod ExecutionMethod = EnumExecutionMethod.EM_MACHINE;
            short method = Conversions.ToShort(ExecMethod);
            if (Enum.IsDefined(typeof(EnumExecutionMethod), method))
            {
                ExecutionMethod = (EnumExecutionMethod)method;
            }


            if (ExecutionMethod == EnumExecutionMethod.EM_MACHINE & SetupEndDate != "0")
            {
                var MachineRow = Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].Select("TreeCode=" + TreeCode + " And OperationCode='" + OperationCode + "' And MachineCode='" + MachineCode + "'");
                MachineSetupTime = Module1.GetAnyTime_TO_Hour(Conversions.ToShort(MachineRow[0]["SetupTimeType"]), Conversions.ToDouble(MachineRow[0]["MachineSetupTime"]), Module1.GetCalendarAccessibleTime(CalendarCode));

                // ماشین Setup بدست آوردن طول زمان و زمان پایان
                SetupDuration = MachineSetupTime;
                string argCurrentDate = SetupEndDate.ToString();
                GetNewDate(CalendarCode, ref argCurrentDate, ref SetupEndHour, MachineSetupTime);
            }
            else
            {
                SetupEndDate = "0";
                SetupEndHour = 0d;
                SetupDuration = 0d;
            }

            double OpSpeed = Conversions.ToDouble(GetOpExecutionSpeed(ExecutionMethod, TreeCode, OperationCode, MachineCode));
            double LT = ProductQuantity / OpSpeed;

            // بدست آوردن طول زمان و زمان پایان عملیات
            OperationDuration = LT;
            string argCurrentDate1 = OperationEndDate.ToString();
            GetNewDate(CalendarCode, ref argCurrentDate1, ref OperationEndHour, LT);
            EndDate = OperationEndDate;
            EndHour = OperationEndHour;
        }


        /// <summary>
        /// این تابع زمان شروع و پایان یک فعالیت را با توجه به فعالیت(های) پیشنیازش محاسبه می کند
        /// </summary>
        private void Get_StartTime_With_PreOperation(
            string OperationCode,
            string PreOperationCode,
            string mPreOpMachineCode,
            EnumRelationType RelationType,
            int LagTime,
            int TimeType,
            ref string PlanningDate,
            ref double PlanningHour,
            ref string SetupDate,
            ref double SetupHour,
            ref string OperationDate,
            ref double OperationHour,
            long mQty,
            string mOPMachineCode)
        {
            FunctionLabel.Text = Conversions.ToString(Operators.ConcatenateObject(FunctionLabel.Tag, "Get_StartTime_With_PreOperation"));
            Application.DoEvents();
            string OpExecSpeed = "0";
            string PreOpExecSpeed = "0";
            double LT1 = 0d;
            double LT2 = 0d;
            double Lag = 0d;
            double Setup1 = 0d;
            double Setup2 = 0d;
            string CalendarCode;
            var drOPCs = Db.DataSet.Tables["Tbl_ProductOPCs"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + PreOperationCode + "'");
            DataRow[] drPlanning = null;
            DataRow[] drExecMachine;

            // Try


            switch (RelationType) // تعیین نوع ارتباط
            {
                case EnumRelationType.RT_FS:
                {
                    drOPCs = Db.DataSet.Tables["Tbl_ProductOPCs"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + OperationCode + "'");

                    // بدست آوردن کد ماشین عملیات پسنیاز
                    // MachineCode = GetMachineCode(drOPCs(0)("ExecutionMethod"), OperationCode)
                    // بدست آوردن تقویم فعالیت پسنیاز
                    EnumExecutionMethod execMethod = CommonTool.GetExecutionMethod(drOPCs[0]["ExecutionMethod"].ToString());
                    CalendarCode = GetCalendarCode(execMethod, OperationCode, mOPMachineCode);
                    Lag = Module1.GetAnyTime_TO_Hour((short)TimeType, LagTime, Module1.GetCalendarAccessibleTime(CalendarCode));
                    OperationDate = PlanningDate;
                    OperationHour = PlanningHour;
                    GetNewDate(CalendarCode, ref OperationDate, ref OperationHour, Lag);

                    switch (execMethod)
                    {
                        case EnumExecutionMethod.EM_MACHINE:
                        {
                            drExecMachine = Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + OperationCode + "' And MachineCode='" + mOPMachineCode + "'");
                            Setup2 = Module1.GetAnyTime_TO_Hour(Conversions.ToShort(drExecMachine[0]["SetupTimeType"]), Conversions.ToDouble(drExecMachine[0]["MachineSetupTime"]), Module1.GetCalendarAccessibleTime(CalendarCode));
                            SetupDate = OperationDate;
                            SetupHour = OperationHour;
                            // از شروع عملیات به عقب بر می گردیم Setup2 به اندازۀ
                            GetNewDate(CalendarCode, ref SetupDate, ref SetupHour, Setup2 * -1);
                            PlanningDate = SetupDate;
                            PlanningHour = SetupHour;
                            break;
                        }

                        default:
                        {
                            SetupDate = 0.ToString();
                            SetupHour = 0d;
                            PlanningDate = OperationDate;
                            PlanningHour = OperationHour;
                            break;
                        }
                    }

                    break;
                }

                case EnumRelationType.RT_SS:
                {
                    drOPCs = Db.DataSet.Tables["Tbl_ProductOPCs"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + OperationCode + "'");

                    // بدست آوردن کد ماشین عملیات پسنیاز
                    // MachineCode = GetMachineCode(drOPCs(0)("ExecutionMethod"), OperationCode)
                    // بدست آوردن تقویم فعالیت پسنیاز
                    EnumExecutionMethod execMethod = CommonTool.GetExecutionMethod(drOPCs[0]["ExecutionMethod"].ToString());
                    CalendarCode = GetCalendarCode(execMethod, OperationCode, mOPMachineCode);
                    Lag = Module1.GetAnyTime_TO_Hour((short)TimeType, LagTime, Module1.GetCalendarAccessibleTime(CalendarCode));
                    OperationDate = PlanningDate;
                    OperationHour = PlanningHour;
                    GetNewDate(CalendarCode, ref OperationDate, ref OperationHour, Lag);
                    switch (execMethod)
                    {
                        case EnumExecutionMethod.EM_MACHINE:
                        {
                            drExecMachine = Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + OperationCode + "' And MachineCode='" + mOPMachineCode + "'");
                            Setup2 = Module1.GetAnyTime_TO_Hour(Conversions.ToShort(drExecMachine[0]["SetupTimeType"]), Conversions.ToDouble(drExecMachine[0]["MachineSetupTime"]), Module1.GetCalendarAccessibleTime(CalendarCode));
                            SetupDate = OperationDate;
                            SetupHour = OperationHour;
                            // از شروع عملیات به عقب بر می گردیم Setup2 به اندازۀ
                            GetNewDate(CalendarCode, ref SetupDate, ref SetupHour, Setup2 * -1);
                            PlanningDate = SetupDate;
                            PlanningHour = SetupHour;
                            break;
                        }

                        default:
                        {
                            SetupDate = 0.ToString();
                            SetupHour = 0d;
                            PlanningDate = OperationDate;
                            PlanningHour = OperationHour;
                            break;
                        }
                    }

                    break;
                }

                case EnumRelationType.RT_FF:
                {
                    drOPCs = Db.DataSet.Tables["Tbl_ProductOPCs"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + OperationCode + "'");

                    // بدست آوردن کد ماشین عملیات پسنیاز
                    // MachineCode = GetMachineCode(drOPCs(0)("ExecutionMethod"), OperationCode)
                    // بدست آوردن تقویم فعالیت پسنیاز
                    EnumExecutionMethod execMethod = CommonTool.GetExecutionMethod(drOPCs[0]["ExecutionMethod"].ToString());

                    CalendarCode = GetCalendarCode(execMethod, OperationCode, mOPMachineCode);
                    Lag = Module1.GetAnyTime_TO_Hour((short)TimeType, LagTime, Module1.GetCalendarAccessibleTime(CalendarCode));
                    OperationDate = PlanningDate;
                    OperationHour = PlanningHour;
                    // بدست آوردن زمان پایان عملیات پسنیاز
                    GetNewDate(CalendarCode, ref OperationDate, ref OperationHour, Lag);
                    LT2 = GetOperationLTTime(OperationCode, execMethod, mQty, mOPMachineCode);
                    switch (execMethod)
                    {
                        case EnumExecutionMethod.EM_MACHINE:
                        {
                            drExecMachine = Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + OperationCode + "' And MachineCode='" + mOPMachineCode + "'");
                            Setup2 = Module1.GetAnyTime_TO_Hour(Conversions.ToShort(drExecMachine[0]["SetupTimeType"]), Conversions.ToDouble(drExecMachine[0]["MachineSetupTime"]), Module1.GetCalendarAccessibleTime(CalendarCode));
                            SetupDate = OperationDate;
                            SetupHour = OperationHour;
                            // از پایان عملیات به عقب بر می گردیم LT2 + Setup2 به اندازۀ
                            // و شروع عملیات پسنیاز را بدست می آوریم
                            GetNewDate(CalendarCode, ref SetupDate, ref SetupHour, (LT2 + Setup2) * -1);
                            PlanningDate = SetupDate;
                            PlanningHour = SetupHour;
                            break;
                        }

                        default:
                        {
                            SetupDate = 0.ToString();
                            SetupHour = 0d;
                            PlanningDate = OperationDate;
                            PlanningHour = OperationHour;
                            break;
                        }
                    }

                    // بدست آوردن شروع عملیات پسنیاز
                    GetNewDate(CalendarCode, ref OperationDate, ref OperationHour, LT2 * -1);
                    break;
                }
                // Case RelationType_enum.RT_SF
                case EnumRelationType.RT_ASAP:
                {
                    int OpMin = 0;
                    int PreOpMin = 0;
                    double OpExecTime = 0d;
                    double PreOpExecTime = 0d;
                    EnumExecutionMethod execMethod = CommonTool.GetExecutionMethod(drOPCs[0]["ExecutionMethod"].ToString());
                    // بدست آوردن تقویم فعالیت پیشنیاز
                    CalendarCode = GetCalendarCode(execMethod, PreOperationCode, mPreOpMachineCode);
                    // بدست آوردن سرعت انجام فعالیت پیشنیاز
                    PreOpExecSpeed = GetOpExecutionSpeed(execMethod, mTreeCode, PreOperationCode, mPreOpMachineCode);
                    // دریافت زمان انجام یکبار عملیات پیشنیاز
                    PreOpExecTime = GetOneOperationExecTime(PreOperationCode, execMethod, mPreOpMachineCode);
                    // دریافت تعداد حداقل تولید اقتصادی عملیات پیشنیاز
                    PreOpMin = GetOperationMinQuantity(PreOperationCode, execMethod, mPreOpMachineCode);
                    switch (execMethod)
                    {
                        case EnumExecutionMethod.EM_MACHINE:
                        {
                            drExecMachine = Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + PreOperationCode + "' And MachineCode='" + mPreOpMachineCode + "'");
                            Setup1 = Module1.GetAnyTime_TO_Hour(Conversions.ToShort(drExecMachine[0]["SetupTimeType"]), Conversions.ToDouble(drExecMachine[0]["MachineSetupTime"]), Module1.GetCalendarAccessibleTime(CalendarCode));
                            break;
                        }
                    }

                    drOPCs = Db.DataSet.Tables["Tbl_ProductOPCs"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + OperationCode + "'");

                    // بدست آوردن کد ماشین عملیات پسنیاز
                    // MachineCode = GetMachineCode(drOPCs(0)("ExecutionMethod"), OperationCode)
                    // بدست آوردن تقویم فعالیت پسنیاز
                    EnumExecutionMethod ExecutionMethod = CommonTool.GetExecutionMethod(drOPCs[0]["ExecutionMethod"].ToString());
                    CalendarCode = GetCalendarCode(ExecutionMethod, OperationCode, mOPMachineCode);
                    // بدست آوردن سرعت انجام فعالیت پسنیاز
                    OpExecSpeed = GetOpExecutionSpeed(ExecutionMethod, mTreeCode, OperationCode, mOPMachineCode);
                    // دریافت زمان انجام یکبار عملیات پسنیاز
                    OpExecTime = GetOneOperationExecTime(OperationCode, ExecutionMethod, mOPMachineCode);
                    // دریافت تعداد حداقل تولید اقتصادی عملیات پسنیاز
                    OpMin = GetOperationMinQuantity(OperationCode, ExecutionMethod, mOPMachineCode);
                    switch (ExecutionMethod)
                    {
                        case EnumExecutionMethod.EM_MACHINE:
                        {
                            drExecMachine = Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + OperationCode + "' And MachineCode='" + mOPMachineCode + "'");
                            Setup2 = Module1.GetAnyTime_TO_Hour(Conversions.ToShort(drExecMachine[0]["SetupTimeType"]), Conversions.ToDouble(drExecMachine[0]["MachineSetupTime"]), Module1.GetCalendarAccessibleTime(CalendarCode));
                            break;
                        }
                    }

                    Lag = Module1.GetAnyTime_TO_Hour((short)TimeType, LagTime, Module1.GetCalendarAccessibleTime(CalendarCode));
                    if (Conversions.ToDouble(OpExecSpeed) >= Conversions.ToDouble(PreOpExecSpeed))
                    {
                        drOPCs = Db.DataSet.Tables["Tbl_ProductOPCs"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + PreOperationCode + "'");
                        drPlanning = Db.DataSet.Tables["Tbl_Planning"].Select("TreeCode=" + mTreeCode + " And SubbatchCode='" + mSubbatchCode + "' And OperationCode='" + PreOperationCode + "'");
                        OperationDate = Conversions.ToString(drPlanning[0]["OperationEndDate"]);
                        OperationHour = Conversions.ToDouble(drPlanning[0]["OperationEndHour"]);
                        mPreOpMachineCode = Conversions.ToString(Interaction.IIf(ReferenceEquals(drPlanning[0]["MachineCode"], DBNull.Value), 0, drPlanning[0]["MachineCode"].ToString()));

                        // بدست آوردن بزرگترین تاریخ پایان(در صورتیکه عملیات موازی کاری باشد)در بین رکوردها
                        for (int I = 1, loopTo = drPlanning.Length - 1; I <= loopTo; I++)
                        {
                            if (Conversions.ToDouble(OperationDate) < Conversions.ToLong(drPlanning[I]["OperationEndDate"]))
                            {
                                OperationDate = Conversions.ToLong(drPlanning[I]["OperationEndDate"]).ToString();
                                OperationHour = Conversions.ToDouble(drPlanning[I]["OperationEndHour"]);
                                mPreOpMachineCode = Conversions.ToString(Interaction.IIf(ReferenceEquals(drPlanning[I]["MachineCode"], DBNull.Value), 0, drPlanning[I]["MachineCode"].ToString()));
                            }
                            else if (Conversions.ToDouble(OperationDate) == Conversions.ToLong(drPlanning[I]["OperationEndDate"]))
                            {
                                if (OperationHour < Conversions.ToDouble(drPlanning[I]["OperationEndHour"]))
                                {
                                    OperationHour = Conversions.ToDouble(drPlanning[I]["OperationEndHour"]);
                                    mPreOpMachineCode = Conversions.ToString(Interaction.IIf(ReferenceEquals(drPlanning[I]["MachineCode"], DBNull.Value), 0, drPlanning[I]["MachineCode"].ToString()));
                                }
                            }
                        }

                        // mohsendokht
                        OperationHour = CheckFloating(OperationHour);
                        PlanningHour = CheckFloating(PlanningHour);
                        if (Operators.CompareString(OperationDate.ToString() + GetCompeleteHour(OperationHour), PlanningDate.ToString() + GetCompeleteHour(PlanningHour), false) < 0)
                        {
                            OperationDate = PlanningDate;
                            OperationHour = PlanningHour;
                        }

                        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                        drOPCs = Db.DataSet.Tables["Tbl_ProductOPCs"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + OperationCode + "'");

                        // بدست آوردن زمان پایان عملیات پسنیاز
                        GetNewDate(CalendarCode, ref OperationDate, ref OperationHour, Lag);
                        LT2 = GetOperationLTTime(OperationCode, (EnumExecutionMethod)(drOPCs[0]["ExecutionMethod"]), mQty, mOPMachineCode);
                        switch (drOPCs[0]["ExecutionMethod"])
                        {
                            case EnumExecutionMethod.EM_MACHINE:
                            {
                                SetupDate = OperationDate;
                                SetupHour = OperationHour;
                                // از پایان عملیات به عقب بر می گردیم LT2 + Setup2 به اندازۀ
                                // و شروع عملیات پسنیاز را بدست می آوریم
                                GetNewDate(CalendarCode, ref SetupDate, ref SetupHour, (LT2 + Setup2) * -1);
                                // mohsendokht
                                SetupHour = CheckFloating(SetupHour);
                                PlanningHour = CheckFloating(PlanningHour);
                                if (Operators.CompareString(SetupDate.ToString() + GetCompeleteHour(SetupHour), PlanningDate.ToString() + GetCompeleteHour(PlanningHour), false) < 0)
                                {
                                    SetupDate = PlanningDate;
                                    SetupHour = PlanningHour;
                                    OperationDate = SetupDate;
                                    OperationHour = SetupHour;
                                    // بدست آوردن شروع عملیات پسنیاز
                                    GetNewDate(CalendarCode, ref OperationDate, ref OperationHour, Setup2);
                                }
                                else
                                {
                                    PlanningDate = SetupDate;
                                    PlanningHour = SetupHour;

                                    // بدست آوردن شروع عملیات پسنیاز
                                    GetNewDate(CalendarCode, ref OperationDate, ref OperationHour, LT2 * -1);
                                }

                                break;
                            }

                            default:
                            {
                                // بدست آوردن شروع عملیات پسنیاز
                                GetNewDate(CalendarCode, ref OperationDate, ref OperationHour, LT2 * -1);
                                SetupDate = 0.ToString();
                                SetupHour = 0d;
                                PlanningDate = OperationDate;
                                PlanningHour = OperationHour;
                                break;
                            }
                        }
                    }
                    // <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    // ElseIf CDbl(OpExecSpeed) = CDbl(PreOpExecSpeed) Then
                    // drOPCs = dsProductionPlanning.Tables("Tbl_ProductOPCs").Select("TreeCode=" & mTreeCode & " And OperationCode='" & PreOperationCode & "'")
                    // drPlanning = dsProductionPlanning.Tables("Tbl_Planning").Select("TreeCode=" & mTreeCode & " And SubbatchCode='" & mSubbatchCode & "' And OperationCode='" & PreOperationCode & "'")

                    // OperationDate = drPlanning(0)("OperationEndDate")
                    // OperationHour = drPlanning(0)("OperationEndHour")
                    // mPreOpMachineCode = IIf(drPlanning(0)("MachineCode") Is DBNull.Value, 0, drPlanning(0)("MachineCode").ToString())

                    // 'بدست آوردن بزرگترین تاریخ پایان(در صورتیکه عملیات موازی کاری باشد)در بین رکوردها
                    // For I As Integer = 1 To drPlanning.Length - 1
                    // If OperationDate < CLng(drPlanning(I)("OperationEndDate")) Then
                    // OperationDate = CLng(drPlanning(I)("OperationEndDate"))
                    // OperationHour = CDbl(drPlanning(I)("OperationEndHour"))
                    // mPreOpMachineCode = IIf(drPlanning(I)("MachineCode") Is DBNull.Value, 0, drPlanning(I)("MachineCode").ToString())
                    // ElseIf OperationDate = CLng(drPlanning(I)("OperationEndDate")) Then
                    // If OperationHour < CDbl(drPlanning(I)("OperationEndHour")) Then
                    // OperationHour = CDbl(drPlanning(I)("OperationEndHour"))
                    // mPreOpMachineCode = IIf(drPlanning(I)("MachineCode") Is DBNull.Value, 0, drPlanning(I)("MachineCode").ToString())
                    // End If
                    // End If
                    // Next I

                    // 'mohsendokht
                    // CheckFloating(OperationHour)
                    // CheckFloating(PlanningHour)
                    // If OperationDate.ToString() & GetCompeleteHour(OperationHour) < PlanningDate.ToString() & GetCompeleteHour(PlanningHour) Then
                    // OperationDate = PlanningDate
                    // OperationHour = PlanningHour
                    // End If

                    // LT1 = PreOpExecTime

                    // GetNewDate(CalendarCode, OperationDate, OperationHour, LT1 + Lag)

                    // drOPCs = dsProductionPlanning.Tables("Tbl_ProductOPCs").Select("TreeCode=" & mTreeCode & " And OperationCode='" & OperationCode & "'")

                    // Select Case drOPCs(0)("ExecutionMethod")
                    // Case ExecutionMethod_enum.EM_MACHINE
                    // LT2 = GetOperationLTTime(OperationCode, ExecutionMethod_enum.EM_MACHINE, mQty, mOPMachineCode)

                    // SetupDate = OperationDate
                    // SetupHour = OperationHour

                    // 'از پایان عملیات به عقب حرکت می کنیم LT2 + Setup2 به اندازۀ
                    // GetNewDate(CalendarCode, SetupDate, SetupHour, (LT2 + Setup2) * -1)

                    // PlanningDate = SetupDate
                    // PlanningHour = SetupHour
                    // Case Else
                    // 'بدست آوردن زمان شروع عملیات جاری
                    // LT2 = mQty / CDbl(OpExecSpeed)
                    // GetNewDate(CalendarCode, OperationDate, OperationHour, LT2 * -1)

                    // SetupDate = 0
                    // SetupHour = 0

                    // PlanningDate = OperationDate
                    // PlanningHour = OperationHour
                    // End Select
                    else if (Conversions.ToDouble(OpExecSpeed) < Conversions.ToDouble(PreOpExecSpeed))
                    {
                        LT1 = OpMin * PreOpExecTime;
                        OperationDate = PlanningDate;
                        OperationHour = PlanningHour;
                        GetNewDate(CalendarCode, ref OperationDate, ref OperationHour, LT1 + Lag);
                        switch ((EnumExecutionMethod)drOPCs[0]["ExecutionMethod"])
                        {
                            case EnumExecutionMethod.EM_MACHINE:
                            {
                                // drExecMachine = dsProductionPlanning.Tables("Tbl_ProductOPCsExecutorMachines").Select("TreeCode=" & mTreeCode & " And OperationCode='" & OperationCode & "' And MachineCode='" & MachineCode & "'")
                                // Setup2 = GetAnyTime_TO_Hour(drExecMachine(0)("SetupTimeType"), drExecMachine(0)("MachineSetupTime"), GetCalendarAccessibleTime(CalendarCode))
                                LT2 = GetOperationLTTime(OperationCode, EnumExecutionMethod.EM_MACHINE, mQty, mOPMachineCode);
                                SetupDate = OperationDate;
                                SetupHour = OperationHour;

                                // از شروع عملیات به عقب حرکت می کنیم - Setup2 به اندازۀ
                                GetNewDate(CalendarCode, ref SetupDate, ref SetupHour, Setup2 * -1);
                                PlanningDate = SetupDate;
                                PlanningHour = SetupHour;
                                break;
                            }

                            default:
                            {
                                SetupDate = 0.ToString();
                                SetupHour = 0d;
                                PlanningDate = OperationDate;
                                PlanningHour = OperationHour;
                                break;
                            }
                        }
                    }

                    break;
                }
            }
        }

        /// <summary>
        /// این تابع زمان شروع یک فعالیت را با توجه به فعالیت پسنیازش محاسبه می کند
        /// </summary>
        private void Get_StartTime_BasedOn_NextOp(string OperationCode,
                                                  string AfterwardOperationCode,
                                                  string mAfterwardOpMachineCode,
                                                  EnumRelationType RelationType,
                                                  int LagTime,
                                                  int LagTimeType,
                                                  ref string PlanningStartDate, ref double PlanningStartHour,
                                                  ref string SetupDate, ref double SetupHour,
                                                  ref string OperationDate, ref double OperationHour,
                                                  long mQty)
        {
            FunctionLabel.Text = Conversions.ToString(Operators.ConcatenateObject(FunctionLabel.Tag, " Get_StartTime_BasedOn_NextOp"));
            Application.DoEvents();
            string OpExecSpeed;
            string AfterwardOpExecSpeed;
            double LT1 = 0d;
            //double LT2 = 0d;
            double Lag = 0d;
            double Setup1 = 0d;
            var drOPCs = Db.DataSet.Tables["Tbl_ProductOPCs"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + OperationCode + "'");
            DataRow[] drExecMachine = null;
            EnumExecutionMethod ExecutionMethod = CommonTool.GetExecutionMethod(drOPCs[0]["ExecutionMethod"].ToString());
            string MachineCode = GetMachineCode(ExecutionMethod, OperationCode);
            string CalendarCode = GetCalendarCode(ExecutionMethod, OperationCode, MachineCode);

            // Try


            switch (RelationType) // تعیین نوع ارتباط
            {
                case EnumRelationType.RT_FS:
                {
                    OperationDate = PlanningStartDate;
                    OperationHour = PlanningStartHour;
                    Lag = Module1.GetAnyTime_TO_Hour((short)LagTimeType, LagTime, Module1.GetCalendarAccessibleTime(CalendarCode));
                    // بدست آوردن زمان پایان عملیات
                    GetNewDate(CalendarCode, ref OperationDate, ref OperationHour, Lag * -1);
                    // بدست آوردن طول زمان اجرای عملیات
                    LT1 = GetOperationLTTime(OperationCode, ExecutionMethod, mQty, MachineCode);
                    // بدست آوردن زمان شروع عملیات
                    GetNewDate(CalendarCode, ref OperationDate, ref OperationHour, LT1 * -1);
                    switch (ExecutionMethod)
                    {
                        case EnumExecutionMethod.EM_MACHINE:
                        {
                            drExecMachine = Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + OperationCode + "' And MachineCode='" + MachineCode + "'");
                            Setup1 = Module1.GetAnyTime_TO_Hour(Conversions.ToShort(drExecMachine[0]["SetupTimeType"]), Conversions.ToDouble(drExecMachine[0]["MachineSetupTime"]), Module1.GetCalendarAccessibleTime(CalendarCode));
                            SetupDate = OperationDate;
                            SetupHour = OperationHour;
                            // Setup بدست آوردن زمان شروع
                            GetNewDate(CalendarCode, ref SetupDate, ref SetupHour, Setup1 * -1);
                            PlanningStartDate = SetupDate;
                            PlanningStartHour = SetupHour;
                            break;
                        }

                        default:
                        {
                            SetupDate = "0";
                            SetupHour = 0d;
                            PlanningStartDate = OperationDate;
                            PlanningStartHour = OperationHour;
                            break;
                        }
                    }

                    break;
                }

                case EnumRelationType.RT_FF:
                {
                    OperationDate = PlanningStartDate;
                    OperationHour = PlanningStartHour;
                    Lag = Module1.GetAnyTime_TO_Hour((short)LagTimeType, LagTime, Module1.GetCalendarAccessibleTime(CalendarCode));
                    // بدست آوردن زمان پایان عملیات
                    GetNewDate(CalendarCode, ref OperationDate, ref OperationHour, Lag * -1);
                    // بدست آوردن طول زمان اجرای عملیات
                    LT1 = GetOperationLTTime(OperationCode, ExecutionMethod, mQty, MachineCode);
                    // بدست آوردن زمان شروع عملیات
                    GetNewDate(CalendarCode, ref OperationDate, ref OperationHour, LT1 * -1);
                    switch (ExecutionMethod)
                    {
                        case EnumExecutionMethod.EM_MACHINE:
                        {
                            drExecMachine = Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + OperationCode + "' And MachineCode='" + MachineCode + "'");
                            Setup1 = Module1.GetAnyTime_TO_Hour(Conversions.ToShort(drExecMachine[0]["SetupTimeType"]), Conversions.ToDouble(drExecMachine[0]["MachineSetupTime"]), Module1.GetCalendarAccessibleTime(CalendarCode));
                            SetupDate = OperationDate;
                            SetupHour = OperationHour;
                            // Setup بدست آوردن زمان شروع
                            GetNewDate(CalendarCode, ref SetupDate, ref SetupHour, Setup1 * -1);
                            PlanningStartDate = SetupDate;
                            PlanningStartHour = SetupHour;
                            break;
                        }

                        default:
                        {
                            SetupDate = 0.ToString();
                            SetupHour = 0d;
                            PlanningStartDate = OperationDate;
                            PlanningStartHour = OperationHour;
                            break;
                        }
                    }

                    break;
                }

                case EnumRelationType.RT_SS:
                {
                    OperationDate = PlanningStartDate;
                    OperationHour = PlanningStartHour;
                    Lag = Module1.GetAnyTime_TO_Hour((short)LagTimeType, LagTime, Module1.GetCalendarAccessibleTime(CalendarCode));
                    // بدست آوردن زمان شروع عملیات
                    GetNewDate(CalendarCode, ref OperationDate, ref OperationHour, Lag * -1);
                    switch (ExecutionMethod)
                    {
                        case EnumExecutionMethod.EM_MACHINE:
                        {
                            drExecMachine = Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + OperationCode + "' And MachineCode='" + MachineCode + "'");
                            Setup1 = Module1.GetAnyTime_TO_Hour(Conversions.ToShort(drExecMachine[0]["SetupTimeType"]), Conversions.ToDouble(drExecMachine[0]["MachineSetupTime"]), Module1.GetCalendarAccessibleTime(CalendarCode));
                            SetupDate = OperationDate;
                            SetupHour = OperationHour;
                            // Setup بدست آوردن زمان شروع
                            GetNewDate(CalendarCode, ref SetupDate, ref SetupHour, Setup1 * -1);
                            PlanningStartDate = SetupDate;
                            PlanningStartHour = SetupHour;
                            break;
                        }

                        default:
                        {
                            SetupDate = 0.ToString();
                            SetupHour = 0d;
                            PlanningStartDate = OperationDate;
                            PlanningStartHour = OperationHour;
                            break;
                        }
                    }

                    break;
                }

                case EnumRelationType.RT_ASAP:
                {
                    double Setup2 = 0d;
                    int Min1 = 0;
                    int Min2 = 0;
                    double OpExecTime = 0d;
                    double AfterwardOpExecTime = 0d;

                    // بدست آوردن سرعت انجام عملیات جاری
                    OpExecSpeed = GetOpExecutionSpeed(ExecutionMethod, mTreeCode, OperationCode, MachineCode);
                    if (ExecutionMethod != EnumExecutionMethod.EM_MACHINE)
                    {
                        drExecMachine = Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + OperationCode + "' And MachineCode='" + MachineCode + "'");
                        Setup1 = Module1.GetAnyTime_TO_Hour(Conversions.ToShort(drExecMachine[0]["SetupTimeType"]), Conversions.ToDouble(drExecMachine[0]["MachineSetupTime"]), Module1.GetCalendarAccessibleTime(CalendarCode));
                    }

                    Min1 = GetOperationMinQuantity(OperationCode, ExecutionMethod, MachineCode);
                    OpExecTime = GetOneOperationExecTime(OperationCode, ExecutionMethod, MachineCode);
                    drOPCs = Db.DataSet.Tables["Tbl_ProductOPCs"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + AfterwardOperationCode + "'");

                    // بدست آوردن سرعت انجام عملیات پسنیاز
                    ExecutionMethod = CommonTool.GetExecutionMethod(drOPCs[0]["ExecutionMethod"].ToString());
                    AfterwardOpExecSpeed = GetOpExecutionSpeed(ExecutionMethod, mTreeCode, AfterwardOperationCode, mAfterwardOpMachineCode);
                    if (ExecutionMethod != EnumExecutionMethod.EM_MACHINE)
                    {
                        drExecMachine = Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + AfterwardOperationCode + "' And MachineCode='" + mAfterwardOpMachineCode + "'");
                        Setup2 = Module1.GetAnyTime_TO_Hour(Conversions.ToShort(drExecMachine[0]["SetupTimeType"]), Conversions.ToDouble(drExecMachine[0]["MachineSetupTime"]), GetCalendarAccessibleTime(GetCalendarCode(EnumExecutionMethod.EM_MACHINE, AfterwardOperationCode, mAfterwardOpMachineCode)));
                    }

                    Min2 = GetOperationMinQuantity(AfterwardOperationCode, ExecutionMethod, mAfterwardOpMachineCode);
                    AfterwardOpExecTime = GetOneOperationExecTime(AfterwardOperationCode, ExecutionMethod, mAfterwardOpMachineCode);
                    drOPCs = Db.DataSet.Tables["Tbl_ProductOPCs"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + OperationCode + "'");
                    ExecutionMethod = CommonTool.GetExecutionMethod(drOPCs[0]["ExecutionMethod"].ToString());
                    Lag = Module1.GetAnyTime_TO_Hour((short)LagTimeType, LagTime, Module1.GetCalendarAccessibleTime(CalendarCode));
                    if (Conversions.ToDouble(OpExecSpeed) > Conversions.ToDouble(AfterwardOpExecSpeed)) // در صورتیکه سرعت عملیات جاری بیشتر از سرعت عملیات پسنیاز باشد
                    {
                        OperationDate = PlanningStartDate;
                        OperationHour = PlanningStartHour;
                        LT1 = Min2 * OpExecTime + Lag;
                        // بدست آوردن زمان شروع عملیات
                        GetNewDate(CalendarCode, ref OperationDate, ref OperationHour, LT1 * -1);

                        switch (ExecutionMethod)
                        {
                            case EnumExecutionMethod.EM_MACHINE:
                            {
                                SetupDate = OperationDate;
                                SetupHour = OperationHour;
                                // Setup بدست آوردن زمان شروع
                                GetNewDate(CalendarCode, ref SetupDate, ref SetupHour, Setup1 * -1);
                                PlanningStartDate = SetupDate;
                                PlanningStartHour = SetupHour;
                                break;
                            }

                            default:
                            {
                                SetupDate = 0.ToString();
                                SetupHour = 0d;
                                PlanningStartDate = OperationDate;
                                PlanningStartHour = OperationHour;
                                break;
                            }
                        }
                    }
                    else if (Conversions.ToDouble(OpExecSpeed) < Conversions.ToDouble(AfterwardOpExecSpeed)) // در صورتیکه سرعت عملیات جاری کمتر از سرعت عملیات پسنیاز باشد
                    {
                        var drPLanning = Db.DataSet.Tables["Tbl_Planning"].Select("TreeCode=" + mTreeCode + " And SubbatchCode='" + mSubbatchCode + "' And OperationCode='" + AfterwardOperationCode + "'");
                        PlanningStartDate = Conversions.ToString(drPLanning[0]["OperationEndDate"]);
                        PlanningStartHour = Conversions.ToDouble(drPLanning[0]["OperationEndHour"]);

                        // بدست آوردن بزرگترین تاریخ پایان(در صورتیکه عملیات موازی کاری باشد)در بین رکوردها
                        for (int I = 1, loopTo = drPLanning.Length - 1; I <= loopTo; I++)
                        {
                            if (Conversions.ToDouble(PlanningStartDate) < Conversions.ToLong(drPLanning[I]["OperationEndDate"]))
                            {
                                PlanningStartDate = Conversions.ToLong(drPLanning[I]["OperationEndDate"]).ToString();
                                PlanningStartHour = Conversions.ToDouble(drPLanning[I]["OperationEndHour"]);
                                mAfterwardOpMachineCode = Conversions.ToString(Interaction.IIf(ReferenceEquals(drPLanning[I]["MachineCode"], DBNull.Value), 0, drPLanning[I]["MachineCode"].ToString()));
                            }
                            else if (Conversions.ToDouble(PlanningStartDate) == Conversions.ToLong(drPLanning[I]["OperationEndDate"]))
                            {
                                if (PlanningStartHour < Conversions.ToDouble(drPLanning[I]["OperationEndHour"]))
                                {
                                    PlanningStartHour = Conversions.ToDouble(drPLanning[I]["OperationEndHour"]);
                                    mAfterwardOpMachineCode = Conversions.ToString(Interaction.IIf(ReferenceEquals(drPLanning[I]["MachineCode"], DBNull.Value), 0, drPLanning[I]["MachineCode"].ToString()));
                                }
                            }
                        }

                        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                        OperationDate = PlanningStartDate;
                        OperationHour = PlanningStartHour;
                        Lag = Module1.GetAnyTime_TO_Hour((short)LagTimeType, LagTime, Module1.GetCalendarAccessibleTime(CalendarCode));
                        // بدست آوردن زمان پایان عملیات
                        GetNewDate(CalendarCode, ref OperationDate, ref OperationHour, Lag * -1);
                        // بدست آوردن طول زمان اجرای عملیات
                        LT1 = GetOperationLTTime(OperationCode, ExecutionMethod, mQty, MachineCode);
                        // بدست آوردن زمان شروع عملیات
                        GetNewDate(CalendarCode, ref OperationDate, ref OperationHour, LT1 * -1);
                        switch (ExecutionMethod)
                        {
                            case EnumExecutionMethod.EM_MACHINE:
                            {
                                SetupDate = OperationDate;
                                SetupHour = OperationHour;
                                // Setup بدست آوردن زمان شروع
                                GetNewDate(CalendarCode, ref SetupDate, ref SetupHour, Setup1 * -1);
                                PlanningStartDate = SetupDate;
                                PlanningStartHour = SetupHour;
                                break;
                            }

                            default:
                            {
                                SetupDate = 0.ToString();
                                SetupHour = 0d;
                                PlanningStartDate = OperationDate;
                                PlanningStartHour = OperationHour;
                                break;
                            }
                        }
                    }
                    // <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    else if (Conversions.ToDouble(OpExecSpeed) == Conversions.ToDouble(AfterwardOpExecSpeed)) // در صورتیکه سرعت عملیات جاری کمتر یا مساوی سرعت عملیات پسنیاز باشد
                    {
                        var drPLanning = Db.DataSet.Tables["Tbl_Planning"].Select("TreeCode=" + mTreeCode + " And SubbatchCode='" + mSubbatchCode + "' And OperationCode='" + AfterwardOperationCode + "'");
                        // TODO: why?
                        PlanningStartDate = Conversions.ToString(drPLanning[0]["OperationEndDate"]);
                        PlanningStartHour = Conversions.ToDouble(drPLanning[0]["OperationEndHour"]);

                        // بدست آوردن بزرگترین تاریخ پایان(در صورتیکه عملیات موازی کاری باشد)در بین رکوردها
                        for (int I = 1, loopTo1 = drPLanning.Length - 1; I <= loopTo1; I++)
                        {
                            if (Conversions.ToDouble(PlanningStartDate) < Conversions.ToLong(drPLanning[I]["OperationEndDate"]))
                            {
                                PlanningStartDate = Conversions.ToLong(drPLanning[I]["OperationEndDate"]).ToString();
                                PlanningStartHour = Conversions.ToDouble(drPLanning[I]["OperationEndHour"]);
                                mAfterwardOpMachineCode = Conversions.ToString(Interaction.IIf(ReferenceEquals(drPLanning[I]["MachineCode"], DBNull.Value), 0, drPLanning[I]["MachineCode"].ToString()));
                            }
                            else if (Conversions.ToDouble(PlanningStartDate) == Conversions.ToLong(drPLanning[I]["OperationEndDate"]))
                            {
                                if (PlanningStartHour < Conversions.ToDouble(drPLanning[I]["OperationEndHour"]))
                                {
                                    PlanningStartHour = Conversions.ToDouble(drPLanning[I]["OperationEndHour"]);
                                    mAfterwardOpMachineCode = Conversions.ToString(Interaction.IIf(ReferenceEquals(drPLanning[I]["MachineCode"], DBNull.Value), 0, drPLanning[I]["MachineCode"].ToString()));
                                }
                            }
                        }

                        OperationDate = PlanningStartDate;
                        OperationHour = PlanningStartHour;

                        // بدست آوردن زمان پایان عملیات
                        GetNewDate(CalendarCode, ref OperationDate, ref OperationHour, (OpExecTime + Lag) * -1);
                        // بدست آوردن طول زمان اجرای عملیات
                        LT1 = GetOperationLTTime(OperationCode, ExecutionMethod, mQty, MachineCode);
                        // بدست آوردن زمان شروع عملیات
                        GetNewDate(CalendarCode, ref OperationDate, ref OperationHour, LT1 * -1);
                        switch (ExecutionMethod)
                        {
                            case EnumExecutionMethod.EM_MACHINE:
                            {
                                SetupDate = OperationDate;
                                SetupHour = OperationHour;
                                // Setup بدست آوردن زمان شروع
                                GetNewDate(CalendarCode, ref SetupDate, ref SetupHour, Setup1 * -1);
                                PlanningStartDate = SetupDate;
                                PlanningStartHour = SetupHour;
                                break;
                            }

                            default:
                            {
                                SetupDate = 0.ToString();
                                SetupHour = 0d;
                                PlanningStartDate = OperationDate;
                                PlanningStartHour = OperationHour;
                                break;
                            }
                        }
                    }

                    break;
                }
            }
        }

        private string GetCalendarAccessibleTime(string v)
        {
            throw new NotImplementedException();
        }

        private int GetOperationExecMethod(string TreeCode, string OperationCode)
        {
            FunctionLabel.Text = Conversions.ToString(Operators.ConcatenateObject(FunctionLabel.Tag, " GetOperationExecMethod"));
            Application.DoEvents();

            // Try


            var drOPCs = Db.DataSet.Tables["Tbl_ProductOPCs"].Select("TreeCode=" + TreeCode + " And OperationCode='" + OperationCode + "'");
            return Conversions.ToInteger(drOPCs[0]["ExecutionMethod"]);
        }

        private double GetOperationLTTime(string OperationCode, EnumExecutionMethod ExecMethod, long ProductionQuantity, string MachineCode)
        {
            FunctionLabel.Text = Conversions.ToString(Operators.ConcatenateObject(FunctionLabel.Tag, " GetOperationLTTime"));
            Application.DoEvents();
            double LTTime = 0d;
            //double OneExecTime = 0d;

            // 3

            // Try


            LTTime = ProductionQuantity * GetOneOperationExecTime(OperationCode, ExecMethod, MachineCode);
            return LTTime;
        }

        private double GetOneOperationExecTime(string OperationCode, EnumExecutionMethod ExecMethod, string MachineCode)
        {
            FunctionLabel.Text = Conversions.ToString(Operators.ConcatenateObject(FunctionLabel.Tag, " GetOneOperationExecTime"));
            Application.DoEvents();
            double OneExecTime = 0d;
            DataRow[] drOPCs;
            string CalendarCode = GetCalendarCode(ExecMethod, OperationCode, MachineCode);

            // Try


            switch (ExecMethod)
            {
                case EnumExecutionMethod.EM_MACHINE:

                    drOPCs = Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + OperationCode + "' And MachineCode='" + MachineCode + "'");
                    OneExecTime = Module1.GetAnyTime_TO_Hour(Conversions.ToShort(drOPCs[0]["TimeType"]), Conversions.ToDouble(drOPCs[0]["OneExecutionTime"]), Module1.GetCalendarAccessibleTime(CalendarCode));
                    break;


                case EnumExecutionMethod.EM_NONMACHINE:

                    drOPCs = Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + OperationCode + "' And MachinePriority=0");
                    OneExecTime = Module1.GetAnyTime_TO_Hour(Conversions.ToShort(drOPCs[0]["TimeType"]), Conversions.ToDouble(drOPCs[0]["OneExecutionTime"]), Module1.GetCalendarAccessibleTime(CalendarCode));
                    break;


                case EnumExecutionMethod.EM_CONTRACTOR:

                    drOPCs = Db.DataSet.Tables["Tbl_ContractorOperations"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + OperationCode + "'");
                    OneExecTime = Module1.GetAnyTime_TO_Hour(Conversions.ToShort(drOPCs[0]["TimeType"]), Conversions.ToDouble(drOPCs[0]["TransferBatchExecutionTime"]), Module1.GetCalendarAccessibleTime(CalendarCode));
                    OneExecTime = Conversions.ToDouble(Operators.DivideObject(OneExecTime, Operators.MultiplyObject(drOPCs[0]["MinimumTransferBatch"], drOPCs[0]["BatchCapacity"])));
                    break;

            }

            return OneExecTime;
        }

        private int GetOperationMinQuantity(string OperationCode, EnumExecutionMethod ExecMethod, string MachineCode)
        {
            FunctionLabel.Text = Conversions.ToString(Operators.ConcatenateObject(FunctionLabel.Tag, " GetOperationMinQuantity"));
            Application.DoEvents();
            var MinQuantity = default(int);
            DataRow[] drMin;

            // Try


            switch (ExecMethod)
            {
                case EnumExecutionMethod.EM_MACHINE:
                case EnumExecutionMethod.EM_NONMACHINE:
                {
                    drMin = Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + OperationCode + "' And MachineCode='" + MachineCode + "'");
                    MinQuantity = Conversions.ToInteger(drMin[0]["MinimumProduction"]);
                    break;
                }

                case EnumExecutionMethod.EM_CONTRACTOR:
                {
                    drMin = Db.DataSet.Tables["Tbl_ContractorOperations"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + OperationCode + "'");
                    MinQuantity = Conversions.ToInteger(Operators.MultiplyObject(drMin[0]["MinimumTransferBatch"], drMin[0]["BatchCapacity"]));
                    break;
                }
            }

            return MinQuantity;
        }

        /// <summary>
        /// این تابع سرعت انجام یک فعالیت را محاسبه می کند
        /// </summary>
        private string GetOpExecutionSpeed(EnumExecutionMethod ExecutionMethod, string TreeCode, string OperationCode, string MachineCode = "-1")
        {
            if (this.LogPlanningProcedure) Logger.LogInfo($"GetOpExecutionSpeed({ExecutionMethod},{TreeCode},{OperationCode},{MachineCode})");
            FunctionLabel.Text = Conversions.ToString(Operators.ConcatenateObject(FunctionLabel.Tag, "GetOpExecutionSpeed"));
            Application.DoEvents();
            string OPExecSpeed = "0";
            DataRow[] drOpSpeed;

            // Try


            switch (ExecutionMethod)
            {
                case EnumExecutionMethod.EM_MACHINE:
                {
                    drOpSpeed = Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].Select("TreeCode=" + TreeCode + " And MachineCode='" + MachineCode + "' And OperationCode='" + OperationCode + "'");
                    var drMachine = Db.DataSet.Tables["Tbl_Machines"].Select("Code='" + MachineCode + "'");
                    OPExecSpeed = Module1.GetAnyTime_TO_Hour(Conversions.ToShort(drOpSpeed[0]["TimeType"]), Conversions.ToDouble(drOpSpeed[0]["OneExecutionTime"]), Module1.GetCalendarAccessibleTime(Conversions.ToString(drMachine[0]["CalendarCode"]))).ToString();
                    OPExecSpeed = (1d / Conversions.ToDouble(OPExecSpeed)).ToString();
                    break;
                }

                case EnumExecutionMethod.EM_NONMACHINE:
                {
                    drOpSpeed = Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].Select("TreeCode=" + TreeCode + " And OperationCode='" + OperationCode + "'");
                    OPExecSpeed = Module1.GetAnyTime_TO_Hour(Conversions.ToShort(drOpSpeed[0]["TimeType"]), Conversions.ToDouble(drOpSpeed[0]["OneExecutionTime"]), Module1.GetCalendarAccessibleTime(Conversions.ToString(drOpSpeed[0]["CalendarCode"]))).ToString();
                    OPExecSpeed = (1d / Conversions.ToDouble(OPExecSpeed)).ToString();
                    break;
                }

                case EnumExecutionMethod.EM_CONTRACTOR:
                {
                    drOpSpeed = Db.DataSet.Tables["Tbl_ContractorOperations"].Select("TreeCode=" + TreeCode + " And OperationCode='" + OperationCode + "'");
                    OPExecSpeed = Module1.GetAnyTime_TO_Hour(Conversions.ToShort(drOpSpeed[0]["TimeType"]), Conversions.ToDouble(drOpSpeed[0]["TransferBatchExecutionTime"]), Module1.GetCalendarAccessibleTime(Conversions.ToString(drOpSpeed[0]["CalendarCode"]))).ToString();
                    OPExecSpeed = Conversions.ToString(Operators.DivideObject(drOpSpeed[0]["BatchCapacity"], Conversions.ToDouble(OPExecSpeed)));
                    break;
                }
            }

            return OPExecSpeed;
        }

        /// <summary>
        /// این تابع بر اساس تاریخ جاری داده شده و تعداد روز مورد نظر
        /// با کنترل تمامی شرایط، تاریخ جدیدی را باز می گرداند
        /// </summary>
        private void GetNewDate(string CalendarCode, ref string CurrentDate, ref double CurrentHour, double Duration)
        {
            FunctionLabel.Text = Conversions.ToString(Operators.ConcatenateObject(FunctionLabel.Tag, "GetNewDate"));
            Application.DoEvents();

            if (Duration == 0)
            {
                return;
            }

            int CalcType = Conversions.ToInteger(Interaction.IIf(Duration >= 0d, DateCalcType_enum.DCT_ADD, DateCalcType_enum.DCT_SUB));
            if (CurrentDate == "0")
            {
                // MessageBox.Show("GetNewDate()" + Constants.vbCrLf + "خطا در یافتن تاریخ جدید: تاریخ جاری نامشخص است.");
                CommonTool.ShowMessage("GetNewDate: " + FunctionLabel.Tag + Constants.vbCrLf + "خطا در یافتن تاریخ جدید: تاریخ جاری نامشخص است.", MessageBoxButtons.OK);
                CurrentDate = Module1.mServerShamsiDate;
            }

            Duration = CheckFloating(Duration);
            CurrentHour = CheckFloating(CurrentHour);
            switch (CalcType)
            {
                case (int)DateCalcType_enum.DCT_ADD: // طول مدت داده شده، به تاریخ و زمان داده شده اضافه می شود
                {
                    AddDurationToDate(ref CurrentDate, ref CurrentHour, CalendarCode, Duration);
                    break;
                }

                case (int)DateCalcType_enum.DCT_SUB: // مدت داده شده از تاریخ داده شده کم می شود
                {
                    GetBackDurationFromDate(ref CurrentDate, ref CurrentHour, CalendarCode, Duration);
                    break;
                }
            }
        }

        private ShDateHour GetNewDateHour(int CalendarCode, string CurrentDate, double CurrentHour, double Duration)
        {
            var dh = new ShDateHour();
            double CurHour = CurrentHour;
            var CalcType = Duration >= 0 ? DateCalcType_enum.DCT_ADD : DateCalcType_enum.DCT_SUB;
            if (CurrentDate == "0")
            {
                //throw new Exception("GetNewDateHour: the CurrentDate is unknown.");
                CommonTool.ShowMessage("GetNewDateHour:" + Constants.vbCrLf + "خطا در یافتن تاریخ جدید: تاریخ جاری نامشخص است.", MessageBoxButtons.OK);
                CurrentDate = Module1.GetServerShamsiTime(); // Module1.mServerShamsiDate;
            }

            Duration = CheckFloating2(Duration);
            CurHour = CheckFloating2(CurHour);
            switch (CalcType)
            {
                case DateCalcType_enum.DCT_ADD: // طول مدت داده شده، به تاریخ و زمان داده شده اضافه می شود
                {
                    AddDurationToDate(ref CurrentDate, ref CurHour, CalendarCode.ToString(), Duration);
                    break;
                }

                case DateCalcType_enum.DCT_SUB: // مدت داده شده از تاریخ داده شده کم می شود
                {
                    GetBackDurationFromDate(ref CurrentDate, ref CurHour, CalendarCode.ToString(), Duration);
                    break;
                }
            }

            dh.ShDate = long.Parse(CurrentDate);
            dh.WhHour = CurHour;
            return dh;
        }

        private (long, double) GetEndDateHourTemp(int CalendarCode, string CurDate, double curHour, double Duration)
        {
            //var dh = new ShDateHour();
            //double CurHour = double.Parse(currentHour);
            var CalcType = Duration >= 0 ? DateCalcType_enum.DCT_ADD : DateCalcType_enum.DCT_SUB;
            if (CurDate == "0")
            {
                throw new Exception("GetEndDateHourTemp: the sDate is unknown.");
            }

            //Duration = CheckFloating2(Duration);
            //CurHour = CheckFloating2(CurHour);
            switch (CalcType)
            {
                case DateCalcType_enum.DCT_ADD: // طول مدت داده شده، به تاریخ و زمان داده شده اضافه می شود
                {
                    AddDurationToDate(ref CurDate, ref curHour, CalendarCode.ToString(), Duration);
                    break;
                }

                case DateCalcType_enum.DCT_SUB: // مدت داده شده از تاریخ داده شده کم می شود
                {
                    GetBackDurationFromDate(ref CurDate, ref curHour, CalendarCode.ToString(), Duration);
                    break;
                }
            }


            return (long.Parse(CurDate), curHour);
        }

        private (string, string) GetEndDateHour(int CalendarCode, string CurDate, string currentHour, double Duration)
        {
            if (this.LogPlanningProcedure) Logger.LogInfo($"GetEndDateHour(CalendarCode:{CalendarCode},CurDate:{CurDate},CurrentHour:{currentHour},Duration:{Duration})");

            double curHour = double.Parse(currentHour);
            var CalcType = Duration >= 0 ? DateCalcType_enum.DCT_ADD : DateCalcType_enum.DCT_SUB;
            if (CurDate == "0")
            {
                throw new Exception("GetEndDateHour: the sDate is unknown.");
            }

            switch (CalcType)
            {
                case DateCalcType_enum.DCT_ADD: // طول مدت داده شده، به تاریخ و زمان داده شده اضافه می شود
                {
                    AddDurationToDate(ref CurDate, ref curHour, CalendarCode.ToString(), Duration);
                    break;
                }

                case DateCalcType_enum.DCT_SUB: // مدت داده شده از تاریخ داده شده کم می شود
                {
                    GetBackDurationFromDate(ref CurDate, ref curHour, CalendarCode.ToString(), Duration);
                    break;
                }
            }

            if (this.LogPlanningProcedure) 
                Logger.LogInfo($"GetEndDateHour Returns CurDate:{CurDate},CurrentHour:{curHour}");

            return (CurDate, GetCompeleteHour(curHour));
        }
        private void AddDurationToDate(ref string CurrentDate, ref double CurrentHour, string CalendarCode, double Duration)
        {
            FunctionLabel.Text = Conversions.ToString(Operators.ConcatenateObject(FunctionLabel.Tag, " AddDurationToDate"));
            Application.DoEvents();

            if (this.LogPlanningProcedure) Logger.LogInfo($"AddDurationToDate(CurrentDate:{CurrentDate},CurrentHour:{CurrentHour},CalendarCode:{CalendarCode},Duration:{Duration})");

            var CurrentDT = CheckFloating(double.Parse(CurrentDate + GetCompeleteHour(CurrentHour)));
            Duration = CheckFloating(Duration);
            if (Duration == 0)
            {
                return;
            }
            // Try


            switch (CheckIsHoliday(CalendarCode, CurrentDate))
            {
                case true: // روز تعطیل می باشد
                {
                    CurrentDate = FarsiDateFunctions.AddToDate(CurrentDate, "00000001");
                    while (CheckIsHoliday(CalendarCode, CurrentDate))
                        CurrentDate = FarsiDateFunctions.AddToDate(CurrentDate, "00000001");
                    CurrentDT = GetShiftStartDT(CalendarCode, 1, CurrentDate);
                    CurrentDate = CurrentDT.ToString().Substring(0, 8);
                    CurrentHour = double.Parse(CurrentDT.ToString().Substring(8));
                    AddDurationToDate(ref CurrentDate, ref CurrentHour, CalendarCode, Duration);
                    break;
                }

                case false: // روز عادی می باشد
                {
                    int ShiftCount;
                    bool IsOverflowAdded = false;
                    int HourCount;
                    int HourCounter;
                    double AddedHour;
                    double ShiftStartDT;
                    //double ShiftEndDT1;
                    string mStartDate;
                    double OpCurrentDT;
                    double ShiftEndDT;

                    // بدست آوردن تعداد شیفتهای تقویم
                    ShiftCount = Conversions.ToInteger(Db.DataSet.Tables["Tbl_Calendar"].Select("CalendarCode=" + CalendarCode)[0]["ShiftCount"]);

                    for (int ShiftCounter = 1, loopTo = ShiftCount; ShiftCounter <= loopTo; ShiftCounter++)
                    {
                        var dtDownTimes = new DataTable();
                        HourCount = 0;
                        HourCounter = 0;
                        AddedHour = 0;

                        // بدست آوردن زمان شروع شیفت
                        ShiftStartDT = GetShiftStartDT(CalendarCode, ShiftCounter, CurrentDate);
                        // بدست آوردن زمان پایان شیفت
                        ShiftEndDT = GetShiftEndDT(CalendarCode, ShiftCounter, CurrentDate);
                        // بدست آوردن شروع و پایان زمان استراحت شیفت
                        dtDownTimes = GetShiftDownTimes(CalendarCode, ShiftCounter.ToString(), CurrentDate);
                        ShiftStartDT = CheckFloating(ShiftStartDT);
                        ShiftEndDT = CheckFloating(ShiftEndDT);
                        mStartDate = CurrentDate;
                        //if (ShiftStartDT > ShiftEndDT1)
                        //{
                        //    mStartDate = FarsiDateFunctions.AddToDate(mStartDate, "00000001");
                        //}

                        // بدست آوردن تعداد ساعات، در مدت زمان درخواستی برای افزایش زمان
                        if (Duration.ToString().Contains("."))
                        {
                            HourCount = Conversions.ToInteger(Strings.Mid(Duration.ToString(), 1, Duration.ToString().IndexOf(".")));
                        }
                        else
                        {
                            HourCount = (int)Math.Round(Duration);
                        }

                        HourCount = HourCount + ((Duration - (double)HourCount) == 0.0d ? 0 : 1);
                        //var loopTo1 = HourCount;
                        for (HourCounter = 1; HourCounter <= HourCount; HourCounter++)
                        {
                            if (HourCounter == HourCount)
                            {
                                AddedHour = Duration;
                            }
                            else
                            {
                                AddedHour = 1.0d;
                            }

                            // کنترل اینکه ساعت محاسبه شده در وقت های استراحت نباشد
                            foreach (DataRow r in dtDownTimes.Rows)
                            {
                                double DT_Start = Conversions.ToDouble(Module1.GetFloatingHour(r["DownTimeStart"].ToString()));
                                double DT_End = Conversions.ToDouble(Module1.GetFloatingHour(r["DownTimeEnd"].ToString()));
                                DT_Start = CheckFloating(DT_Start);
                                DT_End = CheckFloating(DT_End);
                                OpCurrentDT = AddDT_Hours(CurrentDT, AddedHour);
                                double DownStartDT = double.Parse(mStartDate + GetCompeleteHour(DT_Start));
                                double DownEndDT = double.Parse(mStartDate + GetCompeleteHour(DT_End));
                                // Op current time is before down time or already passed the down time. 
                                // No confilict with down time.
                                //if ( (OpCurrentDT <= DownStartDT && OpCurrentDT < DownEndDT) || (OpCurrentDT >= DownStartDT && OpCurrentDT >= DownEndDT))
                                //{
                                // it is ok. no confilict with down time.
                                //    continue;
                                //}

                                // current time is between down times.
                                if (DownStartDT < OpCurrentDT && OpCurrentDT <= DownEndDT)
                                {

                                    Duration -=  (DownStartDT - CurrentDT);
                                    AddedHour -= (DownStartDT - CurrentDT);
                                    CurrentDT = DownEndDT; // + Module1.GetAnyTime_TO_Hour((short)Module1.TimeTypes_enum.TT_MINUTE, 1d, "00:00");
                                    if (AddedHour == 0.0d)
                                    {
                                        goto NextFunctionCall;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                }

                                // the down time is between curent time and current time mines Addedhour 
                                var checkDT = OpCurrentDT;
                                if (AddDT_Hours(checkDT, (-1) * AddedHour) <= DownStartDT && DownEndDT <= OpCurrentDT)
                                {
                                    Duration = Duration + (DownEndDT - DownStartDT);
                                }

                            }

                            //OpCurrentDT = double.Parse(GetCompeleteHour(CurrentDT + AddedHour));
                            OpCurrentDT = AddDT_Hours(CurrentDT, AddedHour);

                            //ShiftEndDT = double.Parse(mStartDate + GetCompeleteHour(ShiftEndDT1));
                            if (OpCurrentDT < ShiftEndDT)
                            {
                                CurrentDT = AddDT_Hours(CurrentDT, AddedHour);
                                Duration -= AddedHour;
                            }
                            //else if ((CurrentDate + GetCompeleteHour(CurrentDT + AddedHour) ?? "") == (mStartDate + GetCompeleteHour(ShiftEndDT1) ?? ""))
                            else if (OpCurrentDT == ShiftEndDT)
                            {
                                Duration -= AddedHour;
                                if (ShiftCounter == ShiftCount)
                                {
                                    if (Duration == 0.0d)
                                    {
                                        CurrentDT = ShiftEndDT;
                                    }
                                    else if (!IsOverflowAdded) // در صورتیکه شیفت آخر از روز جاری رد نشده باشد
                                    {
                                        CurrentDate = FarsiDateFunctions.AddToDate(CurrentDate, "00000001");
                                        while (CheckIsHoliday(CalendarCode, CurrentDate))
                                            CurrentDate = FarsiDateFunctions.AddToDate(CurrentDate, "00000001");
                                        CurrentDT = GetShiftStartDT(CalendarCode, 1, CurrentDate);
                                    }
                                    else // در صورتیکه شیفت آخر از روز جاری رد شده باشد
                                    {
                                        if (CheckIsHoliday(CalendarCode, CurrentDate))
                                        {
                                            // روز محاسبه شده تعطیل باشد
                                            CurrentDate = FarsiDateFunctions.AddToDate(CurrentDate, "00000001");
                                            while (CheckIsHoliday(CalendarCode, CurrentDate))
                                                CurrentDate = FarsiDateFunctions.AddToDate(CurrentDate, "00000001");
                                        }

                                        CurrentDT = GetShiftStartDT(CalendarCode, 1, CurrentDate);
                                    }
                                }
                                else if (Duration == 0.0d)
                                {
                                    CurrentDT = ShiftEndDT;
                                }
                                else
                                {
                                    CurrentDT = GetShiftStartDT(CalendarCode, ShiftCounter + 1, CurrentDate);
                                }

                                break;
                            }
                            else
                            {
                                if (ShiftEndDT - CurrentDT > 0.0d)
                                {
                                    Duration -= ShiftEndDT - CurrentDT;
                                }

                                if (Duration > 0.0d)
                                {
                                    if (ShiftCounter == ShiftCount)
                                    {
                                        CurrentDate = FarsiDateFunctions.AddToDate(CurrentDate, "00000001");
                                        while (CheckIsHoliday(CalendarCode, CurrentDate))
                                            CurrentDate = FarsiDateFunctions.AddToDate(CurrentDate, "00000001");
                                        CurrentDT = GetShiftStartDT(CalendarCode, 1, CurrentDate);
                                    }
                                    else
                                    {
                                        double NextShiftStartDT = GetShiftStartDT(CalendarCode, ShiftCounter + 1, CurrentDate);
                                        if (NextShiftStartDT - CurrentDT > 0.0d)
                                        {
                                            Duration -= NextShiftStartDT - CurrentDT;
                                            CurrentDT = NextShiftStartDT;
                                        }
                                    }
                                }

                                break;
                            }
                        }

                        if (Duration <= 0.0d)
                        {
                            CurrentDate = CurrentDT.ToString().Substring(0, 8);
                            CurrentHour = double.Parse(CurrentDT.ToString().Substring(8));
                            break;
                            //return;
                        }
                    }

                    NextFunctionCall:
                    ;
                    if (Duration <= 0.0d)
                    {
                        CurrentDate = CurrentDT.ToString().Substring(0, 8);
                        CurrentHour = double.Parse(CurrentDT.ToString().Substring(8));
                        break; // return;
                    }
                    else
                    {
                        CurrentDate = CurrentDT.ToString().Substring(0, 8);
                        CurrentHour = double.Parse(CurrentDT.ToString().Substring(8));
                        AddDurationToDate(ref CurrentDate, ref CurrentHour, CalendarCode, Duration);
                    }

                    break;
                }
            }

            // update current Date and current Time:
            
            //  if(CurrentDT > 24)
            //  {
            //      Logger.SaveError(FunctionLabel.Tag.ToString(), $"CurrentDate = {CurrentDate} , CurrentDT = {CurrentDT} , Duration = {Duration}");
            // }
        }

        private double AddDT_Hours(double CurrentDT, double Hours)
        {
            try
            {

           
            double NewDT = CurrentDT;
            string CurDTStr = CurrentDT.ToString();
            
            string CurrentDate = CurDTStr.Substring(0, 8);
           
            string CurrentHour = CurrentDT.ToString().Substring(8, CurrentDT.ToString().Length - 8);
            double NewHour = double.Parse(CurrentHour) + Hours;
            if (NewHour > 24d)
            {
                NewHour -= 24d;
                CurrentDate = FarsiDateFunctions.AddToDate(CurrentDate, "00000001");
            }
            if (NewHour < 0d)
            {
                NewHour += 24d;
                CurrentDate = FarsiDateFunctions.SubDayFromDate(CurrentDate, "00000001");
            }

            NewDT = CheckFloating(double.Parse(CurrentDate + GetCompeleteHour(NewHour)));
            return NewDT;
            }
            catch (Exception exp)
            {
                Logger.LogException("AddDT_Hours", exp   );
                Logger.LogInfo($"CurrentDT = {CurrentDT.ToString()} , Hours = {Hours.ToString()}");
                // throw;
                return 0;
            }
        }

        private void GetBackDurationFromDate(ref string CurrentDate, ref double CurrentHour, string CalendarCode, double Duration)
        {
            if (this.LogPlanningProcedure) Logger.LogInfo($"GetBackDurationFromDate(CurrentDate:{CurrentDate},CurrentHour:{CurrentHour},CalendarCode:{CalendarCode},Duration:{Duration})");

            FunctionLabel.Text = Conversions.ToString(Operators.AddObject(FunctionLabel.Tag, " BackwardDurationFromDate"));
            Application.DoEvents();
            int Counter = 0;
            while (Counter < 5000 & Duration < 0d)
            {
                Duration = BackwardFindDateTime(ref CurrentDate, ref CurrentHour, CalendarCode, Duration);
                Counter += 1;
            }
        }

        private double BackwardFindDateTime(ref string CurrentDate, ref double CurrentHour, string CalendarCode, double Duration)
        {
            // ======================================================
            string SlotStart_str = "";
            string SlotEnd_str = "";
            double DownTimeStart;
            double DownTimeEnd;
            string DownTimeStart_str = "";
            string DowntimeEnd_str = "";
            int ShiftCount;
            int HourCount;
            double SlotHour;
            double ShiftStart;
            string ShiftStart_str;
            double ShiftEnd;
            string ShiftEnd_str;
            FunctionLabel.Text = Conversions.ToString(Operators.AddObject(FunctionLabel.Tag, " SubDurationFromDate"));
            Application.DoEvents();

            // CheckFloating(CurrentDT)
            // CheckFloating(Duration)

            switch (CheckIsHoliday(CalendarCode, CurrentDate))
            {
                case true: // روز تعطیل می باشد
                {
                    CurrentDate = FarsiDateFunctions.SubDayFromDate(CurrentDate, "00000001");
                    while (CheckIsHoliday(CalendarCode, CurrentDate))
                        CurrentDate = FarsiDateFunctions.SubDayFromDate(CurrentDate, "00000001");
                    ShiftCount = Conversions.ToInteger(Db.DataSet.Tables["Tbl_Calendar"].Select("CalendarCode=" + CalendarCode)[0]["ShiftCount"]);
                    CurrentHour = GetShiftEndDT(CalendarCode, ShiftCount, CurrentDate);
                    return Duration;
                }

                case false: // روز عادی می باشد
                {
                    // بدست آوردن تعداد شیفتهای تقویم
                    ShiftCount = Conversions.ToInteger(Db.DataSet.Tables["Tbl_Calendar"].Select("CalendarCode=" + CalendarCode)[0]["ShiftCount"]);
                    for (int ShiftCounter = ShiftCount; ShiftCounter >= 1; ShiftCounter -= 1)
                    {
                        var dtDownTimes = new DataTable();

                        // بدست آوردن زمان شروع شیفت
                        ShiftStart = GetShiftStartDT(CalendarCode, ShiftCounter, CurrentDate);
                        // بدست آوردن زمان پایان شیفت
                        ShiftEnd = GetShiftEndDT(CalendarCode, ShiftCounter, CurrentDate);

                        // بدست آوردن شروع و پایان زمان های استراحت شیفت
                        dtDownTimes = GetShiftDownTimes(CalendarCode, ShiftCounter.ToString(), CurrentDate);

                        // CheckFloating(ShiftStartDT)
                        // CheckFloating(ShiftEndDT1)

                        // بدست آوردن تعداد ساعات، در مدت زمان درخواستی برای کاهش زمان
                        if (Duration.ToString().Contains("."))
                        {
                            HourCount = Conversions.ToInteger(Strings.Mid(Duration.ToString(), 1, Duration.ToString().IndexOf(".")));
                        }
                        else
                        {
                            HourCount = (int)Math.Round(Duration);
                        }

                        HourCount *= -1;
                        HourCount = HourCount + (Duration + HourCount) == 0d ? 0 : 1;
                        for (int HourCounter = 1, loopTo = HourCount; HourCounter <= loopTo; HourCounter++)
                        {
                            if (HourCounter == HourCount)
                            {
                                SlotHour = Duration * -1.0d;
                            }
                            else
                            {
                                SlotHour = 1.0d;
                            }

                            // کنترل اینکه ساعت محاسبه شده در وقت های استراحت نباشد
                            foreach (DataRow r in dtDownTimes.Rows)
                            {
                                DownTimeStart = Conversions.ToDouble(Module1.GetFloatingHour(r["DownTimeStart"].ToString()));
                                DownTimeEnd = Conversions.ToDouble(Module1.GetFloatingHour(r["DownTimeEnd"].ToString()));
                                DownTimeStart = CheckFloating(DownTimeStart);
                                DownTimeEnd = CheckFloating(DownTimeEnd);
                                SlotStart_str = CurrentDate + GetCompeleteHour(CurrentHour - SlotHour);
                                SlotEnd_str = CurrentDate + GetCompeleteHour(CurrentHour);
                                DownTimeStart_str = CurrentDate + GetCompeleteHour(DownTimeStart);
                                DowntimeEnd_str = GetCompareDate(CurrentDate, ShiftEnd, DownTimeEnd) + GetCompeleteHour(DownTimeEnd);
                                if (Operators.CompareString(SlotStart_str, DowntimeEnd_str, false) < 0 & Operators.CompareString(SlotEnd_str, DownTimeStart_str, false) >= 0)
                                {
                                    Duration += CurrentHour - DownTimeEnd;
                                    SlotHour -= CurrentHour - DownTimeEnd;
                                    CurrentHour = DownTimeStart; // - GetAnyTime_TO_Hour(TimeTypes_enum.TT_MINUTE, 1, "0:0")
                                    if (SlotHour == 0.0d)
                                    {
                                        return Duration;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }

                            SlotStart_str = CurrentDate + GetCompeleteHour(CurrentHour - SlotHour);
                            ShiftStart_str = GetCompareDate(CurrentDate, ShiftEnd, ShiftStart) + GetCompeleteHour(ShiftStart);
                            if (Operators.CompareString(SlotStart_str, ShiftStart_str, false) < 0)
                            {
                                if (CurrentHour - ShiftStart > 0.0d)
                                {
                                    Duration += CurrentHour - ShiftStart;
                                    CurrentHour = ShiftStart;
                                    if (Duration >= 0.0d)
                                    {
                                        return Duration;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                if (Duration < 0.0d)
                                {
                                    if (ShiftCounter == 1) // شیفت اول می باشد
                                    {
                                        double mEnd = GetShiftEndDT(CalendarCode, ShiftCount, CurrentDate);
                                        double mStart = GetShiftStartDT(CalendarCode, ShiftCount, CurrentDate);
                                        if (mStart < mEnd)
                                        {
                                            CurrentDate = FarsiDateFunctions.SubDayFromDate(CurrentDate, "00000001");
                                            while (CheckIsHoliday(CalendarCode, CurrentDate))
                                                CurrentDate = FarsiDateFunctions.SubDayFromDate(CurrentDate, "00000001");
                                            mEnd = GetShiftEndDT(CalendarCode, ShiftCount, CurrentDate);
                                        }

                                        // Dim mStart As Double = GetShiftStartDT(CalendarCode, ShiftCount, CurrentDate)

                                        if (CurrentHour >= mEnd)
                                        {
                                            Duration += CurrentHour - mEnd;
                                        }

                                        CurrentHour = mEnd;
                                        break;
                                    }
                                    else // غیر از شیفت اول می باشد
                                    {
                                        break;
                                    }
                                }
                            }
                            else if ((SlotStart_str ?? "") == (DownTimeStart_str ?? ""))
                            {
                                CurrentHour = ShiftStart;
                                Duration += SlotHour;
                            }
                            else
                            {
                                ShiftEnd_str = CurrentDate + GetCompeleteHour(ShiftEnd);
                                if (Operators.CompareString(SlotStart_str, ShiftEnd_str, false) <= 0)
                                {
                                    if (CurrentHour > ShiftEnd)
                                    {
                                        CurrentHour = ShiftEnd;
                                        break;
                                    }

                                    if (CurrentHour - SlotHour < 0.0d)
                                    {
                                        CurrentHour = 24.0d - CurrentHour;
                                        CurrentDate = FarsiDateFunctions.SubDayFromDate(CurrentDate, "00000001");
                                    }
                                    else if (CurrentHour - SlotHour == 0.0d)
                                    {
                                        if (Duration + SlotHour == 0.0d)
                                        {
                                            CurrentHour = Conversions.ToDouble(Module1.GetFloatingHour("00:01"));
                                        }
                                        else
                                        {
                                            CurrentHour = Conversions.ToDouble(Module1.GetFloatingHour("23:59"));
                                            CurrentDate = FarsiDateFunctions.SubDayFromDate(CurrentDate, "00000001");
                                        }
                                    }
                                    else
                                    {
                                        CurrentHour -= SlotHour;
                                    }

                                    Duration += SlotHour;
                                    if (Duration >= 0.0d)
                                    {
                                        return Duration;
                                    }
                                }
                                else
                                {
                                    if (ShiftCounter == 1) // شیفت اول می باشد
                                    {
                                        CurrentHour = GetShiftEndDT(CalendarCode, ShiftCount, CurrentDate);
                                    }
                                    else
                                    {
                                        // CurrentDT = GetShiftEndDT(CalendarCode, ShiftCounter - 1, CurrentDate)
                                    } // غیر از شیفت اول می باشد

                                    break;
                                }
                            }
                        }

                        if (Duration >= 0.0d)
                        {
                            return Duration;
                        }
                    }

                    break;
                }

                // NextFuctionCall:

            }

            return Duration;
        }

        private void SubDurationFromDateNew(ref string CurrentDate, ref double CurrentHour, string CalendarCode, double Duration)
        {
            // TODO: This function HAS PROBLEM. THE PROBLEM IS IT CANNOT RECOGNISE THE DOWN TIME WHEN 
            // DOWN TIME Is LESS THAN ONE HOUR, I THINK AS EACH SLOT IN CALENDAR IS 5 MINUTE , 
            // SO THE DURATION SHOULD REDUCED BY 5 MINUTES Not ONE HOUR.
            // CHECK THE OTHER FUNCTIONS TO SEE HOW THEY DEAL WITH DOWN TIMES
            FunctionLabel.Text = Conversions.ToString(Operators.AddObject(FunctionLabel.Tag, " SubDurationFromDateNew"));
            Application.DoEvents();
            // Try


            CurrentHour = CheckFloating(CurrentHour);
            Duration = CheckFloating(Duration);
            switch (CheckIsHoliday(CalendarCode, CurrentDate))
            {
                case true: // روز تعطیل می باشد
                {
                    CurrentDate = FarsiDateFunctions.SubDayFromDate(CurrentDate, "00000001");
                    while (CheckIsHoliday(CalendarCode, CurrentDate))
                        CurrentDate = FarsiDateFunctions.SubDayFromDate(CurrentDate, "00000001");
                    int ShiftCount = Conversions.ToInteger(Db.DataSet.Tables["Tbl_Calendar"].Select("CalendarCode=" + CalendarCode)[0]["ShiftCount"]);
                    CurrentHour = GetShiftEndDT(CalendarCode, ShiftCount, CurrentDate);
                    GetBackDurationFromDate(ref CurrentDate, ref CurrentHour, CalendarCode, Duration);
                    break;
                }

                case false: // روز عادی می باشد
                {
                    // بدست آوردن تعداد شیفتهای تقویم
                    int ShiftCount = Conversions.ToInteger(Db.DataSet.Tables["Tbl_Calendar"].Select("CalendarCode=" + CalendarCode)[0]["ShiftCount"]);
                    for (int ShiftCounter = ShiftCount; ShiftCounter >= 1; ShiftCounter -= 1)
                    {
                        var dtDownTimes = new DataTable();
                        int HourCount;
                        double SubedHour;
                        // بدست آوردن زمان شروع شیفت
                        double ShiftStartTime = GetShiftStartDT(CalendarCode, ShiftCounter, CurrentDate);
                        // بدست آوردن زمان پایان شیفت
                        double ShiftEndTime = GetShiftEndDT(CalendarCode, ShiftCounter, CurrentDate);

                        // بدست آوردن شروع و پایان زمان های استراحت شیفت
                        dtDownTimes = GetShiftDownTimes(CalendarCode, ShiftCounter.ToString(), CurrentDate);
                        ShiftStartTime = CheckFloating(ShiftStartTime);
                        ShiftEndTime = CheckFloating(ShiftEndTime);

                        // بدست آوردن تعداد ساعات، در مدت زمان درخواستی برای کاهش زمان
                        if (Duration.ToString().Contains("."))
                        {
                            HourCount = Conversions.ToInteger(Strings.Mid(Duration.ToString(), 1, Duration.ToString().IndexOf(".")));
                        }
                        else
                        {
                            HourCount = (int)Math.Round(Duration);
                        }

                        HourCount *= -1;
                        HourCount = HourCount + (Duration + (double)HourCount) == 0d ? 0 : 1;
                        for (int HourCounter = 1, loopTo = HourCount; HourCounter <= loopTo; HourCounter++)
                        {
                            if (HourCounter == HourCount)
                            {
                                SubedHour = Duration * -1.0d;
                            }
                            else
                            {
                                SubedHour = 1.0d;
                            }

                            // کنترل اینکه ساعت محاسبه شده در وقت های استراحت نباشد
                            foreach (DataRow r in dtDownTimes.Rows)
                            {
                                double DT_Start = Conversions.ToDouble(Module1.GetFloatingHour(r["DownTimeStart"].ToString()));
                                double DT_End = Conversions.ToDouble(Module1.GetFloatingHour(r["DownTimeEnd"].ToString()));
                                DT_Start = CheckFloating(DT_Start);
                                DT_End = CheckFloating(DT_End);
                                if (Operators.CompareString(CurrentDate + GetCompeleteHour(CurrentHour - SubedHour), GetCompareDate(CurrentDate, ShiftEndTime, DT_End) + GetCompeleteHour(DT_End), false) <= 0)
                                {
                                    if (Operators.CompareString(CurrentDate + GetCompeleteHour(CurrentHour - SubedHour), GetCompareDate(CurrentDate, ShiftEndTime, DT_Start) + GetCompeleteHour(DT_Start), false) >= 0)
                                    {
                                        Duration += CurrentHour - DT_End;
                                        SubedHour -= CurrentHour - DT_End;
                                        CurrentHour = DT_Start - Module1.GetAnyTime_TO_Hour((short)Module1.TimeTypes_enum.TT_MINUTE, 1d, "0:0");
                                        if (SubedHour == 0.0d)
                                        {
                                            goto NextFuctionCall;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }

                            if (Operators.CompareString(CurrentDate + GetCompeleteHour(CurrentHour - SubedHour), GetCompareDate(CurrentDate, ShiftEndTime, ShiftStartTime) + GetCompeleteHour(ShiftStartTime), false) < 0)
                            {
                                if (CurrentHour - ShiftStartTime > 0.0d)
                                {
                                    Duration += CurrentHour - ShiftStartTime;
                                    CurrentHour = ShiftStartTime;
                                    if (Duration >= 0.0d)
                                    {
                                        return;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                if (Duration < 0.0d)
                                {
                                    if (ShiftCounter == 1) // شیفت اول می باشد
                                    {
                                        double mEnd = GetShiftEndDT(CalendarCode, ShiftCount, CurrentDate);
                                        double mStart = GetShiftStartDT(CalendarCode, ShiftCount, CurrentDate);
                                        if (mStart < mEnd)
                                        {
                                            CurrentDate = FarsiDateFunctions.SubDayFromDate(CurrentDate, "00000001");
                                            while (CheckIsHoliday(CalendarCode, CurrentDate))
                                                CurrentDate = FarsiDateFunctions.SubDayFromDate(CurrentDate, "00000001");
                                            mEnd = GetShiftEndDT(CalendarCode, ShiftCount, CurrentDate);
                                        }

                                        // Dim mStart As Double = GetShiftStartDT(CalendarCode, ShiftCount, CurrentDate)

                                        if (CurrentHour >= mEnd)
                                        {
                                            Duration += CurrentHour - mEnd;
                                        }

                                        CurrentHour = mEnd;
                                        break;
                                    }
                                    else // غیر از شیفت اول می باشد
                                    {
                                        // CurrentDT = mShiftEnd
                                        // If (CurrentDT - SubedHour) < 0.0 Then
                                        // CurrentDT = 24.0 - CurrentDT
                                        // CurrentDate = FarsiDateFunctions.SubDayFromDate(CurrentDate, "00000001")

                                        // Exit For
                                        // ElseIf (CurrentDT - SubedHour) = 0.0 Then
                                        // If (Duration + SubedHour) = 0.0 Then
                                        // CurrentDT = GetFloatingHour("00:01")
                                        // Else
                                        // CurrentDT = GetFloatingHour("23:59")
                                        // CurrentDate = FarsiDateFunctions.SubDayFromDate(CurrentDate, "00000001")
                                        // End If

                                        // Exit For
                                        // Else
                                        // CurrentDT -= SubedHour
                                        // End If

                                        // Duration += SubedHour

                                        // If Duration >= 0.0 Then
                                        // Exit Sub
                                        // End If

                                        break;
                                    }
                                }
                            }
                            else if ((CurrentDate + GetCompeleteHour(CurrentHour - SubedHour) ?? "") == (GetCompareDate(CurrentDate, ShiftEndTime, ShiftStartTime) + GetCompeleteHour(ShiftStartTime) ?? ""))
                            {
                                CurrentHour = ShiftStartTime;
                                Duration += SubedHour;
                            }
                            else if (Operators.CompareString(CurrentDate + GetCompeleteHour(CurrentHour - SubedHour), CurrentDate + GetCompeleteHour(ShiftEndTime), false) <= 0)
                            {
                                if (CurrentHour > ShiftEndTime)
                                {
                                    CurrentHour = ShiftEndTime;
                                    break;
                                }

                                if (CurrentHour - SubedHour < 0.0d)
                                {
                                    CurrentHour = 24.0d - CurrentHour;
                                    CurrentDate = FarsiDateFunctions.SubDayFromDate(CurrentDate, "00000001");
                                }
                                else if (CurrentHour - SubedHour == 0.0d)
                                {
                                    if (Duration + SubedHour == 0.0d)
                                    {
                                        CurrentHour = Conversions.ToDouble(Module1.GetFloatingHour("00:01"));
                                    }
                                    else
                                    {
                                        CurrentHour = Conversions.ToDouble(Module1.GetFloatingHour("23:59"));
                                        CurrentDate = FarsiDateFunctions.SubDayFromDate(CurrentDate, "00000001");
                                    }
                                }
                                else
                                {
                                    CurrentHour -= SubedHour;
                                }

                                Duration += SubedHour;
                                if (Duration >= 0.0d)
                                {
                                    return;
                                }
                            }
                            else
                            {
                                // CurrentDT = ShiftEndDT1
                                // Duration += (CurrentDT - ShiftEndDT1)

                                if (ShiftCounter == 1) // شیفت اول می باشد
                                {
                                    // CurrentDate = FarsiDateFunctions.SubDayFromDate(CurrentDate, "00000001")

                                    // While CheckIsHoliday(CalendarCode, CurrentDate)
                                    // CurrentDate = FarsiDateFunctions.SubDayFromDate(CurrentDate, "00000001")
                                    // End While

                                    // Dim mEnd As Double = GetShiftEndDT(CalendarCode, ShiftCount, CurrentDate)
                                    // Dim mStart As Double = GetShiftStartDT(CalendarCode, ShiftCount, CurrentDate)

                                    CurrentHour = GetShiftEndDT(CalendarCode, ShiftCount, CurrentDate);
                                }
                                else
                                {
                                    // CurrentDT = GetShiftEndDT(CalendarCode, ShiftCounter - 1, CurrentDate)
                                } // غیر از شیفت اول می باشد

                                break;
                            }
                        }

                        if (Duration >= 0.0d)
                        {
                            return;
                        }
                    }

                    NextFuctionCall:
                    ;
                    if (Duration >= 0.0d)
                    {
                        return;
                    }
                    else
                    {
                        GetBackDurationFromDate(ref CurrentDate, ref CurrentHour, CalendarCode, Duration);
                    }

                    break;
                }
            }
        }

        private string GetCompareDate(string mDate, double mBaseTime, double mCompareTime)
        {
            if (mCompareTime > mBaseTime)
            {
                mDate = FarsiDateFunctions.SubDayFromDate(mDate, "00000001");
            }

            return mDate;
        }


        private DataTable GetShiftDownTimes(string CalendarCode, string ShiftNo, string ShamsiDate)
        {
            if (this.LogPlanningProcedure) Logger.LogInfo($"GetShiftDownTimes(CalendarCode:{CalendarCode}, ShiftNo:{ShiftNo}, ShamsiDate: {ShamsiDate})");

            DataTable myDataTable;
            myDataTable = new DataTable();



            switch (IsParticularDay(CalendarCode, ShamsiDate))
            {
                case -1: // تاریخ داده یک روز پیش فرض می باشد
                {
                    Myds_Calender.Tables["Tbl_CalendarShiftDownTimes"].DefaultView.RowFilter = "CalendarCode=" + CalendarCode + " And ShiftNo=" + ShiftNo; ;
                    myDataTable = Myds_Calender.Tables["Tbl_CalendarShiftDownTimes"];
                    break;
                }

                default: // تاریخ داده شده یک روز خاص می باشد
                {
                    Myds_Calender.Tables["Tbl_ParticularShiftDownTimes"].DefaultView.RowFilter = "CalendarCode=" + CalendarCode + " And ShamsiDate=" + ShamsiDate + " And ShiftNo=" + ShiftNo; ;
                    myDataTable = Myds_Calender.Tables["Tbl_ParticularShiftDownTimes"];

                    break;
                }
            }


            return myDataTable;

        }

        /// <summary>
        /// این تابع کد تقویم را در عملیاتهایی که با ماشین یا
        /// بوسیله اپراتور و بدون ماشین انجام می شود را باز می گرداند
        /// </summary>
        private string GetCalendarCode(EnumExecutionMethod ExecutionMethod, string OperationCode, string MachineCode = "-1")
        {
            object MyCalendarcode = "1";
            DataView dvGetCalendarCode = null;

            switch (ExecutionMethod)
            {
                case EnumExecutionMethod.EM_MACHINE:
                {
                    Db.DataSet.Tables["Tbl_Machines"].DefaultView.RowFilter = "Code='" + MachineCode + "'";
                    dvGetCalendarCode = Db.DataSet.Tables["Tbl_Machines"].DefaultView;
                    break;
                }

                case EnumExecutionMethod.EM_NONMACHINE:
                {
                    Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].DefaultView.RowFilter = "TreeCode=" + mTreeCode + " And OperationCode='" + OperationCode + "'";
                    dvGetCalendarCode = Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].DefaultView;
                    break;
                }

                case EnumExecutionMethod.EM_CONTRACTOR:
                {
                    Db.DataSet.Tables["Tbl_ContractorOperations"].DefaultView.RowFilter = "TreeCode=" + mTreeCode + " And OperationCode='" + OperationCode + "'";
                    dvGetCalendarCode = Db.DataSet.Tables["Tbl_ContractorOperations"].DefaultView;
                    break;
                }
            }

            if (dvGetCalendarCode is null | dvGetCalendarCode.Count == 0)
            {
                MessageBox.Show(" تقویم یافت نشد " + OperationCode + " برای عملیات ", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                throw new Exception("Cannot find calendar for operation " + OperationCode);
            }

            MyCalendarcode = dvGetCalendarCode[0]["CalendarCode"];
            return Conversions.ToString(MyCalendarcode);
        }

        /// <summary>
        /// این تابع زمان شروع تقویم را محاسبه می کند
        /// </summary>
        private string GetCalendarStart(string CalendarCode)
        {
            if (this.LogPlanningProcedure) Logger.LogInfo($"GetCalendarStart(CalendarCode:{CalendarCode})");

            string ShiftStart = "00:00";

            Myds_Calender.Tables["Tbl_CalendarShifts"].DefaultView.RowFilter = "CalendarCode=" + CalendarCode + " And ShiftNo= 1" ;
            if (Myds_Calender.Tables["Tbl_CalendarShifts"].DefaultView.Count > 0)
            {
                ShiftStart = Myds_Calender.Tables["Tbl_CalendarShifts"].DefaultView[0]["ShiftStart"].ToString();
            }

            return Module1.GetFloatingHour(ShiftStart);
        }

        /// <summary>
        /// این تابع زمان پایان تقویم را محاسبه می کند
        /// </summary>
        private string GetCalendarEnd(string CalendarCode)
        {
            // FunctionLabel.Text = "GetCalendarEnd"
            // Application.DoEvents()

            var CalendarEnd = default(double);
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cm = new SqlCommand("Select ShiftStart,ShiftDuration,ShiftExtraTime From Tbl_CalendarShifts Where CalendarCode=" + CalendarCode + " Order By ShiftNo Desc", cn);
                var dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    var mCalendarEndTime = TimeSpan.Parse(dr["ShiftStart"].ToString()) + TimeSpan.Parse(dr["ShiftDuration"].ToString()) + TimeSpan.Parse(dr["ShiftExtraTime"].ToString());
                    if (mCalendarEndTime > TimeSpan.Parse("1.00:00"))
                    {
                        mCalendarEndTime = mCalendarEndTime - TimeSpan.Parse("1.00:00");
                    }

                    if (mCalendarEndTime == TimeSpan.Parse("1.00:00"))
                    {
                        mCalendarEndTime = TimeSpan.Parse("23:59:59");
                    }

                    string mHour = Conversions.ToString(Interaction.IIf(mCalendarEndTime.Hours < 10, "0" + mCalendarEndTime.Hours.ToString(), mCalendarEndTime.Hours.ToString()));
                    string mMinute = Conversions.ToString(Interaction.IIf(mCalendarEndTime.Minutes < 10, "0" + mCalendarEndTime.Minutes.ToString(), mCalendarEndTime.Minutes.ToString()));
                    CalendarEnd = Conversions.ToDouble(Module1.GetFloatingHour(mHour + ":" + mMinute));
                }

                dr.Close();
                cn.Close();
            }

            return CalendarEnd.ToString();
        }
     
        private double GetShiftStartDT(string CalendarCode, int ShiftNo, string ShamsiDate)
        {
            if (this.LogPlanningProcedure) Logger.LogInfo($"GetShiftStartDT(CalendarCode:{CalendarCode}, ShiftNo:{ShiftNo}, ShamsiDate: {ShamsiDate})");



            double ShiftStart = 0d;
            switch (IsParticularDay(CalendarCode, ShamsiDate))
            {
                case -1: // تاریخ داده یک روز پیش فرض می باشد
                {
                    Myds_Calender.Tables["Tbl_CalendarShifts"].DefaultView.RowFilter = "CalendarCode=" + CalendarCode + " And ShiftNo=" + ShiftNo;
                    if (Myds_Calender.Tables["Tbl_CalendarShifts"].DefaultView.Count > 0)
                    {
                        ShiftStart = Conversions.ToDouble(Module1.GetFloatingHour(Conversions.ToString(Myds_Calender.Tables["Tbl_CalendarShifts"].DefaultView[0]["ShiftStart"])));
                    }

                    break;
                }

                case 2: // تاریخ داده شده یک روز خاص عادی می باشد
                {
                    Myds_Calender.Tables["Tbl_CalendarParticularShifts"].DefaultView.RowFilter = "CalendarCode=" + CalendarCode + " And ShamsiDate=" + ShamsiDate + " And ShiftNo=" + ShiftNo;
                    if (Myds_Calender.Tables["Tbl_CalendarParticularShifts"].DefaultView.Count > 0)
                    {
                        ShiftStart = Conversions.ToDouble(Module1.GetFloatingHour(Conversions.ToString(Myds_Calender.Tables["Tbl_CalendarParticularShifts"].DefaultView[0]["ShiftStart"])));
                    }

                    break;
                }
            }

            ShiftStart = CheckFloating(ShiftStart);
            double StartDT;
            if (!double.TryParse(ShamsiDate + GetCompeleteHour(ShiftStart), out StartDT))
            {
                throw new Exception($"Error in GetShiftStartDT() when try to parse double from a date {ShamsiDate} and time {ShiftStart} .");
            }
            return StartDT;
        }

        private double GetShiftEndDT(string CalendarCode, int ShiftNo, string ShamsiDate)
        {
            if (this.LogPlanningProcedure) Logger.LogInfo($"GetShiftEndDT(CalendarCode:{CalendarCode}, ShiftNo:{ShiftNo}, ShamsiDate: {ShamsiDate})");

            var ShiftEnd = default(double);

            var MyRowCount = default(int);
            var MyDv = new DataView();
            switch (IsParticularDay(CalendarCode, ShamsiDate))
            {
                case -1: // تاریخ داده یک روز پیش فرض می باشد
                {
                    Myds_Calender.Tables["Tbl_CalendarShifts"].DefaultView.RowFilter = "CalendarCode=" + CalendarCode + " And ShiftNo=" + ShiftNo;
                    MyRowCount = Myds_Calender.Tables["Tbl_CalendarShifts"].DefaultView.Count;
                    MyDv = Myds_Calender.Tables["Tbl_CalendarShifts"].DefaultView;
                    break;
                }

                case 2: // تاریخ داده شده یک روز خاص عادی می باشد
                {
                    Myds_Calender.Tables["Tbl_CalendarParticularShifts"].DefaultView.RowFilter = "CalendarCode=" + CalendarCode + " And ShamsiDate=" + ShamsiDate + " And ShiftNo=" + ShiftNo;
                    MyRowCount = Myds_Calender.Tables["Tbl_CalendarParticularShifts"].DefaultView.Count;
                    MyDv = Myds_Calender.Tables["Tbl_CalendarParticularShifts"].DefaultView;
                    break;
                }
            }

            if (MyRowCount > 0)
            {
                var mShiftEndTime = TimeSpan.Parse(MyDv[0]["ShiftStart"].ToString()) + TimeSpan.Parse(MyDv[0]["ShiftDuration"].ToString()) + TimeSpan.Parse(MyDv[0]["ShiftExtraTime"].ToString());
                if (mShiftEndTime >= TimeSpan.Parse("1.00:00"))
                {
                    ShamsiDate = FarsiDateFunctions.AddToDate(ShamsiDate, "00000001");
                    mShiftEndTime = mShiftEndTime - TimeSpan.Parse("1.00:00");
                }

                string mHour = Conversions.ToString(Interaction.IIf(mShiftEndTime.Hours < 10, "0" + mShiftEndTime.Hours.ToString(), mShiftEndTime.Hours.ToString()));
                string mMinute = Conversions.ToString(Interaction.IIf(mShiftEndTime.Minutes < 10, "0" + mShiftEndTime.Minutes.ToString(), mShiftEndTime.Minutes.ToString()));
                ShiftEnd = Conversions.ToDouble(Module1.GetFloatingHour(mHour + ":" + mMinute));
            }

            ShiftEnd = CheckFloating(ShiftEnd);
            double EndDT;

            if (!double.TryParse(ShamsiDate + GetCompeleteHour(ShiftEnd), out EndDT))
            {
                throw new Exception($"Error in GetShiftStartDT() when try to parse double from a date {ShamsiDate} and time {ShiftEnd} .");
            }
            return EndDT;

        }

        /// <summary>
        /// این تابع مشخص می کند یک روز مشخص در تقویم روز تعطیل رسمی می باشد یا نه
        /// </summary>
        private bool CheckIsHoliday(string CalendarCode, string CurrentDate)
        {
            if (this.LogPlanningProcedure) Logger.LogInfo($"CheckIsHoliday(CalendarCode:{CalendarCode}, CurrentDate:{CurrentDate})");

            const int DT_DEFAULT = -1;
            const int DT_PARTICULARE_HOLIDAY = 1;
            const int DT_PARTICULARE_COMMON = 2;
            int MyRowCount = 0;

            if (CalendarCode == "" || CalendarCode == "0")
            {
                Logger.SaveError("CheckIsHoliday", $"Error in data: Operation Code = {FunctionLabel.Tag} , Calendar Code: {CalendarCode}");
                return false;
            }
            switch (IsParticularDay(CalendarCode, CurrentDate))
            {
                case DT_DEFAULT:
                {

                    Myds_Calender.Tables["Tbl_HoliDays"].DefaultView.RowFilter = "DayNo = " + Conversion.Val(Strings.Right(CurrentDate, 2)).ToString() + " And MonthNo = " + Conversion.Val(Strings.Mid(CurrentDate, 5, 2)).ToString();
                    MyRowCount = Myds_Calender.Tables["Tbl_HoliDays"].DefaultView.Count;
                    Myds_Calender.Tables["Tbl_HoliDays"].DefaultView.RowFilter = "";
                    if (MyRowCount > 0) // DT_HOLIDAY
                    {
                        return true;
                    }
                    else if (GetDayAccessibleTime(CalendarCode, Module1.GetDayNo(CurrentDate), 1, CurrentDate) != "00:00") // DT_COMMON
                    {
                        return false;
                    }

                    break;
                }

                // '''''''''''
                case DT_PARTICULARE_HOLIDAY:
                {
                    return true;
                }

                case DT_PARTICULARE_COMMON:
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// این تابع مشخص می کند یک تاریخ مشخص در تقویم روز خاص می باشد یا نه
        /// DayType: -1= not particular/ default       
        /// DayType: 1 = particular off       روز خاص - تعطیل 
        /// DayType: 2 = particular working   روز خاص -  کاری 
        /// </summary>
        private int IsParticularDay(string CalendarCode, string ShamsiDate)
        {   
            // check cashed variables:
            if (IsParticularDay_CalendarCode == CalendarCode || IsParticularDay_ShamsiDate == ShamsiDate)
            {
                return IsParticularDay_Status;
            }
            
            if (this.LogPlanningProcedure) Logger.LogInfo($"IsParticularDay(CalendarCode:{CalendarCode}, ShamsiDate:{ShamsiDate})");

            int mIsParticular = -1;
            try
            {
                Myds_Calender.Tables["Tbl_CalendarParticularDays"].DefaultView.RowFilter = "CalendarCode=" + CalendarCode + " And ShamsiDate=" + ShamsiDate;
                if (Myds_Calender.Tables["Tbl_CalendarParticularDays"].DefaultView.Count > 0)
                {
                    mIsParticular = Conversions.ToInteger(Myds_Calender.Tables["Tbl_CalendarParticularDays"].DefaultView[0]["DayType"]);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException("frmPlanning:IsParticularDay()", ex);
            }

            // Update cashed variables:
            IsParticularDay_CalendarCode = CalendarCode;
            IsParticularDay_ShamsiDate = ShamsiDate;
            IsParticularDay_Status = mIsParticular;

            Myds_Calender.Tables["Tbl_CalendarParticularDays"].DefaultView.RowFilter = "";

            return mIsParticular;
        }

        /// <summary>
        /// این تابع زمان در دسترس یک روز را باز می گرداند
        /// </summary>
        private string GetDayAccessibleTime(string CalendarCode, int DayNo, int DayType, string CurrentDate)
        {
            // FunctionLabel.Text = " GetDayAccessibleTime" & CStr(DayNo)
            // Application.DoEvents()
            if (this.LogPlanningProcedure) Logger.LogInfo($"GetDayAccessibleTime(CalendarCode:{CalendarCode}, DayNo:{DayNo},DayType:{DayType})");


            const int DT_DEFUALT = 1; // روز پیش فرض
            const int DT_PARTICULAR = 2; // روز خاص
            var mAccessibleTimes = new TimeSpan(0, 0, 0, 0, 0);
            DataView CalendarDaysDownTimes;
            try
            {

                switch (DayType)
                {
                    case DT_DEFUALT:
                    {
                        Myds_Calender.Tables["Tbl_CalendarDays"].DefaultView.RowFilter = "CalendarCode=" + CalendarCode + " And DayNo=" + DayNo;
                        var myDv = Myds_Calender.Tables["Tbl_CalendarDays"].DefaultView;

                        if (myDv.Count > 0)
                        {
                            for (int i = 0; i < myDv.Count; i++)
                            {
                                mAccessibleTimes += TimeSpan.Parse(myDv[i]["ShiftDuration"].ToString()) + TimeSpan.Parse(myDv[i]["ShiftExtraTime"].ToString());
                            }
                        }
                        Myds_Calender.Tables["Tbl_CalendarDaysDownTimes"].DefaultView.RowFilter = "CalendarCode=" + CalendarCode + " And DayNo=" + DayNo;
                        CalendarDaysDownTimes = Myds_Calender.Tables["Tbl_CalendarDaysDownTimes"].DefaultView;
                        if (CalendarDaysDownTimes.Count > 0)
                        {
                            for (int i = 0; i < CalendarDaysDownTimes.Count; i++)
                            {
                                if (TimeSpan.Parse(CalendarDaysDownTimes[i]["DownTimeEnd"].ToString()) >= TimeSpan.Parse(CalendarDaysDownTimes[i]["DownTimeStart"].ToString()))
                                {
                                    mAccessibleTimes -= TimeSpan.Parse(CalendarDaysDownTimes[i]["DownTimeEnd"].ToString()) - TimeSpan.Parse(CalendarDaysDownTimes[i]["DownTimeStart"].ToString());
                                }
                            }

                        }

                        Myds_Calender.Tables["Tbl_CalendarDays"].DefaultView.RowFilter = "";
                        break;
                    }

                    case DT_PARTICULAR:
                    {
                        
                        Myds_Calender.Tables["Tbl_CalendarParticularDays"].DefaultView.RowFilter = "CalendarCode=" + CalendarCode + " And ShamsiDate=" + CurrentDate; ;
                        var calendarParticularDays = Myds_Calender.Tables["Tbl_CalendarParticularDays"].DefaultView;
                        if (calendarParticularDays.Count > 0)
                        {
                            for (int i = 0; i < calendarParticularDays.Count; i++)
                            {
                                mAccessibleTimes += TimeSpan.Parse(calendarParticularDays[i]["AccessibleWorkTime"].ToString());
                            }

                        }
                        Myds_Calender.Tables["Tbl_ParticularShiftDownTimes"].DefaultView.RowFilter = "CalendarCode=" + CalendarCode + " And ShamsiDate=" + DayNo; ;
                        var ParticularShiftDownTimes = Myds_Calender.Tables["Tbl_ParticularShiftDownTimes"].DefaultView;
                        if (ParticularShiftDownTimes.Count > 0)
                        {
                            for (int i = 0; i < ParticularShiftDownTimes.Count; i++)
                            {
                                if (TimeSpan.Parse(ParticularShiftDownTimes[i]["DownTimeEnd"].ToString()) >= TimeSpan.Parse(ParticularShiftDownTimes[i]["DownTimeStart"].ToString()))
                                {
                                    mAccessibleTimes -= TimeSpan.Parse(ParticularShiftDownTimes[i]["DownTimeEnd"].ToString()) - TimeSpan.Parse(ParticularShiftDownTimes[i]["DownTimeStart"].ToString());
                                }
                            }

                        }
                        Myds_Calender.Tables["Tbl_CalendarParticularDays"].DefaultView.RowFilter = "";
                        break;
                    }
                }


                if (mAccessibleTimes >= TimeSpan.Parse("1.00:00"))
                {
                    return "24:00";
                }
                else
                {
                    string mHour = Conversions.ToString(Interaction.IIf(mAccessibleTimes.Hours < 10, "0" + mAccessibleTimes.Hours.ToString(), mAccessibleTimes.Hours.ToString()));
                    string mMinute = Conversions.ToString(Interaction.IIf(mAccessibleTimes.Minutes < 10, "0" + mAccessibleTimes.Minutes.ToString(), mAccessibleTimes.Minutes.ToString()));
                    return mHour + ":" + mMinute;
                }


            }
            catch (Exception ex)
            {
                Logger.LogException("GetDayAccesibleTime:", ex);
                MessageBox.Show("خطا در یافتن زمان در دسترس یک روز ");
                return "0:0";
            }
        }

        private string GetMachineCode(EnumExecutionMethod ExecMethod, string OperationCode)
        {
            // FunctionLabel.Text = " GetMachineCode"
            // Application.DoEvents()
            if (this.LogPlanningProcedure) Logger.LogInfo($"GetMachineCode(ExecMethod:{ExecMethod}, OperationCode:{OperationCode})");

            string MachineCode;
            var drPlanning = Db.DataSet.Tables["Tbl_Planning"].Select("TreeCode=" + mTreeCode + " And SubbatchCode='" + mSubbatchCode + "' And OperationCode='" + OperationCode + "'");
            if (drPlanning.Length > 0)
            {
                MachineCode = Conversions.ToString(drPlanning[0]["MachineCode"]);
            }
            else
            {
                Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].DefaultView.RowFilter = "TreeCode=" + mTreeCode + " And OperationCode='" + OperationCode + "'";
                Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].DefaultView.Sort = "MachinePriority";
                if (ExecMethod == EnumExecutionMethod.EM_MACHINE && Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].DefaultView.Count == 0)
                {
                    MessageBox.Show(" ماشين یافت نشد " + OperationCode + " برای عملیات ", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    throw new Exception("Programmer throw");

                }

                if (Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].DefaultView.Count > 0)
                {
                    MachineCode = Conversions.ToString(Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].DefaultView[0]["MachineCode"]);
                }
                else
                {
                    MachineCode = "-1";
                }
            }

            return MachineCode;
        }

        private string[] GetMachineCodes(int ExecMethod, string OperationCode)
        {
            List<string> MachineList;
            DataView dv;
            MachineList = new List<string>();
            Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].DefaultView.RowFilter = "TreeCode=" + mTreeCode + " And OperationCode='" + OperationCode + "'";
            Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].DefaultView.Sort = "MachinePriority";
            dv = Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].DefaultView;
            if (ExecMethod == (int)EnumExecutionMethod.EM_MACHINE && dv.Count == 0)
            {
                Logger.SaveError("GetMachineCodes", " ماشين یافت نشد " + OperationCode + " برای عملیات ");
                MachineList.Add("-1");
            }
            else
            {
                for (int I = 0, loopTo = dv.Count - 1; I <= loopTo; I++)
                    MachineList.Add(Conversions.ToString(dv[I]["MachineCode"]));
            }

            return MachineList.ToArray();
        }

     
        private double CheckFloating(double dblHour)
        {
            if (!dblHour.ToString().Contains("."))
            {
                dblHour = Conversions.ToDouble(dblHour.ToString() + ".0");
            }

            return dblHour;
        }
        private double CheckFloating2(double dblHour)
        {
            if (!dblHour.ToString().Contains("."))
            {
                dblHour = double.Parse(dblHour.ToString() + ".0");
            }
            return dblHour;
        }

        private string MkStandardHour(string H)
        {
            string Str;
            try
            {
                if (string.IsNullOrEmpty(H))
                {
                    return "0.0";
                }

                if (!H.Contains("."))
                {
                    H = H + ".0";
                }


                int intSection = int.Parse(H.Substring(0, H.IndexOf(".")));
                string decimalSection = H.Substring(H.IndexOf(".") + 1, H.Length - H.IndexOf(".") - 1);


                if (intSection < 10)
                {
                    Str = "0" + intSection.ToString() + "." + decimalSection;
                }
                else
                {
                    Str = H;
                }

                return Str;
            }
            catch (Exception ex)
            {

                Logger.LogException($"MkStandardHour({H})", ex);
                return H;
            }

        }

        private string GetCompeleteHour(double mHour)
        {
            // FunctionLabel.Text = "GetCompeleteHour"
            // Application.DoEvents()

            string mstrHour = mHour.ToString();
            if (Strings.Left(mHour.ToString(), 1) != "0" && mHour < 10.0d)
            {
                mstrHour = "0" + mHour.ToString();
            }

            return mstrHour;
        }

        private string GetBatchProductionCallDate(object mBatchCode)
        {
            // FunctionLabel.Text = "GetBatchProductionCallDate"
            // Application.DoEvents()

            string ReturnedCallDate = "-1";
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    var cmCallDate = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Select IsNull(Max(ProductionCallDate),-1) From Tbl_CustomerOrders Where OrderIndex IN (Select OrderIndex From Tbl_Batch_Order Where BatchCode = '", mBatchCode), "')")), cn);
                    ReturnedCallDate = cmCallDate.ExecuteScalar().ToString();
                }
                catch (Exception)
                {
                    ReturnedCallDate = "-1";
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }

            return ReturnedCallDate;
        }

        private bool IsHasProduction(string mSBCode, string mTCode, string OperationCode)
        {
            // FunctionLabel.Text = " IsHasProduction"
            // Application.DoEvents()
            if (this.LogPlanningProcedure) Logger.LogInfo($"IsHasProduction(mSBCode:{mSBCode},mTCode:{mTCode},OperationCode:{OperationCode})");

            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    var cm = new SqlCommand("Select Count(*) From Tbl_RealProduction Where TreeCode = " + mTCode + " And OperationCode = '" + OperationCode + "' And SubbatchCode = '" + mSBCode + "'", cn);
                    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(cm.ExecuteScalar(), 0, false)))
                    {
                        return false;
                    }
                }
                catch
                {
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }

            return true;
        }

        private void GetBackwardParallelExecutionTimes(string OperationCode,
                                                       DataRow drOPCs,
                                                       string StartDate, double StartHour,
                                                       long FirstDeliveryQty,
                                                       bool IsFixStartHour,
                                                       DataRow[] mParallelMachines,
                                                       DataRow[] mSingleMachines,
                                                       long mQty,
                                                       ref ArrayList[] mSingleMachinesValues,
                                                       ref ArrayList[] mParallelMachinesValues,
                                                       ref int mEarlSingleIndex)
        {
            FunctionLabel.Text = Conversions.ToString(Operators.AddObject(FunctionLabel.Tag, " GetBackwardParallelExecutionTimes"));
            Application.DoEvents();

            if (this.LogPlanningProcedure) Logger.LogInfo($"GetBackwardParallelExecutionTimes(OperationCode:{OperationCode},StartDate:{StartDate},StartHour:{StartHour},FirstDeliveryQty:{FirstDeliveryQty},mQty:{mQty})");

            var mOneExecTimes = new double[mParallelMachines.Length];
            long mExecSpeeds = 0L;
            var ExecMethod = EnumExecutionMethod.EM_MACHINE;
            for (int I = 0, loopTo = mParallelMachines.Length - 1; I <= loopTo; I++)
            {
                mOneExecTimes[I] = GetOneOperationExecTime(OperationCode, ExecMethod, Conversions.ToString(mParallelMachines[I]["MachineCode"]));
                mExecSpeeds += Conversions.ToLong(GetOpExecutionSpeed(ExecMethod, mTreeCode, OperationCode, Conversions.ToString(mParallelMachines[I]["MachineCode"])));
            }

            double mParallelTotalTime = 1d / mExecSpeeds * mQty;

            // بدست آوردن زمان اجرای عملیات برای تمامی ماشین ها به صورت تکی
            for (int I = 0, loopTo1 = mSingleMachines.Length - 1; I <= loopTo1; I++)
            {
                string mMCode;
                double mSDuration = 0.0d;
                double mODuration = 0.0d;
                string mPStartDate = "0";
                string mPStartHour = "0.0";
                string mPEndDate = "0";
                string mPEndHour = "0.0";
                string mSStartDate = "0";
                string mSStartHour = "0.0";
                string mSEndDate = "0";
                string mSEndHour = "0.0";
                string mOStartDate = StartDate.ToString();
                string mOStartHour = StartHour.ToString();
                string mOEndDate = "0";
                string mOEndHour = "0.0";
                mMCode = mSingleMachines[I]["MachineCode"].ToString();
                string CalendarCode = GetCalendarCode(ExecMethod, OperationCode, mMCode);
                if (!IsFixStartHour)
                {
                    while (CheckIsHoliday(CalendarCode, mOStartDate))
                        mOStartDate = FarsiDateFunctions.AddToDate(StartDate.ToString(), "00000001");
                    mOStartHour = GetCalendarEnd(CalendarCode);
                    double argCurrentHour = Conversions.ToDouble(mOStartHour);
                    GetNewDate(CalendarCode, ref mOStartDate, ref argCurrentHour, FirstDeliveryQty * GetOneOperationExecTime(OperationCode, ExecMethod, mMCode) * -1);
                }

                // بدست آوردن اولین زمان ممکن برای اجرای فعالیت
                string argPlanningStartDate = (mPStartDate);
                double argPlanningStartHour = Conversions.ToDouble(mPStartHour);
                string argSetupStartDate = (mSStartDate);
                double argSetupStartHour = Conversions.ToDouble(mSStartHour);
                string argOperationStartDate = (mOStartDate);
                double argOperationStartHour = Conversions.ToDouble(mOStartHour);
                string argPlanningEndDate = (mPEndDate);
                double argPlanningEndHour = Conversions.ToDouble(mPEndHour);
                string argSetupEndDate = (mSEndDate);
                double argSetupEndHour = Conversions.ToDouble(mSEndHour);
                string argOperationEndDate = (mOEndDate);
                double argOperationEndHour = Conversions.ToDouble(mOEndHour);
                Backward_GetFirstAccessibleTime(ExecMethod, OperationCode, ref mMCode, ref argPlanningStartDate, ref argPlanningStartHour, ref argSetupStartDate, ref argSetupStartHour, ref argOperationStartDate, ref argOperationStartHour, ref argPlanningEndDate, ref argPlanningEndHour, ref argSetupEndDate, ref argSetupEndHour, ref argOperationEndDate, ref argOperationEndHour, ref mSDuration, ref mODuration, ref mQty, ExecutionStatus_enum.ES_PARALLEL);
                mOEndDate = mPStartDate;
                mOEndHour = mPStartHour;
                double argCurrentHour1 = Conversions.ToDouble(mOEndHour);
                GetNewDate(CalendarCode, ref mOEndDate, ref argCurrentHour1, mODuration + mSDuration);
                mPEndDate = mOEndDate;
                mPEndHour = mOEndHour;
                mSEndDate = mSStartDate;
                mSEndHour = mSStartHour;
                double argCurrentHour2 = Conversions.ToDouble(mSEndHour);
                GetNewDate(CalendarCode, ref mSEndDate, ref argCurrentHour2, mSDuration);
                mSingleMachinesValues[I] = new ArrayList();
                mSingleMachinesValues[I].Add(mMCode);
                mSingleMachinesValues[I].Add(mPStartDate);
                mSingleMachinesValues[I].Add(mPStartHour);
                mSingleMachinesValues[I].Add(mPEndDate);
                mSingleMachinesValues[I].Add(mPEndHour);
                mSingleMachinesValues[I].Add(mSStartDate);
                mSingleMachinesValues[I].Add(mSStartHour);
                mSingleMachinesValues[I].Add(mOStartDate);
                mSingleMachinesValues[I].Add(mOStartHour);
                mSingleMachinesValues[I].Add(mQty);
                mSingleMachinesValues[I].Add(mSDuration);
                mSingleMachinesValues[I].Add(mODuration);
                mSingleMachinesValues[I].Add(mSEndDate);
                mSingleMachinesValues[I].Add(mSEndHour);
                mSingleMachinesValues[I].Add(mOEndDate);
                mSingleMachinesValues[I].Add(mOEndHour);
            }

            long mParallelProduction = 0L;

            // بدست آوردن زمان اجرای عملیات برای ماشین هایی که در موازی کاری شرکت می کنند
            for (int I = 0, loopTo2 = mParallelMachines.Length - 1; I <= loopTo2; I++)
            {
                string mMCode;
                double mSDuration = 0.0d;
                double mODuration = 0.0d;
                string mPStartDate = "0";
                string mPStartHour = "0.0";
                string mPEndDate = "0";
                string mPEndHour = "0.0";
                string mSStartDate = "0";
                string mSStartHour = "0.0";
                string mSEndDate = "0";
                string mSEndHour = "0.0";
                string mOStartDate = StartDate.ToString();
                string mOStartHour = StartHour.ToString();
                string mOEndDate = "0";
                string mOEndHour = "0.0";
                long mPQ = 0L;
                mMCode = Conversions.ToString(mParallelMachines[I]["MachineCode"]);
                string CalendarCode = GetCalendarCode(ExecMethod, OperationCode, mMCode);
                if (!IsFixStartHour)
                {
                    while (CheckIsHoliday(CalendarCode, mOStartDate))
                        mOStartDate = FarsiDateFunctions.AddToDate(StartDate.ToString(), "00000001");
                    mOStartHour = GetCalendarEnd(CalendarCode);
                    double argCurrentHour3 = Conversions.ToDouble(mOStartHour);
                    GetNewDate(CalendarCode, ref mOStartDate, ref argCurrentHour3, FirstDeliveryQty * GetOneOperationExecTime(OperationCode, ExecMethod, mMCode) * -1);
                }

                // بدست آوردن تعداد تولید برای ماشین در حالت موازی
                if (I == mParallelMachines.Length - 1)
                {
                    mPQ = mQty - mParallelProduction;
                }
                else
                {
                    mPQ = (int)Math.Round(mParallelTotalTime / mOneExecTimes[I]);
                    mParallelProduction += mPQ;
                }

                // بدست آوردن اولین زمان ممکن برای اجرای فعالیت
                string argPlanningStartDate1 = (mPStartDate);
                double argPlanningStartHour1 = Conversions.ToDouble(mPStartHour);
                string argSetupStartDate1 = (mSStartDate);
                double argSetupStartHour1 = Conversions.ToDouble(mSStartHour);
                string argOperationStartDate1 = (mOStartDate);
                double argOperationStartHour1 = Conversions.ToDouble(mOStartHour);
                string argPlanningEndDate1 = (mPEndDate);
                double argPlanningEndHour1 = Conversions.ToDouble(mPEndHour);
                string argSetupEndDate1 = (mSEndDate);
                double argSetupEndHour1 = Conversions.ToDouble(mSEndHour);
                string argOperationEndDate1 = (mOEndDate);
                double argOperationEndHour1 = Conversions.ToDouble(mOEndHour);
                Backward_GetFirstAccessibleTime(ExecMethod, OperationCode, ref mMCode, ref argPlanningStartDate1, ref argPlanningStartHour1, ref argSetupStartDate1, ref argSetupStartHour1, ref argOperationStartDate1, ref argOperationStartHour1, ref argPlanningEndDate1, ref argPlanningEndHour1, ref argSetupEndDate1, ref argSetupEndHour1, ref argOperationEndDate1, ref argOperationEndHour1, ref mSDuration, ref mODuration, ref mPQ, ExecutionStatus_enum.ES_PARALLEL);
                mOEndDate = mPStartDate;
                mOEndHour = mPStartHour;
                double argCurrentHour4 = Conversions.ToDouble(mOEndHour);
                GetNewDate(CalendarCode, ref mOEndDate, ref argCurrentHour4, mODuration + mSDuration);
                mPEndDate = mOEndDate;
                mPEndHour = mOEndHour;
                mSEndDate = mSStartDate;
                mSEndHour = mSStartHour;
                double argCurrentHour5 = Conversions.ToDouble(mSEndHour);
                GetNewDate(CalendarCode, ref mSEndDate, ref argCurrentHour5, mSDuration);
                mParallelMachinesValues[I] = new ArrayList();
                mParallelMachinesValues[I].Add(mMCode);
                mParallelMachinesValues[I].Add(mPStartDate);
                mParallelMachinesValues[I].Add(mPStartHour);
                mParallelMachinesValues[I].Add(mPEndDate);
                mParallelMachinesValues[I].Add(mPEndHour);
                mParallelMachinesValues[I].Add(mSStartDate);
                mParallelMachinesValues[I].Add(mSStartHour);
                mParallelMachinesValues[I].Add(mOStartDate);
                mParallelMachinesValues[I].Add(mOStartHour);
                mParallelMachinesValues[I].Add(mPQ);
                mParallelMachinesValues[I].Add(mSDuration);
                mParallelMachinesValues[I].Add(mODuration);
                mParallelMachinesValues[I].Add(mSEndDate);
                mParallelMachinesValues[I].Add(mSEndHour);
                mParallelMachinesValues[I].Add(mOEndDate);
                mParallelMachinesValues[I].Add(mOEndHour);
            }

            double mSHour = Conversions.ToDouble(mSingleMachinesValues[0][2]);
            double mEHour = Conversions.ToDouble(mSingleMachinesValues[0][4]);
            mSHour = CheckFloating(mSHour);
            mEHour = CheckFloating(mEHour);
            string mTempStart = mSingleMachinesValues[0][1].ToString() + GetCompeleteHour(mSHour);
            string mTempEnd = mSingleMachinesValues[0][3].ToString() + GetCompeleteHour(mEHour);
            mEarlSingleIndex = 0;

            // بدست آوردن زودترین زمان انجام عملیات با استفاده از یک ماشین
            for (int I = 1, loopTo3 = mSingleMachinesValues.Length - 1; I <= loopTo3; I++)
            {
                mSHour = Conversions.ToDouble(mSingleMachinesValues[I][2]);
                mEHour = Conversions.ToDouble(mSingleMachinesValues[I][4]);
                mSHour = CheckFloating(mSHour);
                mEHour = CheckFloating(mEHour);
                string mMachineStart = mSingleMachinesValues[I][1].ToString() + GetCompeleteHour(mSHour);
                string mMachineEnd = mSingleMachinesValues[I][3].ToString() + GetCompeleteHour(mEHour);
                if (Operators.CompareString(mMachineStart, mTempStart, false) <= 0)
                {
                    if (Operators.CompareString(mMachineEnd, mTempEnd, false) < 0)
                    {
                        mEarlSingleIndex = I;
                        mTempStart = mMachineStart;
                        mTempEnd = mMachineEnd;
                    }
                }
                else if (Operators.CompareString(mMachineStart, mTempStart, false) > 0)
                {
                    if (Operators.CompareString(mMachineEnd, mTempEnd, false) <= 0)
                    {
                        mEarlSingleIndex = I;
                        mTempStart = mMachineStart;
                        mTempEnd = mMachineEnd;
                    }
                }
            }

            mSHour = Conversions.ToDouble(mParallelMachinesValues[0][2]);
            mEHour = Conversions.ToDouble(mParallelMachinesValues[0][4]);
            mSHour = CheckFloating(mSHour);
            mEHour = CheckFloating(mEHour);
            string mTempParallelStart = mParallelMachinesValues[0][1].ToString() + GetCompeleteHour(mSHour);
            string mTempParallelEnd = mParallelMachinesValues[0][3].ToString() + GetCompeleteHour(mEHour);

            // بدست آوردن زمان انجام عملیات در حالت موازی
            for (int I = 1, loopTo4 = mParallelMachinesValues.Length - 1; I <= loopTo4; I++) // زمان شروع موازی
            {
                mSHour = Conversions.ToDouble(mParallelMachinesValues[I][2]);
                mSHour = CheckFloating(mSHour);
                string mMachineStart = mParallelMachinesValues[I][1].ToString() + GetCompeleteHour(mSHour);
                if (Operators.CompareString(mMachineStart, mTempParallelStart, false) < 0)
                {
                    mTempParallelStart = mMachineStart;
                }
            }

            for (int I = 1, loopTo5 = mParallelMachinesValues.Length - 1; I <= loopTo5; I++) // زمان پایان موازی
            {
                mEHour = Conversions.ToDouble(mParallelMachinesValues[I][4]);
                mEHour = CheckFloating(mEHour);
                string mMachineEnd = mParallelMachinesValues[I][3].ToString() + GetCompeleteHour(mEHour);
                if (Operators.CompareString(mMachineEnd, mTempParallelEnd, false) > 0)
                {
                    mTempParallelEnd = mMachineEnd;
                }
            }

            // انتخاب زودترین زمان انجام عملیات در بین حالت موازی و حالت تک ماشین
            if (Operators.CompareString(mTempParallelStart, mTempStart, false) <= 0)
            {
                if (Operators.CompareString(mTempParallelEnd, mTempEnd, false) < 0)
                {
                    mTempStart = mTempParallelStart;
                    mTempEnd = mTempParallelEnd;
                    mEarlSingleIndex = -1;
                }
            }
            else if (Operators.CompareString(mTempParallelStart, mTempStart, false) > 0)
            {
                if (Operators.CompareString(mTempParallelEnd, mTempEnd, false) <= 0)
                {
                    mTempStart = mTempParallelStart;
                    mTempEnd = mTempParallelEnd;
                    mEarlSingleIndex = -1;
                }
            }
        }



        private void SaveNewPlanItem(PlanOperation planItem, ref SqlTransaction mTrn)
        {
            var planRec = planItem.planningRec;

            FunctionLabel.Text = FunctionLabel.Tag + " SaveNewPlanItem";
            Application.DoEvents();
            if (this.LogPlanningProcedure) Logger.LogInfo($"SaveNewPlanItem(OperationCode:{planRec.OperationCode},MachineCode {planRec.MachineCode}, PlanningStartDate{planRec.PlanningStartDate},PlanningStartHour {MkStandardHour(planRec.PlanningStartHour)})");


            LastPlanningCode += 1;
            var drInsert = Db.DataSet.Tables["Tbl_Planning"].NewRow();
            drInsert["PlanningCode"] = LastPlanningCode;
            drInsert["SubbatchCode"] = mSubbatchCode;
            drInsert["TreeCode"] = mTreeCode;
            drInsert["OperationCode"] = planRec.OperationCode;
            drInsert["SplitNo"] = 0;
            drInsert["MachineCode"] = planRec.MachineCode;
            drInsert["PlanningStartDate"] = planRec.PlanningStartDate;
            drInsert["LatinStartDate"] = Module1.Get_LatinDate_FromPersianDate(planRec.PlanningStartDate);
            drInsert["PlanningStartHour"] = MkStandardHour(planRec.PlanningStartHour);
            drInsert["PlanningEndDate"] = planRec.PlanningEndDate;
            drInsert["LatinEndDate"] = Module1.Get_LatinDate_FromPersianDate(planRec.PlanningEndDate);
            drInsert["PlanningEndHour"] = MkStandardHour(planRec.PlanningEndHour);
            drInsert["SetupStartDate"] = planRec.SetupStartDate;
            drInsert["SetupStartHour"] = MkStandardHour(planRec.SetupStartHour);
            drInsert["SetupEndDate"] = planRec.SetupEndDate;
            drInsert["SetupEndHour"] = MkStandardHour(planRec.SetupEndHour);
            drInsert["OperationStartDate"] = planRec.OperationStartDate;
            drInsert["OperationStartHour"] = MkStandardHour(planRec.OperationStartHour);
            drInsert["OperationEndDate"] = planRec.OperationEndDate;
            drInsert["OperationEndHour"] = MkStandardHour(planRec.OperationEndHour);
            drInsert["SetupDuration"] = planRec.SetupDuration;
            drInsert["OperationDuration"] = planRec.OperationDuration;
            drInsert["PlanningDuration"] = planRec.SetupDuration + planRec.OperationDuration;
            drInsert["DetailProductionQuantity"] = planRec.ProductionQty;
            drInsert["PreviousPlanningCode"] = PreviousPlanningCode;
            drInsert["NextPlanningCode"] = -1;

            Db.DataSet.Tables["Tbl_Planning"].Rows.Add(drInsert);
        }

        private void InsertPlanningNewRows(ref int mPlanningLastCode,
                                           string mOperationCode,
                                           string[] MachineCodes,
                                           long[] ProductionQtys,
                                           string[] PlanningStartDates, double[] PlanningStartHours,
                                           string[] SetupStartDates, double[] SetupStartHours,
                                           string[] OperationStartDates, double[] OperationStartHours,
                                           string[] PlanningEndDates, double[] PlanningEndHours,
                                           string[] SetupEndDates, double[] SetupEndHours,
                                           string[] OperationEndDates, double[] OperationEndHours,
                                           double[] SetupDurations,
                                           double[] OperationDurations,
                                           long[] CopyPlanningCodes,
                                           ref SqlTransaction mTrn)
        {
            FunctionLabel.Text = Conversions.ToString(Operators.AddObject(FunctionLabel.Tag, " InsertPlanningNewRows"));
            Application.DoEvents();
            if (this.LogPlanningProcedure) Logger.LogInfo($"InsertPlanningNewRows(OperationCode:{mOperationCode})");

            var drOPCs = Db.DataSet.Tables["Tbl_ProductOPCs"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + mOperationCode + "'");
            string PlanningEndDate = "0";
            double PlanningEndHour = 0d;
            string SetupEndDate = "0";
            double SetupEndHour = 0d;
            double SetupDuration = 0d;
            string OperationEndDate = "0";
            double OperationEndHour = 0d;
            double OperationDuration = 0d;
            for (int I = 0, loopTo = PlanningStartDates.Length - 1; I <= loopTo; I++)
            {
                mPlanningLastCode += 1;
                if (mTrn.Connection.State == ConnectionState.Closed)
                    mTrn.Connection.Open();
                var cm = new SqlCommand("Select Count(*) From Tbl_Planning Where PlanningCode = " + mPlanningLastCode, mTrn.Connection);
                cm.Transaction = mTrn;
                while (Conversions.ToInteger(cm.ExecuteScalar()) > 0)
                {
                    mPlanningLastCode += 1;
                    cm.CommandText = "Select Count(*) From Tbl_Planning Where PlanningCode = " + mPlanningLastCode;
                }

                var drInsert = Db.DataSet.Tables["Tbl_Planning"].NewRow();
                drInsert["PlanningCode"] = mPlanningLastCode.ToString();
                drInsert["SubbatchCode"] = mSubbatchCode;
                drInsert["TreeCode"] = mTreeCode;
                drInsert["OperationCode"] = mOperationCode;
                drInsert["SplitNo"] = Interaction.IIf(PlanningStartDates.Length > 1, I + 1, 0);
                drInsert["MachineCode"] = MachineCodes[I];
                drInsert["PlanningStartDate"] = PlanningStartDates[I];
                drInsert["LatinStartDate"] = Module1.Get_LatinDate_FromPersianDate(PlanningStartDates[I].ToString());
                if (PlanningStartHours[I].ToString().Contains("."))
                {
                    if (PlanningStartHours[I] < 10.0d)
                    {
                        drInsert["PlanningStartHour"] = "0" + PlanningStartHours[I].ToString();
                    }
                    else
                    {
                        drInsert["PlanningStartHour"] = PlanningStartHours[I].ToString();
                    }
                }
                else if (PlanningStartHours[I] < 10.0d)
                {
                    drInsert["PlanningStartHour"] = "0" + PlanningStartHours[I].ToString() + ".0";
                }
                else
                {
                    drInsert["PlanningStartHour"] = PlanningStartHours[I].ToString() + ".0";
                }

                drInsert["SetupStartDate"] = SetupStartDates[I];
                if (SetupStartHours[I].ToString().Contains("."))
                {
                    if (SetupStartHours[I] < 10.0d)
                    {
                        drInsert["SetupStartHour"] = "0" + SetupStartHours[I].ToString();
                    }
                    else
                    {
                        drInsert["SetupStartHour"] = SetupStartHours[I].ToString();
                    }
                }
                else if (SetupStartHours[I] < 10.0d)
                {
                    drInsert["SetupStartHour"] = "0" + SetupStartHours[I].ToString() + ".0";
                }
                else
                {
                    drInsert["SetupStartHour"] = SetupStartHours[I].ToString() + ".0";
                }

                drInsert["OperationStartDate"] = OperationStartDates[I];
                if (OperationStartHours[I].ToString().Contains("."))
                {
                    if (OperationStartHours[I] < 10.0d)
                    {
                        drInsert["OperationStartHour"] = "0" + OperationStartHours[I].ToString();
                    }
                    else
                    {
                        drInsert["OperationStartHour"] = OperationStartHours[I].ToString();
                    }
                }
                else if (OperationStartHours[I] < 10.0d)
                {
                    drInsert["OperationStartHour"] = "0" + OperationStartHours[I].ToString() + ".0";
                }
                else
                {
                    drInsert["OperationStartHour"] = OperationStartHours[I].ToString() + ".0";
                }

                drInsert["DetailProductionQuantity"] = ProductionQtys[I];
                drInsert["PreviousPlanningCode"] = PreviousPlanningCode;
                drInsert["NextPlanningCode"] = -1;

                // محاسبۀ زمان پایان فعالیت
                if (PlanningEndDates.Length > 0 && PlanningEndDates[0] != "0") // Backward
                {
                    PlanningEndDate = PlanningEndDates[I];
                    PlanningEndHour = PlanningEndHours[I];
                    SetupEndDate = SetupEndDates[I];
                    SetupEndHour = SetupEndHours[I];
                    SetupDuration = SetupDurations[I];
                    OperationEndDate = OperationEndDates[I];
                    OperationEndHour = OperationEndHours[I];
                    OperationDuration = OperationDurations[I];
                }
                else if (CopyPlanningCodes.Length > 0) // Forward
                                                       // در صورتیکه عملیات جاری یک عملیات جفت باشد
                {
                    var drExistPlanning = Db.DataSet.Tables["Tbl_Planning"].Select("PlanningCode = " + CopyPlanningCodes[I]);
                    if (drExistPlanning.Length > 0)
                    {
                        PlanningEndDate = drExistPlanning[0]["PlanningEndDate"].ToString();
                        PlanningEndHour = Conversions.ToDouble(drExistPlanning[0]["PlanningEndHour"]);
                        SetupEndDate = drExistPlanning[0]["SetupEndDate"].ToString();
                        SetupEndHour = Conversions.ToDouble(drExistPlanning[0]["SetupEndHour"]);
                        SetupDuration = Conversions.ToDouble(drExistPlanning[0]["SetupDuration"]);
                        OperationEndDate = drExistPlanning[0]["OperationEndDate"].ToString();
                        OperationEndHour = Conversions.ToDouble(drExistPlanning[0]["OperationEndHour"]);
                        OperationDuration = Conversions.ToDouble(drExistPlanning[0]["OperationDuration"]);
                    }
                    else
                    {
                        PlanningEndDate = SetupStartDates[I];
                        PlanningEndHour = SetupStartHours[I];
                        SetupEndDate = SetupStartDates[I];
                        SetupEndHour = SetupStartHours[I];
                        SetupDuration = 0d;
                        OperationEndDate = SetupStartDates[I];
                        OperationEndHour = SetupStartHours[I];
                        OperationDuration = 0d;
                    }
                }
                else if (ProductionQtys[I] > 0L)
                {
                    PlanningEndDate = PlanningStartDates[I];
                    PlanningEndHour = PlanningStartHours[I];
                    SetupEndDate = SetupStartDates[I];
                    SetupEndHour = SetupStartHours[I];
                    OperationEndDate = OperationStartDates[I];
                    OperationEndHour = OperationStartHours[I];
                    EnumExecutionMethod execMethod = CommonTool.GetExecutionMethod(drOPCs[0]["ExecutionMethod"].ToString());
                    GetOperationEnd(ref PlanningEndDate, ref PlanningEndHour, ref SetupEndDate, ref SetupEndHour, ref SetupDuration, ref OperationEndDate, ref OperationEndHour, ref OperationDuration, GetCalendarCode(execMethod, mOperationCode, MachineCodes[I]), execMethod.ToString(), mTreeCode, mOperationCode, MachineCodes[I], ProductionQtys[I]);
                }
                else
                {
                    SetupEndDate = SetupStartDates[I];
                    SetupEndHour = SetupStartHours[I];
                    OperationEndDate = SetupStartDates[I];
                    OperationEndHour = SetupStartHours[I];
                    PlanningEndDate = SetupStartDates[I];
                    PlanningEndHour = SetupStartHours[I];
                    SetupDuration = 0d;
                    OperationDuration = 0d;
                }

                drInsert["PlanningEndDate"] = PlanningEndDate;
                drInsert["LatinEndDate"] = Module1.Get_LatinDate_FromPersianDate(PlanningEndDate.ToString());
                if (PlanningEndHour.ToString().Contains("."))
                {
                    if (PlanningEndHour < 10.0d)
                    {
                        drInsert["PlanningEndHour"] = "0" + PlanningEndHour.ToString();
                    }
                    else
                    {
                        drInsert["PlanningEndHour"] = PlanningEndHour.ToString();
                    }
                }
                else if (PlanningEndHour < 10.0d)
                {
                    drInsert["PlanningEndHour"] = "0" + PlanningEndHour.ToString() + ".0";
                }
                else
                {
                    drInsert["PlanningEndHour"] = PlanningEndHour.ToString() + ".0";
                }

                drInsert["SetupEndDate"] = SetupEndDate;
                if (SetupEndHour.ToString().Contains("."))
                {
                    if (SetupEndHour < 10.0d)
                    {
                        drInsert["SetupEndHour"] = "0" + SetupEndHour.ToString();
                    }
                    else
                    {
                        drInsert["SetupEndHour"] = SetupEndHour.ToString();
                    }
                }
                else if (SetupEndHour < 10.0d)
                {
                    drInsert["SetupEndHour"] = "0" + SetupEndHour.ToString() + ".0";
                }
                else
                {
                    drInsert["SetupEndHour"] = SetupEndHour.ToString() + ".0";
                }

                drInsert["OperationEndDate"] = OperationEndDate;
                if (OperationEndHour.ToString().Contains("."))
                {
                    if (OperationEndHour < 10.0d)
                    {
                        drInsert["OperationEndHour"] = "0" + OperationEndHour.ToString();
                    }
                    else
                    {
                        drInsert["OperationEndHour"] = OperationEndHour.ToString();
                    }
                }
                else if (OperationEndHour < 10.0d)
                {
                    drInsert["OperationEndHour"] = "0" + OperationEndHour.ToString() + ".0";
                }
                else
                {
                    drInsert["OperationEndHour"] = OperationEndHour.ToString() + ".0";
                }

                drInsert["PlanningDuration"] = SetupDuration + OperationDuration;
                drInsert["SetupDuration"] = SetupDuration;
                drInsert["OperationDuration"] = OperationDuration;
                drInsert["ProductionCode"] = 0;
                PreviousPlanningCode = Conversions.ToLong(drInsert["PlanningCode"]);
                Db.DataSet.Tables["Tbl_Planning"].Rows.Add(drInsert);
            }
        }

        private long GetMaxMatchedQuantity(string MatchedOperations)
        {
            FunctionLabel.Text = Conversions.ToString(Operators.AddObject(FunctionLabel.Tag, " GetMaxMatchedQuantity"));
            Application.DoEvents();
            long mMaxMatchedQuantity = 0L;
            foreach (string s in MatchedOperations.Split(','))
            {
                long mQunatity = GetOperationPlanningQuantity(s.Replace("'", ""));
                if (mQunatity > mMaxMatchedQuantity)
                {
                    mMaxMatchedQuantity = mQunatity;
                }
            }

            return mMaxMatchedQuantity;
        }

        private void Backward_GetPlanTime_BaseOn_NextOps(string mBaseOperationCode,
                                                         DataRow drAfterOp,
                                                         ref string[] MachineCodes,
                                                         ref string[] PlanningStartDates, ref double[] PlanningStartHours,
                                                         ref string[] SetupStartDates, ref double[] SetupStartHours,
                                                         ref string[] OperationStartDates, ref double[] OperationStartHours,
                                                         ref long[] mQtys)
        {
            if (this.LogPlanningProcedure) Logger.LogInfo($"Backward_GetPlanTime_BaseOn_NextOps(mBaseOperationCode:{mBaseOperationCode})");

            string FilterExp = "";
            string mAfterwardOpMachineCode = "-1"; // برای نگهداری کد ماشین عملیات پسنیاز
            var drPLanning = Db.DataSet.Tables["Tbl_Planning"].Select("TreeCode = " + mTreeCode + " And SubbatchCode = '" + mSubbatchCode + "' And OperationCode = '" + drAfterOp["CurrentOperationCode"].ToString() + "'");
            FunctionLabel.Text = Conversions.ToString(Operators.AddObject(FunctionLabel.Tag, " GetBackwardPlanningTimeWithAfterwardOps"));
            Application.DoEvents();
            switch (CommonTool.GetRelationType(drAfterOp["RelationType"].ToString()))
            {
                case EnumRelationType.RT_FS:
                case EnumRelationType.RT_SS:
                case EnumRelationType.RT_ASAP:
                {
                    PlanningStartDates[0] = drPLanning[0]["OperationStartDate"].ToString();
                    PlanningStartHours[0] = Conversions.ToDouble(drPLanning[0]["OperationStartHour"]);
                    mAfterwardOpMachineCode = drPLanning[0]["MachineCode"].ToString();
                    if (drPLanning.Length > 1)
                    {
                        // بدست آوردن کوچکترین تاریخ شروع عملیات در بین رکوردهای برنامه ریزی(در صورتیکه عملیات پسنیاز موازی کاری باشد)عملیات پسنیاز
                        for (int I = 1, loopTo = drPLanning.Length - 1; I <= loopTo; I++)
                        {
                            if (String.Compare(PlanningStartDates[0], drPLanning[I]["OperationStartDate"].ToString()) > 0)
                            {
                                PlanningStartDates[0] = drPLanning[I]["OperationStartDate"].ToString();
                                PlanningStartHours[0] = Conversions.ToDouble(drPLanning[I]["OperationStartHour"]);
                                mAfterwardOpMachineCode = drPLanning[I]["MachineCode"].ToString();
                            }
                            else if (PlanningStartDates[0] == drPLanning[I]["OperationStartDate"].ToString())
                            {
                                if (PlanningStartHours[0] > Conversions.ToDouble(drPLanning[I]["OperationStartHour"]))
                                {
                                    PlanningStartHours[0] = Conversions.ToDouble(drPLanning[I]["OperationStartHour"]);
                                    mAfterwardOpMachineCode = drPLanning[I]["MachineCode"].ToString();
                                }
                            }
                        }
                    }

                    break;
                }

                case EnumRelationType.RT_FF:
                {
                    PlanningStartDates[0] = drPLanning[0]["OperationEndDate"].ToString();
                    PlanningStartHours[0] = Conversions.ToDouble(drPLanning[0]["OperationEndHour"]);
                    mAfterwardOpMachineCode = drPLanning[0]["MachineCode"].ToString();
                    if (drPLanning.Length > 1)
                    {
                        // بدست آوردن بزرگترین تاریخ پایان عملیات در بین رکوردهای برنامه ریزی(در صورتیکه عملیات پسنیاز موازی کاری باشد)عملیات پسنیاز
                        for (int I = 1, loopTo1 = drPLanning.Length - 1; I <= loopTo1; I++)
                        {
                            if (String.Compare(PlanningStartDates[0], drPLanning[I]["OperationEndDate"].ToString()) > 0)
                            {
                                PlanningStartDates[0] = drPLanning[I]["OperationEndDate"].ToString();
                                PlanningStartHours[0] = Conversions.ToDouble(drPLanning[I]["OperationEndHour"]);
                                mAfterwardOpMachineCode = drPLanning[I]["MachineCode"].ToString();
                            }
                            else if (PlanningStartDates[0] == drPLanning[I]["OperationEndDate"].ToString())
                            {
                                if (PlanningStartHours[0] < Conversions.ToDouble(drPLanning[I]["OperationEndHour"]))
                                {
                                    PlanningStartHours[0] = Conversions.ToDouble(drPLanning[I]["OperationEndHour"]);
                                    mAfterwardOpMachineCode = drPLanning[I]["MachineCode"].ToString();
                                }
                            }
                        }
                    }

                    break;
                }
            }

            var execMethod = CommonTool.GetExecutionMethod(Db.DataSet.Tables["Tbl_ProductOPCs"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + drAfterOp["PreOperationCode"].ToString() + "'")[0]["ExecutionMethod"].ToString());
            if (execMethod == EnumExecutionMethod.EM_MACHINE)
            //    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(Db.DataSet.Tables["Tbl_ProductOPCs"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + drAfterOp["PreOperationCode"].ToString() + "'")[0]["ExecutionMethod"], EnumExecutionMethod.EM_MACHINE, false)))
            {
                // Get machines order by priority
                FilterExp = "TreeCode=" + mTreeCode + " And OperationCode='" + drAfterOp["PreOperationCode"].ToString() + "'";
                var mMachines = Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].Select(FilterExp, "MachinePriority");
                string mPlanningStartDate = PlanningStartDates[0];
                double mPlanningStartHour = PlanningStartHours[0];
                string StartDate = "0";
                double StartHour = 0.0d;
                foreach (DataRow r in mMachines)
                {
                    string TempSetupStartDate = "0";
                    double TempSetupStartHour = 0.0d;
                    string TempOperationStartDate = "0";
                    double TempOperationStartHour = 0.0d;
                    mPlanningStartDate = PlanningStartDates[0];
                    mPlanningStartHour = PlanningStartHours[0];
                    //string argPlanningStartDate = mPlanningStartDate.ToString();
                    //string argSetupDate = TempSetupStartDate.ToString();
                    //string argOperationDate = TempOperationStartDate.ToString();

                    // بدست آوردن زمان شروع عملیات با توجه به عملیات پسنیاز
                    Get_StartTime_BasedOn_NextOp(drAfterOp["PreOperationCode"].ToString(),
                                                 drAfterOp["CurrentOperationCode"].ToString(),
                                                 mAfterwardOpMachineCode,
                                                 CommonTool.GetRelationType(drAfterOp["RelationType"].ToString()),
                                                 Conversions.ToInteger(drAfterOp["LagTime"]),
                                                 Conversions.ToInteger(drAfterOp["TimeType"]),
                                                 ref mPlanningStartDate, ref mPlanningStartHour,
                                                 ref TempSetupStartDate, ref TempSetupStartHour,
                                                 ref TempOperationStartDate, ref TempOperationStartHour,
                                                 mQtys[0]);

                    TempOperationStartHour = CheckFloating(TempOperationStartHour);
                    mPlanningStartHour = CheckFloating(mPlanningStartHour);
                    if (CommonTool.IsDH1BeforeDH2(TempOperationStartDate, TempOperationStartHour, mPlanningStartDate, mPlanningStartHour))
                    {
                        if (mBaseOperationCode.Equals(drAfterOp["PreOperationCode"].ToString()))
                        {
                            MachineCodes[0] = r["MachineCode"].ToString();
                        }

                        SetupStartDates[0] = TempSetupStartDate;
                        SetupStartHours[0] = TempSetupStartHour;
                        OperationStartDates[0] = TempOperationStartDate;
                        OperationStartHours[0] = TempOperationStartHour;
                        StartDate = mPlanningStartDate;
                        StartHour = mPlanningStartHour;
                    }
                }

                PlanningStartDates[0] = StartDate;
                PlanningStartHours[0] = StartHour;
            }
            else
            {
                var tmp = PlanningStartDates;
                string argPlanningStartDate1 = tmp[0].ToString();
                var tmp1 = SetupStartDates;
                string argSetupDate1 = tmp1[0].ToString();
                var tmp2 = OperationStartDates;
                string argOperationDate1 = tmp2[0].ToString();
                // بدست آوردن زمان شروع عملیات با توجه به عملیات پسنیاز
                Get_StartTime_BasedOn_NextOp(drAfterOp["PreOperationCode"].ToString(),
                                             drAfterOp["CurrentOperationCode"].ToString(),
                                             mAfterwardOpMachineCode,
                                             CommonTool.GetRelationType(drAfterOp["RelationType"].ToString()),
                                             Conversions.ToInteger(drAfterOp["LagTime"]),
                                             Conversions.ToInteger(drAfterOp["TimeType"]),
                                             ref argPlanningStartDate1, ref PlanningStartHours[0],
                                             ref argSetupDate1, ref SetupStartHours[0],
                                             ref argOperationDate1, ref OperationStartHours[0],
                                             mQtys[0]);
            }

           
        }

        private void GetMatchedBackwardPlanningTimeWithAfterwardOps(string mBaseOperationCode,
                                                                    DataRow drAfterOp,
                                                                    ref string[] MachineCodes,
                                                                    ref string[] PlanningStartDates, ref double[] PlanningStartHours,
                                                                    ref string[] SetupStartDates, ref double[] SetupStartHours,
                                                                    ref string[] OperationStartDates, ref double[] OperationStartHours,
                                                                    ref long[] mQtys)
        {
            if (this.LogPlanningProcedure) Logger.LogInfo($"GetMatchedBackwardPlanningTimeWithAfterwardOps(mBaseOperationCode:{mBaseOperationCode})");

            string FilterExp = "";
            string mTempPlanningSD = "0";
            var mTempPlanningSH = default(double);
            string mTempSetupSD = "0";
            double mTempSetupSH = 0.0d;
            string mTempOperationSD = "0";
            double mTempOperationSH = 0.0d;
            string mTempMachineCode = MachineCodes[0];
            string mAfterwardOpMachineCode = "-1"; // برای نگهداری کد ماشین عملیات پسنیاز
            var drPLanning = Db.DataSet.Tables["Tbl_Planning"].Select("TreeCode = " + mTreeCode + " And SubbatchCode = '" + mSubbatchCode + "' And OperationCode = '" + drAfterOp["CurrentOperationCode"].ToString() + "'");
            FunctionLabel.Text = Conversions.ToString(Operators.AddObject(FunctionLabel.Tag, " GetMatchedBackwardPlanningTimeWithAfterwardOps"));
            Application.DoEvents();
            switch (CommonTool.GetRelationType(drAfterOp["RelationType"].ToString()))
            {
                case EnumRelationType.RT_FS:
                case EnumRelationType.RT_SS:
                case EnumRelationType.RT_ASAP:
                {
                    mTempPlanningSD = drPLanning[0]["OperationStartDate"].ToString();
                    mTempPlanningSH = Conversions.ToDouble(drPLanning[0]["OperationStartHour"]);
                    mAfterwardOpMachineCode = Conversions.ToString(drPLanning[0]["MachineCode"]);
                    if (drPLanning.Length > 1)
                    {
                        // بدست آوردن کوچکترین تاریخ شروع عملیات در بین رکوردهای برنامه ریزی(در صورتیکه عملیات پسنیاز موازی کاری باشد)عملیات پسنیاز
                        for (int I = 1, loopTo = drPLanning.Length - 1; I <= loopTo; I++)
                        {
                            if (String.Compare(mTempPlanningSD, drPLanning[I]["OperationStartDate"].ToString()) > 0)
                            {
                                mTempPlanningSD = drPLanning[I]["OperationStartDate"].ToString();
                                mTempPlanningSH = Conversions.ToDouble(drPLanning[I]["OperationStartHour"]);
                                mAfterwardOpMachineCode = drPLanning[I]["MachineCode"].ToString();
                            }
                            else if (mTempPlanningSD == drPLanning[I]["OperationStartDate"].ToString())
                            {
                                if (mTempPlanningSH > Conversions.ToDouble(drPLanning[I]["OperationStartHour"]))
                                {
                                    mTempPlanningSH = Conversions.ToDouble(drPLanning[I]["OperationStartHour"]);
                                    mAfterwardOpMachineCode = drPLanning[I]["MachineCode"].ToString();
                                }
                            }
                        }
                    }

                    break;
                }

                case EnumRelationType.RT_FF:
                {
                    mTempPlanningSD = drPLanning[0]["OperationEndDate"].ToString();
                    mTempPlanningSH = Conversions.ToDouble(drPLanning[0]["OperationEndHour"]);
                    mAfterwardOpMachineCode = Conversions.ToString(drPLanning[0]["MachineCode"]);
                    if (drPLanning.Length > 1)
                    {
                        // بدست آوردن بزرگترین تاریخ پایان عملیات در بین رکوردهای برنامه ریزی(در صورتیکه عملیات پسنیاز موازی کاری باشد)عملیات پسنیاز
                        for (int I = 1, loopTo1 = drPLanning.Length - 1; I <= loopTo1; I++)
                        {
                            if (String.Compare(mTempPlanningSD, drPLanning[I]["OperationEndDate"].ToString()) < 0)
                            {
                                mTempPlanningSD = drPLanning[I]["OperationEndDate"].ToString();
                                mTempPlanningSH = Conversions.ToDouble(drPLanning[I]["OperationEndHour"]);
                                mAfterwardOpMachineCode = drPLanning[I]["MachineCode"].ToString();
                            }
                            else if (mTempPlanningSD == drPLanning[I]["OperationEndDate"].ToString())
                            {
                                if (mTempPlanningSH < Conversions.ToDouble(drPLanning[I]["OperationEndHour"]))
                                {
                                    mTempPlanningSH = Conversions.ToDouble(drPLanning[I]["OperationEndHour"]);
                                    mAfterwardOpMachineCode = drPLanning[I]["MachineCode"].ToString();
                                }
                            }
                        }
                    }

                    break;
                }
            }

            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(Db.DataSet.Tables["Tbl_ProductOPCs"].Select("TreeCode=" + mTreeCode + " And OperationCode='" + drAfterOp["PreOperationCode"].ToString() + "'")[0]["ExecutionMethod"], EnumExecutionMethod.EM_MACHINE, false)))
            {
                FilterExp = "TreeCode=" + mTreeCode + " And OperationCode='" + drAfterOp["PreOperationCode"].ToString() + "'";
                var mMachines = Db.DataSet.Tables["Tbl_ProductOPCsExecutorMachines"].Select(FilterExp, "MachinePriority");
                string mPlanningStartDate = mTempPlanningSD;
                double mPlanningStartHour = mTempPlanningSH;
                string StartDate = "0";
                double StartHour = 0.0d;
                foreach (DataRow r in mMachines)
                {
                    string CalcSetupSD = "0";
                    double CalcSetupSH = 0.0d;
                    string CalcOperationSD = "0";
                    double CalcOperationSH = 0.0d;
                    mPlanningStartDate = mTempPlanningSD;
                    mPlanningStartHour = mTempPlanningSH;
                    string argPlanningStartDate = mPlanningStartDate.ToString();
                    string argSetupDate = CalcSetupSD.ToString();
                    string argOperationDate = CalcOperationSD.ToString();

                    // بدست آوردن زمان شروع عملیات با توجه به عملیات پسنیاز
                    Get_StartTime_BasedOn_NextOp(drAfterOp["PreOperationCode"].ToString(),
                                                 drAfterOp["CurrentOperationCode"].ToString(),
                                                 mAfterwardOpMachineCode,
                                                 CommonTool.GetRelationType(drAfterOp["RelationType"].ToString()),
                                                 Conversions.ToInteger(drAfterOp["LagTime"]),
                                                 Conversions.ToInteger(drAfterOp["TimeType"]),
                                                 ref argPlanningStartDate, ref mPlanningStartHour,
                                                 ref argSetupDate, ref CalcSetupSH,
                                                 ref argOperationDate, ref CalcOperationSH,
                                                 mQtys[0]);
                    StartHour = CheckFloating(StartHour);
                    mPlanningStartHour = CheckFloating(mPlanningStartHour);
                    if (Operators.CompareString(mPlanningStartDate + GetCompeleteHour(mPlanningStartHour), StartDate + GetCompeleteHour(StartHour), false) > 0)
                    {
                        if (mBaseOperationCode.Equals(drAfterOp["PreOperationCode"].ToString()))
                        {
                            mTempMachineCode = r["MachineCode"].ToString();
                        }

                        mTempSetupSD = CalcSetupSD;
                        mTempSetupSH = CalcSetupSH;
                        mTempOperationSD = CalcOperationSD;
                        mTempOperationSH = CalcOperationSH;
                        StartDate = mPlanningStartDate;
                        StartHour = mPlanningStartHour;
                    }
                }

                mTempPlanningSD = StartDate;
                mTempPlanningSH = StartHour;
            }
            else
            {
                string argPlanningStartDate1 = mTempPlanningSD.ToString();
                string argSetupDate1 = mTempSetupSD.ToString();
                string argOperationDate1 = mTempOperationSD.ToString();
                // بدست آوردن زمان شروع عملیات با توجه به عملیات پسنیاز
                Get_StartTime_BasedOn_NextOp(drAfterOp["PreOperationCode"].ToString(),
                                             drAfterOp["CurrentOperationCode"].ToString(),
                                             mAfterwardOpMachineCode,
                                             CommonTool.GetRelationType(drAfterOp["RelationType"].ToString()),
                                             Conversions.ToInteger(drAfterOp["LagTime"]),
                                             Conversions.ToInteger(drAfterOp["TimeType"]),
                                             ref argPlanningStartDate1, ref mTempPlanningSH,
                                             ref argSetupDate1, ref mTempSetupSH,
                                             ref argOperationDate1, ref mTempOperationSH,
                                             mQtys[0]);
            }

            mTempPlanningSH = CheckFloating(mTempPlanningSH);
            PlanningStartHours[0] = CheckFloating(PlanningStartHours[0]);
            if (PlanningStartDates[0] == "0")
            {
                MachineCodes[0] = mTempMachineCode;
                SetupStartDates[0] = mTempSetupSD;
                SetupStartHours[0] = mTempSetupSH;
                OperationStartDates[0] = mTempOperationSD;
                OperationStartHours[0] = mTempOperationSH;
                PlanningStartDates[0] = mTempPlanningSD;
                PlanningStartHours[0] = mTempPlanningSH;
            }
            else if (Operators.CompareString(PlanningStartDates[0] + GetCompeleteHour(PlanningStartHours[0]), mTempPlanningSD + GetCompeleteHour(mTempPlanningSH), false) > 0)
            {
                MachineCodes[0] = mTempMachineCode;
                SetupStartDates[0] = mTempSetupSD;
                SetupStartHours[0] = mTempSetupSH;
                OperationStartDates[0] = mTempOperationSD;
                OperationStartHours[0] = mTempOperationSH;
                PlanningStartDates[0] = mTempPlanningSD;
                PlanningStartHours[0] = mTempPlanningSH;
            }
        }

        private long[] GetLongEmptyArray()
        {
            var mEmpty = new long[] { 0L };
            return mEmpty;
        }

        private double[] GetDoubleEmptyArray()
        {
            var mEmpty = new double[] { 0.0d };
            return mEmpty;
        }

        private int zzGetLastPlanningCode()
        {
            var cm = new SqlCommand("Select No = ISNULL(MAX(PlanningCode),0) From Tbl_Planning ", Module1.cnProductionPlanning);
            return Conversions.ToInteger(cm.ExecuteScalar());
        }


        private EnumStdResult CheckPlaningItemsAndThenSave(ref SqlTransaction trnPlanning, ref string DoneMsg)
        {
            if (this.LogPlanningProcedure) Logger.LogInfo($"CheckPlaningItemsAndThenSave()");

            // prepare confirm plnning form:
            frmPlanningConfirm _frmPlanningConfirm = new frmPlanningConfirm();

            // کنترل صحت برنامه ریزی ساب بچ
            if (ControlPlanItems.Checked)
            {
                // CheckSubbatchPlanningValidation(mSubbatchCode, _frmPlanningConfirm) > 0
                FunctionLabel.Text = Conversions.ToString(Operators.AddObject(FunctionLabel.Tag, " CheckSubbatchPlanningValidation"));
                Application.DoEvents();
                var dvPlanning = Db.DataSet.Tables["Tbl_Planning"].DefaultView;
                DataRow[] drPreOperations;
                DataRow[] drPreOpPlanning;
                int PlanningCinfilicts = 0;


                dvPlanning.RowFilter = "SubbatchCode='" + mSubbatchCode + "'";
                for (int OpCounter = 0, loopTo = dvPlanning.Count - 1; OpCounter <= loopTo; OpCounter++)
                {
                    drPreOperations = Db.DataSet.Tables["Tbl_PreOperations"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("TreeCode=", dvPlanning[OpCounter]["TreeCode"]), " And CurrentOperationCode='"), dvPlanning[OpCounter]["OperationCode"]), "'")));
                    // در صورتیکه فعالیت جاری دارای پیشنیار باشد
                    if (drPreOperations.Length > 0)
                    {
                        for (int PreOpCounter = 0, loopTo1 = drPreOperations.Length - 1; PreOpCounter <= loopTo1; PreOpCounter++)
                        {
                            drPreOpPlanning = Db.DataSet.Tables["Tbl_Planning"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("SubbatchCode='" + mSubbatchCode + "' And OperationCode='", drPreOperations[PreOpCounter]["PreOperationCode"]), "'")));
                            // در صورتیکه تاریخ شروع برنامه ریزی عملیات پیشنیاز بزرگتر(بعداز) عملیات فعلی باشد
                            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectLess(dvPlanning[OpCounter]["OperationStartDate"], drPreOpPlanning[0]["OperationStartDate"], false)))
                            {
                                PlanningCinfilicts += 1;
                                _frmPlanningConfirm.dgStartConfilicts.Rows.Add(dvPlanning[OpCounter]["OperationCode"], Operators.ConcatenateObject(Operators.ConcatenateObject(dvPlanning[OpCounter]["OperationStartDate"], " - "), Module1.GetRegulareHour(Conversions.ToString(dvPlanning[OpCounter]["OperationStartHour"]))), drPreOpPlanning[0]["OperationCode"], Operators.ConcatenateObject(Operators.ConcatenateObject(drPreOpPlanning[0]["OperationStartDate"], " - "), Module1.GetRegulareHour(Conversions.ToString(drPreOpPlanning[0]["OperationStartHour"]))));
                            }
                            else if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dvPlanning[OpCounter]["OperationStartDate"], drPreOpPlanning[0]["OperationStartDate"], false)))
                            {
                                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectLess(dvPlanning[OpCounter]["OperationStartHour"], drPreOpPlanning[0]["OperationStartHour"], false)))
                                {
                                    PlanningCinfilicts += 1;
                                    _frmPlanningConfirm.dgStartConfilicts.Rows.Add(dvPlanning[OpCounter]["OperationCode"], Operators.ConcatenateObject(Operators.ConcatenateObject(dvPlanning[OpCounter]["OperationStartDate"], " - "), Module1.GetRegulareHour(Conversions.ToString(dvPlanning[OpCounter]["OperationStartHour"]))), drPreOpPlanning[0]["OperationCode"], Operators.ConcatenateObject(Operators.ConcatenateObject(drPreOpPlanning[0]["OperationStartDate"], " - "), Module1.GetRegulareHour(Conversions.ToString(drPreOpPlanning[0]["OperationStartHour"]))));
                                }
                            }

                            // در صورتیکه تاریخ پایان برنامه ریزی عملیات پیشنیاز بزرگتر(بعداز) عملیات فعلی باشد
                            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectLess(dvPlanning[OpCounter]["OperationEndDate"], drPreOpPlanning[0]["OperationEndDate"], false)))
                            {
                                PlanningCinfilicts += 1;
                                _frmPlanningConfirm.dgEndConfilicts.Rows.Add(dvPlanning[OpCounter]["OperationCode"], Operators.ConcatenateObject(Operators.ConcatenateObject(dvPlanning[OpCounter]["OperationEndDate"], " - "), Module1.GetRegulareHour(Conversions.ToString(dvPlanning[OpCounter]["OperationEndHour"]))), drPreOpPlanning[0]["OperationCode"], Operators.ConcatenateObject(Operators.ConcatenateObject(drPreOpPlanning[0]["OperationEndDate"], " - "), Module1.GetRegulareHour(Conversions.ToString(drPreOpPlanning[0]["OperationEndHour"]))));
                            }
                            else if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dvPlanning[OpCounter]["OperationEndDate"], drPreOpPlanning[0]["OperationEndDate"], false)))
                            {
                                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectLess(dvPlanning[OpCounter]["OperationEndHour"], drPreOpPlanning[0]["OperationEndHour"], false)))
                                {
                                    PlanningCinfilicts += 1;
                                    _frmPlanningConfirm.dgEndConfilicts.Rows.Add(dvPlanning[OpCounter]["OperationCode"], Operators.ConcatenateObject(Operators.ConcatenateObject(dvPlanning[OpCounter]["OperationEndDate"], " - "), Module1.GetRegulareHour(Conversions.ToString(dvPlanning[OpCounter]["OperationEndHour"]))), drPreOpPlanning[0]["OperationCode"], Operators.ConcatenateObject(Operators.ConcatenateObject(drPreOpPlanning[0]["OperationEndDate"], " - "), Module1.GetRegulareHour(Conversions.ToString(drPreOpPlanning[0]["OperationEndHour"]))));
                                }
                            }
                        }
                    }
                }

                if (PlanningCinfilicts > 0)
                {
                    //var withBlock = My.MyProject.Forms.frmPlanningConfirm;

                    _frmPlanningConfirm.TreeCode = mTreeCode;
                    _frmPlanningConfirm.SubbatchCode = mSubbatchCode;
                    _frmPlanningConfirm.ActiveTransaction = trnPlanning;
                    _frmPlanningConfirm.ActiveDataSet = Db.DataSet;
                    _frmPlanningConfirm.lblSubbatchCode.Text = " کد ساب بچ: " + mSubbatchCode;
                    _frmPlanningConfirm.pnlSubbatch.Visible = false;
                    _frmPlanningConfirm.pnlOperations.Visible = true;
                    var result = _frmPlanningConfirm.ShowDialog();
                    _frmPlanningConfirm.Dispose();

                    switch (result)
                    {
                        case DialogResult.OK:
                        {
                            SaveChanges();
                            break;
                        }

                        case DialogResult.Ignore:
                        {
                            //dvPlanningList[I].CancelEdit();

                            trnPlanning.Rollback();
                            trnPlanning = null;
                            Db.DataSet.RejectChanges();
                            DoneMsg = Conversions.ToString(DoneMsg + Operators.ConcatenateObject(Interaction.IIf(string.IsNullOrEmpty(DoneMsg), "", Constants.vbCrLf), string.Format("برنامه ریزی ساب بچ: {0} انجام نشد -- نظر کاربر", mSubbatchCode)));
                            return EnumStdResult.GoTo_PlanningDone;
                        }

                        case DialogResult.Abort:
                        {
                            //dvPlanningList[I].CancelEdit();
                            Db.DataSet.RejectChanges();
                            trnPlanning.Rollback();
                            trnPlanning = null;
                            for (int K = I, loopTo = dvPlanningList.Count - 1; K <= loopTo; K++)
                            {
                                mSubbatchCode = Conversions.ToString(dvPlanningList[K]["SubbatchCode"]);
                                DoneMsg = Conversions.ToString(DoneMsg + Operators.ConcatenateObject(Interaction.IIf(string.IsNullOrEmpty(DoneMsg), "", Constants.vbCrLf), string.Format("برنامه ریزی ساب بچ: {0} انجام نشد -- نظر کاربر", mSubbatchCode)));
                            }

                            //_frmPlanningConfirm.Dispose();
                            return EnumStdResult.GoTo_EndOfPlanning;
                        }
                    }

                }
            }
            else
            {
                SaveChanges();
            }

            //_frmPlanningConfirm.Dispose();

            return EnumStdResult.Continue_Function;
        }

        private EnumStdResult Check_PlanningStartDate_With_ProductionCallDate(SqlTransaction trnPlanning, ref string DoneMsg, string PSD, string mProductionCallDate, string mDayDate)
        {
            if (this.LogPlanningProcedure) Logger.LogInfo($"Check_PlanningStartDate_With_ProductionCallDate()");

            //return EnumStdResult.Continue_Function;
            frmPlanningConfirm _frmPlanningConfirm = new frmPlanningConfirm();

            if (Operators.CompareString(PSD, "0", false) > 0 && Operators.CompareString(PSD, mProductionCallDate, false) < 0)
            {
                {
                    //var withBlock = My.MyProject.Forms.frmPlanningConfirm;
                    _frmPlanningConfirm.TreeCode = mTreeCode;
                    _frmPlanningConfirm.SubbatchCode = mSubbatchCode;
                    _frmPlanningConfirm.ActiveTransaction = trnPlanning;
                    _frmPlanningConfirm.ActiveDataSet = Db.DataSet;
                    _frmPlanningConfirm.pnlOperations.Visible = false;
                    _frmPlanningConfirm.pnlSubbatch.Visible = true;
                    _frmPlanningConfirm.Size = new Size(new Point(550, 284));
                    _frmPlanningConfirm.dgSubbatch.Rows.Add(mSubbatchCode, "شروع برنامه ریزی انجام شده قبل از تاریخ دستور شروع ساخت است");
                    if (Operators.CompareString(mProductionCallDate, mDayDate, false) >= 0)
                    {
                        _frmPlanningConfirm.cmdTomorrowPlanning.Text = "برنامه ریزی از تاریخ دستور شروع ساخت";
                    }
                    var result = _frmPlanningConfirm.ShowDialog();
                    _frmPlanningConfirm.Dispose();
                    switch (result)
                    {
                        case DialogResult.Retry:
                        {
                            SaveChanges();
                            trnPlanning.Commit();
                            trnPlanning = null;
                            DeleteOldPlanningRows(mSubbatchCode);
                            //withBlock.Dispose();
                            dvPlanningList[I].BeginEdit();
                            dvPlanningList[I]["SubbatchFirstDeliveryDate"] = 0;
                            dvPlanningList[I].EndEdit();
                            SaveChanges();

                            return EnumStdResult.GoTo_RetryPlanning;
                        }

                        case DialogResult.Ignore:
                        {
                            Db.DataSet.RejectChanges();
                            trnPlanning.Rollback();
                            trnPlanning = null;
                            DoneMsg = Conversions.ToString(DoneMsg + Operators.ConcatenateObject(Interaction.IIf(string.IsNullOrEmpty(DoneMsg), "", Constants.vbCrLf), string.Format(" برنامه ریزی ساب بچ {0}  ثبت نشد -- نظر کاربر", mSubbatchCode)));
                            return EnumStdResult.GoTo_PlanningDone;
                        }

                        case DialogResult.Abort:
                        {
                            Db.DataSet.RejectChanges();
                            trnPlanning.Rollback();
                            trnPlanning = null;
                            for (int K = I, loopTo = dvPlanningList.Count - 1; K <= loopTo; K++)
                            {
                                mSubbatchCode = Conversions.ToString(dvPlanningList[K]["SubbatchCode"]);
                                DoneMsg = Conversions.ToString(DoneMsg + Operators.ConcatenateObject(Interaction.IIf(string.IsNullOrEmpty(DoneMsg), "", Constants.vbCrLf), string.Format("برنامه ریزی ساب بچ {0} انجام نشد -- نظر کاربر", mSubbatchCode)));
                            }

                            return EnumStdResult.Exit_For;
                        }
                    }
                }
            }
            else if (Operators.CompareString(PSD, "0", false) > 0 && Operators.CompareString(PSD, mDayDate, false) < 0) // کنترل اینکه تاریخ شروع برنامه ریزی قبل از تاریخ روز جاری نباشد
            {
                {
                    //var withBlock1 = My.MyProject.Forms.frmPlanningConfirm;
                    _frmPlanningConfirm.TreeCode = mTreeCode;
                    _frmPlanningConfirm.SubbatchCode = mSubbatchCode;
                    _frmPlanningConfirm.ActiveTransaction = trnPlanning;
                    _frmPlanningConfirm.ActiveDataSet = Db.DataSet;
                    _frmPlanningConfirm.pnlOperations.Visible = false;
                    _frmPlanningConfirm.pnlSubbatch.Visible = true;
                    _frmPlanningConfirm.Size = new Size(new Point(550, 284));
                    _frmPlanningConfirm.dgSubbatch.Rows.Add(mSubbatchCode, "شروع برنامه ریزی انجام شده قبل از روز جاری است");
                    var result = _frmPlanningConfirm.ShowDialog();
                    _frmPlanningConfirm.Dispose();
                    switch (result)
                    {
                        case DialogResult.Retry:
                        {
                            SaveChanges();
                            trnPlanning.Commit();
                            trnPlanning = null;
                            DeleteOldPlanningRows(mSubbatchCode);

                            dvPlanningList[I].BeginEdit();
                            dvPlanningList[I]["SubbatchFirstDeliveryDate"] = 0;
                            dvPlanningList[I].EndEdit();
                            SaveChanges();

                            return EnumStdResult.GoTo_RetryPlanning;
                        }

                        case DialogResult.Ignore:
                        {
                            Db.DataSet.RejectChanges();
                            trnPlanning.Rollback();
                            trnPlanning = null;
                            DoneMsg = Conversions.ToString(DoneMsg + Operators.ConcatenateObject(Interaction.IIf(string.IsNullOrEmpty(DoneMsg), "", Constants.vbCrLf), string.Format("برنامه ریزی ساب بچ: {0} ثبت نشد -- نظر کاربر", mSubbatchCode)));
                            return EnumStdResult.GoTo_PlanningDone;
                        }

                        case DialogResult.Abort:
                        {
                            Db.DataSet.RejectChanges();
                            trnPlanning.Rollback();
                            trnPlanning = null;
                            for (int K = I, loopTo1 = dvPlanningList.Count - 1; K <= loopTo1; K++)
                            {
                                mSubbatchCode = Conversions.ToString(dvPlanningList[K]["SubbatchCode"]);
                                DoneMsg = Conversions.ToString(DoneMsg + Operators.ConcatenateObject(Interaction.IIf(string.IsNullOrEmpty(DoneMsg), "", Constants.vbCrLf), string.Format("برنامه ریزی ساب بچ: {0} انجام نشد -- نظر کاربر", mSubbatchCode)));
                            }

                            //_frmPlanningConfirm.Dispose();
                            return EnumStdResult.Exit_For;
                        }
                    }
                }
            }

            return EnumStdResult.Continue_Function;
        }

        private void SaveChanges()
        {
            if (this.LogPlanningProcedure) Logger.LogInfo($"SaveChanges()");

            DataSet dsChanges;
            FunctionLabel.Text = Conversions.ToString(Operators.AddObject(FunctionLabel.Tag, " SaveChanges"));
            Application.DoEvents();
            dsChanges = Db.DataSet.GetChanges();
            if (dsChanges is null)
            {
                return;
            }

            if (dsChanges.HasErrors)
            {
                Db.DataSet.RejectChanges();
            }
            else
            {
                daPlanning.Update(dsChanges, "Tbl_Planning");
                daBatch.Update(dsChanges, "Tbl_ProductionBatchs");
                // daSubbatchs.Update(dsChanges, "Tbl_PlanningSubbatchs")
                daSubbatchs.Update(dsChanges, "Tbl_ProductionSubbatchs");
                Db.DataSet.AcceptChanges();
            }

            dsChanges = null;
        }

        private void CreateDataAdapterCommands()
        {
            // --------------------- ایجاد دستورات برای جدول برنامه ریزی ساب بچ های تولید -----------------
            // ایجاد دستور اصلاح رکورد جاری در جدول
            daPlanning.InsertCommand = new SqlCommand("Insert Into Tbl_Planning(PlanningCode,SubbatchCode,TreeCode,OperationCode,SplitNo,MachineCode,OperatorCode," + "PlanningStartDate,PlanningStartHour,PlanningEndDate,PlanningEndHour," + "PlanningDuration,SetupStartDate,SetupStartHour,SetupEndDate,SetupEndHour," + "SetupDuration,OperationStartDate,OperationStartHour,OperationEndDate," + "OperationEndHour,OperationDuration,DetailProductionQuantity,PreviousPlanningCode," + "NextPlanningCode,LatinStartDate,LatinEndDate)" + "Values(@PlanningCode,@SubbatchCode,@TreeCode,@OperationCode,@SplitNo,@MachineCode,@OperatorCode,@PlanningStartDate," + "@PlanningStartHour,@PlanningEndDate,@PlanningEndHour,@PlanningDuration,@SetupStartDate,@SetupStartHour," + "@SetupEndDate,@SetupEndHour,@SetupDuration,@OperationStartDate,@OperationStartHour,@OperationEndDate," + "@OperationEndHour,@OperationDuration,@DetailProductionQuantity,@PreviousPlanningCode,@NextPlanningCode," + "@LatinStartDate,@LatinEndDate)", Module1.cnProductionPlanning);
            {
                var withBlock = daPlanning.InsertCommand;
                withBlock.Parameters.Add("@PlanningCode", SqlDbType.Int, default, "PlanningCode");
                withBlock.Parameters.Add("@SubbatchCode", SqlDbType.VarChar, 50, "SubbatchCode");
                withBlock.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode");
                withBlock.Parameters.Add("@OperationCode", SqlDbType.VarChar, 50, "OperationCode");
                withBlock.Parameters.Add("@SplitNo", SqlDbType.Int, default, "SplitNo");
                withBlock.Parameters.Add("@MachineCode", SqlDbType.VarChar, 50, "MachineCode");
                withBlock.Parameters.Add("@OperatorCode", SqlDbType.VarChar, 50, "OperatorCode");
                withBlock.Parameters.Add("@PlanningStartDate", SqlDbType.VarChar, 8, "PlanningStartDate");
                withBlock.Parameters.Add("@PlanningStartHour", SqlDbType.VarChar, 50, "PlanningStartHour");
                withBlock.Parameters.Add("@PlanningEndDate", SqlDbType.VarChar, 8, "PlanningEndDate");
                withBlock.Parameters.Add("@PlanningEndHour", SqlDbType.VarChar, 50, "PlanningEndHour");
                withBlock.Parameters.Add("@PlanningDuration", SqlDbType.VarChar, 50, "PlanningDuration");
                withBlock.Parameters.Add("@SetupStartDate", SqlDbType.VarChar, 8, "SetupStartDate");
                withBlock.Parameters.Add("@SetupStartHour", SqlDbType.VarChar, 50, "SetupStartHour");
                withBlock.Parameters.Add("@SetupEndDate", SqlDbType.VarChar, 8, "SetupEndDate");
                withBlock.Parameters.Add("@SetupEndHour", SqlDbType.VarChar, 50, "SetupEndHour");
                withBlock.Parameters.Add("@SetupDuration", SqlDbType.VarChar, 50, "SetupDuration");
                withBlock.Parameters.Add("@OperationStartDate", SqlDbType.VarChar, 8, "OperationStartDate");
                withBlock.Parameters.Add("@OperationStartHour", SqlDbType.VarChar, 50, "OperationStartHour");
                withBlock.Parameters.Add("@OperationEndDate", SqlDbType.VarChar, 8, "OperationEndDate");
                withBlock.Parameters.Add("@OperationEndHour", SqlDbType.VarChar, 50, "OperationEndHour");
                withBlock.Parameters.Add("@OperationDuration", SqlDbType.VarChar, 50, "OperationDuration");
                withBlock.Parameters.Add("@DetailProductionQuantity", SqlDbType.Int, default, "DetailProductionQuantity");
                withBlock.Parameters.Add("@PreviousPlanningCode", SqlDbType.Int, default, "PreviousPlanningCode");
                withBlock.Parameters.Add("@NextPlanningCode", SqlDbType.Int, default, "NextPlanningCode");
                withBlock.Parameters.Add("@LatinStartDate", SqlDbType.VarChar, 10, "LatinStartDate");
                withBlock.Parameters.Add("@LatinEndDate", SqlDbType.VarChar, 10, "LatinEndDate");
            }
            // ایجاد دستور ویرایش رکورد در جدول برنامه ریزی
            daPlanning.UpdateCommand = new SqlCommand("Update Tbl_Planning Set SubbatchCode=@SubbatchCode,TreeCode=@TreeCode,OperationCode=@OperationCode,SplitNo=@SplitNo," + "MachineCode=@MachineCode,OperatorCode=@OperatorCode,PlanningStartDate=@PlanningStartDate,PlanningStartHour=@PlanningStartHour," + "PlanningEndDate=@PlanningEndDate,PlanningEndHour=@PlanningEndHour,PlanningDuration=@PlanningDuration,SetupStartDate=@SetupStartDate," + "SetupStartHour=@SetupStartHour,SetupEndDate=@SetupEndDate,SetupEndHour=@SetupEndHour,SetupDuration=@SetupDuration," + "OperationStartDate=@OperationStartDate,OperationStartHour=@OperationStartHour,OperationEndDate=@OperationEndDate," + "OperationEndHour=@OperationEndHour,OperationDuration=@OperationDuration,DetailProductionQuantity=@DetailProductionQuantity," + "PreviousPlanningCode=@PreviousPlanningCode,NextPlanningCode=@NextPlanningCode,LatinStartDate=@LatinStartDate,LatinEndDate=@LatinEndDate Where PlanningCode=@PlanningCode", Module1.cnProductionPlanning);
            {
                var withBlock1 = daPlanning.UpdateCommand;
                withBlock1.Parameters.Add("@SubbatchCode", SqlDbType.VarChar, 50, "SubbatchCode");
                withBlock1.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode");
                withBlock1.Parameters.Add("@OperationCode", SqlDbType.VarChar, 50, "OperationCode");
                withBlock1.Parameters.Add("@SplitNo", SqlDbType.Int, default, "SplitNo");
                withBlock1.Parameters.Add("@MachineCode", SqlDbType.VarChar, 50, "MachineCode");
                withBlock1.Parameters.Add("@OperatorCode", SqlDbType.VarChar, 50, "OperatorCode");
                withBlock1.Parameters.Add("@PlanningStartDate", SqlDbType.VarChar, 8, "PlanningStartDate");
                withBlock1.Parameters.Add("@PlanningStartHour", SqlDbType.VarChar, 50, "PlanningStartHour");
                withBlock1.Parameters.Add("@PlanningEndDate", SqlDbType.VarChar, 8, "PlanningEndDate");
                withBlock1.Parameters.Add("@PlanningEndHour", SqlDbType.VarChar, 50, "PlanningEndHour");
                withBlock1.Parameters.Add("@PlanningDuration", SqlDbType.VarChar, 50, "PlanningDuration");
                withBlock1.Parameters.Add("@SetupStartDate", SqlDbType.VarChar, 8, "SetupStartDate");
                withBlock1.Parameters.Add("@SetupStartHour", SqlDbType.VarChar, 50, "SetupStartHour");
                withBlock1.Parameters.Add("@SetupEndDate", SqlDbType.VarChar, 8, "SetupEndDate");
                withBlock1.Parameters.Add("@SetupEndHour", SqlDbType.VarChar, 50, "SetupEndHour");
                withBlock1.Parameters.Add("@SetupDuration", SqlDbType.VarChar, 50, "SetupDuration");
                withBlock1.Parameters.Add("@OperationStartDate", SqlDbType.VarChar, 8, "OperationStartDate");
                withBlock1.Parameters.Add("@OperationStartHour", SqlDbType.VarChar, 50, "OperationStartHour");
                withBlock1.Parameters.Add("@OperationEndDate", SqlDbType.VarChar, 8, "OperationEndDate");
                withBlock1.Parameters.Add("@OperationEndHour", SqlDbType.VarChar, 50, "OperationEndHour");
                withBlock1.Parameters.Add("@OperationDuration", SqlDbType.VarChar, 50, "OperationDuration");
                withBlock1.Parameters.Add("@DetailProductionQuantity", SqlDbType.Int, default, "DetailProductionQuantity");
                withBlock1.Parameters.Add("@PreviousPlanningCode", SqlDbType.Int, default, "PreviousPlanningCode");
                withBlock1.Parameters.Add("@NextPlanningCode", SqlDbType.Int, default, "NextPlanningCode");
                withBlock1.Parameters.Add("@LatinStartDate", SqlDbType.VarChar, 10, "LatinStartDate");
                withBlock1.Parameters.Add("@LatinEndDate", SqlDbType.VarChar, 10, "LatinEndDate");
                withBlock1.Parameters.Add("@PlanningCode", SqlDbType.Int, default, "PlanningCode").SourceVersion = DataRowVersion.Original;
            }
            // ایجاد دستور حذف رکورد جاری در جدول
            daPlanning.DeleteCommand = new SqlCommand("Delete From Tbl_Planning Where PlanningCode=@PlanningCode", Module1.cnProductionPlanning);
            {
                var withBlock2 = daPlanning.DeleteCommand;
                withBlock2.Parameters.Add("@PlanningCode", SqlDbType.Int, default, "PlanningCode");
                withBlock2.Parameters[0].Direction = ParameterDirection.Input;
                withBlock2.Parameters[0].SourceVersion = DataRowVersion.Original;
            }

            // --------------------- ایجاد دستورات برای جدول بج های تولید -----------------
            // ایجاد دستور اصلاح رکورد جاری در جدول
            daBatch.UpdateCommand = new SqlCommand("Update Tbl_ProductionBatchs Set FirstDelivaryDate=@FirstDelivaryDate,PlaningStartDate=@PlaningStartDate,RealStartDate=@RealStartDate Where BatchCode=@BatchCode", Module1.cnProductionPlanning);
            {
                var withBlock3 = daBatch.UpdateCommand;
                withBlock3.Parameters.Add("@FirstDelivaryDate", SqlDbType.VarChar, 8, "FirstDelivaryDate");
                withBlock3.Parameters.Add("@PlaningStartDate", SqlDbType.VarChar, 8, "PlaningStartDate");
                withBlock3.Parameters.Add("@RealStartDate", SqlDbType.VarChar, 8, "RealStartDate");
                withBlock3.Parameters.Add("@BatchCode", SqlDbType.VarChar, 20, "BatchCode");
                withBlock3.Parameters[3].Direction = ParameterDirection.Input;
                withBlock3.Parameters[3].SourceVersion = DataRowVersion.Original;
            }

            // --------------------- ایجاد دستورات برای جدول ساب بچهای بج های تولید -----------------
            // ایجاد دستور اصلاح رکورد جاری در جدول
            daSubbatchs.UpdateCommand = new SqlCommand("Update Tbl_ProductionSubbatchs Set PlanningStartDate=@PlanningStartDate,PlanningStartHour=@PlanningStartHour,PlanningEndDate=@PlanningEndDate,PlanningEndHour=@PlanningEndHour,SubbatchFirstDeliveryDate=@SubbatchFirstDeliveryDate Where SubbatchCode=@SubbatchCode", Module1.cnProductionPlanning);
            {
                var withBlock4 = daSubbatchs.UpdateCommand;
                withBlock4.Parameters.Add("@PlanningStartDate", SqlDbType.VarChar, 8, "PlanningStartDate");
                withBlock4.Parameters.Add("@PlanningStartHour", SqlDbType.VarChar, 50, "PlanningStartHour");
                withBlock4.Parameters.Add("@PlanningEndDate", SqlDbType.VarChar, 8, "PlanningEndDate");
                withBlock4.Parameters.Add("@PlanningEndHour", SqlDbType.VarChar, 50, "PlanningEndHour");
                withBlock4.Parameters.Add("@SubbatchFirstDeliveryDate", SqlDbType.VarChar, 8, "SubbatchFirstDeliveryDate");
                withBlock4.Parameters.Add("@SubbatchCode", SqlDbType.VarChar, 50, "SubbatchCode");
                withBlock4.Parameters[5].Direction = ParameterDirection.Input;
                withBlock4.Parameters[5].SourceVersion = DataRowVersion.Default;
            }
        }

        private void Update_Batch_PlanningStartDate(SqlTransaction trnPlanning, string Batchcode)
        {
            // ثبت شروع برنامه ریزی بچ
            var cmSetBatchPlanningDate = new SqlCommand($"Select IsNull(Min(PlanningStartDate),0) From Tbl_Planning Where SubbatchCode IN (Select SubbatchCode From Tbl_ProductionSubbatchs Where BatchCode = '{Batchcode}')", Module1.cnProductionPlanning);
            cmSetBatchPlanningDate.Transaction = trnPlanning;
            string mBatchPlanningStart = cmSetBatchPlanningDate.ExecuteScalar().ToString();
            if (!DBNull.Value.Equals(mBatchPlanningStart))
            {
                cmSetBatchPlanningDate.CommandText = $"Update Tbl_ProductionBatchs Set PlaningStartDate = '{mBatchPlanningStart}' Where BatchCode = '{Batchcode}'";
                cmSetBatchPlanningDate.ExecuteNonQuery();
            }
        }

        private void Update_SubBatch_PlanningStartDate(SqlTransaction trnPlanning, string SubbatchCode)
        {
            // Update subBatch palnnig start date
            var cm = new SqlCommand($"Select IsNull(Min(PlanningStartDate),0) From Tbl_Planning Where SubbatchCode ='{SubbatchCode}'", Module1.cnProductionPlanning, trnPlanning);
            cm.Transaction = trnPlanning;
            string StartDate = cm.ExecuteScalar().ToString();
            if (!DBNull.Value.Equals(StartDate))
            {
                cm.CommandText = $"Update Tbl_ProductionSubbatchs Set PlanningStartDate = '{StartDate}' Where SubBatchcode = '{SubbatchCode}'";
                cm.ExecuteNonQuery();
            }
        }
        /* TODO ERROR: Skipped EndRegionDirectiveTrivia *//* TODO ERROR: Skipped RegionDirectiveTrivia */
        private void Update_FormControls()
        {
            Cursor = Cursors.WaitCursor;
            FunctionLabel.Text = "Start Planning";
            cmdPlanning.Visible = false;
            cmdExit.Visible = false;
            Label1.Visible = true;
            lblCurrentSubbatchCode.Visible = true;
            Label2.Visible = true;
            lblCurrentOperationCode.Visible = true;
            lblWaiting.Visible = true;
            lblCurrentSubbatchCode.Text = "";
            lblCurrentOperationCode.Text = "";
            Application.DoEvents();
        }

        private void Delete_OldPlanningItems()
        {
            if (this.LogPlanningProcedure) Logger.LogInfo($"Delete_OldPlanningItems()");

            // حذف برنامه ریزی های قبلی ساب بچ جاری
            var loopTo = dvPlanningList.Count - 1;
            for (I = 0; I <= loopTo; I++)
                DeleteOldPlanningRows(Conversions.ToString(dvPlanningList[I]["SubbatchCode"]));
            SaveChanges();
        }

        private void Fill_My_ds_Calendar()
        {
            if (this.LogPlanningProcedure) Logger.LogInfo($"Fill_My_ds_Calendar()");

            var My_cn = new SqlConnection(Module1.PlanningCnnStr);
            My_cn.Open();
            var Myda_calender = new SqlDataAdapter("select * from Tbl_HoliDays", My_cn);
            Myds_Calender = new DataSet();
            Myds_Calender.Tables.Add("Tbl_HoliDays");
            Myds_Calender.Tables.Add("Tbl_CalendarParticularDays");
            Myds_Calender.Tables.Add("Tbl_CalendarShifts");
            Myds_Calender.Tables.Add("Tbl_CalendarParticularShifts");
            Myds_Calender.Tables.Add("Tbl_CalendarDays");
            Myds_Calender.Tables.Add("Tbl_CalendarDaysDownTimes");
            Myds_Calender.Tables.Add("Tbl_ParticularShiftDownTimes");
            Myds_Calender.Tables.Add("Tbl_CalendarShiftDownTimes");

            // Myda_calender.FillSchema(Myds_Calender, SchemaType.Source, "Tbl_Holiday")

            Myda_calender.Fill(Myds_Calender.Tables["Tbl_HoliDays"]);
            Myda_calender.SelectCommand.CommandText = "select * from Tbl_CalendarParticularDays";
            Myda_calender.Fill(Myds_Calender.Tables["Tbl_CalendarParticularDays"]);
            Myda_calender.SelectCommand.CommandText = "select * from Tbl_CalendarShifts";
            Myda_calender.Fill(Myds_Calender.Tables["Tbl_CalendarShifts"]);
            Myda_calender.SelectCommand.CommandText = "select * from Tbl_CalendarParticularShifts";
            Myda_calender.Fill(Myds_Calender.Tables["Tbl_CalendarParticularShifts"]);

            Myda_calender.SelectCommand.CommandText = "select * from Tbl_CalendarDays";
            Myda_calender.Fill(Myds_Calender.Tables["Tbl_CalendarDays"]);

            Myda_calender.SelectCommand.CommandText = "select * from Tbl_CalendarDaysDownTimes";
            Myda_calender.Fill(Myds_Calender.Tables["Tbl_CalendarDaysDownTimes"]);

            Myda_calender.SelectCommand.CommandText = "select * from Tbl_ParticularShiftDownTimes";
            Myda_calender.Fill(Myds_Calender.Tables["Tbl_ParticularShiftDownTimes"]);

            Myda_calender.SelectCommand.CommandText = "select * from Tbl_CalendarShiftDownTimes";
            Myda_calender.Fill(Myds_Calender.Tables["Tbl_CalendarShiftDownTimes"]);

            My_cn.Close();
        }

        private void FillPlaningDataSet()
        {
            if (this.LogPlanningProcedure) Logger.LogInfo($"FillPlaningDataSet()");


            string Query;
            Db = new MyDB();
            Db.ResetTableID("Tbl_Planning", "PlanningCode");
            Query = "Select Distinct A.*, Case ISNULL(B.ProductionCode, 0) When 0 Then 0 Else 1 End AS ProductionCode " + "From   dbo.Tbl_Planning A LEFT OUTER JOIN " + "       dbo.Tbl_RealProduction B ON A.PlanningCode = B.PlanningCode";
            Db.ReadData("Tbl_Planning", Query);
            Db.ReadData("Tbl_ProductionBatchs", "Select * From Tbl_ProductionBatchs");
            Query = "Select A.StopPlanning,A.ProductionPriority,D.ProductCode,B.SequenceNo AS BatchPriority,A.SubbatchCode," + "       A.ProductionQuantityinSubbatch,A.SubbatchFirstDeliveryDate,A.PlanningStartDate," + "       (Select Min(StartDate) From dbo.Tbl_RealProduction Where SubbatchCode = A.SubbatchCode)As RealStartDate," + "       IsNull(dbo.GetSubbatchProductionProgress(A.SubbatchCode, B.ProductTreeCode),0) As ProductionProgress," + "       A.BatchCode,A.PlanningStartHour,A.PlanningEndDate,A.PlanningEndHour,A.SubbatchNo,B.ProductTreeCode,A.TransferMinimumQuantity," + "      (Select IsNull(Sum(IntactQuantity),0) From Tbl_RealProduction Where SubbatchCode=A.SubbatchCode And OperationCode IN (Select OperationCode From dbo.Tbl_OperationNetworkPaths Where TreeCode=B.ProductTreeCode And PathCode=1 And ItemPriority=(Select Count(ItemPriority) From dbo.Tbl_OperationNetworkPaths Where TreeCode=B.ProductTreeCode And PathCode=1))) As ProductionQty," + "       B.FirstDelivaryDate,B.FinishedDate " + "From   dbo.Tbl_ProductionSubbatchs A INNER JOIN " + "       dbo.Tbl_ProductionBatchs B ON A.BatchCode = B.BatchCode INNER JOIN " + "       dbo.Tbl_ProductTree D ON B.ProductTreeCode = D.TreeCode Where B.FinishedDate Is Null Or B.FinishedDate = '0' Or B.FinishedDate = ''";
            Db.ReadData("V_ProductionSubbatchs", Query);
            Db.ReadData("Tbl_ProductionSubbatchs", "SELECT * FROM Tbl_ProductionSubbatchs");
            Db.ReadData("Tbl_Calendar", "Select * From Tbl_Calendar");
            Db.ReadData("Tbl_Machines", "Select * From Tbl_Machines");
            Db.ReadData("Tbl_ProductTree", "Select * From Tbl_ProductTree");
            Db.ReadData("Tbl_ProductOPCs", "Select * From Tbl_ProductOPCs");
            Db.ReadData("Tbl_PreOperations", "Select Distinct * From Tbl_PreOperations");
            Db.ReadData("Tbl_ProductOPCsExecutorMachines", "Select * From Tbl_ProductOPCsExecutorMachines");
            Db.ReadData("Tbl_OperationMaterials", "Select * From Tbl_OperationMaterials");
            Db.ReadData("Tbl_ContractorOperations", "Select * From Tbl_ContractorOperations");
            Query = @"Select A.*,
                    (Select Convert(varchar, Sum(Convert(float, ExecutionTime))) 
                    From   dbo.Tbl_OperationNetworkPaths 
                     Where  TreeCode=A.TreeCode AND PathCode=A.PathCode) AS PathTime 
                 From   dbo.Tbl_OperationNetworkPaths A     ";
            Db.ReadData("Tbl_OperationNetworkPaths", Query);
            Query = " SELECT ID,MachineCode,StartDate,dbo.GetFloatingTimeFromRegularTime([StartHour]) as StartHour,EndDate,dbo.GetFloatingTimeFromRegularTime([EndHour]) as EndHour " + " ,Description,ReasonCode  FROM Tbl_MachineNotAvailableTimes ";
            Db.ReadData("Tbl_MachineNotAvailableTimes", Query);
            Db.ReadData("Tbl_ProductionSubbatchsDetail", "Select * From Tbl_ProductionSubbatchsDetail");
        }

        private void FormLoad()
        {
            // Try
            planTool = new PlanTool();
            CreateDataAdapterCommands();
            FillPlaningDataSet();
            // dsPlanning.Tables("Tbl_PlanningSubbatchs").DefaultView.RowFilter = "ProductionPriority > 0 And FinishedDate <= 0" ' And (RealStartDate Is Null Or Convert(RealStartDate,'System.String')='' Or RealStartDate=0)"
            // dvPlanningList = dsPlanning.Tables("Tbl_PlanningSubbatchs").DefaultView
            Db.DataSet.Tables["V_ProductionSubbatchs"].DefaultView.RowFilter = "ProductionPriority > 0 And FinishedDate <= 0"; // And (RealStartDate Is Null Or Convert(RealStartDate,'System.String')='' Or RealStartDate=0)"
            dvPlanningList = Db.DataSet.Tables["V_ProductionSubbatchs"].DefaultView;
            dvPlanningList.Sort = "ProductionPriority,SubbatchNo"; // "BatchPriority,SubbatchNo"
            SetGridColumns(dvPlanningList);

            var appSettings = ConfigurationManager.AppSettings;
            bool.TryParse(appSettings["LogPlanningProcedure"], out this.LogPlanningProcedure);

        }

        private void SetCommandsTransaction(SqlTransaction trnPlanning)
        {
            daPlanning.DeleteCommand.Transaction = trnPlanning;
            daPlanning.InsertCommand.Transaction = trnPlanning;
            daPlanning.UpdateCommand.Transaction = trnPlanning;
            daSubbatchs.UpdateCommand.Transaction = trnPlanning;
        }

        private void UpdatePlannigProgressControls(string subBatchCode)
        {
            lblCurrentSubbatchCode.Text = mSubbatchCode;
            lblCurrentOperationCode.Text = "";
            lblCurrentOperationCode.Tag = "";
            Application.DoEvents();
        }

        #region Commom Planning Methods
        private int GetOperationPlanningQuantity(string operationCode)
        {
            if (this.LogPlanningProcedure) Logger.LogInfo($"GetOperationPlanningQuantity(operationCode:{operationCode})");

            int mPlanningQuantity = -1;
            using (var cn = new SqlConnection(PlanningCnnStr))
            {
                cn.Open();
                var cm = new SqlCommand("Select RequirementQuantity From Tbl_ProductionSubbatchsDetail Where SubbatchCode='" + mSubbatchCode + "' And DetailCode IN (Select DetailCode From Tbl_ProductOPCs Where TreeCode = " + mTreeCode + " And OperationCode = '" + operationCode + "')", cn);
                var dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    mPlanningQuantity = int.Parse(dr["RequirementQuantity"].ToString());
                }

                dr.Close();
                cn.Close();
            }

            if (mPlanningQuantity < 0)
            {
                mPlanningQuantity = 0;
                Logger.SaveError("GetOperationPlanningQuantity", "تعداد مورد نیاز برای برنامه ریزی عملیات {" + operationCode + "} در ساب بچ {" + mSubbatchCode + "{ یافت نشد");
            }

            return mPlanningQuantity;
        }
              
        private bool HasNotPlanned(string treeCode, string subbatchCode, string operationCode)
        {
            var flag = Db.DataSet.Tables["Tbl_Planning"].Select($"TreeCode={treeCode} And SubbatchCode='{subbatchCode}' And OperationCode='{operationCode}'").Length == 0;
            return flag;
        }
        #endregion

        #region Backward Planning Methods
        #endregion

        #region Forward Planning Methods
        private void Forward_OperationPlan(PlanOperation planItem)
        {
            FunctionLabel.Tag = $"Op = {planItem.Op.Code} : ";
            FunctionLabel.Text = FunctionLabel.Tag + "Forward_OperationPlan";
            if (this.LogPlanningProcedure) Logger.LogInfo($"Forward_OperationPlan(Op: {planItem.Op.Code})");

            // در صورتیکه برای فعالیت جاری، قبلا برنامه ریزی نشده باشد
            if (HasNotPlanned(planItem.TreeCode, planItem.SubbatchCode, planItem.Op.Code))
            {
                if (planItem.PreOps.Count > 0) // در صورتیکه پیشنیاز داشته باشد
                {
                    foreach (PreOperation preOp in planItem.PreOps)
                    {
                        if (preOp.Code != planItem.Op.Code)
                        {
                            if (!IsHasProduction(mSubbatchCode, mTreeCode, preOp.Code))
                            {
                                // در صورتیکه عملیات پیشنیاز برنامه ریزی نشده باشد
                                if (HasNotPlanned(planItem.TreeCode, planItem.SubbatchCode, preOp.Code))
                                {
                                    // برنامه ریزی عملیات پیشنیاز
                                    var prePlanItem = planTool.CreatePlanOpertion(planItem.SubbatchCode, planItem.TreeCode, preOp.Code);
                                    prePlanItem.SubbatchStartDate = planItem.SubbatchStartDate;
                                    Forward_OperationPlan(prePlanItem);
                                }
                            }
                        }
                    }
                }

                lblCurrentOperationCode.Text = planItem.Op.Code;
                Application.DoEvents();

                if (planItem.Qty > 0)
                {
                    // بدست آوردن اولین(زودترین) زمان اجرای فعالیت
                    Forward_GetPlanItemDetails(ref planItem);

                    // ثبت مشخصات برنامه ریزی انجام شده برای عملیات در بانک اطلاعاتی
                    SaveNewPlanItem(planItem, ref trnPlanning);
                }
            }
        }

        /// <summary>
        /// این تابع اولین(زودترین) زمان اجرای فعالیت را محاسبه می کند
        /// </summary>
        private void Forward_GetPlanItemDetails(ref PlanOperation planItem)
        {
            FunctionLabel.Text = FunctionLabel.Tag.ToString() + "Forward_GetPlanItemDetails";
            Application.DoEvents();
            if (this.LogPlanningProcedure) Logger.LogInfo($"Forward_GetPlanItemDetails(Op: {planItem.Op.Code})");

            planItem.planningRec = new PlanningRec();
            Forward_GetOpEarliestExecutionTime(ref planItem);


        }

        private void Forward_GetOpEarliestExecutionTime(ref PlanOperation planItem)
        {
            FunctionLabel.Text = FunctionLabel.Tag.ToString() + " Forward_GetOpEarliestExecutionTime";
            Application.DoEvents();
            if (this.LogPlanningProcedure) Logger.LogInfo($"Forward_GetOpEarliestExecutionTime(Op: {planItem.Op.Code})");

            planItem.planningRec = new PlanningRec();
            // بدست آوردن زمان اجرای عملیات برای تمامی ماشین ها به صورت تکی
            for (int machineIndex = 0; machineIndex < planItem.Op.Machines.Count; machineIndex++)
            {

                // بدست آوردن اولین زمان ممکن برای اجرای فعالیت

                var planningRec = Forward_GetMachineAccessibleTime(ref planItem, planItem.Op.Machines[machineIndex]);

                planItem.planningRecs.Add(planningRec);
                if (machineIndex == 0)
                    planItem.planningRec = planningRec;
            }

            // بدست آوردن زودترین زمان انجام عملیات با استفاده از یک ماشین
            for (int I = 0, loopTo2 = planItem.planningRecs.Count - 1; I <= loopTo2; I++)
            {
                if (double.Parse(planItem.planningRecs[I].OperationEndDate + planItem.planningRecs[I].OperationEndHour) < double.Parse(planItem.planningRec.OperationEndDate + planItem.planningRec.OperationEndHour))
                {
                    planItem.planningRec = planItem.planningRecs[I];
                }
            }

        }

        private (string, string) Forward_GetPlanningTimeWithPreOps(PlanOperation planItem, int calendarCode)
        {
            if (this.LogPlanningProcedure) Logger.LogInfo($"Forward_GetPlanningTimeWithPreOps(Op: {planItem.Op.Code}, Calendar: {calendarCode})");

            string TempOpStartDate = "0";
            string TempOpStartHour = "0.0";
            string startDate = planItem.SubbatchStartDate.ToString();
            string startHour = GetCompeleteHour(double.Parse(GetCalendarStart(calendarCode.ToString())));

            string FilterExp = "";

            FunctionLabel.Text = Conversions.ToString(Operators.AddObject(FunctionLabel.Tag, " Forward_GetPlanningTimeWithPreOps"));
            Application.DoEvents();

            foreach (PreOperation preOp in planItem.PreOps)
            {
                // یافتن مشخصات برنامه ریزی فعالیت پیشنیاز 
                FilterExp = $"SubbatchCode='{planItem.SubbatchCode}' And TreeCode={planItem.TreeCode} And OperationCode='{preOp.Code}'";
                var drPlanning = Db.DataSet.Tables["Tbl_Planning"].Select(FilterExp, "OperationEndDate,OperationEndHour");
                if (drPlanning.Length > 0)
                {
                    double DurationPreOp = double.Parse(drPlanning[0]["PlanningDuration"].ToString());
                    switch (preOp.RelationType)
                    {
                        case EnumRelationType.RT_FS:
                        case EnumRelationType.RT_FF:
                        {
                            TempOpStartDate = drPlanning[0]["OperationEndDate"].ToString();
                            TempOpStartHour = drPlanning[0]["OperationEndHour"].ToString();
                            break;
                        }

                        case EnumRelationType.RT_SS:
                        case EnumRelationType.RT_SF:
                        case EnumRelationType.RT_ASAP:
                        {
                            TempOpStartDate = drPlanning[0]["OperationStartDate"].ToString();
                            TempOpStartHour = drPlanning[0]["OperationStartHour"].ToString();
                            break;
                        }
                    }
                }

                TempOpStartHour = GetCompeleteHour(double.Parse(TempOpStartHour)).ToString();
                (TempOpStartDate, TempOpStartHour) = GetEndDateHour(calendarCode, TempOpStartDate.ToString(), TempOpStartHour.ToString(), preOp.LT);
                if (double.Parse(TempOpStartDate + TempOpStartHour) > double.Parse(startDate + startHour))
                {
                    startDate = TempOpStartDate;
                    startHour = TempOpStartHour;
                }
            }

            return (startDate.ToString(), startHour.ToString());
        }

        /// <summary>
        /// این تابع اولین(زودترین) زمان در دسترس فعالیت بر اساس نحوۀ اجرای آن را محاسبه و باز می گرداند
        /// </summary>
        private PlanningRec Forward_GetMachineAccessibleTime(ref PlanOperation planItem, Machine machine)
        {
            FunctionLabel.Text = Conversions.ToString(Operators.ConcatenateObject(FunctionLabel.Tag, "Forward_GetMachineAccessibleTime"));
            Application.DoEvents();
            if (this.LogPlanningProcedure) Logger.LogInfo($"Forward_GetMachineAccessibleTime(Op: {planItem.Op.Code}, machine: {machine.MachineCode})");

            double OpSpeed, LT;
            var planningRec = new PlanningRec();
            int calendarCode = 0;

            planningRec.MachineCode = machine.MachineCode;
            planningRec.OperationCode = planItem.Op.Code;
            planningRec.ProductionQty = planItem.Qty;
            planningRec.SetupDuration = 0;

            //get the planning start date and time:
            switch (planItem.Op.ExecutionMethod) // تعیین نحوۀ اجرای فعالیت
            {
                case EnumExecutionMethod.EM_MACHINE: // فعالیت با ماشین انجام می شود
                    calendarCode = machine.CalendarCode;
                    break;

                case EnumExecutionMethod.EM_NONMACHINE: // بدون ماشین و با اپراتور انجام می شود
                    calendarCode = machine.CalendarCode;
                    break;

                case EnumExecutionMethod.EM_CONTRACTOR: // فعالیت پیمانکاری انجام می شود
                    calendarCode = planItem.Op.Contractor.CalendarCode;
                    break;
            }

            // Get the start of plan based on the pre operations:-
            (planningRec.PlanningStartDate, planningRec.PlanningStartHour) = Forward_GetPlanningTimeWithPreOps(planItem, calendarCode);
            while (CheckIsHoliday(calendarCode.ToString(), planningRec.PlanningStartDate))
            {
                planningRec.PlanningStartDate = FarsiDateFunctions.AddToDate(planningRec.PlanningStartDate, "00000001");
                planningRec.PlanningStartHour = "0";
            }

            if (planningRec.PlanningStartHour == "0")
            {   //Make sure the start hour is coorect when we pass holiday days:-
                planningRec.PlanningStartHour = GetCalendarStart(calendarCode.ToString());
            }


            switch (planItem.Op.ExecutionMethod) // تعیین نحوۀ اجرای فعالیت
            {
                case EnumExecutionMethod.EM_MACHINE: // فعالیت با ماشین انجام می شود
                    planningRec.OperationDuration = planItem.Qty * machine.OneDuration;
                    planningRec.SetupDuration = machine.SetupDuration;
                    planningRec.PlanningDuration = planningRec.OperationDuration + machine.SetupDuration;

                    // بدست آوردن اولین زمان در دسترس برای  ماشین
                    Forward_GetMachineFirstAccessibleTime(planItem, ref planningRec, machine);
                    break;


                case EnumExecutionMethod.EM_NONMACHINE: // بدون ماشین و با اپراتور انجام می شود

                    // بدست آوردن طول زمان اجرای عملیات
                    LT = planItem.Qty / Conversions.ToDouble(GetOpExecutionSpeed(EnumExecutionMethod.EM_NONMACHINE, mTreeCode, planItem.Op.Code));
                    // اضافه کردن ساعات بدست آمده به تاریخ
                    //double StartHour = double.Parse(planningRec.PlanningStartHour);
                    //string StartDate = planningRec.PlanningStartDate;
                    //GetNewDate(calendarCode.ToString(), ref StartDate, ref StartHour, LT);
                    //planningRec.PlanningStartHour = StartHour.ToString();
                    //planningRec.PlanningStartDate = StartDate;
                    planningRec.OperationStartDate = planningRec.PlanningStartDate;
                    planningRec.OperationStartHour = planningRec.PlanningStartHour;
                    planningRec.OperationDuration = LT;
                    planningRec.PlanningDuration = LT;
                    break;

                case EnumExecutionMethod.EM_CONTRACTOR: // فعالیت پیمانکاری انجام می شود
                                                        // بدست آوردن سرعت انجام عملیات
                    OpSpeed = Conversions.ToDouble(GetOpExecutionSpeed(EnumExecutionMethod.EM_CONTRACTOR, mTreeCode, planItem.Op.Code));
                    // بدست آوردن طول زمان اجرای عملیات
                    LT = planItem.Qty / OpSpeed;
                    // بدست آوردن زمان شروع برنامه ریزی عملیات
                    //string argCurrentDate2 = planningRec.PlanningStartDate;
                    //double argPlanningStartHour =double.Parse(planningRec.PlanningStartHour);
                    //GetNewDate(calendarCode.ToString(), ref argCurrentDate2, ref argPlanningStartHour, LT);
                    //planningRec.PlanningStartDate = argCurrentDate2;
                    //planningRec.PlanningStartHour = argPlanningStartHour.ToString();
                    //SetupStartDate = 0L;
                    //SetupStartHour = 0d;
                    planningRec.OperationStartDate = planningRec.PlanningStartDate;
                    planningRec.OperationStartHour = planningRec.PlanningStartHour;
                    planningRec.OperationDuration = LT;
                    planningRec.PlanningDuration = LT;
                    //MachineCode = "-1";
                    break;

            }

            (planningRec.PlanningEndDate, planningRec.PlanningEndHour) = GetEndDateHour(calendarCode, planningRec.PlanningStartDate, planningRec.PlanningStartHour, planningRec.PlanningDuration);
            (planningRec.OperationEndDate, planningRec.OperationEndHour) = GetEndDateHour(calendarCode, planningRec.OperationStartDate, planningRec.OperationStartHour, planningRec.OperationDuration);
            //find setup timing only for machins
            if (planItem.Op.ExecutionMethod == EnumExecutionMethod.EM_MACHINE)
                (planningRec.SetupEndDate, planningRec.SetupEndHour) = GetEndDateHour(calendarCode, planningRec.SetupStartDate, planningRec.SetupStartHour, planningRec.SetupDuration);

            return planningRec;
        }

        /// <summary>
        /// این تابع اولین زمان در دسترس ماشین را محاسبه و باز می گرداند
        /// </summary>
        private void Forward_GetMachineFirstAccessibleTime(PlanOperation planItem, ref PlanningRec planRec, Machine machine)
        {
            FunctionLabel.Text = Conversions.ToString(Operators.ConcatenateObject(FunctionLabel.Tag, "GetMachineFirstAccessibleTime"));
            Application.DoEvents();
            if (this.LogPlanningProcedure) Logger.LogInfo($"Forward_GetMachineFirstAccessibleTime(Op: {planItem.Op.Code}, machine: {machine.MachineCode})");

            var oneMinute = Module1.ConvertToHour(EnumTimeType.TT_MINUTE, 1);
            string PlanningOldFilter = Db.DataSet.Tables["Tbl_Planning"].DefaultView.RowFilter;

            string StartDate = planRec.PlanningStartDate;
            string StartHour = planRec.PlanningStartHour;
            string EndDate;
            string EndHour;

            double LT = planRec.PlanningDuration;
            string MachineCode = machine.MachineCode;
            int CalendarCode = machine.CalendarCode;

            // زمان پایان برای درخواست ماشین را بدست می آوریم
            (EndDate, EndHour) = GetEndDateHour(CalendarCode, StartDate, StartHour, LT);

            StartHour = GetCompeleteHour(double.Parse(StartHour));
            EndHour = GetCompeleteHour(double.Parse(EndHour));

            // Check Machine is available:-
            string FilterValue = "MachineCode='" + MachineCode + "' And (StartDate + StartHour >='" + StartDate + StartHour + "' And StartDate + StartHour <='" + EndDate + EndHour + "')";
            Db.DataSet.Tables["Tbl_MachineNotAvailableTimes"].DefaultView.RowFilter = FilterValue;
            if (Db.DataSet.Tables["Tbl_MachineNotAvailableTimes"].DefaultView.Count == 0)
            {
                FilterValue = "MachineCode='" + MachineCode + "' And (EndDate + EndHour >='" + StartDate + StartHour + "' And EndDate + EndHour <='" + EndDate + EndHour + "')";
                Db.DataSet.Tables["Tbl_MachineNotAvailableTimes"].DefaultView.RowFilter = FilterValue;
                if (Db.DataSet.Tables["Tbl_MachineNotAvailableTimes"].DefaultView.Count > 0)
                {
                    Db.DataSet.Tables["Tbl_MachineNotAvailableTimes"].DefaultView.Sort = "EndDate Desc,EndHour Desc";
                    StartDate = Db.DataSet.Tables["Tbl_MachineNotAvailableTimes"].DefaultView[0]["EndDate"].ToString();
                    StartHour = Db.DataSet.Tables["Tbl_MachineNotAvailableTimes"].DefaultView[0]["EndHour"].ToString();
                    (EndDate, EndHour) = GetEndDateHour(CalendarCode, StartDate, StartHour, oneMinute);

                    // زمان پایان برای درخواست ماشین را بدست می آوریم
                    (EndDate, EndHour) = GetEndDateHour(CalendarCode, EndDate, EndHour, LT);
                }
            }
            else
            {
                Db.DataSet.Tables["Tbl_MachineNotAvailableTimes"].DefaultView.Sort = "EndDate Desc,EndHour Desc";
                StartDate = Db.DataSet.Tables["Tbl_MachineNotAvailableTimes"].DefaultView[0]["EndDate"].ToString();
                StartHour = Db.DataSet.Tables["Tbl_MachineNotAvailableTimes"].DefaultView[0]["EndHour"].ToString();
                (StartDate, StartHour) = GetEndDateHour(CalendarCode, StartDate, StartHour, oneMinute);

                // زمان پایان برای درخواست ماشین را بدست می آوریم
                string argCurrentDate4 = EndDate.ToString();
                double argCurrentHour4 = Conversions.ToDouble(EndHour);
                (EndDate, EndHour) = GetEndDateHour(CalendarCode, StartDate, StartHour, LT);
            }

            bool NotFoundTime = true;
            while (NotFoundTime)
            {
                StartHour = GetCompeleteHour(double.Parse(StartHour));
                EndHour = GetCompeleteHour(double.Parse(EndHour));
                FilterValue = "MachineCode='" + MachineCode + "' AND (PlanningStartDate + PlanningStartHour >='" + StartDate + StartHour + "' AND PlanningStartDate + PlanningStartHour <='" + EndDate + EndHour + "')";
                Db.DataSet.Tables["Tbl_Planning"].DefaultView.RowFilter = FilterValue;
                if (Db.DataSet.Tables["Tbl_Planning"].DefaultView.Count == 0)
                {
                    FilterValue = "MachineCode='" + MachineCode + "' AND (PlanningEndDate + PlanningEndHour >='" + StartDate + StartHour + "' AND PlanningEndDate + PlanningEndHour<='" + EndDate + EndHour + "')";
                    Db.DataSet.Tables["Tbl_Planning"].DefaultView.RowFilter = FilterValue;
                    if (Db.DataSet.Tables["Tbl_Planning"].DefaultView.Count == 0)
                    {
                        FilterValue = "MachineCode='" + MachineCode + "' AND (PlanningEndDate + PlanningEndHour >='" + StartDate + StartHour + "' AND PlanningStartDate + PlanningStartHour <='" + StartDate + StartHour + "')";
                        Db.DataSet.Tables["Tbl_Planning"].DefaultView.RowFilter = FilterValue;
                        if (Db.DataSet.Tables["Tbl_Planning"].DefaultView.Count == 0)
                        {
                            NotFoundTime = false;
                        }
                        else
                        {
                            Db.DataSet.Tables["Tbl_Planning"].DefaultView.Sort = "PlanningEndDate Desc";
                            StartDate = Db.DataSet.Tables["Tbl_Planning"].DefaultView[0]["PlanningEndDate"].ToString();
                            StartHour = Db.DataSet.Tables["Tbl_Planning"].DefaultView[0]["PlanningEndHour"].ToString();
                            (StartDate, StartHour) = GetEndDateHour(CalendarCode, StartDate, StartHour, oneMinute);
                            // زمان پایان برای درخواست ماشین را بدست می آوریم
                            (EndDate, EndHour) = GetEndDateHour(CalendarCode, StartDate, StartHour, LT);
                        }
                    }
                    else
                    {
                        Db.DataSet.Tables["Tbl_Planning"].DefaultView.Sort = "PlanningEndDate Desc";
                        StartDate = Db.DataSet.Tables["Tbl_Planning"].DefaultView[0]["PlanningEndDate"].ToString();
                        StartHour = Db.DataSet.Tables["Tbl_Planning"].DefaultView[0]["PlanningEndHour"].ToString();
                        (StartDate, StartHour) = GetEndDateHour(CalendarCode, StartDate, StartHour, oneMinute);

                        // زمان پایان برای درخواست ماشین را بدست می آوریم
                        (EndDate, EndHour) = GetEndDateHour(CalendarCode, StartDate, StartHour, LT);
                    }
                }
                else
                {
                    Db.DataSet.Tables["Tbl_Planning"].DefaultView.Sort = "PlanningStartDate Desc";
                    StartDate = Db.DataSet.Tables["Tbl_Planning"].DefaultView[0]["PlanningEndDate"].ToString();
                    StartHour = Db.DataSet.Tables["Tbl_Planning"].DefaultView[0]["PlanningEndHour"].ToString();
                    (StartDate, StartHour) = GetEndDateHour(CalendarCode, StartDate, StartHour, oneMinute);

                    (EndDate, EndHour) = GetEndDateHour(CalendarCode, StartDate, StartHour, LT);
                }
            }

            Db.DataSet.Tables["Tbl_Planning"].DefaultView.RowFilter = PlanningOldFilter;

            planRec.PlanningStartDate = StartDate;
            planRec.PlanningStartHour = StartHour;
            planRec.SetupStartDate = StartDate;
            planRec.SetupStartHour = StartHour;
            //get operation start date and hour:-
            (planRec.OperationStartDate, planRec.OperationStartHour) = GetEndDateHour(CalendarCode, StartDate, StartHour, machine.SetupDuration);

        }


        #endregion


    }
}