using Checkers.DomainModels;

namespace Checkers.Contracts.UseCases
{
    public interface IComputerCalculateNextStepUseCase
    {
        GameState CalculateNextStep(GameState gameState);
    }
}