using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.BL.Services;
using Checkers.Web.Controllers.api;

namespace Checkers.Controllers
{    
    public class BoardController : BaseApiController
    {
        private IMoveFigureService _moveFigureService;
        
        public BoardController(IMoveFigureService moveFigureService)
        {
            _moveFigureService = moveFigureService;
        }        

        [HttpPost]
        public string MoveFigure(string boardState, int fromCoord, int toCoord)
        {
            return _moveFigureService.Move(fromCoord, toCoord, boardState);
        }
    }
}
