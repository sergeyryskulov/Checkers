using Checkers.Core.Constants;
using Checkers.Core.Interfaces;
using Checkers.Core.Models.ValueObjects;
using Checkers.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Checkers.UnitTests.Services
{
    [TestClass()]
    public class MoveFigureServiceUnitTests
    {
        [TestMethod()]
        public void MoveFigureTest()
        {          
            var validateBoardService = new Mock<IValidateBoardService>();
            validateBoardService.Setup(m => m.CanMove(
                It.Is<GameState>(t=>t.Cells.ToString()=="11P1" && t.Turn==Turn.White && t.MustGoFrom==null), 3, 0)).Returns(true);

            
            var directMoveService = new Mock<IDirectMoveService>();
            directMoveService.Setup(m => m.MoveFigureWithoutValidation(
                It.Is<GameState>(t=>t.Cells.ToString()=="11P1" && t.Turn==Turn.White && t.MustGoFrom==null), 3, 0)).Returns(
                new GameState("1Q11", Turn.WhiteWin));


            var moveFigureService = new HumanPlayerService(                
                validateBoardService.Object,
                directMoveService.Object
            );

            var actual = moveFigureService.TryMoveFigure(new GameState("11P1",Turn.White), 3, 0);          
            
            Assert.AreEqual("1Q11", actual.Cells.ToString());
            Assert.AreEqual(Turn.WhiteWin, actual.Turn);
        }
    }
}