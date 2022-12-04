using System.Collections.Generic;
using Checkers.DomainModels;
using Checkers.DomainModels.Enums;
using Checkers.DomainServices;
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
            var cells = gameState.Board;
            var boardWidth = cells.BoardWidth;

            var stateWithNoChangeTurn = new List<GameState>();

            for (int fromCoord = 0; fromCoord < cells.CellsCount; fromCoord++)
            {
                if (gameState.MustGoFromPosition != null && fromCoord != gameState.MustGoFromPosition)
                {
                    continue;
                }

                if ((gameState.Turn == Turn.Black && cells.IsBlackFigureAt(fromCoord)) ||
                    (gameState.Turn == Turn.White && cells.IsWhiteFigureAt(fromCoord)
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
            var toCoordVariants = _validateRulesService.GetAllowedToPositions(board, fromCoord);
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
