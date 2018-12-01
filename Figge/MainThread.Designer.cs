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
            this.SuspendLayout();
            // 
            // AddNewThings
            // 
            this.AddNewThings.Location = new System.Drawing.Point(205, 125);
            this.AddNewThings.Name = "AddNewThings";
            this.AddNewThings.Size = new System.Drawing.Size(200, 124);
            this.AddNewThings.TabIndex = 0;
            this.AddNewThings.Text = "学习新内容";
            this.AddNewThings.UseVisualStyleBackColor = true;
            this.AddNewThings.Click += new System.EventHandler(this.AddNewThings_Click);
            // 
            // Review
            // 
            this.Review.Location = new System.Drawing.Point(579, 125);
            this.Review.Name = "Review";
            this.Review.Size = new System.Drawing.Size(200, 124);
            this.Review.TabIndex = 1;
            this.Review.Text = "复习旧知识";
            this.Review.UseVisualStyleBackColor = true;
            this.Review.Click += new System.EventHandler(this.Review_Click);
            // 
            // Test
            // 
            this.Test.Location = new System.Drawing.Point(205, 356);
            this.Test.Name = "Test";
            this.Test.Size = new System.Drawing.Size(200, 124);
            this.Test.TabIndex = 2;
            this.Test.Text = "听写和考试";
            this.Test.UseVisualStyleBackColor = true;
            this.Test.Click += new System.EventHandler(this.button2_Click);
            // 
            // Statistics
            // 
            this.Statistics.Location = new System.Drawing.Point(579, 356);
            this.Statistics.Name = "Statistics";
            this.Statistics.Size = new System.Drawing.Size(200, 124);
            this.Statistics.TabIndex = 3;
            this.Statistics.Text = "统计信息";
            this.Statistics.UseVisualStyleBackColor = true;
            // 
            // UserStatus
            // 
            this.UserStatus.BackColor = System.Drawing.SystemColors.Control;
            this.UserStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UserStatus.Location = new System.Drawing.Point(205, 41);
            this.UserStatus.Name = "UserStatus";
            this.UserStatus.Size = new System.Drawing.Size(300, 15);
            this.UserStatus.TabIndex = 4;
            // 
            // MainThread
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 571);
            this.Controls.Add(this.UserStatus);
            this.Controls.Add(this.Statistics);
            this.Controls.Add(this.Test);
            this.Controls.Add(this.Review);
            this.Controls.Add(this.AddNewThings);
            this.Name = "MainThread";
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
    }
}