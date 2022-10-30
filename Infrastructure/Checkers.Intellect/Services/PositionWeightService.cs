using System;
using System.Collections.Generic;
using System.Text;
using Checkers.BL.Constants;

namespace Checkers.BL.Services
{
    public class PositionWeightService : IPositionWeightService
    {
        public int GetWeightForWhite(string boardState)
        {
            int result = 0;
            foreach (var figure in boardState)
            {
                switch (figure)
                {
                    case Figures.BlackQueen: result -= 2; break;
                    case Figures.BlackPawn: result -= 1; break;
                    case Figures.WhiteQueen: result += 2; break;
                    case Figures.WhitePawn: result += 1; break;
                    default: break;
                }
            }

            return result;
        }
    }
}
