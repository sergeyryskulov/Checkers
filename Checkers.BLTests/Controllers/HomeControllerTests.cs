using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Checkers.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            var homeController = new HomeController();

            var actual = homeController.Index() as RedirectResult;

            Assert.AreEqual("/Board", actual.Url);
        }
    }
}