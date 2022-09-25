using Checkers.BL.Models;

namespace Checkers.BL.Services
{
    public interface IDirectMoveService
    {
        string DirectMove(BoardState boardState, int fromCoord, int toCoord);
    }
}