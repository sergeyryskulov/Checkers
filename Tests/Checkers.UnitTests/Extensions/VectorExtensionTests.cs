using Checkers.Core.Constants.Enums;
using Checkers.Core.Extensions;
using Checkers.Core.Models.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkers.UnitTests.Extensions
{
    [TestClass()]
    public class VectorExtensionTests
    {
        

        [TestMethod()]
        public void MoveTest_Correct()
        {

            var actual = (new Vector(Direction.RightBottom, 1)).ToCoord(0, 2);

            var expected = 3;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void MoveTest_Incorrect()
        {

            var actual =  (new Vector(Direction.LeftBottom, 1)).ToCoord(0, 2);

            var expected = -1;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void ConvertToVector_RightBottom_Correct()
        {
            var actual = 0.ToVector(3, 2);

            var expected = new Vector(Direction.RightBottom, 1);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CoordToVectorTest()
        {
            var actualVector = 0.ToVector(100, 2);

            Assert.IsNull(actualVector);
            
        }
    }
}