using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using ProductionPlanning.DBChange;
using ProductionPlanning.Planning_Forms.OperatorTasks;
using ProductionPlanning.ToolForms;
using static ProductionPlanning.MyEnums;

namespace ProductionPlanning
{
    public partial class frmMain
    {
        public frmMain()
        {
            InitializeComponent();
            _MenuItem1_1.Name = "MenuItem1_1";
            _MenuItem1_3_1.Name = "MenuItem1_3_1";
            _MenuItem1_3_2.Name = "MenuItem1_3_2";
            _MenuItem1_7.Name = "MenuItem1_7";
            _MenuItem1_4.Name = "MenuItem1_4";
            _MenuItem1_6.Name = "MenuItem1_6";
            _MenuItem1_8.Name = "MenuItem1_8";
            _MenuItem1_2.Name = "MenuItem1_2";
            _MenuItem1_9_1.Name = "MenuItem1_9_1";
            _MenuItem1_9_2.Name = "MenuItem1_9_2";
            _MenuItem1_5.Name = "MenuItem1_5";
            _MenuItem1_15.Name = "MenuItem1_15";
            _MenuItem1_10.Name = "MenuItem1_10";
            _MenuItem1_11.Name = "MenuItem1_11";
            _MenuItem1_12.Name = "MenuItem1_12";
            _MenuItem1_13.Name = "MenuItem1_13";
            _MenuItem1_14.Name = "MenuItem1_14";
            _MenuItem2_1.Name = "MenuItem2_1";
            _MenuItem2_2.Name = "MenuItem2_2";
            _MenuItem22_1.Name = "MenuItem22_1";
            _MenuItem3_1.Name = "MenuItem3_1";
            _MenuItem3_2.Name = "MenuItem3_2";
            _MenuItem3_3.Name = "MenuItem3_3";
            _MenuItem3_4.Name = "MenuItem3_4";
            _MenuItem3_5.Name = "MenuItem3_5";
            _MenuItem3_7.Name = "MenuItem3_7";
            _MenuItem4_1.Name = "MenuItem4_1";
            _MenuItem5_1.Name = "MenuItem5_1";
            _MenuItem5_2_1.Name = "MenuItem5_2_1";
            _MenuItem5_2_2.Name = "MenuItem5_2_2";
            _MenuItem5_2_3.Name = "MenuItem5_2_3";
            _MenuItem5_2_4.Name = "MenuItem5_2_4";
            _MenuItem5_2_5.Name = "MenuItem5_2_5";
            _mnuTools_Backup_CreateBackup.Name = "mnuTools_Backup_CreateBackup";
            _mnuTools_Backup_RestoreBackup.Name = "mnuTools_Backup_RestoreBackup";
            _mnuTools_SetBackground.Name = "mnuTools_SetBackground";
            _mnuTools_Helps_UsersGuide.Name = "mnuTools_Helps_UsersGuide";
            _mnuTools_Helps_ChangesProgress.Name = "mnuTools_Helps_ChangesProgress";
            _mnuTools_Helps_AboutSoftware.Name = "mnuTools_Helps_AboutSoftware";
            _MenuItemExit.Name = "MenuItemExit";
        }

        private Thread thrDailyDateTime = null;
        private AsyncCallback ac = null;
        private MethodInvoker mi = null;
        private bool MenuExitClicked = false;
        private int Plan_Alert_Change = 0;

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
               

                if (!Module_UserAccess.Check_Lock())
                {
                    MessageBox.Show("قفل سخت افزاری شناسایی نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    Environment.Exit(0);
                    return;
                }

                
                var appSettings = ConfigurationManager.AppSettings;
                this.Plan_Alert_Change = Conversions.ToInteger(appSettings["Plan_Alert_Change"] ?? "0");

                // update db
                var dbChange = new DBChange.DBChange();
                dbChange.UpdateDBversion();

            }
            catch (Exception objEx)
            {
                Logger.SaveError(Name + ".frmMain_Load", objEx.Message);
                MessageBox.Show("اجرای برنامه با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);

                // باز پس گیری تمام منابع در اختیار برنامه
                RetrieveAllResources();
                Close();
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Conversions.ToBoolean(!MenuExitClicked))
            {
                if (MessageBox.Show("برای خروج مطمئن هستید؟", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

            if (thrDailyDateTime is object)
            {
                try
                {
                    thrDailyDateTime.Abort();
                }
                catch
                {
                }
            }
        }

        private void mnuTools_SetBackground_Click(object sender, EventArgs e)
        {
            {
                var withBlock = new OpenFileDialog();
                withBlock.Filter = "JPEG Image Files(*.JPG)|*.jpg;*.jpeg";
                withBlock.Title = "انتخاب تصویر پس زمینۀ نرم افزار";
                if (withBlock.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string AppPath = Application.StartupPath;
                        if (!AppPath.Substring(AppPath.Length - 1).Equals(@"\"))
                        {
                            AppPath += @"\";
                        }

                        if (!withBlock.FileName.Equals(AppPath + "PlanningBG.JPG"))
                        {
                            if (BackgroundImage is object)
                            {
                                BackgroundImage.Dispose();
                            }

                            File.Copy(withBlock.FileName, AppPath + "PlanningBG.JPG", true);
                            BackgroundImage = new Bitmap(AppPath + "PlanningBG.JPG");
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogException("mnuTools_SetBackground_Click", ex);
                    }
                }
            }
        }

        private void SetToolStripLabels(SqlConnection mycn)
        {
            ToolStripStatusLabel1.Text = "App Version : " + Application.ProductVersion;
            ToolStripStatusLabel2.Text = " DataBase Name: " + mycn.Database + "(" + Module1.mDBVersion + ")";
            ToolStripStatusLabel3.Text = " User Code : " + Module_UserAccess.UserCodeTmp;
        }

        private void MenuItem1_1_Click(object sender, EventArgs e)
        {
            LoadListForm(ListFormCaller.LFC_OPERATRIONSTITLES, " عنوان پیش فرض فعالیتها");
        }

        private void MenuItem1_2_Click(object sender, EventArgs e)
        {
            LoadListForm(ListFormCaller.LFC_SUPPLIERS, " لیست مشخصات تامین کنندگان");
        }

        private void MenuItem1_3_1_Click(object sender, EventArgs e)
        {
            LoadListForm(ListFormCaller.LFC_TESTUNITS, " واحدهای سنجش");
        }

        private void MenuItem1_3_2_Click(object sender, EventArgs e)
        {
            LoadListForm(ListFormCaller.LFC_UNITSRELATIONS, " ارتباط بین واحدهای سنجش");
        }

        private void MenuItem1_4_Click(object sender, EventArgs e)
        {
            LoadListForm(ListFormCaller.LFC_PRIMARYMATERIALS, " مواد اولیه");
        }

        private void MenuItem1_5_Click(object sender, EventArgs e)
        {
            LoadListForm(ListFormCaller.LFC_MACHINES, " ماشین آلات");
        }

        private void MenuItem1_6_Click(object sender, EventArgs e)
        {
            LoadListForm(ListFormCaller.LFC_CONTRACTORS, " مشخصات پیمانکاران");
        }

        private void MenuItem1_7_Click(object sender, EventArgs e)
        {
            LoadListForm(ListFormCaller.LFC_STORES, " مشخصات انبارها");
        }

        private void MenuItem1_8_Click(object sender, EventArgs e)
        {
            LoadListForm(ListFormCaller.LFC_HALTREASON, " لیست علت توقفات عملیات");
        }

        private void MenuItem1_9_1_Click(object sender, EventArgs e)
        {
            LoadListForm(ListFormCaller.LFC_HOLIDAYS, " روزهای تعطیل رسمی در تقویم سال");
        }

        private void MenuItem1_9_2_Click(object sender, EventArgs e)
        {
            LoadListForm(ListFormCaller.LFC_ACTINGCALENDARS, " تقویم های کاری");
        }

        private void MenuItem1_10_Click(object sender, EventArgs e)
        {
            LoadListForm(ListFormCaller.LFC_PRODUCTIONPARTS, " مشخصات بخش های تولید");
        }

        private void MenuItem1_11_Click(object sender, EventArgs e)
        {
            LoadListForm(ListFormCaller.LFC_OPERATORS, " مشخصات اپراتورهای تولید");
        }

        private void MenuItem1_12_Click(object sender, EventArgs e)
        {
            LoadListForm(ListFormCaller.LFC_OPERATIONUNITS, " مشخصات ضرایب عملیاتها");
        }

        private void MenuItem1_13_Click(object sender, EventArgs e)
        {
            LoadListForm(ListFormCaller.LFC_CUSTOMERS, " لیست مشتریان");
        }

        private void MenuItem2_1_Click(object sender, EventArgs e)
        {
            LoadListForm(ListFormCaller.LFC_PRODUCTS, " محصولات");
        }

        private void MenuItem2_2_Click(object sender, EventArgs e)
        {
            My.MyProject.Forms.frmProductTreesList.MdiParent = this;
            My.MyProject.Forms.frmProductTreesList.Show();
        }

        private void MenuItem22_1_Click(object sender, EventArgs e)
        {
            {
                var withBlock = new frmOrdersList();
                withBlock.MdiParent = this;
                withBlock.Show();
            }
        }

        private void MenuItem3_1_Click_1(object sender, EventArgs e)
        {
            My.MyProject.Forms.frmMPSList.MdiParent = this;
            My.MyProject.Forms.frmMPSList.Show();
        }

        private void MenuItem3_2_Click(object sender, EventArgs e)
        {
            {
                var withBlock = new frmBatchRecordsList();
                withBlock.CallerForm = ListFormCaller.LFC_PRODUCTIONBATCHS;
                withBlock.MdiParent = this;
                withBlock.Show();
            }
        }

        private void MenuItem3_3_Click(object sender, EventArgs e)
        {
            LoadListForm(ListFormCaller.LFC_MACHINENOTAVAILABLE, " مشخصات زمانهای در دسترس نبودن ماشین");
        }

        private void MenuItem3_4_Click(object sender, EventArgs e)
        {
            My.MyProject.Forms.frmPlanningShow.MdiParent = this;
            My.MyProject.Forms.frmPlanningShow.Show();
        }

        private void MenuItem3_5_Click(object sender, EventArgs e)
        {
            var ListForm = new frmPlaningSubbatchsList();
            ListForm.MdiParent = this;
            ListForm.Show();
        }

        private void _MenuItem3_6_Click(object sender, EventArgs e)
        {
            // اختصاص فعالیت برنامه ریزی شده به اپراتور
            var ListForm = new frmOperatorTaskList();
            ListForm.MdiParent = this;
            ListForm.Show();
        }
        /* TODO ERROR: Skipped EndRegionDirectiveTrivia */

        private void MenuItem3_7_Click(object sender, EventArgs e)
        {
            {
                var withBlock = new frmBatchsStatus();
                withBlock.MdiParent = this;
                withBlock.Show();
            }
            // LoadListForm(ListFormCaller.LFC_BATCHSPRODUCTIONPROGRESS, " نمایش پیشرفت تولید بچ")
        }

        private void MenuItem5_1_Click(object sender, EventArgs e)
        {
            string AppPath = Application.StartupPath;
            string Arguments = Module_UserAccess.UserCodeTmp + " " + Module_UserAccess.UserPassTmp + " " + Module_UserAccess.MySoftwareCode + " " + GetReportAccess();
            if (!AppPath.Substring(AppPath.Length - 1, 1).Equals(@"\"))
            {
                AppPath += @"\";
            }

            AppPath += "PSB_ReportViewer.exe";
            if (File.Exists(AppPath))
            {
                try
                {
                    {
                        var withBlock = new Process();
                        withBlock.StartInfo.FileName = AppPath;
                        withBlock.StartInfo.Arguments = Arguments;
                        withBlock.Start();
                    }
                }
                catch (Exception objEx)
                {
                    Logger.SaveError(Name + ".MenuItem5_1_Click", objEx.Message);
                    MessageBox.Show("اجرای نرم افزار گزارشات با مشکل مواجه نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                }
            }
            else
            {
                MessageBox.Show("نرم افزار اجرای گزارشات در مسیر اجرایی برنامه یافت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }

        private void MenuItem5_2_1_Click(object sender, EventArgs e)
        {
            LoadListForm(ListFormCaller.LFC_OEE, "OEE لیست");
        }

        private void MenuItem5_2_2_Click(object sender, EventArgs e)
        {
            {
                var withBlock = new frmMachineEmptyTimes();
                withBlock.MdiParent = this;
                withBlock.Show();
            }
        }

        private void MenuItem5_2_3_Click(object sender, EventArgs e)
        {
            {
                var withBlock = new frmPartsPersonnelRequirement();
                withBlock.MdiParent = this;
                withBlock.Show();
            }
        }

        private void MenuItem5_2_4_Click(object sender, EventArgs e)
        {
            {
                var withBlock = new frmBatchDetour();
                withBlock.MdiParent = this;
                withBlock.Show();
            }
        }

        private void MenuItem5_2_5_Click(object sender, EventArgs e)
        {
            {
                var withBlock = new frmMachineProductionProgram();
                withBlock.MdiParent = this;
                withBlock.Show();
            }
        }

        private void MenuItem1_14_Click(object sender, EventArgs e)
        {
            My.MyProject.Forms.UsersDefine.MdiParent = this;
            My.MyProject.Forms.UsersDefine.Show();
        }

        private void MenuItem1_15_Click(object sender, EventArgs e)
        {
            LoadListForm(ListFormCaller.LFC_MACHINENOTAVAILABLEREASON, " لیست علت های در دسترس نبودن ماشین");
        }

        private void MenuItem4_1_Click(object sender, EventArgs e)
        {
            My.MyProject.Forms.frmRealProductionList.MdiParent = this;
            My.MyProject.Forms.frmRealProductionList.Show();
        }

        private void mnuTools_Helps_AboutSoftware_Click(object sender, EventArgs e)
        {
            My.MyProject.Forms.AboutBox1.ShowDialog();
        }

        private void MenuItemExit_Click(object sender, EventArgs e)
        {
            MenuExitClicked = true;
            Application.Exit();
        }

        private void LoadListForm(ListFormCaller ListType, string Title)
        {
            var ListForm = new frmRecordsLists();
            ListForm.CallerForm = ListType;
            ListForm.MdiParent = this;
            ListForm.Text = Title;
            ListForm.Show();
        }

        private void RetrieveAllResources()
        {
            // باز پس گیری تمام منابع در اختیار برنامه
            Module1.cnProductionPlanning = null;
            GC.Collect();
            // خروج از برنامه
            Application.Exit();
        }

        private string GetReportAccess()
        {
            string mAccess = Constants.vbNullString;
            mAccess = Conversions.ToString(mAccess + Interaction.IIf(Module_UserAccess.HaveAccessToItem(73), "11", "00")); // تعریف گروه گزارشات
            mAccess = Conversions.ToString(mAccess + Interaction.IIf(Module_UserAccess.HaveAccessToItem(74), "11", "00")); // تعریف گزارشات
            return mAccess;
        }

        private void mnuTools_Backup_CreateBackup_Click(object sender, EventArgs e)
        {
            // If Me.MdiChildren.Length > 0 Then
            // MessageBox.Show("براي ايجاد نسخه پشتيبان بايد تمامي فرم هاي برنامه بسته شود", MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)

            // Exit Sub
            // End If

            {
                var withBlock = new frmBackup();
                withBlock.ConnectionString = Module1.PlanningCnnStr;
                withBlock.DatabaseName = Module1.cnProductionPlanning.Database; // "PSB_ProductionPlanning"
                withBlock.MessagesHeader = "برنامه ریزی تولید ";
                withBlock.BackupFormMode = frmBackup.BackupFormModeEnum.BFM_CREATE_BACKUP;
                withBlock.ShowDialog();
            }
        }

        private void mnuTools_Backup_RestoreBackup_Click(object sender, EventArgs e)
        {
            // If Me.MdiChildren.Length > 0 Then
            // MessageBox.Show("براي جايگذاري نسخه پشتيبان بايد تمامي فرمهاي برنامه بسته شود", MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)

            // Exit Sub
            // End If

            {
                var withBlock = new frmBackup();
                withBlock.ConnectionString = Module1.PlanningCnnStr;
                withBlock.DatabaseName = "PSB_ProductionPlanning";
                withBlock.MessagesHeader = "برنامه ریزی تولید ";
                withBlock.BackupFormMode = frmBackup.BackupFormModeEnum.BFM_RESTORE_BACKUP;
                withBlock.ShowDialog();
            }
        }

        private void ShowDailyDateTime()
        {
            // While True
            // Try
            // Dim NowFDate As String = mServerShamsiDate
            // NowFDate = NowFDate.Substring(0, 4) + "/" + NowFDate.Substring(4, 2) + "/" + NowFDate.Substring(6, 2)

            // 'tsslDateTime.Text = GetDayName(DateTime.Now.ToString()) & "  " & NowFDate & " -- " & DateTime.Now.TimeOfDay.ToString().Substring(0, 8)
            // tsslDateTime.Text = GetDayName(DateTime.Now.ToString()) & "  " & NowFDate

            // Application.DoEvents()
            // Catch ex As Exception
            // End Try
            // End While

            string NowFDate = Module1.mServerShamsiDate;
            NowFDate = NowFDate.Substring(0, 4) + "/" + NowFDate.Substring(4, 2) + "/" + NowFDate.Substring(6, 2);
            tsslDateTime.Text = Module1.GetDayName(Conversions.ToDate(DateTime.Now.ToString())) + "  " + NowFDate;
        }

        private void SetBackgroundImage()
        {
            try
            {
                string AppPath = Application.StartupPath;
                if (!AppPath.Substring(AppPath.Length - 1).Equals(@"\"))
                {
                    AppPath += @"\";
                }

                if (File.Exists(AppPath + "PlanningBG.JPG"))
                {
                    BackgroundImage = new Bitmap(AppPath + "PlanningBG.JPG");
                }
            }
            catch (Exception ex)
            {
                Logger.LogException("SetBackgroundImage", ex);
            }
        }

        private void mnuTools_Helps_UsersGuide_Click(object sender, EventArgs e)
        {
            string mHelpPath = Application.StartupPath;
            if (!mHelpPath.Substring(mHelpPath.Length - 1).Equals(@"\"))
            {
                mHelpPath += @"\";
            }

            if (File.Exists(mHelpPath + @"Help\EPPS_UserGuide.pdf"))
            {
                Module1.ShowFile(mHelpPath + @"Help\EPPS_UserGuide.pdf");
            }
            else if (File.Exists(mHelpPath + @"Help\EPPS_UserGuide.doc"))
            {
                Module1.ShowFile(mHelpPath + @"Help\EPPS_UserGuide.doc");
            }
            else
            {
                MessageBox.Show("فایل راهنمای نرم افزار در مسیر اجرای نرم افزار یافت نشد", "سیستم برنامه ریزی و کنترل تولید ", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }

        private void mnuTools_Helps_ChangesProgress_Click(object sender, EventArgs e)
        {
            string mHelpPath = Application.StartupPath;
            if (!mHelpPath.Substring(mHelpPath.Length - 1).Equals(@"\"))
            {
                mHelpPath += @"\";
            }

            if (File.Exists(mHelpPath + @"Help\Epps_ChangeProgress.doc"))
            {
                Module1.ShowFile(mHelpPath + @"Help\Epps_ChangeProgress.doc");
            }
            else
            {
                MessageBox.Show("فایل گزارش روند تغییرات نرم افزار در مسیر اجرای نرم افزار یافت نشد", "سیستم برنامه ریزی و کنترل تولید ", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }

        /* TODO ERROR: Skipped RegionDirectiveTrivia */
        private void PlanningAlertTimerStart()
        {
            
            
            
            if (Plan_Alert_Change != 0 && Plan_Alert_Change < Module1.PLANNING_ALERT_TIMER_INTERVAL)
            {
                Plan_Alert_Change = Module1.PLANNING_ALERT_TIMER_INTERVAL;
            }

            var tmr = new System.Timers.Timer(Plan_Alert_Change + 100);
            tmr.Elapsed += TimerElapsed;
            tmr.Enabled = true;
        }

        private void DeleteAsync(IAsyncResult ar)
        {
            mi.EndInvoke(ar);
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (Module1.PalnningAlarmReShow)
            {
                ControlPlanningAlerts();
            }
        }

        private void ControlPlanningAlerts()
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    {
                        var withBlock = new SqlCommand("Select Count(*) From Tbl_SubbatchPlanningAlerts", cn);
                        if (Conversions.ToInteger(withBlock.ExecuteScalar().ToString()) > 0)
                        {
                            foreach (Form frm in MdiChildren)
                            {
                                if (frm is frmAlert)
                                {
                                    return;
                                }
                            }

                            var updaterMI = new MethodInvoker(LoadAlertForm);
                            BeginInvoke(updaterMI);
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

        protected void LoadAlertForm()
        {
            try
            {
                {
                    var withBlock = new frmAlert();
                    withBlock.MdiParent = this;
                    withBlock.Show();
                }
            }
            catch
            {
            }
        }

        private void frmMain_Activated(object sender, EventArgs e)
        {
            if (this.Tag.ToString() == "0")
            {
                var frmAuthentication = new frmAuthentication();
                frmAuthentication.ShowDialog();
                this.Tag = "1";
                initAppSettings();
                
            }
        }

        private void initAppSettings()
        {
            
            Module1.MainFormObject = this;

            Module1.mServerShamsiDate = Module1.GetServerShamsiTime();
            {
                var withBlock = new Thread(SetBackgroundImage);
                withBlock.Start();
            }

            Module1.cnProductionPlanning = new SqlConnection(Module1.PlanningCnnStr);
            Module1.cnProductionPlanning.Open();
            Module1.cnProductionPlanning.Close();
            Module_UserAccess.Set_AccessToMenuItems(Module_UserAccess.UserCodeTmp.ToString());
            SetToolStripLabels(Module1.cnProductionPlanning);

            // thrDailyDateTime = New Thread(New ThreadStart(AddressOf ShowDailyDateTime))
            // thrDailyDateTime.Start()
            ShowDailyDateTime();
            if (Module_UserAccess.HaveAccessToItem(59) && this.Plan_Alert_Change != 0)
            {
                ControlPlanningAlerts();
                ac = new AsyncCallback(DeleteAsync);
                mi = new MethodInvoker(PlanningAlertTimerStart);
                mi.BeginInvoke(ac, null);

                // mAsyncResult = Me.BeginInvoke(New dlgPlanningAlerts(AddressOf PlanningAlertTimerStart))
            }

            // Set Menu font style:
            var appSettings = ConfigurationManager.AppSettings;
            string FontName = appSettings["MenuFontName"] ?? "";
            int FontSize = Conversions.ToInteger( appSettings["MenuFontSize"] ?? "0");

            if (FontName != "" && FontSize > 0)
                MenuStrip1.Font = new Font(FontName, FontSize);
        }

        private void mnuTools_ErrorsLogged_Click(object sender, EventArgs e)
        {
            var frm = new ErrorLogForm();
            frm.MdiParent = this;
            frm.Show();
        }

       
    }
}