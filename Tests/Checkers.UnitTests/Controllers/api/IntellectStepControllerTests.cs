using Checkers.Core.Constants;
using Checkers.Core.Interfaces;
using Checkers.Core.Models.ValueObjects;
using Checkers.Web.Controllers.api;
using Checkers.Web.Factories;
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
            var intellectService = new Mock<IIntellectService>();
            intellectService.Setup(m => m.CalculateStep(
                    It.Is<BoardState>(t => t.Cells == "p111")))
                .Returns(new BoardState("111Q", Turn.BlackWin));

            var dtoFactory = new Mock<IBoardStateDtoFactory>();

            dtoFactory.Setup(m =>
                m.CreateBoardStateDto(It.Is<BoardState>(t => t.Cells == "111Q" && t.Turn == Turn.BlackWin))).Returns(
                new BoardStateDto("111Q", Turn.BlackWin, null, new LinkDto[0])
            );

            var intellectController = new IntellectController(intellectService.Object, dtoFactory.Object);

            var actual = intellectController.CalculateStep("p111", 'b', null);

            Assert.AreEqual("111Q", actual.Cells);
            Assert.AreEqual((char)Turn.BlackWin, actual.Turn);
            Assert.IsNull(actual.MustGoFrom);
            Assert.AreEqual(0, actual.Links.Length);

        }
    }
}