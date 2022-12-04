using Checkers.Core.Models.Aggregates;
using Checkers.DomainModels;

namespace Checkers.Core.Interfaces
{
    public interface IValidateFigureService
    {
        AllowedVectors GetAllowedMoveVectors(int coord, Board board);
    }
}