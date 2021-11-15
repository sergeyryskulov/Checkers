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
    public class MoveFigureController : ControllerBase
    {
        private MoveFigureService _moveFigureService;
        private BoardRepository _boardRepository;

        public MoveFigureController(MoveFigureService moveFigureService, BoardRepository boardRepository)
        {
            _moveFigureService = moveFigureService;
            _boardRepository = boardRepository;
        }


        public string Post(int fromCoord, int toCoord, string registrationId)
        {
            var oldState= _boardRepository.Load(registrationId);
            var newState= _moveFigureService.Move(oldState, fromCoord, toCoord);
            if (oldState != newState)
            {
                _boardRepository.Save(registrationId, newState);
            }

            return newState;
        }
    }
}
