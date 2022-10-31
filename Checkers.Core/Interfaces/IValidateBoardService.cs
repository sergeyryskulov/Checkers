using Checkers.Core.Models.ValueObjects;

namespace Checkers.Core.Interfaces
{
    public interface IValidateBoardService
    {
        bool CanMove(BoardState boardState, int fromCoord, int toCoord);
    }
}