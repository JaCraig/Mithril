using Microsoft.Extensions.Hosting;
using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Admin.Abstractions.Services;
using Mithril.Communication.Admin.ViewModels;
using Mithril.Communication.Models;
using Mithril.Data.Abstractions.Services;

namespace Mithril.Communication.Admin
{
    /// <summary>
    /// Message template editor
    /// </summary>
    /// <seealso cref="EntityEditorBaseClass&lt;MessageTemplateVM, MessageTemplate&gt;" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="MessageTemplateEditor" /> class.
    /// </remarks>
    /// <param name="hostEnvironment">The host environment.</param>
    /// <param name="dataService">The data service.</param>
    /// <param name="entityMetadataService">The entity metadata service.</param>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="dataType">Type of the data.</param>
    public class MessageTemplateEditor(IHostEnvironment? hostEnvironment, IDataService? dataService, IEntityMetadataService? entityMetadataService, IServiceProvider? serviceProvider, string? dataType = null) : EntityEditorBaseClass<MessageTemplateVM, MessageTemplate>(dataService, entityMetadataService, serviceProvider, dataType)
    {
        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <value>The icon.</value>
        public override string Icon { get; } = "fas fa-note-sticky";

        /// <summary>
        /// Gets the host environment.
        /// </summary>
        /// <value>
        /// The host environment.
        /// </value>
        private IHostEnvironment? HostEnvironment { get; } = hostEnvironment;

        /// <summary>
        /// Converts the model into the appropriate view model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="full">if set to <c>true</c> [full].</param>
        /// <returns>The view model</returns>
        protected override IEntity Convert(MessageTemplate model, bool full = true) => new MessageTemplateVM(model, HostEnvironment, full);

        /// <summary>
        /// Filters the query by search term.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="searchQuery">The search query.</param>
        /// <returns>The resulting query.</returns>
        protected override IQueryable<MessageTemplate>? FilterQueryBySearchQuery(IQueryable<MessageTemplate>? query, string searchQuery)
        {
            return string.IsNullOrEmpty(searchQuery)
                ? query
                : (query?.Where(messageTemplate => messageTemplate.DisplayName.Contains(searchQuery)));
        }
    }
}