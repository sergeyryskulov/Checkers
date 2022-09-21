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
        private IIntellectService _intellectService;

        public IntellectStepController(IIntellectService intellectService)
        {
            _intellectService = intellectService;
        }

        public string Post(string boardState)
        {
            return _intellectService.IntellectStep(boardState);
        }
    }
}
