using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmTreeNode
    {
        public frmTreeNode()
        {
            InitializeComponent();
            _ParentdetailLabel.Name = "ParentdetailLabel";
            _cmdSave.Name = "cmdSave";
            _cmdExit.Name = "cmdExit";
            _cmdDelete.Name = "cmdDelete";
        }

        private DataSet mdsTreeNode;
        private SqlDataAdapter mdaTreeNode = new SqlDataAdapter();
        private SqlDataAdapter daProductOPCs = new SqlDataAdapter();
        private SqlDataAdapter daProductionDetail = new SqlDataAdapter();
        private SqlDataAdapter daProductionSubbatchDetail = new SqlDataAdapter();
        private DataRow mCurrentRow;
        private TreeNode mCurrentNode;
        private string mTreeCode;
        private string mPDetailCode;
        private short mFormMode;
        private short I;

        public short FormMode
        {
            set
            {
                mFormMode = value;
            }
        }

        public DataSet dsProductionPlanning
        {
            set
            {
                mdsTreeNode = value;
            }
        }

        public string TreeCode
        {
            set
            {
                mTreeCode = value;
            }
        }

        public string ParentDetailCode
        {
            set
            {
                mPDetailCode = value;
            }
        }

        public DataRow CurrentRow
        {
            set
            {
                mCurrentRow = value;
            }
        }

        public TreeNode CurrentNode
        {
            set
            {
                mCurrentNode = value;
            }
        }

        private void frmTreeNode_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdSave, 6);
            Module1.SetButtonsImage(cmdDelete, 9);
            Module1.SetButtonsImage(cmdExit, 5);
            FormLoad();
            ParentCodeBox.Text = mPDetailCode;
        }

        private void frmTreeNode_FormClosing(object sender, FormClosingEventArgs e)
        {
            mdsTreeNode.RejectChanges();
            mdaTreeNode.Dispose();
            mCurrentRow = null;
            mCurrentNode = null;
            mdsTreeNode = null;
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("جزء درخت را حذف می کنید", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading) == DialogResult.Yes)
            {
                SqlTransaction trnDelete = null;
                try
                {
                    if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                        Module1.cnProductionPlanning.Open();

                    // کنترل وجود تولید واقعی مرتبط با عملیات های این جزء درخت
                    var cmDelete = new SqlCommand("Select Count(*) From Tbl_RealProduction Where TreeCode = " + mCurrentRow["TreeCode"].ToString() + " And OperationCode IN (Select OperationCode From Tbl_ProductOPCs Where TreeCode = " + mCurrentRow["TreeCode"].ToString() + " And DetailCode = '" + mCurrentRow["DetailCode"].ToString() + "')", Module1.cnProductionPlanning);
                    if (Conversions.ToInteger(cmDelete.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("بدلیل وجود تولید واقعی مرتبط با این جزء درخت امکان حذف وجود ندارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        return;
                    }
                    else
                    {
                        // کنترل وجود برنامه ریزی مرتبط با این درخت
                        cmDelete.CommandText = "Select Count(*) From Tbl_Planning Where TreeCode = " + mCurrentRow["TreeCode"].ToString() + " And OperationCode IN (Select OperationCode From Tbl_ProductOPCs Where TreeCode = " + mCurrentRow["TreeCode"].ToString() + " And DetailCode = '" + mCurrentRow["DetailCode"].ToString() + "')";
                        if (Conversions.ToInteger(cmDelete.ExecuteScalar()) > 0)
                        {
                            MessageBox.Show("بدلیل وجود برنامه ریزی مرتبط با این جزء درخت امکان حذف وجود ندارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                            return;
                        }
                        else
                        {
                            // کنترل وجود عملیات 
                            cmDelete.CommandText = "Select Count(*) From Tbl_ProductOPCs Where TreeCode = " + mCurrentRow["TreeCode"].ToString() + " And DetailCode = '" + mCurrentRow["DetailCode"].ToString() + "'";
                            if (Conversions.ToInteger(cmDelete.ExecuteScalar()) > 0)
                            {
                                MessageBox.Show("پیش از حذف جزءدرخت باید عملیات های موجود در آن حذف گردند", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                                return;
                            }
                        }
                    }

                    trnDelete = Module1.cnProductionPlanning.BeginTransaction();
                    mdaTreeNode.DeleteCommand.Transaction = trnDelete;
                    daProductionDetail.DeleteCommand.Transaction = trnDelete;
                    daProductionSubbatchDetail.DeleteCommand.Transaction = trnDelete;

                    // حذف اطلاعات موجودي بچ
                    for (short I = (short)(mdsTreeNode.Tables["Tbl_ProductionBatchsDetail"].Rows.Count - 1); I >= 0; I += -1)
                        mdsTreeNode.Tables["Tbl_ProductionBatchsDetail"].Rows[I].Delete();

                    // حذف اطلاعات موجودي ساب بچ
                    for (short I = (short)(mdsTreeNode.Tables["Tbl_ProductionSubbatchsDetail"].Rows.Count - 1); I >= 0; I += -1)
                        mdsTreeNode.Tables["Tbl_ProductionSubbatchsDetail"].Rows[I].Delete();
                    mCurrentRow.Delete();
                    SaveChanges();
                    trnDelete.Commit();
                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (Exception objEx)
                {
                    mdsTreeNode.RejectChanges();
                    trnDelete.Rollback();
                    Logger.SaveError(Name + ".cmdDelete_Click", objEx.Message);
                    MessageBox.Show("حذف جزءدرخت با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                }
                finally
                {
                    if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                        Module1.cnProductionPlanning.Close();
                }
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            mPDetailCode = ParentCodeBox.Text;
            if (!ValidationForm())
            {
                return;
            }

            string mAlarmDesc = Constants.vbNullString;
            switch (mFormMode)
            {
                case (short)Module1.FormModeEnum.INSERT_MODE:
                    {
                        DataRow drInsert;
                        SqlTransaction trnInsert = null;
                        try
                        {
                            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                                Module1.cnProductionPlanning.Open();
                            trnInsert = Module1.cnProductionPlanning.BeginTransaction();
                            mdaTreeNode.InsertCommand.Transaction = trnInsert;

                            // اضافه کردن رکورد جدید به جدول در دیتاست
                            drInsert = mdsTreeNode.Tables["Tbl_ProductTreeDetails"].NewRow();
                            drInsert["TreeCode"] = mTreeCode;
                            drInsert["DetailCode"] = txtCode.Text;
                            drInsert["ParentDetailCode"] = mPDetailCode;
                            drInsert["DetailName"] = txtName.Text;
                            drInsert["LevelNo"] = mCurrentNode.Level + 1;
                            drInsert["ParentQuantity"] = txtParentQuantity.Value;
                            drInsert["BuiltType"] = Interaction.IIf(rbBT_Built.Checked, 1, 2);
                            drInsert["TechniqueMapsProperties"] = txtMapsSpecifications.Text;
                            drInsert["Weight"] = txtWeight.Value;
                            drInsert["Height"] = txtHeight.Value;
                            drInsert["Temperature"] = txtTemperature.Value;
                            drInsert["Volume"] = txtVolume.Value;
                            drInsert["Physical1"] = txtPhysical1.Text;
                            drInsert["Physical2"] = txtPhysical2.Text;
                            drInsert["Physical3"] = txtPhysical3.Text;
                            drInsert["ProductionTestUnit"] = cbProductionTestUnit.SelectedValue;
                            drInsert["StorePlace"] = cbStorePlace.SelectedValue;
                            drInsert["StoreTestUnit"] = cbStoreTestUnit.SelectedValue;
                            drInsert["Description"] = txtDescription.Text;
                            mdsTreeNode.Tables["Tbl_ProductTreeDetails"].Rows.Add(drInsert);
                            SaveChanges();
                            trnInsert.Commit();
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            mdsTreeNode.RejectChanges();
                            trnInsert.Rollback();
                            MessageBox.Show("اشکال در ثبت رکورد جدید، رکورد جدید ثبت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            MessageBox.Show(objEx.Message);
                        }
                        finally
                        {
                            drInsert = null;
                            trnInsert.Dispose();
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                        }

                        break;
                    }

                case (short)Module1.FormModeEnum.EDIT_MODE:
                    {
                        SqlTransaction trnUpdate = null;
                        var drChilds = mdsTreeNode.Tables["Tbl_ProductTreeDetails"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("ParentDetailCode='", mCurrentRow["DetailCode"]), "'")));
                        var drProductOPCs = mdsTreeNode.Tables["Tbl_ProductOPCs"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("DetailCode='", mCurrentRow["DetailCode"]), "'")));
                        try
                        {
                            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                                Module1.cnProductionPlanning.Open();
                            trnUpdate = Module1.cnProductionPlanning.BeginTransaction();
                            mdaTreeNode.UpdateCommand.Transaction = trnUpdate;
                            daProductOPCs.UpdateCommand.Transaction = trnUpdate;
                            daProductionDetail.UpdateCommand.Transaction = trnUpdate;
                            daProductionSubbatchDetail.UpdateCommand.Transaction = trnUpdate;
                            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(mCurrentRow["ParentQuantity"], txtParentQuantity.Value, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(mCurrentRow["BuiltType"], Interaction.IIf(rbBT_Built.Checked, 1, 2), false)))
                            {
                                mAlarmDesc = "مشخصات جزء درخت با کد: {" + txtCode.Text + "} تغییر کرد";
                            }

                            // تغییر در اطلاعات رکورد جاری
                            mCurrentRow.BeginEdit();
                            mCurrentRow["TreeCode"] = mTreeCode;
                            mCurrentRow["DetailCode"] = txtCode.Text;
                            mCurrentRow["ParentDetailCode"] = mPDetailCode;
                            mCurrentRow["DetailName"] = txtName.Text;
                            mCurrentRow["LevelNo"] = mCurrentNode.Level;
                            mCurrentRow["ParentQuantity"] = txtParentQuantity.Value;
                            mCurrentRow["BuiltType"] = Interaction.IIf(rbBT_Built.Checked, 1, 2);
                            mCurrentRow["TechniqueMapsProperties"] = txtMapsSpecifications.Text;
                            mCurrentRow["Weight"] = txtWeight.Value;
                            mCurrentRow["Height"] = txtHeight.Value;
                            mCurrentRow["Temperature"] = txtTemperature.Value;
                            mCurrentRow["Volume"] = txtVolume.Value;
                            mCurrentRow["Physical1"] = txtPhysical1.Text;
                            mCurrentRow["Physical2"] = txtPhysical2.Text;
                            mCurrentRow["Physical3"] = txtPhysical3.Text;
                            mCurrentRow["ProductionTestUnit"] = cbProductionTestUnit.SelectedValue;
                            mCurrentRow["StorePlace"] = cbStorePlace.SelectedValue;
                            mCurrentRow["StoreTestUnit"] = cbStoreTestUnit.SelectedValue;
                            mCurrentRow["Description"] = txtDescription.Text;
                            mCurrentRow.EndEdit();
                            var loopTo = (short)(drChilds.Length - 1);
                            for (I = 0; I <= loopTo; I++)
                            {
                                drChilds[I].BeginEdit();
                                drChilds[I]["ParentDetailCode"] = txtCode.Text;
                                drChilds[I].EndEdit();
                            }

                            var loopTo1 = (short)(drProductOPCs.Length - 1);
                            for (I = 0; I <= loopTo1; I++)
                            {
                                drProductOPCs[I].BeginEdit();
                                drProductOPCs[I]["DetailCode"] = txtCode.Text;
                                drProductOPCs[I].EndEdit();
                            }

                            // اصلاح كد جزء موجودي بچ
                            for (short I = 0, loopTo2 = (short)(mdsTreeNode.Tables["Tbl_ProductionBatchsDetail"].Rows.Count - 1); I <= loopTo2; I++)
                                mdsTreeNode.Tables["Tbl_ProductionBatchsDetail"].Rows[I]["DetailCode"] = txtCode.Text;

                            // اصلاح كد جزء موجودي ساب بچ
                            for (short I = 0, loopTo3 = (short)(mdsTreeNode.Tables["Tbl_ProductionSubbatchsDetail"].Rows.Count - 1); I <= loopTo3; I++)
                                mdsTreeNode.Tables["Tbl_ProductionSubbatchsDetail"].Rows[I]["DetailCode"] = txtCode.Text;
                            SaveChanges();
                            trnUpdate.Commit();
                            if (!string.IsNullOrEmpty(mAlarmDesc))
                            {
                                Module1.Check_Subbatch_HasPlanningAlarm(mTreeCode, "", "", "1", mAlarmDesc);
                            }

                            DialogResult = DialogResult.OK;
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            mdsTreeNode.RejectChanges();
                            trnUpdate.Rollback();
                            Logger.SaveError(Name + ".cmdSave_Click", objEx.Message);
                            MessageBox.Show("اشکال در اصلاح رکورد، تغییرات ثبت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        }
                        finally
                        {
                            trnUpdate.Dispose();
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                        }

                        break;
                    }
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Controls_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Strings.Asc(e.KeyChar) == (int)Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void FormLoad()
        {
            try
            {
                CreateDataAdapterCommands();
                {
                    var withBlock = cbProductionTestUnit;
                    withBlock.DataSource = null;
                    withBlock.DataSource = mdsTreeNode.Tables["Tbl_ProductionTestUnits"];
                    withBlock.DisplayMember = "Title";
                    withBlock.ValueMember = "Code";
                }

                {
                    var withBlock1 = cbStoreTestUnit;
                    withBlock1.DataSource = null;
                    withBlock1.DataSource = mdsTreeNode.Tables["Tbl_StoreTestUnits"];
                    withBlock1.DisplayMember = "Title";
                    withBlock1.ValueMember = "Code";
                }

                {
                    var withBlock2 = cbStorePlace;
                    withBlock2.DataSource = null;
                    withBlock2.DataSource = mdsTreeNode.Tables["Tbl_Stores"];
                    withBlock2.DisplayMember = "StoreName";
                    withBlock2.ValueMember = "StoreCode";
                }

                switch (mFormMode)
                {
                    case (short)Module1.FormModeEnum.INSERT_MODE:
                        {
                            txtCode.Focus();
                            break;
                        }

                    case (short)Module1.FormModeEnum.EDIT_MODE:
                    case (short)Module1.FormModeEnum.DELETE_MODE:
                        {
                            FillControls();
                            switch (mFormMode)
                            {
                                case (short)Module1.FormModeEnum.EDIT_MODE:
                                    {
                                        if (Panel2.Enabled)
                                        {
                                            txtCode.Focus();
                                        }
                                        else
                                        {
                                            txtMapsSpecifications.Focus();
                                        }

                                        break;
                                    }

                                case (short)Module1.FormModeEnum.DELETE_MODE:
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
                MessageBox.Show("فراخوانی فرم با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(objEx.Message);
            }
        }

        private void SaveChanges()
        {
            DataSet dsChanges;
            dsChanges = mdsTreeNode.GetChanges();
            if (dsChanges.HasErrors)
            {
                mdsTreeNode.RejectChanges();
            }
            else
            {
                if (mFormMode == (int)Module1.FormModeEnum.INSERT_MODE)
                {
                    mdaTreeNode.Update(dsChanges, "Tbl_ProductTreeDetails");
                }
                else if (mFormMode == (int)Module1.FormModeEnum.EDIT_MODE)
                {
                    mdaTreeNode.Update(dsChanges, "Tbl_ProductTreeDetails");
                    daProductOPCs.Update(dsChanges, "Tbl_ProductOPCs");
                    daProductionDetail.Update(dsChanges, "Tbl_ProductionBatchsDetail");
                    daProductionSubbatchDetail.Update(dsChanges, "Tbl_ProductionSubbatchsDetail");
                }
                else if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
                {
                    daProductionDetail.Update(dsChanges, "Tbl_ProductionBatchsDetail");
                    daProductionSubbatchDetail.Update(dsChanges, "Tbl_ProductionSubbatchsDetail");
                    mdaTreeNode.Update(dsChanges, "Tbl_ProductTreeDetails");
                }

                mdsTreeNode.AcceptChanges();
            }

            dsChanges = null;
        }

        private void CreateDataAdapterCommands()
        {
            // -------------------- ایجاد دستورات جدول اجزای درخت محصول ----------------------
            // ایجاد دستور اضافه کردن رکورد جدید در جدول
            mdaTreeNode.InsertCommand = new SqlCommand("Insert Into Tbl_ProductTreeDetails(TreeCode,DetailCode,ParentDetailCode,DetailName,LevelNo,ParentQuantity,BuiltType,TechniqueMapsProperties,Weight,Height,Temperature,Volume,Physical1,Physical2,Physical3,ProductionTestUnit,StorePlace,StoreTestUnit,Description) " + "Values(@TreeCode,@DetailCode,@ParentDetailCode,@DetailName,@LevelNo,@ParentQuantity,@BuiltType,@TechniqueMapsProperties,@Weight,@Height,@Temperature,@Volume,@Physical1,@Physical2,@Physical3,@ProductionTestUnit,@StorePlace,@StoreTestUnit,@Description)", Module1.cnProductionPlanning);
            {
                var withBlock = mdaTreeNode.InsertCommand;
                withBlock.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode");
                withBlock.Parameters.Add("@DetailCode", SqlDbType.VarChar, 50, "DetailCode");
                withBlock.Parameters.Add("@ParentDetailCode", SqlDbType.VarChar, 50, "ParentDetailCode");
                withBlock.Parameters.Add("@DetailName", SqlDbType.VarChar, 100, "DetailName");
                withBlock.Parameters.Add("@LevelNo", SqlDbType.SmallInt, 25, "LevelNo");
                withBlock.Parameters.Add("@ParentQuantity", SqlDbType.Float, default, "ParentQuantity");
                withBlock.Parameters.Add("@BuiltType", SqlDbType.TinyInt, default, "BuiltType");
                withBlock.Parameters.Add("@TechniqueMapsProperties", SqlDbType.VarChar, 100, "TechniqueMapsProperties");
                withBlock.Parameters.Add("@Weight", SqlDbType.Float, default, "Weight");
                withBlock.Parameters.Add("@Height", SqlDbType.Float, default, "Height");
                withBlock.Parameters.Add("@Temperature", SqlDbType.Float, default, "Temperature");
                withBlock.Parameters.Add("@Volume", SqlDbType.Float, default, "Volume");
                withBlock.Parameters.Add("@Physical1", SqlDbType.VarChar, 50, "Physical1");
                withBlock.Parameters.Add("@Physical2", SqlDbType.VarChar, 50, "Physical2");
                withBlock.Parameters.Add("@Physical3", SqlDbType.VarChar, 50, "Physical3");
                withBlock.Parameters.Add("@ProductionTestUnit", SqlDbType.Int, default, "ProductionTestUnit");
                withBlock.Parameters.Add("@StorePlace", SqlDbType.VarChar, 50, "StorePlace");
                withBlock.Parameters.Add("@StoreTestUnit", SqlDbType.Int, default, "StoreTestUnit");
                withBlock.Parameters.Add("@Description", SqlDbType.VarChar, 255, "Description");
            }
            // ایجاد دستور اصلاح رکورد جاری در جدول
            mdaTreeNode.UpdateCommand = new SqlCommand("Update Tbl_ProductTreeDetails Set DetailCode=@CurrentDetailCode,ParentDetailCode=@ParentDetailCode," + "DetailName=@DetailName,LevelNo=@LevelNo,ParentQuantity=@ParentQuantity,BuiltType=@BuiltType," + "TechniqueMapsProperties=@TechniqueMapsProperties,Weight=@Weight,Height=@Height,Temperature=@Temperature," + "Volume=@Volume,Physical1=@Physical1,Physical2=@Physical2,Physical3=@Physical3,ProductionTestUnit=@ProductionTestUnit," + "StorePlace=@StorePlace,StoreTestUnit=@StoreTestUnit,Description=@Description Where TreeCode=@TreeCode And DetailCode=@OldDetailCode", Module1.cnProductionPlanning);



            {
                var withBlock1 = mdaTreeNode.UpdateCommand;
                withBlock1.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode").SourceVersion = DataRowVersion.Original;
                withBlock1.Parameters.Add("@CurrentDetailCode", SqlDbType.VarChar, 50, "DetailCode").SourceVersion = DataRowVersion.Current;
                withBlock1.Parameters.Add("@ParentDetailCode", SqlDbType.VarChar, 50, "ParentDetailCode");
                withBlock1.Parameters.Add("@DetailName", SqlDbType.VarChar, 100, "DetailName");
                withBlock1.Parameters.Add("@LevelNo", SqlDbType.SmallInt, 25, "LevelNo");
                withBlock1.Parameters.Add("@ParentQuantity", SqlDbType.Float, default, "ParentQuantity");
                withBlock1.Parameters.Add("@BuiltType", SqlDbType.TinyInt, default, "BuiltType");
                withBlock1.Parameters.Add("@TechniqueMapsProperties", SqlDbType.VarChar, 100, "TechniqueMapsProperties");
                withBlock1.Parameters.Add("@Weight", SqlDbType.Float, default, "Weight");
                withBlock1.Parameters.Add("@Height", SqlDbType.Float, default, "Height");
                withBlock1.Parameters.Add("@Temperature", SqlDbType.Float, default, "Temperature");
                withBlock1.Parameters.Add("@Volume", SqlDbType.Float, default, "Volume");
                withBlock1.Parameters.Add("@Physical1", SqlDbType.VarChar, 50, "Physical1");
                withBlock1.Parameters.Add("@Physical2", SqlDbType.VarChar, 50, "Physical2");
                withBlock1.Parameters.Add("@Physical3", SqlDbType.VarChar, 50, "Physical3");
                withBlock1.Parameters.Add("@ProductionTestUnit", SqlDbType.Int, default, "ProductionTestUnit");
                withBlock1.Parameters.Add("@StorePlace", SqlDbType.VarChar, 50, "StorePlace");
                withBlock1.Parameters.Add("@StoreTestUnit", SqlDbType.Int, default, "StoreTestUnit");
                withBlock1.Parameters.Add("@Description", SqlDbType.VarChar, 255, "Description");
                withBlock1.Parameters.Add("@OldDetailCode", SqlDbType.VarChar, 50, "DetailCode").SourceVersion = DataRowVersion.Original;
            }
            // ایجاد دستور حذف رکورد جاری در جدول
            mdaTreeNode.DeleteCommand = new SqlCommand("Delete From Tbl_ProductTreeDetails Where TreeCode=@TreeCode And DetailCode=@DetailCode", Module1.cnProductionPlanning);
            {
                var withBlock2 = mdaTreeNode.DeleteCommand;
                withBlock2.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode");
                withBlock2.Parameters[0].Direction = ParameterDirection.Input;
                withBlock2.Parameters[0].SourceVersion = DataRowVersion.Original;
                withBlock2.Parameters.Add("@DetailCode", SqlDbType.VarChar, 50, "DetailCode");
                withBlock2.Parameters[1].Direction = ParameterDirection.Input;
                withBlock2.Parameters[1].SourceVersion = DataRowVersion.Original;
            }

            // ------>>>>>>>>>>>>> عمليات هاي برگه عمليات
            daProductOPCs.UpdateCommand = new SqlCommand("Update Tbl_ProductOPCs Set DetailCode=@DetailCode Where TreeCode=@TreeCode And OperationCode=@OperationCode", Module1.cnProductionPlanning);
            {
                var withBlock3 = daProductOPCs.UpdateCommand;
                withBlock3.Parameters.Add("@DetailCode", SqlDbType.VarChar, 50, "DetailCode");
                withBlock3.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode").SourceVersion = DataRowVersion.Original;
                withBlock3.Parameters.Add("@OperationCode", SqlDbType.VarChar, 50, "OperationCode").SourceVersion = DataRowVersion.Original;
            }

            // ------>>>>>>>>>>>>> موجودی اولیه بچ تولید
            daProductionDetail.UpdateCommand = new SqlCommand("Update Tbl_ProductionBatchsDetail Set DetailCode=@CurrentDetailCode Where BatchCode=@BatchCode And DetailCode=@OldDetailCode", Module1.cnProductionPlanning);
            {
                var withBlock4 = daProductionDetail.UpdateCommand;
                withBlock4.Parameters.Add("@CurrentDetailCode", SqlDbType.VarChar, 50, "DetailCode").SourceVersion = DataRowVersion.Current;
                withBlock4.Parameters.Add("@BatchCode", SqlDbType.VarChar, 20, "BatchCode").SourceVersion = DataRowVersion.Original;
                withBlock4.Parameters.Add("@OldDetailCode", SqlDbType.VarChar, 50, "DetailCode").SourceVersion = DataRowVersion.Original;
            }

            daProductionDetail.DeleteCommand = new SqlCommand("Delete From Tbl_ProductionBatchsDetail Where BatchCode=@BatchCode And DetailCode=@DetailCode", Module1.cnProductionPlanning);
            {
                var withBlock5 = daProductionDetail.DeleteCommand;
                withBlock5.Parameters.Add("@BatchCode", SqlDbType.VarChar, 20, "BatchCode").SourceVersion = DataRowVersion.Original;
                withBlock5.Parameters.Add("@DetailCode", SqlDbType.VarChar, 50, "DetailCode").SourceVersion = DataRowVersion.Original;
            }

            // ------>>>>>>>>>>>>> موجودی اولیه ساب بچ تولید
            daProductionSubbatchDetail.UpdateCommand = new SqlCommand("Update Tbl_ProductionSubbatchsDetail Set DetailCode=@CurrentDetailCode Where SubbatchCode=@SubbatchCode And DetailCode=@OldDetailCode", Module1.cnProductionPlanning);
            {
                var withBlock6 = daProductionSubbatchDetail.UpdateCommand;
                withBlock6.Parameters.Add("@CurrentDetailCode", SqlDbType.VarChar, 50, "DetailCode").SourceVersion = DataRowVersion.Current;
                withBlock6.Parameters.Add("@SubbatchCode", SqlDbType.VarChar, 50, "SubbatchCode").SourceVersion = DataRowVersion.Original;
                withBlock6.Parameters.Add("@OldDetailCode", SqlDbType.VarChar, 50, "DetailCode").SourceVersion = DataRowVersion.Original;
            }

            daProductionSubbatchDetail.DeleteCommand = new SqlCommand("Delete From Tbl_ProductionSubbatchsDetail Where SubbatchCode=@SubbatchCode And DetailCode=@DetailCode", Module1.cnProductionPlanning);
            {
                var withBlock7 = daProductionSubbatchDetail.DeleteCommand;
                withBlock7.Parameters.Add("@SubbatchCode", SqlDbType.VarChar, 50, "SubbatchCode").SourceVersion = DataRowVersion.Original;
                withBlock7.Parameters.Add("@DetailCode", SqlDbType.VarChar, 50, "DetailCode").SourceVersion = DataRowVersion.Original;
            }
        }

        private DataView GetCurrentRowChilds(string DetailCode)
        {
            var dvChilds = mdsTreeNode.Tables["Tbl_ProductTreeDetails"].DefaultView;
            dvChilds.RowFilter = "ParentDetailCode='" + DetailCode + "'";
            return dvChilds;
        }

        private void FillControls()
        {
            txtCode.Text = Conversions.ToString(mCurrentRow["DetailCode"]);
            txtName.Text = Conversions.ToString(mCurrentRow["DetailName"]);
            txtParentQuantity.Value = Conversions.ToDecimal(mCurrentRow["ParentQuantity"]);
            ParentCodeBox.Text = Conversions.ToString(mCurrentRow["ParentDetailCode"]);
            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(mCurrentRow["BuiltType"], 1, false)))
            {
                rbBT_Built.Checked = true;
            }
            else
            {
                rbBT_Buy.Checked = true;
            }

            txtMapsSpecifications.Text = Conversions.ToString(mCurrentRow["TechniqueMapsProperties"]);
            txtWeight.Value = Conversions.ToDecimal(mCurrentRow["Weight"]);
            txtHeight.Value = Conversions.ToDecimal(mCurrentRow["Height"]);
            txtTemperature.Value = Conversions.ToDecimal(mCurrentRow["Temperature"]);
            txtVolume.Value = Conversions.ToDecimal(mCurrentRow["Volume"]);
            txtPhysical1.Text = Conversions.ToString(mCurrentRow["Physical1"]);
            txtPhysical2.Text = Conversions.ToString(mCurrentRow["Physical2"]);
            txtPhysical3.Text = Conversions.ToString(mCurrentRow["Physical3"]);
            cbProductionTestUnit.SelectedValue = mCurrentRow["ProductionTestUnit"];
            cbStorePlace.SelectedValue = mCurrentRow["StorePlace"];
            cbStoreTestUnit.SelectedValue = mCurrentRow["StoreTestUnit"];
            txtDescription.Text = Conversions.ToString(mCurrentRow["Description"]);
        }

        private bool ValidationForm()
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                MessageBox.Show("کد جزء را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtCode.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("نام جزء را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtName.Focus();
                return false;
            }

            if (cbProductionTestUnit.SelectedIndex == -1)
            {
                MessageBox.Show("واحد سنجش تولید را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbProductionTestUnit.Focus();
                return false;
            }

            if (cbStorePlace.SelectedIndex == -1)
            {
                MessageBox.Show("محل نگهداری را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbStorePlace.Focus();
                return false;
            }

            if (cbStoreTestUnit.SelectedIndex == -1)
            {
                MessageBox.Show("محل نگهداری را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbStoreTestUnit.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtMapsSpecifications.Text))
                txtMapsSpecifications.Text = 0.ToString();
            if (string.IsNullOrEmpty(txtPhysical1.Text))
                txtPhysical1.Text = 0.ToString();
            if (string.IsNullOrEmpty(txtPhysical2.Text))
                txtPhysical2.Text = 0.ToString();
            if (string.IsNullOrEmpty(txtPhysical3.Text))
                txtPhysical3.Text = 0.ToString();
            if (string.IsNullOrEmpty(txtDescription.Text))
                txtDescription.Text = 0.ToString();
            return true;
        }

        private void ParentdetailLabel_Click(object sender, EventArgs e)
        {
        }

        private void ParentdetailLabel_DoubleClick(object sender, EventArgs e)
        {
            ParentdetailLabel.Visible = true;
        }
    }
}