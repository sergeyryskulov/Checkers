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
                It.Is<BoardState>(t=>t.Cells=="11P1" && t.Turn==Turn.White && t.MustGoFrom==null), 3, 0)).Returns(true);

            
            var directMoveService = new Mock<IDirectMoveService>();
            directMoveService.Setup(m => m.DirectMove(
                It.Is<BoardState>(t=>t.Cells=="11P1" && t.Turn==Turn.White && t.MustGoFrom==null), 3, 0)).Returns(
                new BoardState("1Q11", Turn.WhiteWin));


            var moveFigureService = new MoveFigureService(                
                validateBoardService.Object,
                directMoveService.Object
            );

            var actual = moveFigureService.Move(3, 0,  
                new BoardState("11P1",Turn.White));          
            
            Assert.AreEqual(new BoardState("1Q11", Turn.WhiteWin), actual);
        }
    }
}