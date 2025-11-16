using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning.Planning_Forms
{
    public partial class frmDailyPlan : Form
    {
        private DataSet mdsDailyPlan;
        private SqlDataAdapter daDailyPlan;
        private SqlCommand cmSelect;

        public frmDailyPlan()
        {
            InitializeComponent();

            // Use RTL layout for Persian UI
            this.RightToLeft = RightToLeft.Yes;
            this.RightToLeftLayout = true;

            // Initialize data objects
            mdsDailyPlan = new DataSet();
            daDailyPlan = new SqlDataAdapter();
            cmSelect = new SqlCommand();
            daDailyPlan.SelectCommand = cmSelect;
        }

        private void frmDailyPlan_Load(object sender, EventArgs e)
        {
            // Default to today
            dtpDate.Value = DateTime.Now.Date;
            dgvPlan.AutoGenerateColumns = true;

            // Ensure new rows get PlanDateGr set to the currently selected date
            dgvPlan.DefaultValuesNeeded -= dgvPlan_DefaultValuesNeeded;
            dgvPlan.DefaultValuesNeeded += dgvPlan_DefaultValuesNeeded;

            // Save button should be disabled until there are changes
            try { if (btnSave != null) btnSave.Enabled = false; } catch { }

            // Optionally auto-load today's plan:
            // LoadPlanForDate(dtpDate.Value);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                LoadPlanForDate(dtpDate.Value.Date);
            }
            catch (Exception ex)
            {
                Logger.SaveError("frmDailyPlan.btnLoad_Click", ex.Message);
                MessageBox.Show("بارگذاری برنامه با خطا مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveChangesToDailyPlan();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Loads daily plan records from Tbl_DailyPlan for the specified Gregorian date (PlanDateGr).
        /// </summary>
        private void LoadPlanForDate(DateTime date)
        {
            // we filter by PlanDateGr (DATE column)
            string sql = @"
SELECT
    DailyPlanID,
    PlanDateGr,
    PlanDatePe,
    CreatedAt,
    CreatedBy,
    ModifiedAt,
    IsDeleted,
    ConfirmedBy,
    ContractNo,
    OrderNo,
    OrderQuantity,
    DeliveredQty,
    BatchQty,
    SubBatchQty,
    ProductCode,
    PartID,
    OperationCode,
    MachineCode,
    ToolID,
    ShiftID,
    PlanStartHM,
    PlanEndHM,
    ProdStartHM,
    ProdEndHM,
    OperatorCode,
    StockBalanceQty,
    PlanQty,
    ProdQty,
    AcceptedQty,
    WasteQty,
    WasteReasons,
    ProdStopTime,
    ReworkQty,
    ReworkReasons
FROM Tbl_DailyPlan
WHERE PlanDateGr = @PlanDateGr
ORDER BY PlanStartHM, PlanEndHM, MachineCode, OperationCode";

            cmSelect.CommandText = sql;
            cmSelect.Connection = Module1.cnProductionPlanning;
            cmSelect.Parameters.Clear();
            cmSelect.Parameters.Add(new SqlParameter("@PlanDateGr", SqlDbType.Date) { Value = date.Date });

            try
            {
                Module1.Openconnection();

                // Clear previous and fetch schema + data so DataAdapter.Update works correctly
                mdsDailyPlan.Tables.Clear();
                daDailyPlan.SelectCommand = cmSelect;
                daDailyPlan.FillSchema(mdsDailyPlan, SchemaType.Source, "Tbl_DailyPlan");
                daDailyPlan.Fill(mdsDailyPlan, "Tbl_DailyPlan");

                if (mdsDailyPlan.Tables.Contains("Tbl_DailyPlan"))
                {
                    dgvPlan.DataSource = mdsDailyPlan.Tables["Tbl_DailyPlan"].DefaultView;

                    // Allow editing except identity/date/timestamps
                    dgvPlan.ReadOnly = false;
                    TryLocalizeGridColumns();
                    // prevent editing identity and PlanDateGr/CreatedAt/CreatedBy
                    if (dgvPlan.Columns.Contains("DailyPlanID")) dgvPlan.Columns["DailyPlanID"].ReadOnly = true;
                    if (dgvPlan.Columns.Contains("PlanDateGr")) dgvPlan.Columns["PlanDateGr"].ReadOnly = true;
                    if (dgvPlan.Columns.Contains("CreatedAt")) dgvPlan.Columns["CreatedAt"].ReadOnly = true;
                    if (dgvPlan.Columns.Contains("CreatedBy")) dgvPlan.Columns["CreatedBy"].ReadOnly = true;

                    // Wire events to enable Save when user adds/edits rows
                    try
                    {
                        dgvPlan.UserAddedRow -= dgvPlan_UserAddedRow;
                        dgvPlan.UserAddedRow += dgvPlan_UserAddedRow;

                        dgvPlan.CellValueChanged -= dgvPlan_CellValueChanged;
                        dgvPlan.CellValueChanged += dgvPlan_CellValueChanged;

                        dgvPlan.CurrentCellDirtyStateChanged -= dgvPlan_CurrentCellDirtyStateChanged;
                        dgvPlan.CurrentCellDirtyStateChanged += dgvPlan_CurrentCellDirtyStateChanged;

                        // Ensure Save button reflects current state (no pending changes after load)
                        if (btnSave != null) btnSave.Enabled = false;
                    }
                    catch
                    {
                        // ignore wiring failures
                    }
                }
                else
                {
                    dgvPlan.DataSource = null;
                }
            }
            catch (SqlException sqlEx)
            {
                Logger.SaveError("frmDailyPlan.LoadPlanForDate", sqlEx.Message);
                MessageBox.Show("خطا در فراخوانی اطلاعات برنامه روزانه", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
            finally
            {
                Module1.Closeconnection();
            }
        }

        /// <summary>
        /// Save added or modified rows back to Tbl_DailyPlan.
        /// New rows will have PlanDateGr set to the currently selected date.
        /// </summary>
        private void SaveChangesToDailyPlan()
        {
            if (mdsDailyPlan.Tables.Count == 0 || !mdsDailyPlan.Tables.Contains("Tbl_DailyPlan"))
            {
                MessageBox.Show("هیچ داده‌ای برای ذخیره وجود ندارد.", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                return;
            }

            var table = mdsDailyPlan.Tables["Tbl_DailyPlan"];

            if (table.GetChanges() == null)
            {
                MessageBox.Show("تغییری برای ذخیره وجود ندارد.", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                return;
            }

            SqlTransaction trn = null;
            try
            {
                Cursor = Cursors.WaitCursor;
                Module1.Openconnection();
                var cn = Module1.cnProductionPlanning;
                trn = cn.BeginTransaction();

                // Ensure SelectCommand uses the same connection
                daDailyPlan.SelectCommand.Connection = cn;

                // Build commands
                var cb = new SqlCommandBuilder(daDailyPlan);
                daDailyPlan.InsertCommand = cb.GetInsertCommand();
                daDailyPlan.UpdateCommand = cb.GetUpdateCommand();
                daDailyPlan.DeleteCommand = cb.GetDeleteCommand();

                // Attach the transaction to commands
                if (daDailyPlan.InsertCommand != null) daDailyPlan.InsertCommand.Transaction = trn;
                if (daDailyPlan.UpdateCommand != null) daDailyPlan.UpdateCommand.Transaction = trn;
                if (daDailyPlan.DeleteCommand != null) daDailyPlan.DeleteCommand.Transaction = trn;

                // Prepare metadata for new/modified rows
                int currentUser = 0;
                int.TryParse(Module_UserAccess.UserCodeTmp, out currentUser);
                DateTime now = DateTime.Now;
                DateTime selectedDate = dtpDate.Value.Date;
                string selectedDatePe = ToShamsiString(selectedDate);

                foreach (DataRow row in table.Rows)
                {
                    if (row.RowState == DataRowState.Added)
                    {
                        // set required fields for new rows
                        if (table.Columns.Contains("PlanDateGr")) row["PlanDateGr"] = selectedDate;
                        if (table.Columns.Contains("PlanDatePe")) row["PlanDatePe"] = selectedDatePe;
                        if (table.Columns.Contains("CreatedAt")) row["CreatedAt"] = now;
                        if (table.Columns.Contains("CreatedBy")) row["CreatedBy"] = currentUser;
                        if (table.Columns.Contains("ModifiedAt")) row["ModifiedAt"] = now;
                        if (table.Columns.Contains("IsDeleted")) row["IsDeleted"] = 0;
                    }
                    else if (row.RowState == DataRowState.Modified)
                    {
                        if (table.Columns.Contains("ModifiedAt")) row["ModifiedAt"] = now;
                    }
                }

                // Perform update
                daDailyPlan.Update(table);
                trn.Commit();

                // Accept changes in dataset
                mdsDailyPlan.AcceptChanges();

                // No pending changes now — disable Save
                try { if (btnSave != null) btnSave.Enabled = false; } catch { }

                MessageBox.Show("تغییرات ذخیره شد.", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
            catch (Exception ex)
            {
                try { trn?.Rollback(); } catch { }
                Logger.SaveError("frmDailyPlan.SaveChangesToDailyPlan", ex.Message);
                MessageBox.Show("ذخیره تغییرات با خطا مواجه شد.", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
            finally
            {
                Module1.Closeconnection();
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Default values handler for grid new rows.
        /// Sets PlanDateGr to the currently selected date (dtpDate) and fills sensible defaults for NOT NULL columns
        /// using DataTable schema (FillSchema was called in LoadPlanForDate).
        /// </summary>
        private void dgvPlan_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                DateTime planDate = dtpDate.Value.Date;
                DateTime now = DateTime.Now;
                string planDatePe = ToShamsiString(planDate);

                // helper to set cell by column name safely
                void SetCellValue(string columnName, object value)
                {
                    if (dgvPlan.Columns.Contains(columnName))
                    {
                        int idx = dgvPlan.Columns[columnName].Index;
                        if (idx >= 0 && idx < e.Row.Cells.Count)
                            e.Row.Cells[idx].Value = value ?? DBNull.Value;
                    }
                }

                // Always set these common defaults (if present)
                SetCellValue("PlanDateGr", planDate);
                SetCellValue("PlanDatePe", planDatePe);
                SetCellValue("CreatedAt", now);
                if (int.TryParse(Module_UserAccess.UserCodeTmp, out var ucode))
                    SetCellValue("CreatedBy", ucode);
                SetCellValue("IsDeleted", 0);

                // Locate DataTable schema that was filled by FillSchema
                DataTable schemaTable = null;
                if (dgvPlan.DataSource is DataView dv)
                    schemaTable = dv.Table;
                else if (mdsDailyPlan.Tables.Contains("Tbl_DailyPlan"))
                    schemaTable = mdsDailyPlan.Tables["Tbl_DailyPlan"];

                if (schemaTable == null) return;

                foreach (DataColumn col in schemaTable.Columns)
                {
                    try
                    {
                        // Skip technical/auto columns
                        if (col.AutoIncrement || col.ReadOnly) continue;
                        string colName = col.ColumnName;

                        // Skip columns we already handled
                        if (string.Equals(colName, "PlanDateGr", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(colName, "PlanDatePe", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(colName, "CreatedAt", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(colName, "CreatedBy", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(colName, "ModifiedAt", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(colName, "IsDeleted", StringComparison.OrdinalIgnoreCase))
                            continue;

                        // Only concerned with NOT NULL columns
                        if (col.AllowDBNull) continue;

                        // If cell already has a value, skip
                        if (dgvPlan.Columns.Contains(colName))
                        {
                            int idx = dgvPlan.Columns[colName].Index;
                            if (idx >= 0 && idx < e.Row.Cells.Count)
                            {
                                var existing = e.Row.Cells[idx].Value;
                                if (existing != null && existing != DBNull.Value) continue;
                            }
                        }

                        // Prefer DataColumn.DefaultValue when provided by schema
                        if (col.DefaultValue != null && col.DefaultValue != DBNull.Value)
                        {
                            SetCellValue(colName, col.DefaultValue);
                            continue;
                        }

                        // Compute sensible default according to CLR type or column name
                        Type t = col.DataType;
                        object defaultValue = null;

                        if (t == typeof(string))
                        {
                            defaultValue = string.Empty;
                        }
                        else if (t == typeof(int) || t == typeof(long) || t == typeof(short) ||
                                 t == typeof(byte) || t == typeof(decimal) || t == typeof(double) ||
                                 t == typeof(float))
                        {
                            defaultValue = 0;
                        }
                        else if (t == typeof(bool))
                        {
                            defaultValue = false;
                        }
                        else if (t == typeof(DateTime))
                        {
                            // Use plan date for plan-related date columns, otherwise use now
                            if (colName.IndexOf("PlanDate", StringComparison.OrdinalIgnoreCase) >= 0 ||
                                colName.IndexOf("Date", StringComparison.OrdinalIgnoreCase) >= 0)
                                defaultValue = planDate;
                            else
                                defaultValue = now;
                        }
                        else if (t == typeof(Guid))
                        {
                            defaultValue = Guid.Empty;
                        }
                        else
                        {
                            // Try to create default for structs, otherwise null
                            try { defaultValue = Activator.CreateInstance(t); } catch { defaultValue = null; }
                        }

                        if (defaultValue != null)
                            SetCellValue(colName, defaultValue);
                    }
                    catch
                    {
                        // ignore individual column failures
                    }
                }
            }
            catch
            {
                // Swallow — defaults are convenience only
            }
        }

        // Enable Save when user adds a row
        private void dgvPlan_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try { if (btnSave != null) btnSave.Enabled = true; } catch { }
        }

        // Enable Save when a cell value changes (edit committed)
        private void dgvPlan_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            try { if (btnSave != null) btnSave.Enabled = true; } catch { }
        }

        // Commit current cell edit so CellValueChanged fires for certain cell types (checkbox, etc.)
        private void dgvPlan_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvPlan.IsCurrentCellDirty)
                    dgvPlan.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            catch { }
        }

        /// <summary>
        /// Convert Gregorian DateTime to Persian (Shamsi) string "yyyyMMdd"
        /// </summary>
        private static string ToShamsiString(DateTime dt)
        {
            var pc = new PersianCalendar();
            int y = pc.GetYear(dt);
            int m = pc.GetMonth(dt);
            int d = pc.GetDayOfMonth(dt);
            return y.ToString("D4") + m.ToString("D2") + d.ToString("D2");
        }

        /// <summary>
        /// Set Persian headers for known columns. Safe if column names don't exist.
        /// </summary>
        private void TryLocalizeGridColumns()
        {
            try
            {
                void SetCol(string col, string header, int? width = null, bool readOnly = false)
                {
                    if (dgvPlan.Columns.Contains(col))
                    {
                        dgvPlan.Columns[col].HeaderText = header;
                        if (width.HasValue) dgvPlan.Columns[col].Width = width.Value;
                        dgvPlan.Columns[col].ReadOnly = readOnly;
                    }
                }

                SetCol("DailyPlanID", "شناسه", 60, true);
                SetCol("ContractNo", "شماره قرارداد", 100);
                SetCol("OrderNo", "شماره سفارش", 100);
                SetCol("OrderQuantity", "مقدار سفارش", 90);
                SetCol("DeliveredQty", "تحویل شده", 90);
                SetCol("BatchQty", "مقدار بچ", 90);
                SetCol("SubBatchQty", "مقدار ساب‌بچ", 90);
                SetCol("Productionquantity", "تولید شده", 90);
                SetCol("ProductionQuantityinSubbatch", "تولید در ساب‌بچ", 110);
                SetCol("ProductCode", "کد محصول", 90);
                SetCol("PartID", "شناسه جزء", 90);
                SetCol("DetailCode", "کد جزء", 90);
                SetCol("DetailName", "نام جزء", 150);
                SetCol("OperationCode", "کد عملیات", 90);
                SetCol("OperationTitle", "عنوان عملیات", 140);
                SetCol("MachineCode", "کد ماشین", 90);
                SetCol("Name", "نام ماشین", 140);
                SetCol("ToolID", "کد قالب/ابزار", 90);
                SetCol("ToolName", "نام قالب/ابزار", 120);
                SetCol("ShiftID", "شیفت", 60);
                SetCol("ShiftName", "نام شیفت", 100);
                SetCol("PlanStartHM", "شروع برنامه (HHMM)", 110);
                SetCol("PlanEndHM", "پایان برنامه (HHMM)", 110);
                SetCol("ProdStartHM", "شروع تولید (HHMM)", 110);
                SetCol("ProdEndHM", "پایان تولید (HHMM)", 110);
                SetCol("OperatorCode", "کد اپراتور", 90);
                SetCol("OperatorName", "نام اپراتور", 120);
                SetCol("StockBalanceQty", "موجودی انبار", 90);
                SetCol("PlanQty", "مقدار برنامه", 90);
                SetCol("ProdQty", "مقدار تولیدی", 90);
                SetCol("AcceptedQty", "قبول شده", 90);
                SetCol("WasteQty", "ضایعات", 90);
                SetCol("WasteReasons", "علت ضایعات", 200);
                SetCol("ProdStopTime", "زمان توقف تولید", 110);
                SetCol("ReworkQty", "مقدار دوباره کاری", 90);
                SetCol("ReworkReasons", "علت دوباره کاری", 200);

                // Make grid editable
                dgvPlan.ReadOnly = false;
                dgvPlan.AllowUserToAddRows = true;
                dgvPlan.AllowUserToDeleteRows = true;

                // Hide technical columns that should not be shown to the user
                var hiddenCols = new[] { "DailyPlanID", "PlanDateGr", "PlanDatePe", "CreatedAt", "CreatedBy", "ModifiedAt", "ConfirmedBy", "IsDeleted" };
                foreach (var col in hiddenCols)
                {
                    if (dgvPlan.Columns.Contains(col))
                        dgvPlan.Columns[col].Visible = false;
                }
            }
            catch
            {
                // Keep grid visible even if localization fails
            }
        }
    }
}