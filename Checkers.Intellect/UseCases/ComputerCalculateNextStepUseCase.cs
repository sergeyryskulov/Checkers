using System.Linq;
using Checkers.ComputerPlayer.Interfaces;
using Checkers.Contracts.UseCases;
using Checkers.DomainModels;

namespace Checkers.ComputerPlayer.UseCases
{
    public class ComputerCalculateNextStepUseCase : IComputerCalculateNextStepUseCase
    {
        private IStepIteratorService _stepIteratorService;

        private IPositionWeightService _positionWeightService;

        public ComputerCalculateNextStepUseCase(IStepIteratorService stepIteratorService, IPositionWeightService positionWeightService)
        {
            _stepIteratorService = stepIteratorService;
            _positionWeightService = positionWeightService;
        }

        public GameState Execute(GameState gameState)
        {
            var nextStepVariants = _stepIteratorService.GetNextStepVariants(gameState);

            var worstForHumanVariant = nextStepVariants.OrderBy(t => GetBestWeightForHuman(t.ResultState)).First();

            var resultStep = worstForHumanVariant.FirstStepOfResultState;

            return resultStep;
        }

        int GetBestWeightForHuman(GameState state)
        {
            var humanStepVariants = _stepIteratorService.GetNextStepVariants(state);

            var bestForHumanVariant = humanStepVariants.OrderByDescending(t => _positionWeightService.GetWeightForWhite(t.ResultState.Board)).FirstOrDefault();
            if (bestForHumanVariant == null)
            {
                return -100;
            }

            return _positionWeightService.GetWeightForWhite(bestForHumanVariant.ResultState.Board);
        }
    }
}
