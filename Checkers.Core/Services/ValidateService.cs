using Checkers.Core.Constants;
using Checkers.Core.Interfaces;
using Checkers.Core.Models.Aggregates;
using Checkers.DomainModels.Aggregates;

namespace Checkers.Core.Services
{
    public class ValidateService : IValidateFigureService, IValidateEatService
    {
        private IValidatePawnService _validatePawnService;
        private IValidateQueenService _validateQueenService;

        public ValidateService(IValidatePawnService validatePawnService, IValidateQueenService validateQueenService)
        {
            _validatePawnService = validatePawnService;
            _validateQueenService = validateQueenService;
        }

        public bool CanEatFigure(int coord, Cells cells)
        {
            return GetAllowedMoveVectors(coord, cells).EatFigure;
        }
        
        public AllowedVectors GetAllowedMoveVectors(int coord, Cells cells)
        {
            var figure = cells[coord];

            var result = new AllowedVectors();

            if (figure == Figures.WhitePawn || figure == Figures.BlackPawn)
            {
                result = _validatePawnService.GetAllowedMoveVectors(coord, cells);
            }
            else if (figure == Figures.WhiteQueen || figure == Figures.BlackQueen)
            {
                result = _validateQueenService.GetAllowedMoveVectors(coord, cells);
            }

            return result;
        }
    }
}
