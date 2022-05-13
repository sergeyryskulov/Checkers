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
    public class GetFiguresControllerTests
    {
        [TestMethod()]
        public void PostTest()
        {
            var getFiguresService = new Mock<IGetFiguresService>();
            getFiguresService.Setup(m => m.GetFigures("registrationId")).Returns("figures");

            var getFiguresController = new GetFiguresController(getFiguresService.Object);

            var actualFigures = getFiguresController.Post("registrationId");

            Assert.AreEqual("figures", actualFigures);
        }
    }
}