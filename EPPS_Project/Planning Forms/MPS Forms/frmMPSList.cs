using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmMPSList
    {
        public frmMPSList()
        {
            InitializeComponent();
            _txtSearch2.Name = "txtSearch2";
            _cbFilter3.Name = "cbFilter3";
            _cbFilter2.Name = "cbFilter2";
            _cbFilter1.Name = "cbFilter1";
            _txtSearch3.Name = "txtSearch3";
            _txtSearch1.Name = "txtSearch1";
            _dgList.Name = "dgList";
            _cmdFilter.Name = "cmdFilter";
            _cmdExit.Name = "cmdExit";
            _cmdFind.Name = "cmdFind";
            _cmdDelete.Name = "cmdDelete";
            _cmdUpdate.Name = "cmdUpdate";
            _cmdInsert.Name = "cmdInsert";
            _cmdCapametryShow.Name = "cmdCapametryShow";
            _cmdReCapametry.Name = "cmdReCapametry";
        }

        private DataSetConfiguration DataSetConfig = new DataSetConfiguration();
        private DataRow[] FoundRows = null;
        private IEnumerator FoundRowsEnumerator;
        private int mSearchMode = -1;
        private SqlDataAdapter mdaMPS = new SqlDataAdapter();
        private int mFormMode;
        private int I;

        public int FormMode
        {
            get
            {
                return mFormMode;
            }

            set
            {
                mFormMode = value;
            }
        }

        public int SearchMode
        {
            get
            {
                return mSearchMode;
            }

            set
            {
                mSearchMode = value;
            }
        }

        public DataSet dsProductionPlanning
        {
            get
            {
                return DataSetConfig.dsProductionPlanning;
            }
        }

        public enum SearchModeEnum
        {
            SM_FIND,
            SM_FILTER
        }

        private void frmRecordsLists_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdInsert, 0);
            Module1.SetButtonsImage(cmdDelete, 1);
            Module1.SetButtonsImage(cmdUpdate, 2);
            Module1.SetButtonsImage(cmdFilter, 3);
            Module1.SetButtonsImage(cmdFind, 10);
            dgList.Sorted += Module1.DataGridViews_Sorted_EventHandler;
            DataSetConfig.FillDataSet("Tbl_MPSs", "Tbl_MPSs", "Select * From Tbl_MPSs", "MPSCode", "DefinitionYear");
            SetGridColumns(dsProductionPlanning, "Tbl_MPSs");

            // '''''  Set User Access Right  ''''''''''
            cmdInsert.Enabled = Module_UserAccess.HaveAccessToItem(45);
            cmdUpdate.Enabled = Module_UserAccess.HaveAccessToItem(46);
            cmdDelete.Enabled = Module_UserAccess.HaveAccessToItem(47);
            cmdReCapametry.Enabled = Module_UserAccess.HaveAccessToItem(48);
            cmdCapametryShow.Enabled = Module_UserAccess.HaveAccessToItem(49);
            // ''''''''''''''''''''''''''''''''''''''' 
            // ''''''''''''''''''''''''''''''''''''''''
        }

        private void frmRecordsLists_FormClosing(object sender, FormClosingEventArgs e)
        {
            Module1.SaveGridColumnsWidth(Name, 0, dgList);
            dsProductionPlanning.RejectChanges();
            dgList.DataSource = null;
            DataSetConfig = null;
            FoundRows = null;
            FoundRowsEnumerator = null;
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (ReferenceEquals(sender, cmdInsert))
            {
                mFormMode = (int)Module1.FormModeEnum.INSERT_MODE;
            }
            else if (ReferenceEquals(sender, cmdUpdate))
            {
                mFormMode = (int)Module1.FormModeEnum.EDIT_MODE;
            }
            else if (ReferenceEquals(sender, cmdDelete))
            {
                mFormMode = (int)Module1.FormModeEnum.DELETE_MODE;
            }

            if (dgList.Rows.Count == 0 && mFormMode == (int)Module1.FormModeEnum.EDIT_MODE | mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
            {
                MessageBox.Show("رکوردی وجود ندارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                return;
            }

            if (mFormMode == (int)Module1.FormModeEnum.INSERT_MODE)
            {
                DataSetConfig.FillDataSet("Tbl_MPSDetails", "Tbl_MPSDetails", "Select * From Tbl_MPSDetails Where MPSCode=0", "MPSCode", "MPSYear", "MPSDetailPriority");
            }
            else
            {
                DataSetConfig.FillDataSet("Tbl_MPSDetails", "Tbl_MPSDetails", Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select * From Tbl_MPSDetails Where MPSCode=", dgList.CurrentRow.Cells[0].Value), " And MPSYear="), dgList.CurrentRow.Cells[1].Value)), "MPSCode", "MPSYear", "MPSDetailPriority");
                DataSetConfig.FillDataSet("Tbl_MachineCapametry", "Tbl_MachineCapametry", Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select * From Tbl_MachineCapametry Where MPSCode=", dgList.CurrentRow.Cells[0].Value), " And MPSYear="), dgList.CurrentRow.Cells[1].Value)), "MPSCode", "MPSYear", "MPSDetailPriority", "MonthNo", "MachineCode");
                DataSetConfig.FillDataSet("Tbl_PersonnelCapametry", "Tbl_PersonnelCapametry", Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select * From Tbl_PersonnelCapametry Where MPSCode=", dgList.CurrentRow.Cells[0].Value), " And MPSYear="), dgList.CurrentRow.Cells[1].Value)), "MPSCode", "MPSYear", "MPSDetailPriority", "MonthNo");
                DataSetConfig.FillDataSet("Tbl_MaterialCapametry", "Tbl_MaterialCapametry", $"Select * From Tbl_PersonnelCapametry Where MPSCode={dgList.CurrentRow.Cells[0].Value} And MPSYear={dgList.CurrentRow.Cells[1].Value}", "MPSCode", "MPSYear", "MPSDetailPriority", "MonthNo");
                DataSetConfig.FillDataSet("Tbl_CapametryAccessibleTimes", "Tbl_CapametryAccessibleTimes", $"Select * From Tbl_CapametryAccessibleTimes Where MPSCode={dgList.CurrentRow.Cells[0].Value} And MPSYear={dgList.CurrentRow.Cells[1].Value}", "MPSCode");
            }

            {
                var withBlock = My.MyProject.Forms.frmMPS;
                withBlock.ListForm = this;
                withBlock.FormMode = (short)mFormMode;
                if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
                {
                    withBlock.Controls["Panel1"].Controls["cmdSave"].Visible = false;
                    withBlock.Controls["Panel1"].Controls["cmdDelete"].Visible = true;
                }

                withBlock.ShowDialog();
                withBlock.Dispose();
            }
        }

        private void frmRecordsLists_Resize(object sender, EventArgs e)
        {
            tsslSearchMode.Width = (int)Math.Round(0.15d * StatusStrip1.Width);
            ToolStripStatusLabel1.Width = (int)Math.Round(0.14d * StatusStrip1.Width);
            ToolStripStatusLabel2.Width = (int)Math.Round(0.14d * StatusStrip1.Width);
            ToolStripStatusLabel3.Width = (int)Math.Round(0.15d * StatusStrip1.Width);
            ToolStripStatusLabel4.Width = (int)Math.Round(0.15d * StatusStrip1.Width);
            tsslRecNo.Width = (int)Math.Round(0.25d * StatusStrip1.Width);

            // If Not dgList.Tag Is Nothing AndAlso dgList.Tag > -1 Then
            // SetSearchControlsLocation()
            // End If
        }

        private void dgList_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            // If mSearchMode <> -1 Then
            // SetSearchControlsLocation()
            // End If
        }

        private void dgList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int RecordCount = Conversions.ToInteger(Interaction.IIf(dgList.DataSource is DataSet, BindingContext[dsProductionPlanning, "Tbl_MPSs"].Count, dgList.Rows.Count));
            int CurrentRecord = e.RowIndex + 1;
            tsslRecNo.Text = "رکورد  " + CurrentRecord.ToString() + "  از  " + RecordCount.ToString();
        }

        private void dgList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgList.Columns[e.ColumnIndex].Name.Equals("LastUpdate"))
            {
                e.Value = Strings.Mid(Conversions.ToString(e.Value), 1, 4) + "/" + Strings.Mid(Conversions.ToString(e.Value), 5, 2) + "/" + Strings.Mid(Conversions.ToString(e.Value), 7, 2);
            }

            if (e.RowIndex > -1)
            {
                dgList.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
            }
        }

        private void cmdCapametry_Click(object sender, EventArgs e)
        {
            if (dgList.Rows.Count == 0) return;
            if (CalcMPSCapametry(Conversions.ToString(dgList.CurrentRow.Cells["MPSCode"].Value), Conversions.ToString(dgList.CurrentRow.Cells["DefinitionYear"].Value)))
            {
                MessageBox.Show("عملیات ظرفیت سنجی با موفقیت انجام شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
            }
        }

        private void cmdCapametryShow_Click(object sender, EventArgs e)
        {
            if (dgList.Rows.Count > 0 && dgList.CurrentRow is object)
            {
                Cursor = Cursors.WaitCursor;
                Application.DoEvents();
                string Query = "Select  A.ProductCode, A.ProductCode + ' ' + A.ProductName AS ProductName " + 
                               "From    dbo.Tbl_Products A INNER JOIN " + 
                               "        dbo.Tbl_MPSDetails B ON A.ProductCode=B.ProductCode " + 
                               $"Where  B.MPSCode={dgList.CurrentRow.Cells["MPSCode"].Value} AND B.MPSYear= {dgList.CurrentRow.Cells["DefinitionYear"].Value}";


                DataSetConfig.FillDataSet("Tbl_Products", "Tbl_Products", Query);
                DataSetConfig.FillDataSet(
                    "View_CapametryAccessibleTimes", 
                    "Tbl_CapametryAccessibleTimes",
                    $"Select * From View_CapametryAccessibleTimes Where MPSCode = {dgList.CurrentRow.Cells["MPSCode"].Value} And MPSYear={dgList.CurrentRow.Cells["DefinitionYear"].Value}");
                
                var drAllProducts = dsProductionPlanning.Tables["Tbl_Products"].NewRow();
                drAllProducts["ProductCode"] = "-100";
                drAllProducts["ProductName"] = "--- همه محصولات ---";
                dsProductionPlanning.Tables["Tbl_Products"].Rows.InsertAt(drAllProducts, 0);
                dsProductionPlanning.Tables["Tbl_Products"].AcceptChanges();
 
                Query = $"Exec GetMachineCapametry_STP {dgList.CurrentRow.Cells[0].Value},{dgList.CurrentRow.Cells[1].Value}";
                var da = new SqlDataAdapter();
                var cm = new SqlCommand(Query, Module1.cnProductionPlanning);
                da.SelectCommand = cm;
                da.Fill(dsProductionPlanning, "Tbl_MachineCapametry");
                
                Query = $"Exec GetPersonnelCapametry_STP {dgList.CurrentRow.Cells[0].Value},{dgList.CurrentRow.Cells[1].Value}";
                cm.CommandText = Query;
                da.Fill(dsProductionPlanning, "Tbl_PersonnelCapametry");
                // DataSetConfig.FillDataSet("Tbl_PersonnelCapametry", "Tbl_PersonnelCapametry", Query)

                Query = $"Exec GetMaterialCapametry_STP {dgList.CurrentRow.Cells[0].Value}, {dgList.CurrentRow.Cells[1].Value}";
                cm.CommandText = Query;
                da.Fill(dsProductionPlanning, "Tbl_MaterialCapametry");
                // DataSetConfig.FillDataSet("Tbl_MaterialCapametry", "Tbl_MaterialCapametry", Query)

                cm.CommandText = $"SELECT * FROM Tbl_CapametryAccessibleTimes WHERE MPSCode = {dgList.CurrentRow.Cells[0].Value}";
                da.Fill(dsProductionPlanning, "Tbl_CapametryAccessibleTimes");

                {
                    var withBlock = new frmMPSCapametryShow();
                    withBlock.MdiParent = MdiParent;
                    withBlock.mdsCapametryShow = dsProductionPlanning;
                    withBlock.MPSCode = Conversions.ToShort(dgList.CurrentRow.Cells["MPSCode"].Value);
                    withBlock.MPSYear = Conversions.ToShort(dgList.CurrentRow.Cells["DefinitionYear"].Value);
                    withBlock.PersonnelCapametryCalendar = Conversions.ToInteger(dgList.CurrentRow.Cells["PersonnelCapametryCalendarCode"].Value);
                    withBlock.Show();
                }

                Cursor = Cursors.Default;
                Application.DoEvents();
            }
            else
            {
                MessageBox.Show("رکوردی وجود ندارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                return;
            }
        }

        private void cmdFilter_Click(object sender, EventArgs e)
        {
            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgList.Tag, -1, false)))
            {
                if (ReferenceEquals(sender, cmdFilter))
                {
                    dgList.Tag = SearchModeEnum.SM_FILTER;
                    tsslSearchMode.Text = "فیلتر اطلاعات";
                    SetFilterCombosLocation();
                }
                else
                {
                    dgList.Tag = SearchModeEnum.SM_FIND;
                    tsslSearchMode.Text = "جستجوی اطلاعات";
                    SetSearchControlsLocation();
                }

                dgList.ColumnHeadersHeight = 40;
                dgList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft;
            }
            else
            {
                if (ReferenceEquals(sender, cmdFilter))
                {
                    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgList.Tag, SearchModeEnum.SM_FILTER, false)))
                    {
                        for (I = 1; I <= 3; I++)
                            Controls["Panel1"].Controls["cbFilter" + I].Visible = false;
                        dgList.ColumnHeadersHeight = 30;
                        dgList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dgList.Tag = -1;
                        tsslSearchMode.Text = "نمایش کلی";
                    }
                    else if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgList.Tag, SearchModeEnum.SM_FIND, false)))
                    {
                        dgList.Tag = SearchModeEnum.SM_FILTER;
                        tsslSearchMode.Text = "فیلتر اطلاعات";
                        SetFilterCombosLocation();
                    }
                }
                else if (ReferenceEquals(sender, cmdFind))
                {
                    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgList.Tag, SearchModeEnum.SM_FIND, false)))
                    {
                        for (I = 1; I <= 3; I++)
                            Controls["Panel1"].Controls["txtSearch" + I].Visible = false;
                        dgList.ColumnHeadersHeight = 30;
                        dgList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dgList.Tag = -1;
                        tsslSearchMode.Text = "نمایش کلی";
                    }
                    else if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(dgList.Tag, SearchModeEnum.SM_FILTER, false)))
                    {
                        dgList.Tag = SearchModeEnum.SM_FIND;
                        tsslSearchMode.Text = "جستجوی اطلاعات";
                        SetSearchControlsLocation();
                    }
                }

                if (dgList.DataSource is DataView)
                {
                    ((DataView)dgList.DataSource).RowFilter = Constants.vbNullString;
                }
            }
        }

        private void cbFilters_DropDown(object sender, EventArgs e)
        {
            ComboBox CurrentCombo;
            CurrentCombo = (ComboBox)sender;
            CurrentCombo.Items.Clear();
            CurrentCombo.Items.Add("---< همه >---");
            for (short ItemCounter = 0, loopTo = (short)(dgList.Rows.Count - 1); ItemCounter <= loopTo; ItemCounter++)
            {
                if (!CurrentCombo.Items.Contains(dgList.Rows[(int)ItemCounter].Tag.ToString()))
                {
                    CurrentCombo.Items.Add(dgList.Rows[(int)ItemCounter].Tag.ToString());
                }
            }
        }

        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterValue = Constants.vbNullString;
            ComboBox CurrentCombo = (ComboBox)sender;
            ComboBox CurrentVisibleCombo;
            string Control;
            if (CurrentCombo.Text == "---< همه >---")
            {
                CurrentCombo.SelectedIndex = -1;
            }

            for (short ComboCounter = 1; ComboCounter <= 3; ComboCounter++)
            {
                Control = "cbFilter" + ComboCounter;
                if (Controls["Panel1"].Controls[Control].Visible)
                {
                    CurrentVisibleCombo = (ComboBox)Controls["Panel1"].Controls[Control];
                    if (CurrentVisibleCombo.SelectedIndex > -1)
                    {
                        FilterValue = Conversions.ToString(Interaction.IIf(string.IsNullOrEmpty(FilterValue), Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Convert(", Controls["Panel1"].Controls[Control].Tag), ",System.String) ='"), Controls["Panel1"].Controls[Control].Text), "'"), Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(FilterValue + " And Convert(", Controls["Panel1"].Controls[Control].Tag), ",System.String) ='"), Controls["Panel1"].Controls[Control].Text), "'")));

                    }
                }
            }

            CurrentCombo = null;
            CurrentVisibleCombo = null;
            DataView GridDataView;
            if (dgList.DataSource is DataView)
            {
                GridDataView = (DataView)dgList.DataSource;
                GridDataView.RowFilter = FilterValue;
            }
            else
            {
                GridDataView = dsProductionPlanning.Tables[dgList.DataMember].DefaultView;
                GridDataView.RowFilter = FilterValue;
                SetGridColumns(GridDataView, "");
            }
        }

        private void txtSearchs_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox CurrentTextBox = (TextBox)sender;
            if (Strings.Asc(e.KeyChar) == (int)Keys.Back)
            {
                if (string.IsNullOrEmpty(CurrentTextBox.Text))
                {
                    string PreControlName = "txtSearch";
                    PreControlName = Conversions.ToString(Interaction.IIf(Conversions.ToInteger(Strings.Right(CurrentTextBox.Name, 1)) < 3, PreControlName + (Conversions.ToInteger(Strings.Right(CurrentTextBox.Name, 1)) + 1).ToString(), Constants.vbNullString));
                    if (!string.IsNullOrEmpty(PreControlName))
                    {
                        if (Controls["Panel1"].Controls[PreControlName].Visible)
                        {
                            Controls["Panel1"].Controls[PreControlName].Focus();
                        }
                    }
                }
            }
            else if (Strings.Asc(e.KeyChar) == (int)Keys.Enter)
            {
                string Control;
                TextBox CurrentVisibleTextBox;
                string FilterValue = Constants.vbNullString;
                for (short TextBoxCounter = 1; TextBoxCounter <= 3; TextBoxCounter++)
                {
                    Control = "txtSearch" + TextBoxCounter;
                    if (Controls["Panel1"].Controls[Control].Visible)
                    {
                        CurrentVisibleTextBox = (TextBox)Controls["Panel1"].Controls[Control];
                        if (!string.IsNullOrEmpty(CurrentVisibleTextBox.Text))
                        {
                            FilterValue = Conversions.ToString(Interaction.IIf(string.IsNullOrEmpty(FilterValue), Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Convert(", Controls["Panel1"].Controls[Control].Tag), ",System.String) LIKE '%"), Controls["Panel1"].Controls[Control].Text), "%'"), Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(FilterValue + " And Convert(", Controls["Panel1"].Controls[Control].Tag), ",System.String) LIKE '%"), Controls["Panel1"].Controls[Control].Text), "%'")));

                        }
                    }
                }

                CurrentVisibleTextBox = null;
                string TableName;
                if (string.IsNullOrEmpty(FilterValue))
                {
                    FoundRows = null;
                }
                else if (FoundRows is null)
                {
                    if (dgList.DataSource is DataView)
                    {
                        DataView GridDataView = (DataView)dgList.DataSource;
                        TableName = GridDataView.Table.TableName;
                        SetGridColumns(dsProductionPlanning, TableName);
                    }
                    else
                    {
                        TableName = dgList.DataMember;
                    }

                    FoundRows = dsProductionPlanning.Tables[TableName].Select(FilterValue);
                    FoundRowsEnumerator = FoundRows.GetEnumerator();
                    if (FoundRowsEnumerator.MoveNext())
                    {
                        I = dsProductionPlanning.Tables[TableName].Rows.IndexOf((DataRow)FoundRowsEnumerator.Current);
                        BindingContext[dsProductionPlanning, TableName].Position = I;
                    }
                    else
                    {
                        MessageBox.Show("موردی یافت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                        FoundRows = null;
                    }
                }
                else
                {
                    TableName = dgList.DataMember;
                    if (FoundRowsEnumerator.MoveNext())
                    {
                        I = dsProductionPlanning.Tables[TableName].Rows.IndexOf((DataRow)FoundRowsEnumerator.Current);
                        BindingContext[dsProductionPlanning, TableName].Position = I;
                    }
                    else
                    {
                        MessageBox.Show("مورد دیگری وجود ندارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                        FoundRows = null;
                    }
                }
            }

            CurrentTextBox = null;
        }

        private void SetSearchControlsLocation()
        {
            short I, J, ControlIndex;
            int TempWidth;
            string Control;
            for (I = 1; I <= 3; I++)
            {
                Controls["Panel1"].Controls["cbFilter" + I].Visible = false;
                Controls["Panel1"].Controls["cbFilter" + I].Tag = Constants.vbNullString;
            }

            ControlIndex = 1;
            for (I = (short)dgList.Columns.Count; I >= 1; I += -1)
            {
                if (dgList.Columns[I - 1].Visible)
                {
                    Control = "txtSearch" + ControlIndex;
                    TempWidth = dgList.Width;
                    var loopTo = (short)(I - 1);
                    for (J = 0; J <= loopTo; J++)
                    {
                        if (dgList.Columns[J].Visible)
                        {
                            TempWidth = TempWidth - dgList.Columns[J].Width;
                        }
                    }

                    {
                        var withBlock = Controls["Panel1"];
                        withBlock.Controls[Control].Text = Constants.vbNullString;
                        withBlock.Controls[Control].Top = dgList.Location.Y;
                        withBlock.Controls[Control].Left = TempWidth - dgList.RowHeadersWidth + 4;
                        withBlock.Controls[Control].Width = dgList.Columns[I - 1].HeaderCell.OwningColumn.Width - 2;
                        withBlock.Controls[Control].Visible = true;
                        withBlock.Controls[Control].Tag = dgList.Columns[I - 1].Name;
                    }

                    ControlIndex = (short)(ControlIndex + 1);
                }
            }

            if (dgList.DataSource is DataView)
            {
                DataView GridDataView = (DataView)dgList.DataSource;
                SetGridColumns(dsProductionPlanning, GridDataView.Table.TableName);
            }
        }

        private void SetFilterCombosLocation()
        {
            short I, J, ControlIndex;
            int TempWidth;
            string Control;
            for (I = 1; I <= 3; I++)
            {
                Controls["Panel1"].Controls["txtSearch" + I].Visible = false;
                Controls["Panel1"].Controls["txtSearch" + I].Tag = Constants.vbNullString;
            }

            ControlIndex = 1;
            for (I = (short)dgList.Columns.Count; I >= 1; I += -1)
            {
                if (dgList.Columns[I - 1].Visible)
                {
                    Control = "cbFilter" + ControlIndex;
                    TempWidth = dgList.Width;
                    var loopTo = (short)(I - 1);
                    for (J = 0; J <= loopTo; J++)
                    {
                        if (dgList.Columns[J].Visible)
                        {
                            TempWidth = TempWidth - dgList.Columns[J].Width;
                        }
                    }

                    {
                        var withBlock = Controls["Panel1"];
                        withBlock.Controls[Control].Text = Constants.vbNullString;
                        withBlock.Controls[Control].Top = dgList.Location.Y;
                        withBlock.Controls[Control].Left = TempWidth - dgList.RowHeadersWidth + 6;
                        withBlock.Controls[Control].Width = dgList.Columns[I - 1].HeaderCell.OwningColumn.Width - 2;
                        withBlock.Controls[Control].Visible = true;
                        withBlock.Controls[Control].Tag = dgList.Columns[I - 1].Name;
                    }

                    ControlIndex = (short)(ControlIndex + 1);
                }
            }

            if (!(dgList.DataSource is DataView))
            {
                SetGridColumns(dsProductionPlanning.Tables[dgList.DataMember].DefaultView, "");
            }
        }

        private void SetGridColumns(object DataSource, string Table = Constants.vbNullString)
        {
            {
                var withBlock = dgList;
                if (DataSource is DataView)
                {
                    withBlock.DataSource = DataSource;
                }
                else
                {
                    withBlock.DataSource = DataSource;
                    withBlock.DataMember = Table;
                }

                withBlock.Columns[0].HeaderText = "سریال MPS";
                withBlock.Columns[1].HeaderText = "سال MPS";
                withBlock.Columns[2].Visible = false;
                withBlock.Columns[3].HeaderText = "تاریخ آخرین تغییر";
                withBlock.Columns[4].Visible = false;
                withBlock.Columns[5].Visible = false;
            }

            Module1.SetGridColumnsWidth(Name, 0, dgList);
        }

        public DataRow GetRow()
        {
            object crRow;
            var drFind = new DataRow[1];
            drFind = dsProductionPlanning.Tables["Tbl_MPSs"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("MPSCode=", dgList.CurrentRow.Cells[0].Value), " And DefinitionYear="), dgList.CurrentRow.Cells[1].Value)));
            crRow = drFind[0];
            return (DataRow)crRow;
        }

        private void Prepare_To_Show_TableRecordList(string TN, string FC)
        {
            SetGridColumns(dsProductionPlanning, TN);
            Text = FC;
        }

        private bool CalcMPSCapametry(string MPSCode, string MPSYear)
        {
            if (dgList.Rows.Count > 0 && dgList.CurrentRow is object)
            {
                using (var cn = new SqlConnection(Module1.PlanningCnnStr))
                {
                    var dtMPSDeatils = new DataTable();
                    var cmCapametry = new SqlCommand("", cn);
                    SqlTransaction trnCapametry = null;
                    string mDefualtTreeCode;
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        cn.Open();
                        trnCapametry = cn.BeginTransaction();
                        cmCapametry.Transaction = trnCapametry;

                        // حذف مشخصات ظرفیت سنجی ماشین آلات
                        cmCapametry.CommandText = "Delete From Tbl_MachineCapametry Where MPSCode=" + MPSCode + " And MPSYear=" + MPSYear;
                        cmCapametry.ExecuteNonQuery();
                        // حذف مشخصات ظرفیت سنجی پرسنل
                        cmCapametry.CommandText = "Delete From Tbl_PersonnelCapametry Where MPSCode=" + MPSCode + " And MPSYear=" + MPSYear;
                        cmCapametry.ExecuteNonQuery();
                        // حذف مشخصات ظرفیت سنجی مواد اولیه
                        //cmCapametry.CommandText = "Delete From Tbl_MaterialCapametry Where MPSCode=" + MPSCode + " And MPSYear=" + MPSYear;
                        //cmCapametry.ExecuteNonQuery();
                        // حذف مشخصات زمان در دسترس محاسبه شدۀ قبلی
                        cmCapametry.CommandText = "Delete From Tbl_CapametryAccessibleTimes Where MPSCode=" + MPSCode + " And MPSYear=" + MPSYear;
                        cmCapametry.ExecuteNonQuery();

                        // فراخوانی جزئیات برنامه ریزی انتخاب شده
                        {
                            var withBlock = new SqlDataAdapter("Select * From Tbl_MPSDetails Where MPSCode=" + MPSCode + " And MPSYear=" + MPSYear, cn);
                            withBlock.SelectCommand.Transaction = trnCapametry;
                            withBlock.Fill(dtMPSDeatils);
                        }

                        if (dtMPSDeatils.Rows.Count == 0)
                        {
                            trnCapametry.Rollback();
                            Cursor = Cursors.Default;
                            MessageBox.Show("جزئیات برنامه ریزی یافت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                            return false;
                            //break;
                        }

                        for (short MonthCounter = 1; MonthCounter <= 12; MonthCounter++)
                        {
                            for (short DetailCounter = 0, loopTo = (short)(dtMPSDeatils.Rows.Count - 1); DetailCounter <= loopTo; DetailCounter++)
                            {
                                cmCapametry.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Select TreeCode From Tbl_ProductTree Where DefualtTree = 1 And ProductCode = '", dtMPSDeatils.Rows[DetailCounter]["ProductCode"]), "'"));
                                var drCapametry = cmCapametry.ExecuteReader();
                                if (drCapametry.Read())
                                {
                                    mDefualtTreeCode = drCapametry["TreeCode"].ToString();
                                    drCapametry.Close();
                                }
                                else
                                {
                                    drCapametry.Close();
                                    trnCapametry.Rollback();
                                    Cursor = Cursors.Default;
                                    MessageBox.Show(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(" درخت پیش فرض، انتخاب نشده است ", dtMPSDeatils.Rows[DetailCounter]["ProductCode"]), " برای محصول ")), Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                                    break;
                                }

                                // ظرفیت سنجی ماشین آلات
                                CalcMachineCapametry(trnCapametry, Conversions.ToString(dtMPSDeatils.Rows[DetailCounter]["ProductCode"]), Conversions.ToInteger(mDefualtTreeCode), Conversions.ToShort(dtMPSDeatils.Rows[DetailCounter]["MPSDetailPriority"]), MonthCounter, Conversions.ToInteger(dtMPSDeatils.Rows[DetailCounter][MonthCounter + 4]));
                                // ظرفیت سنجی پرسنل
                                CalcPersonnelCapametry(trnCapametry, Conversions.ToInteger(mDefualtTreeCode), Conversions.ToShort(dtMPSDeatils.Rows[DetailCounter]["MPSDetailPriority"]), MonthCounter, Conversions.ToInteger(dtMPSDeatils.Rows[DetailCounter][MonthCounter + 4]));
                                // ظرفیت سنجی مواد اولیه
                                //CalcMaterialCapametry(trnCapametry, Conversions.ToString(dtMPSDeatils.Rows[DetailCounter]["ProductCode"]), Conversions.ToInteger(mDefualtTreeCode), Conversions.ToShort(dtMPSDeatils.Rows[DetailCounter]["MPSDetailPriority"]), MonthCounter, Conversions.ToInteger(dtMPSDeatils.Rows[DetailCounter][MonthCounter + 4]));
                                                                 
                            }
                        }

                        if (!CalcAccessibleTimes(trnCapametry, MPSCode, MPSYear))
                        {
                            trnCapametry.Rollback();
                            return false;
                        }

                        // ظرفیت سنجی مواد اولیه
                        CalcMaterialCapametry(MPSCode, MPSYear);

                        var drLastUpdate = dsProductionPlanning.Tables["Tbl_MPSs"].Select("MPSCode=" + MPSCode + " And DefinitionYear=" + MPSYear);
                        string mLastUpdate = Module1.mServerShamsiDate;
                        drLastUpdate[0]["LastUpdate"] = mLastUpdate;
                        cmCapametry.CommandText = "Update Tbl_MPSs Set LastUpdate = " + mLastUpdate + " Where MPSCode=" + MPSCode + " And DefinitionYear=" + MPSYear;
                        cmCapametry.ExecuteNonQuery();
                        trnCapametry.Commit();
                        trnCapametry = null;
                        dsProductionPlanning.AcceptChanges();
                        Cursor = Cursors.Default;
                        return true;
                    }
                    catch (Exception objEx)
                    {
                        Cursor = Cursors.Default;
                        dsProductionPlanning.RejectChanges();
                        if (trnCapametry != null)
                        {
                            trnCapametry.Dispose(); // .Rollback();
                        }

                        Logger.SaveError(Name + ".CalcMPSCapametry", objEx.Message);
                        MessageBox.Show("محاسبۀ ظرفیت سنجی با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        return false;
                    }
                    finally
                    {
                        if (cn.State == ConnectionState.Open)
                            cn.Close();
                        dtMPSDeatils = null;
                        trnCapametry = null;
                    }
                }
            }
            else
            {
                MessageBox.Show("رکوردی وجود ندارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                //return false;
            }

            return false;
        }

        private void CalcMachineCapametry(SqlTransaction Trn, string ProductCode, int TreeCode, short DetailPriority, short MonthNo, int MonthCapacity)
        {
            var dtCapametry = new DataTable();
            var cmCapametry = new SqlCommand("", Trn.Connection);
            var daCapametry = new SqlDataAdapter("Select   D.MachineCode,D.TimeType," + "         SUM(D.OneExecutionTime * B.ParentQuantity) AS OneExecutionTime " + "From     dbo.Tbl_ProductTree A INNER JOIN dbo.Tbl_ProductTreeDetails B ON A.TreeCode=B.TreeCode INNER JOIN " + "         dbo.Tbl_ProductOPCs C ON B.TreeCode=C.TreeCode AND B.DetailCode=C.DetailCode           INNER JOIN " + "         dbo.Tbl_ProductOPCsExecutorMachines D ON C.TreeCode=D.TreeCode AND C.OperationCode=D.OperationCode " + "Where    A.TreeCode=" + TreeCode + " And A.ProductCode='" + ProductCode + "' And C.ExecutionMethod = 1 And D.MachinePriority = 1" + "Group By D.MachineCode, D.TimeType", Trn.Connection);





            daCapametry.SelectCommand.Transaction = Trn;
            daCapametry.Fill(dtCapametry);
            cmCapametry.Transaction = Trn;
            var loopTo = dtCapametry.Rows.Count - 1;
            for (I = 0; I <= loopTo; I++)
            {
                double mTime = Module1.GetAnyTime_TO_Hour(Conversions.ToShort(dtCapametry.Rows[I]["TimeType"]), Conversions.ToDouble(Operators.MultiplyObject(MonthCapacity, dtCapametry.Rows[I]["OneExecutionTime"])), "0:0");
                double SumOld_mTime;
                int RecordNo;
                RecordNo = 0;
                cmCapametry.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(" SELECT COUNT(*) From Tbl_MachineCapametry  " + " WHERE MPSCode=", dgList.CurrentRow.Cells["MPSCode"].Value), " AND MPSYear="), dgList.CurrentRow.Cells["DefinitionYear"].Value), " AND MPSDetailPriority="), DetailPriority), " AND MonthNo="), MonthNo), " AND MachineCode='"), dtCapametry.Rows[I]["MachineCode"]), "'"));




                RecordNo = Conversions.ToInteger(cmCapametry.ExecuteScalar().ToString());
                if (RecordNo > 0)
                {
                    cmCapametry.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(" SELECT RequirementTime From Tbl_MachineCapametry  " + " WHERE MPSCode=", dgList.CurrentRow.Cells["MPSCode"].Value), " AND MPSYear="), dgList.CurrentRow.Cells["DefinitionYear"].Value), " AND MPSDetailPriority="), DetailPriority), " AND MonthNo="), MonthNo), " AND MachineCode='"), dtCapametry.Rows[I]["MachineCode"]), "'"));




                    SumOld_mTime = Conversions.ToDouble(cmCapametry.ExecuteScalar().ToString());
                    mTime = mTime + SumOld_mTime;
                    cmCapametry.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Update Tbl_MachineCapametry set RequirementTime=" + mTime.ToString() + " WHERE MPSCode=", dgList.CurrentRow.Cells["MPSCode"].Value), " AND MPSYear="), dgList.CurrentRow.Cells["DefinitionYear"].Value), " AND MPSDetailPriority="), DetailPriority), " AND MonthNo="), MonthNo), " AND MachineCode='"), dtCapametry.Rows[I]["MachineCode"]), "'"));




                }
                else
                {
                    cmCapametry.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Insert Into Tbl_MachineCapametry(MPSCode,MPSYear,MPSDetailPriority,MonthNo,MachineCode,RequirementTime)" + "Values(", dgList.CurrentRow.Cells["MPSCode"].Value), ","), dgList.CurrentRow.Cells["DefinitionYear"].Value), ","), DetailPriority), ","), MonthNo), ",'"), dtCapametry.Rows[I]["MachineCode"]), "',"), mTime), ")"));
                }

                cmCapametry.ExecuteNonQuery();
            }
        }

        private void CalcPersonnelCapametry(SqlTransaction Trn, int TreeCode, short DetailPriority, short MonthNo, int MonthCapacity)
        {
            var dtCapametry = new DataTable();
            var cmCapametry = new SqlCommand("", Trn.Connection);
            var TempExecTime = default(double);
            var daCapametry = new SqlDataAdapter("Select(D.OneExecutionTime * B.ParentQuantity * D.Operators) AS OneExecutionTime,D.TimeType " + "From   Tbl_ProductTree A INNER JOIN " + "       Tbl_ProductTreeDetails B ON A.TreeCode=B.TreeCode INNER JOIN " + "       Tbl_ProductOPCs C ON B.TreeCode=C.TreeCode AND B.DetailCode=C.DetailCode INNER JOIN " + "       Tbl_ProductOPCsExecutorMachines D ON C.TreeCode=D.TreeCode AND C.OperationCode=D.OperationCode LEFT OUTER JOIN " + "       Tbl_Machines E ON D.MachineCode=E.Code " + "Where  D.MachinePriority = 1 And A.TreeCode=" + TreeCode, Trn.Connection);





            daCapametry.SelectCommand.Transaction = Trn;
            daCapametry.Fill(dtCapametry);
            cmCapametry.Transaction = Trn;
            var loopTo = dtCapametry.Rows.Count - 1;
            for (I = 0; I <= loopTo; I++)
                TempExecTime += Module1.GetAnyTime_TO_Hour(Conversions.ToShort(dtCapametry.Rows[I]["TimeType"]), Conversions.ToDouble(Operators.MultiplyObject(MonthCapacity, dtCapametry.Rows[I]["OneExecutionTime"])), "0:0");

            // cmCapametry.CommandText = "Select RequirementTime From Tbl_PersonnelCapametry Where MPSCode=" & dgList.CurrentRow.Cells("MPSCode").Value & _
            // " And MPSYear=" & dgList.CurrentRow.Cells("DefinitionYear").Value & _
            // " And MPSDetailPriority=" & DetailPriority & _
            // " And MonthNo=" & MonthNo

            // Dim drCheckExists As SqlDataReader = cmCapametry.ExecuteReader()

            // If drCheckExists.Read() > 0 Then
            // Dim mExistsTime As Double = drCheckExists("RequirementTime")
            // drCheckExists.Close()

            // cmCapametry.CommandText = "Update Tbl_PersonnelCapametry Set RequirementTime = " & (mExistsTime + TempExecTime) & _
            // " Where MPSCode=" & dgList.CurrentRow.Cells("MPSCode").Value & _
            // "   And MPSYear=" & dgList.CurrentRow.Cells("DefinitionYear").Value & _
            // "   And MPSDetailPriority=" & DetailPriority & _
            // "   And MonthNo=" & MonthNo
            // cmCapametry.ExecuteNonQuery()
            // Else
            // drCheckExists.Close()
            // cmCapametry.CommandText = "Insert Into Tbl_PersonnelCapametry(MPSCode,MPSYear,MPSDetailPriority,MonthNo,RequirementTime)" & _
            // "Values(" & dgList.CurrentRow.Cells("MPSCode").Value & "," & dgList.CurrentRow.Cells("DefinitionYear").Value & "," & DetailPriority & "," & MonthNo & "," & TempExecTime & ")"
            // cmCapametry.ExecuteNonQuery()
            // End If

            cmCapametry.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Insert Into Tbl_PersonnelCapametry(MPSCode,MPSYear,MPSDetailPriority,MonthNo,RequirementTime)" + "Values(", dgList.CurrentRow.Cells["MPSCode"].Value), ","), dgList.CurrentRow.Cells["DefinitionYear"].Value), ","), DetailPriority), ","), MonthNo), ","), TempExecTime), ")"));
            cmCapametry.ExecuteNonQuery();
        }

        private void CalcMaterialCapametry(string MPSCode, string MPSYear)
        {
            try
            {
                var cn = new SqlConnection(Module1.PlanningCnnStr);
                var cm = new SqlCommand($"EXEC [dbo].[SP_CalculateMaterialAP]  @MPSCode = {MPSCode}, @MPSYear = {MPSYear}", cn);
                cn.Open();
                cm.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception excep)
            {

                Logger.LogException($"CalcMaterialCapametry: MPSCode = {MPSCode}, MPSYear = {MPSYear}", excep);
            }
            
           
        }

        private bool CalcAccessibleTimes(SqlTransaction Trn, string mMPSCode, string mMPSYear)
        {
            try
            {
                // محاسبۀ زمان در دسترس برای ماشین ها
                CalcMachinesAccessibleTime(Trn, mMPSCode, mMPSYear);
                // محاسبۀ زمان در دسترس پرسنل
                CalcPersonnelaAccessibleTime(Trn, mMPSCode, mMPSYear, Conversions.ToString(dgList.CurrentRow.Cells["PersonnelCapametryCalendarCode"].Value));
                return true;
            }
            catch (Exception objEx)
            {
                Cursor = Cursors.Default;
                Logger.SaveError(Name + ".CalcAccessibleTimes", objEx.Message);
                MessageBox.Show("محاسبۀ زمان های در دسترس با مشکل مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                return false;
            }
        }

        private void CalcMachinesAccessibleTime(SqlTransaction Trn, string mMPSCode, string mMPSYear)
        {
            var dtMachinesTime = new DataTable();
            var CalcedCalendars = new Dictionary<int, string>();
            var cmMachineAccessibleTime = new SqlCommand("", Trn.Connection);
            try
            {
                {
                    var withBlock = new SqlDataAdapter("Select Distinct A.MachineCode, B.CalendarCode From dbo.Tbl_MachineCapametry A Inner Join Tbl_Machines B ON A.MachineCode = B.Code Where MPSCode=" + mMPSCode + " And MPSYear=" + mMPSYear + " Order By MachineCode", Trn.Connection);
                    withBlock.SelectCommand.Transaction = Trn;
                    withBlock.Fill(dtMachinesTime);
                }

                cmMachineAccessibleTime.Transaction = Trn;
                foreach (DataRow dr in dtMachinesTime.Rows)
                {
                    string mMonthTimes = Constants.vbNullString;
                    if (!CalcedCalendars.ContainsKey(Conversions.ToInteger(dr["CalendarCode"].ToString())))
                    {
                        for (short MonthNo = 1; MonthNo <= 12; MonthNo++)
                        {
                            double MachineTimeInMonth = GetCalendarAccessibleTimeInMonth(Conversions.ToString(dr["CalendarCode"]), mMPSYear, MonthNo);
                            if (string.IsNullOrEmpty(mMonthTimes))
                            {
                                mMonthTimes = MachineTimeInMonth.ToString();
                            }
                            else
                            {
                                mMonthTimes += ";" + MachineTimeInMonth.ToString();
                            }
                        }

                        CalcedCalendars.Add(Conversions.ToInteger(dr["CalendarCode"].ToString()), mMonthTimes);
                    }
                    else
                    {
                        var mEnum = CalcedCalendars.GetEnumerator();
                        while (mEnum.MoveNext())
                        {
                            if (mEnum.Current.Key.Equals(Conversions.ToInteger(dr["CalendarCode"].ToString())))
                            {
                                mMonthTimes = mEnum.Current.Value;
                                break;
                            }
                        }
                    }

                    var SplitedStr = mMonthTimes.Split(';');
                    for (short I = 0, loopTo = (short)(SplitedStr.Length - 1); I <= loopTo; I++)
                    {
                        cmMachineAccessibleTime.CommandText = $"Insert Into Tbl_CapametryAccessibleTimes(MPSCode,MPSYear,MonthNo,MachineCode,AccessibleTime) Values({mMPSCode}, {mMPSYear}, {I + 1},'{dr["MachineCode"]}', {SplitedStr[I]})";
                        cmMachineAccessibleTime.ExecuteNonQuery();
                    }
                }

                CalcedCalendars = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CalcPersonnelaAccessibleTime(SqlTransaction Trn, string mMPSCode, string mMPSYear, string mCalendarCode)
        {
            double MonthTime;
            int PersonnelQty;
            var cmPersonnelTime = new SqlCommand("", Trn.Connection);
            cmPersonnelTime.Transaction = Trn;
            cmPersonnelTime.CommandText = "Select IsNull(Count(*),0) From Tbl_Operators Where WorkingStatus = 1";
            PersonnelQty = Conversions.ToInteger(cmPersonnelTime.ExecuteScalar().ToString());

            // بدست آوردن زمان در دسترس هر ماه
            for (short MonthNo = 1; MonthNo <= 12; MonthNo++)
            {
                MonthTime = GetCalendarAccessibleTimeInMonth(mCalendarCode, mMPSYear, MonthNo);
                MonthTime *= PersonnelQty;
                cmPersonnelTime.CommandText = "Insert Into Tbl_CapametryAccessibleTimes(MPSCode,MPSYear,MonthNo,MachineCode,AccessibleTime) Values(" + mMPSCode + "," + mMPSYear + "," + MonthNo + ",'-1'," + MonthTime + ")";
                cmPersonnelTime.ExecuteNonQuery();
            }
        }

        private double GetCalendarAccessibleTimeInMonth(string CalendarCode, string MPSYear, short MonthNo)
        {
            double AccessibleTime = 0d;
            double TempTime = 0d;
            string ShamsiDate;
            short LastDay = Conversions.ToShort(Interaction.IIf(MonthNo <= 6, 31, 30));
            if (MonthNo == 12)
            {
                LastDay = Conversions.ToShort(Interaction.IIf(FarsiDateFunctions.IsKabiseh(Conversions.ToInteger(MPSYear)), 30, 29));
            }

            for (short DayCounter = 1, loopTo = LastDay; DayCounter <= loopTo; DayCounter++)
            {
                // تولید تاریخ شمسی
                ShamsiDate = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(MPSYear, Interaction.IIf(MonthNo < 10, "0" + MonthNo, MonthNo)), Interaction.IIf(DayCounter < 10, "0" + DayCounter, DayCounter)));
                // در صورتیکه تاریخ بدست آمده روز خاص باشد زمان در دسترس روز و در غیر اینصورت صفر باز می گرداند
                TempTime = GetParticularDayAccessibleTime(CalendarCode, ShamsiDate);
                if (TempTime == 0.0d) // تاریخ بدست آمده روز خاص نیست
                {
                    AccessibleTime += GetDayAccessibleTime(CalendarCode, Module1.GetDayNo(ShamsiDate).ToString());
                }
                else
                {
                    AccessibleTime += TempTime;
                }
            }

            return AccessibleTime;
        }

        /// <summary>
    /// این تابع زمان در دسترس یک روز خاص را باز می گرداند
    /// </summary>
        private double GetParticularDayAccessibleTime(string CalendarCode, string ShamsiDate)
        {
            double ParticulareTime;
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                {
                    var withBlock = new SqlCommand("Select dbo.GetParticulareTime(" + CalendarCode + ",'" + ShamsiDate + "')", cn);
                    ParticulareTime = Conversions.ToDouble(withBlock.ExecuteScalar().ToString());
                }

                cn.Close();
            }

            return ParticulareTime;
        }

        /// <summary>
    /// این تابع زمان در دسترس یک روز را باز می گرداند
    /// </summary>
        private double GetDayAccessibleTime(string CalendarCode, string DayNo)
        {
            double AccessibleTime;
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                {
                    var withBlock = new SqlCommand("Select dbo.GetDayTime(" + CalendarCode + "," + DayNo + ")", cn);
                    var result = withBlock.ExecuteScalar();
                    AccessibleTime = result == DBNull.Value ? 0: Conversions.ToDouble(result.ToString());
                }

                cn.Close();
            }

            return AccessibleTime;
        }
    }
}