using Checkers.DomainModels;

namespace Checkers.DomainServices
{
    public interface IMoveRulesService
    {
        GameState MoveFigureWithoutValidation(GameState gameState, int fromPosition, int toPosition);
    }
}