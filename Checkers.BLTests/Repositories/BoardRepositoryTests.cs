using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ckeckers.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ckeckers.DAL.Repositories.Tests
{
    [TestClass()]
    public class BoardRepositoryTests
    {
        [TestMethod()]
        public void SaveTest()
        {
            var boardRepository = new BoardRepository();

            boardRepository.Save("testkey", "testvalue");

            var actual = boardRepository.Load("testkey");

            Assert.AreEqual("testvalue", actual);


        }

        [TestMethod()]
        public void LoadTest()
        {
            var boardRepository = new BoardRepository();

            var defaultData = boardRepository.Load("testkey2");

            Assert.AreEqual('1', defaultData[0]); ;            
        }
    }
}