using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ckeckers.DAL.Repositories;

namespace Checkers.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewGameApiController : ControllerBase
    {
        private BoardRepository _boardRepository;

        public NewGameApiController(BoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        public string Post(string userId)
        {
            _boardRepository.Save(userId, "");
            return _boardRepository.Load(userId);
        }
    }
}
