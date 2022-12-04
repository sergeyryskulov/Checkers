using Checkers.Core.Constants;
using Checkers.Core.Interfaces;
using Checkers.Core.Services;
using Checkers.DomainModels;
using Checkers.DomainModels.Enums;
using Checkers.Intellect.Services;
using Checkers.Rules.Interfaces;
using Checkers.Rules.Services;
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
                new GameState(
                    "p1" +
                    "11",
                    Turn.Black
                ),
                new GameState(

                    "11" +
                    "1q",
                    Turn.BlackWin
                ));
        }


        [TestMethod()]
        public void IntellectStep_NoWhiteMove()
        {
            AssertNotEqual(
                new GameState(
                                    
                    "1111111p" +
                    "111111p1" +
                    "1p111p1p" +
                    "p11111p1" +
                    "1p11111P" +
                    "11111111" +
                    "111P1111" +
                    "11q11111",
                     Turn.Black),                                
                new GameState(
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
                new GameState(
                "111111" +
                "111111" +
                "111111" +
                "111111" +
                "1p1p11" +
                "P1P111",
                Turn.White),
                new GameState(
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
                new GameState(
                "11Q" + 
                "111" + 
                "q11",
                Turn.Black),
                new GameState(
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
                new GameState(
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
                new GameState(

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

        private ComputerPlayerService GetIntellectService()
        {
            var valideteFigureService = new  ValidateService(
                new ValidatePawnService(),
                new ValidateQueenService()
            );

            var directMoveService = new MoveRulesService(
                new ValidateService(
                    new ValidatePawnService(), 
                    new ValidateQueenService()
                    ) as IValidateEatService
            );

            var stepIteratorService = new StepIteratorService(
                    new ValidateRulesService(valideteFigureService),
                    directMoveService
                );
            
            return new ComputerPlayerService(                
                stepIteratorService,
                new PositionWeightService());
        }

        private void AssertEqual(GameState from, GameState to)
        {            
            var service = GetIntellectService();

            var actual = service.CalculateNextStep(from);

            var expected = to;

            Assert.AreEqual(expected.Board.ToString(), actual.Board.ToString());
            Assert.AreEqual(expected.Turn, actual.Turn);
            Assert.AreEqual(expected.MustGoFromPosition, actual.MustGoFromPosition);
        }

        private void AssertNotEqual(GameState from, GameState to)
        {                        
            var service = GetIntellectService();

            var actual = service.CalculateNextStep(from);

            var expected = to;

            Assert.IsTrue(!actual.Equals(expected)
            );
        }
    }
}