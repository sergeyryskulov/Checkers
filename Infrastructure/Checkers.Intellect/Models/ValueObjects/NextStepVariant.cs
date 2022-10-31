using Checkers.Core.Models.ValueObjects;

namespace Checkers.Intellect.Models.ValueObjects
{
    public class NextStepVariant
    {
        public NextStepVariant(BoardState resultState, BoardState firstStepOfResultState)
        {
            ResultState = resultState;
            FirstStepOfResultState = firstStepOfResultState;
        }

        public BoardState ResultState { get; }

        public BoardState FirstStepOfResultState { get; }
    }
}
