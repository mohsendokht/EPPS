using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmMPSCapametryShow
    {
        public frmMPSCapametryShow()
        {
            InitializeComponent();
            _cmdShow.Name = "cmdShow";
            _cmdExit.Name = "cmdExit";
            _dgMachines.Name = "dgMachines";
            _dgPersonnels.Name = "dgPersonnels";
            _dgMaterials.Name = "dgMaterials";
        }

        private enum mShowMode
        {
            SM_ALL,
            SM_SELECTED
        }

        private DataView dvMachineList;
        private DataView dvPersonnelList;
        private DataView dvMaterialList;
        private DataView dvProducts;
        private SqlDataAdapter mdaCapametryShow = new SqlDataAdapter();
        public DataSet mdsCapametryShow;
        private short mMPSCode;
        private short mMPSYear;
        private int mPersonnelCapametryCalendar;
        private short I, J, K;

        public short MPSCode
        {
            set
            {
                mMPSCode = value;
            }
        }

        public short MPSYear
        {
            set
            {
                mMPSYear = value;
            }
        }

        public int PersonnelCapametryCalendar
        {
            set
            {
                mPersonnelCapametryCalendar = value;
            }
        }

        private void frmMPSCapametryShow_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            dgMachines.Sorted += Module1.DataGridViews_Sorted_EventHandler;
            dgMaterials.Sorted += Module1.DataGridViews_Sorted_EventHandler;
            dgPersonnels.Sorted += Module1.DataGridViews_Sorted_EventHandler;
            mdsCapametryShow.Tables["Tbl_MachineCapametry"].Columns["Total"].ReadOnly = false;
            mdsCapametryShow.Tables["Tbl_MaterialCapametry"].Columns["Total"].ReadOnly = false;
            dvProducts = mdsCapametryShow.Tables["Tbl_Products"].DefaultView;
            dvMachineList = mdsCapametryShow.Tables["Tbl_MachineCapametry"].DefaultView;
            dvPersonnelList = mdsCapametryShow.Tables["Tbl_PersonnelCapametry"].DefaultView;
            dvMaterialList = mdsCapametryShow.Tables["Tbl_MaterialCapametry"].DefaultView;
            for (int I = 0, loopTo = dvMaterialList.Count - 1; I <= loopTo; I++)
            {
                double mTotalValue = Conversions.ToDouble(dvMaterialList[I]["Total"]);
                dvMaterialList[I]["Total"] = Math.Round(mTotalValue, 3).ToString();
            }

            {
                var withBlock = cbProductName;
                withBlock.DataSource = dvProducts;
                withBlock.DisplayMember = "ProductName";
                withBlock.ValueMember = "ProductCode";
                withBlock.SelectedIndex = -1;
            }

            Cursor = Cursors.Default;
            Application.DoEvents();
        }

        private void frmMPSCapametryShow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Module1.SaveGridColumnsWidth(Name, 0, dgMachines);
            Module1.SaveGridColumnsWidth(Name, 0, dgPersonnels);
            Module1.SaveGridColumnsWidth(Name, 0, dgMaterials);
            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                Module1.cnProductionPlanning.Close();
            for (I = (short)(mdsCapametryShow.Tables.Count - 1); I >= 1; I += -1)
            {
                mdsCapametryShow.Tables[I].Constraints.Clear();
                mdsCapametryShow.Tables[I].Dispose();
                mdsCapametryShow.Tables.RemoveAt(I);
            }

            mdsCapametryShow.Dispose();
            mdaCapametryShow.Dispose();
            dvMachineList = null;
            dvPersonnelList = null;
            dvProducts = null;
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdShow_Click(object sender, EventArgs e)
        {
            if (cbProductName.Items.Count > 0 && cbProductName.SelectedValue is object && cbProductName.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                if (cbProductName.SelectedValue.ToString().Equals("-100"))
                {
                    dvMachineList.RowFilter = "";
                    dvPersonnelList.RowFilter = "";
                    dvMaterialList.RowFilter = "";
                    SetGridColumns(mShowMode.SM_ALL);
                    string TempCode;
                    string TempNavigated = Constants.vbNullString;
                    var TempHour1 = new double[12];
                    var TempMinute1 = new double[12];
                    var TempHour2 = new double[12];
                    var TempMinute2 = new double[12];
                    var TempTotalHour1 = default(double);
                    var TempTotalHour2 = default(double);
                    var TempTotalMin1 = default(double);
                    var TempTotalMin2 = default(double);
                    DataRow[] drAccessible;

                    // محاسبات ظرفیت سنجی ماشین آلات
                    for (I = (short)(dvMachineList.Count - 1); I >= 0; I += -1)
                    {
                        for (J = 0; J <= 11; J++)
                        {
                            TempHour1[J] = 0d;
                            TempMinute1[J] = 0d;
                            TempHour2[J] = 0d;
                            TempMinute2[J] = 0d;
                        }

                        TempTotalHour1 = 0d;
                        TempTotalHour2 = 0d;
                        TempTotalMin1 = 0d;
                        TempTotalMin2 = 0d;
                        TempCode = Conversions.ToString(dvMachineList[I]["MachineCode"]);
                        dvMachineList.RowFilter = Conversions.ToString(Operators.ConcatenateObject("MachineCode='" + TempCode, Interaction.IIf(string.IsNullOrEmpty(TempNavigated), "'", "' And Not MachineCode IN(" + TempNavigated + ")")));
                        if (dvMachineList.Count > 0)
                        {
                            dvMachineList.Sort = "MachineCode";
                            if (string.IsNullOrEmpty(TempNavigated))
                            {
                                TempNavigated = "'" + TempCode + "'";
                            }
                            else
                            {
                                TempNavigated += ",'" + TempCode + "'";
                            }

                            drAccessible = mdsCapametryShow.Tables["Tbl_CapametryAccessibleTimes"].Select("MachineCode='" + TempCode + "'");
                            if (drAccessible.Length > 0)
                            {
                                for (K = 1; K <= 12; K++)
                                {
                                    TempHour1[K - 1] = Conversions.ToInteger(Strings.Mid(Conversions.ToString(drAccessible[0]["Month" + K.ToString()]), 1, Conversions.ToString(drAccessible[0]["Month" + K.ToString()]).IndexOf(":")));
                                    TempMinute1[K - 1] = Conversions.ToInteger(Strings.Mid(Conversions.ToString(drAccessible[0]["Month" + K.ToString()]), Conversions.ToString(drAccessible[0]["Month" + K.ToString()]).IndexOf(":") + 2, 2));
                                }
                            }

                            var loopTo = (short)(dvMachineList.Count - 1);
                            for (J = 0; J <= loopTo; J++)
                            {
                                if (dvMachineList[J]["RecType"].Equals("زمان مورد نياز"))
                                {
                                    for (K = 1; K <= 12; K++)
                                        TempHour2[K - 1] += Conversions.ToInteger(Strings.Mid(Conversions.ToString(dvMachineList[J]["Month" + K]), 1, Conversions.ToString(dvMachineList[J]["Month" + K]).IndexOf(":")));
                                    for (K = 1; K <= 12; K++)
                                        TempMinute2[K - 1] += Conversions.ToInteger(Strings.Mid(Conversions.ToString(dvMachineList[J]["Month" + K]), Conversions.ToString(dvMachineList[J]["Month" + K]).IndexOf(":") + 2, 2));
                                }
                            }

                            for (J = 0; J <= 11; J++)
                            {
                                TempHour2[J] += (TempMinute2[J] - TempMinute2[J] % 60d) / 60d;
                                TempMinute2[J] = TempMinute2[J] % 60d;
                                TempTotalHour1 += TempHour1[J];
                                TempTotalMin1 += TempMinute1[J];
                                TempTotalHour2 += TempHour2[J];
                                TempTotalMin2 += TempMinute2[J];
                            }

                            TempTotalHour1 += (TempTotalMin1 - TempTotalMin1 % 60d) / 60d;
                            TempTotalMin1 = TempTotalMin1 % 60d;
                            TempTotalHour2 += (TempTotalMin2 - TempTotalMin2 % 60d) / 60d;
                            TempTotalMin2 = TempTotalMin2 % 60d;
                            dgMachines.Rows.Add("", "", "", "", dvMachineList[0]["MachineCode"], dvMachineList[0]["MachineCode"] + "-1", "زمان مورد نياز", 
                                                Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour2[0] < 10d, "0" + TempHour2[0], TempHour2[0]), ":"), Interaction.IIf(TempMinute2[0] < 10d, "0" + TempMinute2[0], TempMinute2[0])), 
                                                Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour2[1] < 10d, "0" + TempHour2[1], TempHour2[1]), ":"), Interaction.IIf(TempMinute2[1] < 10d, "0" + TempMinute2[1], TempMinute2[1])), 
                                                Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour2[2] < 10d, "0" + TempHour2[2], TempHour2[2]), ":"), Interaction.IIf(TempMinute2[2] < 10d, "0" + TempMinute2[2], TempMinute2[2])), 
                                                Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour2[3] < 10d, "0" + TempHour2[3], TempHour2[3]), ":"), Interaction.IIf(TempMinute2[3] < 10d, "0" + TempMinute2[3], TempMinute2[3])), 
                                                Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour2[4] < 10d, "0" + TempHour2[4], TempHour2[4]), ":"), Interaction.IIf(TempMinute2[4] < 10d, "0" + TempMinute2[4], TempMinute2[4])), 
                                                Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour2[5] < 10d, "0" + TempHour2[5], TempHour2[5]), ":"), Interaction.IIf(TempMinute2[5] < 10d, "0" + TempMinute2[5], TempMinute2[5])), 
                                                Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour2[6] < 10d, "0" + TempHour2[6], TempHour2[6]), ":"), Interaction.IIf(TempMinute2[6] < 10d, "0" + TempMinute2[6], TempMinute2[6])), 
                                                Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour2[7] < 10d, "0" + TempHour2[7], TempHour2[7]), ":"), Interaction.IIf(TempMinute2[7] < 10d, "0" + TempMinute2[7], TempMinute2[7])), 
                                                Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour2[8] < 10d, "0" + TempHour2[8], TempHour2[8]), ":"), Interaction.IIf(TempMinute2[8] < 10d, "0" + TempMinute2[8], TempMinute2[8])), 
                                                Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour2[9] < 10d, "0" + TempHour2[9], TempHour2[9]), ":"), Interaction.IIf(TempMinute2[9] < 10d, "0" + TempMinute2[9], TempMinute2[9])), 
                                                Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour2[10] < 10d, "0" + TempHour2[10], TempHour2[10]), ":"), Interaction.IIf(TempMinute2[10] < 10d, "0" + TempMinute2[10], TempMinute2[10])), 
                                                Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour2[11] < 10d, "0" + TempHour2[11], TempHour2[11]), ":"), Interaction.IIf(TempMinute2[11] < 10d, "0" + TempMinute2[11], TempMinute2[11])),
                                                Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempTotalHour2 < 10d, "0" + TempTotalHour2, TempTotalHour2), ":"), Interaction.IIf(TempTotalMin2 < 10d, "0" + TempTotalMin2, TempTotalMin2)));

                            dgMachines.Rows.Add("", "", "", "", dvMachineList[0]["MachineCode"], dvMachineList[0]["MachineCode"] + "-0", "زمان در دسترس", 
                                                Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour1[0] < 10d, "0" + TempHour1[0], TempHour1[0]), ":"), Interaction.IIf(TempMinute1[0] < 10d, "0" + TempMinute1[0], TempMinute1[0])), 
                                                Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour1[1] < 10d, "0" + TempHour1[1], TempHour1[1]), ":"), Interaction.IIf(TempMinute1[1] < 10d, "0" + TempMinute1[1], TempMinute1[1])), 
                                                Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour1[2] < 10d, "0" + TempHour1[2], TempHour1[2]), ":"), Interaction.IIf(TempMinute1[2] < 10d, "0" + TempMinute1[2], TempMinute1[2])), 
                                                Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour1[3] < 10d, "0" + TempHour1[3], TempHour1[3]), ":"), Interaction.IIf(TempMinute1[3] < 10d, "0" + TempMinute1[3], TempMinute1[3])), 
                                                Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour1[4] < 10d, "0" + TempHour1[4], TempHour1[4]), ":"), Interaction.IIf(TempMinute1[4] < 10d, "0" + TempMinute1[4], TempMinute1[4])), 
                                                Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour1[5] < 10d, "0" + TempHour1[5], TempHour1[5]), ":"), Interaction.IIf(TempMinute1[5] < 10d, "0" + TempMinute1[5], TempMinute1[5])), 
                                                Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour1[6] < 10d, "0" + TempHour1[6], TempHour1[6]), ":"), Interaction.IIf(TempMinute1[6] < 10d, "0" + TempMinute1[6], TempMinute1[6])), 
                                                Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour1[7] < 10d, "0" + TempHour1[7], TempHour1[7]), ":"), Interaction.IIf(TempMinute1[7] < 10d, "0" + TempMinute1[7], TempMinute1[7])), 
                                                Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour1[8] < 10d, "0" + TempHour1[8], TempHour1[8]), ":"), Interaction.IIf(TempMinute1[8] < 10d, "0" + TempMinute1[8], TempMinute1[8])), 
                                                Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour1[9] < 10d, "0" + TempHour1[9], TempHour1[9]), ":"), Interaction.IIf(TempMinute1[9] < 10d, "0" + TempMinute1[9], TempMinute1[9])), 
                                                Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour1[10] < 10d, "0" + TempHour1[10], TempHour1[10]), ":"), Interaction.IIf(TempMinute1[10] < 10d, "0" + TempMinute1[10], TempMinute1[10])), 
                                                Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour1[11] < 10d, "0" + TempHour1[11], TempHour1[11]), ":"), Interaction.IIf(TempMinute1[11] < 10d, "0" + TempMinute1[11], TempMinute1[11])), 
                                                Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempTotalHour1 < 10d, "0" + TempTotalHour1, TempTotalHour1), ":"), Interaction.IIf(TempTotalMin1 < 10d, "0" + TempTotalMin1, TempTotalMin1)));

                        }

                        dvMachineList.RowFilter = "";
                    }

                    // محاسبات ظرفیت سنجی پرسنل
                    for (J = 0; J <= 11; J++)
                    {
                        TempHour1[J] = 0d;
                        TempMinute1[J] = 0d;
                        TempHour2[J] = 0d;
                        TempMinute2[J] = 0d;
                    }

                    drAccessible = mdsCapametryShow.Tables["Tbl_CapametryAccessibleTimes"].Select("MachineCode='-1'");
                    if (drAccessible.Length > 0)
                    {
                        for (K = 1; K <= 12; K++)
                        {
                            TempHour1[K - 1] = Conversions.ToInteger(Strings.Mid(Conversions.ToString(drAccessible[0]["Month" + K.ToString()]), 1, Conversions.ToString(drAccessible[0]["Month" + K.ToString()]).IndexOf(":")));
                            TempMinute1[K - 1] = Conversions.ToInteger(Strings.Mid(Conversions.ToString(drAccessible[0]["Month" + K.ToString()]), Conversions.ToString(drAccessible[0]["Month" + K.ToString()]).IndexOf(":") + 2, 2));
                        }
                    }

                    var loopTo1 = (short)(dvPersonnelList.Count - 1);
                    for (I = 0; I <= loopTo1; I++)
                    {
                        if (dvPersonnelList[I]["RecType"].Equals("زمان مورد نياز"))
                        {
                            for (K = 1; K <= 12; K++)
                                TempHour2[K - 1] += Conversions.ToInteger(Strings.Mid(Conversions.ToString(dvPersonnelList[I]["Month" + K]), 1, Conversions.ToString(dvPersonnelList[I]["Month" + K]).IndexOf(":")));
                            for (K = 1; K <= 12; K++)
                                TempMinute2[K - 1] += Conversions.ToInteger(Strings.Mid(Conversions.ToString(dvPersonnelList[I]["Month" + K]), Conversions.ToString(dvPersonnelList[I]["Month" + K]).IndexOf(":") + 2, 2));
                        }
                    }

                    for (J = 0; J <= 11; J++)
                    {
                        TempHour2[J] += (TempMinute2[J] - TempMinute2[J] % 60d) / 60d;
                        TempMinute2[J] = TempMinute2[J] % 60d;
                        TempTotalHour1 += TempHour1[J];
                        TempTotalMin1 += TempMinute1[J];
                        TempTotalHour2 += TempHour2[J];
                        TempTotalMin2 += TempMinute2[J];
                    }

                    TempTotalHour1 += (TempTotalMin1 - TempTotalMin1 % 60d) / 60d;
                    TempTotalMin1 = TempTotalMin1 % 60d;
                    TempTotalHour2 += (TempTotalMin2 - TempTotalMin2 % 60d) / 60d;
                    TempTotalMin2 = TempTotalMin2 % 60d;
                    dgPersonnels.Rows.Add("", "", "", "", "زمان در دسترس", Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour1[0] < 10d, "0" + TempHour1[0], TempHour1[0]), ":"), Interaction.IIf(TempMinute1[0] < 10d, "0" + TempMinute1[0], TempMinute1[0])), Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour1[1] < 10d, "0" + TempHour1[1], TempHour1[1]), ":"), Interaction.IIf(TempMinute1[1] < 10d, "0" + TempMinute1[1], TempMinute1[1])), Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour1[2] < 10d, "0" + TempHour1[2], TempHour1[2]), ":"), Interaction.IIf(TempMinute1[2] < 10d, "0" + TempMinute1[2], TempMinute1[2])), Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour1[3] < 10d, "0" + TempHour1[3], TempHour1[3]), ":"), Interaction.IIf(TempMinute1[3] < 10d, "0" + TempMinute1[3], TempMinute1[3])), Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour1[4] < 10d, "0" + TempHour1[4], TempHour1[4]), ":"), Interaction.IIf(TempMinute1[4] < 10d, "0" + TempMinute1[4], TempMinute1[4])), Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour1[5] < 10d, "0" + TempHour1[5], TempHour1[5]), ":"), Interaction.IIf(TempMinute1[5] < 10d, "0" + TempMinute1[5], TempMinute1[5])), Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour1[6] < 10d, "0" + TempHour1[6], TempHour1[6]), ":"), Interaction.IIf(TempMinute1[6] < 10d, "0" + TempMinute1[6], TempMinute1[6])), Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour1[7] < 10d, "0" + TempHour1[7], TempHour1[7]), ":"), Interaction.IIf(TempMinute1[7] < 10d, "0" + TempMinute1[7], TempMinute1[7])), Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour1[8] < 10d, "0" + TempHour1[8], TempHour1[8]), ":"), Interaction.IIf(TempMinute1[8] < 10d, "0" + TempMinute1[8], TempMinute1[8])), Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour1[9] < 10d, "0" + TempHour1[9], TempHour1[9]), ":"), Interaction.IIf(TempMinute1[9] < 10d, "0" + TempMinute1[9], TempMinute1[9])), Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour1[10] < 10d, "0" + TempHour1[10], TempHour1[10]), ":"), Interaction.IIf(TempMinute1[10] < 10d, "0" + TempMinute1[10], TempMinute1[10])), Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour1[11] < 10d, "0" + TempHour1[11], TempHour1[11]), ":"), Interaction.IIf(TempMinute1[11] < 10d, "0" + TempMinute1[11], TempMinute1[11])), Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempTotalHour1 < 10d, "0" + TempTotalHour1, TempTotalHour1), ":"), Interaction.IIf(TempTotalMin1 < 10d, "0" + TempTotalMin1, TempTotalMin1)));

                    dgPersonnels.Rows.Add("", "", "", "", "زمان مورد نياز", Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour2[0] < 10d, "0" + TempHour2[0], TempHour2[0]), ":"), Interaction.IIf(TempMinute2[0] < 10d, "0" + TempMinute2[0], TempMinute2[0])), Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour2[1] < 10d, "0" + TempHour2[1], TempHour2[1]), ":"), Interaction.IIf(TempMinute2[1] < 10d, "0" + TempMinute2[1], TempMinute2[1])), Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour2[2] < 10d, "0" + TempHour2[2], TempHour2[2]), ":"), Interaction.IIf(TempMinute2[2] < 10d, "0" + TempMinute2[2], TempMinute2[2])), Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour2[3] < 10d, "0" + TempHour2[3], TempHour2[3]), ":"), Interaction.IIf(TempMinute2[3] < 10d, "0" + TempMinute2[3], TempMinute2[3])), Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour2[4] < 10d, "0" + TempHour2[4], TempHour2[4]), ":"), Interaction.IIf(TempMinute2[4] < 10d, "0" + TempMinute2[4], TempMinute2[4])), Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour2[5] < 10d, "0" + TempHour2[5], TempHour2[5]), ":"), Interaction.IIf(TempMinute2[5] < 10d, "0" + TempMinute2[5], TempMinute2[5])), Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour2[6] < 10d, "0" + TempHour2[6], TempHour2[6]), ":"), Interaction.IIf(TempMinute2[6] < 10d, "0" + TempMinute2[6], TempMinute2[6])), Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour2[7] < 10d, "0" + TempHour2[7], TempHour2[7]), ":"), Interaction.IIf(TempMinute2[7] < 10d, "0" + TempMinute2[7], TempMinute2[7])), Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour2[8] < 10d, "0" + TempHour2[8], TempHour2[8]), ":"), Interaction.IIf(TempMinute2[8] < 10d, "0" + TempMinute2[8], TempMinute2[8])), Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour2[9] < 10d, "0" + TempHour2[9], TempHour2[9]), ":"), Interaction.IIf(TempMinute2[9] < 10d, "0" + TempMinute2[9], TempMinute2[9])), Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour2[10] < 10d, "0" + TempHour2[10], TempHour2[10]), ":"), Interaction.IIf(TempMinute2[10] < 10d, "0" + TempMinute2[10], TempMinute2[10])), Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempHour2[11] < 10d, "0" + TempHour2[11], TempHour2[11]), ":"), Interaction.IIf(TempMinute2[11] < 10d, "0" + TempMinute2[11], TempMinute2[11])), Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(TempTotalHour2 < 10d, "0" + TempTotalHour2, TempTotalHour2), ":"), Interaction.IIf(TempTotalMin2 < 10d, "0" + TempTotalMin2, TempTotalMin2)));

                    // محاسبات ظرفیت سنجی مواد اولیه
                    TempNavigated = "";
                    for (I = (short)(dvMaterialList.Count - 1); I >= 0; I += -1)
                    {
                        for (J = 0; J <= 11; J++)
                            TempHour1[J] = 0d;
                        TempTotalHour1 = 0d;
                        TempCode = Conversions.ToString(dvMaterialList[I]["MaterialCode"]);
                        dvMaterialList.RowFilter = Conversions.ToString(Operators.ConcatenateObject("MaterialCode='" + TempCode, Interaction.IIf(string.IsNullOrEmpty(TempNavigated), "'", "' And Not MaterialCode IN(" + TempNavigated + ")")));
                        if (dvMaterialList.Count > 0)
                        {
                            if (string.IsNullOrEmpty(TempNavigated))
                            {
                                TempNavigated = "'" + TempCode + "'";
                            }
                            else
                            {
                                TempNavigated += ",'" + TempCode + "'";
                            }

                            var loopTo2 = (short)(dvMaterialList.Count - 1);
                            for (J = 0; J <= loopTo2; J++)
                            {
                                for (K = 1; K <= 12; K++)
                                    TempHour1[K - 1] = Conversions.ToDouble(TempHour1[(int)K - 1] + (double)dvMaterialList[(int)J]["Month" + K]);
                            }

                            for (J = 0; J <= 11; J++)
                                TempTotalHour1 += TempHour1[J];
                            TempTotalHour1 = Math.Round(TempTotalHour1, 3);
                            dgMaterials.Rows.Add("", "", "", "", dvMaterialList[0]["MaterialCode"], "", dvMaterialList[0]["Title"], "", TempHour1[0], TempHour1[1], TempHour1[2], TempHour1[3], TempHour1[4], TempHour1[5], TempHour1[6], TempHour1[7], TempHour1[8], TempHour1[9], TempHour1[10], TempHour1[11], TempTotalHour1);


                        }

                        dvMaterialList.RowFilter = "";
                    }

                    dvMachineList.RowFilter = "";
                    dvPersonnelList.RowFilter = "";
                    dvMaterialList.RowFilter = "";
                }
                else
                {
                    dvMaterialList.RowFilter = "ProductCode = '" + cbProductName.SelectedValue.ToString() + "'";
                    LoadMachineCapametry();
                    LoadPersonnelCapametry();
                    SetGridColumns(mShowMode.SM_SELECTED);
                }

                dgMachines.Sort(dgMachines.Columns["MTemp5"], System.ComponentModel.ListSortDirection.Ascending);
                dgMaterials.Sort(dgMaterials.Columns["MaterialCode"], System.ComponentModel.ListSortDirection.Ascending);
                for (int I = 0, loopTo3 = dgMachines.Rows.Count - 1; I <= loopTo3; I += 2)
                {
                    for (int C = 7, loopTo4 = dgMachines.Columns.Count - 1; C <= loopTo4; C++)
                    {
                        if (Conversions.ToInteger(dgMachines.Rows[I].Cells[C].Value.ToString().Split(':')[0]) < Conversions.ToInteger(dgMachines.Rows[I + 1].Cells[C].Value.ToString().Split(':')[0]))
                        {
                            dgMachines.Rows[I + 1].Cells[C].Style.BackColor = Color.Red;
                        }
                        else if (Conversions.ToInteger(dgMachines.Rows[I].Cells[C].Value.ToString().Split(':')[0]) == Conversions.ToInteger(dgMachines.Rows[I + 1].Cells[C].Value.ToString().Split(':')[0]))
                        {
                            if (Conversions.ToInteger(dgMachines.Rows[I].Cells[C].Value.ToString().Split(':')[1]) < Conversions.ToInteger(dgMachines.Rows[I + 1].Cells[C].Value.ToString().Split(':')[1]))
                            {
                                dgMachines.Rows[I + 1].Cells[C].Style.BackColor = Color.Red;
                            }
                        }
                    }
                }

                for (int I = 0, loopTo5 = dgPersonnels.Rows.Count - 1; I <= loopTo5; I += 2)
                {
                    for (int C = 5, loopTo6 = dgPersonnels.Columns.Count - 1; C <= loopTo6; C++)
                    {
                        if (Conversions.ToInteger(dgPersonnels.Rows[I].Cells[C].Value.ToString().Split(':')[0]) < Conversions.ToInteger(dgPersonnels.Rows[I + 1].Cells[C].Value.ToString().Split(':')[0]))
                        {
                            dgPersonnels.Rows[I + 1].Cells[C].Style.BackColor = Color.Red;
                        }
                        else if (Conversions.ToInteger(dgPersonnels.Rows[I].Cells[C].Value.ToString().Split(':')[0]) == Conversions.ToInteger(dgPersonnels.Rows[I + 1].Cells[C].Value.ToString().Split(':')[0]))
                        {
                            if (Conversions.ToInteger(dgPersonnels.Rows[I].Cells[C].Value.ToString().Split(':')[1]) < Conversions.ToInteger(dgPersonnels.Rows[I + 1].Cells[C].Value.ToString().Split(':')[1]))
                            {
                                dgPersonnels.Rows[I + 1].Cells[C].Style.BackColor = Color.Red;
                            }
                        }
                    }
                }

                for (int I = 0, loopTo7 = dgMachines.Columns.Count - 1; I <= loopTo7; I++)
                    dgMachines.Columns[I].SortMode = DataGridViewColumnSortMode.NotSortable;
                for (int I = 0, loopTo8 = dgPersonnels.Columns.Count - 1; I <= loopTo8; I++)
                    dgPersonnels.Columns[I].SortMode = DataGridViewColumnSortMode.NotSortable;
                Module1.SetGridColumnsWidth(Name, 0, dgMachines);
                Module1.SetGridColumnsWidth(Name, 0, dgPersonnels);
                Module1.SetGridColumnsWidth(Name, 0, dgMaterials);
            }
        }

        private void tmrMachines_Tick(object sender, EventArgs e)
        {
            tmrMachines.Enabled = false;
            Cursor = Cursors.WaitCursor;
            LoadMachineCapametry();
            Cursor = Cursors.Default;
        }

        private void tmrPersonnels_Tick(object sender, EventArgs e)
        {
            tmrPersonnels.Enabled = false;
            Cursor = Cursors.WaitCursor;
            LoadPersonnelCapametry();
            Cursor = Cursors.Default;
        }

        private void LoadMachineCapametry()
        {
            var mdr = mdsCapametryShow.Tables["Tbl_MachineCapametry"].Select("ProductCode = '" + cbProductName.SelectedValue.ToString() + "'");
            {
                var withBlock = dgMachines;
                withBlock.Columns.Clear();
                withBlock.Columns.Add("MTemp1", "");
                withBlock.Columns.Add("MTemp2", "");
                withBlock.Columns.Add("MTemp3", "");
                withBlock.Columns.Add("MTemp4", "");
                withBlock.Columns[0].Visible = false;
                withBlock.Columns[1].Visible = false;
                withBlock.Columns[2].Visible = false;
                withBlock.Columns[3].Visible = false;
                withBlock.Columns.Add("MachineCode", "كد ماشين");
                withBlock.Columns.Add("MTemp5", "");
                withBlock.Columns[5].Visible = false;
                withBlock.Columns.Add("RecType", "");
                withBlock.Columns.Add("Month1", "فروردين");
                withBlock.Columns.Add("Month2", "ارديبهشت");
                withBlock.Columns.Add("Month3", "خرداد");
                withBlock.Columns.Add("Month4", "تير");
                withBlock.Columns.Add("Month5", "مرداد");
                withBlock.Columns.Add("Month6", "شهريور");
                withBlock.Columns.Add("Month7", "مهر");
                withBlock.Columns.Add("Month8", "آبان");
                withBlock.Columns.Add("Month9", "آذر");
                withBlock.Columns.Add("Month10", "دي");
                withBlock.Columns.Add("Month11", "بهمن");
                withBlock.Columns.Add("Month12", "اسفند");
                withBlock.Columns.Add("Total", "جمع");
            }

            var loopTo = (short)(mdr.Length - 1);
            for (I = 0; I <= loopTo; I++)
            {
                var drAccessible = mdsCapametryShow.Tables["Tbl_CapametryAccessibleTimes"].Select(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("MachineCode='", mdr[I]["MachineCode"]), "'")));
                if (drAccessible.Length > 0)
                {
                    dgMachines.Rows.Add("", "", "", "", drAccessible[0]["MachineCode"], "", "زمان در دسترس", drAccessible[0]["Month1"], drAccessible[0]["Month2"], drAccessible[0]["Month3"], drAccessible[0]["Month4"], drAccessible[0]["Month5"], drAccessible[0]["Month6"], drAccessible[0]["Month7"], drAccessible[0]["Month8"], drAccessible[0]["Month9"], drAccessible[0]["Month10"], drAccessible[0]["Month11"], drAccessible[0]["Month12"]);



                    dgMachines.Rows.Add("", "", "", "", mdr[I]["MachineCode"], "", mdr[I]["RecType"], mdr[I]["Month1"], mdr[I]["Month2"], mdr[I]["Month3"], mdr[I]["Month4"], mdr[I]["Month5"], mdr[I]["Month6"], mdr[I]["Month7"], mdr[I]["Month8"], mdr[I]["Month9"], mdr[I]["Month10"], mdr[I]["Month11"], mdr[I]["Month12"]);



                    dgMachines.Rows[dgMachines.Rows.Count - 2].Cells["Total"].Value = GetAccessibleTotal(drAccessible[0]);
                    dgMachines.Rows[dgMachines.Rows.Count - 1].Cells["Total"].Value = GetRequirmentTotal(dvMachineList, 7, 18, cbProductName.SelectedValue.ToString(), Conversions.ToString(drAccessible[0]["MachineCode"]));
                }
            }
        }

        private void LoadPersonnelCapametry()
        {
            var mdr = mdsCapametryShow.Tables["Tbl_PersonnelCapametry"].Select("ProductCode = '" + cbProductName.SelectedValue.ToString() + "'");
            {
                var withBlock = dgPersonnels;
                withBlock.Columns.Clear();
                withBlock.Columns.Add("PTemp1", "");
                withBlock.Columns.Add("PTemp2", "");
                withBlock.Columns.Add("PTemp3", "");
                withBlock.Columns.Add("PTemp4", "");
                withBlock.Columns[0].Visible = false;
                withBlock.Columns[1].Visible = false;
                withBlock.Columns[2].Visible = false;
                withBlock.Columns[3].Visible = false;
                withBlock.Columns.Add("RecType", "");
                withBlock.Columns.Add("Month1", "فروردين");
                withBlock.Columns.Add("Month2", "ارديبهشت");
                withBlock.Columns.Add("Month3", "خرداد");
                withBlock.Columns.Add("Month4", "تير");
                withBlock.Columns.Add("Month5", "مرداد");
                withBlock.Columns.Add("Month6", "شهريور");
                withBlock.Columns.Add("Month7", "مهر");
                withBlock.Columns.Add("Month8", "آبان");
                withBlock.Columns.Add("Month9", "آذر");
                withBlock.Columns.Add("Month10", "دي");
                withBlock.Columns.Add("Month11", "بهمن");
                withBlock.Columns.Add("Month12", "اسفند");
                withBlock.Columns.Add("Total", "جمع");
            }

            var loopTo = (short)(mdr.Length - 1);
            for (I = 0; I <= loopTo; I++)
            {
                var drAccessible = mdsCapametryShow.Tables["Tbl_CapametryAccessibleTimes"].Select("MachineCode='-1'");
                if (drAccessible.Length > 0)
                {
                    dgPersonnels.Rows.Add("", "", "", "", "زمان در دسترس", drAccessible[0]["Month1"], drAccessible[0]["Month2"], drAccessible[0]["Month3"], drAccessible[0]["Month4"], drAccessible[0]["Month5"], drAccessible[0]["Month6"], drAccessible[0]["Month7"], drAccessible[0]["Month8"], drAccessible[0]["Month9"], drAccessible[0]["Month10"], drAccessible[0]["Month11"], drAccessible[0]["Month12"]);



                    dgPersonnels.Rows.Add("", "", "", "", mdr[I]["RecType"], mdr[I]["Month1"], mdr[I]["Month2"], mdr[I]["Month3"], mdr[I]["Month4"], mdr[I]["Month5"], mdr[I]["Month6"], mdr[I]["Month7"], mdr[I]["Month8"], mdr[I]["Month9"], mdr[I]["Month10"], mdr[I]["Month11"], mdr[I]["Month12"]);



                    dgPersonnels.Rows[dgPersonnels.Rows.Count - 2].Cells["Total"].Value = GetAccessibleTotal(drAccessible[0]);
                    dgPersonnels.Rows[dgPersonnels.Rows.Count - 1].Cells["Total"].Value = GetRequirmentTotal(dvPersonnelList, 5, 16, cbProductName.SelectedValue.ToString(), "");
                }
            }
        }

        private void SetGridColumns(mShowMode GridsShowMode)
        {
            switch (GridsShowMode)
            {
                case mShowMode.SM_ALL:
                    {
                        dgMachines.DataSource = null;
                        dgPersonnels.DataSource = null;
                        dgMaterials.DataSource = null;
                        {
                            var withBlock = dgMachines;
                            withBlock.Columns.Clear();
                            withBlock.Columns.Add("MTemp1", "");
                            withBlock.Columns.Add("MTemp2", "");
                            withBlock.Columns.Add("MTemp3", "");
                            withBlock.Columns.Add("MTemp4", "");
                            withBlock.Columns[0].Visible = false;
                            withBlock.Columns[1].Visible = false;
                            withBlock.Columns[2].Visible = false;
                            withBlock.Columns[3].Visible = false;
                            withBlock.Columns.Add("MachineCode", "كد ماشين");
                            withBlock.Columns.Add("MTemp5", "");
                            withBlock.Columns[5].Visible = false;
                            withBlock.Columns.Add("RecType", "");
                            withBlock.Columns.Add("Month1", "فروردين");
                            withBlock.Columns.Add("Month2", "ارديبهشت");
                            withBlock.Columns.Add("Month3", "خرداد");
                            withBlock.Columns.Add("Month4", "تير");
                            withBlock.Columns.Add("Month5", "مرداد");
                            withBlock.Columns.Add("Month6", "شهريور");
                            withBlock.Columns.Add("Month7", "مهر");
                            withBlock.Columns.Add("Month8", "آبان");
                            withBlock.Columns.Add("Month9", "آذر");
                            withBlock.Columns.Add("Month10", "دي");
                            withBlock.Columns.Add("Month11", "بهمن");
                            withBlock.Columns.Add("Month12", "اسفند");
                            withBlock.Columns.Add("Total", "جمع");
                        }

                        {
                            var withBlock1 = dgPersonnels;
                            withBlock1.Columns.Clear();
                            withBlock1.Columns.Add("PTemp1", "");
                            withBlock1.Columns.Add("PTemp2", "");
                            withBlock1.Columns.Add("PTemp3", "");
                            withBlock1.Columns.Add("PTemp4", "");
                            withBlock1.Columns[0].Visible = false;
                            withBlock1.Columns[1].Visible = false;
                            withBlock1.Columns[2].Visible = false;
                            withBlock1.Columns[3].Visible = false;
                            withBlock1.Columns.Add("RecType", "");
                            withBlock1.Columns.Add("Month1", "فروردين");
                            withBlock1.Columns.Add("Month2", "ارديبهشت");
                            withBlock1.Columns.Add("Month3", "خرداد");
                            withBlock1.Columns.Add("Month4", "تير");
                            withBlock1.Columns.Add("Month5", "مرداد");
                            withBlock1.Columns.Add("Month6", "شهريور");
                            withBlock1.Columns.Add("Month7", "مهر");
                            withBlock1.Columns.Add("Month8", "آبان");
                            withBlock1.Columns.Add("Month9", "آذر");
                            withBlock1.Columns.Add("Month10", "دي");
                            withBlock1.Columns.Add("Month11", "بهمن");
                            withBlock1.Columns.Add("Month12", "اسفند");
                            withBlock1.Columns.Add("Total", "جمع");
                        }

                        {
                            var withBlock2 = dgMaterials;
                            withBlock2.Columns.Clear();
                            withBlock2.Columns.Add("MTTemp1", "");
                            withBlock2.Columns.Add("MTTemp2", "");
                            withBlock2.Columns.Add("MTTemp3", "");
                            withBlock2.Columns.Add("MTTemp4", "");
                            withBlock2.Columns[0].Visible = false;
                            withBlock2.Columns[1].Visible = false;
                            withBlock2.Columns[2].Visible = false;
                            withBlock2.Columns[3].Visible = false;
                            withBlock2.Columns.Add("MaterialCode", "كد مواد");
                            withBlock2.Columns.Add("MTTemp5", "");
                            withBlock2.Columns[5].Visible = false;
                            withBlock2.Columns.Add("Title", "واحد");
                            withBlock2.Columns.Add("MTTemp6", "");
                            withBlock2.Columns[7].Visible = false;
                            withBlock2.Columns.Add("Month1", "فروردين");
                            withBlock2.Columns.Add("Month2", "ارديبهشت");
                            withBlock2.Columns.Add("Month3", "خرداد");
                            withBlock2.Columns.Add("Month4", "تير");
                            withBlock2.Columns.Add("Month5", "مرداد");
                            withBlock2.Columns.Add("Month6", "شهريور");
                            withBlock2.Columns.Add("Month7", "مهر");
                            withBlock2.Columns.Add("Month8", "آبان");
                            withBlock2.Columns.Add("Month9", "آذر");
                            withBlock2.Columns.Add("Month10", "دي");
                            withBlock2.Columns.Add("Month11", "بهمن");
                            withBlock2.Columns.Add("Month12", "اسفند");
                            withBlock2.Columns.Add("Total", "جمع");
                        }

                        break;
                    }

                case mShowMode.SM_SELECTED:
                    {
                        {
                            var withBlock3 = dgMaterials;
                            withBlock3.Columns.Clear();
                            withBlock3.DataSource = null;
                            withBlock3.DataSource = dvMaterialList;
                            withBlock3.Columns[0].Visible = false;
                            withBlock3.Columns[1].Visible = false;
                            withBlock3.Columns[2].Visible = false;
                            withBlock3.Columns[3].Visible = false;
                            withBlock3.Columns[4].HeaderText = "کد مواد";
                            withBlock3.Columns[5].Visible = false;
                            withBlock3.Columns[6].HeaderText = "واحد";
                            withBlock3.Columns[7].Visible = false;
                            withBlock3.Columns[8].HeaderText = "فروردین";
                            withBlock3.Columns[9].HeaderText = "اردیبهشت";
                            withBlock3.Columns[10].HeaderText = "خرداد";
                            withBlock3.Columns[11].HeaderText = "تیر";
                            withBlock3.Columns[12].HeaderText = "مرداد";
                            withBlock3.Columns[13].HeaderText = "شهریور";
                            withBlock3.Columns[14].HeaderText = "مهر";
                            withBlock3.Columns[15].HeaderText = "آبان";
                            withBlock3.Columns[16].HeaderText = "آذر";
                            withBlock3.Columns[17].HeaderText = "دی";
                            withBlock3.Columns[18].HeaderText = "بهمن";
                            withBlock3.Columns[19].HeaderText = "اسفند";
                            withBlock3.Columns[20].HeaderText = "جمع";
                        }

                        break;
                    }
            }
        }

        private string GetRequirmentTotal(DataView mDateViewList, short ColStart, short ColEnd, string mProductCode, string mMachineCode)
        {
            var RowHours = default(int);
            var RowMins = default(int);
            string mOldFilter = mDateViewList.RowFilter;
            mDateViewList.RowFilter = "ProductCode = '" + mProductCode + "'";
            if (!mMachineCode.Equals(""))
            {
                mDateViewList.RowFilter += " And MachineCode = '" + mMachineCode + "'";
            }

            for (short I = ColStart, loopTo = ColEnd; I <= loopTo; I++)
            {
                RowHours += Conversions.ToInteger(Strings.Mid(Conversions.ToString(mDateViewList[0][I]), 1, Conversions.ToString(mDateViewList[0][I]).IndexOf(":")));
                RowMins += Conversions.ToInteger(Strings.Mid(Conversions.ToString(mDateViewList[0][I]), Conversions.ToString(mDateViewList[0][I]).IndexOf(":") + 2));
            }

            mDateViewList.RowFilter = mOldFilter;
            RowHours = (int)Math.Round(RowHours + RowMins / 60d);
            RowMins = RowMins % 60;
            return Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(RowHours < 10, "0" + RowHours, RowHours), ":"), Interaction.IIf(RowMins < 10, "0" + RowMins, RowMins)));
        }

        private string GetAccessibleTotal(DataRow mDataRow)
        {
            var RowHours = default(int);
            var RowMins = default(int);
            for (short I = 1; I <= 12; I++)
            {
                RowHours += Conversions.ToInteger(Strings.Mid(Conversions.ToString(mDataRow["Month" + I.ToString()]), 1, Conversions.ToString(mDataRow["Month" + I.ToString()]).IndexOf(":")));
                RowMins += Conversions.ToInteger(Strings.Mid(Conversions.ToString(mDataRow["Month" + I.ToString()]), Conversions.ToString(mDataRow["Month" + I.ToString()]).IndexOf(":") + 2));
            }

            RowHours = (int)Math.Round(RowHours + RowMins / 60d);
            RowMins = RowMins % 60;
            return Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Interaction.IIf(RowHours < 10, "0" + RowHours, RowHours), ":"), Interaction.IIf(RowMins < 10, "0" + RowMins, RowMins)));
        }

        private void dgMachines_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true & e.KeyCode == Keys.F12)
            {
                Module1.ExportGridToExcel(dgMachines);
            }
        }

        private void dgMaterials_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true & e.KeyCode == Keys.F12)
            {
                Module1.ExportGridToExcel(dgMaterials);
            }
        }

        private void dgPersonnels_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true & e.KeyCode == Keys.F12)
            {
                Module1.ExportGridToExcel(dgPersonnels);
            }
        }
    }
}