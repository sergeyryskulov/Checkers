using Checkers.DomainModels;
using Checkers.DomainModels.Models;
using Checkers.Rules.Interfaces;
using Checkers.Rules.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Checkers.Rules.Services.Tests
{
    [TestClass()]
    public class ValidateFigureServiceTests
    {
        private Mock<IValidatePawnService> _validatePawnService;
        private Mock<IValidateQueenService> _validateQueenService;

        [TestInitialize]
        public void CreateMocks()
        {
            _validatePawnService = new Mock<IValidatePawnService>();
            _validateQueenService = new Mock<IValidateQueenService>();
        }

        [TestMethod()]
        public void GetAllowedMoveVectors_IncorrectFigure()
        {
            var validateFigureService = CreateValidateFigureService();

            var actual = validateFigureService.GetAllowedMoveVectors(new Board("G111"), 0);

            Assert.AreEqual(false, actual.AnyVectorExists());

            Assert.IsFalse(actual.EatFigure);
        }

        private ValidateFigureService CreateValidateFigureService()
        {
            return new ValidateFigureService(
                _validatePawnService.Object,
                _validateQueenService.Object
            );
        }
    }
}
