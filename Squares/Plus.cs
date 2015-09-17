using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Squares
{
    class Plus
    {
        private PlusImage image;   //цвет плюсика
        private Image currentImg;
        private int xCoord;         //координаты плюсика
        private int yCoord;

        public Plus()
        {
            image = new PlusImage();
            SetImage(2);
            xCoord = -100;
            yCoord = -100;
        }

        //установить изображение в зависимости от переданной цифры
        public void SetImage(int number)
        {
            switch (number)
            {
                case 2:
                    currentImg = image.PlusImg[0];
                    break;
                case 3:
                    currentImg = image.PlusImg[1];
                    break;
                case 4:
                    currentImg = image.PlusImg[2];
                    break;
                case 5:
                    currentImg = image.PlusImg[3];
                    break;
                case 6:
                    currentImg = image.PlusImg[4];
                    break;
            }
        }

        //инкапсулирование
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
    }
}
