using System.ComponentModel.DataAnnotations;
using Checkers.Contracts.UseCases;
using Checkers.DomainModels;
using Checkers.DomainModels.Enums;
using Checkers.Web.Controllers.api;
using Checkers.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Checkers.Web.Controllers.api.Tests
{
    [TestClass()]
    public class BoardControllerTests
    {
        [TestMethod()]
        public void PostTest()
        {
            var moveAndSaveFigureService = new Mock<IHumanTryMoveFigureUseCase>();

            moveAndSaveFigureService.Setup(m => m.Execute(It.Is<GameState>(t => t.Board.ToString() == "p111"), 3, 0))
                .Returns(new GameState("111q", Turn.Black));

            var moveFigureController = new BoardController(moveAndSaveFigureService.Object);

            var actual = moveFigureController.MoveFigure("p111", 'b', null, 3, 0);

            Assert.AreEqual("111q", actual.Cells);
            Assert.AreEqual((char)Turn.Black, actual.Turn);

            Assert.AreEqual("calculateStep", actual.Links[0].Rel);
            Assert.AreEqual("/api/intellect/calculateStep?cells=111q&turn=b", actual.Links[0].Href);
        }
    }
}
