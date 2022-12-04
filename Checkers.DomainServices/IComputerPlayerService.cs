using Checkers.DomainModels;

namespace Checkers.DomainServices
{
    public interface IComputerPlayerService
    {
        GameState CalculateNextStep(GameState gameState);
    }
}