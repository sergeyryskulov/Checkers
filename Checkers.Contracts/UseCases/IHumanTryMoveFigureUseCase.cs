using Checkers.DomainModels;
using Checkers.DomainModels.Models;

namespace Checkers.Contracts.UseCases
{
    public interface IHumanTryMoveFigureUseCase
    {
        GameState Execute(GameState gameState, int fromPosition, int toPosition);
    }
}