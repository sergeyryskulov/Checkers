using Checkers.Controllers;
using Checkers.Core.Constants;
using Checkers.Core.Interfaces;
using Checkers.Core.Models.ValueObjects;
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

            moveAndSaveFigureService.Setup(m => m.Move(3, 0, It.Is<BoardState>(t=>t.Cells=="p111")))            
            .Returns(new BoardState("111q", Turn.Black));

            var moveFigureController = new BoardController(moveAndSaveFigureService.Object);

            var actual = moveFigureController.MoveFigure("p111", Turn.Black, null, 3, 0);

            Assert.AreEqual("111q", actual.Cells);
            Assert.AreEqual(Turn.Black, actual.Turn);
        }
    }
}