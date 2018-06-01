using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Figge
{
    public partial class display_data : Form
    {
        public Form callerForm;

        public display_data()
        {
            InitializeComponent();
        }

        private void wordsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.wordsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.repositoryDataSet);

        }

        private void display_data_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'repositoryDataSet.Words' table. You can move, or remove it, as needed.
            this.wordsTableAdapter.Fill(this.repositoryDataSet.Words);

        }

        private void display_data_FormClosing(object sender, FormClosingEventArgs e)
        {
            callerForm.Show();
        }

    }
}
