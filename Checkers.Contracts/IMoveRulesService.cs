using Checkers.DomainModels;

namespace Checkers.Contracts
{
    public interface IMoveRulesService
    {
        GameState MoveFigureWithoutValidation(GameState gameState, int fromPosition, int toPosition);
    }
}