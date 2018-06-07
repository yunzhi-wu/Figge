using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
        private string allText;

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
                // todo: split in the proper way
                foreach (char charactor in selectedText.ToCharArray())
                {
                    // const char[] punctuations = { ',', '.', '\?', '!', '，', '。', '？', '！' };
                    
                    if (!wordsHighlighted.Contains(charactor.ToString()))
                    {
                        Console.Write(charactor);
                        wordsHighlighted.Add(charactor.ToString());
                    }
                }

                // displayList(wordsHighlighted);
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

                // displayList(phrasesHighlighted);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (allTextLen == 0)
            {
                return;
            }
            
            // todo: exclude highlighted word from all the text

            DateTime thisDay = DateTime.Today.Date;

            System.Data.SqlClient.SqlConnection connectionWord =
                new System.Data.SqlClient.SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;" + 
                "AttachDbFilename=C:\\Users\\yunzh\\vc_projects\\Figge\\Figge\\repository.mdf;" + 
                "Integrated Security=True;" +
                "MultipleActiveResultSets=true");

            connectionWord.Open();

            // data base query
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = connectionWord;

            cmd.CommandText = "SELECT * FROM Words"; // todo: optimize, only select highlighted
     
            List<string> listWordsRead = new List<string>();
            List<int> listIDRead = new List<int>();
            List<int> listLevelRead = new List<int>();
            List<int> listLevelWrite = new List<int>();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    listWordsRead.Add(reader["word"].ToString());
                    listIDRead.Add(Convert.ToInt32(reader["wordId"]));
                    listLevelRead.Add(Convert.ToInt32(reader["levelRead"]));
                    listLevelWrite.Add(Convert.ToInt32(reader["levelWrite"]));
                }
            }
            reader.Close();

            // update the highlighted word in database
            cmd.CommandText = "UPDATE Words SET levelRead=@newlevelRead, highlighted=1, dateHighlight=@dateH WHERE wordId=@wordIdTemp";
            cmd.Parameters.AddWithValue("@dateH", thisDay);
            for (int i = wordsHighlighted.Count - 1; i >= 0; i--)
            {
                // prepare record
                string wordInput = wordsHighlighted[i];
                int wordIndex = listWordsRead.FindIndex(x => x == wordInput);
                if (-1 != wordIndex)
                {
                    cmd.Parameters.AddWithValue("@newlevelRead", listLevelRead[wordIndex] + 1);
                    cmd.Parameters.AddWithValue("@wordIdTemp", listIDRead[wordIndex]);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.RemoveAt("@newlevelRead");
                    cmd.Parameters.RemoveAt("@wordIdTemp");
                    wordsHighlighted.RemoveAt(i);
                }
            }

            // insert the highlighted word in database
            cmd.CommandText = "INSERT Words (word, levelRead, levelWrite, dataAdd, highlighted, dateHighlight) " +
                "VALUES (@wordSaveT, 1, 0, @thisDayT, 1, @thisDayT)";
            cmd.Parameters.AddWithValue("@thisDayT", thisDay);

            for (int i = 0;i < wordsHighlighted.Count;i++)
            {
                string wordSave = wordsHighlighted[i];
                cmd.Parameters.AddWithValue("@wordSaveT", wordSave);
                cmd.ExecuteNonQuery();
                cmd.Parameters.RemoveAt("@wordSaveT");
            }

            // update the highlighted phrase in database

            // update all the words
            connectionWord.Close();
        }
    }
}