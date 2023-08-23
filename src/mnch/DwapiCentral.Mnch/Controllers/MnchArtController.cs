using Microsoft.AspNetCore.Mvc;

namespace DwapiCentral.Mnch.Controllers
{
    public class MnchArtController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
