using System.Reflection;
using System.Text;
using Checkers.Core.Constants;
using Checkers.Core.Extensions;
using Checkers.Core.Interfaces;
using Checkers.Core.Models.ValueObjects;
using Checkers.DomainModels.Aggregates;

namespace Checkers.Core.Services
{
    public class DirectMoveService : IDirectMoveService
    {
        private readonly IValidateEatService _validateEatService;

        public DirectMoveService(IValidateEatService validateEatService)
        {
            _validateEatService = validateEatService;
        }

        public GameState MoveFigureWithoutValidation(GameState gameState, int fromCoord, int toCoord)
        {

            var cells = gameState.Cells;

            var boardWidth = cells.BoardWidth();

            var vector = fromCoord.ToVector(toCoord, boardWidth);

            StringBuilder newFiguresBuilder = new StringBuilder(cells.ToString());
            newFiguresBuilder[toCoord] = newFiguresBuilder[fromCoord];
            if (toCoord < boardWidth && gameState.Turn == Turn.White)
            {
                newFiguresBuilder[toCoord] = Figures.WhiteQueen;
            }

            if (toCoord >= boardWidth * (boardWidth - 1) && gameState.Turn == Turn.Black)
            {
                newFiguresBuilder[toCoord] = Figures.BlackQueen;
            }

            newFiguresBuilder[fromCoord] = Figures.Empty;
            bool isDie = false;
            for (int iteratedLength = 1; iteratedLength < vector.Length; iteratedLength++)
            {
                var iteratedCoord = (new Vector(
                    vector.Direction,
                    iteratedLength
                )).ToCoord(fromCoord, boardWidth);

                if (newFiguresBuilder[iteratedCoord] != Figures.Empty)
                {
                    isDie = true;
                    newFiguresBuilder[iteratedCoord] = Figures.Empty;
                }
            }

            var newFigures = newFiguresBuilder.ToString();
            var toggleTurn = true;
            var newCells=new Cells(newFigures);
            

            if (isDie)
            {
                var canEatNextFigure = _validateEatService.CanEatFigure(toCoord, newCells);

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

            if (gameState.Turn == Turn.White && !newFigures.Contains(Figures.BlackPawn) &&
                !newFigures.Contains(Figures.BlackQueen))
            {
                nextTurn = Turn.WhiteWin;
            }

            if (gameState.Turn == Turn.Black && !newFigures.Contains(Figures.WhitePawn) &&
                !newFigures.Contains(Figures.WhiteQueen))
            {
                nextTurn = Turn.BlackWin;
            }


            var resultState =
                toggleTurn ? newFigures + nextTurn : newFigures + nextTurn + toCoord;

            return new GameState(newFigures, nextTurn, toggleTurn ? null : (int?) toCoord);

        }
    }
}
