using Checkers.DomainModels;
using Checkers.DomainModels.Models;
using Checkers.Rules.Models;

namespace Checkers.Rules.Interfaces;

internal interface IValidateQueenService
{
    AllowedVectors GetAllowedMoveVectors(Board board, int fromPosition);
}