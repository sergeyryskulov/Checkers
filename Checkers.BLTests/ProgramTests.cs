using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        
        [TestMethod()]
        public void CreateHostBuilderTest()
        {
            Program.CreateHostBuilder(new string[0]);
        }
    }
}