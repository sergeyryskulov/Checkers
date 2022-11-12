using Checkers.Core.Constants;
using Checkers.DomainModels.Aggregates;
using Checkers.Intellect.Interfaces;

namespace Checkers.Intellect.Services
{
    public class PositionWeightService : IPositionWeightService
    {
        public int GetWeightForWhite(Cells cells)
        {
            int result = 0;
            for (int cellIndex = 0; cellIndex < cells.Length;cellIndex++)
            {
                switch (cells[cellIndex])
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
