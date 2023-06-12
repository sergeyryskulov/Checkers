using Checkers.DomainModels;
using Checkers.DomainModels.Models;

namespace Checkers.ComputerPlayer.Interfaces;

internal interface IPositionWeightService
{
    int GetWeightForWhite(Board board);
}