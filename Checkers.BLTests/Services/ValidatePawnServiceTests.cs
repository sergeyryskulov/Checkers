using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.BL.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Checkers.BL.Constants.Enums;
using Checkers.BL.Helper;
using Checkers.BL.Models;
using Checkers.UnitTests.Extensions;

namespace Checkers.BL.Services.Tests
{
    [TestClass()]
    public class ValidatePawnServiceTests
    {
        [TestMethod()]
        public void CannotMove_OverBoard_ForAllVariants()
        {
            var actual = CreateValidateService().GetAllowedMoveVectors(1, "" +
                                                                          "1P1" +
                                                                          "p11" +
                                                                          "111");
            var expected = new AllowedVectors()
            {
                EatFigure = false,
                Vectors = new List<Vector>()
            };
            AllowedVectorsAssert.AreEquivalent(expected, actual);
        }

        [TestMethod()]
        public void CannotMove_OverBoard_ForOneVariant()
        {
            var actual = CreateValidateService().GetAllowedMoveVectors(2, "11P1"); ;

            var expected = new AllowedVectors()
            {
                EatFigure = false,
                Vectors = new List<Vector>()
                {
                    new Vector()
                    {
                        Direction = Direction.RightTop,
                        Length = 1
                    }
                }
            };

            AllowedVectorsAssert.AreEquivalent(expected, actual);
        }

      
        [TestMethod()]
        public void CanTake_OppositeFigure()
        {
            var actual = CreateValidateService().GetAllowedMoveVectors(8, "" +
                                                                          "111" +
                                                                          "1p1" +
                                                                          "11P");

            var expected = new AllowedVectors()
            {
                EatFigure = true,
                Vectors = new List<Vector>()
                {
                    new Vector()
                    {
                        Direction = Direction.LeftTop,
                        Length = 2
                    }
                }
            };

            AllowedVectorsAssert.AreEquivalent(expected, actual);
        }

        [TestMethod()]
        public void CanTake_OppositeFigure_OnBackMove()
        {

            var actual = CreateValidateService().GetAllowedMoveVectors(0, "" +
                                                                      "P11" +
                                                                      "1p1" +
                                                                      "111");

            var expected = new AllowedVectors()
            {
                EatFigure = true,
                Vectors = new List<Vector>()
                {
                    new Vector()
                    {
                        Direction = Direction.RightBottom,
                        Length = 2
                    }
                }
            };
            AllowedVectorsAssert.AreEquivalent(expected, actual);
            
        }

        [TestMethod()]
        public void CannotMove_IfBlockedOnOtherFigureAndBoardBound()
        {

            var actual = CreateValidateService().GetAllowedMoveVectors(4, "" +
                                                                      "1p11" +
                                                                      "P111" +
                                                                      "1111" +
                                                                      "1111");

            var expected = new AllowedVectors()
            {
                EatFigure = false,
                Vectors = new List<Vector>()
            };

            AllowedVectorsAssert.AreEquivalent(expected, actual);
        }

        ValidatePawnService CreateValidateService()
        {
            return new ValidatePawnService();
        }
    }
}