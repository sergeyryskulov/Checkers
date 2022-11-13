using Checkers.Core.Constants;
using Checkers.DomainModels.Aggregates;
using Checkers.Intellect.Interfaces;

namespace Checkers.Intellect.Services
{
    public class PositionWeightService : IPositionWeightService
    {
        public int GetWeightForWhite(Board board)
        {
            int result = 0;
            for (int cellIndex = 0; cellIndex < board.CellsCount;cellIndex++)
            {

                result += GetWeight(board, cellIndex);
            }

            return result;
        }

        private int GetWeight(Board board, int cellIndex)
        {
            if (board.EmptyCellAt(cellIndex))
            {
                return 0;
            }

            int result = 0;
            if (board.PawnAt(cellIndex))
            {
                result = 1;
            }

            if (board.QueenAt(cellIndex))
            {
                result = 2;
            }

            if (board.BlackFigureAt(cellIndex))
            {
                result = -1 * result;
            }

            return result;
        }
    }
}
