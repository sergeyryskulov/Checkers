using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.BL.Models
{
    public class BoardState
    {        
        public string Cells { get; set; }

        public char Turn { get; set; }

        public int? MustGoFrom { get; set; }

        public BoardState()
        {
            ;
        }

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
          //  if (obj == null || !(obj is BoardState))
            //    return false;

            var other = (BoardState)obj;

            return Cells == other.Cells && Turn == other.Turn && MustGoFrom == other.MustGoFrom;
        }

        //public override int GetHashCode()
        //{
          //  return (Cells + Turn + MustGoFrom).GetHashCode();
        //}

    }
}
