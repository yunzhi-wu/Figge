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
        enum ColorUseFor
        {
            background,
            font
        };

        public MainThread callerForm;

        private Color buttonNewWordBackColor;
        private Color buttonNewPhaseBackColor;
        private Color buttonClearMarkBackColor;
        private List<string> m_words_l;
        private List<string> m_phrase_l;
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
            m_words_l = new List<string>();
            m_phrase_l = new List<string>();
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
            m_words_l = new List<string>();
            m_phrase_l = new List<string>();
            allTextLen = 0;
            this.createdTime = createdTime;
            savedBefore = true;
        }

        private void buttonNewWords_Click(object sender, EventArgs e)
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
                    // do not add if it is marked as new phrase
                    foreach (string item in m_phrase_l)
                    {
                        if (item.Contains(richTextBoxInput.SelectedText))
                        {
                            // popup a warning dialogue box for information
                            DialogResult ret = MessageBox.Show(null,
                                                               "This string has been marked as new phrase, can't be marked as new words as well. Need clear the mark before remark it",
                                                               "Info",
                                                               MessageBoxButtons.OK);
                            return;
                        }
                    }
                    addToListAndUpdateAllText(richTextBoxInput.SelectedText, m_words_l, buttonNewWordBackColor, ColorUseFor.background);
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
                if (richTextBoxInput.SelectedText.Length > 1 &&
                    richTextBoxInput.SelectedText.Length < 50)
                {
                    // remove item from new word list if it is included in this selection
                    List<string> listCopy = new List<string>(m_words_l.ToArray());
                    foreach (string item in listCopy)
                    {
                        if (richTextBoxInput.SelectedText.Contains(item))
                        {
                            m_words_l.Remove(item);
                        }
                    }
                    addToListAndUpdateAllText(richTextBoxInput.SelectedText, m_phrase_l, buttonNewPhaseBackColor, ColorUseFor.background);
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
                removeFromListAndUpdateAllText(m_words_l, richTextBoxInput.BackColor, ColorUseFor.background);
                removeFromListAndUpdateAllText(m_phrase_l, richTextBoxInput.BackColor, ColorUseFor.background);
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
            if (length > 0)
            {
                if (isSeparator(richTextBoxInput.SelectedText[length - 1]))
                {
                    richTextBoxInput.SelectionLength = -- length;
                }
            }
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
                
                // todo: check the current background, if it is not default color,
                //       use some color mixed

                // do not add if it is marked as new phrase
                foreach (string item in m_phrase_l)
                {
                    if (item.Contains(richTextBoxInput.SelectedText))
                    {
                        // popup a warning dialogue box for information
                        DialogResult ret = MessageBox.Show(null,
                                                           "This string has been marked as new phrase, can't be marked as new words as well. Need clear the mark before remark it",
                                                           "Info",
                                                           MessageBoxButtons.OK);

                        return;
                    }
                }

                // set back ground to the selected words
                addToListAndUpdateAllText(richTextBoxInput.SelectedText, m_words_l, buttonNewWordBackColor, ColorUseFor.background);
            }
            else if (buttonNewPhaseBackColor != DefaultBackColor)
            {
                // if it is too long, it must be a wrong operation
                if (length > 50)
                {
                    return;
                }

                // remove item from new word list if it is included in this selection
                List<string> listCopy = new List<string>(m_words_l.ToArray());
                foreach (string item in listCopy)
                {
                    if (richTextBoxInput.SelectedText.Contains(item))
                    {
                        m_words_l.Remove(item);
                        Console.Write("\"" + item + "\" is removed from list, nofItem left " + m_words_l.Count + '\n');
                    }
                }

                // set back ground to the selected words
                // todo: check the current background, if it is not default color,
                //       use some color mixed
                addToListAndUpdateAllText(richTextBoxInput.SelectedText, m_phrase_l, buttonNewPhaseBackColor, ColorUseFor.background);
            }
            else if (buttonClearMarkBackColor != DefaultBackColor)
            {
                removeFromListAndUpdateAllText(m_words_l, richTextBoxInput.BackColor, ColorUseFor.background);
                removeFromListAndUpdateAllText(m_phrase_l, richTextBoxInput.BackColor, ColorUseFor.background);
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

                string content;

                bool isEnglishLikeText = true;

                content = addHtmlPattern(richTextBoxInput.Text,
                    m_words_l,
                    "<nw>",
                    "</nw>",
                    isEnglishLikeText);

                content = addHtmlPattern(content,
                    m_phrase_l,
                    "<np>",
                    "</np>",
                    isEnglishLikeText);

                string temp = content.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "<br>\r\n");

                newRecord[0] = "  <tr>";
                newRecord[1] = "    <td>" + createdTime + "</td>";
                newRecord[2] = "    <td>" + temp + "</td>";
                newRecord[3] = "  </tr>";

                // TODO: seek and insert, instead of overwriting the whole file
                using (StreamWriter wr = new StreamWriter(m_path, false, Encoding.GetEncoding("utf-8")))
                {
                    const int nofHeadLines = 19;
                    for (int i = 0; i < nofHeadLines; i++)
                    {
                        wr.WriteLine(m_records[i]);
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        wr.WriteLine(newRecord[i]);
                    }
                    for (int i = nofHeadLines; i < length; i++)
                    {
                        wr.WriteLine(m_records[i]);
                    }                    
                }
            }

            // update the savedBefore flag
            savedBefore = true;
        }

        private string addHtmlPattern(string orig,
            List<string> list,
            string prefix,
            string suffix,
            bool isEnglishLikeText)
        {
            if (list.Count < 0)
            {
                return "";
            }

            debug_displayList(list);

            string result = String.Copy(orig);

            foreach (string item in list)
            {
                string newitem = prefix + item + suffix;

                string temp = string.Copy(result);
                result = "";

                if (!isEnglishLikeText)
                {
                    // chinese
                    result = temp.Replace(item, newitem);
                }
                else
                {
                    // English like text, words separated by space

                    int start_pos = 0;
                    int lastCopy = start_pos;

                    while (start_pos < temp.Length)
                    {
                        int index = temp.IndexOf(item, start_pos);
                        if (index == -1)
                        {
                            // no new found, copy all the rest
                            result += temp.Substring(lastCopy, temp.Length - lastCopy);
                            break;
                        }
                        if (index != 0 && !isSeparator(temp[index - 1]))
                        {
                            // the char before is not a separator
                            start_pos = index + item.Length;
                            continue;
                        }
                        if ((index + item.Length != temp.Length) &&
                            !isSeparator(temp[index + item.Length]))
                        {
                            // the char after is not a separator
                            start_pos = index + item.Length;
                            continue;
                        }
                        // there is a whole word match
                        result += temp.Substring(lastCopy, index - lastCopy);
                        result += newitem;
                        Console.Write("addHtmlPattern() replaced \"" + item + "\" at index " + index + "\n");
                        start_pos = index + item.Length;
                        lastCopy = start_pos;
                    }
                }
            }
            return result;
        }

        private bool addToList(string item, List<string> list)
        {
            // check if the same item is in the list already
            if (list.Contains(item))
            {
                return false;
            }
            int length = list.Count;
            list.Insert(length, item);
            Console.Write("\"" + item + "\" is added into list, nofItems " + list.Count + "\n");
            return true;
        }

        private bool removeFromList(string item, List<string> list)
        {
            // check if the same item is in the list already
            if (!list.Contains(item))
            {
                return false;
            }
            list.Remove(item);
            Console.Write("\"" + item + "\" is removed from list, nofItems " + list.Count + "\n");
            return true;
        }

        private void updateAllText(string item, Color clr, ColorUseFor use)
        {
            // store the current selection for restoring
            int length = richTextBoxInput.SelectedText.Length;
            int start_pos_old = richTextBoxInput.SelectionStart;

            if (use == ColorUseFor.background)
            {
                int start_pos = 0;
                while (start_pos < richTextBoxInput.Text.Length)
                {
                    int index = richTextBoxInput.Text.IndexOf(item, start_pos);
                    if (index == -1)
                    {
                        break;
                    }

                    // For English like text, word is usaully separated by space, comma, etc,
                    // so need match "whole" word
                    bool isEnglishLikeText = true;
                    if (isEnglishLikeText)
                    {
                        if (index != 0 && !isSeparator(richTextBoxInput.Text[index -1]))
                        {
                            // the char before is not a separator
                            start_pos = index + item.Length;
                            continue;
                        }
                        if ((index + item.Length != richTextBoxInput.Text.Length) && 
                            !isSeparator(richTextBoxInput.Text[index + item.Length]))
                        {
                            // the char after is not a separator
                            start_pos = index + item.Length;
                            continue;
                        }
                    }

                    richTextBoxInput.Select(index, item.Length);
                    richTextBoxInput.SelectionBackColor = clr;

                    Console.Write("\"" + item + "\" is found at " + index + " and is marked / unmarked also\n");

                    start_pos = index + item.Length;
                }
            }

            // restore the selection
            richTextBoxInput.Select(start_pos_old, length);
        }

        private void addToListAndUpdateAllText(string text, List<string> list, Color clr, ColorUseFor usdFor)
        {
            if (addToList(text, list))
            {
                updateAllText(text, clr, usdFor);
            }
        }

        private void removeFromListAndUpdateAllText(List<string> list, Color clr, ColorUseFor usdFor)
        {
            string selectedText = richTextBoxInput.SelectedText;
            if (list.Count > 0)
            {
                List<string> listCopy = new List<string>(list.ToArray());
                foreach (string item in listCopy)
                {
                    if (selectedText.Contains(item))
                    {
                        if (removeFromList(item, list))
                        {
                            updateAllText(item, clr, usdFor);
                        }
                    }
                }
            }
        }

        private bool isSeparator(char c)
        {
            char[] separators = { ' ', ',', '.', '(', ')', '\n' };
            if (separators.Contains(c))
            {
                return true;
            }
            return false;
        }

    }
}