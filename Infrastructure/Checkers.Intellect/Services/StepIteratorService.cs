﻿using System.Collections.Generic;
using Checkers.Core.Constants;
using Checkers.Core.Constants.Enums;
using Checkers.Core.Interfaces;
using Checkers.Core.Models.ValueObjects;
using Checkers.DomainModels.Aggregates;
using Checkers.Intellect.Interfaces;
using Checkers.Intellect.Models.ValueObjects;

namespace Checkers.Intellect.Services
{
    public class StepIteratorService : IStepIteratorService
    {
        private IValidateRulesService _validateRulesService;
        private IMoveRulesService _moveRulesService;

        public StepIteratorService(IValidateRulesService validateRulesService, IMoveRulesService moveRulesService)
        {
            _validateRulesService = validateRulesService;
            _moveRulesService = moveRulesService;
        }


        public IEnumerable<NextStepVariant> GetNextStepVariants(GameState gameState)
        {            
            var cells = gameState.Cells;
            var boardWidth = cells.BoardWidth();

            var stateWithNoChangeTurn = new List<GameState>();

            for (int fromCoord = 0; fromCoord < cells.CellsCount; fromCoord++)
            {
                if (gameState.MustGoFrom != null && fromCoord != gameState.MustGoFrom)
                {
                    continue;
                }

                if ((gameState.Turn == Turn.Black && cells.BlackFigureAt(fromCoord)) ||
                    (gameState.Turn == Turn.White && cells.WhiteFigureAt(fromCoord)
                    ))
                {
                    foreach (var newState in GetAllowedNextStates(gameState, fromCoord, cells, boardWidth))
                    {
                        if (newState.Turn==gameState.Turn)
                        {
                            stateWithNoChangeTurn.Add(newState);
                        }
                        else
                        {
                            yield return new NextStepVariant(
                                resultState: newState,
                                firstStepOfResultState : newState
                            );


                        }
                    }
                }
            }

            foreach (var noChangeCOlorState in stateWithNoChangeTurn)
            {
                foreach (var nextStepvariant in GetNextStepVariants(noChangeCOlorState))
                {
                    yield return new NextStepVariant(
                        resultState : nextStepvariant.ResultState,
                        firstStepOfResultState : noChangeCOlorState
                    );
                }
            }

        }

        private List<GameState> GetAllowedNextStates(GameState inputState, int fromCoord, Board board, int boardWidth)
        {
            List<GameState> result = new List<GameState>();
            var toCoordVariants = _validateRulesService.GetAllowedMoveVariants(board, fromCoord);
            foreach (var toCoord in toCoordVariants)
            {                
                var newState = _moveRulesService.MoveFigureWithoutValidation(inputState, fromCoord, toCoord);
                if (!inputState.Equals(newState))
                {
                    result.Add(newState);
                }
            }

            return result;
        }
    }
}
