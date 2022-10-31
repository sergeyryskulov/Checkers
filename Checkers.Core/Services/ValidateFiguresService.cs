using Checkers.Core.Extensions;
using Checkers.Core.Interfaces;
using Checkers.Core.Models.Aggregates;

namespace Checkers.Core.Services
{
    public class ValidateFiguresService : IValidateFiguresService
    {
        private readonly IValidateFigureService _validateFigureService;

        public ValidateFiguresService(IValidateFigureService validateFigureService)
        {
            _validateFigureService = validateFigureService;
        }

        public AllowedVectors GetAllowedMoveVectors(int coord, string figures)
        {
            
            var result = _validateFigureService.GetAllowedMoveVectors(coord, figures);

            if (result.EatFigure==false && result.AnyVectorExists() && IsBlockedByAnotherFigure(coord, figures))
            {
                return new AllowedVectors();
            }

            return result;
        }

        private bool IsBlockedByAnotherFigure(int coord, string figures)
        {
            var color = figures[coord].ToFigureColor();

            for (int figureCoord = 0; figureCoord < figures.Length; figureCoord++)
            {
                var iteratedFigure = figures[figureCoord];

                if (
                    coord != iteratedFigure &&
                    iteratedFigure.ToFigureColor() == color)
                {
                    if (_validateFigureService.GetAllowedMoveVectors(figureCoord, figures).EatFigure)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }
}
