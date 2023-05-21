using System.Collections.Generic;
using Checkers.ComputerPlayer.Models;
using Checkers.DomainModels;
using Checkers.DomainModels.Models;

namespace Checkers.ComputerPlayer.Interfaces
{
    internal interface IStepIteratorService
    {
        IEnumerable<NextStepVariant> GetNextStepVariants(GameState inputState);
    }
}