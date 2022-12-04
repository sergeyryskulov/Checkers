using System.Collections.Generic;
using System.Linq;
using Checkers.Core.Constants;
using Checkers.Core.Constants.Enums;
using Checkers.Core.Extensions;
using Checkers.Core.Interfaces;
using Checkers.Core.Models.Aggregates;
using Checkers.Core.Models.ValueObjects;
using Checkers.DomainModels;
using Checkers.DomainModels.Enums;

namespace Checkers.Core.Services
{

    public class ValidatePawnService : IValidatePawnService
    {
        public AllowedVectors GetAllowedMoveVectors(Board board, int fromPosition)
        {
            var color = board.FigureColorAt(fromPosition);
            var allowedVectors = new List<Vector>();
            foreach (var forwardDirection in GetForwardDirections(color))
            {
                allowedVectors.AddRange(GetAllowedVectorsForForwardDirection(board, fromPosition, forwardDirection));
            }

            foreach (var backwardDirection in GetBackwardDirections(color))
            {
                allowedVectors.AddRange(GetAllowedVectorsForBackwardDirection(board, fromPosition, backwardDirection));

            }

            var eatingVectors = allowedVectors.Where(m => m.Length == 2).ToList();

            if (eatingVectors.Count > 0)
            {
                return new AllowedVectors(eatingVectors, true);
            }

            return new AllowedVectors(allowedVectors, false);
        }

        private List<Vector> GetAllowedVectorsForForwardDirection(Board board, int fromPosition, Direction forwardDirection)
        {
            int boardWidth = board.BoardWidth;
            var color = board.FigureColorAt(fromPosition);
            var oppositeColor = color == FigureColor.White ? FigureColor.Black : FigureColor.White;

            var vectorOneStepForward = new Vector(forwardDirection, 1);
                
            var coordinateOneStepForward = vectorOneStepForward.ToCoord(fromPosition, boardWidth);
            if (coordinateOneStepForward == -1)
            {
                return new List<Vector>();
            }

            var result = new List<Vector>();
            if (board.IsEmptyCellAt(coordinateOneStepForward))
            {
                result.Add(vectorOneStepForward);
            }
            else if (board.FigureColorAt(coordinateOneStepForward) == oppositeColor)
            {
                var coordTwoStepForward = vectorOneStepForward.ToCoord(coordinateOneStepForward, boardWidth);
                if (coordTwoStepForward == -1)
                {
                    return result;
                }

                if (board.IsEmptyCellAt(coordTwoStepForward))
                {
                    result.Add(new Vector(vectorOneStepForward.Direction, 2));
                }
            }

            return result;
        }

        private List<Vector> GetAllowedVectorsForBackwardDirection(Board board, int fromPosition, Direction backwardDirection)
        {
            int boardWidth = board.BoardWidth;
            var color = board.FigureColorAt(fromPosition);
            var oppositeColor = color == FigureColor.White ? FigureColor.Black : FigureColor.White;

            var vectorOneStepBackward = new Vector(backwardDirection, 1);
                
            
            var result = new List<Vector>();

            var coordinateOneStepBackward = vectorOneStepBackward.ToCoord(fromPosition,  boardWidth);
            if (coordinateOneStepBackward == -1)
            {
                return result;
            }

            if (board.FigureColorAt(coordinateOneStepBackward) == oppositeColor)
            {
                var coordTwoStepBackward = vectorOneStepBackward.ToCoord(coordinateOneStepBackward, boardWidth);
                if (coordTwoStepBackward == -1)
                {
                    return result;
                }

                if (board.IsEmptyCellAt(coordTwoStepBackward))
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
