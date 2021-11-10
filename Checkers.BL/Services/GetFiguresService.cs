using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ckeckers.DAL.Repositories;

namespace Checkers.BL.Services
{
    public class GetFiguresService
    {
        private BoardRepository _boardRepository;

        public GetFiguresService(BoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        public string GetFigures(string registrationId)
        {
            return _boardRepository.Load(registrationId);
        }
    }
}
