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
        private IMoveAndSaveFigureService _moveAndSaveFigureService;
        
        public MoveFigureController(IMoveAndSaveFigureService moveAndSaveFigureService)
        {
            _moveAndSaveFigureService = moveAndSaveFigureService;
        }


        public string Post(int fromCoord, int toCoord, string registrationId)
        {
            return _moveAndSaveFigureService.MoveAndSaveFigure(fromCoord, toCoord, registrationId);
        }
    }
}
