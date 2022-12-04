using Checkers.DomainModels;

namespace Checkers.Rules.Interfaces
{
    public interface IValidateEatService
    {
        bool CanEatFigure(int fromPosition, Board figures);
    }
}
