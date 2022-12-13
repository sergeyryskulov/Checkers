using Checkers.DomainModels;

namespace Checkers.Contracts
{
    public interface IComputerPlayerService
    {
        GameState CalculateNextStep(GameState gameState);
    }
}