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


        public MoveFigureController(MoveFigureService moveFigureService)
        {
            _moveFigureService = moveFigureService;
        }


        public string Post(int fromCoord, int toCoord, string registrationId)
        {
            return _moveFigureService.Move(fromCoord, toCoord, registrationId);
        }
    }
}
