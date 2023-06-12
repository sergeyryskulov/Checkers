using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Checkers.DomainModels;
using Checkers.DomainModels.Enums;
using Checkers.DomainModels.Models;

namespace Checkers.DomainModels.Models.Tests;

[TestClass()]
public class GameStateTests
{
    [TestMethod()]
    public void Equals_False()
    {
        var gameState = new GameState("1111", Turn.Black, null);

        var actual = gameState.Equals(new GameState("1111", Turn.White, null));

        Assert.IsFalse(actual);
    }

    [TestMethod()]
    public void Equals_True()
    {
        var gameState = new GameState("1111", Turn.Black, null);

        var actual = gameState.Equals(new GameState("1111", Turn.Black, null));

        Assert.IsTrue(actual);
    }

    [TestMethod()]
    public void GetHashCodeTest()
    {
        var boardState = new GameState("", Turn.White);

        var actual = boardState.GetHashCode();

        Assert.AreEqual(actual, "".GetHashCode() + (int)Turn.White);
    }
}