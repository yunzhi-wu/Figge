﻿namespace Figge
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
            this.buttonYes = new System.Windows.Forms.Button();
            this.buttonNo = new System.Windows.Forms.Button();
            this.buttonVeryWell = new System.Windows.Forms.Button();
            this.progressBarFamiliarity = new System.Windows.Forms.ProgressBar();
            this.webBrowserContext = new System.Windows.Forms.WebBrowser();
            this.checkBoxContext = new System.Windows.Forms.CheckBox();
            this.buttonGetNewSet = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // newWordText
            // 
            this.newWordText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.newWordText.BackColor = System.Drawing.SystemColors.Control;
            this.newWordText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.newWordText.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newWordText.Location = new System.Drawing.Point(0, 27);
            this.newWordText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.newWordText.MaxLength = 128;
            this.newWordText.Multiline = true;
            this.newWordText.Name = "newWordText";
            this.newWordText.ReadOnly = true;
            this.newWordText.Size = new System.Drawing.Size(736, 61);
            this.newWordText.TabIndex = 0;
            this.newWordText.TabStop = false;
            this.newWordText.Text = "nytt ord";
            this.newWordText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonYes
            // 
            this.buttonYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonYes.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonYes.ForeColor = System.Drawing.Color.Blue;
            this.buttonYes.Location = new System.Drawing.Point(367, 397);
            this.buttonYes.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonYes.Name = "buttonYes";
            this.buttonYes.Size = new System.Drawing.Size(98, 32);
            this.buttonYes.TabIndex = 2;
            this.buttonYes.Text = "认识";
            this.buttonYes.UseVisualStyleBackColor = true;
            this.buttonYes.Click += new System.EventHandler(this.buttonYes_Click);
            // 
            // buttonNo
            // 
            this.buttonNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonNo.Location = new System.Drawing.Point(506, 397);
            this.buttonNo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonNo.Name = "buttonNo";
            this.buttonNo.Size = new System.Drawing.Size(98, 32);
            this.buttonNo.TabIndex = 3;
            this.buttonNo.Text = "不认识";
            this.buttonNo.UseVisualStyleBackColor = true;
            this.buttonNo.Click += new System.EventHandler(this.buttonNo_Click);
            // 
            // buttonVeryWell
            // 
            this.buttonVeryWell.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonVeryWell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonVeryWell.Location = new System.Drawing.Point(228, 397);
            this.buttonVeryWell.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonVeryWell.Name = "buttonVeryWell";
            this.buttonVeryWell.Size = new System.Drawing.Size(98, 32);
            this.buttonVeryWell.TabIndex = 1;
            this.buttonVeryWell.Text = "非常熟悉";
            this.buttonVeryWell.UseVisualStyleBackColor = true;
            this.buttonVeryWell.Click += new System.EventHandler(this.buttonVeryWell_Click);
            // 
            // progressBarFamiliarity
            // 
            this.progressBarFamiliarity.Location = new System.Drawing.Point(16, 358);
            this.progressBarFamiliarity.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.progressBarFamiliarity.Name = "progressBarFamiliarity";
            this.progressBarFamiliarity.Size = new System.Drawing.Size(709, 19);
            this.progressBarFamiliarity.TabIndex = 0;
            // 
            // webBrowserContext
            // 
            this.webBrowserContext.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowserContext.Location = new System.Drawing.Point(0, 109);
            this.webBrowserContext.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.webBrowserContext.MinimumSize = new System.Drawing.Size(15, 16);
            this.webBrowserContext.Name = "webBrowserContext";
            this.webBrowserContext.ScrollBarsEnabled = false;
            this.webBrowserContext.Size = new System.Drawing.Size(736, 229);
            this.webBrowserContext.TabIndex = 0;
            this.webBrowserContext.Visible = false;
            this.webBrowserContext.WebBrowserShortcutsEnabled = false;
            // 
            // checkBoxContext
            // 
            this.checkBoxContext.AutoSize = true;
            this.checkBoxContext.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxContext.Location = new System.Drawing.Point(645, 397);
            this.checkBoxContext.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBoxContext.Name = "checkBoxContext";
            this.checkBoxContext.Size = new System.Drawing.Size(86, 28);
            this.checkBoxContext.TabIndex = 4;
            this.checkBoxContext.Text = "上下文";
            this.checkBoxContext.UseVisualStyleBackColor = true;
            this.checkBoxContext.CheckedChanged += new System.EventHandler(this.checkBoxContext_CheckedChanged);
            // 
            // buttonGetNewSet
            // 
            this.buttonGetNewSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGetNewSet.Location = new System.Drawing.Point(16, 397);
            this.buttonGetNewSet.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonGetNewSet.Name = "buttonGetNewSet";
            this.buttonGetNewSet.Size = new System.Drawing.Size(98, 32);
            this.buttonGetNewSet.TabIndex = 5;
            this.buttonGetNewSet.Text = "换一组";
            this.buttonGetNewSet.UseVisualStyleBackColor = true;
            this.buttonGetNewSet.Click += new System.EventHandler(this.buttonGetNewSet_Click);
            // 
            // MemoryCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 449);
            this.Controls.Add(this.buttonGetNewSet);
            this.Controls.Add(this.checkBoxContext);
            this.Controls.Add(this.webBrowserContext);
            this.Controls.Add(this.progressBarFamiliarity);
            this.Controls.Add(this.buttonVeryWell);
            this.Controls.Add(this.buttonNo);
            this.Controls.Add(this.buttonYes);
            this.Controls.Add(this.newWordText);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
        private System.Windows.Forms.Button buttonYes;
        private System.Windows.Forms.Button buttonNo;
        private System.Windows.Forms.Button buttonVeryWell;
        private System.Windows.Forms.ProgressBar progressBarFamiliarity;
        private System.Windows.Forms.WebBrowser webBrowserContext;
        private System.Windows.Forms.CheckBox checkBoxContext;
        private System.Windows.Forms.Button buttonGetNewSet;
    }
}