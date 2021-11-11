using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.BL.Constants.Enums;
using Checkers.BL.Models;

namespace Checkers.BL.Services.Tests
{
    [TestClass()]
    public class BoundServiceTests
    {
        [TestMethod()]
        public void GetAllowedVectors_OnlyRightBottom()
        {
            var boundService = new BoundService();

            var actual= boundService.GetAllowedVectors(0, 2);

            var expected = new Vector[]
            {
                new Vector()
                {
                    Base = Direction.RightBottom,
                    Length = 1
                }
            };

            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}