﻿using System.Collections.Generic;
using System.Linq;
using Checkers.Core.Constants;
using Checkers.Core.Constants.Enums;
using Checkers.Core.Extensions;
using Checkers.Core.Interfaces;
using Checkers.Core.Models.Aggregates;
using Checkers.Core.Models.ValueObjects;

namespace Checkers.Core.Services
{

    public class ValidatePawnService : IValidatePawnService
    {                        
        
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
                return new AllowedVectors(eatingVectors, true);
            }

            return new AllowedVectors(allowedVectors, false);
        }

        private List<Vector> GetAllowedVectorsForForwardDirection(int coord, string figures, Direction forwardDirection)
        {
            int boardWidth = figures.Length.SquareRoot();
            var color = figures[coord].ToFigureColor();
            var oppositeColor = color == FigureColor.White ? FigureColor.Black : FigureColor.White;

            var vectorOneStepForward = new Vector(forwardDirection, 1);
                
            var coordinateOneStepForward = vectorOneStepForward.ToCoord(coord, boardWidth);
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
                var coordTwoStepForward = vectorOneStepForward.ToCoord(coordinateOneStepForward, boardWidth);
                if (coordTwoStepForward == -1)
                {
                    return result;
                }

                if (figures[coordTwoStepForward] == Figures.Empty)
                {
                    result.Add(new Vector(vectorOneStepForward.Direction, 2));
                }
            }

            return result;
        }

        private List<Vector> GetAllowedVectorsForBackwardDirection(int coord, string figures, Direction backwardDirection)
        {
            int boardWidth = figures.Length.SquareRoot();
            var color = figures[coord].ToFigureColor();
            var oppositeColor = color == FigureColor.White ? FigureColor.Black : FigureColor.White;

            var vectorOneStepBackward = new Vector(backwardDirection, 1);
                
            
            var result = new List<Vector>();

            var coordinateOneStepBackward = vectorOneStepBackward.ToCoord(coord,  boardWidth);
            if (coordinateOneStepBackward == -1)
            {
                return result;
            }

            if (figures[coordinateOneStepBackward].ToFigureColor() == oppositeColor)
            {
                var coordTwoStepBackward = vectorOneStepBackward.ToCoord(coordinateOneStepBackward, boardWidth);
                if (coordTwoStepBackward == -1)
                {
                    return result;
                }

                if (figures[coordTwoStepBackward] == Figures.Empty)
                {
                    result.Add(new Vector(backwardDirection, 2));
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
