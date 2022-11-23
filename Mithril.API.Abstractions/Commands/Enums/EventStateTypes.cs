using BigBook.Patterns.BaseClasses;
using Mithril.Data.Abstractions.Enums;
using System.Globalization;

namespace Mithril.API.Abstractions.Commands.Enums
{
    /// <summary>
    /// Event state types
    /// </summary>
    /// <seealso cref="StringEnumBaseClass&lt;EventStateTypes&gt;"/>
    public class EventStateTypes : StringEnumBaseClass<EventStateTypes>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaimTypes"/> class.
        /// </summary>
        public EventStateTypes()
            : base("")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaimTypes"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public EventStateTypes(string name)
            : base(name)
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

        /// <summary>
        /// The name mapping
        /// </summary>
        private static Dictionary<string, EventStateTypes> NameMapping { get; } = new Dictionary<string, EventStateTypes>
        {
            [Completed.ToString().ToUpper(CultureInfo.InvariantCulture)] = Completed,
            [Created.ToString().ToUpper(CultureInfo.InvariantCulture)] = Created,
            [Retrying.ToString().ToUpper(CultureInfo.InvariantCulture)] = Retrying,
            [Error.ToString().ToUpper(CultureInfo.InvariantCulture)] = Error,
        };

        /// <summary>
        /// Gets the enum types.
        /// </summary>
        /// <returns>The various enum types.</returns>
        public static IEnumerable<EventStateTypes> GetTypes() => NameMapping.Values;

        /// <summary>
        /// Gets the type of the contact information.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The contact info type specified.</returns>
        public static EventStateTypes? GeType(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            var KeyName = name.ToUpper(CultureInfo.InvariantCulture).Replace("-", "", StringComparison.OrdinalIgnoreCase);
            return NameMapping.ContainsKey(KeyName) ? NameMapping[KeyName] : new EventStateTypes(name);
        }
    }
}