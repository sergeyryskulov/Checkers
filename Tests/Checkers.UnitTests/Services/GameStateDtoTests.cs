using Checkers.DomainModels;
using Checkers.DomainModels.Enums;
using Checkers.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkers.UnitTests.Services
{
    [TestClass()]
    public class GameStateDtoTests
    {

        [TestMethod()]
        public void CreateBoardStateDto_WhiteTurn()
        {
            var actual = new GameStateDto(new GameState("cells", Turn.White, 1));
            
            Assert.AreEqual("cells", actual.Cells);
            Assert.AreEqual((char)Turn.White, actual.Turn);
            Assert.AreEqual(1, actual.MustGoFrom);
            Assert.AreEqual(1, actual.Links.Length);
        }


        [TestMethod()]
        public void CreateBoardStateDto_BlackTurn()
        {
            var actual = new GameStateDto(new GameState("cells", Turn.Black));

            Assert.AreEqual("cells", actual.Cells);
            Assert.AreEqual((char)Turn.Black, actual.Turn);
            Assert.IsNull(actual.MustGoFrom);
            Assert.AreEqual(1, actual.Links.Length);
        }
    }
}