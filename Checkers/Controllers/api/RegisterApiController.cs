using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkers.BL.Services;

namespace Checkers.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterApiController : ControllerBase
    {
        private RegisterApiService _registerApiService;

        public RegisterApiController(RegisterApiService registerApiService)
        {
            _registerApiService = registerApiService;
        }

        public string Post()
        {
            return _registerApiService.Register();
        }
    }
}
