using Checkers.Core.Models.ValueObjects;

namespace Checkers.Core.Interfaces
{
    public interface IMoveFigureService
    {
        BoardState Move(int fromCoord, int toCoord, BoardState boardState);
    }
}