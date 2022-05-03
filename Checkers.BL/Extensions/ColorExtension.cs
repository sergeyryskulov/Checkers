﻿using Checkers.BL.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.BL.Extensions
{
    public static class ColorExtension
    {
        public static FigureColor GetFigureColor(this char figure)
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