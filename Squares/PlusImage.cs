using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Squares
{
    class PlusImage
    {
        Image[] plusImg = new Image[] {
            Properties.Resources.__2,   //0 - +2
            Properties.Resources.__3,   //1 - +3
            Properties.Resources.__4,   //2 - +4
            Properties.Resources.__5,   //3 - +5
            Properties.Resources.__6};  //8 - +6

        public Image[] PlusImg
        {
            get { return plusImg; }
        }
    }
}
