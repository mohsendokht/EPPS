using MyUserControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductionPlanning.Controls
{
    public partial class UserControl_LookUp : UserControl
    {
        private string _SerachFromTitle;

        public UserControl_LookUp()
        {
            InitializeComponent();
        }
               

        public bool CB_AutoComplete
        {
            get
            {
                return CB_MultiColumn.AutoComplete;
            }
            set
            {
                CB_MultiColumn.AutoComplete = value;
            }
        }

        public bool CB_AutoDropdown
        {
            get
            {
                return CB_MultiColumn.AutoDropdown;
            }
            set
            {
                CB_MultiColumn.AutoDropdown = value;
            }
        }

        public object CB_DataSource
        {
            get
            {
                return CB_MultiColumn.DataSource;
            }
            set
            {
                CB_MultiColumn.DataSource = value;
            }
        }

        public string CB_DisplayMember
        {
            get
            {
                return CB_MultiColumn.DisplayMember;
            }
            set
            {
                CB_MultiColumn.DisplayMember = value;
            }
        }

        public string CB_ValueMember
        {
            get
            {
                return CB_MultiColumn.ValueMember;
            }
            set
            {
                CB_MultiColumn.ValueMember = value;
            }
        }

        public int CB_SelectedIndex
        {
            get
            {
                return CB_MultiColumn.SelectedIndex;
            }
            set
            {
                CB_MultiColumn.SelectedIndex = value;
            }
        }

        public string CB_SelectedValue
        {
            get
            {
                return CB_MultiColumn.Text;
            }
            set
            {
                CB_MultiColumn.Text = value;
            }
        }

        public string CB_ColumnWidths 
        { 
            get { return CB_MultiColumn.ColumnWidths; } 
            set { CB_MultiColumn.ColumnWidths = value; } 
        }

        public int CB_ColumnWidthDefault
        {
            get => CB_MultiColumn.ColumnWidthDefault;
            set => CB_MultiColumn.ColumnWidthDefault = value;
        }

        public string CB_ColumnNames
        { 
            get => CB_MultiColumn.ColumnNames;
            set => CB_MultiColumn.ColumnNames = value;
        }

        public int CB_LinkedColumnIndex 
        { 
            get => CB_MultiColumn.LinkedColumnIndex; 
            set => CB_MultiColumn.LinkedColumnIndex = value;
        }

        public string CB_SelectedDescription
        {
            get => CB_DescriptionBox.Text;
            
        }

        public string CB_SerachFromTitle 
        { 
            get => _SerachFromTitle; 
            set => _SerachFromTitle = value; 
        }


        private void buttonSearch_Click(object sender, EventArgs e)
        {
           
            CB_MultiColumn_OpenSearchForm(CB_MultiColumn, e) ;
           
        }

        private void CB_MultiColumn_OpenSearchForm(object sender, EventArgs e)
        {
            FormSearch newForm = new FormSearch((MultiColumnComboBox)sender);
            if (_SerachFromTitle != "")
                newForm.Text = _SerachFromTitle;
            newForm.RightToLeft = RightToLeft.Yes;
            newForm.ShowDialog();
        }

        private void UserControl_LookUp_Resize(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                panel_DescriptionBox.Width = (int)(0.70 * this.Width);
                buttonSearch.Left = panel_DescriptionBox.Left + panel_DescriptionBox.Width + 5;
                panel_ComboBox.Width = this.Width - panel_DescriptionBox.Width - buttonSearch.Width - 15 ;
                panel_ComboBox.Left = buttonSearch.Left + buttonSearch.Width + 5;
                //buttonSearch.Top = panel_ComboBox.Top;
            }

        }

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler CB_SelectedValueChanged;
                
        private void CB_MultiColumn_SelectedValueChanged(object sender, EventArgs e)
        {
            //bubble the event up to the parent
            if (this.CB_SelectedValueChanged != null)
                this.CB_SelectedValueChanged(this, e);
        }
    }
}
