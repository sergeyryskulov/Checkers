using Checkers.Contracts;
using Checkers.Contracts.UseCases;
using Checkers.DomainModels;
using Checkers.HumanPlayer.Interfaces;

namespace Checkers.HumanPlayer.Services
{
    public class HumanTryMoveFigureUseCase : IHumanTryMoveFigureUseCase
    {        
        private IValidateHumanService _validateHumanService;
        private IMoveRulesService _moveRulesService;

        public HumanTryMoveFigureUseCase(IValidateHumanService validateHumanService, IMoveRulesService moveRulesService)
        {            
            _validateHumanService = validateHumanService;
            _moveRulesService = moveRulesService;
        }

        public GameState Execute(GameState gameState, int fromPosition, int toPosition)
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
