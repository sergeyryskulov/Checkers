using System.Linq;
using Checkers.Rules.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkers.UnitTests.Helpers
{
    public static class AllowedVectorsAssert
    {
        public static void AreEquivalent(AllowedVectors allowedVectorsExpected, AllowedVectors allowedVectorsActual)
        {
            CollectionAssert.AreEquivalent(allowedVectorsExpected.Vectors.ToList(), allowedVectorsActual.Vectors.ToList());

            Assert.AreEqual(allowedVectorsExpected.EatFigure, allowedVectorsActual.EatFigure);
        }
    }
}
