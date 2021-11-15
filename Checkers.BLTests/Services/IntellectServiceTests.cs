using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var moveService = new MoveFigureService(new VectorHelper(), new MathHelper(),
                new ValidateService(new VectorHelper(), new MathHelper(), new ColorHelper()),
                new ColorHelper());

            return new IntellectService(new ValidateService(new VectorHelper(), new MathHelper(), new ColorHelper()),
                boardRepository,
                new ColorHelper(),
                moveService,
                new VectorHelper(),
                new MathHelper()

            );
        }
    }
}