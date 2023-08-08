using System.Dynamic;

namespace Mithril.Admin.Commands.ViewModels
{
    /// <summary>
    /// Save model command VM
    /// </summary>
    public class SaveModelCommandVM
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public ExpandoObject? Data { get; set; }

        /// <summary>
        /// Gets or sets the type of the entity.
        /// </summary>
        /// <value>The type of the entity.</value>
        public string? EntityType { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public long ID { get; set; }
    }
}