using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmCopyProductTree
    {
        public frmCopyProductTree()
        {
            InitializeComponent();
            _cbSourceTree.Name = "cbSourceTree";
            _cbSourceProduct.Name = "cbSourceProduct";
            _cbDestinationProduct.Name = "cbDestinationProduct";
            _cmdSave.Name = "cmdSave";
        }

        private frmProductTreesList mListForm;
        private SqlDataAdapter daProductTree = new SqlDataAdapter();
        private DataView dvSourceTreeDetails;
        private DataView dvSourceOPCs;
        private DataView dvSourcePreOPs;
        private DataView dvSourceMaterials;
        private DataView dvSourceMachines;
        private DataView dvSourceContractors;
        private short I;

        public frmProductTreesList ListForm
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

        private void frmCopyProductTree_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdSave, 6);
            Module1.SetButtonsImage(cmdExit, 14);
            FormLoad();
        }

        private void frmCopyProductTree_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                ListForm.cbProductName.SelectedValue = cbDestinationProduct.SelectedValue;
            }

            var loopTo = (short)(dsProductionPlanning.Tables.Count - 6);
            for (I = (short)(dsProductionPlanning.Tables.Count - 1); I >= loopTo; I += -1)
            {
                dsProductionPlanning.Tables[I].Dispose();
                dsProductionPlanning.Tables.RemoveAt(I);
            }

            daProductTree.Dispose();
        }

        private void cbSourceProduct_SelectedValueChanged(object sender, EventArgs e)
        {
            DataView dvSourceTrees = (DataView)cbSourceTree.DataSource;
            if (cbSourceProduct.Items.Count > 0 && cbSourceProduct.SelectedValue is object && cbSourceProduct.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                cmdSave.Enabled = true;
                dvSourceTrees.RowFilter = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("ProductCode='", cbSourceProduct.SelectedValue), "'"));
                cbSourceTree.SelectedIndex = -1;
            }
            else
            {
                cmdSave.Enabled = false;
                dvSourceTrees.RowFilter = "ProductCode=''";
                cbSourceTree.SelectedIndex = -1;
            }
        }

        private void cbSourceTree_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbSourceTree.Items.Count > 0 && cbSourceTree.SelectedValue is object && cbSourceTree.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                cmdSave.Enabled = true;
                dvSourceTreeDetails.RowFilter = Conversions.ToString(Operators.ConcatenateObject("TreeCode=", cbSourceTree.SelectedValue));
                dvSourcePreOPs.RowFilter = Conversions.ToString(Operators.ConcatenateObject("TreeCode=", cbSourceTree.SelectedValue));
                dvSourceMaterials.RowFilter = Conversions.ToString(Operators.ConcatenateObject("TreeCode=", cbSourceTree.SelectedValue));
                dvSourceMachines.RowFilter = Conversions.ToString(Operators.ConcatenateObject("TreeCode=", cbSourceTree.SelectedValue));
                dvSourceContractors.RowFilter = Conversions.ToString(Operators.ConcatenateObject("TreeCode=", cbSourceTree.SelectedValue));
            }

            // For I = 0 To dvSourceTreeDetails.Count - 1
            // MessageBox.Show(dvSourceTreeDetails(I)("DetailName"))
            // Next I
            else
            {
                cmdSave.Enabled = false;
                dvSourceTreeDetails.RowFilter = "TreeCode=0";
            }
        }

        private void cbDestinationProduct_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbDestinationProduct.Items.Count > 0 && cbDestinationProduct.SelectedValue is object && cbDestinationProduct.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                cmdSave.Enabled = true;
            }
            else
            {
                cmdSave.Enabled = false;
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!ValidationForm())
            {
                return;
            }

            DataRow drInsert;
            SqlTransaction trnInsert = null;

            // Try
            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                Module1.cnProductionPlanning.Open();
            trnInsert = Module1.cnProductionPlanning.BeginTransaction();
            daProductTree.InsertCommand.Transaction = trnInsert;
            daProductTree.UpdateCommand.Transaction = trnInsert;
            if (chkDefualt.Checked)
            {
                SetDefualtTree();
            }

            // ثبت مشخصات کلی درخت جدید
            drInsert = dsProductionPlanning.Tables["Tbl_ProductTree"].NewRow();
            drInsert["TreeCode"] = txtTreeCode.Value;
            drInsert["ProductCode"] = cbDestinationProduct.SelectedValue;
            drInsert["TreeTitle"] = txtTreeTitle.Text;
            drInsert["DefualtTree"] = chkDefualt.Checked;
            drInsert["ProductCode1"] = cbDestinationProduct.SelectedValue;
            drInsert["ProductName"] = cbDestinationProduct.Text;
            dsProductionPlanning.Tables["Tbl_ProductTree"].Rows.Add(drInsert);
            SaveChanges();

            // ثبت مشخصات ریشۀ درخت جدید
            var cmInsert = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Insert Into Tbl_ProductTreeDetails(TreeCode,DetailCode,ParentDetailCode,DetailName,LevelNo,ParentQuantity,BuiltType," + "TechniqueMapsProperties,Weight,Height,Temperature,Volume,Physical1,Physical2,Physical3," + "ProductionTestUnit,StorePlace,StoreTestUnit,[Description]) " + "Values(" + txtTreeCode.Value + ",'", cbDestinationProduct.SelectedValue), "',0,'"), cbDestinationProduct.Text), "',0,1,1,'',0,0,0,0,0,0,0,0,0,0,''"), ")")), Module1.cnProductionPlanning);



            cmInsert.Transaction = trnInsert;
            cmInsert.ExecuteNonQuery();

            // ثبت مشخصات عملیات های ریشۀ درخت جدید
            AddDetailOperations(cbSourceProduct.SelectedValue.ToString(), cbDestinationProduct.SelectedValue.ToString(), ref trnInsert);

            // ثبت مشخصات اجزاء درخت جدید
            AddChildsDetail(cbSourceProduct.SelectedValue.ToString(), cbDestinationProduct.SelectedValue.ToString(), ref trnInsert);

            // کپی کردن عملیاتهای پیشنیاز درخت
            for (int J = 0, loopTo = dvSourcePreOPs.Count - 1; J <= loopTo; J++)
            {
                cmInsert.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Insert Into Tbl_PreOperations(TreeCode,CurrentOperationCode,PreOperationCode,RelationType,LagTime,TimeType)" + "Values(" + txtTreeCode.Value + ",'", dvSourcePreOPs[J]["CurrentOperationCode"]), "','"), dvSourcePreOPs[J]["PreOperationCode"]), "',"), dvSourcePreOPs[J]["RelationType"]), ","), dvSourcePreOPs[J]["LagTime"]), ","), dvSourcePreOPs[J]["TimeType"]), ")"));


                cmInsert.ExecuteNonQuery();
            }

            // کپی کردن مشخصات مواد وارده به عملیاتهای درخت
            for (int J = 0, loopTo1 = dvSourceMaterials.Count - 1; J <= loopTo1; J++)
            {
                cmInsert.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Insert Into Tbl_OperationMaterials(TreeCode,CurrentOperationCode,MaterialCode,OneUnitBuildAmount,ProductionTestUnit)" + "Values(" + txtTreeCode.Value + ",'", dvSourceMaterials[J]["CurrentOperationCode"]), "','"), dvSourceMaterials[J]["MaterialCode"]), "',"), dvSourceMaterials[J]["OneUnitBuildAmount"]), ","), dvSourceMaterials[J]["ProductionTestUnit"]), ")"));


                cmInsert.ExecuteNonQuery();
            }

            // کپی کردن مشخصات ماشین آلات انجام دهندۀ درخت
            for (int J = 0, loopTo2 = dvSourceMachines.Count - 1; J <= loopTo2; J++)
            {
                cmInsert.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Insert Into Tbl_ProductOPCsExecutorMachines(TreeCode,OperationCode,MachineCode,MachinePriority," + "Operators,OneExecutionTime,TimeType,MinimumProduction,MaximumAccumulation,GarbagePercent," + "PerformancePercent,OperatorPerformancePercent,RedoPercent,ExecutionPerformancePercent," + "MachineSetupTime,SetupTimeType,OneExecutionPrice,CalendarCode)" + "Values(" + txtTreeCode.Value + ",'", dvSourceMachines[J]["OperationCode"]), "','"), dvSourceMachines[J]["MachineCode"]), "',"), dvSourceMachines[J]["MachinePriority"]), ","), dvSourceMachines[J]["Operators"]), ","), dvSourceMachines[J]["OneExecutionTime"]), ","), dvSourceMachines[J]["TimeType"]), ","), dvSourceMachines[J]["MinimumProduction"]), ","), dvSourceMachines[J]["MaximumAccumulation"]), ","), dvSourceMachines[J]["GarbagePercent"]), ","), dvSourceMachines[J]["PerformancePercent"]), ","), dvSourceMachines[J]["OperatorPerformancePercent"]), ","), dvSourceMachines[J]["RedoPercent"]), ",'"), dvSourceMachines[J]["ExecutionPerformancePercent"]), "',"), dvSourceMachines[J]["MachineSetupTime"]), ","), dvSourceMachines[J]["SetupTimeType"]), ","), dvSourceMachines[J]["OneExecutionPrice"]), ",'"), dvSourceMachines[J]["CalendarCode"]), "')"));












                cmInsert.ExecuteNonQuery();
            }

            // کپی کردن پیمانکاران عملیاتها درخت
            for (int J = 0, loopTo3 = dvSourceContractors.Count - 1; J <= loopTo3; J++)
            {
                cmInsert.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Insert Into Tbl_ContractorOperations(TreeCode,OperationCode,ContractorCode,MinimumTransferBatch," + "TimeType,TransferBatchExecutionTime,BatchCapacity)" + "Values(" + txtTreeCode.Value + ",'", dvSourceContractors[J]["OperationCode"]), "','"), dvSourceContractors[J]["ContractorCode"]), "',"), dvSourceContractors[J]["MinimumTransferBatch"]), ","), dvSourceContractors[J]["TimeType"]), ","), dvSourceContractors[J]["TransferBatchExecutionTime"]), ","), dvSourceContractors[J]["BatchCapacity"]), ")"));




                cmInsert.ExecuteNonQuery();
            }

            trnInsert.Commit();
            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                Module1.cnProductionPlanning.Close();
            Close();
            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                Module1.cnProductionPlanning.Close();
        }

        private void FormLoad()
        {
            try
            {
                CreateDataAdapterCommands();
                dvSourceTreeDetails = dsProductionPlanning.Tables["Tbl_ProductTreeDetails"].DefaultView;
                dvSourceOPCs = dsProductionPlanning.Tables["Tbl_ProductOPCs"].DefaultView;
                dvSourcePreOPs = dsProductionPlanning.Tables["Tbl_PreOperations"].DefaultView;
                dvSourceMaterials = dsProductionPlanning.Tables["Tbl_OperationMaterials"].DefaultView;
                dvSourceMachines = dsProductionPlanning.Tables["Tbl_ProductOPCsExecutorMachines"].DefaultView;
                dvSourceContractors = dsProductionPlanning.Tables["Tbl_ContractorOperations"].DefaultView;
                var daCombo = new SqlDataAdapter("Select TreeCode,TreeTitle,ProductCode From Tbl_ProductTree Order By TreeTitle", Module1.cnProductionPlanning);
                var dtCombo = new DataTable();
                daCombo.Fill(dtCombo);
                {
                    var withBlock = cbSourceTree;
                    withBlock.DataSource = dtCombo.DefaultView;
                    withBlock.DisplayMember = "TreeTitle";
                    withBlock.ValueMember = "TreeCode";
                    withBlock.SelectedIndex = -1;
                }

                daCombo.SelectCommand.CommandText = "Select ProductCode,ProductCode+' '+ProductName As ProductName From Tbl_Products";
                dtCombo = new DataTable();
                daCombo.Fill(dtCombo);
                {
                    var withBlock1 = cbSourceProduct;
                    withBlock1.DataSource = dtCombo;
                    withBlock1.DisplayMember = "ProductName";
                    withBlock1.ValueMember = "ProductCode";
                    withBlock1.SelectedIndex = -1;
                }

                daCombo.SelectCommand.CommandText = "Select ProductCode,ProductCode+' '+ProductName As ProductName From Tbl_Products";
                dtCombo = new DataTable();
                daCombo.Fill(dtCombo);
                {
                    var withBlock2 = cbDestinationProduct;
                    withBlock2.DataSource = dtCombo;
                    withBlock2.DisplayMember = "ProductName";
                    withBlock2.ValueMember = "ProductCode";
                    withBlock2.SelectedIndex = -1;
                }

                txtTreeCode.Value = Module1.GetNewTreeCode();
            }
            catch (Exception objEx)
            {
                Logger.SaveError(Name + ".FormLoad", objEx.Message);
                MessageBox.Show("فراخوانی فرم کپی درخت با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                Close();
            }
        }

        private void SetDefualtTree()
        {
            DataView dvDefualtTree;
            dvDefualtTree = dsProductionPlanning.Tables["Tbl_ProductTree"].DefaultView;
            dvDefualtTree.RowFilter = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("ProductCode='", cbDestinationProduct.SelectedValue), "' And DefualtTree=1"));
            if (dvDefualtTree.Count > 0)
            {
                dvDefualtTree[0].BeginEdit();
                dvDefualtTree[0]["DefualtTree"] = 0;
                dvDefualtTree[0].EndEdit();
            }

            dvDefualtTree.RowFilter = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("ProductCode='", cbDestinationProduct.SelectedValue), "'"));
        }

        private void AddChildsDetail(string mSourceParentDetailCode, string mDestinationParentDetailCode, ref SqlTransaction mTrn)
        {
            var drChilds = dsProductionPlanning.Tables["Tbl_ProductTreeDetails"].Select("TreeCode = " + cbSourceTree.SelectedValue.ToString() + " And ParentDetailCode = '" + mSourceParentDetailCode + "'");
            foreach (DataRow r in drChilds)
            {
                // ثبت مشخصات جزء جاری
                var cm = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Insert Into Tbl_ProductTreeDetails(TreeCode,DetailCode,ParentDetailCode,DetailName,LevelNo,ParentQuantity,BuiltType,TechniqueMapsProperties," + "Weight,Height,Temperature,Volume,Physical1,Physical2,Physical3,ProductionTestUnit,StorePlace,StoreTestUnit,[Description]) " + "Values(" + txtTreeCode.Value.ToString() + ",'" + r["DetailCode"].ToString() + "','" + mDestinationParentDetailCode + "','", r["DetailName"]), "',"), r["LevelNo"]), ","), r["ParentQuantity"]), ","), r["BuiltType"]), ",'"), r["TechniqueMapsProperties"]), "','"), r["Weight"]), "','"), r["Height"]), "','"), r["Temperature"]), "','"), r["Volume"]), "','"), r["Physical1"]), "','"), r["Physical2"]), "','"), r["Physical3"]), "',"), r["ProductionTestUnit"]), ",'"), r["StorePlace"]), "',"), r["StoreTestUnit"]), ",'"), r["Description"]), "')")), mTrn.Connection);





                cm.Transaction = mTrn;
                cm.ExecuteNonQuery();

                // اضافه نمودن عملیات های جزء جاری
                AddDetailOperations(r["DetailCode"].ToString(), r["DetailCode"].ToString(), ref mTrn);
                // اضافه نمودن اجزاء زیر مجموعۀ جزء جاری
                AddChildsDetail(r["DetailCode"].ToString(), r["DetailCode"].ToString(), ref mTrn);
            }
        }

        private void AddDetailOperations(string mSourceDetailCode, string mDestinationDetailCode, ref SqlTransaction mTrn)
        {
            var drOperations = dsProductionPlanning.Tables["Tbl_ProductOPCs"].Select("TreeCode = " + cbSourceTree.SelectedValue.ToString() + " And DetailCode = '" + mSourceDetailCode + "'");
            foreach (DataRow r in drOperations)
            {
                var cm = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Insert Into Tbl_ProductOPCs(TreeCode,DetailCode,OperationCode,OperationTitle,HavePreOperation,HavePreItem,ExecutionMethod,OperationPriority,ActivityGroup,NatureCode,IsNotCalcWorking,IsParallelOperation)" + "Values(" + txtTreeCode.Value + "," + "'" + mDestinationDetailCode + "'," + "'", r["OperationCode"]), "',"), "'"), r["OperationTitle"]), "',"), r["HavePreOperation"]), ","), r["HavePreItem"]), ","), r["ExecutionMethod"]), ","), r["OperationPriority"]), ","), "'"), r["ActivityGroup"]), "',"), "'"), r["NatureCode"]), "',"), Interaction.IIf(Conversions.ToBoolean(r["IsNotCalcWorking"]), 1, 0)), ","), Interaction.IIf(Conversions.ToBoolean(r["IsParallelOperation"]), 1, 0)), ")")), mTrn.Connection);
                cm.Transaction = mTrn;
                cm.ExecuteNonQuery();
            }
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
                daProductTree.Update(dsChanges, "Tbl_ProductTree");
                daProductTree.Update(dsChanges, "Tbl_ProductTree");
                dsProductionPlanning.AcceptChanges();
            }

            dsChanges = null;
        }

        private void CreateDataAdapterCommands()
        {
            // -------------------- ایجاد دستورات جدول درخت محصول ----------------------
            // ایجاد دستور اضافه کردن رکورد جدید در جدول
            daProductTree.InsertCommand = new SqlCommand("Insert Into Tbl_ProductTree(TreeCode,ProductCode,TreeTitle,DefualtTree) Values(@TreeCode,@ProductCode,@TreeTitle,@DefualtTree)", Module1.cnProductionPlanning);
            {
                var withBlock = daProductTree.InsertCommand;
                withBlock.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode");
                withBlock.Parameters.Add("@ProductCode", SqlDbType.VarChar, 50, "ProductCode");
                withBlock.Parameters.Add("@TreeTitle", SqlDbType.VarChar, 50, "TreeTitle");
                withBlock.Parameters.Add("@DefualtTree", SqlDbType.Bit, default, "DefualtTree");
            }
            // ایجاد دستور اصلاح رکورد جاری در جدول
            daProductTree.UpdateCommand = new SqlCommand("Update Tbl_ProductTree Set DefualtTree=@DefualtTree Where TreeCode=@TreeCode", Module1.cnProductionPlanning);
            {
                var withBlock1 = daProductTree.UpdateCommand;
                withBlock1.Parameters.Add("@DefualtTree", SqlDbType.Bit, default, "DefualtTree");
                withBlock1.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode");
                withBlock1.Parameters[1].Direction = ParameterDirection.Input;
                withBlock1.Parameters[1].SourceVersion = DataRowVersion.Original;
            }

            // -------------------- ایجاد دستورات جدول اجزای درخت محصول ----------------------
            // ایجاد دستور اضافه کردن رکورد جدید در جدول
            // daProductTreeDetails.InsertCommand = New SqlCommand("Insert Into Tbl_ProductTreeDetails(TreeCode,DetailCode,ParentDetailCode,DetailName,LevelNo,ParentQuantity,BuiltType,TechniqueMapsProperties,Weight,Height,Temperature,Volume,Physical1,Physical2,Physical3,ProductionTestUnit,StorePlace,StoreTestUnit,Description) " & _
            // "Values(@TreeCode,@DetailCode,@ParentDetailCode,@DetailName,@LevelNo,@ParentQuantity,@BuiltType,@TechniqueMapsProperties,@Weight,@Height,@Temperature,@Volume,@Physical1,@Physical2,@Physical3,@ProductionTestUnit,@StorePlace,@StoreTestUnit,@Description)", cnProductionPlanning)
            // With daProductTreeDetails.InsertCommand
            // .Parameters.Add("@TreeCode", SqlDbType.Int, Nothing, "TreeCode")
            // .Parameters.Add("@DetailCode", SqlDbType.VarChar, 50, "DetailCode")
            // .Parameters.Add("@ParentDetailCode", SqlDbType.VarChar, 50, "ParentDetailCode")
            // .Parameters.Add("@DetailName", SqlDbType.VarChar, 100, "DetailName")
            // .Parameters.Add("@LevelNo", SqlDbType.SmallInt, 25, "LevelNo")
            // .Parameters.Add("@ParentQuantity", SqlDbType.Float, Nothing, "ParentQuantity")
            // .Parameters.Add("@BuiltType", SqlDbType.TinyInt, Nothing, "BuiltType")
            // .Parameters.Add("@TechniqueMapsProperties", SqlDbType.VarChar, 100, "TechniqueMapsProperties")
            // .Parameters.Add("@Weight", SqlDbType.Float, Nothing, "Weight")
            // .Parameters.Add("@Height", SqlDbType.Float, Nothing, "Height")
            // .Parameters.Add("@Temperature", SqlDbType.Float, Nothing, "Temperature")
            // .Parameters.Add("@Volume", SqlDbType.Float, Nothing, "Volume")
            // .Parameters.Add("@Physical1", SqlDbType.VarChar, 50, "Physical1")
            // .Parameters.Add("@Physical2", SqlDbType.VarChar, 50, "Physical2")
            // .Parameters.Add("@Physical3", SqlDbType.VarChar, 50, "Physical3")
            // .Parameters.Add("@ProductionTestUnit", SqlDbType.Int, Nothing, "ProductionTestUnit")
            // .Parameters.Add("@StorePlace", SqlDbType.Int, Nothing, "StorePlace")
            // .Parameters.Add("@StoreTestUnit", SqlDbType.Int, Nothing, "StoreTestUnit")
            // .Parameters.Add("@Description", SqlDbType.VarChar, 255, "Description")
            // End With
        }

        private bool ValidationForm()
        {
            if (cbSourceProduct.SelectedIndex == -1)
            {
                MessageBox.Show("محصول مبداء را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbSourceProduct.Focus();
                return false;
            }

            if (cbSourceTree.SelectedIndex == -1)
            {
                MessageBox.Show("درخت مبداء را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbSourceTree.Focus();
                return false;
            }

            if (cbDestinationProduct.SelectedIndex == -1)
            {
                MessageBox.Show("محصول مقصد را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbDestinationProduct.Focus();
                return false;
            }

            if (txtTreeCode.Value == 0m)
            {
                MessageBox.Show("کد درخت را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtTreeCode.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtTreeTitle.Text))
            {
                MessageBox.Show("عنوان درخت را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtTreeTitle.Focus();
                return false;
            }

            return true;
        }
    }
}