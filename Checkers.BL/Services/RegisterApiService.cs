using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.BL.Services
{
    public class RegisterApiService
    {
        public string Register()
        {
            return "" + Guid.NewGuid();
        }
    }
}
