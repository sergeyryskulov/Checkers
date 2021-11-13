﻿using System;
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
    public class PawnService
    {
        private VectorHelper _vectorHelper;
        private MathHelper _mathHelper;
        private ColorHelper _colorHelper;


        public PawnService(VectorHelper vectorHelper, MathHelper mathHelper, ColorHelper colorHelper)
        {
            _vectorHelper = vectorHelper;
            _mathHelper = mathHelper;
            _colorHelper = colorHelper;
        }


        public List<Vector> GetAllowedVectors(int coord, string figures)
        {
            int boardWidth= _mathHelper.Sqrt(figures.Length);

            var color = _colorHelper.GetFigureColor(figures[coord]);
            var appositeColor = color == FigureColor.White ? FigureColor.Black : FigureColor.White;
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
                else if (_colorHelper.GetFigureColor(figures[coordinateOneStepForward]) == appositeColor)
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
     
    }
}
