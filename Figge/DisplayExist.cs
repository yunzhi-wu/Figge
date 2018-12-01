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
    public partial class DisplayExist : Form
    {
        public MainThread callerForm;

        private FileStream m_fs;

        public DisplayExist(string path)
        {
            InitializeComponent();
            m_fs = new FileStream(path, FileMode.Open);
            webDisplay.DocumentStream = m_fs;
        }

        private void DisplayExist_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_fs != null)
            {
                m_fs.Close();
            }
            webDisplay.DocumentStream = null;
            callerForm.Show();
        }

        private void webDisplay_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}
