using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkers.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterApiController : ControllerBase
    {
        public string Post()
        {
            string userId = "" + Guid.NewGuid();
            return userId;
        }
    }
}
