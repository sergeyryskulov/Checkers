using Checkers.Core.Models.Aggregates;

namespace Checkers.Core.Interfaces
{
    public interface IValidateFiguresService
    {
        AllowedVectors GetAllowedMoveVectors(int coord, string figures);
    }
}