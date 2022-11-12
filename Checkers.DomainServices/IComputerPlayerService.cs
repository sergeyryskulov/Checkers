using Checkers.Core.Models.ValueObjects;

namespace Checkers.Core.Interfaces
{
    public interface IComputerPlayerService
    {
        GameState CalculateNextStep(GameState gameState);
    }
}