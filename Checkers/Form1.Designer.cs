namespace Checkers
{
    partial class checkers
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
            this.Blacktakencounter = new System.Windows.Forms.TextBox();
            this.Blackcounter = new System.Windows.Forms.Label();
            this.Redtakencounter = new System.Windows.Forms.TextBox();
            this.Redcounter = new System.Windows.Forms.Label();
            this.Turncounter = new System.Windows.Forms.TextBox();
            this.Turnlabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Blacktakencounter
            // 
            this.Blacktakencounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Blacktakencounter.Location = new System.Drawing.Point(588, 355);
            this.Blacktakencounter.Name = "Blacktakencounter";
            this.Blacktakencounter.ReadOnly = true;
            this.Blacktakencounter.Size = new System.Drawing.Size(33, 31);
            this.Blacktakencounter.TabIndex = 23;
            this.Blacktakencounter.Text = "20";
            // 
            // Blackcounter
            // 
            this.Blackcounter.AutoSize = true;
            this.Blackcounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Blackcounter.Location = new System.Drawing.Point(543, 288);
            this.Blackcounter.Name = "Blackcounter";
            this.Blackcounter.Size = new System.Drawing.Size(182, 16);
            this.Blackcounter.TabIndex = 22;
            this.Blackcounter.Text = "Black Counter Remaining";
            // 
            // Redtakencounter
            // 
            this.Redtakencounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Redtakencounter.Location = new System.Drawing.Point(588, 225);
            this.Redtakencounter.Name = "Redtakencounter";
            this.Redtakencounter.ReadOnly = true;
            this.Redtakencounter.Size = new System.Drawing.Size(33, 31);
            this.Redtakencounter.TabIndex = 21;
            this.Redtakencounter.Text = "20";
            // 
            // Redcounter
            // 
            this.Redcounter.AutoSize = true;
            this.Redcounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Redcounter.Location = new System.Drawing.Point(543, 168);
            this.Redcounter.Name = "Redcounter";
            this.Redcounter.Size = new System.Drawing.Size(172, 16);
            this.Redcounter.TabIndex = 20;
            this.Redcounter.Text = "Red Counter Remaining";
            // 
            // Turncounter
            // 
            this.Turncounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Turncounter.Location = new System.Drawing.Point(577, 92);
            this.Turncounter.Name = "Turncounter";
            this.Turncounter.ReadOnly = true;
            this.Turncounter.Size = new System.Drawing.Size(73, 31);
            this.Turncounter.TabIndex = 19;
            this.Turncounter.Text = "BLACK";
            // 
            // Turnlabel
            // 
            this.Turnlabel.AutoSize = true;
            this.Turnlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Turnlabel.Location = new System.Drawing.Point(584, 47);
            this.Turnlabel.Name = "Turnlabel";
            this.Turnlabel.Size = new System.Drawing.Size(60, 24);
            this.Turnlabel.TabIndex = 18;
            this.Turnlabel.Text = "Turn ";
            // 
            // checkers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 518);
            this.Controls.Add(this.Blacktakencounter);
            this.Controls.Add(this.Blackcounter);
            this.Controls.Add(this.Redtakencounter);
            this.Controls.Add(this.Redcounter);
            this.Controls.Add(this.Turncounter);
            this.Controls.Add(this.Turnlabel);
            this.Name = "checkers";
            this.Text = "Checkers";
            this.Load += new System.EventHandler(this.checkers_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Blacktakencounter;
        private System.Windows.Forms.Label Blackcounter;
        private System.Windows.Forms.TextBox Redtakencounter;
        private System.Windows.Forms.Label Redcounter;
        private System.Windows.Forms.TextBox Turncounter;
        private System.Windows.Forms.Label Turnlabel;
    }
}