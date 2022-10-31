using System.Text;
using Checkers.Core.Constants;
using Checkers.Core.Extensions;
using Checkers.Core.Interfaces;
using Checkers.Core.Models.ValueObjects;

namespace Checkers.Core.Services
{
    public class DirectMoveService : IDirectMoveService
    {
        private readonly IValidateEatService _validateEatService;

        public DirectMoveService(IValidateEatService validateEatService)
        {
            _validateEatService = validateEatService;
        }

        public BoardState DirectMove(BoardState boardState, int fromCoord, int toCoord)
        {            
            string figures = boardState.Cells;

            var boardWidth = figures.Length.SquareRoot();

            var vector = fromCoord.ToVector(toCoord, boardWidth);

            StringBuilder newFiguresBuilder = new StringBuilder(figures);
            newFiguresBuilder[toCoord] = newFiguresBuilder[fromCoord];
            if (toCoord < boardWidth && boardState.Turn == Turn.White)
            {
                newFiguresBuilder[toCoord] = Figures.WhiteQueen;
            }

            if (toCoord >= boardWidth * (boardWidth - 1) && boardState.Turn == Turn.Black)
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


            if (isDie)
            {
                var canEatNextFigure = _validateEatService.CanEatFigure(toCoord, newFigures);

                if (canEatNextFigure)
                {
                    toggleTurn = false;
                }
            }

            var nextTurn = boardState.Turn;
            if (toggleTurn)
            {
                nextTurn = (boardState.Turn == Turn.White ? Turn.Black : Turn.White);
            }

            if (boardState.Turn == Turn.White && !newFigures.Contains(Figures.BlackPawn) &&
                !newFigures.Contains(Figures.BlackQueen))
            {
                nextTurn = Turn.WhiteWin;
            }

            if (boardState.Turn == Turn.Black && !newFigures.Contains(Figures.WhitePawn) &&
                !newFigures.Contains(Figures.WhiteQueen))
            {
                nextTurn = Turn.BlackWin;
            }


            var resultState =
                toggleTurn ? newFigures + nextTurn : newFigures + nextTurn + toCoord;

            return new BoardState(newFigures, nextTurn, toggleTurn ? null : (int?) toCoord);

        }
    }
}
