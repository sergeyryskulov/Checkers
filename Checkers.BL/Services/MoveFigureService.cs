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
        private PawnService _pawnService;

        public MoveFigureService(IBoardRepository boardRepository, VectorHelper vectorHelper, MathHelper mathHelper, PawnService pawnService)
        {
            _boardRepository = boardRepository;
            _vectorHelper = vectorHelper;
            _mathHelper = mathHelper;
            _pawnService = pawnService;

        }


        public string Move(int fromCoord, int toCoord, string registrationId)
        {
            string boardState = _boardRepository.Load(registrationId);

            char turn = boardState[boardState.Length - 1];
            string figures = boardState.Substring(0, boardState.Length - 1);


            var boardWidth = _mathHelper.Sqrt(figures.Length);

            var vector = _vectorHelper.CoordToVector(fromCoord, toCoord, boardWidth);
            if (vector == null)
            {
                return boardState;
            }


            if (figures[fromCoord] == Figures.WhitePawn || figures[fromCoord] == Figures.BlackPawn)
            {
                if (!_pawnService.GetAllowedVectors(fromCoord, figures).Contains(vector))
                {
                    return boardState;
                }
            }

            StringBuilder newFiguresBuilder = new StringBuilder(figures);
            newFiguresBuilder[toCoord] = newFiguresBuilder[fromCoord];
            newFiguresBuilder[fromCoord] = Figures.Empty;

            var isDie = false;
            for (int i = 1; i < vector.Length; i++)
            {
                var dieCoord= _vectorHelper.VectorToCoord(fromCoord, new Vector()
                {
                    Length = i,
                    Direction = vector.Direction
                }, boardWidth);

                if (newFiguresBuilder[dieCoord] != Figures.Empty)
                {
                    newFiguresBuilder[dieCoord] = Figures.Empty;
                    isDie = true;
                }
            }

            var newFigures = newFiguresBuilder.ToString();
            var toggleTurn = true;
            if (isDie)
            {
                if (_pawnService.GetAllowedVectors(toCoord, newFigures).Exists(t => t.Length == 2))
                {
                    toggleTurn = false;
                }
            }

            var nextTurn = turn;
            if (toggleTurn)
            {
                nextTurn = (turn == Turn.White ? Turn.Black : Turn.White);
            }

            var result = newFigures + nextTurn;
            _boardRepository.Save(registrationId, result);
            return result;
        }

      
    }
}
