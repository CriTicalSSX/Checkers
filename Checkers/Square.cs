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
    class Square : Button
    {
        bool occupied;
        bool red;
        bool king = false;
        int x;
        int y;

        public Square(int one, int two)
        {
            x = one;
            y = two;
            occupied = true;
        }

        ~Square()
        {
            occupied = false;
            king = false;
        }

        public bool isOccupied()
        {
            return occupied;
        }

        public bool isRed()
        {
            return red;
        }

        public bool isKing()
        {
            return king;
        }

        public void setOccupied(bool x)
        {/*
            if (x)
            {
                if (isRed())
                {
                    this.BackgroundImage = Properties.Resources.Red_Checker;
                }
                else
                {
                    this.BackgroundImage = Properties.Resources.Black_Checker;
                }
            }
            else
            {
                this.BackColor = Color.DarkSlateGray;
                MessageBox.Show("Reached here");
            }*/

            occupied = x;
        }

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

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public void setCurrent()
        {
            if (isRed() && isOccupied())
            {
                this.BackgroundImage = Properties.Resources.Red_Checker_Selected;
            }
            else if (!isRed() && isOccupied())
            {
                this.BackgroundImage = Properties.Resources.Black_Checker_Selected;
            }
        }

        public void removeCurrent()
        {
            if (!isRed() && isOccupied())
            {
                this.BackgroundImage = Properties.Resources.Black_Checker;
            }
            else 
            {
                this.BackgroundImage = Properties.Resources.Red_Checker;
            }
        }
    }
}
