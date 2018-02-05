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
            this.SuspendLayout();
            // 
            // PlayBTN
            // 
            this.PlayBTN.Location = new System.Drawing.Point(156, 78);
            this.PlayBTN.Name = "PlayBTN";
            this.PlayBTN.Size = new System.Drawing.Size(167, 49);
            this.PlayBTN.TabIndex = 0;
            this.PlayBTN.Text = "Play";
            this.PlayBTN.UseVisualStyleBackColor = true;
            this.PlayBTN.Click += new System.EventHandler(this.PlayBTN_Click);
            // 
            // AboutBTN
            // 
            this.AboutBTN.Location = new System.Drawing.Point(156, 150);
            this.AboutBTN.Name = "AboutBTN";
            this.AboutBTN.Size = new System.Drawing.Size(167, 49);
            this.AboutBTN.TabIndex = 1;
            this.AboutBTN.Text = "About";
            this.AboutBTN.UseVisualStyleBackColor = true;
            // 
            // QuitBTN
            // 
            this.QuitBTN.Location = new System.Drawing.Point(156, 227);
            this.QuitBTN.Name = "QuitBTN";
            this.QuitBTN.Size = new System.Drawing.Size(167, 49);
            this.QuitBTN.TabIndex = 2;
            this.QuitBTN.Text = "Quit";
            this.QuitBTN.UseVisualStyleBackColor = true;
            this.QuitBTN.Click += new System.EventHandler(this.QuitBTN_Click);
            // 
            // Menu
            // 
            this.ClientSize = new System.Drawing.Size(475, 416);
            this.Controls.Add(this.QuitBTN);
            this.Controls.Add(this.AboutBTN);
            this.Controls.Add(this.PlayBTN);
            this.Name = "Menu";
            this.ResumeLayout(false);

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
    }
}
