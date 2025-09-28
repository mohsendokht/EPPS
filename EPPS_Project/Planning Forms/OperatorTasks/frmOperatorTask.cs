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
using static ProductionPlanning.Module1;


namespace ProductionPlanning.Planning_Forms.OperatorTasks
{
    public partial class frmOperatorTask : Form
    {
        public FormModeEnum frmMode = FormModeEnum.INSERT_MODE;
        public string OperatorTaskID = "";
        public String PlanningCode = "";
        public Model.OperatorTask OperatorTask;
        public frmOperatorTask()
        {
            InitializeComponent();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            string Sql = "";
            switch (frmMode)
            {
                //(int)Module1.FormModeEnum.EDIT_MODE
                case FormModeEnum.INSERT_MODE:
                    Sql = @$"INSERT INTO [dbo].[Tbl_OperatorTask]
                                      ([OperatorCode],[PlanningCode],[StartDate],[StartHour],[EndDate],[EndHour])
                               VALUES ('{Operator_Lookup.CB_SelectedValue}',{this.PlanningCode},'{StartDateTBox.Text}','{StartHourTBox.Text}','{EndDateTBox.Text}','{EndHourTBox.Text}')";

                    break;

                case FormModeEnum.DELETE_MODE:
                    MessageBox.Show("Do a Delete");
                    break;

                case FormModeEnum.EDIT_MODE:
                    Sql = @$"update [dbo].[Tbl_OperatorTask] set[PlanningCode]='{this.PlanningCode}',
                           [OperatorCode]='{Operator_Lookup.CB_SelectedValue}',
                           [StartDate]='{StartDateTBox.Text}',[StartHour]='{StartHourTBox.Text}'
                          ,[EndDate]='{EndDateTBox.Text}',[EndHour]='{EndHourTBox.Text}'
                          where Id=" + OperatorTaskID;
                   
                    break;
                default: //FormModeEnum.EDIT_MODE

                    MessageBox.Show("Do an Update");
                    break;
            }
            try
            {
                if (string.IsNullOrEmpty(Sql)) return;
                using (var cn = new SqlConnection(Module1.PlanningCnnStr))
                {
                    var SqlCmd = new SqlCommand(Sql, cn);
                    if (cn.State == ConnectionState.Closed) cn.Open();
                    SqlCmd.ExecuteNonQuery();
                    cn.Close();
                    Close();
                }
            }
            catch (Exception Excp)
            {
                Logger.LogException("cmdSave_Click", Excp);
            }

        }

        private void frmTaskAssign_Load(object sender, EventArgs e)
        {
            // Load Planning item data
            string SelectSQL = @"Select 
                                      Tbl_Planning.PlanningCode,
                                      Product    = Tbl_ProductTree.ProductCode + ' - ' + Tbl_Products.ProductName,
                                      Operation  = Tbl_Planning.OperationCode + ' - ' + Tbl_ProductOPCs.OperationTitle,
                                      Machine    = Case Tbl_ProductOPCs.ExecutionMethod 
                                                         WHEN 1 Then Tbl_Planning.MachineCode + ' - ' + Tbl_Machines.Name
                                                         WHEN 2 Then 'عمليات اپراتوري' 
                                                         When 3 Then 'عمليات پيمانکاري' 
                                                   End,
                                      Duration   = dbo.GetRegularTimeFromFloatingTime(Tbl_Planning.PlanningDuration),
                                      StartDT    = dbo.GetRegularTimeFromFloatingTime(Tbl_Planning.PlanningStartHour)  + ' ' 
                                                 + SUBSTRING(Tbl_Planning.PlanningStartDate,1,4) + '/'
                                                 + SUBSTRING(Tbl_Planning.PlanningStartDate,5,2) + '/'
                                                 + SUBSTRING(Tbl_Planning.PlanningStartDate,7,2) ,
                                      FinishDT   = dbo.GetRegularTimeFromFloatingTime(Tbl_Planning.PlanningEndHour)  + ' ' 
                                                 + SUBSTRING(Tbl_Planning.PlanningEndDate,1,4) + '/'
                                                 + SUBSTRING(Tbl_Planning.PlanningEndDate,5,2) + '/'
                                                 + SUBSTRING(Tbl_Planning.PlanningEndDate,7,2) ,
                                      Tbl_ProductionSubbatchs.SubbatchCode,
                                      Tbl_ProductionBatchs.BatchCode,
                                 	  Tbl_OperatorTaskID = Tbl_OperatorTask.ID,
                                 	  Tbl_Operators.OperatorCode,
                                 	  Tbl_Operators.OperatorName,
                                 	  Tbl_OperatorTask.StartDate,
                                 	  Tbl_OperatorTask.StartHour,
                                 	  Tbl_OperatorTask.EndDate,
                                      Tbl_OperatorTask.EndHour
                                 
                                 From Tbl_Planning  
                                 	 INNER JOIN Tbl_ProductionSubbatchs ON Tbl_ProductionSubbatchs.SubbatchCode = Tbl_Planning.SubbatchCode   
                                 	 INNER JOIN Tbl_ProductionBatchs    ON Tbl_ProductionBatchs.BatchCode = Tbl_ProductionSubbatchs.BatchCode 
                                 	 INNER JOIN Tbl_ProductTree         ON Tbl_ProductTree.TreeCode = Tbl_ProductionBatchs.ProductTreeCode  
                                     INNER JOIN Tbl_Products            ON Tbl_Products.ProductCode = Tbl_ProductTree.ProductCode
                                 	 INNER JOIN Tbl_ProductOPCs         ON Tbl_Planning.TreeCode = Tbl_ProductOPCs.TreeCode And Tbl_Planning.OperationCode = Tbl_ProductOPCs.OperationCode  
                                 	 LEFT  JOIN Tbl_Machines            ON Tbl_Machines.Code = Tbl_Planning.MachineCode
                                     LEFT  JOIN Tbl_OperatorTask        ON Tbl_OperatorTask.PlanningCode  = Tbl_Planning.PlanningCode
                                     LEFT  JOIN Tbl_Operators           ON Tbl_Operators.OperatorCode = Tbl_OperatorTask.OperatorCode
                               ";

            SetComboBoxsDataSource();

            string WhereSQL = "";
            if (!string.IsNullOrEmpty(this.PlanningCode))
            {
                WhereSQL = " WHERE Tbl_Planning.PlanningCode = " + this.PlanningCode;
            }

            if (!string.IsNullOrEmpty(this.OperatorTaskID))
            {
                WhereSQL = " WHERE Tbl_OperatorTask.ID = " + this.OperatorTaskID;
            }

            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                var SqlCmd = new SqlCommand(SelectSQL + WhereSQL, cn);

                cn.Open();
                SqlDataReader SqlDr = SqlCmd.ExecuteReader();
                if (SqlDr.Read())
                {
                    ProductTBox.Text = SqlDr["Product"].ToString();
                    MachineTBox.Text = SqlDr["Machine"].ToString();
                    OperationTBox.Text = SqlDr["Operation"].ToString();
                    BatchTBox.Text = SqlDr["BatchCode"].ToString() + " ( " + SqlDr["SubbatchCode"].ToString() + " )";
                    if (!string.IsNullOrEmpty(SqlDr["OperatorCode"].ToString()))
                        Operator_Lookup.CB_SelectedValue = SqlDr["OperatorCode"].ToString();
                    StartHourTBox.Text = SqlDr["StartHour"].ToString();
                    StartDateTBox.Text = SqlDr["StartDate"].ToString();
                    EndHourTBox.Text = SqlDr["EndHour"].ToString();
                    EndDateTBox.Text = SqlDr["EndDate"].ToString();
                }
                SqlDr.Close();
            }

        }

        private void SetComboBoxsDataSource()
        {
            var daCombo = new SqlDataAdapter("", Module1.cnProductionPlanning);
            var dtCombo = new DataTable();

            daCombo.SelectCommand.CommandText = "Select OperatorCode,OperatorName From Tbl_Operators Order By OperatorName";
            daCombo.Fill(dtCombo);
            Operator_Lookup.CB_DataSource = dtCombo;
            Operator_Lookup.CB_DisplayMember = "OperatorCode";
            Operator_Lookup.CB_ValueMember = "OperatorName";
            Operator_Lookup.CB_SelectedIndex = -1;

        }


    }
}
