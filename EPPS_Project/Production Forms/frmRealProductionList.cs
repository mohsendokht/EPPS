using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using ProductionPlanning.Utils;

namespace ProductionPlanning
{
    public partial class frmRealProductionList
    {
        public frmRealProductionList()
        {
            InitializeComponent();
            _cbOperators.Name = "cbOperators";
            _chkNotConfirmed.Name = "chkNotConfirmed";
            _chkConfirmed.Name = "chkConfirmed";
            _cmdShow.Name = "cmdShow";
            _dgList.Name = "dgList";
            _CalDuringTimeButton.Name = "CalDuringTimeButton";
            _cmdConfirm.Name = "cmdConfirm";
            _cmdConfilicts.Name = "cmdConfilicts";
            _cmdHalts.Name = "cmdHalts";
            _cmdExit.Name = "cmdExit";
            _cmdDelete.Name = "cmdDelete";
            _cmdUpdate.Name = "cmdUpdate";
            _cmdInsert.Name = "cmdInsert";
        }

        public DataSetConfiguration MyDB = new DataSetConfiguration();

        // Private dvProductionList As DataView

        private int mFormMode;
        // Private I As Int16

        private const string TBL_RealProduction = "Tbl_RealProduction";
        private DataSet ds_RealProductionNew;

        public DataSet RealProduction_DataSet
        {
            get
            {
                return ds_RealProductionNew;
            }
        }

        public DataSet dsRealProduction
        {
            get
            {
                return MyDB.dsProductionPlanning; // ds_RealProduction
            }
        }

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

        private void frmRealProduction_Load(object sender, EventArgs e)
        {
            Top = 10;
            Left = 10;
            Module1.SetButtonsImage(cmdInsert, 0);
            Module1.SetButtonsImage(cmdDelete, 1);
            Module1.SetButtonsImage(cmdUpdate, 2);
            Module1.SetButtonsImage(cmdHalts, 15);
            dgList.Sorted += Module1.DataGridViews_Sorted_EventHandler;
            zzSetComboSourceData();

            // ''' Set User Access Right '''''''''''''
            cmdInsert.Enabled = Module_UserAccess.HaveAccessToItem(67);
            cmdUpdate.Enabled = Module_UserAccess.HaveAccessToItem(68);
            cmdDelete.Enabled = Module_UserAccess.HaveAccessToItem(69);
            cmdConfirm.Enabled = Module_UserAccess.HaveAccessToItem(70);
            cmdConfilicts.Enabled = Module_UserAccess.HaveAccessToItem(71);
            zzLoadData(" 1 = 0 ");
        }

        private void frmRealProductionList_FormClosing(object sender, FormClosingEventArgs e)
        {
            Module1.SaveGridColumnsWidth(Name, 0, dgList);
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cbOperators_SelectedChange()
        {
            if (cbOperators.SeletedValue is object)
            {
                lblName.Text = cbOperators.SeletedDisplay;
            }
            else
            {
                lblName.Text = Constants.vbNullString;
            }
        }

        private void cmdShow_Click(object sender, EventArgs e)
        {
            string SQLFilter;
            if (chkAll.Checked)
                ClearFilter();
            SQLFilter = Conversions.ToString(GetFilterValue());
            zzLoadData(SQLFilter);
        }

        private void cmdHalts_Click(object sender, EventArgs e)
        {
            if (dgList.Rows.Count == 0)
            {
                MessageBox.Show("رکوردی وجود ندارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                return;
            }

            if (dgList.CurrentRow is null)
            {
                MessageBox.Show("رکوردی انتخاب نشده است", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                return;
            }

            MyDB.FillDataSet("Tbl_ProductionHalts", "Tbl_ProductionHalts", "Select * From Tbl_ProductionHalts", "HaltID");
            MyDB.FillDataSet("Tbl_Calendar", "Tbl_Calendar", "Select * From Tbl_Calendar", "CalendarCode");
            MyDB.FillDataSet("Tbl_HoliDays", "Tbl_HoliDays", "Select * From Tbl_HoliDays", "DayNo", "MonthNo");
            MyDB.FillDataSet("Tbl_CalendarShifts", "Tbl_CalendarShifts", "Select * From Tbl_CalendarShifts", "CalendarCode", "ShiftNo");
            MyDB.FillDataSet("Tbl_CalendarDays", "Tbl_CalendarDays", "Select * From Tbl_CalendarDays", "CalendarCode", "ShiftNo", "DayNo");
            MyDB.FillDataSet("Tbl_CalendarParticularDays", "Tbl_CalendarParticularDays", "Select * From Tbl_CalendarParticularDays", "CalendarCode", "ShamsiDate");
            MyDB.FillDataSet("Tbl_CalendarParticularShifts", "Tbl_CalendarParticularShifts", "Select * From Tbl_CalendarParticularShifts", "CalendarCode", "ShamsiDate", "ShiftNo");
            MyDB.FillDataSet("Tbl_Machines", "Tbl_Machines", "Select * From Tbl_Machines", "Code");
            MyDB.FillDataSet("Tbl_ProductOPCs", "Tbl_ProductOPCs", "Select * From Tbl_ProductOPCs", "TreeCode", "OperationCode");
            var ProductionRow = dsRealProduction.Tables["Tbl_RealProduction"].Select(Conversions.ToString(Operators.ConcatenateObject("ProductionCode=", dgList.CurrentRow.Cells["ProductionCode"].Value)));
            {
                var withBlock = My.MyProject.Forms.frmProductionHaltList;
                withBlock.ProductionCurrentRow = ProductionRow[0];
                withBlock.dsProductionHalt = dsRealProduction;
                withBlock.ShowDialog();
                withBlock.Dispose();
                short I;
                for (I = 10; I >= 2; I += -1)
                {
                    dsRealProduction.Tables[I].Dispose();
                    dsRealProduction.Tables.RemoveAt(I);
                }
            }
        }

        private void IUD_ButtonClick(object sender, EventArgs e)
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

            if (Conversions.ToBoolean((bool)(mFormMode == (int)Module1.FormModeEnum.EDIT_MODE) && (bool)dgList.SelectedRows[0].Cells["RecordConfirm"].Value))
            {
                MessageBox.Show("رکورد تایید ثبت شده، مجاز به تغییر نمی باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                Application.DoEvents();
                return;
                //var cn1 = new SqlConnection(Module1.PlanningCnnStr);
                //bool CloseBatchFlag = false;
                //string mystr1 = "SELECT  FinishedDate FROM  Tbl_ProductionBatchs INNER JOIN " + " Tbl_ProductionSubbatchs ON Tbl_ProductionBatchs.BatchCode = Tbl_ProductionSubbatchs.BatchCode " + " WHERE  (Tbl_ProductionSubbatchs.SubbatchCode = '" + dgList.SelectedRows[0].Cells["SubbatchCode"].Value.ToString() + "') ";

                //var Mycmd = new SqlCommand(mystr1, cn1);
                //try
                //{
                //    cn1.Open();
                //    var dr1 = Mycmd.ExecuteReader();
                //    if (dr1.Read())
                //    {
                //        if (DBNull.Value.Equals(dr1["FinishedDate"]))
                //        {
                //            CloseBatchFlag = false;
                //        }
                //        else if (Operators.CompareString(dr1["FinishedDate"].ToString(), "0", false) > 0)
                //        {
                //            CloseBatchFlag = true;
                //        }
                //    }

                //    dr1.Close();
                //}
                //catch
                //{
                //}
                //finally
                //{
                //    if (cn1.State == ConnectionState.Open)
                //        cn1.Close();
                //}

                //if (CloseBatchFlag == true)
                //{
                //    MessageBox.Show("رکورد مربوط به ساب بچ بسته  شده می باشد و مجاز به تغییر نمی باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                //    Application.DoEvents();
                //    return;
                //}
            }

            if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
            {
                if (dgList.SelectedRows.Count > 1)
                {
                    if (MessageBox.Show("آیا برای حذف رکوردهای انتخاب شده مطمئن هستید؟", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading) == DialogResult.Yes)
                    {
                        Cursor = Cursors.WaitCursor;
                        int mDeletedCounter = 0;
                        string mProductionCodes = Constants.vbNullString;
                        using (var cn = new SqlConnection(Module1.PlanningCnnStr))
                        {
                            SqlTransaction trnDelete = null;
                            try
                            {
                                cn.Open();
                                foreach (DataGridViewRow dgRow in dgList.SelectedRows)
                                    mProductionCodes = Conversions.ToString(mProductionCodes + Interaction.IIf(string.IsNullOrEmpty(mProductionCodes), Operators.ConcatenateObject(Operators.ConcatenateObject("'", dgRow.Cells["ProductionCode"].Value), "'"), Operators.ConcatenateObject(Operators.ConcatenateObject(",'", dgRow.Cells["ProductionCode"].Value), "'")));
                                var mProductionRows = ds_RealProductionNew.Tables["Tbl_RealProduction"].Select("ProductionCode IN (" + mProductionCodes + ")");
                                for (int I = mProductionRows.Length - 1; I >= 0; I -= 1)
                                {
                                    if (Conversions.ToBoolean(!(bool)mProductionRows[I]["RecordConfirm"]))
                                    {
                                        string mCode = Conversions.ToString(mProductionRows[I]["ProductionCode"]);
                                        trnDelete = cn.BeginTransaction();
                                        var cmDelete = new SqlCommand("Delete From Tbl_ProductionHalts Where ProductionCode = " + mCode, cn);
                                        cmDelete.Transaction = trnDelete;
                                        cmDelete.ExecuteNonQuery();
                                        cmDelete.CommandText = "Delete From Tbl_RealProduction Where ProductionCode = " + mCode;
                                        cmDelete.ExecuteNonQuery();
                                        mProductionRows[I].Delete();
                                        trnDelete.Commit();
                                        trnDelete = null;
                                        ds_RealProductionNew.AcceptChanges();
                                        mDeletedCounter += 1;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.LogException("", ex);
                                Cursor = Cursors.Default;
                                ds_RealProductionNew.RejectChanges();
                                if (trnDelete is object)
                                {
                                    trnDelete.Rollback();
                                }

                                MessageBox.Show("حذف رکوردهای تولید با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                            }
                            finally
                            {
                                if (cn.State == ConnectionState.Open)
                                    cn.Close();
                            }
                        }

                        Cursor = Cursors.Default;
                        MessageBox.Show("تعداد: " + mDeletedCounter.ToString() + " رکورد از تعداد: " + mProductionCodes.Split(',').Length.ToString() + " رکورد انتخاب شده حذف گردید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    }

                    return;
                }
                else if (Conversions.ToBoolean(dgList.SelectedRows[0].Cells["RecordConfirm"].Value))
                {
                    MessageBox.Show("رکورد تایید ثبت شده، مجاز به حذف نمی باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return;
                }
            }

            string Query;

            // If mFormMode = FormModeEnum.INSERT_MODE Then

            Query = "Select Distinct A.BatchCode " + "From   dbo.Tbl_ProductionBatchs A INNER JOIN dbo.Tbl_ProductionSubbatchs B ON A.BatchCode = B.BatchCode " + "Where  B.SubbatchCode IN (Select SubbatchCode From Tbl_Planning) And (A.FinishedDate Is Null Or A.FinishedDate = '' Or A.FinishedDate = '0')";
            MyDB.FillDataSet("Tbl_ProductionBatchs", "Tbl_ProductionBatchs", Query, "BatchCode");
            Query = "Select A.SubbatchCode, B.ProductTreeCode As TreeCode, D.ProductName,D.ProductCode,B.FinishedDate,B.BatchCode " + "From   dbo.Tbl_ProductionSubbatchs A INNER JOIN " + "       dbo.Tbl_ProductionBatchs B ON A.BatchCode = B.BatchCode INNER JOIN " + "       dbo.Tbl_ProductTree C ON B.ProductTreeCode = C.TreeCode INNER JOIN " + "       dbo.Tbl_Products D ON C.ProductCode = D.ProductCode " + "Where  A.SubbatchCode IN (Select SubbatchCode From Tbl_Planning) And (B.FinishedDate Is Null Or B.FinishedDate = '' Or B.FinishedDate = '0')";
            // Else
            // Query = "Select Distinct A.BatchCode " & _
            // "From   dbo.Tbl_ProductionBatchs A INNER JOIN dbo.Tbl_ProductionSubbatchs B ON A.BatchCode = B.BatchCode " & _
            // "Where  B.SubbatchCode IN (Select SubbatchCode From Tbl_Planning)"
            // DataSetConfig.FillDataSet("Tbl_ProductionBatchs", "Tbl_ProductionBatchs", Query, "BatchCode")

            // Query = "Select A.SubbatchCode, B.ProductTreeCode As TreeCode, D.ProductName,D.ProductCode,B.FinishedDate,B.BatchCode " & _
            // "From   dbo.Tbl_ProductionSubbatchs A INNER JOIN " & _
            // "       dbo.Tbl_ProductionBatchs B ON A.BatchCode = B.BatchCode INNER JOIN " & _
            // "       dbo.Tbl_ProductTree C ON B.ProductTreeCode = C.TreeCode INNER JOIN " & _
            // "       dbo.Tbl_Products D ON C.ProductCode = D.ProductCode " & _
            // "Where  A.SubbatchCode IN (Select SubbatchCode From Tbl_Planning)"
            // End If

            MyDB.FillDataSet("Tbl_ProductionSubbatchs", "Tbl_ProductionSubbatchs", Query, "SubbatchCode");
            // فراخوانی اطلاعات جدول اپراتورها
            MyDB.FillDataSet("Tbl_Operators", "ProductionOperators", "Select A.OperatorCode,A.OperatorName,A.WorkingStatus,IsNull((Select EndDate From Tbl_OperatorWorkPeriods Where OperatorCode = A.OperatorCode And StartDate IN (Select Max(StartDate) From Tbl_OperatorWorkPeriods Where OperatorCode = A.OperatorCode)),'0') As WorkingEndDate From Tbl_Operators A Order By A.OperatorName", "OperatorCode");

            // فراخوانی اطلاعات جدول توقفات عملیات
            MyDB.FillDataSet("Tbl_ProductionHalts", "Tbl_ProductionHalts", "Select * From Tbl_ProductionHalts", "HaltID");

            // فراخوانی اطلاعات جدول علت توقفات
            MyDB.FillDataSet("Tbl_HaltReasons", "Tbl_HaltReasons", "Select * From Tbl_HaltReasons", "ReasonCode");

            // فرخوانی جدول ضرایب تبدیل
            Query = "Select A.OperationCode,A.UnitCode,B.Title AS UnitTitle,A.UnitIndex " + "From   dbo.Tbl_Operation_Measurement_UnitIndex A INNER JOIN " + "       dbo.Tbl_TestUnits B ON A.UnitCode = B.Code";
            MyDB.FillDataSet("Tbl_Operation_Measurement_UnitIndex", "Tbl_MeasurementUnit1", Query, "OperationCode", "UnitCode");
            MyDB.FillDataSet("Tbl_Operation_Measurement_UnitIndex", "Tbl_MeasurementUnit2", Query, "OperationCode", "UnitCode");
            {
                var withBlock = My.MyProject.Forms.frmRealProduction;
                withBlock.RealProduction_DataSet = ds_RealProductionNew;
                if (mFormMode != (int)Module1.FormModeEnum.INSERT_MODE)
                    withBlock.Current_DataRow = GetcurrentDataRow();
                withBlock.ListForm = this;
                withBlock.ListForm.FormMode = mFormMode;
                if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
                {
                    withBlock.Controls["Panel1"].Controls["cmdSave"].Visible = false;
                    withBlock.Controls["Panel1"].Controls["cmdDelete"].Visible = true;
                }

                // Dim CurrentRowIndex As Integer
                // Dim dvProductionList As DataView
                // If mFormMode <> FormModeEnum.INSERT_MODE Then
                // dvProductionList = ds_RealProduction.Tables("Tbl_RealProduction").DefaultView
                // CurrentRowIndex = Me.BindingContext(dvProductionList).Position
                // End If

                // Dim drVal As DialogResult = .ShowDialog()
                withBlock.ShowDialog();
                withBlock.Dispose();
            }
        }

        private void cmdConfilicts_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                int I;
                var cmValidation = new SqlCommand("", Module1.cnProductionPlanning);
                string SaveConfilicts = Constants.vbNullString;
                long TempQty;
                long ProductionQty;
                var daConfilicts = new SqlDataAdapter("Select Distinct B.OperationCode,B.OperationTitle,A.SubbatchCode,A.TreeCode " + "From   dbo.Tbl_RealProduction A INNER JOIN " + "       dbo.Tbl_ProductOPCs B ON A.TreeCode = B.TreeCode AND " + "       A.OperationCode = B.OperationCode " + "Where  A.RecordConfirm = 0", Module1.cnProductionPlanning);
                var dtConfilicts = new DataTable();
                var dtConfilictList = new DataTable();
                dtConfilictList.Columns.Add("OperationCode");
                dtConfilictList.Columns.Add("OperationTitle");
                dtConfilictList.Columns.Add("SubbatchCode");
                dtConfilictList.Columns.Add("ConfilictDescription");
                daConfilicts.Fill(dtConfilicts);
                var dvValidation = dtConfilicts.DefaultView;
                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();
                {
                    var withBlock = new frmProductionConfilicts();
                    var loopTo = dvValidation.Count - 1;
                    for (I = 0; I <= loopTo; I++)
                    {
                        SaveConfilicts = Constants.vbNullString;
                        cmValidation.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select Count(*) From Tbl_PreOperations Where TreeCode=", dvValidation[I]["TreeCode"]), " And CurrentOperationCode='"), dvValidation[I]["OperationCode"]), "'"));
                        if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(cmValidation.ExecuteScalar(), 0, false)))
                        {
                            // کنترل اینکه عملیات های پیشنیاز عملیات فعلی وارد تولید شده باشند
                            cmValidation.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select Count(*) From Tbl_RealProduction Where SubbatchCode='", dvValidation[I]["SubbatchCode"]), "' And OperationCode IN(Select PreOperationCode From Tbl_PreOperations Where TreeCode="), dvValidation[I]["TreeCode"]), " And CurrentOperationCode='"), dvValidation[I]["OperationCode"]), "')"));
                            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(cmValidation.ExecuteScalar(), 0, false)))
                            {
                                SaveConfilicts += "-عملیات(های) پیشنیاز وارد تولید نشده اند";
                            }
                            else // كنترل مازاد توليد نسبت به تعداد عمليات قبلي
                            {
                                cmValidation.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select Max(Quantity) From (Select OperationCode,IsNull(Sum(IntactQuantity),0) As Quantity From Tbl_RealProduction " + "Where  SubbatchCode='", dvValidation[I]["SubbatchCode"]), "' And TreeCode="), dvValidation[I]["TreeCode"]), " And OperationCode IN "), "       (Select PreOperationCode From Tbl_PreOperations "), "        Where TreeCode="), dvValidation[I]["TreeCode"]), " And CurrentOperationCode='"), dvValidation[I]["OperationCode"]), "') "), "Group By OperationCode) A"));
                                TempQty = Conversions.ToLong(cmValidation.ExecuteScalar());
                                cmValidation.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select IsNull(Sum(IntactQuantity),0) As Quantity From Tbl_RealProduction " + "Where SubbatchCode='", dvValidation[I]["SubbatchCode"]), "' And TreeCode="), dvValidation[I]["TreeCode"]), " And OperationCode='"), dvValidation[I]["OperationCode"]), "'"));
                                TempQty = Conversions.ToLong(Operators.SubtractObject(cmValidation.ExecuteScalar(), TempQty));
                                if (TempQty > 0L)
                                {
                                    if (string.IsNullOrEmpty(SaveConfilicts))
                                    {
                                        SaveConfilicts += " -مازاد توليد نسبت به عمليات پيشنياز: " + TempQty;
                                    }
                                    else
                                    {
                                        SaveConfilicts += Constants.vbCrLf + " -مازاد توليد نسبت به عمليات پيشنياز: " + TempQty;
                                    }
                                }
                            }
                        }

                        // كنترل مازاد توليد نسبت به تعداد ساب بچ
                        cmValidation.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Select Sum(ProductionQuantityinSubbatch) From Tbl_ProductionSubbatchs " + "Where SubbatchCode='", dvValidation[I]["SubbatchCode"]), "'"));
                        TempQty = Conversions.ToLong(cmValidation.ExecuteScalar());
                        cmValidation.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select IsNull(Sum(IntactQuantity),0) As Quantity From Tbl_RealProduction " + "Where SubbatchCode='", dvValidation[I]["SubbatchCode"]), "' And TreeCode="), dvValidation[I]["TreeCode"]), " And OperationCode='"), dvValidation[I]["OperationCode"]), "'"));
                        TempQty = Conversions.ToLong(Operators.SubtractObject(cmValidation.ExecuteScalar(), TempQty));
                        if (TempQty > 0L)
                        {
                            if (string.IsNullOrEmpty(SaveConfilicts))
                            {
                                SaveConfilicts += " -مازاد توليد نسبت به تعداد ساب بچ: " + TempQty;
                            }
                            else
                            {
                                SaveConfilicts += Constants.vbCrLf + " -مازاد توليد نسبت به تعداد ساب بچ: " + TempQty;
                            }
                        }

                        // کنترل اینکه تعداد تولید شده بیشتر از تعداد برنامه ریزی شده نباشد
                        cmValidation.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select IsNull(Sum(DetailProductionQuantity),0) From Tbl_Planning Where SubbatchCode='", dvValidation[I]["SubbatchCode"]), "' And OperationCode='"), dvValidation[I]["OperationCode"]), "'"));
                        TempQty = Conversions.ToLong(cmValidation.ExecuteScalar());
                        cmValidation.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select IsNull(Sum(IntactQuantity),0) From Tbl_RealProduction Where SubbatchCode='", dvValidation[I]["SubbatchCode"]), "' And OperationCode='"), dvValidation[I]["OperationCode"]), "'"));
                        ProductionQty = Conversions.ToLong(cmValidation.ExecuteScalar());
                        if (ProductionQty > TempQty)
                        {
                            if (string.IsNullOrEmpty(SaveConfilicts))
                            {
                                SaveConfilicts += "-تعداد تولید شده بیش از تعداد برنامه ریزی شده می باشد";
                            }
                            else
                            {
                                SaveConfilicts += Constants.vbCrLf + "-تعداد تولید شده بیش از تعداد برنامه ریزی شده می باشد";
                            }
                        }

                        if (!string.IsNullOrEmpty(SaveConfilicts) && dtConfilictList.Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("SubbatchCode = '", dvValidation[I]["SubbatchCode"]), "' And OperationCode = '"), dvValidation[I]["OperationCode"]), "'"))).Length == 0)
                        {
                            withBlock.dgList.Rows.Add(dvValidation[I]["OperationCode"], dvValidation[I]["OperationTitle"], dvValidation[I]["SubbatchCode"], SaveConfilicts);
                            dtConfilictList.Rows.Add(dvValidation[I]["OperationCode"], dvValidation[I]["OperationTitle"], dvValidation[I]["SubbatchCode"], SaveConfilicts);
                        }
                    }

                    Cursor = Cursors.Default;
                    if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                        Module1.cnProductionPlanning.Close();
                    if (dtConfilictList.Rows.Count > 0)
                    {
                        withBlock.dtConfilicts = dtConfilictList;
                        withBlock.MdiParent = MdiParent;
                        withBlock.Show();
                    }
                    else
                    {
                        MessageBox.Show("مغایرتی یافت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                    }
                }
            }
            catch (Exception objEx)
            {
                Cursor = Cursors.Default;
                Logger.SaveError(Name + ".cmdConfilicts_Click", objEx.Message);
                MessageBox.Show("نمایش لیست مغایرتها با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }

        private void cmdConfirm_Click(object sender, EventArgs e)
        {
            var daConfirm = new SqlDataAdapter();
            int I;
            try
            {
                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();
                if (dgList.SelectedRows.Count > 0)
                {
                    for (I = dgList.SelectedRows.Count - 1; I >= 0; I -= 1)
                    {
                        if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgList.SelectedRows[I].Cells["RecordConfirm"].Value, 0, false)))
                        {
                            dgList.SelectedRows[I].Cells["RecordConfirm"].Value = 1;
                        }
                        else
                        {
                            dgList.SelectedRows[I].Cells["RecordConfirm"].Value = 0;
                        }
                    }

                    daConfirm.UpdateCommand = new SqlCommand("Update Tbl_RealProduction Set RecordConfirm=@RecordConfirm Where ProductionCode=@ProductionCode", Module1.cnProductionPlanning);
                    {
                        var withBlock = daConfirm.UpdateCommand.Parameters;
                        withBlock.Add("@RecordConfirm", SqlDbType.Bit, default, "RecordConfirm");
                        withBlock.Add("@ProductionCode", SqlDbType.Int, default, "ProductionCode").SourceVersion = DataRowVersion.Original;
                    }

                    daConfirm.Update(ds_RealProductionNew, "Tbl_RealProduction");
                    ds_RealProductionNew.AcceptChanges();
                }
            }
            catch (Exception objEx)
            {
                dsRealProduction.RejectChanges();
                Logger.SaveError(Name + ".cmdConfirm_Click", objEx.Message);
                MessageBox.Show("تایید رکورد(های) تولید با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
            finally
            {
                daConfirm.Dispose();
                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                    Module1.cnProductionPlanning.Close();
            }
        }

        private void dgList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string S;
            if (dgList.Columns[e.ColumnIndex].Name.Equals("StartDT") || dgList.Columns[e.ColumnIndex].Name.Equals("EndDT"))
            {
                // For Example e.value = "01:30 13890718"
                S = e.Value.ToString().PadLeft(14);
                e.Value = S.Substring(0, 10) + "/" + S.Substring(10, 2) + "/" + S.Substring(12, 2);
            }

            // If e.RowIndex > -1 Then
            // dgList.Rows(e.RowIndex).HeaderCell.Value = (e.RowIndex + 1).ToString
            // End If
        }

        private void ClearFilter()
        {
            lblName.Text = "";
            cbOperators.SeletedValue = null;
            cbOperators.Text = "";
            txtFromDate.Text = "";
            txtToDate.Text = "";
            cbOperation.SelectedIndex = -1;
            chkConfirmed.Checked = false;
            chkNotConfirmed.Checked = false;
            cbProduct.SelectedIndex = -1;
            cbSubbatch.SelectedIndex = -1;
            chkAll.Checked = false;
        }

        private object GetFilterValue()
        {
            string FilterValue = "";

            // If txtPersonnelCode.Text <> vbNullString Then
            // FilterValue = "OperatorCode='" & txtPersonnelCode.Text & "'"
            // End If

            if (cbOperators.SeletedValue is object)
            {
                FilterValue = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("A.ProductionCode IN (Select ProductionCode From Tbl_ProductionOperators Where OperatorCode='", cbOperators.SeletedValue), "')"));
            }

            if (!string.IsNullOrEmpty(Strings.Trim(Strings.Replace(txtFromDate.Text, "/", ""))))
            {
                FilterValue = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(FilterValue, Interaction.IIf(string.IsNullOrEmpty(FilterValue), "", " AND ")), " A.StartDate>= "), Strings.Trim(Strings.Replace(txtFromDate.Text, "/", ""))));
            }

            if (!string.IsNullOrEmpty(Strings.Trim(Strings.Replace(txtToDate.Text, "/", ""))))
            {
                FilterValue = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(FilterValue, Interaction.IIf(string.IsNullOrEmpty(FilterValue), "", " AND ")), " A.StartDate<="), Strings.Trim(Strings.Replace(txtToDate.Text, "/", ""))));
            }

            if (cbOperation.SelectedValue is object && !cbOperation.Text.Trim().Equals(""))
            {
                FilterValue = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(FilterValue, Interaction.IIf(string.IsNullOrEmpty(FilterValue), "", " AND ")), " A.OperationCode = '"), cbOperation.SelectedValue), "'"));
            }

            if (chkConfirmed.Checked)
            {
                FilterValue = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(FilterValue, Interaction.IIf(string.IsNullOrEmpty(FilterValue), "", " AND ")), " A.RecordConfirm = 1 "));
            }
            else if (chkNotConfirmed.Checked)
            {
                FilterValue = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(FilterValue, Interaction.IIf(string.IsNullOrEmpty(FilterValue), "", " AND ")), " A.RecordConfirm = 0 "));
            }

            if (cbProduct.SelectedValue is object && !cbProduct.Text.Trim().Equals(""))
            {
                FilterValue = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(FilterValue, Interaction.IIf(string.IsNullOrEmpty(FilterValue), "", " AND ")), " F.ProductCode = '"), cbProduct.SelectedValue), "'"));
            }

            if (cbSubbatch.SelectedValue is object && !cbSubbatch.Text.Trim().Equals(""))
            {
                FilterValue = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(FilterValue, Interaction.IIf(string.IsNullOrEmpty(FilterValue), "", " AND ")), " A.SubbatchCode = '"), cbSubbatch.SelectedValue), "'"));
            }

            return FilterValue;
        }

        public DataRow GetcurrentDataRow()
        {
            string strFind = Conversions.ToString(Operators.ConcatenateObject("ProductionCode=", dgList.CurrentRow.Cells[0].Value));
            object crRow;
            var drFind = new DataRow[1];
            drFind = ds_RealProductionNew.Tables["Tbl_RealProduction"].Select(strFind);
            crRow = drFind[0];
            return (DataRow)crRow;
        }

        private void dgList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F12)
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

        private void txtPersonnelCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Strings.Asc(e.KeyChar) == (int)Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
            else if (Strings.Asc(e.KeyChar) != (int)Keys.Back)
            {
                string ValidStr = "0123456789";
                if (Strings.InStr(ValidStr, Conversions.ToString(e.KeyChar)) == 0)
                {
                    e.KeyChar = Conversions.ToChar("");
                }
            }
        }

        private void cbOperators_InputKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lblName.Text = Constants.vbNullString;
                cbOperators.SeletedValue = null;
                using (var cn = new SqlConnection(Module1.PlanningCnnStr))
                {
                    try
                    {
                        cn.Open();
                        {
                            var withBlock = new SqlCommand("Select OperatorCode,OperatorName From Tbl_Operators Where OperatorCode = '" + cbOperators.Text + "'", cn);
                            var dr = withBlock.ExecuteReader();
                            if (dr.Read())
                            {
                                cbOperators.SeletedValue = dr["OperatorCode"].ToString();
                                lblName.Text = dr["OperatorName"].ToString();
                            }
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
            }
        }

        private void CalculateAgine_ProductionDuringTime_For_RealProductionRecord()
        { 
            WorkingTimeCalculater WTCalculater = new WorkingTimeCalculater();

            SqlCommand MyUpdateCommand1;
            string MyMachineCode = "-1";
            string MyStartDate = "";
            //string MyEndDate = "";
            string MyStartHour = "";
            string MyEndHour = "";
            string MyHaltTime = "";
            long MyRecordNo = 0L;
            string MyTreecode = "0";
            string MyOperationCode = "-1";
            string ProductionDuration;
            var Mycnn1 = new SqlConnection(Module1.PlanningCnnStr);
            int I;
            try
            {
                MyUpdateCommand1 = new SqlCommand("Update Tbl_RealProduction Set ProductionDuration= @ProductionDuration Where ProductionCode=@ProductionCode", Mycnn1);
                {
                    var withBlock = MyUpdateCommand1.Parameters;
                    withBlock.Add("@ProductionDuration", SqlDbType.VarChar, 50, "ProductionDuration");
                    withBlock.Add("@ProductionCode", SqlDbType.Int, default, "ProductionCode").SourceVersion = DataRowVersion.Original;
                }

                if (Mycnn1.State == ConnectionState.Closed)
                    Mycnn1.Open();
                if (dgList.RowCount > 0)
                {
                    var loopTo = dgList.RowCount - 1;
                    for (I = 0; I <= loopTo; I += 1)
                    {
                        bool.TryParse(dgList[17, I].Value.ToString(), out var Confirmed);
                        if (!Confirmed)
                        {
                            MyMachineCode = dgList[4, I].Value.ToString();
                            MyStartDate = dgList[18, I].Value.ToString();
                            MyStartHour = dgList[19, I].Value.ToString();
                            MyStartDate = dgList[20, I].Value.ToString();
                            MyEndHour = dgList[21, I].Value.ToString();
                            MyHaltTime = dgList[8, I].Value.ToString();
                            MyRecordNo = Conversions.ToLong(dgList[0, I].Value.ToString());
                            MyTreecode = dgList[14, I].Value.ToString();
                            MyOperationCode = dgList[1, I].Value.ToString();
                            var CalendarCode = RealProductionModule.FindCalendarCode(MyMachineCode, MyTreecode, MyOperationCode);
                            //ProductionDuration = RealProductionModule.Calculate_ProductionDuration(CalendarCode, MyStartDate, MyStartHour, MyEndHour);
                            ProductionDuration = WTCalculater.CalculateHMDuration(CalendarCode, MyStartDate, MyStartHour, MyStartDate, MyEndHour, MyHaltTime);

                            //if (ProductionDuration != 0d & MyHaltTime != "00:00")
                            //    ProductionDuration = ProductionDuration - Conversions.ToDouble(Module1.GetFloatingHour(MyHaltTime));
                            MyUpdateCommand1.Parameters["@ProductionDuration"].Value = ProductionDuration;
                            MyUpdateCommand1.Parameters["@ProductionCode"].Value = MyRecordNo;
                            MyUpdateCommand1.ExecuteNonQuery();
                        }
                    }
                }

                Cursor = Cursors.Default;
                MessageBox.Show("بروزآوری رکورد(های) تولید انجام شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
            catch (Exception objEx)
            {
                Cursor = Cursors.Default;
                Logger.SaveError(Name + ".CalculateAgine_ProductionDuringTime_For_RealProductionRecord", objEx.Message);
                MessageBox.Show("بروزآوری رکورد(های) تولید با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
            finally
            {
                if (Mycnn1.State == ConnectionState.Open)
                    Mycnn1.Close();
            }
        }

        private void CalDuringTimeButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            CalculateAgine_ProductionDuringTime_For_RealProductionRecord();
            cmdShow_Click(sender, e);
            Cursor = Cursors.Default;
        }

        /* TODO ERROR: Skipped RegionDirectiveTrivia */
        private void zzLoadData(string SQLFilter)
        {
            const string SelectSQL_RealProduction = 
                "Select TOP 10000 " + 
                "       A.ProductionCode, " +
                "       B.OperationCode, " + 
                "       B.OperationTitle, " + 
                "       A.SubbatchCode, " + 
                "       Machine      = CASE WHEN B.ExecutionMethod = 1 THEN A.MachineCode " + 
                "                           WHEN B.ExecutionMethod = 2 THEN " + "'عملیات اپراتوری'" + 
                "                           WHEN B.ExecutionMethod = 3 THEN " + "'عملیات پیمانکاری'" +
                "                           ELSE '' " + "                      END, " +
                "       StartDT      = LTRIM(RTRIM(A.StartHour))  + ' ' + LTRIM(RTRIM(A.StartDate)), " + 
                "       EndDT        = LTRIM(RTRIM(A.EndHour))    + ' ' + LTRIM(RTRIM(A.EndDate)),  " +
                "       RPD          = dbo.GetRegularTimeFromFloatingTime(A.ProductionDuration), " +
                "       HaltDuration = ISNull(D.Duration,'00:00'), " + 
                "       HaltReason   = ISNULL(E.ReasonTitle,'-'), " + 
                "       A.IntactQuantity, " +
                "       A.GarbageQuantity, " +
                "       A.RecordConfirm, " +
                "       A.PlanningCode, " +
                "       A.TreeCode ,  " + 
                "       F.ProductCode, " +
                "       A.OperationType,  " +
                "       A.ProductionDuration, " +
                "       A.StartDate,  " +
                "       A.StartHour,  " + 
                "       A.EndDate,  " + 
                "       A.EndHour, " +
                "       Tbl_ProductionSubbatchs.BatchCode,  " +
                "       A.MachineCode " +
                "From  dbo.Tbl_RealProduction AS A " +
                "      INNER JOIN dbo.Tbl_ProductOPCs AS B ON A.TreeCode = B.TreeCode AND A.OperationCode = B.OperationCode " +
                "      LEFT  JOIN dbo.Tbl_ProductionHalts AS D ON A.ProductionCode = D.ProductionCode " +
                "      LEFT  JOIN dbo.Tbl_HaltReasons AS E ON D.HaltReason = E.ReasonCode " +
                "      INNER JOIN dbo.Tbl_ProductTree AS F ON F.TreeCode = A.TreeCode " +
                "      INNER JOIN Tbl_ProductionSubbatchs ON Tbl_ProductionSubbatchs.SubbatchCode = A.SubbatchCode ";
            const string OrderBySQL = "ORDER BY A.StartDate, A.StartHour, B.OperationCode ";
            string SQL;
            string Filter = "";
            try
            {
                if (!string.IsNullOrEmpty(SQLFilter))
                    Filter = " WHERE " + SQLFilter;
                SQL = SelectSQL_RealProduction + Filter + OrderBySQL;
                ds_RealProductionNew = DbTool.GetDataSet("Tbl_RealProduction", SQL);
                dgList.DataSource = ds_RealProductionNew.Tables["Tbl_RealProduction"].DefaultView;
                zzSetGridColumns();
                ds_RealProductionNew.Tables["Tbl_RealProduction"].Columns["ProductionCode"].ReadOnly = false;
                ds_RealProductionNew.Tables["Tbl_RealProduction"].Columns["HaltDuration"].ReadOnly = false;
                ds_RealProductionNew.Tables["Tbl_RealProduction"].Columns["HaltReason"].ReadOnly = false;
                ds_RealProductionNew.Tables["Tbl_RealProduction"].Columns["ProductCode"].ReadOnly = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا در فراخوانی اطلاعات");
                Logger.SaveError("FrmRealProductionList:zzLoadDataGrid()", ex.Message);
            }
        }

        private void zzSetGridColumns()
        {
            // Define Grid columns:-
            // "0      ProductionCode 
            // "1      OperationCode  
            // "2      OperationTitle 
            // "3      SubbatchCode   
            // "4      MachineCode    
            // "5      StartDT       
            // "6      EndDT          
            // "7      RPD            
            // "8      HaltDuration   
            // "9      HaltReason     
            // "10     IntactQuantity 
            // "11     GarbageQuantity
            // "12     RecordConfirm  


            {
                var withBlock = dgList;
                // .DataSource = dataSet.Tables("Tbl_RealProduction").DefaultView
                withBlock.Columns[0].Visible = false;    // ProductionCode
                withBlock.Columns[1].HeaderText = "کد فعالیت";
                withBlock.Columns[2].HeaderText = "عنوان فعالیت";
                withBlock.Columns[3].HeaderText = " ساب بچ";
                withBlock.Columns[4].HeaderText = " ماشین ";
                withBlock.Columns[5].HeaderText = "شروع";
                withBlock.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                withBlock.Columns[6].HeaderText = "پایان";
                withBlock.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                withBlock.Columns[7].HeaderText = "طول تولید";
                withBlock.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                withBlock.Columns[8].HeaderText = "طول توقف";
                withBlock.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                withBlock.Columns[9].HeaderText = "علت توقف";
                withBlock.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                withBlock.Columns[10].HeaderText = "تولید سالم";
                withBlock.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                withBlock.Columns[11].HeaderText = "تولید معیوب";
                withBlock.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                withBlock.Columns[12].HeaderText = "تا یید";
                withBlock.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // Hidden columns:-
                withBlock.Columns[13].Visible = false;   // PlanningCode
                withBlock.Columns[14].Visible = false;   // TreeCode
                withBlock.Columns[15].Visible = false;   // ProductCode
                withBlock.Columns[16].Visible = false;   // OperationType
                withBlock.Columns[17].Visible = false;   // ProductionDuration   , A., F., A., A.
                withBlock.Columns[18].Visible = false;   // StartDate
                withBlock.Columns[19].Visible = false;   // StartHour
                withBlock.Columns[20].Visible = false;   // EndDate
                withBlock.Columns[21].Visible = false;   // EndHour
                withBlock.Columns[22].Visible = false;   // Machine Code
                withBlock.Columns[23].Visible = false;
            }

            Module1.SetGridColumnsWidth(Name, 0, dgList);
        }
        /* TODO ERROR: Skipped EndRegionDirectiveTrivia */
        /* TODO ERROR: Skipped RegionDirectiveTrivia */
        private void zzSetComboSourceData()
        {
            var daCombo = new SqlDataAdapter("", Module1.cnProductionPlanning);
            var dtCombo = new DataTable();
            string Query;

            // daCombo.SelectCommand.CommandText = "Select OperatorCode,OperatorName,WorkingStatus From Tbl_Operators Where WorkingStatus = 1 Order By OperatorName"
            daCombo.SelectCommand.CommandText = "Select OperatorCode,OperatorName From Tbl_Operators Order By OperatorName";
            var ds1 = new DataSet();
            DataView dv1;
            daCombo.Fill(ds1, "Tbl_Operators");
            dv1 = new DataView(ds1.Tables["Tbl_Operators"]);
            {
                var withBlock = cbOperators;
                withBlock.DataSource = dv1; // dtCombo
                withBlock.ValueMember = "OperatorCode";
                withBlock.DisplayMember = "OperatorName";
                withBlock.ValueColumnWidth = 100;
                withBlock.MessagesHeader = Module1.MessagesTitle;
            }

            Query = "Select   Distinct A.TreeCode,A.OperationCode, A.OperationCode + ' ' + A.OperationTitle As OperationTitle " + "From     dbo.Tbl_ProductOPCs A INNER JOIN dbo.Tbl_Planning B ON A.TreeCode = B.TreeCode AND A.OperationCode = B.OperationCode " + "Order By A.OperationCode";
            daCombo.SelectCommand.CommandText = Query;
            dtCombo = new DataTable();
            daCombo.Fill(dtCombo);
            {
                var withBlock1 = cbOperation;
                withBlock1.DataSource = dtCombo;
                withBlock1.DisplayMember = "OperationTitle";
                withBlock1.ValueMember = "OperationCode";
                withBlock1.SelectedIndex = -1;
            }

            Query = "Select A.SubbatchCode, D.ProductCode, B.BatchCode " + "From   dbo.Tbl_ProductionSubbatchs A INNER JOIN dbo.Tbl_ProductionBatchs B ON A.BatchCode = B.BatchCode INNER JOIN " + "       dbo.Tbl_ProductTree C ON C.TreeCode = B.ProductTreeCode Inner Join " + "       dbo.Tbl_Products D ON D.ProductCode = C.ProductCode " + "Where  A.SubbatchCode IN (Select SubbatchCode From Tbl_Planning)";
            daCombo.SelectCommand.CommandText = Query;
            dtCombo = new DataTable();
            daCombo.Fill(dtCombo);
            {
                var withBlock2 = cbSubbatch;
                withBlock2.DataSource = dtCombo;
                withBlock2.DisplayMember = "SubbatchCode";
                withBlock2.ValueMember = "SubbatchCode";
                withBlock2.SelectedIndex = -1;
            }

            Query = "Select ProductCode, ProductCode + ' ' + ProductName As ProductName From dbo.Tbl_Products";
            daCombo.SelectCommand.CommandText = Query;
            dtCombo = new DataTable();
            daCombo.Fill(dtCombo);
            {
                var withBlock3 = cbProduct;
                withBlock3.DataSource = dtCombo;
                withBlock3.DisplayMember = "ProductName";
                withBlock3.ValueMember = "ProductCode";
                withBlock3.SelectedIndex = -1;
            }
        }

        /* TODO ERROR: Skipped EndRegionDirectiveTrivia */
    }
}