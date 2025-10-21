using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class CreateMSPForm
    {
        public CreateMSPForm()
        {
            InitializeComponent();
            _Button1.Name = "Button1";
        }

        private Microsoft.Office.Interop.MSProject.Application prjApp;
        private Microsoft.Office.Interop.MSProject.Project prjProject;
        private Microsoft.Office.Interop.MSProject.Resource prjResource;
        private string FileName;
        private bool GanteOnGridDataTmp;
        private DataView MSPDataPlaningTmp;
        private string SubBatchCodeTmp;
        private string BatchCodeTmp;

        public bool GanteOnGridData
        {
            get
            {
                return GanteOnGridDataTmp;
            }

            set
            {
                GanteOnGridDataTmp = value;
            }
        }

        public DataView MSPDataPlaning
        {
            get
            {
                return MSPDataPlaningTmp;
            }

            set
            {
                MSPDataPlaningTmp = value;
            }
        }

        public string BatchCode
        {
            get
            {
                return BatchCodeTmp;
            }

            set
            {
                BatchCodeTmp = value;
            }
        }

        public string SubBatchCode
        {
            get
            {
                return SubBatchCodeTmp;
            }

            set
            {
                SubBatchCodeTmp = value;
            }
        }

        /* TODO ERROR: Skipped RegionDirectiveTrivia */    // Private Function zzCreateCalendar() As Microsoft.Office.Interop.MSProject.Calendar
                                                           // Dim Cal As Microsoft.Office.Interop.MSProject.Calendar
                                                           // Try
                                                           // If prjProject.Application.BaseCalendarCreate("24-7", "Standard") Then
                                                           // Cal = prjProject.BaseCalendars("24-7")
                                                           // For Each wd As Microsoft.Office.Interop.MSProject.WeekDay In Cal.WeekDays
                                                           // wd.Working = True
                                                           // Next
                                                           // End If
                                                           // Catch ex As Exception
                                                           // Cal = prjProject.BaseCalendars("Standard")
                                                           // End Try


        // Return Cal
        // End Function
        /* TODO ERROR: Skipped EndRegionDirectiveTrivia */
        private void Insert_Nodes_From_DataBase()
        {
            var CN1 = new System.Data.SqlClient.SqlConnection();
            var Cm1 = new System.Data.SqlClient.SqlCommand();
            var Cm_SB = new System.Data.SqlClient.SqlCommand();
            var Da1 = new System.Data.SqlClient.SqlDataAdapter();
            var Ds1 = new DataSet();
            var dV1 = new DataView();
            var dV_Tree = new DataView();
            CN1.ConnectionString = Module1.PlanningCnnStr;
            Cm1.Connection = CN1;
            int I;
            int OutlineLevelTmp;
            Microsoft.Office.Interop.MSProject.Task prjTask;
            string FilterDataTmp;
            string SqlStr;
            string TreeCode_Tmp;
            FilterDataTmp = "";
            if (BatchCode.Length > 0)
            {
                FilterDataTmp = "B.BatchCode='" + BatchCode + "'";
            }

            if (SubBatchCode.Length > 0)
            {
                if (!string.IsNullOrEmpty(FilterDataTmp))
                {
                    FilterDataTmp = FilterDataTmp + "AND A.SubbatchCode='" + SubBatchCode + "'";
                }
                else
                {
                    FilterDataTmp = "A.SubbatchCode='" + SubBatchCode + "'";
                }
            }
            // ''''''''''
            if (!string.IsNullOrEmpty(FilterDataTmp))
            {
                SqlStr = " SELECT DISTINCT A.[SubbatchCode],A.[TreeCode] FROM [Tbl_Planning] AS A INNER JOIN " + " Tbl_ProductionSubbatchs AS B ON A.SubbatchCode = B.SubbatchCode WHERE " + FilterDataTmp + " ORDER BY A.[SubbatchCode]";
            }
            else
            {
                SqlStr = " SELECT DISTINCT A.[SubbatchCode],A.[TreeCode] FROM [Tbl_Planning] AS A  ORDER BY A.[SubbatchCode]";
            }

            Cm1.CommandText = SqlStr;
            Da1.SelectCommand = Cm1;
            Da1.Fill(Ds1, "SubBactchs");
            var loopTo = Ds1.Tables["SubBactchs"].Rows.Count - 1;
            for (I = 0; I <= loopTo; I++)
            {
                OutlineLevelTmp = 1;
                prjTask = prjProject.Tasks.Add();
                prjTask.Name = Conversions.ToString(Ds1.Tables["SubBactchs"].Rows[I][0]);
                prjTask.OutlineLevel = (short)OutlineLevelTmp;
                TreeCode_Tmp = Ds1.Tables["SubBactchs"].Rows[I]["TreeCode"].ToString();
                Insert_SubNode_OPs(Ds1.Tables["SubBactchs"].Rows[I][0].ToString(), prjTask, TreeCode_Tmp, "0");
            }

            {
                var withBlock = prjProject.Application;
                withBlock.TableEdit(Name: "Entry", TaskTable: true, NewName: "", FieldName: "", NewFieldName: "Text1", Title: "تاریخ ش ", Width: 10, Align: 2, ShowInMenu: true, LockFirstColumn: true, DateFormat: 255, RowHeight: 1, ColumnPosition: 2, AlignTitle: 1);
                withBlock.TableApply(Name: "Entry");
                withBlock.SelectTaskColumn(Column: "Duration");
                withBlock.TableEdit(Name: "Entry", TaskTable: true, NewName: "", FieldName: "", NewFieldName: "Text2", Title: "ساعت ش", Width: 6, Align: 2, ShowInMenu: true, LockFirstColumn: true, DateFormat: 255, RowHeight: 1, ColumnPosition: 3, AlignTitle: 1);
                withBlock.TableApply(Name: "Entry");
                withBlock.SelectTaskColumn(Column: "Duration");
                withBlock.TableEdit(Name: "Entry", TaskTable: true, NewName: "", FieldName: "", NewFieldName: "Text3", Title: "تاریخ پ", Width: 10, Align: 2, ShowInMenu: true, LockFirstColumn: true, DateFormat: 255, RowHeight: 1, ColumnPosition: 4, AlignTitle: 1);
                withBlock.TableApply(Name: "Entry");
                withBlock.TableEdit(Name: "Entry", TaskTable: true, NewName: "", FieldName: "", NewFieldName: "Text4", Title: "ساعت پ", Width: 6, Align: 2, ShowInMenu: true, LockFirstColumn: true, DateFormat: 255, RowHeight: 1, ColumnPosition: 5, AlignTitle: 1);
                withBlock.TableApply(Name: "Entry");
                withBlock.TableEdit(Name: "Entry", TaskTable: true, NewName: "", FieldName: "", NewFieldName: "Text5", Title: "کد قطعه", Width: 6, Align: 2, ShowInMenu: true, LockFirstColumn: true, DateFormat: 255, RowHeight: 1, ColumnPosition: 6, AlignTitle: 1);
                withBlock.TableApply(Name: "Entry");
                withBlock.TableEdit(Name: "Entry", TaskTable: true, NewName: "", FieldName: "", NewFieldName: "Text6", Title: "تعداد", Width: 10, Align: 2, ShowInMenu: true, LockFirstColumn: true, DateFormat: 255, RowHeight: 1, ColumnPosition: 7, AlignTitle: 1);
                withBlock.TableApply(Name: "Entry");
                // .TableEdit(Name:="Entry", TaskTable:=True, NewName:="", FieldName:="", NewFieldName:="Text7", Title:="تعداد", Width:=6, Align:=2, ShowInMenu:=True, LockFirstColumn:=True, DateFormat:=255, RowHeight:=1, ColumnPosition:=8, AlignTitle:=1)
                // .TableApply(Name:="Entry")



            }
        }

        private void Insert_Nodes_From_DataRowView()
        {
            Microsoft.Office.Interop.MSProject.Task prjTask;
            // Dim PrjCalendar As Microsoft.Office.Interop.MSProject.Calendar
            DataRowView CurrentRow; // برای نگهداری رکورد جاری

            // Dim sEDate As String, fEDate As String
            DateTime sEDate;
            DateTime fEDate;
            int sTmpYear;
            int sTmpMonth;
            int sTmpDay;
            int sTmpHour;
            int sTmpMinute;
            int fTmpYear;
            int fTmpMonth;
            int fTmpDay;
            int fTmpHour;
            int fTmpMinute;
            var FarsiCalender = new PersianCalendar();
            int OutlineLevelTmp;
            var dV1 = new DataView();
            var Cm2 = new System.Data.SqlClient.SqlCommand();
            var Cm3 = new System.Data.SqlClient.SqlCommand();
            var Da2 = new System.Data.SqlClient.SqlDataAdapter();
            var Ds2 = new DataSet();
            var CN2 = new System.Data.SqlClient.SqlConnection();

            string planningStartDate = "";
            string planningStartHour = "";
            string planningEndDate = "";
            string PlanningEndHour = "";

            // Create a calendar 24/7 working time:-
            // PrjCalendar = zzCreateCalendar()

            MSPDataPlaning.Sort = "PlanningStartDate,PlanningStartHour";
            dV1 = MSPDataPlaning;
            OutlineLevelTmp = 1;

            Set_ProgressBar(dV1.Count);

            foreach (DataRowView DrView in dV1)
            {
               

                CurrentRow = DrView; // dV1.Table.Rows(rr) 'ListForm.GetRow()

                ProgressCountLabel.Text = $"{progressBar1.Value} / {progressBar1.Maximum} "; 
                progressBar1.Increment(1);
                OperationCodeLabel.Text = CurrentRow["OperationCode"].ToString();

                planningStartDate = CurrentRow["PlanningStartDate"].ToString();
                planningStartHour = CurrentRow["PlanningStartHour"].ToString();
                planningEndDate = CurrentRow["PlanningEndDate"].ToString();
                PlanningEndHour = CurrentRow["PlanningEndHour"].ToString();

                //Logger.SaveError("planning Start", $"planningStartDate={planningStartDate} planningStartHour ={planningStartHour} ");

                sTmpYear = int.Parse(Strings.Mid(planningStartDate, 1, 4));
                sTmpMonth = int.Parse(Strings.Mid(planningStartDate, 5, 2));
                sTmpDay = int.Parse(Strings.Mid(planningStartDate, 7, 2));
                sTmpHour = int.Parse(Strings.Mid(planningStartHour, 1, 2));
                //Logger.SaveError("sTmpMinute", Math.Round(double.Parse("0" + Strings.Mid(planningStartHour, 3)) * 60d).ToString());
                sTmpMinute = int.Parse(Math.Round(double.Parse("0" + Strings.Mid(planningStartHour, 3)) * 60d).ToString());
                if (sTmpMinute == 60)
                    sTmpMinute = 59;
                // '''''''''''''
                //Logger.SaveError("planning Start", $" Year={sTmpYear} Month ={sTmpMonth} Day={sTmpDay}  Hour={sTmpHour}  Minute= {sTmpMinute} ");

                //Logger.SaveError("planning End  ", $"planningEndDate  ={planningEndDate}   PlanningEndHour   ={PlanningEndHour}  ");

                fTmpYear = int.Parse(Strings.Mid(planningEndDate, 1, 4));
                fTmpMonth = int.Parse(Strings.Mid(planningEndDate, 5, 2));
                fTmpDay = int.Parse(Strings.Mid(planningEndDate, 7, 2));
                fTmpHour = int.Parse(Strings.Mid(PlanningEndHour, 1, 2));
                fTmpMinute = (int)Math.Round(double.Parse("0" + Strings.Mid(PlanningEndHour, 3)) * 60d);
                if (fTmpMinute == 60)
                    fTmpMinute = 59;
                // '''''''''''''''''''''''
                //Logger.SaveError("planning End  ", $" Year={fTmpYear} Month ={fTmpMonth} Day={fTmpDay}  Hour={fTmpHour}  Minute= {fTmpMinute} ");

            
                sEDate = FarsiCalender.ToDateTime(sTmpYear, sTmpMonth, sTmpDay, sTmpHour, sTmpMinute, 0, 0, PersianCalendar.PersianEra);
                fEDate = FarsiCalender.ToDateTime(fTmpYear, fTmpMonth, fTmpDay, fTmpHour, fTmpMinute, 0, 0, PersianCalendar.PersianEra);
                prjTask = prjProject.Tasks.Add();
                prjTask.OutlineLevel = (short)OutlineLevelTmp;
                prjTask.Name = CurrentRow["SubbatchCode"].ToString() + "_" + CurrentRow["OperationCode"].ToString();
                prjResource = prjProject.Resources.Add();
                prjResource.Name = CurrentRow["MachineCode"].ToString();
                prjTask.Assignments.Add(prjTask.ID, prjResource.ID);
                prjTask.Text1 = CurrentRow["OperationTitle"].ToString();
                prjTask.Text2 = CurrentRow["DetailProductionQuantity"].ToString();
                prjTask.Text3 = Math.Round(decimal.Parse(Conversions.ToString(CurrentRow["PlanningDuration"])), 2).ToString() + " H";
                prjTask.Text4 = CurrentRow["StartDT"].ToString();
                prjTask.Text5 = CurrentRow["FinishDT"].ToString();

                // prjTask.Text4 = CurrentRow.Item("DetailCode").ToString


                prjTask.Start = sEDate;
                prjTask.Finish = fEDate;
                // prjTask.Duration = CurrentRow.Item("PlanningDuration").ToString & "h"
                prjTask.Calendar = "Standard"; // PrjCalendar.Name
                prjTask.IgnoreResourceCalendar = true;
            }

            {
                var withBlock = prjProject.Application;
                withBlock.TableEdit(Name: "Entry", TaskTable: true, NewName: "", FieldName: "", NewFieldName: "Text1", Title: " شرح عملیات ", Width: 10, Align: 2, ShowInMenu: true, LockFirstColumn: true, DateFormat: 255, RowHeight: 1, ColumnPosition: 2, AlignTitle: 1);
                withBlock.TableApply(Name: "Entry");
                // .SelectTaskColumn(Column:="Duration")
                withBlock.TableEdit(Name: "Entry", TaskTable: true, NewName: "", FieldName: "", NewFieldName: "Text2", Title: " تعداد ", Width: 6, Align: 2, ShowInMenu: true, LockFirstColumn: true, DateFormat: 255, RowHeight: 1, ColumnPosition: 3, AlignTitle: 1);
                withBlock.TableApply(Name: "Entry");
                // .SelectTaskColumn(Column:="Duration")
                withBlock.TableEdit(Name: "Entry", TaskTable: true, NewName: "", FieldName: "", NewFieldName: "Text3", Title: " زمان عملیات ", Width: 10, Align: 2, ShowInMenu: true, LockFirstColumn: true, DateFormat: 255, RowHeight: 1, ColumnPosition: 4, AlignTitle: 1);
                withBlock.TableApply(Name: "Entry");
                // .SelectTaskColumn(Column:="Duration")
                withBlock.TableEdit(Name: "Entry", TaskTable: true, NewName: "", FieldName: "", NewFieldName: "Text4", Title: "شروع", Width: 6, Align: 2, ShowInMenu: true, LockFirstColumn: true, DateFormat: 255, RowHeight: 1, ColumnPosition: 6, AlignTitle: 1);
                withBlock.TableApply(Name: "Entry");
                withBlock.TableEdit(Name: "Entry", TaskTable: true, NewName: "", FieldName: "", NewFieldName: "Text5", Title: "پایان", Width: 10, Align: 2, ShowInMenu: true, LockFirstColumn: true, DateFormat: 255, RowHeight: 1, ColumnPosition: 7, AlignTitle: 1);
                withBlock.TableApply(Name: "Entry");
                // .TableEdit(Name:="Entry", TaskTable:=True, NewName:="", FieldName:="", NewFieldName:="Text7", Title:="تعداد", Width:=6, Align:=2, ShowInMenu:=True, LockFirstColumn:=True, DateFormat:=255, RowHeight:=1, ColumnPosition:=8, AlignTitle:=1)
                // .TableApply(Name:="Entry")

            }
        }

        private void Insert_SubNode_OPs(string SubBatchCode, Microsoft.Office.Interop.MSProject.Task prjTask, string TreeCodeTmp, string ParentCodeInTree)
        {
            Microsoft.Office.Interop.MSProject.Task prjTaskTmp;
            Microsoft.Office.Interop.MSProject.Task prjTask2;
            DataRow CurrentRow; // برای نگهداری رکورد جاری
            long rr;
            long j;
            string sEDate;
            string fEDate;
            int sTmpYear;
            int sTmpMonth;
            int sTmpDay;
            int sTmpHour;
            int sTmpMinute;
            int fTmpYear;
            int fTmpMonth;
            int fTmpDay;
            int fTmpHour;
            int fTmpMinute;
            var FarsiCalender = new PersianCalendar();
            int OutlineLevelTmp;
            var dV1 = new DataView();
            var Cm2 = new System.Data.SqlClient.SqlCommand();
            var Cm3 = new System.Data.SqlClient.SqlCommand();
            var Da2 = new System.Data.SqlClient.SqlDataAdapter();
            var Ds2 = new DataSet();
            var CN2 = new System.Data.SqlClient.SqlConnection();
            CN2.ConnectionString = Module1.PlanningCnnStr;
            Cm2.Connection = CN2;
            Cm2.CommandText = "SELECT DetailCode, DetailCode + ' - ' + DetailName AS DetailCN FROM  Tbl_ProductTreeDetails WHERE TreeCode=" + TreeCodeTmp + " AND ParentDetailCode='" + ParentCodeInTree + "'";
            Da2.SelectCommand = Cm2;
            Da2.Fill(Ds2, "TreeDetails");
            dV1 = Ds2.Tables["TreeDetails"].DefaultView;
            var loopTo = (long)(dV1.Table.Rows.Count - 1);
            for (j = 0L; j <= loopTo; j++)
            {
                // For Each d_row In dV_Tree
                OutlineLevelTmp = prjTask.OutlineLevel + 1;
                prjTaskTmp = prjProject.Tasks.Add();
                prjTaskTmp.Name = Conversions.ToString(dV1.Table.Rows[(int)j]["DetailCN"]);
                prjTaskTmp.OutlineLevel = (short)OutlineLevelTmp;
                Insert_SubNode_OPs(SubBatchCode, prjTaskTmp, TreeCodeTmp, Conversions.ToString(dV1.Table.Rows[(int)j]["DetailCode"]));
            }

            Cm2.CommandText = @" SELECT  
           StartDT      =   dbo.GetRegularTimeFromFloatingTime(Tbl_Planning.PlanningStartHour)  + ' ' 
						  + SUBSTRING(Tbl_Planning.PlanningStartDate,1,4) + '/'
						  + SUBSTRING(Tbl_Planning.PlanningStartDate,5,2) + '/'
						  + SUBSTRING(Tbl_Planning.PlanningStartDate,7,2) ,
		   FinishDT     =   dbo.GetRegularTimeFromFloatingTime(Tbl_Planning.PlanningEndHour)  + ' ' 
						  + SUBSTRING(Tbl_Planning.PlanningEndDate,1,4) + '/'
						  + SUBSTRING(Tbl_Planning.PlanningEndDate,5,2) + '/'
						  + SUBSTRING(Tbl_Planning.PlanningEndDate,7,2) ,

                            dbo.Tbl_Planning.*, dbo.Tbl_ProductOPCs.OperationTitle AS OperationTitle, dbo.Tbl_ProductOPCs.HavePreOperation AS HavePreOperation," + " dbo.Tbl_ProductTreeDetails.DetailCode AS DetailCode FROM  dbo.Tbl_Planning INNER JOIN" + " dbo.Tbl_ProductOPCs ON dbo.Tbl_Planning.TreeCode = dbo.Tbl_ProductOPCs.TreeCode AND " + " dbo.Tbl_Planning.OperationCode = dbo.Tbl_ProductOPCs.OperationCode INNER JOIN" + " dbo.Tbl_ProductTreeDetails ON dbo.Tbl_ProductOPCs.TreeCode = dbo.Tbl_ProductTreeDetails.TreeCode AND " + " dbo.Tbl_ProductOPCs.DetailCode = dbo.Tbl_ProductTreeDetails.DetailCode " + " WHERE [SubbatchCode]='" + SubBatchCode + "' AND Tbl_Planning.TreeCode=" + TreeCodeTmp + " AND Tbl_ProductTreeDetails.DetailCode='" + ParentCodeInTree + "'" + " ORDER BY PlanningStartDate,PlanningStartHour";






            Da2.SelectCommand = Cm2;
            Da2.Fill(Ds2, "T1");


            // .RowFilter = "[SubbatchCode]='" & dt1.Rows(I).Item(0) & "'"
            dV1 = Ds2.Tables["T1"].DefaultView;
            // dV1.Sort = "PlanningStartDate,PlanningStartHour"
            var loopTo1 = (long)(dV1.Table.Rows.Count - 1);
            for (rr = 0L; rr <= loopTo1; rr++) // Ds1.Tables("T1").Rows.Count - 1
            {
                CurrentRow = dV1.Table.Rows[(int)rr]; // ListForm.GetRow()
                prjTask2 = prjProject.Tasks.Add();
                prjTask2.OutlineLevel = (short)(prjTask.OutlineLevel + 1);
                prjTask2.Name = CurrentRow[3].ToString();
                prjResource = prjProject.Resources.Add();
                prjResource.Name = CurrentRow[5].ToString();
                prjTask2.Assignments.Add(prjTask2.ID, prjResource.ID);
                sTmpYear = Conversions.ToInteger(Strings.Mid(CurrentRow[7].ToString(), 1, 4));
                sTmpMonth = Conversions.ToInteger(Strings.Mid(CurrentRow[7].ToString(), 5, 2));
                sTmpDay = Conversions.ToInteger(Strings.Mid(CurrentRow[7].ToString(), 7, 2));
                sTmpHour = Conversions.ToInteger(Strings.Mid(CurrentRow[8].ToString(), 1, 2));
                sTmpMinute = (int)Math.Round(Conversion.Val("0" + Strings.Mid(CurrentRow[8].ToString(), 3)) * 60d);
                if (sTmpMinute == 60)
                    sTmpMinute = 59;
                // '''''''''''''
                fTmpYear = Conversions.ToInteger(Strings.Mid(CurrentRow[9].ToString(), 1, 4));
                fTmpMonth = Conversions.ToInteger(Strings.Mid(CurrentRow[9].ToString(), 5, 2));
                fTmpDay = Conversions.ToInteger(Strings.Mid(CurrentRow[9].ToString(), 7, 2));
                fTmpHour = Conversions.ToInteger(Strings.Mid(CurrentRow[10].ToString(), 1, 2));
                fTmpMinute = (int)Math.Round(Conversion.Val("0" + Strings.Mid(CurrentRow[10].ToString(), 3)) * 60d);
                if (fTmpMinute == 60)
                    fTmpMinute = 59;
                // '''''''''''''''''''''''
                sEDate = Conversions.ToString(FarsiCalender.ToDateTime(sTmpYear, sTmpMonth, sTmpDay, sTmpHour, sTmpMinute, 0, 0, PersianCalendar.PersianEra));
                fEDate = Conversions.ToString(FarsiCalender.ToDateTime(fTmpYear, fTmpMonth, fTmpDay, fTmpHour, fTmpMinute, 0, 0, PersianCalendar.PersianEra));
                prjTask2.Start = sEDate;
                prjTask2.Finish = fEDate;
                prjTask2.Text1 = CurrentRow[7].ToString();
                prjTask2.Text2 = sTmpHour + " : " + sTmpMinute;
                prjTask2.Text3 = CurrentRow[9].ToString();
                prjTask2.Text4 = fTmpHour + " : " + fTmpMinute;
                prjTask2.Text5 = CurrentRow[29].ToString();
                prjTask2.Text6 = CurrentRow[11].ToString();
                // prjTask2.Text7 = CurrentRow.Item(9).ToString

                // prjTask.Duration = "7d"
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog1.Filter = "MSP files (*.mpp)|*.mpp";
            SaveFileDialog1.ShowDialog();
            FileName = SaveFileDialog1.FileName;
            if (FileName.Length == 0)
                return;
            if (SaveFileDialog1.CheckFileExists)
            {
                FileSystem.Kill(FileName);
            }

            msgLabel.Text = "لطفا کمی صبر کنید ";
            Cursor = Cursors.WaitCursor;
            try
            {
                prjApp.Visible = false;
                Application.DoEvents();
                prjProject = prjApp.Projects.Add();
                //prjProject.BuiltinDocumentProperties("Subject") = "Project Subject epps";
                //prjProject.BuiltinDocumentProperties("Title") = "Project title :epps";
                // *********************************
                if (GanteOnGridData == true)
                {
                    Insert_Nodes_From_DataRowView();
                }
                else
                {
                    Insert_Nodes_From_DataBase();
                }

                // ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                if (!string.IsNullOrEmpty(FileSystem.Dir(FileName)))
                {
                    FileSystem.Kill(FileName);
                }

                prjProject.SaveAs(FileName, Microsoft.Office.Interop.MSProject.PjFileFormat.pjMPP);
                Cursor = Cursors.Default;
                msgLabel.Text = "عملیات ایجاد فایل پایان یافت ";
                Interaction.MsgBox("MSP File Created .", MsgBoxStyle.Information);
                prjApp.ScreenUpdating = true;
                prjApp.Visible = true;
            }
            // prjApp.Quit()

            catch (Exception ex)
            {
                Logger.LogException("CreateMSPForm_Load", ex);
                MessageBox.Show("خطا در ایجاد فایل");
            }

            prjApp = null;
            Close();
        }

        private void Form1_Disposed(object sender, EventArgs e)
        {
            prjApp = null;
        }

        private void CreateMSPForm_Load(object sender, EventArgs e)
        {
            try
            {
                prjApp = new Microsoft.Office.Interop.MSProject.Application();
                Set_ProgressBar(MSPDataPlaning.Count);
            }
            catch (Exception objEx)
            {
                Logger.LogException("CreateMSPForm_Load", objEx);
                MessageBox.Show("با مشکل مواجه شد " + "MPS" + " فراخوانی فرم ارسال به", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                Close();
            }
        }

        private void Set_ProgressBar(int recNumber)
        {
            progressBar1.Maximum = recNumber + 1;
            progressBar1.Step = 1;
            ProgressCountLabel.Tag = recNumber;
            ProgressCountLabel.Text = recNumber.ToString();

        }

       
    }
}