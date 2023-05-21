using Checkers.Contracts.Rules;
using Checkers.Contracts.UseCases;
using Checkers.DomainModels;
using Checkers.DomainModels.Models;
using Checkers.HumanPlayer.Interfaces;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Checkers.UnitTests")]
[assembly: InternalsVisibleTo("Checkers.FunctionalTests")]
namespace Checkers.HumanPlayer.UseCases
{
    internal class HumanTryMoveFigureUseCase : IHumanTryMoveFigureUseCase
    {
        private IValidateHumanService _validateHumanService;
        private IMoveRule _moveRule;

        public HumanTryMoveFigureUseCase(IValidateHumanService validateHumanService, IMoveRule moveRule)
        {
            _validateHumanService = validateHumanService;
            _moveRule = moveRule;
        }

        public GameState Execute(GameState gameState, int fromPosition, int toPosition)
        {
            if (!_validateHumanService.CanMove(gameState, fromPosition, toPosition))
            {
                return gameState;
            }

            var newState = _moveRule.MoveFigureWithoutValidation(gameState, fromPosition, toPosition);

            return newState;
        }
    }
}
