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
    public class MoveFigureServiceTests
    {


        private MoveFigureService GetMoveFigureService(IBoardRepository boardRepository)
        {
            return new MoveFigureService(boardRepository, new VectorHelper(), new MathHelper(), 
                new PawnService(new VectorHelper(), new MathHelper(), new ColorHelper()),
                new ColorHelper());
        }

        [TestMethod()]
        public void Move_Correct()
        {

            var boardRepository = new Mock<IBoardRepository>();
            boardRepository.Setup(m => m.Load("")).Returns("p111b");

            var service = GetMoveFigureService(boardRepository.Object);

            string result = service.Move(0, 3, "");


            Assert.IsTrue(result == "111qw");
        }

        [TestMethod()]
        public void Die_Correct()
        {

            var boardRepository = new Mock<IBoardRepository>();
            boardRepository.Setup(m => m.Load("")).Returns("" +
                                                           "111" +
                                                           "1p1" +
                                                           "P11w");

            var expected = "" +
                           "11Q" +
                           "111" +
                           "111b";

            var service = GetMoveFigureService(boardRepository.Object);

            string actual = service.Move(6, 2, "");

            Assert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void Block_Correct()
        {

            var boardRepository = new Mock<IBoardRepository>();
            boardRepository.Setup(m => m.Load("")).Returns("" +

                                                           "111" +
                                                           "1p1" +
                                                           "P11w");

            var expected = "" +
                           "11Q" +
                           "111" +
                           "111b";


            var service = GetMoveFigureService(boardRepository.Object);

            string actual = service.Move(6, 2, "");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Move_NotToggleTurm()
        {
            var boardRepository = new Mock<IBoardRepository>();
            boardRepository.Setup(m => m.Load("")).Returns("" +

                                                           "111111" +
                                                           "111111" +
                                                           "P11p11" +
                                                           "111111" +
                                                           "1p1111" +
                                                           "P11111w");

            var expected = "" +
                         "111111" +
                         "111111" +
                         "P11p11" +
                         "11P111" +
                         "111111" +
                         "111111w";

            var service = GetMoveFigureService(boardRepository.Object);

            var actual = service.Move(30, 20, "");
            Assert.AreEqual(expected, actual);
        }
    }
}