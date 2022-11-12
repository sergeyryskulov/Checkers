using Checkers.Core.Constants;

namespace Checkers.Core.Models.ValueObjects
{
    public class BoardState
    {        
        public string Cells { get;  }

        public Turn Turn { get; }

        public int? MustGoFrom { get; }

        public BoardState(string cells, Turn turn, int? mustGoFrom)
        {
            Cells = cells;
            Turn = turn;
            MustGoFrom = mustGoFrom;

        }
        public BoardState(string cells, Turn turn)
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
