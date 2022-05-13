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