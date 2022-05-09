using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.BL.Interfaces
{
    public interface IValidateEatService
    {
        bool CanEatFigure(int coord, string figures);
    }
}
