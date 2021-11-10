using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ckeckers.DAL.Repositories;
using Moq;

namespace Checkers.BL.Services.Tests
{
    [TestClass()]
    public class MoveFigureServiceTests
    {
        [TestMethod()]
        public void Move_Correct()
        {

            var boardRepository = new Mock<IBoardRepository>();
            boardRepository.Setup(m => m.Load("registrationId")).Returns("P111");

            var service = new MoveFigureService(boardRepository.Object);

            string result =  service.Move(0, 3, "registrationId");


            Assert.IsTrue(result=="111P");
        }
    }
}