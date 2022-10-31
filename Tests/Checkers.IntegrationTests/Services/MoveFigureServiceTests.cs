using Checkers.Core.Constants;
using Checkers.Core.Models.ValueObjects;
using Checkers.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkers.IntegrationTests.Services
{
    [TestClass()]
    public class MoveFigureServiceTests
    {

        [TestMethod()]
        public void PawnToQueen()
        {
            AssertMove(
                new BoardState(
                "p1" +
                "11",
                Turn.Black),
                0, 3,
                new BoardState(
                "11" +
                "1q",
                Turn.BlackWin));
        }

        [TestMethod()]
        public void PawnCannotMoveToSelf()
        {
            AssertMove(
                new BoardState(
                "p1" +
                "11",
                Turn.Black),
                0, 0,
                new BoardState(
                "p1" +
                "11",
                Turn.Black));
        }

        [TestMethod()]
        public void QueenCannotMoveToSelf()
        {
            AssertMove(
                new BoardState(
                "q1" +
                "11",
                Turn.Black),
                0, 0,
                new BoardState(
                "q1" +
                "11",
                Turn.Black));
        }


        [TestMethod()]
        public void PawnEatPawn()
        {
            AssertMove(
                new BoardState(
                "111" + 
                "1p1" + 
                "P11",
                Turn.White),
                6, 2,
                new BoardState(
                "11Q" +
                "111" +
                "111",
                Turn.WhiteWin)
            );
        }


        [TestMethod()]
        public void TurnNotToggledAfterPawnMove()
        {
            AssertMove(
                new BoardState(
                "111111" +
                "111111" +
                "P11p11" +
                "111111" +
                "1p1111" +
                "P11111",
                Turn.White),
                30, 20,
                new BoardState(
                "" +
                "111111" +
                "111111" +
                "P11p11" +
                "11P111" +
                "111111" +
                "111111",
                Turn.White,
                20));
        }

        [TestMethod()]
        public void TurnNotToggleAfterQueenMove()
        {
            AssertMove(
                new BoardState(
                "111Q11" +
                "111111" +
                "1p1111" +
                "111111" +
                "1p1111" +
                "111111",
                Turn.White),
                3, 18,
                new BoardState(
                "111111" +
                "111111" +
                "111111" +
                "Q11111" +
                "1p1111" +
                "111111",
                Turn.White,
                18)

            );
        }

        private MoveFigureService CreateMoveFigureService()
        {

            var validateFiguresService = new ValidateFiguresService(
                new ValidateService(
                    new ValidatePawnService(),
                    new ValidateQueenService()));
            
            return new MoveFigureService(                
                new ValidateBoardService(validateFiguresService),
                new DirectMoveService(new ValidateService(
                    new ValidatePawnService(),
                    new ValidateQueenService())));

        }

        private void AssertMove(BoardState from, int fromCoord, int toCoord, BoardState expected)
        {            
            var moveService = CreateMoveFigureService();

            var actual = moveService.Move(fromCoord, toCoord, from);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void BlockedByOtherFigureThatMustMove()
        {
            AssertMove(

                new BoardState(
                "111111" +
                "1p1111" +
                "P11111" +
                "11P111" +
                "111111" +
                "111111",
                Turn.White,
                20),
                12, 2,
                new BoardState(
                "111111" +
                "1p1111" +
                "P11111" +
                "11P111" +
                "111111" +
                "111111",
                Turn.White,
                20));
        }


        [TestMethod()]
        public void CannotMoveOnOtherColorTurn()
        {
            AssertMove(
                new BoardState(
                "111" +
                "1p1" +
                "P11",
                Turn.Black),
                6, 2,
                new BoardState(
                "111" +
                "1p1" +
                "P11",
                Turn.Black
                ));
        }

        [TestMethod()]
        public void CannotMoveToOtherColorPawnPosition()
        {

            AssertMove(
                new BoardState(
                "111" +
                "1p1" +
                "P11",
                Turn.White),
                6, 4,
                new BoardState(
                "111" +
                "1p1" +
                "P11",
                Turn.White));
        }
    }
}