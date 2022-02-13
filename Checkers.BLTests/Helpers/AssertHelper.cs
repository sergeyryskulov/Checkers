using System;
using System.Collections.Generic;
using System.Text;
using Checkers.BL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkers.UnitTests.Extensions
{
    public static class AssertHelper
    {
        public static void AllowedVectorsEquivalent(AllowedVectors allowedVectorsExpected, AllowedVectors allowedVectorsActual)
        {
            CollectionAssert.AreEquivalent(allowedVectorsExpected.Vectors, allowedVectorsActual.Vectors);

            Assert.AreEqual(allowedVectorsExpected.EatFigure, allowedVectorsActual.EatFigure);
        }
    }
}
