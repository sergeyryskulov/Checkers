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

        public IntellectService(ValidateService validateService, IBoardRepository boardRepository, ColorHelper colorHelper, MoveFigureService moveFigureService, VectorHelper vectorHelper, MathHelper mathHelper)
        {
            _validateService = validateService;
            _boardRepository = boardRepository;
            _colorHelper = colorHelper;
            _moveFigureService = moveFigureService;
            _vectorHelper = vectorHelper;
            _mathHelper = mathHelper;
        }

        public string IntellectStep(string registrationId)
        {
            string boardState = _boardRepository.Load(registrationId);

            char turn = boardState[boardState.Length - 1];
            string figures = boardState.Substring(0, boardState.Length - 1);
            var mustCoord = -1;

            if (turn != Turn.White && turn != Turn.Black && turn != Turn.WhiteWin && turn != Turn.BlackWin)
            {
                figures = boardState.Split(new char[]
                {
                    Turn.White, Turn.Black, Turn.WhiteWin, Turn.BlackWin
                }, StringSplitOptions.None)[0];

                var mustCoordString = boardState.Split(new char[]
                {
                    Turn.White, Turn.Black, Turn.WhiteWin, Turn.BlackWin
                }, StringSplitOptions.None)[1];

                mustCoord = int.Parse(mustCoordString);
                

                turn = boardState[figures.Length];
            }

            Dictionary<int, List<Vector>> allVariants = new Dictionary<int, List< Vector>>();
            for (int coord = 0; coord < figures.Length; coord++)
            {
                if (mustCoord != -1 && coord != mustCoord)
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

            var rand = new Random();
            var randomCoord= allVariants.Keys.ToList()[rand.Next(allVariants.Keys.Count)];
            var randomVectors= allVariants[randomCoord];
            var randomVector = randomVectors[rand.Next(randomVectors.Count)];
            var boardWidth  = _mathHelper.Sqrt(figures.Length);
            var randomToCoord = _vectorHelper.VectorToCoord(randomCoord, randomVector, boardWidth);
            return _moveFigureService.Move(randomCoord, randomToCoord, registrationId);
        }
    }
}
