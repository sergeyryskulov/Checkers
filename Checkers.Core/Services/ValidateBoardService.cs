using Checkers.Core.Constants;
using Checkers.Core.Extensions;
using Checkers.Core.Interfaces;
using Checkers.Core.Models.ValueObjects;
using Checkers.DomainModels.Aggregates;

namespace Checkers.Core.Services
{
    public class ValidateBoardService : IValidateBoardService
    {

        private IValidateRulesService _validateRulesService;

        public ValidateBoardService(IValidateRulesService validateRulesService)
        {
            _validateRulesService = validateRulesService;
        }


        public bool CanMove(GameState gameState, int fromCoord, int toCoord)
        {            
            var cells = gameState.Cells;

            var boardWidth = cells.BoardWidth();

            var vector = fromCoord.ToVector(toCoord, boardWidth);

            var needStartFromOtherCoord = (gameState.MustGoFrom != null && gameState.MustGoFrom != fromCoord);
            if (needStartFromOtherCoord)
            {
                return false;
            }

            var incorrectVector = (vector == null);
            if (incorrectVector)
            {
                return false;
            }

            var incorrectTurn = (cells.WhiteFigureAt(fromCoord) && gameState.Turn != Turn.White || cells.BlackFigureAt(fromCoord) && gameState.Turn != Turn.Black);
            if (incorrectTurn)
            {
                return false;
            }

            var notInAllowedVectors = !_validateRulesService.GetAllowedMoveVariants(cells, fromCoord).Contains(toCoord);
            if (notInAllowedVectors)
            {
                return false;
            }


            return true;

        }
    }
}