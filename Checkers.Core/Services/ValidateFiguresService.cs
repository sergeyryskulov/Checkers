using Checkers.Core.Extensions;
using Checkers.Core.Interfaces;
using Checkers.Core.Models.Aggregates;
using Checkers.DomainModels.Aggregates;
using System.Collections.Generic;
using System.Linq;

namespace Checkers.Core.Services
{
    public class ValidateFiguresService : IValidateFiguresService
    {
        private readonly IValidateFigureService _validateFigureService;

        public ValidateFiguresService(IValidateFigureService validateFigureService)
        {
            _validateFigureService = validateFigureService;
        }

        public List<int> GetAllowedMoveVariants(Board board, int coord)
        {
            
            var allowedMoveVectors = _validateFigureService.GetAllowedMoveVectors(coord, board);

            if (allowedMoveVectors.EatFigure==false && allowedMoveVectors.AnyVectorExists() && IsBlockedByAnotherFigure(coord, board))
            {
                return new List<int>();
            }

            return allowedMoveVectors.Vectors.ToList().ConvertAll(m => m.ToCoord(coord, board.BoardWidth()));
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
