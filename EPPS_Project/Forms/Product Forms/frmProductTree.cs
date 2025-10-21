using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmProductTree
    {
        public frmProductTree()
        {
            InitializeComponent();
            _cmdSave.Name = "cmdSave";
            _cmdExit.Name = "cmdExit";
            _cmdDelete.Name = "cmdDelete";
            _chkDefualt.Name = "chkDefualt";
            _txtTitle.Name = "txtTitle";
        }

        private frmProductTreesList mListForm;
        private SqlDataAdapter daProductTree = new SqlDataAdapter();
        private DataRow CurrentRow; // برای نگهداری رکورد جاری
        private string mProductCode;
        //private short I;

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

        public string ProductCode
        {
            set
            {
                mProductCode = value;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdSave, 6);
            Module1.SetButtonsImage(cmdDelete, 9);
            Module1.SetButtonsImage(cmdExit, 14);
            FormLoad();
        }

        private void frmProduct_FormClosing(object sender, FormClosingEventArgs e)
        {
            dsProductionPlanning.RejectChanges();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            string mMessage = ":با حذف درخت محصول، مشخصات" + Constants.vbCrLf + "اجزاء درخت و عملیات های مرتبط با آن حذف خواهند شد" + Constants.vbCrLf + "آیا برای حذف درخت مطمئن هستید؟";
            if (MessageBox.Show(mMessage, Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading) == DialogResult.Yes)
            {
                string mDeleteCode = GetDeleteConfimCode();
                string mInputedCode = Interaction.InputBox("کاربر گرامی، جهت تایید حذف، کد زیر را وارد نمایید" + Constants.vbCrLf + Constants.vbCrLf + mDeleteCode, Module1.MessagesTitle);
                if (!mInputedCode.Equals(mDeleteCode))
                {
                    if (!string.IsNullOrEmpty(mInputedCode))
                    {
                        MessageBox.Show("کد وارد شده صحیح نیست", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    }

                    return;
                }

                SqlTransaction trnDelete = null;
                try
                {
                    if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                        Module1.cnProductionPlanning.Open();
                    trnDelete = Module1.cnProductionPlanning.BeginTransaction();

                    // کنترل وجود تولید واقعی مرتبط با این درخت
                    var cmDelete = new SqlCommand("Select Count(*) From Tbl_RealProduction Where TreeCode = " + CurrentRow["TreeCode"].ToString(), Module1.cnProductionPlanning);
                    cmDelete.Transaction = trnDelete;
                    if (Conversions.ToInteger(cmDelete.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("بدلیل وجود تولید واقعی مرتبط با این درخت امکان حذف وجود ندارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        trnDelete.Commit();
                        return;
                    }
                    else
                    {
                        // کنترل وجود برنامه ریزی مرتبط با این درخت
                        cmDelete.CommandText = "Select Count(*) From Tbl_Planning Where TreeCode = " + CurrentRow["TreeCode"].ToString();
                        if (Conversions.ToInteger(cmDelete.ExecuteScalar()) > 0)
                        {
                            MessageBox.Show("بدلیل وجود برنامه ریزی مرتبط با این درخت امکان حذف وجود ندارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                            trnDelete.Commit();
                            return;
                        }
                        else
                        {
                            // کنترل وجود بچ تولید مرتبط با این درخت
                            cmDelete.CommandText = "Select Count(*) From Tbl_ProductionBatchs Where ProductTreeCode = " + CurrentRow["TreeCode"].ToString();
                            if (Conversions.ToInteger(cmDelete.ExecuteScalar()) > 0)
                            {
                                MessageBox.Show("بدلیل وجود بچ تولید مرتبط با این درخت امکان حذف وجود ندارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                                trnDelete.Commit();
                                return;
                            }
                            else
                            {
                                // حذف مشخصات ارتباط همزمانی عملیات ها
                                cmDelete.CommandText = "Delete From Tbl_MatchedOperations Where TreeCode = " + CurrentRow["TreeCode"].ToString();
                                cmDelete.ExecuteNonQuery();

                                // حذف مشخصات مواد وارده به عملیات ها
                                cmDelete.CommandText = "Delete From Tbl_OperationMaterials Where TreeCode = " + CurrentRow["TreeCode"].ToString();
                                cmDelete.ExecuteNonQuery();

                                // حذف مشخصات روابط پیشنیازی عملیات ها
                                cmDelete.CommandText = "Delete From Tbl_PreOperations Where TreeCode = " + CurrentRow["TreeCode"].ToString();
                                cmDelete.ExecuteNonQuery();

                                // حذف مشخصات پیمانکاری عملیات ها
                                cmDelete.CommandText = "Delete From Tbl_ContractorOperations Where TreeCode = " + CurrentRow["TreeCode"].ToString();
                                cmDelete.ExecuteNonQuery();

                                // حذف مشخصات ماشین های انجام دهنده و اپراتوری عملیات ها
                                cmDelete.CommandText = "Delete From Tbl_ProductOPCsExecutorMachines Where TreeCode = " + CurrentRow["TreeCode"].ToString();
                                cmDelete.ExecuteNonQuery();

                                // حذف مشخصات مسیرهای شبکۀ عملیات ها
                                cmDelete.CommandText = "Delete From Tbl_OperationNetworkPaths Where TreeCode = " + CurrentRow["TreeCode"].ToString();
                                cmDelete.ExecuteNonQuery();

                                // حذف مشخصات عملیات ها
                                cmDelete.CommandText = "Delete From Tbl_ProductOPCs Where TreeCode = " + CurrentRow["TreeCode"].ToString();
                                cmDelete.ExecuteNonQuery();

                                // حذف مشخصات اجزاء درخت
                                cmDelete.CommandText = "Delete From Tbl_ProductTreeDetails Where TreeCode = " + CurrentRow["TreeCode"].ToString();
                                cmDelete.ExecuteNonQuery();
                                daProductTree.DeleteCommand.Transaction = trnDelete;
                                CurrentRow.Delete();
                                SaveChanges();
                                trnDelete.Commit();
                            }
                        }
                    }

                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (Exception objEx)
                {
                    dsProductionPlanning.RejectChanges();
                    trnDelete.Rollback();
                    Logger.SaveError(Name + ".CmdDelete_Click", objEx.Message);
                    MessageBox.Show("حذف درخت با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
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
            if (!ValidationForm())
            {
                return;
            }

            switch (ListForm.FormMode)
            {
                case (int)Module1.FormModeEnum.INSERT_MODE:
                    {
                        DataRow drInsert;
                        SqlTransaction trnInsert = null;
                        var cmInsertFirstNode = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Insert Into Tbl_ProductTreeDetails(TreeCode,DetailCode,ParentDetailCode,DetailName,LevelNo,ParentQuantity,BuiltType,TechniqueMapsProperties," + "Weight,Height,Temperature,Volume,Physical1,Physical2,Physical3,ProductionTestUnit,StorePlace,StoreTestUnit,[Description]) " + "Values(" + txtCode.Value + ",'", cbProductName.SelectedValue), "',0,'"), cbProductName.Text), "',0,1,1,'',0,0,0,0,0,0,0,0,0,0,''"), ")")), Module1.cnProductionPlanning);

                        try
                        {
                            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                                Module1.cnProductionPlanning.Open();
                            trnInsert = Module1.cnProductionPlanning.BeginTransaction();
                            daProductTree.InsertCommand.Transaction = trnInsert;
                            daProductTree.UpdateCommand.Transaction = trnInsert;
                            cmInsertFirstNode.Transaction = trnInsert;
                            if (chkDefualt.Checked)
                            {
                                SetDefualtTree();
                            }

                            // اضافه کردن رکورد جدید به جدول در دیتاست
                            drInsert = dsProductionPlanning.Tables["Tbl_ProductTree"].NewRow();
                            drInsert["TreeCode"] = txtCode.Value;
                            drInsert["ProductCode"] = cbProductName.SelectedValue;
                            drInsert["TreeTitle"] = txtTitle.Text;
                            drInsert["DefualtTree"] = chkDefualt.Checked;
                            drInsert["ProductCode1"] = cbProductName.SelectedValue;
                            drInsert["ProductName"] = cbProductName.Text;
                            dsProductionPlanning.Tables["Tbl_ProductTree"].Rows.Add(drInsert);
                            SaveChanges();
                            // ایجاد اولین نود درخت محصول
                            cmInsertFirstNode.ExecuteNonQuery();
                            trnInsert.Commit();
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            dsProductionPlanning.RejectChanges();
                            trnInsert.Rollback();
                            Logger.SaveError(Name + ".CmdSave_Click", objEx.Message);
                            MessageBox.Show("ثبت درخت محصول جدید با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        try
                        {
                            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                                Module1.cnProductionPlanning.Open();
                            trnUpdate = Module1.cnProductionPlanning.BeginTransaction();
                            daProductTree.UpdateCommand.Transaction = trnUpdate;
                            if (chkDefualt.Checked)
                            {
                                SetDefualtTree();
                            }

                            // تغییر در اطلاعات کلی درخت محصول
                            CurrentRow.BeginEdit();
                            CurrentRow["TreeCode"] = txtCode.Value;
                            CurrentRow["TreeTitle"] = txtTitle.Text;
                            CurrentRow["DefualtTree"] = chkDefualt.Checked;
                            CurrentRow.EndEdit();
                            SaveChanges();
                            trnUpdate.Commit();
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            dsProductionPlanning.RejectChanges();
                            trnUpdate.Rollback();
                            Logger.SaveError(Name + ".CmdSave_Click", objEx.Message);
                            MessageBox.Show("ثبت تغییرات درخت محصول با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
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

        private void Controls_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (ReferenceEquals(sender, txtCode))
            {
                string ValidStr;
                ValidStr = "0123456789";
                if (e.KeyChar > '\u001a')
                {
                    if (Strings.InStr(ValidStr, Conversions.ToString(e.KeyChar)) == 0)
                    {
                        e.KeyChar = Conversions.ToChar("");
                    }
                }
            }

            if (Strings.Asc(e.KeyChar) == (int)Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void chkDefualt_Click(object sender, EventArgs e)
        {
            chkDefualt.Checked = !chkDefualt.Checked;
        }

        private void FormLoad()
        {
            try
            {
                CreateDataAdapterCommands();
                {
                    var withBlock = cbProductName;
                    withBlock.DataSource = dsProductionPlanning.Tables["Tbl_Products"];
                    withBlock.DisplayMember = "ProductName";
                    withBlock.ValueMember = "ProductCode";
                    if (mProductCode is object)
                    {
                        withBlock.SelectedValue = mProductCode;
                    }
                }

                if (cbProductName.SelectedIndex == -1 && cbProductName.Items.Count > 0)
                {
                    cbProductName.SelectedIndex = 0;
                }

                switch (ListForm.FormMode)
                {
                    case (int)Module1.FormModeEnum.INSERT_MODE: // در صورتیکه فرم برای ایجاد رکورد جدید فراخوانی شده باشد این قسمت اجرا خواهد شد
                        {
                            txtCode.Value = Module1.GetNewTreeCode();
                            txtCode.Focus();
                            break;
                        }

                    case (int)Module1.FormModeEnum.EDIT_MODE:
                    case (int)Module1.FormModeEnum.DELETE_MODE: // در صورتیکه فرم برای ویرایش یا حذف رکورد جاری فراخوانی شده باشد این قسمت اجرا خواهد شد
                        {
                            CurrentRow = ListForm.GetRow();
                            // پر کردن کنترل فرم با مقدار رکورد جاری
                            txtCode.Value = Conversions.ToDecimal(CurrentRow["TreeCode"]);
                            txtTitle.Text = Conversions.ToString(CurrentRow["TreeTitle"]);
                            chkDefualt.Checked = Conversions.ToBoolean(CurrentRow["DefualtTree"]);
                            cbProductName.SelectedValue = CurrentRow["ProductCode"];
                            switch (ListForm.FormMode) // در صورتیکه فرم برای ویرایش(اصلاح) رکورد جاری فراخوانی شده باشد این قسمت اجرا خواهد شد
                            {
                                case (int)Module1.FormModeEnum.EDIT_MODE:
                                    {
                                        txtCode.Focus();
                                        break;
                                    }

                                case (int)Module1.FormModeEnum.DELETE_MODE: // در صورتیکه فرم برای حذف رکورد جاری فراخوانی شده باشد این قسمت اجرا خواهد شد
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

        private void SetDefualtTree()
        {
            DataView dvDefualtTree;
            dvDefualtTree = dsProductionPlanning.Tables["Tbl_ProductTree"].DefaultView;
            dvDefualtTree.RowFilter = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("ProductCode='", cbProductName.SelectedValue), "' And DefualtTree=1"));
            if (dvDefualtTree.Count > 0)
            {
                dvDefualtTree[0].BeginEdit();
                dvDefualtTree[0]["DefualtTree"] = 0;
                dvDefualtTree[0].EndEdit();
            }

            dvDefualtTree.RowFilter = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("ProductCode='", cbProductName.SelectedValue), "'"));
        }

        private string GetDeleteConfimCode()
        {
            var mRnd = new Random();
            string mGeneratedCode = Constants.vbNullString;
            for (int I = 1; I <= 5; I++)
            {
                if (mRnd.Next(1, 100) % 2 == 0)
                {
                    mGeneratedCode += Conversions.ToString((char)mRnd.Next(65, 90));
                }
                else
                {
                    mGeneratedCode += mRnd.Next(0, 9).ToString();
                }
            }

            return mGeneratedCode;
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
                daProductTree.Update(dsChanges, "Tbl_ProductTree");
                dsProductionPlanning.AcceptChanges();
            }
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
            daProductTree.UpdateCommand = new SqlCommand("Update Tbl_ProductTree Set TreeCode=@CurrentTreeCode,TreeTitle=@TreeTitle,DefualtTree=@DefualtTree Where TreeCode=@OldTreeCode And ProductCode=@ProductCode", Module1.cnProductionPlanning);
            {
                var withBlock1 = daProductTree.UpdateCommand;
                withBlock1.Parameters.Add("@CurrentTreeCode", SqlDbType.Int, default, "TreeCode").SourceVersion = DataRowVersion.Current;
                withBlock1.Parameters[0].Direction = ParameterDirection.Input;
                withBlock1.Parameters[0].SourceVersion = DataRowVersion.Current;
                withBlock1.Parameters.Add("@ProductCode", SqlDbType.VarChar, 50, "ProductCode");
                withBlock1.Parameters[1].Direction = ParameterDirection.Input;
                withBlock1.Parameters[1].SourceVersion = DataRowVersion.Original;
                withBlock1.Parameters.Add("@TreeTitle", SqlDbType.VarChar, 50, "TreeTitle");
                withBlock1.Parameters.Add("@DefualtTree", SqlDbType.Bit, default, "DefualtTree");
                withBlock1.Parameters.Add("@OldTreeCode", SqlDbType.Int, default, "TreeCode").SourceVersion = DataRowVersion.Original;
            }
            // ایجاد دستور حذف رکورد جاری در جدول
            daProductTree.DeleteCommand = new SqlCommand("Delete From Tbl_ProductTree Where TreeCode=@TreeCode", Module1.cnProductionPlanning);
            {
                var withBlock2 = daProductTree.DeleteCommand;
                withBlock2.Parameters.Add("@TreeCode", SqlDbType.Int, default, "TreeCode").SourceVersion = DataRowVersion.Original;
            }
        }

        private bool ValidationForm()
        {
            if (cbProductName.SelectedValue is null)
            {
                MessageBox.Show("نام محصول را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbProductName.Focus();
                return false;
            }

            if (txtCode.Value == 0m)
            {
                MessageBox.Show("کد درخت محصول را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtCode.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                MessageBox.Show("عنوان درخت محصول را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtTitle.Focus();
                return false;
            }

            return true;
        }
    }
}