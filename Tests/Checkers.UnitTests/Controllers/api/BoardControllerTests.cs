using System.ComponentModel.DataAnnotations;
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
    public class BoardControllerTests
    {
        private Mock<IHumanTryMoveFigureUseCase> _humanTryMoveFigureUseCase;

        [TestInitialize]
        public void CreateMocks()
        {
            _humanTryMoveFigureUseCase = new Mock<IHumanTryMoveFigureUseCase>();
        }

        private BoardController CreateBoardController()
        {
            return new BoardController(
                _humanTryMoveFigureUseCase.Object
            );
        }


        [TestMethod()]
        public void PostTest()
        {
            _humanTryMoveFigureUseCase.Setup(m => m.Execute(It.Is<GameState>(t => t.Board.ToString() == "p111"), 3, 0))
                .Returns(new GameState("111q", Turn.Black));

            var boardController = CreateBoardController();

            var actual = boardController.MoveFigure("p111", 'b', null, 3, 0);

            Assert.AreEqual("111q", actual.Cells);
            Assert.AreEqual((char)Turn.Black, actual.Turn);

            Assert.AreEqual("calculateStep", actual.Links[0].Rel);
            Assert.AreEqual("/api/intellect/calculateStep?cells=111q&turn=b", actual.Links[0].Href);
        }
    }
}