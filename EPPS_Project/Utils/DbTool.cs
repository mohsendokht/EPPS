using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace ProductionPlanning
{
    internal class DbTool
    {
        private DataSet RealProduction;
        

        public DataSet ds_RealProduction
        {
            get
            {
                return RealProduction;
            }
        }

        public static DataSet GetDataSet(string Table, string SelectCommandText)
        {
            var cmSelect = new SqlCommand(SelectCommandText, Module1.cnProductionPlanning);
            var dSet = new DataSet();
            var dAdapter = new SqlDataAdapter();
            try
            {
                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();
                dAdapter.SelectCommand = new SqlCommand(SelectCommandText, Module1.cnProductionPlanning);
                dAdapter.FillSchema(dSet, SchemaType.Source, Table);
                dAdapter.Fill(dSet, Table);
            }
            catch (Exception objEx)
            {
                Logger.SaveError("DbTool.GetDataSet()", objEx.Message + Constants.vbCrLf + SelectCommandText);
                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                    Module1.cnProductionPlanning.Close();
                MessageBox.Show("با اشکال مواجه شد " + Table + " فراخوانی جدول", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }

            return dSet;
        }

        public bool ResetTableID(DataSet dSet, string TableName, params string[] PrimaryKeyCols)
        {
            var dAdapter = new SqlDataAdapter();
            try
            {
                if (dSet.Tables.Count == 0)
                    return false;
                if (dSet.Tables[TableName].Columns[0].AutoIncrement)
                {
                    dSet.Tables[TableName].Columns[0].AutoIncrementSeed = 1L;
                    if (dSet.Tables[TableName].Rows.Count == 0)
                    {
                        var cmResetIdentity = new SqlCommand("DBCC CHECKIDENT(" + TableName + ",RESEED,0)", Module1.cnProductionPlanning);
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
                Logger.LogException("", objEx);
                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                {
                    if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                        Module1.cnProductionPlanning.Close();
                }

                MessageBox.Show("با اشکال مواجه شد " + TableName + " ریست جدول", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                return false;
            }
        }
    }

    internal class MyDB
    {
        private DataSet _ds;

        public DataSet DataSet
        {
            get
            {
                return _ds;
            }
        }

        public MyDB()
        {
            _ds = new DataSet();
        }

        public void ReadData(string TableName, string SelectSQL)
        {
            var cmSelect = new SqlCommand(SelectSQL, Module1.cnProductionPlanning);
            var dAdapter = new SqlDataAdapter();
            try
            {
                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();
                dAdapter.SelectCommand = new SqlCommand(SelectSQL, Module1.cnProductionPlanning);
                dAdapter.FillSchema(_ds, SchemaType.Source, TableName);
                dAdapter.Fill(_ds, TableName);
            }
            catch (Exception objEx)
            {
                Logger.SaveError("MyDataSet.ReadData():", objEx.Message + Constants.vbCrLf + SelectSQL);
                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                    Module1.cnProductionPlanning.Close();
                MessageBox.Show("با اشکال مواجه شد " + TableName + " فراخوانی جدول", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }

        public DataView DefaultView(string TableName, string Filter = "")
        {
            try
            {
                _ds.Tables[TableName].DefaultView.RowFilter = Filter;
                return _ds.Tables[TableName].DefaultView;
            }
            catch (Exception ex)
            {
                Logger.SaveError("MyDBSet.DefaultView():", ex.Message);
                MessageBox.Show("با اشکال مواجه شد " + TableName + "  فراخوانی اطلاعات ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }

            return new DataView();
        }

        public bool ResetTableID(string TableName, params string[] PrimaryKeyCols)
        {
            var dAdapter = new SqlDataAdapter();
            try
            {
                if (_ds.Tables.Count == 0)
                    return false;
                if (_ds.Tables[TableName].Columns[0].AutoIncrement)
                {
                    _ds.Tables[TableName].Columns[0].AutoIncrementSeed = 1L;
                    if (_ds.Tables[TableName].Rows.Count == 0)
                    {
                        var cmResetIdentity = new SqlCommand("DBCC CHECKIDENT(" + TableName + ",RESEED,0)", Module1.cnProductionPlanning);
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
                Logger.LogException("ResetTableID", objEx);
                if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                {
                    if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                        Module1.cnProductionPlanning.Close();
                }

                MessageBox.Show("با اشکال مواجه شد " + TableName + " ریست جدول", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                return false;
            }
        }
    }
}