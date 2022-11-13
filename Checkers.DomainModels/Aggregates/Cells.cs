using System;
using System.Collections.Generic;
using System.Text;
using Checkers.Core.Constants;
using Checkers.Core.Constants.Enums;

namespace Checkers.DomainModels.Aggregates
{
    public class Cells
    {
        private string _cells;


        public Cells(string cells)
        {
            _cells = cells;
        }

        public int BoardWidth()
        {
            return SquareRoot(_cells.Length);
        }

        public int Length
        {
            get
            {
                return _cells.Length;
            }
        }

        public override string ToString()
        {
            return _cells.ToString();
        }

        private int SquareRoot(int value)
        {
            if (value == 64)
            {
                return 8;
            }
            return (int)Math.Sqrt(value);
        }

        public FigureColor FigureColorAt(int cellIndex)
        {
            var figure = this[cellIndex];
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

        public  bool WhiteFigureAt(int cellIndex)
        {
            return this[cellIndex] == Figures.WhitePawn || this[cellIndex]== Figures.WhiteQueen;
        }

        public bool BlackFigureAt(int cellIndex)
        {
            return this[cellIndex] == Figures.BlackPawn || this[cellIndex] == Figures.BlackQueen;
        }


        public Figures this [int index]
        {
            get
            {
                return (Figures) _cells[index];
            }
        }
    }
}
