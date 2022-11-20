using System.Reflection;
using System.Text;
using Checkers.Core.Constants;
using Checkers.Core.Extensions;
using Checkers.Core.Interfaces;
using Checkers.Core.Models.ValueObjects;
using Checkers.DomainModels.Aggregates;

namespace Checkers.Core.Services
{
    public class MoveRulesService : IMoveRulesService
    {
        private readonly IValidateEatService _validateEatService;

        public MoveRulesService(IValidateEatService validateEatService)
        {
            _validateEatService = validateEatService;
        }

        public GameState MoveFigureWithoutValidation(GameState gameState, int fromPosition, int toPosition)
        {

            var cells = gameState.Cells;

            var boardWidth = cells.BoardWidth();

            var vector = fromPosition.ToVector(toPosition, boardWidth);

            var newBoardState = new Board(cells.ToString());

            bool onTopLine = (toPosition < boardWidth);
            bool onBottomLine = toPosition >= boardWidth * (boardWidth - 1);
            bool convertToQueen = (gameState.Turn == Turn.White && onTopLine) ||
                 (gameState.Turn == Turn.Black && onBottomLine);
            newBoardState.GetFigureFromAndPutItTo(fromPosition, toPosition, convertToQueen);

            bool eatFigure = false;
            for (int iteratedLength = 1; iteratedLength < vector.Length; iteratedLength++)
            {
                var iteratedCoord = (new Vector(
                    vector.Direction,
                    iteratedLength
                )).ToCoord(fromPosition, boardWidth);

                if (!newBoardState.EmptyCellAt(iteratedCoord))
                {
                    eatFigure = true;
                    newBoardState.RemoveFigure(iteratedCoord);                    
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
                nextTurn = (gameState.Turn == Turn.White ? Turn.Black : Turn.White);
            }

            if (gameState.Turn == Turn.White && !newBoardState.ContainsAnyBlackFigure())
            {
                nextTurn = Turn.WhiteWin;
            }

            if (gameState.Turn == Turn.Black && !newBoardState.ContainsAnyWhiteFigure())
            {
                nextTurn = Turn.BlackWin;
            }
        
            return new GameState(newBoardState.ToString(), nextTurn, toggleTurn ? null : (int?) toPosition);

        }
    }
}
