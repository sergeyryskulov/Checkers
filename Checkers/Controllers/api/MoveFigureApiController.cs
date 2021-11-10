using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.BL.Services;
using Ckeckers.DAL.Repositories;

namespace Checkers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoveFigureApiController : ControllerBase
    {
        private BoardService _boardService;


        public MoveFigureApiController(BoardService boardService)
        {
            _boardService = boardService;
        }


        public string Post(int fromCoord, int toCoord, string userId)
        {
            return _boardService.Move(fromCoord, toCoord, userId);
        }
    }
}
