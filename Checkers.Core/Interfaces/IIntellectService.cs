using Checkers.BL.Models;

namespace Checkers.BL.Services
{
    public interface IIntellectService
    {
        BoardState CalculateStep(BoardState boardState);
    }
}