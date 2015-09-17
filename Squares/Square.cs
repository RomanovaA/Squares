using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Squares
{
    class Square
    {
        private SquareImage image;   //цвет квадрата
        private Image currentImg;
        private int xCoord;         //координаты квадрата
        private int yCoord;
        private char symbol;        //символ квадрата

        //Инкапсулирование 
        public Image CurrentImg
        {
            get { return currentImg; }
        }
        public int XCoord
        {
            get { return xCoord; }
            set { xCoord = value; }
        }
        public int YCoord
        {
            get { return yCoord; }
            set { yCoord = value; }
        }
        public char Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }

        //Конструктор
        public Square(char clr)
        {
            image = new SquareImage();
            //установка цвета для квадратика, в зависимости от переданного значения
            SetImage(clr);
        }

        //Движение квадрата вниз
        public void Move(int speed)
        {
            YCoord += speed;
        }
        //установить картинку новому цветному квадрату
        public void SetImage(char clr)
        {
            switch (clr)
            {
                case 'R':
                    currentImg = image.SquareImg[0];
                    symbol = 'R';
                    break;
                case 'G':
                    currentImg = image.SquareImg[1];
                    symbol = 'G';
                    break;
                case 'B':
                    currentImg = image.SquareImg[2];
                    symbol = 'B';
                    break;
                case 'Y':
                    currentImg = image.SquareImg[3];
                    symbol = 'Y';
                    break;
                case 'W':
                    currentImg = image.SquareImg[4];
                    symbol = 'W';
                    break;
                case 'K'://bright_red
                    currentImg = image.SquareImg[5];
                    symbol = 'R';
                    break;
                case 'L'://bright_green
                    currentImg = image.SquareImg[6];
                    symbol = 'G';
                    break;
                case 'M'://bright_blue
                    currentImg = image.SquareImg[7];
                    symbol = 'B';
                    break;
                case 'N'://bright_yellow
                    currentImg = image.SquareImg[8];
                    symbol = 'Y';
                    break;

            }
        }
    }
}
