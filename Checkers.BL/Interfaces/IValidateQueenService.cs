using Checkers.BL.Models;

namespace Checkers.BL.Interfaces
{
    public interface IValidateQueenService
    {
        AllowedVectors GetAllowedVectorsQueen(int coord, string figures);
    }
}