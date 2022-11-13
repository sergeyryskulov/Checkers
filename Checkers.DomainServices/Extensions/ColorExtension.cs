using Checkers.Core.Constants;
using Checkers.Core.Constants.Enums;

namespace Checkers.Core.Extensions
{
    public static class ColorExtension
    {
        public static FigureColor ToFigureColor(this Figures figure)
        {
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
    }
}
