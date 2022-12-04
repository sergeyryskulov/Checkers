using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.Core.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Checkers.Core.Constants;
using Checkers.DomainModels;
using Checkers.DomainModels.Enums;

namespace Checkers.Core.Models.ValueObjects.Tests
{
    [TestClass()]
    public class GameStateTests
    {
        [TestMethod()]
        public void EqualsFalse()
        {
            var gameState = new GameState("1111", Turn.Black, null);

            var actual = gameState.Equals(new GameState("1111", Turn.White, null));

            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public void EqualsTrue()
        {
            var gameState = new GameState("1111", Turn.Black, null);

            var actual = gameState.Equals(new GameState("1111", Turn.Black, null));

            Assert.IsTrue(actual);
        }
    }
}