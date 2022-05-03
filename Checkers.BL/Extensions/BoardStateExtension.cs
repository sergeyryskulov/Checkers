using Checkers.BL.Constants;
using Checkers.BL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.BL.Extensions
{
    public static class BoardStateExtension
    {
        public static BoardState ToBoardState(this string boardState)
        {
            char turn = boardState[boardState.Length - 1];
            string figures = boardState.Substring(0, boardState.Length - 1);
            int mustCoord = -1;
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

            return new BoardState()
            {
                Figures = figures,
                MustCoord = mustCoord,
                Turn = turn
            };
        }
    }
}
