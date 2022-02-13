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
        private IBoardRepository _boardRepository;
        private IRegistrationIdGeneratorRepository _registrationIdGeneratorRepository;


        public RegisterService(IBoardRepository boardRepository, IRegistrationIdGeneratorRepository registrationIdGeneratorRepository)
        {
            _boardRepository = boardRepository;
            _registrationIdGeneratorRepository = registrationIdGeneratorRepository;
        }

        public string Register(string position)
        {
            string registrationId = _registrationIdGeneratorRepository.GenerateId();
            if (position != "")
            {
                _boardRepository.Save(registrationId, position);
            }
            return registrationId;
        }
    }
}
