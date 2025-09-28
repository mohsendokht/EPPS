using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;

namespace ProductionPlanning
{
    public partial class frmAlert
    {
        public frmAlert()
        {
            InitializeComponent();
            _cmdViewList.Name = "cmdViewList";
            _cmdClose.Name = "cmdClose";
        }

        private DataTable dt = null;
        private AsyncCallback ac = null;
        private MethodInvoker mi = null;

        private void frmAlert_Load(object sender, EventArgs e)
        {
            ac = new AsyncCallback(DeleteAsync);
            mi = new MethodInvoker(PlanningAlertTimerStart);
            mi.BeginInvoke(ac, null);
            //test chang 1
        }

        private void frmAlert_Shown(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }

        private void cmdViewList_Click(object sender, EventArgs e)
        {
            if (cmdViewList.Tag.ToString().Equals("1"))
            {
                using (var cn = new SqlConnection(Module1.PlanningCnnStr))
                {
                    try
                    {
                        var dt = new DataTable();
                        {
                            var withBlock = new SqlDataAdapter("Select SubbatchCode As 'کد ساب بچ', AlertDescription As 'شرح تغییر' From Tbl_SubbatchPlanningAlerts", cn);
                            withBlock.Fill(dt);
                        }

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("تغییر ثبت شده ای یافت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                            Close();
                        }
                        else
                        {
                            {
                                var withBlock1 = dgList;
                                withBlock1.DataSource = null;
                                withBlock1.DataSource = dt.DefaultView;
                                withBlock1.Columns[0].Width = 100;
                                withBlock1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                            }

                            cmdViewList.Tag = "2";
                            cmdViewList.Text = ">> بستن لیست";
                            Size = new Size(553, 339);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.SaveError(Name + ".cmdViewList_Click", ex.Message);
                        MessageBox.Show("نمایش لیست با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    }
                }
            }
            else if (cmdViewList.Tag.ToString().Equals("2"))
            {
                dgList.DataSource = null;
                cmdViewList.Tag = "1";
                cmdViewList.Text = "<< نمایش لیست";
                Size = new Size(553, 131);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Module1.PalnningAlarmReShow = !chkNotReshow.Checked;
            Close();
        }

        private void PlanningAlertTimerStart()
        {
            var tmr = new System.Timers.Timer(Module1.PLANNING_ALERT_TIMER_INTERVAL);
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
                ControlPlanningAlerts(sender);
            }
            else if (sender is System.Timers.Timer)
            {
                ((System.Timers.Timer)sender).Enabled = false;
            }
        }

        private void ControlPlanningAlerts(object sender)
        {
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    dt = new DataTable();
                    {
                        var withBlock = new SqlDataAdapter("Select SubbatchCode As 'کد ساب بچ', AlertDescription As 'شرح تغییر' From Tbl_SubbatchPlanningAlerts", cn);
                        withBlock.Fill(dt);
                    }

                    if (dt.Rows.Count == 0)
                    {
                        if (sender is System.Timers.Timer)
                        {
                            ((System.Timers.Timer)sender).Enabled = false;
                        }

                        Close();
                    }
                    else if (cmdViewList.Tag.ToString().Equals("2"))
                    {
                        var updaterMI = new MethodInvoker(LoadAlertList);
                        BeginInvoke(updaterMI);
                    }
                }
                catch
                {
                }
            }
        }

        protected void LoadAlertList()
        {
            try
            {
                {
                    var withBlock = dgList;
                    withBlock.DataSource = null;
                    withBlock.Columns.Clear();
                    withBlock.DataSource = dt.DefaultView;
                    withBlock.Columns[0].Width = 100;
                    withBlock.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch
            {
            }
        }
    }
}