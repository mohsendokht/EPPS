using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using static ProductionPlanning.MyEnums;


namespace ProductionPlanning
{
    public partial class frmToolsLists_v3 : Form
    {

        private SqlDataAdapter daTools;
        private DataSet dsLocal;
        private DataSet dsToolTypes;
        private bool isDataLoaded = false;
        bool rowModified = false;
        int editingRowIndex = -1;
        object oldValue = null;

        public frmToolsLists_v3()
        {
            InitializeComponent();
        }

        private void frmToolsLists_v3_Load(object sender, EventArgs e)
        {
            try
            {
                LoadToolTypesData();    // اول انواع ابزارها را بارگذاری می‌کنیم
                LoadToolsData();        // سپس داده‌های اصلی ابزارها
                ConfigureComboBoxColumn(); // ستون ComboBox را تنظیم می‌کنیم
                LoadSavedColumnWidths();   // عرض ستون‌های ذخیره شده را بارگذاری می‌کنیم
                isDataLoaded = true;    // علامت گذاری که داده‌ها بارگذاری شده‌اند
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا در بارگذاری فرم: " + ex.Message, "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// داده‌های جدول Tbl_ToolType را برای پر کردن ComboBox بارگذاری می‌کند
        /// </summary>
        private void LoadToolTypesData()
        {
            try
            {
                string toolTypesQuery = "SELECT [ID], [TypeName] FROM [Tbl_ToolType]  ORDER BY [TypeName]";
                SqlDataAdapter daToolTypes = new SqlDataAdapter(toolTypesQuery, Module1.cnProductionPlanning);
                dsToolTypes = new DataSet();
                daToolTypes.Fill(dsToolTypes, "Tbl_ToolType");
            }
            catch (Exception ex)
            {
                throw new Exception("خطا در بارگذاری انواع ابزار: " + ex.Message);
            }
        }


        /// <summary>
        /// داده‌های جدول Tbl_Tools را از دیتابیس بارگذاری می‌کند
        /// </summary>
        private void LoadToolsData()
        {
            try
            {
                string selectStr = @"SELECT 
            T.[ID],
            T.[ToolCode],
            T.[ToolName],
            T.[ToolTypeID],
            TT.[TypeName],
            T.[TechnicalSpecs],
            T.[CurrentQuantity],
            T.[MinStockLevel],
            T.[ToolLocation],
            T.[MaintenanceCycle],
            T.[CreatedAt],
            T.[ModifiedAt]
        FROM [Tbl_Tools] T
        LEFT JOIN [Tbl_ToolType] TT ON T.ToolTypeID = TT.ID
        WHERE T.[IsDeleted] = 0";

                daTools = new SqlDataAdapter(selectStr, Module1.cnProductionPlanning);

                // ایجاد دستورات INSERT, UPDATE, DELETE
                CreateDataAdapterCommands();

                dsLocal = new DataSet();
                daTools.Fill(dsLocal, "Tbl_Tools");

                dgvTools.AutoGenerateColumns = false;


                dgvTools.DataSource = dsLocal.Tables["Tbl_Tools"];
            }
            catch (Exception ex)
            {
                throw new Exception("خطا در بارگذاری داده‌ها: " + ex.Message);
            }
        }

        /// <summary>
        /// ستون TypeName را از TextBox به ComboBox تبدیل می‌کند
        /// </summary>
        private void ConfigureComboBoxColumn()
        {
            // ایجاد ستون ComboBox برای TypeName
            DataGridViewComboBoxColumn comboColumn = new DataGridViewComboBoxColumn();
            comboColumn.Name = "TypeName";
            comboColumn.HeaderText = "نوع ابزار";
            comboColumn.DataPropertyName = "ToolTypeID"; // این به فیلد ToolTypeID در دیتاست وصل می‌شود
            comboColumn.DisplayMember = "TypeName"; // آنچه نمایش داده می‌شود
            comboColumn.ValueMember = "ID"; // مقدار واقعی که ذخیره می‌شود
            comboColumn.DataSource = dsToolTypes.Tables["Tbl_ToolType"];

            // جایگزینی ستون متن معمولی با ComboBox
            int columnIndex = dgvTools.Columns["TypeName"].Index;
            dgvTools.Columns.Remove("TypeName");
            dgvTools.Columns.Insert(columnIndex, comboColumn);
        }


        /// <summary>
        /// دستورات INSERT, UPDATE, DELETE را برای SqlDataAdapter ایجاد می‌کند
        /// </summary>
        private void CreateDataAdapterCommands()
        {
            // دستور INSERT - بدون ID چون Identity است
            daTools.InsertCommand = new SqlCommand(
                @"INSERT INTO Tbl_Tools 
        (ToolCode, ToolName, ToolTypeID, TechnicalSpecs, CurrentQuantity, 
         MinStockLevel, ToolLocation, MaintenanceCycle, CreatedAt, ModifiedAt) 
        VALUES (@ToolCode, @ToolName, @ToolTypeID, @TechnicalSpecs, @CurrentQuantity, 
                @MinStockLevel, @ToolLocation, @MaintenanceCycle, @CreatedAt, @ModifiedAt);
        SELECT SCOPE_IDENTITY();", // برای گرفتن ID تولید شده
                Module1.cnProductionPlanning);

            // پارامترهای INSERT
            daTools.InsertCommand.Parameters.Add("@ToolCode", SqlDbType.NVarChar, 50, "ToolCode");
            daTools.InsertCommand.Parameters.Add("@ToolName", SqlDbType.NVarChar, 100, "ToolName");
            daTools.InsertCommand.Parameters.Add("@ToolTypeID", SqlDbType.Int, 0, "ToolTypeID");
            daTools.InsertCommand.Parameters.Add("@TechnicalSpecs", SqlDbType.NText, 0, "TechnicalSpecs");
            daTools.InsertCommand.Parameters.Add("@CurrentQuantity", SqlDbType.Int, 0, "CurrentQuantity");
            daTools.InsertCommand.Parameters.Add("@MinStockLevel", SqlDbType.Int, 0, "MinStockLevel");
            daTools.InsertCommand.Parameters.Add("@ToolLocation", SqlDbType.NVarChar, 200, "ToolLocation");
            daTools.InsertCommand.Parameters.Add("@MaintenanceCycle", SqlDbType.NVarChar, 100, "MaintenanceCycle");
            daTools.InsertCommand.Parameters.Add("@CreatedAt", SqlDbType.DateTime, 0, "CreatedAt");
            daTools.InsertCommand.Parameters.Add("@ModifiedAt", SqlDbType.DateTime, 0, "ModifiedAt");

            // دستور UPDATE
            daTools.UpdateCommand = new SqlCommand(
                @"UPDATE Tbl_Tools SET 
        ToolCode = @ToolCode, 
        ToolName = @ToolName, 
        ToolTypeID = @ToolTypeID, 
        TechnicalSpecs = @TechnicalSpecs, 
        CurrentQuantity = @CurrentQuantity, 
        MinStockLevel = @MinStockLevel, 
        ToolLocation = @ToolLocation, 
        MaintenanceCycle = @MaintenanceCycle, 
        ModifiedAt = @ModifiedAt 
        WHERE ID = @ID",
                Module1.cnProductionPlanning);

            // پارامترهای UPDATE
            daTools.UpdateCommand.Parameters.Add("@ToolCode", SqlDbType.NVarChar, 50, "ToolCode");
            daTools.UpdateCommand.Parameters.Add("@ToolName", SqlDbType.NVarChar, 100, "ToolName");
            daTools.UpdateCommand.Parameters.Add("@ToolTypeID", SqlDbType.Int, 0, "ToolTypeID");
            daTools.UpdateCommand.Parameters.Add("@TechnicalSpecs", SqlDbType.NText, 0, "TechnicalSpecs");
            daTools.UpdateCommand.Parameters.Add("@CurrentQuantity", SqlDbType.Int, 0, "CurrentQuantity");
            daTools.UpdateCommand.Parameters.Add("@MinStockLevel", SqlDbType.Int, 0, "MinStockLevel");
            daTools.UpdateCommand.Parameters.Add("@ToolLocation", SqlDbType.NVarChar, 200, "ToolLocation");
            daTools.UpdateCommand.Parameters.Add("@MaintenanceCycle", SqlDbType.NVarChar, 100, "MaintenanceCycle");
            daTools.UpdateCommand.Parameters.Add("@ModifiedAt", SqlDbType.DateTime, 0, "ModifiedAt");
            daTools.UpdateCommand.Parameters.Add("@ID", SqlDbType.Int, 0, "ID").SourceVersion = DataRowVersion.Original;

            // دستور DELETE (Soft Delete)
            daTools.DeleteCommand = new SqlCommand(
                @"UPDATE Tbl_Tools SET IsDeleted = 1, ModifiedAt = GETDATE() WHERE ID = @ID",
                Module1.cnProductionPlanning);
            daTools.DeleteCommand.Parameters.Add("@ID", SqlDbType.Int, 0, "ID").SourceVersion = DataRowVersion.Original;
        }

        /// <summary>
        /// عرض ستون‌های ذخیره شده برای کاربر جاری را بارگذاری می‌کند
        /// </summary>
        private void LoadSavedColumnWidths()
        {
            // بارگذاری عرض ستون‌های ذخیره شده
            Module1.SaveGridColumnsWidth(this.Name, ListFormCaller.LFC_TOOLS, dgvTools);
        }
        /// <summary>
        /// تمام تغییرات انجام شده در گرید را در دیتابیس ذخیره می‌کند
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveAllChanges();
        }

        /// <summary>
        /// تغییرات داده‌ها را اعتبارسنجی و در دیتابیس ذخیره می‌کند
        /// </summary>
        private void SaveAllChanges()
        {
            try
            {
                if (!isDataLoaded) return;

                // اطمینان از پایان ویرایش
                dgvTools.EndEdit();

                // بررسی تغییرات
                if (dsLocal.HasChanges())
                {
                    // برای رکوردهای جدید، تنظیم مقادیر پیش‌فرض
                    foreach (DataRow row in dsLocal.Tables["Tbl_Tools"].Rows)
                    {
                        if (row.RowState == DataRowState.Added)
                        {
                            if (row["ToolCode"] == DBNull.Value || string.IsNullOrEmpty(row["ToolCode"].ToString()))
                                row["ToolCode"] = "TOOL_" + DateTime.Now.ToString("yyyyMMddHHmmss");

                            // مقدار پیش‌فرض برای ToolTypeID اگر خالی است
                            if (row["ToolTypeID"] == DBNull.Value && dsToolTypes.Tables["Tbl_ToolType"].Rows.Count > 0)
                                row["ToolTypeID"] = dsToolTypes.Tables["Tbl_ToolType"].Rows[0]["ID"];

                            if (row["CreatedAt"] == DBNull.Value)
                                row["CreatedAt"] = DateTime.Now;

                            row["ModifiedAt"] = DateTime.Now;

                            // ID نباید تنظیم شود چون Identity است
                            if (row["ID"] == DBNull.Value)
                                row["ID"] = DBNull.Value; // اطمینان از خالی بودن
                        }
                        else if (row.RowState == DataRowState.Modified)
                        {
                            row["ModifiedAt"] = DateTime.Now;
                        }
                    }

                    // تست اتصال
                    if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                        Module1.cnProductionPlanning.Open();

                    // ذخیره در دیتابیس
                    int affectedRows = daTools.Update(dsLocal, "Tbl_Tools");

                    if (affectedRows > 0)
                    {
                        // رفرش داده‌ها برای گرفتن IDهای تولید شده
                        dsLocal.Tables["Tbl_Tools"].Clear();
                        daTools.Fill(dsLocal, "Tbl_Tools");

                        MessageBox.Show("تغییرات با موفقیت ذخیره شد", "پیام",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("هیچ تغییری برای ذخیره وجود ندارد", "اطلاع",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                dsLocal.RejectChanges();
                MessageBox.Show($"خطا در ذخیره تغییرات: {ex.Message}", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                    Module1.cnProductionPlanning.Close();
            }
        }

        private void SaveAllChanges2()
        {
            try
            {
                if (!isDataLoaded) return;

                this.Validate();                  // اعتبارسنجی کنترل‌ها
                dgvTools.EndEdit();               // خروج از حالت ادیت سلول
                CurrencyManager cm =
                    (CurrencyManager)this.BindingContext[dgvTools.DataSource];
                cm.EndCurrentEdit();
                // بررسی تغییرات
                if (dsLocal.HasChanges())
                {
                    // برای رکوردهای جدید، تنظیم مقادیر پیش‌فرض
                    foreach (DataRow row in dsLocal.Tables["Tbl_Tools"].Rows)
                    {
                        if (row.RowState == DataRowState.Added)
                        {
                            if (row["ToolCode"] == DBNull.Value || string.IsNullOrEmpty(row["ToolCode"].ToString()))
                                row["ToolCode"] = "TOOL_" + DateTime.Now.ToString("yyyyMMddHHmmss");

                            // مقدار پیش‌فرض برای ToolTypeID اگر خالی است
                            if (row["ToolTypeID"] == DBNull.Value && dsToolTypes.Tables["Tbl_ToolType"].Rows.Count > 0)
                                row["ToolTypeID"] = dsToolTypes.Tables["Tbl_ToolType"].Rows[0]["ID"];

                            if (row["CreatedAt"] == DBNull.Value)
                                row["CreatedAt"] = DateTime.Now;

                            row["ModifiedAt"] = DateTime.Now;

                            // ID نباید تنظیم شود چون Identity است
                            if (row["ID"] == DBNull.Value)
                                row["ID"] = DBNull.Value; // اطمینان از خالی بودن
                        }
                        else if (row.RowState == DataRowState.Modified)
                        {
                            row["ModifiedAt"] = DateTime.Now;
                        }
                    }

                    // تست اتصال
                    if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                        Module1.cnProductionPlanning.Open();

                    // ذخیره در دیتابیس
                    int affectedRows = daTools.Update(dsLocal, "Tbl_Tools");

                    if (affectedRows > 0)
                    {
                        // رفرش داده‌ها برای گرفتن IDهای تولید شده
                        dsLocal.Tables["Tbl_Tools"].Clear();
                        daTools.Fill(dsLocal, "Tbl_Tools");

                        MessageBox.Show("تغییرات با موفقیت ذخیره شد", "پیام",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("هیچ تغییری برای ذخیره وجود ندارد", "اطلاع",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                dsLocal.RejectChanges();
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
        /// رکورد انتخاب شده را از گرید حذف می‌کند
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTools.CurrentRow != null && !dgvTools.CurrentRow.IsNewRow)
            {
                if (MessageBox.Show("آیا از حذف این رکورد اطمینان دارید؟", "تایید حذف",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        // حذف از گرید
                        dgvTools.Rows.RemoveAt(dgvTools.CurrentRow.Index);
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
        /// داده‌ها را بر اساس متن وارد شده فیلتر می‌کند
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                // نمایش تمام رکوردها
                dgvTools.DataSource = dsLocal.Tables["Tbl_Tools"];
                return;
            }

            try
            {
                DataView dv = new DataView(dsLocal.Tables["Tbl_Tools"]);
                dv.RowFilter = $"ToolName LIKE '%{searchText}%' OR ToolCode LIKE '%{searchText}%'";

                dgvTools.DataSource = dv;
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
        /// هنگام بسته شدن فرم، عرض ستون‌ها ذخیره و منابع آزاد می‌شوند
        /// </summary>
        private void frmToolsLists_v3_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // ذخیره عرض ستون‌ها
                Module1.SaveGridColumnsWidth(this.Name, ListFormCaller.LFC_TOOLS, dgvTools);

                // پاکسازی منابع
                dgvTools.DataSource = null;
                if (dsLocal != null)
                    dsLocal.Dispose();
                if (dsToolTypes != null)
                    dsToolTypes.Dispose();
                if (daTools != null)
                    daTools.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("خطا در ذخیره عرض ستون‌ها: " + ex.Message);
            }
        }

        /// <summary>
        /// برای سطر جدید در گرید مقادیر پیش‌فرض تنظیم می‌کند
        /// </summary>
        private void dgvTools_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            // تنظیم مقادیر پیش‌فرض برای سطر جدید
            // ID تنظیم نمی‌شود چون Identity است
       //     e.Row.Cells["ToolCode"].Value = "TOOL_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            e.Row.Cells["CurrentQuantity"].Value = 0;
            e.Row.Cells["MinStockLevel"].Value = 0;

            // مقدار پیش‌فرض برای ToolTypeID
            //if (dsToolTypes != null && dsToolTypes.Tables["Tbl_ToolType"].Rows.Count > 0)
            //    e.Row.Cells["ToolTypeID"].Value = dsToolTypes.Tables["Tbl_ToolType"].Rows[0]["ID"];

            e.Row.Cells["CreatedAt"].Value = DateTime.Now;
            e.Row.Cells["ModifiedAt"].Value = DateTime.Now;
        }

        /// <summary>
        /// هنگام حذف سطر از کاربر تأیید می‌گیرد
        /// </summary>
        private void dgvTools_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (e.Row.IsNewRow)
            {
                e.Cancel = true;
                return;
            }

            if (MessageBox.Show("آیا از حذف این رکورد اطمینان دارید؟", "تایید حذف",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }
        /// <summary>
        /// خطاهای مربوط به داده‌های نامعتبر را مدیریت می‌کند
        /// </summary>
        private void dgvTools_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // مدیریت خطاهای داده (مثلاً مقادیر نامعتبر در ComboBox)
            MessageBox.Show($"خطا در داده‌ها: {e.Exception.Message}", "خطای داده",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            e.ThrowException = false;
        }
        private void dgvTools_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
       //     SaveAllChanges2();
        }

        /// <summary>
        /// با فشردن کلید Enter در TextBox جستجو، عمل جستجو انجام می‌شود
        /// </summary>
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch_Click(sender, e);
            }
        }

        private void dgvTools_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            editingRowIndex = e.RowIndex;
            rowModified = false;   // از نو شروع کن
            oldValue = dgvTools[e.ColumnIndex, e.RowIndex].Value;
        }

        private void dgvTools_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var newValue = dgvTools[e.ColumnIndex, e.RowIndex].Value;
            if (e.RowIndex == editingRowIndex && !Equals(oldValue, newValue))
            {
                rowModified = true;
            }
        }

        private void dgvTools_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (!isDataLoaded) return;

            // ۱) اول ادیت سلول رو ببند
            dgvTools.EndEdit();

            // ۲) بعد کامیت روی DataRow
            CurrencyManager cm =
                (CurrencyManager)this.BindingContext[dgvTools.DataSource];
            cm.EndCurrentEdit();

            // ۳) حالا با خیال راحت چک کن آیا این سطر واقعاً تغییر کرده
            if (rowModified && e.RowIndex == editingRowIndex)
            {
                rowModified = false;
                editingRowIndex = -1;
                SaveAllChanges2();
            }
        }


    }
}
