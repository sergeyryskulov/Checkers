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
using Moq;

namespace Checkers.BL.Services.Tests
{
    [TestClass()]
    public class ValidateServiceTests
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
            return new ValidateService(new VectorHelper(), new MathHelper(), new ColorHelper(), new Mock<IValidatePawnService>().Object);
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