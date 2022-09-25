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
using Checkers.BL.Extensions;
using Checkers.BL.Models;

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
        [HttpPost]
        public BoardState MoveFigure(string cells, char turn, int? mustGoFrom, int fromCoord, int toCoord)
        {
            return _moveFigureService.Move(fromCoord, toCoord, new BoardState(cells, turn, mustGoFrom));
        }
    }
}
