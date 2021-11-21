using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.BL.Constants;
using Checkers.BL.Constants.Enums;
using Checkers.BL.Helper;
using Checkers.BL.Models;

namespace Checkers.BL.Services
{
    public class ValidateService
    {
        private VectorHelper _vectorHelper;
        private MathHelper _mathHelper;
        private ColorHelper _colorHelper;


        public ValidateService(VectorHelper vectorHelper, MathHelper mathHelper, ColorHelper colorHelper)
        {
            _vectorHelper = vectorHelper;
            _mathHelper = mathHelper;
            _colorHelper = colorHelper;
        }

        private bool IsBlocked(int coord, string figures)
        {
            var color = _colorHelper.GetFigureColor(figures[coord]);

            for (int figureCoord = 0; figureCoord < figures.Length; figureCoord++)
            {
                var iteratedFigure = figures[figureCoord];

                if (
                    coord != iteratedFigure &&
                    _colorHelper.GetFigureColor(iteratedFigure) == color )
                {
                    GetAllowedMoveVectors(figureCoord, figures, out var isDie, true);
                    if (isDie)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public List<Vector> GetAllowedMoveVectors(int coord, string figures, out bool isDie, bool ignoreBlock = false)
        {
            var figure = figures[coord];
            if (figure == Figures.WhitePawn || figure == Figures.BlackPawn)
            {
                return GetAllowedVectorsPawn(coord, figures, out isDie, ignoreBlock);
            }
            if (figure == Figures.WhiteQueen || figure == Figures.BlackQueen)
            {
                return GetAllowedVectorsQueen(coord, figures, out isDie, ignoreBlock);
            }

            isDie = false;
            return new List<Vector>();
        }
        private List<Vector> GetAllowedVectorsQueen(int coord, string figures, out bool isDieParam, bool ignoreBlock)
        {
            int boardWidth = _mathHelper.Sqrt(figures.Length);

            var color = _colorHelper.GetFigureColor(figures[coord]);
            var oppositeColor = color == FigureColor.White ? FigureColor.Black : FigureColor.White;
            var allowedVectors = new List<Vector>();
            var dieVectors = new List<Vector>();

            foreach (var direction in new[]
            {
                Direction.LeftBottom,
                Direction.LeftTop,
                Direction.RightBottom,
                Direction.RightTop
            })
            {
                bool isDieOnDirection = false;
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
                        break;
                    }

                    var figure = figures[stepCoord];

                    if (_colorHelper.GetFigureColor(figure) == FigureColor.Empty)
                    {
                        if (isDieOnDirection)
                        {
                            dieVectors.Add(vector);
                        }
                        else
                        {
                            allowedVectors.Add(vector);
                        }

                    }
                    else if (_colorHelper.GetFigureColor(figure) == oppositeColor)
                    {
                        if (!isDieOnDirection)
                        {
                            isDieOnDirection = true;
                            continue;

                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }

                }

            }

            if (dieVectors.Count > 0)
            {
                isDieParam = true;
                return dieVectors;
            }
            isDieParam = false;

            if (!ignoreBlock && IsBlocked(coord, figures))
            {
                return new List<Vector>();
            }
            return allowedVectors;

        }

        private List<Vector> GetAllowedVectorsPawn(int coord, string figures, out bool isDie, bool ignoreBlock=false)
        {
            int boardWidth= _mathHelper.Sqrt(figures.Length);

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
                        ;
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
            if (allowedVectors.Exists(m=>m.Length==2))
            {
                isDie = true;
                allowedVectors = allowedVectors.Where(m => m.Length == 2).ToList();
            }
            else
            {
                isDie = false;
                if (!ignoreBlock && IsBlocked(coord, figures))
                {
                    return new List<Vector>();
                }
            }

            return allowedVectors;

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
