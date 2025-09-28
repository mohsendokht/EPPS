using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
//using Microsoft.Office.Interop.Owc11;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    public partial class frmOEE
    {
        public frmOEE()
        {
            InitializeComponent();
            _cmdPrint.Name = "cmdPrint";
            _cmdSave.Name = "cmdSave";
            _cmdCalc.Name = "cmdCalc";
            _cmdCancel.Name = "cmdCancel";
            _cmdDeSelectAll.Name = "cmdDeSelectAll";
            _cmdSelectAll.Name = "cmdSelectAll";
            _chkFullDayBase.Name = "chkFullDayBase";
            _chkCalendarBase.Name = "chkCalendarBase";
            //_cmdShowChart.Name = "cmdShowChart";
        }

        private frmRecordsLists mListForm;
        private DataTable dtMachines = new DataTable();
        private ArrayList[] MachinesOEEs = null;
        private ArrayList[] AvailabilityDetails = null;
        private ArrayList[] PerformanceDetails = null;
        private ArrayList[] QualityDetails = null;
        private DataRow mCurrentRow = null;

        public frmRecordsLists ListForm
        {
            get
            {
                return mListForm;
            }

            set
            {
                mListForm = value;
            }
        }

        private void frmOEE_Load(object sender, EventArgs e)
        {
            MdiParent = mListForm.MdiParent;
            var daOEE = new SqlDataAdapter("Select * From Tbl_Machines Where Code <> '-1' Order By Code", Module1.cnProductionPlanning);
            daOEE.Fill(dtMachines);
            for (int I = 0, loopTo = dtMachines.Rows.Count - 1; I <= loopTo; I++)
                lstMachines.Items.Add(dtMachines.Rows[I]["Code"]);
            if (mListForm.FormMode == (int)Module1.FormModeEnum.EDIT_MODE)
            {
                mCurrentRow = mListForm.GetRow();
                if (mCurrentRow is null)
                {
                    MessageBox.Show("یافت نشد OEE مشخصات", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    Close();
                    return;
                }
                else
                {
                    cmdSave.Tag = mCurrentRow["OEEIndex"];
                    txtFromDate.Text = Conversions.ToString(mCurrentRow["StartDate"]);
                    txtFromHour.Text = Conversions.ToString(mCurrentRow["StartHour"]);
                    txtToDate.Text = Conversions.ToString(mCurrentRow["EndDate"]);
                    txtToHour.Text = Conversions.ToString(mCurrentRow["EndHour"]);
                    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(mCurrentRow["CalcBase"], 1, false)))
                    {
                        chkCalendarBase.Checked = true;
                    }
                    else
                    {
                        chkFullDayBase.Checked = true;
                        txtDownTime.Text = Conversions.ToString(mCurrentRow["DownTime"]);
                    }

                    txtDescription.Text = Conversions.ToString(mCurrentRow["Description"]);
                }

                using (var cn = new SqlConnection(Module1.PlanningCnnStr))
                {
                    cn.Open();
                    var cmOEE = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Select * From Tbl_OEEDetails Where OEEIndex =", mCurrentRow["OEEIndex"]), " Order By MachineCode")), cn);
                    var drOEE = cmOEE.ExecuteReader();
                    int HasCounter = -1;
                    if (drOEE.HasRows)
                    {
                        MachinesOEEs = new ArrayList[lstMachines.Items.Count];
                        AvailabilityDetails = new ArrayList[lstMachines.Items.Count];
                        PerformanceDetails = new ArrayList[lstMachines.Items.Count];
                        QualityDetails = new ArrayList[lstMachines.Items.Count];
                    }

                    while (drOEE.Read())
                    {
                        for (int I = 0, loopTo1 = lstMachines.Items.Count - 1; I <= loopTo1; I++)
                        {
                            if (lstMachines.Items[I].Equals(drOEE["MachineCode"]))
                            {
                                HasCounter += 1;
                                lstMachines.SetItemChecked(I, true);
                                double MachineAvailability;
                                double MachinePerformance;
                                double MachineQuality;
                                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(drOEE["AvailabilityRealTime"], 0, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(drOEE["AvailabilityTotalTime"], 0, false)))
                                {
                                    MachineAvailability = 0d;
                                }
                                else
                                {
                                    MachineAvailability = Conversions.ToDouble(drOEE["AvailabilityRealTime"]) / Conversions.ToDouble(drOEE["AvailabilityTotalTime"]);
                                    MachineAvailability = Math.Round(MachineAvailability, 4);
                                }

                                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(drOEE["PerformanceRealQuantity"], 0, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(drOEE["PerformanceStandardQuantity"], 0, false)))
                                {
                                    MachinePerformance = 0d;
                                }
                                else
                                {
                                    MachinePerformance = Conversions.ToDouble(drOEE["PerformanceRealQuantity"]) / Conversions.ToDouble(drOEE["PerformanceStandardQuantity"]);
                                    MachinePerformance = Math.Round(MachinePerformance, 4);
                                }

                                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(drOEE["QualityIntactQuantity"], 0, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(drOEE["QualityTotalQuantity"], 0, false)))
                                {
                                    MachineQuality = 0d;
                                }
                                else
                                {
                                    MachineQuality = Conversions.ToDouble(drOEE["QualityIntactQuantity"]) / Conversions.ToDouble(drOEE["QualityTotalQuantity"]);
                                    MachineQuality = Math.Round(MachineQuality, 4);
                                }

                                dgOEEList.Rows.Add(drOEE["MachineCode"], MachineAvailability.ToString(), MachinePerformance.ToString(), MachineQuality.ToString(), Math.Round(Conversions.ToDouble(drOEE["OEEPercent"]), 2));
                                MachinesOEEs[HasCounter] = new ArrayList();
                                MachinesOEEs[HasCounter].Add(lstMachines.Items[I]);
                                MachinesOEEs[HasCounter].Add(drOEE["OEEPercent"]);
                                AvailabilityDetails[HasCounter] = new ArrayList();
                                AvailabilityDetails[HasCounter].Add(lstMachines.Items[I]);
                                AvailabilityDetails[HasCounter].Add(drOEE["AvailabilityRealTime"]);
                                AvailabilityDetails[HasCounter].Add(drOEE["AvailabilityTotalTime"]);
                                AvailabilityDetails[HasCounter].Add(MachineAvailability);
                                PerformanceDetails[HasCounter] = new ArrayList();
                                PerformanceDetails[HasCounter].Add(lstMachines.Items[I]);
                                PerformanceDetails[HasCounter].Add(drOEE["PerformanceRealQuantity"]);
                                PerformanceDetails[HasCounter].Add(drOEE["PerformanceStandardQuantity"]);
                                PerformanceDetails[HasCounter].Add(MachinePerformance);
                                QualityDetails[HasCounter] = new ArrayList();
                                QualityDetails[HasCounter].Add(lstMachines.Items[I]);
                                QualityDetails[HasCounter].Add(drOEE["QualityIntactQuantity"]);
                                QualityDetails[HasCounter].Add(drOEE["QualityTotalQuantity"]);
                                QualityDetails[HasCounter].Add(MachineQuality);
                            }
                        }
                    }

                    if (drOEE.HasRows)
                    {
                        SetTotalOEE();
                    }

                    drOEE.Close();
                    cn.Close();
                }
            }
        }

        private void chkCalendarBase_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCalendarBase.Checked)
            {
                chkFullDayBase.Checked = false;
            }
            else if (!chkFullDayBase.Checked)
            {
                chkCalendarBase.Checked = true;
            }
        }

        private void chkFullDayBase_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFullDayBase.Checked)
            {
                chkCalendarBase.Checked = false;
            }
            else if (!chkCalendarBase.Checked)
            {
                chkFullDayBase.Checked = true;
            }

            txtDownTime.Enabled = chkFullDayBase.Checked;
        }

        private void cmdSelectAll_Click(object sender, EventArgs e)
        {
            for (int I = 0, loopTo = lstMachines.Items.Count - 1; I <= loopTo; I++)
                lstMachines.SetItemChecked(I, true);
        }

        private void cmdDeSelectAll_Click(object sender, EventArgs e)
        {
            for (int I = 0, loopTo = lstMachines.Items.Count - 1; I <= loopTo; I++)
                lstMachines.SetItemChecked(I, false);
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdCalc_Click(object sender, EventArgs e)
        {
            CalcOEE();
        }

        //private void cmdShowChart_Click(object sender, EventArgs e)
        //{
        //    ArrayList[] CharArr = null;
        //    int ValueColIndex = -1;
        //    if (rbOEE.Checked)
        //    {
        //        CharArr = MachinesOEEs;
        //        ValueColIndex = 1;
        //    }
        //    else if (rbAvailability.Checked)
        //    {
        //        CharArr = AvailabilityDetails;
        //        ValueColIndex = 3;
        //    }
        //    else if (rbPerformance.Checked)
        //    {
        //        CharArr = PerformanceDetails;
        //        ValueColIndex = 3;
        //    }
        //    else if (rbQuality.Checked)
        //    {
        //        CharArr = QualityDetails;
        //        ValueColIndex = 3;
        //    }

        //    if (CharArr is null || CharArr.Length == 0)
        //    {
        //        MessageBox.Show("محاسبه ای انجام نشده است", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
        //        return;
        //    }

        //    acsOEE.Clear();
        //    acsOEE.Charts.Add();
        //    acsOEE.Charts[0].Type = ChartChartTypeEnum.chChartTypeColumnClustered3D;
        //    // chChartTypeBarStacked1003D 'chChartTypeBarStacked100 'chChartTypeBarStacked 'chChartTypeBarClustered3D 'chChartTypeBarClustered 'chChartTypeBar3D
        //    // chChartTypeBarStacked3D 'chChartTypeColumn3D 'chChartTypeColumnClustered 'chChartTypeColumnClustered3D 'chChartTypeColumnStacked 'chChartTypeColumnStacked100
        //    // chChartTypeColumnStacked1003D 'chChartTypeColumnStacked3D 'chChartTypeLineMarkers 'chChartTypeLineStacked100Markers 'chChartTypeLineStackedMarkers
        //    // chChartTypeRadarLineMarkers 'chChartTypeRadarSmoothLineMarkers 'chChartTypeSmoothLineStacked100Markers 'chChartTypeSmoothLineStackedMarkers
        //    acsOEE.Charts[0].HasLegend = false;
        //    acsOEE.HasChartSpaceLegend = true;
        //    acsOEE.ChartSpaceLegend.Position = ChartLegendPositionEnum.chLegendPositionBottom;
        //    var dtChart = new DataTable();
        //    dtChart.Columns.Add("MachineCode", Type.GetType("System.String"));
        //    dtChart.Columns.Add("Value", Type.GetType("System.Double"));
        //    for (int I = 0, loopTo = CharArr.Length - 1; I <= loopTo; I++)
        //    {
        //        if (CharArr[I] is null)
        //        {
        //            break;
        //        }

        //        var drChart = dtChart.NewRow();
        //        drChart["MachineCode"] = CharArr[I][0];
        //        drChart["Value"] = Math.Round(Conversions.ToDouble(CharArr[I][ValueColIndex]), 4);
        //        dtChart.Rows.Add(drChart);
        //    }

        //    dtChart.DefaultView.Sort = "Value Desc";
        //    for (int I = 0, loopTo1 = dtChart.DefaultView.Count - 1; I <= loopTo1; I++)
        //    {
        //        acsOEE.Charts[0].SeriesCollection.Add();
        //        acsOEE.Charts[0].SeriesCollection[I].Caption = Conversions.ToString(dtChart.DefaultView[I]["MachineCode"]);
        //        acsOEE.Charts[0].SeriesCollection[I].SetData(ChartDimensionsEnum.chDimValues, (int)ChartSpecialDataSourcesEnum.chDataLiteral, dtChart.DefaultView[I]["Value"]);
        //        acsOEE.Charts[0].SeriesCollection[I].TipText = Conversions.ToString(dtChart.DefaultView[I]["Value"]);
        //        acsOEE.Charts[0].SeriesCollection[I].DataLabelsCollection.Add();
        //        // acsOEE.Charts(0).SeriesCollection(I).DataLabelsCollection.Item(0).HasSeriesName = False
        //        // acsOEE.Charts(0).SeriesCollection(I).DataLabelsCollection.Item(0).HasValue = True
        //        acsOEE.Charts[0].SeriesCollection[I].DataLabelsCollection[0].Interior.SetOneColorGradient(ChartGradientStyleEnum.chGradientFromCenter, ChartGradientVariantEnum.chGradientVariantEnd, 1d, "red");
        //        acsOEE.Charts[0].SeriesCollection[I].DataLabelsCollection[0].Interior.Color = "Green";
        //    }

        //    dtChart.Dispose();
        //    acsOEE.Charts[0].Axes.Delete(0);
        //    for (int I = 0, loopTo2 = acsOEE.Charts[0].Axes.Count - 1; I <= loopTo2; I++)
        //    {
        //        acsOEE.Charts[0].Axes[I].Font.Color = "Blue";
        //        //acsOEE.Charts[0].Axes[I].Font = "Tahoma";
        //        acsOEE.Charts[0].Axes[I].Font.Size = 10;
        //    }

        //    acsOEE.Charts[0].PlotArea.Interior.Color = "Pink";
        //}

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (AvailabilityDetails is null || PerformanceDetails is null || QualityDetails is null)
            {
                if (MessageBox.Show("انجام نشده است و برای ادامۀ ثبت محاسبه انجام می شود. آیا عملیات ثبت را ادامه می دهید OEE کاربر گرامی، محاسبۀ", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) == DialogResult.Yes)
                {
                    if (!CalcOEE())
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            SqlTransaction trnOEE = null;
            long NewCode = Module1.GetNewCode("Tbl_OEEs", "OEEIndex");
            string CalcDate = Module1.mServerShamsiDate;
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                try
                {
                    cn.Open();
                    trnOEE = cn.BeginTransaction();
                    var cmOEE = new SqlCommand(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Insert Into Tbl_OEEs(OEEIndex,CalcDate,StartDate,StartHour,EndDate,EndHour,CalcBase,DownTime,[Description]) " + "Values(" + NewCode + ",'" + CalcDate + "','" + txtFromDate.Text + "','" + txtFromHour.Text + "','" + txtToDate.Text + "','" + txtToHour.Text + "',", Interaction.IIf(chkCalendarBase.Checked, 1, 2)), ",'"), Interaction.IIf(chkCalendarBase.Checked, "00:00", txtDownTime.Text)), "','"), txtDescription.Text), "')")), cn);


                    cmOEE.Transaction = trnOEE;
                    if (cmdSave.Tag is null || string.IsNullOrEmpty(cmdSave.Tag.ToString()) || cmdSave.Tag.ToString().Equals(""))
                    {
                        // OEE ثبت رکورد جدید جهت مشخصات محاسبۀ
                        cmOEE.ExecuteNonQuery();
                        mCurrentRow = mListForm.dsProductionPlanning.Tables["Tbl_OEEs"].NewRow();
                        mCurrentRow["OEEIndex"] = NewCode;
                        mCurrentRow["CalcDate"] = CalcDate;
                        mCurrentRow["StartDate"] = txtFromDate.Text;
                        mCurrentRow["StartHour"] = txtFromHour.Text;
                        mCurrentRow["EndDate"] = txtToDate.Text;
                        mCurrentRow["EndHour"] = txtToHour.Text;
                        if (chkCalendarBase.Checked)
                        {
                            mCurrentRow["CalcBase"] = 1;
                            mCurrentRow["DownTime"] = "00:00";
                        }
                        else
                        {
                            mCurrentRow["CalcBase"] = 2;
                            mCurrentRow["DownTime"] = txtDownTime.Text;
                        }

                        mCurrentRow["Description"] = txtDescription.Text;
                        mListForm.dsProductionPlanning.Tables["Tbl_OEEs"].Rows.Add(mCurrentRow);

                        // OEE ثبت جزئیات محاسبۀ
                        for (int I = 0, loopTo = MachinesOEEs.Length - 1; I <= loopTo; I++)
                        {
                            cmOEE.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Insert Into Tbl_OEEDetails(OEEIndex,MachineCode,AvailabilityRealTime,AvailabilityTotalTime,PerformanceRealQuantity,PerformanceStandardQuantity,QualityIntactQuantity,QualityTotalQuantity,OEEPercent) " + "Values(" + NewCode + ",'", MachinesOEEs[I][0]), "','"), AvailabilityDetails[I][1]), "','"), AvailabilityDetails[I][2]), "','"), PerformanceDetails[I][1]), "','"), PerformanceDetails[I][2]), "','"), QualityDetails[I][1]), "','"), QualityDetails[I][2]), "','"), MachinesOEEs[I][1]), "')"));
                            cmOEE.ExecuteNonQuery();
                        }
                    }
                    else if (MessageBox.Show("مشخصات قبلا ثبت شده، آیا برای ثبت مشخصات جدید مطمئن هستید؟", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) == DialogResult.Yes)
                    {
                        // OEE به روزرسانی رکورد ثبت شدۀ مشخصات محاسبۀ
                        NewCode = Conversions.ToLong(cmdSave.Tag);
                        cmOEE.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Update Tbl_OEEs Set CalcDate = '" + CalcDate + "',StartDate = '" + txtFromDate.Text + "',StartHour = '" + txtFromHour.Text + "', EndDate = '" + txtToDate.Text + "',EndHour = '" + txtToHour.Text + "',CalcBase = ", Interaction.IIf(chkCalendarBase.Checked, 1, 2)), ",DownTime = '"), Interaction.IIf(chkCalendarBase.Checked, "00:00", txtDownTime.Text)), "',[Description] = '"), txtDescription.Text), "' Where OEEIndex = "), NewCode));


                        cmOEE.ExecuteNonQuery();
                        mCurrentRow.BeginEdit();
                        mCurrentRow["CalcDate"] = CalcDate;
                        mCurrentRow["StartDate"] = txtFromDate.Text;
                        mCurrentRow["StartHour"] = txtFromHour.Text;
                        mCurrentRow["EndDate"] = txtToDate.Text;
                        mCurrentRow["EndHour"] = txtToHour.Text;
                        if (chkCalendarBase.Checked)
                        {
                            mCurrentRow["CalcBase"] = 1;
                            mCurrentRow["DownTime"] = "00:00";
                        }
                        else
                        {
                            mCurrentRow["CalcBase"] = 2;
                            mCurrentRow["DownTime"] = txtDownTime.Text;
                        }

                        mCurrentRow["Description"] = txtDescription.Text;
                        mCurrentRow.EndEdit();

                        // ی قبلیOEE حذف جزئیات محاسبۀ
                        cmOEE.CommandText = "Delete From Tbl_OEEDetails Where OEEIndex = " + NewCode;
                        cmOEE.ExecuteNonQuery();

                        // OEE ثبت جزئیات محاسبۀ
                        for (int I = 0, loopTo1 = MachinesOEEs.Length - 1; I <= loopTo1; I++)
                        {
                            cmOEE.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Insert Into Tbl_OEEDetails(OEEIndex,MachineCode,AvailabilityRealTime,AvailabilityTotalTime,PerformanceRealQuantity,PerformanceStandardQuantity,QualityIntactQuantity,QualityTotalQuantity,OEEPercent) " + "Values(" + NewCode + ",'", MachinesOEEs[I][0]), "','"), AvailabilityDetails[I][1]), "','"), AvailabilityDetails[I][2]), "','"), PerformanceDetails[I][1]), "','"), PerformanceDetails[I][2]), "','"), QualityDetails[I][1]), "','"), QualityDetails[I][2]), "','"), MachinesOEEs[I][1]), "')"));
                            cmOEE.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        trnOEE.Commit();
                        return;
                    }

                    trnOEE.Commit();
                    mListForm.dsProductionPlanning.AcceptChanges();
                    cmdSave.Tag = NewCode.ToString();
                    MessageBox.Show("محاسبات ثبت شد.", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                }
                catch (Exception ex)
                {
                    mListForm.dsProductionPlanning.RejectChanges();
                    trnOEE.Rollback();
                    Logger.SaveError(Name + ".cmdSave_Click", ex.Message);
                    MessageBox.Show("خطا در ثبت اطلاعات", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                }
                finally
                {
                    trnOEE = null;
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }
        }

        private bool CalcOEE()
        {
            if (!FormValidation())
            {
                return false;
            }
            // mohsendokht remark
            // Try
            //acsOEE.Clear();
            Cursor = Cursors.WaitCursor;
            if (TabControl1.TabPages.Count > 2)
            {
                TabControl1.TabPages["tpPrintPreview"].Dispose();
            }

            dgOEEList.Rows.Clear();
            Application.DoEvents();
            prgOEE.Visible = true;
            lblOEECalcAction.Visible = true;
            prgOEE.Minimum = 0;
            prgOEE.Maximum = lstMachines.CheckedItems.Count - 1;
            prgOEE.Value = 0;
            lblOEECalcAction.Text = "Calc Availability(s), please wait...";
            CalcAvailability();
            prgOEE.Value = 0;
            lblOEECalcAction.Text = "Calc Performance(s), please wait...";
            CalcPerformance();
            prgOEE.Value = 0;
            lblOEECalcAction.Text = "Calc Quality(s), please wait...";
            CalcQuality();
            MachinesOEEs = new ArrayList[lstMachines.CheckedItems.Count];
            lblOEECalcAction.Text = "Calc OEE(s), please wait...";
            for (int I = 0, loopTo = lstMachines.CheckedItems.Count - 1; I <= loopTo; I++)
            {
                Application.DoEvents();
                MachinesOEEs[I] = new ArrayList();
                MachinesOEEs[I].Add(lstMachines.CheckedItems[I]);
                MachinesOEEs[I].Add(Operators.MultiplyObject(Operators.MultiplyObject(Operators.MultiplyObject(AvailabilityDetails[I][3], PerformanceDetails[I][3]), QualityDetails[I][3]), 100));
                dgOEEList.Rows.Add(lstMachines.CheckedItems[I], Math.Round((double)AvailabilityDetails[I][3], 4), Math.Round((double)PerformanceDetails[I][3], 4), Math.Round((double)QualityDetails[I][3] , 4), Math.Round((double)MachinesOEEs[I][1], 2));
                prgOEE.Value = I;
            }

            SetTotalOEE();
            lblOEECalcAction.Text = "#";
            prgOEE.Visible = false;
            lblOEECalcAction.Visible = false;
            Application.DoEvents();
            Cursor = Cursors.Default;
            return true;
        }

        private void CalcAvailability()
        {
            AvailabilityDetails = new ArrayList[lstMachines.CheckedItems.Count];
            for (int I = 0, loopTo = lstMachines.CheckedItems.Count - 1; I <= loopTo; I++)
            {
                Application.DoEvents();
                AvailabilityDetails[I] = new ArrayList();
                AvailabilityDetails[I].Add(lstMachines.CheckedItems[I]);
                // بدست آوردن کل زمان در دسترس ماشین
                double TotalAvailabilityTime = GetMachineTotalAvailabilityTime(Conversions.ToString(lstMachines.CheckedItems[I]), txtFromDate.Text, txtFromHour.Text, txtToDate.Text, txtToHour.Text);
                // های) ماشینSetup) بدست آوردن مجموع زمان تنظیمات
                double MachineSetupTime = GetMachineSetupTime(Conversions.ToString(lstMachines.CheckedItems[I]), txtFromDate.Text, txtFromHour.Text, txtToDate.Text, txtToHour.Text);
                // های) ماشینHalt) بدست آوردن مجموع زمان توقفات
                double MachineHaltTime = GetMachineHaltTime(Conversions.ToString(lstMachines.CheckedItems[I]), txtFromDate.Text, txtFromHour.Text, txtToDate.Text, txtToHour.Text);
                // بدست آوردن مجموع زمانهایی که ماشین در دسترس نبوده است
                double MachinePMTime = GetMachinePMTime(Conversions.ToString(lstMachines.CheckedItems[I]), txtFromDate.Text, txtFromHour.Text, txtToDate.Text, txtToHour.Text);
                double MachineRealAvailabilityTime = TotalAvailabilityTime - (MachineSetupTime + MachineHaltTime + MachinePMTime);
                double MachineAvailability = MachineRealAvailabilityTime / TotalAvailabilityTime;
                AvailabilityDetails[I].Add(MachineRealAvailabilityTime);
                AvailabilityDetails[I].Add(TotalAvailabilityTime);
                AvailabilityDetails[I].Add(MachineAvailability);
                prgOEE.Value = I;
            }
        }

        private void CalcPerformance()
        {
            PerformanceDetails = new ArrayList[lstMachines.CheckedItems.Count];
            for (int I = 0, loopTo = lstMachines.CheckedItems.Count - 1; I <= loopTo; I++)
            {
                Application.DoEvents();
                PerformanceDetails[I] = new ArrayList();
                PerformanceDetails[I].Add(lstMachines.CheckedItems[I]);
                double MachinePerformance = GetMachinePerformance(Conversions.ToString(lstMachines.CheckedItems[I]), txtFromDate.Text, txtFromHour.Text, txtToDate.Text, txtToHour.Text, PerformanceDetails[I]);
                PerformanceDetails[I].Add(MachinePerformance);
                prgOEE.Value = I;
            }
        }

        private void CalcQuality()
        {
            QualityDetails = new ArrayList[lstMachines.CheckedItems.Count];
            for (int I = 0, loopTo = lstMachines.CheckedItems.Count - 1; I <= loopTo; I++)
            {
                Application.DoEvents();
                QualityDetails[I] = new ArrayList();
                QualityDetails[I].Add(lstMachines.CheckedItems[I]);
                double MachineQuality = GetMachineQuality(Conversions.ToString(lstMachines.CheckedItems[I]), txtFromDate.Text, txtFromHour.Text, txtToDate.Text, txtToHour.Text, QualityDetails[I]);
                QualityDetails[I].Add(MachineQuality);
                prgOEE.Value = I;
            }
        }

        private void SetTotalOEE()
        {
            double TAReal = 0.0d;
            double TATotal = 0.0d;
            double TPReal = 0.0d;
            double TPStandard = 0.0d;
            double TQIntact = 0.0d;
            double TQTotal = 0.0d;
            for (int I = 0, loopTo = MachinesOEEs.Length - 1; I <= loopTo; I++)
            {
                if (MachinesOEEs[I] is null)
                {
                    break;
                }

                TAReal = TAReal + Conversions.ToDouble(AvailabilityDetails[I][1]);
                TATotal = TATotal + Conversions.ToDouble(AvailabilityDetails[I][2]);
                TPReal = TPReal +   Conversions.ToDouble(PerformanceDetails[I][1].ToString());
                TPStandard = TPStandard +  Conversions.ToDouble(PerformanceDetails[I][2]);
                TQIntact = TQIntact +  Conversions.ToDouble( QualityDetails[I][1]);
                TQTotal = TQTotal +  Conversions.ToDouble( QualityDetails[I][2]);
            }

            if (TAReal == 0d || TATotal == 0d)
            {
                lblAllAvailability.Text = "0";
            }
            else
            {
                lblAllAvailability.Text = Math.Round(TAReal / TATotal, 4).ToString();
            }

            if (TPReal == 0d || TPStandard == 0d)
            {
                lblAllPerformance.Text = "0";
            }
            else
            {
                lblAllPerformance.Text = Math.Round(TPReal / TPStandard, 4).ToString();
            }

            if (TQIntact == 0d || TQTotal == 0d)
            {
                lblAllQuality.Text = "0";
            }
            else
            {
                lblAllQuality.Text = Math.Round(TQIntact / TQTotal, 4).ToString();
            }

            lblAllOEE.Text = Math.Round(Conversions.ToDouble(lblAllAvailability.Text) * Conversions.ToDouble(lblAllPerformance.Text) * Conversions.ToDouble(lblAllQuality.Text) * 100d, 2).ToString();
        }

        private double GetMachineTotalAvailabilityTime(string MachineCode, string StartDate, string StartHour, string EndDate, string EndHour)
        {
            int CalendarCode;
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmDayDuration = new SqlCommand("Select IsNull(CalendarCode,0) From Tbl_Machines Where Code = '" + MachineCode + "'", cn);
                CalendarCode = Conversions.ToInteger(cmDayDuration.ExecuteScalar().ToString());
                cn.Close();
            }

            return GetDaysDurationTime(StartDate, StartHour, EndDate, EndHour, CalendarCode);
        }

        private double GetDaysDurationTime(string StartDate, string StartHour, string EndDate, string EndHour, int CalendarCode)
        {
            double DayTime = 0d;
            if (chkCalendarBase.Checked)
            {
                string CalendarStart = Module1.GetCalendarStart(CalendarCode.ToString());
                string CalendarEnd = Module1.GetCalendarEnd(CalendarCode.ToString());
                while (true)
                {
                    if (Module1.IsHoliday(CalendarCode.ToString(), StartDate))
                    {
                        DayTime += 0.0d;
                    }
                    else
                    {
                        short ParticulareDayType = Module1.IsParticularDay(CalendarCode.ToString(), StartDate);
                        short DayNo = Convert.ToInt16(Module1.GetDayNo(StartDate));
                        string DayAccessibleTime = "0:0";
                        // Const DT_HOLIDAY = 1
                        const int DT_COMMON = 2;
                        switch (ParticulareDayType)
                        {
                            case -1: // روز عادی
                                {
                                    DayAccessibleTime = Module1.GetDayAccessibleTime(CalendarCode.ToString(), DayNo.ToString(), 1);
                                    break;
                                }

                            case DT_COMMON: // روز خاص غیر تعطیل
                                {
                                    DayAccessibleTime = Module1.GetDayAccessibleTime(CalendarCode.ToString(), DayNo.ToString(), 2);
                                    break;
                                }
                        }

                        DayTime += Conversions.ToDouble(Module1.GetFloatingHour(DayAccessibleTime));
                        if (TimeSpan.Parse(CalendarStart) > TimeSpan.Parse(StartHour))
                        {
                            StartHour = CalendarStart;
                        }
                        else if (TimeSpan.Parse(CalendarStart) < TimeSpan.Parse(StartHour))
                        {
                            DayTime -= Conversions.ToDouble(Module1.GetFloatingHour(StartHour)) - Conversions.ToDouble(Module1.GetFloatingHour(CalendarStart));
                            StartHour = CalendarStart;
                        }
                    }

                    StartDate = FarsiDateFunctions.AddToDate(StartDate, "00000001");
                    if (Operators.CompareString(StartDate, EndDate, false) > 0)
                    {
                        break;
                    }
                }

                if (TimeSpan.Parse(CalendarEnd) > TimeSpan.Parse(EndHour))
                {
                    EndHour = (TimeSpan.Parse(CalendarEnd) - TimeSpan.Parse(EndHour)).ToString();
                    DayTime -= Conversions.ToDouble(Module1.GetFloatingHour(EndHour.Substring(0, 5)));
                }
            }
            else if (chkFullDayBase.Checked)
            {
                while (true)
                {
                    DayTime += 24.0d - Conversions.ToDouble(Module1.GetFloatingHour(txtDownTime.Text));
                    if (TimeSpan.Parse("00:00") < TimeSpan.Parse(StartHour))
                    {
                        DayTime -= Conversions.ToDouble(Module1.GetFloatingHour(StartHour));
                        StartHour = "00:00";
                    }

                    StartDate = FarsiDateFunctions.AddToDate(StartDate, "00000001");
                    if (Operators.CompareString(StartDate, EndDate, false) > 0)
                    {
                        break;
                    }
                }

                if (TimeSpan.Parse("00:00") != TimeSpan.Parse(EndHour))
                {
                    EndHour = (TimeSpan.Parse("23:59") - TimeSpan.Parse(EndHour) + TimeSpan.Parse("00:01")).ToString();
                    DayTime -= Conversions.ToDouble(Module1.GetFloatingHour(EndHour.Substring(0, 5)));
                }
            }

            return DayTime;
        }

        private double GetMachineSetupTime(string MachineCode, string StartDate, string StartHour, string EndDate, string EndHour)
        {
            double MachineSetupTime;
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmSetupTime = new SqlCommand("Select IsNull(Sum(SetupDuration),0) From " + "(Select A.PlanningCode, Convert(Float,B.SetupDuration) As SetupDuration " + "From   Tbl_RealProduction A Inner Join Tbl_Planning B ON A.PlanningCode = B.PlanningCode " + "Where  A.MachineCode = '" + MachineCode + "' And Convert(Float,A.StartDate + dbo.GetFloatingTimeFromRegularTime(A.StartHour)) >= Convert(Float,'" + StartDate + "' + dbo.GetFloatingTimeFromRegularTime('" + StartHour + "')) And " + "Convert(Float,A.StartDate + dbo.GetFloatingTimeFromRegularTime(A.StartHour)) <= Convert(Float,'" + EndDate + "' + dbo.GetFloatingTimeFromRegularTime('" + EndHour + "')) " + "Union " + "Select A.PlanningCode, Convert(Float,B.SetupDuration) As SetupDuration " + "From   Tbl_RealProduction A Inner Join Tbl_Planning B ON A.PlanningCode = B.PlanningCode " + "Where  A.MachineCode = '" + MachineCode + "' And Convert(Float,A.EndDate + dbo.GetFloatingTimeFromRegularTime(A.EndHour)) >= Convert(Float,'" + StartDate + "' + dbo.GetFloatingTimeFromRegularTime('" + StartHour + "')) And " + "Convert(Float,A.EndDate + dbo.GetFloatingTimeFromRegularTime(A.EndHour)) <= Convert(Float,'" + EndDate + "' + dbo.GetFloatingTimeFromRegularTime('" + EndHour + "'))) A", cn);

                MachineSetupTime = Conversions.ToDouble(cmSetupTime.ExecuteScalar().ToString());
                cn.Close();
            }

            return MachineSetupTime;
        }

        private double GetMachineHaltTime(string MachineCode, string StartDate, string StartHour, string EndDate, string EndHour)
        {
            double MachineHaltTime;
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmHaltTime = new SqlCommand("Select IsNull(Sum(HaltDuration),0) From " + "(Select B.ProductionCode, Convert(Float,dbo.GetFloatingTimeFromRegularTime(A.Duration)) As HaltDuration " + "From   Tbl_ProductionHalts A Inner Join Tbl_RealProduction B ON A.ProductionCode = B.ProductionCode " + "Where  B.MachineCode = '" + MachineCode + "' And Convert(Float,A.StartDate + dbo.GetFloatingTimeFromRegularTime(A.StartHour)) >= Convert(Float,'" + StartDate + "' + dbo.GetFloatingTimeFromRegularTime('" + StartHour + "')) And " + "Convert(Float,A.StartDate + dbo.GetFloatingTimeFromRegularTime(A.StartHour)) <= Convert(Float,'" + EndDate + "' + dbo.GetFloatingTimeFromRegularTime('" + EndHour + "')) " + "Union " + "Select B.ProductionCode, Convert(Float,dbo.GetFloatingTimeFromRegularTime(A.Duration)) As HaltDuration " + "From   Tbl_ProductionHalts A Inner Join Tbl_RealProduction B ON A.ProductionCode = B.ProductionCode " + "Where  B.MachineCode = '" + MachineCode + "' And Convert(Float,A.EndDate + dbo.GetFloatingTimeFromRegularTime(A.EndHour)) >= Convert(Float,'" + StartDate + "' + dbo.GetFloatingTimeFromRegularTime('" + StartHour + "')) And " + "Convert(Float,A.EndDate + dbo.GetFloatingTimeFromRegularTime(A.EndHour)) <= Convert(Float,'" + EndDate + "' + dbo.GetFloatingTimeFromRegularTime('" + EndHour + "'))) A", cn);


                MachineHaltTime = Conversions.ToDouble(cmHaltTime.ExecuteScalar().ToString());
                cn.Close();
            }

            return MachineHaltTime;
        }

        private double GetMachinePMTime(string MachineCode, string StartDate, string StartHour, string EndDate, string EndHour)
        {
            var MachinePMTime = default(double);
            int CalendarCode;
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmPMTime = new SqlCommand("Select IsNull(CalendarCode,0) From Tbl_Machines Where Code = '" + MachineCode + "'", cn);
                CalendarCode = Conversions.ToInteger(cmPMTime.ExecuteScalar().ToString());
                cmPMTime.CommandText = "Select * From " + "(Select * From Tbl_MachineNotAvailableTimes " + "Where  MachineCode = '" + MachineCode + "' And Convert(Float,StartDate + dbo.GetFloatingTimeFromRegularTime(StartHour)) >= Convert(Float,'" + StartDate + "' + dbo.GetFloatingTimeFromRegularTime('" + StartHour + "')) And " + "Convert(Float,StartDate + dbo.GetFloatingTimeFromRegularTime(StartHour)) <= Convert(Float,'" + EndDate + "' + dbo.GetFloatingTimeFromRegularTime('" + EndHour + "')) " + "Union " + "Select * From Tbl_MachineNotAvailableTimes " + "Where  MachineCode = '" + MachineCode + "' And Convert(Float,EndDate + dbo.GetFloatingTimeFromRegularTime(EndHour)) >= Convert(Float,'" + StartDate + "' + dbo.GetFloatingTimeFromRegularTime('" + StartHour + "')) And " + "Convert(Float,EndDate + dbo.GetFloatingTimeFromRegularTime(EndHour)) <= Convert(Float,'" + EndDate + "' + dbo.GetFloatingTimeFromRegularTime('" + EndHour + "'))) A";






                var drPMTime = cmPMTime.ExecuteReader();
                while (drPMTime.Read())
                    MachinePMTime += GetDaysDurationTime(Conversions.ToString(drPMTime["StartDate"]), Conversions.ToString(drPMTime["StartHour"]), Conversions.ToString(drPMTime["EndDate"]), Conversions.ToString(drPMTime["EndHour"]), CalendarCode);
                drPMTime.Close();
                cn.Close();
            }

            return MachinePMTime;
        }

        private double GetMachinePerformance(string MachineCode, string StartDate, string StartHour, string EndDate, string EndHour, ArrayList arPerformance)
        {
            double MachinePerformance;
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmPerformance = new SqlCommand("Select IsNull(Sum(RealProductionQuantity),0) As RealProductionQuantity, IsNull(Sum(StandardProductionQuantity),0) As StandardProductionQuantity From " + "(Select A.ProductionCode, (A.IntactQuantity + A.GarbageQuantity) As RealProductionQuantity," + "       Round((A.ProductionDuration/(dbo.GetAnyTime_TO_Hour(B.TimeType,B.OneExecutionTime,dbo.GetCalendarAccessibleTime(B.CalendarCode)))),0)As StandardProductionQuantity " + "From   dbo.Tbl_RealProduction A INNER JOIN " + "       dbo.Tbl_ProductOPCsExecutorMachines B ON A.TreeCode = B.TreeCode AND A.OperationCode = B.OperationCode AND A.MachineCode = B.MachineCode " + "Where  B.MachineCode = '" + MachineCode + "' And Convert(Float,A.StartDate + dbo.GetFloatingTimeFromRegularTime(A.StartHour)) >= Convert(Float,'" + StartDate + "' + dbo.GetFloatingTimeFromRegularTime('" + StartHour + "')) And " + "                                  Convert(Float,A.StartDate + dbo.GetFloatingTimeFromRegularTime(A.StartHour)) <= Convert(Float,'" + EndDate + "' + dbo.GetFloatingTimeFromRegularTime('" + EndHour + "')) " + "Union " + "Select A.ProductionCode, (A.IntactQuantity + A.GarbageQuantity) As RealProductionQuantity," + "       Round((A.ProductionDuration/(dbo.GetAnyTime_TO_Hour(B.TimeType,B.OneExecutionTime,dbo.GetCalendarAccessibleTime(B.CalendarCode)))),0)As StandardProductionQuantity " + "From   dbo.Tbl_RealProduction A INNER JOIN " + "       dbo.Tbl_ProductOPCsExecutorMachines B ON A.TreeCode = B.TreeCode AND A.OperationCode = B.OperationCode AND A.MachineCode = B.MachineCode " + "Where  B.MachineCode = '" + MachineCode + "' And Convert(Float,A.EndDate + dbo.GetFloatingTimeFromRegularTime(A.EndHour)) >= Convert(Float,'" + StartDate + "' + dbo.GetFloatingTimeFromRegularTime('" + StartHour + "')) And " + "                                  Convert(Float,A.EndDate + dbo.GetFloatingTimeFromRegularTime(A.EndHour)) <= Convert(Float,'" + EndDate + "' + dbo.GetFloatingTimeFromRegularTime('" + EndHour + "'))) A", cn);












                var drPerformance = cmPerformance.ExecuteReader();
                if (drPerformance.Read())
                {
                    arPerformance.Add(drPerformance["RealProductionQuantity"]);
                    arPerformance.Add(drPerformance["StandardProductionQuantity"]);
                }
                else
                {
                    arPerformance.Add("0");
                    arPerformance.Add("0");
                }

                drPerformance.Close();
                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(arPerformance[1], 0, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(arPerformance[2], 0, false)))
                {
                    MachinePerformance = 0.0d;
                }
                else
                {
                    MachinePerformance = Conversions.ToDouble(Operators.DivideObject(arPerformance[1], arPerformance[2]));
                }

                cn.Close();
            }

            return MachinePerformance;
        }

        private double GetMachineQuality(string MachineCode, string StartDate, string StartHour, string EndDate, string EndHour, ArrayList arQuality)
        {
            double MachineQuality;
            using (var cn = new SqlConnection(Module1.PlanningCnnStr))
            {
                cn.Open();
                var cmQuality = new SqlCommand("Select IsNull(Sum(Convert(Float,IntactQuantity)),0) As IntactQuantity, IsNull(Sum(Convert(Float,RealProductionQuantity)),0) As TotalProductionQuantity From " + "(Select ProductionCode, IntactQuantity, (IntactQuantity + GarbageQuantity) As RealProductionQuantity " + "From   dbo.Tbl_RealProduction " + "Where  MachineCode = '" + MachineCode + "' And Convert(Float,StartDate + dbo.GetFloatingTimeFromRegularTime(StartHour)) >= Convert(Float,'" + StartDate + "' + dbo.GetFloatingTimeFromRegularTime('" + StartHour + "')) And " + "                                  Convert(Float,StartDate + dbo.GetFloatingTimeFromRegularTime(StartHour)) <= Convert(Float,'" + EndDate + "' + dbo.GetFloatingTimeFromRegularTime('" + EndHour + "')) " + "Union " + "Select ProductionCode, IntactQuantity, (IntactQuantity+ GarbageQuantity) As RealProductionQuantity " + "From   dbo.Tbl_RealProduction " + "Where  MachineCode = '" + MachineCode + "' And Convert(Float,EndDate + dbo.GetFloatingTimeFromRegularTime(EndHour)) >= Convert(Float,'" + StartDate + "' + dbo.GetFloatingTimeFromRegularTime('" + StartHour + "')) And " + "                                  Convert(Float,EndDate + dbo.GetFloatingTimeFromRegularTime(EndHour)) <= Convert(Float,'" + EndDate + "' + dbo.GetFloatingTimeFromRegularTime('" + EndHour + "'))) A", cn);








                var drQuality = cmQuality.ExecuteReader();
                if (drQuality.Read())
                {
                    arQuality.Add(drQuality["IntactQuantity"]);
                    arQuality.Add(drQuality["TotalProductionQuantity"]);
                }
                else
                {
                    arQuality.Add("0");
                    arQuality.Add("0");
                }

                drQuality.Close();
                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(arQuality[1], 0, false)) || Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(arQuality[2], 0, false)))
                {
                    MachineQuality = 0.0d;
                }
                else
                {
                    MachineQuality = Conversions.ToDouble(Operators.DivideObject(arQuality[1], arQuality[2]));
                }

                cn.Close();
            }

            return MachineQuality;
        }

        private bool FormValidation()
        {
            if (lstMachines.CheckedItems.Count == 0)
            {
                MessageBox.Show("هیچ ماشینی انتخاب نشده است", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                lstMachines.Focus();
                return false;
            }

            if (txtFromDate.Text is null || string.IsNullOrEmpty(txtFromDate.Text) || txtFromDate.Text.Equals("") || txtFromDate.Text.Trim().Length < 8)
            {
                MessageBox.Show("(محدودۀ تاریخ محاسبه صحیح نیست(از تاریخ", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtFromDate.Focus();
                return false;
            }

            if (txtToDate.Text is null || string.IsNullOrEmpty(txtToDate.Text) || txtToDate.Text.Equals("") || txtToDate.Text.Trim().Length < 8)
            {
                MessageBox.Show("(محدودۀ تاریخ محاسبه صحیح نیست(تا تاریخ", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtToDate.Focus();
                return false;
            }

            if (txtDescription.Text is null || string.IsNullOrEmpty(txtDescription.Text.Trim()) || txtDescription.Text.Trim().Equals(""))
            {
                txtDescription.Text = "-";
            }

            return true;
        }

        private void frmOEE_Resize(object sender, EventArgs e)
        {
            lblAllOEE.Width = (int)Math.Round(dgOEEList.Width * 0.2d - 15d);
            lblAllQuality.Width = (int)Math.Round(dgOEEList.Width * 0.2d - 15d);
            lblAllPerformance.Width = (int)Math.Round(dgOEEList.Width * 0.2d - 15d);
            lblAllAvailability.Width = (int)Math.Round(dgOEEList.Width * 0.2d - 15d);
            lblAllQuality.Left = lblAllOEE.Width + 25;
            lblAllPerformance.Left = lblAllQuality.Left + lblAllQuality.Width + 13;
            lblAllAvailability.Left = lblAllPerformance.Left + lblAllPerformance.Width + 13;
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            if (cmdSave.Tag is null || string.IsNullOrEmpty(cmdSave.Tag.ToString()) || cmdSave.Tag.ToString().Equals(""))
            {
                MessageBox.Show(".ابتدا  باید محاسبۀ انجام شده ثبت گردد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                var tpPrintPreview = new TabPage("پیش نمایش چاپ");
                tpPrintPreview.Name = "tpPrintPreview";
                if (TabControl1.TabPages.Count < 3 || !TabControl1.TabPages[2].Name.Equals("tpPrintPreview"))
                {
                    var cmdClosePreview = new System.Windows.Forms.Button();
                    cmdClosePreview.Text = "بستن";
                    cmdClosePreview.TextAlign = ContentAlignment.MiddleCenter;
                    cmdClosePreview.Font = new System.Drawing.Font("Tahoma", 10f, FontStyle.Bold);
                    cmdClosePreview.Size = new Size(100, 30);
                    cmdClosePreview.Location = new Point(20, 10);
                    cmdClosePreview.Click += cmdClosePreview_Click;
                    tpPrintPreview.Controls.Add(cmdClosePreview);

                    // Dim cmdPrintReport As New Button()
                    // cmdPrintReport.Text = "چاپ"
                    // cmdPrintReport.TextAlign = ContentAlignment.MiddleCenter
                    // cmdPrintReport.Font = New Drawing.Font("Tahoma", 10, FontStyle.Bold)
                    // cmdPrintReport.Size = New Size(100, 30)
                    // cmdPrintReport.Location = New Point(140, 10)
                    // AddHandler cmdPrintReport.Click, AddressOf cmdPrintReport_Click
                    // tpPrintPreview.Controls.Add(cmdPrintReport)

                    var ReportViewer = new CrystalReportViewer();
                    ReportViewer.Size = new Size(tpPrintPreview.Width - 5, tpPrintPreview.Height - 55);
                    ReportViewer.Location = new Point(5, 50);
                    ReportViewer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
                    ReportViewer.DisplayGroupTree = false;
                    ReportViewer.RightToLeft = RightToLeft.No;
                    if (!LoadReport(ReportViewer, cmdSave.Tag.ToString()))
                    {
                        return;
                    }

                    tpPrintPreview.Controls.Add(ReportViewer);
                    TabControl1.TabPages.Add(tpPrintPreview);
                    TabControl1.SelectTab(tpPrintPreview);
                }
                else
                {
                    LoadReport((CrystalReportViewer)TabControl1.TabPages[2].Controls[1], cmdSave.Tag.ToString());
                }

                Cursor = Cursors.Default;
            }
            catch (Exception objEx)
            {
                Cursor = Cursors.Default;
                Logger.SaveError(Name + ".cmdPrint_Click", objEx.Message);
                MessageBox.Show("نمایش نسخۀ چاپی با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }

        private void cmdClosePreview_Click(object sender, EventArgs e)
        {
            TabControl1.TabPages["tpPrintPreview"].Dispose();
        }

        private bool LoadReport(CrystalReportViewer ReportViewer, string OEEIndex)
        {
            var cn = new SqlConnection(Module1.PlanningCnnStr);
            var CnnBuilder = new SqlConnectionStringBuilder(Module1.PlanningCnnStr);
            var CnnInfo = new ConnectionInfo();
            var RptDoc = new ReportDocument();
            CnnInfo.ServerName = CnnBuilder.DataSource;
            CnnInfo.DatabaseName = cn.Database; // CnnBuilder. "PSB_ProductionPlanning";
            CnnInfo.IntegratedSecurity = CnnBuilder.IntegratedSecurity;
            if (!CnnBuilder.IntegratedSecurity)
            {
                CnnInfo.UserID = CnnBuilder.UserID;
                CnnInfo.Password = CnnBuilder.Password;
            }

            if (File.Exists(Module1.GetReportFolderPath() + "Rep102_OEEList.rpt"))
            {
                RptDoc.Load(Module1.GetReportFolderPath() + "Rep102_OEEList.rpt", OpenReportMethod.OpenReportByDefault);
                RptDoc.ParameterFields["OEEIndex"].CurrentValues.AddValue(OEEIndex);
                foreach (Table tb in RptDoc.Database.Tables)
                {
                    tb.LogOnInfo.ConnectionInfo = CnnInfo;
                    tb.ApplyLogOnInfo(tb.LogOnInfo);
                }

                ReportViewer.ReportSource = null;
                ReportViewer.ReportSource = RptDoc;
            }
            else
            {
                Cursor = Cursors.Default;
                MessageBox.Show("فایل گزارش در مسیر اجرایی نرم افزار یافت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                return false;
            }

            return true;
        }
    }
}