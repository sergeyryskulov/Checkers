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

namespace Checkers.BL.Services
{
    public class IntellectService : IIntellectService
    {        
        private IStepIteratorService _stepIteratorService;

        private IPositionWeightService _positionWeightService;

        public IntellectService(IStepIteratorService stepIteratorService, IPositionWeightService positionWeightService)
        {            
            _stepIteratorService = stepIteratorService;
            _positionWeightService = positionWeightService;
        }

        public string IntellectStep(string boardState)
        {            
            var nextStepVariants = _stepIteratorService.GetNextStepVariants(boardState);
            
            var worstForWhiteVariant = nextStepVariants.OrderBy(t => GetBestWeightForWhite(t.ResultState)).First();

            var resultStep = worstForWhiteVariant.FirstStepOfResultState;           

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
