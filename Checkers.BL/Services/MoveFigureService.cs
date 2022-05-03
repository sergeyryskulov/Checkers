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

        private DirectMoveFigureService _directMoveFigureService;

        public MoveFigureService(
            ValidateFiguresService validateFiguresService,
            DirectMoveFigureService directMoveFigureService)
        {
            _validateFiguresService = validateFiguresService;
            _directMoveFigureService = directMoveFigureService;
        }


        public string Move(string boardStateString, int fromCoord, int toCoord, bool skipValidation = false)
        {
            if (!skipValidation)
            {
                if (!CanMove(boardStateString, fromCoord, toCoord))
                {
                    return boardStateString;
                }
            }

            return _directMoveFigureService.DirectMove(boardStateString, fromCoord, toCoord);
        }

       


        private bool CanMove(string boardStateString, int fromCoord, int toCoord)
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
