﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.BL.Constants.Enums;
using Checkers.BL.Helper;
using Checkers.BL.Models;

namespace Checkers.BL.Services.Tests
{
    [TestClass()]
    public class PawnServiceTests
    {
        private PawnService _pawnService = new PawnService(new VectorHelper(), new MathHelper(), new ColorHelper());

        [TestMethod()]
        public void GetAllowedVectors_OneStepTop_Correct()
        {

            var actual = _pawnService.GetAllowedVectors(2, "11P1");

            var expected = new List<Vector>()
            {
                new Vector()
                {
                    Direction = Direction.RightTop,
                    Length = 1
                }
            };
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod()]
        public void GetAllowedVectors_TwoStepTop_Correct()
        {

            var actual = _pawnService.GetAllowedVectors(8, "" +
                                                                  "111" +
                                                                  "1p1" +
                                                                  "11P");

            var expected = new List<Vector>()
            {
                new Vector()
                {
                    Direction = Direction.LeftTop,
                    Length = 2
                }
            };

            CollectionAssert.AreEquivalent(expected, actual);

        }

        [TestMethod()]
        public void GetAllowedVectors_TwoStepBottom_Correct()
        {

            var actual = _pawnService.GetAllowedVectors(0, "" +
                                                           "p11" +
                                                           "1P1" +
                                                           "111");

            var expected = new List<Vector>()
            {
                new Vector()
                {
                    Direction = Direction.RightBottom,
                    Length = 2
                }
            };
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod()]
        public void GetAllowedVectors_Blocked_Error()
        {

            var actual = _pawnService.GetAllowedVectors(0, "" +
                                                           "P111" +
                                                           "1111" +
                                                           "1p11" +
                                                           "P111");

            var expected = new List<Vector>()
            {

            };
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod()]
        public void GetAllowedVectorsTest()
        {
            var figures = "" +
                         "1p1p1p1p" +
                         "p1p1p1p1" +
                         "111p1p1p" +
                         "p1111111" +
                         "11111P11" +
                         "P1P1P111" +
                         "1P1P1P1P" +
                         "P1P1P1P1";

            var actualLength= _pawnService.GetAllowedVectors(8, figures).Count;

            Assert.IsTrue(actualLength > 0);
        }


        [TestMethod()]
        public void GetAllowedVectorsBackTest()
        {
            var actual = _pawnService.GetAllowedVectors(0, "" +
                                                           "P11" +
                                                           "1p1" +
                                                           "111");

            var expected = new List<Vector>()
            {
                new Vector()
                {
                    Direction = Direction.RightBottom,
                    Length = 2
                }
            };
            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}