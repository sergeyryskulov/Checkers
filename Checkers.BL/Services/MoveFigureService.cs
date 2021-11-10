using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ckeckers.DAL.Repositories;

namespace Checkers.BL.Services
{
    public class MoveFigureService
    {
        private BoardRepository _boardRepository;

        public MoveFigureService(BoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }


        public string Move(int fromCoord, int toCoord, string registrationId)
        {
            string state = _boardRepository.Load(registrationId);

            if (!CanMove(fromCoord, toCoord, registrationId))
            {
                return state;
            }
            StringBuilder someString = new StringBuilder(state);

            someString[toCoord] = someString[fromCoord];
            someString[fromCoord] = '1';

            var result = someString.ToString();
            _boardRepository.Save(registrationId, result);
            return result;
        }

        private bool CanMove(int fromCoord, int toCoord, string userId)
        {
            string figures = _boardRepository.Load(userId);
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
