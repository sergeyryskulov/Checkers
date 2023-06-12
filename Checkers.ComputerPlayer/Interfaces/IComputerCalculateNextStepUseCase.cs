using Checkers.DomainModels;
using Checkers.DomainModels.Models;

namespace Checkers.ComputerPlayer.Interfaces;

public interface IComputerCalculateNextStepUseCase
{
    GameState Execute(GameState gameState);
}