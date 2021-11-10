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
    public class GetFiguresApiController : ControllerBase
    {
        private GetFiguresApiService _getFiguresApiService;

        public GetFiguresApiController(GetFiguresApiService getFiguresApiService)
        {
            _getFiguresApiService = getFiguresApiService;
        }

        public string Post(string userId)
        {
            return _getFiguresApiService.GetFigures(userId);
        }
    }
}
