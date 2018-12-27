namespace Figge
{
    partial class MemoryCard
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
            this.newWordText = new System.Windows.Forms.TextBox();
            this.newWordRichText = new System.Windows.Forms.RichTextBox();
            this.buttonYes = new System.Windows.Forms.Button();
            this.buttonNo = new System.Windows.Forms.Button();
            this.buttonVeryWell = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // newWordText
            // 
            this.newWordText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.newWordText.BackColor = System.Drawing.SystemColors.Control;
            this.newWordText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.newWordText.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newWordText.Location = new System.Drawing.Point(0, 33);
            this.newWordText.MaxLength = 128;
            this.newWordText.Name = "newWordText";
            this.newWordText.ReadOnly = true;
            this.newWordText.Size = new System.Drawing.Size(800, 61);
            this.newWordText.TabIndex = 0;
            this.newWordText.TabStop = false;
            this.newWordText.Text = "nytt ord";
            this.newWordText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // newWordRichText
            // 
            this.newWordRichText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.newWordRichText.BackColor = System.Drawing.SystemColors.Control;
            this.newWordRichText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.newWordRichText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newWordRichText.Location = new System.Drawing.Point(0, 122);
            this.newWordRichText.Name = "newWordRichText";
            this.newWordRichText.ReadOnly = true;
            this.newWordRichText.Size = new System.Drawing.Size(800, 174);
            this.newWordRichText.TabIndex = 1;
            this.newWordRichText.TabStop = false;
            this.newWordRichText.Text = "nytt ord är fint";
            this.newWordRichText.Visible = false;
            // 
            // buttonYes
            // 
            this.buttonYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonYes.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonYes.ForeColor = System.Drawing.Color.Blue;
            this.buttonYes.Location = new System.Drawing.Point(481, 387);
            this.buttonYes.Name = "buttonYes";
            this.buttonYes.Size = new System.Drawing.Size(130, 40);
            this.buttonYes.TabIndex = 2;
            this.buttonYes.Text = "认识";
            this.buttonYes.UseVisualStyleBackColor = true;
            // 
            // buttonNo
            // 
            this.buttonNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonNo.Location = new System.Drawing.Point(638, 387);
            this.buttonNo.Name = "buttonNo";
            this.buttonNo.Size = new System.Drawing.Size(130, 40);
            this.buttonNo.TabIndex = 3;
            this.buttonNo.Text = "不认识";
            this.buttonNo.UseVisualStyleBackColor = true;
            // 
            // buttonVeryWell
            // 
            this.buttonVeryWell.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonVeryWell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonVeryWell.Location = new System.Drawing.Point(324, 387);
            this.buttonVeryWell.Name = "buttonVeryWell";
            this.buttonVeryWell.Size = new System.Drawing.Size(130, 40);
            this.buttonVeryWell.TabIndex = 4;
            this.buttonVeryWell.Text = "非常熟悉";
            this.buttonVeryWell.UseVisualStyleBackColor = true;
            // 
            // MemoryCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonVeryWell);
            this.Controls.Add(this.buttonNo);
            this.Controls.Add(this.buttonYes);
            this.Controls.Add(this.newWordRichText);
            this.Controls.Add(this.newWordText);
            this.MaximizeBox = false;
            this.Name = "MemoryCard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MemoryCard";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MemoryCard_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox newWordText;
        private System.Windows.Forms.RichTextBox newWordRichText;
        private System.Windows.Forms.Button buttonYes;
        private System.Windows.Forms.Button buttonNo;
        private System.Windows.Forms.Button buttonVeryWell;
    }
}