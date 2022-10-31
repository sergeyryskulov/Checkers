using Checkers.Core.Constants;
using Checkers.Core.Interfaces;
using Checkers.Core.Models.ValueObjects;
using Checkers.Web.Controllers.api;
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
            var intellectService = new Mock<IIntellectService>();
            intellectService.Setup(m => m.CalculateStep(
                    It.Is<BoardState>(t => t.Cells == "p111")))
                .Returns(new BoardState("111Q", Turn.BlackWin));

            var intellectController = new IntellectController(intellectService.Object);

            var actual = intellectController.CalculateStep("p111", Turn.Black, null);

            Assert.AreEqual("111Q", actual.Cells);
            Assert.AreEqual(Turn.BlackWin, actual.Turn);
            Assert.IsNull(actual.MustGoFrom);

        }
    }
}