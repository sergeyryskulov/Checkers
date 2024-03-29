﻿using Checkers.DomainModels;
using Checkers.DomainModels.Models;

namespace Checkers.HumanPlayer.Interfaces;

internal interface IValidateHumanService
{
    bool CanMove(GameState gameState, int fromPosition, int toPosition);
}