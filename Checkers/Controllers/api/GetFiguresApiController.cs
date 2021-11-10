using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ckeckers.DAL.Repositories;

namespace Checkers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetFiguresApiController : ControllerBase
    {
        private BoardRepository _boardRepository;

        public GetFiguresApiController(BoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        public string Post(string userId)
        {
            return _boardRepository.Load(userId);
        }
    }
}
