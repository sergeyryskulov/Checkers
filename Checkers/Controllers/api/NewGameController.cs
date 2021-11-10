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
    public class NewGameController : ControllerBase
    {
        private NewGameService _newGameService;


        public NewGameController(NewGameService newGameService)
        {
            _newGameService = newGameService;
        }

        public string Post(string registrationId)
        {
            return _newGameService.NewGame(registrationId);
        }
    }
}
