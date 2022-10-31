using Checkers.Core.Models.Aggregates;

namespace Checkers.Core.Interfaces
{
    public interface IValidateFigureService
    {
        AllowedVectors GetAllowedMoveVectors(int coord, string figures);
    }
}