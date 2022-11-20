using Checkers.Core.Constants;

using Checkers.Core.Interfaces;
using Checkers.Core.Models.ValueObjects;
using Checkers.DomainModels.Aggregates;

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
            var cells = gameState.Cells;

            var needStartFromOtherPosition = (gameState.MustGoFrom != null && gameState.MustGoFrom != fromPosition);
            if (needStartFromOtherPosition)
            {
                return false;
            }

            var incorrectTurn = (cells.WhiteFigureAt(fromPosition) && gameState.Turn != Turn.White || cells.BlackFigureAt(fromPosition) && gameState.Turn != Turn.Black);
            if (incorrectTurn)
            {
                return false;
            }

            var notInAllowedVectors = !_validateRulesService.GetAllowedDestinations(cells, fromPosition).Contains(toPosition);
            if (notInAllowedVectors)
            {
                return false;
            }


            return true;

        }
    }
}