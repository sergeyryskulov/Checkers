using Microsoft.AspNetCore.Mvc;

namespace Checkers.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("/Game");
        }
    }
}
