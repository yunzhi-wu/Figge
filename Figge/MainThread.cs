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
        private string[] m_recordsAllText;
        private string m_pathAllText = @"MyLearning.html";

        private string[] m_newWords;
        private string m_pathNewWord = @"MyNewWords.html";

        private Dictionary<char, int> m_histogram;
        private Dictionary<string, int> m_histogram_eng;

        private bool m_isEnglishLikeText = true;

        public MainThread()
        {
            InitializeComponent();
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                m_pathAllText = ofd.FileName;
            }
            // file name should indicate the language
            if (m_pathAllText.ToUpperInvariant().Contains("_SE"))
            {
                m_isEnglishLikeText = true;
            }
            else if (m_pathAllText.ToUpperInvariant().Contains("_ZH"))
            {
                m_isEnglishLikeText = false;
            }

            if (!File.Exists(m_pathAllText))
            {
                StreamWriter sw = File.CreateText(m_pathAllText);
                sw.Close();
            }
            m_recordsAllText = File.ReadAllLines(m_pathAllText, Encoding.GetEncoding("utf-8"));
            
            if (m_recordsAllText.Length == 0)
            {
                m_recordsAllText = new string[21];
                int i = 0;
                m_recordsAllText[i++] = "<!DOCTYPE html>";
                m_recordsAllText[i++] = "<html>";
                m_recordsAllText[i++] = "<head>";
                m_recordsAllText[i++] = "  <meta http-equiv='x-ua-compatible' content='IE=edge,chrome=1'>";
                m_recordsAllText[i++] = "  <meta charset=\"utf-8\">";
                m_recordsAllText[i++] = "<style>";
                m_recordsAllText[i++] = "nw {";
                m_recordsAllText[i++] = "    background-color: Orange;";
                m_recordsAllText[i++] = "}";
                m_recordsAllText[i++] = "np {";
                m_recordsAllText[i++] = "    background-color: DodgerBlue;";
                m_recordsAllText[i++] = "}";
                m_recordsAllText[i++] = "</style>";
                m_recordsAllText[i++] = "</head>";
                m_recordsAllText[i++] = "<table style=\"width:100%\" border=1 frame=void rules=rows>";
                m_recordsAllText[i++] = "  <tr>";
                m_recordsAllText[i++] = "    <th>Time Created</th>";
                m_recordsAllText[i++] = "    <th>Content</th>";
                m_recordsAllText[i++] = "  </tr>";
                m_recordsAllText[i++] = "</table>";
                m_recordsAllText[i++] = "</html>";
                using (StreamWriter sw = new StreamWriter(m_pathAllText, false, Encoding.GetEncoding("utf-8")))
                {
                    for (i = 0; i < m_recordsAllText.Length; i++)
                    {
                        sw.WriteLine(m_recordsAllText[i]);
                    }
                }
            }

            /*************************************************
             * HTML file for New words
             *************************************************/
            m_pathNewWord = string.Copy(m_pathAllText);
            m_pathNewWord = m_pathNewWord.Replace(".html", "_Words.html");
            if (!File.Exists(m_pathNewWord))
            {
                StreamWriter sw = File.CreateText(m_pathNewWord);
                sw.Close();
            }
            m_newWords = File.ReadAllLines(m_pathNewWord, Encoding.GetEncoding("utf-8"));

            if (m_newWords.Length == 0)
            {
                m_newWords = new string[25];
                int i = 0;
                m_newWords[i++] = "<!DOCTYPE html>";
                m_newWords[i++] = "<html>";
                m_newWords[i++] = "<head>";
                m_newWords[i++] = "  <meta http-equiv='x-ua-compatible' content='IE=edge,chrome=1'>";
                m_newWords[i++] = "  <meta charset=\"utf-8\">";
                m_newWords[i++] = "<style>";
                m_newWords[i++] = "nw {";
                m_newWords[i++] = "    background-color: Orange;";
                m_newWords[i++] = "}";
                m_newWords[i++] = "np {";
                m_newWords[i++] = "    background-color: DodgerBlue;";
                m_newWords[i++] = "}";
                m_newWords[i++] = "</style>";
                m_newWords[i++] = "</head>";
                m_newWords[i++] = "<!-- 01/01/1900 00:00:00 -->";
                m_newWords[i++] = "<table style=\"width:100%\" border=1 frame=void rules=rows>";
                m_newWords[i++] = "  <tr>";
                m_newWords[i++] = "    <th>Date</th>";
                m_newWords[i++] = "    <th>OKDate</th>";
                m_newWords[i++] = "    <th>NewWords</th>";
                m_newWords[i++] = "    <th>TimesAdded</th>";
                m_newWords[i++] = "    <th>Familiarity</th>";
                m_newWords[i++] = "  </tr>";
                m_newWords[i++] = "</table>";
                m_newWords[i++] = "</html>";
                using (StreamWriter sw = new StreamWriter(m_pathNewWord, false, Encoding.GetEncoding("utf-8")))
                {
                    for (i = 0; i < m_newWords.Length; i++)
                    {
                        sw.WriteLine(m_newWords[i]);
                    }
                }
            }
            if (m_isEnglishLikeText)
            {
                m_histogram_eng = new Dictionary<string, int>();
            }
            else
            {
                m_histogram = new Dictionary<char, int>();
            }
            
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
            NewInput next_form = new NewInput(m_pathAllText, m_recordsAllText, m_isEnglishLikeText);
            next_form.callerForm = this;
            this.Hide();
            next_form.ShowDialog();
        }

        public void UpdateInfo()
        {
            m_recordsAllText = File.ReadAllLines(m_pathAllText, Encoding.GetEncoding("utf-8"));
            updateHistogram();
            updateNewWords();
        }

        private void updateHistogram()
        {
            bool aRecordStarted = false;

            if (m_isEnglishLikeText)
            {
                m_histogram_eng.Clear();
            }
            else
            {
                m_histogram.Clear();
            }

            foreach (string line in m_recordsAllText)
            {
                // it can be multiple lines for each record
                if (line.Contains("<td>") && !Regex.IsMatch(line, @"\d\d/\d\d/\d\d\d\d"))
                {
                    aRecordStarted = true;
                }
                if (aRecordStarted)
                {
                    if (m_isEnglishLikeText)
                    {
                        string tmp = Regex.Replace(line, @"\.|,", " ");
                        string tmp2 = Regex.Replace(tmp, @"(\<np\>)|(\</np\>)|(\<nw\>)|(\</nw\>)|(\<td\>)|(\</td\>)", "");
                        string tmp3 = tmp2.ToLower();
                        string[] words = tmp3.Split();
                        foreach (string word in words)
                        {
                            if (m_histogram_eng.ContainsKey(word))
                            {
                                m_histogram_eng[word]++;
                            }
                            else
                            {
                                m_histogram_eng.Add(word, 1);
                            }
                        }
                    }
                    else
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
                }
                if (line.Contains("</td>") && !Regex.IsMatch(line, @"\d\d/\d\d/\d\d\d\d"))
                {
                    aRecordStarted = false;
                }
            }
            if (m_isEnglishLikeText)
            {
                UserStatus.Text = "You have recorded " + m_histogram_eng.Count + " words\n";
            }
            else
            {
                UserStatus.Text = "你已经学了 " + m_histogram.Count + " 个汉字\n";
            }
        }

        private void Review_Click(object sender, EventArgs e)
        {
            // display_data dis_form = new display_data();
            DisplayExist next_form = new DisplayExist(m_pathAllText);
            next_form.callerForm = this;
            this.Hide();
            next_form.ShowDialog();
        }

        private void updateNewWords()
        {
            // get the new words table's last update timestamp
            string lastUpdateTimeStr = m_newWords[14];

            Match match = Regex.Match(lastUpdateTimeStr, @"\d\d/\d\d/\d\d\d\d \d\d:\d\d:\d\d");

            if (!match.Success)
            {
                // popup a warning dialogue box for information
                DialogResult ret = MessageBox.Show(null,
                                                   "New words table doesn't contain timestamp, check the html file",
                                                   "Info",
                                                   MessageBoxButtons.OK);
                return;
            }
            Console.WriteLine("Timestamp in new words table file: " + match.Value);
            DateTime lastUpdateTime = Convert.ToDateTime(match.Value);

            // go through the "all text data table", compare the date time in the row with the timestamp
            // if a row's date time is later than the new words table's last update timestamp,
            // collect all the new words in the row
            bool isNewWordsAdded = false;
            bool aRecordStarted = false;
            int rowNr = 1;
            DateTime dataTime = DateTime.MinValue;
            DateTime lastestDateTime = lastUpdateTime;
            List<string> newWord_l = new List<string>();

            foreach (string line in m_recordsAllText)
            {
                // date time in a row of the all text table is before the text content
                if (!aRecordStarted)
                {
                    // date time is like "<td>19/12/2018 20:48:01</td>"
                    if (line.Contains("<td>"))
                    {
                        Match match2 = Regex.Match(line, @"\d\d/\d\d/\d\d\d\d \d\d:\d\d:\d\d");
                        if (match2.Success)
                        {
                            Console.WriteLine("dateTime in all text table row " + rowNr + ": " + match2.Value);
                            dataTime = Convert.ToDateTime(match2.Value);

                            if (DateTime.Compare(dataTime, lastUpdateTime) <= 0)
                            {
                                Console.WriteLine("dateTime in all text table row " + rowNr + " has been processed, stop");
                                break;
                            }

                            if (DateTime.Compare(lastestDateTime, dataTime) < 0)
                            {
                                Console.WriteLine("dateTime in all text table row " + rowNr + " is later");
                                lastestDateTime = dataTime;
                            }
                        }
                    }
                }
                // it can be multiple lines for each record
                if (line.Contains("<td>") && !Regex.IsMatch(line, @"\d\d/\d\d/\d\d\d\d"))
                {
                    aRecordStarted = true;
                    newWord_l.Clear();
                }
                if (aRecordStarted)
                {
                    if (m_isEnglishLikeText)
                    {
                        string tmp = Regex.Replace(line, @"\.|,", " ");
                        string tmp2 = Regex.Replace(tmp, @"(\<td\>)|(\</td\>)", "");
                        string tmp3 = tmp2.ToLower();
                        string[] words = tmp3.Split();
                        foreach (string word in words)
                        {
                            if (Regex.IsMatch(word, @"(<np>)|(</np>)|(<nw>)|(</nw>)"))
                            {
                                string newWord = Regex.Replace(word, @"(<np>)|(</np>)|(<nw>)|(</nw>)", "");
                                Console.WriteLine("Found a new word: \"" + newWord + "\" in row " + rowNr);
                                if (!newWord_l.Contains(newWord))
                                {
                                    newWord_l.Add(newWord);
                                    Console.WriteLine("Added a new word: \"" + newWord + "\" in newWord_l, count " + newWord_l.Count);
                                }
                            }
                        }
                    }
                    else
                    {
                        int startPos = 0;
                        Regex startReg = new Regex(@"(\<np\>)|(\<nw\>)");
                        Regex endWordReg = new Regex(@"(\</nw\>)");
                        Regex endPhraseReg = new Regex(@"(\</np\>)");
                        while (startPos < line.Length)
                        {
                            match = startReg.Match(line, startPos);
                            if (match.Success)
                            {
                                string startStr = line.Substring(match.Index, 4);

                                Match match2;
                                if (startStr.Contains("<nw>"))
                                {
                                    match2 = endWordReg.Match(line, match.Index);
                                }
                                else
                                {
                                    match2 = endPhraseReg.Match(line, match.Index);
                                }

                                if (match2.Success)
                                {
                                    string newWord = line.Substring(match.Index + 4, match2.Index - match.Index - 4);

                                    if (!newWord_l.Contains(newWord))
                                    {
                                        newWord_l.Add(newWord);
                                        Console.WriteLine("Added a new word: \"" + newWord + "\" in newWord_l, count " + newWord_l.Count);
                                    }
                                    startPos = match2.Index + 5;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }

                if (line.Contains("</td>") && !Regex.IsMatch(line, @"\d\d/\d\d/\d\d\d\d"))
                {
                    aRecordStarted = false;

                    // update the new word table
                    if (newWord_l.Count > 0)
                    {
                        isNewWordsAdded = true;
                    }
                    // todo: use hash table to make the search quicker
                    foreach (string newWord in newWord_l)
                    {
                        int lineIndex = 24;
                        for (; lineIndex < m_newWords.Length; lineIndex++)
                        {
                            string lineInNewWord = m_newWords[lineIndex];
                            if (lineInNewWord.Contains(newWord))
                            {
                                string timesAddStr = m_newWords[lineIndex + 1];
                                Match match3 = Regex.Match(timesAddStr, @"\d+");
                                if (!match3.Success)
                                {
                                    // popup a warning dialogue box for information
                                    DialogResult ret = MessageBox.Show(null,
                                                                       "New words table doesn't contain timesAdded, line number: " + lineIndex,
                                                                       "Info",
                                                                       MessageBoxButtons.OK);
                                    return;
                                }
                                int timesAdd = Convert.ToInt32(match3.Value) + 1;
                                Console.WriteLine("new words table contains new word \"" + newWord + "\", added " + timesAdd + " times till now");
                                m_newWords[lineIndex + 1] = Regex.Replace(m_newWords[lineIndex + 1], @"\d+", timesAdd.ToString());
                                break;
                            }
                        }
                        if (lineIndex == m_newWords.Length)
                        {
                            int newLength = m_newWords.Length + 7;
                            string[] tmp = new string[newLength];
                            m_newWords.CopyTo(tmp, 0);

                            tmp[newLength - 2] = tmp[newLength - 9];
                            tmp[newLength - 1] = tmp[newLength - 8];

                            tmp[newLength - 9] = "  <tr>";
                            tmp[newLength - 8] = "    <td>" + dataTime.Date.ToShortDateString() + "</td>";
                            tmp[newLength - 7] = "    <td></td>";
                            tmp[newLength - 6] = "    <td>" + newWord + "</td>";
                            tmp[newLength - 5] = "    <td>1</td>";
                            tmp[newLength - 4] = "    <td>1</td>";
                            tmp[newLength - 3] = "  </tr>";

                            m_newWords = new string[newLength];

                            tmp.CopyTo(m_newWords, 0);
                        }
                    }

                    rowNr++;
                }
            }

            // update the new words table's last update timestamp
            if (isNewWordsAdded)
            {
                // Regex.Match(line, @"\d\d/\d\d/\d\d\d\d \d\d:\d\d:\d\d");
                m_newWords[14] = Regex.Replace(m_newWords[14], @"\d\d/\d\d/\d\d\d\d \d\d:\d\d:\d\d", lastestDateTime.ToString());

                // update these new words into newWords table file
                using (StreamWriter sw = new StreamWriter(m_pathNewWord, false, Encoding.GetEncoding("utf-8")))
                {
                    for (int i = 0; i < m_newWords.Length; i++)
                    {
                        sw.WriteLine(m_newWords[i]);
                    }
                }
            }
        }

        private void MemorizeCard_Click(object sender, EventArgs e)
        {
            // display_data dis_form = new display_data();
            MemoryCard next_form = new MemoryCard(m_pathNewWord,
                m_recordsAllText,
                m_isEnglishLikeText);
            next_form.callerForm = this;
            this.Hide();
            next_form.ShowDialog();
        }
    }
}
