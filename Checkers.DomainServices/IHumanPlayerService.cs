using Checkers.DomainModels;

namespace Checkers.Contracts
{
    public interface IHumanPlayerService
    {
        GameState TryMoveFigure(GameState gameState, int fromPosition, int toPosition);
    }
}