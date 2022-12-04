using Checkers.DomainModels;

namespace Checkers.ComputerPlayer.Interfaces
{
    public interface IPositionWeightService
    {
        int GetWeightForWhite(Board board);
    }
}