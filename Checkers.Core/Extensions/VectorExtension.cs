using System;
using Checkers.Core.Constants.Enums;
using Checkers.Core.Models.ValueObjects;

namespace Checkers.Core.Extensions
{
    public static class VectorExtension
    {
        public static Vector ToVector(this int fromPosition, int toPosition, int boardWidth)
        {
            if (fromPosition == toPosition)
            {
                return null;
            }

            int fromCoordXProjection = (fromPosition % boardWidth);

            int toCoordXProjection = (toPosition % boardWidth);

            bool right = (fromCoordXProjection < toCoordXProjection);

            bool bottom = fromPosition < toPosition;

            int length = Math.Abs(toCoordXProjection - fromCoordXProjection);

            Direction direction;
            if (right)
            {
                direction = bottom ? Direction.RightBottom : Direction.RightTop;
            }
            else
            {
                direction = bottom ? Direction.LeftBottom : Direction.LeftTop;
            }

            var resultVector = new Vector(direction, length);

            if (resultVector.ToCoord(fromPosition, boardWidth) == toPosition)
            {
                return resultVector;
            }

            return null;
        }

        public static int ToCoord(this Vector vector, int fromCoord, int boardWidth)
        {
            int shiftLeftRight = vector.Length * (IsRight(vector.Direction) ? 1 : -1);
            int xProjection = (fromCoord % boardWidth) + shiftLeftRight;

            if (xProjection < 0 || xProjection >= boardWidth)
            {
                return -1;
            }

            int shiftTopBottom = (boardWidth * vector.Length * (IsBottom(vector.Direction) ? 1 : -1));
            int result = fromCoord + shiftLeftRight + shiftTopBottom; ;
            if (result < 0 || result >= boardWidth * boardWidth)
            {
                return -1;
            }

            return result;
        }

        static bool IsBottom(Direction direction)
        {
            return direction == Direction.LeftBottom || direction == Direction.RightBottom;
        }

        static bool IsRight(Direction direction)
        {
            return direction == Direction.RightBottom || direction == Direction.RightTop;
        }
    }
}
