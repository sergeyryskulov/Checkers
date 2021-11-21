using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var service = CreateValidateService();

            var expected = new List<Vector>()
            {
                new Vector()
                {
                    Direction = Direction.RightTop,
                    Length = 1
                }
            };

            var actual = service.GetAllowedMoveVectors(2, "11P1", out var isDie);

            CollectionAssert.AreEquivalent(expected, actual);

            Assert.IsFalse(isDie);
        }

        [TestMethod()]
        public void CanTake_OppositeFigure()
        {

            var actual = CreateValidateService().GetAllowedMoveVectors(8, "" +
                                                                          "111" +
                                                                          "1p1" +
                                                                          "11P",
                out var takeOppositeFigure);

            var expected = new List<Vector>()
            {
                new Vector()
                {
                    Direction = Direction.LeftTop,
                    Length = 2
                }
            };

            CollectionAssert.AreEquivalent(expected, actual);
            Assert.IsTrue(takeOppositeFigure);

        }

        [TestMethod()]
        public void CanTake_OppositeFigure_OnBackMove()
        {

            var actual = CreateValidateService().GetAllowedMoveVectors(0, "" +
                                                                      "p11" +
                                                                      "1P1" +
                                                                      "111", out var isDie);

            var expected = new List<Vector>()
            {
                new Vector()
                {
                    Direction = Direction.RightBottom,
                    Length = 2
                }
            };
            CollectionAssert.AreEquivalent(expected, actual);
            Assert.IsTrue(isDie);
        }

        [TestMethod()]
        public void GetAllowedVectors_Blocked_Error()
        {

            var actual = CreateValidateService().GetAllowedMoveVectors(0, "" +
                                                                      "P111" +
                                                                      "1111" +
                                                                      "1p11" +
                                                                      "P111", out var isDie);

            var expected = new List<Vector>()
            {

            };
            CollectionAssert.AreEquivalent(expected, actual);
            Assert.IsFalse(isDie);
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

            var actualLength= CreateValidateService().GetAllowedMoveVectors(8, figures, out var isDie).Count;

            Assert.IsTrue(actualLength > 0);
            Assert.IsFalse(isDie);
        }


        [TestMethod()]
        public void GetAllowedVectorsBackTest()
        {
            var actual = CreateValidateService().GetAllowedMoveVectors(0, "" +
                                                                      "P11" +
                                                                      "1p1" +
                                                                      "111",
                out var isDie);

            var expected = new List<Vector>()
            {
                new Vector()
                {
                    Direction = Direction.RightBottom,
                    Length = 2
                }
            };


            CollectionAssert.AreEquivalent(expected, actual);
            Assert.IsTrue(isDie);
        }


        [TestMethod()]
        public void GetAllowedVectorsQueen_DieCorrect()
        {
            var actual = CreateValidateService().GetAllowedMoveVectors(0, "" +
                                                                      "Q111" +
                                                                      "1p11" +
                                                                      "1111" +
                                                                      "1111",
                out var isDie);

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
            CollectionAssert.AreEquivalent(expected, actual);
            Assert.IsTrue(isDie);
        }

        [TestMethod()]
        public void GetAllowedVectorsQueen_NotDieCorrect()
        {
            var actual = CreateValidateService().GetAllowedMoveVectors(5, "" +
                                                                      "p111" +
                                                                      "1Q11" +
                                                                      "1111" +
                                                                      "1111",
                out var isDie);

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
            CollectionAssert.AreEquivalent(expected, actual);
            Assert.IsFalse(isDie);
        }
        [TestMethod()]
        public void GetAllowedVectorsQueen_DieExists()
        {
            CreateValidateService().GetAllowedMoveVectors(3, "" +
                                                       "111Q11" +
                                                       "111111" +
                                                       "1p1111" +
                                                       "111111" +
                                                       "1p1111" +
                                                       "111111w",
                out var isDie);

            
            Assert.IsTrue(isDie);
        }

        private ValidateService CreateValidateService()
        {
            return new ValidateService(new VectorHelper(), new MathHelper(), new ColorHelper());
        }

    }
}