using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.BL.Services;
using Checkers.Web.Controllers.api;
using Swashbuckle.Swagger.Annotations;

namespace Checkers.Controllers
{   
    public class BoardController : BaseApiController
    {
        private IMoveFigureService _moveFigureService;
        
        public BoardController(IMoveFigureService moveFigureService)
        {
            _moveFigureService = moveFigureService;
        }

        /// <summary>
        /// Передвинуть фигуру на доске
        /// </summary>        
        /// <param name="boardState">Состояние доски.
        /// 123
        /// </param>
        /// <returns>A newly created TodoItem</returns>       
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item #1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public string MoveFigure(string boardState, int fromCoord, int toCoord)
        {
            return _moveFigureService.Move(fromCoord, toCoord, boardState);
        }
    }
}
