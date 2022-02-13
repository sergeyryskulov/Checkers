namespace Checkers.BL.Services
{
    public interface IMoveFigureService
    {
        string Move(string boardStateString, int fromCoord, int toCoord, bool skipValidation = false);
    }
}