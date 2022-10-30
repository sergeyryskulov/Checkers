using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Checkers.BL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkers.UnitTests.Extensions
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
