using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Squares
{
    class SquareImage
    {
        Image[] squareImg = new Image[] {
            Properties.Resources.sq_red,            //0 - red
            Properties.Resources.sq_green,          //1 - green
            Properties.Resources.sq_blue,           //2 - blue
            Properties.Resources.sq_yellow,         //3 - yellow
            Properties.Resources.sq_white,          //4 - white
            Properties.Resources.sq_red_bright,     //5 - bright_red
            Properties.Resources.sq_green_bright,   //6 - bright_green
            Properties.Resources.sq_blue_bright,    //7 - bright_blue
            Properties.Resources.sq_yellow_bright}; //8 - bright_yellow
        
        public Image[] SquareImg
        {
            get { return squareImg; }
        }
    }
}
