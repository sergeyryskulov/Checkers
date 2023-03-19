using Checkers.DomainModels;
using Checkers.DomainModels.Models;
using Checkers.Rules.Models;

namespace Checkers.Rules.Interfaces
{
    public interface IValidatePawnService
    {
        AllowedVectors GetAllowedMoveVectors(Board board, int fromPosition);
    }
}