using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkers.BL.Services;
using Ckeckers.DAL.Repositories;

namespace Checkers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetFiguresController : ControllerBase
    {
        private IGetFiguresService _getFiguresService;

        public GetFiguresController(IGetFiguresService getFiguresService)
        {
            _getFiguresService = getFiguresService;
        }

        public string Post(string registrationId)
        {
            return _getFiguresService.GetFigures(registrationId);
        }
    }
}
