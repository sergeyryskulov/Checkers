using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkers.Core.Constants;
using Checkers.Core.Models.ValueObjects;
using Checkers.Web.Models;
using Microsoft.AspNetCore.SignalR;

namespace Checkers.Web.Factories
{
    public class BoardStateDtoFactory : IBoardStateDtoFactory
    {
        public BoardStateDto CreateBoardStateDto(BoardState boardState)
        {
            List<LinkDto> links = new List<LinkDto>();
            if (boardState.Turn == Turn.Black)
            {
                links.Add(
                    new LinkDto("calculateStep", 
                    $"/api/intellect/calculateStep?cells={boardState.Cells}&turn={boardState.Turn}" +
                    GetMustGoFlag(boardState.MustGoFrom)
                    ));
            }
            else if (boardState.Turn == Turn.White)
            {
                links.Add(new LinkDto("moveFigure",
                    $"/api/board/moveFigure?cells={boardState.Cells}&turn={boardState.Turn}" +
                    GetMustGoFlag(boardState.MustGoFrom) +
                    "&fromCoord={myFromCoordinate}&toCoord={myToCoordinate}"
                ));
            }


            return new BoardStateDto(boardState.Cells, boardState.Turn, boardState.MustGoFrom, links.ToArray());
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
}
