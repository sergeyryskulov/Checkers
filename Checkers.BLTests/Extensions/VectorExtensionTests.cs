using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.BL.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.BL.Constants.Enums;
using Checkers.BL.Models;
using Checkers.BL.Extensions;

namespace Checkers.BL.Helper.Tests
{
    [TestClass()]
    public class VectorExtensionTests
    {
        

        [TestMethod()]
        public void MoveTest_Correct()
        {

            var actual = (new Vector()
            {
                Direction = Direction.RightBottom,
                Length = 1
            }).VectorToCoord(0, 2);

            var expected = 3;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void MoveTest_Incorrect()
        {

            var actual =  (new Vector()
            {
                Direction = Direction.LeftBottom,
                Length = 1
            }).VectorToCoord(0, 2);

            var expected = -1;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void ConvertToVector_RightBottom_Correct()
        {
            var actual = 0.CoordToVector(3, 2);

            var expected = new Vector()
            {
                Length = 1,
                Direction = Direction.RightBottom
            };

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CoordToVectorTest()
        {
            var actualVector = 0.CoordToVector(100, 2);

            Assert.IsNull(actualVector);
            
        }
    }
}