using Checkers.DomainModels;

namespace Checkers.Core.Interfaces
{
    public interface IValidateHumanService
    {
        bool CanMove(GameState gameState, int fromPosition, int toPosition);
    }
}