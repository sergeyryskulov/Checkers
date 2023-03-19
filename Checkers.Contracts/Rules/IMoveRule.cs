using Checkers.DomainModels;
using Checkers.DomainModels.Models;

namespace Checkers.Contracts.Rules
{
    public interface IMoveRule
    {
        GameState MoveFigureWithoutValidation(GameState gameState, int fromPosition, int toPosition);
    }
}