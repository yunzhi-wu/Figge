namespace Figge
{
    partial class NewInput
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBoxInput = new System.Windows.Forms.RichTextBox();
            this.buttonNewWords = new System.Windows.Forms.Button();
            this.buttonNewPhrase = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBoxInput
            // 
            this.richTextBoxInput.Location = new System.Drawing.Point(22, 23);
            this.richTextBoxInput.Name = "richTextBoxInput";
            this.richTextBoxInput.Size = new System.Drawing.Size(815, 524);
            this.richTextBoxInput.TabIndex = 0;
            this.richTextBoxInput.Text = "";
            this.richTextBoxInput.SelectionChanged += new System.EventHandler(this.richTextBoxInput_SelectionChanged);
            this.richTextBoxInput.TextChanged += new System.EventHandler(this.richTextBoxInput_TextChanged);
            this.richTextBoxInput.MouseUp += new System.Windows.Forms.MouseEventHandler(this.richTextBoxInput_MouseUp);
            // 
            // buttonNewWords
            // 
            this.buttonNewWords.Location = new System.Drawing.Point(861, 94);
            this.buttonNewWords.Name = "buttonNewWords";
            this.buttonNewWords.Size = new System.Drawing.Size(99, 41);
            this.buttonNewWords.TabIndex = 1;
            this.buttonNewWords.Text = "New Words";
            this.buttonNewWords.UseVisualStyleBackColor = true;
            this.buttonNewWords.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonNewPhrase
            // 
            this.buttonNewPhrase.Location = new System.Drawing.Point(861, 187);
            this.buttonNewPhrase.Name = "buttonNewPhrase";
            this.buttonNewPhrase.Size = new System.Drawing.Size(99, 41);
            this.buttonNewPhrase.TabIndex = 2;
            this.buttonNewPhrase.Text = "New Phrase";
            this.buttonNewPhrase.UseVisualStyleBackColor = true;
            this.buttonNewPhrase.Click += new System.EventHandler(this.buttonNewPhrase_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(861, 282);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(99, 41);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // NewInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 571);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonNewPhrase);
            this.Controls.Add(this.buttonNewWords);
            this.Controls.Add(this.richTextBoxInput);
            this.Name = "NewInput";
            this.Text = "NewInput";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewInput_FormClosing);
            this.Load += new System.EventHandler(this.NewInput_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxInput;
        private System.Windows.Forms.Button buttonNewWords;
        private System.Windows.Forms.Button buttonNewPhrase;
        private System.Windows.Forms.Button buttonSave;
    }
}