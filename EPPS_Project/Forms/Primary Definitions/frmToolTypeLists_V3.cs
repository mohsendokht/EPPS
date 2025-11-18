using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static ProductionPlanning.MyEnums;

namespace ProductionPlanning
{
    public partial class frmToolTypeLists_V3 : Form
    {
        private SqlDataAdapter daToolTypes;
        private DataSet dsLocal;
        private bool isDataLoaded = false;

        public frmToolTypeLists_V3()
        {
            InitializeComponent();
        }

        private void frmToolTypeLists_V3_Load(object sender, EventArgs e)
        {
            try
            {
                LoadToolTypesData();
                LoadSavedColumnWidths();
                isDataLoaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا در بارگذاری فرم: " + ex.Message, "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// داده‌های جدول Tbl_ToolType را از دیتابیس بارگذاری می‌کند
        /// </summary>
        private void LoadToolTypesData()
        {
            try
            {
                string selectStr = @"SELECT [ID], [TypeName] FROM [Tbl_ToolType] ORDER BY [TypeName]";
                daToolTypes = new SqlDataAdapter(selectStr, Module1.cnProductionPlanning);

                // ساخت دستورات INSERT/UPDATE/DELETE مخصوص این جدول
                CreateDataAdapterCommands();

                dsLocal = new DataSet();
                daToolTypes.Fill(dsLocal, "Tbl_ToolType");

                dgvToolTypes.AutoGenerateColumns = false;
                dgvToolTypes.DataSource = dsLocal.Tables["Tbl_ToolType"];
            }
            catch (Exception ex)
            {
                throw new Exception("خطا در بارگذاری داده‌های نوع ابزار: " + ex.Message);
            }
        }

        /// <summary>
        /// ایجاد دستورات INSERT, UPDATE, DELETE برای SqlDataAdapter
        /// توجه: جدول Tbl_ToolType فقط ID (Identity) و TypeName دارد.
        /// حذف فیزیکی (DELETE FROM ...) انجام می‌شود.
        /// </summary>
        private void CreateDataAdapterCommands()
        {
            // INSERT
            daToolTypes.InsertCommand = new SqlCommand(
                @"INSERT INTO Tbl_ToolType (TypeName) VALUES (@TypeName);
                  SELECT SCOPE_IDENTITY();",
                Module1.cnProductionPlanning);

            daToolTypes.InsertCommand.Parameters.Add("@TypeName", SqlDbType.NVarChar, 100, "TypeName");

            // UPDATE
            daToolTypes.UpdateCommand = new SqlCommand(
                @"UPDATE Tbl_ToolType SET TypeName = @TypeName WHERE ID = @ID",
                Module1.cnProductionPlanning);

            daToolTypes.UpdateCommand.Parameters.Add("@TypeName", SqlDbType.NVarChar, 100, "TypeName");
            daToolTypes.UpdateCommand.Parameters.Add("@ID", SqlDbType.Int, 0, "ID").SourceVersion = DataRowVersion.Original;

            // DELETE - حذف فیزیکی
            daToolTypes.DeleteCommand = new SqlCommand(
                @"DELETE FROM Tbl_ToolType WHERE ID = @ID",
                Module1.cnProductionPlanning);

            daToolTypes.DeleteCommand.Parameters.Add("@ID", SqlDbType.Int, 0, "ID").SourceVersion = DataRowVersion.Original;
        }

        /// <summary>
        /// ذخیره تغییرات در دیتابیس
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveAllChanges();
        }

        private void SaveAllChanges()
        {
            try
            {
                if (!isDataLoaded) return;

                dgvToolTypes.EndEdit();

                if (dsLocal == null || !dsLocal.HasChanges())
                {
                    MessageBox.Show("هیچ تغییری برای ذخیره وجود ندارد", "اطلاع",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // تنظیم مقادیر پیش‌فرض برای رکوردهای جدید (در این جدول فقط TypeName مهم است)
                foreach (DataRow row in dsLocal.Tables["Tbl_ToolType"].Rows)
                {
                    if (row.RowState == DataRowState.Added)
                    {
                        if (row["TypeName"] == DBNull.Value || string.IsNullOrEmpty(row["TypeName"].ToString()))
                        {
                            row["TypeName"] = "نوع ابزار";
                        }
                    }
                }

                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();

                int affectedRows = daToolTypes.Update(dsLocal, "Tbl_ToolType");

                if (affectedRows > 0)
                {
                    // رفرش داده‌ها برای هماهنگ‌سازی ID های ایجادشده و وضعیت دیتاست
                    dsLocal.Tables["Tbl_ToolType"].Clear();
                    daToolTypes.Fill(dsLocal, "Tbl_ToolType");

                    MessageBox.Show("تغییرات با موفقیت ذخیره شد", "پیام",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("هیچ رکوردی تغییر نکرد یا خطایی رخ داده است", "اطلاع",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                if (dsLocal != null) dsLocal.RejectChanges();
                MessageBox.Show($"خطا در ذخیره تغییرات: {ex.Message}", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                    Module1.cnProductionPlanning.Close();
            }
        }

        /// <summary>
        /// حذف رکورد انتخابی (حذف از گرید => وقتی Update اجرا شود، دستور DELETE اجرا می‌شود)
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvToolTypes.CurrentRow != null && !dgvToolTypes.CurrentRow.IsNewRow)
            {
                if (MessageBox.Show("آیا از حذف این رکورد اطمینان دارید؟", "تأیید حذف",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        // حذف از DataGridView (که RowState را به Deleted تغییر می‌دهد)
                        dgvToolTypes.Rows.RemoveAt(dgvToolTypes.CurrentRow.Index);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("خطا در حذف رکورد: " + ex.Message, "خطا",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("لطفاً یک رکورد را انتخاب کنید", "هشدار",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// جستجو بر اساس TypeName
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                dgvToolTypes.DataSource = dsLocal.Tables["Tbl_ToolType"];
                return;
            }

            try
            {
                DataView dv = new DataView(dsLocal.Tables["Tbl_ToolType"]);
                dv.RowFilter = $"TypeName LIKE '%{searchText.Replace("'", "''")}%'";

                dgvToolTypes.DataSource = dv;
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا در جستجو: " + ex.Message, "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// هنگام بستن فرم منابع آزاد می‌شوند و عرض ستون‌ها ذخیره می‌شود
        /// </summary>
        private void frmToolTypeLists_V3_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // اگر ماژول شما تابع ذخیره عرض ستون دارد، این را نگه دارید
                Module1.SaveGridColumnsWidth(this.Name, ListFormCaller.LFC_TOOLTYPES, dgvToolTypes);

                dgvToolTypes.DataSource = null;
                if (dsLocal != null) dsLocal.Dispose();
                if (daToolTypes != null) daToolTypes.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("خطا در فرم بسته‌شدن: " + ex.Message);
            }
        }

 
        /// <summary>
        /// کنترل حذف سطر توسط کاربر (تأیید)
        /// </summary>
        private void dgvToolTypes_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (e.Row.IsNewRow)
            {
                e.Cancel = true;
                return;
            }

            if (MessageBox.Show("آیا از حذف این رکورد اطمینان دارید؟", "تأیید حذف",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        //private void dgvToolTypes_DataError(object sender, DataGridViewDataErrorEventArgs e)
        //{
        //    MessageBox.Show($"خطا در داده‌ها: {e.Exception.Message}", "خطای داده",
        //        MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    e.ThrowException = false;
        //}

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch_Click(sender, e);
            }
        }

        /// <summary>
        /// بارگذاری عرض ستون‌ها (در صورتی که تابع در Module1 موجود باشد)
        /// </summary>
        private void LoadSavedColumnWidths()
        {
            // نام constant ListFormCaller.LFC_TOOL_TYPES فرضی است — اگر ندارید تغییر دهید یا حذف کنید
            try
            {
                Module1.SaveGridColumnsWidth(this.Name, ListFormCaller.LFC_TOOLTYPES, dgvToolTypes);
            }
            catch
            {
                // اگر تابع یا ثابت وجود ندارد، نادیده بگیرید
            }
        }
    }
}
