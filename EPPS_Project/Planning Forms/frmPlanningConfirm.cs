using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using static ProductionPlanning.MyEnums;


namespace ProductionPlanning
{
    public partial class frmPlanningConfirm
    {
        public frmPlanningConfirm()
        {
            InitializeComponent();
            _dgEndConfilicts.Name = "dgEndConfilicts";
            _dgStartConfilicts.Name = "dgStartConfilicts";
        }

        private string mSubbatchCode = Constants.vbNullString;
        private string mTreeCode = Constants.vbNullString;
        private SqlTransaction mTrn = null;
        private DataSet mDS = null;

        public string SubbatchCode
        {
            set
            {
                mSubbatchCode = value;
            }
        }

        public string TreeCode
        {
            set
            {
                mTreeCode = value;
            }
        }

        public SqlTransaction ActiveTransaction
        {
            set
            {
                mTrn = value;
            }
        }

        public DataSet ActiveDataSet
        {
            set
            {
                mDS = value;
            }
        }

        private void dgStartConfilicts_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            lblOpMachine.Text = "";
            lblOpOET.Text = "";
            lblPreOpMachine.Text = "";
            lblPreOpPET.Text = "";
            lblRelationType.Text = "";
            lblLagTime.Text = "";
            if (dgStartConfilicts.SelectedRows.Count > 0)
            {
                string mOpCode = dgStartConfilicts.SelectedRows[0].Cells[0].Value.ToString();
                string mPreOpCode = dgStartConfilicts.SelectedRows[0].Cells[2].Value.ToString();
                var mPlanningRows = mDS.Tables["Tbl_PLanning"].Select("SubbatchCode = '" + mSubbatchCode + "' And TreeCode = " + mTreeCode + " And OperationCode = '" + mOpCode + "'");
                var cm = new SqlCommand("", mTrn.Connection);
                SqlDataReader dr = null;
                // If dgEndConfilicts.SelectedRows.Count > 0 Then
                // dgEndConfilicts.SelectedRows(0).Selected = False
                // End If

                try
                {
                    cm.Transaction = mTrn;
                    GroupBox2.Text = "عملیات جاری {" + mOpCode + "}";
                    GroupBox3.Text = "عملیات پیشنیاز {" + mPreOpCode + "}";

                    // بدست آوردن مشخصات عملیات جاری
                   
                    if (mPlanningRows.Length > 0)
                    {
                        lblOpMachine.Text = Conversions.ToString(mPlanningRows[0]["MachineCode"]);
                    }

                    cm.CommandText = "Select ExecutionMethod From Tbl_ProductOPCs Where TreeCode = " + mTreeCode + " And OperationCode = '" + mOpCode + "'";
                    dr = cm.ExecuteReader();
                    if (dr.Read())
                    {                                                       
                        switch ((EnumExecutionMethod)dr["ExecutionMethod"])
                        {
                            case EnumExecutionMethod.EM_MACHINE:
                                {
                                    cm.CommandText = "Select OneExecutionTime, TimeType From Tbl_ProductOPCsExecutorMachines Where TreeCode = " + mTreeCode + " And OperationCode = '" + mOpCode + "' And MachineCode = '" + lblOpMachine.Text + "'";
                                    break;
                                }

                            case EnumExecutionMethod.EM_NONMACHINE:
                                {
                                    cm.CommandText = "Select OneExecutionTime, TimeType From Tbl_ProductOPCsExecutorMachines Where TreeCode = " + mTreeCode + " And OperationCode = '" + mOpCode + "' And MachineCode = '" + lblOpMachine.Text + "'";
                                    lblOpMachine.Text = "اپراتوری";
                                    break;
                                }

                            case EnumExecutionMethod.EM_CONTRACTOR:
                                {
                                    cm.CommandText = "Select (Convert(Float,TransferBatchExecutionTime)/Convert(Float,BatchCapacity)) As OneExecutionTime, TimeType From Tbl_ContractorOperations Where TreeCode = " + mTreeCode + " And OperationCode = '" + mOpCode + "'";
                                    lblOpMachine.Text = "پیمانکاری";
                                    break;
                                }
                        }
                    }

                    dr.Close();
                    dr = cm.ExecuteReader();
                    if (dr.Read())
                    {
                        lblOpOET.Text = dr["OneExecutionTime"].ToString() + " " + GetTimeTypeName(Conversions.ToInteger(dr["TimeType"]));
                    }

                    dr.Close();

                    // بدست آوردن مشخصات عملیات پیشنیاز
                    mPlanningRows = mDS.Tables["Tbl_PLanning"].Select("SubbatchCode = '" + mSubbatchCode + "' And TreeCode = " + mTreeCode + " And OperationCode = '" + mPreOpCode + "'");
                    if (mPlanningRows.Length > 0)
                    {
                        lblPreOpMachine.Text = Conversions.ToString(mPlanningRows[0]["MachineCode"]);
                    }

                    cm.CommandText = "Select ExecutionMethod From Tbl_ProductOPCs Where TreeCode = " + mTreeCode + " And OperationCode = '" + mPreOpCode + "'";
                    dr = cm.ExecuteReader();
                    if (dr.Read())
                    {
                        switch ((EnumExecutionMethod)dr["ExecutionMethod"])
                        {
                            case EnumExecutionMethod.EM_MACHINE:
                                {
                                    cm.CommandText = "Select OneExecutionTime, TimeType From Tbl_ProductOPCsExecutorMachines Where TreeCode = " + mTreeCode + " And OperationCode = '" + mPreOpCode + "' And MachineCode = '" + lblPreOpMachine.Text + "'";
                                    break;
                                }

                            case EnumExecutionMethod.EM_NONMACHINE:
                                {
                                    cm.CommandText = "Select OneExecutionTime, TimeType From Tbl_ProductOPCsExecutorMachines Where TreeCode = " + mTreeCode + " And OperationCode = '" + mPreOpCode + "' And MachineCode = '" + lblPreOpMachine.Text + "'";
                                    lblPreOpMachine.Text = "اپراتوری";
                                    break;
                                }

                            case EnumExecutionMethod.EM_CONTRACTOR:
                                {
                                    cm.CommandText = "Select (Convert(Float,TransferBatchExecutionTime)/Convert(Float,BatchCapacity)) As OneExecutionTime, TimeType From Tbl_ContractorOperations Where TreeCode = " + mTreeCode + " And OperationCode = '" + mPreOpCode + "'";
                                    lblPreOpMachine.Text = "پیمانکاری";
                                    break;
                                }
                        }
                    }

                    dr.Close();
                    dr = cm.ExecuteReader();
                    if (dr.Read())
                    {
                        lblPreOpPET.Text = dr["OneExecutionTime"].ToString() + " " + GetTimeTypeName(Conversions.ToInteger(dr["TimeType"]));
                    }

                    dr.Close();

                    // بدست آوردن مشخصات رابطۀ پیشنیازی
                    cm.CommandText = "Select RelationType, LagTime, TimeType From Tbl_PreOperations Where TreeCode = " + mTreeCode + " And CurrentOperationCode = '" + mOpCode + "' And PreOperationCode = '" + mPreOpCode + "'";
                    dr = cm.ExecuteReader();
                    if (dr.Read())
                    {
                        lblRelationType.Text = GetRelationTypeName(Conversions.ToInteger(dr["RelationType"]));
                        lblLagTime.Text = dr["LagTime"].ToString() + " " + GetTimeTypeName(Conversions.ToInteger(dr["TimeType"]));
                    }

                    dr.Close();
                }
                catch (Exception ex)
                {
                    if (!dr.IsClosed) dr.Close();
                    Logger.LogException("dgStartConfilicts_RowEnter", ex);
                  
                }
            }
        }

        private void dgEndConfilicts_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            lblOpMachine.Text = "";
            lblOpOET.Text = "";
            lblPreOpMachine.Text = "";
            lblPreOpPET.Text = "";
            lblRelationType.Text = "";
            lblLagTime.Text = "";
            if (dgEndConfilicts.SelectedRows.Count > 0)
            {
                string mOpCode = dgEndConfilicts.SelectedRows[0].Cells[0].Value.ToString();
                string mPreOpCode = dgEndConfilicts.SelectedRows[0].Cells[2].Value.ToString();
                var mPlanningRows = mDS.Tables["Tbl_PLanning"].Select("SubbatchCode = '" + mSubbatchCode + "' And TreeCode = " + mTreeCode + " And OperationCode = '" + mOpCode + "'");
                var cm = new SqlCommand("", mTrn.Connection);

                // If dgStartConfilicts.SelectedRows.Count > 0 Then
                // dgStartConfilicts.SelectedRows(0).Selected = False
                // End If
                SqlDataReader dr = null;
                try
                {
                    cm.Transaction = mTrn;
                    GroupBox2.Text = "عملیات جاری {" + mOpCode + "}";
                    GroupBox3.Text = "عملیات پیشنیاز {" + mPreOpCode + "}";

                    // بدست آوردن مشخصات عملیات جاری
                    
                    if (mPlanningRows.Length > 0)
                    {
                        lblOpMachine.Text = Conversions.ToString(mPlanningRows[0]["MachineCode"]);
                    }

                    cm.CommandText = "Select ExecutionMethod From Tbl_ProductOPCs Where TreeCode = " + mTreeCode + " And OperationCode = '" + mOpCode + "'";
                    dr = cm.ExecuteReader();
                    if (dr.Read())
                    {
                        switch ((EnumExecutionMethod)dr["ExecutionMethod"])
                        {
                            case EnumExecutionMethod.EM_MACHINE:
                                {
                                    cm.CommandText = "Select OneExecutionTime, TimeType From Tbl_ProductOPCsExecutorMachines Where TreeCode = " + mTreeCode + " And OperationCode = '" + mOpCode + "' And MachineCode = '" + lblOpMachine.Text + "'";
                                    break;
                                }

                            case EnumExecutionMethod.EM_NONMACHINE:
                                {
                                    cm.CommandText = "Select OneExecutionTime, TimeType From Tbl_ProductOPCsExecutorMachines Where TreeCode = " + mTreeCode + " And OperationCode = '" + mOpCode + "' And MachineCode = '" + lblOpMachine.Text + "'";
                                    lblOpMachine.Text = "اپراتوری";
                                    break;
                                }

                            case EnumExecutionMethod.EM_CONTRACTOR:
                                {
                                    cm.CommandText = "Select (Convert(Float,TransferBatchExecutionTime)/Convert(Float,BatchCapacity)) As OneExecutionTime, TimeType From Tbl_ContractorOperations Where TreeCode = " + mTreeCode + " And OperationCode = '" + mOpCode + "'";
                                    lblOpMachine.Text = "پیمانکاری";
                                    break;
                                }
                        }
                    }

                    dr.Close();
                    dr = cm.ExecuteReader();
                    if (dr.Read())
                    {
                        lblOpOET.Text = dr["OneExecutionTime"].ToString() + " " + GetTimeTypeName(Conversions.ToInteger(dr["TimeType"]));
                    }

                    dr.Close();

                    // بدست آوردن مشخصات عملیات پیشنیاز
                    mPlanningRows = mDS.Tables["Tbl_PLanning"].Select("SubbatchCode = '" + mSubbatchCode + "' And TreeCode = " + mTreeCode + " And OperationCode = '" + mPreOpCode + "'");
                    if (mPlanningRows.Length > 0)
                    {
                        lblPreOpMachine.Text = Conversions.ToString(mPlanningRows[0]["MachineCode"]);
                    }

                    cm.CommandText = "Select ExecutionMethod From Tbl_ProductOPCs Where TreeCode = " + mTreeCode + " And OperationCode = '" + mPreOpCode + "'";
                    dr = cm.ExecuteReader();
                    if (dr.Read())
                    {
                        switch ((EnumExecutionMethod)dr["ExecutionMethod"])
                        {
                            case EnumExecutionMethod.EM_MACHINE:
                                {
                                    cm.CommandText = "Select OneExecutionTime, TimeType From Tbl_ProductOPCsExecutorMachines Where TreeCode = " + mTreeCode + " And OperationCode = '" + mPreOpCode + "' And MachineCode = '" + lblPreOpMachine.Text + "'";
                                    break;
                                }

                            case EnumExecutionMethod.EM_NONMACHINE:
                                {
                                    cm.CommandText = "Select OneExecutionTime, TimeType From Tbl_ProductOPCsExecutorMachines Where TreeCode = " + mTreeCode + " And OperationCode = '" + mPreOpCode + "' And MachineCode = '" + lblPreOpMachine.Text + "'";
                                    lblPreOpMachine.Text = "اپراتوری";
                                    break;
                                }

                            case EnumExecutionMethod.EM_CONTRACTOR:
                                {
                                    cm.CommandText = "Select (Convert(Float,TransferBatchExecutionTime)/Convert(Float,BatchCapacity)) As OneExecutionTime, TimeType From Tbl_ContractorOperations Where TreeCode = " + mTreeCode + " And OperationCode = '" + mPreOpCode + "'";
                                    lblPreOpMachine.Text = "پیمانکاری";
                                    break;
                                }
                        }
                    }

                    dr.Close();
                    dr = cm.ExecuteReader();
                    if (dr.Read())
                    {
                        lblPreOpPET.Text = dr["OneExecutionTime"].ToString() + " " + GetTimeTypeName(Conversions.ToInteger(dr["TimeType"]));
                    }

                    dr.Close();

                    // بدست آوردن مشخصات رابطۀ پیشنیازی
                    cm.CommandText = "Select RelationType, LagTime, TimeType From Tbl_PreOperations Where TreeCode = " + mTreeCode + " And CurrentOperationCode = '" + mOpCode + "' And PreOperationCode = '" + mPreOpCode + "'";
                    dr = cm.ExecuteReader();
                    if (dr.Read())
                    {
                        lblRelationType.Text = GetRelationTypeName(Conversions.ToInteger(dr["RelationType"]));
                        lblLagTime.Text = dr["LagTime"].ToString() + " " + GetTimeTypeName(Conversions.ToInteger(dr["TimeType"]));
                    }

                    dr.Close();
                }
                catch (Exception ex)
                {
                    if (!dr.IsClosed) dr.Close();
                    Logger.LogException("dgEndConfilicts_RowEnter", ex);
                }
            }
        }

        private string GetTimeTypeName(int mTypeCode)
        {
            string mTypeName = Constants.vbNullString;
            switch (mTypeCode)
            {
                case 1:
                    {
                        mTypeName = "ثانیه";
                        break;
                    }

                case 2:
                    {
                        mTypeName = "دقیقه";
                        break;
                    }

                case 3:
                    {
                        mTypeName = "ساعت";
                        break;
                    }

                case 4:
                    {
                        mTypeName = "روز";
                        break;
                    }

                case 5:
                    {
                        mTypeName = "هفته";
                        break;
                    }

                case 6:
                    {
                        mTypeName = "ماه";
                        break;
                    }
            }

            return mTypeName;
        }

        private string GetRelationTypeName(int mTypeCode)
        {
            string mTypeName = Constants.vbNullString;
            switch (mTypeCode)
            {
                case 1:
                    {
                        mTypeName = "FS";
                        break;
                    }

                case 2:
                    {
                        mTypeName = "FF";
                        break;
                    }

                case 3:
                    {
                        mTypeName = "SS";
                        break;
                    }

                case 4:
                    {
                        mTypeName = "SF";
                        break;
                    }

                case 5:
                    {
                        mTypeName = "ASAP";
                        break;
                    }
            }

            return mTypeName;
        }

        
    }
}