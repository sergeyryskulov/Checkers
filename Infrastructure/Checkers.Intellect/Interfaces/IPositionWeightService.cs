using Checkers.DomainModels.Aggregates;

namespace Checkers.Intellect.Interfaces
{
    public interface IPositionWeightService
    {
        int GetWeightForWhite(Board board);
    }
}