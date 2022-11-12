﻿using System.Collections.Generic;
using Checkers.Core.Constants;
using Checkers.Core.Constants.Enums;
using Checkers.Core.Extensions;
using Checkers.Core.Interfaces;
using Checkers.Core.Models.ValueObjects;
using Checkers.Intellect.Interfaces;
using Checkers.Intellect.Models.ValueObjects;

namespace Checkers.Intellect.Services
{
    public class StepIteratorService : IStepIteratorService
    {
        private IValidateFiguresService _validateFiguresService;
        private IDirectMoveService _directMoveService;

        public StepIteratorService(IValidateFiguresService validateFiguresService, IDirectMoveService directMoveService)
        {
            _validateFiguresService = validateFiguresService;
            _directMoveService = directMoveService;
        }


        public IEnumerable<NextStepVariant> GetNextStepVariants(GameState gameState)
        {            
            string figures = gameState.Cells;
            var boardWidth = figures.Length.SquareRoot();

            var stateWithNoChangeTurn = new List<GameState>();

            for (int fromCoord = 0; fromCoord < figures.Length; fromCoord++)
            {
                if (gameState.MustGoFrom != null && fromCoord != gameState.MustGoFrom)
                {
                    continue;
                }

                if ((gameState.Turn == Turn.Black && figures[fromCoord].ToFigureColor() == FigureColor.Black) ||
                    (gameState.Turn == Turn.White && figures[fromCoord].ToFigureColor() == FigureColor.White
                    ))
                {
                    foreach (var newState in GetAllowedNextStates(gameState, fromCoord, figures, boardWidth))
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

        private List<GameState> GetAllowedNextStates(GameState inputState, int fromCoord, string figures, int boardWidth)
        {
            List<GameState> result = new List<GameState>();
            var allowedVectors = _validateFiguresService.GetAllowedMoveVariants(figures, fromCoord).Vectors;
            foreach (var allowedVector in allowedVectors)
            {
                var toCoord = allowedVector.ToCoord(fromCoord, boardWidth);
                var newState = _directMoveService.MoveFigureWithoutValidation(inputState, fromCoord, toCoord);
                if (!inputState.Equals(newState))
                {
                    result.Add(newState);
                }
            }

            return result;
        }
    }
}
