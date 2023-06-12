using Checkers.DomainModels;
using Checkers.DomainModels.Models;
using Checkers.Rules.Interfaces;
using Checkers.Rules.Models;

namespace Checkers.Rules.Services;

internal class ValidateFigureService : IValidateFigureService, IValidateEatService
{
    private IValidatePawnService _validatePawnService;
    private IValidateQueenService _validateQueenService;

    public ValidateFigureService(IValidatePawnService validatePawnService, IValidateQueenService validateQueenService)
    {
        _validatePawnService = validatePawnService;
        _validateQueenService = validateQueenService;
    }

    public bool CanEatFigure(int fromPosition, Board board)
    {
        return GetAllowedMoveVectors(board, fromPosition).EatFigure;
    }
        
    public AllowedVectors GetAllowedMoveVectors(Board board, int fromPosition)
    {
        var result = new AllowedVectors();

        if (board.IsPawnAt(fromPosition))
        {
            result = _validatePawnService.GetAllowedMoveVectors(board, fromPosition);
        }
        else if (board.IsQueenAt(fromPosition))
        {
            result = _validateQueenService.GetAllowedMoveVectors(board, fromPosition);
        }

        return result;
    }
}