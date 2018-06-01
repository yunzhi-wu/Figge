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
    public partial class NewInput : Form
    {
        public Form callerForm;

        private Color buttonNewWordBackColor;
        private Color buttonNewPhaseBackColor;
        private List<string> wordsHighlighted;
        private List<string> phrasesHighlighted;
        private int allTextLen;

        public NewInput()
        {
            InitializeComponent();
            buttonNewWordBackColor = DefaultBackColor;
            buttonNewPhaseBackColor = DefaultBackColor;
            wordsHighlighted = new List<string>();
            phrasesHighlighted = new List<string>();
            allTextLen = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // toggle between default and Yellow
            if (buttonNewWordBackColor == DefaultBackColor)
            {
                buttonNewWordBackColor = Color.Yellow;
            }
            else
            {
                buttonNewWordBackColor = DefaultBackColor;
            }
            buttonNewWords.BackColor = buttonNewWordBackColor;
            // Reset background of buttonNewPhrase
            buttonNewPhaseBackColor = DefaultBackColor;
            buttonNewPhrase.BackColor = buttonNewPhaseBackColor;
        }

        private void buttonNewPhrase_Click(object sender, EventArgs e)
        {
            // toggle between default and Green
            if (buttonNewPhaseBackColor == DefaultBackColor)
            {
                buttonNewPhaseBackColor = Color.Green;
            }
            else
            {
                buttonNewPhaseBackColor = DefaultBackColor;
            }
            buttonNewPhrase.BackColor = buttonNewPhaseBackColor;
            // Reset background of buttonNewWord
            buttonNewWordBackColor = DefaultBackColor;
            buttonNewWords.BackColor = buttonNewWordBackColor;
        }

        private void NewInput_Load(object sender, EventArgs e)
        {

        }

        private void NewInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            callerForm.Show();
        }

        private void displayList(List<string> l)
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
            // if some text are removed, these saved strings need be checked
            string allText = richTextBoxInput.Text;
            if (allTextLen == allText.Length)
            {
                return;
            }
            else
            {
                allTextLen = allText.Length;
            }

            for (int i = wordsHighlighted.Count - 1;i >= 0;i--)
            {
                if (!allText.Contains(wordsHighlighted[i]))
                {
                    wordsHighlighted.RemoveAt(i);
                }
            }
            displayList(wordsHighlighted);

            for (int i = phrasesHighlighted.Count - 1; i >= 0; i--)
            {
                if (!allText.Contains(phrasesHighlighted[i]))
                {
                    phrasesHighlighted.RemoveAt(i);
                }
            }
            displayList(phrasesHighlighted);

        }

        private void richTextBoxInput_MouseUp(object sender, MouseEventArgs e)
        {
            String selectedText = richTextBoxInput.SelectedText;
            if (selectedText.Length == 0)
            {
                return;
            }
            // todo: remove comma, full stop, exclamation mark, etc.

            if (buttonNewWordBackColor == Color.Yellow)
            {
                // if it is too long, it must be a wrong operation
                if (selectedText.Length > 50)
                {
                    return;
                }
                // set yellow back ground to the selected words
                // todo: check the current background, if it is Green, use another color
                richTextBoxInput.SelectionBackColor = Color.Yellow;

                // save them into local variabls
                wordsHighlighted.Add(selectedText);

                displayList(wordsHighlighted);
            }
            else if (buttonNewPhaseBackColor == Color.Green)
            {
                // if it is too long, it must be a wrong operation
                if (selectedText.Length > 50)
                {
                    return;
                }

                // set green back color to the select phrase
                // todo: check the current background, if it is Yellow, use another color
                richTextBoxInput.SelectionBackColor = Color.Green;

                // save them into local variables
                phrasesHighlighted.Add(selectedText);

                displayList(phrasesHighlighted);
            }
        }
    }
}
