using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Squares
{
    public partial class GameForm : Form
    {
        //размеры поля
        int fieldHeight = 8; 
        int fieldWidth = 4;

        int squareHeight; 

        Engine engine;
        View view;
        Thread thread;
        System.Threading.Timer ticker;
        System.Threading.Timer tickerScore;

        delegate void Invoker();

        //сообщения
        const string LOOSING1 = "Sorry, you lose :(\n";
        const string LOOSING2 = "\nDo you want to start a new game?";
        const string WINDOW = "Colored Squares";
        const string QUITTEXT = "Do you want to leave the game?";
        const string STARTTEXT = "Pack colored squares in the shapes of Tetris!";

        double screenHeight = 800;

        //Конструктор
        public GameForm()
        {
            InitializeComponent();

            //установка размера кубика, в зависимости от высоты экрана
            int tempX = (int)screenHeight / fieldHeight;
            if (tempX <= 100)
            {
                if (tempX % 2 == 0)
                    squareHeight = tempX - 20;
                else
                    squareHeight = tempX - 21;
            }
            else
                squareHeight = 100;

            //Установка размеров формы
            this.Width = fieldWidth * squareHeight + 100;
            this.Height = fieldHeight * squareHeight + 28 + 50;

            this.panel.Top = 0;
            this.panel.Left = fieldWidth * squareHeight;
            this.panel.Width = 100;
            this.panel.Height = fieldHeight * squareHeight + 28 + 50;

            this.pointsLbl.Top = fieldHeight * squareHeight;
            this.pointsLbl.Left = 0;
            this.pointsLbl.Width = fieldWidth * squareHeight;
            this.pointsLbl.Height = 50;
            this.pointsLbl.Text = "0";



            //создание и запуск Движка :)
            engine = new Engine(fieldWidth, fieldHeight, squareHeight);
            engine.gameStatus = GameStatus.pause;

            ticker = new System.Threading.Timer(IsEndGame, null, 1000, 1000);
            tickerScore = new System.Threading.Timer(ScoreChange, null, 50, 50);

            //добавление визуального компонента
            view = new View(fieldHeight, fieldWidth, squareHeight, engine);
            this.Controls.Add(view);

            engine.Score = 0;

            //MessageBox.Show(Convert.ToString(squareHeight), WINDOW, MessageBoxButtons.YesNo);

        }

        private void startPauseBtn_Click(object sender, EventArgs e)
        {
            if (engine.gameStatus == GameStatus.play)//режим паузы
           { 
                engine.gameStatus = GameStatus.pause;
                startPauseBtn.Image = Properties.Resources.btn_on;
                thread.Abort();
            }
            else//режим игры
            {
                startPauseBtn.Image = Properties.Resources.btn_pause_on;
                engine.gameStatus = GameStatus.play;
                thread = new Thread(engine.Play);
                thread.Start();
                view.Invalidate();
            }
        }

        //Если игра проиграна
        private void IsEndGame(object state)
        {
            if (engine.gameStatus == GameStatus.end_loose)
            {
                ticker.Dispose();
                //tickerScore.Dispose();

                DialogResult dr = MessageBox.Show(LOOSING1 + "Your score: " + engine.Score + LOOSING2, WINDOW, MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    StartGame();                    
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        //Изменение счета
        private void ScoreChange(object state)
        {
            if (engine.Score != Convert.ToInt32(pointsLbl.Text))
            {
                Invoke(new Invoker(SetScore));
                
            }
        }
        
        private void SetScore()
        {
            pointsLbl.Text = engine.Score.ToString();
        }

        //управление клавишами
        private void KeyControl(KeyPressEventArgs e)
        {
            int coord = engine.ActiveSquare.XCoord;
            bool exist= false;
            switch (e.KeyChar)
            {
                case 'A':
                case 'a':
                case 'Ф':
                case 'ф': //подвинуть влево
                    for (int i = 0; i < engine.Squares.Count; i++)
                    {
                        if (engine.Squares[i].Symbol != 'W')
                        {
                            if ((engine.ActiveSquare.XCoord - engine.Squares[i].XCoord) == squareHeight &&
                                (engine.ActiveSquare.YCoord - engine.Squares[i].YCoord) > -squareHeight &&
                                (engine.ActiveSquare.YCoord - engine.Squares[i].YCoord) <= 0)
                            {
                                exist = true;
                                break;
                            }
                        }
                    }
                    if (coord > 0 && !exist)
                        engine.ActiveSquare.XCoord -= squareHeight;
                    break;
                case 'D':
                case 'd':
                case 'В':
                case 'в'://подвинуть вправо
                    for (int i = 0; i < engine.Squares.Count; i++)
                    {
                        if (engine.Squares[i].Symbol != 'W')
                        {
                            if ((engine.ActiveSquare.XCoord - engine.Squares[i].XCoord) == -squareHeight &&
                                (engine.ActiveSquare.YCoord - engine.Squares[i].YCoord) > -squareHeight &&
                                (engine.ActiveSquare.YCoord - engine.Squares[i].YCoord) <= 0)
                            {
                                exist = true;
                                break;
                            }
                        }
                    }
                    if ((coord < (fieldWidth * squareHeight) - squareHeight) && !exist)
                        engine.ActiveSquare.XCoord += squareHeight; 
                    break;
                case 'S':
                case 's':
                case 'Ы':
                case 'ы'://быстро опустить вниз
                    for (int i = 0; i < engine.Squares.Count; i++)
                    {
                        //label3.Text = engine.Squares.Count.ToString();
                        if (engine.Squares[i].Symbol != 'W')
                        {
                            
                            /*
                            bool res1 = engine.Squares[i].XCoord == engine.ActiveSquare.XCoord;
                            bool res2 = engine.ActiveSquare.YCoord >= engine.Squares[i].YCoord - (squareHeight + (squareHeight / 2));
                            if (res1)
                                label1.Text = "res1 true";
                            else
                                label1.Text = "res1 false";
                            if (res2)
                                label2.Text = "res2 true";
                            else
                                label2.Text = "res2 false";

                            */
                            if (((engine.Squares[i].XCoord == engine.ActiveSquare.XCoord) && (engine.ActiveSquare.YCoord >= engine.Squares[i].YCoord - (squareHeight + (squareHeight/2))))
                            || (engine.ActiveSquare.YCoord >= (fieldHeight * squareHeight) - (squareHeight + (squareHeight/2))))
                            {
                                /*label1.Text = engine.ActiveSquare.YCoord.ToString();
                                int res = engine.Squares[i].YCoord - (squareHeight + (squareHeight / 2));
                                label2.Text = engine.Squares[i].YCoord.ToString() + ":" + i.ToString();
                                engine.Squares[i].SetImage('M');*/
                                exist = true;
                                break;
                            }
                        }
                    }
                    if (!exist)
                        engine.ActiveSquare.Move(squareHeight/2);
                    break;
            }
        }
        private void startPauseBtn_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyControl(e);
        }

        private void startPauseBtn_MouseEnter(object sender, EventArgs e)
        {
            if (engine.gameStatus == GameStatus.play)//режим игры
            {
                startPauseBtn.Image = Properties.Resources.btn_pause_enter;
            }
            else//режим паузы
            {
                startPauseBtn.Image = Properties.Resources.btn_enter;
            }
        }
        private void startPauseBtn_MouseLeave(object sender, EventArgs e)
        {
            if (engine.gameStatus == GameStatus.play)//режим игры
            {
                startPauseBtn.Image = Properties.Resources.btn_pause_on;
            }
            else//режим паузы
            {
                startPauseBtn.Image = Properties.Resources.btn_on;
            }
        }
        private void startPauseBtn_EnabledChanged(object sender, EventArgs e)
        {
            if (startPauseBtn.Enabled)
                startPauseBtn.Image = Properties.Resources.btn_on;
            else
                startPauseBtn.Image = Properties.Resources.btn_off;
        }

        //Начать игру (стартовые инициализации)
        private void StartGame()
        {
            engine.gameStatus = GameStatus.play;
            engine.NewGame();
            thread = new Thread(engine.Play);
            ticker = new System.Threading.Timer(IsEndGame, null, 1000, 1000);
            thread.Start();
            view.Invalidate();
        }

        //Выход из игры
        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool isLoose = false;
            if (thread != null)
            {
                thread.Abort();
                if (engine.gameStatus == GameStatus.end_loose)
                    isLoose = true;
                engine.gameStatus = GameStatus.pause;
            }

            DialogResult dr = MessageBox.Show(QUITTEXT, WINDOW, MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                e.Cancel = false;
                ticker.Dispose();                
            }
            else
            {
                if (isLoose)
                    StartGame();
                e.Cancel = true;
            }
        }
    }
}
