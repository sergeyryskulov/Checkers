using Checkers.DomainModels;

namespace Checkers.DomainServices
{
    public interface IHumanPlayerService
    {
        GameState TryMoveFigure(GameState gameState, int fromPosition, int toPosition);
    }
}