using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.BL.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Ckeckers.DAL.Repositories;

namespace Checkers.BL.Services.Tests
{
    [TestClass()]
    public class RegisterServiceTests
    {
        [TestMethod()]
        public void RegisterTest()
        {
            var boardRepository = new Mock<IBoardRepository>();

            var idGeneratorRepository = new Mock<IRegistrationIdGeneratorRepository>();
            idGeneratorRepository.Setup(m => m.GenerateId()).Returns("testid");

            var registerService = new RegisterService(boardRepository.Object, idGeneratorRepository.Object);

            string actual = registerService.Register("firstPosition");

            boardRepository.Verify(m => m.Save(It.IsAny<string>(), "firstPosition"));            

            Assert.AreEqual("testid", actual);
        }

        [TestMethod()]
        public void RegisterDefaultPositionTest()
        {
            var boardRepository = new Mock<IBoardRepository>();

            var idGeneratorRepository = new Mock<IRegistrationIdGeneratorRepository>();
            idGeneratorRepository.Setup(m => m.GenerateId()).Returns("testid");

            var registerService = new RegisterService(boardRepository.Object, idGeneratorRepository.Object);

            string actual = registerService.Register("");

            Assert.AreEqual("testid", actual);
        }
    }
}