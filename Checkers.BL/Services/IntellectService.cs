using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public string IntellectStep(string registrationId)
        {
            string boardStateString = _boardRepository.Load(registrationId);
            var boardState= _stateParserHelper.ParseState(boardStateString);
            string figures = boardState.Figures;
            var boardWidth = _mathHelper.Sqrt(figures.Length);

            Dictionary<int, List<Vector>> allVariants = new Dictionary<int, List< Vector>>();
            for (int coord = 0; coord < figures.Length; coord++)
            {
                if (boardState.MustCoord != -1 && coord != boardState.MustCoord)
                {
                    continue;
                }

                if (_colorHelper.GetFigureColor(figures[coord]) == FigureColor.Black)
                {
                    var allowedVectors = _validateService.GetAllowedVectors(coord, figures, out var isDie);
                    if (allowedVectors.Count > 0)
                    {
                        allVariants.Add(coord, allowedVectors);
                    }
                }
            }

            var maxWeight = -100;
           
            string bestState = "";
            foreach (var fromCoord in allVariants.Keys)
            {
                foreach (var vector in allVariants[fromCoord])
                {
                    var toCoord = _vectorHelper.VectorToCoord(fromCoord, vector, boardWidth);
                    var newState= _moveFigureService.Move(boardStateString, fromCoord, toCoord, true);
                    var weight = GetWeight(newState);
                    if (weight > maxWeight && newState!=boardStateString)
                    {
                        maxWeight = weight;
                        bestState = newState;
                    }
                }
            }
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

        private int GetWeight(string boardState)
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

            return result;
        }
    }

    
}
