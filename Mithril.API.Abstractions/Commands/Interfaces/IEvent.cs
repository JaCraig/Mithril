using Mithril.Data.Abstractions.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace Mithril.API.Abstractions.Commands.Interfaces
{
    /// <summary>
    /// Event interface
    /// </summary>
    /// <seealso cref="IModel"/>
    public interface IEvent : IEquatable<IEvent>, IModel
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Gets or sets the retry count.
        /// </summary>
        /// <value>The retry count.</value>
        int RetryCount { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        [MaxLength(20)]
        string? State { get; set; }

        /// <summary>
        /// Determines whether this instance can run.
        /// </summary>
        /// <returns><c>true</c> if this instance can run; otherwise, <c>false</c>.</returns>
        bool CanRun();

        /// <summary>
        /// Gets the data within the event.
        /// </summary>
        /// <returns>The data from the event.</returns>
        ExpandoObject GetData();

        /// <summary>
        /// Gets the schema.
        /// </summary>
        /// <returns>The data schema.</returns>
        string GetSchema();
    }
}