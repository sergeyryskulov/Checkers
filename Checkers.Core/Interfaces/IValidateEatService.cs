using Checkers.DomainModels;
using Checkers.DomainModels.Models;

namespace Checkers.Rules.Interfaces
{
    internal interface IValidateEatService
    {
        bool CanEatFigure(int fromPosition, Board figures);
    }
}
