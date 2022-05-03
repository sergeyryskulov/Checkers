using Checkers.BL.Services;
using Ckeckers.DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Checkers.IntegrationTests
{
    [TestClass()]
    public class GetFiguresServiceTests
    {
        [TestMethod()]
        public void GetFiguresTest()
        {

            var boardRepository = new Mock<IBoardRepository>();

            boardRepository.Setup(m => m.Load("registrationId")).Returns("figuresState");

            var getFiguresService = new GetFiguresService(boardRepository.Object);

            var figuresActual = getFiguresService.GetFigures("registrationId");

            Assert.AreEqual("figuresState", figuresActual);

        }
    }
}

