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
            intellectService.Setup(m => m.IntellectStep("registrationId")).Returns("boardState");

            var intellectStepController = new IntellectStepController(intellectService.Object);

            var actual = intellectStepController.Post("registrationId");

            Assert.AreEqual("boardState", actual);

        }
    }
}