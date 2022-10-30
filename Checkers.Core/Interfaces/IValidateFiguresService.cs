using Checkers.BL.Models;

namespace Checkers.BL.Services
{
    public interface IValidateFiguresService
    {
        AllowedVectors GetAllowedMoveVectors(int coord, string figures);
    }
}