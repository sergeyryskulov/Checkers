using Checkers.DomainModels;

namespace Checkers.Contracts.Rules
{
    public interface IMoveRule
    {
        GameState MoveFigureWithoutValidation(GameState gameState, int fromPosition, int toPosition);
    }
}