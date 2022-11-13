using Checkers.Core.Extensions;
using Checkers.Core.Interfaces;
using Checkers.Core.Models.Aggregates;
using Checkers.DomainModels.Aggregates;

namespace Checkers.Core.Services
{
    public class ValidateFiguresService : IValidateFiguresService
    {
        private readonly IValidateFigureService _validateFigureService;

        public ValidateFiguresService(IValidateFigureService validateFigureService)
        {
            _validateFigureService = validateFigureService;
        }

        public AllowedVectors GetAllowedMoveVariants(Board board, int coord)
        {
            
            var result = _validateFigureService.GetAllowedMoveVectors(coord, board);

            if (result.EatFigure==false && result.AnyVectorExists() && IsBlockedByAnotherFigure(coord, board))
            {
                return new AllowedVectors();
            }

            return result;
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
