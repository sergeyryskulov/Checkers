using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkers.BL.Services;
using Checkers.BL.Extensions;
using Checkers.BL.Models;

namespace Checkers.Web.Controllers.api
{    
    public class IntellectController : BaseApiController
    {
        private IIntellectService _intellectService;

        public IntellectController(IIntellectService intellectService)
        {
            _intellectService = intellectService;
        }


        /// <summary>
        /// Вычислить следующий ход шашек с помощью искусственного интеллекта
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
        [HttpPost]
        public BoardState CalculateStep(string cells, char turn, int? mustGoFrom)
        {
            return _intellectService.CalculateStep(new BoardState(cells, turn, mustGoFrom));
        }
    }
}
