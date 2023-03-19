using Checkers.DomainModels;
using Checkers.DomainModels.Models;

namespace Checkers.Rules.Interfaces
{
    public interface IValidateEatService
    {
        bool CanEatFigure(int fromPosition, Board figures);
    }
}
