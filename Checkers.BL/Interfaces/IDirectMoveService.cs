namespace Checkers.BL.Services
{
    public interface IDirectMoveService
    {
        string DirectMove(string boardStateString, int fromCoord, int toCoord);
    }
}