using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProductionPlanning
{
    public class DataSetConfiguration
    {
        private SqlDataAdapter mdaProductionPlanning;
        private DataSet mdsProductionPlanning;

        public DataSet dsProductionPlanning
        {
            get
            {
                return mdsProductionPlanning;
            }

            set
            {
                mdsProductionPlanning = value;
            }
        }

        private SqlDataAdapter daProductionPlanning
        {
            get
            {
                return mdaProductionPlanning;
            }

            set
            {
                mdaProductionPlanning = value;
            }
        }

        public DataSetConfiguration()
        {
            mdaProductionPlanning = new SqlDataAdapter();
            mdsProductionPlanning = new DataSet();
        }

        public bool FillDataSet(string srcTableName, string Table, string SelectCommandText, params string[] PrimaryKeyCols)
        {
            var cmSelect = new SqlCommand(SelectCommandText, Module1.cnProductionPlanning);
            try
            {
                daProductionPlanning.SelectCommand = cmSelect;
                daProductionPlanning.FillSchema(dsProductionPlanning, SchemaType.Source, Table);
                daProductionPlanning.Fill(dsProductionPlanning, Table);
                if (dsProductionPlanning.Tables[Table].Columns[0].AutoIncrement)
                {
                    dsProductionPlanning.Tables[Table].Columns[0].AutoIncrement = true;
                    dsProductionPlanning.Tables[Table].Columns[0].AutoIncrementSeed = 1L;
                    if (dsProductionPlanning.Tables[Table].Rows.Count == 0 && dsProductionPlanning.Tables[Table].Columns[0].AutoIncrement)
                    {
                        var cmResetIdentity = new SqlCommand("DBCC CHECKIDENT(" + srcTableName + ",RESEED,0)", Module1.cnProductionPlanning);
                        if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                            Module1.cnProductionPlanning.Open();
                        cmResetIdentity.ExecuteNonQuery();
                        if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                            Module1.cnProductionPlanning.Close();
                        cmResetIdentity.Dispose();
                    }
                }

                return true;
            }
            catch (Exception objEx)
            {
                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                {
                    if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                        Module1.cnProductionPlanning.Close();
                }

                Logger.SaveError("DataSetConfiguration.FillDataSet", objEx.Message);
                MessageBox.Show("با اشکال مواجه شد " + Table + " فراخوانی جدول", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                return false;
            }
        }

        public void RefreshDataSet(string tbl)
        {
            mdaProductionPlanning.Fill(mdsProductionPlanning.Tables[tbl].Rows.Count, 1, mdsProductionPlanning.Tables[tbl]);
        }
    }
}