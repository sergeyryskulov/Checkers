using Checkers.Contracts.Rules;
using Checkers.DomainModels;
using Checkers.DomainModels.Enums;
using Checkers.Rules.Extensions;
using Checkers.Rules.Interfaces;
using Checkers.Rules.Models;

namespace Checkers.Rules.Rules
{
    public class MoveRule : IMoveRule
    {
        private readonly IValidateEatService _validateEatService;

        public MoveRule(IValidateEatService validateEatService)
        {
            _validateEatService = validateEatService;
        }

        public GameState MoveFigureWithoutValidation(GameState gameState, int fromPosition, int toPosition)
        {

            var cells = gameState.Board;

            var boardWidth = cells.BoardWidth;

            var vector = fromPosition.ToVector(toPosition, boardWidth);

            var newBoardState = new Board(cells.ToString());

            bool onTopLine = toPosition < boardWidth;
            bool onBottomLine = toPosition >= boardWidth * (boardWidth - 1);
            bool convertToQueen = gameState.Turn == Turn.White && onTopLine ||
                 gameState.Turn == Turn.Black && onBottomLine;
            newBoardState.MoveFigure(fromPosition, toPosition, convertToQueen);

            bool eatFigure = false;
            for (int iteratedLength = 1; iteratedLength < vector.Length; iteratedLength++)
            {
                var iteratedCoord = new Vector(
                    vector.Direction,
                    iteratedLength
                ).ToPosition(fromPosition, boardWidth);

                if (!newBoardState.IsEmptyCellAt(iteratedCoord))
                {
                    eatFigure = true;
                    newBoardState.DeleteFigure(iteratedCoord);
                }
            }

            var toggleTurn = true;

            if (eatFigure)
            {
                var canEatNextFigure = _validateEatService.CanEatFigure(toPosition, newBoardState);

                if (canEatNextFigure)
                {
                    toggleTurn = false;
                }
            }

            var nextTurn = gameState.Turn;
            if (toggleTurn)
            {
                nextTurn = gameState.Turn == Turn.White ? Turn.Black : Turn.White;
            }

            if (gameState.Turn == Turn.White && !newBoardState.ContainsAnyBlackFigure())
            {
                nextTurn = Turn.WhiteWin;
            }

            if (gameState.Turn == Turn.Black && !newBoardState.ContainsAnyWhiteFigure())
            {
                nextTurn = Turn.BlackWin;
            }

            return new GameState(newBoardState.ToString(), nextTurn, toggleTurn ? null : (int?)toPosition);

        }
    }
}
