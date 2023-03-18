using System.Collections.Generic;
using System.Linq;
using Checkers.Contracts.Rules;
using Checkers.DomainModels;
using Checkers.Rules.Extensions;
using Checkers.Rules.Interfaces;

namespace Checkers.Rules.Services
{
    public class ValidateRule : IValidateRule
    {
        private readonly IValidateFigureService _validateFigureService;

        public ValidateRule(IValidateFigureService validateFigureService)
        {
            _validateFigureService = validateFigureService;
        }

        public List<int> GetAllowedToPositions(Board board, int fromPosition)
        {
            
            var allowedMoveVectors = _validateFigureService.GetAllowedMoveVectors(board, fromPosition);

            if (allowedMoveVectors.EatFigure==false && allowedMoveVectors.AnyVectorExists() && IsBlockedByAnotherFigure(board, fromPosition))
            {
                return new List<int>();
            }

            return allowedMoveVectors.Vectors.ToList().ConvertAll(m => m.ToPosition(fromPosition, board.BoardWidth));
        }

        private bool IsBlockedByAnotherFigure(Board board, int position)
        {
            var color = board.FigureColorAt(position);

            for (int figureCoord = 0; figureCoord < board.CellsCount; figureCoord++)
            {
                if (
                    position != figureCoord &&
                    board.FigureColorAt(figureCoord) == color)
                {
                    if (_validateFigureService.GetAllowedMoveVectors(board, figureCoord).EatFigure)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }
}
