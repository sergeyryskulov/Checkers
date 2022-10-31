using Microsoft.AspNetCore.Mvc;

namespace Checkers.Web.Controllers
{
    public class GameController : Controller 
    {
        public IActionResult Index()
        {
            return View("Game");
        }
    }
}
