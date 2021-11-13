﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        private WhitePawnService _whitePawnService = new WhitePawnService(new VectorHelper(), new MathHelper());

        [TestMethod()]
        public void GetAllowedVectors_OneStepTop_Correct()
        {

            var actual= _whitePawnService.GetAllowedVectors(2, "11p1");

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

            var actual = _whitePawnService.GetAllowedVectors(8, ""+ 
                                                                  "111"+ 
                                                                  "1P1" +
                                                                  "11p");

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
    }
}