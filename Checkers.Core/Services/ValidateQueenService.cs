﻿using System.Collections.Generic;
using Checkers.Core.Constants.Enums;
using Checkers.Core.Extensions;
using Checkers.Core.Interfaces;
using Checkers.Core.Models.Aggregates;
using Checkers.Core.Models.ValueObjects;
using Checkers.DomainModels.Aggregates;

namespace Checkers.Core.Services
{
    public class ValidateQueenService : IValidateQueenService
    {                        
      
        public AllowedVectors GetAllowedMoveVectors(int coord, Cells cells)
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
                var directionAllowedVectores = GetAllowedVectorsQueenDirection(coord, cells, direction);
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
                return new AllowedVectors(eatingVectors, true);
            }
            return new AllowedVectors(notEatingVectors, false);
        }

        private AllowedVectors GetAllowedVectorsQueenDirection(int coord, Cells figures, Direction direction)
        {

            int boardWidth = figures.BoardWidth();
            var color = figures.FigureColorAt(coord);
            var oppositeColor = color == FigureColor.White ? FigureColor.Black : FigureColor.White;

            var eatVectors = new List<Vector>();
            var notEatVectors = new List<Vector>();
            var eatFigure = false;

            for (int iteratedVectorLength = 1; iteratedVectorLength < boardWidth; iteratedVectorLength++)
            {
                var iteratedVector = new Vector(
                    direction,
                    iteratedVectorLength
                );

                var iteratedCoord = iteratedVector.ToCoord(coord, boardWidth);
                if (iteratedCoord == -1)
                {
                    break;
                }

                var figureColor = figures.FigureColorAt(iteratedCoord);

                if (figureColor == FigureColor.Empty)
                {
                    if (eatFigure)
                    {
                        eatVectors.Add(iteratedVector);
                    }
                    else
                    {
                        notEatVectors.Add(iteratedVector);
                    }
                }
                else if (figureColor == oppositeColor)
                {
                    if (eatFigure)
                    {
                        break;
                    }

                    eatFigure = true;
                }
                else
                {
                    break;
                }

            }

            if (eatVectors.Count > 0)
            {
                return new AllowedVectors(eatVectors, true);
            }

            return new AllowedVectors(notEatVectors, false);
        }
     

    }
}
