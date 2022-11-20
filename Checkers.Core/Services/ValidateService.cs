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

        public bool CanEatFigure(int fromPosition, Board board)
        {
            return GetAllowedMoveVectors(fromPosition, board).EatFigure;
        }
        
        public AllowedVectors GetAllowedMoveVectors(int coord, Board board)
        {
            var result = new AllowedVectors();

            if (board.PawnAt(coord))
            {
                result = _validatePawnService.GetAllowedMoveVectors(coord, board);
            }
            else if (board.QueenAt(coord))
            {
                result = _validateQueenService.GetAllowedMoveVectors(coord, board);
            }

            return result;
        }
    }
}
