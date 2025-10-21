using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmBackup
    {
        public frmBackup()
        {
            InitializeComponent();
            _chkBackup.Name = "chkBackup";
            _cmdUMBrowser.Name = "cmdUMBrowser";
            _cmdBrowser.Name = "cmdBrowser";
            _chkUMBackup.Name = "chkUMBackup";
            _txtUMBackupFileName.Name = "txtUMBackupFileName";
            _txtBackupFileName.Name = "txtBackupFileName";
            _cmdCancel.Name = "cmdCancel";
            _cmdRestore.Name = "cmdRestore";
            _cmdBackup.Name = "cmdBackup";
        }

        private string mConnectionString = Constants.vbNullString;
        private string mDatabaseName = Constants.vbNullString;
        private string mErrors_Messages_Header = Application.ProductName;
        private BackupFormModeEnum mFormMode;
        private bool CloseFlag = true;

        public enum BackupFormModeEnum
        {
            BFM_CREATE_BACKUP,
            BFM_RESTORE_BACKUP
        }

        public BackupFormModeEnum BackupFormMode
        {
            set
            {
                mFormMode = value;
            }
        }

        public string ConnectionString
        {
            set
            {
                mConnectionString = value;
            }
        }

        public string DatabaseName
        {
            set
            {
                mDatabaseName = value;
            }
        }

        public string MessagesHeader
        {
            set
            {
                mErrors_Messages_Header = value;
            }
        }

        private void frmBackup_Load(object sender, EventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.IsMdiContainer)
                {
                    var mMainFrom = frm;
                    if (mMainFrom.MdiChildren.Length > 0)
                    {
                        if (mMainFrom.MdiChildren.Length == 1)
                        {
                            if (mMainFrom.MdiChildren[0].Name.Equals(Name))
                            {
                                break;
                            }
                        }

                        MessageBox.Show("براي ايجاد یا جایگذاری نسخۀ پشتيبان، بايد تمامي فرم هاي برنامه بسته باشد", mErrors_Messages_Header, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        CloseFlag = false;
                        Close();
                        return;
                    }
                }
            }

            if (mDatabaseName is null || string.IsNullOrEmpty(mDatabaseName))
            {
                MessageBox.Show("نام بانک اطلاعاتی مشخص نیست", mErrors_Messages_Header, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                CloseFlag = false;
                Close();
            }
            else if (mConnectionString is null || string.IsNullOrEmpty(mConnectionString))
            {
                MessageBox.Show("رشتۀ اتصال به بانک اطلاعاتی مشخص نیست", mErrors_Messages_Header, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                CloseFlag = false;
                Close();
            }

            switch (mFormMode)
            {
                case BackupFormModeEnum.BFM_CREATE_BACKUP:
                    {
                        cmdBackup.Visible = true;
                        Text = "ایجاد پشتیبان ";
                        break;
                    }

                case BackupFormModeEnum.BFM_RESTORE_BACKUP:
                    {
                        cmdRestore.Visible = true;
                        Text = "جایگذاری پشتیبان ";
                        break;
                    }
            }

            txtBackupFileName.Text = "";
            txtUMBackupFileName.Text = "";
        }

        private void frmBackup_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = CloseFlag;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            CloseFlag = false;
            Close();
        }

        private void cmdBrowser_Click(object sender, EventArgs e)
        {
            try
            {
                if (mFormMode == BackupFormModeEnum.BFM_CREATE_BACKUP)
                {
                    {
                        var withBlock = new SaveFileDialog();
                        withBlock.Title = "انتخاب مسير فایل پشتیبان نرم افزار ";
                        withBlock.OverwritePrompt = true;
                        withBlock.RestoreDirectory = true;
                        withBlock.Filter = "backup file (*.bak)|*.bak";
                        if (txtBackupFileName.Text is null || txtBackupFileName.Text.Trim().Equals(""))
                        {
                            withBlock.FileName = "Backup_" + mDatabaseName + "_" + Module1.mServerShamsiDate + ".bak";
                        }
                        else
                        {
                            withBlock.FileName = txtBackupFileName.Text;
                        }

                        if (withBlock.ShowDialog() == DialogResult.OK)
                        {
                            txtBackupFileName.Text = withBlock.FileName;
                        }
                    }
                }
                else if (mFormMode == BackupFormModeEnum.BFM_RESTORE_BACKUP)
                {
                    {
                        var withBlock1 = new OpenFileDialog();
                        withBlock1.Title = "انتخاب فایل پشتیبان نرم افزار ";
                        withBlock1.Filter = "backup file (*.bak)|*.bak";
                        withBlock1.RestoreDirectory = true;
                        withBlock1.Multiselect = false;
                        if (withBlock1.ShowDialog() == DialogResult.OK)
                        {
                            txtBackupFileName.Text = withBlock1.FileName;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveError(Name + ".cmdBrowser_Click", ex.Message);
                MessageBox.Show("فراخوانی فرم انتخاب مسیر پشتیبان نرم افزار با اشکال مواجه شد", mErrors_Messages_Header, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
            finally
            {
                CloseFlag = true;
            }
        }

        private void cmdUMBrowser_Click(object sender, EventArgs e)
        {
            try
            {
                string mShamsiDate = Module1.mServerShamsiDate;
                if (mFormMode == BackupFormModeEnum.BFM_CREATE_BACKUP)
                {
                    {
                        var withBlock = new SaveFileDialog();
                        withBlock.Title = "انتخاب مسير فایل پشتیبان مدیریت کاربران ";
                        withBlock.OverwritePrompt = true;
                        withBlock.RestoreDirectory = true;
                        withBlock.Filter = "backup file (*.bak)|*.bak";
                        if (txtUMBackupFileName.Text is null || txtUMBackupFileName.Text.Trim().Equals(""))
                        {
                            withBlock.FileName = "Backup_UM_PSBS_" + mShamsiDate + ".bak";
                        }
                        else
                        {
                            withBlock.FileName = txtUMBackupFileName.Text;
                        }

                        if (withBlock.ShowDialog() == DialogResult.OK)
                        {
                            txtUMBackupFileName.Text = withBlock.FileName;
                        }
                    }
                }
                else if (mFormMode == BackupFormModeEnum.BFM_RESTORE_BACKUP)
                {
                    {
                        var withBlock1 = new OpenFileDialog();
                        withBlock1.Title = "انتخاب فایل پشتیبان مدیریت کاربران ";
                        withBlock1.Filter = "backup file (*.bak)|*.bak";
                        withBlock1.RestoreDirectory = true;
                        withBlock1.Multiselect = false;
                        if (withBlock1.ShowDialog() == DialogResult.OK)
                        {
                            txtUMBackupFileName.Text = withBlock1.FileName;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveError(Name + ".cmdUMBrowser_Click", ex.Message);
                MessageBox.Show("فراخوانی فرم انتخاب مسیر پشتیبان مدیریت کاربران با اشکال مواجه شد", mErrors_Messages_Header, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
            finally
            {
                CloseFlag = true;
            }
        }

        private void cmdBackup_Click(object sender, EventArgs e)
        {
            string mOperationsMessage = Constants.vbNullString;
            string FilePath = Constants.vbNullString;
            string UMFilePath = Constants.vbNullString;
            if (chkBackup.Checked)
            {
                FilePath = FileNameValidation(txtBackupFileName, "نرم افزار");
                if (string.IsNullOrEmpty(FilePath))
                {
                    return;
                }
            }

            if (chkUMBackup.Checked)
            {
                UMFilePath = FileNameValidation(txtUMBackupFileName, "مدیریت کاربران");
                if (string.IsNullOrEmpty(UMFilePath))
                {
                    return;
                }
            }

            Cursor = Cursors.WaitCursor;
            if (chkBackup.Checked)
            {
                using (var cnBackup = new SqlConnection(mConnectionString))
                {
                    try
                    {
                        cnBackup.Open();
                        var cmBackup = new SqlCommand("Backup DataBase " + mDatabaseName + " To Disk='" + FilePath + "'", cnBackup);
                        SqlConnection.ClearPool(cnBackup);
                        cmBackup.ExecuteNonQuery();
                        mOperationsMessage = "عمليات ايجاد پشتيبان نرم افزار با موفقيت انجام شد";
                        CloseFlag = false;
                    }
                    catch (Exception objEx)
                    {
                        Logger.SaveError(Name + ".cmdBackup_Click", objEx.Message);
                        mOperationsMessage = "ایجاد پشتیبان نرم افزار با مشکل مواجه شد";
                        CloseFlag = true;
                    }
                    finally
                    {
                        if (cnBackup.State == ConnectionState.Open)
                            cnBackup.Close();
                    }
                }
            }

            if (chkUMBackup.Checked)
            {
                using (var cnUMBackup = new SqlConnection(Strings.Replace(mConnectionString, mDatabaseName, "UM_PSBS")))
                {
                    try
                    {
                        cnUMBackup.Open();
                        var cmBackup = new SqlCommand("Backup DataBase UM_PSBS To Disk='" + UMFilePath + "'", cnUMBackup);
                        SqlConnection.ClearPool(cnUMBackup);
                        cmBackup.ExecuteNonQuery();
                        if (string.IsNullOrEmpty(mOperationsMessage))
                        {
                            mOperationsMessage = "عمليات ايجاد پشتيبان مدیریت کاربران با موفقيت انجام شد";
                        }
                        else
                        {
                            mOperationsMessage += Constants.vbCrLf + Constants.vbCrLf + "عمليات ايجاد پشتيبان مدیریت کاربران با موفقيت انجام شد";
                        }

                        CloseFlag = false;
                    }
                    catch (Exception objEx)
                    {
                        Logger.SaveError(Name + ".cmdBackup_Click", objEx.Message);
                        if (string.IsNullOrEmpty(mOperationsMessage))
                        {
                            mOperationsMessage = "ایجاد پشتیبان مدیریت کاربران با مشکل مواجه شد";
                        }
                        else
                        {
                            mOperationsMessage += Constants.vbCrLf + Constants.vbCrLf + "ایجاد پشتیبان مدیریت کاربران با مشکل مواجه شد";
                        }

                        CloseFlag = true;
                    }
                    finally
                    {
                        if (cnUMBackup.State == ConnectionState.Open)
                            cnUMBackup.Close();
                    }
                }
            }

            Cursor = Cursors.Default;
            MessageBox.Show(mOperationsMessage, mErrors_Messages_Header, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            Close();
        }

        private void cmdRestore_Click(object sender, EventArgs e)
        {
            string mOperationsMessage = Constants.vbNullString;
            string FilePath = Constants.vbNullString;
            string UMFilePath = Constants.vbNullString;
            if (chkBackup.Checked)
            {
                FilePath = FileNameValidation(txtBackupFileName, "نرم افزار");
                if (string.IsNullOrEmpty(FilePath))
                {
                    return;
                }
            }

            if (chkUMBackup.Checked)
            {
                UMFilePath = FileNameValidation(txtUMBackupFileName, "مدیریت کاربران");
                if (string.IsNullOrEmpty(UMFilePath))
                {
                    return;
                }
            }

            Cursor = Cursors.WaitCursor;
            if (chkBackup.Checked)
            {
                using (var cnRestore = new SqlConnection(Strings.Replace(mConnectionString, mDatabaseName, "master")))
                {
                    try
                    {
                        cnRestore.Open();
                        var cmRestore = new SqlCommand("ALTER DATABASE " + mDatabaseName + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE", cnRestore);
                        cmRestore.ExecuteNonQuery();
                        cmRestore.CommandText = "Restore DataBase " + mDatabaseName + " From Disk='" + FilePath + "'";
                        var cn = new SqlConnection(mConnectionString);
                        SqlConnection.ClearPool(cn);
                        cn = null;
                        cmRestore.ExecuteNonQuery();
                        cmRestore.CommandText = "ALTER DATABASE " + mDatabaseName + " SET MULTI_USER";
                        cmRestore.ExecuteNonQuery();
                        mOperationsMessage = "عمليات جايگذاري پشتيبان نرم افزار با موفقيت انجام شد";
                        CloseFlag = false;
                    }
                    catch (Exception objEx)
                    {
                        Logger.SaveError(Name + ".cmdRestore_Click", objEx.Message);
                        mOperationsMessage = "جايگذاري پشتيبان نرم افزار با مشکل مواجه شد";
                        CloseFlag = true;
                    }
                    finally
                    {
                        if (cnRestore.State == ConnectionState.Open)
                            cnRestore.Close();
                    }
                }
            }

            if (chkUMBackup.Checked)
            {
                using (var cnUMRestore = new SqlConnection(Strings.Replace(mConnectionString, mDatabaseName, "master")))
                {
                    try
                    {
                        cnUMRestore.Open();
                        var cmRestore = new SqlCommand("ALTER DATABASE UM_PSBS SET SINGLE_USER WITH ROLLBACK IMMEDIATE", cnUMRestore);
                        cmRestore.ExecuteNonQuery();
                        cmRestore.CommandText = "Restore DataBase UM_PSBS From Disk='" + UMFilePath + "'";
                        var cnUM = new SqlConnection(Strings.Replace(mConnectionString, mDatabaseName, "UM_PSBS"));
                        SqlConnection.ClearPool(cnUM);
                        cnUM = null;
                        cmRestore.ExecuteNonQuery();
                        cmRestore.CommandText = "ALTER DATABASE UM_PSBS SET MULTI_USER";
                        cmRestore.ExecuteNonQuery();
                        if (string.IsNullOrEmpty(mOperationsMessage))
                        {
                            mOperationsMessage = "عمليات جايگذاري پشتيبان مدیریت کاربران با موفقيت انجام شد";
                        }
                        else
                        {
                            mOperationsMessage += Constants.vbCrLf + Constants.vbCrLf + "عمليات جايگذاري پشتيبان مدیریت کاربران با موفقيت انجام شد";
                        }

                        CloseFlag = false;
                    }
                    catch (Exception objEx)
                    {
                        Logger.SaveError(Name + ".cmdRestore_Click", objEx.Message);
                        if (string.IsNullOrEmpty(mOperationsMessage))
                        {
                            mOperationsMessage = "جايگذاري پشتيبان مدیریت کاربران با مشکل مواجه شد";
                        }
                        else
                        {
                            mOperationsMessage += Constants.vbCrLf + Constants.vbCrLf + "جايگذاري پشتيبان مدیریت کاربران با مشکل مواجه شد";
                        }

                        CloseFlag = true;
                    }
                    finally
                    {
                        if (cnUMRestore.State == ConnectionState.Open)
                            cnUMRestore.Close();
                    }
                }
            }

            Cursor = Cursors.Default;
            MessageBox.Show(mOperationsMessage, mErrors_Messages_Header, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            Close();
        }

        private string FileNameValidation(TextBox AddressBox, string mDBTitle)
        {
            string FilePath = AddressBox.Text;
            if (mFormMode == BackupFormModeEnum.BFM_RESTORE_BACKUP && !File.Exists(FilePath))
            {
                MessageBox.Show("فايل پشتیبان " + mDBTitle + " موجود نیست", mErrors_Messages_Header, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                AddressBox.Focus();
                return Constants.vbNullString;
            }

            if (string.IsNullOrEmpty(FilePath))
            {
                MessageBox.Show("نام و مسير فايل پشتیبان " + mDBTitle + " وارد كنيد", mErrors_Messages_Header, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                AddressBox.Focus();
                return Constants.vbNullString;
            }

            if (FilePath.Length < 4 && FilePath.Contains(".") || FilePath.Length == 4 && FilePath.Contains(".") && FilePath != ".bak")
            {
                MessageBox.Show("پسوند فايل پشتیبان " + mDBTitle + " اشتباه وارد شده است", mErrors_Messages_Header, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                AddressBox.Focus();
                return Constants.vbNullString;
            }

            if (FilePath.Length > 4 && (FilePath.Substring(FilePath.Length - 5).ToLower() == @"\.bak" || FilePath.Substring(FilePath.Length - 5).ToLower() == "/.bak"))
            {
                MessageBox.Show("نام فايل پشتیبان " + mDBTitle + " وارد كنيد", mErrors_Messages_Header, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                AddressBox.Focus();
                return Constants.vbNullString;
            }

            if (!FilePath.Contains("."))
            {
                FilePath = FilePath + ".bak";
            }

            if (FilePath.Contains(".") && FilePath.Substring(FilePath.Length - 4).ToLower() != ".bak")
            {
                MessageBox.Show("پسوند فايل پشتیبان " + mDBTitle + " اشتباه وارد شده است", mErrors_Messages_Header, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                AddressBox.Focus();
                return Constants.vbNullString;
            }

            if (!FilePath.Contains(":"))
            {
                if (Strings.Right(Application.StartupPath, 1) == @"\")
                {
                    FilePath = Application.StartupPath + FilePath;
                }
                else
                {
                    FilePath = Application.StartupPath + @"\" + FilePath;
                }
            }

            if (!FilePath.Contains(@":\") || FilePath.Contains(@"\\"))
            {
                MessageBox.Show("مسير فايل پشتیبان " + mDBTitle + " اشتباه وارد شده است", mErrors_Messages_Header, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                AddressBox.Focus();
                return Constants.vbNullString;
            }

            return FilePath;
        }

        private void txtBackupFileName_KeyPress(object sender, KeyPressEventArgs e)
        {
            string InvalidChars = "/?|{}[],<>;'~`!@#$%^&*+=";
            if (InvalidChars.Contains(Conversions.ToString(e.KeyChar)))
            {
                e.KeyChar = Conversions.ToChar(Constants.vbNullString);
            }
        }

        private void chkUMBackup_CheckedChanged(object sender, EventArgs e)
        {
            txtUMBackupFileName.Enabled = chkUMBackup.Checked;
            lblUMBackupFileName.Enabled = chkUMBackup.Checked;
            cmdUMBrowser.Enabled = chkUMBackup.Checked;
            txtUMBackupFileName.Focus();
            if (!chkUMBackup.Checked && !chkBackup.Checked)
            {
                cmdBackup.Enabled = false;
                cmdRestore.Enabled = false;
            }
            else
            {
                cmdBackup.Enabled = true;
                cmdRestore.Enabled = true;
            }
        }

        private void chkBackup_CheckedChanged(object sender, EventArgs e)
        {
            txtBackupFileName.Enabled = chkBackup.Checked;
            lblBackupFileName.Enabled = chkBackup.Checked;
            cmdBrowser.Enabled = chkBackup.Checked;
            txtBackupFileName.Focus();
            if (!chkBackup.Checked && !chkUMBackup.Checked)
            {
                cmdBackup.Enabled = false;
                cmdRestore.Enabled = false;
            }
            else
            {
                cmdBackup.Enabled = true;
                cmdRestore.Enabled = true;
            }
        }

        private string GetServerShamsiTime()
        {
            string mServerDate = GetServerMiladyTime();
            var pclServerDate = new PersianCalendar();
            return pclServerDate.GetYear(Conversions.ToDate(mServerDate)).ToString() + pclServerDate.GetMonth(Conversions.ToDate(mServerDate)) + pclServerDate.GetDayOfMonth(Conversions.ToDate(mServerDate));
        }

        private string GetServerMiladyTime()
        {
            string mServerTime = Constants.vbNullString;
            using (var cnn = new SqlConnection(mConnectionString))
            {
                cnn.Open();
                var cmd = new SqlCommand("Select (Convert(char(8),GetDate(),11)) As ServerDate", cnn);
                var rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    mServerTime = rd["ServerDate"].ToString();
                    mServerTime = DateTime.Parse(mServerTime).ToString().Substring(0, 10);
                }

                rd.Close();
                cnn.Close();
            }

            return mServerTime;
        }
    }
}