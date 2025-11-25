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
    public partial class frmToolsLists_v2 : Form
    {
        private SqlDataAdapter daTools;
        private DataSet dsLocal;
        private bool isDataLoaded = false;

        public frmToolsLists_v2()
        {
            InitializeComponent();
        }

        private void frmToolsLists_v2_Load(object sender, EventArgs e)
        {
            try
            {
                LoadToolsData();
                ConfigureGridColumns();
                LoadSavedColumnWidths();
                isDataLoaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا در بارگذاری فرم: " + ex.Message, "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

                // ایجاد دستورات INSERT, UPDATE, DELETE به صورت دستی
                CreateDataAdapterCommands();

                dsLocal = new DataSet();
                daTools.Fill(dsLocal, "Tbl_Tools");

                // تنظیم کلید اصلی برای جدول
                dsLocal.Tables["Tbl_Tools"].PrimaryKey = new DataColumn[] {
                    dsLocal.Tables["Tbl_Tools"].Columns["ID"]
                };

                dgvTools.DataSource = dsLocal.Tables["Tbl_Tools"];
            }
            catch (Exception ex)
            {
                throw new Exception("خطا در بارگذاری داده‌ها: " + ex.Message);
            }
        }

        private void CreateDataAdapterCommands()
        {
            // دستور INSERT
            daTools.InsertCommand = new SqlCommand(
                @"INSERT INTO Tbl_Tools 
        (ToolCode, ToolName, ToolTypeID, TechnicalSpecs, CurrentQuantity, 
         MinStockLevel, ToolLocation, MaintenanceCycle, CreatedAt, ModifiedAt) 
        VALUES (@ToolCode, @ToolName, @ToolTypeID, @TechnicalSpecs, @CurrentQuantity, 
                @MinStockLevel, @ToolLocation, @MaintenanceCycle, @CreatedAt, @ModifiedAt)",
                Module1.cnProductionPlanning);

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

            // دستور UPDATE - اصلاح شده
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

            // اضافه کردن پارامترها به UpdateCommand
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
        private void ConfigureGridColumns()
        {
            dgvTools.Columns["ID"].Visible = false;
            dgvTools.Columns["ToolCode"].HeaderText = "کد ابزار";
            dgvTools.Columns["ToolName"].HeaderText = "نام ابزار";
            dgvTools.Columns["ToolTypeID"].Visible = false;
            dgvTools.Columns["TypeName"].HeaderText = "نوع ابزار";
            dgvTools.Columns["TechnicalSpecs"].HeaderText = "مشخصات فنی";
            dgvTools.Columns["CurrentQuantity"].HeaderText = "موجودی فعلی";
            dgvTools.Columns["MinStockLevel"].HeaderText = "حداقل موجودی";
            dgvTools.Columns["ToolLocation"].HeaderText = "محل ذخیره";
            dgvTools.Columns["MaintenanceCycle"].HeaderText = "دوره تعمیر";
            dgvTools.Columns["CreatedAt"].Visible = false;
            dgvTools.Columns["ModifiedAt"].Visible = false;

            // فعال کردن افزودن سطر جدید از طریق گرید
            dgvTools.AllowUserToAddRows = true;
            dgvTools.ReadOnly = false;
            dgvTools.EditMode = DataGridViewEditMode.EditOnEnter;
        }

        private void LoadSavedColumnWidths()
        {
            // استفاده از ماژول موجود پروژه - بدون ListFormCaller
            Module1.SetGridColumnsWidth(this.Name, ListFormCaller.LFC_TOOLS, dgvTools);
        }

        // ================== EVENT HANDLERS ==================

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

                        MessageBox.Show("رکورد با موفقیت حذف شد", "پیام",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveAllChanges();
        }

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

                            // تنظیم ToolTypeID اگر null است
                            if (row["ToolTypeID"] == DBNull.Value)
                                row["ToolTypeID"] = 1; // مقدار پیش‌فرض

                            if (row["CreatedAt"] == DBNull.Value)
                                row["CreatedAt"] = DateTime.Now;

                            row["ModifiedAt"] = DateTime.Now;
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
                        dsLocal.AcceptChanges();
                        MessageBox.Show("تغییرات با موفقیت ذخیره شد", "پیام",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("هیچ رکوردی بروزرسانی نشد", "اطلاع",
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
                MessageBox.Show($"خطا در ذخیره تغییرات: {ex.Message}\n\nجزئیات فنی: {ex.InnerException?.Message}", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // بستن اتصال اگر باز است
                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                    Module1.cnProductionPlanning.Close();
            }
        }
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
                dv.RowFilter = $"ToolName LIKE '%{searchText}%' OR ToolCode LIKE '%{searchText}%' OR TypeName LIKE '%{searchText}%'";

                dgvTools.DataSource = dv;
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا در جستجو: " + ex.Message, "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                // نمایش فیلتر ساده بر اساس نوع ابزار - بدون استفاده از LINQ
                System.Collections.Generic.List<string> distinctTypes = new System.Collections.Generic.List<string>();

                foreach (DataRow row in dsLocal.Tables["Tbl_Tools"].Rows)
                {
                    if (!row.IsNull("TypeName"))
                    {
                        string typeName = row["TypeName"].ToString();
                        if (!distinctTypes.Contains(typeName))
                        {
                            distinctTypes.Add(typeName);
                        }
                    }
                }

                string[] items = new string[distinctTypes.Count];
                distinctTypes.CopyTo(items);

                string selectedType = ShowSimpleFilterDialog(items);

                if (!string.IsNullOrEmpty(selectedType))
                {
                    DataView dv = new DataView(dsLocal.Tables["Tbl_Tools"]);
                    dv.RowFilter = $"TypeName = '{selectedType.Replace("'", "''")}'";
                    dgvTools.DataSource = dv;
                }
                else if (selectedType == "") // اگر کاربر "همه" را انتخاب کرد
                {
                    dgvTools.DataSource = dsLocal.Tables["Tbl_Tools"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا در فیلتر: " + ex.Message, "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ShowSimpleFilterDialog(string[] items)
        {
            Form filterForm = new Form()
            {
                Text = "فیلتر بر اساس نوع ابزار",
                Size = new System.Drawing.Size(300, 150),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent
            };

            ComboBox cmbFilter = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 200,
                Location = new System.Drawing.Point(50, 20)
            };

            // اضافه کردن آیتم‌ها به کامبوباکس
            cmbFilter.Items.Add("--- همه ---");
            foreach (string item in items)
            {
                cmbFilter.Items.Add(item);
            }
            cmbFilter.SelectedIndex = 0;

            Button btnOK = new Button() { Text = "تایید", DialogResult = DialogResult.OK, Location = new System.Drawing.Point(50, 60) };
            Button btnCancel = new Button() { Text = "انصراف", DialogResult = DialogResult.Cancel, Location = new System.Drawing.Point(150, 60) };

            filterForm.Controls.AddRange(new Control[] { cmbFilter, btnOK, btnCancel });
            filterForm.AcceptButton = btnOK;
            filterForm.CancelButton = btnCancel;

            if (filterForm.ShowDialog() == DialogResult.OK)
            {
                return cmbFilter.SelectedItem?.ToString() == "--- همه ---" ? "" : cmbFilter.SelectedItem?.ToString();
            }

            return null;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvTools_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("test");
            // ذخیره خودکار هنگام خروج از سطر (اختیاری)
        }

        private void dgvTools_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            MessageBox.Show("test2");
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

        private void dgvTools_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            MessageBox.Show("test");
            // تنظیم مقادیر پیش‌فرض برای سطر جدید
            e.Row.Cells["ID"].Value = 2;
            e.Row.Cells["CurrentQuantity"].Value = 0;
            e.Row.Cells["MinStockLevel"].Value = 0;
            e.Row.Cells["CreatedAt"].Value = DateTime.Now;
            e.Row.Cells["ModifiedAt"].Value = DateTime.Now;
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch_Click(sender, e);
            }
        }

        private void frmToolsLists_v2_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // ذخیره عرض ستون‌ها با استفاده از ماژول موجود - بدون ListFormCaller
                Module1.SaveGridColumnsWidth(this.Name, ListFormCaller.LFC_TOOLS, dgvTools);

                // پاکسازی منابع
                dgvTools.DataSource = null;
                if (dsLocal != null)
                {
                    dsLocal.Dispose();
                }
                if (daTools != null)
                {
                    daTools.Dispose();
                }
            }
            catch (Exception ex)
            {
                // در صورت خطا در ذخیره عرض ستون‌ها، فرم بسته می‌شود
                Console.WriteLine("خطا در ذخیره عرض ستون‌ها: " + ex.Message);
            }
        }
    }
}