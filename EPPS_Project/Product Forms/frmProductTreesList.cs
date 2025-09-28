using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmProductTreesList
    {
        public frmProductTreesList()
        {
            InitializeComponent();
            _dgList.Name = "dgList";
            _cbProductName.Name = "cbProductName";
            _txtProductCode.Name = "txtProductCode";
            _cmdFilter.Name = "cmdFilter";
            _cmdExit.Name = "cmdExit";
            _cmdFind.Name = "cmdFind";
            _cmdDelete.Name = "cmdDelete";
            _cmdUpdate.Name = "cmdUpdate";
            _cmdInsert.Name = "cmdInsert";
            _cmdCopy.Name = "cmdCopy";
            _cmdTreeDetails.Name = "cmdTreeDetails";
        }

        private int mFormMode; // برای نگهداری شماره عملیات درخواستی کاربر
        private int mSearchMode = -1; // برای نگهداری شماره حالت جستجو
        private DataSetConfiguration DataSetConfig = new DataSetConfiguration();
        private DataView dvTrees;
        //private DataRow[] FoundRows = null; // برای نگهداری رکوردهای یافت شده در عملیات جستجو
        //private IEnumerator FoundRowsEnumerator; // برای نگهداری شمارنده رکوردهای یافت شده
        //private int I;

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
        // مشخص کننده نوع جستجو(فیلتر یا جستجو) می باشد
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

        private void frmProductTreesList_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdInsert, 0);
            Module1.SetButtonsImage(cmdDelete, 1);
            Module1.SetButtonsImage(cmdUpdate, 2);
            Module1.SetButtonsImage(cmdFilter, 3);
            Module1.SetButtonsImage(cmdFind, 10);
            Module1.SetButtonsImage(cmdCopy, 11);
            dgList.Sorted += Module1.DataGridViews_Sorted_EventHandler;
            DataSetConfig.FillDataSet("Tbl_Products", "Tbl_Products", "Select * From Tbl_Products", "ProductCode");
            DataSetConfig.FillDataSet("Tbl_Products", "Tbl_Products_Combo", "Select * From Tbl_Products", "ProductCode");
            DataSetConfig.FillDataSet("Tbl_ProductTree", "Tbl_ProductTree", "Select   A.TreeCode, A.ProductCode, A.TreeTitle, A.DefualtTree, B.ProductCode AS ProductCode1, B.ProductName From  dbo.Tbl_ProductTree A INNER JOIN  dbo.Tbl_Products B ON A.ProductCode = B.ProductCode", "TreeCode");
            Prepare_To_Show_TableRecordList("Tbl_ProductTree", " مشخصات درخت محصول");
            dvTrees = dsProductionPlanning.Tables["Tbl_ProductTree"].DefaultView;
            SetGridColumns(dvTrees, "Tbl_ProductTree");
            {
                var withBlock = cbProductName;
                withBlock.DataSource = null;
                withBlock.DataSource = dsProductionPlanning.Tables["Tbl_Products_Combo"];
                withBlock.DisplayMember = "ProductName";
                withBlock.ValueMember = "ProductCode";
                withBlock.SelectedIndex = -1;
            }

            // ''''  Set User Access Right ''''''''''
            SetButton_Enabel();
            // ''cmdInsert.Enabled = HaveAccessToItem(24)
            // ''cmdUpdate.Enabled = HaveAccessToItem(25)
            // ''cmdDelete.Enabled = HaveAccessToItem(26)
            // ''cmdTreeDetails.Enabled = HaveAccessToItem(27)
            // ''cmdCopy.Enabled = HaveAccessToItem(77)
        }

        private void frmProductTreesList_FormClosing(object sender, FormClosingEventArgs e)
        {
            Module1.SaveGridColumnsWidth(Name, 0, dgList);
            dsProductionPlanning.RejectChanges();
            dgList.DataSource = null;
            DataSetConfig = null;
            dvTrees = null;
            //FoundRows = null;
            //FoundRowsEnumerator = null;
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
                MessageBox.Show("رکوردی وجود ندارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                return;
            }

            DialogResult DetailsResult = (DialogResult)(-1);
            {
                var withBlock = new frmProductTree();
                withBlock.ProductCode = Conversions.ToString(cbProductName.SelectedValue);
                withBlock.ListForm = this;
                withBlock.ListForm.FormMode = mFormMode;
                if (mFormMode == (int)Module1.FormModeEnum.DELETE_MODE)
                {
                    withBlock.Controls["Panel1"].Controls["cmdSave"].Visible = false;
                    withBlock.Controls["Panel1"].Controls["cmdDelete"].Visible = true;
                }

                DetailsResult = withBlock.ShowDialog();
                withBlock.Dispose();
            }

            if (DetailsResult != DialogResult.Cancel)
            {
                switch (FormMode)
                {
                    case (int)Module1.FormModeEnum.INSERT_MODE:
                        {
                            BindingContext[dsProductionPlanning, "Tbl_ProductTree"].Position = BindingContext[dsProductionPlanning, "Tbl_ProductTree"].Count;
                            break;
                        }

                    case (int)Module1.FormModeEnum.DELETE_MODE:
                        {
                            if (BindingContext[dsProductionPlanning, "Tbl_ProductTree"].Count > 0)
                            {
                                BindingContext[dsProductionPlanning, "Tbl_ProductTree"].Position = 0;
                            }
                            else
                            {
                                cmdUpdate.Enabled = false;
                                cmdDelete.Enabled = false;
                                cmdFilter.Enabled = false;
                                cmdFind.Enabled = false;
                                cmdCopy.Enabled = false;
                                cmdShow.Enabled = false;
                                cmdTreeDetails.Enabled = false;
                            }

                            break;
                        }
                }
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
            if (dgList.Tag is object && Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(dgList.Tag, -1, false)))
            {
                SetSearchControlsLocation();
            }
        }

        private void cmdFilter_Click(object sender, EventArgs e)
        {
            if (mSearchMode == -1)
            {
                if (ReferenceEquals(sender, cmdFilter))
                {
                    mSearchMode = (int)SearchModeEnum.SM_FILTER;
                    tsslSearchMode.Text = "فیلتر اطلاعات";
                }
                else
                {
                    mSearchMode = (int)SearchModeEnum.SM_FIND;
                    tsslSearchMode.Text = "جستجوی اطلاعات";
                }

                SetSearchControlsLocation();
            }
            else if (ReferenceEquals(sender, cmdFilter) && mSearchMode == (int)SearchModeEnum.SM_FILTER || ReferenceEquals(sender, cmdFind) && mSearchMode == (int)SearchModeEnum.SM_FIND)
            {
                txtSearch1.Visible = false;
                txtSearch2.Visible = false;
                txtSearch3.Visible = false;
                tsslSearchMode.Text = "نمایش کلی";
            }
            else if (ReferenceEquals(sender, cmdFilter) && mSearchMode == (int)SearchModeEnum.SM_FIND || ReferenceEquals(sender, cmdFind) && mSearchMode == (int)SearchModeEnum.SM_FILTER)
            {
                if (ReferenceEquals(sender, cmdFilter))
                {
                    mSearchMode = (int)SearchModeEnum.SM_FILTER;
                    tsslSearchMode.Text = "فیلتر اطلاعات";
                }
                else
                {
                    mSearchMode = (int)SearchModeEnum.SM_FIND;
                    tsslSearchMode.Text = "جستجوی اطلاعات";
                }

                SetSearchControlsLocation();
            }
        }

        private void dgList_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (mSearchMode != -1)
            {
                SetSearchControlsLocation();
            }
        }

        private void dgList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int RecordCount = Conversions.ToInteger(Interaction.IIf(dgList.DataSource is DataSet, BindingContext[dsProductionPlanning, "Tbl_ProductTree"].Count, dgList.Rows.Count));
            int CurrentRecord = e.RowIndex + 1;
            tsslRecNo.Text = "رکورد  " + CurrentRecord.ToString() + "  از  " + RecordCount.ToString();
            if (dgList.Rows.Count > 0)
            {
                SetButton_Enabel();
            }
            // cmdUpdate.Enabled = True
            // cmdDelete.Enabled = True
            // cmdFilter.Enabled = True
            // cmdFind.Enabled = True
            // cmdCopy.Enabled = True
            // cmdShow.Enabled = True
            // cmdTreeDetails.Enabled = True
            else
            {
                cmdUpdate.Enabled = false;
                cmdDelete.Enabled = false;
                cmdFilter.Enabled = false;
                cmdFind.Enabled = false;
                cmdCopy.Enabled = false;
                cmdShow.Enabled = false;
                cmdTreeDetails.Enabled = false;
            }
        }

        private void txtProductCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Strings.Asc(e.KeyChar) == (int)Keys.Enter)
            {
                var dvProduct = dsProductionPlanning.Tables["Tbl_Products"].DefaultView;
                dvProduct.RowFilter = Constants.vbNullString;
                if (string.IsNullOrEmpty(txtProductCode.Text))
                {
                    cbProductName.Text = Constants.vbNullString;
                    dvTrees.RowFilter = Constants.vbNullString;
                    if (dvTrees.Count == 0)
                    {
                        cmdInsert.Enabled = false;
                        cmdUpdate.Enabled = false;
                        cmdDelete.Enabled = false;
                        cmdFilter.Enabled = false;
                        cmdFind.Enabled = false;
                        cmdCopy.Enabled = false;
                        cmdShow.Enabled = false;
                        cmdTreeDetails.Enabled = false;
                    }
                }
                else
                {
                    dvProduct.RowFilter = "ProductCode='" + Strings.Trim(txtProductCode.Text) + "'";
                    if (dvProduct.Count > 0)
                    {
                        cbProductName.SelectedValue = Strings.Trim(txtProductCode.Text);
                        dvTrees = dsProductionPlanning.Tables["Tbl_ProductTree"].DefaultView;
                        dvTrees.RowFilter = "ProductCode='" + Strings.Trim(txtProductCode.Text) + "'";
                        SetGridColumns(dvTrees, "Tbl_ProductTree");
                        cmdInsert.Enabled = Module_UserAccess.HaveAccessToItem(24); // True
                        if (dvTrees.Count > 0)
                        {
                            SetButton_Enabel();
                        }

                        // cmdUpdate.Enabled = True
                        // cmdDelete.Enabled = True
                        // cmdFilter.Enabled = True
                        // cmdFind.Enabled = True
                        // cmdCopy.Enabled = True
                        // cmdShow.Enabled = True
                        // cmdTreeDetails.Enabled = True
                        else
                        {
                            cmdUpdate.Enabled = false;
                            cmdDelete.Enabled = false;
                            cmdFilter.Enabled = false;
                            cmdFind.Enabled = false;
                            cmdCopy.Enabled = false;
                            cmdShow.Enabled = false;
                            cmdTreeDetails.Enabled = false;
                        }
                    }
                    else
                    {
                        dgList.DataSource = null;
                        MessageBox.Show("محصولی با کد وارد شده وجود ندارد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        cmdInsert.Enabled = false;
                        cmdUpdate.Enabled = false;
                        cmdDelete.Enabled = false;
                        cmdFilter.Enabled = false;
                        cmdFind.Enabled = false;
                        cmdCopy.Enabled = false;
                        cmdShow.Enabled = false;
                        cmdTreeDetails.Enabled = false;
                    }
                }
            }
        }

        private void SetButton_Enabel()
        {
            cmdUpdate.Enabled = Module_UserAccess.HaveAccessToItem(25); // True
            cmdDelete.Enabled = Module_UserAccess.HaveAccessToItem(26); // True
            cmdFilter.Enabled = true;
            cmdFind.Enabled = true;
            cmdCopy.Enabled = Module_UserAccess.HaveAccessToItem(77); // True
            cmdShow.Enabled = false;
            cmdTreeDetails.Enabled = Module_UserAccess.HaveAccessToItem(27); // True
        }

        private void cmdTreeDetails_Click(object sender, EventArgs e)
        {
            {
                var withBlock = new frmProductTreeDetail();
                withBlock.Text = "مشخصات اجزای درخت محصول (" + dgList.CurrentRow.Cells["TreeCode"].Value.ToString() + " - " + dgList.CurrentRow.Cells["TreeTitle"].Value.ToString() + ")";
                withBlock.TreeView1.Tag = dgList.CurrentRow.Cells["TreeCode"].Value;
                withBlock.MdiParent = MdiParent;
                withBlock.Show();
            }
        }

        private void cmdCopy_Click(object sender, EventArgs e)
        {
            DataSetConfig.FillDataSet("Tbl_ProductTreeDetails", "Tbl_ProductTreeDetails", "Select * From Tbl_ProductTreeDetails", "TreeCode", "DetailCode");
            DataSetConfig.FillDataSet("Tbl_ProductOPCs", "Tbl_ProductOPCs", "Select * From Tbl_ProductOPCs", "TreeCode", "OperationCode");
            DataSetConfig.FillDataSet("Tbl_OperationMaterials", "Tbl_OperationMaterials", "Select * From Tbl_OperationMaterials", "TreeCode", "CurrentOperationCode", "MaterialCode");
            DataSetConfig.FillDataSet("Tbl_PreOperations", "Tbl_PreOperations", "Select * From Tbl_PreOperations", "TreeCode", "CurrentOperationCode", "PreOperationCode");
            DataSetConfig.FillDataSet("Tbl_ProductOPCsExecutorMachines", "Tbl_ProductOPCsExecutorMachines", "Select * From Tbl_ProductOPCsExecutorMachines", "TreeCode", "OperationCode", "MachineCode");
            DataSetConfig.FillDataSet("Tbl_ContractorOperations", "Tbl_ContractorOperations", "Select * From Tbl_ContractorOperations", "TreeCode", "OperationCode", "ContractorCode");
            {
                var withBlock = My.MyProject.Forms.frmCopyProductTree;
                withBlock.ListForm = this;
                withBlock.ShowDialog();
                withBlock.Dispose();
            }
        }

        private void SetSearchControlsLocation()
        {
            short I, J, ControlIndex;
            int TempWidth;
            string Control;
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

                    Controls["Panel1"].Controls[Control].Text = Constants.vbNullString;
                    Controls["Panel1"].Controls[Control].Top = dgList.Location.Y;
                    Controls["Panel1"].Controls[Control].Left = TempWidth - dgList.RowHeadersWidth + 4;
                    Controls["Panel1"].Controls[Control].Width = dgList.Columns[I - 1].HeaderCell.OwningColumn.Width - 2;
                    Controls["Panel1"].Controls[Control].Visible = true;
                    ControlIndex = (short)(ControlIndex + 1);
                }
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

                withBlock.Columns[0].HeaderText = "کد درخت محصول";
                withBlock.Columns[1].Visible = false;
                withBlock.Columns[2].HeaderText = "عنوان درخت محصول";
                withBlock.Columns[3].HeaderText = "درخت محصول پیش فرض";
                withBlock.Columns[4].HeaderText = "کد محصول";
                withBlock.Columns[5].HeaderText = "نام محصول";
            }

            Module1.SetGridColumnsWidth(Name, 0, dgList);
        }
        // این تابع رکورد جاری از جدول جاری استفاده شده در برنامه را باز می گرداند
        public DataRow GetRow()
        {
            object crRow;
            int crRowNo;
            if (dgList.DataSource is DataSet)
            {
                crRowNo = BindingContext[dsProductionPlanning, "Tbl_ProductTree"].Position;
                crRow = dsProductionPlanning.Tables["Tbl_ProductTree"].Rows[crRowNo];
            }
            else
            {
                crRowNo = BindingContext[dsProductionPlanning.Tables["Tbl_ProductTree"].DefaultView].Position;
                string FindValue = Constants.vbNullString;
                var drFind = new DataRow[1];
                FindValue = Conversions.ToString(Operators.ConcatenateObject("TreeCode=", dsProductionPlanning.Tables["Tbl_ProductTree"].DefaultView[crRowNo]["TreeCode"]));
                drFind = dsProductionPlanning.Tables["Tbl_ProductTree"].Select(FindValue);
                crRow = drFind[0];
            }

            return (DataRow)crRow;
        }
        // تنظیم فرم برای نمایش رکوردهای جداول بانک اطلاعاتی
        private void Prepare_To_Show_TableRecordList(string TN, string FC)
        {
            SetGridColumns(dsProductionPlanning, TN);
            Text = FC;
        }

        private void cbProductName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbProductName.Items.Count > 0 && cbProductName.SelectedValue is object && cbProductName.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                txtProductCode.Text = cbProductName.SelectedValue.ToString();
                dvTrees.RowFilter = "ProductCode='" + Strings.Trim(txtProductCode.Text) + "'";
                cmdInsert.Enabled = Module_UserAccess.HaveAccessToItem(24); // True
                if (dvTrees.Count > 0)
                {
                    SetButton_Enabel();
                }
                else
                {
                    cmdUpdate.Enabled = false;
                    cmdDelete.Enabled = false;
                    cmdFilter.Enabled = false;
                    cmdFind.Enabled = false;
                    cmdCopy.Enabled = false;
                    cmdShow.Enabled = false;
                    cmdTreeDetails.Enabled = false;
                }
            }
            else
            {
                cmdInsert.Enabled = false;
                cmdUpdate.Enabled = false;
                cmdDelete.Enabled = false;
                cmdFilter.Enabled = false;
                cmdFind.Enabled = false;
                cmdCopy.Enabled = false;
                cmdShow.Enabled = false;
                cmdTreeDetails.Enabled = false;
            }
        }

        private void dgList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dgList.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
            }
        }
    }
}