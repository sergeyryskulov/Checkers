using Checkers.Core.Constants;
using Checkers.DomainModels.Aggregates;

namespace Checkers.Core.Models.ValueObjects
{
    public class GameState
    {        
        public Board Cells { get;  }

        public Turn Turn { get; }

        public int? MustGoFrom { get; }

        public GameState(string cells, Turn turn, int? mustGoFrom)
        {
            Cells = new Board(cells);
            Turn = turn;
            MustGoFrom = mustGoFrom;

        }
        public GameState(string cells, Turn turn)
        {
            Cells = new Board(cells);
            Turn = turn;
            MustGoFrom = null;                                
        }

        public override bool Equals(object obj)
        {
            var other = (GameState)obj;

            return Cells.ToString() == other.Cells.ToString() && Turn == other.Turn && MustGoFrom == other.MustGoFrom;
        }

        public override int GetHashCode()
        {
            return Cells.ToString().GetHashCode() + (int)Turn + (MustGoFrom??0);
        }
    }
}
