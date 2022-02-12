using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.BL.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Checkers.BL.Constants.Enums;
using Checkers.BL.Helper;
using Checkers.BL.Models;

namespace Checkers.BL.Services.Tests
{
    [TestClass()]
    public class ValidatePawnServiceTests
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


        ValidatePawnService CreateValidateService()
        {
            return new ValidatePawnService(new MathHelper(), new ColorHelper(), new VectorHelper());
        }
    }
}