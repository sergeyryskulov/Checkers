using Checkers.DomainModels;

namespace Checkers.Core.Interfaces
{
    public interface IMoveRulesService
    {
        GameState MoveFigureWithoutValidation(GameState gameState, int fromPosition, int toPosition);
    }
}