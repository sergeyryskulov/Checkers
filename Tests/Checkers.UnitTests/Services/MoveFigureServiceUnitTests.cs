using Checkers.Contracts.Rules;
using Checkers.DomainModels;
using Checkers.DomainModels.Enums;
using Checkers.HumanPlayer.Interfaces;
using Checkers.HumanPlayer.Services;
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
            var validateBoardService = new Mock<IValidateHumanService>();
            validateBoardService.Setup(m => m.CanMove(
                It.Is<GameState>(t=>t.Board.ToString()=="11P1" && t.Turn==Turn.White && t.MustGoFromPosition==null), 3, 0)).Returns(true);

            
            var directMoveService = new Mock<IMoveRule>();
            directMoveService.Setup(m => m.MoveFigureWithoutValidation(
                It.Is<GameState>(t=>t.Board.ToString()=="11P1" && t.Turn==Turn.White && t.MustGoFromPosition==null), 3, 0)).Returns(
                new GameState("1Q11", Turn.WhiteWin));


            var moveFigureService = new HumanTryMoveFigureUseCase(                
                validateBoardService.Object,
                directMoveService.Object
            );

            var actual = moveFigureService.Execute(new GameState("11P1",Turn.White), 3, 0);          
            
            Assert.AreEqual("1Q11", actual.Board.ToString());
            Assert.AreEqual(Turn.WhiteWin, actual.Turn);
        }
    }
}