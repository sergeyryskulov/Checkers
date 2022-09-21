namespace Checkers.BL.Services
{
    public interface IMoveFigureService
    {
        string Move(int fromCoord, int toCoord, string boardState);
    }
}