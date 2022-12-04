using System.ComponentModel.DataAnnotations;
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
    public class MoveFigureControllerTests
    {
        [TestMethod()]
        public void PostTest()
        {
            var moveAndSaveFigureService = new Mock<IHumanPlayerService>();

            moveAndSaveFigureService.Setup(m => m.TryMoveFigure(It.Is<GameState>(t=>t.Board.ToString()=="p111"), 3, 0))            
            .Returns(new GameState("111q", Turn.Black));

            var moveFigureController = new BoardController(moveAndSaveFigureService.Object);

            var actual = moveFigureController.MoveFigure("p111", 'b', null, 3, 0);

            Assert.AreEqual("111q", actual.Cells);
            Assert.AreEqual((char)Turn.Black, actual.Turn);

            Assert.AreEqual("calculateStep", actual.Links[0].Rel);
            Assert.AreEqual("/api/intellect/calculateStep?cells=111q&turn=Black", actual.Links[0].Href);
        }
    }
}