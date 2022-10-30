using Checkers.BL.Models;

namespace Checkers.BL.Services
{
    public interface IMoveFigureService
    {
        BoardState Move(int fromCoord, int toCoord, BoardState boardState);
    }
}