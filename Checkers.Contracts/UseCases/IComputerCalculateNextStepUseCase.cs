using Checkers.DomainModels;

namespace Checkers.Contracts.UseCases
{
    public interface IComputerCalculateNextStepUseCase
    {
        GameState Execute(GameState gameState);
    }
}