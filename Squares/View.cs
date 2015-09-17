using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Squares
{
    partial class View : UserControl
    {
        // длина стороны квадратика
        int WIDTH;
        // длина стороны плюсика
        const int PLUSWIDTH = 75;

        private int fieldHeight, fieldWidth;
        Engine engine;

        //Конструктор
        public View(int fieldHeight, int fieldWidth, int squareHeight, Engine engine)
        {
            InitializeComponent();
            this.fieldHeight = fieldHeight;
            this.fieldWidth = fieldWidth;
            this.Height = FieldHeight * 100;
            this.Width = FieldWidth * 100;
            this.engine = engine;

            WIDTH = squareHeight;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawField(e);//Визуализация массива
            DrawActiveSquare(e);//Визуализация управляемого квадратика
            DrawPlus(e);//Визуализация плюсика
            
            if (engine.gameStatus != GameStatus.play)
                return;
            Thread.Sleep(20);
            Invalidate();//перерисовывает (обновляет) форму
        }

        //Визуализация массива
        private void DrawField(PaintEventArgs e)
        {
            int x = 0;
            for (int i = 0; i < FieldWidth; i++)
                for (int j = 0; j < FieldHeight; j++)
                {
                    e.Graphics.DrawImage(engine.Squares[x].CurrentImg, new Rectangle(engine.Squares[x].XCoord, engine.Squares[x].YCoord, WIDTH, WIDTH));
                    x++;
                }

        }
        //Визуализация управляемого квадратика
        private void DrawActiveSquare(PaintEventArgs e)
        {
            e.Graphics.DrawImage(engine.ActiveSquare.CurrentImg, new Rectangle(engine.ActiveSquare.XCoord, engine.ActiveSquare.YCoord, WIDTH, WIDTH));
        }
        //Визуализация плюсика
        private void DrawPlus(PaintEventArgs e)
        {
            e.Graphics.DrawImage(engine.Plus.CurrentImg, new Rectangle(engine.Plus.XCoord, engine.Plus.YCoord, PLUSWIDTH, PLUSWIDTH));
        }

        //Инкапсулирование
        public int FieldHeight
        {
            get { return fieldHeight; }
        }
        public int FieldWidth
        {
            get { return fieldWidth; }
        }
    }

}
