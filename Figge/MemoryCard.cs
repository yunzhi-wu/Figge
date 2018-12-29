using System;
using System.Collections;
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

        private HtmlAgilityPack.HtmlDocument m_doc;

        private DataTable m_newWords;
        private DataTable m_newWordsSorted;

        private Int32 m_newWordIndex;

        public MemoryCard(string pathNewWord,
            string[] records,
            bool isEnglishLike)
        {
            InitializeComponent();

            m_pathNewWords = string.Copy(pathNewWord);
            m_records = records;
            m_isEnglishLike = isEnglishLike;

            m_newWords = new DataTable();
            m_newWordsSorted = new DataTable();

            m_doc = new HtmlAgilityPack.HtmlDocument();
            m_doc.Load(m_pathNewWords, Encoding.GetEncoding("utf-8"));

            var headers = m_doc.DocumentNode.SelectNodes("//tr/th");

            foreach (HtmlNode header in headers)
            {
                m_newWords.Columns.Add(header.InnerText); // create columns from th
                                                     // select rows with td elements 
            }
            foreach (var row in m_doc.DocumentNode.SelectNodes("//tr[td]"))
            {
                m_newWords.Rows.Add(row.SelectNodes("td").Select(td => td.InnerText).ToArray());
            }

            string expression;
            // expression = "NewWords = 'is'";
            // expression = "TimesAdded > 1";
            // expression = "Date > #12/27/2018#"; // month first
            expression = "Familiarity < 10";

            var filteredDataRows = m_newWords.Select(expression);

            if (filteredDataRows.Length == 0)
            {
                // todo: create list in another way
                return;
            }

            DateTime today = DateTime.Now;

            // a temporay table for weight
            DataTable filteredDataTable = new DataTable();
            filteredDataTable = filteredDataRows.CopyToDataTable();
            filteredDataTable.Columns.Add("weight", typeof(Int16));

            foreach (DataRow row in filteredDataTable.Rows)
            {
                // weight for each column:
                // the older, the bigger: every three days a review should be done (wDay = days / 3)
                // the more times added, the bigger: wTimes = (TimesAdded - 1) * 2
                // the less familiar, the bigger: wF = (10 - famility)
                Int16 nrOfDays = (Int16)Math.Round((today - Convert.ToDateTime(row["Date"])).TotalDays, 0, MidpointRounding.AwayFromZero);
                row["weight"] = nrOfDays / 3 + 2 * (Convert.ToInt16(row["timesAdded"]) - 1) + (10 - Convert.ToInt16(row["Familiarity"]));

                Console.WriteLine("{0}\t\t: days {1}\t, timesAdded {2}, Familiarity {3}: weight {4}", 
                    row["NewWords"],
                    nrOfDays,
                    row["timesAdded"],
                    row["Familiarity"],
                    row["weight"]);
            }

            // sort by the weight

            DataView dv = filteredDataTable.DefaultView;
            dv.Sort = "weight desc";
            m_newWordsSorted = dv.ToTable();

            m_newWordIndex = 0;
            newWordText.Text = m_newWordsSorted.Rows[m_newWordIndex].Field<string>("NewWords");
        }

        private void MemoryCard_FormClosed(object sender, FormClosedEventArgs e)
        {
            callerForm.Show();
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            string xPath = "//tr[td=\"" + newWordText.Text + "\"]";
            HtmlNodeCollection tmp = m_doc.DocumentNode.SelectNodes(xPath);
            // tmp[0].InnerHtml = "\r\n    <td>19/12/2018</td>\r\n    <td>öka</td>\r\n    <td>1</td>\r\n    <td>3</td>\r\n  ";
        }

        private void buttonVeryWell_Click(object sender, EventArgs e)
        {

        }
    }
}
