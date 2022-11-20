using Checkers.Core.Interfaces;
using Checkers.Core.Models.ValueObjects;

namespace Checkers.Core.Services
{
    public class HumanPlayerService : IHumanPlayerService
    {        
        private IValidateBoardService _validateBoardService;
        private IMoveRulesService _moveRulesService;

        public HumanPlayerService(IValidateBoardService validateBoardService, IMoveRulesService moveRulesService)
        {            
            _validateBoardService = validateBoardService;
            _moveRulesService = moveRulesService;
        }

        public GameState TryMoveFigure(GameState gameState, int fromCoord, int toCoord)
        {            

            if (!_validateBoardService.CanMove(gameState, fromCoord, toCoord))
            {
                return gameState;
            }

            var newState = _moveRulesService.MoveFigureWithoutValidation(gameState, fromCoord, toCoord);                        
            
            return newState;
        }
    }
}
