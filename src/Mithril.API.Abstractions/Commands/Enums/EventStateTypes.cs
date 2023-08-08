using BigBook.Patterns.BaseClasses;
using Mithril.Data.Abstractions.BaseClasses;
using Mithril.Data.Abstractions.Enums;

namespace Mithril.API.Abstractions.Commands.Enums
{
    /// <summary>
    /// Event state types
    /// </summary>
    /// <seealso cref="StringEnumBaseClass&lt;EventStateTypes&gt;"/>
    public class EventStateTypes : LookUpEnumBaseClass<EventStateTypes>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaimTypes"/> class.
        /// </summary>
        public EventStateTypes()
            : this("")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaimTypes"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public EventStateTypes(string name)
            : base(name, "fa-user")
        {
        }

        /// <summary>
        /// Gets the completed.
        /// </summary>
        /// <value>The completed.</value>
        public static EventStateTypes Completed { get; } = new EventStateTypes("Completed");

        /// <summary>
        /// Gets the created state.
        /// </summary>
        /// <value>The created state.</value>
        public static EventStateTypes Created { get; } = new EventStateTypes("Created");

        /// <summary>
        /// Gets the error state.
        /// </summary>
        /// <value>The error state.</value>
        public static EventStateTypes Error { get; } = new EventStateTypes("Error");

        /// <summary>
        /// Gets the retrying state.
        /// </summary>
        /// <value>The retrying state.</value>
        public static EventStateTypes Retrying { get; } = new EventStateTypes("Retrying");
    }
}