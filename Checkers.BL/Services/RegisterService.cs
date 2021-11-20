using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ckeckers.DAL.Repositories;

namespace Checkers.BL.Services
{
    public class RegisterService
    {
        private BoardRepository _boardRepository;

        public RegisterService(BoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        public string Register(string position)
        {
            string registrationId = "" + Guid.NewGuid();
            if (position != "")
            {
                _boardRepository.Save(registrationId, position);
            }
            return registrationId;
        }
    }
}
