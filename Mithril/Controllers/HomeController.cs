using Microsoft.AspNetCore.Mvc;
using Mithril.Core.Abstractions.Services;
using Mithril.Models;
using System.Diagnostics;

namespace Mithril.Controllers
{
    /// <summary>
    /// Home controller
    /// </summary>
    /// <seealso cref="Controller"/>
    public class HomeController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="securityService">The security service.</param>
        public HomeController(ILogger<HomeController> logger, ISecurityService securityService)
        {
            _logger = logger;
            this.securityService = securityService;
        }

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// The security service
        /// </summary>
        private readonly ISecurityService securityService;

        /// <summary>
        /// Errors this instance.
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewBag.UserName = securityService.LoadCurrentUser()?.FullName;
            return View();
        }

        /// <summary>
        /// Privacies this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Privacy()
        {
            return View();
        }
    }
}