using Checkers.Core.Models.ValueObjects;
using Checkers.DomainModels;
using Checkers.DomainModels.Enums;
using Checkers.DomainServices;
using Checkers.Web.Controllers.api;
using Checkers.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Checkers.UnitTests.Controllers.api
{
    [TestClass()]
    public class IntellectStepControllerTests
    {
        [TestMethod()]
        public void PostTest()
        {
            var intellectService = new Mock<IComputerPlayerService>();
            intellectService.Setup(m => m.CalculateNextStep(
                    It.Is<GameState>(t => t.Board.ToString() == "p111")))
                .Returns(new GameState("111Q", Turn.BlackWin));


            var intellectController = new IntellectController(intellectService.Object);

            var actual = intellectController.CalculateStep("p111", 'b', null);

            Assert.AreEqual("111Q", actual.Cells);
            Assert.AreEqual((char)Turn.BlackWin, actual.Turn);
            Assert.IsNull(actual.MustGoFrom);
            Assert.AreEqual(0, actual.Links.Length);

        }
    }
}