using Checkers.Core.Interfaces;
using Checkers.Core.Models.ValueObjects;

namespace Checkers.Core.Services
{
    public class HumanPlayerService : IHumanPlayerService
    {        
        private IValidateBoardService _validateBoardService;
        private IDirectMoveService _directMoveService;

        public HumanPlayerService(IValidateBoardService validateBoardService, IDirectMoveService directMoveService)
        {            
            _validateBoardService = validateBoardService;
            _directMoveService = directMoveService;
        }

        public GameState TryMoveFigure(GameState gameState, int fromCoord, int toCoord)
        {            

            if (!_validateBoardService.CanMove(gameState, fromCoord, toCoord))
            {
                return gameState;
            }

            var newState = _directMoveService.MoveFigureWithoutValidation(gameState, fromCoord, toCoord);                        
            
            return newState;
        }
    }
}
