using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.BL.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Ckeckers.DAL.Repositories;
using Moq;

namespace Checkers.BL.Services.Tests
{
    [TestClass()]
    public class NewGameServiceTests
    {
        [TestMethod()]
        public void NewGameTest()
        {
            var boardRepository = new Mock<IBoardRepository>();

            boardRepository.Setup(m => m.Load("registrationId")).Returns("defaultFigures");

            var newGameService = new NewGameService(boardRepository.Object);
                       
            var figuresActual =  newGameService.NewGame("registrationId");

            boardRepository.Verify(m => m.Save("registrationId", ""));

            Assert.AreEqual("defaultFigures", figuresActual);

        }
    }
}