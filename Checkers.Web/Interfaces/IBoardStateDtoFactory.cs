using Checkers.Core.Models.ValueObjects;
using Checkers.DomainModels;
using Checkers.Web.Models;

namespace Checkers.Web.Factories
{
    public interface IBoardStateDtoFactory
    {
        GameStateDto CreateBoardStateDto(GameState gameState);
    }
}