using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.BL.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Ckeckers.DAL.Repositories;
using Moq;

namespace Checkers.BL.Services.Tests
{
    [TestClass()]
    public class MoveAndSaveFigureServiceTests
    {
        [TestMethod()]
        public void MoveAndSaveFigureTest()
        {
            var boardRepository = new Mock<IBoardRepository>();
            boardRepository.Setup(m => m.Load("registrationid")).Returns(
                "11" +
                "P1w");

            var validateBoardService = new Mock<IValidateBoardService>();
            validateBoardService.Setup(m => m.CanMove("11P1w", 3, 0)).Returns(true);

            
            var directMoveService = new Mock<IDirectMoveService>();
            directMoveService.Setup(m => m.DirectMove(
                "11" +
                "P1w", 3, 0)).Returns(
                "1Q" +
                "11W");


            var moveAndSaveFigureService = new MoveAndSaveFigureService(
                boardRepository.Object,
                validateBoardService.Object,
                directMoveService.Object
            );

            var actual = moveAndSaveFigureService.MoveAndSaveFigure(3, 0, "registrationid");

            boardRepository.Verify(m => m.Save("registrationid", "1Q" +
                                                                 "11W"));
            
            Assert.AreEqual("1Q11W", actual);
        }
    }
}