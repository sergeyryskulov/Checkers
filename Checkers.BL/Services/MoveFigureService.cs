﻿using System;
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
    public class MoveFigureService
    {
        private IBoardRepository _boardRepository;
        private VectorHelper _vectorHelper;
        private MathHelper _mathHelper;
        private PawnService _pawnService;

        public MoveFigureService(IBoardRepository boardRepository, VectorHelper vectorHelper, MathHelper mathHelper, PawnService pawnService)
        {
            _boardRepository = boardRepository;
            _vectorHelper = vectorHelper;
            _mathHelper = mathHelper;
            _pawnService = pawnService;

        }


        public string Move(int fromCoord, int toCoord, string registrationId)
        {
            string figures = _boardRepository.Load(registrationId);
            var boardWidth = _mathHelper.Sqrt(figures.Length);

            var vector = _vectorHelper.CoordToVector(fromCoord, toCoord, boardWidth);
            if (vector == null)
            {
                return figures;
            }


            if (figures[fromCoord] == Figures.WhitePawn || figures[fromCoord] == Figures.BlackPawn)
            {
                if (!_pawnService.GetAllowedVectors(fromCoord, figures).Contains(vector))
                {
                    return figures;
                }
            }

            StringBuilder newFiguresBuilder = new StringBuilder(figures);
            newFiguresBuilder[toCoord] = newFiguresBuilder[fromCoord];
            newFiguresBuilder[fromCoord] = Figures.Empty;

            for (int i = 1; i < vector.Length; i++)
            {
                var dieCoord= _vectorHelper.VectorToCoord(fromCoord, new Vector()
                {
                    Length = i,
                    Direction = vector.Direction
                }, boardWidth);

                newFiguresBuilder[dieCoord] = Figures.Empty;
            }
          

            var result = newFiguresBuilder.ToString();
            _boardRepository.Save(registrationId, result);
            return result;
        }

      
    }
}
