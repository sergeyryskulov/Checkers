using Checkers.Core.Models.Aggregates;

namespace Checkers.Core.Interfaces
{
    public interface IValidateFiguresService
    {
        AllowedVectors GetAllowedMoveVariants(string figures, int coord);
    }
}