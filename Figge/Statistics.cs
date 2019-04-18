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
    public partial class Statistics : Form
    {
        public MainThread callerForm;
        private string m_pathNewWords;
        private string m_pathNewWordWeek;
        private string[] m_records;
        private bool m_isEnglishLike;

        public Statistics(string pathNewWord,
            string[] records,
            bool isEnglishLike)
        {
            InitializeComponent();
        }

        private void Statistics_FormClosed(object sender, FormClosedEventArgs e)
        {
            callerForm.Show();
        }

        private void Statistics_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Gray, 2);
            g.DrawLine(p, 50,  20,  50, 120); // length 100
            g.DrawLine(p, 50, 120, 650, 120); // length 600

            g.DrawLine(p, 50, 160,  50, 260); // length 100
            g.DrawLine(p, 50, 260, 650, 260); // length 600

            g.DrawLine(p, 50, 300,  50, 400); // length 100
            g.DrawLine(p, 50, 400, 650, 400); // length 600

            // read the new words table, calculate them
        }
    }
}
