using Checkers.Core.Models.ValueObjects;

namespace Checkers.Core.Interfaces
{
    public interface IDirectMoveService
    {
        BoardState DirectMove(BoardState boardState, int fromCoord, int toCoord);
    }
}