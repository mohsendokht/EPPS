using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmUnitsRelation
    {
        public frmUnitsRelation()
        {
            InitializeComponent();
            _cmdSave.Name = "cmdSave";
            _cmdExit.Name = "cmdExit";
            _cmdDelete.Name = "cmdDelete";
            _txtCommunicationFactor.Name = "txtCommunicationFactor";
            _cbRelatedUnit.Name = "cbRelatedUnit";
            _cbBaseUnit.Name = "cbBaseUnit";
        }

        private frmRecordsLists mListForm;
        private SqlDataAdapter daUnitsRelation = new SqlDataAdapter();
        private DataRow CurrentRow; // برای نگهداری رکورد جاری
        private SqlParameter[] prmUpdate = new SqlParameter[4];
        private SqlParameter[] prmDelete = new SqlParameter[2];
        private short I;

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

        private void Form_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdSave, 6);
            Module1.SetButtonsImage(cmdDelete, 9);
            Module1.SetButtonsImage(cmdExit, 14);
            FormLoad();
        }

        private void frmOperationTitle_FormClosing(object sender, FormClosingEventArgs e)
        {
            dsProductionPlanning.RejectChanges();
            daUnitsRelation.Dispose();
            for (I = 2; I >= 1; I += -1)
            {
                dsProductionPlanning.Tables[I].Constraints.Clear();
                dsProductionPlanning.Tables[I].Dispose();
                dsProductionPlanning.Tables.RemoveAt(I);
            }

            prmUpdate = null;
            prmDelete = null;
            mListForm = null;
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("عنوان واحد سنجش را حذف می کنید", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading, false) == DialogResult.Yes)
            {
                SqlTransaction trnDelete = null;
                try
                {
                    if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                        Module1.cnProductionPlanning.Open();
                    trnDelete = Module1.cnProductionPlanning.BeginTransaction();
                    daUnitsRelation.DeleteCommand.Transaction = trnDelete;
                    CurrentRow.Delete();
                    SaveChanges();
                    trnDelete.Commit();
                    Close();
                }
                catch (InvalidConstraintException ObjCnstEx)
                {
                    Logger.LogException("", ObjCnstEx);
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
                    trnDelete = null;
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
                        try
                        {
                            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                                Module1.cnProductionPlanning.Open();
                            trnInsert = Module1.cnProductionPlanning.BeginTransaction();
                            daUnitsRelation.InsertCommand.Transaction = trnInsert;
                            // اضافه کردن رکورد جدید به جدول در دیتاست
                            drInsert = dsProductionPlanning.Tables["Tbl_UnitsRelations"].NewRow();
                            drInsert["BaseUnitCode"] = cbBaseUnit.SelectedValue;
                            drInsert["Base"] = cbBaseUnit.Text;
                            drInsert["RelatedUnitCode"] = cbRelatedUnit.SelectedValue;
                            drInsert["Related"] = cbRelatedUnit.Text;
                            drInsert["CommunicationFactorUnit"] = txtCommunicationFactor.Text;
                            dsProductionPlanning.Tables["Tbl_UnitsRelations"].Rows.Add(drInsert);
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

                case (int)Module1.FormModeEnum.EDIT_MODE:
                    {
                        SqlTransaction trnUpdate = null;
                        try
                        {
                            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                                Module1.cnProductionPlanning.Open();
                            trnUpdate = Module1.cnProductionPlanning.BeginTransaction();
                            daUnitsRelation.UpdateCommand.Transaction = trnUpdate;
                            CurrentRow.BeginEdit();
                            CurrentRow["BaseUnitCode"] = cbBaseUnit.SelectedValue;
                            CurrentRow["Base"] = cbBaseUnit.Text;
                            CurrentRow["RelatedUnitCode"] = cbRelatedUnit.SelectedValue;
                            CurrentRow["Related"] = cbRelatedUnit.Text;
                            CurrentRow["CommunicationFactorUnit"] = txtCommunicationFactor.Text;
                            CurrentRow.EndEdit();
                            SaveChanges();
                            trnUpdate.Commit();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            CurrentRow.CancelEdit();
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
                {
                    var withBlock = cbBaseUnit;
                    withBlock.DataSource = null;
                    withBlock.DataSource = dsProductionPlanning.Tables["Tbl_TestUnits_Base"];
                    withBlock.DisplayMember = "Title";
                    withBlock.ValueMember = "Code";
                    // .DataBindings.Add("SelectedValue", dsProductionPlanning, "Tbl_UnitsRelations.BaseUnitCode")
                }

                {
                    var withBlock1 = cbRelatedUnit;
                    withBlock1.DataSource = null;
                    withBlock1.DataSource = dsProductionPlanning.Tables["Tbl_TestUnits_Related"];
                    withBlock1.DisplayMember = "Title";
                    withBlock1.ValueMember = "Code";
                    // .DataBindings.Add("SelectedValue", dsProductionPlanning, "Tbl_UnitsRelations.RelatedUnitCode")
                }

                CreateDataAdapterCommands();
                switch (ListForm.FormMode)
                {
                    case (int)Module1.FormModeEnum.INSERT_MODE: // در صورتیکه فرم برای ایجاد رکورد جدید فراخوانی شده باشد این قسمت اجرا خواهد شد
                        {
                            cbBaseUnit.Focus();
                            break;
                        }

                    case (int)Module1.FormModeEnum.EDIT_MODE:
                    case (int)Module1.FormModeEnum.DELETE_MODE: // در صورتیکه فرم برای ویرایش یا حذف رکورد جاری فراخوانی شده باشد این قسمت اجرا خواهد شد
                        {
                            CurrentRow = ListForm.GetRow();
                            // پر کردن کنترل فرم با مقدار رکورد جاری
                            cbBaseUnit.SelectedValue = CurrentRow["BaseUnitCode"];
                            cbRelatedUnit.SelectedValue = CurrentRow["RelatedUnitCode"];
                            txtCommunicationFactor.Text = Conversions.ToString(CurrentRow["CommunicationFactorUnit"]);
                            switch (ListForm.FormMode) // در صورتیکه فرم برای ویرایش(اصلاح) رکورد جاری فراخوانی شده باشد این قسمت اجرا خواهد شد
                            {
                                case (int)Module1.FormModeEnum.EDIT_MODE:
                                    {
                                        cbBaseUnit.Focus();
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
                daUnitsRelation.Update(dsChanges, "Tbl_UnitsRelations");
                dsProductionPlanning.AcceptChanges();
            }

            dsChanges = null;
        }

        private void CreateDataAdapterCommands()
        {
            // ایجاد دستور اضافه کردن رکورد جدید در جدول
            daUnitsRelation.InsertCommand = new SqlCommand("Insert Into Tbl_UnitsRelations(BaseUnitCode,RelatedUnitCode,CommunicationFactorUnit) Values(@BaseUnitCode,@RelatedUnitCode,@CommunicationFactorUnit)", Module1.cnProductionPlanning);
            {
                var withBlock = daUnitsRelation.InsertCommand;
                withBlock.Parameters.Add("@BaseUnitCode", SqlDbType.Int, default, "BaseUnitCode");
                withBlock.Parameters.Add("@RelatedUnitCode", SqlDbType.Int, default, "RelatedUnitCode");
                withBlock.Parameters.Add("@CommunicationFactorUnit", SqlDbType.Real, default, "CommunicationFactorUnit");
            }
            // ایجاد دستور اصلاح رکورد جاری در جدول
            daUnitsRelation.UpdateCommand = new SqlCommand("Update Tbl_UnitsRelations Set BaseUnitCode=@CurrentBaseUnitCode,RelatedUnitCode=@CurrentRelatedUnitCode,CommunicationFactorUnit=@CommunicationFactorUnit Where BaseUnitCode=@OldBaseUnitCode And RelatedUnitCode=@OldRelatedUnitCode", Module1.cnProductionPlanning);
            {
                var withBlock1 = daUnitsRelation.UpdateCommand;
                prmUpdate[0] = withBlock1.Parameters.Add("@CurrentBaseUnitCode", SqlDbType.Int, default, "BaseUnitCode");
                prmUpdate[1] = withBlock1.Parameters.Add("@CurrentRelatedUnitCode", SqlDbType.Int, default, "RelatedUnitCode");
                prmUpdate[2] = withBlock1.Parameters.Add("@OldBaseUnitCode", SqlDbType.Int, default, "BaseUnitCode");
                prmUpdate[3] = withBlock1.Parameters.Add("@OldRelatedUnitCode", SqlDbType.Int, default, "RelatedUnitCode");
                withBlock1.Parameters.Add("@CommunicationFactorUnit", SqlDbType.Real, default, "CommunicationFactorUnit");
            }

            prmUpdate[0].Direction = ParameterDirection.Input;
            prmUpdate[0].SourceVersion = DataRowVersion.Current;
            prmUpdate[1].Direction = ParameterDirection.Input;
            prmUpdate[1].SourceVersion = DataRowVersion.Current;
            prmUpdate[2].Direction = ParameterDirection.Input;
            prmUpdate[2].SourceVersion = DataRowVersion.Original;
            prmUpdate[3].Direction = ParameterDirection.Input;
            prmUpdate[3].SourceVersion = DataRowVersion.Original;
            // ایجاد دستور حذف رکورد جاری در جدول
            daUnitsRelation.DeleteCommand = new SqlCommand("Delete From Tbl_UnitsRelations Where BaseUnitCode=@BaseUnitCode And RelatedUnitCode=@RelatedUnitCode", Module1.cnProductionPlanning);
            {
                var withBlock2 = daUnitsRelation.DeleteCommand;
                prmDelete[0] = withBlock2.Parameters.Add("@BaseUnitCode", SqlDbType.Int, default, "BaseUnitCode");
                prmDelete[1] = withBlock2.Parameters.Add("@RelatedUnitCode", SqlDbType.Int, default, "RelatedUnitCode");
            }

            prmDelete[0].Direction = ParameterDirection.Input;
            prmDelete[0].SourceVersion = DataRowVersion.Original;
            prmDelete[1].Direction = ParameterDirection.Input;
            prmDelete[1].SourceVersion = DataRowVersion.Original;
        }

        private bool ValidationForm()
        {
            if (cbBaseUnit.SelectedIndex == -1)
            {
                MessageBox.Show("واحد سنجش پایه را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbBaseUnit.Focus();
                return false;
            }

            if (cbRelatedUnit.SelectedIndex == -1)
            {
                MessageBox.Show("واحد سنجش مورد نظر را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbRelatedUnit.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtCommunicationFactor.Text))
            {
                MessageBox.Show("ضریب ارتباطی را وارد کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtCommunicationFactor.Focus();
                return false;
            }

            return true;
        }
    }
}