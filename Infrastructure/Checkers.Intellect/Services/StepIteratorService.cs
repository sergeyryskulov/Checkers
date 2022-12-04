using System.Collections.Generic;
using Checkers.ComputerPlayer.Interfaces;
using Checkers.ComputerPlayer.Models;
using Checkers.DomainModels;
using Checkers.DomainModels.Enums;
using Checkers.DomainServices;

namespace Checkers.ComputerPlayer.Services
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

        private List<GameState> GetAllowedNextStates(GameState inputState, int fromPosition, Board board)
        {
            List<GameState> result = new List<GameState>();
            var toCoordVariants = _validateRulesService.GetAllowedToPositions(board, fromPosition);
            foreach (var toCoord in toCoordVariants)
            {                
                var newState = _moveRulesService.MoveFigureWithoutValidation(inputState, fromPosition, toCoord);
                if (!inputState.Equals(newState))
                {
                    result.Add(newState);
                }
            }

            return result;
        }
    }
}
