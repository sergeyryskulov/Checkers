using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.BL.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.BL.Constants.Enums;
using Checkers.BL.Models;

namespace Checkers.BL.Helper.Tests
{
    [TestClass()]
    public class VectorHelperTests
    {

        private VectorHelper _vectorHelper = new VectorHelper();


        [TestMethod()]
        public void MoveTest_Correct()
        {

            var actual = _vectorHelper.Move(0, new Vector()
            {
                Direction = Direction.RightBottom,
                Length = 1
            },
                2);

            var expected = 3;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void MoveTest_Incorrect()
        {

            var actual = _vectorHelper.Move(0, new Vector()
            {
                Direction = Direction.LeftBottom,
                Length = 1
            },
                2);

            var expected = -1;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void ConvertToVector_RightBottom_Correct()
        {
            var actual= _vectorHelper.ConvertToVector(0, 3, 2);

            var expected = new Vector()
            {
                Length = 1,
                Direction = Direction.RightBottom
            };

            Assert.AreEqual(expected, actual);
        }
    }
}