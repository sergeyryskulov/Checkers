using Checkers.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkers.UnitTests.Controllers
{
    [TestClass()]
    public class HomeControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            var homeController = new HomeController();

            var actual = homeController.Index() as RedirectResult;

            Assert.AreEqual("/Game", actual.Url);
        }
    }
}