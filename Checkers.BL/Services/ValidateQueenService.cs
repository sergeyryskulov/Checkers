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
        public ValidateQueenService()
        {
            ;
        }
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
                return new AllowedVectors()
                {
                    EatFigure = true,
                    Vectors = eatingVectors
                };
            }
            return new AllowedVectors()
            {
                EatFigure = false,
                Vectors = notEatingVectors
            };
        }

        private AllowedVectors GetAllowedVectorsQueenDirection(int coord, string figures, Direction direction)
        {

            int boardWidth = figures.Length.SquareRoot();
            var color = figures[coord].GetFigureColor();
            var oppositeColor = color == FigureColor.White ? FigureColor.Black : FigureColor.White;

            var eatVectors = new List<Vector>();
            var notEatVectors = new List<Vector>();
            var eatFigure = false;

            for (int i = 1; i < boardWidth; i++)
            {
                var vector = new Vector()
                {
                    Length = i,
                    Direction = direction
                };

                var stepCoord = vector.VectorToCoord(coord, boardWidth);
                if (stepCoord == -1)
                {
                    break;
                }

                var figure = figures[stepCoord];

                if (figure.GetFigureColor() == FigureColor.Empty)
                {
                    if (eatFigure)
                    {
                        eatVectors.Add(vector);
                    }
                    else
                    {
                        notEatVectors.Add(vector);
                    }
                }
                else if (figure.GetFigureColor() == oppositeColor)
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
                return new AllowedVectors()
                {
                    EatFigure = true,
                    Vectors = eatVectors
                };
            }

            return new AllowedVectors()
            {
                EatFigure = false,
                Vectors = notEatVectors
            };
        }
     

    }
}
