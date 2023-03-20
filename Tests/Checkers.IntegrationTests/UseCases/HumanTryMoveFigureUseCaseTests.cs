using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.HumanPlayer.UseCases;
using System;
using System.Collections.Generic;
using System.Text;
using Checkers.DomainModels.Enums;
using Checkers.DomainModels;
using Checkers.DomainModels.Models;
using Checkers.HumanPlayer.Services;
using Checkers.Rules.Rules;
using Checkers.Rules.Services;

namespace Checkers.HumanPlayer.UseCases.Tests
{
    [TestClass()]
    public class HumanTryMoveFigureUseCaseTests
    {
        internal class AssertMoveModel
        {
            internal GameState FromBoard { get; set; }

            internal int FromCoord { get; set; }

            internal int ToCoord { get; set; }

            internal GameState ToState { get; set; }
        }

        [TestMethod()]
        public void PawnCanMoveToQueen()
        {
            AssertMove(
                new AssertMoveModel()
                {
                    FromBoard = new GameState(
                        "p1" +
                        "11",
                        Turn.Black),
                    FromCoord = 0,
                    ToCoord = 3,
                    ToState = new GameState(
                        "11" +
                        "1q",
                        Turn.BlackWin)
                });
        }

        [TestMethod()]
        public void PawnCannotMoveToSelf()
        {
            AssertMove(
                new AssertMoveModel()
                {
                    FromBoard = new GameState(
                        "p1" +
                        "11",
                        Turn.Black),
                    FromCoord = 0,
                    ToCoord = 0,
                    ToState = new GameState(
                        "p1" +
                        "11",
                        Turn.Black)
                });
        }

        [TestMethod()]
        public void QueenCannotMoveToSelf()
        {
            AssertMove(
                new AssertMoveModel()
                {
                    FromBoard = new GameState(
                        "q1" +
                        "11",
                        Turn.Black),
                    FromCoord = 0,
                    ToCoord = 0,
                    ToState = new GameState(
                        "q1" +
                        "11",
                        Turn.Black)
                });
        }


        [TestMethod()]
        public void PawnEatPawn()
        {
            AssertMove(
                new AssertMoveModel()
                {
                    FromBoard = new GameState(
                        "111" +
                        "1p1" +
                        "P11",
                        Turn.White),
                    FromCoord = 6,
                    ToCoord = 2,
                    ToState = new GameState(
                        "11Q" +
                        "111" +
                        "111",
                        Turn.WhiteWin)
                });
        }


        [TestMethod()]
        public void TurnNotToggledAfterPawnEatFigure()
        {
            AssertMove(
                new AssertMoveModel()
                {
                    FromBoard = new GameState(
                        "111111" +
                        "111111" +
                        "P11p11" +
                        "111111" +
                        "1p1111" +
                        "P11111",
                        Turn.White),
                    FromCoord = 30,
                    ToCoord = 20,
                    ToState = new GameState(
                        "" +
                        "111111" +
                        "111111" +
                        "P11p11" +
                        "11P111" +
                        "111111" +
                        "111111",
                        Turn.White,
                        20)
                });
        }

        [TestMethod()]
        public void TurnNotToggleAfterQueenEatFigure()
        {
            AssertMove(
                new AssertMoveModel()
                {
                    FromBoard = new GameState(
                        "111Q11" +
                        "111111" +
                        "1p1111" +
                        "111111" +
                        "1p1111" +
                        "111111",
                        Turn.White),
                    FromCoord = 3,
                    ToCoord = 18,
                    ToState = new GameState(
                        "111111" +
                        "111111" +
                        "111111" +
                        "Q11111" +
                        "1p1111" +
                        "111111",
                        Turn.White,
                        18)
                });
        }

        [TestMethod()]
        public void BlockedByOtherFigureThatMustMove()
        {
            AssertMove(
                new AssertMoveModel()
                {
                    FromBoard = new GameState(
                        "111111" +
                        "1p1111" +
                        "P11111" +
                        "11P111" +
                        "111111" +
                        "111111",
                        Turn.White,
                        20),
                    FromCoord = 12,
                    ToCoord = 2,
                    ToState = new GameState(
                        "111111" +
                        "1p1111" +
                        "P11111" +
                        "11P111" +
                        "111111" +
                        "111111",
                        Turn.White,
                        20)
                });
        }


        [TestMethod()]
        public void CannotMoveOnOtherColorTurn()
        {
            AssertMove(
                new AssertMoveModel()
                {
                    FromBoard = new GameState(
                        "111" +
                        "1p1" +
                        "P11",
                        Turn.Black),
                    FromCoord = 6,
                    ToCoord = 2,
                    ToState = new GameState(
                        "111" +
                        "1p1" +
                        "P11",
                        Turn.Black
                    )
                });
        }

        [TestMethod()]
        public void CannotMoveToOtherColorPawnPosition()
        {

            AssertMove(
                new AssertMoveModel()
                {
                    FromBoard = new GameState(
                        "111" +
                        "1p1" +
                        "P11",
                        Turn.White),
                    FromCoord = 6,
                    ToCoord = 4,
                    ToState = new GameState(
                        "111" +
                        "1p1" +
                        "P11",
                        Turn.White)
                });
        }

        private void AssertMove(AssertMoveModel assertMoveModel)
        {
            AssertMove(assertMoveModel.FromBoard, assertMoveModel.FromCoord, assertMoveModel.ToCoord, assertMoveModel.ToState);
        }

        private void AssertMove(GameState from, int fromCoord, int toCoord, GameState expected)
        {
            var moveUseCase = CreateHumanTryMoveFigureUseCase();

            var actual = moveUseCase.Execute(@from, fromCoord, toCoord);

            Assert.AreEqual(expected.Board.ToString(), actual.Board.ToString());
            Assert.AreEqual(expected.MustGoFromPosition, actual.MustGoFromPosition);
            Assert.AreEqual(expected.Turn, actual.Turn);
        }

        private HumanTryMoveFigureUseCase CreateHumanTryMoveFigureUseCase()
        {

            var validateFiguresService = new ValidateRule(
                new ValidateFigureService(
                    new ValidatePawnService(),
                    new ValidateQueenService()));

            return new HumanTryMoveFigureUseCase(
                new ValidateHumanService(validateFiguresService),
                new MoveRule(new ValidateFigureService(
                    new ValidatePawnService(),
                    new ValidateQueenService())));

        }
    }
}