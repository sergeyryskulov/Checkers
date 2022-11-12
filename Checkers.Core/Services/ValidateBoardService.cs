using Checkers.Core.Constants;
using Checkers.Core.Extensions;
using Checkers.Core.Interfaces;
using Checkers.Core.Models.ValueObjects;

namespace Checkers.Core.Services
{
    public class ValidateBoardService : IValidateBoardService
    {

        private IValidateFiguresService _validateFiguresService;

        public ValidateBoardService(IValidateFiguresService validateFiguresService)
        {
            _validateFiguresService = validateFiguresService;
        }


        public bool CanMove(GameState gameState, int fromCoord, int toCoord)
        {            
            string figures = gameState.Cells;

            var boardWidth = figures.Length.SquareRoot();

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

            var incorrectTurn = (figures[fromCoord].IsWhite() && gameState.Turn != Turn.White || figures[fromCoord].IsBlack() && gameState.Turn != Turn.Black);
            if (incorrectTurn)
            {
                return false;
            }

            var notInAllowedVectors = !_validateFiguresService.GetAllowedMoveVariants(figures, fromCoord).Contains(vector);
            if (notInAllowedVectors)
            {
                return false;
            }


            return true;

        }
    }
}