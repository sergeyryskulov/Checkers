using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Checkers.BL.Constants;
using Checkers.BL.Extensions;
using Checkers.BL.Models;
using Ckeckers.DAL.Repositories;

namespace Checkers.BL.Services
{
    public class IntellectService : IIntellectService
    {
        private IBoardRepository _boardRepository;

        private StepIteratorService _stepIteratorService;

        private PositionWeightService _positionWeightService;

        public IntellectService(IBoardRepository boardRepository, StepIteratorService stepIteratorService, PositionWeightService positionWeightService)
        {
            _boardRepository = boardRepository;
            _stepIteratorService = stepIteratorService;
            _positionWeightService = positionWeightService;
        }

        public string IntellectStep(string registrationId)
        {
            string boardStateString = _boardRepository.Load(registrationId);

            var nextStepVariants = _stepIteratorService.GetNextStepVariants(boardStateString);
            
            var worstForWhiteVariant = nextStepVariants.OrderBy(t => GetBestWeightForWhite(t.ResultState)).First();

            var resultStep = worstForWhiteVariant.FirstStepOfResultState;

            _boardRepository.Save(registrationId, resultStep);

            return resultStep;
        }

        int GetBestWeightForWhite(string state)
        {
            var nextWhiteVariants = _stepIteratorService.GetNextStepVariants(state);

            var maximumWhite  = nextWhiteVariants.OrderByDescending(t => _positionWeightService.GetWeightForWhite(t.ResultState)).FirstOrDefault();
            if (maximumWhite == null)
            {
                return -100;
            }

            return _positionWeightService.GetWeightForWhite(maximumWhite.ResultState);
        }
    }
}
