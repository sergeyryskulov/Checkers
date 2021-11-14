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
    public class QueenServiceTests
    {
        private QueenService _queenService;



        public QueenServiceTests()
        {
            _queenService = new QueenService(new VectorHelper(), new MathHelper(), new ColorHelper());
            
        }

        [TestMethod()]
        public void GetAllowedVectors_DieCorrect()
        {
            var actual= _queenService.GetAllowedVectors(0, "" +
                                               "Q111" +
                                               "1p11" +
                                               "1111" +
                                               "1111");

            var expected = new List<Vector>()
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
            };
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod()]
        public void GetAllowedVectors_NotDieCorrect()
        {
            var actual = _queenService.GetAllowedVectors(5, "" +
                                                            "p111" +
                                                            "1Q11" +
                                                            "1111" +
                                                            "1111");

            var expected = new List<Vector>()
            {
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
            };
            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}