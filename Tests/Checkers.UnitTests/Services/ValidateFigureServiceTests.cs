using Checkers.Core.Interfaces;
using Checkers.Core.Services;
using Checkers.DomainModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Checkers.UnitTests.Services
{
    [TestClass()]
    public class ValidateFigureServiceTests
    {
        
        [TestMethod()]
        public void GetAllowedMoveVectorsIncorrectFigureTest()
        {
            var actual = CreateValidateFigureService().GetAllowedMoveVectors(0, new Board("G111"));

            Assert.AreEqual(false, actual.AnyVectorExists());

            Assert.IsFalse(actual.EatFigure);
        }

        private ValidateService  CreateValidateFigureService()
        {
            return new ValidateService(new Mock<IValidatePawnService>().Object, new Mock<IValidateQueenService>().Object);
        }
    }
}