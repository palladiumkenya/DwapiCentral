using Microsoft.AspNetCore.Mvc;

namespace DwapiCentral.Mnch.Controllers
{
    public class MnchImmunizationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
