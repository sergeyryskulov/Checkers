using Checkers.DomainModels;
using Checkers.Rules.Interfaces;
using Checkers.Rules.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Checkers.UnitTests.Services
{
    [TestClass()]
    public class ValidateFigureServiceTests
    {
        
        [TestMethod()]
        public void GetAllowedMoveVectors_IncorrectFigure()
        {
            var actual = CreateValidateFigureService().GetAllowedMoveVectors(new Board("G111"), 0);

            Assert.AreEqual(false, actual.AnyVectorExists());

            Assert.IsFalse(actual.EatFigure);
        }

        private ValidateFigureService  CreateValidateFigureService()
        {
            return new ValidateFigureService(new Mock<IValidatePawnService>().Object, new Mock<IValidateQueenService>().Object);
        }
    }
}