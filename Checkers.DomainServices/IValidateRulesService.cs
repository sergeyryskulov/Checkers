using Checkers.DomainModels.Aggregates;
using System.Collections.Generic;

namespace Checkers.Core.Interfaces
{
    public interface IValidateRulesService
    {
        List<int> GetAllowedDestinations(Board board, int fromPosition);
    }
}