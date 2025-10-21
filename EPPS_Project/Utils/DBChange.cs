using ProductionPlanning.Model;
using ProductionPlanning.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ProductionPlanning.DBChange
{
    internal class DBChange
    {
        public int DBver = 0;
        
        public void UpdateDBversion()
        {
            // Use using blocks to ensure resources are disposed
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            using (var cm = cn.CreateCommand())
            {
                cn.Open();

                // Check if the version table exists. Accept either Tbl_DB_Version or DB_Version
                cm.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Tbl_DB_Version'";
                var tblCountObj = cm.ExecuteScalar();
                var tblCount = 0;
                if (tblCountObj != null && int.TryParse(tblCountObj.ToString(), out var tmpCount))
                    tblCount = tmpCount;

                if (tblCount == 0)
                {
                    // Table does not exist -> assume version 0
                    Logger.SaveError("UpdateDBversion", "Tbl_DB_Version (or DB_Version) table not found. Assuming DB version = 0.");
                    this.DBver = 0;
                }
                else
                {
                    // Table exists, try to read version
                    cm.CommandText = "SELECT TOP 1 DB_Version FROM Tbl_DB_Version ORDER BY DB_Version";
                    var scalar = cm.ExecuteScalar();
                    if (scalar == null || scalar == DBNull.Value)
                    {
                        Logger.SaveError("UpdateDBversion", "Tbl_DB_Version has no record or DB_Version is NULL.");
                        this.DBver = 0;
                    }
                    else
                    {
                        if (!int.TryParse(scalar.ToString(), out var parsedVer))
                        {
                            Logger.SaveError("UpdateDBversion", $"Unable to parse DB_Version value: {scalar}");
                            MessageBox.Show(" ورژن بانک اطلاعاتی معتبر نیست.", "اشکال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                            this.DBver = 0;
                        }
                        else
                        {
                            this.DBver = parsedVer;
                        }
                    }
                }

                // Get list of Scripts need to be run:
                var dbScripts = zzGetScripts(this.DBver);

                foreach (var dbScript in dbScripts)
                {
                    
                    zzExecuteSqlScript(cn, dbScript.Script);
                    Logger.LogInfo($"UPDATED Tbl_DB_Version SET DB_Version = {dbScript.Version}");
                   
                }
            } // using ensures cn and cm disposed
        }

        private List<DBScript> zzGetScripts(int DBver)
        {
            var scripts = new List<DBScript>();
            var dbScriptFolder = System.IO.Path.Combine(Application.StartupPath, ".EPPS_DB_Scripts");
            var scriptFiles = FileHelpers.GetFileNames(dbScriptFolder, includeSubdirectories: false, searchPattern: "*.sql", returnFullPath: false);

            
            foreach (var scriptFile in scriptFiles)
            {
                var version= int.Parse(scriptFile.Substring(0, scriptFile.IndexOf('.')));
                if (version > DBver)
                {
                    var scriptPath = System.IO.Path.Combine(dbScriptFolder, scriptFile);
                    var scriptContent = System.IO.File.ReadAllText(scriptPath);
                    var script = new DBScript
                    {
                        Number = scripts.Count + 1,
                        Version = version,
                        Script = scriptContent
                    };
                    scripts.Add(script);
                }
                    
            }
            
            return scripts;
        }

        private void zzExecuteSqlScript(SqlConnection connection, string script, SqlTransaction transaction = null)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (script == null) return;
            if (connection.State != System.Data.ConnectionState.Open) throw new InvalidOperationException("SqlConnection must be open before calling ExecuteSqlScript.");

            try
            {
                // Match lines that contain only GO or GO <count>, optionally followed by comments.
                var regex = new Regex(@"^\s*GO(?:\s+(\d+))?\s*(?:--.*)?$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                var batches = new List<(string Sql, int Repeat)>();

                int start = 0;
                var matches = regex.Matches(script);
                foreach (Match m in matches)
                {
                    int index = m.Index;
                    string batch = script.Substring(start, index - start).Trim();
                    start = m.Index + m.Length;

                    int repeat = 1;
                    if (m.Groups.Count > 1 && m.Groups[1].Success)
                    {
                        if (!int.TryParse(m.Groups[1].Value, out repeat) || repeat < 1) repeat = 1;
                    }

                    batches.Add((batch, repeat));
                }

                // Add remaining tail
                if (start < script.Length)
                {
                    string tail = script.Substring(start).Trim();
                    batches.Add((tail, 1));
                }

                // Execute each non-empty batch the required number of times
                foreach (var (Sql, Repeat) in batches)
                {
                    if (string.IsNullOrWhiteSpace(Sql))
                        continue;

                    for (int i = 0; i < Repeat; i++)
                    {
                        using (var cmd = connection.CreateCommand())
                        {
                            cmd.CommandText = Sql;
                            if (transaction != null) cmd.Transaction = transaction;
                            // adjust timeout if some scripts may run long
                            // cmd.CommandTimeout = 0; // uncomment to disable timeout
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveError("UpdateDBversion", ex.Message);
                throw;
            }
        }
    }
}
