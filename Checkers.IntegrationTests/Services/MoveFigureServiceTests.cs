using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ckeckers.DAL.Repositories;
using Moq;

namespace Checkers.BL.Services.Tests
{
    [TestClass()]
    public class MoveFigureServiceTests
    {
        [TestMethod()]
        public void SimpleMoveCorrect()
        {
            var moveService = CreateMoveFigureService();

            string actual = moveService.Move("p111b", 0, 3);

            var expected = "111qB";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CannotMoveToSelf()
        {
            var service = CreateMoveFigureService();

            var actual = service.Move("p111b", 0, 0);

            var expected = "p111b";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Queen_CannotMoveToSelf()
        {
            var service = CreateMoveFigureService();

            string actual = service.Move("q111b", 0, 0);

            var expected = "q111b";

            Assert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void Die_Correct()
        {

            var expected = "" +
                           "11Q" +
                           "111" +
                           "111W";

            var service = CreateMoveFigureService();

            string actual = service.Move("" +
                                         "111" +
                                         "1p1" +
                                         "P11w", 6, 2);

            Assert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void Block_Correct()
        {


            var expected = "" +
                           "11Q" +
                           "111" +
                           "111W";


            var service = CreateMoveFigureService();

            string actual = service.Move("" +
                                         "111" +
                                         "1p1" +
                                         "P11w", 6, 2);

            Assert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void Move_NotToggleTurm()
        {

            var expected = "" +
                           "111111" +
                           "111111" +
                           "P11p11" +
                           "11P111" +
                           "111111" +
                           "111111w20";

            var service = CreateMoveFigureService();

            var actual = service.Move("" +
                                      "111111" +
                                      "111111" +
                                      "P11p11" +
                                      "111111" +
                                      "1p1111" +
                                      "P11111w", 30, 20);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Move_NotToggleTurn2()
        {

            var expected = "" +
                           "111111" +
                           "111111" +
                           "111111" +
                           "Q11111" +
                           "1p1111" +
                           "111111w18";

            var service = CreateMoveFigureService();

            var actual = service.Move("" +
                                      "111Q11" +
                                      "111111" +
                                      "1p1111" +
                                      "111111" +
                                      "1p1111" +
                                      "111111w", 3, 18);
            Assert.AreEqual(expected, actual);
        }

        private MoveFigureService CreateMoveFigureService()
        {
            return new MoveFigureService( 
                new ValidateService(
                    new ValidatePawnService(),
                    new ValidateQueenService()
                ));
        }

        [TestMethod()]
        public void CannotMove_BlockByOther()
        {

            var expected = "" +
                           "111111" +
                           "1p1111" +
                           "P11111" +
                           "11P111" +
                           "111111" +
                           "111111w20";

            var service = CreateMoveFigureService();

            var actual = service.Move("" +
                                      "111111" +
                                      "1p1111" +
                                      "P11111" +
                                      "11P111" +
                                      "111111" +
                                      "111111w20", 12, 2);
            Assert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void CannotMove_OtherColor()
        {

            var actual = CreateMoveFigureService().Move("" +
                                                        "111" +
                                                        "1p1" +
                                                        "P11b", 6, 2);
            var expected = "" +
                           "111" +
                           "1p1" +
                           "P11b";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CannotMove_IncorrectStep()
        {

            var actual = CreateMoveFigureService().Move("" +
                                                        "111" +
                                                        "1p1" +
                                                        "P11w", 6, 4);
            var expected = "" +
                           "111" +
                           "1p1" +
                           "P11w";

            Assert.AreEqual(expected, actual);
        }
    }
}