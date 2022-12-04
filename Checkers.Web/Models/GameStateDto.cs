using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Checkers.DomainModels;
using Checkers.DomainModels.Enums;
using Swashbuckle.Swagger.Annotations;

namespace Checkers.Web.Models
{
    [DisplayName("GameStateDto")]
    public class GameStateDto
    {
        /// <summary>
        /// Состояние клеток шахматной доски
        /// </summary>
        public string Cells { get; }

        /// <summary>
        /// Состояние хода игры
        /// </summary>
        public char Turn { get; }
        /// <summary>
        /// Номер клетки, с который должен начинаться следующий ход (необязательное поле)
        /// </summary>
        public int? MustGoFrom { get; }

        /// <summary>
        /// Ссылки на возможные последующие операции
        /// </summary>
        public LinkDto[] Links { get; }

        public GameStateDto(Board board, Turn turn, int? mustGoFrom, LinkDto[] links)
        {
            Cells = board.ToString();
            Turn = (char)turn;
            MustGoFrom = mustGoFrom;
            Links = links;
        }

        public GameStateDto(GameState gameState)
        {
            Cells = gameState.Board.ToString();
            Turn = (char)gameState.Turn;
            MustGoFrom = gameState.MustGoFromPosition;
            
            List<LinkDto> links = new List<LinkDto>();
            if (gameState.Turn == DomainModels.Enums.Turn.Black)
            {
                links.Add(
                    new LinkDto("calculateStep",
                        $"/api/intellect/calculateStep?cells={gameState.Board}&turn={(char)gameState.Turn}" +
                        GetMustGoFlag(gameState.MustGoFromPosition)
                    ));
            }
            else if (gameState.Turn == DomainModels.Enums.Turn.White)
            {
                links.Add(new LinkDto("moveFigure",
                    $"/api/board/moveFigure?cells={gameState.Board}&turn={(char)gameState.Turn}" +
                    GetMustGoFlag(gameState.MustGoFromPosition) +
                    "&fromPosition={myFromPosition}&toPosition={myToPosition}"
                ));
            }

            Links = links.ToArray();

        }

        private string GetMustGoFlag(int? mustGoFrom)
        {
            if (mustGoFrom == null)
            {
                return "";
            }

            return $"&mustGoFrom={mustGoFrom}";
        }

    }


    [DisplayName("Link")]
    public class LinkDto
    {
        /// <summary>
        /// Тип ссылки<br/>
        /// <b>moveFigure</b> - перемещение фигуры<br/>
        /// <b>intellectStep</b> - вычисление следующего шага искусственным интеллектом
        /// </summary>
        public string Rel { get; private set; }

        /// <summary>
        /// Ссылка
        /// </summary>
        public string Href { get; private set; }
        
        public LinkDto(string relation, string href)
        {
            Rel = relation;
            Href = href;
        }
    }
}
