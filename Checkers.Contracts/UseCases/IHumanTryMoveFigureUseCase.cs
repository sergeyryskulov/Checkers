using Checkers.DomainModels;

namespace Checkers.Contracts.UseCases
{
    public interface IHumanTryMoveFigureUseCase
    {
        GameState Execute(GameState gameState, int fromPosition, int toPosition);
    }
}