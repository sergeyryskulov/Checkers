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
    public class ValidateServiceTests
    {

        [TestMethod()]
        public void CanMove_OnlyRightTop_OnSmallBoard()
        {
            var validateService = CreateValidateService();

            var expected = new List<Vector>()
            {
                new Vector()
                {
                    Direction = Direction.RightTop,
                    Length = 1
                }
            };

            var actual = validateService.GetAllowedMoveVectors(2, "11P1");

            CollectionAssert.AreEquivalent(expected, actual.Vectors);

            Assert.IsFalse(actual.EatFigure);
        }

        [TestMethod()]
        public void CanTake_OppositeFigure()
        {
            var service = CreateValidateService();

            var expected = new List<Vector>()
            {
                new Vector()
                {
                    Direction = Direction.LeftTop,
                    Length = 2
                }
            };

            var actual = service.GetAllowedMoveVectors(8, "" +
                                                                          "111" +
                                                                          "1p1" +
                                                                          "11P");


            CollectionAssert.AreEquivalent(expected, actual.Vectors);
            Assert.IsTrue(actual.EatFigure);

        }

        [TestMethod()]
        public void CanTake_OppositeFigure_OnBackMove()
        {

            var actual = CreateValidateService().GetAllowedMoveVectors(0, "" +
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
            CollectionAssert.AreEquivalent(expected, actual.Vectors);
            Assert.IsTrue(actual.EatFigure);
        }

        [TestMethod()]
        public void CannotMove_IfBlockedOnOtherFigureAndBoardBound()
        {

            var actual = CreateValidateService().GetAllowedMoveVectors(4, "" +
                                                                      "1p11" +
                                                                      "P111" +
                                                                      "1111" +
                                                                      "1111");

            var expected = new List<Vector>()
            {
            };

            CollectionAssert.AreEquivalent(expected, actual.Vectors);
            Assert.IsFalse(actual.EatFigure);
        }


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
            var actual= CreateValidateService().GetAllowedMoveVectors(3, "" +
                                                       "111Q11" +
                                                       "111111" +
                                                       "1p1111" +
                                                       "111111" +
                                                       "1p1111" +
                                                       "111111w");


            Assert.IsTrue(actual.EatFigure);
        }

        private ValidateService CreateValidateService()
        {
            return new ValidateService(new VectorHelper(), new MathHelper(), new ColorHelper(), new ValidatePawnService(new MathHelper(), new ColorHelper(), new VectorHelper()));
        }

        [TestMethod()]
        public void GetAllowedMoveVectorsIncorrectFigureTest()
        {
            var actual = CreateValidateService().GetAllowedMoveVectors(0, "G111");

            Assert.AreEqual(actual.Vectors.Count, 0);
            Assert.IsFalse(actual.EatFigure);
        }
    }
}