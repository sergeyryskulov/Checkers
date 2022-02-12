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
    }
}