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
        public GameStateDto CreateBoardStateDto(GameState gameState)
        {
            List<LinkDto> links = new List<LinkDto>();
            if (gameState.Turn == Turn.Black)
            {
                links.Add(
                    new LinkDto("calculateStep", 
                    $"/api/intellect/calculateStep?cells={gameState.Cells}&turn={gameState.Turn}" +
                    GetMustGoFlag(gameState.MustGoFrom)
                    ));
            }
            else if (gameState.Turn == Turn.White)
            {
                links.Add(new LinkDto("moveFigure",
                    $"/api/board/moveFigure?cells={gameState.Cells}&turn={gameState.Turn}" +
                    GetMustGoFlag(gameState.MustGoFrom) +
                    "&fromCoord={myFromCoordinate}&toCoord={myToCoordinate}"
                ));
            }


            return new GameStateDto(gameState.Cells, gameState.Turn, gameState.MustGoFrom, links.ToArray());
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
