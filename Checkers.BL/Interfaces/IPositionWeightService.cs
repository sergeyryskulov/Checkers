namespace Checkers.BL.Services
{
    public interface IPositionWeightService
    {
        int GetWeightForWhite(string boardState);
    }
}