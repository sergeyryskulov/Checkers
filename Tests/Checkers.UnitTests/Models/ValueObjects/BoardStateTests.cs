using Checkers.Core.Constants;
using Checkers.Core.Models.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkers.UnitTests.Models.ValueObjects
{
    [TestClass()]
    public class BoardStateTests
    {
        [TestMethod()]
        public void GetHashCodeTest()
        {
            var boardState = new GameState("", Turn.White);

            var actual = boardState.GetHashCode();

            Assert.AreEqual(actual, "".GetHashCode() + (int)Turn.White);
        }
    }
}