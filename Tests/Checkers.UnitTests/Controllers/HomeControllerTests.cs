using Checkers.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkers.Web.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            var homeController = CreateHomeController();

            var actual = homeController.Index() as RedirectResult;

            Assert.AreEqual("/Game", actual.Url);
        }

        HomeController CreateHomeController()
        {
            return new HomeController();
        }
    }
}
