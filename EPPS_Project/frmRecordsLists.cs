using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using static ProductionPlanning.MyEnums;

namespace ProductionPlanning
{
    public partial class frmRecordsLists
    {
        public frmRecordsLists()
        {
            InitializeComponent();
            _cbFilter8.Name = "cbFilter8";
            _cbFilter7.Name = "cbFilter7";
            _cbFilter6.Name = "cbFilter6";
            _cbFilter5.Name = "cbFilter5";
            _cbFilter4.Name = "cbFilter4";
            _cbFilter3.Name = "cbFilter3";
            _cbFilter2.Name = "cbFilter2";
            _cbFilter1.Name = "cbFilter1";
            _txtSearch8.Name = "txtSearch8";
            _txtSearch7.Name = "txtSearch7";
            _txtSearch6.Name = "txtSearch6";
            _txtSearch2.Name = "txtSearch2";
            _txtSearch3.Name = "txtSearch3";
            _txtSearch4.Name = "txtSearch4";
            _txtSearch1.Name = "txtSearch1";
            _txtSearch5.Name = "txtSearch5";
            _dgList.Name = "dgList";
            _cmdFilter.Name = "cmdFilter";
            _cmdExit.Name = "cmdExit";
            _cmdFind.Name = "cmdFind";
            _cmdDelete.Name = "cmdDelete";
            _cmdUpdate.Name = "cmdUpdate";
            _cmdInsert.Name = "cmdInsert";
        }

        private int mFormMode;
        private int mSearchMode = -1;
        private int I;
        private string mCurrentTableName;
        private DataSetConfiguration DataSetConfig = new DataSetConfiguration();
        private DataRow[] FoundRows = null;
        private IEnumerator FoundRowsEnumerator;

        public int FormMode
        {
            get
            {
                return mFormMode;
            }

            set
            {
                mFormMode = value;
            }
        }

        public int SearchMode
        {
            get
            {
                return mSearchMode;
            }

            set
            {
                mSearchMode = value;
            }
        }

        public string CurrentTableName
        {
            get
            {
                return mCurrentTableName;
            }

            set
            {
                mCurrentTableName = value;
            }
        }

        public DataSet dsProductionPlanning
        {
            get
            {
                return DataSetConfig.dsProductionPlanning;
            }
        }

        public ListFormCaller CallerForm { get; set; }

        public enum SearchModeEnum
        {
            SM_FIND,
            SM_FILTER
        }

        private void frmRecordsLists_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdInsert, 0);
            Module1.SetButtonsImage(cmdDelete, 1);
            Module1.SetButtonsImage(cmdUpdate, 2);
            Module1.SetButtonsImage(cmdFilter, 3);
            Module1.SetButtonsImage(cmdFind, 10);
            dgList.Tag = -1;
            dgList.Sorted += Module1.DataGridViews_Sorted_EventHandler;
            string SelectStr = Constants.vbNullString;
            switch (this.CallerForm)
            {
                case ListFormCaller.LFC_OPERATRIONSTITLES:
                    {
                        DataSetConfig.FillDataSet("Tbl_OperationsDefaultTitles", "Tbl_OperationsDefaultTitles", "Select * From Tbl_OperationsDefaultTitles", "Code");
                        Prepare_To_Show_TablesRecordList("Tbl_OperationsDefaultTitles", " عناوین پیش فرض فعالیتها");
                        break;
                    }

                case ListFormCaller.LFC_PRODUCTIONPARTS:
                    {
                        DataSetConfig.FillDataSet("Tbl_Natures", "Tbl_Natures", "Select * From Tbl_Natures", "NatureCode");
                        Prepare_To_Show_TablesRecordList("Tbl_Natures", " مشخصات بخش های تولید");
                        break;
                    }

                case ListFormCaller.LFC_SUPPLIERS:
                    {
                        DataSetConfig.FillDataSet("Tbl_Suppliers", "Tbl_Suppliers", "Select * From Tbl_Suppliers", "SupplierCode");
                        Prepare_To_Show_TablesRecordList("Tbl_Suppliers", " لیست مشخصات تامین کنندگان");
                        break;
                    }

                case ListFormCaller.LFC_TESTUNITS:
                    {
                        DataSetConfig.FillDataSet("Tbl_TestUnits", "Tbl_TestUnits", "Select * From Tbl_TestUnits", "Code");
                        Prepare_To_Show_TablesRecordList("Tbl_TestUnits", " مشخصات واحدهای سنجش");
                        break;
                    }

                case ListFormCaller.LFC_UNITSRELATIONS:
                    {
                        SelectStr = "SELECT A.BaseUnitCode, B.Title AS Base, A.RelatedUnitCode, C.Title AS Related, " + "A.CommunicationFactorUnit FROM dbo.Tbl_UnitsRelations A INNER JOIN dbo.Tbl_TestUnits " + "B ON A.BaseUnitCode = B.Code INNER JOIN dbo.Tbl_TestUnits C ON A.RelatedUnitCode = C.Code";

                        DataSetConfig.FillDataSet("Tbl_UnitsRelations", "Tbl_UnitsRelations", SelectStr, "BaseUnitCode", "RelatedUnitCode");
                        Prepare_To_Show_TablesRecordList("Tbl_UnitsRelations", " مشخصات ارتباط بین واحدهای سنجش");
                        break;
                    }

                case ListFormCaller.LFC_PRIMARYMATERIALS:
                    {
                        SelectStr = "Select A.MaterialCode,A.MaterialTitle,A.TechniqueSpecification,A.StoreUnit," + "       B.Title AS StoreUnitTitle,A.ProductionUnit,A.OrderPoint," + "       A.OrderMinimumAmount,A.StorePlace,A.LastHolding,A.FirstHolding," + "       A.StoredAdmeasurementMethod,A.GarbagePercent,A.BatchBuyTime,A.BatchQuantity,A.TimeType,A.Application " + "From   dbo.Tbl_PrimaryMaterials A INNER JOIN dbo.Tbl_TestUnits B ON A.StoreUnit = B.Code";



                        DataSetConfig.FillDataSet("Tbl_PrimaryMaterials", "Tbl_PrimaryMaterials", SelectStr, "MaterialCode");
                        Prepare_To_Show_TablesRecordList("Tbl_PrimaryMaterials", " مشخصات مواد اولیه");
                        break;
                    }

                case ListFormCaller.LFC_MACHINES:
                    {
                        SelectStr = @"SELECT 
                                         Tbl_Machines.Code,dbo.Tbl_Machines.Name,dbo.Tbl_Machines.Producer,
                                         Tbl_Machines.ProducerCountry,dbo.Tbl_Machines.Application,
                                         Tbl_Machines.CalendarCode,dbo.Tbl_Calendar.CalendarTitle,
                                         Tbl_Machines.Description 
                                      FROM Tbl_Machines 
                                           INNER JOIN Tbl_Calendar ON Tbl_Machines.CalendarCode = Tbl_Calendar.CalendarCode 
                                      ";

                        DataSetConfig.FillDataSet("Tbl_Machines", "Tbl_Machines", SelectStr, "Code");
                        Prepare_To_Show_TablesRecordList("Tbl_Machines", " مشخصات ماشین آلات");
                        break;
                    }

                case ListFormCaller.LFC_MACHINENOTAVAILABLEREASON:
                    {
                        DataSetConfig.FillDataSet("Tbl_MachineNotAvailableReasons", "Tbl_MachineNotAvailableReasons", "Select * From Tbl_MachineNotAvailableReasons", "ReasonCode");
                        Prepare_To_Show_TablesRecordList("Tbl_MachineNotAvailableReasons", " لیست علت های در دسترس نبودن ماشین");
                        break;
                    }

                case ListFormCaller.LFC_CONTRACTORS:
                    {
                        DataSetConfig.FillDataSet("Tbl_Contractors", "Tbl_Contractors", "Select * From Tbl_Contractors", "ContractorCode");
                        Prepare_To_Show_TablesRecordList("Tbl_Contractors", " مشخصات پیمانکاران");
                        break;
                    }

                case ListFormCaller.LFC_STORES:
                    {
                        DataSetConfig.FillDataSet("Tbl_Stores", "Tbl_Stores", "Select * From Tbl_Stores", "StoreCode");
                        Prepare_To_Show_TablesRecordList("Tbl_Stores", " مشخصات انبارها");
                        break;
                    }

                case ListFormCaller.LFC_HOLIDAYS:
                    {
                        SelectStr = "SELECT DayNo, MonthNo," + "CASE MonthNo WHEN 1 THEN 'فروردین' WHEN 2 THEN 'اردیبهشت' WHEN 3 THEN 'خرداد' WHEN 4 THEN 'تیر' WHEN 5 THEN 'مرداد' WHEN 6 THEN 'شهریور'" + "WHEN 7 THEN 'مهر' WHEN 8 THEN 'آبان' WHEN 9 THEN 'آذر' WHEN 10 THEN 'دی' WHEN 11 THEN 'بهمن' WHEN 12 THEN 'اسفند' END AS Month," + "Description FROM dbo.Tbl_HoliDays";


                        DataSetConfig.FillDataSet("Tbl_HoliDays", "Tbl_HoliDays", SelectStr, "DayNo", "MonthNo");
                        dsProductionPlanning.Tables["Tbl_HoliDays"].Columns["Month"].ReadOnly = false;
                        Prepare_To_Show_TablesRecordList("Tbl_HoliDays", " مشخصات روزهای تعطیل رسمی در تقویم سال");
                        break;
                    }

                case ListFormCaller.LFC_ACTINGCALENDARS:
                    {
                        DataSetConfig.FillDataSet("Tbl_Calendar", "Tbl_Calendar", "Select * From Tbl_Calendar", "CalendarCode");
                        Prepare_To_Show_TablesRecordList("Tbl_Calendar", " لیست تقویم های کاری");
                        break;
                    }

                case ListFormCaller.LFC_PRODUCTS:
                    {
                        DataSetConfig.FillDataSet("Tbl_Products", "Tbl_Products", "Select * From Tbl_Products", "ProductCode");
                        Prepare_To_Show_TablesRecordList("Tbl_Products", " مشخصات محصول");
                        // '''''''''''''''''''''''''''''''''''''''
                        // ''''  Set User Access Right  ''''''''''
                        cmdInsert.Enabled = Module_UserAccess.HaveAccessToItem(20);
                        cmdUpdate.Enabled = Module_UserAccess.HaveAccessToItem(21);
                        cmdDelete.Enabled = Module_UserAccess.HaveAccessToItem(22);
                        break;
                    }
                // '''''''''''''''''''''''''''''''''''''''
                // '''''''''''''''''''''''''''''''''''''''
                case ListFormCaller.LFC_PRODUCTIONBATCHS:
                    {
                        SelectStr = "Select A.BatchCode,A.DefineYear,A.SequenceNo,B.ProductCode," + "A.SaleContractNo,A.DefineDate,A.Productionquantity," + "A.FirstDelivaryDate,A.ProductTreeCode,A.PlaningStartDate," + "A.RealStartDate, A.ProductionProgressMeasure, A.SubbatchQuantity " + "From   dbo.Tbl_ProductionBatchs A INNER JOIN " + "dbo.Tbl_ProductTree B ON A.ProductTreeCode = B.TreeCode";




                        DataSetConfig.FillDataSet("Tbl_ProductionBatchs", "Tbl_ProductionBatchs", SelectStr, "BatchCode");
                        Prepare_To_Show_TablesRecordList("Tbl_ProductionBatchs", " بچ های تولید");
                        break;
                    }

                case ListFormCaller.LFC_OPERATORS:
                    {
                        SelectStr = "Select OperatorCode,OperatorName,WorkingStatus," + "       CASE WorkingStatus WHEN 1 THEN 'در حال کار'" + "                          WHEN 2 THEN 'پایان کار'" + "       END AS WorkingStatusTitle,ProductionPart " + "From   Tbl_Operators";



                        DataSetConfig.FillDataSet("Tbl_Operators", "Tbl_Operators", SelectStr, "OperatorCode");
                        dsProductionPlanning.Tables["Tbl_Operators"].Columns["WorkingStatusTitle"].ReadOnly = false;
                        Prepare_To_Show_TablesRecordList("Tbl_Operators", " مشخصات اپراتورهای تولید");
                        break;
                    }

                case ListFormCaller.LFC_OPERATIONUNITS:
                    {
                        SelectStr = "Select A.OperationCode,A.UnitCode,B.Title AS UnitTitle,A.UnitIndex " + "From   dbo.Tbl_Operation_Measurement_UnitIndex A INNER JOIN " + "       dbo.Tbl_TestUnits B ON A.UnitCode = B.Code";

                        DataSetConfig.FillDataSet("Tbl_Operation_Measurement_UnitIndex", "Tbl_MeasurementUnit1", SelectStr, "OperationCode", "UnitCode");
                        Prepare_To_Show_TablesRecordList("Tbl_MeasurementUnit1", " ضرایب انجام عملیات ها");
                        break;
                    }

                case ListFormCaller.LFC_MACHINENOTAVAILABLE:
                    {
                        // mohsendokht remark
                        // SelectStr = "Select A.[ID],A.MachineCode,B.Code+' '+B.[Name] As MachineName," & _
                        // "       A.StartDate,A.StartHour," & _
                        // "       (SubString(A.StartHour,1,CharIndex('.',A.StartHour)-1))+':'+Convert(Char(2),(Round(Convert(real,SubString(A.StartHour,CharIndex('.',A.StartHour),Len(A.StartHour) - (CharIndex('.',A.StartHour)-1)))*60,0))) As MinBaseStartHour," & _
                        // "       A.EndDate,A.EndHour," & _
                        // "       (SubString(A.EndHour,1,CharIndex('.',A.EndHour)-1))+':'+Convert(Char(2),(Round(Convert(real,SubString(A.EndHour,CharIndex('.',A.EndHour),Len(A.EndHour) - (CharIndex('.',A.EndHour)-1)))*60,0))) As MinBaseEndHour,IsNull(A.ReasonCode,0) As ReasonCode,IsNull(C.ReasonTitle,'') As ReasonTitle " & _
                        // "From   dbo.Tbl_MachineNotAvailableTimes A INNER JOIN dbo.Tbl_Machines B ON A.MachineCode=B.Code Left Outer Join Tbl_MachineNotAvailableReasons C ON A.ReasonCode = C.ReasonCode"

                        SelectStr = "Select A.[ID],A.MachineCode,B.Code+' '+B.[Name] As MachineName," + "       A.StartDate,A.StartHour," + "       A.EndDate,A.EndHour," + "       IsNull(A.ReasonCode,0) As ReasonCode,IsNull(C.ReasonTitle,'') As ReasonTitle " + "From   dbo.Tbl_MachineNotAvailableTimes A INNER JOIN dbo.Tbl_Machines B ON A.MachineCode=B.Code Left Outer Join Tbl_MachineNotAvailableReasons C ON A.ReasonCode = C.ReasonCode";



                        DataSetConfig.FillDataSet("Tbl_MachineNotAvailableTimes", "Tbl_MachineNotAvailableTimes", SelectStr, "ID");
                        dsProductionPlanning.Tables["Tbl_MachineNotAvailableTimes"].Columns["MachineName"].ReadOnly = false;
                        // dsProductionPlanning.Tables("Tbl_MachineNotAvailableTimes").Columns("MinBaseStartHour").ReadOnly = False
                        // dsProductionPlanning.Tables("Tbl_MachineNotAvailableTimes").Columns("MinBaseEndHour").ReadOnly = False
                        dsProductionPlanning.Tables["Tbl_MachineNotAvailableTimes"].Columns["ReasonCode"].ReadOnly = false;
                        dsProductionPlanning.Tables["Tbl_MachineNotAvailableTimes"].Columns["ReasonTitle"].ReadOnly = false;
                        Prepare_To_Show_TablesRecordList("Tbl_MachineNotAvailableTimes", " مشخصات زمانهای در دسترس نبودن ماشین");
                        // '''''''''''''''''''''''''''''''''''''''
                        // ''''  Set User Access Right  ''''''''''
                        cmdInsert.Enabled = Module_UserAccess.HaveAccessToItem(56);
                        cmdUpdate.Enabled = Module_UserAccess.HaveAccessToItem(57);
                        cmdDelete.Enabled = Module_UserAccess.HaveAccessToItem(58);
                        break;
                    }
                // '''''''''''''''''''''''''''''''''''''''
                // '''''''''''''''''''''''''''''''''''''''
                case ListFormCaller.LFC_HALTREASON:
                    {
                        DataSetConfig.FillDataSet("Tbl_HaltReasons", "Tbl_HaltReasons", "Select * From Tbl_HaltReasons", "ReasonCode");
                        Prepare_To_Show_TablesRecordList("Tbl_HaltReasons", " مشخصات علت های توقف عملیات");
                        break;
                    }

                case ListFormCaller.LFC_BATCHSPRODUCTIONPROGRESS:
                    {
                        cmdInsert.Visible = false;
                        cmdUpdate.Visible = false;
                        cmdDelete.Visible = false;
                        SelectStr = "Select A.BatchCode, B.ProductCode, C.ProductName,A.Productionquantity," + "       dbo.GetBatchProductionProgress(A.BatchCode) As ProductionProgressMeasure " + "From   dbo.Tbl_ProductionBatchs A INNER JOIN " + "       dbo.Tbl_ProductTree B ON A.ProductTreeCode=B.TreeCode INNER JOIN " + "       dbo.Tbl_Products C ON B.ProductCode = C.ProductCode";



                        DataSetConfig.FillDataSet("Tbl_ProductionBatchs", "Tbl_ProductionBatchs", SelectStr, "BatchCode");
                        Prepare_To_Show_TablesRecordList("Tbl_ProductionBatchs", " نمایش پیشرفت تولید بچ");
                        break;
                    }

                case ListFormCaller.LFC_CUSTOMERS:
                    {
                        DataSetConfig.FillDataSet("Tbl_Customers", "Tbl_Customers", "Select * From Tbl_Customers", "CustomerCode");
                        Prepare_To_Show_TablesRecordList("Tbl_Customers", " مشخصات مشتریان");
                        break;
                    }

                case ListFormCaller.LFC_OEE:
                    {
                        DataSetConfig.FillDataSet("Tbl_OEEs", "Tbl_OEEs", "Select * From Tbl_OEEs", "OEEIndex");
                        Prepare_To_Show_TablesRecordList("Tbl_OEEs", "محاسبۀ OEE ");
                        break;
                    }
            }
        }

        private void frmRecordsLists_FormClosing(object sender, FormClosingEventArgs e)
        {
            Module1.SaveGridColumnsWidth(Name, this.CallerForm, dgList);
            dgList.DataSource = null;
            DataSetConfig = null;
            SearchMode = -1;
            FoundRows = null;
            FoundRowsEnumerator = null;
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            DialogResult DetailsResult = DialogResult.Cancel;

            if (ReferenceEquals(sender, cmdInsert))
            {
                mFormMode = (int)Module1.FormModeEnum.INSERT_MODE;
            }
            else if (ReferenceEquals(sender, cmdUpdate))
            {
                mFormMode = (int)Module1.FormModeEnum.EDIT_MODE;
            }
            else if (ReferenceEquals(sender, cmdDelete))
            {
                mFormMode = (int)Module1.FormModeEnum.DELETE_MODE;
            }

            if (dgList.Rows.Count == 0 && (mFormMode == (int)Module1.FormModeEnum.EDIT_MODE || mFormMode == (int)Module1.FormModeEnum.DELETE_MODE))
            {
                MessageBox.Show("رکوردی وجود ندارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                return;
            }

            if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE && Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(Tag, ListFormCaller.LFC_OEE, false)))
            {
                if (MessageBox.Show("مطمئن هستید OEE آیا برای حذف سابقۀ", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading) == DialogResult.Yes)
                {
                    using (var cn = new System.Data.SqlClient.SqlConnection(Module1.PlanningCnnStr))
                    {
                        System.Data.SqlClient.SqlTransaction trnDelete = null;
                        try
                        {
                            cn.Open();
                            trnDelete = cn.BeginTransaction();
                            var cmDelete = new System.Data.SqlClient.SqlCommand(Conversions.ToString(Operators.ConcatenateObject("Delete From Tbl_OEEDetails Where OEEIndex = ", dgList.CurrentRow.Cells["OEEIndex"].Value)), cn);
                            cmDelete.Transaction = trnDelete;
                            cmDelete.ExecuteNonQuery();
                            cmDelete.CommandText = Conversions.ToString(Operators.ConcatenateObject("Delete From Tbl_OEEs Where OEEIndex = ", dgList.CurrentRow.Cells["OEEIndex"].Value));
                            cmDelete.ExecuteNonQuery();
                            var drDelete = GetRow();
                            if (drDelete is object)
                            {
                                drDelete.Delete();
                            }

                            trnDelete.Commit();
                            dsProductionPlanning.AcceptChanges();
                        }
                        catch (Exception ex)
                        {
                            dsProductionPlanning.RejectChanges();
                            trnDelete.Rollback();
                            Logger.SaveError("frmrecordsLists.cmdDelete_Click", ex.Message);
                            MessageBox.Show("با اشکال مواجه شد OEE حذف", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        }
                        finally
                        {
                            trnDelete.Dispose();
                            if (cn.State == ConnectionState.Open)
                                cn.Close();
                        }
                    }
                }

                return;
            }

            string SelectStr;
            //var objForm = new frmOperationUnit() ;
            
            ForeignKeyConstraint fkColumn;
            switch (this.CallerForm)
            {
                case ListFormCaller.LFC_OPERATRIONSTITLES:
                    {
                        var objForm = new frmOperationTitle();
                        objForm.ListForm = this;
                        objForm.ListForm.FormMode = mFormMode;
                        if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
                        {
                            objForm.Controls["Panel1"].Controls["cmdSave"].Visible = false;
                            objForm.Controls["Panel1"].Controls["cmdDelete"].Visible = true;
                        }
                        DetailsResult = objForm.ShowDialog();
                        objForm.Dispose();
                        break;
                    }

                case ListFormCaller.LFC_PRODUCTIONPARTS:
                    {
                        DataSetConfig.FillDataSet("Tbl_Calendar", "Tbl_Calendar", "Select * From Tbl_Calendar Order By CalendarTitle", "CalendarCode");
                        var objForm = new frmNature();
                        objForm.ListForm = this;
                        objForm.ListForm.FormMode = mFormMode;
                        if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
                        {
                            objForm.Controls["Panel1"].Controls["cmdSave"].Visible = false;
                            objForm.Controls["Panel1"].Controls["cmdDelete"].Visible = true;
                        }
                        DetailsResult = objForm.ShowDialog();
                        objForm.Dispose();
                        break;
                    }

                case ListFormCaller.LFC_SUPPLIERS:
                    {
                        var objForm = new frmSupplier();
                        objForm.ListForm = this;
                        objForm.ListForm.FormMode = mFormMode;
                        if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
                        {
                            objForm.Controls["Panel1"].Controls["cmdSave"].Visible = false;
                            objForm.Controls["Panel1"].Controls["cmdDelete"].Visible = true;
                        }
                        DetailsResult = objForm.ShowDialog();
                        objForm.Dispose();
                        break;
                    }

                case ListFormCaller.LFC_TESTUNITS:
                    {
                        var objForm = new frmTestUnit();
                        objForm.ListForm = this;
                        objForm.ListForm.FormMode = mFormMode;
                        if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
                        {
                            objForm.Controls["Panel1"].Controls["cmdSave"].Visible = false;
                            objForm.Controls["Panel1"].Controls["cmdDelete"].Visible = true;
                        }
                        DetailsResult = objForm.ShowDialog();
                        objForm.Dispose();
                        break;
                    }

                case ListFormCaller.LFC_UNITSRELATIONS:
                    {
                        DataSetConfig.FillDataSet("Tbl_TestUnits", "Tbl_TestUnits_Base", "Select * From Tbl_TestUnits Order By Title", "Code");
                        DataSetConfig.FillDataSet("Tbl_TestUnits", "Tbl_TestUnits_Related", "Select * From Tbl_TestUnits Order By Title", "Code");
                        var objForm = new frmUnitsRelation();
                        objForm.ListForm = this;
                        objForm.ListForm.FormMode = mFormMode;
                        if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
                        {
                            objForm.Controls["Panel1"].Controls["cmdSave"].Visible = false;
                            objForm.Controls["Panel1"].Controls["cmdDelete"].Visible = true;
                        }
                        DetailsResult = objForm.ShowDialog();
                        objForm.Dispose();
                        break;
                    }

                case ListFormCaller.LFC_PRIMARYMATERIALS:
                    {
                        DataSetConfig.FillDataSet("Tbl_TestUnits", "Tbl_StoreTestUnits", "Select * From Tbl_TestUnits Order By Title", "Code");
                        DataSetConfig.FillDataSet("Tbl_Stores", "Tbl_Stores", "Select * From Tbl_Stores Order By StoreName", "StoreCode");

                        // تنظیم محدودیت کلید خارجی برای جدول مواد اولیه و جدول واحدهای سنجش
                        fkColumn = new ForeignKeyConstraint("fkColumn__TestUnits_v_PrimaryMaterials", dsProductionPlanning.Tables["Tbl_StoreTestUnits"].Columns["Code"], dsProductionPlanning.Tables["Tbl_PrimaryMaterials"].Columns["StoreUnit"]);
                        fkColumn.AcceptRejectRule = AcceptRejectRule.None;
                        fkColumn.DeleteRule = Rule.None;
                        fkColumn.UpdateRule = Rule.Cascade;
                        dsProductionPlanning.Tables["Tbl_PrimaryMaterials"].Constraints.Add(fkColumn);

                        // تنظیم رابطه بین جدول مواد اولیه و جدول واحدهای سنجش بوسیله ستون کد واحد سنجش
                        dsProductionPlanning.Relations.Add("StoreTestUnits_PrimaryMaterials", dsProductionPlanning.Tables["Tbl_StoreTestUnits"].Columns["Code"], dsProductionPlanning.Tables["Tbl_PrimaryMaterials"].Columns["StoreUnit"]);
                        var objForm = new frmPrimaryMaterial();
                        objForm.ListForm = this;
                        objForm.ListForm.FormMode = mFormMode;
                        if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
                        {
                            objForm.Controls["Panel1"].Controls["cmdSave"].Visible = false;
                            objForm.Controls["Panel1"].Controls["cmdDelete"].Visible = true;
                        }
                        DetailsResult = objForm.ShowDialog();
                        objForm.Dispose();
                        break;
                    }

                case ListFormCaller.LFC_MACHINES:
                    {
                        DataSetConfig.FillDataSet("Tbl_Calendar", "Tbl_Calendar", "Select * From Tbl_Calendar Order By CalendarTitle", "CalendarCode");

                        // تنظیم محدودیت کلید خارجی برای جدول تقویمهای کاری و جدول ماشین آلات
                        fkColumn = new ForeignKeyConstraint("fkColumn__Calendar_v_Machines", dsProductionPlanning.Tables["Tbl_Calendar"].Columns["CalendarCode"], dsProductionPlanning.Tables["Tbl_Machines"].Columns["CalendarCode"]);
                        fkColumn.AcceptRejectRule = AcceptRejectRule.None;
                        fkColumn.DeleteRule = Rule.None;
                        fkColumn.UpdateRule = Rule.Cascade;
                        dsProductionPlanning.Tables["Tbl_Machines"].Constraints.Add(fkColumn);

                        // تنظیم رابطه بین جدول جدول ماشین آلات جایگزین و جدول ماشین آلات بوسیله ستون کد ماشین
                        dsProductionPlanning.Relations.Add("Calendar_Machines", dsProductionPlanning.Tables["Tbl_Calendar"].Columns["CalendarCode"], dsProductionPlanning.Tables["Tbl_Machines"].Columns["CalendarCode"]);
                        var objForm = new frmMachine();
                        objForm.ListForm = this;
                        objForm.ListForm.FormMode = mFormMode;
                        if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
                        {
                            objForm.Controls["Panel1"].Controls["cmdSave"].Visible = false;
                            objForm.Controls["Panel1"].Controls["cmdDelete"].Visible = true;
                        }
                        DetailsResult = objForm.ShowDialog();
                        objForm.Dispose();
                        break;
                    }

                case ListFormCaller.LFC_MACHINENOTAVAILABLEREASON:
                    {
                        var objForm = new frmMachineNotAvailableReason();
                        objForm.ListForm = this;
                        objForm.ListForm.FormMode = mFormMode;
                        if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
                        {
                            objForm.Controls["Panel1"].Controls["cmdSave"].Visible = false;
                            objForm.Controls["Panel1"].Controls["cmdDelete"].Visible = true;
                        }
                        DetailsResult = objForm.ShowDialog();
                        objForm.Dispose();
                        break;
                    }

                case ListFormCaller.LFC_CONTRACTORS:
                    {
                        var objForm = new frmContractor();
                        objForm.ListForm = this;
                        objForm.ListForm.FormMode = mFormMode;
                        if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
                        {
                            objForm.Controls["Panel1"].Controls["cmdSave"].Visible = false;
                            objForm.Controls["Panel1"].Controls["cmdDelete"].Visible = true;
                        }
                        DetailsResult = objForm.ShowDialog();
                        objForm.Dispose();
                        break;
                    }

                case ListFormCaller.LFC_STORES:
                    {
                        var objForm = new frmStore();
                        objForm.ListForm = this;
                        objForm.ListForm.FormMode = mFormMode;
                        if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
                        {
                            objForm.Controls["Panel1"].Controls["cmdSave"].Visible = false;
                            objForm.Controls["Panel1"].Controls["cmdDelete"].Visible = true;
                        }
                        DetailsResult = objForm.ShowDialog();
                        objForm.Dispose();
                        break;
                    }

                case ListFormCaller.LFC_HOLIDAYS:
                    {
                        var objForm = new frmHoliday();
                        objForm.ListForm = this;
                        objForm.ListForm.FormMode = mFormMode;
                        if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
                        {
                            objForm.Controls["Panel1"].Controls["cmdSave"].Visible = false;
                            objForm.Controls["Panel1"].Controls["cmdDelete"].Visible = true;
                        }
                        DetailsResult = objForm.ShowDialog();
                        objForm.Dispose();
                        break;
                    }

                case ListFormCaller.LFC_ACTINGCALENDARS:
                    {
                        string mCalendarCode;
                        if (mFormMode != (int)Module1.FormModeEnum.INSERT_MODE)
                        {
                            mCalendarCode = dgList.SelectedRows[0].Cells["CalendarCode"].Value.ToString();
                        }
                        else
                        {
                            mCalendarCode = "-1";
                        }

                        DataSetConfig.FillDataSet("Tbl_CalendarShifts", "Tbl_CalendarShifts", "Select * From Tbl_CalendarShifts Where CalendarCode = " + mCalendarCode, "CalendarCode", "ShiftNo");
                        DataSetConfig.FillDataSet("Tbl_CalendarDays", "Tbl_CalendarDays", "Select * From Tbl_CalendarDays Where CalendarCode = " + mCalendarCode, "CalendarCode", "ShiftNo", "DayNo");
                        DataSetConfig.FillDataSet("Tbl_CalendarParticularShifts", "Tbl_CalendarParticularShifts", "Select * From Tbl_CalendarParticularShifts Where CalendarCode = " + mCalendarCode, "CalendarCode", "ShamsiDate", "ShiftNo");
                        DataSetConfig.FillDataSet("Tbl_CalendarParticularDays", "Tbl_CalendarParticularDays", "Select * From Tbl_CalendarParticularDays Where CalendarCode = " + mCalendarCode, "CalendarCode", "ShamsiDate");
                        DataSetConfig.FillDataSet("Tbl_HoliDays", "Tbl_HoliDays", "Select * From Tbl_HoliDays", "DayNo", "MonthNo");
                        DataSetConfig.FillDataSet("Tbl_CalendarShiftDownTimes", "Tbl_CalendarShiftDownTimes", "Select * From Tbl_CalendarShiftDownTimes Where CalendarCode = " + mCalendarCode, "CalendarCode", "ShiftNo", "DownTimeStart");
                        DataSetConfig.FillDataSet("Tbl_CalendarDaysDownTimes", "Tbl_CalendarDaysDownTimes", "Select * From Tbl_CalendarDaysDownTimes Where CalendarCode = " + mCalendarCode, "CalendarCode", "ShiftNo", "DayNo", "DownTimeStart");
                        DataSetConfig.FillDataSet("Tbl_ParticularShiftDownTimes", "Tbl_ParticularShiftDownTimes", "Select * From Tbl_ParticularShiftDownTimes Where CalendarCode = " + mCalendarCode, "CalendarCode", "ShamsiDate", "ShiftNo", "DownTimeStart");
                        var objForm = new frmCalendar();
                        objForm.ListForm = this;
                        objForm.ListForm.FormMode = mFormMode;
                        if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
                        {
                            objForm.Controls["Panel1"].Controls["cmdSave"].Visible = false;
                            objForm.Controls["Panel1"].Controls["cmdDelete"].Visible = true;
                        }
                        DetailsResult = objForm.ShowDialog();
                        objForm.Dispose();
                        break;
                    }

                case ListFormCaller.LFC_PRODUCTS:
                    {
                        var objForm = new frmProduct();
                        objForm.ListForm = this;
                        objForm.ListForm.FormMode = mFormMode;
                        if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
                        {
                            objForm.Controls["Panel1"].Controls["cmdSave"].Visible = false;
                            objForm.Controls["Panel1"].Controls["cmdDelete"].Visible = true;
                        }
                        DetailsResult = objForm.ShowDialog();
                        objForm.Dispose();
                        break;
                    }

                case ListFormCaller.LFC_PRODUCTIONBATCHS:
                    {
                        short EditMode = -1;
                        if (mFormMode == (int)Module1.FormModeEnum.EDIT_MODE)
                        {
                            var cmEditRight = new System.Data.SqlClient.SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Select Count(*) From Tbl_ProductionSubbatchs Where PlanningStartDate>0 And BatchCode='", dgList.CurrentRow.Cells["BatchCode"].Value), "'")), Module1.cnProductionPlanning);
                            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                                Module1.cnProductionPlanning.Open();
                            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(cmEditRight.ExecuteScalar(), 0, false)))
                            {
                                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                    Module1.cnProductionPlanning.Close();
                                cmEditRight.Dispose();
                                EditMode = 1; // هیچ ساب بچی برنامه ریزی نشده است
                                goto FillDataSet;
                            }

                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();

                            // کنترل اینکه آیا تمامی ساب بچها وارد مرحله تولید شده اند یا نه
                            cmEditRight.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Select Count(*) From Tbl_ProductionSubbatchs Where BatchCode='", dgList.CurrentRow.Cells["BatchCode"].Value), "' And (NOT (SubbatchCode IN(SELECT SubbatchCode FROM Tbl_RealProduction)))"));
                            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                                Module1.cnProductionPlanning.Open();
                            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(cmEditRight.ExecuteScalar(), 0, false)))
                            {
                                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                    Module1.cnProductionPlanning.Close();
                                cmEditRight.Dispose();
                                MessageBox.Show("بچ انتخاب شده مجاز به تغییر اطلاعات نمی باشد" + Constants.vbCrLf + "تمامی ساب بچ ها وارد مرحله تولید شده اند", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                                return;
                            }
                            else
                            {
                                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                    Module1.cnProductionPlanning.Close();
                                cmEditRight.Dispose();
                                EditMode = 2; // برخی از ساب بچ ها وارد مرحله تولید شده اند
                                goto FillDataSet;
                            }
                        }
                        else if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
                        {
                            var cmDeleteRight = new System.Data.SqlClient.SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Select Count(*) From Tbl_ProductionSubbatchs Where BatchCode='", dgList.CurrentRow.Cells["BatchCode"].Value), "' And (SubbatchCode IN(SELECT SubbatchCode FROM Tbl_RealProduction))")), Module1.cnProductionPlanning);
                            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                                Module1.cnProductionPlanning.Open();
                            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(cmDeleteRight.ExecuteScalar(), 0, false)))
                            {
                                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                    Module1.cnProductionPlanning.Close();
                                cmDeleteRight.Dispose();
                                MessageBox.Show("بچ انتخاب شده مجاز به حذف نمی باشد" + Constants.vbCrLf + "بعضی از ساب بچ ها وارد مرحله تولید شده اند", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                                return;
                            }

                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                            cmDeleteRight.Dispose();
                        }

                    FillDataSet:
                        ;
                        DataSetConfig.FillDataSet("Tbl_Products", "Tbl_Products", "Select * From Tbl_Products Order By ProductName", "ProductCode");
                        DataSetConfig.FillDataSet("Tbl_ProductTree", "Tbl_ProductTree", "Select * From Tbl_ProductTree Order By TreeTitle", "TreeCode");
                        if (FormMode != (int)Module1.FormModeEnum.INSERT_MODE)
                        {
                            string SubbatchsCode = Constants.vbNullString;
                            DataSetConfig.FillDataSet("Tbl_ProductionSubbatchs", "Tbl_ProductionSubbatchs", Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Select * From Tbl_ProductionSubbatchs Where BatchCode='", dgList.CurrentRow.Cells["BatchCode"].Value), "'")), "SubbatchCode");
                            DataSetConfig.FillDataSet("Tbl_RealProduction", "Tbl_RealProduction", "Select * FROM Tbl_RealProduction", "ProductionCode");
                            var loopTo = dsProductionPlanning.Tables["Tbl_ProductionSubbatchs"].Rows.Count - 1;
                            for (I = 0; I <= loopTo; I++)
                            {
                                if (I == 0)
                                {
                                    SubbatchsCode = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("'", dsProductionPlanning.Tables["Tbl_ProductionSubbatchs"].Rows[I]["SubbatchCode"]), "'"));
                                }
                                else
                                {
                                    SubbatchsCode = Conversions.ToString(SubbatchsCode + Operators.ConcatenateObject(Operators.ConcatenateObject(",'", dsProductionPlanning.Tables["Tbl_ProductionSubbatchs"].Rows[I]["SubbatchCode"]), "'"));
                                }
                            }

                            DataSetConfig.FillDataSet("Tbl_Planning", "Tbl_Planning", "SELECT * FROM Tbl_Planning Where SubbatchCode IN(" + SubbatchsCode + ")", "PlanningCode");
                        }
                        else
                        {
                            DataSetConfig.FillDataSet("Tbl_ProductionSubbatchs", "Tbl_ProductionSubbatchs", "Select * From Tbl_ProductionSubbatchs  Where BatchCode=''", "SubbatchCode");
                            DataSetConfig.FillDataSet("Tbl_RealProduction", "Tbl_RealProduction", "Select * From Tbl_RealProduction", "ProductionCode");
                            DataSetConfig.FillDataSet("Tbl_Planning", "Tbl_Planning", "Select * From Tbl_Planning Where SubbatchCode=''", "PlanningCode");
                        }

                        // ----------------- تنظیم محدودیت کلید خارجی بین ستونها --------------------
                        // تنظیم محدودیت کلید خارجی برای جدول بج های تولید و جدول محصولات
                        fkColumn = new ForeignKeyConstraint("fkColumn__Products_v_ProductionBatchs", dsProductionPlanning.Tables["Tbl_Products"].Columns["ProductCode"], dsProductionPlanning.Tables["Tbl_ProductionBatchs"].Columns["ProductCode"]);
                        fkColumn.AcceptRejectRule = AcceptRejectRule.None;
                        fkColumn.DeleteRule = Rule.None;
                        fkColumn.UpdateRule = Rule.Cascade;
                        dsProductionPlanning.Tables["Tbl_ProductionBatchs"].Constraints.Add(fkColumn);
                        // تنظیم محدودیت کلید خارجی برای جدول بج های تولید و جدول درختهای محصول
                        fkColumn = new ForeignKeyConstraint("fkColumn__ProductTree_v_ProductionBatchs", dsProductionPlanning.Tables["Tbl_ProductTree"].Columns["TreeCode"], dsProductionPlanning.Tables["Tbl_ProductionBatchs"].Columns["ProductTreeCode"]);
                        fkColumn.AcceptRejectRule = AcceptRejectRule.None;
                        fkColumn.DeleteRule = Rule.None;
                        fkColumn.UpdateRule = Rule.Cascade;
                        dsProductionPlanning.Tables["Tbl_ProductionBatchs"].Constraints.Add(fkColumn);
                        // تنظیم محدودیت کلید خارجی برای جدول بج های تولید و جدول ساب بچ های تولید
                        fkColumn = new ForeignKeyConstraint("fkColumn_ProductionBatchs_v_ProductionSubbatchs", dsProductionPlanning.Tables["Tbl_ProductionBatchs"].Columns["BatchCode"], dsProductionPlanning.Tables["Tbl_ProductionSubbatchs"].Columns["BatchCode"]);
                        fkColumn.AcceptRejectRule = AcceptRejectRule.None;
                        fkColumn.DeleteRule = Rule.None;
                        fkColumn.UpdateRule = Rule.Cascade;
                        dsProductionPlanning.Tables["Tbl_ProductionSubbatchs"].Constraints.Add(fkColumn);

                        // ----------------- تنظیم روابط بین ستونها --------------------
                        // تنظیم رابطه بین جدول جدول بچ های تولید و جدول محصولات بوسیله ستون کد محصول
                        dsProductionPlanning.Relations.Add("Products_ProductionBatchs", dsProductionPlanning.Tables["Tbl_Products"].Columns["ProductCode"], dsProductionPlanning.Tables["Tbl_ProductionBatchs"].Columns["ProductCode"]);
                        // تنظیم رابطه بین جدول جدول بچ های تولید و جدول درختهای محصول بوسیله ستون کد درخت
                        dsProductionPlanning.Relations.Add("ProductTree_ProductionBatchs", dsProductionPlanning.Tables["Tbl_ProductTree"].Columns["TreeCode"], dsProductionPlanning.Tables["Tbl_ProductionBatchs"].Columns["ProductTreeCode"]);
                        // تنظیم رابطه بین جدول جدول بچ های تولید و جدول ساب بچ های تولید بوسیله ستون کد بچ
                        dsProductionPlanning.Relations.Add("ProductionBatchs_ProductionSubbatchs", dsProductionPlanning.Tables["Tbl_ProductionBatchs"].Columns["BatchCode"], dsProductionPlanning.Tables["Tbl_ProductionSubbatchs"].Columns["BatchCode"]);
                        frmProductionBatch objForm = new frmProductionBatch();
                        if (mFormMode == (int)Module1.FormModeEnum.EDIT_MODE)
                        {
                            objForm.EditMode =(short) EditMode;
                        }
                        if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
                        {
                            objForm.Controls["Panel1"].Controls["cmdSave"].Visible = false;
                            objForm.Controls["Panel1"].Controls["cmdDelete"].Visible = true;
                        }

                        DetailsResult = objForm.ShowDialog();
                        objForm.Dispose();
                        break;
                    }

                case ListFormCaller.LFC_PRODUCTIONPLANING:
                    {
                        var objForm = new frmPlaningSubbatchsList();
                        
                        if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
                        {
                            objForm.Controls["Panel1"].Controls["cmdSave"].Visible = false;
                            objForm.Controls["Panel1"].Controls["cmdDelete"].Visible = true;
                        }
                        DetailsResult = objForm.ShowDialog();
                        objForm.Dispose();
                        break;
                    }

                case ListFormCaller.LFC_OPERATORS:
                    {
                        DataSetConfig.FillDataSet("Tbl_Natures", "Tbl_Natures", "Select * From Tbl_Natures Order By NatureTitle", "NatureCode");
                        if (mFormMode == (int)Module1.FormModeEnum.INSERT_MODE)
                        {
                            DataSetConfig.FillDataSet("Tbl_OperatorWorkPeriods", "Tbl_OperatorWorkPeriods", "Select * From Tbl_OperatorWorkPeriods Where OperatorCode = '-1' Order By StartDate", "OperatorCode", "StartDate");
                        }
                        else
                        {
                            DataSetConfig.FillDataSet("Tbl_OperatorWorkPeriods", "Tbl_OperatorWorkPeriods", Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Select * From Tbl_OperatorWorkPeriods Where OperatorCode = '", dgList.SelectedRows[0].Cells["OperatorCode"].Value), "' Order By StartDate")), "OperatorCode", "StartDate");
                        }

                        var objForm = new frmOperator();
                        objForm.ListForm = this;
                        objForm.ListForm.FormMode = mFormMode;
                        if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
                        {
                            objForm.Controls["Panel1"].Controls["cmdSave"].Visible = false;
                            objForm.Controls["Panel1"].Controls["cmdDelete"].Visible = true;
                        }
                        DetailsResult = objForm.ShowDialog();
                        objForm.Dispose();
                        break;
                    }

                case ListFormCaller.LFC_OPERATIONUNITS:
                    {
                        SelectStr = "Select   Distinct OperationCode,OperationCode+' '+OperationTitle As OperationTitle " + "From     dbo.Tbl_ProductOPCs " + "Order By OperationCode";
                        DataSetConfig.FillDataSet("TblOperations", "TblOperations", SelectStr, "OperationCode");
                        DataSetConfig.FillDataSet("Tbl_TestUnits", "Tbl_TestUnits", "Select * From Tbl_TestUnits", "Code");
                        frmOperationUnit objForm = new frmOperationUnit();
                                                
                        objForm.dsOperationUnit = DataSetConfig.dsProductionPlanning;
                        objForm.SetFormMode((short) mFormMode);
                        switch (mFormMode)
                        {
                            case (int)Module1.FormModeEnum.EDIT_MODE:
                            case (int)Module1.FormModeEnum.DELETE_MODE:
                                {
                                    objForm.CurrentRow = GetRow();
                                    break;
                                }
                        }

                        objForm.SetFormMode((short)mFormMode);
                        
                        if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
                        {
                            objForm.Controls["Panel1"].Controls["cmdSave"].Visible = false;
                            objForm.Controls["Panel1"].Controls["cmdDelete"].Visible = true;
                        }
                        DetailsResult = objForm.ShowDialog();
                        objForm.Dispose();
                        break;
                    }

                case ListFormCaller.LFC_MACHINENOTAVAILABLE:
                    {
                        DataSetConfig.FillDataSet("Tbl_Machines", "Tbl_Machines", "Select Code,Code+' '+Name As Name From Tbl_Machines Where Code<>'-1' Order By Code", "Code");
                        DataSetConfig.FillDataSet("Tbl_MachineNotAvailableReasons", "Tbl_MachineNotAvailableReasons", "Select * From Tbl_MachineNotAvailableReasons", "ReasonCode");
                        var objForm = new frmMachineNotAvailable();
                        objForm.ListForm = this;
                        objForm.ListForm.FormMode = mFormMode;
                        if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
                        {
                            objForm.Controls["Panel1"].Controls["cmdSave"].Visible = false;
                            objForm.Controls["Panel1"].Controls["cmdDelete"].Visible = true;
                        }
                        DetailsResult = objForm.ShowDialog();
                        objForm.Dispose();
                        break;
                    }

                case ListFormCaller.LFC_HALTREASON:
                    {
                        var objForm = new frmHaltReason();
                        objForm.ListForm = this;
                        objForm.ListForm.FormMode = mFormMode;
                        if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
                        {
                            objForm.Controls["Panel1"].Controls["cmdSave"].Visible = false;
                            objForm.Controls["Panel1"].Controls["cmdDelete"].Visible = true;
                        }
                        DetailsResult = objForm.ShowDialog();
                        objForm.Dispose();
                        break;
                    }

                case ListFormCaller.LFC_CUSTOMERS:
                    {
                        var objForm = new frmCustomer();
                        objForm.ListForm = this;
                        objForm.ListForm.FormMode = mFormMode;
                        if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
                        {
                            objForm.Controls["Panel1"].Controls["cmdSave"].Visible = false;
                            objForm.Controls["Panel1"].Controls["cmdDelete"].Visible = true;
                        }
                        DetailsResult = objForm.ShowDialog();
                        objForm.Dispose();
                        break;
                    }

                case ListFormCaller.LFC_OEE:
                    {
                        var objForm = new frmOEE();
                        objForm.ListForm = this;
                        objForm.ListForm.FormMode = mFormMode;
                        if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
                        {
                            objForm.Controls["Panel1"].Controls["cmdSave"].Visible = false;
                            objForm.Controls["Panel1"].Controls["cmdDelete"].Visible = true;
                        }
                        objForm.MdiParent = MdiParent;
                        objForm.Show();
                        break;
                    }

                default:
                    {
                        DetailsResult = DialogResult.Cancel;
                        break;
                        //return;
                    }
            }

            //int DetailsResult = -1;
            //if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(Tag, ListFormCaller.LFC_OPERATIONUNITS, false)))
            //{
            //    objForm.ListForm = this;
            //    objForm.ListForm.FormMode = mFormMode;
            //}

            //if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
            //{
            //    objForm.Controls("Panel1").Controls("cmdSave").Visible = false;
            //    objForm.Controls("Panel1").Controls("cmdDelete").Visible = true;
            //}

            //if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(Tag, ListFormCaller.LFC_OEE, false)))
            //{
            //    //objForm.MdiParent = MdiParent;
            //    //objForm.Show();
            //}
            //else
            //{
            //    DetailsResult = Conversions.ToInteger(objForm.ShowDialog());
            //    objForm.Dispose();
            //}

            if (DetailsResult != DialogResult.OK )
            {
                switch (FormMode)
                {
                    case (int)Module1.FormModeEnum.INSERT_MODE:
                        {
                            BindingContext[dsProductionPlanning, CurrentTableName].Position = BindingContext[dsProductionPlanning, CurrentTableName].Count;
                            break;
                        }

                    case (int)Module1.FormModeEnum.DELETE_MODE:
                        {
                            if (BindingContext[dsProductionPlanning, CurrentTableName].Count > 0)
                            {
                                BindingContext[dsProductionPlanning, CurrentTableName].Position = 0;
                            }

                            break;
                        }
                }
            }
        }

        private void frmRecordsLists_Resize(object sender, EventArgs e)
        {
            tsslSearchMode.Width = (int)Math.Round(0.15d * StatusStrip1.Width);
            ToolStripStatusLabel1.Width = (int)Math.Round(0.14d * StatusStrip1.Width);
            ToolStripStatusLabel2.Width = (int)Math.Round(0.14d * StatusStrip1.Width);
            ToolStripStatusLabel3.Width = (int)Math.Round(0.15d * StatusStrip1.Width);
            ToolStripStatusLabel4.Width = (int)Math.Round(0.15d * StatusStrip1.Width);
            tsslRecNo.Width = (int)Math.Round(0.25d * StatusStrip1.Width);

            // If dgList.Tag <> -1 Then
            // SetSearchControlsLocation()
            // End If
        }

        private void dgList_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            // If dgList.Tag <> -1 Then
            // SetSearchControlsLocation()
            // End If
        }

        private void dgList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int RecordCount = Conversions.ToInteger(Interaction.IIf(dgList.DataSource is DataSet, BindingContext[dsProductionPlanning, CurrentTableName].Count, dgList.Rows.Count));
            int CurrentRecord = e.RowIndex + 1;
            tsslRecNo.Text = "رکورد  " + CurrentRecord.ToString() + "  از  " + RecordCount.ToString();
        }

        private void dgList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgList.Columns[e.ColumnIndex].Name.Equals("StartDate") || dgList.Columns[e.ColumnIndex].Name.Equals("EndDate") || dgList.Columns[e.ColumnIndex].Name.Equals("CalcDate") || dgList.Columns[e.ColumnIndex].Name.Equals("StartDate"))
            {
                e.Value = Strings.Mid(Conversions.ToString(e.Value), 1, 4) + "/" + Strings.Mid(Conversions.ToString(e.Value), 5, 2) + "/" + Strings.Mid(Conversions.ToString(e.Value), 7, 2);
            }

            if (e.RowIndex > -1)
            {
                dgList.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
            }
        }

        private void cmdFilter_Click(object sender, EventArgs e)
        {
            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgList.Tag, -1, false)))
            {
                if (ReferenceEquals(sender, cmdFilter))
                {
                    dgList.Tag = SearchModeEnum.SM_FILTER;
                    tsslSearchMode.Text = "فیلتر اطلاعات";
                    SetFilterCombosLocation();
                }
                else
                {
                    dgList.Tag = SearchModeEnum.SM_FIND;
                    tsslSearchMode.Text = "جستجوی اطلاعات";
                    SetSearchControlsLocation();
                }

                dgList.ColumnHeadersHeight = 40;
                dgList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft;
            }
            else
            {
                if (ReferenceEquals(sender, cmdFilter))
                {
                    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgList.Tag, SearchModeEnum.SM_FILTER, false)))
                    {
                        for (I = 1; I <= 8; I++)
                            Controls["Panel1"].Controls["cbFilter" + I].Visible = false;
                        dgList.ColumnHeadersHeight = 30;
                        dgList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dgList.Tag = -1;
                        tsslSearchMode.Text = "نمایش کلی";
                    }
                    else if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgList.Tag, SearchModeEnum.SM_FIND, false)))
                    {
                        dgList.Tag = SearchModeEnum.SM_FILTER;
                        tsslSearchMode.Text = "فیلتر اطلاعات";
                        SetFilterCombosLocation();
                    }
                }
                else if (ReferenceEquals(sender, cmdFind))
                {
                    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgList.Tag, SearchModeEnum.SM_FIND, false)))
                    {
                        for (I = 1; I <= 8; I++)
                            Controls["Panel1"].Controls["txtSearch" + I].Visible = false;
                        dgList.ColumnHeadersHeight = 30;
                        dgList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dgList.Tag = -1;
                        tsslSearchMode.Text = "نمایش کلی";
                    }
                    else if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgList.Tag, SearchModeEnum.SM_FILTER, false)))
                    {
                        dgList.Tag = SearchModeEnum.SM_FIND;
                        tsslSearchMode.Text = "جستجوی اطلاعات";
                        SetSearchControlsLocation();
                    }
                }

                if (dgList.DataSource is DataView)
                {
                    ((DataView)dgList.DataSource).RowFilter = Constants.vbNullString;
                }
            }
        }

        private void cbFilters_DropDown(object sender, EventArgs e)
        {
            ComboBox CurrentCombo;
            CurrentCombo = (ComboBox)sender;
            CurrentCombo.Items.Clear();
            CurrentCombo.Items.Add("---< همه >---");
            for (short ItemCounter = 0, loopTo = (short)(dgList.Rows.Count - 1); ItemCounter <= loopTo; ItemCounter++)
            {
                if (!CurrentCombo.Items.Contains(dgList.Rows[(int)ItemCounter].Cells[CurrentCombo.Tag.ToString()].Value))
                {
                    CurrentCombo.Items.Add(dgList.Rows[(int)ItemCounter].Cells[CurrentCombo.Tag.ToString()].Value);
                }
            }
        }

        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterValue = Constants.vbNullString;
            ComboBox CurrentCombo = (ComboBox)sender;
            ComboBox CurrentVisibleCombo;
            string Control;
            if (CurrentCombo.Text == "---< همه >---")
            {
                CurrentCombo.SelectedIndex = -1;
            }

            for (short ComboCounter = 1; ComboCounter <= 8; ComboCounter++)
            {
                Control = "cbFilter" + ComboCounter;
                if (Controls["Panel1"].Controls[Control].Visible)
                {
                    CurrentVisibleCombo = (ComboBox)Controls["Panel1"].Controls[Control];
                    if (CurrentVisibleCombo.SelectedIndex > -1)
                    {
                        FilterValue = Conversions.ToString(Interaction.IIf(string.IsNullOrEmpty(FilterValue), Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Convert(", Controls["Panel1"].Controls[Control].Tag), ",System.String) ='"), Controls["Panel1"].Controls[Control].Text), "'"), Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(FilterValue + " And Convert(", Controls["Panel1"].Controls[Control].Tag), ",System.String) ='"), Controls["Panel1"].Controls[Control].Text), "'")));
                    }
                }
            }

            CurrentCombo = null;
            CurrentVisibleCombo = null;
            DataView GridDataView;
            if (!string.IsNullOrEmpty(FilterValue))
            {
                FilterValue = FilterValue.Replace("ی", "ي");
                FilterValue = FilterValue.Replace("ي", "ي");
            }

            if (dgList.DataSource is DataView)
            {
                GridDataView = (DataView)dgList.DataSource;
                GridDataView.RowFilter = FilterValue;
            }
            else
            {
                GridDataView = dsProductionPlanning.Tables[dgList.DataMember].DefaultView;
                GridDataView.RowFilter = FilterValue;
                SetGridColumns(GridDataView, "");
            }
        }

        private void txtSearchs_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox CurrentTextBox = (TextBox)sender;
            if (Strings.Asc(e.KeyChar) == (int)Keys.Back)
            {
                if (string.IsNullOrEmpty(CurrentTextBox.Text))
                {
                    string PreControlName = "txtSearch";
                    PreControlName = Conversions.ToString(Interaction.IIf(Conversions.ToInteger(Strings.Right(CurrentTextBox.Name, 1)) < 8, PreControlName + (Conversions.ToInteger(Strings.Right(CurrentTextBox.Name, 1)) + 1).ToString(), Constants.vbNullString));
                    if (!string.IsNullOrEmpty(PreControlName))
                    {
                        if (Controls["Panel1"].Controls[PreControlName].Visible)
                        {
                            Controls["Panel1"].Controls[PreControlName].Focus();
                        }
                    }
                }
            }
            else if (Strings.Asc(e.KeyChar) == (int)Keys.Enter)
            {
                string Control;
                TextBox CurrentVisibleTextBox;
                string FilterValue = Constants.vbNullString;
                for (short TextBoxCounter = 1; TextBoxCounter <= 8; TextBoxCounter++)
                {
                    Control = "txtSearch" + TextBoxCounter;
                    if (Controls["Panel1"].Controls[Control].Visible)
                    {
                        CurrentVisibleTextBox = (TextBox)Controls["Panel1"].Controls[Control];
                        if (!string.IsNullOrEmpty(CurrentVisibleTextBox.Text))
                        {
                            FilterValue = Conversions.ToString(Interaction.IIf(string.IsNullOrEmpty(FilterValue), Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Convert(", Controls["Panel1"].Controls[Control].Tag), ",System.String) LIKE '%"), Controls["Panel1"].Controls[Control].Text), "%'"), Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(FilterValue + " And Convert(", Controls["Panel1"].Controls[Control].Tag), ",System.String) LIKE '%"), Controls["Panel1"].Controls[Control].Text), "%'")));
                        }
                    }
                }

                CurrentVisibleTextBox = null;
                string TableName;
                if (string.IsNullOrEmpty(FilterValue))
                {
                    FoundRows = null;
                }
                else if (FoundRows is null)
                {
                    if (dgList.DataSource is DataView)
                    {
                        DataView GridDataView = (DataView)dgList.DataSource;
                        TableName = GridDataView.Table.TableName;
                        SetGridColumns(dsProductionPlanning, TableName);
                    }
                    else
                    {
                        TableName = dgList.DataMember;
                    }

                    if (!string.IsNullOrEmpty(FilterValue))
                    {
                        FilterValue = FilterValue.Replace("ی", "ي");
                        FilterValue = FilterValue.Replace("ي", "ي");
                    }

                    FoundRows = dsProductionPlanning.Tables[TableName].Select(FilterValue);
                    FoundRowsEnumerator = FoundRows.GetEnumerator();
                    if (FoundRowsEnumerator.MoveNext())
                    {
                        I = dsProductionPlanning.Tables[TableName].Rows.IndexOf((DataRow)FoundRowsEnumerator.Current);
                        BindingContext[dsProductionPlanning, TableName].Position = I;
                    }
                    else
                    {
                        MessageBox.Show("موردی یافت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                        FoundRows = null;
                    }
                }
                else
                {
                    TableName = dgList.DataMember;
                    if (FoundRowsEnumerator.MoveNext())
                    {
                        I = dsProductionPlanning.Tables[TableName].Rows.IndexOf((DataRow)FoundRowsEnumerator.Current);
                        BindingContext[dsProductionPlanning, TableName].Position = I;
                    }
                    else
                    {
                        MessageBox.Show("مورد دیگری وجود ندارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                        FoundRows = null;
                    }
                }
            }

            CurrentTextBox = null;
        }

        private void SetSearchControlsLocation()
        {
            short I, J, ControlIndex;
            int TempWidth;
            string Control;
            for (I = 1; I <= 8; I++)
            {
                Controls["Panel1"].Controls["cbFilter" + I].Visible = false;
                Controls["Panel1"].Controls["cbFilter" + I].Tag = Constants.vbNullString;
            }

            ControlIndex = 1;
            for (I = (short)dgList.Columns.Count; I >= 1; I += -1)
            {
                if (dgList.Columns[I - 1].Visible)
                {
                    Control = "txtSearch" + ControlIndex;
                    TempWidth = dgList.Width;
                    var loopTo = (short)(I - 1);
                    for (J = 0; J <= loopTo; J++)
                    {
                        if (dgList.Columns[J].Visible)
                        {
                            TempWidth = TempWidth - dgList.Columns[J].Width;
                        }
                    }

                    {
                        var withBlock = Controls["Panel1"];
                        withBlock.Controls[Control].Text = Constants.vbNullString;
                        withBlock.Controls[Control].Top = dgList.Location.Y;
                        withBlock.Controls[Control].Left = TempWidth - dgList.RowHeadersWidth + 4;
                        withBlock.Controls[Control].Width = dgList.Columns[I - 1].HeaderCell.OwningColumn.Width - 2;
                        withBlock.Controls[Control].Visible = true;
                        withBlock.Controls[Control].Tag = dgList.Columns[I - 1].Name;
                    }

                    ControlIndex = (short)(ControlIndex + 1);
                }
            }

            if (dgList.DataSource is DataView)
            {
                DataView GridDataView = (DataView)dgList.DataSource;
                SetGridColumns(dsProductionPlanning, GridDataView.Table.TableName);
            }
        }

        private void SetFilterCombosLocation()
        {
            short I, J, ControlIndex;
            int TempWidth;
            string Control;
            for (I = 1; I <= 8; I++)
            {
                Controls["Panel1"].Controls["txtSearch" + I].Visible = false;
                Controls["Panel1"].Controls["txtSearch" + I].Tag = Constants.vbNullString;
            }

            ControlIndex = 1;
            for (I = (short)dgList.Columns.Count; I >= 1; I += -1)
            {
                if (dgList.Columns[I - 1].Visible)
                {
                    Control = "cbFilter" + ControlIndex;
                    TempWidth = dgList.Width;
                    var loopTo = (short)(I - 1);
                    for (J = 0; J <= loopTo; J++)
                    {
                        if (dgList.Columns[J].Visible)
                        {
                            TempWidth = TempWidth - dgList.Columns[J].Width;
                        }
                    }

                    {
                        var withBlock = Controls["Panel1"];
                        withBlock.Controls[Control].Text = Constants.vbNullString;
                        withBlock.Controls[Control].Top = dgList.Location.Y;
                        withBlock.Controls[Control].Left = TempWidth - dgList.RowHeadersWidth + 6;
                        withBlock.Controls[Control].Width = dgList.Columns[I - 1].HeaderCell.OwningColumn.Width - 2;
                        withBlock.Controls[Control].Visible = true;
                        withBlock.Controls[Control].Tag = dgList.Columns[I - 1].Name;
                    }

                    ControlIndex = (short)(ControlIndex + 1);
                }
            }

            if (!(dgList.DataSource is DataView))
            {
                SetGridColumns(dsProductionPlanning.Tables[dgList.DataMember].DefaultView, "");
            }
        }

        private void SetGridColumns(object DataSource, string Table = Constants.vbNullString)
        {
            {
                var withBlock = dgList;
                if (DataSource is DataView)
                {
                    withBlock.DataSource = DataSource;
                }
                else
                {
                    withBlock.DataSource = DataSource;
                    withBlock.DataMember = Table;
                }

                switch (this.CallerForm )
                {
                    case ListFormCaller.LFC_OPERATRIONSTITLES:
                        {
                            withBlock.Columns[0].Visible = false;
                            withBlock.Columns[1].HeaderText = "عنوان فعالیت";
                            break;
                        }

                    case ListFormCaller.LFC_PRODUCTIONPARTS:
                        {
                            withBlock.Columns[0].Visible = false;
                            withBlock.Columns[1].HeaderText = "نام بخش";
                            withBlock.Columns[2].Visible = false;
                            break;
                        }

                    case ListFormCaller.LFC_SUPPLIERS:
                        {
                            withBlock.Columns[0].Visible = false;
                            withBlock.Columns[1].HeaderText = "نام تامین کننده";
                            withBlock.Columns[2].Visible = false;
                            withBlock.Columns[3].HeaderText = "تلفن تماس";
                            withBlock.Columns[4].HeaderText = "زمینۀ فعالیت";
                            withBlock.Columns[5].Visible = false;
                            break;
                        }

                    case ListFormCaller.LFC_TESTUNITS:
                        {
                            withBlock.Columns[0].Visible = false;
                            withBlock.Columns[1].HeaderText = "عنوان واحد";
                            break;
                        }

                    case ListFormCaller.LFC_UNITSRELATIONS:
                        {
                            withBlock.Columns[0].Visible = false;
                            withBlock.Columns[1].HeaderText = "واحد پایه";
                            withBlock.Columns[2].Visible = false;
                            withBlock.Columns[3].HeaderText = "واحد مرتبط";
                            withBlock.Columns[4].HeaderText = "ضریب ارتباطی";
                            break;
                        }

                    case ListFormCaller.LFC_PRIMARYMATERIALS:
                        {
                            withBlock.Columns[0].HeaderText = "کد مواد اولیه";
                            withBlock.Columns[1].HeaderText = "نام مواد اولیه";
                            withBlock.Columns[2].Visible = false;
                            withBlock.Columns[3].Visible = false;
                            withBlock.Columns[4].HeaderText = "واحد سنجش انبار";
                            withBlock.Columns[5].Visible = false;
                            withBlock.Columns[6].Visible = false;
                            withBlock.Columns[7].Visible = false;
                            withBlock.Columns[8].Visible = false;
                            withBlock.Columns[9].HeaderText = "آخرین موجودی";
                            withBlock.Columns[10].Visible = false;
                            withBlock.Columns[11].Visible = false;
                            withBlock.Columns[12].Visible = false;
                            withBlock.Columns[13].Visible = false;
                            withBlock.Columns[14].Visible = false;
                            withBlock.Columns[15].Visible = false;
                            withBlock.Columns[16].Visible = false;
                            break;
                        }

                    case ListFormCaller.LFC_MACHINES:
                        {
                            withBlock.Columns[0].HeaderText = "کد ماشین";
                            withBlock.Columns[1].HeaderText = "نام ماشین";
                            withBlock.Columns[2].HeaderText = "نام سازنده";
                            withBlock.Columns[3].Visible = false;
                            withBlock.Columns[4].Visible = false;
                            withBlock.Columns[5].Visible = false;
                            withBlock.Columns[6].HeaderText = "تقویم کاری";
                            withBlock.Columns[7].Visible = false;
                            break;
                        }

                    case ListFormCaller.LFC_CONTRACTORS:
                        {
                            withBlock.Columns[0].Visible = false;
                            withBlock.Columns[1].HeaderText = "نام پیمانکار";
                            withBlock.Columns[2].Visible = false;
                            withBlock.Columns[3].Visible = false;
                            withBlock.Columns[4].HeaderText = "زمینه فعالیت";
                            withBlock.Columns[5].Visible = false;
                            break;
                        }

                    case ListFormCaller.LFC_STORES:
                        {
                            withBlock.Columns[0].Visible = false;
                            withBlock.Columns[1].HeaderText = "نام انبار";
                            break;
                        }

                    case ListFormCaller.LFC_HOLIDAYS:
                        {
                            withBlock.Columns[0].HeaderText = "روز";
                            withBlock.Columns[1].Visible = false;
                            withBlock.Columns[2].HeaderText = "ماه";
                            withBlock.Columns[3].HeaderText = "علت تعطیلی";
                            break;
                        }

                    case ListFormCaller.LFC_ACTINGCALENDARS:
                        {
                            withBlock.Columns[0].Visible = false;
                            withBlock.Columns[1].HeaderText = "عنوان تقویم";
                            withBlock.Columns[2].HeaderText = "تعداد شیفتها";
                            break;
                        }

                    case ListFormCaller.LFC_PRODUCTS:
                        {
                            withBlock.Columns[0].HeaderText = "کد محصول";
                            withBlock.Columns[1].HeaderText = "نام محصول";
                            withBlock.Columns[2].HeaderText = "حداقل موجودی اطمینان";
                            withBlock.Columns[3].Visible = false;
                            break;
                        }

                    case ListFormCaller.LFC_PRODUCTIONBATCHS:
                        {
                            withBlock.Columns[0].HeaderText = "سریال بچ تولید";
                            withBlock.Columns[1].Visible = false;
                            withBlock.Columns[2].Visible = false;
                            withBlock.Columns[3].HeaderText = "کد محصول";
                            withBlock.Columns[4].Visible = false;
                            withBlock.Columns[5].HeaderText = "تاریخ تعریف";
                            withBlock.Columns[6].HeaderText = "تعداد";
                            withBlock.Columns[7].HeaderText = "موعد تحویل اولین محموله";
                            withBlock.Columns[8].Visible = false;
                            withBlock.Columns[9].HeaderText = "تاریخ شروع طبق برنامه ریزی";
                            withBlock.Columns[10].HeaderText = "تاریخ شروع تولید واقعی";
                            withBlock.Columns[11].HeaderText = "میزان پیشرفت تولید";
                            withBlock.Columns[12].Visible = false;
                            break;
                        }

                    case ListFormCaller.LFC_PRODUCTIONPLANING:
                        {
                            withBlock.Columns.Add("Column1", "کد بچ");
                            withBlock.Columns.Add("Column2", "محصول");
                            withBlock.Columns.Add("Column3", "اولویت اجرا");
                            withBlock.Columns.Add("Column4", "موعد تحویل");
                            withBlock.Columns.Add("Column5", "زودترین زمان شروع");
                            withBlock.Columns.Add("Column6", "زودترین زمان پایان");
                            withBlock.Columns.Add("Column7", "دیرترین زمان شروع");
                            withBlock.Columns.Add("Column8", "دیرترین زمان پایان");
                            break;
                        }

                    case ListFormCaller.LFC_OPERATORS:
                        {
                            withBlock.Columns[0].HeaderText = "کد پرسنلی";
                            withBlock.Columns[1].HeaderText = "نام";
                            withBlock.Columns[2].Visible = false;
                            withBlock.Columns[3].HeaderText = "وضعیت شغلی";
                            withBlock.Columns[4].Visible = false;
                            break;
                        }

                    case ListFormCaller.LFC_OPERATIONUNITS:
                        {
                            withBlock.Columns[0].HeaderText = "کد عملیات";
                            withBlock.Columns[1].Visible = false;
                            withBlock.Columns[2].HeaderText = "نام واحد";
                            withBlock.Columns[3].HeaderText = "ضریب تبدیل";
                            break;
                        }

                    case ListFormCaller.LFC_MACHINENOTAVAILABLE:
                        {
                            // mohsendokht remark
                            // .Columns(0).Visible = False
                            // .Columns(1).Visible = False
                            // .Columns(2).HeaderText = "نام ماشین"
                            // .Columns(3).HeaderText = "تاریخ شروع"
                            // .Columns(4).Visible = False
                            // .Columns(5).HeaderText = "ساعت شروع"
                            // .Columns(6).HeaderText = "تاریخ پایان"
                            // .Columns(7).Visible = False
                            // .Columns(8).HeaderText = "ساعت پایان"
                            // .Columns(9).Visible = False
                            // .Columns(10).HeaderText = "علت در دسترس نبودن"

                            withBlock.Columns[0].Visible = false;
                            withBlock.Columns[1].Visible = false;
                            withBlock.Columns[2].HeaderText = "نام ماشین";
                            withBlock.Columns[3].HeaderText = "تاریخ شروع";
                            withBlock.Columns[4].HeaderText = "ساعت شروع";
                            withBlock.Columns[5].HeaderText = "تاریخ پایان";
                            withBlock.Columns[6].HeaderText = "ساعت پایان";
                            withBlock.Columns[7].Visible = false;
                            withBlock.Columns[8].HeaderText = "علت در دسترس نبودن";
                            break;
                        }

                    case ListFormCaller.LFC_HALTREASON:
                        {
                            withBlock.Columns[0].Visible = false;
                            withBlock.Columns[1].HeaderText = "عنوان علت توقف";
                            break;
                        }

                    case ListFormCaller.LFC_MACHINENOTAVAILABLEREASON:
                        {
                            withBlock.Columns[0].Visible = false;
                            withBlock.Columns[1].HeaderText = "عنوان علت در دسترس نبودن";
                            break;
                        }

                    case ListFormCaller.LFC_BATCHSPRODUCTIONPROGRESS:
                        {
                            withBlock.Columns[0].HeaderText = "سریال بچ تولید";
                            withBlock.Columns[1].HeaderText = "کد محصول";
                            withBlock.Columns[2].HeaderText = "نام محصول";
                            withBlock.Columns[3].HeaderText = "تعداد";
                            withBlock.Columns[4].HeaderText = "میزان پیشرفت تولید";
                            break;
                        }

                    case ListFormCaller.LFC_CUSTOMERS:
                        {
                            withBlock.Columns[0].HeaderText = "کد مشتری";
                            withBlock.Columns[1].HeaderText = "نام مشتری";
                            withBlock.Columns[2].HeaderText = "آدرس";
                            withBlock.Columns[3].HeaderText = "تلفن";
                            withBlock.Columns[4].Visible = false;
                            withBlock.Columns[5].Visible = false;
                            withBlock.Columns[6].Visible = false;
                            withBlock.Columns[7].Visible = false;
                            break;
                        }

                    case ListFormCaller.LFC_OEE:
                        {
                            withBlock.Columns[0].HeaderText = "سریال OEE";
                            withBlock.Columns[1].HeaderText = "تاریخ ثبت";
                            withBlock.Columns[2].HeaderText = "محاسبه از تاریخ";
                            withBlock.Columns[3].Visible = false;
                            withBlock.Columns[4].HeaderText = "محاسبه تا تاریخ";
                            withBlock.Columns[5].Visible = false;
                            withBlock.Columns[6].Visible = false;
                            withBlock.Columns[7].Visible = false;
                            withBlock.Columns[8].HeaderText = "شرح ثبت";
                            break;
                        }
                }
            }

            Module1.SetGridColumnsWidth(Name, this.CallerForm , dgList);
        }

        public DataRow GetRow()
        {
            string FindValue = Constants.vbNullString;
            DataRow[] drFind = new DataRow[1];
            DataRow crRow;
            switch (this.CallerForm )
            {
                case ListFormCaller.LFC_OPERATRIONSTITLES:
                case ListFormCaller.LFC_TESTUNITS:
                case ListFormCaller.LFC_MACHINES:
                    {
                        FindValue = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Code='", dgList.CurrentRow.Cells["Code"].Value), "'"));
                        break;
                    }

                case ListFormCaller.LFC_PRODUCTIONPARTS:
                    {
                        FindValue = Conversions.ToString(Operators.ConcatenateObject("NatureCode=", dgList.CurrentRow.Cells["NatureCode"].Value));
                        break;
                    }

                case ListFormCaller.LFC_SUPPLIERS:
                    {
                        FindValue = Conversions.ToString(Operators.ConcatenateObject("SupplierCode=", dgList.CurrentRow.Cells["SupplierCode"].Value));
                        break;
                    }

                case ListFormCaller.LFC_PRIMARYMATERIALS:
                    {
                        FindValue = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("MaterialCode='", dgList.CurrentRow.Cells["MaterialCode"].Value), "'"));
                        break;
                    }

                case ListFormCaller.LFC_UNITSRELATIONS:
                    {
                        FindValue = Conversions.ToString(Operators.ConcatenateObject("BaseUnitCode=", dgList.CurrentRow.Cells["BaseUnitCode"].Value));
                        FindValue = Conversions.ToString(Operators.ConcatenateObject(FindValue + " And RelatedUnitCode=", dgList.CurrentRow.Cells["RelatedUnitCode"].Value));
                        break;
                    }

                case ListFormCaller.LFC_CONTRACTORS:
                    {
                        FindValue = Conversions.ToString(Operators.ConcatenateObject("ContractorCode=", dgList.CurrentRow.Cells["ContractorCode"].Value));
                        break;
                    }

                case ListFormCaller.LFC_STORES:
                    {
                        FindValue = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("StoreCode='", dgList.CurrentRow.Cells["StoreCode"].Value), "'"));
                        break;
                    }

                case ListFormCaller.LFC_HOLIDAYS:
                    {
                        FindValue = Conversions.ToString(Operators.ConcatenateObject("DayNo=", dgList.CurrentRow.Cells["DayNo"].Value));
                        FindValue = Conversions.ToString(Operators.ConcatenateObject(FindValue + " And MonthNo=", dgList.CurrentRow.Cells["MonthNo"].Value));
                        break;
                    }

                case ListFormCaller.LFC_ACTINGCALENDARS:
                    {
                        FindValue = Conversions.ToString(Operators.ConcatenateObject("CalendarCode=", dgList.CurrentRow.Cells["CalendarCode"].Value));
                        break;
                    }

                case ListFormCaller.LFC_HALTREASON:
                    {
                        FindValue = Conversions.ToString(Operators.ConcatenateObject("ReasonCode=", dgList.CurrentRow.Cells["ReasonCode"].Value));
                        break;
                    }

                case ListFormCaller.LFC_OPERATORS:
                    {
                        FindValue = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("OperatorCode='", dgList.CurrentRow.Cells["OperatorCode"].Value), "'"));
                        break;
                    }

                case ListFormCaller.LFC_OPERATIONUNITS:
                    {
                        FindValue = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("OperationCode='", dgList.CurrentRow.Cells["OperationCode"].Value), "' And UnitCode="), dgList.CurrentRow.Cells["UnitCode"].Value));
                        break;
                    }

                case ListFormCaller.LFC_PRODUCTS:
                    {
                        FindValue = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("ProductCode='", dgList.CurrentRow.Cells["ProductCode"].Value), "'"));
                        break;
                    }

                case ListFormCaller.LFC_PRODUCTIONBATCHS:
                    {
                        FindValue = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("BatchCode='", dgList.CurrentRow.Cells["BatchCode"].Value), "'"));
                        break;
                    }

                case ListFormCaller.LFC_MACHINENOTAVAILABLE:
                    {
                        FindValue = Conversions.ToString(Operators.ConcatenateObject("ID=", dgList.CurrentRow.Cells["ID"].Value));
                        break;
                    }

                case ListFormCaller.LFC_CUSTOMERS:
                    {
                        FindValue = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("CustomerCode='", dgList.CurrentRow.Cells["CustomerCode"].Value), "'"));
                        break;
                    }

                case ListFormCaller.LFC_OEE:
                    {
                        FindValue = Conversions.ToString(Operators.ConcatenateObject("OEEIndex = ", dgList.CurrentRow.Cells["OEEIndex"].Value));
                        break;
                    }

                case ListFormCaller.LFC_MACHINENOTAVAILABLEREASON:
                    {
                        FindValue = Conversions.ToString(Operators.ConcatenateObject("ReasonCode=", dgList.CurrentRow.Cells["ReasonCode"].Value));
                        break;
                    }
            }

            drFind = dsProductionPlanning.Tables[mCurrentTableName].Select(FindValue);
            crRow = drFind[0];
            return crRow;
        }

        private void Prepare_To_Show_TablesRecordList(string TN, string FC)
        {
            SetGridColumns(dsProductionPlanning, TN);
            CurrentTableName = TN;
            Text = FC;
        }

        private void dgList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true & e.KeyCode == Keys.F12)
            {
                Module1.ExportGridToExcel(dgList);
            }
        }
    }
}