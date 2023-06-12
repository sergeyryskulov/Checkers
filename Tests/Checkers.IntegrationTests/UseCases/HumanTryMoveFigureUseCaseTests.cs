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

namespace Checkers.HumanPlayer.UseCases.Tests;

[TestClass()]
public class HumanTryMoveFigureUseCaseTests
{
    internal class AssertMoveModel
    {
        internal GameState FromState { get; set; }

        internal int MoveFromCoord { get; set; }

        internal int MoveToCoord { get; set; }

        internal GameState ResultState { get; set; }
    }

    [TestMethod()]
    public void PawnCanMoveToQueen()
    {
        AssertMove(
            new AssertMoveModel()
            {
                FromState = new GameState(
                    "p1" +
                    "11",
                    Turn.Black),
                MoveFromCoord = 0,
                MoveToCoord = 3,
                ResultState = new GameState(
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
                FromState = new GameState(
                    "p1" +
                    "11",
                    Turn.Black),
                MoveFromCoord = 0,
                MoveToCoord = 0,
                ResultState = new GameState(
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
                FromState = new GameState(
                    "q1" +
                    "11",
                    Turn.Black),
                MoveFromCoord = 0,
                MoveToCoord = 0,
                ResultState = new GameState(
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
                FromState = new GameState(
                    "111" +
                    "1p1" +
                    "P11",
                    Turn.White),
                MoveFromCoord = 6,
                MoveToCoord = 2,
                ResultState = new GameState(
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
                FromState = new GameState(
                    "111111" +
                    "111111" +
                    "P11p11" +
                    "111111" +
                    "1p1111" +
                    "P11111",
                    Turn.White),
                MoveFromCoord = 30,
                MoveToCoord = 20,
                ResultState = new GameState(
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
                FromState = new GameState(
                    "111Q11" +
                    "111111" +
                    "1p1111" +
                    "111111" +
                    "1p1111" +
                    "111111",
                    Turn.White),
                MoveFromCoord = 3,
                MoveToCoord = 18,
                ResultState = new GameState(
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
                FromState = new GameState(
                    "111111" +
                    "1p1111" +
                    "P11111" +
                    "11P111" +
                    "111111" +
                    "111111",
                    Turn.White,
                    20),
                MoveFromCoord = 12,
                MoveToCoord = 2,
                ResultState = new GameState(
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
                FromState = new GameState(
                    "111" +
                    "1p1" +
                    "P11",
                    Turn.Black),
                MoveFromCoord = 6,
                MoveToCoord = 2,
                ResultState = new GameState(
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
                FromState = new GameState(
                    "111" +
                    "1p1" +
                    "P11",
                    Turn.White),
                MoveFromCoord = 6,
                MoveToCoord = 4,
                ResultState = new GameState(
                    "111" +
                    "1p1" +
                    "P11",
                    Turn.White)
            });
    }

    private void AssertMove(AssertMoveModel assertMoveModel)
    {
        AssertMove(assertMoveModel.FromState, assertMoveModel.MoveFromCoord, assertMoveModel.MoveToCoord, assertMoveModel.ResultState);
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