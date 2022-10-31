using Checkers.Core.Constants;
using Checkers.Core.Interfaces;
using Checkers.Core.Models.ValueObjects;
using Checkers.Core.Services;
using Checkers.Intellect.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkers.IntegrationTests.Services
{
    [TestClass()]
    public class IntellectServiceTests
    {

        [TestMethod()]
        public void PawnToQueen()
        {
            AssertEqual(
                new BoardState(
                    "p1" +
                    "11",
                    Turn.Black
                ),
                new BoardState(

                    "11" +
                    "1q",
                    Turn.BlackWin
                ));
        }


        [TestMethod()]
        public void IntellectStep_NoWhiteMove()
        {
            AssertNotEqual(
                new BoardState(
                                    
                    "1111111p" +
                    "111111p1" +
                    "1p111p1p" +
                    "p11111p1" +
                    "1p11111P" +
                    "11111111" +
                    "111P1111" +
                    "11q11111",
                     Turn.Black),                                
                new BoardState(
                "1111111p" +
                "111111p1" +
                "1p111p1p" +
                "p11111p1" +
                "1p11111P" +
                "11111111" +
                "111P1111" +
                "11q11111",
                Turn.Black));
        }

        [TestMethod()]
        public void TwoTurn()
        {

            AssertEqual(
                new BoardState(
                "111111" +
                "111111" +
                "111111" +
                "111111" +
                "1p1p11" +
                "P1P111",
                Turn.White),
                new BoardState(
                "111111" +
                "111111" +
                "111111" +
                "11P111" +
                "111p11" +
                "11P111",
                Turn.White,
                20));

        }

        [TestMethod()]
        public void IntellectStep_QueenWeightTest()
        {
            AssertEqual(
                new BoardState(
                "11Q" + 
                "111" + 
                "q11",
                Turn.Black),
                new BoardState(
                "11Q" +
                "1q1" +
                "111",
                Turn.White)
            );

        }


        [TestMethod()]
        public void IntellectStepMustGoTest()
        {
            
            AssertNotEqual(
                new BoardState(
                "1p1p1p1p" +
                "p111p111" +
                "1111111p" +
                "1111p111" +
                "11111111" +
                "P11111P1" +
                "1P1P1p11" +
                "P1P1P1P1",
                Turn.Black,
                53),
                new BoardState(

                "1p1p1p1p" +
                "p111p111" +
                "1111111p" +
                "1111p111" +
                "11111111" +
                "P11111P1" +
                "1P1P1p11" +
                "P1P1P1P1",
                Turn.Black,
                53));

        }

        private IntellectService GetIntellectService()
        {
            var valideteFigureService = new  ValidateService(
                new ValidatePawnService(),
                new ValidateQueenService()
            );

            var directMoveService = new DirectMoveService(
                new ValidateService(
                    new ValidatePawnService(), 
                    new ValidateQueenService()
                    ) as IValidateEatService
            );

            var stepIteratorService = new StepIteratorService(
                    new ValidateFiguresService(valideteFigureService),
                    directMoveService
                );
            
            return new IntellectService(                
                stepIteratorService,
                new PositionWeightService());
        }

        private void AssertEqual(BoardState from, BoardState to)
        {            
            var service = GetIntellectService();

            var actual = service.CalculateStep(from);

            var expected = to;

            Assert.AreEqual(expected, actual);
        }

        private void AssertNotEqual(BoardState from, BoardState to)
        {                        
            var service = GetIntellectService();

            var actual = service.CalculateStep(from);

            var expected = to;

            Assert.AreNotEqual(expected, actual);
        }
    }
}