using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Figge
{
    public partial class MainThread : Form
    {
        private string[] m_records;
        private string m_path = @"MyLearning.html";

        public MainThread()
        {
            InitializeComponent();

            if (!File.Exists(m_path))
            {
                StreamWriter sw = File.CreateText(m_path);
                sw.Close();
            }
            this.m_records = File.ReadAllLines(m_path);
            
            if (m_records.Length == 0)
            {
                this.m_records = new string[6];

                /*
                <table style="width:100%">
                  <tr>
                    <th>Time Created</th>
                    <th>Content</th> 
                  </tr>
                </table>
                */
                this.m_records[0] = "<table style=\"width:100%\">";
                this.m_records[1] = "  <tr>";
                this.m_records[2] = "    <th>Time Created</th>";
                this.m_records[3] = "    <th>Content</th>";
                this.m_records[4] = "  </tr>";
                this.m_records[5] = "</table>";
                using (StreamWriter sw = new StreamWriter(m_path))
                {
                    for (int i = 0; i < m_records.Length; i++)
                    {
                        sw.WriteLine(m_records[i]);
                    }
                }
            }
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
            NewInput next_form = new NewInput(m_path, m_records);
            next_form.callerForm = this;
            this.Hide();
            next_form.ShowDialog();
        }

        public void UpdateInfo()
        {
            m_records = File.ReadAllLines(m_path);
        }
    }
}
