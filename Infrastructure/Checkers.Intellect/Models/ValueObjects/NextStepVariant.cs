using Checkers.DomainModels;

namespace Checkers.Intellect.Models.ValueObjects
{
    public class NextStepVariant
    {
        public NextStepVariant(GameState resultState, GameState firstStepOfResultState)
        {
            ResultState = resultState;
            FirstStepOfResultState = firstStepOfResultState;
        }

        public GameState ResultState { get; }

        public GameState FirstStepOfResultState { get; }
    }
}
