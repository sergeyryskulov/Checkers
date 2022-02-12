using System.Collections.Generic;
using Checkers.BL.Constants;
using Checkers.BL.Constants.Enums;
using Checkers.BL.Helper;
using Checkers.BL.Interfaces;
using Checkers.BL.Models;

namespace Checkers.BL.Services
{
    public class ValidateQueenService : IValidateQueenService
    {
        private MathHelper _mathHelper;
        private ColorHelper _colorHelper;
        private VectorHelper _vectorHelper;

        public ValidateQueenService(MathHelper mathHelper, ColorHelper colorHelper, VectorHelper vectorHelper)
        {
            _mathHelper = mathHelper;
            _colorHelper = colorHelper;
            _vectorHelper = vectorHelper;
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

            AllowedVectors result = new AllowedVectors()
            {
                EatFigure = false,
                Vectors = new List<Vector>()
            };

            int boardWidth = _mathHelper.Sqrt(figures.Length);
            var color = _colorHelper.GetFigureColor(figures[coord]);
            var oppositeColor = color == FigureColor.White ? FigureColor.Black : FigureColor.White;

            bool figureHasBeenEated = false;

            for (int i = 1; i < boardWidth; i++)
            {
                var vector = new Vector()
                {
                    Length = i,
                    Direction = direction
                };

                var stepCoord = _vectorHelper.VectorToCoord(coord, vector, boardWidth);
                if (stepCoord == -1)
                {
                    return result;
                }

                var figure = figures[stepCoord];

                if (_colorHelper.GetFigureColor(figure) == FigureColor.Empty)
                {
                    if (figureHasBeenEated)
                    {
                        result.Vectors.Clear();
                        result.EatFigure = true;
                        figureHasBeenEated = false;
                    }

                    result.Vectors.Add(vector);
                }
                else if (_colorHelper.GetFigureColor(figure) == oppositeColor)
                {
                    if (!result.EatFigure)
                    {
                        figureHasBeenEated = true;
                        continue;
                    }
                    else
                    {
                        return result;
                    }
                }
                else
                {
                    return result;
                }

            }

            return result;

        }
     

    }
}
