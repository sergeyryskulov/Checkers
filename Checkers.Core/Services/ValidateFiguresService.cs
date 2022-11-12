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

        public AllowedVectors GetAllowedMoveVariants(Cells cells, int coord)
        {
            
            var result = _validateFigureService.GetAllowedMoveVectors(coord, cells);

            if (result.EatFigure==false && result.AnyVectorExists() && IsBlockedByAnotherFigure(coord, cells))
            {
                return new AllowedVectors();
            }

            return result;
        }

        private bool IsBlockedByAnotherFigure(int coord, Cells cells)
        {
            var color = cells[coord].ToFigureColor();

            for (int figureCoord = 0; figureCoord < cells.Length; figureCoord++)
            {
                var iteratedFigure = cells[figureCoord];

                if (
                    coord != iteratedFigure &&
                    iteratedFigure.ToFigureColor() == color)
                {
                    if (_validateFigureService.GetAllowedMoveVectors(figureCoord, cells).EatFigure)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }
}
