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
        private IBoardRepository _boardRepository;
        private VectorHelper _vectorHelper;
        private MathHelper _mathHelper;
        private ValidateService _validateService;
        private ColorHelper _colorHelper;


        public MoveFigureService(IBoardRepository boardRepository, VectorHelper vectorHelper, MathHelper mathHelper, ValidateService validateService, ColorHelper colorHelper)
        {
            _boardRepository = boardRepository;
            _vectorHelper = vectorHelper;
            _mathHelper = mathHelper;
            _validateService = validateService;
            _colorHelper = colorHelper;
        }

        public string Move(int fromCoord, int toCoord, string registrationId)
        {
            string boardState = _boardRepository.Load(registrationId);

            char turn = boardState[boardState.Length - 1];
            string figures = boardState.Substring(0, boardState.Length - 1);
            if (turn != Turn.White && turn != Turn.Black)
            {
                figures = boardState.Split(new char[]
                {
                    Turn.White, Turn.Black
                }, StringSplitOptions.None)[0];

                var mustCoordString = boardState.Split(new char[]
                {
                    Turn.White, Turn.Black
                }, StringSplitOptions.None)[1];

                if (fromCoord != int.Parse(mustCoordString))
                {
                    return boardState;
                }

                turn = boardState.Contains(Turn.White) ? Turn.White : Turn.Black;
            }
            
            var boardWidth = _mathHelper.Sqrt(figures.Length);

            var vector = _vectorHelper.CoordToVector(fromCoord, toCoord, boardWidth);
            if (vector == null)
            {
                return boardState;
            }

            if (_colorHelper.GetFigureColor(figures[fromCoord]) == FigureColor.White && turn != Turn.White ||
                _colorHelper.GetFigureColor(figures[fromCoord]) == FigureColor.Black && turn != Turn.Black)
            {
                return boardState;
            }

            bool isDie = false;
            if (figures[fromCoord] == Figures.WhitePawn || figures[fromCoord] == Figures.BlackPawn ||
                figures[fromCoord] == Figures.WhiteQueen || figures[fromCoord] == Figures.BlackQueen)
            {
                if (!_validateService.GetAllowedVectors(fromCoord, figures, out isDie).Contains(vector))
                {
                    return boardState;
                }
            }

            StringBuilder newFiguresBuilder = new StringBuilder(figures);
            newFiguresBuilder[toCoord] = newFiguresBuilder[fromCoord];
            if (toCoord < boardWidth && turn==Turn.White)
            {
                newFiguresBuilder[toCoord] = Figures.WhiteQueen;
            }
            if (toCoord >= boardWidth * (boardWidth - 1) && turn == Turn.Black)
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

            var nextTurn = turn;
            if (toggleTurn)
            {
                nextTurn = (turn == Turn.White ? Turn.Black : Turn.White);
            }

            var result = newFigures + nextTurn + (toggleTurn?"" : toCoord);
            _boardRepository.Save(registrationId, result);
            return result;
        }

      
    }
}
