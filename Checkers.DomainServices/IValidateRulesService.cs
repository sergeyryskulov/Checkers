using Checkers.DomainModels.Aggregates;
using System.Collections.Generic;

namespace Checkers.Core.Interfaces
{
    public interface IValidateRulesService
    {
        List<int> GetAllowedMoveVariants(Board board, int coord);
    }
}