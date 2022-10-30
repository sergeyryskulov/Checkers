using Checkers.BL.Models;

namespace Checkers.BL.Services
{
    public interface IDirectMoveService
    {
        BoardState DirectMove(BoardState boardState, int fromCoord, int toCoord);
    }
}