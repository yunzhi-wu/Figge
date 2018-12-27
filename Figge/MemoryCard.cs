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
    public partial class MemoryCard : Form
    {
        public MainThread callerForm;
        private string m_pathNewWords;
        private string[] m_records;
        private bool m_isEnglishLike;

        public MemoryCard(string pathNewWord,
            string[] records,
            bool isEnglishLike)
        {
            InitializeComponent();

            m_pathNewWords = string.Copy(pathNewWord);
            m_records = records;
            m_isEnglishLike = isEnglishLike;
        }

        private void MemoryCard_FormClosed(object sender, FormClosedEventArgs e)
        {
            callerForm.Show();
        }
    }
}
