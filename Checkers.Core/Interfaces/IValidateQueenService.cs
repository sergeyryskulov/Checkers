using Checkers.Core.Models.Aggregates;
using Checkers.DomainModels.Aggregates;

namespace Checkers.Core.Interfaces
{
    public interface IValidateQueenService
    {
        AllowedVectors GetAllowedMoveVectors(int coord, Board board);
    }
}