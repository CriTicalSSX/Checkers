
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Checkers
{
    public partial class Form1 : Form
    {
        Square[,] squareArray = new Square[10, 10];       // Create 2D array of buttons
        Square firstClick, secondClick, leftCounterToRemove, rightCounterToRemove;
        bool blackTurn = true;
        int redCountersTaken = 0;
        int blackCountersTaken = 0;

        public Form1()
        {
            InitializeComponent();
            BuildBoard();
        }

        void BuildBoard()
        {
            for (int x = 0; x < squareArray.GetLength(0); x++)         // Loop for x
            {
                for (int y = 0; y < squareArray.GetLength(1); y++)     // Loop for y
                {
                    squareArray[x, y] = new Square(x, y);
                    squareArray[x, y].SetBounds(50 * x, 50 * y, 55, 55);
                    squareArray[x, y].setOccupied(false);

                    if ((x % 2 == 0 && y % 2 == 0) || (x % 2 == 1 && y % 2 == 1))
                    {
                        squareArray[x, y].BackColor = Color.Red;
                    }
                    else
                    {
                        squareArray[x, y].BackColor = Color.DarkSlateGray;

                        if (y >= 0 && y <= 3)
                        {
                            squareArray[x, y].setRed(true);
                            squareArray[x, y].setOccupied(true);
                        }
                        if (y >= 6 && y <= 10)
                        {
                            squareArray[x, y].setRed(false);
                            squareArray[x, y].setOccupied(true);
                        }
                    }

                    squareArray[x, y].setKing(false);
                    //squareArray[x, y].Text = Convert.ToString((x + 1) + "," + (y + 1));
                    squareArray[x, y].Click += new EventHandler(this.squareEvent_Click);
                    Controls.Add(squareArray[x, y]);
                }
            }
        }

        void squareEvent_Click(object sender, EventArgs e)
        {
            if (blackTurn == true)
            {
                MessageBox.Show("Is black's turn");
            }
            else
            {
                MessageBox.Show("Is red's turn");
            }

            Square square = sender as Square;

            if (firstClick == null)
            {
                if (blackTurn && square.BackColor != Color.Red && square.isOccupied())
                {
                    square.setRed(false);
                    square.setCurrent();
                    firstClick = square;
                    MessageBox.Show("Black selected");
                }
                else if (!blackTurn && square.BackColor != Color.Red && square.isOccupied())
                {
                    square.setRed(true);
                    square.setCurrent();
                    firstClick = square;
                    MessageBox.Show("Red selected");
                }
                else
                {
                    firstClick = null;
                }
            }
            else
            {
                secondClick = square;

                //BLACK'S TURN
                if (blackTurn)
                {
                    /*Square left = squareArray[firstClick.getX() - 1, firstClick.getY() - 1];
                    Square right = squareArray[firstClick.getX() + 1, firstClick.getY() - 1];

                    if (secondClick != left && secondClick != right)
                    {
                        MessageBox.Show("Must be left or right");
                        firstClick = null;
                        secondClick = null;
                        firstClick.removeCurrent();
                    }
                    else
                    {*/
                        if (canBlackMove())
                        {
                            secondClick.BackgroundImage = Properties.Resources.Black_Checker;
                            square.BackColor = Color.DarkSlateGray;
                            secondClick.setKing(square.isKing());
                            secondClick.setRed(false);
                            square.setOccupied(false);
                            square.setKing(false);
                            //secondClick.Text = square.Text;
                            //square.Text = "";
                            secondClick.setOccupied(true);
                            firstClick.removeCurrent();
                            firstClick.setOccupied(false);
                            firstClick.BackgroundImage = Properties.Resources.Greyback;
                            MessageBox.Show("Moved");
                            switchTurn();
                        }
                        else
                        {
                            MessageBox.Show("Cannot make this move.");
                            firstClick = null;
                            secondClick = null;                           
                        }
                    }
               // }

                //RED'S TURN
                else
                {
                    /*Square left = squareArray[firstClick.getX() + 1, firstClick.getY() + 1];
                    Square right = squareArray[firstClick.getX() - 1, firstClick.getY() + 1];

                    if (secondClick != left && secondClick != right)
                    {
                        MessageBox.Show("Must be left or right");
                        firstClick = null;
                        secondClick = null;
                        firstClick.removeCurrent();
                    }
                    else
                    {*/
                        if (canRedMove())
                        {
                            secondClick.BackgroundImage = Properties.Resources.Red_Checker;
                            square.BackColor = Color.DarkSlateGray;
                            secondClick.setKing(square.isKing());
                            secondClick.setRed(true);
                            square.setOccupied(false);
                            square.setKing(false);
                            //secondClick.Text = square.Text;
                            //square.Text = "";
                            secondClick.setOccupied(true);
                            firstClick.removeCurrent();
                            firstClick.setOccupied(false);
                            firstClick.BackgroundImage = Properties.Resources.Greyback;
                            MessageBox.Show("Moved");
                            switchTurn();
                        }
                        else
                        {
                            MessageBox.Show("Cannot make this move.");
                            firstClick = null;
                            secondClick = null;
                        }
                   // }
                }
            }
        }

        public bool canBlackMove()
        {
            bool leftTaken = false;
            bool rightTaken = false;

            Square diagonalLeft;

            if (firstClick.getX() == 0)
            {
                diagonalLeft = null;        //counter on left side of board
            }
            else
            {
                diagonalLeft = squareArray[firstClick.getX() - 1, firstClick.getY() - 1];

                if (diagonalLeft.isOccupied())
                {
                    if (diagonalLeft.isRed() && diagonalLeft.getX() != 0)
                    {
                        leftCounterToRemove = diagonalLeft;
                        diagonalLeft = squareArray[diagonalLeft.getX() - 1, diagonalLeft.getY() - 1];
                        leftTaken = true;

                        if (diagonalLeft.isOccupied())
                        {
                            diagonalLeft = null;
                        }
                    }
                    else
                    {
                        diagonalLeft = null;
                    }
                }
            }

            Square diagonalRight;

            if (firstClick.getX() == 9)
            {
                diagonalRight = null;
            }
            else
            {
                diagonalRight = squareArray[firstClick.getX() + 1, firstClick.getY() - 1];

                if (diagonalRight.isOccupied())
                {
                    if (diagonalRight.isRed() && diagonalRight.getX() != 9)
                    {
                        rightCounterToRemove = diagonalRight;
                        diagonalRight = squareArray[diagonalRight.getX() + 1, diagonalRight.getY() - 1];
                        rightTaken = true;

                        if (diagonalRight.isOccupied())
                        {
                            diagonalRight = null;
                        }
                    }
                    else
                    {
                        diagonalRight = null;
                    }
                }
            }

            if (diagonalLeft == null && diagonalRight == null)
            {
                firstClick.removeCurrent();
                firstClick = null;
                secondClick = null;
                return false;
            }
            else
            {
                if (secondClick == diagonalLeft)
                {
                    MessageBox.Show("Second click is diagonalLeft"); 
                    
                    if (leftTaken)
                    {
                        leftCounterToRemove.setOccupied(false);
                        leftCounterToRemove.setKing(false);
                        leftCounterToRemove.BackgroundImage = Properties.Resources.Greyback;
                        leftCounterToRemove = null;
                    }

                    return true;
                }
                else if (secondClick == diagonalRight)
                {
                    MessageBox.Show("Second click is diagonalRight");

                    if (rightTaken)
                    {
                        rightCounterToRemove.setOccupied(false);
                        rightCounterToRemove.setKing(false);
                        rightCounterToRemove.BackgroundImage = Properties.Resources.Greyback;
                        rightCounterToRemove = null;
                    }

                    return true;
                }
                else
                {
                    firstClick.removeCurrent();
                    secondClick.removeCurrent();
                    firstClick = null;
                    secondClick = null;
                    return false;
                }
            }
        }

        public bool canRedMove()
        {
            bool leftTaken = false;
            bool rightTaken = false;

            Square diagonalLeft;

            if (firstClick.getX() == 9)
            {
                diagonalLeft = null;        //counter on left side of board
            }
            else
            {
                diagonalLeft = squareArray[firstClick.getX() + 1, firstClick.getY() + 1];

                if (diagonalLeft.isOccupied())
                {
                    if (!diagonalLeft.isRed() && diagonalLeft.getX() != 9)
                    {
                        leftCounterToRemove = diagonalLeft;
                        diagonalLeft = squareArray[diagonalLeft.getX() + 1, diagonalLeft.getY() + 1];
                        leftTaken = true;

                        if (diagonalLeft.isOccupied())
                        {
                            diagonalLeft = null;
                        }
                    }
                    else
                    {
                        diagonalLeft = null;
                    }
                }
            }

            Square diagonalRight;

            if (firstClick.getX() == 0)
            {
                diagonalRight = null;
            }
            else
            {
                diagonalRight = squareArray[firstClick.getX() - 1, firstClick.getY() + 1];

                if (diagonalRight.isOccupied())
                {
                    if (!diagonalRight.isRed() && diagonalRight.getX() != 0)
                    {
                        rightCounterToRemove = diagonalRight;
                        diagonalRight = squareArray[diagonalRight.getX() - 1, diagonalRight.getY() + 1];
                        rightTaken = true;

                        if (diagonalRight.isOccupied())
                        {
                            diagonalRight = null;
                        }
                    }
                    else
                    {
                        diagonalRight = null;
                    }
                }
            }

            if (diagonalLeft == null && diagonalRight == null)
            {
                firstClick = null;
                secondClick = null;
                return false;
            }
            else
            {
                if (secondClick == diagonalLeft)
                {
                    MessageBox.Show("Second click is diagonalLeft");

                    if (leftTaken)
                    {
                        leftCounterToRemove.setOccupied(false);
                        leftCounterToRemove.setKing(false);
                        leftCounterToRemove.BackgroundImage = Properties.Resources.Greyback;
                        leftCounterToRemove = null;
                    }

                    return true;
                }
                else if (secondClick == diagonalRight)
                {
                    MessageBox.Show("Second click is diagonalRight");

                    if (rightTaken)
                    {
                        rightCounterToRemove.setOccupied(false);
                        rightCounterToRemove.setKing(false);
                        rightCounterToRemove.BackgroundImage = Properties.Resources.Greyback;
                        rightCounterToRemove = null;
                    }

                    return true;
                }
                else
                {
                    firstClick = null;
                    secondClick = null;
                    return false;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)  //REQUIRED
        {

        }

        public void switchTurn()
        {
            if (blackTurn == true)
            {
                blackTurn = false;
                MessageBox.Show("Red's turn");
            }
            else
            {
                blackTurn = true;
                MessageBox.Show("Black's turn");
            }

            firstClick = null;
            secondClick = null;
        }

        /*public Form1()
        {
            InitializeComponent();
            Application.DoEvents();
            RoundedButton myButton1 = new RoundedButton();
            RoundedButton myButton2 = new RoundedButton();
            RoundedButton myButton3 = new RoundedButton();
            RoundedButton myButton4 = new RoundedButton();
            RoundedButton myButton5 = new RoundedButton();
            RoundedButton myButton6 = new RoundedButton();
            RoundedButton myButton7 = new RoundedButton();
            RoundedButton myButton8 = new RoundedButton();
            RoundedButton myButton9 = new RoundedButton();
            myButton1.BackColor = Color.Red;
            myButton2.BackColor = Color.Red;
            myButton3.BackColor = Color.Red;
            myButton4.BackColor = Color.Red;
            myButton5.BackColor = Color.Red;
            myButton6.BackColor = Color.Red;
            myButton7.BackColor = Color.Red;
            myButton8.BackColor = Color.Red;
            myButton9.BackColor = Color.Red;
            myButton1.Location = new System.Drawing.Point(20, 20);
            myButton2.Location = new System.Drawing.Point(40, 20);
            myButton3.Location = new System.Drawing.Point(60, 20);
            myButton4.Location = new System.Drawing.Point(80, 20);
            myButton5.Location = new System.Drawing.Point(100, 20);
            myButton6.Location = new System.Drawing.Point(120, 20);
            myButton7.Location = new System.Drawing.Point(140, 20);
            myButton8.Location = new System.Drawing.Point(160, 20);
            myButton9.Location = new System.Drawing.Point(180, 20);
            myButton1.Size = new System.Drawing.Size(20, 20);
            myButton2.Size = new System.Drawing.Size(20, 20);
            myButton3.Size = new System.Drawing.Size(20, 20);
            myButton4.Size = new System.Drawing.Size(20, 20);
            myButton5.Size = new System.Drawing.Size(20, 20);
            myButton6.Size = new System.Drawing.Size(20, 20);
            myButton7.Size = new System.Drawing.Size(20, 20);
            myButton8.Size = new System.Drawing.Size(20, 20);
            myButton9.Size = new System.Drawing.Size(20, 20);
            this.Controls.Add(myButton1);
            this.Controls.Add(myButton2);
            this.Controls.Add(myButton3);
            this.Controls.Add(myButton4);
            this.Controls.Add(myButton5);
            this.Controls.Add(myButton6);
            this.Controls.Add(myButton7);
            this.Controls.Add(myButton8);
            this.Controls.Add(myButton9);
        }
    }

    class RoundedButton : Button
    {
        GraphicsPath GetRoundPath(RectangleF Rect, int radius)
        {
            float r2 = radius / 2f;
            GraphicsPath GraphPath = new GraphicsPath();

            GraphPath.AddArc(Rect.X, Rect.Y, radius, radius, 180, 90);
            GraphPath.AddLine(Rect.X + r2, Rect.Y, Rect.Width - r2, Rect.Y);
            GraphPath.AddArc(Rect.X + Rect.Width - radius, Rect.Y, radius, radius, 270, 90);
            GraphPath.AddLine(Rect.Width, Rect.Y + r2, Rect.Width, Rect.Height - r2);
            GraphPath.AddArc(Rect.X + Rect.Width - radius,
                             Rect.Y + Rect.Height - radius, radius, radius, 0, 90);
            GraphPath.AddLine(Rect.Width - r2, Rect.Height, Rect.X + r2, Rect.Height);
            GraphPath.AddArc(Rect.X, Rect.Y + Rect.Height - radius, radius, radius, 90, 90);
            GraphPath.AddLine(Rect.X, Rect.Height - r2, Rect.X, Rect.Y + r2);

            GraphPath.CloseFigure();
            return GraphPath;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            RectangleF Rect = new RectangleF(0, 0, this.Width, this.Height);
            GraphicsPath GraphPath = GetRoundPath(Rect, 20);

            this.Region = new Region(GraphPath);
            using (Pen pen = new Pen(Color.CadetBlue, 1.75f))
            {
                pen.Alignment = PenAlignment.Inset;
                e.Graphics.DrawPath(pen, GraphPath);
            }
        }*/
    }
}
﻿