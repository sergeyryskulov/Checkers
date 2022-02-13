using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.Controllers.api;
using System;
using System.Collections.Generic;
using System.Text;
using Checkers.BL.Services;
using Moq;

namespace Checkers.Controllers.api.Tests
{
    [TestClass()]
    public class RegisterControllerTests
    {
        [TestMethod()]
        public void PostTest()
        {
            var newGameService = new Mock<INewGameService>();

            newGameService.Setup(m => m.NewGame("registrationId")).Returns("defaultFigures");

            var newGameController = new NewGameController(newGameService.Object);

            var actual = newGameController.Post("registrationId");

            Assert.AreEqual("defaultFigures", actual);
        }
    }
}