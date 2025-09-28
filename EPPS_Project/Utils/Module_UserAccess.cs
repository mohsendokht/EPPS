using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System.Text.Json;

namespace ProductionPlanning
{
    static class Module_UserAccess
    {
        public static string UserLimitStr;
        public static string MyIPLock;
        public static string MySoftwareCode = "102";
        public static string UserCodeTmp = "3";
        public static string UserPassTmp = "1";
        private const int MySoftwareID = 1539073;
        private const int CellUserNumber = 43; // 42
        public static PSB_Data1.Superpro Ltmp;
        // Public Ltmp As PSB_Data1.Superpro
        //private static string MyQueryTable;
        private static byte[,][] QueryResponses;
        public static string arg_usercode;
        public static string arg_userpass;
        public static int LoginFlag;
        public static string[] UN = new string[3];
        // Friend Const mSoftwareCode As String = "102"

        public static void ParseCommandLineArgs()
        {
            if (Environment.GetCommandLineArgs().Length > 1)
            {
                LoginFlag = 1;
                arg_usercode = Environment.GetCommandLineArgs()[1];
                arg_userpass = Environment.GetCommandLineArgs()[2];
            }
            else
            {
                LoginFlag = 2;
            }
        }

        public static bool Do_Login(string Code, string Pass)
        {
            string DataBasePassword;
            UN[0] = Code;
            UN[1] = Pass;
            if (!string.IsNullOrEmpty(Code) && !string.IsNullOrEmpty(Pass))
            {
                // خواندن رمز مربوط به کد کاربری وارد شده از بانک اطلاعاتی
                var cnAuthenticate = new SqlConnection(Module1.UMCnnStr);
                var cmPassword = new SqlCommand("Select UserPass,UserName From UserProperty Where UserCode=" + Code, cnAuthenticate);
                cnAuthenticate.Open();
                var drdPassword = cmPassword.ExecuteReader();
                if (!drdPassword.HasRows)
                {
                    cnAuthenticate.Close();
                    cmPassword.Dispose();
                    drdPassword = null;
                    return false;
                }
                else
                {
                    drdPassword.Read();
                    DataBasePassword = drdPassword.GetString(0);
                    UN[2] = drdPassword.GetString(1);
                }

                cnAuthenticate.Close();
                cmPassword.Dispose();
                drdPassword = null;
                var cmdAuthenticate = new SqlCommand("Select Count(*) From SoftwareUsers Where SoftWareCode=" + MySoftwareCode + " And UserCode=" + Code, cnAuthenticate);
                cnAuthenticate.Open();
                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(cmdAuthenticate.ExecuteScalar(), 0, false)))
                {
                    cnAuthenticate.Close();
                    cmdAuthenticate.Dispose();
                    // TODO: skip pass check
                    // ======================================================
                    if (UN[1] == Encrypt_MyPass(DataBasePassword))
                    {

                        //If UN(1) = DataBasePassword Then
                        //DialogResult = Windows.Forms.DialogResult.OK
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("رمز عبور صحیح نیست.", "EPPS ", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        return false;
                    }
                }
                // ======================================================
                else
                {
                    MessageBox.Show("شما مجاز به استفاده از برنامه  نمی باشید.", "EPPS ", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static void Set_AccessToMenuItems(string MyUserCode)
        {
            Module1.MainFormObject.MenuItem1.Visible = HaveAccessToItem(1); // تعاریف اولیه
            if (HaveAccessToItem(1))
            {
                Module1.MainFormObject.MenuItem1_1.Visible = HaveAccessToItem(2); // عناوین پیش فرض فعالیتها
                Module1.MainFormObject.MenuItem1_3.Visible = HaveAccessToItem(4); // تعریف واحد های سنجش
                if (HaveAccessToItem(4))
                {
                    Module1.MainFormObject.MenuItem1_3_1.Visible = HaveAccessToItem(5); // مشخصات واحدهای سنجش
                    Module1.MainFormObject.MenuItem1_3_2.Visible = HaveAccessToItem(6); // مشخصات ارتباط بین واحدها
                }

                Module1.MainFormObject.MenuItem1_4.Visible = HaveAccessToItem(7); // مشخصات مواد اولیه
                Module1.MainFormObject.MenuItem1_5.Visible = HaveAccessToItem(8); // مشخصات ماشین آلات
                Module1.MainFormObject.MenuItem1_6.Visible = HaveAccessToItem(9); // مشخصات پیمانکاران
                Module1.MainFormObject.MenuItem1_7.Visible = HaveAccessToItem(10); // مشخصات انبارها
                Module1.MainFormObject.MenuItem1_8.Visible = HaveAccessToItem(11); // مشخصات علت توقف عملیات
                Module1.MainFormObject.MenuItem1_2.Visible = HaveAccessToItem(96); // مشخصات تامین کنندگان
                Module1.MainFormObject.MenuItem1_9.Visible = HaveAccessToItem(12); // تعریف تقویم کاری
                Module1.MainFormObject.ToolStripMenuItem2.Visible = HaveAccessToItem(12);
                if (HaveAccessToItem(12))
                {
                    Module1.MainFormObject.MenuItem1_9_1.Visible = HaveAccessToItem(13); // مشخصات روزهای تعطیل رسمی در تقویم سال
                    Module1.MainFormObject.MenuItem1_9_2.Visible = HaveAccessToItem(14); // مشخصات کلی تقویم کاری
                }

                Module1.MainFormObject.MenuItem1_10.Visible = HaveAccessToItem(3); // مشخصات بخش های تولید
                Module1.MainFormObject.ToolStripMenuItem7.Visible = HaveAccessToItem(3);
                Module1.MainFormObject.MenuItem1_11.Visible = HaveAccessToItem(16); // مشخصات اپراتورهای تولید
                Module1.MainFormObject.MenuItem1_12.Visible = HaveAccessToItem(17); // ضرایب انجام عملیات
                Module1.MainFormObject.ToolStripMenuItem3.Visible = HaveAccessToItem(17);
                Module1.MainFormObject.MenuItem1_13.Visible = HaveAccessToItem(82); // لیست مشتریان
                Module1.MainFormObject.ToolStripMenuItem4.Visible = HaveAccessToItem(82);
                Module1.MainFormObject.MenuItem1_14.Visible = HaveAccessToItem(76); // تعیین حدود دسترسی کاربران
                Module1.MainFormObject.ToolStripMenuItem6.Visible = HaveAccessToItem(76);
                Module1.MainFormObject.MenuItem1_15.Visible = HaveAccessToItem(99); // ليست علت هاي در دسترس نبودن ماشين
            }

            Module1.MainFormObject.MenuItem2.Visible = HaveAccessToItem(18); // تعاریف محصول
            if (HaveAccessToItem(18))
            {
                Module1.MainFormObject.MenuItem2_1.Visible = HaveAccessToItem(19); // مشخصات محصول
                Module1.MainFormObject.MenuItem2_2.Visible = HaveAccessToItem(23); // مشخصات درخت محصول 
            }

            Module1.MainFormObject.MenuItem22.Visible = HaveAccessToItem(78); // سفارشات
            if (HaveAccessToItem(78))
            {
                Module1.MainFormObject.MenuItem22_1.Visible = HaveAccessToItem(79); // لیست سفارشات مشتریان
            }

            Module1.MainFormObject.MenuItem3.Visible = HaveAccessToItem(43); // برنامه ریزی
            if (HaveAccessToItem(43))
            {
                Module1.MainFormObject.MenuItem3_1.Visible = HaveAccessToItem(44); // برنامه ریزی سالیانه
                Module1.MainFormObject.MenuItem3_2.Visible = HaveAccessToItem(50); // مشخصات بچ تولید
                Module1.MainFormObject.ToolStripMenuItem9.Visible = HaveAccessToItem(50);
                Module1.MainFormObject.MenuItem3_3.Visible = HaveAccessToItem(55); // مشخصات زمان های در دسترس نبودن ماشین آلات
                Module1.MainFormObject.ToolStripMenuItem1.Visible = HaveAccessToItem(55);
                Module1.MainFormObject.MenuItem3_4.Visible = HaveAccessToItem(59); // برنامه ریزی تولید
                Module1.MainFormObject.MenuItem3_5.Visible = HaveAccessToItem(64); // نمایش برنامه ریزی انجام شده
                Module1.MainFormObject.MenuItem3_7.Visible = HaveAccessToItem(65); // نمایش پیشرفت تولید بچ ها
                Module1.MainFormObject.ToolStripMenuItem5.Visible = HaveAccessToItem(65);
            }

            Module1.MainFormObject.MenuItem4.Visible = HaveAccessToItem(66); // d  منوی اطلاعات تولید
            if (HaveAccessToItem(66))
            {
                Module1.MainFormObject.MenuItem4_1.Visible = HaveAccessToItem(80); // d  اطلاعات تولید
            }

            Module1.MainFormObject.MenuItem5.Visible = HaveAccessToItem(72); // d  گزارشات
            if (HaveAccessToItem(72))
            {
                Module1.MainFormObject.MenuItem5_1.Visible = HaveAccessToItem(91); // d  گزارشات چاپی
                Module1.MainFormObject.MenuItem5_2.Visible = HaveAccessToItem(92); // d  گزارشات محاسباتی
                Module1.MainFormObject.ToolStripMenuItem11.Visible = HaveAccessToItem(92);
                if (HaveAccessToItem(92))
                {
                    Module1.MainFormObject.MenuItem5_2_1.Visible = HaveAccessToItem(81); // d  OEE محاسبۀ
                    Module1.MainFormObject.MenuItem5_2_2.Visible = HaveAccessToItem(86); // d  محاسبۀ زمانهای کارکرد و خالی ماشین
                    Module1.MainFormObject.ToolStripSeparator1.Visible = HaveAccessToItem(86);
                    Module1.MainFormObject.MenuItem5_2_3.Visible = HaveAccessToItem(93); // d  محاسبه پرسنل مورد نیاز بخش های تولید
                    Module1.MainFormObject.ToolStripMenuItem13.Visible = HaveAccessToItem(93);
                    Module1.MainFormObject.MenuItem5_2_4.Visible = HaveAccessToItem(94); // d  گزارش لیست عقب افتادگی بچ های تولید نسبت به برنامه ریزی
                    Module1.MainFormObject.ToolStripMenuItem14.Visible = HaveAccessToItem(94);
                    Module1.MainFormObject.MenuItem5_2_5.Visible = HaveAccessToItem(98); // d  گزارش عملیات تولیدی ماشین در بازۀ زمانی
                    Module1.MainFormObject.ToolStripMenuItem15.Visible = HaveAccessToItem(98);
                }
            }

            Module1.MainFormObject.mnuTools.Visible = HaveAccessToItem(87); // منوی امکانات
            if (HaveAccessToItem(87))
            {
                Module1.MainFormObject.mnuTools_Backup.Visible = HaveAccessToItem(88); // زیر منوی پشتیبان
                if (HaveAccessToItem(88))
                {
                    Module1.MainFormObject.mnuTools_Backup_CreateBackup.Visible = HaveAccessToItem(89); // زیر منوی ایجاد پشتیبان
                    Module1.MainFormObject.mnuTools_Backup_RestoreBackup.Visible = HaveAccessToItem(90); // زیر منوی جایگذاری پشتیبان
                }

                Module1.MainFormObject.mnuTools_SetBackground.Visible = HaveAccessToItem(97); // d  تغییر پس زمینه نرم افزار
                Module1.MainFormObject.ToolStripMenuItem8.Visible = HaveAccessToItem(97);
                Module1.MainFormObject.mnuTools_Helps.Visible = HaveAccessToItem(75); // d  راهنما
                Module1.MainFormObject.ToolStripMenuItem10.Visible = HaveAccessToItem(75);
                if (HaveAccessToItem(75))
                {
                    Module1.MainFormObject.mnuTools_Helps_UsersGuide.Visible = HaveAccessToItem(94); // d  راهنمای کاربران
                    Module1.MainFormObject.mnuTools_Helps_AboutSoftware.Visible = HaveAccessToItem(95); // d  دربازه نرم افزار
                }
            }
        }

        public static bool HaveAccessToItem(int ItemCode)
        {
            string temptext;
            if (UserLimitStr is null)
            {
                UserLimitStr = "";
            }

            if (UserLimitStr.Length < ItemCode * 2)
            {
                return false;
            }

            temptext = Strings.Mid(UserLimitStr, (ItemCode - 1) * 2 + 2, 1);
            if (temptext == "0" | string.IsNullOrEmpty(temptext))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static object CreateAccessString_To_Items(System.Windows.Forms.TreeView MyItemsTree)
        {
            object CreateAccessString_To_ItemsRet = default;
            string MyTempStr;
            int MyTempLen;
            int MyTempPos;
            // Dim MyNodesCount As Integer
            int i;
            int MaxSoftwareItemCode = 0;
            try
            {
                // MyNodesCount = MyItemsTree.GetNodeCount(True)
                // '''''''''''''''''''''''''''''''''''''''''''''
                var objcommand = new SqlCommand();
                var MyCnn = new SqlConnection();
                MyCnn.ConnectionString = Module1.UMCnnStr;
                objcommand.Connection = MyCnn;
                objcommand.CommandText = "SELECT  MAX(ItemCode) AS EXPR1  FROM   SoftwareItems   WHERE    SoftwareCode =" + MySoftwareCode + "";
                MyCnn.Open();
                MaxSoftwareItemCode = Conversions.ToInteger(objcommand.ExecuteScalar().ToString());
                MyCnn.Close();
                // '''''''''''''''''''''''''''''''''''''''''''''
                MyTempStr = Strings.StrDup(MaxSoftwareItemCode * 2, "0");
                MyTempLen = MyTempStr.Length;
                var loopTo = MyItemsTree.Nodes.Count - 1;
                for (i = 0; i <= loopTo; i++)
                {
                    VBMath.Randomize();
                    MyTempPos = (int)Math.Round(Conversion.Val(MyItemsTree.Nodes[i].Tag));
                    if (!MyItemsTree.Nodes[i].Checked)
                    {
                        MyTempStr = Strings.Left(MyTempStr, MyTempPos * 2 - 2) + (char)(Conversion.Int(VBMath.Rnd() * 200f % 64f) + 20f) + "0" + Strings.Right(MyTempStr, MyTempLen - MyTempPos * 2);
                    }
                    else if (MyItemsTree.Nodes[i].Checked)
                    {
                        MyTempStr = Strings.Left(MyTempStr, MyTempPos * 2 - 2) + (char)(Conversion.Int(VBMath.Rnd() * 200f % 64f) + 20f) + (Conversion.Int(VBMath.Rnd() * 100f) % 8f + 1f).ToString() + Strings.Right(MyTempStr, MyTempLen - MyTempPos * 2);
                    }

                    Update_AccessString_ForChilds(ref MyTempStr, MyItemsTree.Nodes[i]);
                }

                CreateAccessString_To_ItemsRet = Strings.Mid(MyTempStr, 1, 1024);
            }
            catch (Exception ex)
            {
                Logger.SaveError("ProductionPlanning.CreateAccessString_To_Items", ex.Message);
                MessageBox.Show("خطا در تعیین سطح دسترسی کاربر به منو برنامه");
                CreateAccessString_To_ItemsRet = "00";
            }

            return CreateAccessString_To_ItemsRet;
        }

        private static void Update_AccessString_ForChilds(ref string MyTmpStr, TreeNode MyNode)
        {
            int i;
            int MyTempPos;
            var loopTo = MyNode.Nodes.Count - 1;
            for (i = 0; i <= loopTo; i++)
            {
                VBMath.Randomize();
                MyTempPos = (int)Math.Round(Conversion.Val(MyNode.Nodes[i].Tag));
                if (!MyNode.Nodes[i].Checked)
                {
                    MyTmpStr = Strings.Left(MyTmpStr, MyTempPos * 2 - 2) + (char)(Conversion.Int(VBMath.Rnd() * 200f % 64f) + 20f) + "0" + Strings.Right(MyTmpStr, MyTmpStr.Length - MyTempPos * 2);
                }
                else if (MyNode.Nodes[i].Checked)
                {
                    MyTmpStr = Strings.Left(MyTmpStr, MyTempPos * 2 - 2) + (char)(Conversion.Int(VBMath.Rnd() * 200f % 64f) + 20f) + (Conversion.Int(VBMath.Rnd() * 100f) % 8f + 1f).ToString() + Strings.Right(MyTmpStr, MyTmpStr.Length - MyTempPos * 2);
                }

                Update_AccessString_ForChilds(ref MyTmpStr, MyNode.Nodes[i]);
            }
        }

        private static void InitQueryTable()
        {
            // EPPS  - cell No. : 38
            var QueryResponses_Tmp = new byte[,][] { { new byte[] { 0x8E, 0x70, 0x53, 0x85 }, new byte[] { 0x55, 0x5C, 0xE9, 0xAB } }, { new byte[] { 0x15, 0xDF, 0x3E, 0x5A }, new byte[] { 0x4D, 0xBC, 0x5E, 0x2C } }, { new byte[] { 0xE7, 0xBA, 0x6B, 0x8F }, new byte[] { 0xBF, 0x8C, 0xE1, 0x63 } }, { new byte[] { 0x6A, 0x34, 0x38, 0xB7 }, new byte[] { 0xC5, 0x5B, 0x19, 0xC0 } }, { new byte[] { 0x88, 0x59, 0x8B, 0xAF }, new byte[] { 0x51, 0x72, 0x6E, 0x53 } }, { new byte[] { 0x7, 0xE7, 0x1, 0x37 }, new byte[] { 0x41, 0xBA, 0x9, 0x97 } }, { new byte[] { 0xF8, 0xBC, 0xC6, 0x54 }, new byte[] { 0xF4, 0xEF, 0x62, 0xA5 } }, { new byte[] { 0x97, 0x34, 0xA0, 0x49 }, new byte[] { 0xAC, 0x63, 0xF, 0x28 } }, { new byte[] { 0x74, 0xB1, 0x2F, 0xC3 }, new byte[] { 0xEE, 0x70, 0x7F, 0x3 } }, { new byte[] { 0xD0, 0x7B, 0x73, 0x54 }, new byte[] { 0x5F, 0x72, 0xB2, 0x47 } }, { new byte[] { 0x4A, 0xCB, 0x8E, 0x22 }, new byte[] { 0xC3, 0x62, 0x86, 0xD3 } }, { new byte[] { 0xBB, 0x22, 0xCB, 0xE1 }, new byte[] { 0x8, 0xD7, 0x6E, 0x54 } }, { new byte[] { 0x5E, 0xDF, 0xE0, 0xFE }, new byte[] { 0x91, 0xB2, 0xF9, 0xD4 } }, { new byte[] { 0x34, 0x11, 0x70, 0x1D }, new byte[] { 0x87, 0xE2, 0x87, 0x3A } }, { new byte[] { 0xA6, 0x8A, 0xD4, 0xC6 }, new byte[] { 0x92, 0x69, 0x2A, 0x54 } }, { new byte[] { 0x6D, 0x35, 0x1B, 0x5B }, new byte[] { 0x37, 0xF4, 0xC6, 0xE7 } }, { new byte[] { 0xAF, 0xA9, 0x4F, 0x4E }, new byte[] { 0x1C, 0xFF, 0x15, 0x10 } }, { new byte[] { 0x6A, 0xFD, 0xFA, 0x90 }, new byte[] { 0xE2, 0xA8, 0x89, 0x5E } }, { new byte[] { 0x16, 0xDE, 0xE9, 0x4D }, new byte[] { 0x31, 0xCF, 0x27, 0x5B } }, { new byte[] { 0x86, 0xE0, 0x30, 0xD6 }, new byte[] { 0xFC, 0x73, 0xD1, 0xAE } }, { new byte[] { 0x10, 0x12, 0x6F, 0xDF }, new byte[] { 0xC6, 0xB3, 0x10, 0x6 } }, { new byte[] { 0xED, 0xD5, 0x53, 0xEA }, new byte[] { 0x4B, 0xAF, 0x7, 0x8F } }, { new byte[] { 0xE2, 0xEE, 0x5F, 0x3 }, new byte[] { 0x87, 0x65, 0xDA, 0xF9 } }, { new byte[] { 0x20, 0xD9, 0xEC, 0xAE }, new byte[] { 0x73, 0x79, 0x1B, 0xA } }, { new byte[] { 0x6A, 0x60, 0x6E, 0x1E }, new byte[] { 0xAB, 0x67, 0x2E, 0xB } }, { new byte[] { 0x75, 0x6D, 0xFA, 0xA7 }, new byte[] { 0x64, 0xA4, 0x38, 0x3 } }, { new byte[] { 0x94, 0x1D, 0x5, 0x76 }, new byte[] { 0xC, 0x88, 0xD, 0xB7 } }, { new byte[] { 0x95, 0x15, 0x6F, 0x80 }, new byte[] { 0x73, 0xBC, 0x41, 0x49 } }, { new byte[] { 0xE5, 0x19, 0xC0, 0xB8 }, new byte[] { 0xD5, 0x64, 0xCF, 0xFD } }, { new byte[] { 0xFB, 0xDB, 0xB1, 0x85 }, new byte[] { 0xB5, 0xCD, 0xA5, 0x3B } }, { new byte[] { 0xF4, 0x10, 0xEE, 0x73 }, new byte[] { 0x93, 0xBF, 0x44, 0xAD } }, { new byte[] { 0x7B, 0xC9, 0x1A, 0x29 }, new byte[] { 0x65, 0x30, 0xBC, 0xDB } }, { new byte[] { 0xEC, 0x1, 0x13, 0x9B }, new byte[] { 0x21, 0xDB, 0x46, 0xAF } }, { new byte[] { 0xB9, 0x72, 0x78, 0x80 }, new byte[] { 0x2F, 0x1D, 0xA6, 0xA7 } }, { new byte[] { 0xC, 0xAE, 0x69, 0x7 }, new byte[] { 0x28, 0xF9, 0xAA, 0x7 } }, { new byte[] { 0xAE, 0x6A, 0x8E, 0xC5 }, new byte[] { 0x39, 0x22, 0x4E, 0xB5 } }, { new byte[] { 0x27, 0x1D, 0x5B, 0xF2 }, new byte[] { 0xA9, 0x61, 0x5, 0x48 } }, { new byte[] { 0x27, 0xCA, 0x92, 0xD6 }, new byte[] { 0x4E, 0x33, 0xC6, 0x44 } }, { new byte[] { 0x24, 0x19, 0x7E, 0x45 }, new byte[] { 0x2E, 0xD3, 0xC8, 0x53 } }, { new byte[] { 0xAD, 0xAD, 0xB3, 0x7F }, new byte[] { 0xB6, 0x6, 0x80, 0x9E } }, { new byte[] { 0xB2, 0xC7, 0x2D, 0xFE }, new byte[] { 0x56, 0xD0, 0x9E, 0xBA } }, { new byte[] { 0xB5, 0x7E, 0x3, 0xC6 }, new byte[] { 0x80, 0xA2, 0x72, 0xDF } }, { new byte[] { 0xB9, 0x9D, 0x66, 0x9A }, new byte[] { 0x66, 0x3D, 0x13, 0x62 } }, { new byte[] { 0x87, 0x97, 0x8F, 0x1C }, new byte[] { 0x3A, 0x2A, 0x2B, 0x9 } }, { new byte[] { 0x45, 0xC9, 0xF6, 0x37 }, new byte[] { 0xB7, 0xE2, 0xB6, 0x5E } }, { new byte[] { 0x4A, 0xFE, 0xC5, 0xBA }, new byte[] { 0xE7, 0x59, 0x25, 0x84 } }, { new byte[] { 0x31, 0x38, 0x8C, 0x45 }, new byte[] { 0x4D, 0x19, 0x28, 0x89 } }, { new byte[] { 0x2C, 0xAE, 0x36, 0x69 }, new byte[] { 0xF5, 0x4B, 0xF2, 0x4 } }, { new byte[] { 0x9B, 0x11, 0x3C, 0xB }, new byte[] { 0xDE, 0xE1, 0x74, 0xEB } }, { new byte[] { 0xDE, 0x13, 0x18, 0x67 }, new byte[] { 0x3A, 0xD, 0xE6, 0x2 } }, { new byte[] { 0x2A, 0xFD, 0x21, 0x13 }, new byte[] { 0x7A, 0x11, 0xFC, 0x25 } }, { new byte[] { 0x91, 0xC5, 0xE, 0xBA }, new byte[] { 0xFD, 0xCE, 0xE8, 0x62 } }, { new byte[] { 0x91, 0x2B, 0xF3, 0x5 }, new byte[] { 0xC9, 0xC4, 0x64, 0x13 } }, { new byte[] { 0xFE, 0x3A, 0xFD, 0x7F }, new byte[] { 0xF0, 0x4D, 0x6A, 0x9 } }, { new byte[] { 0x4, 0x5, 0x44, 0xEF }, new byte[] { 0xF2, 0xE, 0x35, 0x7 } }, { new byte[] { 0x24, 0x99, 0xF3, 0xE6 }, new byte[] { 0xDD, 0x8, 0xE7, 0x1 } }, { new byte[] { 0x79, 0x30, 0xA9, 0x94 }, new byte[] { 0x7A, 0x20, 0xBA, 0xFC } }, { new byte[] { 0x40, 0xA7, 0x1F, 0xDF }, new byte[] { 0x61, 0x71, 0xD9, 0x14 } }, { new byte[] { 0x97, 0x32, 0xA, 0xB7 }, new byte[] { 0xDB, 0x95, 0x27, 0x36 } }, { new byte[] { 0x85, 0x4F, 0x43, 0xA3 }, new byte[] { 0x77, 0x6F, 0x2, 0x1 } }, { new byte[] { 0x3B, 0xF8, 0x25, 0xA0 }, new byte[] { 0x8F, 0x6F, 0x15, 0xC1 } }, { new byte[] { 0x99, 0x1D, 0x36, 0x2C }, new byte[] { 0x37, 0x77, 0xCB, 0x80 } }, { new byte[] { 0xF4, 0x51, 0xA, 0x9E }, new byte[] { 0x96, 0x87, 0xF6, 0x8B } }, { new byte[] { 0x17, 0xC4, 0x65, 0xB8 }, new byte[] { 0xC0, 0xBC, 0x48, 0xE4 } }, { new byte[] { 0x87, 0x71, 0xA0, 0x7F }, new byte[] { 0xCA, 0x6F, 0x43, 0xD0 } }, { new byte[] { 0xC, 0x98, 0x4E, 0x4A }, new byte[] { 0x6F, 0x98, 0xFF, 0xD0 } }, { new byte[] { 0x6C, 0x6F, 0x1F, 0x1A }, new byte[] { 0x2D, 0x85, 0xC, 0x55 } }, { new byte[] { 0x7A, 0x15, 0x4, 0x29 }, new byte[] { 0x42, 0xD4, 0x58, 0x4E } }, { new byte[] { 0x4F, 0xC7, 0x93, 0xC6 }, new byte[] { 0xEE, 0x39, 0xF3, 0x56 } }, { new byte[] { 0xD8, 0x56, 0xAE, 0x5F }, new byte[] { 0x31, 0x51, 0xD8, 0x7 } }, { new byte[] { 0x91, 0xD8, 0x5F, 0xE0 }, new byte[] { 0x86, 0x54, 0x16, 0x1A } }, { new byte[] { 0x8F, 0x9E, 0x7, 0x3C }, new byte[] { 0x4C, 0xEC, 0xCE, 0x8F } }, { new byte[] { 0xC4, 0x67, 0xB8, 0x48 }, new byte[] { 0x2D, 0xE3, 0xC5, 0x78 } }, { new byte[] { 0x7F, 0xD3, 0xDF, 0xD0 }, new byte[] { 0x5D, 0xC6, 0xC, 0x7 } }, { new byte[] { 0x32, 0x1B, 0x26, 0xE5 }, new byte[] { 0x87, 0x78, 0x8D, 0xC5 } }, { new byte[] { 0x78, 0xFE, 0x98, 0x74 }, new byte[] { 0x89, 0xAF, 0xA8, 0x20 } }, { new byte[] { 0x56, 0xFF, 0x6, 0x1D }, new byte[] { 0xA9, 0xE9, 0xE9, 0xB6 } }, { new byte[] { 0xC1, 0xCD, 0xAA, 0x41 }, new byte[] { 0x7F, 0xDF, 0xD3, 0x33 } }, { new byte[] { 0x61, 0x3, 0xB, 0x5D }, new byte[] { 0x5C, 0x3F, 0x90, 0x70 } }, { new byte[] { 0x95, 0x11, 0x20, 0x97 }, new byte[] { 0xCC, 0x44, 0x45, 0x41 } }, { new byte[] { 0xB7, 0x7A, 0xB8, 0x97 }, new byte[] { 0x7C, 0xAC, 0xE8, 0x8C } }, { new byte[] { 0xA1, 0x41, 0x1A, 0x98 }, new byte[] { 0x4B, 0xCC, 0x0, 0x1A } }, { new byte[] { 0x70, 0x9D, 0xE8, 0xBF }, new byte[] { 0xEB, 0x18, 0x62, 0x92 } }, { new byte[] { 0x88, 0xF4, 0x49, 0xAB }, new byte[] { 0x5E, 0x55, 0xA7, 0xF0 } }, { new byte[] { 0xD8, 0x7, 0x46, 0x4D }, new byte[] { 0xB8, 0x7C, 0x8F, 0x15 } }, { new byte[] { 0x5F, 0x6B, 0x75, 0xFA }, new byte[] { 0x7C, 0x9D, 0xD6, 0xE3 } }, { new byte[] { 0xEE, 0x38, 0xD5, 0xBE }, new byte[] { 0xF7, 0x70, 0x2D, 0xC1 } }, { new byte[] { 0x30, 0x5, 0xFA, 0xF4 }, new byte[] { 0xF4, 0x66, 0x29, 0x27 } }, { new byte[] { 0xE9, 0x13, 0x6A, 0x13 }, new byte[] { 0x3E, 0xEE, 0xF4, 0x52 } }, { new byte[] { 0x7B, 0xC8, 0x46, 0xCB }, new byte[] { 0x9B, 0x21, 0xC7, 0xC2 } }, { new byte[] { 0xAE, 0x5F, 0x2D, 0x51 }, new byte[] { 0xE1, 0x59, 0x97, 0xE } }, { new byte[] { 0xB0, 0xDF, 0x5D, 0xF7 }, new byte[] { 0xAA, 0xAC, 0x64, 0x34 } }, { new byte[] { 0x5B, 0x49, 0x1B, 0xFF }, new byte[] { 0xAC, 0x7, 0x32, 0xCE } }, { new byte[] { 0xB7, 0x16, 0x56, 0xB1 }, new byte[] { 0x20, 0x31, 0x2F, 0xE3 } }, { new byte[] { 0xC0, 0xE0, 0x87, 0xAB }, new byte[] { 0x88, 0x90, 0xF9, 0xCB } }, { new byte[] { 0x69, 0x60, 0xDB, 0x79 }, new byte[] { 0x31, 0xE9, 0x1E, 0x6A } }, { new byte[] { 0xE0, 0x98, 0x94, 0x65 }, new byte[] { 0x39, 0xC9, 0x9B, 0xC2 } }, { new byte[] { 0x14, 0x51, 0xAC, 0x90 }, new byte[] { 0x11, 0x25, 0xF8, 0x27 } }, { new byte[] { 0x75, 0xC8, 0xBC, 0x42 }, new byte[] { 0xB5, 0xE4, 0x37, 0xB2 } }, { new byte[] { 0xFA, 0xA5, 0x1D, 0x7F }, new byte[] { 0xA6, 0xB3, 0x21, 0x93 } } };
            QueryResponses = QueryResponses_Tmp;
        }

        public static bool Find_Lock()
        {
            return true;
            //int MyRslt = 0;
            //try
            //{
            //    MyRslt = Ltmp.GetNew_MyLicenceLock(0, MyIPLock);
            //}
            //catch (Exception ex)
            //{
            //    Logger.LogException("Find_Lock", ex);
            //}

            //if (MyRslt == 2299)
            //{
            //    InitQueryTable();
            //    return true;
            //}
            //else
            //{
            //    MessageBox.Show("قفل سخت افزاری یافت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            //    return false;
            //}
        }

        public static string Encrypt_MyPass(string MyInputPassword)
        {
            string cc;
            string MySd;       

            // کنترل وجود قفل نرم افزار  
            if (!Find_Lock())
            {
                return "";
            }

            // get seed from lock
            MySd = zzGetMySeed(); //Ltmp.GetMySeed(1);
            Ltmp.Free_MyLicenceLock();
            int J = Strings.Len(MySd);
            for (int I = 1, loopTo = Strings.Len(MyInputPassword); I <= loopTo; I++)
            {
                cc = Strings.Asc(Strings.Mid(MySd, I % J - J * Conversions.ToInteger(I % J == 0), 1)).ToString();
                var midTmp = Conversions.ToString((char)(Strings.Asc(Strings.Mid(MyInputPassword, I, 1)) ^ Conversions.ToLong(cc)));
                StringType.MidStmtStr(ref MyInputPassword, I, 1, midTmp);
            }

            return MyInputPassword;
        }

        private static string zzGetMySeed()
        {
            // Read seed from EEPS.cfg in the application's execution directory and parse JSON
            string configFile = ".EPPS_Config\\EPPS.cfg";
            string executionPath = System.AppDomain.CurrentDomain.BaseDirectory;
            string filePath = System.IO.Path.Combine(executionPath, configFile);

            if (!System.IO.File.Exists(filePath))
                throw new System.IO.FileNotFoundException("Config file not found.", filePath);

            string jsonContent = System.IO.File.ReadAllText(filePath);
            // Use System.Text.Json for parsing
            var jsonDoc = JsonDocument.Parse(jsonContent);
            if (jsonDoc.RootElement.TryGetProperty("EncryptSeed", out var seedElement))
                return seedElement.GetString() ?? string.Empty;
            else
                throw new InvalidOperationException("EncryptSeed property not found in EEPS.cfg.");
          
            
        }
        public static bool Check_Lock()
        {
            // ------------------------------------
            // TODO: CheckLock,
            //var appSettings = ConfigurationManager.AppSettings;
            //string result = appSettings["Environment"] ?? "";
            //if  (result == "Development")
            //    return true;
                       
            //short QueryLength = 4;
            //var QueryStr = new byte[QueryLength];
            //var ExpectedResponse = new byte[QueryLength];
            //var ResponseStr = new byte[QueryLength];
            //int TableIndex = 1;

            //{
            //    var withBlock = new Random();
            //    TableIndex = withBlock.Next(1, 100);
            //}

            //// کنترل وجود قفل نرم افزار  
            //if (!Find_Lock())
            //{
            //    return false;
            //}

            //for (short J = 0, loopTo = (short)(QueryLength - 1); J <= loopTo; J++)
            //{
            //    QueryStr[J] = QueryResponses[TableIndex, 0][J];
            //    ExpectedResponse[J] = QueryResponses[TableIndex, 1][J];
            //}

            //// Query the key
            //ResponseStr = Ltmp.RunQuery_Mylock(38, QueryStr);
            //Ltmp.Free_MyLicenceLock();
            ////for (int CompIndex = 0, loopTo1 = QueryLength - 1; CompIndex <= loopTo1; CompIndex++)
            ////{
            ////    if (ResponseStr[CompIndex] != ExpectedResponse[CompIndex])
            ////    {
            ////        return false;
            ////    }
            ////}

            return true;

        }

        private static void FreeLicenceLock()
        {
            int mysd;
            mysd = Ltmp.Free_MyLicenceLock();
        }

        public static bool Control_Exist_Lock()
        {
            bool Control_Exist_LockRet = default;
            Control_Exist_LockRet = true;
            //if (Find_Lock() == false) // find lock and get a licence
            //{
            //    Control_Exist_LockRet = false;
            //    return Control_Exist_LockRet;
            //}

           
            //FreeLicenceLock();
            return Control_Exist_LockRet;
        }

        public static int Get_NumberOfUsers()
        {
            int Get_NumberOfUsersRet = 1000;
            //string MyRslt;
            //string MySd;
            //// MyRslt = "10"
            //if (Find_Lock() == false) // find lock and get a licence
            //{
            //    return Get_NumberOfUsersRet;
            //}

            //// get seed from lock
            //MySd = Ltmp.GetMySeed(MySoftwareID);  // get seed from Lock
            //                                      // Call FreeLicenceLock() ' free the licence
            //MyRslt = Ltmp.ReadMyCell(CellUserNumber).ToString();
            //Get_NumberOfUsersRet = Conversions.ToInteger(MyRslt);
            return Get_NumberOfUsersRet;
        }
    }
}