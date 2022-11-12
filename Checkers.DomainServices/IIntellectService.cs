using Checkers.Core.Models.ValueObjects;

namespace Checkers.Core.Interfaces
{
    public interface IIntellectService
    {
        BoardState CalculateStep(BoardState boardState);
    }
}