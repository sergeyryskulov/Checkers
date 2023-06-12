using Checkers.DomainModels;
using Checkers.DomainModels.Models;

namespace Checkers.HumanPlayer.Interfaces;

public interface IHumanTryMoveFigureUseCase
{
    GameState Execute(GameState gameState, int fromPosition, int toPosition);
}