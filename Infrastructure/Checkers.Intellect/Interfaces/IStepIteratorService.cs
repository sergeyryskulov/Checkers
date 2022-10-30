using System.Collections.Generic;
using Checkers.BL.Models;

namespace Checkers.BL.Services
{
    public interface IStepIteratorService
    {
        IEnumerable<NextStepVariant> GetNextStepVariants(BoardState inputState);
    }
}