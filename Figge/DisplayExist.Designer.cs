namespace Figge
{
    partial class DisplayExist
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
            this.webDisplay = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webDisplay
            // 
            this.webDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webDisplay.Location = new System.Drawing.Point(0, 0);
            this.webDisplay.MinimumSize = new System.Drawing.Size(20, 20);
            this.webDisplay.Name = "webDisplay";
            this.webDisplay.Size = new System.Drawing.Size(1441, 633);
            this.webDisplay.TabIndex = 0;
            this.webDisplay.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webDisplay_DocumentCompleted);
            // 
            // DisplayExist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1441, 633);
            this.Controls.Add(this.webDisplay);
            this.Name = "DisplayExist";
            this.Text = "Display Existing";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DisplayExist_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webDisplay;
    }
}