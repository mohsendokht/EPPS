using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using static ProductionPlanning.MyEnums;

namespace ProductionPlanning
{
    public partial class frmToolsLists
    {
        public frmToolsLists()
        {
            InitializeComponent();
            _cbFilter8.Name = "cbFilter8";
            _cbFilter7.Name = "cbFilter7";
            _cbFilter6.Name = "cbFilter6";
            _cbFilter5.Name = "cbFilter5";
            _cbFilter4.Name = "cbFilter4";
            _cbFilter3.Name = "cbFilter3";
            _cbFilter2.Name = "cbFilter2";
            _cbFilter1.Name = "cbFilter1";
            _txtSearch8.Name = "txtSearch8";
            _txtSearch7.Name = "txtSearch7";
            _txtSearch6.Name = "txtSearch6";
            _txtSearch2.Name = "txtSearch2";
            _txtSearch3.Name = "txtSearch3";
            _txtSearch4.Name = "txtSearch4";
            _txtSearch1.Name = "txtSearch1";
            _txtSearch5.Name = "txtSearch5";
            _dgList.Name = "dgList";
            _cmdFilter.Name = "cmdFilter";
            _cmdExit.Name = "cmdExit";
            _cmdFind.Name = "cmdFind";

        }


        private int mSearchMode = -1;
        private int I;
        private string mCurrentTableName;
        private DataSetConfiguration DataSetConfig = new DataSetConfiguration();
        private DataRow[] FoundRows = null;
        private IEnumerator FoundRowsEnumerator;

    

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

        public string CurrentTableName
        {
            get
            {
                return mCurrentTableName;
            }

            set
            {
                mCurrentTableName = value;
            }
        }

        public DataSet dsProductionPlanning
        {
            get
            {
                return DataSetConfig.dsProductionPlanning;
            }
        }

        public ListFormCaller CallerForm { get; set; }

        public enum SearchModeEnum
        {
            SM_FIND,
            SM_FILTER
        }

        private void frmRecordsLists_Load(object sender, EventArgs e)
        {
        

            Module1.SetButtonsImage(cmdFilter, 3);
            Module1.SetButtonsImage(cmdFind, 10);
            dgList.Tag = -1;
            dgList.Sorted += Module1.DataGridViews_Sorted_EventHandler;
            string SelectStr = Constants.vbNullString;
         
        }

        private void frmRecordsLists_FormClosing(object sender, FormClosingEventArgs e)
        {
            Module1.SaveGridColumnsWidth(Name, this.CallerForm, dgList);
            dgList.DataSource = null;
            DataSetConfig = null;
            SearchMode = -1;
            FoundRows = null;
            FoundRowsEnumerator = null;
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void frmRecordsLists_Resize(object sender, EventArgs e)
        {
            tsslSearchMode.Width = (int)Math.Round(0.15d * StatusStrip1.Width);
            ToolStripStatusLabel1.Width = (int)Math.Round(0.14d * StatusStrip1.Width);
            ToolStripStatusLabel2.Width = (int)Math.Round(0.14d * StatusStrip1.Width);
            ToolStripStatusLabel3.Width = (int)Math.Round(0.15d * StatusStrip1.Width);
            ToolStripStatusLabel4.Width = (int)Math.Round(0.15d * StatusStrip1.Width);
            tsslRecNo.Width = (int)Math.Round(0.25d * StatusStrip1.Width);

  
        }



        private void dgList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int RecordCount = Conversions.ToInteger(Interaction.IIf(dgList.DataSource is DataSet, BindingContext[dsProductionPlanning, CurrentTableName].Count, dgList.Rows.Count));
            int CurrentRecord = e.RowIndex + 1;
            tsslRecNo.Text = "رکورد  " + CurrentRecord.ToString() + "  از  " + RecordCount.ToString();
        }

        private void dgList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgList.Columns[e.ColumnIndex].Name.Equals("StartDate") || dgList.Columns[e.ColumnIndex].Name.Equals("EndDate") || dgList.Columns[e.ColumnIndex].Name.Equals("CalcDate") || dgList.Columns[e.ColumnIndex].Name.Equals("StartDate"))
            {
                e.Value = Strings.Mid(Conversions.ToString(e.Value), 1, 4) + "/" + Strings.Mid(Conversions.ToString(e.Value), 5, 2) + "/" + Strings.Mid(Conversions.ToString(e.Value), 7, 2);
            }

            if (e.RowIndex > -1)
            {
                dgList.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
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
                        for (I = 1; I <= 8; I++)
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
                        for (I = 1; I <= 8; I++)
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
                if (!CurrentCombo.Items.Contains(dgList.Rows[(int)ItemCounter].Cells[CurrentCombo.Tag.ToString()].Value))
                {
                    CurrentCombo.Items.Add(dgList.Rows[(int)ItemCounter].Cells[CurrentCombo.Tag.ToString()].Value);
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

            for (short ComboCounter = 1; ComboCounter <= 8; ComboCounter++)
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
            if (!string.IsNullOrEmpty(FilterValue))
            {
                FilterValue = FilterValue.Replace("ی", "ي");
                FilterValue = FilterValue.Replace("ي", "ي");
            }

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
                    PreControlName = Conversions.ToString(Interaction.IIf(Conversions.ToInteger(Strings.Right(CurrentTextBox.Name, 1)) < 8, PreControlName + (Conversions.ToInteger(Strings.Right(CurrentTextBox.Name, 1)) + 1).ToString(), Constants.vbNullString));
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
                for (short TextBoxCounter = 1; TextBoxCounter <= 8; TextBoxCounter++)
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

                    if (!string.IsNullOrEmpty(FilterValue))
                    {
                        FilterValue = FilterValue.Replace("ی", "ي");
                        FilterValue = FilterValue.Replace("ي", "ي");
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
            for (I = 1; I <= 8; I++)
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
            for (I = 1; I <= 8; I++)
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

            }

            Module1.SetGridColumnsWidth(Name, this.CallerForm , dgList);
        }

        public DataRow GetRow()
        {
            string FindValue = Constants.vbNullString;
            DataRow[] drFind = new DataRow[1];
            DataRow crRow;
         

            drFind = dsProductionPlanning.Tables[mCurrentTableName].Select(FindValue);
            crRow = drFind[0];
            return crRow;
        }

        private void Prepare_To_Show_TablesRecordList(string TN, string FC)
        {
            SetGridColumns(dsProductionPlanning, TN);
            CurrentTableName = TN;
            Text = FC;
        }

        private void dgList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true & e.KeyCode == Keys.F12)
            {
                Module1.ExportGridToExcel(dgList);
            }
        }
    }
}