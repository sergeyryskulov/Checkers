using Checkers.Core.Extensions;
using Checkers.Core.Interfaces;
using Checkers.Core.Models.Aggregates;
using System.Collections.Generic;
using System.Linq;
using Checkers.DomainModels;
using Checkers.DomainServices;

namespace Checkers.Core.Services
{
    public class ValidateRulesService : IValidateRulesService
    {
        private readonly IValidateFigureService _validateFigureService;

        public ValidateRulesService(IValidateFigureService validateFigureService)
        {
            _validateFigureService = validateFigureService;
        }

        public List<int> GetAllowedToPositions(Board board, int fromPosition)
        {
            
            var allowedMoveVectors = _validateFigureService.GetAllowedMoveVectors(fromPosition, board);

            if (allowedMoveVectors.EatFigure==false && allowedMoveVectors.AnyVectorExists() && IsBlockedByAnotherFigure(fromPosition, board))
            {
                return new List<int>();
            }

            return allowedMoveVectors.Vectors.ToList().ConvertAll(m => m.ToCoord(fromPosition, board.BoardWidth));
        }

        private bool IsBlockedByAnotherFigure(int coord, Board board)
        {
            var color = board.FigureColorAt(coord);

            for (int figureCoord = 0; figureCoord < board.CellsCount; figureCoord++)
            {
                if (
                    coord != figureCoord &&
                    board.FigureColorAt(figureCoord) == color)
                {
                    if (_validateFigureService.GetAllowedMoveVectors(figureCoord, board).EatFigure)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }
}
