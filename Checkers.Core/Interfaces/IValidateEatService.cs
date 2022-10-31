namespace Checkers.Core.Interfaces
{
    public interface IValidateEatService
    {
        bool CanEatFigure(int coord, string figures);
    }
}
