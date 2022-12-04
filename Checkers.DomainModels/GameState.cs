using Checkers.DomainModels.Enums;

namespace Checkers.DomainModels
{
    public class GameState
    {        
        public Board Board { get;  }

        public Turn Turn { get; }

        public int? MustGoFromPosition { get; }

        public GameState(string cells, Turn turn, int? mustGoFromPosition)
        {
            Board = new Board(cells);
            Turn = turn;
            MustGoFromPosition = mustGoFromPosition;

        }
        public GameState(string cells, Turn turn)
        {
            Board = new Board(cells);
            Turn = turn;
            MustGoFromPosition = null;                                
        }

        public override bool Equals(object obj)
        {
            var other = (GameState)obj;

            return Board.ToString() == other.Board.ToString() && Turn == other.Turn && MustGoFromPosition == other.MustGoFromPosition;
        }

        public override int GetHashCode()
        {
            return Board.ToString().GetHashCode() + (int)Turn + (MustGoFromPosition??0);
        }
    }
}
