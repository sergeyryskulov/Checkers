using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.HumanPlayer.UseCases;
using System;
using System.Collections.Generic;
using System.Text;
using Checkers.Contracts.Rules;
using Checkers.DomainModels.Enums;
using Checkers.DomainModels;
using Checkers.DomainModels.Models;
using Checkers.HumanPlayer.Interfaces;
using Moq;

namespace Checkers.HumanPlayer.UseCases.Tests
{
    [TestClass()]
    public class HumanTryMoveFigureUseCaseTests
    {
        private Mock<IValidateHumanService> _validateHumanService;
        private Mock<IMoveRule> _moveRule;

        [TestInitialize]
        public void InitMocks()
        {
            _validateHumanService = new Mock<IValidateHumanService>();
            _moveRule = new Mock<IMoveRule>();
        }

        private HumanTryMoveFigureUseCase CreateHumanTryMoveFigureUseCase()
        {
            return new HumanTryMoveFigureUseCase(
                _validateHumanService.Object,
                _moveRule.Object
            );
        }


        [TestMethod()]
        public void ExecuteTest()
        {
           _validateHumanService.Setup(m => m.CanMove(
                It.Is<GameState>(t => t.Board.ToString() == "11P1" && t.Turn == Turn.White && t.MustGoFromPosition == null), 3, 0)).Returns(true);

           _moveRule.Setup(m => m.MoveFigureWithoutValidation(
                It.Is<GameState>(t => t.Board.ToString() == "11P1" && t.Turn == Turn.White && t.MustGoFromPosition == null), 3, 0)).Returns(
                new GameState("1Q11", Turn.WhiteWin));

            var humanTryMoveFigureUseCase = CreateHumanTryMoveFigureUseCase();

            var actual = humanTryMoveFigureUseCase.Execute(new GameState("11P1", Turn.White), 3, 0);

            Assert.AreEqual("1Q11", actual.Board.ToString());
            Assert.AreEqual(Turn.WhiteWin, actual.Turn);
        }
    }
}