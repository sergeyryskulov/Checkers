using System.Collections.Generic;
using Checkers.DomainModels;
using Checkers.DomainModels.Models;
using Checkers.Rules.Enums;
using Checkers.Rules.Models;
using Checkers.Rules.Services;
using Checkers.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkers.Rules.Services.Tests
{
    [TestClass()]
    public class ValidatePawnServiceTests
    {
        [TestMethod()]
        public void CannotMove_OverBoard_ForAllVariants()
        {
            var actual = CreateValidateService().GetAllowedMoveVectors(new Board("" +
                "1P1" +
                "p11" +
                "111"), 1);
            var expected = new AllowedVectors();
            AllowedVectorsAssert.AreEquivalent(expected, actual);
        }

        [TestMethod()]
        public void CannotMove_OverBoard_ForOneVariant()
        {
            var actual = CreateValidateService().GetAllowedMoveVectors(new Board("11P1"), 2); ;

            var expected = new AllowedVectors(
                new List<Vector>()
                {
                    new Vector(Direction.RightTop, 1)
                },
                false);

            AllowedVectorsAssert.AreEquivalent(expected, actual);
        }


        [TestMethod()]
        public void CanTake_OppositeFigure()
        {
            var actual = CreateValidateService().GetAllowedMoveVectors(new Board("" +
                "111" +
                "1p1" +
                "11P"), 8);

            var expected = new AllowedVectors(
                new List<Vector>()
                {
                    new Vector(Direction.LeftTop, 2)
                },
                true);

            AllowedVectorsAssert.AreEquivalent(expected, actual);
        }

        [TestMethod()]
        public void CanTake_OppositeFigure_OnBackMove()
        {

            var actual = CreateValidateService().GetAllowedMoveVectors(new Board("" +
                "P11" +
                "1p1" +
                "111"), 0);

            var expected = new AllowedVectors(
                new List<Vector>()
                {
                    new Vector(Direction.RightBottom, 2)
                },
                true);

            AllowedVectorsAssert.AreEquivalent(expected, actual);

        }

        [TestMethod()]
        public void CannotMove_IfBlockedOnOtherFigureAndBoardBound()
        {

            var actual = CreateValidateService().GetAllowedMoveVectors(new Board("" +
                "1p11" +
                "P111" +
                "1111" +
                "1111"), 4);

            var expected = new AllowedVectors();

            AllowedVectorsAssert.AreEquivalent(expected, actual);
        }

        ValidatePawnService CreateValidateService()
        {
            return new ValidatePawnService();
        }
    }
}
