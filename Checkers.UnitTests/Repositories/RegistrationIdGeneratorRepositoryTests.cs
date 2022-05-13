using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ckeckers.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ckeckers.DAL.Repositories.Tests
{
    [TestClass()]
    public class RegistrationIdGeneratorRepositoryTests
    {
        [TestMethod()]
        public void GenerateIdTest()
        {
            var guid = new RegistrationIdGeneratorRepository().GenerateId();
            Assert.IsTrue(guid!="");
            
        }
    }
}