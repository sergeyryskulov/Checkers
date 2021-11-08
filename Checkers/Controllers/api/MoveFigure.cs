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
    public class MoveFigure : ControllerBase
    {
        private BoardRepository _boardRepository;
        private BoardService _boardService;


        public MoveFigure(BoardRepository boardRepository, BoardService boardService)
        {
            _boardRepository = boardRepository;
            _boardService = boardService;
        }


        public string Post(int fromCoord, int toCoord)
        {
            return _boardService.Move(fromCoord, toCoord);
        }
    }
}
