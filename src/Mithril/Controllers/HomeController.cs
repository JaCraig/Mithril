using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Mithril.Communication.Abstractions.Services;
using Mithril.Core.Abstractions.Mvc.Attributes;
using Mithril.Models;
using Mithril.Security.Abstractions.Services;
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
        /// <param name="communicationService">The communication service.</param>
        public HomeController(ILogger<HomeController> logger, ISecurityService securityService, ICommunicationService communicationService)
        {
            _Logger = logger;
            _SecurityService = securityService;
            _CommunicationService = communicationService;
        }

        /// <summary>
        /// The communication service
        /// </summary>
        private readonly ICommunicationService _CommunicationService;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<HomeController> _Logger;

        /// <summary>
        /// The security service
        /// </summary>
        private readonly ISecurityService _SecurityService;

        /// <summary>
        /// Email test.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> EmailTest()
        {
            Communication.Abstractions.Interfaces.IMessage? Message = _CommunicationService.CreateMessage("Email");
            if (Message is null)
                return RedirectToAction("Index");
            Message.Template = "Template1";
            Message.From = "ThatGuy";
            Message.To = "ThatOtherGuy";
            Message.Subject = "That Thing";
            _ = await _CommunicationService.SendMessageAsync(Message, User).ConfigureAwait(false);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Errors this instance.
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewBag.UserName = _SecurityService.LoadCurrentUser()?.FullName;
            return View();
        }

        /// <summary>
        /// Indexes the specified form.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index(IFormCollection form) => RedirectToAction();

        /// <summary>
        /// Privacies this instance.
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "Test")]
        [FeatureGate(MyFeatureFlags.ExampleFlag)]
        [IPFilter("AdminSection")]
        public IActionResult Privacy() => View();
    }

    /// <summary>
    /// Example feature flags controlled by config
    /// </summary>
    public enum MyFeatureFlags
    {
        /// <summary>
        /// The example flag
        /// </summary>
        ExampleFlag
    }
}