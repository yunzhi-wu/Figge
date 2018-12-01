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
using System.Text.RegularExpressions;

namespace Figge
{
    public partial class MainThread : Form
    {
        private string[] m_records;
        private string m_path = @"MyLearning.html";

        private Dictionary<char, int> m_histogram;

        public MainThread()
        {
            InitializeComponent();

            if (!File.Exists(m_path))
            {
                StreamWriter sw = File.CreateText(m_path);
                sw.Close();
            }
            m_records = File.ReadAllLines(m_path);
            
            if (m_records.Length == 0)
            {
                m_records = new string[21];
                int i = 0;
                m_records[i++] = "<!DOCTYPE html>";
                m_records[i++] = "<html>";
                m_records[i++] = "<head>";
                m_records[i++] = "  <meta http-equiv='x-ua-compatible' content='IE=edge,chrome=1'>";
                m_records[i++] = "  <meta charset=\"UTF-8\">";
                m_records[i++] = "<style>";
                m_records[i++] = "nw {";
                m_records[i++] = "    background-color: Orange;";
                m_records[i++] = "}";
                m_records[i++] = "np {";
                m_records[i++] = "    background-color: DodgerBlue;";
                m_records[i++] = "}";
                m_records[i++] = "</style>";
                m_records[i++] = "</head>";
                m_records[i++] = "<table style=\"width:100%\" border=1 frame=void rules=rows>";
                m_records[i++] = "  <tr>";
                m_records[i++] = "    <th>Time Created</th>";
                m_records[i++] = "    <th>Content</th>";
                m_records[i++] = "  </tr>";
                m_records[i++] = "</table>";
                m_records[i++] = "</html>";
                using (StreamWriter sw = new StreamWriter(m_path))
                {
                    for (i = 0; i < m_records.Length; i++)
                    {
                        sw.WriteLine(m_records[i]);
                    }
                }
            }

            m_histogram = new Dictionary<char, int>();
            updateHistogram();

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
            updateHistogram();
        }

        private void updateHistogram()
        {
            bool aRecordStarted = false;
            foreach (string line in m_records)
            {
                // it can be multiple lines for each record
                if (line.Contains("<td>") && !Regex.IsMatch(line, @"\d\d/\d\d/\d\d\d\d"))
                {
                    aRecordStarted = true;
                }
                if (aRecordStarted)
                {
                    foreach (char a in line)
                    {
                        if (m_histogram.ContainsKey(a))
                        {
                            m_histogram[a]++;
                        }
                        else
                        {
                            m_histogram.Add(a, 1);
                        }
                    }
                }
                if (line.Contains("</td>") && !Regex.IsMatch(line, @"\d\d/\d\d/\d\d\d\d"))
                {
                    aRecordStarted = false;
                }
            }
            UserStatus.Text = "你已经学了 " + m_histogram.Count + " 个汉字\n";
        }

        private void Review_Click(object sender, EventArgs e)
        {
            // display_data dis_form = new display_data();
            DisplayExist next_form = new DisplayExist(m_path);
            next_form.callerForm = this;
            this.Hide();
            next_form.ShowDialog();
        }
    }
}
