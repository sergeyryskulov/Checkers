using Checkers.Core.Models.Aggregates;

namespace Checkers.Core.Interfaces
{
    public interface IValidateQueenService
    {
        AllowedVectors GetAllowedMoveVectors(int coord, string figures);
    }
}