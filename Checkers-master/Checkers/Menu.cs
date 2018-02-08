using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace Checkers
{
    public partial class Menu : Form
    {
        WindowsMediaPlayer player = new WindowsMediaPlayer();
        public Menu()
        {
            InitializeComponent();
            player.URL = "gameMusic.wav";
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.PlayBTN = new System.Windows.Forms.Button();
            this.AboutBTN = new System.Windows.Forms.Button();
            this.QuitBTN = new System.Windows.Forms.Button();
            this.TitleLBL = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
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
            this.TitleLBL.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLBL.ForeColor = System.Drawing.Color.White;
            this.TitleLBL.Location = new System.Drawing.Point(166, 19);
            this.TitleLBL.Name = "TitleLBL";
            this.TitleLBL.Size = new System.Drawing.Size(147, 33);
            this.TitleLBL.TabIndex = 3;
            this.TitleLBL.Text = "Checkers";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Info;
            this.label1.Location = new System.Drawing.Point(230, 319);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "by";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Info;
            this.label2.Location = new System.Drawing.Point(152, 337);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(178, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "Sam Glendenning";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Info;
            this.label3.Location = new System.Drawing.Point(221, 361);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = " and";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Info;
            this.label4.Location = new System.Drawing.Point(168, 383);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 24);
            this.label4.TabIndex = 7;
            this.label4.Text = "  Abbas Lawal";
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(-1, 407);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(10, 10);
            this.axWindowsMediaPlayer1.TabIndex = 8;
            // 
            // Menu
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(475, 416);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TitleLBL);
            this.Controls.Add(this.QuitBTN);
            this.Controls.Add(this.AboutBTN);
            this.Controls.Add(this.PlayBTN);
            this.Name = "Menu";
            this.Load += new System.EventHandler(this.Menu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void QuitBTN_Click(object sender, EventArgs e)
        {
            Application.Exit();     //exits the program
        }

        private void PlayBTN_Click(object sender, EventArgs e)
        {
            this.Hide();                //hides menu while game in-play
            new checkers().Show();      //creates new checkers board form

        }

        private void AboutBTN_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Instructions().Show();          //creates new instructions form
        }

        /*
         *  Used to ensure the program properly ends when the user presses the close button at the 
         *  top right of the form
        */
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            player.controls.play();
        }
    }
}
