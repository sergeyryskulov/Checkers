using Checkers.DomainModels;

namespace Checkers.Intellect.Interfaces
{
    public interface IPositionWeightService
    {
        int GetWeightForWhite(Board board);
    }
}