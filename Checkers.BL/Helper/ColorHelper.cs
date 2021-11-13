using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.BL.Constants;

namespace Checkers.BL.Helper
{
    public class ColorHelper
    {
        public FigureColor GetFigureColor(char figure)
        {
            if (figure == Figures.WhitePawn || figure==Figures.WhiteQueen)
            {
                return FigureColor.White;
            }
            else if (figure == Figures.BlackPawn || figure == Figures.BlackQueen)
            {
                return FigureColor.Black;
            }

            return FigureColor.Empty;
        }
    }
}
