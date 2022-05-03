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
        public void PawnToQueen()
        {
            AssertMove(
                "p1" +
                "11b",
                0, 3,
                "11" +
                "1qB");
        }

        [TestMethod()]
        public void PawnCannotMoveToSelf()
        {
            AssertMove(
                "p1" +
                "11b",
                0, 0,
                "p1" +
                "11b");
        }

        [TestMethod()]
        public void QueenCannotMoveToSelf()
        {
            AssertMove(
                "q1" +
                "11b",
                0, 0,
                "q1" +
                "11b"
                );
        }


        [TestMethod()]
        public void PawnEatPawn()
        {
            AssertMove(
                "111" + 
                "1p1" + 
                "P11w",
                6, 2,
                "11Q" +
                "111" +
                "111W"
            );
        }


        [TestMethod()]
        public void TurnNotToggledAfterPawnMove()
        {
            AssertMove(
                "111111" +
                "111111" +
                "P11p11" +
                "111111" +
                "1p1111" +
                "P11111w",
                30, 20,
                "" +
                "111111" +
                "111111" +
                "P11p11" +
                "11P111" +
                "111111" +
                "111111w20");
        }

        [TestMethod()]
        public void TurnNotToggleAfterQueenMove()
        {
            AssertMove(
                "111Q11" +
                "111111" +
                "1p1111" +
                "111111" +
                "1p1111" +
                "111111w",
                3, 18,
                "111111" +
                "111111" +
                "111111" +
                "Q11111" +
                "1p1111" +
                "111111w18"

            );
        }

        private MoveFigureService CreateMoveFigureService()
        {
            return new MoveFigureService( 
                new ValidateFiguresService(
                    new ValidateFigureService(
                    new ValidatePawnService(),
                    new ValidateQueenService())
                ),
                new DirectMoveService(new ValidateFiguresService(
                    new ValidateFigureService(
                        new ValidatePawnService(),
                        new ValidateQueenService())
                ))
                
                );
        }

        private void AssertMove(string from, int fromCoord, int toCoord, string expected)
        {
            var moveService = CreateMoveFigureService();

            string actual = moveService.Move(from, fromCoord, toCoord);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void BlockedByOtherFigureThatMustMove()
        {
            AssertMove(
                "111111" +
                "1p1111" +
                "P11111" +
                "11P111" +
                "111111" +
                "111111w20",
                12, 2,
                "111111" +
                "1p1111" +
                "P11111" +
                "11P111" +
                "111111" +
                "111111w20");
        }


        [TestMethod()]
        public void CannotMoveOnOtherColorTurn()
        {
            AssertMove(
                "111" +
                "1p1" +
                "P11b",
                6, 2,
                "111" +
                "1p1" +
                "P11b"
                );
        }

        [TestMethod()]
        public void CannotMoveToOtherColorPawnPosition()
        {

            AssertMove(
                "111" +
                "1p1" +
                "P11w",
                6, 4,
                "111" +
                "1p1" +
                "P11w");
        }
    }
}