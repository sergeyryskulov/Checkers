using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkers.DomainModels;
using Checkers.DomainModels.Enums;
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
                    $"/api/intellect/calculateStep?cells={gameState.Board}&turn={gameState.Turn}" +
                    GetMustGoFlag(gameState.MustGoFromPosition)
                    ));
            }
            else if (gameState.Turn == Turn.White)
            {
                links.Add(new LinkDto("moveFigure",
                    $"/api/board/moveFigure?cells={gameState.Board}&turn={gameState.Turn}" +
                    GetMustGoFlag(gameState.MustGoFromPosition) +
                    "&fromCoord={myFromCoordinate}&toCoord={myToCoordinate}"
                ));
            }


            return new GameStateDto(gameState.Board, gameState.Turn, gameState.MustGoFromPosition, links.ToArray());
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
