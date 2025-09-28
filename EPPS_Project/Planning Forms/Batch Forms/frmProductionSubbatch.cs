using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmProductionSubbatch
    {
        public frmProductionSubbatch()
        {
            InitializeComponent();
            _cmdSave.Name = "cmdSave";
            _cmdExit.Name = "cmdExit";
        }

        private frmPlaningSubbatchsList mListForm;
        private SqlDataAdapter daSubbatch = new SqlDataAdapter();
        private SqlDataAdapter daProductionSubbatchDetail = new SqlDataAdapter();
        private DataRow CurrentRow; // برای نگهداری رکورد جاری
        private short I;
        private short mEditMode = -1; // 1:Not enter to production    2:Enter to production

        public frmPlaningSubbatchsList ListForm
        {
            get
            {
                return mListForm;
            }

            set
            {
                mListForm = value;
            }
        }

        public DataSet dsProductionPlanning
        {
            get
            {
                return ListForm.dsProductionPlanning;
            }
        }

        public short EditMode
        {
            set
            {
                mEditMode = value;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdSave, 6);
            Module1.SetButtonsImage(cmdExit, 5);
            FormLoad();
        }

        private void frmContractor_FormClosing(object sender, FormClosingEventArgs e)
        {
            {
                var withBlock = dsProductionPlanning;
                withBlock.RejectChanges();
                withBlock.Relations.Clear();
                for (I = (short)(withBlock.Tables.Count - 1); I >= 1; I += -1)
                {
                    withBlock.Tables[I].Dispose();
                    withBlock.Tables.RemoveAt(I);
                }
            }

            daSubbatch.Dispose();
            daProductionSubbatchDetail.Dispose();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!FormValidation())
            {
                return;
            }

            if (Conversions.ToDouble(Strings.Replace(txtDeliveryDate.Text, "/", "")) > 0d && Operators.ConditionalCompareObjectLess(Strings.Replace(txtDeliveryDate.Text, "/", ""), CurrentRow["FirstDelivaryDate"], false))
            {
                MessageBox.Show("تاریخ تحویل اولین محموله نباید از تاریخ تحویل اولین محمولۀ بچ کوچکتر باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                txtDeliveryDate.Focus();
                return;
            }

            DataRow[] drValidation;
            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(CurrentRow["SubbatchNo"], 1, false)))
            {
                drValidation = dsProductionPlanning.Tables["Tbl_PlanningSubbatchs"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("BatchCode='", CurrentRow["BatchCode"]), "' And SubbatchNo="), Operators.SubtractObject(CurrentRow["SubbatchNo"], 1))));
                if (Conversions.ToInteger(Strings.Replace(txtDeliveryDate.Text, "/", "")) > 0 && Conversions.ToInteger(Strings.Replace(txtDeliveryDate.Text, "/", "")) < Conversions.ToInteger(drValidation[0]["SubbatchFirstDeliveryDate"]))
                {
                    MessageBox.Show("تاریخ تحویل اولین محموله نباید از تاریخ تحویل اولین محمولۀ ساب بچ قبلی کوچکتر باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                    drValidation = null;
                    txtDeliveryDate.Focus();
                    return;
                }

                drValidation = dsProductionPlanning.Tables["Tbl_PlanningSubbatchs"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("BatchCode='", CurrentRow["BatchCode"]), "' And SubbatchNo="), Operators.AddObject(CurrentRow["SubbatchNo"], 1))));
                if (drValidation.Length > 0)
                {
                    if (Conversions.ToInteger(drValidation[0]["SubbatchFirstDeliveryDate"]) > 0 && Conversions.ToInteger(Strings.Replace(txtDeliveryDate.Text, "/", "")) > Conversions.ToInteger(drValidation[0]["SubbatchFirstDeliveryDate"]))
                    {
                        MessageBox.Show("تاریخ تحویل اولین محموله نباید از تاریخ تحویل اولین محمولۀ ساب بچ بعدی بزرگتر باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                        drValidation = null;
                        txtDeliveryDate.Focus();
                        return;
                    }
                }

                drValidation = dsProductionPlanning.Tables["Tbl_PlanningSubbatchs"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("BatchCode='", CurrentRow["BatchCode"]), "' And SubbatchNo="), Operators.SubtractObject(CurrentRow["SubbatchNo"], 1))));
                if (drValidation.Length > 0)
                {
                    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectLessEqual(drValidation[0]["PlanningStartDate"], "0", false)))
                    {
                        if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(drValidation[0]["ProductionPriority"], 0, false)))
                        {
                            MessageBox.Show("ساب بچ قبل برنامه ریزی نشده است", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                            drValidation = null;
                            txtExecutionPriority.Focus();
                            return;
                        }
                    }

                    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(drValidation[0]["ProductionPriority"], 0, false)) && txtExecutionPriority.Value > 0m && Conversions.ToBoolean(Operators.ConditionalCompareObjectGreaterEqual(drValidation[0]["ProductionPriority"], txtExecutionPriority.Value, false)))
                    {
                        MessageBox.Show("اولویت اجرای ساب بچ، نباید کوچکتر یا مساوی الویت اجرای ساب بچ قبلی خود باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                        drValidation = null;
                        txtExecutionPriority.Focus();
                        return;
                    }
                }
            }
            else if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(CurrentRow["SubbatchNo"], 1, false)))
            {
                drValidation = dsProductionPlanning.Tables["Tbl_PlanningSubbatchs"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("BatchCode='", CurrentRow["BatchCode"]), "' And SubbatchNo="), Operators.AddObject(CurrentRow["SubbatchNo"], 1))));
                if (drValidation.Length > 0)
                {
                    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(drValidation[0]["SubbatchFirstDeliveryDate"], 0, false)))
                    {
                        if (Conversions.ToInteger(Strings.Replace(txtDeliveryDate.Text, "/", "")) > Conversions.ToInteger(drValidation[0]["SubbatchFirstDeliveryDate"]))
                        {
                            MessageBox.Show("تاریخ تحویل اولین محموله نباید از تاریخ تحویل اولین محمولۀ ساب بچ بعدی بزرگتر باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                            drValidation = null;
                            txtDeliveryDate.Focus();
                            return;
                        }
                    }
                }
            }

            drValidation = dsProductionPlanning.Tables["Tbl_PlanningSubbatchs"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("SubbatchCode <> '", CurrentRow["SubbatchCode"]), "' And ProductionPriority > 0 And ProductionPriority="), txtExecutionPriority.Value)));
            if (drValidation.Length > 0)
            {
                MessageBox.Show("اولویت اجرای وارد شده تکراری است", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                drValidation = null;
                txtExecutionPriority.Focus();
                return;
            }

            drValidation = null;
            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                Module1.cnProductionPlanning.Open();
            var trnSubbatch = Module1.cnProductionPlanning.BeginTransaction();
            string mAlarmDesc = Constants.vbNullString;
            try
            {
                daSubbatch.UpdateCommand.Transaction = trnSubbatch;
                daProductionSubbatchDetail.DeleteCommand.Transaction = trnSubbatch;
                daProductionSubbatchDetail.InsertCommand.Transaction = trnSubbatch;
                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(CurrentRow["ProductionQuantityinSubbatch"], txtProductionQuantity.Text, false)))
                {
                    mAlarmDesc = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("تعداد تولید ساب بچ: {", CurrentRow["SubbatchCode"]), "} تغییر کرد"));
                }

                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(CurrentRow["SubbatchFirstDeliveryDate"], Strings.Replace(txtDeliveryDate.Text, "/", ""), false)))
                {
                    if (string.IsNullOrEmpty(mAlarmDesc))
                    {
                        mAlarmDesc = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("تاریخ تحویل اولین محموله ساب بچ: {", CurrentRow["SubbatchCode"]), "} تغییر کرد"));
                    }
                    else
                    {
                        mAlarmDesc = Conversions.ToString(mAlarmDesc + Operators.ConcatenateObject(Operators.ConcatenateObject(Constants.vbCrLf + "تاریخ تحویل اولین محموله ساب بچ: {", CurrentRow["SubbatchCode"]), "} تغییر کرد"));
                    }
                }

                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(CurrentRow["TransferMinimumQuantity"], txtTransferMinQty.Value, false)))
                {
                    if (string.IsNullOrEmpty(mAlarmDesc))
                    {
                        mAlarmDesc = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("تعداد تحویل اولین محموله ساب بچ: {", CurrentRow["SubbatchCode"]), "} تغییر کرد"));
                    }
                    else
                    {
                        mAlarmDesc = Conversions.ToString(mAlarmDesc + Operators.ConcatenateObject(Operators.ConcatenateObject(Constants.vbCrLf + "تعداد تحویل اولین محموله ساب بچ: {", CurrentRow["SubbatchCode"]), "} تغییر کرد"));
                    }
                }

                DeleteSubbatchDetails();
                CurrentRow.BeginEdit();
                CurrentRow["ProductionQuantityinSubbatch"] = txtProductionQuantity.Text;
                CurrentRow["SubbatchFirstDeliveryDate"] = Strings.Replace(txtDeliveryDate.Text, "/", "");
                CurrentRow["TransferMinimumQuantity"] = txtTransferMinQty.Value;
                CurrentRow["ProductionPriority"] = txtExecutionPriority.Text;
                CurrentRow.EndEdit();
                if (Conversions.ToDouble(txtExecutionPriority.Text) == 0d)
                {
                    var cmDeletePLanning = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Delete From Tbl_Planning Where SubbatchCode='", CurrentRow["SubbatchCode"]), "' And PlanningCode Not IN (Select PlanningCode From Tbl_RealProduction)")), Module1.cnProductionPlanning);
                    cmDeletePLanning.Transaction = trnSubbatch;
                    cmDeletePLanning.ExecuteNonQuery();
                    CurrentRow.BeginEdit();
                    CurrentRow["PlanningStartDate"] = 0;
                    CurrentRow["PlanningStartHour"] = 0;
                    CurrentRow["PlanningEndDate"] = 0;
                    CurrentRow["PlanningEndHour"] = 0;
                    CurrentRow.EndEdit();
                    var drChangePriority = dsProductionPlanning.Tables["Tbl_PlanningSubbatchs"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("BatchCode='", CurrentRow["BatchCode"]), "' And SubbatchNo >"), CurrentRow["SubbatchNo"]), " And ProductionPriority > 0")));
                    for (short I = 0, loopTo = (short)(drChangePriority.Length - 1); I <= loopTo; I++)
                    {
                        drChangePriority[I]["ProductionPriority"] = 0;
                        cmDeletePLanning.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Delete From Tbl_Planning Where SubbatchCode='", drChangePriority[I]["SubbatchCode"]), "' And PlanningCode Not IN (Select PlanningCode From Tbl_RealProduction)"));
                        cmDeletePLanning.ExecuteNonQuery();
                        drChangePriority[I].BeginEdit();
                        drChangePriority[I]["PlanningStartDate"] = 0;
                        drChangePriority[I]["PlanningStartHour"] = 0;
                        drChangePriority[I]["PlanningEndDate"] = 0;
                        drChangePriority[I]["PlanningEndHour"] = 0;
                        drChangePriority[I].EndEdit();
                    }
                }

                for (short I = 0, loopTo1 = (short)(dgDetails.Rows.Count - 1); I <= loopTo1; I++)
                    CreateSubbatchDetailStockRecord(I);
                SaveChanges();
                trnSubbatch.Commit();
                if (!string.IsNullOrEmpty(mAlarmDesc))
                {
                    Module1.Check_Subbatch_HasPlanningAlarm(Conversions.ToString(CurrentRow["ProductTreeCode"]), Conversions.ToString(CurrentRow["BatchCode"]), Conversions.ToString(CurrentRow["SubbatchNo"]), "2", mAlarmDesc);
                }

                Close();
            }
            catch (Exception objEx)
            {
                CurrentRow.CancelEdit();
                trnSubbatch.Rollback();
                Logger.SaveError(Name + ".cmdSave_Click", objEx.Message);
                MessageBox.Show("اشکال در اصلاح رکورد، تغییرات ثبت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                trnSubbatch.Dispose();
                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                    Module1.cnProductionPlanning.Close();
            }
        }

        private void FormLoad()
        {
            try
            {
                CreateDataAdapterCommands();
                CurrentRow = ListForm.GetRow();
                // پر کردن کنترل فرم با مقدار رکورد جاری
                lblBatchCode.Text = Conversions.ToString(CurrentRow["BatchCode"]);
                lblSubbatchCode.Text = Conversions.ToString(CurrentRow["SubbatchCode"]);
                lblSubbatchSerial.Text = Conversions.ToString(CurrentRow["SubbatchNo"]);
                txtProductionQuantity.Text = Conversions.ToString(CurrentRow["ProductionQuantityinSubbatch"]);
                txtDeliveryDate.Text = Conversions.ToString(CurrentRow["SubbatchFirstDeliveryDate"]);
                if (txtDeliveryDate.Text == "0")
                    txtDeliveryDate.Text = "";
                txtExecutionPriority.Text = Conversions.ToString(CurrentRow["ProductionPriority"]);
                txtTransferMinQty.Value = Conversions.ToDecimal(CurrentRow["TransferMinimumQuantity"]);
                dsProductionPlanning.Tables["Tbl_ProductionSubbatchsDetail"].DefaultView.Sort = "LevelNo";
                CreateSubbatchStockDetailGridRow(Conversions.ToString(dsProductionPlanning.Tables["Tbl_ProductionSubbatchsDetail"].DefaultView[0]["DetailCode"]));
                if (mEditMode == 1)
                {
                    txtProductionQuantity.Focus();
                }
                else
                {
                    txtProductionQuantity.Enabled = false;
                    // txtDeliveryDate.Enabled = False
                    // txtTransferMinQty.Enabled = False
                    dgDetails.Enabled = false;
                }
            }
            catch (Exception objEx)
            {
                Logger.SaveError(Name + ".FormLoad", objEx.Message);
                MessageBox.Show("فراخوانی فرم ساب بچ با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateSubbatchDetailStockRecord(short RowNo)
        {
            DataRow drInsert;
            long ParentQuantity;
            if (RowNo == 0)
            {
                ParentQuantity = Conversions.ToLong(Operators.SubtractObject(txtProductionQuantity.Value, dgDetails.Rows[0].Cells[3].Value));
            }
            else
            {
                ParentQuantity = Conversions.ToLong(dsProductionPlanning.Tables["Tbl_ProductionSubbatchsDetail"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("SubbatchCode='" + lblSubbatchCode.Text + "' And DetailCode='", dgDetails.Rows[RowNo].Cells[5].Value), "'")))[0]["RequirementQuantity"]);
            }

            drInsert = dsProductionPlanning.Tables["Tbl_ProductionSubbatchsDetail"].NewRow();
            drInsert["SubbatchCode"] = lblSubbatchCode.Text;
            drInsert["DetailCode"] = dgDetails.Rows[RowNo].Cells[0].Value;
            drInsert["ParentDetailCode"] = 0;
            drInsert["LevelNo"] = 0;
            drInsert["DetailName"] = 0;
            drInsert["Stock"] = dgDetails.Rows[RowNo].Cells[3].Value;
            drInsert["RequirementQuantity"] = GetSubbatchDetailRequirementQty(ParentQuantity, RowNo);
            drInsert["ParentQuantity"] = 0;
            dsProductionPlanning.Tables["Tbl_ProductionSubbatchsDetail"].Rows.Add(drInsert);
        }

        private long GetSubbatchDetailRequirementQty(long ParentQty, short RowNo)
        {
            long RequirementQty;
            if (RowNo == 0)
            {
                RequirementQty = ParentQty;
            }
            else if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(Operators.SubtractObject(Operators.MultiplyObject(ParentQty, dgDetails.Rows[RowNo].Cells[2].Value), dgDetails.Rows[RowNo].Cells[3].Value), 0, false)))
            {
                RequirementQty = Conversions.ToLong(Operators.SubtractObject(Operators.MultiplyObject(ParentQty, dgDetails.Rows[RowNo].Cells[2].Value), dgDetails.Rows[RowNo].Cells[3].Value));
            }
            else
            {
                RequirementQty = 0L;
            }

            return RequirementQty;
        }

        private void CreateSubbatchStockDetailGridRow(string DetailCode)
        {
            DataRow[] drStock = null;
            drStock = dsProductionPlanning.Tables["Tbl_ProductionSubbatchsDetail"].Select("DetailCode='" + DetailCode + "'");
            dgDetails.Rows.Add(drStock[0]["DetailCode"], drStock[0]["DetailName"], drStock[0]["ParentQuantity"], drStock[0]["Stock"], drStock[0]["RequirementQuantity"], drStock[0]["ParentDetailCode"]);
            drStock = dsProductionPlanning.Tables["Tbl_ProductionSubbatchsDetail"].Select("ParentDetailCode='" + DetailCode + "'");
            if (drStock.Length > 0)
            {
                for (short I = 0, loopTo = (short)(drStock.Length - 1); I <= loopTo; I++)
                    CreateSubbatchStockDetailGridRow(Conversions.ToString(drStock[I]["DetailCode"]));
            }
        }

        private void DeleteSubbatchDetails()
        {
            short I;
            for (I = (short)(dsProductionPlanning.Tables["Tbl_ProductionSubbatchsDetail"].DefaultView.Count - 1); I >= 0; I += -1)
                dsProductionPlanning.Tables["Tbl_ProductionSubbatchsDetail"].DefaultView[I].Delete();
        }

        private void SaveChanges()
        {
            DataSet dsChanges;
            dsChanges = dsProductionPlanning.GetChanges();
            if (dsChanges.HasErrors)
            {
                dsProductionPlanning.RejectChanges();
            }
            else
            {
                daSubbatch.Update(dsChanges, "Tbl_PlanningSubbatchs");
                daProductionSubbatchDetail.Update(dsChanges, "Tbl_ProductionSubbatchsDetail");
                dsProductionPlanning.AcceptChanges();
            }

            dsChanges = null;
        }

        private void CreateDataAdapterCommands()
        {
            // ایجاد دستور اصلاح رکورد جاری در جدول
            daSubbatch.UpdateCommand = new SqlCommand("Update Tbl_ProductionSubbatchs Set ProductionQuantityinSubbatch=@ProductionQuantity," + "SubbatchFirstDeliveryDate=@FirstDeliveryDate,TransferMinimumQuantity=@TransferMinimumQuantity," + "ProductionPriority=@ProductionPriority,PlanningStartDate=@PlanningStartDate," + "PlanningStartHour=@PlanningStartHour,PlanningEndDate=@PlanningEndDate,PlanningEndHour=@PlanningEndHour " + "Where SubbatchCode=@SubbatchCode", Module1.cnProductionPlanning);



            {
                var withBlock = daSubbatch.UpdateCommand;
                withBlock.Parameters.Add("@ProductionPriority", SqlDbType.BigInt, default, "ProductionPriority");
                withBlock.Parameters.Add("@ProductionQuantity", SqlDbType.Int, default, "ProductionQuantityinSubbatch");
                withBlock.Parameters.Add("@FirstDeliveryDate", SqlDbType.VarChar, 8, "SubbatchFirstDeliveryDate");
                withBlock.Parameters.Add("@TransferMinimumQuantity", SqlDbType.Int, default, "TransferMinimumQuantity");
                withBlock.Parameters.Add("@PlanningStartDate", SqlDbType.VarChar, 8, "PlanningStartDate");
                withBlock.Parameters.Add("@PlanningStartHour", SqlDbType.VarChar, 50, "PlanningStartHour");
                withBlock.Parameters.Add("@PlanningEndDate", SqlDbType.VarChar, 8, "PlanningEndDate");
                withBlock.Parameters.Add("@PlanningEndHour", SqlDbType.VarChar, 50, "PlanningEndHour");
                withBlock.Parameters.Add("@SubbatchCode", SqlDbType.VarChar, 50, "SubbatchCode").SourceVersion = DataRowVersion.Original;
            }

            // ------>>>>>>>>>>>>> موجودی اولیه ساب بچ تولید
            daProductionSubbatchDetail.InsertCommand = new SqlCommand("Insert Into Tbl_ProductionSubbatchsDetail(SubbatchCode,DetailCode,Stock,RequirementQuantity) " + "Values(@SubbatchCode,@DetailCode,@Stock,@RequirementQuantity)", Module1.cnProductionPlanning);
            {
                var withBlock1 = daProductionSubbatchDetail.InsertCommand;
                withBlock1.Parameters.Add("@SubbatchCode", SqlDbType.VarChar, 50, "SubbatchCode");
                withBlock1.Parameters.Add("@DetailCode", SqlDbType.VarChar, 50, "DetailCode");
                withBlock1.Parameters.Add("@Stock", SqlDbType.Int, default, "Stock");
                withBlock1.Parameters.Add("@RequirementQuantity", SqlDbType.BigInt, default, "RequirementQuantity");
            }

            daProductionSubbatchDetail.DeleteCommand = new SqlCommand("Delete From Tbl_ProductionSubbatchsDetail Where SubbatchCode=@SubbatchCode And DetailCode=@DetailCode", Module1.cnProductionPlanning);
            {
                var withBlock2 = daProductionSubbatchDetail.DeleteCommand;
                withBlock2.Parameters.Add("@SubbatchCode", SqlDbType.VarChar, 50, "SubbatchCode").SourceVersion = DataRowVersion.Original;
                withBlock2.Parameters.Add("@DetailCode", SqlDbType.VarChar, 50, "DetailCode").SourceVersion = DataRowVersion.Original;
            }
        }

        private bool FormValidation()
        {
            if (txtProductionQuantity.Value > 0m)
            {
                var cmQuantityValidation = new SqlCommand("Select A.BatchCode, A.SubbatchCode,A.ProductionQuantityinSubbatch,B.Productionquantity " + "From dbo.Tbl_ProductionSubbatchs A INNER JOIN dbo.Tbl_ProductionBatchs B ON A.BatchCode = B.BatchCode " + "Where A.BatchCode='" + lblBatchCode.Text + "' And A.SubbatchCode<>'" + lblSubbatchCode.Text + "'", Module1.cnProductionPlanning);

                long Sum = 0L;
                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();
                var drQuantityValidation = cmQuantityValidation.ExecuteReader();
                while (drQuantityValidation.Read())
                    Sum = Sum + long.Parse(drQuantityValidation["ProductionQuantityinSubbatch"].ToString());
                drQuantityValidation.Close();
                cmQuantityValidation.CommandText = "Select Productionquantity From Tbl_ProductionBatchs Where BatchCode='" + lblBatchCode.Text + "'";
                long BatchProductionQuantity = Conversions.ToLong(cmQuantityValidation.ExecuteScalar());
                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                    Module1.cnProductionPlanning.Close();
                Sum = (long)Math.Round(Sum + txtProductionQuantity.Value);
                if (Sum > BatchProductionQuantity)
                {
                    MessageBox.Show("تعداد تولید وارد شده بیشتر از تعداد مجاز می باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    txtProductionQuantity.Focus();
                    return false;
                }

                drQuantityValidation.Close();
                drQuantityValidation = null;
                cmQuantityValidation.Dispose();
            }

            if (string.IsNullOrEmpty(Strings.Trim(Strings.Replace(txtDeliveryDate.Text, "/", ""))))
            {
                txtDeliveryDate.Text = "0";
                // MessageBox.Show("تاریخ تحویل اولین محموله را وارد کنید", MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
                // txtDeliveryDate.Focus()
                // Return False
            }

            return true;
        }
    }
}