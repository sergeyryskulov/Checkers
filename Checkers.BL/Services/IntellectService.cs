using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Checkers.BL.Constants;
using Checkers.BL.Helper;
using Checkers.BL.Models;
using Ckeckers.DAL.Repositories;

namespace Checkers.BL.Services
{
    public class IntellectService
    {

      

        private ValidateService _validateService;

        private IBoardRepository _boardRepository;
        ColorHelper _colorHelper;
        private MoveFigureService _moveFigureService;
        private VectorHelper _vectorHelper;
        private MathHelper _mathHelper;
        private StateParserHelper _stateParserHelper;


        public IntellectService(ValidateService validateService, IBoardRepository boardRepository, ColorHelper colorHelper, MoveFigureService moveFigureService, VectorHelper vectorHelper, MathHelper mathHelper, StateParserHelper stateParserHelper)
        {
            _validateService = validateService;
            _boardRepository = boardRepository;
            _colorHelper = colorHelper;
            _moveFigureService = moveFigureService;
            _vectorHelper = vectorHelper;
            _mathHelper = mathHelper;
            _stateParserHelper = stateParserHelper;
        }


        int GetBestForWhite(string state)
        {
            var nextWhiteVariants = GetNextStepVariants(state);

            if (nextWhiteVariants.Count == 0)
            {
                return -100;
            }

            var minimumWhite = nextWhiteVariants.OrderByDescending(t => GetWeight(t, FigureColor.White)).First();

            return GetWeight(minimumWhite, FigureColor.White);

        }
        public string IntellectStep(string registrationId, FigureColor intellectColor = FigureColor.Black)
        {
            string boardStateString = _boardRepository.Load(registrationId);

            var turn = _stateParserHelper.ParseState(boardStateString).Turn;


            string bestState= GetNextStepVariants(boardStateString).OrderBy(t => GetBestForWhite(t)).First();
            
            
            //string bestState = nextVariants.OrderByDescending(t => GetWeight(t, intellectColor)).First();

            _boardRepository.Save(registrationId, bestState);
            return bestState;
            /* var rand = new Random();
             var randomCoord= allVariants.Keys.ToList()[rand.Next(allVariants.Keys.Count)];
             var randomVectors= allVariants[randomCoord];
             var randomVector = randomVectors[rand.Next(randomVectors.Count)];
             var randomToCoord = _vectorHelper.VectorToCoord(randomCoord, randomVector, boardWidth);
             string resultState= _moveFigureService.Move(boardStateString, randomCoord, randomToCoord);
             _boardRepository.Save(registrationId, resultState);*/



        }
        private List<string> GetNextStepVariants(string inputState)
        {
            var result = new List<string>();

            var boardState = _stateParserHelper.ParseState(inputState);
            string figures = boardState.Figures;
            var boardWidth = _mathHelper.Sqrt(figures.Length);

            var stateWithNoChangeTurn = new List<string>();

            for (int fromCoord = 0; fromCoord < figures.Length; fromCoord++)
            {
                if (boardState.MustCoord != -1 && fromCoord != boardState.MustCoord)
                {
                    continue;
                }

                if ((boardState.Turn == Turn.Black && _colorHelper.GetFigureColor(figures[fromCoord]) == FigureColor.Black) ||
                    (boardState.Turn == Turn.White && _colorHelper.GetFigureColor(figures[fromCoord]) == FigureColor.White
                    ))
                {
                    var allowedVectors = _validateService.GetAllowedVectors(fromCoord, figures, out var isDie);
                    foreach (var allowedVector in allowedVectors)
                    {
                        var toCoord = _vectorHelper.VectorToCoord(fromCoord, allowedVector, boardWidth);
                        var newState = _moveFigureService.Move(inputState, fromCoord, toCoord, true);
                        if (newState != inputState)
                        {
                            if (boardState.Turn == Turn.Black && newState.Contains(Turn.Black) ||
                                boardState.Turn == Turn.White && newState.Contains(Turn.White))
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
            }

            foreach (var noChangeCOlorState in stateWithNoChangeTurn)
            {
                result.AddRange(GetNextStepVariants(noChangeCOlorState));
            }

            return result;

        }

        private int GetWeight(string boardState, FigureColor forWhom)
        {
            int result = 0;
            foreach (var figure in boardState)
            {
                switch (figure)
                {
                    case Figures.BlackQueen: result += 2; break;
                    case Figures.BlackPawn: result += 1; break;
                    case Figures.WhiteQueen: result -= 2; break;
                    case Figures.WhitePawn: result -= 1; break;
                    default: break;
                }
            }

            if (forWhom == FigureColor.White)
            {
                return -1*result;
            }

            return result;
        }
    }

    
}
