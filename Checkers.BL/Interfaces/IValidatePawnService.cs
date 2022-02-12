using Checkers.BL.Models;

namespace Checkers.BL.Services
{
    public interface IValidatePawnService
    {
        AllowedVectors GetAllowedMoveVectors(int coord, string figures);
    }
}