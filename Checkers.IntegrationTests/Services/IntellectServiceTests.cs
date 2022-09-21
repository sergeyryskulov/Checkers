using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.BL.Constants;
using Checkers.BL.Interfaces;
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
                "11P111" +
                "111p11" +
                "11P111w20");

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

        private void AssertEqual(string from, string to)
        {            
            var service = GetIntellectService();

            string actual = service.IntellectStep(from);

            string expected = to;

            Assert.AreEqual(expected, actual);
        }

        private void AssertNotEqual(string from, string to)
        {                        
            var service = GetIntellectService();

            string actual = service.IntellectStep(from);

            string expected = to;

            Assert.AreNotEqual(expected, actual);
        }
    }
}