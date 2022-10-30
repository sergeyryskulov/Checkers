using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.BL.Models
{
    public class BoardState
    {        
        public string Cells { get;  }

        public char Turn { get; }

        public int? MustGoFrom { get; }

        public BoardState(string cells, char turn, int? mustGoFrom)
        {
            Cells = cells;
            Turn = turn;
            MustGoFrom = mustGoFrom;

        }
        public BoardState(string cells, char turn)
        {
            Cells = cells;
            Turn = turn;
            MustGoFrom = null;                                
        }

        public override bool Equals(object obj)
        {
            var other = (BoardState)obj;

            return Cells == other.Cells && Turn == other.Turn && MustGoFrom == other.MustGoFrom;
        }

        public override int GetHashCode()
        {
            return Cells.GetHashCode() + (int)Turn + (MustGoFrom??0);
        }
    }
}
