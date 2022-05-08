namespace Checkers.BL.Services
{
    public interface IValidateBoardService
    {
        bool CanMove(string boardStateString, int fromCoord, int toCoord);
    }
}