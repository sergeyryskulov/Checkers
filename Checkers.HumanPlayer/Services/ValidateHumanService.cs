using Checkers.Contracts.Rules;
using Checkers.DomainModels;
using Checkers.DomainModels.Enums;
using Checkers.DomainModels.Models;
using Checkers.HumanPlayer.Interfaces;

namespace Checkers.HumanPlayer.Services
{
    internal class ValidateHumanService : IValidateHumanService
    {

        private IValidateRule _validateRule;

        public ValidateHumanService(IValidateRule validateRule)
        {
            _validateRule = validateRule;
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

            var notInAllowedVectors = !_validateRule.GetAllowedToPositions(cells, fromPosition).Contains(toPosition);
            if (notInAllowedVectors)
            {
                return false;
            }


            return true;

        }
    }
}