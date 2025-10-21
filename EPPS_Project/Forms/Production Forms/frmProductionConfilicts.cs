using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmProductionConfilicts
    {
        public frmProductionConfilicts()
        {
            InitializeComponent();
            _dgList.Name = "dgList";
            _cmdPrintPreview.Name = "cmdPrintPreview";
            _cmdExit.Name = "cmdExit";
        }

        private DataTable mdtConfilicts = new DataTable();

        public object dtConfilicts
        {
            set
            {
                mdtConfilicts = (DataTable)value;
            }
        }

        private void dgList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true & e.KeyCode == Keys.F12)
            {
                Module1.ExportGridToExcel(dgList);
            }
        }

        private void cmdPrintPreview_Click(object sender, EventArgs e)
        {
            try
            {
                var cmPrint = new System.Data.SqlClient.SqlCommand("If exists (Select * From dbo.sysobjects Where id = object_id(N'dbo.Tbl_TempConfilicts') and OBJECTPROPERTY(id, N'IsUserTable') = 1) " + "   drop table dbo.Tbl_TempConfilicts " + "Create Table dbo.Tbl_TempConfilicts(" + "             OperationCode  varchar (50) Collate Arabic_CS_AS_KS_WS NOT NULL ," + "             OperationTitle varchar (100) Collate Arabic_CS_AS_KS_WS NOT NULL ," + "             SubbatchCode   varchar (20) Collate Arabic_CS_AS_KS_WS NOT NULL ," + "             ConfilictDescription varchar (500) Collate Arabic_CS_AS_KS_WS NOT NULL) ON [PRIMARY]", Module1.cnProductionPlanning);
                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();
                cmPrint.ExecuteNonQuery();
                for (int I = 0, loopTo = mdtConfilicts.Rows.Count - 1; I <= loopTo; I++)
                {
                    cmPrint.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Insert Into Tbl_TempConfilicts(OperationCode,OperationTitle,SubbatchCode,ConfilictDescription)" + "Values('", mdtConfilicts.Rows[I]["OperationCode"]), "','"), mdtConfilicts.Rows[I]["OperationTitle"]), "','"), mdtConfilicts.Rows[I]["SubbatchCode"]), "','"), mdtConfilicts.Rows[I]["ConfilictDescription"]), "')"));
                    cmPrint.ExecuteNonQuery();
                }

                {
                    var withBlock = new frmPrintPreview();
                    withBlock.ReporetName = "Rpt102_ProductionConfilicts.rpt";
                    withBlock.ShowType = 1;
                    withBlock.ParameterCont = 1;
                    withBlock.set_Param(0, Module1.mServerShamsiDate);
                    withBlock.MdiParent = MdiParent;
                    withBlock.Show();
                }
            }
            catch (Exception objEx)
            {
                Logger.SaveError(Name + ".cmdPrintPreview_Click", objEx.Message);
                MessageBox.Show("اشکال در فراخوانی گزارش", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
            finally
            {
                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                    Module1.cnProductionPlanning.Close();
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmProductionConfilicts_FormClosing(object sender, FormClosingEventArgs e)
        {
            Module1.SaveGridColumnsWidth(Name, 0, dgList);
            using (var cn = new System.Data.SqlClient.SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    var cmExistsTable = new System.Data.SqlClient.SqlCommand("If exists (Select * From dbo.sysobjects Where id = object_id(N'dbo.Tbl_TempConfilicts') and OBJECTPROPERTY(id, N'IsUserTable') = 1) " + " drop table dbo.Tbl_TempConfilicts ", cn);
                    cmExistsTable.ExecuteNonQuery();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    Logger.LogException("frmProductionConfilicts_FormClosing", ex);
                }
            }
        }

        private void frmProductionConfilicts_Load(object sender, EventArgs e)
        {
            dgList.Sorted += Module1.DataGridViews_Sorted_EventHandler;
            Module1.SetGridColumnsWidth(Name, 0, dgList);
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