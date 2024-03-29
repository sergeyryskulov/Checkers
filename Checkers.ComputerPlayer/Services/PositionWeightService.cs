﻿using Checkers.ComputerPlayer.Interfaces;
using Checkers.DomainModels;
using Checkers.DomainModels.Models;

namespace Checkers.ComputerPlayer.Services;

internal class PositionWeightService : IPositionWeightService
{
    public int GetWeightForWhite(Board board)
    {
        int result = 0;
        for (int cellIndex = 0; cellIndex < board.CellsCount;cellIndex++)
        {

            result += GetWeight(board, cellIndex);
        }

        return result;
    }

    private int GetWeight(Board board, int cellIndex)
    {
        if (board.IsEmptyCellAt(cellIndex))
        {
            return 0;
        }

        int result = 0;
        if (board.IsPawnAt(cellIndex))
        {
            result = 1;
        }

        if (board.IsQueenAt(cellIndex))
        {
            result = 2;
        }

        if (board.IsBlackFigureAt(cellIndex))
        {
            result = -1 * result;
        }

        return result;
    }
}