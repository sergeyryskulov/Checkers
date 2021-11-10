
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkers.Controllers
{
    public class BoardController : Controller
    {

        public IActionResult Index()
        {
            return View("Board");
        }
    }
}
