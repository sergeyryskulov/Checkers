using Checkers.DomainModels;
using Checkers.DomainModels.Models;

namespace Checkers.ComputerPlayer.Models
{
    internal class NextStepVariant
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
