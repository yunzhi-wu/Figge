namespace Figge
{
    partial class Statistics
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
            this.labelNewWords = new System.Windows.Forms.Label();
            this.labelFishedWords = new System.Windows.Forms.Label();
            this.NrofWords = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelNewWords
            // 
            this.labelNewWords.AutoSize = true;
            this.labelNewWords.Location = new System.Drawing.Point(50, 30);
            this.labelNewWords.Name = "labelNewWords";
            this.labelNewWords.Size = new System.Drawing.Size(103, 17);
            this.labelNewWords.TabIndex = 0;
            this.labelNewWords.Text = "NrofNewWords";
            this.labelNewWords.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelFishedWords
            // 
            this.labelFishedWords.AutoSize = true;
            this.labelFishedWords.Location = new System.Drawing.Point(50, 210);
            this.labelFishedWords.Name = "labelFishedWords";
            this.labelFishedWords.Size = new System.Drawing.Size(132, 17);
            this.labelFishedWords.TabIndex = 1;
            this.labelFishedWords.Text = "labelFinishedWords";
            this.labelFishedWords.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NrofWords
            // 
            this.NrofWords.AutoSize = true;
            this.NrofWords.Location = new System.Drawing.Point(50, 390);
            this.NrofWords.Name = "NrofWords";
            this.NrofWords.Size = new System.Drawing.Size(76, 17);
            this.NrofWords.TabIndex = 2;
            this.NrofWords.Text = "NrofWords";
            this.NrofWords.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Statistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 553);
            this.Controls.Add(this.NrofWords);
            this.Controls.Add(this.labelFishedWords);
            this.Controls.Add(this.labelNewWords);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Statistics";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Statistics";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Statistics_FormClosed);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Statistics_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelNewWords;
        private System.Windows.Forms.Label labelFishedWords;
        private System.Windows.Forms.Label NrofWords;
    }
}