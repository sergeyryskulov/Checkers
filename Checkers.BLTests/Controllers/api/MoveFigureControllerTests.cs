using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Checkers.BL.Services;
using Moq;

namespace Checkers.Controllers.Tests
{
    [TestClass()]
    public class MoveFigureControllerTests
    {
        [TestMethod()]
        public void PostTest()
        {
            var moveAndSaveFigureService = new Mock<IMoveFigureService>();

            moveAndSaveFigureService.Setup(m => m.Move(3, 0, "registrationId")).Returns(
                "1Q" +
                "11W");

            var moveFigureController = new MoveFigureController(moveAndSaveFigureService.Object);

            var actual = moveFigureController.Post(3, 0, "registrationId");

            Assert.AreEqual("1Q11W", actual);
        }
    }
}