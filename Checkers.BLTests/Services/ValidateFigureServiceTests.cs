using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.BL.Constants.Enums;
using Checkers.BL.Helper;
using Checkers.BL.Interfaces;
using Checkers.BL.Models;
using Moq;

namespace Checkers.BL.Services.Tests
{
    [TestClass()]
    public class ValidateFigureServiceTests
    {
        
        [TestMethod()]
        public void GetAllowedMoveVectorsIncorrectFigureTest()
        {
            var actual = CreateValidateFigureService().GetAllowedMoveVectors(0, "G111");

            Assert.AreEqual(actual.Vectors.Count, 0);

            Assert.IsFalse(actual.EatFigure);
        }

        private ValidateFigureService  CreateValidateFigureService()
        {
            return new ValidateFigureService(new Mock<IValidatePawnService>().Object, new Mock<IValidateQueenService>().Object);
        }
    }
}