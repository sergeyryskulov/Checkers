using Checkers.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkers.UnitTests.Controllers
{

    [TestClass()]
    public class GameControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            var boardController = new GameController();
            var actual = boardController.Index() as ViewResult;

          // actual.ExecuteResult(new ActionContext(new DefaultHttpContext(), ));

            Assert.IsNull(actual.Model);

            Assert.AreEqual("Game", actual.ViewName);
        }
    }
}