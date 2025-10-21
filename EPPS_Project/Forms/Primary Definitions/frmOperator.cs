using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmOperator
    {
        public frmOperator()
        {
            InitializeComponent();
            _cmdSave.Name = "cmdSave";
            _cmdExit.Name = "cmdExit";
            _txtPersonnelCode.Name = "txtPersonnelCode";
            _txtName.Name = "txtName";
            _cmdDelete.Name = "cmdDelete";
            _cbProductionPart.Name = "cbProductionPart";
            _cmdDeleteWorkPeriod.Name = "cmdDeleteWorkPeriod";
            _cmdEditWorkPeriod.Name = "cmdEditWorkPeriod";
            _cmdAddWorkPeriod.Name = "cmdAddWorkPeriod";
            _dgWorkPeriods.Name = "dgWorkPeriods";
        }

        private frmRecordsLists mListForm;
        private SqlDataAdapter daOperator = new SqlDataAdapter();
        private SqlDataAdapter daWorkPeriod = new SqlDataAdapter();
        private DataRow mCurrentRow; // برای نگهداری رکورد جاری
        //private short I;

        public frmRecordsLists ListForm
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

        private void Form1_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdSave, 6);
            Module1.SetButtonsImage(cmdDelete, 9);
            Module1.SetButtonsImage(cmdExit, 14);
            Module1.SetButtonsImage(cmdAddWorkPeriod, 0);
            Module1.SetButtonsImage(cmdEditWorkPeriod, 2);
            Module1.SetButtonsImage(cmdDeleteWorkPeriod, 1);
            FormLoad();
        }

        private void frmProduct_FormClosing(object sender, FormClosingEventArgs e)
        {
            dsProductionPlanning.RejectChanges();
            for (int I = dsProductionPlanning.Tables.Count - 1; I >= 1; I -= 1)
            {
                dsProductionPlanning.Tables[I].Dispose();
                dsProductionPlanning.Tables.RemoveAt(I);
            }

            daOperator.Dispose();
            daWorkPeriod.Dispose();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var cm = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Select Count(*) From Tbl_ProductionOperators Where OperatorCode = '", mCurrentRow["OperatorCode"]), "'")), Module1.cnProductionPlanning);
                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();
                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(cm.ExecuteScalar().ToString(), 0, false)))
                {
                    MessageBox.Show("براي اپراتور مورد نظر رکورد توليد وجود دارد و مجاز به حذف نمي باشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.SaveError(Name + ".cmdDelete_Click", ex.Message);
                MessageBox.Show("حذف اپراتور با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                return;
            }
            finally
            {
                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                    Module1.cnProductionPlanning.Close();
            }

            if (MessageBox.Show("اپراتور را حذف می کنید", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading, false) == DialogResult.Yes)
            {
                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();
                var trnDelete = Module1.cnProductionPlanning.BeginTransaction();
                try
                {
                    daOperator.DeleteCommand.Transaction = trnDelete;
                    daWorkPeriod.DeleteCommand.Transaction = trnDelete;
                    for (int I = dsProductionPlanning.Tables["Tbl_OperatorWorkPeriods"].Rows.Count - 1; I >= 0; I -= 1)
                        dsProductionPlanning.Tables["Tbl_OperatorWorkPeriods"].Rows[I].Delete();
                    mCurrentRow.Delete();
                    SaveChanges();
                    trnDelete.Commit();
                    dsProductionPlanning.AcceptChanges();
                    Close();
                }
                catch (InvalidConstraintException ObjCnstEx)
                {
                    Logger.LogException("cmdDelete_Click", ObjCnstEx);
                    dsProductionPlanning.RejectChanges();
                    trnDelete.Rollback();
                    MessageBox.Show("اشکال در حذف رکورد، برای حذف رکورد باید رکورد(های) مرتبط با آن حذف شوند", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                }
                catch (Exception objEx)
                {
                    dsProductionPlanning.RejectChanges();
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
            if (!ValidationForm())
            {
                return;
            }

            int mStatus = GetWorkStatus();
            switch (ListForm.FormMode)
            {
                case (int)Module1.FormModeEnum.INSERT_MODE:
                    {
                        if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                            Module1.cnProductionPlanning.Open();
                        var trnInsert = Module1.cnProductionPlanning.BeginTransaction();
                        try
                        {
                            daOperator.InsertCommand.Transaction = trnInsert;
                            daWorkPeriod.InsertCommand.Transaction = trnInsert;
                            daWorkPeriod.UpdateCommand.Transaction = trnInsert;
                            daWorkPeriod.DeleteCommand.Transaction = trnInsert;

                            // اضافه کردن رکورد جدید به جدول در دیتاست
                            mCurrentRow = dsProductionPlanning.Tables["Tbl_Operators"].NewRow();
                            mCurrentRow["OperatorCode"] = txtPersonnelCode.Text;
                            mCurrentRow["OperatorName"] = txtName.Text;
                            mCurrentRow["ProductionPart"] = cbProductionPart.SelectedValue;
                            mCurrentRow["WorkingStatus"] = mStatus;
                            mCurrentRow["WorkingStatusTitle"] = Interaction.IIf(mStatus == 1, "در حال کار", "پایان کار");
                            dsProductionPlanning.Tables["Tbl_Operators"].Rows.Add(mCurrentRow);
                            for (int I = 0, loopTo = dsProductionPlanning.Tables["Tbl_OperatorWorkPeriods"].Rows.Count - 1; I <= loopTo; I++)
                            {
                                {
                                    var withBlock = dsProductionPlanning.Tables["Tbl_OperatorWorkPeriods"];
                                    if (withBlock.Rows[I].RowState != DataRowState.Deleted)
                                    {
                                        withBlock.Rows[I].BeginEdit();
                                        withBlock.Rows[I]["OperatorCode"] = txtPersonnelCode.Text;
                                        withBlock.Rows[I].EndEdit();
                                    }
                                }
                            }

                            SaveChanges();
                            trnInsert.Commit();
                            dsProductionPlanning.AcceptChanges();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            dsProductionPlanning.RejectChanges();
                            trnInsert.Rollback();
                            Logger.SaveError(Name + ".cmdSave_Click", objEx.Message);
                            MessageBox.Show("ثبت اپراتور جدید با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        }
                        finally
                        {
                            mCurrentRow = null;
                            trnInsert.Dispose();
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                        }

                        break;
                    }

                case (int)Module1.FormModeEnum.EDIT_MODE:
                    {
                        if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                            Module1.cnProductionPlanning.Open();
                        var trnUpdate = Module1.cnProductionPlanning.BeginTransaction();
                        try
                        {
                            daOperator.UpdateCommand.Transaction = trnUpdate;
                            daWorkPeriod.InsertCommand.Transaction = trnUpdate;
                            daWorkPeriod.UpdateCommand.Transaction = trnUpdate;
                            daWorkPeriod.DeleteCommand.Transaction = trnUpdate;
                            mCurrentRow.BeginEdit();
                            mCurrentRow["OperatorCode"] = txtPersonnelCode.Text;
                            mCurrentRow["OperatorName"] = txtName.Text;
                            mCurrentRow["ProductionPart"] = cbProductionPart.SelectedValue;
                            mCurrentRow["WorkingStatus"] = mStatus;
                            mCurrentRow["WorkingStatusTitle"] = Interaction.IIf(mStatus == 1, "در حال کار", "پایان کار");
                            mCurrentRow.EndEdit();
                            SaveChanges();
                            var cmUpdate = new SqlCommand("Delete From Tbl_OperatorWorkPeriods Where OperatorCode = '" + txtPersonnelCode.Text + "'", Module1.cnProductionPlanning);
                            cmUpdate.Transaction = trnUpdate;
                            cmUpdate.ExecuteNonQuery();
                            for (int I = 0, loopTo1 = dsProductionPlanning.Tables["Tbl_OperatorWorkPeriods"].Rows.Count - 1; I <= loopTo1; I++)
                            {
                                {
                                    var withBlock1 = dsProductionPlanning.Tables["Tbl_OperatorWorkPeriods"];
                                    if (withBlock1.Rows[I].RowState != DataRowState.Deleted)
                                    {
                                        cmUpdate.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Insert Into Tbl_OperatorWorkPeriods(OperatorCode,StartDate,EndDate,Description) Values('" + txtPersonnelCode.Text + "','", withBlock1.Rows[I]["StartDate"]), "','"), withBlock1.Rows[I]["EndDate"]), "','"), withBlock1.Rows[I]["Description"]), "')"));
                                        cmUpdate.ExecuteNonQuery();
                                    }
                                }
                            }

                            trnUpdate.Commit();
                            dsProductionPlanning.AcceptChanges();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            mCurrentRow.CancelEdit();
                            trnUpdate.Rollback();
                            Logger.SaveError(Name + ".cmdSave_Click", objEx.Message);
                            MessageBox.Show("ثبت تغییرات با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
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
            Close();
        }

        private void Controls_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Strings.Asc(e.KeyChar) == (int)Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
            else if (ReferenceEquals(sender, txtPersonnelCode) && Strings.Asc(e.KeyChar) != (int)Keys.Back)
            {
                string ValidStr = "0123456789";
                if (Strings.InStr(ValidStr, Conversions.ToString(e.KeyChar)) == 0)
                {
                    e.KeyChar = Conversions.ToChar("");
                }
            }
        }

        private void FormLoad()
        {
            try
            {
                CreateDataAdapterCommands();
                {
                    var withBlock = cbProductionPart;
                    withBlock.DataSource = dsProductionPlanning.Tables["Tbl_Natures"].DefaultView;
                    withBlock.DisplayMember = "NatureTitle";
                    withBlock.ValueMember = "NatureCode";
                }

                {
                    var withBlock1 = dgWorkPeriods;
                    withBlock1.Columns.Clear();
                    withBlock1.DataSource = dsProductionPlanning;
                    withBlock1.DataMember = dsProductionPlanning.Tables["Tbl_OperatorWorkPeriods"].TableName;
                    withBlock1.Columns[0].Visible = false;
                    withBlock1.Columns[1].HeaderText = "تاریخ شروع کار";
                    withBlock1.Columns[2].HeaderText = "تاریخ پایان کار";
                    withBlock1.Columns[3].HeaderText = "توضیحات";
                    withBlock1.Columns[1].Resizable = DataGridViewTriState.False;
                    withBlock1.Columns[2].Resizable = DataGridViewTriState.False;
                    withBlock1.Columns[3].Resizable = DataGridViewTriState.False;
                    withBlock1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                    withBlock1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                    withBlock1.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
                    withBlock1.Columns[1].Width = 95;
                    withBlock1.Columns[2].Width = 85;
                    withBlock1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                switch (ListForm.FormMode)
                {
                    case (int)Module1.FormModeEnum.INSERT_MODE:
                        {
                            txtPersonnelCode.Focus();
                            break;
                        }

                    case (int)Module1.FormModeEnum.EDIT_MODE:
                    case (int)Module1.FormModeEnum.DELETE_MODE:
                        {
                            mCurrentRow = ListForm.GetRow();
                            // پر کردن کنترل فرم با مقدار رکورد جاری
                            txtPersonnelCode.Text = Conversions.ToString(mCurrentRow["OperatorCode"]);
                            txtName.Text = Conversions.ToString(mCurrentRow["OperatorName"]);
                            cbProductionPart.SelectedValue = mCurrentRow["ProductionPart"];
                            switch (mCurrentRow["WorkingStatus"])
                            {
                                case 1:
                                    {
                                        GroupBox2.Text = "وضعیت شغلی(در حال کار)";
                                        break;
                                    }

                                case 2:
                                    {
                                        GroupBox2.Text = "وضعیت شغلی(پایان کار)";
                                        break;
                                    }
                            }

                            switch (ListForm.FormMode)
                            {
                                case (int)Module1.FormModeEnum.EDIT_MODE:
                                    {
                                        txtPersonnelCode.Focus();
                                        break;
                                    }

                                case (int)Module1.FormModeEnum.DELETE_MODE:
                                    {
                                        cmdAddWorkPeriod.Enabled = false;
                                        cmdEditWorkPeriod.Enabled = false;
                                        cmdDeleteWorkPeriod.Enabled = false;
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
                MessageBox.Show("در فراخوانی فرم با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private int GetWorkStatus()
        {
            int mStatus;
            dsProductionPlanning.Tables["Tbl_OperatorWorkPeriods"].DefaultView.Sort = "StartDate Desc";
            if (dsProductionPlanning.Tables["Tbl_OperatorWorkPeriods"].DefaultView[0]["EndDate"].ToString().Equals("0"))
            {
                mStatus = 1;
            }
            else
            {
                mStatus = 2;
            }

            return mStatus;
        }

        private void SaveChanges()
        {
            DataSet dsChanges;
            dsChanges = dsProductionPlanning.GetChanges();
            if (dsChanges is object)
            {
                if (dsChanges.HasErrors)
                {
                    dsProductionPlanning.RejectChanges();
                }
                else if (mListForm.FormMode == (int)Module1.FormModeEnum.DELETE_MODE)
                {
                    daWorkPeriod.Update(dsChanges, "Tbl_OperatorWorkPeriods");
                    daOperator.Update(dsChanges, "Tbl_Operators");
                }
                else if (mListForm.FormMode == (int)Module1.FormModeEnum.INSERT_MODE)
                {
                    daOperator.Update(dsChanges, "Tbl_Operators");
                    daWorkPeriod.Update(dsChanges, "Tbl_OperatorWorkPeriods");
                }
                else
                {
                    daOperator.Update(dsChanges, "Tbl_Operators");
                }

                dsChanges = null;
            }
        }

        private void CreateDataAdapterCommands()
        {
            // ایجاد دستور اضافه کردن رکورد جدید در جدول
            daOperator.InsertCommand = new SqlCommand("Insert Into Tbl_Operators(OperatorCode,OperatorName,WorkingStatus,ProductionPart) Values(@OperatorCode,@OperatorName,@WorkingStatus,@ProductionPart)", Module1.cnProductionPlanning);
            {
                var withBlock = daOperator.InsertCommand;
                withBlock.Parameters.Add("@OperatorCode", SqlDbType.VarChar, 50, "OperatorCode");
                withBlock.Parameters.Add("@OperatorName", SqlDbType.VarChar, 50, "OperatorName");
                withBlock.Parameters.Add("@WorkingStatus", SqlDbType.TinyInt, default, "WorkingStatus");
                withBlock.Parameters.Add("@ProductionPart", SqlDbType.Int, default, "ProductionPart");
            }
            // ایجاد دستور اصلاح رکورد جاری در جدول
            daOperator.UpdateCommand = new SqlCommand("Update Tbl_Operators Set OperatorCode=@CurrentOperatorCode,OperatorName=@OperatorName,WorkingStatus=@WorkingStatus,ProductionPart=@ProductionPart Where OperatorCode=@OldOperatorCode", Module1.cnProductionPlanning);
            {
                var withBlock1 = daOperator.UpdateCommand;
                withBlock1.Parameters.Add("@CurrentOperatorCode", SqlDbType.VarChar, 50, "OperatorCode");
                withBlock1.Parameters[0].Direction = ParameterDirection.Input;
                withBlock1.Parameters[0].SourceVersion = DataRowVersion.Current;
                withBlock1.Parameters.Add("@OperatorName", SqlDbType.VarChar, 50, "OperatorName");
                withBlock1.Parameters.Add("@WorkingStatus", SqlDbType.TinyInt, default, "WorkingStatus");
                withBlock1.Parameters.Add("@ProductionPart", SqlDbType.Int, default, "ProductionPart");
                withBlock1.Parameters.Add("@OldOperatorCode", SqlDbType.VarChar, 50, "OperatorCode");
                withBlock1.Parameters[4].Direction = ParameterDirection.Input;
                withBlock1.Parameters[4].SourceVersion = DataRowVersion.Original;
            }
            // ایجاد دستور حذف رکورد جاری در جدول
            daOperator.DeleteCommand = new SqlCommand("Delete From Tbl_Operators Where OperatorCode=@OperatorCode", Module1.cnProductionPlanning);
            {
                var withBlock2 = daOperator.DeleteCommand;
                withBlock2.Parameters.Add("@OperatorCode", SqlDbType.VarChar, 50, "OperatorCode");
                withBlock2.Parameters[0].Direction = ParameterDirection.Input;
                withBlock2.Parameters[0].SourceVersion = DataRowVersion.Original;
            }

            // ایجاد دستورات ثبت سوابق شغلی
            daWorkPeriod.InsertCommand = new SqlCommand("Insert Into Tbl_OperatorWorkPeriods(OperatorCode,StartDate,EndDate,Description) Values(@OperatorCode,@StartDate,@EndDate,@Description)", Module1.cnProductionPlanning);
            {
                var withBlock3 = daWorkPeriod.InsertCommand;
                withBlock3.Parameters.Add("@OperatorCode", SqlDbType.VarChar, 50, "OperatorCode");
                withBlock3.Parameters.Add("@StartDate", SqlDbType.VarChar, 8, "StartDate");
                withBlock3.Parameters.Add("@EndDate", SqlDbType.VarChar, 8, "EndDate");
                withBlock3.Parameters.Add("@Description", SqlDbType.VarChar, 200, "Description");
            }

            daWorkPeriod.UpdateCommand = new SqlCommand("Update Tbl_OperatorWorkPeriods Set StartDate=@CurrentStartDate,EndDate=@EndDate,Description=@Description Where OperatorCode=@OldOperatorCode And StartDate=@OldStartDate", Module1.cnProductionPlanning);
            {
                var withBlock4 = daWorkPeriod.UpdateCommand;
                // OperatorCode=@CurrentOperatorCode,
                // .Parameters.Add("@CurrentOperatorCode", SqlDbType.VarChar, 50, "OperatorCode").SourceVersion = DataRowVersion.Current
                withBlock4.Parameters.Add("@CurrentStartDate", SqlDbType.VarChar, 8, "StartDate").SourceVersion = DataRowVersion.Current;
                withBlock4.Parameters.Add("@EndDate", SqlDbType.VarChar, 8, "EndDate");
                withBlock4.Parameters.Add("@Description", SqlDbType.VarChar, 200, "Description");
                withBlock4.Parameters.Add("@OldOperatorCode", SqlDbType.VarChar, 50, "OperatorCode").SourceVersion = DataRowVersion.Original;
                withBlock4.Parameters.Add("@OldStartDate", SqlDbType.VarChar, 8, "StartDate").SourceVersion = DataRowVersion.Original;
            }

            daWorkPeriod.DeleteCommand = new SqlCommand("Delete From Tbl_OperatorWorkPeriods Where OperatorCode=@OperatorCode And StartDate=@StartDate", Module1.cnProductionPlanning);
            {
                var withBlock5 = daWorkPeriod.DeleteCommand;
                withBlock5.Parameters.Add("@OperatorCode", SqlDbType.VarChar, 50, "OperatorCode").SourceVersion = DataRowVersion.Original;
                withBlock5.Parameters.Add("@StartDate", SqlDbType.VarChar, 8, "StartDate").SourceVersion = DataRowVersion.Original;
            }
        }

        private bool ValidationForm()
        {
            if (dsProductionPlanning.Tables["Tbl_OperatorWorkPeriods"].Rows.Count == 0)
            {
                MessageBox.Show("سوابق شغلی اپراتور را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cmdAddWorkPeriod.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtPersonnelCode.Text))
            {
                MessageBox.Show("کد پرسنلی را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtPersonnelCode.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("نام را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtName.Focus();
                return false;
            }

            if (cbProductionPart.SelectedIndex == -1)
            {
                MessageBox.Show("بخش عملیات را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbProductionPart.Focus();
                return false;
            }

            if (dgWorkPeriods.Rows.Count == 0)
            {
                MessageBox.Show("سابقۀ شغلی ثبت نشده است", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                dgWorkPeriods.Focus();
                return false;
            }

            return true;
        }

        private void cmdAddWorkPeriod_Click(object sender, EventArgs e)
        {
            {
                var withBlock = new frmWorkPeriod();
                if (ReferenceEquals(sender, cmdAddWorkPeriod))
                {
                    withBlock.Text = " ثبت سابقۀ شغلی جدید";
                    withBlock.Tag = Module1.FormModeEnum.INSERT_MODE; // درج سابقۀ جدید
                    withBlock.dtWorkPeriod = dsProductionPlanning.Tables["Tbl_OperatorWorkPeriods"];
                    if (mListForm.FormMode == (int)Module1.FormModeEnum.EDIT_MODE)
                    {
                        withBlock.OperatorCode = Conversions.ToString(mCurrentRow["OperatorCode"]);
                    }
                }
                else if (dgWorkPeriods.SelectedRows.Count > 0) // در صورتیکه سابقه ای انتخاب شده باشد
                {
                    var dr = dsProductionPlanning.Tables["Tbl_OperatorWorkPeriods"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("StartDate = '", dgWorkPeriods.SelectedRows[0].Cells["StartDate"].Value), "'")));
                    if (dr.Length > 0)
                    {
                        if (ReferenceEquals(sender, cmdEditWorkPeriod))
                        {
                            withBlock.Text = " ویرایش سابقۀ شغلی اپراتور";
                            withBlock.CurrentRow = dr[0];
                            withBlock.Tag = Module1.FormModeEnum.EDIT_MODE; // ویرایش سابقۀ انتخاب شده
                            withBlock.dtWorkPeriod = dsProductionPlanning.Tables["Tbl_OperatorWorkPeriods"];
                            if (mListForm.FormMode == (int)Module1.FormModeEnum.EDIT_MODE)
                            {
                                withBlock.OperatorCode = Conversions.ToString(mCurrentRow["OperatorCode"]);
                            }
                        }
                        else if (ReferenceEquals(sender, cmdDeleteWorkPeriod)) // حذف سابقۀ انتخاب شده
                        {
                            if (MessageBox.Show("آیا برای حذف سابقۀ شغلی مطمئن هستید", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading) == DialogResult.Yes)
                            {
                                if (dsProductionPlanning.Tables["Tbl_OperatorWorkPeriods"].Rows.Count > 1)
                                {
                                    try
                                    {
                                        dr[0].Delete();
                                        SetWorkStatus(GetWorkStatus());
                                    }
                                    catch (Exception objEx)
                                    {
                                        Logger.SaveError(Name + ".cmdAddWorkPeriod_Click", objEx.Message);
                                        MessageBox.Show("حذف سابقۀ شغلی اپراتور با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("اپراتور باید حداقل یک سابقۀ شغلی داشته باشد، سابقۀ مورد نظر قابلیت حذف ندارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                                }
                            }

                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("مشخصات سابقۀ انتخاب شده یافت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("سابقه ای انتخاب نشده است", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return;
                }

                if (withBlock.ShowDialog() == DialogResult.OK)
                {
                    SetWorkStatus(GetWorkStatus());
                }
            }
        }

        private void SetWorkStatus(int mStatus)
        {
            if (mStatus == 1)
            {
                GroupBox2.Text = "وضعیت شغلی(در حال کار)";
            }
            else
            {
                GroupBox2.Text = "وضعیت شغلی(پایان کار)";
            }
        }

        private void dgWorkPeriods_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgWorkPeriods.Columns[e.ColumnIndex].Name.Equals("StartDate") || dgWorkPeriods.Columns[e.ColumnIndex].Name.Equals("EndDate"))
            {
                e.Value = Strings.Mid(Conversions.ToString(e.Value), 1, 4) + "/" + Strings.Mid(Conversions.ToString(e.Value), 5, 2) + "/" + Strings.Mid(Conversions.ToString(e.Value), 7, 2);
            }
        }
    }
}