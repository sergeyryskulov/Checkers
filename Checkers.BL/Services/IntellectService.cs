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
        private ValidateFiguresService _validateFiguresService;

        private IBoardRepository _boardRepository;

        private DirectMoveService _directMoveService;

        public IntellectService(ValidateFiguresService validateFiguresService, IBoardRepository boardRepository, DirectMoveService directMoveService)
        {
            _validateFiguresService = validateFiguresService;
            _boardRepository = boardRepository;
            _directMoveService = directMoveService;
        }

        public string IntellectStep(string registrationId)
        {
            string boardStateString = _boardRepository.Load(registrationId);

            var nextStepVariants = GetNextStepVariants(boardStateString);
            
            var worstForWhiteVariant = nextStepVariants.OrderBy(t => GetBestWeightForWhite(t)).First();
            
            _boardRepository.Save(registrationId, worstForWhiteVariant);

            return worstForWhiteVariant;
        }

        private List<string> GetNextStepVariants(string inputState)
        {
            var result = new List<string>();
            
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
                    foreach (var newState  in GetAllowedNextStates(inputState, fromCoord, figures, boardWidth))
                    {
                        if (newState.Contains(boardState.Turn))
                        {
                            stateWithNoChangeTurn.Add(newState);
                        }
                        else
                        {
                            result.Add(newState);
                        }
                    }
                }
            }

            foreach (var noChangeCOlorState in stateWithNoChangeTurn)
            {
                result.AddRange(GetNextStepVariants(noChangeCOlorState));
            }

            return result;

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

        int GetBestWeightForWhite(string state)
        {
            var nextWhiteVariants = GetNextStepVariants(state);

            if (nextWhiteVariants.Count == 0)
            {
                return -100;
            }

            var maximumWhite = nextWhiteVariants.OrderByDescending(t => GetWeightForWhite(t)).First();

            return GetWeightForWhite(maximumWhite);
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
