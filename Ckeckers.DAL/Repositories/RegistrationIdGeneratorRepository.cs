using System;
using System.Collections.Generic;
using System.Text;

namespace Ckeckers.DAL.Repositories
{
    public class RegistrationIdGeneratorRepository : IRegistrationIdGeneratorRepository
    {
        public string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
