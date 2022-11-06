using Checkers.Core.Interfaces;
using Checkers.Core.Models.ValueObjects;
using Checkers.Web.Factories;
using Checkers.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Checkers.Web.Controllers.api
{   
    public class BoardController : BaseApiController
    {
        private IMoveFigureService _moveFigureService;
        private IBoardStateDtoFactory _boardStateDtoFactory;
        
        public BoardController(IMoveFigureService moveFigureService, IBoardStateDtoFactory boardStateDtoFactory)
        {
            _moveFigureService = moveFigureService;
            _boardStateDtoFactory = boardStateDtoFactory;
        }

        /// <summary>
        /// Вычислить состояние доски после перемещения фигуры на доске
        /// </summary>        
        /// <param name="cells">Клетки шахматной доски.
        /// Строка из 64 символов, в которой перечисляются состояния клеток шахматной доски. Клетки перечисляются слева направо сверху вниз.
        /// Состояние клетки описывается одним из пяти символов:<br/>
        /// <b><i>p</i></b> - на клетке стоит простая черная шашка<br/>
        /// <b><i>P</i></b> - на клетке стоит простая белая шашка<br/>
        /// <b><i>q</i></b> - на клетке стоит черная дамка<br/>
        /// <b><i>Q</i></b> - на клетке стоит белая дамка<br/>
        /// <b><i>1</i></b> - пустая клетка
        /// </param>
        /// <param name="turn">Состояние хода игры<br/>
        /// <b><i>w</i></b> - ход на стороне белых<br/>
        /// <b><i>b</i></b> - ход на сторон черных<br/>
        /// <b><i>W</i></b> - белые выиграли<br/>
        /// <b><i>B</i></b> - черные выиграли<br/>        
        /// </param>
        /// <param name="mustGoFrom">Необязательный параметр, используется для повторного хода. Если на предыдущем шаге была срублена шашка, и есть возможность срубить еще одну шашку, то это поле указывает номер клетки, на которую передвинулась рубящая шашка перед повторным ходом. Нумерация клеток идет с нуля, слева направо сверху вниз.</param>
        /// <param name="fromCoord">Номер клетки, на которой стоит фигура, которую нужно передвинуть. Нумерация клеток идет с нуля, слева направо сверху вниз.</param>
        /// <param name="toCoord">Номер клетки, куда нужно передвинуть фигуры.</param>                                                
        /// <response code="200">Состояние доски после перемещения фигуры</response>        
        [HttpGet]
        [ResponseCache(VaryByQueryKeys = new[] { "*" }, Duration = 300)]
        public BoardStateDto MoveFigure(string cells, char turn, int? mustGoFrom, int fromCoord, int toCoord)
        {
            var boardState = _moveFigureService.Move(fromCoord, toCoord, new BoardState(cells, turn, mustGoFrom));

            var boardStateDto = _boardStateDtoFactory.CreateBoardStateDto(boardState);

            return boardStateDto;
        }
    }
}
