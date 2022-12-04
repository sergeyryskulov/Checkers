using System;
using System.Collections.Generic;
using System.Text;
using Checkers.Core.Constants;
using Checkers.Core.Constants.Enums;

namespace Checkers.DomainModels.Aggregates
{
    public class Board
    {
        private string _cells;

        private enum Figures
        {
            WhitePawn = 'P',
            BlackPawn = 'p',
            WhiteQueen = 'Q',
            BlackQueen = 'q',
            Empty = '1'
        }

        public Board(string cells)
        {
            _cells = cells;
        }

        public int CellsCount
        {
            get
            {
                return _cells.Length;
            }
        }

        public int BoardWidth
        {
            get
            {
                return SquareRoot(_cells.Length);
            }
        }

        public void MoveFigure(int fromPosition, int toPosition, bool convertToQueen)
        {            
            Figures resultFigure = (Figures)_cells[fromPosition];
            if (convertToQueen)
            {
                if (resultFigure == Figures.WhitePawn)
                {
                    resultFigure = Figures.WhiteQueen;
                }
                if (resultFigure == Figures.BlackPawn)
                {
                    resultFigure = Figures.BlackQueen;
                }
            }

            StringBuilder stringBuilder = new StringBuilder(_cells);
            stringBuilder[fromPosition] = (char) Figures.Empty;
            stringBuilder[toPosition] = (char)resultFigure;
            _cells = stringBuilder.ToString();
        }

        public void DeleteFigure(int position)
        {           
            StringBuilder stringBuilder = new StringBuilder(_cells);
            stringBuilder[position] = (char)Figures.Empty;            
            _cells = stringBuilder.ToString();
        }

        public bool ContainsAnyWhiteFigure()
        {
            return _cells.Contains((char)Figures.WhitePawn) || _cells.Contains((char)Figures.WhiteQueen);
        }

        public bool ContainsAnyBlackFigure()
        {
            return _cells.Contains((char)Figures.BlackPawn) || _cells.Contains((char)Figures.BlackQueen);
        }

        public FigureColor FigureColorAt(int position)
        {
            var figure = GetFigureAt(position);
            if (figure == Figures.WhitePawn || figure == Figures.WhiteQueen)
            {
                return FigureColor.White;
            }
            else if (figure == Figures.BlackPawn || figure == Figures.BlackQueen)
            {
                return FigureColor.Black;
            }

            return FigureColor.Empty;
        }

        public bool IsPawnAt(int position)
        {
            var figure = GetFigureAt(position);
            return figure == Figures.WhitePawn || figure == Figures.BlackPawn;
        }

        public bool IsQueenAt(int position)
        {
            var figure = GetFigureAt(position);
            return figure == Figures.WhiteQueen || figure == Figures.BlackQueen;
        }

        public  bool IsWhiteFigureAt(int position)
        {
            var figure = GetFigureAt(position); 
            return figure == Figures.WhitePawn || figure == Figures.WhiteQueen;
        }

        public bool IsBlackFigureAt(int position)
        {
            var figure = GetFigureAt(position);
            return figure == Figures.BlackPawn || figure == Figures.BlackQueen;
        }

        public bool IsEmptyCellAt(int position)
        {
            return GetFigureAt(position) == Figures.Empty;
        }

        public override string ToString()
        {
            return _cells.ToString();
        }

        private Figures GetFigureAt(int position)
        { 
            return (Figures) _cells[position];
        }

        private int SquareRoot(int value)
        {
            if (value == 64)
            {
                return 8;
            }
            return (int)Math.Sqrt(value);
        }
    }
}
