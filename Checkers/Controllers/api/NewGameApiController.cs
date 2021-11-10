using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkers.BL.Services;
using Ckeckers.DAL.Repositories;

namespace Checkers.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewGameApiController : ControllerBase
    {
        private NewGameApiService _newGameApiService;


        public NewGameApiController(NewGameApiService newGameApiService)
        {
            _newGameApiService = newGameApiService;
        }

        public string Post(string userId)
        {
            return _newGameApiService.NewGame(userId);
        }
    }
}
