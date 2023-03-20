using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.ComputerPlayer.UseCases;
using System;
using System.Collections.Generic;
using System.Text;
using Checkers.ComputerPlayer.Services;
using Checkers.DomainModels.Enums;
using Checkers.DomainModels;
using Checkers.DomainModels.Models;
using Checkers.Rules.Interfaces;
using Checkers.Rules.Rules;
using Checkers.Rules.Services;

namespace Checkers.ComputerPlayer.UseCases.Tests
{
    [TestClass()]
    public class ComputerCalculateNextStepUseCaseTests
    {

        internal class AssertCalculateModel
        {
            internal GameState FromState { get; set; }

            internal GameState ResultState { get; set;}
        }

        [TestMethod()]
        public void PawnGrowsToQueen()
        {
            AssertNextStepCalculated(
                new AssertCalculateModel
                {
                    FromState = new GameState(
                        "p1" +
                        "11",
                        Turn.Black
                    ),
                    ResultState = new GameState(
                        "11" +
                        "1q",
                        Turn.BlackWin
                    )
                });
        }


        [TestMethod()]
        public void QueenEatPawn()
        {
            AssertNextStepCalculated(
                new AssertCalculateModel()
                {
                    FromState = new GameState(
                        "1111111p" +
                        "111111p1" +
                        "1p111p1p" +
                        "p11111p1" +
                        "1p11111P" +
                        "11111111" +
                        "111P1111" +
                        "11q11111",
                        Turn.Black),
                    ResultState = new GameState(
                        "1111111p" +
                        "111111p1" +
                        "1p111p1p" +
                        "p11111p1" +
                        "1p11111P" +
                        "1111q111" +
                        "11111111" +
                        "11111111",
                        Turn.White)
                });
        }
        
        [TestMethod()]
        public void PawnEatPawnAndCanDoNextStepAfterThat()
        {
            AssertNextStepCalculated(
                new AssertCalculateModel()
                {
                    FromState = new GameState(
                        "1p1111" +
                        "11P111" +
                        "111111" +
                        "1111Q1" +
                        "111111" +
                        "111111",
                        Turn.Black),
                    ResultState = new GameState(
                        "111111" +
                        "111111" +
                        "111p11" +
                        "1111Q1" +
                        "111111" +
                        "111111",
                        Turn.Black,
                        15)
                });

        }

        [TestMethod()]
        public void EatQueenBetterThatEatPawn()
        {
            AssertNextStepCalculated(
                new AssertCalculateModel()
                {
                    FromState = new GameState(
                        "111111" +
                        "11p111" +
                        "1P1Q11" +
                        "111111" +
                        "111111" +
                        "P11111",
                        Turn.Black),
                    ResultState = new GameState(
                        "111111" +
                        "111111" +
                        "1P1111" +
                        "1111p1" +
                        "111111" +
                        "P11111",
                        Turn.White)
                });
        }

        private ComputerCalculateNextStepUseCase CreateComputerCalculateNextStepUseCase()
        {
            var validateRule = new ValidateRule(new ValidateFigureService(
                new ValidatePawnService(),
                new ValidateQueenService()
            ));

            var moveRule = new MoveRule(
                new ValidateFigureService(
                    new ValidatePawnService(),
                    new ValidateQueenService()
                    ) as IValidateEatService
            );

            var stepIteratorService = new StepIteratorService(
                    validateRule,
                    moveRule
                );

            return new ComputerCalculateNextStepUseCase(
                stepIteratorService,
                new PositionWeightService());
        }

        private void AssertNextStepCalculated(AssertCalculateModel assertCalculate)
        {
            AssertNextStepCalculated(assertCalculate.FromState, assertCalculate.ResultState);
        }

        private void AssertNextStepCalculated(GameState from, GameState to)
        {
            var service = CreateComputerCalculateNextStepUseCase();

            var actual = service.Execute(from);

            var expected = to;

            Assert.AreEqual(expected.Board.ToString(), actual.Board.ToString());
            Assert.AreEqual(expected.Turn, actual.Turn);
            Assert.AreEqual(expected.MustGoFromPosition, actual.MustGoFromPosition);
        }
    }
}