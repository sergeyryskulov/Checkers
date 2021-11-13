using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.BL.Helper;
using Ckeckers.DAL.Repositories;
using Moq;

namespace Checkers.BL.Services.Tests
{
    [TestClass()]
    public class MoveFigureServiceTests
    {
        [TestMethod()]
        public void Move_Correct()
        {

            var boardRepository = new Mock<IBoardRepository>();
            boardRepository.Setup(m => m.Load("")).Returns("p111");

            var service = new MoveFigureService(boardRepository.Object, new VectorHelper(), new MathHelper(), new WhitePawnService(new VectorHelper(), new MathHelper(), new ColorHelper()));

            string result =  service.Move(0, 3, "");


            Assert.IsTrue(result=="111p");
        }
    }
}