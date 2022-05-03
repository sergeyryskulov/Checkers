using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Checkers.BL.Constants;
using Checkers.BL.Extensions;
using Checkers.BL.Models;
using Ckeckers.DAL.Repositories;

namespace Checkers.BL.Services
{
    public class MoveFigureService : IMoveFigureService
    {        
        private ValidateFiguresService _validateFiguresService;                

        public MoveFigureService(ValidateFiguresService validateFiguresService)
        {            
            _validateFiguresService = validateFiguresService;            
        }

        public string Move(string boardStateString, int fromCoord, int toCoord, bool skipValidation = false)
        {
            var boardState = boardStateString.ToBoardState();

            var needStartFromOtherCoord = (boardState.MustCoord != -1 && boardState.MustCoord != fromCoord);
            if (needStartFromOtherCoord)
            {
                return boardStateString;
            }

            string figures = boardState.Figures;

            var boardWidth = figures.Length.SquareRoot();

            var vector = fromCoord.ToVector(toCoord, boardWidth);

            var incorrectVector = (vector == null);
            if (incorrectVector)
            {
                return boardStateString;
            }

            var incorrectTurn = (figures[fromCoord].IsWhite() && boardState.Turn != Turn.White || figures[fromCoord].IsBlack() && boardState.Turn != Turn.Black);
            if (incorrectTurn)
            {
                return boardStateString;
            }

            
            if (
                !skipValidation &&
                figures[fromCoord] == Figures.WhitePawn || figures[fromCoord] == Figures.BlackPawn ||
                figures[fromCoord] == Figures.WhiteQueen || figures[fromCoord] == Figures.BlackQueen)
            {
                if (!_validateFiguresService.GetAllowedMoveVectors(fromCoord, figures).Vectors.Contains(vector))
                {
                    return boardStateString;
                }
            }

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
                var cleanCoord =(new Vector()
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


//            var resultState = newFigures + nextTurn + (toggleTurn ? "" : toCoord);

            var resultState = 
                toggleTurn ? newFigures + nextTurn : newFigures + nextTurn + toCoord;

            return resultState;
        }


    }
}
