using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.BL.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Checkers.BL.Constants.Enums;
using Checkers.BL.Helper;
using Checkers.BL.Models;
using Checkers.UnitTests.Extensions;

namespace Checkers.BL.Services.Tests
{
    [TestClass()]
    public class ValidateQueenServiceTests
    {
        [TestMethod()]
        public void CanTake_MultiVariantsAfter()
        {
            var actual = CreateValidateService().GetAllowedMoveVectors(0, "" +
                                                                      "Q111" +
                                                                      "1p11" +
                                                                      "1111" +
                                                                      "1111");

            var expected = new AllowedVectors()
            {
                EatFigure = true,
                Vectors = new List<Vector>()
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
                },
            };

            AssertHelper.AllowedVectorsEquivalent(expected, actual);
            
        }

        [TestMethod()]
        public void CanMove_OnAllBoard()
        {
            var actual = CreateValidateService().GetAllowedMoveVectors(5, "" +
                                                                      "p111" +
                                                                      "1Q11" +
                                                                      "1111" +
                                                                      "1111");

            var expected = new AllowedVectors()
            {
                EatFigure = false,
                Vectors = new List<Vector>(){
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
                },

            };

            AssertHelper.AllowedVectorsEquivalent(expected, actual);
        }
        [TestMethod()]
        public void CanTake_OppositeFigure()
        {
            var actual = CreateValidateService().GetAllowedMoveVectors(3, "" +
                                                       "111Q11" +
                                                       "111111" +
                                                       "1p1111" +
                                                       "111111" +
                                                       "1p1111" +
                                                       "111111");

            var expected = new AllowedVectors()
            {
                EatFigure = true,
                Vectors = new List<Vector>(){
                    new Vector()
                    {
                        Direction = Direction.LeftBottom,
                        Length = 3,
                    },
                  
                },

            };
            AssertHelper.AllowedVectorsEquivalent(expected, actual);
        }

        private ValidateQueenService CreateValidateService()
        {
            return new ValidateQueenService(new MathHelper(), new ColorHelper(), new VectorHelper());
        }

        [TestMethod()]
        public void CannotTake_TwoFiguresOnOneSimpleStep()
        {
            var actual = CreateValidateService().GetAllowedMoveVectors(10, "" +
                                                                          "111111" +
                                                                          "1111Q1" +
                                                                          "111p11" +
                                                                          "111111" +
                                                                          "1p1111" +
                                                                          "111111");


            var expected = new AllowedVectors()
            {
                EatFigure = true,
                Vectors = new List<Vector>(){
                    new Vector()
                    {
                        Direction = Direction.LeftBottom,
                        Length = 2,
                    }
                }
            };

            AssertHelper.AllowedVectorsEquivalent(expected, actual);
        }

        [TestMethod()]
        public void CannotTake_TwoNearFiguresOnOneSimpleStep()
        {
            var actual = CreateValidateService().GetAllowedMoveVectors(10, "" +
                                                                          "111p1p" +
                                                                          "1111Q1" +
                                                                          "111p1p" +
                                                                          "11p111" +
                                                                          "111111" +
                                                                          "111111");


            var expected = new AllowedVectors()
            {
                EatFigure = false,
                Vectors = new List<Vector>()
            };

            AssertHelper.AllowedVectorsEquivalent(expected, actual);
        }

    }
}