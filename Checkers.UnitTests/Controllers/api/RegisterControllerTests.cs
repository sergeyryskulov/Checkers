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
            var registerService = new Mock<IRegisterService>();
            registerService.Setup(m => m.Register("test")).Returns("registrationId");
            
            var registerController = new RegisterController(registerService.Object);

            var actual = registerController.Post("test");

            Assert.AreEqual("registrationId", actual);
        }

        [TestMethod()]
        public void PostNullTest()
        {
            var registerService = new Mock<IRegisterService>();
            registerService.Setup(m => m.Register("")).Returns("registrationId");

            var registerController = new RegisterController(registerService.Object);

            var actual = registerController.Post(null);

            Assert.AreEqual("registrationId", actual);
        }
    }
}