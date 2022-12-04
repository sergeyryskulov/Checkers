using Checkers.Core.Interfaces;
using Checkers.DomainModels;
using Checkers.DomainModels.Enums;
using Checkers.DomainServices;

namespace Checkers.Core.Services
{
    public class ValidateHumanService : IValidateHumanService
    {

        private IValidateRulesService _validateRulesService;

        public ValidateHumanService(IValidateRulesService validateRulesService)
        {
            _validateRulesService = validateRulesService;
        }


        public bool CanMove(GameState gameState, int fromPosition, int toPosition)
        {            
            var cells = gameState.Board;

            var needStartFromOtherPosition = (gameState.MustGoFromPosition != null && gameState.MustGoFromPosition != fromPosition);
            if (needStartFromOtherPosition)
            {
                return false;
            }

            var incorrectTurn = (cells.IsWhiteFigureAt(fromPosition) && gameState.Turn != Turn.White || cells.IsBlackFigureAt(fromPosition) && gameState.Turn != Turn.Black);
            if (incorrectTurn)
            {
                return false;
            }

            var notInAllowedVectors = !_validateRulesService.GetAllowedToPositions(cells, fromPosition).Contains(toPosition);
            if (notInAllowedVectors)
            {
                return false;
            }


            return true;

        }
    }
}