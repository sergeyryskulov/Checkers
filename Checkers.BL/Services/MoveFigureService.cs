﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        private VectorHelper _vectorHelper;
        private MathHelper _mathHelper;
        private ValidateService _validateService;
        private ColorHelper _colorHelper;


        public MoveFigureService( VectorHelper vectorHelper, MathHelper mathHelper, ValidateService validateService, ColorHelper colorHelper)
        {
            _vectorHelper = vectorHelper;
            _mathHelper = mathHelper;
            _validateService = validateService;
            _colorHelper = colorHelper;
        }

        public string Move(string boardState, int fromCoord, int toCoord, string regId="")
        {
            
            char turn = boardState[boardState.Length - 1];
            string figures = boardState.Substring(0, boardState.Length - 1);
            if (turn != Turn.White && turn != Turn.Black && turn!=Turn.WhiteWin && turn!= Turn.BlackWin)
            {
                figures = boardState.Split(new char[]
                {
                    Turn.White, Turn.Black, Turn.WhiteWin, Turn.BlackWin
                }, StringSplitOptions.None)[0];

                var mustCoordString = boardState.Split(new char[]
                {
                    Turn.White, Turn.Black, Turn.WhiteWin, Turn.BlackWin
                }, StringSplitOptions.None)[1];

                if (fromCoord != int.Parse(mustCoordString))
                {
                    return boardState;
                }

                turn = boardState[figures.Length];
            }
            
            var boardWidth = _mathHelper.Sqrt(figures.Length);

            var vector = _vectorHelper.CoordToVector(fromCoord, toCoord, boardWidth);
            if (vector == null)
            {
                return boardState;
            }

            if (_colorHelper.GetFigureColor(figures[fromCoord]) == FigureColor.White && turn != Turn.White ||
                _colorHelper.GetFigureColor(figures[fromCoord]) == FigureColor.Black && turn != Turn.Black)
            {
                return boardState;
            }

            bool isDie = false;
            if (figures[fromCoord] == Figures.WhitePawn || figures[fromCoord] == Figures.BlackPawn ||
                figures[fromCoord] == Figures.WhiteQueen || figures[fromCoord] == Figures.BlackQueen)
            {
                if (!_validateService.GetAllowedVectors(fromCoord, figures, out isDie).Contains(vector))
                {
                    return boardState;
                }
            }

            StringBuilder newFiguresBuilder = new StringBuilder(figures);
            newFiguresBuilder[toCoord] = newFiguresBuilder[fromCoord];
            if (toCoord < boardWidth && turn==Turn.White)
            {
                newFiguresBuilder[toCoord] = Figures.WhiteQueen;
            }
            if (toCoord >= boardWidth * (boardWidth - 1) && turn == Turn.Black)
            {
                newFiguresBuilder[toCoord] = Figures.BlackQueen; 
            }
            newFiguresBuilder[fromCoord] = Figures.Empty;

            for (int i = 1; i < vector.Length; i++)
            {
                var cleanCoord = _vectorHelper.VectorToCoord(fromCoord, new Vector()
                {
                    Length = i,
                    Direction = vector.Direction
                }, boardWidth);

                newFiguresBuilder[cleanCoord] = Figures.Empty;
            }

            var newFigures = newFiguresBuilder.ToString();
            var toggleTurn = true;

        
            if (isDie)
            {
                _validateService.GetAllowedVectors(toCoord, newFigures, out var isDie2);
                {
                    if (isDie2)
                    {
                        toggleTurn = false;
                    }
                }
            }

            var nextTurn = turn;
            if (toggleTurn)
            {
                nextTurn = (turn == Turn.White ? Turn.Black : Turn.White);
            }

            if (turn == Turn.White && !newFigures.Contains(Figures.BlackPawn) && !newFigures.Contains(Figures.BlackQueen))
            {
                nextTurn = Turn.WhiteWin;
            }
            if (turn == Turn.Black && !newFigures.Contains(Figures.WhitePawn) && !newFigures.Contains(Figures.WhiteQueen))
            {
                nextTurn = Turn.BlackWin;
            }


            var resultState = newFigures + nextTurn + (toggleTurn?"" : toCoord);
            
            return resultState;
        }

      
    }
}
