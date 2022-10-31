using Checkers.Core.Interfaces;
using Checkers.Core.Models.ValueObjects;

namespace Checkers.Core.Services
{
    public class MoveFigureService : IMoveFigureService
    {        
        private IValidateBoardService _validateBoardService;
        private IDirectMoveService _directMoveService;

        public MoveFigureService(IValidateBoardService validateBoardService, IDirectMoveService directMoveService)
        {            
            _validateBoardService = validateBoardService;
            _directMoveService = directMoveService;
        }

        public BoardState Move(int fromCoord, int toCoord, BoardState boardState)
        {            

            if (!_validateBoardService.CanMove(boardState, fromCoord, toCoord))
            {
                return boardState;
            }

            var newState = _directMoveService.DirectMove(boardState, fromCoord, toCoord);                        
            
            return newState;
        }
    }
}
