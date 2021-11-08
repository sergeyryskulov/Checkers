using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.BL.Models;
using Ckeckers.DAL.Repositories;

namespace Checkers.BL.Services
{
    public class BoardService
    {
        private BoardRepository _boardRepository;

        public BoardService(BoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }


        public string Move(int fromCoord, int toCoord)
        {
            string state = _boardRepository.Load();

            if (!CanMove(fromCoord, toCoord))
            {
                return state;
            }
            StringBuilder someString = new StringBuilder(state);

            someString[toCoord] = someString[fromCoord];
            someString[fromCoord] = '1';

            var result = someString.ToString();
            _boardRepository.Save(result);
            return result;
        }
        public BoardViewModel GetBoardViewModel()
        {
            return null;
        }

        public bool CanMove(int fromCoord, int toCoord)
        {
            string figures = _boardRepository.Load();
            if (figures[toCoord] == 'k' || figures[toCoord] == 'K')
            {
                return false;
            }

            string fromColor = GetFigureColor(figures[fromCoord]);
            string toColor = GetFigureColor(figures[toCoord]);
            return fromColor != toColor;
        }

        private string GetFigureColor(char figure)
        {
            if ("rnbqkp".Contains(figure))
            {
                return "white";
            }
            if ("RNBQKP".Contains(figure))
            {
                return "black";
            }

            return "empty";
        }
    }
}
