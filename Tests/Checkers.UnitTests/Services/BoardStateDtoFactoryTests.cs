using Checkers.Core.Constants;
using Checkers.Core.Models.ValueObjects;
using Checkers.Web.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkers.UnitTests.Services
{
    [TestClass()]
    public class BoardStateDtoFactoryTests
    {


        [TestMethod()]
        public void CreateBoardStateDto_WhiteTurn()
        {

            var boardStateDtoFactory = new BoardStateDtoFactory();

            var actual = boardStateDtoFactory.CreateBoardStateDto(new BoardState("cells", Turn.White, 1));
            
            Assert.AreEqual("cells", actual.Cells);
            Assert.AreEqual(Turn.White, actual.Turn);
            Assert.AreEqual(1, actual.MustGoFrom);
            Assert.AreEqual(1, actual.Links.Length);

        }


        [TestMethod()]
        public void CreateBoardStateDto_BlackTurn()
        {

            var boardStateDtoFactoryService = new BoardStateDtoFactory();

            var actual = boardStateDtoFactoryService.CreateBoardStateDto(new BoardState("cells", Turn.Black));

            Assert.AreEqual("cells", actual.Cells);
            Assert.AreEqual(Turn.Black, actual.Turn);
            Assert.IsNull(actual.MustGoFrom);
            Assert.AreEqual(1, actual.Links.Length);
        }
    }
}