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


        public IntellectService(IBoardRepository boardRepository, StepIteratorService stepIteratorService)
        {
            _boardRepository = boardRepository;
            _stepIteratorService = stepIteratorService;
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

            if (nextWhiteVariants.Count == 0)
            {
                return -100;
            }

            var maximumWhite = nextWhiteVariants.OrderByDescending(t => GetWeightForWhite(t.ResultState)).First();

            return GetWeightForWhite(maximumWhite.ResultState);
        }

        private int GetWeightForWhite(string boardState)
        {
            int result = 0;
            foreach (var figure in boardState)
            {
                switch (figure)
                {
                    case Figures.BlackQueen: result -= 2; break;
                    case Figures.BlackPawn: result -= 1; break;
                    case Figures.WhiteQueen: result += 2; break;
                    case Figures.WhitePawn: result += 1; break;
                    default: break;
                }
            }

            return result;
        }
    }
}
