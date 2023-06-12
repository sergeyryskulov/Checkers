using Checkers.DomainModels;
using Checkers.DomainModels.Models;

namespace Checkers.Rules.Interfaces;

public interface IMoveRule
{
    GameState MoveFigureWithoutValidation(GameState gameState, int fromPosition, int toPosition);
}