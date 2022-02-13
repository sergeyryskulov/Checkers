using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Checkers.Controllers.Tests
{

    [TestClass()]
    public class BoardControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            var boardController = new BoardController();
            var actual = boardController.Index() as ViewResult;

            Assert.IsNull(actual.Model);

            Assert.AreEqual("Board", actual.ViewName);
        }
    }
}