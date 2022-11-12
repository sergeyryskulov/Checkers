﻿using Checkers.Core.Models.Aggregates;
using Checkers.DomainModels.Aggregates;

namespace Checkers.Core.Interfaces
{
    public interface IValidateFiguresService
    {
        AllowedVectors GetAllowedMoveVariants(Cells cells, int coord);
    }
}