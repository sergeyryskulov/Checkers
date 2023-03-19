﻿using Checkers.DomainModels;
using Checkers.DomainModels.Models;
using Checkers.Rules.Models;

namespace Checkers.Rules.Interfaces
{
    public interface IValidateFigureService
    {
        AllowedVectors GetAllowedMoveVectors(Board board, int fromPosition);
    }
}