using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkers.BL.Services;

namespace Checkers.Web.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntellectStepController : ControllerBase
    {
        private IntellectService _intellectService;

        public IntellectStepController(IntellectService intellectService)
        {
            _intellectService = intellectService;
        }

        public string Post(string registrationId)
        {
            return _intellectService.IntellectStep(registrationId);
        }
    }
}
