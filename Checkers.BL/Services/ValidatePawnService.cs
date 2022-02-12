using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Checkers.BL.Constants;
using Checkers.BL.Constants.Enums;
using Checkers.BL.Helper;
using Checkers.BL.Models;

namespace Checkers.BL.Services
{


    public class ValidatePawnService : IValidatePawnService
    {
        private MathHelper _mathHelper;
        private ColorHelper _colorHelper;
        private VectorHelper _vectorHelper;

        public ValidatePawnService(MathHelper mathHelper, ColorHelper colorHelper, VectorHelper vectorHelper)
        {
            _mathHelper = mathHelper;
            _colorHelper = colorHelper;
            _vectorHelper = vectorHelper;
        }

        public AllowedVectors GetAllowedMoveVectors(int coord, string figures)
        {
            int boardWidth = _mathHelper.Sqrt(figures.Length);

            var color = _colorHelper.GetFigureColor(figures[coord]);
            var oppositeColor = color == FigureColor.White ? FigureColor.Black : FigureColor.White;
            var allowedVectors = new List<Vector>();
            foreach (var forwardDirection in GetForwardDirections(color))
            {
                var vectorOneStepForward = new Vector()
                {
                    Direction = forwardDirection,
                    Length = 1
                };
                var coordinateOneStepForward = _vectorHelper.VectorToCoord(coord, vectorOneStepForward, boardWidth);
                if (coordinateOneStepForward == -1)
                {
                    continue;
                }

                if (figures[coordinateOneStepForward] == Figures.Empty)
                {
                    allowedVectors.Add(vectorOneStepForward);
                }
                else if (_colorHelper.GetFigureColor(figures[coordinateOneStepForward]) == oppositeColor)
                {
                    var coordTwoStepForward = _vectorHelper.VectorToCoord(coordinateOneStepForward, vectorOneStepForward, boardWidth);
                    if (coordTwoStepForward == -1)
                    {
                        continue;
                    }

                    if (figures[coordTwoStepForward] == Figures.Empty)
                    {
                        allowedVectors.Add(new Vector()
                        {
                            Length = 2,
                            Direction = vectorOneStepForward.Direction,
                        });
                    }

                }
            }


            foreach (var backwardDirection in GetBackwardDirections(color))
            {

                var vectorOneStepBackward = new Vector()
                {
                    Direction = backwardDirection,
                    Length = 1
                };
                var coordinateOneStepBackward = _vectorHelper.VectorToCoord(coord, vectorOneStepBackward, boardWidth);
                if (coordinateOneStepBackward == -1)
                {
                    continue;
                }

                if (_colorHelper.GetFigureColor(figures[coordinateOneStepBackward]) == oppositeColor)
                {
                    var coordTwoStepBackward = _vectorHelper.VectorToCoord(coordinateOneStepBackward, vectorOneStepBackward, boardWidth);
                    if (coordTwoStepBackward == -1)
                    {
                        continue;
                    }

                    if (figures[coordTwoStepBackward] == Figures.Empty)
                    {
                        allowedVectors.Add(new Vector()
                        {
                            Length = 2,
                            Direction = backwardDirection,
                        });
                    }
                }

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
