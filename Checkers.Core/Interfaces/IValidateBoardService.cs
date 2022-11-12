using Checkers.Core.Models.ValueObjects;

namespace Checkers.Core.Interfaces
{
    public interface IValidateBoardService
    {
        bool CanMove(GameState gameState, int fromCoord, int toCoord);
    }
}