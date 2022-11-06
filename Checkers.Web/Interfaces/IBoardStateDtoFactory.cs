using Checkers.Core.Models.ValueObjects;
using Checkers.Web.Models;

namespace Checkers.Web.Factories
{
    public interface IBoardStateDtoFactory
    {
        BoardStateDto CreateBoardStateDto(BoardState boardState);
    }
}