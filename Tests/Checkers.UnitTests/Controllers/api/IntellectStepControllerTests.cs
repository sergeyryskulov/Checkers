using Checkers.Core.Constants;
using Checkers.Core.Interfaces;
using Checkers.Core.Models.ValueObjects;
using Checkers.DomainModels;
using Checkers.DomainModels.Enums;
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
            var intellectService = new Mock<IComputerPlayerService>();
            intellectService.Setup(m => m.CalculateNextStep(
                    It.Is<GameState>(t => t.Board.ToString() == "p111")))
                .Returns(new GameState("111Q", Turn.BlackWin));

            var dtoFactory = new Mock<IBoardStateDtoFactory>();

            dtoFactory.Setup(m =>
                m.CreateBoardStateDto(It.Is<GameState>(t => t.Board.ToString() == "111Q" && t.Turn == Turn.BlackWin))).Returns(
                new GameStateDto(new Board("111Q"), Turn.BlackWin, null, new LinkDto[0])
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