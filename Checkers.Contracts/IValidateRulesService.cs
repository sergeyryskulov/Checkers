using System.Collections.Generic;
using Checkers.DomainModels;

namespace Checkers.Contracts
{
    public interface IValidateRulesService
    {
        List<int> GetAllowedToPositions(Board board, int fromPosition);
    }
}