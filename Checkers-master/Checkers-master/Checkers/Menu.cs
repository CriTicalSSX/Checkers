using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checkers
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void InitializeComponent()
        {
            this.PlayBTN = new System.Windows.Forms.Button();
            this.AboutBTN = new System.Windows.Forms.Button();
            this.QuitBTN = new System.Windows.Forms.Button();
            this.TitleLBL = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PlayBTN
            // 
            this.PlayBTN.BackColor = System.Drawing.Color.Transparent;
            this.PlayBTN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PlayBTN.Cursor = System.Windows.Forms.Cursors.Default;
            this.PlayBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayBTN.ForeColor = System.Drawing.Color.Black;
            this.PlayBTN.Location = new System.Drawing.Point(156, 88);
            this.PlayBTN.Name = "PlayBTN";
            this.PlayBTN.Size = new System.Drawing.Size(167, 49);
            this.PlayBTN.TabIndex = 0;
            this.PlayBTN.Text = "Play";
            this.PlayBTN.UseVisualStyleBackColor = false;
            this.PlayBTN.Click += new System.EventHandler(this.PlayBTN_Click);
            // 
            // AboutBTN
            // 
            this.AboutBTN.BackColor = System.Drawing.Color.Transparent;
            this.AboutBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AboutBTN.Location = new System.Drawing.Point(156, 181);
            this.AboutBTN.Name = "AboutBTN";
            this.AboutBTN.Size = new System.Drawing.Size(167, 49);
            this.AboutBTN.TabIndex = 1;
            this.AboutBTN.Text = "Instructions";
            this.AboutBTN.UseVisualStyleBackColor = false;
            this.AboutBTN.Click += new System.EventHandler(this.AboutBTN_Click);
            // 
            // QuitBTN
            // 
            this.QuitBTN.BackColor = System.Drawing.Color.Transparent;
            this.QuitBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuitBTN.Location = new System.Drawing.Point(156, 267);
            this.QuitBTN.Name = "QuitBTN";
            this.QuitBTN.Size = new System.Drawing.Size(167, 49);
            this.QuitBTN.TabIndex = 2;
            this.QuitBTN.Text = "Quit";
            this.QuitBTN.UseVisualStyleBackColor = false;
            this.QuitBTN.Click += new System.EventHandler(this.QuitBTN_Click);
            // 
            // TitleLBL
            // 
            this.TitleLBL.AutoSize = true;
            this.TitleLBL.Font = new System.Drawing.Font("Axure Handwriting", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLBL.ForeColor = System.Drawing.Color.White;
            this.TitleLBL.Location = new System.Drawing.Point(166, 19);
            this.TitleLBL.Name = "TitleLBL";
            this.TitleLBL.Size = new System.Drawing.Size(145, 33);
            this.TitleLBL.TabIndex = 3;
            this.TitleLBL.Text = "Checkers";
            this.TitleLBL.Click += new System.EventHandler(this.label1_Click);
            // 
            // Menu
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(475, 416);
            this.Controls.Add(this.TitleLBL);
            this.Controls.Add(this.QuitBTN);
            this.Controls.Add(this.AboutBTN);
            this.Controls.Add(this.PlayBTN);
            this.Name = "Menu";
            this.Load += new System.EventHandler(this.Menu_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void QuitBTN_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PlayBTN_Click(object sender, EventArgs e)
        {
            this.Hide();
            new checkers().Show();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Menu_Load_1(object sender, EventArgs e)
        {

        }

        private void AboutBTN_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Instructions().Show();
        }
    }
}
