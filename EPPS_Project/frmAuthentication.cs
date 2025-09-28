using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmAuthentication
    {
        public frmAuthentication()
        {
            InitializeComponent();
            _Cancel.Name = "Cancel";
            _OK.Name = "OK";
            _txtPassword.Name = "txtPassword";
            _txtUserCode.Name = "txtUserCode";
            _cbUserName.Name = "cbUserName";
        }

        //private SqlCommand objcommand;
        //private DataSet objdataset;
        //private SqlDataAdapter objdataadapter;
        //private string mConnectionString;
        public string pass;
        public string username;
        private DataTable dtUsersName = new DataTable();

        private void frmAuthentication_Load(object sender, EventArgs e)
        {
            string TempPath = Path.GetTempPath();
            Module_UserAccess.Ltmp = new PSB_Data1.Superpro((PSB_Data1.enumLockMode)1);
            if (!TempPath.Substring(TempPath.Length - 1).Equals(@"\"))
            {
                TempPath += @"\";
            }

            if (File.Exists(TempPath + "UpdateStepsLogger.Txt"))
            {
                try
                {
                    File.Delete(TempPath + "UpdateStepsLogger.Txt");
                }
                catch
                {
                }
            }

            Module1.NeedForUpdate();
            Module_UserAccess.ParseCommandLineArgs();
            try
            {
                Module1.UMCnnStr = Module1.GetConfigParamValue("psbs_Config.txt", "CnnStr");
                Module1.PlanningCnnStr = Module1.GetDBConfigParamValue(Conversions.ToInteger(Module_UserAccess.MySoftwareCode), 1, Module1.UMCnnStr);
                Module1.cnUMPSBS = new SqlConnection(Module1.UMCnnStr);
                Module1.cnUMPSBS.Open();
                Module1.cnUMPSBS.Close();
            }
            catch (Exception objEx)
            {
                Logger.SaveError(Name + ".frmAuthentication_Load", objEx.Message);
                MessageBox.Show("ارتباط با بانک اطلاعاتی مدیریت کاربران با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                Environment.Exit(0);
                return;
            }

            Module_UserAccess.MyIPLock = Module1.GetDBConfigParamValue(0, 101, Module1.UMCnnStr);
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    {
                        var withBlock = new SqlCommand("Select IsNull(DB_Version,'') As DB_Version From Tbl_DB_Version", cn);
                        var dr = withBlock.ExecuteReader();
                        if (dr.Read())
                        {
                            Module1.mDBVersion = dr["DB_Version"].ToString();
                        }

                        dr.Close();
                    }

                    cn.Close();
                }
                catch (Exception ex)
                {
                    Logger.SaveError(Name + ".frmAuthentication_Load", ex.Message);
                    MessageBox.Show("ارتباط با بانک اطلاعاتی برنامه ریزی با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    Environment.Exit(0);
                    return;
                }
            }

            //int sfResult = 0;
            var daUsersName = new SqlDataAdapter("Select A.* From UserProperty A Inner Join SoftwareUsers B On A.UserCode = B.UserCode Where (A.DisabledFlag Is Null Or A.DisabledFlag = 0) And B.SoftwareCode = " + Module_UserAccess.MySoftwareCode, Module1.UMCnnStr);
            daUsersName.Fill(dtUsersName);
            {
                var withBlock1 = cbUserName;
                withBlock1.DataSource = dtUsersName;
                withBlock1.DisplayMember = "UserName";
                withBlock1.ValueMember = "UserCode";
            }

            if (Conversions.ToDouble(Module1.GetDBConfigParamValue(0, 103, Module1.UMCnnStr)) == 1d)
            {
                UsernameLabel.Text = "نام کاربر";
                cbUserName.Text = Constants.vbNullString;
                txtUserCode.Visible = false;
                cbUserName.Visible = true;
                cbUserName.Focus();
            }
            else
            {
                UsernameLabel.Text = "کد کاربری";
                txtUserCode.Text = Constants.vbNullString;
                cbUserName.Visible = false;
                txtUserCode.Visible = true;
                txtUserCode.Focus();
            }

            // TODO: CheckLock, Temp login:-
            // ===================================================
            var appSettings = ConfigurationManager.AppSettings;
            string result = appSettings["Environment"] ?? "";
            var ExpireDate = new DateTime(2023, 12, 30);
                          
            if (result == "Development" && DateAndTime.Now.Date < ExpireDate)
            {
                Module_UserAccess.LoginFlag = 1;
                Module_UserAccess.arg_usercode = "1";
                Module_UserAccess.arg_userpass = "1";
            }
            // ===================================================

            if (Module_UserAccess.LoginFlag == 1) // have arguman
            {
                if (Module_UserAccess.Do_Login(Module_UserAccess.arg_usercode, Module_UserAccess.arg_userpass))
                {
                    LoadMainform(Module_UserAccess.arg_usercode, Module_UserAccess.arg_userpass);
                }
                else
                {
                    MessageBox.Show("مشخصات کاربري  صحيح نيست", "EPPS ", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    Environment.Exit(0);
                    return;
                }
            }
            else
            {
                Panel1.Visible = true;
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (txtUserCode.Visible)
            {
                if (string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtUserCode.Text))
                {
                    MessageBox.Show(".مشخصات کاربر را کامل وارد نماييد", " EPPS ", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    txtUserCode.Focus();
                    DialogResult = DialogResult.None;
                    return;
                }
            }
            else if (cbUserName.SelectedIndex == -1 || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show(".مشخصات کاربر را کامل وارد نماييد", "EPPS ", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbUserName.Focus();
                DialogResult = DialogResult.None;
                return;
            }

            string UserCode = Conversions.ToString(Interaction.IIf(txtUserCode.Visible, txtUserCode.Text, cbUserName.SelectedValue));
            if (Module_UserAccess.Do_Login(UserCode, txtPassword.Text))
            {
                LoadMainform(UserCode, txtPassword.Text);
                // 'If Not CheckUserExists(mSoftwareCode, UN(0), mServerShamsiDate, UMCnnStr, False) Then
                // '    Exit Sub
                // 'End If

                // 'IsDirectLogin = True
                // UserCodeTmp = UserCode
                // UserPassTmp = txtPassword.Text

                // Set_UserAccess()

                // MainFormObject = New frmMain()
                // MainFormObject.Show()
                // ' frmMain.Show()
                // Me.Close()
            }
        }

        private void Set_UserAccess()
        {
            var Objdataadapter = new SqlDataAdapter("Select SoftwareUsers.AccessLimit, UserProperty.UserName FROM SoftwareUsers INNER JOIN UserProperty ON SoftwareUsers.UserCode = UserProperty.UserCode WHERE     (SoftwareUsers.UserCode = " + Module_UserAccess.UserCodeTmp + ") AND (SoftwareUsers.SoftwareCode =" + Module_UserAccess.MySoftwareCode + " )", Module1.UMCnnStr);
            var dt = new DataTable();
            Objdataadapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Module_UserAccess.UserLimitStr = dt.Rows[0]["AccessLimit"].ToString();
            }
            else
            {
                Module_UserAccess.UserLimitStr = "110000000000";
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmAuthentication_KeyDown(object sender, KeyEventArgs e)
       {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txtUserCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Strings.Asc(e.KeyChar) == 39)
            {
                e.KeyChar = Conversions.ToChar("");
            }
            else if (Conversions.ToString(e.KeyChar) == "ی")
            {
                e.KeyChar = 'ي';
            }
        }

        private void LoadMainform(string UCode, string UPass)
        {
            Module_UserAccess.UserCodeTmp = UCode;
            Module_UserAccess.UserPassTmp = UPass;
            Set_UserAccess();
            //Module1.MainFormObject = frmMain();
            //Module1.MainFormObject.Show();

            // AddSoftwareLog(mSoftwareCode, UN(0), mServerShamsiDate, UM_CnnStr, 1, "ورود به نرم افزار کاليبراسيون")

            Close();
        }
    }
}