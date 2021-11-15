using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Checkers.BL.Constants;
using Checkers.BL.Helper;
using Checkers.BL.Models;
using Ckeckers.DAL.Repositories;

namespace Checkers.BL.Services
{
    public class MoveFigureService
    {
        private VectorHelper _vectorHelper;
        private MathHelper _mathHelper;
        private ValidateService _validateService;
        private ColorHelper _colorHelper;

        private StateParserHelper _stateParserHelper;

        public MoveFigureService( VectorHelper vectorHelper, MathHelper mathHelper, ValidateService validateService, ColorHelper colorHelper, StateParserHelper stateParserHelper)
        {
            _vectorHelper = vectorHelper;
            _mathHelper = mathHelper;
            _validateService = validateService;
            _colorHelper = colorHelper;
            _stateParserHelper = stateParserHelper;
        }

        public string Move(string boardStateString, int fromCoord, int toCoord, bool skipValidation = false)
        {
            var boardState = _stateParserHelper.ParseState(boardStateString);
            if (boardState.MustCoord != -1 && boardState.MustCoord != fromCoord)
            {
                return boardStateString;
            }

            string figures = boardState.Figures;

            var boardWidth = _mathHelper.Sqrt(figures.Length);

            var vector = _vectorHelper.CoordToVector(fromCoord, toCoord, boardWidth);
            if (vector == null)
            {
                return boardStateString;
            }
            
            if (_colorHelper.GetFigureColor(figures[fromCoord]) == FigureColor.White && boardState.Turn != Turn.White ||
                _colorHelper.GetFigureColor(figures[fromCoord]) == FigureColor.Black && boardState.Turn != Turn.Black)
            {
                return boardStateString;
            }

            bool isDie = false;
            if (
                !skipValidation &&
                figures[fromCoord] == Figures.WhitePawn || figures[fromCoord] == Figures.BlackPawn ||
                figures[fromCoord] == Figures.WhiteQueen || figures[fromCoord] == Figures.BlackQueen)
            {
                if (!_validateService.GetAllowedVectors(fromCoord, figures, out isDie).Contains(vector))
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

            for (int i = 1; i < vector.Length; i++)
            {
                var cleanCoord = _vectorHelper.VectorToCoord(fromCoord, new Vector()
                {
                    Length = i,
                    Direction = vector.Direction
                }, boardWidth);

                newFiguresBuilder[cleanCoord] = Figures.Empty;
            }

            var newFigures = newFiguresBuilder.ToString();
            var toggleTurn = true;


            if (isDie)
            {
                _validateService.GetAllowedVectors(toCoord, newFigures, out var isDie2);
                {
                    if (isDie2)
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


            var resultState = newFigures + nextTurn + (toggleTurn ? "" : toCoord);

            return resultState;
        }


    }
}
