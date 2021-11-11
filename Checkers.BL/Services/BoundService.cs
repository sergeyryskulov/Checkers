using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.BL.Constants.Enums;
using Checkers.BL.Models;

namespace Checkers.BL.Services
{
    public class BoundService
    {
        public List<Vector> GetAllowedVectors(int coord, int boardWidth)
        {
            
            var shiftX = coord % boardWidth;
            var shiftY = (coord - shiftX) / boardWidth;

            int maxMoveLeft = shiftX;
            int maxMoveRight = (boardWidth - 1) - maxMoveLeft;

            int maxMoveTop = shiftY;
            int maxMoveBottom = (boardWidth - 1) - maxMoveTop;

            var result = new List<Vector>();
            result.AddRange(VectorRange(Direction.RightBottom, maxMoveRight, maxMoveBottom));
            result.AddRange(VectorRange(Direction.LeftBottom, maxMoveLeft, maxMoveBottom));

            result.AddRange(VectorRange(Direction.RightTop, maxMoveRight, maxMoveTop));
            result.AddRange(VectorRange(Direction.LeftTop, maxMoveLeft, maxMoveTop));
            return result;
        }

        private List<Vector> VectorRange(Direction direction, int maxMove1, int maxMove2)
        {
            var result = new List<Vector>();

            for (int i = 1; i <= Math.Min(maxMove1, maxMove2); i++)
            {
                result.Add(new Vector()
                {
                    Length = i,
                    Base = direction
                });
            }

            return result;
        }
    }
}
