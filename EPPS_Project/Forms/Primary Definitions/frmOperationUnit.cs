using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmOperationUnit
    {
        public frmOperationUnit()
        {
            InitializeComponent();
            _cmdExit.Name = "cmdExit";
            _cmdDelete.Name = "cmdDelete";
            _cmdSave.Name = "cmdSave";
        }

        private DataSet mdsOperationUnit;
        private SqlDataAdapter mdaOperationUnit = new SqlDataAdapter();
        private DataRow mCurrentRow;
        private short mFormMode;
        private string mOperationCode;
        private short I;

        public void SetFormMode(short value)
        {
            mFormMode = value;
        }

        public DataSet dsOperationUnit
        {
            set
            {
                mdsOperationUnit = value;
            }
        }

        public DataRow CurrentRow
        {
            set
            {
                mCurrentRow = value;
            }
        }

        public string OperationCode
        {
            set
            {
                mOperationCode = value;
            }
        }

        private void frmOperationUnit_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdSave, 6);
            Module1.SetButtonsImage(cmdDelete, 9);
            Module1.SetButtonsImage(cmdExit, 14);
            FormLoad();
        }

        private void frmOperationUnit_FormClosing(object sender, FormClosingEventArgs e)
        {
            mdsOperationUnit.RejectChanges();
            short UpperBound = (short)(mdsOperationUnit.Tables.Count - 1);
            short LowerBound = (short)(mdsOperationUnit.Tables.Count - 2);
            var loopTo = LowerBound;
            for (I = UpperBound; I >= loopTo; I += -1)
            {
                mdsOperationUnit.Tables[I].Dispose();
                mdsOperationUnit.Tables.RemoveAt(I);
            }

            mCurrentRow = null;
            mdaOperationUnit.Dispose();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("مشخصات واحد عملیات را حذف می کنید", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading, false) == DialogResult.Yes)
            {
                SqlTransaction trnDelete = null;
                try
                {
                    if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                        Module1.cnProductionPlanning.Open();
                    trnDelete = Module1.cnProductionPlanning.BeginTransaction();
                    mdaOperationUnit.DeleteCommand.Transaction = trnDelete;
                    mCurrentRow.Delete();
                    SaveChanges();
                    trnDelete.Commit();
                    Close();
                }
                catch (InvalidConstraintException ObjCnstEx)
                {
                    Logger.LogException("cmdDelete_Click", ObjCnstEx);
                    mdsOperationUnit.RejectChanges();
                    trnDelete.Rollback();
                    MessageBox.Show("اشکال در حذف رکورد، برای حذف رکورد باید رکورد(های) مرتبط با آن حذف شوند", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                }
                catch (Exception objEx)
                {
                    mdsOperationUnit.RejectChanges();
                    trnDelete.Rollback();
                    Logger.SaveError(Name + ".cmdDelete_Click", objEx.Message);
                    string ErrorMsg = "اشکال در حذف رکورد، رکورد حذف نشد";
                    MessageBox.Show(ErrorMsg, Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                }
                finally
                {
                    trnDelete.Dispose();
                    if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                        Module1.cnProductionPlanning.Close();
                }
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!FormValidation())
            {
                return;
            }

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
                            mdaOperationUnit.InsertCommand.Transaction = trnInsert;
                            drInsert = mdsOperationUnit.Tables["Tbl_MeasurementUnit1"].NewRow();
                            drInsert["OperationCode"] = cbOperation.SelectedValue;
                            drInsert["UnitCode"] = cbUnit.SelectedValue;
                            drInsert["UnitTitle"] = cbUnit.Text;
                            drInsert["UnitIndex"] = txtConversionFactor.Value;
                            mdsOperationUnit.Tables["Tbl_MeasurementUnit1"].Rows.Add(drInsert);
                            SaveChanges();
                            trnInsert.Commit();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            mdsOperationUnit.RejectChanges();
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
                        try
                        {
                            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                                Module1.cnProductionPlanning.Open();
                            trnUpdate = Module1.cnProductionPlanning.BeginTransaction();
                            mdaOperationUnit.UpdateCommand.Transaction = trnUpdate;
                            mCurrentRow.BeginEdit();
                            mCurrentRow["OperationCode"] = cbOperation.SelectedValue;
                            mCurrentRow["UnitCode"] = cbUnit.SelectedValue;
                            mCurrentRow["UnitTitle"] = cbUnit.Text;
                            mCurrentRow["UnitIndex"] = txtConversionFactor.Value;
                            mCurrentRow.EndEdit();
                            SaveChanges();
                            trnUpdate.Commit();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            mCurrentRow.CancelEdit();
                            trnUpdate.Rollback();
                            MessageBox.Show("اشکال در اصلاح رکورد، تغییرات ثبت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            MessageBox.Show(objEx.Message);
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

        private void cmdCancel_Click(object sender, EventArgs e)
        {
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
                    var withBlock = cbOperation;
                    withBlock.DataSource = mdsOperationUnit.Tables["TblOperations"];
                    withBlock.DisplayMember = "OperationTitle";
                    withBlock.ValueMember = "OperationCode";
                }

                {
                    var withBlock1 = cbUnit;
                    withBlock1.DataSource = mdsOperationUnit.Tables["Tbl_TestUnits"];
                    withBlock1.DisplayMember = "Title";
                    withBlock1.ValueMember = "Code";
                }

                if (!string.IsNullOrEmpty(mOperationCode))
                {
                    cbOperation.SelectedValue = mOperationCode;
                    cbOperation.Enabled = false;
                }

                switch (mFormMode)
                {
                    case (short)Module1.FormModeEnum.INSERT_MODE:
                        {
                            cbOperation.Focus();
                            break;
                        }

                    case (short)Module1.FormModeEnum.EDIT_MODE:
                    case (short)Module1.FormModeEnum.DELETE_MODE:
                        {
                            // پر کردن کنترل فرم با مقدار رکورد جاری
                            cbOperation.SelectedValue = mCurrentRow["OperationCode"];
                            cbUnit.SelectedValue = mCurrentRow["UnitCode"];
                            txtConversionFactor.Value = Conversions.ToDecimal(mCurrentRow["UnitIndex"]);
                            switch (mFormMode)
                            {
                                case (short)Module1.FormModeEnum.EDIT_MODE:
                                    {
                                        cbOperation.Focus();
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
            dsChanges = mdsOperationUnit.GetChanges();
            if (dsChanges.HasErrors)
            {
                mdsOperationUnit.RejectChanges();
            }
            else
            {
                mdaOperationUnit.Update(dsChanges, "Tbl_MeasurementUnit1");
                mdsOperationUnit.AcceptChanges();
            }

            dsChanges = null;
        }

        private void CreateDataAdapterCommands()
        {
            // ایجاد دستور اضافه کردن رکورد جدید در جدول
            mdaOperationUnit.InsertCommand = new SqlCommand("Insert Into Tbl_Operation_Measurement_UnitIndex(OperationCode,UnitCode,UnitIndex) Values(@OperationCode,@UnitCode,@UnitIndex)", Module1.cnProductionPlanning);
            {
                var withBlock = mdaOperationUnit.InsertCommand;
                withBlock.Parameters.Add("@OperationCode", SqlDbType.VarChar, 50, "OperationCode");
                withBlock.Parameters.Add("@UnitCode", SqlDbType.Int, default, "UnitCode");
                withBlock.Parameters.Add("@UnitIndex", SqlDbType.Real, default, "UnitIndex");
            }
            // ایجاد دستور اصلاح رکورد جاری در جدول
            mdaOperationUnit.UpdateCommand = new SqlCommand("Update Tbl_Operation_Measurement_UnitIndex Set OperationCode=@NewOperationCode,UnitCode=@NewUnitCode," + "UnitIndex=@UnitIndex Where OperationCode=@OldOperationCode " + "And UnitCode=@OldUnitCode", Module1.cnProductionPlanning);

            {
                var withBlock1 = mdaOperationUnit.UpdateCommand;
                withBlock1.Parameters.Add("@NewOperationCode", SqlDbType.VarChar, 50, "OperationCode").SourceVersion = DataRowVersion.Current;
                withBlock1.Parameters.Add("@NewUnitCode", SqlDbType.Int, default, "UnitCode").SourceVersion = DataRowVersion.Current;
                withBlock1.Parameters.Add("@UnitIndex", SqlDbType.Real, default, "UnitIndex");
                withBlock1.Parameters.Add("@OldOperationCode", SqlDbType.VarChar, 50, "OperationCode").SourceVersion = DataRowVersion.Original;
                withBlock1.Parameters.Add("@OldUnitCode", SqlDbType.Int, default, "UnitCode").SourceVersion = DataRowVersion.Original;
            }
            // ایجاد دستور حذف رکورد جاری در جدول
            mdaOperationUnit.DeleteCommand = new SqlCommand("Delete From Tbl_Operation_Measurement_UnitIndex Where OperationCode=@OperationCode And UnitCode=@UnitCode", Module1.cnProductionPlanning);
            {
                var withBlock2 = mdaOperationUnit.DeleteCommand;
                withBlock2.Parameters.Add("@OperationCode", SqlDbType.VarChar, 50, "OperationCode").SourceVersion = DataRowVersion.Original;
                withBlock2.Parameters.Add("@UnitCode", SqlDbType.Int, default, "UnitCode").SourceVersion = DataRowVersion.Original;
            }
        }

        private bool FormValidation()
        {
            if (cbOperation.SelectedIndex == -1)
            {
                MessageBox.Show("کد عملیات را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbOperation.Focus();
                return default;
            }

            if (cbUnit.SelectedIndex == -1)
            {
                MessageBox.Show("نام واحد را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbUnit.Focus();
                return default;
            }

            return true;
        }
    }
}