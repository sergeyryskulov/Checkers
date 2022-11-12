using System.Collections.Generic;
using Checkers.Core.Constants.Enums;
using Checkers.Core.Models.Aggregates;
using Checkers.Core.Models.ValueObjects;
using Checkers.Core.Services;
using Checkers.DomainModels.Aggregates;
using Checkers.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkers.UnitTests.Services
{
    [TestClass()]
    public class ValidateQueenServiceTests
    {
        [TestMethod()]
        public void CanTake_MultiVariantsAfter()
        {
            var actual = CreateValidateService().GetAllowedMoveVectors(0, new Cells("" +
                                                                      "Q111" +
                                                                      "1p11" +
                                                                      "1111" +
                                                                      "1111"));

            var expected = new AllowedVectors(
                 new List<Vector>()
                {
                    new Vector(Direction.RightBottom, 2),
                    new Vector(Direction.RightBottom, 3),
                },
                true
            );

            AllowedVectorsAssert.AreEquivalent(expected, actual);
            
        }

        [TestMethod()]
        public void CanMove_OnAllBoard()
        {
            var actual = CreateValidateService().GetAllowedMoveVectors(5, new Cells("" +
                                                                      "p111" +
                                                                      "1Q11" +
                                                                      "1111" +
                                                                      "1111"));

            var expected = new AllowedVectors(
                new List<Vector>(){
                    new Vector(Direction.RightBottom, 1),
                    new Vector(Direction.RightBottom, 2),
                    new Vector(Direction.RightTop, 1),
                    new Vector(Direction.LeftBottom, 1)
                },
                false);

            AllowedVectorsAssert.AreEquivalent(expected, actual);
        }
        [TestMethod()]
        public void CanTake_OppositeFigure()
        {
            var actual = CreateValidateService().GetAllowedMoveVectors(3, new Cells( "" +
                                                       "111Q11" +
                                                       "111111" +
                                                       "1p1111" +
                                                       "111111" +
                                                       "1p1111" +
                                                       "111111"));

            var expected = new AllowedVectors(
                new List<Vector>(){
                    new Vector(Direction.LeftBottom, 3)
                },
                true
            );

            AllowedVectorsAssert.AreEquivalent(expected, actual);
        }

        private ValidateQueenService CreateValidateService()
        {
            return new ValidateQueenService();
        }

        [TestMethod()]
        public void CannotTake_TwoFiguresOnOneSimpleStep()
        {
            var actual = CreateValidateService().GetAllowedMoveVectors(10,  new Cells("" +
                                                                          "111111" +
                                                                          "1111Q1" +
                                                                          "111p11" +
                                                                          "111111" +
                                                                          "1p1111" +
                                                                          "111111"));


            var expected = new AllowedVectors(
                new List<Vector>(){
                    new Vector(Direction.LeftBottom, 2)
                },
                true
            );

            AllowedVectorsAssert.AreEquivalent(expected, actual);
        }

        [TestMethod()]
        public void CannotTake_TwoNearFiguresOnOneSimpleStep()
        {
            var actual = CreateValidateService().GetAllowedMoveVectors(10, new Cells("" +
                                                                          "111p1p" +
                                                                          "1111Q1" +
                                                                          "111p1p" +
                                                                          "11p111" +
                                                                          "111111" +
                                                                          "111111"));


            var expected = new AllowedVectors();

            AllowedVectorsAssert.AreEquivalent(expected, actual);
        }

    }
}