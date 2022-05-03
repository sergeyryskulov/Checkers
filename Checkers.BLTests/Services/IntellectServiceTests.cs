using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.BL.Constants;
using Checkers.BL.Helper;
using Ckeckers.DAL.Repositories;
using Moq;

namespace Checkers.BL.Services.Tests
{
    [TestClass()]
    public class IntellectServiceTests
    {

        
        [TestMethod()]
        public void IntellectStepTest()
        {
            var boardRepository = new Mock<IBoardRepository>();
            boardRepository.Setup(m => m.Load("")).Returns("p111b");

            var service = GetIntellectService(boardRepository.Object);

            string actual = service.IntellectStep("");

            string expected = "111qB";
            
            Assert.AreEqual(expected, actual);

        }


        [TestMethod()]
        public void IntellectStep_NoWhiteMove()
        {
            var boardRepository = new Mock<IBoardRepository>();
            boardRepository.Setup(m => m.Load("")).Returns("1111111p111111p11p111p1pp11111p11p11111P11111111111P111111q11111b");

            var service = GetIntellectService(boardRepository.Object);

            string actual = service.IntellectStep("");

            string notExpected = "1111111p111111p11p111p1pp11111p11p11111P11111111111P111111q11111b";

            Assert.AreNotEqual(notExpected, actual);

        }

        [TestMethod()]
        public void IntellectStepOneDeepTest()
        {
            var boardRepository = new Mock<IBoardRepository>();
            boardRepository.Setup(m => m.Load("")).Returns("" +
                                                           "" +
                                                           "111111" +
                                                           "111111" +
                                                           "111111" +
                                                           "111111" +
                                                           "1p1p11" +
                                                           "P1P111w");

            var service = GetIntellectService(boardRepository.Object);

            string actual = service.IntellectStep("");

            string expected = "" +
                              "111111" +
                              "111111" +
                              "111111" +
                              "111111" +
                              "111111" +
                              "11P1P1W";

            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void IntellectStep_QueenWeightTest()
        {
            var boardRepository = new Mock<IBoardRepository>();
            boardRepository.Setup(m => m.Load("")).Returns("" +
                                                           "11Q" +
                                                           "111" +
                                                           "q11b");

            var service = GetIntellectService(boardRepository.Object);

            string actual = service.IntellectStep("");

            string expected = "" +
                              "11Q" +
                              "1q1" +
                              "111w";

            Assert.AreEqual(expected, actual);

        }



        [TestMethod()]
        public void IntellectStepMustGoTest()
        {
            var boardRepository = new Mock<IBoardRepository>();
            boardRepository.Setup(m => m.Load("")).Returns("1p1p1p1pp111p1111111111p1111p11111111111P11111P11P1P1p11P1P1P1P1b53");

            var service = GetIntellectService(boardRepository.Object);

            string actual = service.IntellectStep("");

            string notExpected = "1p1p1p1pp111p1111111111p1111p11111111111P11111P11P1P1p11P1P1P1P1b53";

            Assert.AreNotEqual(notExpected, actual);

        }

        private IntellectService GetIntellectService(IBoardRepository boardRepository)
        {
            var moveService = new MoveFigureService(
                new ValidateService( new ValidatePawnService(),
                    new ValidateQueenService(                                                                        )
                    ));

            return new IntellectService(new ValidateService(
                    
                    new ValidatePawnService(),
                    new ValidateQueenService()
                    ),
                boardRepository,                
                moveService                              
            );
        }
    }
}