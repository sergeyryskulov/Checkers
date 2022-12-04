using Checkers.DomainModels;

namespace Checkers.HumanPlayer.Interfaces
{
    public interface IValidateHumanService
    {
        bool CanMove(GameState gameState, int fromPosition, int toPosition);
    }
}