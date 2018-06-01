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
    public partial class MainThread : Form
    {
        public MainThread()
        {
            InitializeComponent();
        }

        private void MainThread_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void AddNewThings_Click(object sender, EventArgs e)
        {
            // display_data dis_form = new display_data();
            NewInput next_form = new NewInput();
            next_form.callerForm = this;
            this.Hide();
            next_form.ShowDialog();
        }
    }
}
