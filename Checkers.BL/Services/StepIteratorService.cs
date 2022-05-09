using System;
using System.Collections.Generic;
using System.Text;
using Checkers.BL.Constants;
using Checkers.BL.Extensions;
using Checkers.BL.Models;

namespace Checkers.BL.Services
{
    public class StepIteratorService
    {
        private ValidateFiguresService _validateFiguresService;
        private DirectMoveService _directMoveService;

        public StepIteratorService(ValidateFiguresService validateFiguresService, DirectMoveService directMoveService)
        {
            _validateFiguresService = validateFiguresService;
            _directMoveService = directMoveService;
        }


        public IEnumerable<NextStepVariant> GetNextStepVariants(string inputState)
        {
            var boardState = inputState.ToBoardState();
            string figures = boardState.Figures;
            var boardWidth = figures.Length.SquareRoot();

            var stateWithNoChangeTurn = new List<string>();

            for (int fromCoord = 0; fromCoord < figures.Length; fromCoord++)
            {
                if (boardState.MustCoord != -1 && fromCoord != boardState.MustCoord)
                {
                    continue;
                }

                if ((boardState.Turn == Turn.Black && figures[fromCoord].ToFigureColor() == FigureColor.Black) ||
                    (boardState.Turn == Turn.White && figures[fromCoord].ToFigureColor() == FigureColor.White
                    ))
                {
                    foreach (var newState in GetAllowedNextStates(inputState, fromCoord, figures, boardWidth))
                    {
                        if (newState.Contains(boardState.Turn))
                        {
                            stateWithNoChangeTurn.Add(newState);
                        }
                        else
                        {
                            yield return new NextStepVariant()
                            {
                                ResultState = newState,
                                FirstStepOfResultState = newState
                            };

                        }
                    }
                }
            }

            foreach (var noChangeCOlorState in stateWithNoChangeTurn)
            {
                foreach (var nextStepvariant in GetNextStepVariants(noChangeCOlorState))
                {
                    yield return new NextStepVariant()
                    {
                        ResultState = nextStepvariant.ResultState,
                        FirstStepOfResultState = noChangeCOlorState
                    };
                }
            }

        }

        private List<string> GetAllowedNextStates(string inputState, int fromCoord, string figures, int boardWidth)
        {
            List<string> result = new List<string>();
            var allowedVectors = _validateFiguresService.GetAllowedMoveVectors(fromCoord, figures).Vectors;
            foreach (var allowedVector in allowedVectors)
            {
                var toCoord = allowedVector.ToCoord(fromCoord, boardWidth);
                var newState = _directMoveService.DirectMove(inputState, fromCoord, toCoord);
                if (inputState != newState)
                {
                    result.Add(newState);
                }
            }

            return result;
        }
    }
}
