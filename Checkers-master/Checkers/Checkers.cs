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
        Square[,] squareArray = new Square[10, 10];         // Create 2D array of buttons
        Square firstClick, secondClick, topLeft, topRight, bottomLeft, bottomRight;        //References to squares being interacted with             
        bool blackTurn = true;                  //Indicates whose turn it is
        bool continueMoving = false;            //Used when consecutive takes are possible
        bool pieceTaken = false;                //Used to indicate when to stop taking pieces, i.e. no more possible takes
        int redCountersRemaining = 20;          //Self-explanatory
        int blackCountersRemaining = 20;

        /*
         *  Constructor to initialise the game board 
        */
        public checkers()
        {
            InitializeComponent();
            BuildBoard();
        }

        /*
         *  Creates the game board using an array of Square objects.  
        */
        void BuildBoard()
        {
            for (int x = 0; x < squareArray.GetLength(0); x++)         // Loop for x
            {
                for (int y = 0; y < squareArray.GetLength(1); y++)     // Loop for y
                {
                    squareArray[x, y] = new Square(x, y);
                    squareArray[x, y].SetBounds(50 * x, 50 * y, 55, 55);
                    squareArray[x, y].setOccupied(false);

                    //Code for occupied squares
                    if ((x % 2 == 0 && y % 2 == 0) || (x % 2 == 1 && y % 2 == 1))
                    {
                        squareArray[x, y].BackgroundImage = Properties.Resources.Redback;       //square needs red background
                    }
                    else
                    {
                        squareArray[x, y].BackgroundImage = Properties.Resources.Greyback;      //square needs grey background

                        if (y >= 0 && y <= 3)
                        {
                            //red counters
                            squareArray[x, y].setRed(true);
                            squareArray[x, y].setOccupied(true);
                        }
                        if (y >= 6 && y <= 10)
                        {
                            //black counters
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

        /*
         *  Event handler for every square object 
        */
        void squareEvent_Click(object sender, EventArgs e)
        {
            Square square = sender as Square;       

            if (firstClick == null)         //firstClick is a reference to a counter that the user has indicated they want to move
            {
                if (blackTurn && square.isOccupied() && !square.isRed())    //if black's turn and there is a counter on the square that is not red
                {
                    firstClick = square;
                    firstClick.setCurrent();        //yellow border around counter
                }
                else if (!blackTurn && square.isOccupied() && square.isRed())   //if red's turn and there is a counter on the square that is not black
                {
                    firstClick = square;
                    firstClick.setCurrent();
                }
            }
            else
            {
                secondClick = square;           //indicates this will be the square to move the first clicked counter to
                bool turnEnd = false;           //indicates whether player's turn has ended or not
                bool nomove = false;            //indicates whether the player has made a move yet
                bool kinged = false;            //brings the player's turn to an end if a counter of theirs is kinged

                //Black's turn
                if (blackTurn)
                {
                    if (continueMoving)     //if counter is continuing from a previous take
                    {
                        if (secondClick == topLeft || secondClick == topRight || secondClick == bottomLeft || secondClick == bottomRight)       //references to all possible squares to move to
                        {
                            Square taken;       //square to be taken, calculated below
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

                            //transfers properties from square moving from to new square
                            secondClick.setKing(firstClick.isKing());      
                            secondClick.setRed(false);
                            secondClick.setOccupied(true);
                            secondClick.setCurrent();
                            firstClick.removeCurrent();                            
                            firstClick.setOccupied(false);
                            firstClick.setKing(false);
                            firstClick.BackgroundImage = Properties.Resources.Greyback;
                            taken.BackgroundImage = Properties.Resources.Greyback;
                            taken.setOccupied(false);
                            taken.setKing(false);

                            //Subtract one from the number of red counters remaining
                            subtract("red");

                            if (canStillMove("black"))      //if there is another take available
                            {
                                firstClick = secondClick;       //moved-to square acts as square moving from for next take
                                firstClick.setCurrent();
                                secondClick = null;
                                continueMoving = true;
                            }
                            else       //no further takes available
                            {
                                continueMoving = false;
                                pieceTaken = false;
                                turnEnd = true;
                            }
                        }
                    }
                    else     //first move in sequence
                    {
                        if (firstClick.isKing())        //if counter being moved is a king
                        {
                            if (canKingMove("black"))           //if the black king selected has a move available
                            {
                                //transfer properties of moving counter to square clicked on
                                secondClick.setKing(true);
                                secondClick.setRed(false);
                                secondClick.setOccupied(true);
                                secondClick.setCurrent();
                                firstClick.setKing(false);
                                firstClick.removeCurrent();
                                firstClick.setOccupied(false);
                                firstClick.BackgroundImage = Properties.Resources.Greyback;

                                if (canStillMove("black"))      //if the counter has taken a piece and can still take a piece
                                {
                                    firstClick = secondClick;
                                    firstClick.setCurrent();
                                    secondClick = null;
                                    continueMoving = true;
                                }
                                else
                                {
                                    continueMoving = false;
                                    pieceTaken = false;
                                    turnEnd = true;
                                }
                            }
                            else
                            {
                                nomove = true;          //counter has not moved, still player's turn
                            }

                        }
                        else        //regular black counter
                        {
                            if (canBlackMove())         //if black counter has a move available
                            {
                                //transfer properties
                                secondClick.setKing(firstClick.isKing());
                                secondClick.setRed(false);
                                secondClick.setOccupied(true);
                                secondClick.setCurrent();
                                firstClick.removeCurrent();
                                firstClick.setOccupied(false);
                                firstClick.BackgroundImage = Properties.Resources.Greyback;

                                //if black counter reaches the top of the board, set counter as king
                                if (secondClick.getY() == 0)
                                {
                                    secondClick.setKing(true);
                                    kinged = true;
                                }

                                //if counter has taken a piece and has not been kinged
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
                                    pieceTaken = false;
                                    turnEnd = true;
                                }
                            }
                            else
                            {
                                nomove = true;      //counter has not moved, still player's turn
                            }
                        }

                        if (nomove)
                        {
                            MessageBox.Show("Cannot make this move.");
                        }
                    }
                }

                //RED'S TURN
                else
                {
                    if (continueMoving)
                    {
                        if (secondClick == topLeft || secondClick == topRight || secondClick == bottomLeft || secondClick == bottomRight)
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

                            secondClick.setKing(firstClick.isKing());
                            secondClick.setRed(true);
                            secondClick.setOccupied(true);
                            secondClick.setCurrent();
                            firstClick.removeCurrent();
                            firstClick.setOccupied(false);
                            firstClick.setKing(false);
                            firstClick.BackgroundImage = Properties.Resources.Greyback;
                            taken.BackgroundImage = Properties.Resources.Greyback;
                            taken.setOccupied(false);
                            taken.setKing(false);

                            //Subtract one from the number of black counters remaining
                            subtract("black");

                            if (canStillMove("red"))        //if the same piece can take another piece
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
                                pieceTaken = false;
                                turnEnd = true;
                            }
                        }
                    }
                    else        //first move in sequence
                    {
                        if (firstClick.isKing())            //if counter being moved is a king
                        {
                            if (canKingMove("red"))         //if the red king selected can be moved
                            {
                                secondClick.setKing(firstClick.isKing());
                                secondClick.setRed(true);
                                secondClick.setOccupied(true);
                                secondClick.setKing(true);
                                firstClick.setKing(false);
                                firstClick.removeCurrent();
                                firstClick.setOccupied(false);
                                firstClick.BackgroundImage = Properties.Resources.Greyback;

                                //if the red counter has taken a piece and can take another
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
                                    pieceTaken = false;
                                    turnEnd = true;
                                }
                            }
                        }
                        else         //regular red counter
                        {
                            if (canRedMove())       //if red counter can make a move
                            {
                                secondClick.setKing(firstClick.isKing());
                                secondClick.setRed(firstClick.isRed());
                                secondClick.setOccupied(true);
                                firstClick.removeCurrent();
                                firstClick.setOccupied(false);
                                firstClick.BackgroundImage = Properties.Resources.Greyback;

                                //if red counter reaches the bottom of the board, set counter as king
                                if (secondClick.getY() == 9)
                                {
                                    secondClick.setKing(true);
                                    kinged = true;
                                }

                                //if counter has taken a piece and can take another and has not been kinged
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
                                    pieceTaken = false;
                                    turnEnd = true;
                                }
                            }
                            else
                            {
                                nomove = true;
                            }
                        }

                        if (nomove)     //counter didn't move
                        {
                            MessageBox.Show("Cannot make this move.");
                        }
                    }
                }

                //if a player's turn has ended, set firstClick and secondClick references to null in preparation for second player's turn
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

                if (turnEnd)
                {
                    switchTurn();       //changes the turn
                }
            }
        }

        /*
         *  Checks whether a regular black counter can move. Can only move diagonally forward 
        */
        public bool canBlackMove()
        {
            bool leftTaken = false;         //indicates whether there is a piece in the way of the counter that can be taken
            bool rightTaken = false;        //indicates whether there is a piece in the way of the counter that can be taken
            Square leftCounterToRemove = null;      //reference to other player's counter to be removed
            Square rightCounterToRemove = null;     //reference to other player's counter to be removed

            Square diagonalLeft;        //the square diagonally left that can be moved to

            if (firstClick.getX() == 0)
            {
                diagonalLeft = null;      //counter on left side of board so no move to left
            }
            else
            {
                diagonalLeft = squareArray[firstClick.getX() - 1, firstClick.getY() - 1];

                if (diagonalLeft.isOccupied())
                {
                    if (diagonalLeft.isRed() && diagonalLeft.getX() != 0 && diagonalLeft.getY() != 0)       //if square diagonally left has a red counter in it and a space past it to jump into
                    {
                        leftCounterToRemove = diagonalLeft;         //counter in the way can be taken
                        diagonalLeft = squareArray[diagonalLeft.getX() - 1, diagonalLeft.getY() - 1];
                        leftTaken = true;

                        if (diagonalLeft.isOccupied())      //if both squares diagonally left are occupied
                        {
                            diagonalLeft = null;            //no move to left
                        }
                    }
                    else    //if counter diagonally left is not red or has no space past it
                    {   
                        diagonalLeft = null;        //no move to right
                    }
                }
            }

            Square diagonalRight;       //the square diagonally right that can be moved to

            if (firstClick.getX() == 9)
            {
                diagonalRight = null;       //counter on right side of board so no move to right
            }
            else
            {
                diagonalRight = squareArray[firstClick.getX() + 1, firstClick.getY() - 1];

                if (diagonalRight.isOccupied())
                {
                    if (diagonalRight.isRed() && diagonalRight.getX() != 9 && diagonalRight.getY() != 0)        //if square diagonally right has a red counter in it and a space past it to jump into
                    {
                        rightCounterToRemove = diagonalRight;       //counter in the way can be taken
                        diagonalRight = squareArray[diagonalRight.getX() + 1, diagonalRight.getY() - 1];
                        rightTaken = true;

                        if (diagonalRight.isOccupied())     //if both squares diagonally right are occupied
                        {
                            diagonalRight = null;           //no move to left
                        }
                    }
                    else    //if counter diagonally left is not red or has no space past it
                    {
                        diagonalRight = null;       //no move to right
                    }
                }
            }

            if (diagonalLeft == null && diagonalRight == null)      //if no valid spaces to move to
            {
                return false;
            }
            else
            {
                if (secondClick == diagonalLeft)        //if space on left
                {
                    if (leftTaken)          //if counter in the way to take
                    {
                        //take counter
                        leftCounterToRemove.setOccupied(false);
                        leftCounterToRemove.setKing(false);
                        leftCounterToRemove.BackgroundImage = Properties.Resources.Greyback;

                        subtract("red");
                    }

                    return true;
                }
                else if (secondClick == diagonalRight)      //if space on right
                {
                    if (rightTaken)         //if counter in the way to take
                    {
                        //take counter
                        rightCounterToRemove.setOccupied(false);
                        rightCounterToRemove.setKing(false);
                        rightCounterToRemove.BackgroundImage = Properties.Resources.Greyback;

                        subtract("red");
                    }

                    return true;
                }
                else        //if player's selection is not a valid move
                {
                    return false;
                }
            }
        }

        /*
         *  Checks whether a regular red counter can move. Can only move diagonally forward 
        */
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

                        subtract("black");
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

                        subtract("black");
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /*
         *  Checks whether a king counter can move. colour parameter indicates the colour of the counter 
        */
        public bool canKingMove(string colour)
        {
            //bool variables indicate whether there are pieces in the way that can be taken
            bool topLeftTaken = false;
            bool topRightTaken = false;
            bool bottomLeftTaken = false;
            bool bottomRightTaken = false;

            //references to counters that can be taken
            Square topLeftToRemove = null;
            Square topRightToRemove = null;
            Square bottomLeftToRemove = null;
            Square bottomRightToRemove = null;

            if (firstClick.getX() == 0 || firstClick.getY() == 0)
            {
                topLeft = null;        //king on left or top side of board, no top left move
            }
            else
            {
                topLeft = squareArray[firstClick.getX() - 1, firstClick.getY() - 1];

                if (topLeft.isOccupied())
                {
                    if (topLeft.isRed() && topLeft.getX() != 0 && topLeft.getY() != 0 && colour == "black")     //if king is black and red piece in the way that can be taken
                    {
                        topLeftToRemove = topLeft;
                        topLeft = squareArray[topLeft.getX() - 1, topLeft.getY() - 1];
                        topLeftTaken = true;

                        if (topLeft.isOccupied())
                        {
                            topLeft = null;         //counter in way of move, no move to top left
                        }
                    }
                    else if (!topLeft.isRed() && topLeft.getX() != 0 && topLeft.getY() != 0 && colour == "red")     //if king is red and black piece in the way that can be taken
                    {
                        topLeftToRemove = topLeft;
                        topLeft = squareArray[topLeft.getX() - 1, topLeft.getY() - 1];
                        topLeftTaken = true;

                        if (topLeft.isOccupied())
                        {
                            topLeft = null;         //counter in way of move, no move to top left
                        }
                    }
                    else
                    {
                        topLeft = null;             //no valid move to top left
                    }
                }
            }

            if (firstClick.getX() == 9 || firstClick.getY() == 0)
            {
                topRight = null;        //king at right or top of board, no move to top right
            }
            else
            {
                topRight = squareArray[firstClick.getX() + 1, firstClick.getY() - 1];

                if (topRight.isOccupied())
                {
                    if (topRight.isRed() && topRight.getX() != 9 && topRight.getY() != 0 && colour == "black")      //if king is black and red piece in the way that can be taken
                    {
                        topRightToRemove = topRight;
                        topRight = squareArray[topRight.getX() + 1, topRight.getY() - 1];
                        topRightTaken = true;

                        if (topRight.isOccupied())
                        {
                            topRight = null;        //counter in way of move, no move to top right
                        }
                    }
                    else if (!topRight.isRed() && topRight.getX() != 9 && topRight.getY() != 0 && colour == "red")      //if king is red and black piece in the way that can be taken
                    {
                        topRightToRemove = topRight;
                        topRight = squareArray[topRight.getX() + 1, topRight.getY() - 1];
                        topRightTaken = true;

                        if (topRight.isOccupied())
                        {
                            topRight = null;        //counter in way of move, no move to top right
                        }
                    }
                    else
                    {
                        topRight = null;            //no valid move to top right
                    }
                }
            }

            if (firstClick.getX() == 0 || firstClick.getY() == 9)
            {
                bottomLeft = null;      //king at left or bottom of board, no move to bottom left
            }
            else
            {
                bottomLeft = squareArray[firstClick.getX() - 1, firstClick.getY() + 1];

                if (bottomLeft.isOccupied())
                {
                    if (bottomLeft.isRed() && bottomLeft.getX() != 0 && bottomLeft.getY() != 9 && colour == "black")        //if king is black and red piece in the way that can be taken
                    {
                        bottomLeftToRemove = bottomLeft;
                        bottomLeft = squareArray[bottomLeft.getX() - 1, bottomLeft.getY() + 1];
                        bottomLeftTaken = true;

                        if (bottomLeft.isOccupied())
                        {
                            bottomLeft = null;          //counter in way of move, no move to bottom left
                        }
                    }
                    else if (!bottomLeft.isRed() && bottomLeft.getX() != 0 && bottomLeft.getY() != 9 && colour == "red")        //if king is red and black piece in the way that can be taken
                    {
                        bottomLeftToRemove = bottomLeft;
                        bottomLeft = squareArray[bottomLeft.getX() - 1, bottomLeft.getY() + 1];
                        bottomLeftTaken = true;

                        if (bottomLeft.isOccupied())
                        {
                            bottomLeft = null;          //counter in way of move, no move to bottom left
                        }
                    }
                    else
                    {
                        bottomLeft = null;          //no valid move to bottom left
                    }
                }
            }

            if (firstClick.getX() == 9 || firstClick.getY() == 9)
            {
                bottomRight = null;         //king at bottom or right of board, no move to bottom right
            }
            else
            {
                bottomRight = squareArray[firstClick.getX() + 1, firstClick.getY() + 1];

                if (bottomRight.isOccupied())
                {
                    if (bottomRight.isRed() && bottomRight.getX() != 9 && bottomRight.getY() != 9 && colour == "black")         //if king is black and red piece in the way that can be taken
                    {
                        bottomRightToRemove = bottomRight;
                        bottomRight = squareArray[bottomRight.getX() + 1, bottomRight.getY() + 1];
                        bottomRightTaken = true;

                        if (bottomRight.isOccupied())
                        {
                            bottomRight = null;         //counter in way of move, no move to bottom right
                        }
                    }
                    else if (!bottomRight.isRed() && bottomRight.getX() != 9 && bottomRight.getY() != 9 && colour == "red")     //if king is red and black piece in the way that can be taken
                    {
                        bottomRightToRemove = bottomRight;
                        bottomRight = squareArray[bottomRight.getX() + 1, bottomRight.getY() + 1];
                        bottomRightTaken = true;

                        if (bottomRight.isOccupied())
                        {
                            bottomRight = null;         //counter in way of move, no move to bottom right
                        }
                    }
                    else
                    {
                        bottomRight = null;         //no valid move to bottom right
                    }
                }
            }

            if (topLeft == null && topRight == null && bottomLeft == null && bottomRight == null)
            {
                return false;           //no valid moves in any direction
            }
            else
            {
                if (secondClick == topLeft)
                {
                    if (topLeftTaken)           //if there is a counter to the top left to take
                    {
                        //take piece in way
                        topLeftToRemove.setOccupied(false);
                        topLeftToRemove.setKing(false);
                        topLeftToRemove.BackgroundImage = Properties.Resources.Greyback;
                        pieceTaken = true;

                        if (colour == "black")
                        {
                            subtract("red");
                        }
                        else
                        {
                            subtract("black");
                        }
                    }

                    return true;
                }
                else if (secondClick == topRight)
                {
                    if (topRightTaken)           //if there is a counter to the top right to take
                    {
                        //take piece in way
                        topRightToRemove.setOccupied(false);
                        topRightToRemove.setKing(false);
                        topRightToRemove.BackgroundImage = Properties.Resources.Greyback;
                        pieceTaken = true;

                        if (colour == "black")
                        {
                            subtract("red");
                        }
                        else
                        {
                            subtract("black");
                        }
                    }

                    return true;
                }
                else if (secondClick == bottomLeft)      //if there is a counter to the bottom left to take
                {
                    if (bottomLeftTaken)
                    {
                        //take piece in way
                        bottomLeftToRemove.setOccupied(false);
                        bottomLeftToRemove.setKing(false);
                        bottomLeftToRemove.BackgroundImage = Properties.Resources.Greyback;
                        pieceTaken = true;

                        if (colour == "black")
                        {
                            subtract("red");
                        }
                        else
                        {
                            subtract("black");
                        }
                    }

                    return true;
                }
                else if (secondClick == bottomRight)         //if there is a counter to the bottom right to take
                {
                    if (bottomRightTaken)
                    {
                        //take piece in way
                        bottomRightToRemove.setOccupied(false);
                        bottomRightToRemove.setKing(false);
                        bottomRightToRemove.BackgroundImage = Properties.Resources.Greyback;
                        pieceTaken = true;

                        if (colour == "black")
                        {
                            subtract("red");
                        }
                        else
                        {
                            subtract("black");
                        }
                    }

                    return true;
                }
                else
                {
                    return false;           //player did not select a valid move
                }
            }
        }

        /*
         *  Called after a player has taken a counter, used to validate whether or not they can take another in the same move
         *  colour parameter indicates the colour of the counter moving
        */
        public bool canStillMove(string colour)
        {
            if (!pieceTaken)        //if no piece was taken in the last move, the chain is broken and player's turn ends
            {
                continueMoving = false;
                topLeft = null;
                topRight = null;
                bottomLeft = null;
                bottomRight = null;
                return false;
            }

            //integers to store the coordinates furthest from the counter, used to validate whether the coordinates are >= 0, i.e. in array bounds
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
                if (leftmost >= 0 && topmost >= 0)      //if square in bounds
                {
                    if (squareArray[leftmost + 1, topmost + 1].isOccupied() && !squareArray[leftmost + 1, topmost + 1].isRed() && !squareArray[leftmost, topmost].isOccupied())     //if piece in the way that can be taken
                    {
                        topLeft = squareArray[leftmost, topmost];       //piece that can be taken
                    }
                }

                if (rightmost <= 9 && topmost >= 0)     //if square in bounds
                {
                    if (squareArray[rightmost - 1, topmost + 1].isOccupied() && !squareArray[rightmost - 1, topmost + 1].isRed() && !squareArray[rightmost, topmost].isOccupied())      //if piece in the way that can be taken
                    {
                        topRight = squareArray[rightmost, topmost];     //piece that can be taken
                    }
                }

                if (leftmost >= 0 && bottommost <= 9)       //if square in bounds
                {
                    if (squareArray[leftmost + 1, bottommost - 1].isOccupied() && !squareArray[leftmost + 1, bottommost - 1].isRed() && !squareArray[leftmost, bottommost].isOccupied())        //if piece in the way that can be taken
                    {
                        bottomLeft = squareArray[leftmost, bottommost];     //piece that can be taken
                    }
                }

                if (rightmost <= 9 && bottommost <= 9)      //if square in bounds
                {
                    if (squareArray[rightmost - 1, bottommost - 1].isOccupied() && !squareArray[rightmost - 1, bottommost - 1].isRed() && !squareArray[rightmost, bottommost].isOccupied())     //if piece in the way that can be taken
                    {
                        bottomRight = squareArray[rightmost, bottommost];       //piece that can be taken
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
                //no pieces to be taken, chain is broken, player's turn ends
                topLeft = null;
                topRight = null;
                bottomLeft = null;
                bottomRight = null;
                return false;
            }
            else
            {
                return true;        //global references to squares set, inform player their turn can continue, they then select which piece to take
            }
        }

        /*
         *  Used to change who's turn it is 
        */
        public void switchTurn()
        {
            if (blackTurn)
            {
                blackTurn = false;
                Turncounter.Text = "RED";       //label indicating who's turn it is
            }
            else
            {
                blackTurn = true;   
                Turncounter.Text = "BLACK";     //label indicating who's turn it is
            }
        }

        /*
         *  Subtracts one from the number of counters remaining of the passed-in colour 
        */
        public void subtract(string colour)
        {
            if (colour == "red")
            {
                redCountersRemaining--;
                Redtakencounter.Text = Convert.ToString(redCountersRemaining);
                pieceTaken = true;

                if (redCountersRemaining == 0)
                {
                    winner("Black");        //no red counters remaining, black is the winner
                }
            }
            else
            {
                blackCountersRemaining--;
                Blacktakencounter.Text = Convert.ToString(blackCountersRemaining);
                pieceTaken = true;

                if (blackCountersRemaining == 0)
                {
                    winner("Red");          //no black counters remaining, red is the winner
                }
            }
        }

        /*
         *  Declares a winner if a player has no counters remaining 
        */
        public void winner(string colour)
        {
            MessageBox.Show(colour + " wins!");     //winning message
            this.Hide();            
            new Menu().Show();          //back to menu
        }

        /*
        *  Used to ensure the program properly ends when the user presses the close button at the top right of the form
        */
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }
    }
}
