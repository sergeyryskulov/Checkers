using System.Collections.Generic;
using Checkers.DomainModels;

namespace Checkers.Core.Interfaces
{
    public interface IValidateRulesService
    {
        List<int> GetAllowedDestinations(Board board, int fromPosition);
    }
}