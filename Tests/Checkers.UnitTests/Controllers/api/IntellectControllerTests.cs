using Checkers.Contracts.UseCases;
using Checkers.DomainModels;
using Checkers.DomainModels.Enums;
using Checkers.DomainModels.Models;
using Checkers.Web.Controllers.api;
using Checkers.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Checkers.Web.Controllers.api.Tests
{
    [TestClass()]
    public class IntellectControllerTests
    {
        private Mock<IComputerCalculateNextStepUseCase> _computerCalculateNextStepUseCase;

        [TestInitialize]
        public void CreateMocks()
        {
            _computerCalculateNextStepUseCase = new Mock<IComputerCalculateNextStepUseCase>();
        }

        [TestMethod()]
        public void PostTest()
        {
            _computerCalculateNextStepUseCase.Setup(m => m.Execute(
                    It.Is<GameState>(t => t.Board.ToString() == "p111")))
                .Returns(new GameState("111Q", Turn.BlackWin));

            var intellectController = CreateIntellectController();

            var actual = intellectController.CalculateStep("p111", 'b', null);

            Assert.AreEqual("111Q", actual.Cells);
            Assert.AreEqual((char)Turn.BlackWin, actual.Turn);
            Assert.IsNull(actual.MustGoFrom);
            Assert.AreEqual(0, actual.Links.Length);

        }
        private IntellectController CreateIntellectController()
        {
            return new IntellectController(
                _computerCalculateNextStepUseCase.Object
            );
        }
    }
}