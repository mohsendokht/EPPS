using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using ADODB;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using ProductionPlanning.ToolForms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace ProductionPlanning
{
    public partial class frmProductionBatch
    {
        public frmProductionBatch()
        {
            InitializeComponent();
            _cmdSave.Name = "cmdSave";
            _cmdExit.Name = "cmdExit";
            _cmdDelete.Name = "cmdDelete";
            _cbProductTree.Name = "cbProductTree";
            _dgOrders.Name = "dgOrders";
        }

        private frmBatchRecordsList mListForm;
        private SqlDataAdapter daBatch = new SqlDataAdapter();
        private long[] arPreSubbatchRemainder; // () = New String(2)(0) {}
        private short StockType = 1; // 1:GridStock 2:PreSubbatchRemainder
        private DataRow mCurrentRow; // برای نگهداری رکورد جاری
        private short mEditMode = -1;
        private string mSelectedOrderIndex = Constants.vbNullString;
        private string mSelectedOrdersProductCode = "-1";
        private short I;
        public short NotEntered_InProduction_SubbatchQuantity = -1;

        public frmBatchRecordsList ListForm
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

        public string SelectedOrderIndex
        {
            set
            {
                mSelectedOrderIndex = value;
            }
        }

        public string SelectedOrdersProductCode
        {
            set
            {
                mSelectedOrdersProductCode = value;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdSave, 6);
            Module1.SetButtonsImage(cmdDelete, 9);
            Module1.SetButtonsImage(cmdExit, 5);
            string QuantityState = Module1.GetDBConfigParamValue((int)Math.Round(Conversion.Val(Module_UserAccess.MySoftwareCode)), 102001, Module1.UMCnnStr);
            if (QuantityState is null || !QuantityState.Equals("2"))
            {
                txtProductionQuantity.ReadOnly = true;
                txtProductionQuantity.Enabled = false;
            }
            else
            {
                txtProductionQuantity.ReadOnly = false;
                txtProductionQuantity.Enabled = true;
            }

            FormLoad();

        }

        private void frmProductionBatch_FormClosing(object sender, FormClosingEventArgs e)
        {

           // cbProduct_LookUp.CB_DataSource = null;
            //{
            //    var withBlock = dsProductionPlanning;
            //    withBlock.RejectChanges();
            //    withBlock.Relations.Clear();
            //    for (I = (short)(withBlock.Tables.Count - 1); I >= 2; I += -1)
            //    {
            //        withBlock.Tables[I].Dispose();
            //        withBlock.Tables.RemoveAt(I);
            //    }
            //}

            //dsProductionPlanning.Reset();
            daBatch.Dispose();

        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            string BatchCode = mCurrentRow["BatchCode"].ToString();

            if (BatchCode == "") return;

            
            var deleteConfirm = new frmDeleteConfirm();
            deleteConfirm.DeleteMessage = GetDeleteMessage_ProductionBatch(BatchCode);
            deleteConfirm.DeleteCode = BatchCode;
            deleteConfirm.ShowDialog();
            if (!deleteConfirm.Confirmed)
            {
                Close();
                return;
            }

            var cmDelete = new SqlCommand();
            SqlTransaction trnDelete = null;
            var dvReArangeBatchs = dsProductionPlanning.Tables["Tbl_ProductionBatchs"].DefaultView;

            try
            {
                

                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();
                trnDelete = Module1.cnProductionPlanning.BeginTransaction();

           

                daBatch.DeleteCommand.Transaction = trnDelete;
                daBatch.UpdateCommand.Transaction = trnDelete;

                // حذف اطلاعات بچ
                string DeleteSQL = 
                           "SELECT SubbatchCode                                                                              " + Environment.NewLine +
                           "INTO #SB                                                                                         " + Environment.NewLine +
                           "FROM Tbl_ProductionSubbatchs                                                                     " + Environment.NewLine +
                           "WHERE BatchCode = @Batchcode;                                                                    " + Environment.NewLine +
                           "-----------------------------                                                                    " + Environment.NewLine +
                           "UPDATE Tbl_CustomerOrders SET ProductionCallDate = ''                                            " + Environment.NewLine +
                           "WHERE OrderIndex IN (SELECT OrderIndex FROM Tbl_Batch_Order WHERE BatchCode= @BatchCode) ;       " + Environment.NewLine +
                           "-----------------------------                                                                    " + Environment.NewLine +
                           "DELETE FROM Tbl_Batch_Order                WHERE BatchCode= @BatchCode ;                         " + Environment.NewLine +
                           "DELETE FROM Tbl_ProductionBatchsDetail     WHERE BatchCode= @BatchCode ;                         " + Environment.NewLine +
                           "DELETE FROM Tbl_SubbatchPlanningAlerts     WHERE SubbatchCode IN (SELECT SubbatchCode FROM #SB) ;" + Environment.NewLine +
                           "DELETE FROM Tbl_ProductionSubbatchsDetail  WHERE SubbatchCode IN (SELECT SubbatchCode FROM #SB) ;" + Environment.NewLine +
                           "-----------------------------                                                                    " + Environment.NewLine +
                           "DELETE Tbl_OperatorTask                                                                          " + Environment.NewLine + 
                           "FROM Tbl_OperatorTask AS T                                                                       " + Environment.NewLine + 
                           "     INNER JOIN Tbl_Planning AS P ON P.PlanningCode =  T.PlanningCode                            " + Environment.NewLine +
                           "     INNER JOIN #SB AS SB ON SB.SubbatchCode = P.SubbatchCode ;                                  " + Environment.NewLine +
                           "DELETE FROM Tbl_Planning WHERE SubbatchCode IN (SELECT SubbatchCode FROM #SB) ;                  "  +Environment.NewLine + 
                           "DELETE FROM Tbl_ProductionSubbatchs WHERE BatchCode = @Batchcode ;                               ";
                using (SqlCommand command = new SqlCommand(DeleteSQL, trnDelete.Connection))
                {
                    command.Parameters.AddWithValue("@BatchCode", BatchCode);
                    command.Transaction = trnDelete;
                    command.ExecuteNonQuery();
                    
                }

                
                // حذف ساب بچ های بچ جاری
                DeleteOldSubbatchs(trnDelete);
                string BatchYear = Conversions.ToString(mCurrentRow["DefineYear"]);

                // حذف بج جاری
                mCurrentRow.Delete();

               

                dvReArangeBatchs.RowFilter = "DefineYear=" + BatchYear;
                if (dvReArangeBatchs.Count > 0)
                {
                    var loopTo = (short)dvReArangeBatchs.Count;
                    for (I = 1; I <= loopTo; I++)
                    {
                        if ((short)dvReArangeBatchs[I - 1]["SequenceNo"] != I)
                        {
                            dvReArangeBatchs[I - 1]["SequenceNo"] = I;
                        }
                    }
                }

                SaveChanges();
                trnDelete.Commit();
                DialogResult = DialogResult.OK;
                Close();



            }
            catch (InvalidConstraintException ObjCnstEx)
                {
                    dsProductionPlanning.RejectChanges();
                    trnDelete.Rollback();
                    MessageBox.Show("اشکال در حذف رکورد، برای حذف رکورد باید رکورد(های) مرتبط با آن حذف شوند", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    Logger.SaveError("", ObjCnstEx.Message);
                }
                catch (Exception objEx)
                {
                    dsProductionPlanning.RejectChanges();
                    trnDelete.Rollback();
                    MessageBox.Show("حذف ساب بچ با اشکال مواجه شد. ابتدا اطلاعات تولید مربوطه باید حذف شوند.", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    Logger.SaveError("frmProductionBatch:cmdDelete_Click", objEx.Message);
                }
                finally
                {
                    dvReArangeBatchs.RowFilter = Constants.vbNullString;
                    if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                        Module1.cnProductionPlanning.Close();
                }
            
        }

        private string GetDeleteMessage_ProductionBatch(string BatchCode)
        {
            var msg = "";
            var CheckSQL = @"SELECT
                                 HasOrder    = CASE WHEN EXISTS(SELECT 1 From Tbl_Batch_Order Where BatchCode = @Batchcode) 
                                                    THEN 1
                                                    ELSE 0
                                               END,
                                 HasPlanning = CASE WHEN EXISTS(SELECT 1
                                                                From Tbl_Planning AS P
                                                                     INNER JOIN Tbl_ProductionSubbatchs AS SB ON SB.SubbatchCode = P.SubbatchCode
                                                                Where SB.BatchCode = @Batchcode)
                                                    THEN 1
                                                    ELSE 0
                                               END";
            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                Module1.cnProductionPlanning.Open();
            
            var command = new SqlCommand(CheckSQL, Module1.cnProductionPlanning);
            command.Parameters.AddWithValue("@BatchCode", BatchCode);
       
            var dr = command.ExecuteReader();
            
            if (dr.Read())
            {
                if (int.Parse(dr["HasOrder"].ToString()) == 1)
                    msg = "سفارش تولید به این بچ مرتبط می باشد.";

                if (int.Parse(dr["HasPlanning"].ToString()) == 1)
                    msg = msg + "اطلاعات برنامه ریزی برای این بچ وجود دارد.";

            }
                
            dr.Close();
           
            if(msg != "")
            {
                msg = msg  +Environment.NewLine + Environment.NewLine + "جهت تایید حذف، لطفا کد بچ را وارد کنید ؟"; 
            }

            return msg;
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!FormValidation())
            {
                return;
            }

            if (ListForm.FormMode == (int)Module1.FormModeEnum.EDIT_MODE && mEditMode == 2)
            {
                // کنترل اینکه تعداد ساب بچها از تعداد
                // ساب بچهایی که وارد تولید شده اند کوچکتر نباشد
                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(Operators.SubtractObject(mCurrentRow["SubbatchQuantity"], NotEntered_InProduction_SubbatchQuantity), txtSubbatchQuantity.Value, false)))
                {
                    MessageBox.Show("تعداد ساب بج ها از ساب بچ هایی که وارد تولید شده اند کوچکتر می باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return;
                }

                // کنترل اینکه تعداد تولید بچ کوچکتر از تعداد تولید ساب بچهایی
                // که وارد تولید شده و یا برنامه ریزی شده اند کوچکتر نباشد
                using (var cn = new SqlConnection(Module1.PlanningCnnStr))
                {
                    try
                    {
                        cn.Open();
                        var cm = new SqlCommand("Select ProductionQuantityinSubbatch From Tbl_ProductionSubbatchs Where BatchCode = '" + mCurrentRow["BatchCode"].ToString() + "' And PlanningStartDate > 0", cn);
                        var drEditRight = cm.ExecuteReader();
                        long SumProductionQuantity = 0L;
                        while (drEditRight.Read())
                            SumProductionQuantity += Conversions.ToLong(drEditRight["ProductionQuantityinSubbatch"]);
                        drEditRight.Close();
                        if (SumProductionQuantity > txtProductionQuantity.Value)
                        {
                            MessageBox.Show("تعداد تولید بج از میزان تولید ساب بچ هایی که وارد تولید شده یا برنامه ریزی شده اند کوچکتر می باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.SaveError(Name + ".cmdSave_Click", ex.Message);
                        MessageBox.Show("کنترل تغییرات بچ با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        return;
                    }
                    finally
                    {
                        if (cn.State == ConnectionState.Open)
                            cn.Close();
                    }
                }
            }

            int SeqNo;
            dsProductionPlanning.Tables["Tbl_ProductionBatchs"].DefaultView.RowFilter = "DefineYear=" + txtDefineDate.Text.Substring(0, 4);
            if (dsProductionPlanning.Tables["Tbl_ProductionBatchs"].DefaultView.Count > 0)
            {
                SeqNo = Conversions.ToInteger(dsProductionPlanning.Tables["Tbl_ProductionBatchs"].DefaultView[dsProductionPlanning.Tables["Tbl_ProductionBatchs"].DefaultView.Count - 1]["SequenceNo"]);
                SeqNo += 1;
            }
            else
            {
                SeqNo = 1;
            }

            dsProductionPlanning.Tables["Tbl_ProductionBatchs"].DefaultView.RowFilter = Constants.vbNullString;
            if (!txtProductionQuantity.ReadOnly)
            {
                if (txtProductionQuantity.Value < GetSelectedOrdersQuantity())
                {
                    if (MessageBox.Show("تعداد بچ کمتر از مجموع سفارشات انتخاب شده می باشد. آیا برای ثبت بچ مطمئن هستید", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading) == DialogResult.No)
                    {
                        return;
                    }
                }
            }

            switch (ListForm.FormMode)
            {
                case (int)Module1.FormModeEnum.INSERT_MODE:
                    {
                        DataRow drInsert;
                        SqlTransaction trnInsert = null;
                        try
                        {
                            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                                Module1.cnProductionPlanning.Open();
                            trnInsert = Module1.cnProductionPlanning.BeginTransaction();
                            daBatch.InsertCommand.Transaction = trnInsert;

                            // اضافه کردن رکورد جدید به جدول در دیتاست
                            drInsert = dsProductionPlanning.Tables["Tbl_ProductionBatchs"].NewRow();
                            drInsert["BatchCode"] = txtCode.Text;
                            drInsert["DefineYear"] = txtDefineDate.Text.Substring(0, 4);
                            drInsert["SequenceNo"] = SeqNo;
                            drInsert["SaleContractNo"] = txtContractNo.Text;
                            drInsert["DefineDate"] = Strings.Replace(txtDefineDate.Text, "/", "");
                            drInsert["ProductCode"] = cbProduct_LookUp.CB_SelectedValue;
                            drInsert["ProductName"] = cbProduct_LookUp.Text;

                            drInsert["Productionquantity"] = txtProductionQuantity.Value;
                            // drInsert["FirstDelivaryDate"] = Strings.Replace(txtDeliveryDate.Text, "/", "");
                            drInsert["FirstDelivaryDate"] = 0;
                            drInsert["ProductTreeCode"] = cbProductTree.SelectedValue;
                            drInsert["PlaningStartDate"] = 0;
                            drInsert["RealStartDate"] = 0;
                            drInsert["ProductionProgressMeasure"] = 0;
                            drInsert["FinishedDate"] = 0;
                            drInsert["RealProductionQuantity"] = 0;
                            drInsert["SubbatchQuantity"] = txtSubbatchQuantity.Value;
                            drInsert["BatchStatus"] = "برنامه ریزی نشده";
                            dsProductionPlanning.Tables["Tbl_ProductionBatchs"].Rows.Add(drInsert);
                            daBatch.Update(dsProductionPlanning, "Tbl_ProductionBatchs");
                            for (short I = 0, loopTo = (short)(dgDetails.Rows.Count - 1); I <= loopTo; I++)
                                CreateDetailStockRecord(I, trnInsert);

                            // ایجاد ساب بچ های جدید برای بچ جاری
                            AddNewSubbatchs(trnInsert);
                            long StockRemainder = Conversions.ToLong(dgDetails.Rows[0].Cells[3].Value);
                            long SubbatchQuantity = 0L;
                            arPreSubbatchRemainder = new long[dgDetails.RowCount];
                            for (short I = 0, loopTo1 = (short)Math.Round(txtSubbatchQuantity.Value - 1m); I <= loopTo1; I++)
                            {
                                if (I == 0)
                                {
                                    StockType = 1;
                                }
                                else
                                {
                                    StockType = 2;
                                }

                                if (StockRemainder > 0L)
                                {
                                    var cm = new SqlCommand("Select IsNull(ProductionQuantityinSubbatch,0) From Tbl_ProductionSubbatchs Where BatchCode = '" + txtCode.Text + "' And SubbatchNo = " + (I + 1).ToString(), trnInsert.Connection);
                                    cm.Transaction = trnInsert;
                                    SubbatchQuantity = Conversions.ToLong(cm.ExecuteScalar());
                                    if (StockRemainder - SubbatchQuantity < 0L)
                                    {
                                        StockRemainder = 0L;
                                    }
                                    else
                                    {
                                        StockRemainder = StockRemainder - SubbatchQuantity;
                                    }
                                }

                                CreateSubbatchDetailStockRecord(I, trnInsert);
                            }

                            SaveChanges();

                            // ثبت سفارشات انتخاب شده
                            for (int I = 0, loopTo2 = dgOrders.Rows.Count - 1; I <= loopTo2; I++)
                            {
                                if (Conversions.ToBoolean(dgOrders.Rows[I].Cells["colorderSelect"].Value))
                                {
                                    {
                                        var withBlock = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Insert Into Tbl_Batch_Order(BatchCode,OrderIndex) Values('" + txtCode.Text + "',", dgOrders.Rows[I].Cells["colOrderIndex"].Value), ")")), Module1.cnProductionPlanning);
                                        withBlock.Transaction = trnInsert;
                                        withBlock.ExecuteNonQuery();
                                        update_ProductionCallDate(trnInsert, Convert.ToInt32(dgOrders.Rows[I].Cells["colOrderIndex"].Value), 1);
                                    }
                                }
                            }

                            trnInsert.Commit();
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            dsProductionPlanning.RejectChanges();
                            trnInsert.Rollback();
                            Logger.SaveError(Name + ".cmdSave_Click", objEx.Message);
                            MessageBox.Show("ثبت مشخصات بچ جدید با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        }
                        finally
                        {
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                        }

                        break;
                    }

                case (int)Module1.FormModeEnum.EDIT_MODE:
                    {
                        SqlTransaction trnUpdate = null;
                        string mAlarmDesc = Constants.vbNullString;
                        try
                        {
                            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                                Module1.cnProductionPlanning.Open();
                            trnUpdate = Module1.cnProductionPlanning.BeginTransaction();
                            daBatch.UpdateCommand.Transaction = trnUpdate;
                            if (mEditMode == 2 && Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(mCurrentRow["SubbatchQuantity"], txtSubbatchQuantity.Value, false)))
                            {
                                mAlarmDesc = "تعداد ساب بچ های بچ تولید تغییر کرده است";
                            }

                            DeleteBatchDetails(trnUpdate);
                            DeleteSubbatchDetails(trnUpdate);
                            update_ProductionCallDate(trnUpdate, Convert.ToInt32(mCurrentRow["BatchCode"].ToString()), 2);

                            // حذف سفارشات انتخاب شدۀ قبلی
                            {
                                var withBlock1 = new SqlCommand("Delete From Tbl_Batch_Order Where BatchCode = '" + mCurrentRow["BatchCode"].ToString() + "'", Module1.cnProductionPlanning);
                                withBlock1.Transaction = trnUpdate;
                                withBlock1.ExecuteNonQuery();

                            }

                            short Subduct = Conversions.ToShort(Operators.SubtractObject(mCurrentRow["SubbatchQuantity"], txtSubbatchQuantity.Value));
                            mCurrentRow.BeginEdit();
                            if (mEditMode == 1)
                            {
                                // حذف ساب بچ های بچ جاری
                                DeleteOldSubbatchs(trnUpdate);
                                mCurrentRow["BatchCode"] = txtCode.Text;
                                mCurrentRow["ProductTreeCode"] = cbProductTree.SelectedValue;
                                //mCurrentRow["FirstDelivaryDate"] = Strings.Replace(txtDeliveryDate.Text, "/", "");
                                mCurrentRow["FirstDelivaryDate"] = "";

                            }
                            else if (mEditMode == 2)
                            {
                                if (Subduct > 0) // اگر تعداد ساب بچ ها کم شده باشد
                                {
                                    DeleteOldSubbatchs(trnUpdate);
                                }
                            }

                            mCurrentRow["DefineYear"] = txtDefineDate.Text.Substring(0, 4);
                            mCurrentRow["SequenceNo"] = Interaction.IIf(Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(mCurrentRow["DefineYear"], txtDefineDate.Text.Substring(0, 4), false)), mCurrentRow["SequenceNo"], SeqNo);
                            mCurrentRow["SaleContractNo"] = txtContractNo.Text;
                            mCurrentRow["DefineDate"] = Strings.Replace(txtDefineDate.Text, "/", "");
                            mCurrentRow["ProductCode"] = cbProduct_LookUp.CB_SelectedValue;
                            mCurrentRow["ProductName"] = cbProduct_LookUp.Text;

                            mCurrentRow["Productionquantity"] = txtProductionQuantity.Value;
                            mCurrentRow["PlaningStartDate"] = Interaction.IIf(mEditMode == 1, 0, mCurrentRow["PlaningStartDate"]);
                            mCurrentRow["RealStartDate"] = Interaction.IIf(mEditMode == 1, 0, mCurrentRow["RealStartDate"]);
                            mCurrentRow["ProductionProgressMeasure"] = Interaction.IIf(mEditMode == 1, 0, mCurrentRow["ProductionProgressMeasure"]);
                            mCurrentRow["SubbatchQuantity"] = txtSubbatchQuantity.Value;
                            mCurrentRow.EndEdit();
                            daBatch.Update(dsProductionPlanning, "Tbl_ProductionBatchs");
                            if (mEditMode == 1)
                            {
                                // ایجاد ساب بچ های جدید برای بچ جاری
                                AddNewSubbatchs(trnUpdate);
                            }
                            else if (mEditMode == 2)
                            {
                                if (Subduct < 0) // اگر به تعداد ساب بچ ها اضافه شده باشد
                                {
                                    AddNewSubbatchs(trnUpdate);
                                }
                            }

                            for (short I = 0, loopTo3 = (short)(dgDetails.Rows.Count - 1); I <= loopTo3; I++)
                                CreateDetailStockRecord(I, trnUpdate);
                            long StockRemainder = Conversions.ToLong(dgDetails.Rows[0].Cells[3].Value);
                            long SubbatchQuantity = 0L;
                            arPreSubbatchRemainder = new long[dgDetails.RowCount];
                            for (short I = 0, loopTo4 = (short)Math.Round(txtSubbatchQuantity.Value - 1m); I <= loopTo4; I++)
                            {
                                if (I == 0)
                                {
                                    StockType = 1;
                                }
                                else
                                {
                                    StockType = 2;
                                }

                                if (StockRemainder > 0L)
                                {
                                    var cm = new SqlCommand("Select IsNull(ProductionQuantityinSubbatch,0) From Tbl_ProductionSubbatchs Where BatchCode = '" + txtCode.Text + "' And SubbatchNo = " + (I + 1).ToString(), trnUpdate.Connection);
                                    cm.Transaction = trnUpdate;
                                    SubbatchQuantity = Conversions.ToLong(cm.ExecuteScalar());
                                    if (StockRemainder - SubbatchQuantity < 0L)
                                    {
                                        StockRemainder = 0L;
                                    }
                                    else
                                    {
                                        StockRemainder = StockRemainder - SubbatchQuantity;
                                    }
                                }

                                CreateSubbatchDetailStockRecord(I, trnUpdate);
                            }

                            SaveChanges();
                            for (int I = 0, loopTo5 = dgOrders.Rows.Count - 1; I <= loopTo5; I++)
                            {
                                if (Conversions.ToBoolean(dgOrders.Rows[I].Cells["colorderSelect"].Value))
                                {
                                    {
                                        var withBlock2 = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Insert Into Tbl_Batch_Order(BatchCode,OrderIndex) Values('" + txtCode.Text + "',", dgOrders.Rows[I].Cells["colOrderIndex"].Value), ")")), trnUpdate.Connection);
                                        withBlock2.Transaction = trnUpdate;
                                        withBlock2.ExecuteNonQuery();
                                        update_ProductionCallDate(trnUpdate, Convert.ToInt32(dgOrders.Rows[I].Cells["colOrderIndex"].Value), 1);
                                    }
                                }
                            }

                            trnUpdate.Commit();
                            if (!string.IsNullOrEmpty(mAlarmDesc))
                            {
                                Module1.Check_Subbatch_HasPlanningAlarm(Conversions.ToString(mCurrentRow["ProductTreeCode"]), txtCode.Text, "", "2", mAlarmDesc);
                            }

                            DialogResult = DialogResult.OK;
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            mCurrentRow.CancelEdit();
                            trnUpdate.Rollback();
                            Logger.SaveError(Name + ".cmdSave_Click", objEx.Message);
                            MessageBox.Show("ثبت تغییرات با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        }
                        finally
                        {
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                        }

                        break;
                    }
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }
          
        private void cbProductTree_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbProductTree.SelectedIndex > -1 && cbProductTree.SelectedValue is object && cbProductTree.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                string BatchCode = ""; // Constants.vbNullString;
                //string mDetailCode = Constants.vbNullString;
                if (ListForm.FormMode == (int)Module1.FormModeEnum.INSERT_MODE)
                {
                    BatchCode = txtCode.Text;
                }
                else
                {
                    BatchCode = Conversions.ToString(mCurrentRow["BatchCode"]);
                }

                dgDetails.Rows.Clear();
                using (var cn = new SqlConnection(Module1.PlanningCnnStr))
                {
                    try
                    {
                        cn.Open();
                        var cm = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Select DetailCode From Tbl_ProductionBatchsDetail Where BatchCode='" + BatchCode + "' And DetailCode IN (Select DetailCode From Tbl_ProductTreeDetails Where TreeCode = ", cbProductTree.SelectedValue), " And LevelNo = 0)")), cn);
                        var dr = cm.ExecuteReader();
                        string mRootDetailCode = Constants.vbNullString;
                        if (dr.Read())
                        {
                            mRootDetailCode = dr["DetailCode"].ToString();
                            dr.Close();
                            CreateBatchStockDetailGridRow(Conversions.ToString(cbProductTree.SelectedValue), BatchCode, mRootDetailCode, 2);
                        }
                        else
                        {
                            dr.Close();
                            cm.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Select DetailCode From Tbl_ProductTreeDetails Where TreeCode = ", cbProductTree.SelectedValue), " And LevelNo = 0"));
                            dr = cm.ExecuteReader();
                            if (dr.Read())
                            {
                                mRootDetailCode = dr["DetailCode"].ToString();
                                dr.Close();
                                CreateBatchStockDetailGridRow(Conversions.ToString(cbProductTree.SelectedValue), BatchCode, mRootDetailCode, 1);
                            }
                            else
                            {
                                dr.Close();
                                dgDetails.Rows.Clear();
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
            else
            {
                dgDetails.Rows.Clear();
                dgOrders.Rows.Clear();
            }
        }

        private void dgOrders_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.RowIndex != -1 && e.ColumnIndex == 1)
            {
                if (Conversions.ToBoolean(dgOrders.Rows[e.RowIndex].Cells["colorderSelect"].Value))
                {
                    dgOrders.Rows[e.RowIndex].Cells["colorderSelect"].Value = false;
                }
                else
                {
                    dgOrders.Rows[e.RowIndex].Cells["colorderSelect"].Value = true;
                }

                if (txtProductionQuantity.ReadOnly || ListForm.FormMode == (int)Module1.FormModeEnum.INSERT_MODE)
                {
                    txtProductionQuantity.Value = GetSelectedOrdersQuantity();
                }
            }
        }

        private void dgOrders_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && dgOrders.CurrentCell.ColumnIndex == 1)
            {
                if (Conversions.ToBoolean(dgOrders.CurrentRow.Cells["colorderSelect"].Value))
                {
                    dgOrders.CurrentRow.Cells["colorderSelect"].Value = false;
                }
                else
                {
                    dgOrders.CurrentRow.Cells["colorderSelect"].Value = true;
                }

                if (txtProductionQuantity.ReadOnly)
                {
                    txtProductionQuantity.Value = GetSelectedOrdersQuantity();
                }
                else if (ListForm.FormMode == (int)Module1.FormModeEnum.INSERT_MODE)
                {
                    txtProductionQuantity.Value = GetSelectedOrdersQuantity();
                }
            }
        }

        private void FormLoad()
        {
            try
            {
                switch (ListForm.FormMode)
                {
                    case (int)Module1.FormModeEnum.EDIT_MODE:
                    case (int)Module1.FormModeEnum.DELETE_MODE:
                        {
                            mCurrentRow = ListForm.GetRow();
                            break;
                        }
                }
                CreateDataAdapterCommands();
                {
                   
                    cbProduct_LookUp.CB_SelectedIndex = -1;
                    cbProduct_LookUp.CB_SelectedValue = "";
                    cbProduct_LookUp.CB_DataSource = dsProductionPlanning.Tables["Tbl_Products_Lookup"].DefaultView;
                    cbProduct_LookUp.CB_DisplayMember = "ProductCode";
                    cbProduct_LookUp.CB_ValueMember = "ProductName";
                    
                }

                switch (ListForm.FormMode)
                {
                    case (int)Module1.FormModeEnum.INSERT_MODE:
                        {
                            // lblMinDelivary.Visible = true;
                            // txtTransferMinQuantity.Visible = true;
                            if (!mSelectedOrdersProductCode.Equals("-1"))
                            {
                               cbProduct_LookUp.CB_SelectedValue = mSelectedOrdersProductCode;
                            }
                            
                            txtCode.Focus();
                            break;
                        }

                    case (int)Module1.FormModeEnum.EDIT_MODE:
                    case (int)Module1.FormModeEnum.DELETE_MODE:
                        {
                            // پر کردن کنترل فرم با مقدار رکورد جاری
                            FillForm();
                            FillcallerDate();
                            switch (ListForm.FormMode)
                            {
                                case (int)Module1.FormModeEnum.EDIT_MODE:
                                    {
                                        if (mEditMode == 2)
                                        {
                                            txtCode.Enabled = false;
                                            cbProductTree.Enabled = false;
                                            cbProduct_LookUp.Enabled = false;


                                            txtDeliveryDate.Enabled = false;
                                            txtContractNo.Focus();
                                        }
                                        else
                                        {
                                            txtCode.Focus();
                                        }

                                        break;
                                    }

                                case (int)Module1.FormModeEnum.DELETE_MODE:
                                    {
                                        cmdDelete.Focus();
                                        break;
                                    }
                            }

                            break;
                        }
                }
            }
            catch (Exception objEx)
            {
                Logger.SaveError(Name + ".FormLoad", objEx.Message);
                MessageBox.Show("فراخوانی فرم با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }

        private void FillForm()
        {
            txtCode.Text = Conversions.ToString(mCurrentRow["BatchCode"]);
            cbProduct_LookUp.CB_SelectedValue = mCurrentRow["ProductCode"].ToString();
            txtContractNo.Text = Conversions.ToString(mCurrentRow["SaleContractNo"]);
            txtDefineDate.Text = Conversions.ToString(mCurrentRow["DefineDate"]);
            //txtDeliveryDate.Text = Conversions.ToString(mCurrentRow["FirstDelivaryDate"]);
            txtDeliveryDate.Text = "";
            cbProductTree.SelectedValue = mCurrentRow["ProductTreeCode"];
            txtSubbatchQuantity.Value = Conversions.ToDecimal(mCurrentRow["SubbatchQuantity"]);
            LoadOrderList();
            txtProductionQuantity.Value = Conversions.ToDecimal(mCurrentRow["ProductionQuantity"]);
        }

        private void CreateDetailStockRecord(short RowNo, SqlTransaction mTrn)
        {
            var cm = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Select IsNull(RequirementQuantity,0) From Tbl_ProductionBatchsDetail Where BatchCode='" + txtCode.Text + "' And DetailCode='", dgDetails.Rows[RowNo].Cells[5].Value), "'")), mTrn.Connection);
            long ParentQuantity;
            cm.Transaction = mTrn;
            if (RowNo == 0)
            {
                ParentQuantity = Conversions.ToLong(Operators.SubtractObject(txtProductionQuantity.Value, dgDetails.Rows[0].Cells[3].Value));
            }
            else
            {
                ParentQuantity = Conversions.ToLong(cm.ExecuteScalar());
            }

            cm.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Insert Into Tbl_ProductionBatchsDetail(BatchCode,DetailCode,Stock,RequirementQuantity,OverStock,ProductionQuantity,ProductionStock,CalcedOverStock,GarbageQuantity) " + "Values('" + txtCode.Text + "','", dgDetails.Rows[RowNo].Cells[0].Value), "',"), dgDetails.Rows[RowNo].Cells[3].Value), ","), GetDetailRequirementQty(ParentQuantity, RowNo)), ",0,0,0,0,0)"));

            cm.ExecuteNonQuery();
        }

        private long GetDetailRequirementQty(long ParentQty, short RowNo)
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

        private string GetSubbatchCode(SqlTransaction mTrn, short SubbatchNo)
        {
            string mSubbatchCode = "-1";
            var cm = new SqlCommand("Select SubbatchCode From Tbl_ProductionSubbatchs Where BatchCode = '" + txtCode.Text + "' And SubbatchNo = " + SubbatchNo, mTrn.Connection);
            cm.Transaction = mTrn;
            var dr = cm.ExecuteReader();
            if (dr.Read())
            {
                mSubbatchCode = dr["SubbatchCode"].ToString();
            }

            dr.Close();
            return mSubbatchCode;
        }

        private int GetDetailParentQuantity(SqlTransaction mTrn, string mTreeCode, string mDetailCode)
        {
            int mParentQuantity = 0;
            var cm = new SqlCommand("Select IsNull(ParentQuantity,0) From Tbl_ProductTreeDetails Where TreeCode=" + mTreeCode + " And DetailCode='" + mDetailCode + "'", mTrn.Connection);
            cm.Transaction = mTrn;
            mParentQuantity = Conversions.ToInteger(cm.ExecuteScalar());
            return mParentQuantity;
        }

        private long GetSubbatchProductionQuantity(SqlTransaction mTrn, string SubbatchCode)
        {
            int mSubbatchQty = 0;
            var cm = new SqlCommand("Select IsNull(ProductionQuantityinSubbatch,0) From Tbl_ProductionSubbatchs Where SubbatchCode = '" + SubbatchCode + "'", mTrn.Connection);
            cm.Transaction = mTrn;
            mSubbatchQty = (int)Conversions.ToLong(cm.ExecuteScalar());
            return mSubbatchQty;
        }

        private void CreateSubbatchDetailStockRecord(short SubbatchIndex, SqlTransaction mTrn)
        {
            string mSubbatchCode = GetSubbatchCode(mTrn, (short)(SubbatchIndex + 1));
            int mParentQty = GetDetailParentQuantity(mTrn, Conversions.ToString(cbProductTree.SelectedValue), Conversions.ToString(dgDetails.Rows[0].Cells[0].Value));
            long ParentQuantity = GetSubbatchProductionQuantity(mTrn, mSubbatchCode) * mParentQty;
            for (short I = 0, loopTo = (short)(dgDetails.Rows.Count - 1); I <= loopTo; I++)
            {
                long mRequirementQty = GetSubbatchDetailRequirementQty(ParentQuantity, I);
                long mStock = Conversions.ToLong(Interaction.IIf(mRequirementQty == ParentQuantity, 0, Interaction.IIf(arPreSubbatchRemainder[I] > 0L, arPreSubbatchRemainder[I], dgDetails.Rows[I].Cells[3].Value)));
                var cm = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Insert Into Tbl_ProductionSubbatchsDetail(SubbatchCode,DetailCode,Stock,RequirementQuantity) " + "Values('" + mSubbatchCode + "','", dgDetails.Rows[I].Cells[0].Value), "',"), mStock), ","), mRequirementQty), ")")), mTrn.Connection);
                cm.Transaction = mTrn;
                cm.ExecuteNonQuery();
                if (I < dgDetails.Rows.Count - 1 && Operators.ConditionalCompareObjectNotEqual(dgDetails.Rows[I].Cells[5].Value, dgDetails.Rows[I + 1].Cells[5].Value, false))
                {
                    cm.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Select IsNull(RequirementQuantity,0) From Tbl_ProductionSubbatchsDetail Where SubbatchCode='" + mSubbatchCode + "' And DetailCode='", dgDetails.Rows[I + 1].Cells[5].Value), "'"));
                    ParentQuantity = Conversions.ToLong(cm.ExecuteScalar());
                }
            }
        }

        private long GetSubbatchDetailRequirementQty(long ParentQty, short RowNo)
        {
            long RequirementQty;
            long Stock = Conversions.ToLong(Interaction.IIf(StockType == 1, dgDetails.Rows[RowNo].Cells[3].Value, arPreSubbatchRemainder[RowNo]));
            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreaterEqual(Operators.SubtractObject(Operators.MultiplyObject(ParentQty, dgDetails.Rows[RowNo].Cells[2].Value), Stock), 0, false)))
            {
                RequirementQty = Conversions.ToLong(Operators.SubtractObject(Operators.MultiplyObject(ParentQty, dgDetails.Rows[RowNo].Cells[2].Value), Stock));
                if (StockType == 2)
                {
                    arPreSubbatchRemainder[RowNo] = 0L;
                }
            }
            else
            {
                RequirementQty = 0L;
                arPreSubbatchRemainder[RowNo] = Conversions.ToLong(Operators.SubtractObject(Stock, Operators.MultiplyObject(ParentQty, dgDetails.Rows[RowNo].Cells[2].Value)));
            }

            return RequirementQty;
        }

        private void CreateBatchStockDetailGridRow(string TreeCode, string BatchCode, string DetailCode, short SourceType)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cm = new SqlCommand("", cn);
                SqlDataReader dr = null;
                switch (SourceType)
                {
                    case 1: // سابقه ای در جدول موجودی وجود ندارد
                        {
                            cm.CommandText = "Select * From Tbl_ProductTreeDetails Where TreeCode=" + TreeCode + " And DetailCode='" + DetailCode + "'";
                            dr = cm.ExecuteReader();
                            if (dr.Read())
                            {
                                dgDetails.Rows.Add(dr["DetailCode"], dr["DetailName"], dr["ParentQuantity"], 0, 0, dr["ParentDetailCode"]);
                            }

                            dr.Close();
                            cm.CommandText = "Select DetailCode From Tbl_ProductTreeDetails Where TreeCode=" + TreeCode + " And ParentDetailCode='" + DetailCode + "'";
                            dr = cm.ExecuteReader();
                            while (dr.Read())
                                CreateBatchStockDetailGridRow(TreeCode, BatchCode, Conversions.ToString(dr["DetailCode"]), SourceType);
                            dr.Close();
                            break;
                        }

                    case 2: // قبلا در جدول موجودی ثبت شده است
                        {
                            cm.CommandText = "Select A.DetailCode, B.DetailName, B.ParentQuantity, A.Stock, A.RequirementQuantity, B.ParentDetailCode " + "From   dbo.Tbl_ProductionBatchsDetail A Inner Join dbo.Tbl_ProductTreeDetails B ON A.DetailCode = B.DetailCode " + "Where  A.BatchCode = '" + BatchCode + "' And B.TreeCode = " + TreeCode + " And A.DetailCode = '" + DetailCode + "'";

                            dr = cm.ExecuteReader();
                            if (dr.Read())
                            {
                                dgDetails.Rows.Add(dr["DetailCode"], dr["DetailName"], dr["ParentQuantity"], dr["Stock"], dr["RequirementQuantity"], dr["ParentDetailCode"]);
                            }

                            dr.Close();
                            cm.CommandText = "Select A.DetailCode " + "From   dbo.Tbl_ProductionBatchsDetail A Inner Join dbo.Tbl_ProductTreeDetails B ON A.DetailCode = B.DetailCode " + "Where  A.BatchCode = '" + BatchCode + "' And B.TreeCode = " + TreeCode + " And B.ParentDetailCode = '" + DetailCode + "'";

                            dr = cm.ExecuteReader();
                            while (dr.Read())
                                CreateBatchStockDetailGridRow(TreeCode, BatchCode, Conversions.ToString(dr["DetailCode"]), SourceType);
                            dr.Close();
                            break;
                        }
                }

                cn.Close();
            }
        }

        private void DeleteOldSubbatchs(SqlTransaction mTrn)
        {
            var cm = new SqlCommand("", mTrn.Connection);
            cm.Transaction = mTrn;
            switch (ListForm.FormMode)
            {
                case (int)Module1.FormModeEnum.EDIT_MODE:
                    {
                        switch (mEditMode)
                        {
                            case 1:
                                {
                                    cm.CommandText = "Delete From Tbl_ProductionSubbatchs Where BatchCode = '" + mCurrentRow["BatchCode"].ToString() + "'";
                                    cm.ExecuteNonQuery();
                                    break;
                                }

                            case 2:
                                {
                                    var loopTo = (short)Math.Round(txtSubbatchQuantity.Value);
                                    for (I = Conversions.ToShort(mCurrentRow["SubbatchQuantity"]); I >= loopTo; I += -1)
                                    {
                                        // حذف مشخصات برنامه ریزی ساب بچ
                                        cm.CommandText = "Delete From Tbl_Planning Where SubbatchCode IN (Select SubbatchCode From Tbl_ProductionSubbatchs Where BatchCode = '" + mCurrentRow["BatchCode"].ToString() + "' And SubbatchNo = " + I.ToString() + ")";
                                        cm.ExecuteNonQuery();
                                        cm.CommandText = "Delete From Tbl_ProductionSubbatchsDetail Where SubbatchCode IN (Select SubbatchCode From Tbl_ProductionSubbatchs Where BatchCode = '" + mCurrentRow["BatchCode"].ToString() + "' And SubbatchNo = " + I.ToString() + ")";
                                        cm.ExecuteNonQuery();

                                        // حذف ساب بچ
                                        cm.CommandText = "Delete From Tbl_ProductionSubbatchs Where BatchCode = '" + mCurrentRow["BatchCode"].ToString() + "' And SubbatchNo = " + I.ToString() + ")";
                                        cm.ExecuteNonQuery();
                                    }

                                    break;
                                }
                        }

                        break;
                    }
                    
                case (int)Module1.FormModeEnum.DELETE_MODE:
                    {
                        cm.CommandText = "Delete From Tbl_Planning Where SubbatchCode IN (Select SubbatchCode From Tbl_ProductionSubbatchs Where BatchCode = '" + mCurrentRow["BatchCode"].ToString() + "')";
                        cm.ExecuteNonQuery();
                        cm.CommandText = "Delete From Tbl_SubbatchPlanningAlerts  Where SubbatchCode IN (Select SubbatchCode From Tbl_ProductionSubbatchs Where BatchCode = '" + mCurrentRow["BatchCode"].ToString() + "')";
                        cm.ExecuteNonQuery();
                        cm.CommandText = "Delete From Tbl_ProductionSubbatchs Where BatchCode = '" + mCurrentRow["BatchCode"].ToString() + "'";
                        cm.ExecuteNonQuery();
                        break;
                    }
            }
        }
        private void DeleteBatchDetails(SqlTransaction mTrn)
        {
            var cm = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Delete From Tbl_ProductionBatchsDetail Where BatchCode='", mCurrentRow["BatchCode"]), "'")), mTrn.Connection);
            cm.Transaction = mTrn;
            cm.ExecuteNonQuery();
        }
        private void DeleteSubbatchDetails(SqlTransaction mTrn)
        {
            var cm = new SqlCommand("Delete From Tbl_ProductionSubbatchsDetail Where SubbatchCode IN (Select SubbatchCode From Tbl_ProductionSubbatchs Where BatchCode = '" + mCurrentRow["BatchCode"] + "')", mTrn.Connection);
            cm.Transaction = mTrn;
            cm.ExecuteNonQuery();
        }

        private void AddNewSubbatchs(SqlTransaction mTrn)
        {
            long Quantity = 0L;
            var cm = new SqlCommand("", mTrn.Connection);
            cm.Transaction = mTrn;
            if (ListForm.FormMode == (int)Module1.FormModeEnum.INSERT_MODE || Conversions.ToBoolean(Module1.FormModeEnum.EDIT_MODE) && mEditMode == 1)
            {
                var loopTo = (short)Math.Round(txtSubbatchQuantity.Value);
                for (I = 1; I <= loopTo; I++)
                {
                    int mProductionQty = 0;
                    if ((txtProductionQuantity.Value / txtSubbatchQuantity.Value).ToString().Contains("."))
                    {
                        if (I < txtSubbatchQuantity.Value)
                        {
                            mProductionQty = (int)Conversions.ToLong((txtProductionQuantity.Value / txtSubbatchQuantity.Value).ToString().Substring(0, Strings.InStr((txtProductionQuantity.Value / txtSubbatchQuantity.Value).ToString(), ".") - 1));
                            Quantity += mProductionQty;
                        }
                        else
                        {
                            mProductionQty = (int)Math.Round(txtProductionQuantity.Value - Quantity);
                        }
                    }
                    else
                    {
                        mProductionQty = (int)Math.Round(txtProductionQuantity.Value / txtSubbatchQuantity.Value);
                    }

                    //  cm.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Insert Into Tbl_ProductionSubbatchs(BatchCode,SubbatchNo,SubbatchCode,ProductionQuantityinSubbatch,PlanningStartDate,PlanningStartHour,PlanningEndDate,PlanningEndHour,SubbatchFirstDeliveryDate,ProductionPriority,TransferMinimumQuantity,StopPlanning) " + "Values('" + txtCode.Text + "'," + I.ToString() + ",'" + txtCode.Text, Interaction.IIf(I < 10, "0" + I.ToString(), I.ToString())), "',"), mProductionQty), ",0,0.0,0,0.0,'"), Interaction.IIf(I == 1, Strings.Replace(txtDeliveryDate.Text, "/", ""), "0")), "',0,"), txtTransferMinQuantity.Value), ",0)"));
                    cm.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Insert Into Tbl_ProductionSubbatchs(BatchCode,SubbatchNo,SubbatchCode,ProductionQuantityinSubbatch,PlanningStartDate,PlanningStartHour,PlanningEndDate,PlanningEndHour,SubbatchFirstDeliveryDate,ProductionPriority,TransferMinimumQuantity,StopPlanning) " + "Values('" + txtCode.Text + "'," + I.ToString() + ",'" + txtCode.Text, Interaction.IIf(I < 10, "0" + I.ToString(), I.ToString())), "',"), mProductionQty), ",0,0.0,0,0.0,'"), Interaction.IIf(I == 1, Strings.Replace("", "/", ""), "0")), "',0,"), txtTransferMinQuantity.Value), ",0)"));
                    cm.ExecuteNonQuery();
                }
            }
            else if (Conversions.ToBoolean(Module1.FormModeEnum.EDIT_MODE) && mEditMode == 2)
            {
                var loopTo1 = (short)Math.Round(txtSubbatchQuantity.Value);
                for (I = Conversions.ToShort(mCurrentRow["SubbatchQuantity"]); I <= loopTo1; I++)
                {
                    int mProductionQty = 0;
                    cm.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Insert Into Tbl_ProductionSubbatchs(BatchCode,SubbatchNo,SubbatchCode,ProductionQuantityinSubbatch,PlanningStartDate,PlanningStartHour,PlanningEndDate,PlanningEndHour,SubbatchFirstDeliveryDate,ProductionPriority,TransferMinimumQuantity,StopPlanning) " + "Values('" + txtCode.Text + "'," + I.ToString() + ",'" + txtCode.Text, Interaction.IIf(I < 10, "0" + I.ToString(), I.ToString())), "',"), mProductionQty), ",0,0.0,0,0.0,'0',0,0,0)"));
                    cm.ExecuteNonQuery();
                }
            }
        }

        private void SaveChanges()
        {
            var dsChanges = dsProductionPlanning.GetChanges();
            if (dsChanges is null || dsChanges.HasErrors)
            {
                dsProductionPlanning.RejectChanges();
            }
            else
            {
                daBatch.Update(dsChanges, "Tbl_ProductionBatchs");
                dsProductionPlanning.AcceptChanges();
            }

            dsChanges = null;
        }

        private void CreateDataAdapterCommands()
        {
            // --------------------- ایجاد دستورات برای جدول بج های تولید -----------------
            // ایجاد دستور اضافه کردن رکورد جدید در جدول
            daBatch.InsertCommand = new SqlCommand("Insert Into Tbl_ProductionBatchs(BatchCode,DefineYear,SequenceNo,SaleContractNo,DefineDate,Productionquantity,FirstDelivaryDate,ProductTreeCode,PlaningStartDate,RealStartDate,ProductionProgressMeasure,SubbatchQuantity,FinishedDate,RealProductionQuantity) " + "Values(@BatchCode,@DefineYear,@SequenceNo,@SaleContractNo,@DefineDate,@Productionquantity,@FirstDelivaryDate,@ProductTreeCode,@PlaningStartDate,@RealStartDate,@ProductionProgressMeasure,@SubbatchQuantity,@FinishedDate,@RealProductionQuantity)", Module1.cnProductionPlanning);
            {
                var withBlock = daBatch.InsertCommand;
                withBlock.Parameters.Add("@BatchCode", SqlDbType.VarChar, 20, "BatchCode");
                withBlock.Parameters.Add("@DefineYear", SqlDbType.SmallInt, default, "DefineYear");
                withBlock.Parameters.Add("@SequenceNo", SqlDbType.SmallInt, default, "SequenceNo");
                withBlock.Parameters.Add("@SaleContractNo", SqlDbType.VarChar, 50, "SaleContractNo");
                withBlock.Parameters.Add("@DefineDate", SqlDbType.Int, default, "DefineDate");
                withBlock.Parameters.Add("@Productionquantity", SqlDbType.Int, default, "Productionquantity");
                withBlock.Parameters.Add("@FirstDelivaryDate", SqlDbType.VarChar, 8, "FirstDelivaryDate");
                withBlock.Parameters.Add("@ProductTreeCode", SqlDbType.Int, default, "ProductTreeCode");
                withBlock.Parameters.Add("@PlaningStartDate", SqlDbType.VarChar, 8, "PlaningStartDate");
                withBlock.Parameters.Add("@RealStartDate", SqlDbType.VarChar, 8, "RealStartDate");
                withBlock.Parameters.Add("@ProductionProgressMeasure", SqlDbType.VarChar, 5, "ProductionProgressMeasure");
                withBlock.Parameters.Add("@SubbatchQuantity", SqlDbType.TinyInt, default, "SubbatchQuantity");
                withBlock.Parameters.Add("@FinishedDate", SqlDbType.VarChar, 8, "FinishedDate");
                withBlock.Parameters.Add("@RealProductionQuantity", SqlDbType.Int, default, "RealProductionQuantity");
            }
            // ایجاد دستور اصلاح رکورد جاری در جدول
            daBatch.UpdateCommand = new SqlCommand("Update Tbl_ProductionBatchs Set BatchCode=@CurrentBatchCode,DefineYear=@DefineYear,SequenceNo=@SequenceNo,SaleContractNo=@SaleContractNo," + "DefineDate=@DefineDate,Productionquantity=@Productionquantity,FirstDelivaryDate=@FirstDelivaryDate," + "ProductTreeCode=@ProductTreeCode,PlaningStartDate=@PlaningStartDate,RealStartDate=@RealStartDate," + "ProductionProgressMeasure=@ProductionProgressMeasure,SubbatchQuantity=@SubbatchQuantity Where BatchCode=@OldBatchCode", Module1.cnProductionPlanning);


            {
                var withBlock1 = daBatch.UpdateCommand;
                withBlock1.Parameters.Add("@CurrentBatchCode", SqlDbType.VarChar, 20, "BatchCode").SourceVersion = DataRowVersion.Current;
                withBlock1.Parameters.Add("@DefineYear", SqlDbType.SmallInt, default, "DefineYear");
                withBlock1.Parameters.Add("@SequenceNo", SqlDbType.SmallInt, default, "SequenceNo");
                withBlock1.Parameters.Add("@SaleContractNo", SqlDbType.VarChar, 50, "SaleContractNo");
                withBlock1.Parameters.Add("@DefineDate", SqlDbType.Int, default, "DefineDate");
                withBlock1.Parameters.Add("@Productionquantity", SqlDbType.Int, default, "Productionquantity");
                withBlock1.Parameters.Add("@FirstDelivaryDate", SqlDbType.VarChar, 8, "FirstDelivaryDate");
                withBlock1.Parameters.Add("@ProductTreeCode", SqlDbType.Int, default, "ProductTreeCode");
                withBlock1.Parameters.Add("@PlaningStartDate", SqlDbType.VarChar, 8, "PlaningStartDate");
                withBlock1.Parameters.Add("@RealStartDate", SqlDbType.VarChar, 8, "RealStartDate");
                withBlock1.Parameters.Add("@ProductionProgressMeasure", SqlDbType.VarChar, 5, "ProductionProgressMeasure");
                withBlock1.Parameters.Add("@SubbatchQuantity", SqlDbType.TinyInt, default, "SubbatchQuantity");
                withBlock1.Parameters.Add("@OldBatchCode", SqlDbType.VarChar, 20, "BatchCode").SourceVersion = DataRowVersion.Original;
            }

            // ایجاد دستور حذف رکورد جاری در جدول
            daBatch.DeleteCommand = new SqlCommand("Delete From Tbl_ProductionBatchs Where BatchCode=@BatchCode", Module1.cnProductionPlanning);
            {
                var withBlock2 = daBatch.DeleteCommand;
                withBlock2.Parameters.Add("@BatchCode", SqlDbType.VarChar, 20, "BatchCode").SourceVersion = DataRowVersion.Original;
            }
        }

        private bool FormValidation()
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                MessageBox.Show("کد بچ را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtCode.Focus();
                return false;
            }

            if (cbProduct_LookUp.CB_SelectedValue.Length == 0)
            {
                MessageBox.Show("محصول را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                return false;
            }

            if (cbProductTree.SelectedIndex == -1)
            {
                MessageBox.Show("درخت محصول را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbProductTree.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtContractNo.Text))
            {
                txtContractNo.Text = "0";
            }

            if (string.IsNullOrEmpty(Strings.Trim(Strings.Replace(txtDefineDate.Text, "/", ""))))
            {
                MessageBox.Show("تاریخ تعریف بچ را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtDefineDate.Focus();
                return false;
            }

            //if (string.IsNullOrEmpty(Strings.Trim(Strings.Replace(txtDeliveryDate.Text, "/", ""))))
            //{
            //    txtDeliveryDate.Text = "0";
            //    return false;
            //}

            if (txtProductionQuantity.ReadOnly && GetSelectedOrdersQuantity() == 1m)
            {
                MessageBox.Show("سفارشی انتخاب نشده است", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                dgOrders.Focus();
                return false;
            }

            return true;
        }

        private void LoadOrderList()
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    dgOrders.Rows.Clear();
                    var cmOrders = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select A.OrderIndex,A.OrderNo,A.OrderQuantity,B.CustomerName,A.DeliveryDueDate " + "From   Tbl_CustomerOrders A Inner Join Tbl_Customers B ON A.CustomerCode = B.CustomerCode " + "Where  A.AllowFlag = 1 And A.ProductCode = '", cbProduct_LookUp.CB_SelectedValue), "' And Not A.OrderIndex IN (Select OrderIndex From Tbl_Batch_Order Where BatchCode <> '"), txtCode.Text.Trim()), "') Order By OrderNo")), cn);
                    var drOrders = cmOrders.ExecuteReader();
                    while (drOrders.Read())
                    {
                        string DeliveryDate = "0";
                        if (!DBNull.Value.Equals(drOrders["DeliveryDueDate"]) && drOrders["DeliveryDueDate"].ToString().Length == 8)
                        {
                            DeliveryDate = Conversions.ToString(drOrders["DeliveryDueDate"]).Substring(0, 4) + "/" + Conversions.ToString(drOrders["DeliveryDueDate"]).Substring(4, 2) + "/" + Conversions.ToString(drOrders["DeliveryDueDate"]).Substring(6, 2);
                        }

                        dgOrders.Rows.Add(drOrders["OrderIndex"], 0, drOrders["OrderNo"], drOrders["OrderQuantity"], drOrders["CustomerName"], DeliveryDate);
                        if (!string.IsNullOrEmpty(mSelectedOrderIndex))
                        {
                            if (mSelectedOrderIndex.Contains(Conversions.ToString(dgOrders.Rows[dgOrders.RowCount - 1].Cells["colOrderIndex"].Value)))
                            {
                                dgOrders.Rows[dgOrders.RowCount - 1].Cells["colorderSelect"].Value = true;
                            }
                        }
                    }

                    drOrders.Close();
                    cmOrders.CommandText = "Select OrderIndex From Tbl_Batch_Order Where BatchCode='" + txtCode.Text.Trim() + "'";
                    drOrders = cmOrders.ExecuteReader();
                    while (drOrders.Read())
                    {
                        for (int I = 0, loopTo = dgOrders.Rows.Count - 1; I <= loopTo; I++)
                        {
                            if (dgOrders.Rows[I].Cells["colOrderIndex"].Value.Equals(drOrders["OrderIndex"]))
                            {
                                dgOrders.Rows[I].Cells["colorderSelect"].Value = true;
                            }
                        }
                    }

                    drOrders.Close();
                    if (txtProductionQuantity.ReadOnly || ListForm.FormMode == (int)Module1.FormModeEnum.INSERT_MODE)
                    {
                        txtProductionQuantity.Value = GetSelectedOrdersQuantity();
                    }
                }
                catch (Exception objEx)
                {
                    Logger.SaveError(Name + ".LoadOrderList", objEx.Message);
                    MessageBox.Show("فراخوانی لیست سفارشات با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }
        }

        private decimal GetSelectedOrdersQuantity()
        {
            decimal ReturnQuantity = 0m;
            for (int I = 0, loopTo = dgOrders.Rows.Count - 1; I <= loopTo; I++)
            {
                if (Conversions.ToBoolean(dgOrders.Rows[I].Cells["colorderSelect"].Value))
                {
                    ReturnQuantity += Conversions.ToDecimal(dgOrders.Rows[I].Cells["colOrderQuantity"].Value);
                }
            }

            return Conversions.ToDecimal(Interaction.IIf(ReturnQuantity == 0m, 1, ReturnQuantity));
        }

        private void _cbProduct_LookUp_CB_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (cbProduct_LookUp.CB_SelectedValue.Length  > 0)
            if (cbProduct_LookUp.CB_SelectedIndex > -1 && cbProduct_LookUp.CB_SelectedValue is object && cbProduct_LookUp.CB_SelectedValue.ToString() != "System.Data.DataRowView")
                {
                dsProductionPlanning.Tables["Tbl_ProductTree"].DefaultView.RowFilter = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("ProductCode='", cbProduct_LookUp.CB_SelectedValue), "'"));
                if (ListForm.FormMode == (int)Module1.FormModeEnum.INSERT_MODE)
                {
                    var drProduct = dsProductionPlanning.Tables["Tbl_Products"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("ProductCode='", cbProduct_LookUp.CB_SelectedValue), "'")));
                    txtTransferMinQuantity.Value = Conversions.ToDecimal(drProduct[0]["TransferMinimumQuantity"]);
                }

                if (dsProductionPlanning.Tables["Tbl_ProductTree"].DefaultView.Count > 0)
                {
                    {
                        var withBlock = cbProductTree;
                        withBlock.DataSource = null;
                        withBlock.DataSource = dsProductionPlanning.Tables["Tbl_ProductTree"].DefaultView;
                        withBlock.DisplayMember = "TreeTitle";
                        withBlock.ValueMember = "TreeCode";
                    }
                }
                else
                {
                    cbProductTree.DataSource = null;
                }

                if (txtProductionQuantity.ReadOnly || ListForm.FormMode == (int)Module1.FormModeEnum.INSERT_MODE)
                {
                    txtProductionQuantity.Value = 1m;
                }

                LoadOrderList();
            }
            else
            {
                dgOrders.Rows.Clear();
            }
        }
        private void update_ProductionCallDate(SqlTransaction mTrn, int order_index, int Flag)
        {
            if (Flag == 1)
            {
                SqlCommand cm = new SqlCommand("update Tbl_CustomerOrders  set ProductionCallDate='" + Strings.Replace(txtProductionCallDate.Text, " / ", "") + "' where Orderindex='" + order_index + "'", mTrn.Connection);
                cm.Transaction = mTrn;
                cm.ExecuteNonQuery();
            }
            else
            {
                SqlCommand cm = new SqlCommand("update dbo.Tbl_CustomerOrders set ProductionCallDate = '' where OrderIndex in (select OrderIndex from Tbl_Batch_Order where BatchCode = " + order_index + ")", mTrn.Connection);
                cm.Transaction = mTrn;
                cm.ExecuteNonQuery();
            }

        }
        private void FillcallerDate()
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Select ProductionCallDate From Tbl_Batch_Order inner join  dbo.Tbl_CustomerOrders on Tbl_Batch_Order.OrderIndex=dbo.Tbl_CustomerOrders.OrderIndex  Where BatchCode='" + txtCode.Text + "'", cn);
                var rslt = cmd.ExecuteScalar();
                if (rslt != null)
                {
                    txtProductionCallDate.Text = rslt.ToString();
                }
                
                cn.Close();
            }

        }
    }
}
