using Checkers.DomainModels;
using Checkers.Rules.Models;

namespace Checkers.Rules.Interfaces
{
    public interface IValidateQueenService
    {
        AllowedVectors GetAllowedMoveVectors(Board board, int fromPosition);
    }
}