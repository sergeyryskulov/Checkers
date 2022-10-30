using System.Collections.Generic;
using System.IO;
using Checkers.BL.Constants;
using Checkers.BL.Constants.Enums;
using Checkers.BL.Extensions;
using Checkers.BL.Interfaces;
using Checkers.BL.Models;

namespace Checkers.BL.Services
{
    public class ValidateQueenService : IValidateQueenService
    {                        
      
        public AllowedVectors GetAllowedMoveVectors(int coord, string figures)
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
                var directionAllowedVectores = GetAllowedVectorsQueenDirection(coord, figures, direction);
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

        private AllowedVectors GetAllowedVectorsQueenDirection(int coord, string figures, Direction direction)
        {

            int boardWidth = figures.Length.SquareRoot();
            var color = figures[coord].ToFigureColor();
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

                var iteratedCoord = iteratedVector.ToCoord(coord, boardWidth);
                if (iteratedCoord == -1)
                {
                    break;
                }

                var figure = figures[iteratedCoord];

                if (figure.ToFigureColor() == FigureColor.Empty)
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
                else if (figure.ToFigureColor() == oppositeColor)
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
