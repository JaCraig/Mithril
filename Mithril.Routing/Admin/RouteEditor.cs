using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Data.Abstractions.Services;
using Mithril.Routing.Admin.ViewModels;
using Mithril.Routing.Models;

namespace Mithril.Routing.Admin
{
    /// <summary>
    /// Route editor
    /// </summary>
    /// <seealso cref="EntityEditorBaseClass&lt;RouteEntryVM,RouteEntry&gt;"/>
    public class RouteEditor : EntityEditorBaseClass<RouteEntryVM, RouteEntry>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RouteEditor"/> class.
        /// </summary>
        /// <param name="dataService"></param>
        /// <param name="dataType"></param>
        public RouteEditor(IDataService dataService, string? dataType = null) : base(dataService, dataType)
        {
        }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <value>The icon.</value>
        public override string Icon { get; } = "fas fa-route";

        /// <summary>
        /// Converts the model into the appropriate view model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="full">if set to <c>true</c> [full].</param>
        /// <returns>The view model</returns>
        protected override IEntity Convert(RouteEntry model, bool full = true) => new RouteEntryVM(model);
    }
}