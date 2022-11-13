using System;
using System.Collections.Generic;
using System.Text;
using Checkers.Core.Constants;

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
