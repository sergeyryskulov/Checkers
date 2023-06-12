using Checkers.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkers.Web.Controllers.Tests;

[TestClass()]
public class GameControllerTests
{
    [TestMethod()]
    public void IndexTest()
    {
        var gameController = CreateGameController();

        var actual = gameController.Index() as ViewResult;

        Assert.IsNull(actual.Model);

        Assert.AreEqual("Game", actual.ViewName);
    }

    private GameController CreateGameController()
    {
        return new GameController();
    }
}