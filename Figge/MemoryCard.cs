﻿using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace Figge
{
    public partial class MemoryCard : Form
    {
        public const int MAX_NUMBER_OF_WORDS_TO_REMEMBER_PER_WEEK = 20;

        public MainThread callerForm;
        private string m_pathNewWords;
        private string m_pathNewWordWeek;
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
            m_pathNewWordWeek = m_pathNewWords.Insert(m_pathNewWords.Length - 5, "_Week");
            
            m_records = records;
            m_isEnglishLike = isEnglishLike;

            m_newWords = new DataTable();
            m_newWordsSorted = new DataTable();

            m_doc = new HtmlAgilityPack.HtmlDocument();
            m_doc.Load(m_pathNewWords, Encoding.GetEncoding("utf-8"));

            var headers = m_doc.DocumentNode.SelectNodes("//tr/th");

            foreach (HtmlNode header in headers)
            {
                /*
                if (header.InnerText.Contains("Date"))
                {
                    m_newWords.Columns.Add(new DataColumn(header.InnerText, typeof(DateTime)));
                }
                else */
                if (header.InnerText.Contains("Familiarity") ||
                    header.InnerText.Contains("TimesAdded"))
                {
                    m_newWords.Columns.Add(new DataColumn(header.InnerText, typeof(int)));
                }
                else
                {
                    m_newWords.Columns.Add(new DataColumn(header.InnerText, typeof(string)));
                }
            }

            getWeekTask(false);

            if (!m_isEnglishLike)
            {
                newWordText.Font = new Font("楷体", 32f);
            }
            m_newWordIndex = 0;
            displayNewWord();
        }

        private void getWeekTask(bool isRegenerate)
        {
            m_newWords.Rows.Clear();
            m_newWordsSorted.Rows.Clear();

            if ((!isRegenerate && !File.Exists(m_pathNewWordWeek)) // called from MemoryCard()
                || isRegenerate)                                   // called from buttonGetNewSet_Click()
            {
                foreach (var row in m_doc.DocumentNode.SelectNodes("//tr[td]"))
                {
                    var sn = row.SelectNodes("td");
                    m_newWords.Rows.Add(
                        sn[0].InnerText,
                        sn[1].InnerText,
                        sn[2].InnerText,
                        Convert.ToInt32(sn[3].InnerText),
                        Convert.ToInt32(sn[4].InnerText));
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
                    row["weight"] = nrOfDays / 4 + 2 * (row.Field<int>("timesAdded") - 1) + (10 - row.Field<int>("Familiarity"));

                    Console.WriteLine("{0}\t\t: days {1}\t, timesAdded {2}, Familiarity {3}: weight {4}",
                        row["NewWords"],
                        nrOfDays,
                        row.Field<int>("timesAdded"),
                        row.Field<int>("Familiarity"),
                        row["weight"]);
                }

                // sort by the weight

                DataView dv = filteredDataTable.DefaultView;
                dv.Sort = "weight desc";
                m_newWordsSorted = dv.ToTable();

                // export the first 20 rows to file
                string export = ConvertDataTableToHTML(m_newWordsSorted, MAX_NUMBER_OF_WORDS_TO_REMEMBER_PER_WEEK);
                using (StreamWriter sw = new StreamWriter(m_pathNewWordWeek, false, Encoding.GetEncoding("utf-8")))
                {
                    sw.Write(export);
                }

                GeneratePrintableWeek();
            }

            HtmlAgilityPack.HtmlDocument doc_week;
            doc_week = new HtmlAgilityPack.HtmlDocument();
            doc_week.Load(m_pathNewWordWeek, Encoding.GetEncoding("utf-8"));

            var headers = doc_week.DocumentNode.SelectNodes("//tr/th");
            DataTable newWordsWeek = new DataTable();
            foreach (HtmlNode header in headers)
            {
                newWordsWeek.Columns.Add(header.InnerText); // create columns from th
                                                          // select rows with td elements 
            }
            foreach (var row in doc_week.DocumentNode.SelectNodes("//tr[td]"))
            {
                newWordsWeek.Rows.Add(row.SelectNodes("td").Select(td => td.InnerText).ToArray());
            }
            m_newWordsSorted = newWordsWeek;
            m_newWordIndex = 0;
        }

        private void MemoryCard_FormClosed(object sender, FormClosedEventArgs e)
        {
            // save the updated xml back to file
            m_doc.Save(m_pathNewWords, Encoding.GetEncoding("utf-8"));
            // export the first 20 rows to file
            string export = ConvertDataTableToHTML(m_newWordsSorted, MAX_NUMBER_OF_WORDS_TO_REMEMBER_PER_WEEK);
            using (StreamWriter sw = new StreamWriter(m_pathNewWordWeek, false, Encoding.GetEncoding("utf-8")))
            {
                sw.Write(export);
            }
            callerForm.Show();
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            updateFamiliarity(newWordText.Text, 1);
            this.buttonYes.Enabled = false;
            System.Threading.Thread.Sleep(500);
            m_newWordIndex++;
            displayNewWord();
            this.buttonYes.Enabled = true;
        }

        private void buttonVeryWell_Click(object sender, EventArgs e)
        {
            updateFamiliarity(newWordText.Text, 3);
            this.buttonVeryWell.Enabled = false;
            System.Threading.Thread.Sleep(500);
            m_newWordIndex++;
            displayNewWord();
            this.buttonVeryWell.Enabled = true;
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            updateFamiliarity(newWordText.Text, -1);
            this.buttonNo.Enabled = false;
            System.Threading.Thread.Sleep(500);
            m_newWordIndex++;
            displayNewWord();
            this.buttonNo.Enabled = true;
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

            // todo, use the same way to update the new words table and new words for week table
            m_newWordsSorted.Rows[m_newWordIndex]["familiarity"] = newFamiliarity.ToString();

            // if the familiarity reaches 10, remember the OK date
            if (newFamiliarity >= 10 &&
                string.IsNullOrEmpty(m_newWordsSorted.Rows[m_newWordIndex]["OKDate"].ToString()))
            {
                m_newWordsSorted.Rows[m_newWordIndex]["OKDate"] = DateTime.Now.ToShortDateString();

                // update tmp[0], format is like:
                // "\r\n    <td>26/11/2018</td>\r\n    <td></td>\r\n    <td>算</td>\r\n    <td>1</td>\r\n    <td>2</td>\r\n  "
                MatchCollection match_td = Regex.Matches(tmp[0].InnerHtml, @"\</td\>");
                if (match_td.Count != 5)
                {
                    Console.WriteLine("updateFamiliarity() got different format");
                    return;
                }
                tmp[0].InnerHtml = tmp[0].InnerHtml.Insert(match_td[1].Index, DateTime.Now.ToShortDateString());
            }
        }

        private string ReplaceLastOccurrence(string Source, string Find, string Replace)
        {
            int place = Source.LastIndexOf(Find);

            if (place == -1)
                return Source;

            string result = Source.Remove(place, Find.Length).Insert(place, Replace);
            return result;
        }

        private int GetNextNewWord()
        {
            string finishReasonStr = "你已经完成";

            while (true)
            {
                bool isFinished = false;
                if (m_newWordIndex >= m_newWordsSorted.Rows.Count)
                {
                    isFinished = true;
                    finishReasonStr += "全部新字。\n";
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
                        m_newWordIndex = 0;
                        // shuffle these words
                    }
                    else
                    {
                        this.Close();
                        return -1;
                    }
                }
                int fami = Convert.ToInt32(m_newWordsSorted.Rows[m_newWordIndex].Field<string>("Familiarity"));
                if (fami <= 10)
                {
                    return m_newWordIndex;
                }
                else
                {
                    m_newWordIndex++;
                }
            }
        }

        private void displayNewWord()
        {
            int foundIndex = GetNextNewWord();
            if (foundIndex == -1)
            {
                return;
            }

            newWordText.Text = m_newWordsSorted.Rows[m_newWordIndex].Field<string>("NewWords");
            progressBarFamiliarity.Value = Math.Min(100, 10 * Convert.ToInt32(m_newWordsSorted.Rows[m_newWordIndex].Field<string>("Familiarity")));


            /*
             *  prepare the context string 
             */
            checkBoxContext.Checked = false;
            webBrowserContext.Visible = false;

            string cntxt = "<!DOCTYPE html>" +
                          "<html>" +
                          "<head>" +
                          "  <meta http-equiv='x-ua-compatible' content='IE=edge,chrome=1'>" +
                          "  <meta charset=\"UTF-8\">" +
                          "<style>" +
                          "nw {" +
                          "    background-color: Orange;" +
                          "}" +
                          "np {" +
                          "    background-color: DodgerBlue; " +
                          "}" +
                          "</style>" +
                          "</head>";  // remember to add  "</html>";

            cntxt += FindContext(newWordText.Text);
            cntxt += "</html>";

            webBrowserContext.DocumentText = cntxt;
        }

        private string FindContext(string word)
        {
            string cntxt = "";
            string wordWithNw = "<nw>" + word + "</nw>";
            string wordWithNp = "<np>" + word + "</np>";

            int foundCount = 0;
            foreach (string line in m_records)
            {
                int startIndex;
                string lineTmp;

                lineTmp = Regex.Replace(line, @"^ *\<td\>", "");
                lineTmp = Regex.Replace(lineTmp, @"\</td\>", "");
                startIndex = lineTmp.IndexOf(wordWithNw);
                if (startIndex < 0)
                {
                    startIndex = lineTmp.IndexOf(wordWithNp);
                }
                if (startIndex < 0)
                {
                    continue;
                }
                MatchCollection matches;
                if (m_isEnglishLike)
                {
                    matches = Regex.Matches(lineTmp, @"^|\.|$|：|:|\?|\!");
                }
                else
                {
                    matches = Regex.Matches(lineTmp, @"^|\.|。|$|：|:|？|！|('”')|('“')|(“)|(”)");
                }
                if (matches.Count < 2)
                {
                    Console.WriteLine("Match count less than 2. magic: MOFALEF");
                }

                for (int i = 0; i < matches.Count; i++)
                {
                    if (matches[i].Index <= startIndex &&
                        i + 1 < matches.Count &&
                        matches[i + 1].Index > startIndex)
                    {
                        int startPos = 0;
                        int length = 0;

                        if (matches[i].Index == 0)
                        {
                            startPos = 0;
                        }
                        else
                        {
                            // skip the previous separator
                            startPos = matches[i].Index + 1;
                        }

                        if (matches[i + 1].Index == lineTmp.Length)
                        {
                            // end with $
                            length = lineTmp.Length - startPos;
                        }
                        else
                        {
                            // can have the separator in this string
                            length = matches[i + 1].Index + 1 - startPos;
                        }
                        string aCntxt = lineTmp.Substring(startPos, length);
                        cntxt += aCntxt;
                        cntxt += "<br>";
                        Console.WriteLine("Found one context: " + aCntxt);
                        foundCount++;
                        continue;
                    }
                }
                if (foundCount >= 2) break;
            }
            return cntxt;
        }

        private void checkBoxContext_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxContext.Checked)
            {
                // display some text

                webBrowserContext.Visible = true;
            }
            else
            {
                webBrowserContext.Visible = false;
            }
        }

        private string ConvertDataTableToHTML(DataTable dt,
            int maxNrOfRows)
        {
            string html = "<table>\n";
            //add header row
            html += "<tr>\n";
            for (int i = 0; i < dt.Columns.Count && i < maxNrOfRows; i++)
                html += "  <th>" + dt.Columns[i].ColumnName + "</th>\n";
            html += "</tr>\n";
            //add rows
            for (int i = 0; i < dt.Rows.Count && i < maxNrOfRows; i++)
            {
                html += "<tr>\n";
                for (int j = 0; j < dt.Columns.Count; j++)
                    html += "  <td>" + dt.Rows[i][j].ToString() + "</td>\n";
                html += "</tr>\n";
            }
            html += "</table>";
            return html;
        }

        private void buttonGetNewSet_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(null,
                                                 "确定要换一组吗？\n一般是每周学习一组20个字，学好后才换。",
                                                 "Warning",
                                                 MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                getWeekTask(true);
                displayNewWord();
            }
        }

        private void GeneratePrintableWeek()
        {
            // generate the printable html file

            string printable = "<!DOCTYPE html>\r\n" +
                            "<html>\r\n" +
                            "<head>\r\n" +
                            "  <meta http-equiv='x-ua-compatible' content='IE=edge,chrome=1'>\r\n" +
                            "  <meta charset=\"utf-8\">\r\n" +
                            "<style>\r\n" +
                            "nw {\r\n" +
                            "    background-color: White;\r\n" +
                            "}\r\n" +
                            "np {\r\n" +
                            "    background-color: Whrite;\r\n" +
                            "}\r\n" +
                            "</style>\r\n" +
                            "</head>\r\n" +
                            "<head>\r\n" +
                            "  <link rel=\"stylesheet\" href=\"print-a4.css\">\r\n" +
                            "</head>\r\n" +
                            "\r\n" +
                            "<body>\r\n" +
                            "\r\n" +
                            "<div class=\"book\">\r\n" +
                            "  <div class=\"page\">\r\n" +
                            "    <table style=\"width:100%\" border=\"1\" frame=\"void\" rules=\"rows\" table-layout=\"fixed\">\r\n" +
                            "      <tr style='display:none;'>\r\n" +
                            "        <th>NewWords</th>\r\n" +
                            "        <th>PinYin</th>\r\n" +
                            "        <th>Context</th>\r\n" +
                            "      </tr>\r\n";  // remember to add  " </table>";

            HtmlAgilityPack.HtmlDocument docPrint;
            docPrint = new HtmlAgilityPack.HtmlDocument();
            docPrint.Load(m_pathNewWordWeek, Encoding.GetEncoding("utf-8"));

            string[] arr = new string[] { };

            foreach (var row in docPrint.DocumentNode.SelectNodes("//tr[td]"))
            {
                arr = row.SelectNodes("td").Select(td => td.InnerText).ToArray();
                printable = printable + "      <tr>\r\n";
                // printable = printable + "        <td style='min-width:200px'><font size=\"8\">" + arr[2] + "</td>\r\n";
                printable = printable + "        <td style='min-width:200px'>" + arr[2] + "</td>\r\n";
                printable = printable + "        <td>        </td>\r\n";
                printable = printable + "        <td>" + FindContext(arr[2]) + "</td>\r\n";
                printable = printable + "      </tr>\r\n";
            }
            printable = printable + "    </table>\r\n";
            printable = printable + "  </div class=\"page\">\r\n";
            printable = printable + "</div class=\"book\">\r\n";
            printable = printable + "</html>\r\n";
            string m_pathNewWordWeekPrint = m_pathNewWordWeek.Insert(m_pathNewWordWeek.Length - 5, "_Print");
            using (StreamWriter sw = new StreamWriter(m_pathNewWordWeekPrint, false, Encoding.GetEncoding("utf-8")))
            {
                sw.Write(printable);
            }
        }
    }
}
