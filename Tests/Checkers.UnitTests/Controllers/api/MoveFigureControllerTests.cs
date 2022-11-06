using System.ComponentModel.DataAnnotations;
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
    public class MoveFigureControllerTests
    {
        [TestMethod()]
        public void PostTest()
        {
            var moveAndSaveFigureService = new Mock<IMoveFigureService>();

            var dtoFactory = new Mock<IBoardStateDtoFactory>();

            dtoFactory.Setup(m =>
                m.CreateBoardStateDto(It.Is<BoardState>(t => t.Cells == "111q" && t.Turn == Turn.Black))).Returns(
                new BoardStateDto("111q", Turn.Black, null, new []
                {
                    new LinkDto("relation", "href"),
                })
            );

            moveAndSaveFigureService.Setup(m => m.Move(3, 0, It.Is<BoardState>(t=>t.Cells=="p111")))            
            .Returns(new BoardState("111q", Turn.Black));

            var moveFigureController = new BoardController(moveAndSaveFigureService.Object, dtoFactory.Object);

            var actual = moveFigureController.MoveFigure("p111", Turn.Black, null, 3, 0);

            Assert.AreEqual("111q", actual.Cells);
            Assert.AreEqual(Turn.Black, actual.Turn);

            Assert.AreEqual("relation", actual.Links[0].Rel);
            Assert.AreEqual("href", actual.Links[0].Href);
        }
    }
}