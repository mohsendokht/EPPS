using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmDoneBatch
    {
        public frmDoneBatch()
        {
            InitializeComponent();
            _cmdSave.Name = "cmdSave";
            _cmdExit.Name = "cmdExit";
            _dgDetails.Name = "dgDetails";
            _cmdCalcGarbageQuantity.Name = "cmdCalcGarbageQuantity";
        }

        private DataSet mdsDoneBatch;
        private SqlDataAdapter daBatch = new SqlDataAdapter();
        private SqlDataAdapter daProductionDetail = new SqlDataAdapter();
        private DataView dvDetails;
        private DataRow mCurrentRow;
        private string NavigatedOperations = Constants.vbNullString;
        private short I, mFormMode;

        public enum DoneFormModeEnum
        {
            DFM_DoneBatch = 1,
            DFM_CancelDoneBatch = 2
        }

        public DataSet dsDoneBatch
        {
            set
            {
                mdsDoneBatch = value;
            }
        }

        public DataRow CurrentRow
        {
            set
            {
                mCurrentRow = value;
            }
        }

        public DoneFormModeEnum FormMode
        {
            set
            {
                mFormMode = (short)value;
            }
        }

        private void frmDoneBatch_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdSave, 6);
            Module1.SetButtonsImage(cmdExit, 5);
            cmdCalcGarbageQuantity.Tag = "0";
            FormLoad();
        }

        private void frmDoneBatch_FormClosing(object sender, FormClosingEventArgs e)
        {
            {
                var withBlock = mdsDoneBatch;
                withBlock.RejectChanges();
                withBlock.Relations.Clear();
                for (I = (short)(withBlock.Tables.Count - 1); I >= 2; I += -1)
                {
                    withBlock.Tables[I].Dispose();
                    withBlock.Tables.RemoveAt(I);
                }
            }

            daBatch.Dispose();
            daProductionDetail.Dispose();
            mdsDoneBatch = null;
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dgDetails.Columns[e.ColumnIndex].Name.Equals("colRealOverQuantity"))
                {
                    dgDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.FromArgb(128, 255, 128);
                }
                else if (dgDetails.Columns[e.ColumnIndex].Name.Equals("colGarbageQuantity"))
                {
                    dgDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                }
            }
        }

        private void cmdCalcGarbageQuantity_Click(object sender, EventArgs e)
        {
            if (txtDoneQuantity.Value == 0m)
            {
                MessageBox.Show("تعداد محصول تولید شده را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtDoneQuantity.Focus();
                return;
            }

            try
            {
                for (int I = 0, loopTo = dgDetails.Rows.Count - 1; I <= loopTo; I++)
                {
                    dgDetails.Rows[I].Cells["colCalcedOverStock"].Value = Conversions.ToInteger(dgDetails.Rows[I].Cells["colProductionStock"].Value) - txtDoneQuantity.Value;
                    dgDetails.Rows[I].Cells["colGarbageQuantity"].Value = Conversions.ToInteger(dgDetails.Rows[I].Cells["colCalcedOverStock"].Value) - Conversions.ToInteger(dgDetails.Rows[I].Cells["colRealOverQuantity"].Value);
                }

                cmdCalcGarbageQuantity.Tag = "1";
            }
            catch (Exception objEx)
            {
                Logger.SaveError(Name + ".cmdCalcGarbageQuantity_Click", objEx.Message);
                MessageBox.Show("محاسبۀ تعداد ضایعات با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (mFormMode == (int)DoneFormModeEnum.DFM_DoneBatch)
            {
                if (txtDoneQuantity.Value == 0m)
                {
                    MessageBox.Show("تعداد محصول تولید شده را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    txtDoneQuantity.Focus();
                    return;
                }

                if (txtDoneDate.Text is null || txtDoneDate.Text.Equals("") || txtDoneDate.Text.Equals("0"))
                {
                    MessageBox.Show("تاریخ بستن بچ را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    txtDoneDate.Focus();
                    return;
                }

                if (cmdCalcGarbageQuantity.Tag.ToString().Equals("0"))
                {
                    MessageBox.Show("محاسبۀ تعداد ضایعات انجام نشده است", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    cmdCalcGarbageQuantity.Focus();
                    return;
                }

                if (!CheckProductionValidate())
                {
                    if (MessageBox.Show("تعدادی از عملیاتها به اندازۀ تعداد بستن بچ تولید نشده اند، آیا جهت بستن بچ مطمئن هستید", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading) == DialogResult.No)
                    {
                        return;
                    }
                }
            }

            SqlTransaction trnUpdate = null;
            try
            {
                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();
                trnUpdate = Module1.cnProductionPlanning.BeginTransaction();
                daBatch.UpdateCommand.Transaction = trnUpdate;
                daProductionDetail.DeleteCommand.Transaction = trnUpdate;
                daProductionDetail.InsertCommand.Transaction = trnUpdate;
                DeleteBatchDetails();
                switch (mFormMode)
                {
                    case (short)DoneFormModeEnum.DFM_DoneBatch:
                        {
                            mCurrentRow.BeginEdit();
                            mCurrentRow["RealProductionQuantity"] = txtDoneQuantity.Value;
                            mCurrentRow["FinishedDate"] = Strings.Replace(txtDoneDate.Text, "/", "");
                            mCurrentRow.EndEdit();
                            break;
                        }

                    case (short)DoneFormModeEnum.DFM_CancelDoneBatch:
                        {
                            mCurrentRow.BeginEdit();
                            mCurrentRow["RealProductionQuantity"] = 0;
                            mCurrentRow["FinishedDate"] = 0;
                            mCurrentRow.EndEdit();
                            break;
                        }
                }

                for (short I = 0, loopTo = (short)(dgDetails.Rows.Count - 1); I <= loopTo; I++)
                    CreateDetailStockRecord(I);
                SaveChanges();
                trnUpdate.Commit();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception objEx)
            {
                mCurrentRow.CancelEdit();
                trnUpdate.Rollback();
                MessageBox.Show(objEx.Message);
            }
            finally
            {
                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                    Module1.cnProductionPlanning.Close();
            }
        }

        private void FormLoad()
        {
            try
            {
                dvDetails = mdsDoneBatch.Tables["Tbl_ProductTreeDetails"].DefaultView;
                CreateDataAdapterCommands();
                FillForm();
                switch (mFormMode)
                {
                    case (short)DoneFormModeEnum.DFM_DoneBatch:
                        {
                            cmdSave.Text = "تایید بستن";
                            break;
                        }

                    case (short)DoneFormModeEnum.DFM_CancelDoneBatch:
                        {
                            cmdSave.Text = "تایید بازکردن";
                            break;
                        }
                }
            }
            catch (Exception objEx)
            {
                MessageBox.Show("فراخوانی فرم با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(objEx.Message);
            }
        }

        private void FillForm()
        {
            txtDoneQuantity.Value = Conversions.ToDecimal(mCurrentRow["RealProductionQuantity"]);
            txtDoneDate.Text = Conversions.ToString(mCurrentRow["FinishedDate"]);
            if (txtDoneDate.Text == "0") txtDoneDate.Text = "";
            dgBatch.Rows.Add(mCurrentRow["BatchCode"], mCurrentRow["DefineDate"], Operators.ConcatenateObject(Operators.ConcatenateObject(mCurrentRow["ProductCode"], " "), mCurrentRow["ProductName"]), mCurrentRow["Productionquantity"], mCurrentRow["RealStartDate"], mCurrentRow["ProductionProgressMeasure"]);
            dgDetails.Rows.Clear();
            dvDetails.Sort = "LevelNo";
            mdsDoneBatch.Tables["Tbl_ProductionBatchsDetail"].DefaultView.Sort = "LevelNo";

            // If mdsDoneBatch.Tables("Tbl_ProductionBatchsDetail").DefaultView.Count > 0 Then
            if (!mCurrentRow["FinishedDate"].ToString().Equals("0"))
            {
                CreateBatchStockDetailGridRow(Conversions.ToString(mCurrentRow["ProductTreeCode"]), Conversions.ToString(mCurrentRow["BatchCode"]), Conversions.ToString(mdsDoneBatch.Tables["Tbl_ProductionBatchsDetail"].DefaultView[0]["DetailCode"]), 2);
                cmdCalcGarbageQuantity.Tag = "1";
            }
            else if (dvDetails.Count > 0)
            {
                CreateBatchStockDetailGridRow(Conversions.ToString(mCurrentRow["ProductTreeCode"]), Conversions.ToString(mCurrentRow["BatchCode"]), Conversions.ToString(dvDetails[0]["DetailCode"]), 1);
            }
        }

        private void CreateBatchStockDetailGridRow(string TreeCode, string BatchCode, string DetailCode, short SourceType)
        {
            DataRow[] drStock = null;
            switch (SourceType)
            {
                case 1: // سابقۀ بستن در جدول موجودی بچ وجود ندارد
                    {
                        int DetailProductionQuantity = GetDetailProductionQuantity(TreeCode, DetailCode, BatchCode);
                        drStock = mdsDoneBatch.Tables["Tbl_ProductionBatchsDetail"].Select("DetailCode='" + DetailCode + "'");
                        dgDetails.Rows.Add(drStock[0]["DetailCode"], drStock[0]["DetailName"], drStock[0]["Stock"], drStock[0]["RequirementQuantity"], DetailProductionQuantity, Operators.AddObject(DetailProductionQuantity, drStock[0]["Stock"]), 0, 0, 0);
                        drStock = mdsDoneBatch.Tables["Tbl_ProductTreeDetails"].Select("ParentDetailCode='" + DetailCode + "'");
                        break;
                    }

                case 2: // قبلا در جدول موجودی بچ سابقۀ بستن ثبت شده است
                    {
                        drStock = mdsDoneBatch.Tables["Tbl_ProductionBatchsDetail"].Select("DetailCode='" + DetailCode + "'");
                        dgDetails.Rows.Add(drStock[0]["DetailCode"], drStock[0]["DetailName"], drStock[0]["Stock"], drStock[0]["RequirementQuantity"], drStock[0]["ProductionQuantity"], drStock[0]["ProductionStock"], drStock[0]["CalcedOverStock"], drStock[0]["OverStock"], drStock[0]["GarbageQuantity"]);
                        drStock = mdsDoneBatch.Tables["Tbl_ProductTreeDetails"].Select("ParentDetailCode='" + DetailCode + "'");
                        break;
                    }
            }

            if (drStock.Length > 0)
            {
                for (short I = 0, loopTo = (short)(drStock.Length - 1); I <= loopTo; I++)
                    CreateBatchStockDetailGridRow(TreeCode, BatchCode, Conversions.ToString(drStock[I]["DetailCode"]), SourceType);
            }
        }

        private int GetDetailProductionQuantity(string mTreeCode, string mDetailCode, string mBatchCode)
        {
            int mProductionQuantity = 0;
            string LastOperation = GetDetailLastOperation(mTreeCode, mDetailCode);
            if (!LastOperation.Equals("-1"))
            {
                using (var cn = new SqlConnection(Module1.PlanningCnnStr))
                {
                    cn.Open();
                    var cmProductionQty = new SqlCommand("Select IsNull(Sum(IntactQuantity),0) From Tbl_RealProduction Where TreeCode = " + mTreeCode + " And OperationCode = " + LastOperation + " And SubbatchCode IN (Select SubbatchCode From Tbl_ProductionSubbatchs Where BatchCode = '" + mBatchCode + "')", cn);
                    mProductionQuantity = Conversions.ToInteger(cmProductionQty.ExecuteScalar());
                    cn.Close();
                }
            }

            return mProductionQuantity;
        }

        private string GetDetailLastOperation(string mTreeCode, string mDetailCode)
        {
            string LastOperation = "-1";
            NavigatedOperations = Constants.vbNullString;
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    // فراخوانی عملیات هایی که پیشنیاز آنها در جزء جاری وجود ندارد و در اجزاء دیگر درخت می باشد
                    var cmDetailOperations = new SqlCommand("Select OperationCode " + "From   Tbl_ProductOPCs " + "Where  TreeCode = " + mTreeCode + " And DetailCode = '" + mDetailCode + "' And " + "       OperationCode IN (Select CurrentOperationCode From Tbl_PreOperations Where TreeCode = " + mTreeCode + " And " + "       Not PreOperationCode IN (Select OperationCode From Tbl_ProductOPCs Where  TreeCode = " + mTreeCode + " And DetailCode = '" + mDetailCode + "'))", cn);



                    var drDetailOperations = cmDetailOperations.ExecuteReader();
                    while (drDetailOperations.Read())
                    {
                        if (NavigatedOperations is object)
                        {
                            if (!NavigatedOperations.Contains(Conversions.ToString(drDetailOperations["OperationCode"])))
                            {
                                NavigatedOperations = Conversions.ToString(NavigatedOperations + Interaction.IIf(string.IsNullOrEmpty(NavigatedOperations), Operators.ConcatenateObject(Operators.ConcatenateObject("'", drDetailOperations["OperationCode"]), "'"), Operators.ConcatenateObject(Operators.ConcatenateObject(",'", drDetailOperations["OperationCode"]), "'")));
                                AddAfterardOperations(mTreeCode, mDetailCode, Conversions.ToString(drDetailOperations["OperationCode"]));
                            }
                        }
                        else
                        {
                            NavigatedOperations = Conversions.ToString(NavigatedOperations + Interaction.IIf(string.IsNullOrEmpty(NavigatedOperations), Operators.ConcatenateObject(Operators.ConcatenateObject("'", drDetailOperations["OperationCode"]), "'"), Operators.ConcatenateObject(Operators.ConcatenateObject(",'", drDetailOperations["OperationCode"]), "'")));
                            AddAfterardOperations(mTreeCode, mDetailCode, Conversions.ToString(drDetailOperations["OperationCode"]));
                        }
                    }

                    drDetailOperations.Close();
                    // فراخوانی لیست عملیات هایی که دارای پیش نیاز نیستند
                    cmDetailOperations.CommandText = "Select OperationCode " + "From   Tbl_ProductOPCs " + "Where  TreeCode = " + mTreeCode + " And DetailCode = '" + mDetailCode + "' And " + "       Not OperationCode IN (Select CurrentOperationCode From Tbl_PreOperations Where TreeCode = " + mTreeCode + ")";


                    drDetailOperations = cmDetailOperations.ExecuteReader();
                    while (drDetailOperations.Read())
                    {
                        if (NavigatedOperations is object)
                        {
                            if (!NavigatedOperations.Contains(Conversions.ToString(drDetailOperations["OperationCode"])))
                            {
                                NavigatedOperations = Conversions.ToString(NavigatedOperations + Interaction.IIf(string.IsNullOrEmpty(NavigatedOperations), Operators.ConcatenateObject(Operators.ConcatenateObject("'", drDetailOperations["OperationCode"]), "'"), Operators.ConcatenateObject(Operators.ConcatenateObject(",'", drDetailOperations["OperationCode"]), "'")));
                                AddAfterardOperations(mTreeCode, mDetailCode, Conversions.ToString(drDetailOperations["OperationCode"]));
                            }
                        }
                        else
                        {
                            NavigatedOperations = Conversions.ToString(NavigatedOperations + Interaction.IIf(string.IsNullOrEmpty(NavigatedOperations), Operators.ConcatenateObject(Operators.ConcatenateObject("'", drDetailOperations["OperationCode"]), "'"), Operators.ConcatenateObject(Operators.ConcatenateObject(",'", drDetailOperations["OperationCode"]), "'")));
                            AddAfterardOperations(mTreeCode, mDetailCode, Conversions.ToString(drDetailOperations["OperationCode"]));
                        }
                    }

                    drDetailOperations.Close();
                    if (!string.IsNullOrEmpty(NavigatedOperations))
                    {
                        var SplitedCodes = NavigatedOperations.Split(',');
                        LastOperation = SplitedCodes[SplitedCodes.Length - 1];
                    }
                }
                catch (Exception objEx)
                {
                    Logger.LogException("GetDetailLastOperation", objEx);
                    LastOperation = "-1";
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }

            return LastOperation;
        }

        private void AddAfterardOperations(string mTreeCode, string mDetailCode, string PreOperationCode)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmHasAfterward = new SqlCommand("Select A.CurrentOperationCode As OperationCode From Tbl_PreOperations A Inner Join Tbl_ProductOPCs B ON A.TreeCode = B.TreeCode And A.CurrentOperationCode = B.OperationCode Where A.TreeCode = " + mTreeCode + " And A.PreOperationCode = '" + PreOperationCode + "' And A.CurrentOperationCode IN (Select OperationCode From Tbl_ProductOPCs Where TreeCode = " + mTreeCode + " And DetailCode = '" + mDetailCode + "')", cn);
                var drHasAfterward = cmHasAfterward.ExecuteReader();
                if (drHasAfterward.Read())
                {
                    if (!NavigatedOperations.Contains(Conversions.ToString(drHasAfterward["OperationCode"])))
                    {
                        NavigatedOperations = Conversions.ToString(NavigatedOperations + Interaction.IIf(string.IsNullOrEmpty(NavigatedOperations), Operators.ConcatenateObject(Operators.ConcatenateObject("'", drHasAfterward["OperationCode"]), "'"), Operators.ConcatenateObject(Operators.ConcatenateObject(",'", drHasAfterward["OperationCode"]), "'")));
                        AddAfterardOperations(mTreeCode, mDetailCode, Conversions.ToString(drHasAfterward["OperationCode"]));
                    }
                }

                cn.Close();
            }
        }

        private bool CheckProductionValidate()
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    var cmValidate = new SqlCommand("", cn);
                    int DPQty = 1;
                    for (int I = 0, loopTo = dgDetails.Rows.Count - 1; I <= loopTo; I++)
                    {
                        DPQty = Module1.GetDetailParentQuantity(Conversions.ToString(mCurrentRow["ProductTreeCode"]), Conversions.ToString(dgDetails.Rows[I].Cells["colDetailCode"].Value));
                        //cmValidate.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select   OperationCode, Convert(Int,(IsNull(Sum(IntactQuantity),0)/ " + DPQty.ToString() + ")) As IntactQuantity " + "From     Tbl_RealProduction " + "Where    TreeCode = ", mCurrentRow["ProductTreeCode"]), " And "), "         OperationCode IN (Select OperationCode From Tbl_ProductOPCs Where TreeCode = "), mCurrentRow["ProductTreeCode"]), " And "), "                                                                           DetailCode = '"), dgDetails.Rows[I].Cells["colDetailCode"].Value), "') And "), "         SubbatchCode IN (Select SubbatchCode From Tbl_ProductionSubbatchs Where BatchCode = '"), mCurrentRow["BatchCode"]), "') "), "Group By OperationCode "), "Union "), "Select OperationCode , 0 "), "From   Tbl_ProductOPCs "), "Where  TreeCode = "), mCurrentRow["ProductTreeCode"]), " And DetailCode = '"), dgDetails.Rows[I].Cells["colDetailCode"].Value), "' And "), "       Not OperationCode IN (Select OperationCode "), "                             From   Tbl_RealProduction "), "                             Where  TreeCode = "), mCurrentRow["ProductTreeCode"]), " And SubbatchCode IN (Select SubbatchCode "), "                                                                                                           From   Tbl_ProductionSubbatchs "), "                                                                                                           Where  BatchCode = '"), mCurrentRow["BatchCode"]), "'))"));
                       
                        cmValidate.CommandText = "Select OperationCode, " + Environment.NewLine +
                                                 "       IntactQuantity = Convert(Int,(IsNull(Sum(IntactQuantity),0)/ " + DPQty.ToString() + ")) " + Environment.NewLine +
                                                 "From Tbl_RealProduction " + Environment.NewLine +
                                                 "WHERE TreeCode = " + Conversions.ToString(mCurrentRow["ProductTreeCode"]) + Environment.NewLine +
                                                 "  And OperationCode IN (Select OperationCode From Tbl_ProductOPCs " + Environment.NewLine +
                                                 "                        Where TreeCode =" + Conversions.ToString(mCurrentRow["ProductTreeCode"]) + Environment.NewLine +
                                                 "                          And DetailCode = '" + Conversions.ToString(dgDetails.Rows[I].Cells["colDetailCode"].Value) + "'" + Environment.NewLine +
                                                 "                          And SubbatchCode IN ( Select SubbatchCode "  + Environment.NewLine + 
                                                 "                                                From Tbl_ProductionSubbatchs " + Environment.NewLine +
                                                 "                                                Where BatchCode = '" + Conversions.ToString(mCurrentRow["BatchCode"]) + "'" + Environment.NewLine +
                                                 "                                              )" + Environment.NewLine +
                                                 "                        )" + Environment.NewLine +
                                                 "Group By OperationCode " + Environment.NewLine +
                                                 "Union ALL " + Environment.NewLine +
                                                 "Select OperationCode , 0 " + Environment.NewLine +
                                                 "From Tbl_ProductOPCs " + Environment.NewLine +
                                                 "Where TreeCode = " + Conversions.ToString(mCurrentRow["ProductTreeCode"]) + Environment.NewLine +
                                                 "  And DetailCode = '" + Conversions.ToString(dgDetails.Rows[I].Cells["colDetailCode"].Value) + "'" + Environment.NewLine +
                                                 "  And Not OperationCode IN (Select OperationCode " + Environment.NewLine +
                                                 "                            From Tbl_RealProduction " + Environment.NewLine +
                                                 "                            Where TreeCode = " + Conversions.ToString(mCurrentRow["ProductTreeCode"]) + Environment.NewLine +
                                                 "                              And SubbatchCode IN ( Select SubbatchCode " + Environment.NewLine +
                                                 "                                                    From Tbl_ProductionSubbatchs  " + Environment.NewLine +
                                                 "                                                    Where BatchCode = '" + Conversions.ToString(mCurrentRow["BatchCode"]) + "'" + Environment.NewLine +
                                                 "                                                  ) " + Environment.NewLine +
                                                 "                            ) " ;

                        var drValidate = cmValidate.ExecuteReader();
                        while (drValidate.Read())
                        {
                            if (Conversions.ToInteger(drValidate["IntactQuantity"]) < Conversions.ToInteger(txtDoneQuantity.Value))
                            {
                                drValidate.Close();
                                return false;
                            }
                        }

                        drValidate.Close();
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogException("CheckProductionValidate", ex);
                    MessageBox.Show("کنترل شرایط بستن بچ با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return false;
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }

            return true;
        }

        private void CreateDataAdapterCommands()
        {
            // --------------------- ایجاد دستورات برای جدول بج های تولید -----------------
            // ایجاد دستور اصلاح رکورد جاری در جدول
            daBatch.UpdateCommand = new SqlCommand("Update Tbl_ProductionBatchs Set FinishedDate=@FinishedDate," + "RealProductionQuantity=@RealProductionQuantity Where BatchCode=@BatchCode", Module1.cnProductionPlanning);
            {
                var withBlock = daBatch.UpdateCommand;
                withBlock.Parameters.Add("@FinishedDate", SqlDbType.VarChar, 8, "FinishedDate");
                withBlock.Parameters.Add("@RealProductionQuantity", SqlDbType.Int, default, "RealProductionQuantity");
                withBlock.Parameters.Add("@BatchCode", SqlDbType.VarChar, 20, "BatchCode").SourceVersion = DataRowVersion.Original;
            }

            // ------>>>>>>>>>>>>> موجودی اولیه بچ تولید
            daProductionDetail.InsertCommand = new SqlCommand("Insert Into Tbl_ProductionBatchsDetail(BatchCode,DetailCode,Stock,RequirementQuantity,OverStock,ProductionQuantity,ProductionStock,CalcedOverStock,GarbageQuantity) " + "Values(@BatchCode,@DetailCode,@Stock,@RequirementQuantity,@OverStock,@ProductionQuantity,@ProductionStock,@CalcedOverStock,@GarbageQuantity)", Module1.cnProductionPlanning);
            {
                var withBlock1 = daProductionDetail.InsertCommand;
                withBlock1.Parameters.Add("@BatchCode", SqlDbType.VarChar, 20, "BatchCode");
                withBlock1.Parameters.Add("@DetailCode", SqlDbType.VarChar, 50, "DetailCode");
                withBlock1.Parameters.Add("@Stock", SqlDbType.Int, default, "Stock");
                withBlock1.Parameters.Add("@RequirementQuantity", SqlDbType.BigInt, default, "RequirementQuantity");
                withBlock1.Parameters.Add("@OverStock", SqlDbType.Int, default, "OverStock");
                withBlock1.Parameters.Add("@ProductionQuantity", SqlDbType.Int, default, "ProductionQuantity");
                withBlock1.Parameters.Add("@ProductionStock", SqlDbType.Int, default, "ProductionStock");
                withBlock1.Parameters.Add("@CalcedOverStock", SqlDbType.Int, default, "CalcedOverStock");
                withBlock1.Parameters.Add("@GarbageQuantity", SqlDbType.Int, default, "GarbageQuantity");
            }

            daProductionDetail.DeleteCommand = new SqlCommand("Delete From Tbl_ProductionBatchsDetail Where BatchCode=@BatchCode And DetailCode=@DetailCode", Module1.cnProductionPlanning);
            {
                var withBlock2 = daProductionDetail.DeleteCommand;
                withBlock2.Parameters.Add("@BatchCode", SqlDbType.VarChar, 20, "BatchCode").SourceVersion = DataRowVersion.Original;
                withBlock2.Parameters.Add("@DetailCode", SqlDbType.VarChar, 50, "DetailCode").SourceVersion = DataRowVersion.Original;
            }
        }

        private void DeleteBatchDetails()
        {
            mdsDoneBatch.Tables["Tbl_ProductionBatchsDetail"].DefaultView.RowFilter = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("BatchCode='", mCurrentRow["BatchCode"]), "'"));
            for (short I = (short)(mdsDoneBatch.Tables["Tbl_ProductionBatchsDetail"].DefaultView.Count - 1); I >= 0; I += -1)
                mdsDoneBatch.Tables["Tbl_ProductionBatchsDetail"].DefaultView[I].Delete();
        }

        private void CreateDetailStockRecord(short RowNo)
        {
            DataRow drInsert;
            drInsert = mdsDoneBatch.Tables["Tbl_ProductionBatchsDetail"].NewRow();
            drInsert["BatchCode"] = mCurrentRow["BatchCode"];
            drInsert["DetailCode"] = dgDetails.Rows[RowNo].Cells["colDetailCode"].Value;
            drInsert["Stock"] = dgDetails.Rows[RowNo].Cells["colPrimaryStock"].Value;
            drInsert["RequirementQuantity"] = dgDetails.Rows[RowNo].Cells["colRequirementQuntity"].Value;
            switch (mFormMode)
            {
                case (short)DoneFormModeEnum.DFM_DoneBatch:
                    {
                        drInsert["OverStock"] = dgDetails.Rows[RowNo].Cells["colRealOverQuantity"].Value;
                        drInsert["ProductionQuantity"] = dgDetails.Rows[RowNo].Cells["colProductionQuantity"].Value;
                        drInsert["ProductionStock"] = dgDetails.Rows[RowNo].Cells["colProductionStock"].Value;
                        drInsert["CalcedOverStock"] = dgDetails.Rows[RowNo].Cells["colCalcedOverStock"].Value;
                        drInsert["GarbageQuantity"] = dgDetails.Rows[RowNo].Cells["colGarbageQuantity"].Value;
                        break;
                    }

                case (short)DoneFormModeEnum.DFM_CancelDoneBatch:
                    {
                        drInsert["OverStock"] = 0;
                        drInsert["ProductionQuantity"] = 0;
                        drInsert["ProductionStock"] = 0;
                        drInsert["CalcedOverStock"] = 0;
                        drInsert["GarbageQuantity"] = 0;
                        break;
                    }
            }

            drInsert["ParentDetailCode"] = 0;
            drInsert["TreeCode"] = 0;
            drInsert["LevelNo"] = 0;
            drInsert["DetailName"] = 0;
            mdsDoneBatch.Tables["Tbl_ProductionBatchsDetail"].Rows.Add(drInsert);
        }

        private void SaveChanges()
        {
            var dsChanges = mdsDoneBatch.GetChanges();
            if (dsChanges.HasErrors)
            {
                mdsDoneBatch.RejectChanges();
            }
            else
            {
                daBatch.Update(dsChanges, "Tbl_ProductionBatchs");
                daProductionDetail.Update(dsChanges, "Tbl_ProductionBatchsDetail");
                mdsDoneBatch.AcceptChanges();
            }

            dsChanges = null;
        }
    }
}