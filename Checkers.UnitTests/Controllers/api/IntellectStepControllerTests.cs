using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.Web.Controllers.api;
using System;
using System.Collections.Generic;
using System.Text;
using Checkers.BL.Services;
using Moq;
using Checkers.BL.Models;
using Checkers.BL.Constants;

namespace Checkers.Web.Controllers.api.Tests
{
    [TestClass()]
    public class IntellectStepControllerTests
    {
        [TestMethod()]
        public void PostTest()
        {
            var intellectService = new Mock<IIntellectService>();
            intellectService.Setup(m => m.CalculateStep(
                It.Is<BoardState>(t => t.Cells == "p111")))            
            .Returns(new BoardState()
            {
                Cells = "111Q",
                Turn = Turn.BlackWin
            });

            var intellectController = new IntellectController(intellectService.Object);

            var actual = intellectController.CalculateStep("p111", Turn.Black, null);

            Assert.AreEqual("111Q", actual.Cells);
            Assert.AreEqual(Turn.BlackWin, actual.Turn);
            Assert.IsNull(actual.MustGoFrom);

        }
    }
}