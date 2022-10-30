using Checkers.BL.Models;

namespace Checkers.BL.Interfaces
{
    public interface IValidateQueenService
    {
        AllowedVectors GetAllowedMoveVectors(int coord, string figures);
    }
}