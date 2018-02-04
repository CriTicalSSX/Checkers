
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
    public partial class checkers : Form
    {
        Square[,] squareArray = new Square[10, 10];       // Create 2D array of buttons
        Square firstClick, secondClick;
        Square topLeft;
        Square topRight;
        Square bottomLeft;
        Square bottomRight;
        bool blackTurn = true;
        bool continueMoving = false;
        bool pieceTaken = false;
        int redCountersRemaining = 20;
        int blackCountersRemaining = 20;

        public checkers()
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
                    squareArray[x, y].Click += new EventHandler(this.squareEvent_Click);
                    Controls.Add(squareArray[x, y]);
                }
            }
        }

        void squareEvent_Click(object sender, EventArgs e)
        {
            Square square = sender as Square;
          
            if (firstClick == null)
            {
                if (blackTurn && square.BackColor != Color.Red && square.isOccupied() && !square.isRed())
                {
                    square.setCurrent();
                    firstClick = square;
                    
                    if (firstClick.isKing())
                    {
                        firstClick.BackgroundImage = Properties.Resources.Black_King_Selected;
                    }
                    else
                    {
                        firstClick.BackgroundImage = Properties.Resources.Black_Checker_Selected;
                    }
                }
                else if (!blackTurn && square.BackColor != Color.Red && square.isOccupied() && square.isRed())
                {
                    square.setCurrent();
                    firstClick = square;

                    if (firstClick.isKing())
                    {
                        firstClick.BackgroundImage = Properties.Resources.Red_King_Selected;
                    }
                    else
                    {
                        firstClick.BackgroundImage = Properties.Resources.Red_Checker_Selected;
                    }
                }
                else
                {
                    
                }
            }
            else
            {
                secondClick = square;
                bool moved = false;
                bool nomove = false;
                bool kinged = false;

                //BLACK'S TURN
                if (blackTurn)
                {
                    if (continueMoving)
                    {
                        if (secondClick == topLeft || secondClick == topRight || secondClick == bottomLeft || secondClick == bottomRight && secondClick != null)
                        {
                            Square taken;
                            if (secondClick == topLeft)
                            {
                                taken = squareArray[secondClick.getX() + 1, secondClick.getY() + 1];
                            }
                            else if (secondClick == topRight)
                            {
                                taken = squareArray[secondClick.getX() - 1, secondClick.getY() + 1];
                            }
                            else if (secondClick == bottomLeft)
                            {
                                taken = squareArray[secondClick.getX() + 1, secondClick.getY() - 1];
                            }
                            else
                            {
                                taken = squareArray[secondClick.getX() - 1, secondClick.getY() - 1];
                            }

                            if (firstClick.isKing())
                            {
                                secondClick.BackgroundImage = Properties.Resources.Black_King;
                                secondClick.setKing(true);
                            }
                            else
                            {
                                secondClick.BackgroundImage = Properties.Resources.Black_Checker;
                            }
                            
                            secondClick.setKing(firstClick.isKing());
                            secondClick.setRed(false);
                            secondClick.setOccupied(true);
                            firstClick.removeCurrent();
                            secondClick.setCurrent();
                            firstClick.setOccupied(false);
                            firstClick.setKing(false);
                            firstClick.BackgroundImage = Properties.Resources.Greyback;
                            taken.BackgroundImage = Properties.Resources.Greyback;
                            taken.setOccupied(false);
                            taken.setKing(false);
                            redCountersRemaining--;
                            Redtakencounter.Text = Convert.ToString(redCountersRemaining);
                            pieceTaken = true;
                            
                            if (redCountersRemaining == 0)
                            {
                                winner("Black");
                            }

                            if (canStillMove("black"))
                            {
                                firstClick = secondClick;
                                firstClick.setKing(secondClick.isKing());
                                firstClick.setCurrent();
                                secondClick = null;
                                continueMoving = true;
                            }
                            else
                            {
                                continueMoving = false;
                                if (secondClick != null)
                                {
                                    secondClick.removeCurrent();
                                }
                                pieceTaken = false;
                                moved = true;
                            }
                        }
                    }
                    else
                    {
                        if (firstClick.isKing())
                        {
                            if (canKingMove("black"))
                            {
                                secondClick.setKing(true);
                                secondClick.BackgroundImage = Properties.Resources.Black_King;
                                secondClick.setRed(false);
                                secondClick.setOccupied(true); 
                                secondClick.setCurrent();
                                firstClick.setKing(false);
                                firstClick.removeCurrent();
                                firstClick.setOccupied(false);
                                firstClick.BackgroundImage = Properties.Resources.Greyback;

                                if (canStillMove("black"))
                                {
                                    firstClick = secondClick;
                                    firstClick.setCurrent();
                                    secondClick = null;
                                    continueMoving = true;
                                }
                                else
                                {
                                    MessageBox.Show("Finished");
                                    continueMoving = false;

                                    if (secondClick != null)
                                    {
                                        secondClick.removeCurrent();
                                    }

                                    pieceTaken = false;
                                    moved = true;
                                }
                            }
                            else
                            {
                                nomove = true;
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
                                    kinged = true;
                                }

                                if (canStillMove("black") && secondClick != null && !kinged)
                                {
                                    firstClick = secondClick;
                                    firstClick.setKing(secondClick.isKing());
                                    firstClick.setCurrent();
                                    secondClick = null;
                                    continueMoving = true;
                                }
                                else
                                {
                                    continueMoving = false;
                                    if (secondClick != null)
                                    {
                                        secondClick.removeCurrent();
                                    }
                                    pieceTaken = false;
                                    moved = true;
                                }
                            }
                            else
                            {
                                nomove = true;
                            }
                        }

                        if (nomove)
                        {
                            MessageBox.Show("Cannot make this move.");
                            firstClick.removeCurrent();
                        }
                    }
                }

                //RED'S TURN
                else
                {
                    if (continueMoving)
                    {
                        if (secondClick == topLeft || secondClick == topRight || secondClick == bottomLeft || secondClick == bottomRight && secondClick != null)
                        {
                            if (firstClick.isKing())
                            {
                                secondClick.BackgroundImage = Properties.Resources.Red_King;
                                secondClick.setKing(true);
                            }
                            else
                            {
                                secondClick.BackgroundImage = Properties.Resources.Red_Checker;
                                secondClick.setKing(false);
                            }

                            Square taken;
                            if (secondClick == topLeft)
                            {
                                taken = squareArray[secondClick.getX() + 1, secondClick.getY() + 1];
                            }
                            else if (secondClick == topRight)
                            {
                                taken = squareArray[secondClick.getX() - 1, secondClick.getY() + 1];
                            }
                            else if (secondClick == bottomLeft)
                            {
                                taken = squareArray[secondClick.getX() + 1, secondClick.getY() - 1];
                            }
                            else
                            {
                                taken = squareArray[secondClick.getX() - 1, secondClick.getY() - 1];
                            }
                           
                            secondClick.setKing(firstClick.isKing());
                            secondClick.setRed(true);
                            secondClick.setOccupied(true);
                            secondClick.setCurrent();
                            firstClick.removeCurrent();
                            firstClick.setOccupied(false);
                            firstClick.BackgroundImage = Properties.Resources.Greyback;
                            taken.BackgroundImage = Properties.Resources.Greyback;
                            taken.setOccupied(false);
                            taken.setKing(false);
                            blackCountersRemaining--;
                            Blacktakencounter.Text = Convert.ToString(blackCountersRemaining);
                            pieceTaken = true;

                            if (blackCountersRemaining == 0)
                            {
                                winner("Red");
                            }

                            if (canStillMove("red"))
                            {
                                firstClick = secondClick;
                                firstClick.setKing(secondClick.isKing());
                                firstClick.setCurrent();                                
                                secondClick = null;
                                continueMoving = true;
                            }
                            else
                            {
                                continueMoving = false;
                                if (secondClick != null)
                                {
                                    secondClick.removeCurrent();
                                }
                                pieceTaken = false;
                                moved = true;
                            }
                        }
                    }
                    else
                    {
                        if (firstClick.isKing())
                        {
                            if (canKingMove("red"))
                            {
                                secondClick.BackgroundImage = Properties.Resources.Red_King;
                                square.BackColor = Color.DarkSlateGray;
                                secondClick.setKing(square.isKing());
                                secondClick.setRed(true);
                                square.setOccupied(false);
                                square.setKing(false);
                                secondClick.setOccupied(true);
                                secondClick.setKing(true);
                                firstClick.setKing(false);
                                firstClick.removeCurrent();
                                firstClick.setOccupied(false);
                                firstClick.BackgroundImage = Properties.Resources.Greyback;

                                if (canStillMove("red"))
                                {
                                    firstClick = secondClick;
                                    firstClick.setKing(secondClick.isKing());
                                    firstClick.setCurrent();                                   
                                    secondClick = null;
                                    continueMoving = true;
                                }
                                else
                                {
                                    continueMoving = false;
                                    if (secondClick != null)
                                    {
                                        secondClick.removeCurrent();
                                    }
                                    pieceTaken = false;
                                    moved = true;
                                }
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
                                    kinged = true;
                                }

                                if (canStillMove("red") && secondClick != null && !kinged)
                                {
                                    firstClick = secondClick;
                                    firstClick.setKing(secondClick.isKing());
                                    firstClick.setCurrent();                                   
                                    secondClick = null;
                                    continueMoving = true;
                                }
                                else
                                {
                                    continueMoving = false;
                                    if (secondClick != null)
                                    {
                                        secondClick.removeCurrent();
                                    }
                                    pieceTaken = false;
                                    moved = true;
                                }
                            }
                            else
                            {
                                nomove = true;
                            }
                        }

                        if (nomove)
                        {
                            MessageBox.Show("Cannot make this move.");
                        }
                    }
                }

                if (!continueMoving)
                {
                    if (firstClick != null)
                    {
                        firstClick.removeCurrent();
                    }

                    if (secondClick != null)
                    {
                        secondClick.removeCurrent();
                    }

                    firstClick = null;
                    secondClick = null;
                }

                if (moved)
                {
                    switchTurn();
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
                return false;
            }
            else
            {
                if (secondClick == diagonalLeft)
                {
                    if (leftTaken)
                    {
                        leftCounterToRemove.setOccupied(false);
                        leftCounterToRemove.setKing(false);
                        leftCounterToRemove.BackgroundImage = Properties.Resources.Greyback;
                        leftCounterToRemove = null;
                        redCountersRemaining--;
                        Redtakencounter.Text = Convert.ToString(redCountersRemaining);
                        pieceTaken = true;

                        if (redCountersRemaining == 0)
                        {
                            winner("Black");
                        }
                    }

                    return true;
                }
                else if (secondClick == diagonalRight)
                {
                    if (rightTaken)
                    {
                        rightCounterToRemove.setOccupied(false);
                        rightCounterToRemove.setKing(false);
                        rightCounterToRemove.BackgroundImage = Properties.Resources.Greyback;
                        rightCounterToRemove = null;
                        redCountersRemaining--;
                        Redtakencounter.Text = Convert.ToString(redCountersRemaining);
                        pieceTaken = true;

                        if (redCountersRemaining == 0)
                        {
                            winner("Black");
                        }
                    }

                    return true;
                }
                else
                {
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
                    if (!diagonalRight.isRed() && diagonalRight.getX() != 0 && diagonalRight.getY() != 9)
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
                return false;
            }
            else
            {
                if (secondClick == diagonalLeft)
                {
                    if (leftTaken)
                    {
                        leftCounterToRemove.setOccupied(false);
                        leftCounterToRemove.setKing(false);
                        leftCounterToRemove.BackgroundImage = Properties.Resources.Greyback;
                        leftCounterToRemove = null;
                        blackCountersRemaining--;
                        Blacktakencounter.Text = Convert.ToString(blackCountersRemaining);
                        pieceTaken = true;

                        if (blackCountersRemaining == 0)
                        {
                            winner("Red");
                        }
                    }

                    return true;
                }
                else if (secondClick == diagonalRight)
                {
                    if (rightTaken)
                    {
                        rightCounterToRemove.setOccupied(false);
                        rightCounterToRemove.setKing(false);
                        rightCounterToRemove.BackgroundImage = Properties.Resources.Greyback;
                        rightCounterToRemove = null;
                        blackCountersRemaining--;
                        Blacktakencounter.Text = Convert.ToString(blackCountersRemaining);
                        pieceTaken = true;

                        if (blackCountersRemaining == 0)
                        {
                            winner("Red");
                        }
                    }

                    return true;
                }
                else
                {
                    //firstClick.BackgroundImage = Properties.Resources.Red_Checker;
                    return false;
                }
            }
        }

        public bool canKingMove(string colour)
        {
            bool topLeftTaken = false;
            bool topRightTaken = false;
            bool bottomLeftTaken = false;
            bool bottomRightTaken = false;
            Square topLeftToRemove = null;
            Square topRightToRemove = null;
            Square bottomLeftToRemove = null;
            Square bottomRightToRemove = null;

            if (firstClick.getX() == 0 || firstClick.getY() == 0)
            {
                topLeft = null;        //counter on left side of board
            }
            else
            {
                topLeft = squareArray[firstClick.getX() - 1, firstClick.getY() - 1];

                if (topLeft.isOccupied())
                {
                    if (topLeft.isRed() && topLeft.getX() != 0 && topLeft.getY() != 0 && colour == "black")
                    {
                        topLeftToRemove = topLeft;
                        topLeft = squareArray[topLeft.getX() - 1, topLeft.getY() - 1];
                        topLeftTaken = true;

                        if (topLeft.isOccupied())
                        {
                            topLeft = null;
                        }
                    }
                    else if (!topLeft.isRed() && topLeft.getX() != 0 && topLeft.getY() != 0 && colour == "red")
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

            if (firstClick.getX() == 9 || firstClick.getY() == 0)
            {
                topRight = null;
            }
            else
            {
                topRight = squareArray[firstClick.getX() + 1, firstClick.getY() - 1];

                if (topRight.isOccupied())
                {
                    if (topRight.isRed() && topRight.getX() != 9 && topRight.getY() != 0 && colour == "black")
                    {
                        topRightToRemove = topRight;
                        topRight = squareArray[topRight.getX() + 1, topRight.getY() - 1];
                        topRightTaken = true;

                        if (topRight.isOccupied())
                        {
                            topRight = null;
                        }
                    }
                    else if (!topRight.isRed() && topRight.getX() != 9 && topRight.getY() != 0 && colour == "red")
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

            if (firstClick.getX() == 0 || firstClick.getY() == 9)
            {
                bottomLeft = null;
            }
            else
            {
                bottomLeft = squareArray[firstClick.getX() - 1, firstClick.getY() + 1];

                if (bottomLeft.isOccupied())
                {
                    if (bottomLeft.isRed() && bottomLeft.getX() != 0 && bottomLeft.getY() != 9 && colour == "black")
                    {
                        bottomLeftToRemove = bottomLeft;
                        bottomLeft = squareArray[bottomLeft.getX() - 1, bottomLeft.getY() + 1];
                        bottomLeftTaken = true;

                        if (bottomLeft.isOccupied())
                        {
                            bottomLeft = null;
                        }
                    }
                    else if (!bottomLeft.isRed() && bottomLeft.getX() != 0 && bottomLeft.getY() != 9 && colour == "red")
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

            if (firstClick.getX() == 9 || firstClick.getY() == 9)
            {
                bottomRight = null;
            }
            else
            {
                bottomRight = squareArray[firstClick.getX() + 1, firstClick.getY() + 1];

                if (bottomRight.isOccupied())
                {
                    if (bottomRight.isRed() && bottomRight.getX() != 9 && bottomRight.getY() != 9 && colour == "black")
                    {
                        bottomRightToRemove = bottomRight;
                        bottomRight = squareArray[bottomRight.getX() + 1, bottomRight.getY() + 1];
                        bottomRightTaken = true;

                        if (bottomRight.isOccupied())
                        {
                            bottomRight = null;
                        }
                    }
                    else if (!bottomRight.isRed() && bottomRight.getX() != 9 && bottomRight.getY() != 9 && colour == "red")
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
                return false;
            }
            else
            {
                if (secondClick == topLeft)
                {
                    if (topLeftTaken)
                    {
                        topLeftToRemove.setOccupied(false);
                        topLeftToRemove.setKing(false);
                        topLeftToRemove.BackgroundImage = Properties.Resources.Greyback;
                        topLeftToRemove = null;
                        pieceTaken = true;

                        if (colour == "black")
                        {
                            redCountersRemaining--;
                            Redtakencounter.Text = Convert.ToString(redCountersRemaining);
                        }
                        else
                        {
                            blackCountersRemaining--;
                            Blacktakencounter.Text = Convert.ToString(blackCountersRemaining);
                        }

                        if (redCountersRemaining == 0)
                        {
                            winner("Black");
                        }
                        else if (blackCountersRemaining == 0)
                        {
                            winner("Red");
                        }
                    }

                    return true;
                }
                else if (secondClick == topRight)
                {
                    if (topRightTaken)
                    {
                        topRightToRemove.setOccupied(false);
                        topRightToRemove.setKing(false);
                        topRightToRemove.BackgroundImage = Properties.Resources.Greyback;
                        topRightToRemove = null;
                        pieceTaken = true;

                        if (colour == "black")
                        {
                            redCountersRemaining--;
                            Redtakencounter.Text = Convert.ToString(redCountersRemaining);
                        }
                        else
                        {
                            blackCountersRemaining--;
                            Blacktakencounter.Text = Convert.ToString(blackCountersRemaining);
                        }

                        if (redCountersRemaining == 0)
                        {
                            winner("Black");
                        }
                        else if (blackCountersRemaining == 0)
                        {
                            winner("Red");
                        }
                    }

                    return true;
                }
                else if (secondClick == bottomLeft)
                {
                    if (bottomLeftTaken)
                    {
                        bottomLeftToRemove.setOccupied(false);
                        bottomLeftToRemove.setKing(false);
                        bottomLeftToRemove.BackgroundImage = Properties.Resources.Greyback;
                        bottomLeftToRemove = null;
                        pieceTaken = true;

                        if (colour == "black")
                        {
                            redCountersRemaining--;
                            Redtakencounter.Text = Convert.ToString(redCountersRemaining);
                        }
                        else
                        {
                            blackCountersRemaining--;
                            Blacktakencounter.Text = Convert.ToString(blackCountersRemaining);
                        }

                        if (redCountersRemaining == 0)
                        {
                            winner("Black");
                        }
                        else if (blackCountersRemaining == 0)
                        {
                            winner("Red");
                        }
                    }

                    return true;
                }
                else if (secondClick == bottomRight)
                {
                    if (bottomRightTaken)
                    {
                        bottomRightToRemove.setOccupied(false);
                        bottomRightToRemove.setKing(false);
                        bottomRightToRemove.BackgroundImage = Properties.Resources.Greyback;
                        bottomRightToRemove = null;
                        pieceTaken = true;

                        if (colour == "black")
                        {
                            redCountersRemaining--;
                            Redtakencounter.Text = Convert.ToString(redCountersRemaining);
                        }
                        else
                        {
                            blackCountersRemaining--;
                            Blacktakencounter.Text = Convert.ToString(blackCountersRemaining);
                        }

                        if (redCountersRemaining == 0)
                        {
                            winner("Black");
                        }
                        else if (blackCountersRemaining == 0)
                        {
                            winner("Red");
                        }
                    }

                    return true;
                }
                else
                {                   
                    return false;
                }
            }
        }     

        public bool canStillMove(string colour)
        {
            if (!pieceTaken)
            {
                continueMoving = false;
                topLeft = null;
                topRight = null;
                bottomLeft = null;
                bottomRight = null;
                return false;
            }

            int leftmost = secondClick.getX() - 2;
            int rightmost = secondClick.getX() + 2;
            int topmost = secondClick.getY() - 2;
            int bottommost = secondClick.getY() + 2;
            topLeft = null;
            topRight = null;
            bottomLeft = null;
            bottomRight = null;

            if (colour == "red")
            {
                if (leftmost >= 0 && topmost >= 0)
                {
                    if (squareArray[leftmost+1, topmost+1].isOccupied() && !squareArray[leftmost+1, topmost+1].isRed() && !squareArray[leftmost, topmost].isOccupied())
                    {
                        topLeft = squareArray[leftmost, topmost];
                    }
                }

                if (rightmost <= 9 && topmost >= 0)
                {
                    if (squareArray[rightmost - 1, topmost + 1].isOccupied() && !squareArray[rightmost - 1, topmost + 1].isRed() && !squareArray[rightmost, topmost].isOccupied())
                    {
                        topRight = squareArray[rightmost, topmost];
                    }
                }

                if (leftmost >= 0 && bottommost <= 9)
                {
                    if (squareArray[leftmost + 1, bottommost - 1].isOccupied() && !squareArray[leftmost + 1, bottommost - 1].isRed() && !squareArray[leftmost, bottommost].isOccupied())
                    {
                        bottomLeft = squareArray[leftmost, bottommost];
                    }
                }

                if (rightmost <=9 && bottommost <= 9)
                {
                    if (squareArray[rightmost - 1, bottommost - 1].isOccupied() && !squareArray[rightmost - 1, bottommost - 1].isRed() && !squareArray[rightmost, bottommost].isOccupied())
                    {
                        bottomRight = squareArray[rightmost, bottommost];
                    }
                }
            }
            else
            {
                if (leftmost >= 0 && topmost >= 0)
                {
                    if (squareArray[leftmost + 1, topmost + 1].isOccupied() && squareArray[leftmost + 1, topmost + 1].isRed() && !squareArray[leftmost, topmost].isOccupied())
                    {
                        topLeft = squareArray[leftmost, topmost];
                    }
                }

                if (rightmost <= 9 && topmost >= 0)
                {
                    if (squareArray[rightmost - 1, topmost + 1].isOccupied() && squareArray[rightmost - 1, topmost + 1].isRed() && !squareArray[rightmost, topmost].isOccupied())
                    {
                        topRight = squareArray[rightmost, topmost];
                    }
                }

                if (leftmost >= 0 && bottommost <= 9)
                {
                    if (squareArray[leftmost + 1, bottommost - 1].isOccupied() && squareArray[leftmost + 1, bottommost - 1].isRed() && !squareArray[leftmost, bottommost].isOccupied())
                    {
                        bottomLeft = squareArray[leftmost, bottommost];
                    }
                }

                if (rightmost <= 9 && bottommost <= 9)
                {
                    if (squareArray[rightmost - 1, bottommost - 1].isOccupied() && squareArray[rightmost - 1, bottommost - 1].isRed() && !squareArray[rightmost, bottommost].isOccupied())
                    {
                        bottomRight = squareArray[rightmost, bottommost];
                    }
                }
            }

            if (topLeft == null && topRight == null && bottomLeft == null && bottomRight == null)
            {
                topLeft = null;
                topRight = null;
                bottomLeft = null;
                bottomRight = null;
                return false;
            }
            else
            {
                return true;
            }
        }

        private void checkers_Load(object sender, EventArgs e)  //REQUIRED
        {

        }

        public void switchTurn()
        {
            if (blackTurn == true)
            {
                blackTurn = false;
                Turncounter.Text = "RED";
            }
            else
            {
                blackTurn = true;
                Turncounter.Text = "BLACK";
            }
        }

        public void winner(string colour)
        {
            MessageBox.Show(colour + " wins!");
            Close();
        }
    }
}
﻿