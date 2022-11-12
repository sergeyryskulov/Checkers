using Checkers.Core.Models.ValueObjects;

namespace Checkers.Core.Interfaces
{
    public interface IHumanPlayerService
    {
        GameState TryMoveFigure(GameState gameState, int fromCoord, int toCoord);
    }
}