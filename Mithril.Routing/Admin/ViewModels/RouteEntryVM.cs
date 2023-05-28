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
    public class RouteEntryVM : IEntity<RouteEntry>
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
        {
            if (model is null)
                return;
            ID = model.ID;
            Active = model.Active;
            InputPath = model.InputPath;
            OutputPath = model.OutputPath;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see
        /// cref="T:Mithril.Admin.Abstractions.Interfaces.IEntity"/> is active.
        /// </summary>
        /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public long ID { get; set; }

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
        public Task<RouteEntry?> SaveAsync(long id, IDataService dataService) => Task.FromResult<RouteEntry?>(null);
    }
}