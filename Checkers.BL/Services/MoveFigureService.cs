using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.BL.Constants;
using Checkers.BL.Helper;
using Ckeckers.DAL.Repositories;

namespace Checkers.BL.Services
{
    public class MoveFigureService
    {
        private IBoardRepository _boardRepository;
        private VectorHelper _vectorHelper;
        private MathHelper _mathHelper;
        private WhitePawnService _whitePawnService;

        public MoveFigureService(IBoardRepository boardRepository, VectorHelper vectorHelper, MathHelper mathHelper, WhitePawnService whitePawnService)
        {
            _boardRepository = boardRepository;
            _vectorHelper = vectorHelper;
            _mathHelper = mathHelper;
            _whitePawnService = whitePawnService;

        }


        public string Move(int fromCoord, int toCoord, string registrationId)
        {
            string figures = _boardRepository.Load(registrationId);

            if (!CanMove(fromCoord, toCoord, registrationId))
            {
                return figures;
            }
            StringBuilder stringBuilder = new StringBuilder(figures);
            stringBuilder[toCoord] = stringBuilder[fromCoord];
            stringBuilder[fromCoord] = '1';
            var result = stringBuilder.ToString();
            _boardRepository.Save(registrationId, result);
            return result;
        }

        

        private bool CanMove(int fromCoord, int toCoord, string userId)
        {
            string figures = _boardRepository.Load(userId);
            
            var vector = _vectorHelper.ConvertToVector(fromCoord, toCoord, _mathHelper.Sqrt(figures.Length));

            if (vector == null)
            {
                return false;
            }

            if (figures[fromCoord] == Figures.WhitePawn)
            {
                if (!_whitePawnService.GetAllowedVectors(fromCoord, figures).Contains(vector))
                {
                    return false;
                }
            }

            return true;
        }

      
    }
}
