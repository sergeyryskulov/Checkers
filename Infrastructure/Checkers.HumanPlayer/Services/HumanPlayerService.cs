using Checkers.DomainModels;
using Checkers.DomainServices;
using Checkers.HumanPlayer.Interfaces;

namespace Checkers.HumanPlayer.Services
{
    public class HumanPlayerService : IHumanPlayerService
    {        
        private IValidateHumanService _validateHumanService;
        private IMoveRulesService _moveRulesService;

        public HumanPlayerService(IValidateHumanService validateHumanService, IMoveRulesService moveRulesService)
        {            
            _validateHumanService = validateHumanService;
            _moveRulesService = moveRulesService;
        }

        public GameState TryMoveFigure(GameState gameState, int fromPosition, int toPosition)
        {            

            if (!_validateHumanService.CanMove(gameState, fromPosition, toPosition))
            {
                return gameState;
            }

            var newState = _moveRulesService.MoveFigureWithoutValidation(gameState, fromPosition, toPosition);                        
            
            return newState;
        }
    }
}
