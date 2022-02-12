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
        private ValidatePawnService _validatePawnService;


        public ValidateService(VectorHelper vectorHelper, MathHelper mathHelper, ColorHelper colorHelper, ValidatePawnService validatePawnService)
        {
            _vectorHelper = vectorHelper;
            _mathHelper = mathHelper;
            _colorHelper = colorHelper;
            _validatePawnService = validatePawnService;
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

            var result = new AllowedVectors()
            {
                Vectors = new List<Vector>(),
                EatFigure = false
            };

            if (figure == Figures.WhitePawn || figure == Figures.BlackPawn)
            {
                result = _validatePawnService.GetAllowedVectorsPawn(coord, figures);
            }
            else if (figure == Figures.WhiteQueen || figure == Figures.BlackQueen)
            {
                result = GetAllowedVectorsQueen(coord, figures, ignoreBlock);
            }

            if (!ignoreBlock && result.EatFigure==false && result.Vectors.Count > 0 && IsBlocked(coord, figures))
            {
                return new AllowedVectors()
                {
                    Vectors = new List<Vector>(),
                    EatFigure = false
                };
            }

            return result;
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

    }
}
