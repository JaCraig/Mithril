using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Data.Abstractions.Services;
using Mithril.Routing.Models;
using System.ComponentModel.DataAnnotations;

namespace Mithril.Routing.Admin.ViewModels
{
    /// <summary>
    /// RouteEntry VM
    /// </summary>
    /// <seealso cref="IEntity&lt;RouteEntry&gt;"/>
    public class RouteEntryVM : EntityBaseClass<RouteEntry>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RouteEntryVM"/> class.
        /// </summary>
        public RouteEntryVM()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteEntryVM"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public RouteEntryVM(RouteEntry model)
            : base(model)
        {
            if (model is null)
                return;
            InputPath = model.InputPath;
            OutputPath = model.OutputPath;
        }

        /// <summary>
        /// Gets or sets the input path.
        /// </summary>
        /// <value>The input path.</value>
        [MaxLength(1024)]
        public string? InputPath { get; set; }

        /// <summary>
        /// Gets or sets the output path.
        /// </summary>
        /// <value>The output path.</value>
        [MaxLength(1024)]
        public string? OutputPath { get; set; }

        /// <summary>
        /// Saves the changes asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dataService"></param>
        /// <returns>The async task.</returns>
        public override Task<RouteEntry?> SaveAsync(long id, IDataService dataService) => Task.FromResult<RouteEntry?>(null);
    }
}