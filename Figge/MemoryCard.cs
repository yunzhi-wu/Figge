using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using HtmlAgilityPack;

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


            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.Load(m_pathNewWords, Encoding.GetEncoding("utf-8"));
            string newFileName = m_pathNewWords + "01.html";
            HtmlNodeCollection tmp = doc.DocumentNode.SelectNodes("//tr[td=\"öka\"]");
            tmp[0].InnerHtml = "\r\n    <td>19/12/2018</td>\r\n    <td>öka</td>\r\n    <td>1</td>\r\n    <td>3</td>\r\n  ";



            doc.Save(newFileName, Encoding.GetEncoding("utf-8"));
            
            var headers = doc.DocumentNode.SelectNodes("//tr/th");
            DataTable table = new DataTable();
            foreach (HtmlNode header in headers)
                table.Columns.Add(header.InnerText); // create columns from th
                                                     // select rows with td elements 
            foreach (var row in doc.DocumentNode.SelectNodes("//tr[td]"))
                table.Rows.Add(row.SelectNodes("td").Select(td => td.InnerText).ToArray());

            Console.WriteLine("htmlAgilityPack read file " + m_pathNewWords);


        }

        private void MemoryCard_FormClosed(object sender, FormClosedEventArgs e)
        {
            callerForm.Show();
        }
    }
}
