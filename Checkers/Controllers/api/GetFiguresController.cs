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
        private GetFiguresService _getFiguresService;

        public GetFiguresController(GetFiguresService getFiguresService)
        {
            _getFiguresService = getFiguresService;
        }

        public string Post(string userId)
        {
            return _getFiguresService.GetFigures(userId);
        }
    }
}
