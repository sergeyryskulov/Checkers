using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
                    if (GetAllowedMoveVectors(figureCoord, figures, true).EatFigure)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public AllowedVectors GetAllowedMoveVectors(int coord, string figures, bool ignoreBlock = false)
        {
            var figure = figures[coord];
          
            if (figure == Figures.WhitePawn || figure == Figures.BlackPawn)
            {
                return GetAllowedVectorsPawn(coord, figures, ignoreBlock);
            }
            else if (figure == Figures.WhiteQueen || figure == Figures.BlackQueen)
            {
                return  GetAllowedVectorsQueen(coord, figures, ignoreBlock);
            }

            return new AllowedVectors()
            {
                Vectors = new List<Vector>(),
                EatFigure = false
            };
        }

        private AllowedVectors ComposeAllowedVectors(List<Vector> eatingVectors, List<Vector> notEatingVectors, bool ignoreBlock, int coord, string figures)
        {

            if (eatingVectors.Count > 0)
            {
                return new AllowedVectors()
                {
                    EatFigure = true,
                    Vectors = eatingVectors
                };
            }

            if (!ignoreBlock && IsBlocked(coord, figures))
            {
                return new AllowedVectors()
                {
                    EatFigure = false,
                    Vectors = new List<Vector>()
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
        private AllowedVectors  GetAllowedVectorsQueen(int coord, string figures, bool ignoreBlock)
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

            return ComposeAllowedVectors(eatingVectors, notEatingVectors, ignoreBlock, coord, figures);
        }

      
        private AllowedVectors GetAllowedVectorsPawn(int coord, string figures, bool ignoreBlock=false)
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

            return ComposeAllowedVectors(
                allowedVectors.Where(m => m.Length == 2).ToList(),
                allowedVectors.Where(m => m.Length != 2).ToList(),
                ignoreBlock, coord, figures);
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
