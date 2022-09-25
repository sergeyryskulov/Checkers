using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkers.BL.Services;
using Checkers.BL.Extensions;

namespace Checkers.Web.Controllers.api
{    
    public class IntellectController : BaseApiController
    {
        private IIntellectService _intellectService;

        public IntellectController(IIntellectService intellectService)
        {
            _intellectService = intellectService;
        }

        [HttpPost]
        public string CalculateStep(string boardState)
        {
            return _intellectService.CalculateStep(boardState.ToBoardState()).ToBoardStateString();
        }
    }
}
