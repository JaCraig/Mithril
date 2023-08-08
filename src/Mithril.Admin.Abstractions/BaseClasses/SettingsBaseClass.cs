using Mithril.Admin.Abstractions.Interfaces;
using System.Text.Json.Serialization;

namespace Mithril.Admin.Abstractions.BaseClasses
{
    /// <summary>
    /// Base class for settings
    /// </summary>
    /// <seealso cref="IEntity" />
    public class SettingsBaseClass : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsBaseClass"/> class.
        /// </summary>
        protected SettingsBaseClass()
        { }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IEntity"/> is active.
        /// </summary>
        /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
        [JsonIgnore]
        public bool Active { get; set; } = true;

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonIgnore]
        public long ID { get; set; }
    }
}