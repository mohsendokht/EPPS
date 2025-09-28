using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmMPS
    {
        public frmMPS()
        {
            InitializeComponent();
            _dgMPSList.Name = "dgMPSList";
            _cmdSave.Name = "cmdSave";
            _cmdExit.Name = "cmdExit";
            _cmdDelete.Name = "cmdDelete";
            _txtYear.Name = "txtYear";
        }

        private frmMPSList mListForm;
        private short mFormMode;
        private SqlDataAdapter mdaMPS = new SqlDataAdapter();
        private SqlDataAdapter mdaMPSDetail = new SqlDataAdapter();
        private SqlDataAdapter mdaMachineCapametry = new SqlDataAdapter();
        private SqlDataAdapter mdaPersonnelCapametry = new SqlDataAdapter();
        private SqlDataAdapter mdaMaterialCapametry = new SqlDataAdapter();
        private SqlDataAdapter mdaCapametryAccessibleTimes = new SqlDataAdapter();
        private DataView dvMPSDetail;
        private DataRow mCurrentRow;
        private SqlCommand cmLastCode = new SqlCommand("Select IsNull(Max(MPSCode),0) From Tbl_MPSs Where DefinitionYear=", Module1.cnProductionPlanning);
        private int LastCode;
        private short I, J;

        public frmMPSList ListForm
        {
            set
            {
                mListForm = value;
            }
        }

        private DataSet dsProductionPlanning
        {
            get
            {
                return mListForm.dsProductionPlanning;
            }
        }

        public short FormMode
        {
            set
            {
                mFormMode = value;
            }
        }

        private void frmCalender_FormClosing(object sender, FormClosingEventArgs e)
        {
            dsProductionPlanning.RejectChanges();
            dsProductionPlanning.Relations.Clear();
            for (I = (short)(dsProductionPlanning.Tables.Count - 1); I >= 1; I += -1)
            {
                dsProductionPlanning.Tables[I].Constraints.Clear();
                dsProductionPlanning.Tables[I].Dispose();
                dsProductionPlanning.Tables.RemoveAt(I);
            }

            mdaPersonnelCapametry.Dispose();
            mdaMachineCapametry.Dispose();
            mdaMaterialCapametry.Dispose();
            mdaCapametryAccessibleTimes.Dispose();
            mdaMPSDetail.Dispose();
            mdaMPS.Dispose();
            mCurrentRow = null;
            cmLastCode.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdSave, 6);
            Module1.SetButtonsImage(cmdDelete, 9);
            Module1.SetButtonsImage(cmdExit, 5);
            FormLoad();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgMPSList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true & e.KeyCode == Keys.F12)
            {
                Module1.ExportGridToExcel(dgMPSList);
            }
        }

        private void dgMPSList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            var loopTo = (short)(dgMPSList.Rows.Count - 2);
            for (I = 0; I <= loopTo; I++)
                dgMPSList.Rows[I].Cells[0].Value = I + 1;
        }

        private void dgMPSList_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            var loopTo = (short)(dgMPSList.Rows.Count - 2);
            for (I = 0; I <= loopTo; I++)
                dgMPSList.Rows[I].Cells[0].Value = I + 1;
        }

        private void txtYear_ValueChanged(object sender, EventArgs e)
        {
            if (Information.IsNumeric(Strings.Right(cmLastCode.CommandText, 4)))
            {
                cmLastCode.CommandText = Strings.Mid(cmLastCode.CommandText, 1, cmLastCode.CommandText.IndexOf(Strings.Right(cmLastCode.CommandText, 4)));
                cmLastCode.CommandText += txtYear.Value.ToString();
                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();
                LastCode = Conversions.ToInteger(cmLastCode.ExecuteScalar().ToString());
                lblMPSSerial.Text = (LastCode + 1).ToString();
                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                    Module1.cnProductionPlanning.Close();
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("برنامه ریزی را حذف می کنید", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading, false) == DialogResult.Yes)
            {
                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();
                var trnDelete = Module1.cnProductionPlanning.BeginTransaction();
                try
                {
                    mdaMPS.DeleteCommand.Transaction = trnDelete;
                    mdaMPSDetail.DeleteCommand.Transaction = trnDelete;
                    mdaMachineCapametry.DeleteCommand.Transaction = trnDelete;
                    mdaMaterialCapametry.DeleteCommand.Transaction = trnDelete;
                    mdaPersonnelCapametry.DeleteCommand.Transaction = trnDelete;
                    mdaCapametryAccessibleTimes.DeleteCommand.Transaction = trnDelete;

                    // حذف مشخصات برنامه ریزی
                    DeleteOldCapametrys();
                    DeleteOldMPSDetails();
                    mCurrentRow.Delete();
                    SaveChanges();
                    trnDelete.Commit();
                    Close();
                }
                catch (InvalidConstraintException ObjCnstEx)
                {
                    Logger.LogException("cmdDelete_Click", ObjCnstEx);
                    dsProductionPlanning.RejectChanges();
                    trnDelete.Rollback();
                    MessageBox.Show("برای حذف رکورد باید رکورد(های) مرتبط با آن حذف شوند", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                }
                catch (Exception objEx)
                {
                    dsProductionPlanning.RejectChanges();
                    trnDelete.Rollback();
                    Logger.LogException("cmdDelete_Click", objEx);
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
                        if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                            Module1.cnProductionPlanning.Open();
                        var trnInsert = Module1.cnProductionPlanning.BeginTransaction();
                        DataRow drInsert;
                        try
                        {
                            mdaMPS.InsertCommand.Transaction = trnInsert;
                            mdaMPSDetail.InsertCommand.Transaction = trnInsert;

                            // اضافه کردن رکورد جدید به جدول در دیتاست
                            drInsert = dsProductionPlanning.Tables["Tbl_MPSs"].NewRow();
                            drInsert["MPSCode"] = lblMPSSerial.Text;
                            drInsert["DefinitionYear"] = txtYear.Value;
                            drInsert["DefinitionDate"] = Strings.Replace(lblDefinitionDate.Text, "/", "");
                            drInsert["LastUpdate"] = 0;
                            drInsert["Description"] = txtDescription.Text;
                            drInsert["PersonnelCapametryCalendarCode"] = cbCalendar.SelectedValue;
                            dsProductionPlanning.Tables["Tbl_MPSs"].Rows.Add(drInsert);

                            // ایجاد مشخصات برنامه ریزی جدید
                            AddNewMPSDetail();
                            SaveChanges();
                            trnInsert.Commit();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            dsProductionPlanning.RejectChanges();
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
                        if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                            Module1.cnProductionPlanning.Open();
                        var trnUpdate = Module1.cnProductionPlanning.BeginTransaction();
                        try
                        {
                            mdaMPS.UpdateCommand.Transaction = trnUpdate;
                            mdaMPSDetail.DeleteCommand.Transaction = trnUpdate;
                            mdaMPSDetail.InsertCommand.Transaction = trnUpdate;
                            mdaMachineCapametry.DeleteCommand.Transaction = trnUpdate;
                            mdaPersonnelCapametry.DeleteCommand.Transaction = trnUpdate;
                            mdaMaterialCapametry.DeleteCommand.Transaction= trnUpdate;
                            mdaCapametryAccessibleTimes.DeleteCommand.Transaction = trnUpdate;

                            // حذف مشخصات برنامه ریزی قبلی
                            DeleteOldMPSDetails();
                            // حذف مشخصات ظرفیت سنجی های قبلی
                            DeleteOldCapametrys();
                            mCurrentRow.BeginEdit();
                            mCurrentRow["MPSCode"] = lblMPSSerial.Text;
                            mCurrentRow["DefinitionYear"] = txtYear.Value;
                            mCurrentRow["DefinitionDate"] = Strings.Replace(lblDefinitionDate.Text, "/", "");
                            // mCurrentRow("LastUpdate") = Replace(lblLastUpdate.Text, "/", "")
                            mCurrentRow["Description"] = txtDescription.Text;
                            mCurrentRow["PersonnelCapametryCalendarCode"] = cbCalendar.SelectedValue;
                            mCurrentRow.EndEdit();

                            // ایجاد مشخصات برنامه ریزی جدید
                            AddNewMPSDetail();
                            SaveChanges();
                            trnUpdate.Commit();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            dsProductionPlanning.RejectChanges();
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

        private void FormLoad()
        {
            try
            {
                CreateDataAdapterCommands();
                var daFillCombo = new SqlDataAdapter("Select CalendarCode, CalendarTitle From Tbl_Calendar", Module1.cnProductionPlanning);
                var dtFillCombo = new DataTable();
                daFillCombo.Fill(dtFillCombo);
                {
                    var withBlock = cbCalendar;
                    withBlock.DataSource = dtFillCombo.DefaultView;
                    withBlock.DisplayMember = "CalendarTitle";
                    withBlock.ValueMember = "CalendarCode";
                }

                dvMPSDetail = dsProductionPlanning.Tables["Tbl_MPSDetails"].DefaultView;
                switch (mFormMode)
                {
                    case (short)Module1.FormModeEnum.INSERT_MODE:
                        {
                            // بدست آوردن آخرین شماره سریال ثبت شده در سال جاری
                            int CurrentYear = Conversions.ToInteger(Module1.mServerShamsiDate);
                            CurrentYear = Conversions.ToInteger(Strings.Mid(CurrentYear.ToString(), 1, 4));
                            cmLastCode.CommandText += CurrentYear.ToString();
                            txtYear.Value = CurrentYear;
                            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                                Module1.cnProductionPlanning.Open();
                            LastCode = Conversions.ToInteger(cmLastCode.ExecuteScalar().ToString());
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                            lblMPSSerial.Text = (LastCode + 1).ToString();
                            lblDefinitionDate.Text = Module1.mServerShamsiDate;
                            txtYear.Focus();
                            break;
                        }

                    case (short)Module1.FormModeEnum.EDIT_MODE:
                    case (short)Module1.FormModeEnum.DELETE_MODE:
                        {
                            mCurrentRow = mListForm.GetRow();

                            // پر کردن کنترل فرم با مقدار رکورد جاری
                            FillForm();
                            switch (mFormMode)
                            {
                                case (short)Module1.FormModeEnum.EDIT_MODE:
                                    {
                                        txtYear.Focus();
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

        private void FillForm()
        {
            lblMPSSerial.Text = Conversions.ToString(mCurrentRow["MPSCode"]);
            txtYear.Text = Conversions.ToString(mCurrentRow["DefinitionYear"]);
            lblDefinitionDate.Text = Strings.Mid(Conversions.ToString(mCurrentRow["DefinitionDate"]), 1, 4) + "/" + Strings.Mid(Conversions.ToString(mCurrentRow["DefinitionDate"]), 5, 2) + "/" + Strings.Mid(Conversions.ToString(mCurrentRow["DefinitionDate"]), 7, 2);
            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(mCurrentRow["LastUpdate"], 0, false)))
            {
                lblLastUpdate.Text = Strings.Mid(Conversions.ToString(mCurrentRow["LastUpdate"]), 1, 4) + "/" + Strings.Mid(Conversions.ToString(mCurrentRow["LastUpdate"]), 5, 2) + "/" + Strings.Mid(Conversions.ToString(mCurrentRow["LastUpdate"]), 7, 2);
            }

            txtDescription.Text = Conversions.ToString(Interaction.IIf(Information.IsDBNull(mCurrentRow["Description"]), "", mCurrentRow["Description"]));
            cbCalendar.SelectedValue = mCurrentRow["PersonnelCapametryCalendarCode"];
            int I;
            var loopTo = dvMPSDetail.Count - 1;
            for (I = 0; I <= loopTo; I++)
                dgMPSList.Rows.Add(dvMPSDetail[I][2], dvMPSDetail[I][3], dvMPSDetail[I][4], dvMPSDetail[I][5], dvMPSDetail[I][6], dvMPSDetail[I][7], dvMPSDetail[I][8], dvMPSDetail[I][9], dvMPSDetail[I][10], dvMPSDetail[I][11], dvMPSDetail[I][12], dvMPSDetail[I][13], dvMPSDetail[I][14], dvMPSDetail[I][15], dvMPSDetail[I][16]);



        }

        private void AddNewMPSDetail()
        {
            DataRow drInsert;
            var loopTo = (short)(dgMPSList.Rows.Count - 2);
            for (I = 0; I <= loopTo; I++)
            {
                drInsert = dsProductionPlanning.Tables["Tbl_MPSDetails"].NewRow();
                drInsert["MPSCode"] = lblMPSSerial.Text;
                drInsert["MPSYear"] = txtYear.Value;
                drInsert["MPSDetailPriority"] = dgMPSList.Rows[I].Cells[0].Value;
                drInsert["ProductCode"] = dgMPSList.Rows[I].Cells[1].Value;
                drInsert["ContractNo"] = dgMPSList.Rows[I].Cells[2].Value;
                drInsert["Order1"] = dgMPSList.Rows[I].Cells[3].Value;
                drInsert["Order2"] = dgMPSList.Rows[I].Cells[4].Value;
                drInsert["Order3"] = dgMPSList.Rows[I].Cells[5].Value;
                drInsert["Order4"] = dgMPSList.Rows[I].Cells[6].Value;
                drInsert["Order5"] = dgMPSList.Rows[I].Cells[7].Value;
                drInsert["Order6"] = dgMPSList.Rows[I].Cells[8].Value;
                drInsert["Order7"] = dgMPSList.Rows[I].Cells[9].Value;
                drInsert["Order8"] = dgMPSList.Rows[I].Cells[10].Value;
                drInsert["Order9"] = dgMPSList.Rows[I].Cells[11].Value;
                drInsert["Order10"] = dgMPSList.Rows[I].Cells[12].Value;
                drInsert["Order11"] = dgMPSList.Rows[I].Cells[13].Value;
                drInsert["Order12"] = dgMPSList.Rows[I].Cells[14].Value;
                dsProductionPlanning.Tables["Tbl_MPSDetails"].Rows.Add(drInsert);
            }
        }

        private void DeleteOldMPSDetails()
        {
            for (I = (short)(dvMPSDetail.Count - 1); I >= 0; I += -1)
                dvMPSDetail[I].Delete();
        }

        private void DeleteOldCapametrys()
        {
           
            for (int c = dsProductionPlanning.Tables["Tbl_PersonnelCapametry"].Rows.Count - 1; c >= 0; c += -1)
                 dsProductionPlanning.Tables["Tbl_PersonnelCapametry"].Rows[c].Delete();

            for (int c = dsProductionPlanning.Tables["Tbl_MachineCapametry"].Rows.Count - 1; c >= 0; c += -1)
                dsProductionPlanning.Tables["Tbl_MachineCapametry"].Rows[c].Delete();
            for (int c = dsProductionPlanning.Tables["Tbl_MaterialCapametry"].Rows.Count - 1; c >= 0; c += -1)
                dsProductionPlanning.Tables["Tbl_MaterialCapametry"].Rows[c].Delete();

            for (int c =dsProductionPlanning.Tables["Tbl_CapametryAccessibleTimes"].Rows.Count - 1; c >= 0; c += -1)
                dsProductionPlanning.Tables["Tbl_CapametryAccessibleTimes"].Rows[c].Delete();

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
                if (mFormMode != (int)Module1.FormModeEnum.INSERT_MODE)
                {
                    mdaPersonnelCapametry.Update(dsChanges, "Tbl_PersonnelCapametry");
                    mdaMachineCapametry.Update(dsChanges, "Tbl_MachineCapametry");
                    mdaMaterialCapametry.Update(dsChanges, "Tbl_MaterialCapametry");
                    mdaCapametryAccessibleTimes.Update(dsChanges, "Tbl_CapametryAccessibleTimes");
                }

                if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
                {
                    mdaMPSDetail.Update(dsChanges, "Tbl_MPSDetails");
                    mdaMPS.Update(dsChanges, "Tbl_MPSs");
                }
                else
                {
                    mdaMPS.Update(dsChanges, "Tbl_MPSs");
                    mdaMPSDetail.Update(dsChanges, "Tbl_MPSDetails");
                }

                dsProductionPlanning.AcceptChanges();
            }

            dsChanges = null;
        }

        private void CreateDataAdapterCommands()
        {
            mdaMPS.InsertCommand = new SqlCommand("Insert Into Tbl_MPSs(MPSCode,DefinitionYear,DefinitionDate,LastUpdate,Description,PersonnelCapametryCalendarCode) Values(@MPSCode,@DefinitionYear,@DefinitionDate,@LastUpdate,@Description,@PersonnelCapametryCalendarCode)", Module1.cnProductionPlanning);
            {
                var withBlock = mdaMPS.InsertCommand;
                withBlock.Parameters.Add("@MPSCode", SqlDbType.Int, default, "MPSCode");
                withBlock.Parameters.Add("@DefinitionYear", SqlDbType.SmallInt, default, "DefinitionYear");
                withBlock.Parameters.Add("@DefinitionDate", SqlDbType.Int, default, "DefinitionDate");
                withBlock.Parameters.Add("@LastUpdate", SqlDbType.Int, default, "LastUpdate");
                withBlock.Parameters.Add("@Description", SqlDbType.VarChar, 300, "Description");
                withBlock.Parameters.Add("@PersonnelCapametryCalendarCode", SqlDbType.Int, default, "PersonnelCapametryCalendarCode");
            }

            mdaMPS.UpdateCommand = new SqlCommand("Update Tbl_MPSs Set MPSCode=@CurrentMPSCode,DefinitionYear=@CurrentDefinitionYear,DefinitionDate=@DefinitionDate,LastUpdate=@LastUpdate,Description=@Description,PersonnelCapametryCalendarCode=@PersonnelCapametryCalendarCode Where MPSCode=@OldMPSCode And DefinitionYear=@OldDefinitionYear", Module1.cnProductionPlanning);
            {
                var withBlock1 = mdaMPS.UpdateCommand;
                withBlock1.Parameters.Add("@CurrentMPSCode", SqlDbType.Int, default, "MPSCode").SourceVersion = DataRowVersion.Current;
                withBlock1.Parameters.Add("@CurrentDefinitionYear", SqlDbType.SmallInt, default, "DefinitionYear").SourceVersion = DataRowVersion.Current;
                withBlock1.Parameters.Add("@DefinitionDate", SqlDbType.Int, default, "DefinitionDate");
                withBlock1.Parameters.Add("@LastUpdate", SqlDbType.Int, default, "LastUpdate");
                withBlock1.Parameters.Add("@Description", SqlDbType.VarChar, 300, "Description");
                withBlock1.Parameters.Add("@PersonnelCapametryCalendarCode", SqlDbType.Int, default, "PersonnelCapametryCalendarCode");
                withBlock1.Parameters.Add("@OldMPSCode", SqlDbType.Int, default, "MPSCode").SourceVersion = DataRowVersion.Original;
                withBlock1.Parameters.Add("@OldDefinitionYear", SqlDbType.SmallInt, default, "DefinitionYear").SourceVersion = DataRowVersion.Original;
            }

            mdaMPS.DeleteCommand = new SqlCommand("Delete From Tbl_MPSs Where MPSCode=@MPSCode And DefinitionYear=@DefinitionYear", Module1.cnProductionPlanning);
            {
                var withBlock2 = mdaMPS.DeleteCommand;
                withBlock2.Parameters.Add("@MPSCode", SqlDbType.Int, default, "MPSCode").SourceVersion = DataRowVersion.Current;
                withBlock2.Parameters.Add("@DefinitionYear", SqlDbType.SmallInt, default, "DefinitionYear").SourceVersion = DataRowVersion.Current;
            }

            mdaMPSDetail.InsertCommand = new SqlCommand("Insert Into Tbl_MPSDetails(MPSCode,MPSYear,MPSDetailPriority,ProductCode,ContractNo,Order1,Order2,Order3,Order4,Order5,Order6,Order7,Order8,Order9,Order10,Order11,Order12) " + "Values(@MPSCode,@MPSYear,@MPSDetailPriority,@ProductCode,@ContractNo,@Order1,@Order2,@Order3,@Order4,@Order5,@Order6,@Order7,@Order8,@Order9,@Order10,@Order11,@Order12)", Module1.cnProductionPlanning);
            {
                var withBlock3 = mdaMPSDetail.InsertCommand;
                withBlock3.Parameters.Add("@MPSCode", SqlDbType.Int, default, "MPSCode");
                withBlock3.Parameters.Add("@MPSYear", SqlDbType.SmallInt, default, "MPSYear");
                withBlock3.Parameters.Add("@MPSDetailPriority", SqlDbType.Int, default, "MPSDetailPriority");
                withBlock3.Parameters.Add("@ProductCode", SqlDbType.VarChar, 50, "ProductCode");
                withBlock3.Parameters.Add("@ContractNo", SqlDbType.VarChar, 50, "ContractNo");
                withBlock3.Parameters.Add("@Order1", SqlDbType.Int, default, "Order1");
                withBlock3.Parameters.Add("@Order2", SqlDbType.Int, default, "Order2");
                withBlock3.Parameters.Add("@Order3", SqlDbType.Int, default, "Order3");
                withBlock3.Parameters.Add("@Order4", SqlDbType.Int, default, "Order4");
                withBlock3.Parameters.Add("@Order5", SqlDbType.Int, default, "Order5");
                withBlock3.Parameters.Add("@Order6", SqlDbType.Int, default, "Order6");
                withBlock3.Parameters.Add("@Order7", SqlDbType.Int, default, "Order7");
                withBlock3.Parameters.Add("@Order8", SqlDbType.Int, default, "Order8");
                withBlock3.Parameters.Add("@Order9", SqlDbType.Int, default, "Order9");
                withBlock3.Parameters.Add("@Order10", SqlDbType.Int, default, "Order10");
                withBlock3.Parameters.Add("@Order11", SqlDbType.Int, default, "Order11");
                withBlock3.Parameters.Add("@Order12", SqlDbType.Int, default, "Order12");
            }

            mdaMPSDetail.DeleteCommand = new SqlCommand("Delete From Tbl_MPSDetails Where MPSCode=@MPSCode And MPSYear=@MPSYear And MPSDetailPriority=@MPSDetailPriority", Module1.cnProductionPlanning);
            {
                var withBlock4 = mdaMPSDetail.DeleteCommand;
                withBlock4.Parameters.Add("@MPSCode", SqlDbType.Int, default, "MPSCode").SourceVersion = DataRowVersion.Original;
                withBlock4.Parameters.Add("@MPSYear", SqlDbType.SmallInt, default, "MPSYear").SourceVersion = DataRowVersion.Original;
                withBlock4.Parameters.Add("@MPSDetailPriority", SqlDbType.Int, default, "MPSDetailPriority").SourceVersion = DataRowVersion.Original;
            }

            // ایجاد دستور حذف ظرفیت سنجی ماشین آلات
            mdaMachineCapametry.DeleteCommand = new SqlCommand("Delete From Tbl_MachineCapametry Where MPSCode=@MPSCode And MPSYear=@MPSYear And MPSDetailPriority=@MPSDetailPriority And MonthNo=@MonthNo And MachineCode=@MachineCode", Module1.cnProductionPlanning);
            {
                var withBlock5 = mdaMachineCapametry.DeleteCommand;
                withBlock5.Parameters.Add("@MPSCode", SqlDbType.Int, default, "MPSCode").SourceVersion = DataRowVersion.Original;
                withBlock5.Parameters.Add("@MPSYear", SqlDbType.SmallInt, default, "MPSYear").SourceVersion = DataRowVersion.Original;
                withBlock5.Parameters.Add("@MPSDetailPriority", SqlDbType.Int, default, "MPSDetailPriority").SourceVersion = DataRowVersion.Original;
                withBlock5.Parameters.Add("@MonthNo", SqlDbType.TinyInt, default, "MonthNo").SourceVersion = DataRowVersion.Original;
                withBlock5.Parameters.Add("@MachineCode", SqlDbType.VarChar, 50, "MachineCode").SourceVersion = DataRowVersion.Original;
            }
            // ایجاد دستور حذف ظرفیت سنجی نیروس انسانی
            mdaPersonnelCapametry.DeleteCommand = new SqlCommand("Delete From Tbl_PersonnelCapametry Where MPSCode=@MPSCode And MPSYear=@MPSYear And MPSDetailPriority=@MPSDetailPriority And MonthNo=@MonthNo", Module1.cnProductionPlanning);
            {
                var withBlock6 = mdaPersonnelCapametry.DeleteCommand;
                withBlock6.Parameters.Add("@MPSCode", SqlDbType.Int, default, "MPSCode").SourceVersion = DataRowVersion.Original;
                withBlock6.Parameters.Add("@MPSYear", SqlDbType.SmallInt, default, "MPSYear").SourceVersion = DataRowVersion.Original;
                withBlock6.Parameters.Add("@MPSDetailPriority", SqlDbType.Int, default, "MPSDetailPriority").SourceVersion = DataRowVersion.Original;
                withBlock6.Parameters.Add("@MonthNo", SqlDbType.TinyInt, default, "MonthNo").SourceVersion = DataRowVersion.Original;
            }

            //Create delect commands for Tbl_MaterialCapametry
            mdaMaterialCapametry.DeleteCommand = new SqlCommand("Delete From Tbl_MaterialCapametry Where MPSCode=@MPSCode And MPSYear=@MPSYear And MPSDetailPriority=@MPSDetailPriority And MonthNo=@MonthNo", Module1.cnProductionPlanning);
            {
                var withBlock6 = mdaMaterialCapametry.DeleteCommand;
                withBlock6.Parameters.Add("@MPSCode", SqlDbType.Int, default, "MPSCode").SourceVersion = DataRowVersion.Original;
                withBlock6.Parameters.Add("@MPSYear", SqlDbType.SmallInt, default, "MPSYear").SourceVersion = DataRowVersion.Original;
                withBlock6.Parameters.Add("@MPSDetailPriority", SqlDbType.Int, default, "MPSDetailPriority").SourceVersion = DataRowVersion.Original;
                withBlock6.Parameters.Add("@MonthNo", SqlDbType.TinyInt, default, "MonthNo").SourceVersion = DataRowVersion.Original;
            }

            mdaCapametryAccessibleTimes.DeleteCommand = new SqlCommand("Delete From Tbl_CapametryAccessibleTimes Where MPSCode=@MPSCode AND MPSYear = @MPSYear AND MonthNo = @MonthNo AND MachineCode= @MachineCode ", Module1.cnProductionPlanning);
            mdaCapametryAccessibleTimes.DeleteCommand.Parameters.Add("@MPSCode", SqlDbType.Int, default, "MPSCode").SourceVersion = DataRowVersion.Original;
            mdaCapametryAccessibleTimes.DeleteCommand.Parameters.Add("@MPSYear", SqlDbType.Int, default, "MPSYear").SourceVersion = DataRowVersion.Original;
            mdaCapametryAccessibleTimes.DeleteCommand.Parameters.Add("@MonthNo", SqlDbType.Int, default, "MonthNo").SourceVersion = DataRowVersion.Original;
            mdaCapametryAccessibleTimes.DeleteCommand.Parameters.Add("@MachineCode", SqlDbType.VarChar, 50, "MachineCode").SourceVersion = DataRowVersion.Original;
        }

            private bool FormValidation()
        {
            if (txtYear.Value == 0m)
            {
                MessageBox.Show("سال را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtYear.Focus();
                return false;
            }

            if (cbCalendar.SelectedValue is null)
            {
                MessageBox.Show("تقویم ظرفیت سنجی پرسنل را انتخاب نمایید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbCalendar.Focus();
                return false;
            }

            var loopTo = (short)(dgMPSList.Rows.Count - 2);
            for (I = 0; I <= loopTo; I++)
            {
                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgMPSList.Rows[I].Cells[1].Value, "0", false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgMPSList.Rows[I].Cells[1].Value, Constants.vbNullString, false)) || dgMPSList.Rows[I].Cells[1].Value is null || Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgMPSList.Rows[I].Cells[2].Value, "0", false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgMPSList.Rows[I].Cells[2].Value, Constants.vbNullString, false)) || dgMPSList.Rows[I].Cells[2].Value is null)
                {
                    MessageBox.Show("کد محصول یا شماره قرارداد وارد نشده است", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    dgMPSList.Focus();
                    return false;
                }

                for (J = 3; J <= 14; J++)
                {
                    if (dgMPSList.Rows[I].Cells[J].Value is null)
                    {
                        dgMPSList.Rows[I].Cells[J].Value = 0;
                    }

                    if (!Information.IsNumeric(dgMPSList.Rows[I].Cells[J].Value))
                    {
                        MessageBox.Show("برای تعداد سفارش ماه ها، مقادیر عددی وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        dgMPSList.Focus();
                        return false;
                    }
                }
            }

            return true;
        }
    }
}