using System;
using System.Collections.Generic;
using System.Text;
using Checkers.BL.Constants;
using Checkers.BL.Extensions;

namespace Checkers.BL.Services
{
    public class ValidateBoardService : IValidateBoardService
    {

        private IValidateFiguresService _validateFiguresService;

        public ValidateBoardService(IValidateFiguresService validateFiguresService)
        {
            _validateFiguresService = validateFiguresService;
        }


        public bool CanMove(string boardStateString, int fromCoord, int toCoord)
        {

            var boardState = boardStateString.ToBoardState();

            string figures = boardState.Figures;

            var boardWidth = figures.Length.SquareRoot();

            var vector = fromCoord.ToVector(toCoord, boardWidth);

            var needStartFromOtherCoord = (boardState.MustCoord != -1 && boardState.MustCoord != fromCoord);
            if (needStartFromOtherCoord)
            {
                return false;
            }

            var incorrectVector = (vector == null);
            if (incorrectVector)
            {
                return false;
            }

            var incorrectTurn = (figures[fromCoord].IsWhite() && boardState.Turn != Turn.White || figures[fromCoord].IsBlack() && boardState.Turn != Turn.Black);
            if (incorrectTurn)
            {
                return false;
            }

            var notInAllowedVectors = !_validateFiguresService.GetAllowedMoveVectors(fromCoord, figures).Vectors.Contains(vector);
            if (notInAllowedVectors)
            {
                return false;
            }


            return true;

        }
    }
}