using Checkers.Core.Interfaces;
using Checkers.Core.Models.ValueObjects;

namespace Checkers.Core.Services
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

        public GameState TryMoveFigure(GameState gameState, int fromCoord, int toCoord)
        {            

            if (!_validateHumanService.CanMove(gameState, fromCoord, toCoord))
            {
                return gameState;
            }

            var newState = _moveRulesService.MoveFigureWithoutValidation(gameState, fromCoord, toCoord);                        
            
            return newState;
        }
    }
}
