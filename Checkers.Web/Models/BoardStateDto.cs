﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.Swagger.Annotations;

namespace Checkers.Web.Models
{
    [DisplayName("BoardState")]
    public class BoardStateDto
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

        public BoardStateDto(string cells, char turn, int? mustGoFrom, LinkDto[] links)
        {
            Cells = cells;
            Turn = turn;
            MustGoFrom = mustGoFrom;
            Links = links;
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
