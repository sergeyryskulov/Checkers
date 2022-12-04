using System.Collections.Generic;
using Checkers.DomainModels;
using Checkers.Intellect.Models.ValueObjects;

namespace Checkers.Intellect.Interfaces
{
    public interface IStepIteratorService
    {
        IEnumerable<NextStepVariant> GetNextStepVariants(GameState inputState);
    }
}