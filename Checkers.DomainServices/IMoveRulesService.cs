using Checkers.Core.Models.ValueObjects;

namespace Checkers.Core.Interfaces
{
    public interface IMoveRulesService
    {
        GameState MoveFigureWithoutValidation(GameState gameState, int fromCoord, int toCoord);
    }
}