using Microsoft.AspNetCore.Mvc;
using Mithril.Core.Abstractions.Services;
using Mithril.Models;
using System.Diagnostics;

namespace Mithril.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private readonly ILogger<HomeController> _logger;

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index(ISecurityService securityService)
        {
            return View(securityService.LoadCurrentUser()?.FullName);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}