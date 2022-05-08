using System;
using System.Collections.Generic;
using System.Text;
using Checkers.BL.Constants;
using Checkers.BL.Extensions;
using Checkers.BL.Models;

namespace Checkers.BL.Services
{
    public class DirectMoveService : IDirectMoveService
    {
        private readonly ValidateFiguresService _validateFiguresService;

        public DirectMoveService(ValidateFiguresService validateFiguresService)
        {
            _validateFiguresService = validateFiguresService;
        }

        public string DirectMove(string boardStateString, int fromCoord, int toCoord)
        {
            var boardState = boardStateString.ToBoardState();

            string figures = boardState.Figures;

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
            for (int i = 1; i < vector.Length; i++)
            {
                var cleanCoord = (new Vector()
                {
                    Length = i,
                    Direction = vector.Direction
                }).ToCoord(fromCoord, boardWidth);

                if (newFiguresBuilder[cleanCoord] != Figures.Empty)
                {
                    isDie = true;
                    newFiguresBuilder[cleanCoord] = Figures.Empty;
                }
            }

            var newFigures = newFiguresBuilder.ToString();
            var toggleTurn = true;


            if (isDie)
            {
                var possibleNextStepVectors = _validateFiguresService.GetAllowedMoveVectors(toCoord, newFigures);
                {
                    if (possibleNextStepVectors.EatFigure)
                    {
                        toggleTurn = false;
                    }
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

            return resultState;
        }
    }
}
