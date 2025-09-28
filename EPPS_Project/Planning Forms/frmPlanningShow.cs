using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmPlanningShow
    {
        public frmPlanningShow()
        {
            InitializeComponent();
            _dgList.Name = "dgList";
            _cmdExportToMSP.Name = "cmdExportToMSP";
            _cmdExit.Name = "cmdExit";
            //_cbBatch.Name = "cbBatch";
            //_cbProduct.Name = "cbProduct";
            _cmdShow.Name = "cmdShow";
        }
        // Private dvPlanningList As DataView

        private DataSet PlanningDataset = new DataSet();
        // Private DataSetConfig As New DataSetConfiguration

        private void frmPlanningShow_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                Left = 10;
                Top = 10;
                // AddHandler dgList.Sorted, AddressOf DataGridViews_Sorted_EventHandler

                SetComboBoxsDataSource();
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                Logger.SaveError("frmPlanningShow_Load", ex.Message);
                MessageBox.Show("فراخوانی فرم نمایش برنامه ریزی با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                Close();
            }
        }

        private void frmPlanningShow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Module1.SaveGridColumnsWidth(Name, 0, dgList);
            // cbProduct.DataSource = null;
            //cbMachine.DataSource = null;
            Machine_Lookup.CB_DataSource = null;
            //   cbBatch.DataSource = null;
            //cbSubbatch.DataSource = null;
            dgList.DataSource = null;
            cbTreeDetails.DataSource = null;
            PlanningDataset.Dispose();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        //private void cbProduct_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    cbTreeDetails.DataSource = null;
        //    if (cbProduct.Items.Count > 0 && cbProduct.SelectedValue is object && cbProduct.SelectedValue.ToString() != "System.Data.DataRowView")
        //    {
        //        if (cbBatch.DataSource is object)
        //        {
        //            ((DataView)cbBatch.DataSource).RowFilter = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("ProductCode='", cbProduct.SelectedValue), "'"));
        //        }

        //        var dtTreeDetails = new DataTable();
        //        var daTreeDetails = new SqlDataAdapter(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Select Distinct A.DetailCode,A.DetailName " + "From   Tbl_ProductTreeDetails A INNER JOIN dbo.Tbl_ProductTree B ON A.TreeCode=B.TreeCode " + "Where  A.DetailCode IN (Select D.DetailCode From dbo.Tbl_Planning C INNER JOIN " + "                                          dbo.Tbl_ProductOPCs D ON C.TreeCode = D.TreeCode And C.OperationCode = D.OperationCode) " + "And ProductCode='", cbProduct.SelectedValue), "' And DefualtTree = 1")), Module1.cnProductionPlanning);
        //        daTreeDetails.Fill(dtTreeDetails);
        //        daTreeDetails.Dispose();
        //        {
        //            var withBlock = cbTreeDetails;
        //            withBlock.DataSource = dtTreeDetails;
        //            withBlock.DisplayMember = "DetailName";
        //            withBlock.ValueMember = "DetailCode";
        //            withBlock.SelectedIndex = -1;
        //        }
        //    }
        //    else if (cbBatch.DataSource is object)
        //    {
        //        ((DataView)cbBatch.DataSource).RowFilter = Constants.vbNullString;
        //    }

        //    cbBatch.SelectedIndex = -1;
        //}

        //private void cbBatch_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    if (cbBatch.Items.Count > 0 && cbBatch.SelectedValue is object && cbBatch.SelectedValue.ToString() != "System.Data.DataRowView")
        //    {
        //        if (cbSubbatch.DataSource is object)
        //        {
        //            ((DataView)cbSubbatch.DataSource).RowFilter = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("BatchCode='", cbBatch.SelectedValue), "'"));
        //        }
        //    }
        //    else if (cbSubbatch.DataSource is object)
        //    {
        //        ((DataView)cbSubbatch.DataSource).RowFilter = Constants.vbNullString;
        //    }

        //    cbSubbatch.SelectedIndex = -1;
        //}

        //private void cbBatch_TextChanged(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(cbBatch.Text))
        //    {
        //        if (cbSubbatch.DataSource is object)
        //        {
        //            ((DataView)cbSubbatch.DataSource).RowFilter = Constants.vbNullString;
        //        }

        //        cbSubbatch.SelectedIndex = -1;
        //    }
        //}

        private void cmdShow_Click(object sender, EventArgs e)
        {
            string Filter;
            string SelectStr = @"Select TOP 10000
           ProductCode    = Tbl_ProductTree.ProductCode + ' - ' + Tbl_Products.ProductName,
		   OperationCode  = Tbl_Planning.OperationCode,
		   OperationTitle = Tbl_ProductOPCs.OperationTitle,
		   MachineCode    = Case Tbl_ProductOPCs.ExecutionMethod 
		                         WHEN 1 Then Tbl_Planning.MachineCode + ' - ' + Tbl_Machines.Name
								 WHEN 2 Then 'عمليات اپراتوري' 
								 When 3 Then 'عمليات پيمانکاري' 
							End,
           Tbl_Planning.DetailProductionQuantity,
           Duration      = dbo.GetRegularTimeFromFloatingTime(Tbl_Planning.PlanningDuration),
		   SetupDuration = dbo.GetRegularTimeFromFloatingTime(Tbl_Planning.SetupDuration),
           StartDT      =   dbo.GetRegularTimeFromFloatingTime(Tbl_Planning.PlanningStartHour)  + ' ' 
						  + SUBSTRING(Tbl_Planning.PlanningStartDate,1,4) + '/'
						  + SUBSTRING(Tbl_Planning.PlanningStartDate,5,2) + '/'
						  + SUBSTRING(Tbl_Planning.PlanningStartDate,7,2) ,
		   FinishDT     =   dbo.GetRegularTimeFromFloatingTime(Tbl_Planning.PlanningEndHour)  + ' ' 
						  + SUBSTRING(Tbl_Planning.PlanningEndDate,1,4) + '/'
						  + SUBSTRING(Tbl_Planning.PlanningEndDate,5,2) + '/'
						  + SUBSTRING(Tbl_Planning.PlanningEndDate,7,2) ,
		   Tbl_ProductionSubbatchs.SubbatchCode,
		   BatchStatus = CASE WHEN Tbl_ProductionBatchs.FinishedDate <> '0' THEN 'بسته شده'
	                          WHEN RealProduction.SubbatchCode IS NULL      THEN 'وارد توليد نشده'  
				              ELSE 'در حال توليد' 
	                     END,
		   Tbl_ProductionBatchs.BatchCode,
		   Tbl_Planning.PlanningStartDate,
		   Tbl_Planning.PlanningStartHour,
           Tbl_Planning.PlanningDuration,
		   MinBaseStartHour=dbo.GetRegularTimeFromFloatingTime(Tbl_Planning.PlanningStartHour),
		   Tbl_Planning.PlanningEndDate,
		   Tbl_Planning.PlanningEndHour,
		   MinBaseEndHour=dbo.GetRegularTimeFromFloatingTime(Tbl_Planning.PlanningEndHour),
		   Tbl_ProductOPCs.ExecutionMethod,
		   Tbl_ProductOPCs.DetailCode,
		   Tbl_Planning.OperationStartDate,
		   Tbl_Planning.OperationEndDate,
		   -- Tbl_ProductTree.ProductCode,
		   Tbl_ProductTree.TreeCode
		From  Tbl_Planning  
			  INNER JOIN Tbl_ProductionSubbatchs ON Tbl_ProductionSubbatchs.SubbatchCode = Tbl_Planning.SubbatchCode   
			  INNER JOIN Tbl_ProductionBatchs    ON Tbl_ProductionBatchs.BatchCode = Tbl_ProductionSubbatchs.BatchCode 
			  INNER JOIN Tbl_ProductTree         ON Tbl_ProductTree.TreeCode = Tbl_ProductionBatchs.ProductTreeCode  
              INNER JOIN Tbl_Products            ON Tbl_Products.ProductCode = Tbl_ProductTree.ProductCode
			  INNER JOIN Tbl_ProductOPCs         ON Tbl_Planning.TreeCode = Tbl_ProductOPCs.TreeCode And Tbl_Planning.OperationCode = Tbl_ProductOPCs.OperationCode  
			  LEFT  JOIN Tbl_Machines            ON Tbl_Machines.Code = Tbl_Planning.MachineCode
              LEFT  JOIN (SELECT DISTINCT SubbatchCode FROM Tbl_RealProduction)  
	                  AS RealProduction   	     ON RealProduction.SubbatchCode = Tbl_ProductionSubbatchs.SubbatchCode
        ";

            string DefaultOrderBy = " ORDER BY Tbl_Planning.PlanningStartDate, Tbl_Planning.PlanningStartHour, Tbl_Planning.SubbatchCode ";
            string SelectSQL;
            if (chkAll.Checked == true)
            {
                chkAll.Checked = false;
                zzClearFilter();
            }

            Filter = Conversions.ToString(GetFilterValue());
            if (!string.IsNullOrEmpty(Filter))
            {
                SelectSQL = SelectStr + " WHERE " + Filter;
            }
            else
            {
                SelectSQL = SelectStr;
            }

            PlanningDataset = DbTool.GetDataSet("PlanItems", SelectSQL + DefaultOrderBy);
            dgList.DataSource = PlanningDataset.Tables["PlanItems"].DefaultView;
            zzSetGridColumns();
        }

        private void cmdExportToMSP_Click(object sender, EventArgs e)
        {
            var Tmpfrm = new CreateMSPForm();
            //int FilterType = 0;
            //string BactCodeTmp = "";
            //string SubBatchCodeTmp = "";
            //FilterType = 0;
            try
            {
                if (PlanningDataset.Tables.Count == 0)
                    return;
                Tmpfrm.MSPDataPlaning = PlanningDataset.Tables["PlanItems"].DefaultView;
                Tmpfrm.GanteOnGridData = true;
                Tmpfrm.ShowDialog();
                Tmpfrm.Dispose();
            }
            catch (Exception objEx)
            {
                Logger.LogException("cmdExportToMSP_Click", objEx);
                MessageBox.Show("با مشکل مواجه شد MPS ایجاد خروجی به", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }

        private void dgList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true & e.KeyCode == Keys.F12)
            {
                Module1.ExportGridToExcel(dgList);
            }
        }

        /* TODO ERROR: Skipped RegionDirectiveTrivia */
        private void zzClearFilter()
        {
            txtFromDate.Text = Constants.vbNullString;
            txtToDate.Text = Constants.vbNullString;
            // cbProduct.Text = Constants.vbNullString;
            cbTreeDetails.Text = Constants.vbNullString;
            // cbMachine.Text = Constants.vbNullString;
            Machine_Lookup.ResetText();
            //  cbBatch.Text = Constants.vbNullString;
            //  cbSubbatch.Text = Constants.vbNullString;
            // cbProduct.SelectedIndex = -1;
            cbProduct_Lookup.ResetText();
            // cbProduct.SelectedIndex = -1;
            cbTreeDetails.SelectedIndex = -1;
            // cbMachine.SelectedIndex = -1;
            Machine_Lookup.CB_SelectedIndex = -1;
            // cbBatch.SelectedIndex = -1;
            // cbSubbatch.SelectedIndex = -1;
            chkDoneBatchs.Checked = false;
            chkHasProduction.Checked = false;
            chkNoProduction.Checked = false;
        }

        private void zzSetGridColumns()
        {
            int C;
            // BatchStatus
            // B.SubbatchCode,
            // C.BatchCode,
            // Tbl_Planning.PlanningStartDate,
            // Tbl_Planning.PlanningStartHour,
            // --MinBaseStartHour = dbo.GetRegularTimeFromFloatingTime(Tbl_Planning.PlanningStartHour),
            // Tbl_Planning.PlanningEndDate,
            // Tbl_Planning.PlanningEndHour,
            // --MinBaseEndHour = dbo.GetRegularTimeFromFloatingTime(Tbl_Planning.PlanningEndHour),
            // E.ExecutionMethod,
            // E.DetailCode,
            // Tbl_Planning.OperationStartDate,
            // --OperationStartHour = dbo.GetRegularTimeFromFloatingTime(Tbl_Planning.OperationStartHour),
            // Tbl_Planning.OperationEndDate,
            // --OperationEndHour = dbo.GetRegularTimeFromFloatingTime(Tbl_Planning.OperationEndHour),
            // --dbo.GetBatchStatus(C.BatchCode) As BatchStatus,
            // D.ProductCode,
            // D.TreeCode 
            {
                var withBlock = dgList;
                withBlock.Columns[0].HeaderText = "محصول";
                withBlock.Columns[1].HeaderText = "کد عملیات";
                withBlock.Columns[2].HeaderText = "عنوان عملیات";
                withBlock.Columns[3].HeaderText = "ماشین ";
                withBlock.Columns[4].HeaderText = "تعداد تولید";
                withBlock.Columns[5].HeaderText = "زمان عملیات";
                withBlock.Columns[6].HeaderText = "Setup ماشین";
                withBlock.Columns[7].HeaderText = "شروع ";
                withBlock.Columns[8].HeaderText = "پایان ";
                withBlock.Columns[9].HeaderText = "بچ تولید";
                withBlock.Columns[10].HeaderText = "وضعیت بچ";
                var loopTo = withBlock.Columns.Count - 1;
                for (C = 11; C <= loopTo; C++)
                    withBlock.Columns[C].Visible = false;
            }

            Module1.SetGridColumnsWidth(Name, 0, dgList);
        }

        /* TODO ERROR: Skipped EndRegionDirectiveTrivia */
        private void SetComboBoxsDataSource()
        {
            var daCombo = new SqlDataAdapter("", Module1.cnProductionPlanning);
            var dtCombo = new DataTable();
            daCombo.SelectCommand.CommandText = "Select ProductCode,ProductName From Tbl_Products Order By ProductName";
            daCombo.Fill(dtCombo);
            //{
            //    var withBlock = cbProduct;
            //    withBlock.DataSource = dtCombo;
            //    withBlock.DisplayMember = "ProductName";
            //    withBlock.ValueMember = "ProductCode";
            //    withBlock.SelectedIndex = -1;

            //}
            {

                var withBlock = cbProduct_Lookup;
                withBlock.CB_DataSource = dtCombo;
                withBlock.CB_DisplayMember = "ProductCode";
                withBlock.CB_ValueMember = "ProductName";
                withBlock.CB_SelectedIndex = -1;
            }



            daCombo.SelectCommand.CommandText = "Select Code, Code+' '+Name As MachineName From Tbl_Machines Where Code <> '-1'";
            dtCombo = new DataTable();
            daCombo.Fill(dtCombo);
            {
                var withBlock2 = Machine_Lookup;
                withBlock2.CB_DataSource = dtCombo.DefaultView;
                withBlock2.CB_DisplayMember = "Code";
                withBlock2.CB_ValueMember = "MachineName";
                withBlock2.CB_SelectedIndex = -1;
            }
            //Machine_Lookup.CB_DataSource = dtCombo.DefaultView;
            //Machine_Lookup.CB_DisplayMember = "Code";
            //Machine_Lookup.CB_ValueMember = "MachineName";
            //Machine_Lookup.CB_AutoComplete = true;
            //Machine_Lookup.CB_LinkedColumnIndex = 1;
            //Machine_Lookup.CB_SerachFromTitle = "ماشین";

            daCombo.SelectCommand.CommandText = "Select A.BatchCode, B.ProductCode " + "From   dbo.Tbl_ProductionBatchs A INNER JOIN " + "       dbo.Tbl_ProductTree B ON A.ProductTreeCode = B.TreeCode";

            dtCombo = new DataTable();
            daCombo.Fill(dtCombo);
            //{
            //    var withBlock2 = cbBatch;
            //    withBlock2.DataSource = dtCombo.DefaultView;
            //    withBlock2.DisplayMember = "BatchCode";
            //    withBlock2.ValueMember = "BatchCode";
            //    withBlock2.SelectedIndex = -1;
            //}
            //fill cbBatch_Lookup
            {
                var withBlock2 = cbBatch_Lookup;
                withBlock2.CB_DataSource = dtCombo.DefaultView;
                withBlock2.CB_DisplayMember = "BatchCode";
                withBlock2.CB_ValueMember = "ProductCode";
                withBlock2.CB_SelectedIndex = -1;
            }



            daCombo.SelectCommand.CommandText = "Select SubbatchCode,BatchCode From Tbl_ProductionSubbatchs";
            dtCombo = new DataTable();
            daCombo.Fill(dtCombo);
            //{
            //    var withBlock3 = cbSubbatch;
            //    withBlock3.DataSource = dtCombo.DefaultView;
            //    withBlock3.DisplayMember = "SubbatchCode";
            //    withBlock3.ValueMember = "SubbatchCode";
            //    withBlock3.SelectedIndex = -1;
            //}
            //fill cbSubbatch_LookUp
            {
                var withBlock2 = cbSubbatch_LookUp;
                withBlock2.CB_DataSource = dtCombo.DefaultView;
                withBlock2.CB_DisplayMember = "SubbatchCode";
                withBlock2.CB_ValueMember = "BatchCode";
                withBlock2.CB_SelectedIndex = -1;
            }
        }

        private object GetFilterValue()
        {
            string Filter = "";
            string FilterBatch = "";
            //if (cbProduct.SelectedValue is object && !cbProduct.Text.Trim().Equals(""))
            //{
            //    Filter = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(" Tbl_ProductTree.ProductCode ='", cbProduct.SelectedValue), "'"));
            //}
            if (cbProduct_Lookup.CB_SelectedValue is object && !cbProduct_Lookup.CB_SelectedDescription.Trim().Equals(""))
            {
                Filter = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(" Tbl_ProductTree.ProductCode ='", cbProduct_Lookup.CB_SelectedValue), "'"));
            }

            if (cbTreeDetails.SelectedValue is object && !cbTreeDetails.Text.Trim().Equals(""))
            {
                Filter = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Filter, Interaction.IIf(string.IsNullOrEmpty(Filter), "", " AND ")), " Tbl_ProductOPCs.DetailCode ='"), cbTreeDetails.SelectedValue), "'"));
            }

            //if (cbMachine.SelectedValue is object && !cbMachine.Text.Trim().Equals(""))
            //{
            //    Filter = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Filter, Interaction.IIf(string.IsNullOrEmpty(Filter), "", " AND ")), " Tbl_Planning.MachineCode ='"), cbMachine.SelectedValue), "'"));
            //}

            if (Machine_Lookup.CB_SelectedValue is object && !Machine_Lookup.CB_SelectedDescription.Trim().Equals(""))
            {
                Filter = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Filter, Interaction.IIf(string.IsNullOrEmpty(Filter), "", " AND ")), " Tbl_Planning.MachineCode ='"), Machine_Lookup.CB_SelectedValue), "'"));
            }

            //if (cbBatch.SelectedValue is object && !cbBatch.Text.Trim().Equals(""))
            //{
            //    Filter = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Filter, Interaction.IIf(string.IsNullOrEmpty(Filter), "", " AND ")), " Tbl_ProductionBatchs.BatchCode ='"), cbBatch.SelectedValue), "'"));
            //}
            if (cbBatch_Lookup.CB_SelectedValue is object && !cbBatch_Lookup.CB_SelectedDescription.Trim().Equals(""))
            {
                Filter = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Filter, Interaction.IIf(string.IsNullOrEmpty(Filter), "", " AND ")), " Tbl_ProductionBatchs.BatchCode ='"), cbBatch_Lookup.CB_SelectedValue), "'"));
            }



            //if (cbSubbatch.SelectedValue is object && !cbSubbatch.Text.Trim().Equals(""))
            //{
            //    Filter = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Filter, Interaction.IIf(string.IsNullOrEmpty(Filter), "", " AND ")), " Tbl_Planning.SubbatchCode ='"), cbSubbatch.SelectedValue), "'"));
            //}
            if (cbSubbatch_LookUp.CB_SelectedValue is object && !cbSubbatch_LookUp.CB_SelectedDescription.Trim().Equals(""))
            {
                Filter = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Filter, Interaction.IIf(string.IsNullOrEmpty(Filter), "", " AND ")), " Tbl_Planning.SubbatchCode ='"), cbSubbatch_LookUp.CB_SelectedValue), "'"));
            }


            if (!string.IsNullOrEmpty(Strings.Trim(Strings.Replace(txtFromDate.Text, "/", ""))))
            {
                Filter = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Filter, Interaction.IIf(string.IsNullOrEmpty(Filter), "", " AND ")), " Tbl_Planning.PlanningStartDate >='"), Strings.Trim(Strings.Replace(txtFromDate.Text, "/", ""))), "'"));
            }

            if (!string.IsNullOrEmpty(Strings.Trim(Strings.Replace(txtToDate.Text, "/", ""))))
            {
                Filter = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Filter, Interaction.IIf(string.IsNullOrEmpty(Filter), "", " AND ")), " Tbl_Planning.PlanningStartDate <= '"), Strings.Trim(Strings.Replace(txtToDate.Text, "/", ""))), "'"));
            }

            switch (true)
            {
                case object _ when chkNoProduction.Checked && chkHasProduction.Checked && chkDoneBatchs.Checked:
                    {
                        break;
                    }
                // No need to filter, all options choosed.
                case object _ when chkNoProduction.Checked && chkHasProduction.Checked:
                    {
                        FilterBatch = " Tbl_ProductionBatchs.FinishedDate = '0' ";
                        break;
                    }

                case object _ when chkNoProduction.Checked && chkDoneBatchs.Checked:
                    {
                        FilterBatch = " (RealProduction.SubbatchCode IS NULL  OR  Tbl_ProductionBatchs.FinishedDate <> '0' ) ";
                        break;
                    }

                case object _ when chkHasProduction.Checked && chkDoneBatchs.Checked:
                    {
                        FilterBatch = " (RealProduction.SubbatchCode IS NOT NULL  OR  Tbl_ProductionBatchs.FinishedDate <> '0' ) ";
                        break;
                    }

                case object _ when chkNoProduction.Checked:
                    {
                        FilterBatch = " RealProduction.SubbatchCode IS NULL AND Tbl_ProductionBatchs.FinishedDate = '0'";
                        break;
                    }

                case object _ when chkHasProduction.Checked:
                    {
                        FilterBatch = " RealProduction.SubbatchCode IS NOT NULL AND Tbl_ProductionBatchs.FinishedDate = '0' ";
                        break;
                    }

                case object _ when chkDoneBatchs.Checked:
                    {
                        FilterBatch = " Tbl_ProductionBatchs.FinishedDate <> '0' ";
                        break;
                    }
            }

            if (!string.IsNullOrEmpty(FilterBatch))
            {
                Filter = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Filter, Interaction.IIf(string.IsNullOrEmpty(Filter), "", " AND ")), FilterBatch));
            }

            return Filter;
        }

        private void cbProduct_Lookup_SelectedValueChanged(object sender, EventArgs e)
        {
            string x;
            x = cbProduct_Lookup.CB_SelectedValue;
            cbTreeDetails.DataSource = null;
            if (cbProduct_Lookup.CB_SelectedValue is object)
            {


                var dtTreeDetails = new DataTable();
                var daTreeDetails = new SqlDataAdapter(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Select Distinct A.DetailCode,A.DetailName " + "From   Tbl_ProductTreeDetails A INNER JOIN dbo.Tbl_ProductTree B ON A.TreeCode=B.TreeCode " + "Where  A.DetailCode IN (Select D.DetailCode From dbo.Tbl_Planning C INNER JOIN " + "                                          dbo.Tbl_ProductOPCs D ON C.TreeCode = D.TreeCode And C.OperationCode = D.OperationCode) " + "And ProductCode='", cbProduct_Lookup.CB_SelectedValue), "' And DefualtTree = 1")), Module1.cnProductionPlanning);
                daTreeDetails.Fill(dtTreeDetails);
                daTreeDetails.Dispose();
                {
                    var withBlock = cbTreeDetails;
                    withBlock.DataSource = dtTreeDetails;
                    withBlock.DisplayMember = "DetailName";
                    withBlock.ValueMember = "DetailCode";
                    withBlock.SelectedIndex = -1;
                }
            }

        }

        






        private void _cbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbProduct_Lookup_Load(object sender, EventArgs e)
        {

        }

        private void txtFromDate_Load(object sender, EventArgs e)
        {

        }
    }
}