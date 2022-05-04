﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.BL.Constants;
using Ckeckers.DAL.Repositories;
using Moq;

namespace Checkers.BL.Services.Tests
{
    [TestClass()]
    public class IntellectServiceTests
    {

        [TestMethod()]
        public void PawnToQueen()
        {
            AssertEqual(
                "p1" +
                "11b",

                "11" +
                "1qB");

        }


        [TestMethod()]
        public void IntellectStep_NoWhiteMove()
        {
            AssertNotEqual(
                "1111111p" +
                "111111p1" +
                "1p111p1p" +
                "p11111p1" +
                "1p11111P" +
                "11111111" +
                "111P1111" +
                "11q11111b",

                "1111111p" +
                "111111p1" +
                "1p111p1p" +
                "p11111p1" +
                "1p11111P" +
                "11111111" +
                "111P1111" +
                "11q11111b");
        }

        [TestMethod()]
        public void TwoTurn()
        {

            AssertEqual(
                "111111" +
                "111111" +
                "111111" +
                "111111" +
                "1p1p11" +
                "P1P111w",

                "111111" +
                "111111" +
                "111111" +
                "111111" +
                "111111" +
                "11P1P1W");

        }

        [TestMethod()]
        public void IntellectStep_QueenWeightTest()
        {
            AssertEqual(
                "11Q" + 
                "111" + 
                "q11b",

                "11Q" +
                "1q1" +
                "111w"
            );

        }


        [TestMethod()]
        public void IntellectStepMustGoTest()
        {
            
            AssertNotEqual(
                "1p1p1p1p" +
                "p111p111" +
                "1111111p" +
                "1111p111" +
                "11111111" +
                "P11111P1" +
                "1P1P1p11" +
                "P1P1P1P1b53", 

                "1p1p1p1p" +
                "p111p111" +
                "1111111p" +
                "1111p111" +
                "11111111" +
                "P11111P1" +
                "1P1P1p11" +
                "P1P1P1P1b53");

        }

        private IntellectService GetIntellectService(IBoardRepository boardRepository)
        {
            var moveService = new DirectMoveService(
                new ValidateFiguresService(
                    new ValidateFigureService(
                    
                    new ValidatePawnService(),
                    new ValidateQueenService(                                                                        )
                    ))
            );

            return new IntellectService(new ValidateFiguresService(
                new ValidateFigureService(
                    new ValidatePawnService(),
                    new ValidateQueenService()
                    )),
                boardRepository,                
                moveService                              
            );
        }

        private void AssertEqual(string from, string to)
        {
            var boardRepository = new Mock<IBoardRepository>();
            boardRepository.Setup(m => m.Load("")).Returns(from);

            var service = GetIntellectService(boardRepository.Object);

            string actual = service.IntellectStep("");

            string expected = to;

            Assert.AreEqual(expected, actual);
        }

        private void AssertNotEqual(string from, string to)
        {
            var boardRepository = new Mock<IBoardRepository>();
            boardRepository.Setup(m => m.Load("")).Returns(from);

            var service = GetIntellectService(boardRepository.Object);

            string actual = service.IntellectStep("");

            string expected = to;

            Assert.AreNotEqual(expected, actual);
        }
    }
}