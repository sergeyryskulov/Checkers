﻿using System.Collections.Generic;
using Checkers.ComputerPlayer.Interfaces;
using Checkers.ComputerPlayer.Models;
using Checkers.DomainModels;
using Checkers.DomainModels.Enums;
using Checkers.DomainModels.Models;
using Checkers.Rules.Interfaces;

namespace Checkers.ComputerPlayer.Services;

internal class StepIteratorService : IStepIteratorService
{
    private IValidateRule _validateRule;
    private IMoveRule _moveRule;

    public StepIteratorService(IValidateRule validateRule, IMoveRule moveRule)
    {
        _validateRule = validateRule;
        _moveRule = moveRule;
    }

    public IEnumerable<NextStepVariant> GetNextStepVariants(GameState gameState)
    {            
        var cells = gameState.Board;
            
        var stateWithNoChangeTurn = new List<GameState>();

        for (int fromPosition = 0; fromPosition < cells.CellsCount; fromPosition++)
        {
            if (gameState.MustGoFromPosition != null && fromPosition != gameState.MustGoFromPosition)
            {
                continue;
            }

            if ((gameState.Turn == Turn.Black && cells.IsBlackFigureAt(fromPosition)) ||
                (gameState.Turn == Turn.White && cells.IsWhiteFigureAt(fromPosition)
                ))
            {
                foreach (var newState in GetAllowedNextStates(gameState, fromPosition, cells))
                {
                    if (newState.Turn==gameState.Turn)
                    {
                        stateWithNoChangeTurn.Add(newState);
                    }
                    else
                    {
                        yield return new NextStepVariant(
                            ResultState: newState,
                            FirstStepOfResultState : newState
                        );


                    }
                }
            }
        }

        foreach (var noChangeColorState in stateWithNoChangeTurn)
        {
            foreach (var nextStepVariant in GetNextStepVariants(noChangeColorState))
            {
                yield return nextStepVariant with { FirstStepOfResultState = noChangeColorState };
            }
        }

    }

    private List<GameState> GetAllowedNextStates(GameState inputState, int fromPosition, Board board)
    {
        List<GameState> result = new List<GameState>();
        var toCoordVariants = _validateRule.GetAllowedToPositions(board, fromPosition);
        foreach (var toCoord in toCoordVariants)
        {                
            var newState = _moveRule.MoveFigureWithoutValidation(inputState, fromPosition, toCoord);
            if (!inputState.Equals(newState))
            {
                result.Add(newState);
            }
        }

        return result;
    }
}