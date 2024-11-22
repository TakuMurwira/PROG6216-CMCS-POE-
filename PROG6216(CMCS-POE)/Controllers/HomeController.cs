using Microsoft.AspNetCore.Mvc;
using PROG6216_CMCS_POE_.Models;
using System.Diagnostics;

namespace PROG6216_CMCS_POE_.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Message = $"Welcome, {User.Identity.Name}!";
            }
            else
            {
                ViewBag.Message = "Please log in to access the site.";
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
