﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.BL.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Checkers.BL.Constants.Enums;
using Checkers.BL.Helper;
using Checkers.BL.Models;

namespace Checkers.BL.Services.Tests
{
    [TestClass()]
    public class ValidateQueenServiceTests
    {
        [TestMethod()]
        public void CanTake_ByQueen_MultiVariants()
        {
            var actual = CreateValidateService().GetAllowedMoveVectors(0, "" +
                                                                      "Q111" +
                                                                      "1p11" +
                                                                      "1111" +
                                                                      "1111");

            var expected = new List<Vector>()
            {
                new Vector()
                {
                    Direction = Direction.RightBottom,
                    Length = 2,
                },
                new Vector()
                {
                    Direction = Direction.RightBottom,
                    Length = 3,
                },
            };
            CollectionAssert.AreEquivalent(expected, actual.Vectors);
            Assert.IsTrue(actual.EatFigure);
        }

        [TestMethod()]
        public void QueenCanMove_OnAllBoard()
        {
            var actual = CreateValidateService().GetAllowedMoveVectors(5, "" +
                                                                      "p111" +
                                                                      "1Q11" +
                                                                      "1111" +
                                                                      "1111");

            var expected = new List<Vector>()
            {
                new Vector()
                {
                    Direction = Direction.RightBottom,
                    Length = 1,
                },
                new Vector()
                {
                    Direction = Direction.RightBottom,
                    Length = 2,
                },
                new Vector()
                {
                    Direction = Direction.RightTop,
                    Length = 1,
                },
                new Vector()
                {
                    Direction = Direction.LeftBottom,
                    Length = 1,
                },
            };
            CollectionAssert.AreEquivalent(expected, actual.Vectors);
            Assert.IsFalse(actual.EatFigure);
        }
        [TestMethod()]
        public void QueenCanTake_OppositeFigure()
        {
            var actual = CreateValidateService().GetAllowedMoveVectors(3, "" +
                                                       "111Q11" +
                                                       "111111" +
                                                       "1p1111" +
                                                       "111111" +
                                                       "1p1111" +
                                                       "111111w");


            Assert.IsTrue(actual.EatFigure);
        }

        private ValidateQueenService CreateValidateService()
        {
            return new ValidateQueenService(new MathHelper(), new ColorHelper(), new VectorHelper());
        }
    }
}