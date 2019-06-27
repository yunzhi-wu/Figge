namespace Figge
{
    partial class MainThread
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
            this.AddNewThings = new System.Windows.Forms.Button();
            this.Review = new System.Windows.Forms.Button();
            this.Test = new System.Windows.Forms.Button();
            this.Statistics = new System.Windows.Forms.Button();
            this.UserStatus = new System.Windows.Forms.TextBox();
            this.MemorizeCard = new System.Windows.Forms.Button();
            this.ButtonPrintAll = new System.Windows.Forms.Button();
            this.buttonPrintNewWordsCard = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AddNewThings
            // 
            this.AddNewThings.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddNewThings.Location = new System.Drawing.Point(150, 96);
            this.AddNewThings.Margin = new System.Windows.Forms.Padding(2);
            this.AddNewThings.Name = "AddNewThings";
            this.AddNewThings.Size = new System.Drawing.Size(150, 101);
            this.AddNewThings.TabIndex = 0;
            this.AddNewThings.Text = "学习新内容";
            this.AddNewThings.UseVisualStyleBackColor = true;
            this.AddNewThings.Click += new System.EventHandler(this.AddNewThings_Click);
            // 
            // Review
            // 
            this.Review.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Review.Location = new System.Drawing.Point(450, 96);
            this.Review.Margin = new System.Windows.Forms.Padding(2);
            this.Review.Name = "Review";
            this.Review.Size = new System.Drawing.Size(150, 101);
            this.Review.TabIndex = 1;
            this.Review.Text = "显示全部";
            this.Review.UseVisualStyleBackColor = true;
            this.Review.Click += new System.EventHandler(this.Review_Click);
            // 
            // Test
            // 
            this.Test.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Test.Location = new System.Drawing.Point(150, 297);
            this.Test.Margin = new System.Windows.Forms.Padding(2);
            this.Test.Name = "Test";
            this.Test.Size = new System.Drawing.Size(150, 101);
            this.Test.TabIndex = 2;
            this.Test.Text = "听写和考试";
            this.Test.UseVisualStyleBackColor = true;
            this.Test.Click += new System.EventHandler(this.button2_Click);
            // 
            // Statistics
            // 
            this.Statistics.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Statistics.Location = new System.Drawing.Point(450, 297);
            this.Statistics.Margin = new System.Windows.Forms.Padding(2);
            this.Statistics.Name = "Statistics";
            this.Statistics.Size = new System.Drawing.Size(150, 101);
            this.Statistics.TabIndex = 3;
            this.Statistics.Text = "统计信息";
            this.Statistics.UseVisualStyleBackColor = true;
            this.Statistics.Click += new System.EventHandler(this.Statistics_Click);
            // 
            // UserStatus
            // 
            this.UserStatus.BackColor = System.Drawing.SystemColors.Control;
            this.UserStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UserStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserStatus.Location = new System.Drawing.Point(154, 33);
            this.UserStatus.Margin = new System.Windows.Forms.Padding(2);
            this.UserStatus.MaxLength = 256;
            this.UserStatus.Name = "UserStatus";
            this.UserStatus.Size = new System.Drawing.Size(446, 19);
            this.UserStatus.TabIndex = 4;
            this.UserStatus.TabStop = false;
            // 
            // MemorizeCard
            // 
            this.MemorizeCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MemorizeCard.Location = new System.Drawing.Point(300, 197);
            this.MemorizeCard.Margin = new System.Windows.Forms.Padding(2);
            this.MemorizeCard.Name = "MemorizeCard";
            this.MemorizeCard.Size = new System.Drawing.Size(150, 101);
            this.MemorizeCard.TabIndex = 5;
            this.MemorizeCard.Text = "单词卡";
            this.MemorizeCard.UseVisualStyleBackColor = true;
            this.MemorizeCard.Click += new System.EventHandler(this.MemorizeCard_Click);
            // 
            // ButtonPrintAll
            // 
            this.ButtonPrintAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonPrintAll.Location = new System.Drawing.Point(450, 197);
            this.ButtonPrintAll.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonPrintAll.Name = "ButtonPrintAll";
            this.ButtonPrintAll.Size = new System.Drawing.Size(150, 101);
            this.ButtonPrintAll.TabIndex = 6;
            this.ButtonPrintAll.Text = "打印全部";
            this.ButtonPrintAll.UseVisualStyleBackColor = true;
            this.ButtonPrintAll.Click += new System.EventHandler(this.ButtonPrintAll_Click);
            // 
            // buttonPrintNewWordsCard
            // 
            this.buttonPrintNewWordsCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPrintNewWordsCard.Location = new System.Drawing.Point(300, 297);
            this.buttonPrintNewWordsCard.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPrintNewWordsCard.Name = "buttonPrintNewWordsCard";
            this.buttonPrintNewWordsCard.Size = new System.Drawing.Size(150, 101);
            this.buttonPrintNewWordsCard.TabIndex = 7;
            this.buttonPrintNewWordsCard.Text = "打印单词卡";
            this.buttonPrintNewWordsCard.UseVisualStyleBackColor = true;
            this.buttonPrintNewWordsCard.Click += new System.EventHandler(this.buttonPrintNewWordsCard_Click);
            // 
            // MainThread
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 449);
            this.Controls.Add(this.buttonPrintNewWordsCard);
            this.Controls.Add(this.ButtonPrintAll);
            this.Controls.Add(this.MemorizeCard);
            this.Controls.Add(this.UserStatus);
            this.Controls.Add(this.Statistics);
            this.Controls.Add(this.Test);
            this.Controls.Add(this.Review);
            this.Controls.Add(this.AddNewThings);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainThread";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainThread";
            this.Load += new System.EventHandler(this.MainThread_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AddNewThings;
        private System.Windows.Forms.Button Review;
        private System.Windows.Forms.Button Test;
        private System.Windows.Forms.Button Statistics;
        private System.Windows.Forms.TextBox UserStatus;
        private System.Windows.Forms.Button MemorizeCard;
        private System.Windows.Forms.Button ButtonPrintAll;
        private System.Windows.Forms.Button buttonPrintNewWordsCard;
    }
}