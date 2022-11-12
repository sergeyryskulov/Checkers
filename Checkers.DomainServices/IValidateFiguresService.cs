using Checkers.Core.Models.Aggregates;

namespace Checkers.Core.Interfaces
{
    public interface IValidateFiguresService
    {
        AllowedVectors GetAllowedMoveVariants(int coord, string figures);
    }
}