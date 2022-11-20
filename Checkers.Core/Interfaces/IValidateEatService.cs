using Checkers.DomainModels.Aggregates;

namespace Checkers.Core.Interfaces
{
    public interface IValidateEatService
    {
        bool CanEatFigure(int fromPosition, Board figures);
    }
}
