using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Checkers
{
    /*
     *  Self-defined class that extends the Button class. Used in a 2D array in the Checkers class to represent
     *  the checkers board.
    */
    class Square : Button
    {
        bool occupied;          //indicates the square has a counter on it
        bool red;               //indicates whether the counter on the square is red or black
        bool king = false;      //indicates whether the counter on thr square is a king or not
        int x;                  
        int y;                  //the x and y coordinates of the square in the board are stored as fields in
                                //the object. They each have accessor functions and are used to calculate the
                                //position of a moving counter on the board

        /*
         *  Constructor, requires the x and y coordinates of the object's position in the grid to be passed in 
        */
        public Square(int xCo, int yCo)
        {
            x = xCo;
            y = yCo;
            occupied = true;            //new square is occupied by default
        }

        /*
         * Important in determining whether the square has a counter on it or not
        */
        public bool isOccupied()
        {
            return occupied;
        }

        /*
         *  Important in determining whether the counter may be taken or not 
        */
        public bool isRed()
        {
            return red;
        }

        /*
         *  Determines the direction the counter may move in, can move any diagonal direction if king 
        */
        public bool isKing()
        {
            return king;
        }

        /*
         *  If a counter is moving, the square it is moving to needs to be set as occupied. Its background image
         *  is determined by other internal fields.
        */
        public void setOccupied(bool x)
        {
            occupied = x;

            if (isKing() && isRed())
            {
                this.BackgroundImage = Properties.Resources.Red_King;
            }
            else if (isKing() && !isRed())
            {
                this.BackgroundImage = Properties.Resources.Black_King;
            }
        }

        /*
         *  Sets the colour of a counter. Its background image is determined by this 
        */
        public void setRed(bool x)
        {
            if (x)
            {
                this.BackgroundImage = Properties.Resources.Red_Checker;
            }
            else
            {
                this.BackgroundImage = Properties.Resources.Black_Checker;
            }

            red = x;
        }

        /*
         *  Sets a counter as a king. Its background image is determined by this 
        */
        public void setKing(bool x)
        {
            if (isRed() && x)
            {
                this.BackgroundImage = Properties.Resources.Red_King;
            }
            else if (!isRed() && x)
            {
                this.BackgroundImage = Properties.Resources.Black_King;
            }

            king = x;
        }

        /*
         * Used to determine the counter's position
        */
        public int getX()
        {
            return x;
        }

        /*
         *  Used to determine the counter's position 
        */
        public int getY()
        {
            return y;
        }

        /*
         *  Any square with current equal to true has a yellow border placed around its counter. This is used
         *  to tell the player which counter they have selected
        */
        public void setCurrent()
        {
            if (isRed() && isOccupied() && isKing())
            {
                this.BackgroundImage = Properties.Resources.Red_King_Selected;
            }
            else if (!isRed() && isOccupied() && isKing())
            {
                this.BackgroundImage = Properties.Resources.Black_King_Selected;
            }
            else if (isRed() && isOccupied() && !isKing())
            {
                this.BackgroundImage = Properties.Resources.Red_Checker_Selected;
            }
            else if (!isRed() && isOccupied() && !isKing())
            {
                this.BackgroundImage = Properties.Resources.Black_Checker_Selected;
            }
        }

        /*
         *  Removes the yellow border from around a counter 
        */
        public void removeCurrent()
        {
            if (!isRed() && isOccupied() && !isKing())
            {
                this.BackgroundImage = Properties.Resources.Black_Checker;
            }
            else if (isRed() && isOccupied() && !isKing())
            {
                this.BackgroundImage = Properties.Resources.Red_Checker;
            }
            else if (!isRed() && isOccupied() && isKing())
            {
                this.BackgroundImage = Properties.Resources.Black_King;
            }
            else if (isRed() && isOccupied() && isKing())
            {
                this.BackgroundImage = Properties.Resources.Red_King;
            }
        }
    }
}
