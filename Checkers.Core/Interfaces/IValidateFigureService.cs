using Checkers.Core.Models.Aggregates;
using Checkers.DomainModels.Aggregates;

namespace Checkers.Core.Interfaces
{
    public interface IValidateFigureService
    {
        AllowedVectors GetAllowedMoveVectors(int coord, Cells cells);
    }
}