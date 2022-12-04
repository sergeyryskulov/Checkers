using System.Linq;
using Checkers.ComputerPlayer.Interfaces;
using Checkers.DomainModels;
using Checkers.DomainServices;

namespace Checkers.ComputerPlayer.Services
{
    public class ComputerPlayerService : IComputerPlayerService
    {        
        private IStepIteratorService _stepIteratorService;

        private IPositionWeightService _positionWeightService;

        public ComputerPlayerService(IStepIteratorService stepIteratorService, IPositionWeightService positionWeightService)
        {            
            _stepIteratorService = stepIteratorService;
            _positionWeightService = positionWeightService;
        }

        public GameState CalculateNextStep(GameState gameState)
        {            
            var nextStepVariants = _stepIteratorService.GetNextStepVariants(gameState);
            
            var worstForWhiteVariant = nextStepVariants.OrderBy(t => GetBestWeightForWhite(t.ResultState)).First();

            var resultStep = worstForWhiteVariant.FirstStepOfResultState;           

            return resultStep;
        }

        int GetBestWeightForWhite(GameState state)
        {
            var nextWhiteVariants = _stepIteratorService.GetNextStepVariants(state);

            var maximumWhite  = nextWhiteVariants.OrderByDescending(t => _positionWeightService.GetWeightForWhite(t.ResultState.Board)).FirstOrDefault();
            if (maximumWhite == null)
            {
                return -100;
            }

            return _positionWeightService.GetWeightForWhite(maximumWhite.ResultState.Board);
        }
    }
}
