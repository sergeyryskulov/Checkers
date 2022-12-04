using System.Collections.Generic;
using Checkers.DomainModels;

namespace Checkers.DomainServices
{
    public interface IValidateRulesService
    {
        List<int> GetAllowedToPositions(Board board, int fromPosition);
    }
}