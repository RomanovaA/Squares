using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Squares
{
    class GameMass
    {
        //размеры массива
        int massWidth;
        int massHeight;   
    
        static Random rand = new Random();

        //игровой массив
        public char[,] fieldSymbols;
        
        //Конструктор
        public GameMass(int width,int height)
        {
            massHeight = height;
            massWidth = width;
            //инициализация массива
            fieldSymbols = new char[massWidth, massHeight];

            int coloredCount = massHeight / 2;    
            // установка значений элементов массива (кроме последних двух строк) в W (т.е. белый цвет)
            for (int i = 0; i < massWidth; i++)
                for (int j = 0; j < massHeight - coloredCount; j++)
                {
                    fieldSymbols[i, j] = 'W';
                }
            // установка значений последних двух строк массива в случайные (цвета)
            for (int i = 0; i < massWidth; i++)
                for (int j = massHeight - coloredCount; j < massHeight; j++)
                {
                    int randomColor = rand.Next(4);
                    switch (randomColor)
                    {
                        case 0:
                            fieldSymbols[i, j] = 'R';
                            break;
                        case 1:
                            fieldSymbols[i, j] = 'G';
                            break;
                        case 2:
                            fieldSymbols[i, j] = 'B';
                            break;
                        case 3:
                            fieldSymbols[i, j] = 'Y';
                            break;
                    }
                }
        }

        //Инкапсулирование
        public int MassHeight
        {
            get { return massHeight; }
        }
        public int MassWidth
        {
            get { return massWidth; }
        }
    }
}
