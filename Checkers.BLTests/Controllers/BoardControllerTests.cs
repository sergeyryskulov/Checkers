using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.Controllers;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Checkers.Controllers.Tests
{

    [TestClass()]
    public class BoardControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            var boardController = new GameController();
            var actual = boardController.Index() as ViewResult;

          // actual.ExecuteResult(new ActionContext(new DefaultHttpContext(), ));

            Assert.IsNull(actual.Model);

            Assert.AreEqual("Board", actual.ViewName);
        }
    }
}