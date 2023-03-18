using System.Collections.Generic;
using Checkers.DomainModels;

namespace Checkers.Contracts.Rules
{
    public interface IValidateRule
    {
        List<int> GetAllowedToPositions(Board board, int fromPosition);
    }
}