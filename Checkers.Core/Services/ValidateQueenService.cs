using System.Collections.Generic;
using Checkers.DomainModels;
using Checkers.DomainModels.Enums;
using Checkers.DomainModels.Models;
using Checkers.Rules.Enums;
using Checkers.Rules.Extensions;
using Checkers.Rules.Interfaces;
using Checkers.Rules.Models;

namespace Checkers.Rules.Services
{
    public class ValidateQueenService : IValidateQueenService
    {                        
      
        public AllowedVectors GetAllowedMoveVectors(Board board, int fromPosition)
        {

            List<Vector> eatingVectors = new List<Vector>();
            List<Vector> notEatingVectors = new List<Vector>();

            foreach (var direction in new[]
                     {
                         Direction.LeftBottom,
                         Direction.LeftTop,
                         Direction.RightBottom,
                         Direction.RightTop })
            {
                var directionAllowedVectores = GetAllowedVectorsForQueenDirection(board, fromPosition, direction);
                if (directionAllowedVectores.EatFigure)
                {
                    eatingVectors.AddRange(directionAllowedVectores.Vectors);
                }
                else
                {
                    notEatingVectors.AddRange(directionAllowedVectores.Vectors);
                }
            }

            if (eatingVectors.Count > 0)
            {
                return new AllowedVectors(eatingVectors, true);
            }
            return new AllowedVectors(notEatingVectors, false);
        }

        private AllowedVectors GetAllowedVectorsForQueenDirection(Board board, int fromPosition, Direction direction)
        {
            int boardWidth = board.BoardWidth;
            var color = board.FigureColorAt(fromPosition);
            var oppositeColor = color == FigureColor.White ? FigureColor.Black : FigureColor.White;

            var eatVectors = new List<Vector>();
            var notEatVectors = new List<Vector>();
            var eatFigure = false;

            for (int iteratedVectorLength = 1; iteratedVectorLength < boardWidth; iteratedVectorLength++)
            {
                var iteratedVector = new Vector(
                    direction,
                    iteratedVectorLength
                );

                var iteratedCoord = iteratedVector.ToPosition(fromPosition, boardWidth);
                if (iteratedCoord == -1)
                {
                    break;
                }

                var figureColor = board.FigureColorAt(iteratedCoord);

                if (figureColor == FigureColor.Empty)
                {
                    if (eatFigure)
                    {
                        eatVectors.Add(iteratedVector);
                    }
                    else
                    {
                        notEatVectors.Add(iteratedVector);
                    }
                }
                else if (figureColor == oppositeColor)
                {
                    if (eatFigure)
                    {
                        break;
                    }

                    eatFigure = true;
                }
                else
                {
                    break;
                }

            }

            if (eatVectors.Count > 0)
            {
                return new AllowedVectors(eatVectors, true);
            }

            return new AllowedVectors(notEatVectors, false);
        }
     

    }
}
