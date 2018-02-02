
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
        Square firstClick, secondClick; 
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
            /*if (blackTurn == true)
            {
                MessageBox.Show("Is black's turn");
            }
            else
            {
                MessageBox.Show("Is red's turn");
            }
            */
            Square square = sender as Square;

            if (firstClick == null)
            {
                if (blackTurn && square.BackColor != Color.Red && square.isOccupied())
                {
                    square.setRed(false);
                    square.setCurrent();
                    firstClick = square;
                    //MessageBox.Show("Black selected");
                }
                else if (!blackTurn && square.BackColor != Color.Red && square.isOccupied())
                {
                    square.setRed(true);
                    square.setCurrent();
                    firstClick = square;
                    //MessageBox.Show("Red selected");
                }
                else
                {
                    firstClick = null;
                }
            }
            else
            {
                secondClick = square;
                bool moved = false;

                //BLACK'S TURN
                if (blackTurn)
                {
                    if (secondClick.isKing())
                    {
                        while (canBlackKingMove())
                        {
                            secondClick.BackgroundImage = Properties.Resources.Black_Checker;
                            square.BackColor = Color.DarkSlateGray;
                            secondClick.setKing(square.isKing());
                            secondClick.setRed(false);
                            square.setOccupied(false);
                            square.setKing(false);
                            secondClick.setOccupied(true);
                            firstClick.removeCurrent();
                            firstClick.setOccupied(false);
                            firstClick.BackgroundImage = Properties.Resources.Greyback;

                            if (secondClick.getY() == 0)
                            {
                                secondClick.setKing(true);
                            }

                            MessageBox.Show("Moved");
                            moved = true;
                        }
                    }
                    else
                    {
                        if (canBlackMove())
                        {
                            secondClick.BackgroundImage = Properties.Resources.Black_Checker;
                            square.BackColor = Color.DarkSlateGray;
                            secondClick.setKing(square.isKing());
                            secondClick.setRed(false);
                            square.setOccupied(false);
                            square.setKing(false);
                            secondClick.setOccupied(true);
                            firstClick.removeCurrent();
                            firstClick.setOccupied(false);
                            firstClick.BackgroundImage = Properties.Resources.Greyback;

                            if (secondClick.getY() == 0)
                            {
                                secondClick.setKing(true);
                                firstClick = null;
                                secondClick = null;
                                switchTurn();
                            }

                            MessageBox.Show("Moved");
                            moved = true;

                            while (canBlackKingMove())
                            {
                                secondClick.BackgroundImage = Properties.Resources.Black_Checker;
                                square.BackColor = Color.DarkSlateGray;
                                secondClick.setKing(square.isKing());
                                secondClick.setRed(false);
                                square.setOccupied(false);
                                square.setKing(false);
                                secondClick.setOccupied(true);
                                firstClick.removeCurrent();
                                firstClick.setOccupied(false);
                                firstClick.BackgroundImage = Properties.Resources.Greyback;

                                MessageBox.Show("Moved");
                            }
                        }
                    }

                    MessageBox.Show("Cannot make this move.");
                    firstClick = null;
                    secondClick = null;

                    if (moved)
                    {
                        switchTurn();
                    }
                }

                //RED'S TURN
                else
                {
                    if (secondClick.isKing())
                    {
                        while (canRedKingMove())
                        {
                            secondClick.BackgroundImage = Properties.Resources.Red_Checker;
                            square.BackColor = Color.DarkSlateGray;
                            secondClick.setKing(square.isKing());
                            secondClick.setRed(true);
                            square.setOccupied(false);
                            square.setKing(false);
                            secondClick.setOccupied(true);
                            firstClick.removeCurrent();
                            firstClick.setOccupied(false);
                            firstClick.BackgroundImage = Properties.Resources.Greyback;

                            if (secondClick.getY() == 9)
                            {
                                secondClick.setKing(true);
                                firstClick = null;
                                secondClick = null;
                                switchTurn();
                            }

                            MessageBox.Show("Moved");
                            moved = true;
                        }
                    }
                    else
                    {
                        if (canRedMove())
                        {
                            secondClick.BackgroundImage = Properties.Resources.Red_Checker;
                            square.BackColor = Color.DarkSlateGray;
                            secondClick.setKing(square.isKing());
                            secondClick.setRed(true);
                            square.setOccupied(false);
                            square.setKing(false);
                            secondClick.setOccupied(true);
                            firstClick.removeCurrent();
                            firstClick.setOccupied(false);
                            firstClick.BackgroundImage = Properties.Resources.Greyback;

                            if (secondClick.getY() == 9)
                            {
                                secondClick.setKing(true);
                            }

                            MessageBox.Show("Moved");
                            moved = true;

                            while (canRedKingMove())
                            {
                                secondClick.BackgroundImage = Properties.Resources.Red_Checker;
                                square.BackColor = Color.DarkSlateGray;
                                secondClick.setKing(square.isKing());
                                secondClick.setRed(true);
                                square.setOccupied(false);
                                square.setKing(false);
                                secondClick.setOccupied(true);
                                firstClick.removeCurrent();
                                firstClick.setOccupied(false);
                                firstClick.BackgroundImage = Properties.Resources.Greyback;

                                if (secondClick.getY() == 9)
                                {
                                    secondClick.setKing(true);
                                }

                                MessageBox.Show("Moved");
                            }
                        }
                    }

                    MessageBox.Show("Cannot make this move.");
                    firstClick = null;
                    secondClick = null;

                    if (moved)
                    {
                        switchTurn();
                    }
                }
            }
        }

        public bool canBlackMove()
        {
            bool leftTaken = false;
            bool rightTaken = false;
            Square leftCounterToRemove = null;
            Square rightCounterToRemove = null;

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
                    if (diagonalLeft.isRed() && diagonalLeft.getX() != 0 && diagonalLeft.getY() != 0)
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
                    if (diagonalRight.isRed() && diagonalRight.getX() != 9 && diagonalRight.getY() != 0)
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
            Square leftCounterToRemove = null;
            Square rightCounterToRemove = null;

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
                    if (!diagonalLeft.isRed() && diagonalLeft.getX() != 9 && diagonalLeft.getY() != 9)
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
                    if (!diagonalRight.isRed() && diagonalRight.getX() != 0 && diagonalLeft.getY() != 9)
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

        public bool canBlackKingMove()
        {
            bool topLeftTaken = false;
            bool topRightTaken = false;
            bool bottomLeftTaken = false;
            bool bottomRightTaken = false;
            Square topLeftToRemove = null;
            Square topRightToRemove = null;
            Square bottomLeftToRemove = null;
            Square bottomRightToRemove = null;

            Square topLeft;

            if (firstClick.getX() == 0 || firstClick.getY() == 0)
            {
                topLeft = null;        //counter on left side of board
            }
            else
            {
                topLeft = squareArray[firstClick.getX() - 1, firstClick.getY() - 1];

                if (topLeft.isOccupied())
                {
                    if (topLeft.isRed() && topLeft.getX() != 0 && topLeft.getY() != 0)
                    {
                        topLeftToRemove = topLeft;
                        topLeft = squareArray[topLeft.getX() - 1, topLeft.getY() - 1];
                        topLeftTaken = true;

                        if (topLeft.isOccupied())
                        {
                            topLeft = null;
                        }
                    }
                    else
                    {
                        topLeft = null;
                    }
                }
            }

            Square topRight;

            if (firstClick.getX() == 9 || firstClick.getY() == 0)
            {
                topRight = null;
            }
            else
            {
                topRight = squareArray[firstClick.getX() + 1, firstClick.getY() - 1];

                if (topRight.isOccupied())
                {
                    if (topRight.isRed() && topRight.getX() != 9 && topRight.getY() != 0)
                    {
                        topRightToRemove = topRight;
                        topRight = squareArray[topRight.getX() + 1, topRight.getY() - 1];
                        topRightTaken = true;

                        if (topRight.isOccupied())
                        {
                            topRight = null;
                        }
                    }
                    else
                    {
                        topRight = null;
                    }
                }
            }

            Square bottomLeft;

            if (firstClick.getX() == 0 || firstClick.getY() == 9)
            {
                bottomLeft = null;
            }
            else
            {
                bottomLeft = squareArray[firstClick.getX() - 1, firstClick.getY() + 1];

                if (bottomLeft.isOccupied())
                {
                    if (bottomLeft.isRed() && bottomLeft.getX() != 0 && bottomLeft.getY() != 9)
                    {
                        bottomLeftToRemove = bottomLeft;
                        bottomLeft = squareArray[bottomLeft.getX() - 1, bottomLeft.getY() + 1];
                        bottomLeftTaken = true;

                        if (bottomLeft.isOccupied())
                        {
                            bottomLeft = null;
                        }
                    }
                    else
                    {
                        bottomLeft = null;
                    }
                }
            }

            Square bottomRight;

            if (firstClick.getX() == 9 || firstClick.getY() == 9)
            {
                bottomRight = null;
            }
            else
            {
                bottomRight = squareArray[firstClick.getX() + 1, firstClick.getY() + 1];

                if (bottomRight.isOccupied())
                {
                    if (bottomRight.isRed() && bottomRight.getX() != 9 && bottomRight.getY() != 9)
                    {
                        bottomRightToRemove = bottomRight;
                        bottomRight = squareArray[bottomRight.getX() + 1, bottomRight.getY() + 1];
                        bottomRightTaken = true;

                        if (bottomRight.isOccupied())
                        {
                            bottomRight = null;
                        }
                    }
                    else
                    {
                        bottomRight = null;
                    }
                }
            }

            if (topLeft == null && topRight == null && bottomLeft == null && bottomRight == null)
            {
                firstClick = null;
                secondClick = null;
                return false;
            }
            else
            {
                if (secondClick == topLeft)
                {
                    MessageBox.Show("Second click is topLeft");

                    if (topLeftTaken)
                    {
                        topLeftToRemove.setOccupied(false);
                        topLeftToRemove.setKing(false);
                        topLeftToRemove.BackgroundImage = Properties.Resources.Greyback;
                        topLeftToRemove = null;
                    }

                    return true;
                }
                else if (secondClick == topRight)
                {
                    MessageBox.Show("Second click is topRight");

                    if (topRightTaken)
                    {
                        topRightToRemove.setOccupied(false);
                        topRightToRemove.setKing(false);
                        topRightToRemove.BackgroundImage = Properties.Resources.Greyback;
                        topRightToRemove = null;
                    }

                    return true;
                }
                else if (secondClick == bottomLeft)
                {
                    MessageBox.Show("Second click is bottomLeft");

                    if (bottomLeftTaken)
                    {
                        bottomLeftToRemove.setOccupied(false);
                        bottomLeftToRemove.setKing(false);
                        bottomLeftToRemove.BackgroundImage = Properties.Resources.Greyback;
                        bottomLeftToRemove = null;
                    }

                    return true;
                }
                else if (secondClick == bottomRight)
                {
                    MessageBox.Show("Second click is bottomRight");

                    if (bottomRightTaken)
                    {
                        bottomRightToRemove.setOccupied(false);
                        bottomRightToRemove.setKing(false);
                        bottomRightToRemove.BackgroundImage = Properties.Resources.Greyback;
                        bottomRightToRemove = null;
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

        public bool canRedKingMove()
        {
            bool topLeftTaken = false;
            bool topRightTaken = false;
            bool bottomLeftTaken = false;
            bool bottomRightTaken = false;
            Square topLeftToRemove = null;
            Square topRightToRemove = null;
            Square bottomLeftToRemove = null;
            Square bottomRightToRemove = null;

            Square topLeft;

            if (firstClick.getX() == 0 || firstClick.getY() == 0)
            {
                topLeft = null;        //counter on left side of board
            }
            else
            {
                topLeft = squareArray[firstClick.getX() - 1, firstClick.getY() - 1];

                if (topLeft.isOccupied())
                {
                    if (!topLeft.isRed() && topLeft.getX() != 0 && topLeft.getY() != 0)
                    {
                        topLeftToRemove = topLeft;
                        topLeft = squareArray[topLeft.getX() - 1, topLeft.getY() - 1];
                        topLeftTaken = true;

                        if (topLeft.isOccupied())
                        {
                            topLeft = null;
                        }
                    }
                    else
                    {
                        topLeft = null;
                    }
                }
            }

            Square topRight;

            if (firstClick.getX() == 9 || firstClick.getY() == 0)
            {
                topRight = null;
            }
            else
            {
                topRight = squareArray[firstClick.getX() + 1, firstClick.getY() + 1];

                if (topRight.isOccupied())
                {
                    if (!topRight.isRed() && topRight.getX() != 9 && topRight.getY() != 0)
                    {
                        topRightToRemove = topRight;
                        topRight = squareArray[topRight.getX() + 1, topRight.getY() + 1];
                        topRightTaken = true;

                        if (topRight.isOccupied())
                        {
                            topRight = null;
                        }
                    }
                    else
                    {
                        topRight = null;
                    }
                }
            }

            Square bottomLeft;

            if (firstClick.getX() == 0 || firstClick.getY() == 9)
            {
                bottomLeft = null;
            }
            else
            {
                bottomLeft = squareArray[firstClick.getX() - 1, firstClick.getY() + 1];

                if (bottomLeft.isOccupied())
                {
                    if (!bottomLeft.isRed() && bottomLeft.getX() != 0 && bottomLeft.getY() != 9)
                    {
                        bottomLeftToRemove = bottomLeft;
                        bottomLeft = squareArray[bottomLeft.getX() - 1, bottomLeft.getY() + 1];
                        bottomLeftTaken = true;

                        if (bottomLeft.isOccupied())
                        {
                            bottomLeft = null;
                        }
                    }
                    else
                    {
                        bottomLeft = null;
                    }
                }
            }

            Square bottomRight;

            if (firstClick.getX() == 9 || firstClick.getY() == 9)
            {
                bottomRight = null;
            }
            else
            {
                bottomRight = squareArray[firstClick.getX() + 1, firstClick.getY() + 1];

                if (bottomRight.isOccupied())
                {
                    if (!bottomRight.isRed() && bottomRight.getX() != 9 && bottomRight.getY() != 9)
                    {
                        bottomRightToRemove = bottomRight;
                        bottomRight = squareArray[bottomRight.getX() + 1, bottomRight.getY() + 1];
                        bottomRightTaken = true;

                        if (bottomRight.isOccupied())
                        {
                            bottomRight = null;
                        }
                    }
                    else
                    {
                        bottomRight = null;
                    }
                }
            }

            if (topLeft == null && topRight == null && bottomLeft == null && bottomRight == null)
            {
                firstClick = null;
                secondClick = null;
                return false;
            }
            else
            {
                if (secondClick == topLeft)
                {
                    MessageBox.Show("Second click is topLeft");

                    if (topLeftTaken)
                    {
                        topLeftToRemove.setOccupied(false);
                        topLeftToRemove.setKing(false);
                        topLeftToRemove.BackgroundImage = Properties.Resources.Greyback;
                        topLeftToRemove = null;
                    }

                    return true;
                }
                else if (secondClick == topRight)
                {
                    MessageBox.Show("Second click is topRight");

                    if (topRightTaken)
                    {
                        topRightToRemove.setOccupied(false);
                        topRightToRemove.setKing(false);
                        topRightToRemove.BackgroundImage = Properties.Resources.Greyback;
                        topRightToRemove = null;
                    }

                    return true;
                }
                else if (secondClick == bottomLeft)
                {
                    MessageBox.Show("Second click is bottomLeft");

                    if (bottomLeftTaken)
                    {
                        bottomLeftToRemove.setOccupied(false);
                        bottomLeftToRemove.setKing(false);
                        bottomLeftToRemove.BackgroundImage = Properties.Resources.Greyback;
                        bottomLeftToRemove = null;
                    }

                    return true;
                }
                else if (secondClick == bottomRight)
                {
                    MessageBox.Show("Second click is bottomRight");

                    if (bottomRightTaken)
                    {
                        bottomRightToRemove.setOccupied(false);
                        bottomRightToRemove.setKing(false);
                        bottomRightToRemove.BackgroundImage = Properties.Resources.Greyback;
                        bottomRightToRemove = null;
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
    }
}
﻿