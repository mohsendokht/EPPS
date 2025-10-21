using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductionPlanning.ToolForms
{
    public partial class ErrorLogForm : Form
    {
        string logFolder = Application.StartupPath + "\\ErrorLog\\";
        public ErrorLogForm()
        {
            InitializeComponent();
        }

        private void ErrorLogForm_Load(object sender, EventArgs e)
        {
            
            var start = logFolder.Length;

            String[] files = System.IO.Directory.GetFiles(logFolder);
            
            for (int i = files.Length-1; i >= 0 ; i--)
            {
                FilelistBox.Items.Add(files[i].Substring(start).Trim());
            }
        }
               

        private void FilelistBox_DoubleClick(object sender, EventArgs e)
        {
            LoadFileContent(FilelistBox.SelectedIndex);
        }

       private void LoadFileContent(int index)
        {
            var file = "ErrorLog\\" + FilelistBox.Items[index].ToString();
            ErrorTextBox.Text = System.IO.File.ReadAllText(file);
        }
    }
}
