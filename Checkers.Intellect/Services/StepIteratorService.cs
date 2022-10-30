using System;
using System.Collections.Generic;
using System.Text;
using Checkers.BL.Constants;
using Checkers.BL.Extensions;
using Checkers.BL.Models;

namespace Checkers.BL.Services
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


        public IEnumerable<NextStepVariant> GetNextStepVariants(BoardState boardState)
        {            
            string figures = boardState.Cells;
            var boardWidth = figures.Length.SquareRoot();

            var stateWithNoChangeTurn = new List<BoardState>();

            for (int fromCoord = 0; fromCoord < figures.Length; fromCoord++)
            {
                if (boardState.MustGoFrom != null && fromCoord != boardState.MustGoFrom)
                {
                    continue;
                }

                if ((boardState.Turn == Turn.Black && figures[fromCoord].ToFigureColor() == FigureColor.Black) ||
                    (boardState.Turn == Turn.White && figures[fromCoord].ToFigureColor() == FigureColor.White
                    ))
                {
                    foreach (var newState in GetAllowedNextStates(boardState, fromCoord, figures, boardWidth))
                    {
                        if (newState.Turn==boardState.Turn)
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

        private List<BoardState> GetAllowedNextStates(BoardState inputState, int fromCoord, string figures, int boardWidth)
        {
            List<BoardState> result = new List<BoardState>();
            var allowedVectors = _validateFiguresService.GetAllowedMoveVectors(fromCoord, figures).Vectors;
            foreach (var allowedVector in allowedVectors)
            {
                var toCoord = allowedVector.ToCoord(fromCoord, boardWidth);
                var newState = _directMoveService.DirectMove(inputState, fromCoord, toCoord);
                if (!inputState.Equals(newState))
                {
                    result.Add(newState);
                }
            }

            return result;
        }
    }
}
