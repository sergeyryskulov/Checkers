using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Checkers.DomainModels.Enums;
using Checkers.DomainModels;
using Checkers.DomainModels.Models;

namespace Checkers.Web.Models.Tests;

[TestClass()]
public class GameStateDtoTests
{
    [TestMethod()]
    public void CreateBoardStateDto_WhiteTurn()
    {
        var actual = new GameStateDto(new GameState("cells", Turn.White, 1));

        Assert.AreEqual("cells", actual.Cells);
        Assert.AreEqual((char)Turn.White, actual.Turn);
        Assert.AreEqual(1, actual.MustGoFrom);
        Assert.AreEqual(1, actual.Links.Length);
    }


    [TestMethod()]
    public void CreateBoardStateDto_BlackTurn()
    {
        var actual = new GameStateDto(new GameState("cells", Turn.Black));

        Assert.AreEqual("cells", actual.Cells);
        Assert.AreEqual((char)Turn.Black, actual.Turn);
        Assert.IsNull(actual.MustGoFrom);
        Assert.AreEqual(1, actual.Links.Length);
    }
}