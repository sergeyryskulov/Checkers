using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.BL.Constants;

namespace Checkers.BL.Helper
{

    public class BoardState
    {
        public char Turn;

        public string Figures;

        public int MustCoord;
    }

    public class StateParserHelper
    {
        public BoardState ParseState(string boardState)
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

                mustCoord=  int.Parse(mustCoordString);
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
