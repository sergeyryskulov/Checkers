using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Checkers.BL.Constants;
using Checkers.BL.Constants.Enums;
using Checkers.BL.Extensions;
using Checkers.BL.Models;

namespace Checkers.BL.Services
{

    public class ValidatePawnService : IValidatePawnService
    {                        
        public ValidatePawnService()
        {
            ;
        }

        public AllowedVectors GetAllowedMoveVectors(int coord, string figures)
        {
            var color = figures[coord].ToFigureColor();
            var allowedVectors = new List<Vector>();
            foreach (var forwardDirection in GetForwardDirections(color))
            {
                allowedVectors.AddRange(GetAllowedVectorsForForwardDirection(coord, figures, forwardDirection));
            }

            foreach (var backwardDirection in GetBackwardDirections(color))
            {
                allowedVectors.AddRange(GetAllowedVectorsForBackwardDirection(coord, figures, backwardDirection));

            }

            var eatingVectors = allowedVectors.Where(m => m.Length == 2).ToList();

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
                Vectors = allowedVectors
            };
        }

        private List<Vector> GetAllowedVectorsForForwardDirection(int coord, string figures, Direction forwardDirection)
        {
            int boardWidth = figures.Length.SquareRoot();
            var color = figures[coord].ToFigureColor();
            var oppositeColor = color == FigureColor.White ? FigureColor.Black : FigureColor.White;

            var vectorOneStepForward = new Vector()
            {
                Direction = forwardDirection,
                Length = 1
            };
            var coordinateOneStepForward = vectorOneStepForward.VectorToCoord(coord, boardWidth);
            if (coordinateOneStepForward == -1)
            {
                return new List<Vector>();
            }

            var result = new List<Vector>();
            if (figures[coordinateOneStepForward] == Figures.Empty)
            {
                result.Add(vectorOneStepForward);
            }
            else if (figures[coordinateOneStepForward].ToFigureColor() == oppositeColor)
            {
                var coordTwoStepForward = vectorOneStepForward.VectorToCoord(coordinateOneStepForward, boardWidth);
                if (coordTwoStepForward == -1)
                {
                    return result;
                }

                if (figures[coordTwoStepForward] == Figures.Empty)
                {
                    result.Add(new Vector()
                    {
                        Length = 2,
                        Direction = vectorOneStepForward.Direction,
                    });
                }
            }

            return result;
        }

        private List<Vector> GetAllowedVectorsForBackwardDirection(int coord, string figures, Direction backwardDirection)
        {
            int boardWidth = figures.Length.SquareRoot();
            var color = figures[coord].ToFigureColor();
            var oppositeColor = color == FigureColor.White ? FigureColor.Black : FigureColor.White;

            var vectorOneStepBackward = new Vector()
            {
                Direction = backwardDirection,
                Length = 1
            };
            var result = new List<Vector>();

            var coordinateOneStepBackward = vectorOneStepBackward.VectorToCoord(coord,  boardWidth);
            if (coordinateOneStepBackward == -1)
            {
                return result;
            }

            if (figures[coordinateOneStepBackward].ToFigureColor() == oppositeColor)
            {
                var coordTwoStepBackward = vectorOneStepBackward.VectorToCoord(coordinateOneStepBackward, boardWidth);
                if (coordTwoStepBackward == -1)
                {
                    return result;
                }

                if (figures[coordTwoStepBackward] == Figures.Empty)
                {
                    result.Add(new Vector()
                    {
                        Length = 2,
                        Direction = backwardDirection,
                    });
                }
            }

            return result;
        }
        private Direction[] GetForwardDirections(FigureColor color)
        {
            if (color == FigureColor.White)
            {
                return new[]
                {
                    Direction.LeftTop,
                    Direction.RightTop
                };
            }
            return new[]
            {
                Direction.LeftBottom,
                Direction.RightBottom
            };

        }

        private Direction[] GetBackwardDirections(FigureColor color)
        {
            if (color == FigureColor.White)
            {
                return new[]
                {
                    Direction.LeftBottom,
                    Direction.RightBottom
                };
            }
            return new[]
            {
                Direction.LeftTop,
                Direction.RightTop
            };

        }
    }
}
