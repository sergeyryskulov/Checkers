namespace Checkers.BL.Services
{
    public interface IMoveAndSaveFigureService
    {
        string MoveAndSaveFigure(int fromCoord, int toCoord, string registrationId);
    }
}