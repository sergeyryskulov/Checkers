using System.Collections.Generic;
using Checkers.Core.Constants.Enums;
using Checkers.Core.Models.Aggregates;
using Checkers.Core.Models.ValueObjects;
using Checkers.Core.Services;
using Checkers.DomainModels;
using Checkers.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkers.UnitTests.Services
{
    [TestClass()]
    public class ValidatePawnServiceTests
    {
        [TestMethod()]
        public void CannotMove_OverBoard_ForAllVariants()
        {
            var actual = CreateValidateService().GetAllowedMoveVectors(1, new Board("" +
                                                                          "1P1" +
                                                                          "p11" +
                                                                          "111"));
            var expected = new AllowedVectors();
            AllowedVectorsAssert.AreEquivalent(expected, actual);
        }

        [TestMethod()]
        public void CannotMove_OverBoard_ForOneVariant()
        {
            var actual = CreateValidateService().GetAllowedMoveVectors(2, new Board("11P1")); ;

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
            var actual = CreateValidateService().GetAllowedMoveVectors(8, new Board( "" +
                                                                          "111" +
                                                                          "1p1" +
                                                                          "11P"));

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

            var actual = CreateValidateService().GetAllowedMoveVectors(0, new Board( "" +
                                                                      "P11" +
                                                                      "1p1" +
                                                                      "111"));

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

            var actual = CreateValidateService().GetAllowedMoveVectors(4, new Board("" +
                                                                      "1p11" +
                                                                      "P111" +
                                                                      "1111" +
                                                                      "1111"));

            var expected = new AllowedVectors();

            AllowedVectorsAssert.AreEquivalent(expected, actual);
        }

        ValidatePawnService CreateValidateService()
        {
            return new ValidatePawnService();
        }
    }
}