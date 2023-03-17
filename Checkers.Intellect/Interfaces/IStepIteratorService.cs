using System.Collections.Generic;
using Checkers.ComputerPlayer.Models;
using Checkers.DomainModels;

namespace Checkers.ComputerPlayer.Interfaces
{
    public interface IStepIteratorService
    {
        IEnumerable<NextStepVariant> GetNextStepVariants(GameState inputState);
    }
}