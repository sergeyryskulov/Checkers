using Checkers.Core.Models.Aggregates;

namespace Checkers.Core.Interfaces
{
    public interface IValidatePawnService
    {
        AllowedVectors GetAllowedMoveVectors(int coord, string figures);
    }
}