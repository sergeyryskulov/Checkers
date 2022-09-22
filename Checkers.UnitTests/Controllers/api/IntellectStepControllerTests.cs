using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.Web.Controllers.api;
using System;
using System.Collections.Generic;
using System.Text;
using Checkers.BL.Services;
using Moq;

namespace Checkers.Web.Controllers.api.Tests
{
    [TestClass()]
    public class IntellectStepControllerTests
    {
        [TestMethod()]
        public void PostTest()
        {
            var intellectService = new Mock<IIntellectService>();
            intellectService.Setup(m => m.CalculateStep("oldBoardState")).Returns("newBoardState");

            var intellectController = new IntellectController(intellectService.Object);

            var actual = intellectController.CalculateStep("oldBoardState");

            Assert.AreEqual("newBoardState", actual);

        }
    }
}