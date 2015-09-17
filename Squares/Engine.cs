using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Squares
{
    class Engine
    {
        int fieldHeight;
        int fieldWidth;

        int squareHeight;

        GameMass gameMass;
        List<Square> squares;
        Square activeSquare;
        Plus plus;

        public GameStatus gameStatus;
        private Random rand;

        int score;
        int howManyTimes;
        

        //Конструктор
        public Engine(int fieldWidth, int fieldHeight, int squareHeight)
        {
            this.fieldHeight = fieldHeight;
            this.fieldWidth = fieldWidth;

            this.squareHeight = squareHeight;

            plus = new Plus();
            gameMass = new GameMass(fieldWidth,fieldHeight);
            squares = new List<Square>();
            
            rand = new Random();

            CreateSquareList();
            NewActiveSquare();
        }

        //методы исполняемые "по кругу"
        public void Sycle()
        {
            RefreshSquareList();
            NewActiveSquare();
        }
        
        //создать новый управляемый квадратик
        public void NewActiveSquare()
        {
            int randomColor = rand.Next(4);
            char symbol = 'R';
            switch (randomColor)
            {
                case 0:
                    symbol = 'R';
                    break;
                case 1:
                    symbol = 'G';
                    break;
                case 2:
                    symbol = 'B';
                    break;
                case 3:
                    symbol = 'Y';
                    break;
            }
            activeSquare = new Square(symbol);
            activeSquare.XCoord = (FieldWidth / 2) * squareHeight;
            activeSquare.YCoord = 0;
        }
        //создать список статических квадратов (в начале игры)
        private void CreateSquareList()
        {
            char squareSymbol;
            for (int i =0; i<FieldWidth; i++)
                for (int j = 0; j < FieldHeight; j++)
                {
                    squareSymbol = GameMass.fieldSymbols[i, j];
                    squares.Add(new Square(squareSymbol));
                }
            int x = 0;
            for (int i = 0; i < FieldWidth; i++)
                for (int j = 0; j < FieldHeight; j++)
                {
                    Squares[x].XCoord = i * squareHeight;
                    Squares[x].YCoord = j * squareHeight;
                    x++;
                }
        }
        //обновить список квадратов (по ходу игры)
        private void RefreshSquareList()
        {
            char squareSymbol;
            int x = 0;
            for (int i = 0; i < FieldWidth; i++)
                for (int j = 0; j < FieldHeight; j++)
                { 
                    squareSymbol = GameMass.fieldSymbols[i, j];
                    if (squareSymbol != Squares[x].Symbol)
                    {
                        Squares[x].Symbol = squareSymbol;
                        Squares[x].SetImage(squareSymbol);
                    }
                    x++;
                }
            x = 0;
            for (int i = 0; i < FieldWidth; i++)
                for (int j = 0; j < FieldHeight; j++)
                {
                    Squares[x].XCoord = i * squareHeight;
                    Squares[x].YCoord = j * squareHeight;
                    x++;
                }
        }
        //добавить в игровой массив новый цветной квадрат
        private void AddNewColoredSquare(int xCoord, int yCoord, char colorSymbol)
        {
            int x = xCoord / squareHeight;
            int y = yCoord / squareHeight;
            GameMass.fieldSymbols[x, y] = colorSymbol;

            //проверка получившегося массива на "фигурки"
            CheckFugures();
        }

        //проверить, получились ли фигуры
        public void CheckFugures()
        {
            CheckHorizontal();
            CheckVertical();
            CheckBrickAndZigzag();
        }
        //горизонтальные
        private void CheckHorizontal()
        {
            char symbol;
            int scoreCount = 100;
            for (int i = 0; i < FieldWidth - 2; i++)
                for (int j = 0; j < FieldHeight; j++)
                {
                    symbol = GameMass.fieldSymbols[i, j];
                    if (symbol != 'W')  //любой цветной квадрат
                    {
                        if (GameMass.fieldSymbols[i + 1, j] == symbol && GameMass.fieldSymbols[i + 2, j] == symbol)
                        {
                            //*
                            //***
                            if (j != 0 && GameMass.fieldSymbols[i, j - 1] == symbol)
                            {
                                scoreCount = 200;
                                DeleteFugure(i, j - 1, i, j, i + 1, j, i + 2, j, symbol, scoreCount);
                                i = -1;
                                break;
                            }
                            // *
                            //***
                            if (j != 0 && GameMass.fieldSymbols[i + 1, j - 1] == symbol)
                            {
                                scoreCount = 150;
                                DeleteFugure(i + 1, j - 1, i, j, i + 1, j, i + 2, j, symbol, scoreCount);
                                i = -1;
                                break;
                            }
                            //  *
                            //***
                            if (j != 0 && GameMass.fieldSymbols[i + 2, j - 1] == symbol)
                            {
                                scoreCount = 200;
                                DeleteFugure(i + 2, j - 1, i, j, i + 1, j, i + 2, j, symbol, scoreCount);
                                i = -1;
                                break;
                            }
                            //****
                            if (i != 0 && GameMass.fieldSymbols[i - 1, j] == symbol)
                            {
                                scoreCount = 350;
                                DeleteFugure(i - 1, j, i, j, i + 1, j, i + 2, j, symbol, scoreCount);
                                i = -1;
                                break;
                            }
                            //***
                            //*
                            if (j + 1 < FieldHeight && GameMass.fieldSymbols[i, j + 1] == symbol)
                            {
                                scoreCount = 200;
                                DeleteFugure(i, j + 1, i, j, i + 1, j, i + 2, j, symbol, scoreCount);
                                i = -1;
                                break;
                            }
                            //***
                            // *
                            if (j + 1 < FieldHeight && GameMass.fieldSymbols[i + 1, j + 1] == symbol)
                            {
                                scoreCount = 150;
                                DeleteFugure(i + 1, j + 1, i, j, i + 1, j, i + 2, j, symbol, scoreCount);
                                i = -1;
                                break;
                            }
                            //***
                            //  *
                            if (j + 1 < FieldHeight && GameMass.fieldSymbols[i + 2, j + 1] == symbol)
                            {
                                scoreCount = 200;
                                DeleteFugure(i + 2, j + 1, i, j, i + 1, j, i + 2, j, symbol, scoreCount);
                                i = -1;
                                break;
                            }
                        }
                    }
                }
           
        }
        //вертикальные
        private void CheckVertical()
        {
            char symbol;
            int scoreCount = 100;
            for (int i = 0; i < FieldWidth; i++)
                for (int j = 0; j < FieldHeight - 2; j++)
                {
                    symbol = GameMass.fieldSymbols[i, j];
                    if (symbol != 'W')  //любой цветной квадрат
                    {
                        if (GameMass.fieldSymbols[i, j + 1] == symbol && GameMass.fieldSymbols[i, j + 2] == symbol)
                        {
                            //**
                            //*
                            //*
                            if (i + 1 < FieldWidth && GameMass.fieldSymbols[i + 1, j] == symbol)
                            {
                                scoreCount = 200;
                                DeleteFugure(i + 1, j, i, j, i, j + 1, i, j + 2, symbol, scoreCount);
                                i = -1;
                                break;
                            }
                            //*
                            //**
                            //*
                            if (i + 1 < FieldWidth && GameMass.fieldSymbols[i + 1, j + 1] == symbol)
                            {
                                scoreCount = 150;
                                DeleteFugure(i + 1, j + 1, i, j, i, j + 1, i, j + 2, symbol, scoreCount);
                                i = -1;
                                break;
                            }
                            //*
                            //*
                            //**
                            if (i + 1 < FieldWidth && GameMass.fieldSymbols[i + 1, j + 2] == symbol)
                            {
                                scoreCount = 200;
                                DeleteFugure(i + 1, j + 2, i, j, i, j + 1, i, j + 2, symbol, scoreCount);
                                i = -1;
                                break;
                            }
                            //*
                            //*
                            //*
                            //*
                            if (j > 0 && GameMass.fieldSymbols[i, j - 1] == symbol)
                            {
                                scoreCount = 100;
                                DeleteFugure(i, j - 1, i, j, i, j + 1, i, j + 2, symbol, scoreCount);
                                i = -1;
                                break;
                            }
                            //**
                            // *
                            // *
                            if (i > 0 && GameMass.fieldSymbols[i - 1, j] == symbol)
                            {
                                scoreCount = 200;
                                DeleteFugure(i - 1, j, i, j, i, j + 1, i, j + 2, symbol, scoreCount);
                                i = -1;
                                break;
                            }
                            // *
                            //**
                            // *
                            if (i > 0 && GameMass.fieldSymbols[i - 1, j + 1] == symbol)
                            {
                                scoreCount = 150;
                                DeleteFugure(i - 1, j + 1, i, j, i, j + 1, i, j + 2, symbol, scoreCount);
                                i = -1;
                                break;
                            }
                            // *
                            // *
                            //**
                            if (i > 0 && GameMass.fieldSymbols[i - 1, j + 2] == symbol)
                            {
                                scoreCount = 200;
                                DeleteFugure(i - 1, j + 2, i, j, i, j + 1, i, j + 2, symbol, scoreCount);
                                i = -1;
                                break;
                            }
                        }
                    }
                }
        }
        //кубик и зигзаг
        private void CheckBrickAndZigzag()
        {
            char symbol;
            int scoreCount = 100;
            for (int i = 0; i < FieldWidth - 1; i++)
                for (int j = 0; j < FieldHeight - 1; j++)
                {
                    symbol = GameMass.fieldSymbols[i, j];
                    if (symbol != 'W')  //любой цветной квадрат
                    {
                        if (GameMass.fieldSymbols[i + 1, j] == symbol)
                        {
                            //**
                            //**
                            if (GameMass.fieldSymbols[i, j + 1] == symbol && GameMass.fieldSymbols[i + 1, j + 1] == symbol)
                            {
                                scoreCount = 170;
                                DeleteFugure(i, j + 1, i + 1, j + 1, i, j, i + 1, j, symbol, scoreCount);
                                i = -1;
                                break;
                            }
                            //**
                            // **
                            if (i + 2 < FieldWidth && GameMass.fieldSymbols[i + 1, j + 1] == symbol && GameMass.fieldSymbols[i + 2, j + 1] == symbol)
                            {
                                scoreCount = 310;
                                DeleteFugure(i + 1, j + 1, i + 2, j + 1, i, j, i + 1, j, symbol, scoreCount);
                                i = -1;
                                break;
                            }
                            // **
                            //**
                            if (i > 0 && GameMass.fieldSymbols[i - 1, j + 1] == symbol && GameMass.fieldSymbols[i, j + 1] == symbol)
                            {
                                scoreCount = 310;
                                DeleteFugure(i - 1, j + 1, i, j + 1, i, j, i + 1, j, symbol, scoreCount);
                                i = -1;
                                break;
                            }
                            // *
                            //**
                            //*
                            if (j > 0 && GameMass.fieldSymbols[i + 1, j - 1] == symbol && GameMass.fieldSymbols[i, j + 1] == symbol)
                            {
                                scoreCount = 310;
                                DeleteFugure(i + 1, j - 1, i, j + 1, i, j, i + 1, j, symbol, scoreCount);
                                i = -1;
                                break;
                            }
                            //*
                            //**
                            // *
                            if (j > 0 && GameMass.fieldSymbols[i, j - 1] == symbol && GameMass.fieldSymbols[i + 1, j + 1] == symbol)
                            {
                                scoreCount = 310;
                                DeleteFugure(i, j - 1, i + 1, j + 1, i, j, i + 1, j, symbol, scoreCount);
                                i = -1;
                                break;
                            }
                        }
                    }
                }
        }

        //удалить фигуру
        private void DeleteFugure(int coord1X, int coord1Y, int coord2X, int coord2Y, int coord3X, int coord3Y, int coord4X, int coord4Y, char symbol, int xScore)
        {
            //Увеличить счет
            score += (xScore * howManyTimes++);

            //СпецЭффект :)
            SpecialEffects(coord1X, coord1Y, coord2X, coord2Y, coord3X, coord3Y, coord4X, coord4Y, symbol);
            
            GameMass.fieldSymbols[coord1X, coord1Y] = 'W';
            GameMass.fieldSymbols[coord2X, coord2Y] = 'W';
            GameMass.fieldSymbols[coord3X, coord3Y] = 'W';
            GameMass.fieldSymbols[coord4X, coord4Y] = 'W';

            //Изобразить плюсик
            if (howManyTimes > 2)
                PlusEffect(howManyTimes-1, coord1X, coord1Y);

            //установить управляемому квадрату белый цвет, если надо
            if (activeSquare.Symbol != 'W'
                && ((activeSquare.XCoord == coord1X * squareHeight && activeSquare.YCoord == coord1Y * squareHeight)
                || (activeSquare.XCoord == coord2X * squareHeight && activeSquare.YCoord == coord2Y * squareHeight)
                || (activeSquare.XCoord == coord3X * squareHeight && activeSquare.YCoord == coord3Y * squareHeight)
                || (activeSquare.XCoord == coord4X * squareHeight && activeSquare.YCoord == coord4Y * squareHeight)))
            {
                activeSquare.Symbol = 'W';
                activeSquare.SetImage(activeSquare.Symbol);
            }


            RefreshSquareList();
            Thread.Sleep(100);

            for (int i = 0; i < 4; i++)
            {
                PutDownSquares();
                Thread.Sleep(75);
            }
            CheckFugures();
        }
        //Изобразить плюсик
        private void PlusEffect(int number, int xCoord, int yCoord)
        {
            plus.SetImage(number);
            plus.XCoord = xCoord * squareHeight;
            plus.YCoord = yCoord * squareHeight + 25;

            for (int i = 0; i < 16; i++)
            {
                plus.YCoord -= 5;
                Thread.Sleep(40);
            }
            plus.XCoord = -100;
            plus.YCoord = -100;
        }

        //Спецэффект, "мигание" при удалении квадратов
        private void SpecialEffects(int coord1X, int coord1Y, int coord2X, int coord2Y, int coord3X, int coord3Y, int coord4X, int coord4Y, char symbol)
        {
            char newSymbol = 'R';
            for (int i = 0; i < 3; i++)
            {
                switch (symbol)
                {
                    case 'R':
                        newSymbol = 'K';
                        break;
                    case 'G':
                        newSymbol = 'L';
                        break;
                    case 'B':
                        newSymbol = 'M';
                        break;
                    case 'Y':
                        newSymbol = 'N';
                        break;
                }
                ChangeColor(coord1X, coord1Y, coord2X, coord2Y, coord3X, coord3Y, coord4X, coord4Y, newSymbol);
                ChangeColor(coord1X, coord1Y, coord2X, coord2Y, coord3X, coord3Y, coord4X, coord4Y, symbol);
            }
        }
        //Изменить цвет ("мигание")
        private void ChangeColor(int coord1X, int coord1Y, int coord2X, int coord2Y, int coord3X, int coord3Y, int coord4X, int coord4Y, char newSymbol)
        {
            int threadTime = 100;
            GameMass.fieldSymbols[coord1X, coord1Y] = newSymbol;
            GameMass.fieldSymbols[coord2X, coord2Y] = newSymbol;
            GameMass.fieldSymbols[coord3X, coord3Y] = newSymbol;
            GameMass.fieldSymbols[coord4X, coord4Y] = newSymbol;

            //RefreshSquareList();
            for (int i = 0; i < Squares.Count; i++)
            {
                if ((Squares[i].XCoord == coord1X * squareHeight && Squares[i].YCoord == coord1Y * squareHeight)
                    || (Squares[i].XCoord == coord2X * squareHeight && Squares[i].YCoord == coord2Y * squareHeight)
                    || (Squares[i].XCoord == coord3X * squareHeight && Squares[i].YCoord == coord3Y * squareHeight)
                    || (Squares[i].XCoord == coord4X * squareHeight && Squares[i].YCoord == coord4Y * squareHeight))
                {
                    Squares[i].SetImage(newSymbol);
                }

            }

                if (activeSquare.Symbol != 'W'
                    && ((activeSquare.XCoord == coord1X * squareHeight && activeSquare.YCoord == coord1Y * squareHeight)
                    || (activeSquare.XCoord == coord2X * squareHeight && activeSquare.YCoord == coord2Y * squareHeight)
                    || (activeSquare.XCoord == coord3X * squareHeight && activeSquare.YCoord == coord3Y * squareHeight)
                    || (activeSquare.XCoord == coord4X * squareHeight && activeSquare.YCoord == coord4Y * squareHeight)))
                {
                    //ActiveSquare.Symbol = newSymbol;
                    ActiveSquare.SetImage(newSymbol);
                }

            Thread.Sleep(threadTime);
        }

        //"уронить" квадраты
        private void PutDownSquares()
        {
            for (int i = FieldWidth - 1; i >= 0; i--)
                for (int j = FieldHeight - 2; j >= 0; j--)
                {
                    if (GameMass.fieldSymbols[i, j + 1] == 'W')
                    {
                        GameMass.fieldSymbols[i, j + 1] = GameMass.fieldSymbols[i, j];
                        GameMass.fieldSymbols[i, j] = 'W';
                        RefreshSquareList();
                    }
                }
        }
        //Конец игры
        private bool EndGame()
        {
            int x = (FieldWidth / 2);
            int y = 0;
            if (GameMass.fieldSymbols[x, y] != 'W')
                return true;
            return false;
        }

        //Течение игры
        bool isStart = true;
        public void Play()
        {
            while (gameStatus == GameStatus.play)
            {
                howManyTimes = 1;
                
                if (isStart)//в самом начале игры убрать уже сложенные фигуры
                {
                    CheckFugures();
                    isStart = false;
                }

                if (EndGame())//не заполнено ли поле квадратами полностью
                {
                    gameStatus = GameStatus.end_loose;
                    break;
                }

                Thread.Sleep(20);
                bool squareStopped = false;

                for (int i = 0; i < squares.Count; i++)
                    if (squares[i].Symbol != 'W')
                    {
                        if (((squares[i].XCoord == activeSquare.XCoord) && (squares[i].YCoord == (activeSquare.YCoord + squareHeight)))
                            || activeSquare.YCoord == (fieldHeight * squareHeight) - squareHeight) //остановить квадрат, когда он встанет на цветной или на конец поля
                        {
                            AddNewColoredSquare(activeSquare.XCoord, activeSquare.YCoord, activeSquare.Symbol);
                            squareStopped = true;
                        }
                    }
                    else
                    {
                        if (activeSquare.YCoord == (fieldHeight * squareHeight) - squareHeight)
                        {
                            AddNewColoredSquare(activeSquare.XCoord, activeSquare.YCoord, activeSquare.Symbol);
                            squareStopped = true;
                        }
                    }
                if (squareStopped)
                {
                    Sycle();
                    continue;
                }
                activeSquare.Move(5);//движение управляемого квадрата
            }
        }
        //Начать новую игру
        public void NewGame()
        {
            gameMass = new GameMass(fieldWidth, fieldHeight);
            //CreateSquareList();
            NewActiveSquare();
            RefreshSquareList();
            isStart = true;
            score = 0;
        }        

        //Инкапсулирование
        internal GameMass GameMass
        {
            get { return gameMass; }
            set { gameMass = value; }
        }
        internal List<Square> Squares
        {
            get { return squares; }
            set { squares = value; }
        }
        internal Square ActiveSquare
        {
            get { return activeSquare; }
        }
        internal Plus Plus
        {
            get { return plus; }
            set { plus = value; }
        }
        public int FieldHeight
        {
            get {return fieldHeight; }
        }
        public int FieldWidth
        {
            get { return fieldWidth; }
        }
        public int Score
        {
            get { return score; }
            set { score = value; }
        }
        public int HowManyTimes
        {
            get { return howManyTimes; }
            set { howManyTimes = value; }
        }
        public int SquareHeight
        {
            get { return squareHeight; }
            set { squareHeight = value; }
        }
    }
}
