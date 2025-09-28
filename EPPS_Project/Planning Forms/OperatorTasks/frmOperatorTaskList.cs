using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductionPlanning.Planning_Forms.OperatorTasks
{
    public partial class frmOperatorTaskList : Form
    {
        private DataSet PlanningDataset = new DataSet();

        public frmOperatorTaskList()
        {
            InitializeComponent();
        }

        private void frmOperatorTaskList_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                Left = 10;
                Top = 10;
              
                SetComboBoxsDataSource();
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                Logger.SaveError("frmOperatorTaskList_Load", ex.Message);
                MessageBox.Show("فراخوانی فرم نمایش برنامه ریزی با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                Close();
            }
        }

        private void SetComboBoxsDataSource()
        {
            var daCombo = new SqlDataAdapter("", Module1.cnProductionPlanning);
            var dtCombo = new DataTable();
            
            daCombo.SelectCommand.CommandText = "Select ProductCode,ProductName From Tbl_Products Order By ProductName";
            daCombo.Fill(dtCombo);
            cbProduct_Lookup.CB_DataSource = dtCombo;
            cbProduct_Lookup.CB_DisplayMember = "ProductCode";
            cbProduct_Lookup.CB_ValueMember = "ProductName";
            cbProduct_Lookup.CB_SelectedIndex = -1;


            daCombo.SelectCommand.CommandText = "Select Code, Code+' '+Name As MachineName From Tbl_Machines Where Code <> '-1'";
            dtCombo = new DataTable();
            daCombo.Fill(dtCombo);
            Machine_Lookup.CB_DataSource = dtCombo.DefaultView;
            Machine_Lookup.CB_DisplayMember = "Code";
            Machine_Lookup.CB_ValueMember = "MachineName";
            Machine_Lookup.CB_SelectedIndex = -1;
            
            
            daCombo.SelectCommand.CommandText = "Select A.BatchCode, B.ProductCode " + "From   dbo.Tbl_ProductionBatchs A INNER JOIN " + "       dbo.Tbl_ProductTree B ON A.ProductTreeCode = B.TreeCode";
            dtCombo = new DataTable();
            daCombo.Fill(dtCombo);
            cbBatch_Lookup.CB_DataSource = dtCombo.DefaultView;
            cbBatch_Lookup.CB_DisplayMember = "BatchCode";
            cbBatch_Lookup.CB_ValueMember = "BatchCode";
            cbBatch_Lookup.CB_SelectedIndex = -1;
            
            daCombo.SelectCommand.CommandText = "Select SubbatchCode,BatchCode From Tbl_ProductionSubbatchs";
            dtCombo = new DataTable();
            daCombo.Fill(dtCombo);
            
            cbSubbatch_LookUp.CB_DataSource = dtCombo.DefaultView;
            cbSubbatch_LookUp.CB_DisplayMember = "SubbatchCode";
            cbSubbatch_LookUp.CB_ValueMember = "SubbatchCode";
            cbSubbatch_LookUp.CB_SelectedIndex = -1;
            
        }

        private void _cmdShow_Click(object sender, EventArgs e)
        {
            
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
                                   StartDT       =   dbo.GetRegularTimeFromFloatingTime(Tbl_Planning.PlanningStartHour)  + ' ' 
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
                             	   Tbl_ProductTree.TreeCode,
                             	   Tbl_Planning.PlanningCode
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

            string Filter = GetFilterValue();
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

        private void zzClearFilter()
        {
            txtFromDate.Text = Constants.vbNullString;
            txtToDate.Text = Constants.vbNullString;
           
            Machine_Lookup.ResetText();
            cbProduct_Lookup.ResetText();
            Machine_Lookup.CB_SelectedIndex = -1;
        }

        private string GetFilterValue()
        {
            string Filter = "";
            
            
            if (!string.IsNullOrEmpty(cbProduct_Lookup.CB_SelectedValue))
            {
                Filter += $" Tbl_ProductTree.ProductCode ='{cbProduct_Lookup.CB_SelectedValue} '";
            }

 
            if (!string.IsNullOrEmpty(Machine_Lookup.CB_SelectedValue))
            {
                Filter += (Filter == "" ? "": " AND ") + $" Tbl_Planning.MachineCode ='{Machine_Lookup.CB_SelectedValue} '";
            }

            if (!string.IsNullOrEmpty(cbBatch_Lookup.CB_SelectedValue))
            {
                Filter += (Filter == "" ? "" : " AND ") +  $" Tbl_ProductionBatchs.BatchCode ='{cbBatch_Lookup.CB_SelectedValue}'";
            }

            if (!string.IsNullOrEmpty(cbSubbatch_LookUp.CB_SelectedValue))
            {
                Filter += (Filter == "" ? "" : " AND ") +  $" Tbl_Planning.SubbatchCode ='{cbSubbatch_LookUp.CB_SelectedValue}'";
            }

            if (!string.IsNullOrEmpty(txtFromDate.Text))
            {
                Filter += (Filter == "" ? "" : " AND ") +  $" Tbl_Planning.PlanningStartDate >='{Strings.Replace(txtFromDate.Text, "/", "")}'";
            }

            if (!string.IsNullOrEmpty(txtToDate.Text))
            {
                Filter += (Filter == "" ? "" : " AND ") +  $" Tbl_Planning.PlanningStartDate <= '{ Strings.Replace(txtToDate.Text, "/", "")}'";
            }

            
            return Filter;
        }

        private void zzSetGridColumns()
        {
            int C;


            dgList.Columns[0].HeaderText = "محصول";
            dgList.Columns[1].HeaderText = "کد عملیات";
            dgList.Columns[2].HeaderText = "عنوان عملیات";
            dgList.Columns[3].HeaderText = "ماشین ";
            dgList.Columns[4].HeaderText = "تعداد تولید";
            dgList.Columns[5].HeaderText = "زمان عملیات";
            dgList.Columns[6].HeaderText = "Setup ماشین";
            dgList.Columns[7].HeaderText = "شروع ";
            dgList.Columns[8].HeaderText = "پایان ";
            dgList.Columns[9].HeaderText = "بچ تولید";
            dgList.Columns[10].HeaderText = "وضعیت بچ";
           
            for (C = 11; C <= dgList.Columns.Count - 1; C++)
                dgList.Columns[C].Visible = false;
            

            Module1.SetGridColumnsWidth(Name, 0, dgList);
        }

        private void _cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgList.Rows.Count == 0) return;
            if (e.ColumnIndex != -1) return;


            OpMachine.Text = dgList.CurrentRow.Cells["MachineCode"].Value.ToString();
            OpCode.Text = dgList.CurrentRow.Cells["OperationCode"].Value.ToString() + " " + dgList.CurrentRow.Cells["OperationTitle"].Value.ToString()  ;

            PlanningCodeTBox.Text = dgList.CurrentRow.Cells["PlanningCode"].Value.ToString();
            if (!string.IsNullOrEmpty(PlanningCodeTBox.Text))
                zzLoadAssignedOperator(PlanningCodeTBox.Text);

        }

        private void zzLoadAssignedOperator(string PlanningCode)
        {
            string SelectSQL = @"SELECT 
                                     Tbl_Operators.OperatorName,
                                     StartDT   = Tbl_OperatorTask.StartHour  + ' ' 
                             				   + SUBSTRING(Tbl_OperatorTask.StartDate,1,4) + '/'
                             				   + SUBSTRING(Tbl_OperatorTask.StartDate,5,2) + '/'
                             				   + SUBSTRING(Tbl_OperatorTask.StartDate,7,2) ,
                             	     FinishDT  = Tbl_OperatorTask.EndHour  + ' ' 
                             				   + SUBSTRING(Tbl_OperatorTask.EndDate,1,4) + '/'
                             				   + SUBSTRING(Tbl_OperatorTask.EndDate,5,2) + '/'
                             				   + SUBSTRING(Tbl_OperatorTask.EndDate,7,2) ,
                                     Tbl_OperatorTask.ID,
                                     Tbl_OperatorTask.OperatorCode,
                                     Tbl_OperatorTask.PlanningCode,
                                     Tbl_OperatorTask.StartDate,
	                                 Tbl_OperatorTask.StartHour,
                                     Tbl_OperatorTask.EndDate,
                                     Tbl_OperatorTask.EndHour
                                 FROM Tbl_OperatorTask
                                      INNER JOIN Tbl_Operators ON Tbl_Operators.OperatorCode = Tbl_OperatorTask.OperatorCode
                                 WHERE PlanningCode = " + PlanningCode +
                               " ORDER BY OperatorName ";

            PlanningDataset = DbTool.GetDataSet("Tbl_OperatorTask", SelectSQL);
            dgOperators.DataSource = PlanningDataset.Tables["Tbl_OperatorTask"].DefaultView;
            // set columns
            dgOperators.Columns[0].HeaderText = "نام اپراتور";
            dgOperators.Columns[1].HeaderText = "شروع";
            dgOperators.Columns[2].HeaderText = "پایان";
            
            for (var C = 3; C <= dgOperators.Columns.Count - 1; C++)
                dgOperators.Columns[C].Visible = false;
        }

        private void NewOperatorTask_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PlanningCodeTBox.Text)) return;

            frmOperatorTask objForm = new frmOperatorTask();
            objForm.frmMode = Module1.FormModeEnum.INSERT_MODE;
            objForm.PlanningCode = PlanningCodeTBox.Text;
            
            objForm.ShowDialog();
            objForm.Dispose();

            if (!string.IsNullOrEmpty(PlanningCodeTBox.Text))
                zzLoadAssignedOperator(PlanningCodeTBox.Text);
        }

        private void EditOperatorTask_Click(object sender, EventArgs e)
        {

            if (dgOperators.SelectedRows.Count == 0) return;
          
            var OperatorTaskId =  dgOperators.CurrentRow.Cells["ID"].Value.ToString();
            
            if(string.IsNullOrEmpty(OperatorTaskId)) return;
                       
            LoadfrmOperatorTask(Module1.FormModeEnum.EDIT_MODE, OperatorTaskId);
        }

        private void LoadfrmOperatorTask(Module1.FormModeEnum Mode, string OperatorTaskId= "")
        {
            if (string.IsNullOrEmpty(PlanningCodeTBox.Text)) return;

            frmOperatorTask objForm = new frmOperatorTask();
            objForm.frmMode = Mode;
            objForm.PlanningCode = PlanningCodeTBox.Text;
            objForm.OperatorTaskID = OperatorTaskId;
            objForm.ShowDialog();
            objForm.Dispose();

            if (!string.IsNullOrEmpty(PlanningCodeTBox.Text))
                zzLoadAssignedOperator(PlanningCodeTBox.Text);
        }

        private void dgList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DeleteOperatorTask_Click(object sender, EventArgs e)
        {
            if (dgOperators.SelectedRows.Count == 0) return;

        }
    }
}
