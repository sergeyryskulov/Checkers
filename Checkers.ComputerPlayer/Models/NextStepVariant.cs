using Checkers.DomainModels;
using Checkers.DomainModels.Models;

namespace Checkers.ComputerPlayer.Models
{
    internal record NextStepVariant(GameState ResultState, GameState FirstStepOfResultState);
}
