using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ckeckers.DAL.Repositories;

namespace Checkers.BL.Services
{
    public class NewGameService
    {
        private IBoardRepository _boardRepository;

        public NewGameService(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }


        public string NewGame(string registrationId)
        {
            _boardRepository.Save(registrationId, "");
            return _boardRepository.Load(registrationId);
        }
    }
}
