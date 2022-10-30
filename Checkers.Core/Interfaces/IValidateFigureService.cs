using Checkers.BL.Models;

namespace Checkers.BL.Services
{
    public interface IValidateFigureService
    {
        AllowedVectors GetAllowedMoveVectors(int coord, string figures);
    }
}