using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning.Tools.ExportToExcel
{
    public class ExportToExcel
    {
        public void dataGridView2Excel(DataGridView dgv, string pFullPath_toExport, string nameSheet)
        {
            string vFileName = Path.GetTempFileName();
            FileSystem.FileOpen(1, vFileName, OpenMode.Output, OpenAccess.Default, OpenShare.Default, -1);
            string sb = string.Empty;
            int Cc;
            int i = 0;
            int Rr;
            if (dgv is object)
            {
                var loopTo = dgv.Columns.Count - 1;
                for (Cc = 0; Cc <= loopTo; Cc++) // Count
                {
                    System.Windows.Forms.Application.DoEvents();
                    string title = string.Empty;
                    if (dgv.Columns[Cc].Visible == true)
                    {
                        title = dgv.Columns[Cc].HeaderText;
                        sb += title + Conversions.ToString(ControlChars.Tab);
                    }
                }
            }
            else
            {
                var loopTo1 = dgv.ColumnCount - 1;
                for (Cc = 0; Cc <= loopTo1; Cc++)
                {
                    System.Windows.Forms.Application.DoEvents();
                    string title = string.Empty;
                    if (dgv.Columns[Cc].Visible == true)
                    {
                        title = "Column_" + Cc.ToString();
                        sb += title + Conversions.ToString(ControlChars.Tab);
                    }
                }
            }

            FileSystem.PrintLine(1, sb);
            var loopTo2 = dgv.Rows.Count - 1;
            for (Rr = 0; Rr <= loopTo2; Rr++)
            {
                System.Windows.Forms.Application.DoEvents();
                i = 0;
                sb = string.Empty;
                var loopTo3 = dgv.ColumnCount - 1;
                for (Cc = 0; Cc <= loopTo3; Cc++)
                {
                    if (dgv.Columns[Cc].Visible == true)
                    {
                        if (dgv is object && dgv.Columns[Cc] is object)
                        {
                            System.Windows.Forms.Application.DoEvents();
                            string CellValue = dgv.Rows[Rr].Cells[Cc].Value.ToString().Replace(Conversions.ToString('\r'), " ").Trim();
                            sb = sb + FormatCell(CellValue) + Conversions.ToString(ControlChars.Tab);
                        }

                        i += 1;
                    }
                }

                FileSystem.PrintLine(1, sb);
            }

            FileSystem.FileClose(1);
            TextToExcel(vFileName, pFullPath_toExport, nameSheet);
        }

        public void dataTable2Excel(System.Data.DataTable pDataTable, DataGridView dgv, string pFullPath_toExport, string nameSheet)
        {
            string vFileName = Path.GetTempFileName();
            FileSystem.FileOpen(1, vFileName, OpenMode.Output, OpenAccess.Default, OpenShare.Default, -1);
            string sb = string.Empty;
            int Cc;
            // si existe datagridview, tomar de él los nombres de columnas y la visibilidad de las mismas 
            if (dgv is object)
            {
                var loopTo = dgv.Columns.Count - 1;
                for (Cc = 0; Cc <= loopTo; Cc++) // Count
                {
                    System.Windows.Forms.Application.DoEvents();
                    string title = string.Empty;
                    if (dgv.Columns[Cc].Visible == true)
                    {
                        // Obtener el texto de cabecera de la grilla 
                        title = dgv.Columns[Cc].HeaderText;
                        sb += title + Conversions.ToString(ControlChars.Tab);
                    }
                }
            }
            // ' '' ''For Each dc As DataColumn In pDataTable.Columns
            // ' '' ''    System.Windows.Forms.Application.DoEvents()
            // ' '' ''    Dim title As String = String.Empty

            // ' '' ''    'recuperar el título que aparece en la grilla 
            // ' '' ''    'Notar que debe haber sincronía con las columnas del detalle 
            // ' '' ''    If dgv.Columns(dc.Caption) IsNot Nothing Then
            // ' '' ''        'Obtener el texto de cabecera de la grilla 
            // ' '' ''        title = dgv.Columns(dc.Caption).HeaderText
            // ' '' ''        sb += title + ControlChars.Tab
            // ' '' ''    End If
            // ' '' ''Next
            else
            {
                // si no existe datagridview tomar el nombre de la columna del datatable 
                var loopTo1 = dgv.ColumnCount - 1;
                for (Cc = 0; Cc <= loopTo1; Cc++)
                {
                    System.Windows.Forms.Application.DoEvents();
                    string title = string.Empty;
                    if (dgv.Columns[Cc].Visible == true)
                    {
                        // Obtener el texto de cabecera de la grilla 
                        title = "Column_" + Cc.ToString();
                        sb += title + Conversions.ToString(ControlChars.Tab);
                    }
                }
            }

            FileSystem.PrintLine(1, sb);
            int i = 0;
            int Rr;
            // para cada fila de datos 
            var loopTo2 = dgv.Rows.Count - 1;
            for (Rr = 0; Rr <= loopTo2; Rr++)
            {
                System.Windows.Forms.Application.DoEvents();
                i = 0;
                sb = string.Empty;
                // para cada columna de datos 
                var loopTo3 = dgv.ColumnCount - 1;
                for (Cc = 0; Cc <= loopTo3; Cc++)
                {
                    if (dgv.Columns[Cc].Visible == true)
                    {
                        if (dgv is object && dgv.Columns[Cc] is object)
                        {
                            System.Windows.Forms.Application.DoEvents();
                            // Linea q genera la impresión del registro 

                            sb = sb + FormatCell(dgv.Rows[Rr].Cells[Cc].Value.ToString()) + Conversions.ToString(ControlChars.Tab);
                            // ElseIf dgv Is Nothing Then
                            // System.Windows.Forms.Application.DoEvents()
                            // 'Linea q genera la impresión del registro 
                            // sb = sb + (IIf(Information.IsDBNull(dr(i)), String.Empty, FormatCell(dr(i)))) + ControlChars.Tab
                        }

                        i += 1;
                    }
                }

                FileSystem.PrintLine(1, sb);
            }

            // ' '' ''For Each dr As DataRow In pDataTable.Rows
            // ' '' ''    System.Windows.Forms.Application.DoEvents()
            // ' '' ''    i = 0
            // ' '' ''    sb = String.Empty
            // ' '' ''    'para cada columna de datos 
            // ' '' ''    For Each dc As DataColumn In pDataTable.Columns
            // ' '' ''        'solo mostrar aquellas columnas q pertenezcan a la grilla 
            // ' '' ''        'notar que debe haber sincronia con las columnas del la cabecera 
            // ' '' ''        If dgv IsNot Nothing AndAlso dgv.Columns(dc.Caption) IsNot Nothing Then
            // ' '' ''            System.Windows.Forms.Application.DoEvents()
            // ' '' ''            'Linea q genera la impresión del registro 

            // ' '' ''            sb = sb + (IIf(Information.IsDBNull(dr(i)), String.Empty, FormatCell(dr(i)))) + ControlChars.Tab
            // ' '' ''        ElseIf dgv Is Nothing Then
            // ' '' ''            System.Windows.Forms.Application.DoEvents()
            // ' '' ''            'Linea q genera la impresión del registro 
            // ' '' ''            sb = sb + (IIf(Information.IsDBNull(dr(i)), String.Empty, FormatCell(dr(i)))) + ControlChars.Tab
            // ' '' ''        End If
            // ' '' ''        i += 1
            // ' '' ''    Next
            // ' '' ''    FileSystem.PrintLine(1, sb)
            // ' '' ''Next
            FileSystem.FileClose(1);
            TextToExcel(vFileName, pFullPath_toExport, nameSheet);
        }

        private string FormatCell(object cell)
        {
            string TextToParse = Convert.ToString(cell);
            TextToParse = TextToParse.Replace(",", string.Empty);
            TextToParse = TextToParse.Replace("¡", "_");
            return TextToParse;
        }

        private void TextToExcel(string pFileName, string pFullPath_toExport, string nameSheet)
        {
            var vCultura = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
            var Exc = new Microsoft.Office.Interop.Excel.Application();
            Exc.Workbooks.OpenText(pFileName, Missing.Value, 1, XlTextParsingType.xlDelimited, XlTextQualifier.xlTextQualifierNone, Missing.Value, Missing.Value, Missing.Value, true, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            var Wb = Exc.ActiveWorkbook;
            Worksheet Ws = (Worksheet)Wb.ActiveSheet;
            Ws.Name = nameSheet;
            try
            {
                // Formato de cabecera 
                Ws.get_Range(Ws.Cells[1, 1], Ws.Cells[Ws.UsedRange.Rows.Count, Ws.UsedRange.Columns.Count]).AutoFormat(XlRangeAutoFormat.xlRangeAutoFormatClassic1, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            }
            catch
            {
                // Ws.Range(Ws.Cells(1, 1), Ws.Cells(Ws.UsedRange.Rows.Count, Ws.UsedRange.Columns.Count))

            }

            string tempPath = Path.GetTempFileName();
            pFileName = tempPath.Replace("tmp", "xls");
            File.Delete(pFileName);
            if (File.Exists(pFullPath_toExport))
            {
                try
                {
                    File.Delete(pFullPath_toExport);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            Exc.ActiveWorkbook.SaveAs(pFullPath_toExport, 1, null, null, null, null, XlSaveAsAccessMode.xlNoChange, null, null, null, null, null);
            Exc.Workbooks.Close();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(Ws);
            Ws = null;
            System.Runtime.InteropServices.Marshal.ReleaseComObject(Wb);
            Wb = null;
            Exc.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(Exc);
            Exc = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            System.Threading.Thread.CurrentThread.CurrentCulture = vCultura;
        }

        public static System.Data.DataTable ArrayListToDataTable(ArrayList array)
        {
            var dt = new System.Data.DataTable();
            if (array.Count > 0)
            {
                var obj = array[0];
                // Convertir las propiedades del objeto en columnas del datarow 
                foreach (PropertyInfo info in obj.GetType().GetProperties())
                    dt.Columns.Add(info.Name, info.PropertyType);
            }

            foreach (object obj in array)
            {
                var dr = dt.NewRow();
                foreach (DataColumn col in dt.Columns)
                {
                    var type = obj.GetType();
                    var members = type.GetMember(col.ColumnName);
                    object valor;
                    if (members.Length != 0)
                    {
                        switch (members[0].MemberType)
                        {
                            case MemberTypes.Property:
                                {
                                    // leer las propiedades del objeto 
                                    PropertyInfo prop = (PropertyInfo)members[0];
                                    try
                                    {
                                        valor = prop.GetValue(obj, new object[0]);
                                    }
                                    catch
                                    {
                                        valor = prop.GetValue(obj, null);
                                    }

                                    break;
                                }

                            case MemberTypes.Field:
                                {
                                    // leer los campos del objeto (no se usa 
                                    // dado q hemos poblado el dt con las propiedades del arraylist) 
                                    FieldInfo field = (FieldInfo)members[0];
                                    valor = field.GetValue(obj);
                                    break;
                                }

                            default:
                                {
                                    throw new NotImplementedException();
                                    //break;
                                }
                        }

                        dr[col] = valor;
                    }
                }

                dt.Rows.Add(dr);
            }

            return dt;
        }

        public static string readcell(Range oRange)
        {
            string result = string.Empty;
            if (oRange is object)
            {
                if (oRange.Text is object)
                {
                    result = oRange.Text.ToString();
                }
            }

            return result;
        }
    }
}