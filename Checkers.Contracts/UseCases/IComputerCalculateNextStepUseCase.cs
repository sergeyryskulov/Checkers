﻿using Checkers.DomainModels;
using Checkers.DomainModels.Models;

namespace Checkers.Contracts.UseCases
{
    public interface IComputerCalculateNextStepUseCase
    {
        GameState Execute(GameState gameState);
    }
}