using System.Collections.Generic;
using Checkers.DomainModels;
using Checkers.DomainModels.Models;

namespace Checkers.Rules.Interfaces
{
    public interface IValidateRule
    {
        List<int> GetAllowedToPositions(Board board, int fromPosition);
    }
}