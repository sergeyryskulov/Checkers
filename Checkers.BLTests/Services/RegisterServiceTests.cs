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
            
            var registerService = new RegisterService(boardRepository.Object);

            registerService.Register("firstPosition");

            boardRepository.Verify(m => m.Save(It.IsAny<string>(), "firstPosition"));            
        }
    }
}