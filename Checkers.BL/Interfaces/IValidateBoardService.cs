using Checkers.BL.Models;

namespace Checkers.BL.Services
{
    public interface IValidateBoardService
    {
        bool CanMove(BoardState boardState, int fromCoord, int toCoord);
    }
}