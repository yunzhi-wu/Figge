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
            this.ClearMark = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBoxInput
            // 
            this.richTextBoxInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxInput.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBoxInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxInput.Location = new System.Drawing.Point(21, 23);
            this.richTextBoxInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBoxInput.Name = "richTextBoxInput";
            this.richTextBoxInput.Size = new System.Drawing.Size(1400, 827);
            this.richTextBoxInput.TabIndex = 0;
            this.richTextBoxInput.Text = "";
            this.richTextBoxInput.SelectionChanged += new System.EventHandler(this.richTextBoxInput_SelectionChanged);
            this.richTextBoxInput.TextChanged += new System.EventHandler(this.richTextBoxInput_TextChanged);
            this.richTextBoxInput.MouseUp += new System.Windows.Forms.MouseEventHandler(this.richTextBoxInput_MouseUp);
            // 
            // buttonNewWords
            // 
            this.buttonNewWords.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonNewWords.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNewWords.Location = new System.Drawing.Point(1429, 40);
            this.buttonNewWords.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonNewWords.Name = "buttonNewWords";
            this.buttonNewWords.Size = new System.Drawing.Size(141, 38);
            this.buttonNewWords.TabIndex = 1;
            this.buttonNewWords.Text = "标注生字";
            this.buttonNewWords.UseVisualStyleBackColor = true;
            this.buttonNewWords.Click += new System.EventHandler(this.buttonNewWords_Click);
            // 
            // buttonNewPhrase
            // 
            this.buttonNewPhrase.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonNewPhrase.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNewPhrase.Location = new System.Drawing.Point(1429, 134);
            this.buttonNewPhrase.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonNewPhrase.Name = "buttonNewPhrase";
            this.buttonNewPhrase.Size = new System.Drawing.Size(141, 38);
            this.buttonNewPhrase.TabIndex = 2;
            this.buttonNewPhrase.Text = "标注新词";
            this.buttonNewPhrase.UseVisualStyleBackColor = true;
            this.buttonNewPhrase.Click += new System.EventHandler(this.buttonNewPhrase_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.Location = new System.Drawing.Point(1429, 374);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(141, 38);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // ClearMark
            // 
            this.ClearMark.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ClearMark.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearMark.Location = new System.Drawing.Point(1429, 229);
            this.ClearMark.Margin = new System.Windows.Forms.Padding(4);
            this.ClearMark.Name = "ClearMark";
            this.ClearMark.Size = new System.Drawing.Size(141, 38);
            this.ClearMark.TabIndex = 4;
            this.ClearMark.Text = "清除标注";
            this.ClearMark.UseVisualStyleBackColor = true;
            this.ClearMark.Click += new System.EventHandler(this.ClearMark_Click);
            // 
            // NewInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1582, 874);
            this.Controls.Add(this.ClearMark);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonNewPhrase);
            this.Controls.Add(this.buttonNewWords);
            this.Controls.Add(this.richTextBoxInput);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "NewInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NewInput";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewInput_FormClosing);
            this.Load += new System.EventHandler(this.NewInput_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxInput;
        private System.Windows.Forms.Button buttonNewWords;
        private System.Windows.Forms.Button buttonNewPhrase;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button ClearMark;
    }
}