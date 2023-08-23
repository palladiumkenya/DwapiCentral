using Microsoft.AspNetCore.Mvc;

namespace DwapiCentral.Mnch.Controllers
{
    public class MnchLabController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
