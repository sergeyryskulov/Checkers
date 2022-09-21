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
    public class MoveFigureServiceUnitTests
    {
        [TestMethod()]
        public void MoveFigureTest()
        {          
            var validateBoardService = new Mock<IValidateBoardService>();
            validateBoardService.Setup(m => m.CanMove("11P1w", 3, 0)).Returns(true);

            
            var directMoveService = new Mock<IDirectMoveService>();
            directMoveService.Setup(m => m.DirectMove(
                "11" +
                "P1w", 3, 0)).Returns(
                "1Q" +
                "11W");


            var moveFigureService = new MoveFigureService(                
                validateBoardService.Object,
                directMoveService.Object
            );

            var actual = moveFigureService.Move(3, 0,  "11P1w");          
            
            Assert.AreEqual("1Q11W", actual);
        }
    }
}