using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProductionPlanning
{
    public partial class frmReplacementMachines
    {
        public frmReplacementMachines()
        {
            InitializeComponent();
            _cmdSave.Name = "cmdSave";
            _cmdExit.Name = "cmdExit";
            _cmdDelete.Name = "cmdDelete";
        }

        private short mFormMode;
        private string mParentMachineCode = "-1";
        private string mReplacementMachineCode = "-1";
        private string mDescription = "-1";
        private string mConditions = "-1";
        //private short I;

        public short FormMode
        {
            set
            {
                mFormMode = value;
            }
        }

        public string ParentMachineCode
        {
            set
            {
                mParentMachineCode = value;
            }
        }

        public string ReplacementMachineCode
        {
            set
            {
                mReplacementMachineCode = value;
            }
        }

        public string Description
        {
            set
            {
                mDescription = value;
            }
        }

        public string Conditions
        {
            set
            {
                mConditions = value;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Module1.SetButtonsImage(cmdSave, 6);
            Module1.SetButtonsImage(cmdDelete, 9);
            Module1.SetButtonsImage(cmdExit, 14);
            FormLoad();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ماشین جایگزین را حذف می کنید", Module1.MessagesTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading, false) == DialogResult.Yes)
            {
                if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                    Module1.cnProductionPlanning.Open();
                var trnReplacementMachine = Module1.cnProductionPlanning.BeginTransaction();
                try
                {
                    {
                        var withBlock = new SqlCommand("Delete From Tbl_ReplacementMachines Where MachineCode = '" + mParentMachineCode + "' And ReplacementMachineCode = '" + mReplacementMachineCode + "'", Module1.cnProductionPlanning);
                        withBlock.Transaction = trnReplacementMachine;
                        withBlock.ExecuteNonQuery();
                    }

                    trnReplacementMachine.Commit();
                    Close();
                }
                catch (Exception objEx)
                {
                    trnReplacementMachine.Rollback();
                    Logger.SaveError(Name + ".cmdDelete_Click", objEx.Message);
                    MessageBox.Show("اشکال در حذف رکورد، رکورد حذف نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                }
                finally
                {
                    trnReplacementMachine.Dispose();
                    if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                        Module1.cnProductionPlanning.Close();
                }
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (cbReplacementMachine.SelectedIndex == -1)
            {
                MessageBox.Show("ماشین جایگزین را انتخاب کنید", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cbReplacementMachine.Focus();
                return;
            }

            if (Module1.cnProductionPlanning.State == ConnectionState.Closed)
                Module1.cnProductionPlanning.Open();
            var trnReplacementMachine = Module1.cnProductionPlanning.BeginTransaction();
            switch (mFormMode)
            {
                case (short)Module1.FormModeEnum.INSERT_MODE:
                    {
                        try
                        {
                            {
                                var withBlock = new SqlCommand("Insert Into Tbl_ReplacementMachines(MachineCode,ReplacementMachineCode,Description) Values('" + mParentMachineCode + "','" + cbReplacementMachine.SelectedValue.ToString() + "','" + txtDescription.Text + "')", Module1.cnProductionPlanning);
                                withBlock.Transaction = trnReplacementMachine;
                                withBlock.ExecuteNonQuery();
                            }

                            trnReplacementMachine.Commit();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            trnReplacementMachine.Rollback();
                            Logger.SaveError(Name + ".cmdSave_Click", objEx.Message);
                            MessageBox.Show("اشکال در ثبت رکورد جدید، رکورد جدید ثبت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                        }

                        break;
                    }

                case (short)Module1.FormModeEnum.EDIT_MODE:
                    {
                        try
                        {
                            {
                                var withBlock1 = new SqlCommand("Update Tbl_ReplacementMachines Set ReplacementMachineCode = '" + cbReplacementMachine.SelectedValue.ToString() + "',Description = '" + txtDescription.Text + "' Where MachineCode = '" + mParentMachineCode + "' And ReplacementMachineCode = '" + mReplacementMachineCode + "'", Module1.cnProductionPlanning);
                                withBlock1.Transaction = trnReplacementMachine;
                                withBlock1.ExecuteNonQuery();
                            }

                            trnReplacementMachine.Commit();
                            Close();
                        }
                        catch (Exception objEx)
                        {
                            trnReplacementMachine.Rollback();
                            Logger.SaveError(Name + ".cmdSave_Click", objEx.Message);
                            MessageBox.Show("اشکال در اصلاح رکورد، تغییرات ثبت نشد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            if (Module1.cnProductionPlanning.State == ConnectionState.Open)
                                Module1.cnProductionPlanning.Close();
                        }

                        break;
                    }
            }
        }

        private void FormLoad()
        {
            try
            {
                var dt = new DataTable();
                {
                    var withBlock = new SqlDataAdapter("Select * From Tbl_Machines " + mConditions, Module1.cnProductionPlanning);
                    withBlock.Fill(dt);
                }

                {
                    var withBlock1 = cbReplacementMachine;
                    withBlock1.DataSource = dt.DefaultView;
                    withBlock1.DisplayMember = "Name";
                    withBlock1.ValueMember = "Code";
                }

                switch (mFormMode)
                {
                    case (short)Module1.FormModeEnum.INSERT_MODE:
                        {
                            cbReplacementMachine.Focus();
                            break;
                        }

                    case (short)Module1.FormModeEnum.EDIT_MODE:
                    case (short)Module1.FormModeEnum.DELETE_MODE:
                        {
                            cbReplacementMachine.SelectedValue = mReplacementMachineCode;
                            txtDescription.Text = mDescription;
                            switch (mFormMode)
                            {
                                case (short)Module1.FormModeEnum.EDIT_MODE:
                                    {
                                        cbReplacementMachine.Focus();
                                        break;
                                    }

                                case (short)Module1.FormModeEnum.DELETE_MODE:
                                    {
                                        cmdDelete.Focus();
                                        break;
                                    }
                            }

                            break;
                        }
                }
            }
            catch (Exception objEx)
            {
                Logger.SaveError(Name + ".FormLoad", objEx.Message);
                MessageBox.Show("فراخوانی فرم با اشکال مواجه شد", Module1.MessagesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                Close();
            }
        }
    }
}