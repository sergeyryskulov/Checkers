using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.BL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.BL.Models.Tests
{
    [TestClass()]
    public class VectorTests
    {
        [TestMethod()]
        public void NotEqualsToNullTest()
        {
            var vector = new Vector()
            {
                Direction = Constants.Enums.Direction.LeftTop,
                Length = 2
            };

            var isEqualsToNull = vector.Equals(null);

            Assert.AreEqual(false, isEqualsToNull);

        }

        [TestMethod()]
        public void ToStringTest()
        {
            var vector = new Vector()
            {
                Direction = Constants.Enums.Direction.LeftTop,
                Length = 2
            };

            var actualString = vector.ToString();

            Assert.AreEqual("LeftTop (2)", actualString);

        }

        [TestMethod()]
        public void EqualsTest()
        {
            var vector1 = new Vector()
            {
                Direction = Constants.Enums.Direction.LeftTop,
                Length = 2
            };

            var vector2 = new Vector()
            {
                Direction = Constants.Enums.Direction.LeftTop,
                Length = 2
            };

            var actual = vector1.Equals(vector2);
            
            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public void NotEqualsTest()
        {
            var vector1 = new Vector()
            {
                Direction = Constants.Enums.Direction.LeftTop,
                Length = 2
            };

            var vector2 = new Vector()
            {
                Direction = Constants.Enums.Direction.RightBottom,
                Length = 2
            };

            var actual = vector1.Equals(vector2);

            Assert.IsFalse(actual);
        }
    }
}