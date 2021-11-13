using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.BL.Constants.Enums;
using Checkers.BL.Models;

namespace Checkers.BL.Helper
{
    public class VectorHelper
    {

        public Vector CoordToVector(int fromCoord, int toCoord, int boardWidth)
        {
            int fromCoordXProjection = (fromCoord % boardWidth);

            int toCoordXProjection = (toCoord % boardWidth);

            bool right = (fromCoordXProjection < toCoordXProjection);

            bool bottom = fromCoord < toCoord;

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

            var resultVector = new Vector()
            {
                Direction = direction,
                Length = length
            };

            if (VectorToCoord(fromCoord, resultVector, boardWidth) == toCoord)
            {
                return resultVector;
            }

            return null;
        }

      
        
        public int VectorToCoord(int coord, Vector vector, int boardWidth)
        {
            int shiftLeftRight = vector.Length * (IsRight(vector.Direction) ? 1 : -1);
            int xProjection = (coord % boardWidth) + shiftLeftRight;
            
            if (xProjection < 0 || xProjection >= boardWidth)
            {
                return -1;
            }

            int shiftTopBottom = (boardWidth * vector.Length * (IsBottom(vector.Direction) ? 1 : -1));
            int result = coord + shiftLeftRight + shiftTopBottom; ;
            if (result < 0 || result >= boardWidth * boardWidth)
            {
                return -1;
            }

            return result;
        }

        bool IsBottom(Direction direction)
        {
            return direction == Direction.LeftBottom || direction == Direction.RightBottom;
        }

        bool IsRight(Direction direction)
        {
            return direction == Direction.RightBottom || direction == Direction.RightTop;
        }
    }
}
