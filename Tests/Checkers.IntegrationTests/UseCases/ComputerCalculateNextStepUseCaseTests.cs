using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.ComputerPlayer.UseCases;
using System;
using System.Collections.Generic;
using System.Text;
using Checkers.ComputerPlayer.Services;
using Checkers.DomainModels.Enums;
using Checkers.DomainModels;
using Checkers.Rules.Interfaces;
using Checkers.Rules.Rules;
using Checkers.Rules.Services;

namespace Checkers.ComputerPlayer.UseCases.Tests
{
    [TestClass()]
    public class ComputerCalculateNextStepUseCaseTests
    {
        [TestMethod()]
        public void PawnToQueen()
        {
            AssertNextStep(
                new GameState(
                    "p1" +
                    "11",
                    Turn.Black
                ),
                new GameState(

                    "11" +
                    "1q",
                    Turn.BlackWin
                ));
        }


        [TestMethod()]
        public void QueenEatPawn()
        {
            AssertNextStep(
                new GameState(

                    "1111111p" +
                    "111111p1" +
                    "1p111p1p" +
                    "p11111p1" +
                    "1p11111P" +
                    "11111111" +
                    "111P1111" +
                    "11q11111",
                     Turn.Black),
                new GameState(
                    "1111111p" +
                    "111111p1" +
                    "1p111p1p" +
                    "p11111p1" +
                    "1p11111P" +
                    "1111q111" +
                    "11111111" +
                    "11111111",
                Turn.White));
        }

        [TestMethod()]
        public void CanDoNextStepAfterEat()
        {

            AssertNextStep(
                new GameState(
                "111111" +
                "111111" +
                "111111" +
                "111111" +
                "1p1p11" +
                "P1P111",
                Turn.White),
                new GameState(
                "111111" +
                "111111" +
                "111111" +
                "11P111" +
                "111p11" +
                "11P111",
                Turn.White,
                20));

        }

        [TestMethod()]
        public void IntellectStepMustGoTest()
        {
            AssertNotNextStep(
                new GameState(
                "1p1p1p1p" +
                "p111p111" +
                "1111111p" +
                "1111p111" +
                "11111111" +
                "P11111P1" +
                "1P1P1p11" +
                "P1P1P1P1",
                Turn.Black,
                53),
                new GameState(

                "1p1p1p1p" +
                "p111p111" +
                "1111111p" +
                "1111p111" +
                "11111111" +
                "P11111P1" +
                "1P1P1p11" +
                "P1P1P1P1",
                Turn.Black,
                53));

        }

        private ComputerCalculateNextStepUseCase CreateComputerCalculateNextStepUseCase()
        {
            var valideteFigureService = new ValidateService(
                new ValidatePawnService(),
                new ValidateQueenService()
            );

            var directMoveService = new MoveRule(
                new ValidateService(
                    new ValidatePawnService(),
                    new ValidateQueenService()
                    ) as IValidateEatService
            );

            var stepIteratorService = new StepIteratorService(
                    new ValidateRule(valideteFigureService),
                    directMoveService
                );

            return new ComputerCalculateNextStepUseCase(
                stepIteratorService,
                new PositionWeightService());
        }

        private void AssertNextStep(GameState from, GameState to)
        {
            var service = CreateComputerCalculateNextStepUseCase();

            var actual = service.CalculateNextStep(from);

            var expected = to;

            Assert.AreEqual(expected.Board.ToString(), actual.Board.ToString());
            Assert.AreEqual(expected.Turn, actual.Turn);
            Assert.AreEqual(expected.MustGoFromPosition, actual.MustGoFromPosition);
        }

        private void AssertNotNextStep(GameState from, GameState to)
        {
            var service = CreateComputerCalculateNextStepUseCase();

            var actual = service.CalculateNextStep(from);

            var expected = to;

            Assert.IsTrue(!actual.Equals(expected)
            );
        }
    }
}