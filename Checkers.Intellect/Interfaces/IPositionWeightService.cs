using Checkers.DomainModels;
using Checkers.DomainModels.Models;

namespace Checkers.ComputerPlayer.Interfaces
{
    public interface IPositionWeightService
    {
        int GetWeightForWhite(Board board);
    }
}