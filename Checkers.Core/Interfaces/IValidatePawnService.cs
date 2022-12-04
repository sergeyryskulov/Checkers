using Checkers.Core.Models.Aggregates;
using Checkers.DomainModels;

namespace Checkers.Core.Interfaces
{
    public interface IValidatePawnService
    {
        AllowedVectors GetAllowedMoveVectors(int fromPosition, Board board);
    }
}