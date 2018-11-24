using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;



namespace Figge
{
    public partial class NewInput : Form
    {
        public MainThread callerForm;

        private Color buttonNewWordBackColor;
        private Color buttonNewPhaseBackColor;
        private Color buttonClearMarkBackColor;
        private List<string> wordsHighlighted;
        private List<string> phrasesHighlighted;
        private int allTextLen;
        private string allText;
        private DateTime createdTime;
        private Boolean savedBefore;

        private string m_path;

        private string[] m_records;

        public NewInput(string path, string[] records)
        {
            InitializeComponent();

            m_path = path;
            m_records = records;

            buttonNewWordBackColor = DefaultBackColor;
            buttonNewPhaseBackColor = DefaultBackColor;
            buttonClearMarkBackColor = DefaultBackColor;
            wordsHighlighted = new List<string>();
            phrasesHighlighted = new List<string>();
            allTextLen = 0;
            createdTime = DateTime.Now;
            Console.Write("createdTime: " + createdTime + '\n');
            savedBefore = false;
        }

        // this is to read from existing record, so there is createdTime
        // this createdTime is used to find the right place to save it back
        public NewInput(DateTime createdTime)
        {
            InitializeComponent();
            buttonNewWordBackColor = DefaultBackColor;
            buttonNewPhaseBackColor = DefaultBackColor;
            buttonClearMarkBackColor = DefaultBackColor;
            wordsHighlighted = new List<string>();
            phrasesHighlighted = new List<string>();
            allTextLen = 0;
            this.createdTime = createdTime;
            savedBefore = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Reset background of buttonNewPhrase
            buttonNewPhaseBackColor = DefaultBackColor;
            buttonNewPhrase.BackColor = buttonNewPhaseBackColor;
            // Reset background of clearMark
            buttonClearMarkBackColor = DefaultBackColor;
            ClearMark.BackColor = buttonClearMarkBackColor;

            // toggle between default and Orange
            if (buttonNewWordBackColor == DefaultBackColor)
            {
                buttonNewWordBackColor = Color.Orange;
                if (richTextBoxInput.SelectedText.Length > 0 &&
                    richTextBoxInput.SelectedText.Length < 50)
                {
                    richTextBoxInput.SelectionBackColor = Color.Orange;
                }
            }
            else
            {
                buttonNewWordBackColor = DefaultBackColor;
            }
            buttonNewWords.BackColor = buttonNewWordBackColor;
        }

        private void buttonNewPhrase_Click(object sender, EventArgs e)
        {
            // Reset background of buttonNewWord
            buttonNewWordBackColor = DefaultBackColor;
            buttonNewWords.BackColor = buttonNewWordBackColor;
            // Reset background of clearMark
            buttonClearMarkBackColor = DefaultBackColor;
            ClearMark.BackColor = buttonClearMarkBackColor;

            // toggle between default and DodgerBlue
            if (buttonNewPhaseBackColor == DefaultBackColor)
            {
                buttonNewPhaseBackColor = Color.DodgerBlue;
                if (richTextBoxInput.SelectedText.Length > 0 &&
                    richTextBoxInput.SelectedText.Length < 50)
                {
                    // richTextBoxInput.SelectionBackColor = Color.DodgerBlue;
                    richTextBoxInput.SelectionColor = Color.Brown;
                }
            }
            else
            {
                buttonNewPhaseBackColor = DefaultBackColor;
            }
            buttonNewPhrase.BackColor = buttonNewPhaseBackColor;
        }

        private void ClearMark_Click(object sender, EventArgs e)
        {
            // Reset background of buttonNewWord
            buttonNewWordBackColor = DefaultBackColor;
            buttonNewWords.BackColor = buttonNewWordBackColor;
            // Reset background of buttonNewPhrase
            buttonNewPhaseBackColor = DefaultBackColor;
            buttonNewPhrase.BackColor = buttonNewPhaseBackColor;

            // toggle between default and White
            if (buttonClearMarkBackColor == DefaultBackColor)
            {
                buttonClearMarkBackColor = Color.White;
                if (richTextBoxInput.SelectedText.Length > 0 &&
                    richTextBoxInput.SelectedText.Length < 50)
                {
                    richTextBoxInput.SelectionBackColor = richTextBoxInput.BackColor;
                }
            }
            else
            {
                buttonClearMarkBackColor = DefaultBackColor;
            }
            ClearMark.BackColor = buttonClearMarkBackColor;
        }

        private void NewInput_Load(object sender, EventArgs e)
        {

        }

        private void NewInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            callerForm.UpdateInfo();
            callerForm.Show();
        }

        private void debug_displayList(List<string> l)
        {
            // debug output
            Console.Write("Number of items: " + l.Count + '\n');
            for (int i = l.Count - 1; i >= 0; i--)
            {
                Console.Write(l[i] + '\n');
            }
        }

        private void richTextBoxInput_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void richTextBoxInput_TextChanged(object sender, EventArgs e)
        {
            // get the new text
            allText = richTextBoxInput.Text;

            // if some text are removed, these saved strings need be checked
            if (allTextLen == allText.Length)
            {
                return;
            }
            else
            {
                allTextLen = allText.Length;
            }
        }

        private void richTextBoxInput_MouseUp(object sender, MouseEventArgs e)
        {
            int length = richTextBoxInput.SelectedText.Length;
            if (length == 0)
            {
                return;
            }

            if (buttonNewWordBackColor != DefaultBackColor)
            {
                // if it is too long, it must be a wrong operation
                if (length > 50)
                {
                    return;
                }
                // set back ground to the selected words
                // todo: check the current background, if it is not default color,
                //       use some color mixed
                richTextBoxInput.SelectionBackColor = buttonNewWordBackColor;
            }
            else if (buttonNewPhaseBackColor != DefaultBackColor)
            {
                // if it is too long, it must be a wrong operation
                if (length > 50)
                {
                    return;
                }
                // set back ground to the selected words
                // todo: check the current background, if it is not default color,
                //       use some color mixed
                richTextBoxInput.SelectionBackColor = buttonNewPhaseBackColor;
            }
            else if (buttonClearMarkBackColor != DefaultBackColor)
            {
                richTextBoxInput.SelectionBackColor = richTextBoxInput.BackColor;
            }

        }

        // save as html 5 format
        // color scheme https://www.w3schools.com/html/html_colors.asp

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // save or overwrite this piece
            if (savedBefore)
            {
                // popup a warning dialogue box for overwriting
                DialogResult ret = MessageBox.Show(null,
                                                   "Do you want to update by overwriting?",
                                                   "Warning!",
                                                   MessageBoxButtons.YesNo);
                if (ret != DialogResult.OK)
                {
                    return;
                }
            }
            else // a newly created record
            {
                // attach the record at the end
                int length = m_records.Length;

                string[] newRecord = new string[4];

                /*
                 <tr>
                   <td>time</td>
                   <td>content</td> 
                 </tr>
                 */
                
                newRecord[0] = "  <tr>";
                newRecord[1] = "    <td>" + createdTime + "</td>";
                newRecord[2] = "    <td>" + richTextBoxInput.Text + "</td>";
                newRecord[3] = "  </tr>";

                using (StreamWriter wr = new StreamWriter(m_path))
                {
                    for (int i = 0; i < length - 1; i++)
                    {
                        wr.WriteLine(m_records[i]);
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        wr.WriteLine(newRecord[i]);
                    }
                    wr.WriteLine(m_records[length - 1]);
                }
            }

            // update the savedBefore flag
            savedBefore = true;
        }

    }
}