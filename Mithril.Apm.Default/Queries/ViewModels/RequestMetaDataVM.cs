using Mithril.Apm.Default.Models;

namespace Mithril.Apm.Default.Queries.ViewModels
{
    /// <summary>
    /// RequestMetaData VM
    /// </summary>
    public class RequestMetaDataVM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestMetaDataVM"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public RequestMetaDataVM(RequestMetaData model)
        {
            if (model is null)
                return;
            DisplayName = model.DisplayName;
            MetaData = model.MetaData;
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <value>The display name.</value>
        public string? DisplayName { get; }

        /// <summary>
        /// Gets the meta data.
        /// </summary>
        /// <value>The meta data.</value>
        public string? MetaData { get; }
    }
}