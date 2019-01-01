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
using System.Text.RegularExpressions;

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
        private Int32 m_newWordGroup = 1;

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

            getWeightTable();

            if (!m_isEnglishLike)
            {
                newWordText.Font = new Font("楷体", 32f);
            }
            m_newWordIndex = 0;
            displayNewWord();
        }

        private void getWeightTable()
        {
            m_newWords.Clear();
            m_newWordsSorted.Clear();

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
        }

        private void MemoryCard_FormClosed(object sender, FormClosedEventArgs e)
        {
            // save the updated xml back to file
            m_doc.Save(m_pathNewWords, Encoding.GetEncoding("utf-8"));
            callerForm.Show();
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            updateFamiliarity(newWordText.Text, 1);
            m_newWordIndex++;
            displayNewWord();
        }

        private void buttonVeryWell_Click(object sender, EventArgs e)
        {
            updateFamiliarity(newWordText.Text, 3);
            m_newWordIndex++;
            displayNewWord();
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            updateFamiliarity(newWordText.Text, -1);
            m_newWordIndex++;
            displayNewWord();
        }

        private void updateFamiliarity(string newWord, int delta)
        {
            string xPath = "//tr[td=\"" + newWord + "\"]";
            HtmlNodeCollection tmp = m_doc.DocumentNode.SelectNodes(xPath);

            Match match = Regex.Match(tmp[0].InnerHtml, @"\d+", RegexOptions.RightToLeft);

            if (!match.Success)
            {
                Console.WriteLine("updateFamiliarity(): No match found. Check it");
                return;
            }

            int newFamiliarity = Convert.ToInt16(match.Value) + delta;

            if (newFamiliarity < 1)
            {
                newFamiliarity = 1;
            }

            tmp[0].InnerHtml = ReplaceLastOccurrence(tmp[0].InnerHtml, match.Value, newFamiliarity.ToString());
        }

        private string ReplaceLastOccurrence(string Source, string Find, string Replace)
        {
            int place = Source.LastIndexOf(Find);

            if (place == -1)
                return Source;

            string result = Source.Remove(place, Find.Length).Insert(place, Replace);
            return result;
        }

        private void displayNewWord()
        {
            bool isFinished = false;
            int finishReason = 0;
            string finishReasonStr = "你已经完成";
            if (m_newWordIndex >= m_newWordsSorted.Rows.Count)
            {
                isFinished = true;
                finishReason = 1;
                finishReasonStr += "全部新字。\n";
            }
            else if (m_newWordIndex >= 12 * m_newWordGroup)
            {
                isFinished = true;
                finishReason = 2;
                finishReasonStr += "一组十二个新字。\n";
            }
            if (isFinished)
            {
                finishReasonStr += "还要再练习吗？";
                DialogResult result = MessageBox.Show(null,
                                                     finishReasonStr,
                                                     "Info",
                                                     MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (finishReason == 1)
                    {
                        getWeightTable();
                        m_newWordIndex = 0;
                        m_newWordGroup = 1;
                    }
                    else if (finishReason == 2)
                    {
                        m_newWordGroup++;
                    }
                    finishReason = 0;
                }
                else
                {
                    this.Close();
                }
            }

            newWordText.Text = m_newWordsSorted.Rows[m_newWordIndex].Field<string>("NewWords");
            progressBarFamiliarity.Value = Convert.ToInt32(m_newWordsSorted.Rows[m_newWordIndex].Field<string>("Familiarity"));
        }
    }
}
