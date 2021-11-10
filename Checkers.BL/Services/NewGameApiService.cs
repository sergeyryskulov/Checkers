using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ckeckers.DAL.Repositories;

namespace Checkers.BL.Services
{
    public class NewGameApiService
    {
        private BoardRepository _boardRepository;

        public NewGameApiService(BoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }


        public string NewGame(string userId)
        {
            _boardRepository.Save(userId, "");
            return _boardRepository.Load(userId);
        }
    }
}
