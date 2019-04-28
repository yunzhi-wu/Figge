using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace Figge
{
    public partial class Statistics : Form
    {
        public MainThread callerForm;
        private string m_pathNewWords;
        private string m_pathNewWordWeek;
        private string[] m_records;
        private bool m_isEnglishLike;

        private HtmlAgilityPack.HtmlDocument m_doc;

        private DataTable m_newWords;

        private const int nrofWeeks = 30;

        private PointF[] ptsCountNewArray;
        private PointF[] ptsCountOKedNewArray;

        private int countMaxNewWords;
        private int countMaxOKedNewWords;

        public Statistics(string pathNewWord,
            string[] records,
            bool isEnglishLike)
        {
            InitializeComponent();

            m_pathNewWords = string.Copy(pathNewWord);
            m_pathNewWordWeek = m_pathNewWords.Insert(m_pathNewWords.Length - 5, "_Week");

            m_records = records;
            m_isEnglishLike = isEnglishLike;

            m_newWords = new DataTable();

            ptsCountNewArray = new PointF[nrofWeeks];
            ptsCountOKedNewArray = new PointF[nrofWeeks];

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
            // expression = "Familiarity < 10";
            /* *******
             *
              <tr>
                <th>Date</th>
                <th>OKDate</th>
                <th>NewWords</th>
                <th>TimesAdded</th>
                <th>Familiarity</th>
              </tr>
             */
            /*
            // 2015 is year, 12 is month, 25 is day  28/12/2018
            DateTime date1 = new DateTime(2018, 12, 28);

            IEnumerable<DataRow> selectedRows =
              m_newWords.AsEnumerable().Where(row => (Convert.ToDateTime(row["Date"]) == date1));

            foreach (DataRow row in selectedRows)
            {
                Console.WriteLine("{0}, {1}", row[0], row[1]);
            }

            */
            DateTime nextSunday = DateTime.Today;

            while (nextSunday.DayOfWeek != DayOfWeek.Sunday)
                nextSunday = nextSunday.AddDays(1);

            Console.WriteLine("Today {0} is {1}, next Sunday is {2}",
                DateTime.Today, 
                DateTime.Today.DayOfWeek,
                nextSunday);
            
            int[] countNewWords = new int[nrofWeeks];
            int[] countOKedNewWords = new int[nrofWeeks];

            foreach (DataRow row in m_newWords.Rows)
            {
                DateTime dateAdded = Convert.ToDateTime(row["Date"]);
                DateTime dataOked;
                int timeInWeek = (int)((nextSunday - dateAdded).TotalDays / 7);
                if (timeInWeek < nrofWeeks)
                {
                    countNewWords[nrofWeeks - 1 - timeInWeek]++;
                    if (nrofWeeks - 1 - timeInWeek == 27)
                    {
                        Console.WriteLine("Week 2: {0} {1} {2} {3}",
                            row[0], row[1], row[2], row[3]);
                    }
                }
                if (!row["OKDate"].Equals(""))
                {
                    dataOked = Convert.ToDateTime(row["OKDate"]);
                    timeInWeek = (int)((nextSunday - dataOked).TotalDays / 7);
                    if (timeInWeek < nrofWeeks)
                    {
                        countOKedNewWords[nrofWeeks - 1 - timeInWeek]++;
                    }
                }
            }

            Console.WriteLine("count of new words:");
            foreach (int cnt in countNewWords)
            {
                Console.WriteLine("{0} ", cnt);
            }
            Console.WriteLine("count of finished words:");
            foreach (int cnt in countOKedNewWords)
            {
                Console.WriteLine("{0} ", cnt);
            }

            // find the maximum value
            int max = countNewWords.Max();
            countMaxNewWords = max;

            // zero point (50, 140)
            for (int i = 0; i < nrofWeeks; i++)
            {
                float x = (i + 1) * 20 + 50;
                float y = 140 - ((float)countNewWords[i] / (float)max * 100);
                ptsCountNewArray[i] = new PointF(x, y);
            }

            // find the maximum value
            max = countOKedNewWords.Max();
            countMaxOKedNewWords = max;

            // zero point (50, 140)
            for (int i = 0; i < nrofWeeks; i++)
            {
                float x = (i + 1) * 20 + 50;
                float y = 280 - ((float)countOKedNewWords[i] / (float) max * 100);
                Console.WriteLine("point {0}: ({1}, {2})", i, x, y);
                ptsCountOKedNewArray[i] = new PointF(x, y);
            }
        }

        private void Statistics_FormClosed(object sender, FormClosedEventArgs e)
        {
            callerForm.Show();
        }

        private void Statistics_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Gray, 2);
            g.DrawLine(p, 50,  40,  50, 140); // length 100
            g.DrawLine(p, 50, 140, 650, 140); // length 600

            g.DrawLine(p, 50, 180,  50, 280); // length 100
            g.DrawLine(p, 50, 280, 650, 280); // length 600

            g.DrawLine(p, 50, 320,  50, 420); // length 100
            g.DrawLine(p, 50, 420, 650, 420); // length 600

            // reference line
            p = new Pen(Color.Gray, 1);
            g.DrawLine(p, 50, 140 - (float)20 / (float)countMaxNewWords * 100,
                         650, 140 - (float)20 / (float)countMaxNewWords * 100);

            g.DrawLine(p, 50, 280 - (float)20 / (float)countMaxNewWords * 100,
                         650, 280 - (float)20 / (float)countMaxNewWords * 100);

            p = new Pen(Color.Blue, 2);
            g.DrawLines(p, ptsCountNewArray);

            p = new Pen(Color.Green, 2);
            g.DrawLines(p, ptsCountOKedNewArray);

            
            
        }
    }
}
