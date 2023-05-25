using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mithril.Admin.Controllers
{
    /// <summary>
    /// Admin controller
    /// TODO: Add tests
    /// </summary>
    /// <seealso cref="Controller" />
    [Area("Admin")]
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/Admin")]
    public class AdminController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class.
        /// </summary>
        /// <param name="editors">The editors.</param>
        public AdminController()//IEnumerable<IEditor> editors)
        {
            //Editors = editors;
        }

        /// <summary>
        /// Gets the editors.
        /// </summary>
        /// <value>The editors.</value>
        //public IEnumerable<IEditor> Editors { get; }

        /// <summary>
        /// Returns the admin home page.
        /// </summary>
        /// <returns>The view.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();// Editors.Where(x => x.CanView(User)).ToList());
        }
    }
}