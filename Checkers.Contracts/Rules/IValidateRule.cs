using System.Collections.Generic;
using Checkers.DomainModels;
using Checkers.DomainModels.Models;

namespace Checkers.Contracts.Rules
{
    public interface IValidateRule
    {
        List<int> GetAllowedToPositions(Board board, int fromPosition);
    }
}