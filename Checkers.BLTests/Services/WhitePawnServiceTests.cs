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

namespace Checkers.BL.Services.Tests
{
    [TestClass()]
    public class WhitePawnServiceTests
    {
        private PawnService _pawnService = new PawnService(new VectorHelper(), new MathHelper(), new ColorHelper());

        [TestMethod()]
        public void GetAllowedVectors_OneStepTop_Correct()
        {
            
            var actual= _pawnService.GetAllowedVectors(2, "11P1");

            var expected = new List<Vector>()
            {
                new Vector()
                {
                    Direction = Direction.RightTop,
                    Length = 1
                }
            };
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod()]
        public void GetAllowedVectors_TwoStepTop_Correct()
        {

            var actual = _pawnService.GetAllowedVectors(8, ""+ 
                                                                  "111"+ 
                                                                  "1p1" +
                                                                  "11P");

            var expected = new List<Vector>()
            {
                new Vector()
                {
                    Direction = Direction.LeftTop,
                    Length = 2
                }
            };
            
            CollectionAssert.AreEquivalent(expected, actual);
            
        }

        [TestMethod()]
        public void GetAllowedVectors_TwoStepBottom_Correct()
        {

            var actual = _pawnService.GetAllowedVectors(0, "" +
                                                           "p11" +
                                                           "1P1" +
                                                           "111");

            var expected = new List<Vector>()
            {
                new Vector()
                {
                    Direction = Direction.RightBottom,
                    Length = 2
                }
            };
            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}