using Checkers.DomainModels;

namespace Checkers.Core.Interfaces
{
    public interface IComputerPlayerService
    {
        GameState CalculateNextStep(GameState gameState);
    }
}