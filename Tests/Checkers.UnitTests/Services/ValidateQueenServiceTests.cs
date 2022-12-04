using System.Collections.Generic;
using Checkers.Core.Models.ValueObjects;
using Checkers.DomainModels;
using Checkers.Rules.Enums;
using Checkers.Rules.Models;
using Checkers.Rules.Services;
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
            var actual = CreateValidateService().GetAllowedMoveVectors(new Board("" +
                "Q111" +
                "1p11" +
                "1111" +
                "1111"), 0);

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
            var actual = CreateValidateService().GetAllowedMoveVectors(new Board("" +
                "p111" +
                "1Q11" +
                "1111" +
                "1111"), 5);

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
            var actual = CreateValidateService().GetAllowedMoveVectors(new Board( "" +
                "111Q11" +
                "111111" +
                "1p1111" +
                "111111" +
                "1p1111" +
                "111111"), 3);

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
            var actual = CreateValidateService().GetAllowedMoveVectors(new Board("" +
                "111111" +
                "1111Q1" +
                "111p11" +
                "111111" +
                "1p1111" +
                "111111"), 10);


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
            var actual = CreateValidateService().GetAllowedMoveVectors(new Board("" +
                "111p1p" +
                "1111Q1" +
                "111p1p" +
                "11p111" +
                "111111" +
                "111111"), 10);


            var expected = new AllowedVectors();

            AllowedVectorsAssert.AreEquivalent(expected, actual);
        }

    }
}