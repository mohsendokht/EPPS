using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using static ProductionPlanning.MyEnums;

namespace ProductionPlanning
{
    public static class Module1
    {
       

        public enum FormModeEnum
        {
            INSERT_MODE,
            EDIT_MODE,
            DELETE_MODE
        }

        public enum TimeTypes_enum : short
        {
            TT_SECOND = 1,
            TT_MINUTE = 2,
            TT_HOUR = 3,
            TT_DAY = 4,
            TT_WEEK = 5,
            TT_MONTH = 6
        }

        

        public static SqlConnection cnProductionPlanning;
        public static frmMain MainFormObject = null;
        public static SqlConnection cnUMPSBS;
        public static string PlanningCnnStr;
        public static string UMCnnStr;
        public static string mServerShamsiDate = Constants.vbNullString;
        public static string MessagesTitle = "سیستم برنامه ریزی و کنترل تولید ";
        public const int ENCRYPTION_SEED_LENGTH = 5;
        public const int PLANNING_ALERT_TIMER_INTERVAL = 300000;
        public static bool PalnningAlarmReShow = true;
        public static string mDBVersion = Constants.vbNullString;

        public static void NeedForUpdate()
        {
            // control : exist PSB_Update.exe ?
            // control : exist PSB_Update_Folder.txt ?
            // control : exist PSB_Update_Log.txt in Update_Folder ?

            // read PSB_Update_Log.txt from Update_Folder
            // control : exist PSB_Update_Log.txt in Current path ?
            // read PSB_Update_Log.txt from Current path

            // if My_Update_Date is less than Update_Date ?
            // yes : Do Update ( run PSB_Update.exe )
            // No :continue

            string Exist_Result;
            string FileName;
            string MyUpdate_Folder;
            string New_UpdateDate;
            string Last_UpdateDate;
            var MyStep = default(int);
            StreamReader srUpdateAddress;
            string AppPath = Application.StartupPath;
            if (!AppPath.Substring(AppPath.Length - 1, 1).Equals(@"\"))
            {
                AppPath += @"\";
            }

            try
            {
                WriteLog(">>> Enter step1");
                MyStep = 1;
                FileName = AppPath + "PSB_Update.exe";
                Exist_Result = FileSystem.Dir(FileName);
                if (string.IsNullOrEmpty(Exist_Result))
                {
                    WriteLog("ERROR--> PSB_Update.exe file not found!?");
                    return;
                }

                WriteLog(">>> Enter step2");
                MyStep = 2;
                FileName = AppPath + "PSB_Update_Folder.txt";
                Exist_Result = FileSystem.Dir(FileName);
                if (string.IsNullOrEmpty(Exist_Result))
                {
                    WriteLog("ERROR--> PSB_Update_Folder.txt file not found!?");
                    return;
                }

                WriteLog(">>> Enter step3");
                MyStep = 3;
                srUpdateAddress = new StreamReader(FileName);
                MyUpdate_Folder = srUpdateAddress.ReadLine();
                srUpdateAddress.Close();
                if (MyUpdate_Folder is null || MyUpdate_Folder.Trim().Equals(""))
                {
                    WriteLog("ERROR--> PSB_Update_Folder.txt file is empty!?");
                    return;
                }

                if (MyUpdate_Folder.Substring(MyUpdate_Folder.Length - 1) != @"\")
                    MyUpdate_Folder += @"\";
                WriteLog(">>> Enter step4");
                MyStep = 4;
                FileName = MyUpdate_Folder + "PSB_Update_Log.txt";
                Exist_Result = FileSystem.Dir(FileName);
                if (string.IsNullOrEmpty(Exist_Result))
                {
                    WriteLog("ERROR--> Server PSB_Update_Log.txt file not found!?");
                    return;
                }

                WriteLog(">>> Enter step5");
                MyStep = 5;
                srUpdateAddress = new StreamReader(FileName);
                New_UpdateDate = srUpdateAddress.ReadLine();
                srUpdateAddress.Close();
                if (New_UpdateDate is null || New_UpdateDate.Trim().Equals(""))
                {
                    WriteLog("ERROR--> Server PSB_Update_Log.txt file is empty!?");
                    return;
                }

                WriteLog(">>> Enter step6");
                MyStep = 6;
                FileName = AppPath + "PSB_Update_Log.txt";
                Exist_Result = FileSystem.Dir(FileName);
                if (string.IsNullOrEmpty(Exist_Result))
                {
                    WriteLog("ERROR--> Local PSB_Update_Log.txt file not found!?");
                    return;
                }

                WriteLog(">>> Enter step7");
                MyStep = 7;
                srUpdateAddress = new StreamReader(FileName);
                Last_UpdateDate = srUpdateAddress.ReadLine();
                srUpdateAddress.Close();
                if (Last_UpdateDate is null || Last_UpdateDate.Trim().Equals(""))
                {
                    Last_UpdateDate = 0.ToString();
                    WriteLog("ERROR--> Local PSB_Update_Log.txt file is empty!, then fill it with (0) value.");
                }

                WriteLog(">>> Enter step8");
                MyStep = 8;
                if (!(Operators.CompareString(Last_UpdateDate, New_UpdateDate, false) < 0))
                {
                    WriteLog("Success Operation--> Check update condition and found not require status for update this time.");
                    return;
                }
                else
                {
                    WriteLog("Update Operation--> Check update condition and found require status for update this time(goto step 9).");
                }

                // کپی کردن ورژن جدید فایل اجرایی نرم افزار به روزرسانی کلاینت ها
                try
                {
                    if (File.Exists(MyUpdate_Folder + "PSB_Update.exe"))
                    {
                        string mAppPath = Application.StartupPath;
                        if (mAppPath.Substring(mAppPath.Length - 1) != @"\")
                            mAppPath += @"\";
                        File.Copy(MyUpdate_Folder + "PSB_Update.exe", mAppPath + "PSB_Update.exe", true);
                    }
                }
                catch
                {
                }

                WriteLog(">>> Enter step9");
                MyStep = 9;
                FileName = AppPath + "PSB_Update.exe";
                WriteLog("Start Update--> Start update software.");
                Interaction.Shell(FileName + " ProductionPlanning.exe", Constants.vbNormalFocus);
                WriteLog("Quit EPPS --> Unload EPPS software until update operaion compelete successful.");
                Application.Exit();
            }
            catch (Exception objEx)
            {
                Logger.SaveError("NeedForUpdate", objEx.Message);
                switch (MyStep)
                {
                    case 4:
                        {
                            MessageBox.Show("به روزآوری نرم افزار با مشکل مواجه شد" + Constants.vbCrLf + "Step " + MyStep.ToString() + Constants.vbCrLf + " دسترسي به مسير بروزآوري فايل ها وجو ندارد", MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                            break;
                        }

                    default:
                        {
                            MessageBox.Show("به روزآوری نرم افزار با مشکل مواجه شد" + Constants.vbCrLf + "Step " + MyStep.ToString(), MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                            break;
                        }
                }
            }
        }

        private static void WriteLog(object Log)
        {
            string TempPath = Path.GetTempPath();
            StreamWriter swLogger;
            if (!TempPath.Substring(TempPath.Length - 1).Equals(@"\"))
            {
                TempPath += @"\";
            }

            if (!File.Exists(TempPath + "UpdateStepsLogger.Txt"))
            {
                swLogger = new StreamWriter(TempPath + "UpdateStepsLogger.Txt", false);
            }
            else
            {
                swLogger = new StreamWriter(TempPath + "UpdateStepsLogger.Txt", true);
            }

            swLogger.WriteLine(Log);
            swLogger.Close();
        }

        public static void SetButtonsImage(System.Windows.Forms.Button control, int ImageIndex)
        {
            control.ImageList = MainFormObject.ImageList1;
            control.ImageIndex = ImageIndex;
        }

        public static string GetFormatedDate(string mUnFormatedDate)
        {
            return mUnFormatedDate.Substring(0, 4) + "/" + mUnFormatedDate.Substring(4, 2) + "/" + mUnFormatedDate.Substring(6, 2);
        }

        /// <summary>
    /// این تابع هر زمانی را به ساعت تبدیل می کند
    /// </summary>
        public static double GetAnyTime_TO_Hour(short TimeType, double Time, string AccessibleTime)
        {
            double HourBaseTime = 0d;
            double CalendarAccessibleHour = Conversions.ToDouble(GetFloatingHour(AccessibleTime));
            switch (TimeType)
            {
                case (short)TimeTypes_enum.TT_SECOND:
                    {
                        HourBaseTime = Time / 3600d;
                        break;
                    }

                case (short)TimeTypes_enum.TT_MINUTE:
                    {
                        HourBaseTime = Time / 60d;
                        break;
                    }

                case (short)TimeTypes_enum.TT_HOUR:
                    {
                        HourBaseTime = Time;
                        break;
                    }

                case (short)TimeTypes_enum.TT_DAY:
                    {
                        HourBaseTime = Time * CalendarAccessibleHour;
                        break;
                    }

                case (short)TimeTypes_enum.TT_WEEK:
                    {
                        HourBaseTime = 7d * Time * CalendarAccessibleHour;
                        break;
                    }

                case (short)TimeTypes_enum.TT_MONTH:
                    {
                        HourBaseTime = 30d * Time * CalendarAccessibleHour;
                        break;
                    }
            }

            return HourBaseTime;
        }

        public static double ConvertToHour(EnumTimeType TimeType, double Time, int calendarCode = 0)
        {
            double myHour = 0;
            double CalendarDayHour;

            switch (TimeType)
            {
                case EnumTimeType.TT_SECOND:
                    {
                        myHour = Time / 3600;
                        break;
                    }

                case EnumTimeType.TT_MINUTE:
                    {
                        myHour = Time / 60;
                        break;
                    }

                case EnumTimeType.TT_HOUR:
                    {
                        myHour = Time;
                        break;
                    }

                case EnumTimeType.TT_DAY:
                    {
                        CalendarDayHour = calendarCode == 0 ? 24 : double.Parse(Module1.GetCalendarAccessibleTime(calendarCode.ToString()));
                        myHour = Time * CalendarDayHour;
                        break;
                    }

                case EnumTimeType.TT_WEEK:
                    {
                        CalendarDayHour = calendarCode == 0 ? 24 : double.Parse(Module1.GetCalendarAccessibleTime(calendarCode.ToString()));
                        myHour = 7 * Time * CalendarDayHour;
                        break;
                    }

                case EnumTimeType.TT_MONTH:
                    {
                        CalendarDayHour = calendarCode == 0 ? 24 : double.Parse(Module1.GetCalendarAccessibleTime(calendarCode.ToString()));
                        myHour = 30 * Time * CalendarDayHour;
                        break;
                    }
            }

            return myHour;
        }

        /// <summary>
        /// این تابع هر زمان پیمانکاری را به ساعت تبدیل می کند
        /// </summary>
        public static string GetContractorTime_To_Hour(short TimeType, string Time)
        {
            string HourBaseTime = 0.ToString();
            switch (TimeType)
            {
                case (short)TimeTypes_enum.TT_SECOND:
                case (short)TimeTypes_enum.TT_MINUTE:
                case (short)TimeTypes_enum.TT_HOUR:
                    {
                        switch (TimeType)
                        {
                            case (short)TimeTypes_enum.TT_SECOND:
                                {
                                    HourBaseTime = (Conversions.ToDouble(Time) / 3600d).ToString();
                                    break;
                                }

                            case (short)TimeTypes_enum.TT_MINUTE:
                                {
                                    HourBaseTime = (Conversions.ToDouble(Time) / 60d).ToString();
                                    break;
                                }

                            case (short)TimeTypes_enum.TT_HOUR:
                                {
                                    HourBaseTime = Time;
                                    break;
                                }
                        }

                        break;
                    }

                case (short)TimeTypes_enum.TT_DAY:
                    {
                        HourBaseTime = (Conversions.ToDouble(Time) * 24d).ToString();
                        break;
                    }

                case (short)TimeTypes_enum.TT_WEEK:
                    {
                        HourBaseTime = (Conversions.ToDouble(Time) * 24d * 7d).ToString();
                        break;
                    }

                case (short)TimeTypes_enum.TT_MONTH:
                    {
                        HourBaseTime = (Conversions.ToDouble(Time) * 24d * 30d).ToString();
                        break;
                    }
            }

            return HourBaseTime;
        }

        /// <summary>
    /// این تابع زمان را بصورت ساعت اعشاری باز می گرداند
    /// </summary>
        public static string GetFloatingHour(string strTime)
        {
            return (Conversions.ToInteger(Strings.Mid(strTime, 1, strTime.IndexOf(":"))) + Conversions.ToInteger(Strings.Mid(strTime, strTime.IndexOf(":") + 2)) / 60d).ToString();
        }

        /// <summary>
    /// این تابع زمان را بصورت ساعت عادی باز می گرداند
    /// </summary>
        public static string GetRegulareHour(string FloatingHour)
        {
            if (FloatingHour.Contains("."))
            {
                long Hour = Conversions.ToLong(Strings.Mid(FloatingHour, 1, FloatingHour.IndexOf(".")));
                short Minute = (short)(int)Math.Round(Conversions.ToDouble(Strings.Mid(FloatingHour, FloatingHour.IndexOf(".") + 1)) * 60d);
                if (Minute == 60)
                {
                    Minute = 59;
                }

                if (Hour < 0L)
                {
                    return Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(Hour > -10, "-0" + (Hour * -1).ToString(), Hour.ToString()), ":"), Interaction.IIf(Minute < 10, "0" + Minute.ToString(), Minute.ToString())));
                }
                else
                {
                    return Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(Hour < 10L, "0" + Hour.ToString(), Hour.ToString()), ":"), Interaction.IIf(Minute < 10, "0" + Minute.ToString(), Minute.ToString())));
                }
            }
            else
            {
                return Conversions.ToString(Operators.ConcatenateObject(Interaction.IIf(Conversions.ToDouble(FloatingHour) < 10d, "0" + FloatingHour, FloatingHour), ":00"));
            }
        }

        /// <summary>
    /// این تابع شمارۀ روز در تاریخ شمسی را باز می گرداند
    /// </summary>
        public static short GetDayNo(string ShamsiDate)
        {
            // Dim LDate As String = FarsiDateFunctions.FLDate(ShamsiDate)
            switch (Conversions.ToDate(Get_LatinDate_FromPersianDate(ShamsiDate)).DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    {
                        return 1;
                    }

                case DayOfWeek.Sunday:
                    {
                        return 2;
                    }

                case DayOfWeek.Monday:
                    {
                        return 3;
                    }

                case DayOfWeek.Tuesday:
                    {
                        return 4;
                    }

                case DayOfWeek.Wednesday:
                    {
                        return 5;
                    }

                case DayOfWeek.Thursday:
                    {
                        return 6;
                    }

                case DayOfWeek.Friday:
                    {
                        return 7;
                    }
            }

            return default;
        }

        /// <summary>
    /// این تابع نام روز در تاریخ شمسی را باز می گرداند
    /// </summary>
        public static string GetDayName(DateTime LDate)
        {
            string DayName = "";
            switch (LDate.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    {
                        DayName = "شنبه";
                        break;
                    }

                case DayOfWeek.Sunday:
                    {
                        DayName = "یکشنبه";
                        break;
                    }

                case DayOfWeek.Monday:
                    {
                        DayName = "دوشنبه";
                        break;
                    }

                case DayOfWeek.Tuesday:
                    {
                        DayName = "سه شنبه";
                        break;
                    }

                case DayOfWeek.Wednesday:
                    {
                        DayName = "چهارشنبه";
                        break;
                    }

                case DayOfWeek.Thursday:
                    {
                        DayName = "پنجشنبه";
                        break;
                    }

                case DayOfWeek.Friday:
                    {
                        DayName = "جمعه";
                        break;
                    }
            }

            return DayName;
        }

        public static long GetNewTreeCode()
        {
            var cmMaxCode = new SqlCommand("Select Max(TreeCode) From dbo.Tbl_ProductTree", cnProductionPlanning);
            long MaxCode;
            if (cnProductionPlanning.State == ConnectionState.Closed)
                cnProductionPlanning.Open();
            var result = cmMaxCode.ExecuteScalar();
            MaxCode = result == DBNull.Value  ? 0: Conversions.ToLong(result);
            if (cnProductionPlanning.State == ConnectionState.Open)
                cnProductionPlanning.Close();
            switch (MaxCode)
            {
                case var @case when @case < 2147483647L:
                    {
                        MaxCode += 1L;
                        break;
                    }

                case var case1 when case1 == 2147483647L:
                    {
                        MaxCode = 0L;
                        break;
                    }

                case var case2 when case2 > 2147483647L:
                    {
                        MaxCode = 0L;
                        break;
                    }
            }

            cmMaxCode.Dispose();
            return MaxCode;
        }

        public static DataGridViewComboBoxCell CreateComboBoxCell(DataView DataSource, DataSet dsCombo, string TableName, string DisplayMember, string ValueMember, short DropDownWidth)
        {
            var bindSource = new BindingSource();
            var Cell = new DataGridViewComboBoxCell();
            var dtCombo = new DataTable();
            short J;
            dtCombo.Columns.Add(new DataColumn(DisplayMember));
            if ((DisplayMember ?? "") != (ValueMember ?? ""))
            {
                dtCombo.Columns.Add(new DataColumn(ValueMember));
            }
            else
            {
                dtCombo.Columns.Add(new DataColumn(ValueMember + "1"));
            }

            if (!string.IsNullOrEmpty(TableName))
            {
                var loopTo = (short)(dsCombo.Tables[TableName].Rows.Count - 1);
                for (J = 0; J <= loopTo; J++)
                    dtCombo.Rows.Add(dsCombo.Tables[TableName].Rows[J][DisplayMember], dsCombo.Tables[TableName].Rows[J][ValueMember]);
            }
            else
            {
                var loopTo1 = Conversions.ToShort(Operators.SubtractObject(DataSource.Count, 1));
                for (J = 0; J <= loopTo1; J++)
                    dtCombo.Rows.Add(DataSource[J][DisplayMember], DataSource[J][ValueMember]);
            }

            bindSource.DataSource = dtCombo;
            Cell.DropDownWidth = DropDownWidth;
            Cell.DataSource = null;
            Cell.DataSource = bindSource;
            Cell.DisplayMember = DisplayMember;
            Cell.ValueMember = ValueMember;
            return Cell;
        }

        public static DataGridViewComboBoxColumn CreateComboBoxColumn(object DataSource, string TableName, string DisplayMember, string ValueMember, string ColumnHeader, short ColumnWidth, short DropDownWidth)
        {
            var bindSource = new BindingSource();
            var Column = new DataGridViewComboBoxColumn();
            bindSource.DataSource = DataSource;
            if (!string.IsNullOrEmpty(TableName))
            {
                bindSource.DataMember = TableName;
            }

            Column.DataPropertyName = ValueMember;
            Column.Width = ColumnWidth;
            Column.HeaderText = ColumnHeader;
            Column.DropDownWidth = DropDownWidth;
            Column.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column.DataSource = bindSource;
            Column.DisplayMember = DisplayMember;
            Column.ValueMember = ValueMember;
            Column.Resizable = DataGridViewTriState.False;
            return Column;
        }

        public static void DataGridViews_Sorted_EventHandler(object sender, EventArgs e)
        {
            DataGridView dg = (DataGridView)sender;
            for (int I = 0, loopTo = dg.Columns.Count - 1; I <= loopTo; I++)
            {
                if (dg.Columns[I].Visible)
                {
                    var cs1 = dg.Columns[I].HeaderCell.GetInheritedStyle(dg.Columns[I].HeaderCell.Style, -1, true);
                    cs1.Font = dg.ColumnHeadersDefaultCellStyle.Font; // New Font("Tahoma", "8.25", FontStyle.Regular, GraphicsUnit.Point)
                    cs1.BackColor = dg.ColumnHeadersDefaultCellStyle.ForeColor; // Color.FromArgb(255, 236, 233, 216)
                    cs1.ForeColor = dg.ColumnHeadersDefaultCellStyle.BackColor; // Color.FromArgb(255, 0, 0, 0)
                }
            }

            var cs = dg.SortedColumn.HeaderCell.GetInheritedStyle(dg.SortedColumn.HeaderCell.Style, -1, false);
            cs.Font = new Font("Tahoma", Conversions.ToSingle("8.25"), FontStyle.Bold, GraphicsUnit.Point);
            cs.BackColor = Color.Green;
        }

        public static void SaveGridColumnsWidth(string mFormName, ListFormCaller mListCaller, DataGridView mGrid)
        {
            using (var cn = new SqlConnection(PlanningCnnStr))
            {
                SqlTransaction trn = null;
                try
                {
                    cn.Open();
                    trn = cn.BeginTransaction();
                    var cmSavingWidth = new SqlCommand("Delete From Tbl_GridsColumnsWidth Where FormName = '" + mFormName + "' And GridName = '" + mGrid.Name + "' And ListCaller = " + (int)mListCaller + " And UserCode = " + Module_UserAccess.UserCodeTmp, cn);
                    cmSavingWidth.Transaction = trn;
                    cmSavingWidth.ExecuteNonQuery();
                    for (int I = 0, loopTo = mGrid.Columns.Count - 1; I <= loopTo; I++)
                    {
                        if (mGrid.Columns[I].Visible)
                        {
                            cmSavingWidth.CommandText = "Insert Into Tbl_GridsColumnsWidth(FormName,GridName,ColumnName,ColumnWidth,ListCaller,UserCode) Values('" + mFormName + "','" + mGrid.Name + "','" + mGrid.Columns[I].Name + "'," + mGrid.Columns[I].Width + "," + (int)mListCaller + "," + Module_UserAccess.UserCodeTmp + ")";
                            cmSavingWidth.ExecuteNonQuery();
                        }
                    }

                    trn.Commit();
                }
                catch (Exception objEx)
                {
                    if (trn is object)
                    {
                        trn.Rollback();
                    }

                    Logger.SaveError("SaveGridColumnsWidth", objEx.Message);
                    MessageBox.Show("ثبت طول ستونهای گرید با مشکل مواجه شد", MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }
        }

        public static void ExportGridToExcel(DataGridView MyDataGridView)
        {
            var exp = new Tools.ExportToExcel.ExportToExcel();
            var sfdExportToxcel = new SaveFileDialog();
            sfdExportToxcel.FileName = "FilenameToExport.xls";
            sfdExportToxcel.Filter = "Archivos Excel (*.xls)|*.xls|Todos los Archivos (*.*)|*.*";
            sfdExportToxcel.FilterIndex = 1;
            sfdExportToxcel.RestoreDirectory = true;
            if (sfdExportToxcel.ShowDialog() == DialogResult.OK)
            {
                string path = sfdExportToxcel.FileName;
                exp.dataGridView2Excel(MyDataGridView, path, "NameSheet");
            }
        }

        public static long GetNewCode(string tbl_nam, string ColumnName)
        {
            long NewCode = 0;
            using (var cn = new SqlConnection(PlanningCnnStr))
            {
                cn.Open();
                var cmNewCode = new SqlCommand("Select IsNull(Max(" + ColumnName + "),0) From " + tbl_nam, cn);
                var Result = cmNewCode.ExecuteScalar();
                //NewCode = (long)Result;
                NewCode = (Result == DBNull.Value ? 0: long.Parse(Result.ToString())) + 1;
                cn.Close();
            }

            return NewCode;
        }

        public static string GetReportFolderPath()
        {
            string CheckParam = GetDBConfigParamValue(0, 104, UMCnnStr);
            string rptPath = Application.StartupPath;
            if (CheckParam is null || CheckParam.Trim().Equals("") || CheckParam.Trim().Equals("2"))
            {
                rptPath = Conversions.ToString(CheckHasBackSlash(rptPath));
                rptPath += @"Reports\";
            }
            else
            {
                CheckParam = GetDBConfigParamValue(0, 105, UMCnnStr);
                if (CheckParam is null || CheckParam.Trim().Equals(""))
                {
                    rptPath = Conversions.ToString(CheckHasBackSlash(rptPath));
                    rptPath += @"Reports\";
                }
                else
                {
                    CheckParam = Conversions.ToString(CheckHasBackSlash(CheckParam));
                    rptPath = CheckParam;
                }
            }

            return rptPath;
        }

        public static object CheckHasBackSlash(string DirPath)
        {
            if (!DirPath.Substring(DirPath.Length - 1).Equals(@"\"))
            {
                DirPath += @"\";
            }

            return DirPath;
        }

        public static string GetConfigParamValue(string FileName, string ParamName)
        {
            StreamReader Reader;
            string ConfigPath;
            string CurrentLine;
            string ParamValue = Constants.vbNullString;
            if (Conversions.ToString(Application.StartupPath[Application.StartupPath.Length - 1]) == @"\")
            {
                ConfigPath = Application.StartupPath + FileName;
            }
            else
            {
                ConfigPath = Application.StartupPath + @"\" + FileName;
            }

            if (File.Exists(ConfigPath))
            {
                if (ParamName.ToLower() == "cnnstr")
                {
                    string FlagValue = GetConfigParamValue(FileName, "Flag");
                    Reader = new StreamReader(ConfigPath);
                    if (string.IsNullOrEmpty(FlagValue) || Conversions.ToDouble(FlagValue) == 1d)
                    {
                        while (!Reader.EndOfStream)
                        {
                            CurrentLine = Reader.ReadLine();
                            if (!string.IsNullOrEmpty(CurrentLine) && Strings.Len(CurrentLine) >= Strings.Len(ParamName) && (CurrentLine.ToLower().Substring(0, Strings.Len(ParamName)) ?? "") == (ParamName.ToLower() ?? ""))
                            {
                                ParamValue = CurrentLine.Substring(CurrentLine.IndexOf("=") + 1);
                                break;
                            }
                        }
                    }
                    else if (Conversions.ToDouble(FlagValue) == 2d)
                    {
                        string EncryptionSeed = Constants.vbNullString;
                        string FileContent = Reader.ReadToEnd();
                        CurrentLine = FileContent.Substring(0, FileContent.IndexOf("Flag") - 2);
                        if (!string.IsNullOrEmpty(CurrentLine) && Strings.Len(CurrentLine) >= Strings.Len("CnnStr") && (CurrentLine.Substring(0, Strings.Len("CnnStr")).ToLower() ?? "") == ("CnnStr".ToLower() ?? ""))
                        {
                            EncryptionSeed = CurrentLine.Substring(CurrentLine.Length - ENCRYPTION_SEED_LENGTH);
                            CurrentLine = ConfigFile_Encryption(EncryptionSeed, CurrentLine.Substring(0, CurrentLine.Length - ENCRYPTION_SEED_LENGTH).Substring(CurrentLine.IndexOf("=") + 1));
                            ParamValue = CurrentLine.Substring(0, CurrentLine.Length - ENCRYPTION_SEED_LENGTH);
                        }
                    }
                }
                else
                {
                    Reader = new StreamReader(ConfigPath);
                    while (!Reader.EndOfStream)
                    {
                        CurrentLine = Reader.ReadLine();
                        if (!string.IsNullOrEmpty(CurrentLine) && Strings.Len(CurrentLine) >= Strings.Len(ParamName) && (CurrentLine.ToLower().Substring(0, Strings.Len(ParamName)) ?? "") == (ParamName.ToLower() ?? ""))
                        {
                            if (CurrentLine.IndexOf("=") > 0)
                            {
                                ParamValue = CurrentLine.Substring(CurrentLine.IndexOf("=") + 1);
                            }
                            else
                            {
                                ParamValue = CurrentLine;
                            }

                            break;
                        }
                    }
                }

                Reader.Close();
                Reader.Dispose();
            }
            else
            {
                MessageBox.Show("در مسیر برنامه یافت نشد " + FileName + " :فایل پیکربندی", MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                Environment.Exit(0);
            }

            return ParamValue;
        }

        public static string GetDBConfigParamValue(int mSoftwareCode, int mParamCode, string UMDefualtConnectionString = Constants.vbNullString)
        {
            string Paramvalue = Constants.vbNullString;
            string UMConnectionString;
            if (UMDefualtConnectionString is null || string.IsNullOrEmpty(UMDefualtConnectionString) || UMDefualtConnectionString.Equals(""))
            {
                UMConnectionString = GetConfigParamValue("psbs_Config.txt", "Cnnstr");
            }
            else
            {
                UMConnectionString = UMDefualtConnectionString;
            }

            if (mSoftwareCode == 100 && mParamCode == 1)
            {
                Paramvalue = UMConnectionString;
            }
            else
            {
                using (var cnParam = new SqlConnection(UMConnectionString))
                {
                    cnParam.Open();
                    var cmParam = new SqlCommand("Select ParameterValue From Tbl_SoftwareParameters Where SoftwareCode=" + mSoftwareCode + " And ParameterCode=" + mParamCode, cnParam);
                    var drParam = cmParam.ExecuteReader();
                    if (drParam.HasRows)
                    {
                        drParam.Read();
                        Paramvalue = drParam.GetString(0);
                    }

                    drParam.Close();
                    cnParam.Close();
                    drParam = null;
                }
            }

            return Paramvalue;
        }

        public static string ConfigFile_Encryption(string EncryptSeed, string ConfigParamValue)
        {
            int cc, ModValue, CodedChar;

            // Encrypt The text
            for (int X = 1, loopTo = Strings.Len(ConfigParamValue); X <= loopTo; X++)
            {
                ModValue = X % ENCRYPTION_SEED_LENGTH;
                cc = Strings.Asc(Strings.Mid(EncryptSeed, ModValue - ENCRYPTION_SEED_LENGTH * Conversions.ToInteger(ModValue == 0), 1));
                CodedChar = Strings.Asc(Strings.Mid(ConfigParamValue, X, 1)) ^ cc;
                switch (CodedChar)
                {
                    case 0:
                    case 9:
                    case 10:
                    case 13:
                    case 26:
                    case 28:
                    case 29:
                    case 30:
                    case 31:
                    case 39:
                    case 157:
                    case 158:
                    case 160:
                        {
                            break;
                        }

                    default:
                        {
                            var midTmp = Conversions.ToString((char)CodedChar);
                            StringType.MidStmtStr(ref ConfigParamValue, X, 1, midTmp);
                            break;
                        }
                }
            }

            return ConfigParamValue + EncryptSeed;
        }



        /// <summary>
    /// این تابع مشخص می کند یک روز مشخص در تقویم روز تعطیل می باشد یا نه
    /// </summary>
        public static bool IsHoliday(string CalendarCode, string CurrentDate)
        {
            const int DT_DEFAULT = -1;
            const int DT_HOLIDAY = 1;
            const int DT_COMMON = 2;
            switch (IsParticularDay(CalendarCode, CurrentDate))
            {
                case DT_DEFAULT:
                    {
                        using (var cn = new SqlConnection(PlanningCnnStr))
                        {
                            cn.Open();
                            var cmCheckHoliday = new SqlCommand("Select Count(*) From Tbl_HoliDays Where DayNo=" + Strings.Right(CurrentDate, 2) + " And MonthNo=" + Strings.Mid(CurrentDate, 5, 2), cn);
                            var result = int.Parse (cmCheckHoliday.ExecuteScalar().ToString()) ;
                            if (result > 0 ) // DT_HOLIDAY
                            {
                                return true;
                            }
                            else if (!(GetDayAccessibleTime(CalendarCode, GetDayNo(CurrentDate).ToString(), 1) == "00:00")) // DT_COMMON
                            {
                                return false;
                            }

                            cn.Close();
                        }

                        return true;
                    }

                case DT_HOLIDAY:
                    {
                        return true;
                    }

                case DT_COMMON:
                    {
                        return false;
                    }
            }

            return default;
        }

        /// <summary>
    /// این تابع مشخص می کند یک تاریخ مشخص در تقویم روز خاص می باشد یا نه
    /// </summary>
        public static short IsParticularDay(string CalendarCode, string ShamsiDate)
        {
            short ParticurarDay = 0;
            // mohsendokht
            if (CalendarCode is null)
            {
                return ParticurarDay;
                
            }

            using (var cn = new SqlConnection(PlanningCnnStr))
            {
                cn.Open();
                var cmParticularDay = new SqlCommand("Select IsNull(DayType, -1) As DayType From Tbl_CalendarParticularDays Where CalendarCode=" + CalendarCode + " And ShamsiDate='" + ShamsiDate + "'", cn);
                var drParticularDay = cmParticularDay.ExecuteReader();
                if (drParticularDay.Read())
                {
                    ParticurarDay = Conversions.ToShort(drParticularDay["DayType"]);
                }
                else
                {
                    ParticurarDay = -1;
                }

                drParticularDay.Close();
                cn.Close();
            }

            return ParticurarDay;
        }

        /// <summary>
    /// این تابع زمان در دسترس تقویم را محاسبه می کند
    /// </summary>
        public static string GetCalendarAccessibleTime(string CalendarCode)
        {
            var mAccessibleTimes = new TimeSpan(0, 0, 0, 0, 0);
            // mohsendokht
            if (CalendarCode is null)
            {
                return "0:0";
                
            }

            using (var cn = new SqlConnection(PlanningCnnStr))
            {
                cn.Open();
                var cm = new SqlCommand("Select * From Tbl_CalendarShifts Where CalendarCode=" + CalendarCode, cn);
                var dr = cm.ExecuteReader();
                while (dr.Read())
                    mAccessibleTimes += TimeSpan.Parse(dr["ShiftDuration"].ToString()) + TimeSpan.Parse(dr["ShiftExtraTime"].ToString());
                dr.Close();
                cm.CommandText = "Select * From Tbl_CalendarShiftDownTimes Where CalendarCode=" + CalendarCode;
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    if (TimeSpan.Parse(dr["DownTimeEnd"].ToString()) >= TimeSpan.Parse(dr["DownTimeStart"].ToString()))
                    {
                        mAccessibleTimes -= TimeSpan.Parse(dr["DownTimeEnd"].ToString()) - TimeSpan.Parse(dr["DownTimeStart"].ToString());
                    }
                }

                dr.Close();
                cn.Close();
            }

            if (mAccessibleTimes >= TimeSpan.Parse("1.00:00"))
            {
                mAccessibleTimes = TimeSpan.Parse("23:59:59");
            }

            string mHour = Conversions.ToString(Interaction.IIf(mAccessibleTimes.Hours < 10, "0" + mAccessibleTimes.Hours.ToString(), mAccessibleTimes.Hours.ToString()));
            string mMinute = Conversions.ToString(Interaction.IIf(mAccessibleTimes.Minutes < 10, "0" + mAccessibleTimes.Minutes.ToString(), mAccessibleTimes.Minutes.ToString()));
            return mHour + ":" + mMinute;
        }

        /// <summary>
    /// این تابع زمان در دسترس یک روز را باز می گرداند
    /// </summary>
        public static string GetDayAccessibleTime(string CalendarCode, string DayNo, short DayType)
        {
            string AccessibleTime = "00:00";
            const int DT_DEFUALT = 1; // روز پیض فرض
            const int DT_PARTICULAR = 2; // روز خاص
                                         // mohsendokht
            if (CalendarCode is null)
            {
                return ":0";
               
            }

            using (var cn = new SqlConnection(PlanningCnnStr))
            {
                cn.Open();
                var cmDayAccessibleTime = new SqlCommand("Select dbo.GetDayTime(" + CalendarCode + "," + DayNo + ")", cn);
                SqlDataReader drDayAccessibleTime = null;
                switch (DayType)
                {
                    case DT_DEFUALT:
                        {
                            var DayAccessibleTime = default(double);
                            drDayAccessibleTime = cmDayAccessibleTime.ExecuteReader();
                            if (drDayAccessibleTime.Read())
                            {
                                DayAccessibleTime = Conversions.ToDouble(drDayAccessibleTime[0]);
                            }

                            drDayAccessibleTime.Close();
                            int mHour = Conversions.ToInteger(DayAccessibleTime.ToString().Split('.')[0]);
                            int mMinute;
                            if (DayAccessibleTime.ToString().Split('.').Length == 1)
                            {
                                mMinute = 0;
                            }
                            else
                            {
                                mMinute = (int)Math.Round((DayAccessibleTime - mHour) * 60d);
                            }

                            AccessibleTime = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(mHour < 10, "0" + mHour.ToString(), mHour.ToString()), ":"), Interaction.IIf(mMinute < 10, "0" + mMinute.ToString(), mMinute.ToString())));
                            break;
                        }

                    case DT_PARTICULAR:
                        {
                            cmDayAccessibleTime.CommandText = "Select AccessibleWorkTime From Tbl_CalendarParticularDays Where CalendarCode=" + CalendarCode + " And ShamsiDate=" + DayNo;
                            drDayAccessibleTime = cmDayAccessibleTime.ExecuteReader();
                            if (drDayAccessibleTime.Read())
                            {
                                AccessibleTime = Conversions.ToString(drDayAccessibleTime["AccessibleWorkTime"]);
                            }

                            drDayAccessibleTime.Close();
                            if (!AccessibleTime.Contains(":"))
                            {
                                AccessibleTime += ":00";
                            }

                            break;
                        }
                }

                cn.Close();
            }

            return AccessibleTime;
        }

        /// <summary>
    /// این تابع زمان شروع تقویم را محاسبه می کند
    /// </summary>
        public static string GetCalendarStart(string CalendarCode)
        {
            string CalendarStart = "00:00";
            // mohsendokht
            if (CalendarCode is null)
            {
                MessageBox.Show("CalendarCode Is Nothing");
                return CalendarStart;
                
            }

            using (var cn = new SqlConnection(PlanningCnnStr))
            {
                cn.Open();
                var cmShifts = new SqlCommand("Select ShiftStart From Tbl_CalendarShifts Where CalendarCode=" + CalendarCode + " And ShiftNo = 1", cn);
                var drShifts = cmShifts.ExecuteReader();
                if (drShifts.Read())
                {
                    CalendarStart = drShifts["ShiftStart"].ToString();
                }

                drShifts.Close();
                cn.Close();
            }

            return CalendarStart;
        }

        /// <summary>
    /// این تابع زمان پایان تقویم را محاسبه می کند
    /// </summary>
        public static string GetCalendarEnd(string CalendarCode)
        {
            string CalendarEnd = "00:00";
            if (CalendarCode is null)
            {
                MessageBox.Show("CalendarCode Is Nothing");
                return CalendarEnd;
                
            }

            using (var cn = new SqlConnection(PlanningCnnStr))
            {
                cn.Open();
                var cm = new SqlCommand("Select ShiftStart,ShiftDuration,ShiftExtraTime From Tbl_CalendarShifts Where CalendarCode=" + CalendarCode + " Order By ShiftNo Desc", cn);
                var dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    var mCalendarEndTime = TimeSpan.Parse(dr["ShiftStart"].ToString()) + TimeSpan.Parse(dr["ShiftDuration"].ToString()) + TimeSpan.Parse(dr["ShiftExtraTime"].ToString());
                    if (mCalendarEndTime > TimeSpan.Parse("1.00:00"))
                    {
                        mCalendarEndTime = mCalendarEndTime - TimeSpan.Parse("1.00:00");
                    }

                    if (mCalendarEndTime == TimeSpan.Parse("1.00:00"))
                    {
                        mCalendarEndTime = TimeSpan.Parse("23:59:59");
                    }

                    string mHour = Conversions.ToString(Interaction.IIf(mCalendarEndTime.Hours < 10, "0" + mCalendarEndTime.Hours.ToString(), mCalendarEndTime.Hours.ToString()));
                    string mMinute = Conversions.ToString(Interaction.IIf(mCalendarEndTime.Minutes < 10, "0" + mCalendarEndTime.Minutes.ToString(), mCalendarEndTime.Minutes.ToString()));
                    CalendarEnd = mHour + ":" + mMinute;
                }

                dr.Close();
                cn.Close();
            }

            return CalendarEnd;
        }

        /// <summary>
    /// این تابع ضریب مصرف یک جزء درخت را با توجه به اجزاء پدر آن باز می گرداند را محاسبه می کند
    /// </summary>
        public static int GetDetailParentQuantity(string mTreeCode, string mDetailCode)
        {
            int DPQty = 1;
                var SQlqry= "Select IsNull(ParentQuantity,1) From Tbl_ProductTreeDetails Where TreeCode = " + mTreeCode + " And DetailCode = '" + mDetailCode + "'";
                DPQty = CmdExecuteScalarInt(SQlqry);
                SQlqry = "Select ParentDetailCode From Tbl_ProductTreeDetails Where TreeCode = " + mTreeCode + " And DetailCode = '" + mDetailCode + "'";

                var ParentDetailCode = CmdExecuteScalarStr(SQlqry); 
                if (ParentDetailCode != "0" && ParentDetailCode != "")
                {
                    DPQty *= GetDetailParentQuantity(mTreeCode, ParentDetailCode);
                }

   
            return DPQty;
        }

        public static string GetMatchedOperations(string mOpCode, string mTreeCode)
        {
            return "";
            // ---------------------------------------------------------------------------------
            // Dim mMatchedOperations As String = vbNullString
            // frmPlanning.FunctionLabel.Text = "OperationPlanning_FirstMethod"
            // 'Try

            // Using cn As New SqlConnection(PlanningCnnStr)
            // cn.Open()

            // Dim cm As New SqlCommand("Select MatchedOperationCode From Tbl_MatchedOperations Where TreeCode= " & mTreeCode & " And OperationCode = '" & mOpCode & "'", cn)
            // Dim dr As SqlDataReader = cm.ExecuteReader()

            // While dr.Read()
            // If mMatchedOperations = vbNullString OrElse Not mMatchedOperations.Contains(dr("MatchedOperationCode").ToString()) Then
            // mMatchedOperations &= IIf(mMatchedOperations = vbNullString, "'" & dr("MatchedOperationCode").ToString() & "'", ",'" & dr("MatchedOperationCode").ToString() & "'")
            // End If
            // End While

            // dr.Close()

            // cm.CommandText = "Select OperationCode From Tbl_MatchedOperations Where TreeCode= " & mTreeCode & " And MatchedOperationCode = '" & mOpCode & "'"
            // dr = cm.ExecuteReader()
            // Dim mOperationCodes As String = vbNullString

            // While dr.Read()
            // If mMatchedOperations = vbNullString OrElse Not mMatchedOperations.Contains(dr("OperationCode").ToString()) Then
            // mMatchedOperations &= IIf(mMatchedOperations = vbNullString, "'" & dr("OperationCode").ToString() & "'", ",'" & dr("OperationCode").ToString() & "'")

            // mOperationCodes &= IIf(mOperationCodes = vbNullString, "'" & dr("OperationCode").ToString() & "'", ",'" & dr("OperationCode").ToString() & "'")
            // End If
            // End While

            // dr.Close()

            // If mOperationCodes <> vbNullString Then
            // cm.CommandText = "Select MatchedOperationCode From Tbl_MatchedOperations Where TreeCode= " & mTreeCode & " And OperationCode IN (" & mOperationCodes & ")"
            // dr = cm.ExecuteReader()

            // While dr.Read()
            // If (mMatchedOperations = vbNullString OrElse Not mMatchedOperations.Contains(dr("MatchedOperationCode").ToString()) AndAlso Not dr("MatchedOperationCode").ToString().Equals(mOpCode)) Then
            // mMatchedOperations &= IIf(mMatchedOperations = vbNullString, "'" & dr("MatchedOperationCode").ToString() & "'", ",'" & dr("MatchedOperationCode").ToString() & "'")
            // End If
            // End While

            // dr.Close()
            // End If

            // cn.Close()
            // End Using

            // Return mMatchedOperations

        }

        public static string GetServerShamsiTime()
        {
            string mServerDate = GetServerLatinDate();
            var pclServerDate = new PersianCalendar();
            return pclServerDate.GetYear(Conversions.ToDate(mServerDate)) + pclServerDate.GetMonth(Conversions.ToDate(mServerDate)).ToString("D2") + pclServerDate.GetDayOfMonth(Conversions.ToDate(mServerDate)).ToString("D2");
        }

        private static string GetServerLatinDate()
        {
            string mServerDate = Constants.vbNullString;
            using (var cnn = new SqlConnection(PlanningCnnStr))
            {
                cnn.Open();
                var cmd = new SqlCommand("Select (Convert(VarChar,GetDate(),111)) As ServerDate", cnn);
                var rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    mServerDate = rd["ServerDate"].ToString();
                    // mServerDate = DateTime.Parse(mServerDate).ToString().Substring(0, 10)
                }

                rd.Close();
                cnn.Close();
            }

            return mServerDate;
        }

        public static string Get_LatinDate_FromPersianDate(string mPersianDate)
        {
            var pclServerDate = new PersianCalendar();
            int mPYear = Conversions.ToInteger(mPersianDate.Substring(0, 4));
            int mPMonth = Conversions.ToInteger(mPersianDate.Replace("/", "").Substring(4, 2));
            int mPDay = Conversions.ToInteger(mPersianDate.Replace("/", "").Substring(6, 2));
            int mPHour = new DateTime().TimeOfDay.Hours;
            int mPMinute = new DateTime().TimeOfDay.Minutes;
            int mPSecond = new DateTime().TimeOfDay.Seconds;
            if (mPMonth > 6)
            {
                if (mPDay > 30)
                {
                    mPDay = 30;
                }

                if (mPMonth == 12)
                {
                    if (!pclServerDate.IsLeapYear(mPYear))
                    {
                        if (mPDay > 29)
                        {
                            mPDay = 29;
                        }
                    }
                }
            }

            var mLatinDate = pclServerDate.ToDateTime(mPYear, mPMonth, mPDay, mPHour, mPMinute, mPSecond, 0, PersianCalendar.PersianEra);
            return mLatinDate.Year.ToString() + "/" + mLatinDate.Month.ToString() + "/" + mLatinDate.Day.ToString();
        }

        public static void ShowFile(string FilePath)
        {
            var prcLoad = new Process();

            // Try
            if (prcLoad is null)
            {
                {
                    var withBlock = new Process();
                    withBlock.StartInfo.FileName = FilePath;
                    withBlock.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                    withBlock.Start();
                }
            }
            else
            {
                if (prcLoad is null)
                {
                    prcLoad = new Process();
                }

                prcLoad.StartInfo.FileName = FilePath;
                prcLoad.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                prcLoad.Start();
            }
        }

        public static void Check_Subbatch_HasPlanningAlarm(string mTreeCode, string mBatchCode, string mSubbatchNo, string mAlarmCode, string mAlarmDesc)
        {
            using (var cn = new SqlConnection(PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    var dt = new DataTable();
                    string mQuery = "Select Distinct SubbatchCode From Tbl_Planning Where SubbatchCode IN (Select SubbatchCode From Tbl_ProductionSubbatchs Where StopPlanning = 0 And BatchCode IN (Select BatchCode From Tbl_ProductionBatchs Where ProductTreeCode = " + mTreeCode + " And (IsNull(FinishedDate,'0') = '0' Or IsNull(FinishedDate,'0') = '')))";
                    if (!string.IsNullOrEmpty(mBatchCode))
                    {
                        if (!string.IsNullOrEmpty(mSubbatchNo))
                        {
                            mQuery = "Select Distinct SubbatchCode From Tbl_Planning Where SubbatchCode IN (Select SubbatchCode From Tbl_ProductionSubbatchs Where StopPlanning = 0 And SubbatchNo >= " + mSubbatchNo + " And BatchCode IN (Select BatchCode From Tbl_ProductionBatchs Where BatchCode = '" + mBatchCode + "' And ProductTreeCode = " + mTreeCode + " And (IsNull(FinishedDate,'0') = '0' Or IsNull(FinishedDate,'0') = '')))";
                        }
                        else
                        {
                            mQuery = "Select Distinct SubbatchCode From Tbl_Planning Where SubbatchCode IN (Select SubbatchCode From Tbl_ProductionSubbatchs Where StopPlanning = 0 And BatchCode IN (Select BatchCode From Tbl_ProductionBatchs Where BatchCode = '" + mBatchCode + "' And ProductTreeCode = " + mTreeCode + " And (IsNull(FinishedDate,'0') = '0' Or IsNull(FinishedDate,'0') = '')))";
                        }
                    }
                    else if (!string.IsNullOrEmpty(mSubbatchNo))
                    {
                        mQuery = "Select Distinct SubbatchCode From Tbl_Planning Where SubbatchCode IN (Select SubbatchCode From Tbl_ProductionSubbatchs Where StopPlanning = 0 And SubbatchNo >= " + mSubbatchNo + " And BatchCode IN (Select BatchCode From Tbl_ProductionBatchs Where ProductTreeCode = " + mTreeCode + " And (IsNull(FinishedDate,'0') = '0' Or IsNull(FinishedDate,'0') = '')))";
                    }

                    {
                        var withBlock = new SqlDataAdapter(mQuery, cn);
                        withBlock.Fill(dt);
                    }

                    foreach (DataRow dr in dt.Rows)
                    {
                        {
                            var withBlock1 = new SqlCommand("Insert Into Tbl_SubbatchPlanningAlerts(SubbatchCode,AlertCode,AlertDescription) Values('" + dr["SubbatchCode"].ToString() + "'," + mAlarmCode + ",'" + mAlarmDesc + "')", cn);
                            withBlock1.ExecuteNonQuery();
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

        public static bool Delete_Subbatchs_PlanningAlarms()
        {
            using (var cn = new SqlConnection(PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    {
                        var withBlock = new SqlCommand("Delete From Tbl_SubbatchPlanningAlerts", cn);
                        withBlock.ExecuteNonQuery();
                    }
                }
                catch
                {
                    return false;
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }

            return true;
        }

        public static void SetGridColumnsWidth(string mFormName, ListFormCaller mListCaller, DataGridView mGrid)
        {
            SqlDataReader drSavingWidth = null;
            using (var cn = new SqlConnection(PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    var cmSavingWidth = new SqlCommand("Select * From Tbl_GridsColumnsWidth Where FormName = '" + mFormName + "' And GridName = '" + mGrid.Name + "' And ListCaller = " + (int)mListCaller + " And UserCode = " + Module_UserAccess.UserCodeTmp, cn);
                    drSavingWidth = cmSavingWidth.ExecuteReader();
                    while (drSavingWidth.Read())
                    {
                        try
                        {
                            mGrid.Columns[drSavingWidth["ColumnName"].ToString()].Width = Conversions.ToInteger(drSavingWidth["ColumnWidth"]);

                        }
                        catch (Exception)
                        {

                        }
                       
                    }
                    drSavingWidth.Close();
                    drSavingWidth = null;
                }
                catch (Exception )
                {
                    if (drSavingWidth is object)
                    {
                        drSavingWidth.Close();
                    }

                    MessageBox.Show("تنظیم طول ستونهای گرید با مشکل مواجه شد", MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }
        }
        
        public static void Openconnection()
        {
            if (cnProductionPlanning.State == ConnectionState.Closed)
                cnProductionPlanning.Open();
        }

        public static void Closeconnection()
        {
            if (cnProductionPlanning.State == ConnectionState.Open)
                cnProductionPlanning.Close();
        }
        
        public static int CmdExecuteScalarInt(string cmd)
        {

            var cmValidation = new SqlCommand(cmd, cnProductionPlanning);
            if (cnProductionPlanning.State == ConnectionState.Closed)
                cnProductionPlanning.Open();

            var value = cmValidation.ExecuteScalar();
            cnProductionPlanning.Close();

            return value == System.DBNull.Value ? 0: Conversions.ToInteger(value);
           
        }

        public static bool CmdExecuteScalarBool(string cmd)
        {

            var cmValidation = new SqlCommand(cmd, cnProductionPlanning);
            if (cnProductionPlanning.State == ConnectionState.Closed)
                cnProductionPlanning.Open();

            var value = cmValidation.ExecuteScalar();
            cnProductionPlanning.Close();

            return value == System.DBNull.Value ? false:Conversions.ToBoolean(value);

        }
        public static string CmdExecuteScalarStr(string cmd)
        {

            var cmValidation = new SqlCommand(cmd, cnProductionPlanning);
            if (cnProductionPlanning.State == ConnectionState.Closed)
                cnProductionPlanning.Open();

            var value = cmValidation.ExecuteScalar();
            cnProductionPlanning.Close();

            return value == System.DBNull.Value ? "": Conversions.ToString(value);

        }

        public static void CmdExecuteNonQuery(string sqlCmd)
        {
           var cmd = new SqlCommand(sqlCmd, cnProductionPlanning);
           if (cnProductionPlanning.State == ConnectionState.Closed)
               cnProductionPlanning.Open();

           cmd.ExecuteNonQuery();
           cnProductionPlanning.Close();
                 
        }
        public static void CheckMachineCodeForOperatorWork()
        {

            try
            {
                var exists = CmdExecuteScalarBool("SELECT CAST(1 AS BIT) FROM Tbl_Machines WHERE Code = '-1'");
                if (!exists)
                {
                    var InsertSQL = @"INSERT INTO dbo.Tbl_Machines
                                                (Code
                                                ,Name
                                                ,Producer
                                                ,ProducerCountry
                                                ,Application
                                                ,CalendarCode
                                                ,Description)
                                       SELECT TOP 1 
                                           Code = '-1'
                                          ,Name = 'عملیات اپراتوری'
                                          ,Producer = ''
                                          ,ProducerCountry = ''
                                          ,Application = ''
                                          ,CalendarCode = Tbl_Calendar.CalendarCode
                                          ,Description = 'این ماشین جهت عملیات اپراتوری استفاد شده است و جزیی از تنظیمات سیستم میباشد. تقویم کاری مشخص شده در برنامه ریزی سالیانه استفاده خواهد شد'
                                       FROM Tbl_Calendar
                                       ORDER bY Tbl_Calendar.CalendarCode";

                    CmdExecuteNonQuery(InsertSQL);
                    
                }

            }

            catch (SqlException ObjEx)
            {
                Logger.LogException("CheckMachineCodeForOperatorWork", ObjEx);
                MessageBox.Show("خطا در ثبت ماشین برای عملیاتهای اپراتوری", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);

            }

        }

    }
}