using Checkers.DomainModels;
using Checkers.Rules.Interfaces;
using Checkers.Rules.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Checkers.UnitTests.Services
{
    [TestClass()]
    public class ValidateServiceTests
    {
        
        [TestMethod()]
        public void GetAllowedMoveVectorsIncorrectFigureTest()
        {
            var actual = CreateValidateFigureService().GetAllowedMoveVectors(new Board("G111"), 0);

            Assert.AreEqual(false, actual.AnyVectorExists());

            Assert.IsFalse(actual.EatFigure);
        }

        private ValidateService  CreateValidateFigureService()
        {
            return new ValidateService(new Mock<IValidatePawnService>().Object, new Mock<IValidateQueenService>().Object);
        }
    }
}